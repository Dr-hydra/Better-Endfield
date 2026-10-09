// Exercise the actual layout pump without touching Unity or game memory.
#include "../../modules/ui/module.cpp"
#include <cassert>
#include <iostream>
using namespace BetterEndfieldNext::UiModule;
static int input = 1, calls = 0;
static int32_t boxed;
static int change;
static void PumpNow() {
    g_next_input_type_check_tick=0;
    PumpInputType();
}
static void* InvokeInput(void*,const void* method,void*,void** args,void** exception) {
    *exception=nullptr;
    if(method==&change){++calls;input=*static_cast<int32_t*>(args[0]);}
    return nullptr;
}
int main() {
    BE_HostApiV1 host{};
    host.runtime_invoke=InvokeInput;
    host.object_unbox=[](void*,void* value)->void*{return value;};
    host.field_get_value_object=[](void*,const void*,void*)->void*{boxed=input;return &boxed;};
    g_host=&host;g_input_type_field=&boxed;g_change_input_type_method=&change;
    g_keyboard_input_type=0;
    g_desired_generation=1;PumpNow();assert(input==1&&calls==0);
    g_pc_ui_enabled=true;g_desired_generation++;PumpNow();assert(input==0&&calls==1);
    g_desired_generation++;PumpNow();assert(input==0&&calls==1);
    g_pc_ui_enabled=false;g_desired_generation++;PumpNow();assert(input==1&&calls==2);
    input=2;g_desired_generation++;PumpNow();assert(input==2&&calls==2);
    g_pc_ui_enabled=true;g_desired_generation++;PumpNow();assert(input==0&&calls==3);
    g_pc_ui_enabled=false;g_desired_generation++;PumpNow();assert(input==2&&calls==4);
    // A late DeviceInfo.Init/direct field write can change the mode without a
    // settings generation. The enabled override must notice the real state.
    input=0;g_restore_input_type=-1;
    g_pc_ui_enabled=true;g_desired_generation++;PumpNow();assert(input==0&&calls==4);
    assert(g_restore_input_type==-1); // no override happened yet
    input=1;
    PumpInputType();assert(input==1&&calls==4); // rate-limited on the same frame
    PumpNow();assert(input==0&&calls==5);
    assert(g_restore_input_type==1); // capture the game choice, not its early default
    g_pc_ui_enabled=false;g_desired_generation++;PumpNow();assert(input==1&&calls==6);
    input=2;PumpNow();assert(input==2&&calls==6); // disabled state never forces keyboard
    // The shared PC -> touch direction also reconciles a direct field reset.
    input=0;g_mobile_ui_enabled=true;g_desired_generation++;PumpNow();assert(input==1&&calls==7);
    input=0;PumpNow();assert(input==1&&calls==8);
    g_mobile_ui_enabled=false;g_desired_generation++;PumpNow();assert(input==0&&calls==9);
    g_host=nullptr;
    std::cout<<"PASS Android layout: defaults, explicit PC/touch switches, same-generation drift, rate limit and restoration\n";
}
