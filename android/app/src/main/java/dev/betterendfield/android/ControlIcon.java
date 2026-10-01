package dev.betterendfield.android;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.Path;
import android.view.View;

/** Small, resolution-independent line icons; no emoji/font-dependent symbols. */
final class ControlIcon extends View {
    static final int HUD = 0, CAMERA = 1, PAUSE = 2, EYE = 3, HANDLE = 4, MMD = 5;
    private final Paint paint = new Paint(Paint.ANTI_ALIAS_FLAG);
    private final int kind;
    private int color;
    ControlIcon(Context context, int kind, int color) {
        super(context);
        this.kind = kind;
        this.color = color;
        setImportantForAccessibility(IMPORTANT_FOR_ACCESSIBILITY_NO);
    }
    void tint(int color) { this.color = color; invalidate(); }
    @Override protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);
        canvas.save();
        float width = getWidth() - getPaddingLeft() - getPaddingRight();
        float height = getHeight() - getPaddingTop() - getPaddingBottom();
        float size = Math.max(0f, Math.min(width, height));
        canvas.translate(getPaddingLeft() + (width - size) / 2,
                getPaddingTop() + (height - size) / 2);
        canvas.scale(size / 24f, size / 24f);
        paint.setColor(color);
        paint.setStyle(Paint.Style.STROKE);
        paint.setStrokeWidth(1.6f);
        paint.setStrokeCap(Paint.Cap.ROUND);
        paint.setStrokeJoin(Paint.Join.ROUND);
        switch (kind) {
            case MMD:
                canvas.drawCircle(12, 5, 2.3f, paint);
                canvas.drawLine(12, 8, 12, 14, paint);
                canvas.drawLine(12, 10, 5, 7, paint);
                canvas.drawLine(12, 10, 19, 7, paint);
                canvas.drawLine(12, 14, 7, 21, paint);
                canvas.drawLine(12, 14, 17, 21, paint);
                break;
            case CAMERA:
                canvas.drawRoundRect(3, 7, 21, 20, 3, 3, paint);
                canvas.drawCircle(12, 13.5f, 3.2f, paint);
                canvas.drawLine(7, 7, 9, 4, paint);
                canvas.drawLine(9, 4, 15, 4, paint);
                canvas.drawLine(15, 4, 17, 7, paint);
                break;
            case PAUSE:
                canvas.drawRoundRect(6, 4, 9, 20, 1, 1, paint);
                canvas.drawRoundRect(15, 4, 18, 20, 1, 1, paint);
                break;
            case EYE:
                Path eye = new Path();
                eye.moveTo(2, 12); eye.quadTo(12, -1, 22, 12); eye.quadTo(12, 25, 2, 12);
                canvas.drawPath(eye, paint); canvas.drawCircle(12, 12, 3, paint);
                break;
            case HANDLE:
                Path mark = new Path();
                mark.moveTo(6, 4); mark.lineTo(18, 4); mark.lineTo(14, 10);
                mark.lineTo(19, 10); mark.lineTo(7, 21); mark.lineTo(10, 13);
                mark.lineTo(5, 13); mark.close(); canvas.drawPath(mark, paint);
                break;
            default:
                canvas.drawLine(3, 9, 3, 4, paint); canvas.drawLine(3, 4, 8, 4, paint);
                canvas.drawLine(16, 4, 21, 4, paint); canvas.drawLine(21, 4, 21, 9, paint);
                canvas.drawLine(3, 15, 3, 20, paint); canvas.drawLine(3, 20, 8, 20, paint);
                canvas.drawLine(16, 20, 21, 20, paint); canvas.drawLine(21, 20, 21, 15, paint);
                canvas.drawLine(8, 12, 16, 12, paint);
        }
        canvas.restore();
    }
}
