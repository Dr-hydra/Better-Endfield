package dev.betterendfield.android;

import android.app.Activity;
import android.app.Application;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.Typeface;
import android.graphics.drawable.GradientDrawable;
import android.os.Bundle;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewConfiguration;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.function.Supplier;

/**
 * The in-game control panel: a draggable handle that opens the actions which need
 * to be triggered while playing.
 *
 * Division of labour with the settings app: anything that needs a keypress on
 * desktop lives here, everything else lives on the settings screen. The buttons do
 * not talk to the modules directly - they press the same Windows virtual keys the
 * ported desktop modules already poll for (see {@link Hotkeys}), which is why the
 * desktop UI and camera code paths needed no Android-specific branch.
 *
 * Attached straight to the scoped game's Activity, so it needs no
 * SYSTEM_ALERT_WINDOW permission, no foreground service and no second process.
 */
final class GameOverlay {
    private static final int ACCENT = 0xFFFFC845;
    private static final int ACCENT_INK = 0xFF120E03;
    private static final int PANEL = 0xF00C1016;
    private static final int ROW = 0xFF1B222B;
    private static final int ROW_PRESSED = 0xFF2A3341;
    private static final int TEXT = 0xFFF4F6F8;
    private static final int TEXT_DIM = 0xFFA7B0BC;
    private static final int TEXT_MUTED = 0xFF75808E;
    private static final int BORDER = 0xFF2B333E;

    private final Activity activity;
    private final FrameLayout host;
    private final View handle;
    private final ScrollView panel;
    private final LinearLayout content;
    private final TextView footer;
    private final boolean preview;
    private final Supplier<OverlayFeatures> features;

    private boolean closed;
    private float xFraction = 0.02f;
    private float yFraction = 0.28f;
    private float downX;
    private float downY;
    private float startX;
    private float startY;
    private boolean dragged;
    private OverlayFeatures shown = OverlayFeatures.off();
    private boolean bridgeMissing;

    static void install(Application app, ClassLoader loader, Supplier<OverlayFeatures> features) {
        try {
            Class.forName("com.unity3d.player.UnityPlayer", false, loader);
        } catch (ClassNotFoundException notUnity) {
            return;
        }
        app.registerActivityLifecycleCallbacks(new Application.ActivityLifecycleCallbacks() {
            private final java.util.Map<Activity, GameOverlay> surfaces = new java.util.HashMap<>();

            @Override public void onActivityResumed(Activity activity) {
                OverlayFeatures current;
                try {
                    current = features.get();
                } catch (RuntimeException unavailable) {
                    current = OverlayFeatures.off();
                }
                GameOverlay surface = surfaces.get(activity);
                if (current.panel() && surface == null) {
                    try {
                        surface = new GameOverlay(activity, false, features);
                        surfaces.put(activity, surface);
                    } catch (RuntimeException error) {
                        android.util.Log.e("BetterEndfield.Overlay", "Unable to attach panel", error);
                    }
                }
                if (surface == null) return;
                // A setting changed while the game was in the background has to
                // reach the controls, not just the panel's visibility.
                surface.refresh();
                surface.host.setVisibility(
                        current.panel() && !surface.closed ? View.VISIBLE : View.GONE);
            }

            @Override public void onActivityPaused(Activity activity) {
                GameOverlay surface = surfaces.get(activity);
                if (surface == null) return;
                // Leaving a held movement key latched would keep the camera
                // drifting for as long as the game stays in the background.
                surface.releaseHeldKeys();
                surface.host.setVisibility(View.GONE);
            }

            @Override public void onActivityDestroyed(Activity activity) {
                GameOverlay surface = surfaces.remove(activity);
                if (surface != null) {
                    surface.releaseHeldKeys();
                    surface.remove();
                }
            }

            @Override public void onActivityCreated(Activity a, Bundle b) { }
            @Override public void onActivityStarted(Activity a) { }
            @Override public void onActivityStopped(Activity a) { }
            @Override public void onActivitySaveInstanceState(Activity a, Bundle b) { }
        });
    }

    GameOverlay(Activity activity, boolean preview) {
        this(activity, preview, () -> OverlayFeatures.read(FrameworkSettings.open(activity)));
    }

