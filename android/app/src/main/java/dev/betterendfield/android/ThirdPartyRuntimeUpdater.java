package dev.betterendfield.android;
import android.content.Context;
import android.content.SharedPreferences;
import java.nio.charset.StandardCharsets;
import java.io.FileInputStream;
import java.io.ByteArrayOutputStream;
import java.util.function.Consumer;
import java.util.function.Supplier;

/** Separate queue from built-in camera and BEM commands. Binary generations stay pinned. */
final class ThirdPartyRuntimeUpdater {
    static void start(Context context, Supplier<SharedPreferences> preferences,
            ThirdPartyRuntimeMaterializer.Source source, String initial, Consumer<String> log) {
        if (initial == null || initial.isBlank()) return;
        Thread worker = new Thread(() -> {
            String applied = initial, lastFailure = "";
            while (!Thread.currentThread().isInterrupted()) {
                try {
                    if (RuntimeBootstrap.loaded()) {
                        String current = preferences.get().getString(ThirdPartyRuntimeMaterializer.PREFERENCE, "");
                        if (!current.equals(applied) && !current.isBlank()) {
                            String path = ThirdPartyRuntimeMaterializer.prepare(context, current, source, log);
                            String privateIndex;
                            try (FileInputStream input = new FileInputStream(path); ByteArrayOutputStream output = new ByteArrayOutputStream()) {
                                byte[] buffer = new byte[4096]; int n;
                                while ((n = input.read(buffer)) != -1) { if (output.size() + n > 1024 * 1024) throw new java.io.IOException("Runtime index exceeds limit"); output.write(buffer, 0, n); }
                                privateIndex = output.toString(StandardCharsets.UTF_8.name());
                            }
                            if (NativeCommandBridge.updateThirdPartyRuntime(privateIndex)) { applied = current; lastFailure = ""; }
                        }
                    }
                } catch (Exception | LinkageError failure) {
                    String text = failure.getClass().getSimpleName() + ": " + failure.getMessage();
                    if (!text.equals(lastFailure)) { log.accept("Third-party update failed: " + text); lastFailure = text; }
                }
                try { Thread.sleep(1000); } catch (InterruptedException stopped) { Thread.currentThread().interrupt(); return; }
            }
        }, "BetterEndfield-ThirdPartyUpdates");
        worker.setDaemon(true); worker.start();
    }
}
