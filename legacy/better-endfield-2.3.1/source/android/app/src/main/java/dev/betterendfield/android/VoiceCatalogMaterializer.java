package dev.betterendfield.android;

import android.content.Context;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.ByteArrayOutputStream;
import java.io.EOFException;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.RandomAccessFile;
import java.nio.charset.StandardCharsets;
import java.nio.file.AtomicMoveNotSupportedException;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;
import java.security.MessageDigest;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Set;
import java.util.TreeMap;
import java.util.TreeSet;

/** Builds Android-specific BEVCAT01 files from the target app's downloaded CHKs. */
final class VoiceCatalogMaterializer {
    private static final String INDEX_ASSET = "voice-catalog-index.json";
    private static final String[] LANGUAGE_NAMES = {
            "Chinese", "English", "Japanese", "Korean"
    };
    private static final int CATALOG_HEADER_SIZE = 36;
    private static final int CATALOG_ENTRY_SIZE = 24;
    private static final long UINT32_MAX = 0xffff_ffffL;
    private static final int XOR_CONSTANT = 0x9C5A0B29;
    private static final int MULTIPLY_CONSTANT = 81861667;

    record Result(int requested, int generated, int reused, List<String> failures) {
        String summary() {
            return "requested=" + requested + " generated=" + generated +
                    " reused=" + reused + " failed=" + failures.size() +
                    (failures.isEmpty() ? "" : " first_error=" + failures.get(0));
        }
    }

    private VoiceCatalogMaterializer() {}

    static Result prepare(
            Context targetContext,
            Context moduleContext,
            String serializedRules) throws Exception {
        LinkedHashMap<String, Integer> configured = parseRules(serializedRules);
        if (configured.isEmpty()) {
            return new Result(0, 0, 0, List.of());
        }

        byte[] indexBytes = readAsset(moduleContext, INDEX_ASSET);
        String indexIdentity = hex(MessageDigest.getInstance("SHA-256").digest(indexBytes));
        JSONObject root = new JSONObject(new String(indexBytes, StandardCharsets.UTF_8));
        if (root.optInt("schemaVersion") != 1 ||
                !"betterendfield-voice-catalog-index".equals(root.optString("kind"))) {
            throw new IOException("内置语气词对照表版本不受支持");
        }

        Map<Integer, String> packageFolders = readPackageFolders(root.getJSONArray("packages"));
        Map<String, RouteSet> availableRoutes = readSelectedRouteSets(
                root.getJSONArray("catalogs"), configured);
        List<RouteSet> selected = new ArrayList<>();
        List<String> failures = new ArrayList<>();
        for (Map.Entry<String, Integer> rule : configured.entrySet()) {
            String key = routeKey(rule.getKey(), rule.getValue());
            RouteSet routeSet = availableRoutes.get(key);
            if (routeSet == null) {
                failures.add(rule.getKey() + "/" + languageName(rule.getValue()) +
                        ": 旧表中没有该目录");
            } else {
                selected.add(routeSet);
            }
        }

        File externalRoot = targetContext.getExternalFilesDir(null);
        if (externalRoot == null) {
            throw new IOException("游戏外部数据目录不可用");
        }
        File catalogRoot = new File(targetContext.getFilesDir(), "betterendfield/catalog");
        if (!catalogRoot.isDirectory() && !catalogRoot.mkdirs()) {
            throw new IOException("无法创建 catalog 目录：" + catalogRoot);
        }

        Map<Integer, List<RouteSet>> byLanguage = new LinkedHashMap<>();
        for (RouteSet routeSet : selected) {
            byLanguage.computeIfAbsent(routeSet.language, ignored -> new ArrayList<>())
                    .add(routeSet);
        }

        int generated = 0;
        int reused = 0;
        for (Map.Entry<Integer, List<RouteSet>> group : byLanguage.entrySet()) {
            int language = group.getKey();
            String folder = packageFolders.get(language);
            if (folder == null || folder.isEmpty()) {
                for (RouteSet routeSet : group.getValue()) {
                    failures.add(routeSet.characterId + "/" + languageName(language) +
                            ": 旧表缺少语言包分区");
                }
                continue;
            }

            Set<Long> requiredTargets = new HashSet<>();
            for (RouteSet routeSet : group.getValue()) {
                requiredTargets.addAll(routeSet.targets());
            }

            PckIndex packageIndex;
            try {
                packageIndex = locatePackage(
                        new File(new File(externalRoot, "VFS"), folder), requiredTargets);
            } catch (Exception error) {
                for (RouteSet routeSet : group.getValue()) {
                    failures.add(routeSet.characterId + "/" + languageName(language) +
                            ": " + error.getMessage());
                }
                continue;
            }

            for (RouteSet routeSet : group.getValue()) {
                try {
                    File output = new File(catalogRoot,
                            catalogFileName(routeSet.language, routeSet.characterId));
                    if (isCurrentCatalog(
                            output, indexIdentity, packageIndex, routeSet)) {
                        reused++;
                    } else {
                        buildCatalog(output, indexIdentity, packageIndex, routeSet);
                        generated++;
                    }
                } catch (Exception error) {
                    failures.add(routeSet.characterId + "/" +
                            languageName(routeSet.language) + ": " + error.getMessage());
                }
            }
        }
        return new Result(configured.size(), generated, reused, List.copyOf(failures));
    }

