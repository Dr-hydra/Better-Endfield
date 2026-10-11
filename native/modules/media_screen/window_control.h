// Included in the module's anonymous namespace; all Unity calls run on its frame
// thread. WndProc only collects directed input, never invokes managed methods.
BE_ResolvedClassV1 control_mesh_class{};
std::atomic_bool control_active{false}, control_queue_failed{false};
std::atomic<void*> control_input_manager{nullptr};
bool control_hooks_ready=false;
HWND control_game_window=nullptr;
WNDPROC control_original_proc=nullptr;
int32_t control_saved_lock=0;
uint8_t control_saved_visible=0;
uint32_t control_manager_handle=0, control_request_handle=0;
void* control_request=nullptr;
void* control_cursor_manager=nullptr;
CaptureGeometry control_geometry{};
struct ControlSurface { void* transform; std::vector<Vec3> vertices; std::vector<Vec2> uvs; std::vector<int32_t> triangles; Vec2 scale, offset; };
std::vector<ControlSurface> control_surfaces;
std::mutex control_events_mutex;
std::deque<WindowInputEvent> control_events;
int control_last_x=0, control_last_y=0;
bool control_has_point=false;
std::array<bool,3> control_buttons{};
std::atomic_bool control_draining{false};
unsigned control_quiet_frames=0;

