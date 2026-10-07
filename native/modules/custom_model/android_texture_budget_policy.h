#pragma once

#include <algorithm>
#include <cstdint>
#include <limits>

namespace BetterEndfield::CustomModel {

// Pump-thread state only. The caller supplies engine/system observations and
// records external setter requests independently, excluding its own writes.
// No Unity calls, OS queries, allocations or setters occur in this policy.
class AndroidTextureBudgetPolicy {
public:
    static constexpr uint64_t MiB=1024ull*1024;
    static constexpr uint64_t Quantum=64*MiB;
    static constexpr uint64_t EvaluationMs=2000;
    static constexpr uint64_t ShrinkStableMs=30000;
    static constexpr uint64_t ShrinkHysteresis=128*MiB;

    struct Snapshot {
        uint64_t now_ms=0;
        bool active_valid=false,active=false,force_lod=false;
        bool budget_valid=false;
        uint64_t observed_budget_bytes=0;
        bool counters_valid=false;
        // Desired already includes nonstreaming textures. Never add BEM
        // payload, package counts, wrappers or nonstreaming memory again.
        uint64_t current_bytes=0,desired_bytes=0,target_bytes=0;
        bool memory_valid=false;
        uint64_t physical_ram_bytes=0,available_bytes=0;
        uint64_t external_request_epoch=0;
        bool external_budget_valid=false;
        uint64_t external_budget_bytes=0;
    };
    enum class Action {None,SetBudget,RestoreBase};
    enum class Reason {
        NoBase,ExternalRebase,Unavailable,Disabled,PendingWrite,
        AttemptUnconfirmed,Throttled,NoPressure,ConfirmingPressure,
        MemoryCap,GrowOwned,StableShrink,ShrinkPending,Hysteresis,Unchanged
    };
    struct Decision {
        Action action=Action::None;
        Reason reason=Reason::NoBase;
        uint64_t id=0,value_bytes=0,base_bytes=0,desired_bytes=0,cap_bytes=0;
        uint64_t request_epoch=0,expected_observed_budget_bytes=0;
    };

    bool HasOwnedOverride() const noexcept {return owned_ || attempt_.valid;}
    bool HasConfirmedOverride() const noexcept {return owned_;}
    bool HasPendingWrite() const noexcept {return pending_.action!=Action::None;}
    bool HasUnconfirmedAttempt() const noexcept {return attempt_.valid;}
    bool HasGameBase() const noexcept {return base_known_;}
    uint64_t GameBaseBytes() const noexcept {return base_;}
    uint64_t OwnedBudgetBytes() const noexcept {return owned_value_;}
    uint64_t RequestEpoch() const noexcept {return epoch_;}
    void Reset() noexcept {*this=AndroidTextureBudgetPolicy{};}

    static const char* ReasonText(Reason reason) noexcept {
        switch (reason) {
        case Reason::NoBase:return "no-game-base";
        case Reason::ExternalRebase:return "external-budget-rebase";
        case Reason::Unavailable:return "observations-unavailable";
        case Reason::Disabled:return "disabled-restore";
        case Reason::PendingWrite:return "write-awaits-commit";
        case Reason::AttemptUnconfirmed:return "write-awaits-readable-budget";
        case Reason::Throttled:return "evaluation-throttled";
        case Reason::NoPressure:return "no-confirmed-budget-pressure";
        case Reason::ConfirmingPressure:return "confirming-budget-pressure";
        case Reason::MemoryCap:return "memory-reserve-cap";
        case Reason::GrowOwned:return "owned-budget-follows-desired";
        case Reason::StableShrink:return "desired-stable-shrink";
        case Reason::ShrinkPending:return "waiting-for-stable-desired";
        case Reason::Hysteresis:return "shrink-hysteresis";
        case Reason::Unchanged:return "budget-unchanged";
        }
        return "unknown";
    }

