#pragma once
#include <dlfcn.h>
#include <link.h>
#include <cstring>
namespace betterendfieldnext {
inline void* OpenLoadedIl2Cpp() {
    if (void* image = dlopen("libil2cpp.so", RTLD_NOLOAD | RTLD_NOW)) return image;
    void* image = nullptr;
    dl_iterate_phdr([](dl_phdr_info* info, size_t, void* context) {
        const char* path = info->dlpi_name;
        if (!path || !*path) return 0;
        const char* slash = std::strrchr(path, '/');
        const char* name = slash ? slash + 1 : path;
        if (std::strcmp(name, "libil2cpp.so") != 0) return 0;
        auto*& result = *static_cast<void**>(context);
        result = dlopen(path, RTLD_NOLOAD | RTLD_NOW);
        return result ? 1 : 0;
    }, &image);
    return image;
}
}
