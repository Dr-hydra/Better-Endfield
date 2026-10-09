#include "hook_broker.h"

#include "logging.h"

#include <MinHook.h>

#include <algorithm>
#include <cstdio>
#include "../hooks/hook_chain.h"

namespace BetterEndfieldNext::Host {
namespace {

// Idle hooks are reported once per checkpoint after installation.
constexpr std::chrono::minutes kIdleCheckpoints[]{
    std::chrono::minutes(3), std::chrono::minutes(15), std::chrono::minutes(60)};

} // namespace

HookBroker::HookBroker(Logger& logger) : logger_(logger) {}

HookBroker::~HookBroker() {
    Shutdown();
}

bool HookBroker::Initialize() {
    std::lock_guard lock(mutex_);
    if (initialized_) {
        return true;
    }
    const MH_STATUS status = MH_Initialize();
    if (status != MH_OK) {
        logger_.Write("host.hooks", "MinHook initialization failed: " +
            std::to_string(static_cast<int>(status)));
        return false;
    }
    initialized_ = true;
    // Backend callbacks run under mutex_ (Link, Shutdown).
    chains_=std::make_unique<BetterEndfieldNext::Hooks::Chain>(BetterEndfieldNext::Hooks::Backend{
        [this](void* target,void* entry,void** original){
            const int status=Patch(target,entry,original);
            if(status!=MH_OK)logger_.Write("host.hooks","MinHook creation failed at "+
                HookDiagnostics::FormatAddress(target)+": "+std::to_string(status));
            return status==MH_OK;},
        [this](void* target){
            const MH_STATUS status=MH_EnableHook(target);
            if(status!=MH_OK)logger_.Write("host.hooks","MinHook enable failed at "+
                HookDiagnostics::FormatAddress(target)+": "+std::to_string(static_cast<int>(status)));
            return status==MH_OK;},
        [this](void* target){MH_RemoveHook(target);probes_.erase(target);},
        [this](void* target){
            MH_DisableHook(target);
            if(auto probe=probes_.find(target);probe!=probes_.end())probe->second.retired=true;
        }});
    chain_api_={sizeof(BE_HookChainApiV1),BETTER_ENDFIELD_NEXT_HOOK_CHAIN_ABI_V1,this,
        &CreateChain,&DisableChain,&DisableModuleChain};
    return true;
}

// Built-in create_hook and the third-party chain ABI share one Chain per
// target. A single owner is a one-node chain: original is that node's next
// relay, which forwards to the MinHook trampoline. Further owners are appended
// in registration order, so earlier owners run first and their original reaches
// the next owner. Releasing a module turns its nodes into pass-throughs; the
// patch, relays and trampoline stay alive because a callback already inside a
// detour may still call its original.
BE_Result HookBroker::Create(const std::string& module_id, void* target,
    void* detour, void** original) {
    if (module_id.empty() || !target || !detour || !original) {
        return BE_Result_InvalidArgument;
    }
    std::lock_guard lock(mutex_);
    if (!initialized_ || !chains_) {
        return BE_Result_NotReady;
    }
    uint64_t handle = 0;
    return Link(module_id.c_str(), target, detour, original, &handle, true);
}

BE_Result HookBroker::Link(const char* module, void* target, void* detour,
    void** next, uint64_t* handle, bool built_in) {
    const bool first = target && !chains_->Contains(target);
    const BE_Result result = chains_->Create(module, target, detour, next, handle,
        built_in ? BetterEndfieldNext::Hooks::Chain::Duplicate::Reject
                 : BetterEndfieldNext::Hooks::Chain::Duplicate::Reuse);
    const std::string owner = module ? module : "<unnamed>";
    if (result == BE_Result_Conflict) {
        logger_.Write("host.hooks", "Hook conflict: " + owner +
            " already has an active hook at " + Label(target) + " (" +
            HookDiagnostics::FormatAddress(target) + "); a module may hook an entry once.");
        return result;
    }
    if (result != BE_Result_Ok) {
        logger_.Write("host.hooks", "Hook creation failed for " + owner + " at " +
            Label(target) + ": " + std::to_string(static_cast<int>(result)));
        return result;
    }
    const auto known = probes_.find(target);
    const bool was_retired = known != probes_.end() && known->second.retired;
    const std::string previous = RefreshOwners(target);
    if (first) {
        LogInstalled(target);
    } else if (const auto probe = probes_.find(target); probe != probes_.end() &&
               (was_retired || probe->second.owners != previous)) {
        logger_.Write("host.hooks", "Hook chain at " + Label(target) + " (" +
            HookDiagnostics::FormatAddress(target) + ") now runs " +
            probe->second.owners + " -> original.");
    }
    return result;
}

std::string HookBroker::RefreshOwners(void* target) {
    const auto probe = probes_.find(target);
    if (probe == probes_.end()) {
        return {};
    }
    std::string previous = std::move(probe->second.owners);
    std::string owners;
    for (const auto& module : chains_->Modules(target)) {
        owners += (owners.empty() ? "" : " -> ") + module;
    }
    // Every node a pass-through: the target behaves like the original, so it
    // is no longer reported as an idle hook.
    probe->second.retired = owners.empty();
    probe->second.owners = owners.empty() ? previous : owners;
    return previous;
}

void HookBroker::RefreshAllOwners(const char* reason) {
    for (auto& [target, probe] : probes_) {
        const bool was_retired = probe.retired;
        const std::string previous = RefreshOwners(target);
        if (probe.retired == was_retired && probe.owners == previous) {
            continue;
        }
        logger_.Write("host.hooks", std::string("Hook chain at ") + Label(target) + " (" +
            HookDiagnostics::FormatAddress(target) + ") after " + reason + ": " +
            (probe.retired ? "all owners released, pass-through to original"
                           : probe.owners + " -> original") + ".");
    }
}

BE_Result HookBroker::ReleaseModule(const std::string& module_id) {
    std::lock_guard lock(mutex_);
    if (!initialized_ || !chains_) {
        return BE_Result_NotReady;
    }
    const BE_Result result = chains_->DisableModule(module_id.c_str());
    RefreshAllOwners(("release of " + module_id).c_str());
    return result;
}

BE_Result HookBroker::RetireModule(const std::string& module_id) {
    // Chain nodes are never freed, so retiring and releasing are the same
    // operation: the module's detours become pass-throughs, trampolines stay.
    std::lock_guard lock(mutex_);
    if (!initialized_ || !chains_) return BE_Result_NotReady;
    const BE_Result result = chains_->DisableModule(module_id.c_str());
    RefreshAllOwners(("retirement of " + module_id).c_str());
    return result;
}

void HookBroker::Shutdown() {
    std::lock_guard lock(mutex_);
    if (!initialized_) {
        return;
    }

    const bool has_targets = chains_ && chains_->HasTargets();
    if (chains_) chains_->Shutdown();
    probes_.clear();
    // A pass-through callback may still be inside a trampoline. Its bounded
    // executable storage lives until process exit once any target was patched.
    if (!has_targets) MH_Uninitialize();
    initialized_ = false;
}

const BE_HookChainApiV1* HookBroker::ChainApi() {return &chain_api_;}
BE_Result BE_CALL HookBroker::CreateChain(void* context,const char* module,void* target,
        void* detour,void** next,uint64_t* handle) {
    auto* self=static_cast<HookBroker*>(context);if(!self)return BE_Result_InvalidArgument;
    std::lock_guard lock(self->mutex_);
    if(!self->initialized_||!self->chains_)return BE_Result_NotReady;
    return self->Link(module,target,detour,next,handle,false);
}
BE_Result BE_CALL HookBroker::DisableChain(void* context,uint64_t handle) {
    auto* self=static_cast<HookBroker*>(context);if(!self)return BE_Result_InvalidArgument;
    std::lock_guard lock(self->mutex_);
    if(!self->chains_)return BE_Result_NotReady;
    const BE_Result result=self->chains_->Disable(handle);
    if(result==BE_Result_Ok)self->RefreshAllOwners("a chain handle was disabled");
    return result;
}
BE_Result BE_CALL HookBroker::DisableModuleChain(void* context,const char* module) {
    auto* self=static_cast<HookBroker*>(context);if(!self)return BE_Result_InvalidArgument;
    std::lock_guard lock(self->mutex_);
    if(!self->chains_)return BE_Result_NotReady;
    const BE_Result result=self->chains_->DisableModule(module);
    if(result==BE_Result_Ok)self->RefreshAllOwners((std::string("release of ")+module).c_str());
    return result;
}

int HookBroker::Patch(void* target, void* detour, void** original) {
    // Read the target before MinHook rewrites its first instructions.
    Probe probe;
    probe.shape = HookDiagnostics::Describe(target);
    const auto relay = BetterEndfieldNext::Hooks::RelayPool::Instance().MakeTracked();
    if (relay) {
        relay.Set(detour);
    }
    const MH_STATUS status = MH_CreateHook(target, relay ? relay.code : detour, original);
    if (status != MH_OK) {
        return status;
    }
    probe.hit = relay.hit;
    probe.installed = std::chrono::steady_clock::now();
    probes_[target] = std::move(probe);
    return MH_OK;
}

void HookBroker::LogInstalled(void* target) {
    const auto found = probes_.find(target);
    if (found == probes_.end()) {
        return;
    }
    const Probe& probe = found->second;
    std::string text = "Hook installed for " + probe.owners + ": " + Label(target) +
        " at " + HookDiagnostics::FormatAddress(target);
    text += probe.shape.leaf ? ", leaf"
                             : ", " + std::to_string(probe.shape.size) + " bytes";
    if (probe.shape.tail_target) {
        text += ", tail-jumps to " + Label(probe.shape.tail_target) + " at " +
            HookDiagnostics::FormatAddress(probe.shape.tail_target);
    }
    if (!probe.hit) {
        text += ", call tracking unavailable";
    }
    logger_.Write("host.hooks", text + ".");
    WarnSharedEntry(target);
}

std::string HookBroker::Label(void* entry) const {
    const auto found = labels_.find(entry);
    if (found == labels_.end() || found->second.empty()) {
        return "<unresolved native target>";
    }
    std::string text;
    for (const auto& label : found->second) {
        text += (text.empty() ? "" : " / ") + label;
    }
    return text;
}

void HookBroker::WarnSharedEntry(void* entry) {
    const auto found = labels_.find(entry);
    if (found == labels_.end() || found->second.size() < 2) {
        return;
    }
    logger_.Write("host.hooks", "Shared native entry at " +
        HookDiagnostics::FormatAddress(entry) + ": " + Label(entry) +
        ". A hook there runs for every listed method (identical code folding or "
        "generic sharing); its detour must check the MethodInfo or instance it receives.");
}

void HookBroker::DescribeEntry(void* entry, const std::string& label) {
    if (!entry || label.empty()) {
        return;
    }
    std::lock_guard lock(mutex_);
    auto& labels = labels_[entry];
    if (std::find(labels.begin(), labels.end(), label) != labels.end()) {
        return;
    }
    labels.push_back(label);
    if (labels.size() > 1 && probes_.contains(entry)) {
        WarnSharedEntry(entry);
    }
}

std::optional<bool> HookBroker::Called(void* target) {
    std::lock_guard lock(mutex_);
    const auto found = probes_.find(target);
    if (found == probes_.end() || !found->second.hit) {
        return std::nullopt;
    }
    return found->second.hit->load(std::memory_order_relaxed) != 0;
}

void HookBroker::Poll() {
    struct Idle {
        void* target = nullptr;
        std::string description;
        HMODULE module = nullptr;
        std::string tail;
        long long minutes = 0;
    };
    std::string first_calls;
    std::vector<Idle> idle;
    {
        std::lock_guard lock(mutex_);
        const auto now = std::chrono::steady_clock::now();
        for (auto& [target, probe] : probes_) {
            if (!probe.hit || probe.retired || probe.reported) {
                continue;
            }
            if (probe.hit->load(std::memory_order_relaxed) != 0) {
                probe.reported = true;
                char elapsed[32]{};
                std::snprintf(elapsed, sizeof(elapsed), " (%.1fs)",
                    std::chrono::duration<double>(now - probe.installed).count());
                first_calls += (first_calls.empty() ? "" : ", ") + Label(target) + elapsed;
                continue;
            }
            size_t next = probe.checkpoint;
            while (next < std::size(kIdleCheckpoints) &&
                   now - probe.installed >= kIdleCheckpoints[next]) {
                ++next;
            }
            if (next == probe.checkpoint) {
                continue;
            }
            probe.checkpoint = next;
            Idle item{target, Label(target) + " for " + probe.owners + " at " +
                HookDiagnostics::FormatAddress(target), probe.shape.module, {},
                kIdleCheckpoints[next - 1].count()};
            if (probe.shape.tail_target) {
                item.tail = Label(probe.shape.tail_target) + " at " +
                    HookDiagnostics::FormatAddress(probe.shape.tail_target);
            }
            idle.push_back(std::move(item));
        }
    }
    if (!first_calls.empty()) {
        logger_.Write("host.hooks", "First call observed: " + first_calls + ".");
    }
    if (idle.empty()) {
        return;
    }

    // Scanning a module's code takes a moment; do it outside the lock and once
    // per target (the image does not change while the process runs).
    std::unordered_map<HMODULE, std::vector<uintptr_t>> pending;
    for (const auto& item : idle) {
        const auto address = reinterpret_cast<uintptr_t>(item.target);
        if (item.module && !call_sites_.contains(address)) {
            pending[item.module].push_back(address);
        }
    }
    for (const auto& [module, targets] : pending) {
        for (const auto& [target, count] :
             HookDiagnostics::CountDirectReferences(module, targets)) {
            call_sites_[target] = count;
        }
    }
    bool suspicious = false;
    for (const auto& item : idle) {
        std::string text = "Hook not called within " + std::to_string(item.minutes) +
            " min: " + item.description;
        const auto sites = call_sites_.find(reinterpret_cast<uintptr_t>(item.target));
        if (sites != call_sites_.end()) {
            text += ", direct call sites: " + std::to_string(sites->second);
            suspicious = suspicious || sites->second == 0;
        }
        if (!item.tail.empty()) {
            text += ", tail-jumps to " + item.tail;
            suspicious = true;
        }
        logger_.Write("host.hooks", text + ".");
    }
    if (suspicious) {
        logger_.Write("host.hooks",
            "An idle hook is expected while its feature is unused. If the game already "
            "ran it, the callers may use an inlined copy of the method (0 direct call "
            "sites is typical; virtual and Unity message methods also show 0): hook the "
            "tail-jump target or a caller instead.");
    }
}

} // namespace BetterEndfieldNext::Host
