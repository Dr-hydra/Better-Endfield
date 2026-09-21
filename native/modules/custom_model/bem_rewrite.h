#pragma once
#include "bem.h"
#include "../../shared/third_party/nlohmann/json.hpp"
#include <functional>
namespace BetterEndfield::CustomModel {
using BemJson = nlohmann::json;
// Installer only: one texture at a time, with opaque geometry copied unchanged.
using TextureTransform = std::function<void(const BemJson&, BemJson&, std::vector<uint8_t>&)>;
bool RewriteBemTextures(const std::filesystem::path& input, const std::filesystem::path& output,
    const TextureTransform& transform, const std::function<void()>& checkpoint,
    std::string& report, std::string& error);
}
