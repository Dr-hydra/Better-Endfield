#include "../../modules/camera/module.cpp"
#include "eiem_body_fake.h"
#include "test_support.h"
#include <deque>
using namespace BetterEndfield::CameraModule;
namespace {
struct Object {
    bool alive=true,enabled=true,offscreen=false;
    int mode=1,vertex_count=3;
    float scale=1;
    const char* name="unknown";
    Object* parent=nullptr;
    Object* game_object=nullptr;
    Object* transform=nullptr;
    Object* mesh=nullptr;
    Object* shadow=nullptr;
    std::vector<void*> bones,renderers;
};
bool boxed_bool=false,fail_mode_write=false,fail_vertex_count_read=false;
int boxed_int=0,mode_writes=0;
uint32_t handle=1;
Vector3 boxed_vector{};
// Unity returns a fresh Transform[] from get_bones; created GameObjects are
// self-transforms so the anchor tests can observe parent/scale writes.
std::deque<std::vector<void*>> bone_arrays;
std::deque<Object> created;
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
    if(key=="unity.game_object.transform")return o->transform;
    if(key=="unity.game_object.get_components")return &o->renderers;
    if(key=="unity.skinned_mesh_renderer.bones.get"){bone_arrays.push_back(o->bones);return &bone_arrays.back();}
    if(key=="fp.renderer.bones.set"){o->bones=*static_cast<std::vector<void*>*>(args[0]);return nullptr;}
    if(key=="fp.array.clone"){bone_arrays.push_back(*static_cast<std::vector<void*>*>(instance));return &bone_arrays.back();}
    if(key=="fp.array.set_value"){(*static_cast<std::vector<void*>*>(instance)).at(*static_cast<int*>(args[1]))=args[0];return nullptr;}
    if(key=="fp.game_object.ctor"){o->name=static_cast<const char*>(args[0]);o->transform=o;o->game_object=o;return nullptr;}
    if(key=="fp.transform.set_parent"){o->parent=static_cast<Object*>(args[0]);return nullptr;}
    if(key=="fp.transform.local_position.set"||key=="fp.transform.local_rotation.set")return nullptr;
    if(key=="fp.transform.local_scale.set"){o->scale=static_cast<Vector3*>(args[0])->x;return nullptr;}
    if(key=="fp.transform.local_scale.get"){boxed_vector={o->scale,o->scale,o->scale};return &boxed_vector;}
    if(key=="unity.transform.find") {
        for(auto& item:created)if(item.alive&&item.parent==o&&std::string_view(item.name)==static_cast<const char*>(args[0]))return &item;
        return nullptr;
    }
    if(key=="unity.skinned_mesh_renderer.shared_mesh.get")return o->mesh;
    if(key=="unity.renderer.enabled.get") {boxed_bool=o->enabled;return &boxed_bool;}
    if(key=="unity.renderer.enabled.set") {o->enabled=*static_cast<bool*>(args[0]);return nullptr;}
    if(key=="unity.renderer.shadow_mode.get") {boxed_int=o->mode;return &boxed_int;}
    if(key=="unity.renderer.shadow_mode.set") {
        ++mode_writes;
        if(fail_mode_write){*exception=reinterpret_cast<void*>(1);return nullptr;}
        o->mode=*static_cast<int*>(args[0]);return nullptr;
    }
    if(key=="unity.mesh.vertex_count.get") {
        if(fail_vertex_count_read){*exception=reinterpret_cast<void*>(1);return nullptr;}
        boxed_int=o->vertex_count;return &boxed_int;
    }
    return nullptr;
}
HeadPartProbe Probe(Object& renderer,const char* name="hair_01") {
    HeadPartProbe part;part.object_name=name;part.renderer=&renderer;part.matched=MatchesHeadPartToken(name);return part;
}
void Setup(BE_HostApiV1& host) {
    host.runtime_invoke=InvokeFake;host.object_unbox=[](void*,void* value){return value;};
    host.gchandle_new=[](void*,void*,int){return handle++;};host.gchandle_free=[](void*,uint32_t){};
    host.string_new=[](void*,const char* value)->void*{return const_cast<char*>(value);};
    host.object_new=[](void*,const void*)->void*{created.emplace_back();return &created.back();};
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
    Object hat,brim,foreign_head;
    hat.name="hat_base_M_a_01_jnt";hat.parent=&head;brim.name="brim_base_L_a_01_jnt";brim.parent=&hat;
    foreign_head.name="Bip001_Head";foreign_head.parent=&body;
    CHECK(FpBoneKind(&hat)==1&&FpBoneKind(&brim)==1&&FpBoneKind(&foreign_head)==0);
    renderer.bones={&hat,&brim};
    FpPatch accessory;
    CHECK(FpTryDirectNamedHide(Probe(renderer,"cloth_02"),accessory));
    CHECK(renderer.mode==3&&FpRestore(accessory)&&renderer.mode==1);
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
    // A pure Head hairshadow helper is hidden like other standalone parts; a
    // mixed one is never cropped or palette-collapsed.
    CHECK(FpTryDirectNamedHide(Probe(renderer,"hairshadow"),patch)&&renderer.mode==3);
    CHECK(FpRestore(patch)&&renderer.mode==1);
    renderer.bones={&head,&torso};CHECK(!FpTryDirectNamedHide(Probe(renderer,"hairshadow"),patch));
    renderer.bones={&head};
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

    // A failed optional count read must not filter an unhinted, proven Head
    // accessory before the no-GPU direct path. Another renderer stays mixed.
    Object mixed_renderer;mixed_renderer.mesh=&mesh;mixed_renderer.bones={&head,&torso};
    renderer.name="cloth_03";renderer.bones={&hat,&brim};go.renderers={&renderer,&mixed_renderer};
    fail_vertex_count_read=true;g_first_person_pump_frames=320;EnsureNeckCap();
    CHECK(renderer.mode==3&&renderer.enabled&&g_fp_mesh_session.patches.size()==1);
    g_first_person_pump_frames=360;EnsureNeckCap();
    CHECK(mixed_renderer.mode==1&&mixed_renderer.enabled);
    ReleaseNeckCap();CHECK(renderer.mode==1);fail_vertex_count_read=false;go.renderers={&renderer};
    mesh.vertex_count=0;g_first_person_pump_frames=400;EnsureNeckCap();
    CHECK(renderer.mode==1&&g_fp_mesh_session.patches.empty());mesh.vertex_count=3;ReleaseNeckCap();

    // Reparenting can complete a late accessory binding without replacing any
    // palette object. Renew exhausted retries, then release it if it leaves Head.
    Object late_hat;late_hat.name="hat_base_late_jnt";late_hat.parent=&torso;
    renderer.bones={&late_hat};g_first_person_pump_frames=500;EnsureNeckCap();
    g_first_person_pump_frames=540;EnsureNeckCap();g_first_person_pump_frames=580;EnsureNeckCap();
    CHECK(renderer.mode==1&&g_fp_mesh_session.patches.empty());late_hat.parent=&head;
    g_first_person_pump_frames=620;EnsureNeckCap();
    CHECK(renderer.mode==3&&g_fp_mesh_session.patches.size()==1);late_hat.parent=&torso;
    g_first_person_pump_frames=660;EnsureNeckCap();
    CHECK(renderer.mode==1&&renderer.enabled&&g_fp_mesh_session.patches.empty());ReleaseNeckCap();
    renderer.mode=0;late_hat.parent=&head;g_first_person_pump_frames=700;EnsureNeckCap();
    CHECK(!renderer.enabled&&g_fp_mesh_session.patches.size()==1);late_hat.parent=&torso;
    g_first_person_pump_frames=740;EnsureNeckCap();
    CHECK(renderer.enabled&&renderer.mode==0&&g_fp_mesh_session.patches.empty());ReleaseNeckCap();
    renderer.mode=1;renderer.bones={&head};

    // No readable geometry (Android original mesh, no GPU readback): a
    // non-casting mixed costume collapses only its Head-subtree palette entries
    // onto one private zero-scale child of Head. Neck, body and tail entries stay.
    {
        g_fp_game_object_class.class_info=reinterpret_cast<void*>(2);
        Object costume,costume_mesh;costume.mesh=&costume_mesh;costume.mode=0;costume.name="cloth_02";
        costume.bones={&torso,&hair,&neck,&head,&tail_child};go.renderers={&costume};
        const auto original=costume.bones;
        g_fp_mesh_session={};g_first_person_pump_frames=800;EnsureNeckCap();
        CHECK(g_fp_mesh_session.patches.size()==1&&g_fp_mesh_session.patches[0].redirected);
        CHECK(created.size()==2&&created[0].parent==&head&&created[0].scale==0);
        auto* anchor=&created[0];auto* tail_anchor=&created[1];
        CHECK(tail_anchor->parent==&tail&&tail_anchor->scale==0); // tail base, not Head
        CHECK(costume.bones==std::vector<void*>({&torso,anchor,&neck,anchor,tail_anchor}));
        CHECK(costume.enabled&&costume.mode==0&&costume.mesh==&costume_mesh);
        g_first_person_pump_frames=840;EnsureNeckCap();CHECK(g_fp_mesh_session.patches.size()==1);
        // Reparenting a collapsed bone out of Head releases and re-evaluates it.
        hair.parent=&torso;g_first_person_pump_frames=870;EnsureNeckCap();
        CHECK(costume.bones[1]==&hair&&costume.bones[3]==anchor&&g_fp_mesh_session.patches.size()==1);
        hair.parent=&head;g_first_person_pump_frames=875;EnsureNeckCap();
        CHECK(costume.bones[1]==anchor&&costume.bones[3]==anchor);
        ReleaseNeckCap();CHECK(costume.bones==original&&g_fp_mesh_session.patches.empty());
        // The named anchor is reused, not duplicated, on the next session.
        g_first_person_pump_frames=880;EnsureNeckCap();
        CHECK(created.size()==2&&costume.bones[1]==anchor&&g_fp_mesh_session.patches.size()==1);
        // An external palette write (BEM or game) supersedes ours and is kept.
        costume.bones={&torso,&neck};g_first_person_pump_frames=920;EnsureNeckCap();
        CHECK(costume.bones==std::vector<void*>({&torso,&neck}));
        ReleaseNeckCap();costume.bones=original;
        // A shadow-casting mixed part keeps its palette: it is its own shadow source.
        costume.mode=1;g_fp_mesh_session={};g_first_person_pump_frames=960;EnsureNeckCap();
        CHECK(costume.bones==original&&g_fp_mesh_session.patches.empty());
        // The game turning casting on releases the collapse.
        costume.mode=0;g_fp_mesh_session={};g_first_person_pump_frames=1000;EnsureNeckCap();
        CHECK(costume.bones[1]==anchor);costume.mode=1;
        g_first_person_pump_frames=1040;EnsureNeckCap();
        CHECK(costume.bones==original&&g_fp_mesh_session.patches.empty());
        // An externally rescaled anchor releases the patch; the next attempt
        // forces the collapse scale back before reusing our named child.
        costume.mode=0;g_fp_mesh_session={};g_first_person_pump_frames=1060;EnsureNeckCap();
        CHECK(costume.bones[1]==anchor);anchor->scale=1;
        g_first_person_pump_frames=1070;EnsureNeckCap();
        CHECK(costume.bones[1]==anchor&&anchor->scale==0&&g_fp_mesh_session.patches.size()==1&&created.size()==2);
        ReleaseNeckCap();CHECK(costume.bones==original);
        // A palette with only Neck/body entries is never touched; nor is hairshadow.
        costume.bones={&torso,&neck};g_fp_mesh_session={};g_first_person_pump_frames=1080;EnsureNeckCap();
        CHECK(costume.bones==std::vector<void*>({&torso,&neck})&&g_fp_mesh_session.patches.empty());
        costume.bones={&torso,&hair};costume.name="hairshadow_01";g_fp_mesh_session={};
        g_first_person_pump_frames=1120;EnsureNeckCap();
        CHECK(costume.bones==std::vector<void*>({&torso,&hair})&&g_fp_mesh_session.patches.empty());
        // Missing optional bindings disable the fallback without side effects.
        costume.name="cloth_02";Contract("fp.array.clone")->resolved=false;g_fp_mesh_session={};
        g_first_person_pump_frames=1160;EnsureNeckCap();
        CHECK(costume.bones==std::vector<void*>({&torso,&hair})&&g_fp_mesh_session.patches.empty());
        Contract("fp.array.clone")->resolved=true;
        go.renderers={&renderer};g_fp_mesh_session={};
    }

    // Per-role face/hair Neck boundaries may hide completely without GPU;
    // a garment or palette with any torso influence stays on the crop path.
    Object mesh_all,lod1,face_go;
    mesh_all.name="Mesh_all";mesh_all.parent=&body;
    lod1.name="lod1";lod1.parent=&mesh_all;
    face_go.name="S_actor_typhoea_face_01_lod1";face_go.parent=&lod1;face_go.transform=&face_go;
    renderer.game_object=&face_go;renderer.bones={&head,&neck};g_first_person.model_id="chr_0034_typhoea";
    CHECK(FpTryDirectNamedHide(Probe(renderer,"face_01"),patch));
    CHECK(renderer.mode==3&&FpRestore(patch)&&renderer.mode==1);
    renderer.bones={&head,&neck,&torso};CHECK(!FpTryDirectNamedHide(Probe(renderer,"face_01"),patch));
    face_go.name="S_actor_typhoea_cloth_02_lod1";renderer.bones={&head,&neck};
    CHECK(!FpTryDirectNamedHide(Probe(renderer,"cloth_02"),patch));
    g_first_person.model_id.clear();renderer.game_object=nullptr;renderer.bones={&head};
    Object rig_root,bip,pelvis,spine,spine1,spine2,hat_root,hat_child;
    rig_root.name="Root";rig_root.parent=&body;bip.name="Bip001";bip.parent=&rig_root;
    pelvis.name="Bip001_Pelvis";pelvis.parent=&bip;spine.name="Bip001_Spine";spine.parent=&pelvis;
    spine1.name="Bip001_Spine1";spine1.parent=&spine;spine2.name="Bip001_Spine2";spine2.parent=&spine1;
    hat_root.name="maozi_a1_M";hat_root.parent=&spine2;hat_child.name="maozi_a2_M";hat_child.parent=&hat_root;
    g_first_person.model_id="chr_0003_endminf";
    CHECK(FpBoneKind(&hat_root)==1&&FpBoneKind(&hat_child)==1&&FpBoneKind(&spine2)==0);
    {
        // A profile-proven Spine2 hat chain collapses onto its own root anchor,
        // never onto Head, and the Spine2 entry itself is preserved.
        Object hat_mesh,coat;coat.mesh=&hat_mesh;coat.mode=0;coat.name="cloth_01";
        coat.bones={&spine2,&hat_child,&hair};go.renderers={&coat};
        g_fp_mesh_session={};g_first_person_pump_frames=1200;EnsureNeckCap();
        CHECK(g_fp_mesh_session.patches.size()==1&&g_fp_mesh_session.patches[0].redirected);
        auto* hat_anchor=static_cast<Object*>(coat.bones[1]);auto* head_anchor=static_cast<Object*>(coat.bones[2]);
        CHECK(coat.bones[0]==&spine2&&hat_anchor->parent==&hat_root&&hat_anchor->scale==0&&head_anchor->parent==&head);
        ReleaseNeckCap();CHECK(coat.bones==std::vector<void*>({&spine2,&hat_child,&hair}));
        // Without the profile the Spine2 chain is ordinary body.
        g_first_person.model_id.clear();g_fp_mesh_session={};g_first_person_pump_frames=1240;EnsureNeckCap();
        CHECK(coat.bones[0]==&spine2&&coat.bones[1]==&hat_child&&static_cast<Object*>(coat.bones[2])->parent==&head);
        ReleaseNeckCap();go.renderers={&renderer};g_fp_mesh_session={};g_first_person.model_id="chr_0003_endminf";
    }
    g_first_person.model_id.clear();CHECK(FpBoneKind(&hat_root)==0);

    // The local policy can remove a Head/Neck boundary while a connected body
    // triangle remains. Passing the policy mask does not enable hide_all.
    std::vector<FpMesh::Vertex> local_vertices(4);
    for(auto& vertex:local_vertices){vertex.position={0,1,0};vertex.weight={1,0,0,0};}
    local_vertices[0].bone[0]=local_vertices[2].bone[0]=0;
    local_vertices[1].bone={0,1,0,0};local_vertices[1].weight={0.2,0.8,0,0};
    local_vertices[3].bone[0]=2;
    const std::vector<FpMesh::Part> local_parts{{{0,1,2,1,2,3}}};
    const std::vector<uint8_t> local_bones{1,2,0},local_mask{1,1,1,0};
    CHECK(FpMesh::Build(local_vertices,local_parts,local_bones,{},false,false).hidden_triangles==0);
    const auto local_result=FpMesh::Build(local_vertices,local_parts,local_bones,{},false,false,1,&local_mask);
    CHECK(local_result.hidden_triangles==1&&local_result.parts[0].indices[5]==3);

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
