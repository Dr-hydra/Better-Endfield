package dev.betterendfield.android;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Application;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.Typeface;
import android.graphics.drawable.GradientDrawable;
import android.graphics.drawable.StateListDrawable;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
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

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.function.Supplier;

/** In-Activity hotkey deck, not a second settings screen or overlay service. */
final class GameOverlay {
    private static final int INK = 0xFFF2F2EF, MUTED = 0xFF9DA6AC, GOLD = 0xFFE6CD82;
    private static final int SURFACE = 0xF2161B20, TILE = 0xFF242B31, EDGE = 0xFF3B434A;
    private static float savedX = 0.02f, savedY = 0.32f;
    private final Activity activity;
    private final boolean preview;
    private final Supplier<OverlayFeatures> features;
    private final Handler main = new Handler(Looper.getMainLooper());
    private final FrameLayout host;
    private final View handle;
    private final ScrollView panel;
    private final LinearLayout body;
    private final TextView status;
    private final ArrayList<Tile> tiles = new ArrayList<>();
    private final ArrayList<View> heldControls = new ArrayList<>();
    private RuntimeSnapshot snapshot = RuntimeSnapshot.offline();
    private OverlayFeatures shown = OverlayFeatures.off();
    private boolean resumed, closed, expanded, details, bridgeError, movementAllowed;
    private float x = savedX, y = savedY;
    private final Runnable pulse = new Runnable() {
        @Override public void run() {
            if (!resumed || closed) return;
            reconcileHost();
            refreshState();
            main.postDelayed(this, 500);
        }
    };

    static void install(Application app, ClassLoader ignored, Supplier<OverlayFeatures> features) {
        // Don't gate Activity lifecycle registration on Unity's lazy class loading.
        app.registerActivityLifecycleCallbacks(new Application.ActivityLifecycleCallbacks() {
            final Map<Activity, GameOverlay> surfaces = new HashMap<>();
            @Override public void onActivityResumed(Activity activity) {
                try {
                    OverlayFeatures current = features.get();
                    GameOverlay deck = surfaces.get(activity);
                    if (current.panel() && deck == null) {
                        deck = new GameOverlay(activity, false, features);
                        surfaces.put(activity, deck);
                    }
                    if (deck != null) deck.resume(current.panel());
                    if (RuntimeBootstrap.loaded()) NativeCommandBridge.foreground(true);
                } catch (RuntimeException | LinkageError error) {
                    android.util.Log.e("BetterEndfield.Overlay", "resume failed", error);
                }
            }
            @Override public void onActivityPaused(Activity activity) {
                GameOverlay deck = surfaces.get(activity);
                if (deck != null) deck.pause();
                try { if (RuntimeBootstrap.loaded()) NativeCommandBridge.foreground(false); }
                catch (LinkageError error) { android.util.Log.e("BetterEndfield.Overlay", "suspend bridge failed", error); }
            }
            @Override public void onActivityDestroyed(Activity activity) {
                GameOverlay deck = surfaces.remove(activity);
                if (deck != null) deck.remove();
            }
            @Override public void onActivityCreated(Activity a, Bundle b) { }
            @Override public void onActivityStarted(Activity a) { }
            @Override public void onActivityStopped(Activity a) { }
            @Override public void onActivitySaveInstanceState(Activity a, Bundle b) { }
        });
    }

