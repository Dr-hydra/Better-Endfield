package dev.betterendfield.android;

import android.app.Application;

public final class ModuleApplication extends Application {
    @Override public void onCreate() {
        super.onCreate();
        OverlaySettingsDiagnostics.record(this, "owner=start");
        FrameworkSettings.initialize(this);
    }
}
