package dev.betterendfield.android;

import android.content.Context;
import android.content.SharedPreferences;
import java.lang.reflect.Proxy;
import java.util.HashMap;
import java.util.Map;

/** Host-only execution of production ModuleSettings/FrameworkSettings with isolated preferences. */
public final class ModuleSettingsFovTest {
    private static final Map<String, Object> values = new HashMap<>();
    private static boolean failNextCommit;
    public static void main(String[] args) throws Exception {
        SharedPreferences prefs = (SharedPreferences) Proxy.newProxyInstance(ModuleSettingsFovTest.class.getClassLoader(),
                new Class<?>[]{SharedPreferences.class}, (proxy, method, arguments) -> {
                    String name = method.getName();
                    if (name.equals("getAll")) return new HashMap<>(values);
                    if (name.equals("edit")) return editor();
                    if (name.startsWith("get")) return values.getOrDefault(arguments[0], arguments[1]);
                    if (name.equals("contains")) return values.containsKey(arguments[0]);
                    throw new AssertionError("Unexpected preference call: " + name);
                });
        Context context = new Context(prefs);
        values.put("camera_free_enabled", true);
        values.put("camera_field_of_view", "83.25");
        values.put("camera_movement_speed", "7.125");
        values.put("camera_first_person_fov", "92");
        values.put("camera_follow_character", true);
        values.put("unrelated", "keep");
        values.put(BemInstaller.INDEX, "[]");
        ModuleSettings.setGlobalFieldOfView(context, 5);
        ModuleSettings.setGlobalFovEnabled(context, true);
        check("5".equals(values.get("camera_global_fov")), "minimum FOV");
        check(Boolean.TRUE.equals(values.get("camera_global_fov_enabled")), "global switch");
        String config = (String) values.get(ModuleSettings.CAMERA_CONFIGURATION);
        check(config.contains("global_fov=5\n") && config.contains("global_fov_enabled=true\n"), "camera configuration publication");
        check(config.contains("field_of_view=83.25\n") && config.contains("first_person_fov=92\n"), "independent FOV configuration");
        check("83.25".equals(values.get("camera_field_of_view")) && "7.125".equals(values.get("camera_movement_speed")), "free camera keys rewritten");
        check("keep".equals(values.get("unrelated")) && "[]".equals(values.get(BemInstaller.INDEX)), "unrelated preferences rewritten");
        ModuleSettings.setGlobalFieldOfView(context, 150);
        check("150".equals(values.get("camera_global_fov")), "maximum FOV");
        Map<String, Object> before = new HashMap<>(values);
        failNextCommit = true;
        try { ModuleSettings.saveGlobalFov(context, false, 70.0); throw new AssertionError("Failed commit accepted"); }
        catch (java.io.IOException expected) {}
        check(before.equals(values), "optimistic in-memory commit was not rolled back");
        values.remove("camera_global_fov"); values.remove("camera_global_fov_enabled"); values.remove(ModuleSettings.CAMERA_CONFIGURATION);
        before = new HashMap<>(values); failNextCommit = true;
        try { ModuleSettings.saveGlobalFov(context, true, 60.0); throw new AssertionError("Failed first commit accepted"); }
        catch (java.io.IOException expected) {}
        check(before.equals(values), "rollback created previously absent keys");
        System.out.println("PASS global FOV persistence: same store, bounds, independent camera fields and failed-commit rollback");
    }
    private static SharedPreferences.Editor editor() {
        Map<String, Object> pending = new HashMap<>();
        return (SharedPreferences.Editor) Proxy.newProxyInstance(ModuleSettingsFovTest.class.getClassLoader(),
                new Class<?>[]{SharedPreferences.Editor.class}, (proxy, method, arguments) -> {
                    String name = method.getName();
                    if (name.startsWith("put")) { pending.put((String) arguments[0], arguments[1]); return proxy; }
                    if (name.equals("remove")) { pending.put((String) arguments[0], null); return proxy; }
                    if (name.equals("commit")) {
                        pending.forEach((key, value) -> { if (value == null) values.remove(key); else values.put(key, value); });
                        boolean success = !failNextCommit; failNextCommit = false; return success;
                    }
                    throw new AssertionError("Unexpected editor call: " + name);
                });
    }
    private static void check(boolean value, String message) { if (!value) throw new AssertionError(message); }
}
