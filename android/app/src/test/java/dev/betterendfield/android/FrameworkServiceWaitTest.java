package dev.betterendfield.android;

import java.io.IOException;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.concurrent.atomic.AtomicReference;

/** Connection timing regression checks; run with javac/java, without a device. */
public final class FrameworkServiceWaitTest {
    public static void main(String[] args) throws Exception {
        Object monitor = new Object();
        check(FrameworkServiceWait.await(monitor, () -> true, 0, () -> {}), "ready service rejected");
        check(!FrameworkServiceWait.await(monitor, () -> false, 30, () -> {}), "missing service accepted");
        delayedConnection();
        cancellation();
        interruption();
        System.out.println("FrameworkServiceWaitTest: ready, timeout, delayed callback, spurious wake, cancellation and interruption passed");
    }

    private static void delayedConnection() throws Exception {
        Object monitor = new Object();
        AtomicBoolean connected = new AtomicBoolean();
        AtomicReference<Throwable> failure = new AtomicReference<>();
        AtomicBoolean success = new AtomicBoolean();
        AtomicInteger checks = new AtomicInteger();
        CountDownLatch started = new CountDownLatch(1), rechecked = new CountDownLatch(1);
        Thread worker = start(() -> {
            try {
                success.set(FrameworkServiceWait.await(monitor, connected::get, 2_000, () -> {
                    if (checks.incrementAndGet() == 1) started.countDown();
                    else rechecked.countDown();
                }));
            } catch (Throwable error) { failure.set(error); }
        });
        check(started.await(1, TimeUnit.SECONDS), "wait did not start");
        synchronized (monitor) { monitor.notifyAll(); }
        check(rechecked.await(1, TimeUnit.SECONDS), "spurious callback was not rechecked");
        check(!success.get(), "spurious callback accepted without service");
        synchronized (monitor) { connected.set(true); monitor.notifyAll(); }
        join(worker);
        check(failure.get() == null && success.get(), "late service callback was rejected: " + failure.get());
    }

    private static void cancellation() throws Exception {
        AtomicBoolean cancelled = new AtomicBoolean();
        AtomicReference<Throwable> failure = new AtomicReference<>();
        CountDownLatch started = new CountDownLatch(1);
        Thread worker = start(() -> {
            try {
                FrameworkServiceWait.await(new Object(), () -> false, 10_000, () -> {
                    started.countDown();
                    if (cancelled.get()) throw new IOException("cancelled");
                });
            } catch (Throwable error) { failure.set(error); }
        });
        check(started.await(1, TimeUnit.SECONDS), "cancellable wait did not start");
        cancelled.set(true); // No connection notification: the timed wake must check cancellation.
        join(worker);
        check(failure.get() instanceof IOException && "cancelled".equals(failure.get().getMessage()),
                "cancellation lost: " + failure.get());
        try {
            FrameworkServiceWait.await(new Object(), () -> true, 0, () -> { throw new IOException("cancelled"); });
            throw new AssertionError("cancelled import accepted a ready service");
        } catch (IOException expected) { check("cancelled".equals(expected.getMessage()), "wrong cancellation error"); }
    }

    private static void interruption() throws Exception {
        AtomicReference<Throwable> failure = new AtomicReference<>();
        AtomicBoolean interrupted = new AtomicBoolean();
        CountDownLatch started = new CountDownLatch(1);
        Thread worker = start(() -> {
            try {
                FrameworkServiceWait.await(new Object(), () -> false, 10_000, started::countDown);
            } catch (Throwable error) {
                failure.set(error); interrupted.set(Thread.currentThread().isInterrupted());
            }
        });
        check(started.await(1, TimeUnit.SECONDS), "interruptible wait did not start");
        worker.interrupt(); join(worker);
        check(failure.get() instanceof IOException && interrupted.get(), "interruption lost");
    }

    private static Thread start(Runnable work) {
        Thread thread = new Thread(work, "framework-wait-test");
        thread.setDaemon(true); thread.start(); return thread;
    }
    private static void join(Thread worker) throws InterruptedException {
        worker.join(2_000);
        if (worker.isAlive()) { worker.interrupt(); throw new AssertionError("connection wait stalled"); }
    }
    private static void check(boolean condition, String message) { if (!condition) throw new AssertionError(message); }
}
