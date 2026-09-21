package dev.betterendfield.android;

import android.app.Instrumentation;
import org.json.*;
import java.io.*;
import java.nio.file.Files;

public final class BemInstallerTest extends Instrumentation {
    private File directory;
    private String method;
    private String realInput;
    @Override public void onCreate(android.os.Bundle arguments) {method=arguments.getString("method","");realInput=arguments.getString("input","installer-real-input.bem");start();}
    @Override public void onStart() {
        android.os.Bundle result=new android.os.Bundle();
        try {
            setUp();
            for(java.lang.reflect.Method test:getClass().getMethods()) if(test.getName().startsWith("test") && (method.isEmpty() || method.equals(test.getName()))) {
                test.invoke(this);android.util.Log.i("BetterEndfield.InstallTest","PASS "+test.getName());
            }
            result.putString("stream","PASS BEM installation tests\n");finish(-1,result);
        } catch(Throwable error) {result.putString("stream","FAIL "+android.util.Log.getStackTraceString(error));finish(0,result);}
    }
    private Instrumentation getInstrumentation(){return this;}
    private static void assertTrue(boolean value){if(!value) throw new AssertionError("Expected true");}
    private static void assertFalse(boolean value){assertTrue(!value);}
    private static void assertEquals(int a,int b){if(a!=b) throw new AssertionError(a+" != "+b);}
    private static void fail(String text){throw new AssertionError(text);}
    private void setUp() throws Exception {
        directory=new File(getInstrumentation().getTargetContext().getCacheDir(),"installer-tests");directory.mkdirs();
        BemInstaller.loadCodec();
    }
    private File fixture() throws Exception {
        File file=new File(directory,"input.bem");
        try(InputStream in=getInstrumentation().getContext().getAssets().open("installer-fixture.bem");FileOutputStream out=new FileOutputStream(file)){in.transferTo(out);}
        return file;
    }
    public void testAstcConversionAndOverwriteRejection() throws Exception {
        File input=fixture(),output=new File(directory,"converted.bem");output.delete();
        JSONObject report=new JSONObject(BemInstaller.convertNative(input.getPath(),output.getPath(),"{}",true));
        assertEquals(100,BemInstaller.progressPercent);
        assertTrue(BemInstaller.status.contains("mip"));
        assertEquals(48,report.getJSONArray("textures").getJSONObject(0).getInt("format"));
        assertEquals(50,report.getJSONArray("textures").getJSONObject(1).getInt("format"));
        assertEquals(2,report.getJSONArray("appearances").length());
        byte[] saved=Files.readAllBytes(output.toPath());
        try {BemInstaller.convertNative(input.getPath(),output.getPath(),"{}",true);fail("Overwrote output");}catch(IOException expected){}
        assertTrue(java.util.Arrays.equals(saved,Files.readAllBytes(output.toPath())));
    }
    public void testOriginalImportInspection() throws Exception {
        File input=fixture();
        byte[] original=Files.readAllBytes(input.toPath());
        JSONObject report=new JSONObject(BemInstaller.inspectNative(input.getPath()));
        assertEquals(2,report.getJSONArray("appearances").length());
        assertTrue(report.getLong("bytes")==original.length);
        assertTrue(java.util.Arrays.equals(original,Files.readAllBytes(input.toPath())));
        File bad=new File(directory,"inspect-bad.bem");
        Files.write(bad.toPath(),new byte[40]);
        try {BemInstaller.inspectNative(bad.getPath());fail("Accepted corrupt import");}catch(IOException expected){}
    }
    public void testRgbaFallbackAndCorruptPackage() throws Exception {
        File input=fixture(),output=new File(directory,"fallback.bem");output.delete();
        JSONObject report=new JSONObject(BemInstaller.convertNative(input.getPath(),output.getPath(),"{}",false));
        assertEquals(4,report.getJSONArray("textures").getJSONObject(0).getInt("format"));
        File bad=new File(directory,"bad.bem");try(FileOutputStream out=new FileOutputStream(bad)){out.write(new byte[40]);}
        File rejected=new File(directory,"rejected.bem");rejected.delete();
        try {BemInstaller.convertNative(bad.getPath(),rejected.getPath(),"{}",true);fail("Accepted bad header");}catch(IOException expected){}
        assertFalse(rejected.exists());assertFalse(new File(rejected+".payloads").exists());
    }
    public void testGamePrivateMaterialization() throws Exception {
        File input=fixture();String generation="00000000-0000-0000-0000-000000000001";
        JSONObject entry=new JSONObject().put("generation",generation).put("remote","bem-"+generation+".bem")
            .put("bytes",input.length()).put("package_id","test.package").put("default_appearance","hidden");
        String config=BemInstalledResources.prepare(getInstrumentation().getTargetContext(),new JSONArray().put(entry).toString(),name->new FileInputStream(input),x->{});
        assertTrue(config.contains("appearances=hidden"));assertTrue(config.contains("replace=1"));
        entry.put("generation","../../bad");
        try {BemInstalledResources.prepare(getInstrumentation().getTargetContext(),new JSONArray().put(entry).toString(),name->new FileInputStream(input),x->{});fail("Accepted path traversal");}catch(IOException expected){}
    }
    public void testRealPackageConversionWhenProvided() throws Exception {
        File input=new File(getInstrumentation().getTargetContext().getFilesDir(),realInput);
        if(!input.isFile()) {if(method.equals("testRealPackageConversionWhenProvided")) fail("Missing real package");return;}
        File output=new File(directory,"real-converted.bem");output.delete();String rules;
        try(InputStream in=getInstrumentation().getTargetContext().getAssets().open("android-normal-rules.json")){rules=new String(in.readAllBytes(),java.nio.charset.StandardCharsets.UTF_8);}
        long start=System.currentTimeMillis();
        String report=BemInstaller.convertNative(input.getPath(),output.getPath(),rules,true);
        Files.writeString(new File(directory,"real-report.json").toPath(),report);
        android.util.Log.i("BetterEndfield.InstallTest","real conversion ms="+(System.currentTimeMillis()-start)+" report="+report);
        assertTrue(output.isFile());
    }
    public void testRemovalPreservesOtherPackages() throws Exception {
        android.content.Context base=getTargetContext();
        String run=java.util.UUID.randomUUID().toString();
        File isolated=new File(base.getCacheDir(),"remove-test-"+run);isolated.mkdirs();
        android.content.Context app=new android.content.ContextWrapper(base) {
            @Override public android.content.Context getApplicationContext(){return this;}
            @Override public File getFilesDir(){return isolated;}
            @Override public android.content.SharedPreferences getSharedPreferences(String name,int mode){return base.getSharedPreferences("remove-test-"+run,mode);}
        };
        String target=java.util.UUID.randomUUID().toString(),other=java.util.UUID.randomUUID().toString(),old=java.util.UUID.randomUUID().toString();
        File root=new File(isolated,"bem-installed");root.mkdirs();
        for(String id:new String[]{target,other,old}) {
            File folder=new File(root,id);folder.mkdir();
            Files.writeString(new File(folder,"installed.bem").toPath(),"test bytes");
            Files.writeString(new File(folder,"report.json").toPath(),"{\"character_id\":\""+(id.equals(other)?"other":"target")+"\"}");
        }
        JSONObject keep=new JSONObject().put("generation",other).put("character_id","other").put("enabled",true).put("selected_appearance","alternate");
        JSONArray entries=new JSONArray().put(new JSONObject().put("generation",target).put("character_id","target").put("name","Removal fixture")).put(keep);
        FrameworkSettings.open(app).edit().putString(BemInstaller.INDEX,entries.toString()).commit();
        try {BemInstaller.remove(app,"../../bad");fail("Accepted unsafe generation");}catch(IOException expected){}
        assertEquals(2,BemInstaller.index(app).length());
        BemInstaller.remove(app,target);
        long deadline=System.currentTimeMillis()+10000;
        while(BemInstaller.busy && System.currentTimeMillis()<deadline) Thread.sleep(50);
        assertFalse(BemInstaller.busy);
        JSONArray remaining=BemInstaller.index(app);
        assertEquals(1,remaining.length());assertTrue(keep.toString().equals(remaining.getJSONObject(0).toString()));
        assertFalse(new File(root,target).exists());assertFalse(new File(root,old).exists());
        assertTrue(new File(root,other+"/installed.bem").isFile());
    }
}
