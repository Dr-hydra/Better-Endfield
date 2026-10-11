package dev.betterendfield.next;
import android.content.Context;
import org.json.JSONArray;
import org.json.JSONObject;
import java.io.*;
import java.nio.file.*;
import java.util.*;

/** Production cache preparation with a synthetic remote file provider. */
public final class MediaScreenFilesTest {
    static final String NAME = "media-01234567-89ab-cdef-0123-456789abcdef.png";
    static int opens;
    static void check(boolean value, String message) { if (!value) throw new AssertionError(message); }
    static String manifest(String name, long bytes) {
        return new JSONArray().put(new JSONObject().put("remote", name).put("bytes", bytes)).toString();
    }
    static void prepare(Context context, String token, String index, byte[] remote) {
        MediaScreenFiles.prepare(context, "enabled=true\nsource=image\nsource_path=" + token + "\n",
            index, name -> { opens++; return new ByteArrayInputStream(remote); }, message -> {});
    }
    public static void main(String[] args) throws Exception {
        check(args.length == 1, "fixture root required");
        File files = new File(args[0], UUID.randomUUID().toString());
        check(files.mkdirs(), "fixture directory"); Context context = new Context(files);
        byte[] bytes = {1,2,3,4};
        prepare(context, "@" + NAME, manifest(NAME, 4), bytes);
        File cached = new File(files, "betterendfieldnext/media/" + NAME);
        check(MediaScreenFiles.configuration.contains("source_path=" + cached.getAbsolutePath()) && cached.length() == 4, "game-private path rewrite/copy");
        prepare(context, "@" + NAME, manifest(NAME, 4), bytes);
        check(opens == 1, "immutable cache must not repeat transfers");
        prepare(context, "@" + NAME, manifest(NAME, 5), bytes);
        check(MediaScreenFiles.configuration.isEmpty() && cached.length() == 4 && !new File(cached + ".tmp").exists(), "truncated transfer published or damaged old cache");
        prepare(context, "@" + NAME, manifest(NAME, 3), bytes);
        check(MediaScreenFiles.configuration.isEmpty() && cached.length() == 4, "oversized transfer published");
        for (String token : new String[]{"@../outside.png", "@media-------------------------------------.png", "C:/outside.png", "@media-11111111-1111-1111-1111-111111111111.png"}) {
            prepare(context, token, manifest(NAME, 4), bytes); check(MediaScreenFiles.configuration.isEmpty(), "invalid token accepted");
        }
        prepare(context, "@" + NAME, manifest(NAME, 33L * 1024 * 1024), bytes);
        check(MediaScreenFiles.configuration.isEmpty(), "image cache size bound ignored");
        String duplicate = new JSONArray().put(new JSONObject().put("remote", NAME).put("bytes", 4))
            .put(new JSONObject().put("remote", NAME).put("bytes", 4)).toString();
        prepare(context, "@" + NAME, duplicate, bytes); check(MediaScreenFiles.configuration.isEmpty(), "duplicate manifest accepted");
        JSONArray excessive = new JSONArray(); for (int i = 0; i < 257; ++i) excessive.put(new JSONObject().put("remote", NAME).put("bytes", 4));
        prepare(context, "@" + NAME, excessive.toString(), bytes); check(MediaScreenFiles.configuration.isEmpty(), "manifest count ignored");
        String second = "media-11111111-1111-1111-1111-111111111111.jpg";
        String two = new JSONArray().put(new JSONObject().put("remote", NAME).put("bytes", 4))
            .put(new JSONObject().put("remote", second).put("bytes", 4)).toString();
        MediaScreenFiles.prepare(context, "enabled=true\nsource=slideshow\nsource_path=\nimage_paths=@" + NAME + "|@" + second + "\n",
            two, name -> new ByteArrayInputStream(bytes), message -> {});
        check(MediaScreenFiles.configuration.contains("image_paths=" + cached.getAbsolutePath() + "|") &&
            !MediaScreenFiles.configuration.contains("@media-"), "slideshow token rewrite");
        MediaScreenFiles.prepare(context, "", "malformed", name -> { throw new AssertionError("disabled configuration opened files"); }, message -> {});
        check(MediaScreenFiles.configuration.isEmpty(), "disabled preparation kept stale configuration");
        Path link = new File(files, "betterendfieldnext/media/" + second + ".tmp").toPath();
        try {
            Files.createSymbolicLink(link, cached.toPath());
            prepare(context, "@" + second, manifest(second, 5), bytes);
            check(MediaScreenFiles.configuration.isEmpty() && Files.isSymbolicLink(link), "symlink cache entry followed");
        } catch (FileSystemException | UnsupportedOperationException unavailable) { System.out.println("SKIP symlink creation: host permission/filesystem"); }
        System.out.println("PASS Android media: private-path copy, cache reuse, bounds, token validation, atomic failure and slideshow rewrite");
    }
}
