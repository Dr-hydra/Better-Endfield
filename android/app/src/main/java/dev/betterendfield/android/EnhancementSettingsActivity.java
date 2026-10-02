package dev.betterendfield.android;

import android.app.Activity;
import android.content.res.Configuration;
import android.graphics.Insets;
import android.os.Build;
import android.os.Bundle;
import android.view.Gravity;
import android.view.WindowInsets;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.TextView;

import java.util.LinkedHashMap;
import java.util.Map;
import java.util.function.BooleanSupplier;
import java.util.function.Consumer;
import java.util.function.DoubleSupplier;

/** Each feature edits only its own settings; the main page is a navigation menu. */
public final class EnhancementSettingsActivity extends Activity {
    static final String EXTRA_FEATURE = "enhancement_feature";
    static final String INTERFACE = "interface";
    static final String CAMERA = "camera";
    static final String DASH = "dash";
    static final String OVERLAY = "overlay";

    private final Map<Integer, SettingRow> rows = new LinkedHashMap<>();
    private final Map<Integer, BooleanSupplier> rowValues = new LinkedHashMap<>();
    private final Map<Integer, ValueSlider> sliders = new LinkedHashMap<>();
    private final Map<Integer, DoubleSupplier> sliderValues = new LinkedHashMap<>();
    private String feature;
    private ScrollView scroll;
    private GameOverlay overlayPreview;
    private int portraitScrollY = -1;
    private int landscapeScrollY = -1;

    @Override protected void onCreate(Bundle state) {
        super.onCreate(state);
        feature = getIntent().getStringExtra(EXTRA_FEATURE);
        if (!INTERFACE.equals(feature) && !CAMERA.equals(feature)
                && !DASH.equals(feature) && !OVERLAY.equals(feature)) {
            finish();
            return;
        }

        scroll = new ScrollView(this);
        scroll.setFillViewport(true);
        scroll.setBackgroundColor(getColor(R.color.app_background));
        LinearLayout page = new LinearLayout(this);
        page.setOrientation(LinearLayout.VERTICAL);
        page.setPadding(dp(16), dp(16), dp(16), dp(24));
        scroll.addView(page, new FrameLayout.LayoutParams(-1, -2, Gravity.CENTER_HORIZONTAL));

        Button back = button("‹ 返回");
        back.setOnClickListener(view -> finish());
        page.addView(back, SectionCard.stacked(this, 0));

        SectionCard card;
        if (INTERFACE.equals(feature)) card = buildInterfaceCard();
        else if (CAMERA.equals(feature)) card = buildCameraCard();
        else if (DASH.equals(feature)) card = buildDashCard();
        else card = buildOverlayCard();
        page.addView(card, SectionCard.stacked(this, 12));
        setContentView(scroll);

        scroll.setOnApplyWindowInsetsListener((view, insets) -> {
            if (Build.VERSION.SDK_INT >= 30) {
                Insets safe = insets.getInsets(WindowInsets.Type.systemBars()
                        | WindowInsets.Type.displayCutout() | WindowInsets.Type.ime());
                view.setPadding(safe.left, safe.top, safe.right, safe.bottom);
            } else {
                view.setPadding(insets.getSystemWindowInsetLeft(), insets.getSystemWindowInsetTop(),
                        insets.getSystemWindowInsetRight(), insets.getSystemWindowInsetBottom());
            }
            return insets;
        });
        if (state != null) {
            portraitScrollY = state.getInt("portrait_scroll_y", -1);
            landscapeScrollY = state.getInt("landscape_scroll_y", -1);
        }
        int orientationY = isLandscape() ? landscapeScrollY : portraitScrollY;
        int restoredY = orientationY >= 0 ? orientationY
                : (state == null ? 0 : state.getInt("scroll_y", 0));
        scroll.addOnLayoutChangeListener((view, left, top, right, bottom,
                oldLeft, oldTop, oldRight, oldBottom) -> {
            int width = Math.max(0, right - left - scroll.getPaddingLeft() - scroll.getPaddingRight());
            FrameLayout.LayoutParams params = (FrameLayout.LayoutParams) page.getLayoutParams();
            int contentWidth = Math.min(width, dp(860));
            if (params.width != contentWidth) {
                params.width = contentWidth;
                page.setLayoutParams(params);
            }
        });
        // Wait until the bounded content has been measured, including after rotation.
        scroll.post(() -> scroll.post(() -> scroll.scrollTo(0, restoredY)));
        scroll.requestApplyInsets();
    }

