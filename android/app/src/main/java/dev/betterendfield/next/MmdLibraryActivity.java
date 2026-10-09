package dev.betterendfield.next;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.text.Editable;
import android.text.InputType;
import android.text.TextWatcher;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/** Settings and SAF imports live in the module app; playback lives in the game. */
public final class MmdLibraryActivity extends Activity {
    private final Handler handler = new Handler(Looper.getMainLooper());
    private final List<String> folders = new ArrayList<>();
    private final List<Button> workButtons = new ArrayList<>();
    private SettingRow enabled, loop, music;
    private SettingRow body, face, terrain, keyframeLoop;
    private Spinner selectedWork;
    private Spinner clothMode, motionPreset;
    private EditText seek, gain, offset;
    private EditText motionScale, smoothing, motionSpeed, orbitSpeed, duration, targetHeight, segmentSeconds;
    private int selectedClothMode;
    private String selectedPreset;
    private TextView status;
    private LinearLayout works;
    private Button importButton;
    private boolean initializing = true;
    private boolean pending;
    private String renderedIndex;
    private String selectedFolder = "";
    private String indexError;
    private final Runnable save = () -> {
        pending = false;
        saveSettings();
    };
    private final Runnable poll = new Runnable() {
        @Override public void run() {
            refreshLibrary();
            boolean available = !MmdInstaller.busy && indexError == null;
            importButton.setEnabled(available);
            selectedWork.setEnabled(available && folders.size() > 1);
            for (Button button : workButtons) button.setEnabled(available);
            enabled.setAvailable(available);
            loop.setAvailable(available);
            music.setAvailable(available);
            seek.setEnabled(available);
            gain.setEnabled(available);
            offset.setEnabled(available);
            body.setAvailable(available);
            face.setAvailable(available);
            terrain.setAvailable(available && body.isChecked());
            clothMode.setEnabled(available);
            motionPreset.setEnabled(available);
            keyframeLoop.setAvailable(available);
            for (EditText field : advancedEdits()) field.setEnabled(available);
            String message = indexError == null ? MmdInstaller.status : indexError;
            boolean error = indexError != null || message.contains("失败") || message.contains("未完成");
            status.setText(message);
            status.setVisibility(MmdInstaller.busy || error ? View.VISIBLE : View.GONE);
            handler.postDelayed(this, 500);
        }
    };

