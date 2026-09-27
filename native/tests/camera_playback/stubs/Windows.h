#pragma once
// Host-only compile fixture, NOT a Windows SDK or a runtime backend.
// It never hooks or opens a game. Windows builds use the real SDK instead.
#include <chrono>
#include <atomic>
#include <cstdint>
#include <cstring>
#include <thread>
#define __fastcall
#define __cdecl
#define __stdcall
#define WINAPI
#define CALLBACK
using DWORD=uint32_t;
using BOOL=int;
using HMODULE=void*;
using HWND=void*;
using HHOOK=void*;
using WPARAM=uintptr_t;
using LPARAM=intptr_t;
using LRESULT=intptr_t;
struct LARGE_INTEGER { int64_t QuadPart=0; };
struct POINT { long x=0,y=0; };
struct MSLLHOOKSTRUCT { POINT pt; DWORD mouseData=0,flags=0,time=0; uintptr_t dwExtraInfo=0; };
struct MSG {};
inline constexpr int FALSE=0, TRUE=1, HC_ACTION=0, WH_MOUSE_LL=14, QS_ALLINPUT=0, PM_REMOVE=1;
inline constexpr int WM_MOUSEMOVE=0x200, WM_MOUSEWHEEL=0x20A, LLMHF_INJECTED=1, WHEEL_DELTA=120;
inline constexpr int VK_SHIFT=0x10,VK_CONTROL=0x11,VK_PRIOR=0x21,VK_NEXT=0x22,
    VK_LEFT=0x25,VK_UP=0x26,VK_RIGHT=0x27,VK_DOWN=0x28,VK_OEM_MINUS=0xBD,
    VK_SUBTRACT=0x6D,VK_F1=0x70,VK_NUMPAD0=0x60,VK_NUMPAD1=0x61,VK_NUMPAD2=0x62,
    VK_NUMPAD3=0x63,VK_NUMPAD4=0x64,VK_NUMPAD5=0x65,VK_NUMPAD6=0x66,VK_NUMPAD7=0x67,
    VK_NUMPAD8=0x68,VK_NUMPAD9=0x69;
inline BOOL QueryPerformanceFrequency(LARGE_INTEGER* v) { v->QuadPart=1000000000; return 1; }
inline BOOL QueryPerformanceCounter(LARGE_INTEGER* v) {
    v->QuadPart=std::chrono::duration_cast<std::chrono::nanoseconds>(
        std::chrono::steady_clock::now().time_since_epoch()).count(); return 1;
}
inline uint64_t GetTickCount64() { LARGE_INTEGER v; QueryPerformanceCounter(&v); return uint64_t(v.QuadPart/1000000); }
inline HWND GetForegroundWindow() { return nullptr; }
inline DWORD GetCurrentProcessId() { return 1; }
inline DWORD GetWindowThreadProcessId(HWND,DWORD* p) { *p=1; return 1; }
inline short GetAsyncKeyState(int) { return 0; }
inline BOOL GetCursorPos(POINT*) { return 0; }
inline LRESULT CallNextHookEx(HHOOK,int,WPARAM,LPARAM) { return 0; }
inline HHOOK SetWindowsHookExW(int,LRESULT(*)(int,WPARAM,LPARAM),HMODULE,DWORD) { return nullptr; }
inline BOOL UnhookWindowsHookEx(HHOOK) { return 1; }
inline HMODULE GetModuleHandleW(const wchar_t*) { return nullptr; }
inline DWORD MsgWaitForMultipleObjects(DWORD,const void*,BOOL,DWORD ms,DWORD) {
    std::this_thread::sleep_for(std::chrono::milliseconds(ms)); return 0;
}
inline BOOL PeekMessageW(MSG*,HWND,unsigned,unsigned,unsigned) { return 0; }
inline BOOL TranslateMessage(const MSG*) { return 0; }
inline LRESULT DispatchMessageW(const MSG*) { return 0; }
inline void* GetProcAddress(HMODULE,const char*) { return nullptr; }
inline uint16_t HIWORD(uint32_t value) { return uint16_t(value>>16); }

inline DWORD GetCurrentThreadId() { static std::atomic<DWORD> next{1}; thread_local DWORD id=next.fetch_add(1); return id; }
