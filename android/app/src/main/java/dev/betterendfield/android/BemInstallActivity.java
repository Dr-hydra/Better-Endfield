package dev.betterendfield.android;

import android.app.Activity;
import android.content.Intent;
import android.os.*;
import android.view.View;
import android.widget.*;
import org.json.*;

/** Import and startup-selection screen for installed BEM packages. */
public final class BemInstallActivity extends Activity {
    private static final int PICK=101;
    private final Handler handler=new Handler(Looper.getMainLooper());
    private TextView status;private LinearLayout entries;private String displayed="";
    private Button importButton,cancel;
    private Button saveAll;
    private final java.util.Map<String,java.util.function.Supplier<JSONObject>> selections=new java.util.LinkedHashMap<>();
    private ProgressBar progress;
    private TextView progressLabel;
    private final java.util.List<Button> packageActions=new java.util.ArrayList<>();
    private final Runnable refresh=new Runnable(){public void run(){
        status.setText(BemInstaller.status);importButton.setEnabled(!BemInstaller.busy);cancel.setEnabled(BemInstaller.busy && !BemInstaller.removing);
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
        String index=FrameworkSettings.open(BemInstallActivity.this).getString(BemInstaller.INDEX,"[]");
        if(!index.equals(displayed)){displayed=index;showEntries();}
        for(Button action:packageActions) action.setEnabled(!BemInstaller.busy);
        saveAll.setEnabled(!BemInstaller.busy && !selections.isEmpty());
        handler.postDelayed(this,500);
    }};
    @Override public void onCreate(Bundle state) {
        super.onCreate(state);
        setContentView(R.layout.activity_bem_install);
        ScrollView scroll=findViewById(R.id.bem_scroll);
        scroll.setOnApplyWindowInsetsListener((view,insets)->{
            view.setPadding(insets.getSystemWindowInsetLeft(),insets.getSystemWindowInsetTop(),insets.getSystemWindowInsetRight(),insets.getSystemWindowInsetBottom());
            return insets;
        });
        status=findViewById(R.id.bem_status);
        progress=findViewById(R.id.bem_progress);
        progressLabel=findViewById(R.id.bem_progress_label);
        importButton=findViewById(R.id.bem_import);
        cancel=findViewById(R.id.bem_cancel);
        saveAll=findViewById(R.id.bem_save);
        entries=findViewById(R.id.bem_entries);
        saveAll.setOnClickListener(v -> {
            try {
                JSONArray changes=new JSONArray();
                for(java.util.function.Supplier<JSONObject> selection:selections.values()) changes.put(selection.get());
                BemInstaller.saveAll(this,changes);BemInstaller.status="全部设置已保存，重启游戏后生效。";
            } catch(Exception error) {BemInstaller.status="保存失败："+error.getMessage();}
        });
        importButton.setOnClickListener(v -> startActivityForResult(new Intent(Intent.ACTION_OPEN_DOCUMENT).setType("*/*").addCategory(Intent.CATEGORY_OPENABLE),PICK));
        cancel.setOnClickListener(v -> BemInstaller.cancel());
        // Explicit URI grant is still required; no arbitrary filesystem path extra.
        if(Intent.ACTION_VIEW.equals(getIntent().getAction()) && getIntent().getData()!=null && "content".equals(getIntent().getData().getScheme()))
            BemInstaller.start(this,getIntent().getData());
    }
    @Override protected void onActivityResult(int request,int result,Intent data) {
        super.onActivityResult(request,result,data);
        if(request==PICK&&result==RESULT_OK&&data!=null&&data.getData()!=null) BemInstaller.start(this,data.getData());
    }
    @Override protected void onResume(){super.onResume();handler.post(refresh);}
    @Override protected void onPause(){handler.removeCallbacks(refresh);super.onPause();}
    private void showEntries() {
        java.util.Map<String,JSONObject> drafts=new java.util.HashMap<>();
        for(java.util.Map.Entry<String,java.util.function.Supplier<JSONObject>> item:selections.entrySet()) drafts.put(item.getKey(),item.getValue().get());
        entries.removeAllViews();packageActions.clear();selections.clear();
        try {
            JSONArray list=BemInstaller.index(this);
            if(list.length()==0) {
                TextView empty=new TextView(this);
                empty.setText("还没有模型包\n点击上方「导入 BEM 包」添加模型。");
                empty.setTextColor(getColor(R.color.text_secondary));empty.setTextSize(14);
                empty.setGravity(android.view.Gravity.CENTER);empty.setPadding(dp(18),dp(32),dp(18),dp(32));
                empty.setBackgroundResource(R.drawable.bg_card);
                LinearLayout.LayoutParams emptyLayout=new LinearLayout.LayoutParams(-1,-2);emptyLayout.topMargin=dp(12);
                entries.addView(empty,emptyLayout);
            }
            for(int i=0;i<list.length();++i) {
                JSONObject entry=list.getJSONObject(i);String generation=entry.getString("generation");
                JSONObject draft=drafts.get(generation);
                if(draft!=null) {
                    entry.put("enabled",draft.getBoolean("enabled"));
                    if(entry.optInt("bem_minor",0)>=1) entry.put("selected_options",draft.getString("options"));
                    else entry.put("selected_appearance",draft.getString("appearance"));
                }
                LinearLayout card=new LinearLayout(this);card.setOrientation(LinearLayout.VERTICAL);card.setPadding(dp(16),dp(16),dp(16),dp(16));
                card.setBackgroundResource(R.drawable.bg_card);
                LinearLayout.LayoutParams cardLayout=new LinearLayout.LayoutParams(-1,-2);cardLayout.topMargin=dp(12);entries.addView(card,cardLayout);
                LinearLayout heading=new LinearLayout(this);heading.setGravity(android.view.Gravity.CENTER_VERTICAL);card.addView(heading);
                TextView name=new TextView(this);name.setText(entry.getString("name"));name.setTextColor(getColor(R.color.text_primary));
                name.setTextSize(17);name.setTypeface(null,android.graphics.Typeface.BOLD);
                heading.addView(name,new LinearLayout.LayoutParams(0,-2,1));
                Switch enabled=new Switch(this);enabled.setChecked(entry.optBoolean("enabled",true));
                enabled.setContentDescription("启用「"+entry.getString("name")+"」");enabled.setMinHeight(dp(48));heading.addView(enabled);
                String mode=entry.optString("texture_mode","converted");
                TextView textureState=new TextView(this);textureState.setText("original".equals(mode)?"原始纹理 · 可按需转换":"手机纹理已就绪");
                textureState.setTextColor(getColor(R.color.text_secondary));textureState.setTextSize(12);card.addView(textureState);
                if(entry.optInt("bem_minor",0)>=1) selections.put(generation,addOptionControls(card,entry,generation,enabled));
                else {
                    JSONArray apps=entry.getJSONArray("appearances");String[] choices=new String[apps.length()];int selected=0;
                    String active=entry.optString("selected_appearance",entry.getString("default_appearance"));
                    for(int j=0;j<choices.length;++j){choices[j]=apps.getString(j);if(active.equals(choices[j])) selected=j;}
                    TextView choiceLabel=fieldLabel("启动外观");card.addView(choiceLabel);
                    Spinner spinner=choiceSpinner(choices,selected);card.addView(spinner,new LinearLayout.LayoutParams(-1,dp(52)));
                    selections.put(generation,()->{
                        try {return new JSONObject().put("generation",generation).put("appearance",choices[spinner.getSelectedItemPosition()]).put("enabled",enabled.isChecked());}
                        catch(JSONException error) {throw new IllegalStateException(error);}
                    });
                }
                LinearLayout actions=new LinearLayout(this);actions.setOrientation(LinearLayout.HORIZONTAL);
                LinearLayout.LayoutParams actionLayout=new LinearLayout.LayoutParams(-1,-2);actionLayout.topMargin=dp(16);card.addView(actions,actionLayout);
                if("original".equals(mode)) {
                    Button convert=actionButton("转换纹理");actions.addView(convert,new LinearLayout.LayoutParams(0,dp(44),1));packageActions.add(convert);
                    convert.setOnClickListener(v -> BemInstaller.convert(this,generation));
                }
                Button remove=actionButton("移除模型包");
                LinearLayout.LayoutParams removeLayout=new LinearLayout.LayoutParams(0,dp(44),1);
                if("original".equals(mode)) removeLayout.leftMargin=dp(8);
                actions.addView(remove,removeLayout);packageActions.add(remove);
                String packageName=entry.getString("name");
                remove.setOnClickListener(v -> new android.app.AlertDialog.Builder(this)
                        .setTitle("移除「"+packageName+"」？")
                        .setMessage("将删除此模型包的安装文件和配置。下载目录中的原始 BEM 文件会保留，可重新导入。游戏内已加载的模型需重启游戏后恢复。")
                        .setNegativeButton("取消",null)
                        .setPositiveButton("移除",(dialog,which)->{
                            try {BemInstaller.remove(this,generation);}
                            catch(Exception error) {BemInstaller.status=error.getMessage();}
                        }).show());
            }
        } catch(Exception error){status.setText("安装列表读取失败："+error.getMessage());}
    }
    private java.util.function.Supplier<JSONObject> addOptionControls(LinearLayout card,JSONObject entry,String generation,Switch enabled) throws Exception {
        String active=entry.optString("selected_options",entry.getString("default_options"));
        java.util.LinkedHashMap<String,String> saved;
        try {saved=BemOptions.parse(entry,active);}
        catch(Exception stale) {saved=BemOptions.parse(entry,entry.getString("default_options"));}
        final java.util.LinkedHashMap<String,String> values=saved;
        JSONArray groups=entry.getJSONArray("option_groups");
        java.util.LinkedHashMap<String,LinearLayout> rows=new java.util.LinkedHashMap<>();
        for(int i=0;i<groups.length();++i) {
            JSONObject group=groups.getJSONObject(i);String id=group.getString("id");
            JSONArray items=group.getJSONArray("choices");String[] labels=new String[items.length()],ids=new String[items.length()];int selected=0;
            for(int j=0;j<items.length();++j) {
                JSONObject item=items.getJSONObject(j);ids[j]=item.getString("id");labels[j]=item.getString("name");
                if(ids[j].equals(values.get(id))) selected=j;
            }
            LinearLayout row=new LinearLayout(this);row.setOrientation(LinearLayout.VERTICAL);card.addView(row);rows.put(id,row);
            row.addView(fieldLabel(group.getString("name")));
            Spinner spinner=choiceSpinner(labels,selected);row.addView(spinner,new LinearLayout.LayoutParams(-1,dp(52)));
            spinner.setOnItemSelectedListener(new android.widget.AdapterView.OnItemSelectedListener() {
                @Override public void onItemSelected(android.widget.AdapterView<?> parent,View view,int position,long selectedId) {
                    String previous=values.put(id,ids[position]);
                    if(ids[position].equals(previous)) return;
                    try {
                        if(!BemOptions.valid(entry,values)) {
                            values.put(id,previous);status.setText("这个选项组合在包内不可达，请选择其他组合。");
                            for(int j=0;j<ids.length;++j) if(ids[j].equals(previous)) spinner.setSelection(j);
                            return;
                        }
                        java.util.Map<String,String> effective=BemOptions.effective(entry,values);
                        for(java.util.Map.Entry<String,LinearLayout> item:rows.entrySet())
                            item.getValue().setVisibility(effective.containsKey(item.getKey())?View.VISIBLE:View.GONE);
                    } catch(Exception error) {values.put(id,previous);status.setText("选项错误："+error.getMessage());}
                }
                @Override public void onNothingSelected(android.widget.AdapterView<?> parent) {}
            });
        }
        java.util.Map<String,String> effective=BemOptions.effective(entry,values);
        for(java.util.Map.Entry<String,LinearLayout> item:rows.entrySet())
            item.getValue().setVisibility(effective.containsKey(item.getKey())?View.VISIBLE:View.GONE);
        return ()->{
            try {return new JSONObject().put("generation",generation).put("options",BemOptions.encode(values)).put("enabled",enabled.isChecked());}
            catch(JSONException error) {throw new IllegalStateException(error);}
        };
    }
    private TextView fieldLabel(String label) {
        TextView view=new TextView(this);view.setText(label);view.setTextColor(getColor(R.color.text_secondary));
        view.setTextSize(12);view.setPadding(0,dp(18),0,dp(7));return view;
    }
    private Spinner choiceSpinner(String[] labels,int selected) {
        Spinner spinner=new Spinner(this);ArrayAdapter<String> adapter=new ArrayAdapter<>(this,R.layout.bem_spinner_item,labels);
        adapter.setDropDownViewResource(R.layout.bem_spinner_dropdown_item);spinner.setAdapter(adapter);
        spinner.setSelection(selected);spinner.setMinimumHeight(dp(52));spinner.setBackgroundResource(R.drawable.bg_input);
        spinner.setPopupBackgroundResource(R.color.surface_high);return spinner;
    }
    private Button actionButton(String text) {
        Button button=new Button(this);button.setText(text);button.setTransformationMethod(null);
        button.setTextSize(13);button.setTextColor(getColor(R.color.text_primary));
        button.setBackgroundResource(R.drawable.bg_ghost_button);button.setMinHeight(0);button.setMinWidth(0);
        return button;
    }
    private int dp(int value) {return Math.round(value*getResources().getDisplayMetrics().density);}
}
