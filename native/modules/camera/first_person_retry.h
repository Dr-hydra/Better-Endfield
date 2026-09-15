#pragma once

#include <unordered_map>

namespace BetterEndfield::FirstPerson {
// The same source mesh can be shared by multiple renderers. Each binding gets
// its own retry budget, renewed when the source or a bound patch changes.
class RetryBudget {
    struct Entry { void* source = nullptr; int count = 0; };
    std::unordered_map<void*, Entry> entries;

public:
    bool Try(void* renderer, void* source) {
        auto& entry = entries[renderer];
        if (entry.source != source) entry = {source, 0};
        if (entry.count >= 2) return false;
        ++entry.count;
        return true;
    }

    void Forget(void* renderer) { entries.erase(renderer); }
    void Clear() { entries.clear(); }

    template<class IsPresent>
    void Prune(IsPresent present) {
        for (auto it = entries.begin(); it != entries.end();) {
            if (!present(it->first)) it = entries.erase(it);
            else ++it;
        }
    }
};
}
