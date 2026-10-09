package dev.betterendfield.next;

import android.content.*;
import android.net.Uri;
import org.json.*;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.util.*;
import java.util.zip.*;
import com.sun.net.httpserver.HttpServer;

public final class ThirdPartyAndroidTest {
    private static int checks;
    private static void check(boolean value,String message){checks++;if(!value)throw new AssertionError(message);}
    private interface Operation {void run() throws Exception;}
    private static void rejects(Operation operation,String message)throws Exception{try{operation.run();}catch(IOException expected){checks++;return;}throw new AssertionError(message);}
    private static final class Preferences implements SharedPreferences {
        String value="";boolean failNext;
        public String getString(String key,String fallback){return value.isEmpty()?fallback:value;}
        public Editor edit(){return new Editor(){String pending;public Editor putString(String key,String text){pending=text;return this;}public boolean commit(){value=pending;boolean success=!failNext;failNext=false;return success;}};}
    }
    private static final class App extends Context {
        final File root;final Preferences preferences=new Preferences();App()throws Exception{root=Files.createTempDirectory("third-party-android-").toFile();}
        public File getFilesDir(){return root;}public Context getApplicationContext(){return this;}public SharedPreferences getSharedPreferences(String name,int mode){return preferences;}
        public ContentResolver getContentResolver(){return new ContentResolver(){public InputStream openInputStream(Uri uri)throws IOException{return new FileInputStream(uri.source);}};}
    }
    static JSONObject manifest(String id)throws Exception{return new JSONObject().put("format",1).put("abi",1).put("id",id).put("name","Example").put("author","Creator").put("version","1")
        .put("libraries",new JSONObject()).put("ui","ui/index.html").put("default_configuration",new JSONObject().put("label","default"));}
    static File zip(App app,JSONObject manifest,String... extras)throws Exception {
        File file=new File(app.root,UUID.randomUUID()+".zip");try(ZipOutputStream out=new ZipOutputStream(new FileOutputStream(file))) {
            out.putNextEntry(new ZipEntry("module.json"));out.write(manifest.toString().getBytes(StandardCharsets.UTF_8));out.closeEntry();
            out.putNextEntry(new ZipEntry("ui/index.html"));out.write("<!doctype html><html>Creator UI</html>".getBytes(StandardCharsets.UTF_8));out.closeEntry();
            for(String path:extras){out.putNextEntry(new ZipEntry(path));out.write(new byte[]{1,2,3});out.closeEntry();}
        }return file;
    }
    public static void main(String[] args)throws Exception {
        App app=new App();JSONObject imported=ThirdPartyModuleStore.importArchive(app,new Uri(zip(app,manifest("example.ui"))));String id=imported.getString("id");
        check(!imported.getBoolean("enabled")&&ThirdPartyModulePackage.supported(ThirdPartyModuleStore.manifest(imported)),"UI-only import/default disabled failed");
        check(FrameworkSettings.lastThirdPartyRemote.equals(imported.getString("android_remote")),"Immutable ZIP was not published");
        check(Arrays.equals(FrameworkSettings.lastThirdPartyBytes,Files.readAllBytes(new File(new File(ThirdPartyModuleStore.root(app),"archives"),imported.getString("generation")+".zip").toPath())),"Framework publication altered ZIP bytes");
        ThirdPartyModuleStore.enabled(app,id,true);JSONObject config=new JSONObject("{\"label\":\"changed\",\"future\":[1,{\"nested\":true}],\"float\":0.25}");ThirdPartyModuleStore.saveConfig(app,id,config);
        JSONObject nextManifest=manifest(id).put("version","2");JSONObject upgraded=ThirdPartyModuleStore.importArchive(app,new Uri(zip(app,nextManifest)));
        check(upgraded.getBoolean("enabled")&&upgraded.getJSONObject("configuration").toString().equals(config.toString()),"Upgrade discarded opaque config/enabled state");
        check(!upgraded.getString("directory").equals(imported.getString("directory"))&&new File(imported.getString("directory")).isDirectory(),"Update overwrote loaded generation");
        check(ThirdPartyModuleStore.index(app).getJSONArray("retired_directories").length()==1,"Update did not track old generation");
        String previous=app.preferences.value;
        for(String bad:new String[]{"../escape","ui/../escape","ui\\escape","C:/escape","/absolute","UI/INDEX.HTML","MODULE.JSON","native/CON.so"})
            rejects(()->ThirdPartyModuleStore.importArchive(app,new Uri(zip(app,manifest(id),bad))),"Accepted bad ZIP entry "+bad);
        check(previous.equals(app.preferences.value),"Failed imports changed installed index");
        rejects(()->ThirdPartyModuleStore.importArchive(app,new Uri(zip(app,manifest("betterendfieldnext.camera")))),"Overwrote built-in module ID");
        rejects(()->ThirdPartyModuleStore.importArchive(app,new Uri(zip(app,manifest("voice.character")))),"Accepted builtin voice module ID");
        rejects(()->ThirdPartyModuleStore.importArchive(app,new Uri(zip(app,manifest("VOICE.Character")))),"Accepted builtin voice module ID case alias");
        for(String dependency:new String[]{"voice.character","betterendfieldnext.camera",id})
            rejects(()->ThirdPartyModuleStore.importArchive(app,new Uri(zip(app,manifest(id).put("dependencies",new JSONArray().put(dependency))))),"Accepted invalid dependency "+dependency);
        JSONArray excessive=new JSONArray();for(int i=0;i<129;i++)excessive.put("example.dependency"+i);
        rejects(()->ThirdPartyModuleStore.importArchive(app,new Uri(zip(app,manifest(id).put("dependencies",excessive)))),"Accepted >128 dependencies");
        rejects(()->ThirdPartyModuleStore.importArchive(app,new Uri(zip(app,manifest(id).put("abi",2)))),"Accepted unsupported ABI");
        rejects(()->ThirdPartyModuleStore.importArchive(app,new Uri(zip(app,manifest(id).put("ui","ui/missing.html")))),"Accepted missing UI entry");
        rejects(()->ThirdPartyModuleStore.importArchive(app,new Uri(zip(app,manifest(id).put("default_configuration",new JSONArray())))),"Accepted array default config");
        JSONObject foreign=manifest("example.windows");foreign.remove("ui");foreign.put("libraries",new JSONObject().put("windows-x64","native/example.dll"));
        JSONObject other=ThirdPartyModuleStore.importArchive(app,new Uri(zip(app,foreign,"native/example.dll")));
        check(!ThirdPartyModulePackage.supported(ThirdPartyModuleStore.manifest(other))&&!other.getBoolean("enabled"),"Enabled incompatible native platform");
        rejects(()->ThirdPartyModuleStore.enabled(app,other.getString("id"),true),"Enabled foreign module");
        ThirdPartyModuleStore.move(app,other.getString("id"),-1);check(ThirdPartyModuleStore.index(app).getJSONArray("modules").getJSONObject(0).getString("id").equals(other.getString("id")),"Reorder failed");
        ThirdPartyModuleStore.remove(app,other.getString("id"));check(ThirdPartyModuleStore.index(app).getJSONArray("modules").length()==1&&new File(other.getString("directory")).isDirectory(),"Removal destroyed possibly loaded files");
        previous=app.preferences.value;app.preferences.failNext=true;rejects(()->ThirdPartyModuleStore.saveConfig(app,id,new JSONObject().put("bad","commit")),"Failed preference commit advertised success");
        check(app.preferences.value.equals(previous)&&Files.readString(new File(ThirdPartyModuleStore.root(app),"index.json").toPath()).equals(previous),"Failed commit did not roll back durable snapshot");
        JSONObject index=ThirdPartyModuleStore.index(app);String token=index.getString("token");
        HttpServer server=HttpServer.create(new java.net.InetSocketAddress("127.0.0.1",index.getInt("port")),0);
        server.createContext("/send",exchange->{try {
            check(exchange.getRequestHeaders().getFirst("Authorization").equals("Bearer "+token),"Missing runtime authorization");
            JSONObject body=new JSONObject(new String(exchange.getRequestBody().readAllBytes(),StandardCharsets.UTF_8));
            check(body.getString("module_id").equals(id)&&body.getString("request_id").equals("test.request"),"Runtime request lost bound module identity");
            check(body.getJSONArray("body").length()==2,"Opaque array message changed");byte[] result="{\"accepted\":true}".getBytes(StandardCharsets.UTF_8);
            exchange.sendResponseHeaders(200,result.length);exchange.getResponseBody().write(result);exchange.close();
        }catch(Exception error){throw new RuntimeException(error);}});server.start();
        try{check(ThirdPartyModuleStore.runtime(app,"send",id,new JSONArray().put(1).put("raw"),"test.request").getBoolean("accepted"),"Runtime response failed");}finally{server.stop(0);}
        if(args.length>0) {
            App sdkApp=new App();JSONObject sample=ThirdPartyModuleStore.importArchive(sdkApp,new Uri(new File(args[0])));JSONObject sampleManifest=ThirdPartyModuleStore.manifest(sample);
            check(sample.getString("id").equals("example.echo")&&!sample.getBoolean("enabled")&&ThirdPartyModulePackage.supported(sampleManifest),"SDK Echo not importable/default disabled");
            check(sampleManifest.getJSONObject("libraries").has("windows-x64")&&sampleManifest.getJSONObject("libraries").has("android-arm64"),"SDK Echo is not dual-platform");
            check(new File(sample.getString("directory"),sampleManifest.getString("ui")).isFile(),"SDK Echo UI missing after import");
        }
        System.out.println("PASS "+checks+" Android third-party ZIP, publication, immutable updates, configuration, rollback and HTTP checks");
    }
}
