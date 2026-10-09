package dev.betterendfield.next;

import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.SystemClock;

/** Starts ModuleApplication's framework listener without opening a settings page. */
public final class FrameworkBootstrapActivity extends Activity {
    private final Handler handler = new Handler(Looper.getMainLooper());
    private boolean resumed;
    private long startedAt;
    private final Runnable waitForFramework = new Runnable() {
        @Override public void run() {
            if (!resumed || isFinishing() || isDestroyed()) return;
            long elapsed = SystemClock.uptimeMillis() - startedAt;
            // A resumed activity lets the framework renew a cached owner's Binder.
            // Allow that foreground event to arrive even if a connection existed.
            if (elapsed >= 500) {
                boolean connected = FrameworkSettings.isConnected();
                if (connected || elapsed >= 1_500) {
                    OverlaySettingsDiagnostics.record(FrameworkBootstrapActivity.this,
                            connected ? "bootstrap=connected" : "bootstrap=timeout");
                    finishBootstrap();
                    return;
                }
            }
            handler.postDelayed(this, 100);
        }
    };

    @Override protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        // No caller data or configuration is consumed by this exported entry.
    }

    @Override protected void onResume() {
        super.onResume();
        if (isFinishing() || isDestroyed()) return;
        resumed = true;
        startedAt = SystemClock.uptimeMillis();
        OverlaySettingsDiagnostics.record(this, "bootstrap=foreground");
        handler.removeCallbacks(waitForFramework);
        handler.postDelayed(waitForFramework, 100);
    }

    @Override protected void onPause() {
        resumed = false;
        finishBootstrap();
        super.onPause();
    }

    @Override protected void onDestroy() {
        resumed = false;
        handler.removeCallbacks(waitForFramework);
        super.onDestroy();
    }

    private void finishBootstrap() {
        handler.removeCallbacks(waitForFramework);
        if (!isFinishing()) finish();
    }
}
