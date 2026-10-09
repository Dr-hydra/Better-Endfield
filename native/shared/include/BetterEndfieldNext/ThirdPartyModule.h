#pragma once
#include "ModuleApi.h"
#include "HookChain.h"

#define BETTER_ENDFIELD_NEXT_THIRD_PARTY_ABI_V1 1u
#define BETTER_ENDFIELD_NEXT_THIRD_PARTY_ENTRY_V1 "BetterEndfieldNext_GetThirdPartyModuleV1"

// UTF-8 strings are borrowed for the callback only. Host configuration/message
// bodies are opaque JSON. Copy strings needed after return. Callbacks execute
// on a Host worker, not a Unity/game thread. Loaded libraries remain resident.
typedef struct BE_ThirdPartyHostV1 {
    uint32_t struct_size;
    uint32_t version;
    void* context;
    const char* module_id;
    const char* platform;
    const char* package_root;
    const BE_HostApiV1* runtime; // Optional helpers available at initialization.
    const BE_HookChainApiV1* hooks; // Optional shared chain, otherwise null.
    void (BE_CALL* log)(void* context,const char* message);
    BE_Result (BE_CALL* reply)(void* context,const char* request_id,BE_Result result,const char* json);
    BE_Result (BE_CALL* emit)(void* context,const char* json);
    // Optional helpers may become ready after module initialization. A null
    // result means not ready; modules remain free to maintain their own tables.
    const BE_HostApiV1* (BE_CALL* get_runtime)(void* context);
} BE_ThirdPartyHostV1;

typedef struct BE_ThirdPartyModuleV1 {
    uint32_t struct_size;
    uint32_t version;
    const char* id;
    BE_Result (BE_CALL* initialize)(const BE_ThirdPartyHostV1* host,const char* configuration_json);
    BE_Result (BE_CALL* configuration_changed)(const char* configuration_json);
    BE_Result (BE_CALL* on_message)(const char* request_id,const char* body_json);
    void (BE_CALL* shutdown)(void);
} BE_ThirdPartyModuleV1;

typedef const BE_ThirdPartyModuleV1* (BE_CALL* BE_GetThirdPartyModuleV1Fn)(void);