    private GameOverlay(Activity activity, boolean preview, Supplier<OverlayFeatures> features) {
        this.activity = activity;
        this.preview = preview;
        this.features = features;

        host = new FrameLayout(activity);
        host.setClipChildren(false);
        host.setImportantForAccessibility(View.IMPORTANT_FOR_ACCESSIBILITY_NO);
        // The host has no click listener of its own, so a touch anywhere except
        // the handle and the panel goes straight through to the game.
        activity.addContentView(host, new ViewGroup.LayoutParams(-1, -1));
        host.setOnApplyWindowInsetsListener((view, insets) -> {
            host.setPadding(insets.getSystemWindowInsetLeft(), insets.getSystemWindowInsetTop(),
                    insets.getSystemWindowInsetRight(), insets.getSystemWindowInsetBottom());
            host.post(this::layout);
            return insets;
        });

        handle = buildHandle();
        host.addView(handle, new FrameLayout.LayoutParams(dp(50), dp(50)));

        panel = new ScrollView(activity);
        panel.setFillViewport(false);
        panel.setBackground(surface(PANEL, 20, BORDER));
        panel.setElevation(dp(16));
        panel.setClipToOutline(true);
        content = new LinearLayout(activity);
        content.setOrientation(LinearLayout.VERTICAL);
        content.setPadding(dp(14), dp(14), dp(14), dp(14));
        panel.addView(content);

        footer = label("", 11, TEXT_MUTED);
        footer.setLineSpacing(dp(2), 1f);

        host.addView(panel, new FrameLayout.LayoutParams(dp(300), -2));
        panel.setVisibility(preview ? View.VISIBLE : View.GONE);
        refresh();

        handle.setOnTouchListener(this::drag);
        host.addOnLayoutChangeListener((v, l, t, r, b, ol, ot, or, ob) -> layout());
        host.requestApplyInsets();
        host.post(this::layout);
    }

    // ------------------------------------------------------------------ handle

    private View buildHandle() {
        TextView view = new TextView(activity);
        view.setText("BE");
        view.setTextSize(15);
        view.setTypeface(Typeface.create("sans-serif-medium", Typeface.BOLD));
        view.setTextColor(ACCENT);
        view.setGravity(Gravity.CENTER);
        view.setElevation(dp(10));
        GradientDrawable background = new GradientDrawable();
        background.setColor(0xE60B1016);
        background.setCornerRadius(dp(16));
        background.setStroke(dp(2), ACCENT);
        view.setBackground(background);
        view.setContentDescription("Better Endfield 控制面板：点击展开，拖动可移动");
        return view;
    }

    // ------------------------------------------------------------------- panel

    /** Rebuilds the panel body for the currently configured features. */
    private void refresh() {
        OverlayFeatures current;
        try {
            current = features.get();
        } catch (RuntimeException unavailable) {
            current = OverlayFeatures.off();
        }
        if (preview) {
            // The preview runs inside the settings app, where the panel switch is
            // what the user is about to turn on. Show the controls their other
            // choices selected rather than an empty panel.
            current = new OverlayFeatures(true, current.hideHud(), current.freeCamera(),
                    current.worldPause(), current.firstPerson());
        }
        if (content.getChildCount() > 0 && current.equals(shown)) return;
        shown = current;
        content.removeAllViews();

        content.addView(header());

        if (current.hideHud()) {
            group("界面");
            content.addView(action("隐藏 / 恢复 HUD", Hotkeys.HIDE_HUD_NAME,
                    Hotkeys.HIDE_HUD, "通过游戏自己的 UI 相机遮罩隐藏整个 HUD"));
        }

        if (current.freeCamera() || current.firstPerson()) {
            group("相机");
            if (current.freeCamera()) {
                content.addView(action("自由视角", Hotkeys.FREE_CAMERA_NAME,
                        Hotkeys.FREE_CAMERA, "脱离角色自由移动镜头"));
            }
            if (current.worldPause()) {
                content.addView(action("时间冻结", Hotkeys.WORLD_PAUSE_NAME,
                        Hotkeys.WORLD_PAUSE, "冻结游戏时间，镜头仍可移动"));
            }
            if (current.firstPerson()) {
                content.addView(action("第一人称", Hotkeys.FIRST_PERSON_NAME,
                        Hotkeys.FIRST_PERSON, "把镜头移到角色头部"));
            }
            if (current.freeCamera()) {
                content.addView(movementPad());
            }
        }

        if (!current.anyControl()) {
            content.addView(notice("还没有需要即时操作的功能。\n"
                    + "在「画面增强」页启用隐藏 HUD、自由视角或第一人称后，按钮会出现在这里。"));
        }

        content.addView(ghost(preview ? "结束预览" : "打开增强设置",
                view -> {
                    if (preview) {
                        remove();
                    } else {
                        openSettings();
                    }
                }), stacked(14));

        footer.setText(preview
                ? "预览模式：按钮不会发送指令。"
                : "按钮按下的是桌面端同一套热键，模块在游戏内自行响应。");
        LinearLayout.LayoutParams footerParams = stacked(10);
        if (footer.getParent() instanceof ViewGroup) {
            ((ViewGroup) footer.getParent()).removeView(footer);
        }
        content.addView(footer, footerParams);
        host.post(this::layout);
    }

