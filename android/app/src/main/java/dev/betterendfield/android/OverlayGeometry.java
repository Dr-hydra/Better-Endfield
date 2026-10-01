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
        width = Math.max(0, width); height = Math.max(0, height);
        int left = bound(insetLeft, 0, width), top = bound(insetTop, 0, height);
        int right = bound(width - Math.max(0, insetRight), left, width);
        int bottom = bound(height - Math.max(0, insetBottom), top, height);
        int mx = Math.min(Math.max(0, margin), Math.max(0, (right - left - 1) / 2));
        int my = Math.min(Math.max(0, margin), Math.max(0, (bottom - top - 1) / 2));
        Rect safe = new Rect(left + mx, top + my, right - left - 2 * mx, bottom - top - 2 * my);
        int hw = Math.min(Math.max(0, handleSize), safe.width);
        int hh = Math.min(Math.max(0, handleSize), safe.height);
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

    private static int bound(int value, int min, int max) { return Math.max(min, Math.min(max, value)); }
}
