#pragma once

#include "BetterEndfield/ModuleApi.h"
#include "BetterEndfield/HookChain.h"

#include <mutex>
#include <memory>
#include <string>
#include <unordered_map>
#include <vector>

namespace BetterEndfield::Hooks {class Chain;}
namespace BetterEndfield::Host {

class Logger;

class HookBroker {
public:
    explicit HookBroker(Logger& logger);
    ~HookBroker();

    bool Initialize();
    BE_Result Create(const std::string& module_id, void* target, void* detour,
        void** original);
    BE_Result ReleaseModule(const std::string& module_id);
    // Disable entry points while retaining trampolines for callbacks already
    // dispatched into a process-pinned module. Retired targets cannot be reused.
    BE_Result RetireModule(const std::string& module_id);
    const BE_HookChainApiV1* ChainApi();
    void Shutdown();

private:
    struct HookRecord {
        std::string module_id;
        void* target = nullptr;
        bool retired = false;
    };

    Logger& logger_;
    std::mutex mutex_;
    bool initialized_ = false;
    std::unordered_map<void*, HookRecord> hooks_;
    std::unique_ptr<BetterEndfield::Hooks::Chain> chains_;
    BE_HookChainApiV1 chain_api_{};
    static BE_Result BE_CALL CreateChain(void*,const char*,void*,void*,void**,uint64_t*);
    static BE_Result BE_CALL DisableChain(void*,uint64_t);
    static BE_Result BE_CALL DisableModuleChain(void*,const char*);
};

} // namespace BetterEndfield::Host
