package dev.betterendfield.next;

import org.apache.commons.compress.archivers.zip.ZipArchiveEntry;
import org.apache.commons.compress.archivers.zip.ZipFile;
import org.apache.commons.compress.archivers.sevenz.SevenZArchiveEntry;
import org.apache.commons.compress.archivers.sevenz.SevenZFile;

import java.io.*;
import java.util.HashSet;
import java.util.Locale;
import java.util.Set;
import java.util.zip.CRC32;
import java.nio.ByteBuffer;
import java.nio.charset.StandardCharsets;
import java.nio.charset.CodingErrorAction;
import java.nio.charset.CharacterCodingException;
import java.nio.charset.Charset;

/** Bounded extraction into a new, private cache directory. */
final class MmdImportArchive {
    static final long MAX_TOTAL = 1024L * 1024 * 1024;
    static final long MAX_AUDIO = 512L * 1024 * 1024;
    static final int MAX_ENTRIES = 4096, MAX_DEPTH = 16;
    interface Progress { void update(String text) throws IOException; }
    static final class Budget {
        int entries; long bytes;
        final Set<String> names = new HashSet<>();
        void entry(String path) throws IOException {
            if (++entries > MAX_ENTRIES) throw new IOException("文件数超过 4096");
            if (!names.add(path.toLowerCase(Locale.ROOT))) throw new IOException("重复路径：" + path);
        }
    }
    static String path(String raw) throws IOException {
        if (raw == null) throw new IOException("缺少文件名");
        String path = raw.replace('\\', '/');
        if (path.endsWith("/")) path = path.substring(0, path.length() - 1);
        if (path.isEmpty() || path.length() > 1024 || path.startsWith("/") || path.contains(":"))
            throw new IOException("无效资源路径");
        String[] parts = path.split("/", -1);
        if (parts.length > MAX_DEPTH) throw new IOException("目录层级超过 16");
        for (String part : parts) {
            if (part.isEmpty() || part.equals(".") || part.equals("..")) throw new IOException("无效资源路径：" + path);
            for (int i = 0; i < part.length(); i++) if (part.charAt(i) < 32 || part.charAt(i) == 127)
                throw new IOException("无效文件名");
        }
        return path;
    }
    static File target(File root, String path) throws IOException {
        File file = new File(root, path).getCanonicalFile();
        if (!file.getPath().startsWith(root.getCanonicalPath() + File.separator)) throw new IOException("资源路径越界");
        return file;
    }
    static long limit(String path) {
        String lower = path.toLowerCase(Locale.ROOT);
        return lower.equals("archive") ? MAX_TOTAL : lower.endsWith(".vmd") ? MmdVmdParser.MAX_BYTES
                : lower.endsWith("set.ini") ? 16384 : MAX_AUDIO;
    }
    static void extract(File archive, File root, Progress progress) throws IOException {
        byte[] signature = new byte[8]; int length;
        try (InputStream in = new FileInputStream(archive)) { length = in.read(signature); }
        Budget budget = new Budget();
        if (length >= 4 && signature[0] == 'P' && signature[1] == 'K') zip(archive, root, budget, progress);
        else if (length >= 6 && signature[0] == '7' && signature[1] == 'z'
                && (signature[2] & 255) == 0xbc && (signature[3] & 255) == 0xaf
                && signature[4] == 0x27 && signature[5] == 0x1c) seven(archive, root, budget, progress);
        else if (length >= 4 && signature[0] == 'R' && signature[1] == 'a' && signature[2] == 'r')
            throw new IOException("不支持 RAR，请使用 ZIP 或 7z");
        else throw new IOException("请选择 ZIP 或 7z 压缩包");
    }
    private static void zip(File archive, File root, Budget budget, Progress progress) throws IOException {
        checkZipDirectory(archive);
        // Lossless decoding while reading the directory permits mixed unflagged UTF-8/GBK.
        // EFS and Unicode extra fields are still interpreted by Commons Compress.
        try (ZipFile zip = ZipFile.builder().setFile(archive).setCharset("ISO-8859-1").get()) {
            java.util.Enumeration<ZipArchiveEntry> entries = zip.getEntries();
            while (entries.hasMoreElements()) {
                ZipArchiveEntry entry = entries.nextElement();
                String path = path(zipName(entry)); budget.entry(path); progress.update("解压：" + path);
                if (entry.getGeneralPurposeBit().usesEncryption() || entry.getGeneralPurposeBit().usesStrongEncryption()) throw new IOException("不支持加密压缩包");
                if (entry.getMethod() == 20 || entry.getMethod() == 93) throw new IOException("不支持 Zstandard ZIP 条目：" + path);
                if (entry.isUnixSymlink() || !zip.canReadEntryData(entry)) throw new IOException("不支持的 ZIP 条目：" + path);
                File file = target(root, path);
                if (entry.isDirectory()) {
                    if (entry.getSize() != 0) throw new IOException("目录条目长度异常");
                    mkdir(file); continue;
                }
                checkSize(entry.getSize(), path, budget);
                CRC32 crc = new CRC32();
                try (InputStream in = zip.getInputStream(entry)) {
                    copy(in, file, path, entry.getSize(), budget, progress, crc);
                }
                if (entry.getCrc() < 0 || crc.getValue() != entry.getCrc()) throw new IOException("ZIP 校验失败：" + path);
            }
        }
    }
    // Check counts before Commons Compress allocates its central-directory entries.
    private static void checkZipDirectory(File archive) throws IOException {
        try (RandomAccessFile in = new RandomAccessFile(archive, "r")) {
            long length = in.length(), start = Math.max(0, length - 65557), end = -1;
            if (length < 22) throw new IOException("ZIP 长度异常");
            byte[] tail = new byte[(int) (length - start)]; in.seek(start); in.readFully(tail);
            for (int i = tail.length - 22; i >= 0; i--) {
                if (tail[i] == 'P' && tail[i + 1] == 'K' && tail[i + 2] == 5 && tail[i + 3] == 6
                        && i + 22 + ((tail[i + 20] & 255) | ((tail[i + 21] & 255) << 8)) == tail.length) {
                    end = start + i; break;
                }
            }
            if (end < 0) throw new IOException("ZIP 目录缺失");
            in.seek(end + 4);
            int disk = Short.toUnsignedInt(Short.reverseBytes(in.readShort()));
            int centralDisk = Short.toUnsignedInt(Short.reverseBytes(in.readShort()));
            long diskCount = Short.toUnsignedInt(Short.reverseBytes(in.readShort()));
            long count = Short.toUnsignedInt(Short.reverseBytes(in.readShort()));
            long bytes = Integer.toUnsignedLong(Integer.reverseBytes(in.readInt()));
            long offset = Integer.toUnsignedLong(Integer.reverseBytes(in.readInt()));
            if (count == 65535 || bytes == 0xffffffffL || offset == 0xffffffffL) {
                if (end < 20) throw new IOException("ZIP64 目录缺失");
                in.seek(end - 20);
                if (Integer.reverseBytes(in.readInt()) != 0x07064b50 || Integer.reverseBytes(in.readInt()) != 0)
                    throw new IOException("不支持分卷 ZIP");
                long zip64 = Long.reverseBytes(in.readLong());
                if (Integer.reverseBytes(in.readInt()) != 1 || zip64 < 0 || zip64 > end - 76) throw new IOException("ZIP64 长度异常");
                in.seek(zip64);
                if (Integer.reverseBytes(in.readInt()) != 0x06064b50) throw new IOException("ZIP64 目录异常");
                long record = Long.reverseBytes(in.readLong());
                if (record < 44 || record > end - 20 - zip64 - 12) throw new IOException("ZIP64 长度异常");
                in.skipBytes(4);
                disk = Integer.reverseBytes(in.readInt()); centralDisk = Integer.reverseBytes(in.readInt());
                diskCount = Long.reverseBytes(in.readLong()); count = Long.reverseBytes(in.readLong());
                bytes = Long.reverseBytes(in.readLong()); offset = Long.reverseBytes(in.readLong());
            }
            if (disk != 0 || centralDisk != 0 || diskCount != count) throw new IOException("不支持分卷 ZIP");
            if (count < 0 || count > MAX_ENTRIES || bytes < 0 || bytes > 16L * 1024 * 1024
                    || offset < 0 || offset > end || bytes > end - offset) throw new IOException("ZIP 目录长度或文件数超限");
            long position = offset, centralEnd = offset + bytes, actual = 0;
            while (position < centralEnd) {
                if (centralEnd - position < 6) throw new IOException("ZIP 目录截断");
                in.seek(position); int signature = Integer.reverseBytes(in.readInt());
                if (signature == 0x05054b50) { // Optional central-directory digital signature.
                    int size = Short.toUnsignedInt(Short.reverseBytes(in.readShort()));
                    if (position + 6 + size != centralEnd) throw new IOException("ZIP 签名长度异常");
                    position = centralEnd; break;
                }
                if (signature != 0x02014b50 || centralEnd - position < 46 || ++actual > MAX_ENTRIES)
                    throw new IOException("ZIP 目录条目异常或过多");
                in.seek(position + 28);
                int name = Short.toUnsignedInt(Short.reverseBytes(in.readShort()));
                int extra = Short.toUnsignedInt(Short.reverseBytes(in.readShort()));
                int comment = Short.toUnsignedInt(Short.reverseBytes(in.readShort()));
                long entry = 46L + name + extra + comment;
                if (name == 0 || name > 4096 || entry > centralEnd - position) throw new IOException("ZIP 条目长度异常");
                position += entry;
            }
            if (actual != count) throw new IOException("ZIP 文件数声明异常");
        }
    }
    private static void seven(File archive, File root, Budget budget, Progress progress) throws IOException {
        try (SevenZFile seven = SevenZFile.builder().setFile(archive).setMaxMemoryLimitKiB(65536).get()) {
            SevenZArchiveEntry entry;
            while ((entry = seven.getNextEntry()) != null) {
                String path = path(entry.getName()); budget.entry(path); progress.update("解压：" + path);
                File file = target(root, path);
                if (entry.isAntiItem()) throw new IOException("不支持 7z 删除条目");
                if (entry.getHasWindowsAttributes() && ((entry.getWindowsAttributes() >>> 16) & 0170000) == 0120000)
                    throw new IOException("不支持符号链接");
                if (entry.isDirectory()) {
                    if (entry.getSize() != 0 || entry.hasStream()) throw new IOException("目录条目长度异常");
                    mkdir(file); continue;
                }
                checkSize(entry.getSize(), path, budget);
                InputStream in = new InputStream() {
                    @Override public int read() throws IOException { return seven.read(); }
                    @Override public int read(byte[] b, int off, int len) throws IOException { return seven.read(b, off, len); }
                };
                copy(in, file, path, entry.getSize(), budget, progress, null);
            }
        } catch (org.apache.commons.compress.PasswordRequiredException encrypted) {
            throw new IOException("不支持加密压缩包", encrypted);
        }
    }
    private static String zipName(ZipArchiveEntry entry) throws IOException {
        if (!entry.getGeneralPurposeBit().usesUTF8ForNames()
                && entry.getNameSource() != ZipArchiveEntry.NameSource.UNICODE_EXTRA_FIELD
                && entry.getRawName() != null) {
            try {
                return StandardCharsets.UTF_8.newDecoder().onMalformedInput(CodingErrorAction.REPORT)
                        .decode(ByteBuffer.wrap(entry.getRawName())).toString();
            } catch (CharacterCodingException gbk) {
                return Charset.forName("GBK").newDecoder().onMalformedInput(CodingErrorAction.REPORT)
                        .decode(ByteBuffer.wrap(entry.getRawName())).toString();
            }
        }
        return entry.getName();
    }
    private static void checkSize(long size, String path, Budget budget) throws IOException {
        if (size < 0 || size > limit(path) || size > MAX_TOTAL - budget.bytes) throw new IOException("资源长度超限：" + path);
    }
    static void copy(InputStream in, File file, String path, long expected, Budget budget,
                     Progress progress, CRC32 crc) throws IOException {
        if (expected < -1) throw new IOException("资源长度异常：" + path);
        if (expected >= 0) checkSize(expected, path, budget);
        mkdir(file.getParentFile()); long size = 0;
        try (FileOutputStream out = new FileOutputStream(file)) {
            byte[] buffer = new byte[65536]; int count;
            while ((count = in.read(buffer)) != -1) {
                progress.update("复制：" + path);
                size += count; budget.bytes += count;
                if (size > limit(path) || budget.bytes > MAX_TOTAL || (expected >= 0 && size > expected))
                    throw new IOException("资源长度超限：" + path);
                out.write(buffer, 0, count); if (crc != null) crc.update(buffer, 0, count);
            }
            if (expected >= 0 && size != expected) throw new IOException("资源长度异常：" + path);
        }
    }
    static void mkdir(File file) throws IOException {
        if (!file.isDirectory() && !file.mkdirs()) throw new IOException("无法创建缓存目录");
    }
    static void delete(File file) {
        if (java.nio.file.Files.isSymbolicLink(file.toPath())) { file.delete(); return; }
        File[] children = file.listFiles();
        if (children != null) for (File child : children) delete(child);
        file.delete();
    }
}
