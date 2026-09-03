package dev.betterendfield.android;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.ComposeShader;
import android.graphics.LinearGradient;
import android.graphics.Paint;
import android.graphics.PorterDuff;
import android.graphics.RectF;
import android.graphics.Shader;
import android.graphics.SweepGradient;
import android.util.AttributeSet;
import android.view.MotionEvent;
import android.view.View;

/**
 * HSV colour wheel: a hue ring around a saturation/value square, mirroring the
 * desktop ring-shaped ColorPicker. Pure framework drawing, no dependencies.
 */
public final class ColorWheelView extends View {
    public interface OnColorChangedListener {
        /** {@code committed} is true once the finger lifts, false while dragging. */
        void onColorChanged(int rgb, boolean committed);
    }

    private static final int DRAG_NONE = 0;
    private static final int DRAG_RING = 1;
    private static final int DRAG_SQUARE = 2;

    private final Paint ringPaint = new Paint(Paint.ANTI_ALIAS_FLAG);
    private final Paint squarePaint = new Paint(Paint.ANTI_ALIAS_FLAG);
    private final Paint markerPaint = new Paint(Paint.ANTI_ALIAS_FLAG);
    private final Paint markerFillPaint = new Paint(Paint.ANTI_ALIAS_FLAG);
    private final RectF square = new RectF();
    private final float[] hsv = {45f, 0.84f, 1f};

    private float centerX;
    private float centerY;
    private float outerRadius;
    private float innerRadius;
    private int dragMode = DRAG_NONE;
    private int shaderHue = -1;
    private OnColorChangedListener listener;

    public ColorWheelView(Context context) {
        this(context, null);
    }

    public ColorWheelView(Context context, AttributeSet attrs) {
        super(context, attrs);
        ringPaint.setStyle(Paint.Style.STROKE);
        markerPaint.setStyle(Paint.Style.STROKE);
        markerPaint.setColor(Color.WHITE);
        markerFillPaint.setStyle(Paint.Style.FILL);
    }

    public void setOnColorChangedListener(OnColorChangedListener listener) {
        this.listener = listener;
    }

    public int getColor() {
        return Color.HSVToColor(hsv) & 0xFFFFFF;
    }

    /** Sets the shown colour without notifying the listener. */
    public void setColor(int rgb) {
        float[] next = new float[3];
        Color.colorToHSV(rgb | 0xFF000000, next);
        // Keep the hue marker in place for grey/white: hue is undefined there.
        if (next[1] < 0.001f) {
            next[0] = hsv[0];
        }
        hsv[0] = next[0];
        hsv[1] = next[1];
        hsv[2] = next[2];
        invalidate();
    }

