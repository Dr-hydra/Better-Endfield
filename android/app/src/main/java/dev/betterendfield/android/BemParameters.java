package dev.betterendfield.android;

import org.json.JSONArray;
import org.json.JSONObject;
import java.io.IOException;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.Map;

/** BEM 1.3 shape weights. No code or expression from a package is executed. */
final class BemParameters {
    private BemParameters() {}

    static JSONArray groups(JSONObject entry) throws Exception {
        if(entry.optInt("bem_minor",0)<3) return new JSONArray();
        JSONArray groups=entry.getJSONArray("parameters");
        HashSet<String> seen=new HashSet<>();
        for(int i=0;i<groups.length();++i) {
            JSONObject group=groups.getJSONObject(i);
            String id=group.getString("id");BemOptions.requireToken(id);group.getString("name");
            int min=tick(group,"min"),max=tick(group,"max"),step=tick(group,"step");
            int defaultValue=tick(group,"default"),neutral=tick(group,"neutral");
            if(!seen.add(id) || min>max || step<1 || (max-min)%step!=0 ||
                    !accepts(group,defaultValue) || !accepts(group,neutral)) throw new IOException("无效的滑条目录");
        }
        return groups;
    }

    static int tick(JSONObject group,String key) throws Exception {
        Object value=group.get(key);
        if(!(value instanceof Integer || value instanceof Long)) throw new IOException("滑条数值必须是整数");
        long number=((Number)value).longValue();
        if(number<0 || number>1000) throw new IOException("滑条数值必须在 0–1000 范围内");
        return (int)number;
    }

    static boolean accepts(JSONObject group,int value) throws Exception {
        int min=tick(group,"min"),max=tick(group,"max"),step=tick(group,"step");
        return step>0 && value>=min && value<=max && (value-min)%step==0;
    }

    static int snap(JSONObject group,int value) throws Exception {
        int min=tick(group,"min"),max=tick(group,"max"),step=tick(group,"step");
        return Math.max(min,Math.min(max,min+Math.round((value-min)/(float)step)*step));
    }

    static LinkedHashMap<String,Integer> remembered(String encoded) throws Exception {
        LinkedHashMap<String,Integer> saved=new LinkedHashMap<>();
        if(!encoded.isEmpty()) for(String pair:encoded.split("&",-1)) {
            String[] parts=pair.split(":",-1);
            if(parts.length!=2 || !parts[1].matches("[0-9]{1,10}")) throw new IOException("无效的滑条设置");
            BemOptions.requireToken(parts[0]);
            long value=Long.parseLong(parts[1]);
            if(value>1000 || saved.put(parts[0],(int)value)!=null) throw new IOException("无效或重复的滑条设置");
        }
        return saved;
    }

    static LinkedHashMap<String,Integer> parse(JSONObject entry,String encoded) throws Exception {
        LinkedHashMap<String,Integer> saved=remembered(encoded),result=new LinkedHashMap<>();
        JSONArray groups=groups(entry);
        for(int i=0;i<groups.length();++i) {
            JSONObject group=groups.getJSONObject(i);String id=group.getString("id");
            int value=saved.containsKey(id)?saved.remove(id):tick(group,"default");
            if(!accepts(group,value)) throw new IOException("滑条数值不符合包内范围或步长："+group.getString("name"));
            result.put(id,value);
        }
        if(!saved.isEmpty()) throw new IOException("滑条已从包中移除");
        return result;
    }

    /** Preserve unknown sliders separately across package upgrades/downgrades. */
    static void restore(JSONObject entry,String saved) throws Exception {
        LinkedHashMap<String,Integer> remembered=remembered(saved),current=new LinkedHashMap<>();
        JSONArray groups=groups(entry);
        for(int i=0;i<groups.length();++i) {
            JSONObject group=groups.getJSONObject(i);String id=group.getString("id");
            int value=remembered.containsKey(id)?remembered.get(id):tick(group,"default");
            if(!accepts(group,value)) value=tick(group,"default");
            current.put(id,value);remembered.put(id,value);
        }
        if(groups.length()==0 && remembered.isEmpty() && !entry.has("selected_parameters") && !entry.has("remembered_parameters")) return;
        entry.put("selected_parameters",encode(current));
        entry.put("remembered_parameters",encode(remembered));
    }

    static void select(JSONObject entry,String encoded) throws Exception {
        LinkedHashMap<String,Integer> current=parse(entry,encoded);
        LinkedHashMap<String,Integer> remembered=remembered(entry.optString("remembered_parameters",""));
        remembered.putAll(current);
        entry.put("selected_parameters",encode(current));entry.put("remembered_parameters",encode(remembered));
    }

    static String encode(Map<String,Integer> values) {
        StringBuilder encoded=new StringBuilder();
        for(Map.Entry<String,Integer> value:values.entrySet()) {
            if(encoded.length()>0) encoded.append('&');
            encoded.append(value.getKey()).append(':').append(value.getValue());
        }
        return encoded.toString();
    }

    static boolean available(JSONObject entry,JSONObject parameter,Map<String,String> options) throws Exception {
        return BemOptions.test(parameter.opt("available_when"),BemOptions.effective(entry,options));
    }

    static LinkedHashMap<String,Integer> effective(JSONObject entry,Map<String,Integer> saved,Map<String,String> options) throws Exception {
        LinkedHashMap<String,Integer> result=new LinkedHashMap<>();JSONArray groups=groups(entry);
        for(int i=0;i<groups.length();++i) {
            JSONObject group=groups.getJSONObject(i);String id=group.getString("id");
            result.put(id,available(entry,group,options)?saved.get(id):tick(group,"neutral"));
        }
        return result;
    }
}
