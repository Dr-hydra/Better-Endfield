package dev.betterendfield.next;

import android.content.SharedPreferences;
import java.util.function.Supplier;

/** The existing module preferences remain authoritative, including edits from the module app. */
final class GlobalFovUpdater {
    static void start(Supplier<SharedPreferences> preferences) {
        Thread worker = new Thread(() -> {
            String applied = "";
            while (!Thread.currentThread().isInterrupted()) {
                try {
                    if (RuntimeBootstrap.loaded()) {
                        SharedPreferences prefs = preferences.get();
                        boolean enabled = prefs.getBoolean(ModuleSettings.CAMERA_GLOBAL_FOV_ENABLED, false);
                        float value = (float) ModuleSettings.parse(prefs.getString(ModuleSettings.CAMERA_GLOBAL_FOV, "60"), 60);
                        value = Math.max(5, Math.min(150, value));
                        String current = enabled + ":" + value;
                        if (!current.equals(applied) && NativeCommandBridge.globalFov(enabled, value)) applied = current;
                    }
                } catch (RuntimeException | LinkageError unavailable) { /* Retry when the framework/runtime returns. */ }
                try { Thread.sleep(500); } catch (InterruptedException stopped) { Thread.currentThread().interrupt(); return; }
            }
        }, "BetterEndfieldNext-GlobalFov");
        worker.setDaemon(true); worker.start();
    }
}
