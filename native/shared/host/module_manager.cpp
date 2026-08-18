#include "module_manager.h"

#include "host_runtime.h"
#include "logging.h"
#include "settings_store.h"

#include <Windows.h>

#include <algorithm>
#include <array>
#include <cctype>
#include <string_view>
#include <vector>

namespace BetterEndfield::Host {
namespace {

std::string ToUtf8(const std::wstring& value) {
    if (value.empty()) {
        return {};
    }
    const int size = WideCharToMultiByte(CP_UTF8, 0, value.data(),
        static_cast<int>(value.size()), nullptr, 0, nullptr, nullptr);
    std::string output(static_cast<size_t>(size), '\0');
    WideCharToMultiByte(CP_UTF8, 0, value.data(), static_cast<int>(value.size()),
        output.data(), size, nullptr, nullptr);
    return output;
}

std::wstring ReadManifestValue(const std::filesystem::path& path, const wchar_t* key) {
    std::array<wchar_t, 1024> value{};
    GetPrivateProfileStringW(L"Module", key, L"", value.data(),
        static_cast<DWORD>(value.size()), path.c_str());
    return value.data();
}

bool ReadManifestBool(const std::filesystem::path& path, const wchar_t* key,
    bool default_value) {
    const wchar_t* default_text = default_value ? L"true" : L"false";
    std::array<wchar_t, 32> value{};
    GetPrivateProfileStringW(L"Module", key, default_text, value.data(),
        static_cast<DWORD>(value.size()), path.c_str());
    return _wcsicmp(value.data(), L"true") == 0 ||
        _wcsicmp(value.data(), L"1") == 0 || _wcsicmp(value.data(), L"yes") == 0;
}

std::vector<std::string> SplitRequirements(const std::wstring& value) {
    const std::string utf8 = ToUtf8(value);
    std::vector<std::string> result;
    std::string_view remaining(utf8);
    while (!remaining.empty()) {
        const size_t separator = remaining.find('|');
        std::string_view item = remaining.substr(0, separator);
        while (!item.empty() && std::isspace(static_cast<unsigned char>(item.front()))) {
            item.remove_prefix(1);
        }
        while (!item.empty() && std::isspace(static_cast<unsigned char>(item.back()))) {
            item.remove_suffix(1);
        }
        if (!item.empty()) {
            result.emplace_back(item);
        }
        if (separator == std::string_view::npos) {
            break;
        }
        remaining.remove_prefix(separator + 1);
    }
    return result;
}

} // namespace

ModuleManager::ModuleManager(HostRuntime& runtime, Logger& logger,
    SettingsStore& settings)
    : runtime_(runtime), logger_(logger), settings_(settings) {}

ModuleManager::~ModuleManager() {
    Shutdown();
}

bool ModuleManager::LoadAll() {
    const auto modules_root = settings_.ModulesRoot();
    std::error_code error;
    if (!std::filesystem::is_directory(modules_root, error)) {
        logger_.Write("host.modules", "Module directory is missing: " +
            modules_root.string());
        return false;
    }

    std::vector<std::filesystem::path> manifests;
    for (const auto& entry : std::filesystem::directory_iterator(modules_root, error)) {
        if (error) {
            logger_.Write("host.modules", "Unable to enumerate module directory.");
            return false;
        }
        if (!entry.is_regular_file() || entry.path().extension() != ".ini" ||
            entry.path().filename().wstring().find(L".module") == std::wstring::npos) {
            continue;
        }
        manifests.push_back(entry.path());
    }
    std::sort(manifests.begin(), manifests.end(),
        [](const auto& left, const auto& right) {
            return left.filename().wstring() < right.filename().wstring();
        });

    pending_module_count_ = 0;
    for (const auto& manifest : manifests) {
        if (LoadManifest(manifest) == LoadResult::Deferred) {
            ++pending_module_count_;
        }
    }
    return !modules_.empty();
}

bool ModuleManager::IsLoaded(const std::string& module_id) const {
    return std::any_of(modules_.begin(), modules_.end(),
        [&module_id](const LoadedModule& module) {
            return module.module_id == module_id;
        });
}

bool ModuleManager::DependenciesReady(const std::filesystem::path& manifest_path,
    const std::string& module_id) {
    const auto requirements = SplitRequirements(
        ReadManifestValue(manifest_path, L"requires"));
    std::vector<std::string> missing;
    for (const auto& assembly : requirements) {
        if (!runtime_.HasAssembly(assembly)) {
            missing.push_back(assembly);
        }
    }
    if (missing.empty()) {
        deferred_log_once_.erase(module_id);
        return true;
    }
    if (deferred_log_once_.insert(module_id).second) {
        std::string message = "Module waiting for IL2CPP assemblies: " + module_id + " [";
        for (size_t index = 0; index < missing.size(); ++index) {
            if (index != 0) {
                message += ", ";
            }
            message += missing[index];
        }
        message += "]";
        logger_.Write("host.modules", message);
    }
    return false;
}

ModuleManager::LoadResult ModuleManager::LoadManifest(
    const std::filesystem::path& manifest_path) {
    const std::wstring module_id_wide = ReadManifestValue(manifest_path, L"id");
    const std::wstring library_name = ReadManifestValue(manifest_path, L"library");
    const std::wstring api_name_wide = ReadManifestValue(manifest_path, L"api");
    const std::wstring contract = ReadManifestValue(manifest_path, L"contract");
    const std::wstring contract_version =
        ReadManifestValue(manifest_path, L"contract_version");
    const std::string module_id = ToUtf8(module_id_wide);
    const std::string api_name = ToUtf8(api_name_wide);
    const std::filesystem::path library_file(library_name);
    if (module_id.empty() || library_name.empty() || api_name.empty() ||
        _wcsicmp(contract.c_str(), L"il2cpp-metadata") != 0 ||
        contract_version != L"1" || library_file.has_root_path() ||
        library_file.has_parent_path() ||
        _wcsicmp(library_file.extension().c_str(), L".dll") != 0) {
        logger_.Write("host.modules", "Ignoring invalid module manifest: " +
            manifest_path.string());
        return LoadResult::Rejected;
    }
    const bool manifest_enabled = ReadManifestBool(manifest_path, L"enabled", true);
    if (!settings_.IsModuleEnabled(module_id, manifest_enabled)) {
        return LoadResult::Disabled;
    }
    if (IsLoaded(module_id)) {
        return LoadResult::AlreadyLoaded;
    }
    if (rejected_modules_.contains(module_id)) {
        return LoadResult::Rejected;
    }
    if (!DependenciesReady(manifest_path, module_id)) {
        return LoadResult::Deferred;
    }

    const auto library_path = manifest_path.parent_path() / library_name;
    HMODULE library = LoadLibraryExW(library_path.c_str(), nullptr,
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR | LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
    if (!library) {
        logger_.Write("host.modules", "Unable to load module " + module_id + ".");
        rejected_modules_.insert(module_id);
        return LoadResult::Rejected;
    }

    const auto get_api = reinterpret_cast<BE_GetModuleApiV1Fn>(
        GetProcAddress(library, api_name.c_str()));
    const BE_ModuleApiV1* api = get_api ? get_api() : nullptr;
    if (!api || api->descriptor.abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !api->descriptor.module_id || module_id != api->descriptor.module_id ||
        !api->initialize || !api->shutdown) {
        logger_.Write("host.modules", "Module ABI validation failed: " + module_id);
        FreeLibrary(library);
        rejected_modules_.insert(module_id);
        return LoadResult::Rejected;
    }

    const BE_Result result = api->initialize(&runtime_.Api());
    if (result != BE_Result_Ok) {
        logger_.Write("host.modules", "Module contract was rejected: " + module_id);
        FreeLibrary(library);
        rejected_modules_.insert(module_id);
        return LoadResult::Rejected;
    }

    if (api->configuration_changed) {
        const std::string configuration = settings_.CopyModuleSection(module_id);
        const BE_Result config_result = api->configuration_changed(configuration.c_str());
        if (config_result != BE_Result_Ok) {
            logger_.Write("host.modules", "Module configuration was rejected: " +
                module_id);
            api->shutdown();
            runtime_.ReleaseHooks(module_id);
            FreeLibrary(library);
            rejected_modules_.insert(module_id);
            return LoadResult::Rejected;
        }
    }
    modules_.push_back(LoadedModule{library, api, module_id});
    logger_.Write("host.modules", "Module started: " + module_id);
    return LoadResult::Loaded;
}

bool ModuleManager::HasPendingModules() const {
    return pending_module_count_ != 0;
}

size_t ModuleManager::LoadedCount() const {
    return modules_.size();
}

void ModuleManager::ReloadConfigurations() {
    for (const auto& module : modules_) {
        if (!module.api->configuration_changed) {
            continue;
        }
        const std::string configuration =
            settings_.CopyModuleSection(module.module_id);
        const BE_Result result = module.api->configuration_changed(
            configuration.c_str());
        if (result == BE_Result_Ok) {
            logger_.Write("host.modules", "Module configuration reloaded: " +
                module.module_id);
        } else {
            logger_.Write("host.modules", "Module rejected a configuration update: " +
                module.module_id);
        }
    }
}

void ModuleManager::Shutdown() {
    for (auto iterator = modules_.rbegin(); iterator != modules_.rend(); ++iterator) {
        iterator->api->shutdown();
        runtime_.ReleaseHooks(iterator->module_id);
        FreeLibrary(iterator->library);
    }
    modules_.clear();
    rejected_modules_.clear();
    deferred_log_once_.clear();
    pending_module_count_ = 0;
}

} // namespace BetterEndfield::Host
