#include <dobby.h>
#include "../core/code_patch_posix.h"
#include <sys/mman.h>
#include <unistd.h>
#include <fcntl.h>
#include <cstdlib>
#include <cstdio>
#include <cstring>
#include <cstdint>
#include <stdexcept>
#include <iostream>
void Check(bool ok,const char* reason){if(!ok)throw std::runtime_error(reason);}
using Function=int(*)();
Function original=nullptr;
int Replacement(){return original()+10;}
int Permission(uintptr_t address) {
    FILE* file=std::fopen("/proc/self/maps","r");Check(file,"maps unavailable");
    char line[512];int result=-1;
    while(std::fgets(line,sizeof(line),file)) {
        uintptr_t low,high;char p[5];
        if(std::sscanf(line,"%lx-%lx %4s",&low,&high,p)==3&&low<=address&&address<high){
            result=(p[0]=='r'?PROT_READ:0)|(p[1]=='w'?PROT_WRITE:0)|(p[2]=='x'?PROT_EXEC:0);break;}
    }
    std::fclose(file);return result;
}
int main() {
    const size_t page=sysconf(_SC_PAGESIZE);
    auto base=static_cast<uint8_t*>(mmap(nullptr,page*3,PROT_READ|PROT_WRITE,MAP_PRIVATE|MAP_ANONYMOUS,-1,0));
    Check(base!=MAP_FAILED,"mmap failed");auto target=base+page-12;
    uint32_t code[]{0x528000e0,0xd65f03c0,0xd503201f,0xd503201f}; // mov w0,7; ret; nop; nop
    std::memcpy(target,code,sizeof(code));
    Check(mprotect(base,page,PROT_READ|PROT_EXEC)==0&&mprotect(base+page,page,PROT_READ)==0&&
        mprotect(base+page*2,page,PROT_NONE)==0,"initial permissions failed");
    auto function=reinterpret_cast<int(*)()>(target);Check(function()==7,"initial code failed");
    code[0]=0x52800120; // mov w0,9
    betterendfieldnext::ResetCodePatchFailure();
    Check(CodePatch(target,reinterpret_cast<uint8_t*>(code),sizeof(code))==kMemoryOperationSuccess,"cross-page patch refused");
    Check(!betterendfieldnext::CodePatchFailed()&&function()==9,"patched instruction/cache incorrect");
    Check(Permission(reinterpret_cast<uintptr_t>(base))==(PROT_READ|PROT_EXEC)&&
        Permission(reinterpret_cast<uintptr_t>(base+page))==PROT_READ,"mixed page protections not restored");
    betterendfieldnext::ResetCodePatchFailure();
    Check(DobbyPrepare(target,reinterpret_cast<void*>(&Replacement),reinterpret_cast<void**>(&original))==0,
        "cross-page hook preparation failed");
    Check(DobbyCommit(target)==0&&!betterendfieldnext::CodePatchFailed(),"cross-page hook commit failed");
    Check(function()==19&&original()==9,"cross-page detour/original trampoline failed");
    Check(DobbyDestroy(target)==0&&function()==9,"cross-page hook restoration failed");
    Check(Permission(reinterpret_cast<uintptr_t>(base))==(PROT_READ|PROT_EXEC)&&
        Permission(reinterpret_cast<uintptr_t>(base+page))==PROT_READ,"hook changed page permissions");
    uint8_t before[8];std::memcpy(before,base+2*page-8,sizeof(before));
    Check(CodePatch(base+2*page-8,reinterpret_cast<uint8_t*>(code),sizeof(code))==kMemoryOperationError,
        "inaccessible second page was accepted");
    Check(std::memcmp(before,base+2*page-8,sizeof(before))==0&&Permission(reinterpret_cast<uintptr_t>(base+page))==PROT_READ,
        "failed patch modified bytes or protections");
    Check(CodePatch(reinterpret_cast<void*>(UINTPTR_MAX-3),reinterpret_cast<uint8_t*>(code),16)==kMemoryOperationError,"overflow accepted");
    Check(CodePatch(nullptr,reinterpret_cast<uint8_t*>(code),16)==kMemoryOperationError,"null address accepted");
    Check(CodePatch(target,nullptr,16)==kMemoryOperationError,"null payload accepted");
    // First page can be opened for writing, second is a shared read-only file.
    // A failed second mprotect must undo the first before any byte is copied.
    char path[]="/data/local/tmp/be-codepatch-XXXXXX";int fd=mkstemp(path);
    Check(fd>=0&&ftruncate(fd,page)==0,"test file creation failed");close(fd);
    fd=open(path,O_RDONLY);unlink(path);Check(fd>=0,"read-only test file unavailable");
    Check(mmap(base+page,page,PROT_READ,MAP_SHARED|MAP_FIXED,fd,0)==base+page,"read-only page mapping failed");close(fd);
    code[0]=0x52800160; // mov w0,11; must not be written on rejection
    Check(CodePatch(target,reinterpret_cast<uint8_t*>(code),sizeof(code))==kMemoryOperationError,
        "write-denied page was accepted");
    Check(function()==9&&Permission(reinterpret_cast<uintptr_t>(base))==(PROT_READ|PROT_EXEC)&&
        Permission(reinterpret_cast<uintptr_t>(base+page))==PROT_READ,"write-denial did not restore preceding pages");
    munmap(base,page*3);
    std::cout<<"PASS cross-page ARM64 code patch and Dobby prepare/commit/detour/original/restore, mixed permissions, write-denial rollback, unreadable page/overflow/null rejection\n";
}
