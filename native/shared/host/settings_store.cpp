#include "settings_store.h"

#include "BetterEndfield/BootstrapConfig.h"

#include <Windows.h>

#include <array>
#include <cstring>

namespace BetterEndfield::Host {
namespace {

std::filesystem::path GetLocalAppDataRoot() {
    std::array<wchar_t, 32768> buffer{};
    const DWORD size = GetEnvironmentVariableW(L"LOCALAPPDATA", buffer.data(),
        static_cast<DWORD>(buffer.size()));
    if (size == 0 || size >= buffer.size()) {
        return {};
    }
    return std::filesystem::path(buffer.data()) / L"BetterEndfield";
}

std::filesystem::path ModulePathFromHandle(void* host_module) {
    std::array<wchar_t, 32768> buffer{};
    const DWORD size = GetModuleFileNameW(static_cast<HMODULE>(host_module),
        buffer.data(), static_cast<DWORD>(buffer.size()));
    if (size == 0 || size >= buffer.size()) {
        return {};
    }
    return std::filesystem::path(buffer.data());
}

std::wstring ToWide(const std::string& value) {
    if (value.empty()) {
        return {};
    }
    const int size = MultiByteToWideChar(CP_UTF8, 0, value.data(),
        static_cast<int>(value.size()), nullptr, 0);
    if (size <= 0) {
        return {};
    }
    std::wstring output(static_cast<size_t>(size), L'\0');
    MultiByteToWideChar(CP_UTF8, 0, value.data(), static_cast<int>(value.size()),
        output.data(), size);
    return output;
}

std::string ToUtf8(const wchar_t* value, int length) {
    if (!value || length <= 0) {
        return {};
    }
    const int size = WideCharToMultiByte(CP_UTF8, 0, value, length, nullptr, 0,
        nullptr, nullptr);
    if (size <= 0) {
        return {};
    }
    std::string output(static_cast<size_t>(size), '\0');
    WideCharToMultiByte(CP_UTF8, 0, value, length, output.data(), size, nullptr,
        nullptr);
    return output;
}

bool ReadBool(const std::filesystem::path& path, const std::wstring& section,
    const wchar_t* key, bool default_value) {
    const wchar_t* default_text = default_value ? L"true" : L"false";
    std::array<wchar_t, 32> value{};
    GetPrivateProfileStringW(section.c_str(), key, default_text, value.data(),
        static_cast<DWORD>(value.size()), path.c_str());
    return _wcsicmp(value.data(), L"true") == 0 ||
        _wcsicmp(value.data(), L"1") == 0 || _wcsicmp(value.data(), L"yes") == 0;
}

} // namespace

HostPaths SettingsStore::DiscoverPaths(void* host_module, const void* bootstrap_data) {
    HostPaths paths{};
    const auto module_path = ModulePathFromHandle(host_module);
    if (!module_path.empty()) {
        const auto runtime_root = module_path.parent_path();
        paths.install_root = runtime_root.parent_path();
    }

    if (bootstrap_data) {
        const auto* bootstrap = static_cast<const BE_BootstrapConfigV1*>(bootstrap_data);
        if (bootstrap->size == sizeof(BE_BootstrapConfigV1) &&
            std::memcmp(bootstrap->magic, BETTER_ENDFIELD_BOOTSTRAP_MAGIC, 9) == 0) {
            if (bootstrap->install_root[0] != L'\0') {
                paths.install_root = bootstrap->install_root;
                paths.bootstrap_install_root = true;
            }
            if (bootstrap->settings_root[0] != L'\0') {
                paths.settings_root = bootstrap->settings_root;
            }
            if (bootstrap->catalog_root[0] != L'\0') {
                paths.catalog_root = bootstrap->catalog_root;
            }
            if (bootstrap->log_root[0] != L'\0') {
                paths.log_root = bootstrap->log_root;
            }
        }
    }

    if (paths.settings_root.empty()) {
        paths.settings_root = GetLocalAppDataRoot();
    }
    if (paths.catalog_root.empty()) {
        paths.catalog_root = paths.settings_root / L"catalog";
    }
    if (paths.log_root.empty()) {
        paths.log_root = paths.settings_root / L"logs";
    }
    paths.modules_root = paths.install_root / L"modules";
    paths.settings_file = paths.settings_root / L"BetterEndfield.ini";
    return paths;
}

bool SettingsStore::Initialize(HostPaths paths, std::string& error) {
    if (paths.install_root.empty()) {
        error = "Host install root could not be determined.";
        return false;
    }

    std::error_code filesystem_error;
    std::filesystem::create_directories(paths.settings_root, filesystem_error);
    std::filesystem::create_directories(paths.catalog_root, filesystem_error);
    std::filesystem::create_directories(paths.log_root, filesystem_error);
    if (filesystem_error) {
        error = "Unable to create BetterEndfield user directories.";
        return false;
    }

    std::array<wchar_t, 32768> configured_modules{};
    GetPrivateProfileStringW(L"Host", L"modules_root", L"", configured_modules.data(),
        static_cast<DWORD>(configured_modules.size()), paths.settings_file.c_str());
    if (!paths.bootstrap_install_root && configured_modules[0] != L'\0') {
        paths.modules_root = configured_modules.data();
    }

    paths_ = std::move(paths);
    return true;
}

const HostPaths& SettingsStore::Paths() const {
    return paths_;
}

std::string SettingsStore::CopyModuleSection(const std::string& module_id) const {
    const std::wstring section = ToWide(module_id);
    if (section.empty()) {
        return {};
    }

    std::array<wchar_t, 32768> values{};
    const DWORD copied = GetPrivateProfileSectionW(section.c_str(), values.data(),
        static_cast<DWORD>(values.size()), paths_.settings_file.c_str());
    if (copied == 0 || copied >= values.size() - 2) {
        return {};
    }

    std::string output;
    const wchar_t* current = values.data();
    const wchar_t* end = values.data() + copied;
    while (current < end && *current != L'\0') {
        const size_t length = wcslen(current);
        output += ToUtf8(current, static_cast<int>(length));
        output.push_back('\n');
        current += length + 1;
    }
    return output;
}

bool SettingsStore::IsModuleEnabled(const std::string& module_id,
    bool default_value) const {
    return ReadBool(paths_.settings_file, ToWide(module_id), L"enabled", default_value);
}

std::filesystem::path SettingsStore::ModulesRoot() const {
    return paths_.modules_root;
}

uint64_t SettingsStore::ChangeToken() const {
    std::error_code error;
    if (!std::filesystem::is_regular_file(paths_.settings_file, error)) {
        return 0;
    }
    const auto write_time = std::filesystem::last_write_time(paths_.settings_file, error);
    if (error) {
        return 0;
    }
    const uint64_t size = std::filesystem::file_size(paths_.settings_file, error);
    const uint64_t time = static_cast<uint64_t>(write_time.time_since_epoch().count());
    return error ? time : time ^ (size + 0x9E3779B97F4A7C15ull);
}

} // namespace BetterEndfield::Host
