package dev.betterendfield.android;

import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import android.provider.DocumentsContract;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.ByteBuffer;
import java.nio.charset.CodingErrorAction;
import java.nio.charset.StandardCharsets;
import java.util.LinkedHashMap;
import java.util.LinkedHashSet;
import java.util.Locale;
import java.util.Map;
import java.util.Set;
import java.util.UUID;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/** Immutable work generations; publish all bytes before advertising a work. */
final class MmdInstaller {
    // [{generation: UUID, folder: UUID, name: string,
    //   files: [{remote: "mmd-UUID-tag", name: "plain filename", bytes: long}]}]
    // The game adapter materializes files beneath its own mmd/<folder>/ directory.
    static final String INDEX = "installed_mmd_works";
    static volatile boolean busy;
    static volatile String status = "请选择包含 set.ini 的作品目录。";
    private static final long MAX_FILE_BYTES = 512L * 1024 * 1024;
    private static final long MAX_WORK_BYTES = 1024L * 1024 * 1024;
    private static final ExecutorService WORKER = Executors.newSingleThreadExecutor();

    private MmdInstaller() {}

    static JSONArray index(Context context) {
        try {
            return new JSONArray(FrameworkSettings.open(context).getString(INDEX, "[]"));
        } catch (Exception invalid) {
            throw new IllegalStateException("作品索引损坏；未覆盖已有作品", invalid);
        }
    }

    static synchronized boolean start(Context context, Uri tree) {
        if (tree == null || !"content".equals(tree.getScheme())
                || !DocumentsContract.isTreeUri(tree))
            throw new IllegalArgumentException("请选择作品目录");
        if (busy) return false;
        Context app = context.getApplicationContext();
        boolean ownedGrant = false;
        try {
            boolean existing = false;
            for (android.content.UriPermission permission : app.getContentResolver().getPersistedUriPermissions())
                if (tree.equals(permission.getUri()) && permission.isReadPermission()) existing = true;
            if (!existing) {
                app.getContentResolver().takePersistableUriPermission(tree,
                        android.content.Intent.FLAG_GRANT_READ_URI_PERMISSION);
                ownedGrant = true;
            }
        } catch (SecurityException unsupported) {
            // Some providers offer only the temporary grant; copy while it lasts.
        }
        final boolean releaseGrant = ownedGrant;
        try {
            return run(() -> {
                try { install(app, tree); }
                finally { if (releaseGrant) releaseGrant(app, tree); }
            });
        } catch (RuntimeException rejected) {
            if (releaseGrant) releaseGrant(app, tree);
            throw rejected;
        }
    }

    static synchronized boolean republish(Context context, String generation) {
        Context app = context.getApplicationContext();
        return run(() -> {
            JSONObject entry = requireEntry(app, generation);
            JSONArray files = entry.getJSONArray("files");
            File folder = ownedFolder(app, entry.getString("folder"));
            for (int i = 0; i < files.length(); i++) {
                JSONObject file = files.getJSONObject(i);
                String name = file.getString("name");
                requirePlainName(name);
                File source = new File(folder, name);
                if (!source.isFile() || source.length() != file.getLong("bytes"))
                    throw new IOException("本地作品文件缺失或已变化：" + name);
                status = "正在发布 " + (i + 1) + "/" + files.length() + "：" + name;
                if (!FrameworkSettings.publishMmd(source, file.getString("remote")))
                    throw new IOException("框架服务未连接或发布失败，请启用模块后重试");
            }
            // Re-publish the configuration snapshot after the files are available.
            ModuleSettings.republishCameraConfiguration(app);
            status = "作品已重新发布。重启游戏后装载。";
        });
    }

