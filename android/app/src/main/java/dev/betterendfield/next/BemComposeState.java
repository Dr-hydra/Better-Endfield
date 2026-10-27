package dev.betterendfield.next;

import org.json.*;
import java.util.*;

/** Shared editor operations; checkbox selection never changes enabled state. */
final class BemComposeState {
    final JSONObject plan,choices;
    private final Map<String,Integer> triangleCounts=new HashMap<>();
    BemComposeState(JSONObject plan,JSONObject choices){this.plan=plan;this.choices=choices;}
    List<JSONObject> groups() {
        List<JSONObject> groups=new ArrayList<>();
        for(String key:new String[]{"slots","atomic_items"})for(JSONObject group:objects(plan.optJSONArray(key)))groups.add(group);
        return groups;
    }
    static List<JSONObject> objects(JSONArray array){List<JSONObject> result=new ArrayList<>();if(array!=null)for(int i=0;i<array.length();i++){JSONObject item=array.optJSONObject(i);if(item!=null)result.add(item);}return result;}
    String choice(JSONObject group){return choices.optString(group.optString("id"),group.optString("default"));}
    boolean enabled(JSONObject group,JSONObject segment){return choice(group).equals("split")?choices.optString(segment.optString("id"),segment.optString("default")).equals("on"):choice(group).equals(segment.optString("source_id"));}
    void setEnabled(JSONObject group,Collection<JSONObject> targets,boolean enabled) throws JSONException {
        // Snapshot the existing whole-source choice before switching to split.
        for(JSONObject segment:objects(group.optJSONArray("segments")))choices.put(segment.getString("id"),enabled(group,segment)?"on":"off");
        choices.put(group.getString("id"),"split");
        for(JSONObject segment:targets)choices.put(segment.getString("id"),enabled?"on":"off");
    }
    int triangles(JSONObject segment) {
        if(!segment.has("body_region"))return segment.optInt("count")/3;
        Integer cached=triangleCounts.get(segment.optString("id"));if(cached!=null)return cached;
        Set<Integer> faces=new HashSet<>();JSONArray original=segment.optJSONArray("triangles");
        if(original!=null)for(int i=0;i<original.length();i++)faces.add(original.optInt(i));
        JSONObject edits=choices.optJSONObject("body_edits");edits=edits==null?null:edits.optJSONObject(segment.optString("body_draw"));
        if(edits!=null)for(Iterator<String> it=edits.keys();it.hasNext();) {
            String key=it.next();int face=Integer.parseInt(key);
            if(edits.optString(key).equals(segment.optString("body_region")))faces.add(face);else faces.remove(face);
        }
        triangleCounts.put(segment.optString("id"),faces.size());return faces.size();
    }
    LinkedHashMap<String,List<JSONObject>> bundles(JSONObject group) {
        LinkedHashMap<String,List<JSONObject>> result=new LinkedHashMap<>();
        for(JSONObject segment:objects(group.optJSONArray("segments"))) {
            String key=segment.optString("source_id")+"|"+segment.optString("body_region",segment.optJSONArray("option_bindings")==null?"[]":segment.optJSONArray("option_bindings").toString());
            result.computeIfAbsent(key,k->new ArrayList<>()).add(segment);
        }
        return result;
    }
    String label(JSONObject segment) {
        if(segment.has("body_region"))return BemComposeSession.bodyName(segment.optString("body_region"));
        List<String> labels=new ArrayList<>();for(JSONObject option:objects(segment.optJSONArray("option_bindings")))labels.add(option.optString("name"));
        return labels.isEmpty()?Text.of("固定部分"):String.join(" / ",labels);
    }
    JSONObject inspection(JSONObject group,Collection<JSONObject> selected) throws JSONException {
        JSONObject result=new JSONObject(choices.toString());result.put(group.getString("id"),"split");
        Set<String> ids=new HashSet<>();for(JSONObject segment:selected)ids.add(segment.getString("id"));
        for(JSONObject segment:objects(group.optJSONArray("segments")))result.put(segment.getString("id"),ids.contains(segment.getString("id"))?"on":"off");
        return result;
    }
}
