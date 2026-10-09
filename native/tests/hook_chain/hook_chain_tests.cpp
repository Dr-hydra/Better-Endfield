#include "../../shared/hooks/hook_chain.h"
#include <cmath>
#include <iostream>
#include <thread>
#include <stdexcept>
#if defined(_WIN32)
#include "../../shared/host/hook_broker.h"
#include "../../shared/host/hook_diagnostics.h"
#include "../../shared/host/logging.h"
#include <filesystem>
#include <fstream>
#include <sstream>
#endif
using namespace BetterEndfieldNext::Hooks;
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
FpFn next_fp2=nullptr;
double FpDetour2(double a,int b,float c,double d,int e,double f,float g,int h){return next_fp2(a,b,c,d,e,f,g,h)+.5;}
// Two built-in modules on one Unity entry (model and custom_model on
// Internal_CloneSingleWithParent). A distinct body keeps ICF from folding it.
NOINLINE int CloneOriginal(int a,int b,int c,int d,int e,int f){return a*b+c*d+e*f;}
IntFn next_m=nullptr,next_n=nullptr;
int M(int a,int b,int c,int d,int e,int f){order+='M';return next_m(a,b,c,d,e,f)+1000;}
int N(int a,int b,int c,int d,int e,int f){order+='N';return next_n(a,b,c,d,e,f)+10000;}
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
    // Built-in registrations (Duplicate::Reject) share the chain with
    // third-party ones (Duplicate::Reuse) and keep one node per module.
    using Dup=Chain::Duplicate;FakeBackend mixed;Chain shared(mixed.Api());uint64_t hm=0,hn=0,ht2=0;void* other=nullptr;
    Check(shared.Create("m",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),reinterpret_cast<void**>(&next_a),&hm,Dup::Reject)==BE_Result_Ok,"built-in m");
    Check(shared.Create("n",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&B),reinterpret_cast<void**>(&next_b),&hn,Dup::Reject)==BE_Result_Ok,"built-in n on the same target");
    Check(mixed.prepared==1&&mixed.enabled==1,"second built-in repatched the target");
    order.clear();Check(mixed.Entry()(1,2,3,4,5,6)==Original(1,2,3,4,5,6)+11&&order=="AB","built-in registration order");
    const auto kept_a=next_a;const uint64_t kept_m=hm;
    Check(shared.Create("m",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),reinterpret_cast<void**>(&next_a),&hm,Dup::Reject)==BE_Result_Conflict&&next_a==kept_a&&hm==kept_m,"rejected duplicate clobbered the live original");
    Check(shared.Create("m",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&C),&other,&ht2,Dup::Reject)==BE_Result_Conflict,"second detour of one module accepted");
    Check(shared.Create("tp",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&C),reinterpret_cast<void**>(&next_c),&ht2)==BE_Result_Ok,"third-party joins built-in chain");
    order.clear();Check(mixed.Entry()(1,2,3,4,5,6)==Original(1,2,3,4,5,6)+111&&order=="ABC","mixed order");
    Check((shared.Modules(reinterpret_cast<void*>(&Original))==std::vector<std::string>{"m","n","tp"}),"owner list");
    Check(shared.TargetOf(hn)==reinterpret_cast<void*>(&Original)&&shared.TargetOf(0)==nullptr,"handle target");
    Check(shared.DisableModule("m")==BE_Result_Ok,"release built-in m");order.clear();
    Check(mixed.Entry()(1,2,3,4,5,6)==Original(1,2,3,4,5,6)+110&&order=="BC","release removed another module");
    Check((shared.Modules(reinterpret_cast<void*>(&Original))==std::vector<std::string>{"n","tp"}),"owner list after release");
    order.clear();Check(kept_a(1,2,3,4,5,6)==Original(1,2,3,4,5,6)+110&&order=="BC","released module's original lost the rest of the chain");
    Check(shared.Create("m",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),reinterpret_cast<void**>(&next_a),&hm,Dup::Reject)==BE_Result_Ok&&next_a==kept_a&&hm!=kept_m,"built-in re-registration");
    order.clear();Check(mixed.Entry()(1,2,3,4,5,6)==Original(1,2,3,4,5,6)+111&&order=="ABC","re-registration lost its position");
    Check(shared.Disable(hn)==BE_Result_Ok&&shared.DisableModule("tp")==BE_Result_Ok&&shared.DisableModule("m")==BE_Result_Ok,"release all");
    Check(mixed.Entry()(1,2,3,4,5,6)==Original(1,2,3,4,5,6)&&shared.Modules(reinterpret_cast<void*>(&Original)).empty(),"all pass-through");
    shared.Shutdown();
}
#if defined(_WIN32)
void ProductionTests() {
    const auto log_root=std::filesystem::temp_directory_path()/"be_hook_chain_tests";
    std::filesystem::remove_all(log_root);
    BetterEndfieldNext::Host::Logger logger;logger.Initialize(log_root);BetterEndfieldNext::Host::HookBroker broker(logger);
    Check(broker.Initialize(),"MinHook init");auto* api=broker.ChainApi();
    uint64_t ha=0,hb=0;void* ignored=nullptr;
    Check(api->create(api->context,"a",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),reinterpret_cast<void**>(&next_a),&ha)==BE_Result_Ok,"real A");
    Check(api->create(api->context,"b",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&B),reinterpret_cast<void**>(&next_b),&hb)==BE_Result_Ok,"real B");
    volatile IntFn call=&Original;order.clear();
    Check(broker.Called(reinterpret_cast<void*>(&Original))==false,"chain target reported a call before one happened");
    Check(call(1,2,3,4,5,6)==102&&order=="AB","real chain invocation");
    Check(broker.Called(reinterpret_cast<void*>(&Original))==true,"tracked chain relay missed a call");
    // Built-in create_hook joins the third-party chain instead of conflicting.
    Check(broker.Create("builtin",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&C),reinterpret_cast<void**>(&next_c))==BE_Result_Ok,"built-in rejected by chain");
    order.clear();Check(call(1,2,3,4,5,6)==202&&order=="ABC","built-in appended to chain");
    const auto kept_c=next_c;
    Check(broker.Create("builtin",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&C),reinterpret_cast<void**>(&next_c))==BE_Result_Conflict&&next_c==kept_c,"duplicate built-in registration");
    Check(broker.Create("builtin",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),&ignored)==BE_Result_Conflict,"second detour of one built-in module");
    uint64_t again=0;
    Check(api->create(api->context,"a",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&A),&ignored,&again)==BE_Result_Ok&&again==ha&&ignored==reinterpret_cast<void*>(next_a),"third-party idempotence changed");
    Check(api->disable(api->context,hb)==BE_Result_Ok&&call(1,2,3,4,5,6)==192,"real B disable");
    Check(broker.ReleaseModule("a")==BE_Result_Ok&&call(1,2,3,4,5,6)==191,"release chain module");
    Check(broker.ReleaseModule("builtin")==BE_Result_Ok&&call(1,2,3,4,5,6)==91,"release built-in module");
    Check(broker.Create("builtin",reinterpret_cast<void*>(&Original),reinterpret_cast<void*>(&C),reinterpret_cast<void**>(&next_c))==BE_Result_Ok&&next_c==kept_c&&call(1,2,3,4,5,6)==191,"built-in re-registration");
    Check(broker.ReleaseModule("builtin")==BE_Result_Ok&&call(1,2,3,4,5,6)==91,"release re-registered built-in");
    // Two built-in modules hook one target: both run, release keeps the other.
    broker.DescribeEntry(reinterpret_cast<void*>(&CloneOriginal),"UnityEngine.Object::Internal_CloneSingleWithParent(...)");
    Check(broker.Create("betterendfieldnext.model",reinterpret_cast<void*>(&CloneOriginal),reinterpret_cast<void*>(&M),reinterpret_cast<void**>(&next_m))==BE_Result_Ok,"model clone hook");
    Check(broker.Create("betterendfieldnext.custom_model",reinterpret_cast<void*>(&CloneOriginal),reinterpret_cast<void*>(&N),reinterpret_cast<void**>(&next_n))==BE_Result_Ok,"custom_model clone hook refused");
    volatile IntFn clone_call=&CloneOriginal;order.clear();
    Check(clone_call(1,2,3,4,5,6)==11044&&order=="MN","both built-in hooks must run in registration order");
    Check(broker.ReleaseModule("betterendfieldnext.model")==BE_Result_Ok,"release model");order.clear();
    Check(clone_call(1,2,3,4,5,6)==10044&&order=="N","custom_model lost its hook when model was released");
    order.clear();Check(next_m(1,2,3,4,5,6)==10044&&order=="N","released original skipped the remaining chain");
    Check(broker.Create("betterendfieldnext.model",reinterpret_cast<void*>(&CloneOriginal),reinterpret_cast<void*>(&M),reinterpret_cast<void**>(&next_m))==BE_Result_Ok,"model re-registration");
    order.clear();Check(clone_call(1,2,3,4,5,6)==11044&&order=="MN","re-registered model moved");
    Check(broker.ReleaseModule("betterendfieldnext.custom_model")==BE_Result_Ok,"release custom_model");order.clear();
    Check(clone_call(1,2,3,4,5,6)==1044&&order=="M","model lost its hook when custom_model was released");
    // Computed before FpOriginal is patched; later direct calls enter the chain.
    const double fp_base=FpOriginal(.125,2,.25f,3.5,4,5.75,.5f,6);
    broker.DescribeEntry(reinterpret_cast<void*>(&FpOriginal),"Test.Fp::Original(...)");
    Check(broker.Create("exclusive",reinterpret_cast<void*>(&FpOriginal),reinterpret_cast<void*>(&FpDetour),reinterpret_cast<void**>(&next_fp))==BE_Result_Ok,"legacy exclusive creation");
    Check(broker.Called(reinterpret_cast<void*>(&FpOriginal))==false,"exclusive target reported a call before one happened");
    volatile FpFn fp_call=&FpOriginal;
    Check(std::abs(fp_call(.125,2,.25f,3.5,4,5.75,.5f,6)-(next_fp(.125,2,.25f,3.5,4,5.75,.5f,6)+.25))<1e-8,"tracked relay changed FP or stack arguments");
    Check(broker.Called(reinterpret_cast<void*>(&FpOriginal))==true,"tracked exclusive relay missed a call");
    broker.DescribeEntry(reinterpret_cast<void*>(&FpOriginal),"Test.Folded::Twin(...)");
    broker.Poll();
    Check(api->create(api->context,"joined",reinterpret_cast<void*>(&FpOriginal),reinterpret_cast<void*>(&FpDetour2),reinterpret_cast<void**>(&next_fp2),&ha)==BE_Result_Ok,"third-party rejected by built-in hook");
    Check(std::abs(fp_call(.125,2,.25f,3.5,4,5.75,.5f,6)-(fp_base+.75))<1e-8,"third-party node after built-in");
    Check(broker.ReleaseModule("exclusive")==BE_Result_Ok,"built-in release");
    Check(std::abs(fp_call(.125,2,.25f,3.5,4,5.75,.5f,6)-(fp_base+.5))<1e-8,"third-party node lost by built-in release");
    Check(api->disable_module(api->context,"joined")==BE_Result_Ok,"third-party release");
    Check(std::abs(fp_call(.125,2,.25f,3.5,4,5.75,.5f,6)-fp_base)<1e-8,"released target is not a pass-through");
    broker.Shutdown();Check(call(1,2,3,4,5,6)==91,"real shutdown original");
    std::ifstream log_file(log_root/"BetterEndfieldNext.log");std::stringstream log;log<<log_file.rdbuf();
    const std::string text=log.str();
    Check(text.find("Hook installed for exclusive: Test.Fp::Original(...) at ")!=std::string::npos,"install line missing");
    Check(text.find("First call observed: ")!=std::string::npos&&text.find("Test.Fp::Original(...) / Test.Folded::Twin(...) (")!=std::string::npos,"first call line missing");
    Check(text.find("Shared native entry at ")!=std::string::npos,"shared entry warning missing");
    Check(text.find("Hook installed for betterendfieldnext.model: UnityEngine.Object::Internal_CloneSingleWithParent(...) at ")!=std::string::npos,"clone install line missing");
    Check(text.find("now runs betterendfieldnext.model -> betterendfieldnext.custom_model -> original.")!=std::string::npos,"chain owner line missing");
    Check(text.find("after release of betterendfieldnext.model: betterendfieldnext.custom_model -> original.")!=std::string::npos,"release owner line missing");
    Check(text.find("Hook conflict: builtin already has an active hook at ")!=std::string::npos,"duplicate conflict line missing");
}
void DiagnosticsTests() {
    namespace D=BetterEndfieldNext::Host::HookDiagnostics;
    // 0: xor edx,edx; jmp +57 -> 64 (a tail call into a leaf). 64: ret.
    // 128: xor eax,eax; je +4; ret (leaf without a tail call).
    auto* code=static_cast<uint8_t*>(VirtualAlloc(nullptr,4096,MEM_COMMIT|MEM_RESERVE,PAGE_EXECUTE_READWRITE));
    Check(code!=nullptr,"code page");std::memset(code,0xCC,4096);
    const uint8_t thunk[]{0x31,0xD2,0xE9,57,0,0,0};std::memcpy(code,thunk,sizeof(thunk));code[64]=0xC3;
    const uint8_t plain[]{0x31,0xC0,0x74,0x04,0xC3};std::memcpy(code+128,plain,sizeof(plain));
    auto shape=D::Describe(code);
    Check(shape.leaf&&shape.tail_target==code+64,"tail jump into another function not found");
    Check(D::Describe(code+128).tail_target==nullptr,"leaf without a tail call reported one");
    // Two E8 and one E9 to code+64, one E8 elsewhere.
    uint8_t calls[32]{};const uintptr_t base=0x10000,target=0x10040;
    auto put=[&](size_t at,uint8_t op,uintptr_t to){calls[at]=op;const int32_t d=static_cast<int32_t>(to-(base+at+5));std::memcpy(calls+at+1,&d,4);};
    put(0,0xE8,target);put(5,0xE9,target);put(10,0xE8,0x20000);put(20,0xE8,target);
    auto counts=D::CountRel32References(calls,sizeof(calls),base,{target,0x30000});
    Check(counts[target]==3&&counts[0x30000]==0,"rel32 reference count");
    Check(D::FormatAddress(reinterpret_cast<void*>(&Original)).find(".exe+0x")!=std::string::npos,"module-relative address");
    VirtualFree(code,0,MEM_RELEASE);
}
#endif
}
int main(){try{CoreTests();
#if defined(_WIN32)
ProductionTests();DiagnosticsTests();
#endif
std::cout<<"PASS shared Hook chain: "<<checks<<" ABI, ownership, ordering, failure and in-flight checks\n";
return 0;}catch(const std::exception& e){std::cerr<<e.what()<<'\n';return 1;}}
