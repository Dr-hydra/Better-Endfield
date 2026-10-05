package dev.betterendfield.android;

import org.json.JSONArray;
import org.json.JSONObject;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.util.Iterator;
import java.util.Set;
import java.util.UUID;

/** Small, host-testable policy shared by the Binder boundary and installer. */
final class OverlayWritePolicy {
    static final int MAX_PATCH = 16 * 1024;
    static boolean callerAllowed(int caller, int owner, String[] packages, String supplied, String expected) {
        return caller == owner || (caller % 100000 >= 10000 && caller / 100000 == owner / 100000
                && packages != null && packages.length == 1 && packages[0] != null && !packages[0].isEmpty()
                && validToken(expected) && validToken(supplied)
                && MessageDigest.isEqual(expected.getBytes(StandardCharsets.US_ASCII), supplied.getBytes(StandardCharsets.US_ASCII)));
    }
    static boolean validToken(String token) {
        return token != null && token.matches("[a-f0-9]{64}");
    }
    static String newToken() {
        byte[] bytes = new byte[32]; new java.security.SecureRandom().nextBytes(bytes);
        StringBuilder token = new StringBuilder(64);
        for (byte b : bytes) token.append(String.format(java.util.Locale.ROOT, "%02x", b & 255));
        return token.toString();
    }
    static String revision(String value) {
        try {
            byte[] digest = MessageDigest.getInstance("SHA-256").digest(value.getBytes(StandardCharsets.UTF_8));
            StringBuilder result = new StringBuilder();
            for (byte b : digest) result.append(String.format(java.util.Locale.ROOT, "%02x", b & 255));
            return result.toString();
        } catch (java.security.NoSuchAlgorithmException impossible) { throw new AssertionError(impossible); }
    }
    static void requireRevision(String expected, String current) throws IOException {
        if (expected == null || !expected.equals(revision(current)))
            throw new IOException("设置已被更新，请重新选择");
    }
    static JSONObject patch(String text, boolean fov) throws Exception {
        if (text == null || text.length() > MAX_PATCH) throw new IOException("无效的设置请求");
        JSONObject patch = new JSONObject(text);
        Set<String> fields = fov ? Set.of("enabled", "value")
                : Set.of("generation", "enabled", "appearance", "options", "parameters");
        for (Iterator<String> keys = patch.keys(); keys.hasNext();) {
            String key = keys.next(); Object value = patch.get(key);
            if (!fields.contains(key) || (key.equals("enabled") ? !(value instanceof Boolean)
                    : key.equals("value") ? !(value instanceof Number) : !(value instanceof String)))
                throw new IOException("无效的设置字段");
        }
        if (fov) {
            if (patch.length() == 0) throw new IOException("缺少设置字段");
            if (patch.has("value")) {
                double value = patch.getDouble("value");
                if (!Double.isFinite(value) || value < 5 || value > 150) throw new IOException("FOV 超出范围");
            }
        } else {
            String generation = patch.getString("generation");
            if (!UUID.fromString(generation).toString().equals(generation) || patch.length() < 2)
                throw new IOException("无效的模型包版本");
        }
        return patch;
    }
    static JSONArray apply(JSONArray previous, JSONArray changes) throws Exception {
        JSONArray entries = new JSONArray(previous.toString());
        for (int i = 0; i < changes.length(); ++i) {
            JSONObject change = changes.getJSONObject(i), entry = null;
            for (int j = 0; j < entries.length(); ++j) {
                JSONObject candidate = entries.getJSONObject(j);
                if (change.getString("generation").equals(candidate.getString("generation"))) entry = candidate;
            }
            if (entry == null) throw new IOException("模型包列表已更新，请重新选择");
            if ((entry.optInt("bem_minor", 0) >= 1 && change.has("appearance"))
                    || (entry.optInt("bem_minor", 0) < 1 && change.has("options"))
                    || (entry.optInt("bem_minor", 0) < 3 && change.has("parameters")))
                throw new IOException("选项类型与模型包不匹配");
            if (change.has("options")) entry.put("selected_options", BemOptions.encode(BemOptions.parse(entry, change.getString("options"))));
            if (change.has("appearance")) entry.put("selected_appearance", BemOptions.appearance(entry, change.getString("appearance")));
            if (change.has("parameters")) BemParameters.select(entry, change.getString("parameters"));
            if (change.has("enabled")) {
                boolean enabled = change.getBoolean("enabled");
                if (enabled) BemOptions.requireAndroid(entry);
                if (enabled) for (int j = 0; j < entries.length(); ++j) {
                    JSONObject other = entries.getJSONObject(j);
                    if (BemOptions.conflicts(entry, other)) other.put("enabled", false);
                }
                entry.put("enabled", enabled);
            }
        }
        return entries;
    }
}
