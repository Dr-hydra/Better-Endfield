#pragma once
#if defined(_WIN32)
#include "model_overlay_protocol.h"
#include <filesystem>
#include <mutex>

namespace BetterEndfield::CustomModel {
// Lifecycle only: Win32 process/IPC, no worker and no Unity calls.
class ModelOverlayHost {
public:
    bool Start(const std::filesystem::path& root,bool hot_switch,const void* module_address) {
        std::lock_guard lock(mutex_);
        Stop(); HMODULE module=nullptr;wchar_t dll[32768]{};
        if(!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(module_address),&module)||!GetModuleFileNameW(module,dll,32768)) return false;
        const auto directory=std::filesystem::path(dll).parent_path();
        const auto executable=directory/L"BetterEndfield.ModelOverlay.exe";
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
    void Stop() {
        std::lock_guard lock(mutex_);
        if(view_) InterlockedExchange(&view_->shutdown_requested,1);
        if(process_) {WaitForSingleObject(process_,1000);CloseHandle(process_);process_=nullptr;}
        if(view_) {UnmapViewOfFile(view_);view_=nullptr;}
        if(mapping_) {CloseHandle(mapping_);mapping_=nullptr;}
    }
private:
    std::recursive_mutex mutex_;
    HANDLE mapping_=nullptr,process_=nullptr;
    OverlayProtocol::Shared* view_=nullptr;
    std::filesystem::path executable_;
    std::wstring mapping_name_;
    ULONGLONG last_launch_=0;
};
} // namespace BetterEndfield::CustomModel
#endif