    private View header() {
        LinearLayout header = new LinearLayout(activity);
        header.setOrientation(LinearLayout.HORIZONTAL);
        header.setGravity(Gravity.CENTER_VERTICAL);

        LinearLayout titles = new LinearLayout(activity);
        titles.setOrientation(LinearLayout.VERTICAL);
        TextView eyebrow = label("BETTER ENDFIELD", 10, ACCENT);
        eyebrow.setLetterSpacing(0.14f);
        eyebrow.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        titles.addView(eyebrow);
        TextView title = label(preview ? "悬浮窗预览" : "游戏内控制", 18, TEXT);
        title.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        titles.addView(title);
        header.addView(titles, new LinearLayout.LayoutParams(0, -2, 1f));

        TextView collapse = new TextView(activity);
        collapse.setText("收起");
        collapse.setTextSize(12);
        collapse.setTextColor(TEXT_DIM);
        collapse.setGravity(Gravity.CENTER);
        collapse.setMinimumWidth(dp(52));
        collapse.setMinimumHeight(dp(36));
        collapse.setBackground(surface(ROW, 12, BORDER));
        collapse.setContentDescription("收起控制面板");
        collapse.setOnClickListener(view -> {
            if (preview) remove(); else panel.setVisibility(View.GONE);
        });
        header.addView(collapse);
        return header;
    }

    private void group(String name) {
        TextView view = label(name, 11, TEXT_MUTED);
        view.setLetterSpacing(0.08f);
        view.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        LinearLayout.LayoutParams params = stacked(content.getChildCount() > 1 ? 16 : 14);
        params.leftMargin = dp(2);
        params.bottomMargin = dp(2);
        content.addView(view, params);
    }

    /**
     * A tap control. Sends a pulse rather than a press so that one tap is exactly
     * one rising edge, which is what the desktop modules' edge detection expects.
     */
    private View action(String title, String keyName, int virtualKey, String hint) {
        LinearLayout row = new LinearLayout(activity);
        row.setOrientation(LinearLayout.HORIZONTAL);
        row.setGravity(Gravity.CENTER_VERTICAL);
        row.setPadding(dp(13), dp(11), dp(11), dp(11));
        row.setBackground(pressable(ROW, ROW_PRESSED, 13));
        row.setMinimumHeight(dp(58));

        LinearLayout text = new LinearLayout(activity);
        text.setOrientation(LinearLayout.VERTICAL);
        TextView name = label(title, 15, TEXT);
        name.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        text.addView(name);
        text.addView(label(hint, 11, TEXT_DIM));
        row.addView(text, new LinearLayout.LayoutParams(0, -2, 1f));

        TextView key = label(keyName, 11, ACCENT);
        key.setTypeface(Typeface.MONOSPACE);
        key.setGravity(Gravity.CENTER);
        key.setMinimumWidth(dp(28));
        key.setPadding(dp(7), dp(3), dp(7), dp(3));
        key.setBackground(surface(0x1FFFC845, 7, Color.TRANSPARENT));
        LinearLayout.LayoutParams keyParams = new LinearLayout.LayoutParams(-2, -2);
        keyParams.leftMargin = dp(10);
        row.addView(key, keyParams);

        row.setContentDescription(title + "。" + hint);
        row.setOnClickListener(view -> pulse(virtualKey, title));
        LinearLayout.LayoutParams params = stacked(8);
        row.setLayoutParams(params);
        return row;
    }

    /**
     * Free-camera movement. These are held, not tapped: the desktop module reads
     * the arrow keys every 5 ms for as long as they are down.
     */
    private View movementPad() {
        LinearLayout pad = new LinearLayout(activity);
        pad.setOrientation(LinearLayout.VERTICAL);
        pad.setPadding(dp(10), dp(10), dp(10), dp(10));
        pad.setBackground(surface(0xFF141A22, 13, BORDER));

        pad.addView(label("移动（按住）", 11, TEXT_MUTED));

        LinearLayout plane = new LinearLayout(activity);
        plane.setOrientation(LinearLayout.HORIZONTAL);
        plane.addView(hold("←", Hotkeys.MOVE_LEFT, "左移"), padCell(0));
        plane.addView(hold("↑", Hotkeys.MOVE_FORWARD, "前进"), padCell(8));
        plane.addView(hold("↓", Hotkeys.MOVE_BACK, "后退"), padCell(8));
        plane.addView(hold("→", Hotkeys.MOVE_RIGHT, "右移"), padCell(8));
        pad.addView(plane, stacked(8));

        LinearLayout vertical = new LinearLayout(activity);
        vertical.setOrientation(LinearLayout.HORIZONTAL);
        vertical.addView(hold("升 ⤒", Hotkeys.MOVE_UP, "上升"), padCell(0));
        vertical.addView(hold("降 ⤓", Hotkeys.MOVE_DOWN, "下降"), padCell(8));
        pad.addView(vertical, stacked(8));

        LinearLayout.LayoutParams params = stacked(8);
        pad.setLayoutParams(params);
        return pad;
    }

