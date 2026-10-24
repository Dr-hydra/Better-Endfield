package dev.betterendfield.next;

import android.media.MediaPlayer;
import android.os.Handler;
import android.os.HandlerThread;

import java.util.LinkedHashMap;
import java.util.Map;

/**
 * Independent one-shot/looping sound effects for BEM driver animations.
 *
 * <p>Each voice is its own {@link MediaPlayer}, so Android's mixer keeps the
 * game's audio and BGM running: nothing here touches Unity or Wwise. The native
 * side owns the semantic key; this class only maps keys to players.
 */
final class DriverAudio {
    private static final int MAX_VOICES = 8;
    private static final HandlerThread THREAD = new HandlerThread("BetterEndfieldNext-DriverAudio");
    private static final Handler WORKER;
    static {
        THREAD.start();
        WORKER = new Handler(THREAD.getLooper());
    }

    /** Guarded by WORKER; every player is created, started and released there. */
    private static final Map<String, MediaPlayer> VOICES = new LinkedHashMap<>();

    private DriverAudio() {}

    /** Returns 1 when the voice is (or became) active for this path. */
    static synchronized int start(String key, String path, float gain, boolean loop) {
        if (key == null || key.isEmpty() || path == null || path.isEmpty()) return 0;
        WORKER.post(() -> {
            final float volume = Math.max(0f, Math.min(2f, gain));
            MediaPlayer existing = VOICES.get(key);
            if (existing != null) {
                try {
                    existing.setVolume(Math.min(1f, volume), Math.min(1f, volume));
                    existing.setLooping(loop);
                    return;
                } catch (RuntimeException ignored) {
                    releaseVoice(key, existing);
                }
            }
            if (VOICES.size() >= MAX_VOICES) {
                String oldest = VOICES.keySet().iterator().next();
                releaseVoice(oldest, VOICES.get(oldest));
            }
            try {
                MediaPlayer player = new MediaPlayer();
                player.setDataSource(path);
                player.setVolume(Math.min(1f, volume), Math.min(1f, volume));
                player.setLooping(loop);
                player.setOnErrorListener((mp, what, extra) -> {
                    synchronized (DriverAudio.class) {
                        if (VOICES.get(key) == mp) VOICES.remove(key);
                    }
                    mp.release();
                    return true;
                });
                player.setOnCompletionListener(mp -> {
                    if (loop) return;
                    synchronized (DriverAudio.class) {
                        if (VOICES.get(key) == mp) VOICES.remove(key);
                    }
                    mp.release();
                });
                player.prepare();
                player.start();
                VOICES.put(key, player);
            } catch (Exception ignored) {
                // A broken effect file must never affect the model or animation.
            }
        });
        return 1;
    }

    static synchronized void stop(String key) {
        if (key == null) return;
        WORKER.post(() -> {
            MediaPlayer player = VOICES.remove(key);
            if (player != null) releaseVoice(key, player);
        });
    }

    static synchronized void stopAll() {
        WORKER.post(() -> {
            for (Map.Entry<String, MediaPlayer> entry : VOICES.entrySet())
                releaseVoice(entry.getKey(), entry.getValue());
            VOICES.clear();
        });
    }

    /** Called on the worker only. */
    private static void releaseVoice(String key, MediaPlayer player) {
        VOICES.remove(key);
        if (player == null) return;
        try { player.stop(); } catch (RuntimeException ignored) { }
        try { player.release(); } catch (RuntimeException ignored) { }
    }

    static synchronized String status() {
        return "voices=" + VOICES.size();
    }
}
