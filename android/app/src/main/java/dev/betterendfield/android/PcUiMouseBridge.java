package dev.betterendfield.android;

import android.app.Activity;
import android.app.Application;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.view.InputDevice;
import android.view.InputEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;

import java.lang.reflect.Method;
import java.util.IdentityHashMap;
import java.util.Map;
import java.util.function.Consumer;

/** Captured Android mouse deltas for the game's existing Mouse X / Mouse Y axes. */
final class PcUiMouseBridge {
    interface NativeInput {
        boolean ready();
        boolean requested();
        void captured(boolean value);
        void motion(float dx, float dy);
    }

    private static PcUiMouseBridge instance;
    private final Handler main = new Handler(Looper.getMainLooper());
    private final Map<Activity, Session> sessions = new IdentityHashMap<>();
    private final NativeInput input;
    private final Consumer<String> log;
    private Session active;

    PcUiMouseBridge(NativeInput input, Consumer<String> log) {
        this.input = input;
        this.log = log;
    }

    static void install(Application application, Consumer<String> log) {
        if (instance != null) return;
        PcUiMouseBridge bridge = new PcUiMouseBridge(new NativeInput() {
            public boolean ready() { return RuntimeBootstrap.loaded(); }
            public boolean requested() { return NativeCommandBridge.pcMouseCaptureRequested(); }
            public void captured(boolean value) { NativeCommandBridge.pcMouseCaptured(value); }
            public void motion(float dx, float dy) { NativeCommandBridge.pcMouseMotion(dx, dy); }
        }, log);
        application.registerActivityLifecycleCallbacks(bridge.callbacks());
        instance = bridge;
    }

    static boolean capturedEvent(View view, MotionEvent event) {
        return instance != null && instance.handle(view, event);
    }

    static void captureChanged(View view, boolean captured) {
        if (instance != null) instance.onCaptureChanged(view, captured);
    }

    static void windowFocusChanged(View view, boolean focused) {
        if (instance != null && !focused && instance.active != null) {
            Session session = instance.active;
            if (view == session.unity || view == session.captureView) instance.release(session);
        }
    }

    Application.ActivityLifecycleCallbacks callbacks() {
        return new Application.ActivityLifecycleCallbacks() {
            public void onActivityResumed(Activity activity) { resume(activity); }
            public void onActivityPaused(Activity activity) { pause(activity); }
            public void onActivityDestroyed(Activity activity) { destroy(activity); }
            public void onActivityCreated(Activity activity, Bundle state) { }
            public void onActivityStarted(Activity activity) { }
            public void onActivityStopped(Activity activity) { }
            public void onActivitySaveInstanceState(Activity activity, Bundle state) { }
        };
    }

    void resume(Activity activity) {
        if (active != null && active.activity != activity) pause(active.activity);
        Session session = sessions.computeIfAbsent(activity, Session::new);
        active = session;
        session.resumed = true;
        main.removeCallbacks(session.poll);
        main.post(session.poll);
    }

    void pause(Activity activity) {
        Session session = sessions.get(activity);
        if (session == null) return;
        session.resumed = false;
        main.removeCallbacks(session.poll);
        release(session);
    }

    void destroy(Activity activity) {
        pause(activity);
        Session session = sessions.remove(activity);
        if (active == session) active = null;
    }

    private final class Session {
        final Activity activity;
        boolean resumed;
        boolean reportedFailure;
        boolean transportFailed;
        boolean notifiedCaptured;
        boolean requestedCapture;
        boolean ownsCapture;
        long nextRequestAt;
        View unity;
        View captureView;
        Method inject;
        MotionEvent heldButtons;
        final Runnable poll = new Runnable() {
            @Override public void run() {
                if (!resumed) return;
                try { update(Session.this); }
                catch (RuntimeException | LinkageError error) {
                    transportFailed = true;
                    release(Session.this);
                    if (!reportedFailure) {
                        reportedFailure = true;
                        log.accept("PC mouse capture unavailable: " + error);
                    }
                }
                if (resumed) main.postDelayed(this, 50);
            }
        };
        Session(Activity activity) { this.activity = activity; }
    }

