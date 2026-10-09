#pragma once

#include "BetterEndfieldNext/ModuleApi.h"
#include "BetterEndfieldNext/HookChain.h"
#include "hook_diagnostics.h"

#include <atomic>
#include <chrono>
#include <mutex>
#include <memory>
#include <optional>
#include <string>
#include <unordered_map>
#include <vector>

namespace BetterEndfieldNext::Hooks {class Chain;}
namespace BetterEndfieldNext::Host {

class Logger;

class HookBroker {
public:
    explicit HookBroker(Logger& logger);
    ~HookBroker();

    bool Initialize();
    // Built-in create_hook. Several modules may hook one target: they share the
    // target's chain with third-party modules and run in registration order
    // (first registered is entered first). original is this module's next link.
    // A module hooking a target it already actively hooks gets BE_Result_Conflict.
    BE_Result Create(const std::string& module_id, void* target, void* detour,
        void** original);
    // Turns the module's chain nodes into pass-throughs. Other owners on the
    // same targets keep running; relays and trampolines are never freed.
    BE_Result ReleaseModule(const std::string& module_id);
    // Same as ReleaseModule (chain nodes are never freed); kept for the
    // BetterEndfieldNext_RetireModuleHooksV1 lifecycle of process-pinned modules.
    BE_Result RetireModule(const std::string& module_id);
    const BE_HookChainApiV1* ChainApi();
    void Shutdown();

    // Diagnostics only; hook behaviour does not depend on them.
    // Records the managed method a resolved native entry belongs to.
    void DescribeEntry(void* entry, const std::string& label);
    // Whether a patched target has been entered; nullopt when untracked.
    std::optional<bool> Called(void* target);
    // Logs first calls and hooks still idle at the report checkpoints.
    void Poll();

private:
    struct Probe {
        std::string owners; // Active chain owners in call order, "a -> b".
        std::atomic<uint8_t>* hit = nullptr;
        std::chrono::steady_clock::time_point installed;
        HookDiagnostics::NativeShape shape;
        size_t checkpoint = 0;
        bool reported = false;
        bool retired = false;
    };

    // MH_CreateHook through a tracked relay; returns the MH_STATUS.
    int Patch(void* target, void* detour, void** original);
    // Mutex held. Adds a node to target's chain; built_in keeps create_hook's
    // one-hook-per-module-and-target rule, the chain ABI reuses an active node.
    BE_Result Link(const char* module, void* target, void* detour, void** next,
        uint64_t* handle, bool built_in);
    // Mutex held. Rebuilds a probe's owner list from the chain; returns the old one.
    std::string RefreshOwners(void* target);
    void RefreshAllOwners(const char* reason);
    void LogInstalled(void* target);
    std::string Label(void* entry) const;
    void WarnSharedEntry(void* entry);

    Logger& logger_;
    std::mutex mutex_;
    bool initialized_ = false;
    std::unordered_map<void*, Probe> probes_;
    std::unordered_map<void*, std::vector<std::string>> labels_;
    std::unordered_map<uintptr_t, uint32_t> call_sites_; // Poll thread only.
    std::unique_ptr<BetterEndfieldNext::Hooks::Chain> chains_;
    BE_HookChainApiV1 chain_api_{};
    static BE_Result BE_CALL CreateChain(void*,const char*,void*,void*,void**,uint64_t*);
    static BE_Result BE_CALL DisableChain(void*,uint64_t);
    static BE_Result BE_CALL DisableModuleChain(void*,const char*);
};

} // namespace BetterEndfieldNext::Host
