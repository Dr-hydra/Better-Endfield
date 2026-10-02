#include "core/hook_broker.h"
#include "core/log.h"
#include <dobby.h>
#include <map>
#include <memory>
#include <mutex>
#include <vector>
#include "../../../../../../native/shared/hooks/hook_chain.h"
namespace betterendfield {
namespace {
struct Record { HookBroker* owner; void* target; };
struct Registry {
    std::mutex mutex;
    std::map<void*, std::unique_ptr<Record>> targets;
    // Retired handles are never reused: a stale caller must not remove a new hook.
    std::vector<std::unique_ptr<Record>> retired;
    std::unique_ptr<BetterEndfield::Hooks::Chain> chains;
};
// Hooks are process-lifetime resources. Avoid global destructor order races.
Registry& Hooks() { static auto* registry = new Registry; return *registry; }
}
bool HookBroker::Initialize(std::string& error) { error.clear();ChainApi();return true; }
bool HookBroker::Install(void* target, void* replacement, void** original,
        void*& stub, std::string& error) {
    if (!target || !replacement || !original || stub) {
        error = "invalid native hook request or occupied handle";
        return false;
    }
    auto& registry = Hooks();
    std::lock_guard lock(registry.mutex);
    if ((registry.chains&&registry.chains->Contains(target))||registry.targets.contains(target)) {
        error = "native hook target already owned; refusing to overwrite another hook";
        return false;
    }
    // Allocate bookkeeping before the code patch; exceptions cannot leave an
    // installed hook unowned.
    auto record = std::make_unique<Record>(Record{this, target});
    auto [position, inserted] = registry.targets.emplace(target, std::move(record));
    (void)inserted;
    const int result = DobbyHook(target, replacement, original);
    if (result != 0) {
        registry.targets.erase(position);
        error = "Dobby native hook failed: " + std::to_string(result);
        return false;
    }
    stub = position->second.get();
    error.clear();
    return true;
}
bool HookBroker::Remove(void*& stub) {
    if (!stub) return true;
    auto& registry = Hooks();
    std::lock_guard lock(registry.mutex);
    // Search by handle without dereferencing a caller-supplied/stale pointer.
    for (auto position = registry.targets.begin(); position != registry.targets.end(); ++position) {
        if (position->second.get() != stub) continue;
        if (position->second->owner != this) return false;
        registry.retired.reserve(registry.retired.size() + 1);
        if (DobbyDestroy(position->first) != 0) {
            LogError("host.hooks", "native hook removal failed; ownership retained");
            return false;
        }
        registry.retired.push_back(std::move(position->second));
        registry.targets.erase(position);
        stub = nullptr;
        return true;
    }
    return false;
}
const BE_HookChainApiV1* HookBroker::ChainApi() {
    auto& registry=Hooks();std::lock_guard lock(registry.mutex);
    if(!registry.chains)registry.chains=std::make_unique<BetterEndfield::Hooks::Chain>(BetterEndfield::Hooks::Backend{
        [](void* target,void* entry,void** original){return DobbyPrepare(target,entry,original)==0;},
        [](void* target){return DobbyCommit(target)==0;},
        [](void* target){DobbyDestroy(target);},
        [](void*){/* Process-pinned Dobby trampoline: relay forwards to original. */}});
    chain_api_={sizeof(BE_HookChainApiV1),BETTER_ENDFIELD_HOOK_CHAIN_ABI_V1,this,
        &CreateChain,&DisableChain,&DisableModuleChain};
    return &chain_api_;
}
BE_Result BE_CALL HookBroker::CreateChain(void* context,const char* module,void* target,
        void* detour,void** next,uint64_t* handle) {
    if(!context)return BE_Result_InvalidArgument;
    auto& registry=Hooks();std::lock_guard lock(registry.mutex);
    if(!registry.chains)return BE_Result_NotReady;
    if(registry.targets.contains(target))return BE_Result_Conflict;
    return registry.chains->Create(module,target,detour,next,handle);
}
BE_Result BE_CALL HookBroker::DisableChain(void* context,uint64_t handle) {
    if(!context)return BE_Result_InvalidArgument;
    auto& registry=Hooks();std::lock_guard lock(registry.mutex);
    return registry.chains?registry.chains->Disable(handle):BE_Result_NotReady;
}
BE_Result BE_CALL HookBroker::DisableModuleChain(void* context,const char* module) {
    if(!context)return BE_Result_InvalidArgument;
    auto& registry=Hooks();std::lock_guard lock(registry.mutex);
    return registry.chains?registry.chains->DisableModule(module):BE_Result_NotReady;
}
}
