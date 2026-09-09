#include "module.cpp"
#include <sstream>
#include <cstdlib>
using namespace BetterEndfield::Actions;
#define CHECK(x) do{if(!(x)){std::fprintf(stderr,"line %d: %s\n",__LINE__,#x);std::exit(1);}}while(false)
namespace {
struct Stub {bool alive=true;void* animator=nullptr;void* root=nullptr;BonePose pose;};
int calls=0,writes=0,frees=0;
void* BE_CALL InvokeFake(void*,const void* method,void* instance,void** args,void** exc){
    ++calls;*exc=nullptr;auto id=static_cast<MethodId>(reinterpret_cast<uintptr_t>(method)-1);
    switch(id){
        case UnityAlive:return &static_cast<Stub*>(args[0])->alive;
        case GetAnimator:return static_cast<Stub*>(instance)->animator;
        case PoseComponentTransform:return static_cast<Stub*>(instance)->root;
        case PoseIsChild:{static bool result;result=static_cast<Stub*>(instance)->root==args[0];return &result;}
        case PoseGetLocal:{auto p=static_cast<Stub*>(instance)->pose;*static_cast<PoseVector*>(args[0])=p.position;*static_cast<PoseQuaternion*>(args[1])=p.rotation;return nullptr;}
        case PoseSetLocal:static_cast<Stub*>(instance)->pose={*static_cast<PoseQuaternion*>(args[1]),*static_cast<PoseVector*>(args[0])};++writes;return nullptr;
        default:CHECK(false);return nullptr;
    }
}
void* BE_CALL UnboxFake(void*,void* p){return p;}
void BE_CALL FreeFake(void*,uint32_t){++frees;}
void Own(Stub& component,Stub& animator,Stub& root,Stub& bone){
    component.animator=&animator;animator.root=&root;bone.root=&root;
    g_pose_owner={};g_pose_owner.component=&component;g_pose_owner.animator=&animator;g_pose_owner.root=&root;
    g_pose_owner.component_pin=1;g_pose_owner.animator_pin=2;g_pose_owner.root_pin=3;
    PoseBinding binding;binding.transform=&bone;binding.pin=4;binding.wrote=true;binding.baseline.position={1,2,3};binding.last.position={4,5,6};bone.pose=binding.last;
    g_pose_owner.bindings.push_back(binding);calls=writes=frees=0;
}
}
int main(int argc,char** argv){
    CHECK(argc==2);std::ifstream file(argv[1],std::ios::binary);std::string bytes((std::istreambuf_iterator<char>(file)),{});CHECK(!bytes.empty());
    PoseBank bank;std::string error;std::istringstream valid(bytes);CHECK(bank.Load(valid,error));CHECK(bank.bones.size()==399&&bank.period==103);
    for(auto bad:{bytes.substr(0,bytes.size()-1),bytes+"x"}){PoseBank rejected;std::istringstream input(bad);CHECK(!rejected.Load(input,error));}
    for(size_t at:{size_t(12),size_t(20)}){auto bad=bytes;uint32_t invalid=0x7F800000;std::memcpy(bad.data()+at,&invalid,4);PoseBank rejected;std::istringstream input(bad);CHECK(!rejected.Load(input,error));}
    auto bad=bytes;bad[44]='/';std::istringstream pathBad(bad);PoseBank rejected;CHECK(!rejected.Load(pathBad,error));
    const double period=double(bank.period)/bank.fps;
    for(int side=0;side<2;side++)for(size_t i=0;i<bank.bones.size();i++){
        auto a=bank.Sample(side,i,.31),b=bank.Sample(side,i,.31+period);CHECK(PoseSame(a.position,b.position)&&PoseSame(a.rotation,b.rotation));
    }
    PoseClock clock;CHECK(!clock.Tick(1,.016f,true,0,.1f,208.f/60,bank.entry));CHECK(clock.Tick(2,.016f,true,0,.2f,208.f/60,bank.entry));
    double t=clock.seconds;CHECK(clock.Tick(2,.5f,true,0,.6f,208.f/60,bank.entry)&&clock.seconds==t);
    CHECK(clock.Tick(3,.016f,true,0,.474f,208.f/60,bank.entry)&&std::fabs(clock.seconds-t-.016)<1e-6);
    for(int i=4;i<14;i++)clock.Tick(i,.016f,true,0,.5f,208.f/60,bank.entry);
    CHECK(clock.weight==1);clock.End();CHECK(clock.Tick(14,.06f,false,-1,.2f,1,bank.entry));CHECK(!clock.Tick(15,.061f,false,-1,.2f,1,bank.entry)&&clock.state==PoseClock::State::Done);
    auto q=PoseSlerp({0,0,0,1},{0,0,0,-1},.5f);CHECK(PoseSame(q,{0,0,0,1}));
    BonePose last,baseline;last.position={4,5,6};baseline.position={1,2,3};auto clean=PoseNativeBaseline(last,last,baseline,true);CHECK(PoseSame(clean.position,baseline.position));
    auto fresh=last;fresh.position.x=9;clean=PoseNativeBaseline(fresh,last,baseline,true);CHECK(clean.position.x==9);
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.gchandle_free=FreeFake;g_host=&host;
    for(int i=0;i<MethodCount;i++)g_methods[i].resolved.method_info=reinterpret_cast<void*>(uintptr_t(i+1));
    Stub actor,animator,root,bone,foreign;
    Own(actor,animator,root,bone);ReleasePoseOverlay(true);CHECK(PoseSame(bone.pose.position,{1,2,3})&&writes==1&&frees==4);
    Own(actor,animator,root,bone);bone.pose.position={8,8,8};ReleasePoseOverlay(true);CHECK(PoseSame(bone.pose.position,{8,8,8})&&writes==0&&frees==4);
    Own(actor,animator,root,bone);bone.root=&foreign;ReleasePoseOverlay(true);CHECK(writes==0&&frees==4);
    Own(actor,animator,root,bone);animator.root=&foreign;ReleasePoseOverlay(true);CHECK(writes==0&&frees==4);
    Own(actor,animator,root,bone);ReleasePoseOverlay(false);CHECK(calls==0&&writes==0&&frees==4);
    std::puts("Pose overlay: bounded input, independent loop clock, fade, baseline preservation and scoped cleanup passed.");
}
