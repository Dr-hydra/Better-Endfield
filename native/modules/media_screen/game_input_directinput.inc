// Included in the source bridge namespace. Only keyboard and mouse devices
// using the standard DirectInput data formats are virtualized; other devices
// continue through their original methods.
using DirectStateFn=HRESULT(STDMETHODCALLTYPE*)(void*,DWORD,LPVOID);
using DirectDataFn=HRESULT(STDMETHODCALLTYPE*)(void*,DWORD,LPDIDEVICEOBJECTDATA,LPDWORD,DWORD);
using DirectAcquireFn=HRESULT(STDMETHODCALLTYPE*)(void*);
struct DirectHook {void* target;void* original;};
std::vector<DirectHook> direct_state_hooks,direct_data_hooks,direct_acquire_hooks;
std::array<BYTE,256> direct_keys{};
LONG direct_dx=0,direct_dy=0,direct_wheel=0;
struct DirectRecord {uint64_t sequence;bool keyboard;DWORD offset,value,time;};
std::deque<DirectRecord> direct_records;
std::unordered_map<void*,uint64_t> direct_cursors;
uint64_t direct_sequence=0;

void* DirectOriginal(void* self,size_t slot,const std::vector<DirectHook>& hooks){auto target=(*reinterpret_cast<void***>(self))[slot];for(auto& item:hooks)if(item.target==target)return item.original;return nullptr;}
int DirectKind(void* self){
    DIDEVCAPS caps{};caps.dwSize=sizeof(caps);auto result=static_cast<IDirectInputDevice8W*>(self)->GetCapabilities(&caps);
    if(FAILED(result))return 0;auto type=GET_DIDEVICE_TYPE(caps.dwDevType);
    return type==DI8DEVTYPE_KEYBOARD?1:type==DI8DEVTYPE_MOUSE?2:0;
}
void ResetDirectInput(){direct_keys.fill(0);direct_dx=direct_dy=direct_wheel=0;direct_records.clear();direct_cursors.clear();direct_sequence=0;}
void DirectRecordEvent(bool keyboard,DWORD offset,DWORD value){direct_records.push_back({++direct_sequence,keyboard,offset,value,GetTickCount()});if(direct_records.size()>1024)direct_records.pop_front();}
void DirectKey(const Event& event,bool down){
    UINT scan=UINT((event.data>>16)&255);if(!scan)scan=MapVirtualKeyW(event.key,MAPVK_VK_TO_VSC_EX)&255;
    if(event.data&(int64_t(1)<<24))scan|=0x80;
    if(scan<direct_keys.size()){direct_keys[scan]=down?0x80:0;DirectRecordEvent(true,scan,down?0x80:0);}
}
void DirectMouse(const RAWMOUSE& mouse){
    direct_dx+=mouse.lLastX;direct_dy+=mouse.lLastY;
    if(mouse.lLastX)DirectRecordEvent(false,DIMOFS_X,DWORD(mouse.lLastX));if(mouse.lLastY)DirectRecordEvent(false,DIMOFS_Y,DWORD(mouse.lLastY));
    if(mouse.usButtonFlags&RI_MOUSE_WHEEL){LONG wheel=SHORT(mouse.usButtonData);direct_wheel+=wheel;DirectRecordEvent(false,DIMOFS_Z,DWORD(wheel));}
    constexpr USHORT down[]={RI_MOUSE_LEFT_BUTTON_DOWN,RI_MOUSE_RIGHT_BUTTON_DOWN,RI_MOUSE_MIDDLE_BUTTON_DOWN};
    constexpr USHORT up[]={RI_MOUSE_LEFT_BUTTON_UP,RI_MOUSE_RIGHT_BUTTON_UP,RI_MOUSE_MIDDLE_BUTTON_UP};
    for(DWORD i=0;i<3;++i){if(mouse.usButtonFlags&down[i])DirectRecordEvent(false,DIMOFS_BUTTON0+i,0x80);if(mouse.usButtonFlags&up[i])DirectRecordEvent(false,DIMOFS_BUTTON0+i,0);}
}
HRESULT STDMETHODCALLTYPE DirectState(void* self,DWORD size,LPVOID data){
    auto original=reinterpret_cast<DirectStateFn>(DirectOriginal(self,9,direct_state_hooks));
    if(!Live())return original?original(self,size,data):DIERR_NOTINITIALIZED;
    int kind=DirectKind(self);if(!data)return DIERR_INVALIDPARAM;
    if(kind==1&&size==direct_keys.size()){std::lock_guard lock(state_mutex);std::memcpy(data,direct_keys.data(),size);Count(&Shared::key_reads);return DI_OK;}
    if(kind==2&&(size==sizeof(DIMOUSESTATE)||size==sizeof(DIMOUSESTATE2))){
        std::lock_guard lock(state_mutex);DIMOUSESTATE2 mouse{};mouse.lX=direct_dx;mouse.lY=direct_dy;mouse.lZ=direct_wheel;
        for(int i=0;i<3;++i)mouse.rgbButtons[i]=buttons[i]?0x80:0;std::memcpy(data,&mouse,size);direct_dx=direct_dy=direct_wheel=0;Count(&Shared::cursor_reads);return DI_OK;
    }
    return original?original(self,size,data):DIERR_NOTINITIALIZED;
}
HRESULT STDMETHODCALLTYPE DirectData(void* self,DWORD object_size,LPDIDEVICEOBJECTDATA data,LPDWORD count,DWORD flags){
    auto original=reinterpret_cast<DirectDataFn>(DirectOriginal(self,10,direct_data_hooks));
    if(!Live())return original?original(self,object_size,data,count,flags):DIERR_NOTINITIALIZED;
    int kind=DirectKind(self);if(!kind)return original?original(self,object_size,data,count,flags):DIERR_NOTINITIALIZED;
    if(!count||object_size<sizeof(DIDEVICEOBJECTDATA_DX3)||object_size>sizeof(DIDEVICEOBJECTDATA)||(flags&~DIGDD_PEEK))return DIERR_INVALIDPARAM;
    std::lock_guard lock(state_mutex);auto& read=direct_cursors[self];DWORD capacity=*count,written=0;uint64_t next=read;
    bool overflow=!direct_records.empty()&&read+1<direct_records.front().sequence;
    for(const auto& record:direct_records){
        if(record.sequence<=read)continue;
        if(record.keyboard!=(kind==1)){next=record.sequence;continue;}
        if(written>=capacity)break;
        if(data){DIDEVICEOBJECTDATA item{};item.dwOfs=record.offset;item.dwData=record.value;item.dwTimeStamp=record.time;item.dwSequence=DWORD(record.sequence);
            std::memcpy(reinterpret_cast<BYTE*>(data)+size_t(written)*object_size,&item,object_size);}
        ++written;next=record.sequence;
    }
    if(!(flags&DIGDD_PEEK))read=next;*count=written;return overflow?DI_BUFFEROVERFLOW:DI_OK;
}
HRESULT STDMETHODCALLTYPE DirectAcquire(void* self){
    if(Live()&&DirectKind(self))return DI_OK;
    auto original=reinterpret_cast<DirectAcquireFn>(DirectOriginal(self,7,direct_acquire_hooks));return original?original(self):DIERR_NOTINITIALIZED;
}
template<class T>bool DirectHookMethod(void* target,T callback,std::vector<DirectHook>& hooks){
    for(auto& item:hooks)if(item.target==target)return true;
    T original=nullptr;if(!Hook(reinterpret_cast<T>(target),callback,original))return false;
    hooks.push_back({target,reinterpret_cast<void*>(original)});return true;
}
bool InstallDirectInput(){
    HMODULE library=LoadLibraryExW(L"dinput8.dll",nullptr,LOAD_LIBRARY_SEARCH_SYSTEM32);
    auto create=reinterpret_cast<decltype(&DirectInput8Create)>(library?GetProcAddress(library,"DirectInput8Create"):nullptr);if(!create)return false;
    size_t first=created_hooks.size();bool ok=true;
    for(const GUID* interface_id:{&IID_IDirectInput8W,&IID_IDirectInput8A}){
        IDirectInput8W* input=nullptr;
        if(FAILED(create(GetModuleHandleW(nullptr),DIRECTINPUT_VERSION,*interface_id,reinterpret_cast<void**>(&input),nullptr))){ok=false;break;}
        for(const GUID* guid:{&GUID_SysKeyboard,&GUID_SysMouse}){
            IDirectInputDevice8W* device=nullptr;
            if(FAILED(input->CreateDevice(*guid,&device,nullptr))){ok=false;break;}
            auto table=*reinterpret_cast<void***>(device);
            ok=DirectHookMethod(table[9],&DirectState,direct_state_hooks)&&DirectHookMethod(table[10],&DirectData,direct_data_hooks)&&DirectHookMethod(table[7],&DirectAcquire,direct_acquire_hooks);
            device->Release();if(!ok)break;
        }
        input->Release();if(!ok)break;
    }
    if(!ok){for(size_t i=first;i<created_hooks.size();++i)MH_RemoveHook(created_hooks[i]);created_hooks.resize(first);direct_state_hooks.clear();direct_data_hooks.clear();direct_acquire_hooks.clear();}
    // The DLL remains loaded because original device vtables can refer to it.
    return ok;
}
