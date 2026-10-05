#include "mod_registry.h"
#include "bem.h"
#include <map>
#include <algorithm>
#include <set>
#include <mutex>

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
std::string FileTimeIdentity(std::filesystem::file_time_type time) {
    // libc++ uses a 128-bit file-clock representation on Android. Preserve the
    // complete tick count without an ambiguous to_string or narrowing cast.
    auto ticks=time.time_since_epoch().count();
    if (!ticks) return "0";
    const bool negative=ticks<0; std::string digits;
    while (ticks) {
        const int digit=static_cast<int>(ticks%10);
        digits.push_back(static_cast<char>('0'+(digit<0?-digit:digit))); ticks/=10;
    }
    if (negative) digits.push_back('-');
    std::reverse(digits.begin(),digits.end()); return digits;
}
bool CachedPackageInfo(const std::filesystem::path& path,BemPackageInfo& info,std::string& error,bool unchecked) {
    struct Cached {
        std::filesystem::file_time_type mtime;
        uintmax_t size;
        bool unchecked;
        BemPackageInfo info;
    };
    static std::mutex mutex;
    static std::map<std::filesystem::path,Cached> cache;
    const auto key=path.lexically_normal();std::error_code te,se;
    const auto mtime=std::filesystem::last_write_time(key,te);const auto size=std::filesystem::file_size(key,se);
    if(te||se) {error="Package file unavailable";return false;}
    {
        std::lock_guard lock(mutex);const auto entry=cache.find(key);
        if(entry!=cache.end()&&entry->second.mtime==mtime&&entry->second.size==size&&entry->second.unchecked==unchecked) {
            info=entry->second.info;return true;
        }
    }
    if(!ReadBemPackageInfo(key,info,error,unchecked)) return false;
    const auto after=std::filesystem::last_write_time(key,te);const auto after_size=std::filesystem::file_size(key,se);
    if(te||se||after!=mtime||after_size!=size) {error="Package changed while reading metadata";return false;}
    std::lock_guard lock(mutex);
    if(cache.size()>=128) cache.clear();
    cache.insert_or_assign(key,Cached{mtime,size,unchecked,info});return true;
}
}
std::span<const CharacterAdapter> CharacterAdapters() { return {}; }
const EnabledMod* ModRegistry::Match(std::string_view resource) const {
    resource=ResourceBaseName(resource);
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
        if(auto i=sections["CustomModel"].find("skip_validation");i!=sections["CustomModel"].end()&&!Boolean(i->second,parsed.skip_validation)) {
            error="skip_validation must be boolean";return false;
        }
        if(auto i=sections["CustomModel"].find("hot_switch");i!=sections["CustomModel"].end()&&!Boolean(i->second,parsed.hot_switch)) {
            error="hot_switch must be boolean";return false;
        }
        // The former experimental loading_optimization key is now always on and ignored.
        if(auto i=sections["CustomModel"].find("fast_loading");i!=sections["CustomModel"].end()&&!Boolean(i->second,parsed.fast_loading)) {
            error="fast_loading must be boolean";return false;
        }
        if(parsed.skip_validation) parsed.diagnostics.push_back("Developer mode: model validation disabled; crashes and incorrect rendering are possible.");
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
            if(!CachedPackageInfo(path,info,why,parsed.skip_validation)) {parsed.diagnostics.push_back("Package refused: "+name+": "+why);continue;}
            auto appearance=info.minor?get("options"):get("appearance");
            if(appearance.empty()) appearance=info.minor?info.default_options:info.default_appearance;
            if(!info.minor && std::find(info.appearances.begin(),info.appearances.end(),appearance)==info.appearances.end()) {
                parsed.diagnostics.push_back("Appearance removed; using package default: "+name);appearance=info.default_appearance;
            }
            std::string parameters;
            if(!ResolveBemParameters(info,get("parameters"),parameters,why)) {
                parsed.diagnostics.push_back("Parameters removed or invalid; using package defaults: "+name+": "+why);
                parameters=info.default_parameters;
            }
            if(!roles.insert(info.character_id).second || resources.contains(info.world_resource)||resources.contains(info.ui_resource)) {
                conflicts.insert(info.character_id); parsed.diagnostics.push_back("Conflicting enabled package: "+info.character_id); continue;
            }
            resources.insert(info.world_resource); resources.insert(info.ui_resource);
            auto own=std::make_shared<OwnedCharacterAdapter>(); own->id=info.character_id;
            own->world=info.world_resource; own->ui=info.ui_resource; own->names=info.component_names;
            for(size_t i=0;i<own->names.size();++i) own->components.push_back({own->names[i].c_str(),info.original_counts[i]});
            own->adapter={own->id.c_str(),own->world.c_str(),own->ui.c_str(),"",false,own->components};
            std::error_code file_error;
            const auto stamp=std::filesystem::last_write_time(path,file_error);
            const auto bytes=std::filesystem::file_size(path,file_error);
            const auto canonical=path.lexically_normal();
            const std::string key=canonical.string()+"\n"+appearance+"\n"+parameters+"\n"+
                (parsed.skip_validation?"unchecked":"checked")+"\n"+
                (parsed.loading_optimization?"optimized":"normal")+"\n"+
                FileTimeIdentity(stamp)+":"+std::to_string(bytes);
            parsed.enabled.push_back({&own->adapter,canonical,appearance,parsed.skip_validation,parsed.loading_optimization,key,parameters});
            parsed.owned_adapters.push_back(std::move(own));
        }
        std::erase_if(parsed.enabled,[&](const auto& m){return conflicts.contains(m.adapter->id);});
        output=std::move(parsed);return true;
    } catch(const std::exception& e) {error=e.what();return false;}
}
}
