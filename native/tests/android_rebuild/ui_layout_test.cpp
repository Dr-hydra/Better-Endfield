// Exercise the actual layout pump without touching Unity or game memory.
#include "../../modules/ui/module.cpp"
#include <cassert>
#include <iostream>
using namespace BetterEndfield::UiModule;
static int input = 1, calls = 0;
static int32_t boxed;
static int change;
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
    g_desired_generation=1;PumpInputType();assert(input==1&&calls==0);
    g_pc_ui_enabled=true;g_desired_generation++;PumpInputType();assert(input==0&&calls==1);
    g_desired_generation++;PumpInputType();assert(input==0&&calls==1);
    g_pc_ui_enabled=false;g_desired_generation++;PumpInputType();assert(input==1&&calls==2);
    input=2;g_desired_generation++;PumpInputType();assert(input==2&&calls==2);
    g_pc_ui_enabled=true;g_desired_generation++;PumpInputType();assert(input==0&&calls==3);
    g_pc_ui_enabled=false;g_desired_generation++;PumpInputType();assert(input==2&&calls==4);
    g_host=nullptr;
    std::cout<<"PASS Android layout: default preserves touch/controller, explicit PC switch and restoration\n";
}
