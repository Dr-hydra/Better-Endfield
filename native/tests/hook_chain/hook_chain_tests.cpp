#include "../../shared/hooks/hook_chain.h"
#include <cmath>
#include <iostream>
#include <thread>
#include <stdexcept>
#if defined(_WIN32)
#include "../../shared/host/hook_broker.h"
#include "../../shared/host/logging.h"
#endif
using namespace BetterEndfield::Hooks;
namespace {
unsigned checks=0;
void Check(bool value,const char* message){++checks;if(!value)throw std::runtime_error(message);}
using IntFn=int(*)(int,int,int,int,int,int);
IntFn next_a=nullptr,next_b=nullptr,next_c=nullptr;
std::string order;
std::atomic<bool> pause_b=false,entered_b=false,continue_b=false;
#if defined(_MSC_VER)
#define NOINLINE __declspec(noinline)
#else
#define NOINLINE __attribute__((noinline))
#endif
NOINLINE int Original(int a,int b,int c,int d,int e,int f){return a+2*b+3*c+4*d+5*e+6*f;}
int A(int a,int b,int c,int d,int e,int f){if(!pause_b.load())order+='A';return next_a(a,b,c,d,e,f)+1;}
int B(int a,int b,int c,int d,int e,int f){
    if(pause_b.load()){entered_b=true;while(!continue_b.load())std::this_thread::yield();}
    else order+='B';return next_b(a,b,c,d,e,f)+10;
}
int C(int a,int b,int c,int d,int e,int f){order+='C';return next_c(a,b,c,d,e,f)+100;}
using FpFn=double(*)(double,int,float,double,int,double,float,int);
FpFn next_fp=nullptr;
NOINLINE double FpOriginal(double a,int b,float c,double d,int e,double f,float g,int h){return a+2*b+3*c+4*d+5*e+6*f+7*g+8*h;}
double FpDetour(double a,int b,float c,double d,int e,double f,float g,int h){return next_fp(a,b,c,d,e,f,g,h)+.25;}
struct Triple {double a,b,c;};
using TripleFn=Triple(*)(int,double,int);
TripleFn next_triple=nullptr;
NOINLINE Triple TripleOriginal(int a,double b,int c){return {double(a),b,double(c)};}
Triple TripleDetour(int a,double b,int c){auto v=next_triple(a,b,c);v.b+=.5;return v;}
struct FakeBackend {
    std::unordered_map<void*,void*> entries;
    int prepared=0,enabled=0,aborted=0,retired=0;
    bool fail_prepare=false,fail_enable=false,throw_prepare=false,throw_enable=false;
    Backend Api(){return {
        [&](void* target,void* entry,void** original){++prepared;if(throw_prepare)throw std::bad_alloc();if(fail_prepare)return false;entries[target]=entry;*original=target;return true;},
        [&](void*){++enabled;if(throw_enable)throw std::bad_alloc();return !fail_enable;},
        [&](void* target){++aborted;entries.erase(target);},
        [&](void*){++retired;}};}
    IntFn Entry(){return reinterpret_cast<IntFn>(entries.at(reinterpret_cast<void*>(&Original)));}
};
void CoreTests() {
    FakeBackend backend;Chain chain(backend.Api());uint64_t ha=0,hb=0,hc=0;
    Check(chain.Create("a",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),reinterpret_cast<void**>(&next_a),&ha)==BE_Result_Ok,"first registration");
    Check(chain.Create("b",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&B),reinterpret_cast<void**>(&next_b),&hb)==BE_Result_Ok,"second registration");
    order.clear();Check(backend.Entry()(1,2,3,4,5,6)==Original(1,2,3,4,5,6)+11&&order=="AB","register/stack arguments/order");
    auto stable=next_a;uint64_t repeated=0;void* repeat_next=nullptr;
    Check(chain.Create("a",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),&repeat_next,&repeated)==BE_Result_Ok&&repeated==ha&&repeat_next==reinterpret_cast<void*>(stable),"idempotent registration");
    Check(backend.prepared==1&&backend.enabled==1,"target patched more than once");
    Check(chain.Disable(hb)==BE_Result_Ok,"disable middle");order.clear();
    Check(backend.Entry()(1,2,3,4,5,6)==Original(1,2,3,4,5,6)+1&&order=="A","disabled node still dispatched");
    Check(next_a==stable,"next identity changed");
    const uint64_t old_b=hb;
    Check(chain.Create("b",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&B),reinterpret_cast<void**>(&next_b),&hb)==BE_Result_Ok&&hb!=old_b,"reactivation reused old handle");
    order.clear();Check(backend.Entry()(1,2,3,4,5,6)==Original(1,2,3,4,5,6)+11&&order=="AB","reactivation changed startup order");
    Check(chain.Disable(old_b)==BE_Result_NotFound,"old handle disabled new registration");
    Check(chain.Disable(hb)==BE_Result_Ok,"disable reactivated node");
    Check(chain.Create("c",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&C),reinterpret_cast<void**>(&next_c),&hc)==BE_Result_Ok,"append after disabled tail");order.clear();
    Check(backend.Entry()(1,2,3,4,5,6)==Original(1,2,3,4,5,6)+101&&order=="AC","append after disabled node");
    Check(chain.DisableModule("a")==BE_Result_Ok,"module disable");order.clear();
    Check(backend.Entry()(1,2,3,4,5,6)==Original(1,2,3,4,5,6)+100&&order=="C","unrelated node removed");
    uint64_t hf=0,ht=0;
    Check(chain.Create("fp",reinterpret_cast<void*>(&FpOriginal),reinterpret_cast<void*>(&FpDetour),reinterpret_cast<void**>(&next_fp),&hf)==BE_Result_Ok,"mixed FP registration");
    auto fp=reinterpret_cast<FpFn>(backend.entries.at(reinterpret_cast<void*>(&FpOriginal)));
    Check(std::abs(fp(.125,2,.25f,3.5,4,5.75,.5f,6)-(FpOriginal(.125,2,.25f,3.5,4,5.75,.5f,6)+.25))<1e-8,"FP or stack registers changed");
    Check(chain.Create("triple",reinterpret_cast<void*>(&TripleOriginal),reinterpret_cast<void*>(&TripleDetour),reinterpret_cast<void**>(&next_triple),&ht)==BE_Result_Ok,"aggregate registration");
    auto triple=reinterpret_cast<TripleFn>(backend.entries.at(reinterpret_cast<void*>(&TripleOriginal)))(7,8.25,9);
    Check(triple.a==7&&triple.b==8.75&&triple.c==9,"aggregate/sret/HFA ABI changed");
    Check(chain.Disable(0)==BE_Result_NotFound,"stale handle accepted");
    Check(chain.Create("",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),&repeat_next,&repeated)==BE_Result_InvalidArgument,"empty owner accepted");
    chain.Shutdown();Check(backend.retired==3,"shutdown retirement");
    Check(backend.Entry()(1,2,3,4,5,6)==Original(1,2,3,4,5,6),"shutdown lost original");
    Check(next_a(1,2,3,4,5,6)==Original(1,2,3,4,5,6),"retained next invalid after shutdown");
    Check(chain.Create("a",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),&repeat_next,&repeated)==BE_Result_NotReady,"register after shutdown");
    FakeBackend failures;Chain bad(failures.Api());failures.fail_prepare=true;
    Check(bad.Create("a",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),&repeat_next,&repeated)==BE_Result_Failed&&!bad.Contains(reinterpret_cast<void*>(&Original))&&failures.aborted==0,"prepare failure destroyed a foreign hook");
    failures.fail_prepare=false;failures.fail_enable=true;
    Check(bad.Create("a",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),&repeat_next,&repeated)==BE_Result_Failed&&!bad.HasTargets()&&!repeat_next&&!repeated&&failures.aborted==1,"enable failure rollback");
    failures.fail_enable=false;failures.throw_prepare=true;
    Check(bad.Create("a",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),&repeat_next,&repeated)==BE_Result_Failed&&!bad.HasTargets()&&!repeat_next&&!repeated&&failures.aborted==1,"prepare exception left an empty target");
    failures.throw_prepare=false;failures.throw_enable=true;
    Check(bad.Create("a",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),&repeat_next,&repeated)==BE_Result_Failed&&!bad.HasTargets()&&!repeat_next&&!repeated&&failures.aborted==2,"enable exception did not roll back owned patch");
    failures.throw_enable=false;Check(bad.Create("a",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),reinterpret_cast<void**>(&next_a),&ha)==BE_Result_Ok,"rollback blocks retry");bad.Shutdown();
    // B already executing still has a valid next after it is disabled. Future
    // calls bypass B. No relay or module code is freed underneath the old call.
    FakeBackend concurrent;Chain live(concurrent.Api());
    Check(live.Create("a",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),reinterpret_cast<void**>(&next_a),&ha)==BE_Result_Ok,"live A");
    Check(live.Create("b",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&B),reinterpret_cast<void**>(&next_b),&hb)==BE_Result_Ok,"live B");
    pause_b=true;entered_b=false;continue_b=false;int result=0;
    std::thread pending([&]{result=concurrent.Entry()(1,2,3,4,5,6);});
    while(!entered_b.load())std::this_thread::yield();
    Check(live.Disable(hb)==BE_Result_Ok,"disable in flight");
    Check(concurrent.Entry()(1,2,3,4,5,6)==Original(1,2,3,4,5,6)+1,"new call entered disabled B");
    continue_b=true;pending.join();Check(result==Original(1,2,3,4,5,6)+11,"in-flight next corrupted");pause_b=false;live.Shutdown();
}
#if defined(_WIN32)
void ProductionTests() {
    BetterEndfield::Host::Logger logger;BetterEndfield::Host::HookBroker broker(logger);
    Check(broker.Initialize(),"MinHook init");auto* api=broker.ChainApi();
    uint64_t ha=0,hb=0;void* ignored=nullptr;
    Check(api->create(api->context,"a",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),reinterpret_cast<void**>(&next_a),&ha)==BE_Result_Ok,"real A");
    Check(api->create(api->context,"b",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&B),reinterpret_cast<void**>(&next_b),&hb)==BE_Result_Ok,"real B");
    volatile IntFn call=&Original;order.clear();
    Check(call(1,2,3,4,5,6)==102&&order=="AB","real chain invocation");
    Check(broker.Create("exclusive",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&C),&ignored)==BE_Result_Conflict,"exclusive overwrote chain");
    Check(api->disable(api->context,hb)==BE_Result_Ok&&call(1,2,3,4,5,6)==92,"real B disable");
    Check(broker.ReleaseModule("a")==BE_Result_Ok&&call(1,2,3,4,5,6)==91,"release chain module");
    Check(broker.Create("exclusive",reinterpret_cast<void*>(&FpOriginal),reinterpret_cast<void*>(&FpDetour),reinterpret_cast<void**>(&next_fp))==BE_Result_Ok,"legacy exclusive creation");
    Check(api->create(api->context,"blocked",reinterpret_cast<void*>(&FpOriginal),reinterpret_cast<void*>(&FpDetour),&ignored,&ha)==BE_Result_Conflict,"chain overwrote exclusive");
    Check(broker.ReleaseModule("exclusive")==BE_Result_Ok,"legacy release");
    broker.Shutdown();Check(call(1,2,3,4,5,6)==91,"real shutdown original");
}
#endif
}
int main(){try{CoreTests();
#if defined(_WIN32)
ProductionTests();
#endif
std::cout<<"PASS shared Hook chain: "<<checks<<" ABI, ownership, ordering, failure and in-flight checks\n";
return 0;}catch(const std::exception& e){std::cerr<<e.what()<<'\n';return 1;}}
