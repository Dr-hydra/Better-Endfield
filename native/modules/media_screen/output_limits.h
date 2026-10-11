#pragma once
#include <cstdint>
namespace BetterEndfieldNext::MediaScreen {
inline constexpr int kMaxOutputDimension = 3840;
inline constexpr uint32_t kMaxOutputPixels = 3840 * 2160;
inline constexpr int kMaxCaptureFps = 120;
inline constexpr int kDefaultOutputWidth = 1920, kDefaultOutputHeight = 1080, kDefaultCaptureFps = 60;
inline bool ValidOutput(int width, int height, int fps) {
    return width >= 64 && height >= 64 && width <= kMaxOutputDimension && height <= kMaxOutputDimension &&
        uint64_t(width) * height <= kMaxOutputPixels && fps >= 1 && fps <= kMaxCaptureFps;
}
}
