package dev.betterendfield.android;

import java.io.*;
import java.util.Arrays;

/** Runs the production stream guard on a real JVM; no Android/native stubs. */
final class BemImportStreamTest {
    private static int checks;
    private static final BemImportStream.Checkpoint CONTINUE = () -> {};
    private interface Operation { void run() throws IOException; }
    private static void check(boolean condition) {
        ++checks;
        if (!condition) throw new AssertionError("check " + checks);
    }
    private static void rejected(Operation operation) throws IOException {
        try { operation.run(); } catch (IOException expected) { ++checks; return; }
        throw new AssertionError("unexpected acceptance");
    }
    private static byte[] fixture(int length) {
        byte[] result = new byte[length];
        for (int i = 8; i < length; ++i) result[i] = (byte)(i % 251);
        System.arraycopy(new byte[]{'B', 'E', 'M', 0, 'P', 'K', 'G', 0}, 0, result, 0, 8);
        return result;
    }
    public static void main(String[] args) throws Exception {
        byte[] valid = fixture(150003);
        ByteArrayOutputStream copied = new ByteArrayOutputStream();
        check(BemImportStream.copy(new ByteArrayInputStream(valid), copied, valid.length, CONTINUE) == valid.length);
        check(Arrays.equals(valid, copied.toByteArray()));
        // Short/zero-length reads must not corrupt the header or spin indefinitely.
        InputStream chunked = new ByteArrayInputStream(valid) {
            private int reads;
            @Override public synchronized int read(byte[] b, int offset, int length) {
                return (++reads % 11 == 0) ? 0 : super.read(b, offset, Math.min(length, 3));
            }
        };
        copied.reset();
        BemImportStream.copy(chunked, copied, valid.length, CONTINUE);
        check(Arrays.equals(valid, copied.toByteArray()));
        for (int size = 0; size < 8; ++size) {
            byte[] truncated = Arrays.copyOf(valid, size);
            copied.reset();
            rejected(() -> BemImportStream.copy(new ByteArrayInputStream(truncated), copied, 200000, CONTINUE));
            check(copied.size() == 0);
        }
        byte[] junk = new byte[100000];
        copied.reset();
        rejected(() -> BemImportStream.copy(new ByteArrayInputStream(junk), copied, 200000, CONTINUE));
        check(copied.size() == 0);
        check(Arrays.equals(junk, new byte[100000]));
        // Magic is only a preflight, not a claim that an eight-byte file is a valid package.
        copied.reset();
        check(BemImportStream.copy(new ByteArrayInputStream(fixture(8)), copied, 8, CONTINUE) == 8);
        rejected(() -> BemImportStream.copy(new ByteArrayInputStream(valid), new ByteArrayOutputStream(), valid.length - 1, CONTINUE));
        copied.reset();
        rejected(() -> BemImportStream.copy(new ByteArrayInputStream(valid), copied, valid.length,
                () -> { throw new IOException("cancelled"); }));
        check(copied.size() == 0);
        int[] visits = {0};
        copied.reset();
        rejected(() -> BemImportStream.copy(new ByteArrayInputStream(valid), copied, valid.length,
                () -> { if (++visits[0] == 5) throw new IOException("cancelled while copying"); }));
        check(copied.size() > 0 && copied.size() < valid.length);
        InputStream broken = new InputStream() {
            public int read() throws IOException { throw new IOException("provider disconnected"); }
        };
        rejected(() -> BemImportStream.copy(broken, copied, 100, CONTINUE));
        boolean[] closed = {false};
        InputStream owned = new ByteArrayInputStream(valid) {
            @Override public void close() { closed[0] = true; }
        };
        BemImportStream.copy(owned, new ByteArrayOutputStream(), valid.length, CONTINUE);
        check(!closed[0]);
        check(BemImportStream.MAX_BYTES == 2147483648L);
        System.out.println("PASS BEM stream preflight: " + checks + " assertions; bytes, short reads, invalid magic, limits, cancellation, stream ownership");
    }
}
