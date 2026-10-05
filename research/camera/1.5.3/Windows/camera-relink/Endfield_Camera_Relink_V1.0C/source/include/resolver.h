#pragma once
// 目标方法句柄与解析
#include <cstdint>

namespace ecl {

struct MethodHandle {
    void* pointer = nullptr;     // 原生函数指针(校验过位于 GameAssembly 可执行段)
    void* methodInfo = nullptr;  // MethodInfo*, 供 runtime_invoke
    bool ok = false;
};

// 按 程序集名/命名空间/类/方法名/参数个数 解析。
// 校验 methodPointer 落在 GameAssembly.dll 的 PE 可执行区段(镜像内, 非导入表等)。
MethodHandle ResolveMethod(const char* assemblyName, const char* ns,
                           const char* cls, const char* method, int argCount);

// 解析类(供 FindObjectOfType 等需要 Il2CppClass*/Type* 的场景)
void* ResolveClass(const char* assemblyName, const char* ns, const char* cls);

// 跨程序集解析: 不知道目标类在哪个程序集时用(例如 Cinemachine 的实现位置未知)。
// 只读元数据, 安全。
MethodHandle ResolveMethodAnywhere(const char* ns, const char* cls,
                                  const char* method, int argCount);
void* ResolveClassAnywhere(const char* ns, const char* cls);
// 上一步跨程序集查找命中的程序集名(诊断用, 可能为 nullptr)
const char* LastResolvedAssembly();

}  // namespace ecl
