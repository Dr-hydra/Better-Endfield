#include "android_win32.h"
#include "loaded_il2cpp.h"
#include <mutex>

#include <atomic>
#include <chrono>
#include <dlfcn.h>
#include <thread>
#include <unistd.h>

namespace betterendfieldnext {
namespace {

constexpr int kVirtualKeyCount = 256;
constexpr uint32_t kHeld = 1, kGap = 2, kPulse = 4, kMaxPulses = 32;
std::atomic<uint32_t> g_keys[kVirtualKeyCount]{};
std::uint64_t NowMilliseconds() {
    return static_cast<std::uint64_t>(std::chrono::duration_cast<std::chrono::milliseconds>(
        std::chrono::steady_clock::now().time_since_epoch()).count());
}
} // namespace
bool SetVirtualKey(int key, VirtualKeyAction action) {
    if (key <= 0 || key >= kVirtualKeyCount) return false;
    auto& state = g_keys[key];
    switch (action) {
        case VirtualKeyAction::Press: state.fetch_or(kHeld, std::memory_order_acq_rel); return true;
        case VirtualKeyAction::Release: state.fetch_and(~kHeld, std::memory_order_acq_rel); return true;
        case VirtualKeyAction::Pulse: {
            auto value = state.load(std::memory_order_acquire);
            do {
                if (value / kPulse >= kMaxPulses) return false;
            } while (!state.compare_exchange_weak(value, value + kPulse, std::memory_order_acq_rel));
            return true;
        }
        default: return false;
    }
}
void ReleaseAllVirtualKeys() {
    for (auto& state : g_keys) state.store(0, std::memory_order_release);
}
bool VirtualKeyDown(int key) {
    if (key <= 0 || key >= kVirtualKeyCount) return false;
    auto& state = g_keys[key];
    auto value = state.load(std::memory_order_acquire);
    for (;;) {
        if (value & kHeld) return true;
        const bool down = !(value & kGap) && value >= kPulse;
        const uint32_t next = value & kGap ? value & ~kGap : down ? (value - kPulse) | kGap : 0;
        if (state.compare_exchange_weak(value, next, std::memory_order_acq_rel)) return down;
    }
}

namespace win32 {

std::uint64_t MonotonicMilliseconds() { return NowMilliseconds(); }

std::int64_t MonotonicNanoseconds() {
    return static_cast<std::int64_t>(
        std::chrono::duration_cast<std::chrono::nanoseconds>(
            std::chrono::steady_clock::now().time_since_epoch())
            .count());
}

void SleepMilliseconds(std::uint32_t milliseconds) {
    std::this_thread::sleep_for(std::chrono::milliseconds(milliseconds));
}

std::uint32_t ThreadId() { return static_cast<std::uint32_t>(gettid()); }

std::uint32_t ProcessId() { return static_cast<std::uint32_t>(getpid()); }

void* Il2CppImage() {
    static std::mutex mutex;
    static void* image = nullptr;
    std::lock_guard lock(mutex);
    if (!image) image = OpenLoadedIl2Cpp();
    return image;
}

void* Symbol(void* image, const char* name) {
    if (name == nullptr) return nullptr;
    void* target = image != nullptr ? image : Il2CppImage();
    return target != nullptr ? dlsym(target, name) : nullptr;
}

}  // namespace win32
}  // namespace betterendfieldnext