    GameOverlay(Activity activity, boolean preview) {
        this(activity, preview, () -> OverlayFeatures.read(FrameworkSettings.open(activity)));
        resume(true);
    }
    private GameOverlay(Activity activity, boolean preview, Supplier<OverlayFeatures> features) {
        this.activity = activity; this.preview = preview; this.features = features;
        host = new FrameLayout(activity) {
            @Override public void onWindowFocusChanged(boolean focused) {
                super.onWindowFocusChanged(focused);
                if (!focused) releaseHeldKeys();
                // Focus can leave for our diagnostics dialog: release input,
                // but only Activity pause suspends the camera/world state.
            }
        };
        host.setClipChildren(false);
        host.setClickable(false); // Outside the deck, touches belong to the game.
        host.setMotionEventSplittingEnabled(true);
        handle = new ControlIcon(activity, ControlIcon.HANDLE, GOLD);
        handle.setBackground(surface(SURFACE, 16, EDGE));
        handle.setElevation(dp(8));
        handle.setPadding(dp(11), dp(11), dp(11), dp(11));
        handle.setImportantForAccessibility(View.IMPORTANT_FOR_ACCESSIBILITY_YES);
        handle.setContentDescription("快捷控制，点按展开，拖动移动");
        handle.setOnClickListener(view -> toggle());
        handle.setOnTouchListener(new View.OnTouchListener() {
            float downX, downY, originX, originY; boolean moved;
            @Override public boolean onTouch(View view, MotionEvent event) {
                switch (event.getActionMasked()) {
                    case MotionEvent.ACTION_DOWN:
                        downX = event.getRawX(); downY = event.getRawY();
                        originX = handle.getX(); originY = handle.getY(); moved = false;
                        return true;
                    case MotionEvent.ACTION_MOVE:
                        float dx = event.getRawX() - downX, dy = event.getRawY() - downY;
                        moved |= Math.hypot(dx, dy) > ViewConfiguration.get(activity).getScaledTouchSlop();
                        if (moved) {
                            x = clamp((originX + dx - host.getPaddingLeft() - dp(8)) / Math.max(1, usableWidth() - dp(48)));
                            y = clamp((originY + dy - host.getPaddingTop() - dp(8)) / Math.max(1, usableHeight() - dp(48)));
                            savedX = x; savedY = y; position();
                        }
                        return true;
                    case MotionEvent.ACTION_UP:
                        if (!moved) view.performClick();
                        return true;
                    case MotionEvent.ACTION_CANCEL: return true;
                    default: return true;
                }
            }
        });
        host.addView(handle, new FrameLayout.LayoutParams(dp(48), dp(48)));
        body = new LinearLayout(activity); body.setOrientation(LinearLayout.VERTICAL);
        body.setPadding(dp(12), dp(10), dp(12), dp(12));
        panel = new ScrollView(activity); panel.setFillViewport(false);
        panel.setBackground(surface(SURFACE, 20, EDGE)); panel.setElevation(dp(12));
        panel.setClipToOutline(true); panel.setVerticalScrollBarEnabled(false);
        panel.addView(body); panel.setVisibility(preview ? View.VISIBLE : View.GONE);
        host.addView(panel, new FrameLayout.LayoutParams(dp(320), -2));
        status = text("", 11, MUTED);
        status.setPadding(dp(2), dp(10), 0, 0);
        status.setOnLongClickListener(v -> { showDiagnostics(); return true; });
        host.setOnApplyWindowInsetsListener((view, insets) -> {
            if (Build.VERSION.SDK_INT >= 30) {
                android.graphics.Insets safe = insets.getInsets(android.view.WindowInsets.Type.systemBars()
                        | android.view.WindowInsets.Type.displayCutout());
                host.setPadding(safe.left, safe.top, safe.right, safe.bottom);
            } else host.setPadding(insets.getSystemWindowInsetLeft(), insets.getSystemWindowInsetTop(),
                    insets.getSystemWindowInsetRight(), insets.getSystemWindowInsetBottom());
            host.post(this::position); return insets;
        });
        host.addOnLayoutChangeListener((v,l,t,r,b,ol,ot,or,ob) -> position());
        rebuild();
    }

    private void resume(boolean visible) {
        resumed = visible; host.setVisibility(visible ? View.VISIBLE : View.GONE);
        main.removeCallbacks(pulse);
        if (visible) { rebuild(); main.post(pulse); }
        else releaseHeldKeys();
    }
    private void pause() {
        resumed = false; main.removeCallbacks(pulse); releaseHeldKeys(); host.setVisibility(View.GONE);
    }
    private void reconcileHost() {
        if (!(activity.getWindow().getDecorView() instanceof ViewGroup)) return;
        ViewGroup root = (ViewGroup) activity.getWindow().getDecorView();
        if (host.getParent() != root) {
            releaseHeldKeys();
            if (host.getParent() instanceof ViewGroup) ((ViewGroup) host.getParent()).removeView(host);
            root.addView(host, new ViewGroup.LayoutParams(-1, -1)); host.requestApplyInsets();
        }
        // SDKs can replace Activity content or append their own surface later.
        // This check only runs while resumed, not from an immortal background task.
        if (root.indexOfChild(host) != root.getChildCount() - 1) host.bringToFront();
    }
    private void toggle() {
        releaseHeldKeys(); panel.animate().cancel();
        boolean open = panel.getVisibility() != View.VISIBLE;
        panel.setVisibility(open ? View.VISIBLE : View.GONE);
        if (open) { rebuild(); panel.setAlpha(0); panel.animate().alpha(1).setDuration(120).start(); }
        else panel.setAlpha(1);
        position();
    }

