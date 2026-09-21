package dev.betterendfield.android;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.drawable.GradientDrawable;
import android.os.Bundle;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.Spinner;
import android.widget.Switch;
import android.widget.TextView;

import java.util.LinkedHashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;

public final class MainActivity extends Activity {
    static final String EXTRA_PAGE = "settings_page";
    private static final int CUSTOM_MODEL_PAGE = 4;
    private static final int ENHANCEMENT_PAGE = 2;
    private static final int DIAGNOSTICS_PAGE = 3;
    private static final int[] THEME_COLOR_VIEW_IDS = {
            R.id.theme_color_amber,
            R.id.theme_color_cyan,
            R.id.theme_color_green,
            R.id.theme_color_coral,
            R.id.theme_color_magenta,
            R.id.theme_color_white
    };
    private static final String[] THEME_COLORS = {
            "#FFC928", "#35C8E8", "#41C77A", "#F0645A", "#D866B7", "#F2F2F2"
    };
    private static final String[] LANGUAGE_VALUES = {
            "FollowGlobal", "Chinese", "English", "Japanese", "Korean"
    };
    private static final String[] LANGUAGE_LABELS = {
            "跟随游戏", "中文", "English", "日本語", "한국어"
    };

    private final Map<String, Spinner> ruleSpinners = new LinkedHashMap<>();
    private TextView status;
    private boolean initializingRules = true;
    private String lastSavedRules = "";

    private ModelPresetIndex modelIndex;
    private Spinner modelCharacter;
    private Spinner modelAction;
    private Switch modelEnabled;
    private Switch modelFinalLoop;
    private Switch modelForceLoop;
    private Switch modelCrossfade;
    private Switch logoEnabled;
    private EditText modelScale;
    private EditText modelLoopStart;
    private EditText modelLoopEnd;
    private EditText modelCrossfadeDuration;
    private EditText logoColor;
    private ColorWheelView logoColorWheel;
    private View logoColorPreview;
    private TextView modelSelectionStatus;
    private boolean initializingModel = true;

    private int currentPage;
    private GameOverlay overlayPreview;

    // The enhancement page is assembled in buildEnhancementPage() from
    // SectionCard/SettingRow/ValueSlider, so its controls are fields rather than
    // findViewById lookups.
    private SectionCard interfaceCard;
    private SectionCard cameraCard;
    private SectionCard dashCard;
    private SettingRow hideUidRow;
    private SettingRow hideHudRow;
    private SettingRow ditherRow;
    private SettingRow freeCameraRow;
    private SettingRow worldPauseRow;
    private SettingRow firstPersonRow;
    private SettingRow hideHeadRow;
    private SettingRow fillNeckRow;
    private ValueSlider cameraSpeed;
    private ValueSlider cameraFov;
    private ValueSlider firstPersonFov;
    private SettingRow dashRow;
    private SettingRow dashAglinaRow;
    private SettingRow dashLiinoRow;
    private SettingRow dashLiinoCleanRow;
    private SettingRow overlayRow;
    private boolean populatingEnhancement = true;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        String requestedPage = getIntent().getStringExtra(EXTRA_PAGE);
        currentPage = savedInstanceState == null
                ? ("custom_model".equals(requestedPage) ? CUSTOM_MODEL_PAGE
                        : ("enhancement".equals(requestedPage) ? ENHANCEMENT_PAGE : 0))
                : savedInstanceState.getInt("page", 0);
        currentPage = Math.max(0, Math.min(CUSTOM_MODEL_PAGE, currentPage));

