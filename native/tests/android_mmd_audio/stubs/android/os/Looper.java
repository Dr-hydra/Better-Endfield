package android.os;

import java.util.PriorityQueue;

/** A manual worker clock: posts never run inline, callbacks share the worker. */
public final class Looper {
    private record Task(long due, long order, Runnable action) implements Comparable<Task> {
        @Override public int compareTo(Task other) {
            int time = Long.compare(due, other.due);
            return time == 0 ? Long.compare(order, other.order) : time;
        }
    }
    private static final ThreadLocal<Looper> ACTIVE = new ThreadLocal<>();
    private final PriorityQueue<Task> tasks = new PriorityQueue<>();
    private long now, sequence;

    public static Looper myLooper() { return ACTIVE.get(); }
    public synchronized boolean post(Runnable task, long delay) {
        tasks.add(new Task(now + Math.max(0, delay), sequence++, task)); return true;
    }
    public boolean runOne() {
        Task task;
        synchronized (this) {
            if (tasks.isEmpty() || tasks.peek().due > now) return false;
            task = tasks.remove();
        }
        Looper previous = ACTIVE.get(); ACTIVE.set(this);
        try { task.action.run(); }
        finally { if (previous == null) ACTIVE.remove(); else ACTIVE.set(previous); }
        return true;
    }
    public void drain() {
        int count = 0;
        while (runOne()) if (++count > 10000) throw new AssertionError("worker queue did not settle");
    }
    public void advanceBy(long milliseconds) {
        if (milliseconds < 0) throw new IllegalArgumentException("negative clock advance");
        synchronized (this) { now += milliseconds; }
        drain();
    }
    public static void requireWorker() {
        if (ACTIVE.get() == null) throw new AssertionError("MediaPlayer accessed outside its worker");
    }
}
