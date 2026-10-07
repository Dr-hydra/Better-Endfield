package dev.betterendfield.android;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.SystemClock;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.Executors;
import java.util.function.Consumer;
import java.util.function.Supplier;

/** Binder and disk operations never run on the game UI/render thread. */
final class OverlaySettingsClient {
    private static volatile Supplier<SharedPreferences> frameworkPreferences;
    // Only the single settings worker accesses these objects.
    private static final Map<String, String> lastOutcome = new HashMap<>();
    private static final OverlayReconnectPolicy reconnect = new OverlayReconnectPolicy();
    /** Installed exclusively by the scoped libxposed entry; never open preferences with the game Context. */
    static void initialize(Supplier<SharedPreferences> preferences) { frameworkPreferences = preferences; }
    private static final java.util.concurrent.ExecutorService WORKER = Executors.newSingleThreadExecutor(r -> {
        Thread thread = new Thread(r, "BetterEndfield-OverlaySettings"); thread.setDaemon(true); return thread;
    });
    private record Exchange(Bundle reply, String authorizationState) {}

    static void call(Context context, String method, String revision, String patch, Consumer<Bundle> callback) {
        Context app = context.getApplicationContext();
        Activity activity = context instanceof Activity ? (Activity) context : null;
        WORKER.execute(() -> {
            Exchange exchange = invoke(app, method, revision, patch);
            recordOutcome(app, method, exchange);
            if (!exchange.reply().getBoolean("ok") && frameworkPreferences != null
                    && !RuntimeBootstrap.MODULE_PACKAGE.equals(app.getPackageName())
                    && reconnect.begin(method, exchange.reply().getString("error_code"),
                            foreground(activity), SystemClock.elapsedRealtime()) && bootstrap(app, activity)) {
                for (int retry = 0; retry < OverlayReconnectPolicy.READ_RETRIES && usable(activity); ++retry) {
                    try { Thread.sleep(OverlayReconnectPolicy.RETRY_DELAY_MS); }
                    catch (InterruptedException stopped) { Thread.currentThread().interrupt(); break; }
                    exchange = invoke(app, method, null, null);
                    recordOutcome(app, method, exchange);
                    if (exchange.reply().getBoolean("ok")) break;
                }
            }
            Bundle result = exchange.reply();
            new Handler(Looper.getMainLooper()).post(() -> callback.accept(result));
        });
    }

    private static boolean usable(Activity activity) {
        return activity != null && !activity.isFinishing() && !activity.isDestroyed();
    }
    private static boolean foreground(Activity activity) { return usable(activity) && activity.hasWindowFocus(); }

    private static boolean bootstrap(Context app, Activity activity) {
        if (!foreground(activity)) return false;
        try {
            Intent intent = new Intent().setClassName(RuntimeBootstrap.MODULE_PACKAGE,
                    RuntimeBootstrap.MODULE_PACKAGE + ".FrameworkBootstrapActivity")
                    .addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_NO_ANIMATION);
            app.startActivity(intent);
            OverlaySettingsDiagnostics.record(app, "bootstrap=requested");
            return true;
        } catch (RuntimeException unavailable) {
            OverlaySettingsDiagnostics.record(app, "bootstrap=failed type=" + unavailable.getClass().getSimpleName());
            return false;
        }
    }

    private static Exchange invoke(Context app, String method, String revision, String patch) {
        String authorizationState = "unavailable";
        Bundle reply;
        try {
            Bundle request = new Bundle();
            Supplier<SharedPreferences> source = frameworkPreferences;
            if (source != null) {
                String authorization = source.get().getString(OverlayWriteAuthorization.PREFERENCE, "");
                boolean valid = OverlayWritePolicy.validToken(authorization);
                authorizationState = valid ? "valid" : "missing_or_invalid";
                if (valid) request.putString(OverlayWriteAuthorization.REQUEST, authorization);
            }
            if (revision != null) request.putString("expected", revision);
            if (patch != null) request.putString("patch", patch);
            reply = app.getContentResolver().call(Uri.parse("content://" + OverlaySettingsProvider.AUTHORITY), method, null, request);
            if (reply == null) throw new IllegalStateException("设置桥不可用");
        } catch (RuntimeException error) {
            reply = new Bundle(); reply.putBoolean("ok", false);
            reply.putString("error", error instanceof SecurityException ? "设置桥授权检查未通过，请重试" : "设置桥不可用");
            reply.putString("error_code", error instanceof SecurityException ? "caller_rejected" : "transport_" + error.getClass().getSimpleName());
        }
        return new Exchange(reply, authorizationState);
    }

    private static void recordOutcome(Context app, String method, Exchange exchange) {
        Bundle reply = exchange.reply();
        String outcome = reply.getBoolean("ok") ? "ok revision_present=" + !reply.getString("revision", "").isEmpty()
                : "failed category=" + reply.getString("error_code", "provider_failed") + " authorization=" + exchange.authorizationState();
        String previous = lastOutcome.put(method, outcome);
        if (outcome.equals(previous)) return;
        String status = "operation=" + method + " result=" + outcome;
        if (reply.getBoolean("ok") && previous != null && previous.startsWith("failed")) status += " recovered_from=" + previous;
        // Never log request bodies, tokens, revisions, preference contents or provider error messages.
        OverlaySettingsDiagnostics.record(app, status);
        if (reply.getBoolean("ok")) android.util.Log.i("BetterEndfield.Overlay", status);
        else android.util.Log.e("BetterEndfield.Overlay", status);
    }
}
