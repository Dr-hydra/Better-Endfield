package dev.betterendfield.android;

import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;

import java.util.Map;
import java.util.Set;
import java.io.FileOutputStream;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import android.os.ParcelFileDescriptor;
import io.github.libxposed.service.XposedService;
import io.github.libxposed.service.XposedServiceHelper;

/** Local UI preferences remain authoritative; framework service publishes snapshots. */
final class FrameworkSettings {
    private static XposedService service;
    private static SharedPreferences local;
    private static XposedService remoteService;
    // SharedPreferences keeps weak references; retain this for the application lifetime.
    private static final SharedPreferences.OnSharedPreferenceChangeListener listener = (prefs, key) -> publish();

    static void initialize(Context context) {
        local = open(context);
        local.registerOnSharedPreferenceChangeListener(listener);
        XposedServiceHelper.registerListener(new XposedServiceHelper.OnServiceListener() {
            @Override public void onServiceBind(XposedService connected) {
                synchronized (FrameworkSettings.class) { service = connected; remoteService = connected; publish(); }
            }
            @Override public void onServiceDied(XposedService disconnected) {
                synchronized (FrameworkSettings.class) { if (service == disconnected) { service = null; remoteService = null; } }
            }
        });
    }

    static synchronized boolean writeRemoteCommand(String payload) {
        if (remoteService == null || payload == null) return false;
        try (ParcelFileDescriptor descriptor = remoteService.openRemoteFile("command.next");
                FileOutputStream stream = new FileOutputStream(descriptor.getFileDescriptor())) {
            stream.write(payload.getBytes(java.nio.charset.StandardCharsets.UTF_8));
            stream.flush();
            return true;
        } catch (RuntimeException | java.io.IOException error) {
            return false;
        }
    }

    static synchronized boolean writeRemoteStatus(String payload) {
        if (remoteService == null || payload == null) return false;
        try (ParcelFileDescriptor descriptor = remoteService.openRemoteFile("command.status");
                FileOutputStream stream = new FileOutputStream(descriptor.getFileDescriptor())) {
            stream.write(payload.getBytes(java.nio.charset.StandardCharsets.UTF_8));
            stream.flush();
            return true;
        } catch (RuntimeException | java.io.IOException error) {
            return false;
        }
    }

    static synchronized String readRemoteStatus() {
        if (remoteService == null) return "";
        try (ParcelFileDescriptor descriptor = remoteService.openRemoteFile("command.status");
                BufferedReader reader = new BufferedReader(new InputStreamReader(
                        new ParcelFileDescriptor.AutoCloseInputStream(descriptor),
                        java.nio.charset.StandardCharsets.UTF_8))) {
            StringBuilder value = new StringBuilder(); String line;
            while ((line = reader.readLine()) != null) value.append(line).append('\n');
            return value.toString();
        } catch (RuntimeException | java.io.IOException error) { return ""; }
    }

    static SharedPreferences open(Context context) {
        return context.getSharedPreferences("module_settings", Context.MODE_PRIVATE);
    }

    @SuppressWarnings("unchecked")
    private static synchronized void publish() {
        if (service == null || local == null) return;
        try {
            SharedPreferences remote = service.getRemotePreferences("module_settings");
            SharedPreferences.Editor edit = remote.edit().clear();
            for (Map.Entry<String, ?> entry : local.getAll().entrySet()) {
                String key = entry.getKey(); Object value = entry.getValue();
                if (value instanceof String) edit.putString(key, (String) value);
                else if (value instanceof Boolean) edit.putBoolean(key, (Boolean) value);
                else if (value instanceof Integer) edit.putInt(key, (Integer) value);
                else if (value instanceof Long) edit.putLong(key, (Long) value);
                else if (value instanceof Float) edit.putFloat(key, (Float) value);
                else if (value instanceof Set<?>) edit.putStringSet(key, (Set<String>) value);
            }
            edit.putInt("schemaVersion", 1);
            edit.putLong("generation", remote.getLong("generation", 0) + 1);
            if (!edit.commit()) Log.e("BetterEndfield.Settings", "framework snapshot commit failed");
        } catch (RuntimeException error) {
            Log.e("BetterEndfield.Settings", "framework snapshot unavailable; local settings retained", error);
        }
    }
}
