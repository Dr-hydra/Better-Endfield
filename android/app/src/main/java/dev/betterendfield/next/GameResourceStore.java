package dev.betterendfield.next;

import android.content.Context;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.*;
import java.util.*;
import java.util.zip.*;
import org.json.*;

/** Versioned external game data with an APK-bundled offline core baseline. */
final class GameResourceStore {
    static final class VersionedInput extends FilterInputStream {
        final String version; final long length;
        VersionedInput(InputStream source,String version,long length){super(source);this.version=version;this.length=length;}
    }
    record Snapshot(File root, String version, JSONObject manifest) {
        File file(String relative) throws Exception {
            File result=inside(root,relative);
            if(!manifest.getJSONObject("files").has(relative) || !result.isFile() ||
                    result.length()!=manifest.getJSONObject("files").getLong(relative))
                throw new IOException("Resource file is missing or incomplete: "+relative);
            return result;
        }
    }
    private GameResourceStore() {}
    static synchronized void ensureBundledCore(Context context) throws Exception {
        if (resolve(context, "core") != null) return;
        // Never replace a user's explicitly selected version behind their back.
        File state = new File(root(context), "state.json");
        if (state.isFile() && json(state).has("core.android")) return;
        try (InputStream baseline = context.getAssets().open("bundled-resources/core.zip")) {
            install(context, baseline, true, null);
        }
    }
    static File root(Context context) { return new File(context.getFilesDir(),"betterendfieldnext/resources"); }
    static JSONObject json(File file) throws Exception {
        if(file.length()>16L*1024*1024)throw new IOException("Resource metadata is too large");
        return new JSONObject(new String(Files.readAllBytes(file.toPath()),StandardCharsets.UTF_8));
    }
    static File inside(File root,String path) throws IOException {
        if(path.isEmpty() || path.contains("\\") || path.contains(":") || path.startsWith("/"))throw new IOException("Invalid resource path");
        for(String part:path.split("/",-1))if(part.isEmpty() || part.equals(".") || part.equals(".."))throw new IOException("Invalid resource path");
        File result=new File(root,path);
        if(!result.getCanonicalPath().startsWith(root.getCanonicalPath()+File.separator))throw new IOException("Resource path escapes its root");
        for(File part=result;part!=null && !part.equals(root);part=part.getParentFile())
            if(Files.isSymbolicLink(part.toPath()))throw new IOException("Resource links are not supported");
        return result;
    }
    static int compare(String a,String b) {
        String[] x=a.split("\\."),y=b.split("\\.");
        for(int i=0;i<4;i++){int c=Long.compare(Long.parseLong(x[i]),Long.parseLong(y[i]));if(c!=0)return c;}return 0;
    }
    static boolean version(String s) { return s.matches("[0-9]{1,9}(\\.[0-9]{1,9}){3}"); }
    static Snapshot resolve(Context context,String type) {
        try {
            File base=new File(root(context),type+"/android");
            File[] dirs=base.listFiles(File::isDirectory);if(dirs==null)return null;
            dirs=Arrays.stream(dirs).filter(d->version(d.getName())).toArray(File[]::new);
            String selected=null;File state=new File(root(context),"state.json");
            if(state.isFile())selected=json(state).optString(type+".android",null);
            Arrays.sort(dirs,(a,b)->-compare(a.getName(),b.getName()));
            for(File dir:dirs) {
                if(!version(dir.getName()) || (selected!=null && !selected.equals(dir.getName())))continue;
                try {
                    JSONObject m=json(new File(dir,"manifest.json"));
                    validate(m);
                    if(type.equals(m.getString("packageType")) && dir.getName().equals(m.getString("version")))return new Snapshot(dir,dir.getName(),m);
                }catch(Exception invalid){}
            }
        }catch(Exception invalid){}
        return null;
    }
    static InputStream openCore(Context context,String name) throws IOException {
        try {
            name=switch(name){case "character-presets.json","character-names.json","character-names-en.json","android-normal-rules.json" -> "model/"+name;case "voice-catalog-index.json" -> "voice/"+name;default -> name;};
            if(context.getApplicationInfo().uid!=android.os.Process.myUid() || !BuildConfig.APPLICATION_ID.equals(context.getPackageName()))
                return OverlaySettingsClient.openCore(context,name);
            ensureBundledCore(context);
            Snapshot snapshot=resolve(context,"core");
            if(snapshot==null)throw new IOException("Missing Android core resource pack");
            File file=snapshot.file(name);
            return new VersionedInput(new FileInputStream(file),snapshot.version(),file.length());
        }catch(IOException error){throw error;}catch(Exception error){throw new IOException(error);}
    }
    static void validate(JSONObject m) throws Exception {
        if(m.getInt("schemaVersion")!=1 || !"betterendfield-resource-pack".equals(m.getString("kind")) ||
                !"android".equals(m.getString("platform")) || !version(m.getString("version")) ||
                !Set.of("core","preview").contains(m.getString("packageType")))throw new IOException("Wrong platform or unsupported resource package");
        m.getJSONObject("files");
    }
    static synchronized String install(Context context,InputStream source) throws Exception {
        return install(context,source,false,null);
    }
    static synchronized String install(Context context,InputStream source,boolean automatic,JSONObject expected) throws Exception {
        File stage=new File(root(context),"staging/"+UUID.randomUUID());
        if(!stage.mkdirs())throw new IOException("Cannot create resource staging directory");
        try {
            Set<String> names=new HashSet<>();long total=0;
            try(ZipInputStream zip=new ZipInputStream(source)) {
                ZipEntry entry;byte[] buffer=new byte[65536];
                while((entry=zip.getNextEntry())!=null) {
                    if(entry.isDirectory())continue;
                    String name=entry.getName();File out=inside(stage,name);
                    if(!names.add(name) || names.size()>200000)throw new IOException("Duplicate or excessive resource entries");
                    if(!out.getParentFile().isDirectory() && !out.getParentFile().mkdirs())throw new IOException("Cannot create resource directory");
                    try(FileOutputStream sink=new FileOutputStream(out)) {
                        int n;while((n=zip.read(buffer))!=-1){total+=n;if(stage.getUsableSpace()<n+1024L*1024)throw new IOException("Insufficient storage");sink.write(buffer,0,n);}sink.getFD().sync();
                    }
                }
            }
            JSONObject m=json(new File(stage,"manifest.json"));validate(m);
            if(!m.getString("gameVersion").equals(m.getString("version").substring(0,m.getString("version").lastIndexOf('.'))))throw new IOException("Resource game version mismatch");
            if(expected!=null)for(String key:List.of("packageType","platform","version","gameVersion"))
                if(!expected.getString(key).equals(m.getString(key)))throw new IOException("Downloaded resource identity mismatch");
            Snapshot snapshot=new Snapshot(stage,m.getString("version"),m);
            JSONObject files=m.getJSONObject("files");Iterator<String> it=files.keys();
            while(it.hasNext())snapshot.file(it.next());
            if(names.size()!=files.length()+1)throw new IOException("Unlisted resource entries");
            String type=m.getString("packageType");
            if(type.equals("preview"))snapshot.file("characters.json");
            else {snapshot.file("model/character-presets.json");snapshot.file("model/character-names.json");snapshot.file("voice/voice-catalog-index.json");}
            File target=new File(root(context),type+"/android/"+snapshot.version());
            boolean existing=target.exists();
            if(existing)for(String name:names)if(!sameFile(inside(stage,name),inside(target,name)))
                throw new IOException("Existing resource version differs; increment its revision");
            if(!target.getParentFile().isDirectory() && !target.getParentFile().mkdirs())throw new IOException("Cannot create resource directory");
            if(!existing)Files.move(stage.toPath(),target.toPath(),StandardCopyOption.ATOMIC_MOVE);
            File state=new File(root(context),"state.json");JSONObject active=state.isFile()?json(state):new JSONObject();
            active.put(type+".android",snapshot.version());
            active.put("auto."+type+".android",automatic);
            File pending=new File(root(context),"state.json.tmp");
            try(FileOutputStream output=new FileOutputStream(pending)){output.write(active.toString().getBytes(StandardCharsets.UTF_8));output.getFD().sync();}
            Files.move(pending.toPath(),state.toPath(),StandardCopyOption.REPLACE_EXISTING,StandardCopyOption.ATOMIC_MOVE);
            return type+" "+snapshot.version();
        }finally{remove(stage);}
    }
    private static boolean sameFile(File a,File b) throws IOException {
        if(!b.isFile() || a.length()!=b.length())return false;
        try(InputStream x=new BufferedInputStream(new FileInputStream(a));InputStream y=new BufferedInputStream(new FileInputStream(b))) {
            byte[] xb=new byte[65536],yb=new byte[65536];int n;
            while((n=x.read(xb))!=-1) {int done=0;while(done<n){int r=y.read(yb,done,n-done);if(r<0)return false;done+=r;}for(int i=0;i<n;i++)if(xb[i]!=yb[i])return false;}
            return y.read()==-1;
        }
    }
    private static void remove(File path) {File[] files=path.listFiles();if(files!=null)for(File file:files)remove(file);path.delete();}
}
