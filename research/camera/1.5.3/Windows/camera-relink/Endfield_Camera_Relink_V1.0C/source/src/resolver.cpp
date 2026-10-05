#include "resolver.h"
#include "il2cpp_api.h"
#include <windows.h>
#include <cstring>

namespace ecl {

namespace {
bool ImageNameIs(void* image, const char* want) {
    const char* name = Il2Cpp().image_get_name(image);
    return name && _stricmp(name, want) == 0;
}

// IL2CPP MethodInfo 布局(社区逆向结论, Unity 2021 x64):
//   偏移 0x00 即 methodPointer(原生函数指针)。直接复用该 ABI, 不做字段扫描。
void* MethodPointerFromInfo(void* methodInfo) {
    return *(reinterpret_cast<void**>(methodInfo));
}

bool InModuleRange(void* ptr, const char* moduleName) {
    HMODULE m = GetModuleHandleA(moduleName);
    if (!m) return false;
    auto* dos = reinterpret_cast<const IMAGE_DOS_HEADER*>(m);
    auto* nt = reinterpret_cast<const IMAGE_NT_HEADERS*>(
        reinterpret_cast<const BYTE*>(m) + dos->e_lfanew);
    const uintptr_t lo = reinterpret_cast<uintptr_t>(m);
    const uintptr_t hi = lo + nt->OptionalHeader.SizeOfImage;
    const uintptr_t p = reinterpret_cast<uintptr_t>(ptr);
    return p >= lo && p < hi;
}
}  // namespace

namespace {
const char* g_lastAssembly = nullptr;
}

MethodHandle ResolveMethod(const char* assemblyName, const char* ns,
                           const char* cls, const char* method, int argCount) {
    MethodHandle out;
    const Il2CppApi& a = Il2Cpp();
    if (!a.loaded) return out;

    void* domain = a.domain_get();
    if (!domain) return out;
    void* size = nullptr;
    void** assemblies = a.domain_get_assemblies(domain, &size);
    if (!assemblies) return out;
    const size_t count = reinterpret_cast<size_t>(size);

    for (size_t i = 0; i < count; ++i) {
        void* image = a.assembly_get_image(assemblies[i]);
        if (!image) continue;
        // assemblyName == nullptr 表示跨程序集查找
        if (assemblyName && !ImageNameIs(image, assemblyName)) continue;
        void* klass = a.class_from_name(image, ns, cls);
        if (!klass) continue;
        void* methodInfo = a.class_get_method_from_name(klass, method, argCount);
        if (!methodInfo) continue;
        void* pointer = MethodPointerFromInfo(methodInfo);
        // 安全校验: 原生指针必须落在 GameAssembly.dll 镜像内
        if (!InModuleRange(pointer, "GameAssembly.dll")) continue;
        g_lastAssembly = a.image_get_name(image);
        out.pointer = pointer;
        out.methodInfo = methodInfo;
        out.ok = true;
        return out;
    }
    return out;
}

MethodHandle ResolveMethodAnywhere(const char* ns, const char* cls,
                                   const char* method, int argCount) {
    g_lastAssembly = nullptr;
    return ResolveMethod(nullptr, ns, cls, method, argCount);
}

const char* LastResolvedAssembly() { return g_lastAssembly; }

void* ResolveClass(const char* assemblyName, const char* ns, const char* cls) {
    const Il2CppApi& a = Il2Cpp();
    if (!a.loaded) return nullptr;
    void* domain = a.domain_get();
    if (!domain) return nullptr;
    void* size = nullptr;
    void** assemblies = a.domain_get_assemblies(domain, &size);
    if (!assemblies) return nullptr;
    const size_t count = reinterpret_cast<size_t>(size);
    for (size_t i = 0; i < count; ++i) {
        void* image = a.assembly_get_image(assemblies[i]);
        if (!image) continue;
        if (assemblyName && !ImageNameIs(image, assemblyName)) continue;
        void* klass = a.class_from_name(image, ns, cls);
        if (klass) {
            g_lastAssembly = a.image_get_name(image);
            return klass;
        }
    }
    return nullptr;
}

void* ResolveClassAnywhere(const char* ns, const char* cls) {
    g_lastAssembly = nullptr;
    return ResolveClass(nullptr, ns, cls);
}

}  // namespace ecl
