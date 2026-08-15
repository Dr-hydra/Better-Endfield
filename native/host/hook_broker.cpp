#include "hook_broker.h"

#include "logging.h"

#include <MinHook.h>

#include <algorithm>

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

    std::vector<void*> targets;
    for (const auto& [target, record] : hooks_) {
        if (record.module_id == module_id) {
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

void HookBroker::Shutdown() {
    std::lock_guard lock(mutex_);
    if (!initialized_) {
        return;
    }

    for (const auto& [target, ignored] : hooks_) {
        (void)ignored;
        MH_DisableHook(target);
        MH_RemoveHook(target);
    }
    hooks_.clear();
    MH_Uninitialize();
    initialized_ = false;
}

} // namespace BetterEndfield::Host
