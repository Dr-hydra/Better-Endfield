package dev.betterendfield.next;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.webkit.JavascriptInterface;
import android.webkit.WebResourceRequest;
import android.webkit.WebResourceResponse;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.LinearLayout;
import android.widget.TextView;
import org.json.JSONArray;
import org.json.JSONObject;
import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.util.UUID;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.atomic.AtomicBoolean;

/** Standalone preview; drafts use the installed settings transaction on explicit save. */
public final class BemPreviewActivity extends ModuleActivity {
    private static final String HOST="bem-preview.local";
    private final ExecutorService worker=Executors.newSingleThreadExecutor();
    private final AtomicBoolean loading=new AtomicBoolean();
    private volatile boolean closed;
    private File directory,cancelFile;
    private String generation,currentScene,selectionDraft,parameterDraft;
    private long sourceSize,sourceTime;
    private WebView web;
    private LinearLayout root;
    private BemComposeSession composition;
    private JSONObject compositionReport,inspection;
    private File compositionOutput;
    private String compositionSignature="",outputPrefix="",editRegion="neck";
    private int outputIndex;
    private boolean editing;
    private ButtonRow composeBar;
    private final java.util.ArrayDeque<String> undo=new java.util.ArrayDeque<>();
    private static native String previewNative(String input,String output,String selection,String parameters,String resource,String cancel,String gpu) throws java.io.IOException;