    Decision Evaluate(const Snapshot& s) noexcept {
        const bool budget_ok=s.budget_valid && s.observed_budget_bytes>0;
        bool rebased=false;
        if (!epoch_seen_ || s.external_request_epoch!=epoch_) {
            const bool new_request=epoch_seen_ || s.external_request_epoch!=0;
            epoch_seen_=true;epoch_=s.external_request_epoch;
            if (new_request) {
                // A recorded external request equal to an own write still
                // relinquishes ownership. The live getter wins over an
                // asynchronously applied profile's recorded request value.
                owned_=false;owned_value_=0;attempt_={};pending_={};ResetDemand();
                if (budget_ok) {base_=s.observed_budget_bytes;base_known_=true;}
                else if (s.external_budget_valid && s.external_budget_bytes>0) {
                    base_=s.external_budget_bytes;base_known_=true;
                } else base_known_=false;
                rebased=true;
            }
        }
        if (attempt_.valid) {
            if (!budget_ok) return Report(s,Reason::AttemptUnconfirmed);
            if (s.observed_budget_bytes==attempt_.value) {
                base_=attempt_.base;base_known_=true;
                owned_=attempt_.action!=Action::RestoreBase;
                owned_value_=owned_?attempt_.value:0;
                attempt_={};
            } else {
                attempt_={};
                // A rejected increase can leave the previously confirmed own
                // value intact. It must not become a new game base either.
                if (!owned_ || s.observed_budget_bytes!=owned_value_) {
                    owned_=false;owned_value_=0;
                    base_=s.observed_budget_bytes;base_known_=true;ResetDemand();rebased=true;
                }
            }
        }
        if (budget_ok && (!base_known_ || (!owned_ && s.observed_budget_bytes!=base_) ||
            (owned_ && s.observed_budget_bytes!=owned_value_))) {
            base_=s.observed_budget_bytes;base_known_=true;owned_=false;owned_value_=0;
            pending_={};ResetDemand();rebased=true;
        }
        if (pending_.action!=Action::None) return Report(s,Reason::PendingWrite);
        if (!base_known_) return Report(s,Reason::NoBase);
        if (!budget_ok) return Report(s,Reason::Unavailable);

        const bool memory_ok=s.memory_valid && s.physical_ram_bytes>0 &&
            s.available_bytes<=s.physical_ram_bytes;
        const bool enabled=s.active_valid && s.active && s.force_lod;
        if (!enabled || !s.counters_valid || !memory_ok) {
            ResetDemand();
            const Reason why=(!s.active_valid || !s.counters_valid || !memory_ok)?Reason::Unavailable:Reason::Disabled;
            // Restoration is permitted only while the live observation still
            // equals our confirmed own value at the same external epoch.
            if (owned_ && s.observed_budget_bytes==owned_value_ && owned_value_!=base_)
                return Propose(s,base_,why,base_);
            return Report(s,why,base_);
        }

        const uint64_t cap=Cap(s);
        // A reduced safe allowance may be applied immediately, independently
        // of desired-demand shrink hysteresis or the ordinary 2-second gate.
        if (owned_ && cap<owned_value_) {
            ResetDemand();return Propose(s,cap,Reason::MemoryCap,cap);
        }
        if (evaluated_ && s.now_ms>=last_evaluation_ms_ &&
            s.now_ms-last_evaluation_ms_<EvaluationMs)
            return Report(s,Reason::Throttled,cap);
        if (evaluated_ && s.now_ms<last_evaluation_ms_) ResetDemand();
        evaluated_=true;last_evaluation_ms_=s.now_ms;
        const uint64_t wanted=(std::max)(base_,(std::min)(cap,RoundUp(SaturatingAdd(s.desired_bytes,Quantum))));
        if (!owned_) {
            const bool pressure=s.desired_bytes>s.target_bytes &&
                s.desired_bytes-s.target_bytes>Quantum &&
                s.target_bytes>=s.observed_budget_bytes-s.observed_budget_bytes/50;
            if (!pressure) {pressure_seen_=false;return Report(s,rebased?Reason::ExternalRebase:Reason::NoPressure,cap);}
            if (wanted<=base_) {pressure_seen_=false;return Report(s,Reason::MemoryCap,cap);}
            if (!pressure_seen_) {pressure_seen_=true;pressure_since_ms_=s.now_ms;return Report(s,Reason::ConfirmingPressure,cap);}
            if (s.now_ms<pressure_since_ms_ || s.now_ms-pressure_since_ms_<EvaluationMs)
                return Report(s,Reason::ConfirmingPressure,cap);
            ResetDemand();return Propose(s,wanted,Reason::GrowOwned,cap);
        }
        if (wanted>owned_value_) {shrink_seen_=false;return Propose(s,wanted,Reason::GrowOwned,cap);}
        if (wanted==owned_value_) {shrink_seen_=false;return Report(s,Reason::Unchanged,cap);}
        if (owned_value_-wanted<ShrinkHysteresis) {shrink_seen_=false;return Report(s,Reason::Hysteresis,cap);}
        if (!shrink_seen_ || wanted!=shrink_candidate_ || s.now_ms<shrink_since_ms_) {
            shrink_seen_=true;shrink_candidate_=wanted;shrink_since_ms_=s.now_ms;
            return Report(s,Reason::ShrinkPending,cap);
        }
        if (s.now_ms-shrink_since_ms_<ShrinkStableMs) return Report(s,Reason::ShrinkPending,cap);
        ResetDemand();return Propose(s,wanted,Reason::StableShrink,cap);
    }

