// Android's forced resource LOD can outgrow the game's original texture pool.
// All policy evaluation and writes run on the confirmed Unity pump. Native
// observers distinguish game requests from our writes, including equal values.
using AndroidBudgetSetterFn=void(*)(float);
using AndroidBudgetGetterFn=float(*)();
using AndroidQualitySetterFn=void(*)(int32_t,bool);
using AndroidQualityGetterFn=int32_t(*)();
AndroidBudgetSetterFn g_original_android_budget_setter=nullptr;
AndroidQualitySetterFn g_original_android_quality_setter=nullptr;
AndroidBudgetGetterFn g_android_budget_getter=nullptr;
AndroidQualityGetterFn g_android_quality_getter=nullptr;
std::atomic<bool> g_android_budget_hooks_ready{false};
thread_local bool g_android_internal_budget_write=false;
thread_local bool g_android_quality_hook_active=false;
struct AndroidBudgetLease {
    bool valid=false;
    int32_t tier=-1;
    float base=0,own=0;
    uint64_t epoch=0;
    float previous_own=0;
    bool external_override=false;
};
struct AndroidBudgetIo {
    uint64_t epoch=0;
    bool external_valid=false;
    float external_value=0;
    AndroidBudgetLease lease;
};
std::mutex g_android_budget_io_mutex;
// This gate serializes native setter operations, while the metadata mutex is
// never held across a Unity call. Nested quality callbacks remain reentrant.
std::recursive_mutex g_android_budget_execution_mutex;
AndroidBudgetIo g_android_budget_io;
AndroidTextureBudgetPolicy g_android_budget_policy;
uint64_t g_android_next_budget_ms=0,g_android_next_budget_report_ms=0;

