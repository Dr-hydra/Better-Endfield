package dev.betterendfield.android;

import android.app.Activity;
import android.os.*;
import android.webkit.*;
import android.widget.*;
import org.json.*;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.*;
import java.util.concurrent.*;

/** A separate local origin per immutable module generation; native identity stays in Java. */
public final class ThirdPartyModuleActivity extends Activity {
    private WebView web;
    private TextView status;
    private String id,origin,script;
    private File directory;
    private volatile boolean closed;
    private final Handler handler=new Handler(Looper.getMainLooper());
    private final ExecutorService worker=Executors.newSingleThreadExecutor();
    private final Runnable poll=new Runnable(){public void run(){
        if(closed)return;
        worker.execute(()->{
            try {JSONArray messages=ThirdPartyModuleStore.runtime(ThirdPartyModuleActivity.this,"poll",id,null,null).optJSONArray("messages");
                if(messages!=null)for(int i=0;i<messages.length();++i)deliver(new JSONObject().put("kind","runtime_message").put("message",messages.get(i)));
            } catch(Exception ignored) {}
            if(!closed)handler.postDelayed(this,600);
        });
    }};
    @Override protected void onCreate(Bundle state) {
        super.onCreate(state);
        LinearLayout layout=new LinearLayout(this);layout.setOrientation(LinearLayout.VERTICAL);
        status=new TextView(this);status.setPadding(16,12,16,12);layout.addView(status,new LinearLayout.LayoutParams(-1,-2));
        web=new WebView(this);layout.addView(web,new LinearLayout.LayoutParams(-1,0,1));setContentView(layout);
        try {
            id=getIntent().getStringExtra("module_id");ThirdPartyModulePackage.id(id,true);
            JSONObject entry=ThirdPartyModuleStore.find(ThirdPartyModuleStore.index(this),id),manifest=ThirdPartyModuleStore.manifest(entry);
            String ui=manifest.optString("ui","");if(ui.isEmpty())throw new IOException("此模块没有网页界面");
            directory=new File(entry.getString("directory")).getCanonicalFile();origin="https://m-"+entry.getString("generation").replace("-","")+".bemod.local";
            setTitle(manifest.getString("name"));script=read(getAssets().open("third-party-bridge.js"),256*1024);
            WebSettings settings=web.getSettings();settings.setJavaScriptEnabled(true);settings.setDomStorageEnabled(true);
            settings.setAllowFileAccess(false);settings.setAllowContentAccess(false);settings.setMixedContentMode(WebSettings.MIXED_CONTENT_NEVER_ALLOW);
            web.addJavascriptInterface(new Bridge(),"BetterEndfieldModuleHost");
            web.setWebViewClient(new WebViewClient(){
                @Override public boolean shouldOverrideUrlLoading(WebView view,WebResourceRequest request){return !owned(request.getUrl().toString());}
                @Override public WebResourceResponse shouldInterceptRequest(WebView view,WebResourceRequest request) {
                    try {
                        if(!owned(request.getUrl().toString()))return response(403,"Forbidden",new ByteArrayInputStream(new byte[0]),"text/plain");
                        String path=request.getUrl().getPath();if(path==null)return response(404,"Not Found",new ByteArrayInputStream(new byte[0]),"text/plain");
                        File file=ThirdPartyModulePackage.resource(directory,path.startsWith("/")?path.substring(1):path);
                        if(!file.isFile())return response(404,"Not Found",new ByteArrayInputStream(new byte[0]),"text/plain");
                        String mime=mime(file.getName());InputStream input=new FileInputStream(file);
                        if(mime.equals("text/html")) {
                            String html=read(input,16*1024*1024),lower=html.toLowerCase(Locale.ROOT);int head=lower.indexOf("<head"),at=head<0?-1:html.indexOf('>',head);
                            if(at<0){int doc=lower.indexOf("<!doctype");at=doc<0?-1:html.indexOf('>',doc);}
                            String injected=html.substring(0,at+1)+"<script>"+script+"</script>"+html.substring(at+1);
                            input=new ByteArrayInputStream(injected.getBytes(StandardCharsets.UTF_8));
                        }
                        return response(200,"OK",input,mime);
                    } catch(Exception error){return response(404,"Not Found",new ByteArrayInputStream(new byte[0]),"text/plain");}
                }
                @Override public void onPageFinished(WebView view,String url){status.setText("本地模块界面 · 配置保存在本机；游戏连接后处理消息");}
            });
            web.loadUrl(origin+"/"+ui);handler.post(poll);
        } catch(Exception error){status.setText("模块网页无法打开："+error.getMessage());}
    }
    private boolean owned(String url){return url!=null && (url.equals(origin) || url.startsWith(origin+"/"));}
    private static String read(InputStream input,int limit) throws IOException {
        try(InputStream stream=input;ByteArrayOutputStream bytes=new ByteArrayOutputStream()) {
            byte[] buffer=new byte[8192];int count;while((count=stream.read(buffer))!=-1){if(bytes.size()+count>limit)throw new IOException("网页文本过大");bytes.write(buffer,0,count);}
            return new String(bytes.toByteArray(),StandardCharsets.UTF_8);
        }
    }
    private static String mime(String file) {
        String lower=file.toLowerCase(Locale.ROOT);
        if(lower.endsWith(".html"))return "text/html";if(lower.endsWith(".js"))return "application/javascript";
        if(lower.endsWith(".css"))return "text/css";if(lower.endsWith(".json"))return "application/json";
        String extension=MimeTypeMap.getFileExtensionFromUrl(lower),mime=MimeTypeMap.getSingleton().getMimeTypeFromExtension(extension);
        return mime==null?"application/octet-stream":mime;
    }
    private static WebResourceResponse response(int status,String reason,InputStream input,String mime) {
        return new WebResourceResponse(mime,"UTF-8",status,reason,Collections.singletonMap("X-Content-Type-Options","nosniff"),input);
    }
    private void deliver(JSONObject envelope) {
        handler.post(()->{if(!closed)web.evaluateJavascript("window.__beModuleDeliver&&window.__beModuleDeliver("+envelope+");",null);});
    }
    private final class Bridge {
        @JavascriptInterface public void postMessage(String source) {
            if(closed || source==null || source.length()>256*1024)return;
            worker.execute(()->{
                String requestId="";
                try {
                    JSONObject request=new JSONObject(source);if(!request.optString("protocol").equals("better-endfield.module-ui.v1"))return;
                    requestId=request.getString("request_id");if(requestId.length()>128)return;
                    String operation=request.getString("operation");Object value;
                    switch(operation) {
                        case "readConfig":value=ThirdPartyModuleStore.find(ThirdPartyModuleStore.index(ThirdPartyModuleActivity.this),id).getJSONObject("configuration");break;
                        case "saveConfig":
                            JSONObject configuration=request.getJSONObject("payload");ThirdPartyModuleStore.saveConfig(ThirdPartyModuleActivity.this,id,configuration);
                            try {ThirdPartyModuleStore.runtime(ThirdPartyModuleActivity.this,"configure",id,configuration,null);}catch(IOException ignored){}
                            value=new JSONObject().put("saved",true);break;
                        case "send":value=ThirdPartyModuleStore.runtime(ThirdPartyModuleActivity.this,"send",id,request.opt("payload"),requestId);break;
                        case "status":
                            try {value=ThirdPartyModuleStore.runtime(ThirdPartyModuleActivity.this,"status",id,null,null);}catch(IOException offline){value=new JSONObject().put("connected",false);}break;
                        default:throw new IOException("不支持的网页模块操作");
                    }
                    deliver(new JSONObject().put("kind","bridge_reply").put("request_id",requestId).put("value",value));
                } catch(Exception error){try {deliver(new JSONObject().put("kind","bridge_reply").put("request_id",requestId).put("error",error.getMessage()));}catch(JSONException ignored){}}
            });
        }
    }
    @Override protected void onDestroy(){closed=true;handler.removeCallbacksAndMessages(null);worker.shutdownNow();if(web!=null){web.removeJavascriptInterface("BetterEndfieldModuleHost");web.destroy();}super.onDestroy();}
}
