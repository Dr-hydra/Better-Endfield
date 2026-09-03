#pragma once

#include <stddef.h>
#include <stdint.h>

#if defined(_WIN32)
#define BE_CALL __cdecl
#define BE_EXPORT extern "C" __declspec(dllexport)
#else
#define BE_CALL
#define BE_EXPORT extern "C"
#endif

#define BETTER_ENDFIELD_MODULE_ABI_V1 1u

typedef enum BE_Result {
    BE_Result_Ok = 0,
    BE_Result_InvalidArgument = 1,
    BE_Result_NotReady = 2,
    BE_Result_NotFound = 3,
    BE_Result_ContractMismatch = 4,
    BE_Result_Conflict = 5,
    BE_Result_Failed = 6
} BE_Result;

typedef struct BE_MethodDescriptorV1 {
    const char* assembly_name;
    const char* namespace_name;
    const char* class_name;
    const char* method_name;
    const char* parameter_types;
    const char* return_type;
    uint32_t parameter_count;
} BE_MethodDescriptorV1;

typedef struct BE_ResolvedMethodV1 {
    const void* method_info;
    void* method_pointer;
} BE_ResolvedMethodV1;

typedef struct BE_FieldDescriptorV1 {
    const char* assembly_name;
    const char* namespace_name;
    const char* class_name;
    const char* field_name;
    const char* field_type;
} BE_FieldDescriptorV1;

typedef struct BE_ResolvedFieldV1 {
    const void* field_info;
    int32_t offset;
} BE_ResolvedFieldV1;

// Resolves a managed class and its System.Type object without exposing a
// client-specific address or layout to modules.
typedef struct BE_ResolvedClassV1 {
    const void* class_info;
    const void* type_info;
    void* type_object;
} BE_ResolvedClassV1;

typedef struct BE_ModuleDescriptorV1 {
    const char* module_id;
    const char* display_name;
    const char* module_version;
    uint32_t abi_version;
} BE_ModuleDescriptorV1;

typedef struct BE_HostApiV1 {
    uint32_t abi_version;
    void* context;

    void(BE_CALL* log)(void* context, const char* module_id, const char* message);
    BE_Result(BE_CALL* resolve_method)(
        void* context,
        const BE_MethodDescriptorV1* descriptor,
        BE_ResolvedMethodV1* result);
    BE_Result(BE_CALL* resolve_field)(
        void* context,
        const BE_FieldDescriptorV1* descriptor,
        BE_ResolvedFieldV1* result);
    BE_Result(BE_CALL* create_hook)(
        void* context,
        const char* module_id,
        void* target,
        void* detour,
        void** original);
    BE_Result(BE_CALL* release_module_hooks)(void* context, const char* module_id);
    int(BE_CALL* copy_module_configuration)(
        void* context,
        const char* module_id,
        char* destination,
        size_t destination_size);
    int(BE_CALL* copy_catalog_root)(
        void* context,
        char* destination,
        size_t destination_size);
    int(BE_CALL* copy_managed_string)(
        void* context,
        const void* managed_string,
        char* destination,
        size_t destination_size);
    BE_Result(BE_CALL* resolve_class)(
        void* context,
        const char* assembly_name,
        const char* namespace_name,
        const char* class_name,
        BE_ResolvedClassV1* result);
    void*(BE_CALL* object_new)(void* context, const void* class_info);
    void*(BE_CALL* string_new)(void* context, const char* utf8);
    void*(BE_CALL* runtime_invoke)(
        void* context,
        const void* method_info,
        void* instance,
        void** parameters,
        void** exception);
    void*(BE_CALL* object_unbox)(void* context, void* boxed_value);
    uint32_t(BE_CALL* gchandle_new)(void* context, void* object, int pinned);
    void(BE_CALL* gchandle_free)(void* context, uint32_t handle);
    void*(BE_CALL* field_get_value_object)(
        void* context,
        const void* field_info,
        void* instance);
} BE_HostApiV1;

typedef BE_Result(BE_CALL* BE_ModuleInitializeFn)(const BE_HostApiV1* host);
typedef BE_Result(BE_CALL* BE_ModuleConfigChangedFn)(const char* configuration);
typedef void(BE_CALL* BE_ModuleShutdownFn)(void);

typedef struct BE_ModuleApiV1 {
    BE_ModuleDescriptorV1 descriptor;
    BE_ModuleInitializeFn initialize;
    BE_ModuleConfigChangedFn configuration_changed;
    BE_ModuleShutdownFn shutdown;
} BE_ModuleApiV1;

typedef const BE_ModuleApiV1*(BE_CALL* BE_GetModuleApiV1Fn)(void);
