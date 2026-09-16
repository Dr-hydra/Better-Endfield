#pragma once
#include <cstdint>
#include <span>
#include <string>

namespace BetterEndfield::CustomModel {
struct NativeMeshLayout {
    uint32_t bones_per_vertex_offset = 0;
    uint32_t agreeing_serializers = 0;
};

// Input is a mapped PE image (RVA == span offset), never a raw file.
// Resolves the field from named serialization operations; no client RVA or
// native Mesh field displacement is embedded in the implementation.
bool ResolveNativeMeshLayout(std::span<const uint8_t> image,
    NativeMeshLayout& layout, std::string& error);
}
