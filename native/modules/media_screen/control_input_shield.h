// Included in the Endfield module namespace. The Host composes these native
// hooks with other modules (including the camera's Raw Input observer).
LRESULT CALLBACK ControlWindowProc(HWND,UINT,WPARAM,LPARAM);
using ControlAsyncFn=SHORT(WINAPI*)(int);
using ControlKeyboardFn=BOOL(WINAPI*)(PBYTE);
using ControlRawFn=UINT(WINAPI*)(HRAWINPUT,UINT,LPVOID,PUINT,UINT);
using ControlRawBufferFn=UINT(WINAPI*)(PRAWINPUT,PUINT,UINT);
ControlAsyncFn control_real_async=nullptr,control_real_key_state=nullptr;
ControlKeyboardFn control_real_keyboard=nullptr;
ControlRawFn control_real_raw=nullptr;
ControlRawBufferFn control_real_raw_buffer=nullptr;
SHORT WINAPI ControlAsync(int key){auto value=control_real_async(key);return ControlBlocks()&&!real_control_key_reads?0:value;}
SHORT WINAPI ControlKeyState(int key){auto value=control_real_key_state(key);return ControlBlocks()&&!real_control_key_reads?SHORT(value&1):value;}
BOOL WINAPI ControlKeyboard(PBYTE state){auto result=control_real_keyboard(state);if(result&&state&&ControlBlocks()&&!real_control_key_reads)for(int key=0;key<256;++key)state[key]&=1;return result;}
void NeutralizeControlRaw(RAWINPUT& input,size_t bytes){
    if(!ControlBlocks()||bytes<sizeof(RAWINPUTHEADER)||input.header.dwSize>bytes||input.header.dwSize<offsetof(RAWINPUT,data))return;
    auto payload=input.header.dwSize- offsetof(RAWINPUT,data);
    if(input.header.dwType==RIM_TYPEKEYBOARD&&payload>=sizeof(RAWKEYBOARD)){
        // Releases clear pre-existing device caches. All game-facing readers
        // remain neutral while active and throughout the release barrier.
        if(!(input.data.keyboard.Flags&RI_KEY_BREAK)){input.data.keyboard.VKey=255;input.data.keyboard.MakeCode=0;input.data.keyboard.Message=0;}
    }else if(input.header.dwType==RIM_TYPEMOUSE&&payload>=sizeof(RAWMOUSE)){
        input.data.mouse.lLastX=input.data.mouse.lLastY=0;input.data.mouse.usButtonData=0;
        input.data.mouse.usButtonFlags&=RI_MOUSE_LEFT_BUTTON_UP|RI_MOUSE_RIGHT_BUTTON_UP|RI_MOUSE_MIDDLE_BUTTON_UP|RI_MOUSE_BUTTON_4_UP|RI_MOUSE_BUTTON_5_UP;
    }
}
UINT WINAPI ControlRawData(HRAWINPUT handle,UINT command,LPVOID data,PUINT size,UINT header){
    auto result=control_real_raw(handle,command,data,size,header);if(command==RID_INPUT&&data&&result!=UINT(-1)&&header==sizeof(RAWINPUTHEADER))NeutralizeControlRaw(*static_cast<RAWINPUT*>(data),result);return result;
}
UINT WINAPI ControlRawBuffer(PRAWINPUT data,PUINT size,UINT header){
    UINT capacity=size?*size:0;auto result=control_real_raw_buffer(data,size,header);
    if(data&&result!=UINT(-1)&&header==sizeof(RAWINPUTHEADER)){
        auto begin=reinterpret_cast<uintptr_t>(data),end=begin+capacity;
        for(UINT i=0;i<result;++i){auto point=reinterpret_cast<uintptr_t>(data);if(point>end||end-point<sizeof(RAWINPUTHEADER)||data->header.dwSize<sizeof(RAWINPUTHEADER)||data->header.dwSize>end-point)break;
            auto bytes=data->header.dwSize;NeutralizeControlRaw(*data,bytes);auto next=(point+bytes+sizeof(void*)-1)&~uintptr_t(sizeof(void*)-1);if(next>end)break;data=reinterpret_cast<PRAWINPUT>(next);}
    }
    return result;
}
bool InstallControlInputShield(){
    HMODULE user=GetModuleHandleW(L"user32.dll");
    auto install=[&](const char* name,void* detour,void** original){auto target=user?reinterpret_cast<void*>(GetProcAddress(user,name)):nullptr;return target&&host->create_hook(host->context,kId,target,detour,original)==BE_Result_Ok;};
    bool ready=install("GetAsyncKeyState",reinterpret_cast<void*>(&ControlAsync),reinterpret_cast<void**>(&control_real_async));
    ready=install("GetKeyState",reinterpret_cast<void*>(&ControlKeyState),reinterpret_cast<void**>(&control_real_key_state))&&ready;
    ready=install("GetKeyboardState",reinterpret_cast<void*>(&ControlKeyboard),reinterpret_cast<void**>(&control_real_keyboard))&&ready;
    ready=install("GetRawInputData",reinterpret_cast<void*>(&ControlRawData),reinterpret_cast<void**>(&control_real_raw))&&ready;
    ready=install("GetRawInputBuffer",reinterpret_cast<void*>(&ControlRawBuffer),reinterpret_cast<void**>(&control_real_raw_buffer))&&ready;
    Log(ready?"Window control native input shield ready":"Window control native input shield incomplete");return ready;
}