    @Override protected void onCreate(Bundle state) {
        super.onCreate(state);
        ScrollView scroll = new ScrollView(this);
        scroll.setFillViewport(true);
        scroll.setBackgroundColor(getColor(R.color.app_background));
        LinearLayout page = new LinearLayout(this);
        page.setOrientation(LinearLayout.VERTICAL);
        page.setPadding(dp(16), dp(16), dp(16), dp(24));
        scroll.addView(page);

        Button back = button("‹ 返回相机设置");
        back.setOnClickListener(view -> finish());
        page.addView(back, SectionCard.stacked(this, 0));

        SectionCard settings = new SectionCard(this, "MMD · CAMERA",
                getString(R.string.mmd_title), getString(R.string.mmd_description));
        enabled = new SettingRow(this, getString(R.string.mmd_enable),
                getString(R.string.mmd_enable_hint), null);
        enabled.initialize(ModuleSettings.isMmdEnabled(this));
        enabled.onChanged((button, checked) -> {
            if (initializing) return;
            ModuleSettings.setMmdEnabled(this, checked);

        });
        settings.add(enabled);
        settings.add(label(R.string.mmd_active_work));
        selectedWork = new Spinner(this);
        selectedWork.setBackgroundResource(R.drawable.bg_input);
        selectedWork.setMinimumHeight(dp(52));
        selectedWork.setContentDescription(getString(R.string.mmd_active_work));
        settings.add(selectedWork);
        loop = new SettingRow(this, getString(R.string.mmd_loop), "作品结束后重新播放。", null);
        loop.initialize(ModuleSettings.isMmdLoop(this));
        loop.onChanged((button, checked) -> saveSettings());
        settings.add(loop);
        music = new SettingRow(this, getString(R.string.mmd_music),
                "作品需在 set.ini 中配置 music 文件。音频会叠加游戏背景音乐，建议先关闭游戏 BGM。", null);
        music.initialize(ModuleSettings.isMmdMusicEnabled(this));
        music.onChanged((button, checked) -> saveSettings());
        settings.add(music);
        seek = edit(settings, R.string.mmd_seek, ModuleSettings.getMmdSeekSeconds(this), false);
        gain = edit(settings, R.string.mmd_gain, ModuleSettings.getMmdMusicGain(this), false);
        offset = edit(settings, R.string.mmd_offset, ModuleSettings.getMmdAudioOffset(this), true);
        ModuleSettings.CameraExtras extras = ModuleSettings.getCameraExtras(this);
        body = toggle(settings, R.string.mmd_body, R.string.mmd_body_hint, extras.body());
        face = toggle(settings, R.string.mmd_face, R.string.mmd_face_hint, extras.face());
        motionScale = edit(settings, R.string.mmd_motion_scale,
                ModuleSettings.number(extras.motionScale()), false);
        terrain = toggle(settings, R.string.mmd_terrain, R.string.mmd_terrain_hint, extras.terrain());
        selectedClothMode = extras.clothMode();
        clothMode = selection(settings, R.string.mmd_cloth_mode,
                new String[]{getString(R.string.mmd_cloth_game), getString(R.string.mmd_cloth_stable),
                        getString(R.string.mmd_cloth_freeze)}, selectedClothMode);
        page.addView(settings, SectionCard.stacked(this, 12));

        SectionCard advanced = new SectionCard(this, "CAMERA", getString(R.string.camera_advanced_title),
                getString(R.string.camera_advanced_hint));
        selectedPreset = extras.preset();
        motionPreset = selection(advanced, R.string.camera_motion_preset,
                new String[]{"Orbit · 环绕", "DollyZoom · 滑动变焦", "Crane · 升降", "Truck · 平移"},
                ModuleSettings.choice(ModuleSettings.MOTION_PRESETS, selectedPreset, 0));
        smoothing = edit(advanced, R.string.camera_smoothing, ModuleSettings.number(extras.smoothing()), false);
        motionSpeed = edit(advanced, R.string.camera_motion_speed, ModuleSettings.number(extras.motionSpeed()), true);
        orbitSpeed = edit(advanced, R.string.camera_orbit_speed, ModuleSettings.number(extras.orbitSpeed()), true);
        duration = edit(advanced, R.string.camera_motion_duration, ModuleSettings.number(extras.duration()), false);
        targetHeight = edit(advanced, R.string.camera_target_height, ModuleSettings.number(extras.targetHeight()), true);
        segmentSeconds = edit(advanced, R.string.camera_keyframe_segment,
                ModuleSettings.number(extras.segmentSeconds()), false);
        keyframeLoop = toggle(advanced, R.string.camera_keyframe_loop,
                R.string.camera_keyframe_loop_hint, extras.keyframeLoop());
        page.addView(advanced, SectionCard.stacked(this, 12));

        SectionCard library = new SectionCard(this, "LIBRARY", "导入与管理", "");
        importButton = button(getString(R.string.mmd_import));
        importButton.setOnClickListener(view -> {
            flushEdits();
            startActivity(new Intent(this, MmdImportActivity.class));
        });
        library.add(importButton);
        status = label(0);
        status.setVisibility(View.GONE);
        status.setTextIsSelectable(true);
        library.add(status);
        works = new LinearLayout(this);
        works.setOrientation(LinearLayout.VERTICAL);
        library.add(works);
        page.addView(library, SectionCard.stacked(this, 12));
        setContentView(scroll);
        scroll.setOnApplyWindowInsetsListener((view, insets) -> {
            view.setPadding(insets.getSystemWindowInsetLeft(), insets.getSystemWindowInsetTop(),
                    insets.getSystemWindowInsetRight(), insets.getSystemWindowInsetBottom());
            return insets;
        });
        scroll.requestApplyInsets();
        refreshLibrary();
        selectedWork.setOnItemSelectedListener(new android.widget.AdapterView.OnItemSelectedListener() {
            @Override public void onItemSelected(android.widget.AdapterView<?> parent, View view,
                    int position, long id) {
                if (initializing || position < 0 || position >= folders.size()
                        || position != selectedWork.getSelectedItemPosition()) return;
                String folder = folders.get(position);
                if (folder.equals(selectedFolder)) return;
                selectedFolder = folder;
                saveSettings();
            }
            @Override public void onNothingSelected(android.widget.AdapterView<?> parent) { }
        });
        watchSelection(clothMode, () -> {
            int mode = clothMode.getSelectedItemPosition();
            if (mode == selectedClothMode) return;
            selectedClothMode = mode;
            saveSettings();
        });
        watchSelection(motionPreset, () -> {
            String preset = ModuleSettings.MOTION_PRESETS[motionPreset.getSelectedItemPosition()];
            if (preset.equals(selectedPreset)) return;
            selectedPreset = preset;
            saveSettings();
        });
        initializing = false;
    }

    @Override protected void onResume() {
        super.onResume();
        handler.removeCallbacks(poll);
        handler.post(poll);
    }

