package dev.betterendfield.android;

import android.app.Application;
import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;

import java.lang.reflect.Method;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.atomic.AtomicBoolean;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import android.os.ParcelFileDescriptor;

import io.github.libxposed.api.XposedModule;

/** The module's only framework entry point: libxposed API 102. */
public final class XposedEntry extends XposedModule {
    private String processName;
    private final AtomicBoolean attached = new AtomicBoolean();
    private final AtomicBoolean nativeReady = new AtomicBoolean();

    @Override public void onModuleLoaded(ModuleLoadedParam param) {
        processName = param.getProcessName();
    }

    @Override public void onPackageReady(PackageReadyParam param) {
        if (!RuntimeBootstrap.isTarget(param.getPackageName(), processName)
                || !attached.compareAndSet(false, true)) return;
        try {
            SharedPreferences settings = getRemotePreferences("module_settings");
            if (settings.getInt("schemaVersion", 1) != 1) {
                report("unsupported settings schema; native runtime disabled");
                return;
            }
            OverlaySettingsClient.initialize(()->getRemotePreferences("module_settings"));
            ModuleConfigurations configs = ModuleConfigurations.read(settings);
            hook(Application.class.getDeclaredMethod("attach", Context.class)).intercept(chain -> {
                Object result = chain.proceed();
                try {
                    GameOverlay.install((Application) chain.getThisObject(), param.getClassLoader(),
                            () -> {
                                // The service-backed preferences proxy can be
                                // stale after the settings Activity commits a
                                // new snapshot. Re-acquire it when an Activity
                                // resumes or the visible deck polls, so appearance
                                // changes also reach the already running game.
                                try {
                                    return getRemotePreferences("module_settings");
                                } catch (RuntimeException unavailable) {
                                    return settings;
                                }
                            });
                    Application application=(Application) chain.getThisObject();
                    installPcMouseCapture(application);
                    Context context=(Context) chain.getArg(0);
                    new Thread(() -> {
                        String initialIndex=settings.getString(BemInstaller.INDEX,"[]");
                        boolean skipValidation=settings.getBoolean(BemInstaller.SKIP_VALIDATION,false);
                        boolean hotSwitch=settings.getBoolean(BemInstaller.HOT_SWITCH,false);
                        boolean fastLoading=settings.getBoolean(BemInstaller.FAST_LOADING,false);
                        boolean installedPrepared=false;
                        BemInstalledResources.Source modelSource=
                            name -> new ParcelFileDescriptor.AutoCloseInputStream(openRemoteFile(name));
                        try {
                            BemInstalledResources.configuration=BemInstalledResources.prepare(context,
                                initialIndex,modelSource,this::report,skipValidation,hotSwitch,fastLoading,true);
                            installedPrepared=true;
                        } catch(Exception error) {report("Installed BEM preparation failed: "+error);}
                        if (settings.getBoolean(ModuleSettings.MMD_ENABLED, false)) try {
                            MmdInstalledResources.prepare(context, settings.getString(MmdInstaller.INDEX, "[]"),
                                    name -> new ParcelFileDescriptor.AutoCloseInputStream(openRemoteFile(name)), this::report);
                        } catch (Exception error) { report("MMD preparation failed: " + error); }
                        try { ThirdPartyRuntimeMaterializer.prepare(context, configs.thirdParty(),
                                name -> new ParcelFileDescriptor.AutoCloseInputStream(openRemoteFile(name)), this::report); }
                        catch (Exception error) { report("Third-party preparation failed: " + error); }
                        RuntimeBootstrap.prepare(application,context,param.getClassLoader(),configs,this::installFrames,this::report);
                        GlobalFovUpdater.start(()->getRemotePreferences("module_settings"));
                        ThirdPartyRuntimeUpdater.start(context,()->getRemotePreferences("module_settings"),
                            name -> new ParcelFileDescriptor.AutoCloseInputStream(openRemoteFile(name)), configs.thirdParty(), this::report);
                        if(installedPrepared) BemHotSwitchUpdater.start(context,
                            ()->getRemotePreferences("module_settings"),modelSource,initialIndex,
                            skipValidation,hotSwitch,fastLoading,this::report);
                    },"BetterEndfield-InstalledModels").start();
                } catch (Throwable error) { report("bootstrap failed: " + error); }
                return result;
            });
            report("attached to " + param.getPackageName());
            startRemoteCommandPoller();
        } catch (Throwable error) { report("entry failed: " + error); }
    }