    void update(Session session) {
        if (session.transportFailed && session.unity != null && session.unity.isAttachedToWindow()) {
            release(session);
            return;
        }
        if (!session.resumed || !session.activity.hasWindowFocus() || !input.ready()
                || !input.requested()) {
            release(session);
            return;
        }
        if (session.unity == null || !session.unity.isAttachedToWindow()) {
            release(session);
            session.unity = findUnity(session.activity.getWindow().getDecorView(), 0);
            session.inject = null;
            session.transportFailed = false;
            if (session.unity != null) {
                try { session.inject = session.unity.getClass().getMethod("injectEvent", InputEvent.class); }
                catch (NoSuchMethodException missing) {
                    if (!session.reportedFailure) {
                        session.reportedFailure = true;
                        log.accept("PC mouse capture unavailable: UnityPlayer.injectEvent(InputEvent) missing");
                    }
                }
            }
        }
        if (session.unity == null || session.inject == null || !session.unity.isShown()
                || session.unity.getWidth() <= 0 || session.unity.getHeight() <= 0) return;
        View focused = session.unity.findFocus();
        if (focused == null || !focused.isAttachedToWindow()) { release(session); return; }
        if (session.captureView != null && session.captureView != focused) release(session);
        if (session.requestedCapture && focused.hasPointerCapture()) {
            notifyCaptured(session, true);
            return;
        }
        // A game-owned capture remains owned by its original listener.
        if (!session.requestedCapture && focused.hasPointerCapture()) return;
        if (!hasMouse()) { release(session); return; }
        long now = android.os.SystemClock.uptimeMillis();
        if (now < session.nextRequestAt) return;
        session.captureView = focused;
        if (!session.requestedCapture) log.accept("PC mouse capture requested for Unity input");
        session.requestedCapture = true;
        session.ownsCapture = true;
        session.nextRequestAt = now + 500;
        focused.requestPointerCapture();
    }

    private static boolean hasMouse() {
        for (int id : InputDevice.getDeviceIds()) {
            InputDevice device = InputDevice.getDevice(id);
            if (device != null && (device.getSources() & InputDevice.SOURCE_MOUSE) == InputDevice.SOURCE_MOUSE)
                return true;
        }
        return false;
    }

    private static View findUnity(View view, int depth) {
        if (view == null || depth > 16) return null;
        for (Class<?> type = view.getClass(); type != null; type = type.getSuperclass())
            if (type.getName().equals("com.unity3d.player.UnityPlayer")) return view;
        if (view instanceof ViewGroup group) {
            for (int i = 0; i < group.getChildCount(); i++) {
                View found = findUnity(group.getChildAt(i), depth + 1);
                if (found != null) return found;
            }
        }
        return null;
    }

    private void notifyCaptured(Session session, boolean value) {
        // Native intent can clear its capture bit during a short menu transition
        // before the UI thread sees it. Re-ack actual platform capture; native
        // treats repeated true as idempotent and retains queued motion.
        input.captured(value);
        if (session.notifiedCaptured == value) return;
        session.notifiedCaptured = value;
        log.accept(value ? "PC mouse capture granted" : "PC mouse capture released");
    }

    void onCaptureChanged(View view, boolean captured) {
        Session session = active;
        if (session == null || view != session.captureView || !session.ownsCapture) return;
        try {
            if (!captured) {
                notifyCaptured(session, false);
                releaseButtons(session);
                session.requestedCapture = false;
                session.ownsCapture = false;
            } else if (session.requestedCapture && session.resumed && session.activity.hasWindowFocus() && input.ready() && input.requested()) {
                notifyCaptured(session, true);
            } else release(session);
        } catch (RuntimeException | LinkageError error) {
            session.transportFailed = true;
            release(session);
            if (!session.reportedFailure) {
                session.reportedFailure = true;
                log.accept("PC mouse capture callback failed: " + error);
            }
        }
    }