    static void open(Activity activity,String generation) {
        activity.startActivity(new Intent(activity,BemPreviewActivity.class).putExtra("generation",generation));
    }
    static void openComposition(Activity activity,String session) {
        activity.startActivity(new Intent(activity,BemPreviewActivity.class).putExtra("composition",session));
    }
    @Override public void onCreate(Bundle state) {
        super.onCreate(state);
        root=new LinearLayout(this);root.setOrientation(LinearLayout.VERTICAL);setContentView(root);
        generation=getIntent().getStringExtra("generation");
        String session=getIntent().getStringExtra("composition");
        if(session!=null)try{composition=BemComposeSession.open(this,session);inspection=composition.read("inspection.json");addCompositionBar();}catch(Exception failure){error(failure.getMessage());return;}
        else if(generation==null || !generation.matches("[a-f0-9-]{36}")) {error("Invalid installed package");return;}
        setTitle(Text.of("模型预览"));
        if(GameResourceStore.resolve(this,"preview")==null)
            error(Text.of("未下载预览资源。请前往「关于」页点击「下载预览资源」，下载完成后重新打开预览。"));
        directory=new File(getCacheDir(),"bem-preview-"+UUID.randomUUID());
        cancelFile=new File(directory,"cancel");
        try {
            if(!directory.mkdirs()) throw new java.io.IOException("Cannot create preview cache");
            web=new WebView(this);
            WebSettings settings=web.getSettings();settings.setJavaScriptEnabled(true);
            settings.setAllowFileAccess(false);settings.setAllowContentAccess(false);
            settings.setMixedContentMode(WebSettings.MIXED_CONTENT_NEVER_ALLOW);
            settings.setMediaPlaybackRequiresUserGesture(true);
            web.addJavascriptInterface(new Bridge(),"BemHost");
            web.setWebViewClient(new WebViewClient() {
                @Override public boolean shouldOverrideUrlLoading(WebView view,WebResourceRequest request) {return true;}
                @Override public WebResourceResponse shouldInterceptRequest(WebView view,WebResourceRequest request) {
                    try {
                        var uri=request.getUrl();
                        if(!"https".equals(uri.getScheme()) || !HOST.equals(uri.getHost()) || !"GET".equals(request.getMethod())) return denied();
                        String path=uri.getPath();if(path==null) return denied();
                        String name=path.substring(1);
                        java.io.InputStream input;String mime;
                        if(name.matches("data/[a-f0-9]{32}/(scene[.]json|[0-9]+[.]bin)")) {
                            File file=new File(directory,name);
                            if(!file.getCanonicalPath().startsWith(directory.getCanonicalPath()+File.separator)) return denied();
                            input=new FileInputStream(file);mime=name.endsWith("json")?"application/json":"application/octet-stream";
                        } else if(java.util.Set.of("index.html","renderer.js","preview.js","preview.css","bcn.js","decode-worker.js").contains(name)) {
                            input=getAssets().open("bem-preview/"+name);
                            mime=name.endsWith("html")?"text/html":name.endsWith("css")?"text/css":"text/javascript";
                        } else return denied();
                        return new WebResourceResponse(mime,mime.equals("application/octet-stream")?null:"UTF-8",200,"OK",java.util.Map.of("Cache-Control","no-store","X-Content-Type-Options","nosniff"),input);
                    } catch(Exception ignored) {return denied();}
                }
                @Override public boolean onRenderProcessGone(WebView view,android.webkit.RenderProcessGoneDetail detail) {
                    root.removeView(view);view.destroy();web=null;error(Text.of("预览进程已退出，请重新打开。"));return true;
                }
            });
            root.addView(web,new LinearLayout.LayoutParams(-1,0,1));
            String language=getResources().getConfiguration().getLocales().get(0).getLanguage();
            web.loadUrl("https://"+HOST+"/index.html?lang="+(language.equals("zh")?"zh":"en")+(composition==null?"":"&mode=composition"));
        } catch(Exception exception) {error(Text.of("无法打开预览：")+exception.getMessage());}
    }
    private static WebResourceResponse denied() {
        return new WebResourceResponse("text/plain","UTF-8",404,"Not Found",java.util.Map.of(),new ByteArrayInputStream(new byte[0]));
    }
    private void error(String message) {
        TextView label=new TextView(this);label.setPadding(24,24,24,24);label.setText(message);root.addView(label);
    }
    public final class Bridge {
        @JavascriptInterface public void request(String message) {
            if(closed || message==null || message.length()>131072) return;
            try {
                JSONObject request=new JSONObject(message);
                String type=request.optString("type");
                if(composition!=null && type.equals("loaded")){runOnUiThread(()->{if(!closed)syncBodyEdit();});return;}
                if(composition!=null && type.equals("bodyPick")){
                    if(!editing || inspection.has("choices") || !request.optString("id").equals(currentScene) || !loading.compareAndSet(false,true))return;
                    worker.execute(()->{try{pickBody(request);runOnUiThread(()->{loading.set(false);if(!closed && web!=null)web.evaluateJavascript("window.bemPreviewRefreshDraft(true)",null);});}catch(Exception failure){loading.set(false);callback("bemPreviewError",failure.getMessage());}});return;
                }
                if(!type.equals("load") && !type.equals("save"))return;
                String resource=request.optString("resource");if(resource.length()>256 || !loading.compareAndSet(false,true))return;
                worker.execute(()->{
                    try {
                        if(type.equals("save"))save(request.optString("id"));
                        else load(resource,request.has("selection")?request.optString("selection"):null,request.has("parameters")?request.optString("parameters"):null,request.optJSONObject("gpu"));
                    } finally {loading.set(false);}
                });
            } catch(Exception ignored) {}
        }
    }
    private JSONObject installed() throws Exception {
        JSONArray entries=BemInstaller.index(this);
        for(int i=0;i<entries.length();i++)if(generation.equals(entries.getJSONObject(i).optString("generation")))return entries.getJSONObject(i);
        throw new java.io.IOException(Text.of("模型包已移除，请关闭预览。"));
    }
    private File source() {return new File(new File(new File(getFilesDir(),"bem-installed"),generation),"installed.bem");}
    private void callback(String method,String value) {
        runOnUiThread(()->{if(!closed && web!=null)web.evaluateJavascript("window."+method+"("+JSONObject.quote(value)+")",null);});
    }
    private void save(String id) {
        try {
            if(closed)return;
            if(currentScene==null || !currentScene.equals(id))throw new java.io.IOException("Stale preview");
            File input=composition==null?source():compositionSource();
            if(!input.isFile() || input.length()!=sourceSize || input.lastModified()!=sourceTime)
                throw new java.io.IOException(Text.of("源包已更新，请重新打开预览。"));
            if(composition!=null){
                if(inspection.has("choices"))throw new java.io.IOException("Inspection cannot be saved");
                composition.applySelection(selectionDraft,compositionDefaults(),outputPrefix);
                callback("bemPreviewSaved",Text.of("已应用到组合"));return;
            }
            JSONObject entry=installed();
            JSONObject change=new JSONObject().put("generation",generation)
                .put(entry.optInt("bem_minor",0)>0?"options":"appearance",selectionDraft);
            if(entry.optInt("bem_minor",0)>=3) {
                // Only declared sliders enter the patch; the transaction retains unknown remembered values.
                java.util.Map<String,Integer> remembered=BemParameters.remembered(parameterDraft);
                java.util.LinkedHashMap<String,Integer> values=new java.util.LinkedHashMap<>();
                JSONArray parameters=BemParameters.groups(entry);
                for(int i=0;i<parameters.length();i++) {
                    JSONObject parameter=parameters.getJSONObject(i);String key=parameter.getString("id");
                    values.put(key,remembered.getOrDefault(key,parameter.getInt("default")));
                }
                change.put("parameters",BemParameters.encode(values));
            }
            BemInstaller.saveAll(this,new JSONArray().put(change));
            BemInstaller.status=Text.of("设置已保存");
            callback("bemPreviewSaved",Text.of("设置已保存"));
        } catch(Exception exception) {callback("bemPreviewSaveError",String.valueOf(exception.getMessage()));}
    }
    private void load(String resource,String selection,String parameters,JSONObject gpu) {
        String id=UUID.randomUUID().toString().replace("-","");File output=new File(directory,"data/"+id);
        try {
            if(closed)return;
            JSONObject entry=composition==null?installed():null;
            File input=composition==null?source():prepareComposition();
            long size=input.length(),time=input.lastModified();
            if(selection==null)selection=entry==null?"":entry.optInt("bem_minor",0)>0?entry.optString("selected_options",entry.optString("default_options")):entry.optString("selected_appearance",entry.optString("default_appearance"));
            if(parameters==null)parameters=entry==null?"":entry.optString("remembered_parameters",entry.optString("selected_parameters",entry.optString("default_parameters")));
            // Optional resources must never prevent the BEM itself from loading.
            GameResourceStore.Snapshot nativePack=GameResourceStore.resolve(this,"preview");
            if(gpu==null)gpu=new JSONObject();
            gpu.put("nativeAssets",nativePack==null?"":nativePack.root().getAbsolutePath());
            if(composition!=null)gpu.put("compositionDraft",true);
            gpu.put("textureCache",new File(getCacheDir(),"bem-preview-textures").getAbsolutePath());
            BemInstaller.loadCodec();
            previewNative(input.getAbsolutePath(),output.getAbsolutePath(),selection,parameters,resource,cancelFile.getAbsolutePath(),gpu==null?"{}":gpu.toString());
            if(closed)return;
            if(input.length()!=size || input.lastModified()!=time)throw new java.io.IOException(Text.of("源包已更新，请重新打开预览。"));
            JSONObject scene=new JSONObject(new String(java.nio.file.Files.readAllBytes(new File(output,"scene.json").toPath()),java.nio.charset.StandardCharsets.UTF_8));
            selectionDraft=scene.getString("selection");parameterDraft=scene.getString("parameters");sourceSize=size;sourceTime=time;
            String previous=currentScene;currentScene=id;
            File[] folders=new File(directory,"data").listFiles();
            if(folders!=null) for(File folder:folders) if(!folder.getName().equals(id) && !folder.getName().equals(previous)) remove(folder);
            callback("bemPreviewLoad",id);
            if(composition!=null && inspection.has("choices"))script("window.bemPreviewHighlightSegments("+inspection.optJSONArray("keys")+",true)");
        } catch(Exception exception) {
            remove(output);callback("bemPreviewError",exception.getMessage()==null?"Preview failed":exception.getMessage());
        }
    }
    private JSONObject compositionDefaults() throws Exception {
        JSONArray parts=compositionReport.optJSONArray("parts");
        return (parts==null?compositionReport:parts.getJSONObject(outputIndex)).getJSONObject("defaults");
    }
    private File compositionSource() throws Exception {
        JSONArray outputs=compositionReport.getJSONArray("outputs");
        outputIndex=Math.min(outputIndex,outputs.length()-1);
        File source=new File(outputs.getString(outputIndex));
        outputPrefix=compositionReport.has("parts")?source.getParentFile().getName()+".":"";
        return source;
    }
    private File prepareComposition() throws Exception {
        JSONObject choices=inspection.has("choices")?inspection.getJSONObject("choices"):composition.read("choices.json");
        String signature=choices.toString();
        if(compositionOutput==null || !signature.equals(compositionSignature)) {
            File next=new File(directory,"composition-"+UUID.randomUUID());
            File defaults=new File(directory,"compose-choices.json");
            java.nio.file.Files.write(defaults.toPath(),signature.getBytes(java.nio.charset.StandardCharsets.UTF_8));
            JSONObject report=composition.run("draft",next,cancelFile,defaults);
            File previous=compositionOutput;compositionOutput=next;compositionReport=report;compositionSignature=signature;
            if(previous!=null)remove(previous);
            runOnUiThread(()->{if(!closed && composeBar!=null)composeBar.outputs.setText(Text.of("预览包 ")+(outputIndex+1)+" / "+report.optJSONArray("outputs").length());});
        }
        return compositionSource();
    }
    private void script(String script){runOnUiThread(()->{if(!closed && web!=null)web.evaluateJavascript(script,null);});}
    private final class ButtonRow {
        android.widget.Button outputs,edit;
        android.widget.Spinner region;
    }
    private android.widget.Button composeButton(LinearLayout line,String title,Runnable action){
        android.widget.Button button=new android.widget.Button(this);button.setText(Text.of(title));button.setAllCaps(false);button.setOnClickListener(v->{if(!loading.get())action.run();});line.addView(button,new LinearLayout.LayoutParams(0,-2,1));return button;
    }
    private void addCompositionBar() {
        composeBar=new ButtonRow();LinearLayout first=new LinearLayout(this);root.addView(first);
        composeButton(first,"返回组合",this::finish);
        composeBar.outputs=composeButton(first,"预览包",()->{
            if(compositionReport==null)return;
            JSONArray outputs=compositionReport.optJSONArray("outputs");String[] names=new String[outputs.length()];
            for(int i=0;i<names.length;i++)names[i]=Text.of("预览包 ")+(i+1);
            new android.app.AlertDialog.Builder(this).setItems(names,(d,w)->{outputIndex=w;editing=false;script("window.bemPreviewBodyEdit([]);window.bemPreviewRefreshDraft(false)");composeBar.outputs.setText(names[w]);}).show();
        });
        if(inspection.has("choices"))return;
        LinearLayout second=new LinearLayout(this);root.addView(second);
        composeBar.region=new android.widget.Spinner(this);
        String[] regions={"neck","head","torso","pelvis","left_arm","right_arm","left_leg","right_leg","unclassified"};String[] names=new String[regions.length];
        for(int i=0;i<regions.length;i++)names[i]=BemComposeSession.bodyName(regions[i]);
        composeBar.region.setAdapter(new android.widget.ArrayAdapter<>(this,android.R.layout.simple_spinner_dropdown_item,names));
        composeBar.region.setOnItemSelectedListener(new android.widget.AdapterView.OnItemSelectedListener(){public void onNothingSelected(android.widget.AdapterView<?> parent){} public void onItemSelected(android.widget.AdapterView<?> parent,android.view.View view,int index,long id){editRegion=regions[index];}});
        second.addView(composeBar.region,new LinearLayout.LayoutParams(0,-2,1));
        composeBar.edit=composeButton(second,"点面修正",()->{editing=!editing;syncBodyEdit();});
        composeButton(second,"撤销",()->{
            if(undo.isEmpty())return;
            try{composition.write("choices.json",new JSONObject(undo.removeLast()));script("window.bemPreviewRefreshDraft(true)");}catch(Exception failure){error(failure.getMessage());}
        });
        composeButton(second,"自动分区",()->{
            try{JSONObject choices=composition.read("choices.json");undo.add(choices.toString());choices.remove("body_edits");composition.write("choices.json",choices);script("window.bemPreviewRefreshDraft(true)");}catch(Exception failure){error(failure.getMessage());}
        });
    }
    private void syncBodyEdit() {
        if(composition==null || composeBar==null || composeBar.edit==null)return;
        composeBar.edit.setText(Text.of(editing?"结束修正":"点面修正"));
        try {
            BemComposeState state=new BemComposeState(composition.read("plan.json"),composition.read("choices.json"));
            java.util.Set<String> draws=new java.util.LinkedHashSet<>();
            for(JSONObject group:state.groups())if(outputPrefix.isEmpty() || group.optString("id").startsWith(outputPrefix))
                for(JSONObject segment:BemComposeState.objects(group.optJSONArray("segments")))if(segment.has("body_draw"))draws.add(segment.optString("body_draw"));
            composeBar.edit.setEnabled(!draws.isEmpty());
            script("window.bemPreviewBodyEdit("+new JSONArray(editing?draws:java.util.Set.of())+")");
        }catch(Exception failure){error(failure.getMessage());}
    }
    private void pickBody(JSONObject request) throws Exception {
        String draw=request.optString("bodyDraw");int triangle=request.optInt("triangle",-1);if(triangle<0)return;
        composition.applySelection(selectionDraft,compositionDefaults(),outputPrefix);
        BemComposeState state=new BemComposeState(composition.read("plan.json"),composition.read("choices.json"));
        JSONObject destination=null,parent=null;boolean known=false;
        for(JSONObject group:state.groups())if(outputPrefix.isEmpty() || group.optString("id").startsWith(outputPrefix))for(JSONObject segment:BemComposeState.objects(group.optJSONArray("segments")))if(draw.equals(segment.optString("body_draw"))){
            JSONArray triangles=segment.optJSONArray("triangles");if(triangles!=null)for(int i=0;i<triangles.length();i++)if(triangles.optInt(i)==triangle)known=true;
            if(editRegion.equals(segment.optString("body_region"))){destination=segment;parent=group;}
        }
        if(!known || destination==null)return;
        String before=state.choices.toString();
        state.setEnabled(parent,java.util.List.of(destination),true);
        JSONObject edits=state.choices.optJSONObject("body_edits");if(edits==null){edits=new JSONObject();state.choices.put("body_edits",edits);}
        JSONObject faces=edits.optJSONObject(draw);if(faces==null){faces=new JSONObject();edits.put(draw,faces);}faces.put(Integer.toString(triangle),editRegion);
        if(undo.size()>=30)undo.removeFirst();undo.add(before);composition.write("choices.json",state.choices);
    }
    private static void remove(File file) {
        if(file==null)return;File[] children=file.listFiles();if(children!=null) for(File child:children)remove(child);file.delete();
    }
    @Override protected void onPause() {if(web!=null){web.evaluateJavascript("window.bemPreviewPause(true)",null);web.onPause();}super.onPause();}
    @Override protected void onResume() {super.onResume();if(web!=null){web.onResume();web.evaluateJavascript("window.bemPreviewPause(false)",null);}}
    @Override protected void onDestroy() {
        closed=true;
        if(cancelFile!=null)try(FileOutputStream out=new FileOutputStream(cancelFile)){out.write(1);}catch(Exception ignored){}
        if(web!=null){web.removeJavascriptInterface("BemHost");root.removeView(web);web.destroy();web=null;}
        worker.execute(()->remove(directory));worker.shutdown();super.onDestroy();
    }
}
