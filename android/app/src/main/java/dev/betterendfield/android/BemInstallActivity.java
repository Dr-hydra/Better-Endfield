package dev.betterendfield.android;

import android.app.Activity;
import android.content.Intent;
import android.os.*;
import android.widget.*;
import org.json.*;

/** Small import screen independent of the login-model controls. */
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
        progress.setVisibility(BemInstaller.busy?android.view.View.VISIBLE:android.view.View.GONE);
        progressLabel.setVisibility(BemInstaller.busy?android.view.View.VISIBLE:android.view.View.GONE);
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
        LinearLayout root=new LinearLayout(this);root.setOrientation(LinearLayout.VERTICAL);root.setPadding(dp(18),dp(18),dp(18),dp(18));
        ScrollView scroll=new ScrollView(this);scroll.addView(root);setContentView(scroll);
        scroll.setOnApplyWindowInsetsListener((view,insets)->{
            view.setPadding(insets.getSystemWindowInsetLeft(),insets.getSystemWindowInsetTop(),insets.getSystemWindowInsetRight(),insets.getSystemWindowInsetBottom());
            return insets;
        });
        TextView title=new TextView(this);title.setText("第三方模型 · 包管理");title.setTextSize(22);root.addView(title);
        saveAll=new Button(this);saveAll.setText("保存全部启用状态与外观");root.addView(saveAll);
        saveAll.setOnClickListener(v -> {
            try {
                JSONArray changes=new JSONArray();
                for(java.util.function.Supplier<JSONObject> selection:selections.values()) changes.put(selection.get());
                BemInstaller.saveAll(this,changes);BemInstaller.status="全部设置已保存，重启游戏后生效。";
            } catch(Exception error) {BemInstaller.status="保存失败："+error.getMessage();}
        });
        TextView hint=new TextView(this);hint.setText("导入时保留原始纹理。若游戏中贴图显示异常，再对对应模型包手动转换手机纹理。修改后重启游戏生效。");hint.setPadding(0,dp(12),0,dp(12));root.addView(hint);
        status=new TextView(this);root.addView(status);
        progress=new ProgressBar(this,null,android.R.attr.progressBarStyleHorizontal);progress.setMax(100);root.addView(progress,new LinearLayout.LayoutParams(-1,dp(12)));
        progressLabel=new TextView(this);root.addView(progressLabel);
        importButton=new Button(this);importButton.setText("选择并导入 BEM 包");root.addView(importButton);
        importButton.setOnClickListener(v -> startActivityForResult(new Intent(Intent.ACTION_OPEN_DOCUMENT).setType("*/*").addCategory(Intent.CATEGORY_OPENABLE),PICK));
        cancel=new Button(this);cancel.setText("取消当前操作");root.addView(cancel);cancel.setOnClickListener(v -> BemInstaller.cancel());
        entries=new LinearLayout(this);entries.setOrientation(LinearLayout.VERTICAL);root.addView(entries);
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
            if(list.length()==0) {TextView empty=new TextView(this);empty.setText("尚未导入模型包");entries.addView(empty);}
            for(int i=0;i<list.length();++i) {
                JSONObject entry=list.getJSONObject(i);String generation=entry.getString("generation");
                JSONObject draft=drafts.get(generation);
                if(draft!=null) entry.put("enabled",draft.getBoolean("enabled")).put("selected_appearance",draft.getString("appearance"));
                LinearLayout card=new LinearLayout(this);card.setOrientation(LinearLayout.VERTICAL);card.setPadding(dp(16),dp(12),dp(16),dp(12));
                card.setBackgroundResource(R.drawable.bg_card);
                LinearLayout.LayoutParams cardLayout=new LinearLayout.LayoutParams(-1,-2);cardLayout.topMargin=dp(14);entries.addView(card,cardLayout);
                Switch enabled=new Switch(this);enabled.setText(entry.getString("name"));enabled.setChecked(entry.optBoolean("enabled",true));enabled.setMinHeight(dp(48));card.addView(enabled);
                String mode=entry.optString("texture_mode","converted");
                TextView textureState=new TextView(this);textureState.setText("original".equals(mode)?"纹理：原始包（未转换）":"纹理：已转换手机格式" );card.addView(textureState);
                JSONArray apps=entry.getJSONArray("appearances");String[] choices=new String[apps.length()];int selected=0;
                String active=entry.optString("selected_appearance",entry.getString("default_appearance"));
                for(int j=0;j<choices.length;++j){choices[j]=apps.getString(j);if(active.equals(choices[j])) selected=j;}
                Spinner spinner=new Spinner(this);spinner.setAdapter(new ArrayAdapter<>(this,android.R.layout.simple_spinner_dropdown_item,choices));spinner.setSelection(selected);spinner.setMinimumHeight(dp(48));card.addView(spinner);
                selections.put(generation,()->{
                    try {return new JSONObject().put("generation",generation).put("appearance",choices[spinner.getSelectedItemPosition()]).put("enabled",enabled.isChecked());}
                    catch(JSONException error) {throw new IllegalStateException(error);}
                });
                if("original".equals(mode)) {
                    Button convert=new Button(this);convert.setText("转换手机纹理");card.addView(convert);packageActions.add(convert);
                    convert.setOnClickListener(v -> BemInstaller.convert(this,generation));
                }
                Button remove=new Button(this);remove.setText("移除模型包");card.addView(remove);packageActions.add(remove);
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
    private int dp(int value) {return Math.round(value*getResources().getDisplayMetrics().density);}
}
