// The non-Windows target compiles Actions with its existing platform shim and
// enables only the newly added lease guard; it does not execute game functions.
#include "../../modules/actions/module.cpp"
#include "../../shared/motion/pose_lease_registry.h"
#include "test_support.h"
using namespace BetterEndfield;
using namespace BetterEndfield::Actions;
static Motion::PoseLeaseRegistry registry;
static uint64_t BE_CALL Acquire(const void* r,const char* o){return registry.Acquire(r,o);}
static int BE_CALL Owns(const void* r,const char* o,uint64_t t){return registry.Owns(r,o,t);}
static int BE_CALL ReleaseLease(const void* r,const char* o,uint64_t t){return registry.Release(r,o,t);}
static int freed=0,invokes=0;
static void BE_CALL Free(void*,uint32_t){++freed;}
static void* BE_CALL InvokeUnexpected(void*,const void*,void*,void**,void**){++invokes;return nullptr;}
int main() {
    BE_HostApiV1 host{};host.gchandle_free=Free;host.runtime_invoke=InvokeUnexpected;g_host=&host;
    const BE_PoseLeaseApiV1 api{1,Acquire,Owns,ReleaseLease};g_pose_leases=&api;g_pose_lease_ready=true;
    CHECK(BetterEndfield_ActionsPoseLeaseVersionV1()==1);
    auto root=reinterpret_cast<void*>(42);
    auto lease=registry.Acquire(root,kId);
    PoseOwner owner;owner.root=root;owner.lease=lease;owner.root_pin=10;
    FreePoseOwner(owner);CHECK(!registry.Owns(root,kId,lease));CHECK(freed==1);
    lease=registry.Acquire(root,kId);g_pose_owner.root=root;g_pose_owner.lease=lease;g_pose_owner.root_pin=11;
    ReleasePoseOverlay(false);CHECK(!registry.Owns(root,kId,lease));CHECK(freed==2);
    // A stale session must neither restore the new writer's bones nor release its lease.
    auto old=registry.Acquire(root,kId);CHECK(registry.Release(root,kId,old));auto next=registry.Acquire(root,"camera");
    g_pose_owner.root=root;g_pose_owner.lease=old;g_pose_owner.component=reinterpret_cast<void*>(1);
    ReleasePoseOverlay(true);CHECK(registry.Owns(root,"camera",next));CHECK(invokes==0);
    g_pose_owner.root=root;g_pose_owner.lease=old;g_pose_owner.component=reinterpret_cast<void*>(1);
    g_game_thread=GetCurrentThreadId();ApplyPoseOverlay(g_pose_owner.component,0.1f);
    CHECK(!g_pose_owner.root);CHECK(invokes==0);CHECK(registry.Owns(root,"camera",next));
    CHECK(registry.Release(root,"camera",next));g_pose_lease_ready=false;CHECK(BetterEndfield_ActionsPoseLeaseVersionV1()==0);g_host=nullptr;
    std::cout<<"PASS production Actions lease release and stale-writer guards: "<<checks<<" checks\n";
}