bool AndroidBudgetBytes(float value,uint64_t& bytes) {
    if(!std::isfinite(value) || value<=0 || value>65536) return false;
    bytes=static_cast<uint64_t>(static_cast<double>(value)*AndroidTextureBudgetPolicy::MiB);
    return bytes>0;
}
struct AndroidBudgetOwnWrite {
    bool previous=g_android_internal_budget_write;
    AndroidBudgetOwnWrite() {g_android_internal_budget_write=true;}
    ~AndroidBudgetOwnWrite() {g_android_internal_budget_write=previous;}
};
AndroidBudgetIo AndroidBudgetIoSnapshot() {
    std::lock_guard lock(g_android_budget_io_mutex);return g_android_budget_io;
}
bool AndroidBudgetHasLease() {return AndroidBudgetIoSnapshot().lease.valid;}
void AndroidRecordBudgetRequest(float value,bool valid,int32_t tier,bool budget_request=true) {
    uint64_t ignored=0;
    std::lock_guard lock(g_android_budget_io_mutex);
    ++g_android_budget_io.epoch;
    g_android_budget_io.external_valid=valid && AndroidBudgetBytes(value,ignored);
    g_android_budget_io.external_value=value;
    // A request for a different tier cannot release an older tier's lease.
    if(g_android_budget_io.lease.valid && tier==g_android_budget_io.lease.tier)
        g_android_budget_io.lease={};
    else if(budget_request && tier<0 && g_android_budget_io.lease.valid)
        g_android_budget_io.lease.external_override=true;
}
void AndroidObserveTextureBudget(float value) {
    std::lock_guard execution(g_android_budget_execution_mutex);
    if(!g_original_android_budget_setter) return;
    if(g_android_budget_hooks_ready.load(std::memory_order_acquire) && !g_android_internal_budget_write) {
        const bool pump=AndroidTextureDiagnosticsOnPump();
        const int32_t tier=pump?g_android_quality_getter():-1;
        const auto io=AndroidBudgetIoSnapshot();
        const bool apply_echo=g_android_quality_hook_active && io.lease.valid &&
            !io.lease.external_override && io.lease.tier==tier && io.lease.own==value;
        if(!apply_echo) AndroidRecordBudgetRequest(value,true,tier);
    }
    g_original_android_budget_setter(value);
}
bool AndroidRestoreTierLease() {
    std::lock_guard execution(g_android_budget_execution_mutex);
    if(!AndroidTextureDiagnosticsOnPump()) return false;
    const auto io=AndroidBudgetIoSnapshot();
    if(!io.lease.valid) return true;
    const int32_t tier=g_android_quality_getter();
    const float observed=g_android_budget_getter();
    uint64_t observed_bytes=0;
    if(tier<0 || tier!=io.lease.tier || !AndroidBudgetBytes(observed,observed_bytes)) return false;
    if(!io.lease.external_override &&
        (observed==io.lease.own || (io.lease.previous_own>0 && observed==io.lease.previous_own))) {
        AndroidBudgetOwnWrite own;
        g_original_android_budget_setter(io.lease.base);
        const float readback=g_android_budget_getter();
        uint64_t readback_bytes=0;
        if(g_android_quality_getter()!=tier || !AndroidBudgetBytes(readback,readback_bytes) || readback!=io.lease.base) return false;
    }
    // An external value in the same tier supersedes our ownership. Only clear
    // the unchanged lease, so an intervening game request cannot be discarded.
    std::lock_guard lock(g_android_budget_io_mutex);
    if(g_android_budget_io.epoch!=io.epoch) return false;
    g_android_budget_io.lease={};return true;
}
void AndroidObserveQualityLevel(int32_t level,bool expensive) {
    std::lock_guard execution(g_android_budget_execution_mutex);
    if(!g_original_android_quality_setter) return;
    if(!g_android_budget_hooks_ready.load(std::memory_order_acquire) ||
        g_android_quality_hook_active || g_android_internal_budget_write) {
        g_original_android_quality_setter(level,expensive);return;
    }
    g_android_quality_hook_active=true;
    struct EndHook {~EndHook(){g_android_quality_hook_active=false;}} end;
    const bool pump=AndroidTextureDiagnosticsOnPump();
    const int32_t before=pump?g_android_quality_getter():-1;
    // Restore the old tier's stored budget before leaving it. Otherwise a
    // return to that tier would mistake our saved override for a game base.
    if(pump && before!=level && !AndroidRestoreTierLease())
        Log("Android texture budget: old tier restoration not confirmed before quality change");
    g_original_android_quality_setter(level,expensive);
    if(!pump) {AndroidRecordBudgetRequest(0,false,-1,false);return;}
    const int32_t after=g_android_quality_getter();
    // Applying the same stored tier does not request a different budget.
    if(after!=before) AndroidRecordBudgetRequest(g_android_budget_getter(),true,after,false);
}

void InitializeAndroidTextureBudget() {
    g_android_budget_hooks_ready.store(false,std::memory_order_release);
    g_android_budget_policy.Reset();g_android_next_budget_ms=0;g_android_next_budget_report_ms=0;
    {std::lock_guard lock(g_android_budget_io_mutex);g_android_budget_io={};}
    auto* setter=Contract("android.streaming_budget_set");
    void* budget_entry=betterendfieldnext::AndroidTextureBudgetSetterEntry();
    void* quality_entry=betterendfieldnext::AndroidQualityLevelSetterEntry();
    g_android_budget_getter=reinterpret_cast<AndroidBudgetGetterFn>(betterendfieldnext::AndroidTextureBudgetGetterEntry());
    g_android_quality_getter=reinterpret_cast<AndroidQualityGetterFn>(betterendfieldnext::AndroidQualityLevelGetterEntry());
    const bool available=setter && setter->resolved && budget_entry && quality_entry &&
        g_android_budget_getter && g_android_quality_getter;
    const bool budget_hook=available && g_host->create_hook(g_host->context,kModuleId,budget_entry,
        reinterpret_cast<void*>(&AndroidObserveTextureBudget),
        reinterpret_cast<void**>(&g_original_android_budget_setter))==BE_Result_Ok && g_original_android_budget_setter;
    const bool quality_hook=budget_hook && g_host->create_hook(g_host->context,kModuleId,quality_entry,
        reinterpret_cast<void*>(&AndroidObserveQualityLevel),
        reinterpret_cast<void**>(&g_original_android_quality_setter))==BE_Result_Ok && g_original_android_quality_setter;
    g_android_budget_hooks_ready.store(quality_hook,std::memory_order_release);
    Log(quality_hook?"Android texture budget coordinator ready: native game requests and tier changes tracked":
        "Android texture budget coordinator unavailable: original budget retained");
}

