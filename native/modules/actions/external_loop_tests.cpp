// Production ownership restoration, with a managed API test double.
#include "module.cpp"
#include <cstdlib>
#include <vector>
using namespace BetterEndfield::Actions;
#define CHECK(x) do { if (!(x)) { std::fprintf(stderr,"line %d: %s\n",__LINE__,#x);std::exit(1); } } while(false)
namespace {
struct Stub {bool alive=true;void* controller=nullptr;};
std::vector<uint32_t> freed;
int calls=0,sets=0,destroys=0;
bool fail_set=false;
bool fail_asset=false;int64_t asset_hash_seen=0;void* asset_type_seen=nullptr;
void* BE_CALL FakeInvoke(void*,const void* method,void* instance,void** args,void** exception){
 *exception=nullptr;++calls;auto id=static_cast<MethodId>(reinterpret_cast<uintptr_t>(method)-1);
 switch(id){
 case UnityAlive:return &static_cast<Stub*>(args[0])->alive;
 case ControllerGet:return static_cast<Stub*>(instance)->controller;
 case ControllerSet:
  CHECK(g_external_owner.component==nullptr);++sets;
  if(fail_set){*exception=reinterpret_cast<void*>(1);return nullptr;}
  static_cast<Stub*>(instance)->controller=args[0];return nullptr;
 case DestroyOwned:++destroys;return nullptr;
 case BundleAsset:
  CHECK(instance==g_external_assets.bundle);
  asset_hash_seen=*static_cast<int64_t*>(args[0]);asset_type_seen=args[1];
  if(fail_asset){*exception=reinterpret_cast<void*>(1);return nullptr;}
  return instance;
 default:CHECK(false);return nullptr;
 }
}
void* BE_CALL FakeUnbox(void*,void* object){return object;}
void BE_CALL FakeFree(void*,uint32_t id){freed.push_back(id);}
void Own(Stub& actor,Stub& animator,Stub& original,Stub& wrapper){
 freed.clear();calls=sets=destroys=0;fail_set=false;
 animator.controller=&wrapper;g_external_owner={&actor,&animator,&original,&wrapper,1,2,3,4,true};
}
}
int main(){
 CHECK(!Configuration{}.external_loop);
 BE_HostApiV1 host{};host.runtime_invoke=&FakeInvoke;host.object_unbox=&FakeUnbox;host.gchandle_free=&FakeFree;g_host=&host;
 for(int i=0;i<MethodCount;i++)g_methods[i].resolved.method_info=reinterpret_cast<void*>(static_cast<uintptr_t>(i+1));
 Stub actor,animator,original,wrapper,foreign;
 g_external_assets.bundle=&original;bool asset_ok=true;
 CHECK(LoadNativeExternalClip(0,&actor,asset_ok)==&original&&asset_ok);
 CHECK(asset_hash_seen==0x0EC405CB4A5FF5ECll&&asset_type_seen==&actor);
 CHECK(LoadNativeExternalClip(1,&actor,asset_ok)==&original&&asset_ok);
 CHECK(asset_hash_seen==0x00AB35F9779D7048ll);
 fail_asset=true;CHECK(!LoadNativeExternalClip(0,&actor,asset_ok)&&!asset_ok);fail_asset=false;
 int calls_before=calls;asset_ok=true;CHECK(!LoadNativeExternalClip(2,&actor,asset_ok)&&!asset_ok&&calls==calls_before);
 g_external_assets={};
 Own(actor,animator,original,wrapper);RestoreExternal(true);
 CHECK(animator.controller==&original&&sets==1&&destroys==1&&freed.size()==4&&!g_external_owner.component);
 RestoreExternal(true);CHECK(sets==1&&destroys==1&&freed.size()==4);
 Own(actor,animator,original,wrapper);animator.controller=&foreign;RestoreExternal(true);
 CHECK(animator.controller==&foreign&&sets==0&&destroys==0&&freed.size()==4);
 Own(actor,animator,original,wrapper);RestoreExternal(false);
 CHECK(calls==0&&sets==0&&destroys==0&&freed.size()==4);
 Own(actor,animator,original,wrapper);fail_set=true;RestoreExternal(true);
 CHECK(g_external_owner.component==&actor&&g_external_owner.pending_restore&&freed.empty()&&destroys==0);
 fail_set=false;RestoreExternal(true);CHECK(animator.controller==&original&&freed.size()==4&&destroys==1);
 ImportedLoopSchedule loop;Frame f{};f.allowed=f.current_special=true;f.length=208.f/60;f.delta=1.f/60;
 f.time=.5f;CHECK(loop.Update(true,f)==LoopAction::None);
 for(int i=0;i<100;i++){
  f.time=ImportedLoopSchedule::begin+.002f;CHECK(loop.Update(true,f)==LoopAction::Blend);loop.Submitted(f.time);
  f.time=ImportedLoopSchedule::target+.006f;CHECK(loop.Update(true,f)==LoopAction::None);CHECK(!loop.pending);
 }
 f.time=ImportedLoopSchedule::begin;loop.Submitted(f.time);f.time=.087f;CHECK(loop.Update(true,f)==LoopAction::Abort);
 loop={};f.time=ImportedLoopSchedule::begin;loop.Submitted(f.time);f.delta=.6f;CHECK(loop.Update(true,f)==LoopAction::Abort);
 loop={};f.delta=.016f;f.outgoing=true;CHECK(loop.Update(true,f)==LoopAction::Abort);
 f.outgoing=false;f.length=0;CHECK(loop.Update(true,f)==LoopAction::Abort);
 std::puts("Actions external clip ownership and closed-loop timing: all checks passed.");
}
