#include <array>
#include <atomic>
#include <cstdint>
#include <iostream>
#include <limits>
#include <stdexcept>
#include <string>
#include <thread>

#include "../modules/custom_model/android_lod_streaming_observer.inc"

namespace {
uint32_t checks = 0;
void Check(bool condition, const char* expression, int line) {
    ++checks;
    if (!condition)
        throw std::runtime_error("observer check failed at line " + std::to_string(line) + ": " + expression);
}
#define CHECK(condition) Check((condition), #condition, __LINE__)
std::atomic<uint64_t> forwarded{0};
std::atomic<uint32_t> last_tag{0};
std::atomic<int32_t> last_value{0};
void Forward(uint32_t tag, int32_t value) {
    last_tag.store(tag, std::memory_order_relaxed);
    last_value.store(value, std::memory_order_relaxed);
    forwarded.fetch_add(1, std::memory_order_relaxed);
}
}

int main() {
    ResetAndroidLodStreamingObserver();
    CHECK(g_original_android_lod_streaming_offset == nullptr);
    auto report = AndroidLodStreamingOffsetReport();
    CHECK(report.find("optional observer unavailable") != std::string::npos);
    CHECK(report.find("tracked_write_count=0, observed_tags=0/35, values={}") != std::string::npos);
    CHECK(report.find("unobserved tags are unknown (not zero)") != std::string::npos);
    CHECK(report.find("not a coherent full-table snapshot") != std::string::npos);
    AndroidObserveLodStreamingOffset(7, -123);
    CHECK(g_android_lod_streaming_observed_offsets[7] == 0);
    CHECK(g_android_lod_streaming_observed_write_count == 0);
    CHECK(AndroidLodStreamingOffsetReport().find("observed_tags=0/35, values={}") != std::string::npos);

    g_original_android_lod_streaming_offset = Forward;
    g_android_lod_streaming_observer_installed.store(true, std::memory_order_release);
    AndroidObserveLodStreamingOffset(0, -9);
    CHECK(forwarded == 1 && last_tag == 0 && last_value == -9);
    CHECK(g_android_lod_streaming_observed_offsets[0] == (kAndroidLodStreamingOffsetSeen | uint32_t(-9)));
    AndroidObserveLodStreamingOffset(34, 0);
    CHECK(forwarded == 2 && last_tag == 34 && last_value == 0);
    CHECK(g_android_lod_streaming_observed_offsets[34] == kAndroidLodStreamingOffsetSeen);
    AndroidObserveLodStreamingOffset(35, -11);
    CHECK(forwarded == 3 && last_tag == 35 && last_value == -11);
    AndroidObserveLodStreamingOffset(std::numeric_limits<uint32_t>::max(), 27);
    CHECK(forwarded == 4 && last_tag == std::numeric_limits<uint32_t>::max() && last_value == 27);
    CHECK(g_android_lod_streaming_observed_write_count == 2); // unsupported tags still forwarded, never indexed
    report = AndroidLodStreamingOffsetReport();
    CHECK(report.find("optional observer active") != std::string::npos);
    CHECK(report.find("tracked_write_count=2, observed_tags=2/35, values={0=-9, 34=0}") != std::string::npos);
    const auto write_count = g_android_lod_streaming_observed_write_count.load();
    AndroidLodStreamingOffsetReport();
    CHECK(g_android_lod_streaming_observed_write_count == write_count); // snapshots do not consume writes
    ResetAndroidLodStreamingObserver();
    CHECK(g_android_lod_streaming_observed_write_count == 2); // active state cannot be reset accidentally

    AndroidObserveLodStreamingOffset(34, std::numeric_limits<int32_t>::min());
    AndroidObserveLodStreamingOffset(0, std::numeric_limits<int32_t>::max());
    report = AndroidLodStreamingOffsetReport();
    CHECK(report.find("0=2147483647") != std::string::npos && report.find("34=-2147483648") != std::string::npos);

    // Two actual writers share a tag while the main thread reads reports.
    // Every published sample must be one complete signed value, never a mix.
    std::atomic<bool> start{false};
    auto writer = [&](int32_t value) {
        while (!start.load(std::memory_order_acquire)) std::this_thread::yield();
        for (int i = 0; i < 64; ++i) {
            AndroidObserveLodStreamingOffset(5, value);
            std::this_thread::yield();
        }
    };
    std::thread negative(writer, -2048), positive(writer, 1048576);
    start.store(true, std::memory_order_release);
    for (int i = 0; i < 64; ++i) {
        const uint64_t sample = g_android_lod_streaming_observed_offsets[5].load(std::memory_order_relaxed);
        CHECK(sample == 0 || sample == (kAndroidLodStreamingOffsetSeen | uint32_t(-2048)) ||
            sample == (kAndroidLodStreamingOffsetSeen | uint32_t(1048576)));
        report = AndroidLodStreamingOffsetReport();
        const auto tag_at = report.find("5=");
        if (tag_at != std::string::npos)
            CHECK(report.compare(tag_at, 7, "5=-2048") == 0 || report.compare(tag_at, 9, "5=1048576") == 0);
        std::this_thread::yield();
    }
    negative.join(); positive.join();
    CHECK(forwarded == 134 && g_android_lod_streaming_observed_write_count == 132);
    CHECK(AndroidLodStreamingOffsetReport().find("observed_tags=3/35") != std::string::npos);
    g_android_lod_streaming_observer_installed.store(false, std::memory_order_release);
    ResetAndroidLodStreamingObserver();
    CHECK(g_original_android_lod_streaming_offset == Forward); // diagnostics reset never affects forwarding
    for (const auto& offset : g_android_lod_streaming_observed_offsets) CHECK(offset == 0);
    CHECK(g_android_lod_streaming_observed_write_count == 0);
    CHECK(AndroidLodStreamingOffsetReport().find("optional observer unavailable") != std::string::npos);
    std::cout << "PASS Android LOD streaming observer: " << checks
        << " Release-active checks; unchanged exactly-once forwarding, signed values, tag boundaries, unknown state, non-consuming reports, reset and concurrent observations\n";
}