    private void rebuild() {
        releaseHeldKeys(); tiles.clear(); heldControls.clear(); body.removeAllViews();
        try { shown = features.get(); } catch (RuntimeException unavailable) { shown = OverlayFeatures.off(); }
        if (preview && !shown.anyControl()) shown = new OverlayFeatures(true, true, true, true, true);
        LinearLayout heading = row();
        LinearLayout title = column();
        TextView brand = text("BETTER ENDFIELD", 9, GOLD); brand.setLetterSpacing(0.14f);
        title.addView(brand); title.addView(text("快捷控制", 18, INK));
        heading.addView(title, new LinearLayout.LayoutParams(0, -2, 1));
        TextView close = smallButton(preview ? "完成" : "收起");
        close.setOnClickListener(v -> { if (preview) remove(); else toggle(); });
        heading.addView(close, new LinearLayout.LayoutParams(dp(48), dp(48))); body.addView(heading);
        LinearLayout shortcuts = row();
        if (shown.hideHud()) shortcuts.addView(tile("HUD", "0", ControlIcon.HUD, Hotkeys.HIDE_HUD, 0), weighted(0));
        if (shown.freeCamera()) shortcuts.addView(tile("自由镜头", "9", ControlIcon.CAMERA, Hotkeys.FREE_CAMERA, 1), weighted(shortcuts.getChildCount()));
        if (shown.worldPause()) shortcuts.addView(tile("冻结", "8", ControlIcon.PAUSE, Hotkeys.WORLD_PAUSE, 8), weighted(shortcuts.getChildCount()));
        if (shown.firstPerson()) shortcuts.addView(tile("第一人称", "−", ControlIcon.EYE, Hotkeys.FIRST_PERSON, 2), weighted(shortcuts.getChildCount()));
        body.addView(shortcuts, stacked(10));
        if (!shown.anyControl()) body.addView(text("尚未配置热键功能。请在增强设置中选择。", 12, MUTED), stacked(14));
        if (shown.freeCamera()) {
            TextView expand = smallButton(expanded ? "镜头控制  −" : "镜头控制  +");
            expand.setTextColor(GOLD); expand.setOnClickListener(v -> { expanded = !expanded; rebuild(); });
            body.addView(expand, stacked(8));
            if (expanded) addCameraControls();
        }
        LinearLayout foot = row();
        TextView settings = text("增强设置", 11, MUTED); settings.setMinimumHeight(dp(40));
        settings.setGravity(Gravity.CENTER_VERTICAL); settings.setOnClickListener(v -> openSettings());
        foot.addView(settings, new LinearLayout.LayoutParams(0, -2, 1));
        TextView info = text("状态", 11, MUTED); info.setMinimumHeight(dp(40)); info.setGravity(Gravity.CENTER);
        info.setOnClickListener(v -> showDiagnostics()); foot.addView(info, new LinearLayout.LayoutParams(dp(48), -2));
        body.addView(foot, stacked(2));
        View rule = new View(activity); rule.setBackgroundColor(EDGE); body.addView(rule, new LinearLayout.LayoutParams(-1, dp(1)));
        body.addView(status); refreshState(); host.post(this::position);
    }
    private Tile tile(String title, String keyName, int icon, int key, int capability) {
        Tile tile = new Tile(title, keyName, icon, key, capability); tiles.add(tile); return tile;
    }
    private final class Tile extends LinearLayout {
        final int key, capability; final ControlIcon icon;
        Tile(String title, String keyName, int kind, int key, int capability) {
            super(activity); this.key = key; this.capability = capability;
            setOrientation(VERTICAL); setGravity(Gravity.CENTER); setPadding(dp(2), dp(8), dp(2), dp(8));
            setMinimumHeight(dp(78)); setBackground(buttonBackground());
            icon = new ControlIcon(activity, kind, MUTED); addView(icon, new LinearLayout.LayoutParams(dp(24), dp(24)));
            TextView name = text(title, 11, INK); name.setGravity(Gravity.CENTER); addView(name, stacked(5));
            TextView keycap = text(keyName, 9, MUTED); keycap.setTypeface(Typeface.MONOSPACE); keycap.setGravity(Gravity.CENTER);
            addView(keycap, stacked(2)); setContentDescription(title + "，对应热键 " + keyName);
            setOnClickListener(v -> send(key, NativeCommandBridge.KEY_PULSE));
        }
        void update() {
            boolean available = preview || (capability == 0 ? snapshot.ready("betterendfield.ui") : snapshot.cameraAvailable(capability));
            if (capability == 8 && !preview) available &= snapshot.cameraActive(1) || snapshot.cameraActive(8);
            setEnabled(available); setAlpha(available ? 1 : 0.38f);
            boolean active = !preview && (capability == 0 ? snapshot.hudHidden() : snapshot.cameraActive(capability));
            setSelected(active); icon.tint(active ? GOLD : INK);
        }
    }
    private void addCameraControls() {
        LinearLayout controls = row(); LinearLayout pad = column();
        pad.addView(padRow(hold("升", Hotkeys.MOVE_UP), hold("↑", Hotkeys.MOVE_FORWARD), hold("降", Hotkeys.MOVE_DOWN)));
        pad.addView(padRow(hold("←", Hotkeys.MOVE_LEFT), stopButton(), hold("→", Hotkeys.MOVE_RIGHT)), stacked(4));
        pad.addView(padRow(hold("慢", 0x11), hold("↓", Hotkeys.MOVE_BACK), hold("快", 0x10)), stacked(4));
        controls.addView(pad, new LinearLayout.LayoutParams(0, -2, 1));
        TextView look = smallButton("划动转向\nLOOK"); look.setTextSize(12); look.setTextColor(MUTED);
        look.setOnTouchListener(new View.OnTouchListener() {
            float px, py;
            @Override public boolean onTouch(View v, MotionEvent e) {
                if (!preview && !snapshot.cameraActive(1)) return false;
                switch (e.getActionMasked()) {
                    case MotionEvent.ACTION_DOWN:
                        px = e.getX(); py = e.getY(); v.setPressed(true);
                        v.getParent().requestDisallowInterceptTouchEvent(true); return true;
                    case MotionEvent.ACTION_MOVE:
                        float dx = e.getX() - px, dy = e.getY() - py; px = e.getX(); py = e.getY();
                        if (!preview && RuntimeBootstrap.loaded()) {
                            try { NativeCommandBridge.look(Math.round(dx), Math.round(dy)); }
                            catch (LinkageError error) { bridgeError = true; }
                        }
                        return true;
                    case MotionEvent.ACTION_UP: v.performClick();
                    case MotionEvent.ACTION_CANCEL:
                        v.setPressed(false); v.getParent().requestDisallowInterceptTouchEvent(false); return true;
                    default: return true;
                }
            }
        });
        LinearLayout.LayoutParams lookParams = new LinearLayout.LayoutParams(0, dp(152), 1); lookParams.leftMargin = dp(10);
        controls.addView(look, lookParams); heldControls.add(look); body.addView(controls, stacked(4));
        body.addView(padRow(hold("左滚", 0x67), tap("复位", 0x65), hold("右滚", 0x69)), stacked(8));
        body.addView(padRow(hold("广角", 0x61), tap("运镜", 0x68), hold("长焦", 0x63)), stacked(4));
        TextView more = smallButton(details ? "收起关键帧" : "关键帧热键");
        more.setOnClickListener(v -> { details = !details; rebuild(); }); body.addView(more, stacked(6));
        if (details) body.addView(padRow(tap("添加帧", 0x60), tap("播放帧", 0x62), tap("清空帧", 0x64)), stacked(4));
    }
    private LinearLayout padRow(View a, View b, View c) {
        LinearLayout row = row(); row.setMotionEventSplittingEnabled(true);
        row.addView(a, cell(0)); row.addView(b, cell(4)); row.addView(c, cell(4)); return row;
    }
    private View stopButton() {
        TextView stop = smallButton("停"); stop.setOnClickListener(v -> releaseHeldKeys()); heldControls.add(stop); return stop;
    }
    private View tap(String label, int key) {
        TextView view = smallButton(label); view.setOnClickListener(v -> send(key, NativeCommandBridge.KEY_PULSE));
        heldControls.add(view); return view;
    }
    private View hold(String label, int key) {
        TextView view = smallButton(label); view.setContentDescription(label + "，按住生效"); heldControls.add(view);
        view.setOnTouchListener(new View.OnTouchListener() {
            int pointer = -1;
            @Override public boolean onTouch(View v, MotionEvent event) {
                switch (event.getActionMasked()) {
                    case MotionEvent.ACTION_DOWN:
                        pointer = event.getPointerId(0); v.setPressed(true);
                        v.getParent().requestDisallowInterceptTouchEvent(true);
                        send(key, NativeCommandBridge.KEY_PRESS); return true;
                    case MotionEvent.ACTION_POINTER_UP:
                        if (event.getPointerId(event.getActionIndex()) != pointer) return true;
                        // The finger owning this button left, even if another remains.
                    case MotionEvent.ACTION_UP:
                    case MotionEvent.ACTION_CANCEL:
                        pointer = -1; v.setPressed(false);
                        send(key, NativeCommandBridge.KEY_RELEASE);
                        v.getParent().requestDisallowInterceptTouchEvent(false); return true;
                    default: return true;
                }
            }
        }); return view;
    }

