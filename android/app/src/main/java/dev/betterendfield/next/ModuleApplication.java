package dev.betterendfield.next;

import android.app.Application;

public final class ModuleApplication extends Application {
    @Override protected void attachBaseContext(android.content.Context base) {
        super.attachBaseContext(AppLanguage.wrap(base));
    }

    @Override public void onCreate() {
        super.onCreate();
        try { GameResourceStore.ensureBundledCore(this); }
        catch (Exception failure) {
            android.util.Log.e("BetterEndfieldNext.Resources", "Offline core resource installation failed", failure);
        }
        OverlaySettingsDiagnostics.record(this, "owner=start");
        FrameworkSettings.initialize(this);
    }
}
