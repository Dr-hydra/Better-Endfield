#pragma once

#include <string>
#include "BetterEndfield/HookChain.h"

namespace betterendfield {

class HookBroker final {
public:
    bool Initialize(std::string& error);
    bool Install(
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
