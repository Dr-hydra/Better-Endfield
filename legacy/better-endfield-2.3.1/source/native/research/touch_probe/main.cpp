// Windows synthetic-touch probe.
//
// Route A of the mobile-UI input problem rests on one unverified assumption:
// that this build's Unity backend turns WM_POINTER touch into an InputSystem
// Touchscreen device, which is what fills EnhancedTouch.Touch.activeTouches.
// Injecting synthetic touch is the cheapest way to find out, but a negative
// result only means something if the injection itself is known good.  So the
// probe first runs a closed loop against its own window -- inject, let Windows
// route, observe WM_POINTERDOWN -- and only then offers the live mode that
// drives the game.  That separates "Windows refused" from "Unity ignored it".

#include <windows.h>

#include <cstdio>
#include <cstdlib>
#include <cstring>

// CreateSyntheticPointerDevice and friends are Windows 10 1809+.  Resolving
// them dynamically keeps the probe buildable against older SDKs and lets it
// report a clean diagnosis instead of failing to load.
#ifndef POINTER_FEEDBACK_DEFAULT
#define POINTER_FEEDBACK_DEFAULT 1
#define POINTER_FEEDBACK_INDIRECT 2
#define POINTER_FEEDBACK_NONE 3
#endif

using PFN_CreateSyntheticPointerDevice =
    HANDLE(WINAPI*)(POINTER_INPUT_TYPE, ULONG, DWORD);
using PFN_InjectSyntheticPointerInput =
    BOOL(WINAPI*)(HANDLE, const POINTER_TYPE_INFO*, UINT32);
using PFN_DestroySyntheticPointerDevice = void(WINAPI*)(HANDLE);

