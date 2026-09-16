#include "bem.h"
#include <algorithm>
#include <cstring>
#include <fstream>
#include <limits>
// Parsing and BCn size validation migrated from module_poc2_part_01.inc.
// New checks: unique IDs, component references, skin stride and triangle bounds.
namespace BetterEndfield::CustomModel {
namespace {
struct Reader {
    std::string& error;
    void Log(const std::string& message) { error = message; }
bool CheckedAdvance(size_t& cursor, size_t amount, size_t limit) {
    if (amount > limit || cursor > limit - amount) return false;
    cursor += amount;
    return true;
}

// Block-compressed mip chain size, used to prove the payload's declared
// dimensions agree with the number of bytes actually carried. Nothing here
// decodes or reinterprets the surface; it is uploaded exactly as stored.
size_t BlockCompressedChainSize(
    uint32_t width, uint32_t height, uint32_t mip_count, uint32_t block_bytes) {
    size_t total = 0;
    for (uint32_t level = 0; level < mip_count; ++level) {
        const uint32_t level_width = std::max(width >> level, 1u);
        const uint32_t level_height = std::max(height >> level, 1u);
        total += static_cast<size_t>((level_width + 3) / 4) *
            static_cast<size_t>((level_height + 3) / 4) * block_bytes;
    }
    return total;
}

// Bytes per 4x4 block for the UnityEngine.TextureFormat values the converter is
// allowed to emit. An unlisted format is refused rather than guessed at.
bool BlockBytesForTextureFormat(int32_t format, uint32_t& block_bytes) {
    switch (format) {
    case 10:  // DXT1
    case 26:  // BC4
        block_bytes = 8;
        return true;
    case 12:  // DXT5
    case 25:  // BC7
    case 27:  // BC5
        block_bytes = 16;
        return true;
    default:
        return false;
    }
}

bool LoadBemTextures(
    std::span<const uint8_t> bytes, size_t& cursor, BemPocData& output) {
    const uint32_t texture_count = output.header.texture_count;
    if (texture_count == 0) {
        return true;
    }
    if (texture_count > kMaxBemTextures) {
        Log("BEM PoC-2.4 declares an implausible texture count: " +
            std::to_string(texture_count));
        return false;
    }

    output.textures.reserve(texture_count);
    for (uint32_t index = 0; index < texture_count; ++index) {
        const std::string label =
            "BEM PoC-2.4 texture[" + std::to_string(index) + "]";
        BemTexture texture;
        if (bytes.size() - cursor < sizeof(BemTextureEntryRaw)) {
            Log(label + " header is truncated.");
            return false;
        }
        std::memcpy(
            &texture.info, bytes.data() + cursor, sizeof(BemTextureEntryRaw));
        if (!CheckedAdvance(cursor, sizeof(BemTextureEntryRaw), bytes.size())) {
            return false;
        }

        const BemTextureEntryRaw& info = texture.info;
        uint32_t block_bytes = 0;
        if (!BlockBytesForTextureFormat(info.create_format, block_bytes)) {
            Log(label + " uses unsupported TextureFormat " +
                std::to_string(info.create_format));
            return false;
        }
        if (info.width == 0 || info.height == 0 || info.width % 4 != 0 ||
            info.height % 4 != 0 || info.width > 32768 || info.height > 32768 || info.mip_count == 0 ||
            info.mip_count > 16 || info.name_length > 256 ||
            info.component_mask == 0 || info.data_size == 0 ||
            info.data_size > kMaxBemTextureBytes) {
            Log(label + " has implausible dimensions, sizes or component set.");
            return false;
        }
        const size_t expected = BlockCompressedChainSize(
            info.width, info.height, info.mip_count, block_bytes);
        if (expected != info.data_size) {
            Log(label + " declares " + std::to_string(info.data_size) +
                " bytes but " + std::to_string(info.width) + "x" +
                std::to_string(info.height) + " with " +
                std::to_string(info.mip_count) + " mip level(s) needs " +
                std::to_string(expected));
            return false;
        }
        if (bytes.size() - cursor < info.name_length ||
            bytes.size() - cursor - info.name_length < info.data_size) {
            Log(label + " payload is truncated.");
            return false;
        }

        texture.name.assign(
            reinterpret_cast<const char*>(bytes.data() + cursor),
            info.name_length);
        if (!CheckedAdvance(cursor, info.name_length, bytes.size())) {
            return false;
        }
        texture.data.resize(info.data_size);
        std::memcpy(
            texture.data.data(), bytes.data() + cursor, info.data_size);
        if (!CheckedAdvance(cursor, info.data_size, bytes.size())) {
            return false;
        }
        output.textures.push_back(std::move(texture));
    }
    return true;
}

bool LoadBemComponent(
    std::span<const uint8_t> bytes, size_t& cursor, BemComponent& output) {
    if (bytes.size() - cursor < sizeof(BemComponentHeaderRaw)) {
        Log("BEM PoC-2.4 component header is truncated.");
        return false;
    }
    std::memcpy(
        &output.info, bytes.data() + cursor, sizeof(BemComponentHeaderRaw));
    if (!CheckedAdvance(
            cursor, sizeof(BemComponentHeaderRaw), bytes.size())) {
        return false;
    }

    const BemComponentHeaderRaw& info = output.info;
    const std::string label =
        "BEM PoC-2.4 C" + std::to_string(info.component_id);
    if (info.component_id >= kMaxBemComponents || info.original_index_count == 0 ||
        info.original_index_count % 3 != 0) {
        Log(label + " declares an implausible original index count.");
        return false;
    }
    if ((info.flags & kComponentFlagNoGeometry) != 0) {
        return info.vertex_count == 0 && info.index_count == 0;
    }

    if (info.stream_count != kBemStreamCount ||
        info.index_element_size != kIndexElementSize ||
        info.vertex_count == 0 || info.vertex_count > (1u << 20) ||
        info.index_count == 0 || info.index_count % 3 != 0 ||
        info.stride0 == 0 || info.stride1 == 0 || info.stride2 == 0 ||
        info.stride0 > 64 || info.stride1 > 64 ||
        (info.stride2 != 4 && info.stride2 != 12)) {
        Log(label + " has an implausible geometry description.");
        return false;
    }

    const std::array<uint32_t, 3> strides{
        info.stride0, info.stride1, info.stride2};
    for (uint32_t stream = 0; stream < kBemStreamCount; ++stream) {
        const size_t size =
            static_cast<size_t>(info.vertex_count) * strides[stream];
        if (bytes.size() - cursor < size) {
            Log(label + " stream " + std::to_string(stream) +
                " is truncated.");
            return false;
        }
        output.streams[stream].resize(size);
        std::memcpy(
            output.streams[stream].data(), bytes.data() + cursor, size);
        if (!CheckedAdvance(cursor, size, bytes.size())) return false;
    }

    const size_t index_bytes =
        static_cast<size_t>(info.index_count) * kIndexElementSize;
    if (bytes.size() - cursor < index_bytes) {
        Log(label + " index buffer is truncated.");
        return false;
    }
    output.indices.resize(index_bytes);
    std::memcpy(output.indices.data(), bytes.data() + cursor, index_bytes);
    if (!CheckedAdvance(cursor, index_bytes, bytes.size())) return false;

    const uint16_t* indices =
        reinterpret_cast<const uint16_t*>(output.indices.data());
    for (uint32_t i = 0; i < info.index_count; ++i) {
        if (indices[i] >= info.vertex_count) {
            Log(label + " index " + std::to_string(indices[i]) +
                " is outside vertexCount " +
                std::to_string(info.vertex_count));
            return false;
        }
    }

    // Bone indices are the last four bytes of every stream 2 element in every
    // declaration this format allows, with or without preceding weights.
    for (size_t i = 0; i < output.indices.size(); i += 2) {
        uint16_t index = 0;
        std::memcpy(&index, output.indices.data() + i, sizeof(index));
        if (index >= info.vertex_count) { Log(label + " triangle index out of range."); return false; }
    }
    uint32_t max_bone = 0;
    for (uint32_t i = 0; i < info.vertex_count; ++i) {
        const uint8_t* bones = output.streams[2].data() +
            static_cast<size_t>(i) * info.stride2 + (info.stride2 - 4);
        for (int k = 0; k < 4; ++k) {
            max_bone = std::max(max_bone, static_cast<uint32_t>(bones[k]));
        }
    }
    if (max_bone != info.max_bone) {
        Log(label + " stream 2 max bone " + std::to_string(max_bone) +
            " disagrees with the header's " + std::to_string(info.max_bone));
        return false;
    }
    return true;
}

bool Parse(std::span<const uint8_t> bytes, BemPocData& output) {
    if (bytes.size() < sizeof(BemFileHeader) || bytes.size() > 512u * 1024u * 1024u) {
        Log("Invalid BEM size."); return false;
    }
    std::memcpy(&output.header, bytes.data(), sizeof(output.header));
    const std::array<char, 8> expected_magic{
        'B','E','M','P','C','2','4','\0'};
    if (std::memcmp(output.header.magic, expected_magic.data(),
            expected_magic.size()) != 0 ||
        output.header.version != 24) {
        Log("BEM payload is not a PoC-2.4 file; regenerate it with "
            "tools/CustomModel/convert_efmi_poc.py.");
        return false;
    }
    if (output.header.component_count == 0 ||
        output.header.component_count > kMaxBemComponents) {
        Log("BEM payload declares an implausible component count: " +
            std::to_string(output.header.component_count));
        return false;
    }

    size_t cursor = sizeof(BemFileHeader);
    output.components.resize(output.header.component_count);
    for (BemComponent& component : output.components) {
        if (!LoadBemComponent(bytes, cursor, component)) return false;
    }
    if (!LoadBemTextures(bytes, cursor, output)) return false;
    if (cursor != bytes.size()) {
        Log("BEM payload has " + std::to_string(bytes.size() - cursor) +
            " trailing bytes.");
        return false;
    }

    uint64_t ids = 0;
    for (const auto& component : output.components) {
        const uint64_t bit = uint64_t{1} << component.info.component_id;
        if (ids & bit) { Log("Duplicate BEM component id."); return false; }
        ids |= bit;
    }
    for (const auto& texture : output.textures) {
        if ((texture.info.component_mask & ids) != texture.info.component_mask ||
            texture.info.reserved > 2) {
            Log("Texture references missing components or an unknown pin type."); return false;
        }
    }
    return true;
}
};
}
bool ParseBem(std::span<const uint8_t> bytes, BemPocData& output, std::string& error) {
    output = {}; error.clear();
    BemPocData parsed;
    try {
        if (!Reader{error}.Parse(bytes, parsed)) {
            if (error.empty()) error = "Invalid BEM payload.";
            return false;
        }
        output = std::move(parsed);
        return true;
    } catch (const std::exception& e) { error = e.what(); return false; }
}
bool LoadBem(const std::filesystem::path& path, BemPocData& output, std::string& error) {
    output = {}; error.clear();
    try {
        std::ifstream input(path, std::ios::binary | std::ios::ate);
        if (!input) { error = "BEM package cannot be opened."; return false; }
        const auto size = input.tellg();
        if (size < static_cast<std::streamoff>(sizeof(BemFileHeader)) || size > 512 * 1024 * 1024) {
            error = "Invalid BEM file size."; return false;
        }
        std::vector<uint8_t> bytes(static_cast<size_t>(size));
        input.seekg(0);
        if (!input.read(reinterpret_cast<char*>(bytes.data()), static_cast<std::streamsize>(bytes.size()))) {
            error = "BEM read failed."; return false;
        }
        return ParseBem(bytes, output, error);
    } catch (const std::exception& e) { error = e.what(); return false; }
}
} // namespace BetterEndfield::CustomModel
