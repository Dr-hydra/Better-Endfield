package dev.betterendfield.next;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.Bundle;
import android.view.View;
import android.widget.*;
import org.json.JSONArray;
import org.json.JSONObject;
import java.io.*;
import java.util.*;

/** File sources are configured here; takeover is explicitly triggered in-game. */
public final class MediaScreenActivity extends Activity {
    private static final int PICK = 71;
    private SharedPreferences settings;
    private Switch enabled, audio, loop, wallScreen, deskScreen;
    private Spinner source, resolution, deskFit;
    private EditText interval, gain, target, toggleHotkey, pauseHotkey, width, height;
    private LinearLayout customResolution;
    private static final int[][] RESOLUTIONS = {{1280, 720}, {1920, 1080}, {2560, 1440}, {3840, 2160}};
    private int captureFps = 60;
    private TextView state;
    private Button choose;
    private boolean loading, busy;
    private String selectedPath = "", selectedImages = "";
    @Override public void onCreate(Bundle saved) {
        super.onCreate(saved); settings = FrameworkSettings.open(this); loading = true;
        LinearLayout body = new LinearLayout(this); body.setOrientation(LinearLayout.VERTICAL); body.setPadding(32, 24, 32, 24);
        TextView title = new TextView(this); title.setText("会客室媒体屏"); title.setTextSize(24); body.addView(title);
        enabled = toggle(body, "启用媒体屏"); source = new Spinner(this);
        wallScreen = toggle(body, "大屏投屏（墙面轮播屏）"); deskScreen = toggle(body, "小屏投屏（桌面电脑）");
        TextView fitTitle = new TextView(this); fitTitle.setText("小屏画面适配"); body.addView(fitTitle);
        deskFit = new Spinner(this);
        deskFit.setAdapter(new ArrayAdapter<>(this, android.R.layout.simple_spinner_dropdown_item,
                new String[]{"完整显示（保持比例，留黑边）", "裁剪填满（保持比例）", "拉伸填满"})); body.addView(deskFit);
        source.setAdapter(new ArrayAdapter<>(this, android.R.layout.simple_spinner_dropdown_item, new String[]{"图片", "图片轮播", "视频文件"})); body.addView(source);
        choose = new Button(this); choose.setText("导入文件"); body.addView(choose);
        audio = toggle(body, "播放视频声音"); loop = toggle(body, "循环播放");
        interval = edit(body, "轮播间隔（秒）", "10"); gain = edit(body, "音量", "1");
        target = edit(body, "屏幕对象路径", "ReceptionRoomImagePoster");
        TextView resolutionTitle = new TextView(this); resolutionTitle.setText("输出分辨率"); body.addView(resolutionTitle);
        resolution = new Spinner(this);
        resolution.setAdapter(new ArrayAdapter<>(this, android.R.layout.simple_spinner_dropdown_item,
                new String[]{"720p · 1280 × 720", "1080p · 1920 × 1080", "1440p · 2560 × 1440", "4K · 3840 × 2160", "自定义"})); body.addView(resolution);
        customResolution = new LinearLayout(this); customResolution.setOrientation(LinearLayout.VERTICAL); body.addView(customResolution);
        width = edit(customResolution, "画布宽度", "1920"); height = edit(customResolution, "画布高度", "1080");
        toggleHotkey = edit(body, "启停快捷键", "-"); pauseHotkey = edit(body, "暂停／继续快捷键", "");
        state = new TextView(this); body.addView(state);
        ScrollView scroll = new ScrollView(this); scroll.addView(body); setContentView(scroll);
        Map<String, String> values = values(settings.getString(ModuleSettings.MEDIA_SETTINGS,
                settings.getString(ModuleSettings.MEDIA_CONFIGURATION, "")));
        enabled.setChecked(!values.isEmpty() && "true".equals(values.get("enabled")));
        wallScreen.setChecked(!"false".equals(values.get("wall_screen_enabled")) && !"0".equals(values.get("wall_screen_enabled")));
        deskScreen.setChecked(!"false".equals(values.get("desk_screen_enabled")) && !"0".equals(values.get("desk_screen_enabled")));
        deskFit.setSelection("crop".equals(values.get("desk_fit_mode")) ? 1 : "stretch".equals(values.get("desk_fit_mode")) ? 2 : 0);
        deskFit.setEnabled(deskScreen.isChecked());
        audio.setChecked(!"false".equals(values.get("audio_enabled"))); loop.setChecked(!"false".equals(values.get("loop")));
        source.setSelection("video".equals(values.get("source")) ? 2 : "slideshow".equals(values.get("source")) ? 1 : 0);
        interval.setText(values.getOrDefault("slide_interval", "10")); gain.setText(values.getOrDefault("gain", "1"));
        target.setText(values.getOrDefault("target_object", "ReceptionRoomImagePoster"));
        toggleHotkey.setText(values.getOrDefault("toggle_hotkey", "-"));
        String pauseBinding = values.getOrDefault("pause_hotkey", ""); pauseHotkey.setText("NONE".equals(pauseBinding) ? "" : pauseBinding);
        width.setText(values.getOrDefault("width", "1920")); height.setText(values.getOrDefault("height", "1080"));
        try { captureFps = Math.max(1, Math.min(120, Integer.parseInt(values.getOrDefault("fps", "60")))); } catch (NumberFormatException ignored) {}
        int preset = RESOLUTIONS.length;
        for (int i = 0; i < RESOLUTIONS.length; i++)
            if (width.getText().toString().equals(Integer.toString(RESOLUTIONS[i][0])) && height.getText().toString().equals(Integer.toString(RESOLUTIONS[i][1]))) preset = i;
        resolution.setSelection(preset); customResolution.setVisibility(preset == RESOLUTIONS.length ? View.VISIBLE : View.GONE);
        selectedPath = values.getOrDefault("source_path", ""); selectedImages = values.getOrDefault("image_paths", ""); loading = false;
        for (Switch toggle : new Switch[]{enabled, audio, loop, wallScreen, deskScreen}) toggle.setOnCheckedChangeListener((button, checked) -> {
            deskFit.setEnabled(deskScreen.isChecked() && !busy); save();
        });
        deskFit.setOnItemSelectedListener(new android.widget.AdapterView.OnItemSelectedListener() {
            public void onItemSelected(android.widget.AdapterView<?> p, View view, int position, long id) { save(); }
            public void onNothingSelected(android.widget.AdapterView<?> p) {}
        });
        source.setOnItemSelectedListener(new android.widget.AdapterView.OnItemSelectedListener() {
            public void onItemSelected(android.widget.AdapterView<?> p, View view, int position, long id) { save(); }
            public void onNothingSelected(android.widget.AdapterView<?> p) {}
        });
        resolution.setOnItemSelectedListener(new android.widget.AdapterView.OnItemSelectedListener() {
            public void onItemSelected(android.widget.AdapterView<?> p, View view, int position, long id) {
                if (position < RESOLUTIONS.length) { width.setText(Integer.toString(RESOLUTIONS[position][0])); height.setText(Integer.toString(RESOLUTIONS[position][1])); }
                customResolution.setVisibility(position == RESOLUTIONS.length ? View.VISIBLE : View.GONE); save();
            }
            public void onNothingSelected(android.widget.AdapterView<?> p) {}
        });
        for (EditText edit : new EditText[]{interval, gain, target, toggleHotkey, pauseHotkey, width, height}) edit.setOnFocusChangeListener((v, focus) -> { if (!focus) save(); });
        choose.setOnClickListener(v -> {
            if (busy) return;
            Intent intent = new Intent(Intent.ACTION_OPEN_DOCUMENT).addCategory(Intent.CATEGORY_OPENABLE)
                .setType(source.getSelectedItemPosition() == 2 ? "video/mp4" : "image/*")
                .putExtra(Intent.EXTRA_ALLOW_MULTIPLE, source.getSelectedItemPosition() == 1);
            startActivityForResult(intent, PICK);
        });
        refresh();
    }
    private Switch toggle(LinearLayout body, String label) { Switch view = new Switch(this); view.setText(label); body.addView(view); return view; }
    private EditText edit(LinearLayout body, String label, String initial) {
        TextView text = new TextView(this); text.setText(label); body.addView(text);
        EditText view = new EditText(this); view.setSingleLine(true); view.setText(initial); body.addView(view); return view;
    }
    static Map<String, String> values(String ini) {
        Map<String, String> values = new HashMap<>();
        for (String line : ini.split("\\n")) { int equal = line.indexOf('='); if (equal > 0) values.put(line.substring(0, equal), line.substring(equal + 1).trim()); }
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
        } catch (Exception error) { state.setText(error.getMessage()); return false; }
    }
    private void refresh() { state.setText(selectedPath.isEmpty() && selectedImages.isEmpty() ? "未选择文件" : "文件已选择"); }
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
        for (View control : new View[]{choose, enabled, audio, loop, source, interval, gain, target, toggleHotkey, pauseHotkey, resolution, width, height, wallScreen, deskScreen}) control.setEnabled(!value);
        deskFit.setEnabled(!value && deskScreen.isChecked());
    }
    @Override public void onPause() { save(); super.onPause(); }
    @Override protected void onActivityResult(int request, int result, Intent data) {
        super.onActivityResult(request, result, data);
        if (request != PICK || result != RESULT_OK || data == null || busy) return;
        ArrayList<Uri> uris = new ArrayList<>();
        if (data.getClipData() != null) for (int i = 0; i < Math.min(256, data.getClipData().getItemCount()); ++i) uris.add(data.getClipData().getItemAt(i).getUri());
        else if (data.getData() != null) uris.add(data.getData());
        if (uris.isEmpty()) return;
        int mode = source.getSelectedItemPosition(); setBusy(true); state.setText("导入中");
        new Thread(() -> {
            ArrayList<String> tokens = new ArrayList<>(); ArrayList<File> imported = new ArrayList<>();
            try {
                FrameworkSettings.awaitConnection(); JSONArray manifest = new JSONArray(settings.getString(MediaScreenFiles.INDEX, "[]"));
                if (manifest.length() + uris.size() > 256) throw new IOException("媒体文件数量已达上限");
                File root = new File(getFilesDir(), "media-screen"); if (!root.isDirectory() && !root.mkdirs()) throw new IOException("无法创建媒体目录");
                for (Uri uri : uris) {
                    String mime = getContentResolver().getType(uri);
                    String extension = "image/png".equals(mime) ? ".png" : "image/jpeg".equals(mime) ? ".jpg" : "video/mp4".equals(mime) ? ".mp4" : "";
                    if (extension.isEmpty() || (mode == 2) != extension.equals(".mp4")) throw new IOException("请选择 PNG、JPEG 或 MP4 文件");
                    String name = "media-" + UUID.randomUUID() + extension; File file = new File(root, name); imported.add(file);
                    try (InputStream input = getContentResolver().openInputStream(uri); FileOutputStream output = new FileOutputStream(file)) {
                        if (input == null) throw new IOException("无法读取所选文件");
                        byte[] buffer = new byte[65536]; long total = 0; int count; long limit = mode == 2 ? 512L * 1024 * 1024 : 32L * 1024 * 1024;
                        while ((count = input.read(buffer)) != -1) { total += count; if (total > limit) throw new IOException("文件过大"); output.write(buffer, 0, count); }
                        if (total == 0) throw new IOException("文件为空"); output.getFD().sync();
                    }
                    FrameworkSettings.publishMedia(file, name);
                    manifest.put(new JSONObject().put("remote", name).put("bytes", file.length())); tokens.add("@" + name);
                }
                if (!settings.edit().putString(MediaScreenFiles.INDEX, manifest.toString()).commit()) throw new IOException("媒体索引保存失败");
                runOnUiThread(() -> { if (mode == 1) selectedImages = String.join("|", tokens); else selectedPath = tokens.get(0);
                    setBusy(false); if (save()) state.setText("导入完成"); });
            } catch (Exception error) {
                for (File file : imported) { FrameworkSettings.removeMedia(file.getName()); file.delete(); }
                runOnUiThread(() -> { setBusy(false); state.setText("导入失败：" + error.getMessage()); });
            }
        }, "BetterEndfieldNext-MediaImport").start();
    }
}
