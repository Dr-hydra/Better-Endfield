// Include the production translation unit so readiness is tested rather than
// duplicating its algorithm. No Unity functions or game pointers are executed.
#include "../../modules/camera/module.cpp"
#include <cassert>
#include <iostream>
using namespace BetterEndfieldNext::CameraModule;
namespace betterendfieldnext { const BE_LocalMusicApiV1* AndroidLocalMusicApi() { return nullptr; } }
static int mode = 0;
static float clockScale = 0.75f;
static int getScale, setScale;
static void* InvokeClock(void*, const void* method, void*, void** args, void** exception) {
    *exception = nullptr;
    if (method == &getScale) return &clockScale;
    if (method == &setScale) { clockScale = *static_cast<float*>(args[0]); return nullptr; }
    return nullptr;
}
static BE_Result Create(void*,const char*,void* target,void*,void** original) {
    bool allow = (mode==1 && target==reinterpret_cast<void*>(4)) ||
        (mode==2 && target==reinterpret_cast<void*>(2));
    if (!allow) return BE_Result_Failed;
    *original = target; return BE_Result_Ok;
}
static void Reset() {
    g_free_camera_contract_ready = true;
    g_dither_contract_ready = g_time_heartbeat_contract_ready = true;
    g_state_layout.ready = true;
    g_push_state_hook_ready = false;
    g_pause_contract_ready = false;
    const char* keys[]{"camera_manager.tail_late_tick","cinemachine.push_state",
        "unity.time.unscaled_delta.get","camera.process_dither"};
    for (int i=0;i<4;++i) { auto* c=Contract(keys[i]); c->resolved=true; c->pointer=reinterpret_cast<void*>(static_cast<uintptr_t>(i+1)); }
}
int main() {
    BE_HostApiV1 host{};host.create_hook=Create;g_host=&host;
    betterendfieldnext::DispatchAndroidFrame();
    Reset(); mode=0;assert(!InstallHook());
    assert(!g_free_camera_contract_ready&&!g_dither_contract_ready);
    Reset(); mode=1;assert(InstallHook());
    assert(g_dither_contract_ready&&!g_free_camera_contract_ready);
    Reset(); mode=2;assert(InstallHook());
    assert(g_free_camera_contract_ready&&!g_dither_contract_ready);
    // A pause-only profile still has the real nativeRender pump when optional
    // camera hooks fail. The test executes production time ownership logic.
    Reset(); mode=0;g_pause_contract_ready=true;assert(InstallHook());
    host.runtime_invoke=InvokeClock;
    host.object_unbox=[](void*,void* value)->void*{return value;};
    Contract("unity.time.scale.get")->method_info=&getScale;
    Contract("unity.time.scale.set")->method_info=&setScale;
    g_free_camera_enabled=false;g_pause_enabled=true;g_pause_request=true;
    PumpFreeCameraControl();assert(clockScale==0&&g_changed_time_scale);
    g_free_camera_active=true;
    ExitFreeCamera("test camera mode change");assert(clockScale==0&&g_changed_time_scale);
    g_pause_request=true;PumpFreeCameraControl();assert(clockScale==0.75f&&!g_changed_time_scale);
    g_pause_request=true;PumpFreeCameraControl();assert(clockScale==0&&g_changed_time_scale);
    g_pause_enabled=false;PumpFreeCameraControl();assert(clockScale==0.75f&&!g_changed_time_scale);
    g_host=nullptr;
    std::cout<<"PASS camera: hook capabilities, pause-only frame pump, independent freeze, camera mode preservation and exact time restoration\n";
}
