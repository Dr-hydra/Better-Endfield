package dev.betterendfield.next;

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
                test.invoke(this);android.util.Log.i("BetterEndfieldNext.InstallTest","PASS "+test.getName());
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
            .put("bytes",input.length()).put("package_id","test.package").put("character_id","target")
            .put("default_appearance","hidden").put("appearances",new JSONArray().put("hidden"));
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
        android.util.Log.i("BetterEndfieldNext.InstallTest","real conversion ms="+(System.currentTimeMillis()-start)+" report="+report);
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
        JSONObject keep=new JSONObject().put("generation",other).put("character_id","target").put("enabled",true)
                .put("default_appearance","alternate").put("appearances",new JSONArray().put("alternate")).put("selected_appearance","alternate");
        JSONArray entries=new JSONArray().put(new JSONObject().put("generation",target).put("character_id","target").put("enabled",false)
                .put("name","Removal fixture").put("default_appearance","default").put("appearances",new JSONArray().put("default"))).put(keep);
        FrameworkSettings.open(app).edit().putString(BemInstaller.INDEX,entries.toString()).commit();
        try {BemInstaller.remove(app,"../../bad");fail("Accepted unsafe generation");}catch(IOException expected){}
        assertEquals(2,BemInstaller.index(app).length());
        BemInstaller.remove(app,target);
        long deadline=System.currentTimeMillis()+10000;
        while(BemInstaller.busy && System.currentTimeMillis()<deadline) Thread.sleep(50);
        assertFalse(BemInstaller.busy);
        JSONArray remaining=BemInstaller.index(app);
        assertEquals(1,remaining.length());assertTrue(keep.toString().equals(remaining.getJSONObject(0).toString()));
        assertFalse(new File(root,target).exists());assertTrue(new File(root,old).exists());
        assertTrue(new File(root,other+"/installed.bem").isFile());
    }
    public void testLegacyMigrationAndImmediateExclusiveSelection() throws Exception {
        android.content.Context base=getTargetContext();String run=java.util.UUID.randomUUID().toString();
        File isolated=new File(base.getCacheDir(),"selection-test-"+run);isolated.mkdirs();
        android.content.Context app=new android.content.ContextWrapper(base) {
            @Override public android.content.Context getApplicationContext(){return this;}
            @Override public File getFilesDir(){return isolated;}
            @Override public android.content.SharedPreferences getSharedPreferences(String name,int mode){return base.getSharedPreferences("selection-test-"+run,mode);}
        };
        JSONArray entries=new JSONArray();
        for(int i=0;i<3;i++) entries.put(new JSONObject().put("generation",java.util.UUID.randomUUID().toString())
                .put("character_id",i<2?"same":"other").put("name","Selection fixture")
                .put("default_appearance","default").put("appearances",new JSONArray().put("default").put("alternate")));
        assertTrue(FrameworkSettings.open(app).edit().putString(BemInstaller.INDEX,entries.toString()).commit());
        JSONArray migrated=BemInstaller.index(app);
        assertEquals(3,migrated.length());assertFalse(migrated.getJSONObject(0).getBoolean("enabled"));
        assertTrue(migrated.getJSONObject(1).getBoolean("enabled"));assertTrue(migrated.getJSONObject(2).getBoolean("enabled"));
        String first=migrated.getJSONObject(0).getString("generation"),second=migrated.getJSONObject(1).getString("generation");
        BemInstaller.select(app,first,"alternate",true);
        JSONArray saved=BemInstaller.index(app);
        assertTrue(saved.getJSONObject(0).getBoolean("enabled"));assertFalse(saved.getJSONObject(1).getBoolean("enabled"));
        assertTrue(saved.getJSONObject(0).getString("selected_appearance").equals("alternate"));
        assertTrue(saved.getJSONObject(2).toString().equals(migrated.getJSONObject(2).toString()));
        String durable=Files.readString(new File(isolated,"bem-index.json").toPath());
        assertTrue(durable.equals(saved.toString()));
        try {BemInstaller.select(app,second,"removed-choice",true);fail("Saved invalid appearance");}catch(IOException expected){}
        assertTrue(saved.toString().equals(BemInstaller.index(app).toString()));
        JSONObject converted=new JSONObject(saved.getJSONObject(0).toString()).put("generation",java.util.UUID.randomUUID().toString());
        JSONArray next=BemInstaller.installedIndex(saved,converted,first);
        assertEquals(3,next.length());assertTrue(next.getJSONObject(2).getBoolean("enabled"));
        assertTrue(next.getJSONObject(2).getString("selected_appearance").equals("alternate"));
        assertTrue(next.getJSONObject(0).getString("generation").equals(second));
    }
    /** Android framework tests: compile locally, execute with the installed test APK. */
    public void testIncomingBemOpenAndShareRequests() throws Exception {
        android.net.Uri uri=android.net.Uri.parse("content://bem.test.provider/document/123");
        android.content.Intent view=new android.content.Intent(android.content.Intent.ACTION_VIEW,uri);
        assertTrue(uri.equals(BemImportRequest.fromIntent(view)));
        android.content.Intent share=new android.content.Intent(android.content.Intent.ACTION_SEND)
                .setType("application/octet-stream").putExtra(android.content.Intent.EXTRA_STREAM,uri);
        assertTrue(uri.equals(BemImportRequest.fromIntent(share)));
        share.setClipData(android.content.ClipData.newRawUri("opaque",uri));
        assertTrue(uri.equals(BemImportRequest.fromIntent(share)));
        android.content.Intent clipOnly=new android.content.Intent(android.content.Intent.ACTION_SEND);
        clipOnly.setClipData(android.content.ClipData.newRawUri("fixture.BEM",uri));
        assertTrue(uri.equals(BemImportRequest.fromIntent(clipOnly)));
        assertTrue(BemImportRequest.fromIntent(new android.content.Intent())==null);
        assertTrue(BemImportRequest.fromIntent(new android.content.Intent(android.content.Intent.ACTION_MAIN))==null);
        for(String raw:new String[]{"file:///sdcard/model.bem","https://example.com/model.bem","content:/missing-authority"}) {
            try {BemImportRequest.fromIntent(new android.content.Intent(android.content.Intent.ACTION_VIEW,android.net.Uri.parse(raw)));fail("Accepted unsafe URI");}
            catch(IllegalArgumentException expected) {}
        }
        android.content.Intent text=new android.content.Intent(android.content.Intent.ACTION_SEND)
                .putExtra(android.content.Intent.EXTRA_TEXT,uri.toString());
        try {BemImportRequest.fromIntent(text);fail("Accepted text instead of a grant-bearing URI");}
        catch(IllegalArgumentException expected) {}
        android.content.Intent wrongType=new android.content.Intent(android.content.Intent.ACTION_SEND)
                .putExtra(android.content.Intent.EXTRA_STREAM,"not a Uri");
        try {BemImportRequest.fromIntent(wrongType);fail("Accepted malformed stream extra");}
        catch(IllegalArgumentException expected) {}
        android.content.ClipData multiple=android.content.ClipData.newRawUri("first",uri);
        multiple.addItem(new android.content.ClipData.Item(android.net.Uri.parse("content://bem.test.provider/second")));
        clipOnly.setClipData(multiple);
        try {BemImportRequest.fromIntent(clipOnly);fail("Silently selected from multiple documents");}
        catch(IllegalArgumentException expected) {}
        share.setClipData(android.content.ClipData.newRawUri("conflict",android.net.Uri.parse("content://bem.test.provider/other")));
        try {BemImportRequest.fromIntent(share);fail("Accepted conflicting URI fields");}
        catch(IllegalArgumentException expected) {}
    }
    public void testBemImportBusyResult() throws Exception {
        // Do not touch storage/native parsing; verify that busy is observable and
        // the running operation's status is not overwritten by another request.
        synchronized(BemInstaller.class) {
            if(BemInstaller.busy) fail("Run installer tests with no active import");
            String previous=BemInstaller.status;
            BemInstaller.busy=true;
            try {
                assertFalse(BemInstaller.start(getTargetContext(),android.net.Uri.parse("content://bem.test.provider/document/123")));
                assertTrue(previous.equals(BemInstaller.status));
            } finally {BemInstaller.busy=false;}
        }
    }
    public void testBemOpenWithManifestResolution() throws Exception {
        android.content.Context context=getTargetContext();
        android.content.pm.PackageManager pm=context.getPackageManager();
        android.content.ComponentName component=new android.content.ComponentName(context,BemInstallActivity.class);
        android.content.pm.ActivityInfo info=pm.getActivityInfo(component,0);
        assertTrue(info.exported);
        assertEquals(android.content.pm.ActivityInfo.LAUNCH_SINGLE_TOP,info.launchMode);
        android.net.Uri content=android.net.Uri.parse("content://bem.test.provider/document/123");
        for(String mime:new String[]{"application/x-bem","application/vnd.betterendfield.bem","application/octet-stream","application/x-binary"}) {
            android.content.Intent view=new android.content.Intent(android.content.Intent.ACTION_VIEW)
                    .setDataAndType(content,mime).setPackage(context.getPackageName());
            assertTrue(resolvesBem(pm,view));
            android.content.Intent send=new android.content.Intent(android.content.Intent.ACTION_SEND)
                    .setType(mime).putExtra(android.content.Intent.EXTRA_STREAM,content).setPackage(context.getPackageName());
            assertTrue(resolvesBem(pm,send));
        }
        for(String mime:new String[]{"image/png","application/pdf","text/plain"}) {
            android.content.Intent unrelated=new android.content.Intent(android.content.Intent.ACTION_VIEW)
                    .setDataAndType(content,mime).setPackage(context.getPackageName());
            assertFalse(resolvesBem(pm,unrelated));
        }
        android.content.Intent path=new android.content.Intent(android.content.Intent.ACTION_VIEW)
                .setDataAndType(android.net.Uri.parse("file:///sdcard/model.bem"),"application/octet-stream")
                .setPackage(context.getPackageName());
        assertFalse(resolvesBem(pm,path));
    }
    private static boolean resolvesBem(android.content.pm.PackageManager pm,android.content.Intent intent) {
        for(android.content.pm.ResolveInfo info:pm.queryIntentActivities(intent,android.content.pm.PackageManager.MATCH_DEFAULT_ONLY))
            if(BemInstallActivity.class.getName().equals(info.activityInfo.name)) return true;
        return false;
    }

}
