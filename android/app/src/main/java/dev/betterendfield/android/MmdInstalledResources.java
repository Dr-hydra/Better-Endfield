package dev.betterendfield.android;

import android.content.Context;
import java.io.*;
import java.util.HashSet;
import java.util.function.Consumer;
import org.json.*;

/** Prepare immutable works outside the Unity frame; incomplete works stay invisible. */
final class MmdInstalledResources {
    static void prepare(Context context, String index, BemInstalledResources.Source source,
                        Consumer<String> log) throws Exception {
        File root = new File(context.getFilesDir(), "betterendfield/mmd");
        if (!root.isDirectory() && !root.mkdirs()) throw new IOException("Cannot create MMD directory");
        HashSet<String> advertised = new HashSet<>();
        JSONArray works = new JSONArray(index);
        if (works.length() > 512) throw new IOException("Too many MMD works");
        for (int i = 0; i < works.length(); i++) {
            JSONObject work = works.getJSONObject(i);
            String folderName = work.getString("folder"), generation = work.getString("generation");
            if (!folderName.equals(generation) || !generation.matches("[a-f0-9-]{36}"))
                throw new IOException("Invalid MMD generation");
            File folder = new File(root, folderName);
            File stage = new File(root, ".stage-" + generation);
            JSONArray files = work.getJSONArray("files");
            try {
                if (files.length() < 2 || files.length() > 32) throw new IOException("Invalid MMD file count");
                boolean complete = folder.isDirectory(), set = false;
                HashSet<String> names = new HashSet<>();
                long total = 0;
                for (int j = 0; j < files.length(); j++) {
                    JSONObject file = files.getJSONObject(j);
                    String name = file.getString("name"), remote = file.getString("remote");
                    long size = file.getLong("bytes"); total += size;
                    MmdInstaller.requirePlainName(name);
                    if (!names.add(name) || !FrameworkSettings.validMmdRemote(remote)
                            || !remote.startsWith("mmd-" + generation + "-")
                            || size <= 0 || size > 512L * 1024 * 1024 || total > 1024L * 1024 * 1024)
                        throw new IOException("Invalid MMD payload");
                    set |= "set.ini".equals(name);
                    File cached = new File(folder, name);
                    complete &= cached.isFile() && cached.length() == size;
                }
                if (!set) throw new IOException("Missing set.ini");
                if (!complete) {
                    deleteOwned(stage);
                    if (!stage.mkdir()) throw new IOException("Cannot create work staging directory");
                    for (int j = 0; j < files.length(); j++) {
                        JSONObject file = files.getJSONObject(j);
                        long size = file.getLong("bytes"), length = 0;
                        try (InputStream in = source.open(file.getString("remote"));
                             FileOutputStream out = new FileOutputStream(new File(stage, file.getString("name")))) {
                            byte[] buffer = new byte[65536]; int count;
                            while ((count = in.read(buffer)) != -1) {
                                length += count;
                                if (length > size) throw new IOException("MMD payload exceeds declared size");
                                out.write(buffer, 0, count);
                            }
                            if (length != size) throw new IOException("Truncated MMD payload");
                            out.getFD().sync();
                        }
                    }
                    // Withdraw only an incomplete owned generation before publishing a complete one.
                    deleteOwned(folder);
                    if (!stage.renameTo(folder)) throw new IOException("Cannot publish MMD work");
                }
                advertised.add(folderName);
            } catch (Exception error) {
                log.accept("MMD work unavailable " + folderName + ": " + error.getMessage());
                deleteOwned(folder);
            } finally { deleteOwned(stage); }
        }
        // Called before runtime startup: withdrawn works must not appear in the library.
        File[] folders = root.listFiles();
        if (folders != null) for (File folder : folders)
            if (folder.getName().matches("[a-f0-9-]{36}") && !advertised.contains(folder.getName())) deleteOwned(folder);
        log.accept("MMD works prepared: " + advertised.size());
    }
    private static void deleteOwned(File file) {
        if (java.nio.file.Files.isSymbolicLink(file.toPath())) { file.delete(); return; }
        File[] children = file.listFiles();
        if (children != null) for (File child : children) deleteOwned(child);
        file.delete();
    }
}
