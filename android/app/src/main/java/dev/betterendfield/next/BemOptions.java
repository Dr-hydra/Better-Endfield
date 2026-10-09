package dev.betterendfield.next;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.IOException;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.HashSet;
import java.util.ArrayList;
import java.util.Set;

/** BEM 1.1 finite option selection; never executes source INI commands. */
final class BemOptions {
    private BemOptions() {}

    static LinkedHashMap<String,String> parse(JSONObject entry, String encoded) throws Exception {
        JSONArray groups=entry.getJSONArray("option_groups");
        LinkedHashMap<String,String> values=new LinkedHashMap<>();
        for(int i=0;i<groups.length();++i) {
            JSONObject group=groups.getJSONObject(i);
            String id=group.getString("id"),value=group.getString("default");
            requireToken(id);requireToken(value);
            if(values.put(id,value)!=null) throw new IOException("重复的选项组");
        }
        HashSet<String> seen=new HashSet<>();
        if(!encoded.isEmpty()) for(String pair:encoded.split("&",-1)) {
            String[] parts=pair.split(":",-1);
            if(parts.length!=2 || !values.containsKey(parts[0]) || !seen.add(parts[0])) throw new IOException("无效的选项组");
            values.put(parts[0],parts[1]);
        }
        for(int i=0;i<groups.length();++i) {
            JSONObject group=groups.getJSONObject(i);
            String value=values.get(group.getString("id"));
            requireToken(value);
            JSONArray choices=group.getJSONArray("choices");boolean found=false;
            HashSet<String> choiceIds=new HashSet<>();
            for(int j=0;j<choices.length();++j) {
                String id=choices.getJSONObject(j).getString("id");requireToken(id);
                if(!choiceIds.add(id)) throw new IOException("重复的组件选项");
                found |= value.equals(id);
            }
            if(!found) throw new IOException("选项已从包中移除："+group.getString("name"));
        }
        if(!valid(entry,values)) throw new IOException("此选项组合在包内不可达");
        return values;
    }

    static void requireToken(String value) throws IOException {
        if(value==null || !value.matches("[A-Za-z0-9][A-Za-z0-9_.-]{0,95}")) throw new IOException("无效的选项标识");
    }

    static String appearance(JSONObject entry,String value) throws Exception {
        requireToken(value);
        JSONArray choices=entry.getJSONArray("appearances");
        for(int i=0;i<choices.length();++i) if(value.equals(choices.getString(i))) return value;
        throw new IOException("无效的外观选项");
    }

    static String targetKind(JSONObject entry) throws Exception {
        String kind=entry.optInt("bem_minor",0)>=4?entry.getString("target_kind"):entry.optString("target_kind","character");
        if(!kind.equals("character") && !kind.equals("weapon")) throw new IOException("无效的模型目标类别");
        return kind;
    }

    static String targetId(JSONObject entry) throws Exception {
        String id=entry.has("target_id")?entry.getString("target_id"):entry.getString("character_id");
        if(entry.optInt("bem_minor",0)>=4 && !entry.has("target_id")) throw new IOException("模型包缺少目标标识");
        requireToken(id);return id;
    }

    static String targetKey(JSONObject entry) throws Exception {return targetKind(entry)+":"+targetId(entry);}

    static String targetLabel(String key) {
        int split=key.indexOf(':');String id=split<0?key:key.substring(split+1);
        return key.startsWith("weapon:")?"武器 · "+id:id;
    }

    static Set<String> resourceKeys(JSONObject entry) throws Exception {
        Set<String> keys=new HashSet<>();
        if(!entry.has("resource_keys")) {
            if(entry.optInt("bem_minor",0)>=4) throw new IOException("模型包缺少资源目标");
            return keys; // Old installed indexes are normalized conservatively by owner.
        }
        JSONArray resources=entry.getJSONArray("resource_keys");
        if(resources.length()<1 || resources.length()>64) throw new IOException("无效的模型资源目标");
        for(int i=0;i<resources.length();++i) {
            String key=resources.getString(i);int split=key.indexOf(':');
            if(split<1) throw new IOException("无效的模型资源目标");
            String platform=key.substring(0,split),name=key.substring(split+1);requireToken(name);
            if(entry.optInt("bem_minor",0)>=4 && !name.equals(name.toLowerCase(java.util.Locale.ROOT)))
                throw new IOException("无效的模型资源目标");
            if((!platform.equals("windows-x64") && !platform.equals("android-arm64")) || !keys.add(key))
                throw new IOException("无效的模型资源目标");
        }
        return keys;
    }

