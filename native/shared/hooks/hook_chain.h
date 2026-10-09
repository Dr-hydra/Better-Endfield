#pragma once
#include "BetterEndfieldNext/HookChain.h"
#include <atomic>
#include <cstring>
#include <functional>
#include <memory>
#include <mutex>
#include <string>
#include <unordered_map>
#include <vector>
#if defined(_WIN32)
#include <Windows.h>
#else
#include <sys/mman.h>
#include <unistd.h>
#endif

namespace BetterEndfieldNext::Hooks {
// RX instructions never change. Only a separate aligned, lock-free RW pointer
// changes. Relays remain alive until process exit, including a disabled next
// retained by a callback which entered before its module was stopped.
struct Relay {
    void* code=nullptr;
    std::atomic<void*>* destination=nullptr;
    // Tracked relays only: the relay code stores 1 here on its first dispatch.
    std::atomic<uint8_t>* hit=nullptr;
    void Set(void* value) const {destination->store(value,std::memory_order_release);}
    explicit operator bool() const {return code&&destination;}
};
class RelayPool {
public:
    Relay Make() {return Make(false);}
    // Same forwarding contract as Make(), plus a hit flag that tells the Host
    // whether anything ever entered the patched target. x64 only; elsewhere it
    // returns an empty relay and callers fall back to Make().
    Relay MakeTracked() {return Make(true);}
    static RelayPool& Instance() {static auto* pool=new RelayPool;return *pool;}
private:
    Relay Make(bool tracked) {
#if !defined(_M_X64)&&!defined(__x86_64__)
        if(tracked)return {};
#endif
        std::lock_guard lock(mutex_);
        if(issued_>=32768)return {};
        auto& pages=tracked?tracked_pages_:pages_;
        const size_t slot=tracked?64:32;
        if(pages.empty()||pages.back()->used==pages.back()->count) {
            auto page=std::make_unique<Page>();
#if defined(_WIN32)
            SYSTEM_INFO info{};GetSystemInfo(&info);page->size=info.dwPageSize;
            page->code=VirtualAlloc(nullptr,page->size,MEM_COMMIT|MEM_RESERVE,PAGE_READWRITE);
#else
            const long size=sysconf(_SC_PAGESIZE);if(size<=0)return {};
            page->size=size;page->code=mmap(nullptr,page->size,PROT_READ|PROT_WRITE,MAP_PRIVATE|MAP_ANONYMOUS,-1,0);
            if(page->code==MAP_FAILED)page->code=nullptr;
#endif
            if(!page->code)return {};
            page->count=page->size/slot;
            page->cells=std::make_unique<std::atomic<void*>[]>(page->count);
            if(tracked)page->hits=std::make_unique<std::atomic<uint8_t>[]>(page->count);
            for(size_t i=0;i<page->count;++i) {
                auto* code=static_cast<uint8_t*>(page->code)+i*slot;
                const uintptr_t cell=reinterpret_cast<uintptr_t>(&page->cells[i]);
#if defined(_M_X64)||defined(__x86_64__)
                if(tracked) {
                    // ENDBR64; mov r11, hit; cmp byte [r11], 0; jne +4;
                    // mov byte [r11], 1; mov r11, cell; jmp [r11]. The flag is
                    // written once, later calls only read it. R11 and flags
                    // are ABI scratch at a call boundary.
                    const uintptr_t hit=reinterpret_cast<uintptr_t>(&page->hits[i]);
                    const uint8_t head[]{0xF3,0x0F,0x1E,0xFA,0x49,0xBB};
                    const uint8_t mark[]{0x41,0x80,0x3B,0x00,0x75,0x04,0x41,0xC6,0x03,0x01,0x49,0xBB};
                    const uint8_t jump[]{0x41,0xFF,0x23};
                    std::memcpy(code,head,sizeof(head));std::memcpy(code+6,&hit,8);
                    std::memcpy(code+14,mark,sizeof(mark));std::memcpy(code+26,&cell,8);
                    std::memcpy(code+34,jump,sizeof(jump));
                    continue;
                }
                // ENDBR64; mov r11, cell; jmp [r11]. R11 is ABI scratch.
                const uint8_t prefix[]{0xF3,0x0F,0x1E,0xFA,0x49,0xBB};
                const uint8_t suffix[]{0x41,0xFF,0x23};
                std::memcpy(code,prefix,sizeof(prefix));std::memcpy(code+6,&cell,8);
                std::memcpy(code+14,suffix,sizeof(suffix));
#elif defined(__aarch64__)||defined(_M_ARM64)
                // BTI jc; ldr x16,literal; ldar x16,[x16]; br x16; two NOPs.
                // X16 is AAPCS64 IP0, argument and return registers stay intact.
                const uint32_t words[]{0xD50324DF,0x580000B0,0xC8DFFE10,0xD61F0200,0xD503201F,0xD503201F};
                std::memcpy(code,words,sizeof(words));std::memcpy(code+24,&cell,8);
#else
                return {};
#endif
            }
#if defined(_WIN32)
            DWORD old=0;
            if(!VirtualProtect(page->code,page->size,PAGE_EXECUTE_READ,&old))return {};
            FlushInstructionCache(GetCurrentProcess(),page->code,page->size);
#else
            __builtin___clear_cache(static_cast<char*>(page->code),static_cast<char*>(page->code)+page->size);
            if(mprotect(page->code,page->size,PROT_READ|PROT_EXEC)!=0)return {};
#endif
            pages.push_back(std::move(page));
        }
        auto& page=*pages.back();const size_t index=page.used++;++issued_;
        return {static_cast<uint8_t*>(page.code)+index*slot,&page.cells[index],
            tracked?&page.hits[index]:nullptr};
    }
    struct Page {
        void* code=nullptr;size_t size=0,count=0,used=0;
        std::unique_ptr<std::atomic<void*>[]> cells;
        std::unique_ptr<std::atomic<uint8_t>[]> hits;
        ~Page() {
            if(!code)return;
#if defined(_WIN32)
            VirtualFree(code,0,MEM_RELEASE);
#else
            munmap(code,size);
#endif
        }
    };
    std::mutex mutex_;size_t issued_=0;
    std::vector<std::unique_ptr<Page>> pages_,tracked_pages_;
};
static_assert(std::atomic<void*>::is_always_lock_free&&sizeof(void*)==8);

struct Backend {
    std::function<bool(void*,void*,void**)> prepare;
    std::function<bool(void*)> enable;
    std::function<void(void*)> abort;
    std::function<void(void*)> retire; // Must retain the original trampoline.
};
// One patched target, many owners. Nodes run in first-registration order: the
// earliest registered detour is entered first and its next reaches the second
// one, the last next reaches the game's original code. A disabled node keeps
// its position as a pass-through; reactivating the same module/detour pair
// restores it in place, any other registration is appended at the end.
class Chain {
public:
    // Same module registering the same target again while its node is active:
    // Reuse returns the existing next/handle (third-party chain ABI), Reject
    // returns BE_Result_Conflict (built-in create_hook kept its exclusive
    // per-module contract; a module still cannot hook one entry twice).
    enum class Duplicate {Reuse,Reject};
    explicit Chain(Backend backend):backend_(std::move(backend)) {}
    BE_Result Create(const char* module,void* target,void* detour,void** next,uint64_t* handle,
            Duplicate duplicate=Duplicate::Reuse) {
        if(!module||!*module||!target||!detour||!next||!handle||target==detour)return BE_Result_InvalidArgument;
        static std::atomic<uint64_t> serial{1};
        try {
            std::lock_guard lock(mutex_);
            auto found=targets_.find(target);
            // A rejected duplicate leaves next/handle untouched: the caller may
            // pass the variable its live hook still calls through.
            if(!stopped_&&found!=targets_.end()&&duplicate==Duplicate::Reject)
                for(auto* existing:found->second->nodes)
                    if(existing->active&&existing->module==module)return BE_Result_Conflict;
            *next=nullptr;*handle=0;
            if(stopped_)return BE_Result_NotReady;
            if(found!=targets_.end())for(auto* existing:found->second->nodes)
                if(existing->module==module&&existing->detour==detour) {
                    if(!existing->active) {
                        auto owned=nodes_.extract(existing->handle);
                        const uint64_t fresh=serial.fetch_add(1);owned.key()=fresh;
                        nodes_.insert(std::move(owned));existing->handle=fresh;
                        *next=existing->next.code;*handle=fresh;
                        existing->active=true;existing->entry.Set(detour);
                    }else {*next=existing->next.code;*handle=existing->handle;}
                    return BE_Result_Ok;
                }
            if(nodes_.size()>=4096||(found==targets_.end()&&targets_.size()>=1024))return BE_Result_Failed;
            auto node=std::make_unique<Node>();node->module=module;node->target=target;node->detour=detour;
            node->entry=RelayPool::Instance().Make();node->next=RelayPool::Instance().Make();
            if(!node->entry||!node->next)return BE_Result_Failed;
            node->handle=serial.fetch_add(1);
            const uint64_t id=node->handle;
            const bool first=found==targets_.end();
            if(first) {
                auto record=std::make_unique<Target>();record->head=RelayPool::Instance().Make();
                if(!record->head)return BE_Result_Failed;
                record->nodes.reserve(1);found=targets_.emplace(target,std::move(record)).first;
            }else found->second->nodes.reserve(found->second->nodes.size()+1);
            auto& record=*found->second;
            Node* added=node.get();bool prepared=false;
            // All bookkeeping is transactional until the physical patch is
            // enabled. In particular, map allocation failure must not leave
            // an empty target which a subsequent registration could reuse.
            auto rollback=[&] {
                if(first) {
                    if(prepared) {
                        record.head.Set(record.original);
                        added->entry.Set(added->next.code);
                        backend_.abort(target);
                    }
                    targets_.erase(found);
                }
                nodes_.erase(id);*next=nullptr;*handle=0;
            };
            try {
                nodes_.emplace(id,std::move(node));
                if(first) {
                    prepared=backend_.prepare(target,record.head.code,&record.original);
                    if(!prepared||!record.original) {rollback();return BE_Result_Failed;}
                }
                added->next.Set(record.original);added->entry.Set(detour);
                // Publish the caller's next before a detour can be dispatched.
                *next=added->next.code;*handle=id;
                record.nodes.push_back(added); // Capacity was reserved above.
                if(first) {
                    record.head.Set(added->entry.code);
                    if(!backend_.enable(target)) {rollback();return BE_Result_Failed;}
                }else record.nodes[record.nodes.size()-2]->next.Set(added->entry.code);
            }catch(...) {rollback();return BE_Result_Failed;}
            return BE_Result_Ok;
        }catch(...){return BE_Result_Failed;}
    }
    BE_Result Disable(uint64_t handle) {
        std::lock_guard lock(mutex_);auto found=nodes_.find(handle);
        if(found==nodes_.end())return BE_Result_NotFound;
        auto& node=*found->second;node.entry.Set(node.next.code);node.active=false;return BE_Result_Ok;
    }
    BE_Result DisableModule(const char* module) {
        if(!module||!*module)return BE_Result_InvalidArgument;
        std::lock_guard lock(mutex_);
        for(auto& [id,node]:nodes_)if(node->module==module){node->entry.Set(node->next.code);node->active=false;}
        return BE_Result_Ok;
    }
    bool Contains(void* target) const {std::lock_guard lock(mutex_);return targets_.contains(target);}
    // Active owners of target in call order; empty when every node is a pass-through.
    std::vector<std::string> Modules(void* target) const {
        std::lock_guard lock(mutex_);std::vector<std::string> modules;
        const auto found=targets_.find(target);if(found==targets_.end())return modules;
        for(const auto* node:found->second->nodes)if(node->active)modules.push_back(node->module);
        return modules;
    }
    // Patched target of a live handle, nullptr when the handle is unknown.
    void* TargetOf(uint64_t handle) const {
        std::lock_guard lock(mutex_);const auto found=nodes_.find(handle);
        return found==nodes_.end()?nullptr:found->second->target;
    }
    bool HasTargets() const {std::lock_guard lock(mutex_);return !targets_.empty();}
    void Shutdown() {
        std::lock_guard lock(mutex_);if(stopped_)return;stopped_=true;
        for(auto& [id,node]:nodes_){node->entry.Set(node->next.code);node->active=false;}
        for(auto& [target,record]:targets_){record->head.Set(record->original);backend_.retire(target);}
        // RX relays/cells and original trampolines remain process-pinned.
    }
private:
    struct Node {std::string module;void* target=nullptr;void* detour=nullptr;uint64_t handle=0;Relay entry,next;bool active=true;};
    struct Target {Relay head;void* original=nullptr;std::vector<Node*> nodes;};
    Backend backend_;mutable std::mutex mutex_;bool stopped_=false;
    std::unordered_map<void*,std::unique_ptr<Target>> targets_;
    std::unordered_map<uint64_t,std::unique_ptr<Node>> nodes_;
};
} // namespace BetterEndfieldNext::Hooks