    private static LinkedHashMap<String, Integer> parseRules(String value) {
        LinkedHashMap<String, Integer> result = new LinkedHashMap<>();
        if (value == null || value.isEmpty()) {
            return result;
        }
        for (String item : value.split(";")) {
            int separator = item.indexOf(':');
            if (separator <= 0 || separator + 1 >= item.length()) {
                continue;
            }
            String speaker = item.substring(0, separator).toLowerCase(Locale.ROOT);
            int language = languageIndex(item.substring(separator + 1));
            if (("*".equals(speaker) || speaker.matches("chr_[a-z0-9_]+")) &&
                    language >= 0) {
                result.put(speaker, language);
            }
        }
        return result;
    }

    private static Map<Integer, String> readPackageFolders(JSONArray packages) throws Exception {
        Map<Integer, String> result = new HashMap<>();
        for (int index = 0; index < packages.length(); ++index) {
            JSONObject descriptor = packages.getJSONObject(index);
            int language = descriptor.getInt("language");
            String source = descriptor.getString("source").replace('\\', '/');
            String marker = "/VFS/";
            int start = source.toUpperCase(Locale.ROOT).indexOf(marker);
            if (start < 0) {
                continue;
            }
            start += marker.length();
            int end = source.indexOf('/', start);
            if (end > start) {
                result.put(language, source.substring(start, end));
            }
        }
        return result;
    }

    private static Map<String, RouteSet> readSelectedRouteSets(
            JSONArray catalogs,
            Map<String, Integer> configured) throws Exception {
        Set<String> wanted = new HashSet<>();
        for (Map.Entry<String, Integer> rule : configured.entrySet()) {
            wanted.add(routeKey(rule.getKey(), rule.getValue()));
        }

        Map<String, RouteSet> result = new HashMap<>();
        for (int index = 0; index < catalogs.length(); ++index) {
            JSONObject item = catalogs.getJSONObject(index);
            String characterId = item.getString("characterId").toLowerCase(Locale.ROOT);
            int language = item.getInt("language");
            String key = routeKey(characterId, language);
            if (!wanted.contains(key)) {
                continue;
            }
            JSONArray encoded = item.getJSONArray("routes");
            if (encoded.length() == 0 || (encoded.length() & 1) != 0) {
                throw new IOException("内置语气词对照表损坏：" + key);
            }
            TreeMap<Long, Long> routes = new TreeMap<>();
            for (int route = 0; route < encoded.length(); route += 2) {
                long source = encoded.getLong(route);
                long target = encoded.getLong(route + 1);
                if (source <= 0 || source > UINT32_MAX ||
                        target <= 0 || target > UINT32_MAX) {
                    throw new IOException("Media ID 越界：" + key);
                }
                Long previous = routes.putIfAbsent(source, target);
                if (previous != null && previous != target) {
                    throw new IOException("Media 路由冲突：" + key + "/" + source);
                }
            }
            result.put(key, new RouteSet(characterId, language, routes));
        }
        return result;
    }

