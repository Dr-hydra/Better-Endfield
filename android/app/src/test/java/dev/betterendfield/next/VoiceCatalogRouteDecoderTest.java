package dev.betterendfield.next;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.TreeMap;

public final class VoiceCatalogRouteDecoderTest {
    public static void main(String[] args) throws Exception {
        check(1, new long[]{20, 30, 10, 40}, 2, 10, 40);
        check(2, new long[]{0, 20, 30, 3, 10, 40}, 2, 10, 40);
        check(2, new long[]{0, 10, 20, 3, 10, 20}, 1, 10, 20);
        check(1, new long[]{1, 0xffff_ffffL}, 1, 1, 0xffff_ffffL);
        check(2, new long[]{2, 0xffff_ffffL, 1}, 1, 0xffff_ffffL, 1);
        reject(1, new long[]{10, 20, 10, 30});
        reject(2, new long[]{0, 10, 20, 1, 10, 30});
        reject(1, new long[]{});
        reject(1, new long[]{10});
        reject(2, new long[]{0, 10});
        reject(2, new long[]{4, 10, 20});
        reject(2, new long[]{-1, 10, 20});
        reject(1, new long[]{0, 1});
        reject(1, new long[]{1, 0});
        reject(2, new long[]{0, 0x1_0000_0000L, 1});
        reject(2, new long[]{0, 1, 0x1_0000_0000L});
        reject(3, new long[]{1, 2});
        if (!VoiceCatalogRouteDecoder.supports(1) || !VoiceCatalogRouteDecoder.supports(2) ||
                VoiceCatalogRouteDecoder.supports(0)) throw new AssertionError();
        int catalogs = 0;
        int encodedRoutes = 0;
        if (args.length > 0) {
            for (String line : Files.readAllLines(Path.of(args[0]))) {
                String[] fields = line.split("\t");
                int schema = Integer.parseInt(fields[0]);
                long[] values = new long[fields.length - 1];
                for (int i = 1; i < fields.length; ++i) values[i - 1] = Long.parseLong(fields[i]);
                VoiceCatalogRouteDecoder.decode(schema, values);
                catalogs++;
                encodedRoutes += values.length / (schema == 1 ? 2 : 3);
            }
        }
        System.out.println("VoiceCatalogRouteDecoderTest: 18 checks passed; real catalogs=" +
                catalogs + " routes=" + encodedRoutes);
    }

    private static void check(int schema, long[] values, int size, long source, long target)
            throws Exception {
        TreeMap<Long, Long> result = VoiceCatalogRouteDecoder.decode(schema, values);
        if (result.size() != size || result.get(source) != target) throw new AssertionError();
    }

    private static void reject(int schema, long[] values) throws Exception {
        try {
            VoiceCatalogRouteDecoder.decode(schema, values);
        } catch (IOException expected) {
            return;
        }
        throw new AssertionError("Expected rejection for schema " + schema);
    }
}
