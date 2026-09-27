#include "core/hook_broker.h"
#include <atomic>
#include <cassert>
#include <chrono>
#include <thread>
#include <vector>
#include <iostream>
namespace betterendfield { void LogError(const char*,const char*) {} }
static std::atomic_int inside{0}, maximum{0}, installs{0}, destroys{0};
static bool failInstall=false, failDestroy=false;
extern "C" int DobbyHook(void*,void*,void** original){
    int n=++inside; if(n>maximum) maximum=n; ++installs;
    std::this_thread::sleep_for(std::chrono::milliseconds(1));
    if(!failInstall)*original=reinterpret_cast<void*>(0xabc);
    --inside;return failInstall?-1:0;
}
extern "C" int DobbyDestroy(void*) {++destroys;return failDestroy?-1:0;}
int main(){
    using betterendfield::HookBroker;HookBroker a,b;std::string error;
    void* target=reinterpret_cast<void*>(0x1000);void* replacement=reinterpret_cast<void*>(0x2000);
    void *stub=nullptr,*original=nullptr,*second=nullptr;
    assert(a.Install(target,replacement,&original,stub,error));assert(original);
    assert(!b.Install(target,replacement,&original,second,error));assert(!second&&installs==1);
    void* foreign=stub;assert(!b.Remove(foreign)&&foreign==stub&&destroys==0);
    failDestroy=true;assert(!a.Remove(stub)&&stub);failDestroy=false;
    void* stale=stub;assert(a.Remove(stub)&&!stub);
    assert(a.Install(target,replacement,&original,stub,error));assert(stub!=stale);
    assert(!a.Remove(stale));assert(a.Remove(stub));
    failInstall=true;assert(!a.Install(target,replacement,&original,stub,error));assert(!stub);
    failInstall=false;assert(b.Install(target,replacement,&original,stub,error));assert(b.Remove(stub));
    std::vector<std::thread> workers;
    for(int i=0;i<16;++i)workers.emplace_back([i,replacement]{HookBroker owner;void* handle=nullptr;void* orig=nullptr;std::string e;assert(owner.Install(reinterpret_cast<void*>(0x4000+i*16),replacement,&orig,handle,e));assert(owner.Remove(handle));});
    for(auto& worker:workers)worker.join();assert(maximum==1);
    std::cout<<"PASS hook ownership, duplicate rejection, rollback, failed remove, stale handle, serialization\n";
}
