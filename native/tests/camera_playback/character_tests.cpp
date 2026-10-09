#include "../../shared/motion/character_pose.h"
#include "../../shared/motion/character_mapping.h"
#include "../../shared/motion/pose_lease_registry.h"
#include "test_support.h"
#include <atomic>
#include <thread>
using namespace BetterEndfieldNext;
using namespace CharacterPose;
struct Fake : Backend {
    std::map<uintptr_t,Quaternion> rotations;
    std::map<std::pair<uintptr_t,int>,float> weights;
    int writes=0,fail_at=-1;
    bool ValidBone(uintptr_t t)override{return rotations.contains(t);}
    bool ReadRotation(uintptr_t t,Quaternion& q)override {if(!ValidBone(t))return false;q=rotations.at(t);return true;}
    bool WriteRotation(uintptr_t t,Quaternion q)override {if(!ValidBone(t)||++writes==fail_at)return false;rotations[t]=q;return true;}
    bool ValidMorph(uintptr_t t,int i)override{return weights.contains({t,i});}
    bool ReadMorph(uintptr_t t,int i,float& w)override {if(!ValidMorph(t,i))return false;w=weights.at({t,i});return true;}
    bool WriteMorph(uintptr_t t,int i,float w)override {if(!ValidMorph(t,i)||++writes==fail_at)return false;weights[{t,i}]=w;return true;}
};
static std::shared_ptr<const Vmd::Motion> Clip() {
    auto m=std::make_shared<Vmd::Motion>();
    Vmd::BoneKey a,b;b.frame=30;b.rotation={0,0,0.70710678f,0.70710678f};
    // Linear Bezier channels.
    for(int c=0;c<4;++c){b.interpolation[c]=20;b.interpolation[c+4]=20;b.interpolation[c+8]=107;b.interpolation[c+12]=107;}
    m->bones["arm"]={a,b};m->bones["eye"]={a,b};
    m->morphs["blink"]={{0,0},{30,1}};m->morphs["wink"]={{0,0},{30,.6f}};
    m->last_frame=30;return m;
}
int main() {
    Fake f;f.rotations[1]={};f.rotations[2]={};f.weights[{3,0}]=15;
    Bone b;b.target=1;b.sources={"arm"};
    Morph m;m.target=3;m.index=0;m.sources={"blink","wink"};
    Session s;std::string error;
    CHECK(s.Start(Clip(),{b},{m},f,error));
    CHECK(s.Apply(0,1,f,error));CHECK(Same(f.rotations[1],{}));CHECK(nearly(f.weights[{3,0}],0));
    CHECK(s.Apply(30,1,f,error));CHECK(nearly(f.rotations[1].z,.70710678));CHECK(nearly(f.weights[{3,0}],100));
    CHECK(s.Apply(30,1,f,error));CHECK(nearly(f.rotations[1].z,.70710678)); // no accumulated output
    s.Stop(f);CHECK(Same(f.rotations[1],{}));CHECK(nearly(f.weights[{3,0}],15));
    CHECK(!s.Active());
    CHECK(s.Start(Clip(),{b},{m},f,error));CHECK(s.Apply(30,1,f,error));
    Quaternion native{.70710678f,0,0,.70710678f};
    f.rotations[1]=native;f.weights[{3,0}]=33;s.Stop(f);
    CHECK(Same(f.rotations[1],native));CHECK(nearly(f.weights[{3,0}],33)); // external writer wins
    f.rotations[1]={};f.weights[{3,0}]=15;
    CHECK(s.Start(Clip(),{b},{m},f,error));CHECK(s.Apply(15,0,f,error));CHECK(Same(f.rotations[1],{}));CHECK(nearly(f.weights[{3,0}],15));
    CHECK(s.Apply(30,.5f,f,error));CHECK(nearly(f.rotations[1].z,std::sin(3.141592653589793/8)));CHECK(nearly(f.weights[{3,0}],57.5));
    s.Stop(f);
    Bone b2=b;b2.target=2;
    CHECK(!s.Start(Clip(),{b,b},{},f,error));
    CHECK(s.Start(Clip(),{b,b2},{m},f,error));f.fail_at=f.writes+2;
    CHECK(!s.Apply(30,1,f,error));CHECK(!s.Active());CHECK(Same(f.rotations[1],{}));CHECK(Same(f.rotations[2],{}));CHECK(nearly(f.weights[{3,0}],15));
    f.fail_at=-1;
    CHECK(s.Start(Clip(),{b},{m},f,error));CHECK(s.Apply(30,1,f,error));f.weights.erase({3,0});
    CHECK(!s.Apply(15,1,f,error));CHECK(!s.Active());CHECK(Same(f.rotations[1],{})); // mesh replacement restores remaining owned channels
    CHECK(s.Start(Clip(),{b},{},f,error));CHECK(!s.Apply(NAN,1,f,error));s.Stop(f);
    b.sources={"missing"};CHECK(!s.Start(Clip(),{b},{},f,error));b.sources={"arm"};
    f.rotations[1]={0,0,0,0};CHECK(!s.Start(Clip(),{b},{},f,error));f.rotations[1]={};
    Quaternion source0{0,.70710678f,0,.70710678f};
    CHECK(Same(RelativeRotation(native,source0,source0,source0,1),native));
    // Basis conjugation changes axes, not quaternion byte order.
    auto rotated=RelativeRotation({},source0,{},Quaternion{0,0,.70710678f,.70710678f},1);
    CHECK(nearly(std::abs(rotated.x),.70710678));CHECK(nearly(rotated.z,0));
    Clock clock;double t=-1;clock.Start(1,2,false);
    CHECK(clock.Advance(2,t)&&nearly(t,1));clock.Pause(true,2);
    CHECK(clock.Advance(20,t)&&nearly(t,1));clock.Pause(false,20);
    CHECK(clock.Advance(30,t)&&nearly(t,2));CHECK(!clock.Advance(31,t));
    clock.Start(1,0,false);CHECK(clock.Advance(1,t)&&t==0);CHECK(!clock.Advance(2,t));
    clock.Start(0,2,true);CHECK(clock.Advance(11,t)&&nearly(t,1));CHECK(!clock.Advance(10,t));CHECK(!clock.Advance(NAN,t));
    CHECK(DefaultRig().size()==46);CHECK(DefaultMorphs().size()==9);
    std::set<std::string> targets;for(auto& spec:DefaultRig())CHECK(targets.insert(spec.target).second);
    Motion::PoseLeaseRegistry leases;
    auto root=reinterpret_cast<void*>(0x1234);auto token=leases.Acquire(root,"camera");
    CHECK(token);CHECK(!leases.Acquire(root,"actions"));CHECK(!leases.Acquire(root,"camera"));
    CHECK(!leases.Release(root,"actions",token));CHECK(!leases.Release(root,"camera",token+1));CHECK(leases.Owns(root,"camera",token));
    CHECK(leases.Release(root,"camera",token));auto next=leases.Acquire(root,"actions");CHECK(next&&next!=token);
    CHECK(!leases.Release(root,"camera",token));CHECK(leases.Release(root,"actions",next));
    CHECK(!leases.Acquire(nullptr,"x"));CHECK(!leases.Acquire(root,""));
    std::atomic<int> inside{0},wins{0},violations{0};std::vector<std::thread> workers;
    for(int i=0;i<8;++i)workers.emplace_back([&]{for(int k=0;k<1000;++k){auto lease=leases.Acquire(root,"worker");if(!lease)continue;
        if(inside.fetch_add(1)!=0)++violations;++wins;inside.fetch_sub(1);leases.Release(root,"worker",lease);}});
    for(auto& w:workers)w.join();CHECK(wins>0);CHECK(violations==0);
    std::cout<<"PASS character FK/morph lifecycle, clock and cross-module lease policy: "<<checks<<" checks\n";
}
