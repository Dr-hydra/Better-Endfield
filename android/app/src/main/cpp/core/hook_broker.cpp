#include "core/hook_broker.h"

#include <dobby.h>

namespace betterendfield {

bool HookBroker::Initialize(std::string& error) {
    (void)error;
    return true;
}

bool HookBroker::Install(
    void* target,
    void* replacement,
    void** original,
    void*& stub,
    std::string& error) {
    if (target == nullptr || replacement == nullptr || original == nullptr) {
        error = "invalid native hook request";
        return false;
    }
    const int result = DobbyHook(target, replacement, original);
    if (result == 0) {
        stub = target;
        return true;
    }
    error = "Dobby native hook failed: " + std::to_string(result);
    return false;
}

void HookBroker::Remove(void*& stub) {
    if (stub == nullptr) {
        return;
    }
    DobbyDestroy(stub);
    stub = nullptr;
}

}  // namespace betterendfield
