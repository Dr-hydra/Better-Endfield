package dev.betterendfield.next;

import android.content.Context;
import org.json.JSONArray;
import org.json.JSONObject;
import java.io.File;
import java.nio.file.Files;

/** Host-only check of production updater preparation and startup experiment flags. */
public final class BemOverlayPreparationTest {
    public static void main(String[] args) throws Exception {
        File directory = new File(args[0]), installed = new File(directory, "betterendfieldnext/installed-models");
        if (!installed.isDirectory() && !installed.mkdirs()) throw new AssertionError("Cannot create fixture");
        String generation = "11111111-1111-4111-8111-111111111111";
        File existing = new File(installed, generation + ".bem"); Files.write(existing.toPath(), new byte[]{1});
        JSONObject entry = new JSONObject().put("generation", generation).put("remote", "bem-" + generation + ".bem")
                .put("package_id", "fixture").put("character_id", "same").put("enabled", true).put("bem_minor", 3).put("bytes", 1)
                .put("default_options", "body:on").put("selected_options", "body:on").put("default_parameters", "shape:400").put("selected_parameters", "shape:700")
                .put("option_groups", new JSONArray().put(new JSONObject().put("id", "body").put("name", "Body").put("default", "on")
                    .put("choices", new JSONArray().put(new JSONObject().put("id", "on")).put(new JSONObject().put("id", "off")))))
                .put("parameters", new JSONArray().put(new JSONObject().put("id", "shape").put("name", "Shape").put("min", 0).put("max", 1000)
                    .put("step", 10).put("default", 400).put("neutral", 0)));
        Context context = new Context(null, directory);
        BemInstalledResources.Source source = name -> { throw new AssertionError("Recopied an installed generation"); };
        String config = BemInstalledResources.prepare(context, new JSONArray().put(entry).toString(), source, value -> {}, true, true, true);
        check(config.contains("skip_validation=1;hot_switch=1;fast_loading=1;clone_support=0"), "latched developer mode dropped");
        String cloneConfig=BemInstalledResources.prepare(context,new JSONArray().put(entry).toString(),source,value->{},false,false,false,false,true);
        check(cloneConfig.contains("hot_switch=0;fast_loading=0;clone_support=1"), "clone support is not independent of hot switch");
        check(config.contains("options=body:on;parameters=shape:700"), "selections dropped");
        entry.put("selected_options", "body:off").put("selected_parameters", "shape:800");
        String updated = BemInstalledResources.prepare(context, new JSONArray().put(entry).toString(), source, value -> {}, true, true, true);
        check(updated.contains("packages=" + existing.getAbsolutePath()), "created a second model path");
        check(updated.contains("options=body:off;parameters=shape:800"), "selection update lost fields");
        entry.put("enabled", false);
        String empty = BemInstalledResources.prepare(context, new JSONArray().put(entry).toString(), source, value -> {}, true, true, true);
        check(empty.contains("packages=;appearances=;options=;parameters=;skip_validation=1;hot_switch=1"), "disable all lost runtime mode");
        check(existing.isFile() && existing.length() == 1, "deleted resident generation during update");
        resourceTargets(new File(directory, "bem14-preparation"));
        System.out.println("PASS BEM overlay preparation: unchanged resources, option/shape updates, empty selection and latched skip-validation");
    }

