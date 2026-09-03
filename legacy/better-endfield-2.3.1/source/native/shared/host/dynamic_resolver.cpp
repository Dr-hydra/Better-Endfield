#include "dynamic_resolver.h"

#include <algorithm>
#include <cstdint>
#include <sstream>
#include <string_view>
#include <vector>

namespace BetterEndfield::Host {
namespace {

std::vector<std::string_view> SplitParameters(const char* parameters) {
    if (!parameters || *parameters == '\0') {
        return {};
    }

    std::vector<std::string_view> result;
    std::string_view remaining(parameters);
    while (!remaining.empty()) {
        const size_t separator = remaining.find('|');
        const std::string_view value = remaining.substr(0, separator);
        result.push_back(value);
        if (separator == std::string_view::npos) {
            break;
        }
        remaining.remove_prefix(separator + 1);
    }
    return result;
}

bool SameText(const char* actual, std::string_view expected) {
    return actual && std::string_view(actual) == expected;
}

bool SameTypeText(const char* actual, std::string_view expected) {
    if (!actual) {
        return false;
    }
    const std::string_view actual_view(actual);
    if (actual_view.size() != expected.size()) {
        return false;
    }
    for (size_t index = 0; index < actual_view.size(); ++index) {
        const auto normalize = [](char value) {
            return value == '/' || value == '+' ? '.' : value;
        };
        if (normalize(actual_view[index]) != normalize(expected[index])) {
            return false;
        }
    }
    return true;
}

struct Il2CppMethodInfoPrefix {
    const void* method_pointer;
};

template <typename T>
T Export(HMODULE module, const char* name) {
    return reinterpret_cast<T>(GetProcAddress(module, name));
}

} // namespace

bool DynamicResolver::Initialize(std::string& error) {
    game_assembly_ = GetModuleHandleW(L"GameAssembly.dll");
    if (!game_assembly_) {
        error = "GameAssembly.dll is not loaded.";
        return false;
    }

    domain_get_ = Export<DomainGetFn>(game_assembly_, "il2cpp_domain_get");
    domain_get_assemblies_ = Export<DomainGetAssembliesFn>(game_assembly_,
        "il2cpp_domain_get_assemblies");
    assembly_get_image_ = Export<AssemblyGetImageFn>(game_assembly_,
        "il2cpp_assembly_get_image");
    image_get_name_ = Export<ImageGetNameFn>(game_assembly_, "il2cpp_image_get_name");
    class_from_name_ = Export<ClassFromNameFn>(game_assembly_, "il2cpp_class_from_name");
    class_get_name_ = Export<ClassGetNameFn>(game_assembly_, "il2cpp_class_get_name");
    class_get_nested_types_ = Export<ClassGetNestedTypesFn>(game_assembly_,
        "il2cpp_class_get_nested_types");
    class_get_methods_ = Export<ClassGetMethodsFn>(game_assembly_,
        "il2cpp_class_get_methods");
    method_get_name_ = Export<MethodGetNameFn>(game_assembly_, "il2cpp_method_get_name");
    method_get_parameter_count_ = Export<MethodGetParameterCountFn>(game_assembly_,
        "il2cpp_method_get_param_count");
    method_get_parameter_ = Export<MethodGetParameterFn>(game_assembly_,
        "il2cpp_method_get_param");
    method_get_return_type_ = Export<MethodGetReturnTypeFn>(game_assembly_,
        "il2cpp_method_get_return_type");
    type_get_name_ = Export<TypeGetNameFn>(game_assembly_, "il2cpp_type_get_name");
    class_get_field_from_name_ = Export<ClassGetFieldFromNameFn>(game_assembly_,
        "il2cpp_class_get_field_from_name");
    field_get_offset_ = Export<FieldGetOffsetFn>(game_assembly_,
        "il2cpp_field_get_offset");
    field_get_type_ = Export<FieldGetTypeFn>(game_assembly_, "il2cpp_field_get_type");
    string_length_ = Export<StringLengthFn>(game_assembly_, "il2cpp_string_length");
    string_chars_ = Export<StringCharsFn>(game_assembly_, "il2cpp_string_chars");
    thread_attach_ = Export<ThreadAttachFn>(game_assembly_, "il2cpp_thread_attach");
    thread_detach_ = Export<ThreadDetachFn>(game_assembly_, "il2cpp_thread_detach");
    class_get_type_ = Export<ClassGetTypeFn>(game_assembly_, "il2cpp_class_get_type");
    type_get_object_ = Export<TypeGetObjectFn>(game_assembly_, "il2cpp_type_get_object");
    object_new_ = Export<ObjectNewFn>(game_assembly_, "il2cpp_object_new");
    string_new_ = Export<StringNewFn>(game_assembly_, "il2cpp_string_new");
    runtime_invoke_ = Export<RuntimeInvokeFn>(game_assembly_, "il2cpp_runtime_invoke");
    object_unbox_ = Export<ObjectUnboxFn>(game_assembly_, "il2cpp_object_unbox");
    field_get_value_object_ = Export<FieldGetValueObjectFn>(game_assembly_,
        "il2cpp_field_get_value_object");
    gchandle_new_ = Export<GCHandleNewFn>(game_assembly_, "il2cpp_gchandle_new");
    gchandle_free_ = Export<GCHandleFreeFn>(game_assembly_, "il2cpp_gchandle_free");
    BuildExecutableRanges();

    if (!domain_get_ || !domain_get_assemblies_ || !assembly_get_image_ ||
        !image_get_name_ || !class_from_name_ || !class_get_methods_ ||
        !method_get_name_ || !method_get_parameter_count_ || !method_get_parameter_ ||
        !method_get_return_type_ || !type_get_name_ || !class_get_field_from_name_ ||
        !field_get_offset_ || !field_get_type_ || !string_length_ || !string_chars_ ||
        !thread_attach_ || !thread_detach_ || !class_get_type_ ||
        !type_get_object_ || !object_new_ || !string_new_ || !runtime_invoke_ ||
        !object_unbox_ || !field_get_value_object_ || !gchandle_new_ ||
        !gchandle_free_ ||
        executable_ranges_.empty()) {
        error = "The client does not export the required IL2CPP metadata contract.";
        return false;
    }
    return true;
}

void DynamicResolver::BuildExecutableRanges() {
    executable_ranges_.clear();
    if (!game_assembly_) {
        return;
    }

    const auto base = reinterpret_cast<uintptr_t>(game_assembly_);
    __try {
        const auto* dos = reinterpret_cast<const IMAGE_DOS_HEADER*>(base);
        if (dos->e_magic != IMAGE_DOS_SIGNATURE || dos->e_lfanew <= 0) {
            return;
        }
        const auto* nt = reinterpret_cast<const IMAGE_NT_HEADERS*>(
            base + static_cast<uintptr_t>(dos->e_lfanew));
        if (nt->Signature != IMAGE_NT_SIGNATURE ||
            nt->OptionalHeader.Magic != IMAGE_NT_OPTIONAL_HDR64_MAGIC) {
            return;
        }

        const auto* section = IMAGE_FIRST_SECTION(nt);
        for (WORD index = 0; index < nt->FileHeader.NumberOfSections; ++index, ++section) {
            if ((section->Characteristics & IMAGE_SCN_MEM_EXECUTE) == 0 ||
                section->Misc.VirtualSize == 0) {
                continue;
            }
            const uintptr_t begin = base + section->VirtualAddress;
            const uintptr_t end = begin + section->Misc.VirtualSize;
            if (end > begin) {
                executable_ranges_.emplace_back(begin, end);
            }
        }
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        executable_ranges_.clear();
    }
}

bool DynamicResolver::IsExecutableAddress(uintptr_t address) const {
    if (address == 0) {
        return false;
    }
    const auto range = std::find_if(executable_ranges_.begin(), executable_ranges_.end(),
        [address](const auto& candidate) {
            return address >= candidate.first && address < candidate.second;
        });
    if (range == executable_ranges_.end()) {
        return false;
    }

    MEMORY_BASIC_INFORMATION memory{};
    if (VirtualQuery(reinterpret_cast<const void*>(address), &memory, sizeof(memory)) == 0 ||
        memory.State != MEM_COMMIT || (memory.Protect & PAGE_GUARD) != 0 ||
        (memory.Protect & PAGE_NOACCESS) != 0) {
        return false;
    }
    const DWORD protection = memory.Protect & 0xFFu;
    return protection == PAGE_EXECUTE || protection == PAGE_EXECUTE_READ ||
        protection == PAGE_EXECUTE_READWRITE || protection == PAGE_EXECUTE_WRITECOPY;
}

void* DynamicResolver::ResolveMethodEntry(void* method) const {
    if (!method) {
        return nullptr;
    }

    // Unity 2021's IL2CPP MethodInfo ABI starts with methodPointer. This is the
    // sole native-entry contract: reject it unless it points into a current
    // executable GameAssembly section. No alternate slot or client address is
    // attempted.
    __try {
        const auto* info = static_cast<const Il2CppMethodInfoPrefix*>(method);
        const uintptr_t candidate =
            reinterpret_cast<uintptr_t>(info->method_pointer);
        if (IsExecutableAddress(candidate)) {
            return reinterpret_cast<void*>(candidate);
        }
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
    }
    return nullptr;
}

bool DynamicResolver::AttachCurrentThread(std::string& error) {
    if (attached_thread_) {
        return true;
    }
    if (!domain_get_ || !thread_attach_) {
        error = "IL2CPP thread attach exports are unavailable.";
        return false;
    }
    void* domain = domain_get_();
    attached_thread_ = domain ? thread_attach_(domain) : nullptr;
    if (!attached_thread_) {
        error = "The Host thread could not attach to the IL2CPP domain.";
        return false;
    }
    return true;
}

void DynamicResolver::DetachCurrentThread() {
    if (attached_thread_ && thread_detach_) {
        thread_detach_(attached_thread_);
    }
    attached_thread_ = nullptr;
}

bool DynamicResolver::HasAssembly(const char* assembly_name) const {
    return FindImage(assembly_name) != nullptr;
}

BE_Result DynamicResolver::ResolveField(const BE_FieldDescriptorV1& descriptor,
    BE_ResolvedFieldV1& result, std::string& error) const {
    result = {};
    if (!descriptor.assembly_name || !descriptor.namespace_name ||
        !descriptor.class_name || !descriptor.field_name ||
        !class_get_field_from_name_ || !field_get_offset_) {
        error = "Field descriptor is incomplete or unsupported.";
        return BE_Result_InvalidArgument;
    }
    void* image = FindImage(descriptor.assembly_name);
    if (!image) {
        error = "Assembly was not found for field: " + std::string(descriptor.assembly_name);
        return BE_Result_NotFound;
    }
    void* klass = FindClass(image, descriptor.namespace_name, descriptor.class_name);
    void* field = klass ? class_get_field_from_name_(klass, descriptor.field_name) : nullptr;
    if (!field) {
        error = "Field was not found: " + std::string(descriptor.field_name);
        return BE_Result_NotFound;
    }
    const int32_t offset = field_get_offset_(field);
    if (offset < 0) {
        error = "Field has no instance offset: " + std::string(descriptor.field_name);
        return BE_Result_ContractMismatch;
    }
    if (descriptor.field_type && *descriptor.field_type != '\0') {
        void* field_type = field_get_type_(field);
        const char* actual_type = field_type ? type_get_name_(field_type) : nullptr;
        if (!SameText(actual_type, descriptor.field_type)) {
            error = "Field type contract mismatch: " +
                std::string(descriptor.field_name);
            return BE_Result_ContractMismatch;
        }
    }
    result.field_info = field;
    result.offset = offset;
    return BE_Result_Ok;
}

BE_Result DynamicResolver::ResolveClass(const char* assembly_name,
    const char* namespace_name, const char* class_name, BE_ResolvedClassV1& result,
    std::string& error) const {
    result = {};
    if (!assembly_name || !namespace_name || !class_name || !class_from_name_) {
        error = "Class descriptor is incomplete.";
        return BE_Result_InvalidArgument;
    }
    void* image = FindImage(assembly_name);
    if (!image) {
        error = "Assembly was not found for class: " + std::string(assembly_name);
        return BE_Result_NotFound;
    }
    void* klass = FindClass(image, namespace_name, class_name);
    if (!klass) {
        error = "Class was not found: " + std::string(class_name);
        return BE_Result_NotFound;
    }
    result.class_info = klass;
    if (class_get_type_) {
        result.type_info = class_get_type_(klass);
    }
    if (result.type_info && type_get_object_) {
        result.type_object = type_get_object_(result.type_info);
    }
    return BE_Result_Ok;
}

int DynamicResolver::CopyManagedString(const void* managed_string, char* destination,
    size_t destination_size) const {
    if (destination && destination_size > 0) {
        destination[0] = '\0';
    }
    if (!managed_string || !destination || destination_size == 0 || !string_length_ ||
        !string_chars_) {
        return 0;
    }
    __try {
        const int32_t length = string_length_(const_cast<void*>(managed_string));
        wchar_t* chars = string_chars_(const_cast<void*>(managed_string));
        if (length <= 0 || !chars) {
            return 0;
        }
        const int required = WideCharToMultiByte(CP_UTF8, 0, chars, length, nullptr, 0,
            nullptr, nullptr);
        if (required <= 0) {
            return 0;
        }
        const int copied = WideCharToMultiByte(CP_UTF8, 0, chars, length, destination,
            static_cast<int>(destination_size - 1), nullptr, nullptr);
        destination[std::max(copied, 0)] = '\0';
        return copied;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        destination[0] = '\0';
        return 0;
    }
}

void* DynamicResolver::ObjectNew(const void* class_info) const {
    return object_new_ && class_info
        ? object_new_(const_cast<void*>(class_info))
        : nullptr;
}

void* DynamicResolver::StringNew(const char* utf8) const {
    return string_new_ && utf8 ? string_new_(utf8) : nullptr;
}

void* DynamicResolver::RuntimeInvoke(const void* method_info, void* instance,
    void** parameters, void** exception) const {
    if (exception) {
        *exception = nullptr;
    }
    return runtime_invoke_ && method_info
        ? runtime_invoke_(method_info, instance, parameters, exception)
        : nullptr;
}

void* DynamicResolver::ObjectUnbox(void* boxed_value) const {
    return object_unbox_ && boxed_value ? object_unbox_(boxed_value) : nullptr;
}

void* DynamicResolver::FieldGetValueObject(const void* field_info,
    void* instance) const {
    // A null instance is how IL2CPP reads a static field; only the field itself
    // is required.
    return field_get_value_object_ && field_info
        ? field_get_value_object_(const_cast<void*>(field_info), instance)
        : nullptr;
}

uint32_t DynamicResolver::GCHandleNew(void* object, bool pinned) const {
    return gchandle_new_ && object ? gchandle_new_(object, pinned) : 0;
}

void DynamicResolver::GCHandleFree(uint32_t handle) const {
    if (gchandle_free_ && handle != 0) {
        gchandle_free_(handle);
    }
}

void* DynamicResolver::FindImage(const char* assembly_name) const {
    if (!assembly_name || !domain_get_ || !domain_get_assemblies_) {
        return nullptr;
    }
    void* domain = domain_get_();
    if (!domain) {
        return nullptr;
    }
    size_t assembly_count = 0;
    void** assemblies = domain_get_assemblies_(domain, &assembly_count);
    for (size_t index = 0; assemblies && index < assembly_count; ++index) {
        void* image = assembly_get_image_(assemblies[index]);
        const char* image_name = image ? image_get_name_(image) : nullptr;
        if (image_name && _stricmp(image_name, assembly_name) == 0) {
            return image;
        }
    }
    return nullptr;
}

void* DynamicResolver::FindNestedClass(void* outer, const char* class_name) const {
    if (!outer || !class_name || !*class_name || !class_get_nested_types_ ||
        !class_get_name_) {
        return nullptr;
    }
    std::string_view path(class_name);
    const size_t separator = path.find_first_of("./+");
    const std::string_view wanted = path.substr(0, separator);
    void* iterator = nullptr;
    while (void* nested = class_get_nested_types_(outer, &iterator)) {
        const char* nested_name = class_get_name_(nested);
        if (!nested_name || std::string_view(nested_name) != wanted) {
            continue;
        }
        if (separator == std::string_view::npos) {
            return nested;
        }
        const std::string remainder(path.substr(separator + 1));
        return FindNestedClass(nested, remainder.c_str());
    }
    return nullptr;
}

void* DynamicResolver::FindClass(void* image, const char* namespace_name,
    const char* class_name) const {
    if (!image || !namespace_name || !class_name || !class_from_name_) {
        return nullptr;
    }
    if (void* direct = class_from_name_(image, namespace_name, class_name)) {
        return direct;
    }
    const std::string_view path(class_name);
    if (path.find_first_of("./+") == std::string_view::npos ||
        !class_get_nested_types_ || !class_get_name_) {
        return nullptr;
    }
    const size_t separator = path.find_first_of("./+");
    const std::string outer_name(path.substr(0, separator));
    void* outer = class_from_name_(image, namespace_name, outer_name.c_str());
    if (!outer) {
        return nullptr;
    }
    const std::string remainder(path.substr(separator + 1));
    return FindNestedClass(outer, remainder.c_str());
}

bool DynamicResolver::MatchesMethod(void* method,
    const BE_MethodDescriptorV1& descriptor) const {
    const char* method_name = method_get_name_(method);
    if (!method_name || std::string_view(method_name) != descriptor.method_name ||
        method_get_parameter_count_(method) != descriptor.parameter_count) {
        return false;
    }

    const std::vector<std::string_view> parameters =
        SplitParameters(descriptor.parameter_types);
    if (!parameters.empty() && parameters.size() != descriptor.parameter_count) {
        return false;
    }
    for (uint32_t index = 0; index < descriptor.parameter_count; ++index) {
        if (!parameters.empty()) {
            const char* type_name = type_get_name_(const_cast<void*>(
                method_get_parameter_(method, index)));
            if (!SameTypeText(type_name, parameters[index])) {
                return false;
            }
        }
    }

    if (descriptor.return_type && *descriptor.return_type != '\0') {
        const char* return_type = type_get_name_(method_get_return_type_(method));
        if (!SameText(return_type, descriptor.return_type)) {
            return false;
        }
    }
    return true;
}

BE_Result DynamicResolver::ResolveMethod(const BE_MethodDescriptorV1& descriptor,
    BE_ResolvedMethodV1& result, std::string& error) const {
    result = {};
    if (!descriptor.assembly_name || !descriptor.namespace_name ||
        !descriptor.class_name || !descriptor.method_name) {
        error = "Method descriptor is incomplete.";
        return BE_Result_InvalidArgument;
    }

    void* image = FindImage(descriptor.assembly_name);
    if (!image) {
        error = "Assembly was not found: " + std::string(descriptor.assembly_name);
        return BE_Result_NotFound;
    }
    void* klass = FindClass(image, descriptor.namespace_name, descriptor.class_name);
    if (!klass) {
        error = "Class was not found: " + std::string(descriptor.class_name);
        return BE_Result_NotFound;
    }

    void* iterator = nullptr;
    while (void* method = class_get_methods_(klass, &iterator)) {
        if (!MatchesMethod(method, descriptor)) {
            continue;
        }
        void* pointer = ResolveMethodEntry(method);
        if (!pointer) {
            error = "IL2CPP MethodInfo did not expose a verified callable entry.";
            return BE_Result_ContractMismatch;
        }
        result.method_info = method;
        result.method_pointer = pointer;
        return BE_Result_Ok;
    }

    error = "Method contract was not found: " + std::string(descriptor.method_name);
    return BE_Result_NotFound;
}

} // namespace BetterEndfield::Host
