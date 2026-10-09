#pragma once
#include "../bem.h"
#include "../runtime_ini.h"
#include "../../../shared/third_party/nlohmann/json.hpp"
#include <filesystem>
#include <memory>
#include <set>

namespace BetterEndfieldNext::CustomModel::ModelManagement {
using Json=nlohmann::json;
inline std::filesystem::path Utf8Path(std::string_view value) {
    return std::filesystem::path(std::u8string_view(reinterpret_cast<const char8_t*>(value.data()),value.size()));
}
inline std::string PathUtf8(const std::filesystem::path& path) {
    const auto value=path.u8string();return {reinterpret_cast<const char*>(value.data()),value.size()};
}
struct Metadata {
    std::filesystem::path path;
    std::filesystem::file_time_type mtime{};
    uintmax_t size=0;bool unchecked=false;
    BemPackageInfo info;
    Json manifest,groups,parameters;
    std::string error;
};
struct Package {
    std::string section;
    std::shared_ptr<const Metadata> metadata;
    bool enabled=false;
    BemSelection selection;
    std::string error;
    std::string options_source,parameters_source;
    mutable std::map<std::pair<std::string,std::string>,bool> choice_available;
};
inline bool CanChoose(const Package& package,const std::string& group,const std::string& choice) {
    const auto key=std::make_pair(group,choice);const auto found=package.choice_available.find(key);
    if(found!=package.choice_available.end()) return found->second;
    BemSelection next;std::string error;
    const bool available=std::find(package.selection.available_groups.begin(),package.selection.available_groups.end(),group)!=package.selection.available_groups.end();
    const bool valid=available&&ResolveBemSelection(package.metadata->info,
        Settings::PatchPairs(package.selection.options,group,choice),package.selection.parameters_saved,next,error);
    package.choice_available.emplace(key,valid);return valid;
}
enum class ActionKind {Enable,Disable,DisableAll,Option,Appearance,Parameter,Defaults};
struct Action {ActionKind kind;std::string section,id,value;};
inline std::string TargetKey(const BemPackageInfo& info) {return info.target_kind+":"+info.target_id;}
inline bool SupportsWindows(const BemPackageInfo& info) {
    return info.minor<4 || std::any_of(info.resource_keys.begin(),info.resource_keys.end(),[](const auto& key){return key.starts_with("windows-x64:");});
}
inline bool Conflicts(const BemPackageInfo& first,const BemPackageInfo& second) {
    if(!first.package_id.empty() && first.package_id==second.package_id) return true;
    // Preserve the legacy character contract while allowing explicit forms to
    // coexist with a normal character package when their resource roots differ.
    if(first.minor<4 && second.minor<4 && !first.character_id.empty() && first.character_id==second.character_id) return true;
    for(const auto& key:first.resource_keys)
        if(key.starts_with("windows-x64:") && std::find(second.resource_keys.begin(),second.resource_keys.end(),key)!=second.resource_keys.end()) return true;
    return false;
}
class Library {
public:
    std::filesystem::path root,installed_directory;
    std::vector<Package> packages;
    size_t metadata_reads=0;
    void Refresh(const Settings::Ini& ini) {
        previous_=std::move(packages);packages.clear();const bool unchecked=ini.Flag("CustomModel","skip_validation");
        std::set<std::filesystem::path> used;
        std::set<std::string> sections;
        for(const auto& [section,values]:ini.All()) {
            if(!section.starts_with("Mod.")) continue;
            const auto file=ini.Get(section,"package");
            if(file.empty()) continue;
            auto path=Utf8Path(file);if(path.is_relative()) path=root/path;path=std::filesystem::absolute(path).lexically_normal();
            used.insert(path);sections.insert(section);Add(ini,section,Get(path,unchecked));
        }
        // Installed library beside the launcher, then legacy profile packages.
        for(const auto& dir:{installed_directory,root/"packages"}) {
            std::error_code error;if(dir.empty()||!std::filesystem::is_directory(dir,error)) continue;
            for(std::filesystem::directory_iterator it(dir,error),end;!error&&it!=end;it.increment(error)) {
                if(it->path().extension()!=L".bem") continue;
                const auto path=std::filesystem::absolute(it->path()).lexically_normal();if(!used.insert(path).second) continue;
                const auto metadata=Get(path,unchecked);const auto section="Mod."+metadata->info.package_id;
                // A saved package path is authoritative, including missing/error entries.
                if(metadata->error.empty()&&!sections.insert(section).second) continue;
                Add(ini,metadata->error.empty()?section:"File."+PathUtf8(path.filename()),metadata);
            }
        }
        std::sort(packages.begin(),packages.end(),[](const auto& a,const auto& b) {
            return std::tie(a.metadata->info.target_kind,a.metadata->info.target_id,a.metadata->info.name,a.section)<
                std::tie(b.metadata->info.target_kind,b.metadata->info.target_id,b.metadata->info.name,b.section);
        });
        std::erase_if(cache_,[&](const auto& item){return !used.contains(item.first);});
    }
    const Package* Find(const std::string& section) const {
        for(const auto& package:packages) if(package.section==section) return &package;return nullptr;
    }
    void Apply(Settings::Ini& ini,const Action& action) {
        if(action.kind==ActionKind::DisableAll) {
            std::vector<std::string> mods;
            for(const auto& [s,_]:ini.All()) if(s.starts_with("Mod.")) mods.push_back(s);
            for(const auto& package:packages) if(package.section.starts_with("Mod.")) mods.push_back(package.section);
            for(const auto& section:mods) ini.Set(section,"enabled","false");return;
        }
        if(action.kind==ActionKind::Disable) {ini.Set(action.section,"enabled","false");return;}
        Refresh(ini); // called outside the cross-process mutex by optimistic Update
        const auto* package=Find(action.section);
        if(!package) throw std::runtime_error("Package changed or was removed; refresh and retry");
        if(!package->metadata->error.empty()) throw std::runtime_error(package->metadata->error);
        const auto& info=package->metadata->info;
        auto options=ini.Get(action.section,info.minor?"options":"appearance");
        auto remembered=ini.Get(action.section,"parameters_saved",ini.Get(action.section,"parameters"));
        BemSelection previous,next;std::string why;
        if(action.kind==ActionKind::Defaults) {options={};remembered={};}
        else {
            if(!ResolveBemSelection(info,options,remembered,previous,why)) throw std::runtime_error(why);
            options=previous.options;remembered=previous.parameters_saved;
            if(action.kind==ActionKind::Option) options=Settings::PatchPairs(options,action.id,action.value);
            if(action.kind==ActionKind::Appearance) options=action.value;
            if(action.kind==ActionKind::Parameter) {
                if(std::find(previous.available_parameters.begin(),previous.available_parameters.end(),action.id)==previous.available_parameters.end())
                    throw std::runtime_error("Parameter is unavailable for the current options");
                remembered=Settings::PatchPairs(remembered,action.id,action.value);
            }
            if(action.kind==ActionKind::Option&&std::find(previous.available_groups.begin(),previous.available_groups.end(),action.id)==previous.available_groups.end())
                throw std::runtime_error("Option group is unavailable");
        }
        if(!ResolveBemSelection(info,options,remembered,next,why)) throw std::runtime_error(why);
        if(action.kind==ActionKind::Enable) {
            if(!SupportsWindows(info)) throw std::runtime_error("This package has no Windows resource targets and cannot be enabled on this platform");
            for(const auto& [section,_]:ini.All()) if(section.starts_with("Mod.")&&ini.Flag(section,"enabled")&&!Find(section))
                throw std::runtime_error("Enabled package has no readable metadata; disable it first");
            for(const auto& peer:packages) {
                if(peer.section==action.section) continue;
                if(peer.enabled&&!peer.metadata->error.empty()) throw std::runtime_error("Enabled package metadata unavailable; disable it first");
                const auto& other=peer.metadata->info;
                if(Conflicts(info,other))
                    ini.Set(peer.section,"enabled","false");
            }
            ini.Set(action.section,"enabled","true");
        }
        // Write complete author-default-filled values, retaining dormant sliders.
        ini.Set(action.section,"package",ini.Get(action.section,"package",PathUtf8(package->metadata->path)));
        ini.Set(action.section,info.minor?"options":"appearance",next.options);
        ini.Set(action.section,"parameters",next.parameters);
        ini.Set(action.section,"parameters_saved",next.parameters_saved);
    }
private:
    std::map<std::filesystem::path,std::shared_ptr<Metadata>> cache_;
    std::vector<Package> previous_;
    std::shared_ptr<const Metadata> Get(const std::filesystem::path& path,bool unchecked) {
        std::error_code te,se;const auto time=std::filesystem::last_write_time(path,te);const auto size=std::filesystem::file_size(path,se);
        const auto found=cache_.find(path);
        if(found!=cache_.end()&&found->second->mtime==time&&found->second->size==size&&found->second->unchecked==unchecked) return found->second;
        auto metadata=std::make_shared<Metadata>();metadata->path=path;metadata->mtime=time;metadata->size=size;metadata->unchecked=unchecked;
        if(te||se) metadata->error="Package file unavailable: "+PathUtf8(path.filename());
        else {
            ++metadata_reads;
            if(ReadBemManagementInfo(path,metadata->info,metadata->error,unchecked)) {
                const auto after=std::filesystem::last_write_time(path,te);const auto after_size=std::filesystem::file_size(path,se);
                if(te||se||time!=after||size!=after_size) metadata->error="Package changed while reading metadata; retry";
                metadata->manifest=Json::parse(metadata->info.manifest_json);
                metadata->groups=metadata->manifest.value("option_groups",Json::array());
                metadata->parameters=Json::parse(metadata->info.parameter_groups_json);
            }
        }
        cache_[path]=metadata;return metadata;
    }
    void Add(const Settings::Ini& ini,const std::string& section,std::shared_ptr<const Metadata> metadata) {
        Package package{section,std::move(metadata)};package.enabled=ini.Flag(section,"enabled") && SupportsWindows(package.metadata->info);
        const auto& info=package.metadata->info;
        package.options_source=ini.Get(section,info.minor?"options":"appearance");
        package.parameters_source=ini.Get(section,"parameters_saved",ini.Get(section,"parameters"));
        for(const auto& previous:previous_) if(previous.section==section&&previous.metadata==package.metadata&&
            previous.options_source==package.options_source&&previous.parameters_source==package.parameters_source) {
            package.selection=previous.selection;package.error=previous.error;package.choice_available=previous.choice_available;
            packages.push_back(std::move(package));return;
        }
        package.error=package.metadata->error;
        if(package.error.empty()) {
            ResolveBemSelection(info,package.options_source,package.parameters_source,package.selection,package.error);
        }
        packages.push_back(std::move(package));
    }
};
} // namespace BetterEndfieldNext::CustomModel::ModelManagement
