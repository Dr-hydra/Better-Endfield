#pragma once
// 从 GameAssembly.dll (IL2CPP) 动态解析 il2cpp_* API。不硬编码任何地址。
// 注意: 解析支持重复调用(游戏启动早期 GameAssembly 尚未加载, 需重试直到 loaded)。
#include <cstdint>

namespace ecl {

struct Il2CppApi {
    void* (*domain_get)() = nullptr;
    void** (*domain_get_assemblies)(void* domain, void** size) = nullptr;
    void* (*assembly_get_image)(void* assembly) = nullptr;
    const char* (*image_get_name)(void* image) = nullptr;
    void* (*class_from_name)(void* image, const char* ns, const char* name) = nullptr;
    void* (*class_get_method_from_name)(void* klass, const char* name, int argCount) = nullptr;
    void* (*runtime_invoke)(void* method, void* obj, void** params, void** exc) = nullptr;
    void* (*object_unbox)(void* obj) = nullptr;
    void* (*thread_attach)(void* domain) = nullptr;
    // 运行时枚举(符号清单用, 缺失不致命)
    uint32_t (*image_get_class_count)(void* image) = nullptr;
    void* (*image_get_class)(void* image, uint32_t index) = nullptr;
    const char* (*class_get_name)(void* klass) = nullptr;
    const char* (*class_get_namespace)(void* klass) = nullptr;
    void* (*class_get_methods)(void* klass, void** iter) = nullptr;
    const char* (*method_get_name)(void* method) = nullptr;
    uint32_t (*method_get_param_count)(void* method) = nullptr;
    void* (*class_get_fields)(void* klass, void** iter) = nullptr;
    const char* (*field_get_name)(void* field) = nullptr;
    void* (*class_get_field_from_name)(void* klass, const char* name) = nullptr;
    void (*field_set_value)(void* field, void* obj, void* value) = nullptr;
    void (*field_get_value)(void* field, void* obj, void* value) = nullptr;
    void* (*class_get_type)(void* klass) = nullptr;  // 供 FindObjectOfType 等使用
    // 运行时对象/字段反射(v2: 用于转储 CameraManager 字段、定位 DOF 组件)
    void* (*object_get_class)(void* obj) = nullptr;
    void* (*field_get_type)(void* field) = nullptr;
    char* (*type_get_name)(void* type) = nullptr;
    void* (*object_new)(void* klass) = nullptr;
    void* (*class_get_parent)(void* klass) = nullptr;
    void* (*class_get_property_from_name)(void* klass, const char* name) = nullptr;
    void* (*property_get_get_method)(void* prop) = nullptr;
    void* (*property_get_set_method)(void* prop) = nullptr;
    // 字段尺寸/类型判定(v3: 防止按固定缓冲区写越界 —— 上次崩溃的根因)
    void* (*class_from_type)(void* type) = nullptr;
    bool (*class_is_valuetype)(void* klass) = nullptr;
    int32_t (*class_value_size)(void* klass, uint32_t* align) = nullptr;
    size_t (*field_get_offset)(void* field) = nullptr;
    // 方法签名自省(只读元数据): 用来确认参数类型, 避免按错误类型调用托管方法
    void* (*method_get_param)(void* method, uint32_t index) = nullptr;
    void* (*method_get_return_type)(void* method) = nullptr;
    // v1.0.0ai(隐藏 UI 探测): 可选导出 —— 数组长度/元素寻址 与 托管字符串读取。
    //   只在 UI 体检(枚举 UnityEngine.Camera.allCameras / 打印相机名)里用;
    //   任一缺失时对应子探测自动降级为"不可用", 绝不做裸内存硬编码偏移的替代实现。
    uint32_t (*array_length)(void* arr) = nullptr;
    char* (*array_addr)(void* arr, int32_t elemSize, uintptr_t index) = nullptr;
    int32_t (*string_length)(void* str) = nullptr;
    uint16_t* (*string_chars)(void* str) = nullptr;
    // v1.0.0aj: 构造托管字符串 —— 给 AddUICamCullingMaskConfig(System.String, System.Int32)
    // 这类"按名字登记配置"的接口传参用。缺失时该通道降级为"不可用"(不猜内存布局)。
    void* (*string_new)(const char* utf8) = nullptr;
    bool loaded = false;
    const char* firstMissing = nullptr;  // 诊断: 第一个缺失的导出名
};

// 统一的 runtime_invoke 包装(异常时返回 nullptr)
void* InvokeMethod(void* methodInfo, void* obj, void** params);

// 每次调用都会尝试解析(未成功时), 成功后退化为常量返回。
const Il2CppApi& Il2Cpp();

}  // namespace ecl
