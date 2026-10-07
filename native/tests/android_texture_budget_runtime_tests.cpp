#include <array>
#include <atomic>
#include <cmath>
#include <cstdint>
#include <iostream>
#include <limits>
#include <mutex>
#include <stdexcept>
#include <string>
#include <string_view>
#include <vector>

#include "../modules/custom_model/android_texture_budget_policy.h"

namespace {
uint32_t checks=0;
void Check(bool condition,const char* expression,int line) {
    ++checks;
    if(!condition) throw std::runtime_error("runtime check at line "+std::to_string(line)+": "+expression);
}
#define CHECK(expression) Check((expression),#expression,__LINE__)
constexpr uint64_t MiB=1024ull*1024;
constexpr uint32_t PumpThread=7;

struct Fixture {
    uint64_t now=1000;
    uint32_t thread=PumpThread;
    bool active=true,memory_available=true,getters_available=true;
    bool setter_ok=true,apply_failed_setter=false,omit_readback_after_write=false;
    bool invalid_native_budget=false,unknown_native_quality=false,ignore_next_native_write=false;
    bool quality_budget_callback=false,quality_callback_uses_stored=true;
    float quality_callback_budget=600;
    int omitted_budget_reads=0,hook_failure_at=0;
    int32_t tier=0;
    std::array<float,3> budgets{{600,512,1152}};
    uint64_t current=629134994,desired=1088751570,target=629134994;
    uint64_t physical=8192*MiB,available=3072*MiB;
    uint32_t invokes=0,getter_reads=0,quality_reads=0,memory_reads=0;
    uint32_t write_attempts=0,budget_calls=0,quality_calls=0,hook_calls=0;
    std::vector<std::string> calls,logs;
    std::vector<float> budget_requests,quality_old_budgets,managed_set_attempts;
    void(*budget_entry)(float)=nullptr;
    void(*quality_entry)(int32_t,bool)=nullptr;
};
Fixture fixture;
float NativeGetBudget() {
    ++fixture.getter_reads;
    return fixture.invalid_native_budget?(std::numeric_limits<float>::quiet_NaN)():fixture.budgets.at(static_cast<size_t>(fixture.tier));
}
int32_t NativeGetQuality() {++fixture.quality_reads;return fixture.unknown_native_quality?-1:fixture.tier;}
void NativeSetBudget(float value) {
    ++fixture.budget_calls;
    fixture.calls.push_back("budget:"+std::to_string(value));
    fixture.budget_requests.push_back(value);
    if(fixture.ignore_next_native_write) {fixture.ignore_next_native_write=false;return;}
    fixture.budgets.at(static_cast<size_t>(fixture.tier))=value;
}
void NativeSetQuality(int32_t level,bool expensive) {
    CHECK(level>=0 && static_cast<size_t>(level)<fixture.budgets.size());
    ++fixture.quality_calls;
    fixture.calls.push_back("quality:"+std::to_string(level)+(expensive?":expensive":":ordinary"));
    fixture.quality_old_budgets.push_back(fixture.budgets.at(static_cast<size_t>(fixture.tier)));
    fixture.tier=level;
    // Unity's Full Apply uses the stored tier budget directly. It does not
    // pass through the named budget icall and must not produce its observer.
    // An optional managed callback also tests games/profiles that echo a
    // stored budget through the public property while SetQualityLevel runs.
    if(fixture.quality_budget_callback) {
        CHECK(fixture.budget_entry);
        fixture.budget_entry(fixture.quality_callback_uses_stored?
            fixture.budgets.at(static_cast<size_t>(fixture.tier)):fixture.quality_callback_budget);
    }
}
}

namespace betterendfield {
void* AndroidTextureBudgetSetterEntry() {return reinterpret_cast<void*>(&NativeSetBudget);}
void* AndroidTextureBudgetGetterEntry() {return reinterpret_cast<void*>(&NativeGetBudget);}
void* AndroidQualityLevelSetterEntry() {return reinterpret_cast<void*>(&NativeSetQuality);}
void* AndroidQualityLevelGetterEntry() {return reinterpret_cast<void*>(&NativeGetQuality);}
bool AndroidReadMemoryHeadroom(uint64_t& physical,uint64_t& available) {
    ++fixture.memory_reads;
    if(!fixture.memory_available) return false;
    physical=fixture.physical;available=fixture.available;return true;
}
}