bool ControlBlocks() {
    return control_active.load()||control_draining.load();
}
bool ControlCollects() {
    if (!control_active.load()) return false;
    DWORD pid=0; GetWindowThreadProcessId(GetForegroundWindow(),&pid);
    return pid==GetCurrentProcessId();
}
#include "control_input_shield.h"
void RestoreControlWindow(){
    if(control_game_window&&control_original_proc&&IsWindow(control_game_window)&&reinterpret_cast<WNDPROC>(GetWindowLongPtrW(control_game_window,GWLP_WNDPROC))==ControlWindowProc)
        SetWindowLongPtrW(control_game_window,GWLP_WNDPROC,reinterpret_cast<LONG_PTR>(control_original_proc));
    control_game_window=nullptr;
}
void AdvanceControlDrain(){
    if(!control_draining||control_active)return;
    bool held=false;for(int key=1;key<256;++key)if(RealKeyDown(key)){held=true;break;}
    control_quiet_frames=held?0:control_quiet_frames+1;
    if(control_quiet_frames>=2||!control_game_window||!IsWindow(control_game_window)){control_draining=false;control_quiet_frames=0;RestoreControlWindow();}
}
LRESULT CALLBACK ControlWindowProc(HWND hwnd, UINT message, WPARAM wparam, LPARAM lparam) {
    if (ControlBlocks()) {
        if (message==WM_SETCURSOR) { SetCursor(LoadCursorW(nullptr,MAKEINTRESOURCEW(32512))); return TRUE; }
        if(message==WM_SYSCOMMAND&&(wparam&0xfff0)==SC_KEYMENU)return 0;
        if (message==WM_INPUT) return DefWindowProcW(hwnd,message,wparam,lparam);
        bool keyboard=message==WM_KEYDOWN||message==WM_KEYUP||message==WM_SYSKEYDOWN||message==WM_SYSKEYUP||message==WM_CHAR||message==WM_SYSCHAR;
        bool mouse=message==WM_MOUSEMOVE||message==WM_LBUTTONDOWN||message==WM_LBUTTONUP||message==WM_RBUTTONDOWN||message==WM_RBUTTONUP||
            message==WM_MBUTTONDOWN||message==WM_MBUTTONUP||message==WM_MOUSEWHEEL||message==WM_MOUSEHWHEEL;
        if (keyboard || mouse) {
            if(!ControlCollects()){
                // Let native caches observe releases; game readers remain
                // neutral until the physical keys are up for two frames.
                if(message==WM_KEYUP||message==WM_SYSKEYUP||message==WM_LBUTTONUP||message==WM_RBUTTONUP||message==WM_MBUTTONUP)
                    return CallWindowProcW(control_original_proc,hwnd,message,wparam,lparam);
                return 0;
            }
            // Reserve media hotkeys and their text; modifier events may be sent,
            // but the broker releases them when the control generation changes.
            bool reserved=false;
            if (keyboard) {
                std::lock_guard lock(config_mutex);
                for (int binding:{desired.hotkey,desired.pause_hotkey,desired.control_hotkey})
                    if (binding && (int(wparam)==Input::BaseKey(binding) || ((message==WM_CHAR||message==WM_SYSCHAR) && (wparam=='-'||wparam=='_'))) && ExactModifiers(binding)) reserved=true;
            }
            if (!reserved) {
                POINT point{GET_X_LPARAM(lparam),GET_Y_LPARAM(lparam)};
                if (message==WM_MOUSEWHEEL||message==WM_MOUSEHWHEEL) ScreenToClient(hwnd,&point);
                WindowInputEvent event{message,uint32_t(mouse?GET_WHEEL_DELTA_WPARAM(wparam):wparam),int64_t(lparam),point.x,point.y};
                std::lock_guard lock(control_events_mutex);
                if (message==WM_MOUSEMOVE && !control_events.empty() && control_events.back().message==WM_MOUSEMOVE) control_events.back()=event;
                else if (control_events.size()<256) control_events.push_back(event); else control_queue_failed.store(true);
            }
            return 0;
        }
        if (message==WM_LBUTTONDBLCLK||message==WM_RBUTTONDBLCLK||message==WM_MBUTTONDBLCLK||message==WM_XBUTTONDOWN||message==WM_XBUTTONUP||message==WM_XBUTTONDBLCLK) return 0;
    }
    return CallWindowProcW(control_original_proc,hwnd,message,wparam,lparam);
}
void EndControl(bool unity=true) {
    bool was_active=control_active.exchange(false);
    if (!was_active && !control_game_window && !control_manager_handle && !control_request_handle && control_surfaces.empty()) return;
    capture.Control(false);
    if(was_active){control_draining=true;control_quiet_frames=0;}
    if(!control_draining||!control_game_window||!IsWindow(control_game_window))RestoreControlWindow();
    if (unity && was_active) {
        try {
            if (control_manager_handle && control_request) {
                uint8_t show=0; void* args[]{control_request,&show}; Invoke(ControlCursorRequest,control_cursor_manager,args);
            }
            SetValue<int32_t>(ControlCursorSetLock,nullptr,control_saved_lock);
            SetValue<uint8_t>(ControlCursorSetVisible,nullptr,control_saved_visible);
            if(methods[ControlUnityReset].resolved.method_info)Invoke(ControlUnityReset,nullptr);
        } catch (...) { Log("Window control cursor restore failed"); }
    }
    if (host) {
        if (control_manager_handle) host->gchandle_free(host->context,control_manager_handle);
        if (control_request_handle) host->gchandle_free(host->context,control_request_handle);
    }
    control_manager_handle=control_request_handle=0; control_request=control_cursor_manager=nullptr;
    { std::lock_guard lock(control_events_mutex); control_events.clear(); }
    control_surfaces.clear(); control_buttons.fill(false); control_has_point=false; control_queue_failed=false;
    if (was_active) Log("Window control ended; source input released");
}
template<class T> std::vector<T> ControlArray(void* array, int maximum) {
    if (!array) throw std::runtime_error("Screen mesh data unavailable");
    int32_t dimension=0; void* size_args[]{&dimension}; int count=Value<int32_t>(ControlArrayLength,array,size_args);
    if (count<=0 || count>maximum) throw std::runtime_error("Screen mesh data exceeds input mapping limits");
    std::vector<T> output; output.reserve(count);
    for (int32_t i=0;i<count;++i) { void* args[]{&i}; output.push_back(Value<T>(ControlArrayValue,array,args)); }
    return output;
}
void BeginControl() {
    if (!bound || running.source!="window" || !last_frame_serial) throw std::runtime_error("Start window projection before window control");
    if (!control_hooks_ready || !control_input_manager.load()) throw std::runtime_error("Game input control interfaces unavailable");
    if (!control_geometry.source_width) throw std::runtime_error("Capture input geometry unavailable");
    control_game_window=GetForegroundWindow(); DWORD pid=0; GetWindowThreadProcessId(control_game_window,&pid);
    if (pid!=GetCurrentProcessId() || GetWindowThreadProcessId(control_game_window,nullptr)!=GetCurrentThreadId())
        throw std::runtime_error("Game input window is not on the media frame thread");
    for (const auto& receiver:receivers) {
        void* args[]{control_mesh_class.type_object}; auto filter=Invoke(ControlComponent,receiver.renderer,args);
        if (!IsAlive(filter)) continue;
        auto mesh=Invoke(ControlMesh,filter);
        if (!IsAlive(mesh) || !Value<uint8_t>(ControlReadable,mesh)) throw std::runtime_error("Screen mesh is not readable for precise input mapping");
        ControlSurface surface{}; surface.transform=Invoke(ControlTransform,receiver.renderer);
        surface.vertices=ControlArray<Vec3>(Invoke(ControlVertices,mesh),4096);
        surface.uvs=ControlArray<Vec2>(Invoke(ControlUvs,mesh),4096);
        surface.triangles=ControlArray<int32_t>(Invoke(ControlTriangles,mesh),24576);
        if (surface.uvs.size()!=surface.vertices.size() || surface.triangles.size()%3) throw std::runtime_error("Screen mesh UV data is invalid");
        for (auto index:surface.triangles) if (index<0 || size_t(index)>=surface.vertices.size()) throw std::runtime_error("Screen mesh triangle is invalid");
        void* property[]{&texture_property}; surface.scale=Value<Vec2>(ControlTextureScale,receiver.material,property); surface.offset=Value<Vec2>(ControlTextureOffset,receiver.material,property);
        if (receiver.desk) {
            auto layout=LayoutDisplay(running.width,running.height,receiver.aspect,running.desk_fit_mode,canvas_content);
            surface.scale={surface.scale.x*layout.canvas_scale.x,surface.scale.y*layout.canvas_scale.y};
            surface.offset={surface.offset.x*layout.canvas_scale.x+layout.canvas_offset.x,surface.offset.y*layout.canvas_scale.y+layout.canvas_offset.y};
        }
        Log("Window control surface: vertices="+std::to_string(surface.vertices.size())+" triangles="+std::to_string(surface.triangles.size()/3));
        control_surfaces.push_back(std::move(surface));
    }
    if (control_surfaces.empty()) throw std::runtime_error("Screen mesh filter unavailable for input mapping");
    control_saved_lock=Value<int32_t>(ControlCursorLock,nullptr); control_saved_visible=Value<uint8_t>(ControlCursorVisible,nullptr);
    control_cursor_manager=control_input_manager.load();
    control_manager_handle=host->gchandle_new(host->context,control_cursor_manager,0);
    control_request=host->string_new(host->context,"BetterEndfieldNext.MediaScreen.Control");
    control_request_handle=host->gchandle_new(host->context,control_request,0);
    if (!control_manager_handle || !control_request_handle) throw std::runtime_error("Cannot root window control cursor request");
    SetLastError(0);
    if(reinterpret_cast<WNDPROC>(GetWindowLongPtrW(control_game_window,GWLP_WNDPROC))!=ControlWindowProc)
        control_original_proc=reinterpret_cast<WNDPROC>(SetWindowLongPtrW(control_game_window,GWLP_WNDPROC,reinterpret_cast<LONG_PTR>(&ControlWindowProc)));
    if (!control_original_proc) throw std::runtime_error("Cannot intercept game input window");
    control_active=true;
    control_draining=false;control_quiet_frames=0;
    if(methods[ControlUnityReset].resolved.method_info)Invoke(ControlUnityReset,nullptr);
    uint8_t show=1; void* cursor_args[]{control_request,&show}; Invoke(ControlCursorRequest,control_cursor_manager,cursor_args);
    SetValue<int32_t>(ControlCursorSetLock,nullptr,0); SetValue<uint8_t>(ControlCursorSetVisible,nullptr,1);
    if (!capture.Control(true)) throw std::runtime_error("Capture helper refused window control");
    Log("Window control active; Alt+- exits; Escape forwarded to source");
}
void ToggleControl() {
    if (control_active) { EndControl(); return; }
    try { BeginControl(); } catch (const std::exception& error) { EndControl(); Log(std::string("Window control unavailable: ")+error.what()); }
}
bool ControlPoint(int client_x,int client_y,int& x,int& y) {
    auto camera=Invoke(ControlCamera,nullptr); if (!IsAlive(camera)) throw std::runtime_error("Game camera unavailable for window input");
    RECT rect{}; if (!GetClientRect(control_game_window,&rect) || rect.right<=0 || rect.bottom<=0) return false;
    int width=Value<int32_t>(ControlScreenWidth,nullptr), height=Value<int32_t>(ControlScreenHeight,nullptr);
    Vec3 point{float(double(client_x)*width/rect.right),float(height-double(client_y)*height/rect.bottom),0}; void* args[]{&point}; Ray world=Value<Ray>(ControlRay,camera,args);
    float nearest=std::numeric_limits<float>::max(); Vec2 selected{}; bool hit=false;
    for (const auto& surface:control_surfaces) {
        if (!IsAlive(surface.transform)) throw std::runtime_error("Screen transform released during window control");
        Vec3 second{world.origin.x+world.direction.x,world.origin.y+world.direction.y,world.origin.z+world.direction.z};
        void* origin_args[]{&world.origin}; void* second_args[]{&second};
        Ray local{Value<Vec3>(ControlInversePoint,surface.transform,origin_args),{}};
        local.direction=Sub(Value<Vec3>(ControlInversePoint,surface.transform,second_args),local.origin);
        const auto& indices=surface.triangles;
        for (size_t i=0;i<indices.size();i+=3) {
            auto a=indices[i],b=indices[i+1],c=indices[i+2]; Vec2 uv{};
            if (HitTriangle(local,surface.vertices[a],surface.vertices[b],surface.vertices[c],surface.uvs[a],surface.uvs[b],surface.uvs[c],nearest,uv)) {
                selected={uv.x*surface.scale.x+surface.offset.x,uv.y*surface.scale.y+surface.offset.y}; hit=true;
            }
        }
    }
    return hit && SourcePoint(selected,running.width,running.height,control_geometry,x,y);
}
void ControlFrame() {
    if (!control_active) return;
    try {
        auto error=capture.InputError(); if (!error.empty()) throw std::runtime_error(error);
        if (control_queue_failed) throw std::runtime_error("Game input queue overflow");
        SetValue<int32_t>(ControlCursorSetLock,nullptr,0); SetValue<uint8_t>(ControlCursorSetVisible,nullptr,1);
        std::deque<WindowInputEvent> events;
        { std::lock_guard lock(control_events_mutex); events.swap(control_events); }
        for (auto event:events) {
            bool mouse=event.message>=WM_MOUSEFIRST && event.message<=WM_MOUSELAST;
            if (mouse) {
                int x=0,y=0; bool mapped=ControlPoint(event.x,event.y,x,y);
                bool up=event.message==WM_LBUTTONUP||event.message==WM_RBUTTONUP||event.message==WM_MBUTTONUP;
                int button=event.message==WM_LBUTTONDOWN||event.message==WM_LBUTTONUP?0:event.message==WM_RBUTTONDOWN||event.message==WM_RBUTTONUP?1:event.message==WM_MBUTTONDOWN||event.message==WM_MBUTTONUP?2:-1;
                if (!mapped && !(up && control_has_point && control_buttons[button])) continue;
                if (mapped) { control_last_x=x; control_last_y=y; control_has_point=true; }
                event.x=control_last_x; event.y=control_last_y;
                if (button>=0) control_buttons[button]=!up;
            }
            if (!capture.Input(event)) throw std::runtime_error("Cannot deliver window input to capture helper");
        }
    } catch (const std::exception& error) { EndControl(); Log(std::string("Window control stopped: ")+error.what()); }
}
using ControlCheckFn=void(*)(void*,void*);
ControlCheckFn control_original_check=nullptr;
void ControlCheckCall(void* self,void* method) { control_input_manager=self; if (!ControlBlocks()) control_original_check(self,method); }
template<MethodId Id,class Result,class... Args> struct ControlRead {
    static inline Result(*original)(void*,Args...,void*)=nullptr;
    static Result Call(void* self,Args... args,void* method) { return ControlBlocks()?Result{}:original(self,args...,method); }
    static bool Install() { return Hook(Id,reinterpret_cast<void*>(&Call),reinterpret_cast<void**>(&original)); }
};
template<MethodId Id,class Result,class... Args> struct ControlStaticRead {
    static inline Result(*original)(Args...,void*)=nullptr;
    static Result Call(Args... args,void* method){return ControlBlocks()?Result{}:original(args...,method);}
    static bool Install(){return Hook(Id,reinterpret_cast<void*>(&Call),reinterpret_cast<void**>(&original));}
};
#define BE_CONTROL_READS(X) \
    X(ControlAxisString,float,uintptr_t) X(ControlAxisInt,float,uintptr_t) X(ControlRawString,float,uintptr_t) X(ControlRawInt,float,uintptr_t) \
    X(ControlButtonString,uint8_t,uintptr_t) X(ControlButtonInt,uint8_t,uintptr_t) X(ControlDownString,uint8_t,uintptr_t) X(ControlDownInt,uint8_t,uintptr_t) \
    X(ControlUpString,uint8_t,uintptr_t) X(ControlUpInt,uint8_t,uintptr_t) \
    X(ControlAxis2String,Vec2,uintptr_t,uintptr_t) X(ControlAxis2Int,Vec2,uintptr_t,uintptr_t) X(ControlRaw2String,Vec2,uintptr_t,uintptr_t) X(ControlRaw2Int,Vec2,uintptr_t,uintptr_t)
