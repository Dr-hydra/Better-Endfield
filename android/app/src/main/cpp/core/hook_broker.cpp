#include "core/hook_broker.h"
#include "core/log.h"
#include <dobby.h>
#include <cstdio>
#include <memory>
#include <mutex>
#include <vector>
#include "../../../../../../native/shared/hooks/hook_chain.h"
namespace betterendfield {
namespace {
struct Record { HookBroker* owner; void* target; uint64_t handle; };
struct Registry {
    std::mutex mutex;
    // Live stubs handed to built-in callers; a stub is a Record address.
    std::vector<std::unique_ptr<Record>> live;
    // Removed stubs are never reused: a stale caller must not remove a new hook.
    std::vector<std::unique_ptr<Record>> retired;
    std::unique_ptr<BetterEndfield::Hooks::Chain> chains;
};
// Hooks are process-lifetime resources. Avoid global destructor order races.
Registry& Hooks() { static auto* registry = new Registry; return *registry; }
// Registry mutex held.
BetterEndfield::Hooks::Chain& Chains(Registry& registry) {
    if (!registry.chains) registry.chains = std::make_unique<BetterEndfield::Hooks::Chain>(BetterEndfield::Hooks::Backend{
        [](void* target, void* entry, void** original) { return DobbyPrepare(target, entry, original) == 0; },
        [](void* target) { return DobbyCommit(target) == 0; },
        [](void* target) { DobbyDestroy(target); },
        [](void*) { /* Process-pinned Dobby trampoline: relay forwards to original. */ }});
    return *registry.chains;
}
// Registry mutex held. Logs the call order of target's active owners.
void LogOwners(Registry& registry, void* target, const std::string& event) {
    std::string owners;
    for (const auto& module : Chains(registry).Modules(target))
        owners += (owners.empty() ? "" : " -> ") + module;
    char address[32]{};
    std::snprintf(address, sizeof(address), "%p", target);
    const std::string text = std::string("native hook chain at ") + address + " after " + event + ": " +
        (owners.empty() ? std::string("all owners released, pass-through to original")
                        : owners + " -> original");
    LogInfo("host.hooks", text.c_str());
}
std::string BrokerOwner(const HookBroker* broker) {
    char text[48]{};
    std::snprintf(text, sizeof(text), "native-broker@%p", static_cast<const void*>(broker));
    return text;
}
}
bool HookBroker::Initialize(std::string& error) { error.clear();ChainApi();return true; }
bool HookBroker::Install(void* target, void* replacement, void** original,
        void*& stub, std::string& error) {
    const std::string owner = BrokerOwner(this);
    return Install(owner.c_str(), target, replacement, original, stub, error);
}
bool HookBroker::Install(const char* module, void* target, void* replacement, void** original,
        void*& stub, std::string& error) {
    if (!module || !*module || !target || !replacement || !original || stub) {
        error = "invalid native hook request or occupied handle";
        return false;
    }
    auto& registry = Hooks();
    std::lock_guard lock(registry.mutex);
    auto& chains = Chains(registry);
    // Allocate bookkeeping before the code patch; exceptions cannot leave an
    // installed hook unowned.
    auto record = std::make_unique<Record>(Record{this, target, 0});
    registry.live.reserve(registry.live.size() + 1);
    const bool first = !chains.Contains(target);
    const BE_Result result = chains.Create(module, target, replacement, original, &record->handle,
        BetterEndfield::Hooks::Chain::Duplicate::Reject);
    if (result == BE_Result_Conflict) {
        error = std::string("native hook target already hooked by ") + module +
            "; a module may hook an entry once";
        return false;
    }
    if (result != BE_Result_Ok) {
        error = "native hook chain installation failed: " + std::to_string(static_cast<int>(result));
        return false;
    }
    stub = record.get();
    registry.live.push_back(std::move(record)); // Capacity reserved above.
    if (!first) LogOwners(registry, target, std::string("adding ") + module);
    error.clear();
    return true;
}
bool HookBroker::Remove(void*& stub) {
    if (!stub) return true;
    auto& registry = Hooks();
    std::lock_guard lock(registry.mutex);
    // Search by handle without dereferencing a caller-supplied/stale pointer.
    for (auto position = registry.live.begin(); position != registry.live.end(); ++position) {
        if (position->get() != stub) continue;
        if ((*position)->owner != this) return false;
        registry.retired.reserve(registry.retired.size() + 1);
        void* target = (*position)->target;
        // The node becomes a pass-through; other owners on target keep running.
        // NotFound: the handle was already disabled through a reactivation.
        const BE_Result result = Chains(registry).Disable((*position)->handle);
        if (result != BE_Result_Ok && result != BE_Result_NotFound) {
            LogError("host.hooks", "native hook removal failed; ownership retained");
            return false;
        }
        registry.retired.push_back(std::move(*position));
        registry.live.erase(position);
        stub = nullptr;
        if (!Chains(registry).Modules(target).empty()) LogOwners(registry, target, "removal");
        return true;
    }
    return false;
}
const BE_HookChainApiV1* HookBroker::ChainApi() {
    auto& registry=Hooks();std::lock_guard lock(registry.mutex);
    Chains(registry);
    chain_api_={sizeof(BE_HookChainApiV1),BETTER_ENDFIELD_HOOK_CHAIN_ABI_V1,this,
        &CreateChain,&DisableChain,&DisableModuleChain};
    return &chain_api_;
}
BE_Result BE_CALL HookBroker::CreateChain(void* context,const char* module,void* target,
        void* detour,void** next,uint64_t* handle) {
    if(!context)return BE_Result_InvalidArgument;
    auto& registry=Hooks();std::lock_guard lock(registry.mutex);
    auto& chains=Chains(registry);
    const bool first=target&&!chains.Contains(target);
    const BE_Result result=chains.Create(module,target,detour,next,handle);
    if(result==BE_Result_Ok&&!first)LogOwners(registry,target,std::string("adding ")+module);
    return result;
}
BE_Result BE_CALL HookBroker::DisableChain(void* context,uint64_t handle) {
    if(!context)return BE_Result_InvalidArgument;
    auto& registry=Hooks();std::lock_guard lock(registry.mutex);
    return Chains(registry).Disable(handle);
}
BE_Result BE_CALL HookBroker::DisableModuleChain(void* context,const char* module) {
    if(!context)return BE_Result_InvalidArgument;
    auto& registry=Hooks();std::lock_guard lock(registry.mutex);
    return Chains(registry).DisableModule(module);
}
}
