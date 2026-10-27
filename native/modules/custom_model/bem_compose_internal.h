#pragma once
#include "bem_compose.h"
namespace BetterEndfieldNext::CustomModel {
bool PlanBemCompositionPart(const nlohmann::json&, nlohmann::json&, std::string&);
bool BuildBemCompositionPart(const nlohmann::json&, const nlohmann::json&, const std::filesystem::path&,
    nlohmann::json&, const std::function<void()>&, std::string&, bool previewOnly);
}