    static boolean conflicts(JSONObject first,JSONObject second) throws Exception {
        if(first.has("package_id") && second.has("package_id") && first.getString("package_id").equals(second.getString("package_id"))) return true;
        if(first.optInt("bem_minor",0)<4 && second.optInt("bem_minor",0)<4 && targetKey(first).equals(targetKey(second))) return true;
        Set<String> a=resourceKeys(first),b=resourceKeys(second);
        if(a.isEmpty() || b.isEmpty()) return targetKey(first).equals(targetKey(second));
        for(String key:a) if(key.startsWith("android-arm64:") && b.contains(key)) return true;
        return false;
    }

    static boolean supportsAndroid(JSONObject entry) throws Exception {
        if(entry.optInt("bem_minor",0)<4) return true;
        for(String key:resourceKeys(entry)) if(key.startsWith("android-arm64:")) return true;
        return false;
    }

    static void requireAndroid(JSONObject entry) throws Exception {
        if(!supportsAndroid(entry)) throw new IOException("此模型包没有 Android 资源目标，无法在当前平台导入或启用");
    }

    /** Newest enabled entry wins for legacy/corrupt snapshots; keep every package. */
    static JSONArray exclusive(JSONArray entries) throws Exception {
        JSONArray result=new JSONArray(entries.toString());
        HashSet<String> generations=new HashSet<>();
        ArrayList<JSONObject> active=new ArrayList<>();
        for(int i=result.length()-1;i>=0;--i) {
            JSONObject entry=result.getJSONObject(i);
            if(!generations.add(entry.getString("generation"))) throw new IOException("重复的模型包版本");
            targetKey(entry);resourceKeys(entry);
            if(entry.has("enabled") && !(entry.get("enabled") instanceof Boolean)) throw new IOException("无效的启用状态");
            boolean enabled=entry.optBoolean("enabled",true) && supportsAndroid(entry);
            if(enabled) for(JSONObject other:active) if(conflicts(entry,other)) {enabled=false;break;}
            entry.put("enabled",enabled);
            if(enabled) active.add(entry);
        }
        return result;
    }

    static String encode(Map<String,String> values) {
        StringBuilder text=new StringBuilder();
        for(Map.Entry<String,String> item:values.entrySet()) {
            if(text.length()>0) text.append('&');
            text.append(item.getKey()).append(':').append(item.getValue());
        }
        return text.toString();
    }

    static LinkedHashMap<String,String> effective(JSONObject entry, Map<String,String> saved) throws Exception {
        LinkedHashMap<String,String> active=new LinkedHashMap<>();
        JSONArray groups=entry.getJSONArray("option_groups");
        for(int i=0;i<groups.length();++i) {
            JSONObject group=groups.getJSONObject(i);
            if(test(group.opt("available_when"),active))
                active.put(group.getString("id"),saved.get(group.getString("id")));
        }
        return active;
    }

    static boolean valid(JSONObject entry, Map<String,String> saved) throws Exception {
        Map<String,String> active=effective(entry,saved);
        JSONArray constraints=entry.optJSONArray("selection_constraints");
        if(constraints==null) return true;
        for(int i=0;i<constraints.length();++i) if(!test(constraints.get(i),active)) return false;
        return true;
    }

    static boolean test(Object condition, Map<String,String> active) throws Exception {
        if(condition==null || condition==JSONObject.NULL) return true;
        if(condition instanceof Boolean) return (Boolean)condition;
        JSONObject rule=(JSONObject)condition;
        if(rule.has("eq")) {
            JSONArray pair=rule.getJSONArray("eq");
            return pair.getString(1).equals(active.get(pair.getString(0)));
        }
        if(rule.has("all")) {
            JSONArray children=rule.getJSONArray("all");
            for(int i=0;i<children.length();++i) if(!test(children.get(i),active)) return false;
            return true;
        }
        if(rule.has("any")) {
            JSONArray children=rule.getJSONArray("any");
            for(int i=0;i<children.length();++i) if(test(children.get(i),active)) return true;
            return false;
        }
        if(rule.has("not")) return !test(rule.get("not"),active);
        throw new IOException("未知选项条件");
    }
}
