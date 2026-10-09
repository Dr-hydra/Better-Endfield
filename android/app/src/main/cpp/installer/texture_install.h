#pragma once
#include "bem_rewrite.h"
namespace betterendfieldnext {
void CancelTextureCompression();
void ConvertInstalledTexture(const BetterEndfieldNext::CustomModel::BemJson& manifest,
    BetterEndfieldNext::CustomModel::BemJson& texture, std::vector<uint8_t>& bytes,
    const BetterEndfieldNext::CustomModel::BemJson& rules, bool astc,
    const std::function<void()>& checkpoint,
    const std::function<void(unsigned, unsigned, float)>& progress = {});
}
