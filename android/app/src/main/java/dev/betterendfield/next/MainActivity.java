package dev.betterendfield.next;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.graphics.Color;
import android.graphics.drawable.GradientDrawable;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.text.Editable;
import android.text.TextWatcher;
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
    private static final int ABOUT_PAGE = 3;
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
    private boolean initializingModel = true;
    private final Handler saveHandler = new Handler(Looper.getMainLooper());
    private boolean pendingModelSave;
    private final Runnable debouncedModelSave = () -> {
        pendingModelSave = false;
        saveModelSettings();
    };

    private int currentPage;
    private AboutPage aboutPage;
    private BemInstallPage bemPage;
    private android.widget.HorizontalScrollView navigationScroll;
    private boolean resumed;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        String requestedPage = getIntent().getStringExtra(EXTRA_PAGE);
        currentPage = savedInstanceState == null
                ? ("custom_model".equals(requestedPage) ? CUSTOM_MODEL_PAGE
                        : ("enhancement".equals(requestedPage) ? ENHANCEMENT_PAGE
                                : ("about".equals(requestedPage) ? ABOUT_PAGE : 0)))
                : savedInstanceState.getInt("page", 0);
        // Bound restored navigation to the pages exposed by this product.
        if (currentPage > ABOUT_PAGE && currentPage != CUSTOM_MODEL_PAGE) currentPage = ENHANCEMENT_PAGE;
        currentPage = Math.max(0, currentPage);

        View bemContent = findViewById(R.id.bem_content);
        bemContent.setPadding(0, 0, 0, 0);
        bemPage = new BemInstallPage(this, bemContent, savedInstanceState);
        setupPageNavigation();
        setupModelPage();
        setupVoicePage();
        setupEnhancementPage();
        setupAboutPage();
        findViewById(R.id.sponsor_button).setOnClickListener(view -> SponsorDialog.show(this));
        applyResponsiveShell();

    }

    @Override protected void onResume() {
        super.onResume();
        resumed = true;
        updateBemRefresh();
    }

    @Override protected void onPause() {
        resumed = false;
        if (bemPage != null) bemPage.pause();
        flushModelEdits();
        refreshModelEdits();
        super.onPause();
    }

    @Override protected void onDestroy() {
        saveHandler.removeCallbacksAndMessages(null);
        if (aboutPage != null) aboutPage.close();
        if (bemPage != null) bemPage.close();
        super.onDestroy();
    }

    private void updateBemRefresh() {
        if (bemPage == null) return;
        if (resumed && currentPage == CUSTOM_MODEL_PAGE) bemPage.resume();
        else bemPage.pause();
    }

    @Override protected void onActivityResult(int request, int result, Intent data) {
        super.onActivityResult(request, result, data);
        if (bemPage != null) bemPage.onActivityResult(request, result, data);
    }

    private void flushModelEdits() {
        saveHandler.removeCallbacks(debouncedModelSave);
        if (pendingModelSave) debouncedModelSave.run();
    }

    private void watchModelEdit(EditText edit) {
        edit.addTextChangedListener(new TextWatcher() {
            @Override public void beforeTextChanged(CharSequence s, int start, int count, int after) {}
            @Override public void onTextChanged(CharSequence s, int start, int before, int count) {}
            @Override public void afterTextChanged(Editable text) {
                if (initializingModel) return;
                pendingModelSave = true;
                saveHandler.removeCallbacks(debouncedModelSave);
                saveHandler.postDelayed(debouncedModelSave, 450);
            }
        });
        edit.setOnFocusChangeListener((view, focused) -> {
            if (!focused) flushModelEdits();
        });
    }

    private void refreshModelEdits() {
        if (initializingModel || modelIndex == null) return;
        initializingModel = true;
        modelScale.setText(ModuleSettings.getModelScale(this));
        modelLoopStart.setText(ModuleSettings.getModelLoopStart(this));
        modelLoopEnd.setText(ModuleSettings.getModelLoopEnd(this));
        modelCrossfadeDuration.setText(ModuleSettings.getModelCrossfadeDuration(this));
        logoColor.setText(ModuleSettings.getLogoColor(this));
        for (EditText edit : new EditText[]{modelScale, modelLoopStart, modelLoopEnd,
                modelCrossfadeDuration, logoColor}) edit.setError(null);
        updateThemeColorPalette(logoColor.getText().toString());
        initializingModel = false;
    }

    @Override protected void onSaveInstanceState(Bundle state) {
        state.putInt("page", currentPage);
        if (bemPage != null) bemPage.saveState(state);
        super.onSaveInstanceState(state);
    }

    private void setupPageNavigation() {
        View[] sections = {
                findViewById(R.id.model_section),
                findViewById(R.id.voice_section),
                findViewById(R.id.enhancement_section),
                findViewById(R.id.about_section),
                findViewById(R.id.custom_model_section)
        };
        View[] buttons = {
                findViewById(R.id.show_model_button),
                findViewById(R.id.show_voice_button),
                findViewById(R.id.show_enhancement_button),
                findViewById(R.id.show_about_button),
                findViewById(R.id.show_custom_model_button)
        };
        android.widget.ScrollView scroll = findViewById(R.id.responsive_scroll);
        for (int index = 0; index < buttons.length; ++index) {
            final int page = index;
            buttons[index].setOnClickListener(view -> {
                flushModelEdits();
                if (currentPage == 0 && page != 0) refreshModelEdits();
                currentPage = page;
                showPage(sections, buttons, page);
                updateBemRefresh();
                scrollSelectedNavigation();
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


            installModelListeners();
            initializingModel = false;
        } catch (Exception error) {
            tableStatus.setText(getString(R.string.model_table_failed, error.getMessage()));
            tableStatus.setVisibility(View.VISIBLE);
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
                        ModelPresetIndex.Character selected = selectedCharacter();
                        if (selected == null || selected.id().equalsIgnoreCase(
                                ModuleSettings.getModelCharacter(MainActivity.this))) return;
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
        watchModelEdit(modelScale);
        watchModelEdit(modelLoopStart);
        watchModelEdit(modelLoopEnd);
        watchModelEdit(modelCrossfadeDuration);
        watchModelEdit(logoColor);
        logoColorWheel.setOnColorChangedListener((rgb, committed) -> {
            String color = String.format(Locale.ROOT, "#%06X", rgb);
            logoColor.setText(color);
            updateThemeColorPalette(color);
            if (committed) saveModelSettings();
        });
    }

    private android.widget.AdapterView.OnItemSelectedListener simpleModelSelectionListener() {
        return new android.widget.AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(
                    android.widget.AdapterView<?> parent,
                    View view,
                    int position,
                    long id) {
                ModelPresetIndex.Action selected = selectedAction();
                if (!initializingModel && selected != null && !selected.id().equalsIgnoreCase(
                        ModuleSettings.getModelAction(MainActivity.this))) saveModelSettings();
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

    }

    private ModelPresetIndex.Character selectedCharacter() {
        return modelCharacter == null || modelCharacter.getSelectedItem() == null
                ? null : (ModelPresetIndex.Character) modelCharacter.getSelectedItem();
    }

    private ModelPresetIndex.Action selectedAction() {
        return modelAction == null || modelAction.getSelectedItem() == null
                ? null : (ModelPresetIndex.Action) modelAction.getSelectedItem();
    }

    private void saveModelSettings() {
        if (initializingModel || modelIndex == null) return;
        saveHandler.removeCallbacks(debouncedModelSave);
        pendingModelSave = false;
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
            modelScale.setError(null);
        } catch (NumberFormatException error) {
            modelScale.setError("模型缩放应为 0.05–20；保留上次有效值");
            scale = ModuleSettings.parse(ModuleSettings.getModelScale(this), 1);
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
            modelLoopEnd.setError(null);
        } catch (NumberFormatException error) {
            modelLoopEnd.setError("循环区间或混合时长无效；保留上次有效值");
            loopStart = ModuleSettings.parse(ModuleSettings.getModelLoopStart(this), 0.968);
            loopEnd = ModuleSettings.parse(ModuleSettings.getModelLoopEnd(this), 2.3760002);
            crossfadeDuration = ModuleSettings.parse(
                    ModuleSettings.getModelCrossfadeDuration(this), 0.2);
        }

        String color = logoColor.getText().toString().trim().toUpperCase(Locale.ROOT);
        if (!color.matches("#[0-9A-F]{6}")) {
            logoColor.setError("主题色应为 #RRGGBB；保留上次有效值");
            color = ModuleSettings.getLogoColor(this);
        } else {
            logoColor.setError(null);
        }
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
            // Spinner selection notifications are posted during first layout.
            rows.post(() -> initializingRules = false);
        } catch (Exception error) {
            tableStatus.setText(getString(
                    R.string.voice_table_failed, error.getMessage()));
            tableStatus.setVisibility(View.VISIBLE);
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
        if (initializingRules || ruleSpinners.isEmpty()) return;
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
        ModuleSettings.republishConfigurations(this);
        LinearLayout page = findViewById(R.id.enhancement_section);
        page.removeAllViews();
        addEnhancementEntry(page, getString(R.string.ui_card_title),
                EnhancementSettingsActivity.INTERFACE);
        addEnhancementEntry(page, getString(R.string.camera_card_title),
                EnhancementSettingsActivity.CAMERA);
        addEnhancementEntry(page, "持续冲刺", EnhancementSettingsActivity.DASH);
        addEnhancementEntry(page, getString(R.string.overlay_card_title),
                EnhancementSettingsActivity.OVERLAY);
        Button mmd = enhancementButton(getString(R.string.mmd_title));
        mmd.setOnClickListener(view -> {
            flushModelEdits();
            startActivity(new Intent(this, MmdLibraryActivity.class));
        });
        page.addView(mmd, SectionCard.stacked(this, 12));

        Button workshop = enhancementButton("创意工坊");
        workshop.setOnClickListener(view -> {
            try {
                startActivity(new Intent(Intent.ACTION_VIEW,
                        Uri.parse("https://146.235.16.65:8443/endfield/"))
                        .addCategory(Intent.CATEGORY_BROWSABLE));
            } catch (RuntimeException ignored) {
                android.widget.Toast.makeText(this, "无法打开创意工坊", android.widget.Toast.LENGTH_SHORT).show();
            }
        });
        page.addView(workshop, SectionCard.stacked(this, 12));
    }

    private void addEnhancementEntry(LinearLayout page, String title, String feature) {
        Button entry = enhancementButton(title);
        entry.setOnClickListener(view -> {
            flushModelEdits();
            startActivity(new Intent(this, EnhancementSettingsActivity.class)
                    .putExtra(EnhancementSettingsActivity.EXTRA_FEATURE, feature));
        });
        page.addView(entry, SectionCard.stacked(this, page.getChildCount() == 0 ? 0 : 12));
    }

    private Button enhancementButton(String title) {
        Button button = new Button(this);
        button.setText(title);
        button.setTextSize(15);
        button.setAllCaps(false);
        button.setMinHeight(0);
        button.setMinimumHeight(dp(56));
        button.setPadding(dp(16), dp(12), dp(16), dp(12));
        button.setGravity(android.view.Gravity.START | android.view.Gravity.CENTER_VERTICAL);
        button.setTextColor(getColor(R.color.text_primary));
        button.setBackgroundResource(R.drawable.bg_ghost_button);
        return button;
    }

    private void setupAboutPage() {
        LinearLayout page = findViewById(R.id.about_section);
        aboutPage = new AboutPage(this);
        page.addView(aboutPage);
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
            navigationScroll=new android.widget.HorizontalScrollView(this);
            navigationScroll.setHorizontalScrollBarEnabled(false);navigationScroll.setFillViewport(false);
            navigationScroll.addView(navigation,new android.widget.FrameLayout.LayoutParams(-2,-2));
            shell.addView(navigationScroll, new LinearLayout.LayoutParams(-1, -2));
            for (int i = 0; i < navigation.getChildCount(); i++) {
                View tab = navigation.getChildAt(i);
                LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(-2, -2);
                p.setMarginStart(dp(4));tab.setLayoutParams(p); tab.setMinimumHeight(dp(56));tab.setMinimumWidth(dp(92));
                tab.setPadding(dp(14),dp(8),dp(14),dp(8));
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
        scrollSelectedNavigation();
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
    private void scrollSelectedNavigation() {
        if(navigationScroll==null)return;
        LinearLayout navigation=findViewById(R.id.page_navigation);
        navigationScroll.post(()->{for(int i=0;i<navigation.getChildCount();++i)if(navigation.getChildAt(i).isSelected()) {
            navigationScroll.smoothScrollTo(Math.max(0,navigation.getChildAt(i).getLeft()-dp(16)),0);break;
        }});
    }
}
