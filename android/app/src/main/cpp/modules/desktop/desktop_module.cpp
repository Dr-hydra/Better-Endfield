#include "modules/desktop/desktop_module.h"

#include "core/log.h"
#include "core/runtime.h"

#include <algorithm>
#include <cstdlib>
#include <cstring>

namespace betterendfield {

DesktopModule::DesktopModule(
    const char* id,
    const char* configuration_variable,
    ApiGetter api_getter,
    std::string active_message)
    : id_(id),
      configuration_variable_(configuration_variable),
      api_getter_(api_getter),
      active_message_(std::move(active_message)) {
    host_.abi_version = BETTER_ENDFIELD_MODULE_ABI_V1;
    host_.context = this;
    host_.log = &LogCallback;
    host_.resolve_method = &ResolveMethodCallback;
    host_.resolve_field = &ResolveFieldCallback;
    host_.create_hook = &CreateHookCallback;
    host_.release_module_hooks = &ReleaseHooksCallback;
    host_.copy_module_configuration = &CopyConfigurationCallback;
    host_.copy_catalog_root = &CopyCatalogRootCallback;
    host_.copy_managed_string = &CopyManagedStringCallback;
    host_.resolve_class = &ResolveClassCallback;
    host_.object_new = &ObjectNewCallback;
    host_.string_new = &StringNewCallback;
    host_.runtime_invoke = &RuntimeInvokeCallback;
    host_.object_unbox = &ObjectUnboxCallback;
    host_.gchandle_new = &GcHandleNewCallback;
    host_.gchandle_free = &GcHandleFreeCallback;
    host_.field_get_value_object = &FieldGetValueObjectCallback;
}

DesktopModule::~DesktopModule() {
    if (initialized_ && api_ != nullptr && api_->shutdown != nullptr) {
        api_->shutdown();
    }
    ReleaseHooksCallback(this, id_);
}

const char* DesktopModule::Id() const { return id_; }

ModuleResult DesktopModule::Start(Il2CppRuntime& runtime) {
    const char* configured = std::getenv(configuration_variable_);
    if (configured == nullptr || *configured == 0) {
        return {false, std::string("no configuration published in ") + configuration_variable_};
    }
    configuration_ = configured;
    // The desktop loader hands each module the body of its own [Module] section.
    // A leading section header would be parsed as a key, so drop it the same way
    // the login-model adapter does.
    if (!configuration_.empty() && configuration_.front() == '[') {
        const size_t line_end = configuration_.find_first_of("\r\n");
        const size_t content_start = line_end == std::string::npos
            ? std::string::npos
            : configuration_.find_first_not_of("\r\n", line_end);
        configuration_ = content_start == std::string::npos
            ? std::string{} : configuration_.substr(content_start);
    }
    runtime_ = &runtime;
    std::string hook_error;
    if (!hook_broker_.Initialize(hook_error)) {
        return {false, std::move(hook_error)};
    }
    api_ = api_getter_ == nullptr ? nullptr : api_getter_();
    if (api_ == nullptr || api_->descriptor.abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        api_->initialize == nullptr || api_->configuration_changed == nullptr) {
        return {false, "desktop module ABI is unavailable"};
    }
    const BE_Result initialize = api_->initialize(&host_);
    if (initialize != BE_Result_Ok) {
        return {false, "desktop contract initialization failed: " +
            std::to_string(static_cast<int>(initialize))};
    }
    initialized_ = true;
    const BE_Result applied = api_->configuration_changed(configuration_.c_str());
    if (applied != BE_Result_Ok) {
        return {false, "desktop configuration rejected: " +
            std::to_string(static_cast<int>(applied))};
    }
    return {true, active_message_};
}

DesktopModule* DesktopModule::Self(void* context) {
    return static_cast<DesktopModule*>(context);
}

void DesktopModule::LogCallback(void* context, const char* module_id, const char* message) {
    DesktopModule* self = Self(context);
    LogInfo(module_id != nullptr ? module_id : (self != nullptr ? self->id_ : "betterendfield"),
        message == nullptr ? "" : message);
}

BE_Result DesktopModule::ResolveMethodCallback(
    void* context, const BE_MethodDescriptorV1* descriptor, BE_ResolvedMethodV1* result) {
    DesktopModule* self = Self(context);
    if (self == nullptr || self->runtime_ == nullptr || descriptor == nullptr ||
        result == nullptr) {
        return BE_Result_InvalidArgument;
    }
    const ResolvedMethod method = self->runtime_->ResolveMethodExact(
        descriptor->assembly_name,
        descriptor->namespace_name,
        descriptor->class_name,
        descriptor->method_name,
        descriptor->parameter_types,
        descriptor->return_type,
        static_cast<int>(descriptor->parameter_count));
    if (method.entry == nullptr || method.info == nullptr) {
        *result = {};
        return BE_Result_NotFound;
    }
    result->method_info = method.info;
    result->method_pointer = method.entry;
    return BE_Result_Ok;
}

BE_Result DesktopModule::ResolveFieldCallback(
    void* context, const BE_FieldDescriptorV1* descriptor, BE_ResolvedFieldV1* result) {
    DesktopModule* self = Self(context);
    if (self == nullptr || self->runtime_ == nullptr || descriptor == nullptr ||
        result == nullptr) {
        return BE_Result_InvalidArgument;
    }
    const ResolvedField field = self->runtime_->ResolveField(
        descriptor->assembly_name,
        descriptor->namespace_name,
        descriptor->class_name,
        descriptor->field_name);
    if (field.info == nullptr || field.offset < 0) {
        *result = {};
        return BE_Result_NotFound;
    }
    result->field_info = field.info;
    result->offset = field.offset;
    return BE_Result_Ok;
}

BE_Result DesktopModule::CreateHookCallback(
    void* context, const char* module_id, void* target, void* detour, void** original) {
    DesktopModule* self = Self(context);
    if (self == nullptr || target == nullptr || detour == nullptr || original == nullptr) {
        return BE_Result_InvalidArgument;
    }
    void* stub = nullptr;
    std::string error;
    if (!self->hook_broker_.Install(target, detour, original, stub, error)) {
        LogError(self->id_, error.c_str());
        return BE_Result_Failed;
    }
    self->hooks_.push_back({module_id == nullptr ? self->id_ : module_id, stub});
    return BE_Result_Ok;
}

BE_Result DesktopModule::ReleaseHooksCallback(void* context, const char* module_id) {
    DesktopModule* self = Self(context);
    if (self == nullptr) {
        return BE_Result_InvalidArgument;
    }
    const std::string requested = module_id == nullptr ? std::string{} : module_id;
    for (HookRecord& hook : self->hooks_) {
        if (requested.empty() || hook.module_id == requested) {
            self->hook_broker_.Remove(hook.stub);
            hook.stub = nullptr;
        }
    }
    self->hooks_.erase(
        std::remove_if(self->hooks_.begin(), self->hooks_.end(),
            [](const HookRecord& hook) { return hook.stub == nullptr; }),
        self->hooks_.end());
    return BE_Result_Ok;
}

int DesktopModule::CopyConfigurationCallback(
    void* context, const char* module_id, char* destination, size_t destination_size) {
    DesktopModule* self = Self(context);
    if (self == nullptr || destination == nullptr || destination_size == 0 ||
        (module_id != nullptr && std::strcmp(module_id, self->id_) != 0)) {
        return 0;
    }
    const size_t count = std::min(destination_size - 1, self->configuration_.size());
    std::memcpy(destination, self->configuration_.data(), count);
    destination[count] = 0;
    return static_cast<int>(count);
}

int DesktopModule::CopyCatalogRootCallback(
    void* context, char* destination, size_t destination_size) {
    (void)context;
    if (destination != nullptr && destination_size > 0) destination[0] = 0;
    return 0;
}

int DesktopModule::CopyManagedStringCallback(
    void* context, const void* managed_string, char* destination, size_t destination_size) {
    DesktopModule* self = Self(context);
    if (self == nullptr || self->runtime_ == nullptr || destination == nullptr ||
        destination_size == 0) {
        return 0;
    }
    const std::string value = self->runtime_->CopyString(const_cast<void*>(managed_string));
    const size_t count = std::min(destination_size - 1, value.size());
    std::memcpy(destination, value.data(), count);
    destination[count] = 0;
    return static_cast<int>(count);
}

BE_Result DesktopModule::ResolveClassCallback(
    void* context,
    const char* assembly_name,
    const char* namespace_name,
    const char* class_name,
    BE_ResolvedClassV1* result) {
    DesktopModule* self = Self(context);
    if (self == nullptr || self->runtime_ == nullptr || result == nullptr) {
        return BE_Result_InvalidArgument;
    }
    const ResolvedClass klass = self->runtime_->ResolveClass(
        assembly_name, namespace_name, class_name);
    if (klass.info == nullptr) {
        *result = {};
        return BE_Result_NotFound;
    }
    result->class_info = klass.info;
    result->type_info = klass.type;
    result->type_object = klass.type_object;
    return BE_Result_Ok;
}

void* DesktopModule::ObjectNewCallback(void* context, const void* class_info) {
    DesktopModule* self = Self(context);
    return self == nullptr || self->runtime_ == nullptr
        ? nullptr
        : self->runtime_->NewObject(reinterpret_cast<const Il2CppClass*>(class_info));
}

void* DesktopModule::StringNewCallback(void* context, const char* utf8) {
    DesktopModule* self = Self(context);
    return self == nullptr || self->runtime_ == nullptr
        ? nullptr : self->runtime_->NewString(utf8);
}

void* DesktopModule::RuntimeInvokeCallback(
    void* context,
    const void* method_info,
    void* instance,
    void** parameters,
    void** exception) {
    DesktopModule* self = Self(context);
    return self == nullptr || self->runtime_ == nullptr
        ? nullptr
        : self->runtime_->Invoke(
            reinterpret_cast<const MethodInfo*>(method_info), instance, parameters, exception);
}

void* DesktopModule::ObjectUnboxCallback(void* context, void* boxed_value) {
    DesktopModule* self = Self(context);
    return self == nullptr || self->runtime_ == nullptr
        ? nullptr : self->runtime_->Unbox(boxed_value);
}

uint32_t DesktopModule::GcHandleNewCallback(void* context, void* object, int pinned) {
    DesktopModule* self = Self(context);
    return self == nullptr || self->runtime_ == nullptr
        ? 0 : self->runtime_->NewGcHandle(object, pinned != 0);
}

void DesktopModule::GcHandleFreeCallback(void* context, uint32_t handle) {
    DesktopModule* self = Self(context);
    if (self != nullptr && self->runtime_ != nullptr) {
        self->runtime_->FreeGcHandle(handle);
    }
}

void* DesktopModule::FieldGetValueObjectCallback(
    void* context, const void* field_info, void* instance) {
    DesktopModule* self = Self(context);
    return self == nullptr || self->runtime_ == nullptr
        ? nullptr
        : self->runtime_->ReadFieldObject(
            reinterpret_cast<const FieldInfo*>(field_info), instance);
}

}  // namespace betterendfield