    static synchronized boolean remove(Context context, String generation) {
        Context app = context.getApplicationContext();
        return run(() -> {
            JSONObject removed = requireEntry(app, generation);
            JSONArray previous = index(app), next = new JSONArray();
            for (int i = 0; i < previous.length(); i++) {
                JSONObject entry = previous.getJSONObject(i);
                if (!generation.equals(entry.getString("generation"))) next.put(entry);
            }
            // Withdraw the index before deleting any advertised bytes.
            if (!FrameworkSettings.open(app).edit().putString(INDEX, next.toString()).commit())
                throw new IOException("作品索引保存失败");
            if (removed.getString("folder").equals(ModuleSettings.getMmdWork(app))) {
                ModuleSettings.setMmdSettings(app, "", ModuleSettings.isMmdLoop(app),
                        ModuleSettings.isMmdMusicEnabled(app),
                        ModuleSettings.parse(ModuleSettings.getMmdSeekSeconds(app), 5),
                        ModuleSettings.parse(ModuleSettings.getMmdMusicGain(app), 1),
                        ModuleSettings.parse(ModuleSettings.getMmdAudioOffset(app), 0));
            }
            boolean complete = true;
            JSONArray files = removed.getJSONArray("files");
            for (int i = 0; i < files.length(); i++)
                complete &= FrameworkSettings.removeMmd(files.getJSONObject(i).getString("remote"));
            complete &= deleteOwned(ownedFolder(app, removed.getString("folder")));
            status = "作品已移除。重启游戏后刷新。"
                    + (complete ? "" : "部分文件暂未清理，但该作品已停止发布。");
        });
    }

    @FunctionalInterface private interface Operation { void run() throws Exception; }

    private static boolean run(Operation operation) {
        if (busy) return false;
        busy = true;
        status = "正在准备作品文件…";
        try {
            WORKER.execute(() -> {
                try { operation.run(); }
                catch (Exception error) {
                    status = "操作未完成：" + (error instanceof SecurityException
                            ? "目录读取授权失效，请重新选择。"
                            : (error.getMessage() == null ? error.getClass().getSimpleName()
                                    : error.getMessage()));
                    android.util.Log.e("BetterEndfield.Mmd", status, error);
                } finally { busy = false; }
            });
            return true;
        } catch (RuntimeException rejected) {
            busy = false;
            throw rejected;
        }
    }

    private static void releaseGrant(Context app, Uri tree) {
        try {
            app.getContentResolver().releasePersistableUriPermission(tree,
                    android.content.Intent.FLAG_GRANT_READ_URI_PERMISSION);
        } catch (SecurityException ignored) { }
    }

