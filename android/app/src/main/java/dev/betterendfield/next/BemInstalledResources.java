package dev.betterendfield.next;

import android.content.Context;
import org.json.*;
import java.io.*;
import java.util.function.Consumer;

/** Copy framework-published immutable generations into the game's own private directory. */
final class BemInstalledResources {
    interface Source {InputStream open(String name) throws Exception;}
    static volatile String configuration="";
    static String prepare(Context context,String index,Source source,Consumer<String> log) throws Exception {
        return prepare(context,index,source,log,false);
    }
    static String prepare(Context context,String index,Source source,Consumer<String> log,boolean skipValidation) throws Exception {
        return prepare(context,index,source,log,skipValidation,false,false);
    }
    static String prepare(Context context,String index,Source source,Consumer<String> log,
            boolean skipValidation,boolean hotSwitch,boolean fastLoading) throws Exception {
        return prepare(context,index,source,log,skipValidation,hotSwitch,fastLoading,false);
    }
    /** pruneUnused is only safe before the runtime has opened any package path. */
    static String prepare(Context context,String index,Source source,Consumer<String> log,
            boolean skipValidation,boolean hotSwitch,boolean fastLoading,boolean pruneUnused) throws Exception {
        java.util.Set<String> used=new java.util.HashSet<>();
        JSONArray entries=BemOptions.exclusive(new JSONArray(index));StringBuilder paths=new StringBuilder(),appearances=new StringBuilder(),options=new StringBuilder(),parameters=new StringBuilder();
        File root=new File(context.getFilesDir(),"betterendfieldnext/installed-models");
        if(!root.isDirectory()&&!root.mkdirs()) throw new IOException("Cannot create game model directory");
        for(int i=0;i<entries.length();++i) {
            JSONObject entry=entries.getJSONObject(i);if(!entry.optBoolean("enabled",true)) continue;
            boolean composable=entry.optInt("bem_minor",0)>=1;
            String appearance=composable?"":BemOptions.appearance(entry,entry.optString("selected_appearance",entry.getString("default_appearance")));
            String selection=composable?BemOptions.encode(BemOptions.parse(entry,entry.optString("selected_options",entry.getString("default_options")))):"";
            String parameterSelection=BemParameters.encode(BemParameters.parse(entry,entry.optString("selected_parameters",entry.optString("default_parameters",""))));
            String generation=entry.getString("generation"),remote=entry.getString("remote");
            if(!generation.matches("[a-f0-9-]{36}")||!remote.equals("bem-"+generation+".bem")) throw new IOException("Invalid installed generation");
            long expected=entry.getLong("bytes");if(expected<=0||expected>2L*1024*1024*1024) throw new IOException("Invalid installed size");
            File output=new File(root,generation+".bem"),temp=new File(root,generation+".tmp");used.add(output.getName());
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
            if(paths.length()>0) {paths.append(',');appearances.append(',');options.append(',');parameters.append(',');}
            paths.append(output.getAbsolutePath());appearances.append(appearance);options.append(selection);parameters.append(parameterSelection);
            log.accept("Installed BEM ready: "+entry.getString("package_id")+" selection="+(composable?selection:appearance));
        }
        File[] stale=pruneUnused?root.listFiles():null;
        if(stale!=null) for(File file:stale) {
            String name=file.getName();
            if(name.matches("[a-f0-9-]{36}\\.(bem|tmp)") && !used.contains(name) && file.delete())
                log.accept("Unused installed BEM removed: "+name);
        }
        return paths.length()==0&&!hotSwitch?"":"resource=auto;replace=1;lod_pipeline=1;lod_npc=1;packages="+paths
            +";appearances="+appearances+";options="+options+";parameters="+parameters+";skip_validation="+(skipValidation?"1":"0")
            +";hot_switch="+(hotSwitch?"1":"0")+";fast_loading="+(fastLoading?"1":"0");
    }
}
