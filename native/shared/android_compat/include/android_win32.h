#pragma once

// Win32 surface used by the desktop Better Endfield modules, implemented for
// Android/ARM64. The desktop module sources are compiled unchanged; this header
// is reached through the compatibility <Windows.h> beside it, which is only on
// the include path of the desktop-module translation units.
//
// Nothing here emulates Windows in general. Every entry is either a direct POSIX
// equivalent (thread id, monotonic clock, dlopen/dlsym) or the documented
// Android stand-in for a desktop-only concept:
//
//   * GetAsyncKeyState reads the virtual-key latch in android_virtual_keys.h.
//     The in-game panel presses those keys, so the desktop hotkey code paths run
//     unmodified on a device that has no keyboard.
//   * GetForegroundWindow/GetWindowThreadProcessId report this process, because
//     the injected library is only ever mapped inside the running game.
//   * CaptureStackBackTrace reports "no frames" instead of fabricating
//     addresses that a reader would take for real GameAssembly offsets.
//
// Desktop-only facilities that cannot be represented honestly - structured
// exception handling, the module's own DLL directory, the append-only trace file
// - are not declared here at all. Their call sites carry an explicit
// `#if defined(_WIN32)` branch instead.

#if defined(_WIN32)
#error "android_win32.h is the Android stand-in for <Windows.h>"
#endif

#include <cstddef>
#include <cstdint>
#include <strings.h>

#include "android_virtual_keys.h"

// The desktop modules spell out the x86 calling convention on every detour and
// every managed function pointer. AArch64 has one convention, so the qualifier
// has to disappear rather than be translated.
#define __fastcall
#define __cdecl
#define __stdcall
#define WINAPI
#define CALLBACK

using DWORD = std::uint32_t;
using ULONG = std::uint32_t;
using WORD = std::uint16_t;
using USHORT = std::uint16_t;
using SHORT = std::int16_t;
using BYTE = std::uint8_t;
using BOOL = int;
using HMODULE = void*;
using HWND = void*;
using LPCWSTR = const wchar_t*;
using LPWSTR = wchar_t*;
using LPCSTR = const char*;
using LPVOID = void*;

#define MAX_PATH 260

// The MSVC spellings of the case-insensitive comparisons. POSIX names them
// differently; the semantics are identical for the ASCII identifiers the modules
// compare (character codenames, perform ids, clip names).
inline int _stricmp(const char* left, const char* right) {
    return ::strcasecmp(left, right);
}

inline int _strnicmp(const char* left, const char* right, std::size_t count) {
    return ::strncasecmp(left, right, count);
}

// QueryPerformanceCounter is used for the pose-overlay per-frame cost report.
// Reporting nanosecond ticks keeps the desktop arithmetic
// (delta * 1e6 / frequency) producing microseconds unchanged.
union LARGE_INTEGER {
    struct {
        DWORD LowPart;
        std::int32_t HighPart;
    };
    std::int64_t QuadPart;
};

// Virtual-key codes the desktop modules name explicitly. The numeric values are
// the Windows ones, so one configuration line means the same key on both
// platforms and a desktop profile can be read on a device without translation.
#define VK_PRIOR 0x21
#define VK_NEXT 0x22
#define VK_LEFT 0x25
#define VK_UP 0x26
#define VK_RIGHT 0x27
#define VK_DOWN 0x28
#define VK_NUMPAD0 0x60
#define VK_SUBTRACT 0x6D
#define VK_F1 0x70
#define VK_OEM_MINUS 0xBD

namespace betterendfield::win32 {

std::uint64_t MonotonicMilliseconds();
std::int64_t MonotonicNanoseconds();
void SleepMilliseconds(std::uint32_t milliseconds);
std::uint32_t ThreadId();
std::uint32_t ProcessId();
// Handle for the client's IL2CPP image. GameAssembly.dll on Windows is
// libil2cpp.so here, and the desktop sources only ever ask for that one image.
void* Il2CppImage();
void* Symbol(void* image, const char* name);

}  // namespace betterendfield::win32

inline std::uint64_t GetTickCount64() {
    return betterendfield::win32::MonotonicMilliseconds();
}

inline DWORD GetTickCount() {
    return static_cast<DWORD>(betterendfield::win32::MonotonicMilliseconds());
}

inline void Sleep(DWORD milliseconds) {
    betterendfield::win32::SleepMilliseconds(milliseconds);
}

inline DWORD GetCurrentThreadId() { return betterendfield::win32::ThreadId(); }
inline DWORD GetCurrentProcessId() { return betterendfield::win32::ProcessId(); }

inline BOOL QueryPerformanceFrequency(LARGE_INTEGER* frequency) {
    if (frequency == nullptr) return 0;
    frequency->QuadPart = 1000000000;
    return 1;
}

inline BOOL QueryPerformanceCounter(LARGE_INTEGER* counter) {
    if (counter == nullptr) return 0;
    counter->QuadPart = betterendfield::win32::MonotonicNanoseconds();
    return 1;
}

inline SHORT GetAsyncKeyState(int virtual_key) {
    return betterendfield::VirtualKeyDown(virtual_key)
        ? static_cast<SHORT>(0x8000) : static_cast<SHORT>(0);
}

// The injected library only exists inside the game process, and Android has no
// notion of a different process holding the foreground while this code runs.
inline HWND GetForegroundWindow() { return reinterpret_cast<HWND>(1); }

inline DWORD GetWindowThreadProcessId(HWND, DWORD* process_id) {
    if (process_id != nullptr) *process_id = betterendfield::win32::ProcessId();
    return betterendfield::win32::ThreadId();
}

inline HMODULE GetModuleHandleW(LPCWSTR) { return betterendfield::win32::Il2CppImage(); }
inline HMODULE GetModuleHandleA(LPCSTR) { return betterendfield::win32::Il2CppImage(); }

inline void* GetProcAddress(HMODULE image, const char* name) {
    return betterendfield::win32::Symbol(image, name);
}

// Desktop crash forensics: a GameAssembly-relative return-address list attached
// to a diagnostic log line. Android emits the same line without the addresses,
// rather than printing offsets that were never read from a stack.
inline USHORT CaptureStackBackTrace(ULONG, ULONG, void**, ULONG*) { return 0; }
