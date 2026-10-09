#include "core/hook_broker.h"
#include <atomic>
#include <cassert>
#include <chrono>
#include <thread>
#include <vector>
#include <iostream>
namespace betterendfieldnext { void LogError(const char*,const char*) {} void LogInfo(const char*,const char*) {} }
static std::atomic_int inside{0}, maximum{0}, installs{0}, commits{0}, destroys{0};
static bool failInstall=false;
extern "C" int DobbyHook(void*,void*,void**){return -1;} // Brokers must patch through the chain.
extern "C" int DobbyPrepare(void*,void*,void** original){
    int n=++inside; if(n>maximum) maximum=n; ++installs;
    std::this_thread::sleep_for(std::chrono::milliseconds(1));
    if(!failInstall)*original=reinterpret_cast<void*>(0xabc);
    --inside;return failInstall?-1:0;
}
extern "C" int DobbyCommit(void*) {++commits;return 0;}
extern "C" int DobbyDestroy(void*) {++destroys;return 0;}
int main(){
    using betterendfieldnext::HookBroker;HookBroker a,b;std::string error;
    void* target=reinterpret_cast<void*>(0x1000);void* replacement=reinterpret_cast<void*>(0x2000);
    void* other=reinterpret_cast<void*>(0x3000);
    void *stub=nullptr,*original=nullptr,*second=nullptr,*second_original=nullptr;
    assert(a.Install(target,replacement,&original,stub,error));assert(original&&original!=reinterpret_cast<void*>(0xabc));
    // Another owner joins the chain on the same target; the target is patched once.
    assert(b.Install(target,other,&second_original,second,error));assert(second&&second_original&&installs==1&&commits==1);
    // The same owner cannot hook one target twice; its live original is untouched.
    void* kept=original;void* duplicate=nullptr;
    assert(!a.Install(target,other,&original,duplicate,error));assert(!duplicate&&original==kept&&!error.empty());
    void* foreign=stub;assert(!b.Remove(foreign)&&foreign==stub&&destroys==0);
    void* stale=stub;assert(a.Remove(stub)&&!stub&&destroys==0);
    // Re-registration of the same detour restores the node and its original.
    assert(a.Install(target,replacement,&original,stub,error));assert(stub!=stale&&original==kept&&installs==1);
    assert(!a.Remove(stale));assert(a.Remove(stub));assert(b.Remove(second));
    // Named owners: two modules hosted by one broker share a target.
    void *m=nullptr,*n=nullptr,*m_original=nullptr,*n_original=nullptr,*m_again=nullptr;
    void* shared=reinterpret_cast<void*>(0x5000);
    assert(a.Install("betterendfieldnext.model",shared,replacement,&m_original,m,error));
    assert(a.Install("betterendfieldnext.custom_model",shared,other,&n_original,n,error));
    assert(!a.Install("betterendfieldnext.model",shared,other,&m_original,m_again,error)&&!m_again);
    assert(a.Remove(m)&&a.Remove(n));
    failInstall=true;void* failed=nullptr;
    assert(!a.Install(reinterpret_cast<void*>(0x6000),replacement,&original,failed,error));assert(!failed&&destroys==0);
    failInstall=false;assert(b.Install(reinterpret_cast<void*>(0x6000),replacement,&original,failed,error));assert(b.Remove(failed));
    std::vector<std::thread> workers;
    for(int i=0;i<16;++i)workers.emplace_back([i,replacement]{HookBroker owner;void* handle=nullptr;void* orig=nullptr;std::string e;assert(owner.Install(reinterpret_cast<void*>(0x8000+i*16),replacement,&orig,handle,e));assert(owner.Remove(handle));});
    for(auto& worker:workers)worker.join();assert(maximum==1);
    std::cout<<"PASS hook chain sharing, duplicate rejection, rollback, stale handle, serialization\n";
}
