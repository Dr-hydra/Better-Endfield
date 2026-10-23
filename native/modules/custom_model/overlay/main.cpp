#include "win32_overlay_window.h"
#include "model_library.h"
#include "../runtime_ini_win32.h"
#include "../model_overlay_protocol.h"
#include "../model_overlay_hotkey.h"
#include <shellapi.h>
#include <optional>

namespace BetterEndfieldNext::CustomModel::ModelOverlay {
namespace {
namespace UI=Win32Overlay;
namespace Management=ModelManagement;
namespace Protocol=OverlayProtocol;
using namespace Gdiplus;
using Management::Action;
using Management::ActionKind;
UI::Window window;
Management::Library library;
std::filesystem::path ini_path;
HANDLE game_process=nullptr,mapping=nullptr,single_instance=nullptr;
Protocol::Shared* shared=nullptr;
Settings::Ini current;
std::string current_text,filter,message;
std::set<std::string> expanded,expanded_groups;
bool filter_open=false,key_down=false,save_failed=false;
int binding=VK_OEM_PLUS;
float scroll=0,content_height=0;
ULONGLONG last_scan=0,last_save=0;
std::optional<Action> pending;
uint64_t saved_hash=0;
std::filesystem::file_time_type observed_mtime{};
uintmax_t observed_size=0;
bool observed_ini=false;
struct Control {
    RectF rect;
    bool enabled=true;
    std::function<void()> click;
    std::optional<Action> slider;
    // >= 0 marks a section 13 speed slider: it edits the shared driver block
    // instead of runtime.ini, so releasing it must not queue an ini save.
    int driver=-1;
    uint32_t min=0,max=1000,step=1;
};
std::vector<Control> controls;
std::optional<Control> pressed,drag_slider;
constexpr float kContentTop=98;
const Color accent(255,67,201,255),text(255,241,244,249),disabled(150,140,148,162);

bool IsEnglish() {
    static ULONGLONG last=0;static bool english=false;
    const auto now=GetTickCount64();if(last&&now-last<3000) return english;last=now;
    const auto profile=library.root.parent_path().parent_path()/L"BetterEndfieldNext.ini";
    wchar_t language[64]{};
    GetPrivateProfileStringW(L"Launcher",L"Language",L"",language,64,profile.c_str());
    english=language[0]?(_wcsicmp(language,L"en")==0||_wcsicmp(language,L"en-US")==0||
        _wcsicmp(language,L"en_US")==0||_wcsicmp(language,L"English")==0):
        PRIMARYLANGID(GetUserDefaultUILanguage())!=LANG_CHINESE;
    return english;
}
const wchar_t* T(const wchar_t* zh,const wchar_t* en) {return IsEnglish()?en:zh;}
std::wstring CharacterName(const std::string& id) {
    static std::map<std::string,std::string> chinese,english;
    static bool loaded=false;
    if(!loaded) {
        loaded=true;wchar_t executable[32768]{};GetModuleFileNameW(nullptr,executable,32768);
        const auto directory=std::filesystem::path(executable).parent_path();
        for(const auto& [filename,target]:std::initializer_list<std::pair<const wchar_t*,std::map<std::string,std::string>*>>{
                {L"model-character-names.json",&chinese},{L"model-character-names-en.json",&english}}) {
            try {
                const auto names=Management::Json::parse(Settings::ReadFile(directory/filename));
                if(names.is_object()) for(const auto& [key,value]:names.items())
                    if(value.is_string()) target->emplace(key,value.get<std::string>());
            } catch(const std::exception&) { }
        }
    }
    const auto& names=IsEnglish()?english:chinese;
    const auto found=names.find(id);return UI::Wide(found==names.end()?id:found->second);
}
std::wstring TargetName(const std::string& key) {
    const auto split=key.find(':');const auto id=split==key.npos?key:key.substr(split+1);
    return key.starts_with("weapon:")?std::wstring(T(L"武器 · ",L"Weapon · "))+UI::Wide(id):CharacterName(id);
}
std::wstring StatusText() {
    if(IsEnglish()) {
        static const std::map<std::string,std::string> labels{
            {"已保存 · 等待运行时接收","Saved · pending runtime"},{"已保存 · 重启后生效","Saved · restart required"},
            {"未保存的修改已撤销","Unsaved changes discarded"},{"已保存 · 运行时已接收","Saved · accepted by runtime"},
            {"已保存 · 运行时拒绝变更","Saved · rejected by runtime"}};
        const auto found=labels.find(message);if(found!=labels.end()) return UI::Wide(found->second);
    }
    return UI::Wide(message);
}

void Refresh(bool force=true) {
    std::error_code te,se;
    const auto mtime=std::filesystem::last_write_time(ini_path,te);
    const auto size=std::filesystem::file_size(ini_path,se);
    if(force||!observed_ini||mtime!=observed_mtime||size!=observed_size) {
        const auto source=Settings::ReadLocked(ini_path);Settings::Ini latest(source);
        const int hotkey=ParseModelOverlayHotkey(latest.Get("CustomModel","overlay_hotkey","PLUS"));
        current=std::move(latest);current_text=source;binding=hotkey;
        observed_ini=true;observed_mtime=mtime;observed_size=size;
    }
    library.Refresh(current); // package cache uses mtime/size, including failed reads
}
bool SavePending() {
    if(!pending) return true;
    const auto action=*pending;std::string error;
    const bool ok=Settings::Update(ini_path,[&](Settings::Ini& ini){library.Apply(ini,action);},error);
    last_save=GetTickCount64();
    if(!ok) {save_failed=true;message=error;return false;}
    pending.reset();save_failed=false;Refresh();saved_hash=Protocol::ConfigHash(current_text);
    message=shared->runtime_hot_switch?"已保存 · 等待运行时接收":"已保存 · 重启后生效";
    return true;
}
void Save(Action action) {
    if(save_failed) return;
    pending=std::move(action);SavePending();
}
void SetVisible(bool visible) {
    std::string error;
    if(!Settings::Update(ini_path,[&](Settings::Ini& ini){ini.Set("CustomModel","overlay_visible",visible?"true":"false");},error)) {
        message=error;return;
    }
    Refresh();window.visible=visible;
}
bool Available(const std::vector<std::string>& values,const std::string& id) {
    return std::find(values.begin(),values.end(),id)!=values.end();
}
void Button(Graphics& g,const std::wstring& label,const RectF& rect,std::function<void()> click,
    bool selected=false,bool enabled=true,bool content=false) {
    const bool in_view=!content||(rect.Y>=kContentTop&&rect.GetBottom()<=window.height-48);
    GraphicsPath shape;UI::Rounded(shape,rect,6);
    SolidBrush brush(selected?Color(95,67,201,255):Color(80,55,62,76));g.FillPath(&brush,&shape);
    UI::Label(g,label,{rect.X+8,rect.Y,rect.Width-16,rect.Height},enabled?(selected?accent:text):disabled,14,selected);
    if(in_view) controls.push_back({rect,enabled,std::move(click)});
}
// ---- Section 13 driver panel ----
// Only one driver set is live at a time, so the controls attach to the package
// whose path the module published. A stale path simply hides the section.
constexpr uint32_t kSpeedMin=250,kSpeedMax=4000,kSpeedStep=50;
std::wstring FileName(const wchar_t* path) {
    const wchar_t* slash=wcsrchr(path,L'\\');
    if(!slash) slash=wcsrchr(path,L'/');
    return slash?slash+1:path;
}
int VisibleDrivers(const Management::Package& package) {
    if(!shared->driver_package[0]) return 0;
    const auto owner=UI::Wide(Management::PathUtf8(package.metadata->path));
    // Exact path first. The registry may hold the same package under a relative
    // or differently normalised path, so the file name is accepted as well: only
    // one driver set is live at a time, so it cannot be ambiguous.
    if(_wcsicmp(shared->driver_package,owner.c_str())!=0 &&
       _wcsicmp(FileName(shared->driver_package).c_str(),FileName(owner.c_str()).c_str())!=0)
        return 0;
    return static_cast<int>(std::clamp<LONG>(
        InterlockedCompareExchange(&shared->driver_count,0,0),0,
        static_cast<LONG>(Protocol::kDriverCapacity)));
}
void DriverPlaying(size_t index,bool playing) {
    if(index>=Protocol::kDriverCapacity) return;
    InterlockedExchange(&shared->drivers[index].playing,playing?1:0);
    InterlockedIncrement(&shared->driver_control_revision);
}
void DriverSpeed(size_t index,uint32_t millis) {
    if(index>=Protocol::kDriverCapacity) return;
    InterlockedExchange(&shared->drivers[index].speed_millis,static_cast<LONG>(millis));
    InterlockedIncrement(&shared->driver_control_revision);
}
void DrawDrivers(Graphics& g,const Management::Package& package,float& y) {
    const int count=VisibleDrivers(package);
    if(count<=0) return;
    UI::Label(g,T(L"动效",L"Motion"),{32,y,window.width-64,26},Color(255,170,180,196),14);y+=28;
    for(int i=0;i<count;++i) {
        auto& entry=shared->drivers[i];
        const bool playing=InterlockedCompareExchange(&entry.playing,0,0)!=0;
        const uint32_t speed=std::clamp<uint32_t>(static_cast<uint32_t>(std::max<LONG>(
            InterlockedCompareExchange(&entry.speed_millis,0,0),1000)),kSpeedMin,kSpeedMax);
        const std::wstring name=entry.name[0]?entry.name:entry.id;
        UI::Label(g,name,{32,y,window.width-204,30},text);
        Button(g,playing?T(L"暂停",L"Pause"):T(L"播放",L"Play"),{window.width-160,y,136,28},
            [i,playing]{DriverPlaying(static_cast<size_t>(i),!playing);});
        y+=32;
        const RectF rect(44,y,window.width-172,26);
        Pen track(Color(255,64,73,91),4),fill(accent,4);
        g.DrawLine(&track,rect.X,rect.Y+13,rect.GetRight(),rect.Y+13);
        const float fraction=static_cast<float>(speed-kSpeedMin)/static_cast<float>(kSpeedMax-kSpeedMin);
        g.DrawLine(&fill,rect.X,rect.Y+13,rect.X+rect.Width*fraction,rect.Y+13);
        SolidBrush knob(accent);g.FillEllipse(&knob,rect.X+rect.Width*fraction-6,rect.Y+7,12.0f,12.0f);
        wchar_t label[32]{};swprintf_s(label,L"%.2fx",speed/1000.0);
        UI::Label(g,label,{window.width-120,y-2,96,26},accent,13);
        if(rect.Y>=kContentTop&&rect.GetBottom()<=window.height-48) {
            Control control{rect,true};control.driver=i;control.min=kSpeedMin;control.max=kSpeedMax;
            control.step=kSpeedStep;controls.push_back(std::move(control));
        }
        y+=32;
    }
    y+=4;
}
void DrawSlider(Graphics& g,const Management::Package& package,const Management::Json& parameter,float& y) {
    const auto id=parameter.at("id").get<std::string>();
    const bool active=Available(package.selection.available_parameters,id)&&!save_failed;
    auto remembered=package.selection.parameters_saved;
    if(pending&&pending->section==package.section&&pending->kind==ActionKind::Parameter)
        remembered=Settings::PatchPairs(remembered,pending->id,pending->value);
    const auto value=Settings::PairValue(remembered,id);
    const auto min=parameter.at("min").get<uint32_t>(),max=parameter.at("max").get<uint32_t>(),step=parameter.at("step").get<uint32_t>();
    const uint32_t tick=value.empty()?parameter.at("default").get<uint32_t>():static_cast<uint32_t>(std::stoul(value));
    UI::Label(g,UI::Wide(parameter.at("name").get<std::string>()),{32,y,window.width-154,30},active?text:disabled);
    UI::Label(g,std::to_wstring(tick),{window.width-112,y,80,30},active?accent:disabled);y+=30;
    const RectF rect(44,y,window.width-88,28);
    Pen track(Color(255,64,73,91),4),fill(active?accent:disabled,4);
    g.DrawLine(&track,rect.X,rect.Y+14,rect.GetRight(),rect.Y+14);
    const float position=rect.X+rect.Width*static_cast<float>(tick-min)/static_cast<float>(max-min);
    g.DrawLine(&fill,rect.X,rect.Y+14,position,rect.Y+14);
    SolidBrush knob(active?accent:disabled);g.FillEllipse(&knob,position-6,rect.Y+8,12.0f,12.0f);
    if(rect.Y>=kContentTop&&rect.GetBottom()<=window.height-48) {
        Control control{rect,active};control.slider=Action{ActionKind::Parameter,package.section,id,{}};
        control.min=min;control.max=max;control.step=step;controls.push_back(std::move(control));
    }
    y+=34;
}
void DrawDetails(Graphics& g,const Management::Package& package,float& y) {
    const auto metadata=package.metadata;const auto section=package.section;
    if(!package.error.empty()) {UI::Label(g,UI::Wide(package.error),{32,y,window.width-64,32},Color(255,255,130,130));y+=36;}
    Button(g,T(L"作者默认",L"Defaults"),{32,y,110,28},[section]{Save({ActionKind::Defaults,section,{},{}});},false,!save_failed,true);y+=36;
    if(!metadata->error.empty()) return;
    const auto& info=metadata->info;
    if(!info.minor) {
        for(const auto& appearance:metadata->manifest.at("appearances")) {
            const auto id=appearance.at("id").get<std::string>();
            Button(g,UI::Wide(appearance.at("name").get<std::string>()),{32,y,window.width-64,30},
                [section,id]{Save({ActionKind::Appearance,section,{},id});},package.selection.options==id,!save_failed,true);y+=36;
        }
    } else for(const auto& group:metadata->groups) {
        const auto id=group.at("id").get<std::string>(),key=section+"/"+id;
        const bool available=Available(package.selection.available_groups,id);
        const auto selected=Settings::PairValue(package.selection.options,id);
        std::string choice_name=selected;
        for(const auto& choice:group.at("choices")) if(choice.at("id")==selected) choice_name=choice.at("name").get<std::string>();
        Button(g,(expanded_groups.contains(key)?L"− ":L"+ ")+UI::Wide(group.at("name").get<std::string>())+L"  ·  "+UI::Wide(choice_name),
            {32,y,window.width-64,30},[key]{if(!expanded_groups.erase(key)) expanded_groups.insert(key);},false,available,true);y+=36;
        if(!expanded_groups.contains(key)) continue;
        for(const auto& choice:group.at("choices")) {
            const auto choice_id=choice.at("id").get<std::string>();
            const bool valid=Management::CanChoose(package,id,choice_id);
            Button(g,UI::Wide(choice.at("name").get<std::string>()),{48,y,window.width-80,28},
                [section,id,choice_id]{Save({ActionKind::Option,section,id,choice_id});},choice_id==selected,valid&&!save_failed,true);y+=34;
        }
    }
    for(const auto& parameter:metadata->parameters) DrawSlider(g,package,parameter,y);
    DrawDrivers(g,package,y);
}
void Paint(Graphics& g) {
    controls.clear();SolidBrush bar(accent);g.FillRectangle(&bar,18.0f,14.0f,4.0f,22.0f);
    UI::Label(g,T(L"模型管理",L"Models"),{30,10,160,30},text,18,true);
    UI::Label(g,shared->runtime_hot_switch?T(L"热切换",L"Hot switch"):T(L"重启后生效",L"Restart required"),{190,10,window.width-268,30},accent,14);
    Button(g,L"×",{window.width-48,10,30,30},[]{SetVisible(false);});
    Button(g,filter.empty()?std::wstring(T(L"全部模型 ▾",L"All models ▾")):TargetName(filter)+L" ▾",{18,56,window.width-158,30},[]{filter_open=!filter_open;scroll=0;});
    Button(g,T(L"关闭全部",L"Disable all"),{window.width-130,56,112,30},[]{Save({ActionKind::DisableAll,{},{},{}});},false,!save_failed);
    const auto state=g.Save();g.SetClip(RectF(16,kContentTop,window.width-32,window.height-kContentTop-48));
    float y=kContentTop-scroll;
    if(filter_open) {
        std::set<std::string> roles;for(const auto& package:library.packages) if(!package.metadata->info.target_id.empty()) roles.insert(Management::TargetKey(package.metadata->info));
        Button(g,T(L"全部模型",L"All models"),{24,y,window.width-48,32},[]{filter={};filter_open=false;scroll=0;},filter.empty(),true,true);y+=38;
        for(const auto& role:roles) {Button(g,TargetName(role),{24,y,window.width-48,32},[role]{filter=role;filter_open=false;scroll=0;},filter==role,true,true);y+=38;}
    } else {
        for(const auto& package:library.packages) {
            if(!filter.empty()&&Management::TargetKey(package.metadata->info)!=filter) continue;
            const auto section=package.section;
            const auto title=package.metadata->info.name.empty()?Management::PathUtf8(package.metadata->path.filename()):package.metadata->info.name;
            const auto role=Management::TargetKey(package.metadata->info);
            Button(g,(expanded.contains(section)?L"− ":L"+ ")+UI::Wide(title),{24,y,window.width-142,34},
                [section]{if(!expanded.erase(section)) expanded.insert(section);},false,true,true);
            Button(g,package.enabled?T(L"已启用",L"Enabled"):T(L"启用",L"Enable"),{window.width-108,y,84,34},[section,enabled=package.enabled]{
                Save({enabled?ActionKind::Disable:ActionKind::Enable,section,{},{}});
            },package.enabled,!save_failed&&(package.enabled||package.metadata->error.empty()),true);y+=38;
            UI::Label(g,TargetName(role),{32,y,window.width-64,26},Color(255,170,180,196),14);y+=30;
            if(expanded.contains(section)) DrawDetails(g,package,y);
            y+=8;
        }
        if(library.packages.empty()) UI::Label(g,T(L"暂无模型包",L"No installed models"),{24,y,window.width-48,40});
    }
    content_height=y+scroll-kContentTop;g.Restore(state);
    scroll=std::clamp(scroll,0.0f,std::max(0.0f,content_height-(window.height-kContentTop-48)));
    UI::Label(g,StatusText(),{24,window.height-42,window.width-(save_failed?224:48),32},save_failed?Color(255,255,130,130):text,14);
    if(save_failed) {
        Button(g,T(L"重试",L"Retry"),{window.width-184,window.height-42,76,30},[]{SavePending();});
        Button(g,T(L"撤销",L"Undo"),{window.width-100,window.height-42,76,30},[]{pending.reset();save_failed=false;Refresh();message="未保存的修改已撤销";});
    }
    if(content_height>window.height-kContentTop-48) {
        const float viewport=window.height-kContentTop-48,thumb=std::max(18.0f,viewport*viewport/content_height);
        const float top=kContentTop+scroll/(content_height-viewport)*(viewport-thumb);
        SolidBrush brush(Color(200,100,120,145));g.FillRectangle(&brush,window.width-10,top,3.0f,thumb);
    }
}
uint32_t SliderTick(const Control& control,float x) {
    const double fraction=std::clamp(static_cast<double>((x-control.rect.X)/control.rect.Width),0.0,1.0);
    const auto steps=static_cast<uint32_t>(std::llround(fraction*(control.max-control.min)/control.step));
    return control.min+steps*control.step;
}
void SliderValue(const Control& control,float x) {
    if(control.driver>=0) {DriverSpeed(static_cast<size_t>(control.driver),SliderTick(control,x));return;}
    auto action=*control.slider;action.value=std::to_string(SliderTick(control,x));pending=std::move(action);
}
bool Mouse(UINT msg,float x,float y,WPARAM wp) {
    if(msg==WM_MOUSEWHEEL) {scroll+=GET_WHEEL_DELTA_WPARAM(wp)>0?-54.0f:54.0f;window.Render();return true;}
    if(msg==WM_CAPTURECHANGED) {pressed.reset();drag_slider.reset();return true;}
    if(msg==WM_MOUSEMOVE&&drag_slider) {SliderValue(*drag_slider,x);window.Render();return true;}
    if(msg==WM_LBUTTONDOWN) {
        for(const auto& control:controls) if(control.rect.Contains(x,y)) {
            if(control.enabled) {pressed=control;if(control.slider) {drag_slider=control;SliderValue(control,x);}}
            return true;
        }
    }
    if(msg==WM_LBUTTONUP) {
        const auto control=pressed;pressed.reset();
        if(drag_slider) {
            const bool ini_slider=drag_slider->driver<0;
            SliderValue(*drag_slider,x);drag_slider.reset();
            // Speed lives in the shared block, not in runtime.ini, so there is
            // nothing to queue for a save.
            if(ini_slider) SavePending();
        }
        else if(control&&control->rect.Contains(x,y)&&control->click) control->click();
        window.Render();return control.has_value();
    }
    return false;
}
void Tick() {
    if(WaitForSingleObject(game_process,0)!=WAIT_TIMEOUT||shared->shutdown_requested) {PostQuitMessage(0);return;}
    try {
        const auto now=GetTickCount64();
        if((!last_scan||now-last_scan>=1000)&&!drag_slider&&!pending) {last_scan=now;Refresh(false);}
        const bool down=Input::IsDown(binding);
        if(down&&!key_down&&window.ForegroundGame()&&current.Flag("CustomModel","overlay_enabled",true)) SetVisible(!current.Flag("CustomModel","overlay_visible"));
        key_down=down;
        if(pending&&!save_failed&&now-last_save>=150) SavePending();
        if(saved_hash&&static_cast<uint64_t>(InterlockedCompareExchange64(&shared->last_config_hash,0,0))==saved_hash) {
            message=shared->last_result==1?"已保存 · 运行时已接收":"已保存 · 运行时拒绝变更";saved_hash=0;
        }
        window.visible=current.Flag("CustomModel","overlay_enabled",true)&&current.Flag("CustomModel","overlay_visible");
    } catch(const std::exception& e) {message=e.what();}
    window.Follow();
}
int Run(HINSTANCE instance) {
    SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
    int count=0;LPWSTR* args=CommandLineToArgvW(GetCommandLineW(),&count);std::wstring name;
    if(!args) return 2;
    for(int i=1;i<count;++i) {
        const std::wstring_view argument(args[i]);
        if(argument==L"--game-pid"&&i+1<count) window.game_pid=wcstoul(args[++i],nullptr,10);
        else if(argument==L"--mapping"&&i+1<count) name=args[++i];
    }
    LocalFree(args);if(!window.game_pid||name!=Protocol::MappingName(window.game_pid)) return 2;
    single_instance=CreateMutexW(nullptr,FALSE,(name+L".Companion").c_str());
    if(!single_instance||GetLastError()==ERROR_ALREADY_EXISTS) return 0;
    game_process=OpenProcess(SYNCHRONIZE,FALSE,window.game_pid);if(!game_process) return 3;
    mapping=OpenFileMappingW(FILE_MAP_ALL_ACCESS,FALSE,name.c_str());if(!mapping) return 3;
    shared=static_cast<Protocol::Shared*>(MapViewOfFile(mapping,FILE_MAP_ALL_ACCESS,0,0,sizeof(Protocol::Shared)));
    if(!shared||shared->magic!=Protocol::kMagic||shared->version!=Protocol::kVersion||shared->structure_size!=sizeof(Protocol::Shared)||shared->game_pid!=window.game_pid) return 4;
    if(!wmemchr(shared->runtime_ini,L'\0',Protocol::kPathCapacity)||!wmemchr(shared->package_directory,L'\0',Protocol::kPathCapacity)) return 4;
    ini_path=shared->runtime_ini;library.root=ini_path.parent_path();library.installed_directory=shared->package_directory;
    window.tick=Tick;window.paint=Paint;window.mouse=Mouse;
    if(!window.Create(instance)) return 5;
    Tick();MSG msg{};while(GetMessageW(&msg,nullptr,0,0)>0) {TranslateMessage(&msg);DispatchMessageW(&msg);}
    KillTimer(window.handle,1);UnmapViewOfFile(shared);shared=nullptr;CloseHandle(mapping);CloseHandle(game_process);CloseHandle(single_instance);
    return 0;
}
} // namespace
} // namespace BetterEndfieldNext::CustomModel::ModelOverlay
int WINAPI wWinMain(HINSTANCE instance,HINSTANCE,PWSTR,int) {
    return BetterEndfieldNext::CustomModel::ModelOverlay::Run(instance);
}
