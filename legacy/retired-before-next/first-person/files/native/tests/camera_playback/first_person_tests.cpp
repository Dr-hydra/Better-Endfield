// Exercise the production camera TU with typed scene objects. A wrong
// Entity-as-Component call or a destroyed Transform access fails immediately.
#include "../../modules/camera/module.cpp"
#include "eiem_body_fake.h"
#include "test_support.h"
#include <array>
#include <limits>
#include <thread>
using namespace BetterEndfield;
using namespace BetterEndfield::CameraModule;
namespace {
struct Object {
    enum Kind { Entity, GameObject, Transform, Camera, Brain, Extension,
        Rotator, Movement, Ability, RootMotion } kind;
    bool alive=true;
    const char* name="test";
    Object* object=nullptr;
    Object* transform=nullptr;
    Object* model=nullptr;
    Object* head=nullptr;
    Object* parent=nullptr;
    Object* owner=nullptr;
    Object* rotator=nullptr;
    Object* movement=nullptr;
    Object* ability=nullptr;
    Object* root_motion=nullptr;
    bool active=false,in_air=false,root_rotation=false,locked=false,immobilized=false;
    Vector3 position{};
    Quaternion rotation{0,0,0,1};
    int position_reads=0,rotation_writes=0;
};
struct Snapshot {
    std::array<uint8_t,16> header{};
    bool active=false,pending=false;
    Object* rotation=nullptr;
    Object* offset=nullptr;
} snapshot;
Object model_a{Object::GameObject},body_a{Object::Transform},head_a{Object::Transform};
Object model_b{Object::GameObject},body_b{Object::Transform},head_b{Object::Transform};
Object entity_a{Object::Entity},entity_b{Object::Entity};
Object rotator_a{Object::Rotator},rotator_b{Object::Rotator};
Object movement_a{Object::Movement},movement_b{Object::Movement};
Object ability_a{Object::Ability},ability_b{Object::Ability},root_motion{Object::RootMotion};
Object camera_go{Object::GameObject},other_go{Object::GameObject};
Object camera{Object::Camera},other_camera{Object::Camera};
Object brain{Object::Brain},other_brain{Object::Brain};
Object post_rotation{Object::Extension},camera_offset{Object::Extension};
Object* main_character=&entity_a;
Object* main_camera=&camera;
float dt=1.0f/60.0f,fov=60;
bool boolean=false,post_override=false;
int integer=0,snapshot_clear_calls=0,original_push_calls=0;
bool root_motion_getter_fails=false;
uint32_t next_handle=1;
struct Lens {float fov=60,near_clip=0.3f,dutch=0;};
struct State {
    Vector3 position{1,2,3};
    Quaternion orientation{0,0,0,1};
    Vector3 correction{0.1f,0.2f,0.3f};
    Quaternion orientation_correction{0,0.70710678f,0,0.70710678f};
    Lens lens;
} pushed_state;

void* InvokeFake(void*,const void* method,void* instance,void** args,void** exception) {
    *exception=nullptr;
    const std::string_view key=static_cast<const char*>(method);
    auto* object=static_cast<Object*>(instance);
    if(key=="player_controller.get_main_character")return main_character;
    if(key=="unity.camera.main")return main_camera;
    if(key=="unity.camera.orthographic.get") {boolean=false;return &boolean;}
    if(key=="unity.object.op_equality") {
        auto* value=static_cast<Object*>(args[0]);
        CHECK(!value||reinterpret_cast<void*>(value)==&snapshot||value->kind!=Object::Entity);
        boolean=value&&reinterpret_cast<void*>(value)!=&snapshot&&!value->alive;
        return &boolean;
    }
    if(key=="unity.object.find_object_of_type")return &snapshot;
    if(key=="snapshot.set_first_person") {
        CHECK(instance==&snapshot);
        CHECK(!*static_cast<bool*>(args[0]));
        snapshot.active=false;snapshot.pending=true;
        return nullptr;
    }
    if(key=="snapshot.clear_first_person") {
        CHECK(instance==&snapshot&&!snapshot.active&&snapshot.pending);
        CHECK(snapshot.rotation&&snapshot.rotation->alive&&snapshot.offset&&snapshot.offset->alive);
        ++snapshot_clear_calls;snapshot.pending=false;post_override=false;
        return nullptr;
    }
    if(key=="entity.get_model_com") {CHECK(object&&object->kind==Object::Entity);return object->model;}
    if(key=="fp.entity.rotation") {CHECK(object&&object->kind==Object::Entity);return &object->rotation;}
    if(key=="fp.entity.alive") {CHECK(object&&object->kind==Object::Entity);return &object->alive;}
    if(key=="fp.entity.in_cinematic") {CHECK(object&&object->kind==Object::Entity);return &object->active;}
    if(key=="fp.entity.rotator") {CHECK(object&&object->kind==Object::Entity);return object->rotator;}
    if(key=="fp.entity.movement") {CHECK(object&&object->kind==Object::Entity);return object->movement;}
    if(key=="fp.entity.ability") {CHECK(object&&object->kind==Object::Entity);return object->ability;}
    if(key=="fp.model.id")return nullptr;
    if(key=="fp.rotator.is_rotating") {CHECK(object&&object->kind==Object::Rotator);return &object->active;}
    if(key=="fp.rotator.lock_to_camera") {CHECK(object&&object->kind==Object::Rotator);return &object->locked;}
    if(key=="fp.movement.is_moving") {CHECK(object&&object->kind==Object::Movement);return &object->active;}
    if(key=="fp.movement.is_in_air") {CHECK(object&&object->kind==Object::Movement);return &object->in_air;}
    if(key=="fp.movement.root_motion_rotation") {CHECK(object&&object->kind==Object::Movement);return &object->root_rotation;}
    if(key=="fp.ability.in_skill") {CHECK(object&&object->kind==Object::Ability);return &object->active;}
    if(key=="fp.ability.immobilized") {CHECK(object&&object->kind==Object::Ability);return &object->immobilized;}
    if(key=="fp.movement.root_motion_data") {
        CHECK(object&&object->kind==Object::Movement);
        if(root_motion_getter_fails)*exception=reinterpret_cast<void*>(1);
        return object->root_motion;
    }
    if(key=="fp.root_motion.has_motion") {CHECK(object&&object->kind==Object::RootMotion);return &object->active;}
    if(key=="fp.rotator.set_rotation") {
        CHECK(object&&object->kind==Object::Rotator&&object->owner);
        CHECK(!object->active&&!object->locked); // SetRotation stops a game rotation task.
        ++object->rotation_writes;
        auto* entity=object->owner;
        const Quaternion next=*static_cast<Quaternion*>(args[0]);
        const Quaternion change=Multiply(next,Conjugate(entity->rotation));
        entity->rotation=next;
        auto* body=entity->model->transform;
        body->rotation=Multiply(change,body->rotation);
        if(body->head)body->head->position=Add(body->position,
            RotateVector(change,Subtract(body->head->position,body->position)));
        return nullptr;
    }
    if(key=="base_model_component.get_model_go")return object;
    if(key=="unity.component.transform"||key=="unity.component.game_object") {
        CHECK(object&&object->kind!=Object::Entity&&object->kind!=Object::GameObject&&object->alive);
        return key=="unity.component.transform"?object->transform:object->object;
    }
    if(key=="unity.game_object.transform") {CHECK(object&&object->kind==Object::GameObject&&object->alive);return object->transform;}
    if(key=="unity.transform.find") {CHECK(object&&object->alive);return object->head;}
    if(key=="unity.transform.parent.get") {CHECK(object&&object->alive);return object->parent;}
    if(key=="unity.transform.child_count.get") {integer=0;return &integer;}
    if(key=="unity.object.name.get")return const_cast<char*>(object->name);
    if(key=="unity.transform.position.get") {CHECK(object&&object->alive);++object->position_reads;return &object->position;}
    if(key=="unity.transform.rotation.get") {CHECK(object&&object->alive);return &object->rotation;}
    if(key=="unity.transform.rotation.set") {CHECK(object&&object->alive);++object->rotation_writes;object->rotation=*static_cast<Quaternion*>(args[0]);return nullptr;}
    if(key=="unity.camera.fov.get")return &fov;
    if(key=="unity.time.unscaled_delta.get")return &dt;
    return nullptr;
}
void __fastcall PushFake(void*,void* state,void*) {++original_push_calls;pushed_state=*static_cast<State*>(state);}
float __fastcall DeltaFake(void*) {return dt;}

void BindField(const char* key,size_t offset) {
    auto* field=Field(key);CHECK(field);field->ready=true;field->resolved.offset=static_cast<int>(offset);
}
void Setup(BE_HostApiV1& host) {
    host.runtime_invoke=InvokeFake;
    host.object_unbox=[](void*,void* value)->void*{return value;};
    host.gchandle_new=[](void*,void*,int)->uint32_t{return next_handle++;};
    host.gchandle_free=[](void*,uint32_t){};
    host.string_new=[](void*,const char* text)->void*{return const_cast<char*>(text);};
    host.copy_managed_string=[](void*,const void* value,char* out,size_t size)->int {
        const auto* text=static_cast<const char*>(value);std::snprintf(out,size,"%s",text);return static_cast<int>(std::strlen(text));
    };
    g_host=&host;
    for(auto& c:g_contracts){c.resolved=true;c.method_info=c.key;}
    BindField("snapshot.is_first_person",offsetof(Snapshot,active));
    BindField("snapshot.pending_first_person_exit",offsetof(Snapshot,pending));
    BindField("snapshot.post_rotation",offsetof(Snapshot,rotation));
    BindField("snapshot.camera_offset",offsetof(Snapshot,offset));
    g_snapshot_controller_class.type_object=reinterpret_cast<void*>(1);
    g_state_layout={static_cast<int>(offsetof(State,position)),static_cast<int>(offsetof(State,orientation)),
        static_cast<int>(offsetof(State,correction)),static_cast<int>(offsetof(State,orientation_correction)),
        static_cast<int>(offsetof(State,lens)),static_cast<int>(offsetof(Lens,fov)),
        static_cast<int>(offsetof(Lens,near_clip)),static_cast<int>(offsetof(Lens,dutch)),true};
    g_first_person_contract_ready=true;g_push_state_hook_ready=true;
    g_time_heartbeat_contract_ready=true;g_first_person_hide_head=false;
    g_first_person_camera_enabled=true;g_free_camera_enabled=false;
    g_original_push_state=PushFake;g_original_time_unscaled_delta=DeltaFake;
    model_a.transform=&body_a;model_b.transform=&body_b;
    body_a.head=&head_a;body_b.head=&head_b;
    head_a.parent=&body_a;head_b.parent=&body_b;
    body_a.object=&model_a;body_b.object=&model_b;
    head_a.object=&model_a;head_b.object=&model_b;
    head_a.name="Bip001_Head";head_b.name="Bip001_Head";
    head_a.position={0,1.7f,0};head_b.position={3,1.8f,0};
    body_b.position={3,0,0};
    entity_a.model=&model_a;entity_b.model=&model_b;
    entity_a.rotator=&rotator_a;entity_b.rotator=&rotator_b;
    entity_a.movement=&movement_a;entity_b.movement=&movement_b;
    entity_a.ability=&ability_a;entity_b.ability=&ability_b;
    rotator_a.owner=&entity_a;rotator_b.owner=&entity_b;
    camera.object=&camera_go;brain.object=&camera_go;
    other_camera.object=&other_go;other_brain.object=&other_go;
    snapshot.rotation=&post_rotation;snapshot.offset=&camera_offset;
    g_camera_unity_thread_id=GetCurrentThreadId();
}
} // namespace

