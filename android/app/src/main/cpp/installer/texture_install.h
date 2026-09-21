#pragma once
#include "bem_rewrite.h"
namespace betterendfield {
void CancelTextureCompression();
void ConvertInstalledTexture(const BetterEndfield::CustomModel::BemJson& manifest,
    BetterEndfield::CustomModel::BemJson& texture, std::vector<uint8_t>& bytes,
    const BetterEndfield::CustomModel::BemJson& rules, bool astc,
    const std::function<void()>& checkpoint,
    const std::function<void(unsigned, unsigned, float)>& progress = {});
}
