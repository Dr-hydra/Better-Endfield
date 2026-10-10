#include "android_cloth_probe.h"
#include "android_mesh_builder.h"
#include "core/log.h"
#include "cloth_proxy_validation.h"
#include "cloth_contact_counter.h"
#include "cloth_display_binding.h"
#include "cloth_bone_binding.h"
#include "cloth_layer_order.h"
#include "mod_registry.h"
#include "core/hook_broker.h"
#include <atomic>
#include "loaded_il2cpp.h"
#include <dlfcn.h>
#include <cstdio>
#include "../../../../../../../native/shared/third_party/nlohmann/json.hpp"
#include <algorithm>
#include <cstdlib>
#include <cstring>
#include <filesystem>
#include <fstream>
#include <set>
#include <stdexcept>
#include <array>
#include <chrono>
#include <cmath>
#include <memory>
#include <map>

namespace betterendfieldnext {
namespace {
using Json = nlohmann::json;
constexpr const char* kUnity="UnityEngine.CoreModule.dll";
constexpr const char* kCloth="BeyondDynamicBone.dll";
Il2CppRuntime* runtime=nullptr;
std::string request_id;
std::set<int> sampled;
Json report;
std::filesystem::path candidate_claim_path;
bool persistent_request=false;
bool capture_seam_request=false;
std::map<std::array<std::string,7>,ResolvedMethod> probe_method_cache;
bool LiveMode(const std::string& mode){return mode=="bind-live-pair"||mode=="bind-live-bonecloth";}
bool CleanActorRetirement(const Json& value) {
    if(value.value("status","")!="actor_retired"||value.contains("error")||
        !value.value("cleanup_confirmed",false)||!value.value("contact_hooks_released",false))return false;
    const auto reason=value.value("retirement_reason","");
    return reason=="bone cloth receiver mesh replaced by the game"||reason=="bone cloth actor retired"||
        reason=="bone cloth actor inactive"||reason=="bone cloth receiver bones replaced"||
        reason=="bone cloth display context changed";
}
bool HealthyCandidate(const Json& value) {
    if(value.value("bonecloth_bound",false))
        return value.value("pair_contact_registration_verified",false)&&
            value.value("body_collider_registration_verified",false)&&!value.contains("error");
    return value.value("display_frames_written",uint64_t{0})>=300&&
        value.value("pair_contact_registration_verified",false)&&value.value("live_mesh_readback_verified",false)&&
        !value.contains("error");
}

bool ClaimCandidateRequest(const char* result_key="candidate") {
    // A process crash must not replay an experimental construction on restart.
    if(std::filesystem::exists(candidate_claim_path)) {
        Json previous;std::ifstream(candidate_claim_path)>>previous;
        const bool safe_restart=persistent_request&&previous.value("persistent",false)&&
            (HealthyCandidate(previous.value("candidate",Json::object()))||
             CleanActorRetirement(previous.value("candidate",Json::object())));
        if(previous.value("request_id","")==request_id&&!safe_restart) {
            report[result_key]=previous.value(result_key,Json{{"status","interrupted_or_result_unavailable"}});
            report["automatic_replay_refused"]=true;
            return false;
        }
    }
    std::ofstream file(candidate_claim_path,std::ios::trunc);
    file<<Json{{"request_id",request_id},{"state","started"},{"persistent",persistent_request}}.dump();file.flush();
    if(!file)throw std::runtime_error("candidate one-shot marker write failed");
    return true;
}

struct Scope {
    std::vector<uint32_t> roots;
    std::string missing_method;
    ~Scope() { for (auto h:roots) runtime->FreeGcHandle(h); }
    void* Root(void* value) {
        if (!value) return nullptr;
        auto h=runtime->NewGcHandle(value,false);
        if (!h) throw std::runtime_error("GC root allocation failed");
        roots.push_back(h);return value;
    }
    ResolvedMethod Method(const char* assembly,const char* ns,const char* cls,
            const char* name,const char* parameters,const char* result,int count) {
        const std::array<std::string,7> key{assembly,ns,cls,name,parameters,result,std::to_string(count)};
        auto found=probe_method_cache.find(key);
        if(found!=probe_method_cache.end())return found->second;
        auto method=runtime->ResolveMethodExact(assembly,ns,cls,name,parameters,result,count);
        if(method.info)probe_method_cache.emplace(key,method);
        if(!method.info)missing_method=std::string(assembly)+":"+ns+"."+cls+"."+name+"("+parameters+") -> "+result;
        return method;
    }
    void* Call(ResolvedMethod method,void* object,void** args=nullptr) {
        if (!method.info) throw std::runtime_error("required managed method missing: "+missing_method);
        void* exception=nullptr;
        void* result=runtime->Invoke(method.info,object,args,&exception);
        if (exception) throw std::runtime_error("managed invocation failed");
        return Root(result);
    }
    std::string String(void* value) { return value?runtime->CopyString(value):std::string{}; }
    int Int(ResolvedMethod method,void* object,void** args=nullptr) {
        void* box=Call(method,object,args);
        auto* value=runtime->Unbox(box);
        if (!value) throw std::runtime_error("missing integer result");
        int result=0;std::memcpy(&result,value,sizeof(result));return result;
    }
    void* Field(void* object,const char* cls,const char* name) {
        const auto f=runtime->ResolveField(kCloth,"BeyondDynamicBone",cls,name);
        return f.info?Root(runtime->ReadFieldObject(f,object)):nullptr;
    }
};
Json Serialize(Scope& scope,void* value) {
    if (!value) return nullptr;
    auto method=scope.Method("UnityEngine.JSONSerializeModule.dll","UnityEngine","JsonUtility",
        "ToJson","System.Object|System.Boolean","System.String",2);
    bool pretty=false;void* args[]{value,&pretty};
    const auto text=scope.String(scope.Call(method,nullptr,args));
    if (text.empty() || text.size()>1024*1024) throw std::runtime_error("cloth serialization size invalid");
    return Json::parse(text);
}
#include "android_cloth_contact_abi.inc"
#include "android_cloth_contact_hooks.inc"
Json Contracts(Scope& scope) {
    Json result=Json::object();
    struct Entry { const char *cls,*name,*params,*result;int count; };
    const Entry entries[]{
        {"BeyondBoneCloth","get_SerializeData","","BeyondDynamicBone.ClothSerializeData",0},
        {"BeyondBoneCloth","get_Process","","BeyondDynamicBone.ClothProcess",0},
        {"BeyondBoneCloth","BuildAndRun","","System.Boolean",0},
        {"BeyondBoneCloth","SetParameterChange","","System.Void",0},
        {"BeyondBoneCloth","ResetCloth","System.Boolean","System.Void",1},
        {"BeyondBoneCloth","SetSkipWriting","System.Boolean","System.Void",1},
        {"ClothSerializeData",".ctor","","System.Void",0},
        {"ClothSerializeData","Import","BeyondDynamicBone.ClothSerializeData|System.Boolean","System.Void",2},
        {"ClothProcess","IsValid","","System.Boolean",0},
        {"ClothProcess","IsRunning","","System.Boolean",0},
    };
    for (const auto& e:entries) result[std::string(e.cls)+"."+e.name]=
        scope.Method(kCloth,"BeyondDynamicBone",e.cls,e.name,e.params,e.result,e.count).info!=nullptr;
    for (auto name:{"None","FullMesh"}) {
        auto field=runtime->ResolveField(kCloth,"BeyondDynamicBone","SelfCollisionConstraint.SelfCollisionMode",name);
        auto value=field.info?scope.Root(runtime->ReadFieldObject(field,nullptr)):nullptr;
        // Convert boxes through managed Convert instead of assuming enum layout.
        auto convert=scope.Method("mscorlib.dll","System","Convert","ToString","System.Object","System.String",1);
        void* args[]{value};
        result[std::string("SelfCollisionMode.")+name]=value?Json(scope.String(scope.Call(convert,nullptr,args))):Json(nullptr);
    }
    result["solver_creation_tested"]=false;
    result["pair_collision_tested"]=false;
    Json contact=Json::object();
    for(auto type:{"SelfCollisionConstraint.UpdateEdgeEdgeBroadPhaseCrossFrameJob",
                  "SelfCollisionConstraint.UpdatePointTriangleBroadPhaseCrossFrameJob"}) {
        Json fields=Json::object();
        for(auto name:{"indexCount","nextPosArray","oldPosArray","edgeEdgeContactList","pointTriangleContactList"}) {
            const auto f=runtime->ResolveField(kCloth,"BeyondDynamicBone",type,name);
            if(f.info)fields[name]=f.offset;
        }
        contact[type]={{"metadata_field_offsets",fields},
            {"execute_resolved",scope.Method(kCloth,"BeyondDynamicBone",type,"Execute","","System.Void",0).info!=nullptr}};
    }
    contact["update_broad_phase_resolved"]=scope.Method(kCloth,"BeyondDynamicBone","SelfCollisionConstraint",
        "UpdateBroadPhase","Unity.Jobs.JobHandle","Unity.Jobs.JobHandle",1).info!=nullptr;
    contact["android_counter_adapter_installed"]=false;
    result["contact_scheduler"]=std::move(contact);
    result["contact_abi"]=ContactAbiSnapshot();
    return result;
}
Json CloneConfiguration(Scope& scope,void* original) {
    const auto klass=runtime->ResolveClass(kCloth,"BeyondDynamicBone","ClothSerializeData");
    auto ctor=scope.Method(kCloth,"BeyondDynamicBone","ClothSerializeData",".ctor","","System.Void",0);
    auto import=scope.Method(kCloth,"BeyondDynamicBone","ClothSerializeData","Import",
        "BeyondDynamicBone.ClothSerializeData|System.Boolean","System.Void",2);
    if (!klass.info || !ctor.info || !import.info) return {{"status","unsupported"}};
    auto* copy=scope.Root(runtime->NewObject(klass.info));
    if (!copy || copy==original) throw std::runtime_error("private cloth configuration allocation failed");
    const auto before=Serialize(scope,original);
    scope.Call(ctor,copy);
    bool deep=true;void* args[]{original,&deep};scope.Call(import,copy,args);
    Json isolation=Json::object();bool private_collections=true;
    for (auto field:{"sourceRenderers","rootBones","ignoreFromRootBones","paintMaps",
            "customSkinningSetting","colliderCollisionConstraint","selfCollisionConstraint"}) {
        auto* a=scope.Field(original,"ClothSerializeData",field);
        auto* b=scope.Field(copy,"ClothSerializeData",field);
        const bool different=a&&b&&a!=b;
        isolation[field]=different;private_collections&=different;
    }
    const bool original_unchanged=before==Serialize(scope,original);
    return {{"status",original_unchanged&&private_collections?"isolated":"not_isolated"},
        {"original_unchanged",original_unchanged},{"private_fields",isolation},
        {"solver_created",false},{"configuration",Serialize(scope,copy)}};
}
void LiveDetach(Scope&);
void BoneDetach(Scope&);
void UpdateBoneLayerOrder(Scope&);
namespace layer_hook {void Clear(Scope&);bool Release(Scope&);}
#include "android_cloth_candidate.inc"
#include "android_cloth_layer_hooks.inc"
#include "android_cloth_live.inc"
#include "android_cloth_bone.inc"
#include "android_cloth_display_probe.inc"

Json ReadRoot(Scope& scope,const AndroidClothProbeRoot& root,bool clone,const Json* experiment=nullptr) {
    scope.Root(root.game_object);
    auto name=scope.Method(kUnity,"UnityEngine","Object","get_name","","System.String",0);
    auto instance=scope.Method(kUnity,"UnityEngine","Object","GetInstanceID","","System.Int32",0);
    auto get_children=scope.Method(kUnity,"UnityEngine","GameObject","GetComponentsInChildren",
        "System.Type|System.Boolean","UnityEngine.Component[]",2);
    auto length=scope.Method("mscorlib.dll","System","Array","get_Length","","System.Int32",0);
    auto item=scope.Method("mscorlib.dll","System","Array","GetValue","System.Int32","System.Object",1);
    const auto klass=runtime->ResolveClass(kCloth,"BeyondDynamicBone","BeyondBoneCloth");
    if (!klass.type_object) throw std::runtime_error("BeyondBoneCloth unavailable");
    scope.Root(klass.type_object);bool inactive=true;void* args[]{klass.type_object,&inactive};
    void* components=scope.Call(get_children,root.game_object,args);
    const int count=scope.Int(length,components);
    if (count<0||count>64) throw std::runtime_error("cloth component count exceeds probe budget");
    auto get_data=scope.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","get_SerializeData","","BeyondDynamicBone.ClothSerializeData",0);
    Json data={{"character_id",root.character_id},{"resource",root.resource},
        {"name",scope.String(scope.Call(name,root.game_object))},{"components",Json::array()}};
    for (int i=0;i<count;++i) {
        void* args_i[]{&i};auto* component=scope.Call(item,components,args_i);
        if (!component) continue;
        auto* configuration=scope.Call(get_data,component);
        Json row={{"name",scope.String(scope.Call(name,component))},{"instance_id",scope.Int(instance,component)},
            {"configuration",Serialize(scope,configuration)}};
        // Probe only the coat configuration copy. Other native physics stays read-only.
        if (clone && row["name"].get<std::string>()=="MC_coat" && configuration)
            row["clone_configuration"]=CloneConfiguration(scope,configuration);
        if (experiment && !candidate && !report.contains("candidate") && row["name"].get<std::string>()=="MC_coat" && configuration) {
            const auto original_before=Serialize(scope,configuration);
            try {
                if(ClaimCandidateRequest()) {
                    CandidateStart(scope,configuration,*experiment);report["candidate"]=candidate->result;
                }
            }
            catch (const std::exception& e) {
                report["candidate"]=candidate?candidate->result:Json::object();
                report["candidate"]["status"]="failed";report["candidate"]["error"]=e.what();
                report["candidate"]["render_binding_applied"]=false;
                report["candidate"]["original_configuration_unchanged"]=original_before==Serialize(scope,configuration);
                if(candidate){candidate->result=report["candidate"];candidate->finished=true;CandidateCleanup(scope);}
            }
        }
        data["components"].push_back(std::move(row));
    }
    return data;
}
}

void ConfigureAndroidClothProbe(Il2CppRuntime& value) {
    runtime=&value;request_id.clear();sampled.clear();report=Json{};probe_method_cache.clear();
}
void PollAndroidClothProbe(const std::vector<AndroidClothProbeRoot>& roots) {
    if (!runtime) return;
    const char* diagnostics=std::getenv("BETTER_ENDFIELD_NEXT_DIAGNOSTICS_PATH");
    if (!diagnostics||!*diagnostics) return;
    const auto directory=std::filesystem::path(diagnostics).parent_path();
    candidate_claim_path=directory/"bem-cloth-candidate-consumed.json";
    const auto request_path=directory/"bem-cloth-probe.request.json";
    if (!std::filesystem::exists(request_path)) {ShutdownAndroidClothProbe();return;}
    try {
        if (std::filesystem::file_size(request_path)>4096) return;
        Json request;std::ifstream(request_path)>>request;
        const auto token=request.at("request_id").get<std::string>();
        const auto character=request.at("character_id").get<std::string>();
        const auto mode=request.at("mode").get<std::string>();
        capture_seam_request=request.value("capture_seam",false);
        persistent_request=LiveMode(mode)&&request.value("persistent",false);
        if (request.value("schema",0)!=1 || token.empty()||token.size()>96 ||
            character!="chr_0013_aglina" || (mode!="inspect"&&mode!="clone-configuration"&&
                mode!="build-proxy-pair"&&mode!="build-independent-proxies"&&mode!="verify-contact-scheduler"&&
                mode!="verify-display-write"&&mode!="bind-live-pair"&&mode!="bind-live-bonecloth")) {ShutdownAndroidClothProbe();return;}
        Scope scope;
        bool changed=token!=request_id;
        if (changed) {
            if (!CandidateCleanup(scope)||!FinishDisplayProbe(scope)) return;
            request_id=token;sampled.clear();
            report={{"schema",1},{"request_id",token},{"mode",mode},{"live_solver_modified",false},
                {"contracts",Contracts(scope)},{"roots",Json::array()}};
        }
        if(persistent_request&&!candidate&&report.contains("candidate")&&
            report["candidate"].value("cleanup_confirmed",false)&&
            (HealthyCandidate(report["candidate"])||CleanActorRetirement(report["candidate"]))) {
            report["retired_candidate"]=report["candidate"];
            report.erase("candidate");sampled.clear();changed=true;
        }
        if(mode=="bind-live-pair") {
            try {
                live_cloth::ShowRest(scope,request.value("show_rest_mesh",false));
                live_cloth::DisplayTest(request.value("demo_displacement_mm",0),request.value("force_gpu_upload",false));
                if(request.contains("contact_thickness_mm"))live_cloth::ContactPolicy(scope,request.at("contact_thickness_mm").get<int>());
                live_cloth::ContactCounts(scope);
            }catch(const std::exception& e) {
                if(candidate){candidate->result["status"]="failed";candidate->result["error"]=e.what();candidate->finished=true;}
                changed=true;
            }
        }
        if(mode=="bind-live-bonecloth") {
            try {bone_cloth::Monitor(scope);}
            catch(const std::exception& e) {
                if(candidate){candidate->result["status"]="actor_retired";candidate->result["retirement_reason"]=e.what();candidate->finished=true;}
                changed=true;
            }
        }
        Json authoring;
        bool build=mode=="build-independent-proxies"||mode=="verify-contact-scheduler"||mode=="build-proxy-pair"||LiveMode(mode);
        if(mode=="verify-contact-scheduler"||mode=="build-proxy-pair"||LiveMode(mode)) {
            for(const auto& method:report["contracts"]["contact_abi"]["specialized_schedules"])
                if(method.contains("error")) {
                    report["candidate"]={{"status","unsupported"},{"error",method["error"]},
                        {"render_binding_applied",false},{"cleanup_confirmed",true},{"contact_hooks_released",true}};
                    build=false;break;
                }
        }
        if(build) {
            const auto input=directory/(mode=="bind-live-bonecloth"?"bem-bonecloth-authoring.json":"bem-cloth-authoring.json");
            if(!std::filesystem::exists(input)||std::filesystem::file_size(input)>2*1024*1024)
                throw std::runtime_error("candidate authoring data unavailable or too large");
            std::ifstream(input)>>authoring;
        }
        auto instance=scope.Method(kUnity,"UnityEngine","Object","GetInstanceID","","System.Int32",0);
        if(mode=="bind-live-pair"&&build&&!candidate&&!report.contains("candidate")) {
            try {LiveDiscoverAndStart(scope,authoring,request);}
            catch(const std::exception& e) {
                report["candidate"]={{"status","failed"},{"error",e.what()},{"render_binding_applied",false}};
                if(candidate){candidate->result=report["candidate"];candidate->finished=true;CandidateCleanup(scope);}
            }
            changed=true;
        }
        if(mode=="bind-live-bonecloth"&&build&&!candidate&&!report.contains("candidate")) {
            try {bone_cloth::DiscoverAndStart(scope,authoring);if(candidate)report["candidate"]=candidate->result;}
            catch(const std::exception& e) {
                report["candidate"]=candidate?candidate->result:Json::object();
                report["candidate"]["status"]="failed";report["candidate"]["error"]=e.what();
                if(candidate){candidate->result=report["candidate"];candidate->finished=true;CandidateCleanup(scope);}
                else BoneDetach(scope);
            }
            changed=true;
        }
        for (const auto& root:roots) {
            if (!root.game_object||root.character_id!=character||sampled.size()>=8) continue;
            const int id=scope.Int(instance,root.game_object);
            if (sampled.contains(id)) continue;
            if(mode=="verify-display-write")ProbeDisplayWriter(scope,root.game_object);
            try {report["roots"].push_back(ReadRoot(scope,root,mode=="clone-configuration",build&&!LiveMode(mode)?&authoring:nullptr));}
            catch (const std::exception& e) {report["roots"].push_back({{"resource",root.resource},{"error",e.what()}});}
            sampled.insert(id);
            changed=true;
        }
        try {changed=CandidatePoll(scope)||changed;}
        catch (const std::exception& e) {
            if(candidate) {candidate->result["status"]="failed";candidate->result["error"]=e.what();candidate->finished=true;CandidateCleanup(scope);}
            changed=true;
        }
        if(display_probe_mesh){FinishDisplayProbe(scope);changed=true;}
        if (!changed) return;
        report["status"]=sampled.empty()?"waiting-for-model":"captured";
        // Preserve the attempt's latest state across game restarts. The
        // one-shot refusal must never replace a completed/failed result.
        const char* result_key=mode=="verify-display-write"?"display_writer":"candidate";
        if(report.contains(result_key)&&!report.value("automatic_replay_refused",false)&&
                (build||mode=="verify-display-write")) {
            const auto temporary_claim=directory/"bem-cloth-candidate-consumed.tmp";
            {std::ofstream file(temporary_claim,std::ios::trunc);
                file<<Json{{"request_id",request_id},{"state","observed"},{"persistent",persistent_request},{result_key,report[result_key]}}.dump(2);
                if(!file)throw std::runtime_error("candidate result checkpoint failed");}
            std::filesystem::rename(temporary_claim,candidate_claim_path);
        }
        const auto output=directory/"bem-cloth-probe-response.json";
        const auto temporary=directory/"bem-cloth-probe-response.tmp";
        {std::ofstream file(temporary,std::ios::trunc);file<<report.dump(2);if(!file)throw std::runtime_error("probe report write failed");}
        std::filesystem::rename(temporary,output);
    } catch (const std::exception& e) {
        static std::string previous;
        if (previous!=e.what()) {previous=e.what();LogError("bem_cloth_probe",previous.c_str());}
    }
}
bool ShutdownAndroidClothProbe() {
    if(!runtime)return true;
    try {Scope scope;const bool a=CandidateCleanup(scope),b=FinishDisplayProbe(scope);return a&&b;}
    catch(const std::exception& e){LogError("bem_cloth_probe",e.what());return false;}
}
void* AndroidClothSourceMesh(void* mesh) {
    const auto& d=live_cloth::display;
    if(d&&mesh==d->mesh)return d->source;
    const auto& b=bone_cloth::binding;
    return b&&mesh==b->mesh?b->source:mesh;
}
}
