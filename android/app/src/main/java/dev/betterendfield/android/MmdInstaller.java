package dev.betterendfield.android;

import android.content.Context;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;
import java.util.LinkedHashMap;
import java.util.Locale;
import java.util.Map;
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
    static volatile String status = "";
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

    static synchronized boolean start(Context context, MmdImportSession session,
                                      String name, Map<String, String> slots,
                                      Map<String, String> settings) {
        if (busy || session.busy || session.plan == null || session.cancelled) return false;
        Context app = context.getApplicationContext();
        MmdImportPlan plan = session.plan;
        Map<String, String> selected = new LinkedHashMap<>(slots);
        Map<String, String> options = new LinkedHashMap<>(settings);
        session.busy = true;
        try {
            return run(() -> {
                try { install(app, session, plan, name, selected, options); session.installed = true; }
                catch (Exception error) { session.status = "导入失败：" + error.getMessage(); throw error; }
                finally { session.busy = false; if (session.cancelled) session.dispose(); }
            });
        } catch (RuntimeException error) { session.busy = false; throw error; }
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
            status = "作品已重新发布";
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
            status = "作品已移除"
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

    private static void install(Context app, MmdImportSession session, MmdImportPlan plan,
                                String name, Map<String, String> slots,
                                Map<String, String> settings) throws Exception {
        if (index(app).length() >= 512) throw new IOException("作品库最多 512 个作品");
        plan.validate(name, slots);
        Map<String, String> filenames = new LinkedHashMap<>();
        for (String slot : MmdImportPlan.SLOTS) {
            String path = slots.get(slot);
            if (path != null && !path.isEmpty() && !filenames.containsKey(path)) {
                String extension = "music".equals(slot) ? extension(path) : ".vmd";
                filenames.put(path, "f" + filenames.size() + extension);
            }
        }
        byte[] setBytes = plan.generate(name, slots, settings, filenames);
        String generation = UUID.randomUUID().toString();
        File root = new File(app.getFilesDir(), "mmd");
        if (!root.isDirectory() && !root.mkdirs()) throw new IOException("无法创建作品库目录");
        File stage = new File(root, ".stage-" + generation);
        if (!stage.mkdir()) throw new IOException("无法创建临时作品目录");
        JSONArray files = new JSONArray();
        boolean advertised = false;
        try {
            long total = setBytes.length;
            try (FileOutputStream output = new FileOutputStream(new File(stage, "set.ini"))) {
                output.write(setBytes); output.getFD().sync();
            }
            files.put(payload(generation, "set.ini", setBytes.length));
            for (Map.Entry<String, String> item : filenames.entrySet()) {
                String path = item.getKey(), filename = item.getValue();
                session.check("复制：" + path); status = session.status;
                File source = MmdImportArchive.target(plan.root, path), destination = new File(stage, filename);
                long bytes;
                try (InputStream input = new FileInputStream(source); FileOutputStream output = new FileOutputStream(destination)) {
                    bytes = copy(input, output, Math.min(MmdImportArchive.limit(path), MAX_WORK_BYTES - total), session);
                    if (bytes != source.length() || bytes == 0) throw new IOException("资料长度异常：" + path);
                    output.getFD().sync();
                }
                if (filename.endsWith(".vmd")) {
                    MmdVmdParser.Sections sections = MmdVmdParser.read(destination);
                    for (Map.Entry<String, String> slot : slots.entrySet())
                        if (path.equals(slot.getValue()) && !sections.supports(slot.getKey())) throw new IOException("VMD 类型不符");
                }
                total += bytes; files.put(payload(generation, filename, bytes));
            }
            session.check("正在发布…");
            File installed = ownedFolder(app, generation);
            if (!stage.renameTo(installed)) throw new IOException("无法发布本地作品目录");
            stage = installed;
            for (int i = 0; i < files.length(); i++) {
                JSONObject file = files.getJSONObject(i);
                session.check("发布：" + (i + 1) + "/" + files.length()); status = session.status;
                if (!FrameworkSettings.publishMmd(new File(installed, file.getString("name")), file.getString("remote")))
                    throw new IOException("框架服务未连接或发布失败");
            }
            JSONObject entry = new JSONObject().put("generation", generation).put("folder", generation)
                    .put("name", name.trim()).put("files", files);
            JSONArray next = index(app); next.put(entry);
            synchronized (session) {
                session.check("正在完成…");
                if (!FrameworkSettings.open(app).edit().putString(INDEX, next.toString()).commit())
                    throw new IOException("作品索引保存失败");
                advertised = true;
                session.installed = true;
            }
            session.status = status = "已导入：" + name.trim();
        } finally {
            if (!advertised) {
                for (int i = 0; i < files.length(); i++) FrameworkSettings.removeMmd(files.getJSONObject(i).getString("remote"));
                deleteOwned(stage);
            }
        }
    }

    private static JSONObject payload(String generation, String name, long bytes) throws Exception {
        return new JSONObject().put("remote", "mmd-" + generation + "-" + name).put("name", name).put("bytes", bytes);
    }
    private static String extension(String path) {
        String name = MmdImportPlan.base(path); int dot = name.lastIndexOf('.');
        return dot < 0 ? ".audio" : name.substring(dot).toLowerCase(Locale.ROOT);
    }
    static Map<String, String> parseSet(String text) throws IOException { return MmdImportPlan.parseSet(text); }

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

    private static long copy(InputStream input, java.io.OutputStream output, long limit,
                             MmdImportSession session) throws IOException {
        byte[] buffer = new byte[65536]; long bytes = 0; int count;
        while ((count = input.read(buffer)) != -1) {
            session.check(session.status); bytes += count;
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
