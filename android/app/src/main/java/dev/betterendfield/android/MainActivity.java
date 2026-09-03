package dev.betterendfield.android;

import android.app.Activity;
import android.graphics.Color;
import android.graphics.drawable.GradientDrawable;
import android.os.Bundle;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
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
    private TextView modelSelectionStatus;
    private boolean initializingModel = true;

    private Switch enhancementHideUid;
    private Switch enhancementDisableDither;
    private TextView enhancementStatus;
    private boolean initializingEnhancement = true;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        status = findViewById(R.id.restart_status);
        setupPageNavigation();
        setupModelPage();
        setupVoicePage();
        setupEnhancementPage();
    }

    private void setupPageNavigation() {
        View[] sections = {
                findViewById(R.id.model_section),
                findViewById(R.id.voice_section),
                findViewById(R.id.enhancement_section)
        };
        View[] buttons = {
                findViewById(R.id.show_model_button),
                findViewById(R.id.show_voice_button),
                findViewById(R.id.show_enhancement_button)
        };
        for (int index = 0; index < buttons.length; ++index) {
            final int page = index;
            buttons[index].setOnClickListener(view -> showPage(sections, buttons, page));
        }
        showPage(sections, buttons, 0);
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
        enhancementHideUid = findViewById(R.id.enhancement_hide_uid);
        enhancementDisableDither = findViewById(R.id.enhancement_disable_dither);
        enhancementStatus = findViewById(R.id.enhancement_status);
        enhancementHideUid.setChecked(ModuleSettings.isHideUidEnabled(this));
        enhancementDisableDither.setChecked(ModuleSettings.isDisableDitherEnabled(this));
        updateEnhancementStatus();
        enhancementHideUid.setOnCheckedChangeListener(
                (button, checked) -> saveEnhancementSettings());
        enhancementDisableDither.setOnCheckedChangeListener(
                (button, checked) -> saveEnhancementSettings());
        findViewById(R.id.save_enhancement_settings).setOnClickListener(
                view -> saveEnhancementSettings());
        initializingEnhancement = false;
    }

    private void saveEnhancementSettings() {
        if (initializingEnhancement) return;
        boolean hideUid = enhancementHideUid.isChecked();
        boolean disableDither = enhancementDisableDither.isChecked();
        // An empty configuration keeps the native runtime from starting the
        // module at all, mirroring how the model configuration behaves.
        String configuration = hideUid || disableDither
                ? "hide_uid=" + hideUid + '\n' + "disable_dither=" + disableDither + '\n'
                : "";
        ModuleSettings.setEnhancementSettings(this, hideUid, disableDither, configuration);
        updateEnhancementStatus();
        status.setText(R.string.enhancement_restart_required);
    }

    private void updateEnhancementStatus() {
        enhancementStatus.setText(getString(
                R.string.enhancement_status,
                getString(enhancementHideUid.isChecked() ? R.string.state_on : R.string.state_off),
                getString(enhancementDisableDither.isChecked()
                        ? R.string.state_on : R.string.state_off)));
    }

    private int dp(int value) {
        return Math.round(value * getResources().getDisplayMetrics().density);
    }
}