    private static void install(Context app, Uri tree) throws Exception {
        if (index(app).length() >= 512) throw new IOException("作品库最多支持 512 个作品");
        Map<String, Uri> children = children(app, tree);
        Uri setUri = children.get("set.ini");
        if (setUri == null) throw new IOException("目录根部缺少 set.ini");
        byte[] setBytes;
        try (InputStream input = app.getContentResolver().openInputStream(setUri)) {
            if (input == null) throw new IOException("无法读取 set.ini");
            ByteArrayOutputStream output = new ByteArrayOutputStream();
            copy(input, output, 16 * 1024);
            setBytes = output.toByteArray();
        }
        String text = StandardCharsets.UTF_8.newDecoder()
                .onMalformedInput(CodingErrorAction.REPORT)
                .onUnmappableCharacter(CodingErrorAction.REPORT)
                .decode(ByteBuffer.wrap(setBytes)).toString();
        if (text.startsWith("\uFEFF")) text = text.substring(1);
        Map<String, String> settings = parseSet(text);
        if (settings.getOrDefault("motion", "").isEmpty()
                && settings.getOrDefault("camera", "").isEmpty())
            throw new IOException("set.ini 至少需要 motion 或 camera VMD");
        Set<String> required = new LinkedHashSet<>();
        required.add("set.ini");
        Set<String> vmdFiles = new LinkedHashSet<>();
        for (Map.Entry<String, String> setting : settings.entrySet()) {
            String key = setting.getKey();
            boolean vmd = key.matches("motion[2-4]?|face[2-4]?|camera");
            if (!vmd && !"music".equals(key)) continue;
            String name = setting.getValue();
            if (name.isEmpty()) continue;
            requirePlainName(name);
            if ("set.ini".equals(name)) throw new IOException("资源不能引用 set.ini");
            if (!children.containsKey(name)) throw new IOException("缺少引用文件：" + name);
            required.add(name);
            if (vmd) vmdFiles.add(name);
        }
        String generation = UUID.randomUUID().toString();
        File root = new File(app.getFilesDir(), "mmd");
        if (!root.isDirectory() && !root.mkdirs()) throw new IOException("无法创建作品库目录");
        File stage = new File(root, ".stage-" + generation);
        if (!stage.mkdir()) throw new IOException("无法创建临时作品目录");
        JSONArray files = new JSONArray();
        boolean advertised = false;
        try {
            long total = 0;
            int ordinal = 0;
            for (String name : required) {
                status = "正在复制 " + (ordinal + 1) + "/" + required.size() + "：" + name;
                File destination = new File(stage, name);
                long bytes;
                try (FileOutputStream output = new FileOutputStream(destination)) {
                    if ("set.ini".equals(name)) {
                        output.write(setBytes);
                        bytes = setBytes.length;
                    } else {
                        try (InputStream input = app.getContentResolver().openInputStream(children.get(name))) {
                            if (input == null) throw new IOException("无法读取：" + name);
                            bytes = copy(input, output, Math.min(MAX_FILE_BYTES, MAX_WORK_BYTES - total));
                        }
                    }
                    output.getFD().sync();
                }
                if (bytes == 0) throw new IOException("文件为空：" + name);
                total += bytes;
                if (vmdFiles.contains(name)) verifyVmd(destination);
                String tag = "set.ini".equals(name) ? "set.ini"
                        : "f" + Integer.toHexString(ordinal) + (vmdFiles.contains(name) ? ".vmd" : ".audio");
                files.put(new JSONObject().put("remote", "mmd-" + generation + "-" + tag)
                        .put("name", name).put("bytes", bytes));
                ordinal++;
            }
            File installed = ownedFolder(app, generation);
            if (!stage.renameTo(installed)) throw new IOException("无法发布本地作品目录");
            stage = installed;
            for (int i = 0; i < files.length(); i++) {
                JSONObject file = files.getJSONObject(i);
                status = "正在发布 " + (i + 1) + "/" + files.length() + "：" + file.getString("name");
                if (!FrameworkSettings.publishMmd(new File(installed, file.getString("name")),
                        file.getString("remote")))
                    throw new IOException("框架服务未连接或发布失败；请启用模块后重新导入");
            }
            String name = settings.getOrDefault("name", "");
            if (name.isEmpty()) name = treeName(app, tree);
            JSONObject entry = new JSONObject().put("generation", generation).put("folder", generation)
                    .put("name", name).put("files", files);
            JSONArray next = index(app);
            next.put(entry);
            if (!FrameworkSettings.open(app).edit().putString(INDEX, next.toString()).commit())
                throw new IOException("作品索引保存失败");
            advertised = true;
            status = "已导入：" + name + "。请选择启动作品，重启游戏后装载。";
        } finally {
            if (!advertised) {
                for (int i = 0; i < files.length(); i++)
                    FrameworkSettings.removeMmd(files.getJSONObject(i).getString("remote"));
                deleteOwned(stage);
            }
        }
    }

    private static Map<String, Uri> children(Context app, Uri tree) throws IOException {
        Uri listing = DocumentsContract.buildChildDocumentsUriUsingTree(tree,
                DocumentsContract.getTreeDocumentId(tree));
        Map<String, Uri> result = new LinkedHashMap<>();
        String[] projection = {DocumentsContract.Document.COLUMN_DOCUMENT_ID,
                DocumentsContract.Document.COLUMN_DISPLAY_NAME, DocumentsContract.Document.COLUMN_MIME_TYPE};
        try (Cursor cursor = app.getContentResolver().query(listing, projection, null, null, null)) {
            if (cursor == null) throw new IOException("无法列出目录文件");
            int count = 0;
            while (cursor.moveToNext()) {
                if (++count > 4096) throw new IOException("作品目录文件过多");
                if (DocumentsContract.Document.MIME_TYPE_DIR.equals(cursor.getString(2))) continue;
                String name = cursor.getString(1);
                Uri child = DocumentsContract.buildDocumentUriUsingTree(tree, cursor.getString(0));
                if (result.put(name, child) != null) throw new IOException("目录包含重名文件：" + name);
            }
        }
        return result;
    }