    private LinearLayout.LayoutParams padCell(int leftMarginDp) {
        LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(0, dp(50), 1f);
        params.leftMargin = dp(leftMarginDp);
        return params;
    }

    private View hold(String glyph, int virtualKey, String description) {
        TextView view = new TextView(activity);
        view.setText(glyph);
        view.setTextSize(16);
        view.setTextColor(TEXT);
        view.setGravity(Gravity.CENTER);
        view.setBackground(pressable(ROW, ROW_PRESSED, 12));
        view.setContentDescription(description + "，按住生效");
        view.setOnTouchListener((v, event) -> {
            switch (event.getActionMasked()) {
                case MotionEvent.ACTION_DOWN:
                    v.setPressed(true);
                    sendKey(virtualKey, NativeCommandBridge.KEY_PRESS, description);
                    return true;
                case MotionEvent.ACTION_UP:
                case MotionEvent.ACTION_CANCEL:
                    v.setPressed(false);
                    sendKey(virtualKey, NativeCommandBridge.KEY_RELEASE, description);
                    return true;
                default:
                    return false;
            }
        });
        return view;
    }

    private View notice(String message) {
        TextView view = label(message, 12, TEXT_DIM);
        view.setLineSpacing(dp(3), 1f);
        view.setPadding(dp(13), dp(12), dp(13), dp(12));
        view.setBackground(surface(0xFF11161C, 13, BORDER));
        view.setLayoutParams(stacked(12));
        return view;
    }

    private View ghost(String title, View.OnClickListener listener) {
        TextView view = new TextView(activity);
        view.setText(title);
        view.setTextSize(14);
        view.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        view.setTextColor(ACCENT_INK);
        view.setGravity(Gravity.CENTER);
        view.setMinimumHeight(dp(48));
        view.setBackground(pressable(ACCENT, 0xFFE0A513, 14));
        view.setOnClickListener(listener);
        return view;
    }

    // ------------------------------------------------------------------ actions

    private void pulse(int virtualKey, String title) {
        sendKey(virtualKey, NativeCommandBridge.KEY_PULSE, title);
    }

    private void sendKey(int virtualKey, int action, String description) {
        if (preview) {
            if (action != NativeCommandBridge.KEY_RELEASE) {
                toast("预览模式：不会发送「" + description + "」");
            }
            return;
        }
        try {
            if (!NativeCommandBridge.key(virtualKey, action)) {
                toast("无法发送「" + description + "」");
            }
            bridgeMissing = false;
        } catch (UnsatisfiedLinkError | NoSuchMethodError unavailable) {
            // Before the first Unity frame the runtime is not loaded yet, and if a
            // module was never configured it never will be. Say so once.
            if (!bridgeMissing) {
                bridgeMissing = true;
                toast("增强运行时尚未载入，请稍后重试");
            }
        }
    }

    private void releaseHeldKeys() {
        if (preview) return;
        try {
            NativeCommandBridge.releaseKeys();
        } catch (UnsatisfiedLinkError | NoSuchMethodError ignored) {
            // Nothing is latched if the runtime was never loaded.
        }
    }

    private void openSettings() {
        Intent intent = new Intent()
                .setClassName(RuntimeBootstrap.MODULE_PACKAGE,
                        RuntimeBootstrap.MODULE_PACKAGE + ".MainActivity")
                .putExtra(MainActivity.EXTRA_PAGE, "enhancement")
                .addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
        try {
            activity.startActivity(intent);
        } catch (RuntimeException unavailable) {
            toast("无法打开增强设置");
        }
    }

    private void toast(String message) {
        Toast.makeText(activity, message, Toast.LENGTH_SHORT).show();
    }

    // ------------------------------------------------------------------- layout