#define BE_CONTROL_DEVICE_READS(X) \
    X(ControlKeyboardKey,uint8_t,uintptr_t) X(ControlKeyboardDown,uint8_t,uintptr_t) X(ControlKeyboardUp,uint8_t,uintptr_t) \
    X(ControlMouseButton,uint8_t,uintptr_t) X(ControlMouseDown,uint8_t,uintptr_t) X(ControlMouseUp,uint8_t,uintptr_t)
#define BE_CONTROL_UNITY_READS(X) \
    X(ControlUnityKey,uint8_t,uintptr_t) X(ControlUnityDown,uint8_t,uintptr_t) X(ControlUnityUp,uint8_t,uintptr_t) \
    X(ControlUnityMouse,uint8_t,uintptr_t) X(ControlUnityMouseDown,uint8_t,uintptr_t) X(ControlUnityMouseUp,uint8_t,uintptr_t) \
    X(ControlUnityAxis,float,uintptr_t) X(ControlUnityRawAxis,float,uintptr_t) \
    X(ControlUnityButton,uint8_t,uintptr_t) X(ControlUnityButtonDown,uint8_t,uintptr_t) X(ControlUnityButtonUp,uint8_t,uintptr_t)
void InstallControlHooks() {
    bool ready=Hook(ControlCheckInput,reinterpret_cast<void*>(&ControlCheckCall),reinterpret_cast<void**>(&control_original_check));
#define INSTALL(Id,Result,...) ready=ControlRead<Id,Result,__VA_ARGS__>::Install() && ready;
    BE_CONTROL_READS(INSTALL)
    BE_CONTROL_DEVICE_READS(INSTALL)
#undef INSTALL
    bool unity_ready=true;
#define INSTALL(Id,Result,...) if(methods[Id].resolved.method_pointer)unity_ready=ControlStaticRead<Id,Result,__VA_ARGS__>::Install()&&unity_ready;
    BE_CONTROL_UNITY_READS(INSTALL)
#undef INSTALL
    if(methods[ControlUnityAny].resolved.method_pointer)unity_ready=ControlStaticRead<ControlUnityAny,uint8_t>::Install()&&unity_ready;
    if(methods[ControlUnityAnyDown].resolved.method_pointer)unity_ready=ControlStaticRead<ControlUnityAnyDown,uint8_t>::Install()&&unity_ready;
    ready=InstallControlInputShield()&&unity_ready&&ready;
    control_hooks_ready=ready;
    Log(ready?"Window input suppression hooks ready":"Window input suppression hooks incomplete; projection remains available");
}
#undef BE_CONTROL_READS
#undef BE_CONTROL_DEVICE_READS
#undef BE_CONTROL_UNITY_READS
void ControlContracts() {
    auto core=[&](MethodId id,const char* cls,const char* name,const char* args,const char* result,int count) { Resolve(id,kCore,"UnityEngine",cls,name,args,result,count,false); };
    Class(kCore,"UnityEngine","MeshFilter",control_mesh_class,false);
    core(ControlComponent,"Component","GetComponent","System.Type","UnityEngine.Component",1);
    core(ControlTransform,"Component","get_transform",nullptr,"UnityEngine.Transform",0);
    core(ControlMesh,"MeshFilter","get_sharedMesh",nullptr,"UnityEngine.Mesh",0);
    core(ControlReadable,"Mesh","get_isReadable",nullptr,"System.Boolean",0);
    core(ControlVertices,"Mesh","get_vertices",nullptr,"UnityEngine.Vector3[]",0);
    core(ControlUvs,"Mesh","get_uv",nullptr,"UnityEngine.Vector2[]",0);
    core(ControlTriangles,"Mesh","get_triangles",nullptr,"System.Int32[]",0);
    Resolve(ControlArrayLength,"mscorlib.dll","System","Array","GetLength","System.Int32","System.Int32",1,false);
    Resolve(ControlArrayValue,"mscorlib.dll","System","Array","GetValue","System.Int32","System.Object",1,false);
    core(ControlCamera,"Camera","get_main",nullptr,"UnityEngine.Camera",0);
    core(ControlRay,"Camera","ScreenPointToRay","UnityEngine.Vector3","UnityEngine.Ray",1);
    core(ControlInversePoint,"Transform","InverseTransformPoint","UnityEngine.Vector3","UnityEngine.Vector3",1);
    core(ControlScreenWidth,"Screen","get_width",nullptr,"System.Int32",0); core(ControlScreenHeight,"Screen","get_height",nullptr,"System.Int32",0);
    core(ControlCursorVisible,"Cursor","get_visible",nullptr,"System.Boolean",0); core(ControlCursorSetVisible,"Cursor","set_visible","System.Boolean","System.Void",1);
    core(ControlCursorLock,"Cursor","get_lockState",nullptr,"UnityEngine.CursorLockMode",0); core(ControlCursorSetLock,"Cursor","set_lockState","UnityEngine.CursorLockMode","System.Void",1);
    core(ControlTextureScale,"Material","GetTextureScale","System.Int32","UnityEngine.Vector2",1); core(ControlTextureOffset,"Material","GetTextureOffset","System.Int32","UnityEngine.Vector2",1);
    Resolve(ControlCheckInput,"Input.Beyond.dll","Beyond.Input","InputManager","_CheckInput",nullptr,"System.Void",0,false);
    Resolve(ControlCursorRequest,"Input.Beyond.dll","Beyond.Input","InputManager","SetCursorShowRequest","System.String|System.Boolean","System.Void",2,false);
    auto input_read=[&](MethodId id,const char* name,const char* args,const char* result,int count) { Resolve(id,"Rewired_Core.dll","Rewired","Player",name,args,result,count,false); };
    input_read(ControlAxisString,"GetAxis","System.String","System.Single",1); input_read(ControlAxisInt,"GetAxis","System.Int32","System.Single",1);
    input_read(ControlRawString,"GetAxisRaw","System.String","System.Single",1); input_read(ControlRawInt,"GetAxisRaw","System.Int32","System.Single",1);
    input_read(ControlButtonString,"GetButton","System.String","System.Boolean",1); input_read(ControlButtonInt,"GetButton","System.Int32","System.Boolean",1);
    input_read(ControlDownString,"GetButtonDown","System.String","System.Boolean",1); input_read(ControlDownInt,"GetButtonDown","System.Int32","System.Boolean",1);
    input_read(ControlUpString,"GetButtonUp","System.String","System.Boolean",1); input_read(ControlUpInt,"GetButtonUp","System.Int32","System.Boolean",1);
    input_read(ControlAxis2String,"GetAxis2D","System.String|System.String","UnityEngine.Vector2",2); input_read(ControlAxis2Int,"GetAxis2D","System.Int32|System.Int32","UnityEngine.Vector2",2);
    input_read(ControlRaw2String,"GetAxis2DRaw","System.String|System.String","UnityEngine.Vector2",2); input_read(ControlRaw2Int,"GetAxis2DRaw","System.Int32|System.Int32","UnityEngine.Vector2",2);
    auto device=[&](MethodId id,const char* cls,const char* name,const char* arg){Resolve(id,"Rewired_Core.dll","Rewired",cls,name,arg,"System.Boolean",1,false);};
    device(ControlKeyboardKey,"Keyboard","GetKey","UnityEngine.KeyCode");device(ControlKeyboardDown,"Keyboard","GetKeyDown","UnityEngine.KeyCode");device(ControlKeyboardUp,"Keyboard","GetKeyUp","UnityEngine.KeyCode");
    device(ControlMouseButton,"Controller","GetButton","System.Int32");device(ControlMouseDown,"Controller","GetButtonDown","System.Int32");device(ControlMouseUp,"Controller","GetButtonUp","System.Int32");
    auto unity=[&](MethodId id,const char* name,const char* arg,const char* result,int count){Resolve(id,"UnityEngine.InputLegacyModule.dll","UnityEngine","Input",name,arg,result,count,false);};
    unity(ControlUnityKey,"GetKeyInt","UnityEngine.KeyCode","System.Boolean",1);unity(ControlUnityDown,"GetKeyDownInt","UnityEngine.KeyCode","System.Boolean",1);unity(ControlUnityUp,"GetKeyUpInt","UnityEngine.KeyCode","System.Boolean",1);
    unity(ControlUnityMouse,"GetMouseButton","System.Int32","System.Boolean",1);unity(ControlUnityMouseDown,"GetMouseButtonDown","System.Int32","System.Boolean",1);unity(ControlUnityMouseUp,"GetMouseButtonUp","System.Int32","System.Boolean",1);
    unity(ControlUnityAxis,"GetAxis","System.String","System.Single",1);unity(ControlUnityRawAxis,"GetAxisRaw","System.String","System.Single",1);
    unity(ControlUnityButton,"GetButton","System.String","System.Boolean",1);unity(ControlUnityButtonDown,"GetButtonDown","System.String","System.Boolean",1);unity(ControlUnityButtonUp,"GetButtonUp","System.String","System.Boolean",1);
    unity(ControlUnityAny,"get_anyKey",nullptr,"System.Boolean",0);unity(ControlUnityAnyDown,"get_anyKeyDown",nullptr,"System.Boolean",0);unity(ControlUnityReset,"ResetInputAxes",nullptr,"System.Void",0);
}