namespace BetterEndfield::CustomModel {
enum BE_Result {BE_Result_Ok=0,BE_Result_Failed=1};
struct MethodContract {const char* key;bool resolved=true;};
struct TestHost {
    void* context=nullptr;
    BE_Result(*create_hook)(void*,const char*,void*,void*,void**)=nullptr;
};
constexpr const char* kModuleId="custom_model.fixture";
std::atomic<uint32_t> g_pump_thread{PumpThread};
uint64_t GetTickCount64() {return fixture.now;}
uint32_t GetCurrentThreadId() {return fixture.thread;}
bool AndroidTextureDiagnosticsOnPump() {
    const auto pump=g_pump_thread.load(std::memory_order_acquire);
    return pump!=0 && pump==GetCurrentThreadId();
}
void Log(const std::string& line) {fixture.logs.push_back(line);}
MethodContract* Contract(const char* key) {
    static std::array<MethodContract,6> contracts{{
        {"android.streaming_budget_set"},{"android.streaming_budget"},
        {"android.streaming_active"},{"android.texture_current"},
        {"android.texture_desired"},{"android.texture_target"}
    }};
    for(auto& contract:contracts) if(std::string_view(key)==contract.key) return &contract;
    throw std::runtime_error("unexpected runtime contract: "+std::string(key));
}
template<class T> bool InvokeValue(MethodContract* contract,void* object,void** args,T& output) {
    CHECK(contract && !object && !args);
    CHECK(AndroidTextureDiagnosticsOnPump());
    ++fixture.invokes;
    if(!fixture.getters_available) return false;
    const std::string_view key(contract->key);
    if(key=="android.streaming_budget") {
        if(fixture.omitted_budget_reads>0) {--fixture.omitted_budget_reads;return false;}
        output=static_cast<T>(NativeGetBudget());
    } else if(key=="android.streaming_active") output=static_cast<T>(fixture.active);
    else if(key=="android.texture_current") output=static_cast<T>(fixture.current);
    else if(key=="android.texture_desired") output=static_cast<T>(fixture.desired);
    else if(key=="android.texture_target") output=static_cast<T>(fixture.target);
    else throw std::runtime_error("unexpected getter contract");
    return true;
}
bool InvokeVoid(MethodContract* contract,void* object,void** args) {
    CHECK(contract && std::string_view(contract->key)=="android.streaming_budget_set" && !object && args && args[0]);
    CHECK(AndroidTextureDiagnosticsOnPump());
    ++fixture.write_attempts;
    fixture.managed_set_attempts.push_back(*static_cast<float*>(args[0]));
    if(fixture.setter_ok || fixture.apply_failed_setter) {
        CHECK(fixture.budget_entry);
        fixture.budget_entry(*static_cast<float*>(args[0]));
    }
    if(fixture.omit_readback_after_write) {
        fixture.omitted_budget_reads=1;fixture.omit_readback_after_write=false;
    }
    return fixture.setter_ok;
}
BE_Result CreateHook(void* context,const char* module,void* target,void* detour,void** original) {
    CHECK(!context && std::string_view(module)==kModuleId && detour && original);
    ++fixture.hook_calls;
    if(fixture.hook_failure_at==static_cast<int>(fixture.hook_calls)) return BE_Result_Failed;
    if(target==reinterpret_cast<void*>(&NativeSetBudget)) {
        fixture.budget_entry=reinterpret_cast<void(*)(float)>(detour);
        *original=reinterpret_cast<void*>(&NativeSetBudget);
    } else if(target==reinterpret_cast<void*>(&NativeSetQuality)) {
        fixture.quality_entry=reinterpret_cast<void(*)(int32_t,bool)>(detour);
        *original=reinterpret_cast<void*>(&NativeSetQuality);
    } else throw std::runtime_error("hook target is not a named native entry");
    return BE_Result_Ok;
}
TestHost fixture_host{nullptr,&CreateHook};
const TestHost* g_host=&fixture_host;

// Compile and exercise the real production adapter and policy, not a copy.
#include "../modules/custom_model/android_texture_budget_runtime.inc"

void ResetFixture() {
    fixture=Fixture{};
    g_pump_thread.store(PumpThread,std::memory_order_release);
    g_original_android_budget_setter=nullptr;
    g_original_android_quality_setter=nullptr;
    g_android_budget_getter=nullptr;g_android_quality_getter=nullptr;
    g_android_internal_budget_write=false;g_android_quality_hook_active=false;
    InitializeAndroidTextureBudget();
    CHECK(g_android_budget_hooks_ready.load(std::memory_order_acquire));
    CHECK(fixture.hook_calls==2 && fixture.budget_entry && fixture.quality_entry);
    CHECK(fixture.getter_reads==0 && fixture.quality_reads==0 && fixture.budget_calls==0);
}
void Advance(uint64_t elapsed=AndroidTextureBudgetPolicy::EvaluationMs) {fixture.now+=elapsed;}
void SettledCounters() {
    fixture.current=static_cast<uint64_t>(static_cast<double>(fixture.budgets.at(static_cast<size_t>(fixture.tier)))*MiB);
    fixture.target=fixture.current;
}
void Grow() {
    const float base=fixture.budgets.at(static_cast<size_t>(fixture.tier));
    CHECK(TickAndroidTextureBudget(true));
    CHECK(fixture.budget_calls==0 && !g_android_budget_policy.HasOwnedOverride());
    Advance();CHECK(TickAndroidTextureBudget(true));
    CHECK(fixture.budgets.at(static_cast<size_t>(fixture.tier))==1152);
    CHECK(g_android_budget_policy.HasConfirmedOverride() && AndroidBudgetHasLease());
    CHECK(g_android_budget_policy.GameBaseBytes()==static_cast<uint64_t>(base*MiB));
    CHECK(AndroidBudgetIoSnapshot().epoch==0);
    SettledCounters();
}
void NativeGameBudget(float value) {CHECK(fixture.budget_entry);fixture.budget_entry(value);}
void NativeGameQuality(int32_t tier,bool expensive=true) {CHECK(fixture.quality_entry);fixture.quality_entry(tier,expensive);}

void PumpRestrictionAndShutdown() {
    ResetFixture();
    fixture.thread=PumpThread+1;
    CHECK(!TickAndroidTextureBudget(true));
    CHECK(!TickAndroidTextureBudget(false,true));
    CHECK(!fixture.invokes && !fixture.getter_reads && !fixture.quality_reads && !fixture.write_attempts && !fixture.memory_reads);
    fixture.thread=PumpThread;g_pump_thread.store(0);
    CHECK(!TickAndroidTextureBudget(true));
    CHECK(!fixture.invokes && !fixture.budget_calls);
    g_pump_thread.store(PumpThread);Grow();
    const auto reads=fixture.invokes,gets=fixture.getter_reads,tiers=fixture.quality_reads,writes=fixture.budget_calls;
    fixture.thread=PumpThread+1;
    CHECK(!TickAndroidTextureBudget(false,true));
    CHECK(fixture.invokes==reads && fixture.getter_reads==gets && fixture.quality_reads==tiers && fixture.budget_calls==writes);
    CHECK(AndroidBudgetHasLease() && g_android_budget_policy.HasOwnedOverride());
    fixture.thread=PumpThread;
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(fixture.budgets[0]==600 && !g_android_budget_policy.HasOwnedOverride() && !AndroidBudgetHasLease());
}
void PressureAndOwnEpoch() {
    ResetFixture();
    CHECK(TickAndroidTextureBudget(true));
    CHECK(fixture.budget_calls==0);
    Advance(1999);CHECK(TickAndroidTextureBudget(true));
    CHECK(fixture.budget_calls==0);
    Advance(1);CHECK(TickAndroidTextureBudget(true));
    CHECK(fixture.budget_calls==1 && fixture.budgets[0]==1152);
    CHECK(g_android_budget_policy.HasConfirmedOverride());
    CHECK(g_android_budget_policy.GameBaseBytes()==600*MiB && AndroidBudgetIoSnapshot().epoch==0);
    const auto attempts=fixture.write_attempts;
    SettledCounters();
    for(int i=0;i<10;++i) {
        Advance();CHECK(TickAndroidTextureBudget(true));
        CHECK(fixture.budgets[0]==1152 && fixture.write_attempts==attempts);
        CHECK(g_android_budget_policy.GameBaseBytes()==600*MiB && AndroidBudgetIoSnapshot().epoch==0);
    }
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(fixture.budgets[0]==600 && !AndroidBudgetHasLease());
}
void EqualExternalRequest() {
    ResetFixture();Grow();
    NativeGameBudget(1152);
    CHECK(AndroidBudgetIoSnapshot().epoch==1 && !AndroidBudgetHasLease());
    const auto writes=fixture.budget_calls;
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(g_android_budget_policy.GameBaseBytes()==1152*MiB);
    CHECK(fixture.budgets[0]==1152 && fixture.budget_calls==writes && !g_android_budget_policy.HasOwnedOverride());
}
void DifferentTierAndReturn() {
    ResetFixture();Grow();
    NativeGameQuality(1);
    CHECK(fixture.tier==1 && fixture.budgets[0]==600 && fixture.budgets[1]==512);
    CHECK(fixture.quality_old_budgets.back()==600);
    CHECK(AndroidBudgetIoSnapshot().epoch==1 && !AndroidBudgetHasLease());
    // The new tier's pressure is confirmed independently after its event.
    fixture.current=512*MiB;fixture.target=512*MiB;
    Advance();CHECK(TickAndroidTextureBudget(true));
    CHECK(!g_android_budget_policy.HasOwnedOverride() && g_android_budget_policy.GameBaseBytes()==512*MiB);
    Advance();CHECK(TickAndroidTextureBudget(true));
    CHECK(g_android_budget_policy.HasConfirmedOverride() && fixture.budgets[1]==1152);
    NativeGameQuality(0,false);
    CHECK(fixture.tier==0 && fixture.budgets[1]==512 && fixture.budgets[0]==600);
    CHECK(fixture.quality_old_budgets.back()==512);
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(g_android_budget_policy.GameBaseBytes()==600*MiB && !AndroidBudgetHasLease());
}
void NewTierEqualOwn() {
    ResetFixture();Grow();
    NativeGameQuality(2);
    CHECK(fixture.tier==2 && fixture.budgets[0]==600 && fixture.budgets[2]==1152);
    CHECK(AndroidBudgetIoSnapshot().epoch==1);
    const auto writes=fixture.budget_calls;
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(g_android_budget_policy.GameBaseBytes()==1152*MiB);
    CHECK(fixture.budget_calls==writes && !g_android_budget_policy.HasOwnedOverride() && !AndroidBudgetHasLease());
}
void SameTierNoFlap() {
    ResetFixture();Grow();
    const auto writes=fixture.budget_calls,attempts=fixture.write_attempts;
    for(int i=0;i<8;++i) {
        NativeGameQuality(0,false);
        CHECK(AndroidBudgetIoSnapshot().epoch==0 && AndroidBudgetHasLease());
        Advance();CHECK(TickAndroidTextureBudget(true));
        CHECK(g_android_budget_policy.GameBaseBytes()==600*MiB && g_android_budget_policy.HasConfirmedOverride());
        CHECK(fixture.budgets[0]==1152 && fixture.budget_calls==writes && fixture.write_attempts==attempts);
    }
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(fixture.budgets[0]==600);
}
void RestoreReadbackLateConfirmation() {
    ResetFixture();Grow();
    fixture.omit_readback_after_write=true;
    CHECK(!TickAndroidTextureBudget(false,true));
    CHECK(fixture.budgets[0]==600 && g_android_budget_policy.HasUnconfirmedAttempt() && AndroidBudgetHasLease());
    const auto writes=fixture.budget_calls;
    Advance(1);
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(!g_android_budget_policy.HasOwnedOverride() && !AndroidBudgetHasLease());
    CHECK(fixture.budgets[0]==600 && fixture.budget_calls==writes);
}
void GrowReadbackLateConfirmation() {
    ResetFixture();CHECK(TickAndroidTextureBudget(true));
    Advance();fixture.omit_readback_after_write=true;
    CHECK(TickAndroidTextureBudget(true));
    CHECK(fixture.budgets[0]==1152 && g_android_budget_policy.HasUnconfirmedAttempt());
    CHECK(!g_android_budget_policy.HasConfirmedOverride() && AndroidBudgetHasLease());
    Advance();CHECK(TickAndroidTextureBudget(true));
    CHECK(g_android_budget_policy.HasConfirmedOverride() && !g_android_budget_policy.HasUnconfirmedAttempt());
    CHECK(g_android_budget_policy.GameBaseBytes()==600*MiB && fixture.budget_calls==1);
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(fixture.budgets[0]==600 && !AndroidBudgetHasLease());
}
void RejectedSetterNeverClaimed() {
    ResetFixture();CHECK(TickAndroidTextureBudget(true));
    fixture.setter_ok=false;Advance();CHECK(TickAndroidTextureBudget(true));
    CHECK(fixture.write_attempts==1 && fixture.budget_calls==0 && fixture.budgets[0]==600);
    CHECK(!g_android_budget_policy.HasOwnedOverride() && !AndroidBudgetHasLease());
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(fixture.budget_calls==0 && fixture.budgets[0]==600);
}
void MissingObservationsRestore() {
    ResetFixture();Grow();
    fixture.memory_available=false;Advance();CHECK(TickAndroidTextureBudget(true));
    CHECK(fixture.budgets[0]==600 && !g_android_budget_policy.HasOwnedOverride() && !AndroidBudgetHasLease());
    ResetFixture();fixture.getters_available=false;
    CHECK(TickAndroidTextureBudget(true));Advance();CHECK(TickAndroidTextureBudget(true));
    CHECK(!fixture.budget_calls && !g_android_budget_policy.HasGameBase());
}
void OutsidePumpEqualExternalRequest() {
    ResetFixture();Grow();
    const auto gets=fixture.getter_reads,tiers=fixture.quality_reads;
    fixture.thread=PumpThread+1;
    NativeGameBudget(1152);
    CHECK(fixture.getter_reads==gets && fixture.quality_reads==tiers);
    CHECK(AndroidBudgetIoSnapshot().epoch==1);
    CHECK(!TickAndroidTextureBudget(false,true));
    fixture.thread=PumpThread;
    const auto writes=fixture.budget_calls;
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(fixture.budgets[0]==1152 && fixture.budget_calls==writes);
    CHECK(!g_android_budget_policy.HasOwnedOverride() && !AndroidBudgetHasLease());
}
void OutsidePumpTierRetainsUnrestoredBoundary() {
    ResetFixture();Grow();
    const auto gets=fixture.getter_reads,tiers=fixture.quality_reads,writes=fixture.budget_calls;
    fixture.thread=PumpThread+1;
    NativeGameQuality(1);
    CHECK(fixture.tier==1 && fixture.budgets[0]==1152);
    CHECK(fixture.getter_reads==gets && fixture.quality_reads==tiers && fixture.budget_calls==writes);
    CHECK(!TickAndroidTextureBudget(false,true));
    fixture.thread=PumpThread;
    CHECK(!TickAndroidTextureBudget(false,true));
    CHECK(AndroidBudgetHasLease() && fixture.budgets[0]==1152 && fixture.budgets[1]==512);
    // Return to the original tier. It is now possible to restore that lease
    // without guessing an inactive tier's native layout or switching it.
    fixture.thread=PumpThread+1;NativeGameQuality(0);
    fixture.thread=PumpThread;
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(fixture.budgets[0]==600 && !AndroidBudgetHasLease());
}
void HookFailureDisablesOwnWrites() {
    ResetFixture();fixture.hook_calls=0;fixture.hook_failure_at=2;
    InitializeAndroidTextureBudget();
    CHECK(!g_android_budget_hooks_ready.load(std::memory_order_acquire));
    CHECK(TickAndroidTextureBudget(true));Advance();CHECK(TickAndroidTextureBudget(true));
    CHECK(fixture.budget_calls==0 && fixture.getter_reads==0 && fixture.memory_reads==0);
    CHECK(TickAndroidTextureBudget(false,true));
}
void InvalidNativeRestoreReadback() {
    ResetFixture();Grow();
    const auto writes=fixture.budget_calls;
    fixture.invalid_native_budget=true;
    CHECK(!AndroidRestoreTierLease());
    CHECK(AndroidBudgetHasLease() && fixture.budget_calls==writes);
    CHECK(!TickAndroidTextureBudget(false,true));
    CHECK(AndroidBudgetHasLease() && fixture.budgets[0]==1152);
    fixture.invalid_native_budget=false;fixture.unknown_native_quality=true;
    CHECK(!AndroidRestoreTierLease());
    CHECK(!TickAndroidTextureBudget(false,true));
    CHECK(AndroidBudgetHasLease() && fixture.budget_calls==writes);
    fixture.unknown_native_quality=false;
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(fixture.budgets[0]==600 && !AndroidBudgetHasLease());
}
void SameTierBudgetEchoPreservesLease() {
    ResetFixture();Grow();
    fixture.quality_budget_callback=true;
    const auto attempts=fixture.write_attempts,writes=fixture.budget_calls;
    for(uint32_t i=0;i<5;++i) {
        NativeGameQuality(0,false);
        CHECK(AndroidBudgetIoSnapshot().epoch==0 && AndroidBudgetHasLease());
        Advance();CHECK(TickAndroidTextureBudget(true));
        CHECK(g_android_budget_policy.GameBaseBytes()==600*MiB && g_android_budget_policy.HasConfirmedOverride());
        CHECK(fixture.budgets[0]==1152 && fixture.write_attempts==attempts);
        CHECK(fixture.budget_calls==writes+i+1);
    }
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(fixture.budgets[0]==600 && !AndroidBudgetHasLease());
}
void SameTierDifferentBudgetIsExternal() {
    ResetFixture();Grow();
    fixture.quality_budget_callback=true;fixture.quality_callback_uses_stored=false;
    fixture.quality_callback_budget=600;
    NativeGameQuality(0,false);
    CHECK(AndroidBudgetIoSnapshot().epoch==1 && !AndroidBudgetHasLease());
    CHECK(fixture.budgets[0]==600);
    const auto attempts=fixture.write_attempts,writes=fixture.budget_calls;
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(g_android_budget_policy.GameBaseBytes()==600*MiB && !g_android_budget_policy.HasOwnedOverride());
    CHECK(fixture.write_attempts==attempts && fixture.budget_calls==writes && fixture.budgets[0]==600);
}
void RejectedGrowthPreservesOwnership() {
    for(const uint64_t physical:{8192*MiB,12288*MiB}) {
        for(const bool invocation_ok:{false,true}) {
            ResetFixture();Grow();fixture.physical=physical;fixture.desired=1300*MiB;
            fixture.setter_ok=invocation_ok;fixture.ignore_next_native_write=invocation_ok;
            Advance();CHECK(TickAndroidTextureBudget(true));
            CHECK(fixture.managed_set_attempts.back()==(physical==8192*MiB?1344:1408));
            CHECK(fixture.budgets[0]==1152 && g_android_budget_policy.HasConfirmedOverride());
            CHECK(!g_android_budget_policy.HasUnconfirmedAttempt() && g_android_budget_policy.GameBaseBytes()==600*MiB);
            const auto lease=AndroidBudgetIoSnapshot().lease;
            CHECK(lease.valid && lease.base==600 && lease.own==1152 && lease.previous_own==0);
            NativeGameQuality(1);
            CHECK(fixture.budgets[0]==600 && fixture.quality_old_budgets.back()==600);
            CHECK(TickAndroidTextureBudget(false,true));
            CHECK(fixture.budgets[1]==512 && !AndroidBudgetHasLease());
        }
    }
}
void RejectedGrowthLateReadback() {
    ResetFixture();Grow();fixture.desired=1300*MiB;
    fixture.ignore_next_native_write=true;fixture.omit_readback_after_write=true;
    Advance();CHECK(TickAndroidTextureBudget(true));
    CHECK(fixture.managed_set_attempts.back()==1344 && fixture.budgets[0]==1152);
    CHECK(g_android_budget_policy.HasConfirmedOverride() && g_android_budget_policy.HasUnconfirmedAttempt());
    auto lease=AndroidBudgetIoSnapshot().lease;
    CHECK(lease.valid && lease.base==600 && lease.own==1344 && lease.previous_own==1152);
    // The late observation confirms that the old override remained installed.
    // Keep demand steady so this check cannot be masked by another proposal.
    fixture.desired=1038*MiB;
    const auto attempts=fixture.write_attempts;
    Advance();CHECK(TickAndroidTextureBudget(true));
    CHECK(g_android_budget_policy.HasConfirmedOverride() && !g_android_budget_policy.HasUnconfirmedAttempt());
    CHECK(g_android_budget_policy.OwnedBudgetBytes()==1152*MiB && fixture.write_attempts==attempts);
    lease=AndroidBudgetIoSnapshot().lease;
    CHECK(lease.valid && lease.base==600 && lease.own==1152 && lease.previous_own==0);
    NativeGameQuality(1);
    CHECK(fixture.budgets[0]==600 && fixture.quality_old_budgets.back()==600);
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(!AndroidBudgetHasLease() && fixture.budgets[1]==512);
}
void UnconfirmedGrowthTierChangeUsesPreviousOwn() {
    ResetFixture();Grow();fixture.desired=1300*MiB;
    fixture.ignore_next_native_write=true;fixture.omit_readback_after_write=true;
    Advance();CHECK(TickAndroidTextureBudget(true));
    CHECK(g_android_budget_policy.HasUnconfirmedAttempt() && fixture.budgets[0]==1152);
    CHECK(AndroidBudgetIoSnapshot().lease.previous_own==1152);
    NativeGameQuality(1);
    CHECK(fixture.budgets[0]==600 && fixture.quality_old_budgets.back()==600);
    CHECK(TickAndroidTextureBudget(false,true));
    CHECK(!g_android_budget_policy.HasOwnedOverride() && !AndroidBudgetHasLease() && fixture.budgets[1]==512);
}
}

