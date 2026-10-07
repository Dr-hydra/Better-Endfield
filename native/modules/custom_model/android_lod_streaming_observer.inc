// Read-only observer of the game's native streaming-offset writes. The hook
// owner publishes installed only after the original function is available.
using AndroidLodStreamingOffsetFn = void(*)(uint32_t, int32_t);
AndroidLodStreamingOffsetFn g_original_android_lod_streaming_offset = nullptr;
std::atomic<bool> g_android_lod_streaming_observer_installed{false};

constexpr uint32_t kAndroidLodStreamingObservedTagCount = 35;
constexpr uint64_t kAndroidLodStreamingOffsetSeen = uint64_t{1} << 32;
std::array<std::atomic<uint64_t>, kAndroidLodStreamingObservedTagCount>
    g_android_lod_streaming_observed_offsets{};
std::atomic<uint64_t> g_android_lod_streaming_observed_write_count{0};

void AndroidObserveLodStreamingOffset(uint32_t tag, int32_t value) {
    if (!g_original_android_lod_streaming_offset) return;
    g_original_android_lod_streaming_offset(tag, value);
    if (tag >= kAndroidLodStreamingObservedTagCount) return;
    // A zero value remains distinguishable from a tag never observed. One
    // atomic contains both the seen bit and all signed-value bits, so concurrent
    // readers cannot combine a new seen bit with an old/torn value.
    g_android_lod_streaming_observed_offsets[tag].store(
        kAndroidLodStreamingOffsetSeen | static_cast<uint32_t>(value),
        std::memory_order_relaxed);
    g_android_lod_streaming_observed_write_count.fetch_add(1, std::memory_order_relaxed);
}

// Only reset while no observer is installed and before exposing the detour.
// Never replace the original function or clear state under an active observer.
void ResetAndroidLodStreamingObserver() {
    if (g_android_lod_streaming_observer_installed.load(std::memory_order_acquire)) return;
    for (auto& offset : g_android_lod_streaming_observed_offsets)
        offset.store(0, std::memory_order_relaxed);
    g_android_lod_streaming_observed_write_count.store(0, std::memory_order_relaxed);
}

std::string AndroidLodStreamingOffsetReport() {
    const bool installed = g_android_lod_streaming_observer_installed.load(std::memory_order_acquire);
    const uint64_t writes = g_android_lod_streaming_observed_write_count.load(std::memory_order_relaxed);
    uint32_t observed = 0;
    std::string values;
    for (uint32_t tag = 0; tag < kAndroidLodStreamingObservedTagCount; ++tag) {
        const uint64_t encoded = g_android_lod_streaming_observed_offsets[tag].load(std::memory_order_relaxed);
        if ((encoded & kAndroidLodStreamingOffsetSeen) == 0) continue;
        ++observed;
        const uint32_t bits = static_cast<uint32_t>(encoded);
        const int64_t value = bits <= 0x7FFFFFFFu ? static_cast<int64_t>(bits)
            : static_cast<int64_t>(bits) - 0x100000000LL;
        if (!values.empty()) values += ", ";
        values += std::to_string(tag) + "=" + std::to_string(value);
    }
    return std::string(installed ? "optional observer active" : "optional observer unavailable") +
        "; last observed writes: tracked_write_count=" + std::to_string(writes) +
        ", observed_tags=" + std::to_string(observed) + "/35, values={" + values +
        "}; unobserved tags are unknown (not zero); independent per-tag observations and count, "
        "not a coherent full-table snapshot";
}
