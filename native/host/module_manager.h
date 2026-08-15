#pragma once

#include "BetterEndfield/ModuleApi.h"

#include <Windows.h>

#include <filesystem>
#include <string>
#include <unordered_set>
#include <vector>

namespace BetterEndfield::Host {

class HostRuntime;
class Logger;
class SettingsStore;

class ModuleManager {
public:
    ModuleManager(HostRuntime& runtime, Logger& logger, SettingsStore& settings);
    ~ModuleManager();

    bool LoadAll();
    bool HasPendingModules() const;
    size_t LoadedCount() const;
    void ReloadConfigurations();
    void Shutdown();

private:
    struct LoadedModule {
        HMODULE library = nullptr;
        const BE_ModuleApiV1* api = nullptr;
        std::string module_id;
    };

    enum class LoadResult {
        Loaded,
        AlreadyLoaded,
        Deferred,
        Rejected,
        Disabled,
    };

    LoadResult LoadManifest(const std::filesystem::path& manifest_path);
    bool DependenciesReady(const std::filesystem::path& manifest_path,
        const std::string& module_id);
    bool IsLoaded(const std::string& module_id) const;

    HostRuntime& runtime_;
    Logger& logger_;
    SettingsStore& settings_;
    std::vector<LoadedModule> modules_;
    std::unordered_set<std::string> rejected_modules_;
    std::unordered_set<std::string> deferred_log_once_;
    size_t pending_module_count_ = 0;
};

} // namespace BetterEndfield::Host
