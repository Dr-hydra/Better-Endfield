package dev.betterendfield.android;

import android.app.Application;
import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.system.Os;

import java.io.File;
import java.util.Set;
import java.util.concurrent.atomic.AtomicBoolean;

import de.robv.android.xposed.IXposedHookLoadPackage;
import de.robv.android.xposed.XC_MethodHook;
import de.robv.android.xposed.XSharedPreferences;
import de.robv.android.xposed.XposedBridge;
import de.robv.android.xposed.XposedHelpers;
import de.robv.android.xposed.callbacks.XC_LoadPackage;

public final class BetterEndfieldXposed implements IXposedHookLoadPackage {
    private static final String MODULE_PACKAGE = "dev.betterendfield.android";
    private static final String TARGET_PACKAGE = "com.hypergryph.endfield";
    private static final String NATIVE_LIBRARY = "libbetterendfield_android.so";
    private static final AtomicBoolean PREPARATION_REQUESTED = new AtomicBoolean();
    private static final AtomicBoolean LOAD_REQUESTED = new AtomicBoolean();
    private static volatile Set<XC_MethodHook.Unhook> renderHooks;

    @Override
    public void handleLoadPackage(XC_LoadPackage.LoadPackageParam loadPackageParam) {
        if (!TARGET_PACKAGE.equals(loadPackageParam.packageName) ||
                !TARGET_PACKAGE.equals(loadPackageParam.processName)) {
            return;
        }

        XposedBridge.log("BetterEndfield.Xposed: target process matched");
        String voiceRules = readVoiceRules();
        String modelConfiguration = readModelConfiguration();
        XposedHelpers.findAndHookMethod(
                Application.class,
                "attach",
                Context.class,
                new XC_MethodHook() {
                    @Override
                    protected void afterHookedMethod(MethodHookParam param) {
                        prepareCatalogsAndInstallNativeLoadTrigger(
                                (Application) param.thisObject,
                                (Context) param.args[0],
                                loadPackageParam.classLoader,
                                voiceRules,
                                modelConfiguration);
                    }
                });
    }

    private static void prepareCatalogsAndInstallNativeLoadTrigger(
            Application targetApplication,
            Context targetContext,
            ClassLoader targetClassLoader,
            String voiceRules,
            String modelConfiguration) {
        if (voiceRules.isEmpty() && modelConfiguration.isEmpty()) {
            XposedBridge.log(
                    "BetterEndfield.Xposed: no Android modules selected; native runtime skipped");
            return;
        }
        if (!PREPARATION_REQUESTED.compareAndSet(false, true)) {
            return;
        }

        Thread worker = new Thread(() -> {
            if (!voiceRules.isEmpty()) {
                try {
                    Context moduleContext = targetContext.createPackageContext(
                            MODULE_PACKAGE,
                            Context.CONTEXT_IGNORE_SECURITY | Context.CONTEXT_INCLUDE_CODE);
                    VoiceCatalogMaterializer.Result result = VoiceCatalogMaterializer.prepare(
                            targetContext,
                            moduleContext,
                            voiceRules);
                    XposedBridge.log(
                            "BetterEndfield.Xposed: Android voice catalog preparation complete; " +
                                    result.summary());
                    for (String failure : result.failures()) {
                        XposedBridge.log(
                                "BetterEndfield.Xposed: catalog preparation warning: " + failure);
                    }
                } catch (Throwable error) {
                    XposedBridge.log(
                            "BetterEndfield.Xposed: Android voice catalog preparation failed; " +
                                    "other configured modules will still start: " + error);
                }
            }
            installNativeLoadTrigger(
                    targetApplication,
                    targetContext,
                    targetClassLoader,
                    voiceRules,
                    modelConfiguration);
        }, "BetterEndfield-Catalog");
        worker.setDaemon(true);
        worker.start();
    }

    private static void installNativeLoadTrigger(
            Application targetApplication,
            Context targetContext,
            ClassLoader targetClassLoader,
            String voiceRules,
            String modelConfiguration) {
        try {
            Class<?> unityPlayer = XposedHelpers.findClass(
                    "com.unity3d.player.UnityPlayer",
                    targetClassLoader);
            renderHooks = XposedBridge.hookAllMethods(
                    unityPlayer,
                    "nativeRender",
                    new XC_MethodHook() {
                        @Override
                        protected void afterHookedMethod(MethodHookParam param) {
                            if (!Boolean.TRUE.equals(param.getResult())) {
                                return;
                            }
                            loadNativeLibrary(
                                    targetApplication,
                                    targetContext,
                                    voiceRules,
                                    modelConfiguration);
                            if (LOAD_REQUESTED.get()) {
                                removeNativeLoadTrigger();
                            }
                        }
                    });
            if (renderHooks.isEmpty()) {
                throw new NoSuchMethodException("UnityPlayer.nativeRender");
            }
            XposedBridge.log(
                    "BetterEndfield.Xposed: waiting for the first successful Unity frame; " +
                            "voice=" + !voiceRules.isEmpty() +
                            " model=" + !modelConfiguration.isEmpty());
        } catch (Throwable error) {
            XposedBridge.log(
                    "BetterEndfield.Xposed: failed to install Unity frame trigger: " + error);
        }
    }

