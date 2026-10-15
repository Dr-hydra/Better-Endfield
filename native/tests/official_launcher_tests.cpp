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
        Check(Parse({L"helper", L"--parent", L"C:\\Official\\Games.exe", L"--game", game.wstring(), L"--steam-command", L"ignored"}, false, options), "Explicit parent rejected.");
        Check(options.parent == L"C:\\Official\\Games.exe" && options.arguments.empty(), "Explicit parent changed.");
        Check(!Parse({L"helper", L"--game", game.wstring(), L"--manual-map"}, false, options), "Launcher accepted injection mode.");
        Check(!Parse({L"helper", L"--game", game.wstring(), L"--manual-map", L"--manual-map-late"}, true, options), "Conflicting injection modes accepted.");
        Check(Parse({L"helper", L"--game", game.wstring(), L"--manual-map-late"}, true, options) && options.late_manual_map, "Injector mode was lost.");
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
        std::wcout << L"Official launcher: " << checks << L" checks passed; no processes started.\n";
        return 0;
    } catch (const std::exception& exception) {
        std::cerr << exception.what() << '\n'; return 1;
    }
}
