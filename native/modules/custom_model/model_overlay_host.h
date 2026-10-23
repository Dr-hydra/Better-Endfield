#pragma once
#if defined(_WIN32)
#include "model_overlay_protocol.h"
#include <algorithm>
#include <filesystem>
#include <mutex>
#include <string_view>
#include <vector>

namespace BetterEndfieldNext::CustomModel {
// One row of the section 13 driver panel. Strings stay UTF-8 on the module
// side; only the shared block is UTF-16, matching every other field there.
struct DriverPanelState {
    std::string id,name;
    bool playing=false;
    uint32_t speed_millis=1000;
};
// Lifecycle only: Win32 process/IPC, no worker and no Unity calls.
class ModelOverlayHost {
public:
    bool Start(const std::filesystem::path& root,bool hot_switch,const void* module_address) {
        std::lock_guard lock(mutex_);
        Stop(); HMODULE module=nullptr;wchar_t dll[32768]{};
        if(!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(module_address),&module)||!GetModuleFileNameW(module,dll,32768)) return false;
        const auto directory=std::filesystem::path(dll).parent_path();
        const auto executable=directory/L"BetterEndfieldNext.ModelOverlay.exe";
        const auto mapping_name=OverlayProtocol::MappingName(GetCurrentProcessId());
        mapping_=CreateFileMappingW(INVALID_HANDLE_VALUE,nullptr,PAGE_READWRITE,0,sizeof(OverlayProtocol::Shared),mapping_name.c_str());
        if(!mapping_) return false;
        view_=static_cast<OverlayProtocol::Shared*>(MapViewOfFile(mapping_,FILE_MAP_ALL_ACCESS,0,0,sizeof(OverlayProtocol::Shared)));
        if(!view_) {Stop();return false;}
        *view_=OverlayProtocol::Shared{};view_->game_pid=GetCurrentProcessId();
        view_->runtime_hot_switch=hot_switch?1:0;
        const auto ini=(root/L"runtime.ini").wstring();
        // Same library location used by UI when installed beside the launcher.
        const auto packages=(directory.parent_path()/L"models").wstring();
        if(ini.size()>=OverlayProtocol::kPathCapacity||packages.size()>=OverlayProtocol::kPathCapacity) {Stop();return false;}
        wcscpy_s(view_->runtime_ini,ini.c_str());wcscpy_s(view_->package_directory,packages.c_str());
        executable_=executable;mapping_name_=mapping_name;last_launch_=0;
        Tick(); return true;
    }
    void Tick() {
        std::lock_guard lock(mutex_);
        if(!view_) return;
        if(process_) {
            if(WaitForSingleObject(process_,0)==WAIT_TIMEOUT) return;
            CloseHandle(process_);process_=nullptr;
        }
        const auto now=GetTickCount64();if(last_launch_&&now-last_launch_<5000) return;last_launch_=now;
        std::wstring command=L"\""+executable_.wstring()+L"\" --game-pid "+std::to_wstring(GetCurrentProcessId())+
            L" --mapping \""+mapping_name_+L"\"";
        STARTUPINFOW startup{sizeof(startup)};startup.dwFlags=STARTF_USESHOWWINDOW;startup.wShowWindow=SW_HIDE;
        PROCESS_INFORMATION process{};
        if(CreateProcessW(executable_.c_str(),command.data(),nullptr,nullptr,FALSE,CREATE_NO_WINDOW,nullptr,
            executable_.parent_path().c_str(),&startup,&process)) {process_=process.hProcess;CloseHandle(process.hThread);}
    }
    void Result(std::string_view config,bool accepted) {
        std::lock_guard lock(mutex_);
        if(!view_) return;
        InterlockedExchange(&view_->last_result,accepted?1:-1);
        InterlockedExchange64(&view_->last_config_hash,static_cast<LONG64>(OverlayProtocol::ConfigHash(config)));
    }
    // Publishes the live driver set. Called when a package's drivers are
    // (re)loaded, not every frame: the companion polls the revision instead.
    void PublishDrivers(std::string_view package,const std::vector<DriverPanelState>& list) {
        std::lock_guard lock(mutex_);
        if(!view_) return;
        Wide(view_->driver_package,OverlayProtocol::kPathCapacity,package);
        // Count first down to zero so a reader never pairs a new count with
        // stale rows, then up again once every row is in place.
        InterlockedExchange(&view_->driver_count,0);
        const size_t count=std::min<size_t>(list.size(),OverlayProtocol::kDriverCapacity);
        for(size_t i=0;i<count;++i) {
            auto& row=view_->drivers[i];
            Wide(row.id,OverlayProtocol::kDriverIdCapacity,list[i].id);
            Wide(row.name,OverlayProtocol::kDriverNameCapacity,list[i].name);
            InterlockedExchange(&row.speed_millis,static_cast<LONG>(list[i].speed_millis));
            InterlockedExchange(&row.playing,list[i].playing?1:0);
        }
        InterlockedExchange(&view_->driver_count,static_cast<LONG>(count));
        InterlockedIncrement(&view_->driver_revision);
    }
    // Returns true when the companion edited a control since the previous call;
    // `out` then carries every row so the caller can apply the whole set.
    bool PollDriverControls(std::vector<DriverPanelState>& out) {
        std::lock_guard lock(mutex_);
        if(!view_) return false;
        const LONG revision=InterlockedCompareExchange(&view_->driver_control_revision,0,0);
        if(revision==seen_control_revision_) return false;
        seen_control_revision_=revision;
        const LONG count=std::clamp<LONG>(InterlockedCompareExchange(&view_->driver_count,0,0),
            0,static_cast<LONG>(OverlayProtocol::kDriverCapacity));
        out.clear();
        for(LONG i=0;i<count;++i) {
            auto& row=view_->drivers[i];
            DriverPanelState state;
            state.id=Narrow(row.id,OverlayProtocol::kDriverIdCapacity);
            state.name=Narrow(row.name,OverlayProtocol::kDriverNameCapacity);
            state.playing=InterlockedCompareExchange(&row.playing,0,0)!=0;
            state.speed_millis=static_cast<uint32_t>(std::max<LONG>(
                InterlockedCompareExchange(&row.speed_millis,0,0),1));
            if(state.id.empty()) continue;
            out.push_back(std::move(state));
        }
        return true;
    }
    void Stop() {
        std::lock_guard lock(mutex_);
        if(view_) InterlockedExchange(&view_->shutdown_requested,1);
        if(process_) {WaitForSingleObject(process_,1000);CloseHandle(process_);process_=nullptr;}
        if(view_) {UnmapViewOfFile(view_);view_=nullptr;}
        if(mapping_) {CloseHandle(mapping_);mapping_=nullptr;}
    }
private:
    static void Wide(wchar_t* target,size_t capacity,std::string_view text) {
        std::wstring buffer;
        if(!text.empty()) {
            const int size=MultiByteToWideChar(CP_UTF8,0,text.data(),static_cast<int>(text.size()),nullptr,0);
            if(size>0) {buffer.resize(static_cast<size_t>(size));
                MultiByteToWideChar(CP_UTF8,0,text.data(),static_cast<int>(text.size()),buffer.data(),size);}
        }
        const size_t length=std::min(buffer.size(),capacity?capacity-1:0);
        if(length) wmemcpy(target,buffer.data(),length);
        if(capacity) target[length]=L'\0';
    }
    static std::string Narrow(const wchar_t* text,size_t capacity) {
        size_t length=0; while(length<capacity&&text[length]) ++length;
        if(!length) return {};
        const int size=WideCharToMultiByte(CP_UTF8,0,text,static_cast<int>(length),nullptr,0,nullptr,nullptr);
        if(size<=0) return {};
        std::string buffer(static_cast<size_t>(size),'\0');
        WideCharToMultiByte(CP_UTF8,0,text,static_cast<int>(length),buffer.data(),size,nullptr,nullptr);
        return buffer;
    }
    std::recursive_mutex mutex_;
    HANDLE mapping_=nullptr,process_=nullptr;
    OverlayProtocol::Shared* view_=nullptr;
    std::filesystem::path executable_;
    std::wstring mapping_name_;
    ULONGLONG last_launch_=0;
    LONG seen_control_revision_=0;
};
} // namespace BetterEndfieldNext::CustomModel
#endif