    @Override
    protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec) {
        int width = MeasureSpec.getSize(widthMeasureSpec);
        int height = MeasureSpec.getMode(heightMeasureSpec) == MeasureSpec.UNSPECIFIED
                ? width : MeasureSpec.getSize(heightMeasureSpec);
        int size = Math.min(width, height);
        setMeasuredDimension(size, size);
    }

    @Override
    protected void onSizeChanged(int w, int h, int oldw, int oldh) {
        float size = Math.min(w, h);
        centerX = w / 2f;
        centerY = h / 2f;
        float ringWidth = size * 0.11f;
        outerRadius = size / 2f - dp(2);
        innerRadius = outerRadius - ringWidth;
        ringPaint.setStrokeWidth(ringWidth);
        markerPaint.setStrokeWidth(dp(2));
        // Hue ring: 0° at 3 o'clock going clockwise, like SweepGradient.
        int[] hues = new int[13];
        for (int i = 0; i < hues.length; ++i) {
            hues[i] = Color.HSVToColor(new float[] {i * 30f % 360f, 1f, 1f});
        }
        ringPaint.setShader(new SweepGradient(centerX, centerY, hues, null));
        float half = (innerRadius - dp(10)) / (float) Math.sqrt(2);
        square.set(centerX - half, centerY - half, centerX + half, centerY + half);
        shaderHue = -1;
    }

    @Override
    protected void onDraw(Canvas canvas) {
        float ringCenter = (outerRadius + innerRadius) / 2f;
        canvas.drawCircle(centerX, centerY, ringCenter, ringPaint);

        int hueInt = Math.round(hsv[0]);
        if (hueInt != shaderHue) {
            shaderHue = hueInt;
            int pure = Color.HSVToColor(new float[] {hsv[0], 1f, 1f});
            Shader saturation = new LinearGradient(square.left, 0, square.right, 0,
                    Color.WHITE, pure, Shader.TileMode.CLAMP);
            Shader value = new LinearGradient(0, square.top, 0, square.bottom,
                    Color.TRANSPARENT, Color.BLACK, Shader.TileMode.CLAMP);
            squarePaint.setShader(new ComposeShader(saturation, value, PorterDuff.Mode.SRC_OVER));
        }
        canvas.drawRoundRect(square, dp(4), dp(4), squarePaint);

        // Hue marker on the ring.
        double angle = Math.toRadians(hsv[0]);
        float hx = centerX + (float) Math.cos(angle) * ringCenter;
        float hy = centerY + (float) Math.sin(angle) * ringCenter;
        markerFillPaint.setColor(Color.HSVToColor(new float[] {hsv[0], 1f, 1f}));
        canvas.drawCircle(hx, hy, ringPaint.getStrokeWidth() * 0.42f, markerFillPaint);
        canvas.drawCircle(hx, hy, ringPaint.getStrokeWidth() * 0.42f, markerPaint);

        // Saturation/value marker in the square.
        float sx = square.left + hsv[1] * square.width();
        float sy = square.top + (1f - hsv[2]) * square.height();
        markerFillPaint.setColor(Color.HSVToColor(hsv));
        canvas.drawCircle(sx, sy, dp(7), markerFillPaint);
        canvas.drawCircle(sx, sy, dp(7), markerPaint);
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        float x = event.getX();
        float y = event.getY();
        switch (event.getActionMasked()) {
            case MotionEvent.ACTION_DOWN: {
                float dx = x - centerX;
                float dy = y - centerY;
                float distance = (float) Math.hypot(dx, dy);
                if (square.contains(x, y)) {
                    dragMode = DRAG_SQUARE;
                } else if (distance <= outerRadius + dp(8) && distance >= innerRadius - dp(8)) {
                    dragMode = DRAG_RING;
                } else {
                    return false;
                }
                getParent().requestDisallowInterceptTouchEvent(true);
                update(x, y, false);
                return true;
            }
            case MotionEvent.ACTION_MOVE:
                if (dragMode == DRAG_NONE) return false;
                update(x, y, false);
                return true;
            case MotionEvent.ACTION_UP:
                if (dragMode == DRAG_NONE) return false;
                update(x, y, true);
                dragMode = DRAG_NONE;
                return true;
            case MotionEvent.ACTION_CANCEL:
                dragMode = DRAG_NONE;
                return true;
            default:
                return super.onTouchEvent(event);
        }
    }

    private void update(float x, float y, boolean committed) {
        if (dragMode == DRAG_RING) {
            float degrees = (float) Math.toDegrees(Math.atan2(y - centerY, x - centerX));
            hsv[0] = (degrees + 360f) % 360f;
        } else if (dragMode == DRAG_SQUARE) {
            hsv[1] = clamp((x - square.left) / square.width());
            hsv[2] = clamp(1f - (y - square.top) / square.height());
        }
        invalidate();
        if (listener != null) {
            listener.onColorChanged(getColor(), committed);
        }
    }

    private static float clamp(float value) {
        return Math.max(0f, Math.min(1f, value));
    }

    private float dp(float value) {
        return value * getResources().getDisplayMetrics().density;
    }
}
