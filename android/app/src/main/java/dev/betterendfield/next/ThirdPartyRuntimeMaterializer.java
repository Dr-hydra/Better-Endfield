package dev.betterendfield.next;

import android.content.Context;
import org.json.JSONArray;
import org.json.JSONObject;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.HashSet;
import java.util.Locale;
import java.util.UUID;
import java.util.function.Consumer;
import java.util.zip.ZipEntry;
import java.util.zip.ZipInputStream;

/** Copies framework-owned module ZIPs into immutable, private game generations. */
final class ThirdPartyRuntimeMaterializer {
    static final String PREFERENCE = "third_party_runtime";
    static volatile String indexPath = "";
    interface Source { InputStream open(String name) throws IOException; }
    private static final long MAX = 256L * 1024 * 1024;
    static String prepare(Context context, String encoded, Source source, Consumer<String> log) throws Exception {
        if (encoded == null || encoded.isBlank()) { indexPath = ""; return ""; }
        JSONObject index = new JSONObject(encoded);
        if (index.optInt("schema") != 1 || index.optInt("port") < 1024 || index.optInt("port") > 65535
                || !index.optString("token").matches("[A-Za-z0-9_-]{32,128}")) throw new IOException("Invalid third-party runtime index");
        File root = new File(context.getFilesDir(), "betterendfieldnext/third-party");
        if (!root.isDirectory() && !root.mkdirs()) throw new IOException("Cannot create third-party directory");
        JSONArray records = index.getJSONArray("modules");
        if (records.length() > 128) throw new IOException("Too many third-party modules");
        HashSet<String> ids = new HashSet<>();
        for (int i = 0; i < records.length(); i++) {
            JSONObject record = records.getJSONObject(i);
            String id = record.getString("id");
            if (!id.matches("[A-Za-z0-9][A-Za-z0-9_.-]{0,95}") || id.toLowerCase(Locale.ROOT).startsWith("betterendfieldnext.")
                    || id.equalsIgnoreCase("voice.character") || !ids.add(id))
                throw new IOException("Invalid or duplicate third-party module ID");
            try {
            String generation = UUID.fromString(record.getString("generation")).toString();
            String remote = record.getString("android_remote");
            if (!remote.equals("tpm-" + generation + ".zip")) throw new IOException("Invalid module archive name");
            long archiveBytes = record.getLong("archive_bytes");
            if (archiveBytes <= 0 || archiveBytes > MAX) throw new IOException("Module ZIP exceeds limit");
            File directory = new File(root, "generations/" + generation);
            if (record.optBoolean("enabled", false) && !directory.isDirectory()) {
                File staging = new File(root, "staging-" + UUID.randomUUID());
                if (!staging.mkdirs()) throw new IOException("Cannot create module staging directory");
                try {
                    File archive = new File(staging, "source.zip");
                    long copied = 0;
                    try (InputStream input = source.open(remote); OutputStream out = new FileOutputStream(archive)) {
                        byte[] buffer = new byte[65536]; int count;
                        while ((count = input.read(buffer)) != -1) { copied += count; if (copied > archiveBytes || copied > MAX) throw new IOException("Module ZIP size differs"); out.write(buffer, 0, count); }
                    }
                    if (copied != archiveBytes) throw new IOException("Module ZIP size differs");
                    File payload = new File(staging, "payload"); if (!payload.mkdirs()) throw new IOException("Cannot create module payload");
                    extract(archive, payload);
                    JSONObject manifest = new JSONObject(read(new File(payload, "module.json"), 1024 * 1024));
                    if (manifest.optInt("format") != 1 || manifest.optInt("abi") != 1 || !id.equals(manifest.optString("id"))) throw new IOException("Module identity or ABI differs");
                    String nativePath = manifest.getJSONObject("libraries").optString("android-arm64", "");
                    if (!nativePath.isEmpty()) {
                        File library = child(payload, nativePath);
                        if (!nativePath.endsWith(".so") || !library.isFile()) throw new IOException("Missing Android module library");
                    } else {
                        String page = manifest.optString("ui", "");
                        if (page.isEmpty() || !child(payload, page).isFile()) throw new IOException("No Android runtime or UI in this module");
                    }
                    File parent = directory.getParentFile(); if (!parent.isDirectory() && !parent.mkdirs()) throw new IOException("Cannot create generation parent");
                    if (!payload.renameTo(directory)) throw new IOException("Cannot publish module generation");
                    log.accept("Third-party module prepared: " + id);
                } finally { remove(staging); }
            }
            record.put("directory", directory.getAbsolutePath());
            } catch (Exception failure) {
                record.put("enabled", false);
                record.put("directory", new File(root, "unavailable-" + i).getAbsolutePath());
                record.put("preparation_error", failure.getClass().getSimpleName() + ": " + failure.getMessage());
                log.accept("Third-party module preparation failed: " + id + ": " + failure.getMessage());
            }
        }
        File destination = new File(root, "index.json"), pending = new File(root, "index-" + UUID.randomUUID() + ".tmp");
        try (FileOutputStream out = new FileOutputStream(pending)) { out.write(index.toString().getBytes(StandardCharsets.UTF_8)); out.getFD().sync(); }
        try { android.system.Os.rename(pending.getAbsolutePath(), destination.getAbsolutePath()); }
        catch (Exception failure) { pending.delete(); throw new IOException("Cannot publish module index", failure); }
        indexPath = destination.getAbsolutePath(); return indexPath;
    }
    private static File child(File root, String path) throws IOException {
        if (path == null || path.isEmpty() || path.startsWith("/") || path.indexOf('\\') >= 0 || path.indexOf(':') >= 0 || path.indexOf('\0') >= 0)
            throw new IOException("Invalid module path");
        for (String part : path.split("/", -1)) if (part.isEmpty() || part.equals(".") || part.equals("..")) throw new IOException("Invalid module path segment");
        File candidate = new File(root, path).getCanonicalFile();
        if (!candidate.getPath().startsWith(root.getCanonicalPath() + File.separator)) throw new IOException("Module path escapes generation");
        return candidate;
    }
    private static void extract(File archive, File root) throws IOException {
        long total = 0; int entries = 0; HashSet<String> names = new HashSet<>();
        try (ZipInputStream zip = new ZipInputStream(new FileInputStream(archive))) {
            ZipEntry entry; byte[] buffer = new byte[65536];
            while ((entry = zip.getNextEntry()) != null) {
                if (++entries > 4096) throw new IOException("Too many ZIP entries");
                String path = entry.getName(); if (entry.isDirectory() && path.endsWith("/")) path = path.substring(0, path.length() - 1);
                File target = child(root, path);
                if (!names.add(path.toLowerCase(Locale.ROOT))) throw new IOException("Duplicate ZIP entry");
                if (entry.isDirectory()) { if (!target.isDirectory() && !target.mkdirs()) throw new IOException("Cannot create module directory"); }
                else {
                    File parent = target.getParentFile(); if (!parent.isDirectory() && !parent.mkdirs()) throw new IOException("Cannot create module directory");
                    long size = 0;
                    try (OutputStream out = new FileOutputStream(target)) { int count; while ((count = zip.read(buffer)) != -1) {
                        total += count; size += count; if (total > MAX || size > 128L * 1024 * 1024) throw new IOException("Unpacked module exceeds limit"); out.write(buffer, 0, count);
                    } }
                }
                zip.closeEntry();
            }
        }
    }
    private static String read(File file, int limit) throws IOException {
        if (!file.isFile() || file.length() > limit) throw new IOException("Invalid module manifest");
        try (InputStream in = new FileInputStream(file); ByteArrayOutputStream out = new ByteArrayOutputStream()) { byte[] buffer = new byte[4096]; int n; while ((n = in.read(buffer)) != -1) { if (out.size() + n > limit) throw new IOException("Manifest exceeds limit"); out.write(buffer, 0, n); } return out.toString(StandardCharsets.UTF_8.name()); }
    }
    private static void remove(File root) { File[] children = root.listFiles(); if (children != null) for (File child : children) remove(child); root.delete(); }
}
