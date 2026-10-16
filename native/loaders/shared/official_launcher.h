#pragma once

#include <Windows.h>
#include <filesystem>
#include <string>
#include <vector>

namespace better_endfield::launch {

struct Options {
    std::filesystem::path game;
    std::filesystem::path parent;
    std::vector<std::wstring> arguments;
    bool manual_map = false;
    bool late_manual_map = false;
    bool steam_launch = false;
    bool hide_console = false;
    bool no_parent = false;
};

bool ParseOptions(int argc, wchar_t* argv[], bool allow_injection, Options& options,
    std::wstring& error);
bool CreateGameProcess(const std::filesystem::path& game,
    const std::vector<std::wstring>& arguments, DWORD creation_flags,
    PROCESS_INFORMATION& process, std::wstring& error);
std::wstring QuoteArgument(const std::wstring& argument);
std::wstring WindowsError(const wchar_t* operation, DWORD code);
void ShowError(const std::wstring& message);
void ConfigureConsole(const Options& options);

bool ShouldInjectSteamOverlay(const Options& options);
void PrepareSteamEnvironment(const Options& options);
std::filesystem::path FindSteamOverlayDll();
bool InjectSteamOverlay(HANDLE process, const Options& options);

class Handle {
public:
    explicit Handle(HANDLE value = nullptr) : value_(value) {}
    ~Handle();
    Handle(const Handle&) = delete;
    Handle& operator=(const Handle&) = delete;
    Handle(Handle&& other) noexcept;
    Handle& operator=(Handle&& other) noexcept;
    HANDLE get() const { return value_; }
    void reset(HANDLE value = nullptr);
private:
    HANDLE value_ = nullptr;
};

struct VerifiedLauncher {
    std::filesystem::path executable;
    // Deny file replacement from signature verification through process creation.
    Handle file;
};

bool IsOfficialPublisher(const std::wstring& organization);
bool VerifyOfficialLauncher(const std::filesystem::path& path, VerifiedLauncher& result,
    std::wstring& error);
bool FindOfficialLauncher(const std::filesystem::path& requested, VerifiedLauncher& result,
    std::wstring& error);

class OfficialParent {
public:
    OfficialParent() = default;
    ~OfficialParent();
    OfficialParent(const OfficialParent&) = delete;
    OfficialParent& operator=(const OfficialParent&) = delete;
    bool Start(const VerifiedLauncher& launcher, std::wstring& error);
    bool CreateGame(const std::filesystem::path& game,
        const std::vector<std::wstring>& arguments, DWORD creation_flags,
        PROCESS_INFORMATION& process, std::wstring& error) const;
private:
    Handle process_;
};

bool WaitForGame(HANDLE process, DWORD& exit_code, std::wstring& error);

} // namespace better_endfield::launch