    @Override protected void onPause() {
        flushEdits();
        refreshEdits();
        handler.removeCallbacks(poll);
        super.onPause();
    }

    @Override protected void onDestroy() {
        handler.removeCallbacksAndMessages(null);
        super.onDestroy();
    }

    private EditText edit(SectionCard card, int label, String value, boolean signed) {
        card.add(label(label));
        EditText field = new EditText(this);
        field.setSingleLine(true);
        field.setTextColor(getColor(R.color.text_primary));
        field.setBackgroundResource(R.drawable.bg_input);
        field.setPadding(dp(14), dp(12), dp(14), dp(12));
        field.setMinimumHeight(dp(52));
        field.setInputType(InputType.TYPE_CLASS_NUMBER | InputType.TYPE_NUMBER_FLAG_DECIMAL
                | (signed ? InputType.TYPE_NUMBER_FLAG_SIGNED : 0));
        field.setContentDescription(getString(label));
        field.setText(value);
        field.addTextChangedListener(new TextWatcher() {
            @Override public void beforeTextChanged(CharSequence s, int start, int count, int after) { }
            @Override public void onTextChanged(CharSequence s, int start, int before, int count) { }
            @Override public void afterTextChanged(Editable text) {
                if (initializing) return;
                pending = true;
                handler.removeCallbacks(save);
                handler.postDelayed(save, 450);
            }
        });
        field.setOnFocusChangeListener((view, focused) -> { if (!focused) flushEdits(); });
        card.add(field);
        return field;
    }

    private void flushEdits() {
        handler.removeCallbacks(save);
        if (pending) save.run();
    }

    private void refreshEdits() {
        if (seek == null) return;
        initializing = true;
        seek.setText(ModuleSettings.getMmdSeekSeconds(this));
        gain.setText(ModuleSettings.getMmdMusicGain(this));
        offset.setText(ModuleSettings.getMmdAudioOffset(this));
        seek.setError(null); gain.setError(null); offset.setError(null);
        ModuleSettings.CameraExtras extras = ModuleSettings.getCameraExtras(this);
        double[] numbers = {extras.motionScale(), extras.smoothing(), extras.motionSpeed(),
                extras.orbitSpeed(), extras.duration(), extras.targetHeight(), extras.segmentSeconds()};
        EditText[] fields = advancedEdits();
        for (int i = 0; i < fields.length; i++) {
            fields[i].setText(ModuleSettings.number(numbers[i]));
            fields[i].setError(null);
        }
        initializing = false;
    }

    private void saveSettings() {
        if (initializing || seek == null || segmentSeconds == null) return;
        handler.removeCallbacks(save);
        pending = false;
        ModuleSettings.CameraExtras previous = ModuleSettings.getCameraExtras(this);
        ModuleSettings.CameraExtras extras = new ModuleSettings.CameraExtras(
                body.isChecked(), face.isChecked(), terrain.isChecked(),
                valid(motionScale, 0.05, 5, ModuleSettings.number(previous.motionScale()), 1),
                selectedClothMode, selectedPreset,
                valid(smoothing, 0, 0.95, ModuleSettings.number(previous.smoothing()), 0.3),
                valid(motionSpeed, -20, 20, ModuleSettings.number(previous.motionSpeed()), 1),
                valid(orbitSpeed, -180, 180, ModuleSettings.number(previous.orbitSpeed()), 20),
                valid(duration, 0, 600, ModuleSettings.number(previous.duration()), 0),
                valid(targetHeight, -5, 5, ModuleSettings.number(previous.targetHeight()), 1.2),
                valid(segmentSeconds, 0.2, 60, ModuleSettings.number(previous.segmentSeconds()), 3),
                keyframeLoop.isChecked());
        ModuleSettings.setMmdSettings(this, selectedFolder, loop.isChecked(), music.isChecked(),
                valid(seek, 0.5, 60, ModuleSettings.getMmdSeekSeconds(this), 5),
                valid(gain, 0, 1, ModuleSettings.getMmdMusicGain(this), 1),
                valid(offset, -600, 600, ModuleSettings.getMmdAudioOffset(this), 0), extras);

    }

    private double valid(EditText field, double minimum, double maximum, String previous, double fallback) {
        double value = ModuleSettings.parse(field.getText().toString(), Double.NaN);
        if (!Double.isFinite(value) || value < minimum || value > maximum) {
            field.setError("范围 " + ModuleSettings.number(minimum) + "–" + ModuleSettings.number(maximum)
                    + "；保留上次有效值");
            return ModuleSettings.parse(previous, fallback);
        }
        field.setError(null);
        return value;
    }

    private EditText[] advancedEdits() {
        return new EditText[]{motionScale, smoothing, motionSpeed, orbitSpeed, duration, targetHeight, segmentSeconds};
    }

