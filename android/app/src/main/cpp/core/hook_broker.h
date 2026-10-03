#pragma once

#include <string>
#include "BetterEndfield/HookChain.h"

namespace betterendfield {

// Every native hook, built-in or third-party, is a node of the shared
// per-target chain (native/shared/hooks/hook_chain.h), matching the Windows
// Host. Several modules may hook one target; they run in registration order
// (first registered is entered first) and original is the caller's next link.
// Remove() turns a node into a pass-through; relays and the Dobby trampoline
// stay alive for calls already inside a detour.
class HookBroker final {
public:
    bool Initialize(std::string& error);
    // Owner is this broker instance (callers without a module id).
    bool Install(
        void* target,
        void* replacement,
        void** original,
        void*& stub,
        std::string& error);
    // Owner is module. One active hook per owner and target: a second one is
    // rejected, as the exclusive broker did; other owners are chained.
    bool Install(
        const char* module,
        void* target,
        void* replacement,
        void** original,
        void*& stub,
        std::string& error);
    bool Remove(void*& stub);
    const BE_HookChainApiV1* ChainApi();
private:
    BE_HookChainApiV1 chain_api_{};
    static BE_Result BE_CALL CreateChain(void*,const char*,void*,void*,void**,uint64_t*);
    static BE_Result BE_CALL DisableChain(void*,uint64_t);
    static BE_Result BE_CALL DisableModuleChain(void*,const char*);
};

}  // namespace betterendfield
