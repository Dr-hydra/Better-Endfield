#include "android_frame.h"
#include "android_virtual_keys.h"
#include <atomic>
#include <algorithm>
#include <unistd.h>
namespace betterendfield {
namespace {
std::atomic<pid_t> g_thread{0};
std::atomic<FrameCallback> g_clients[static_cast<unsigned>(FrameClient::Count)]{};
std::atomic_bool g_foreground{true}, g_suspend{false}, g_hud{false};
std::atomic_uint g_capabilities{0}, g_active{0};
std::atomic_int g_look_x{0}, g_look_y{0};
}
bool OnAndroidFrameThread() { return g_thread.load(std::memory_order_acquire) == gettid(); }
bool HasAndroidFrameBridge() { return g_thread.load(std::memory_order_acquire) != 0; }
void DispatchAndroidFrame() {
    pid_t unset = 0;
    g_thread.compare_exchange_strong(unset, gettid(), std::memory_order_acq_rel);
    if (!OnAndroidFrameThread()) return;
    const bool suspend = g_suspend.exchange(false, std::memory_order_acq_rel) || !AndroidForeground();
    for (auto& slot : g_clients) {
        if (auto callback = slot.load(std::memory_order_acquire)) callback(suspend);
    }
}
void SetAndroidFrameClient(FrameClient client, FrameCallback callback) {
    const auto index = static_cast<unsigned>(client);
    if (index < static_cast<unsigned>(FrameClient::Count))
        g_clients[index].store(callback, std::memory_order_release);
}
void SetAndroidForeground(bool foreground) {
    g_foreground.store(foreground, std::memory_order_release);
    if (!foreground) {
        g_suspend.store(true, std::memory_order_release);
        ReleaseAllVirtualKeys();
        g_look_x.store(0);
        g_look_y.store(0);
    }
}
bool AndroidForeground() { return g_foreground.load(std::memory_order_acquire); }
void PublishAndroidCameraState(unsigned capabilities, unsigned active) {
    g_capabilities.store(capabilities, std::memory_order_release);
    g_active.store(active, std::memory_order_release);
}
unsigned AndroidCameraCapabilities() { return g_capabilities.load(std::memory_order_acquire); }
unsigned AndroidCameraActive() { return g_active.load(std::memory_order_acquire); }
void PublishAndroidHudState(bool hidden) { g_hud.store(hidden, std::memory_order_release); }
bool AndroidHudHidden() { return g_hud.load(std::memory_order_acquire); }
void AddAndroidLook(int dx, int dy) {
    if (!AndroidForeground()) return;
    auto add = [](std::atomic_int& target, int delta) {
        delta = std::clamp(delta, -512, 512);
        auto value = target.load(std::memory_order_acquire);
        while (!target.compare_exchange_weak(value, std::clamp(value + delta, -8192, 8192),
                std::memory_order_acq_rel)) {}
    };
    add(g_look_x, dx);
    add(g_look_y, dy);
}
void TakeAndroidLook(int& dx, int& dy) {
    dx = g_look_x.exchange(0, std::memory_order_acq_rel);
    dy = g_look_y.exchange(0, std::memory_order_acq_rel);
}
}
