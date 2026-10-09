package dev.betterendfield.next;

import java.io.*;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.util.*;
import java.util.zip.CheckedInputStream;
import java.util.zip.CRC32;
import java.util.zip.ZipEntry;
import java.util.zip.ZipFile;

/** Stages BEM bytes under private, generated filenames; ZIP paths are labels only. */
final class BemImportArchive {
    static final long MAX_ZIP_BYTES = 8L * 1024 * 1024 * 1024;
    static final long MAX_TOTAL_BYTES = MAX_ZIP_BYTES;
    static final int MAX_ENTRIES = 4096, MAX_PACKAGES = 256;
    private static final long MAX_DIRECTORY_BYTES = 16L * 1024 * 1024;
    interface Progress { void update(String text) throws IOException; }
    static final class Item {
        final String name;
        final File directory, file;
        Item(String name, File directory) {
            this.name = name; this.directory = directory; this.file = new File(directory, "installed.bem");
        }
    }
    static final class Bundle {
        final boolean zip;
        final List<Item> packages = new ArrayList<>();
        final List<String> issues = new ArrayList<>();
        Bundle(boolean zip) { this.zip = zip; }
    }
    private BemImportArchive() {}

    static Bundle read(InputStream source, File stage, BemImportStream.Checkpoint checkpoint,
            Progress progress) throws IOException {
        PushbackInputStream input = new PushbackInputStream(source, 8);
        byte[] header = new byte[8]; int filled = 0;
        while (filled < header.length) {
            checkpoint.check();
            int count = input.read(header, filled, header.length - filled);
            if (count < 0) break;
            if (count == 0) {
                int next = input.read(); if (next < 0) break;
                header[filled++] = (byte) next;
            } else filled += count;
        }
        input.unread(header, 0, filled);
        if (filled == 8 && BemImportStream.matchesHeader(header)) {
            Bundle bundle = new Bundle(false);
            Item item = item(stage, "BEM");
            try (FileOutputStream output = new FileOutputStream(item.file)) {
                BemImportStream.copy(input, output, BemImportStream.MAX_BYTES, checkpoint);
                output.getFD().sync();
            }
            bundle.packages.add(item); return bundle;
        }
        boolean zip = filled >= 4 && header[0] == 'P' && header[1] == 'K'
                && ((header[2] == 3 && header[3] == 4) || (header[2] == 5 && header[3] == 6));
        if (!zip) throw new IOException("请选择 BEM 文件或包含 BEM 的 ZIP；修改扩展名不能转换格式。");
        File archive = new File(stage, "input.zip");
        try {
            progress.update("正在读取 ZIP 合集…");
            try (FileOutputStream output = new FileOutputStream(archive)) {
                copyZip(input, output, checkpoint); output.getFD().sync();
            }
            return unpack(archive, stage, checkpoint, progress, MAX_TOTAL_BYTES, MAX_PACKAGES);
        } finally { archive.delete(); }
    }

    private static void copyZip(InputStream input, OutputStream output,
            BemImportStream.Checkpoint checkpoint) throws IOException {
        byte[] buffer = new byte[65536]; long total = 0;
        while (true) {
            checkpoint.check(); int count = input.read(buffer);
            if (count < 0) break;
            if (count == 0) {
                int value = input.read(); if (value < 0) break;
                buffer[0] = (byte) value; count = 1;
            }
            if (count > MAX_ZIP_BYTES - total) throw new IOException("ZIP 超过 8 GiB 大小上限。");
            output.write(buffer, 0, count); total += count;
        }
        checkpoint.check();
    }

