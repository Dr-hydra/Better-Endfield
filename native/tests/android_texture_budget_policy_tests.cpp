#include <cstdint>
#include <iostream>
#include <limits>
#include <stdexcept>
#include <string>

#include "../modules/custom_model/android_texture_budget_policy.h"

namespace {
using Policy=BetterEndfield::CustomModel::AndroidTextureBudgetPolicy;
using Action=Policy::Action;
using Reason=Policy::Reason;
constexpr uint64_t M=Policy::MiB;
unsigned checks=0;
void Check(bool value,const char* expression,int line) {
    ++checks;
    if (!value) throw std::runtime_error("budget policy check failed at line "+std::to_string(line)+": "+expression);
}
#define CHECK(expression) Check((expression),#expression,__LINE__)

Policy::Snapshot Valid(uint64_t base_mib=600) {
    Policy::Snapshot s;
    s.active_valid=true;s.active=true;s.force_lod=true;s.budget_valid=true;
    s.observed_budget_bytes=base_mib*M;s.counters_valid=true;
    s.current_bytes=base_mib*M;s.target_bytes=base_mib*M;s.desired_bytes=1038*M;
    s.memory_valid=true;s.physical_ram_bytes=8192*M;s.available_bytes=3072*M;
    s.external_request_epoch=10;s.external_budget_valid=true;s.external_budget_bytes=base_mib*M;
    return s;
}
Policy::Decision StartGrowth(Policy& p,Policy::Snapshot& s,uint64_t value_mib=1152) {
    auto d=p.Evaluate(s);
    CHECK(d.action==Action::None && d.reason==Reason::ConfirmingPressure);
    s.now_ms+=2000;d=p.Evaluate(s);
    CHECK(d.action==Action::SetBudget && d.value_bytes==value_mib*M);
    CHECK(d.value_bytes%Policy::Quantum==0);
    CHECK(p.HasPendingWrite() && !p.HasConfirmedOverride());
    CHECK(p.CanApply(d,true,s.observed_budget_bytes,s.external_request_epoch));
    return d;
}
void Grow(Policy& p,Policy::Snapshot& s,uint64_t value_mib=1152) {
    const auto base=s.observed_budget_bytes;
    auto d=StartGrowth(p,s,value_mib);
    CHECK(p.Commit(d,true,true,d.value_bytes,s.external_request_epoch));
    CHECK(p.HasOwnedOverride() && p.HasConfirmedOverride() && !p.HasPendingWrite());
    CHECK(p.GameBaseBytes()==base && p.OwnedBudgetBytes()==d.value_bytes);
    s.observed_budget_bytes=d.value_bytes;s.current_bytes=d.value_bytes;s.target_bytes=d.value_bytes;
}
}

