#pragma once
#if defined(_WIN32)
#include <Windows.h>
#include "runtime_ini.h"
#include <atomic>
#include <filesystem>
#include <functional>

namespace BetterEndfield::CustomModel::Settings {
inline constexpr wchar_t kMutexName[]=L"Local\\BetterEndfield.CustomModel.Settings";
class MutexLock {
public:
    MutexLock() {
        mutex_=CreateMutexW(nullptr,FALSE,kMutexName);
        const DWORD wait=mutex_?WaitForSingleObject(mutex_,1500):WAIT_FAILED;
        owned_=wait==WAIT_OBJECT_0||wait==WAIT_ABANDONED;
        if(!owned_) {if(mutex_) CloseHandle(mutex_);mutex_=nullptr;throw std::runtime_error("Runtime settings mutex unavailable");}
    }
    ~MutexLock() {if(owned_) ReleaseMutex(mutex_);if(mutex_) CloseHandle(mutex_);}
    MutexLock(const MutexLock&)=delete;
    MutexLock& operator=(const MutexLock&)=delete;
private:
    HANDLE mutex_=nullptr; bool owned_=false;
};
inline std::string ReadFile(const std::filesystem::path& path) {
    HANDLE file=CreateFileW(path.c_str(),GENERIC_READ,FILE_SHARE_READ|FILE_SHARE_WRITE|FILE_SHARE_DELETE,
        nullptr,OPEN_EXISTING,FILE_ATTRIBUTE_NORMAL,nullptr);
    if(file==INVALID_HANDLE_VALUE) {
        if(GetLastError()==ERROR_FILE_NOT_FOUND||GetLastError()==ERROR_PATH_NOT_FOUND) return {};
        throw std::runtime_error("Cannot read runtime.ini");
    }
    LARGE_INTEGER size{};
    if(!GetFileSizeEx(file,&size)||size.QuadPart<0||size.QuadPart>kMaxIniBytes) {
        CloseHandle(file);throw std::runtime_error("Invalid runtime.ini size");
    }
    std::string text(static_cast<size_t>(size.QuadPart),'\0'); DWORD read=0;
    const bool ok=::ReadFile(file,text.data(),static_cast<DWORD>(text.size()),&read,nullptr)&&read==text.size();
    CloseHandle(file);if(!ok) throw std::runtime_error("Cannot read runtime.ini");return text;
}
inline std::string ReadLocked(const std::filesystem::path& path) {MutexLock lock;return ReadFile(path);}
inline void ReplaceLocked(const std::filesystem::path& path,const std::string& text) {
    if(text.size()>kMaxIniBytes) throw std::runtime_error("Runtime configuration exceeds 1 MiB");
    static std::atomic_uint serial{0};
    const auto temp=path.parent_path()/(L"runtime."+std::to_wstring(GetCurrentProcessId())+L"."+
        std::to_wstring(GetTickCount64())+L"."+std::to_wstring(serial.fetch_add(1))+L".tmp");
    HANDLE file=CreateFileW(temp.c_str(),GENERIC_WRITE,0,nullptr,CREATE_NEW,FILE_ATTRIBUTE_NORMAL,nullptr);
    if(file==INVALID_HANDLE_VALUE) throw std::runtime_error("Cannot create settings temporary file");
    DWORD written=0;
    const bool ok=WriteFile(file,text.data(),static_cast<DWORD>(text.size()),&written,nullptr)&&
        written==text.size()&&FlushFileBuffers(file);
    CloseHandle(file);
    if(!ok||!MoveFileExW(temp.c_str(),path.c_str(),MOVEFILE_REPLACE_EXISTING|MOVEFILE_WRITE_THROUGH)) {
        DeleteFileW(temp.c_str());throw std::runtime_error("Runtime settings atomic replacement failed");
    }
}
// Selection/metadata work is outside the mutex. Recheck the complete read baseline
// before committing; rebase the operation on a newer file rather than clobbering it.
inline bool Update(const std::filesystem::path& path,const std::function<void(Ini&)>& operation,std::string& error) {
    error.clear();
    try {
        std::filesystem::create_directories(path.parent_path());
        for(int attempt=0;attempt<12;++attempt) {
            const auto baseline=ReadLocked(path); Ini ini(baseline); operation(ini);const auto next=ini.Text();
            MutexLock lock;
            if(ReadFile(path)!=baseline) continue;
            if(next!=baseline) ReplaceLocked(path,next);
            return true;
        }
        throw std::runtime_error("Runtime settings changed repeatedly; retry save");
    } catch(const std::exception& e) {error=e.what();return false;}
}
} // namespace BetterEndfield::CustomModel::Settings
#endif
