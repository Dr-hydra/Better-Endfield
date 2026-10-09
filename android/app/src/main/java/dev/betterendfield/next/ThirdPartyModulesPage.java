package dev.betterendfield.next;

import android.app.Activity;
import android.content.Intent;
import android.view.View;
import android.widget.*;
import org.json.*;
import java.util.concurrent.*;

final class ThirdPartyModulesPage {
    private static final int PICK=107;
    private final Activity activity;
    private final LinearLayout cards;
    private final TextView status;
    private final ExecutorService worker=Executors.newSingleThreadExecutor();
    private boolean busy,closed;
    private interface Operation {String run() throws Exception;}
    ThirdPartyModulesPage(Activity activity,LinearLayout root) {
        this.activity=activity;root.setOrientation(LinearLayout.VERTICAL);root.setPadding(dp(16),dp(16),dp(16),dp(16));
        TextView title=text("第三方模块",22);root.addView(title);
        root.addView(text("导入作者提供的 ZIP，自由网页界面独立打开。原生模块会运行作者代码，请选择信任来源。配置和已加载模块启停可运行中更新；二进制更新、移除和排序需重启游戏完成。旧版本文件保留，避免打断运行中的模块。",14));
        Button add=button("导入模块 ZIP");root.addView(add);add.setOnClickListener(v->{if(!busy)activity.startActivityForResult(new Intent(Intent.ACTION_OPEN_DOCUMENT).setType("application/zip").addCategory(Intent.CATEGORY_OPENABLE),PICK);});
        Button refresh=button("刷新");root.addView(refresh);refresh.setOnClickListener(v->render());
        status=text("",14);root.addView(status);cards=new LinearLayout(activity);cards.setOrientation(LinearLayout.VERTICAL);root.addView(cards);
    }
    private TextView text(String value,int size){TextView view=new TextView(activity);view.setText(value);view.setTextSize(size);view.setTextColor(activity.getColor(R.color.text_primary));view.setPadding(0,dp(6),0,dp(6));return view;}
    private Button button(String value){Button button=new Button(activity);button.setText(value);button.setTransformationMethod(null);button.setEnabled(!busy);return button;}
    private int dp(int n){return Math.round(n*activity.getResources().getDisplayMetrics().density);}
    void render() {
        if(closed)return;cards.removeAllViews();
        try {
            JSONArray records=ThirdPartyModuleStore.index(activity).getJSONArray("modules");if(records.length()==0)cards.addView(text("尚未导入第三方模块",15));
            for(int i=0;i<records.length();++i) {
                JSONObject record=records.getJSONObject(i);String id=record.getString("id"),fileError="";JSONObject manifest;
                try {manifest=ThirdPartyModuleStore.manifest(record);}catch(Exception error) {
                    fileError=String.valueOf(error.getMessage());manifest=new JSONObject().put("name",id).put("author","").put("version","").put("libraries",new JSONObject());
                }
                LinearLayout card=new LinearLayout(activity);card.setOrientation(LinearLayout.VERTICAL);card.setPadding(dp(14),dp(12),dp(14),dp(12));card.setBackgroundResource(R.drawable.bg_card);
                LinearLayout.LayoutParams margin=new LinearLayout.LayoutParams(-1,-2);margin.topMargin=dp(12);cards.addView(card,margin);
                card.addView(text(manifest.getString("name"),19));card.addView(text(manifest.getString("author")+" · "+manifest.getString("version")+" · "+id,13));
                boolean supported=fileError.isEmpty()&&ThirdPartyModulePackage.supported(manifest);
                card.addView(text(!fileError.isEmpty()?"模块文件读取失败："+fileError:supported?"支持 Android 原生模块或网页":"仅支持其他平台，当前不可启用",13));
                Switch enabled=new Switch(activity);enabled.setText("启用模块");enabled.setChecked(record.getBoolean("enabled"));enabled.setEnabled(supported&&!busy);card.addView(enabled);
                enabled.setOnCheckedChangeListener((view,value)->run(()->{ThirdPartyModuleStore.enabled(activity,id,value);return "启用状态已保存；已连接游戏会更新状态，原生库保留到游戏退出";}));
                Button web=button("打开模块网页");web.setEnabled(!busy&&!manifest.optString("ui","").isEmpty());card.addView(web);
                web.setOnClickListener(v->activity.startActivity(new Intent(activity,ThirdPartyModuleActivity.class).putExtra("module_id",id)));
                Button log=button("状态 / 日志");card.addView(log);log.setOnClickListener(v->run(()->{
                    try{return ThirdPartyModuleStore.runtime(activity,"status",id,null,null).toString(2);}catch(java.io.IOException offline){return "游戏模块未连接；启用后重启游戏。已保存配置保留。";}
                }));
                LinearLayout order=new LinearLayout(activity);order.setOrientation(LinearLayout.HORIZONTAL);card.addView(order);
                Button up=button("上移"),down=button("下移"),remove=button("移除");order.addView(up,new LinearLayout.LayoutParams(0,-2,1));order.addView(down,new LinearLayout.LayoutParams(0,-2,1));order.addView(remove,new LinearLayout.LayoutParams(0,-2,1));
                up.setOnClickListener(v->run(()->{ThirdPartyModuleStore.move(activity,id,-1);return "顺序已保存，重启游戏生效";}));
                down.setOnClickListener(v->run(()->{ThirdPartyModuleStore.move(activity,id,1);return "顺序已保存，重启游戏生效";}));
                remove.setOnClickListener(v->run(()->{ThirdPartyModuleStore.remove(activity,id);return "已从加载列表移除，重启游戏生效；运行中仍使用的旧文件保留";}));
            }
        } catch(Exception error){status.setText("模块列表读取失败："+error.getMessage());}
    }
    private void run(Operation operation) {
        if(busy||closed)return;busy=true;status.setText("正在处理…");render();
        worker.execute(()->{String result;try{result=operation.run();}catch(Exception error){result="操作未完成："+error.getMessage();}String message=result;
            activity.runOnUiThread(()->{busy=false;if(!closed){status.setText(message);render();}});
        });
    }
    void onActivityResult(int request,int result,Intent data) {
        if(request==PICK && result==Activity.RESULT_OK && data!=null && data.getData()!=null)run(()->{
            ThirdPartyModuleStore.importArchive(activity.getApplicationContext(),data.getData());return "模块导入完成。新包默认停用；更新保留配置，重启游戏生效。";
        });
    }
    void close(){closed=true;worker.shutdown();}
}
