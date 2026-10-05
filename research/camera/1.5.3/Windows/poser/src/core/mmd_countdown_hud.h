#pragma once
#include <atomic>
// The overlay may be hidden while the game thread prepares the first pose.
inline std::atomic<int> g_mmdCountdownDisplay{0};