    private static PckIndex locatePackage(File languageRoot, Set<Long> targets)
            throws Exception {
        File[] candidates = languageRoot.listFiles(file ->
                file.isFile() && file.getName().toLowerCase(Locale.ROOT).endsWith(".chk"));
        if (candidates == null || candidates.length == 0) {
            throw new IOException("未下载对应语言包（" + languageRoot + "）");
        }
        Arrays.sort(candidates, Comparator.comparingLong(File::length).reversed());
        List<String> rejected = new ArrayList<>();
        for (File candidate : candidates) {
            try {
                PckIndex index = readPck(candidate);
                if (index.media.keySet().containsAll(targets)) {
                    return index;
                }
                int missing = 0;
                for (Long target : targets) {
                    if (!index.media.containsKey(target)) missing++;
                }
                rejected.add(candidate.getName() + " 缺少 " + missing + " 个 Media");
            } catch (IOException error) {
                rejected.add(candidate.getName() + "：" + error.getMessage());
            }
        }
        throw new IOException("当前设备语言包与旧表不匹配；" +
                String.join("；", rejected));
    }

    private static PckIndex readPck(File path) throws Exception {
        byte[] prefix = new byte[12];
        try (InputStream input = new BufferedInputStream(new FileInputStream(path))) {
            readExactly(input, prefix);
        }
        if (!hasMagic(prefix, "AKPK") && !hasMagic(prefix, ":)xD")) {
            throw new IOException("不是 Endfield PCK");
        }
        long encodedHeaderSize = readU32(prefix, 4);
        if (encodedHeaderSize < 16 || encodedHeaderSize > 64L * 1024 * 1024) {
            throw new IOException("PCK header size 不受支持");
        }
        int headerLength = Math.toIntExact(encodedHeaderSize + 8);
        byte[] header = new byte[headerLength];
        try (InputStream input = new BufferedInputStream(new FileInputStream(path))) {
            readExactly(input, header);
        }
        if (hasMagic(header, ":)xD")) {
            decryptVfs(header, 12, Math.toIntExact(encodedHeaderSize - 4),
                    (int) encodedHeaderSize);
            byte[] magic = "AKPK".getBytes(StandardCharsets.US_ASCII);
            System.arraycopy(magic, 0, header, 0, magic.length);
            writeU32(header, 8, 1);
        }
        if (!hasMagic(header, "AKPK")) {
            throw new IOException("PCK header 解密失败");
        }

        Cursor cursor = new Cursor(4);
        long parsedHeaderSize = readU32(header, cursor);
        readU32(header, cursor);
        long languagesSize = readU32(header, cursor);
        long banksSize = readU32(header, cursor);
        long soundsSize = readU32(header, cursor);
        long externalsSize = 0;
        if (languagesSize + banksSize + soundsSize + 0x10L < parsedHeaderSize) {
            externalsSize = readU32(header, cursor);
        }

        int languagesStart = cursor.position;
        int banksStart = checkedAdd(languagesStart, languagesSize);
        int soundsStart = checkedAdd(banksStart, banksSize);
        int externalsStart = checkedAdd(soundsStart, soundsSize);
        ensureSector(header, languagesStart, languagesSize);
        ensureSector(header, banksStart, banksSize);
        ensureSector(header, soundsStart, soundsSize);
        ensureSector(header, externalsStart, externalsSize);

        Map<Long, PckMedia> media = new HashMap<>();
        parseMediaSector(header, soundsStart, soundsSize, false, media);
        parseMediaSector(header, externalsStart, externalsSize, true, media);
        String headerHash = hex(MessageDigest.getInstance("SHA-256").digest(header));
        return new PckIndex(path, path.length(), path.lastModified(), headerHash, media);
    }

    private static void parseMediaSector(
            byte[] header,
            int start,
            long size,
            boolean external,
            Map<Long, PckMedia> media) throws IOException {
        if (size == 0) return;
        Cursor cursor = new Cursor(start);
        long count = readU32(header, cursor);
        if (count == 0) return;
        long payloadSize = size - 4;
        if (payloadSize < 0 || payloadSize % count != 0) {
            throw new IOException("PCK Media sector 未对齐");
        }
        long entrySize = payloadSize / count;
        if (entrySize != 20 && entrySize != 24) {
            throw new IOException("PCK Media entry size 不受支持：" + entrySize);
        }
        for (long index = 0; index < count; ++index) {
            int entryStart = cursor.position;
            long fileId = readU32(header, cursor);
            if (entrySize == 24 && external) {
                fileId |= readU32(header, cursor) << 32;
            }
            long blockSize = readU32(header, cursor);
            long fileSize = entrySize == 24 && !external
                    ? readU64(header, cursor) : readU32(header, cursor);
            long fileOffset = readU32(header, cursor);
            readU32(header, cursor);
            if (blockSize != 0) {
                fileOffset = Math.multiplyExact(fileOffset, blockSize);
            }
            if (fileId <= UINT32_MAX && fileSize <= UINT32_MAX) {
                media.putIfAbsent(fileId, new PckMedia(fileOffset, fileSize));
            }
            cursor.position = Math.toIntExact((long) entryStart + entrySize);
        }
    }