    private void layout() {
        if (closed || host.getWidth() == 0) return;
        int left = host.getPaddingLeft() + dp(8);
        int top = host.getPaddingTop() + dp(8);
        int width = Math.max(1, host.getWidth() - left - host.getPaddingRight() - dp(8));
        int height = Math.max(1, host.getHeight() - top - host.getPaddingBottom() - dp(8));
        handle.setX(left + xFraction * Math.max(0, width - dp(50)));
        handle.setY(top + yFraction * Math.max(0, height - dp(50)));

        int panelWidth = Math.min(dp(320), width);
        panel.measure(View.MeasureSpec.makeMeasureSpec(panelWidth, View.MeasureSpec.EXACTLY),
                View.MeasureSpec.makeMeasureSpec(height, View.MeasureSpec.AT_MOST));
        int panelHeight = Math.min(height, panel.getMeasuredHeight());
        FrameLayout.LayoutParams params = (FrameLayout.LayoutParams) panel.getLayoutParams();
        if (params.width != panelWidth || params.height != panelHeight) {
            params.width = panelWidth;
            params.height = panelHeight;
            panel.setLayoutParams(params);
        }
        // Prefer opening to the right of the handle, and flip to its left when the
        // panel would run off the screen.
        float beside = handle.getX() + dp(58);
        if (beside + panelWidth > left + width) beside = handle.getX() - panelWidth - dp(8);
        panel.setX(Math.max(left, Math.min(beside, left + width - panelWidth)));
        panel.setY(Math.max(top, Math.min(handle.getY(), top + height - panelHeight)));
        handle.bringToFront();
    }

    private boolean drag(View view, MotionEvent event) {
        switch (event.getActionMasked()) {
            case MotionEvent.ACTION_DOWN:
                downX = event.getRawX();
                downY = event.getRawY();
                startX = handle.getX();
                startY = handle.getY();
                dragged = false;
                handle.setAlpha(0.75f);
                return true;
            case MotionEvent.ACTION_MOVE: {
                float dx = event.getRawX() - downX;
                float dy = event.getRawY() - downY;
                dragged |= Math.hypot(dx, dy) > ViewConfiguration.get(activity).getScaledTouchSlop();
                if (dragged) {
                    int left = host.getPaddingLeft() + dp(8);
                    int top = host.getPaddingTop() + dp(8);
                    int w = host.getWidth() - left - host.getPaddingRight() - dp(58);
                    int h = host.getHeight() - top - host.getPaddingBottom() - dp(58);
                    xFraction = clamp((startX + dx - left) / Math.max(1, w));
                    yFraction = clamp((startY + dy - top) / Math.max(1, h));
                    layout();
                }
                return true;
            }
            case MotionEvent.ACTION_UP:
                handle.setAlpha(1f);
                if (!dragged) togglePanel();
                return true;
            case MotionEvent.ACTION_CANCEL:
                handle.setAlpha(1f);
                return true;
            default:
                return false;
        }
    }

    private void togglePanel() {
        boolean opening = panel.getVisibility() != View.VISIBLE;
        if (opening) refresh();
        panel.setVisibility(opening ? View.VISIBLE : View.GONE);
        layout();
    }

    void remove() {
        closed = true;
        releaseHeldKeys();
        if (host.getParent() instanceof ViewGroup) {
            ((ViewGroup) host.getParent()).removeView(host);
        }
    }

    // -------------------------------------------------------------------- atoms

    private static float clamp(float value) {
        return Math.max(0f, Math.min(1f, value));
    }

    private TextView label(String text, int size, int color) {
        TextView view = new TextView(activity);
        view.setText(text);
        view.setTextSize(size);
        view.setTextColor(color);
        return view;
    }

    private LinearLayout.LayoutParams stacked(int topMarginDp) {
        LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(-1, -2);
        params.topMargin = dp(topMarginDp);
        return params;
    }

    private GradientDrawable surface(int color, int radius, int stroke) {
        GradientDrawable drawable = new GradientDrawable();
        drawable.setColor(color);
        drawable.setCornerRadius(dp(radius));
        if (stroke != Color.TRANSPARENT) drawable.setStroke(dp(1), stroke);
        return drawable;
    }

    private android.graphics.drawable.StateListDrawable pressable(
            int color, int pressedColor, int radius) {
        android.graphics.drawable.StateListDrawable states =
                new android.graphics.drawable.StateListDrawable();
        states.addState(new int[]{android.R.attr.state_pressed},
                surface(pressedColor, radius, ACCENT));
        states.addState(new int[]{}, surface(color, radius, BORDER));
        return states;
    }

    private int dp(int value) {
        return Math.round(value * activity.getResources().getDisplayMetrics().density);
    }
}
