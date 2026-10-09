#pragma once

#include <string>

namespace betterendfieldnext {

class Il2CppRuntime;

struct ModuleResult {
    bool active = false;
    std::string message;
};

class Module {
public:
    virtual ~Module() = default;
    virtual const char* Id() const = 0;
    virtual ModuleResult Start(Il2CppRuntime& runtime) = 0;
};

}  // namespace betterendfieldnext
