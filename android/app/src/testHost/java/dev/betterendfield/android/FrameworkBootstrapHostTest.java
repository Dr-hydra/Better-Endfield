package dev.betterendfield.android;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import java.util.concurrent.TimeUnit;

/** Real production lifecycle with asynchronous framework connection events. */
public final class FrameworkBootstrapHostTest {
    private static int checks;
    private static void check(boolean value, String message) {
        ++checks;
        if (!value) throw new AssertionError(message);
    }

    public static void main(String[] args) throws Exception {
        activityLifecycle();
        updateReceiver();
        System.out.println("PASS framework bootstrap: " + checks + " active checks; cached-owner recovery, bounded foreground lifecycle, no payload consumption, callback cleanup, receiver release on connection/interruption/error/timeout");
    }

    private static FrameworkBootstrapActivity activity(boolean connected) {
        Handler.reset();FrameworkSettings.reset(connected);OverlaySettingsDiagnostics.reset();
        FrameworkBootstrapActivity activity = new FrameworkBootstrapActivity();
        activity.onCreate(new Bundle());
        return activity;
    }

    private static void activityLifecycle() {
        FrameworkBootstrapActivity connected = activity(true);
        check(connected.creates == 1 && !connected.isFinishing() && Handler.pending() == 0,
                "bootstrap must reach resume so cached Binder owners receive a foreground event");
        connected.onResume();Handler.advance(499);
        check(!connected.isFinishing(), "already-connected bootstrap ended before the minimum foreground window");
        check(OverlaySettingsDiagnostics.statuses().equals("bootstrap=foreground"), "bootstrap connection milestone preceded its foreground window");
        Handler.advance(1);
        check(connected.finishes == 1 && Handler.pending() == 0, "connected bootstrap did not finish and clear callbacks");
        connected.onPause();connected.onDestroy();Handler.advance(2000);
        check(OverlaySettingsDiagnostics.statuses().equals("bootstrap=foreground,bootstrap=connected"), "normal bootstrap milestones were missing or duplicated");
        check(connected.finishes == 1 && connected.intentReads == 0 && Handler.pending() == 0,
                "normal teardown repeated finish, consumed caller data, or leaked a callback");

        FrameworkBootstrapActivity cached = activity(false);
        cached.onResume();Handler.advance(500);
        check(!cached.isFinishing(), "cached owner without a Binder finished before connection renewal");
        FrameworkSettings.connect();Handler.advance(100);
        check(cached.finishes == 1 && Handler.pending() == 0 && cached.intentReads == 0,
                "renewed cached owner did not finish cleanly without consuming caller data");
        check(OverlaySettingsDiagnostics.statuses().equals("bootstrap=foreground,bootstrap=connected"), "cached-owner renewal was not diagnosed");

        FrameworkBootstrapActivity timeout = activity(false);
        timeout.onResume();Handler.advance(1499);
        check(!timeout.isFinishing(), "disconnected bootstrap did not wait for its bounded connection opportunity");
        Handler.advance(1);
        check(timeout.finishes == 1 && Handler.pending() == 0, "connection timeout left an invisible foreground activity alive");
        check(OverlaySettingsDiagnostics.statuses().equals("bootstrap=foreground,bootstrap=timeout"), "bootstrap timeout milestone was missing or reported connected");

        FrameworkBootstrapActivity paused = activity(false);
        paused.onResume();Handler.advance(200);paused.onPause();
        int queries = FrameworkSettings.queries;
        check(paused.finishes == 1 && Handler.pending() == 0, "losing foreground retained an active bootstrap callback");
        Handler.advance(2000);
        check(paused.finishes == 1 && FrameworkSettings.queries == queries, "paused bootstrap continued polling or finished again");
        check(OverlaySettingsDiagnostics.statuses().equals("bootstrap=foreground"), "paused bootstrap emitted a callback milestone after removal");

        FrameworkBootstrapActivity destroyed = activity(false);
        destroyed.onResume();Handler.advance(200);destroyed.onDestroy();
        queries = FrameworkSettings.queries;Handler.advance(2000);
        check(destroyed.isDestroyed() && Handler.pending() == 0 && FrameworkSettings.queries == queries,
                "destroyed bootstrap retained its lifecycle or framework polling callback");

        FrameworkBootstrapActivity finishing = activity(false);
        finishing.finish();finishing.onResume();
        check(Handler.pending() == 0 && finishing.finishes == 1, "finishing activity restarted bootstrap polling");
    }

