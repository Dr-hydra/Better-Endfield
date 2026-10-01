package dev.betterendfield.android;

import java.util.Random;

/** Pure UI geometry checks. Run with javac/java; no Android runtime or JNI. */
public final class OverlayGeometryTest {
    public static void main(String[] args) {
        // Landscape phone with cutout and gesture navigation, at both drag edges.
        verify(640, 320, 36, 0, 0, 24, 8, 48, 480, 560, 8, 0, 0);
        verify(640, 320, 36, 0, 0, 24, 8, 48, 480, 560, 8, 1, 1);
        // Rotation, split screen, keyboard, and pre-layout/fully consumed space.
        verify(320, 640, 0, 24, 0, 36, 8, 48, 480, 560, 8, 0.5f, 0.5f);
        verify(240, 160, 24, 0, 24, 20, 8, 48, 480, 560, 8, 1, 1);
        verify(320, 640, 0, 24, 0, 420, 8, 48, 480, 560, 8, 0.5f, 1);
        verify(0, 0, 0, 0, 0, 0, 8, 48, 480, 560, 8, 1, 1);
        verify(20, 10, 30, 30, 30, 30, 8, 48, 480, 560, 8, Float.NaN, Float.POSITIVE_INFINITY);
        Random random = new Random(20261001);
        for (int i = 0; i < 100000; i++) {
            verify(random.nextInt(4001), random.nextInt(4001), random.nextInt(200), random.nextInt(200),
                    random.nextInt(200), random.nextInt(200), random.nextInt(40), random.nextInt(160),
                    random.nextInt(1600), random.nextInt(1600), random.nextInt(40),
                    random.nextFloat() * 3 - 1, random.nextFloat() * 3 - 1);
        }
        check(OverlayGeometry.normalized(90, 10, 100, 20) == 1f, "drag right edge");
        check(OverlayGeometry.normalized(-20, 10, 100, 20) == 0f, "drag left edge");
        check(OverlayGeometry.normalized(90, 10, 20, 20) == 0f, "no draggable space");
        OverlayGeometry.Layout first = OverlayGeometry.layout(640, 320, 36, 0, 0, 24, 8, 48, 480, 560, 8, 0.2f, 0.4f);
        float nx = OverlayGeometry.normalized(first.handle().left(), first.safe().left(), first.safe().width(), first.handle().width());
        float ny = OverlayGeometry.normalized(first.handle().top(), first.safe().top(), first.safe().height(), first.handle().height());
        OverlayGeometry.Layout second = OverlayGeometry.layout(640, 320, 36, 0, 0, 24, 8, 48, 480, 560, 8, nx, ny);
        check(first.handle().equals(second.handle()), "drag normalization round-trip");
        System.out.println("OverlayGeometryTest: 100007 viewport cases and drag checks passed");
    }

    private static void verify(int width, int height, int il, int it, int ir, int ib,
            int margin, int handle, int pw, int ph, int gap, float x, float y) {
        OverlayGeometry.Layout layout = OverlayGeometry.layout(width, height, il, it, ir, ib, margin, handle, pw, ph, gap, x, y);
        inside(layout.safe(), new OverlayGeometry.Rect(0, 0, width, height));
        inside(layout.handle(), layout.safe()); inside(layout.panel(), layout.safe());
        check(layout.panel().width() <= pw && layout.panel().height() <= ph, "panel preference bound");
        check(layout.handle().width() <= handle && layout.handle().height() <= handle, "handle preference bound");
    }
    private static void inside(OverlayGeometry.Rect inner, OverlayGeometry.Rect outer) {
        check(inner.width() >= 0 && inner.height() >= 0, "nonnegative size");
        check(inner.left() >= outer.left() && inner.top() >= outer.top()
                && inner.right() <= outer.right() && inner.bottom() <= outer.bottom(), "overflow: " + inner + " in " + outer);
    }
    private static void check(boolean condition, String message) { if (!condition) throw new AssertionError(message); }
}
