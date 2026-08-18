#include <Windows.h>

#include <array>
#include <cstring>
#include <filesystem>
#include <string>

#ifndef BETTER_ENDFIELD_INPUT_PROXY_MARKER
#error BETTER_ENDFIELD_INPUT_PROXY_MARKER must identify this proxy.
#endif

// Use PE forwarders instead of calling LoadLibrary from DllMain. These match
// the XInput 1.4 API surface imported by the client.
#if defined(BETTER_ENDFIELD_INPUT_PROXY_XINPUT14)
#pragma comment(linker, "/export:XInputGetState=C:\\Windows\\System32\\XInput1_4.XInputGetState,@2")
#pragma comment(linker, "/export:XInputSetState=C:\\Windows\\System32\\XInput1_4.XInputSetState,@3")
#pragma comment(linker, "/export:XInputGetCapabilities=C:\\Windows\\System32\\XInput1_4.XInputGetCapabilities,@4")
#pragma comment(linker, "/export:XInputEnable=C:\\Windows\\System32\\XInput1_4.XInputEnable,@5")
#pragma comment(linker, "/export:XInputGetBatteryInformation=C:\\Windows\\System32\\XInput1_4.XInputGetBatteryInformation,@7")
#pragma comment(linker, "/export:XInputGetKeystroke=C:\\Windows\\System32\\XInput1_4.XInputGetKeystroke,@8")
#pragma comment(linker, "/export:XInputGetAudioDeviceIds=C:\\Windows\\System32\\XInput1_4.XInputGetAudioDeviceIds,@10")
#elif defined(BETTER_ENDFIELD_INPUT_BOOTSTRAP)
// The built-in injector loads this ordinary image-backed DLL through the
// Windows loader. It intentionally exports no input API.
#else
#error An input-proxy DLL kind must be selected.
#endif