namespace {

PFN_CreateSyntheticPointerDevice g_create = nullptr;
PFN_InjectSyntheticPointerInput g_inject = nullptr;
PFN_DestroySyntheticPointerDevice g_destroy = nullptr;

// Set by the self-test window procedure.
bool g_saw_pointer_down = false;
bool g_saw_pointer_update = false;
bool g_saw_pointer_up = false;

constexpr UINT32 kFlagsDown =
    POINTER_FLAG_DOWN | POINTER_FLAG_INRANGE | POINTER_FLAG_INCONTACT;
constexpr UINT32 kFlagsUpdate =
    POINTER_FLAG_UPDATE | POINTER_FLAG_INRANGE | POINTER_FLAG_INCONTACT;
constexpr UINT32 kFlagsUp = POINTER_FLAG_UP;

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

void FillTouch(POINTER_TYPE_INFO& info, POINT point, UINT32 flags) {
    ::ZeroMemory(&info, sizeof(info));
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
}

bool Inject(HANDLE device, POINT point, UINT32 flags) {
    POINTER_TYPE_INFO info{};
    FillTouch(info, point, flags);
    if (g_inject(device, &info, 1)) {
        return true;
    }
    std::printf("    inject failed, GetLastError=%lu\n", ::GetLastError());
    return false;
}

void PumpMessages(DWORD milliseconds) {
    const DWORD deadline = ::GetTickCount() + milliseconds;
    MSG message;
    for (;;) {
        while (::PeekMessageW(&message, nullptr, 0, 0, PM_REMOVE)) {
            ::TranslateMessage(&message);
            ::DispatchMessageW(&message);
        }
        if (::GetTickCount() >= deadline) {
            return;
        }
        ::Sleep(4);
    }
}

void PumpOnce() {
    MSG message;
    while (::PeekMessageW(&message, nullptr, 0, 0, PM_REMOVE)) {
        ::TranslateMessage(&message);
        ::DispatchMessageW(&message);
    }
}

// Marker overlay ------------------------------------------------------------
//
// The game hides the cursor during gameplay, so without something visible
// there is no way to tell where the synthetic contact is being placed, and a
// null result becomes unreadable -- you cannot tell a rejected touch from one
// aimed at empty screen.  The marker is a small always-on-top dot at the
// injection point: green while idle, red while a contact is held.
//
// WS_EX_TRANSPARENT is load-bearing.  Without it the marker sits exactly under
// the injection point and would swallow the very touch it is meant to
// visualise; with it, hit testing passes straight through to the game.

constexpr int kMarkerSize = 26;
bool g_marker_contact = false;

LRESULT CALLBACK MarkerWndProc(HWND window, UINT message, WPARAM wparam,
                               LPARAM lparam) {
    if (message == WM_PAINT) {
        PAINTSTRUCT paint;
        const HDC dc = ::BeginPaint(window, &paint);
        RECT client;
        ::GetClientRect(window, &client);
        const HBRUSH brush = ::CreateSolidBrush(
            g_marker_contact ? RGB(230, 40, 40) : RGB(40, 200, 90));
        ::FillRect(dc, &client, brush);
        ::DeleteObject(brush);
        ::EndPaint(window, &paint);
        return 0;
    }
    return ::DefWindowProcW(window, message, wparam, lparam);
}

HWND CreateMarkerWindow() {
    WNDCLASSEXW window_class{};
    window_class.cbSize = sizeof(window_class);
    window_class.lpfnWndProc = MarkerWndProc;
    window_class.hInstance = ::GetModuleHandleW(nullptr);
    window_class.lpszClassName = L"BetterEndfieldTouchMarker";
    ::RegisterClassExW(&window_class);

    const HWND window = ::CreateWindowExW(
        WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_TOOLWINDOW |
            WS_EX_NOACTIVATE,
        window_class.lpszClassName, L"", WS_POPUP, 0, 0, kMarkerSize,
        kMarkerSize, nullptr, nullptr, window_class.hInstance, nullptr);
    if (!window) {
        return nullptr;
    }

    const HRGN region = ::CreateEllipticRgn(0, 0, kMarkerSize, kMarkerSize);
    ::SetWindowRgn(window, region, TRUE);
    ::SetLayeredWindowAttributes(window, 0, 190, LWA_ALPHA);
    ::ShowWindow(window, SW_SHOWNOACTIVATE);
    return window;
}

void MoveMarker(HWND marker, POINT point, bool contact) {
    if (!marker) {
        return;
    }
    if (contact != g_marker_contact) {
        g_marker_contact = contact;
        ::InvalidateRect(marker, nullptr, FALSE);
    }
    ::SetWindowPos(marker, HWND_TOPMOST, point.x - kMarkerSize / 2,
                   point.y - kMarkerSize / 2, kMarkerSize, kMarkerSize,
                   SWP_NOACTIVATE);
}

LRESULT CALLBACK SelfTestWndProc(HWND window, UINT message, WPARAM wparam,
                                 LPARAM lparam) {
    switch (message) {
        case WM_POINTERDOWN:
            g_saw_pointer_down = true;
            return 0;
        case WM_POINTERUPDATE:
            g_saw_pointer_update = true;
            return 0;
        case WM_POINTERUP:
            g_saw_pointer_up = true;
            return 0;
        default:
            break;
    }
    return ::DefWindowProcW(window, message, wparam, lparam);
}

// Injects into a window this process owns.  Success proves the whole Windows
// side of route A: the device is created, the OS accepts the contact, and it
// routes to the window under the point as real pointer input.
bool RunSelfTest(HANDLE device) {
    WNDCLASSEXW window_class{};
    window_class.cbSize = sizeof(window_class);
    window_class.lpfnWndProc = SelfTestWndProc;
    window_class.hInstance = ::GetModuleHandleW(nullptr);
    window_class.hCursor = ::LoadCursor(nullptr, IDC_ARROW);
    window_class.hbrBackground =
        reinterpret_cast<HBRUSH>(::GetStockObject(BLACK_BRUSH));
    window_class.lpszClassName = L"BetterEndfieldTouchProbe";
    if (!::RegisterClassExW(&window_class)) {
        std::printf("    RegisterClassExW failed, GetLastError=%lu\n",
                    ::GetLastError());
        return false;
    }

    const int width = 320;
    const int height = 200;
    const int x = ::GetSystemMetrics(SM_CXSCREEN) / 2 - width / 2;
    const int y = ::GetSystemMetrics(SM_CYSCREEN) / 2 - height / 2;
    const HWND window = ::CreateWindowExW(
        WS_EX_TOPMOST | WS_EX_TOOLWINDOW, window_class.lpszClassName,
        L"Better Endfield touch probe", WS_POPUP | WS_BORDER, x, y, width,
        height, nullptr, nullptr, window_class.hInstance, nullptr);
    if (!window) {
        std::printf("    CreateWindowExW failed, GetLastError=%lu\n",
                    ::GetLastError());
        return false;
    }

    ::ShowWindow(window, SW_SHOWNOACTIVATE);
    ::UpdateWindow(window);
    PumpMessages(200);

    POINT target{x + width / 2, y + height / 2};

    // The point must actually resolve to the probe window, or a pass would be
    // measuring some other window's willingness to accept touch.
    const HWND under_point = ::WindowFromPoint(target);
    if (under_point != window) {
        std::printf(
            "    warning: point resolves to HWND %p, probe window is %p\n",
            static_cast<void*>(under_point), static_cast<void*>(window));
    }

    bool ok = Inject(device, target, kFlagsDown);
    PumpMessages(60);

    for (int step = 1; ok && step <= 4; ++step) {
        target.y += 4;
        ok = Inject(device, target, kFlagsUpdate);
        PumpMessages(30);
    }

    if (ok) {
        Inject(device, target, kFlagsUp);
    } else {
        // Leave no dangling contact behind on a partial failure.
        Inject(device, target, kFlagsUp);
    }
    PumpMessages(120);

    ::DestroyWindow(window);
    ::UnregisterClassW(window_class.lpszClassName, window_class.hInstance);

    std::printf("    WM_POINTERDOWN   : %s\n", g_saw_pointer_down ? "yes" : "NO");
    std::printf("    WM_POINTERUPDATE : %s\n",
                g_saw_pointer_update ? "yes" : "NO");
    std::printf("    WM_POINTERUP     : %s\n", g_saw_pointer_up ? "yes" : "NO");
    return g_saw_pointer_down;
}

// Hold F8 to hold a synthetic contact.  Esc quits.
//
// Two aiming modes.  By default the contact follows the mouse cursor, which is
// the natural way to drive it.  But a game in combat typically locks and hides
// the cursor, so GetCursorPos then returns a fixed centre and aiming dies --
// hence the anchored mode, where the point is passed on the command line and
// nudged with the arrow keys.  The touch joystick sits at a fixed screen
// position anyway, so anchoring loses nothing against the game.
void RunLiveMode(HANDLE device, const POINT* anchor) {
    POINT point = anchor ? *anchor : POINT{};

    if (anchor) {
        std::printf(
            "\n  Live mode, anchored at (%ld, %ld).\n"
            "  The green dot marks the injection point; it turns red while a\n"
            "  contact is held. Arrow keys move it by 10px (Shift for 1px).\n"
            "  Hold F8 to press. Press Esc to quit.\n\n",
            point.x, point.y);
    } else {
        std::printf(
            "\n  Live mode, following the cursor.\n"
            "  Hold F8 to place a synthetic touch at the cursor; move the\n"
            "  mouse while holding to drag it. Press Esc to quit.\n\n");
    }

    const HWND marker = CreateMarkerWindow();
    if (!marker) {
        std::printf("  warning: marker overlay could not be created\n");
    }

    bool contact_down = false;

    for (;;) {
        PumpOnce();
        if (::GetAsyncKeyState(VK_ESCAPE) & 0x8000) {
            break;
        }

        if (anchor) {
            const LONG step =
                (::GetAsyncKeyState(VK_SHIFT) & 0x8000) ? 1 : 10;
            if (::GetAsyncKeyState(VK_LEFT) & 0x8000) point.x -= step;
            if (::GetAsyncKeyState(VK_RIGHT) & 0x8000) point.x += step;
            if (::GetAsyncKeyState(VK_UP) & 0x8000) point.y -= step;
            if (::GetAsyncKeyState(VK_DOWN) & 0x8000) point.y += step;
        } else {
            ::GetCursorPos(&point);
        }

        const bool wants_contact = (::GetAsyncKeyState(VK_F8) & 0x8000) != 0;

        if (wants_contact && !contact_down) {
            if (Inject(device, point, kFlagsDown)) {
                contact_down = true;
                std::printf("  down  (%ld, %ld)\n", point.x, point.y);
            }
        } else if (wants_contact && contact_down) {
            // A synthetic contact is dropped if it is not refreshed, so this
            // has to keep firing even when the point has not moved.
            Inject(device, point, kFlagsUpdate);
        } else if (!wants_contact && contact_down) {
            Inject(device, point, kFlagsUp);
            contact_down = false;
            std::printf("  up    (%ld, %ld)\n", point.x, point.y);
        }

        MoveMarker(marker, point, contact_down);
        ::Sleep(8);
    }

    if (contact_down) {
        Inject(device, point, kFlagsUp);
    }
    if (marker) {
        ::DestroyWindow(marker);
    }
}

bool IsElevated() {
    HANDLE token = nullptr;
    if (!::OpenProcessToken(::GetCurrentProcess(), TOKEN_QUERY, &token)) {
        return false;
    }
    TOKEN_ELEVATION elevation{};
    DWORD size = sizeof(elevation);
    const bool ok = ::GetTokenInformation(token, TokenElevation, &elevation,
                                          sizeof(elevation), &size) != FALSE;
    ::CloseHandle(token);
    return ok && elevation.TokenIsElevated != 0;
}

}  // namespace

