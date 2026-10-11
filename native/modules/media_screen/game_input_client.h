#pragma once
#include "game_input_protocol.h"
#include "window_capture.h"
#include <TlHelp32.h>
#include <dwmapi.h>
#include <filesystem>
#include <array>
#include <memory>
#include <vector>
#include <cstdio>

namespace BetterEndfieldNext::MediaScreen {
class GameInputClient {
    std::unique_ptr<GameInput::Channel> channel_;
    HWND window_=nullptr;
    std::string error_;
    std::array<bool,3> buttons_{};
    POINT last_{};
    bool has_point_=false;
    static void Log(const char* action,const GameInput::Shared& state){
        wchar_t base[32768]{};if(!GetEnvironmentVariableW(L"LOCALAPPDATA",base,32768))return;
        auto directory=std::filesystem::path(base)/L"BetterEndfieldNext";CreateDirectoryW(directory.c_str(),nullptr);directory/=L"logs";CreateDirectoryW(directory.c_str(),nullptr);
        HANDLE file=CreateFileW((directory/L"MediaInputBridge.log").c_str(),FILE_APPEND_DATA,FILE_SHARE_READ|FILE_SHARE_WRITE,nullptr,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,nullptr);
        if(file==INVALID_HANDLE_VALUE)return;
        SYSTEMTIME now{};GetSystemTime(&now);char line[768]{};
        int size=std::snprintf(line,sizeof(line),"%04u-%02u-%02uT%02u:%02u:%02uZ %s source_pid=%u hooks=%u events=%llu raw_reads=%llu key_reads=%llu cursor_reads=%llu focus_reads=%llu raw_targets=%u/%u\r\n",now.wYear,now.wMonth,now.wDay,now.wHour,now.wMinute,now.wSecond,action,state.source_pid,state.hooks,state.processed,state.raw_reads,state.key_reads,state.cursor_reads,state.focus_reads,state.raw_keyboard_targets,state.raw_mouse_targets);
        if(size>0&&size<int(sizeof(line))){DWORD written=0;WriteFile(file,line,DWORD(size),&written,nullptr);}CloseHandle(file);
    }
    static uintptr_t Module(DWORD pid,const wchar_t* name) {
        HANDLE snapshot=CreateToolhelp32Snapshot(TH32CS_SNAPMODULE|TH32CS_SNAPMODULE32,pid);
        if(snapshot==INVALID_HANDLE_VALUE)return 0;
        MODULEENTRY32W item{};item.dwSize=sizeof(item);uintptr_t result=0;
        for(BOOL more=Module32FirstW(snapshot,&item);more;more=Module32NextW(snapshot,&item))
            if(!_wcsicmp(item.szModule,name)){result=reinterpret_cast<uintptr_t>(item.modBaseAddr);break;}
        CloseHandle(snapshot);return result;
    }
    static uintptr_t RemoteFunction(DWORD pid,const char* name) {
        auto address=GetProcAddress(GetModuleHandleW(L"kernel32.dll"),name);HMODULE owner=nullptr;
        if(!address||!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,reinterpret_cast<LPCWSTR>(address),&owner))return 0;
        wchar_t path[MAX_PATH]{};GetModuleFileNameW(owner,path,MAX_PATH);
        auto remote=Module(pid,std::filesystem::path(path).filename().c_str());
        return remote?remote+reinterpret_cast<uintptr_t>(address)-reinterpret_cast<uintptr_t>(owner):0;
    }
    static uintptr_t RemoteExport(HANDLE process,uintptr_t module,const char* name) {
        // Resolve the loaded image itself. A source may still have an earlier
        // build loaded, whose export RVAs differ from the DLL beside the helper.
        auto read=[&](uintptr_t address,void* data,size_t bytes){
            SIZE_T received=0;return ReadProcessMemory(process,reinterpret_cast<LPCVOID>(address),data,bytes,&received)&&received==bytes;
        };
        IMAGE_DOS_HEADER dos{};IMAGE_NT_HEADERS64 nt{};
        if(!read(module,&dos,sizeof(dos))||dos.e_magic!=IMAGE_DOS_SIGNATURE||dos.e_lfanew<0||dos.e_lfanew>1024*1024||
            !read(module+dos.e_lfanew,&nt,sizeof(nt))||nt.Signature!=IMAGE_NT_SIGNATURE||
            nt.OptionalHeader.Magic!=IMAGE_NT_OPTIONAL_HDR64_MAGIC||nt.OptionalHeader.NumberOfRvaAndSizes<=IMAGE_DIRECTORY_ENTRY_EXPORT)return 0;
        auto image_bytes=nt.OptionalHeader.SizeOfImage;
        auto read_rva=[&](DWORD rva,void* data,size_t bytes){return rva&&rva<image_bytes&&bytes<=image_bytes-rva&&read(module+rva,data,bytes);};
        auto directory=nt.OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_EXPORT];IMAGE_EXPORT_DIRECTORY exports{};
        if(directory.Size<sizeof(exports)||directory.VirtualAddress>=image_bytes||directory.Size>image_bytes-directory.VirtualAddress||
            !read_rva(directory.VirtualAddress,&exports,sizeof(exports))||!exports.NumberOfNames||!exports.NumberOfFunctions||
            exports.NumberOfNames>65536||exports.NumberOfFunctions>65536)return 0;
        std::vector<DWORD> names(exports.NumberOfNames),functions(exports.NumberOfFunctions);
        std::vector<WORD> ordinals(exports.NumberOfNames);
        if(!read_rva(exports.AddressOfNames,names.data(),names.size()*sizeof(DWORD))||
            !read_rva(exports.AddressOfNameOrdinals,ordinals.data(),ordinals.size()*sizeof(WORD))||
            !read_rva(exports.AddressOfFunctions,functions.data(),functions.size()*sizeof(DWORD)))return 0;
        auto name_bytes=std::strlen(name)+1;std::vector<char> candidate(name_bytes);
        for(size_t i=0;i<names.size();++i){
            if(!read_rva(names[i],candidate.data(),name_bytes)||std::memcmp(candidate.data(),name,name_bytes))continue;
            if(ordinals[i]>=functions.size())return 0;auto rva=functions[ordinals[i]];
            if(!rva||rva>=image_bytes||(rva>=directory.VirtualAddress&&rva-directory.VirtualAddress<directory.Size))return 0;
            MEMORY_BASIC_INFORMATION memory{};
            if(!VirtualQueryEx(process,reinterpret_cast<LPCVOID>(module+rva),&memory,sizeof(memory))||memory.State!=MEM_COMMIT||
                !(memory.Protect&(PAGE_EXECUTE|PAGE_EXECUTE_READ|PAGE_EXECUTE_READWRITE|PAGE_EXECUTE_WRITECOPY))||(memory.Protect&PAGE_GUARD))return 0;
            return module+rva;
        }
        return 0;
    }
    bool RemoteCall(HANDLE process,uintptr_t function,void* argument,DWORD& result) {
        HANDLE thread=CreateRemoteThread(process,nullptr,0,reinterpret_cast<LPTHREAD_START_ROUTINE>(function),argument,0,nullptr);
        if(!thread){error_="Game input bridge could not start (Win32 "+std::to_string(GetLastError())+")";return false;}
        bool done=WaitForSingleObject(thread,5000)==WAIT_OBJECT_0;
        if(done)done=GetExitCodeThread(thread,&result)!=FALSE;
        CloseHandle(thread);
        if(!done)error_="Game input bridge initialization timed out; restart the source game before retrying";
        return done;
    }
    bool Load(DWORD pid,const std::wstring& path) {
        HANDLE process=OpenProcess(PROCESS_CREATE_THREAD|PROCESS_QUERY_INFORMATION|PROCESS_VM_OPERATION|PROCESS_VM_WRITE|PROCESS_VM_READ,FALSE,pid);
        if(!process){error_="Source game input access denied (Win32 "+std::to_string(GetLastError())+")";return false;}
        BOOL self_wow=FALSE,target_wow=FALSE;
        IsWow64Process(GetCurrentProcess(),&self_wow);IsWow64Process(process,&target_wow);
        if(self_wow!=target_wow){CloseHandle(process);error_="Game input bridge currently supports 64-bit source games";return false;}
        auto remote=Module(pid,GameInput::kDllName);
        if(!remote){
            auto size=(path.size()+1)*sizeof(wchar_t);void* buffer=VirtualAllocEx(process,nullptr,size,MEM_COMMIT|MEM_RESERVE,PAGE_READWRITE);
            SIZE_T written=0;DWORD exit_code=0;
            bool written_ok=buffer&&WriteProcessMemory(process,buffer,path.c_str(),size,&written)&&written==size;
            auto loader=RemoteFunction(pid,"LoadLibraryW");
            bool done=written_ok&&loader&&RemoteCall(process,loader,buffer,exit_code);
            // An unfinished remote loader may still be reading its argument.
            if(buffer&&(done||!written_ok||!loader))VirtualFreeEx(process,buffer,0,MEM_RELEASE);
            if(!done){CloseHandle(process);if(error_.empty())error_="Cannot load source game input bridge";return false;}
            remote=Module(pid,GameInput::kDllName);
        }
        auto entry=remote?RemoteExport(process,remote,"BE_GameInputInitialize"):0;
        if(!entry)error_="Loaded source input bridge has no valid initializer; restart the source game before retrying";
        DWORD exit_code=1;bool ok=entry&&RemoteCall(process,entry,nullptr,exit_code)&&exit_code==0;
        CloseHandle(process);if(!ok&&error_.empty())error_="Source game input bridge initialization failed";return ok;
    }
    bool Dispatch() {
        DWORD_PTR result=0;auto message=RegisterWindowMessageW(GameInput::kDispatchName);
        if(!message||!SendMessageTimeoutW(window_,message,0,0,SMTO_ABORTIFHUNG|SMTO_BLOCK,250,&result)) {
            error_="Source game input dispatch timed out (Win32 "+std::to_string(GetLastError())+")";return false;
        }
        if(result!=1){error_="Source game input handler did not acknowledge input";return false;}
        GameInput::Lock lock(*channel_);
        if(!lock.acquired){error_="Source game input channel locked";return false;}
        if(channel_->data->error_code){error_=channel_->data->error;return false;}
        return true;
    }
