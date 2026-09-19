#pragma once
#include <filesystem>
#include <span>
#include <string>
#include <string_view>
#include <vector>
#include <cstdint>
#include <memory>

namespace BetterEndfield::CustomModel {
struct ComponentIdentity { const char* name; uint32_t indices; };
struct CharacterAdapter {
    const char* id;
    const char* world_resource;
    const char* ui_resource;
    const char* default_package;
    bool union_texture_masks;
    std::span<const ComponentIdentity> components;
};
struct EnabledMod {
    const CharacterAdapter* adapter = nullptr;
    std::filesystem::path package;
    std::string appearance;
};
struct OwnedCharacterAdapter {
    std::string id, world, ui;
    std::vector<std::string> names;
    std::vector<ComponentIdentity> components;
    CharacterAdapter adapter{};
};
struct ModRegistry {
    bool standalone_lod = false;
    std::vector<EnabledMod> enabled;
    std::vector<std::string> diagnostics;
    std::vector<std::unique_ptr<OwnedCharacterAdapter>> owned_adapters;
    const EnabledMod* Match(std::string_view resource) const;
};
std::span<const CharacterAdapter> CharacterAdapters();
bool ParseModRegistry(std::string_view ini, const std::filesystem::path& package_root,
    ModRegistry& output, std::string& error);
}
