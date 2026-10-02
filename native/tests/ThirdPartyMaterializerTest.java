package dev.betterendfield.android;
import android.content.Context;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.util.UUID;
import java.util.zip.ZipEntry;
import java.util.zip.ZipOutputStream;
import org.json.JSONArray;
import org.json.JSONObject;
public final class ThirdPartyMaterializerTest {
    static int checks;
    static void check(boolean value,String message){checks++;if(!value)throw new AssertionError(message);}
    static byte[] archive(String id,String extra) throws Exception {
        ByteArrayOutputStream bytes=new ByteArrayOutputStream();
        try(ZipOutputStream zip=new ZipOutputStream(bytes)){
            zip.putNextEntry(new ZipEntry("module.json"));zip.write(new JSONObject().put("format",1).put("abi",1).put("id",id)
                .put("libraries",new JSONObject().put("android-arm64","native/android-arm64/example.so")).toString().getBytes(StandardCharsets.UTF_8));zip.closeEntry();
            zip.putNextEntry(new ZipEntry("native/android-arm64/example.so"));zip.write(new byte[]{1,2,3});zip.closeEntry();
            if(extra!=null){zip.putNextEntry(new ZipEntry(extra));zip.write(new byte[]{5});zip.closeEntry();}
        }
        return bytes.toByteArray();
    }
    static JSONObject index(String id,String generation,long bytes){
        return new JSONObject().put("schema",1).put("port",41021).put("token","test_local_only_private_token_12345678").put("modules",new JSONArray().put(
            new JSONObject().put("id",id).put("generation",generation).put("android_remote","tpm-"+generation+".zip")
                .put("archive_bytes",bytes).put("directory","ignored-app-path").put("enabled",true).put("configuration",new JSONObject().put("opaque",new JSONArray().put(3)))));
    }
    public static void main(String[] arguments)throws Exception{
        File directory=Files.createTempDirectory("third-party-materializer-").toFile();
        Context context=new Context(){public File getFilesDir(){return directory;}};
        try{
            byte[] zip=archive("example.echo",null);String generation=UUID.randomUUID().toString();JSONObject input=index("example.echo",generation,zip.length);
            int[] opens={0};String output=ThirdPartyRuntimeMaterializer.prepare(context,input.toString(),name->{opens[0]++;return new ByteArrayInputStream(zip);},message->{});
            JSONObject materialized=new JSONObject(Files.readString(new File(output).toPath()));JSONObject record=materialized.getJSONArray("modules").getJSONObject(0);
            check(record.getBoolean("enabled"),"valid module disabled");check(new File(record.getString("directory"),"native/android-arm64/example.so").isFile(),"native payload missing");
            check(record.getJSONObject("configuration").getJSONArray("opaque").getInt(0)==3,"opaque config lost");
            ThirdPartyRuntimeMaterializer.prepare(context,input.toString(),name->{opens[0]++;throw new IOException("should reuse generation");},message->{});
            check(opens[0]==1,"immutable generation was unnecessarily downloaded");
            for(String invalid:new String[]{"../outside","/absolute","a/../escape","a\\escape","MODULE.JSON"}){
                byte[] bad=archive("example.echo",invalid);JSONObject malformed=index("example.echo",UUID.randomUUID().toString(),bad.length);
                String path=ThirdPartyRuntimeMaterializer.prepare(context,malformed.toString(),name->new ByteArrayInputStream(bad),message->{});
                JSONObject error=new JSONObject(Files.readString(new File(path).toPath())).getJSONArray("modules").getJSONObject(0);
                check(!error.getBoolean("enabled")&&error.has("preparation_error"),"invalid archive accepted: "+invalid);
            }
            JSONObject wrong=index("example.wrong",UUID.randomUUID().toString(),zip.length);
            String wrongPath=ThirdPartyRuntimeMaterializer.prepare(context,wrong.toString(),name->new ByteArrayInputStream(zip),message->{});
            check(!new JSONObject(Files.readString(new File(wrongPath).toPath())).getJSONArray("modules").getJSONObject(0).getBoolean("enabled"),"manifest identity mismatch accepted");
            JSONObject mixed=index("example.echo",generation,zip.length);mixed.getJSONArray("modules").put(wrong.getJSONArray("modules").getJSONObject(0));
            String mixedPath=ThirdPartyRuntimeMaterializer.prepare(context,mixed.toString(),name->new ByteArrayInputStream(zip),message->{});
            JSONArray records=new JSONObject(Files.readString(new File(mixedPath).toPath())).getJSONArray("modules");
            check(records.getJSONObject(0).getBoolean("enabled")&&!records.getJSONObject(1).getBoolean("enabled"),"one failed module rejected other valid modules");
            check(!new File(directory,"outside").exists(),"ZIP traversal created outside file");
            ByteArrayOutputStream uiBytes=new ByteArrayOutputStream();
            try(ZipOutputStream ui=new ZipOutputStream(uiBytes)){
                ui.putNextEntry(new ZipEntry("module.json"));ui.write(new JSONObject().put("format",1).put("abi",1).put("id","example.ui")
                    .put("libraries",new JSONObject()).put("ui","ui/index.html").toString().getBytes(StandardCharsets.UTF_8));ui.closeEntry();
                ui.putNextEntry(new ZipEntry("ui/index.html"));ui.write("<!doctype html><title>UI</title>".getBytes(StandardCharsets.UTF_8));ui.closeEntry();
            }
            byte[] uiArchive=uiBytes.toByteArray();JSONObject uiIndex=index("example.ui",UUID.randomUUID().toString(),uiArchive.length);
            String uiPath=ThirdPartyRuntimeMaterializer.prepare(context,uiIndex.toString(),name->new ByteArrayInputStream(uiArchive),message->{});
            check(new JSONObject(Files.readString(new File(uiPath).toPath())).getJSONArray("modules").getJSONObject(0).getBoolean("enabled"),"UI-only package required Android native binary");
            for(String reserved:new String[]{"voice.character","VoIcE.ChArAcTeR","betterendfield.test"}){
                boolean rejected=false;try{ThirdPartyRuntimeMaterializer.prepare(context,index(reserved,UUID.randomUUID().toString(),zip.length).toString(),
                    name->new ByteArrayInputStream(zip),message->{});}catch(IOException expected){rejected=true;}
                check(rejected,"Reserved native module ID accepted: "+reserved);
            }
            System.out.println("PASS Android runtime ZIP materialization: immutable generations/config/paths/duplicates/identity/failure isolation ("+checks+" checks)");
        }finally{try(var files=Files.walk(directory.toPath())){files.sorted(java.util.Comparator.reverseOrder()).forEach(path->{try{Files.delete(path);}catch(IOException error){throw new RuntimeException(error);}});}}
    }
}
