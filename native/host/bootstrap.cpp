#include "BetterEndfield/BootstrapConfig.h"

#include "host_runtime.h"

#include <Windows.h>

#include <cstring>
#include <new>
#include <string>

namespace {

HMODULE g_host_module = nullptr;
BE_BootstrapConfigV1 g_bootstrap{};
bool g_has_bootstrap = false;
// The Host is pinned and lives until process termination. Keeping this as a
// raw process-lifetime pointer prevents C++ static teardown from invoking
// Wwise, MinHook, or FreeLibrary while Windows holds the loader lock.
BetterEndfield::Host::HostRuntime* g_runtime = nullptr;

void WriteAttachMarker(const wchar_t* message) {
    wchar_t local_app_data[32768]{};
    const DWORD length = GetEnvironmentVariableW(
        L"LOCALAPPDATA", local_app_data,
        static_cast<DWORD>(_countof(local_app_data)));
    if (length == 0 || length >= _countof(local_app_data)) {
        return;
    }
    std::wstring directory(local_app_data);
    directory += L"\\BetterEndfield\\logs";
    CreateDirectoryW((directory.substr(0, directory.find_last_of(L'\\'))).c_str(), nullptr);
    CreateDirectoryW(directory.c_str(), nullptr);
    const std::wstring path = directory + L"\\host-attach.marker";
    HANDLE file = CreateFileW(path.c_str(), FILE_APPEND_DATA,
        FILE_SHARE_READ | FILE_SHARE_WRITE, nullptr, OPEN_ALWAYS,
        FILE_ATTRIBUTE_NORMAL, nullptr);
    if (file == INVALID_HANDLE_VALUE) {
        return;
    }
    SYSTEMTIME now{};
    GetLocalTime(&now);
    wchar_t line[256]{};
    const int line_length = _snwprintf_s(line, _countof(line), _TRUNCATE,
        L"%04u-%02u-%02u %02u:%02u:%02u %s\r\n",
        now.wYear, now.wMonth, now.wDay, now.wHour, now.wMinute,
        now.wSecond, message ? message : L"<null>");
    if (line_length > 0) {
        DWORD written = 0;
        WriteFile(file, line, static_cast<DWORD>(line_length * sizeof(wchar_t)),
            &written, nullptr);
    }
    CloseHandle(file);
}

DWORD WINAPI HostThread(void*) {
    WriteAttachMarker(L"HostThread entered");
    HMODULE pinned_module = nullptr;
    if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
            GET_MODULE_HANDLE_EX_FLAG_PIN,
            reinterpret_cast<LPCWSTR>(g_host_module), &pinned_module)) {
        WriteAttachMarker(L"Host pin failed");
        return 0;
    }
    WriteAttachMarker(L"Host pinned for process lifetime");

    const void* bootstrap = g_has_bootstrap ? &g_bootstrap : nullptr;
    g_runtime = new (std::nothrow) BetterEndfield::Host::HostRuntime(
        g_host_module, bootstrap);
    if (!g_runtime) {
        WriteAttachMarker(L"Host runtime allocation failed");
        return 0;
    }
    g_runtime->Run();
    return 0;
}

} // namespace

BOOL WINAPI DllMain(HINSTANCE instance, DWORD reason, LPVOID reserved) {
    if (reason == DLL_PROCESS_DETACH) {
        // ExitProcess has already terminated the worker threads and holds the
        // loader lock here. Heavy cleanup is both unnecessary and unsafe: the
        // OS will reclaim hooks, Wwise media buffers, and loaded modules with
        // the process address space.
        return TRUE;
    }
    if (reason != DLL_PROCESS_ATTACH) {
        return TRUE;
    }

    DisableThreadLibraryCalls(instance);
    WriteAttachMarker(L"DllMain attach");
    g_host_module = instance;
    if (reserved) {
        __try {
            const auto* candidate = static_cast<const BE_BootstrapConfigV1*>(reserved);
            if (candidate->size == sizeof(BE_BootstrapConfigV1) &&
                std::memcmp(candidate->magic, BETTER_ENDFIELD_BOOTSTRAP_MAGIC, 9) == 0) {
                std::memcpy(&g_bootstrap, candidate, sizeof(g_bootstrap));
                g_has_bootstrap = true;
            }
        }
        __except (EXCEPTION_EXECUTE_HANDLER) {
            g_has_bootstrap = false;
        }
    }
    CreateThread(nullptr, 0, HostThread, nullptr, 0, nullptr);
    return TRUE;
}
