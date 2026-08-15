#pragma once

#include "BetterEndfield/ModuleApi.h"

#include <Windows.h>

#include <memory>
#include <atomic>
#include <string>

namespace BetterEndfield::Host {

class DynamicResolver;
class HookBroker;
class Logger;
class ModuleManager;
class SettingsStore;

class HostRuntime {
public:
    HostRuntime(HMODULE host_module, const void* bootstrap_data);
    ~HostRuntime();

    void Run();
    void RequestStop();
    const BE_HostApiV1& Api() const;
    BE_Result ReleaseHooks(const std::string& module_id);
    bool HasAssembly(const std::string& assembly_name) const;

private:
    static void BE_CALL LogCallback(void* context, const char* module_id,
        const char* message);
    static BE_Result BE_CALL ResolveMethodCallback(void* context,
        const BE_MethodDescriptorV1* descriptor, BE_ResolvedMethodV1* result);
    static BE_Result BE_CALL ResolveFieldCallback(void* context,
        const BE_FieldDescriptorV1* descriptor, BE_ResolvedFieldV1* result);
    static BE_Result BE_CALL CreateHookCallback(void* context, const char* module_id,
        void* target, void* detour, void** original);
    static BE_Result BE_CALL ReleaseModuleHooksCallback(void* context,
        const char* module_id);
    static int BE_CALL CopyModuleConfigurationCallback(void* context,
        const char* module_id, char* destination, size_t destination_size);
    static int BE_CALL CopyCatalogRootCallback(void* context, char* destination,
        size_t destination_size);
    static int BE_CALL CopyManagedStringCallback(void* context,
        const void* managed_string, char* destination, size_t destination_size);
    static BE_Result BE_CALL ResolveClassCallback(void* context,
        const char* assembly_name, const char* namespace_name, const char* class_name,
        BE_ResolvedClassV1* result);
    static void* BE_CALL ObjectNewCallback(void* context, const void* class_info);
    static void* BE_CALL StringNewCallback(void* context, const char* utf8);
    static void* BE_CALL RuntimeInvokeCallback(void* context, const void* method_info,
        void* instance, void** parameters, void** exception);
    static void* BE_CALL ObjectUnboxCallback(void* context, void* boxed_value);
    static void* BE_CALL FieldGetValueObjectCallback(void* context,
        const void* field_info, void* instance);
    static uint32_t BE_CALL GCHandleNewCallback(void* context, void* object, int pinned);
    static void BE_CALL GCHandleFreeCallback(void* context, uint32_t handle);

    HMODULE host_module_ = nullptr;
    const void* bootstrap_data_ = nullptr;
    BE_HostApiV1 api_{};
    std::unique_ptr<Logger> logger_;
    std::unique_ptr<SettingsStore> settings_;
    std::unique_ptr<DynamicResolver> resolver_;
    std::unique_ptr<HookBroker> hooks_;
    std::unique_ptr<ModuleManager> modules_;
    std::atomic_bool stop_requested_{false};
};

} // namespace BetterEndfield::Host
