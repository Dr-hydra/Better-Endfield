package dev.betterendfield.next;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.*;
import java.util.*;

/** Review cached sources and explicitly confirm an immutable work installation. */
public final class MmdImportActivity extends Activity {
    private static final int ARCHIVE = 71, DIRECTORY = 72, FILE = 73;
    private final Handler handler = new Handler(Looper.getMainLooper());
    private final List<View> controls = new ArrayList<>();
    private final Map<String, Button> slots = new LinkedHashMap<>();
    private MmdImportSession session;
    private Spinner workPicker;
    private EditText name;
    private TextView status, results;
    private Button confirm;
    private boolean rendering;
    private int rendered = -1;
    private String pendingSlot;
    private Bundle restored;
    private final Runnable poll = new Runnable() {
        @Override public void run() {
            if (session.plan != null && rendered != session.revision) render();
            boolean available = !session.busy && !MmdInstaller.busy && !session.cancelled;
            for (View control : controls) control.setEnabled(available);
            workPicker.setEnabled(available && session.plan != null && session.plan.works.size() > 1);
            confirm.setEnabled(available && session.plan != null);
            String message = session.status;
            status.setText(message); status.setVisibility(message.isEmpty() ? View.GONE : View.VISIBLE);
            if (session.installed) { session.dispose(); finish(); return; }
            handler.postDelayed(this, 250);
        }
    };
    @Override protected void onCreate(Bundle state) {
        super.onCreate(state);
        restored = state;
        session = MmdImportSession.open(getApplicationContext(), state == null ? null : state.getString("session"));
        pendingSlot = state == null ? null : state.getString("pendingSlot");
        ScrollView scroll = new ScrollView(this); scroll.setFillViewport(true);
        scroll.setBackgroundColor(getColor(R.color.app_background));
        LinearLayout page = new LinearLayout(this); page.setOrientation(LinearLayout.VERTICAL);
        page.setPadding(dp(16), dp(16), dp(16), dp(24)); scroll.addView(page);
        Button back = button("‹ 返回"); back.setOnClickListener(view -> { session.cancel(); finish(); });
        page.addView(back, SectionCard.stacked(this, 0));
        TextView title = label("导入 MMD"); title.setTextSize(22); page.addView(title, SectionCard.stacked(this, 12));
        Button archive = button("选择 ZIP / 7z"); archive.setOnClickListener(view -> pick(ARCHIVE));
        page.addView(archive, SectionCard.stacked(this, 12)); controls.add(archive);
        Button directory = button("选择目录"); directory.setOnClickListener(view -> pick(DIRECTORY));
        page.addView(directory, SectionCard.stacked(this, 6)); controls.add(directory);
        page.addView(label("作品"), SectionCard.stacked(this, 12));
        workPicker = new Spinner(this); workPicker.setBackgroundResource(R.drawable.bg_input); workPicker.setMinimumHeight(dp(52));
        page.addView(workPicker); controls.add(workPicker);
        workPicker.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if (rendering || session.plan == null || position < 0 || position >= session.plan.works.size()
                        || position != workPicker.getSelectedItemPosition()) return;
                session.selected = position; renderWork();
            }
            @Override public void onNothingSelected(AdapterView<?> parent) { }
        });
        page.addView(label("作品名"), SectionCard.stacked(this, 12));
        name = new EditText(this); name.setSingleLine(true); name.setTextColor(getColor(R.color.text_primary));
        name.setBackgroundResource(R.drawable.bg_input); name.setMinimumHeight(dp(52));
        name.setSaveEnabled(false); page.addView(name); controls.add(name);
        name.addTextChangedListener(new TextWatcher() {
            @Override public void beforeTextChanged(CharSequence s, int start, int count, int after) { }
            @Override public void onTextChanged(CharSequence s, int start, int before, int count) { }
            @Override public void afterTextChanged(Editable value) {
                if (!rendering && session.plan != null) current().name = value.toString();
            }
        });
        results = label(""); results.setTextIsSelectable(true); page.addView(results, SectionCard.stacked(this, 12));
        for (String slot : MmdImportPlan.SLOTS) {
            page.addView(label(slotLabel(slot)), SectionCard.stacked(this, 12));
            LinearLayout row = new LinearLayout(this); row.setOrientation(LinearLayout.HORIZONTAL);
            Button choose = button("选择文件"); choose.setOnClickListener(view -> chooseSlot(slot));
            row.addView(choose, new LinearLayout.LayoutParams(0, -2, 1)); slots.put(slot, choose); controls.add(choose);
            Button clear = button("清空"); clear.setOnClickListener(view -> {
                if (session.plan != null) { current().slots.remove(slot); renderWork(); }
            }); row.addView(clear); controls.add(clear); page.addView(row);
        }
        status = label(""); page.addView(status, SectionCard.stacked(this, 12));
        confirm = button("导入"); confirm.setOnClickListener(view -> {
            if (session.plan == null) return;
            try {
                MmdImportPlan.Work work = current();
                session.plan.validate(work.name, work.slots);
                if (!MmdInstaller.start(this, session, work.name, work.slots, work.settings)) session.status = "请等待当前操作完成";
            } catch (Exception error) { session.status = error.getMessage(); }
        }); page.addView(confirm, SectionCard.stacked(this, 12)); controls.add(confirm);
        setContentView(scroll);
        scroll.setOnApplyWindowInsetsListener((view, insets) -> {
            view.setPadding(insets.getSystemWindowInsetLeft(), insets.getSystemWindowInsetTop(), insets.getSystemWindowInsetRight(), insets.getSystemWindowInsetBottom()); return insets;
        }); scroll.requestApplyInsets();
    }
    private void pick(int request) {
        Intent intent;
        if (request == DIRECTORY) intent = new Intent(Intent.ACTION_OPEN_DOCUMENT_TREE);
        else {
            intent = new Intent(Intent.ACTION_OPEN_DOCUMENT).addCategory(Intent.CATEGORY_OPENABLE).setType("*/*");
            if (request == ARCHIVE) intent.putExtra(Intent.EXTRA_MIME_TYPES, new String[]{"application/zip", "application/x-zip-compressed", "application/x-7z-compressed", "application/octet-stream"});
            // VMD providers use inconsistent MIME types; inspect the copied bytes instead.
        }
        intent.addFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION | Intent.FLAG_GRANT_PERSISTABLE_URI_PERMISSION);
        try { startActivityForResult(intent, request); }
        catch (android.content.ActivityNotFoundException error) { session.status = "文件选择器不可用"; }
    }
    private void chooseSlot(String slot) {
        List<MmdImportPlan.Asset> candidates = new ArrayList<>();
        if (session.plan != null) for (MmdImportPlan.Asset asset : session.plan.assets) if (asset.supports(slot)) candidates.add(asset);
        String[] labels = new String[candidates.size() + 1];
        labels[0] = "选择外部文件";
        for (int i = 0; i < candidates.size(); i++) labels[i + 1] = candidates.get(i).label();
        new AlertDialog.Builder(this).setTitle(slotLabel(slot)).setItems(labels, (dialog, index) -> {
            if (session.busy || MmdInstaller.busy) return;
            if (index == 0) { pendingSlot = slot; pick(FILE); }
            else { current().slots.put(slot, candidates.get(index - 1).path); renderWork(); }
        }).setNegativeButton("取消", null).show();
    }
    @Override protected void onActivityResult(int request, int result, Intent data) {
        super.onActivityResult(request, result, data);
        if (result != RESULT_OK || data == null || data.getData() == null) return;
        if (request != ARCHIVE && request != DIRECTORY && request != FILE) return;
        String slot = request == FILE ? pendingSlot : null;
        if (request == FILE && slot == null) { session.status = "请重新选择文件"; return; }
        session.prepare(this, data.getData(), data.getFlags(), request == DIRECTORY, slot);
        pendingSlot = null;
    }
    private void render() {
        rendering = true;
        try {
            if (restored != null) {
                session.selected = Math.min(restored.getInt("selected", 0), session.plan.works.size() - 1);
                restored = null;
            }
            List<String> names = new ArrayList<>();
            for (MmdImportPlan.Work work : session.plan.works) names.add(work.name);
            ArrayAdapter<String> adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_item, names);
            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            workPicker.setAdapter(adapter); workPicker.setSelection(session.selected, false);
            rendered = session.revision;
        } finally { rendering = false; }
        renderWork();
    }
    private MmdImportPlan.Work current() { return session.plan.works.get(session.selected); }
    private void renderWork() {
        if (session.plan == null) return;
        rendering = true; name.setText(current().name); rendering = false;
        for (String slot : MmdImportPlan.SLOTS) {
            MmdImportPlan.Asset asset = session.plan.asset(current().slots.get(slot));
            slots.get(slot).setText(asset == null ? "选择文件" : asset.display());
        }
        StringBuilder summary = new StringBuilder();
        for (Map.Entry<String, String> selected : current().slots.entrySet()) {
            MmdImportPlan.Asset asset = session.plan.asset(selected.getValue());
            if (asset != null) summary.append(slotLabel(selected.getKey())).append("：").append(asset.label()).append('\n');
        }
        for (String warning : session.plan.warnings) summary.append(warning).append('\n');
        results.setText(summary); results.setVisibility(summary.length() == 0 ? View.GONE : View.VISIBLE);
    }
    @Override protected void onSaveInstanceState(Bundle state) {
        super.onSaveInstanceState(state); state.putString("session", session.id); state.putString("pendingSlot", pendingSlot);
        state.putInt("selected", session.selected);
        session.saveEdits();
    }
    @Override protected void onResume() { super.onResume(); handler.post(poll); }
    @Override protected void onPause() { handler.removeCallbacks(poll); session.saveEdits(); super.onPause(); }
    @Override protected void onDestroy() {
        handler.removeCallbacksAndMessages(null);
        if (isFinishing() && !session.installed) session.cancel();
        super.onDestroy();
    }
    private static String slotLabel(String slot) {
        if (slot.equals("camera")) return "镜头"; if (slot.equals("music")) return "音乐";
        String number = slot.replaceAll("[^0-9]", "");
        return (slot.startsWith("motion") ? "动作" : "表情") + (number.isEmpty() ? "" : " " + number);
    }
    private Button button(String text) {
        Button button = new Button(this); button.setText(text); button.setAllCaps(false);
        button.setTextColor(getColor(R.color.text_primary)); button.setBackgroundResource(R.drawable.bg_ghost_button); button.setMinimumHeight(dp(52)); return button;
    }
    private TextView label(String text) {
        TextView view = new TextView(this); view.setText(text); view.setTextColor(getColor(R.color.text_secondary)); view.setTextSize(13); return view;
    }
    private int dp(int value) { return Math.round(value * getResources().getDisplayMetrics().density); }
}