bool TickAndroidTextureBudget(bool force_lod,bool restoring=false) noexcept {
    try {
        if(!AndroidTextureDiagnosticsOnPump()) return false;
        if(!g_android_budget_hooks_ready.load(std::memory_order_acquire)) return !g_android_budget_policy.HasOwnedOverride();
        const uint64_t now=GetTickCount64();
        if(!restoring && now<g_android_next_budget_ms) return true;
        g_android_next_budget_ms=now+AndroidTextureBudgetPolicy::EvaluationMs;
        AndroidTextureBudgetPolicy::Snapshot s;
        s.now_ms=now;s.force_lod=force_lod && !restoring;
        uint8_t active=0;float budget=0;
        s.active_valid=InvokeValue(Contract("android.streaming_active"),nullptr,nullptr,active);s.active=active!=0;
        s.budget_valid=InvokeValue(Contract("android.streaming_budget"),nullptr,nullptr,budget) &&
            AndroidBudgetBytes(budget,s.observed_budget_bytes);
        s.counters_valid=InvokeValue(Contract("android.texture_current"),nullptr,nullptr,s.current_bytes) &&
            InvokeValue(Contract("android.texture_desired"),nullptr,nullptr,s.desired_bytes) &&
            InvokeValue(Contract("android.texture_target"),nullptr,nullptr,s.target_bytes);
        s.memory_valid=betterendfieldnext::AndroidReadMemoryHeadroom(s.physical_ram_bytes,s.available_bytes);
        auto io=AndroidBudgetIoSnapshot();
        s.external_request_epoch=io.epoch;
        s.external_budget_valid=io.external_valid && AndroidBudgetBytes(io.external_value,s.external_budget_bytes);
        const auto d=g_android_budget_policy.Evaluate(s);
        // Reconcile late readback and outside-pump request ownership even
        // when Evaluate has no new setter proposal.
        {
            std::lock_guard execution(g_android_budget_execution_mutex);
            const int32_t current_tier=g_android_quality_getter();
            std::lock_guard metadata(g_android_budget_io_mutex);
            auto& lease=g_android_budget_io.lease;
            if(lease.valid && lease.tier==current_tier && s.budget_valid &&
                g_android_budget_io.epoch==s.external_request_epoch) {
                if(!g_android_budget_policy.HasOwnedOverride() &&
                    (lease.external_override || budget!=lease.own)) lease={};
                else if(g_android_budget_policy.HasConfirmedOverride() &&
                    !g_android_budget_policy.HasUnconfirmedAttempt() &&
                    g_android_budget_io.epoch==s.external_request_epoch) {
                    lease.own=static_cast<float>(static_cast<double>(g_android_budget_policy.OwnedBudgetBytes())/AndroidTextureBudgetPolicy::MiB);
                    lease.previous_own=0;
                }
            }
        }
        bool committed=false;
        if(d.action!=AndroidTextureBudgetPolicy::Action::None) {
            std::lock_guard execution(g_android_budget_execution_mutex);
            float check=0;uint64_t check_bytes=0;
            const bool check_valid=InvokeValue(Contract("android.streaming_budget"),nullptr,nullptr,check) && AndroidBudgetBytes(check,check_bytes);
            io=AndroidBudgetIoSnapshot();
            const int32_t tier=g_android_quality_getter();
            if(tier<0 || !g_android_budget_policy.CanApply(d,check_valid,check_bytes,io.epoch) ||
                (io.lease.valid && io.lease.tier!=tier)) {
                g_android_budget_policy.Cancel(d);
            } else {
                float value=static_cast<float>(static_cast<double>(d.value_bytes)/AndroidTextureBudgetPolicy::MiB);
                {
                    std::lock_guard lock(g_android_budget_io_mutex);
                    if(d.action==AndroidTextureBudgetPolicy::Action::SetBudget)
                        g_android_budget_io.lease={true,tier,
                            static_cast<float>(static_cast<double>(d.base_bytes)/AndroidTextureBudgetPolicy::MiB),value,d.request_epoch,
                            io.lease.valid?check:0,false};
                }
                bool written=false;
                {
                    AndroidBudgetOwnWrite own;void* args[]{&value};
                    written=InvokeVoid(Contract("android.streaming_budget_set"),nullptr,args);
                }
                uint64_t readback_bytes=0;float readback=0;
                const bool readable=InvokeValue(Contract("android.streaming_budget"),nullptr,nullptr,readback) && AndroidBudgetBytes(readback,readback_bytes);
                const int32_t readback_tier=g_android_quality_getter();
                if(readback_tier!=tier) AndroidRecordBudgetRequest(readback,readable,readback_tier,false);
                const auto after=AndroidBudgetIoSnapshot();
                committed=g_android_budget_policy.Commit(d,written,readable,readback_bytes,after.epoch);
                if(readable && g_android_quality_getter()==tier &&
                    g_android_budget_policy.HasConfirmedOverride() && !g_android_budget_policy.HasUnconfirmedAttempt()) {
                    std::lock_guard lock(g_android_budget_io_mutex);
                    if(g_android_budget_io.epoch==after.epoch && g_android_budget_io.lease.valid) {
                        g_android_budget_io.lease.own=static_cast<float>(static_cast<double>(g_android_budget_policy.OwnedBudgetBytes())/AndroidTextureBudgetPolicy::MiB);
                        g_android_budget_io.lease.previous_own=0;
                    }
                }
                if(readable && g_android_quality_getter()==tier &&
                    !g_android_budget_policy.HasOwnedOverride()) {
                    std::lock_guard lock(g_android_budget_io_mutex);
                    if(g_android_budget_io.epoch==after.epoch) g_android_budget_io.lease={};
                }
            }
        }
        if(d.action!=AndroidTextureBudgetPolicy::Action::None || now>=g_android_next_budget_report_ms) {
            Log("Android texture budget event="+std::string(AndroidTextureBudgetPolicy::ReasonText(d.reason))+
                " baseMiB="+std::to_string(static_cast<double>(d.base_bytes)/AndroidTextureBudgetPolicy::MiB)+
                " observedMiB="+std::to_string(budget)+
                " desiredMiB="+std::to_string(static_cast<double>(s.desired_bytes)/AndroidTextureBudgetPolicy::MiB)+
                " capMiB="+std::to_string(static_cast<double>(d.cap_bytes)/AndroidTextureBudgetPolicy::MiB)+
                " requestedMiB="+std::to_string(static_cast<double>(d.value_bytes)/AndroidTextureBudgetPolicy::MiB)+
                " availableMiB="+std::to_string(static_cast<double>(s.available_bytes)/AndroidTextureBudgetPolicy::MiB)+
                " externalEpoch="+std::to_string(s.external_request_epoch)+" commit="+(committed?"1":"0"));
            g_android_next_budget_report_ms=now+15000;
        }
        if(restoring && !g_android_budget_policy.HasOwnedOverride() && AndroidBudgetHasLease())
            AndroidRestoreTierLease();
        return !restoring || (!g_android_budget_policy.HasOwnedOverride() && !AndroidBudgetHasLease());
    } catch(...) {return false;}
}
