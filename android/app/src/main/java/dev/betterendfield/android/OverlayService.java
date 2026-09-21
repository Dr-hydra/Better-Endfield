package dev.betterendfield.android;

import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.ActivityManager;
import android.app.Service;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.PixelFormat;
import android.graphics.drawable.GradientDrawable;
import android.os.Build;
import android.os.IBinder;
import android.provider.Settings;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.View;
import android.view.WindowManager;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.os.Handler;
import android.os.Looper;

/** Small overlay control surface; complex settings remain in MainActivity. */
public final class OverlayService extends Service {
    private static final String CHANNEL = "betterendfield.overlay";
    private WindowManager windowManager;
    private WindowManager.LayoutParams params;
    private LinearLayout panel;
    private TextView handle;
    private TextView commandStatus;
    private boolean expanded;
    private float downX, downY;
    private int startX, startY;
    private final Handler monitor = new Handler(Looper.getMainLooper());
    private boolean attached;

    @Override public void onCreate() {
        super.onCreate();
        if (!Settings.canDrawOverlays(this)) { stopSelf(); return; }
        createNotificationChannel();
        startForeground(0xBEF1, notification());
        windowManager = (WindowManager) getSystemService(WINDOW_SERVICE);
        int type = Build.VERSION.SDK_INT >= 26
                ? WindowManager.LayoutParams.TYPE_APPLICATION_OVERLAY
                : WindowManager.LayoutParams.TYPE_PHONE;
        params = new WindowManager.LayoutParams(dp(54), dp(54), type,
                WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE
                        | WindowManager.LayoutParams.FLAG_LAYOUT_NO_LIMITS,
                PixelFormat.TRANSLUCENT);
        params.gravity = Gravity.TOP | Gravity.START;
        params.x = dp(12); params.y = dp(160);
        handle = text("BE", 13);
        handle.setGravity(Gravity.CENTER);
        handle.setOnClickListener(view -> toggle());
        handle.setOnTouchListener(this::drag);
        monitor.post(checkGameVisibility);
    }

    private final Runnable checkGameVisibility = new Runnable() {
        @Override public void run() {
            boolean gameForeground = false;
            ActivityManager manager = (ActivityManager) getSystemService(ACTIVITY_SERVICE);
            if (manager != null) for (ActivityManager.RunningAppProcessInfo process : manager.getRunningAppProcesses()) {
                if ("com.hypergryph.endfield".equals(process.processName)
                        && process.importance == ActivityManager.RunningAppProcessInfo.IMPORTANCE_FOREGROUND) {
                    gameForeground = true; break;
                }
            }
            if (gameForeground && !attached) { windowManager.addView(handle, params); attached = true; }
            if (!gameForeground && attached) { if (panel != null) { windowManager.removeViewImmediate(panel); panel = null; expanded = false; } windowManager.removeViewImmediate(handle); attached = false; }
            monitor.postDelayed(this, 1000);
        }
    };

    private void toggle() {
        if (!attached) return;
        if (expanded) { windowManager.removeView(panel); panel = null; expanded = false; return; }
        panel = new LinearLayout(this);
        panel.setOrientation(LinearLayout.VERTICAL);
        panel.setPadding(dp(10), dp(8), dp(10), dp(8));
        panel.setBackground(background(0xEE15181D, dp(12)));
        commandStatus = text("命令状态：等待", 12);
        commandStatus.setPadding(dp(10), dp(6), dp(10), dp(6));
        panel.addView(commandStatus);
        panel.addView(action("模型开/关", "model_toggle", "toggle"));
        panel.addView(action("隐藏悬浮窗", "overlay_hide", "1"));
        WindowManager.LayoutParams panelParams = new WindowManager.LayoutParams(dp(178),
                WindowManager.LayoutParams.WRAP_CONTENT, params.type, params.flags, params.format);
        panelParams.gravity = params.gravity; panelParams.x = params.x; panelParams.y = params.y + dp(58);
        windowManager.addView(panel, panelParams);
        expanded = true;
        refreshCommandStatus.run();
    }

    private final Runnable refreshCommandStatus = new Runnable() {
        @Override public void run() {
            if (commandStatus != null) {
                String raw = ModuleCommandRouter.readStatus();
                String[] lines = raw.split("\\n");
                String state = lines.length > 2 ? lines[2] : "等待";
                commandStatus.setText("命令状态：" + state);
            }
            if (expanded) monitor.postDelayed(this, 500);
        }
    };

    private TextView action(String label, String command, String value) {
        TextView view = text(label, 14);
        view.setPadding(dp(10), dp(8), dp(10), dp(8));
        view.setOnClickListener(button -> {
            ModuleCommandRouter.issue(this, command, value);
            if ("overlay_hide".equals(command)) stopSelf();
        });
        return view;
    }

    private boolean drag(View view, MotionEvent event) {
        switch (event.getActionMasked()) {
            case MotionEvent.ACTION_DOWN -> { downX = event.getRawX(); downY = event.getRawY(); startX = params.x; startY = params.y; return true; }
            case MotionEvent.ACTION_MOVE -> { params.x = startX + (int) (event.getRawX() - downX); params.y = startY + (int) (event.getRawY() - downY); windowManager.updateViewLayout(handle, params); return true; }
            case MotionEvent.ACTION_UP -> { if (Math.abs(event.getRawX() - downX) < dp(8) && Math.abs(event.getRawY() - downY) < dp(8)) toggle(); return true; }
            default -> { return false; }
        }
    }

    private TextView text(String value, int size) {
        TextView result = new TextView(this); result.setText(value); result.setTextSize(size);
        result.setTextColor(Color.WHITE); result.setBackground(background(0xEEFFC928, dp(27))); return result;
    }
    private GradientDrawable background(int color, float radius) { GradientDrawable d = new GradientDrawable(); d.setColor(color); d.setCornerRadius(radius); return d; }
    private int dp(int value) { return Math.round(value * getResources().getDisplayMetrics().density); }
    private Notification notification() { return new Notification.Builder(this, CHANNEL).setContentTitle("Better Endfield").setContentText("悬浮控制已启用").setSmallIcon(android.R.drawable.ic_menu_manage).setOngoing(true).build(); }
    private void createNotificationChannel() { if (Build.VERSION.SDK_INT >= 26) ((NotificationManager) getSystemService(NOTIFICATION_SERVICE)).createNotificationChannel(new NotificationChannel(CHANNEL, "Better Endfield 悬浮控制", NotificationManager.IMPORTANCE_LOW)); }
    @Override public void onDestroy() { monitor.removeCallbacksAndMessages(null); if (windowManager != null) { if (panel != null) windowManager.removeViewImmediate(panel); if (attached && handle != null) windowManager.removeViewImmediate(handle); } commandStatus = null; super.onDestroy(); }
    @Override public IBinder onBind(Intent intent) { return null; }
}
