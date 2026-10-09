package dev.betterendfield.next;

import org.json.JSONArray;
import org.json.JSONObject;
import java.nio.file.Files;
import java.nio.file.Path;

/** Executes the production catalog state with failed/stale Binder replies. */
public final class OverlayModelCatalogStateTest {
    private static int checks;
    private static void check(boolean value, String message) {
        ++checks;
        if (!value) throw new AssertionError(message);
    }
    public static void main(String[] args) throws Exception {
        OverlayModelCatalogState state = new OverlayModelCatalogState();
        check(!state.hasCatalog() && !state.hasError() && !state.canWrite(), "initial placeholder became an authoritative catalog");
        check(state.revision().isEmpty() && state.message().contains("正在读取"), "initial state did not show loading");
        state.beginRead(1);
        check(state.fail(1, "设置桥不可用"), "current read failure was not accepted");
        check(!state.hasCatalog() && state.hasError() && !state.canWrite(), "failed initial read was reported as an empty catalog");
        check(state.message().contains("读取失败") && state.message().contains("设置桥不可用"), "bridge error was hidden");
        int failedVersion = state.version();
        state.beginRead(2);state.fail(2, "设置桥不可用");
        check(state.version() == failedVersion, "identical polling failures forced a view rebuild");

        state.beginRead(3);
        check(state.succeed(3, "[]", "revision-empty"), "successful empty catalog was rejected");
        check(state.hasCatalog() && !state.hasError() && state.canWrite() && state.index().equals("[]"),
                "true empty catalog remained an error or unavailable state");

        JSONArray entries = new JSONArray()
                .put(entry(1, "character", "chr_0004_pelica", "normal", true))
                .put(entry(2, "character", "chr_0004_pelica", "ultimate", false))
                .put(entry(3, "weapon", "sword_001", "sword", false));
        String catalog = entries.toString();
        state.beginRead(4);state.succeed(4, catalog, "revision-four");
        check(state.hasCatalog() && state.canWrite() && !state.hasError(), "successful retry did not restore editing");
        JSONArray displayed = new JSONArray(state.index());
        check(displayed.length() == 3 && !displayed.getJSONObject(1).getBoolean("enabled"),
                "catalog view dropped a disabled installed package");
        check(BemOptions.targetKey(displayed.getJSONObject(0)).equals(BemOptions.targetKey(displayed.getJSONObject(1))),
                "normal and ultimate character forms changed their existing target identity");
        check(BemOptions.targetKey(displayed.getJSONObject(2)).equals("weapon:sword_001"), "BEM 1.4 weapon disappeared");
        check(BemOptions.resourceKeys(displayed.getJSONObject(1)).contains("android-arm64:ultimate"), "ultimate resource metadata was lost");
        check(state.index().equals(catalog), "view state mutated the authoritative catalog");

        state.beginRead(5);state.fail(5, "安装索引损坏");
        check(state.hasCatalog() && state.index().equals(catalog), "later read failure erased the last successful list");
        check(state.hasError() && state.revision().isEmpty() && !state.canWrite(), "failure reused an old writable revision");
        check(state.message().contains("安装索引损坏"), "provider error detail was replaced by an empty state");
        int beforeStale = state.version();
        check(!state.succeed(4, "[]", "stale-four") && !state.fail(4, "stale-error"), "older callback replaced the current state");
        check(state.version() == beforeStale && state.index().equals(catalog), "stale callback changed view state");

        state.beginRead(6);state.beginRead(7);
        check(!state.isCurrent(6) && state.isCurrent(7), "read sequence did not identify the newest callback");
        check(!state.succeed(6, "[]", "revision-six"), "out-of-order older success was accepted");
        check(state.succeed(7, catalog, "revision-seven") && !state.hasError() && state.canWrite(), "newest success failed to clear error/re-enable edits");
        state.invalidate(8);
        check(!state.succeed(7, "[]", "stale-seven") && !state.fail(7, "stale-seven"), "write invalidation accepted an older read reply");
        check(state.index().equals(catalog) && state.revision().equals("revision-seven"), "stale reply undid the current usable catalog");

        state.beginRead(9);state.succeed(9, "{}", "revision-invalid");
        check(state.hasError() && !state.canWrite() && state.index().equals(catalog), "invalid payload replaced the saved list or became editable");
        check(state.message().contains("格式不可用"), "malformed array did not expose its read failure");
        state.beginRead(10);state.succeed(10, "[]", "");
        check(state.hasError() && !state.canWrite() && state.index().equals(catalog), "missing revision enabled writes or erased the saved list");
        state.beginRead(11);state.succeed(11, null, "revision-eleven");
        check(state.hasError() && state.index().equals(catalog), "missing index was treated as an empty catalog");
        state.beginRead(12);state.succeed(12, " ".repeat(200_001), "revision-large");
        check(state.hasError() && !state.canWrite() && state.index().equals(catalog), "oversized payload bypassed catalog boundary");
        state.beginRead(13);state.fail(13, " ");
        check(state.message().contains("设置桥不可用"), "blank error lost the visible failure state");
        state.beginRead(14);state.fail(14, "x".repeat(1000));
        check(state.message().length() < 200, "error display was not bounded");
        state.beginRead(15);state.succeed(15, catalog, "revision-final");
        check(state.canWrite() && !state.hasError() && state.revision().equals("revision-final"), "successful retry did not return to normal editing");

        rejectInvalidRows(state, catalog);
        transientStartupRecovery();

        if (args.length > 0) {
            String saved = Files.readString(Path.of(args[0]));
            JSONArray original = new JSONArray(saved);
            state.beginRead(100);state.succeed(100, saved, "saved-revision");
            JSONArray installed = new JSONArray(state.index());
            check(state.canWrite() && !state.hasError() && installed.length() == original.length(), "saved catalog did not recover every entry");
            check(state.index().equals(saved), "saved catalog was rewritten while recovering");
            int enabled = 0, originalEnabled = 0;
            for (int i = 0; i < installed.length(); ++i) {
                JSONObject item = installed.getJSONObject(i);BemOptions.targetKey(item);
                item.getString("generation");item.getString("name");
                if (item.optBoolean("enabled", true)) ++enabled;
                if (original.getJSONObject(i).optBoolean("enabled", true)) ++originalEnabled;
            }
            check(enabled == originalEnabled, "saved catalog enable state changed during recovery");
        }
        System.out.println("PASS Overlay model catalog state: " + checks + " active checks; initial/read errors, real empty result, preserved list, disabled stale revision, retries and stale callbacks");
    }

