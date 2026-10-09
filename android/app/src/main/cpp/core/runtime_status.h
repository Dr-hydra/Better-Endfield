#pragma once
#include <map>
#include <mutex>
#include <string>
namespace betterendfieldnext {
class RuntimeStatus final {
public:
    void Set(const std::string& id, const std::string& state) {
        std::lock_guard lock(mutex_);
        states_[id] = state;
    }
    std::string Copy() const {
        std::lock_guard lock(mutex_);
        std::string result = "BE_RUNTIME_V1\n";
        for (const auto& [id, state] : states_) result += id + "=" + state + "\n";
        return result;
    }
private:
    mutable std::mutex mutex_;
    std::map<std::string, std::string> states_;
};
}