    // Recheck these immediately before executing the setter. A changed
    // external epoch is significant even if the observed value is identical.
    bool CanApply(const Decision& d,bool budget_valid,uint64_t observed_bytes,uint64_t external_epoch) const noexcept {
        return d.action!=Action::None && pending_.id==d.id && pending_.action==d.action &&
            pending_.value_bytes==d.value_bytes && budget_valid && observed_bytes==d.expected_observed_budget_bytes &&
            external_epoch==d.request_epoch && external_epoch==epoch_;
    }
    void Cancel(const Decision& d) noexcept {if (pending_.id==d.id) pending_={};}

    // Return true only for immediate setter success plus exact readback.
    // Missing readback retains an attempted write; subsequent observations can
    // confirm it without rebasing the game to our own possibly applied value.
    bool Commit(const Decision& d,bool setter_ok,bool readback_valid,uint64_t readback_bytes,uint64_t external_epoch) noexcept {
        if (d.action==Action::None || pending_.id!=d.id || pending_.action!=d.action || pending_.value_bytes!=d.value_bytes)
            return false;
        pending_={};
        if (external_epoch!=d.request_epoch) {
            owned_=false;owned_value_=0;attempt_={};ResetDemand();
            if (readback_valid && readback_bytes>0) {base_=readback_bytes;base_known_=true;}
            else base_known_=false;
            // Keep epoch_ until a Snapshot with the recorded external value
            // arrives; Evaluate will consume that new external request.
            return false;
        }
        if (setter_ok && readback_valid && readback_bytes==d.value_bytes) {
            owned_=d.action!=Action::RestoreBase;owned_value_=owned_?d.value_bytes:0;attempt_={};
            return true;
        }
        if (!readback_valid || readback_bytes==d.value_bytes) {
            attempt_={true,d.value_bytes,d.base_bytes,d.request_epoch,d.action};
        } else if (readback_bytes>0 && (!owned_ || readback_bytes!=owned_value_)) {
            owned_=false;owned_value_=0;base_=readback_bytes;base_known_=true;ResetDemand();
        }
        return false;
    }

private:
    struct Attempt {
        bool valid=false;
        uint64_t value=0,base=0,epoch=0;
        Action action=Action::None;
    };
    static uint64_t SaturatingAdd(uint64_t a,uint64_t b) noexcept {
        return a>(std::numeric_limits<uint64_t>::max)()-b?(std::numeric_limits<uint64_t>::max)():a+b;
    }
    static uint64_t RoundUp(uint64_t bytes) noexcept {
        const uint64_t remainder=bytes%Quantum;
        return remainder?SaturatingAdd(bytes,Quantum-remainder):bytes;
    }
    uint64_t Cap(const Snapshot& s) const noexcept {
        const uint64_t reserve=(std::max)(1024*MiB,s.physical_ram_bytes/8);
        // Account only for actually resident texture memory. Neither the base
        // nor an own budget is evidence that its full amount is resident.
        uint64_t memory_total=0;
        if (s.available_bytes>=reserve) memory_total=SaturatingAdd(s.current_bytes,s.available_bytes-reserve);
        else {
            const uint64_t deficit=reserve-s.available_bytes;
            memory_total=s.current_bytes>deficit?s.current_bytes-deficit:0;
        }
        const uint64_t pool=(std::min)(2048*MiB,s.physical_ram_bytes/6);
        const uint64_t safe_total=(std::max)(base_,(std::min)(pool,memory_total));
        return (std::max)(base_,safe_total-safe_total%Quantum);
    }
    Decision Report(const Snapshot& s,Reason why,uint64_t cap=0) const noexcept {
        Decision d;d.reason=why;d.base_bytes=base_known_?base_:0;d.desired_bytes=s.desired_bytes;
        d.cap_bytes=cap;d.request_epoch=epoch_;d.expected_observed_budget_bytes=s.observed_budget_bytes;
        return d;
    }
    Decision Propose(const Snapshot& s,uint64_t value,Reason why,uint64_t cap) noexcept {
        Decision d=Report(s,why,cap);d.action=value==base_?Action::RestoreBase:Action::SetBudget;
        d.id=++sequence_;d.value_bytes=value;pending_=d;return d;
    }
    void ResetDemand() noexcept {pressure_seen_=false;shrink_seen_=false;}
    bool base_known_=false,owned_=false,epoch_seen_=false,evaluated_=false;
    bool pressure_seen_=false,shrink_seen_=false;
    uint64_t base_=0,owned_value_=0,epoch_=0,sequence_=0,last_evaluation_ms_=0;
    uint64_t pressure_since_ms_=0,shrink_since_ms_=0,shrink_candidate_=0;
    Attempt attempt_{};
    Decision pending_{};
};

} // namespace BetterEndfield::CustomModel
