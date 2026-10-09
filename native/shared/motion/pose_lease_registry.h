#pragma once
#include <cstdint>
#include <limits>
#include <map>
#include <mutex>
#include <string>
#include <string_view>
namespace BetterEndfieldNext::Motion {
class PoseLeaseRegistry {
public:
    uint64_t Acquire(const void* root,std::string_view owner) {
        if(!root || owner.empty() || owner.size()>96) return 0;
        std::lock_guard lock(mutex_);
        if(entries_.contains(root) || entries_.size()>=64 || serial_==UINT64_MAX) return 0;
        auto token=++serial_; entries_.emplace(root,Entry{std::string(owner),token});return token;
    }
    bool Owns(const void* root,std::string_view owner,uint64_t token) {
        std::lock_guard lock(mutex_);return Matches(root,owner,token);
    }
    bool Release(const void* root,std::string_view owner,uint64_t token) {
        std::lock_guard lock(mutex_);
        if(!Matches(root,owner,token))return false;
        entries_.erase(root);return true;
    }
private:
    bool Matches(const void* root,std::string_view owner,uint64_t token)const {
        auto it=entries_.find(root);
        return token && it!=entries_.end() && it->second.owner==owner && it->second.token==token;
    }
    struct Entry {std::string owner;uint64_t token;};
    std::mutex mutex_;
    std::map<const void*,Entry> entries_;
    uint64_t serial_=0;
};
}
