#pragma once

#include <string>

namespace betterendfield {

class HookBroker final {
public:
    bool Initialize(std::string& error);
    bool Install(
        void* target,
        void* replacement,
        void** original,
        void*& stub,
        std::string& error);
    void Remove(void*& stub);
};

}  // namespace betterendfield
