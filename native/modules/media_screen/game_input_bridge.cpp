#include "game_input_protocol.h"
#include <MinHook.h>
#include <windowsx.h>
#include <dinput.h>
#include <array>
#include <atomic>
#include <deque>
#include <memory>
#include <mutex>
#include <unordered_map>
#include <vector>

using namespace BetterEndfieldNext::MediaScreen::GameInput;
namespace {
std::unique_ptr<Channel> channel;
std::mutex initialization, state_mutex, packet_mutex;
std::atomic_bool active{false};
std::atomic<uint64_t> lease{0};
HWND root=nullptr,virtual_focus=nullptr,virtual_capture=nullptr;
WNDPROC original_proc=nullptr;
UINT dispatch_message=0;
uint64_t generation=0;
uint32_t hook_mask=0;
std::array<BYTE,256> keys{},toggles{};
std::array<bool,3> buttons{};
POINT cursor{},last_client{};
bool have_position=false;
int virtual_cursor_count=0;
thread_local int virtual_notification_depth=0;
struct Packet { RAWINPUT input; uint64_t created; };
std::unordered_map<HRAWINPUT,Packet> packets;
std::atomic<uint64_t> packet_number{0};
struct RawTargets { std::vector<HWND> keyboard,mouse; HANDLE keyboard_device=nullptr,mouse_device=nullptr; };
RawTargets targets;
std::atomic_bool refresh_targets{true};
std::vector<void*> created_hooks;

decltype(&GetRawInputData) real_raw=nullptr;
decltype(&GetRawInputBuffer) real_raw_buffer=nullptr;
decltype(&RegisterRawInputDevices) real_register_raw=nullptr;
decltype(&GetAsyncKeyState) real_async=nullptr;
decltype(&GetKeyState) real_key=nullptr;
decltype(&GetKeyboardState) real_keyboard=nullptr;
decltype(&GetCursorPos) real_cursor=nullptr;
decltype(&SetCursorPos) real_set_cursor=nullptr;
decltype(&ClipCursor) real_clip=nullptr;
decltype(&ShowCursor) real_show=nullptr;
decltype(&GetForegroundWindow) real_foreground=nullptr;
decltype(&GetActiveWindow) real_active=nullptr;
decltype(&GetFocus) real_focus=nullptr;
decltype(&SetFocus) real_set_focus=nullptr;
decltype(&SetActiveWindow) real_set_active=nullptr;
decltype(&SetForegroundWindow) real_set_foreground=nullptr;
decltype(&GetCapture) real_capture=nullptr;
decltype(&SetCapture) real_set_capture=nullptr;
decltype(&ReleaseCapture) real_release_capture=nullptr;
decltype(&DefWindowProcW) real_default_w=nullptr;
decltype(&DefWindowProcA) real_default_a=nullptr;

bool Live() { return active.load(std::memory_order_acquire)&&GetTickCount64()<lease.load(std::memory_order_relaxed); }
void Count(uint64_t Shared::*field) { if(channel&&channel->data)InterlockedIncrement64(reinterpret_cast<volatile LONG64*>(&(channel->data->*field))); }
bool Owned(HWND window) { DWORD pid=0;return window&&GetWindowThreadProcessId(window,&pid)&&pid==GetCurrentProcessId(); }
uint32_t PhysicalKey(uint32_t key,int64_t data) {
    UINT scan=UINT((data>>16)&255);bool extended=(data&(int64_t(1)<<24))!=0;
    if(key==VK_SHIFT)return MapVirtualKeyW(scan,MAPVK_VSC_TO_VK_EX);
    if(key==VK_CONTROL)return extended?VK_RCONTROL:VK_LCONTROL;
    if(key==VK_MENU)return extended?VK_RMENU:VK_LMENU;
    return key;
}
void SetKey(uint32_t key,int64_t data,bool down) {
    key=PhysicalKey(key,data);if(key>=keys.size())return;
    if(down&&!keys[key]&&(key==VK_CAPITAL||key==VK_NUMLOCK||key==VK_SCROLL))toggles[key]^=1;
    keys[key]=down?0x80:0;
    keys[VK_SHIFT]=keys[VK_LSHIFT]|keys[VK_RSHIFT];keys[VK_CONTROL]=keys[VK_LCONTROL]|keys[VK_RCONTROL];keys[VK_MENU]=keys[VK_LMENU]|keys[VK_RMENU];
}
void FilterHardware(RAWINPUT& raw) {
    if(raw.header.dwType==RIM_TYPEKEYBOARD){raw.data.keyboard.VKey=255;raw.data.keyboard.MakeCode=0;raw.data.keyboard.Message=0;Count(&Shared::raw_filtered);}
    else if(raw.header.dwType==RIM_TYPEMOUSE){raw.data.mouse.usButtonFlags=0;raw.data.mouse.usButtonData=0;raw.data.mouse.lLastX=raw.data.mouse.lLastY=0;Count(&Shared::raw_filtered);}
}
UINT WINAPI RawData(HRAWINPUT handle,UINT command,LPVOID buffer,PUINT size,UINT header_size) {
    RAWINPUT raw{};bool found=false;
    {std::lock_guard lock(packet_mutex);auto it=packets.find(handle);if(it!=packets.end()){raw=it->second.input;found=true;}}
    if(found){
        if(!size||header_size!=sizeof(RAWINPUTHEADER)||(command!=RID_HEADER&&command!=RID_INPUT)){SetLastError(ERROR_INVALID_PARAMETER);return UINT(-1);}
        UINT required=command==RID_HEADER?sizeof(RAWINPUTHEADER):raw.header.dwSize;
        if(!buffer){*size=required;return 0;}
        if(*size<required){*size=required;SetLastError(ERROR_INSUFFICIENT_BUFFER);return UINT(-1);}
        std::memcpy(buffer,&raw,required);*size=required;Count(&Shared::raw_reads);return required;
    }
    auto result=real_raw(handle,command,buffer,size,header_size);
    if(Live()&&buffer&&command==RID_INPUT&&result!=UINT(-1)&&result>=sizeof(RAWINPUTHEADER))FilterHardware(*static_cast<RAWINPUT*>(buffer));
    return result;
}
UINT WINAPI RawBuffer(PRAWINPUT buffer,PUINT size,UINT header_size) {
    auto result=real_raw_buffer(buffer,size,header_size);
    if(Live()&&buffer&&result!=UINT(-1)){
        auto item=buffer;for(UINT i=0;i<result;++i){FilterHardware(*item);auto next=reinterpret_cast<uintptr_t>(item)+item->header.dwSize;item=reinterpret_cast<PRAWINPUT>((next+sizeof(void*)-1)&~uintptr_t(sizeof(void*)-1));}
    }
    return result;
}
BOOL WINAPI RegisterRaw(PCRAWINPUTDEVICE devices,UINT count,UINT size) { auto result=real_register_raw(devices,count,size);if(result)refresh_targets=true;return result; }
SHORT WINAPI AsyncKey(int key) { if(!Live())return real_async(key);Count(&Shared::key_reads);std::lock_guard lock(state_mutex);return key>=0&&key<256?SHORT(keys[key]?0x8000:0):0; }
SHORT WINAPI KeyState(int key) { if(!Live())return real_key(key);Count(&Shared::key_reads);std::lock_guard lock(state_mutex);return key>=0&&key<256?SHORT((keys[key]?0x8000:0)|toggles[key]):0; }
BOOL WINAPI KeyboardState(PBYTE data) { if(!Live())return real_keyboard(data);if(!data){SetLastError(ERROR_INVALID_PARAMETER);return FALSE;}Count(&Shared::key_reads);std::lock_guard lock(state_mutex);for(size_t i=0;i<keys.size();++i)data[i]=keys[i]|toggles[i];return TRUE; }
BOOL WINAPI CursorPos(LPPOINT point) { if(!Live())return real_cursor(point);if(!point){SetLastError(ERROR_INVALID_PARAMETER);return FALSE;}Count(&Shared::cursor_reads);std::lock_guard lock(state_mutex);*point=cursor;return TRUE; }
BOOL WINAPI SetCursor(int x,int y) { if(!Live())return real_set_cursor(x,y);std::lock_guard lock(state_mutex);cursor={x,y};return TRUE; }
BOOL WINAPI Clip(const RECT* rect) { return Live()?TRUE:real_clip(rect); }
int WINAPI Show(BOOL show) { if(!Live())return real_show(show);std::lock_guard lock(state_mutex);return show?++virtual_cursor_count:--virtual_cursor_count; }
HWND WINAPI Foreground() { if(!Live())return real_foreground();Count(&Shared::focus_reads);return root; }
HWND WINAPI Active() { if(!Live())return real_active();Count(&Shared::focus_reads);return root; }
HWND WINAPI Focus() { if(!Live())return real_focus();Count(&Shared::focus_reads);std::lock_guard lock(state_mutex);return virtual_focus?virtual_focus:root; }
HWND WINAPI SetFocusWindow(HWND window) { if(!Live()||(!Owned(window)&&window))return real_set_focus(window);std::lock_guard lock(state_mutex);HWND previous=virtual_focus;virtual_focus=window;return previous; }
HWND WINAPI SetActiveWindowVirtual(HWND window) { if(!Live()||(!Owned(window)&&window))return real_set_active(window);return root; }
BOOL WINAPI SetForegroundWindowVirtual(HWND window) { if(!Live()||!Owned(window))return real_set_foreground(window);return TRUE; }
HWND WINAPI Capture() { if(!Live())return real_capture();std::lock_guard lock(state_mutex);return virtual_capture; }
HWND WINAPI SetCaptureWindow(HWND window) { if(!Live()||!Owned(window))return real_set_capture(window);std::lock_guard lock(state_mutex);HWND previous=virtual_capture;virtual_capture=window;return previous; }
BOOL WINAPI ReleaseCaptureVirtual() { if(!Live())return real_release_capture();std::lock_guard lock(state_mutex);virtual_capture=nullptr;return TRUE; }
bool VirtualFocusNotification(UINT message){return virtual_notification_depth&&(message==WM_ACTIVATE||message==WM_ACTIVATEAPP||message==WM_SETFOCUS||message==WM_KILLFOCUS);}
LRESULT WINAPI DefaultW(HWND window,UINT message,WPARAM key,LPARAM data){return VirtualFocusNotification(message)?0:real_default_w(window,message,key,data);}
LRESULT WINAPI DefaultA(HWND window,UINT message,WPARAM key,LPARAM data){return VirtualFocusNotification(message)?0:real_default_a(window,message,key,data);}

template<class T> bool Hook(T target,T callback,T& original) {
    if(!target)return false;auto address=reinterpret_cast<void*>(target);
    if(MH_CreateHook(address,reinterpret_cast<void*>(callback),reinterpret_cast<void**>(&original))!=MH_OK)return false;
    created_hooks.push_back(address);return true;
}
#include "game_input_directinput.inc"
bool InstallHooks() {
    if(MH_Initialize()!=MH_OK)return false;
    bool ok=Hook(&GetRawInputData,&RawData,real_raw)&&Hook(&GetRawInputBuffer,&RawBuffer,real_raw_buffer)&&Hook(&RegisterRawInputDevices,&RegisterRaw,real_register_raw);
    if(ok)hook_mask|=Raw;
    ok=ok&&Hook(&GetAsyncKeyState,&AsyncKey,real_async)&&Hook(&GetKeyState,&KeyState,real_key)&&Hook(&GetKeyboardState,&KeyboardState,real_keyboard);
    if(ok)hook_mask|=BetterEndfieldNext::MediaScreen::GameInput::KeyState;
    ok=ok&&Hook(&GetCursorPos,&CursorPos,real_cursor)&&Hook(&SetCursorPos,&SetCursor,real_set_cursor)&&Hook(&ClipCursor,&Clip,real_clip)&&Hook(&ShowCursor,&Show,real_show);
    if(ok)hook_mask|=Cursor;
    ok=ok&&Hook(&GetForegroundWindow,&Foreground,real_foreground)&&Hook(&GetActiveWindow,&Active,real_active)&&Hook(&GetFocus,&Focus,real_focus)&&
        Hook(&SetFocus,&SetFocusWindow,real_set_focus)&&Hook(&SetActiveWindow,&SetActiveWindowVirtual,real_set_active)&&Hook(&SetForegroundWindow,&SetForegroundWindowVirtual,real_set_foreground)&&
        Hook(&GetCapture,&Capture,real_capture)&&Hook(&SetCapture,&SetCaptureWindow,real_set_capture)&&Hook(&ReleaseCapture,&ReleaseCaptureVirtual,real_release_capture);
    ok=ok&&Hook(&DefWindowProcW,&DefaultW,real_default_w)&&Hook(&DefWindowProcA,&DefaultA,real_default_a);
    if(ok)hook_mask|=BetterEndfieldNext::MediaScreen::GameInput::Focus;
    if(ok&&InstallDirectInput())hook_mask|=DirectInput;
    if(ok){for(auto target:created_hooks)MH_QueueEnableHook(target);ok=MH_ApplyQueued()==MH_OK;}
    if(!ok){for(auto target:created_hooks){MH_DisableHook(target);MH_RemoveHook(target);}created_hooks.clear();hook_mask=0;MH_Uninitialize();}
    return ok;
}
void DiscoverTargets() {
    if(!refresh_targets.exchange(false))return;
    RawTargets value;UINT count=0;
    if(GetRegisteredRawInputDevices(nullptr,&count,sizeof(RAWINPUTDEVICE))!=UINT(-1)&&count){
        std::vector<RAWINPUTDEVICE> list(count);
        if(GetRegisteredRawInputDevices(list.data(),&count,sizeof(RAWINPUTDEVICE))!=UINT(-1))for(auto& item:list){
            if(item.usUsagePage!=1)continue;HWND window=item.hwndTarget?item.hwndTarget:root;if(!Owned(window))continue;
            auto* target=item.usUsage==6?&value.keyboard:item.usUsage==2?&value.mouse:nullptr;if(target)target->push_back(window);
        }
    }
    if(value.keyboard.empty())value.keyboard.push_back(root);if(value.mouse.empty())value.mouse.push_back(root);
    count=0;if(GetRawInputDeviceList(nullptr,&count,sizeof(RAWINPUTDEVICELIST))!=UINT(-1)&&count){
        std::vector<RAWINPUTDEVICELIST> list(count);if(GetRawInputDeviceList(list.data(),&count,sizeof(RAWINPUTDEVICELIST))!=UINT(-1))for(auto& item:list){
            if(item.dwType==RIM_TYPEKEYBOARD&&!value.keyboard_device)value.keyboard_device=item.hDevice;
            if(item.dwType==RIM_TYPEMOUSE&&!value.mouse_device)value.mouse_device=item.hDevice;
        }
    }
    targets=std::move(value);
    if(channel&&channel->data){channel->data->raw_keyboard_targets=uint32_t(targets.keyboard.size());channel->data->raw_mouse_targets=uint32_t(targets.mouse.size());}
}
void RawEvent(RAWINPUT raw) {
    DiscoverTargets();raw.header.dwSize=sizeof(RAWINPUTHEADER)+(raw.header.dwType==RIM_TYPEKEYBOARD?sizeof(RAWKEYBOARD):sizeof(RAWMOUSE));raw.header.wParam=RIM_INPUT;
    raw.header.hDevice=raw.header.dwType==RIM_TYPEKEYBOARD?targets.keyboard_device:targets.mouse_device;
    const auto& receivers=raw.header.dwType==RIM_TYPEKEYBOARD?targets.keyboard:targets.mouse;
    for(HWND window:receivers){
        auto number=++packet_number;HRAWINPUT handle=reinterpret_cast<HRAWINPUT>(uintptr_t(0xBE71000000000000ULL|(number&0x0000FFFFFFFFFFFFULL)));
        {std::lock_guard lock(packet_mutex);packets[handle]={raw,GetTickCount64()};}
        // Each raw-input recipient consumes its own token, on its own thread.
        if(window==root)CallWindowProcW(original_proc,window,WM_INPUT,RIM_INPUT,reinterpret_cast<LPARAM>(handle));
        else PostMessageW(window,WM_INPUT,RIM_INPUT,reinterpret_cast<LPARAM>(handle));
        Count(&Shared::raw_dispatched);
    }
}
void Forward(const Event& event) {
    bool down=event.message==WM_KEYDOWN||event.message==WM_SYSKEYDOWN;
    bool up=event.message==WM_KEYUP||event.message==WM_SYSKEYUP;
    if(down||up){
        if(!event.key||event.key>=256)return;
        {std::lock_guard lock(state_mutex);SetKey(event.key,event.data,down);DirectKey(event,down);}
        RAWINPUT raw{};raw.header.dwType=RIM_TYPEKEYBOARD;raw.data.keyboard.VKey=USHORT(event.key);raw.data.keyboard.MakeCode=USHORT((event.data>>16)&255);
        if(!raw.data.keyboard.MakeCode)raw.data.keyboard.MakeCode=USHORT(MapVirtualKeyW(event.key,MAPVK_VK_TO_VSC));
        raw.data.keyboard.Flags=up?RI_KEY_BREAK:RI_KEY_MAKE;if(event.data&(int64_t(1)<<24))raw.data.keyboard.Flags|=RI_KEY_E0;
        raw.data.keyboard.Message=event.message;RawEvent(raw);
        CallWindowProcW(original_proc,root,event.message,event.key,LPARAM(event.data));return;
    }
    if(event.message==WM_CHAR||event.message==WM_SYSCHAR){CallWindowProcW(original_proc,root,event.message,event.key,LPARAM(event.data));return;}
    RAWINPUT raw{};raw.header.dwType=RIM_TYPEMOUSE;
    WPARAM flags=0;POINT point{event.x,event.y};
    {
        std::lock_guard lock(state_mutex);
        POINT screen=point;ClientToScreen(root,&screen);cursor=screen;
        if(event.message==WM_MOUSEMOVE&&have_position){raw.data.mouse.lLastX=point.x-last_client.x;raw.data.mouse.lLastY=point.y-last_client.y;}
        last_client=point;have_position=true;
        struct Button { UINT down,up; USHORT raw_down,raw_up; int vk; WPARAM flag; };
        constexpr Button table[]={{WM_LBUTTONDOWN,WM_LBUTTONUP,RI_MOUSE_LEFT_BUTTON_DOWN,RI_MOUSE_LEFT_BUTTON_UP,VK_LBUTTON,MK_LBUTTON},{WM_RBUTTONDOWN,WM_RBUTTONUP,RI_MOUSE_RIGHT_BUTTON_DOWN,RI_MOUSE_RIGHT_BUTTON_UP,VK_RBUTTON,MK_RBUTTON},{WM_MBUTTONDOWN,WM_MBUTTONUP,RI_MOUSE_MIDDLE_BUTTON_DOWN,RI_MOUSE_MIDDLE_BUTTON_UP,VK_MBUTTON,MK_MBUTTON}};
        for(int i=0;i<3;++i){if(event.message==table[i].down){buttons[i]=true;keys[table[i].vk]=0x80;raw.data.mouse.usButtonFlags=table[i].raw_down;}if(event.message==table[i].up){buttons[i]=false;keys[table[i].vk]=0;raw.data.mouse.usButtonFlags=table[i].raw_up;}if(buttons[i])flags|=table[i].flag;}
        if(keys[VK_CONTROL])flags|=MK_CONTROL;if(keys[VK_SHIFT])flags|=MK_SHIFT;
        if(event.message==WM_MOUSEWHEEL||event.message==WM_MOUSEHWHEEL){raw.data.mouse.usButtonFlags=event.message==WM_MOUSEWHEEL?RI_MOUSE_WHEEL:RI_MOUSE_HWHEEL;raw.data.mouse.usButtonData=USHORT(event.key);flags=MAKEWPARAM(flags,SHORT(event.key));point=screen;}
        DirectMouse(raw.data.mouse);
    }
    RawEvent(raw);CallWindowProcW(original_proc,root,event.message,flags,MAKELPARAM(point.x,point.y));
}
void NotifyFocus(bool focus) {
    ++virtual_notification_depth;
    CallWindowProcW(original_proc,root,WM_ACTIVATEAPP,focus,GetCurrentThreadId());
    CallWindowProcW(original_proc,root,WM_ACTIVATE,focus?WA_ACTIVE:WA_INACTIVE,0);
    CallWindowProcW(original_proc,root,focus?WM_SETFOCUS:WM_KILLFOCUS,0,0);
    --virtual_notification_depth;
}
void End() {
    if(!active.load())return;
    std::vector<Event> release;
    {std::lock_guard lock(state_mutex);
        for(uint32_t key=1;key<256;++key)if(keys[key]&&key!=VK_SHIFT&&key!=VK_CONTROL&&key!=VK_MENU&&key!=VK_LBUTTON&&key!=VK_RBUTTON&&key!=VK_MBUTTON){
            auto scan=MapVirtualKeyW(key,MAPVK_VK_TO_VSC_EX);int64_t data=1|(int64_t(scan&255)<<16)|((scan&0xff00)?int64_t(1)<<24:0)|0xC0000000LL;
            release.push_back({WM_KEYUP,key,data,0,0});
        }
        constexpr UINT up[]={WM_LBUTTONUP,WM_RBUTTONUP,WM_MBUTTONUP};for(int i=0;i<3;++i)if(buttons[i])release.push_back({up[i],0,0,last_client.x,last_client.y});
    }
    // Keep proxy state enabled while the recipient processes releases.
    lease=GetTickCount64()+500;for(auto& event:release)Forward(event);
    active=false;lease=0;
    {std::lock_guard lock(state_mutex);keys.fill(0);buttons.fill(false);have_position=false;virtual_capture=nullptr;virtual_focus=nullptr;ResetDirectInput();}
    if(root&&IsWindow(root))NotifyFocus(real_foreground()==root);
}
void Begin() {
    End();
    {std::lock_guard lock(state_mutex);keys.fill(0);buttons.fill(false);have_position=false;virtual_focus=root;virtual_capture=nullptr;
        ResetDirectInput();
        for(size_t i=0;i<toggles.size();++i)toggles[i]=BYTE(real_key(int(i))&1);
        RECT rect{};GetClientRect(root,&rect);cursor={(rect.right-rect.left)/2,(rect.bottom-rect.top)/2};ClientToScreen(root,&cursor);
        virtual_cursor_count=0;
    }
    refresh_targets=true;lease=GetTickCount64()+2000;active=true;NotifyFocus(true);
}

using ResolveIcall=void*(*)(const char*);
int UnityKey(uint32_t vk) {
    if(vk>='A'&&vk<='Z')return int(vk-'A'+'a');if(vk>='0'&&vk<='9')return int(vk);
    if(vk>=VK_F1&&vk<=VK_F15)return 282+int(vk-VK_F1);
    return vk==VK_SPACE?32:vk==VK_RETURN?13:vk==VK_ESCAPE?27:vk==VK_LEFT?276:vk==VK_RIGHT?275:vk==VK_UP?273:vk==VK_DOWN?274:0;
}
void Probe(Shared& value) {
    value.probe_available=0;value.probe_unity_keys=value.probe_unity_mouse=value.probe_rewired_keys=value.probe_rewired_mouse=0;
    HMODULE game=GetModuleHandleW(L"GameAssembly.dll");auto resolve=reinterpret_cast<ResolveIcall>(game?GetProcAddress(game,"il2cpp_resolve_icall"):nullptr);if(!resolve)return;
    auto frame=reinterpret_cast<int(*)()>(resolve("UnityEngine.Time::get_frameCount"));
    auto key=reinterpret_cast<bool(*)(int)>(resolve("UnityEngine.Input::GetKeyInt"));
    auto mouse=reinterpret_cast<bool(*)(int)>(resolve("UnityEngine.Input::GetMouseButton"));
    auto position=reinterpret_cast<void(*)(float*)>(resolve("UnityEngine.Input::get_mousePosition_Injected"));
    if(frame)value.probe_frame=frame();if(key&&mouse){value.probe_available|=1;value.probe_unity_keys=key(UnityKey(value.probe_key));value.probe_unity_mouse=mouse(int(value.probe_mouse));}
    if(position){float point[3]{};position(point);value.probe_unity_x=point[0];value.probe_unity_y=point[1];}
    auto domain_get=reinterpret_cast<void*(*)()>(GetProcAddress(game,"il2cpp_domain_get"));
    auto assemblies_get=reinterpret_cast<void**(*)(void*,size_t*)>(GetProcAddress(game,"il2cpp_domain_get_assemblies"));
    auto image_get=reinterpret_cast<void*(*)(void*)>(GetProcAddress(game,"il2cpp_assembly_get_image"));
    auto class_get=reinterpret_cast<void*(*)(void*,const char*,const char*)>(GetProcAddress(game,"il2cpp_class_from_name"));
    auto method_get=reinterpret_cast<void*(*)(void*,const char*,int)>(GetProcAddress(game,"il2cpp_class_get_method_from_name"));
    auto invoke=reinterpret_cast<void*(*)(void*,void*,void**,void**)>(GetProcAddress(game,"il2cpp_runtime_invoke"));
    auto object_class=reinterpret_cast<void*(*)(void*)>(GetProcAddress(game,"il2cpp_object_get_class"));
    auto unbox=reinterpret_cast<void*(*)(void*)>(GetProcAddress(game,"il2cpp_object_unbox"));
    auto methods_get=reinterpret_cast<void*(*)(void*,void**)>(GetProcAddress(game,"il2cpp_class_get_methods"));
    auto name_get=reinterpret_cast<const char*(*)(void*)>(GetProcAddress(game,"il2cpp_method_get_name"));
    auto param_count=reinterpret_cast<uint32_t(*)(void*)>(GetProcAddress(game,"il2cpp_method_get_param_count"));
    auto param_get=reinterpret_cast<void*(*)(void*,uint32_t)>(GetProcAddress(game,"il2cpp_method_get_param"));
    auto type_name=reinterpret_cast<char*(*)(void*)>(GetProcAddress(game,"il2cpp_type_get_name"));
    auto free_name=reinterpret_cast<void(*)(void*)>(GetProcAddress(game,"il2cpp_free"));
    if(!domain_get||!assemblies_get||!image_get||!class_get||!method_get||!invoke||!object_class||!unbox||!methods_get||!name_get||!param_count||!param_get||!type_name||!free_name)return;
    auto call=[&](void* method,void* self,void** args=nullptr)->void*{if(!method)return nullptr;void* exception=nullptr;auto result=invoke(method,self,args,&exception);return exception?nullptr:result;};
    auto typed_method=[&](void* type,const char* name,const char* parameter)->void*{
        void* iterator=nullptr;
        while(auto method=methods_get(type,&iterator))if(!std::strcmp(name_get(method),name)&&param_count(method)==1){
            char* actual=type_name(param_get(method,0));bool match=actual&&!std::strcmp(actual,parameter);if(actual)free_name(actual);if(match)return method;
        }
        return nullptr;
    };
    size_t count=0;auto assemblies=assemblies_get(domain_get(),&count);void* klass=nullptr;
    for(size_t i=0;i<count&&!klass;++i)klass=class_get(image_get(assemblies[i]),"Rewired","ReInput");if(!klass)return;
    auto controllers=call(method_get(klass,"get_controllers",0),nullptr);if(!controllers)return;
    auto keyboard=call(method_get(object_class(controllers),"get_Keyboard",0),controllers);
    auto mouse_device=call(method_get(object_class(controllers),"get_Mouse",0),controllers);
    int key_code=UnityKey(value.probe_key),button=int(value.probe_mouse);void* key_args[]{&key_code};void* mouse_args[]{&button};
    if(keyboard){auto result=call(typed_method(object_class(keyboard),"GetKey","UnityEngine.KeyCode"),keyboard,key_args);if(result){value.probe_available|=2;value.probe_rewired_keys=*static_cast<uint8_t*>(unbox(result))!=0;}}
    if(mouse_device){auto result=call(typed_method(object_class(mouse_device),"GetButton","System.Int32"),mouse_device,mouse_args);if(result){value.probe_available|=4;value.probe_rewired_mouse=*static_cast<uint8_t*>(unbox(result))!=0;}
        auto point=call(method_get(object_class(mouse_device),"get_screenPosition",0),mouse_device);if(point){auto vec=static_cast<float*>(unbox(point));value.probe_rewired_x=vec[0];value.probe_rewired_y=vec[1];}}
}
LRESULT CALLBACK WindowProc(HWND hwnd,UINT message,WPARAM wparam,LPARAM lparam) {
    if(message==dispatch_message){
        bool wanted=false;uint64_t next_generation=0,heartbeat=0;std::vector<Event> events;uint32_t probe_key=0,probe_mouse=0;
        {
            Lock lock(*channel);if(!lock.acquired)return 0;auto& value=*channel->data;
            if(value.magic!=kMagic||value.version!=kVersion||value.source_pid!=GetCurrentProcessId()||value.window!=uintptr_t(root))return 0;
            wanted=value.requested_active!=0;next_generation=value.generation;heartbeat=value.heartbeat;
            if(value.write<value.read||value.write-value.read>kCapacity){Error(value,"Invalid game input queue");wanted=false;}
            else while(value.read<value.write)events.push_back(value.events[value.read++%kCapacity]);
            probe_key=value.probe_key;probe_mouse=value.probe_mouse;
            value.probe_key=0;
        }
        if(!wanted||GetTickCount64()>heartbeat+2000){End();return 1;}
        if(generation!=next_generation||!active){generation=next_generation;Begin();}
        lease=heartbeat+2000;
        for(const auto& event:events){Forward(event);Count(&Shared::processed);}
        if(probe_key){Shared result{};result.probe_key=probe_key;result.probe_mouse=probe_mouse;Probe(result);
            Lock lock(*channel);if(lock.acquired){auto& value=*channel->data;value.probe_available=result.probe_available;value.probe_frame=result.probe_frame;
                value.probe_unity_keys=result.probe_unity_keys;value.probe_unity_mouse=result.probe_unity_mouse;value.probe_rewired_keys=result.probe_rewired_keys;value.probe_rewired_mouse=result.probe_rewired_mouse;
                value.probe_unity_x=result.probe_unity_x;value.probe_unity_y=result.probe_unity_y;value.probe_rewired_x=result.probe_rewired_x;value.probe_rewired_y=result.probe_rewired_y;}}
        return 1;
    }
    if(Live()&&(message==WM_KILLFOCUS||(message==WM_ACTIVATEAPP&&!wparam)||(message==WM_ACTIVATE&&LOWORD(wparam)==WA_INACTIVE)))return 0;
    return CallWindowProcW(original_proc,hwnd,message,wparam,lparam);
}
DWORD WINAPI Watchdog(void*) {
    for(;;){
        if(!IsWindow(root)){active=false;return 0;}
        bool expire=false;
        {Lock lock(*channel,10);if(lock.acquired){auto& value=*channel->data;
            if(active&&value.requested_active&&GetTickCount64()<=value.heartbeat+2000)lease=value.heartbeat+2000;
            if(active&&(!value.requested_active||GetTickCount64()>value.heartbeat+2000)){value.requested_active=0;expire=true;}
        }}
        if(expire)PostMessageW(root,dispatch_message,0,0);
        {std::lock_guard lock(packet_mutex);auto now=GetTickCount64();for(auto it=packets.begin();it!=packets.end();)if(now>it->second.created+3000)it=packets.erase(it);else ++it;}
        Sleep(50);
    }
}
}
extern "C" __declspec(dllexport) DWORD WINAPI BE_GameInputInitialize(void*) {
    std::lock_guard init(initialization);
    if(!channel)channel=std::make_unique<Channel>(GetCurrentProcessId(),false);
    if(!channel->data)return 1;
    {
        Lock lock(*channel);if(!lock.acquired)return 2;auto& value=*channel->data;
        if(value.magic!=kMagic||value.version!=kVersion||value.source_pid!=GetCurrentProcessId())return 3;
        HWND wanted=reinterpret_cast<HWND>(uintptr_t(value.window));if(!Owned(wanted))return 4;
        if(root&&root!=wanted){Error(value,"Source game window changed; restart the source before retrying");return 5;}
        root=wanted;
    }
    if(!original_proc){
        if(!InstallHooks()){Lock lock(*channel);if(lock.acquired)Error(*channel->data,"Cannot install game input hooks");return 6;}
        dispatch_message=RegisterWindowMessageW(kDispatchName);
        original_proc=reinterpret_cast<WNDPROC>(SetWindowLongPtrW(root,GWLP_WNDPROC,reinterpret_cast<LONG_PTR>(&WindowProc)));
        if(!original_proc)return 7;
        HANDLE thread=CreateThread(nullptr,0,&Watchdog,nullptr,0,nullptr);if(!thread)return 8;CloseHandle(thread);
    }
    Lock lock(*channel);if(!lock.acquired)return 9;channel->data->hooks=hook_mask;channel->data->ready=1;return 0;
}
BOOL WINAPI DllMain(HINSTANCE,DWORD,LPVOID){return TRUE;}