    private static void removeNativeLoadTrigger() {
        Set<XC_MethodHook.Unhook> hooks = renderHooks;
        renderHooks = null;
        if (hooks == null) {
            return;
        }
        for (XC_MethodHook.Unhook hook : hooks) {
            hook.unhook();
        }
    }

    private static void loadNativeLibrary(
            Application targetApplication,
            Context targetContext,
            String voiceRules,
            String modelConfiguration) {
        if (!LOAD_REQUESTED.compareAndSet(false, true)) {
            return;
        }

        try {
            Context moduleContext = targetContext.createPackageContext(
                    MODULE_PACKAGE,
                    Context.CONTEXT_IGNORE_SECURITY | Context.CONTEXT_INCLUDE_CODE);
            ApplicationInfo moduleInfo = moduleContext.getApplicationInfo();
            File library = new File(moduleInfo.nativeLibraryDir, NATIVE_LIBRARY);
            if (!library.isFile()) {
                throw new IllegalStateException("native library was not extracted: " + library);
            }

            File catalogRoot = new File(
                    targetContext.getFilesDir(), "betterendfield/catalog");
            Os.setenv("BETTER_ENDFIELD_VOICE_RULES", voiceRules, true);
            Os.setenv("BETTER_ENDFIELD_MODEL_CONFIG", modelConfiguration, true);
            Os.setenv(
                    "BETTER_ENDFIELD_VOICE_CATALOG_ROOT",
                    catalogRoot.getAbsolutePath(),
                    true);
            if (BuildConfig.DEBUG) {
                Os.setenv(
                        "BETTER_ENDFIELD_DIAGNOSTICS_PATH",
                        new File(targetContext.getCacheDir(),
                                "betterendfield-diagnostics.log").getAbsolutePath(),
                        true);
            }

            loadIntoTargetNamespace(
                    library.getAbsolutePath(),
                    targetContext.getClassLoader(),
                    targetApplication.getClass());
            XposedBridge.log(
                    "BetterEndfield.Xposed: native runtime loaded; voice=" +
                            !voiceRules.isEmpty() + " model=" +
                            !modelConfiguration.isEmpty() + " library=" + library);
        } catch (Throwable error) {
            LOAD_REQUESTED.set(false);
            XposedBridge.log("BetterEndfield.Xposed: native runtime load failed: " + error);
        }
    }

    private static String readVoiceRules() {
        try {
            XSharedPreferences preferences = new XSharedPreferences(
                    MODULE_PACKAGE,
                    "module_settings");
            preferences.reload();
            String value = preferences.getString("voice_language_rules", "");
            if (value.isEmpty()) {
                String legacy = preferences.getString("voice_catalogs", "");
                value = legacy
                        .replace("aglina", "chr_0013_aglina:Japanese")
                        .replace("liino", "chr_0035_liino:Japanese")
                        .replace(',', ';');
            }
            XposedBridge.log(
                    "BetterEndfield.Xposed: settings file=" + preferences.getFile() +
                            " readable=" + preferences.getFile().canRead() +
                            " voice_rules=" + value);
            return value.isEmpty() ? readDebugVoiceRules() : value;
        } catch (Throwable error) {
            XposedBridge.log(
                    "BetterEndfield.Xposed: settings unavailable; using FollowGlobal: " +
                            error);
        }
        return readDebugVoiceRules();
    }

    private static String readDebugVoiceRules() {
        if (!BuildConfig.DEBUG) {
            return "";
        }
        try {
            Class<?> systemProperties = XposedHelpers.findClass(
                    "android.os.SystemProperties",
                    null);
            String raw = (String) XposedHelpers.callStaticMethod(
                    systemProperties,
                    "get",
                    "debug.betterendfield.voice_rules",
                    "");
            return raw.matches("([*]|chr_[a-zA-Z0-9_]+):(Chinese|English|Japanese|Korean)(;([*]|chr_[a-zA-Z0-9_]+):(Chinese|English|Japanese|Korean))*")
                    ? raw : "";
        } catch (Throwable ignored) {
            return "";
        }
    }

    private static String readModelConfiguration() {
        try {
            XSharedPreferences preferences = new XSharedPreferences(
                    MODULE_PACKAGE,
                    "module_settings");
            preferences.reload();
            String value = preferences.getString("model_configuration", "");
            if (value == null) value = "";
            XposedBridge.log(
                    "BetterEndfield.Xposed: model configuration readable=" +
                            preferences.getFile().canRead() +
                            " enabled=" + !value.isEmpty());
            return value;
        } catch (Throwable error) {
            XposedBridge.log(
                    "BetterEndfield.Xposed: model configuration unavailable: " + error);
            return "";
        }
    }

    private static void loadIntoTargetNamespace(
            String libraryPath,
            ClassLoader targetClassLoader,
            Class<?> targetCaller) {
        Object error;
        try {
            error = XposedHelpers.callStaticMethod(
                    Runtime.class,
                    "nativeLoad",
                    libraryPath,
                    targetClassLoader,
                    targetCaller);
        } catch (NoSuchMethodError threeArgumentMethodUnavailable) {
            error = XposedHelpers.callStaticMethod(
                    Runtime.class,
                    "nativeLoad",
                    libraryPath,
                    targetClassLoader);
        }

        if (error != null) {
            throw new UnsatisfiedLinkError(error.toString());
        }
    }
}
