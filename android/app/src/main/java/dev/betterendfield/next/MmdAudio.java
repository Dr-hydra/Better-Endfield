package dev.betterendfield.next;

import android.media.MediaPlayer;
import android.os.Handler;
import android.os.HandlerThread;

/** One local MMD track. The media worker owns the player; Unity reads snapshots. */
final class MmdAudio {
    private static final HandlerThread THREAD = new HandlerThread("BetterEndfieldNext-MmdAudio");
    private static final Handler WORKER;
    static { THREAD.start(); WORKER = new Handler(THREAD.getLooper()); }
    private static long serial, token;
    private static volatile int state;
    private static volatile double position, duration;
    private static volatile String failure = "";
    // Accessed only by WORKER.
    private static MediaPlayer player;
    private static boolean seeking, startAfterSeek;
    private static double queuedSeek = -1;
    private static boolean playingWanted;
    private static volatile float requestedGain = 1;

    static synchronized long open(String path) {
        if (path == null || path.isEmpty()) return 0;
        final long generation = ++serial;
        token = generation; state = 1; position = duration = 0; failure = "";
        WORKER.post(() -> {
          synchronized (MmdAudio.class) {
            releasePlayer();
            if (!owns(generation)) return;
            try {
                MediaPlayer created = new MediaPlayer(); player = created;
                created.setOnPreparedListener(p -> guarded(generation, () -> {
                    if (!owns(generation) || player != p) { p.release(); return; }
                    duration = p.getDuration() / 1000.0; p.setVolume(requestedGain, requestedGain); state = 2;
                }));
                created.setOnCompletionListener(p -> guarded(generation, () -> {
                    if (owns(generation) && player == p && !seeking) {
                        position = duration; state = 5; playingWanted = false;
                    }
                }));
                created.setOnErrorListener((p, what, extra) -> {
                    guarded(generation, () -> { if (player == p) fail("MediaPlayer error " + what + "/" + extra); });
                    return true;
                });
                created.setOnSeekCompleteListener(p -> guarded(generation, () -> {
                    if (!owns(generation) || player != p) return;
                    seeking = false;
                    if (queuedSeek >= 0) {
                        double next = queuedSeek; queuedSeek = -1;
                        seek(p, next, playingWanted);
                    } else {
                        position = p.getCurrentPosition() / 1000.0;
                        if (startAfterSeek && playingWanted) { p.start(); state = 3; }
                        else state = 4;
                    }
                }));
                created.setDataSource(path); created.prepareAsync();
                WORKER.postDelayed(new Runnable() {
                    @Override public void run() {
                      synchronized (MmdAudio.class) {
                        if (!owns(generation) || player != created) return;
                        try {
                            if (state == 3 && !seeking) position = created.getCurrentPosition() / 1000.0;
                        } catch (RuntimeException error) { fail(error.toString()); }
                        WORKER.postDelayed(this, 50);
                      }
                    }
                }, 50);
            } catch (Exception error) { fail(error.toString()); releasePlayer(); }
          }
        });
        return generation;
    }
    private static synchronized boolean owns(long value) { return value != 0 && token == value; }
    static synchronized int control(long value, int operation, double amount) {
        if (!owns(value) || !Double.isFinite(amount)) return 0;
        if (operation == 3) {
            token = 0; state = 0; position = duration = 0;
            WORKER.post(MmdAudio::releasePlayer); return 1;
        }
        if (operation == 4) {
            requestedGain = (float) Math.max(0, Math.min(1, amount));
            if (state == 1) return 1; // applied by onPrepared
        }
        if (state < 2 || state == 6) return 0;
        WORKER.post(() -> {
          synchronized (MmdAudio.class) {
            if (!owns(value) || player == null) return;
            try {
                switch (operation) {
                    case 0: // play/resume, optionally at a new timeline position
                        playingWanted = true;
                        if (amount >= 0) seek(player, amount, true);
                        else if (seeking) startAfterSeek = true;
                        else { player.start(); state = 3; }
                        break;
                    case 1:
                        playingWanted = startAfterSeek = false;
                        if (player.isPlaying()) player.pause();
                        position = player.getCurrentPosition() / 1000.0; state = 4;
                        break;
                    case 2: seek(player, amount, playingWanted); break;
                    case 4:
                        float gain = (float) Math.max(0, Math.min(1, amount));
                        player.setVolume(gain, gain); break;
                    default: break;
                }
            } catch (RuntimeException error) { fail(error.toString()); }
          }
        });
        return operation >= 0 && operation <= 4 ? 1 : 0;
    }
    private static void seek(MediaPlayer p, double seconds, boolean start) {
        seconds = Math.max(0, Math.min(duration, seconds));
        startAfterSeek = start;
        if (seeking) { queuedSeek = seconds; return; }
        if (p.isPlaying()) p.pause();
        seeking = true; state = 4;
        p.seekTo(Math.round(seconds * 1000), MediaPlayer.SEEK_CLOSEST);
    }
    static synchronized double[] status(long value) {
        if (!owns(value)) return null;
        return new double[]{state, position, duration, state == 3 ? 1 : 0};
    }
    static synchronized String error(long value) { return owns(value) ? failure : "Track closed"; }
    // A token check and snapshot publication are one operation. A new open
    // cannot be clobbered by a callback from the previous media generation.
    private static synchronized void guarded(long generation, Runnable action) {
        if (!owns(generation)) return;
        try { action.run(); }
        catch (RuntimeException error) { fail(error.toString()); }
    }
    private static void fail(String detail) { failure = detail; state = 6; }
    private static synchronized void releasePlayer() {
        if (player != null) { player.release(); player = null; }
        seeking = startAfterSeek = playingWanted = false; queuedSeek = -1;
    }
}