    private void installFrames(ClassLoader loader, java.util.function.BooleanSupplier callback) throws Throwable {
        Class<?> unity = null;
        for (int attempt = 0; attempt < 120; ++attempt) {
            try { unity = Class.forName("com.unity3d.player.UnityPlayer", false, loader); break; }
            catch (ClassNotFoundException missing) { Thread.sleep(100); }
        }
        if (unity == null) throw new ClassNotFoundException("UnityPlayer did not become available");
        CopyOnWriteArrayList<HookHandle> hooks = new CopyOnWriteArrayList<>();
        AtomicBoolean complete = new AtomicBoolean();
        try {
            for (Method method : unity.getDeclaredMethods()) {
                if (!method.getName().equals("nativeRender") || method.getReturnType() != boolean.class) continue;
                HookHandle handle = hook(method).intercept(chain -> {
                    Object result = chain.proceed();
                    if (Boolean.TRUE.equals(result)) {
                        if (!complete.get() && callback.getAsBoolean()) complete.set(true);
                        if (RuntimeBootstrap.loaded()) {
                            // Already on Unity's nativeRender thread. Never use a
                            // background watchdog to invoke Unity APIs for unpause.
                            NativeCommandBridge.frame();
                        } else if (complete.get()) {
                            hooks.forEach(HookHandle::unhook);
                        }
                    }
                    return result;
                });
                hooks.add(handle);
                if (complete.get() && !RuntimeBootstrap.loaded()) handle.unhook();
            }
            if (hooks.isEmpty()) throw new NoSuchMethodException("UnityPlayer.nativeRender");
        } catch (Throwable error) {
            hooks.forEach(HookHandle::unhook);
            throw error;
        }
    }

    private void installPcMouseCapture(Application application) {
        CopyOnWriteArrayList<HookHandle> hooks = new CopyOnWriteArrayList<>();
        try {
            for (Class<?> type : new Class<?>[]{android.view.View.class, android.view.ViewGroup.class}) {
                hooks.add(hook(type.getDeclaredMethod("dispatchCapturedPointerEvent", android.view.MotionEvent.class))
                        .intercept(chain -> {
                            if (PcUiMouseBridge.capturedEvent((android.view.View) chain.getThisObject(),
                                    (android.view.MotionEvent) chain.getArg(0))) return true;
                            return chain.proceed();
                        }));
            }
            hooks.add(hook(android.view.View.class.getDeclaredMethod("dispatchPointerCaptureChanged", boolean.class))
                    .intercept(chain -> {
                        Object result = chain.proceed();
                        PcUiMouseBridge.captureChanged((android.view.View) chain.getThisObject(), (boolean) chain.getArg(0));
                        return result;
                    }));
            hooks.add(hook(android.view.View.class.getDeclaredMethod("dispatchWindowFocusChanged", boolean.class))
                    .intercept(chain -> {
                        Object result = chain.proceed();
                        PcUiMouseBridge.windowFocusChanged((android.view.View) chain.getThisObject(), (boolean) chain.getArg(0));
                        return result;
                    }));
            PcUiMouseBridge.install(application, this::report);
        } catch (Throwable error) {
            hooks.forEach(HookHandle::unhook);
            report("PC mouse capture bridge unavailable: " + error);
        }
    }

    private void report(String message) { log(Log.INFO, "BetterEndfield.Xposed", message); }

    private void startRemoteCommandPoller() {
        Thread worker = new Thread(() -> {
            String last = "";
            String lastStatus = "";
            long nextNativeProbeAt = 0L;
            while (attached.get()) {
                try {
                    boolean present = false;
                    for (String file : listRemoteFiles()) if ("command.next".equals(file)) { present = true; break; }
                    long now = System.currentTimeMillis();
                    if (present && (nativeReady.get() || now >= nextNativeProbeAt)) {
                        try {
                            String status = NativeCommandBridge.status();
                            nativeReady.set(true);
                            if (status != null && !status.isEmpty() && !status.equals(lastStatus)) {
                                if (FrameworkSettings.writeRemoteStatus(status)) lastStatus = status;
                            }
                        } catch (UnsatisfiedLinkError | NoSuchMethodError ignored) {
                            nativeReady.set(false);
                            nextNativeProbeAt = now + 10000L;
                        }
                    }
                    if (!present) { Thread.sleep(250); continue; }
                    try (ParcelFileDescriptor descriptor = openRemoteFile("command.next");
                        BufferedReader reader = new BufferedReader(new InputStreamReader(
                                new ParcelFileDescriptor.AutoCloseInputStream(descriptor),
                                java.nio.charset.StandardCharsets.UTF_8))) {
                        StringBuilder content = new StringBuilder(); String line;
                        while ((line = reader.readLine()) != null) content.append(line).append('\n');
                        String value = content.toString();
                        if (!value.isEmpty() && !value.equals(last)) {
                            try { if (NativeCommandBridge.submit(value)) last = value; }
                            catch (UnsatisfiedLinkError | NoSuchMethodError ignored) {
                                nativeReady.set(false);
                                nextNativeProbeAt = System.currentTimeMillis() + 2000L;
                            }
                        }
                    }
                } catch (java.io.FileNotFoundException missing) {
                    // The UI has not issued a command. Avoid repeatedly asking
                    // the framework to open a file that does not exist.
                } catch (Throwable ignored) { /* remote file may be unavailable during reload */ }
                try { Thread.sleep(250); } catch (InterruptedException stopped) { return; }
            }
        }, "BetterEndfield-Commands");
        worker.setDaemon(true); worker.start();
    }
}
