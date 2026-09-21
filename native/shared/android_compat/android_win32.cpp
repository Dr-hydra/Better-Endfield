#include "android_win32.h"

#include <atomic>
#include <chrono>
#include <dlfcn.h>
#include <thread>
#include <unistd.h>

namespace betterendfield {
namespace {

constexpr int kVirtualKeyCount = 256;
constexpr std::uint64_t kHeldForever = ~std::uint64_t{0};

// Zero means released, kHeldForever means held until an explicit release, and
// anything else is the monotonic millisecond at which a pulse expires.
std::atomic<std::uint64_t> g_key_deadline[kVirtualKeyCount];

std::uint64_t NowMilliseconds() {
    return static_cast<std::uint64_t>(
        std::chrono::duration_cast<std::chrono::milliseconds>(
            std::chrono::steady_clock::now().time_since_epoch())
            .count());
}

}  // namespace

bool SetVirtualKey(int virtual_key, VirtualKeyAction action) {
    if (virtual_key <= 0 || virtual_key >= kVirtualKeyCount) return false;
    std::uint64_t deadline = 0;
    switch (action) {
        case VirtualKeyAction::Release: deadline = 0; break;
        case VirtualKeyAction::Press: deadline = kHeldForever; break;
        case VirtualKeyAction::Pulse:
            deadline = NowMilliseconds() + kVirtualKeyPulseMs;
            break;
        default: return false;
    }
    g_key_deadline[virtual_key].store(deadline, std::memory_order_release);
    return true;
}

void ReleaseAllVirtualKeys() {
    for (auto& key : g_key_deadline) key.store(0, std::memory_order_release);
}

bool VirtualKeyDown(int virtual_key) {
    if (virtual_key <= 0 || virtual_key >= kVirtualKeyCount) return false;
    const std::uint64_t deadline =
        g_key_deadline[virtual_key].load(std::memory_order_acquire);
    if (deadline == 0) return false;
    if (deadline == kHeldForever) return true;
    if (NowMilliseconds() < deadline) return true;
    // Clear the expired pulse so the next tap is a fresh rising edge. The
    // compare_exchange leaves a deadline a racing writer has just re-armed.
    std::uint64_t expected = deadline;
    g_key_deadline[virtual_key].compare_exchange_strong(
        expected, 0, std::memory_order_acq_rel, std::memory_order_acquire);
    return false;
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
    // RTLD_NOLOAD: the client maps libil2cpp.so long before any module starts,
    // and loading a second copy would resolve exports against the wrong image.
    static void* image = dlopen("libil2cpp.so", RTLD_NOLOAD | RTLD_NOW);
    return image;
}

void* Symbol(void* image, const char* name) {
    if (name == nullptr) return nullptr;
    void* target = image != nullptr ? image : Il2CppImage();
    return target != nullptr ? dlsym(target, name) : nullptr;
}

}  // namespace win32
}  // namespace betterendfield
