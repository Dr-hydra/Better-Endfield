package dev.betterendfield.android;

/** Pixel geometry only: usable even before layout, and independently testable. */
final class OverlayGeometry {
    record Rect(int left, int top, int width, int height) {
        int right() { return left + width; }
        int bottom() { return top + height; }
    }
    record Layout(Rect safe, Rect handle, Rect panel) {}

    private OverlayGeometry() {}

    static Layout layout(int width, int height, int insetLeft, int insetTop,
            int insetRight, int insetBottom, int margin, int handleSize,
            int panelWidth, int panelHeight, int gap, float x, float y) {
        return layout(width, height, insetLeft, insetTop, insetRight, insetBottom, margin,
                handleSize, handleSize, panelWidth, panelHeight, gap, x, y, false);
    }

    static Rect usableArea(int width, int height, int insetLeft, int insetTop,
            int insetRight, int insetBottom) {
        width = Math.max(0, width); height = Math.max(0, height);
        int left = bound(insetLeft, 0, width), top = bound(insetTop, 0, height);
        int right = bound(width - Math.max(0, insetRight), left, width);
        int bottom = bound(height - Math.max(0, insetBottom), top, height);
        return new Rect(left, top, right - left, bottom - top);
    }

    static Layout layout(int width, int height, int insetLeft, int insetTop,
            int insetRight, int insetBottom, int margin, int handleWidth, int handleHeight,
            int panelWidth, int panelHeight, int gap, float x, float y, boolean docked) {
        Rect usable = usableArea(width, height, insetLeft, insetTop, insetRight, insetBottom);
        // A docked tab touches the actual inset edge; only its vertical margin remains.
        int mx = docked ? 0 : Math.min(Math.max(0, margin), Math.max(0, (usable.width - 1) / 2));
        int my = Math.min(Math.max(0, margin), Math.max(0, (usable.height - 1) / 2));
        Rect safe = new Rect(usable.left + mx, usable.top + my, usable.width - 2 * mx, usable.height - 2 * my);
        int hw = Math.min(Math.max(0, handleWidth), safe.width);
        int hh = Math.min(Math.max(0, handleHeight), safe.height);
        int hx = safe.left + Math.round(fraction(x) * (safe.width - hw));
        int hy = safe.top + Math.round(fraction(y) * (safe.height - hh));
        int pw = Math.min(Math.max(0, panelWidth), safe.width);
        int ph = Math.min(Math.max(0, panelHeight), safe.height);
        int beside = hx + hw + Math.max(0, gap);
        if (beside + pw > safe.right()) beside = hx - pw - Math.max(0, gap);
        Rect panel = new Rect(bound(beside, safe.left, safe.right() - pw),
                bound(hy, safe.top, safe.bottom() - ph), pw, ph);
        return new Layout(safe, new Rect(hx, hy, hw, hh), panel);
    }

    static float fraction(float value) {
        return Float.isFinite(value) ? Math.max(0f, Math.min(1f, value)) : 0f;
    }

    static float normalized(float coordinate, int origin, int available, int size) {
        return available <= size ? 0f : fraction((coordinate - origin) / (available - size));
    }

    static float nearestHorizontalEdge(float x) {
        return fraction(x) < 0.5f ? 0f : 1f;
    }

    private static int bound(int value, int min, int max) { return Math.max(min, Math.min(max, value)); }
}
