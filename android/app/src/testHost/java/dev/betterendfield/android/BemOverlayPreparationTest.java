package dev.betterendfield.android;

import android.content.Context;
import org.json.JSONArray;
import org.json.JSONObject;
import java.io.File;
import java.nio.file.Files;

/** Host-only check of production updater preparation and startup experiment flags. */
public final class BemOverlayPreparationTest {
    public static void main(String[] args) throws Exception {
        File directory = new File(args[0]), installed = new File(directory, "betterendfield/installed-models");
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
        check(config.contains("skip_validation=1;hot_switch=1;fast_loading=1"), "latched developer mode dropped");
        check(config.contains("options=body:on;parameters=shape:700"), "selections dropped");
        entry.put("selected_options", "body:off").put("selected_parameters", "shape:800");
        String updated = BemInstalledResources.prepare(context, new JSONArray().put(entry).toString(), source, value -> {}, true, true, true);
        check(updated.contains("packages=" + existing.getAbsolutePath()), "created a second model path");
        check(updated.contains("options=body:off;parameters=shape:800"), "selection update lost fields");
        entry.put("enabled", false);
        String empty = BemInstalledResources.prepare(context, new JSONArray().put(entry).toString(), source, value -> {}, true, true, true);
        check(empty.contains("packages=;appearances=;options=;parameters=;skip_validation=1;hot_switch=1"), "disable all lost runtime mode");
        check(existing.isFile() && existing.length() == 1, "deleted resident generation during update");
        System.out.println("PASS BEM overlay preparation: unchanged resources, option/shape updates, empty selection and latched skip-validation");
    }
    private static void check(boolean value, String message) { if (!value) throw new AssertionError(message); }
}