    static Bundle unpack(File archive, File stage, BemImportStream.Checkpoint checkpoint,
            Progress progress, long totalLimit, int packageLimit) throws IOException {
        if (totalLimit < 8 || packageLimit < 1) throw new IllegalArgumentException("Invalid ZIP limits");
        checkpoint.check(); int expectedEntries = checkDirectory(archive);
        Bundle bundle = new Bundle(true); long[] consumed = {0}; int entries = 0, packages = 0;
        Set<String> names = new HashSet<>();
        try (ZipFile zip = openZip(archive)) {
            Enumeration<? extends ZipEntry> contents = zip.entries();
            while (contents.hasMoreElements()) {
                checkpoint.check(); ZipEntry entry = contents.nextElement();
                if (++entries > MAX_ENTRIES) throw new IOException("ZIP 文件数超过 4096。");
                if (entry.isDirectory() || !entry.getName().toLowerCase(Locale.ROOT).endsWith(".bem")) continue;
                if (++packages > packageLimit) throw new IOException("ZIP 中的 BEM 数量超过 " + packageLimit + " 个。");
                Item item = null;
                try {
                    String path = checkedName(entry.getName());
                    if (!names.add(path.toLowerCase(Locale.ROOT))) throw new IOException("ZIP 中存在重复文件路径。");
                    if (entry.getMethod() != ZipEntry.STORED && entry.getMethod() != ZipEntry.DEFLATED)
                        throw new IOException("不支持此 ZIP 条目的压缩方式，请使用存储或 Deflate。");
                    if (entry.getSize() < 0 || entry.getSize() > BemImportStream.MAX_BYTES)
                        throw new IOException("ZIP 内 BEM 超过 2 GiB 大小上限。");
                    if (entry.getSize() > totalLimit - consumed[0]) throw new IOException("ZIP 内 BEM 的总大小超过导入上限。");
                    item = item(stage, path); progress.update("正在读取：" + path);
                    CRC32 crc = new CRC32(); long bytes;
                    long allowance = Math.min(BemImportStream.MAX_BYTES, totalLimit - consumed[0]);
                    try (InputStream input = new CheckedInputStream(budget(zip.getInputStream(entry), consumed, totalLimit), crc);
                            FileOutputStream output = new FileOutputStream(item.file)) {
                        bytes = BemImportStream.copy(input, output, allowance, checkpoint);
                        output.getFD().sync();
                    }
                    // ZipFile streams do not promise a CRC check of extracted bytes.
                    if (bytes != entry.getSize() || crc.getValue() != entry.getCrc())
                        throw new IOException("ZIP 条目长度或 CRC 校验失败。");
                    bundle.packages.add(item);
                } catch (IOException error) {
                    checkpoint.check(); // Cancellation aborts preparation instead of becoming an invalid item.
                    if (item != null) { item.file.delete(); item.directory.delete(); }
                    bundle.issues.add(label(entry.getName()) + "：" + error.getMessage());
                }
            }
        }
        if (entries != expectedEntries) throw new IOException("ZIP 目录记录数量不匹配。");
        if (packages == 0) throw new IOException("ZIP 中没有找到 BEM 文件。");
        return bundle;
    }
    private static InputStream budget(InputStream input, long[] consumed, long limit) {
        return new FilterInputStream(input) {
            private void charge(int count) throws IOException {
                if (count > limit - consumed[0]) throw new IOException("ZIP 内 BEM 的总大小超过导入上限。");
                consumed[0] += count;
            }
            @Override public int read() throws IOException {
                int value = in.read(); if (value >= 0) charge(1); return value;
            }
            @Override public int read(byte[] bytes, int offset, int length) throws IOException {
                int count = in.read(bytes, offset, length); if (count > 0) charge(count); return count;
            }
        };
    }

    private static Item item(File stage, String name) throws IOException {
        File directory = new File(stage, "package-" + UUID.randomUUID());
        if (!directory.mkdir()) throw new IOException("无法创建模型包临时目录。");
        return new Item(name, directory);
    }
    private static String checkedName(String raw) throws IOException {
        String name = raw.replace('\\', '/');
        if (name.isEmpty() || name.length() > 1024 || name.startsWith("/") || name.contains(":"))
            throw new IOException("ZIP 条目路径不合法。");
        String[] parts = name.split("/", -1);
        if (parts.length > 32) throw new IOException("ZIP 目录层级超过 32。");
        for (String part : parts) {
            if (part.isEmpty() || part.equals(".") || part.equals("..")) throw new IOException("ZIP 条目路径不合法。");
            for (int i = 0; i < part.length(); i++) if (part.charAt(i) < 32 || part.charAt(i) == 127)
                throw new IOException("ZIP 条目文件名不合法。");
        }
        return name;
    }
    private static String label(String raw) {
        String value = raw.replaceAll("[\\p{Cntrl}]", "?");
        return value.length() > 200 ? value.substring(0, 200) + "…" : value;
    }
    private static ZipFile openZip(File archive) throws IOException {
        try { return new ZipFile(archive, StandardCharsets.UTF_8); }
        catch (IOException | IllegalArgumentException utf8) {
            // EFS entries still use UTF-8. This fallback supports legacy Chinese ZIP names.
            try { return new ZipFile(archive, Charset.forName("GB18030")); }
            catch (IOException | IllegalArgumentException fallback) {
                throw new IOException("ZIP 文件损坏、加密或编码不支持。", fallback);
            }
        }
    }

