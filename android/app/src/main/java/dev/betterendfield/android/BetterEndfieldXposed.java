package dev.betterendfield.android;

import android.app.Application;
import android.content.Context;
import android.content.pm.ApplicationInfo;

import java.io.File;
import java.util.concurrent.atomic.AtomicBoolean;

import de.robv.android.xposed.IXposedHookLoadPackage;
import de.robv.android.xposed.XC_MethodHook;
import de.robv.android.xposed.XposedBridge;
import de.robv.android.xposed.XposedHelpers;
import de.robv.android.xposed.callbacks.XC_LoadPackage;

public final class BetterEndfieldXposed implements IXposedHookLoadPackage {
    private static final String MODULE_PACKAGE = "dev.betterendfield.android";
    private static final String TARGET_PACKAGE = "com.hypergryph.endfield";
    private static final String NATIVE_LIBRARY = "libbetterendfield_android.so";
    private static final AtomicBoolean LOAD_REQUESTED = new AtomicBoolean();

    @Override
    public void handleLoadPackage(XC_LoadPackage.LoadPackageParam loadPackageParam) {
        if (!TARGET_PACKAGE.equals(loadPackageParam.packageName) ||
                !TARGET_PACKAGE.equals(loadPackageParam.processName)) {
            return;
        }

        XposedBridge.log("BetterEndfield.Xposed: target process matched");
        XposedHelpers.findAndHookMethod(
                Application.class,
                "attach",
                Context.class,
                new XC_MethodHook() {
                    @Override
                    protected void afterHookedMethod(MethodHookParam param) {
                        loadNativeLibrary(
                                (Application) param.thisObject,
                                (Context) param.args[0]);
                    }
                });
    }

    private static void loadNativeLibrary(
            Application targetApplication,
            Context targetContext) {
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

            loadIntoTargetNamespace(
                    library.getAbsolutePath(),
                    targetContext.getClassLoader(),
                    targetApplication.getClass());
            XposedBridge.log("BetterEndfield.Xposed: native probe loaded from " + library);
        } catch (Throwable error) {
            LOAD_REQUESTED.set(false);
            XposedBridge.log("BetterEndfield.Xposed: native probe load failed: " + error);
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
