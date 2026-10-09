#pragma once
#include "ModuleApi.h"

#define BETTER_ENDFIELD_NEXT_HOOK_CHAIN_ABI_V1 1u

// Optional Host facility; BE_HostApiV1 is unchanged. Built-in create_hook
// registrations and this API share one chain per target, so neither conflicts
// with the other. Nodes run in first-registration order. Detours and next must
// use the target's platform ABI.
// Next is a stable forwarding entry, not a promise to bypass other modules.
typedef struct BE_HookChainApiV1 {
    uint32_t struct_size;
    uint32_t version;
    void* context;
    BE_Result (BE_CALL* create)(void* context,const char* module_id,void* target,
        void* detour,void** next,uint64_t* handle);
    BE_Result (BE_CALL* disable)(void* context,uint64_t handle);
    BE_Result (BE_CALL* disable_module)(void* context,const char* module_id);
} BE_HookChainApiV1;
