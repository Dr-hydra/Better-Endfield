#pragma once
#include "bem.h"
#include "../../shared/third_party/nlohmann/json.hpp"
#include <functional>
namespace BetterEndfieldNext::CustomModel {
struct BemPreviewGpu {
    bool bc7 = false;
    uint32_t max_texture_size = 0;
    bool astc = false;
    std::filesystem::path native_assets; // Host-owned resources, never package-controlled.
    std::filesystem::path texture_cache; // Private, host-owned preview-session cache.
    bool composition_draft = false;
    bool desktop = false;
};
// Read-only export for manager WebViews; output must be a new session directory.
bool ExportBemPreview(const std::filesystem::path& package, const std::filesystem::path& output,
    std::string_view selection, std::string_view parameters, std::string_view resource,
    const std::function<void()>& checkpoint, nlohmann::json& report, std::string& error, const BemPreviewGpu& gpu = {});
}
