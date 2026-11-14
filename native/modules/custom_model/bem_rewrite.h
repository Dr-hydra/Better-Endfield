#pragma once
#include "bem.h"
#include "../../shared/third_party/nlohmann/json.hpp"
#include <functional>
namespace BetterEndfieldNext::CustomModel {
using BemJson = nlohmann::json;
// Installer only: one texture at a time, with opaque geometry copied unchanged.
using TextureTransform = std::function<void(const BemJson&, BemJson&, std::vector<uint8_t>&)>;
bool RewriteBemTextures(const std::filesystem::path& input, const std::filesystem::path& output,
    const TextureTransform& transform, const std::function<void()>& checkpoint,
    std::string& report, std::string& error);
// Installer only: insert one root-level manifest field by editing the manifest
// bytes, so every payload block is copied verbatim instead of re-encoded. Returns
// false with `error` set when the field is already present or the input is not a
// plain binary package.
bool InsertBemManifestField(const std::filesystem::path& input, const std::filesystem::path& output,
    std::string_view name, std::string_view value, std::string& error);
}
