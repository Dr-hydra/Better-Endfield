#include <windows.h>

#include <filesystem>
#include <iostream>
#include <string>
#include <vector>

namespace {

struct Handle {
    HANDLE value = nullptr;
    Handle() = default;
    explicit Handle(HANDLE handle) : value(handle) {}
    Handle(const Handle&) = delete;
    Handle& operator=(const Handle&) = delete;
    ~Handle() { if (value && value != INVALID_HANDLE_VALUE) CloseHandle(value); }
};

void Error(const wchar_t* operation, DWORD code) {
    wchar_t* message = nullptr;
    FormatMessageW(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM |
        FORMAT_MESSAGE_IGNORE_INSERTS, nullptr, code, 0,
        reinterpret_cast<LPWSTR>(&message), 0, nullptr);
    std::wcerr << operation << L" failed: Win32 " << code;
    if (message) { std::wcerr << L" (" << message << L")"; LocalFree(message); }
    if (code == ERROR_ELEVATION_REQUIRED)
        std::wcerr << L" Elevation is required; no automatic runas fallback was attempted.";
    std::wcerr << L'\n';
}

// CommandLineToArgvW/CRT quoting, including empty arguments and trailing slashes.
std::wstring Quote(const std::wstring& argument) {
    std::wstring result = L"\"";
    size_t slashes = 0;
    for (wchar_t character : argument) {
        if (character == L'\\') { ++slashes; continue; }
        if (character == L'\"') {
            result.append(slashes * 2 + 1, L'\\');
            result += L'\"';
        } else {
            result.append(slashes, L'\\');
            result += character;
        }
        slashes = 0;
    }
    result.append(slashes * 2, L'\\');
    result += L'\"';
    return result;
}

struct SuspendedParent {
    Handle process;
    DWORD id;
    SuspendedParent(HANDLE handle, DWORD process_id) : process(handle), id(process_id) {}
    ~SuspendedParent() {
        // This handle comes exclusively from our own CREATE_SUSPENDED call.
        // Never terminate the target process or open/terminate another launcher.
        const DWORD state = WaitForSingleObject(process.value, 0);
        if (state == WAIT_TIMEOUT && !TerminateProcess(process.value, 0)) {
            Error(L"Terminate own suspended parent", GetLastError());
            return;
        }
        const DWORD wait = WaitForSingleObject(process.value, 5000);
        if (wait == WAIT_OBJECT_0)
            std::wcout << L"Suspended parent cleaned: " << id << L'\n';
        else
            Error(L"Wait for own suspended parent cleanup", wait == WAIT_FAILED ? GetLastError() : ERROR_TIMEOUT);
    }
};

struct Attributes {
    std::vector<unsigned char> storage;
    LPPROC_THREAD_ATTRIBUTE_LIST list = nullptr;
    HANDLE parent_value = nullptr;
    ~Attributes() { if (list) DeleteProcThreadAttributeList(list); }
    bool Initialize(HANDLE parent) {
        SIZE_T bytes = 0;
        InitializeProcThreadAttributeList(nullptr, 1, 0, &bytes);
        if (!bytes) { Error(L"Measure process attributes", GetLastError()); return false; }
        storage.resize(bytes);
        auto candidate = reinterpret_cast<LPPROC_THREAD_ATTRIBUTE_LIST>(storage.data());
        if (!InitializeProcThreadAttributeList(candidate, 1, 0, &bytes)) {
            Error(L"Initialize process attributes", GetLastError()); return false;
        }
        list = candidate;
        parent_value = parent;
        if (!UpdateProcThreadAttribute(list, 0, PROC_THREAD_ATTRIBUTE_PARENT_PROCESS,
                &parent_value, sizeof(parent_value), nullptr, nullptr)) {
            Error(L"Set parent process attribute", GetLastError()); return false;
        }
        return true;
    }
};

DWORD Run(int argc, wchar_t* argv[]) {
    std::filesystem::path parent_path, game_path;
    std::vector<std::wstring> arguments;
    for (int index = 1; index < argc; ++index) {
        const std::wstring option = argv[index];
        if (option == L"--parent" && index + 1 < argc && parent_path.empty())
            parent_path = argv[++index];
        else if (option == L"--game" && index + 1 < argc && game_path.empty())
            game_path = argv[++index];
        else if (option == L"--") {
            for (++index; index < argc; ++index) arguments.emplace_back(argv[index]);
            break;
        } else {
            std::wcerr << L"Unknown, duplicate or incomplete option: " << option << L'\n';
            return ERROR_BAD_ARGUMENTS;
        }
    }
    if (parent_path.empty() || game_path.empty()) {
        std::wcerr << L"Usage: LaunchBridge.exe --parent <Games.exe> --game <target.exe> -- <target arguments>\n";
        return ERROR_BAD_ARGUMENTS;
    }
    parent_path = std::filesystem::absolute(parent_path).lexically_normal();
    game_path = std::filesystem::absolute(game_path).lexically_normal();
    if (!std::filesystem::is_regular_file(parent_path) || !std::filesystem::is_regular_file(game_path)) {
        std::wcerr << L"Both --parent and --game must refer to existing executable files.\n";
        return ERROR_FILE_NOT_FOUND;
    }

    STARTUPINFOW parent_start{};
    parent_start.cb = sizeof(parent_start);
    parent_start.dwFlags = STARTF_USESHOWWINDOW;
    parent_start.wShowWindow = SW_HIDE;
    PROCESS_INFORMATION parent_info{};
    std::wstring parent_command = Quote(parent_path.wstring());
    if (!CreateProcessW(parent_path.c_str(), parent_command.data(), nullptr, nullptr, FALSE,
            CREATE_SUSPENDED | CREATE_NO_WINDOW, nullptr, parent_path.parent_path().c_str(),
            &parent_start, &parent_info)) {
        const DWORD code = GetLastError(); Error(L"Create suspended parent", code); return code;
    }
    SuspendedParent parent(parent_info.hProcess, parent_info.dwProcessId);
    {
        Handle parent_thread(parent_info.hThread);
        // Deliberately never ResumeThread: official launcher code is not run.
    }
    std::wcout << L"Suspended parent created: " << parent.id << L'\n';

    Attributes attributes;
    if (!attributes.Initialize(parent.process.value)) return ERROR_INVALID_PARAMETER;
    STARTUPINFOEXW target_start{};
    target_start.StartupInfo.cb = sizeof(target_start);
    target_start.lpAttributeList = attributes.list;
    PROCESS_INFORMATION target_info{};
    std::wstring target_command = Quote(game_path.wstring());
    for (const auto& argument : arguments) target_command += L" " + Quote(argument);
    // Console probes must not attach to the suspended parent's uninitialized
    // console. CREATE_NO_WINDOW is ignored for GUI target executables.
    if (!CreateProcessW(game_path.c_str(), target_command.data(), nullptr, nullptr, FALSE,
            EXTENDED_STARTUPINFO_PRESENT | CREATE_NO_WINDOW, nullptr, game_path.parent_path().c_str(),
            &target_start.StartupInfo, &target_info)) {
        const DWORD code = GetLastError(); Error(L"Create target", code); return code;
    }
    Handle target(target_info.hProcess);
    { Handle target_thread(target_info.hThread); }
    std::wcout << L"Target created: " << target_info.dwProcessId << L'\n';
    std::wcout.flush();
    const DWORD wait = WaitForSingleObject(target.value, INFINITE);
    if (wait != WAIT_OBJECT_0) {
        const DWORD code = wait == WAIT_FAILED ? GetLastError() : ERROR_GEN_FAILURE;
        Error(L"Wait for target", code); return code;
    }
    DWORD exit_code = 0;
    if (!GetExitCodeProcess(target.value, &exit_code)) {
        const DWORD code = GetLastError(); Error(L"Read target exit code", code); return code;
    }
    std::wcout << L"Target exit code: " << exit_code << L'\n';
    return exit_code;
}
} // namespace

int wmain(int argc, wchar_t* argv[]) {
    DWORD exit_code = ERROR_GEN_FAILURE;
    try { exit_code = Run(argc, argv); }
    catch (const std::exception& exception) { std::cerr << exception.what() << '\n'; }
    std::wcout.flush();
    std::wcerr.flush();
    ExitProcess(exit_code);
}
