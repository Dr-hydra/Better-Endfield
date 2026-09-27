#include "../../modules/camera/module.cpp"
#include "../../shared/motion/pose_lease_registry.h"
#include "test_support.h"
#include <deque>
using namespace BetterEndfield;
using namespace BetterEndfield::CameraModule;
namespace CM=BetterEndfield::CameraModule::CharacterMotion;
using Q=CharacterPose::Quaternion;
struct Object {
    std::string name;
    Q rotation;
    bool alive=true,child=true;
    Object* transform=nullptr;
    Object* mesh=nullptr;
    std::vector<Object*> children;
    std::vector<float> weights{20};
};
Object model,root,animator,mesh,renderer,character,modelComponent;
std::deque<Object> bones;
std::deque<std::string> texts;
std::vector<Object*> animators,renderers;
Object* current_model=&model;
std::map<uint32_t,void*> pins;
uint32_t next_pin=1;
int frame_count=1;
bool reenter=false;
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
    if(id==1001)return &character;
    if(id==1002)return &modelComponent;
    if(id==1003)return current_model;
    if(id==1004)return &root;
    switch(CM::Id(id-1)) {
        case CM::Alive:return Box(a[0]&&static_cast<Object*>(a[0])->alive);
        case CM::Transform:return object->transform;
        case CM::GameObject:return &model;
        case CM::Children:return a[0]==reinterpret_cast<void*>(201)?static_cast<void*>(&animators):static_cast<void*>(&renderers);
        case CM::ArrayLength:return Box(int(static_cast<std::vector<Object*>*>(instance)->size()));
        case CM::ArrayValue:return static_cast<std::vector<Object*>*>(instance)->at(*static_cast<int*>(a[0]));
        case CM::LocalGet:case CM::WorldGet:return Box(object->rotation);
        case CM::LocalSet:object->rotation=*static_cast<Q*>(a[0]);if(reenter)PumpCharacterMotion();return nullptr;
        case CM::IsChild:return Box(object->child&&object->alive&&a[0]==&root);
        case CM::ChildCount:return Box(int(object->children.size()));
        case CM::ChildAt:return object->children.at(*static_cast<int*>(a[0]));
        case CM::Name:return &object->name;
        case CM::FrameCount:return Box(frame_count);
        case CM::SharedMesh:return object->mesh;
        case CM::ShapeIndex:return Box(*static_cast<std::string*>(a[0])=="A"?0:-1);
        case CM::ShapeCount:return Box(1);
        case CM::WeightGet:return Box(object->weights.at(*static_cast<int*>(a[0])));
        case CM::WeightSet:object->weights.at(*static_cast<int*>(a[0]))=*static_cast<float*>(a[1]);return nullptr;
    }
    *exception=reinterpret_cast<void*>(1);return nullptr;
}
void* BE_CALL UnboxFake(void*,void* v){return v;}
uint32_t BE_CALL PinFake(void*,void* p,int){auto id=next_pin++;pins[id]=p;return id;}
void BE_CALL UnpinFake(void*,uint32_t p){pins.erase(p);}
void* BE_CALL StringFake(void*,const char* name){texts.emplace_back(name);return &texts.back();}
int BE_CALL CopyFake(void*,const void* text,char* output,size_t n){auto& s=*static_cast<const std::string*>(text);auto count=std::min(n-1,s.size());std::memcpy(output,s.data(),count);output[count]=0;return int(count);}
void BE_CALL LogFake(void*,const char*,const char*){}
static BE_HostApiV1 host{};
static auto Config() {
    auto config=std::make_shared<CM::Config>();config->enabled=config->body=config->eyes=config->morphs=true;return config;
}
static auto Clip() {
    auto motion=std::make_shared<Vmd::Motion>();
    for(auto& spec:CharacterPose::DefaultRig()) {
        if(bones.size()>=6)break;
        if(spec.eye)continue;
        Object bone;bone.name=spec.target;bones.push_back(bone);root.children.push_back(&bones.back());
        Vmd::BoneKey a,b;b.frame=30;b.rotation={0,0,.70710678f,.70710678f};
        for(int c=0;c<4;++c){b.interpolation[c]=b.interpolation[c+4]=20;b.interpolation[c+8]=b.interpolation[c+12]=107;}
        motion->bones[spec.sources[0]]={a,b};
    }
    auto name=CharacterPose::DefaultMorphs()[0].sources[0];motion->morphs[name]={{0,0},{30,1}};
    return motion;
}
int main() {
    host.abi_version=1;host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.gchandle_new=PinFake;host.gchandle_free=UnpinFake;
    host.string_new=StringFake;host.copy_managed_string=CopyFake;host.log=LogFake;g_host=&host;
    for(size_t i=0;i<std::size(CM::methods);++i)CM::methods[i].value.method_info=reinterpret_cast<void*>(i+1);
    Contract("player_controller.get_main_character")->method_info=reinterpret_cast<void*>(1001);
    Contract("entity.get_model_com")->method_info=reinterpret_cast<void*>(1002);
    Contract("base_model_component.get_model_go")->method_info=reinterpret_cast<void*>(1003);
    Contract("unity.game_object.transform")->method_info=reinterpret_cast<void*>(1004);
    root.name="rig";root.transform=&root;animator.transform=&root;renderer.transform=&root;renderer.mesh=&mesh;
    animators={&animator};renderers={&renderer};model.transform=&root;
    const auto clip=Clip();
    auto& r=CM::runtime;r.ready=true;r.morph_ready=true;r.leases=&leases;r.applied=Config();CM::config.store(r.applied);
    r.animator_class.type_object=reinterpret_cast<void*>(201);r.renderer_class.type_object=reinterpret_cast<void*>(202);r.thread_id=GetCurrentThreadId();
    auto prepare=[&]{r.pending_model=&model;r.pending_pin=PinFake(nullptr,&model,1);};
    auto foreign=registry.Acquire(&root,"actions");prepare();CHECK(!r.Bind(clip));r.Stop(false,"test");
    CHECK(registry.Owns(&root,"actions",foreign));CHECK(pins.empty());CHECK(registry.Release(&root,"actions",foreign));
    prepare();CHECK(r.Bind(clip));r.ClearPending();CHECK(r.session.Active());CHECK(!pins.empty());
    std::string error;CHECK(r.session.Apply(30,1,r,error));CHECK(near(bones[0].rotation.z,.70710678));CHECK(near(renderer.weights[0],100));
    // No rebind to a switched model; owned unchanged values restored and pins released.
    Object new_model;current_model=&new_model;PumpCharacterMotion();CHECK(!r.session.Active());CHECK(pins.empty());
    CHECK(near(bones[0].rotation.z,0));CHECK(near(renderer.weights[0],20));current_model=&model;
    prepare();CHECK(r.Bind(clip));r.ClearPending();CHECK(r.session.Apply(30,1,r,error));
    // Reparent one bone out of the rig. Do not write it during restoration.
    bones[0].child=false;CHECK(!r.session.Apply(10,1,r,error));r.Stop(true,"test");CHECK(near(bones[0].rotation.z,.70710678));CHECK(near(bones[1].rotation.z,0));CHECK(pins.empty());bones[0].child=true;bones[0].rotation={};
    prepare();CHECK(r.Bind(clip));r.ClearPending();CHECK(r.session.Apply(30,1,r,error));
    Object next_mesh;renderer.mesh=&next_mesh;CHECK(!r.session.Apply(10,1,r,error));r.Stop(true,"test");
    CHECK(near(renderer.weights[0],100));CHECK(pins.empty());renderer.mesh=&mesh;renderer.weights[0]=20;
    prepare();CHECK(r.Bind(clip));r.ClearPending();r.clock.Start(NowSeconds()-.5,1,false);reenter=true;++frame_count;
    PumpCharacterMotion();CHECK(r.frames==1);CHECK(r.session.Active());reenter=false;
    CM::requests.store(4);PumpCharacterMotion();CHECK(!r.session.Active());CHECK(pins.empty());
    // Configuration invalidation dominates stale pending results/requests.
    prepare();CHECK(r.Bind(clip));r.ClearPending();auto generation=r.generation;CM::config.store(std::make_shared<const CM::Config>());
    PumpCharacterMotion();CHECK(!r.session.Active());CHECK(r.generation>generation);CHECK(pins.empty());
    r.applied=Config();CM::config.store(r.applied);animators.push_back(&animator);prepare();CHECK(!r.Bind(clip));r.Stop(false,"test");CHECK(pins.empty());animators.pop_back();
    // A callback on a foreign thread must not consume a queued file completion.
    r.files.Start();
    CHECK(r.files.Submit({CameraFiles::Kind::LoadMotion,"",998,{}}));
    std::this_thread::sleep_for(std::chrono::milliseconds(30));
    std::thread foreign_thread([]{PumpCharacterMotion(false);});foreign_thread.join();
    std::optional<CameraFiles::Result> completion;
    for(int i=0;i<200&&!completion;++i){completion=r.files.Poll();if(!completion)std::this_thread::sleep_for(std::chrono::milliseconds(1));}
    CHECK(completion&&completion->generation==998);
    CHECK(completion&&!completion->error.empty());
    r.files.Stop();
    // Losing ownership must never restore over the next writer's output.
    prepare();CHECK(r.Bind(clip));r.ClearPending();CHECK(r.session.Apply(30,1,r,error));
    CHECK(registry.Release(&root,CM::kOwner,r.lease));
    auto next_writer=registry.Acquire(&root,"next writer");CHECK(next_writer);
    PumpCharacterMotion();CHECK(!r.session.Active());CHECK(pins.empty());
    CHECK(near(bones[0].rotation.z,.70710678));
    CHECK(registry.Release(&root,"next writer",next_writer));
    // Opt-in flags and weight are parsed by production CameraConfiguration.
    auto c=ParseConfiguration("enabled=true\nvmd_body_enabled=true\nvmd_eyes_enabled=true\nvmd_motion_weight=5\nvmd_motion_file=C:/dance.vmd\nvmd_motion_hotkey=F11\n");
    CHECK(c.vmd_body_enabled&&c.vmd_eyes_enabled&&!c.vmd_face_enabled);CHECK(c.vmd_motion_weight==1);CHECK(c.vmd_motion_key==VK_F1+10);CHECK(c.vmd_motion_file=="C:/dance.vmd");
    r.Stop(false,"test end");r.files.Stop();g_host=nullptr;
    std::cout<<"PASS production character adapter/lifecycle: "<<checks<<" checks\n";
}
