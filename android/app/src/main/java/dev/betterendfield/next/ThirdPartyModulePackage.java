package dev.betterendfield.next;

import org.json.*;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.*;
import java.util.zip.*;

/** Package format only. Imports never load code in the settings application. */
final class ThirdPartyModulePackage {
    static final long LIMIT=256L*1024*1024, ENTRY_LIMIT=128L*1024*1024;
    static void id(String value,boolean packageId) throws IOException {
        if(value==null || !value.matches("[A-Za-z0-9][A-Za-z0-9_.-]{0,95}") ||
                (packageId && (value.toLowerCase(Locale.ROOT).startsWith("betterendfieldnext.") || value.equalsIgnoreCase("voice.character"))))
            throw new IOException("无效或与内置模块冲突的模块 ID");
    }
    static String relative(String path) throws IOException {
        if(path==null || path.isEmpty() || path.length()>240 || path.startsWith("/") || path.contains("\\") || path.contains(":"))
            throw new IOException("不安全的模块资源路径");
        for(String part:path.split("/",-1)) {
            if(part.isEmpty() || part.equals(".") || part.equals("..") || part.endsWith(".") || part.endsWith(" ") ||
                    part.matches("(?i)(CON|PRN|AUX|NUL|COM[1-9]|LPT[1-9])(\\..*)?")) throw new IOException("不安全的模块资源路径");
            for(char c:part.toCharArray()) if(Character.isISOControl(c)) throw new IOException("不安全的模块资源路径");
        }
        return path;
    }
    static File resource(File root,String path) throws IOException {
        File target=new File(root,relative(path)).getCanonicalFile();
        if(!target.getPath().startsWith(root.getCanonicalPath()+File.separator)) throw new IOException("模块资源越过安装目录");
        return target;
    }
    static JSONObject manifest(File directory) throws Exception {
        File file=new File(directory,"module.json");
        if(!file.isFile() || file.length()>256*1024) throw new IOException("缺少或过大的 module.json");
        byte[] bytes=new byte[(int)file.length()];
        try(FileInputStream input=new FileInputStream(file)) {int offset=0,count;while(offset<bytes.length && (count=input.read(bytes,offset,bytes.length-offset))!=-1)offset+=count;if(offset!=bytes.length)throw new IOException("模块声明截断");}
        JSONObject manifest=new JSONObject(new String(bytes,StandardCharsets.UTF_8));
        if(!(manifest.get("format") instanceof Integer) || manifest.getInt("format")!=1 ||
                !(manifest.get("abi") instanceof Integer) || manifest.getInt("abi")!=1) throw new IOException("不支持的模块格式或 ABI");
        id(manifest.getString("id"),true);
        for(String key:new String[]{"name","author","version"}) {
            String value=manifest.getString(key);
            if(value.length()>200 || (key.equals("name") && value.isEmpty())) throw new IOException("无效的模块 "+key);
        }
        JSONObject libraries=manifest.getJSONObject("libraries");
        for(Iterator<String> platforms=libraries.keys();platforms.hasNext();) {
            String platform=platforms.next();
            String path=libraries.getString(platform);
            if(!resource(directory,path).isFile() || (platform.equals("windows-x64") && !path.toLowerCase(Locale.ROOT).endsWith(".dll")) ||
                    (platform.equals("android-arm64") && !path.endsWith(".so"))) throw new IOException("模块原生文件不存在或格式不正确");
        }
        String ui=manifest.optString("ui","");
        if(!ui.isEmpty() && (!ui.toLowerCase(Locale.ROOT).endsWith(".html") || !resource(directory,ui).isFile())) throw new IOException("模块网页入口不存在");
        if(ui.isEmpty() && libraries.length()==0) throw new IOException("模块需要原生库或网页入口");
        if(manifest.has("default_configuration") && !(manifest.get("default_configuration") instanceof JSONObject)) throw new IOException("配置必须是 JSON 对象");
        if(manifest.has("dependencies")) {
            JSONArray dependencies=manifest.getJSONArray("dependencies");
            if(dependencies.length()>128) throw new IOException("模块依赖不能超过 128 项");
            for(int i=0;i<dependencies.length();++i) {
                String dependency=dependencies.getString(i);id(dependency,true);
                if(dependency.equals(manifest.getString("id"))) throw new IOException("模块不能依赖自身");
            }
        }
        return manifest;
    }
    static boolean supported(JSONObject manifest) throws Exception {
        return !manifest.optString("ui","").isEmpty() || manifest.getJSONObject("libraries").has("android-arm64");
    }
    static JSONObject extract(File archive,File directory) throws Exception {
        if(archive.length()>LIMIT || !directory.mkdirs()) throw new IOException("模块 ZIP 过大或临时目录不可用");
        HashSet<String> names=new HashSet<>();long declared=0,actual=0;int entries=0;boolean rootManifest=false;
        try(ZipFile zip=new ZipFile(archive)) {
            Enumeration<? extends ZipEntry> iterator=zip.entries();
            while(iterator.hasMoreElements()) {
                ZipEntry entry=iterator.nextElement();boolean folder=entry.isDirectory();
                String name=relative(folder?entry.getName().substring(0,entry.getName().length()-1):entry.getName());
                if(++entries>4096 || !names.add(name.toLowerCase(Locale.ROOT)) || entry.getSize()<0 || entry.getSize()>ENTRY_LIMIT ||
                        (declared+=entry.getSize())>LIMIT) throw new IOException("模块 ZIP 存在重复路径或超出容量限制");
                File target=resource(directory,name);
                if(folder) {if(!target.isDirectory() && !target.mkdirs()) throw new IOException("无法创建模块目录");continue;}
                if(!target.getParentFile().isDirectory() && !target.getParentFile().mkdirs()) throw new IOException("无法创建模块目录");
                try(InputStream input=zip.getInputStream(entry);FileOutputStream output=new FileOutputStream(target)) {
                    byte[] buffer=new byte[65536];int count;long size=0;
                    while((count=input.read(buffer))!=-1) {
                        size+=count;actual+=count;if(size>ENTRY_LIMIT || actual>LIMIT) throw new IOException("模块 ZIP 解压超过限制");
                        output.write(buffer,0,count);
                    }
                    if(size!=entry.getSize()) throw new IOException("模块 ZIP 长度不一致");output.getFD().sync();
                }
                rootManifest |= entry.getName().equals("module.json");
            }
        }
        if(!rootManifest) throw new IOException("ZIP 根目录必须包含 module.json");
        return manifest(directory);
    }
}
