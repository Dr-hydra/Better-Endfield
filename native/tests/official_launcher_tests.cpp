#include "../loaders/shared/official_launcher.h"
#include <shellapi.h>

#include <fstream>
#include <iostream>
#include <stdexcept>

namespace {
int checks = 0;
void Check(bool value, const char* message) {
    if (!value) throw std::runtime_error(message);
    ++checks;
}
bool Parse(std::vector<std::wstring> arguments, bool injection,
    better_endfield::launch::Options& options) {
    std::vector<wchar_t*> pointers;
    for (auto& argument : arguments) pointers.push_back(argument.data());
    std::wstring error;
    return better_endfield::launch::ParseOptions(static_cast<int>(pointers.size()), pointers.data(), injection, options, error);
}
} // namespace

int wmain(int argc, wchar_t* argv[]) {
    using namespace better_endfield::launch;
    if (argc == 2 && (std::wstring(argv[1]) == L"--console-child" ||
        std::wstring(argv[1]) == L"--hidden-console-child")) {
        const HWND original = GetConsoleWindow();
        if (!original) return 10;
        Options options;
        ConfigureConsole(options);
        if (GetConsoleWindow() != original) return 11;
        options.steam_launch = std::wstring(argv[1]) == L"--console-child";
        options.hide_console = !options.steam_launch;
        ConfigureConsole(options);
        if (GetConsoleWindow()) return 12;
        Sleep(100);
        return 37;
    }
    if (argc < 2 || argc > 3) return 2;
    try {
        const auto root = std::filesystem::absolute(argv[1]);
        std::filesystem::create_directories(root);
        const auto game = root / L"Endfield.exe";
        if (std::filesystem::exists(game)) throw std::runtime_error("Use an empty fixture directory.");
        { std::ofstream file(game); file << "Inert test fixture; never executable."; }
        const std::vector<std::wstring> values{L"", L"with spaces", L"中文", L"quote\"value", L"C:\\trailing \\", L"\\\\server\\share\\", L"two\\\\\"quotes"};
        std::wstring command = L"dummy.exe";
        for (const auto& value : values) command += L" " + QuoteArgument(value);
        int count = 0;
        wchar_t** parsed = CommandLineToArgvW(command.c_str(), &count);
        Check(parsed && count == static_cast<int>(values.size()) + 1, "Quoted argument count changed.");
        for (size_t index = 0; index < values.size(); ++index)
            Check(values[index] == parsed[index + 1], "Quoted argument changed.");
        LocalFree(parsed);
        Options options;
        Check(Parse({L"helper", L"--game", game.wstring(), L"--", L"-force-d3d11", L"", L"--steam-command", L"international.exe", L"--unexpected"}, false, options), "Steam command rejected.");
        Check(options.arguments == std::vector<std::wstring>{L"-force-d3d11", L""}, "Steam residual command reached game arguments.");
        Check(options.steam_launch, "Steam launch did not request console detachment.");
        Check(Parse({L"helper", L"--parent", L"C:\\Official\\Games.exe", L"--game", game.wstring(), L"--steam-command", L"ignored"}, false, options), "Explicit parent rejected.");
        Check(options.parent == L"C:\\Official\\Games.exe" && options.arguments.empty(), "Explicit parent changed.");
        Check(options.steam_launch, "Steam marker before game arguments was lost.");
        Check(!Parse({L"helper", L"--game", game.wstring(), L"--manual-map"}, false, options), "Launcher accepted injection mode.");
        Check(!Parse({L"helper", L"--game", game.wstring(), L"--manual-map", L"--manual-map-late"}, true, options), "Conflicting injection modes accepted.");
        Check(Parse({L"helper", L"--game", game.wstring(), L"--manual-map-late"}, true, options) && options.late_manual_map, "Injector mode was lost.");
        Check(!options.steam_launch, "CLI launch unexpectedly requested console detachment.");
        Check(!options.hide_console, "CLI launch unexpectedly enabled explicit console hiding.");
        Check(Parse({L"helper", L"--hide-console", L"--game", game.wstring(), L"--", L"--hide-console"}, false, options)
            && options.hide_console && !options.steam_launch, "GUI launcher console option was lost.");
        Check(options.arguments == std::vector<std::wstring>{L"--hide-console"}, "Console option after separator was not preserved as a game argument.");
        Check(Parse({L"helper", L"--hide-console", L"--game", game.wstring(), L"--manual-map-late"}, true, options)
            && options.hide_console && options.late_manual_map, "GUI injector console option was lost.");
        Check(Parse({L"helper", L"--game", game.wstring(), L"--manual-map-late", L"--steam-command", L"ignored"}, true, options)
            && options.steam_launch && options.late_manual_map, "Injector Steam launch mode was lost.");

        // Steam overlay injection checks: non-Steam users must remain 100% unaffected.
        Options non_steam_options{};
        SetEnvironmentVariableW(L"SteamOverlayGameId", nullptr);
        SetEnvironmentVariableW(L"SteamAppId", nullptr);
        SetEnvironmentVariableW(L"SteamGameId", nullptr);
        Check(!ShouldInjectSteamOverlay(non_steam_options), "Non-Steam options triggered Steam overlay injection.");

        Options steam_options{};
        steam_options.steam_launch = true;
        Check(ShouldInjectSteamOverlay(steam_options), "Steam options did not trigger Steam overlay injection.");
        PrepareSteamEnvironment(steam_options);
        wchar_t steam_env_buf[64]{};
        Check(GetEnvironmentVariableW(L"SteamOverlayGameId", steam_env_buf, _countof(steam_env_buf)) > 0
            && std::wstring(steam_env_buf) == L"4732690", "SteamOverlayGameId not set correctly.");
        Check(GetEnvironmentVariableW(L"SteamAppId", steam_env_buf, _countof(steam_env_buf)) > 0
            && std::wstring(steam_env_buf) == L"4732690", "SteamAppId not set correctly.");
        Check(GetEnvironmentVariableW(L"SteamGameId", steam_env_buf, _countof(steam_env_buf)) > 0
            && std::wstring(steam_env_buf) == L"4732690", "SteamGameId not set correctly.");

        const auto overlay_dll = FindSteamOverlayDll();
        if (!overlay_dll.empty()) {
            std::error_code overlay_ec;
            Check(std::filesystem::is_regular_file(overlay_dll, overlay_ec), "Found overlay DLL is not a regular file.");
        }

        // Exercise real console detachment and the same wait/exit-code path as
        // the launch helpers, using only this inert test child (never the game).
        wchar_t executable[32768]{};
        Check(GetModuleFileNameW(nullptr, executable, _countof(executable)) != 0, "Test executable path missing.");
        for (const auto* mode : {L"--console-child", L"--hidden-console-child"}) {
            auto child_command = QuoteArgument(executable) + L" " + mode;
            STARTUPINFOW startup{};
            startup.cb = sizeof(startup);
            startup.dwFlags = STARTF_USESHOWWINDOW;
            startup.wShowWindow = SW_HIDE;
            PROCESS_INFORMATION child{};
            Check(CreateProcessW(executable, child_command.data(), nullptr, nullptr, FALSE,
                CREATE_NEW_CONSOLE, nullptr, nullptr, &startup, &child) != FALSE, "Console test child failed to start.");
            Handle child_process(child.hProcess);
            CloseHandle(child.hThread);
            DWORD child_exit_code = 0;
            std::wstring child_error;
            Check(WaitForGame(child_process.get(), child_exit_code, child_error), "Console test child wait failed.");
            Check(child_exit_code == 37, "Console detachment changed CLI behavior, process lifetime, or exit code.");
        }
        Check(IsOfficialPublisher(L"Shanghai Hypergryph Network Technology Co., Ltd."), "Official publisher rejected.");
        Check(!IsOfficialPublisher(L"Shanghai Hypergryph Network Technology Co., Ltd. Untrusted"), "Publisher suffix spoof accepted.");
        Check(!IsOfficialPublisher(L"Microsoft Corporation"), "Different valid publisher accepted.");
        const auto unsigned_parent = root / L"Games.exe";
        { std::ofstream file(unsigned_parent); file << "Inert unsigned Games.exe; never executable."; }
        VerifiedLauncher launcher;
        std::wstring error;
        Check(!VerifyOfficialLauncher(unsigned_parent, launcher, error), "Unsigned Games.exe accepted.");
        if (argc == 3 && std::wstring(argv[2]) == L"--verify-installed") {
            Check(FindOfficialLauncher({}, launcher, error), "Installed official launcher discovery/signature verification failed.");
            std::wcout << L"Verified official launcher (not started): " << launcher.executable.wstring() << L'\n';
        }
        std::wcout << L"Official launcher: " << checks << L" checks passed; no game or Steam processes started.\n";
        return 0;
    } catch (const std::exception& exception) {
        std::cerr << exception.what() << '\n'; return 1;
    }
}
