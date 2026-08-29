#include "runtime.h"

#include <dlfcn.h>

#include <cstdint>
#include <cstdio>
#include <cstring>
#include <string>
#include <string_view>
#include <vector>

namespace betterendfield {
namespace {

struct Il2CppMethodInfoPrefix {
    void* method_pointer;
};

template <typename Function>
Function ResolveExport(void* library, const char* name) {
    return reinterpret_cast<Function>(dlsym(library, name));
}

bool IsExecutableAddress(const void* pointer) {
    const uintptr_t address = reinterpret_cast<uintptr_t>(pointer);
    FILE* maps = std::fopen("/proc/self/maps", "r");
    if (maps == nullptr) {
        return false;
    }

    char line[512]{};
    bool executable = false;
    while (std::fgets(line, sizeof(line), maps) != nullptr) {
        uintptr_t start = 0;
        uintptr_t end = 0;
        char permissions[5]{};
        if (std::sscanf(line, "%lx-%lx %4s", &start, &end, permissions) == 3 &&
            address >= start && address < end) {
            executable = permissions[2] == 'x';
            break;
        }
    }
    std::fclose(maps);
    return executable;
}

void AppendUtf8(std::string& output, uint32_t codepoint) {
    if (codepoint <= 0x7f) {
        output.push_back(static_cast<char>(codepoint));
    } else if (codepoint <= 0x7ff) {
        output.push_back(static_cast<char>(0xc0 | (codepoint >> 6)));
        output.push_back(static_cast<char>(0x80 | (codepoint & 0x3f)));
    } else if (codepoint <= 0xffff) {
        output.push_back(static_cast<char>(0xe0 | (codepoint >> 12)));
        output.push_back(static_cast<char>(0x80 | ((codepoint >> 6) & 0x3f)));
        output.push_back(static_cast<char>(0x80 | (codepoint & 0x3f)));
    } else {
        output.push_back(static_cast<char>(0xf0 | (codepoint >> 18)));
        output.push_back(static_cast<char>(0x80 | ((codepoint >> 12) & 0x3f)));
        output.push_back(static_cast<char>(0x80 | ((codepoint >> 6) & 0x3f)));
        output.push_back(static_cast<char>(0x80 | (codepoint & 0x3f)));
    }
}

std::vector<std::string_view> SplitTypes(const char* value) {
    std::vector<std::string_view> result;
    if (value == nullptr || *value == '\0') {
        return result;
    }
    std::string_view remaining(value);
    while (true) {
        const size_t separator = remaining.find('|');
        result.push_back(remaining.substr(0, separator));
        if (separator == std::string_view::npos) {
            break;
        }
        remaining.remove_prefix(separator + 1);
    }
    return result;
}

}  // namespace

Il2CppRuntime::~Il2CppRuntime() {
    if (library_ != nullptr) {
        dlclose(library_);
    }
}

bool Il2CppRuntime::Connect() {
    if (library_ != nullptr) {
        return true;
    }

    void* library = dlopen("libil2cpp.so", RTLD_NOW | RTLD_NOLOAD);
    if (library == nullptr) {
        return false;
    }

    const auto domain_get = ResolveExport<decltype(domain_get_)>(library, "il2cpp_domain_get");
    const auto thread_attach = ResolveExport<decltype(thread_attach_)>(library, "il2cpp_thread_attach");
    const auto thread_detach = ResolveExport<decltype(thread_detach_)>(library, "il2cpp_thread_detach");
    const auto domain_assembly_open = ResolveExport<decltype(domain_assembly_open_)>(
        library, "il2cpp_domain_assembly_open");
    const auto assembly_get_image = ResolveExport<decltype(assembly_get_image_)>(
        library, "il2cpp_assembly_get_image");
    const auto class_from_name = ResolveExport<decltype(class_from_name_)>(
        library, "il2cpp_class_from_name");
    const auto class_get_method_from_name = ResolveExport<decltype(class_get_method_from_name_)>(
        library, "il2cpp_class_get_method_from_name");
    const auto class_get_methods = ResolveExport<decltype(class_get_methods_)>(
        library, "il2cpp_class_get_methods");
    const auto method_get_name = ResolveExport<decltype(method_get_name_)>(
        library, "il2cpp_method_get_name");
    const auto method_get_parameter_count = ResolveExport<decltype(method_get_parameter_count_)>(
        library, "il2cpp_method_get_param_count");
    const auto method_get_parameter = ResolveExport<decltype(method_get_parameter_)>(
        library, "il2cpp_method_get_param");
    const auto method_get_return_type = ResolveExport<decltype(method_get_return_type_)>(
        library, "il2cpp_method_get_return_type");
    const auto type_get_name = ResolveExport<decltype(type_get_name_)>(
        library, "il2cpp_type_get_name");
    const auto class_get_type = ResolveExport<decltype(class_get_type_)>(
        library, "il2cpp_class_get_type");
    const auto type_get_object = ResolveExport<decltype(type_get_object_)>(
        library, "il2cpp_type_get_object");
    const auto class_get_field_from_name = ResolveExport<decltype(class_get_field_from_name_)>(
        library, "il2cpp_class_get_field_from_name");
    const auto field_get_offset = ResolveExport<decltype(field_get_offset_)>(
        library, "il2cpp_field_get_offset");
    const auto field_get_value_object = ResolveExport<decltype(field_get_value_object_)>(
        library, "il2cpp_field_get_value_object");
    const auto string_chars = ResolveExport<decltype(string_chars_)>(
        library, "il2cpp_string_chars");
    const auto string_length = ResolveExport<decltype(string_length_)>(
        library, "il2cpp_string_length");
    const auto string_new = ResolveExport<decltype(string_new_)>(
        library, "il2cpp_string_new");
    const auto object_new = ResolveExport<decltype(object_new_)>(
        library, "il2cpp_object_new");
    const auto object_unbox = ResolveExport<decltype(object_unbox_)>(
        library, "il2cpp_object_unbox");
    const auto gchandle_new = ResolveExport<decltype(gchandle_new_)>(
        library, "il2cpp_gchandle_new");
    const auto gchandle_free = ResolveExport<decltype(gchandle_free_)>(
        library, "il2cpp_gchandle_free");
    const auto runtime_invoke = ResolveExport<decltype(runtime_invoke_)>(
        library, "il2cpp_runtime_invoke");

    if (domain_get == nullptr || thread_attach == nullptr || thread_detach == nullptr ||
        domain_assembly_open == nullptr || assembly_get_image == nullptr ||
        class_from_name == nullptr || class_get_method_from_name == nullptr ||
        class_get_methods == nullptr || method_get_name == nullptr ||
        method_get_parameter_count == nullptr || method_get_parameter == nullptr ||
        method_get_return_type == nullptr || type_get_name == nullptr ||
        class_get_type == nullptr || type_get_object == nullptr ||
        class_get_field_from_name == nullptr || field_get_offset == nullptr ||
        field_get_value_object == nullptr ||
        string_chars == nullptr || string_length == nullptr || string_new == nullptr ||
        object_new == nullptr || object_unbox == nullptr ||
        gchandle_new == nullptr || gchandle_free == nullptr ||
        runtime_invoke == nullptr) {
        dlclose(library);
        return false;
    }

    library_ = library;
    domain_get_ = domain_get;
    thread_attach_ = thread_attach;
    thread_detach_ = thread_detach;
    domain_assembly_open_ = domain_assembly_open;
    assembly_get_image_ = assembly_get_image;
    class_from_name_ = class_from_name;
    class_get_method_from_name_ = class_get_method_from_name;
    class_get_methods_ = class_get_methods;
    method_get_name_ = method_get_name;
    method_get_parameter_count_ = method_get_parameter_count;
    method_get_parameter_ = method_get_parameter;
    method_get_return_type_ = method_get_return_type;
    type_get_name_ = type_get_name;
    class_get_type_ = class_get_type;
    type_get_object_ = type_get_object;
    class_get_field_from_name_ = class_get_field_from_name;
    field_get_offset_ = field_get_offset;
    field_get_value_object_ = field_get_value_object;
    string_chars_ = string_chars;
    string_length_ = string_length;
    string_new_ = string_new;
    object_new_ = object_new;
    object_unbox_ = object_unbox;
    gchandle_new_ = gchandle_new;
    gchandle_free_ = gchandle_free;
    runtime_invoke_ = runtime_invoke;
    return true;
}

std::string Il2CppRuntime::CopyString(void* value) const {
    if (value == nullptr || string_chars_ == nullptr || string_length_ == nullptr) {
        return {};
    }
    const char16_t* chars = string_chars_(value);
    const int32_t length = string_length_(value);
    if (chars == nullptr || length <= 0 || length > 32768) {
        return {};
    }

    std::string output;
    output.reserve(static_cast<size_t>(length));
    for (int32_t index = 0; index < length; ++index) {
        uint32_t codepoint = chars[index];
        if (codepoint >= 0xd800 && codepoint <= 0xdbff && index + 1 < length) {
            const uint32_t low = chars[index + 1];
            if (low >= 0xdc00 && low <= 0xdfff) {
                codepoint = 0x10000 + ((codepoint - 0xd800) << 10) +
                    (low - 0xdc00);
                ++index;
            }
        }
        AppendUtf8(output, codepoint);
    }
    return output;
}

void* Il2CppRuntime::NewString(const char* value) const {
    return value == nullptr || string_new_ == nullptr ? nullptr : string_new_(value);
}

void* Il2CppRuntime::NewObject(const Il2CppClass* klass) const {
    return klass == nullptr || object_new_ == nullptr ? nullptr : object_new_(klass);
}

void* Il2CppRuntime::Unbox(void* value) const {
    return value == nullptr || object_unbox_ == nullptr ? nullptr : object_unbox_(value);
}

uint32_t Il2CppRuntime::NewGcHandle(void* value, bool pinned) const {
    return value == nullptr || gchandle_new_ == nullptr
        ? 0 : gchandle_new_(value, pinned);
}

void Il2CppRuntime::FreeGcHandle(uint32_t handle) const {
    if (handle != 0 && gchandle_free_ != nullptr) {
        gchandle_free_(handle);
    }
}

void* Il2CppRuntime::Invoke(
    const MethodInfo* method,
    void* instance,
    void** parameters,
    void** exception) const {
    if (exception != nullptr) {
        *exception = nullptr;
    }
    return method == nullptr || runtime_invoke_ == nullptr
        ? nullptr
        : runtime_invoke_(method, instance, parameters, exception);
}

void* Il2CppRuntime::AttachCurrentThread() const {
    Il2CppDomain* domain = domain_get_ == nullptr ? nullptr : domain_get_();
    return domain == nullptr ? nullptr : thread_attach_(domain);
}

void Il2CppRuntime::DetachCurrentThread(void* thread) const {
    if (thread != nullptr && thread_detach_ != nullptr) {
        thread_detach_(thread);
    }
}

ResolvedMethod Il2CppRuntime::ResolveMethod(
    const char* assembly,
    const char* namespaze,
    const char* klass,
    const char* method,
    int parameter_count) const {
    if (library_ == nullptr) {
        return {};
    }

    Il2CppDomain* domain = domain_get_();
    const Il2CppAssembly* target_assembly = domain == nullptr
        ? nullptr
        : domain_assembly_open_(domain, assembly);
    const Il2CppImage* image = target_assembly == nullptr
        ? nullptr
        : assembly_get_image_(target_assembly);
    Il2CppClass* target_class = image == nullptr
        ? nullptr
        : class_from_name_(image, namespaze, klass);
    const MethodInfo* info = target_class == nullptr
        ? nullptr
        : class_get_method_from_name_(target_class, method, parameter_count);
    if (info == nullptr) {
        return {};
    }

    const auto* prefix = reinterpret_cast<const Il2CppMethodInfoPrefix*>(info);
    void* entry = prefix->method_pointer;
    return IsExecutableAddress(entry) ? ResolvedMethod{info, entry} : ResolvedMethod{};
}

ResolvedMethod Il2CppRuntime::ResolveMethodExact(
    const char* assembly,
    const char* namespaze,
    const char* klass,
    const char* method,
    const char* parameter_types,
    const char* return_type,
    int parameter_count) const {
    if (library_ == nullptr || method == nullptr) {
        return {};
    }
    const ResolvedClass target = ResolveClass(assembly, namespaze, klass);
    if (target.info == nullptr) {
        return {};
    }
    const std::vector<std::string_view> expected_parameters = SplitTypes(parameter_types);
    if (!expected_parameters.empty() &&
        expected_parameters.size() != static_cast<size_t>(parameter_count)) {
        return {};
    }
    void* iterator = nullptr;
    while (const MethodInfo* candidate = class_get_methods_(target.info, &iterator)) {
        const char* candidate_name = method_get_name_(candidate);
        if (candidate_name == nullptr || std::strcmp(candidate_name, method) != 0 ||
            method_get_parameter_count_(candidate) != static_cast<uint32_t>(parameter_count)) {
            continue;
        }
        bool matches = true;
        for (uint32_t index = 0; index < expected_parameters.size(); ++index) {
            char* actual = type_get_name_(method_get_parameter_(candidate, index));
            if (actual == nullptr || expected_parameters[index] != actual) {
                matches = false;
                break;
            }
        }
        if (matches && return_type != nullptr && *return_type != '\0') {
            char* actual = type_get_name_(method_get_return_type_(candidate));
            matches = actual != nullptr && std::string_view(actual) == return_type;
        }
        if (!matches) {
            continue;
        }
        void* entry = reinterpret_cast<const Il2CppMethodInfoPrefix*>(candidate)->method_pointer;
        return IsExecutableAddress(entry) ? ResolvedMethod{candidate, entry} : ResolvedMethod{};
    }
    return {};
}

ResolvedClass Il2CppRuntime::ResolveClass(
    const char* assembly,
    const char* namespaze,
    const char* klass) const {
    if (library_ == nullptr) {
        return {};
    }
    Il2CppDomain* domain = domain_get_();
    const Il2CppAssembly* target_assembly = domain == nullptr
        ? nullptr : domain_assembly_open_(domain, assembly);
    const Il2CppImage* image = target_assembly == nullptr
        ? nullptr : assembly_get_image_(target_assembly);
    Il2CppClass* target_class = image == nullptr
        ? nullptr : class_from_name_(image, namespaze, klass);
    const Il2CppType* type = target_class == nullptr
        ? nullptr : class_get_type_(target_class);
    void* type_object = type == nullptr ? nullptr : type_get_object_(type);
    return {target_class, type, type_object};
}

ResolvedField Il2CppRuntime::ResolveField(
    const char* assembly,
    const char* namespaze,
    const char* klass,
    const char* field) const {
    if (library_ == nullptr) {
        return {};
    }
    Il2CppDomain* domain = domain_get_();
    const Il2CppAssembly* target_assembly = domain == nullptr
        ? nullptr : domain_assembly_open_(domain, assembly);
    const Il2CppImage* image = target_assembly == nullptr
        ? nullptr : assembly_get_image_(target_assembly);
    Il2CppClass* target_class = image == nullptr
        ? nullptr : class_from_name_(image, namespaze, klass);
    const FieldInfo* info = target_class == nullptr
        ? nullptr : class_get_field_from_name_(target_class, field);
    if (info == nullptr) {
        return {};
    }
    const size_t offset = field_get_offset_(info);
    return offset > static_cast<size_t>(INT32_MAX)
        ? ResolvedField{} : ResolvedField{info, static_cast<int32_t>(offset)};
}

void* Il2CppRuntime::ReadFieldObject(
    const ResolvedField& field, void* instance) const {
    return field.info == nullptr || instance == nullptr ||
        field_get_value_object_ == nullptr
        ? nullptr : field_get_value_object_(field.info, instance);
}

void* Il2CppRuntime::ReadFieldObject(const FieldInfo* field, void* instance) const {
    return field == nullptr || instance == nullptr || field_get_value_object_ == nullptr
        ? nullptr : field_get_value_object_(field, instance);
}

Il2CppThreadScope::Il2CppThreadScope(const Il2CppRuntime& runtime)
    : runtime_(runtime), thread_(runtime.AttachCurrentThread()) {}

Il2CppThreadScope::~Il2CppThreadScope() {
    runtime_.DetachCurrentThread(thread_);
}

}  // namespace betterendfield