    // Bound the directory count before ZipFile allocates all central entries.
    // ZIP64 is needed for large collections even when their entry count is small.
    private static int checkDirectory(File archive) throws IOException {
        try (RandomAccessFile file = new RandomAccessFile(archive, "r")) {
            long length = file.length();
            if (length < 22 || length > MAX_ZIP_BYTES) throw new IOException("ZIP 长度不合法。");
            long begin = Math.max(0, length - 65557); byte[] tail = new byte[(int)(length - begin)];
            file.seek(begin); file.readFully(tail); int end = -1;
            for (int i = tail.length - 22; i >= 0; i--) {
                if (u32(tail, i) == 0x06054b50L && i + 22 + u16(tail, i + 20) == tail.length) { end = i; break; }
            }
            if (end < 0) throw new IOException("ZIP 目录缺失或不完整。");
            if (u16(tail, end + 4) != 0 || u16(tail, end + 6) != 0)
                throw new IOException("不支持分卷 ZIP。");
            long count = u16(tail, end + 10), diskCount = u16(tail, end + 8);
            long size = u32(tail, end + 12), offset = u32(tail, end + 16), directoryEnd = begin + end;
            if (count == 0xffff || diskCount == 0xffff || size == 0xffffffffL || offset == 0xffffffffL) {
                long locator = begin + end - 20;
                if (locator < 0) throw new IOException("ZIP64 目录缺失。");
                byte[] record = new byte[56], location = new byte[20];
                file.seek(locator); file.readFully(location);
                long recordOffset = u64(location, 8);
                if (u32(location, 0) != 0x07064b50L || u32(location, 4) != 0 || u32(location, 16) != 1
                        || recordOffset < 0 || recordOffset > locator - 56) throw new IOException("ZIP64 目录不合法。");
                file.seek(recordOffset); file.readFully(record);
                long recordSize = u64(record, 4);
                if (u32(record, 0) != 0x06064b50L || recordSize < 44 || recordSize > locator - recordOffset - 12
                        || u32(record, 16) != 0 || u32(record, 20) != 0) throw new IOException("ZIP64 目录不合法。");
                diskCount = u64(record, 24); count = u64(record, 32); size = u64(record, 40); offset = u64(record, 48);
                directoryEnd = recordOffset;
            }
            if (count < 0 || count > MAX_ENTRIES || diskCount != count)
                throw new IOException("ZIP 文件数超过 4096 或目录不合法。");
            if (offset < 0 || size < 0 || offset > directoryEnd || size > directoryEnd - offset)
                throw new IOException("ZIP 目录长度不合法。");
            if (size > MAX_DIRECTORY_BYTES) throw new IOException("ZIP 目录信息超过大小上限。");
            long position=offset, limit=offset+size;byte[] entry=new byte[46];
            for(int i=0;i<count;i++) {
                if(position>limit-entry.length) throw new IOException("ZIP 目录记录不完整。");
                file.seek(position);file.readFully(entry);
                if(u32(entry,0)!=0x02014b50L) throw new IOException("ZIP 目录记录不合法。");
                long next=position+46L+u16(entry,28)+u16(entry,30)+u16(entry,32);
                if(next>limit) throw new IOException("ZIP 目录记录长度不合法。");
                position=next;
            }
            if(position!=limit) {
                byte[] signature=new byte[6];
                if(position>limit-6) throw new IOException("ZIP 目录记录数量不匹配。");
                file.seek(position);file.readFully(signature);
                if(u32(signature,0)!=0x05054b50L || position+6+u16(signature,4)!=limit)
                    throw new IOException("ZIP 目录记录数量不匹配。");
            }
            return (int)count;
        }
    }
    private static int u16(byte[] bytes, int offset) { return (bytes[offset] & 255) | ((bytes[offset + 1] & 255) << 8); }
    private static long u32(byte[] bytes, int offset) { return (u16(bytes, offset) & 65535L) | ((long)u16(bytes, offset + 2) << 16); }
    private static long u64(byte[] bytes, int offset) { return u32(bytes, offset) | (u32(bytes, offset + 4) << 32); }
}
