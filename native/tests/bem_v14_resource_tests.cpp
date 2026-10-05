#include "../modules/custom_model/bem.h"
#include "../modules/custom_model/bem_rewrite.h"
#include "../modules/custom_model/mod_registry.h"
#include "../shared/third_party/nlohmann/json.hpp"
#include <algorithm>
#include <cstring>
#include <filesystem>
#include <iostream>
#include <stdexcept>

using namespace BetterEndfield::CustomModel;
namespace {
int checks=0;
void Require(bool value,const std::string& why) { ++checks; if(!value) throw std::runtime_error(why); }
float X(const BemComponent& component) { float x; std::memcpy(&x,component.streams[0].data()+12,4); return x; }
std::string Config(const std::string& file,const std::string& section="one") {
    return "[CustomModel]\n[Mod."+section+"]\nenabled=true\npackage="+file+"\n";
}
}
#ifdef _WIN32
int wmain(int argc,wchar_t** argv) {
#else
int main(int argc,char** argv) {
#endif
    try {
        Require(argc==3,"Usage: BemV14ResourceTests <fixture directory> <rewrite output>");
        const auto root=std::filesystem::path(argv[1]); const auto valid=root/"multi-resource-valid.bem";
        const auto rewritten=std::filesystem::path(argv[2]); std::string error;
        BemPackageInfo info;
        Require(ReadBemManagementInfo(valid,info,error),"Management metadata: "+error);
        Require(info.minor==4 && info.target_kind=="character" && info.target_id=="chr_synthetic","Owner metadata differs");
        Require(info.resources.size()==3 && info.resource_keys.size()==5,"Resource/platform metadata differs");
        BemPocData all;
        Require(LoadBem(valid,all,error,"detail:show",nullptr,false,true,"length:1000"),"All-resource decode: "+error);
        Require(all.components.size()==3 && all.header.component_count==3,"Full package omitted resources");
        Require(!all.components[0].static_mesh && all.components[2].static_mesh,"Renderer kinds differ");
        for(size_t i=0;i<info.resources.size();++i) {
            const auto& resource=info.resources[i]; BemPocData selected; BemLoadPlan plan; BemLoadStats stats;
            Require(ReadBemLoadPlan(valid,plan,error,"detail:show",false,"length:1000",false,resource.id),"Resource load plan: "+error);
            Require(LoadBem(valid,selected,error,"detail:show",&stats,false,true,"length:1000",plan.reservation_bytes,false,resource.id),"Resource decode: "+error);
            Require(selected.components.size()==1 && selected.header.component_count==1,"Sibling resource was decoded");
            const auto& c=selected.components[0];
            Require(c.info.component_id==0 && c.draws.size()==1 && c.draws[0].material_component==0,"Material donor was not localized");
            Require(c.indices==all.components[i].indices && c.streams==all.components[i].streams,"Resource selection changed payload");
            Require(X(c)==1.0f+float(i+1)/4.0f,"Morph used sibling resource channel");
            Require(stats.decoded_cache_remaining_bytes==0,"Selected cache was retained");
            Require(stats.payload_ids.size()==plan.payload_ids.size(),"Plan/load payload closure differs");
            if(resource.id=="weapon") {
                Require(c.static_mesh && c.bones.empty() && c.info.stream_count==2 && c.streams[2].empty(),"Static data received a skin stream");
                BemPocData hidden;
                Require(LoadBem(valid,hidden,error,"detail:hide",nullptr,false,true,{},UINT64_MAX,false,resource.id),"Hidden resource decode: "+error);
                Require(hidden.components.size()==1 && hidden.components[0].static_mesh &&
                    (hidden.components[0].info.flags&kComponentFlagHidden),"Hide lost static receiver kind");
            } else Require(!c.static_mesh && c.bones.size()==1 && c.bones[0].component==0,"Bone donor was not localized");
        }
        BemPocData missing;
        Require(!LoadBem(valid,missing,error,{},nullptr,false,false,{},UINT64_MAX,false,"missing"),"Unknown resource was accepted");
        Require(!ReadBemPackageInfo(root/"cross-resource-donor-invalid.bem",info,error),"Cross-resource donor metadata accepted");
        Require(!ReadBemPackageInfo(root/"cross-resource-donor-invalid.bem",info,error,true),"Skip validation crossed resource boundary");
        Require(!ReadBemPackageInfo(root/"downgraded-header-invalid.bem",info,error,true),"Older header accepted resource extension");
        const auto wrongLod=root/"resource-lod-mismatch-invalid.bem";
        Require(std::filesystem::is_regular_file(wrongLod),"Missing receiver/resource LOD fixture");
        Require(!ReadBemPackageInfo(wrongLod,info,error) && error.find("LOD differs")!=error.npos,
            "Contradictory receiver/resource LOD accepted");
        Require(!ReadBemPackageInfo(wrongLod,info,error,true) && error.find("LOD differs")!=error.npos,
            "Skip validation bypassed receiver/resource LOD");
        Require(!LoadBem(wrongLod,missing,error,{},nullptr,true,false,{},UINT64_MAX,false,"body") &&
            error.find("LOD differs")!=error.npos,
            "Selected unchecked load bypassed receiver/resource LOD");
        ModRegistry registry;
        Require(ParseModRegistry(Config("multi-resource-valid.bem"),root,registry,error),"Registry parsing: "+error);
#if defined(__ANDROID__)
        Require(registry.enabled.size()==2 && !registry.Match("ultimate"),"Android platform filter differs");
#else
        Require(registry.enabled.size()==3 && registry.Match("ultimate"),"Windows resource registrations differ");
#endif
        const auto* body=registry.Match("body(Clone)#2"); const auto* weapon=registry.Match("weapon");
        Require(body && weapon && body->selection_key!=weapon->selection_key,"Resource cache identity collided");
        Require(body->adapter->explicit_resource && body->adapter->components.size()==1 &&
            weapon->adapter->components[0].static_mesh,"Registry lowered invalid component contracts");
        const std::string duplicate=Config("multi-resource-valid.bem")+"[Mod.two]\nenabled=true\npackage=multi-resource-valid.bem\n";
        Require(ParseModRegistry(duplicate,root,registry,error) && registry.enabled.empty(),"Duplicate package entries were enabled");
        std::string report;
        Require(RewriteBemTextures(valid,rewritten,[](const BemJson&,BemJson&,std::vector<uint8_t>&){},[] {},report,error),"Rewrite: "+error);
        const auto metadata=nlohmann::json::parse(report);
        Require(metadata.at("target_kind")=="character" && metadata.at("resource_keys").size()==5,"Rewrite lost target identities");
        BemPocData after;
        Require(LoadBem(rewritten,after,error,"detail:show",nullptr,false,true,"length:1000",UINT64_MAX,false,"weapon"),"Rewritten resource decode: "+error);
        Require(after.components[0].static_mesh && after.components[0].streams==all.components[2].streams,"Rewrite changed static mesh data");
        std::cout<<"BEM 1.4 resource checks passed: "<<checks<<"\n"; return 0;
    } catch(const std::exception& e) { std::cerr<<e.what()<<"\n"; return 1; }
}
