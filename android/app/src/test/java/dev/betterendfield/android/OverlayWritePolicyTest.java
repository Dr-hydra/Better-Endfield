package dev.betterendfield.android;

import org.json.JSONArray;
import org.json.JSONObject;

/** Host regressions for the production boundary and the installer's shared patch implementation. */
public final class OverlayWritePolicyTest {
    private static final String A = "11111111-1111-4111-8111-111111111111", B = "22222222-2222-4222-8222-222222222222";
    interface Attempt { void run() throws Exception; }
    public static void main(String[] args) throws Exception {
        String token = "a".repeat(64), wrong = "b".repeat(64);
        check(OverlayWritePolicy.validToken(OverlayWritePolicy.newToken()), "256-bit random token format");
        check(!OverlayWritePolicy.newToken().equals(OverlayWritePolicy.newToken()), "token reused across fresh authorizations");
        check(OverlayWritePolicy.callerAllowed(12001, 12001, null, null, null), "owner preview needs no token");
        check(OverlayWritePolicy.callerAllowed(12002, 12001, new String[]{"com.hypergryph.endfield"}, token, token), "CN game with scoped token");
        check(OverlayWritePolicy.callerAllowed(12002, 12001, new String[]{"com.gryphline.endfield.gp"}, token, token), "global game with scoped token");
        check(OverlayWritePolicy.callerAllowed(12002, 12001, new String[]{"com.future.channel.endfield"}, token, token), "unknown channel with scoped token");
        check(OverlayWritePolicy.callerAllowed(112002, 112001, new String[]{"com.future.channel.endfield"}, token, token), "authorized work-profile channel");
        check(!OverlayWritePolicy.callerAllowed(12003, 12001, new String[]{"ordinary.app"}, null, token), "unauthorized UID without token");
        check(!OverlayWritePolicy.callerAllowed(12003, 12001, new String[]{"ordinary.unity.app"}, wrong, token), "Unity package is not authorization");
        check(!OverlayWritePolicy.callerAllowed(12002, 12001, new String[]{"com.hypergryph.endfield"}, null, token), "known package cannot bypass scope token");
        check(!OverlayWritePolicy.callerAllowed(12002, 12001, new String[]{"com.future.channel.endfield"}, wrong, token), "incorrect token");
        check(!OverlayWritePolicy.callerAllowed(12002, 12001, new String[]{"com.future.channel.endfield"}, "", ""), "uninitialized token is not authorization");
        check(!OverlayWritePolicy.callerAllowed(12002, 12001, new String[]{"com.future.channel.endfield"}, token, null), "owner secret unavailable");
        check(!OverlayWritePolicy.callerAllowed(12002, 12001, new String[]{"com.future.channel.endfield"}, token.substring(1), token), "truncated token");
        check(!OverlayWritePolicy.callerAllowed(112002, 12001, new String[]{"com.future.channel.endfield"}, token, token), "cross-user UID despite valid token");
        check(!OverlayWritePolicy.callerAllowed(12002, 12001, new String[]{"com.future.channel.endfield", "attacker"}, token, token), "shared UID despite valid token");
        check(!OverlayWritePolicy.callerAllowed(0, 12001, new String[]{"com.future.channel.endfield"}, token, token), "privileged caller");
        check(!OverlayWritePolicy.callerAllowed(100001, 112001, new String[]{"android"}, token, token), "non-app work-profile UID");
        check(!OverlayWritePolicy.callerAllowed(12002, 12001, null, token, token), "unresolved UID");
        check(!OverlayWritePolicy.callerAllowed(12002, 12001, new String[]{}, token, token), "UID without installed package");
        check(!OverlayWritePolicy.callerAllowed(12002, 12001, new String[]{null}, token, token), "invalid resolved package");
        check(!OverlayWritePolicy.callerAllowed(12002, 12001, new String[]{""}, token, token), "empty resolved package");
        OverlayWritePolicy.requireRevision(OverlayWritePolicy.revision("[]"), "[]");
        rejected(() -> OverlayWritePolicy.requireRevision(OverlayWritePolicy.revision("[]"), "[{}]"));
        rejected(() -> OverlayWritePolicy.requireRevision(null, "[]"));
        rejected(() -> OverlayWritePolicy.patch("{\"generation\":\"../../pkg\",\"enabled\":true}", false));
        rejected(() -> OverlayWritePolicy.patch("{\"generation\":\"" + A + "\",\"enabled\":\"true\"}", false));
        rejected(() -> OverlayWritePolicy.patch("{\"generation\":\"" + A + "\",\"remote\":\"replacement.bem\"}", false));
        rejected(() -> OverlayWritePolicy.patch("{\"generation\":\"" + A + "\"}", false));
        rejected(() -> OverlayWritePolicy.patch("x".repeat(OverlayWritePolicy.MAX_PATCH + 1), false));
        rejected(() -> OverlayWritePolicy.patch("{\"value\":4}", true));
        rejected(() -> OverlayWritePolicy.patch("{\"value\":151}", true));
        rejected(() -> OverlayWritePolicy.patch("{\"value\":\"60\"}", true));
        rejected(() -> OverlayWritePolicy.patch("{\"camera_field_of_view\":60}", true));
        OverlayWritePolicy.patch("{\"value\":5,\"enabled\":true}", true);
        OverlayWritePolicy.patch("{\"value\":150}", true);
        JSONObject first = fixture(A, "same", false), second = fixture(B, "same", true), other = fixture("33333333-3333-4333-8333-333333333333", "other", true);
        JSONArray original = new JSONArray().put(first).put(second).put(other);
        String before = original.toString();
        JSONArray next = apply(original, A, "enabled", true);
        check(next.getJSONObject(0).getBoolean("enabled") && !next.getJSONObject(1).getBoolean("enabled"), "exclusive enable");
        check(other.toString().equals(next.getJSONObject(2).toString()), "unrelated character preserved");
        check(before.equals(original.toString()), "transaction mutated source");
        next = apply(next, A, "parameters", "shape:700");
        JSONObject saved = next.getJSONObject(0);
        check("body:on".equals(saved.getString("selected_options")), "parameter edit changed options");
        check(saved.getString("remembered_parameters").contains("removed:300"), "forgot unknown parameter");
        check(saved.getString("remembered_parameters").contains("shape:700"), "active parameter not remembered");
        check(saved.getJSONObject("future_metadata").getInt("keep") == 42 && saved.getString("remote").equals("bem-" + A + ".bem"), "metadata/identity preserved");
        next = apply(next, A, "options", "body:off");
        check("shape:700".equals(next.getJSONObject(0).getString("selected_parameters")), "option edit changed saved shape");
        var shape = BemParameters.parse(next.getJSONObject(0), "shape:700");
        var options = BemOptions.parse(next.getJSONObject(0), "body:off");
        check(BemParameters.effective(next.getJSONObject(0), shape, options).get("shape") == 0, "hidden shape not neutral");
        final JSONArray current = next;
        rejected(() -> apply(current, A, "parameters", "shape:701"));
        rejected(() -> apply(current, A, "options", "body:removed"));
        rejected(() -> apply(current, "44444444-4444-4444-8444-444444444444", "enabled", true));
        rejected(() -> apply(current, A, "appearance", "default"));
        String latest = current.toString();
        rejected(() -> OverlayWritePolicy.requireRevision(OverlayWritePolicy.revision(before), latest));
        // A batch fails before the installer reaches commitIndex, retaining all package states.
        rejected(() -> OverlayWritePolicy.apply(current, new JSONArray().put(new JSONObject().put("generation", B).put("enabled", true))
                .put(new JSONObject().put("generation", A).put("parameters", "shape:999"))));
        check(latest.equals(current.toString()), "failed batch changed committed snapshot");
        JSONArray disabled = OverlayWritePolicy.apply(current, new JSONArray()
                .put(new JSONObject().put("generation", A).put("enabled", false))
                .put(new JSONObject().put("generation", B).put("enabled", false))
                .put(new JSONObject().put("generation", other.getString("generation")).put("enabled", false)));
        for (int i = 0; i < disabled.length(); ++i) check(!disabled.getJSONObject(i).getBoolean("enabled"), "disable all");
        check("shape:700".equals(disabled.getJSONObject(0).getString("selected_parameters")), "disable discarded selections");
        JSONObject legacy = new JSONObject().put("generation", A).put("character_id", "legacy").put("bem_minor", 0)
                .put("appearances", new JSONArray().put("default").put("alt")).put("selected_appearance", "default");
        check("alt".equals(apply(new JSONArray().put(legacy), A, "appearance", "alt").getJSONObject(0).getString("selected_appearance")), "legacy appearance");
        System.out.println("PASS overlay write boundary: scoped token, channel/work-profile UID, revision, types, generation, exclusive selection and field retention");
    }
    private static JSONObject fixture(String generation, String character, boolean enabled) throws Exception {
        return new JSONObject().put("generation", generation).put("character_id", character).put("enabled", enabled).put("bem_minor", 3)
                .put("remote", "bem-" + generation + ".bem").put("future_metadata", new JSONObject().put("keep", 42))
                .put("selected_options", "body:on").put("selected_parameters", "shape:400").put("remembered_parameters", "shape:400&removed:300")
                .put("option_groups", new JSONArray().put(new JSONObject().put("id", "body").put("name", "Body").put("default", "on")
                    .put("choices", new JSONArray().put(new JSONObject().put("id", "on")).put(new JSONObject().put("id", "off")))))
                .put("parameters", new JSONArray().put(new JSONObject().put("id", "shape").put("name", "Shape").put("min", 0).put("max", 1000)
                    .put("step", 10).put("default", 400).put("neutral", 0).put("available_when", new JSONObject().put("eq", new JSONArray().put("body").put("on")))));
    }
    private static JSONArray apply(JSONArray entries, String generation, String field, Object value) throws Exception {
        JSONObject patch = new JSONObject().put("generation", generation).put(field, value);
        return OverlayWritePolicy.apply(entries, new JSONArray().put(OverlayWritePolicy.patch(patch.toString(), false)));
    }
    private static void rejected(Attempt attempt) throws Exception {
        try { attempt.run(); } catch (Exception expected) { return; }
        throw new AssertionError("Unsafe/stale request was accepted");
    }
    private static void check(boolean value, String message) { if (!value) throw new AssertionError(message); }
}
