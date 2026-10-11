#include "../../modules/media_screen/game_input_client.h"
#include <iostream>
#include <stdexcept>
#include <functional>
#include <dinput.h>

using namespace BetterEndfieldNext::MediaScreen;
namespace {
struct Fixture {
    uint64_t window,raw_window;
    LONG key_down,key_up,raw_down,raw_up,mouse_down,mouse_up,wheel,dx,dy,text;
    LONG async_down,key_state_down,keyboard_state_down,focus_proxy,raw_header,raw_size_retry;
    LONG left_control,right_control;
    LONG direct_key,direct_mouse,direct_buffered_key;
};
Fixture* fixture=nullptr;
IDirectInputDevice8W* direct_keyboard=nullptr;
IDirectInputDevice8W* direct_mouse=nullptr;
void Check(bool value,const char* label){if(!value)throw std::runtime_error(label);}
LRESULT CALLBACK Receiver(HWND hwnd,UINT message,WPARAM key,LPARAM data) {
    if(message==WM_ACTIVATEAPP&&key&&uintptr_t(hwnd)==fixture->window){auto window=fixture->window,raw_window=fixture->raw_window;std::memset(fixture,0,sizeof(Fixture));fixture->window=window;fixture->raw_window=raw_window;}
    if(message==WM_KEYDOWN&&key==VK_F15){InterlockedIncrement(&fixture->key_down);fixture->async_down=(GetAsyncKeyState(VK_F15)&0x8000)!=0;fixture->key_state_down=(GetKeyState(VK_F15)&0x8000)!=0;BYTE state[256]{};GetKeyboardState(state);fixture->keyboard_state_down=(state[VK_F15]&0x80)!=0;fixture->focus_proxy=uintptr_t(GetForegroundWindow())==fixture->window&&uintptr_t(GetFocus())==fixture->window;}
    if(message==WM_KEYUP&&key==VK_F15)InterlockedIncrement(&fixture->key_up);
    if(message==WM_KEYDOWN&&key==VK_RCONTROL){fixture->left_control=(GetKeyState(VK_CONTROL)&0x8000)!=0;fixture->right_control=(GetKeyState(VK_RCONTROL)&0x8000)!=0;}
    if(message==WM_KEYDOWN&&key==VK_F15){BYTE state[256]{};if(SUCCEEDED(direct_keyboard->GetDeviceState(sizeof(state),state)))fixture->direct_key=(state[DIK_F15]&0x80)!=0;
        DIDEVICEOBJECTDATA records[8]{};DWORD count=8;if(SUCCEEDED(direct_keyboard->GetDeviceData(sizeof(DIDEVICEOBJECTDATA),records,&count,0)))for(DWORD i=0;i<count;++i)if(records[i].dwOfs==DIK_F15&&records[i].dwData==0x80)++fixture->direct_buffered_key;}
    if(message==WM_CHAR)InterlockedIncrement(&fixture->text);
    if(message==WM_INPUT){
        RAWINPUTHEADER header{};UINT size=sizeof(header);auto got=GetRawInputData(reinterpret_cast<HRAWINPUT>(data),RID_HEADER,&header,&size,sizeof(header));
        if(got==sizeof(header))fixture->raw_header=1;
        BYTE small_buffer[1]{};size=1;got=GetRawInputData(reinterpret_cast<HRAWINPUT>(data),RID_INPUT,small_buffer,&size,sizeof(header));
        if(got==UINT(-1)&&GetLastError()==ERROR_INSUFFICIENT_BUFFER&&size>1)fixture->raw_size_retry=1;
        RAWINPUT raw{};size=sizeof(raw);got=GetRawInputData(reinterpret_cast<HRAWINPUT>(data),RID_INPUT,&raw,&size,sizeof(header));
        if(got!=UINT(-1)&&got>=sizeof(header)){
            if(raw.header.dwType==RIM_TYPEKEYBOARD&&raw.data.keyboard.VKey==VK_F15){if(raw.data.keyboard.Flags&RI_KEY_BREAK)InterlockedIncrement(&fixture->raw_up);else InterlockedIncrement(&fixture->raw_down);}
            if(raw.header.dwType==RIM_TYPEMOUSE){
                if(raw.data.mouse.usButtonFlags&RI_MOUSE_LEFT_BUTTON_DOWN){InterlockedIncrement(&fixture->mouse_down);DIMOUSESTATE state{};if(SUCCEEDED(direct_mouse->GetDeviceState(sizeof(state),&state)))fixture->direct_mouse=(state.rgbButtons[0]&0x80)!=0;
                    SetCursorPos(0,0);RECT clip{0,0,1,1};ClipCursor(&clip);SetCapture(hwnd);ReleaseCapture();}
                if(raw.data.mouse.usButtonFlags&RI_MOUSE_LEFT_BUTTON_UP)InterlockedIncrement(&fixture->mouse_up);
                if(raw.data.mouse.usButtonFlags&RI_MOUSE_WHEEL)InterlockedExchangeAdd(&fixture->wheel,SHORT(raw.data.mouse.usButtonData));
                InterlockedExchangeAdd(&fixture->dx,raw.data.mouse.lLastX);InterlockedExchangeAdd(&fixture->dy,raw.data.mouse.lLastY);
            }
        }
    }
    return DefWindowProcW(hwnd,message,key,data);
}
int Child(const wchar_t* name){
    HANDLE mapping=OpenFileMappingW(FILE_MAP_ALL_ACCESS,FALSE,name);fixture=static_cast<Fixture*>(MapViewOfFile(mapping,FILE_MAP_ALL_ACCESS,0,0,sizeof(Fixture)));if(!fixture)return 2;
    WNDCLASSW klass{};klass.lpfnWndProc=Receiver;klass.hInstance=GetModuleHandleW(nullptr);klass.lpszClassName=L"BE.GameInput.Fixture";RegisterClassW(&klass);
    HWND root=CreateWindowW(klass.lpszClassName,L"BE.GameInput.Fixture",WS_POPUP,100,100,400,300,nullptr,nullptr,klass.hInstance,nullptr);
    HWND raw=CreateWindowW(klass.lpszClassName,L"BE.GameInput.RawRecipient",0,0,0,0,0,HWND_MESSAGE,nullptr,klass.hInstance,nullptr);
    fixture->window=uintptr_t(root);fixture->raw_window=uintptr_t(raw);
    IDirectInput8W* input=nullptr;if(FAILED(DirectInput8Create(GetModuleHandleW(nullptr),DIRECTINPUT_VERSION,IID_IDirectInput8W,reinterpret_cast<void**>(&input),nullptr)))return 4;
    if(FAILED(input->CreateDevice(GUID_SysKeyboard,&direct_keyboard,nullptr))||FAILED(input->CreateDevice(GUID_SysMouse,&direct_mouse,nullptr)))return 5;
    direct_keyboard->SetDataFormat(&c_dfDIKeyboard);direct_mouse->SetDataFormat(&c_dfDIMouse);input->Release();
    RAWINPUTDEVICE devices[]={{1,6,RIDEV_INPUTSINK,raw},{1,2,RIDEV_INPUTSINK,raw}};if(!RegisterRawInputDevices(devices,2,sizeof(RAWINPUTDEVICE)))return 3;
    auto ready_name=std::wstring(name)+L".Ready",exit_name=std::wstring(name)+L".Exit";
    HANDLE ready=OpenEventW(EVENT_MODIFY_STATE,FALSE,ready_name.c_str()),stop=OpenEventW(SYNCHRONIZE,FALSE,exit_name.c_str());SetEvent(ready);
    while(WaitForSingleObject(stop,0)!=WAIT_OBJECT_0){MSG message{};while(PeekMessageW(&message,nullptr,0,0,PM_REMOVE))DispatchMessageW(&message);Sleep(2);}
    return 0;
}
struct ChildProcess {
    PROCESS_INFORMATION process{};HANDLE stop=nullptr,mapping=nullptr,ready=nullptr;Fixture* data=nullptr;
    ~ChildProcess(){if(stop)SetEvent(stop);if(process.hProcess){WaitForSingleObject(process.hProcess,5000);CloseHandle(process.hProcess);CloseHandle(process.hThread);}if(data)UnmapViewOfFile(data);if(mapping)CloseHandle(mapping);if(ready)CloseHandle(ready);if(stop)CloseHandle(stop);}
    void Start(){
        auto name=L"Local\\BE.GameInputTests."+std::to_wstring(GetCurrentProcessId());mapping=CreateFileMappingW(INVALID_HANDLE_VALUE,nullptr,PAGE_READWRITE,0,sizeof(Fixture),name.c_str());
        data=static_cast<Fixture*>(MapViewOfFile(mapping,FILE_MAP_ALL_ACCESS,0,0,sizeof(Fixture)));Check(data!=nullptr,"fixture channel");std::memset(data,0,sizeof(Fixture));
        ready=CreateEventW(nullptr,TRUE,FALSE,(name+L".Ready").c_str());stop=CreateEventW(nullptr,TRUE,FALSE,(name+L".Exit").c_str());
        wchar_t path[32768]{};GetModuleFileNameW(nullptr,path,32768);auto command=L"\""+std::wstring(path)+L"\" --fixture "+name;
        STARTUPINFOW startup{};startup.cb=sizeof(startup);startup.dwFlags=STARTF_USESHOWWINDOW;startup.wShowWindow=SW_HIDE;
        Check(CreateProcessW(path,command.data(),nullptr,nullptr,FALSE,CREATE_NO_WINDOW,nullptr,nullptr,&startup,&process),"start isolated fixture");
        Check(WaitForSingleObject(ready,5000)==WAIT_OBJECT_0,"fixture ready");
    }
};
struct LocalPlaceholder {
    std::filesystem::path path=std::filesystem::temp_directory_path()/(L"BE.GameInput.LocalPlaceholder."+std::to_wstring(GetCurrentProcessId())+L".dll");
    LocalPlaceholder(){
        HANDLE file=CreateFileW(path.c_str(),GENERIC_WRITE,0,nullptr,CREATE_NEW,FILE_ATTRIBUTE_TEMPORARY,nullptr);
        Check(file!=INVALID_HANDLE_VALUE,"create unrelated local bridge placeholder");CloseHandle(file);
    }
    ~LocalPlaceholder(){DeleteFileW(path.c_str());}
};
void Wait(GameInputClient& input,const std::function<bool()>& condition){auto end=GetTickCount64()+2000;while(!condition()&&GetTickCount64()<end){input.Heartbeat();Sleep(5);}if(!condition()){auto state=input.Snapshot();std::cerr<<"Raw diagnostics: dispatched="<<state.raw_dispatched<<" reads="<<state.raw_reads<<" targets="<<state.raw_keyboard_targets<<'/'<<state.raw_mouse_targets<<'\n';}Check(condition(),"fixture did not consume forwarded input");}
}
int wmain(int argc,wchar_t** argv){
    try{
        SetThreadDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
        if(argc==3&&std::wstring(argv[1])==L"--fixture")return Child(argv[2]);
        if(argc==5&&std::wstring(argv[1])==L"--source"){
            HWND target=reinterpret_cast<HWND>(uintptr_t(std::stoull(argv[2])));DWORD pid=0;GetWindowThreadProcessId(target,&pid);
            HWND foreground=GetForegroundWindow();GameInputClient input;Check(input.Bind(target,pid,argv[3]),input.Error().c_str());
            RECT frame{};if(FAILED(DwmGetWindowAttribute(target,DWMWA_EXTENDED_FRAME_BOUNDS,&frame,sizeof(frame))))GetWindowRect(target,&frame);
            CaptureGeometry geometry{uint32_t(frame.right-frame.left),uint32_t(frame.bottom-frame.top),0,0,uint32_t(frame.right-frame.left),uint32_t(frame.bottom-frame.top)};
            POINT corner{8,8};ClientToScreen(target,&corner);int x=corner.x-frame.left,y=corner.y-frame.top;
            auto wait_frames=[&]{for(int i=0;i<20;++i){Check(input.Heartbeat(),"source heartbeat");Sleep(10);}};
            auto probe=[&]{Check(input.Probe(VK_F15,0),"source engine query");return input.Snapshot();};
            wait_frames();auto before=probe();
            auto scan=MapVirtualKeyW(VK_F15,MAPVK_VK_TO_VSC_EX);int64_t data=1|(int64_t(scan&255)<<16);
            Check(input.Send({WM_MOUSEMOVE,0,0,x,y},geometry),"source corner position");
            Check(input.Send({WM_KEYDOWN,VK_F15,data,0,0},geometry),"source F15 down");wait_frames();auto down=probe();
            Check(input.Send({WM_LBUTTONDOWN,0,0,x,y},geometry),"source mouse down");wait_frames();auto click=probe();
            Check(input.Send({WM_LBUTTONUP,0,0,x,y},geometry)&&input.Send({WM_KEYUP,VK_F15,data|0xC0000000LL,0,0},geometry),"source input release");wait_frames();auto up=probe();
            input.Release();Check(GetForegroundWindow()==foreground,"source test changed actual foreground");
            FILE* file=nullptr;_wfopen_s(&file,argv[4],L"wb");Check(file!=nullptr,"source diagnostic output");
            auto write=[&](const char* label,const GameInput::Shared& value,bool comma){
                std::fprintf(file,"\"%s\":{\"hooks\":%u,\"processed\":%llu,\"raw_reads\":%llu,\"key_reads\":%llu,\"cursor_reads\":%llu,\"focus_reads\":%llu,\"raw_keyboard_targets\":%u,\"raw_mouse_targets\":%u,\"available\":%u,\"frame\":%d,\"unity_key\":%u,\"unity_mouse\":%u,\"rewired_key\":%u,\"rewired_mouse\":%u,\"unity_xy\":[%g,%g],\"rewired_xy\":[%g,%g]}%s\n",label,value.hooks,value.processed,value.raw_reads,value.key_reads,value.cursor_reads,value.focus_reads,value.raw_keyboard_targets,value.raw_mouse_targets,value.probe_available,value.probe_frame,value.probe_unity_keys,value.probe_unity_mouse,value.probe_rewired_keys,value.probe_rewired_mouse,value.probe_unity_x,value.probe_unity_y,value.probe_rewired_x,value.probe_rewired_y,comma?",":"");};
            std::fprintf(file,"{\"source_pid\":%lu,\"background\":%s,\"foreground_preserved\":true,\n",pid,foreground!=target?"true":"false");write("before",before,true);write("down",down,true);write("click",click,true);write("up",up,false);std::fprintf(file,"}\n");std::fclose(file);
            std::cout<<"SOURCE probe written; raw="<<up.raw_reads<<" Unity="<<down.probe_unity_keys<<'/'<<click.probe_unity_mouse<<" Rewired="<<down.probe_rewired_keys<<'/'<<click.probe_rewired_mouse<<" frames="<<before.probe_frame<<"->"<<up.probe_frame<<'\n';return 0;
        }
        Check(argc==1||argc==2,"unexpected bridge test arguments");auto dll=argc==2?std::wstring(argv[1]):GameInputClient::DefaultDll();
        ChildProcess child;child.Start();HWND foreground=GetForegroundWindow();RECT original_clip{};GetClipCursor(&original_clip);
        HWND target=reinterpret_cast<HWND>(uintptr_t(child.data->window));GameInputClient input;Check(input.Bind(target,child.process.dwProcessId,dll),input.Error().c_str());
        Check(GetForegroundWindow()==foreground,"game bridge activated source during bind");
        CaptureGeometry geometry{400,300,0,0,400,300};auto scan=MapVirtualKeyW(VK_F15,MAPVK_VK_TO_VSC_EX);int64_t key_data=1|(int64_t(scan&255)<<16);
        Check(input.Send({WM_KEYDOWN,VK_F15,key_data,0,0},geometry),"game key down");
        Wait(input,[&]{return child.data->raw_down==1;});
        Check(child.data->key_down==1&&child.data->async_down&&child.data->key_state_down&&child.data->keyboard_state_down,"virtual keyboard state missing");
        Check(child.data->focus_proxy&&child.data->raw_header&&child.data->raw_size_retry,"source focus/raw API contracts missing");
        Check(input.Send({WM_CHAR,'a',1,0,0},geometry),"game text");Check(child.data->text==1,"game text duplicated");
        Check(input.Send({WM_MOUSEMOVE,0,0,40,50},geometry)&&input.Send({WM_MOUSEMOVE,0,0,53,59},geometry),"game relative mouse");
        Check(input.Send({WM_MOUSEWHEEL,120,0,53,59},geometry),"game wheel");
        Wait(input,[&]{return child.data->dx==13&&child.data->dy==9&&child.data->wheel==120;});
        Check(input.Send({WM_LBUTTONDOWN,0,0,53,59},geometry),"game click");Wait(input,[&]{return child.data->mouse_down==1;});
        RECT clip{};GetClipCursor(&clip);Check(EqualRect(&clip,&original_clip),"source confined the physical cursor");
        int64_t control=1|(int64_t(0x1d)<<16)|(int64_t(1)<<24);Check(input.Send({WM_KEYDOWN,VK_RCONTROL,control,0,0},geometry),"right control");
        Check(child.data->left_control&&child.data->right_control,"modifier aggregation lost");
        auto snapshot=input.Snapshot();Check(snapshot.hooks==(GameInput::Raw|GameInput::KeyState|GameInput::Cursor|GameInput::Focus|GameInput::DirectInput),"bridge hook coverage missing");
        Check(child.data->direct_key&&child.data->direct_mouse&&child.data->direct_buffered_key==1,"existing DirectInput devices did not consume state and buffered input");
        Check(snapshot.raw_keyboard_targets==1&&snapshot.raw_mouse_targets==1&&snapshot.raw_reads>=4,"raw recipient not observed");
        input.Release();
        auto finish=GetTickCount64()+1000;while((child.data->raw_up!=1||child.data->mouse_up!=1)&&GetTickCount64()<finish)Sleep(5);
        Check(child.data->key_up==1&&child.data->raw_up==1&&child.data->mouse_up==1,"exit failed to release game keys/buttons");
        Check(GetForegroundWindow()==foreground,"game bridge stole actual foreground");
        // The already loaded source image is authoritative; this empty local
        // file has no export table and must never supply its initializer RVA.
        LocalPlaceholder local;
        Check(input.Bind(target,child.process.dwProcessId,local.path.wstring()),"reconnect must resolve the loaded source bridge export");
        Check(input.Send({WM_KEYDOWN,VK_F15,key_data,0,0},geometry),"reconnected source key down");
        Wait(input,[&]{return child.data->raw_down==1;});input.Release();
        Check(child.data->raw_up==1&&GetForegroundWindow()==foreground,"reconnected bridge release/foreground");
        std::cout<<"PASS game input: cross-process raw keyboard/mouse, real source API consumption, state, relative motion, wheel, modifiers, text, release, foreground/cursor preservation and loaded-image reconnect\n";
        return 0;
    }catch(const std::exception& error){std::cerr<<"FAIL "<<error.what()<<'\n';return 1;}
}