    boolean handle(View view, MotionEvent event) {
        Session session = active;
        if (session == null || view != session.captureView || !session.ownsCapture
                || !event.isFromSource(InputDevice.SOURCE_MOUSE_RELATIVE)) return false;
        try {
            if (!session.requestedCapture || !session.resumed || !session.activity.hasWindowFocus() || !input.ready() || !input.requested()) {
                release(session);
                return true; // Do not pass relative coordinates to the absolute Unity path during release.
            }
            notifyCaptured(session, view.hasPointerCapture());
            if (!session.notifiedCaptured) return true;
            int action = event.getActionMasked();
            if (action == MotionEvent.ACTION_MOVE || action == MotionEvent.ACTION_HOVER_MOVE) {
                for (int i = 0; i < event.getHistorySize(); i++)
                    input.motion(event.getHistoricalX(i), event.getHistoricalY(i));
                input.motion(event.getX(), event.getY());
                if (event.getButtonState() != 0 || session.heldButtons != null) forward(session, event);
            } else if (action == MotionEvent.ACTION_DOWN || action == MotionEvent.ACTION_UP
                    || action == MotionEvent.ACTION_BUTTON_PRESS || action == MotionEvent.ACTION_BUTTON_RELEASE
                    || action == MotionEvent.ACTION_SCROLL || action == MotionEvent.ACTION_CANCEL) {
                forward(session, event);
            }
            return true;
        } catch (ReflectiveOperationException | RuntimeException | LinkageError error) {
            session.transportFailed = true;
            release(session);
            if (!session.reportedFailure) {
                session.reportedFailure = true;
                log.accept("PC mouse input failed: " + error);
            }
            return true;
        }
    }

    private void forward(Session session, MotionEvent event) throws ReflectiveOperationException {
        MotionEvent normal = MotionEvent.obtain(event);
        try {
            normal.setSource(InputDevice.SOURCE_MOUSE);
            normal.setLocation(session.unity.getWidth() * 0.5f, session.unity.getHeight() * 0.5f);
            if (Boolean.FALSE.equals(session.inject.invoke(session.unity, normal)))
                throw new IllegalStateException("UnityPlayer rejected mouse button/scroll input");
            if (session.heldButtons != null) session.heldButtons.recycle();
            session.heldButtons = normal.getButtonState() != 0 ? MotionEvent.obtain(normal) : null;
        } finally { normal.recycle(); }
    }

    private void releaseButtons(Session session) {
        MotionEvent held = session.heldButtons;
        session.heldButtons = null;
        if (held == null) return;
        MotionEvent release = null;
        try {
            int count = held.getPointerCount();
            MotionEvent.PointerProperties[] properties = new MotionEvent.PointerProperties[count];
            MotionEvent.PointerCoords[] coordinates = new MotionEvent.PointerCoords[count];
            for (int i = 0; i < count; i++) {
                properties[i] = new MotionEvent.PointerProperties();
                coordinates[i] = new MotionEvent.PointerCoords();
                held.getPointerProperties(i, properties[i]);
                held.getPointerCoords(i, coordinates[i]);
            }
            release = MotionEvent.obtain(held.getDownTime(), android.os.SystemClock.uptimeMillis(),
                    MotionEvent.ACTION_UP, count, properties, coordinates, held.getMetaState(), 0,
                    held.getXPrecision(), held.getYPrecision(), held.getDeviceId(), held.getEdgeFlags(),
                    InputDevice.SOURCE_MOUSE, held.getFlags());
            if (session.inject != null && session.unity != null) session.inject.invoke(session.unity, release);
        } catch (ReflectiveOperationException | RuntimeException ignored) {
            // Focus/lifecycle teardown can invalidate UnityPlayer before delivery.
        } finally {
            held.recycle();
            if (release != null) release.recycle();
        }
    }

    private void release(Session session) {
        boolean actualCapture = session.notifiedCaptured
                || session.captureView != null && session.captureView.hasPointerCapture();
        if (session.notifiedCaptured) {
            try { notifyCaptured(session, false); }
            catch (RuntimeException | LinkageError ignored) { session.notifiedCaptured = false; }
        }
        releaseButtons(session);
        if (session.requestedCapture && session.captureView != null) session.captureView.releasePointerCapture();
        session.requestedCapture = false;
        session.nextRequestAt = 0;
        if (!actualCapture) {
            // A rejected/pending request need not produce a capture=false event.
            // Clear ownership now so a later game-owned capture stays untouched.
            session.ownsCapture = false;
            session.captureView = null;
        }
    }
}
