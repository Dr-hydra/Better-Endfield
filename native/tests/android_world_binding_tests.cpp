// Exercise the production Android world adapter with managed-object stand-ins.
// No game, Android device or graphics driver is needed for these route checks.
#include "BetterEndfield/ModuleApi.h"
#include "../modules/custom_model/bem.h"
#include "../modules/custom_model/mod_registry.h"
#include "../modules/custom_model/texture_binding_policy.h"
#include <atomic>
#include <cmath>
#include <iostream>
#include <memory>
#include <stdexcept>
#include <unordered_map>
#include <type_traits>

namespace {
struct Matrix4x4Raw { float m[16]{}; };
struct Slot { void* texture=nullptr; };
struct Object {
    std::string name,path;
    std::vector<void*> array;
    std::vector<Slot> slots;
    void* mesh=nullptr;
    void* bones=nullptr;
    void* materials=nullptr;
    void* renderers=nullptr;
    void* transforms=nullptr;
    Matrix4x4Raw matrix{};
};
std::vector<std::unique_ptr<Object>> objects;
Object* Make(std::string name={},std::string path={}) {
    auto value=std::make_unique<Object>(); value->name=std::move(name); value->path=std::move(path);
    for (int i=0;i<4;++i) value->matrix.m[i*5]=1;
    auto* result=value.get(); objects.push_back(std::move(value)); return result;
}
Object* Array(std::initializer_list<void*> values) { auto* result=Make(); result->array=values; return result; }
Object* ui_donor=nullptr;
Object* transform_type=Make("TransformType");
Object* renderer_type=Make("RendererType");
bool cached=false;
size_t checks=0;
void Check(bool value,const char* message) { ++checks; if (!value) throw std::runtime_error(message); }
}
namespace betterendfield {
void* AndroidLoadUiDonor(const char*,void*&,uint32_t&) { return ui_donor; }
void AndroidReleaseUiDonor(void*&,uint32_t&) {}
}
namespace BetterEndfield::CustomModel {
struct PreparedBinding {
    uint32_t component_id=0;
    void* renderer=nullptr;
    void* original_mesh=nullptr;
    void* original_bones=nullptr;
    void* original_materials=nullptr;
    void* custom_mesh=nullptr;
    void* custom_bones=nullptr;
    void* custom_materials=nullptr;
    bool saved_original=false,donor_enabled=true,original_enabled=true,custom_enabled=true;
    bool change_shadow=false;
    int32_t original_shadow=0,custom_shadow=0;
    void* original_shadow_mesh=nullptr;
    std::vector<std::string> bone_names;
};
std::vector<PreparedBinding> ui_sources;
std::atomic_bool g_hot_switch_runtime{false};
BE_HostApiV1 host{};
const BE_HostApiV1* g_host=&host;
BE_ResolvedClassV1 g_skinned_renderer_class{};
struct Construction { bool failed=false; } construction;
Construction* g_construction=&construction;
void Log(const std::string&) {}
void* RootTemporary(void* value) { return value; }
std::string ObjectName(void* value) { return value?static_cast<Object*>(value)->name:std::string{}; }
std::string BuildTransformPath(void* value) { return value?static_cast<Object*>(value)->path:std::string{}; }
int ArrayLength(void* value) { return value?static_cast<int>(static_cast<Object*>(value)->array.size()):0; }
void* ArrayValue(void* value,int index) { return static_cast<Object*>(value)->array.at(index); }
const char* Contract(const char* key) { return key; }
void* Invoke(const char* key,void* value,void** args) {
    auto* object=static_cast<Object*>(value); const std::string_view name=key;
    if (name=="game_object.renderers") return args[0]==transform_type?object->transforms:object->renderers;
    if (name=="skinned.get_shared_mesh") return object->mesh;
    if (name=="skinned.get_bones") return object->bones;
    if (name=="renderer.get_shared_materials") return object->materials;
    return nullptr;
}
template<class T> bool InvokeValue(const char*,void* value,void**,T& output) {
    if (!value) return false;
    if constexpr(std::is_same_v<T,Matrix4x4Raw>) output=static_cast<Object*>(value)->matrix;
    else output=0;
    return true;
}
bool GetRendererEnabled(void*,bool& enabled) { enabled=true; return true; }
bool UseSavedOriginal(void*,PreparedBinding&) { return true; }
void* DonorMesh(const PreparedBinding& binding) { return binding.original_mesh; }
void* DonorMaterials(const PreparedBinding& binding) { return binding.original_materials; }
void* DonorBones(const PreparedBinding& binding) { return binding.original_bones; }
bool SameMeshSpace(void* a,void* b) {
    const auto& left=static_cast<Object*>(a)->matrix;
    const auto& right=static_cast<Object*>(b)->matrix;
    for (int i=0;i<16;++i) if (std::abs(left.m[i]-right.m[i])>0.0001f) return false;
    return true;
}
std::vector<Slot> ReadMaterialTextureSlots(void* material) { return static_cast<Object*>(material)->slots; }
void LogTexturePinFailure(const char*,void*,const std::vector<Slot>&,const std::string&) {}
void* NewArrayLike(void* original,int count,bool skip_validation=false) {
    if (!original || count<=0 || (!skip_validation && count>256)) return nullptr;
    auto* result=Make(); result->array.resize(count); return result;
}
bool SetArrayValue(void* array,int index,void* object) {
    if (!array || !object || index<0 || index>=ArrayLength(array)) return false;
    static_cast<Object*>(array)->array[index]=object; return true;
}
bool ReadCompletedAndroidDonor(const CharacterAdapter&,const BemPocData&,void*,std::vector<PreparedBinding>& output) {
    if (!cached) return false; output=ui_sources; return true;
}
bool PrepareResource(const CharacterAdapter&,const BemPocData&,void*,std::vector<PreparedBinding>& output) {
    output=ui_sources; return true;
}
#include "../../android/app/src/main/cpp/modules/custom_model/world_resource_adapter.inc"
}
namespace {
using namespace BetterEndfield::CustomModel;
struct Fixture {
    std::array<ComponentIdentity,1> identities{{{"Body_lod0",3}}};
    CharacterAdapter adapter{"chr_test","chr_test_postmodel","chr_test_uimodel","",false,identities};
    BemPocData bem;
    Object* world=Make(adapter.world_resource);
    Object* world_renderer=Make("Body_lod1","chr_test_postmodel/Mesh_all/lod1/Body_lod1");
    Object* ui_renderer=Make("Body_lod0","chr_test_uimodel/Mesh_all/lod0/Body_lod0");
    Object* ui_bone=Make("Bone","chr_test_uimodel/Root/Bone");
    Object* world_bone=Make("Bone","chr_test_postmodel/Root/Bone");
    Object* ui_material=Make("Material");
    Object* world_material=Make("Material");
    std::vector<PreparedBinding> bindings;
    Fixture() {
        ui_donor=Make(adapter.ui_resource); cached=false; g_hot_switch_runtime=false;
        world->renderers=Array({world_renderer}); world->transforms=Array({world_bone});
        world_renderer->mesh=Make("Body_lod1"); world_renderer->bones=Array({world_bone});
        world_renderer->materials=Array({world_material});
        PreparedBinding source;
        source.renderer=ui_renderer; source.original_mesh=Make("Body_lod0");
        source.original_materials=Array({ui_material}); source.original_bones=Array({ui_bone});
        source.custom_mesh=Make("Replacement"); source.custom_materials=Array({Make("Copy")});
        source.custom_bones=Array({ui_bone}); ui_sources={source};
        bem.components.resize(1); bem.components[0].bone_names={"Bone"};
    }
    void KeepTexture() {
        bem.components[0].keep_material_overrides={{0,0,1}};
        bem.components[0].keep_material_names={"Material"};
        bem.textures.resize(1); bem.textures[0].original_name="OriginalTexture";
        auto* texture=Make("OriginalTexture"); world_material->slots={{texture}};
    }
    bool Run() { bindings.clear(); return PrepareAndroidWorldResource(adapter,bem,world,bindings); }
};
}
int main() {
    try {
        using namespace BetterEndfield::CustomModel;
        host.resolve_class=[](void*,const char*,const char*,const char*,BE_ResolvedClassV1* type)->BE_Result {
            type->type_object=transform_type; return BE_Result_Ok;
        };
        g_skinned_renderer_class.type_object=renderer_type;
        {
            Fixture fixture; Check(fixture.Run(),"baseline world binding failed");
            Check(ArrayValue(fixture.bindings[0].custom_bones,0)==fixture.world_bone,"world renderer retained UI bones");
            Check(fixture.bindings[0].change_shadow && fixture.bindings[0].custom_shadow==1,"replacement lost its shadow");
        }
        {
            Fixture fixture; fixture.KeepTexture();
            fixture.world_material->slots.push_back(fixture.world_material->slots.front());
            Check(fixture.Run(),"UI accepts a shared source texture, but Android world still rejects multiple slots");
            cached=true;
            Check(fixture.Run(),"cached UI donor changed shared texture acceptance");
        }
        {
            Fixture fixture; fixture.KeepTexture();
            fixture.world_material->slots.push_back({Make("OriginalTexture")});
            Check(!fixture.Run(),"validated world accepted distinct same-name textures");
            fixture.bem.skip_validation=true;
            Check(fixture.Run(),"unchecked world still rejects distinct same-name texture pins");
        }
        {
            Fixture fixture; fixture.bem.components[0].bone_names={"PackageBone"};
            Check(!fixture.Run(),"validated world ignored a bone name mismatch");
            fixture.bem.skip_validation=true;
            Check(fixture.Run(),"unchecked world still validates package bone names");
            Check(ArrayValue(fixture.bindings[0].custom_bones,0)==fixture.world_bone,"unchecked world did not bind the mapped live bone");
        }
        {
            Fixture fixture; fixture.world_bone->name="WorldBone";
            fixture.world_bone->path="chr_test_postmodel/Root/WorldBone";
            fixture.bem.components[0].bone_aliases={{"WorldBone"}};
            Check(fixture.Run(),"validated world stopped resolving declared BEM bone aliases");
            Check(ArrayValue(fixture.bindings[0].custom_bones,0)==fixture.world_bone,"declared alias did not bind the live world bone");
        }
        {
            Fixture fixture; fixture.world_renderer->matrix.m[12]=5;
            Check(!fixture.Run(),"validated world ignored a renderer-space mismatch");
            fixture.bem.skip_validation=true;
            Check(fixture.Run(),"unchecked world still validates renderer space");
        }
        {
            Fixture fixture; static_cast<Object*>(fixture.world_renderer->mesh)->name="ChangedMesh";
            Check(!fixture.Run(),"validated world ignored source mesh identity");
            fixture.bem.skip_validation=true;
            Check(fixture.Run(),"unchecked world still validates source mesh identity");
        }
        {
            Fixture fixture; fixture.KeepTexture(); fixture.world_material->name="DifferentMaterial";
            Check(!fixture.Run(),"validated world ignored keep-material identity");
            fixture.bem.skip_validation=true;
            Check(fixture.Run(),"unchecked world still validates keep-material identity");
        }
        {
            Fixture fixture; fixture.bem.components[0].bone_names.resize(257,"Bone");
            static_cast<Object*>(ui_sources[0].custom_bones)->array.resize(257,fixture.ui_bone);
            Check(!fixture.Run(),"validated world ignored the palette capacity limit");
            fixture.bem.skip_validation=true;
            Check(fixture.Run() && ArrayLength(fixture.bindings[0].custom_bones)==257,"unchecked world still applies the palette capacity limit");
        }
        {
            Fixture fixture; fixture.bem.skip_validation=true;
            fixture.world_bone->path="chr_test_postmodel/Unrelated/Bone";
            Check(!fixture.Run(),"unchecked world guessed a bone outside the mapped skeleton path");
        }
        {
            Fixture fixture; fixture.KeepTexture(); fixture.world_material->slots.clear();
            Check(!fixture.Run(),"validated world ignored a missing texture pin");
            fixture.bem.skip_validation=true;
            Check(fixture.Run(),"unchecked world still rejects a missing texture pin");
            fixture.bem.components[0].keep_material_overrides[0].material_slot=1;
            Check(!fixture.Run(),"unchecked world accessed a material outside the live slot array");
        }
        std::cout<<"PASS "<<checks<<" Android world route, shared texture, unchecked identity/capacity, live bones and shadow checks\n";
        return 0;
    } catch (const std::exception& error) { std::cerr<<error.what()<<'\n'; return 1; }
}
