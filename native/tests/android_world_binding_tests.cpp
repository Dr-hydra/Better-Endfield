// Exercise the production Android world adapter with managed-object stand-ins.
// No game, Android device or graphics driver is needed for these route checks.
#include "BetterEndfield/ModuleApi.h"
#include "../modules/custom_model/bem.h"
#include "../modules/custom_model/mod_registry.h"
#include "../modules/custom_model/resource_policy.h"
#include "../modules/custom_model/generic_model_matcher.h"
#include "../modules/custom_model/texture_binding_policy.h"
#include <atomic>
#include <cmath>
#include <iostream>
#include <memory>
#include <stdexcept>
#include <unordered_map>
#include <type_traits>
#include <limits>

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
    int32_t submeshes=1;
    int32_t shadow_mode=0;
    void* shadow_mesh=nullptr;
    bool static_renderer=false;
    bool enabled=true;
    uint32_t indices=7;
};
std::vector<std::unique_ptr<Object>> objects;
std::unordered_map<std::string,Object*> resource_roots,transform_nodes;
std::unordered_map<void*,void*> pristine_meshes;
std::unordered_map<void*,std::pair<int32_t,void*>> saved_shadow_states;
std::vector<void*> known_custom_receivers;
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
size_t ui_prepare_calls=0;
size_t checks=0;
void Check(bool value,const char* message) { ++checks; if (!value) throw std::runtime_error(message); }
}
namespace betterendfield {
void* AndroidLoadUiDonor(const char*,void*&,uint32_t&) { return ui_donor; }
void AndroidReleaseUiDonor(void*&,uint32_t&) {}
}
namespace BetterEndfield::CustomModel {
struct PreparedBinding {
    GenericMatching::ReceiverKey receiver_key;
    uint32_t component_id=0;
    void* renderer=nullptr;
    void* original_mesh=nullptr;
    void* original_bones=nullptr;
    void* original_materials=nullptr;
    void* donor_mesh=nullptr;
    void* donor_materials=nullptr;
    void* custom_mesh=nullptr;
    void* custom_bones=nullptr;
    void* custom_materials=nullptr;
    bool saved_original=false,donor_enabled=true,original_enabled=true,custom_enabled=true;
    bool change_shadow=false;
    int32_t original_shadow=0,custom_shadow=0;
    void* original_shadow_mesh=nullptr;
    void* custom_shadow_mesh=nullptr;
    std::vector<std::string> bone_names;
};
std::vector<PreparedBinding> ui_sources;
std::atomic_bool g_hot_switch_runtime{false};
BE_HostApiV1 host{};
const BE_HostApiV1* g_host=&host;
BE_ResolvedClassV1 g_skinned_renderer_class{};
bool IsStaticRenderer(void* object) {return object && static_cast<Object*>(object)->static_renderer;}
bool IsModelRenderer(void* object) {return object!=nullptr;}
void* ModelRendererType() {return renderer_type;}
void* GetRendererMesh(void* object) {return static_cast<Object*>(object)->mesh;}
void* GetRendererBones(void* object) {return IsStaticRenderer(object)?nullptr:static_cast<Object*>(object)->bones;}
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
    if (name=="game_object.get_transform" || name=="component.get_transform") return value;
    if (name=="transform.get_parent") {
        if (!object) return nullptr;
        const auto slash=object->path.rfind('/');
        if (slash==object->path.npos) return nullptr;
        const auto parent_path=object->path.substr(0,slash);
        if (auto it=resource_roots.find(parent_path);it!=resource_roots.end()) return it->second;
        auto [it,inserted]=transform_nodes.emplace(parent_path,nullptr);
        if (inserted) it->second=Make(parent_path.substr(parent_path.rfind('/')+1),parent_path);
        return it->second;
    }
    if (name=="game_object.renderers") return args[0]==transform_type?object->transforms:object->renderers;
    if (name=="skinned.get_shared_mesh") return object->mesh;
    if (name=="skinned.get_bones") return object->bones;
    if (name=="renderer.get_shared_materials") return object->materials;
    return nullptr;
}
template<class T> bool InvokeValue(const char* key,void* value,void**,T& output) {
    if (!value) return false;
    if constexpr(std::is_same_v<T,Matrix4x4Raw>) output=static_cast<Object*>(value)->matrix;
    else if (std::string_view(key)=="mesh.get_sub_mesh_count") output=static_cast<T>(static_cast<Object*>(value)->submeshes);
    else if (std::string_view(key)=="mesh.get_index_count") output=static_cast<T>(static_cast<Object*>(value)->indices);
    else if (std::string_view(key)=="android.shadow_get") output=static_cast<T>(static_cast<Object*>(value)->shadow_mode);
    else output=0;
    return true;
}
bool GetRendererEnabled(void* value,bool& enabled) { enabled=static_cast<Object*>(value)->enabled; return true; }
bool IsNativeObjectAlive(void* value) { return value!=nullptr; }
bool saved_world_materials_unloaded=false;
bool UseSavedOriginal(void*,PreparedBinding& binding,bool require_materials=true) {
    if (auto it=pristine_meshes.find(binding.renderer);it!=pristine_meshes.end()) {
        binding.donor_mesh=it->second; binding.saved_original=true;
        // Production keeps Original materials weak; unloaded ones resolve to null.
        binding.donor_materials=saved_world_materials_unloaded?nullptr:binding.original_materials;
        if (!binding.donor_materials && require_materials) return false;
    }
    return true;
}
void* DonorMesh(const PreparedBinding& binding) { return binding.donor_mesh?binding.donor_mesh:binding.original_mesh; }
void* DonorMaterials(const PreparedBinding& binding) { return binding.donor_materials?binding.donor_materials:binding.original_materials; }
void* DonorBones(const PreparedBinding& binding) { return binding.original_bones; }
thread_local std::array<void*,2> g_verified_mesh_space_roots{};
thread_local bool g_instance_rebind_active=false;
void LogMeshSpaceDifference(void*,void*) {}
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
    ++ui_prepare_calls; output=ui_sources; return true;
}
#include "../modules/custom_model/generic_model_matcher.inc"
bool ReadGenericPristineMesh(const CharacterAdapter&,void*,const GenericRendererCandidate& candidate,
    GenericMatching::MeshIdentity& identity) {
    identity={};
    if (auto it=pristine_meshes.find(candidate.renderer);it!=pristine_meshes.end()) {
        identity.origin=GenericMatching::DonorOrigin::SavedOriginal; identity.mesh=it->second;
    } else if (std::find(known_custom_receivers.begin(),known_custom_receivers.end(),candidate.renderer)!=known_custom_receivers.end()) {
        identity.origin=GenericMatching::DonorOrigin::CompletedWithoutOriginal; return true;
    } else { identity.origin=GenericMatching::DonorOrigin::Pristine; identity.mesh=candidate.mesh; }
    const auto mesh=identity.mesh; const auto origin=identity.origin;
    return ReadGenericMeshIdentity(mesh,origin,identity);
}
#include "../../android/app/src/main/cpp/modules/custom_model/world_resource_adapter.inc"
bool shadow_contracts=true,shadow_getter=true;
bool AndroidShadowContractsAvailable() {return shadow_contracts;}
bool ReadNullableObject(const char*,void* object,void*& value) {value=static_cast<Object*>(object)->shadow_mesh;return shadow_getter;}
void* PristineShadowMesh(const PreparedBinding& binding) {
    if (auto it=saved_shadow_states.find(binding.renderer);it!=saved_shadow_states.end()) return it->second.second;
    return binding.original_shadow_mesh;
}
bool RestoreSavedShadowState(PreparedBinding& binding) {
    auto it=saved_shadow_states.find(binding.renderer);if (it==saved_shadow_states.end()) return false;
    binding.change_shadow=true;binding.custom_shadow=it->second.first;binding.custom_shadow_mesh=it->second.second;return true;
}
#include "../modules/custom_model/explicit_android_shadows.inc"
}
namespace {
using namespace BetterEndfield::CustomModel;
struct Fixture {
    std::array<ComponentIdentity,1> identities{{{"Body_lod0",3}}};
    std::string component_name,world_name,ui_name;
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
        ui_donor=Make(adapter.ui_resource); cached=false; ui_prepare_calls=0; g_hot_switch_runtime=false;
        pristine_meshes.clear(); saved_shadow_states.clear(); known_custom_receivers.clear();
        world->path=world->name; ui_donor->path=ui_donor->name;
        resource_roots[world->path]=world; resource_roots[ui_donor->path]=ui_donor;
        world->renderers=Array({world_renderer}); world->transforms=Array({world_bone});
        world_renderer->mesh=Make("Body_lod1"); world_renderer->bones=Array({world_bone});
        world_renderer->materials=Array({world_material});
        PreparedBinding source;
        source.renderer=ui_renderer; source.original_mesh=Make("Body_lod0");
        source.original_materials=Array({ui_material}); source.original_bones=Array({ui_bone});
        source.custom_mesh=Make("Replacement"); source.custom_materials=Array({Make("Copy")});
        source.custom_bones=Array({ui_bone}); ui_sources={source};
        ui_renderer->mesh=source.original_mesh; ui_renderer->bones=source.original_bones;
        ui_renderer->materials=source.original_materials;
        bem.components.resize(1); bem.components[0].bone_names={"Bone"};
    }
    void KeepTexture() {
        bem.components[0].keep_material_overrides={{0,0,1}};
        bem.components[0].keep_material_names={"Material"};
        bem.textures.resize(1); bem.textures[0].original_name="OriginalTexture";
        auto* texture=Make("OriginalTexture"); world_material->slots={{texture}};
    }
    void Target(const char* character,std::string name,std::string renderer_name={}) {
        component_name=std::move(name); world_name=std::string(character)+"_postmodel";
        ui_name=std::string(character)+"_uimodel";
        identities[0].name=component_name.c_str(); adapter.id=character;
        adapter.world_resource=world_name.c_str(); adapter.ui_resource=ui_name.c_str();
        world->name=world_name; ui_donor->name=ui_name;
        world->path=world_name; ui_donor->path=ui_name;
        resource_roots[world_name]=world; resource_roots[ui_name]=ui_donor;
        if (renderer_name.empty()) renderer_name=component_name;
        auto lod1=renderer_name; lod1.back()='1';
        world_renderer->name=lod1; world_renderer->path=world_name+"/Mesh_all/lod1/"+lod1;
        static_cast<Object*>(world_renderer->mesh)->name=lod1;
        ui_renderer->name=renderer_name; ui_renderer->path=ui_name+"/Mesh_all/lod0/"+renderer_name;
        static_cast<Object*>(ui_sources[0].original_mesh)->name=component_name;
        ui_bone->path=ui_name+"/Root/Bone"; world_bone->path=world_name+"/Root/Bone";
    }
    bool Run() { bindings.clear(); return PrepareAndroidWorldResource(adapter,bem,world,bindings); }
    Object* Proxy(std::string name="Body_shadowProxyMobile",bool shared=true) {
        auto* value=Make(name,world->path+"/Shadow_Proxy/SP_Mobile/"+name);
        value->mesh=shared?world_renderer->mesh:Make(ObjectName(world_renderer->mesh));
        value->bones=Array({world_bone}); value->materials=Array({world_material});
        static_cast<Object*>(world->renderers)->array.push_back(value); return value;
    }
    void Explicit() {
        adapter.explicit_resource=true;adapter.ui_resource=adapter.world_resource;
        PreparedBinding b;b.renderer=world_renderer;b.original_mesh=world_renderer->mesh;
        b.original_bones=world_renderer->bones;b.original_materials=b.custom_materials=world_renderer->materials;
        b.original_enabled=world_renderer->enabled;b.custom_enabled=!(bem.components[0].info.flags&kComponentFlagHidden);
        UseSavedOriginal(world,b);
        b.custom_mesh=(bem.components[0].info.flags&kComponentFlagNoGeometry)?DonorMesh(b):Make("Explicit replacement");bindings={b};
    }
};
void ExplicitShadowChecks() {
    const auto run=[](Fixture& f){return PrepareExplicitAndroidShadows(f.adapter,f.bem,f.world,f.bindings);};
    {
        Fixture f;f.Explicit();auto* original=Make("proxy field");f.world_renderer->shadow_mesh=original;f.world_renderer->shadow_mode=2;
        Check(run(f) && f.bindings[0].change_shadow && f.bindings[0].original_shadow_mesh==original &&
            !f.bindings[0].custom_shadow_mesh && f.bindings[0].custom_shadow==2,"explicit receiver proxy was not snapshotted/cleared with original mode");
        Check(f.world_renderer->shadow_mesh==original,"shadow preparation mutated a live receiver");
        auto apply=[](PreparedBinding& b){auto* r=static_cast<Object*>(b.renderer);r->shadow_mesh=b.custom_shadow_mesh;r->shadow_mode=b.custom_shadow;return false;};
        auto restore=[](PreparedBinding& b){auto* r=static_cast<Object*>(b.renderer);r->shadow_mesh=b.original_shadow_mesh;r->shadow_mode=b.original_shadow;return true;};
        Check(CommitResource<PreparedBinding>(f.bindings,apply,restore)==CommitResult::Restored &&
            f.world_renderer->shadow_mesh==original && f.world_renderer->shadow_mode==2,"explicit shadow snapshots did not restore after partial failure");
    }
    {
        Fixture f;f.Explicit();auto* proxy=f.Proxy("arbitrary_name");
        Check(run(f) && f.bindings.size()==2 && f.bindings[1].renderer==proxy && !f.bindings[1].custom_enabled &&
            f.bindings[0].custom_shadow==1,"same-object Mesh/bones/space proxy owner was not prepared for transactional disable");
    }
    {
        Fixture f;f.Explicit();f.Proxy("Body_shadowProxyMobile",false);
        Check(!run(f) && f.bindings.size()==1 && !f.bindings[0].change_shadow,"reduced proxy was guessed from a matching name or changed a refused transaction");
    }
    {
        Fixture f;f.Explicit();auto* proxy=f.Proxy("reduced",false);f.world_renderer->shadow_mesh=proxy->mesh;
        Check(run(f) && f.bindings.size()==2,"live shadowProxyMesh reference did not prove reduced proxy ownership");
    }
    {
        Fixture f;f.Explicit();f.Proxy();f.bindings.push_back(f.bindings[0]);f.bem.components.push_back(f.bem.components[0]);
        Check(!run(f),"shared proxy Mesh with two receiver owners was resolved by order");
    }
    {
        Fixture f;f.Explicit();f.Proxy();auto* sibling=Make("other",f.world->path+"/Mesh_all/lod1/other");
        sibling->mesh=f.world_renderer->mesh;sibling->bones=f.world_renderer->bones;
        static_cast<Object*>(f.world->renderers)->array.push_back(sibling);
        Check(!run(f),"proxy owner uniqueness ignored a receiver outside the package component subset");
    }
    {
        Fixture f;f.Explicit();auto* proxy=f.Proxy("unrelated",false);proxy->bones=Array({Make("other bone")});
        Check(run(f) && f.bindings.size()==1,"unrelated skeleton's proxy blocked the explicit resource");
    }
    {
        Fixture f;f.Explicit();auto* proxy=f.Proxy();proxy->bones=Array({Make("wrong bone")});
        Check(!run(f),"shared Mesh accepted a proxy with different bones");
    }
    {
        Fixture f;f.Explicit();auto* proxy=f.Proxy();proxy->matrix.m[12]=1;
        Check(!run(f),"shared Mesh accepted a proxy in another mesh space");
    }
    {
        Fixture f;f.Explicit();auto* proxy=f.Proxy("kept-part",false);
        auto* kept=Make("kept",f.world->path+"/Mesh_all/lod1/kept");kept->mesh=proxy->mesh;kept->bones=proxy->bones;
        PreparedBinding b=f.bindings[0];b.renderer=kept;b.original_mesh=kept->mesh;b.original_bones=kept->bones;f.bindings.push_back(b);
        BemComponent c;c.info.flags=kComponentFlagNoGeometry;f.bem.components.push_back(c);
        Check(run(f) && f.bindings.size()==2,"proxy proven to belong to an unchanged component blocked a different replacement");
    }
    {
        Fixture f;f.Explicit();auto* proxy=f.Proxy();f.world_renderer->static_renderer=proxy->static_renderer=true;
        f.bindings[0].original_bones=nullptr;proxy->bones=nullptr;
        Check(run(f) && f.bindings.size()==2,"static proxy identity wrongly required a skin palette");
    }
    {
        Fixture f;f.Explicit();shadow_getter=false;
        Check(!run(f) && !f.bindings[0].change_shadow,"unreadable nullable shadow getter accepted as an empty proxy");shadow_getter=true;
        shadow_contracts=false;Check(!run(f),"missing shadow setters accepted");shadow_contracts=true;
    }
    {
        Fixture f;auto* proxy=f.Proxy("shared-shape");auto* source_mesh=f.world_renderer->mesh;
        f.world_renderer->shadow_mesh=proxy->mesh;f.world_renderer->shadow_mode=2;f.Explicit();
        const auto apply=[](PreparedBinding& b) {
            auto* r=static_cast<Object*>(b.renderer);r->mesh=b.custom_mesh;r->enabled=b.custom_enabled;
            if (b.change_shadow) {r->shadow_mode=b.custom_shadow;r->shadow_mesh=b.custom_shadow_mesh;}return true;
        };
        const auto restore=[](PreparedBinding& b) {
            auto* r=static_cast<Object*>(b.renderer);r->mesh=b.original_mesh;r->enabled=b.original_enabled;
            if (b.change_shadow) {r->shadow_mode=b.original_shadow;r->shadow_mesh=b.original_shadow_mesh;}return true;
        };
        const auto publish=[&] {
            Check(run(f),"explicit shadow transition preparation refused");
            for (const auto& b:f.bindings) {
                pristine_meshes.emplace(b.renderer,b.original_mesh);
                if (b.change_shadow) saved_shadow_states.emplace(b.renderer,std::pair{b.original_shadow,b.original_shadow_mesh});
            }
            Check(CommitResource<PreparedBinding>(f.bindings,apply,restore)==CommitResult::Committed,"explicit shadow transition did not commit");
        };
        publish();auto first_transaction=f.bindings;
        Check(!proxy->enabled && !f.world_renderer->shadow_mesh,"replace did not clear old shadows");
        f.bem.components[0].info.flags=kComponentFlagNoGeometry;f.Explicit();publish();
        Check(proxy->enabled && f.world_renderer->shadow_mesh==source_mesh && f.world_renderer->shadow_mode==2,
            "replace to keep left old proxy disabled or shadowProxyMesh cleared");
        f.bem.components[0].info.flags=0;f.Explicit();publish();
        f.bem.components[0].info.flags=kComponentFlagNoGeometry|kComponentFlagHidden;f.Explicit();publish();
        Check(!proxy->enabled && !f.world_renderer->enabled,"hide left a source shadow caster enabled");
        f.bem.components[0].info.flags=kComponentFlagNoGeometry;f.Explicit();publish();
        Check(proxy->enabled && f.world_renderer->enabled && f.world_renderer->shadow_mesh==source_mesh,
            "replace to hide to keep did not restore original shadow state");
        // The Original snapshots retained on first publication are also the
        // data consumed by disable; verify they survive all appearances.
        for (auto& b:first_transaction) restore(b);
        Check(proxy->enabled && f.world_renderer->mesh==source_mesh && f.world_renderer->shadow_mesh==source_mesh &&
            f.world_renderer->shadow_mode==2,"original shadow snapshots no longer restore after appearance transitions");
    }
}
}
int main() {
    try {
        using namespace BetterEndfield::CustomModel;
        host.resolve_class=[](void*,const char*,const char*,const char*,BE_ResolvedClassV1* type)->BE_Result {
            type->type_object=transform_type; return BE_Result_Ok;
        };
        g_skinned_renderer_class.type_object=renderer_type;
        ExplicitShadowChecks();
        {
            Fixture fixture; Check(fixture.Run(),"baseline world binding failed");
            Check(ArrayValue(fixture.bindings[0].custom_bones,0)==fixture.world_bone,"world renderer retained UI bones");
            Check(fixture.bindings[0].change_shadow && fixture.bindings[0].custom_shadow==1,"replacement lost its shadow");
        }
        for (bool hot_switch:{false,true}) {
            Fixture fixture; g_hot_switch_runtime=hot_switch;
            std::vector<PreparedBinding> paired_ui; void* paired_asset=nullptr;
            Check(PrepareAndroidWorldResource(fixture.adapter,fixture.bem,fixture.world,fixture.bindings,
                &paired_ui,&paired_asset),"world-first paired preparation failed");
            Check(paired_asset==ui_donor && paired_ui.size()==ui_sources.size(),
                "fresh world-first UI donor was not returned for publication in normal/hot-switch mode");
            Check(ui_prepare_calls==1,"world-first prepared its UI donor more than once");
            Check(fixture.bindings[0].custom_mesh==paired_ui[0].custom_mesh &&
                fixture.bindings[0].custom_materials==paired_ui[0].custom_materials,
                "world and paired UI did not share the freshly built mesh/material/texture graph");
            Check(ArrayValue(fixture.bindings[0].custom_bones,0)==fixture.world_bone &&
                ArrayValue(paired_ui[0].custom_bones,0)==fixture.ui_bone,
                "paired publication mixed the world and UI live skeletons");
            auto transaction=fixture.bindings;
            transaction.insert(transaction.end(),paired_ui.begin(),paired_ui.end());
            auto apply=[](PreparedBinding& binding) {
                auto* renderer=static_cast<Object*>(binding.renderer);
                renderer->mesh=binding.custom_mesh; renderer->bones=binding.custom_bones;
                renderer->materials=binding.custom_materials; return true;
            };
            auto restore=[](PreparedBinding& binding) {
                auto* renderer=static_cast<Object*>(binding.renderer);
                renderer->mesh=binding.original_mesh; renderer->bones=binding.original_bones;
                renderer->materials=binding.original_materials; return true;
            };
            Check(CommitResource<PreparedBinding>(transaction,apply,restore)==CommitResult::Committed &&
                fixture.world_renderer->mesh==fixture.ui_renderer->mesh &&
                fixture.world_renderer->materials==fixture.ui_renderer->materials,
                "paired commit did not publish the shared graph to both resources");
            Check(CommitResource<PreparedBinding>(transaction,[&](PreparedBinding& binding) {
                apply(binding); return binding.renderer!=fixture.ui_renderer;
            },restore)==CommitResult::Restored,"paired transaction did not roll back a mutating UI receiver");
            Check(fixture.world_renderer->mesh==fixture.bindings[0].original_mesh &&
                fixture.ui_renderer->mesh==paired_ui[0].original_mesh &&
                fixture.world_renderer->materials==fixture.bindings[0].original_materials &&
                fixture.ui_renderer->materials==paired_ui[0].original_materials &&
                fixture.world_renderer->bones==fixture.bindings[0].original_bones &&
                fixture.ui_renderer->bones==paired_ui[0].original_bones,
                "paired rollback retained replacement mesh/material/bones on world or UI");
        }
        {
            Fixture fixture; cached=true;
            std::vector<PreparedBinding> paired_ui; void* paired_asset=nullptr;
            Check(PrepareAndroidWorldResource(fixture.adapter,fixture.bem,fixture.world,fixture.bindings,
                &paired_ui,&paired_asset),"cached world-first donor preparation failed");
            Check(ui_prepare_calls==0 && paired_ui.empty() && !paired_asset,
                "cached committed UI donor was rebuilt or republished");
            Check(fixture.bindings[0].custom_mesh==ui_sources[0].custom_mesh &&
                fixture.bindings[0].custom_materials==ui_sources[0].custom_materials,
                "cached world route did not reuse the committed UI graph");
        }
        {
            Fixture fixture; cached=true;
            // Normal-mode completed UI donor has current custom output but no
            // pristine strong donor. Its package action still proves geometry
            // was generated; names/pointer comparisons cannot do that.
            ui_sources[0].original_mesh=ui_sources[0].custom_mesh;
            Check(fixture.Run() && fixture.bindings[0].change_shadow &&
                fixture.bindings[0].custom_mesh==ui_sources[0].custom_mesh,
                "completed UI custom Mesh was mistaken for an unchanged pristine donor");
        }
        {
            Fixture fixture;
            static_cast<Object*>(fixture.world_renderer->mesh)->name="Body_lod1_8";
            Check(!fixture.Run(),"Android world accepted an unverified _8 target identity by stripping digits");
            Check(fixture.bindings.empty(),"failed world identity check returned partial bindings");
            Fixture unchecked;
            unchecked.bem.skip_validation=true;
            static_cast<Object*>(unchecked.world_renderer->mesh)->name="Body_lod1_8";
            Check(unchecked.Run(),"unchecked Android world did not use the bounded _8 fallback");
            auto* ambiguous=Make("Body_lod1_20",unchecked.world->path+"/Mesh_all/lod1/Body_lod1_20");
            ambiguous->mesh=Make("Body_lod1_20"); ambiguous->bones=Array({unchecked.world_bone});
            ambiguous->materials=Array({unchecked.world_material});
            static_cast<Object*>(unchecked.world->renderers)->array.push_back(ambiguous);
            Check(!unchecked.Run(),"unchecked Android world accepted an ambiguous bounded fallback");
            Fixture relation_fixture;
            static_cast<Object*>(relation_fixture.world_renderer->mesh)->name="Body_lod1_8";
            GenericMatching::ExactLodRelation relation{{"Android","test-snapshot"},"chr_test",
                fixture.adapter.ui_resource,fixture.adapter.world_resource,"Mesh_all/lod0/Body_lod0","Body_lod0",
                "Mesh_all/lod1/Body_lod1","Body_lod1_8",true,true,7,true};
            const auto rows=std::span<const GenericMatching::ExactLodRelation>(&relation,1);
            auto run=[&](GenericMatching::AssetScope scope) {
                relation_fixture.bindings.clear();
                return PrepareAndroidWorldResource(relation_fixture.adapter,relation_fixture.bem,relation_fixture.world,relation_fixture.bindings,
                    nullptr,nullptr,rows,scope);
            };
            Check(run({"Android","test-snapshot"}) && relation_fixture.bindings[0].renderer==relation_fixture.world_renderer,
                "validated exact Android relationship could not use an independent _8 Mesh identity");
            Check(!run({"Android",""}) && !run({"Windows","test-snapshot"}) && !run({"Android","old-snapshot"}),
                "unknown/foreign/stale asset scope enabled an Android _8 relationship");
            relation.target_indices=3; // UI original count must not be applied to world.
            Check(!run({"Android","test-snapshot"}),"world original count mismatch ignored in exact relationship");
            relation.target_indices=7;
            relation.target_path="Mesh_all/lod1/x";
            relation_fixture.world_renderer->name="x"; relation_fixture.world_renderer->path=relation_fixture.world->path+"/Mesh_all/lod1/x";
            Check(run({"Android","test-snapshot"}),"exact real relationship still required a guessed target Renderer suffix");
        }
        {
            Fixture fixture; auto* proxy=fixture.Proxy("UnrelatedRendererName");
            Check(fixture.Run() && fixture.bindings.size()==2 && fixture.bindings.back().renderer==proxy &&
                !fixture.bindings.back().custom_enabled,"proxy with a real original Mesh reference was guessed by Renderer name");
            auto* desktop=Make("Body_shadowProxyDesktop",fixture.world->path+"/Shadow_Proxy/SP_Desktop/Body_shadowProxyDesktop");
            desktop->mesh=proxy->mesh; desktop->materials=proxy->materials; desktop->bones=proxy->bones;
            static_cast<Object*>(fixture.world->renderers)->array.push_back(desktop);
            fixture.Proxy("SecondMobileReference");
            Check(fixture.Run() && fixture.bindings.size()==3,"multiple verified mobile proxies or desktop isolation failed");
            for (const auto& binding:fixture.bindings)
                Check(binding.renderer!=desktop,"mobile policy disabled a desktop proxy");
        }
        {
            // Device SP_Mobile proxies carry their own reduced Mesh; the exact
            // dedicated path derived from the validated LOD1 target binds it.
            Fixture fixture; auto* proxy=fixture.Proxy("Body_shadowProxyMobile",false);
            Check(fixture.Run() && fixture.bindings.size()==2 && fixture.bindings.back().renderer==proxy &&
                !fixture.bindings.back().custom_enabled,"exact dedicated mobile proxy path was not bound");
        }
        {
            Fixture fixture; fixture.Proxy("Other_shadowProxyMobile",false);
            Check(fixture.Run() && fixture.bindings.size()==1,"dedicated proxy with a non-matching path was bound");
        }
        {
            Fixture fixture; auto* proxy=fixture.Proxy();
            auto* second=Make("Other_lod1",fixture.world->path+"/Mesh_all/lod1/Other_lod1");
            second->mesh=proxy->mesh; second->bones=fixture.world_renderer->bones;
            second->materials=fixture.world_renderer->materials;
            static_cast<Object*>(fixture.world->renderers)->array.push_back(second);
            Check(!fixture.Run(),"proxy owner ambiguity ignored a visible component absent from the BEM");
        }
        {
            Fixture fixture; fixture.Proxy();
            fixture.bem.components[0].info.flags=kComponentFlagNoGeometry;
            Check(fixture.Run() && fixture.bindings.size()==1 && !fixture.bindings[0].change_shadow &&
                fixture.bindings[0].custom_mesh==fixture.world_renderer->mesh,
                "material-only replacement changed the world geometry/shadow policy");
            known_custom_receivers.push_back(static_cast<Object*>(fixture.world->renderers)->array.back());
            Check(fixture.Run(),"material-only replacement inspected an untouched custom proxy donor");
            known_custom_receivers.clear();
            fixture.bem.components[0].info.flags|=kComponentFlagHidden;
            Check(fixture.Run() && fixture.bindings.size()==2 && !fixture.bindings[0].custom_enabled &&
                !fixture.bindings[1].custom_enabled,"hidden material-only component left its verified proxy enabled");
        }
        {
            Fixture fixture; known_custom_receivers.push_back(fixture.world_renderer);
            Check(!fixture.Run(),"known world custom Mesh used as pristine because its full name matched");
            auto* original=fixture.world_renderer->mesh;
            fixture.world_renderer->mesh=Make("Body_lod1");
            pristine_meshes[fixture.world_renderer]=original;
            Check(fixture.Run() && fixture.bindings[0].original_mesh==fixture.world_renderer->mesh &&
                DonorMesh(fixture.bindings[0])==original,"saved world Original overwrote the current rollback Mesh");
            auto* proxy=fixture.Proxy();
            pristine_meshes[proxy]=original;
            Check(fixture.Run() && fixture.bindings.size()==2,
                "cached world/proxy owner relationship used current custom Mesh pointers");
        }
        {
            // diag6 regression: the world receiver shows the previous package;
            // its Original Mesh is pinned, its Original materials were unloaded.
            // A world hot switch takes materials from the UI donor and must still
            // proceed; only keep-material validation needs world Originals.
            Fixture fixture; auto* original=fixture.world_renderer->mesh;
            fixture.world_renderer->mesh=Make("previous-package");
            pristine_meshes[fixture.world_renderer]=original; saved_world_materials_unloaded=true;
            Check(fixture.Run() && DonorMesh(fixture.bindings[0])==original &&
                fixture.bindings[0].custom_mesh==ui_sources[0].custom_mesh &&
                fixture.bindings[0].custom_materials==ui_sources[0].custom_materials &&
                fixture.bindings[0].original_mesh==fixture.world_renderer->mesh,
                "world hot switch refused after its unpinned Original materials were unloaded");
            fixture.KeepTexture();
            Check(!fixture.Run(),"keep override validated against the previous package instead of unloaded world Originals");
            saved_world_materials_unloaded=false;
            Check(fixture.Run(),"keep override with live world Original materials refused");
            pristine_meshes.clear();
        }
        {
            Fixture fixture;
            fixture.world->path="Scene/WorldInstance/"+fixture.world->name;
            resource_roots[fixture.world->path]=fixture.world;
            fixture.world_renderer->path=fixture.world->path+"/Mesh_all/lod1/Body_lod1";
            fixture.world_bone->path=fixture.world->path+"/Root/Bone";
            ui_donor->path="Scene/UiInstance/"+ui_donor->name; resource_roots[ui_donor->path]=ui_donor;
            fixture.ui_renderer->path=ui_donor->path+"/Mesh_all/lod0/Body_lod0";
            fixture.ui_bone->path=ui_donor->path+"/Root/Bone";
            Check(fixture.Run(),"scene parents changed the true world/UI resource-relative identity");
            fixture.world_renderer->path="OtherRoot/Mesh_all/lod1/Body_lod1";
            Check(!fixture.Run(),"Renderer outside the actual resource root was accepted");
        }
        {
            Fixture fixture;
            fixture.world->name+="(Clone)";
            Check(fixture.Run(),"natural root clone lost the exact resource route");
        }
        {
            Fixture fixture; fixture.world_bone->matrix.m[3]=std::numeric_limits<float>::quiet_NaN();
            Check(!fixture.Run(),"nonfinite bone transform was accepted as a rest-pose difference");
            fixture.bem.skip_validation=true;
            Check(!fixture.Run(),"unchecked world uploaded a nonfinite live bone transform");
        }
        {
            Fixture fixture; Check(fixture.Run() && ui_prepare_calls==1,
                "world adapter without paired output arguments changed its donor preparation semantics");
            std::vector<PreparedBinding> paired_ui;
            Check(PrepareAndroidWorldResource(fixture.adapter,fixture.bem,fixture.world,fixture.bindings,
                &paired_ui,nullptr) && paired_ui.empty(),"incomplete paired output arguments published a UI binding");
            void* paired_asset=nullptr;
            Check(PrepareAndroidWorldResource(fixture.adapter,fixture.bem,fixture.world,fixture.bindings,
                nullptr,&paired_asset) && !paired_asset,"incomplete paired output arguments published a UI asset");
        }
        // Exact GameObject names captured from the current PC/Android prefabs
        // on 2026-10-03. These are asset evidence, not display-name guesses.
        for (const auto* stem:{"body_01","cloth_01","cloth_02","cloth_03","cloth_04","cloth_05",
                "eyebrow_01","eyeshadow_01","face_01","furcard_01","furcard_02","hair_01","hair_02","hs_01","iris_01"}) {
            Fixture fixture; fixture.Target("chr_0006_wolfgd",std::string("S_actor_wolfgd_")+stem+"_lod0");
            Check(fixture.Run(),"current Wolfgd UI name did not bind its exact Android LOD1 renderer");
            Check(fixture.bindings[0].renderer==fixture.world_renderer,"Wolfgd bound a different world renderer");
        }
        for (const auto* stem:{"body_01","brow_01","cloth_01","cloth_02","cloth_03","cloth_04","cloth_05",
                "cloth_06","cloth_07","eyeshadow_01","face_01","hair_01","hairshadow_01","iris_01"}) {
            Fixture fixture; fixture.Target("chr_0034_typhoea",std::string("S_actor_typhoea_")+stem+"_lod0");
            Check(fixture.Run(),"current Typhoea UI name did not bind its exact Android LOD1 renderer");
        }
        for (const auto* stem:{"vfxpart_01","vfxpart_02","vfxpart_03"}) {
            Fixture fixture; fixture.Target("chr_0034_typhoea",std::string("S_actor_Typhoea_")+stem+"_lod0");
            Check(fixture.Run(),"current mixed-case Typhoea VFX name was normalized");
            fixture.world_renderer->name[8]='t';
            Check(!fixture.Run(),"world renderer matching ignored the captured asset-name case");
        }
        for (const auto* stem:{"body_01","brow_01","cloth_01","cloth_02","cloth_03","cloth_05","cloth_06",
                "eyeshadow_01","face_01","hair_01","hair_02","hairshadow_01","iris_01","vfxpart_01"}) {
            Fixture fixture; fixture.Target("chr_0025_ardelia",std::string("S_actor_ardelia_")+stem+"_lod0");
            Check(fixture.Run(),"current Ardelia UI renderer did not bind its exact Android LOD1 counterpart");
        }
        {
            Fixture fixture; fixture.Target("chr_0025_ardelia","S_actor_ardelia_fur_01_lod0_20","S_actor_ardelia_fur_01_lod0");
            Check(std::string_view(fixture.identities[0].name)=="S_actor_ardelia_fur_01_lod0_20" &&
                ObjectName(ui_sources[0].original_mesh)==fixture.identities[0].name,
                "Ardelia renderer mapping changed the package/source Mesh identity");
            auto* proxy=Make("S_actor_ardelia_fur_01_shadowProxyMobile",
                fixture.world_name+"/Shadow_Proxy/SP_Mobile/S_actor_ardelia_fur_01_shadowProxyMobile");
            proxy->mesh=fixture.world_renderer->mesh; proxy->materials=Array({fixture.world_material});
            proxy->bones=Array({fixture.world_bone}); fixture.world->renderers=Array({fixture.world_renderer,proxy});
            Check(fixture.Run(),"Ardelia's suffixed source Mesh did not bind its unsuffixed world renderer");
            Check(fixture.bindings.size()==2 && fixture.bindings[0].renderer==fixture.world_renderer &&
                fixture.bindings[1].renderer==proxy && !fixture.bindings[1].custom_enabled,
                "Ardelia fur replacement did not disable its exact Mobile shadow proxy");
            cached=true; Check(fixture.Run(),"cached Ardelia donor lost its mapped world renderer");
            fixture.world->name="chr_0006_wolfgd_postmodel";
            Check(!fixture.Run(),"world adapter accepted another character resource root");
        }
        for (const auto* name:{"S_actor_ardelia_fur_01_lod0_2","S_actor_ardelia_fur_01_lod0_21",
                "S_actor_other_fur_01_lod0_20","S_actor_Ardelia_fur_01_lod0_20"}) {
            Check(ComponentRendererName("chr_0025_ardelia",name)==name,
                "component renderer mapping stripped an unverified suffix or normalized a name");
        }
        {
            Fixture fixture; fixture.Target("chr_0006_wolfgd","S_actor_wolfgd_hs_01_lod0");
            fixture.world_renderer->path=fixture.world_name+"/Mesh_all/lod2/"+fixture.world_renderer->name;
            Check(!fixture.Run(),"world route accepted a same-name renderer from another LOD");
            fixture.world_renderer->path=fixture.world_name+"/Shadow_Proxy/SP_Mobile/"+fixture.world_renderer->name;
            Check(!fixture.Run(),"world route accepted a same-name shadow proxy");
            fixture.world_renderer->path=fixture.world_name+"/Mesh_all/lod1/"+fixture.world_renderer->name;
            auto* duplicate=Make(fixture.world_renderer->name,fixture.world_renderer->path);
            fixture.world->renderers=Array({fixture.world_renderer,duplicate});
            Check(!fixture.Run(),"validated world route accepted two exact renderer candidates");
        }
        {
            Fixture fixture; fixture.Target("chr_0034_typhoea","S_actor_typhoea_cloth_01_lod0");
            const std::string parent="Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/skirt_base_L_c_01_jnt/skirt_base_L_c_02_jnt/";
            fixture.ui_bone->name="skirt_base_L_c_03_jnt";
            fixture.ui_bone->path=fixture.ui_name+"/"+parent+fixture.ui_bone->name;
            fixture.world_bone->name="skirt_base_R_c_03_jnt";
            fixture.world_bone->path=fixture.world_name+"/"+parent+fixture.world_bone->name;
            fixture.bem.components[0].bone_names={fixture.ui_bone->name};
            Check(!fixture.Run(),"Typhoea skirt bone was guessed without a declared alias");
            fixture.bem.components[0].bone_aliases={{fixture.world_bone->name}};
            fixture.bem.components[0].bone_aliases_by_resource.resize(1);
            fixture.bem.components[0].bone_aliases_by_resource[0][0]={fixture.world_bone->name};
            Check(fixture.Run(),"declared Typhoea skirt alias did not bind at its captured parent path");
            Check(ArrayValue(fixture.bindings[0].custom_bones,0)==fixture.world_bone,"Typhoea skirt retained a UI bone");
            fixture.world_bone->path=fixture.world_name+"/Unrelated/"+fixture.world_bone->name;
            Check(!fixture.Run(),"Typhoea alias accepted a same-name bone under another parent");
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
            fixture.bem.components[0].bone_aliases_by_resource.resize(1);
            fixture.bem.components[0].bone_aliases_by_resource[0][0]={"WorldBone"};
            Check(fixture.Run(),"validated world stopped resolving declared BEM bone aliases");
            Check(ArrayValue(fixture.bindings[0].custom_bones,0)==fixture.world_bone,"declared alias did not bind the live world bone");
        }
        {
            Fixture fixture;
            fixture.world_bone->name="WorldBone"; fixture.world_bone->path="chr_test_postmodel/Root/WorldBone";
            auto* other=Make("OtherAlias","chr_test_postmodel/Root/OtherAlias");
            fixture.world->transforms=Array({fixture.world_bone,other});
            fixture.bem.components[0].bone_aliases={{"WorldBone","OtherAlias"}};
            fixture.bem.components[0].bone_aliases_by_resource.resize(1);
            fixture.bem.components[0].bone_aliases_by_resource[0][0]={"WorldBone","OtherAlias"};
            Check(!fixture.Run(),"two valid world aliases were selected by alias order");
            fixture.bem.skip_validation=true;
            Check(!fixture.Run(),"unchecked world selected a live bone from ambiguous alias paths");
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
            Fixture fixture; static_cast<Object*>(fixture.world_renderer->mesh)->submeshes=0;
            Check(!fixture.Run(),"world donor with no valid submesh identity was accepted");
            static_cast<Object*>(fixture.world_renderer->mesh)->submeshes=257;
            Check(!fixture.Run(),"world donor exceeded the checked native submesh bounds");
        }
        {
            Fixture fixture; fixture.KeepTexture(); fixture.world_material->name="DifferentMaterial";
            Check(!fixture.Run(),"validated world ignored keep-material identity");
            fixture.bem.skip_validation=true;
            Check(fixture.Run(),"unchecked world still validates keep-material identity");
        }
        {
            Fixture fixture; fixture.KeepTexture();
            fixture.ui_material->name="M_actor_typhoea_face_01";
            fixture.world_material->name="M_actor_lod_typhoea_face_01";
            fixture.bem.components[0].keep_material_names={fixture.ui_material->name};
            fixture.world_material->slots.clear();
            Check(fixture.Run(),"validated world rejected the native Android face LOD counterpart");
            Check(fixture.bindings[0].custom_materials==ui_sources[0].custom_materials,
                "LOD counterpart did not receive the validated UI material copy");
            fixture.world_material->name="M_actor_lod_other_face_01";
            Check(!fixture.Run(),"LOD counterpart rule accepted a different character's material");
        }
        for (const auto* suffix:{"aglina_hair_01","pelica_body_01","purrche_cloth_01"}) {
            Fixture fixture; fixture.KeepTexture();
            fixture.ui_material->name=std::string("M_actor_")+suffix;
            fixture.world_material->name=std::string("M_actor_lod_")+suffix;
            fixture.bem.components[0].keep_material_names={fixture.ui_material->name};
            fixture.world_material->slots.clear();
            Check(fixture.Run(),"native LOD counterpart rule was restricted to Typhoea or the face component");
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
        std::cout<<"PASS "<<checks<<" Android world route, paired publication/rollback, current prefab names, shared texture, live bones and shadow checks\n";
        return 0;
    } catch (const std::exception& error) { std::cerr<<error.what()<<'\n'; return 1; }
}
