package dev.betterendfield.android;

import android.content.Context;
import android.util.AtomicFile;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.charset.StandardCharsets;

/** One bounded private status record; no request, preference or authorization contents. */
final class OverlaySettingsDiagnostics {
    private static String previous = "";
    private OverlaySettingsDiagnostics() {}
    static synchronized void record(Context context, String status) {
        if (context == null || status == null || status.length() > 512
                || !status.matches("[A-Za-z0-9_.= :;,-]*")) return;
        AtomicFile file = null;
        FileOutputStream stream = null;
        try {
            file = new AtomicFile(new File(context.getCacheDir(), "betterendfield-overlay-settings.log"));
            stream = file.startWrite();
            stream.write((System.currentTimeMillis() + " " + status + "\nprevious " + previous + "\n").getBytes(StandardCharsets.US_ASCII));
            file.finishWrite(stream); stream = null;
        } catch (IOException | RuntimeException unavailable) {
            // Diagnostics never change the Binder result or interrupt the game.
        } finally {
            previous = status;
            if (stream != null && file != null) {
                try { file.failWrite(stream); } catch (RuntimeException ignored) {}
            }
        }
    }
}