    private SettingRow toggle(SectionCard card, int title, int description, boolean checked) {
        SettingRow row = new SettingRow(this, getString(title), getString(description), null);
        row.initialize(checked);
        row.onChanged((button, value) -> saveSettings());
        card.add(row);
        return row;
    }

    private Spinner selection(SectionCard card, int title, String[] labels, int position) {
        card.add(label(title));
        Spinner spinner = new Spinner(this);
        spinner.setBackgroundResource(R.drawable.bg_input);
        spinner.setMinimumHeight(dp(52));
        spinner.setContentDescription(getString(title));
        ArrayAdapter<String> adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_item, labels);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner.setAdapter(adapter);
        spinner.setSelection(position, false);
        card.add(spinner);
        return spinner;
    }

    private void watchSelection(Spinner spinner, Runnable changed) {
        spinner.setOnItemSelectedListener(new android.widget.AdapterView.OnItemSelectedListener() {
            @Override public void onItemSelected(android.widget.AdapterView<?> parent, View view,
                    int position, long id) {
                if (initializing || position < 0 || position != spinner.getSelectedItemPosition()) return;
                changed.run();
            }
            @Override public void onNothingSelected(android.widget.AdapterView<?> parent) { }
        });
    }

    private void refreshLibrary() {
        try {
            JSONArray entries = MmdInstaller.index(this);
            String serialized = entries.toString();
            if (serialized.equals(renderedIndex)) return;
            boolean wasInitializing = initializing;
            initializing = true;
            try {
                folders.clear(); workButtons.clear(); works.removeAllViews();
                List<String> names = new ArrayList<>();
                names.add(getString(R.string.mmd_no_selection)); folders.add("");
                String configured = ModuleSettings.getMmdWork(this);
                int position = 0;
                for (int i = 0; i < entries.length(); i++) {
                    JSONObject entry = entries.getJSONObject(i);
                    String folder = entry.getString("folder");
                    folders.add(folder); names.add(entry.getString("name"));
                    if (folder.equals(configured)) position = i + 1;
                    TextView title = label(0);
                    title.setText(entry.getString("name"));
                    works.addView(title, SectionCard.stacked(this, 12));
                    String generation = entry.getString("generation");
                    Button publish = button(getString(R.string.mmd_publish));
                    publish.setOnClickListener(view -> {
                        flushEdits();
                        if (!MmdInstaller.republish(this, generation)) toast("请等待当前作品操作完成");
                    });
                    works.addView(publish, SectionCard.stacked(this, 6)); workButtons.add(publish);
                    Button remove = button(getString(R.string.mmd_remove));
                    remove.setOnClickListener(view -> new AlertDialog.Builder(this)
                            .setTitle(R.string.mmd_remove_title).setMessage(R.string.mmd_remove_message)
                            .setNegativeButton(android.R.string.cancel, null)
                            .setPositiveButton(R.string.mmd_remove, (dialog, which) -> {
                                flushEdits();
                                if (!MmdInstaller.remove(this, generation)) toast("请等待当前作品操作完成");
                            }).show());
                    works.addView(remove, SectionCard.stacked(this, 6)); workButtons.add(remove);
                }
                if (entries.length() == 0) {
                    TextView empty = label(0);
                    empty.setText("暂无作品");
                    works.addView(empty);
                }
                ArrayAdapter<String> adapter = new ArrayAdapter<>(this,
                        android.R.layout.simple_spinner_item, names);
                adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                selectedFolder = folders.get(position);
                selectedWork.setAdapter(adapter);
                selectedWork.setSelection(position, false);
                renderedIndex = serialized;
                indexError = null;
            } finally { initializing = wasInitializing; }
        } catch (Exception error) {
            indexError = "无法读取作品库：" + error.getMessage();
        }
    }

    private Button button(String text) {
        Button button = new Button(this);
        button.setText(text); button.setAllCaps(false);
        button.setTextColor(getColor(R.color.text_primary));
        button.setBackgroundResource(R.drawable.bg_ghost_button);
        button.setMinimumHeight(dp(52));
        return button;
    }

    private TextView label(int resource) {
        TextView label = new TextView(this);
        if (resource != 0) label.setText(resource);
        label.setTextColor(getColor(R.color.text_secondary));
        label.setTextSize(13); label.setLineSpacing(dp(3), 1);
        return label;
    }

    private void toast(String text) {
        Toast.makeText(this, text == null ? "操作失败" : text, Toast.LENGTH_LONG).show();
    }

    private int dp(int value) {
        return Math.round(value * getResources().getDisplayMetrics().density);
    }
}
