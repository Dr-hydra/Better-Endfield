#include "../modules/custom_model/bem.h"
#include "../modules/custom_model/bem_rewrite.h"
#include "../modules/custom_model/mod_registry.h"
#include "../shared/third_party/nlohmann/json.hpp"
#include <iostream>
#include <fstream>
#include <cstring>
#include <algorithm>
#include <set>
#include <random>
using namespace BetterEndfieldNext::CustomModel;
namespace {
bool SamePayload(const BemPocData& a,const BemPocData& b) {
    if(std::memcmp(&a.header,&b.header,sizeof(a.header)) || a.components.size()!=b.components.size() ||
        a.textures.size()!=b.textures.size() || a.skip_validation!=b.skip_validation) return false;
    auto sameRaw=[](const auto& left,const auto& right) {
        return left.size()==right.size() && (left.empty() ||
            std::memcmp(left.data(),right.data(),left.size()*sizeof(left[0]))==0);
    };
    for(size_t i=0;i<a.components.size();++i) {
        const auto& x=a.components[i]; const auto& y=b.components[i];
        if(std::memcmp(&x.info,&y.info,sizeof(x.info)) || x.streams!=y.streams || x.indices!=y.indices ||
            x.static_mesh!=y.static_mesh || x.layout_crc!=y.layout_crc || x.attributes!=y.attributes || x.bone_names!=y.bone_names ||
            x.bone_aliases!=y.bone_aliases || x.material_names!=y.material_names ||
            x.keep_material_names!=y.keep_material_names || !sameRaw(x.bones,y.bones) ||
            !sameRaw(x.draws,y.draws) || !sameRaw(x.keep_material_overrides,y.keep_material_overrides)) return false;
    }
    for(size_t i=0;i<a.textures.size();++i) {
        const auto& x=a.textures[i]; const auto& y=b.textures[i];
        if(std::memcmp(&x.info,&y.info,sizeof(x.info)) || x.name!=y.name ||
            x.original_name!=y.original_name || x.data!=y.data) return false;
    }
    return true;
}
}
#ifdef _WIN32
int wmain(int argc,wchar_t** argv) {
#else
int main(int argc,char** argv) {
#endif
    if(argc<2) return 2;
    std::vector<std::string> explicitSelections;
    int rewriteArg=0;
    bool skipValidation=false;
    bool loadingOptimization=false,compareLoading=false;
    std::string explicitParameters;
    bool hasParameters=false;
    for(int i=2;i<argc;++i) {
#ifdef _WIN32
        const bool isOption=std::wstring_view(argv[i])==L"--options";
        const bool isSkip=std::wstring_view(argv[i])==L"--skip-validation";
        const bool isLoading=std::wstring_view(argv[i])==L"--loading-optimization";
        const bool isCompare=std::wstring_view(argv[i])==L"--compare-loading";
        const bool isParameters=std::wstring_view(argv[i])==L"--parameters";
#else
        const bool isOption=std::string_view(argv[i])=="--options";
        const bool isSkip=std::string_view(argv[i])=="--skip-validation";
        const bool isLoading=std::string_view(argv[i])=="--loading-optimization";
        const bool isCompare=std::string_view(argv[i])=="--compare-loading";
        const bool isParameters=std::string_view(argv[i])=="--parameters";
#endif
        if(isSkip) {skipValidation=true;continue;}
        if(isLoading) {loadingOptimization=true;continue;}
        if(isCompare) {compareLoading=true;continue;}
        if(isParameters) {
            if(hasParameters || ++i>=argc) {std::cerr<<"--parameters needs one tick selection";return 2;}
            hasParameters=true;
#ifdef _WIN32
            for(wchar_t c:std::wstring_view(argv[i])) {
                if(c<0 || c>127) {std::cerr<<"--parameters must use ASCII stable IDs";return 2;}
                explicitParameters.push_back(static_cast<char>(c));
            }
#else
            explicitParameters=argv[i];
#endif
            continue;
        }
        if(isOption) {
            if(++i>=argc) {std::cerr<<"--options needs a selection";return 2;}
#ifdef _WIN32
            std::wstring_view value(argv[i]);
            if(!std::all_of(value.begin(),value.end(),[](wchar_t c){return c>=0 && c<=127;})) {
                std::cerr<<"--options must use ASCII stable IDs";return 2;
            }
            std::string selection;selection.reserve(value.size());
            for(wchar_t c:value) selection.push_back(static_cast<char>(c));
            explicitSelections.push_back(std::move(selection));
#else
            explicitSelections.emplace_back(argv[i]);
#endif
        } else if(!rewriteArg) rewriteArg=i;
        else {std::cerr<<"Unexpected argument";return 2;}
    }
    std::string error; BemPackageInfo info;
    if(!ReadBemPackageInfo(argv[1],info,error,skipValidation)) {std::cerr<<error;return 1;}
    if(!info.minor && !explicitSelections.empty()) {std::cerr<<"--options needs BEM 1.1";return 2;}
    std::vector<std::string> selections=info.appearances;
    if(!explicitSelections.empty()) selections=explicitSelections;
    else if(info.minor) {
        selections.clear();
        const auto groups=nlohmann::json::parse(info.option_groups_json);
        std::vector<std::string> ids,defaults;
        std::vector<std::vector<std::string>> choices;
        size_t product=1;
        bool exhaustive=true;
        for(const auto& group:groups) {
            ids.push_back(group.at("id").get<std::string>());
            defaults.push_back(group.at("default").get<std::string>());
            auto& values=choices.emplace_back();
            for(const auto& item:group.at("choices")) values.push_back(item.at("id").get<std::string>());
            if(exhaustive && product<=1024/values.size()) product*=values.size();
            else exhaustive=false;
        }
        auto encode=[&](const std::vector<std::string>& values) {
            std::string result;
            for(size_t i=0;i<ids.size();++i) {
                if(i) result+='&';
                result+=ids[i]+":"+values[i];
            }
            return result;
        };
        if(exhaustive) {
            auto enumerate=[&](auto&& self,size_t index,std::vector<std::string>& values)->void {
                if(index==groups.size()) {selections.push_back(encode(values));return;}
                for(const auto& choice:choices[index]) {values[index]=choice;self(self,index+1,values);}
            };
            auto values=defaults;enumerate(enumerate,0,values);
        } else {
            // The creator proves all assignments symbolically. This executable checks
            // actual native payload decoding for a bounded, deterministic set of states.
            std::set<std::string> seen;
            auto add=[&](const std::vector<std::string>& values) {
                auto encoded=encode(values);
                if(seen.insert(encoded).second) selections.push_back(std::move(encoded));
            };
            add(defaults);
            for(size_t group=0;group<groups.size();++group)
                for(const auto& choice:choices[group]) {
                    auto values=defaults;values[group]=choice;add(values);
                }
            auto values=defaults;
            for(size_t i=0;i<values.size();++i) values[i]=choices[i].back();
            add(values);
            for(size_t parity=0;parity<2;++parity) {
                values=defaults;
                for(size_t i=parity;i<values.size();i+=2) values[i]=choices[i].back();
                add(values);
            }
            std::mt19937 rng(0xB3E110u);
            for(int trial=0;trial<24;++trial) {
                values=defaults;
                for(size_t i=0;i<values.size();++i) values[i]=choices[i][rng()%choices[i].size()];
                add(values);
            }
            std::cout<<"Native validation checks "<<selections.size()
                <<" deterministic selections; exhaustive option proof belongs to the creator validator\n";
        }
    }
    BemPocData first,second;
    const std::string firstSelection=explicitSelections.empty()?info.default_options:explicitSelections.front();
    if(info.minor && !LoadBem(argv[1],first,error,firstSelection,nullptr,skipValidation,loadingOptimization,explicitParameters)) {std::cerr<<error;return 1;}
    size_t accepted=0;
    bool capturedRepeat=false;
    for(const auto& appearance:selections) {
        BemPocData data;BemLoadStats stats;
        if(!LoadBem(argv[1],data,error,appearance,&stats,skipValidation,loadingOptimization,explicitParameters)) {
            if(info.minor && explicitSelections.empty() && error=="Unreachable option combination") continue;
            std::cerr<<appearance<<": "<<error;return 1;
        }
        ++accepted;
        std::cout<<appearance<<": components="<<data.components.size()<<" textures="<<data.textures.size()<<" payloads=";
        for(size_t i=0;i<stats.payload_ids.size();++i)
            std::cout<<(i?",":"")<<stats.payload_ids[i];
        std::cout<<'\n';
        if(compareLoading) {
            BemPocData other; BemLoadStats otherStats;
            if(!LoadBem(argv[1],other,error,appearance,&otherStats,skipValidation,!loadingOptimization,explicitParameters) ||
                !SamePayload(data,other) || stats.payload_ids!=otherStats.payload_ids) {
                std::cerr<<"Loading optimization changed selected bytes or payload accesses: "<<error;return 1;
            }
            const auto& optimized=loadingOptimization?stats:otherStats;
            if(optimized.decoded_cache_remaining_bytes!=0) {std::cerr<<"Decoded cache was retained";return 1;}
        }
        if(loadingOptimization || compareLoading)
            std::cout<<"memory cache_peak="<<stats.decoded_cache_peak_bytes<<" cache_remaining="<<stats.decoded_cache_remaining_bytes
                <<" copied="<<stats.payload_copy_bytes<<" moved="<<stats.payload_move_bytes<<'\n';
        if(info.minor && appearance==firstSelection) {second=std::move(data);capturedRepeat=true;}
    }
    if(!accepted) {std::cerr<<"No reachable selection";return 1;}
    if(info.minor) {
        if(!capturedRepeat && !LoadBem(argv[1],second,error,firstSelection,nullptr,skipValidation,loadingOptimization,explicitParameters)) {std::cerr<<error;return 1;}
        if(std::memcmp(&first.header,&second.header,sizeof(first.header))!=0 ||
           first.components.size()!=second.components.size() || first.textures.size()!=second.textures.size()) return 1;
        for(size_t i=0;i<first.components.size();++i) {
            const auto& a=first.components[i];const auto& b=second.components[i];
            if(std::memcmp(&a.info,&b.info,sizeof(a.info))!=0 || a.indices!=b.indices || a.streams!=b.streams ||
               a.static_mesh!=b.static_mesh || a.layout_crc!=b.layout_crc || a.attributes!=b.attributes || a.bone_names!=b.bone_names ||
               a.material_names!=b.material_names || a.bones.size()!=b.bones.size() || a.draws.size()!=b.draws.size()) return 1;
            for(size_t j=0;j<a.bones.size();++j)
                if(std::memcmp(&a.bones[j],&b.bones[j],sizeof(BemComponent::BoneSource))!=0) return 1;
            for(size_t j=0;j<a.draws.size();++j)
                if(std::memcmp(&a.draws[j],&b.draws[j],sizeof(BemComponent::Draw))!=0) return 1;
        }
        for(size_t i=0;i<first.textures.size();++i)
            if(std::memcmp(&first.textures[i].info,&second.textures[i].info,sizeof(BemTextureEntryRaw))!=0 ||
               first.textures[i].name!=second.textures[i].name ||
               first.textures[i].original_name!=second.textures[i].original_name ||
               first.textures[i].data!=second.textures[i].data) return 1;
    }
    if(info.minor>=3 && !hasParameters) {
        const auto parameters=nlohmann::json::parse(info.parameter_groups_json);
        size_t samples=0;
        for(size_t index=0;index<parameters.size();++index) {
            const auto& parameter=parameters.at(index);auto values=info.parameter_frame_values.at(index);
            const auto min=parameter.at("min").get<uint32_t>(),max=parameter.at("max").get<uint32_t>(),step=parameter.at("step").get<uint32_t>();
            values.push_back(min);values.push_back(max);values.push_back(parameter.at("neutral").get<uint32_t>());
            values.push_back(min+((max-min)/step/2)*step);
            std::sort(values.begin(),values.end());values.erase(std::unique(values.begin(),values.end()),values.end());
            for(const auto tick:values) {
                const auto selection=parameter.at("id").get<std::string>()+":"+std::to_string(tick);BemPocData sample;
                if(!LoadBem(argv[1],sample,error,firstSelection,nullptr,skipValidation,loadingOptimization,selection)) {
                    std::cerr<<"Parameter sample "<<selection<<": "<<error;return 1;
                }
                ++samples;
            }
        }
        std::cout<<"Position parameters: "<<samples<<" authored frame/range/interpolation samples accepted\n";
    }
    auto utf=std::filesystem::path(argv[1]).filename().u8string();
    std::string ini="[CustomModel]\nstandalone_lod=false\nskip_validation="+std::string(skipValidation?"true":"false")+"\n[Mod.test]\nenabled=true\npackage="+
        std::string(reinterpret_cast<const char*>(utf.data()),utf.size())+
        (info.minor?"\noptions="+info.default_options:"\nappearance="+info.default_appearance)+"\n";
    ModRegistry registry;
    if(!ParseModRegistry(ini,std::filesystem::path(argv[1]).parent_path(),registry,error)) {std::cerr<<"Registry: "<<error;return 1;}
    if(info.minor>=4) {
#if defined(__ANDROID__)
        const std::string platform="android-arm64";
#else
        const std::string platform="windows-x64";
#endif
        size_t expected=0;
        for(const auto& resource:info.resources) if(std::find(resource.platforms.begin(),resource.platforms.end(),platform)!=resource.platforms.end()) {
            ++expected;
            const auto* mod=registry.Match(resource.name);
            if(!mod || mod->resource_id!=resource.id || !mod->adapter->explicit_resource ||
                mod->adapter->components.size()!=resource.component_ids.size()) {std::cerr<<"Explicit resource registry mismatch";return 1;}
        }
        if(registry.enabled.size()!=expected) {std::cerr<<"Resource registry count differs";return 1;}
    } else if(registry.enabled.size()!=1 || !registry.Match(info.world_resource)||!registry.Match(info.ui_resource)) {
        std::cerr<<"Registry: "<<error;return 1;
    }
    if(rewriteArg) {
        std::string report;
        if(!RewriteBemTextures(argv[1],argv[rewriteArg],[](const BemJson&,BemJson&,std::vector<uint8_t>&){},
            []{},report,error)) {std::cerr<<"Identity rewrite: "<<error;return 1;}
        BemPackageInfo rewritten;
        if(!ReadBemPackageInfo(argv[rewriteArg],rewritten,error) || rewritten.minor!=info.minor ||
           rewritten.package_id!=info.package_id) {std::cerr<<"Rewritten metadata: "<<error;return 1;}
    }
    std::cout<<"BEMv1 package and resource routing accepted\n";
    return 0;
}
