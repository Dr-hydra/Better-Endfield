package dev.betterendfield.next;

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
                .put("package_id","test."+character).put("bytes",4).put("bem_minor",0)
                .put("appearances",new JSONArray().put("default").put("alternate"))
                .put("default_appearance","default").put("selected_appearance","alternate");
    }
    private static JSONObject find(JSONArray entries,String id) throws Exception {
        for(int i=0;i<entries.length();i++) if(id.equals(entries.getJSONObject(i).getString("generation"))) return entries.getJSONObject(i);
        return null;
    }

    private static JSONObject resourceEntry(String kind,String owner,String platform,String resource,boolean enabled) throws Exception {
        JSONObject value=entry(owner,enabled).put("bem_minor",4).put("target_kind",kind).put("target_id",owner)
                .put("package_id","test."+UUID.randomUUID()).put("resource_keys",new JSONArray().put(platform+":"+resource))
                .put("option_groups",new JSONArray()).put("default_options","").put("selected_options","")
                .put("parameters",new JSONArray()).put("default_parameters","");
        value.remove("character_id");return value;
    }

    private static void resourceTargets() throws Exception {
        JSONObject normal=entry("zhuangfy",true).put("resource_keys",new JSONArray()
                .put("windows-x64:normal").put("android-arm64:normal").put("windows-x64:ui").put("android-arm64:ui"));
        JSONObject ultimate=resourceEntry("character","zhuangfy","android-arm64","ultimate",true);
        JSONObject weapon=resourceEntry("weapon","zhuangfy","android-arm64","sword",true);
        JSONObject android=resourceEntry("character","zhuangfy","android-arm64","another_form",true);
        ultimate.getJSONArray("resource_keys").put("windows-x64:shared_windows");
        weapon.getJSONArray("resource_keys").put("windows-x64:shared_windows");
        JSONArray all=new JSONArray().put(normal).put(ultimate).put(weapon).put(android);
        String original=all.toString();
        JSONArray normalized=BemOptions.exclusive(all);
        for(int i=0;i<normalized.length();++i) check(normalized.getJSONObject(i).getBoolean("enabled"),"Distinct resource scopes must coexist");
        check(original.equals(all.toString()),"Target normalization mutated source");
        JSONObject overlap=resourceEntry("character","other","android-arm64","ultimate",true);
        JSONArray installed=BemInstaller.installedIndex(all,overlap,null);
        check(!find(installed,ultimate.getString("generation")).getBoolean("enabled"),"Overlap with another owner was not disabled");
        check(find(installed,normal.getString("generation")).getBoolean("enabled"),"Normal model disabled by ultimate");
        check(find(installed,weapon.getString("generation")).getBoolean("enabled"),"Weapon disabled by ultimate");
        check(find(installed,android.getString("generation")).getBoolean("enabled"),"Another platform disabled by ultimate");
        JSONObject oldIndex=entry("zhuangfy",true);
        check(BemOptions.conflicts(oldIndex,ultimate),"Legacy index without resources must conflict conservatively");
        check(!BemOptions.conflicts(oldIndex,weapon),"Owner ID must include target kind");
        JSONObject otherLegacy=entry("zhuangfy",true).put("package_id","test.otherlegacy")
                .put("resource_keys",new JSONArray().put("windows-x64:different_world").put("windows-x64:different_ui"));
        check(BemOptions.conflicts(normal,otherLegacy),"Two legacy packages must retain same-owner exclusivity even with disjoint roots");
        JSONObject updated=resourceEntry("character","zhuangfy","android-arm64","different_root",true)
                .put("package_id",ultimate.getString("package_id"));
        check(BemOptions.conflicts(updated,ultimate),"Same package ID generations must not be enabled together");
        JSONObject malformed=new JSONObject(ultimate.toString());malformed.remove("resource_keys");
        rejects(()->BemOptions.exclusive(new JSONArray().put(malformed)),"Missing v1.4 resources accepted");
        JSONObject duplicate=new JSONObject(ultimate.toString()).put("resource_keys",new JSONArray()
                .put("windows-x64:ultimate").put("windows-x64:ultimate"));
        rejects(()->BemOptions.exclusive(new JSONArray().put(duplicate)),"Duplicate resource accepted");
        JSONObject invalid=new JSONObject(ultimate.toString()).put("resource_keys",new JSONArray().put("windows-x64:../bad"));
        rejects(()->BemOptions.exclusive(new JSONArray().put(invalid)),"Unsafe resource key accepted");
        JSONObject unknown=new JSONObject(ultimate.toString()).put("target_kind","unknown");
        rejects(()->BemOptions.exclusive(new JSONArray().put(unknown)),"Unknown target kind accepted");
        JSONObject converted=new JSONObject(ultimate.toString()).put("generation",UUID.randomUUID().toString());
        JSONArray conversion=BemInstaller.installedIndex(all,converted,ultimate.getString("generation"));
        check(find(conversion,normal.getString("generation")).getBoolean("enabled") &&
                find(conversion,converted.getString("generation")).getBoolean("enabled"),"Conversion lost disjoint enabled state");
        converted.put("resource_keys",new JSONArray().put("android-arm64:changed"));
        rejects(()->BemInstaller.installedIndex(all,converted,ultimate.getString("generation")),"Conversion changed resource identity");
        JSONObject changedKind=new JSONObject(ultimate.toString()).put("generation",UUID.randomUUID().toString()).put("target_kind","weapon");
        rejects(()->BemInstaller.installedIndex(all,changedKind,null),"Same package ID imported with another target kind");
        JSONObject changedOwner=new JSONObject(ultimate.toString()).put("generation",UUID.randomUUID().toString()).put("target_id","other");
        rejects(()->BemInstaller.installedIndex(all,changedOwner,null),"Same package ID imported with another target owner");
        JSONObject windowsOnly=resourceEntry("character","zhuangfy","windows-x64","ultimate",true);
        rejects(()->BemInstaller.installedIndex(all,windowsOnly,null),"Windows-only draft imported on Android");
        check(!BemOptions.exclusive(new JSONArray().put(windowsOnly)).getJSONObject(0).getBoolean("enabled"),"Windows-only persisted enabled flag was advertised as active");
        rejects(()->OverlayWritePolicy.apply(new JSONArray().put(windowsOnly),new JSONArray().put(change(windowsOnly,"enabled",true))),"Windows-only draft enabled through overlay");
        App app=new App();app.seed(all);
        BemInstaller.saveAll(app,new JSONArray().put(change(ultimate,"enabled",true)));
        JSONArray saved=BemInstaller.index(app);
        for(int i=0;i<saved.length();++i) check(saved.getJSONObject(i).getBoolean("enabled"),"Saved selection lost disjoint target");
        check(original.equals(all.toString()),"Install or conversion modified source metadata");
        System.out.println("PASS BEM 1.4 resource targets, owner kinds, platform overlap, legacy index fallback and conversion identity");
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
    private static JSONObject parameterEntry(String character) throws Exception {
        JSONObject value=optionsEntry(character).put("bem_minor",3);
        value.put("parameters",new JSONArray("[{\"id\":\"size\",\"name\":\"Size\",\"min\":0,\"max\":1000,\"step\":10,\"default\":500,\"neutral\":0},{\"id\":\"trim_size\",\"name\":\"Trim size\",\"min\":100,\"max\":900,\"step\":20,\"default\":500,\"neutral\":100,\"available_when\":{\"eq\":[\"body\",\"on\"]}}]"));
        value.put("default_parameters","size:500&trim_size:500");
        return value;
    }
    private static void continuousParameters() throws Exception {
        App app=new App();JSONObject value=parameterEntry("shape");app.seed(new JSONArray().put(value));
        JSONObject current=BemInstaller.index(app).getJSONObject(0);
        check(current.getString("selected_parameters").equals("size:500&trim_size:500"),"BEM 1.3 defaults did not persist");
        int commits=app.prefs.commits;BemInstaller.index(app);check(commits==app.prefs.commits,"BEM 1.3 migration is not idempotent");
        BemInstaller.saveAll(app,new JSONArray().put(change(value,"parameters","trim_size:740&size:820")).put(change(value,"options","body:off&trim:red")));
        current=BemInstaller.index(app).getJSONObject(0);
        check(current.getString("selected_parameters").equals("size:820&trim_size:740"),"Parameter selection was not canonical/atomic with options");
        java.util.Map<String,Integer> saved=BemParameters.parse(current,current.getString("selected_parameters"));
        java.util.Map<String,Integer> effective=BemParameters.effective(current,saved,BemOptions.parse(current,current.getString("selected_options")));
        check(effective.get("trim_size")==100 && saved.get("trim_size")==740,"Hidden parameter did not use neutral/preserve saved value");
        String before=app.prefs.value;
        for(String invalid:new String[]{"size:821","size:1001","size:-1","size:0.5","size:20&size:30","missing:100"})
            rejects(()->BemInstaller.saveAll(app,new JSONArray().put(change(value,"parameters",invalid))),"Accepted invalid BEM 1.3 parameter "+invalid);
        check(before.equals(app.prefs.value),"Rejected parameter changed persisted snapshot");
        JSONObject downgrade=optionsEntry("shape");
        JSONArray downgraded=BemInstaller.installedIndex(BemInstaller.index(app),downgrade,value.getString("generation"));
        check(downgraded.getJSONObject(0).getString("selected_parameters").isEmpty(),"Downgrade advertised unsupported parameters");
        check(downgraded.getJSONObject(0).getString("remembered_parameters").equals("size:820&trim_size:740"),"Downgrade lost remembered parameters");
        JSONObject upgrade=parameterEntry("shape");
        JSONArray upgraded=BemInstaller.installedIndex(downgraded,upgrade,downgrade.getString("generation"));
        check(upgraded.getJSONObject(0).getString("selected_parameters").equals("size:820&trim_size:740"),"Upgrade did not restore parameter values");
        JSONArray reimported=BemInstaller.installedIndex(upgraded,parameterEntry("shape"),null);
        check(reimported.getJSONObject(reimported.length()-1).getString("selected_parameters").equals("size:820&trim_size:740"),"Same-package reimport discarded slider values");
        JSONObject resized=parameterEntry("shape");resized.getJSONArray("parameters").getJSONObject(0).put("max",600);
        JSONArray adapted=BemInstaller.installedIndex(upgraded,resized,upgrade.getString("generation"));
        check(adapted.getJSONObject(0).getString("selected_parameters").equals("size:500&trim_size:740"),"Range change did not repair only invalid parameter");
        JSONObject malformed=parameterEntry("shape");malformed.getJSONArray("parameters").getJSONObject(0).put("step",0);
        rejects(()->BemParameters.parse(malformed,""),"Accepted zero step metadata");
        malformed.getJSONArray("parameters").getJSONObject(0).put("step",10).put("min",0.0);
        rejects(()->BemParameters.parse(malformed,""),"Coerced floating point metadata ticks");
        App game=new App();java.util.List<String> opened=new java.util.ArrayList<>();
        BemInstalledResources.Source source=name->{opened.add(name);return new ByteArrayInputStream(new byte[4]);};
        JSONObject first=upgraded.getJSONObject(0),legacy=entry("legacy",true);
        String runtime=BemInstalledResources.prepare(game,new JSONArray().put(first).put(legacy).toString(),source,message->{},false,true,false);
        check(runtime.contains(";parameters=size:820&trim_size:740,;skip_validation="),"Runtime parameter slots did not align with legacy package");
        String next=BemInstalledResources.prepare(game,new JSONArray().put(new JSONObject(first.toString()).put("selected_parameters","size:800&trim_size:740")).put(legacy).toString(),source,message->{},false,true,false);
        check(!next.equals(runtime) && opened.size()==2,"Parameter-only update recopied immutable generations or left configuration unchanged");
        JSONObject corrupt=new JSONObject(first.toString()).put("selected_parameters","size:821");
        rejects(()->BemInstalledResources.prepare(game,new JSONArray().put(corrupt).toString(),source,message->{},true,true,false),"Skip validation bypassed parameter wire validation");
        check(opened.size()==2,"Invalid parameter opened payload before validation");
        byte[] original=Files.readAllBytes(new File(game.root,"betterendfieldnext/installed-models/"+first.getString("generation")+".bem").toPath());
        check(java.util.Arrays.equals(original,new byte[4]),"Runtime materialization rewrote package payload");
        System.out.println("PASS BEM 1.3 defaults, atomic saves, hidden weights, upgrades, runtime payload preservation and selection updates");
    }
    private static void realShapePayload(String path) throws Exception {
        byte[] bytes=Files.readAllBytes(java.nio.file.Path.of(path));
        java.nio.ByteBuffer header=java.nio.ByteBuffer.wrap(bytes).order(java.nio.ByteOrder.LITTLE_ENDIAN);
        check(bytes.length>=40 && header.getShort(8)==1 && header.getShort(10)==3,"Expected real BEM 1.3 fixture header");
        int manifestSize=Math.toIntExact(header.getLong(24));
        JSONObject manifest=new JSONObject(new String(bytes,40,manifestSize,java.nio.charset.StandardCharsets.UTF_8));
        JSONObject entry=parameterEntry(manifest.getJSONObject("target").getString("character_id"));
        entry.put("package_id",manifest.getString("package_id")).put("name",manifest.getString("name")).put("bytes",bytes.length)
             .put("option_groups",manifest.getJSONArray("option_groups")).put("parameters",manifest.getJSONArray("parameters"))
             .put("selection_constraints",manifest.optJSONArray("selection_constraints"));
        entry.put("default_options",BemOptions.encode(BemOptions.parse(entry,"")));
        entry.put("default_parameters",BemParameters.encode(BemParameters.parse(entry,"")));
        App app=new App();app.seed(new JSONArray().put(entry));JSONObject installed=BemInstaller.index(app).getJSONObject(0);
        java.util.Map<String,Integer> weights=BemParameters.parse(installed,installed.getString("selected_parameters"));
        JSONObject parameter=installed.getJSONArray("parameters").getJSONObject(0);String id=parameter.getString("id");
        weights.put(id,BemParameters.snap(parameter,500));
        BemInstaller.saveAll(app,new JSONArray().put(change(installed,"parameters",BemParameters.encode(weights))));
        String config=BemInstalledResources.prepare(app,BemInstaller.index(app).toString(),name->new ByteArrayInputStream(bytes),message->{},false,true,false);
        check(config.contains(";parameters="+BemParameters.encode(weights)),"Real package selection not handed to native configuration");
        File copied=new File(app.root,"betterendfieldnext/installed-models/"+installed.getString("generation")+".bem");
        check(java.util.Arrays.equals(bytes,Files.readAllBytes(copied.toPath())),"Materialization altered real 1.3 manifest or deformation payload");
        System.out.println("PASS real BEM 1.3 manifest/deformation bytes preserved through installed index and private materialization ("+bytes.length+" bytes)");
    }
    public static void main(String[] args) {
        try {coexistenceAndConversion();migrationAndImmediateSave();componentValidation();continuousParameters();runtimeDefenseAndRemoval();resourceTargets();if(args.length>0)realShapePayload(args[0]);System.out.println("PASS "+checks+" BEM package state checks");System.exit(0);}
        catch(Throwable error){error.printStackTrace();System.exit(1);}
    }
}
