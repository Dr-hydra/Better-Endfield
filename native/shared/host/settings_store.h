#pragma once

#include <filesystem>
#include <string>

namespace BetterEndfield::Host {

struct HostPaths {
    std::filesystem::path install_root;
    std::filesystem::path settings_root;
    std::filesystem::path catalog_root;
    std::filesystem::path log_root;
    std::filesystem::path modules_root;
    std::filesystem::path settings_file;
};

class SettingsStore {
public:
    bool Initialize(HostPaths paths, std::string& error);

    const HostPaths& Paths() const;
    std::string CopyModuleSection(const std::string& module_id) const;
    bool IsModuleEnabled(const std::string& module_id, bool default_value) const;
    std::filesystem::path ModulesRoot() const;
    uint64_t ChangeToken() const;

    static HostPaths DiscoverPaths(void* host_module, const void* bootstrap_data);

private:
    HostPaths paths_;
};

} // namespace BetterEndfield::Host
