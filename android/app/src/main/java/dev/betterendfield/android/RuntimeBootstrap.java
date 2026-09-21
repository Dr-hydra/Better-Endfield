package dev.betterendfield.android;

import android.app.Application;
import android.content.Context;
import android.system.Os;

import java.io.File;
import java.lang.reflect.Method;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.function.BooleanSupplier;
import java.util.function.Consumer;

/** Framework-independent catalog preparation and first-frame native loading. */
final class RuntimeBootstrap {
    static final String MODULE_PACKAGE = "dev.betterendfield.android";
    private static final AtomicBoolean PREPARED = new AtomicBoolean();
    private static final AtomicBoolean LOADING = new AtomicBoolean();
    private static final AtomicInteger ATTEMPTS = new AtomicInteger();
    private static volatile boolean loaded;

    interface FrameTrigger {
        // Remove the hook when callback returns true (loaded or retries exhausted).
        void install(ClassLoader loader, BooleanSupplier callback) throws Throwable;
    }

    static boolean isTarget(String packageName, String processName) {
        return packageName.equals(processName) && !MODULE_PACKAGE.equals(packageName)
                && !"android".equals(packageName);
    }

    static void prepare(Application application, Context context, ClassLoader loader,
            ModuleConfigurations configs, FrameTrigger trigger, Consumer<String> log) {
        if (configs.none() && !debugResourceProbe() && customModelConfig().isEmpty()) {
            log.accept("no modules selected; native runtime skipped");
            return;
        }
        if (!PREPARED.compareAndSet(false, true)) return;
        Thread worker = new Thread(() -> {
            String poseRoot = "";
            if (!configs.voice().isEmpty() || configs.needsActionPoses()) {
                try {
                    Context module = context.createPackageContext(MODULE_PACKAGE,
                            Context.CONTEXT_IGNORE_SECURITY | Context.CONTEXT_INCLUDE_CODE);
                    if (!configs.voice().isEmpty()) {
                        VoiceCatalogMaterializer.Result result =
                                VoiceCatalogMaterializer.prepare(context, module, configs.voice());
                        log.accept("catalog prepared: " + result.summary());
                        for (String failure : result.failures()) log.accept(failure);
                    }
                    if (configs.needsActionPoses()) {
                        poseRoot = ActionPoseAssets.materialize(context, module, log);
                    }
                } catch (Throwable error) {
                    log.accept("asset preparation failed: " + error);
                }
            }
            String actionPoseRoot = poseRoot;
            try {
                trigger.install(loader,
                        () -> load(application, context, configs, actionPoseRoot, log));
                log.accept("waiting for first successful Unity frame");
            } catch (Throwable error) {
                log.accept("Unity frame trigger unavailable: " + error);
            }
        }, "BetterEndfield-Catalog");
        worker.setDaemon(true);
        worker.start();
    }

    private static boolean load(Application application, Context context,
            ModuleConfigurations configs, String actionPoseRoot, Consumer<String> log) {
        if (loaded || ATTEMPTS.get() >= 3) return true;
        if (!LOADING.compareAndSet(false, true)) return false;
        try {
            // Recheck after acquiring the guard: another frame may have completed.
            if (loaded || ATTEMPTS.get() >= 3) return true;
            ATTEMPTS.incrementAndGet();
            Context module = context.createPackageContext(MODULE_PACKAGE,
                    Context.CONTEXT_IGNORE_SECURITY | Context.CONTEXT_INCLUDE_CODE);
            File library = new File(module.getApplicationInfo().nativeLibraryDir,
                    "libbetterendfield_android.so");
            if (!library.isFile()) throw new IllegalStateException("missing library: " + library);
            Os.setenv("BETTER_ENDFIELD_VOICE_RULES", configs.voice(), true);
            Os.setenv("BETTER_ENDFIELD_MODEL_CONFIG", configs.model(), true);
            Os.setenv("BETTER_ENDFIELD_UI_CONFIG", configs.ui(), true);
            Os.setenv("BETTER_ENDFIELD_CAMERA_CONFIG", configs.camera(), true);
            Os.setenv("BETTER_ENDFIELD_ACTIONS_CONFIG", configs.actions(), true);
            Os.setenv("BETTER_ENDFIELD_ACTIONS_ASSET_ROOT", actionPoseRoot, true);
            Os.setenv("BETTER_ENDFIELD_CUSTOM_MODEL_PROBE", debugResourceProbe() ? "1" : "0", true);
            Os.setenv("BETTER_ENDFIELD_CUSTOM_MODEL_CONFIG", customModelConfig(), true);
            Os.setenv("BETTER_ENDFIELD_VOICE_CATALOG_ROOT",
                    new File(context.getFilesDir(), "betterendfield/catalog").getAbsolutePath(), true);
            if (BuildConfig.DEBUG) Os.setenv("BETTER_ENDFIELD_DIAGNOSTICS_PATH",
                    new File(context.getCacheDir(), "betterendfield-diagnostics.log").getAbsolutePath(), true);
            loadIntoTargetNamespace(library.getAbsolutePath(), context.getClassLoader(), application.getClass());
            loaded = true;
            log.accept("native runtime loaded; " + configs.summary());
        } catch (Throwable error) {
            log.accept("native runtime load attempt " + ATTEMPTS.get() + "/3 failed: " + error);
        } finally {
            LOADING.set(false);
        }
        return loaded || ATTEMPTS.get() >= 3;
    }

    private static void loadIntoTargetNamespace(String path, ClassLoader loader,
            Class<?> caller) throws ReflectiveOperationException {
        Method method;
        Object[] args;
        try {
            method = Runtime.class.getDeclaredMethod("nativeLoad", String.class, ClassLoader.class, Class.class);
            args = new Object[]{path, loader, caller};
        } catch (NoSuchMethodException unavailable) {
            method = Runtime.class.getDeclaredMethod("nativeLoad", String.class, ClassLoader.class);
            args = new Object[]{path, loader};
        }
        method.setAccessible(true);
        Object error = method.invoke(null, args);
        if (error != null) throw new UnsatisfiedLinkError(error.toString());
    }

    private static boolean debugResourceProbe() {
        if (!BuildConfig.DEBUG) return false;
        try {
            Method get = Class.forName("android.os.SystemProperties")
                    .getDeclaredMethod("get", String.class, String.class);
            return "1".equals(get.invoke(null, "debug.betterendfield.resource_probe", "0"));
        } catch (ReflectiveOperationException unavailable) { return false; }
    }

    static String debugCustomModelConfig() {
        if (!BuildConfig.DEBUG) return "";
        try {
            Method get = Class.forName("android.os.SystemProperties")
                    .getDeclaredMethod("get", String.class, String.class);
            String value = (String) get.invoke(null, "debug.betterendfield.custom_model_config", "");
            android.util.Log.i("BetterEndfield.Debug", "custom model config present=" + (value != null && !value.isEmpty()));
            return value == null ? "" : value;
        } catch (ReflectiveOperationException unavailable) { return ""; }
    }

    static String customModelConfig() {
        String debug=debugCustomModelConfig();
        return debug.isEmpty()?BemInstalledResources.configuration:debug;
    }
}
