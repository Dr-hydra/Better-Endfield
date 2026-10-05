package dev.betterendfield.android;

import android.content.Context;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import java.util.concurrent.Executors;
import java.util.function.Consumer;
import java.util.function.Supplier;

/** Binder and disk operations never run on the game UI/render thread. */
final class OverlaySettingsClient {
    private static volatile Supplier<SharedPreferences> frameworkPreferences;
    /** Installed exclusively by the scoped libxposed entry; never open preferences with the game Context. */
    static void initialize(Supplier<SharedPreferences> preferences) { frameworkPreferences = preferences; }
    private static final java.util.concurrent.ExecutorService WORKER = Executors.newSingleThreadExecutor(r -> {
        Thread thread = new Thread(r, "BetterEndfield-OverlaySettings"); thread.setDaemon(true); return thread;
    });
    static void call(Context context, String method, String revision, String patch, Consumer<Bundle> callback) {
        Context app = context.getApplicationContext();
        WORKER.execute(() -> {
            Bundle reply;
            try {
                Bundle request = new Bundle();
                Supplier<SharedPreferences> source = frameworkPreferences;
                if (source != null) {
                    String authorization = source.get().getString(OverlayWriteAuthorization.PREFERENCE, "");
                    if (OverlayWritePolicy.validToken(authorization)) request.putString(OverlayWriteAuthorization.REQUEST, authorization);
                }
                if (revision != null) request.putString("expected", revision);
                if (patch != null) request.putString("patch", patch);
                reply = app.getContentResolver().call(Uri.parse("content://" + OverlaySettingsProvider.AUTHORITY), method, null, request);
                if (reply == null) throw new IllegalStateException("设置桥不可用");
            } catch (RuntimeException error) {
                reply = new Bundle(); reply.putBoolean("ok", false); reply.putString("error", "设置桥不可用");
                android.util.Log.e("BetterEndfield.Overlay", "settings bridge failed", error);
            }
            Bundle result = reply;
            new Handler(Looper.getMainLooper()).post(() -> callback.accept(result));
        });
    }
}
