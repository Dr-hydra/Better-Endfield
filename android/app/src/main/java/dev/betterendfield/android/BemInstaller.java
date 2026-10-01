package dev.betterendfield.android;

import android.content.Context;
import android.net.Uri;
import org.json.JSONArray;
import org.json.JSONObject;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.UUID;
import java.util.concurrent.Executors;

/** Installation happens in the module app; the injected runtime never encodes textures. */
final class BemInstaller {
    static final String INDEX = "installed_bem_packages";
    static volatile String status = "原样导入 BEM 包；贴图异常时，可在对应模型包下手动转换手机纹理。";
    static volatile boolean busy;
    static volatile boolean removing;
    static volatile int progressPercent=-1;
    static volatile long startedAt;
    private static volatile boolean cancelled;
    private static boolean nativeLoaded;
    private static final java.util.concurrent.ExecutorService worker = Executors.newSingleThreadExecutor();
    static native String convertNative(String input, String output, String rules, boolean astc) throws IOException;
    static native String inspectNative(String input) throws IOException;
    static native void cancelNative();
    // Called on the native conversion worker, consumed by the management UI.
    static void conversionProgress(String texture,int current,int total,int mip,int mips,float percent) {
        if(!cancelled) {
            progressPercent=Math.max(0,Math.min(100,Math.round(percent)));
            status="正在转换第 "+current+" 张纹理 · mip "+mip+" / "+mips+"\n"+texture;
        }
    }
    static synchronized void loadCodec() { if (!nativeLoaded) { System.loadLibrary("betterendfield_installer"); nativeLoaded=true; } }
    static synchronized void cancel() { if(removing || !busy) return; cancelled=true; if(nativeLoaded) cancelNative(); status="正在取消，保留原安装版本…"; }
    static void checkpoint() throws IOException { if(cancelled) throw new IOException("已取消安装"); }
    static synchronized JSONArray index(Context context) {
        try {
            String stored=FrameworkSettings.open(context).getString(INDEX,"[]");
            JSONArray entries=BemOptions.exclusive(new JSONArray(stored));
            // Legacy entries had implicit enabled/default selections. Repair them on disk
            // before the UI can advertise the normalized state.
            for(int i=0;i<entries.length();++i) {
                JSONObject entry=entries.getJSONObject(i);
                if(entry.optInt("bem_minor",0)>=1) {
                    String selection=entry.optString("selected_options",entry.getString("default_options"));
                    try {selection=BemOptions.encode(BemOptions.parse(entry,selection));}
                    catch(Exception stale) {selection=BemOptions.encode(BemOptions.parse(entry,entry.getString("default_options")));}
                    entry.put("selected_options",selection);
                } else {
                    String selection=entry.optString("selected_appearance",entry.getString("default_appearance"));
                    try {BemOptions.appearance(entry,selection);}
                    catch(Exception stale) {selection=BemOptions.appearance(entry,entry.getString("default_appearance"));}
                    entry.put("selected_appearance",selection);
                }
            }
            if(!entries.toString().equals(stored)) commitIndex(context,stored,entries,"安装索引迁移保存失败");
            return entries;
        }
        catch (Exception e) { throw new IllegalStateException("安装索引损坏",e); }
    }
    /** Returns false when another operation owns the worker; never drops a request silently. */
    static synchronized boolean start(Context context, Uri uri) {
        return run(context, BemImportRequest.requireContentUri(uri), null);
    }
    static synchronized void convert(Context context, String generation) {
        run(context, null, generation);
    }
    private static synchronized boolean run(Context context, Uri uri, String previousGeneration) {
        if(busy) return false;
        boolean converting=previousGeneration!=null;
        progressPercent=-1;startedAt=android.os.SystemClock.elapsedRealtime();
        busy=true; cancelled=false; status=converting?"正在准备纹理转换…":"正在读取并校验包（不转换纹理）…";
        Context app=context.getApplicationContext();
        try {
        worker.execute(() -> {
            File stage=null;
            try {
                File root=new File(app.getFilesDir(),"bem-installed");
                if(!root.isDirectory() && !root.mkdirs()) throw new IOException("无法创建安装目录");
                // Every transaction gets its own directory. A process death cannot replace the active generation.
                stage=new File(root,"stage-"+UUID.randomUUID());
                if(!stage.mkdir()) throw new IOException("无法创建临时目录");
                File output=new File(stage,"installed.bem");
                File source=converting?new File(stage,"source.bem"):output;
                JSONObject previousEntry=null;
                if(converting) {
                    if(!previousGeneration.matches("[a-f0-9-]{36}")) throw new IOException("无效的模型包版本");
                    previousEntry=findEntry(index(app),previousGeneration);
                    if(previousEntry==null) throw new IOException("模型包已被替换，请刷新后重试");
                }
                try(InputStream in=converting
                        ?new FileInputStream(new File(new File(root,previousGeneration),"installed.bem"))
                        :app.getContentResolver().openInputStream(uri); FileOutputStream out=new FileOutputStream(source)) {
                    if(in==null) throw new IOException("无法打开包");
                    if(converting) copy(in,out,BemImportStream.MAX_BYTES);
                    else BemImportStream.copy(in,out,BemImportStream.MAX_BYTES,BemInstaller::checkpoint);
                    out.getFD().sync();
                }
                loadCodec();checkpoint();
                JSONObject result;
                if(converting) {
                    boolean astc=AstcSupport.available();
                    status=astc?"正在转换 ASTC 纹理；大包可能需要数分钟…":"设备未报告 ASTC，正在生成 RGBA32 资源…";
                    String rules;
                    try(InputStream in=app.getAssets().open("android-normal-rules.json")) {rules=new String(in.readAllBytes(),StandardCharsets.UTF_8);}
                    result=new JSONObject(convertNative(source.getAbsolutePath(),output.getAbsolutePath(),rules,astc));
                    result.put("texture_mode",astc?"astc":"rgba32");
                    source.delete();
                } else {
                    result=new JSONObject(inspectNative(source.getAbsolutePath()));
                    result.put("texture_mode","original");
                }
                checkpoint();
                try(FileOutputStream out=new FileOutputStream(new File(stage,"report.json"))) {out.write(result.toString(2).getBytes(StandardCharsets.UTF_8));out.getFD().sync();}
                String generation=UUID.randomUUID().toString();
                File installed=new File(root,generation);
                if(!stage.renameTo(installed)) throw new IOException("安装结果发布失败");
                stage=installed;
                result.put("generation",generation).put("remote","bem-"+generation+".bem").put("enabled",true);
                // Validate all preserved selections before publishing a payload.
                installedIndex(index(app),result,previousGeneration);
                progressPercent=-1;status="正在发布给游戏…";
                if(!FrameworkSettings.publishBem(new File(installed,"installed.bem"),result.getString("remote")))
                    throw new IOException("框架服务未连接；请启用 modern 模块后重试");
                checkpoint();
                synchronized(BemInstaller.class) {
                    checkpoint();
                    JSONArray previous=index(app),next=installedIndex(previous,result,previousGeneration);
                    commitIndex(app,previous.toString(),next,"安装索引保存失败");
                }
                // Old generations remain until explicit removal; a running game can still be reading them.
                stage=null;status=(converting?"已转换：":"已原样导入：")+result.getString("name")+"。重启游戏后生效。";
            } catch(Throwable error) {
                String reason=error instanceof SecurityException
                        ?"没有读取权限或临时授权已失效，请从文件管理器重新打开，或使用应用内文件选择器。"
                        :error.getMessage();
                if(reason==null || reason.isEmpty()) reason=error.getClass().getSimpleName();
                if(reason!=null && reason.contains("Normal slot has no verified source encoding"))
                    reason="此包缺少已确认的法线贴图编码信息，暂时无法转换；可继续使用原包。";
                status=(converting?"转换未完成，原包和启用状态已保留：":"导入未完成：")+reason;
                android.util.Log.e("BetterEndfield.Install",status,error);
            }
            finally {if(stage!=null) deleteOwned(stage);busy=false;}
        });
        return true;
        } catch(RuntimeException rejected) {
            busy=false;
            status="无法启动导入任务，请重试。";
            throw rejected;
        }
    }
    private static JSONObject findEntry(JSONArray entries,String generation) throws Exception {
        for(int i=0;i<entries.length();++i) {
            JSONObject entry=entries.getJSONObject(i);
            if(generation.equals(entry.getString("generation"))) return entry;
        }
        return null;
    }
    /** Preserve immutable package identities; conversion replaces only its target. */
    static JSONArray installedIndex(JSONArray previous,JSONObject report,String previousGeneration) throws Exception {
        JSONArray entries=BemOptions.exclusive(previous),next=new JSONArray();
        JSONObject result=new JSONObject(report.toString());
        JSONObject latest=previousGeneration==null?null:findEntry(entries,previousGeneration);
        if(previousGeneration!=null && latest==null) throw new IOException("模型包已被替换，原配置保持不变");
        String character=result.getString("character_id");
        if(latest!=null && !character.equals(latest.getString("character_id"))) throw new IOException("转换结果的角色不匹配");
        if(findEntry(entries,result.getString("generation"))!=null) throw new IOException("模型包版本重复");
        result.put("enabled",latest==null || latest.optBoolean("enabled",true));
        if(result.optInt("bem_minor",0)>=1) {
            String saved=latest==null?result.getString("default_options"):latest.optString("selected_options",result.getString("default_options"));
            // Conversion may not silently discard a user's saved choice.
            result.put("selected_options",BemOptions.encode(BemOptions.parse(result,saved)));
        } else {
            String saved=latest==null?result.getString("default_appearance"):latest.optString("selected_appearance",latest.getString("default_appearance"));
            result.put("selected_appearance",BemOptions.appearance(result,saved));
        }
        for(int i=0;i<entries.length();++i) {
            JSONObject old=entries.getJSONObject(i);
            if(previousGeneration!=null && previousGeneration.equals(old.getString("generation"))) continue;
            if(result.getBoolean("enabled") && character.equals(old.getString("character_id"))) old.put("enabled",false);
            next.put(old);
        }
        next.put(result);
        return next;
    }
    private static void commitIndex(Context app,String previous,JSONArray next,String message) throws IOException {
        android.content.SharedPreferences prefs=FrameworkSettings.open(app);
        String value=next.toString();
        // SharedPreferences listeners can run before commit() reports disk failure.
        // Keep a verified durable snapshot first, so the framework listener never
        // publishes an index that exists only in an optimistic in-memory map.
        durableIndex(app,value);
        boolean committed=prefs.edit().putString(INDEX,value).commit();
        if(!committed || !value.equals(prefs.getString(INDEX,"[]"))) {
            // Android commit() may update the in-memory map even when disk I/O fails.
            // Never let the next UI refresh mistake that map for a saved change.
            try {durableIndex(app,previous);}
            finally {prefs.edit().putString(INDEX,previous).commit();}
            throw new IOException(committed?"安装索引校验失败":message);
        }
    }
    private static void durableIndex(Context app,String value) throws IOException {
        android.util.AtomicFile file=new android.util.AtomicFile(new File(app.getFilesDir(),"bem-index.json"));
        FileOutputStream stream=null;
        try {
            stream=file.startWrite();
            stream.write(value.getBytes(StandardCharsets.UTF_8));stream.getFD().sync();
            file.finishWrite(stream);stream=null;
            if(!value.equals(new String(file.readFully(),StandardCharsets.UTF_8))) throw new IOException("安装索引磁盘校验失败");
        } catch(IOException error) {if(stream!=null) file.failWrite(stream);throw error;}
    }
    static synchronized void remove(Context context,String generation) throws Exception {
        if(busy) throw new IOException("请等待当前操作完成，或取消后再移除");
        if(!generation.matches("[a-f0-9-]{36}")) throw new IOException("无效的模型包版本");
        Context app=context.getApplicationContext();
        JSONArray previous=index(app),next=new JSONArray();
        JSONObject removed=findEntry(previous,generation);
        if(removed==null) throw new IOException("模型包已被移除或更新，请刷新后重试");
        for(int i=0;i<previous.length();++i) {
            JSONObject entry=previous.getJSONObject(i);
            if(!generation.equals(entry.getString("generation"))) next.put(entry);
        }
        // Stop advertising the package before touching its immutable files.
        commitIndex(app,previous.toString(),next,"移除配置保存失败");
        busy=true;removing=true;cancelled=false;progressPercent=-1;startedAt=android.os.SystemClock.elapsedRealtime();
        status="已从列表移除，正在清理安装文件…";
        try {worker.execute(()->{
            boolean complete=true;
            try {
                File root=new File(app.getFilesDir(),"bem-installed").getCanonicalFile();
                File version=new File(root,generation);
                if(!version.getCanonicalFile().getParentFile().equals(root)) throw new IOException("无效的安装目录");
                complete &= FrameworkSettings.removeBem("bem-"+version.getName()+".bem");
                deleteOwned(version);complete &= !version.exists();
                status="已移除："+removed.getString("name")+"。其他包已保留，重启游戏后生效。"
                        +(complete?"":"部分残留文件未能清理，但该包已停用。");
            } catch(Exception error) {status="模型包已移除；部分文件清理失败："+error.getMessage();}
            finally {removing=false;busy=false;}
        });} catch(RuntimeException rejected) {removing=false;busy=false;throw rejected;}
    }
    static void copy(InputStream in,OutputStream out,long limit) throws IOException {
        byte[] buffer=new byte[65536];long size=0;int count;
        while((count=in.read(buffer))!=-1) {checkpoint();size+=count;if(size>limit) throw new IOException("文件超过大小限制");out.write(buffer,0,count);}
    }
    private static void deleteOwned(File file) {File[] children=file.listFiles();if(children!=null) for(File child:children) deleteOwned(child);file.delete();}
    static synchronized void select(Context app,String generation,String appearance,boolean enabled) throws Exception {
        saveAll(app,new JSONArray().put(new JSONObject().put("generation",generation).put("appearance",appearance).put("enabled",enabled)));
    }
    static synchronized void saveAll(Context app,JSONArray changes) throws Exception {
        if(busy) throw new IOException("请等待当前操作完成后修改");
        JSONArray previous=index(app),entries=new JSONArray(previous.toString());
        for(int i=0;i<changes.length();++i) {
            JSONObject change=changes.getJSONObject(i);
            JSONObject entry=findEntry(entries,change.getString("generation"));
            if(entry==null) throw new IOException("模型包列表已更新，请重新选择");
            if((entry.optInt("bem_minor",0)>=1 && change.has("appearance")) ||
                    (entry.optInt("bem_minor",0)<1 && change.has("options"))) throw new IOException("选项类型与模型包不匹配");
            if(entry.optInt("bem_minor",0)>=1 && change.has("options")) {
                String options=BemOptions.encode(BemOptions.parse(entry,change.getString("options")));
                entry.put("selected_options",options);
            } else if(entry.optInt("bem_minor",0)<1 && change.has("appearance")) {
                entry.put("selected_appearance",BemOptions.appearance(entry,change.getString("appearance")));
            }
            if(change.has("enabled")) {
                boolean enabled=change.getBoolean("enabled");
                if(enabled) for(int j=0;j<entries.length();++j) {
                    JSONObject other=entries.getJSONObject(j);
                    if(entry.getString("character_id").equals(other.getString("character_id"))) other.put("enabled",false);
                }
                entry.put("enabled",enabled);
            }
        }
        commitIndex(app,previous.toString(),entries,"保存失败");
    }
}