    @Override protected void onResume() {
        super.onResume();
        refreshControls();
    }

    @Override protected void onPause() {
        removeOverlayPreview();
        super.onPause();
    }

    @Override protected void onDestroy() {
        removeOverlayPreview();
        super.onDestroy();
    }

    @Override protected void onSaveInstanceState(Bundle state) {
        if (scroll != null) {
            int y = scroll.getScrollY();
            if (isLandscape()) landscapeScrollY = y;
            else portraitScrollY = y;
            state.putInt("scroll_y", y);
        }
        state.putInt("portrait_scroll_y", portraitScrollY);
        state.putInt("landscape_scroll_y", landscapeScrollY);
        super.onSaveInstanceState(state);
    }

    private boolean isLandscape() {
        return getResources().getConfiguration().orientation == Configuration.ORIENTATION_LANDSCAPE;
    }

    private SectionCard buildInterfaceCard() {
        SectionCard card = card(getString(R.string.ui_card_title));
        addToggle(card, R.string.ui_hide_uid, () -> ModuleSettings.isHideUidEnabled(this),
                checked -> saveInterfaceSetting(R.string.ui_hide_uid, checked));
        addToggle(card, R.string.ui_hide_hud, () -> ModuleSettings.isHideHudEnabled(this),
                checked -> saveInterfaceSetting(R.string.ui_hide_hud, checked));
        addToggle(card, R.string.ui_pc, () -> ModuleSettings.isPcUiEnabled(this),
                checked -> ModuleSettings.setPcUiEnabled(this, checked));
        return card;
    }

    private SectionCard buildCameraCard() {
        SectionCard card = card(getString(R.string.camera_card_title));
        addCameraToggle(card, R.string.camera_dither, () -> ModuleSettings.isDisableDitherEnabled(this));
        addCameraToggle(card, R.string.camera_free, () -> ModuleSettings.isFreeCameraEnabled(this));
        addToggle(card, R.string.camera_follow_character,
                () -> ModuleSettings.isCameraFollowCharacter(this),
                checked -> ModuleSettings.setCameraFollowCharacter(this, checked));
        addToggle(card, R.string.camera_global_fov,
                () -> ModuleSettings.isGlobalFovEnabled(this),
                checked -> ModuleSettings.setGlobalFovEnabled(this, checked));
        addCameraToggle(card, R.string.camera_pause, () -> ModuleSettings.isWorldPauseEnabled(this));
        addCameraToggle(card, R.string.camera_first_person, () -> ModuleSettings.isFirstPersonEnabled(this));
        addCameraToggle(card, R.string.camera_hide_head, () -> ModuleSettings.isFirstPersonHideHead(this));
        addCameraToggle(card, R.string.camera_fill_neck, () -> ModuleSettings.isFirstPersonFillNeck(this));
        addCameraSlider(card, R.string.camera_speed_label, "",
                ModuleSettings.SPEED_MINIMUM, ModuleSettings.SPEED_MAXIMUM,
                () -> ModuleSettings.parse(ModuleSettings.getCameraSpeed(this), 5));
        addCameraSlider(card, R.string.camera_fov_label, getString(R.string.degree_suffix),
                ModuleSettings.FOV_MINIMUM, ModuleSettings.FOV_MAXIMUM,
                () -> ModuleSettings.parse(ModuleSettings.getCameraFieldOfView(this), 60));
        addCameraSlider(card, R.string.camera_fp_fov_label, getString(R.string.degree_suffix),
                ModuleSettings.FOV_MINIMUM, ModuleSettings.FOV_MAXIMUM,
                () -> ModuleSettings.parse(ModuleSettings.getFirstPersonFieldOfView(this), 75));
        addCameraSlider(card, R.string.camera_global_fov_value, getString(R.string.degree_suffix),
                5, 150, () -> ModuleSettings.parse(ModuleSettings.getGlobalFieldOfView(this), 60));
        addCameraSlider(card, R.string.camera_fp_side_limit, getString(R.string.degree_suffix), 30, 170,
                () -> ModuleSettings.parse(ModuleSettings.getFirstPersonSideLookLimit(this), 90));
        addCameraSlider(card, R.string.camera_fp_turn_speed, getString(R.string.degree_per_second), 30, 1080,
                () -> ModuleSettings.parse(ModuleSettings.getFirstPersonTurnSpeed(this), 360));
        return card;
    }

