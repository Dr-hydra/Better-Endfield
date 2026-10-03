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
    private static final String STATE_CHARACTER="bem.character_filter";
    private Uri pendingImport;
    private View incoming;
    private TextView incomingNotice;
    private Button incomingRetry;
    private final Handler handler=new Handler(Looper.getMainLooper());
    private TextView status;private LinearLayout entries;private String displayed="";
    private Button importButton,cancel;
    private Button disableAll;
    private Spinner characterFilter;
    private String selectedCharacter="";
    private boolean updatingCharacterFilter,hasEnabledPackages;
    private final java.util.List<String> characterIds=new java.util.ArrayList<>();
    private JSONObject characterNames=new JSONObject();
    private Switch skipValidation,hotSwitch,fastLoading,keepLocalCopies;
    private Button cleanUnused;
    private final java.util.Set<String> expanded=new java.util.HashSet<>();
    private final java.util.Map<String,java.util.LinkedHashMap<String,Integer>> parameterDrafts=new java.util.HashMap<>();
    private int renderVersion;
    private ProgressBar progress;
    private TextView progressLabel;
    private final java.util.List<View> packageActions=new java.util.ArrayList<>();
    private final Runnable refresh=new Runnable(){public void run(){
        if(!active) return;
        skipValidation.setEnabled(!BemInstaller.busy);
        skipValidation.setChecked(FrameworkSettings.open(activity).getBoolean(BemInstaller.SKIP_VALIDATION,false));
        refreshExperiment(hotSwitch,BemInstaller.HOT_SWITCH);
        refreshExperiment(fastLoading,BemInstaller.FAST_LOADING);
        keepLocalCopies.setEnabled(!BemInstaller.busy);cleanUnused.setEnabled(!BemInstaller.busy);
        status.setText(BemInstaller.status);
        root.findViewById(R.id.bem_operation_state).setVisibility(BemInstaller.busy || BemInstaller.status.contains("未完成") || BemInstaller.status.contains("失败") || BemInstaller.status.startsWith("已清理") || BemInstaller.status.contains("本地副本") ? View.VISIBLE : View.GONE);importButton.setEnabled(!BemInstaller.busy);cancel.setEnabled(BemInstaller.busy && !BemInstaller.removing);
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
        disableAll.setEnabled(!BemInstaller.busy && hasEnabledPackages);
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
        skipValidation=root.findViewById(R.id.bem_skip_validation);
        hotSwitch=root.findViewById(R.id.bem_hot_switch);
        fastLoading=root.findViewById(R.id.bem_fast_loading);
        bindExperiment(hotSwitch,BemInstaller.HOT_SWITCH);
        bindExperiment(fastLoading,BemInstaller.FAST_LOADING);
        keepLocalCopies=root.findViewById(R.id.bem_keep_local_copies);
        cleanUnused=root.findViewById(R.id.bem_clean_unused);
        keepLocalCopies.setChecked(FrameworkSettings.open(activity).getBoolean(BemInstaller.KEEP_LOCAL_COPIES,true));
        keepLocalCopies.setOnCheckedChangeListener((button,checked)->{
            android.content.SharedPreferences settings=FrameworkSettings.open(activity);
            if(settings.getBoolean(BemInstaller.KEEP_LOCAL_COPIES,true)==checked) return;
            if(BemInstaller.busy || !settings.edit().putBoolean(BemInstaller.KEEP_LOCAL_COPIES,checked).commit()) {
                keepLocalCopies.setChecked(!checked);
                if(!BemInstaller.busy) saveError(new IllegalStateException("存储选项保存失败"));
                return;
            }
            BemInstaller.applyLocalCopies(activity,checked);
            handler.removeCallbacks(refresh);handler.post(refresh);
        });
        cleanUnused.setOnClickListener(v->{
            if(BemInstaller.cleanUnused(activity)) {handler.removeCallbacks(refresh);handler.post(refresh);}
        });
        skipValidation.setChecked(FrameworkSettings.open(activity).getBoolean(BemInstaller.SKIP_VALIDATION,false));
        skipValidation.setOnCheckedChangeListener((button,checked)->{
            android.content.SharedPreferences settings=FrameworkSettings.open(activity);
            if(settings.getBoolean(BemInstaller.SKIP_VALIDATION,false)==checked) return;
            if(!settings.edit().putBoolean(BemInstaller.SKIP_VALIDATION,checked).commit()) {
                skipValidation.setChecked(!checked);
                saveError(new IllegalStateException("实验选项保存失败"));
            }
        });
        entries=root.findViewById(R.id.bem_entries);
        addModelManagement();
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
        root.findViewById(R.id.bem_models_katfile).setOnClickListener(v -> openModelSource("https://katfile.biz/users/hydra405/"));
        cancel.setOnClickListener(v -> BemInstaller.cancel());
        if(state!=null) {
            selectedCharacter=state.getString(STATE_CHARACTER,"");
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
    private void addModelManagement() {
        try(java.io.InputStream input=activity.getAssets().open("character-names.json");
                java.io.ByteArrayOutputStream output=new java.io.ByteArrayOutputStream()) {
            byte[] buffer=new byte[8192];int count;
            while((count=input.read(buffer))!=-1) output.write(buffer,0,count);
            characterNames=new JSONObject(new String(output.toByteArray(),java.nio.charset.StandardCharsets.UTF_8));
        } catch(Exception unavailable) { /* Unknown characters still use their stable IDs. */ }
        LinearLayout management=new LinearLayout(activity);management.setOrientation(LinearLayout.VERTICAL);
        management.setPadding(dp(16),dp(16),dp(16),dp(16));management.setBackgroundResource(R.drawable.bg_card);
        management.addView(fieldLabel(activity.getString(R.string.bem_filter_character)));
        characterFilter=new Spinner(activity);characterFilter.setMinimumHeight(dp(52));
        characterFilter.setBackgroundResource(R.drawable.bg_input);characterFilter.setPopupBackgroundResource(R.color.surface_high);
        characterFilter.setContentDescription(activity.getString(R.string.bem_filter_character));
        management.addView(characterFilter,new LinearLayout.LayoutParams(-1,dp(52)));
        characterFilter.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override public void onItemSelected(AdapterView<?> parent,View view,int position,long id) {
                if(updatingCharacterFilter || position<0 || position>=characterIds.size()) return;
                String selected=characterIds.get(position);
                if(selected.equals(selectedCharacter)) return;
                selectedCharacter=selected;applyCharacterFilter();
            }
            @Override public void onNothingSelected(AdapterView<?> parent) {}
        });
        disableAll=actionButton(activity.getString(R.string.bem_disable_all));disableAll.setEnabled(false);
        LinearLayout.LayoutParams buttonLayout=new LinearLayout.LayoutParams(-1,dp(44));buttonLayout.topMargin=dp(12);
        management.addView(disableAll,buttonLayout);
        disableAll.setOnClickListener(v->{
            try {
                BemInstaller.disableAll(activity);
                BemInstaller.status=activity.getString(R.string.bem_all_disabled);status.setText(BemInstaller.status);
                showEntries();
                Toast.makeText(activity,R.string.bem_all_disabled,Toast.LENGTH_SHORT).show();
            } catch(Exception error) {saveError(error);}
        });
        LinearLayout parent=(LinearLayout)entries.getParent();
        LinearLayout.LayoutParams layout=new LinearLayout.LayoutParams(-1,-2);layout.topMargin=dp(18);
        parent.addView(management,parent.indexOfChild(entries),layout);
    }
    private void updateCharacterFilter(JSONArray list) throws JSONException {
        java.util.Set<String> installed=new java.util.TreeSet<>();hasEnabledPackages=false;
        for(int i=0;i<list.length();++i) {
            JSONObject entry=list.getJSONObject(i);installed.add(entry.getString("character_id"));
            hasEnabledPackages |= entry.optBoolean("enabled",true);
        }
        if(!installed.contains(selectedCharacter)) selectedCharacter="";
        java.util.List<String> next=new java.util.ArrayList<>();next.add("");next.addAll(installed);
        updatingCharacterFilter=true;
        try {
            if(!next.equals(characterIds)) {
                characterIds.clear();characterIds.addAll(next);
                String[] labels=new String[characterIds.size()];labels[0]=activity.getString(R.string.bem_all_characters);
                for(int i=1;i<labels.length;++i) {
                    String id=characterIds.get(i);labels[i]=characterNames.optString(id,id);
                }
                ArrayAdapter<String> adapter=new ArrayAdapter<>(activity,R.layout.bem_spinner_item,labels);
                adapter.setDropDownViewResource(R.layout.bem_spinner_dropdown_item);characterFilter.setAdapter(adapter);
            }
            characterFilter.setSelection(characterIds.indexOf(selectedCharacter));
            characterFilter.setEnabled(!installed.isEmpty());
        } finally {updatingCharacterFilter=false;}
        disableAll.setEnabled(!BemInstaller.busy && hasEnabledPackages);
    }
    private void applyCharacterFilter() {
        for(int i=0;i<entries.getChildCount();++i) {
            View card=entries.getChildAt(i);
            card.setVisibility(selectedCharacter.isEmpty() || card.getTag()==null || selectedCharacter.equals(card.getTag()) ? View.VISIBLE : View.GONE);
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
        state.putString(STATE_CHARACTER,selectedCharacter);
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
            updateCharacterFilter(list);
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
                card.setTag(entry.getString("character_id"));
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
                String detailsLabel=entry.optInt("bem_minor",0)>=3?"组件 / 形态滑条选项 ▾":"详细组件 / 外观选项 ▾";
                Button details=actionButton(expanded.contains(generation)?"收起详细选项 ▴":detailsLabel);
                card.addView(details,new LinearLayout.LayoutParams(-1,dp(44)));
                LinearLayout detailPanel=new LinearLayout(activity);detailPanel.setOrientation(LinearLayout.VERTICAL);
                detailPanel.setVisibility(expanded.contains(generation)?View.VISIBLE:View.GONE);card.addView(detailPanel);
                details.setOnClickListener(v->{
                    boolean opening=detailPanel.getVisibility()!=View.VISIBLE;
                    if(opening) expanded.add(generation);else expanded.remove(generation);
                    detailPanel.setVisibility(opening?View.VISIBLE:View.GONE);
                    details.setText(opening?"收起详细选项 ▴":detailsLabel);
                });
                if(entry.optInt("bem_minor",0)>=1) {
                    addOptionControls(detailPanel,entry,generation,version);
                    if(entry.optInt("bem_minor",0)>=3) addParameterControls(detailPanel,entry,generation,version);
                }
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
            applyCharacterFilter();
        } catch(Exception error){
            entries.removeAllViews();packageActions.clear();
            hasEnabledPackages=false;disableAll.setEnabled(false);
            String message="安装列表读取失败："+error.getMessage();
            if(!BemInstaller.busy) BemInstaller.status=message;
            status.setText(message);
        }
    }
    private void saveChange(JSONObject change) throws Exception {
        BemInstaller.saveAll(activity,new JSONArray().put(change));
        BemInstaller.status=FrameworkSettings.open(activity).getBoolean(BemInstaller.HOT_SWITCH,false)
            ? "设置已保存；已启用实验热切换的游戏将在下次切换配队或重新打开详情时更新。首次开启需重启游戏。"
            : "设置已保存，重启游戏后生效。同一角色最多启用一个包。";
        status.setText(BemInstaller.status);
        showEntries();
    }
    private void refreshExperiment(Switch control,String key) {
        control.setEnabled(!BemInstaller.busy);
        control.setChecked(FrameworkSettings.open(activity).getBoolean(key,false));
    }
    private void bindExperiment(Switch control,String key) {
        refreshExperiment(control,key);
        control.setOnCheckedChangeListener((button,checked)->{
            android.content.SharedPreferences settings=FrameworkSettings.open(activity);
            if(settings.getBoolean(key,false)==checked) return;
            if(!settings.edit().putBoolean(key,checked).commit()) {
                control.setChecked(!checked);
                saveError(new IllegalStateException("实验选项保存失败"));
                return;
            }
            BemInstaller.status="实验设置已保存，重启游戏后生效。";
            status.setText(BemInstaller.status);
        });
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
    private void addParameterControls(LinearLayout card,JSONObject entry,String generation,int version) throws Exception {
        JSONArray parameters=BemParameters.groups(entry);if(parameters.length()==0) return;
        java.util.LinkedHashMap<String,Integer> saved=BemParameters.parse(entry,entry.optString("selected_parameters",entry.optString("default_parameters","")));
        java.util.LinkedHashMap<String,Integer> previous=parameterDrafts.get(generation);
        if(previous!=null) for(int i=0;i<parameters.length();++i) {
            JSONObject parameter=parameters.getJSONObject(i);String id=parameter.getString("id");
            if(previous.containsKey(id) && BemParameters.accepts(parameter,previous.get(id))) saved.put(id,previous.get(id));
        }
        final java.util.LinkedHashMap<String,Integer> values=saved;parameterDrafts.put(generation,values);
        java.util.Map<String,String> options=BemOptions.parse(entry,entry.optString("selected_options",entry.getString("default_options")));
        java.util.List<SeekBar> sliders=new java.util.ArrayList<>();
        for(int i=0;i<parameters.length();++i) {
            JSONObject parameter=parameters.getJSONObject(i);String id=parameter.getString("id");
            int min=BemParameters.tick(parameter,"min"),max=BemParameters.tick(parameter,"max"),step=BemParameters.tick(parameter,"step");
            LinearLayout row=new LinearLayout(activity);row.setOrientation(LinearLayout.VERTICAL);card.addView(row);
            TextView label=fieldLabel(parameter.getString("name"));row.addView(label);
            SeekBar slider=new SeekBar(activity);slider.setMax((max-min)/step);slider.setProgress((values.get(id)-min)/step);
            row.addView(slider,new LinearLayout.LayoutParams(-1,dp(48)));packageActions.add(slider);sliders.add(slider);
            slider.setEnabled(!BemInstaller.busy);
            Runnable updateLabel=()->{
                try {
                    label.setText(parameter.getString("name")+"："+String.format(java.util.Locale.ROOT,"%.3f",values.get(id)/1000.0)+
                        "（"+min/1000.0+"–"+max/1000.0+"，步长 "+step/1000.0+"，默认 "+BemParameters.tick(parameter,"default")/1000.0+
                        "，原形 "+BemParameters.tick(parameter,"neutral")/1000.0+"）");
                } catch(Exception error) {label.setText("滑条读取失败");}
            };
            updateLabel.run();
            slider.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
                @Override public void onProgressChanged(SeekBar view,int progress,boolean fromUser) {
                    if(version!=renderVersion) return;values.put(id,min+progress*step);updateLabel.run();
                }
                @Override public void onStartTrackingTouch(SeekBar view) {}
                @Override public void onStopTrackingTouch(SeekBar view) {}
            });
            row.setVisibility(BemParameters.available(entry,parameter,options)?View.VISIBLE:View.GONE);
        }
        TextView hint=fieldLabel("调整后点击应用。隐藏的滑条按原形生效，并保留已保存数值。开启实验热切换后，下次切换配队或打开详情时更新；否则重启游戏生效。");
        card.addView(hint);
        LinearLayout actions=new LinearLayout(activity);actions.setOrientation(LinearLayout.HORIZONTAL);card.addView(actions);
        Button apply=actionButton("应用滑条");actions.addView(apply,new LinearLayout.LayoutParams(0,dp(44),1));packageActions.add(apply);
        apply.setOnClickListener(v->{
            if(version!=renderVersion) return;
            try {saveChange(new JSONObject().put("generation",generation).put("parameters",BemParameters.encode(values)));}
            catch(Exception error) {parameterDrafts.remove(generation);saveError(error);}
        });
        Button reset=actionButton("作者默认值");actions.addView(reset,new LinearLayout.LayoutParams(0,dp(44),1));packageActions.add(reset);
        reset.setOnClickListener(v->{
            if(version!=renderVersion) return;
            try {for(int i=0;i<parameters.length();++i) {
                JSONObject parameter=parameters.getJSONObject(i);
                sliders.get(i).setProgress((BemParameters.tick(parameter,"default")-BemParameters.tick(parameter,"min"))/BemParameters.tick(parameter,"step"));
            }} catch(Exception error) {saveError(error);}
        });
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
