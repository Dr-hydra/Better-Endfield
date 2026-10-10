#include "code_patch_posix.h"
#include <dobby.h>
#include <unistd.h>
#include <sys/mman.h>
#include <cstdio>
#include <cstring>
#include <limits>
#include <mutex>
#include <vector>

namespace {
thread_local bool failed=false;
std::mutex patch_mutex;
struct Page {uintptr_t address;int protection;};
bool Permissions(uintptr_t page,size_t size,int& protection) {
    FILE* file=std::fopen("/proc/self/maps","r");if(!file)return false;
    char line[1024]{};bool found=false;
    while(std::fgets(line,sizeof(line),file)) {
        uintptr_t low=0,high=0;char mode[5]{};
        if(std::sscanf(line,"%lx-%lx %4s",&low,&high,mode)!=3||page<low||page>=high||size>high-page)continue;
        protection=(mode[0]=='r'?PROT_READ:0)|(mode[1]=='w'?PROT_WRITE:0)|(mode[2]=='x'?PROT_EXEC:0);
        found=(protection&PROT_READ)!=0;break;
    }
    std::fclose(file);return found;
}
}
namespace betterendfieldnext {
void ResetCodePatchFailure(){failed=false;}
bool CodePatchFailed(){return failed;}
}

// Repository-owned replacement for Dobby 1.0.5's one-page POSIX patcher.
// Cover the complete write and retain every page's original permissions.
extern "C" __attribute__((visibility("default")))
MemoryOperationError CodePatch(void* address,uint8_t* buffer,uint32_t size) {
    auto reject=[](){failed=true;return kMemoryOperationError;};
    const uintptr_t begin=reinterpret_cast<uintptr_t>(address);
    const long page_size=sysconf(_SC_PAGESIZE);
    if(!address||!buffer||!size||page_size<=0||begin>UINTPTR_MAX-size)return reject();
    const size_t page=static_cast<size_t>(page_size);
    const uintptr_t first=begin-begin%page,last=(begin+size-1)-(begin+size-1)%page;
    if((last-first)/page>65536)return reject();
    try {
        std::lock_guard<std::mutex> lock(patch_mutex);
        std::vector<Page> pages;
        for(uintptr_t at=first;;at+=page) {
            int protection=0;if(!Permissions(at,page,protection))return reject();
            pages.push_back({at,protection});if(at==last)break;
        }
        size_t writable=0;
        for(;writable<pages.size();++writable)
            if(mprotect(reinterpret_cast<void*>(pages[writable].address),page,pages[writable].protection|PROT_WRITE)!=0)break;
        if(writable!=pages.size()) {
            while(writable>0){--writable;mprotect(reinterpret_cast<void*>(pages[writable].address),page,pages[writable].protection);}
            return reject();
        }
        std::memmove(address,buffer,size);
        __builtin___clear_cache(static_cast<char*>(address),static_cast<char*>(address)+size);
        bool restored=true;
        for(const auto& entry:pages)
            restored=(mprotect(reinterpret_cast<void*>(entry.address),page,entry.protection)==0)&&restored;
        return restored?kMemoryOperationSuccess:reject();
    }catch(...){return reject();}
}
