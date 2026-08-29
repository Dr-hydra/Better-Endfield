// Launcher for the synthetic-touch probe.
//
// The probe has to inject input into the game's window, and UIPI silently
// drops input aimed at a window whose integrity level is higher than the
// injector's.  The game runs elevated, so a probe started from an ordinary
// shell fails without saying so -- the worst possible outcome for something
// whose only job is to answer a yes/no question.
//
// This module runs inside the game process, which is already elevated, so the
// probe it spawns inherits that token and the integrity levels match.  It does
// nothing else: no hooks, no contracts, no managed calls.
//
// It deliberately takes no settings from BetterEndfield.ini.  The launcher UI
// rewrites that file wholesale from the sections it models, so a hand-added
// section for a research module is deleted on the next save.  Staging this
// module into modules/ is itself the opt-in, and arguments come from a sidecar
// file beside the DLL where nothing else will touch them.

#include <BetterEndfield/ModuleApi.h>

#include <Windows.h>

#include <fstream>
#include <string>

namespace {

constexpr const char* kModuleId = "betterendfield.touch-probe-launcher";
constexpr const wchar_t* kProbeExecutable = L"BetterEndfield.TouchProbe.exe";
constexpr const wchar_t* kArgumentsFile = L"BetterEndfield.TouchProbe.args";

const BE_HostApiV1* g_host = nullptr;
HANDLE g_child = nullptr;
DWORD g_child_id = 0;

void Log(const std::string& message) {
    if (g_host && g_host->log) {
        g_host->log(g_host->context, kModuleId, message.c_str());
    }
}

std::wstring OwnDirectory() {
    HMODULE self = nullptr;
    if (!::GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
                                  GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
                              reinterpret_cast<LPCWSTR>(&OwnDirectory),
                              &self)) {
        return {};
    }
    wchar_t path[MAX_PATH];
    const DWORD length = ::GetModuleFileNameW(self, path, MAX_PATH);
    if (length == 0 || length >= MAX_PATH) {
        return {};
    }
    std::wstring result(path, length);
    const size_t separator = result.find_last_of(L"\\/");
    return separator == std::wstring::npos ? std::wstring{}
                                           : result.substr(0, separator);
}

std::string Narrow(const std::wstring& value) {
    if (value.empty()) {
        return {};
    }
    const int size = ::WideCharToMultiByte(CP_UTF8, 0, value.c_str(),
                                           static_cast<int>(value.size()),
                                           nullptr, 0, nullptr, nullptr);
    if (size <= 0) {
        return {};
    }
    std::string result(static_cast<size_t>(size), '\0');
    ::WideCharToMultiByte(CP_UTF8, 0, value.c_str(),
                          static_cast<int>(value.size()), result.data(), size,
                          nullptr, nullptr);
    return result;
}

std::wstring Widen(const std::string& value) {
    if (value.empty()) {
        return {};
    }
    const int size = ::MultiByteToWideChar(CP_UTF8, 0, value.c_str(),
                                           static_cast<int>(value.size()),
                                           nullptr, 0);
    if (size <= 0) {
        return {};
    }
    std::wstring result(static_cast<size_t>(size), L'\0');
    ::MultiByteToWideChar(CP_UTF8, 0, value.c_str(),
                          static_cast<int>(value.size()), result.data(), size);
    return result;
}

// One line of command-line arguments, e.g. "300 800 --skip-self-test".
// Absent file means no arguments, which is the cursor-following default.
std::wstring ReadArguments(const std::wstring& directory) {
    const std::wstring path = directory + L"\\" + kArgumentsFile;
    std::ifstream stream(path);
    if (!stream) {
        return {};
    }
    std::string line;
    while (std::getline(stream, line)) {
        const size_t begin = line.find_first_not_of(" \t\r");
        if (begin == std::string::npos || line[begin] == ';' ||
            line[begin] == '#') {
            continue;
        }
        const size_t end = line.find_last_not_of(" \t\r");
        return Widen(line.substr(begin, end - begin + 1));
    }
    return {};
}

bool Launch() {
    const std::wstring directory = OwnDirectory();
    if (directory.empty()) {
        Log("Could not resolve the module directory; probe not started.");
        return false;
    }

    const std::wstring executable = directory + L"\\" + kProbeExecutable;
    if (::GetFileAttributesW(executable.c_str()) == INVALID_FILE_ATTRIBUTES) {
        Log("Probe executable is missing, expected it beside the module at " +
            Narrow(executable));
        return false;
    }

    const std::wstring arguments = ReadArguments(directory);
    std::wstring command = L"\"" + executable + L"\"";
    if (!arguments.empty()) {
        command += L" " + arguments;
    }

    STARTUPINFOW startup{};
    startup.cb = sizeof(startup);
    PROCESS_INFORMATION process{};

    // CREATE_NEW_CONSOLE because the host is a windowed game with no console
    // of its own, and the probe is meant to be read while it runs.
    if (!::CreateProcessW(nullptr, command.data(), nullptr, nullptr, FALSE,
                          CREATE_NEW_CONSOLE, nullptr, directory.c_str(),
                          &startup, &process)) {
        Log("CreateProcessW failed for the probe, GetLastError=" +
            std::to_string(::GetLastError()));
        return false;
    }

    ::CloseHandle(process.hThread);
    g_child = process.hProcess;
    g_child_id = process.dwProcessId;

    Log("Probe started as pid " + std::to_string(g_child_id) +
        " with the game's token; command line: " + Narrow(command));
    return true;
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    g_host = host;
    Launch();
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char*) {
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    if (g_child) {
        // The probe holds a synthetic pointer device open, so it should not
        // outlive the game it was aimed at.
        ::TerminateProcess(g_child, 0);
        ::CloseHandle(g_child);
        g_child = nullptr;
        Log("Probe process terminated.");
    }
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Better Endfield Touch Probe Launcher", "0.2.0",
        BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown,
};

}  // namespace

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1(void) {
    return &kApi;
}
