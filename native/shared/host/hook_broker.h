#pragma once

#include "BetterEndfield/ModuleApi.h"

#include <mutex>
#include <string>
#include <unordered_map>
#include <vector>

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
};

} // namespace BetterEndfield::Host
