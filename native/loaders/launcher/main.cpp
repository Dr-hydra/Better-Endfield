#include "../shared/official_launcher.h"

namespace {
DWORD Run(int argc, wchar_t* argv[]) {
    using namespace better_endfield::launch;
    Options options;
    std::wstring error;
    if (!ParseOptions(argc, argv, false, options, error)) { ShowError(error); return 2; }
    VerifiedLauncher launcher;
    if (!FindOfficialLauncher(options.parent, launcher, error)) { ShowError(error); return 7; }
    OfficialParent parent;
    if (!parent.Start(launcher, error)) { ShowError(error); return 4; }
    // Signature and mapped parent image are now fixed. Allow official updater
    // normal access to the executable while the game continues running.
    launcher.file.reset();
    PROCESS_INFORMATION game{};
    if (!parent.CreateGame(options.game, options.arguments, 0, game, error)) { ShowError(error); return 4; }
    Handle process(game.hProcess);
    CloseHandle(game.hThread);
    DWORD exit_code = 0;
    if (!WaitForGame(process.get(), exit_code, error)) { ShowError(error); return 8; }
    return exit_code;
}
} // namespace

int wmain(int argc, wchar_t* argv[]) {
    DWORD code = 4;
    try { code = Run(argc, argv); }
    catch (const std::exception&) {
        better_endfield::launch::ShowError(L"启动时发生异常，请检查游戏和官方启动器安装。 / Launch failed; check the game and official launcher installation.");
    }
    ExitProcess(code);
}
