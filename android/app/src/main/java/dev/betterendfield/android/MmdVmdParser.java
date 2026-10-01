package dev.betterendfield.android;

import java.io.File;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.nio.charset.StandardCharsets;

/** Read section counts, never infer a VMD's role from its filename. */
final class MmdVmdParser {
    static final long MAX_BYTES = 64L * 1024 * 1024;
    static final class Sections {
        final long bones, morphs, cameras;
        Sections(long bones, long morphs, long cameras) {
            this.bones = bones; this.morphs = morphs; this.cameras = cameras;
        }
        boolean supports(String slot) {
            return slot.startsWith("motion") ? bones > 0
                    : slot.startsWith("face") ? morphs > 0 : "camera".equals(slot) && cameras > 0;
        }
        String label() {
            return (bones > 0 ? "动作 " : "") + (morphs > 0 ? "表情 " : "")
                    + (cameras > 0 ? "镜头" : "");
        }
    }
    static Sections read(File file) throws IOException {
        if (file.length() > MAX_BYTES) throw new IOException("VMD 超过 64 MiB：" + file.getName());
        try (RandomAccessFile in = new RandomAccessFile(file, "r")) {
            byte[] header = new byte[30]; in.readFully(header);
            String magic = new String(header, StandardCharsets.US_ASCII).split("\u0000", 2)[0];
            int modelBytes;
            if (magic.equals("Vocaloid Motion Data 0002")) modelBytes = 20;
            else if (magic.equals("Vocaloid Motion Data file") || magic.equals("Vocaloid Motion Data")) modelBytes = 10;
            else throw new IOException("无效 VMD：" + file.getName());
            skip(in, modelBytes);
            long bones = section(in, 111, false);
            long morphs = section(in, 23, true);
            long cameras = section(in, 61, true);
            section(in, 28, true); // lights
            section(in, 9, true);  // shadows
            if (in.getFilePointer() < in.length()) {
                long displays = count(in);
                if (displays > (in.length() - in.getFilePointer()) / 9) throw new IOException("VMD IK 长度异常");
                for (long i = 0; i < displays; i++) {
                    skip(in, 5);
                    long ik = count(in);
                    skip(in, ik * 21);
                }
            }
            if (in.getFilePointer() != in.length()) throw new IOException("VMD 尾部长度异常");
            if (bones == 0 && morphs == 0 && cameras == 0) throw new IOException("VMD 没有动作、表情或镜头");
            return new Sections(bones, morphs, cameras);
        }
    }
    private static long section(RandomAccessFile in, int bytes, boolean optional) throws IOException {
        if (optional && in.getFilePointer() == in.length()) return 0;
        long count = count(in); skip(in, count * bytes); return count;
    }
    private static long count(RandomAccessFile in) throws IOException {
        return Integer.toUnsignedLong(Integer.reverseBytes(in.readInt()));
    }
    private static void skip(RandomAccessFile in, long bytes) throws IOException {
        if (bytes < 0 || bytes > in.length() - in.getFilePointer()) throw new IOException("VMD 节长度异常");
        in.seek(in.getFilePointer() + bytes);
    }
}
