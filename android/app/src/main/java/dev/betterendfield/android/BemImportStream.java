package dev.betterendfield.android;

import java.io.EOFException;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.Arrays;
import java.util.Objects;

/** Cheap first-stage validation. The native BEM parser still validates the full package. */
final class BemImportStream {
    static final long MAX_BYTES = 2L * 1024 * 1024 * 1024;
    private static final byte[] MAGIC = {'B', 'E', 'M', 0, 'P', 'K', 'G', 0};

    @FunctionalInterface
    interface Checkpoint { void check() throws IOException; }

    private BemImportStream() {}

    // Streams are owned by the caller. No provider names, filesystem paths or MIME
    // types participate in validation. Check the magic before copying a large file.
    static long copy(InputStream input, OutputStream output, long limit,
            Checkpoint checkpoint) throws IOException {
        Objects.requireNonNull(input, "input");
        Objects.requireNonNull(output, "output");
        Objects.requireNonNull(checkpoint, "checkpoint");
        if (limit < MAGIC.length) throw new IllegalArgumentException("limit is too small");
        byte[] header = new byte[MAGIC.length];
        int filled = 0;
        while (filled < header.length) {
            checkpoint.check();
            int count = input.read(header, filled, header.length - filled);
            if (count < 0) throw new EOFException("文件为空或不完整，不是有效的 BEM 包。");
            if (count == 0) {
                int value = input.read();
                if (value < 0) throw new EOFException("文件为空或不完整，不是有效的 BEM 包。");
                header[filled++] = (byte) value;
            } else {
                filled += count;
            }
        }
        if (!Arrays.equals(MAGIC, header)) {
            throw new IOException("不是有效的 BEM 文件；修改扩展名不能转换文件格式。");
        }
        checkpoint.check();
        output.write(header);
        long total = header.length;
        byte[] buffer = new byte[65536];
        while (true) {
            checkpoint.check();
            int count = input.read(buffer);
            if (count < 0) break;
            if (count == 0) {
                int value = input.read();
                if (value < 0) break;
                buffer[0] = (byte) value;
                count = 1;
            }
            if (count > limit - total) throw new IOException("BEM 文件超过大小限制（最大 2 GiB）。");
            checkpoint.check();
            output.write(buffer, 0, count);
            total += count;
        }
        checkpoint.check();
        return total;
    }
}
