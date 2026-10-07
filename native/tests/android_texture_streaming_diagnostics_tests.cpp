// Compile the production read-only diagnostics without Unity or an Android device.
#include <algorithm>
#include <array>
#include <atomic>
#include <cmath>
#include <cstdint>
#include <iostream>
#include <stdexcept>
#include <string>
#include <string_view>
#include <unordered_map>
#include <vector>

namespace {
bool inspect=false;
uint64_t fixture_now=1000;
unsigned reads=0,enumerations=0,array_reads=0,request_reads=0,checks=0;
uint32_t current_thread=7;
bool getters_available=true;
bool pending_available=true,loading_available=true,enumeration_fails=false,request_fails=false;
uint64_t pending_loads=0,loading_textures=0;
std::string fixture_request_token;
int fixture_inventory_length=-1;
uint8_t fixture_active=1;
float fixture_budget=128;
uint64_t fixture_nonstreaming=144ull*1024*1024;
std::vector<std::string> logs;
struct Texture {int32_t id;bool streams;int32_t loaded=4,desired=0,requested=-1;std::string name="scene_rocks";};
std::vector<Texture> fixture_textures;
void Check(bool ok,const char* why) {++checks;if (!ok) throw std::runtime_error(why);}
bool Logged(std::string_view part) {
    return std::any_of(logs.begin(),logs.end(),[&](const auto& line){return line.find(part)!=std::string::npos;});
}
unsigned LogCount(std::string_view part) {
    return static_cast<unsigned>(std::count_if(logs.begin(),logs.end(),[&](const auto& line){return line.find(part)!=std::string::npos;}));
}
}
namespace betterendfield {
bool AndroidInspectionEnabled() {return inspect;}
std::string AndroidTextureMipRequestToken() {
    ++request_reads;
    if (request_fails) throw std::runtime_error("request read unavailable");
    return fixture_request_token;
}
}
namespace BetterEndfield::CustomModel {
struct {void* type_object=reinterpret_cast<void*>(1);} g_texture2d_class;
std::unordered_map<int32_t,std::string> g_generated_texture_identity;
std::atomic<uint32_t> g_pump_thread{7};
uint64_t GetTickCount64() {return fixture_now;}
uint32_t GetCurrentThreadId() {return current_thread;}
const char* Contract(const char* key) {return key;}
void Log(const std::string& line) {logs.push_back(line);}
template<class T> bool InvokeValue(const char* raw_key,void* object,void**,T& value) {
    ++reads;const std::string_view key(raw_key);
    if (!getters_available) return false;
    if (key=="android.streaming_active") value=static_cast<T>(fixture_active);
    else if (key=="android.streaming_budget") value=static_cast<T>(fixture_budget);
    else if (key=="android.quality_max_lod") value=static_cast<T>(1);
    else if (key=="android.quality_lod_offset") value=static_cast<T>(0);
    else if (key=="android.global_mip_limit") value=static_cast<T>(0);
    else if (key=="android.master_texture_limit") value=static_cast<T>(0);
    else if (key=="android.streaming_max_reduction") value=static_cast<T>(2);
    else if (key=="android.lod_streaming_active") value=static_cast<T>(1);
    else if (key=="android.lod_streaming_status") value=static_cast<T>(0);
    else if (key=="android.texture_nonstreaming") value=static_cast<T>(fixture_nonstreaming);
    else if (key=="android.texture_current") value=static_cast<T>(160ull*1024*1024);
    else if (key=="android.texture_desired") value=static_cast<T>(256ull*1024*1024);
    else if (key=="android.texture_target") value=static_cast<T>(152ull*1024*1024);
    else if (key=="android.texture_pending") {if (!pending_available) return false;value=static_cast<T>(pending_loads);}
    else if (key=="android.texture_loading") {if (!loading_available) return false;value=static_cast<T>(loading_textures);}
    else if (key=="android.texture_streaming_count" || key=="android.texture_nonstreaming_count") value=static_cast<T>(10);
    else {
        Check(object!=nullptr,"instance-only diagnostic used a null object");
        const auto& texture=*static_cast<Texture*>(object);
        if (key=="object.instance_id") value=static_cast<T>(texture.id);
        else if (key=="android.texture_is_streaming") value=static_cast<T>(texture.streams);
        else if (key=="android.texture_loaded_mip") value=static_cast<T>(texture.loaded);
        else if (key=="android.texture_desired_mip") value=static_cast<T>(texture.desired);
        else if (key=="android.texture_requested_mip") value=static_cast<T>(texture.requested);
        else if (key=="texture.get_width" || key=="texture.get_height") value=static_cast<T>(4096);
        else throw std::runtime_error("diagnostics attempted an unexpected contract");
    }
    return true;
}
void* Invoke(const char* key,void* object,void** args,bool) {
    Check(std::string_view(key)=="resources.find_all" && !object && args[0]==g_texture2d_class.type_object,
        "diagnostics attempted a setter or non-texture enumeration");
    ++enumerations;return enumeration_fails?nullptr:&fixture_textures;
}
int ArrayLength(void*) {return fixture_inventory_length==-1?static_cast<int>(fixture_textures.size()):fixture_inventory_length;}
void* ArrayValue(void*,int index) {++array_reads;return &fixture_textures.at(index);}
std::string ObjectName(void* object) {return static_cast<Texture*>(object)->name;}
#include "../modules/custom_model/android_lod_streaming_observer.inc"
#include "../modules/custom_model/android_texture_streaming_diagnostics.inc"
}
int main() {
    try {
        using namespace BetterEndfield::CustomModel;
        current_thread=6;
        ObserveAndroidTextureStreamingBeforeUpload();ObserveAndroidTextureStreamingAfterUpload(true,2,1234,567,890);
        Check(!reads && !enumerations && logs.empty(),"unconfirmed resource thread ran Unity diagnostics");
        current_thread=7;ObserveAndroidTextureStreamingPeriodic();
        Check(Logged("deferred-upload-observation (before-first-upload unavailable)"),
            "deferred first-upload baseline was mislabeled as an actual before snapshot");
        g_android_texture_diagnostics={};logs.clear();reads=0;
        ObserveAndroidTextureStreamingBeforeUpload();
        Check(Logged("before-first-model-upload") && Logged("budgetMiB=128") &&
            Logged("pressure=nonstreaming-at-or-above-budget"),"baseline did not identify non-streaming budget pressure");
        Check(Logged("qualityMaximumLOD=1") && Logged("qualityLODOffset=0") && Logged("globalMipLimit=0") && Logged("masterTextureLimit=0") && Logged("maxMipReduction=2") &&
            Logged("HGLODStreamingActive=1") && Logged("HGLODStreamingStatusBits=0"),
            "quality and HG streaming diagnostics lost the distinction between LOD, global mip limit and streaming state");
        const auto initial_reads=reads;
        ObserveAndroidTextureStreamingBeforeUpload();ObserveAndroidTextureStreamingPeriodic();
        Check(reads==initial_reads,"baseline repeated or periodic diagnostics ignored the 15 second limit");
        Check(!enumerations,"normal diagnostics enumerated the scene");
        for (unsigned i=0;i<20;++i) ObserveAndroidTextureStreamingAfterUpload(true,2,1234,567,890);
        Check(LogCount("event=after-model-upload")==8 && Logged("transactionSubmittedBytes=1234") &&
            Logged("transactionLiveReuseBytes=890"),"upload diagnostic limit or BEM payload accounting was lost");
        const auto limited_reads=reads;
        for (unsigned i=0;i<20;++i) {
            current_thread=6;ObserveAndroidTextureStreamingAfterUpload(true,2,1234,567,890);
            current_thread=7;ObserveAndroidTextureStreamingPeriodic();
        }
        Check(reads==limited_reads && LogCount("event=after-model-upload")==8,
            "repeated deferred uploads bypassed the shared 15 second diagnostic limit");
        Check(fixture_active==1 && fixture_budget==128 && fixture_nonstreaming==144ull*1024*1024,
            "read-only diagnostics changed streaming settings or accounting");
        fixture_now+=15000;fixture_active=0;logs.clear();ObserveAndroidTextureStreamingPeriodic();
        Check(Logged("active=0") && !Logged("pressure="),"disabled Unity streaming was labeled budget-starved");
        fixture_now+=15000;fixture_active=1;fixture_budget=0;logs.clear();ObserveAndroidTextureStreamingPeriodic();
        Check(!Logged("pressure="),"invalid memory budget was labeled budget-starved");
        fixture_now+=15000;fixture_budget=128;getters_available=false;logs.clear();ObserveAndroidTextureStreamingPeriodic();
        fixture_now+=15000;ObserveAndroidTextureStreamingPeriodic();
        Check(LogCount("partially unavailable")==1 && !Logged("event=periodic"),
            "missing contracts were repeated or reported as successful zero values");
        getters_available=true;g_android_texture_diagnostics={};inspect=true;logs.clear();
        fixture_textures.reserve(1124);
        for (int32_t i=0;i<1124;++i) fixture_textures.push_back({i,true});
        fixture_textures[0].streams=false;g_generated_texture_identity[0]="known-bem-texture";
        ObserveAndroidTextureStreamingPeriodic();
        Check(enumerations==1 && LogCount("mip sample name=")==6 &&
            Logged("scanned=1024 total=1124") && Logged("BEMInSample=1") &&
            Logged("behindDesiredInSample=1023"),"opt-in texture enumeration or sample/log bounds changed");
        fixture_now+=15000;ObserveAndroidTextureStreamingPeriodic();
        Check(enumerations==1,"opt-in scene enumeration ignored its one minute limit");
        fixture_now+=60000;ObserveAndroidTextureStreamingPeriodic();
        Check(enumerations==2,"opt-in scene enumeration did not become available again");
        Check(fixture_textures[1].loaded==4 && fixture_textures[1].requested==-1,
            "mip diagnostics changed game texture requests");
        const auto reset_request_test=[&] {
            g_android_texture_diagnostics={};g_texture2d_class.type_object=reinterpret_cast<void*>(1);
            logs.clear();fixture_textures.clear();g_generated_texture_identity.clear();
            reads=0;enumerations=0;array_reads=0;request_reads=0;inspect=false;fixture_now=200000;
            current_thread=7;getters_available=true;pending_available=true;loading_available=true;
            enumeration_fails=false;request_fails=false;pending_loads=0;loading_textures=0;fixture_request_token.clear();
            fixture_inventory_length=-1;
        };
        const auto tick=[&](uint64_t elapsed=15000) {fixture_now+=elapsed;ObserveAndroidTextureStreamingPeriodic();};
        reset_request_test();fixture_request_token="off-pump";fixture_textures.push_back({1,true});current_thread=6;
        ObserveAndroidTextureStreamingPeriodic();ObserveAndroidTextureStreamingBeforeUpload();
        ObserveAndroidTextureStreamingAfterUpload(true,1,1,0,0);SampleAndroidTextureMips(fixture_now,fixture_request_token);
        Check(!reads && !enumerations && !request_reads && g_android_texture_diagnostics.last_mip_request_token.empty(),
            "off-pump request was queried or consumed");

        reset_request_test();fixture_textures.push_back({1,true});fixture_request_token="periodic-only";
        ObserveAndroidTextureStreamingBeforeUpload();
        for (int i=0;i<5;++i) {fixture_now+=15000;ObserveAndroidTextureStreamingAfterUpload(true,1,1,0,0);}
        Check(!enumerations && !request_reads && g_android_texture_diagnostics.last_mip_request_token.empty(),
            "forced upload observations consumed a mip request");

        reset_request_test();fixture_textures.push_back({1,true});ObserveAndroidTextureStreamingPeriodic();tick();
        Check(!enumerations && request_reads==1,"no-file normal mode enumerated inventory");
        const std::vector<std::string> invalid_tokens{"","bad token","bad/token","bad.token",std::string(65,'a'),std::string(1,static_cast<char>(0x80))};
        for (const auto& invalid:invalid_tokens) {fixture_request_token=invalid;tick();}
        Check(!enumerations && g_android_texture_diagnostics.last_mip_request_token.empty(),
            "invalid request token was accepted or consumed");
        Check(AndroidTextureMipTokenValid(std::string(64,'a')) && AndroidTextureMipTokenValid("ABC_xyz-019") &&
            !AndroidTextureMipTokenValid(std::string("abc\0tail",8)),"ASCII token length or embedded-NUL bounds changed");

        reset_request_test();fixture_textures.push_back({1,true});fixture_request_token="wait-loading";pending_loads=1;
        ObserveAndroidTextureStreamingPeriodic();tick();
        Check(!enumerations && !request_reads,"pending loads did not delay request");
        pending_loads=0;tick();
        Check(!enumerations,"one zero-value snapshot was treated as stable");
        loading_textures=1;tick();loading_textures=0;tick();
        Check(!enumerations,"loading did not reset the stable interval");
        tick(14999);Check(!enumerations,"request bypassed the 15-second periodic limit");
        tick(1);Check(enumerations==1 && g_android_texture_diagnostics.last_mip_request_token==fixture_request_token,
            "request did not run after two stable periodic snapshots");
        tick(60000);tick(60000);
        Check(enumerations==1,"unchanged request file caused repeated enumeration");

        reset_request_test();fixture_textures.push_back({1,true});fixture_request_token="missing-getters";pending_available=false;
        ObserveAndroidTextureStreamingPeriodic();tick();tick();
        Check(!enumerations && !request_reads,"missing pending getter was treated as idle");
        pending_available=true;loading_available=false;tick();tick();
        Check(!enumerations && !request_reads,"missing loading getter was treated as idle");
        loading_available=true;tick();tick();
        Check(enumerations==1,"getter recovery failed to permit stable request");

        reset_request_test();fixture_textures.push_back({1,true});fixture_request_token="missing-class";g_texture2d_class.type_object=nullptr;
        ObserveAndroidTextureStreamingPeriodic();tick();tick();
        Check(!enumerations && !request_reads && g_android_texture_diagnostics.last_mip_request_token.empty(),
            "missing Texture2D class consumed a request");
        g_texture2d_class.type_object=reinterpret_cast<void*>(1);tick();
        Check(enumerations==1,"request could not recover when Texture2D class became available");

        reset_request_test();fixture_request_token="bounded-A";
        fixture_textures.reserve(9000);
        for (int32_t i=0;i<9000;++i) fixture_textures.push_back({i,true,3,3,-1,"world_misc"});
        fixture_textures[0].name="BEM_rock_excluded";g_generated_texture_identity[0]="generated";
        fixture_textures[1].loaded=4;fixture_textures[1].desired=1;
        fixture_textures[3].loaded=1;fixture_textures[3].desired=4;
        fixture_textures[1000].name="scene_ROCK_wall";
        fixture_textures[4000].name="scene_stone_ground_terrain";
        fixture_textures[7999].name="BEM_map01_excluded";g_generated_texture_identity[7999]="generated";
        fixture_textures[8000].name="map01_scene";
        fixture_textures[8191].name="scene_mod_object_mod_";
        fixture_textures[8192].name="outside_scan_limit";
        ObserveAndroidTextureStreamingPeriodic();tick();
        Check(enumerations==1 && array_reads==8192 && LogCount("mip sample name=")==12 &&
            Logged("scanned=8192 total=9000") && Logged("behindDesiredInSample=1") &&
            Logged("equalDesiredInSample=8190") && Logged("aheadDesiredInSample=1") && Logged("BEMInSample=2"),
            "independent request lost inventory, logging bounds or non-behind summary counts");
        Check(Logged("name=scene_ROCK_wall") && Logged("name=scene_stone_ground_terrain") &&
            Logged("name=map01_scene") && Logged("name=scene_mod_object_mod_") &&
            !Logged("name=BEM_rock_excluded") && !Logged("name=BEM_map01_excluded") && !Logged("outside_scan_limit"),
            "late scene candidates did not take priority or known BEM textures were included");
        Check(Logged("loaded=3 desired=3 requested=-1") && Logged("requestToken=bounded-A"),
            "stable mip samples omitted loaded, desired or requested levels");
        Check(!inspect && fixture_textures[1000].loaded==3 && fixture_textures[1000].desired==3 && fixture_textures[1000].requested==-1 &&
            fixture_active==1 && fixture_budget==128,"request sampling changed BEM mode or game texture/streaming settings");
        fixture_request_token="bounded-B";tick();tick();tick();
        Check(enumerations==1 && g_android_texture_diagnostics.last_mip_request_token=="bounded-A",
            "new token bypassed the global 60-second inventory limit or was consumed while delayed");
        tick();Check(enumerations==2 && array_reads==16384 && g_android_texture_diagnostics.last_mip_request_token=="bounded-B",
            "next token did not run at the 60-second boundary");
        tick(60000);Check(enumerations==2,"second token was enumerated repeatedly");

        reset_request_test();fixture_textures.push_back({1,true});fixture_request_token="failed-adapter";request_fails=true;
        ObserveAndroidTextureStreamingPeriodic();tick();
        Check(!enumerations && g_android_texture_diagnostics.last_mip_request_token.empty(),
            "optional adapter failure escaped or consumed the token");
        request_fails=false;tick();Check(enumerations==1,"adapter failure did not recover");

        reset_request_test();fixture_textures.push_back({1,true});fixture_request_token="retry-enumeration";enumeration_fails=true;
        ObserveAndroidTextureStreamingPeriodic();tick();
        Check(enumerations==1 && !array_reads && g_android_texture_diagnostics.last_mip_request_token.empty(),
            "failed enumeration consumed a request token");
        enumeration_fails=false;tick();tick();tick();
        Check(enumerations==1,"failed enumeration retry ignored the 60-second backoff");
        tick();Check(enumerations==2 && array_reads==1 && g_android_texture_diagnostics.last_mip_request_token==fixture_request_token,
            "failed enumeration did not permit a successful retry after 60 seconds");

        reset_request_test();fixture_textures.push_back({1,true});fixture_request_token="invalid-inventory";
        fixture_inventory_length=100001;ObserveAndroidTextureStreamingPeriodic();tick();
        Check(enumerations==1 && !array_reads && g_android_texture_diagnostics.last_mip_request_token.empty(),
            "invalid inventory length consumed a token or accessed texture entries");
        fixture_inventory_length=-2;tick(60000);
        Check(enumerations==2 && !array_reads && g_android_texture_diagnostics.last_mip_request_token.empty(),
            "negative inventory length consumed a token or accessed texture entries");
        fixture_inventory_length=-1;tick(60000);
        Check(enumerations==3 && array_reads==1 && g_android_texture_diagnostics.last_mip_request_token==fixture_request_token,
            "valid inventory did not recover after invalid length retries");

        std::cout<<"PASS Android production texture diagnostics: "<<checks<<" Release-active checks; confirmed pump, read-only settings, inspection fallback, independent stable requests, 8192/12 bounds, 60-second limit\n";
        return 0;
    } catch (const std::exception& error) {std::cerr<<error.what()<<'\n';return 1;}
}
