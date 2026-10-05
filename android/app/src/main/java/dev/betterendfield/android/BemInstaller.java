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
    static final String SKIP_VALIDATION = "bem_skip_validation";
    static final String HOT_SWITCH = "bem_hot_switch";
    static final String FAST_LOADING = "bem_fast_loading";
    static final String KEEP_LOCAL_COPIES = "bem_keep_local_copies";
    static volatile String status = "原样导入 BEM 包；贴图异常时，可在对应模型包下手动转换手机纹理。";
    static volatile boolean busy;
    static volatile boolean removing;
    static volatile int progressPercent=-1;
    static volatile long startedAt;
    private static volatile boolean cancelled;
    private static boolean nativeLoaded;
    private static final java.util.concurrent.ExecutorService worker = Executors.newSingleThreadExecutor();
    static native String convertNative(String input, String output, String rules, boolean astc) throws IOException;
    static native String inspectNativeWithOptions(String input, boolean skipValidation) throws IOException;
    static String inspectNative(String input) throws IOException { return inspectNativeWithOptions(input, false); }
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
                String parameters=entry.optString("remembered_parameters",entry.optString("selected_parameters",entry.optString("default_parameters","")));
                try {BemParameters.restore(entry,parameters);}
                catch(IOException stale) {BemParameters.restore(entry,entry.optString("default_parameters",""));}
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
        boolean skipValidation=FrameworkSettings.open(app).getBoolean(SKIP_VALIDATION,false);
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
                File localSource=converting?new File(new File(root,previousGeneration),"installed.bem"):null;
                if(converting && !localSource.isFile()) {
                    if(!FrameworkSettings.isConnected()) status="正在等待框架服务连接…";
                    FrameworkSettings.awaitConnection();
                    status="正在准备纹理转换…";
                }
                try(InputStream in=converting
                        ?(localSource.isFile()?new FileInputStream(localSource):FrameworkSettings.openBem("bem-"+previousGeneration+".bem"))
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
                    try(InputStream in=app.getAssets().open("android-normal-rules.json");
                        ByteArrayOutputStream bytes=new ByteArrayOutputStream()) {
                        byte[] buffer=new byte[8192];int count;
                        while((count=in.read(buffer))!=-1) bytes.write(buffer,0,count);
                        rules=new String(bytes.toByteArray(),StandardCharsets.UTF_8);
                    }
                    result=new JSONObject(convertNative(source.getAbsolutePath(),output.getAbsolutePath(),rules,astc));
                    result.put("texture_mode",astc?"astc":"rgba32");
                    source.delete();
                } else {
                    result=new JSONObject(inspectNativeWithOptions(source.getAbsolutePath(),skipValidation));
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
                progressPercent=-1;
                if(!FrameworkSettings.isConnected()) status="正在等待框架服务连接…";
                FrameworkSettings.awaitConnection();
                checkpoint();status="正在发布给游戏…";
                FrameworkSettings.publishBem(new File(installed,"installed.bem"),result.getString("remote"));
                checkpoint();
                synchronized(BemInstaller.class) {
                    checkpoint();
                    JSONArray previous=index(app),next=installedIndex(previous,result,previousGeneration);
                    commitIndex(app,previous.toString(),next,"安装索引保存失败");
                }
                // The game reads its private copy, so a replaced conversion source is unreachable.
                if(converting) discardGeneration(root,previousGeneration);
                if(!FrameworkSettings.open(app).getBoolean(KEEP_LOCAL_COPIES,true)) new File(installed,"installed.bem").delete();
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
        JSONObject parameterSource=latest;
        if(parameterSource==null) for(int i=entries.length()-1;i>=0;--i) {
            JSONObject candidate=entries.getJSONObject(i);
            if(character.equals(candidate.getString("character_id")) && result.getString("package_id").equals(candidate.getString("package_id"))) {
                parameterSource=candidate;break;
            }
        }
        String savedParameters=parameterSource==null?result.optString("default_parameters",""):
                parameterSource.optString("remembered_parameters",parameterSource.optString("selected_parameters",result.optString("default_parameters","")));
        BemParameters.restore(result,savedParameters);
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
                complete=discardGeneration(new File(app.getFilesDir(),"bem-installed"),generation);
                status="已移除："+removed.getString("name")+"。其他包已保留，重启游戏后生效。"
                        +(complete?"":"部分残留文件未能清理，但该包已停用。");
            } catch(Exception error) {status="模型包已移除；部分文件清理失败："+error.getMessage();}
            finally {removing=false;busy=false;}
        });} catch(RuntimeException rejected) {removing=false;busy=false;throw rejected;}
    }
    private static boolean discardGeneration(File installed,String generation) throws IOException {
        File root=installed.getCanonicalFile(),version=new File(root,generation);
        if(!generation.matches("[a-f0-9-]{36}") || !version.getCanonicalFile().getParentFile().equals(root))
            throw new IOException("无效的安装目录");
        boolean complete=FrameworkSettings.removeBem("bem-"+generation+".bem");
        deleteOwned(version);
        return complete && !version.exists();
    }
    private static java.util.Set<String> referencedGenerations(Context app) throws Exception {
        java.util.Set<String> keep=new java.util.HashSet<>();
        JSONArray entries=index(app);
        for(int i=0;i<entries.length();++i) keep.add(entries.getJSONObject(i).getString("generation"));
        // The durable mirror is consulted too; a generation either copy references is never discarded.
        File durable=new File(app.getFilesDir(),"bem-index.json");
        if(durable.isFile()) {
            JSONArray saved=new JSONArray(new String(new android.util.AtomicFile(durable).readFully(),StandardCharsets.UTF_8));
            for(int i=0;i<saved.length();++i) keep.add(saved.getJSONObject(i).getString("generation"));
        }
        return keep;
    }
    private static long sizeOf(File file) {
        File[] children=file.listFiles();if(children==null) return file.length();
        long total=0;for(File child:children) total+=sizeOf(child);return total;
    }
    private static String megabytes(long bytes) {return String.format(java.util.Locale.ROOT,"%.1f MB",bytes/1048576.0);}
    /** Removes data no installed package references: superseded generations, shared copies and interrupted stages. */
    static synchronized boolean cleanUnused(Context context) {
        if(busy) return false;
        Context app=context.getApplicationContext();
        busy=true;cancelled=false;progressPercent=-1;startedAt=android.os.SystemClock.elapsedRealtime();
        status="正在清理未使用数据…";
        try {worker.execute(()->{
            try {
                FrameworkSettings.awaitConnection();
                java.util.Set<String> keep=referencedGenerations(app);
                long freed=0;
                File root=new File(app.getFilesDir(),"bem-installed");
                File[] children=root.listFiles();
                if(children!=null) for(File child:children) {
                    String name=child.getName();
                    if(name.startsWith("stage-") || name.matches("[a-f0-9-]{36}") && !keep.contains(name)) {
                        freed+=sizeOf(child);deleteOwned(child);
                    }
                }
                for(String remote:FrameworkSettings.listBem()) {
                    if(keep.contains(remote.substring(4,remote.length()-4))) continue;
                    long size=FrameworkSettings.bemSize(remote);
                    if(FrameworkSettings.removeBem(remote)) freed+=Math.max(0,size);
                }
                status="已清理 "+megabytes(freed)+"。游戏目录中未启用的模型会在下次启动游戏时清理。";
            } catch(Exception error) {status="清理未完成："+error.getMessage();}
            finally {busy=false;}
        });} catch(RuntimeException rejected) {busy=false;status="无法启动清理任务，请重试。";throw rejected;}
        return true;
    }
    /** Local copies are optional; the framework copy remains the source the game and conversion read. */
    static synchronized boolean applyLocalCopies(Context context,boolean keepLocal) {
        if(busy) return false;
        Context app=context.getApplicationContext();
        busy=true;cancelled=false;progressPercent=-1;startedAt=android.os.SystemClock.elapsedRealtime();
        status=keepLocal?"正在恢复本地副本…":"正在移除本地副本…";
        try {worker.execute(()->{
            try {
                FrameworkSettings.awaitConnection();
                File root=new File(app.getFilesDir(),"bem-installed");
                JSONArray entries=index(app);
                java.util.List<String> shared=java.util.Arrays.asList(FrameworkSettings.listBem());
                long changed=0;StringBuilder missing=new StringBuilder();
                for(int i=0;i<entries.length();++i) {
                    checkpoint();
                    JSONObject entry=entries.getJSONObject(i);
                    String generation=entry.getString("generation"),remote="bem-"+generation+".bem";
                    long bytes=entry.getLong("bytes");
                    File local=new File(new File(root,generation),"installed.bem");
                    boolean published=shared.contains(remote) && FrameworkSettings.bemSize(remote)==bytes;
                    if(keepLocal) {
                        if(local.isFile() && local.length()==bytes) continue;
                        if(!published) {missing.append(missing.length()>0?"、":"").append(entry.getString("name"));continue;}
                        File folder=local.getParentFile(),temp=new File(folder,"installed.tmp");
                        if(!folder.isDirectory() && !folder.mkdirs()) throw new IOException("无法创建安装目录");
                        try(InputStream in=FrameworkSettings.openBem(remote);FileOutputStream out=new FileOutputStream(temp)) {
                            copy(in,out,bytes);out.getFD().sync();
                        }
                        if(temp.length()!=bytes || !temp.renameTo(local)) {temp.delete();throw new IOException("本地副本写入失败");}
                        changed+=bytes;
                    } else if(local.isFile()) {
                        // Never drop the last copy of a package.
                        if(!published) {missing.append(missing.length()>0?"、":"").append(entry.getString("name"));continue;}
                        if(local.delete()) changed+=bytes;
                    }
                }
                status=(keepLocal?"已恢复本地副本 ":"已移除本地副本 ")+megabytes(changed)+"。"
                        +(missing.length()==0?"":(keepLocal?"框架中缺少文件，需重新导入：":"框架中缺少文件，已保留本地副本：")+missing);
            } catch(Exception error) {status=(keepLocal?"恢复本地副本未完成：":"移除本地副本未完成：")+error.getMessage();}
            finally {busy=false;}
        });} catch(RuntimeException rejected) {busy=false;status="无法启动任务，请重试。";throw rejected;}
        return true;
    }
    static void copy(InputStream in,OutputStream out,long limit) throws IOException {
        byte[] buffer=new byte[65536];long size=0;int count;
        while((count=in.read(buffer))!=-1) {checkpoint();size+=count;if(size>limit) throw new IOException("文件超过大小限制");out.write(buffer,0,count);}
    }
    private static void deleteOwned(File file) {File[] children=file.listFiles();if(children!=null) for(File child:children) deleteOwned(child);file.delete();}
    static synchronized void select(Context app,String generation,String appearance,boolean enabled) throws Exception {
        saveAll(app,new JSONArray().put(new JSONObject().put("generation",generation).put("appearance",appearance).put("enabled",enabled)));
    }
    static synchronized void disableAll(Context app) throws Exception {
        if(busy) throw new IOException("请等待当前操作完成后修改");
        JSONArray entries=index(app),changes=new JSONArray();
        for(int i=0;i<entries.length();++i)
            changes.put(new JSONObject().put("generation",entries.getJSONObject(i).getString("generation")).put("enabled",false));
        saveAll(app,changes);
    }
    static synchronized void saveAll(Context app,JSONArray changes) throws Exception {
        if(busy) throw new IOException("请等待当前操作完成后修改");
        JSONArray previous=index(app),entries=OverlayWritePolicy.apply(previous,changes);
        commitIndex(app,previous.toString(),entries,"保存失败");
    }
}
