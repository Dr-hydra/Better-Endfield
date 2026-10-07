package dev.betterendfield.android;

import org.json.JSONArray;
import org.json.JSONObject;

/** View state only: a failed bridge read is never an authoritative empty catalog. */
final class OverlayModelCatalogState {
    private String index = "[]", revision = "", error = "";
    private boolean known;
    private long currentRequest;
    private int version;

    void beginRead(long request) { currentRequest = request; }
    void invalidate(long request) { currentRequest = request; }
    boolean isCurrent(long request) { return request == currentRequest; }
    boolean hasCatalog() { return known; }
    boolean hasError() { return !error.isEmpty(); }
    boolean canWrite() { return known && !hasError() && !revision.isEmpty(); }
    String index() { return index; }
    String revision() { return revision; }
    int version() { return version; }
    String message() { return hasError() ? "模型列表读取失败：" + error : "正在读取模型列表"; }

    boolean succeed(long request, String value, String nextRevision) {
        if (!isCurrent(request)) return false;
        if (value == null || nextRevision == null || nextRevision.isEmpty())
            return fail(request, "设置桥返回的数据不完整");
        if (value.length() > 200_000) return fail(request, "模型列表超出桥接大小限制");
        if (known && !hasError() && index.equals(value) && revision.equals(nextRevision)) return true;
        try {
            JSONArray entries = new JSONArray(value);
            // Validate only the identity required to render collapsed cards.
            // Keep legacy BEM identities and optional detail metadata unchanged.
            for (int i = 0; i < entries.length(); ++i) {
                JSONObject entry = entries.getJSONObject(i);
                entry.getString("generation"); entry.getString("name");
                BemOptions.targetKey(entry);
            }
        }
        catch (Exception invalid) { return fail(request, "模型列表格式不可用"); }
        if (!known || !index.equals(value) || !revision.equals(nextRevision) || hasError()) ++version;
        known = true; index = value; revision = nextRevision; error = "";
        return true;
    }

    boolean fail(long request, String reason) {
        if (!isCurrent(request)) return false;
        String next = reason == null || reason.trim().isEmpty() ? "设置桥不可用" : reason.trim();
        if (next.length() > 160) next = next.substring(0, 160);
        if (!error.equals(next) || !revision.isEmpty()) ++version;
        // Retain a previously fetched list for display, but never reuse its
        // revision after failure. Editing resumes only on a successful reply.
        revision = ""; error = next;
        return true;
    }
}
