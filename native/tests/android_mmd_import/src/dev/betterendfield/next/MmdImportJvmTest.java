package dev.betterendfield.next;

import org.apache.commons.compress.archivers.zip.*;
import org.apache.commons.compress.archivers.sevenz.*;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.util.*;

/** Real production parsers/extractors, synthetic VMD records and actual ZIP/7z codecs. */
public final class MmdImportJvmTest {
    public static void main(String[] args) throws Exception {
        File root = Files.createTempDirectory("be-mmd-import-tests-").toFile();
        try {
            classification(root);
            nested(root, "UTF-8"); nested(root, "GBK");
            unflaggedUtf8(root);
            multiWorks(root);
            slots(root);
            seven(root);
            unsafe(root);
            unsupportedZstandard(root);
            System.out.println("MMD import: 9 cases passed");
        } finally { MmdImportArchive.delete(root); }
    }
    private static void classification(File root) throws Exception {
        File motion = write(root, "fake-camera.vmd", vmd(1, 0, 0));
        MmdVmdParser.Sections parsed = MmdVmdParser.read(motion);
        check(parsed.supports("motion") && !parsed.supports("camera"), "filename must not determine role");
        parsed = MmdVmdParser.read(write(root, "mixed.vmd", vmd(1, 2, 1)));
        check(parsed.supports("motion4") && parsed.supports("face4") && parsed.supports("camera"), "mixed sections");
        byte[] invalid = vmd(0, 0, 1); invalid[50] = (byte) 255; invalid[51] = (byte) 255; invalid[52] = (byte) 255; invalid[53] = (byte) 255;
        File bad = write(root, "bad.vmd", invalid);
        rejects(() -> MmdVmdParser.read(bad), "invalid section length");
    }
    private static void unsupportedZstandard(File root) throws Exception {
        File original = new File(root, "zstd-template.zip");
        try (ZipArchiveOutputStream out = new ZipArchiveOutputStream(original)) {
            zip(out, "motion.vmd", vmd(1, 0, 0));
        }
        byte[] template = Files.readAllBytes(original.toPath());
        for (int method : new int[]{20, 93}) {
            byte[] bytes = template.clone();
            for (int i = 0; i < bytes.length - 12; i++) {
                if (bytes[i] != 'P' || bytes[i + 1] != 'K') continue;
                int offset = bytes[i + 2] == 3 && bytes[i + 3] == 4 ? 8
                        : bytes[i + 2] == 1 && bytes[i + 3] == 2 ? 10 : -1;
                if (offset >= 0) { bytes[i + offset] = (byte) method; bytes[i + offset + 1] = 0; }
            }
            File archive = write(root, "zstd-" + method + ".zip", bytes);
            try {
                MmdImportArchive.extract(archive, folder(root, "zstd-" + method), text -> {});
                throw new AssertionError("Zstandard entry was not rejected");
            } catch (IOException expected) {
                check(expected.getMessage().contains("Zstandard"), "reject optional codec before native class loading");
            }
        }
    }
    private static void nested(File root, String charset) throws Exception {
        File archive = new File(root, charset + ".zip");
        try (ZipArchiveOutputStream out = new ZipArchiveOutputStream(archive)) {
            out.setEncoding(charset); out.setUseLanguageEncodingFlag(charset.equals("UTF-8"));
            zip(out, "配布/作品/set.ini", "name=作品\nmotion=动作/舞蹈.vmd\nface=表情/笑.vmd\ncamera=镜头.vmd\n".getBytes(StandardCharsets.UTF_8));
            zip(out, "配布/作品/动作/舞蹈.vmd", vmd(1, 0, 0));
            zip(out, "配布/作品/表情/笑.vmd", vmd(0, 1, 0));
            zip(out, "配布/作品/镜头.vmd", vmd(0, 0, 1));
        }
        File data = folder(root, charset); MmdImportArchive.extract(archive, data, text -> {});
        MmdImportPlan plan = MmdImportPlan.inspect(data);
        check(plan.works.size() == 1 && plan.warnings.isEmpty(), "nested set work " + charset);
        check(plan.works.get(0).slots.get("motion").equals("配布/作品/动作/舞蹈.vmd"), "reference resolution " + charset);
    }
    private static void unflaggedUtf8(File root) throws Exception {
        File archive = new File(root, "unflagged.zip");
        try (ZipArchiveOutputStream out = new ZipArchiveOutputStream(archive)) {
            out.setEncoding("UTF-8"); out.setUseLanguageEncodingFlag(false);
            zip(out, "未标记动作.vmd", vmd(1, 0, 0));
        }
        File data = folder(root, "unflagged"); MmdImportArchive.extract(archive, data, text -> {});
        check(new File(data, "未标记动作.vmd").isFile(), "unflagged UTF-8");
    }
    private static void multiWorks(File root) throws Exception {
        File data = folder(root, "multi");
        write(data, "dance-A/a.vmd", vmd(1, 0, 0)); write(data, "dance-A/song.mp3", new byte[]{1});
        write(data, "dance-B/b.vmd", vmd(1, 0, 0)); write(data, "dance-B/song.mp3", new byte[]{2});
        MmdImportPlan plan = MmdImportPlan.inspect(data);
        check(plan.works.size() == 2, "different dances must be separate choices");
        for (MmdImportPlan.Work work : plan.works) {
            check(!work.slots.containsKey("motion2"), "no implicit multiplayer");
            check(MmdImportPlan.parent(work.slots.get("music")).equals(MmdImportPlan.parent(work.slots.get("motion"))), "same work music");
        }
        write(data, "dance-A/alternate.mp3", new byte[]{3});
        plan = MmdImportPlan.inspect(data);
        check(!plan.works.get(0).slots.containsKey("music"), "multiple music choices require explicit selection");
    }
    private static void slots(File root) throws Exception {
        File data = folder(root, "slots");
        write(data, "bone.vmd", vmd(1, 0, 0)); write(data, "morph.vmd", vmd(0, 1, 0));
        write(data, "cam.vmd", vmd(0, 0, 1)); write(data, "music.wav", new byte[]{1});
        MmdImportPlan plan = MmdImportPlan.inspect(data);
        Map<String, String> slots = new LinkedHashMap<>(), files = new LinkedHashMap<>();
        for (String slot : MmdImportPlan.SLOTS) slots.put(slot, slot.startsWith("motion") ? "bone.vmd"
                : slot.startsWith("face") ? "morph.vmd" : slot.equals("camera") ? "cam.vmd" : "music.wav");
        files.put("bone.vmd", "f0.vmd"); files.put("morph.vmd", "f1.vmd"); files.put("cam.vmd", "f2.vmd"); files.put("music.wav", "f3.wav");
        Map<String, String> generated = MmdImportPlan.parseSet(new String(plan.generate("test", slots,
                Collections.singletonMap("loop", "1"), files), StandardCharsets.UTF_8));
        check(generated.get("motion4").equals("f0.vmd") && generated.get("face4").equals("f1.vmd") && generated.get("loop").equals("1"), "multi-slot generation");
        Map<String, String> cameraOnly = Collections.singletonMap("camera", "cam.vmd");
        plan.validate("camera only", cameraOnly);
        slots.put("face", "cam.vmd"); rejects(() -> plan.validate("test", slots), "wrong slot type");
    }
    private static void seven(File root) throws Exception {
        File archive = new File(root, "real.7z"), source = write(root, "7z-source.vmd", vmd(1, 1, 0));
        try (SevenZOutputFile out = new SevenZOutputFile(archive)) {
            SevenZArchiveEntry entry = out.createArchiveEntry(source, "嵌套/动作.vmd");
            out.putArchiveEntry(entry); out.write(Files.readAllBytes(source.toPath())); out.closeArchiveEntry();
        }
        File data = folder(root, "seven"); MmdImportArchive.extract(archive, data, text -> {});
        check(MmdImportPlan.inspect(data).works.get(0).slots.containsKey("motion"), "actual 7z codec");
    }
    private static void unsafe(File root) throws Exception {
        for (String path : new String[]{"../escape.vmd", "/absolute.vmd", "C:\\escape.vmd", "a//b", "a/../b"}) rejects(() -> MmdImportArchive.path(path), "unsafe path");
        File archive = new File(root, "duplicate.zip");
        try (ZipArchiveOutputStream out = new ZipArchiveOutputStream(archive)) {
            zip(out, "dance.vmd", vmd(1, 0, 0)); zip(out, "DANCE.vmd", vmd(1, 0, 0));
        }
        rejects(() -> MmdImportArchive.extract(archive, folder(root, "duplicate"), text -> {}), "duplicate path");
        File rar = write(root, "unsupported.rar", new byte[]{'R', 'a', 'r', '!'});
        try { MmdImportArchive.extract(rar, folder(root, "rar"), text -> {}); throw new AssertionError("RAR accepted"); }
        catch (IOException expected) { check(expected.getMessage().contains("RAR"), "explicit RAR rejection"); }
        rejects(() -> MmdImportArchive.extract(new File(root, "real.7z"), folder(root, "cancelled"), text -> { throw new IOException("cancelled"); }), "cancellation");
    }
    private static byte[] vmd(int bones, int morphs, int cameras) throws IOException {
        ByteArrayOutputStream out = new ByteArrayOutputStream();
        byte[] header = new byte[50]; byte[] magic = "Vocaloid Motion Data 0002".getBytes(StandardCharsets.US_ASCII);
        System.arraycopy(magic, 0, header, 0, magic.length); out.write(header);
        count(out, bones); out.write(new byte[bones * 111]);
        count(out, morphs); out.write(new byte[morphs * 23]);
        count(out, cameras); out.write(new byte[cameras * 61]);
        count(out, 0); count(out, 0); count(out, 0); return out.toByteArray();
    }
    private static void count(OutputStream out, int count) throws IOException { for (int i = 0; i < 4; i++) out.write(count >>> (i * 8)); }
    private static void zip(ZipArchiveOutputStream out, String name, byte[] bytes) throws IOException {
        out.putArchiveEntry(new ZipArchiveEntry(name)); out.write(bytes); out.closeArchiveEntry();
    }
    private static File write(File root, String name, byte[] bytes) throws IOException {
        File file = new File(root, name); MmdImportArchive.mkdir(file.getParentFile()); Files.write(file.toPath(), bytes); return file;
    }
    private static File folder(File root, String name) throws IOException { File file = new File(root, name); MmdImportArchive.mkdir(file); return file; }
    private static void check(boolean condition, String message) { if (!condition) throw new AssertionError(message); }
    private interface Action { void run() throws Exception; }
    private static void rejects(Action action, String message) throws Exception {
        try { action.run(); } catch (IOException expected) { return; } throw new AssertionError(message);
    }
}
