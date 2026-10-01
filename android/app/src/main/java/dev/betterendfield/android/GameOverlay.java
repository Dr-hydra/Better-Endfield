package dev.betterendfield.android;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Application;
import android.content.Intent;
import android.content.res.ColorStateList;
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
import android.widget.SeekBar;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.Locale;
import java.util.HashSet;
import java.util.Set;
import java.util.function.Supplier;
import java.util.function.BooleanSupplier;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/** In-Activity hotkey deck, not a second settings screen or overlay service. */
final class GameOverlay {
    private static final int INK = 0xFFF2F2EF, MUTED = 0xFF9DA6AC, GOLD = 0xFFE6CD82;
    private static final int SURFACE = 0xF2161B20, TILE = 0xFF242B31, EDGE = 0xFF3B434A;
    private static float savedX = 0.02f, savedY = 0.32f;
    // Deliberately process-local: Activity recreation must not undo "本次关闭".
    private static boolean sessionDismissed;
    private static final int HUD = 0, CAMERA = 1, PAUSE = 2, FIRST_PERSON = 3, MMD = 4;
    private final Activity activity;
    private final boolean preview;
    private final Supplier<OverlayFeatures> features;
    private final Handler main = new Handler(Looper.getMainLooper());
    private final FrameLayout host;
    private final View handle;
    private final LinearLayout panel;
    private final ScrollView content;
    private final LinearLayout navigation;
    private final LinearLayout body;
    private final TextView status;
    private final ArrayList<Tile> tiles = new ArrayList<>();
    private final ArrayList<View> heldControls = new ArrayList<>();
    private final Set<Integer> heldKeys = new HashSet<>();
    private final ArrayList<DragTouch> dragGestures = new ArrayList<>();
    private final ArrayList<MmdButton> mmdButtons = new ArrayList<>();
    private final ArrayList<Runnable> cancelGestures = new ArrayList<>();
    private int selectedTab = CAMERA;
    private CameraSlider speedSlider, fovSlider;
    private float cameraSpeed = 5f, cameraFov = 60f;
    private JSONObject mmd = new JSONObject();
    private boolean mmdConnected, seeking;
    private TextView mmdSummary, mmdTime, mmdMessage, mmdKeys;
    private SeekBar timeline;
    private LinearLayout library;
    private String librarySignature = "";
    private final ArrayList<String> workFolders = new ArrayList<>();
    private final ArrayList<TextView> workRows = new ArrayList<>();
    private RuntimeSnapshot snapshot = RuntimeSnapshot.offline();
    private OverlayFeatures shown = OverlayFeatures.off();
    private boolean resumed, closed, details, bridgeError, movementAllowed;
    private float x = savedX, y = savedY;
    private final Runnable pulse = new Runnable() {
        @Override public void run() {
            if (!resumed || closed) return;
            if (!preview && sessionDismissed) { remove(); return; }
            reconcileHost();
            OverlayFeatures current = readFeatures();
            if (!current.panel() && !preview) { pause(); return; }
            if (!current.equals(shown)) rebuild();
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
                    if (current.panel() && !sessionDismissed && deck == null) {
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
                catch (RuntimeException | LinkageError error) { android.util.Log.e("BetterEndfield.Overlay", "suspend bridge failed", error); }
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
            @Override protected void onDetachedFromWindow() {
                releaseHeldKeys();
                super.onDetachedFromWindow();
            }
        };
        host.setClipChildren(true);
        host.setClickable(false); // Outside the deck, touches belong to the game.
        host.setMotionEventSplittingEnabled(true);
        handle = new ControlIcon(activity, ControlIcon.HANDLE, GOLD);
        handle.setBackground(surface(SURFACE, 16, EDGE));
        handle.setElevation(dp(8));
        handle.setPadding(dp(11), dp(11), dp(11), dp(11));
        handle.setImportantForAccessibility(View.IMPORTANT_FOR_ACCESSIBILITY_YES);
        handle.setContentDescription("快捷控制，点按展开，拖动移动");
        handle.setOnClickListener(view -> toggle());
        handle.setOnTouchListener(new DragTouch(true));
        host.addView(handle, new FrameLayout.LayoutParams(dp(48), dp(48)));
        body = new LinearLayout(activity); body.setOrientation(LinearLayout.VERTICAL);
        body.setPadding(dp(12), dp(10), dp(12), dp(12));
        panel = row(); panel.setGravity(Gravity.TOP); panel.setClickable(true);
        panel.setBackground(surface(SURFACE, 20, EDGE)); panel.setElevation(dp(12));
        panel.setClipToOutline(true);
        ScrollView rail = new ScrollView(activity); rail.setVerticalScrollBarEnabled(false);
        navigation = column(); navigation.setPadding(dp(6), dp(8), dp(6), dp(8));
        rail.addView(navigation); panel.addView(rail, new LinearLayout.LayoutParams(dp(76), -1));
        content = new ScrollView(activity); content.setFillViewport(false); content.setVerticalScrollBarEnabled(true);
        content.setNestedScrollingEnabled(true);
        content.addView(body); panel.addView(content, new LinearLayout.LayoutParams(0, -1, 1));
        panel.setVisibility(preview ? View.VISIBLE : View.GONE);
        host.addView(panel, new FrameLayout.LayoutParams(dp(480), dp(560)));
        status = text("", 11, MUTED);
        status.setPadding(dp(2), dp(10), 0, 0);
        status.setOnLongClickListener(v -> { showDiagnostics(); return true; });
        host.setOnApplyWindowInsetsListener((view, insets) -> {
            if (Build.VERSION.SDK_INT >= 30) {
                android.graphics.Insets safe = insets.getInsets(android.view.WindowInsets.Type.systemBars()
                        | android.view.WindowInsets.Type.displayCutout() | android.view.WindowInsets.Type.ime());
                host.setPadding(safe.left, safe.top, safe.right, safe.bottom);
            } else {
                android.view.DisplayCutout cutout = insets.getDisplayCutout();
                host.setPadding(Math.max(insets.getSystemWindowInsetLeft(), cutout == null ? 0 : cutout.getSafeInsetLeft()),
                        Math.max(insets.getSystemWindowInsetTop(), cutout == null ? 0 : cutout.getSafeInsetTop()),
                        Math.max(insets.getSystemWindowInsetRight(), cutout == null ? 0 : cutout.getSafeInsetRight()),
                        Math.max(insets.getSystemWindowInsetBottom(), cutout == null ? 0 : cutout.getSafeInsetBottom()));
            }
            host.post(this::position); return insets;
        });
        host.addOnLayoutChangeListener((v,l,t,r,b,ol,ot,or,ob) -> position());
        if (preview) {
            cameraSpeed = finiteNumber(ModuleSettings.getCameraSpeed(activity), 5f, ModuleSettings.SPEED_MINIMUM, ModuleSettings.SPEED_MAXIMUM);
            cameraFov = finiteNumber(ModuleSettings.getCameraFieldOfView(activity), 60f, ModuleSettings.FOV_MINIMUM, ModuleSettings.FOV_MAXIMUM);
        }
        rebuild();
    }

    private void resume(boolean visible) {
        if (closed || (!preview && sessionDismissed)) { pause(); return; }
        resumed = visible; host.setVisibility(visible ? View.VISIBLE : View.GONE);
        main.removeCallbacks(pulse);
        if (visible) { rebuild(); reconcileHost(); main.post(pulse); }
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
        releaseHeldKeys(); tiles.clear(); heldControls.clear(); cancelGestures.clear();
        mmdButtons.clear(); workRows.clear(); workFolders.clear(); librarySignature = "";
        // Keep the handle gesture; drop listeners belonging to the old title.
        dragGestures.removeIf(gesture -> !gesture.clickable);
        speedSlider = null; fovSlider = null; timeline = null; library = null;
        mmdSummary = null; mmdTime = null; mmdMessage = null; mmdKeys = null;
        body.removeAllViews(); navigation.removeAllViews();
        shown = readFeatures();
        if (!hasTab(selectedTab)) {
            selectedTab = -1;
            for (int tab = 0; tab <= MMD; tab++) if (hasTab(tab)) { selectedTab = tab; break; }
        }
        addNavigation(HUD, "界面", ControlIcon.HUD);
        addNavigation(CAMERA, "自由镜头", ControlIcon.CAMERA);
        addNavigation(PAUSE, "冻结时间", ControlIcon.PAUSE);
        addNavigation(FIRST_PERSON, "第一人称", ControlIcon.EYE);
        addNavigation(MMD, "MMD", ControlIcon.MMD);
        TextView brand = text("BETTER ENDFIELD", 9, GOLD); brand.setLetterSpacing(0.14f);
        brand.setMinimumHeight(dp(32)); brand.setGravity(Gravity.CENTER_VERTICAL);
        brand.setContentDescription("拖动标题移动悬浮窗"); brand.setOnTouchListener(new DragTouch(false));
        body.addView(brand); body.addView(text(tabTitle(), 18, INK), stacked(6));
        LinearLayout windowActions = row();
        TextView collapse = smallButton(preview ? "完成" : "收起");
        collapse.setOnClickListener(v -> { if (preview) remove(); else toggle(); });
        windowActions.addView(collapse, weighted(0));
        if (!preview) {
            TextView dismiss = smallButton("本次关闭");
            dismiss.setContentDescription("本次关闭悬浮窗，保留功能状态，游戏重启后可再次出现");
            dismiss.setOnClickListener(v -> { sessionDismissed = true; remove(); });
            windowActions.addView(dismiss, weighted(1));
        }
        body.addView(windowActions, stacked(10));
        switch (selectedTab) {
            case HUD: body.addView(tile("隐藏 / 恢复 HUD", "0", ControlIcon.HUD, Hotkeys.HIDE_HUD, 0), stacked(10)); break;
            case CAMERA:
                body.addView(tile("启用 / 退出自由镜头", "9", ControlIcon.CAMERA, Hotkeys.FREE_CAMERA, 1), stacked(10));
                speedSlider = new CameraSlider("移动速度", "", ModuleSettings.SPEED_MINIMUM, ModuleSettings.SPEED_MAXIMUM, true);
                fovSlider = new CameraSlider("视野 FOV", "°", ModuleSettings.FOV_MINIMUM, ModuleSettings.FOV_MAXIMUM, false);
                body.addView(speedSlider, stacked(8)); body.addView(fovSlider, stacked(4));
                addCameraControls(); break;
            case PAUSE:
                body.addView(tile("冻结 / 恢复时间", "8", ControlIcon.PAUSE, Hotkeys.WORLD_PAUSE, 8), stacked(10));
                body.addView(text("冻结时间可独立使用。关闭悬浮窗会保留冻结状态；请先点按恢复，或在设置中关闭后重启游戏。", 12, MUTED), stacked(10)); break;
            case FIRST_PERSON: body.addView(tile("启用 / 退出第一人称", "−", ControlIcon.EYE, Hotkeys.FIRST_PERSON, 2), stacked(10)); break;
            case MMD: addMmdControls(); break;
            default: body.addView(text("尚未配置功能。请在增强设置中选择。", 12, MUTED), stacked(14));
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
    private OverlayFeatures readFeatures() {
        try { OverlayFeatures current = features.get(); return current == null ? OverlayFeatures.off() : current; }
        catch (RuntimeException unavailable) { return OverlayFeatures.off(); }
    }
    private final class DragTouch implements View.OnTouchListener {
        final boolean clickable;
        float downX, downY, originX, originY; boolean moved; int pointer = -1;
        DragTouch(boolean clickable) { this.clickable = clickable; dragGestures.add(this); }
        void cancel() { pointer = -1; }
        @Override public boolean onTouch(View view, MotionEvent event) {
            switch (event.getActionMasked()) {
                case MotionEvent.ACTION_DOWN:
                    pointer = event.getPointerId(0); downX = event.getRawX(); downY = event.getRawY();
                    originX = handle.getX(); originY = handle.getY(); moved = false;
                    view.getParent().requestDisallowInterceptTouchEvent(true); return true;
                case MotionEvent.ACTION_MOVE:
                    if (pointer < 0 || event.findPointerIndex(pointer) != 0) return true;
                    float dx = event.getRawX() - downX, dy = event.getRawY() - downY;
                    moved |= Math.hypot(dx, dy) > ViewConfiguration.get(activity).getScaledTouchSlop();
                    if (moved) {
                        OverlayGeometry.Layout geometry = geometry();
                        x = OverlayGeometry.normalized(originX + dx, geometry.safe().left(), geometry.safe().width(), geometry.handle().width());
                        y = OverlayGeometry.normalized(originY + dy, geometry.safe().top(), geometry.safe().height(), geometry.handle().height());
                        savedX = x; savedY = y; position();
                    }
                    return true;
                case MotionEvent.ACTION_POINTER_UP:
                    if (event.getPointerId(event.getActionIndex()) != pointer) return true;
                    pointer = -1; view.getParent().requestDisallowInterceptTouchEvent(false); return true;
                case MotionEvent.ACTION_UP:
                    if (pointer >= 0 && !moved && clickable) view.performClick();
                case MotionEvent.ACTION_CANCEL:
                    pointer = -1; view.getParent().requestDisallowInterceptTouchEvent(false); return true;
                default: return true;
            }
        }
    }
    private boolean hasTab(int tab) {
        if (preview) return tab >= HUD && tab <= MMD;
        switch (tab) {
            case HUD: return shown.hideHud();
            case CAMERA: return shown.freeCamera();
            case PAUSE: return shown.worldPause();
            case FIRST_PERSON: return shown.firstPerson();
            case MMD: return shown.mmd();
            default: return false;
        }
    }
    private String tabTitle() {
        switch (selectedTab) {
            case HUD: return "界面控制";
            case CAMERA: return "自由镜头";
            case PAUSE: return "冻结时间";
            case FIRST_PERSON: return "第一人称";
            case MMD: return "MMD 控制台";
            default: return "快捷控制";
        }
    }
    private void addNavigation(int tab, String label, int kind) {
        if (!hasTab(tab)) return;
        LinearLayout item = column(); item.setGravity(Gravity.CENTER);
        item.setPadding(dp(2), dp(10), dp(2), dp(10)); item.setMinimumHeight(dp(72));
        item.setBackground(buttonBackground()); item.setSelected(selectedTab == tab);
        item.setContentDescription(label); item.setImportantForAccessibility(View.IMPORTANT_FOR_ACCESSIBILITY_YES);
        item.addView(new ControlIcon(activity, kind, selectedTab == tab ? GOLD : MUTED), new LinearLayout.LayoutParams(dp(24), dp(24)));
        TextView name = text(label, 10, selectedTab == tab ? GOLD : INK); name.setGravity(Gravity.CENTER);
        name.setMaxLines(2); name.setImportantForAccessibility(View.IMPORTANT_FOR_ACCESSIBILITY_NO); item.addView(name, stacked(6));
        item.setOnClickListener(v -> { if (selectedTab != tab) { selectedTab = tab; rebuild(); content.scrollTo(0, 0); } });
        navigation.addView(item, stacked(4));
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
            setEnabled(available); setAlpha(available ? 1 : 0.38f);
            boolean active = !preview && (capability == 0 ? snapshot.hudHidden() : snapshot.cameraActive(capability));
            setSelected(active); icon.tint(active ? GOLD : INK);
        }
    }
    private final class CameraSlider extends LinearLayout {
        final SeekBar bar;
        final TextView label;
        final String title, unit;
        final float minimum, maximum;
        final boolean speed;
        boolean tracking;
        CameraSlider(String title, String unit, float minimum, float maximum, boolean speed) {
            super(activity); this.title = title; this.unit = unit; this.minimum = minimum;
            this.maximum = maximum; this.speed = speed;
            setOrientation(VERTICAL); label = text("", 12, GOLD); addView(label);
            bar = new SeekBar(activity); styleSlider(bar); bar.setMax(1000); bar.setContentDescription(title);
            addView(bar, new LinearLayout.LayoutParams(-1, dp(48)));
            bar.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
                @Override public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                    float value = minimum + (maximum - minimum) * progress / 1000f;
                    label.setText(String.format(Locale.ROOT, "%s  %.1f%s", title, value, unit));
                    if (!fromUser) return;
                    if (speed) cameraSpeed = value; else cameraFov = value;
                    if (!canSend() || !snapshot.cameraAvailable(1)) return;
                    try { NativeCommandBridge.cameraValues(speed ? value : Float.NaN, speed ? Float.NaN : value); }
                    catch (RuntimeException | LinkageError error) { bridgeError = true; }
                }
                @Override public void onStartTrackingTouch(SeekBar seekBar) {
                    tracking = true; seekBar.getParent().requestDisallowInterceptTouchEvent(true);
                }
                @Override public void onStopTrackingTouch(SeekBar seekBar) {
                    tracking = false; seekBar.getParent().requestDisallowInterceptTouchEvent(false);
                }
            });
            update();
        }
        void update() {
            if (!tracking) bar.setProgress(Math.round(((speed ? cameraSpeed : cameraFov) - minimum) / (maximum - minimum) * 1000f));
            boolean available = preview || snapshot.cameraAvailable(1);
            bar.setEnabled(available); setAlpha(available ? 1f : 0.35f);
            // setProgress(0) need not invoke the listener.
            label.setText(String.format(Locale.ROOT, "%s  %.1f%s", title,
                    minimum + (maximum - minimum) * bar.getProgress() / 1000f, unit));
        }
    }
    private void styleSlider(SeekBar bar) {
        bar.setProgressTintList(ColorStateList.valueOf(GOLD));
        bar.setProgressBackgroundTintList(ColorStateList.valueOf(EDGE));
        bar.setThumbTintList(ColorStateList.valueOf(GOLD));
        bar.setSplitTrack(false); bar.setPadding(dp(12), 0, dp(12), 0);
    }

    private void addMmdControls() {
        mmdSummary = text("等待 MMD 运行状态", 12, GOLD); body.addView(mmdSummary, stacked(12));
        body.addView(text("作品库", 13, INK), stacked(12));
        body.addView(mmdButton("不选作品", 6, 0, 0, "", () -> !mmd.optString("work").isEmpty()), stacked(4));
        // A bounded inner list keeps transport reachable even with hundreds of works.
        ScrollView works = new ScrollView(activity); works.setFillViewport(false);
        works.setNestedScrollingEnabled(true);
        library = column(); works.addView(library); body.addView(works, new LinearLayout.LayoutParams(-1, dp(156)));
        mmdTime = text("00:00 / 00:00", 12, INK); body.addView(mmdTime, stacked(10));
        timeline = new SeekBar(activity); styleSlider(timeline); timeline.setMax(1000);
        timeline.setContentDescription("MMD 播放进度"); body.addView(timeline, new LinearLayout.LayoutParams(-1, dp(48)));
        timeline.setOnTouchListener((view, event) -> {
            if (event.getActionMasked() == MotionEvent.ACTION_CANCEL) seeking = false;
            return false;
        });
        timeline.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
            @Override public void onProgressChanged(SeekBar bar, int progress, boolean fromUser) {
                if (fromUser) mmdTime.setText(time(duration() * progress / 1000d) + " / " + time(duration()));
                // Accessibility edits don't produce a tracking gesture.
                if (fromUser && !seeking && activeMmd()) sendMmd(4, 0, duration() * progress / 1000d, "");
            }
            @Override public void onStartTrackingTouch(SeekBar bar) {
                seeking = true; bar.getParent().requestDisallowInterceptTouchEvent(true);
            }
            @Override public void onStopTrackingTouch(SeekBar bar) {
                boolean commit = seeking; seeking = false;
                bar.getParent().requestDisallowInterceptTouchEvent(false);
                if (commit && activeMmd()) sendMmd(4, 0, duration() * bar.getProgress() / 1000d, "");
            }
        });
        body.addView(padRow(mmdButton("−5 秒", 3, 0, -5, "", this::activeMmd),
                mmdButton("播放", 1, 0, 0, "", () -> mmd.optInt("state") != 1),
                mmdButton("+5 秒", 3, 0, 5, "", this::activeMmd)), stacked(4));
        LinearLayout transport = row();
        transport.addView(mmdButton("停止", 2, 0, 0, "", () -> mmd.optInt("state") != 0), weighted(0));
        transport.addView(mmdButton("循环", 7, 0, 0, "", () -> true), weighted(1)); body.addView(transport, stacked(4));
        body.addView(text("镜头模式", 13, INK), stacked(12));
        body.addView(padRow(mmdButton("VMD", 5, 0, 0, "", () -> true),
                mmdButton("自由", 5, 1, 0, "", () -> (mmd.optInt("available") & 8) != 0),
                mmdButton("游戏", 5, 2, 0, "", () -> true)), stacked(4));
        mmdKeys = text("机位 · 0 个", 13, INK); body.addView(mmdKeys, stacked(12));
        body.addView(padRow(mmdButton("记录", 8, 0, 0, "", this::freeMmd),
                mmdButton("播放", 9, 0, 0, "", () -> freeMmd() && mmd.optInt("keyframe_count") >= 2),
                mmdButton("清空", 10, 0, 0, "", () -> freeMmd() && mmd.optInt("keyframe_count") > 0)), stacked(4));
        body.addView(padRow(mmdButton("保存", 11, 0, 0, "", () -> mmd.optInt("keyframe_count") > 0),
                mmdButton("读取", 12, 0, 0, "", () -> true),
                mmdButton("运镜", 13, 0, 0, "", this::freeMmd)), stacked(4));
        body.addView(mmdButton("自由视角", 14, 0, 0, "", () -> (mmd.optInt("available") & 8) != 0), stacked(4));
        mmdMessage = text("", 11, MUTED); body.addView(mmdMessage, stacked(10));
    }
    private final class MmdButton {
        final TextView view; final int type, argument; final BooleanSupplier allowed;
        MmdButton(TextView view, int type, int argument, BooleanSupplier allowed) {
            this.view = view; this.type = type; this.argument = argument; this.allowed = allowed;
        }
        void update() {
            boolean enabled = preview || (mmdConnected && allowed.getAsBoolean());
            view.setEnabled(enabled); view.setAlpha(enabled ? 1f : 0.35f);
            boolean selected = (type == 5 && mmd.optInt("camera_mode") == argument)
                    || (type == 7 && flag(mmd, "loop")) || (type == 14 && freeMmd())
                    || (type == 1 && mmd.optInt("state") == 2);
            view.setSelected(!preview && mmdConnected && selected);
            if (type == 1) view.setText(mmd.optInt("state") == 2 ? "暂停" : "播放");
            if (type == 14) view.setText(freeMmd() ? "退出自由视角" : "自由视角");
        }
    }
    private TextView mmdButton(String title, int type, int argument, double value, String work, BooleanSupplier allowed) {
        TextView button = smallButton(title);
        mmdButtons.add(new MmdButton(button, type, argument, allowed));
        button.setOnClickListener(v -> { if (preview || (mmdConnected && allowed.getAsBoolean())) sendMmd(type, argument, value, work); });
        return button;
    }
    private boolean activeMmd() { return mmd.optInt("state") == 2 || mmd.optInt("state") == 3; }
    private boolean freeMmd() { return flag(mmd, "free_camera_active"); }
    private double duration() { return finiteNumber(mmd.optString("duration"), 0f, 0f, Float.MAX_VALUE); }
    private static boolean flag(JSONObject value, String key) { return value.optBoolean(key, false) || value.optInt(key, 0) != 0; }
    private static String time(double seconds) {
        long value = (long) Math.max(0d, seconds);
        return String.format(Locale.ROOT, "%02d:%02d", value / 60, value % 60);
    }
    private void sendMmd(int type, int argument, double value, String work) {
        if (!canSend() || !mmdConnected) return;
        try {
            JSONObject command = new JSONObject().put("type", type).put("argument", argument)
                    .put("value", value).put("text", work);
            if (!NativeCommandBridge.mmdCommand(command.toString())) toast("MMD 指令被拒绝或队列已满");
        } catch (JSONException | RuntimeException | LinkageError error) { toast("MMD 控制桥不可用"); }
    }
    private void refreshMmd() {
        if (library == null) return;
        mmdConnected = false;
        if (!preview && RuntimeBootstrap.loaded()) {
            try {
                String json = NativeCommandBridge.mmdStatus();
                if (json != null && json.length() <= 1048576) {
                    JSONObject next = new JSONObject(json);
                    if (next.has("state")) { mmd = next; mmdConnected = snapshot.ready("betterendfield.camera"); }
                }
            } catch (JSONException | RuntimeException | LinkageError error) { /* Disabled until the next valid observation. */ }
        }
        if (!mmdConnected) mmd = new JSONObject();
        int state = mmd.optInt("state");
        String stateText = state == 1 ? "加载中" : state == 2 ? "播放中" : state == 3 ? "已暂停" : "空闲";
        mmdSummary.setText(preview ? "预览模式" : mmdConnected ? stateText : "等待 MMD 运行状态");
        JSONArray works = mmd.optJSONArray("library");
        String signature = (preview ? "preview:" : mmdConnected ? "connected:" : "waiting:")
                + (works == null ? "[]" : works.toString());
        if (!signature.equals(librarySignature)) {
            librarySignature = signature; library.removeAllViews(); workRows.clear(); workFolders.clear();
            if (works != null) for (int i = 0; i < works.length(); i++) {
                JSONObject work = works.optJSONObject(i); if (work == null) continue;
                String folder = work.optString("folder"); if (folder.isEmpty()) continue;
                TextView item = smallButton(work.optString("name", folder)); item.setPadding(dp(6), dp(4), dp(6), dp(4));
                item.setGravity(Gravity.CENTER_VERTICAL); item.setContentDescription("选择作品 " + work.optString("name", folder));
                item.setOnClickListener(v -> sendMmd(6, 0, 0, folder));
                library.addView(item, stacked(4)); workRows.add(item); workFolders.add(folder);
            }
            if (workRows.isEmpty()) library.addView(text(preview ? "预览不读取作品库。" : mmdConnected
                    ? "还没有作品。请在管理器的 MMD 作品库中导入。" : "连接后显示已安装作品。", 12, MUTED), stacked(8));
        }
        for (int i = 0; i < workRows.size(); i++) {
            workRows.get(i).setSelected(workFolders.get(i).equals(mmd.optString("work")));
            workRows.get(i).setEnabled(mmdConnected);
        }
        double seconds = finiteNumber(mmd.optString("seconds"), 0f, 0f, (float) duration());
        if (!seeking) {
            timeline.setProgress(duration() <= 0 ? 0 : (int) Math.round(seconds / duration() * 1000));
            mmdTime.setText(time(seconds) + " / " + time(duration()));
        }
        boolean canSeek = preview || (mmdConnected && activeMmd() && duration() > 0);
        if (!canSeek && seeking) { seeking = false; timeline.getParent().requestDisallowInterceptTouchEvent(false); }
        timeline.setEnabled(canSeek); timeline.setAlpha(canSeek ? 1f : 0.35f);
        mmdKeys.setText("机位 · " + mmd.optInt("keyframe_count") + " 个");
        String message = mmdMessage();
        if (activeMmd() && mmd.optBoolean("terrain_requested") && !mmd.optBoolean("terrain_available"))
            message += "\n地形跟随未启用 · " + mmd.optString("terrain_reason", "接口不可用，继续普通动作播放");
        mmdMessage.setText(message);
        for (MmdButton button : mmdButtons) button.update();
    }
    private String mmdMessage() {
        String[] messages = {"", "正在加载", "开始播放", "已暂停", "已停止", "播放完成", "请先选择作品", "作品配置无法读取",
                "没有可播放的内容，请检查动作 / 镜头 / 音乐开关", "动作加载失败", "镜头加载失败", "音乐加载失败",
                "音乐模块未加载", "自由镜头未启用", "角色动作未启用", "已切换镜头模式", "已选择作品", "已切换循环"};
        int code = mmd.optInt("message_code"); String detail = mmd.optString("message");
        String message = code >= 0 && code < messages.length ? messages[code] : "";
        return detail.isEmpty() ? message : message.isEmpty() ? detail : message + " · " + detail;
    }
    private static float finiteNumber(String text, float fallback, float min, float max) {
        try { float value = Float.parseFloat(text); return Float.isFinite(value) ? Math.max(min, Math.min(max, value)) : fallback; }
        catch (NumberFormatException | NullPointerException invalid) { return fallback; }
    }
    private void addCameraControls() {
        LinearLayout controls = row(); LinearLayout pad = column();
        pad.addView(padRow(hold("升", Hotkeys.MOVE_UP), hold("↑", Hotkeys.MOVE_FORWARD), hold("降", Hotkeys.MOVE_DOWN)));
        pad.addView(padRow(hold("←", Hotkeys.MOVE_LEFT), stopButton(), hold("→", Hotkeys.MOVE_RIGHT)), stacked(4));
        pad.addView(padRow(hold("慢", 0x11), hold("↓", Hotkeys.MOVE_BACK), hold("快", 0x10)), stacked(4));
        controls.addView(pad, new LinearLayout.LayoutParams(0, -2, 1));
        TextView look = smallButton("划动转向\nLOOK"); look.setTextSize(12); look.setTextColor(MUTED);
        class LookTouch implements View.OnTouchListener {
            float px, py; int pointer = -1;
            void cancel() { pointer = -1; look.setPressed(false); look.getParent().requestDisallowInterceptTouchEvent(false); }
            @Override public boolean onTouch(View v, MotionEvent e) {
                switch (e.getActionMasked()) {
                    case MotionEvent.ACTION_DOWN:
                        if (!preview && (!canSend() || !snapshot.cameraActive(1))) return false;
                        pointer = e.getPointerId(0);
                        px = e.getX(); py = e.getY(); v.setPressed(true);
                        v.getParent().requestDisallowInterceptTouchEvent(true); return true;
                    case MotionEvent.ACTION_MOVE:
                        int index = e.findPointerIndex(pointer);
                        if (index < 0) return true;
                        float dx = e.getX(index) - px, dy = e.getY(index) - py; px = e.getX(index); py = e.getY(index);
                        if (canSend() && snapshot.cameraActive(1)) {
                            try { NativeCommandBridge.look(Math.round(dx), Math.round(dy)); }
                            catch (RuntimeException | LinkageError error) { bridgeError = true; }
                        }
                        return true;
                    case MotionEvent.ACTION_POINTER_UP:
                        if (e.getPointerId(e.getActionIndex()) != pointer) return true;
                        cancel(); return true;
                    case MotionEvent.ACTION_UP:
                        if (pointer >= 0) v.performClick();
                    case MotionEvent.ACTION_CANCEL:
                        cancel(); return true;
                    default: return true;
                }
            }
        }
        LookTouch lookTouch = new LookTouch(); look.setOnTouchListener(lookTouch); cancelGestures.add(lookTouch::cancel);
        LinearLayout.LayoutParams lookParams = new LinearLayout.LayoutParams(0, dp(152), 1); lookParams.leftMargin = dp(10);
        controls.addView(look, lookParams); heldControls.add(look); body.addView(controls, stacked(4));
        body.addView(padRow(hold("左滚", 0x67), tap("复位", 0x68), hold("右滚", 0x69)), stacked(8));
        body.addView(cameraAction("运镜", 13), stacked(4));
        TextView more = smallButton(details ? "收起关键帧" : "关键帧热键");
        more.setOnClickListener(v -> { details = !details; rebuild(); }); body.addView(more, stacked(6));
        if (details) {
            body.addView(padRow(cameraAction("添加帧", 8), cameraAction("播放帧", 9), cameraAction("清空帧", 10)), stacked(4));
            body.addView(padRow(cameraAction("保存机位", 11), cameraAction("读取机位", 12), stopButton()), stacked(4));
        }
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
        class HoldTouch implements View.OnTouchListener {
            int pointer = -1;
            void cancel() { pointer = -1; view.setPressed(false); view.getParent().requestDisallowInterceptTouchEvent(false); }
            @Override public boolean onTouch(View v, MotionEvent event) {
                switch (event.getActionMasked()) {
                    case MotionEvent.ACTION_DOWN:
                        if (!preview && (!canSend() || !snapshot.cameraActive(1))) return false;
                        pointer = event.getPointerId(0); v.setPressed(true);
                        v.getParent().requestDisallowInterceptTouchEvent(true);
                        send(key, NativeCommandBridge.KEY_PRESS); return true;
                    case MotionEvent.ACTION_POINTER_UP:
                        if (event.getPointerId(event.getActionIndex()) != pointer) return true;
                        // The finger owning this button left, even if another remains.
                    case MotionEvent.ACTION_UP:
                    case MotionEvent.ACTION_CANCEL:
                        if (pointer >= 0) send(key, NativeCommandBridge.KEY_RELEASE);
                        cancel(); return true;
                    default: return true;
                }
            }
        }
        HoldTouch touch = new HoldTouch(); view.setOnTouchListener(touch); cancelGestures.add(touch::cancel); return view;
    }

    private void refreshState() {
        if (preview) { status.setText("预览模式 · 不发送任何游戏指令"); }
        else if (RuntimeBootstrap.loaded()) {
            try { snapshot = RuntimeSnapshot.parse(NativeCommandBridge.runtimeStatus()); bridgeError = false; }
            catch (RuntimeException | LinkageError error) { snapshot = RuntimeSnapshot.offline(); bridgeError = true; }
            status.setText(bridgeError ? "控制桥绑定失败 · 请查看日志" : snapshot.summary());
        } else { snapshot = RuntimeSnapshot.offline(); status.setText(RuntimeBootstrap.failure()); }
        for (Tile tile : tiles) tile.update();
        boolean movable = preview || (snapshot.cameraAvailable(1) && snapshot.cameraActive(1));
        if (movementAllowed && !movable) releaseHeldKeys();
        movementAllowed = movable;
        for (View view : heldControls) { view.setEnabled(movable); view.setAlpha(movable ? 1 : 0.35f); }
        cameraSpeed = finiteNumber(snapshot.values.get("camera.speed"), cameraSpeed, ModuleSettings.SPEED_MINIMUM, ModuleSettings.SPEED_MAXIMUM);
        cameraFov = finiteNumber(snapshot.values.get("camera.fov"), cameraFov, ModuleSettings.FOV_MINIMUM, ModuleSettings.FOV_MAXIMUM);
        if (speedSlider != null) speedSlider.update(); if (fovSlider != null) fovSlider.update();
        refreshMmd();
    }
    private View cameraAction(String label, int type) {
        TextView view = smallButton(label);
        view.setOnClickListener(v -> {
            if (preview) { toast("预览模式"); return; }
            if (!canSend() || !snapshot.cameraAvailable(1)) { toast("请先启用自由镜头功能"); return; }
            try {
                if (!NativeCommandBridge.mmdCommand(new JSONObject().put("type", type).toString()))
                    toast("镜头指令未接受，请查看运行状态");
            } catch (JSONException | RuntimeException | LinkageError error) { bridgeError = true; }
        });
        heldControls.add(view); return view;
    }
    private boolean canSend() {
        return !preview && resumed && !closed && !sessionDismissed && host.hasWindowFocus() && RuntimeBootstrap.loaded();
    }
    private void send(int key, int action) {
        if (preview) return;
        if (action == NativeCommandBridge.KEY_RELEASE) {
            if (!heldKeys.remove(key) || !RuntimeBootstrap.loaded()) return;
        } else if (!canSend()) return;
        try {
            if (!NativeCommandBridge.key(key, action)) {
                if (action == NativeCommandBridge.KEY_RELEASE) releaseHeldKeys();
                toast("输入队列已满或键位无效");
            }
            else if (action == NativeCommandBridge.KEY_PRESS) heldKeys.add(key);
        } catch (RuntimeException | LinkageError error) {
            if (action == NativeCommandBridge.KEY_RELEASE) releaseHeldKeys();
            toast("控制桥绑定失败，请查看运行日志");
        }
    }
    private void releaseHeldKeys() {
        for (View view : heldControls) view.setPressed(false);
        for (Runnable cancel : cancelGestures) cancel.run();
        for (DragTouch gesture : dragGestures) gesture.cancel();
        heldKeys.clear(); seeking = false;
        if (timeline != null) timeline.getParent().requestDisallowInterceptTouchEvent(false);
        if (speedSlider != null) speedSlider.tracking = false;
        if (fovSlider != null) fovSlider.tracking = false;
        if (body != null && body.getParent() != null) body.getParent().requestDisallowInterceptTouchEvent(false);
        if (!preview && RuntimeBootstrap.loaded()) {
            try { NativeCommandBridge.releaseKeys(); }
            catch (RuntimeException | LinkageError error) { android.util.Log.e("BetterEndfield.Overlay", "key release bridge failed", error); }
        }
    }
    private void showDiagnostics() {
        releaseHeldKeys();
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
        OverlayGeometry.Layout geometry = geometry();
        place(handle, geometry.handle()); place(panel, geometry.panel());
        LinearLayout.LayoutParams rail = (LinearLayout.LayoutParams) panel.getChildAt(0).getLayoutParams();
        int railWidth = Math.min(dp(76), geometry.panel().width() / 4);
        if (rail.width != railWidth) { rail.width = railWidth; panel.getChildAt(0).setLayoutParams(rail); }
        int padding = Math.min(dp(12), Math.max(0, (geometry.panel().width() - railWidth) / 16));
        body.setPadding(padding, dp(10), padding, dp(12));
        if (panel.getVisibility() == View.VISIBLE) panel.bringToFront(); else handle.bringToFront();
    }
    private OverlayGeometry.Layout geometry() {
        return OverlayGeometry.layout(host.getWidth(), host.getHeight(), host.getPaddingLeft(), host.getPaddingTop(),
                host.getPaddingRight(), host.getPaddingBottom(), dp(8), dp(48), dp(480), dp(560), dp(8), x, y);
    }
    private void place(View view, OverlayGeometry.Rect rect) {
        FrameLayout.LayoutParams params = (FrameLayout.LayoutParams) view.getLayoutParams();
        if (params.width != rect.width() || params.height != rect.height()) {
            params.width = rect.width(); params.height = rect.height(); view.setLayoutParams(params);
        }
        // FrameLayout lays its children out at the padded origin. Translation
        // must be relative to that origin, including before the first layout.
        view.setTranslationX(rect.left() - host.getPaddingLeft());
        view.setTranslationY(rect.top() - host.getPaddingTop());
    }
    private int dp(int n) { return Math.round(n * activity.getResources().getDisplayMetrics().density); }
    private void toast(String text) { Toast.makeText(activity, text, Toast.LENGTH_SHORT).show(); }
    private LinearLayout column() { LinearLayout v = new LinearLayout(activity); v.setOrientation(LinearLayout.VERTICAL); return v; }
    private LinearLayout row() { LinearLayout v = new LinearLayout(activity); v.setOrientation(LinearLayout.HORIZONTAL); v.setGravity(Gravity.CENTER_VERTICAL); return v; }
    private LinearLayout.LayoutParams stacked(int margin) { LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(-1, -2); p.topMargin = dp(margin); return p; }
    private LinearLayout.LayoutParams weighted(int index) { LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(0, -2, 1); if (index > 0) p.leftMargin = dp(5); return p; }
    private LinearLayout.LayoutParams cell(int margin) { LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(0, -2, 1); p.leftMargin = dp(margin); return p; }
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
