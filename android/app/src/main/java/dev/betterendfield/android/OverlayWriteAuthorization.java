package dev.betterendfield.android;

import android.content.Context;
import android.os.Process;
import android.util.AtomicFile;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.charset.StandardCharsets;

/** Owner-only durable secret. It is published only in framework-protected remote preferences. */
final class OverlayWriteAuthorization {
    static final String PREFERENCE = "overlay_write_authorization_v1";
    static final String REQUEST = "authorization";
    private static volatile String token;

    static synchronized void initialize(Context module) throws IOException {
        if (!RuntimeBootstrap.MODULE_PACKAGE.equals(module.getPackageName())
                || module.getApplicationInfo().uid != Process.myUid())
            throw new SecurityException("Only the module owner can initialize overlay authorization");
        if (token != null) return;
        File path = new File(module.getFilesDir(), "overlay-write-authorization");
        AtomicFile file = new AtomicFile(path);
        if (path.exists() || new File(path.getPath() + ".bak").exists()) {
            token = read(file); return;
        }
        String created = OverlayWritePolicy.newToken();
        FileOutputStream stream = null;
        try {
            stream = file.startWrite(); stream.write(created.getBytes(StandardCharsets.US_ASCII)); stream.getFD().sync();
            file.finishWrite(stream); stream = null;
            String persisted = read(file);
            if (!created.equals(persisted)) throw new IOException("Overlay authorization persistence failed");
            token = persisted;
        } finally { if (stream != null) file.failWrite(stream); }
    }
    static String ownerToken() { return token; }

    private static String read(AtomicFile file) throws IOException {
        // Bound the read even if the owner file was corrupted. Never include the secret in errors.
        byte[] bytes = new byte[65]; int length = 0, count;
        try (FileInputStream input = file.openRead()) {
            while (length < bytes.length && (count = input.read(bytes, length, bytes.length - length)) != -1) length += count;
        }
        String value = new String(bytes, 0, length, StandardCharsets.US_ASCII);
        if (!OverlayWritePolicy.validToken(value)) throw new IOException("Overlay authorization file is invalid");
        return value;
    }
}
