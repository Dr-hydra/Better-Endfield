#pragma once
#include <Windows.h>
#include <cstdint>
#include <cstring>
#include <algorithm>
#include <string>

namespace BetterEndfieldNext::MediaScreen::GameInput {
inline constexpr uint32_t kMagic=0x42454749, kVersion=1;
inline constexpr size_t kCapacity=256;
inline constexpr wchar_t kDllName[]=L"BetterEndfieldNext.GameInputBridge.dll";
inline constexpr wchar_t kDispatchName[]=L"BetterEndfieldNext.GameInput.Dispatch.V1";
enum Hooks:uint32_t { Raw=1, KeyState=2, Cursor=4, Focus=8, DirectInput=16 };
struct Event { uint32_t message,key; int64_t data; int32_t x,y; };
struct alignas(8) Shared {
    uint32_t magic,version,source_pid,owner_pid;
    uint64_t window,generation,heartbeat,read,write;
    uint32_t requested_active,ready,hooks,error_code;
    uint64_t processed,raw_reads,key_reads,cursor_reads,focus_reads,raw_dispatched,raw_filtered;
    uint32_t raw_keyboard_targets,raw_mouse_targets;
    uint32_t probe_key,probe_mouse,probe_unity_keys,probe_unity_mouse,probe_rewired_keys,probe_rewired_mouse;
    int32_t probe_frame;
    uint32_t probe_available;
    float probe_unity_x,probe_unity_y,probe_rewired_x,probe_rewired_y;
    char error[256];
    Event events[kCapacity];
};
inline std::wstring Name(DWORD pid) { return L"Local\\BetterEndfieldNext.GameInput.V1."+std::to_wstring(pid); }
class Channel {
    HANDLE mapping_=nullptr,mutex_=nullptr;
public:
    Shared* data=nullptr;
    explicit Channel(DWORD pid,bool create) {
        auto name=Name(pid),lock=name+L".Lock";
        mutex_=create?CreateMutexW(nullptr,FALSE,lock.c_str()):OpenMutexW(SYNCHRONIZE|MUTEX_MODIFY_STATE,FALSE,lock.c_str());
        if (mutex_) mapping_=create?CreateFileMappingW(INVALID_HANDLE_VALUE,nullptr,PAGE_READWRITE,0,sizeof(Shared),name.c_str()):OpenFileMappingW(FILE_MAP_ALL_ACCESS,FALSE,name.c_str());
        if (mapping_) data=static_cast<Shared*>(MapViewOfFile(mapping_,FILE_MAP_ALL_ACCESS,0,0,sizeof(Shared)));
    }
    ~Channel() { if(data)UnmapViewOfFile(data); if(mapping_)CloseHandle(mapping_); if(mutex_)CloseHandle(mutex_); }
    Channel(const Channel&)=delete;
    bool Lock(DWORD timeout=100) { auto value=WaitForSingleObject(mutex_,timeout); return value==WAIT_OBJECT_0||value==WAIT_ABANDONED; }
    void Unlock() { ReleaseMutex(mutex_); }
};
struct Lock {
    Channel& channel; bool acquired;
    explicit Lock(Channel& value,DWORD timeout=100):channel(value),acquired(value.Lock(timeout)){}
    ~Lock(){if(acquired)channel.Unlock();}
};
inline void Error(Shared& value,const std::string& text,uint32_t code=1) {
    value.error_code=code;value.requested_active=0;
    auto length=std::min(text.size(),sizeof(value.error)-1); std::memcpy(value.error,text.data(),length);value.error[length]=0;
}
}