    private static boolean isCurrentCatalog(
            File output,
            String indexIdentity,
            PckIndex packageIndex,
            RouteSet routeSet) {
        File reportFile = new File(output.getPath() + ".json");
        if (!output.isFile() || !reportFile.isFile()) return false;
        try {
            JSONObject report = new JSONObject(new String(
                    readFile(reportFile), StandardCharsets.UTF_8));
            return report.optInt("schemaVersion") == 1 &&
                    "betterendfield-voice-catalog".equals(report.optString("kind")) &&
                    "android".equals(report.optString("platform")) &&
                    routeSet.characterId.equalsIgnoreCase(report.optString("characterId")) &&
                    routeSet.language == report.optInt("language", -1) &&
                    routeSet.routes.size() == report.optInt("entryCount", -1) &&
                    indexIdentity.equalsIgnoreCase(report.optString("catalogIndexSha256")) &&
                    packageIndex.path.getAbsolutePath().equals(report.optString("sourcePackage")) &&
                    packageIndex.size == report.optLong("sourcePackageSize", -1) &&
                    packageIndex.lastModified == report.optLong("sourcePackageLastModified", -1) &&
                    packageIndex.headerSha256.equalsIgnoreCase(
                            report.optString("sourcePackageHeaderSha256")) &&
                    output.length() == report.optLong("catalogLength", -1);
        } catch (Exception ignored) {
            return false;
        }
    }

    private static void buildCatalog(
            File output,
            String indexIdentity,
            PckIndex packageIndex,
            RouteSet routeSet) throws Exception {
        TreeSet<Long> targets = new TreeSet<>(routeSet.routes.values());
        for (Long target : targets) {
            if (!packageIndex.media.containsKey(target)) {
                throw new IOException("语言包缺少 Media ID " + target);
            }
        }

        long dataOffset = Math.addExact(
                CATALOG_HEADER_SIZE,
                Math.multiplyExact((long) CATALOG_ENTRY_SIZE, routeSet.routes.size()));
        long cursor = dataOffset;
        Map<Long, Long> payloadOffsets = new HashMap<>();
        for (Long target : targets) {
            payloadOffsets.put(target, cursor);
            cursor = Math.addExact(cursor, packageIndex.media.get(target).size);
        }

        File temporary = new File(output.getPath() + "." +
                Long.toUnsignedString(System.nanoTime()) + ".tmp");
        try {
            try (FileOutputStream rawOutput = new FileOutputStream(temporary);
                 BufferedOutputStream stream = new BufferedOutputStream(rawOutput, 1024 * 1024);
                 RandomAccessFile source = new RandomAccessFile(packageIndex.path, "r")) {
                stream.write("BEVCAT01".getBytes(StandardCharsets.US_ASCII));
                writeU16(stream, 1);
                writeU16(stream, routeSet.language);
                writeU32(stream, routeSet.routes.size());
                writeU32(stream, 0);
                writeU64(stream, CATALOG_HEADER_SIZE);
                writeU64(stream, dataOffset);
                for (Map.Entry<Long, Long> route : routeSet.routes.entrySet()) {
                    PckMedia media = packageIndex.media.get(route.getValue());
                    writeU32(stream, route.getKey());
                    writeU32(stream, route.getValue());
                    writeU64(stream, payloadOffsets.get(route.getValue()));
                    writeU32(stream, media.size);
                    writeU32(stream, 0);
                }

                for (Long target : targets) {
                    PckMedia media = packageIndex.media.get(target);
                    if (media.size > Integer.MAX_VALUE ||
                            media.offset < 0 ||
                            media.offset + media.size > packageIndex.size) {
                        throw new IOException("PCK Media ID " + target + " 越界");
                    }
                    byte[] payload = new byte[(int) media.size];
                    source.seek(media.offset);
                    source.readFully(payload);
                    decryptVfs(payload, 0, payload.length, (int) (long) target);
                    stream.write(payload);
                }
                stream.flush();
                rawOutput.getFD().sync();
            }
            moveReplace(temporary, output);

            JSONObject report = new JSONObject();
            report.put("schemaVersion", 1);
            report.put("kind", "betterendfield-voice-catalog");
            report.put("platform", "android");
            report.put("characterId", routeSet.characterId);
            report.put("language", routeSet.language);
            report.put("targetLanguage", languageName(routeSet.language));
            report.put("entryCount", routeSet.routes.size());
            report.put("uniqueTargetMediaCount", targets.size());
            report.put("catalogIndexSha256", indexIdentity);
            report.put("sourcePackage", packageIndex.path.getAbsolutePath());
            report.put("sourcePackageSize", packageIndex.size);
            report.put("sourcePackageLastModified", packageIndex.lastModified);
            report.put("sourcePackageHeaderSha256", packageIndex.headerSha256);
            report.put("catalogLength", output.length());
            writeAtomically(new File(output.getPath() + ".json"),
                    report.toString(2).getBytes(StandardCharsets.UTF_8));
        } finally {
            if (temporary.exists()) temporary.delete();
        }
    }

