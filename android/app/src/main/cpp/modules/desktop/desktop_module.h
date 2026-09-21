#pragma once

#include "BetterEndfield/ModuleApi.h"
#include "core/hook_broker.h"
#include "modules/module.h"

#include <string>
#include <vector>

namespace betterendfield {

// Runs a desktop module translation unit unchanged on Android.
//
// The desktop modules only ever reach the client through BE_HostApiV1, so the
// port is an adapter, not a rewrite: this class answers every host callback with
// the Android Il2CppRuntime and HookBroker. The login-model module proved the
// approach; this is the same contract shared by every later module, so the three
// feature modules do not each grow their own copy.
//
// Method and field resolution stays exact (assembly, namespace, class, name,
// parameter types, return type, parameter count) and no game offsets are stored.
class DesktopModule final : public Module {
public:
    using ApiGetter = const BE_ModuleApiV1* (*)();

    // `configuration_variable` is the environment variable the Java settings
    // page publishes. An empty or absent value keeps the module out of the
    // process entirely, matching how every other Android module behaves.
    DesktopModule(
        const char* id,
        const char* configuration_variable,
        ApiGetter api_getter,
        std::string active_message);
    ~DesktopModule() override;

    const char* Id() const override;
    ModuleResult Start(Il2CppRuntime& runtime) override;

private:
    struct HookRecord {
        std::string module_id;
        void* stub = nullptr;
    };

    static DesktopModule* Self(void* context);
    static void LogCallback(void* context, const char* module_id, const char* message);
    static BE_Result ResolveMethodCallback(
        void* context,
        const BE_MethodDescriptorV1* descriptor,
        BE_ResolvedMethodV1* result);
    static BE_Result ResolveFieldCallback(
        void* context,
        const BE_FieldDescriptorV1* descriptor,
        BE_ResolvedFieldV1* result);
    static BE_Result CreateHookCallback(
        void* context,
        const char* module_id,
        void* target,
        void* detour,
        void** original);
    static BE_Result ReleaseHooksCallback(void* context, const char* module_id);
    static int CopyConfigurationCallback(
        void* context, const char* module_id, char* destination, size_t destination_size);
    static int CopyCatalogRootCallback(
        void* context, char* destination, size_t destination_size);
    static int CopyManagedStringCallback(
        void* context,
        const void* managed_string,
        char* destination,
        size_t destination_size);
    static BE_Result ResolveClassCallback(
        void* context,
        const char* assembly_name,
        const char* namespace_name,
        const char* class_name,
        BE_ResolvedClassV1* result);
    static void* ObjectNewCallback(void* context, const void* class_info);
    static void* StringNewCallback(void* context, const char* utf8);
    static void* RuntimeInvokeCallback(
        void* context,
        const void* method_info,
        void* instance,
        void** parameters,
        void** exception);
    static void* ObjectUnboxCallback(void* context, void* boxed_value);
    static uint32_t GcHandleNewCallback(void* context, void* object, int pinned);
    static void GcHandleFreeCallback(void* context, uint32_t handle);
    static void* FieldGetValueObjectCallback(
        void* context, const void* field_info, void* instance);

    const char* const id_;
    const char* const configuration_variable_;
    const ApiGetter api_getter_;
    const std::string active_message_;

    BE_HostApiV1 host_{};
    Il2CppRuntime* runtime_ = nullptr;
    HookBroker hook_broker_;
    std::vector<HookRecord> hooks_;
    std::string configuration_;
    const BE_ModuleApiV1* api_ = nullptr;
    bool initialized_ = false;
};

}  // namespace betterendfield
