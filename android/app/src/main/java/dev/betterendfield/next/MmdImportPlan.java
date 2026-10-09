package dev.betterendfield.next;

import java.io.*;
import java.nio.ByteBuffer;
import java.nio.charset.*;
import java.util.*;

/** Cache inspection and editable work proposals; this class does not install anything. */
final class MmdImportPlan {
    static final String[] SLOTS = {"motion", "face", "camera", "music", "motion2", "face2",
            "motion3", "face3", "motion4", "face4"};
    static final class Asset {
        final String path;
        final MmdVmdParser.Sections vmd;
        Asset(String path, MmdVmdParser.Sections vmd) { this.path = path; this.vmd = vmd; }
        boolean supports(String slot) { return vmd == null ? "music".equals(slot) : vmd.supports(slot); }
        String display() { return path.replaceFirst("^import-[a-f0-9-]{36}/", ""); }
        String label() { return display() + (vmd == null ? " · 音乐" : " · " + vmd.label().trim()); }
    }
    static final class Work {
        String name;
        final Map<String, String> slots = new LinkedHashMap<>();
        final Map<String, String> settings = new LinkedHashMap<>();
        Work(String name) { this.name = name; }
    }
    final File root;
    final List<Asset> assets = new ArrayList<>();
    final List<Work> works = new ArrayList<>();
    final List<String> warnings = new ArrayList<>();
    private MmdImportPlan(File root) { this.root = root; }
    static MmdImportPlan inspect(File root) throws IOException {
        MmdImportPlan plan = new MmdImportPlan(root);
        List<String> paths = new ArrayList<>(); scan(root, root, paths, 0, new MmdImportArchive.Budget());
        Collections.sort(paths);
        List<String> sets = new ArrayList<>();
        for (String path : paths) {
            String lower = path.toLowerCase(Locale.ROOT);
            if (lower.endsWith(".vmd")) {
                try { plan.assets.add(new Asset(path, MmdVmdParser.read(MmdImportArchive.target(root, path)))); }
                catch (IOException invalid) { plan.warnings.add(path + "：" + invalid.getMessage()); }
            } else if (lower.matches(".*\\.(mp3|wav|ogg|m4a|aac|flac|opus)")) plan.assets.add(new Asset(path, null));
            else if (base(path).equalsIgnoreCase("set.ini")) sets.add(path);
        }
        Set<String> covered = new HashSet<>();
        for (String set : sets) {
            try {
                String directory = parent(set);
                Map<String, String> settings = parseSet(decode(readSmall(MmdImportArchive.target(root, set))));
                Work work = new Work(settings.getOrDefault("name", directory.isEmpty() ? "MMD" : base(directory)));
                work.settings.putAll(settings);
                for (String slot : SLOTS) {
                    String ref = settings.getOrDefault(slot, "");
                    if (ref.isEmpty()) continue;
                    String path = MmdImportArchive.path(ref);
                    path = directory.isEmpty() ? path : directory + "/" + path;
                    Asset asset = plan.asset(path);
                    if (asset == null || !asset.supports(slot)) throw new IOException(slot + " 引用缺失或类型不符：" + ref);
                    work.slots.put(slot, asset.path);
                }
                requirePlayable(work.slots);
                plan.works.add(work); covered.add(directory);
            } catch (IOException invalid) { plan.warnings.add(set + "：" + invalid.getMessage()); }
        }
        List<Asset> unassigned = new ArrayList<>();
        for (Asset asset : plan.assets) {
            boolean inside = false;
            for (String directory : covered) if (directory.isEmpty() || asset.path.startsWith(directory + "/")) inside = true;
            if (!inside) unassigned.add(asset);
        }
        List<Asset> motions = new ArrayList<>();
        for (Asset asset : unassigned) if (asset.supports("motion")) motions.add(asset);
        if (motions.isEmpty()) {
            for (Asset asset : unassigned) if (asset.supports("camera")) {
                Work work = new Work(stem(asset.path)); work.slots.put("camera", asset.path);
                plan.unique(work, unassigned, "music", parent(asset.path)); plan.works.add(work);
            }
        } else for (Asset motion : motions) {
            Work work = new Work(stem(motion.path)); work.slots.put("motion", motion.path);
            String scope = motions.size() == 1 ? null : parent(motion.path);
            int peers = 0;
            for (Asset other : motions) if (parent(other.path).equals(parent(motion.path))) peers++;
            // A combined bone+morph file is also a valid face source.
            if (motion.supports("face")) work.slots.put("face", motion.path);
            else if (peers == 1) plan.unique(work, unassigned, "face", scope);
            if (peers == 1) {
                plan.unique(work, unassigned, "camera", scope);
                plan.unique(work, unassigned, "music", scope);
            }
            plan.works.add(work);
        }
        if (plan.works.isEmpty()) plan.works.add(new Work("MMD"));
        if (plan.works.size() > 512) throw new IOException("识别作品超过 512 个");
        return plan;
    }
    private void unique(Work work, List<Asset> assets, String slot, String scope) {
        Asset only = null;
        for (Asset asset : assets) if (asset.supports(slot) && (scope == null || parent(asset.path).equals(scope))) {
            if (only != null) return;
            only = asset;
        }
        if (only != null) work.slots.put(slot, only.path);
    }
    Asset asset(String path) {
        for (Asset asset : assets) if (asset.path.equals(path)) return asset;
        return null;
    }
    void validate(String name, Map<String, String> slots) throws IOException {
        safeValue(name); if (name.trim().isEmpty() || name.length() > 120) throw new IOException("请输入作品名（最多 120 字）");
        requirePlayable(slots); long total = 0; Set<String> used = new HashSet<>();
        for (String slot : SLOTS) {
            String path = slots.get(slot); if (path == null || path.isEmpty()) continue;
            Asset asset = asset(path);
            if (asset == null || !asset.supports(slot)) throw new IOException("资源类型不符：" + slot);
            File file = MmdImportArchive.target(root, path);
            if (!file.isFile() || file.length() <= 0 || file.length() > MmdImportArchive.limit(path)) throw new IOException("资源缺失或长度异常：" + path);
            if (used.add(path)) total += file.length();
        }
        if (total > MmdImportArchive.MAX_TOTAL - 16384) throw new IOException("作品超过 1 GiB");
    }
    byte[] generate(String name, Map<String, String> slots, Map<String, String> extra,
                    Map<String, String> filenames) throws IOException {
        validate(name, slots);
        StringBuilder ini = new StringBuilder("name=").append(name.trim()).append('\n');
        for (String slot : SLOTS) {
            String path = slots.get(slot);
            if (path != null && !path.isEmpty()) ini.append(slot).append('=').append(filenames.get(path)).append('\n');
        }
        // Keep playback options from a supplied set.ini, replacing only resource paths/name.
        for (Map.Entry<String, String> option : extra.entrySet()) {
            if (option.getKey().equals("name") || Arrays.asList(SLOTS).contains(option.getKey())) continue;
            safeValue(option.getKey()); safeValue(option.getValue());
            ini.append(option.getKey()).append('=').append(option.getValue()).append('\n');
        }
        byte[] bytes = ini.toString().getBytes(StandardCharsets.UTF_8);
        if (bytes.length > 16384) throw new IOException("set.ini 过大");
        return bytes;
    }
    static Map<String, String> parseSet(String text) throws IOException {
        Map<String, String> values = new LinkedHashMap<>();
        for (String raw : text.replace("\uFEFF", "").split("\n")) {
            String line = raw.trim();
            if (line.isEmpty() || line.startsWith(";") || line.startsWith("#") || line.startsWith("[")) continue;
            int equals = line.indexOf('='); if (equals < 0) continue;
            String key = line.substring(0, equals).trim().toLowerCase(Locale.ROOT), value = line.substring(equals + 1).trim();
            if (key.matches("motion[0-9]+|face[0-9]+") && !key.matches("motion[2-4]|face[2-4]")) throw new IOException("仅支持动作/表情 1–4");
            safeValue(key); safeValue(value);
            if (values.put(key, value) != null) throw new IOException("重复字段：" + key);
        }
        return values;
    }
    private static void requirePlayable(Map<String, String> slots) throws IOException {
        if (slots.getOrDefault("motion", "").isEmpty() && slots.getOrDefault("camera", "").isEmpty())
            throw new IOException("请选择动作或镜头");
    }
    private static void safeValue(String value) throws IOException {
        if (value == null) throw new IOException("缺少配置值");
        for (int i = 0; i < value.length(); i++) if (value.charAt(i) < 32 || value.charAt(i) == 127) throw new IOException("配置值包含控制字符");
    }
    private static void scan(File root, File directory, List<String> paths, int depth, MmdImportArchive.Budget budget) throws IOException {
        if (depth > MmdImportArchive.MAX_DEPTH) throw new IOException("目录层级过深");
        File[] files = directory.listFiles(); if (files == null) throw new IOException("无法读取缓存资料");
        for (File file : files) {
            if (java.nio.file.Files.isSymbolicLink(file.toPath())) throw new IOException("不支持符号链接");
            String path = root.toPath().relativize(file.toPath()).toString().replace('\\', '/');
            MmdImportArchive.path(path); budget.entry(path);
            if (file.isDirectory()) scan(root, file, paths, depth + 1, budget);
            else {
                budget.bytes += file.length();
                if (budget.bytes > MmdImportArchive.MAX_TOTAL || file.length() > MmdImportArchive.limit(path)) throw new IOException("资料长度超限：" + path);
                paths.add(path);
            }
        }
    }
    static String parent(String path) { int slash = path.lastIndexOf('/'); return slash < 0 ? "" : path.substring(0, slash); }
    static String base(String path) { return path.substring(path.lastIndexOf('/') + 1); }
    static String stem(String path) { String name = base(path); int dot = name.lastIndexOf('.'); return dot < 0 ? name : name.substring(0, dot); }
    private static byte[] readSmall(File file) throws IOException {
        if (file.length() > 16384) throw new IOException("set.ini 过大");
        try (InputStream in = new FileInputStream(file); ByteArrayOutputStream out = new ByteArrayOutputStream()) {
            byte[] buffer = new byte[1024]; int count;
            while ((count = in.read(buffer)) != -1) { if (out.size() + count > 16384) throw new IOException("set.ini 过大"); out.write(buffer, 0, count); }
            return out.toByteArray();
        }
    }
    private static String decode(byte[] bytes) throws IOException {
        try { return StandardCharsets.UTF_8.newDecoder().onMalformedInput(CodingErrorAction.REPORT).decode(ByteBuffer.wrap(bytes)).toString(); }
        catch (CharacterCodingException invalid) {
            return Charset.forName("GBK").newDecoder().onMalformedInput(CodingErrorAction.REPORT).decode(ByteBuffer.wrap(bytes)).toString();
        }
    }
}
