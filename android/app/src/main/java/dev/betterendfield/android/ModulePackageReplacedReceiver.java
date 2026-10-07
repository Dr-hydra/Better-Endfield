package dev.betterendfield.android;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;
import java.io.IOException;

/** Keep the update-started Application alive briefly for the framework callback. */
public final class ModulePackageReplacedReceiver extends BroadcastReceiver {
    @Override public void onReceive(Context context, Intent intent) {
        if (intent == null || !Intent.ACTION_MY_PACKAGE_REPLACED.equals(intent.getAction())) return;
        // Android creates ModuleApplication before dispatching this component.
        // Do not register a second framework listener or start an activity here.
        if (FrameworkSettings.isConnected()) {
            OverlaySettingsDiagnostics.record(context, "package_replace=connected");
            return;
        }
        OverlaySettingsDiagnostics.record(context, "package_replace=waiting");
        PendingResult pending = goAsync();
        boolean started = false;
        try {
            new Thread(() -> {
                try {
                    // Leave headroom within the broadcast's usual ten-second budget.
                    // An installer cancellation must not cancel framework bootstrap.
                    boolean connected = FrameworkServiceWait.await(FrameworkSettings.class,
                            FrameworkSettings::isConnected, 8_000, () -> {});
                    OverlaySettingsDiagnostics.record(context,
                            connected ? "package_replace=connected" : "package_replace=timeout");
                } catch (IOException | RuntimeException unavailable) {
                    OverlaySettingsDiagnostics.record(context, "package_replace=unavailable");
                    Log.w("BetterEndfield.Settings", "package replacement framework wait unavailable");
                } finally {
                    pending.finish();
                }
            }, "BE-update-framework").start();
            started = true;
        } catch (RuntimeException unavailable) {
            OverlaySettingsDiagnostics.record(context, "package_replace=unavailable");
            Log.w("BetterEndfield.Settings", "package replacement framework worker unavailable");
        } finally {
            // Release even if thread creation fails before its own finally runs.
            if (!started) pending.finish();
        }
    }
}
