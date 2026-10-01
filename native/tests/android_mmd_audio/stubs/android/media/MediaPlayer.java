package android.media;

import android.os.Looper;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

/** Strict state machine with manually delivered Android callbacks and fault hooks. */
public final class MediaPlayer {
    public static final int SEEK_CLOSEST = 3;
    public interface OnPreparedListener { void onPrepared(MediaPlayer player); }
    public interface OnCompletionListener { void onCompletion(MediaPlayer player); }
    public interface OnErrorListener { boolean onError(MediaPlayer player, int what, int extra); }
    public interface OnSeekCompleteListener { void onSeekComplete(MediaPlayer player); }
    private enum Phase { NEW, INITIALIZED, PREPARING, READY, PLAYING, PAUSED, ENDED, ERROR, RELEASED }
    private static final List<MediaPlayer> INSTANCES = new ArrayList<>();
    public static String failingPath;
    public Runnable beforeDurationRead, beforePositionRead, beforeStart;
    public RuntimeException positionFailure, startFailure, volumeFailure;
    public final List<Long> seeks = new ArrayList<>();
    public int starts, pauses, releases, volumeWrites;
    public float leftGain = 1, rightGain = 1;
    public String path;
    private final Looper worker;
    private Phase phase = Phase.NEW;
    private int duration, position;
    private long pendingSeek = -1;
    private OnPreparedListener prepared;
    private OnCompletionListener completion;
    private OnErrorListener error;
    private OnSeekCompleteListener seekComplete;

    public MediaPlayer() {
        Looper.requireWorker(); worker = Looper.myLooper(); INSTANCES.add(this);
    }
    public static MediaPlayer latest() {
        if (INSTANCES.isEmpty()) throw new AssertionError("no MediaPlayer created");
        return INSTANCES.get(INSTANCES.size() - 1);
    }
    public static int instanceCount() { return INSTANCES.size(); }
    private void worker() {
        if (Looper.myLooper() != worker) throw new AssertionError("MediaPlayer used by another worker");
    }
    private void ready() {
        worker();
        if (phase != Phase.READY && phase != Phase.PLAYING && phase != Phase.PAUSED && phase != Phase.ENDED)
            throw new IllegalStateException("player not ready: " + phase);
    }
    public void setOnPreparedListener(OnPreparedListener listener) { worker(); prepared = listener; }
    public void setOnCompletionListener(OnCompletionListener listener) { worker(); completion = listener; }
    public void setOnErrorListener(OnErrorListener listener) { worker(); error = listener; }
    public void setOnSeekCompleteListener(OnSeekCompleteListener listener) { worker(); seekComplete = listener; }
    public void setDataSource(String path) throws IOException {
        worker(); if (phase != Phase.NEW) throw new IllegalStateException("data source already set");
        if (path.equals(failingPath)) throw new IOException("injected bad data source: " + path);
        this.path = path; phase = Phase.INITIALIZED;
    }
    public void prepareAsync() {
        worker(); if (phase != Phase.INITIALIZED) throw new IllegalStateException("not initialized");
        phase = Phase.PREPARING;
    }
    public int getDuration() {
        ready(); Runnable hook = beforeDurationRead; beforeDurationRead = null;
        if (hook != null) hook.run(); return duration;
    }
    public int getCurrentPosition() {
        ready(); Runnable hook = beforePositionRead; beforePositionRead = null;
        if (hook != null) hook.run();
        if (positionFailure != null) throw positionFailure;
        return position;
    }
    public boolean isPlaying() { ready(); return phase == Phase.PLAYING; }
    public void start() {
        ready(); Runnable hook = beforeStart; beforeStart = null;
        if (hook != null) hook.run();
        if (startFailure != null) throw startFailure;
        if (pendingSeek >= 0) throw new AssertionError("started before final seek callback");
        if (phase == Phase.ENDED) position = 0;
        starts++; phase = Phase.PLAYING;
    }
    public void pause() {
        ready(); if (phase != Phase.PLAYING) throw new IllegalStateException("not playing");
        pauses++; phase = Phase.PAUSED;
    }
    public void seekTo(long milliseconds, int mode) {
        ready(); if (mode != SEEK_CLOSEST) throw new AssertionError("unexpected seek mode");
        if (pendingSeek >= 0) throw new AssertionError("overlapping seek was not coalesced");
        pendingSeek = milliseconds; seeks.add(milliseconds);
    }
    public void setVolume(float left, float right) {
        ready(); if (volumeFailure != null) throw volumeFailure;
        leftGain = left; rightGain = right; volumeWrites++;
    }
    public void release() { worker(); releases++; phase = Phase.RELEASED; }
    public boolean released() { return phase == Phase.RELEASED; }
    public boolean playing() { return phase == Phase.PLAYING; }
    public void positionAt(int milliseconds) { position = milliseconds; }

    public void prepared(int milliseconds) {
        if (phase != Phase.PREPARING) throw new AssertionError("test prepared non-loading player");
        worker.post(() -> {
            if (phase != Phase.RELEASED) { phase = Phase.READY; duration = milliseconds; }
            prepared.onPrepared(this);
        }, 0);
    }
    public void seekCompleted() {
        if (pendingSeek < 0) throw new AssertionError("test completed nonexistent seek");
        worker.post(() -> {
            if (phase != Phase.RELEASED) { position = (int) pendingSeek; pendingSeek = -1; }
            seekComplete.onSeekComplete(this);
        }, 0);
    }
    public void completed() {
        worker.post(() -> {
            if (phase != Phase.RELEASED) { phase = Phase.ENDED; position = duration; }
            completion.onCompletion(this);
        }, 0);
    }
    public void failed(int what, int extra) {
        worker.post(() -> {
            if (phase != Phase.RELEASED) phase = Phase.ERROR;
            if (!error.onError(this, what, extra)) throw new AssertionError("error was not handled");
        }, 0);
    }
}