    private SectionCard buildDashCard() {
        SectionCard card = card("持续冲刺");
        addDashToggle(card, R.string.dash_enable, () -> ModuleSettings.isSustainedDashEnabled(this));
        addDashToggle(card, R.string.dash_aglina, () -> ModuleSettings.isDashCharacterEnabled(this, "aglina"));
        addDashToggle(card, R.string.dash_liino, () -> ModuleSettings.isDashCharacterEnabled(this, "liino"));
        addDashToggle(card, R.string.dash_liino_clean, () -> ModuleSettings.isLiinoCleanDashEnabled(this));
        return card;
    }

    private SectionCard buildOverlayCard() {
        SectionCard card = card(getString(R.string.overlay_card_title));
        addToggle(card, R.string.overlay_enable, () -> ModuleSettings.isOverlayEnabled(this),
                checked -> ModuleSettings.setOverlayEnabled(this, checked));
        Button preview = button(getString(R.string.overlay_preview));
        preview.setOnClickListener(view -> {
            removeOverlayPreview();
            overlayPreview = new GameOverlay(this, true);
        });
        card.add(preview);
        return card;
    }

    private SectionCard card(String title) {
        setTitle(title);
        return new SectionCard(this, "", title, "");
    }

    private Button button(String title) {
        Button button = new Button(this);
        button.setText(title);
        button.setTextSize(15);
        button.setAllCaps(false);
        button.setMinHeight(0);
        button.setMinimumHeight(dp(52));
        button.setPadding(dp(14), dp(12), dp(14), dp(12));
        button.setTextColor(getColor(R.color.text_primary));
        button.setBackgroundResource(R.drawable.bg_ghost_button);
        return button;
    }

    private void addToggle(SectionCard card, int title, BooleanSupplier current,
            Consumer<Boolean> changed) {
        SettingRow row = new SettingRow(this, getString(title), "", null);
        row.initialize(current.getAsBoolean());
        row.onChanged((button, checked) -> {
            changed.accept(checked);
            refreshControls();
        });
        rows.put(title, row);
        rowValues.put(title, current);
        card.add(row);
    }

    private void addCameraToggle(SectionCard card, int title, BooleanSupplier current) {
        addToggle(card, title, current, checked -> saveCameraSetting(title, checked, 0));
    }

    private void addDashToggle(SectionCard card, int title, BooleanSupplier current) {
        addToggle(card, title, current, checked -> saveDashSetting(title, checked));
    }

    private void addCameraSlider(SectionCard card, int title, String unit, float minimum,
            float maximum, DoubleSupplier current) {
        ValueSlider slider = new ValueSlider(this, getString(title), unit, minimum, maximum);
        slider.setValue((float) current.getAsDouble());
        slider.onChanged(() -> {
            saveCameraSetting(title, false, slider.getValue());
            refreshControls();
        });
        sliders.put(title, slider);
        sliderValues.put(title, current);
        card.add(slider);
    }

