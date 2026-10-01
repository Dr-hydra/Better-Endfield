package dev.betterendfield.android;

import android.content.Context;
import android.content.SharedPreferences;
import org.json.JSONArray;
import org.json.JSONObject;
import java.io.*;
import java.nio.file.Files;
import java.util.UUID;

/** Runs the production state/validation code with host filesystem and preference adapters. */
public final class BemPackageStateTest {
    private static int checks;
    private static void check(boolean value,String message) {checks++;if(!value) throw new AssertionError(message);}
    private interface Operation {void run() throws Exception;}
    private static void rejects(Operation operation,String message) throws Exception {
        try {operation.run();throw new AssertionError(message);} catch(IOException expected) {checks++;}
    }
    private static JSONObject entry(String character,boolean enabled) throws Exception {
        String generation=UUID.randomUUID().toString();
        return new JSONObject().put("generation",generation).put("remote","bem-"+generation+".bem")
                .put("character_id",character).put("enabled",enabled).put("name","Fixture")
                .put("package_id","test.package").put("bytes",4).put("bem_minor",0)
                .put("appearances",new JSONArray().put("default").put("alternate"))
                .put("default_appearance","default").put("selected_appearance","alternate");
    }
    private static JSONObject find(JSONArray entries,String id) throws Exception {
        for(int i=0;i<entries.length();i++) if(id.equals(entries.getJSONObject(i).getString("generation"))) return entries.getJSONObject(i);
        return null;
    }
    private static JSONObject optionsEntry(String character) throws Exception {
        JSONObject value=entry(character,true);value.put("bem_minor",1).remove("selected_appearance");
        value.put("default_options","body:on&trim:blue");
        value.put("option_groups",new JSONArray("[{\"id\":\"body\",\"name\":\"Body\",\"default\":\"on\",\"choices\":[{\"id\":\"on\",\"name\":\"On\"},{\"id\":\"off\",\"name\":\"Off\"}]},{\"id\":\"trim\",\"name\":\"Trim\",\"default\":\"blue\",\"available_when\":{\"eq\":[\"body\",\"on\"]},\"choices\":[{\"id\":\"blue\",\"name\":\"Blue\"},{\"id\":\"red\",\"name\":\"Red\"}]}]"));
        value.put("selection_constraints",new JSONArray("[{\"not\":{\"all\":[{\"eq\":[\"body\",\"on\"]},{\"eq\":[\"trim\",\"red\"]}]}}]"));
        value.put("selected_options","body:on&trim:blue");return value;
    }
    private static JSONObject change(JSONObject entry,String field,Object value) throws Exception {
        return new JSONObject().put("generation",entry.getString("generation")).put(field,value);
    }
    private static void coexistenceAndConversion() throws Exception {
        JSONObject first=entry("same",true),inactive=entry("same",false),other=entry("other",true),imported=entry("same",true);
        JSONArray previous=new JSONArray().put(first).put(inactive).put(other);
        String unchanged=previous.toString();
        JSONArray next=BemInstaller.installedIndex(previous,imported,null);
        check(next.length()==4,"Import discarded an installed package");
        check(previous.toString().equals(unchanged),"Merge mutated input before commit");
        check(!find(next,first.getString("generation")).getBoolean("enabled"),"Import did not disable old active package");
        check(find(next,inactive.getString("generation")).toString().equals(inactive.toString()),"Import changed inactive sibling selection");
        check(find(next,other.getString("generation")).toString().equals(other.toString()),"Import changed unrelated character");
        check(find(next,imported.getString("generation")).getString("selected_appearance").equals("default"),"Import did not use declared default");
        JSONObject converted=entry("same",true);
        JSONArray conversion=BemInstaller.installedIndex(previous,converted,inactive.getString("generation"));
        check(conversion.length()==3,"Conversion changed number of installed packages");
        check(find(conversion,inactive.getString("generation"))==null,"Conversion retained replaced generation in index");
        check(!find(conversion,converted.getString("generation")).getBoolean("enabled"),"Conversion enabled an inactive target");
        check(find(conversion,first.getString("generation")).getBoolean("enabled"),"Inactive conversion disabled active sibling");
        check(find(conversion,converted.getString("generation")).getString("selected_appearance").equals("alternate"),"Conversion lost saved appearance");
        JSONObject wrong=entry("wrong",true);
        rejects(()->BemInstaller.installedIndex(previous,wrong,inactive.getString("generation")),"Accepted changed conversion character");
        converted.put("appearances",new JSONArray().put("default"));
        rejects(()->BemInstaller.installedIndex(previous,converted,inactive.getString("generation")),"Conversion silently reset invalid choice");
        rejects(()->BemInstaller.installedIndex(previous,imported,UUID.randomUUID().toString()),"Converted a stale target");
        JSONArray activeConversion=BemInstaller.installedIndex(previous,entry("same",true),first.getString("generation"));
        check(activeConversion.length()==3 && !find(activeConversion,inactive.getString("generation")).getBoolean("enabled"),"Active conversion removed or enabled sibling");
        System.out.println("PASS coexistence and conversion boundaries");
    }
    private static final class Preferences implements SharedPreferences {
        String value="[]";boolean failNext;int commits;
        final File root;
        Preferences(File root){this.root=root;}
        public String getString(String key,String fallback){return value;}
        public Editor edit(){return new Editor(){
            String pending;
            public Editor putString(String key,String value){pending=value;return this;}
            public boolean commit(){
                // Match Android: listeners see the new memory value before disk success.
                commits++;value=pending;
                try {
                    check(value.equals(Files.readString(new File(root,"bem-index.json").toPath())),"Published a snapshot before it was durably saved");
                } catch(IOException error){throw new AssertionError(error);}
                boolean result=!failNext;failNext=false;return result;
            }
        };}
    }
    private static final class App extends Context {
        final File root;final Preferences prefs;
        App() throws IOException {root=Files.createTempDirectory("bem-state-test-").toFile();prefs=new Preferences(root);}
        public File getFilesDir(){return root;}
        public Context getApplicationContext(){return this;}
        public SharedPreferences getSharedPreferences(String name,int mode){return prefs;}
        void seed(JSONArray entries){prefs.value=entries.toString();}
    }
    private static void migrationAndImmediateSave() throws Exception {
        App app=new App();JSONObject old=entry("same",true),newer=entry("same",true),other=entry("other",true);
        old.remove("enabled");old.remove("selected_appearance");newer.put("selected_appearance","removed");
        app.seed(new JSONArray().put(old).put(newer).put(other));
        JSONArray migrated=BemInstaller.index(app);
        check(migrated.length()==3,"Migration discarded old installed entries");
        check(!migrated.getJSONObject(0).getBoolean("enabled") && migrated.getJSONObject(1).getBoolean("enabled"),"Migration did not resolve duplicate enabled packages");
        check(migrated.getJSONObject(1).getString("selected_appearance").equals("default"),"Migration did not persist repaired appearance");
        int commits=app.prefs.commits;BemInstaller.index(app);
        check(commits==app.prefs.commits,"Migration is not idempotent");
        BemInstaller.saveAll(app,new JSONArray().put(change(old,"enabled",true)));
        JSONArray enabled=BemInstaller.index(app);
        check(enabled.getJSONObject(0).getBoolean("enabled") && !enabled.getJSONObject(1).getBoolean("enabled"),"Enabling package did not immediately disable sibling");
        check(enabled.getJSONObject(2).toString().equals(other.toString()),"Switch changed unrelated package");
        BemInstaller.saveAll(app,new JSONArray().put(change(old,"appearance","alternate")));
        check(BemInstaller.index(app).getJSONObject(0).getString("selected_appearance").equals("alternate"),"Appearance was not saved immediately");
        String saved=app.prefs.value;
        rejects(()->BemInstaller.select(app,old.getString("generation"),"missing",true),"select bypassed validation");
        check(saved.equals(app.prefs.value),"Invalid appearance changed saved index");
        rejects(()->BemInstaller.saveAll(app,new JSONArray().put(change(old,"enabled",false)).put(change(newer,"appearance","missing"))),"Invalid batch was partly saved");
        check(saved.equals(app.prefs.value),"Invalid batch altered earlier package");
        JSONObject stale=new JSONObject().put("generation",UUID.randomUUID().toString()).put("enabled",true);
        rejects(()->BemInstaller.saveAll(app,new JSONArray().put(stale)),"Accepted stale generation");
        app.prefs.failNext=true;
        rejects(()->BemInstaller.saveAll(app,new JSONArray().put(change(newer,"enabled",true))),"Reported success for failed commit");
        check(saved.equals(app.prefs.value),"Failed commit left optimistic preference value");
        check(saved.equals(Files.readString(new File(app.root,"bem-index.json").toPath())),"Failed commit did not restore durable snapshot");
        BemInstaller.busy=true;
        try {rejects(()->BemInstaller.saveAll(app,new JSONArray().put(change(old,"enabled",false))),"Edited while worker owned transaction");}
        finally {BemInstaller.busy=false;}
        check(saved.equals(app.prefs.value),"Busy write changed preferences");
        rejects(()->BemInstaller.saveAll(app,new JSONArray().put(change(old,"options","body:on"))),"Silently ignored mismatched option type");
        App noDisk=new App();noDisk.seed(BemInstaller.index(app));
        new File(noDisk.root,"bem-index.json.new").mkdir();
        String before=noDisk.prefs.value;
        rejects(()->BemInstaller.saveAll(noDisk,new JSONArray().put(change(newer,"enabled",true))),"Advertised failed durable write");
        check(before.equals(noDisk.prefs.value) && noDisk.prefs.commits==0,"Published preferences after failed disk write");
        App failedMigration=new App();failedMigration.seed(new JSONArray().put(old).put(newer));failedMigration.prefs.failNext=true;
        String legacy=failedMigration.prefs.value;
        try {BemInstaller.index(failedMigration);throw new AssertionError("Exposed migration despite failed save");}
        catch(IllegalStateException expected){checks++;}
        check(legacy.equals(failedMigration.prefs.value),"Failed migration exposed optimistic normalized index");
        JSONObject malformed=entry("malformed",true).put("enabled","unknown");
        rejects(()->BemOptions.exclusive(new JSONArray().put(malformed)),"Defaulted corrupt enabled state to true");
        System.out.println("PASS migration, immediate saves, atomic rejection and failed commit rollback");
    }
    private static void componentValidation() throws Exception {
        App app=new App();JSONObject value=optionsEntry("same");app.seed(new JSONArray().put(value));
        BemInstaller.saveAll(app,new JSONArray().put(change(value,"options","body:off&trim:red")));
        check(BemInstaller.index(app).getJSONObject(0).getString("selected_options").equals("body:off&trim:red"),"Inactive choice was lost");
        String saved=app.prefs.value;
        rejects(()->BemInstaller.saveAll(app,new JSONArray().put(change(value,"options","body:on&trim:red"))),"Saved unreachable combination");
        check(saved.equals(app.prefs.value),"Invalid options changed stored configuration");
        rejects(()->BemOptions.parse(value,"body:on&body:off"),"Accepted duplicate group selection");
        rejects(()->BemOptions.parse(value,"body:unknown"),"Accepted removed choice");
        JSONObject converted=optionsEntry("same");
        JSONArray result=BemInstaller.installedIndex(BemInstaller.index(app),converted,value.getString("generation"));
        check(result.getJSONObject(0).getString("selected_options").equals("body:off&trim:red"),"Conversion lost component selections");
        JSONObject malformed=optionsEntry("same");
        malformed.getJSONArray("option_groups").put(malformed.getJSONArray("option_groups").getJSONObject(0));
        rejects(()->BemOptions.parse(malformed,malformed.getString("default_options")),"Accepted duplicate metadata groups");
        value.put("selected_options","body:on&trim:red");app.seed(new JSONArray().put(value));
        check(BemInstaller.index(app).getJSONObject(0).getString("selected_options").equals("body:on&trim:blue"),"Legacy invalid option selection was not durably repaired");
        System.out.println("PASS component constraints, saved choices and metadata validation");
    }
    private static void runtimeDefenseAndRemoval() throws Exception {
        App app=new App();JSONObject older=entry("same",true),newer=entry("same",true),other=entry("other",true);
        java.util.List<String> opened=new java.util.ArrayList<>();
        BemInstalledResources.Source source=name->{opened.add(name);return new ByteArrayInputStream(new byte[4]);};
        String config=BemInstalledResources.prepare(app,new JSONArray().put(older).put(newer).put(other).toString(),source,message->{});
        check(opened.size()==2 && !opened.contains(older.getString("remote")),"Runtime materialized two packages for one character");
        check(config.contains(newer.getString("generation")) && !config.contains(older.getString("generation")),"Runtime advertised duplicate character");
        JSONObject invalid=optionsEntry("bad").put("selected_options","body:on&trim:red");
        rejects(()->BemInstalledResources.prepare(app,new JSONArray().put(invalid).toString(),source,message->{}),"Runtime advertised unreachable options");
        check(opened.size()==2,"Runtime read invalid package before selection validation");
        JSONObject empty=optionsEntry("empty").put("option_groups",new JSONArray()).put("selection_constraints",new JSONArray()).put("default_options","").put("selected_options","");
        check(BemInstalledResources.prepare(app,new JSONArray().put(empty).toString(),source,message->{}).contains("options="),"Runtime rejected zero option groups");
        String stale=UUID.randomUUID().toString();File root=new File(app.root,"bem-installed");root.mkdirs();
        for(String id:new String[]{older.getString("generation"),newer.getString("generation"),other.getString("generation"),stale}) {
            File folder=new File(root,id);folder.mkdir();Files.writeString(new File(folder,"installed.bem").toPath(),"test");
            Files.writeString(new File(folder,"report.json").toPath(),"{\"character_id\":\"same\"}");
        }
        older.put("enabled",false);app.seed(new JSONArray().put(older).put(newer).put(other));
        BemInstaller.remove(app,older.getString("generation"));
        long deadline=System.currentTimeMillis()+5000;
        while(BemInstaller.busy && System.currentTimeMillis()<deadline) Thread.sleep(10);
        check(!BemInstaller.busy,"Removal worker did not finish");
        check(BemInstaller.index(app).length()==2,"Removal discarded sibling index entries");
        check(!new File(root,older.getString("generation")).exists(),"Removal did not delete target generation");
        check(new File(root,newer.getString("generation")+"/installed.bem").isFile(),"Removal deleted installed sibling");
        check(new File(root,other.getString("generation")+"/installed.bem").isFile(),"Removal deleted unrelated package");
        check(new File(root,stale+"/installed.bem").isFile(),"Removal swept unindexed same-character generation");
        System.out.println("PASS runtime duplicate/selection defenses and generation-only removal");
    }
    public static void main(String[] args) {
        try {coexistenceAndConversion();migrationAndImmediateSave();componentValidation();runtimeDefenseAndRemoval();System.out.println("PASS "+checks+" BEM package state checks");System.exit(0);}
        catch(Throwable error){error.printStackTrace();System.exit(1);}
    }
}
