package dev.betterendfield.next;

import android.media.MediaPlayer;
import android.os.HandlerThread;
import android.os.Looper;
import java.util.LinkedHashMap;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.atomic.AtomicReference;

/** Each case runs in a fresh JVM, against the actual production source. */
public final class MmdAudioJvmTest {
    private static final int PLAY = 0, PAUSE = 1, SEEK = 2, CLOSE = 3, GAIN = 4;
    private static final int LOADING = 1, READY = 2, PLAYING = 3, PAUSED = 4, ENDED = 5, ERROR = 6;
    private static final Map<String, Runnable> CASES = new LinkedHashMap<>();
    private record Producer(Thread thread, AtomicReference<Throwable> failure) {}
    private static final List<Producer> PRODUCERS = new ArrayList<>();
    static {
        CASES.put("async_prepare_loading_gain", MmdAudioJvmTest::asyncPrepareLoadingGain);
        CASES.put("play_pause_poll_resume", MmdAudioJvmTest::playPausePollResume);
        CASES.put("seek_ready_bounds", MmdAudioJvmTest::seekReadyBounds);
        CASES.put("seek_playing", MmdAudioJvmTest::seekPlaying);
        CASES.put("seek_burst_last_wins", MmdAudioJvmTest::seekBurstLastWins);
        CASES.put("play_timeline_burst", MmdAudioJvmTest::playTimelineBurst);
        CASES.put("pause_during_seek_burst", MmdAudioJvmTest::pauseDuringSeekBurst);
        CASES.put("resume_during_pending_seek", MmdAudioJvmTest::resumeDuringPendingSeek);
        CASES.put("stale_token_and_queued_control", MmdAudioJvmTest::staleTokenAndQueuedControl);
        CASES.put("stale_prepared_callback", MmdAudioJvmTest::stalePreparedCallback);
        CASES.put("stale_seek_error_completion", MmdAudioJvmTest::staleSeekErrorCompletion);
        CASES.put("queued_open_close_reopen", MmdAudioJvmTest::queuedOpenCloseReopen);
        CASES.put("background_close_queue", MmdAudioJvmTest::backgroundCloseQueue);
        CASES.put("close_loading_callback_reopen", MmdAudioJvmTest::closeLoadingCallbackReopen);
        CASES.put("data_source_error_recovery", MmdAudioJvmTest::dataSourceErrorRecovery);
        CASES.put("media_error_snapshot", MmdAudioJvmTest::mediaErrorSnapshot);
        CASES.put("poll_error_snapshot", MmdAudioJvmTest::pollErrorSnapshot);
        CASES.put("completion_replay", MmdAudioJvmTest::completionReplay);
        CASES.put("prepared_volume_failure", MmdAudioJvmTest::preparedVolumeFailure);
        CASES.put("seek_resume_start_failure", MmdAudioJvmTest::seekResumeStartFailure);
        CASES.put("prepared_generation_snapshot_race", MmdAudioJvmTest::preparedGenerationSnapshotRace);
        CASES.put("poll_error_generation_snapshot_race", MmdAudioJvmTest::pollErrorGenerationSnapshotRace);
        CASES.put("play_generation_snapshot_race", MmdAudioJvmTest::playGenerationSnapshotRace);
    }
    public static void main(String[] args) {
        if (args.length == 1 && args[0].equals("--list")) { CASES.keySet().forEach(System.out::println); return; }
        if (args.length != 1 || !CASES.containsKey(args[0])) {
            System.err.println("Pass one case name or --list"); System.exit(2);
        }
        try { CASES.get(args[0]).run(); System.out.println("PASS " + args[0]); }
        catch (Throwable error) { System.err.println("FAIL " + args[0]); error.printStackTrace(); System.exit(1); }
    }
    private static Looper worker() { return HandlerThread.worker(); }
    private static long loading(String path) {
        long token = MmdAudio.open(path); check(token > 0, "open returns a token");
        snapshot(token, LOADING, 0, 0, false); return token;
    }
    private static long ready(String path) {
        long token = loading(path); worker().drain();
        MediaPlayer.latest().prepared(120000); worker().drain();
        snapshot(token, READY, 0, 120, false); return token;
    }
    private static long playing(String path) {
        long token = ready(path); accepted(token, PLAY, -1); worker().drain();
        snapshot(token, PLAYING, 0, 120, true); return token;
    }
    private static void accepted(long token, int operation, double value) {
        check(MmdAudio.control(token, operation, value) == 1, "operation accepted: " + operation);
    }
    private static void snapshot(long token, int state, double position, double duration, boolean active) {
        double[] value = MmdAudio.status(token);
        check(value != null && value.length == 4, "four-field snapshot for current token");
        check(value[0] == state, "snapshot state expected " + state + ", got " + value[0]);
        near(value[1], position, "snapshot position"); near(value[2], duration, "snapshot duration");
        check(value[3] == (active ? 1 : 0), "snapshot output_active");
    }
    private static void closed(long token) {
        check(MmdAudio.status(token) == null, "closed token has no snapshot");
        check(MmdAudio.error(token).equals("Track closed"), "closed token has closed error");
        for (int op = 0; op <= 4; op++) check(MmdAudio.control(token, op, 0) == 0, "old token rejects operation " + op);
    }
    private static void asyncPrepareLoadingGain() {
        check(MmdAudio.open(null) == 0 && MmdAudio.open("") == 0, "empty paths rejected");
        long token = loading("gain.mp3");
        check(MediaPlayer.instanceCount() == 0, "open must not create player inline");
        for (int op = 0; op < 3; op++) check(MmdAudio.control(token, op, 0) == 0, "transport rejected while loading");
        accepted(token, GAIN, 0.25); worker().drain();
        MediaPlayer player = MediaPlayer.latest();
        check(player.volumeWrites == 0, "gain is deferred during prepareAsync");
        accepted(token, GAIN, 0.6); accepted(token, GAIN, 0.4);
        check(MmdAudio.control(token, GAIN, Double.NaN) == 0, "nonfinite gain rejected");
        player.prepared(120000); snapshot(token, LOADING, 0, 0, false); worker().drain();
        snapshot(token, READY, 0, 120, false); near(player.leftGain, 0.4, "latest loading gain");
        near(player.rightGain, 0.4, "stereo gain"); check(player.starts == 0, "prepare does not auto-start");
        double[] copy = MmdAudio.status(token); copy[0] = 999;
        snapshot(token, READY, 0, 120, false);
        accepted(token, GAIN, 0.8); near(player.leftGain, 0.4, "ready gain is queued"); worker().drain();
        near(player.leftGain, 0.8, "ready gain applied");
    }
    private static void playPausePollResume() {
        long token = ready("play.mp3"); MediaPlayer player = MediaPlayer.latest();
        accepted(token, PLAY, -1); snapshot(token, READY, 0, 120, false); worker().drain();
        snapshot(token, PLAYING, 0, 120, true); check(player.playing(), "player started");
        player.positionAt(4250); worker().advanceBy(50); snapshot(token, PLAYING, 4.25, 120, true);
        accepted(token, PAUSE, 0); snapshot(token, PLAYING, 4.25, 120, true); worker().drain();
        snapshot(token, PAUSED, 4.25, 120, false); check(!player.playing(), "player paused");
        accepted(token, PLAY, -1); worker().drain(); snapshot(token, PLAYING, 4.25, 120, true);
        check(player.starts == 2 && player.pauses == 1 && player.seeks.isEmpty(), "resume keeps position");
        check(MmdAudio.control(token, SEEK, Double.POSITIVE_INFINITY) == 0, "nonfinite seek rejected");
        check(MmdAudio.control(token, 99, 0) == 0, "unknown operation rejected"); worker().drain();
    }
    private static void seekReadyBounds() {
        long token = ready("seek.mp3"); MediaPlayer player = MediaPlayer.latest();
        accepted(token, SEEK, -10); worker().drain();
        check(player.seeks.equals(List.of(0L)), "negative seek clamped"); snapshot(token, PAUSED, 0, 120, false);
        player.seekCompleted(); worker().drain();
        accepted(token, SEEK, 999); worker().drain();
        check(player.seeks.equals(List.of(0L, 120000L)), "seek clamped to duration");
        player.seekCompleted(); worker().drain(); snapshot(token, PAUSED, 120, 120, false);
        check(player.starts == 0, "seek on ready track does not start playback");
    }
    private static void seekPlaying() {
        long token = playing("seek-playing.mp3"); MediaPlayer player = MediaPlayer.latest();
        accepted(token, SEEK, 12.345); worker().drain();
        snapshot(token, PAUSED, 0, 120, false); check(!player.playing(), "seek pauses output");
        check(player.seeks.equals(List.of(12345L)), "millisecond seek target");
        player.seekCompleted(); worker().drain(); snapshot(token, PLAYING, 12.345, 120, true);
        check(player.starts == 2, "playback resumes after seek callback");
    }
    private static void seekBurstLastWins() {
        long token = playing("seek-burst.mp3"); MediaPlayer player = MediaPlayer.latest();
        accepted(token, SEEK, 10); accepted(token, SEEK, 20); accepted(token, SEEK, 30); worker().drain();
        check(player.seeks.equals(List.of(10000L)), "only first seek submitted while in flight");
        player.seekCompleted(); worker().drain();
        check(player.seeks.equals(List.of(10000L, 30000L)), "last queued target wins");
        check(player.starts == 1 && !player.playing(), "no intermediate restart");
        player.seekCompleted(); worker().drain(); snapshot(token, PLAYING, 30, 120, true);
        check(player.starts == 2, "one final restart");
    }
    private static void pauseDuringSeekBurst() {
        long token = playing("pause-burst.mp3"); MediaPlayer player = MediaPlayer.latest();
        accepted(token, SEEK, 10); worker().drain();
        accepted(token, SEEK, 20); accepted(token, PAUSE, 0); accepted(token, SEEK, 30); worker().drain();
        player.seekCompleted(); worker().drain(); player.seekCompleted(); worker().drain();
        snapshot(token, PAUSED, 30, 120, false); check(player.starts == 1, "pause cancels pending restarts");
        accepted(token, PLAY, -1); worker().drain(); snapshot(token, PLAYING, 30, 120, true);
    }
    private static void playTimelineBurst() {
        long token = ready("play-timeline.mp3"); MediaPlayer player = MediaPlayer.latest();
        // The director's native play(token, MusicTarget()) uses this path.
        accepted(token, PLAY, 12.5); accepted(token, PLAY, 20); accepted(token, PLAY, 30.25); worker().drain();
        snapshot(token, PAUSED, 0, 120, false);
        check(player.seeks.equals(List.of(12500L)) && player.starts == 0, "timeline play waits for seek");
        player.seekCompleted(); worker().drain();
        check(player.seeks.equals(List.of(12500L, 30250L)) && player.starts == 0, "timeline play coalesces without intermediate output");
        player.seekCompleted(); worker().drain(); snapshot(token, PLAYING, 30.25, 120, true);
        check(player.starts == 1, "timeline play starts once at final target");
    }
    private static void resumeDuringPendingSeek() {
        long token = ready("resume-seek.mp3"); MediaPlayer player = MediaPlayer.latest();
        accepted(token, SEEK, 8); worker().drain(); accepted(token, PLAY, -1); worker().drain();
        check(player.starts == 0, "resume waits for seek");
        player.seekCompleted(); worker().drain(); snapshot(token, PLAYING, 8, 120, true);
    }
    private static void staleTokenAndQueuedControl() {
        long old = ready("old.mp3"); MediaPlayer previous = MediaPlayer.latest();
        accepted(old, PLAY, -1); long next = loading("next.mp3"); closed(old); worker().drain();
        check(previous.released() && previous.starts == 0, "queued old play is discarded");
        snapshot(next, LOADING, 0, 0, false);
        MediaPlayer.latest().prepared(15000); worker().drain(); snapshot(next, READY, 0, 15, false);
    }
    private static void stalePreparedCallback() {
        long old = loading("old-loading.mp3"); worker().drain(); MediaPlayer previous = MediaPlayer.latest();
        previous.prepared(999000); long next = loading("new-loading.mp3"); worker().drain(); closed(old);
        snapshot(next, LOADING, 0, 0, false); check(previous.released(), "old prepared callback released old player");
        MediaPlayer.latest().prepared(5000); worker().drain(); snapshot(next, READY, 0, 5, false);
    }
    private static void staleSeekErrorCompletion() {
        long old = playing("old-seeking.mp3"); MediaPlayer previous = MediaPlayer.latest();
        accepted(old, SEEK, 8); worker().drain();
        previous.seekCompleted(); previous.failed(100, -1); previous.completed();
        long next = loading("new.mp3"); worker().drain(); closed(old);
        snapshot(next, LOADING, 0, 0, false); check(MmdAudio.error(next).isEmpty(), "stale errors discarded");
        check(previous.starts == 1, "old seek callback does not restart audio");
    }
    private static void queuedOpenCloseReopen() {
        long a = loading("a.mp3"); accepted(a, CLOSE, 0); closed(a);
        long b = loading("b.mp3"); accepted(b, CLOSE, 0); closed(b);
        long c = loading("c.mp3"); worker().drain();
        check(MediaPlayer.instanceCount() == 1, "only newest open constructs a player");
        check(MediaPlayer.latest().path.equals("c.mp3"), "close queue does not release new player");
        MediaPlayer.latest().prepared(7000); worker().drain(); accepted(c, PLAY, -1); worker().drain();
        snapshot(c, PLAYING, 0, 7, true);
    }
    private static void backgroundCloseQueue() {
        long old = playing("background.mp3"); MediaPlayer previous = MediaPlayer.latest();
        accepted(old, PAUSE, 0); accepted(old, SEEK, 30); accepted(old, PLAY, -1);
        // Simulate foreground suspension closing audio before the worker drains.
        accepted(old, CLOSE, 0); closed(old); worker().drain();
        check(previous.released() && previous.seeks.isEmpty() && previous.starts == 1,
                "queued transport discarded after background close");
        worker().advanceBy(500);
        long next = ready("foreground.mp3"); accepted(next, PLAY, -1); worker().drain();
        snapshot(next, PLAYING, 0, 120, true);
    }
    private static void closeLoadingCallbackReopen() {
        long old = loading("close-loading.mp3"); worker().drain(); MediaPlayer previous = MediaPlayer.latest();
        previous.prepared(50000); accepted(old, CLOSE, 0); closed(old); worker().drain();
        check(previous.released() && previous.starts == 0, "pending prepare cannot play after close");
        long next = ready("reopen.mp3"); worker().advanceBy(100); snapshot(next, READY, 0, 120, false);
    }
    private static void dataSourceErrorRecovery() {
        MediaPlayer.failingPath = "bad.mp3"; long old = loading("bad.mp3"); worker().drain();
        snapshot(old, ERROR, 0, 0, false); check(MmdAudio.error(old).contains("injected bad data source"), "data-source failure retained");
        check(MediaPlayer.latest().released(), "failed open releases player");
        check(MmdAudio.control(old, PLAY, -1) == 0, "transport rejected in error"); accepted(old, CLOSE, 0);
        long next = ready("good.mp3"); check(MmdAudio.error(next).isEmpty(), "new open clears prior error");
    }
    private static void mediaErrorSnapshot() {
        long token = playing("media-error.mp3"); MediaPlayer.latest().failed(1, -1004); worker().drain();
        snapshot(token, ERROR, 0, 120, false); check(MmdAudio.error(token).contains("1/-1004"), "Android error detail retained");
        check(MmdAudio.control(token, PLAY, -1) == 0, "error cannot be resumed"); accepted(token, CLOSE, 0); worker().drain(); closed(token);
    }
    private static void pollErrorSnapshot() {
        long token = playing("poll-error.mp3"); MediaPlayer player = MediaPlayer.latest();
        player.positionFailure = new IllegalStateException("injected poll failure"); worker().advanceBy(50);
        snapshot(token, ERROR, 0, 120, false); check(MmdAudio.error(token).contains("injected poll failure"), "poll error retained");
        accepted(token, CLOSE, 0); worker().drain(); check(player.released(), "error close releases resources");
    }
    private static void completionReplay() {
        long token = playing("complete.mp3"); MediaPlayer player = MediaPlayer.latest();
        player.completed(); worker().drain(); snapshot(token, ENDED, 120, 120, false);
        accepted(token, PLAY, -1); worker().drain(); worker().advanceBy(50);
        snapshot(token, PLAYING, 0, 120, true); check(player.starts == 2, "completed track can replay");
    }
    private static void preparedVolumeFailure() {
        long token = loading("prepared-volume-error.mp3"); worker().drain(); MediaPlayer player = MediaPlayer.latest();
        player.volumeFailure = new IllegalStateException("injected prepared volume failure");
        player.prepared(120000); worker().drain();
        errorSnapshot(token);
        check(MmdAudio.error(token).contains("injected prepared volume failure"), "prepared callback error retained");
        accepted(token, CLOSE, 0); worker().drain(); check(player.released(), "worker survives prepared error for cleanup");
    }
    private static void seekResumeStartFailure() {
        long token = playing("seek-start-error.mp3"); MediaPlayer player = MediaPlayer.latest();
        accepted(token, SEEK, 7); worker().drain();
        player.startFailure = new IllegalStateException("injected seek resume failure");
        player.seekCompleted(); worker().drain();
        errorSnapshot(token);
        check(MmdAudio.error(token).contains("injected seek resume failure"), "seek callback error retained");
        accepted(token, CLOSE, 0); worker().drain(); check(player.released(), "worker survives seek error for cleanup");
    }
    private static void preparedGenerationSnapshotRace() {
        long old = loading("prepared-old.mp3"); worker().drain(); MediaPlayer previous = MediaPlayer.latest();
        long[] next = {0}; previous.beforeDurationRead = () -> producer(() -> next[0] = MmdAudio.open("prepared-new.mp3"));
        previous.prepared(90000); check(worker().runOne(), "prepared callback queued");
        finishProducers();
        closed(old); snapshot(next[0], LOADING, 0, 0, false);
    }
    private static void pollErrorGenerationSnapshotRace() {
        long old = playing("poll-old.mp3"); MediaPlayer previous = MediaPlayer.latest(); long[] next = {0};
        previous.beforePositionRead = () -> producer(() -> next[0] = MmdAudio.open("poll-new.mp3"));
        previous.positionFailure = new IllegalStateException("old-player snapshot read failed");
        worker().advanceBy(50); finishProducers(); closed(old);
        snapshot(next[0], LOADING, 0, 0, false); check(MmdAudio.error(next[0]).isEmpty(), "old poll error must not poison new generation");
    }
    private static void playGenerationSnapshotRace() {
        long old = ready("play-old.mp3"); MediaPlayer previous = MediaPlayer.latest(); long[] next = {0};
        previous.beforeStart = () -> producer(() -> next[0] = MmdAudio.open("play-new.mp3"));
        accepted(old, PLAY, -1); check(worker().runOne(), "play command queued");
        finishProducers();
        closed(old); snapshot(next[0], LOADING, 0, 0, false);
    }
    private static void producer(Runnable action) {
        AtomicReference<Throwable> failure = new AtomicReference<>();
        Thread producer = new Thread(() -> { try { action.run(); } catch (Throwable error) { failure.set(error); } }, "Unity-audio-producer");
        PRODUCERS.add(new Producer(producer, failure)); producer.setDaemon(true); producer.start();
        long deadline = System.nanoTime() + 2000000000L;
        while (producer.isAlive()) {
            // A legitimate fix may serialize producer updates with this callback.
            // Let that callback return before awaiting its blocked producer.
            if (producer.getState() == Thread.State.BLOCKED) return;
            check(System.nanoTime() < deadline, "producer did not complete or serialize");
            join(producer, 1);
        }
        if (failure.get() != null) throw new AssertionError("producer failed", failure.get());
    }
    private static void finishProducers() {
        for (Producer producer : PRODUCERS) {
            join(producer.thread, 2000);
            check(!producer.thread.isAlive(), "producer remained blocked after callback finished");
            if (producer.failure.get() != null) throw new AssertionError("producer failed", producer.failure.get());
        }
        PRODUCERS.clear();
    }
    private static void join(Thread thread, long milliseconds) {
        try { thread.join(milliseconds); }
        catch (InterruptedException error) { Thread.currentThread().interrupt(); throw new AssertionError(error); }
    }
    private static void errorSnapshot(long token) {
        double[] value = MmdAudio.status(token);
        check(value != null && value.length == 4 && value[0] == ERROR && value[3] == 0, "error snapshot without active output");
        check(Double.isFinite(value[1]) && value[1] >= 0 && Double.isFinite(value[2]) && value[2] >= 0,
                "finite error snapshot position/duration");
    }
    private static void near(double actual, double expected, String label) { check(Math.abs(actual - expected) < 0.0001, label + ": " + actual + " != " + expected); }
    private static void check(boolean condition, String message) { if (!condition) throw new AssertionError(message); }
}
