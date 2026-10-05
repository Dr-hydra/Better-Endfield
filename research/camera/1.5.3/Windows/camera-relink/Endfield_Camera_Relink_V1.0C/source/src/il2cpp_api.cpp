#include "il2cpp_api.h"
#include <windows.h>
#include <cstring>

namespace ecl {

namespace {
HMODULE GameAssembly() {
    return GetModuleHandleA("GameAssembly.dll");
}

// 尝试解析一个导出; 失败时记录缺失名(仅第一个)
template <typename T>
bool TryExport(const char* name, T& out, const char*& firstMissing) {
    HMODULE m = GameAssembly();
    if (!m) {
        if (!firstMissing) firstMissing = "GameAssembly.dll(模块未加载)";
        out = nullptr;
        return false;
    }
    out = reinterpret_cast<T>(GetProcAddress(m, name));
    if (!out) {
        if (!firstMissing) firstMissing = name;
        return false;
    }
    return true;
}
}  // namespace

const Il2CppApi& Il2Cpp() {
    static Il2CppApi api;
    if (api.loaded) return api;
    const char* missing = nullptr;
    Il2CppApi fresh{};
    TryExport("il2cpp_domain_get", fresh.domain_get, missing);
    TryExport("il2cpp_domain_get_assemblies", fresh.domain_get_assemblies, missing);
    TryExport("il2cpp_assembly_get_image", fresh.assembly_get_image, missing);
    TryExport("il2cpp_image_get_name", fresh.image_get_name, missing);
    TryExport("il2cpp_class_from_name", fresh.class_from_name, missing);
    TryExport("il2cpp_class_get_method_from_name",
              fresh.class_get_method_from_name, missing);
    TryExport("il2cpp_runtime_invoke", fresh.runtime_invoke, missing);
    TryExport("il2cpp_object_unbox", fresh.object_unbox, missing);
    TryExport("il2cpp_thread_attach", fresh.thread_attach, missing);
    // 枚举 API: 缺失不致命(仅影响符号清单功能)
    const char* ignored = nullptr;
    TryExport("il2cpp_image_get_class_count", fresh.image_get_class_count, ignored);
    TryExport("il2cpp_image_get_class", fresh.image_get_class, ignored);
    TryExport("il2cpp_class_get_name", fresh.class_get_name, ignored);
    TryExport("il2cpp_class_get_namespace", fresh.class_get_namespace, ignored);
    TryExport("il2cpp_class_get_methods", fresh.class_get_methods, ignored);
    TryExport("il2cpp_method_get_name", fresh.method_get_name, ignored);
    TryExport("il2cpp_method_get_param_count", fresh.method_get_param_count, ignored);
    TryExport("il2cpp_class_get_fields", fresh.class_get_fields, ignored);
    TryExport("il2cpp_field_get_name", fresh.field_get_name, ignored);
    TryExport("il2cpp_class_get_field_from_name", fresh.class_get_field_from_name, ignored);
    TryExport("il2cpp_field_set_value", fresh.field_set_value, ignored);
    TryExport("il2cpp_field_get_value", fresh.field_get_value, ignored);
    TryExport("il2cpp_class_get_type", fresh.class_get_type, ignored);
    TryExport("il2cpp_object_get_class", fresh.object_get_class, ignored);
    TryExport("il2cpp_field_get_type", fresh.field_get_type, ignored);
    TryExport("il2cpp_type_get_name", fresh.type_get_name, ignored);
    TryExport("il2cpp_object_new", fresh.object_new, ignored);
    TryExport("il2cpp_class_get_parent", fresh.class_get_parent, ignored);
    TryExport("il2cpp_class_get_property_from_name",
              fresh.class_get_property_from_name, ignored);
    TryExport("il2cpp_property_get_get_method", fresh.property_get_get_method, ignored);
    TryExport("il2cpp_property_get_set_method", fresh.property_get_set_method, ignored);
    TryExport("il2cpp_class_from_type", fresh.class_from_type, ignored);
    TryExport("il2cpp_class_is_valuetype", fresh.class_is_valuetype, ignored);
    TryExport("il2cpp_class_value_size", fresh.class_value_size, ignored);
    TryExport("il2cpp_field_get_offset", fresh.field_get_offset, ignored);
    TryExport("il2cpp_method_get_param", fresh.method_get_param, ignored);
    TryExport("il2cpp_method_get_return_type", fresh.method_get_return_type, ignored);
    // v1.0.0ai: 可选(缺失不致命, 仅 UI 探测降级)
    TryExport("il2cpp_array_length", fresh.array_length, ignored);
    TryExport("il2cpp_array_addr_with_size", fresh.array_addr, ignored);
    TryExport("il2cpp_string_length", fresh.string_length, ignored);
    TryExport("il2cpp_string_chars", fresh.string_chars, ignored);
    TryExport("il2cpp_string_new", fresh.string_new, ignored);
    fresh.loaded = fresh.domain_get && fresh.domain_get_assemblies &&
                   fresh.assembly_get_image && fresh.image_get_name &&
                   fresh.class_from_name && fresh.class_get_method_from_name &&
                   fresh.runtime_invoke && fresh.object_unbox;
    fresh.firstMissing = missing;
    api = fresh;
    return api;
}

void* InvokeMethod(void* methodInfo, void* obj, void** params) {
    const Il2CppApi& a = Il2Cpp();
    if (!a.loaded || !methodInfo) return nullptr;
    // 关键: 托管调用前确保当前线程已附加到 IL2CPP 域,
    // 否则在非 Unity 线程分配托管对象会触发 "Collecting from unknown thread" 致命错误
    if (a.thread_attach && a.domain_get) {
        void* domain = a.domain_get();
        if (domain) a.thread_attach(domain);
    }
    void* exc = nullptr;
    void* result = a.runtime_invoke(methodInfo, obj, params, &exc);
    return exc ? nullptr : result;
}

}  // namespace ecl
