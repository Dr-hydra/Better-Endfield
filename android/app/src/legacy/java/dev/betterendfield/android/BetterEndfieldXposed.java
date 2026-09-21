package dev.betterendfield.android;

import android.app.Application;
import android.content.Context;

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
    private static final String SYSTEM_PACKAGE = "android";

    /**
     * Runs in whatever the user put in the module's scope, as long as it is that
     * app's own main process.
     *
     * The package name is deliberately not checked against a list. The game
     * ships under at least three ids — {@code com.hypergryph.endfield} (官服),
     * {@code com.gryphline.endfield.gp} (国际服) and a bilibili channel build —
     * and pinning one of them is exactly what kept the others out, while a list
     * would still miss the next channel or a repackaged build. LSPosed already
     * asked the user which app to attach to; second-guessing that answer buys
     * nothing.
     *
     * What actually decides whether anything happens is evidence, further down:
     * {@code com.unity3d.player.UnityPlayer.nativeRender} has to exist before a
     * trigger is installed, and the native runtime resolves every hook by name
     * through {@code libil2cpp.so}'s exports. A process that is not this Unity
     * game gets a log line and nothing else.
     */
    @Override
    public void handleLoadPackage(XC_LoadPackage.LoadPackageParam loadPackageParam) {
        // Subprocesses (push services, crash handlers) have no Unity runtime.
        if (!loadPackageParam.packageName.equals(loadPackageParam.processName) ||
                MODULE_PACKAGE.equals(loadPackageParam.packageName) ||
                SYSTEM_PACKAGE.equals(loadPackageParam.packageName)) {
            return;
        }

        XposedBridge.log(
                "BetterEndfield.Xposed: attached to " + loadPackageParam.packageName);
        String voiceRules = readVoiceRules();
        String modelConfiguration = readModelConfiguration();
        String enhancementConfiguration = readEnhancementConfiguration();
        XposedHelpers.findAndHookMethod(
                Application.class,
                "attach",
                Context.class,
                new XC_MethodHook() {
                    @Override
                    protected void afterHookedMethod(MethodHookParam param) {
                        RuntimeBootstrap.prepare(
                                (Application) param.thisObject,
                                (Context) param.args[0],
                                loadPackageParam.classLoader,
                                voiceRules,
                                modelConfiguration,
                                enhancementConfiguration,
                                BetterEndfieldXposed::installFrames,
                                message -> XposedBridge.log("BetterEndfield.Legacy: " + message));
                    }
                });
    }

    private static void installFrames(ClassLoader loader, java.util.function.BooleanSupplier callback) {
        java.util.concurrent.CopyOnWriteArrayList<XC_MethodHook.Unhook> hooks =
                new java.util.concurrent.CopyOnWriteArrayList<>();
        AtomicBoolean complete = new AtomicBoolean();
        Set<XC_MethodHook.Unhook> installed = XposedBridge.hookAllMethods(
                XposedHelpers.findClass("com.unity3d.player.UnityPlayer", loader), "nativeRender",
                new XC_MethodHook() {
                    @Override protected void afterHookedMethod(MethodHookParam param) {
                        if (Boolean.TRUE.equals(param.getResult()) && !complete.get() && callback.getAsBoolean()) {
                            complete.set(true);
                            hooks.forEach(XC_MethodHook.Unhook::unhook);
                        }
                    }
                });
        hooks.addAll(installed);
        if (complete.get()) hooks.forEach(XC_MethodHook.Unhook::unhook);
        if (hooks.isEmpty()) throw new IllegalStateException("UnityPlayer.nativeRender unavailable");
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

    private static String readEnhancementConfiguration() {
        try {
            XSharedPreferences preferences = new XSharedPreferences(
                    MODULE_PACKAGE,
                    "module_settings");
            preferences.reload();
            String value = preferences.getString("enhancement_configuration", "");
            if (value == null) value = "";
            XposedBridge.log(
                    "BetterEndfield.Xposed: enhancement configuration readable=" +
                            preferences.getFile().canRead() +
                            " enabled=" + !value.isEmpty());
            return value;
        } catch (Throwable error) {
            XposedBridge.log(
                    "BetterEndfield.Xposed: enhancement configuration unavailable: " + error);
            return "";
        }
    }

}