    private void refreshControls() {
        for (Map.Entry<Integer, SettingRow> entry : rows.entrySet()) {
            entry.getValue().initialize(rowValues.get(entry.getKey()).getAsBoolean());
        }
        for (Map.Entry<Integer, ValueSlider> entry : sliders.entrySet()) {
            entry.getValue().setValue((float) sliderValues.get(entry.getKey()).getAsDouble());
        }
        if (CAMERA.equals(feature)) {
            boolean free = ModuleSettings.isFreeCameraEnabled(this);
            boolean firstPerson = ModuleSettings.isFirstPersonEnabled(this);
            sliders.get(R.string.camera_speed_label).setAvailable(free);
            sliders.get(R.string.camera_fov_label).setAvailable(free);
            rows.get(R.string.camera_hide_head).setAvailable(firstPerson);
            rows.get(R.string.camera_fill_neck).setAvailable(
                    firstPerson && ModuleSettings.isFirstPersonHideHead(this));
            sliders.get(R.string.camera_fp_fov_label).setAvailable(firstPerson);
            sliders.get(R.string.camera_fp_side_limit).setAvailable(firstPerson);
            sliders.get(R.string.camera_fp_turn_speed).setAvailable(firstPerson);
            // Pausing the world remains independent of free camera.
        } else if (DASH.equals(feature)) {
            boolean dash = ModuleSettings.isSustainedDashEnabled(this);
            rows.get(R.string.dash_aglina).setAvailable(dash);
            rows.get(R.string.dash_liino).setAvailable(dash);
            rows.get(R.string.dash_liino_clean).setAvailable(
                    dash && ModuleSettings.isDashCharacterEnabled(this, "liino"));
        }
    }

    // Read the latest group immediately before saving. Never serialize untouched
    // sliders from their rounded UI values, or copy stale switches back to storage.
    private void saveInterfaceSetting(int option, boolean checked) {
        ModuleSettings.setInterfaceSettings(this,
                option == R.string.ui_hide_uid ? checked : ModuleSettings.isHideUidEnabled(this),
                option == R.string.ui_hide_hud ? checked : ModuleSettings.isHideHudEnabled(this));
    }

    private void saveCameraSetting(int option, boolean checked, double value) {
        if (option == R.string.camera_global_fov_value) {
            ModuleSettings.setGlobalFieldOfView(this, value);
            return;
        }
        ModuleSettings.setCameraSettings(this,
                option == R.string.camera_dither ? checked : ModuleSettings.isDisableDitherEnabled(this),
                option == R.string.camera_free ? checked : ModuleSettings.isFreeCameraEnabled(this),
                option == R.string.camera_pause ? checked : ModuleSettings.isWorldPauseEnabled(this),
                option == R.string.camera_first_person ? checked : ModuleSettings.isFirstPersonEnabled(this),
                option == R.string.camera_hide_head ? checked : ModuleSettings.isFirstPersonHideHead(this),
                option == R.string.camera_fill_neck ? checked : ModuleSettings.isFirstPersonFillNeck(this),
                option == R.string.camera_speed_label ? value
                        : ModuleSettings.parse(ModuleSettings.getCameraSpeed(this), 5),
                option == R.string.camera_fov_label ? value
                        : ModuleSettings.parse(ModuleSettings.getCameraFieldOfView(this), 60),
                option == R.string.camera_fp_fov_label ? value
                        : ModuleSettings.parse(ModuleSettings.getFirstPersonFieldOfView(this), 75),
                option == R.string.camera_fp_side_limit ? value
                        : ModuleSettings.parse(ModuleSettings.getFirstPersonSideLookLimit(this), 90),
                option == R.string.camera_fp_turn_speed ? value
                        : ModuleSettings.parse(ModuleSettings.getFirstPersonTurnSpeed(this), 360));
    }

    private void saveDashSetting(int option, boolean checked) {
        ModuleSettings.setSustainedDashSettings(this,
                option == R.string.dash_enable ? checked : ModuleSettings.isSustainedDashEnabled(this),
                option == R.string.dash_liino_clean ? checked : ModuleSettings.isLiinoCleanDashEnabled(this),
                option == R.string.dash_aglina ? checked : ModuleSettings.isDashCharacterEnabled(this, "aglina"),
                option == R.string.dash_liino ? checked : ModuleSettings.isDashCharacterEnabled(this, "liino"));
    }

    private void removeOverlayPreview() {
        if (overlayPreview == null) return;
        overlayPreview.remove();
        overlayPreview = null;
    }

    private int dp(int value) {
        return Math.round(value * getResources().getDisplayMetrics().density);
    }
}