    private static void writeAtomically(File output, byte[] bytes) throws IOException {
        File temporary = new File(output.getPath() + "." +
                Long.toUnsignedString(System.nanoTime()) + ".tmp");
        try {
            try (FileOutputStream stream = new FileOutputStream(temporary)) {
                stream.write(bytes);
                stream.flush();
                stream.getFD().sync();
            }
            moveReplace(temporary, output);
        } finally {
            if (temporary.exists()) temporary.delete();
        }
    }

    private static void moveReplace(File source, File destination) throws IOException {
        try {
            Files.move(source.toPath(), destination.toPath(),
                    StandardCopyOption.ATOMIC_MOVE,
                    StandardCopyOption.REPLACE_EXISTING);
        } catch (AtomicMoveNotSupportedException unsupported) {
            Files.move(source.toPath(), destination.toPath(),
                    StandardCopyOption.REPLACE_EXISTING);
        }
    }

    private static byte[] readAsset(Context context, String name) throws IOException {
        try (InputStream input = context.getAssets().open(name);
             ByteArrayOutputStream output = new ByteArrayOutputStream()) {
            byte[] buffer = new byte[8192];
            int count;
            while ((count = input.read(buffer)) >= 0) {
                output.write(buffer, 0, count);
            }
            return output.toByteArray();
        }
    }

    private static byte[] readFile(File path) throws IOException {
        if (path.length() > Integer.MAX_VALUE) throw new IOException("文件过大");
        byte[] bytes = new byte[(int) path.length()];
        try (InputStream input = new FileInputStream(path)) {
            readExactly(input, bytes);
        }
        return bytes;
    }

    private static void readExactly(InputStream input, byte[] output) throws IOException {
        int position = 0;
        while (position < output.length) {
            int count = input.read(output, position, output.length - position);
            if (count < 0) throw new EOFException("文件被截断");
            position += count;
        }
    }

    private static void decryptVfs(byte[] data, int offset, int length, int seed) {
        int position = 0;
        int keyIndex = seed;
        while (length - position >= 4) {
            int absolute = offset + position;
            int value = (int) readU32(data, absolute);
            writeU32(data, absolute, value ^ deriveKey(keyIndex));
            position += 4;
            keyIndex++;
        }
        if (position < length) {
            int key = deriveKey(keyIndex);
            for (int index = 0; position + index < length; ++index) {
                data[offset + position + index] ^= (byte) (key >>> (index * 8));
            }
        }
    }

    private static int deriveKey(int seed) {
        int key = ((seed & 0xff) ^ XOR_CONSTANT) * MULTIPLY_CONSTANT;
        key = (key ^ ((seed >>> 8) & 0xff)) * MULTIPLY_CONSTANT;
        key = (key ^ ((seed >>> 16) & 0xff)) * MULTIPLY_CONSTANT;
        return (key ^ ((seed >>> 24) & 0xff)) * MULTIPLY_CONSTANT;
    }

