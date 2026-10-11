#pragma once
#include <cstdint>
#include <cstring>
#include <span>

namespace BetterEndfieldNext::MediaScreen {
// Check encoded dimensions before handing compressed bytes to Unity's decoder.
inline bool SupportedImage(std::span<const unsigned char> bytes) {
    auto fits = [](uint32_t w, uint32_t h) {
        return w && h && w <= 8192 && h <= 8192 && uint64_t(w) * h <= 16777216;
    };
    constexpr unsigned char png[]{137, 80, 78, 71, 13, 10, 26, 10};
    if (bytes.size() >= 24 && std::memcmp(bytes.data(), png, 8) == 0 &&
        bytes[8] == 0 && bytes[9] == 0 && bytes[10] == 0 && bytes[11] == 13 &&
        std::memcmp(bytes.data() + 12, "IHDR", 4) == 0) {
        auto be32 = [&](size_t p) { return uint32_t(bytes[p]) << 24 | uint32_t(bytes[p + 1]) << 16 |
                                          uint32_t(bytes[p + 2]) << 8 | bytes[p + 3]; };
        return fits(be32(16), be32(20));
    }
    if (bytes.size() < 4 || bytes[0] != 0xff || bytes[1] != 0xd8) return false;
    size_t p = 2;
    while (p + 3 < bytes.size()) {
        if (bytes[p++] != 0xff) return false;
        while (p < bytes.size() && bytes[p] == 0xff) ++p;
        if (p >= bytes.size()) return false;
        const unsigned char marker = bytes[p++];
        if (marker == 0xda || marker == 0xd9 || marker == 0) return false;
        if (marker == 0x01 || (marker >= 0xd0 && marker <= 0xd7)) continue;
        if (p + 2 > bytes.size()) return false;
        size_t length = size_t(bytes[p]) << 8 | bytes[p + 1];
        if (length < 2 || length > bytes.size() - p) return false;
        if (marker >= 0xc0 && marker <= 0xcf && marker != 0xc4 && marker != 0xc8 && marker != 0xcc) {
            if (length < 8) return false;
            return fits(uint32_t(bytes[p + 5]) << 8 | bytes[p + 6], uint32_t(bytes[p + 3]) << 8 | bytes[p + 4]);
        }
        p += length;
    }
    return false;
}
}
