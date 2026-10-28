package dev.betterendfield.next;

import android.content.Context;
import org.json.*;
import java.io.*;
import java.net.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.*;
import java.util.*;
import java.util.concurrent.atomic.AtomicBoolean;

/** One release index for software and platform-specific, immutable resource packs. */
final class DistributionUpdater {
    static final String ROOT="https://github.com/Dr-hydra/Better-Endfield/releases/download/game-resources/";
    private static JSONObject cached;
    private static long cachedAt;
    static final AtomicBoolean busy=new AtomicBoolean(),cancelled=new AtomicBoolean();
    private DistributionUpdater(){}
    static HttpURLConnection connect(String url) throws Exception {
        HttpURLConnection c=(HttpURLConnection)new URL(url).openConnection();
        c.setConnectTimeout(15000);c.setReadTimeout(30000);
        c.setRequestProperty("User-Agent","BetterEndfieldNext-Android/"+BuildConfig.VERSION_NAME);
        return c;
    }
    static synchronized JSONObject index() throws Exception {
        if(cached!=null && System.currentTimeMillis()-cachedAt<120000)return cached;
        HttpURLConnection c=connect(ROOT+"distribution-index.json");
        try {
            if(c.getResponseCode()==404)return null;
            if(c.getResponseCode()!=200)throw new IOException("HTTP "+c.getResponseCode());
            ByteArrayOutputStream bytes=new ByteArrayOutputStream();
            try(InputStream input=c.getInputStream()) {
                byte[] buffer=new byte[65536];int n;
                while((n=input.read(buffer))!=-1){if(bytes.size()+n>8*1024*1024)throw new IOException("Distribution index too large");bytes.write(buffer,0,n);}
            }
            JSONObject result=new JSONObject(bytes.toString(StandardCharsets.UTF_8.name()));
            if(result.getInt("schemaVersion")!=1 || !"betterendfield-distribution-index".equals(result.getString("kind")))throw new IOException("Unsupported distribution index");
            cached=result;cachedAt=System.currentTimeMillis();return result;
        }finally{c.disconnect();}
    }
    static void checkCancelled() throws IOException {if(cancelled.get() || Thread.currentThread().isInterrupted())throw new IOException("Cancelled");}
    static JSONObject state(Context context) throws Exception {
        File file=new File(GameResourceStore.root(context),"state.json");return file.isFile()?GameResourceStore.json(file):new JSONObject();
    }
    static void resume(Context context,String type,GameResourceStore.Snapshot current) throws Exception {
        synchronized(GameResourceStore.class) {
            JSONObject active=state(context);active.put("auto."+type+".android",true);
            File root=GameResourceStore.root(context);root.mkdirs();File tmp=new File(root,UUID.randomUUID()+".state.tmp");
            try {
                Files.writeString(tmp.toPath(),active.toString());
                Files.move(tmp.toPath(),new File(root,"state.json").toPath(),StandardCopyOption.REPLACE_EXISTING,StandardCopyOption.ATOMIC_MOVE);
            }finally{tmp.delete();}
        }
    }
    static String update(Context context,boolean preview,boolean resume) throws Exception {
        if(!busy.compareAndSet(false,true))throw new IOException(Text.of("资源更新正在进行"));
        cancelled.set(false);
        try {
            JSONObject index=index();if(index==null)throw new IOException(Text.of("资源发布尚未就绪"));
            String game=context.getPackageManager().getPackageInfo("com.hypergryph.endfield",0).versionName;
            if(game==null || !game.matches("[0-9]+([.][0-9]+){2}"))throw new IOException(Text.of("无法识别游戏版本"));
            List<String> result=new ArrayList<>();
            for(String type:List.of("core","preview")) {
                checkCancelled();GameResourceStore.Snapshot current=GameResourceStore.resolve(context,type);
                if(type.equals("preview") && current==null && !preview)continue;
                String key=type+".android";JSONObject state=state(context);
                if(state.has(key) && !state.optBoolean("auto."+key,false) && !resume){result.add(type+" "+Text.of("已固定版本"));continue;}
                JSONObject best=null;JSONArray candidates=index.getJSONArray("resources");
                for(int i=0;i<candidates.length();i++) {
                    JSONObject e=candidates.getJSONObject(i);
                    if(!type.equals(e.getString("packageType")) || !"android".equals(e.getString("platform")) || !game.equals(e.getString("gameVersion")) || e.optInt("resourceApi")!=1 || !GameResourceStore.version(e.getString("version")))continue;
                    if(best==null || GameResourceStore.compare(e.getString("version"),best.getString("version"))>0)best=e;
                }
                if(best==null){result.add(type+" "+Text.of("暂无兼容资源"));continue;}
                if(current!=null && game.equals(current.manifest().getString("gameVersion")) && GameResourceStore.compare(current.version(),best.getString("version"))>=0){if(resume)resume(context,type,current);result.add(type+" "+current.version());continue;}
                String asset=best.getString("asset"),url=best.getString("url");
                if(!asset.equals(new File(asset).getName()) || !url.equals(ROOT+asset))throw new IOException("Untrusted resource URL");
                long expected=best.getLong("bytes");if(expected<=0)throw new IOException("Invalid download length");
                File downloads=new File(GameResourceStore.root(context),"downloads");downloads.mkdirs();File file=new File(downloads,UUID.randomUUID()+".part");
                HttpURLConnection connection=connect(url);
                try {
                    if(connection.getResponseCode()!=200)throw new IOException("HTTP "+connection.getResponseCode());
                    try(InputStream input=connection.getInputStream();FileOutputStream output=new FileOutputStream(file)) {
                        byte[] buffer=new byte[65536];long total=0;int n;
                        while((n=input.read(buffer))!=-1){checkCancelled();total+=n;if(total>expected)throw new IOException("Download exceeds index size");output.write(buffer,0,n);}
                        if(total!=expected)throw new IOException("Incomplete resource download");output.getFD().sync();
                    }
                    checkCancelled();
                    // Manual imports and automatic activation share the same store lock.
                    synchronized(GameResourceStore.class) {
                        JSONObject now=state(context);
                        if(now.has(key) && !now.optBoolean("auto."+key,false) && !resume)throw new IOException(Text.of("已固定版本"));
                        try(InputStream input=new FilterInputStream(new FileInputStream(file)) {
                            @Override public int read(byte[] b,int off,int len)throws IOException{checkCancelled();return super.read(b,off,len);}
                        }) {result.add(GameResourceStore.install(context,input,true,best));}
                    }
                }finally{connection.disconnect();file.delete();}
            }
            return String.join("; ",result);
        }finally{busy.set(false);}
    }
}
