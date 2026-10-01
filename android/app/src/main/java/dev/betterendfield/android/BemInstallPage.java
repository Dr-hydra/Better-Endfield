package dev.betterendfield.android;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.*;
import android.view.View;
import android.widget.*;
import org.json.*;

/** Reusable BEM management content for the main tab and external file entry. */
final class BemInstallPage {
    private final Activity activity;
    private final View root;
    private boolean active;
    private static final int PICK=101;
    private static final String STATE_PENDING_URI="bem.pending_uri";
    private static final String STATE_INCOMING_NOTICE="bem.incoming_notice";
    private Uri pendingImport;
    private View incoming;
    private TextView incomingNotice;
    private Button incomingRetry;
    private final Handler handler=new Handler(Looper.getMainLooper());
    private TextView status;private LinearLayout entries;private String displayed="";
    private Button importButton,cancel;
    private final java.util.Set<String> expanded=new java.util.HashSet<>();
    private int renderVersion;
    private ProgressBar progress;
    private TextView progressLabel;
    private final java.util.List<View> packageActions=new java.util.ArrayList<>();
    private final Runnable refresh=new Runnable(){public void run(){
        if(!active) return;
        status.setText(BemInstaller.status);
        root.findViewById(R.id.bem_operation_state).setVisibility(BemInstaller.busy || BemInstaller.status.contains("未完成") || BemInstaller.status.contains("失败") ? View.VISIBLE : View.GONE);importButton.setEnabled(!BemInstaller.busy);cancel.setEnabled(BemInstaller.busy && !BemInstaller.removing);
        incomingRetry.setEnabled(pendingImport!=null && !BemInstaller.busy);
        progress.setVisibility(BemInstaller.busy?View.VISIBLE:View.GONE);
        progressLabel.setVisibility(BemInstaller.busy?View.VISIBLE:View.GONE);
        cancel.setVisibility(BemInstaller.busy?View.VISIBLE:View.GONE);
        if(BemInstaller.busy) {
            int percent=BemInstaller.progressPercent;
            progress.setIndeterminate(percent<0);
            if(percent>=0) progress.setProgress(percent);
            long seconds=(SystemClock.elapsedRealtime()-BemInstaller.startedAt)/1000;
            progressLabel.setText((percent<0?"正在处理":"当前 mip 编码："+percent+"%")+" · 已用 "+(seconds/60)+"分"+(seconds%60)+"秒");
        }
        String index=FrameworkSettings.open(activity).getString(BemInstaller.INDEX,"[]");
        if(!index.equals(displayed)) showEntries();
        for(View action:packageActions) action.setEnabled(!BemInstaller.busy);
        handler.postDelayed(this,500);
    }};
    BemInstallPage(Activity activity, View root, Bundle state) {
        this.activity=activity;
        this.root=root;
        status=root.findViewById(R.id.bem_status);
        progress=root.findViewById(R.id.bem_progress);
        progressLabel=root.findViewById(R.id.bem_progress_label);
        importButton=root.findViewById(R.id.bem_import);
        cancel=root.findViewById(R.id.bem_cancel);
        entries=root.findViewById(R.id.bem_entries);
        incoming=root.findViewById(R.id.bem_incoming);
        incomingNotice=root.findViewById(R.id.bem_incoming_notice);
        incomingRetry=root.findViewById(R.id.bem_incoming_retry);
        incomingRetry.setOnClickListener(v -> startPendingImport());
        root.findViewById(R.id.bem_incoming_dismiss).setOnClickListener(v -> {
            pendingImport=null;
            incoming.setVisibility(View.GONE);
        });
        importButton.setOnClickListener(v -> activity.startActivityForResult(new Intent(Intent.ACTION_OPEN_DOCUMENT).setType("*/*").addCategory(Intent.CATEGORY_OPENABLE),PICK));
        root.findViewById(R.id.bem_models_quark).setOnClickListener(v -> openModelSource("https://pan.quark.cn/s/97a9ca8f9bf2"));
        root.findViewById(R.id.bem_models_baidu).setOnClickListener(v -> openModelSource("https://pan.baidu.com/s/5ekaAiiLmZKXHZ7pHH0W-Vw"));
        cancel.setOnClickListener(v -> BemInstaller.cancel());
        if(state!=null) {
            java.util.ArrayList<String> restored=state.getStringArrayList("bem.expanded");
            if(restored!=null) expanded.addAll(restored);
            // A rotation/recreated task must not replay an already accepted Intent.
            // Only an explicitly pending (busy) request is restored for manual retry.
            String pending=state.getString(STATE_PENDING_URI);
            if(pending!=null) {
                try {pendingImport=BemImportRequest.requireContentUri(Uri.parse(pending));}
                catch(IllegalArgumentException invalid) {pendingImport=null;}
            }
            String notice=state.getString(STATE_INCOMING_NOTICE);
            if(notice!=null) showIncoming(notice);
        }
    }
    private void openModelSource(String url) {
        try {
            activity.startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse(url)).addCategory(Intent.CATEGORY_BROWSABLE));
        } catch(RuntimeException error) {
            String detail=error.getMessage();
            new android.app.AlertDialog.Builder(activity)
                    .setTitle("无法打开模型链接")
                    .setMessage(error.getClass().getSimpleName()+(detail==null || detail.isEmpty()?"":"："+detail))
                    .setPositiveButton("确定",null).show();
        }
    }
    void saveState(Bundle state) {
        state.putStringArrayList("bem.expanded",new java.util.ArrayList<>(expanded));
        if(pendingImport!=null) state.putString(STATE_PENDING_URI,pendingImport.toString());
        if(incoming.getVisibility()==View.VISIBLE)
            state.putString(STATE_INCOMING_NOTICE,incomingNotice.getText().toString());
    }
    void receiveImport(Intent intent) {
        try {
            Uri uri=BemImportRequest.fromIntent(intent);
            if(uri!=null) {
                pendingImport=uri;
                startPendingImport();
            }
        } catch(RuntimeException error) {
            // Do not overwrite a different operation's global progress/status.
            pendingImport=null;
            showIncoming(error.getMessage()==null?"无法打开此文件，请重新选择 BEM 包。":error.getMessage());
        }
    }
    private void startPendingImport() {
        if(pendingImport==null) return;
        try {
            if(BemInstaller.start(activity,pendingImport)) {
                pendingImport=null;
                incoming.setVisibility(View.GONE);
                // An Activity kept on the back stack retains the temporary URI grant
                // while the worker streams the file. Never finish() a relay Activity.
            } else {
                showIncoming("已有导入、转换或移除任务正在进行。此文件尚未导入；任务结束后可点下方按钮继续。再次打开文件会替换这条待处理请求。");
            }
        } catch(RuntimeException error) {
            showIncoming(error.getMessage()==null?"无法开始导入，请重新选择 BEM 包。":error.getMessage());
        }
    }
    private void showIncoming(String message) {
        incomingNotice.setText(message);
        incomingRetry.setVisibility(pendingImport==null?View.GONE:View.VISIBLE);
        incomingRetry.setEnabled(pendingImport!=null && !BemInstaller.busy);
        incoming.setVisibility(View.VISIBLE);
    }
    boolean onActivityResult(int request,int result,Intent data) {
        if(request!=PICK) return false;
        if(result==Activity.RESULT_OK && data!=null) {
            // Reuse the same validation for the in-app picker. Only URI-bearing
            // fields are forwarded, never arbitrary paths or filenames.
            Intent selected=new Intent(Intent.ACTION_VIEW).setData(data.getData());
            selected.setClipData(data.getClipData());
            receiveImport(selected);
        }
        return true;
    }
    void resume(){active=true;handler.removeCallbacks(refresh);handler.post(refresh);}
    void pause(){active=false;handler.removeCallbacks(refresh);}
    void close(){pause();++renderVersion;}
    private void showEntries() {
        final int version=++renderVersion;
        entries.removeAllViews();packageActions.clear();
        try {
            JSONArray list=BemInstaller.index(activity);
            displayed=list.toString();
            if(list.length()==0) {
                TextView empty=new TextView(activity);
                empty.setText("暂无模型包");
                empty.setTextColor(activity.getColor(R.color.text_secondary));empty.setTextSize(14);
                empty.setGravity(android.view.Gravity.CENTER);empty.setPadding(dp(18),dp(32),dp(18),dp(32));
                empty.setBackgroundResource(R.drawable.bg_card);
                LinearLayout.LayoutParams emptyLayout=new LinearLayout.LayoutParams(-1,-2);emptyLayout.topMargin=dp(12);
                entries.addView(empty,emptyLayout);
            }
            for(int i=0;i<list.length();++i) {
                JSONObject entry=list.getJSONObject(i);String generation=entry.getString("generation");
                LinearLayout card=new LinearLayout(activity);card.setOrientation(LinearLayout.VERTICAL);card.setPadding(dp(16),dp(16),dp(16),dp(16));
                card.setBackgroundResource(R.drawable.bg_card);
                LinearLayout.LayoutParams cardLayout=new LinearLayout.LayoutParams(-1,-2);cardLayout.topMargin=dp(12);entries.addView(card,cardLayout);
                LinearLayout heading=new LinearLayout(activity);heading.setGravity(android.view.Gravity.CENTER_VERTICAL);card.addView(heading);
                TextView name=new TextView(activity);name.setText(entry.getString("name"));name.setTextColor(activity.getColor(R.color.text_primary));
                name.setTextSize(17);name.setTypeface(null,android.graphics.Typeface.BOLD);
                heading.addView(name,new LinearLayout.LayoutParams(0,-2,1));
                Switch enabled=new Switch(activity);enabled.setChecked(entry.optBoolean("enabled",true));
                enabled.setContentDescription("启用「"+entry.getString("name")+"」");enabled.setMinHeight(dp(48));heading.addView(enabled);
                packageActions.add(enabled);enabled.setEnabled(!BemInstaller.busy);
                enabled.setOnCheckedChangeListener((button,checked)->{
                    if(version!=renderVersion) return;
                    try {saveChange(new JSONObject().put("generation",generation).put("enabled",checked));}
                    catch(Exception error) {saveError(error);}
                });
                String mode=entry.optString("texture_mode","converted");
                Button details=actionButton(expanded.contains(generation)?"收起详细选项 ▴":"详细组件 / 外观选项 ▾");
                card.addView(details,new LinearLayout.LayoutParams(-1,dp(44)));
                LinearLayout detailPanel=new LinearLayout(activity);detailPanel.setOrientation(LinearLayout.VERTICAL);
                detailPanel.setVisibility(expanded.contains(generation)?View.VISIBLE:View.GONE);card.addView(detailPanel);
                details.setOnClickListener(v->{
                    boolean opening=detailPanel.getVisibility()!=View.VISIBLE;
                    if(opening) expanded.add(generation);else expanded.remove(generation);
                    detailPanel.setVisibility(opening?View.VISIBLE:View.GONE);
                    details.setText(opening?"收起详细选项 ▴":"详细组件 / 外观选项 ▾");
                });
                if(entry.optInt("bem_minor",0)>=1) addOptionControls(detailPanel,entry,generation,version);
                else {
                    JSONArray apps=entry.getJSONArray("appearances");String[] choices=new String[apps.length()];int selected=0;
                    String active=entry.optString("selected_appearance",entry.getString("default_appearance"));
                    for(int j=0;j<choices.length;++j){choices[j]=apps.getString(j);if(active.equals(choices[j])) selected=j;}
                    TextView choiceLabel=fieldLabel("启动外观");detailPanel.addView(choiceLabel);
                    Spinner spinner=choiceSpinner(choices,selected);detailPanel.addView(spinner,new LinearLayout.LayoutParams(-1,dp(52)));
                    spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                        @Override public void onItemSelected(AdapterView<?> parent,View view,int position,long id) {
                            if(version!=renderVersion || active.equals(choices[position])) return;
                            try {saveChange(new JSONObject().put("generation",generation).put("appearance",choices[position]));}
                            catch(Exception error) {saveError(error);}
                        }
                        @Override public void onNothingSelected(AdapterView<?> parent) {}
                    });
                }
                LinearLayout actions=new LinearLayout(activity);actions.setOrientation(LinearLayout.HORIZONTAL);
                LinearLayout.LayoutParams actionLayout=new LinearLayout.LayoutParams(-1,-2);actionLayout.topMargin=dp(16);card.addView(actions,actionLayout);
                if("original".equals(mode)) {
                    Button convert=actionButton("转换纹理");actions.addView(convert,new LinearLayout.LayoutParams(0,dp(44),1));packageActions.add(convert);
                    convert.setOnClickListener(v -> BemInstaller.convert(activity,generation));
                }
                Button remove=actionButton("移除模型包");
                LinearLayout.LayoutParams removeLayout=new LinearLayout.LayoutParams(0,dp(44),1);
                if("original".equals(mode)) removeLayout.leftMargin=dp(8);
                actions.addView(remove,removeLayout);packageActions.add(remove);
                String packageName=entry.getString("name");
                remove.setOnClickListener(v -> new android.app.AlertDialog.Builder(activity)
                        .setTitle("移除「"+packageName+"」？")
                        .setMessage("将删除此模型包的安装文件和配置。下载目录中的原始 BEM 文件会保留，可重新导入。游戏内已加载的模型需重启游戏后恢复。")
                        .setNegativeButton("取消",null)
                        .setPositiveButton("移除",(dialog,which)->{
                            try {BemInstaller.remove(activity,generation);}
                            catch(Exception error) {saveError(error);}
                        }).show());
            }
        } catch(Exception error){
            entries.removeAllViews();packageActions.clear();
            String message="安装列表读取失败："+error.getMessage();
            if(!BemInstaller.busy) BemInstaller.status=message;
            status.setText(message);
        }
    }
    private void saveChange(JSONObject change) throws Exception {
        BemInstaller.saveAll(activity,new JSONArray().put(change));
        BemInstaller.status="设置已保存，重启游戏后生效。同一角色最多启用一个包。";
        status.setText(BemInstaller.status);
        showEntries();
    }
    private void saveError(Exception error) {
        String message="设置未保存："+error.getMessage();
        // Replace every control with the actual committed snapshot, including
        // switches of other packages that an enable operation would disable.
        showEntries();
        if(!BemInstaller.busy) BemInstaller.status=message;
        status.setText(message);
        Toast.makeText(activity,message,Toast.LENGTH_LONG).show();
    }
    private void addOptionControls(LinearLayout card,JSONObject entry,String generation,int version) throws Exception {
        String active=entry.optString("selected_options",entry.getString("default_options"));
        final java.util.LinkedHashMap<String,String> values=BemOptions.parse(entry,active);
        JSONArray groups=entry.getJSONArray("option_groups");
        java.util.LinkedHashMap<String,LinearLayout> rows=new java.util.LinkedHashMap<>();
        for(int i=0;i<groups.length();++i) {
            JSONObject group=groups.getJSONObject(i);String id=group.getString("id");
            JSONArray items=group.getJSONArray("choices");String[] labels=new String[items.length()],ids=new String[items.length()];int selected=0;
            for(int j=0;j<items.length();++j) {
                JSONObject item=items.getJSONObject(j);ids[j]=item.getString("id");labels[j]=item.getString("name");
                if(ids[j].equals(values.get(id))) selected=j;
            }
            LinearLayout row=new LinearLayout(activity);row.setOrientation(LinearLayout.VERTICAL);card.addView(row);rows.put(id,row);
            row.addView(fieldLabel(group.getString("name")));
            Spinner spinner=choiceSpinner(labels,selected);row.addView(spinner,new LinearLayout.LayoutParams(-1,dp(52)));
            spinner.setOnItemSelectedListener(new android.widget.AdapterView.OnItemSelectedListener() {
                @Override public void onItemSelected(android.widget.AdapterView<?> parent,View view,int position,long selectedId) {
                    if(version!=renderVersion || ids[position].equals(values.get(id))) return;
                    java.util.LinkedHashMap<String,String> next=new java.util.LinkedHashMap<>(values);
                    next.put(id,ids[position]);
                    try {
                        saveChange(new JSONObject().put("generation",generation).put("options",BemOptions.encode(next)));
                    } catch(Exception error) {saveError(error);}
                }
                @Override public void onNothingSelected(android.widget.AdapterView<?> parent) {}
            });
        }
        java.util.Map<String,String> effective=BemOptions.effective(entry,values);
        for(java.util.Map.Entry<String,LinearLayout> item:rows.entrySet())
            item.getValue().setVisibility(effective.containsKey(item.getKey())?View.VISIBLE:View.GONE);
    }
    private TextView fieldLabel(String label) {
        TextView view=new TextView(activity);view.setText(label);view.setTextColor(activity.getColor(R.color.text_secondary));
        view.setTextSize(12);view.setPadding(0,dp(18),0,dp(7));return view;
    }
    private Spinner choiceSpinner(String[] labels,int selected) {
        Spinner spinner=new Spinner(activity);ArrayAdapter<String> adapter=new ArrayAdapter<>(activity,R.layout.bem_spinner_item,labels);
        adapter.setDropDownViewResource(R.layout.bem_spinner_dropdown_item);spinner.setAdapter(adapter);
        spinner.setSelection(selected);spinner.setMinimumHeight(dp(52));spinner.setBackgroundResource(R.drawable.bg_input);
        spinner.setPopupBackgroundResource(R.color.surface_high);
        packageActions.add(spinner);spinner.setEnabled(!BemInstaller.busy);return spinner;
    }
    private Button actionButton(String text) {
        Button button=new Button(activity);button.setText(text);button.setTransformationMethod(null);
        button.setTextSize(13);button.setTextColor(activity.getColor(R.color.text_primary));
        button.setBackgroundResource(R.drawable.bg_ghost_button);button.setMinHeight(0);button.setMinWidth(0);
        button.setEnabled(!BemInstaller.busy);
        return button;
    }
    private int dp(int value) {return Math.round(value*activity.getResources().getDisplayMetrics().density);}
}
