#include "host_runtime.h"

#include "dynamic_resolver.h"
#include "hook_broker.h"
#include "logging.h"
#include "module_manager.h"
#include "settings_store.h"

#include <Windows.h>

#include <chrono>
#include <cstring>
#include <thread>

namespace BetterEndfield::Host {
namespace {

constexpr auto kRuntimeWait = std::chrono::seconds(90);
constexpr auto kRuntimePoll = std::chrono::milliseconds(100);
constexpr auto kRuntimeSettle = std::chrono::seconds(3);

} // namespace

HostRuntime::HostRuntime(HMODULE host_module, const void* bootstrap_data)
    : host_module_(host_module), bootstrap_data_(bootstrap_data) {
    api_.abi_version = BETTER_ENDFIELD_MODULE_ABI_V1;
    api_.context = this;
    api_.log = &HostRuntime::LogCallback;
    api_.resolve_method = &HostRuntime::ResolveMethodCallback;
    api_.resolve_field = &HostRuntime::ResolveFieldCallback;
    api_.create_hook = &HostRuntime::CreateHookCallback;
    api_.release_module_hooks = &HostRuntime::ReleaseModuleHooksCallback;
    api_.copy_module_configuration = &HostRuntime::CopyModuleConfigurationCallback;
    api_.copy_catalog_root = &HostRuntime::CopyCatalogRootCallback;
    api_.copy_managed_string = &HostRuntime::CopyManagedStringCallback;
    api_.resolve_class = &HostRuntime::ResolveClassCallback;
    api_.object_new = &HostRuntime::ObjectNewCallback;
    api_.string_new = &HostRuntime::StringNewCallback;
    api_.runtime_invoke = &HostRuntime::RuntimeInvokeCallback;
    api_.object_unbox = &HostRuntime::ObjectUnboxCallback;
    api_.gchandle_new = &HostRuntime::GCHandleNewCallback;
    api_.gchandle_free = &HostRuntime::GCHandleFreeCallback;
    api_.field_get_value_object = &HostRuntime::FieldGetValueObjectCallback;
}

HostRuntime::~HostRuntime() {
    if (modules_) {
        modules_->Shutdown();
    }
    if (hooks_) {
        hooks_->Shutdown();
    }
    if (resolver_) {
        resolver_->DetachCurrentThread();
    }
}

void HostRuntime::Run() {
    settings_ = std::make_unique<SettingsStore>();
    std::string error;
    if (!settings_->Initialize(SettingsStore::DiscoverPaths(host_module_, bootstrap_data_),
            error)) {
        return;
    }

    logger_ = std::make_unique<Logger>();
    logger_->Initialize(settings_->Paths().log_root);
    logger_->Write("host", "BetterEndfield.Host starting.");
    logger_->Write("host",
        "Process-lifetime mode active; detach cleanup is intentionally skipped.");

    const auto deadline = std::chrono::steady_clock::now() + kRuntimeWait;
    while (!GetModuleHandleW(L"GameAssembly.dll")) {
        if (std::chrono::steady_clock::now() >= deadline) {
            logger_->Write("host", "GameAssembly.dll did not load before timeout.");
            return;
        }
        std::this_thread::sleep_for(kRuntimePoll);
    }

    // GameAssembly can become visible before Unity has finished registering the
    // collector's own threads. Attaching an injected worker in that interval can
    // terminate the process with "Collecting from unknown thread".
    logger_->Write("host",
        "GameAssembly.dll observed; waiting for IL2CPP GC initialization.");
    std::this_thread::sleep_for(kRuntimeSettle);

    resolver_ = std::make_unique<DynamicResolver>();
    if (!resolver_->Initialize(error)) {
        logger_->Write("host", "Dynamic runtime contract failed: " + error);
        return;
    }
    const auto domain_deadline = std::chrono::steady_clock::now() + kRuntimeWait;
    while (!resolver_->AttachCurrentThread(error)) {
        if (std::chrono::steady_clock::now() >= domain_deadline) {
            logger_->Write("host", "IL2CPP thread contract failed: " + error);
            return;
        }
        std::this_thread::sleep_for(kRuntimePoll);
    }

    hooks_ = std::make_unique<HookBroker>(*logger_);
    if (!hooks_->Initialize()) {
        logger_->Write("host", "Hook broker was not initialized.");
        resolver_->DetachCurrentThread();
        return;
    }

    modules_ = std::make_unique<ModuleManager>(*this, *logger_, *settings_);
    const auto module_deadline = std::chrono::steady_clock::now() + kRuntimeWait;
    do {
        modules_->LoadAll();
        if (!modules_->HasPendingModules() ||
            std::chrono::steady_clock::now() >= module_deadline) {
            break;
        }
        std::this_thread::sleep_for(std::chrono::milliseconds(100));
    } while (!stop_requested_.load(std::memory_order_acquire));

    if (modules_->HasPendingModules()) {
        logger_->Write("host", "One or more modules timed out waiting for IL2CPP assemblies.");
    }
    if (modules_->LoadedCount() == 0) {
        logger_->Write("host", "No module passed its runtime contract.");
        resolver_->DetachCurrentThread();
        return;
    }
    logger_->Write("host", "Module initialization completed.");
    resolver_->DetachCurrentThread();
    logger_->Write("host", "Host worker detached from the IL2CPP runtime.");

    uint64_t configuration_token = settings_->ChangeToken();
    while (!stop_requested_.load(std::memory_order_acquire)) {
        std::this_thread::sleep_for(std::chrono::milliseconds(500));
        const uint64_t next_token = settings_->ChangeToken();
        if (next_token != configuration_token) {
            configuration_token = next_token;
            std::string attach_error;
            if (resolver_->AttachCurrentThread(attach_error)) {
                modules_->ReloadConfigurations();
                resolver_->DetachCurrentThread();
            } else {
                logger_->Write("host",
                    "Configuration reload skipped: " + attach_error);
            }
        }
    }
}

void HostRuntime::RequestStop() {
    stop_requested_.store(true, std::memory_order_release);
}

const BE_HostApiV1& HostRuntime::Api() const {
    return api_;
}

BE_Result HostRuntime::ReleaseHooks(const std::string& module_id) {
    return hooks_ ? hooks_->ReleaseModule(module_id) : BE_Result_NotReady;
}

bool HostRuntime::HasAssembly(const std::string& assembly_name) const {
    return resolver_ && resolver_->HasAssembly(assembly_name.c_str());
}

void BE_CALL HostRuntime::LogCallback(void* context, const char* module_id,
    const char* message) {
    auto* runtime = static_cast<HostRuntime*>(context);
    if (!runtime || !runtime->logger_) {
        return;
    }
    runtime->logger_->Write(module_id ? module_id : "module",
        message ? message : "");
}

BE_Result BE_CALL HostRuntime::ResolveMethodCallback(void* context,
    const BE_MethodDescriptorV1* descriptor, BE_ResolvedMethodV1* result) {
    auto* runtime = static_cast<HostRuntime*>(context);
    if (!runtime || !runtime->resolver_ || !descriptor || !result) {
        return BE_Result_NotReady;
    }
    std::string error;
    const BE_Result status = runtime->resolver_->ResolveMethod(*descriptor, *result, error);
    if (status != BE_Result_Ok && runtime->logger_) {
        runtime->logger_->Write("host.resolver", error);
    }
    return status;
}

BE_Result BE_CALL HostRuntime::ResolveFieldCallback(void* context,
    const BE_FieldDescriptorV1* descriptor, BE_ResolvedFieldV1* result) {
    auto* runtime = static_cast<HostRuntime*>(context);
    if (!runtime || !runtime->resolver_ || !descriptor || !result) {
        return BE_Result_NotReady;
    }
    std::string error;
    const BE_Result status = runtime->resolver_->ResolveField(*descriptor, *result, error);
    if (status != BE_Result_Ok && runtime->logger_) {
        runtime->logger_->Write("host.resolver", error);
    }
    return status;
}

BE_Result BE_CALL HostRuntime::CreateHookCallback(void* context,
    const char* module_id, void* target, void* detour, void** original) {
    auto* runtime = static_cast<HostRuntime*>(context);
    if (!runtime || !runtime->hooks_ || !module_id) {
        return BE_Result_NotReady;
    }
    return runtime->hooks_->Create(module_id, target, detour, original);
}

BE_Result BE_CALL HostRuntime::ReleaseModuleHooksCallback(void* context,
    const char* module_id) {
    auto* runtime = static_cast<HostRuntime*>(context);
    if (!runtime || !module_id) {
        return BE_Result_InvalidArgument;
    }
    return runtime->ReleaseHooks(module_id);
}

int BE_CALL HostRuntime::CopyModuleConfigurationCallback(void* context,
    const char* module_id, char* destination, size_t destination_size) {
    auto* runtime = static_cast<HostRuntime*>(context);
    if (!runtime || !runtime->settings_ || !module_id || !destination ||
        destination_size == 0) {
        return 0;
    }
    const std::string configuration = runtime->settings_->CopyModuleSection(module_id);
    const size_t copy_size = std::min(configuration.size(), destination_size - 1);
    std::memcpy(destination, configuration.data(), copy_size);
    destination[copy_size] = '\0';
    return static_cast<int>(copy_size);
}

int BE_CALL HostRuntime::CopyCatalogRootCallback(void* context, char* destination,
    size_t destination_size) {
    auto* runtime = static_cast<HostRuntime*>(context);
    if (!runtime || !runtime->settings_ || !destination || destination_size == 0) {
        return 0;
    }
    const std::string path = runtime->settings_->Paths().catalog_root.string();
    const size_t copy_size = std::min(path.size(), destination_size - 1);
    std::memcpy(destination, path.data(), copy_size);
    destination[copy_size] = '\0';
    return static_cast<int>(copy_size);
}

int BE_CALL HostRuntime::CopyManagedStringCallback(void* context,
    const void* managed_string, char* destination, size_t destination_size) {
    auto* runtime = static_cast<HostRuntime*>(context);
    if (!runtime || !runtime->resolver_ || !destination || destination_size == 0) {
        return 0;
    }
    return runtime->resolver_->CopyManagedString(managed_string, destination,
        destination_size);
}

BE_Result BE_CALL HostRuntime::ResolveClassCallback(void* context,
    const char* assembly_name, const char* namespace_name, const char* class_name,
    BE_ResolvedClassV1* result) {
    auto* runtime = static_cast<HostRuntime*>(context);
    if (!runtime || !runtime->resolver_ || !assembly_name || !namespace_name ||
        !class_name || !result) {
        return BE_Result_NotReady;
    }
    std::string error;
    const BE_Result status = runtime->resolver_->ResolveClass(assembly_name,
        namespace_name, class_name, *result, error);
    if (status != BE_Result_Ok && runtime->logger_) {
        runtime->logger_->Write("host.resolver", error);
    }
    return status;
}

void* BE_CALL HostRuntime::ObjectNewCallback(void* context, const void* class_info) {
    auto* runtime = static_cast<HostRuntime*>(context);
    return runtime && runtime->resolver_
        ? runtime->resolver_->ObjectNew(class_info)
        : nullptr;
}

void* BE_CALL HostRuntime::StringNewCallback(void* context, const char* utf8) {
    auto* runtime = static_cast<HostRuntime*>(context);
    return runtime && runtime->resolver_ ? runtime->resolver_->StringNew(utf8) : nullptr;
}

void* BE_CALL HostRuntime::RuntimeInvokeCallback(void* context, const void* method_info,
    void* instance, void** parameters, void** exception) {
    auto* runtime = static_cast<HostRuntime*>(context);
    return runtime && runtime->resolver_
        ? runtime->resolver_->RuntimeInvoke(method_info, instance, parameters, exception)
        : nullptr;
}

void* BE_CALL HostRuntime::ObjectUnboxCallback(void* context, void* boxed_value) {
    auto* runtime = static_cast<HostRuntime*>(context);
    return runtime && runtime->resolver_
        ? runtime->resolver_->ObjectUnbox(boxed_value)
        : nullptr;
}

void* BE_CALL HostRuntime::FieldGetValueObjectCallback(void* context,
    const void* field_info, void* instance) {
    auto* runtime = static_cast<HostRuntime*>(context);
    return runtime && runtime->resolver_
        ? runtime->resolver_->FieldGetValueObject(field_info, instance)
        : nullptr;
}

uint32_t BE_CALL HostRuntime::GCHandleNewCallback(void* context, void* object,
    int pinned) {
    auto* runtime = static_cast<HostRuntime*>(context);
    return runtime && runtime->resolver_
        ? runtime->resolver_->GCHandleNew(object, pinned != 0)
        : 0;
}

void BE_CALL HostRuntime::GCHandleFreeCallback(void* context, uint32_t handle) {
    auto* runtime = static_cast<HostRuntime*>(context);
    if (runtime && runtime->resolver_) {
        runtime->resolver_->GCHandleFree(handle);
    }
}

} // namespace BetterEndfield::Host
