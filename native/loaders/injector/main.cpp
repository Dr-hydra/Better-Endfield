#include <Windows.h>
#include <TlHelp32.h>

#include "BetterEndfieldNext/BootstrapConfig.h"
#include "launch_guard.h"
#include "../shared/official_launcher.h"

#include <algorithm>
#include <cstdint>
#include <fstream>
#include <cstring>
#include <filesystem>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

namespace {

using LoadLibraryAFn = HMODULE(WINAPI*)(LPCSTR);
using GetProcAddressFn = FARPROC(WINAPI*)(HMODULE, LPCSTR);
using RtlAddFunctionTableFn = BOOLEAN(*)(PRUNTIME_FUNCTION, DWORD, DWORD64);
using DllEntryPointFn = BOOL(WINAPI*)(void*, DWORD, void*);
struct ManualMappingData {
    LoadLibraryAFn load_library_a;
    GetProcAddressFn get_proc_address;
    RtlAddFunctionTableFn rtl_add_function_table;
    BYTE* image_base;
    HINSTANCE mapped_module;
    DWORD reason;
    void* reserved;
    BOOL seh_support;
};

const HINSTANCE kMappingInvalid = reinterpret_cast<HINSTANCE>(0x404040);
const HINSTANCE kMappingSehFailure = reinterpret_cast<HINSTANCE>(0x505050);

#define BE_RELOC_TYPE(value) ((value) >> 12)
#define BE_IS_ORDINAL(value) (((value) & IMAGE_ORDINAL_FLAG64) != 0)

void PrintLastError(const wchar_t* prefix) {
    const DWORD error = GetLastError();
    wchar_t* message = nullptr;
    const DWORD length = FormatMessageW(
        FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM |
            FORMAT_MESSAGE_IGNORE_INSERTS,
        nullptr, error, 0, reinterpret_cast<wchar_t*>(&message), 0, nullptr);
    std::wcerr << prefix << L" (" << error << L")";
    if (length != 0 && message) {
        std::wcerr << L": " << message;
    }
    std::wcerr << L"\n";
    if (message) {
        LocalFree(message);
    }
}

// Before startup completes, every failure (including C++ exceptions) must
// discard only the game process this invocation just created. After startup,
// waiting/diagnostic failures must never terminate a running game.
class OwnedGameStartup {
public:
    explicit OwnedGameStartup(const PROCESS_INFORMATION& process) noexcept
        : process_(process.hProcess), thread_(process.hThread) {}
    OwnedGameStartup(const OwnedGameStartup&) = delete;
    OwnedGameStartup& operator=(const OwnedGameStartup&) = delete;
    ~OwnedGameStartup() {
        if (!started_ && WaitForSingleObject(process_, 0) == WAIT_TIMEOUT) {
            if (TerminateProcess(process_, 1)) WaitForSingleObject(process_, 5000);
            else PrintLastError(L"Unable to clean up incomplete Endfield startup");
        }
        if (thread_) CloseHandle(thread_);
        CloseHandle(process_);
    }
    void Complete() noexcept {
        started_ = true;
        CloseHandle(thread_);
        thread_ = nullptr;
    }
private:
    HANDLE process_;
    HANDLE thread_;
    bool started_ = false;
};

// Preserve detailed injection failures for a dialog after the helper console
// has been detached. CLI launches continue writing directly to stderr.
class InjectionDiagnostics {
public:
    explicit InjectionDiagnostics(bool capture)
        : previous_(capture ? std::wcerr.rdbuf(output_.rdbuf()) : nullptr) {}
    ~InjectionDiagnostics() { if (previous_) std::wcerr.rdbuf(previous_); }
    std::wstring Details() const { return output_.str(); }
private:
    std::wostringstream output_;
    std::wstreambuf* previous_;
};

std::filesystem::path ExecutablePath() {
    std::wstring value(32768, L'\0');
    const DWORD length = GetModuleFileNameW(nullptr, value.data(),
        static_cast<DWORD>(value.size()));
    if (length == 0 || length >= value.size()) {
        return {};
    }
    value.resize(length);
    return value;
}

void CopyBootstrapPath(wchar_t (&destination)[BETTER_ENDFIELD_NEXT_BOOTSTRAP_PATH_CAPACITY],
    const std::filesystem::path& source) {
    const std::wstring value = source.wstring();
    wcsncpy_s(destination, _countof(destination), value.c_str(),
        _TRUNCATE);
}

BE_BootstrapConfigV1 BuildBootstrapConfig(const std::filesystem::path& install_root) {
    BE_BootstrapConfigV1 config{};
    std::memcpy(config.magic, BETTER_ENDFIELD_NEXT_BOOTSTRAP_MAGIC,
        sizeof(config.magic));
    config.size = sizeof(config);
    CopyBootstrapPath(config.install_root, install_root);

    wchar_t local_app_data[32768]{};
    const DWORD length = GetEnvironmentVariableW(L"LOCALAPPDATA",
        local_app_data, static_cast<DWORD>(_countof(local_app_data)));
    if (length != 0 && length < _countof(local_app_data)) {
        const std::filesystem::path settings_root =
            std::filesystem::path(local_app_data) / L"BetterEndfieldNext";
        CopyBootstrapPath(config.settings_root, settings_root);
        CopyBootstrapPath(config.catalog_root, settings_root / L"catalog");
        CopyBootstrapPath(config.log_root, settings_root / L"logs");
    }
    return config;
}

// This is deliberately a small, self-contained PE loader. It is used only for
// the Host entry point; the Host itself still loads the regular module DLLs
// through LoadLibraryExW after the IL2CPP runtime is ready.
#pragma runtime_checks("", off)
#pragma optimize("", off)
__declspec(noinline) void WINAPI ManualMapShellcode(ManualMappingData* data) {
    if (!data) {
        return;
    }

    BYTE* base = data->image_base;
    auto* dos = reinterpret_cast<IMAGE_DOS_HEADER*>(base);
    auto* nt = reinterpret_cast<IMAGE_NT_HEADERS64*>(
        base + static_cast<uintptr_t>(dos->e_lfanew));
    auto& optional = nt->OptionalHeader;

    const uintptr_t delta = reinterpret_cast<uintptr_t>(base) -
        static_cast<uintptr_t>(optional.ImageBase);
    if (delta != 0 && optional.DataDirectory[IMAGE_DIRECTORY_ENTRY_BASERELOC].Size != 0) {
        auto* reloc = reinterpret_cast<IMAGE_BASE_RELOCATION*>(
            base + optional.DataDirectory[IMAGE_DIRECTORY_ENTRY_BASERELOC].VirtualAddress);
        auto* reloc_end = reinterpret_cast<IMAGE_BASE_RELOCATION*>(
            reinterpret_cast<BYTE*>(reloc) +
            optional.DataDirectory[IMAGE_DIRECTORY_ENTRY_BASERELOC].Size);
        while (reloc < reloc_end && reloc->SizeOfBlock != 0) {
            const UINT count = (reloc->SizeOfBlock - sizeof(IMAGE_BASE_RELOCATION)) /
                sizeof(WORD);
            auto* entries = reinterpret_cast<WORD*>(reloc + 1);
            for (UINT index = 0; index < count; ++index) {
                if (BE_RELOC_TYPE(entries[index]) == IMAGE_REL_BASED_DIR64) {
                    auto* patch = reinterpret_cast<UINT_PTR*>(
                        base + reloc->VirtualAddress + (entries[index] & 0x0fff));
                    *patch += delta;
                }
            }
            reloc = reinterpret_cast<IMAGE_BASE_RELOCATION*>(
                reinterpret_cast<BYTE*>(reloc) + reloc->SizeOfBlock);
        }
    }

    if (optional.DataDirectory[IMAGE_DIRECTORY_ENTRY_IMPORT].Size != 0) {
        auto* imports = reinterpret_cast<IMAGE_IMPORT_DESCRIPTOR*>(
            base + optional.DataDirectory[IMAGE_DIRECTORY_ENTRY_IMPORT].VirtualAddress);
        for (; imports->Name != 0; ++imports) {
            HMODULE dependency = data->load_library_a(
                reinterpret_cast<LPCSTR>(base + imports->Name));
            if (!dependency) {
                data->mapped_module = kMappingInvalid;
                return;
            }

            auto* thunk = reinterpret_cast<ULONG_PTR*>(
                base + (imports->OriginalFirstThunk != 0
                    ? imports->OriginalFirstThunk : imports->FirstThunk));
            auto* function = reinterpret_cast<ULONG_PTR*>(base + imports->FirstThunk);
            for (; *thunk != 0; ++thunk, ++function) {
                FARPROC resolved = nullptr;
                if (BE_IS_ORDINAL(*thunk)) {
                    resolved = data->get_proc_address(dependency,
                        reinterpret_cast<LPCSTR>(*thunk & 0xffff));
                } else {
                    auto* name = reinterpret_cast<IMAGE_IMPORT_BY_NAME*>(base + *thunk);
                    resolved = data->get_proc_address(dependency,
                        reinterpret_cast<LPCSTR>(name->Name));
                }
                if (!resolved) {
                    data->mapped_module = kMappingInvalid;
                    return;
                }
                *function = reinterpret_cast<ULONG_PTR>(resolved);
            }
        }
    }

    if (optional.DataDirectory[IMAGE_DIRECTORY_ENTRY_TLS].Size != 0) {
        auto* tls = reinterpret_cast<IMAGE_TLS_DIRECTORY64*>(
            base + optional.DataDirectory[IMAGE_DIRECTORY_ENTRY_TLS].VirtualAddress);
        auto** callback = reinterpret_cast<PIMAGE_TLS_CALLBACK*>(tls->AddressOfCallBacks);
        for (; callback && *callback; ++callback) {
            (*callback)(base, DLL_PROCESS_ATTACH, nullptr);
        }
    }

    bool seh_failed = false;
    if (data->seh_support && data->rtl_add_function_table &&
        optional.DataDirectory[IMAGE_DIRECTORY_ENTRY_EXCEPTION].Size != 0) {
        const auto exception_directory =
            optional.DataDirectory[IMAGE_DIRECTORY_ENTRY_EXCEPTION];
        seh_failed = !data->rtl_add_function_table(
            reinterpret_cast<PRUNTIME_FUNCTION>(base + exception_directory.VirtualAddress),
            exception_directory.Size / sizeof(RUNTIME_FUNCTION),
            reinterpret_cast<DWORD64>(base));
    }

    auto entry = reinterpret_cast<DllEntryPointFn>(base + optional.AddressOfEntryPoint);
    entry(base, data->reason, data->reserved);
    data->mapped_module = seh_failed ? kMappingSehFailure : reinterpret_cast<HINSTANCE>(base);
}
#pragma runtime_checks("", restore)
#pragma optimize("", on)

std::vector<BYTE> ReadBinaryFile(const std::filesystem::path& path) {
    std::ifstream stream(path, std::ios::binary | std::ios::ate);
    if (!stream) {
        return {};
    }
    const std::streamsize size = stream.tellg();
    if (size <= 0) {
        return {};
    }
    std::vector<BYTE> data(static_cast<size_t>(size));
    stream.seekg(0, std::ios::beg);
    if (!stream.read(reinterpret_cast<char*>(data.data()), size)) {
        return {};
    }
    return data;
}

bool WriteRemote(HANDLE process, void* destination, const void* source, SIZE_T size) {
    SIZE_T written = 0;
    return WriteProcessMemory(process, destination, source, size, &written) == TRUE &&
        written == size;
}

bool ManualMapHost(HANDLE process, const std::filesystem::path& host_path,
    const BE_BootstrapConfigV1& bootstrap) {
    const std::vector<BYTE> file = ReadBinaryFile(host_path);
    if (file.size() < sizeof(IMAGE_DOS_HEADER)) {
        std::wcerr << L"Host image could not be read.\n";
        return false;
    }

    const auto* dos = reinterpret_cast<const IMAGE_DOS_HEADER*>(file.data());
    if (dos->e_magic != IMAGE_DOS_SIGNATURE || dos->e_lfanew <= 0 ||
        static_cast<size_t>(dos->e_lfanew) + sizeof(IMAGE_NT_HEADERS64) > file.size()) {
        std::wcerr << L"Host image has an invalid DOS header.\n";
        return false;
    }
    const auto* nt = reinterpret_cast<const IMAGE_NT_HEADERS64*>(
        file.data() + dos->e_lfanew);
    if (nt->Signature != IMAGE_NT_SIGNATURE ||
        nt->FileHeader.Machine != IMAGE_FILE_MACHINE_AMD64 ||
        nt->OptionalHeader.Magic != IMAGE_NT_OPTIONAL_HDR64_MAGIC ||
        nt->OptionalHeader.SizeOfImage == 0 ||
        nt->OptionalHeader.SizeOfHeaders > file.size()) {
        std::wcerr << L"Host image is not a valid Windows x64 DLL.\n";
        return false;
    }

    const SIZE_T image_size = nt->OptionalHeader.SizeOfImage;
    void* remote_image = VirtualAllocEx(process, nullptr, image_size,
        MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
    if (!remote_image) {
        PrintLastError(L"VirtualAllocEx for Host image failed");
        return false;
    }

    auto release_image = [&] {
        VirtualFreeEx(process, remote_image, 0, MEM_RELEASE);
    };

    if (!WriteRemote(process, remote_image, file.data(), nt->OptionalHeader.SizeOfHeaders)) {
        PrintLastError(L"Writing Host headers failed");
        release_image();
        return false;
    }

    const auto* section = IMAGE_FIRST_SECTION(nt);
    for (WORD index = 0; index < nt->FileHeader.NumberOfSections; ++index, ++section) {
        if (section->SizeOfRawData == 0) {
            continue;
        }
        if (static_cast<uint64_t>(section->PointerToRawData) + section->SizeOfRawData >
            file.size() || section->VirtualAddress >= image_size) {
            std::wcerr << L"Host image contains an invalid section.\n";
            release_image();
            return false;
        }
        const SIZE_T copy_size = std::min<SIZE_T>(section->SizeOfRawData,
            image_size - section->VirtualAddress);
        if (!WriteRemote(process,
                static_cast<BYTE*>(remote_image) + section->VirtualAddress,
                file.data() + section->PointerToRawData, copy_size)) {
            PrintLastError(L"Writing Host section failed");
            release_image();
            return false;
        }
    }

    void* remote_bootstrap = VirtualAllocEx(process, nullptr, sizeof(bootstrap),
        MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
    if (!remote_bootstrap || !WriteRemote(process, remote_bootstrap, &bootstrap,
            sizeof(bootstrap))) {
        PrintLastError(L"Writing Host bootstrap failed");
        if (remote_bootstrap) {
            VirtualFreeEx(process, remote_bootstrap, 0, MEM_RELEASE);
        }
        release_image();
        return false;
    }

    ManualMappingData mapping{};
    mapping.load_library_a = &LoadLibraryA;
    mapping.get_proc_address = &GetProcAddress;
    mapping.rtl_add_function_table = reinterpret_cast<RtlAddFunctionTableFn>(
        &RtlAddFunctionTable);
    mapping.image_base = static_cast<BYTE*>(remote_image);
    mapping.reason = DLL_PROCESS_ATTACH;
    mapping.reserved = remote_bootstrap;
    mapping.seh_support = TRUE;

    void* remote_data = VirtualAllocEx(process, nullptr, sizeof(mapping),
        MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
    constexpr SIZE_T shellcode_size = 0x2000;
    void* remote_shellcode = VirtualAllocEx(process, nullptr, shellcode_size,
        MEM_COMMIT | MEM_RESERVE, PAGE_EXECUTE_READWRITE);
    if (!remote_data || !remote_shellcode ||
        !WriteRemote(process, remote_data, &mapping, sizeof(mapping)) ||
        !WriteRemote(process, remote_shellcode,
            reinterpret_cast<const void*>(&ManualMapShellcode), shellcode_size)) {
        PrintLastError(L"Preparing Host mapping failed");
        if (remote_shellcode) {
            VirtualFreeEx(process, remote_shellcode, 0, MEM_RELEASE);
        }
        if (remote_data) {
            VirtualFreeEx(process, remote_data, 0, MEM_RELEASE);
        }
        VirtualFreeEx(process, remote_bootstrap, 0, MEM_RELEASE);
        release_image();
        return false;
    }

    HANDLE thread = CreateRemoteThread(process, nullptr, 0,
        reinterpret_cast<LPTHREAD_START_ROUTINE>(remote_shellcode), remote_data, 0,
        nullptr);
    if (!thread) {
        PrintLastError(L"CreateRemoteThread for Host mapping failed");
        VirtualFreeEx(process, remote_shellcode, 0, MEM_RELEASE);
        VirtualFreeEx(process, remote_data, 0, MEM_RELEASE);
        VirtualFreeEx(process, remote_bootstrap, 0, MEM_RELEASE);
        release_image();
        return false;
    }

    const DWORD wait = WaitForSingleObject(thread, 30000);
    DWORD exit_code = 0;
    GetExitCodeThread(thread, &exit_code);
    CloseHandle(thread);
    if (wait != WAIT_OBJECT_0) {
        std::wcerr << L"Host mapping thread did not finish (wait=" << wait << L").\n";
        VirtualFreeEx(process, remote_shellcode, 0, MEM_RELEASE);
        VirtualFreeEx(process, remote_data, 0, MEM_RELEASE);
        VirtualFreeEx(process, remote_bootstrap, 0, MEM_RELEASE);
        release_image();
        return false;
    }

    ManualMappingData result{};
    if (!ReadProcessMemory(process, remote_data, &result, sizeof(result), nullptr) ||
        !result.mapped_module || result.mapped_module == kMappingInvalid) {
        std::wcerr << L"Host manual mapping failed (thread exit=" << exit_code << L").\n";
        VirtualFreeEx(process, remote_shellcode, 0, MEM_RELEASE);
        VirtualFreeEx(process, remote_data, 0, MEM_RELEASE);
        VirtualFreeEx(process, remote_bootstrap, 0, MEM_RELEASE);
        release_image();
        return false;
    }
    if (result.mapped_module == kMappingSehFailure) {
        std::wcerr << L"Host mapped, but exception table registration failed.\n";
    }

    VirtualFreeEx(process, remote_shellcode, 0, MEM_RELEASE);
    VirtualFreeEx(process, remote_data, 0, MEM_RELEASE);
    VirtualFreeEx(process, remote_bootstrap, 0, MEM_RELEASE);
    std::wcout << L"BetterEndfieldNext.Host mapped before game resume.\n";
    return true;
}

uintptr_t FindRemoteModuleBase(DWORD process_id, const wchar_t* module_name) {
    HANDLE snapshot = CreateToolhelp32Snapshot(
        TH32CS_SNAPMODULE | TH32CS_SNAPMODULE32, process_id);
    if (snapshot == INVALID_HANDLE_VALUE) {
        return 0;
    }

    MODULEENTRY32W entry{};
    entry.dwSize = sizeof(entry);
    uintptr_t result = 0;
    if (Module32FirstW(snapshot, &entry)) {
        do {
            if (_wcsicmp(entry.szModule, module_name) == 0) {
                result = reinterpret_cast<uintptr_t>(entry.modBaseAddr);
                break;
            }
        } while (Module32NextW(snapshot, &entry));
    }
    CloseHandle(snapshot);
    return result;
}

void* RebaseLocalFunctionForRemoteProcess(DWORD process_id, FARPROC function) {
    if (!function) {
        return nullptr;
    }

    HMODULE owner = nullptr;
    if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
            GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(function), &owner)) {
        return nullptr;
    }

    wchar_t owner_path[32768]{};
    const DWORD length = GetModuleFileNameW(owner, owner_path,
        static_cast<DWORD>(_countof(owner_path)));
    if (length == 0 || length >= _countof(owner_path)) {
        return nullptr;
    }
    const std::wstring module_name =
        std::filesystem::path(owner_path).filename().wstring();
    const uintptr_t remote_base = FindRemoteModuleBase(process_id,
        module_name.c_str());
    if (!remote_base) {
        return nullptr;
    }

    return reinterpret_cast<void*>(remote_base +
        (reinterpret_cast<uintptr_t>(function) -
            reinterpret_cast<uintptr_t>(owner)));
}

bool LoadBootstrapWithRemoteThread(HANDLE process, DWORD process_id,
    const std::filesystem::path& bootstrap_path) {
    const std::wstring bootstrap = bootstrap_path.wstring();
    const SIZE_T bytes = (bootstrap.size() + 1) * sizeof(wchar_t);
    void* remote_path = VirtualAllocEx(process, nullptr, bytes,
        MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
    if (!remote_path || !WriteRemote(process, remote_path, bootstrap.c_str(), bytes)) {
        PrintLastError(L"Preparing Bootstrap DLL path failed");
        if (remote_path) {
            VirtualFreeEx(process, remote_path, 0, MEM_RELEASE);
        }
        return false;
    }

    LPTHREAD_START_ROUTINE remote_load_library = nullptr;
    for (DWORD attempt = 0; attempt < 50 && !remote_load_library; ++attempt) {
        remote_load_library = reinterpret_cast<LPTHREAD_START_ROUTINE>(
            RebaseLocalFunctionForRemoteProcess(process_id,
                reinterpret_cast<FARPROC>(&LoadLibraryW)));
        if (!remote_load_library) {
            Sleep(100);
        }
    }
    if (!remote_load_library) {
        std::wcerr << L"The target LoadLibraryW function could not be resolved.\n";
        VirtualFreeEx(process, remote_path, 0, MEM_RELEASE);
        return false;
    }

    HANDLE thread = CreateRemoteThread(process, nullptr, 0,
        remote_load_library, remote_path, 0, nullptr);
    if (!thread) {
        PrintLastError(L"CreateRemoteThread for Bootstrap DLL failed");
        VirtualFreeEx(process, remote_path, 0, MEM_RELEASE);
        return false;
    }

    const DWORD wait = WaitForSingleObject(thread, 30000);
    DWORD remote_result = 0;
    GetExitCodeThread(thread, &remote_result);
    CloseHandle(thread);
    VirtualFreeEx(process, remote_path, 0, MEM_RELEASE);
    const std::wstring module_name = bootstrap_path.filename().wstring();
    const bool loaded = wait == WAIT_OBJECT_0 &&
        FindRemoteModuleBase(process_id, module_name.c_str()) != 0;
    if (!loaded) {
        std::wcerr << L"Bootstrap DLL load failed (wait=" << wait
                   << L", thread=0x" << std::hex << remote_result << std::dec << L").\n";
        return false;
    }
    std::wcout << L"BetterEndfieldNext.Bootstrap loaded; its worker will wait for IL2CPP.\n";
    return true;
}

bool WaitForModule(DWORD process_id, const wchar_t* module_name,
    DWORD timeout_ms) {
    const ULONGLONG deadline = GetTickCount64() + timeout_ms;
    while (GetTickCount64() < deadline) {
        HANDLE snapshot = CreateToolhelp32Snapshot(
            TH32CS_SNAPMODULE | TH32CS_SNAPMODULE32, process_id);
        if (snapshot != INVALID_HANDLE_VALUE) {
            MODULEENTRY32W entry{};
            entry.dwSize = sizeof(entry);
            if (Module32FirstW(snapshot, &entry)) {
                do {
                    if (_wcsicmp(entry.szModule, module_name) == 0) {
                        CloseHandle(snapshot);
                        return true;
                    }
                } while (Module32NextW(snapshot, &entry));
            }
            CloseHandle(snapshot);
        }

        HANDLE process = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION,
            FALSE, process_id);
        if (process) {
            DWORD exit_code = STILL_ACTIVE;
            const bool queried = GetExitCodeProcess(process, &exit_code) == TRUE;
            CloseHandle(process);
            if (queried && exit_code != STILL_ACTIVE) {
                return false;
            }
        }
        Sleep(100);
    }
    return false;
}

} // namespace

DWORD RunInjector(int argc, wchar_t* argv[]) {
    namespace launch = better_endfield::launch;
    launch::Options options;
    std::wstring launch_error;
    if (!launch::ParseOptions(argc, argv, true, options, launch_error)) {
        launch::ShowError(launch_error);
        return 2;
    }
    launch::ConfigureConsole(options);
    const bool hide_console = options.hide_console || options.steam_launch;
    const auto report_error = [hide_console](const std::wstring& message) {
        if (hide_console) launch::ShowError(message);
        else std::wcerr << message << L'\n';
    };
    const auto& game_path = options.game;
    const auto& game_arguments = options.arguments;
    const bool manual_map = options.manual_map;
    const bool late_manual_map = options.late_manual_map;

    if (better_endfield::HasLocalXInputProxy(game_path)) {
        report_error(L"xinput1_4.dll already exists in the game directory. "
                      L"Use the XInput auto-loader, or uninstall the BE proxy before "
                      L"using this injector. Other loaders must be resolved separately.");
        return 6;
    }

    const std::filesystem::path loader_path = ExecutablePath();
    const std::filesystem::path install_root = loader_path.parent_path().parent_path();
    const std::filesystem::path host_path = install_root / L"runtime" /
        L"BetterEndfieldNext.Host.dll";
    const std::filesystem::path bootstrap_path = install_root / L"loaders" /
        L"BetterEndfieldNext.Bootstrap.dll";
    if (!std::filesystem::is_regular_file(host_path)) {
        report_error(L"BetterEndfieldNext.Host.dll was not found.");
        return 3;
    }
    if (!manual_map && !late_manual_map &&
        !std::filesystem::is_regular_file(bootstrap_path)) {
        report_error(L"BetterEndfieldNext.Bootstrap.dll was not found.");
        return 3;
    }

    launch::VerifiedLauncher official_launcher;
    if (!launch::FindOfficialLauncher(options.parent, official_launcher, launch_error)) {
        launch::ShowError(launch_error);
        return 7;
    }
    launch::OfficialParent official_parent;
    if (!official_parent.Start(official_launcher, launch_error)) {
        launch::ShowError(launch_error);
        return 4;
    }
    official_launcher.file.reset();
    PROCESS_INFORMATION process{};
    if (!official_parent.CreateGame(game_path, game_arguments, CREATE_SUSPENDED, process, launch_error)) {
        launch::ShowError(launch_error);
        return 4;
    }
    OwnedGameStartup owned_game(process);
    InjectionDiagnostics diagnostics(hide_console);

    bool injected = false;

    if (manual_map) {
        const BE_BootstrapConfigV1 bootstrap = BuildBootstrapConfig(install_root);
        injected = ManualMapHost(process.hProcess, host_path, bootstrap);
    } else {
        if (ResumeThread(process.hThread) == static_cast<DWORD>(-1)) {
            report_error(launch::WindowsError(L"Unable to resume Endfield", GetLastError()));
            return 5;
        }
        if (late_manual_map) {
            if (!WaitForModule(process.dwProcessId, L"GameAssembly.dll", 90000)) {
                report_error(L"GameAssembly.dll was not observed before timeout.");
                return 5;
            }
            // Give Unity's native runtime and the GC a short settling window
            // before starting a manually mapped Host thread.
            Sleep(3000);
            const BE_BootstrapConfigV1 bootstrap = BuildBootstrapConfig(install_root);
            injected = ManualMapHost(process.hProcess, host_path, bootstrap);
        } else {
            injected = LoadBootstrapWithRemoteThread(process.hProcess,
                process.dwProcessId, bootstrap_path);
        }
    }

    if (!injected) {
        report_error(L"Unable to load BetterEndfieldNext Host.\n" + diagnostics.Details());
        return 5;
    }

    if (manual_map && ResumeThread(process.hThread) == static_cast<DWORD>(-1)) {
        report_error(launch::WindowsError(L"Unable to resume Endfield after Host mapping", GetLastError()));
        return 5;
    }

    owned_game.Complete();
    DWORD exit_code = 0;
    const bool waited = launch::WaitForGame(process.hProcess, exit_code, launch_error);
    if (!waited) {
        launch::ShowError(launch_error);
        return 8;
    }
    return exit_code;
}

int wmain(int argc, wchar_t* argv[]) {
    DWORD code = 4;
    try { code = RunInjector(argc, argv); }
    catch (const std::exception&) {
        better_endfield::launch::ShowError(L"启动时发生异常，请检查游戏和官方启动器安装。 / Launch failed; check the game and official launcher installation.");
    }
    ExitProcess(code);
}