    private static void rejectInvalidRows(OverlayModelCatalogState state, String catalog) throws Exception {
        JSONObject missingGeneration = entry(20, "character", "chr_0004_pelica", "normal", true);
        missingGeneration.remove("generation");
        JSONObject missingName = entry(21, "character", "chr_0004_pelica", "normal", true);
        missingName.remove("name");
        JSONObject missingTarget = entry(22, "character", "chr_0004_pelica", "normal", true);
        missingTarget.remove("target_id");
        JSONObject badKind = entry(23, "other", "chr_0004_pelica", "normal", true);
        String[] malformed = {"[null]", "[1]", "[\"scalar\"]", new JSONArray().put(missingGeneration).toString(),
                new JSONArray().put(missingName).toString(), new JSONArray().put(missingTarget).toString(),
                new JSONArray().put(badKind).toString()};
        long request = 20;
        for (String value : malformed) {
            state.beginRead(request);state.succeed(request++, value, "invalid-row");
            check(state.hasCatalog() && state.index().equals(catalog), "unrenderable row discarded the last good catalog: " + value);
            check(state.hasError() && !state.canWrite() && state.revision().isEmpty(), "unrenderable row left a writable revision");
            check(state.message().contains("格式不可用"), "unrenderable row did not expose a format error");
        }
        JSONObject legacy = entry(24, "character", "chr_0004_pelica", "normal", true);
        legacy.put("bem_minor", 3).put("character_id", "chr_0004_pelica");
        legacy.remove("target_kind");legacy.remove("target_id");legacy.remove("resource_keys");
        String oldIndex = new JSONArray().put(legacy).toString();
        state.beginRead(30);state.succeed(30, oldIndex, "legacy-revision");
        check(state.canWrite() && state.index().equals(oldIndex), "minimal collapsed-card validation rejected legacy BEM 1.3 identity");
    }

    private static void transientStartupRecovery() throws Exception {
        OverlayModelCatalogState state = new OverlayModelCatalogState();
        JSONArray entries = new JSONArray();
        for (int i = 0; i < 12; ++i) entries.put(entry(i + 1, i == 11 ? "weapon" : "character",
                i == 11 ? "sword_001" : "chr_0004_pelica", i == 1 ? "ultimate" : "normal", i == 0));
        String catalog = entries.toString();
        state.beginRead(1);state.fail(1, "设置桥暂时不可用");
        state.beginRead(2);state.fail(2, "设置桥暂时不可用");
        check(!state.hasCatalog() && !state.canWrite() && state.hasError(), "transient startup errors became an empty editable list");
        state.beginRead(3);state.beginRead(4);
        check(!state.succeed(3, "[]", "late-empty"), "late startup success replaced the newer retry");
        check(state.succeed(4, catalog, "ready") && state.canWrite() && !state.hasError(), "newest retry did not recover from startup errors");
        JSONArray displayed = new JSONArray(state.index());
        check(displayed.length() == 12, "startup recovery lost synthetic installed entries");
        int enabled = 0;
        for (int i = 0; i < displayed.length(); ++i) if (displayed.getJSONObject(i).optBoolean("enabled", true)) ++enabled;
        check(enabled == 1, "startup recovery changed synthetic enable states");
        int version = state.version();
        check(!state.fail(2, "late-failure") && state.version() == version && state.canWrite(), "late failure disabled the recovered page");
        state.beginRead(5);state.succeed(5, catalog, "ready");
        check(state.version() == version, "identical successful polling rebuilt the recovered view");
        state.invalidate(6);
        check(!state.succeed(5, "[]", "old-read") && state.index().equals(catalog), "old read overwrote a write in progress");
        state.beginRead(6);state.fail(6, "恢复读取暂时失败");
        check(state.hasCatalog() && state.index().equals(catalog) && !state.canWrite(), "failed restoration erased the displayed list or kept writes enabled");
        state.beginRead(7);state.succeed(7, catalog, "restored");
        check(state.canWrite() && !state.hasError(), "subsequent polling failed to recover a restoration error");

        OverlayModelCatalogState reopened = new OverlayModelCatalogState();
        check(!reopened.hasCatalog() && !reopened.canWrite(), "a reopened page reused stale state without reading");
        reopened.beginRead(1);reopened.succeed(1, catalog, "reopened");
        state.fail(6, "closed-page-callback");
        check(reopened.canWrite() && reopened.index().equals(catalog), "a late callback from the old page affected its replacement");
    }

    private static JSONObject entry(int number, String kind, String owner, String resource, boolean enabled) throws Exception {
        return new JSONObject().put("generation", String.format(java.util.Locale.ROOT, "%08d-1111-4111-8111-111111111111", number))
                .put("name", "Model " + resource).put("bem_minor", 4).put("target_kind", kind).put("target_id", owner)
                .put("resource_keys", new JSONArray().put("android-arm64:" + resource)).put("enabled", enabled);
    }
}
