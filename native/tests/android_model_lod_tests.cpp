// Exercise the exact Android LodState and pump entry without Unity or a device.
#include "BetterEndfield/ModuleApi.h"
#include "../modules/custom_model/mod_registry.h"
#include <algorithm>
#include <array>
#include <atomic>
#include <cstring>
#include <iostream>
#include <stdexcept>

namespace {
bool pipeline_config=true;
struct Pipeline { float bias=1.0f;unsigned enables=0,disables=0;bool reject_enable=false; };
Pipeline* current_pipeline=nullptr;
unsigned quality_reads=0,quality_writes=0,static_accesses=0,checks=0;
void Check(bool ok,const char* why) {++checks;if (!ok) throw std::runtime_error(why);}
}
namespace betterendfield {
bool AndroidPipelineLodEnabled() {return pipeline_config;}
bool AndroidNpcParametersEnabled() {return false;}
}
namespace BetterEndfield::CustomModel {
struct Float2 {float x,y;};
struct WeakManagedReference {
    void* object=nullptr;
    void Reset() {object=nullptr;}
    bool Set(void* value) {object=value;return value!=nullptr;}
    void* Get() const {return object;}
};
using StaticFieldFn=void(*)(const void*,void*);
StaticFieldFn g_static_get=nullptr,g_static_set=nullptr;
bool SafeStaticField(StaticFieldFn,const void*,void*) {++static_accesses;return false;}
struct LodField {
    const char* name;const char* type;size_t size;
    BE_ResolvedFieldV1 resolved{};
    std::array<uint8_t,8> original{},desired{};
};
const BE_HostApiV1* g_host=nullptr;
std::atomic_bool g_lod_bias_locked{false};
void Log(const std::string&) {}
const char* Contract(const char* key) {return key;}
void* Invoke(const char* key,void*,void**) {
    Check(std::string_view(key)=="pipeline.current","unexpected object contract in Android LOD maintenance");
    return current_pipeline;
}
bool InvokeVoid(const char* key,void* object,void**) {
    auto* pipeline=static_cast<Pipeline*>(object);
    if (std::string_view(key)=="pipeline.enable_force_lod0") {
        Check(pipeline!=nullptr,"LOD enable lost its pipeline");++pipeline->enables;
        if (pipeline->reject_enable) return false;
        pipeline->bias=1e-7f;return true;
    }
    if (std::string_view(key)=="pipeline.disable_force_lod0") {
        Check(pipeline!=nullptr,"LOD restore lost its pipeline");++pipeline->disables;
        pipeline->bias=1.0f;return true;
    }
    if (std::string_view(key)=="quality.set_max_lod") {++quality_writes;return true;}
    throw std::runtime_error("unexpected write contract in Android LOD maintenance");
}
template<class T> bool InvokeValue(const char* key,void*,void**,T& value) {
    if (std::string_view(key)=="quality.get_max_lod") {++quality_reads;value=2;return true;}
    throw std::runtime_error("unexpected value contract in Android LOD maintenance");
}
// This test intentionally compiles the production Android branch on the host.
#if !defined(__ANDROID__)
#define BEM_TEST_DEFINED_ANDROID
#define __ANDROID__
#endif
#include "../modules/custom_model/model_lod_state.inc"
#if defined(BEM_TEST_DEFINED_ANDROID)
#undef __ANDROID__
#undef BEM_TEST_DEFINED_ANDROID
#endif
}

int main() {
    try {
        using namespace BetterEndfield::CustomModel;
        for (const int route:{0,1,2}) {
            const bool explicit_resource=route!=0,static_mesh=route==2;
            const std::array<ComponentIdentity,1> identities{{{"synthetic_mobile_lod1",6,"Mesh_all/lod1/part",static_mesh}}};
            CharacterAdapter adapter{"synthetic","synthetic_mobile","synthetic_mobile","",false,identities,
                "mobile","assets/synthetic_mobile.prefab",1,explicit_resource};
            ModRegistry registry;EnabledMod enabled;enabled.adapter=&adapter;registry.enabled.push_back(enabled);
            Pipeline pipeline;current_pipeline=&pipeline;pipeline_config=true;LodState lod;
            Check(lod.MaintainAndroid(false,registry) && lod.active && lod.applied && g_lod_bias_locked &&
                pipeline.enables==1 && pipeline.bias==1e-7f,"enabled legacy or explicit LOD1 model lost pipeline bias");
            Check(lod.MaintainAndroid(false,registry) && pipeline.enables==1,"stable Android pipeline was needlessly reapplied");
            pipeline_config=false;
            Check(lod.MaintainAndroid(false,registry) && !lod.active && !g_lod_bias_locked && pipeline.disables==1 &&
                pipeline.bias==1.0f,"configuration off did not restore original Android pipeline state");
            pipeline_config=true;Check(lod.MaintainAndroid(false,registry),"configuration on failed to restore bias");
            registry.enabled.clear();
            Check(lod.MaintainAndroid(false,registry) && !lod.active && pipeline.disables==2,"disabling all packages did not release bias");
            registry.enabled.push_back(enabled);Check(lod.MaintainAndroid(false,registry),"reenabling model failed");
            Check(lod.MaintainAndroid(true,registry) && !lod.active && pipeline.disables==3 && !g_lod_bias_locked,
                "module stop did not release bias for an enabled model");
            Check(!quality_reads && !quality_writes && !static_accesses,"Android LOD1 maintenance touched QualitySettings or disabled NPC overrides");
        }
        {
            CharacterAdapter adapter{"synthetic","mobile","mobile","",false,{},"mobile","",1,true};
            ModRegistry registry;EnabledMod enabled;enabled.adapter=&adapter;registry.enabled.push_back(enabled);
            Pipeline first,second;current_pipeline=&first;pipeline_config=true;LodState lod;
            Check(lod.MaintainAndroid(false,registry),"initial pipeline setup failed");
            current_pipeline=&second;
            Check(lod.MaintainAndroid(false,registry) && first.disables==1 && first.bias==1.0f && second.enables==1,
                "new Android pipeline did not restore the previous instance and adopt the bias");
            Check(lod.MaintainAndroid(true,registry),"new pipeline stop failed");
            second.reject_enable=true;
            Check(!lod.MaintainAndroid(false,registry) && !lod.active && !g_lod_bias_locked && second.bias==1.0f,
                "failed pipeline contract left an active bias lock");
        }
        Check(!quality_reads && !quality_writes && !static_accesses,"Android LOD maintenance escaped its pipeline-only contract");
        std::cout<<"PASS "<<checks<<" Android production LOD maintenance checks: legacy/explicit LOD1, static/skinned, disable/config/stop, pipeline replacement and failure\n";
        return 0;
    } catch (const std::exception& error) {std::cerr<<error.what()<<'\n';return 1;}
}
