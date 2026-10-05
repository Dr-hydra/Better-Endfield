#include "../overlay/model_library.h"
#include "../runtime_ini_win32.h"
#include "../model_overlay_hotkey.h"
#include "../model_overlay_protocol.h"
#include "../mod_registry.h"
#include <fstream>
#include <iostream>
#include <thread>
#include <atomic>

using namespace BetterEndfield::CustomModel;
namespace Management=ModelManagement;
using Json=Management::Json;
namespace {
size_t checks=0;
void Check(bool value,const std::string& why) {++checks;if(!value) throw std::runtime_error(why);}
struct Temporary {
    std::filesystem::path root=std::filesystem::temp_directory_path()/(L"model-overlay-test-"+std::to_wstring(GetCurrentProcessId())+L"-"+std::to_wstring(GetTickCount64()));
    Temporary() {std::filesystem::create_directories(root/"packages");}
    ~Temporary() {std::error_code error;std::filesystem::remove_all(root,error);}
};
#pragma pack(push,1)
struct Header {char magic[8];uint16_t major,minor;uint32_t size;uint64_t file,manifest;uint32_t count,flags;};
#pragma pack(pop)
void Package(const std::filesystem::path& path,std::string id,std::string role,bool broken_directory=false) {
    const auto eq=[](const char* group,const char* value){return Json{{"eq",Json::array({group,value})}};};
    Json m={{"schema",1},{"package_id",id},{"name",id},{"author","Tests"},{"version","1"},
        {"required_capabilities",Json::array({"composable-options","body-parameters"})},
        {"target",{{"platform","windows-x64"},{"character_id",role},{"profile_id","test"},{"revision","r1"},
            {"world_resource",role+"_world"},{"ui_resource",role+"_ui"},
            {"components",Json::array({{{"id",0},{"mesh_name","Body"},{"original_index_count",3},{"bone_names",Json::array()},{"materials",Json::array()}}})}}},
        {"option_groups",Json::array({{{"id","style"},{"name","Style"},{"default","on"},
            {"choices",Json::array({{{"id","on"},{"name","On"}},{{"id","off"},{"name","Off"}}})}},
            {{"id","accessory"},{"name","Accessory"},{"default","hide"},{"available_when",eq("style","on")},
            {"choices",Json::array({{{"id","hide"},{"name","Hide"}},{{"id","show"},{"name","Show"}}})}}})},
        {"selection_constraints",Json::array({{{"not",{{"all",Json::array({eq("style","on"),eq("accessory","show")})}}}}})},
        {"component_rules",Json::array({{{"target",0},{"candidates",Json::array({{{"operation","keep"}}})}}})},
        {"meshes",Json::array()},{"textures",Json::array()},
        {"parameters",Json::array({{{"id","width"},{"name","Width"},{"min",100},{"max",900},{"neutral",100},{"default",500},{"step",100},{"available_when",eq("style","on")}},
            {{"id","height"},{"name","Height"},{"min",0},{"max",1000},{"neutral",0},{"default",300},{"step",10}}})}};
    const auto json=m.dump();Header h{};std::memcpy(h.magic,"BEM\0PKG\0",8);h.major=1;h.minor=3;h.size=40;h.manifest=json.size();
    h.count=broken_directory?1:0;h.file=40+json.size()+(broken_directory?48:0);
    std::ofstream file(path,std::ios::binary);file.write(reinterpret_cast<const char*>(&h),40);file<<json;
    if(broken_directory) {const std::string garbage(48,'X');file<<garbage;}
    Check(bool(file),"Fixture write failed");
}
}
int main() {try {
    Temporary temp;const auto one=temp.root/"packages"/"one.bem",two=temp.root/"packages"/"two.bem",other=temp.root/"packages"/"other.bem";
    Package(one,"pkg.one","chr_one");Package(two,"pkg.two","chr_one");Package(other,"pkg.other","chr_other");
    const auto corrupt=temp.root/"directory.bem";Package(corrupt,"pkg.directory","chr_dir",true);
    BemPackageInfo info;std::string error;
    Check(ReadBemManagementInfo(corrupt,info,error),error);
    Check(!ReadBemPackageInfo(corrupt,info,error),"Management accidentally depended on payload directory validation");
    Check(ReadBemManagementInfo(one,info,error),error);
    BemSelection selection;
    Check(ResolveBemSelection(info,"style:off","width:700",selection,error),error);
    Check(selection.options=="style:off&accessory:hide","Missing option defaults not retained");
    Check(selection.parameters=="width:100&height:300"&&selection.parameters_saved=="width:700&height:300","Dormant/author-default parameter values lost");
    Check(selection.available_groups==std::vector<std::string>{"style"}&&selection.available_parameters==std::vector<std::string>{"height"},"available_when not evaluated");
    Check(!ResolveBemSelection(info,"style:on&accessory:show",{},selection,error),"Unreachable combination accepted");
    Check(!ResolveBemSelection(info,"style:on","width:750",selection,error),"Slider step ignored");
    Check(!ResolveBemSelection(info,"style:on","width:1000",selection,error),"Slider bounds ignored");
    Check(!ResolveBemSelection(info,"style:on&unknown:value",{},selection,error),"Unknown selection silently accepted");
    Check(ParseModelOverlayHotkey("PLUS")==VK_OEM_PLUS,"Default hotkey is not bare main keyboard =");
    for(const auto* alias:{"=","+","OemPlus","OEM_PLUS"}) Check(ParseModelOverlayHotkey(alias)==VK_OEM_PLUS,"OEM alias failed");
    Check(ParseModelOverlayHotkey("Shift+OemPlus")== (VK_OEM_PLUS|BetterEndfield::Input::kShift),"Shift alias failed");
    Check(ParseModelOverlayHotkey("CTRL++")== (VK_OEM_PLUS|BetterEndfield::Input::kCtrl),"Literal plus chord failed");
    Check(ParseModelOverlayHotkey("ADD")==VK_ADD&&VK_ADD!=ParseModelOverlayHotkey("PLUS"),"Numpad ADD confused with PLUS");
    bool bad_hotkey=false;try {ParseModelOverlayHotkey("nonsense");} catch(...) {bad_hotkey=true;}Check(bad_hotkey,"Invalid hotkey silently ignored");
    const auto runtime=temp.root/"runtime.ini";
    const std::string initial="; keep this comment\n[CustomModel]\nhot_switch=true\nfast_loading=true\nskip_validation=false\nunknown=retain-me\n[Foreign]\nopaque=x=y\n[Mod.pkg.one]\npackage=packages/one.bem\nenabled=true\noptions=style:on\nparameters_saved=width:700\nunknown_mod=keep\n[Mod.pkg.two]\npackage=packages/two.bem\nenabled=false\n[Mod.pkg.other]\npackage=packages/other.bem\nenabled=true\n";
    {Settings::MutexLock lock;Settings::ReplaceLocked(runtime,initial);}
    Management::Library library;library.root=temp.root;
    library.Refresh(Settings::Ini(initial));const auto reads=library.metadata_reads;
    library.Refresh(Settings::Ini(initial));Check(library.metadata_reads==reads,"Unchanged metadata reread");
    auto apply=[&](Management::Action action) {return Settings::Update(runtime,[&](Settings::Ini& ini){library.Apply(ini,action);},error);};
    Check(apply({Management::ActionKind::Enable,"Mod.pkg.two",{}, {}}),error);
    Settings::Ini saved(Settings::ReadLocked(runtime));
    Check(saved.Flag("Mod.pkg.two","enabled")&&!saved.Flag("Mod.pkg.one","enabled")&&saved.Flag("Mod.pkg.other","enabled"),"Enable did not enforce per-character exclusivity");
    Check(saved.Get("CustomModel","unknown")=="retain-me"&&saved.Flag("CustomModel","hot_switch")&&saved.Flag("CustomModel","fast_loading")&&saved.Get("Foreign","opaque")=="x=y"&&saved.Get("Mod.pkg.one","unknown_mod")=="keep","Unknown or experimental fields lost");
    Check(Settings::ReadLocked(runtime).find("; keep this comment")!=std::string::npos,"INI comments lost");
    Check(saved.Get("Mod.pkg.two","options")=="style:on&accessory:hide"&&saved.Get("Mod.pkg.two","parameters_saved")=="width:500&height:300","Enable failed to materialize complete defaults");
    Check(apply({Management::ActionKind::Option,"Mod.pkg.one","style","off"}),error);
    saved=Settings::Ini(Settings::ReadLocked(runtime));Check(saved.Get("Mod.pkg.one","parameters")=="width:100&height:300"&&saved.Get("Mod.pkg.one","parameters_saved")=="width:700&height:300","Options discarded remembered slider");
    Check(apply({Management::ActionKind::Option,"Mod.pkg.one","style","on"}),error);
    saved=Settings::Ini(Settings::ReadLocked(runtime));Check(saved.Get("Mod.pkg.one","parameters")=="width:700&height:300","Reenabled slider did not restore remembered value");
    const auto baseline=Settings::ReadLocked(runtime);
    Check(!apply({Management::ActionKind::Parameter,"Mod.pkg.one","width","750"}),"Invalid step committed");
    Check(Settings::ReadLocked(runtime)==baseline,"Failed selection damaged previous file");
    Check(!apply({Management::ActionKind::Option,"Mod.pkg.one","accessory","show"}),"Constraint violation committed");
    Check(Settings::ReadLocked(runtime)==baseline,"Failed constraint damaged previous file");
    bool race=false;int attempts=0;
    Check(Settings::Update(runtime,[&](Settings::Ini& ini) {
        ++attempts;
        if(!race) {race=true;std::string ui_error;Check(Settings::Update(runtime,[](Settings::Ini& ui){ui.Set("Foreign","concurrent_ui","preserved");ui.Set("Mod.pkg.one","parameters_saved","width:700&height:400");},ui_error),ui_error);}
        library.Apply(ini,{Management::ActionKind::Parameter,"Mod.pkg.one","width","800"});
    },error),error);
    saved=Settings::Ini(Settings::ReadLocked(runtime));
    Check(attempts==2&&saved.Get("Foreign","concurrent_ui")=="preserved"&&saved.Get("Mod.pkg.one","parameters_saved")=="width:800&height:400","Concurrent UI save was clobbered or control delta did not rebase");
    Check(library.metadata_reads==reads,"Slider save read package metadata again");
    std::atomic_bool parallel_ok=true;
    std::thread a([&]{for(int i=0;i<10;++i) {std::string e;if(!Settings::Update(runtime,[&](Settings::Ini& ini){ini.Set("Foreign","writer_a",std::to_string(i));},e)) parallel_ok=false;}});
    std::thread b([&]{for(int i=0;i<10;++i) {std::string e;if(!Settings::Update(runtime,[&](Settings::Ini& ini){ini.Set("Foreign","writer_b",std::to_string(i));},e)) parallel_ok=false;}});
    a.join();b.join();saved=Settings::Ini(Settings::ReadLocked(runtime));
    Check(parallel_ok&&saved.Get("Foreign","writer_a")=="9"&&saved.Get("Foreign","writer_b")=="9","Concurrent atomic saves lost edits");
    Check(Settings::Update(runtime,[](Settings::Ini& ini){ini.Set("Mod.missing","enabled","true");ini.Set("Mod.missing","opaque","keep");},error),error);
    Check(apply({Management::ActionKind::DisableAll,{},{},{}}),error);saved=Settings::Ini(Settings::ReadLocked(runtime));
    for(const auto& [section,_]:saved.All()) if(section.starts_with("Mod.")) Check(!saved.Flag(section,"enabled"),"Close all missed an unknown package");
    Check(saved.Get("Mod.missing","opaque")=="keep","Close all lost unknown package fields");
    Check(Settings::Update(runtime,[](Settings::Ini& ini){ini.Set("Mod.missing","enabled","broken");},error),error);
    Check(apply({Management::ActionKind::DisableAll,{},{},{}}),error);
    Check(!Settings::Ini(Settings::ReadLocked(runtime)).Flag("Mod.missing","enabled"),"Close all tried to validate unreadable/malformed package states first");
    bool duplicate=false;try {Settings::Ini invalid("[CustomModel]\nx=1\nx=2\n");}catch(...) {duplicate=true;}Check(duplicate,"Duplicate INI keys silently accepted");
    std::cout<<"PASS model overlay: "<<checks<<" checks (selection, availability, constraints, step, mutual exclusion, defaults, unknown fields, conflict rebase, atomic writers, metadata cache, OEM hotkeys)\n";
    return 0;
} catch(const std::exception& e) {std::cerr<<e.what()<<'\n';return 1;}}
