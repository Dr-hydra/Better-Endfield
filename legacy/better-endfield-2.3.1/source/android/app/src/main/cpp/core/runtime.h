#pragma once

#include <cstddef>
#include <cstdint>
#include <string>

namespace betterendfield {

struct Il2CppDomain;
struct Il2CppAssembly;
struct Il2CppImage;
struct Il2CppClass;
struct MethodInfo;
struct FieldInfo;
struct Il2CppType;

struct ResolvedMethod {
    const MethodInfo* info = nullptr;
    void* entry = nullptr;
};

struct ResolvedField {
    const FieldInfo* info = nullptr;
    int32_t offset = -1;
};

struct ResolvedClass {
    Il2CppClass* info = nullptr;
    const Il2CppType* type = nullptr;
    void* type_object = nullptr;
};

class Il2CppRuntime final {
public:
    Il2CppRuntime() = default;
    ~Il2CppRuntime();

    Il2CppRuntime(const Il2CppRuntime&) = delete;
    Il2CppRuntime& operator=(const Il2CppRuntime&) = delete;

    bool Connect();
    void* AttachCurrentThread() const;
    void DetachCurrentThread(void* thread) const;
    ResolvedMethod ResolveMethod(
        const char* assembly,
        const char* namespaze,
        const char* klass,
        const char* method,
        int parameter_count) const;
    ResolvedMethod ResolveMethodExact(
        const char* assembly,
        const char* namespaze,
        const char* klass,
        const char* method,
        const char* parameter_types,
        const char* return_type,
        int parameter_count) const;
    ResolvedClass ResolveClass(
        const char* assembly,
        const char* namespaze,
        const char* klass) const;
    ResolvedField ResolveField(
        const char* assembly,
        const char* namespaze,
        const char* klass,
        const char* field) const;
    void* ReadFieldObject(const ResolvedField& field, void* instance) const;
    void* ReadFieldObject(const FieldInfo* field, void* instance) const;
    std::string CopyString(void* value) const;
    void* NewString(const char* value) const;
    void* NewObject(const Il2CppClass* klass) const;
    void* Unbox(void* value) const;
    uint32_t NewGcHandle(void* value, bool pinned) const;
    void FreeGcHandle(uint32_t handle) const;
    void* Invoke(
        const MethodInfo* method,
        void* instance,
        void** parameters,
        void** exception) const;

private:
    void* library_ = nullptr;
    Il2CppDomain* (*domain_get_)() = nullptr;
    void* (*thread_attach_)(Il2CppDomain*) = nullptr;
    void (*thread_detach_)(void*) = nullptr;
    const Il2CppAssembly* (*domain_assembly_open_)(Il2CppDomain*, const char*) = nullptr;
    const Il2CppImage* (*assembly_get_image_)(const Il2CppAssembly*) = nullptr;
    Il2CppClass* (*class_from_name_)(const Il2CppImage*, const char*, const char*) = nullptr;
    const MethodInfo* (*class_get_method_from_name_)(Il2CppClass*, const char*, int) = nullptr;
    const MethodInfo* (*class_get_methods_)(Il2CppClass*, void**) = nullptr;
    const char* (*method_get_name_)(const MethodInfo*) = nullptr;
    uint32_t (*method_get_parameter_count_)(const MethodInfo*) = nullptr;
    const Il2CppType* (*method_get_parameter_)(const MethodInfo*, uint32_t) = nullptr;
    const Il2CppType* (*method_get_return_type_)(const MethodInfo*) = nullptr;
    char* (*type_get_name_)(const Il2CppType*) = nullptr;
    const Il2CppType* (*class_get_type_)(Il2CppClass*) = nullptr;
    void* (*type_get_object_)(const Il2CppType*) = nullptr;
    const FieldInfo* (*class_get_field_from_name_)(Il2CppClass*, const char*) = nullptr;
    size_t (*field_get_offset_)(const FieldInfo*) = nullptr;
    void* (*field_get_value_object_)(const FieldInfo*, void*) = nullptr;
    const char16_t* (*string_chars_)(void*) = nullptr;
    int32_t (*string_length_)(void*) = nullptr;
    void* (*string_new_)(const char*) = nullptr;
    void* (*object_new_)(const Il2CppClass*) = nullptr;
    void* (*object_unbox_)(void*) = nullptr;
    uint32_t (*gchandle_new_)(void*, bool) = nullptr;
    void (*gchandle_free_)(uint32_t) = nullptr;
    void* (*runtime_invoke_)(const MethodInfo*, void*, void**, void**) = nullptr;
};

class Il2CppThreadScope final {
public:
    explicit Il2CppThreadScope(const Il2CppRuntime& runtime);
    ~Il2CppThreadScope();

    bool attached() const { return thread_ != nullptr; }

private:
    const Il2CppRuntime& runtime_;
    void* thread_ = nullptr;
};

}  // namespace betterendfield
