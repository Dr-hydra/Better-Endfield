package dev.betterendfield.android;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.IOException;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.HashSet;

/** BEM 1.1 finite option selection; never executes source INI commands. */
final class BemOptions {
    private BemOptions() {}

    static LinkedHashMap<String,String> parse(JSONObject entry, String encoded) throws Exception {
        JSONArray groups=entry.getJSONArray("option_groups");
        LinkedHashMap<String,String> values=new LinkedHashMap<>();
        for(int i=0;i<groups.length();++i) {
            JSONObject group=groups.getJSONObject(i);
            values.put(group.getString("id"),group.getString("default"));
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
            JSONArray choices=group.getJSONArray("choices");boolean found=false;
            for(int j=0;j<choices.length();++j) found |= value.equals(choices.getJSONObject(j).getString("id"));
            if(!found) throw new IOException("选项已从包中移除："+group.getString("name"));
        }
        if(!valid(entry,values)) throw new IOException("此选项组合在包内不可达");
        return values;
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

    private static boolean test(Object condition, Map<String,String> active) throws Exception {
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