    private void refreshState() {
        if (preview) { status.setText("预览模式 · 不发送任何游戏指令"); }
        else if (RuntimeBootstrap.loaded()) {
            try { snapshot = RuntimeSnapshot.parse(NativeCommandBridge.runtimeStatus()); bridgeError = false; }
            catch (LinkageError error) { snapshot = RuntimeSnapshot.offline(); bridgeError = true; }
            status.setText(bridgeError ? "控制桥绑定失败 · 请查看日志" : snapshot.summary());
        } else status.setText(RuntimeBootstrap.failure());
        for (Tile tile : tiles) tile.update();
        boolean movable = preview || snapshot.cameraActive(1);
        if (movementAllowed && !movable) releaseHeldKeys();
        movementAllowed = movable;
        for (View view : heldControls) { view.setEnabled(movable); view.setAlpha(movable ? 1 : 0.35f); }
    }
    private void send(int key, int action) {
        if (preview) return;
        if (!RuntimeBootstrap.loaded()) { toast(RuntimeBootstrap.failure()); return; }
        try {
            if (!NativeCommandBridge.key(key, action)) toast("输入队列已满或键位无效");
        } catch (LinkageError error) { toast("控制桥绑定失败，请查看运行日志"); }
    }
    private void releaseHeldKeys() {
        for (View view : heldControls) view.setPressed(false);
        if (!preview && RuntimeBootstrap.loaded()) {
            try { NativeCommandBridge.releaseKeys(); }
            catch (LinkageError error) { android.util.Log.e("BetterEndfield.Overlay", "key release bridge failed", error); }
        }
    }
    private void showDiagnostics() {
        StringBuilder text = new StringBuilder();
        if (preview) text.append("预览模式，不读取游戏运行状态。");
        else if (!snapshot.connected) text.append(RuntimeBootstrap.failure());
        else snapshot.values.forEach((key, value) -> text.append(key).append(": ").append(value).append('\n'));
        text.append("\n设置只表示配置意图。修改模块开关后需要重新启动游戏。");
        new AlertDialog.Builder(activity).setTitle("运行状态").setMessage(text).setPositiveButton("关闭", null).show();
    }
    private void openSettings() {
        releaseHeldKeys();
        if (preview) { remove(); return; }
        try { activity.startActivity(new Intent().setClassName(RuntimeBootstrap.MODULE_PACKAGE,
                RuntimeBootstrap.MODULE_PACKAGE + ".MainActivity").putExtra(MainActivity.EXTRA_PAGE, "enhancement")); }
        catch (RuntimeException error) { toast("无法打开增强设置"); }
    }
    void remove() {
        closed = true; pause(); panel.animate().cancel();
        if (host.getParent() instanceof ViewGroup) ((ViewGroup) host.getParent()).removeView(host);
    }
    private void position() {
        if (closed || host.getWidth() == 0) return;
        int left = host.getPaddingLeft() + dp(8), top = host.getPaddingTop() + dp(8);
        int width = usableWidth(), height = usableHeight();
        handle.setX(left + x * Math.max(0, width - dp(48)));
        handle.setY(top + y * Math.max(0, height - dp(48)));
        int pw = Math.min(dp(320), width);
        panel.measure(View.MeasureSpec.makeMeasureSpec(pw, View.MeasureSpec.EXACTLY),
                View.MeasureSpec.makeMeasureSpec(height, View.MeasureSpec.AT_MOST));
        int ph = Math.min(height, panel.getMeasuredHeight());
        FrameLayout.LayoutParams params = (FrameLayout.LayoutParams) panel.getLayoutParams();
        if (params.width != pw || params.height != ph) { params.width = pw; params.height = ph; panel.setLayoutParams(params); }
        float beside = handle.getX() + dp(56);
        if (beside + pw > left + width) beside = handle.getX() - pw - dp(8);
        panel.setX(Math.max(left, Math.min(beside, left + width - pw)));
        panel.setY(Math.max(top, Math.min(handle.getY(), top + height - ph)));
        handle.bringToFront();
    }
    private int usableWidth() { return Math.max(1, host.getWidth() - host.getPaddingLeft() - host.getPaddingRight() - dp(16)); }
    private int usableHeight() { return Math.max(1, host.getHeight() - host.getPaddingTop() - host.getPaddingBottom() - dp(16)); }
    private int dp(int n) { return Math.round(n * activity.getResources().getDisplayMetrics().density); }
    private static float clamp(float value) { return Math.max(0, Math.min(1, value)); }
    private void toast(String text) { Toast.makeText(activity, text, Toast.LENGTH_SHORT).show(); }
    private LinearLayout column() { LinearLayout v = new LinearLayout(activity); v.setOrientation(LinearLayout.VERTICAL); return v; }
    private LinearLayout row() { LinearLayout v = new LinearLayout(activity); v.setOrientation(LinearLayout.HORIZONTAL); v.setGravity(Gravity.CENTER_VERTICAL); return v; }
    private LinearLayout.LayoutParams stacked(int margin) { LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(-1, -2); p.topMargin = dp(margin); return p; }
    private LinearLayout.LayoutParams weighted(int index) { LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(0, -2, 1); if (index > 0) p.leftMargin = dp(5); return p; }
    private LinearLayout.LayoutParams cell(int margin) { LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(0, dp(48), 1); p.leftMargin = dp(margin); return p; }
    private TextView text(String value, int size, int color) {
        TextView v = new TextView(activity); v.setText(value); v.setTextSize(size); v.setTextColor(color);
        v.setIncludeFontPadding(false); v.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL)); return v;
    }
    private TextView smallButton(String title) {
        TextView v = text(title, 12, INK); v.setGravity(Gravity.CENTER); v.setMinimumHeight(dp(48));
        v.setBackground(buttonBackground()); return v;
    }
    private GradientDrawable surface(int color, int radius, int border) {
        GradientDrawable d = new GradientDrawable(); d.setColor(color); d.setCornerRadius(dp(radius)); d.setStroke(dp(1), border); return d;
    }
    private StateListDrawable buttonBackground() {
        StateListDrawable d = new StateListDrawable();
        d.addState(new int[]{android.R.attr.state_pressed}, surface(0xFF454339, 12, GOLD));
        d.addState(new int[]{android.R.attr.state_selected}, surface(0xFF35342B, 12, 0xFF82744E));
        d.addState(new int[]{}, surface(TILE, 12, Color.TRANSPARENT)); return d;
    }
}
