package dev.betterendfield.next;

import android.content.Context;
import org.json.JSONArray;
import org.json.JSONObject;
import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;
import java.util.HashMap;
import java.util.Map;
import java.util.function.Consumer;

/** Immutable imported files are copied into the game's own private directory. */
final class MediaScreenFiles {
    static final String INDEX = "media_screen_files";
    static volatile String configuration = "";
    static boolean valid(String name) { return name != null && name.matches("media-[a-f0-9]{8}-[a-f0-9]{4}-[a-f0-9]{4}-[a-f0-9]{4}-[a-f0-9]{12}\\.(png|jpg|mp4)"); }
    static void prepare(Context context, String ini, String manifest, BemInstalledResources.Source source, Consumer<String> log) {
        configuration = "";
        if (ini == null || ini.isEmpty()) return;
        try {
            File root = new File(context.getFilesDir(), "betterendfieldnext/media");
            if (Files.isSymbolicLink(root.toPath()) || Files.isSymbolicLink(root.getParentFile().toPath())) throw new IOException("Invalid media directory");
            if (!root.isDirectory() && !root.mkdirs()) throw new IOException("Cannot create media directory");
            JSONArray entries = new JSONArray(manifest); Map<String, Long> sizes = new HashMap<>();
            if (entries.length() > 256) throw new IOException("Too many media files");
            for (int i = 0; i < entries.length(); ++i) {
                JSONObject entry = entries.getJSONObject(i); String remote = entry.getString("remote"); long size = entry.getLong("bytes");
                long limit = remote.endsWith(".mp4") ? 512L * 1024 * 1024 : 32L * 1024 * 1024;
                if (!valid(remote) || size <= 0 || size > limit || sizes.putIfAbsent(remote, size) != null) throw new IOException("Invalid media manifest");
            }
            StringBuilder output = new StringBuilder();
            for (String line : ini.split("\\n")) {
                if (line.startsWith("source_path=")) line = "source_path=" + materialize(root, line.substring(12).trim(), sizes, source);
                else if (line.startsWith("image_paths=")) {
                    java.util.ArrayList<String> paths = new java.util.ArrayList<>();
                    for (String item : line.substring(12).split("\\|"))
                        if (!item.trim().isEmpty()) paths.add(materialize(root, item.trim(), sizes, source));
                    line = "image_paths=" + String.join("|", paths);
                }
                output.append(line).append('\n');
            }
            configuration = output.toString(); log.accept("media files prepared");
        } catch (Exception error) { log.accept("media files unavailable: " + error.getMessage()); }
    }
    private static String materialize(File root, String token, Map<String, Long> sizes, BemInstalledResources.Source source) throws Exception {
        if (token.isEmpty()) return "";
        if (!token.startsWith("@")) throw new IOException("Media must be imported through the manager");
        String name = token.substring(1);
        if (!valid(name) || !sizes.containsKey(name)) throw new IOException("Media file missing from manifest");
        long expected = sizes.get(name); File target = new File(root, name), stage = new File(root, name + ".tmp");
        if (Files.isSymbolicLink(target.toPath()) || Files.isSymbolicLink(stage.toPath())) throw new IOException("Invalid media cache entry");
        if (target.isFile() && target.length() == expected) return target.getAbsolutePath();
        try {
            try (InputStream input = source.open(name); FileOutputStream output = new FileOutputStream(stage)) {
                byte[] buffer = new byte[65536]; long total = 0; int count;
                while ((count = input.read(buffer)) != -1) {
                    total += count; if (total > expected) throw new IOException("Media file exceeds manifest size");
                    output.write(buffer, 0, count);
                }
                if (total != expected) throw new IOException("Media file is incomplete"); output.getFD().sync();
            }
            Files.move(stage.toPath(), target.toPath(), StandardCopyOption.REPLACE_EXISTING, StandardCopyOption.ATOMIC_MOVE);
            return target.getAbsolutePath();
        } finally { Files.deleteIfExists(stage.toPath()); }
    }
}