        status = findViewById(R.id.restart_status);
        setupPageNavigation();
        setupModelPage();
        setupCustomModelPage();
        setupVoicePage();
        setupEnhancementPage();
        setupDiagnosticsPage();
        applyResponsiveShell();
    }

    private void setupCustomModelPage() {
        findViewById(R.id.install_bem).setOnClickListener(view ->
                startActivity(new Intent(this, BemInstallActivity.class)));
    }

    private void showOverlayPreview() {
        if (overlayPreview != null) overlayPreview.remove();
        overlayPreview = new GameOverlay(this, true);
    }

    @Override
    protected void onResume() {
        super.onResume();
        if (overlayRow != null) overlayRow.initialize(ModuleSettings.isOverlayEnabled(this));
        setupDiagnosticsPage();
    }

    @Override protected void onSaveInstanceState(Bundle state) {
        state.putInt("page", currentPage);
        super.onSaveInstanceState(state);
    }

    private void setupPageNavigation() {
        View[] sections = {
                findViewById(R.id.model_section),
                findViewById(R.id.voice_section),
                findViewById(R.id.enhancement_section),
                findViewById(R.id.diagnostics_section),
                findViewById(R.id.custom_model_section)
        };
        View[] buttons = {
                findViewById(R.id.show_model_button),
                findViewById(R.id.show_voice_button),
                findViewById(R.id.show_enhancement_button),
                findViewById(R.id.show_diagnostics_button),
                findViewById(R.id.show_custom_model_button)
        };
        android.widget.ScrollView scroll = findViewById(R.id.responsive_scroll);
        for (int index = 0; index < buttons.length; ++index) {
            final int page = index;
            buttons[index].setOnClickListener(view -> {
                currentPage = page;
                // The diagnostics page reports which modules the game will load,
                // which the enhancement page can have changed since it was built.
                if (page == DIAGNOSTICS_PAGE) setupDiagnosticsPage();
                showPage(sections, buttons, page);
                scroll.post(() -> scroll.smoothScrollTo(0, 0));
            });
        }
        showPage(sections, buttons, currentPage);
    }

    private static void showPage(View[] sections, View[] buttons, int page) {
        for (int index = 0; index < sections.length; ++index) {
            sections[index].setVisibility(index == page ? View.VISIBLE : View.GONE);
            buttons[index].setSelected(index == page);
        }
    }

    private void setupModelPage() {
        TextView tableStatus = findViewById(R.id.model_table_status);
        modelSelectionStatus = findViewById(R.id.model_selection_status);
        modelCharacter = findViewById(R.id.model_character);
        modelAction = findViewById(R.id.model_action);
        modelEnabled = findViewById(R.id.model_enabled);
        modelFinalLoop = findViewById(R.id.model_final_loop);
        modelForceLoop = findViewById(R.id.model_force_loop);
        modelCrossfade = findViewById(R.id.model_crossfade);
        logoEnabled = findViewById(R.id.logo_enabled);
        modelScale = findViewById(R.id.model_scale);
        modelLoopStart = findViewById(R.id.model_loop_start);
        modelLoopEnd = findViewById(R.id.model_loop_end);
        modelCrossfadeDuration = findViewById(R.id.model_crossfade_duration);
        logoColor = findViewById(R.id.logo_color);
        logoColorWheel = findViewById(R.id.logo_color_wheel);
        logoColorPreview = findViewById(R.id.logo_color_preview);

        try {
            modelIndex = ModelPresetIndex.load(this);
            ArrayAdapter<ModelPresetIndex.Character> characters = new ArrayAdapter<>(
                    this,
                    android.R.layout.simple_spinner_item,
                    modelIndex.characters());
            characters.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            modelCharacter.setAdapter(characters);

            modelEnabled.setChecked(ModuleSettings.isModelEnabled(this));
            modelFinalLoop.setChecked(ModuleSettings.isModelFinalLoop(this));
            modelForceLoop.setChecked(ModuleSettings.isModelForceLoop(this));
            modelCrossfade.setChecked(ModuleSettings.isModelCrossfade(this));
            if (modelCrossfade.isChecked()) {
                modelFinalLoop.setChecked(true);
            }
            logoEnabled.setChecked(ModuleSettings.isLogoEnabled(this));
            modelScale.setText(ModuleSettings.getModelScale(this));
            modelLoopStart.setText(ModuleSettings.getModelLoopStart(this));
            modelLoopEnd.setText(ModuleSettings.getModelLoopEnd(this));
            modelCrossfadeDuration.setText(
                    ModuleSettings.getModelCrossfadeDuration(this));
            logoColor.setText(ModuleSettings.getLogoColor(this));
            installThemeColorPalette();
            updateThemeColorPalette(logoColor.getText().toString());

            int characterPosition = findCharacterPosition(
                    ModuleSettings.getModelCharacter(this));
            modelCharacter.setSelection(characterPosition, false);
            refreshActionOptions(ModuleSettings.getModelAction(this));
            updateModelSelectionStatus();
            tableStatus.setText(getString(
                    R.string.model_table_ready,
                    modelIndex.characters().size(),
                    modelIndex.actionCount()));

            installModelListeners();
            initializingModel = false;
        } catch (Exception error) {
            tableStatus.setText(getString(R.string.model_table_failed, error.getMessage()));
            modelEnabled.setEnabled(false);
            logoEnabled.setEnabled(false);
        }
    }

    private void installModelListeners() {
        modelCharacter.setOnItemSelectedListener(
                new android.widget.AdapterView.OnItemSelectedListener() {
                    @Override
                    public void onItemSelected(
                            android.widget.AdapterView<?> parent,
                            View view,
                            int position,
                            long id) {
                        if (initializingModel) return;
                        refreshActionOptions("");
                        saveModelSettings();
                    }

                    @Override
                    public void onNothingSelected(android.widget.AdapterView<?> parent) {}
                });
        modelAction.setOnItemSelectedListener(simpleModelSelectionListener());
        modelEnabled.setOnCheckedChangeListener((button, checked) -> saveModelSettings());
        modelFinalLoop.setOnCheckedChangeListener((button, checked) -> {
            if (!checked && modelCrossfade.isChecked()) {
                modelCrossfade.setChecked(false);
            }
            saveModelSettings();
        });
        modelForceLoop.setOnCheckedChangeListener((button, checked) -> saveModelSettings());
        modelCrossfade.setOnCheckedChangeListener((button, checked) -> {
            if (checked && !modelFinalLoop.isChecked()) {
                modelFinalLoop.setChecked(true);
            }
            saveModelSettings();
        });
        logoEnabled.setOnCheckedChangeListener((button, checked) -> saveModelSettings());
        modelScale.setOnFocusChangeListener((view, hasFocus) -> {
            if (!hasFocus) saveModelSettings();
        });
        modelLoopStart.setOnFocusChangeListener((view, hasFocus) -> {
            if (!hasFocus) saveModelSettings();
        });
        modelLoopEnd.setOnFocusChangeListener((view, hasFocus) -> {
            if (!hasFocus) saveModelSettings();
        });
        modelCrossfadeDuration.setOnFocusChangeListener((view, hasFocus) -> {
            if (!hasFocus) saveModelSettings();
        });
        logoColor.setOnFocusChangeListener((view, hasFocus) -> {
            if (!hasFocus) saveModelSettings();
        });
        logoColorWheel.setOnColorChangedListener((rgb, committed) -> {
            String color = String.format(Locale.ROOT, "#%06X", rgb);
            logoColor.setText(color);
            updateThemeColorPalette(color);
            if (committed) saveModelSettings();
        });
        findViewById(R.id.save_model_settings).setOnClickListener(
                view -> saveModelSettings());
    }

    private android.widget.AdapterView.OnItemSelectedListener simpleModelSelectionListener() {
        return new android.widget.AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(
                    android.widget.AdapterView<?> parent,
                    View view,
                    int position,
                    long id) {
                if (!initializingModel) saveModelSettings();
            }

            @Override
            public void onNothingSelected(android.widget.AdapterView<?> parent) {}
        };
    }

    private int findCharacterPosition(String id) {
        List<ModelPresetIndex.Character> characters = modelIndex.characters();
        for (int index = 0; index < characters.size(); ++index) {
            if (characters.get(index).id().equalsIgnoreCase(id)) return index;
        }
        for (int index = 0; index < characters.size(); ++index) {
            if ("chr_0013_aglina".equalsIgnoreCase(characters.get(index).id())) return index;
        }
        return 0;
    }

    private void refreshActionOptions(String preferredAction) {
        ModelPresetIndex.Character character = selectedCharacter();
        if (character == null) return;
        ArrayAdapter<ModelPresetIndex.Action> actions = new ArrayAdapter<>(
                this,
                android.R.layout.simple_spinner_item,
                character.actions());
        actions.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        modelAction.setAdapter(actions);

        String desired = preferredAction == null || preferredAction.isEmpty()
                ? character.defaultActionId() : preferredAction;
        int selected = 0;
        for (int index = 0; index < character.actions().size(); ++index) {
            if (character.actions().get(index).id().equalsIgnoreCase(desired)) {
                selected = index;
                break;
            }
        }
        modelAction.setSelection(selected, false);
        updateModelSelectionStatus();
    }

    private ModelPresetIndex.Character selectedCharacter() {
        return modelCharacter == null || modelCharacter.getSelectedItem() == null
                ? null : (ModelPresetIndex.Character) modelCharacter.getSelectedItem();
    }

    private ModelPresetIndex.Action selectedAction() {
        return modelAction == null || modelAction.getSelectedItem() == null
                ? null : (ModelPresetIndex.Action) modelAction.getSelectedItem();
    }

    private void updateModelSelectionStatus() {
        ModelPresetIndex.Character character = selectedCharacter();
        ModelPresetIndex.Action action = selectedAction();
        if (character != null && action != null) {
            modelSelectionStatus.setText(getString(
                    R.string.model_selection_ready,
                    character.id(),
                    action.id()));
        }
    }

    private void saveModelSettings() {
        if (initializingModel || modelIndex == null) return;
        ModelPresetIndex.Character character = selectedCharacter();
        ModelPresetIndex.Action action = selectedAction();
        if (character == null || action == null) return;

        String scaleText = modelScale.getText().toString().trim();
        double scale;
        try {
            scale = Double.parseDouble(scaleText);
            if (!Double.isFinite(scale) || scale < 0.05 || scale > 20.0) {
                throw new NumberFormatException("范围 0.05–20");
            }
        } catch (NumberFormatException error) {
            modelSelectionStatus.setText(getString(
                    R.string.model_settings_invalid, "模型缩放应为 0.05–20"));
            return;
        }

        double loopStart;
        double loopEnd;
        double crossfadeDuration;
        try {
            loopStart = Double.parseDouble(modelLoopStart.getText().toString().trim());
            loopEnd = Double.parseDouble(modelLoopEnd.getText().toString().trim());
            crossfadeDuration = Double.parseDouble(
                    modelCrossfadeDuration.getText().toString().trim());
            if (!Double.isFinite(loopStart) || loopStart < 0.0 || loopStart > 30.0 ||
                    !Double.isFinite(loopEnd) || loopEnd < 0.05 || loopEnd > 60.0 ||
                    loopEnd < loopStart + 0.05 ||
                    !Double.isFinite(crossfadeDuration) ||
                    crossfadeDuration < 0.01 || crossfadeDuration > 10.0 ||
                    crossfadeDuration > (loopEnd - loopStart) * 0.5) {
                throw new NumberFormatException();
            }
        } catch (NumberFormatException error) {
            modelSelectionStatus.setText(getString(
                    R.string.model_settings_invalid,
                    "循环区间或混合时长无效"));
            return;
        }

        String color = logoColor.getText().toString().trim().toUpperCase(Locale.ROOT);
        if (!color.matches("#[0-9A-F]{6}")) {
            modelSelectionStatus.setText(getString(
                    R.string.model_settings_invalid, "主题色应为 #RRGGBB"));
            return;
        }
        logoColor.setText(color);
        updateThemeColorPalette(color);

        boolean enableModel = modelEnabled.isChecked();
        boolean enableLogo = logoEnabled.isChecked();
        String configuration = enableModel || enableLogo
                ? buildModelConfiguration(character, action, scale, color,
                        loopStart, loopEnd, crossfadeDuration)
                : "";
        ModuleSettings.setModelSettings(
                this,
                enableModel,
                character.id(),
                action.id(),
                modelFinalLoop.isChecked(),
                modelForceLoop.isChecked(),
                modelCrossfade.isChecked(),
                number(loopStart),
                number(loopEnd),
                number(crossfadeDuration),
                number(scale),
                enableLogo,
                color,
                configuration);
        modelScale.setText(number(scale));
        modelLoopStart.setText(number(loopStart));
        modelLoopEnd.setText(number(loopEnd));
        modelCrossfadeDuration.setText(number(crossfadeDuration));
        updateModelSelectionStatus();
        status.setText(R.string.model_restart_required);
    }

    private void installThemeColorPalette() {
        for (int index = 0; index < THEME_COLOR_VIEW_IDS.length; ++index) {
            View swatch = findViewById(THEME_COLOR_VIEW_IDS[index]);
            String color = THEME_COLORS[index];
            swatch.setOnClickListener(view -> {
                logoColor.setText(color);
                updateThemeColorPalette(color);
                saveModelSettings();
            });
        }
    }

    private void updateThemeColorPalette(String selectedColor) {
        for (int index = 0; index < THEME_COLOR_VIEW_IDS.length; ++index) {
            GradientDrawable background = new GradientDrawable();
            background.setShape(GradientDrawable.RECTANGLE);
            background.setColor(Color.parseColor(THEME_COLORS[index]));
            background.setCornerRadius(dp(6));
            int strokeColor = THEME_COLORS[index].equalsIgnoreCase(selectedColor)
                    ? Color.WHITE : Color.TRANSPARENT;
            background.setStroke(dp(2), strokeColor);
            findViewById(THEME_COLOR_VIEW_IDS[index]).setBackground(background);
        }
        if (!selectedColor.matches("#[0-9A-Fa-f]{6}")) {
            return;
        }
        int rgb = Color.parseColor(selectedColor);
        if (logoColorWheel.getColor() != (rgb & 0xFFFFFF)) {
            logoColorWheel.setColor(rgb);
        }
        GradientDrawable preview = new GradientDrawable();
        preview.setShape(GradientDrawable.RECTANGLE);
        preview.setColor(rgb);
        preview.setCornerRadius(dp(8));
        preview.setStroke(dp(1), getColor(R.color.surface_border));
        logoColorPreview.setBackground(preview);
    }

    private String buildModelConfiguration(
            ModelPresetIndex.Character character,
            ModelPresetIndex.Action action,
            double scale,
            String color,
            double loopStart,
            double loopEnd,
            double crossfadeDuration) {
        StringBuilder text = new StringBuilder();
        append(text, "schema_version=5");
        append(text, "enabled=true");
        append(text, "model_replacement_enabled=" + modelEnabled.isChecked());
        append(text, "logo_theme_enabled=" + logoEnabled.isChecked());
        append(text, "logo_theme_color=" + color);
        append(text, "diagnostics=true");
        append(text, "character=" + character.id());
        append(text, "final_action=" + action.id());
        append(text, "model_path=" + character.modelPath());
        append(text, "model_path_hash=" + character.modelPathHash());
        append(text, "model_bundle_hash=" + character.modelBundleHash());
        appendAsset(text, "sit_loop", character.sitLoop());
        appendAsset(text, "sit_special", character.sitSpecial());
        appendAsset(text, "sit_to_walk", character.sitToWalk());
        append(text, "final_path=" + action.path());
        append(text, "final_path_hash=" + action.pathHash());
        append(text, "final_label=" + action.id());
        append(text, "final_native_loop=" + action.nativeLoop());
        append(text, "start_yaw=-120");
        append(text, "turn_duration=3.0333335");
        append(text, "scale=" + number(scale));
        append(text, "forward_lean_sample=1");
        append(text, "sit_loop_speed=1");
        append(text, "sit_special_speed=1");
        append(text, "sit_to_walk_speed=1");
        append(text, "final_speed=1");
        append(text, "final_loop=" + modelFinalLoop.isChecked());
        append(text, "force_loop=" + modelForceLoop.isChecked());
        append(text, "use_crossfade=" + modelCrossfade.isChecked());
        append(text, "loop_start=" + number(loopStart));
        append(text, "loop_end=" + number(loopEnd));
        append(text, "crossfade_duration=" + number(crossfadeDuration));
        return text.toString();
    }

    private static void appendAsset(
            StringBuilder text, String prefix, ModelPresetIndex.Asset asset) {
        append(text, prefix + "_path=" + asset.path());
        append(text, prefix + "_path_hash=" + asset.pathHash());
        append(text, prefix + "_label=" + asset.label());
    }

    private static void append(StringBuilder text, String line) {
        text.append(line).append('\n');
    }

    private static String number(double value) {
        String result = String.format(Locale.ROOT, "%.8f", value);
        return result.replaceFirst("0+$", "").replaceFirst("\\.$", "");
    }

    private void setupVoicePage() {
        LinearLayout rows = findViewById(R.id.voice_rule_rows);
        TextView tableStatus = findViewById(R.id.voice_table_status);
        if (BuildConfig.DEBUG && getIntent().hasExtra("voice_rules")) {
            ModuleSettings.setVoiceRules(
                    this, getIntent().getStringExtra("voice_rules"));
        }

        try {
            VoiceCatalogIndex index = VoiceCatalogIndex.load(this);
            lastSavedRules = ModuleSettings.getVoiceRules(this);
            Map<String, String> configured = parseRules(lastSavedRules);
            for (VoiceCatalogIndex.CharacterChoice choice : index.characters()) {
                addRuleRow(rows, choice, configured.getOrDefault(
                        choice.characterId(), "FollowGlobal"));
            }
            initializingRules = false;
            tableStatus.setText(getString(
                    R.string.voice_table_ready,
                    index.characters().size() - 1,
                    index.catalogCount()));
        } catch (Exception error) {
            tableStatus.setText(getString(
                    R.string.voice_table_failed, error.getMessage()));
            status.setText(R.string.voice_table_unavailable);
        }
    }

    private void addRuleRow(
            LinearLayout parent,
            VoiceCatalogIndex.CharacterChoice choice,
            String selectedLanguage) {
        LinearLayout row = new LinearLayout(this);
        row.setOrientation(LinearLayout.HORIZONTAL);
        row.setGravity(android.view.Gravity.CENTER_VERTICAL);
        row.setBackgroundResource(R.drawable.bg_voice_row);
        row.setPadding(dp(14), dp(10), dp(10), dp(10));

        TextView label = new TextView(this);
        label.setText(choice.displayName());
        label.setTextSize(14);
        label.setTextColor(getColor(R.color.text_primary));
        row.addView(label, new LinearLayout.LayoutParams(
                0, ViewGroup.LayoutParams.WRAP_CONTENT, 1.0f));

        Spinner spinner = new Spinner(this);
        ArrayAdapter<String> adapter = new ArrayAdapter<>(
                this,
                android.R.layout.simple_spinner_item,
                LANGUAGE_LABELS);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner.setAdapter(adapter);
        spinner.setBackgroundResource(R.drawable.bg_input);
        spinner.setPadding(dp(10), 0, dp(4), 0);
        spinner.setSelection(languagePosition(selectedLanguage), false);
        spinner.setContentDescription(choice.displayName());
        spinner.setOnItemSelectedListener(new android.widget.AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(
                    android.widget.AdapterView<?> parent,
                    View view,
                    int position,
                    long id) {
                if (!initializingRules) saveRules();
            }

            @Override
            public void onNothingSelected(android.widget.AdapterView<?> parent) {}
        });
        ruleSpinners.put(choice.characterId(), spinner);
        row.addView(spinner, new LinearLayout.LayoutParams(
                dp(132), ViewGroup.LayoutParams.WRAP_CONTENT));
        spinner.setMinimumHeight(dp(48));
        row.addOnLayoutChangeListener((v,l,t,r,b,ol,ot,or,ob) -> {
            boolean narrow = r-l < dp(340) || getResources().getConfiguration().fontScale >= 1.3f;
            int orientation = narrow ? LinearLayout.VERTICAL : LinearLayout.HORIZONTAL;
            if (row.getOrientation() == orientation) return;
            row.setOrientation(orientation);
            label.setLayoutParams(new LinearLayout.LayoutParams(narrow ? -1 : 0, -2, narrow ? 0 : 1));
            spinner.setLayoutParams(new LinearLayout.LayoutParams(narrow ? -1 : dp(132), -2));
        });
        LinearLayout.LayoutParams rowParams = new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.MATCH_PARENT,
                ViewGroup.LayoutParams.WRAP_CONTENT);
        rowParams.bottomMargin = dp(8);
        parent.addView(row, rowParams);
    }

    private void saveRules() {
        if (ruleSpinners.isEmpty()) return;
        StringBuilder rules = new StringBuilder();
        for (Map.Entry<String, Spinner> entry : ruleSpinners.entrySet()) {
            int position = entry.getValue().getSelectedItemPosition();
            if (position <= 0 || position >= LANGUAGE_VALUES.length) continue;
            if (rules.length() > 0) rules.append(';');
            rules.append(entry.getKey()).append(':').append(LANGUAGE_VALUES[position]);
        }
        String serialized = rules.toString();
        if (serialized.equals(lastSavedRules)) return;
        ModuleSettings.setVoiceRules(this, serialized);
        lastSavedRules = serialized;
        status.setText(R.string.restart_required);
    }

    private static Map<String, String> parseRules(String value) {
        Map<String, String> rules = new LinkedHashMap<>();
        if (value == null || value.isEmpty()) return rules;
        for (String item : value.split(";")) {
            int separator = item.indexOf(':');
            if (separator > 0 && separator + 1 < item.length()) {
                rules.put(item.substring(0, separator), item.substring(separator + 1));
            }
        }
        return rules;
    }

    private static int languagePosition(String value) {
        for (int index = 0; index < LANGUAGE_VALUES.length; ++index) {
            if (LANGUAGE_VALUES[index].equalsIgnoreCase(value)) return index;
        }
        return 0;
    }

    private void setupEnhancementPage() {
        // Rewrites every module configuration from the stored switches once per
        // launch. That is what carries a pre-3.3 "enhancement" choice over to the
        // ui/camera modules, and what publishes a key added by an update without
        // asking the user to re-toggle anything.
        ModuleSettings.republishConfigurations(this);
        buildEnhancementPage();
    }

    private void buildEnhancementPage() {
        LinearLayout page = findViewById(R.id.enhancement_section);
        page.removeAllViews();
        populatingEnhancement = true;

        TextView intro = new TextView(this);
        intro.setText(R.string.enhancement_description);
        intro.setTextSize(13);
        intro.setLineSpacing(dp(3), 1f);
        intro.setTextColor(getColor(R.color.text_secondary));
        page.addView(intro, SectionCard.stacked(this, 0));

        page.addView(buildInterfaceCard(), SectionCard.stacked(this, 14));
        page.addView(buildCameraCard(), SectionCard.stacked(this, 12));
        page.addView(buildDashCard(), SectionCard.stacked(this, 12));
        page.addView(buildOverlayCard(), SectionCard.stacked(this, 12));

        populatingEnhancement = false;
        refreshEnhancementAvailability();
        refreshEnhancementStatus();
    }

    private SectionCard buildInterfaceCard() {
        interfaceCard = new SectionCard(this,
                getString(R.string.ui_card_eyebrow),
                getString(R.string.ui_card_title),
                getString(R.string.ui_card_subtitle));
        hideUidRow = row(R.string.ui_hide_uid, R.string.ui_hide_uid_hint, null);
        hideUidRow.initialize(ModuleSettings.isHideUidEnabled(this));
        hideUidRow.onChanged((button, checked) -> saveInterfaceSettings());
        interfaceCard.add(hideUidRow);

        hideHudRow = row(R.string.ui_hide_hud, R.string.ui_hide_hud_hint,
                getString(R.string.badge_overlay));
        hideHudRow.initialize(ModuleSettings.isHideHudEnabled(this));
        hideHudRow.onChanged((button, checked) -> saveInterfaceSettings());
        interfaceCard.add(hideHudRow);
        return interfaceCard;
    }

    private SectionCard buildCameraCard() {
        cameraCard = new SectionCard(this,
                getString(R.string.camera_card_eyebrow),
                getString(R.string.camera_card_title),
                getString(R.string.camera_card_subtitle));

        ditherRow = row(R.string.camera_dither, R.string.camera_dither_hint, null);
        ditherRow.initialize(ModuleSettings.isDisableDitherEnabled(this));
        ditherRow.onChanged((button, checked) -> saveCameraSettings());
        cameraCard.add(ditherRow);

        freeCameraRow = row(R.string.camera_free, R.string.camera_free_hint,
                getString(R.string.badge_overlay));
        freeCameraRow.initialize(ModuleSettings.isFreeCameraEnabled(this));
        freeCameraRow.onChanged((button, checked) -> saveCameraSettings());
        cameraCard.add(freeCameraRow);

        worldPauseRow = row(R.string.camera_pause, R.string.camera_pause_hint,
                getString(R.string.badge_overlay));
        worldPauseRow.initialize(ModuleSettings.isWorldPauseEnabled(this));
        worldPauseRow.onChanged((button, checked) -> saveCameraSettings());
        cameraCard.add(worldPauseRow);

        firstPersonRow = row(R.string.camera_first_person, R.string.camera_first_person_hint,
                getString(R.string.badge_overlay));
        firstPersonRow.initialize(ModuleSettings.isFirstPersonEnabled(this));
        firstPersonRow.onChanged((button, checked) -> saveCameraSettings());
        cameraCard.add(firstPersonRow);

        hideHeadRow = row(R.string.camera_hide_head, R.string.camera_hide_head_hint, null);
        hideHeadRow.initialize(ModuleSettings.isFirstPersonHideHead(this));
        hideHeadRow.onChanged((button, checked) -> saveCameraSettings());
        cameraCard.add(hideHeadRow);

        fillNeckRow = row(R.string.camera_fill_neck, R.string.camera_fill_neck_hint, null);
        fillNeckRow.initialize(ModuleSettings.isFirstPersonFillNeck(this));
        fillNeckRow.onChanged((button, checked) -> saveCameraSettings());
        cameraCard.add(fillNeckRow);

        cameraCard.addGroupLabel(getString(R.string.camera_group_values));
        cameraSpeed = new ValueSlider(this, getString(R.string.camera_speed_label), "",
                ModuleSettings.SPEED_MINIMUM, ModuleSettings.SPEED_MAXIMUM);
        cameraSpeed.setValue((float) ModuleSettings.parse(
                ModuleSettings.getCameraSpeed(this), 5.0));
        cameraSpeed.onChanged(this::saveCameraSettings);
        cameraCard.add(cameraSpeed);

        cameraFov = new ValueSlider(this, getString(R.string.camera_fov_label),
                getString(R.string.degree_suffix),
                ModuleSettings.FOV_MINIMUM, ModuleSettings.FOV_MAXIMUM);
        cameraFov.setValue((float) ModuleSettings.parse(
                ModuleSettings.getCameraFieldOfView(this), 60.0));
        cameraFov.onChanged(this::saveCameraSettings);
        cameraCard.add(cameraFov);

        firstPersonFov = new ValueSlider(this, getString(R.string.camera_fp_fov_label),
                getString(R.string.degree_suffix),
                ModuleSettings.FOV_MINIMUM, ModuleSettings.FOV_MAXIMUM);
        firstPersonFov.setValue((float) ModuleSettings.parse(
                ModuleSettings.getFirstPersonFieldOfView(this), 75.0));
        firstPersonFov.onChanged(this::saveCameraSettings);
        cameraCard.add(firstPersonFov);
        return cameraCard;
    }

    private SectionCard buildDashCard() {
        dashCard = new SectionCard(this,
                getString(R.string.dash_card_eyebrow),
                getString(R.string.dash_card_title),
                getString(R.string.dash_card_subtitle));

        dashRow = row(R.string.dash_enable, R.string.dash_enable_hint, null);
        dashRow.initialize(ModuleSettings.isSustainedDashEnabled(this));
        dashRow.onChanged((button, checked) -> saveDashSettings());
        dashCard.add(dashRow);

        dashCard.addGroupLabel(getString(R.string.dash_group_characters));
        dashAglinaRow = row(R.string.dash_aglina, R.string.dash_aglina_hint, null);
        dashAglinaRow.initialize(ModuleSettings.isDashCharacterEnabled(this, "aglina"));
        dashAglinaRow.onChanged((button, checked) -> saveDashSettings());
        dashCard.add(dashAglinaRow);

        dashLiinoRow = row(R.string.dash_liino, R.string.dash_liino_hint, null);
        dashLiinoRow.initialize(ModuleSettings.isDashCharacterEnabled(this, "liino"));
        dashLiinoRow.onChanged((button, checked) -> saveDashSettings());
        dashCard.add(dashLiinoRow);

        dashCard.addGroupLabel(getString(R.string.dash_group_options));
        dashLiinoCleanRow = row(R.string.dash_liino_clean, R.string.dash_liino_clean_hint, null);
        dashLiinoCleanRow.initialize(ModuleSettings.isLiinoCleanDashEnabled(this));
        dashLiinoCleanRow.onChanged((button, checked) -> saveDashSettings());
        dashCard.add(dashLiinoCleanRow);

        return dashCard;
    }

    private SectionCard buildOverlayCard() {
        SectionCard card = new SectionCard(this,
                getString(R.string.overlay_card_eyebrow),
                getString(R.string.overlay_card_title),
                getString(R.string.overlay_card_subtitle));
        overlayRow = row(R.string.overlay_enable, R.string.overlay_enable_hint, null);
        overlayRow.initialize(ModuleSettings.isOverlayEnabled(this));
        overlayRow.onChanged((button, checked) -> {
            if (populatingEnhancement) return;
            ModuleSettings.setOverlayEnabled(this, checked);
            status.setText(checked
                    ? R.string.overlay_turned_on : R.string.overlay_turned_off);
        });
        card.add(overlayRow);

        Button preview = new Button(this);
        preview.setText(R.string.overlay_preview);
        preview.setTextSize(15);
        preview.setAllCaps(false);
        preview.setMinHeight(0);
        preview.setMinimumHeight(dp(52));
        preview.setTextColor(getColor(R.color.text_primary));
        preview.setBackgroundResource(R.drawable.bg_ghost_button);
        preview.setContentDescription(getString(R.string.overlay_preview_hint));
        preview.setOnClickListener(view -> showOverlayPreview());
        card.add(preview);
        return card;
    }

    private SettingRow row(int titleId, int descriptionId, String badge) {
        return new SettingRow(this, getString(titleId), getString(descriptionId), badge);
    }

    private void saveInterfaceSettings() {
        if (populatingEnhancement) return;
        ModuleSettings.setInterfaceSettings(
                this, hideUidRow.isChecked(), hideHudRow.isChecked());
        afterEnhancementChange();
    }

    private void saveCameraSettings() {
        if (populatingEnhancement) return;
        ModuleSettings.setCameraSettings(
                this,
                ditherRow.isChecked(),
                freeCameraRow.isChecked(),
                worldPauseRow.isChecked(),
                firstPersonRow.isChecked(),
                hideHeadRow.isChecked(),
                fillNeckRow.isChecked(),
                cameraSpeed.getValue(),
                cameraFov.getValue(),
                firstPersonFov.getValue());
        afterEnhancementChange();
    }

    private void saveDashSettings() {
        if (populatingEnhancement) return;
        ModuleSettings.setSustainedDashSettings(
                this,
                dashRow.isChecked(),
                dashLiinoCleanRow.isChecked(),
                dashAglinaRow.isChecked(),
                dashLiinoRow.isChecked());
        afterEnhancementChange();
    }

    private void afterEnhancementChange() {
        refreshEnhancementAvailability();
        refreshEnhancementStatus();
        status.setText(R.string.enhancement_restart_required);
    }

    /**
     * Greys out the options that only mean something while their parent feature is
     * on. World pause is a free-camera sub-mode on desktop, the first-person mesh
     * options only apply in first person, and the clean-exhaust option is Liino's.
     */
    private void refreshEnhancementAvailability() {
        boolean free = freeCameraRow.isChecked();
        boolean firstPerson = firstPersonRow.isChecked();
        worldPauseRow.setAvailable(free);
        cameraSpeed.setAvailable(free);
        cameraFov.setAvailable(free);
        hideHeadRow.setAvailable(firstPerson);
        fillNeckRow.setAvailable(firstPerson && hideHeadRow.isChecked());
        firstPersonFov.setAvailable(firstPerson);

        boolean dash = dashRow.isChecked();
        dashAglinaRow.setAvailable(dash);
        dashLiinoRow.setAvailable(dash);
        dashLiinoCleanRow.setAvailable(dash && dashLiinoRow.isChecked());
    }

    private void refreshEnhancementStatus() {
        interfaceCard.setStatus(moduleStatus("betterendfield.ui",
                ModuleSettings.isHideUidEnabled(this) || ModuleSettings.isHideHudEnabled(this)));
        cameraCard.setStatus(moduleStatus("betterendfield.camera",
                ModuleSettings.isDisableDitherEnabled(this)
                        || ModuleSettings.isFreeCameraEnabled(this)
                        || ModuleSettings.isFirstPersonEnabled(this)));
        dashCard.setStatus(getString(R.string.dash_pose_note, "pose_*.bin") + "\n"
                + moduleStatus("betterendfield.actions",
                ModuleSettings.isSustainedDashEnabled(this)
                        && (ModuleSettings.isDashCharacterEnabled(this, "aglina")
                                || ModuleSettings.isDashCharacterEnabled(this, "liino"))));
    }

    /**
     * Says whether the module will be loaded at all, which is the one thing this
     * screen can state for certain. Whether each Hook resolved is only knowable
     * inside the game, and is reported there.
     */
    private String moduleStatus(String moduleId, boolean loaded) {
        return moduleId + "  ·  " + getString(loaded
                ? R.string.module_will_load : R.string.module_will_not_load);
    }

    private void setupDiagnosticsPage() {
        TextView overlay = findViewById(R.id.diagnostics_overlay_status);
        TextView modules = findViewById(R.id.diagnostics_model_status);
        overlay.setText(getString(R.string.diagnostics_overlay,
                state(ModuleSettings.isOverlayEnabled(this)), "无需系统悬浮权限"));
        modules.setText(getString(R.string.diagnostics_modules, loadedModules()));
        findViewById(R.id.diagnostics_status).setContentDescription(
                getString(R.string.diagnostics_ready));
    }

    /**
     * The module ids the game process will actually start, read back from the same
     * configuration strings it reads. An empty configuration is how a module is
     * kept out of the process, so "absent from this list" is the real state rather
     * than a guess.
     */
    private String loadedModules() {
        StringBuilder loaded = new StringBuilder();
        appendModule(loaded, "voice.character", !ModuleSettings.getVoiceRules(this).isEmpty());
        appendModule(loaded, "model", ModuleSettings.isModelEnabled(this)
                || ModuleSettings.isLogoEnabled(this));
        appendModule(loaded, "ui", ModuleSettings.isHideUidEnabled(this)
                || ModuleSettings.isHideHudEnabled(this));
        appendModule(loaded, "camera", ModuleSettings.isDisableDitherEnabled(this)
                || ModuleSettings.isFreeCameraEnabled(this)
                || ModuleSettings.isFirstPersonEnabled(this));
        appendModule(loaded, "actions", ModuleSettings.isSustainedDashEnabled(this));
        return loaded.length() == 0 ? getString(R.string.state_none) : loaded.toString();
    }

    private void appendModule(StringBuilder text, String name, boolean loaded) {
        if (!loaded) return;
        if (text.length() > 0) text.append(" · ");
        text.append(name);
    }

    private String state(boolean on) {
        return getString(on ? R.string.state_on : R.string.state_off);
    }

    private void applyResponsiveShell() {
        android.widget.ScrollView scroll = findViewById(R.id.responsive_scroll);
        LinearLayout content = findViewById(R.id.responsive_content);
        LinearLayout navigation = findViewById(R.id.page_navigation);
        View header = findViewById(R.id.app_header);
        content.removeView(header);
        content.removeView(navigation);
        ((ViewGroup) scroll.getParent()).removeView(scroll);
        LinearLayout shell = new LinearLayout(this);
        shell.setBackgroundColor(getColor(R.color.app_background));
        boolean wide = getResources().getConfiguration().screenWidthDp >= 720
                && getResources().getConfiguration().fontScale < 1.5f;
        shell.setOrientation(wide ? LinearLayout.HORIZONTAL : LinearLayout.VERTICAL);
        if (wide) {
            LinearLayout rail = new LinearLayout(this);
            rail.setOrientation(LinearLayout.VERTICAL);
            rail.setPadding(dp(12), dp(16), dp(12), dp(12));
            rail.addView(header, new LinearLayout.LayoutParams(-1, -2));
            navigation.setOrientation(LinearLayout.VERTICAL);
            for (int i = 0; i < navigation.getChildCount(); i++) {
                View tab = navigation.getChildAt(i);
                LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(-1, -2);
                p.topMargin = dp(6);
                tab.setLayoutParams(p); tab.setMinimumHeight(dp(52));
            }
            rail.addView(navigation, new LinearLayout.LayoutParams(-1, -2));
            shell.addView(rail, new LinearLayout.LayoutParams(dp(240), -1));
            shell.addView(scroll, new LinearLayout.LayoutParams(0, -1, 1));
        } else {
            shell.addView(header, new LinearLayout.LayoutParams(-1, -2));
            shell.addView(scroll, new LinearLayout.LayoutParams(-1, 0, 1));
            shell.addView(navigation, new LinearLayout.LayoutParams(-1, -2));
            for (int i = 0; i < navigation.getChildCount(); i++) {
                View tab = navigation.getChildAt(i);
                LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(0, -2, 1);
                tab.setLayoutParams(p); tab.setMinimumHeight(dp(56));
                ((TextView) tab).setTextSize(13);
            }
        }
        setContentView(shell);
        shell.setOnApplyWindowInsetsListener((view, insets) -> {
            view.setPadding(insets.getSystemWindowInsetLeft(), insets.getSystemWindowInsetTop(),
                    insets.getSystemWindowInsetRight(), insets.getSystemWindowInsetBottom());
            return insets;
        });
        shell.requestApplyInsets();
        scroll.addOnLayoutChangeListener((v,l,t,r,b,ol,ot,or,ob) -> {
            int maxWidth = Math.min(r-l, dp(860));
            android.widget.FrameLayout.LayoutParams p = (android.widget.FrameLayout.LayoutParams) content.getLayoutParams();
            if (p.width != maxWidth) {
                p.width = maxWidth; p.gravity = android.view.Gravity.CENTER_HORIZONTAL;
                content.setLayoutParams(p);
            }
        });
    }

    private int dp(int value) {
        return Math.round(value * getResources().getDisplayMetrics().density);
    }
}
