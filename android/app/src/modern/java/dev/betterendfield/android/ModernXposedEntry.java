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

/** API 102 adapter; never links or loads the legacy Xposed API. */
public final class ModernXposedEntry extends XposedModule {
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
            String voice = readString(settings, "voice_language_rules");
            String model = readString(settings, "model_configuration");
            String enhancement = readString(settings, "enhancement_configuration");
            hook(Application.class.getDeclaredMethod("attach", Context.class)).intercept(chain -> {
                Object result = chain.proceed();
                try {
                    RuntimeBootstrap.prepare((Application) chain.getThisObject(), (Context) chain.getArg(0),
                            param.getClassLoader(), voice, model, enhancement, this::installFrames, this::report);
                } catch (Throwable error) { report("bootstrap failed: " + error); }
                return result;
            });
            report("API 102 attached to " + param.getPackageName());
            startRemoteCommandPoller();
        } catch (Throwable error) { report("entry failed: " + error); }
    }

    private void installFrames(ClassLoader loader, java.util.function.BooleanSupplier callback) throws Throwable {
        Class<?> unity = Class.forName("com.unity3d.player.UnityPlayer", false, loader);
        CopyOnWriteArrayList<HookHandle> hooks = new CopyOnWriteArrayList<>();
        AtomicBoolean complete = new AtomicBoolean();
        try {
            for (Method method : unity.getDeclaredMethods()) {
                if (!method.getName().equals("nativeRender") || method.getReturnType() != boolean.class) continue;
                HookHandle handle = hook(method).intercept(chain -> {
                    Object result = chain.proceed();
                    if (Boolean.TRUE.equals(result) && !complete.get() && callback.getAsBoolean()) {
                        complete.set(true);
                        hooks.forEach(HookHandle::unhook);
                    }
                    return result;
                });
                hooks.add(handle);
                if (complete.get()) handle.unhook();
            }
            if (hooks.isEmpty()) throw new NoSuchMethodException("UnityPlayer.nativeRender");
        } catch (Throwable error) {
            hooks.forEach(HookHandle::unhook);
            throw error;
        }
    }

    private void report(String message) { log(Log.INFO, "BetterEndfield.Modern", message); }

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

    private static String readString(SharedPreferences settings, String key) {
        String value = settings.getString(key, "");
        return value == null ? "" : value;
    }
}
