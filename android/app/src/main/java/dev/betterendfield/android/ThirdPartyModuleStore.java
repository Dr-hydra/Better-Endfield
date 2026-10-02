package dev.betterendfield.android;

import android.content.Context;
import android.content.SharedPreferences;
import android.net.Uri;
import org.json.*;
import java.io.*;
import java.net.*;
import java.nio.charset.StandardCharsets;
import java.security.SecureRandom;
import java.util.*;

final class ThirdPartyModuleStore {
    static final String KEY="third_party_runtime";
    static File root(Context context) {return new File(context.getFilesDir(),"third-party");}
    private static JSONObject fresh() throws Exception {
        int port;try(ServerSocket socket=new ServerSocket(0,1,InetAddress.getByName("127.0.0.1"))) {port=socket.getLocalPort();}
        byte[] secret=new byte[32];new SecureRandom().nextBytes(secret);StringBuilder token=new StringBuilder();
        for(byte b:secret) token.append(String.format(Locale.ROOT,"%02x",b&255));
        return new JSONObject().put("schema",1).put("port",port).put("token",token.toString()).put("modules",new JSONArray());
    }
    static synchronized JSONObject index(Context context) throws Exception {
        String source=FrameworkSettings.open(context).getString(KEY,"");
        if(source.isEmpty()) {JSONObject value=fresh();commit(context,"",value);return value;}
        if(source.getBytes(StandardCharsets.UTF_8).length>1024*1024) throw new IOException("模块索引过大");
        JSONObject index=new JSONObject(source);
        if(index.getInt("schema")!=1 || index.getInt("port")<1024 || index.getInt("port")>65535 || !index.getString("token").matches("[a-f0-9]{64}"))
            throw new IOException("模块索引不合法");
        JSONArray modules=index.getJSONArray("modules");HashSet<String> ids=new HashSet<>();
        for(int i=0;i<modules.length();++i) {
            JSONObject entry=modules.getJSONObject(i);String id=entry.getString("id"),generation=entry.getString("generation");
            ThirdPartyModulePackage.id(id,true);
            if(!ids.add(id.toLowerCase(Locale.ROOT)) || !generation.matches("[a-f0-9-]{36}") ||
                    !entry.getString("android_remote").equals("tpm-"+generation+".zip") || entry.getLong("archive_bytes")<=0 ||
                    entry.getLong("archive_bytes")>ThirdPartyModulePackage.LIMIT || !(entry.get("enabled") instanceof Boolean)) throw new IOException("模块记录不合法");
            entry.getJSONObject("configuration");
            if(!new File(entry.getString("directory")).getCanonicalFile().equals(new File(new File(root(context),"packages"),generation).getCanonicalFile()))
                throw new IOException("模块目录不属于本机索引");
        }
        return index;
    }
    private static void durable(Context context,String value) throws Exception {
        File root=root(context);if(!root.isDirectory() && !root.mkdirs()) throw new IOException("无法创建模块配置目录");
        android.util.AtomicFile file=new android.util.AtomicFile(new File(root,"index.json"));FileOutputStream output=null;
        try {output=file.startWrite();output.write(value.getBytes(StandardCharsets.UTF_8));file.finishWrite(output);}
        catch(Exception error) {if(output!=null)file.failWrite(output);throw error;}
    }
    private static void commit(Context context,String previous,JSONObject next) throws Exception {
        String value=next.toString();if(value.getBytes(StandardCharsets.UTF_8).length>1024*1024) throw new IOException("模块配置超过 1 MiB");
        durable(context,value);SharedPreferences preferences=FrameworkSettings.open(context);
        if(!preferences.edit().putString(KEY,value).commit()) {
            durable(context,previous);preferences.edit().putString(KEY,previous).commit();throw new IOException("模块配置保存失败");
        }
    }
    static JSONObject find(JSONObject index,String id) throws Exception {
        JSONArray modules=index.getJSONArray("modules");
        for(int i=0;i<modules.length();++i)if(id.equals(modules.getJSONObject(i).getString("id")))return modules.getJSONObject(i);
        throw new IOException("模块已移除，请重新打开");
    }
    static JSONObject manifest(JSONObject entry) throws Exception {return ThirdPartyModulePackage.manifest(new File(entry.getString("directory")));}
    static synchronized void saveConfig(Context context,String id,JSONObject configuration) throws Exception {
        JSONObject index=index(context);String previous=index.toString();find(index,id).put("configuration",new JSONObject(configuration.toString()));commit(context,previous,index);
    }
    static synchronized void enabled(Context context,String id,boolean enabled) throws Exception {
        JSONObject index=index(context);String previous=index.toString();JSONObject record=find(index,id);
        if(enabled && !ThirdPartyModulePackage.supported(manifest(record)))throw new IOException("此包没有 Android 模块或网页入口");
        record.put("enabled",enabled);commit(context,previous,index);
    }
    static synchronized void move(Context context,String id,int direction) throws Exception {
        JSONObject index=index(context);String previous=index.toString();JSONArray records=index.getJSONArray("modules");int from=-1;
        for(int i=0;i<records.length();++i)if(records.getJSONObject(i).getString("id").equals(id))from=i;
        int to=from+direction;if(from<0 || to<0 || to>=records.length())return;
        Object entry=records.remove(from);JSONArray next=new JSONArray();
        for(int i=0;i<=records.length();++i) {if(i==to)next.put(entry);if(i<records.length())next.put(records.get(i));}
        index.put("modules",next);commit(context,previous,index);
    }
    static synchronized void remove(Context context,String id) throws Exception {
        JSONObject index=index(context);String previous=index.toString();JSONArray records=index.getJSONArray("modules");JSONObject entry=find(index,id);
        JSONArray retired=index.optJSONArray("retired_directories");if(retired==null){retired=new JSONArray();index.put("retired_directories",retired);}retired.put(entry.getString("directory"));
        for(int i=0;i<records.length();++i)if(records.getJSONObject(i).getString("id").equals(id)){records.remove(i);break;}
        commit(context,previous,index); // Keep immutable generations/remote ZIPs while a game can still read them.
    }
    static void deleteOwned(File path) {File[] children=path.listFiles();if(children!=null)for(File child:children)deleteOwned(child);path.delete();}
    static JSONObject importArchive(Context context,Uri uri) throws Exception {
        BemImportRequest.requireContentUri(uri);String generation=UUID.randomUUID().toString();
        File archives=new File(root(context),"archives");if(!archives.isDirectory() && !archives.mkdirs())throw new IOException("无法创建 ZIP 目录");
        File archive=new File(archives,generation+".zip"),directory=new File(new File(root(context),"packages"),generation);boolean published=false;
        try {
            try(InputStream input=context.getContentResolver().openInputStream(uri);FileOutputStream output=new FileOutputStream(archive)) {
                if(input==null)throw new IOException("无法读取模块 ZIP");byte[] buffer=new byte[65536];long bytes=0;int count;
                while((count=input.read(buffer))!=-1){bytes+=count;if(bytes>ThirdPartyModulePackage.LIMIT)throw new IOException("模块 ZIP 超过 256 MiB");output.write(buffer,0,count);}output.getFD().sync();
            }
            JSONObject manifest=ThirdPartyModulePackage.extract(archive,directory);String id=manifest.getString("id");
            String remote="tpm-"+generation+".zip";FrameworkSettings.awaitThirdPartyConnection();FrameworkSettings.publishThirdParty(archive,remote);
            synchronized(ThirdPartyModuleStore.class) {
                JSONObject index=index(context);String previous=index.toString();JSONArray modules=index.getJSONArray("modules");JSONObject old=null;int position=modules.length();
                for(int i=0;i<modules.length();++i)if(modules.getJSONObject(i).getString("id").equalsIgnoreCase(id)){old=modules.getJSONObject(i);position=i;break;}
                if(old!=null && !old.getString("id").equals(id))throw new IOException("模块 ID 仅大小写不同，无法更新");
                JSONObject result=new JSONObject().put("id",id).put("generation",generation).put("directory",directory.getCanonicalPath()).put("android_remote",remote)
                    .put("archive_bytes",archive.length()).put("enabled",old!=null && old.getBoolean("enabled") && ThirdPartyModulePackage.supported(manifest))
                    .put("configuration",old!=null?new JSONObject(old.getJSONObject("configuration").toString()):new JSONObject(manifest.optJSONObject("default_configuration")==null?"{}":manifest.getJSONObject("default_configuration").toString()));
                if(old!=null) {JSONArray retired=index.optJSONArray("retired_directories");if(retired==null){retired=new JSONArray();index.put("retired_directories",retired);}retired.put(old.getString("directory"));modules.put(position,result);}else modules.put(result);
                commit(context,previous,index);published=true;return result;
            }
        } finally {if(!published){deleteOwned(directory);archive.delete();FrameworkSettings.removeThirdParty("tpm-"+generation+".zip");}}
    }
    static JSONObject runtime(Context context,String operation,String id,Object payload,String requestId) throws Exception {
        JSONObject index=index(context);find(index,id);
        HttpURLConnection connection=(HttpURLConnection)new URL("http://127.0.0.1:"+index.getInt("port")+"/"+operation).openConnection(Proxy.NO_PROXY);
        connection.setConnectTimeout(1500);connection.setReadTimeout(1500);connection.setRequestProperty("Authorization","Bearer "+index.getString("token"));
        try {
            if(!operation.equals("status")) {
                connection.setRequestMethod("POST");connection.setDoOutput(true);connection.setRequestProperty("Content-Type","application/json");
                JSONObject request=new JSONObject().put("module_id",id);
                if(operation.equals("send"))request.put("request_id",requestId).put("body",payload==null?JSONObject.NULL:payload);
                if(operation.equals("configure"))request.put("configuration",payload);
                byte[] bytes=request.toString().getBytes(StandardCharsets.UTF_8);connection.setFixedLengthStreamingMode(bytes.length);
                try(OutputStream output=connection.getOutputStream()){output.write(bytes);}
            }
            if(connection.getResponseCode()!=200)throw new IOException("游戏模块桥未接受请求");
            try(InputStream input=connection.getInputStream();ByteArrayOutputStream output=new ByteArrayOutputStream()) {
                byte[] buffer=new byte[8192];int count;while((count=input.read(buffer))!=-1){if(output.size()+count>1024*1024)throw new IOException("模块响应过大");output.write(buffer,0,count);}
                JSONObject response=new JSONObject(new String(output.toByteArray(),StandardCharsets.UTF_8));
                if(!operation.equals("status"))return response;
                JSONObject result=new JSONObject().put("connected",response.optBoolean("connected",true));JSONArray modules=response.optJSONArray("modules");
                if(modules!=null)for(int i=0;i<modules.length();++i)if(id.equals(modules.getJSONObject(i).optString("id")))result.put("module",modules.getJSONObject(i));
                return result;
            }
        } finally {connection.disconnect();}
    }
}
