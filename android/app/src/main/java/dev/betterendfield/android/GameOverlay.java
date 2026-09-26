package dev.betterendfield.android;

import android.app.Activity;
import android.app.Application;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.Typeface;
import android.graphics.drawable.GradientDrawable;
import android.os.Build;
import android.os.Bundle;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewConfiguration;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.Space;
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
    private static final int CYAN = 0xFF73D5D2;
    private static final int PANEL = 0xF50C141C;
    private static final int PANEL_TOP = 0xFF14212B;
    private static final int ROW = 0xFF17232D;
    private static final int ROW_PRESSED = 0xFF263746;
    private static final int TEXT = 0xFFF4F6F8;
    private static final int TEXT_DIM = 0xFFA7B0BC;
    private static final int TEXT_MUTED = 0xFF75808E;
    private static final int BORDER = 0xFF2B333E;
    private static volatile float lastXFraction = 0.02f;
    private static volatile float lastYFraction = 0.28f;

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

    static void install(Application app, Supplier<OverlayFeatures> features) {
        // Do not probe UnityPlayer during Application.attach().  On some
        // Android builds the class is loaded lazily after attach; returning here
        // would permanently disable the overlay for the whole process.  The
        // callback is already installed only in the scoped target process, so it
        // is safe to defer all view work until an Activity is resumed.
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
        this.xFraction = lastXFraction;
        this.yFraction = lastYFraction;

        host = new FrameLayout(activity);
        host.setClipChildren(false);
        host.setImportantForAccessibility(View.IMPORTANT_FOR_ACCESSIBILITY_NO);
        // The host has no click listener of its own, so a touch anywhere except
        // the handle and the panel goes straight through to the game.
        activity.addContentView(host, new ViewGroup.LayoutParams(-1, -1));
        host.setOnApplyWindowInsetsListener((view, insets) -> {
            if (Build.VERSION.SDK_INT >= 30) {
                android.graphics.Insets bars = insets.getInsets(
                        android.view.WindowInsets.Type.systemBars()
                                | android.view.WindowInsets.Type.displayCutout());
                host.setPadding(bars.left, bars.top, bars.right, bars.bottom);
            } else {
                host.setPadding(insets.getSystemWindowInsetLeft(), insets.getSystemWindowInsetTop(),
                        insets.getSystemWindowInsetRight(), insets.getSystemWindowInsetBottom());
            }
            host.post(this::layout);
            return insets;
        });

        handle = buildHandle();
        host.addView(handle, new FrameLayout.LayoutParams(dp(50), dp(50)));

        panel = new ScrollView(activity);
        panel.setFillViewport(false);
        panel.setVerticalScrollBarEnabled(false);
        panel.setOverScrollMode(View.OVER_SCROLL_NEVER);
        panel.setBackground(surface(PANEL, 24, 0x663C4B59));
        panel.setElevation(dp(16));
        panel.setClipToOutline(true);
        content = new LinearLayout(activity);
        content.setOrientation(LinearLayout.VERTICAL);
        content.setPadding(dp(15), dp(12), dp(15), dp(17));
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
        FrameLayout view = new FrameLayout(activity);
        view.setElevation(dp(12));
        GradientDrawable background = new GradientDrawable();
        background.setColor(0xF20D1821);
        background.setCornerRadius(dp(18));
        background.setStroke(dp(1), 0xCCFFC845);
        view.setBackground(background);
        TextView mark = label("BE", 14, ACCENT);
        mark.setTypeface(Typeface.create("sans-serif-medium", Typeface.BOLD));
        mark.setGravity(Gravity.CENTER);
        view.addView(mark, new FrameLayout.LayoutParams(-1, -1));
        View status = new View(activity);
        status.setBackground(surface(CYAN, 8, Color.TRANSPARENT));
        FrameLayout.LayoutParams statusParams = new FrameLayout.LayoutParams(dp(8), dp(8),
                Gravity.TOP | Gravity.RIGHT);
        statusParams.setMargins(0, dp(5), dp(5), 0);
        view.addView(status, statusParams);
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

        content.addView(header(current));

        java.util.ArrayList<View> quickActions = new java.util.ArrayList<>();
        if (current.hideHud()) {
            quickActions.add(action("隐藏 / 恢复 HUD", Hotkeys.HIDE_HUD_NAME,
                    Hotkeys.HIDE_HUD, "切换游戏界面"));
        }
        if (current.freeCamera()) {
            quickActions.add(action("自由视角", Hotkeys.FREE_CAMERA_NAME,
                    Hotkeys.FREE_CAMERA, "进入 / 退出镜头"));
        }
        if (current.worldPause()) {
            quickActions.add(action("时间冻结", Hotkeys.WORLD_PAUSE_NAME,
                    Hotkeys.WORLD_PAUSE, "冻结 / 恢复时间"));
        }
        if (current.firstPerson()) {
            quickActions.add(action("第一人称", Hotkeys.FIRST_PERSON_NAME,
                    Hotkeys.FIRST_PERSON, "进入 / 退出视角"));
        }
        if (!quickActions.isEmpty()) {
            group("触控热键");
            LinearLayout grid = new LinearLayout(activity);
            grid.setOrientation(LinearLayout.VERTICAL);
            for (int index = 0; index < quickActions.size(); index += 2) {
                LinearLayout row = new LinearLayout(activity);
                row.setOrientation(LinearLayout.HORIZONTAL);
                row.addView(quickActions.get(index), gridCardParams(0));
                if (index + 1 < quickActions.size()) {
                    row.addView(quickActions.get(index + 1), gridCardParams(7));
                } else {
                    row.addView(new Space(activity), gridCardParams(7));
                }
                grid.addView(row, stacked(index == 0 ? 8 : 7));
            }
            content.addView(grid);
        }

        if (current.freeCamera()) {
            group("自由视角移动");
            content.addView(movementPad());
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

    private View header(OverlayFeatures current) {
        LinearLayout block = new LinearLayout(activity);
        block.setOrientation(LinearLayout.VERTICAL);
        View accent = new View(activity);
        accent.setBackground(surface(ACCENT, 3, Color.TRANSPARENT));
        block.addView(accent, new LinearLayout.LayoutParams(-1, dp(3)));

        LinearLayout header = new LinearLayout(activity);
        header.setOrientation(LinearLayout.HORIZONTAL);
        header.setGravity(Gravity.CENTER_VERTICAL);
        header.setPadding(dp(2), dp(11), dp(1), dp(2));

        LinearLayout titles = new LinearLayout(activity);
        titles.setOrientation(LinearLayout.VERTICAL);
        TextView eyebrow = label("BETTER ENDFIELD", 10, ACCENT);
        eyebrow.setLetterSpacing(0.14f);
        eyebrow.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        titles.addView(eyebrow);
        TextView title = label(preview ? "悬浮窗预览" : "游戏内控制", 18, TEXT);
        title.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        titles.addView(title);
        titles.addView(label(preview ? "只展示布局，不会发送按键" : "点按发送热键 · 按住保持按键",
                10, TEXT_MUTED));
        header.addView(titles, new LinearLayout.LayoutParams(0, -2, 1f));

        TextView badge = label(preview ? "预览" : current.anyControl()
                ? (currentCount(current) + " 项已启用") : "待配置", 10,
                preview ? CYAN : TEXT_DIM);
        badge.setGravity(Gravity.CENTER);
        badge.setPadding(dp(8), dp(5), dp(8), dp(5));
        badge.setBackground(surface(preview ? 0x2436D9D4 : 0x1FFFFFFF, 10, Color.TRANSPARENT));
        LinearLayout.LayoutParams badgeParams = new LinearLayout.LayoutParams(-2, -2);
        badgeParams.rightMargin = dp(7);
        header.addView(badge, badgeParams);

        TextView collapse = label("×", 22, TEXT_DIM);
        collapse.setGravity(Gravity.CENTER);
        collapse.setMinimumWidth(dp(38));
        collapse.setMinimumHeight(dp(38));
        collapse.setBackground(pressable(ROW, ROW_PRESSED, 12));
        collapse.setContentDescription("收起控制面板");
        collapse.setOnClickListener(view -> {
            if (preview) remove(); else panel.setVisibility(View.GONE);
        });
        header.addView(collapse);
        block.addView(header, new LinearLayout.LayoutParams(-1, -2));
        return block;
    }

    private int currentCount(OverlayFeatures current) {
        int count = 0;
        if (current.hideHud()) count++;
        if (current.freeCamera()) count++;
        if (current.worldPause()) count++;
        if (current.firstPerson()) count++;
        return count;
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
        row.setOrientation(LinearLayout.VERTICAL);
        row.setGravity(Gravity.CENTER_VERTICAL);
        row.setPadding(dp(10), dp(10), dp(10), dp(10));
        row.setBackground(pressable(ROW, ROW_PRESSED, 13));
        row.setMinimumHeight(dp(94));

        LinearLayout top = new LinearLayout(activity);
        top.setOrientation(LinearLayout.HORIZONTAL);
        top.setGravity(Gravity.CENTER_VERTICAL);
        TextView icon = label(actionGlyph(title), 17, ACCENT);
        icon.setGravity(Gravity.CENTER);
        icon.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        icon.setBackground(surface(0x1FFFC845, 10, Color.TRANSPARENT));
        top.addView(icon, new LinearLayout.LayoutParams(dp(34), dp(34)));

        TextView name = label(title, 14, TEXT);
        name.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        name.setMaxLines(2);
        LinearLayout.LayoutParams nameParams = new LinearLayout.LayoutParams(0, -2, 1f);
        nameParams.leftMargin = dp(7);
        top.addView(name, nameParams);

        TextView key = label(keyName, 10, ACCENT);
        key.setTypeface(Typeface.MONOSPACE);
        key.setGravity(Gravity.CENTER);
        key.setMinimumWidth(dp(34));
        key.setPadding(dp(8), dp(5), dp(8), dp(5));
        key.setBackground(surface(0x1FFFC845, 8, 0x55FFC845));
        LinearLayout.LayoutParams keyParams = new LinearLayout.LayoutParams(-2, -2);
        keyParams.leftMargin = dp(5);
        top.addView(key, keyParams);
        row.addView(top, new LinearLayout.LayoutParams(-1, -2));

        TextView summary = label(hint, 10, TEXT_DIM);
        summary.setMaxLines(2);
        LinearLayout.LayoutParams summaryParams = new LinearLayout.LayoutParams(-1, -2);
        summaryParams.topMargin = dp(7);
        row.addView(summary, summaryParams);

        row.setContentDescription(title + "，对应 " + keyName + "。点按触发。");
        row.setOnClickListener(view -> pulse(virtualKey, title));
        return row;
    }

    private LinearLayout.LayoutParams gridCardParams(int leftMarginDp) {
        LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(0, -2, 1f);
        params.leftMargin = dp(leftMarginDp);
        return params;
    }

    private String actionGlyph(String title) {
        if (title.contains("HUD")) return "◌";
        if (title.contains("自由")) return "✦";
        if (title.contains("冻结")) return "Ⅱ";
        if (title.contains("第一")) return "◎";
        return "•";
    }

    /**
     * Free-camera movement. These are held, not tapped: the desktop module reads
     * the arrow keys every 5 ms for as long as they are down.
     */
    private View movementPad() {
        LinearLayout pad = new LinearLayout(activity);
        pad.setOrientation(LinearLayout.VERTICAL);
        pad.setPadding(dp(11), dp(11), dp(11), dp(11));
        pad.setBackground(surface(PANEL_TOP, 16, 0x553C4B59));

        LinearLayout padHeader = new LinearLayout(activity);
        padHeader.setGravity(Gravity.CENTER_VERTICAL);
        TextView padTitle = label("移动", 12, TEXT);
        padTitle.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        padHeader.addView(padTitle, new LinearLayout.LayoutParams(0, -2, 1f));
        padHeader.addView(label("按住", 10, TEXT_MUTED));
        pad.addView(padHeader);

        LinearLayout plane = new LinearLayout(activity);
        plane.setOrientation(LinearLayout.VERTICAL);
        plane.addView(movementRow(emptyCell(), hold("↑", Hotkeys.MOVE_FORWARD, "前进"),
                emptyCell()), stacked(8));
        plane.addView(movementRow(hold("←", Hotkeys.MOVE_LEFT, "左移"),
                hold("↓", Hotkeys.MOVE_BACK, "后退"),
                hold("→", Hotkeys.MOVE_RIGHT, "右移")), stacked(6));
        pad.addView(plane);

        LinearLayout vertical = new LinearLayout(activity);
        vertical.setOrientation(LinearLayout.HORIZONTAL);
        vertical.addView(hold("升", Hotkeys.MOVE_UP, "上升"), padCell(0));
        vertical.addView(hold("降", Hotkeys.MOVE_DOWN, "下降"), padCell(8));
        pad.addView(vertical, stacked(9));

        LinearLayout.LayoutParams params = stacked(8);
        pad.setLayoutParams(params);
        return pad;
    }

    private LinearLayout movementRow(View left, View center, View right) {
        LinearLayout row = new LinearLayout(activity);
        row.setOrientation(LinearLayout.HORIZONTAL);
        row.setGravity(Gravity.CENTER);
        row.addView(left, padCell(0));
        row.addView(center, padCell(7));
        row.addView(right, padCell(7));
        return row;
    }

    private View emptyCell() {
        return new Space(activity);
    }

    private LinearLayout.LayoutParams padCell(int leftMarginDp) {
        LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(0, dp(50), 1f);
        params.leftMargin = dp(leftMarginDp);
        return params;
    }

    private View hold(String glyph, int virtualKey, String description) {
        TextView view = new TextView(activity);
        view.setText(glyph);
        view.setTextSize(17);
        view.setTextColor(TEXT);
        view.setGravity(Gravity.CENTER);
        view.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        view.setBackground(pressable(ROW, ROW_PRESSED, 14));
        view.setMinimumHeight(dp(50));
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

        // The panel is intentionally wider than a settings card: two large
        // touch targets make the desktop hotkeys usable without precision taps.
        int panelWidth = Math.min(dp(360), width);
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
                    lastXFraction = xFraction;
                    lastYFraction = yFraction;
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
        if (opening) {
            refresh();
            panel.setVisibility(View.VISIBLE);
            panel.setAlpha(0f);
            panel.setScaleX(0.96f);
            panel.setScaleY(0.96f);
            layout();
            panel.animate().alpha(1f).scaleX(1f).scaleY(1f).setDuration(150).start();
        } else {
            panel.animate().alpha(0f).scaleX(0.96f).scaleY(0.96f).setDuration(110)
                    .withEndAction(() -> {
                        panel.setVisibility(View.GONE);
                        panel.setAlpha(1f);
                        panel.setScaleX(1f);
                        panel.setScaleY(1f);
                    }).start();
        }
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
        view.setIncludeFontPadding(false);
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
