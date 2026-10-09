package dev.betterendfield.next;

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
        check(OverlayGeometry.nearestHorizontalEdge(0.49f) == 0f, "left of midpoint");
        check(OverlayGeometry.nearestHorizontalEdge(0.51f) == 1f, "right of midpoint");
        check(OverlayGeometry.nearestHorizontalEdge(0.5f) == 1f, "midpoint has stable right tie-break");
        check(OverlayGeometry.nearestHorizontalEdge(Float.NaN) == 0f, "invalid position remains reachable");
        verifyRotation(0f);
        verifyRotation(1f);
        verifyDockedTransitions(0f);
        verifyDockedTransitions(1f);
        System.out.println("OverlayGeometryTest: 100007 free + 100007 snapped + 100007 compact-tab viewport cases, rotation and style transitions passed");
    }

    private static void verify(int width, int height, int il, int it, int ir, int ib,
            int margin, int handle, int pw, int ph, int gap, float x, float y) {
        OverlayGeometry.Layout layout = OverlayGeometry.layout(width, height, il, it, ir, ib, margin, handle, pw, ph, gap, x, y);
        inside(layout.safe(), new OverlayGeometry.Rect(0, 0, width, height));
        inside(layout.handle(), layout.safe()); inside(layout.panel(), layout.safe());
        check(layout.panel().width() <= pw && layout.panel().height() <= ph, "panel preference bound");
        check(layout.handle().width() <= handle && layout.handle().height() <= handle, "handle preference bound");
        OverlayGeometry.Layout snapped = OverlayGeometry.layout(width, height, il, it, ir, ib, margin,
                handle, pw, ph, gap, OverlayGeometry.nearestHorizontalEdge(x), y);
        inside(snapped.handle(), snapped.safe()); inside(snapped.panel(), snapped.safe());
        check(snapped.handle().left() == snapped.safe().left()
                || snapped.handle().right() == snapped.safe().right(), "snap reaches a horizontal safe edge");
        check(snapped.handle().top() == layout.handle().top(), "snap preserves vertical position");
        int toLeft = layout.handle().left() - layout.safe().left();
        int toRight = layout.safe().right() - layout.handle().right();
        check(Math.abs(snapped.handle().left() - layout.handle().left()) == Math.min(toLeft, toRight),
                "snap takes the shortest horizontal path");
        OverlayGeometry.Layout tab = OverlayGeometry.layout(width, height, il, it, ir, ib, margin,
                24, 40, pw, ph, gap, OverlayGeometry.nearestHorizontalEdge(x), y, true);
        OverlayGeometry.Rect usable = OverlayGeometry.usableArea(width, height, il, it, ir, ib);
        inside(tab.safe(), usable); inside(tab.handle(), tab.safe()); inside(tab.panel(), tab.safe());
        check(tab.handle().left() == usable.left() || tab.handle().right() == usable.right(),
                "compact tab touches actual inset edge without horizontal margin");
        check(tab.handle().width() <= 24 && tab.handle().height() <= 40, "compact tab remains small");
        check(tab.handle().top() == tab.safe().top()
                + Math.round(OverlayGeometry.fraction(y) * (tab.safe().height() - tab.handle().height())),
                "compact tab preserves normalized height");
    }

    private static void verifyRotation(float edge) {
        // Preserve normalized edge and height through a density/viewport/inset change.
        OverlayGeometry.Layout portrait = OverlayGeometry.layout(1080, 2400, 0, 72, 0, 96,
                24, 144, 1440, 1680, 24, edge, 0.7f);
        OverlayGeometry.Layout landscape = OverlayGeometry.layout(2400, 1080, 144, 0, 0, 72,
                24, 144, 1440, 1680, 24, edge, 0.7f);
        OverlayGeometry.Layout splitScreen = OverlayGeometry.layout(320, 240, 0, 0, 48, 80,
                8, 48, 480, 560, 8, edge, 0.7f);
        for (OverlayGeometry.Layout layout : new OverlayGeometry.Layout[]{portrait, landscape, splitScreen}) {
            inside(layout.handle(), layout.safe()); inside(layout.panel(), layout.safe());
            check(edge == 0f ? layout.handle().left() == layout.safe().left()
                    : layout.handle().right() == layout.safe().right(), "rotation preserves chosen edge");
            float height = OverlayGeometry.normalized(layout.handle().top(), layout.safe().top(),
                    layout.safe().height(), layout.handle().height());
            check(Math.abs(height - 0.7f) <= 0.01f, "rotation preserves normalized height");
        }
    }

    private static void verifyDockedTransitions(float edge) {
        // Phone, rotation with asymmetric cutout, then a window smaller than the normal icon.
        int[][] viewports = {{320, 640, 0, 24, 0, 36}, {640, 320, 36, 0, 0, 24},
                {30, 42, 4, 3, 7, 9}, {0, 0, 0, 0, 0, 0}};
        for (int[] v : viewports) {
            OverlayGeometry.Rect usable = OverlayGeometry.usableArea(v[0], v[1], v[2], v[3], v[4], v[5]);
            OverlayGeometry.Layout tab = OverlayGeometry.layout(v[0], v[1], v[2], v[3], v[4], v[5],
                    8, 24, 40, 480, 560, 8, edge, 0.7f, true);
            check(edge == 0f ? tab.handle().left() == usable.left() : tab.handle().right() == usable.right(),
                    "rotation preserves compact tab's physical edge");
            check(tab.handle().width() == Math.min(24, usable.width()), "compact width clamps to inset space");
            OverlayGeometry.Layout restored = OverlayGeometry.layout(v[0], v[1], v[2], v[3], v[4], v[5],
                    8, 48, 48, 480, 560, 8, edge, 0.7f, false);
            OverlayGeometry.Layout original = OverlayGeometry.layout(v[0], v[1], v[2], v[3], v[4], v[5],
                    8, 48, 480, 560, 8, edge, 0.7f);
            check(restored.equals(original), "undocking restores original icon/panel geometry");
            inside(restored.handle(), restored.safe()); inside(restored.panel(), restored.safe());
            if (restored.safe().width() >= 48) check(restored.handle().width() == 48, "normal width restored");
            if (restored.safe().height() >= 48) check(restored.handle().height() == 48, "normal height restored");
        }
        // Density changes scale compact dimensions without reintroducing the old 8dp gap.
        for (int density : new int[]{1, 2, 3, 4}) {
            OverlayGeometry.Layout scaled = OverlayGeometry.layout(640 * density, 320 * density,
                    36 * density, 0, 0, 24 * density, 8 * density, 24 * density, 40 * density,
                    480 * density, 560 * density, 8 * density, edge, 0.7f, true);
            check(scaled.handle().width() == 24 * density && scaled.handle().height() == 40 * density,
                    "compact tab scales with density");
            check(edge == 0f ? scaled.handle().left() == 36 * density : scaled.handle().right() == 640 * density,
                    "density-scaled tab has no horizontal gap");
        }
    }
    private static void inside(OverlayGeometry.Rect inner, OverlayGeometry.Rect outer) {
        check(inner.width() >= 0 && inner.height() >= 0, "nonnegative size");
        check(inner.left() >= outer.left() && inner.top() >= outer.top()
                && inner.right() <= outer.right() && inner.bottom() <= outer.bottom(), "overflow: " + inner + " in " + outer);
    }
    private static void check(boolean condition, String message) { if (!condition) throw new AssertionError(message); }
}
