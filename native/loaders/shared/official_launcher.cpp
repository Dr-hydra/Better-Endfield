#include "official_launcher.h"

#include <Softpub.h>
#include <Wincrypt.h>
#include <Wintrust.h>
#include <shellapi.h>

#include <algorithm>
#include <cwctype>
#include <iostream>
#include <utility>

namespace better_endfield::launch {
namespace {

constexpr wchar_t kPublisher[] = L"Shanghai Hypergryph Network Technology Co., Ltd.";

bool Equal(const std::wstring& left, const std::wstring& right) {
    return _wcsicmp(left.c_str(), right.c_str()) == 0;
}

bool StartsWith(const std::wstring& text, const wchar_t* prefix) {
    return _wcsnicmp(text.c_str(), prefix, wcslen(prefix)) == 0;
}

struct RegistryKey {
    HKEY value = nullptr;
    ~RegistryKey() { if (value) RegCloseKey(value); }
};

std::wstring ReadRegistry(HKEY root, const wchar_t* key, const wchar_t* name,
    REGSAM view = 0) {
    RegistryKey opened;
    if (RegOpenKeyExW(root, key, 0, KEY_QUERY_VALUE | view, &opened.value) != ERROR_SUCCESS) return {};
    DWORD type = 0, bytes = 0;
    if (RegQueryValueExW(opened.value, name, nullptr, &type, nullptr, &bytes) != ERROR_SUCCESS ||
        (type != REG_SZ && type != REG_EXPAND_SZ) || bytes > 65536) return {};
    std::vector<wchar_t> text(bytes / sizeof(wchar_t) + 1, L'\0');
    if (RegQueryValueExW(opened.value, name, nullptr, nullptr,
            reinterpret_cast<BYTE*>(text.data()), &bytes) != ERROR_SUCCESS) return {};
    if (type != REG_EXPAND_SZ) return text.data();
    const DWORD required = ExpandEnvironmentStringsW(text.data(), nullptr, 0);
    if (!required || required > 32768) return {};
    std::vector<wchar_t> expanded(required);
    if (!ExpandEnvironmentStringsW(text.data(), expanded.data(), required)) return {};
    return expanded.data();
}

std::vector<std::wstring> Subkeys(HKEY root, const wchar_t* path, REGSAM view = 0) {
    RegistryKey opened;
    std::vector<std::wstring> names;
    if (RegOpenKeyExW(root, path, 0, KEY_ENUMERATE_SUB_KEYS | view, &opened.value) != ERROR_SUCCESS) return names;
    for (DWORD index = 0;; ++index) {
        wchar_t name[256]{};
        DWORD size = static_cast<DWORD>(std::size(name));
        const LSTATUS status = RegEnumKeyExW(opened.value, index, name, &size, nullptr, nullptr, nullptr, nullptr);
        if (status == ERROR_NO_MORE_ITEMS) break;
        if (status == ERROR_SUCCESS) names.emplace_back(name, size);
    }
    return names;
}

void AddRoot(std::vector<std::filesystem::path>& roots, const std::filesystem::path& path) {
    if (path.empty()) return;
    auto root = path;
    if (root.has_extension() && Equal(root.extension().wstring(), L".exe")) root = root.parent_path();
    if (std::none_of(roots.begin(), roots.end(), [&](const auto& existing) { return Equal(existing.wstring(), root.wstring()); }))
        roots.push_back(root);
}

std::vector<std::filesystem::path> InstalledRoots() {
    std::vector<std::filesystem::path> roots;
    for (const auto& protocol : Subkeys(HKEY_CLASSES_ROOT, L"")) {
        if (!StartsWith(protocol, L"hypergryphlauncher")) continue;
        const auto command = ReadRegistry(HKEY_CLASSES_ROOT, (protocol + L"\\shell\\open\\command").c_str(), nullptr);
        if (command.empty()) continue;
        int count = 0;
        wchar_t** arguments = CommandLineToArgvW(command.c_str(), &count);
        if (arguments) {
            if (count > 0 && Equal(std::filesystem::path(arguments[0]).filename().wstring(), L"Launcher.exe"))
                AddRoot(roots, arguments[0]);
            LocalFree(arguments);
        }
    }
    constexpr wchar_t uninstall[] = L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
    for (HKEY hive : {HKEY_CURRENT_USER, HKEY_LOCAL_MACHINE}) {
        for (REGSAM view : {KEY_WOW64_64KEY, KEY_WOW64_32KEY}) {
            for (const auto& child : Subkeys(hive, uninstall, view)) {
                const auto key = std::wstring(uninstall) + L"\\" + child;
                const auto name = ReadRegistry(hive, key.c_str(), L"DisplayName", view);
                if (!Equal(name, L"鹰角启动器") && !Equal(name, L"Hypergryph Launcher")) continue;
                AddRoot(roots, ReadRegistry(hive, key.c_str(), L"InstallLocation", view));
            }
        }
    }
    for (const wchar_t* variable : {L"ProgramFiles", L"ProgramFiles(x86)", L"LOCALAPPDATA"}) {
        wchar_t value[32768]{};
        const DWORD length = GetEnvironmentVariableW(variable, value, static_cast<DWORD>(std::size(value)));
        if (length && length < std::size(value)) AddRoot(roots, std::filesystem::path(value) / L"Hypergryph Launcher");
    }
    return roots;
}

std::vector<unsigned long> Version(const std::filesystem::path& path) {
    const auto name = path.parent_path().filename().wstring();
    if (name.empty() || name.find_first_not_of(L"0123456789.") != std::wstring::npos) return {};
    std::vector<unsigned long> result;
    size_t offset = 0;
    try {
        while (offset < name.size()) {
            size_t consumed = 0;
            result.push_back(std::stoul(name.substr(offset), &consumed));
            offset += consumed;
            if (offset < name.size() && name[offset++] != L'.') return {};
        }
    } catch (...) { return {}; }
    return result;
}

struct TrustState {
    GUID policy = WINTRUST_ACTION_GENERIC_VERIFY_V2;
    WINTRUST_DATA data{};
    ~TrustState() {
        data.dwStateAction = WTD_STATEACTION_CLOSE;
        WinVerifyTrust(nullptr, &policy, &data);
    }
};

} // namespace

Handle::~Handle() { reset(); }
Handle::Handle(Handle&& other) noexcept : value_(std::exchange(other.value_, nullptr)) {}
Handle& Handle::operator=(Handle&& other) noexcept {
    if (this != &other) { reset(); value_ = std::exchange(other.value_, nullptr); }
    return *this;
}
void Handle::reset(HANDLE value) {
    if (value_ && value_ != INVALID_HANDLE_VALUE) CloseHandle(value_);
    value_ = value;
}

std::wstring QuoteArgument(const std::wstring& argument) {
    std::wstring result = L"\"";
    size_t slashes = 0;
    for (wchar_t character : argument) {
        if (character == L'\\') { ++slashes; continue; }
        result.append(character == L'\"' ? slashes * 2 + 1 : slashes, L'\\');
        result += character;
        slashes = 0;
    }
    result.append(slashes * 2, L'\\');
    return result + L'\"';
}

bool ParseOptions(int argc, wchar_t* argv[], bool allow_injection, Options& options,
    std::wstring& error) {
    options = {};
    bool game_arguments = false;
    for (int index = 1; index < argc; ++index) {
        const std::wstring option = argv[index];
        if (option == L"--steam-command") break;
        if (game_arguments) { options.arguments.push_back(option); continue; }
        if (option == L"--") game_arguments = true;
        else if (option == L"--game" && index + 1 < argc && options.game.empty()) options.game = argv[++index];
        else if (option == L"--parent" && index + 1 < argc && options.parent.empty()) options.parent = argv[++index];
        else if (allow_injection && option == L"--manual-map") options.manual_map = true;
        else if (allow_injection && option == L"--manual-map-late") options.late_manual_map = true;
        else { error = L"无效启动参数 / Invalid launch option: " + option; return false; }
    }
    if (options.game.empty() || !Equal(options.game.filename().wstring(), L"Endfield.exe")) {
        error = L"请指定 --game <Endfield.exe>。 / Specify --game <Endfield.exe>.";
        return false;
    }
    if (options.manual_map && options.late_manual_map) {
        error = L"--manual-map 与 --manual-map-late 不能同时使用。 / Mapping modes are mutually exclusive.";
        return false;
    }
    options.game = std::filesystem::absolute(options.game).lexically_normal();
    std::error_code filesystem_error;
    if (!std::filesystem::is_regular_file(options.game, filesystem_error)) {
        error = L"找不到游戏文件 / Game executable not found: " + options.game.wstring();
        return false;
    }
    return true;
}

std::wstring WindowsError(const wchar_t* operation, DWORD code) {
    wchar_t* message = nullptr;
    FormatMessageW(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
        nullptr, code, 0, reinterpret_cast<LPWSTR>(&message), 0, nullptr);
    std::wstring result = std::wstring(operation) + L" (Win32 " + std::to_wstring(code) + L")";
    if (message) { result += L"\n"; result += message; LocalFree(message); }
    if (code == ERROR_ELEVATION_REQUIRED)
        result += L"\n需要管理员权限，请允许正常的 Windows UAC 提示。 / Administrator privileges are required; allow Windows UAC.";
    return result;
}

void ShowError(const std::wstring& message) {
    std::wcerr << message << L'\n';
    MessageBoxW(nullptr, message.c_str(), L"Better Endfield — 启动失败 / Launch failed", MB_OK | MB_ICONERROR);
}

bool IsOfficialPublisher(const std::wstring& organization) { return Equal(organization, kPublisher); }

bool VerifyOfficialLauncher(const std::filesystem::path& input, VerifiedLauncher& result,
    std::wstring& error) {
    if (!Equal(input.filename().wstring(), L"Games.exe")) {
        error = L"官方父进程必须是 Games.exe。 / The official parent must be Games.exe.";
        return false;
    }
    const auto path = std::filesystem::absolute(input).lexically_normal();
    Handle file(CreateFileW(path.c_str(), GENERIC_READ, FILE_SHARE_READ, nullptr, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, nullptr));
    if (file.get() == INVALID_HANDLE_VALUE) {
        error = WindowsError(L"无法读取官方启动器 / Cannot read official launcher", GetLastError());
        return false;
    }
    WINTRUST_FILE_INFO info{};
    info.cbStruct = sizeof(info);
    info.pcwszFilePath = path.c_str();
    info.hFile = file.get();
    TrustState trust;
    trust.data.cbStruct = sizeof(trust.data);
    trust.data.dwUIChoice = WTD_UI_NONE;
    trust.data.fdwRevocationChecks = WTD_REVOKE_NONE;
    trust.data.dwUnionChoice = WTD_CHOICE_FILE;
    trust.data.pFile = &info;
    trust.data.dwStateAction = WTD_STATEACTION_VERIFY;
    trust.data.dwProvFlags = WTD_CACHE_ONLY_URL_RETRIEVAL | WTD_REVOCATION_CHECK_NONE;
    const LONG status = WinVerifyTrust(nullptr, &trust.policy, &trust.data);
    if (status != ERROR_SUCCESS) {
        error = L"官方启动器签名验证失败，请通过官方安装程序修复。 / Official launcher signature verification failed; repair its installation.\n" + path.wstring();
        return false;
    }
    const auto provider = WTHelperProvDataFromStateData(trust.data.hWVTStateData);
    const auto signer = provider ? WTHelperGetProvSignerFromChain(provider, 0, FALSE, 0) : nullptr;
    std::wstring organization;
    if (signer && signer->csCertChain && signer->pasCertChain[0].pCert) {
        const auto certificate = signer->pasCertChain[0].pCert;
        const DWORD length = CertGetNameStringW(certificate, CERT_NAME_ATTR_TYPE, 0,
            const_cast<char*>(szOID_ORGANIZATION_NAME), nullptr, 0);
        if (length > 1) {
            organization.resize(length);
            CertGetNameStringW(certificate, CERT_NAME_ATTR_TYPE, 0,
                const_cast<char*>(szOID_ORGANIZATION_NAME), organization.data(), length);
            organization.resize(length - 1);
        }
    }
    if (!IsOfficialPublisher(organization)) {
        error = L"启动器签名者不是鹰角官方组织。 / Launcher signer is not the official Hypergryph publisher.\n" + path.wstring();
        return false;
    }
    result.executable = path;
    result.file = std::move(file);
    return true;
}

bool FindOfficialLauncher(const std::filesystem::path& requested, VerifiedLauncher& result,
    std::wstring& error) {
    if (!requested.empty()) return VerifyOfficialLauncher(requested, result, error);
    std::vector<std::filesystem::path> candidates;
    for (const auto& root : InstalledRoots()) {
        std::error_code filesystem_error;
        if (std::filesystem::is_regular_file(root / L"Games.exe", filesystem_error)) candidates.push_back(root / L"Games.exe");
        for (std::filesystem::directory_iterator current(root, filesystem_error), end;
                !filesystem_error && current != end; current.increment(filesystem_error)) {
            const auto path = current->path() / L"Games.exe";
            if (!Version(path).empty() && std::filesystem::is_regular_file(path, filesystem_error)) candidates.push_back(path);
            filesystem_error.clear();
        }
    }
    std::stable_sort(candidates.begin(), candidates.end(), [](const auto& left, const auto& right) {
        return Version(left) > Version(right);
    });
    for (const auto& path : candidates) if (VerifyOfficialLauncher(path, result, error)) return true;
    error = L"未找到签名有效的国服鹰角启动器 Games.exe。请安装或修复官方启动器后重试。\n"
        L"No valid signed CN Hypergryph launcher Games.exe was found. Install or repair the official launcher and retry.";
    return false;
}

OfficialParent::~OfficialParent() {
    if (!process_.get()) return;
    if (WaitForSingleObject(process_.get(), 0) == WAIT_TIMEOUT && !TerminateProcess(process_.get(), 0)) {
        std::wcerr << WindowsError(L"清理自建启动器父进程 / Clean up owned launcher parent", GetLastError()) << L'\n';
        return;
    }
    WaitForSingleObject(process_.get(), 5000);
}

bool OfficialParent::Start(const VerifiedLauncher& launcher, std::wstring& error) {
    if (process_.get() || !launcher.file.get() || launcher.file.get() == INVALID_HANDLE_VALUE) {
        error = L"Invalid verified launcher state."; return false;
    }
    STARTUPINFOW startup{};
    startup.cb = sizeof(startup);
    startup.dwFlags = STARTF_USESHOWWINDOW;
    startup.wShowWindow = SW_HIDE;
    PROCESS_INFORMATION process{};
    auto command = QuoteArgument(launcher.executable.wstring());
    if (!CreateProcessW(launcher.executable.c_str(), command.data(), nullptr, nullptr, FALSE,
            CREATE_SUSPENDED | CREATE_NO_WINDOW, nullptr, launcher.executable.parent_path().c_str(), &startup, &process)) {
        error = WindowsError(L"创建官方启动器父进程 / Create official launcher parent", GetLastError());
        return false;
    }
    process_.reset(process.hProcess);
    CloseHandle(process.hThread);
    return true;
}

bool OfficialParent::CreateGame(const std::filesystem::path& game,
    const std::vector<std::wstring>& arguments, DWORD creation_flags,
    PROCESS_INFORMATION& process, std::wstring& error) const {
    SIZE_T size = 0;
    InitializeProcThreadAttributeList(nullptr, 1, 0, &size);
    if (!size) { error = WindowsError(L"Prepare parent attributes", GetLastError()); return false; }
    std::vector<unsigned char> storage(size);
    auto attributes = reinterpret_cast<LPPROC_THREAD_ATTRIBUTE_LIST>(storage.data());
    if (!InitializeProcThreadAttributeList(attributes, 1, 0, &size)) {
        error = WindowsError(L"Initialize parent attributes", GetLastError()); return false;
    }
    struct Cleanup { LPPROC_THREAD_ATTRIBUTE_LIST list; ~Cleanup() { DeleteProcThreadAttributeList(list); } } cleanup{attributes};
    HANDLE parent = process_.get();
    if (!UpdateProcThreadAttribute(attributes, 0, PROC_THREAD_ATTRIBUTE_PARENT_PROCESS, &parent, sizeof(parent), nullptr, nullptr)) {
        error = WindowsError(L"Set official parent", GetLastError()); return false;
    }
    STARTUPINFOEXW startup{};
    startup.StartupInfo.cb = sizeof(startup);
    startup.lpAttributeList = attributes;
    auto command = QuoteArgument(game.wstring());
    for (const auto& argument : arguments) command += L" " + QuoteArgument(argument);
    if (!CreateProcessW(game.c_str(), command.data(), nullptr, nullptr, FALSE,
            creation_flags | EXTENDED_STARTUPINFO_PRESENT | CREATE_NO_WINDOW,
            nullptr, game.parent_path().c_str(), &startup.StartupInfo, &process)) {
        error = WindowsError(L"创建游戏进程 / Create game process", GetLastError()); return false;
    }
    return true;
}

bool WaitForGame(HANDLE process, DWORD& exit_code, std::wstring& error) {
    if (WaitForSingleObject(process, INFINITE) != WAIT_OBJECT_0) {
        error = WindowsError(L"等待游戏退出 / Wait for game exit", GetLastError()); return false;
    }
    if (!GetExitCodeProcess(process, &exit_code)) {
        error = WindowsError(L"读取游戏退出状态 / Read game exit status", GetLastError()); return false;
    }
    return true;
}

} // namespace better_endfield::launch