    private static BroadcastReceiver.PendingResult startReceiver(ModulePackageReplacedReceiver receiver) {
        receiver.onReceive(new Context(), new Intent(Intent.ACTION_MY_PACKAGE_REPLACED));
        return BroadcastReceiver.latest;
    }

    private static void released(BroadcastReceiver.PendingResult pending, long timeoutSeconds) throws Exception {
        check(pending.done.await(timeoutSeconds, TimeUnit.SECONDS), "async update broadcast was not released within its bound");
        Thread worker = FrameworkSettings.worker;
        if (worker != null) worker.join(1000);
        check(pending.finishes.get() == 1, "async update broadcast was finished multiple times");
        check(worker != null && !worker.isAlive(), "update receiver worker leaked after release");
    }

    private static void updateReceiver() throws Exception {
        ModulePackageReplacedReceiver receiver = new ModulePackageReplacedReceiver();
        FrameworkSettings.reset(false);BroadcastReceiver.asyncCalls = 0;OverlaySettingsDiagnostics.reset();
        receiver.onReceive(new Context(), null);
        Intent unrelated = new Intent("android.intent.action.PACKAGE_REPLACED");
        receiver.onReceive(new Context(), unrelated);
        check(BroadcastReceiver.asyncCalls == 0 && FrameworkSettings.queries == 0 && unrelated.payloadReads == 0,
                "unrelated broadcast started framework work or consumed a setting payload");
        check(OverlaySettingsDiagnostics.statuses().isEmpty(), "unrelated broadcast emitted a framework update milestone");
        FrameworkSettings.reset(true);startReceiver(receiver);
        check(BroadcastReceiver.asyncCalls == 0, "already-connected update unnecessarily retained the broadcast");
        check(OverlaySettingsDiagnostics.statuses().equals("package_replace=connected"), "already-connected update milestone was missing");

        FrameworkSettings.reset(false);OverlaySettingsDiagnostics.reset();
        long began = System.nanoTime();
        BroadcastReceiver.PendingResult pending = startReceiver(receiver);
        check(TimeUnit.NANOSECONDS.toMillis(System.nanoTime() - began) < 1000, "onReceive blocked the main thread awaiting the Binder");
        check(FrameworkSettings.entered.await(1, TimeUnit.SECONDS), "update receiver never started its background connection wait");
        check(FrameworkSettings.worker != Thread.currentThread() && pending.finishes.get() == 0,
                "receiver waited on the caller thread or released before connection");
        FrameworkSettings.connect();released(pending, 2);
        check(OverlaySettingsDiagnostics.statuses().equals("package_replace=waiting,package_replace=connected"), "asynchronous update connection milestones were out of order");

        FrameworkSettings.reset(false);OverlaySettingsDiagnostics.reset();pending = startReceiver(receiver);
        check(FrameworkSettings.entered.await(1, TimeUnit.SECONDS), "interruption test did not enter the connection wait");
        FrameworkSettings.worker.interrupt();released(pending, 2);
        check(FrameworkSettings.worker.isInterrupted(), "connection wait swallowed worker interruption");
        check(OverlaySettingsDiagnostics.statuses().equals("package_replace=waiting,package_replace=unavailable"), "interrupted update falsely reported success or timeout");

        FrameworkSettings.reset(false);OverlaySettingsDiagnostics.reset();FrameworkSettings.throwOnWorker = true;
        pending = startReceiver(receiver);released(pending, 2);
        check(OverlaySettingsDiagnostics.statuses().equals("package_replace=waiting,package_replace=unavailable"), "failed update worker did not record unavailability");

        FrameworkSettings.reset(false);OverlaySettingsDiagnostics.reset();began = System.nanoTime();
        pending = startReceiver(receiver);released(pending, 12);
        long elapsed = TimeUnit.NANOSECONDS.toMillis(System.nanoTime() - began);
        check(elapsed >= 7500 && elapsed < 10000, "update receiver timeout did not preserve headroom within the broadcast budget: " + elapsed);
        check(OverlaySettingsDiagnostics.statuses().equals("package_replace=waiting,package_replace=timeout"), "update timeout milestone was missing or reported connected");
    }
}
