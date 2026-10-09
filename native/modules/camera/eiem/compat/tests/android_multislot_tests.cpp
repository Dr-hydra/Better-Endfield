#define BE_EIEM_FIXTURE_LIBRARY
#include "android_slot_tests.cpp"
namespace Body=BetterEndfieldNext::EiemBody;
void WaitLoad(int slot){
  for(int n=0;n<400;++n){if(Body::LoadStatus(slot)==Body::LoadState::Ready)return;assert(Body::LoadStatus(slot)!=Body::LoadState::Failed);std::this_thread::sleep_for(std::chrono::milliseconds(2));}
  assert(false);
}
int main(){
  using namespace fixture;
  auto h=Host();assert(Body::Initialize(&h,L"offline"));
  Body::Options options;options.face=true;options.cloth=Body::ClothMode::Stable;options.terrain=true;Body::SetOptions(options);
  std::array<Object*,4> owners{}, animators{}, roots{}, bipeds{};
  for(int i=0;i<4;++i){
    assert(Body::EnsureActor(i));owners[i]=New(FindClass("Entity"));animators[i]=New(FindClass("Animator"));roots[i]=MakeRig(animators[i]);MakeSmc(owners[i]);
    bipeds[i]=New(FindClass("BipedIK"));bipeds[i]->root=roots[i];
    Body::SetTarget(i,owners[i],animators[i],New(FindClass("MovementComponent")));Body::Load(i,"native/modules/camera/eiem/compat/tests/fixtures/slot"+std::to_string(i)+".vmd","");WaitLoad(i);assert(Body::DurationSeconds(i)==1);assert(Body::Start(i,"offline-four-actors"));assert(!animators[i]->enabled);
  }
  std::array<float,4> initial{};
  for(int n=0;n<30;++n){Body::PublishClock(0,false);std::this_thread::sleep_for(std::chrono::milliseconds(2));}
  for(int i=0;i<4;++i)initial[i]=animators[i]->human[0]->position.x;
  for(int n=0;n<30;++n){Body::PublishClock(.5,false);std::this_thread::sleep_for(std::chrono::milliseconds(2));}
  for(int i=0;i<4;++i){const float moved=animators[i]->human[0]->position.x-initial[i];assert(std::fabs(moved)>.01f);if(i)assert(std::fabs(moved)>std::fabs(animators[i-1]->human[0]->position.x-initial[i-1])+.001f);}
  const float flatHeight=animators[0]->human[0]->position.y;
  floorBase=.3f;
  for(int tick=0;tick<90;++tick){Body::PublishClock(.5+tick/60.0,true);std::this_thread::sleep_for(std::chrono::milliseconds(2));}
  assert(Body::TerrainAvailable(0));assert(Body::TerrainUnavailableReason(0).empty());
  assert(floorQueries>100&&animators[0]->human[0]->position.y>flatHeight+.10f);
  for(int n=0;n<20;++n){Body::PublishClock(2,false);std::this_thread::sleep_for(std::chrono::milliseconds(2));}
  const auto paused=animators[0]->human[0]->position;
  for(int n=0;n<10;++n){Body::PublishClock(2,false);std::this_thread::sleep_for(std::chrono::milliseconds(2));}
  assert(DirectVmdLength(DirectVmdSub(paused,animators[0]->human[0]->position))<1e-6f);
  // This is a callback on owned fixture objects, not a process/game hook.
  auto solve=reinterpret_cast<void(*)(void*,void*)>(bipedDetour);assert(solve);
  solve(bipeds[0],nullptr);assert(originalBipedCalls==0);
  auto* other=New(FindClass("BipedIK"));other->root=New(FindClass("Transform"));solve(other,nullptr);assert(originalBipedCalls==1);
  assert(setPoseCalls>=4&&faceUpdates>=4);
  for(int i=0;i<4;++i){Body::Stop(i,"offline-background");assert(animators[i]->enabled);assert(!Body::Active(i));assert(Body::Start(i,"offline-restart"));}
  for(int n=0;n<15;++n){Body::PublishClock(.1,false);std::this_thread::sleep_for(std::chrono::milliseconds(2));}
  for(int i=0;i<4;++i){Body::Stop(i,"offline-stop");Body::SetTarget(i,nullptr,nullptr,nullptr);}
  Body::Shutdown();assert(handles.empty());
  std::cout<<"PASS: four independent slots + async real VMD loading/sampling + director seek/pause + owned FinalIK suppression/pass-through + SMC evaluation + actual managed body terrain height application + background Stop/Start + shutdown GC cleanup\n";
}
