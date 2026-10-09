#pragma once
#include <filesystem>
#include <span>
#include <string>
#include <string_view>
#include <vector>
#include <cstdint>
#include <memory>

namespace BetterEndfieldNext::CustomModel {
// Unity names an instance "<prefab>(Clone)"; the game's model pool may append
// "#<serial>" as well (device 2026-10-04: "chr_0003_endminf_postmodel(Clone)#27").
inline std::string_view ResourceBaseName(std::string_view name) {
    if (const auto hash=name.rfind('#');hash!=std::string_view::npos && hash+1<name.size() &&
        name.find_first_not_of("0123456789",hash+1)==std::string_view::npos) name=name.substr(0,hash);
    if (name.ends_with("(Clone)")) name.remove_suffix(7);
    return name;
}
struct ComponentIdentity {
    const char* name;
    uint32_t indices;
    const char* receiver_path = "";
    bool static_mesh = false;
};
struct CharacterAdapter {
    const char* id;
    const char* world_resource;
    const char* ui_resource;
    const char* default_package;
    bool union_texture_masks;
    std::span<const ComponentIdentity> components;
    const char* resource_id = "";
    const char* asset_path = "";
    uint32_t receiver_lod = 0;
    bool explicit_resource = false;
};
struct EnabledMod {
    const CharacterAdapter* adapter = nullptr;
    std::filesystem::path package;
    std::string appearance;
    bool skip_validation = false;
    bool loading_optimization = false;
    std::string selection_key;
    std::string parameters;
    std::string resource_id;
    std::string package_id; // Conflict/reload identity independent of owner ID.
};
struct OwnedCharacterAdapter {
    std::string id, world, ui;
    std::string resource_id, asset_path;
    std::vector<std::string> names;
    std::vector<std::string> receiver_paths;
    std::vector<ComponentIdentity> components;
    CharacterAdapter adapter{};
};
struct ModRegistry {
    bool standalone_lod = false;
    bool skip_validation = false;
    bool hot_switch = false;
    // Payload move/release parsing; verified byte-identical, always on.
    bool loading_optimization = true;
    // Optional: fewer render-thread syncs while uploading (faster, higher peak).
    bool fast_loading = false;
    std::vector<EnabledMod> enabled;
    std::vector<std::string> diagnostics;
    std::vector<std::shared_ptr<OwnedCharacterAdapter>> owned_adapters;
    const EnabledMod* Match(std::string_view resource) const;
};
std::span<const CharacterAdapter> CharacterAdapters();
bool ParseModRegistry(std::string_view ini, const std::filesystem::path& package_root,
    ModRegistry& output, std::string& error);
}