    private static void resourceTargets(File directory) throws Exception {
        File installed = new File(directory, "betterendfieldnext/installed-models");
        if (!installed.isDirectory() && !installed.mkdirs()) throw new AssertionError("Cannot create BEM 1.4 fixture");
        // Synthetic resource names exercise the manager contract, not live Android asset discovery.
        JSONObject normal = resourceEntry(installed, 1, "character", "zhuangfy", "body:on", "shape:400",
                "android-arm64:normal", "android-arm64:normal_ui");
        JSONObject ultimate = resourceEntry(installed, 2, "character", "zhuangfy", "body:off", "shape:700",
                "android-arm64:ultimate", "windows-x64:shared");
        JSONObject weapon = resourceEntry(installed, 3, "weapon", "zhuangfy", "body:on", "shape:800",
                "android-arm64:sword", "windows-x64:shared");
        JSONObject windowsOnly = resourceEntry(null, 4, "weapon", "other", "body:on", "shape:400",
                "windows-x64:android_unavailable");
        JSONArray entries = new JSONArray().put(normal).put(ultimate).put(weapon).put(windowsOnly);
        String original = entries.toString();
        Context context = new Context(null, directory);
        BemInstalledResources.Source source = name -> { throw new AssertionError("Unexpected copy of " + name); };
        String config = BemInstalledResources.prepare(context, original, source, value -> {}, true, true, true);
        checkConfig(config, installed, new JSONObject[]{normal, ultimate, weapon},
                "body:on,body:off,body:on", "shape:400,shape:700,shape:800");
        check(original.equals(entries.toString()), "BEM 1.4 preparation changed the source snapshot");

        // An overlapping Android root wins even under a different owner; Windows overlap has no effect.
        JSONObject replacement = resourceEntry(installed, 5, "character", "other", "body:off", "shape:900",
                "android-arm64:ultimate", "windows-x64:normal");
        entries.put(replacement);
        config = BemInstalledResources.prepare(context, entries.toString(), source, value -> {}, true, true, true);
        checkConfig(config, installed, new JSONObject[]{normal, weapon, replacement},
                "body:on,body:on,body:off", "shape:400,shape:800,shape:900");
        check(ultimate.getBoolean("enabled") && windowsOnly.getBoolean("enabled"), "Conflict filtering mutated stored enable flags");
        check(new File(installed, ultimate.getString("generation") + ".bem").isFile(), "Hot update deleted a resident form");

        JSONArray disabled = new JSONArray();
        for (int i = 0; i < entries.length(); ++i)
            disabled.put(new JSONObject().put("generation", entries.getJSONObject(i).getString("generation")).put("enabled", false));
        config = BemInstalledResources.prepare(context, OverlayWritePolicy.apply(entries, disabled).toString(), source,
                value -> {}, true, true, true);
        checkConfig(config, installed, new JSONObject[0], "", "");
        System.out.println("PASS BEM 1.4 overlay preparation: multi-resource character/forms/weapons, Android conflicts, Windows-only filtering and aligned selections");
    }

    private static JSONObject resourceEntry(File installed, int number, String kind, String owner,
            String options, String parameters, String... resources) throws Exception {
        String generation = String.format(java.util.Locale.ROOT, "%08d-1111-4111-8111-111111111111", number);
        if (installed != null) Files.write(new File(installed, generation + ".bem").toPath(), new byte[]{(byte) number});
        return new JSONObject().put("generation", generation).put("remote", "bem-" + generation + ".bem")
                .put("package_id", "fixture." + number).put("bem_minor", 4).put("target_kind", kind).put("target_id", owner)
                .put("resource_keys", new JSONArray(resources)).put("enabled", true).put("bytes", 1)
                .put("default_options", "body:on").put("selected_options", options)
                .put("default_parameters", "shape:400").put("selected_parameters", parameters)
                .put("option_groups", new JSONArray().put(new JSONObject().put("id", "body").put("name", "Body").put("default", "on")
                    .put("choices", new JSONArray().put(new JSONObject().put("id", "on")).put(new JSONObject().put("id", "off")))))
                .put("parameters", new JSONArray().put(new JSONObject().put("id", "shape").put("name", "Shape").put("min", 0).put("max", 1000)
                    .put("step", 10).put("default", 400).put("neutral", 0)));
    }

    private static void checkConfig(String actual, File installed, JSONObject[] active, String options, String parameters) throws Exception {
        String[] paths = new String[active.length], appearances = new String[active.length];
        for (int i = 0; i < active.length; ++i) {
            paths[i] = new File(installed, active[i].getString("generation") + ".bem").getAbsolutePath();
            appearances[i] = "";
        }
        String expected = "resource=auto;replace=1;lod_pipeline=1;lod_npc=1;packages=" + String.join(",", paths)
                + ";appearances=" + String.join(",", appearances) + ";options=" + options + ";parameters=" + parameters
                + ";skip_validation=1;hot_switch=1;fast_loading=1;clone_support=0";
        check(expected.equals(actual), "BEM 1.4 runtime configuration differs: " + actual);
    }
    private static void check(boolean value, String message) { if (!value) throw new AssertionError(message); }
}