int main() {
    using namespace BetterEndfield::CustomModel;
    struct Test {const char* name;void(*run)();};
    const std::array<Test,19> tests{{
        {"pump restriction and shutdown",PumpRestrictionAndShutdown},
        {"pressure confirmation and own epoch",PressureAndOwnEpoch},
        {"external equal own request",EqualExternalRequest},
        {"different tier and return",DifferentTierAndReturn},
        {"new tier equal own",NewTierEqualOwn},
        {"same tier does not flap",SameTierNoFlap},
        {"restore late readback",RestoreReadbackLateConfirmation},
        {"grow late readback",GrowReadbackLateConfirmation},
        {"rejected setter not claimed",RejectedSetterNeverClaimed},
        {"missing observations",MissingObservationsRestore},
        {"outside pump external equal request",OutsidePumpEqualExternalRequest},
        {"outside pump tier restore boundary",OutsidePumpTierRetainsUnrestoredBoundary},
        {"hook failure",HookFailureDisablesOwnWrites},
        {"invalid native restore readback",InvalidNativeRestoreReadback},
        {"same tier Apply echo",SameTierBudgetEchoPreservesLease},
        {"same tier different budget",SameTierDifferentBudgetIsExternal},
        {"rejected growth preserves lease",RejectedGrowthPreservesOwnership},
        {"rejected growth late readback",RejectedGrowthLateReadback},
        {"unconfirmed growth previous own",UnconfirmedGrowthTierChangeUsesPreviousOwn}
    }};
    uint32_t failures=0;
    for(const auto& test:tests) {
        try {test.run();}
        catch(const std::exception& error) {
            ++failures;std::cerr<<test.name<<": "<<error.what()<<'\n';
        }
    }
    std::cout<<"Android texture budget runtime: "<<tests.size()-failures<<'/'<<tests.size()
        <<" cases, "<<checks<<" checks, "<<failures<<" failures\n";
    return failures?1:0;
}
