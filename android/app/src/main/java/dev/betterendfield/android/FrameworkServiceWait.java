package dev.betterendfield.android;

import java.io.IOException;
import java.util.concurrent.TimeUnit;
import java.util.function.BooleanSupplier;

/** Wait on the connection callback without blocking the UI or ignoring cancellation. */
final class FrameworkServiceWait {
    interface Checkpoint { void check() throws IOException; }

    static boolean await(Object monitor, BooleanSupplier connected, long timeoutMillis,
            Checkpoint checkpoint) throws IOException {
        long deadline = System.nanoTime() + TimeUnit.MILLISECONDS.toNanos(timeoutMillis);
        synchronized (monitor) {
            while (true) {
                checkpoint.check();
                if (connected.getAsBoolean()) return true;
                long remaining = deadline - System.nanoTime();
                if (remaining <= 0) return false;
                try {
                    // Cancellation has no connection callback, so check it at least every 250 ms.
                    monitor.wait(Math.min(250, Math.max(1, TimeUnit.NANOSECONDS.toMillis(remaining))));
                } catch (InterruptedException error) {
                    Thread.currentThread().interrupt();
                    throw new IOException("等待框架服务连接已中断", error);
                }
            }
        }
    }
}