int main() {
    BE_HostApiV1 host{};Setup(host);
    CHECK(EnterFirstPerson());
    CHECK(g_first_person.target_valid&&g_first_person.character==&entity_a&&g_first_person.body==&body_a);
    State state,original=state;
    DetourPushState(&brain,&state,nullptr);
    CHECK(pushed_state.lens.fov==75&&pushed_state.lens.near_clip==kFirstPersonNearClip);
    CHECK(nearly(pushed_state.position.y,head_a.position.y+kFirstPersonEyeUp));
    CHECK(std::memcmp(&state,&original,sizeof(state))==0); // cached game state is untouched
    const int head_reads=head_a.position_reads;
    DetourPushState(&other_brain,&state,nullptr);
    CHECK(head_a.position_reads==head_reads&&pushed_state.position.y==original.position.y);

    // The feature can close using only unscaled time; no TailLateTick is called.
    g_first_person_camera_enabled=false;g_first_person_exit_request=true;
    std::thread worker([]{DetourTimeUnscaledDelta(nullptr);});worker.join();
    CHECK(g_first_person_active&&g_first_person_exit_request); // off-thread reads cannot touch Unity
    DetourTimeUnscaledDelta(nullptr);
    CHECK(!g_first_person_active&&!g_first_person_exit_request&&!g_first_person.body&&!g_active_camera);
    CHECK(std::memcmp(&state,&original,sizeof(state))==0);

    // A closing request also wins at the next Brain push before its state write.
    g_first_person_camera_enabled=true;CHECK(EnterFirstPerson());
    g_first_person_exit_request=true;
    DetourPushState(&brain,&state,nullptr);
    CHECK(!g_first_person_active&&pushed_state.position.y==original.position.y);

    // A -> null -> B, with A still alive, never reuses A for the transition push.
    CHECK(EnterFirstPerson());main_character=nullptr;
    const int reads_before=head_a.position_reads;
    DetourPushState(&brain,&state,nullptr);
    CHECK(!g_first_person.target_valid&&head_a.position_reads==reads_before);
    PumpFirstPerson();CHECK(!g_first_person.character&&!g_first_person.head&&!g_first_person.body);
    main_character=&entity_b;PumpFirstPerson();
    CHECK(g_first_person.target_valid&&g_first_person.head==&head_b&&g_first_person.body==&body_b);
    DetourPushState(&brain,&state,nullptr);CHECK(nearly(pushed_state.position.x,3+kFirstPersonEyeForward));
    // Destroyed anchors are rejected before any Transform method can execute.
    body_b.alive=false;head_b.alive=false;main_character=nullptr;
    DetourPushState(&brain,&state,nullptr);PumpFirstPerson();
    CHECK(!g_first_person.head&&!g_first_person.body);
    body_b.alive=true;head_b.alive=true;main_character=&entity_a;PumpFirstPerson();
    CHECK(g_first_person.target_valid);
    // The same Entity can receive a rebuilt model/skeleton.
    entity_a.model=&model_b;PumpFirstPerson();
    CHECK(g_first_person.model==&model_b&&g_first_person.head==&head_b);
    entity_a.model=&model_a;PumpFirstPerson();

    // Restore only an entity-facing rotation that is still owned by this session.
    g_first_person_view_forward={-1,0,0};g_first_person_view_forward_valid=true;
    g_first_person_side_look_limit=30;ApplyFirstPersonFacing();
    CHECK(g_first_person.body_rotation_owned&&body_a.rotation.y!=0);
    ExitFirstPerson("test");CHECK(nearly(body_a.rotation.y,0));
    CHECK(EnterFirstPerson());g_first_person_view_forward={-1,0,0};g_first_person_view_forward_valid=true;
    ApplyFirstPersonFacing();entity_a.rotation=body_a.rotation={0,0.38268343f,0,0.92387953f};
    ExitFirstPerson("game took over facing");CHECK(nearly(body_a.rotation.y,0.38268343f));

    // Captured camera identity owns the session; a new main camera closes it.
    CHECK(EnterFirstPerson());main_camera=&other_camera;
    DetourPushState(&other_brain,&state,nullptr);CHECK(!g_first_person_active);
    main_camera=&camera;

    // Snapshot's false flag is insufficient: a deferred post-rotation override
    // must be completed without waiting for a camera transition tick.
    snapshot.active=true;snapshot.pending=false;post_override=true;g_first_person_hide_head=true;
    CHECK(EnterFirstPerson());
    CHECK(!snapshot.active&&!snapshot.pending&&!post_override&&snapshot_clear_calls==1);
    ExitFirstPerson("snapshot completed");
    // Retain cleanup ownership across exit when a required component is absent.
    snapshot.active=true;post_override=true;snapshot.rotation=nullptr;
    CHECK(EnterFirstPerson());CHECK(snapshot.pending&&g_first_person.snapshot_exit_owned);
    ExitFirstPerson("paused while pending");
    CHECK(g_first_person.snapshot_controller==&snapshot&&g_first_person.snapshot_exit_owned);
    snapshot.rotation=&post_rotation;PumpFirstPerson(false);
    CHECK(!snapshot.pending&&!post_override&&!g_first_person.snapshot_controller&&snapshot_clear_calls==2);
    // A subsequent user-owned photo session is preserved.
    g_first_person.snapshot_controller=&snapshot;g_first_person.snapshot_exit_owned=true;
    snapshot.active=true;snapshot.pending=false;post_override=true;
    FinishGameFirstPersonExit();
    CHECK(snapshot.active&&post_override&&!g_first_person.snapshot_exit_owned&&snapshot_clear_calls==2);
    // Pending state from an unrelated session is never cleared by this module.
    snapshot.active=false;snapshot.pending=true;
    FinishGameFirstPersonExit();CHECK(snapshot.pending&&snapshot_clear_calls==2);
    ReleaseSnapshotController();
    // The same ModelGo can replace its skeleton while detached old bones live.
    g_first_person_hide_head=false;
    main_character=&entity_a;entity_a.model=&model_a;
    head_a.parent=&body_a;body_a.head=&head_a;
    CHECK(EnterFirstPerson());
    Object replacement_head{Object::Transform};
    replacement_head.name="Bip001_Head";
    replacement_head.parent=&body_a;replacement_head.object=&model_a;
    replacement_head.position={0,2.1f,0};
    body_a.head=&replacement_head;head_a.parent=nullptr;
    const int old_head_reads=head_a.position_reads;
    DetourPushState(&brain,&state,nullptr);
    CHECK(!g_first_person.target_valid&&head_a.position_reads==old_head_reads);
    PumpFirstPerson();
    CHECK(g_first_person.target_valid&&g_first_person.head==&replacement_head);
    ExitFirstPerson("same model skeleton replacement");
    body_a.head=&head_a;head_a.parent=&body_a;
    CHECK(original_push_calls>=7);

    // Final view: raw yaw 90, local correction pitch 90, Dutch roll 90.
    // The noncommuting combination points forward down and up along +Z.
    entity_a.rotation=body_a.rotation={0,0,0,1};
    head_a.position={0,1.7f,0};
    CHECK(EnterFirstPerson());
    State tilted;
    tilted.orientation={0,0.70710678f,0,0.70710678f};
    tilted.orientation_correction={0.70710678f,0,0,0.70710678f};
    tilted.lens.dutch=90;
    const State tilted_original=tilted;
    DetourPushState(&brain,&tilted,nullptr);
    CHECK(nearly(pushed_state.position.x,0));
    CHECK(nearly(pushed_state.position.y,1.7f-kFirstPersonEyeForward));
    CHECK(nearly(pushed_state.position.z,kFirstPersonEyeUp));
    CHECK(std::memcmp(&pushed_state.orientation,&tilted.orientation,sizeof(Quaternion))==0);
    CHECK(std::memcmp(&pushed_state.orientation_correction,&tilted.orientation_correction,sizeof(Quaternion))==0);
    CHECK(pushed_state.lens.dutch==90&&std::memcmp(&tilted,&tilted_original,sizeof(tilted))==0);

    // Tail/lifecycle refresh queues one turn. The next push uses its current
    // view, not the previous frame's view, and samples the head after turning.
    State facing;
    facing.orientation_correction={0,0,0,1};
    facing.orientation={0,-0.70710678f,0,0.70710678f}; // current view left
    g_first_person_view_forward={1,0,0};g_first_person_view_forward_valid=true; // stale view right
    head_a.position={0,1.7f,0.2f};
    const int writes_before=rotator_a.rotation_writes;
    PumpFirstPerson();
    CHECK(rotator_a.rotation_writes==writes_before);
    DetourPushState(&other_brain,&facing,nullptr);
    CHECK(rotator_a.rotation_writes==writes_before&&g_first_person.facing_pending);
    DetourPushState(&brain,&facing,nullptr);
    CHECK(entity_a.rotation.y<0&&rotator_a.rotation_writes==writes_before+1);
    CHECK(nearly(pushed_state.position.x,head_a.position.x-kFirstPersonEyeForward));
    CHECK(nearly(pushed_state.position.z,head_a.position.z));
    DetourPushState(&brain,&facing,nullptr);
    CHECK(rotator_a.rotation_writes==writes_before+1); // repeated push cannot accelerate turning
    ExitFirstPerson("current push facing");
    CHECK(nearly(entity_a.rotation.y,0));

    // Invalid current orientation cannot reuse the previous view to turn.
    CHECK(EnterFirstPerson());
    State invalid_states[3];
    invalid_states[0].orientation={0,0,0,0};
    invalid_states[1].orientation_correction={0,0,0,0};
    invalid_states[2].lens.dutch=std::numeric_limits<float>::quiet_NaN();
    const int invalid_writes=rotator_a.rotation_writes;
    for(auto& invalid:invalid_states) {
        const State cached=invalid;
        PumpFirstPerson();DetourPushState(&brain,&invalid,nullptr);
        CHECK(!g_first_person_view_forward_valid&&rotator_a.rotation_writes==invalid_writes);
        CHECK(pushed_state.lens.fov==60&&std::memcmp(&invalid,&cached,sizeof(invalid))==0);
    }
    ExitFirstPerson("invalid view");

    // An authored model yaw offset survives entity turning and restoration.
    body_a.rotation={0,0.38268343f,0,0.92387953f}; // visual model differs from entity
    CHECK(EnterFirstPerson());
    g_first_person_view_forward={-1,0,0};g_first_person_view_forward_valid=true;
    ApplyFirstPersonFacing();
    CHECK(entity_a.rotation.y<0&&body_a.rotation.y>0&&body_a.rotation_writes==0);
    ExitFirstPerson("preserve authored model offset");
    CHECK(nearly(entity_a.rotation.y,0)&&nearly(body_a.rotation.y,0.38268343f));
    body_a.rotation={0,0,0,1};

    // Movement, airborne actions, skills, root motion, camera locking and
    // cinematic/immobilized states all retain the game's entity/model facing.
    CHECK(EnterFirstPerson());
    bool* activity_flags[]{&movement_a.active,&movement_a.in_air,&movement_a.root_rotation,
        &ability_a.active,&ability_a.immobilized,&rotator_a.active,&rotator_a.locked,&entity_a.active};
    for(bool* flag:activity_flags) {
        const int before=rotator_a.rotation_writes;
        *flag=true;
        PumpFirstPerson();DetourPushState(&brain,&facing,nullptr);
        CHECK(rotator_a.rotation_writes==before&&!g_first_person.body_rotation_owned);
        CHECK(nearly(entity_a.rotation.y,0)&&nearly(body_a.rotation.y,0));
        *flag=false;
    }
    movement_a.root_motion=&root_motion;root_motion.active=true;
    const int guarded_writes=rotator_a.rotation_writes;
    PumpFirstPerson();DetourPushState(&brain,&facing,nullptr);
    CHECK(rotator_a.rotation_writes==guarded_writes);
    root_motion.active=false;root_motion_getter_fails=true;
    PumpFirstPerson();DetourPushState(&brain,&facing,nullptr);
    CHECK(rotator_a.rotation_writes==guarded_writes); // getter failure is unknown
    root_motion_getter_fails=false;movement_a.root_motion=nullptr;
    auto* guard=Contract("fp.ability.in_skill");const void* saved_method=guard->method_info;
    guard->method_info=nullptr;
    PumpFirstPerson();DetourPushState(&brain,&facing,nullptr);
    CHECK(rotator_a.rotation_writes==guarded_writes&&pushed_state.lens.fov==75);
    guard->method_info=saved_method;

    // A game-authored facing change between pushes wins even if idle flags
    // have already cleared. It must not be overwritten or restored on exit.
    PumpFirstPerson();DetourPushState(&brain,&facing,nullptr);
    CHECK(g_first_person.body_rotation_owned);
    entity_a.rotation=body_a.rotation={0,0.38268343f,0,0.92387953f};
    const int takeover_writes=rotator_a.rotation_writes;
    PumpFirstPerson();DetourPushState(&brain,&facing,nullptr);
    CHECK(rotator_a.rotation_writes==takeover_writes&&!g_first_person.body_rotation_owned);
    ExitFirstPerson("game facing between pushes");
    CHECK(nearly(entity_a.rotation.y,0.38268343f));

    // Exit during a stationary skill must not call SetRotation/StopRotate.
    CHECK(EnterFirstPerson());g_first_person_view_forward={-1,0,0};g_first_person_view_forward_valid=true;
    ApplyFirstPersonFacing();CHECK(g_first_person.body_rotation_owned);
    ability_a.active=true;const Quaternion skill_facing=entity_a.rotation;
    const int skill_writes=rotator_a.rotation_writes;
    ExitFirstPerson("skill owns facing on exit");
    CHECK(rotator_a.rotation_writes==skill_writes&&FirstPersonSameRotation(entity_a.rotation,skill_facing));
    ability_a.active=false;
    g_host=nullptr;
    std::cout<<"PASS production first-person lifecycle: "<<checks<<" checks\n";
}
