#pragma once
// Runtime mesh upload representation. These structs are NOT the BEMv1 wire format.
#include <array>
#include <cstdint>
#include <filesystem>
#include <span>
#include <string>
#include <string_view>
#include <vector>
namespace BetterEndfield::CustomModel {
constexpr uint32_t kMaxBemComponents = 64;
// BEM 1.2 limits; 1.0/1.1 packages keep their own tighter limits in bem.cpp.
constexpr uint32_t kMaxBemTextures = 64;
constexpr uint32_t kMaxBemTextureBytes = 256u * 1024u * 1024u;
constexpr uint32_t kBemFlagIndex16 = 1u << 2;
constexpr uint32_t kBemFlagRawStream0 = 1u << 5;
constexpr uint32_t kBemFlagRawStream1 = 1u << 6;
constexpr uint32_t kBemFlagRawStream2 = 1u << 7;
constexpr uint32_t kBemFlagTextures = 1u << 8;
#pragma pack(push, 1)
struct BemFileHeader {
    char magic[8];
    uint32_t version;
    uint32_t component_count;
    uint32_t texture_count;
    uint32_t flags;
    uint32_t reserved;
};
#pragma pack(pop)
static_assert(sizeof(BemFileHeader) == 28);

#pragma pack(push, 1)
struct BemComponentHeaderRaw {
    uint32_t component_id;
    // Source index count is an additional compatibility check, not resource identity.
    uint32_t original_index_count;
    uint32_t vertex_count;
    uint32_t index_count;
    uint32_t max_bone;
    uint32_t stream_count;
    uint32_t stride0;
    uint32_t stride1;
    uint32_t stride2;
    uint32_t index_element_size; // v24: 2; v25: 2 or 4 (explicit UInt32 extension)
    uint32_t reserved0;
    uint32_t flags;
    uint32_t reserved1;
};
#pragma pack(pop)
static_assert(sizeof(BemComponentHeaderRaw) == 52);

// A component the mod does not replace is still carried, so the runtime can
// identify its renderer while working out which components bind each texture.
constexpr uint32_t kComponentFlagHidden = 1u << 0;
constexpr uint32_t kComponentFlagNoGeometry = 1u << 1;

#pragma pack(push, 1)
struct BemTextureEntryRaw {
    uint32_t width;
    uint32_t height;
    uint32_t mip_count;
    uint32_t data_size;
    // How to rebuild the replacement surface: the DDS payload's own format.
    int32_t create_format;
    int32_t create_srgb;
    // How to find the slot it belongs to: the format of the original texture
    // EFMI's content hash identifies, which is what the live material still
    // holds. These two differ whenever the mod ships a re-encoded replacement.
    int32_t slot_format;
    int32_t slot_srgb;
    // Which components' draws sampled the original texture. This is the actual
    // slot key: the same set can be computed from the live materials, so a
    // texture EFMI saw in exactly components {1,4} is the one texture both the
    // face and the eyebrow material bind.
    uint32_t component_mask;
    // Internal compatibility fields used by upload helpers, never serialized by BEMv1.
    int32_t explicit_slot;
    uint32_t name_length;
    uint32_t reserved;
};
#pragma pack(pop)
static_assert(sizeof(BemTextureEntryRaw) == 48);

struct BemTexture {
    BemTextureEntryRaw info{};
    std::string name;
    std::vector<uint8_t> data;  // complete block-compressed mip chain
    std::string original_name;
};

// BEMv1 resources lowered into the established native upload representation.
struct BemComponent {
    BemComponentHeaderRaw info{};
    std::array<std::vector<uint8_t>, 3> streams;
    std::vector<uint8_t> indices;
    uint32_t layout_crc=0; // v25 exact native VertexAttributeDescriptor sequence
    // v25: palette indices in the packed skin stream address this table.
    struct BoneSource { uint32_t component, index, name_crc; };
    // `textures` is a bit mask over BemPocData::textures (at most kMaxBemTextures).
    struct Draw { uint32_t start, count, material_component, material_slot, material_crc, reserved; uint64_t textures; };
    struct KeepMaterialOverride { uint32_t material_slot, material_crc; uint64_t textures; };
    std::vector<BoneSource> bones;
    std::vector<Draw> draws;
    std::vector<KeepMaterialOverride> keep_material_overrides;
    std::vector<std::string> keep_material_names;
    std::vector<std::array<int32_t,4>> attributes;
    std::vector<std::string> bone_names, material_names;
    // BEM 1.2: other names a resource's renderer may carry for palette bone i
    // (a bone the game names differently in its world and UI skeletons).
    std::vector<std::vector<std::string>> bone_aliases;
    bool BoneNameMatches(size_t i, std::string_view name) const {
        if (bone_names[i] == name) return true;
        if (i < bone_aliases.size())
            for (const auto& alias : bone_aliases[i]) if (alias == name) return true;
        return false;
    }
};
static_assert(sizeof(BemComponent::BoneSource) == 12);
static_assert(sizeof(BemComponent::Draw) == 32);
static_assert(sizeof(BemComponent::KeepMaterialOverride) == 16);

struct BemPocData {
    BemFileHeader header{};
    std::vector<BemComponent> components;
    std::vector<BemTexture> textures;
};

struct BemPackageInfo {
    uint16_t minor = 0;
    std::string package_id, name, author, version, character_id;
    std::string world_resource, ui_resource, default_appearance;
    std::string default_options, option_groups_json, selection_constraints_json;
    std::vector<std::string> appearances, component_names;
    std::vector<uint32_t> original_counts;
};
struct BemLoadStats { std::vector<uint32_t> payload_ids; };
bool ReadBemPackageInfo(const std::filesystem::path&, BemPackageInfo&, std::string& error);

constexpr uint32_t kBemStreamCount = 3;
constexpr int32_t kIndexElementSize = 2;


bool ParseBem(std::span<const uint8_t> bytes, BemPocData& output, std::string& error);
bool LoadBem(const std::filesystem::path& path, BemPocData& output, std::string& error,
    std::string_view appearance = {}, BemLoadStats* stats = nullptr);
} // namespace BetterEndfield::CustomModel
