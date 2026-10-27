package dev.betterendfield.next;

import android.app.*;
import android.content.*;
import android.os.*;
import android.text.TextUtils;
import android.view.*;
import android.widget.*;
import org.json.*;
import java.io.*;
import java.util.*;
import java.util.concurrent.*;

/** Native editor; the preview Activity is created exclusively by an explicit action. */
public final class BemComposeActivity extends Activity {
    private final ExecutorService worker=Executors.newSingleThreadExecutor();
    private final Set<String> sources=new LinkedHashSet<>(),expanded=new HashSet<>(),selected=new HashSet<>();
    private BemComposeSession session;
    private BemComposeState state;
    private LinearLayout content;
    private ScrollView scroll;
    private TextView status;
    private android.widget.ProgressBar progress;
    private Button cancel;
    private String installerStatus="";
    private final Handler handler=new Handler(Looper.getMainLooper());
    private final Runnable refresh=new Runnable(){public void run(){
        if(closed)return;
        if(!installerStatus.equals(BemInstaller.status)){installerStatus=BemInstaller.status;status.setText(installerStatus);}
        progress.setVisibility(BemInstaller.busy?View.VISIBLE:View.GONE);cancel.setVisibility(BemInstaller.busy?View.VISIBLE:View.GONE);
        handler.postDelayed(this,500);
    }};
    private boolean busy,closed;
    static void open(Activity activity){activity.startActivity(new Intent(activity,BemComposeActivity.class));}
    @Override public void onCreate(Bundle saved) {
        super.onCreate(saved);setTitle(Text.of("外观组合"));
        installerStatus=BemInstaller.status;
        LinearLayout root=new LinearLayout(this);root.setOrientation(LinearLayout.VERTICAL);
        root.setPadding(dp(12),dp(12),dp(12),dp(12));setContentView(root);
        status=new TextView(this);status.setPadding(0,dp(8),0,dp(8));root.addView(status);
        progress=new android.widget.ProgressBar(this,null,android.R.attr.progressBarStyleHorizontal);progress.setIndeterminate(true);progress.setVisibility(View.GONE);root.addView(progress);
        cancel=new Button(this);cancel.setText(Text.of("取消"));cancel.setOnClickListener(v->BemInstaller.cancel());cancel.setVisibility(View.GONE);root.addView(cancel);
        scroll=new ScrollView(this);content=new LinearLayout(this);content.setOrientation(LinearLayout.VERTICAL);scroll.addView(content);root.addView(scroll,new LinearLayout.LayoutParams(-1,0,1));
        try {
            session=saved!=null && saved.containsKey("session")?BemComposeSession.open(this,saved.getString("session")):BemComposeSession.create(this);
            if(saved!=null){Collections.addAll(sources,saved.getStringArray("sources")==null?new String[0]:saved.getStringArray("sources"));Collections.addAll(expanded,saved.getStringArray("expanded")==null?new String[0]:saved.getStringArray("expanded"));}
            if(session.file("plan.json").isFile())state=new BemComposeState(session.read("plan.json"),session.read("choices.json"));
            render();
        }catch(Exception error){error(error);}
    }
    @Override protected void onResume(){super.onResume();handler.removeCallbacks(refresh);handler.post(refresh);if(session!=null && state!=null)try{state=new BemComposeState(session.read("plan.json"),session.read("choices.json"));render();}catch(Exception error){error(error);}}
    @Override protected void onPause(){handler.removeCallbacks(refresh);super.onPause();}
    @Override protected void onSaveInstanceState(Bundle out){super.onSaveInstanceState(out);if(session!=null)out.putString("session",session.id());out.putStringArray("sources",sources.toArray(new String[0]));out.putStringArray("expanded",expanded.toArray(new String[0]));}
    private int dp(int value){return Math.round(value*getResources().getDisplayMetrics().density);}
    private TextView label(String text){TextView view=new TextView(this);view.setText(text);view.setTextSize(14);view.setPadding(0,dp(6),0,dp(6));view.setMaxLines(2);view.setEllipsize(TextUtils.TruncateAt.END);view.setOnLongClickListener(v->{new AlertDialog.Builder(this).setMessage(text).setPositiveButton(android.R.string.ok,null).show();return true;});return view;}
    private Button button(String text,Runnable action){Button view=new Button(this);view.setText(Text.of(text));view.setAllCaps(false);view.setMinHeight(dp(44));view.setOnClickListener(v->{if(!busy)try{action.run();}catch(Exception error){error(error);}});return view;}
    private LinearLayout row(LinearLayout parent){LinearLayout row=new LinearLayout(this);row.setOrientation(LinearLayout.HORIZONTAL);parent.addView(row,new LinearLayout.LayoutParams(-1,-2));return row;}
    private void action(LinearLayout row,String title,Runnable task){row.addView(button(title,task),new LinearLayout.LayoutParams(0,-2,1));}
    private void error(Throwable error){status.setText(Text.of("操作失败：")+(error.getMessage()==null?error.getClass().getSimpleName():error.getMessage()));}
    private void persist(){try{session.write("choices.json",state.choices);}catch(Exception error){error(error);}render();}
    private void render() {
        int y=scroll.getScrollY();content.removeAllViews();
        LinearLayout top=row(content);action(top,"选择来源",this::chooseSources);
        if(state==null){status.setText(Text.of("选择至少两个已安装的包"));return;}
        action(top,"重新分析",()->analyze(null));action(top,"预览",()->preview(null));
        TextView version=label("BEM 1."+state.plan.optInt("bem_minor")+" · "+state.plan.optString("package_version"));content.addView(version);
        for(JSONObject group:state.groups())renderGroup(group);
        renderSourceOptions();
        content.addView(label(Text.of("重叠部分会同时绘制；不会自动裁剪或消除穿模。")));
        content.addView(button("生成并安装组合",()->BemInstaller.installComposition(this,session)));
        scroll.post(()->scroll.scrollTo(0,y));
    }
    private void chooseSources() {
        if(BemInstaller.busy){status.setText(Text.of("正在处理"));return;}
        List<JSONObject> entries=BemComposeState.objects(BemInstaller.index(this));
        String[] names=new String[entries.size()];boolean[] checked=new boolean[names.length];
        for(int i=0;i<names.length;i++){JSONObject entry=entries.get(i);names[i]=entry.optString("name");checked[i]=sources.contains(entry.optString("generation"));}
        new AlertDialog.Builder(this).setTitle(Text.of("选择来源")).setMultiChoiceItems(names,checked,(d,which,value)->checked[which]=value)
            .setNegativeButton(android.R.string.cancel,null).setPositiveButton(Text.of("分析组合"),(d,w)->{
                sources.clear();for(int i=0;i<checked.length;i++)if(checked[i])sources.add(entries.get(i).optString("generation"));
                if(sources.size()<2 || sources.size()>16){status.setText(Text.of("请选择 2 至 16 个来源包"));return;}
                EditText name=new EditText(this);name.setSingleLine(true);name.setText(Text.of("我的外观组合"));
                new AlertDialog.Builder(this).setTitle(Text.of("组合名称")).setView(name).setNegativeButton(android.R.string.cancel,null).setPositiveButton(Text.of("分析组合"),(dialog,which)->analyze(name.getText().toString().trim())).show();
            }).show();
    }
    private void analyze(String name) {
        if(BemInstaller.busy)return;
        if(name!=null && name.isEmpty()){status.setText(Text.of("组合名称不能为空"));return;}
        busy=true;status.setText(Text.of("正在分析组合…"));content.setVisibility(View.INVISIBLE);
        worker.execute(()->{
            try {
                JSONObject request;
                if(name==null)request=session.read("request.json");
                else {
                    JSONArray inputs=new JSONArray(),index=BemInstaller.index(this);
                    for(String generation:sources){JSONObject found=null;for(JSONObject entry:BemComposeState.objects(index))if(generation.equals(entry.optString("generation")))found=entry;
                        if(found==null)throw new IOException(Text.of("源包已更新，请重新打开预览。"));
                        String selection=found.optInt("bem_minor",0)>0?found.optString("selected_options",found.optString("default_options")):found.optString("selected_appearance",found.optString("default_appearance"));
                        inputs.put(new JSONObject().put("path",BemComposeSession.installed(this,generation).getAbsolutePath()).put("selection",selection).put("parameters",""));
                    }
                    request=new JSONObject().put("schema",1).put("platform","android-arm64").put("project_id","compose."+session.id()).put("name",name).put("author","BEM Composer").put("version","auto").put("sources",inputs);
                }
                session.write("request.json",request);session.analyze();
                BemComposeState next=new BemComposeState(session.read("plan.json"),session.read("choices.json"));
                runOnUiThread(()->{if(!closed){state=next;selected.clear();expanded.clear();status.setText(Text.of("分析完成"));render();}});
            }catch(Exception error){runOnUiThread(()->{if(!closed)error(error);});}
            finally{runOnUiThread(()->{busy=false;if(!closed)content.setVisibility(View.VISIBLE);});}
        });
    }
    private void renderGroup(JSONObject group) {
        String parent=group.optString("id");LinearLayout card=new LinearLayout(this);card.setOrientation(LinearLayout.VERTICAL);card.setPadding(dp(8),dp(8),dp(8),dp(12));card.setBackgroundResource(R.drawable.bg_card);
        LinearLayout.LayoutParams layout=new LinearLayout.LayoutParams(-1,-2);layout.topMargin=dp(10);content.addView(card,layout);
        String heading=group.optString("display_name")+(group.optString("resource_name").isEmpty()?"":" · "+group.optString("resource_name"));card.addView(label(heading));
        List<JSONObject> candidates=BemComposeState.objects(group.optJSONArray("candidates"));String value=state.choice(group);
        String current=value;for(JSONObject candidate:candidates)if(value.equals(candidate.optString("id")))current=candidate.optString("name");
        String shown=localChoice(value,current);
        card.addView(button(shown,()->{
            String[] options=new String[candidates.size()];for(int i=0;i<options.length;i++)options[i]=localChoice(candidates.get(i).optString("id"),candidates.get(i).optString("name"));
            new AlertDialog.Builder(this).setTitle(heading).setItems(options,(d,w)->{try{state.choices.put(parent,candidates.get(w).getString("id"));persist();}catch(Exception error){error(error);}}).show();
        }));
        List<JSONObject> segments=BemComposeState.objects(group.optJSONArray("segments"));if(segments.isEmpty())return;
        if(segments.stream().anyMatch(s->s.has("body_region")))renderBody(card,group,segments);
        Button expand=button(expanded.contains(parent)?"收起细分":"展开细分",()->{if(!expanded.add(parent))expanded.remove(parent);render();});card.addView(expand);
        if(!expanded.contains(parent))return;
        LinearLayout bulk=row(card);action(bulk,"启用所选",()->set(group,selectedSegments(segments),true));action(bulk,"停用所选",()->set(group,selectedSegments(segments),false));action(bulk,"高亮所选",()->highlight(group,selectedSegments(segments)));
        String source="";
        for(List<JSONObject> members:state.bundles(group).values()) {
            JSONObject first=members.get(0);
            if(!source.equals(first.optString("source_id"))){source=first.optString("source_id");card.addView(label(first.optString("source_name")));
                List<String> shared=new ArrayList<>();for(JSONObject binding:BemComposeState.objects(first.optJSONArray("shared_option_bindings")))shared.add(binding.optString("name"));if(!shared.isEmpty())card.addView(label(Text.of("共用选项：")+String.join(" / ",shared)));
            }
            renderRecord(card,group,members,state.label(first));
            if(members.size()>1){String key=parent+"|"+first.optString("id");card.addView(button(expanded.contains(key)?"收起绘制记录":"展开绘制记录",()->{if(!expanded.add(key))expanded.remove(key);render();}));
                if(expanded.contains(key))for(JSONObject segment:members)renderRecord(card,group,List.of(segment),Text.of("绘制段 ")+(segment.optInt("draw")+1)+" · "+segment.optString("material_name"));
            }
        }
    }
    private String localChoice(String id,String name){return switch(id){case "original"->Text.of("保留原版 / 不加入");case "split"->Text.of("自定义分段");default->name;};}
    private List<JSONObject> selectedSegments(List<JSONObject> segments){List<JSONObject> result=new ArrayList<>();for(JSONObject segment:segments)if(selected.contains(segment.optString("id")) && state.triangles(segment)>0)result.add(segment);return result;}
    private void renderRecord(LinearLayout parent,JSONObject group,List<JSONObject> members,String name) {
        int count=members.stream().mapToInt(state::triangles).sum();boolean all=members.stream().filter(s->state.triangles(s)>0).allMatch(s->state.enabled(group,s));boolean any=members.stream().filter(s->state.triangles(s)>0).anyMatch(s->state.enabled(group,s));
        LinearLayout line=row(parent);CheckBox select=new CheckBox(this);select.setChecked(members.stream().allMatch(s->selected.contains(s.optString("id"))));select.setEnabled(count>0);select.setContentDescription(Text.of("选择")+name);select.setOnCheckedChangeListener((b,on)->{for(JSONObject segment:members){String id=segment.optString("id");if(on)selected.add(id);else selected.remove(id);}});line.addView(select);
        TextView title=label(name+" · "+count+Text.of(" 个三角形")+" · "+Text.of(count==0?"未提供":all?"已启用":any?"部分启用":"已停用"));line.addView(title,new LinearLayout.LayoutParams(0,-2,1));
        Button toggle=button(all?"停用":"启用",()->set(group,members,!all));toggle.setEnabled(count>0);line.addView(toggle,new LinearLayout.LayoutParams(dp(72),-2));
    }
    private void set(JSONObject group,List<JSONObject> targets,boolean value){if(targets.isEmpty()){status.setText(Text.of("请先选择绘制段"));return;}try{state.setEnabled(group,targets,value);persist();}catch(Exception error){error(error);}}
    private void renderBody(LinearLayout card,JSONObject group,List<JSONObject> segments) {
        for(String region:new String[]{"neck","head","torso","pelvis","left_arm","right_arm","left_leg","right_leg","unclassified"}) {
            List<JSONObject> present=new ArrayList<>();for(JSONObject segment:segments)if(region.equals(segment.optString("body_region")) && state.triangles(segment)>0)present.add(segment);
            Set<String> active=new LinkedHashSet<>();for(JSONObject segment:present)if(state.enabled(group,segment))active.add(segment.optString("source_name"));
            String text=BemComposeSession.bodyName(region)+" · "+(present.isEmpty()?Text.of("未提供"):active.isEmpty()?Text.of("不启用"):String.join(" + ",active));
            Button choose=button(text,()->{
                LinkedHashMap<String,List<JSONObject>> options=new LinkedHashMap<>();for(JSONObject segment:present)options.computeIfAbsent(segment.optString("source_name"),k->new ArrayList<>()).add(segment);
                List<String> names=new ArrayList<>(options.keySet());names.add(0,Text.of("不启用"));
                new AlertDialog.Builder(this).setTitle(BemComposeSession.bodyName(region)).setItems(names.toArray(new String[0]),(d,w)->{
                    try{state.setEnabled(group,present,false);if(w>0)state.setEnabled(group,options.get(names.get(w)),true);persist();}catch(Exception error){error(error);}
                }).show();
            });choose.setMaxLines(2);choose.setEllipsize(TextUtils.TruncateAt.END);choose.setEnabled(!present.isEmpty());card.addView(choose);
        }
        card.addView(button("预览与分区修正",()->preview(null)));
    }
    private void renderSourceOptions(){
        JSONArray projects=state.plan.optJSONArray("projects");
        if(projects==null)sourceOptions(state.plan,"");
        else for(JSONObject project:BemComposeState.objects(projects))sourceOptions(project.optJSONObject("plan"),project.optString("id")+".");
    }
    private void sourceOptions(JSONObject plan,String prefix) {
        for(JSONObject source:BemComposeState.objects(plan.optJSONArray("source_options"))) {
            String expandKey=prefix+"source"+source.optInt("source_index");
            content.addView(button(source.optString("name")+" · "+Text.of("切换选项"),()->{if(!expanded.add(expandKey))expanded.remove(expandKey);render();}));
            if(!expanded.contains(expandKey))continue;
            List<JSONObject> groups=BemComposeState.objects(source.optJSONArray("groups"));java.util.Map<String,String> active=new java.util.HashMap<>();
            for(int i=0;i<groups.size();i++)active.put(groups.get(i).optString("id"),state.choices.optString(prefix+"s"+source.optInt("source_index")+".g"+i,groups.get(i).optString("default")));
            for(int i=0;i<groups.size();i++) {
                JSONObject group=groups.get(i);String id=prefix+"s"+source.optInt("source_index")+".g"+i;
                List<JSONObject> options=BemComposeState.objects(group.optJSONArray("choices"));String choice=active.get(group.optString("id")),current=choice;
                for(JSONObject option:options)if(choice.equals(option.optString("id")))current=option.optString("name");
                Button button=button(group.optString("name")+" · "+current,()->{
                    String[] labels=new String[options.size()];for(int n=0;n<labels.length;n++)labels[n]=options.get(n).optString("name");
                    new AlertDialog.Builder(this).setTitle(group.optString("name")).setItems(labels,(d,w)->{try{state.choices.put(id,options.get(w).getString("id"));persist();}catch(Exception error){error(error);}}).show();
                });
                try{button.setEnabled(BemOptions.test(group.opt("available_when"),active));}catch(Exception unavailable){button.setEnabled(false);}
                content.addView(button);
            }
        }
    }
    private void highlight(JSONObject group,List<JSONObject> segments){
        if(segments.isEmpty()){status.setText(Text.of("请先选择绘制段"));return;}
        Set<String> layouts=new HashSet<>();for(JSONObject segment:segments)layouts.add(segment.optJSONObject("layout").toString());
        if(layouts.size()>1){status.setText(Text.of("所选段的顶点布局不兼容，请分别查看"));return;}
        try {
            JSONArray keys=new JSONArray();for(JSONObject segment:segments)keys.put(segment.optString("preview_key"));
            JSONObject inspect=new JSONObject().put("choices",state.inspection(group,segments)).put("keys",keys);preview(inspect);
        }catch(Exception error){error(error);}
    }
    private void preview(JSONObject inspect){
        try{session.write("inspection.json",inspect==null?new JSONObject():inspect);BemPreviewActivity.openComposition(this,session.id());}catch(Exception error){error(error);}
    }
    @Override protected void onDestroy(){closed=true;handler.removeCallbacks(refresh);worker.shutdown();super.onDestroy();}
}
