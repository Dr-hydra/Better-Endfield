// Include the production translation unit so readiness is tested rather than
// duplicating its algorithm. No Unity functions or game pointers are executed.
#include "../../modules/camera/module.cpp"
#include <cassert>
#include <iostream>
using namespace BetterEndfield::CameraModule;
static int mode = 0;
static BE_Result Create(void*,const char*,void* target,void*,void** original) {
    bool allow = (mode==1 && target==reinterpret_cast<void*>(4)) ||
        (mode==2 && target==reinterpret_cast<void*>(2));
    if (!allow) return BE_Result_Failed;
    *original = target; return BE_Result_Ok;
}
static void Reset() {
    g_free_camera_contract_ready = g_first_person_contract_ready = true;
    g_dither_contract_ready = g_time_heartbeat_contract_ready = true;
    g_state_layout.ready = true;
    g_push_state_hook_ready = false;
    const char* keys[]{"camera_manager.tail_late_tick","cinemachine.push_state",
        "unity.time.unscaled_delta.get","camera.process_dither"};
    for (int i=0;i<4;++i) { auto* c=Contract(keys[i]); c->resolved=true; c->pointer=reinterpret_cast<void*>(static_cast<uintptr_t>(i+1)); }
}
int main() {
    BE_HostApiV1 host{};host.create_hook=Create;g_host=&host;
    betterendfield::DispatchAndroidFrame();
    Reset(); mode=0;assert(!InstallHook());
    assert(!g_free_camera_contract_ready&&!g_first_person_contract_ready&&!g_dither_contract_ready);
    Reset(); mode=1;assert(InstallHook());
    assert(g_dither_contract_ready&&!g_free_camera_contract_ready&&!g_first_person_contract_ready);
    Reset(); mode=2;assert(InstallHook());
    assert(g_free_camera_contract_ready&&g_first_person_contract_ready&&!g_dither_contract_ready);
    g_host=nullptr;
    std::cout<<"PASS camera: all hooks failed is failure; independent optional dither; actual frame entry supports core\n";
}