    private static String treeName(Context app, Uri tree) {
        Uri document = DocumentsContract.buildDocumentUriUsingTree(tree, DocumentsContract.getTreeDocumentId(tree));
        try (Cursor cursor = app.getContentResolver().query(document,
                new String[]{DocumentsContract.Document.COLUMN_DISPLAY_NAME}, null, null, null)) {
            if (cursor != null && cursor.moveToFirst() && cursor.getString(0) != null)
                return cursor.getString(0);
        } catch (RuntimeException ignored) { }
        return "MMD 作品";
    }

    static Map<String, String> parseSet(String text) throws IOException {
        Map<String, String> values = new LinkedHashMap<>();
        for (String raw : text.split("\n")) {
            String line = raw.trim();
            if (line.isEmpty() || line.startsWith(";") || line.startsWith("#") || line.startsWith("[")) continue;
            int equals = line.indexOf('=');
            if (equals < 0) continue;
            String key = line.substring(0, equals).trim().toLowerCase(Locale.ROOT);
            String value = line.substring(equals + 1).trim();
            if (key.matches("motion[0-9]+|face[0-9]+") && !key.matches("motion[2-4]|face[2-4]"))
                throw new IOException("最多支持 motion、motion2–4 和对应表情文件");
            if (values.put(key, value) != null) throw new IOException("set.ini 包含重复字段：" + key);
        }
        return values;
    }

    static void requirePlainName(String name) throws IOException {
        if (name == null || name.isEmpty() || name.equals(".") || name.equals("..")
                || name.getBytes(StandardCharsets.UTF_8).length > 200)
            throw new IOException("资源必须是作品目录内的单层文件名");
        for (int i = 0; i < name.length(); i++) {
            char c = name.charAt(i);
            if (c < 32 || c == 127 || c == '/' || c == '\\' || c == ':')
                throw new IOException("不支持子目录或绝对路径：" + name);
        }
    }

    private static void verifyVmd(File file) throws IOException {
        byte[] header = new byte[30];
        try (InputStream input = new FileInputStream(file)) {
            int count = 0, read;
            while (count < header.length && (read = input.read(header, count, header.length - count)) != -1)
                count += read;
            String signature = new String(header, StandardCharsets.US_ASCII);
            if (count != header.length || file.length() < 50 || !signature.startsWith("Vocaloid Motion Data"))
                throw new IOException("不是有效的 VMD 文件：" + file.getName());
        }
    }

    private static long copy(InputStream input, java.io.OutputStream output, long limit) throws IOException {
        byte[] buffer = new byte[65536];
        long bytes = 0;
        int count;
        while ((count = input.read(buffer)) != -1) {
            bytes += count;
            if (bytes > limit) throw new IOException("作品文件超过大小限制");
            output.write(buffer, 0, count);
        }
        return bytes;
    }

    private static JSONObject requireEntry(Context app, String generation) throws Exception {
        JSONArray entries = index(app);
        for (int i = 0; i < entries.length(); i++) {
            JSONObject entry = entries.getJSONObject(i);
            if (generation.equals(entry.getString("generation"))) return entry;
        }
        throw new IOException("作品已被移除，请刷新后重试");
    }

    private static File ownedFolder(Context app, String folder) throws IOException {
        if (!folder.matches("[a-f0-9-]{36}")) throw new IOException("无效的作品目录编号");
        File root = new File(app.getFilesDir(), "mmd").getCanonicalFile();
        File result = new File(root, folder).getCanonicalFile();
        if (!root.equals(result.getParentFile())) throw new IOException("作品目录超出应用存储范围");
        return result;
    }

    private static boolean deleteOwned(File folder) {
        boolean complete = true;
        File[] children = folder.listFiles();
        if (children != null) for (File file : children) complete &= file.delete();
        return folder.delete() && complete;
    }
}
