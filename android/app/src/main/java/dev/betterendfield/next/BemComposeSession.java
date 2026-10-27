package dev.betterendfield.next;

import android.content.Context;
import org.json.*;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.util.*;

/** Private, durable editor state. No mesh or WebView exists until preview is requested. */
final class BemComposeSession {
    final File directory;
    private static native String runNative(String action,String input,String choices,String output,String cancel) throws IOException;
    private BemComposeSession(File root){directory=root;}
    static BemComposeSession open(Context context,String id) throws IOException {
        if(id==null || !id.matches("[a-f0-9-]{36}"))throw new IOException("Invalid composition session");
        File root=new File(new File(context.getFilesDir(),"bem-composer"),id);
        if(!root.isDirectory())throw new IOException("Composition session unavailable");
        return new BemComposeSession(root);
    }
    static BemComposeSession create(Context context) throws IOException {
        File root=new File(new File(context.getFilesDir(),"bem-composer"),UUID.randomUUID().toString());
        if(!root.mkdirs())throw new IOException("Cannot create composition session");
        BemComposeSession result=new BemComposeSession(root);result.write("choices.json",new JSONObject());return result;
    }
    String id(){return directory.getName();}
    File file(String name){return new File(directory,name);}
    synchronized JSONObject read(String name) throws Exception {
        return new JSONObject(new String(Files.readAllBytes(file(name).toPath()),StandardCharsets.UTF_8));
    }
    synchronized void write(String name,JSONObject value) throws IOException {
        File temp=file(name+".tmp");
        try(FileOutputStream stream=new FileOutputStream(temp)){stream.write(value.toString().getBytes(StandardCharsets.UTF_8));stream.getFD().sync();}
        Files.move(temp.toPath(),file(name).toPath(),java.nio.file.StandardCopyOption.REPLACE_EXISTING);
    }
    JSONObject run(String action,File output,File cancel) throws Exception {
        return run(action,output,cancel,file("choices.json"));
    }
    JSONObject run(String action,File output,File cancel,File choices) throws Exception {
        BemInstaller.loadCodec();
        return new JSONObject(runNative(action,file(action.equals("plan")?"request.json":"plan.json").getAbsolutePath(),
            choices.getAbsolutePath(),output.getAbsolutePath(),cancel.getAbsolutePath()));
    }
    void analyze() throws Exception {
        JSONObject plan=run("plan",file("unused"),file("cancel-plan"));
        write("plan.json",plan);write("choices.json",new JSONObject());
    }
    static File installed(Context context,String generation) throws IOException {
        if(!generation.matches("[a-f0-9-]{36}"))throw new IOException("Invalid source generation");
        File input=new File(new File(new File(context.getFilesDir(),"bem-installed"),generation),"installed.bem");
        if(!input.isFile())throw new IOException(Text.of("请先开启保留本地副本"));return input;
    }
    // Apply only declared options from a successfully rendered snapshot. Region edits stay intact.
    void applySelection(String selection,JSONObject declared,String prefix) throws Exception {
        JSONObject choices=read("choices.json");
        for(String pair:selection.split("&")) {
            int split=pair.indexOf(':');if(split<=0)continue;
            String key=pair.substring(0,split);
            if(declared.has(key) && !key.equals("body_edits"))choices.put(prefix+key,pair.substring(split+1));
        }
        write("choices.json",choices);
    }
    static String bodyName(String key) {
        return Text.of(switch(key){case "neck"->"颈部";case "head"->"头部";case "torso"->"躯干";case "pelvis"->"骨盆";
            case "left_arm"->"左臂";case "right_arm"->"右臂";case "left_leg"->"左腿";case "right_leg"->"右腿";default->"未分类";});
    }
    static void remove(File root) {
        File[] children=root.listFiles();if(children!=null)for(File child:children)remove(child);root.delete();
    }
}
