#include "hook_broker.h"

#include "logging.h"

#include <MinHook.h>

#include <algorithm>
#include "../hooks/hook_chain.h"

namespace BetterEndfield::Host {

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
    chains_=std::make_unique<BetterEndfield::Hooks::Chain>(BetterEndfield::Hooks::Backend{
        [](void* target,void* entry,void** original){return MH_CreateHook(target,entry,original)==MH_OK;},
        [](void* target){return MH_EnableHook(target)==MH_OK;},
        [](void* target){MH_RemoveHook(target);},
        [](void* target){MH_DisableHook(target);}});
    chain_api_={sizeof(BE_HookChainApiV1),BETTER_ENDFIELD_HOOK_CHAIN_ABI_V1,this,
        &CreateChain,&DisableChain,&DisableModuleChain};
    return true;
}

BE_Result HookBroker::Create(const std::string& module_id, void* target,
    void* detour, void** original) {
    if (module_id.empty() || !target || !detour || !original) {
        return BE_Result_InvalidArgument;
    }

    std::lock_guard lock(mutex_);
    if (!initialized_) {
        return BE_Result_NotReady;
    }
    if (chains_&&chains_->Contains(target))return BE_Result_Conflict;
    if (hooks_.contains(target)) {
        logger_.Write("host.hooks", "Hook conflict at a target already owned by " +
            hooks_.at(target).module_id + ".");
        return BE_Result_Conflict;
    }

    const MH_STATUS create_status = MH_CreateHook(target, detour, original);
    if (create_status != MH_OK) {
        logger_.Write("host.hooks", "Hook creation failed for " + module_id + ": " +
            std::to_string(static_cast<int>(create_status)));
        return BE_Result_Failed;
    }

    const MH_STATUS enable_status = MH_EnableHook(target);
    if (enable_status != MH_OK) {
        MH_RemoveHook(target);
        logger_.Write("host.hooks", "Hook enable failed for " + module_id + ": " +
            std::to_string(static_cast<int>(enable_status)));
        return BE_Result_Failed;
    }

    hooks_.emplace(target, HookRecord{module_id, target});
    return BE_Result_Ok;
}

BE_Result HookBroker::ReleaseModule(const std::string& module_id) {
    std::lock_guard lock(mutex_);
    if (!initialized_) {
        return BE_Result_NotReady;
    }

    if(chains_)chains_->DisableModule(module_id.c_str());

    std::vector<void*> targets;
    for (const auto& [target, record] : hooks_) {
        if (record.module_id == module_id && !record.retired) {
            targets.push_back(target);
        }
    }

    bool failed = false;
    for (void* target : targets) {
        const MH_STATUS disable_status = MH_DisableHook(target);
        const MH_STATUS remove_status = MH_RemoveHook(target);
        if ((disable_status != MH_OK && disable_status != MH_ERROR_DISABLED) ||
            remove_status != MH_OK) {
            failed = true;
            logger_.Write("host.hooks", "Unable to release a hook owned by " +
                module_id + ".");
            continue;
        }
        hooks_.erase(target);
    }
    return failed ? BE_Result_Failed : BE_Result_Ok;
}

BE_Result HookBroker::RetireModule(const std::string& module_id) {
    std::lock_guard lock(mutex_);
    if (!initialized_) return BE_Result_NotReady;
    if(chains_)chains_->DisableModule(module_id.c_str());
    bool failed = false;
    for (auto& [target, record] : hooks_) {
        if (record.module_id != module_id) continue;
        // Preserve the trampoline even if disabling reports an error. The
        // module's disabled detour remains a safe pass-through in that case.
        record.retired = true;
        const auto status = MH_DisableHook(target);
        if (status != MH_OK && status != MH_ERROR_DISABLED) failed = true;
    }
    return failed ? BE_Result_Failed : BE_Result_Ok;
}

void HookBroker::Shutdown() {
    std::lock_guard lock(mutex_);
    if (!initialized_) {
        return;
    }

    bool has_retired = chains_&&chains_->HasTargets();
    if(chains_)chains_->Shutdown();
    for (const auto& [target, record] : hooks_) {
        MH_DisableHook(target);
        if (record.retired) has_retired = true;
        else MH_RemoveHook(target);
    }
    hooks_.clear();
    // Retired trampolines may still be reached by a previously dispatched
    // pass-through callback. Their bounded executable storage lives until exit.
    if (!has_retired) MH_Uninitialize();
    initialized_ = false;
}

const BE_HookChainApiV1* HookBroker::ChainApi() {return &chain_api_;}
BE_Result BE_CALL HookBroker::CreateChain(void* context,const char* module,void* target,
        void* detour,void** next,uint64_t* handle) {
    auto* self=static_cast<HookBroker*>(context);if(!self)return BE_Result_InvalidArgument;
    std::lock_guard lock(self->mutex_);
    if(!self->initialized_||!self->chains_)return BE_Result_NotReady;
    if(self->hooks_.contains(target))return BE_Result_Conflict;
    return self->chains_->Create(module,target,detour,next,handle);
}
BE_Result BE_CALL HookBroker::DisableChain(void* context,uint64_t handle) {
    auto* self=static_cast<HookBroker*>(context);if(!self)return BE_Result_InvalidArgument;
    std::lock_guard lock(self->mutex_);
    return self->chains_?self->chains_->Disable(handle):BE_Result_NotReady;
}
BE_Result BE_CALL HookBroker::DisableModuleChain(void* context,const char* module) {
    auto* self=static_cast<HookBroker*>(context);if(!self)return BE_Result_InvalidArgument;
    std::lock_guard lock(self->mutex_);
    return self->chains_?self->chains_->DisableModule(module):BE_Result_NotReady;
}

} // namespace BetterEndfield::Host
