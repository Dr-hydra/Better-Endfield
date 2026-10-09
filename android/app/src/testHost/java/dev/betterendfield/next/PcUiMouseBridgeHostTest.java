package dev.betterendfield.next;

import android.app.Activity;
import android.os.Handler;
import android.os.SystemClock;
import android.view.InputDevice;
import android.view.MotionEvent;
import android.view.View;
import android.view.PointerIcon;
import com.unity3d.player.UnityPlayer;
import java.util.ArrayList;

/** Lifecycle and event transport regression using the real main Java bridge. */
public final class PcUiMouseBridgeHostTest {
    private static int checks;
    private static final class Native implements PcUiMouseBridge.NativeInput {
        boolean ready = true, requested = true;
        boolean actual;
        int acknowledgements;
        final ArrayList<Boolean> capture = new ArrayList<>();
        float x, y;
        int moves;
        boolean enabled = true, directTouch;
        int absoluteEvents;
        float absoluteX, absoluteY;
        public boolean ready() { return ready; }
        public boolean requested() { return requested; }
        public int cursorMode() { return !enabled || !ready ? 0 : requested ? 1 : 2; }
        public void captured(boolean value) {
            acknowledgements++;
            if (actual != value) capture.add(value);
            actual = value;
        }
        public void motion(float dx, float dy) { x += dx; y += dy; moves++; }
        public void absolute(float x, float y) { absoluteX = x; absoluteY = y; absoluteEvents++; }
        public void directTouch(boolean active) { directTouch = active; }
    }
    private static final class Fixture {
        final Native input = new Native();
        final Activity activity = new Activity();
        final UnityPlayer unity = new UnityPlayer();
        final ArrayList<String> errors = new ArrayList<>();
        final PcUiMouseBridge bridge = new PcUiMouseBridge(input, errors::add);
        Fixture() {
            Handler.reset(); SystemClock.time = 1000; InputDevice.mouse = true;
            activity.window.decor = unity;
        }
        void poll() { Handler.runNext(); }
        void start() { bridge.resume(activity); poll(); }
        void grant() { unity.captured = true; bridge.onCaptureChanged(unity, true); }
        void close() { bridge.destroy(activity); Handler.reset(); }
    }
    public static void main(String[] args) {
        Fixture f = new Fixture();
        f.input.ready = false; f.start();
        check(f.unity.requests == 0, "no capture before native consumer ready");
        f.input.ready = true; f.input.requested = false; f.poll();
        check(f.unity.requests == 0, "normal touch/default does not capture");
        f.input.requested = true; InputDevice.mouse = false; f.poll();
        check(f.unity.requests == 0, "no pointer request without physical mouse");
        InputDevice.mouse = true; f.activity.focus = false; f.poll();
        check(f.unity.requests == 0, "no capture behind another focused window");
        f.activity.focus = true; f.unity.focus = null; f.poll();
        check(f.unity.requests == 0, "do not steal focus from outside Unity");
        f.unity.focus = f.unity; f.unity.captured = true; f.poll();
        check(f.unity.requests == 0 && f.input.capture.isEmpty(), "leave preexisting game-owned capture alone");
        f.unity.captured = false; f.poll();
        check(f.unity.requests == 1 && f.input.capture.isEmpty(), "request alone is not a capture grant");
        f.poll(); check(f.unity.requests == 1, "do not hammer platform after capture denial");
        SystemClock.time += 500; f.poll();
        check(f.unity.requests == 2, "denied capture can retry while Unity remains focused");
        f.grant(); f.poll(); f.bridge.onCaptureChanged(f.unity, true);
        check(f.input.capture.equals(java.util.List.of(true)), "actual grant notified once, preserving queued deltas");
        MotionEvent move = event(MotionEvent.ACTION_MOVE, 0.25f, -0.5f, 0);
        move.history = new float[][]{{1.5f, 2.25f}, {-0.25f, 0.5f}};
        check(f.bridge.handle(f.unity, move), "owned captured motion handled");
        check(f.input.x == 1.5f && f.input.y == 2.25f && f.input.moves == 3,
                "batched relative samples and subpixel deltas preserved without amplification");
        check(f.unity.injected.isEmpty(), "relative motion never enters Unity's absolute mouse path");
        check(!f.bridge.handle(new View(), move), "unrelated captured view remains on original path");
        MotionEvent ordinary = event(MotionEvent.ACTION_MOVE, 4, 5, 0);
        ordinary.source = InputDevice.SOURCE_MOUSE;
        check(!f.bridge.handle(f.unity, ordinary), "ordinary mouse events remain on original path");
        MotionEvent button = event(MotionEvent.ACTION_BUTTON_PRESS, -5, 3, 2);
        button.actionButton = 2;
        check(f.bridge.handle(f.unity, button), "captured button handled");
        MotionEvent sent = f.unity.injected.get(0);
        check(sent.source == InputDevice.SOURCE_MOUSE && sent.x == 960 && sent.y == 540,
                "button becomes ordinary mouse at Unity view center");
        check(sent.buttons == 2 && sent.actionButton == 2 && sent.device == 7 && sent.meta == 5
                && sent.flags == 4 && sent.edge == 3 && sent.down == 10 && sent.time == 20,
                "button action/state, modifier, device and event timing preserved");
        check(button.source == InputDevice.SOURCE_MOUSE_RELATIVE && button.x == -5 && button.y == 3,
                "input MotionEvent remains unchanged");
        MotionEvent scroll = event(MotionEvent.ACTION_SCROLL, 0, 0, 2);
        scroll.scrollX = -1.5f; scroll.scrollY = 2.5f;
        f.bridge.handle(f.unity, scroll);
        sent = f.unity.injected.get(1);
        check(sent.scrollX == -1.5f && sent.scrollY == 2.5f && sent.buttons == 2,
                "captured horizontal/vertical scrolling preserved");
        f.input.requested = false;
        check(f.bridge.handle(f.unity, move), "in-flight relative motion consumed while menu releases capture");
        check(f.input.capture.equals(java.util.List.of(true, false)) && f.input.moves == 3,
                "menu immediately clears actual capture and stops motion delivery");
        sent = f.unity.injected.get(f.unity.injected.size() - 1);
        check(sent.action == MotionEvent.ACTION_UP && sent.buttons == 0,
                "held game mouse buttons released with capture");
        check(f.unity.releases == 1, "menu releases platform capture");
        check(f.bridge.handle(f.unity, move), "async release does not leak relative coordinates into Unity");
        f.unity.captured = false; f.bridge.onCaptureChanged(f.unity, false);
        check(!f.bridge.handle(f.unity, move), "after ownership ends original event path resumes");
        f.close();

        f = new Fixture(); f.start(); f.grant();
        f.bridge.pause(f.activity);
        check(f.unity.releases == 1 && f.input.capture.equals(java.util.List.of(true, false)),
                "activity pause releases capture even before another poll");
        int requests = f.unity.requests; f.poll();
        check(f.unity.requests == requests, "background activities stop polling/reacquiring");
        f.close();

        f = new Fixture(); f.start(); f.grant();
        f.activity.focus = false; f.poll();
        check(f.unity.releases == 1 && f.input.capture.get(1) == false, "dialog/window focus loss releases capture");
        f.close();

        f = new Fixture(); f.start(); f.grant();
        f.unity.focus = null; f.poll();
        check(f.unity.releases == 1, "focus leaving Unity releases capture");
        f.close();

        f = new Fixture(); f.start(); f.grant();
        f.unity.accepts = false;
        f.bridge.handle(f.unity, event(MotionEvent.ACTION_BUTTON_PRESS, 2, 3, 1));
        check(f.unity.releases == 1 && f.input.capture.equals(java.util.List.of(true, false))
                && f.errors.stream().filter(line -> line.contains("input failed")).count() == 1,
                "rejected Unity event fails closed and restores ordinary input");
        f.unity.captured = false; f.bridge.onCaptureChanged(f.unity, false);
        requests = f.unity.requests; f.poll();
        check(f.unity.requests == requests, "failed input transport does not reacquire every poll");
        UnityPlayer replacement = new UnityPlayer();
        f.unity.attached = false; f.activity.window.decor = replacement; f.poll();
        check(replacement.requests == 1, "new Unity instance can retry a failed transport");
        f.close();

        f = new Fixture(); f.start();
        f.input.requested = false; f.poll(); // Pending request was never granted.
        check(f.unity.releases == 1 && f.input.capture.isEmpty(), "ungranted request is cancelled without fake native grant");
        f.unity.captured = true; // Game subsequently acquires its own capture.
        check(!f.bridge.handle(f.unity, event(MotionEvent.ACTION_MOVE, 1, 2, 0)),
                "cancelled pending request does not retain ownership of later game capture");
        f.close();

        f = new Fixture(); f.start(); f.grant();
        f.input.actual = false; // Native saw menu show/hide before the UI poll.
        f.poll();
        check(f.input.actual && f.unity.requests == 1,
                "short native menu transition re-acknowledges actual capture without another platform request");
        check(f.errors.stream().filter(line -> line.equals("PC mouse capture granted")).count() == 1,
                "idempotent native re-ack does not repeat lifecycle diagnostics");
        f.close();

        f = new Fixture(); f.unity.throwRequest = true; f.start();
        requests = f.unity.requests; f.poll(); f.poll();
        check(requests == 1 && f.unity.requests == 1 && f.input.capture.isEmpty(),
                "platform capture exception falls back without repeated requests");
        check(f.errors.stream().filter(line -> line.equals("PC mouse capture requested for Unity input")).count() == 1,
                "platform capture exception does not spam request diagnostics");
        f.close();
        f = new Fixture(); f.input.requested = false;
        PointerIcon hidden = new PointerIcon(PointerIcon.TYPE_NULL);
        View surface = new View(); surface.icon = hidden; f.unity.children.add(surface);
        f.start();
        check(f.unity.requests == 0 && surface.icon.type == PointerIcon.TYPE_ARROW,
                "game visible intent restores an actual Android pointer on Unity surface children");
        MotionEvent absolute = event(MotionEvent.ACTION_DOWN, 384, 270, 1);
        absolute.source = InputDevice.SOURCE_MOUSE;
        f.bridge.observe(f.unity, absolute);
        check(f.input.absoluteEvents == 1 && f.input.absoluteX == 0.2f && f.input.absoluteY == 0.25f,
                "physical menu mouse normalized against current Unity viewport, without center recentering");
        check(absolute.action == MotionEvent.ACTION_DOWN && absolute.x == 384 && absolute.y == 270 && absolute.buttons == 1
                && !absolute.recycled && f.unity.injected.isEmpty(), "observer preserves source button event and original dispatch");
        f.unity.width = 960; f.unity.height = 540; f.bridge.observe(f.unity, absolute);
        check(f.input.absoluteX == 0.4f && f.input.absoluteY == 0.5f, "resize is reflected on the next absolute event");
        f.bridge.observe(new View(), absolute); f.bridge.observe(f.unity, event(MotionEvent.ACTION_MOVE, 2, 3, 0));
        check(f.input.absoluteEvents == 2, "unrelated views and relative samples never publish absolute coordinates");
        absolute.x = Float.NaN; f.bridge.observe(f.unity, absolute);
        check(f.input.absoluteEvents == 2, "invalid menu coordinates do not replace the last valid pointer");
        MotionEvent finger = event(MotionEvent.ACTION_DOWN, 10, 20, 0); finger.source = InputDevice.SOURCE_TOUCHSCREEN;
        f.bridge.observe(f.unity, finger); check(f.input.directTouch, "real touchscreen press retains Unity touch authority");
        finger.action = MotionEvent.ACTION_UP; f.bridge.observe(f.unity, finger);
        check(!f.input.directTouch, "last touchscreen release clears held contact state");
        finger.action = 6; f.bridge.observe(f.unity, finger); // ACTION_POINTER_UP: another finger is still down.
        check(f.input.directTouch, "multi-touch pointer-up does not release the remaining contacts");
        finger.action = MotionEvent.ACTION_CANCEL; f.bridge.observe(f.unity, finger);
        check(!f.input.directTouch, "touch cancellation clears contact ownership");
        MotionEvent mouseUp = event(MotionEvent.ACTION_UP, 384, 270, 0); mouseUp.source = InputDevice.SOURCE_MOUSE;
        f.bridge.observe(f.unity, mouseUp);
        check(f.input.absoluteX == 0.4f && f.input.absoluteY == 0.5f && mouseUp.action == MotionEvent.ACTION_UP,
                "button release samples the same true local position as the press without replacing its action");
        f.input.requested = true; f.poll();
        check(surface.icon == hidden && f.unity.icon == null, "gameplay returns original per-view pointer icons");
        f.bridge.observe(f.unity, absolute); check(f.input.absoluteEvents == 3, "hidden-cursor gameplay does not publish absolute positions");
        f.input.requested = false; f.poll(); f.input.enabled = false; f.poll();
        check(surface.icon == hidden, "configuration disable restores hidden/default icons");
        f.input.enabled = true; f.poll(); f.bridge.pause(f.activity);
        check(surface.icon == hidden && !f.input.directTouch, "pause restores icons and clears touch ownership");
        f.close();
        System.out.println("PcUiMouseBridgeHostTest: " + checks + " lifecycle, delta, absolute pointer and button transport checks passed");
    }
    private static MotionEvent event(int action, float x, float y, int buttons) {
        MotionEvent e = new MotionEvent(); e.action = action; e.x = x; e.y = y; e.buttons = buttons; return e;
    }
    private static void check(boolean value, String message) {
        checks++; if (!value) throw new AssertionError(message);
    }
}
