package dev.betterendfield.next;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Insets;
import android.graphics.Typeface;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.view.Gravity;
import android.view.View;
import android.view.WindowInsets;
import android.widget.*;
import org.json.JSONArray;
import org.json.JSONObject;

import java.io.*;
import java.util.*;

/** File sources are configured here; takeover is explicitly triggered in-game. */
public final class MediaScreenActivity extends Activity {
    private static final int PICK = 71;
    private SharedPreferences settings;
    private SettingRow enabled, audio, loop, wallScreen, deskScreen;
    private Spinner source, resolution, deskFit;
    private EditText interval, gain, target, toggleHotkey, pauseHotkey, width, height;
    private LinearLayout customResolution;
    private static final int[][] RESOLUTIONS = {{1280, 720}, {1920, 1080}, {2560, 1440}, {3840, 2160}};
    private int captureFps = 60;
    private TextView state;
    private Button choose, saveButton;
    private boolean loading, busy;
    private String selectedPath = "", selectedImages = "";

    @Override public void onCreate(Bundle saved) {
        super.onCreate(saved);
        settings = FrameworkSettings.open(this);
        loading = true;

        ScrollView scroll = new ScrollView(this);
        scroll.setFillViewport(true);
        scroll.setBackgroundColor(getColor(R.color.app_background));

        LinearLayout page = new LinearLayout(this);
        page.setOrientation(LinearLayout.VERTICAL);
        page.setPadding(dp(16), dp(16), dp(16), dp(24));
        scroll.addView(page, new FrameLayout.LayoutParams(-1, -2, Gravity.CENTER_HORIZONTAL));

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

        scroll.addOnLayoutChangeListener((view, left, top, right, bottom, oldLeft, oldTop, oldRight, oldBottom) -> {
            int widthPx = Math.max(0, right - left - scroll.getPaddingLeft() - scroll.getPaddingRight());
            FrameLayout.LayoutParams params = (FrameLayout.LayoutParams) page.getLayoutParams();
            int contentWidth = Math.min(widthPx, dp(860));
            if (params.width != contentWidth) {
                params.width = contentWidth;
                page.setLayoutParams(params);
            }
        });

        Button back = ghostButton("‹ 返回");
        back.setOnClickListener(v -> finish());
        page.addView(back, SectionCard.stacked(this, 0));

        TextView title = new TextView(this);
        title.setText("会客室媒体屏");
        title.setTextSize(22);
        title.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        title.setTextColor(getColor(R.color.text_primary));
        page.addView(title, SectionCard.stacked(this, 12));

        // Card 1: 功能状态
        SectionCard cardEnable = new SectionCard(this, "", "功能状态", "");
        enabled = new SettingRow(this, "启用媒体屏", "在帝江号会客室替换屏幕显示自选媒体内容", null);
        cardEnable.add(enabled);
        page.addView(cardEnable, SectionCard.stacked(this, 12));

        // Card 2: 媒体内容与文件
        SectionCard cardSource = new SectionCard(this, "", "媒体内容", "");
        cardSource.addGroupLabel("媒体来源类型");
        source = styledSpinner(new String[]{"单张图片", "图片轮播", "视频文件"});
        cardSource.add(source);

        choose = primaryButton("导入媒体文件");
        cardSource.add(choose);

        state = label("未选择文件");
        state.setPadding(dp(4), dp(4), dp(4), dp(8));
        cardSource.add(state);

        audio = new SettingRow(this, "播放视频声音", "启用视频内置音频流", null);
        loop = new SettingRow(this, "循环播放", "视频结束或图片轮播到末尾时自动重新循环", null);
        cardSource.add(audio);
        cardSource.add(loop);

        interval = edit(cardSource, "轮播间隔（秒）", "10");
        gain = edit(cardSource, "音量（0.0 - 2.0）", "1");
        page.addView(cardSource, SectionCard.stacked(this, 12));

        // Card 3: 屏幕投屏与适配
        SectionCard cardScreen = new SectionCard(this, "", "投屏与适配", "");
        wallScreen = new SettingRow(this, "大屏投屏（墙面轮播屏）", "接管会客室主墙壁大显示屏", null);
        deskScreen = new SettingRow(this, "小屏投屏（桌面电脑）", "接管会客室桌面电脑显示屏", null);
        cardScreen.add(wallScreen);
        cardScreen.add(deskScreen);

        cardScreen.addGroupLabel("小屏画面适配");
        deskFit = styledSpinner(new String[]{"完整显示（保持比例，留黑边）", "裁剪填满（保持比例）", "拉伸填满"});
        cardScreen.add(deskFit);

        target = edit(cardScreen, "屏幕对象路径", "ReceptionRoomImagePoster");
        page.addView(cardScreen, SectionCard.stacked(this, 12));

        // Card 4: 分辨率与控制
        SectionCard cardRender = new SectionCard(this, "", "分辨率与控制", "");
        cardRender.addGroupLabel("输出分辨率");
        resolution = styledSpinner(new String[]{"720p · 1280 × 720", "1080p · 1920 × 1080", "1440p · 2560 × 1440", "4K · 3840 × 2160", "自定义"});
        cardRender.add(resolution);

        customResolution = new LinearLayout(this);
        customResolution.setOrientation(LinearLayout.VERTICAL);
        width = edit(customResolution, "画布宽度", "1920");
        height = edit(customResolution, "画布高度", "1080");
        cardRender.add(customResolution);

        toggleHotkey = edit(cardRender, "启停快捷键", "-");
        pauseHotkey = edit(cardRender, "暂停／继续快捷键", "");
        page.addView(cardRender, SectionCard.stacked(this, 12));

        // Save Button & Hint
        saveButton = primaryButton("保存设置");
        saveButton.setOnClickListener(v -> {
            if (save()) Toast.makeText(this, "媒体屏设置已保存，冷重启游戏后生效", Toast.LENGTH_SHORT).show();
        });
        page.addView(saveButton, SectionCard.stacked(this, 16));

        TextView hint = label("注意：修改设置后需彻底强制停止并重新启动游戏。");
        hint.setTextColor(getColor(R.color.text_muted));
        hint.setGravity(Gravity.CENTER_HORIZONTAL);
        page.addView(hint, SectionCard.stacked(this, 8));

        setContentView(scroll);
        scroll.requestApplyInsets();

        Map<String, String> values = values(settings.getString(ModuleSettings.MEDIA_SETTINGS,
                settings.getString(ModuleSettings.MEDIA_CONFIGURATION, "")));

        enabled.initialize(!values.isEmpty() && "true".equals(values.get("enabled")));
        wallScreen.initialize(!"false".equals(values.get("wall_screen_enabled")) && !"0".equals(values.get("wall_screen_enabled")));
        deskScreen.initialize(!"false".equals(values.get("desk_screen_enabled")) && !"0".equals(values.get("desk_screen_enabled")));
        deskFit.setSelection("crop".equals(values.get("desk_fit_mode")) ? 1 : "stretch".equals(values.get("desk_fit_mode")) ? 2 : 0);
        deskFit.setEnabled(deskScreen.isChecked());
        audio.initialize(!"false".equals(values.get("audio_enabled")));
        loop.initialize(!"false".equals(values.get("loop")));
        source.setSelection("video".equals(values.get("source")) ? 2 : "slideshow".equals(values.get("source")) ? 1 : 0);
        interval.setText(values.getOrDefault("slide_interval", "10"));
        gain.setText(values.getOrDefault("gain", "1"));
        target.setText(values.getOrDefault("target_object", "ReceptionRoomImagePoster"));
        toggleHotkey.setText(values.getOrDefault("toggle_hotkey", "-"));
        String pauseBinding = values.getOrDefault("pause_hotkey", "");
        pauseHotkey.setText("NONE".equals(pauseBinding) ? "" : pauseBinding);
        width.setText(values.getOrDefault("width", "1920"));
        height.setText(values.getOrDefault("height", "1080"));
        try { captureFps = Math.max(1, Math.min(120, Integer.parseInt(values.getOrDefault("fps", "60")))); } catch (NumberFormatException ignored) {}

        int preset = RESOLUTIONS.length;
        for (int i = 0; i < RESOLUTIONS.length; i++) {
            if (width.getText().toString().equals(Integer.toString(RESOLUTIONS[i][0])) &&
                height.getText().toString().equals(Integer.toString(RESOLUTIONS[i][1]))) preset = i;
        }
        resolution.setSelection(preset);
        customResolution.setVisibility(preset == RESOLUTIONS.length ? View.VISIBLE : View.GONE);
        selectedPath = values.getOrDefault("source_path", "");
        selectedImages = values.getOrDefault("image_paths", "");
        loading = false;

        enabled.onChanged((b, checked) -> save());
        wallScreen.onChanged((b, checked) -> save());
        deskScreen.onChanged((b, checked) -> {
            deskFit.setEnabled(checked && !busy);
            save();
        });
        audio.onChanged((b, checked) -> save());
        loop.onChanged((b, checked) -> save());

        deskFit.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            public void onItemSelected(AdapterView<?> p, View view, int position, long id) { if (!loading) save(); }
            public void onNothingSelected(AdapterView<?> p) {}
        });
        source.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            public void onItemSelected(AdapterView<?> p, View view, int position, long id) {
                if (!loading) {
                    refresh();
                    save();
                }
            }
            public void onNothingSelected(AdapterView<?> p) {}
        });
        resolution.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            public void onItemSelected(AdapterView<?> p, View view, int position, long id) {
                if (loading) return;
                if (position < RESOLUTIONS.length) {
                    width.setText(Integer.toString(RESOLUTIONS[position][0]));
                    height.setText(Integer.toString(RESOLUTIONS[position][1]));
                }
                customResolution.setVisibility(position == RESOLUTIONS.length ? View.VISIBLE : View.GONE);
                save();
            }
            public void onNothingSelected(AdapterView<?> p) {}
        });
        for (EditText edit : new EditText[]{interval, gain, target, toggleHotkey, pauseHotkey, width, height}) {
            edit.setOnFocusChangeListener((v, focus) -> { if (!focus) save(); });
        }
        choose.setOnClickListener(v -> {
            if (busy) return;
            int mode = source.getSelectedItemPosition();
            Intent intent = new Intent(Intent.ACTION_OPEN_DOCUMENT).addCategory(Intent.CATEGORY_OPENABLE)
                .setType(mode == 2 ? "video/mp4" : "image/*")
                .putExtra(Intent.EXTRA_ALLOW_MULTIPLE, mode == 1);
            startActivityForResult(intent, PICK);
        });
        refresh();
    }

    private Spinner styledSpinner(String[] items) {
        Spinner sp = new Spinner(this);
        sp.setBackgroundResource(R.drawable.bg_input);
        sp.setMinimumHeight(dp(48));
        ArrayAdapter<String> adapter = new ArrayAdapter<>(this, R.layout.bem_spinner_item, items);
        adapter.setDropDownViewResource(R.layout.bem_spinner_dropdown_item);
        sp.setAdapter(adapter);
        return sp;
    }

    private Button primaryButton(String text) {
        Button button = new Button(this);
        button.setText(text);
        button.setTextSize(15);
        button.setAllCaps(false);
        button.setMinHeight(0);
        button.setMinimumHeight(dp(50));
        button.setPadding(dp(14), dp(12), dp(14), dp(12));
        button.setTextColor(getColor(R.color.accent_ink));
        button.setBackgroundResource(R.drawable.bg_primary_button);
        button.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        return button;
    }

    private Button ghostButton(String text) {
        Button button = new Button(this);
        button.setText(text);
        button.setTextSize(14);
        button.setAllCaps(false);
        button.setMinHeight(0);
        button.setMinimumHeight(dp(44));
        button.setPadding(dp(14), dp(10), dp(14), dp(10));
        button.setTextColor(getColor(R.color.text_primary));
        button.setBackgroundResource(R.drawable.bg_ghost_button);
        return button;
    }

    private TextView label(String text) {
        TextView view = new TextView(this);
        view.setText(text);
        view.setTextColor(getColor(R.color.text_secondary));
        view.setTextSize(13);
        return view;
    }

    private EditText edit(SectionCard card, String labelText, String initial) {
        card.addGroupLabel(labelText);
        EditText view = new EditText(this);
        view.setSingleLine(true);
        view.setText(initial);
        view.setTextColor(getColor(R.color.text_primary));
        view.setHintTextColor(getColor(R.color.text_muted));
        view.setBackgroundResource(R.drawable.bg_input);
        view.setMinimumHeight(dp(48));
        view.setPadding(dp(14), dp(10), dp(14), dp(10));
        view.setTextSize(14);
        card.add(view);
        return view;
    }

    private EditText edit(LinearLayout container, String labelText, String initial) {
        TextView tv = new TextView(this);
        tv.setText(labelText);
        tv.setTextSize(11);
        tv.setTextColor(getColor(R.color.text_muted));
        tv.setTypeface(Typeface.create("sans-serif-medium", Typeface.NORMAL));
        LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.WRAP_CONTENT);
        lp.topMargin = dp(8);
        lp.bottomMargin = dp(4);
        lp.leftMargin = dp(2);
        container.addView(tv, lp);

        EditText view = new EditText(this);
        view.setSingleLine(true);
        view.setText(initial);
        view.setTextColor(getColor(R.color.text_primary));
        view.setHintTextColor(getColor(R.color.text_muted));
        view.setBackgroundResource(R.drawable.bg_input);
        view.setMinimumHeight(dp(48));
        view.setPadding(dp(14), dp(10), dp(14), dp(10));
        view.setTextSize(14);
        container.addView(view, new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.WRAP_CONTENT));
        return view;
    }

    private int dp(int value) {
        return Math.round(value * getResources().getDisplayMetrics().density);
    }

    static Map<String, String> values(String ini) {
        Map<String, String> values = new HashMap<>();
        if (ini == null) return values;
        for (String line : ini.split("\\n")) {
            int equal = line.indexOf('=');
            if (equal > 0) values.put(line.substring(0, equal), line.substring(equal + 1).trim());
        }
        return values;
    }

    private boolean save() {
        if (loading || busy) return false;
        try {
            double seconds = Double.parseDouble(interval.getText().toString()), volume = Double.parseDouble(gain.getText().toString());
            int outputWidth = Integer.parseInt(width.getText().toString()), outputHeight = Integer.parseInt(height.getText().toString());
            String destination = target.getText().toString().trim();
            if (!Double.isFinite(seconds) || seconds < 1 || seconds > 3600 || !Double.isFinite(volume) || volume < 0 || volume > 2 ||
                outputWidth < 64 || outputHeight < 64 || outputWidth > 3840 || outputHeight > 3840 || (long)outputWidth * outputHeight > 3840L * 2160 ||
                destination.isEmpty() || destination.contains("\n") || destination.contains("\r") || destination.indexOf('\0') >= 0) throw new IllegalArgumentException("设置值无效");
            String type = new String[]{"image", "slideshow", "video"}[source.getSelectedItemPosition()];
            String ini = "enabled=" + enabled.isChecked() + "\nsource=" + type + "\nsource_path=" + selectedPath + "\nimage_paths=" + selectedImages +
                "\nwall_screen_enabled=" + wallScreen.isChecked() + "\ndesk_screen_enabled=" + deskScreen.isChecked() +
                "\ndesk_fit_mode=" + new String[]{"fit", "crop", "stretch"}[deskFit.getSelectedItemPosition()] +
                "\ntarget_object=" + destination + "\naudio_enabled=" + audio.isChecked() + "\nloop=" + loop.isChecked() +
                "\nslide_interval=" + seconds + "\ngain=" + volume + "\nwidth=" + outputWidth + "\nheight=" + outputHeight + "\nfps=" + captureFps + "\ntoggle_hotkey=" + hotkey(toggleHotkey) + "\npause_hotkey=" + hotkey(pauseHotkey) + "\n";
            if (!settings.edit().putString(ModuleSettings.MEDIA_SETTINGS, ini)
                    .putString(ModuleSettings.MEDIA_CONFIGURATION, enabled.isChecked() ? ini : "").commit()) throw new IOException("设置保存失败");
            refresh();
            return true;
        } catch (Exception error) {
            state.setText(error.getMessage());
            return false;
        }
    }

    private void refresh() {
        if (source.getSelectedItemPosition() == 1) {
            int count = selectedImages.isEmpty() ? 0 : selectedImages.split("\\|").length;
            state.setText(count > 0 ? "已选择 " + count + " 张轮播图片" : "未选择轮播图片");
        } else {
            if (selectedPath.isEmpty()) {
                state.setText("未选择媒体文件");
            } else {
                String filename = selectedPath.startsWith("@") ? selectedPath.substring(1) : selectedPath;
                state.setText("已就绪：" + filename);
            }
        }
    }

    private static String hotkey(EditText view) {
        String text = view.getText().toString().trim().toUpperCase(Locale.ROOT).replace(" ", "");
        if (text.isEmpty() || text.equals("OFF") || text.equals("DISABLED")) return "NONE";
        if (text.equals("+")) text = "PLUS";
        else if (text.endsWith("++")) text = text.substring(0, text.length() - 1) + "PLUS";
        if (!text.matches("NONE|(?:(?:CTRL|ALT|SHIFT|WIN)\\+)*(?:[A-Z0-9]|-|=|PLUS|MINUS|OEM_MINUS|OEM_PLUS|ENTER|NUMPAD_ENTER|SPACE|TAB|ESC|ADD|SUBTRACT|MULTIPLY|DIVIDE|DECIMAL|UP|DOWN|LEFT|RIGHT|NUMPAD[0-9]|F(?:[1-9]|1[0-9]|2[0-4]))"))
            throw new IllegalArgumentException("快捷键无效");
        return text;
    }

    private void setBusy(boolean value) {
        busy = value;
        choose.setEnabled(!value);
        if (saveButton != null) saveButton.setEnabled(!value);
        enabled.setAvailable(!value);
        audio.setAvailable(!value);
        loop.setAvailable(!value);
        wallScreen.setAvailable(!value);
        deskScreen.setAvailable(!value);
        deskFit.setEnabled(!value && deskScreen.isChecked());
        for (View control : new View[]{source, interval, gain, target, toggleHotkey, pauseHotkey, resolution, width, height}) {
            control.setEnabled(!value);
        }
    }

    @Override public void onPause() {
        save();
        super.onPause();
    }

    @Override protected void onActivityResult(int request, int result, Intent data) {
        super.onActivityResult(request, result, data);
        if (request != PICK || result != RESULT_OK || data == null || busy) return;
        ArrayList<Uri> uris = new ArrayList<>();
        if (data.getClipData() != null) {
            for (int i = 0; i < Math.min(256, data.getClipData().getItemCount()); ++i) {
                uris.add(data.getClipData().getItemAt(i).getUri());
            }
        } else if (data.getData() != null) {
            uris.add(data.getData());
        }
        if (uris.isEmpty()) return;
        int mode = source.getSelectedItemPosition();
        setBusy(true);
        state.setText("导入中...");
        new Thread(() -> {
            ArrayList<String> tokens = new ArrayList<>();
            ArrayList<File> imported = new ArrayList<>();
            try {
                FrameworkSettings.awaitConnection();
                JSONArray manifest = new JSONArray(settings.getString(MediaScreenFiles.INDEX, "[]"));
                if (manifest.length() + uris.size() > 256) throw new IOException("媒体文件数量已达上限");
                File root = new File(getFilesDir(), "media-screen");
                if (!root.isDirectory() && !root.mkdirs()) throw new IOException("无法创建媒体目录");
                for (Uri uri : uris) {
                    String mime = getContentResolver().getType(uri);
                    String extension = "image/png".equals(mime) ? ".png" : "image/jpeg".equals(mime) ? ".jpg" : "video/mp4".equals(mime) ? ".mp4" : "";
                    if (extension.isEmpty() || (mode == 2) != extension.equals(".mp4")) throw new IOException("请选择 PNG、JPEG 或 MP4 文件");
                    String name = "media-" + UUID.randomUUID() + extension;
                    File file = new File(root, name);
                    imported.add(file);
                    try (InputStream input = getContentResolver().openInputStream(uri); FileOutputStream output = new FileOutputStream(file)) {
                        if (input == null) throw new IOException("无法读取所选文件");
                        byte[] buffer = new byte[65536];
                        long total = 0;
                        int count;
                        long limit = mode == 2 ? 512L * 1024 * 1024 : 32L * 1024 * 1024;
                        while ((count = input.read(buffer)) != -1) {
                            total += count;
                            if (total > limit) throw new IOException("文件过大");
                            output.write(buffer, 0, count);
                        }
                        if (total == 0) throw new IOException("文件为空");
                        output.getFD().sync();
                    }
                    FrameworkSettings.publishMedia(file, name);
                    manifest.put(new JSONObject().put("remote", name).put("bytes", file.length()));
                    tokens.add("@" + name);
                }
                if (!settings.edit().putString(MediaScreenFiles.INDEX, manifest.toString()).commit()) throw new IOException("媒体索引保存失败");
                runOnUiThread(() -> {
                    if (mode == 1) selectedImages = String.join("|", tokens);
                    else selectedPath = tokens.get(0);
                    setBusy(false);
                    if (save()) {
                        state.setText("导入完成");
                        Toast.makeText(this, "媒体导入完成并已自动保存", Toast.LENGTH_SHORT).show();
                    }
                });
            } catch (Exception error) {
                for (File file : imported) {
                    FrameworkSettings.removeMedia(file.getName());
                    file.delete();
                }
                runOnUiThread(() -> {
                    setBusy(false);
                    state.setText("导入失败：" + error.getMessage());
                });
            }
        }, "BetterEndfieldNext-MediaImport").start();
    }
}
