#pragma once
#include <atomic>
#include <cstdint>
#include <mutex>
#include <unordered_set>

namespace BetterEndfieldNext::Music {
class BgmPauseRequests {
    std::mutex mutex_;
    std::unordered_set<uint64_t> tokens_;
    uint64_t next_ = 0;
    bool accepting_ = false;
    std::atomic_bool active_{false};
public:
    void Start() { std::lock_guard lock(mutex_); tokens_.clear(); active_ = false; accepting_ = true; }
    void Stop() { std::lock_guard lock(mutex_); accepting_ = false; tokens_.clear(); active_ = false; }
    uint64_t Acquire() {
        std::lock_guard lock(mutex_);
        if (!accepting_ || next_ == UINT64_MAX) return 0;
        const auto token = ++next_;
        tokens_.insert(token); active_ = true; return token;
    }
    void Release(uint64_t token) {
        std::lock_guard lock(mutex_);
        tokens_.erase(token); active_ = !tokens_.empty();
    }
    bool Active() const { return active_.load(); }
};
}
