#include "../../modules/camera/module.cpp"
#include "../../shared/motion/pose_lease_registry.h"
#include "eiem_body_fake.h"
#include "test_support.h"
using namespace BetterEndfield;
using namespace BetterEndfield::CameraModule;
namespace CM=BetterEndfield::CameraModule::CharacterMotion;
using LoadState=BetterEndfield::EiemBody::LoadState;
struct Object {
    bool alive=true;
    Object* transform=nullptr;
    Object* model_com=nullptr;
    Object* model_go=nullptr;
    Object* movement=nullptr;
    std::vector<Object*> animators;
    Vector3 position{};
    bool human=true,enabled=true;
};
struct Rig {
    Object entity,component,model,animator,root,movement;
    void Setup(float x) {
        entity.model_com=&component;component.model_go=&model;model.transform=&root;root.transform=&root;
        animator.transform=&root;model.animators={&animator};entity.movement=&movement;root.position={x,0,0};
    }
};
struct PlayerFake {char header[16]{};void* manager=nullptr;};
Rig leader,member1,member2;
Object manager;
PlayerFake player;
std::vector<Object*> squad;
std::map<uint32_t,void*> pins;
uint32_t next_pin=1;
Motion::PoseLeaseRegistry registry;
uint64_t BE_CALL Acquire(const void* root,const char* owner){return registry.Acquire(root,owner);}
int BE_CALL Owns(const void* root,const char* owner,uint64_t id){return registry.Owns(root,owner,id);}
int BE_CALL Release(const void* root,const char* owner,uint64_t id){return registry.Release(root,owner,id);}
const BE_PoseLeaseApiV1 leases{1,Acquire,Owns,Release};
template<class T> void* Box(T value){static thread_local T box;box=value;return &box;}
void* BE_CALL InvokeFake(void*,const void* method,void* instance,void** a,void** exception) {
    *exception=nullptr;
    auto object=static_cast<Object*>(instance);
    const auto id=reinterpret_cast<uintptr_t>(method);
    if(id==1001)return &leader.entity;
    if(id==1002)return object->model_com;
    if(id==1003)return object->model_go;
    if(id==1004)return object->transform;
    if(id==1005)return Box(object->position);
    if(id==1006)return Box(Quaternion{0,0,0,1});
    switch(CM::Id(id-1)) {
        case CM::Alive:return Box(a[0]&&static_cast<Object*>(a[0])->alive);
        case CM::Transform:return object->transform;
        case CM::Children:return a[0]==reinterpret_cast<void*>(201)?static_cast<void*>(&object->animators):nullptr;
        case CM::ArrayLength:return Box(int(static_cast<std::vector<Object*>*>(instance)->size()));
        case CM::ArrayValue:return static_cast<std::vector<Object*>*>(instance)->at(*static_cast<int*>(a[0]));
        case CM::IsChild:return Box(object->alive&&a[0]==object->transform);
        case CM::Movement:return object->movement;
        case CM::Player:return &player;
        case CM::SquadLoading:return Box(false);
        case CM::SquadCount:return Box(int(squad.size()));
        case CM::SquadMember:return squad.at(*static_cast<int*>(a[0]));
        case CM::IsHuman:return Box(object->human);
        case CM::ActiveEnabled:return Box(object->enabled);
    }
    *exception=reinterpret_cast<void*>(1);return nullptr;
}
void* BE_CALL UnboxFake(void*,void* v){return v;}
uint32_t BE_CALL PinFake(void*,void* p,int){auto id=next_pin++;pins[id]=p;return id;}
void BE_CALL UnpinFake(void*,uint32_t p){pins.erase(p);}
void BE_CALL LogFake(void*,const char*,const char*){}
static BE_HostApiV1 host{};
static auto Config() {
    auto config=std::make_shared<CM::Config>();
    config->enabled=config->face=true;config->terrain=true;config->motion_scale=1.5f;config->cloth=EiemBody::ClothMode::Freeze;config->file="C:/dance.vmd";
    return config;
}
static bool Owned(const Rig& rig,int actor) {
    const auto lease=CM::runtime.actors[actor].lease;
    return lease&&registry.Owns(&rig.root,CM::kOwner,lease);
}
static int Dancing() {int n=0;for(auto& a:CM::runtime.actors)if(a.active)++n;return n;}
static void Ready() {EiemFake::SetAllLoadStates(LoadState::Ready);PumpCharacterMotion();}
static std::vector<CM::Part> Cast(int count) {
    std::vector<CM::Part> parts{{"C:/w/motion.vmd","C:/w/face.vmd"},{"C:/w/motion2.vmd","C:/w/face2.vmd"},
        {"C:/w/motion3.vmd",""},{"C:/w/motion4.vmd",""}};
    parts.resize(size_t(count));return parts;
}
int main() {
    host.abi_version=1;host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.gchandle_new=PinFake;host.gchandle_free=UnpinFake;
    host.log=LogFake;g_host=&host;
    for(size_t i=0;i<std::size(CM::methods);++i)CM::methods[i].value.method_info=reinterpret_cast<void*>(i+1);
    Contract("player_controller.get_main_character")->method_info=reinterpret_cast<void*>(1001);
    Contract("entity.get_model_com")->method_info=reinterpret_cast<void*>(1002);
    Contract("base_model_component.get_model_go")->method_info=reinterpret_cast<void*>(1003);
    Contract("unity.game_object.transform")->method_info=reinterpret_cast<void*>(1004);
    Contract("unity.transform.position.get")->method_info=reinterpret_cast<void*>(1005);
    Contract("unity.transform.rotation.get")->method_info=reinterpret_cast<void*>(1006);
    leader.Setup(3);member1.Setup(10);member2.Setup(20);
    player.manager=&manager;squad={&member1.entity,&leader.entity,&member2.entity};
    auto& r=CM::runtime;r.resolved=true;r.leases=&leases;r.applied=Config();std::atomic_store(&CM::config,r.applied);
    r.squad_manager_offset=int(offsetof(PlayerFake,manager));
    r.animator_class.type_object=reinterpret_cast<void*>(201);r.thread_id=GetCurrentThreadId();

    // Another writer owns the rig: nothing is loaded, nothing stays pinned.
    auto foreign=registry.Acquire(&leader.root,"actions");
    CM::requests.store(1);PumpCharacterMotion();
    CHECK(EiemFake::initialize_calls==1);CHECK(EiemFake::actors[0].loads==0);CHECK(!r.Loading()&&!r.Playing());CHECK(pins.empty());
    CHECK(registry.Owns(&leader.root,"actions",foreign));CHECK(registry.Release(&leader.root,"actions",foreign));

    // Standalone play: target, options and file reach EIEM; Start waits for the load.
    CM::requests.store(1);PumpCharacterMotion();
    auto& a0=EiemFake::actors[0];
    CHECK(a0.loads==1);CHECK(a0.motion=="C:/dance.vmd");CHECK(a0.face.empty());
    CHECK(a0.entity==&leader.entity&&a0.animator==&leader.animator&&a0.movement==&leader.movement);
    CHECK(EiemFake::options.face&&EiemFake::options.terrain&&nearly(EiemFake::options.motion_scale,1.5));
    CHECK(EiemFake::options.cloth==EiemBody::ClothMode::Freeze);
    CHECK(!EiemFake::anchor_valid);
    CHECK(r.Loading()&&!r.Playing()&&Owned(leader,0));CHECK(a0.starts==0);
    PumpCharacterMotion();CHECK(a0.starts==0);
    Ready();
    CHECK(Dancing()==1&&!r.Loading());CHECK(a0.starts==1);CHECK(EiemFake::clock_playing);CHECK(EiemFake::clock_seconds>=0);
    // Pause toggles the published clock only in standalone mode.
    CM::requests.store(2);PumpCharacterMotion();CHECK(!EiemFake::clock_playing);
    CM::requests.store(2);PumpCharacterMotion();CHECK(EiemFake::clock_playing);

    // A switched model stops EIEM and releases lease and pins; no rebind.
    const int stops=a0.stops;
    Object new_model;leader.component.model_go=&new_model;PumpCharacterMotion();
    CHECK(!r.Playing());CHECK(a0.stops==stops+1);CHECK(pins.empty());
    leader.component.model_go=&leader.model;

    // Play key toggles; stop key stops.
    CM::requests.store(1);PumpCharacterMotion();Ready();CHECK(Dancing()==1);
    CM::requests.store(1);PumpCharacterMotion();CHECK(!r.Playing());CHECK(pins.empty());
    CM::requests.store(1);PumpCharacterMotion();Ready();CHECK(Dancing()==1);
    CM::requests.store(4);PumpCharacterMotion();CHECK(!r.Playing());CHECK(pins.empty());

    // Load failure releases everything.
    CM::requests.store(1);PumpCharacterMotion();EiemFake::SetAllLoadStates(LoadState::Failed);PumpCharacterMotion();
    CHECK(!r.Playing()&&!r.Loading());CHECK(pins.empty());

    // Several Animators: the only active humanoid one is the body; ambiguity refuses.
    Object prop,sub_rig,inactive_body;prop.human=false;prop.transform=&leader.root;
    leader.model.animators={&prop,&leader.animator};
    CM::requests.store(1);PumpCharacterMotion();
    CHECK(a0.animator==&leader.animator);CHECK(r.Loading()&&Owned(leader,0));
    CM::requests.store(4);PumpCharacterMotion();CHECK(!r.Playing()&&!r.Loading());CHECK(pins.empty());
    inactive_body.enabled=false;inactive_body.transform=&leader.root;
    leader.model.animators={&inactive_body,&leader.animator};
    CM::requests.store(1);PumpCharacterMotion();CHECK(a0.animator==&leader.animator);
    CM::requests.store(4);PumpCharacterMotion();CHECK(pins.empty());
    const int before_loads=a0.loads;
    sub_rig.transform=&leader.root;leader.model.animators={&sub_rig,&leader.animator};
    CM::requests.store(1);PumpCharacterMotion();
    CHECK(a0.loads==before_loads);CHECK(!r.Loading()&&!r.Playing());CHECK(pins.empty());
    leader.model.animators={};
    CM::requests.store(1);PumpCharacterMotion();CHECK(a0.loads==before_loads);CHECK(pins.empty());
    leader.model.animators={&leader.animator};

    // Director, one dancer: phases, duration, and the director's clock.
    RequestCharacterDirectorLoad("C:/w/motion.vmd","C:/w/face.vmd");
    CHECK(CM::director_phase.load()==CM::DirectorLoading);
    PumpCharacterMotion();CHECK(a0.motion=="C:/w/motion.vmd"&&a0.face=="C:/w/face.vmd");
    CHECK(CM::director_phase.load()==CM::DirectorLoading);
    EiemFake::clock_seconds=-1;Ready();
    CHECK(CM::director_phase.load()==CM::DirectorBound);CHECK(nearly(CM::director_duration.load(),12.5));
    CHECK(CM::director_dancers.load()==1);
    CHECK(nearly(EiemFake::clock_seconds,MmdDirectorSeconds()));CHECK(EiemFake::clock_playing==MmdDirectorPlaying());
    // The director's VMD camera follows EIEM's stage anchor and the dancer's size.
    {
        Mmd::g.state=Mmd::State::Playing;Mmd::g.body_ok=true;Mmd::g.sources.camera_from_motion=true;
        EiemFake::camera_ok=true;
        auto& ref=EiemFake::camera;
        ref.position[0]=1;ref.position[1]=2;ref.position[2]=3;ref.motion_scale=0.08f;ref.natural_height=1.5f;ref.offset[1]=0.2f;
        g_vmd_camera_scale=0.07f;
        Vector3 anchor{},offset{};Quaternion rotation{};float scale=0.07f;
        CHECK(MmdCameraReference(anchor,rotation,scale,offset));
        CHECK(nearly(anchor.x,1)&&nearly(anchor.z,3)&&nearly(offset.y,0.2)&&nearly(scale,0.08));
        Mmd::g.sources.camera_from_motion=false;
        CHECK(MmdCameraReference(anchor,rotation,scale,offset));CHECK(nearly(scale,0.07*1.5/1.245));
        g_vmd_camera_scale=0.14f;
        CHECK(MmdCameraReference(anchor,rotation,scale,offset));CHECK(nearly(scale,2*0.07*1.5/1.245));
        ref.motion_scale=0;Mmd::g.sources.camera_from_motion=true;
        CHECK(!MmdCameraReference(anchor,rotation,scale,offset));
        EiemFake::camera_ok=false;Mmd::g.state=Mmd::State::Idle;
        CHECK(!MmdCameraReference(anchor,rotation,scale,offset));
        Mmd::g.body_ok=false;g_vmd_camera_scale=0.07f;
    }
    CM::requests.store(16);PumpCharacterMotion();CHECK(!r.Playing());CHECK(CM::director_phase.load()==CM::DirectorIdle);CHECK(pins.empty());

    // Squad: parts go to the other members in slot order; one shared stage origin.
    EiemFake::actors[1].duration=20;
    RequestCharacterDirectorLoad(Cast(3));PumpCharacterMotion();
    CHECK(r.Loading());CHECK(EiemFake::anchor_valid);CHECK(nearly(EiemFake::anchor_position[0],3));
    CHECK(EiemFake::actors[1].entity==&member1.entity&&EiemFake::actors[1].motion=="C:/w/motion2.vmd"&&EiemFake::actors[1].face=="C:/w/face2.vmd");
    CHECK(EiemFake::actors[2].entity==&member2.entity&&EiemFake::actors[2].motion=="C:/w/motion3.vmd");
    CHECK(Owned(leader,0)&&Owned(member1,1)&&Owned(member2,2));
    // Start waits for every dancer.
    EiemFake::actors[0].load_state=LoadState::Ready;PumpCharacterMotion();CHECK(Dancing()==0);
    Ready();CHECK(Dancing()==3);CHECK(CM::director_dancers.load()==3);CHECK(nearly(CM::director_duration.load(),20));
    // A member leaving the field stops only that member.
    member1.model.alive=false;PumpCharacterMotion();
    CHECK(Dancing()==2);CHECK(!r.actors[1].Bound());CHECK(r.actors[2].active&&r.actors[0].active);member1.model.alive=true;
    // The leader switching stops everyone and clears the shared origin.
    leader.component.model_go=&new_model;PumpCharacterMotion();
    CHECK(Dancing()==0);CHECK(pins.empty());CHECK(!EiemFake::anchor_valid);CHECK(CM::director_phase.load()==CM::DirectorFailed);
    leader.component.model_go=&leader.model;

    // More parts than members in the field: the rest is skipped.
    RequestCharacterDirectorLoad(Cast(4));PumpCharacterMotion();Ready();CHECK(Dancing()==3);
    CM::requests.store(16);PumpCharacterMotion();
    // A member's rig owned by another writer: the next member takes that part.
    auto member_lease=registry.Acquire(&member1.root,"actions");
    RequestCharacterDirectorLoad(Cast(2));PumpCharacterMotion();
    CHECK(r.actors[1].entity==&member2.entity);Ready();CHECK(Dancing()==2);
    CHECK(registry.Owns(&member1.root,"actions",member_lease));CHECK(registry.Release(&member1.root,"actions",member_lease));
    CM::requests.store(16);PumpCharacterMotion();CHECK(pins.empty());
    // A member's file failing leaves the others dancing; the leader's failing fails the load.
    RequestCharacterDirectorLoad(Cast(3));PumpCharacterMotion();
    EiemFake::actors[2].load_state=LoadState::Failed;Ready();CHECK(Dancing()==2);CHECK(!r.actors[2].Bound());
    CM::requests.store(16);PumpCharacterMotion();
    RequestCharacterDirectorLoad(Cast(3));PumpCharacterMotion();
    EiemFake::actors[0].load_state=LoadState::Failed;PumpCharacterMotion();
    CHECK(Dancing()==0);CHECK(pins.empty());CHECK(CM::director_phase.load()==CM::DirectorFailed);
    // Squad contracts missing: only the controlled character dances.
    CM::methods[CM::SquadMember].value.method_info=nullptr;
    RequestCharacterDirectorLoad(Cast(3));PumpCharacterMotion();Ready();CHECK(Dancing()==1);CHECK(!EiemFake::anchor_valid);
    CM::requests.store(16);PumpCharacterMotion();
    CM::methods[CM::SquadMember].value.method_info=reinterpret_cast<void*>(size_t(CM::SquadMember)+1);
    // Face disabled: no face VMD is passed on.
    {auto config=Config();config->face=false;r.applied=config;std::atomic_store(&CM::config,r.applied);}
    RequestCharacterDirectorLoad(Cast(2));PumpCharacterMotion();
    CHECK(EiemFake::actors[0].face.empty()&&EiemFake::actors[1].face.empty());
    CM::requests.store(16);PumpCharacterMotion();

    // Configuration invalidation dominates a running session.
    r.applied=Config();std::atomic_store(&CM::config,r.applied);
    CM::requests.store(1);PumpCharacterMotion();Ready();CHECK(Dancing()==1);
    std::atomic_store(&CM::config,std::make_shared<const CM::Config>());PumpCharacterMotion();CHECK(!r.Playing());CHECK(pins.empty());
    // Disabled: a director load is refused immediately.
    RequestCharacterDirectorLoad("C:/w/motion.vmd","");PumpCharacterMotion();CHECK(CM::director_phase.load()==CM::DirectorFailed);
    r.applied=Config();std::atomic_store(&CM::config,r.applied);

    // Two Animators under the model: refuse rather than animate a sub-rig.
    leader.model.animators.push_back(&leader.animator);
    CM::requests.store(1);PumpCharacterMotion();CHECK(!r.Loading());CHECK(pins.empty());leader.model.animators.pop_back();

    // Losing ownership stops without touching the next writer.
    CM::requests.store(1);PumpCharacterMotion();Ready();CHECK(Dancing()==1);
    CHECK(registry.Release(&leader.root,CM::kOwner,r.actors[0].lease));
    auto next_writer=registry.Acquire(&leader.root,"next writer");CHECK(next_writer);
    PumpCharacterMotion();CHECK(!r.Playing());CHECK(pins.empty());CHECK(registry.Owns(&leader.root,"next writer",next_writer));
    CHECK(registry.Release(&leader.root,"next writer",next_writer));

    // A foreign thread never drives EIEM.
    CM::requests.store(1);
    const int loads=EiemFake::actors[0].loads;
    std::thread foreign_thread([]{PumpCharacterMotion(false);});foreign_thread.join();
    CHECK(EiemFake::actors[0].loads==loads);CHECK(CM::requests.load()==1);CM::requests.store(0);

    // EIEM unavailable on this client: play fails cleanly.
    EiemFake::available=false;CM::requests.store(1);PumpCharacterMotion();CHECK(!r.Loading());CHECK(pins.empty());EiemFake::available=true;

    // Opt-in flags are parsed by production CameraConfiguration.
    auto c=ParseConfiguration("enabled=true\nvmd_body_enabled=true\nvmd_terrain_enabled=true\nvmd_motion_scale=9\nvmd_motion_file=C:/dance.vmd\nvmd_motion_hotkey=F11\nhotkey_layout=2\n");
    CHECK(c.vmd_body_enabled&&c.vmd_terrain_enabled&&!c.vmd_face_enabled);CHECK(nearly(c.vmd_motion_scale,5));CHECK(c.vmd_motion_key==VK_F1+10);CHECK(c.vmd_motion_file=="C:/dance.vmd");
    CHECK(nearly(ParseConfiguration("vmd_motion_scale=0\n").vmd_motion_scale,0.05));
    CHECK(nearly(ParseConfiguration("").vmd_motion_scale,1));
    // Cloth mode: EIEM's playback service by default; unknown values keep it.
    CHECK(ParseConfiguration("").vmd_cloth_mode==1);
    CHECK(ParseConfiguration("vmd_cloth_mode=game\n").vmd_cloth_mode==0);
    CHECK(ParseConfiguration("vmd_cloth_mode=Freeze\n").vmd_cloth_mode==2);
    CHECK(ParseConfiguration("vmd_cloth_mode=stable\n").vmd_cloth_mode==1);
    CHECK(ParseConfiguration("vmd_cloth_mode=whatever\n").vmd_cloth_mode==1);
    // Layout 1 hotkeys are replaced by the MMD numpad defaults (F8 is no longer bound).
    CHECK(ParseConfiguration("vmd_motion_hotkey=F11\nkeyframe_clear_hotkey=NUMPAD4\n").vmd_motion_key==0);
    CHECK(ParseConfiguration("keyframe_clear_hotkey=NUMPAD4\n").keyframe_clear_key==0);
    CHECK(ParseConfiguration("mmd_play_hotkey=NUMPAD_ENTER\nhotkey_layout=2\n").mmd_play_key==kVkNumpadEnter);
    // Shutdown from Host's thread: no Unity calls, EIEM workers stopped.
    StopCharacterMotion();CHECK(EiemFake::shutdowns==1);
    g_host=nullptr;
    std::cout<<"PASS production character adapter/lifecycle/squad: "<<checks<<" checks\n";
}