namespace {

using Il2CppDomainGetFn = void*(*)();
using Il2CppThreadAttachFn = void*(*)(void*);
using Il2CppThreadDetachFn = void(*)(void*);

std::filesystem::path ProxyDirectory(void* module) {
    std::array<wchar_t, 32768> module_path{};
    const auto length = GetModuleFileNameW(static_cast<HMODULE>(module),
        module_path.data(), static_cast<DWORD>(module_path.size()));
    if (length == 0 || length >= module_path.size()) {
        return {};
    }
    return std::filesystem::path(std::wstring(module_path.data(), length)).parent_path();
}

void WriteTextFile(const std::filesystem::path& path, const char* content,
    DWORD disposition) {
    const HANDLE file = CreateFileW(path.c_str(), GENERIC_WRITE,
        FILE_SHARE_READ | FILE_SHARE_WRITE, nullptr, disposition,
        FILE_ATTRIBUTE_NORMAL, nullptr);
    if (file != INVALID_HANDLE_VALUE) {
        if (disposition == OPEN_ALWAYS) {
            SetFilePointer(file, 0, nullptr, FILE_END);
        }
        DWORD written = 0;
        WriteFile(file, content, static_cast<DWORD>(std::strlen(content)), &written, nullptr);
        CloseHandle(file);
    }
}

std::filesystem::path SettingsPath() {
    std::array<wchar_t, 32768> local_app_data{};
    const DWORD size = GetEnvironmentVariableW(L"LOCALAPPDATA", local_app_data.data(),
        static_cast<DWORD>(local_app_data.size()));
    if (size == 0 || size >= local_app_data.size()) {
        return {};
    }
    return std::filesystem::path(local_app_data.data()) /
        L"BetterEndfield" / L"BetterEndfield.ini";
}

std::filesystem::path ReadHostPath() {
    const auto settings = SettingsPath();
    if (settings.empty()) {
        return {};
    }

    std::array<wchar_t, 32768> install_root{};
    GetPrivateProfileStringW(L"Loader", L"install_root", L"",
        install_root.data(), static_cast<DWORD>(install_root.size()), settings.c_str());
    if (install_root[0] == L'\0') {
        return {};
    }
    return std::filesystem::path(install_root.data()) / L"runtime" /
        L"BetterEndfield.Host.dll";
}

bool ShouldLoadHost() {
    const auto settings = SettingsPath();
    if (settings.empty()) {
        return false;
    }
    std::array<wchar_t, 16> value{};
    GetPrivateProfileStringW(L"Loader", L"load_host", L"true", value.data(),
        static_cast<DWORD>(value.size()), settings.c_str());
    return _wcsicmp(value.data(), L"true") == 0 ||
        _wcsicmp(value.data(), L"1") == 0 ||
        _wcsicmp(value.data(), L"yes") == 0 ||
        _wcsicmp(value.data(), L"on") == 0;
}

DWORD WINAPI ProxyWorker(void* module) {
    const auto directory = ProxyDirectory(module);
    if (directory.empty()) {
        return 0;
    }
    WriteTextFile(directory / BETTER_ENDFIELD_INPUT_PROXY_MARKER, "loaded\r\n",
        CREATE_ALWAYS);

#if defined(BETTER_ENDFIELD_INPUT_PROXY_LOAD_HOST)
    const auto status = directory / BETTER_ENDFIELD_INPUT_PROXY_STATUS;
    WriteTextFile(status, "worker entered\r\n", CREATE_ALWAYS);
    if (!ShouldLoadHost()) {
        WriteTextFile(status, "host disabled by configuration\r\n", OPEN_ALWAYS);
        return 0;
    }

    const auto host = ReadHostPath();
    if (!std::filesystem::is_regular_file(host)) {
        WriteTextFile(status, "Host file was not found\r\n", OPEN_ALWAYS);
        return 0;
    }

#if defined(BETTER_ENDFIELD_INPUT_BOOTSTRAP)
    // This image is loaded by the built-in injector after the game is resumed.
    // Loading the native Host does not require this short-lived worker to enter
    // the managed runtime. The Host owns the single IL2CPP thread attachment.
    HMODULE loaded = LoadLibraryExW(host.c_str(), nullptr,
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR | LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
    WriteTextFile(status,
        loaded ? "Host loaded without bootstrap IL2CPP attach\r\n"
               : "Host load failed\r\n",
        OPEN_ALWAYS);
    return 0;
#else

    Il2CppDomainGetFn domain_get = nullptr;
    Il2CppThreadAttachFn thread_attach = nullptr;
    Il2CppThreadDetachFn thread_detach = nullptr;
    void* domain = nullptr;
    for (int attempt = 0; attempt < 180; ++attempt) {
        HMODULE game_assembly = GetModuleHandleW(L"GameAssembly.dll");
        if (game_assembly && !domain_get) {
            domain_get = reinterpret_cast<Il2CppDomainGetFn>(
                GetProcAddress(game_assembly, "il2cpp_domain_get"));
            thread_attach = reinterpret_cast<Il2CppThreadAttachFn>(
                GetProcAddress(game_assembly, "il2cpp_thread_attach"));
            thread_detach = reinterpret_cast<Il2CppThreadDetachFn>(
                GetProcAddress(game_assembly, "il2cpp_thread_detach"));
        }
        if (domain_get && thread_attach && thread_detach) {
            domain = domain_get();
            if (domain) {
                break;
            }
        }
        Sleep(500);
    }
    void* attached_thread = domain && thread_attach && thread_detach
        ? thread_attach(domain) : nullptr;
    if (!attached_thread) {
        WriteTextFile(status, "IL2CPP domain or thread attach failed\r\n", OPEN_ALWAYS);
        return 0;
    }
    HMODULE loaded = LoadLibraryExW(host.c_str(), nullptr,
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR | LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
    thread_detach(attached_thread);
    WriteTextFile(status, loaded ? "Host loaded\r\n" : "Host load failed\r\n",
        OPEN_ALWAYS);
#endif
#endif
    return 0;
}

} // namespace

BOOL WINAPI DllMain(HINSTANCE instance, DWORD reason, LPVOID) {
    if (reason == DLL_PROCESS_ATTACH) {
        DisableThreadLibraryCalls(instance);
        HANDLE worker = CreateThread(nullptr, 0, ProxyWorker, instance, 0, nullptr);
        if (worker) {
            CloseHandle(worker);
        }
    }
    return TRUE;
}
