package dev.betterendfield.android;

import android.content.ContentProvider;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import android.os.Binder;
import android.os.Bundle;
import android.os.Process;
import org.json.JSONArray;
import org.json.JSONObject;

/** Only bounded settings patches cross UIDs; module-owned commits publish through FrameworkSettings. */
public final class OverlaySettingsProvider extends ContentProvider {
    static final String AUTHORITY = "dev.betterendfield.android.overlay.settings";
    @Override public boolean onCreate() {
        try { OverlayWriteAuthorization.initialize(getContext()); }
        catch (java.io.IOException unavailable) { android.util.Log.e("BetterEndfield.Overlay", "overlay authorization unavailable", unavailable); }
        return true;
    }
    @Override public Bundle call(String method, String arg, Bundle extras) {
        Context app = getContext();
        int caller = Binder.getCallingUid();
        Object authorization = extras == null ? null : extras.get(OverlayWriteAuthorization.REQUEST);
        // Provider IPC grants visibility of its calling UID, including previously unknown channel packages.
        if (app == null || !OverlayWritePolicy.callerAllowed(caller, Process.myUid(),
                app.getPackageManager().getPackagesForUid(caller),
                authorization instanceof String ? (String) authorization : null, OverlayWriteAuthorization.ownerToken()))
            throw new SecurityException("Caller cannot modify module settings");
        if (extras != null) { extras = new Bundle(extras); extras.remove(OverlayWriteAuthorization.REQUEST); }
        if (arg != null) throw new IllegalArgumentException("Unexpected argument");
        long identity = Binder.clearCallingIdentity();
        Bundle result = new Bundle();
        try {
            boolean models = "read_models".equals(method) || "edit_models".equals(method) || "disable_models".equals(method);
            if (!models && !"read_fov".equals(method) && !"edit_fov".equals(method))
                throw new IllegalArgumentException("Unknown settings operation");
            boolean read = method.startsWith("read_");
            if (!read && (extras == null || !extras.keySet().equals("disable_models".equals(method)
                    ? java.util.Set.of("expected") : java.util.Set.of("expected", "patch"))))
                throw new IllegalArgumentException("Invalid request fields");
            if (read && extras != null && !extras.isEmpty()) throw new IllegalArgumentException("Invalid read request");
            if (models) synchronized (BemInstaller.class) {
                // Normalize once through the installer, then compare under the very same lock as every writer.
                JSONArray current = BemInstaller.index(app);
                if (current.toString().length() > 200_000) throw new java.io.IOException("模型列表超出桥接大小限制");
                if (!read) {
                    OverlayWritePolicy.requireRevision(extras.getString("expected"), current.toString());
                    JSONArray changes = new JSONArray();
                    boolean disable = "disable_models".equals(method);
                    if (disable) {
                        for (int i = 0; i < current.length(); ++i) changes.put(new JSONObject()
                                .put("generation", current.getJSONObject(i).getString("generation")).put("enabled", false));
                    } else changes.put(OverlayWritePolicy.patch(extras.getString("patch"), false));
                    if (OverlayWritePolicy.apply(current, changes).toString().length() > 200_000)
                        throw new java.io.IOException("模型列表超出桥接大小限制");
                    if (disable) BemInstaller.disableAll(app); else BemInstaller.saveAll(app, changes);
                    current = BemInstaller.index(app);
                }
                String index = current.toString();
                result.putString("index", index); result.putString("revision", OverlayWritePolicy.revision(index));
                result.putBoolean("busy", BemInstaller.busy);
            } else synchronized (ModuleSettings.class) {
                if (!read) {
                    OverlayWritePolicy.requireRevision(extras.getString("expected"), fovState(app));
                    JSONObject patch = OverlayWritePolicy.patch(extras.getString("patch"), true);
                    ModuleSettings.saveGlobalFov(app, patch.has("enabled") ? patch.getBoolean("enabled") : null,
                            patch.has("value") ? patch.getDouble("value") : null);
                }
                result.putBoolean("enabled", ModuleSettings.isGlobalFovEnabled(app));
                result.putString("value", ModuleSettings.getGlobalFieldOfView(app));
                result.putString("revision", OverlayWritePolicy.revision(fovState(app)));
            }
            result.putBoolean("ok", true);
        } catch (Exception error) {
            result.clear(); result.putBoolean("ok", false);
            result.putString("error", error.getMessage() == null ? "设置未保存" : error.getMessage());
        } finally {
            Binder.restoreCallingIdentity(identity);
        }
        return result;
    }
    private static String fovState(Context app) {
        return ModuleSettings.isGlobalFovEnabled(app) + ":" + ModuleSettings.getGlobalFieldOfView(app);
    }
    @Override public Cursor query(Uri u, String[] p, String s, String[] a, String o) { throw new SecurityException("Unsupported"); }
    @Override public String getType(Uri u) { return null; }
    @Override public Uri insert(Uri u, ContentValues v) { throw new SecurityException("Unsupported"); }
    @Override public int update(Uri u, ContentValues v, String s, String[] a) { throw new SecurityException("Unsupported"); }
    @Override public int delete(Uri u, String s, String[] a) { throw new SecurityException("Unsupported"); }
}
