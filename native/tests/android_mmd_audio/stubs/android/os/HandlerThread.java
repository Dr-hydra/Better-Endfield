package android.os;

public final class HandlerThread {
    private static HandlerThread last;
    private final Looper looper = new Looper();
    private boolean started;
    public HandlerThread(String name) { }
    public void start() { started = true; last = this; }
    public Looper getLooper() {
        if (!started) throw new AssertionError("HandlerThread was not started");
        return looper;
    }
    public static Looper worker() {
        if (last == null) throw new AssertionError("production worker was not created");
        return last.getLooper();
    }
}
