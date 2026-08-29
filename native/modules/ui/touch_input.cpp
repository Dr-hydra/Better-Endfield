#include "touch_input.h"

#include <Windows.h>

#include <atomic>
#include <cstdio>
#include <thread>

namespace BetterEndfield::UiModule::TouchInput {
namespace {

// CreateSyntheticPointerDevice and friends are Windows 10 1809+.  Resolving
// them dynamically keeps the module loadable on anything older, where the
// conversion simply reports itself unavailable instead of failing to bind.
#ifndef POINTER_FEEDBACK_NONE
#define POINTER_FEEDBACK_NONE 3
#endif

using PFN_CreateSyntheticPointerDevice =
    HANDLE(WINAPI*)(POINTER_INPUT_TYPE, ULONG, DWORD);
using PFN_InjectSyntheticPointerInput =
    BOOL(WINAPI*)(HANDLE, const POINTER_TYPE_INFO*, UINT32);
using PFN_DestroySyntheticPointerDevice = void(WINAPI*)(HANDLE);

constexpr UINT32 kFlagsDown =
    POINTER_FLAG_DOWN | POINTER_FLAG_INRANGE | POINTER_FLAG_INCONTACT;
constexpr UINT32 kFlagsUpdate =
    POINTER_FLAG_UPDATE | POINTER_FLAG_INRANGE | POINTER_FLAG_INCONTACT;
constexpr UINT32 kFlagsUp = POINTER_FLAG_UP;

// Windows stamps mouse messages it synthesises from touch or pen with this
// signature in dwExtraInfo.  Our own injected contact comes back as one of
// those, and converting it again would latch the finger down forever.
constexpr ULONG_PTR kTouchSynthesizedMouse = 0xFF515700;
constexpr ULONG_PTR kTouchSynthesizedMouseMask = 0xFFFFFF00;

constexpr int kHotkeyId = 0xBE01;
constexpr UINT WM_TOUCHINPUT_RELEASE = WM_APP + 1;

PFN_CreateSyntheticPointerDevice g_create = nullptr;
PFN_InjectSyntheticPointerInput g_inject = nullptr;
PFN_DestroySyntheticPointerDevice g_destroy = nullptr;

LogFn g_log = nullptr;
HANDLE g_device = nullptr;
HHOOK g_hook = nullptr;
std::thread g_thread;
std::atomic<DWORD> g_thread_id{0};
std::atomic_bool g_running{false};

// Follows the touch UI configuration.
std::atomic_bool g_enabled{false};
// Runtime override behind the hotkey; independent of configuration so a toggle
// survives a settings reload.
std::atomic_bool g_user_enabled{true};
std::atomic_bool g_contact_down{false};

void Log(const char* message) {
    if (g_log) {
        g_log(message);
    }
}

bool ResolveApi() {
    const HMODULE user32 = ::GetModuleHandleW(L"user32.dll");
    if (!user32) {
        return false;
    }
    g_create = reinterpret_cast<PFN_CreateSyntheticPointerDevice>(
        ::GetProcAddress(user32, "CreateSyntheticPointerDevice"));
    g_inject = reinterpret_cast<PFN_InjectSyntheticPointerInput>(
        ::GetProcAddress(user32, "InjectSyntheticPointerInput"));
    g_destroy = reinterpret_cast<PFN_DestroySyntheticPointerDevice>(
        ::GetProcAddress(user32, "DestroySyntheticPointerDevice"));
    return g_create && g_inject && g_destroy;
}

bool Inject(POINT point, UINT32 flags) {
    if (!g_device || !g_inject) {
        return false;
    }

    POINTER_TYPE_INFO info{};
    info.type = PT_TOUCH;

    POINTER_TOUCH_INFO& touch = info.touchInfo;
    touch.pointerInfo.pointerType = PT_TOUCH;
    touch.pointerInfo.pointerId = 0;
    touch.pointerInfo.ptPixelLocation = point;
    touch.pointerInfo.pointerFlags = flags;
    touch.touchFlags = TOUCH_FLAG_NONE;
    touch.touchMask =
        TOUCH_MASK_CONTACTAREA | TOUCH_MASK_ORIENTATION | TOUCH_MASK_PRESSURE;
    touch.orientation = 90;
    touch.pressure = 32000;
    touch.rcContact.left = point.x - 2;
    touch.rcContact.top = point.y - 2;
    touch.rcContact.right = point.x + 2;
    touch.rcContact.bottom = point.y + 2;

    return g_inject(g_device, &info, 1) != FALSE;
}

// Conversion is scoped to the game's own window.  Without this the hook would
// swallow left clicks across the whole desktop, including the mod's own
// overlay, whenever the mobile layout happened to be on.
bool GameHasFocus() {
    const HWND foreground = ::GetForegroundWindow();
    if (!foreground) {
        return false;
    }
    DWORD process_id = 0;
    ::GetWindowThreadProcessId(foreground, &process_id);
    return process_id == ::GetCurrentProcessId();
}

bool ConversionActive() {
    return g_enabled.load(std::memory_order_relaxed) &&
           g_user_enabled.load(std::memory_order_relaxed) && GameHasFocus();
}

void ReleaseContact(POINT point) {
    if (g_contact_down.exchange(false)) {
        Inject(point, kFlagsUp);
    }
}

LRESULT CALLBACK MouseProc(int code, WPARAM wparam, LPARAM lparam) {
    if (code != HC_ACTION) {
        return ::CallNextHookEx(nullptr, code, wparam, lparam);
    }

    const auto* info = reinterpret_cast<const MSLLHOOKSTRUCT*>(lparam);

    // Only the touch signature is filtered, deliberately not LLMHF_INJECTED:
    // remote-play and streaming clients deliver ordinary mouse input via
    // SendInput, and those are exactly the events worth converting.
    const bool is_own_echo =
        (info->dwExtraInfo & kTouchSynthesizedMouseMask) ==
        kTouchSynthesizedMouse;

    if (is_own_echo || !ConversionActive()) {
        return ::CallNextHookEx(nullptr, code, wparam, lparam);
    }

    switch (wparam) {
        case WM_LBUTTONDOWN:
            if (Inject(info->pt, kFlagsDown)) {
                g_contact_down.store(true);
                return 1;
            }
            break;
        case WM_MOUSEMOVE:
            // Movement is only swallowed while a finger is down; otherwise the
            // cursor must keep working for menus and the rest of the desktop.
            if (g_contact_down.load(std::memory_order_relaxed)) {
                Inject(info->pt, kFlagsUpdate);
                return 1;
            }
            break;
        case WM_LBUTTONUP:
            if (g_contact_down.load(std::memory_order_relaxed)) {
                ReleaseContact(info->pt);
                return 1;
            }
            break;
        default:
            break;
    }

    return ::CallNextHookEx(nullptr, code, wparam, lparam);
}

void ThreadMain() {
    g_thread_id.store(::GetCurrentThreadId());

    g_device = g_create(PT_TOUCH, 1, POINTER_FEEDBACK_NONE);
    if (!g_device) {
        char buffer[128];
        std::snprintf(buffer, sizeof(buffer),
                      "Touch input: CreateSyntheticPointerDevice failed, "
                      "GetLastError=%lu",
                      ::GetLastError());
        Log(buffer);
        return;
    }

    // A low-level mouse hook delivers its callbacks on the installing thread,
    // which therefore has to own a message loop of its own.
    g_hook = ::SetWindowsHookExW(WH_MOUSE_LL, &MouseProc, nullptr, 0);
    if (!g_hook) {
        char buffer[128];
        std::snprintf(buffer, sizeof(buffer),
                      "Touch input: SetWindowsHookExW failed, GetLastError=%lu",
                      ::GetLastError());
        Log(buffer);
        g_destroy(g_device);
        g_device = nullptr;
        return;
    }

    const bool hotkey =
        ::RegisterHotKey(nullptr, kHotkeyId,
                         MOD_CONTROL | MOD_ALT | MOD_NOREPEAT, 'T') != FALSE;
    Log(hotkey ? "Touch input: mouse-to-touch ready; Ctrl+Alt+T toggles it."
               : "Touch input: mouse-to-touch ready, but Ctrl+Alt+T could not "
                 "be registered (already taken by another process).");

    MSG message;
    while (::GetMessageW(&message, nullptr, 0, 0) > 0) {
        if (message.message == WM_HOTKEY &&
            message.wParam == static_cast<WPARAM>(kHotkeyId)) {
            const bool now = !g_user_enabled.load();
            g_user_enabled.store(now);
            if (!now) {
                POINT cursor{};
                ::GetCursorPos(&cursor);
                ReleaseContact(cursor);
            }
            Log(now ? "Touch input: mouse-to-touch ENABLED by hotkey."
                    : "Touch input: mouse-to-touch DISABLED by hotkey.");
        } else if (message.message == WM_TOUCHINPUT_RELEASE) {
            POINT cursor{};
            ::GetCursorPos(&cursor);
            ReleaseContact(cursor);
        }
        ::TranslateMessage(&message);
        ::DispatchMessageW(&message);
    }

    if (hotkey) {
        ::UnregisterHotKey(nullptr, kHotkeyId);
    }
    ::UnhookWindowsHookEx(g_hook);
    g_hook = nullptr;

    POINT cursor{};
    ::GetCursorPos(&cursor);
    ReleaseContact(cursor);

    g_destroy(g_device);
    g_device = nullptr;
}

}  // namespace

bool Start(LogFn log) {
    if (g_running.exchange(true)) {
        return true;
    }
    g_log = log;

    if (!ResolveApi()) {
        Log("Touch input: synthetic pointer injection is unavailable on this "
            "system (needs Windows 10 1809 or newer); mouse-to-touch is off.");
        g_running.store(false);
        return false;
    }

    g_thread = std::thread(&ThreadMain);
    return true;
}

void Stop() {
    if (!g_running.exchange(false)) {
        return;
    }
    const DWORD thread_id = g_thread_id.load();
    if (thread_id != 0) {
        ::PostThreadMessageW(thread_id, WM_QUIT, 0, 0);
    }
    if (g_thread.joinable()) {
        g_thread.join();
    }
    g_thread_id.store(0);
    g_log = nullptr;
}

void SetEnabled(bool enabled) {
    const bool previous = g_enabled.exchange(enabled);
    if (previous && !enabled && g_contact_down.load()) {
        // Dropping the configuration mid-press must not leave a finger stuck
        // down; the release has to happen on the thread that owns the device.
        const DWORD thread_id = g_thread_id.load();
        if (thread_id != 0) {
            ::PostThreadMessageW(thread_id, WM_TOUCHINPUT_RELEASE, 0, 0);
        }
    }
}

}  // namespace BetterEndfield::UiModule::TouchInput
