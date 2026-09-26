#include "mod_registry.h"
#include "bem.h"
#include <map>
#include <algorithm>
#include <set>

namespace BetterEndfield::CustomModel {
namespace {
std::string Trim(std::string_view s) {
    auto a=s.find_first_not_of(" \t\r\n"); if(a==s.npos) return {};
    return std::string(s.substr(a,s.find_last_not_of(" \t\r\n")-a+1));
}
bool Boolean(std::string_view s,bool& out) {
    if(s=="true"||s=="1") {out=true;return true;}
    if(s=="false"||s=="0") {out=false;return true;}
    return false;
}
}
std::span<const CharacterAdapter> CharacterAdapters() { return {}; }
const EnabledMod* ModRegistry::Match(std::string_view resource) const {
    if(resource.ends_with("(Clone)")) resource.remove_suffix(7);
    for(const auto& mod:enabled) if(resource==mod.adapter->world_resource||resource==mod.adapter->ui_resource) return &mod;
    return nullptr;
}
bool ParseModRegistry(std::string_view ini,const std::filesystem::path& root,ModRegistry& output,std::string& error) {
    output={}; error.clear();
    try {
        if(ini.size()>1024*1024) {error="Runtime configuration exceeds 1 MiB";return false;}
        if(ini.starts_with("\xef\xbb\xbf")) ini.remove_prefix(3);
        std::map<std::string,std::map<std::string,std::string>> sections; std::string section;
        while(!ini.empty()) {
            auto e=ini.find('\n'); auto line=Trim(ini.substr(0,e)); ini=e==ini.npos?std::string_view{}:ini.substr(e+1);
            if(line.empty()||line[0]=='#'||line[0]==';') continue;
            if(line.front()=='['&&line.back()==']') {section=line.substr(1,line.size()-2);continue;}
            auto equal=line.find('=');
            if(section.empty()||equal==line.npos||!sections[section].emplace(Trim(line.substr(0,equal)),Trim(line.substr(equal+1))).second) {
                error="Malformed or duplicate runtime configuration";return false;
            }
        }
        ModRegistry parsed;
        if(auto i=sections["CustomModel"].find("standalone_lod");i!=sections["CustomModel"].end()&&!Boolean(i->second,parsed.standalone_lod)) {
            error="standalone_lod must be boolean";return false;
        }
        std::set<std::string> roles,resources,conflicts;
        for(const auto& [name,values]:sections) {
            if(!name.starts_with("Mod.")) continue;
            auto get=[&](const char* key) {auto i=values.find(key);return i==values.end()?std::string{}:i->second;};
            bool enabled=false;
            if(!get("enabled").empty()&&!Boolean(get("enabled"),enabled)) {error="Invalid enabled flag";return false;}
            if(!enabled) continue;
            auto file=get("package");
            auto path=std::filesystem::path(std::u8string_view(reinterpret_cast<const char8_t*>(file.data()),file.size()));
            if(file.empty()||path.extension()!=".bem") {parsed.diagnostics.push_back("Refused non-BEMv1 package: "+name);continue;}
            if(path.is_relative()) path=root/path;
            BemPackageInfo info; std::string why;
            if(!ReadBemPackageInfo(path,info,why)) {parsed.diagnostics.push_back("Package refused: "+name+": "+why);continue;}
            auto appearance=info.minor?get("options"):get("appearance");
            if(appearance.empty()) appearance=info.minor?info.default_options:info.default_appearance;
            if(!info.minor && std::find(info.appearances.begin(),info.appearances.end(),appearance)==info.appearances.end()) {
                parsed.diagnostics.push_back("Appearance removed; using package default: "+name);appearance=info.default_appearance;
            }
            if(!roles.insert(info.character_id).second || resources.contains(info.world_resource)||resources.contains(info.ui_resource)) {
                conflicts.insert(info.character_id); parsed.diagnostics.push_back("Conflicting enabled package: "+info.character_id); continue;
            }
            resources.insert(info.world_resource); resources.insert(info.ui_resource);
            auto own=std::make_unique<OwnedCharacterAdapter>(); own->id=info.character_id;
            own->world=info.world_resource; own->ui=info.ui_resource; own->names=info.component_names;
            for(size_t i=0;i<own->names.size();++i) own->components.push_back({own->names[i].c_str(),info.original_counts[i]});
            own->adapter={own->id.c_str(),own->world.c_str(),own->ui.c_str(),"",false,own->components};
            parsed.enabled.push_back({&own->adapter,path.lexically_normal(),appearance}); parsed.owned_adapters.push_back(std::move(own));
        }
        std::erase_if(parsed.enabled,[&](const auto& m){return conflicts.contains(m.adapter->id);});
        output=std::move(parsed);return true;
    } catch(const std::exception& e) {error=e.what();return false;}
}
}
