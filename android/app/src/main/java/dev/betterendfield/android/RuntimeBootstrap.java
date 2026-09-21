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
            String voice, String model, String enhancement, FrameTrigger trigger,
            Consumer<String> log) {
        if (voice.isEmpty() && model.isEmpty() && enhancement.isEmpty()
                && !debugResourceProbe() && debugCustomModelConfig().isEmpty()) {
            log.accept("no modules selected; native runtime skipped");
            return;
        }
        if (!PREPARED.compareAndSet(false, true)) return;
        Thread worker = new Thread(() -> {
            if (!voice.isEmpty()) {
                try {
                    Context module = context.createPackageContext(MODULE_PACKAGE,
                            Context.CONTEXT_IGNORE_SECURITY | Context.CONTEXT_INCLUDE_CODE);
                    VoiceCatalogMaterializer.Result result =
                            VoiceCatalogMaterializer.prepare(context, module, voice);
                    log.accept("catalog prepared: " + result.summary());
                    for (String failure : result.failures()) log.accept(failure);
                } catch (Throwable error) {
                    log.accept("catalog preparation failed: " + error);
                }
            }
            try {
                trigger.install(loader, () -> load(application, context, voice, model, enhancement, log));
                log.accept("waiting for first successful Unity frame");
            } catch (Throwable error) {
                log.accept("Unity frame trigger unavailable: " + error);
            }
        }, "BetterEndfield-Catalog");
        worker.setDaemon(true);
        worker.start();
    }

    private static boolean load(Application application, Context context,
            String voice, String model, String enhancement, Consumer<String> log) {
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
            Os.setenv("BETTER_ENDFIELD_VOICE_RULES", voice, true);
            Os.setenv("BETTER_ENDFIELD_MODEL_CONFIG", model, true);
            Os.setenv("BETTER_ENDFIELD_ENHANCEMENT_CONFIG", enhancement, true);
            Os.setenv("BETTER_ENDFIELD_CUSTOM_MODEL_PROBE", debugResourceProbe() ? "1" : "0", true);
            Os.setenv("BETTER_ENDFIELD_CUSTOM_MODEL_CONFIG", debugCustomModelConfig(), true);
            Os.setenv("BETTER_ENDFIELD_VOICE_CATALOG_ROOT",
                    new File(context.getFilesDir(), "betterendfield/catalog").getAbsolutePath(), true);
            if (BuildConfig.DEBUG) Os.setenv("BETTER_ENDFIELD_DIAGNOSTICS_PATH",
                    new File(context.getCacheDir(), "betterendfield-diagnostics.log").getAbsolutePath(), true);
            loadIntoTargetNamespace(library.getAbsolutePath(), context.getClassLoader(), application.getClass());
            loaded = true;
            log.accept("native runtime loaded; voice=" + !voice.isEmpty()
                    + " model=" + !model.isEmpty() + " enhancement=" + !enhancement.isEmpty());
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

    private static String debugCustomModelConfig() {
        if (!BuildConfig.DEBUG) return "";
        try {
            Method get = Class.forName("android.os.SystemProperties")
                    .getDeclaredMethod("get", String.class, String.class);
            String value = (String) get.invoke(null, "debug.betterendfield.custom_model_config", "");
            android.util.Log.i("BetterEndfield.Debug", "custom model config present=" + (value != null && !value.isEmpty()));
            return value == null ? "" : value;
        } catch (ReflectiveOperationException unavailable) { return ""; }
    }
}