    private static boolean hasMagic(byte[] data, String magic) {
        byte[] expected = magic.getBytes(StandardCharsets.US_ASCII);
        if (data.length < expected.length) return false;
        for (int index = 0; index < expected.length; ++index) {
            if (data[index] != expected[index]) return false;
        }
        return true;
    }

    private static void ensureSector(byte[] data, int start, long size) throws IOException {
        if (start < 0 || size < 0 || start + size > data.length) {
            throw new IOException("PCK header sector 越界");
        }
    }

    private static int checkedAdd(int start, long size) throws IOException {
        long result = start + size;
        if (result < 0 || result > Integer.MAX_VALUE) {
            throw new IOException("PCK header sector 过大");
        }
        return (int) result;
    }

    private static long readU32(byte[] data, Cursor cursor) throws IOException {
        long value = readU32(data, cursor.position);
        cursor.position += 4;
        return value;
    }

    private static long readU64(byte[] data, Cursor cursor) throws IOException {
        if (cursor.position < 0 || cursor.position + 8 > data.length) {
            throw new IOException("PCK header 被截断");
        }
        long low = readU32(data, cursor.position);
        long high = readU32(data, cursor.position + 4);
        cursor.position += 8;
        if ((high & 0x8000_0000L) != 0) {
            throw new IOException("PCK 64-bit 字段超出 Android 支持范围");
        }
        return low | (high << 32);
    }

    private static long readU32(byte[] data, int position) {
        return ((long) data[position] & 0xff) |
                (((long) data[position + 1] & 0xff) << 8) |
                (((long) data[position + 2] & 0xff) << 16) |
                (((long) data[position + 3] & 0xff) << 24);
    }

    private static void writeU32(byte[] data, int position, long value) {
        data[position] = (byte) value;
        data[position + 1] = (byte) (value >>> 8);
        data[position + 2] = (byte) (value >>> 16);
        data[position + 3] = (byte) (value >>> 24);
    }

    private static void writeU16(BufferedOutputStream output, long value) throws IOException {
        output.write((int) value & 0xff);
        output.write((int) (value >>> 8) & 0xff);
    }

    private static void writeU32(BufferedOutputStream output, long value) throws IOException {
        output.write((int) value & 0xff);
        output.write((int) (value >>> 8) & 0xff);
        output.write((int) (value >>> 16) & 0xff);
        output.write((int) (value >>> 24) & 0xff);
    }

    private static void writeU64(BufferedOutputStream output, long value) throws IOException {
        writeU32(output, value);
        writeU32(output, value >>> 32);
    }

    private static String routeKey(String characterId, int language) {
        return characterId.toLowerCase(Locale.ROOT) + "\u0000" + language;
    }

    private static int languageIndex(String value) {
        for (int index = 0; index < LANGUAGE_NAMES.length; ++index) {
            if (LANGUAGE_NAMES[index].equalsIgnoreCase(value)) return index;
        }
        return -1;
    }

    private static String languageName(int language) {
        return language >= 0 && language < LANGUAGE_NAMES.length
                ? LANGUAGE_NAMES[language] : "Unknown";
    }

    private static String catalogFileName(int language, String characterId) {
        String prefix = "voice." + languageName(language).toLowerCase(Locale.ROOT);
        return "*".equals(characterId)
                ? prefix + ".becat"
                : prefix + "." + characterId.toLowerCase(Locale.ROOT) + ".becat";
    }

    private static String hex(byte[] bytes) {
        StringBuilder result = new StringBuilder(bytes.length * 2);
        for (byte value : bytes) {
            result.append(String.format(Locale.ROOT, "%02X", value & 0xff));
        }
        return result.toString();
    }

    private static final class Cursor {
        int position;
        Cursor(int position) { this.position = position; }
    }

    private record PckMedia(long offset, long size) {}

    private record PckIndex(
            File path,
            long size,
            long lastModified,
            String headerSha256,
            Map<Long, PckMedia> media) {}

    private record RouteSet(
            String characterId,
            int language,
            TreeMap<Long, Long> routes) {
        Set<Long> targets() { return new HashSet<>(routes.values()); }
    }
}
