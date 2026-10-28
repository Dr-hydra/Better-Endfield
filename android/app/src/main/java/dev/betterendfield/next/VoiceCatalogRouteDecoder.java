package dev.betterendfield.next;

import java.io.IOException;
import java.util.TreeMap;

/** Adapts index routes to the native BEVCAT01 version 1 Media-ID map. */
final class VoiceCatalogRouteDecoder {
    private static final long UINT32_MAX = 0xffff_ffffL;

    private VoiceCatalogRouteDecoder() {}

    static boolean supports(int schemaVersion) {
        return schemaVersion == 1 || schemaVersion == 2;
    }

    static TreeMap<Long, Long> decode(int schemaVersion, long[] encoded) throws IOException {
        if (!supports(schemaVersion)) {
            throw new IOException("Unsupported voice route schema: " + schemaVersion);
        }
        int stride = schemaVersion == 1 ? 2 : 3;
        if (encoded.length == 0 || encoded.length % stride != 0) {
            throw new IOException("Invalid voice route length: " + encoded.length);
        }
        TreeMap<Long, Long> routes = new TreeMap<>();
        for (int offset = 0; offset < encoded.length; offset += stride) {
            if (schemaVersion == 2 && (encoded[offset] < 0 || encoded[offset] > 3)) {
                throw new IOException("Invalid source language: " + encoded[offset]);
            }
            long source = encoded[offset + stride - 2];
            long target = encoded[offset + stride - 1];
            if (source <= 0 || source > UINT32_MAX || target <= 0 || target > UINT32_MAX) {
                throw new IOException("Media ID out of range: " + source + " -> " + target);
            }
            // Native lookup has no source-language key. Reject ambiguous mappings.
            Long previous = routes.putIfAbsent(source, target);
            if (previous != null && previous.longValue() != target) {
                throw new IOException("Conflicting Media route: " + source +
                        " -> " + previous + " / " + target);
            }
        }
        return routes;
    }
}
