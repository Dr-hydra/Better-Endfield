#pragma once

#if defined(__ANDROID__)
#include <algorithm>
#include <chrono>
#include <codecvt>
#include <cstdlib>
#include <dlfcn.h>
#include <filesystem>
#include <link.h>
#include <locale>
#include <sys/syscall.h>
#include <sys/uio.h>
#include <unistd.h>

using HMODULE = void*;
using HINSTANCE = void*;
using DWORD = uint32_t;
using BOOL = int;
using LPVOID = void*;
using LPCWSTR = const wchar_t*;
constexpr BOOL TRUE = 1;
constexpr BOOL FALSE = 0;
constexpr DWORD DLL_PROCESS_ATTACH = 1;
constexpr DWORD DLL_PROCESS_DETACH = 0;
constexpr int GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS = 0x4;
constexpr int GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT = 0x2;
constexpr int GET_MODULE_HANDLE_EX_FLAG_PIN = 0x1;
constexpr int MOVEFILE_REPLACE_EXISTING = 1;
constexpr int EXCEPTION_EXECUTE_HANDLER = 1;
#define WINAPI
#define __fastcall
// The Android adapter uses process_vm_readv/writev for explicit memory checks.
// These macros only preserve the Windows source's control-flow shape; they do
// not turn arbitrary invalid accesses into safe accesses.
#define __try if (true)
#define __except(...) else

inline HMODULE GetModuleHandleW(LPCWSTR name) {
    if (!name) return nullptr;
    std::wstring value(name);
    const char* mapped = value.find(L"GameAssembly") != std::wstring::npos
        ? "libil2cpp.so" : "libunity.so";
    return dlopen(mapped, RTLD_NOW | RTLD_NOLOAD);
}
inline void* GetProcAddress(HMODULE module, const char* name) {
    return module && name ? dlsym(module, name) : nullptr;
}
inline BOOL GetModuleHandleExW(DWORD flags, LPCWSTR address, HMODULE* output) {
    (void)flags;
    if (!output || !address) return FALSE;
    Dl_info info{};
    if (!dladdr(reinterpret_cast<const void*>(address), &info) || !info.dli_fname) return FALSE;
    *output = dlopen(info.dli_fname, RTLD_NOW | RTLD_NOLOAD);
    return *output ? TRUE : FALSE;
}
inline BOOL DisableThreadLibraryCalls(HINSTANCE) { return TRUE; }
inline uint64_t GetTickCount64() {
    return static_cast<uint64_t>(std::chrono::duration_cast<std::chrono::milliseconds>(
        std::chrono::steady_clock::now().time_since_epoch()).count());
}
inline DWORD GetCurrentThreadId() { return static_cast<DWORD>(syscall(SYS_gettid)); }
inline DWORD GetCurrentProcessId() { return static_cast<DWORD>(getpid()); }
inline DWORD GetModuleFileNameW(HMODULE module, wchar_t* output, DWORD output_length) {
    if (!output || output_length == 0) return 0;
    Dl_info info{};
    if (!module || !dladdr(module, &info) || !info.dli_fname) return 0;
    std::wstring value;
    try { value = std::wstring_convert<std::codecvt_utf8<wchar_t>>().from_bytes(info.dli_fname); }
    catch (...) { return 0; }
    const size_t count = std::min(value.size(), static_cast<size_t>(output_length - 1));
    std::copy_n(value.data(), count, output);
    output[count] = L'\0';
    return static_cast<DWORD>(count);
}
inline DWORD GetEnvironmentVariableW(const wchar_t* name, wchar_t* output, DWORD output_length) {
    if (!name) return 0;
    std::wstring key(name);
    std::string narrow;
    try { narrow = std::wstring_convert<std::codecvt_utf8<wchar_t>>().to_bytes(key); }
    catch (...) { return 0; }
    const char* raw = std::getenv(narrow.c_str());
    if (!raw) return 0;
    std::wstring value;
    try { value = std::wstring_convert<std::codecvt_utf8<wchar_t>>().from_bytes(raw); }
    catch (...) { return 0; }
    if (!output || output_length == 0) return static_cast<DWORD>(value.size());
    const size_t count = std::min(value.size(), static_cast<size_t>(output_length - 1));
    std::copy_n(value.data(), count, output);
    output[count] = L'\0';
    return static_cast<DWORD>(count);
}
inline BOOL MoveFileExW(const wchar_t* from, const wchar_t* to, DWORD) {
    try { std::filesystem::rename(from, to); return TRUE; } catch (...) { return FALSE; }
}
inline BOOL MoveFileExW(const char* from, const char* to, DWORD) {
    try { std::filesystem::rename(from, to); return TRUE; } catch (...) { return FALSE; }
}
inline BOOL MoveFileExW(const wchar_t* from, const char* to, DWORD) {
    if (!from || !to) return FALSE;
    try {
        const std::string source = std::wstring_convert<std::codecvt_utf8<wchar_t>>().to_bytes(from);
        std::filesystem::rename(source, to);
        return TRUE;
    } catch (...) { return FALSE; }
}
inline int MultiByteToWideChar(unsigned, unsigned, const char* input, int length,
        wchar_t* output, int output_length) {
    if (!input || length < 0) return 0;
    std::wstring value;
    try { value = std::wstring_convert<std::codecvt_utf8<wchar_t>>().from_bytes(input, input + length); }
    catch (...) { return 0; }
    if (!output) return static_cast<int>(value.size());
    if (output_length < static_cast<int>(value.size())) return 0;
    std::copy(value.begin(), value.end(), output);
    return static_cast<int>(value.size());
}
constexpr unsigned CP_UTF8 = 65001;
constexpr unsigned MB_ERR_INVALID_CHARS = 8;

inline bool AndroidReadMemory(uintptr_t address, void* output, size_t size) {
    if (!address || !output || !size || address > UINTPTR_MAX - size) return false;
    iovec local{output, size}, remote{reinterpret_cast<void*>(address), size};
    return process_vm_readv(getpid(), &local, 1, &remote, 1, 0) == static_cast<ssize_t>(size);
}
inline bool AndroidWriteMemory(uintptr_t address, const void* input, size_t size) {
    if (!address || !input || !size || address > UINTPTR_MAX - size) return false;
    iovec local{const_cast<void*>(input), size}, remote{reinterpret_cast<void*>(address), size};
    return process_vm_writev(getpid(), &local, 1, &remote, 1, 0) == static_cast<ssize_t>(size);
}
#endif