int main() {
    try {
        // First pressure needs two observations at least 2 s apart. The policy
        // has no loading-state or BEM wrapper/count inputs that could refund it.
        {
            Policy p;auto s=Valid();auto d=p.Evaluate(s);
            CHECK(d.reason==Reason::ConfirmingPressure && d.action==Action::None);
            s.now_ms=1999;d=p.Evaluate(s);CHECK(d.reason==Reason::Throttled && d.action==Action::None);
            s.now_ms=2000;d=p.Evaluate(s);CHECK(d.action==Action::SetBudget && d.value_bytes==1152*M);
            CHECK(d.base_bytes==600*M && d.desired_bytes==1038*M && d.cap_bytes==1344*M && d.request_epoch==10);
            CHECK(!p.CanApply(d,true,600*M,11));CHECK(!p.CanApply(d,true,601*M,10));CHECK(!p.CanApply(d,false,600*M,10));
            CHECK(p.Evaluate(s).reason==Reason::PendingWrite);
            p.Cancel(d);CHECK(!p.HasPendingWrite() && !p.HasOwnedOverride());
        }
        {
            Policy p;auto s=Valid();p.Evaluate(s);s.now_ms=2000;s.target_bytes=500*M;
            CHECK(p.Evaluate(s).reason==Reason::NoPressure);
            s.now_ms=4000;s.target_bytes=600*M;CHECK(p.Evaluate(s).reason==Reason::ConfirmingPressure);
            s.now_ms=6000;CHECK(p.Evaluate(s).action==Action::SetBudget);
            Policy below;auto low=Valid();low.target_bytes=587*M;low.desired_bytes=900*M;
            CHECK(below.Evaluate(low).action==Action::None);low.now_ms+=2000;
            CHECK(below.Evaluate(low).reason==Reason::NoPressure);
            Policy edge;low.target_bytes=588*M;CHECK(edge.Evaluate(low).reason==Reason::ConfirmingPressure);
            Policy exact;auto gap=Valid();gap.desired_bytes=664*M;CHECK(exact.Evaluate(gap).action==Action::None);
            gap.now_ms+=2000;CHECK(exact.Evaluate(gap).reason==Reason::NoPressure);
            Policy over;gap.desired_bytes=665*M;auto d=StartGrowth(over,gap,768);CHECK(d.value_bytes==768*M);
        }
        // Own observations cannot become a new base or accumulate compensation.
        {
            Policy p;auto s=Valid();Grow(p,s);s.current_bytes=600*M;
            for (int i=0;i<20;++i) {s.now_ms+=2000;CHECK(p.Evaluate(s).action==Action::None);CHECK(p.GameBaseBytes()==600*M);}
            s.desired_bytes=1400*M;s.now_ms+=2000;auto d=p.Evaluate(s);
            CHECK(d.action==Action::SetBudget && d.value_bytes==1344*M && d.base_bytes==600*M);
            CHECK(p.Commit(d,true,true,d.value_bytes,10));
            s.observed_budget_bytes=d.value_bytes;s.current_bytes=d.value_bytes;s.target_bytes=d.value_bytes;
            s.now_ms+=1;s.force_lod=false;d=p.Evaluate(s);
            CHECK(d.action==Action::RestoreBase && d.value_bytes==600*M);
            CHECK(p.Commit(d,true,true,600*M,10));CHECK(!p.HasOwnedOverride());
        }
        // External equal-valued requests still transfer ownership to the game.
        {
            Policy p;auto s=Valid();Grow(p,s);s.external_request_epoch=11;
            s.external_budget_bytes=s.observed_budget_bytes;s.force_lod=false;s.now_ms+=1;
            auto d=p.Evaluate(s);CHECK(d.action==Action::None && p.GameBaseBytes()==1152*M && !p.HasOwnedOverride());
            CHECK(p.OwnedBudgetBytes()==0 && p.RequestEpoch()==11);
            s.external_request_epoch=12;s.external_budget_bytes=600*M;s.observed_budget_bytes=850*M;
            CHECK(p.Evaluate(s).action==Action::None && p.GameBaseBytes()==850*M);
        }
        {
            Policy p;auto s=Valid();Grow(p,s);s.observed_budget_bytes=700*M;s.force_lod=false;
            CHECK(p.Evaluate(s).action==Action::None && p.GameBaseBytes()==700*M && !p.HasOwnedOverride());
        }
        // Neither huge startup profiles nor a game's own high base are clipped.
        {
            Policy p;auto s=Valid(9043);s.desired_bytes=10000*M;
            CHECK(p.Evaluate(s).action==Action::None && p.GameBaseBytes()==9043*M);
            s.now_ms+=2000;CHECK(p.Evaluate(s).action==Action::None);
            s.external_request_epoch=11;s.external_budget_bytes=850*M;s.observed_budget_bytes=850*M;
            s.current_bytes=850*M;s.target_bytes=850*M;s.desired_bytes=1300*M;s.now_ms+=2000;
            CHECK(p.Evaluate(s).reason==Reason::ConfirmingPressure);
            s.now_ms+=2000;auto d=p.Evaluate(s);CHECK(d.value_bytes==1344*M && d.base_bytes==850*M);
            p.Cancel(d);s.external_request_epoch=12;s.external_budget_bytes=600*M;s.observed_budget_bytes=600*M;
            s.target_bytes=600*M;s.current_bytes=600*M;s.desired_bytes=1038*M;s.now_ms+=2000;
            CHECK(p.Evaluate(s).reason==Reason::ConfirmingPressure && p.GameBaseBytes()==600*M);
            s.physical_ram_bytes=3072*M;s.now_ms+=2000;CHECK(p.Evaluate(s).action==Action::None);
            CHECK(p.GameBaseBytes()==600*M);
        }
        // RAM/6 and 2 GiB cap the pool only above the game baseline. Desired
        // already includes all textures; the result is desired + one margin.
        {
            Policy p;auto s=Valid();s.physical_ram_bytes=16384*M;s.available_bytes=8192*M;s.desired_bytes=3000*M;
            auto d=StartGrowth(p,s,2048);CHECK(d.cap_bytes==2048*M);
            Policy small;auto q=Valid();q.physical_ram_bytes=4096*M;q.available_bytes=1152*M;
            d=StartGrowth(small,q,640);CHECK(d.cap_bytes==640*M);
            Policy reserve;auto r=Valid();r.physical_ram_bytes=4096*M;r.available_bytes=1024*M;
            CHECK(reserve.Evaluate(r).action==Action::None);r.now_ms+=2000;
            CHECK(reserve.Evaluate(r).cap_bytes==600*M);
            Policy demand;auto t=Valid();d=StartGrowth(demand,t);
            CHECK(d.value_bytes==1152*M); // Not desired + nonstreaming/payload + margin.
        }
        // Available headroom does not credit unresident game or own budgets.
        {
            Policy p;auto s=Valid();s.current_bytes=100*M;s.available_bytes=1280*M;
            auto d=p.Evaluate(s);CHECK(d.action==Action::None && d.cap_bytes==600*M);
            s.now_ms+=2000;CHECK(p.Evaluate(s).action==Action::None);
            Policy own;auto q=Valid();Grow(own,q);q.current_bytes=600*M;q.available_bytes=1280*M;
            q.now_ms+=1;d=own.Evaluate(q);CHECK(d.action==Action::SetBudget && d.value_bytes==832*M);
            CHECK(d.reason==Reason::MemoryCap && d.base_bytes==600*M);
        }
        // Low memory can shrink immediately, subtracting reserve deficits.
        {
            Policy p;auto s=Valid();Grow(p,s);s.available_bytes=800*M;s.desired_bytes=2000*M;s.now_ms+=1;
            auto d=p.Evaluate(s);CHECK(d.reason==Reason::MemoryCap && d.value_bytes==896*M);
            CHECK(p.Commit(d,true,true,d.value_bytes,10));s.observed_budget_bytes=d.value_bytes;
            s.current_bytes=200*M;s.available_bytes=0;s.now_ms+=1;d=p.Evaluate(s);
            CHECK(d.action==Action::RestoreBase && d.value_bytes==600*M);
            Policy high;auto h=Valid(850);h.desired_bytes=950*M;Grow(high,h,1024);
            h.physical_ram_bytes=3072*M;h.available_bytes=256*M;h.now_ms+=1;d=high.Evaluate(h);
            CHECK(d.action==Action::RestoreBase && d.value_bytes==850*M);
        }
        // Ordinary shrink waits 30 s for a quantized candidate and 128 MiB
        // hysteresis. Small desired byte changes must not prevent recycling.
        {
            Policy p;auto s=Valid();Grow(p,s);s.desired_bytes=600*M;s.now_ms+=2000;
            CHECK(p.Evaluate(s).reason==Reason::ShrinkPending);
            for (int i=1;i<15;++i) {s.now_ms+=2000;s.desired_bytes=600*M+static_cast<uint64_t>(i)*4096;CHECK(p.Evaluate(s).action==Action::None);}
            s.now_ms+=2000;auto d=p.Evaluate(s);CHECK(d.action==Action::SetBudget && d.value_bytes==704*M);
            CHECK(d.reason==Reason::StableShrink && p.GameBaseBytes()==600*M);
            Policy lag;auto q=Valid();Grow(lag,q);q.desired_bytes=1000*M;q.now_ms+=2000;
            CHECK(lag.Evaluate(q).reason==Reason::Hysteresis);
            q.now_ms+=60000;CHECK(lag.Evaluate(q).action==Action::None);
        }
        // A candidate crossing or demand recovery restarts the stable window.
        {
            Policy p;auto s=Valid();Grow(p,s);s.desired_bytes=600*M;s.now_ms+=2000;p.Evaluate(s);
            s.now_ms+=28000;CHECK(p.Evaluate(s).action==Action::None);
            s.desired_bytes=650*M;s.now_ms+=2000;CHECK(p.Evaluate(s).reason==Reason::ShrinkPending);
            s.now_ms+=28000;CHECK(p.Evaluate(s).action==Action::None);
            s.now_ms+=2000;auto d=p.Evaluate(s);CHECK(d.action==Action::SetBudget && d.value_bytes==768*M);
            p.Cancel(d);s.desired_bytes=1038*M;s.now_ms+=2000;CHECK(p.Evaluate(s).reason==Reason::Unchanged);
            s.desired_bytes=600*M;s.now_ms+=2000;CHECK(p.Evaluate(s).reason==Reason::ShrinkPending);
            s.now_ms+=28000;CHECK(p.Evaluate(s).action==Action::None);
            s.now_ms+=2000;CHECK(p.Evaluate(s).value_bytes==704*M);
        }
        // Every unavailable observation disables growth, and restores only a
        // verified own value. A missing budget getter never permits a setter.
        for (int invalid=0;invalid<6;++invalid) {
            Policy p;auto s=Valid();
            if (invalid==0) s.active_valid=false;
            if (invalid==1) s.counters_valid=false;
            if (invalid==2) s.memory_valid=false;
            if (invalid==3) s.physical_ram_bytes=0;
            if (invalid==4) s.available_bytes=s.physical_ram_bytes+1;
            if (invalid==5) s.budget_valid=false;
            CHECK(p.Evaluate(s).action==Action::None);s.now_ms+=2000;CHECK(p.Evaluate(s).action==Action::None);
        }
        for (int invalid=0;invalid<5;++invalid) {
            Policy p;auto s=Valid();Grow(p,s);s.now_ms+=1;
            if (invalid==0) s.active_valid=false;
            if (invalid==1) s.active=false;
            if (invalid==2) s.counters_valid=false;
            if (invalid==3) s.memory_valid=false;
            if (invalid==4) s.force_lod=false;
            auto d=p.Evaluate(s);CHECK(d.action==Action::RestoreBase && d.value_bytes==600*M);
        }
        {
            Policy p;auto s=Valid();Grow(p,s);s.budget_valid=false;s.force_lod=false;
            CHECK(p.Evaluate(s).action==Action::None && p.HasOwnedOverride());
            s.budget_valid=true;s.observed_budget_bytes=700*M;
            CHECK(p.Evaluate(s).action==Action::None && !p.HasOwnedOverride() && p.GameBaseBytes()==700*M);
        }
        // Native writes may succeed before readback fails. Recover ownership
        // from the next reliable observation instead of accumulating our base.
        {
            Policy p;auto s=Valid();auto d=StartGrowth(p,s);
            CHECK(!p.Commit(d,true,false,0,10));CHECK(p.HasOwnedOverride() && !p.HasConfirmedOverride() && p.HasUnconfirmedAttempt());
            CHECK(p.GameBaseBytes()==600*M);
            s.budget_valid=false;s.force_lod=false;s.now_ms+=1;auto waiting=p.Evaluate(s);
            CHECK(waiting.action==Action::None && waiting.reason==Reason::AttemptUnconfirmed);
            CHECK(p.HasOwnedOverride() && !p.HasConfirmedOverride() && p.GameBaseBytes()==600*M);
            s.budget_valid=true;s.observed_budget_bytes=d.value_bytes;s.now_ms+=1;
            d=p.Evaluate(s);CHECK(d.action==Action::RestoreBase && d.value_bytes==600*M && p.HasConfirmedOverride());
            CHECK(p.Commit(d,true,true,600*M,10));CHECK(!p.HasOwnedOverride());
            Policy expand;auto q=Valid();d=StartGrowth(expand,q);CHECK(!expand.Commit(d,true,false,0,10));
            q.observed_budget_bytes=d.value_bytes;q.current_bytes=d.value_bytes;q.target_bytes=d.value_bytes;
            q.desired_bytes=2000*M;q.now_ms+=2000;d=expand.Evaluate(q);
            CHECK(d.value_bytes==1344*M && d.base_bytes==600*M && expand.GameBaseBytes()==600*M);
        }
        {
            Policy p;auto s=Valid();Grow(p,s);s.force_lod=false;auto d=p.Evaluate(s);
            CHECK(!p.Commit(d,true,false,0,10));CHECK(p.HasOwnedOverride() && p.HasUnconfirmedAttempt());
            s.observed_budget_bytes=600*M;s.now_ms+=1;
            CHECK(p.Evaluate(s).action==Action::None && !p.HasOwnedOverride() && p.GameBaseBytes()==600*M);
        }
        {
            Policy p;auto s=Valid();auto d=StartGrowth(p,s);
            CHECK(!p.Commit(d,false,true,600*M,10));CHECK(!p.HasOwnedOverride() && !p.HasUnconfirmedAttempt());
            Policy missing;auto q=Valid();d=StartGrowth(missing,q);CHECK(!missing.Commit(d,false,false,0,10));
            CHECK(missing.HasOwnedOverride());q.observed_budget_bytes=850*M;q.force_lod=false;
            CHECK(missing.Evaluate(q).action==Action::None && missing.GameBaseBytes()==850*M && !missing.HasOwnedOverride());
            Policy rejected;auto r=Valid();d=StartGrowth(rejected,r);CHECK(!rejected.Commit(d,false,true,d.value_bytes,10));
            CHECK(!rejected.HasConfirmedOverride() && rejected.HasUnconfirmedAttempt());
            r.observed_budget_bytes=d.value_bytes;r.force_lod=false;CHECK(rejected.Evaluate(r).action==Action::RestoreBase);
        }
        // New external epochs, including equal values, invalidate pending
        // attempts. A setter execution race must never restore an old base.
        {
            Policy p;auto s=Valid();auto d=StartGrowth(p,s);CHECK(!p.Commit(d,true,false,0,10));
            s.observed_budget_bytes=d.value_bytes;s.external_request_epoch=11;s.external_budget_bytes=d.value_bytes;s.force_lod=false;
            CHECK(p.Evaluate(s).action==Action::None && p.GameBaseBytes()==1152*M && !p.HasOwnedOverride());
            Policy race;auto q=Valid();d=StartGrowth(race,q);
            CHECK(!race.Commit(d,true,true,850*M,11));CHECK(!race.HasOwnedOverride() && race.GameBaseBytes()==850*M);
            q.observed_budget_bytes=850*M;q.external_request_epoch=11;q.external_budget_bytes=850*M;q.force_lod=false;
            CHECK(race.Evaluate(q).action==Action::None && race.GameBaseBytes()==850*M);
        }
        // Failed growth preserving a prior confirmed override must not rebase
        // that old own value. Restore still refers to the original 600 MiB.
        {
            Policy p;auto s=Valid();Grow(p,s);s.desired_bytes=2000*M;s.now_ms+=2000;auto d=p.Evaluate(s);
            CHECK(d.value_bytes==1344*M);CHECK(!p.Commit(d,false,true,1152*M,10));
            CHECK(p.HasConfirmedOverride() && p.OwnedBudgetBytes()==1152*M && p.GameBaseBytes()==600*M);
            s.force_lod=false;CHECK(p.Evaluate(s).value_bytes==600*M);
        }
        // Saturated counters and rollback of the clock cannot overflow writes.
        {
            Policy p;auto s=Valid();s.desired_bytes=(std::numeric_limits<uint64_t>::max)();
            auto d=StartGrowth(p,s,1344);CHECK(d.value_bytes<=2048*M);
            p.Cancel(d);p.Reset();CHECK(!p.HasOwnedOverride() && !p.HasGameBase() && !p.HasPendingWrite());
            s=Valid();s.now_ms=9000;CHECK(p.Evaluate(s).reason==Reason::ConfirmingPressure);
            s.now_ms=1;CHECK(p.Evaluate(s).reason==Reason::ConfirmingPressure);
            s.now_ms=2001;CHECK(p.Evaluate(s).action==Action::SetBudget);
        }
        std::cout<<"PASS Android production texture budget policy: "<<checks<<" Release-active CHECKs; pressure, owned/base isolation, epochs, caps/reserves, actual residency, shrink stability/hysteresis, unavailable getters, late readback and setter races\n";
        return 0;
    } catch (const std::exception& e) {std::cerr<<e.what()<<'\n';return 1;}
}
