package dev.betterendfield.android;

import android.content.Context;
import org.json.*;
import java.io.*;
import java.util.function.Consumer;

/** Copy framework-published immutable generations into the game's own private directory. */
final class BemInstalledResources {
    interface Source {InputStream open(String name) throws Exception;}
    static volatile String configuration="";
    static String prepare(Context context,String index,Source source,Consumer<String> log) throws Exception {
        JSONArray entries=new JSONArray(index);StringBuilder paths=new StringBuilder(),appearances=new StringBuilder();
        File root=new File(context.getFilesDir(),"betterendfield/installed-models");
        if(!root.isDirectory()&&!root.mkdirs()) throw new IOException("Cannot create game model directory");
        for(int i=0;i<entries.length();++i) {
            JSONObject entry=entries.getJSONObject(i);if(!entry.optBoolean("enabled",true)) continue;
            String generation=entry.getString("generation"),remote=entry.getString("remote");
            if(!generation.matches("[a-f0-9-]{36}")||!remote.equals("bem-"+generation+".bem")) throw new IOException("Invalid installed generation");
            long expected=entry.getLong("bytes");if(expected<=0||expected>2L*1024*1024*1024) throw new IOException("Invalid installed size");
            File output=new File(root,generation+".bem"),temp=new File(root,generation+".tmp");
            if(!output.isFile()||output.length()!=expected) {
                try {
                    try(InputStream in=source.open(remote);FileOutputStream out=new FileOutputStream(temp)) {
                        byte[] buffer=new byte[65536];long length=0;int n;
                        while((n=in.read(buffer))!=-1) {length+=n;if(length>expected) throw new IOException("Installed payload exceeds declared size");out.write(buffer,0,n);}
                        if(length!=expected) throw new IOException("Truncated installed payload");out.getFD().sync();
                    }
                    android.system.Os.rename(temp.getAbsolutePath(),output.getAbsolutePath());
                } finally {temp.delete();}
            }
            String appearance=entry.optString("selected_appearance",entry.getString("default_appearance"));
            if(!appearance.matches("[A-Za-z0-9][A-Za-z0-9_.-]{0,95}")) throw new IOException("Invalid appearance");
            if(paths.length()>0) {paths.append(',');appearances.append(',');}
            paths.append(output.getAbsolutePath());appearances.append(appearance);
            log.accept("Installed BEM ready: "+entry.getString("package_id")+" appearance="+appearance);
        }
        return paths.length()==0?"":"resource=auto;replace=1;lod_pipeline=1;lod_npc=1;packages="+paths+";appearances="+appearances;
    }
}
