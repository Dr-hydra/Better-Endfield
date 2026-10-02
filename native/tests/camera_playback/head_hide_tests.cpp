#include "../../modules/camera/module.cpp"
#include "eiem_body_fake.h"
#include "test_support.h"
using namespace BetterEndfield::CameraModule;
namespace {
struct Object {
    bool alive=true,enabled=true,offscreen=false;
    int mode=1;
    const char* name="unknown";
    Object* parent=nullptr;
    Object* game_object=nullptr;
    Object* mesh=nullptr;
    Object* shadow=nullptr;
    std::vector<void*> bones,renderers;
};
bool boxed_bool=false,fail_mode_write=false;
int boxed_int=0,mode_writes=0;
uint32_t handle=1;
void* InvokeFake(void*,const void* method,void* instance,void** args,void** exception) {
    *exception=nullptr;
    const std::string_view key=static_cast<const char*>(method);
    auto* o=static_cast<Object*>(instance);
    if(key=="unity.object.op_equality") {
        auto* value=static_cast<Object*>(args[0]);boxed_bool=value&&!value->alive;return &boxed_bool;
    }
    if(key=="system.array.get_length") {boxed_int=int(static_cast<std::vector<void*>*>(instance)->size());return &boxed_int;}
    if(key=="system.array.get_value")return (*static_cast<std::vector<void*>*>(instance))[*static_cast<int*>(args[0])];
    if(key=="unity.object.name.get")return const_cast<char*>(o->name);
    if(key=="unity.transform.parent.get") {CHECK(o&&o->alive);return o->parent;}
    if(key=="unity.transform.child_count.get") {boxed_int=0;return &boxed_int;}
    if(key=="unity.component.game_object")return o->game_object;
    if(key=="unity.game_object.get_components")return &o->renderers;
    if(key=="unity.skinned_mesh_renderer.bones.get")return &o->bones;
    if(key=="unity.skinned_mesh_renderer.shared_mesh.get")return o->mesh;
    if(key=="unity.renderer.enabled.get") {boxed_bool=o->enabled;return &boxed_bool;}
    if(key=="unity.renderer.enabled.set") {o->enabled=*static_cast<bool*>(args[0]);return nullptr;}
    if(key=="unity.renderer.shadow_mode.get") {boxed_int=o->mode;return &boxed_int;}
    if(key=="unity.renderer.shadow_mode.set") {
        ++mode_writes;
        if(fail_mode_write){*exception=reinterpret_cast<void*>(1);return nullptr;}
        o->mode=*static_cast<int*>(args[0]);return nullptr;
    }
    if(key=="unity.mesh.vertex_count.get") {boxed_int=3;return &boxed_int;}
    return nullptr;
}
HeadPartProbe Probe(Object& renderer,const char* name="hair_01") {
    HeadPartProbe part;part.object_name=name;part.renderer=&renderer;part.matched=MatchesHeadPartToken(name);return part;
}
void Setup(BE_HostApiV1& host) {
    host.runtime_invoke=InvokeFake;host.object_unbox=[](void*,void* value){return value;};
    host.gchandle_new=[](void*,void*,int){return handle++;};host.gchandle_free=[](void*,uint32_t){};
    host.copy_managed_string=[](void*,const void* value,char* out,size_t size) {
        auto* p=static_cast<const char*>(value);std::snprintf(out,size,"%s",p);return int(std::strlen(p));
    };
    g_host=&host;for(auto& c:g_contracts){c.resolved=true;c.method_info=c.key;}
    g_fp_engine.attempted=true; // The Android no-GPU path must remain usable.
    g_first_person_hide_head=true;g_first_person.target_valid=true;
    g_skinned_mesh_renderer_class.type_object=reinterpret_cast<void*>(1);
}
}
int main() {
    BE_HostApiV1 host{};Setup(host);
    Object body,head,neck,hair,tail,tail_child,torso,mesh,renderer,go;
    body.game_object=&go;go.renderers={&renderer};renderer.mesh=&mesh;
    head.parent=&neck;neck.parent=&body;hair.parent=&head;torso.parent=&body;
    tail.name="tail_base_M_a_01_jnt";tail.parent=&body;tail_child.parent=&tail;
    g_first_person.body=&body;g_first_person.head=&head;g_first_person.neck=&neck;
    CHECK(FpBoneKind(&head)==1&&FpBoneKind(&hair)==1&&FpBoneKind(&tail_child)==1);
    CHECK(FpBoneKind(&neck)==2&&FpBoneKind(&torso)==0);
    Object detached;detached.name="tail_base_bad";CHECK(FpBoneKind(&detached)==0);
    renderer.bones={&head,&hair,&tail_child};
    FpPatch patch;
    CHECK(FpTryDirectNamedHide(Probe(renderer,"unnamed_part"),patch));
    CHECK(renderer.enabled&&renderer.mode==3&&patch.bones.size()==3);
    CHECK(FpRestore(patch)&&renderer.enabled&&renderer.mode==1);
    renderer.bones={&head,&torso};CHECK(!FpTryDirectNamedHide(Probe(renderer),patch)&&renderer.mode==1);
    renderer.bones={&head,nullptr};CHECK(!FpTryDirectNamedHide(Probe(renderer),patch));
    renderer.bones.clear();CHECK(!FpTryDirectNamedHide(Probe(renderer),patch));
    renderer.bones={&head};
    CHECK(FpShadowProxy(Probe(renderer,"hair_01_shadowProxyMobile")));
    CHECK(!FpTryDirectNamedHide(Probe(renderer,"hairshadow"),patch));
    renderer.enabled=false;CHECK(!FpTryDirectNamedHide(Probe(renderer),patch));renderer.enabled=true;
    renderer.mode=0;CHECK(FpTryDirectNamedHide(Probe(renderer),patch));
    CHECK(!renderer.enabled&&renderer.mode==0);CHECK(FpRestore(patch)&&renderer.mode==0&&renderer.enabled);
    CHECK(FpTryDirectNamedHide(Probe(renderer),patch));
    Object hidden_replacement;renderer.mesh=&hidden_replacement;
    CHECK(FpRestore(patch)&&!renderer.enabled&&renderer.mode==0); // A BEM Hidden draw owns this state.
    renderer.mesh=&mesh;renderer.enabled=true;
    renderer.mode=2;CHECK(FpTryDirectNamedHide(Probe(renderer),patch));
    renderer.mode=1;CHECK(FpRestore(patch)&&renderer.mode==1); // External update wins.
    CHECK(FpTryDirectNamedHide(Probe(renderer),patch));
    fail_mode_write=true;CHECK(!FpRestore(patch)&&patch.renderer);fail_mode_write=false;
    CHECK(FpRestore(patch)&&renderer.mode==1);
    CHECK(FpTryDirectNamedHide(Probe(renderer),patch));renderer.alive=false;
    CHECK(FpRestore(patch));renderer.alive=true;renderer.mode=1;

    // A new palette on the same renderer/mesh releases our mode. A named
    // mixed body is retained when CPU/GPU geometry is unavailable.
    CHECK(FpTryDirectNamedHide(Probe(renderer),patch));
    g_fp_mesh_session.patches.push_back(std::move(patch));renderer.bones={&head,&torso};
    g_first_person_pump_frames=100;EnsureNeckCap();
    CHECK(renderer.mode==1&&renderer.enabled&&g_fp_mesh_session.patches.empty());

    // The same mesh can acquire its skeleton after two unavailable scans.
    renderer.bones.clear();g_fp_mesh_session={};
    g_first_person_pump_frames=200;EnsureNeckCap();g_first_person_pump_frames=240;EnsureNeckCap();
    CHECK(g_fp_mesh_session.patches.empty());renderer.bones={&head};
    g_first_person_pump_frames=280;EnsureNeckCap();
    CHECK(renderer.mode==3&&g_fp_mesh_session.patches.size()==1);ReleaseNeckCap();
    CHECK(renderer.mode==1);

    auto& engine=g_fp_engine;
    engine.get_mesh=[](void* o)->void*{return static_cast<Object*>(o)->mesh;};
    engine.set_mesh=[](void* o,void* mesh){static_cast<Object*>(o)->mesh=static_cast<Object*>(mesh);};
    engine.get_shadow=[](void* o)->void*{return static_cast<Object*>(o)->shadow;};
    engine.set_shadow=[](void* o,void* mesh){static_cast<Object*>(o)->shadow=static_cast<Object*>(mesh);};
    engine.get_offscreen=[](void* o){return static_cast<Object*>(o)->offscreen;};
    engine.set_offscreen=[](void* o,bool value){static_cast<Object*>(o)->offscreen=value;};
    Object a,clipped,b,proxy;head.name="Bip001_Head";
    auto Topology=[&] {
        FpPatch p;p.renderer=FpRoot(&renderer);p.source=FpRoot(&a);p.copy=FpRoot(&clipped);
        p.bones={&head};p.bone_names={head.name};p.assigned=true;p.assigned_shadow=&a;
        return p;
    };
    // A->B outside Camera must retire A's shadow, not resurrect an old proxy.
    patch=Topology();renderer.mesh=&b;renderer.shadow=&a;renderer.offscreen=true;
    CHECK(FpRestore(patch)&&renderer.mesh==&b&&renderer.shadow==&b&&!renderer.offscreen);
    // An external shadow write wins even when Camera's copy is still bound.
    patch=Topology();renderer.mesh=&clipped;renderer.shadow=&proxy;renderer.bones={&head};
    CHECK(FpRestore(patch)&&renderer.mesh==&a&&renderer.shadow==&proxy);
    // Palette order/semantics changed without a mesh handoff: retain ownership
    // until a compatible palette or a game/BEM mesh update allows restoration.
    patch=Topology();renderer.mesh=&clipped;renderer.shadow=&a;renderer.bones={&torso};
    CHECK(!FpRestore(patch)&&renderer.mesh==&clipped&&patch.copy);
    renderer.mesh=&b;CHECK(FpRestore(patch)&&renderer.shadow==&b);
    Object new_head;new_head.name=head.name;
    patch=Topology();renderer.mesh=&clipped;renderer.bones={&new_head};renderer.shadow=&a;
    CHECK(FpRestore(patch)&&renderer.mesh==&a&&renderer.shadow==nullptr);

    // Real bridge representation, including stride4's implicit first weight.
    const float positions[]{0,0,0,1,0,0,0,1,0,2,0,0,3,0,0,2,1,0};
    uint8_t skin[]{1,250,250,250,1,0,0,0,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0};
    const uint32_t indices[]{0,1,2,3,4,5};const BE_CustomModelDrawV1 draw{0,6};
    BE_CustomModelGeometryV1 view{sizeof(view),1,6,6,positions,skin,4,indices,&draw,1};
    FpCpuGeometry cpu;CHECK(FpCaptureCpuGeometry(&cpu,&view)==BE_Result_Ok);
    CHECK(cpu.vertices[0].weight[0]==1&&cpu.vertices[0].weight[1]==0&&cpu.vertices[0].bone[1]==0);
    auto result=FpMesh::Build(cpu.vertices,cpu.parts,{0,1},{},false,false);
    CHECK(result.error.empty()&&result.hidden_triangles==1);
    CHECK(result.parts[0].indices==std::vector<uint32_t>({0,0,0,3,4,5}));
    uint8_t packed[6*12]{};
    for(int i=0;i<6;++i){uint16_t weight=65535;std::memcpy(packed+i*12,&weight,2);packed[i*12+8]=i<3?1:0;}
    view.skin=packed;view.skin_stride=12;CHECK(FpCaptureCpuGeometry(&cpu,&view)==BE_Result_Ok);
    CHECK(cpu.vertices[0].bone[0]==1&&cpu.vertices[0].weight[0]==1);
    uint8_t full[6*32]{};
    for(int i=0;i<6;++i){float weight=1;uint32_t bone=i<3?1:0;std::memcpy(full+i*32,&weight,4);std::memcpy(full+i*32+16,&bone,4);}
    view.skin=full;view.skin_stride=32;CHECK(FpCaptureCpuGeometry(&cpu,&view)==BE_Result_Ok);
    CHECK(cpu.vertices[0].bone[0]==1&&cpu.vertices[0].weight[0]==1);
    float invalid=std::numeric_limits<float>::quiet_NaN();std::memcpy(full,&invalid,4);
    CHECK(FpCaptureCpuGeometry(&cpu,&view)==BE_Result_InvalidArgument);
    view.skin=skin;view.skin_stride=4;view.vertex_count=2;
    CHECK(FpCaptureCpuGeometry(&cpu,&view)==BE_Result_InvalidArgument);
    view.vertex_count=6;view.version=2;CHECK(FpCaptureCpuGeometry(&cpu,&view)==BE_Result_InvalidArgument);
    CHECK(FpCaptureCpuGeometry(nullptr,&view)==BE_Result_InvalidArgument);
    CHECK(mode_writes>5);g_host=nullptr;
    std::cout<<"PASS production hair/shadow ownership and CPU fallback: "<<checks<<" checks\n";
}