int main(int argc, char** argv) {
    // The probe is normally read live and is often killed rather than exited,
    // so buffered output would be lost exactly when it matters.
    ::setvbuf(stdout, nullptr, _IONBF, 0);

    std::printf("Better Endfield synthetic touch probe\n");
    std::printf("=====================================\n\n");

    POINT anchor{};
    bool have_anchor = false;
    bool skip_self_test = false;
    bool follow_cursor = false;
    int positional = 0;
    for (int i = 1; i < argc; ++i) {
        if (std::strcmp(argv[i], "--follow-cursor") == 0) {
            follow_cursor = true;
        } else if (std::strcmp(argv[i], "--skip-self-test") == 0) {
            // The self-test needs a topmost window of its own, which can pull
            // a fullscreen game out of focus. Skippable once it has passed.
            skip_self_test = true;
        } else if (positional == 0) {
            anchor.x = std::strtol(argv[i], nullptr, 10);
            ++positional;
        } else if (positional == 1) {
            anchor.y = std::strtol(argv[i], nullptr, 10);
            ++positional;
            have_anchor = true;
        }
    }

    // Coordinates are screen pixels; without this the injected point would be
    // scaled against a virtualised desktop on a high-DPI display.
    ::SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);

    std::printf("  elevated: %s\n", IsElevated() ? "yes" : "no");

    if (!ResolveApi()) {
        std::printf(
            "\n  FAIL: synthetic pointer injection is unavailable on this\n"
            "  system (needs Windows 10 1809 or newer). Route A is dead.\n");
        return 2;
    }
    std::printf("  CreateSyntheticPointerDevice: resolved\n\n");

    const HANDLE device = g_create(PT_TOUCH, 1, POINTER_FEEDBACK_INDIRECT);
    if (!device) {
        std::printf("  FAIL: CreateSyntheticPointerDevice, GetLastError=%lu\n",
                    ::GetLastError());
        return 3;
    }
    std::printf("  device created: %p\n\n", device);

    if (skip_self_test) {
        std::printf("  Self-test skipped by --skip-self-test.\n");
    } else {
        std::printf("  Self-test (inject into our own window):\n");
        if (!RunSelfTest(device)) {
            std::printf(
                "\n  FAIL: Windows did not deliver the injected contact.\n"
                "  Fix this before drawing any conclusion about the game.\n");
            g_destroy(device);
            return 4;
        }
        std::printf("\n  Self-test PASSED: injection reaches windows as touch.\n");
    }

    // Anchored is the default: the game hides the cursor during gameplay, so
    // following it would pin the contact to wherever the game parked it.
    if (!have_anchor) {
        anchor.x = ::GetSystemMetrics(SM_CXSCREEN) / 2;
        anchor.y = ::GetSystemMetrics(SM_CYSCREEN) / 2;
    }
    RunLiveMode(device, follow_cursor ? nullptr : &anchor);

    g_destroy(device);
    std::printf("\n  device destroyed. bye.\n");
    return 0;
}
