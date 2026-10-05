package dev.betterendfield.android;

import android.app.Activity;
import android.content.res.ColorStateList;
import android.graphics.drawable.GradientDrawable;
import android.os.Bundle;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.SeekBar;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;
import org.json.JSONArray;
import org.json.JSONObject;
import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Set;
import java.util.TreeSet;
import java.util.function.IntConsumer;

/** Two independent tabs, sharing only a settings transport, never package materialization. */
final class OverlaySettingsPage {
    private static final int INK = 0xFFF2F2EF, GOLD = 0xFFE6CD82, EDGE = 0xFF3B434A;
    final LinearLayout root;
    private final Activity activity;
    private final boolean models, preview;
    private final Set<String> expanded;
    private final ArrayList<View> mutations = new ArrayList<>();
    private String character, revision = "", index = "[]";
    private boolean closed, reading, writing, busy, tracking, dirty, fovEnabled, updating, fovReady, hot;
    private float fov = 60;
    private float savedFov = 60;
    private boolean savedFovEnabled, renderedHot;
    private int render;
    private long sequence;
    private Switch fovSwitch;
    private SeekBar fovSlider;
    private TextView fovLabel, modelState;
    private Runnable finishDrag;

    OverlaySettingsPage(Activity activity, boolean models, boolean preview, Set<String> expanded, String character) {
        this.activity = activity; this.models = models; this.preview = preview;
        this.expanded = expanded; this.character = character;
        root = column();
        if (models) renderModels(); else buildFov();
        refresh();
    }
    String character() { return character; }
    void runtime(RuntimeSnapshot snapshot) {
        fovReady = snapshot.ready("betterendfield.camera") && snapshot.number("camera.global_fov_ready") == 1;
        boolean nextHot = BemHotSwitchUpdater.running && snapshot.ready("betterendfield.custom_model");
        hot = nextHot;
        if (models) {
            if (renderedHot != hot && !tracking && !writing) renderModels();
            if (modelState != null) modelState.setText(preview ? "预览模式" : hot ? "热切换：已启用" : "热切换：未启用");
            // The saved enabled switches stay editable when hot switching was not loaded at startup.
            updateEnabled();
        } else updateFov();
    }
    void refresh() {
        if (closed || reading || writing || tracking) return;
        reading = true;
        long request = ++sequence;
        OverlaySettingsClient.call(activity, models ? "read_models" : "read_fov", null, null, reply -> {
            reading = false;
            if (closed || request != sequence || tracking) return;
            if (reply.getBoolean("ok")) accept(reply);
            else { revision = ""; updateEnabled(); if (!models) updateFov(); }
        });
    }
    private void accept(Bundle reply) {
        revision = reply.getString("revision", ""); busy = reply.getBoolean("busy");
        if (models) {
            String next = reply.getString("index", "[]");
            if (!next.equals(index) || root.getChildCount() < 3) { index = next; renderModels(); }
            updateEnabled();
        } else {
            fovEnabled = reply.getBoolean("enabled");
            fov = (float) Math.max(5, Math.min(150, ModuleSettings.parse(reply.getString("value", "60"), 60)));
            savedFovEnabled = fovEnabled; savedFov = fov;
            updateFov();
        }
    }
    private void save(String method, JSONObject patch) {
        if (preview || writing || revision.isEmpty() || (models && busy)) return;
        ++sequence; writing = true; updateEnabled(); if (!models) updateFov();
        OverlaySettingsClient.call(activity, method, revision, patch == null ? null : patch.toString(), reply -> {
            writing = false;
            // A closed tab still rolls a failed FOV preview back to the authoritative module setting.
            if (reply.getBoolean("ok")) {
                if (!models) applyFov(reply.getBoolean("enabled"), (float) ModuleSettings.parse(reply.getString("value", "60"), 60));
                if (!closed) { accept(reply); if (models) renderModels(); }
            } else {
                writing = true;
                if (!models) { fovEnabled = savedFovEnabled; fov = savedFov; applyFov(fovEnabled, fov); updateFov(); }
                else if (!closed) renderModels();
                if (!closed) Toast.makeText(activity, reply.getString("error", "设置未保存"), Toast.LENGTH_SHORT).show();
                OverlaySettingsClient.call(activity, models ? "read_models" : "read_fov", null, null, restored -> {
                    writing = false;
                    if (!models && restored.getBoolean("ok")) applyFov(restored.getBoolean("enabled"),
                            (float) ModuleSettings.parse(restored.getString("value", "60"), 60));
                    if (!closed) {
                        if (restored.getBoolean("ok")) { accept(restored); if (models) renderModels(); }
                        else { revision = ""; updateEnabled(); if (!models) updateFov(); }
                    }
                });
            }
        });
    }
    private void modelPatch(String generation, String field, Object value) {
        try { save("edit_models", new JSONObject().put("generation", generation).put(field, value)); }
        catch (Exception error) { fail(error); }
    }
    private void updateEnabled() {
        boolean enabled = !preview && !writing && !busy && !revision.isEmpty();
        for (View view : mutations) { view.setEnabled(enabled); view.setAlpha(enabled ? 1 : 0.4f); }
    }
    private void buildFov() {
        fovSwitch = new Switch(activity); fovSwitch.setTextColor(INK); fovSwitch.setTextSize(14);
        fovSwitch.setMinimumHeight(dp(48)); root.addView(fovSwitch);
        fovSwitch.setOnCheckedChangeListener((button, checked) -> {
            if (updating) return;
            fovEnabled = checked;
            applyFov(fovEnabled, fov);
            try { save("edit_fov", new JSONObject().put("enabled", checked)); }
            catch (Exception error) { fail(error); }
        });
        fovLabel = label(""); root.addView(fovLabel);
        fovSlider = slider(145, 55); root.addView(fovSlider, height(48));
        fovSlider.setContentDescription("全局 FOV");
        bindSlider(fovSlider, progress -> {
            fov = 5 + progress; fovLabel.setText("全局 FOV  " + (int) fov + "°");
            dirty = true; applyFov(fovEnabled, fov);
        }, () -> {
            if (!dirty) return; dirty = false;
            try { save("edit_fov", new JSONObject().put("value", fov)); }
            catch (Exception error) { fail(error); }
        });
        updateFov();
    }
    private void applyFov(boolean enabled, float value) {
        if (preview || !RuntimeBootstrap.loaded()) return;
        try { NativeCommandBridge.globalFov(enabled, value); }
        catch (RuntimeException | LinkageError error) { android.util.Log.e("BetterEndfield.Overlay", "FOV bridge failed", error); }
    }
    private void updateFov() {
        if (models || fovSwitch == null) return;
        updating = true;
        fovSwitch.setChecked(fovEnabled);
        boolean loaded = RuntimeBootstrap.loaded();
        fovSwitch.setText(preview ? "全局 FOV（预览）" : !loaded ? "全局 FOV（运行时未加载）"
                : fovReady ? "全局 FOV" : "全局 FOV（重启后生效）");
        boolean editable = !preview && loaded && !writing && !revision.isEmpty();
        fovSwitch.setEnabled(editable); fovSwitch.setAlpha(editable ? 1 : 0.4f);
        fovSlider.setEnabled(editable && fovEnabled); fovSlider.setAlpha(editable && fovEnabled ? 1 : 0.4f);
        if (!tracking) fovSlider.setProgress(Math.round(fov) - 5);
        fovLabel.setText("全局 FOV  " + Math.round(fov) + "°"); updating = false;
    }
    private void renderModels() {
        renderedHot = hot;
        ++render; mutations.clear(); root.removeAllViews();
        modelState = label(preview ? "预览模式" : hot ? "热切换：已启用" : "热切换：未启用"); root.addView(modelState);
        try {
            JSONArray entries = new JSONArray(index); TreeSet<String> characters = new TreeSet<>(); boolean enabled = false;
            for (int i = 0; i < entries.length(); ++i) {
                JSONObject entry = entries.getJSONObject(i); characters.add(BemOptions.targetKey(entry));
                enabled |= entry.optBoolean("enabled", true);
            }
            if (!characters.contains(character)) character = "";
            TextView filter = button(character.isEmpty() ? "全部模型 ▾" : BemOptions.targetLabel(character) + " ▾"); root.addView(filter, height(44));
            filter.setOnClickListener(v -> {
                String[] ids = new String[characters.size() + 1]; ids[0] = ""; int i = 1;
                for (String id : characters) ids[i++] = id;
                String[] names = ids.clone(); names[0] = "全部模型";
                for (int n = 1; n < names.length; ++n) names[n] = BemOptions.targetLabel(ids[n]);
                new android.app.AlertDialog.Builder(activity).setTitle("角色或武器筛选").setItems(names, (dialog, which) -> {
                    character = ids[which]; renderModels();
                }).show();
            });
            TextView disable = button("关闭全部"); root.addView(disable, height(44));
            disable.setOnClickListener(v -> save("disable_models", null));
            if (enabled) mutations.add(disable); else { disable.setEnabled(false); disable.setAlpha(0.4f); }
            if (entries.length() == 0) root.addView(label("无已安装模型"));
            for (int i = 0; i < entries.length(); ++i) {
                JSONObject entry = entries.getJSONObject(i);
                if (!character.isEmpty() && !character.equals(BemOptions.targetKey(entry))) continue;
                addModel(entry);
            }
            updateEnabled();
        } catch (Exception error) { revision = ""; root.addView(label("模型列表不可用")); updateEnabled(); }
    }
    private void addModel(JSONObject entry) throws Exception {
        String generation = entry.getString("generation"); final int version = render;
        LinearLayout card = column(); card.setPadding(dp(10), dp(8), dp(10), dp(8)); card.setBackground(background());
        LinearLayout.LayoutParams space = new LinearLayout.LayoutParams(-1, -2); space.topMargin = dp(10); root.addView(card, space);
        card.addView(label(entry.getString("name")));
        Switch enabled = new Switch(activity); enabled.setTextColor(INK); enabled.setTextSize(13); enabled.setMinimumHeight(dp(48));
        enabled.setText(BemOptions.targetLabel(BemOptions.targetKey(entry)) + " · " + (hot ? "启用（下次加载生效）" : "启用（重启后生效）"));
        enabled.setChecked(entry.optBoolean("enabled", true)); card.addView(enabled); mutations.add(enabled);
        enabled.setOnCheckedChangeListener((v, checked) -> { if (version == render) modelPatch(generation, "enabled", checked); });
        TextView details = button(expanded.contains(generation) ? "收起详细选项 ▴" : "详细选项 ▾"); card.addView(details, height(44));
        details.setOnClickListener(v -> {
            if (!expanded.remove(generation)) expanded.add(generation); renderModels();
        });
        if (!expanded.contains(generation)) return;
        if (entry.optInt("bem_minor", 0) < 1) {
            JSONArray apps = entry.getJSONArray("appearances"); String[] ids = new String[apps.length()];
            for (int i = 0; i < ids.length; ++i) ids[i] = apps.getString(i);
            choice(card, "外观", ids, ids, entry.optString("selected_appearance", entry.getString("default_appearance")),
                    value -> { if (version == render) modelPatch(generation, "appearance", value); });
            return;
        }
        LinkedHashMap<String, String> options = BemOptions.parse(entry, entry.optString("selected_options", entry.getString("default_options")));
        Map<String, String> effective = BemOptions.effective(entry, options);
        JSONArray groups = entry.getJSONArray("option_groups");
        for (int i = 0; i < groups.length(); ++i) {
            JSONObject group = groups.getJSONObject(i); String id = group.getString("id");
            if (!effective.containsKey(id)) continue;
            JSONArray choices = group.getJSONArray("choices"); String[] ids = new String[choices.length()], labels = ids.clone();
            for (int j = 0; j < ids.length; ++j) { ids[j] = choices.getJSONObject(j).getString("id"); labels[j] = choices.getJSONObject(j).getString("name"); }
            choice(card, group.getString("name"), ids, labels, options.get(id), value -> {
                if (version != render) return;
                LinkedHashMap<String, String> next = new LinkedHashMap<>(options); next.put(id, value);
                try { BemOptions.parse(entry, BemOptions.encode(next)); modelPatch(generation, "options", BemOptions.encode(next)); }
                catch (Exception error) { fail(error); }
            });
        }
        JSONArray parameters = BemParameters.groups(entry);
        LinkedHashMap<String, Integer> values = BemParameters.parse(entry, entry.optString("selected_parameters", entry.optString("default_parameters", "")));
        for (int i = 0; i < parameters.length(); ++i) {
            JSONObject parameter = parameters.getJSONObject(i); if (!BemParameters.available(entry, parameter, options)) continue;
            String id = parameter.getString("id"), name = parameter.getString("name");
            int min = BemParameters.tick(parameter, "min"), max = BemParameters.tick(parameter, "max"), step = BemParameters.tick(parameter, "step");
            TextView title = label(name + "  " + values.get(id) / 1000.0); card.addView(title);
            SeekBar slider = slider((max - min) / step, (values.get(id) - min) / step); mutations.add(slider); card.addView(slider, height(48));
            slider.setContentDescription(name);
            final boolean[] changed = {false};
            bindSlider(slider, progress -> {
                if (version != render) return;
                values.put(id, min + progress * step); title.setText(name + "  " + values.get(id) / 1000.0); changed[0] = true;
            }, () -> {
                if (version == render && changed[0]) { changed[0] = false; modelPatch(generation, "parameters", BemParameters.encode(values)); }
            });
        }
        if (parameters.length() > 0) {
            TextView reset = button("作者默认值"); card.addView(reset, height(44)); mutations.add(reset);
            reset.setOnClickListener(v -> { if (version == render) modelPatch(generation, "parameters", entry.optString("default_parameters", "")); });
        }
    }
    private void choice(LinearLayout parent, String title, String[] ids, String[] names, String selected, java.util.function.Consumer<String> changed) {
        int active = 0; for (int i = 0; i < ids.length; ++i) if (ids[i].equals(selected)) active = i;
        TextView view = button(title + " · " + (names.length == 0 ? "—" : names[active]) + " ▾"); parent.addView(view, height(48)); mutations.add(view);
        final int version = render;
        view.setOnClickListener(v -> new android.app.AlertDialog.Builder(activity).setTitle(title).setItems(names, (dialog, which) -> {
            if (version == render && !writing && !closed && !ids[which].equals(selected)) changed.accept(ids[which]);
        }).show());
    }
    private void bindSlider(SeekBar slider, IntConsumer changed, Runnable save) {
        slider.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
            @Override public void onProgressChanged(SeekBar bar, int progress, boolean fromUser) {
                if (!fromUser || updating) return; changed.accept(progress); if (!tracking) save.run();
            }
            @Override public void onStartTrackingTouch(SeekBar bar) {
                tracking = true; finishDrag = save; bar.getParent().requestDisallowInterceptTouchEvent(true);
            }
            @Override public void onStopTrackingTouch(SeekBar bar) {
                tracking = false; finishDrag = null; bar.getParent().requestDisallowInterceptTouchEvent(false); save.run();
            }
        });
        slider.setOnTouchListener((view, event) -> {
            if (event.getActionMasked() == MotionEvent.ACTION_CANCEL) finishGesture(); return false;
        });
    }
    void finishGesture() {
        Runnable save = finishDrag; finishDrag = null; tracking = false;
        root.requestDisallowInterceptTouchEvent(false); if (save != null) save.run();
    }
    void close() { finishGesture(); closed = true; }
    private void fail(Exception error) { Toast.makeText(activity, error.getMessage(), Toast.LENGTH_SHORT).show(); }
    private int dp(int value) { return Math.round(value * activity.getResources().getDisplayMetrics().density); }
    private LinearLayout column() { LinearLayout v = new LinearLayout(activity); v.setOrientation(LinearLayout.VERTICAL); return v; }
    private LinearLayout.LayoutParams height(int value) { return new LinearLayout.LayoutParams(-1, dp(value)); }
    private TextView label(String value) { TextView v = new TextView(activity); v.setText(value); v.setTextColor(GOLD); v.setTextSize(13); v.setPadding(0, dp(8), 0, dp(8)); return v; }
    private TextView button(String value) { TextView v = label(value); v.setTextColor(INK); v.setPadding(dp(8), dp(4), dp(8), dp(4)); v.setGravity(Gravity.CENTER_VERTICAL); v.setBackground(background()); return v; }
    private GradientDrawable background() { GradientDrawable d = new GradientDrawable(); d.setColor(0xFF242B31); d.setCornerRadius(dp(8)); d.setStroke(dp(1), EDGE); return d; }
    private SeekBar slider(int max, int value) {
        SeekBar bar = new SeekBar(activity); bar.setMax(max); bar.setProgress(value);
        bar.setProgressTintList(ColorStateList.valueOf(GOLD)); bar.setThumbTintList(ColorStateList.valueOf(GOLD));
        bar.setProgressBackgroundTintList(ColorStateList.valueOf(EDGE)); return bar;
    }
}