public:
    ~GameInputClient(){Release();}
    static bool GameWindow(HWND window) {
        wchar_t name[128]{};if(!GetClassNameW(window,name,128))return false;
        return !_wcsicmp(name,L"UnityWndClass")||!_wcsicmp(name,L"UnrealWindow");
    }
    static std::wstring DefaultDll() { wchar_t path[32768]{};GetModuleFileNameW(nullptr,path,32768);return (std::filesystem::path(path).parent_path()/GameInput::kDllName).wstring(); }
    std::string Error()const{return error_;}
    bool Bound()const{return channel_!=nullptr;}
    bool Bind(HWND window,DWORD pid,const std::wstring& dll=DefaultDll()) {
        Release();error_.clear();DWORD owner=0;GetWindowThreadProcessId(window,&owner);
        if(!IsWindow(window)||owner!=pid||pid==GetCurrentProcessId()){error_="Invalid source game window";return false;}
        if(!std::filesystem::exists(dll)){error_="Source game input bridge DLL missing";return false;}
        window_=window;channel_=std::make_unique<GameInput::Channel>(pid,true);
        if(!channel_->data){error_="Cannot open source game input channel";return false;}
        {
            GameInput::Lock lock(*channel_);if(!lock.acquired){error_="Source game input channel locked";return false;}
            auto& value=*channel_->data;uint64_t generation=value.magic==GameInput::kMagic?value.generation+1:1;
            std::memset(&value,0,sizeof(value));value.magic=GameInput::kMagic;value.version=GameInput::kVersion;
            value.source_pid=pid;value.owner_pid=GetCurrentProcessId();value.window=uintptr_t(window);value.generation=generation;value.heartbeat=GetTickCount64();
        }
        if(!Load(pid,dll))return false;
        {
            GameInput::Lock lock(*channel_);if(!lock.acquired)return false;
            auto& value=*channel_->data;
            if(!value.ready||!(value.hooks&GameInput::Raw)){error_=value.error[0]?value.error:"Source game input hooks unavailable";return false;}
            value.requested_active=1;value.heartbeat=GetTickCount64();
        }
        if(!Dispatch())return false;
        Log("started",Snapshot());
        return true;
    }
    void Release() {
        if(channel_&&channel_->data){
            {GameInput::Lock lock(*channel_);if(lock.acquired){channel_->data->requested_active=0;channel_->data->heartbeat=GetTickCount64();}}
            if(window_&&IsWindow(window_))Dispatch();
            Log("ended",Snapshot());
        }
        channel_.reset();window_=nullptr;buttons_.fill(false);has_point_=false;
    }
    bool Heartbeat() {
        if(!channel_)return true;GameInput::Lock lock(*channel_);
        if(!lock.acquired){error_="Source game input channel locked";return false;}
        auto& value=*channel_->data;value.heartbeat=GetTickCount64();
        if(value.error_code||!value.requested_active){error_=value.error[0]?value.error:"Source game input bridge stopped";return false;}
        return true;
    }
    GameInput::Shared Snapshot(){GameInput::Shared result{};if(channel_){GameInput::Lock lock(*channel_);if(lock.acquired)result=*channel_->data;}return result;}
    bool Probe(uint32_t key,uint32_t button) {
        if(!channel_)return false;
        {GameInput::Lock lock(*channel_);if(!lock.acquired)return false;channel_->data->probe_key=key;channel_->data->probe_mouse=button;}
        return Dispatch();
    }
    bool Send(const WindowInputEvent& event,const CaptureGeometry& geometry) {
        if(!channel_){error_="Source game input bridge unbound";return false;}
        GameInput::Event item{event.message,event.key,event.data,event.x,event.y};
        bool mouse=event.message>=WM_MOUSEFIRST&&event.message<=WM_MOUSELAST;
        int button=event.message==WM_LBUTTONDOWN||event.message==WM_LBUTTONUP?0:event.message==WM_RBUTTONDOWN||event.message==WM_RBUTTONUP?1:event.message==WM_MBUTTONDOWN||event.message==WM_MBUTTONUP?2:-1;
        bool up=event.message==WM_LBUTTONUP||event.message==WM_RBUTTONUP||event.message==WM_MBUTTONUP;
        if(mouse){
            bool inside=geometry.source_width&&geometry.source_height&&event.x>=0&&event.y>=0&&event.x<int(geometry.source_width)&&event.y<int(geometry.source_height);
            POINT point{};RECT frame{},client{};
            if(inside){
                if(FAILED(DwmGetWindowAttribute(window_,DWMWA_EXTENDED_FRAME_BOUNDS,&frame,sizeof(frame))))GetWindowRect(window_,&frame);
                point={frame.left+LONG(int64_t(event.x)*(frame.right-frame.left)/geometry.source_width),frame.top+LONG(int64_t(event.y)*(frame.bottom-frame.top)/geometry.source_height)};
                ScreenToClient(window_,&point);GetClientRect(window_,&client);inside=PtInRect(&client,point)!=FALSE;
            }
            if(!inside){if(!(up&&button>=0&&buttons_[button]&&has_point_))return true;point=last_;}
            if(up&&button>=0&&!buttons_[button])return true;
            last_=point;has_point_=true;item.x=point.x;item.y=point.y;
        }
        {
            GameInput::Lock lock(*channel_);if(!lock.acquired){error_="Source game input channel locked";return false;}
            auto& value=*channel_->data;
            if(value.error_code||!value.requested_active){error_=value.error[0]?value.error:"Source game input bridge stopped";return false;}
            if(value.write-value.read>=GameInput::kCapacity){GameInput::Error(value,"Source game input queue overflow");error_=value.error;return false;}
            value.heartbeat=GetTickCount64();value.events[value.write++%GameInput::kCapacity]=item;
        }
        bool ok=Dispatch();if(ok&&button>=0)buttons_[button]=!up;return ok;
    }
};
}
