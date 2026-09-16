#include "mod_registry.h"
#include <algorithm>
#include <map>
#include <set>

namespace BetterEndfield::CustomModel {
namespace {
// Evidence: CUSTOM_MODEL_RUNTIME_VALIDATION_ENDMIN_20260910.md and the
// validated Pelica profile. Counts supplement names; they never route a role.
constexpr ComponentIdentity endmin[]{
    {"S_actor_endminf_hair_01_lod0",27615}, {"S_actor_endminf_face_01_lod0",9000},
    {"S_actor_endminf_cloth_03_lod0",4524}, {"S_actor_endminf_cloth_04_lod0",20577},
    {"S_actor_endminf_eyebrow_01_lod0",1638}, {"S_actor_endminf_body_01_lod0",16524},
    {"S_actor_endminf_cloth_05_lod0",117}, {"S_actor_endminf_iris_01_lod0",1386},
    {"S_actor_endminf_eyeshadow_01_lod0",90}, {"S_actor_endminf_cloth_01_lod0",101994},
    {"S_actor_endminf_cloth_02_lod0",2286}
};
constexpr ComponentIdentity pelica[]{
    {"S_actor_pelica_hair_01_lod0",45003}, {"S_actor_pelica_face_01_lod0",9186},
    {"S_actor_pelica_body_01_lod0",5922}, {"S_actor_pelica_hairshadow_01_lod0",525},
    {"S_actor_pelica_cloth_01_lod0",78186}, {"S_actor_pelica_iris_01_lod0",480},
    {"S_actor_pelica_eyeshadow_01_lod0",96}, {"S_actor_pelica_cloth_02_lod0",70656},
    {"S_actor_pelica_cloth_04_lod0",8460}, {"S_actor_pelica_cloth_03_lod0",2178}
};
const CharacterAdapter adapters[]{
    {"endminf","chr_0003_endminf_postmodel","chr_0003_endminf_uimodel",
        "endmin-casualwear-c9.bempoc",true,endmin},
    {"pelica","chr_0004_pelica_postmodel","chr_0004_pelica_uimodel",
        "pelica-lod0-native-materials.bempoc",false,pelica}
};
std::string Trim(std::string_view text) {
    auto first=text.find_first_not_of(" \t\r\n");
    if (first==text.npos) return {};
    return std::string(text.substr(first,text.find_last_not_of(" \t\r\n")-first+1));
}
bool Boolean(std::string_view value, bool& result) {
    if (value=="true" || value=="1" || value=="yes") { result=true; return true; }
    if (value=="false" || value=="0" || value=="no") { result=false; return true; }
    return false;
}
}
std::span<const CharacterAdapter> CharacterAdapters() { return adapters; }
const EnabledMod* ModRegistry::Match(std::string_view resource) const {
    constexpr std::string_view clone="(Clone)";
    if (resource.ends_with(clone)) resource.remove_suffix(clone.size());
    for (const auto& mod : enabled) {
        if (resource==mod.adapter->world_resource || resource==mod.adapter->ui_resource) return &mod;
    }
    return nullptr;
}
bool ParseModRegistry(std::string_view ini, const std::filesystem::path& package_root,
    ModRegistry& output, std::string& error) {
    output={}; error.clear();
    if (ini.size()>65536) { error="Runtime configuration exceeds 64 KiB."; return false; }
    if (ini.starts_with("\xef\xbb\xbf")) ini.remove_prefix(3);
    std::map<std::string,std::map<std::string,std::string>> sections;
    std::string section;
    while (!ini.empty()) {
        const auto split=ini.find('\n'); const std::string line=Trim(ini.substr(0,split));
        if (split==ini.npos) ini={}; else ini.remove_prefix(split+1);
        if (line.empty() || line[0]==';' || line[0]=='#') continue;
        if (line.front()=='[' && line.back()==']') { section=Trim(std::string_view(line).substr(1,line.size()-2)); continue; }
        const auto equal=line.find('=');
        if (section.empty() || equal==line.npos) { error="Malformed runtime INI."; return false; }
        auto key=Trim(std::string_view(line).substr(0,equal));
        if (key.empty() || !sections[section].emplace(key,Trim(std::string_view(line).substr(equal+1))).second) {
            error="Duplicate or empty configuration key: "+section+"."+key; return false;
        }
    }
    ModRegistry parsed;
    const auto& common=sections["CustomModel"];
    if (auto it=common.find("standalone_lod");it!=common.end() && !Boolean(it->second,parsed.standalone_lod)) {
        error="standalone_lod must be boolean."; return false;
    }
    std::map<std::string,std::vector<std::string>> selected;
    bool explicit_mods=false;
    for (const auto& [name,values] : sections) {
        if (!name.starts_with("Mod.")) continue;
        explicit_mods=true;
        bool enabled=false;
        if (auto it=values.find("enabled");it!=values.end() && !Boolean(it->second,enabled)) {
            error="Invalid enabled value: "+name; return false;
        }
        if (!enabled) continue;
        const auto character=values.find("character"),package=values.find("package");
        if (character==values.end() || package==values.end() || package->second.empty()) {
            error="Enabled Mod needs character and package: "+name; return false;
        }
        selected[character->second].push_back(package->second);
    }
    if (!explicit_mods) {
        // Compatibility with the existing validation configuration; absent
        // configuration enables no Mod, so standalone LOD needs no payload.
        const auto it=common.find("target");
        const std::string target=it==common.end()?"none":it->second;
        if (target!="none" && target!="both" && target!="endminf" && target!="pelica") {
            error="Unknown legacy target."; return false;
        }
        for (const auto& adapter : adapters) {
            if (target=="both" || target==adapter.id) selected[adapter.id].push_back(adapter.default_package);
        }
    }
    for (const auto& [character,packages] : selected) {
        const auto found=std::find_if(std::begin(adapters),std::end(adapters),
            [&](const auto& adapter){return character==adapter.id;});
        if (found==std::end(adapters) || packages.size()!=1) {
            parsed.diagnostics.push_back("Role disabled: unknown adapter or conflicting enabled packages: "+character);
            continue;
        }
        const auto& utf8=packages.front();
        auto path=std::filesystem::path(std::u8string_view(reinterpret_cast<const char8_t*>(utf8.data()),utf8.size()));
        if (path.is_relative()) path=package_root/path;
        parsed.enabled.push_back({found,path.lexically_normal()});
    }
    output=std::move(parsed); return true;
}
}
