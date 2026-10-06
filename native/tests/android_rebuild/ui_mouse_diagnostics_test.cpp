// Exercise the read-only production diagnostics with boxed engine results.
#include "../../modules/ui/module.cpp"
#include <cassert>
#include <iostream>
#include <limits>
using namespace BetterEndfield::UiModule;
static int lock_method, visible_method, position_method, width_method, height_method, axis_method;
static int32_t lock_value = 0, screen_width = 1920, screen_height = 1080;
static bool visible_value = true, reject_axis = false;
static AndroidPcMousePosition mouse_position{960, 540, 0};
static float axis_value = 0.5f;
static int invocations = 0, axis_invocations = 0;
static std::vector<std::string> logs;
static void* InvokeEngine(void*, const void* method, void*, void**, void** exception) {
    ++invocations;
    *exception = nullptr;
    if (method == &lock_method) return &lock_value;
    if (method == &visible_method) return &visible_value;
    if (method == &position_method) return &mouse_position;
    if (method == &width_method) return &screen_width;
    if (method == &height_method) return &screen_height;
    if (method == &axis_method) {
        ++axis_invocations;
        if (reject_axis) { *exception = &axis_method; return nullptr; }
        return &axis_value;
    }
    return nullptr;
}
static void SampleNow() {
    g_android_pc_mouse_diagnostics.next_sample_tick = 0;
    PumpAndroidPcMouseDiagnostics();
}
int main() {
    BE_HostApiV1 host{};
    host.log = [](void*, const char*, const char* value) { logs.emplace_back(value); };
    host.runtime_invoke = InvokeEngine;
    host.object_unbox = [](void*, void* value) -> void* { return value; };
    host.string_new = [](void*, const char* value) -> void* { return const_cast<char*>(value); };
    g_host = &host;
    auto& state = g_android_pc_mouse_diagnostics;
    state.lock_state = &lock_method;
    state.visible = &visible_method;
    state.position = &position_method;
    state.screen_width = &width_method;
    state.screen_height = &height_method;
    state.axis_raw = &axis_method;
    betterendfield::SetAndroidForeground(true);
    g_pc_ui_enabled = false;
    SampleNow();
    assert(invocations == 0 && logs.empty());
    g_pc_ui_enabled = true;
    g_diagnostics_enabled = false;
    SampleNow();
    assert(invocations == 0);
    g_diagnostics_enabled = true;
    SampleNow();
    assert(logs.size() == 1 && logs.back().find("lock=0, visible=1") != std::string::npos);
    const int first_calls = invocations;
    PumpAndroidPcMouseDiagnostics();
    assert(invocations == first_calls); // at most four engine samples per second
    for (int i = 0; i < 20; ++i) SampleNow();
    assert(logs.size() == 1); // stable state does not log each sample
    mouse_position = {1919, 540, 0};
    SampleNow();
    assert(state.edge_samples == 1 && state.samples == 21);
    state.next_report_tick = 0;
    SampleNow();
    assert(logs.size() == 2 && logs.back().find("edge_samples=2/22") != std::string::npos);
    assert(logs.back().find("not total movement") != std::string::npos);
    lock_value = 1;
    visible_value = false;
    SampleNow();
    assert(logs.size() == 3 && logs.back().find("lock=1, visible=0") != std::string::npos);
    reject_axis = true;
    SampleNow();
    const int failures = axis_invocations;
    assert(state.axis_failed);
    for (int i = 0; i < 5; ++i) SampleNow();
    assert(axis_invocations == failures); // an absent engine mapping cannot throw repeatedly
    mouse_position.x = std::numeric_limits<float>::quiet_NaN();
    state.next_report_tick = 0;
    SampleNow();
    assert(logs.back().find("position=(nan,540.0) [unavailable]") != std::string::npos);
    const int before_suspend = invocations;
    betterendfield::SetAndroidForeground(false);
    SampleNow();
    assert(invocations == before_suspend);
    betterendfield::SetAndroidForeground(true);
    // Missing optional metadata leaves the module's input mode untouched.
    state.lock_state = state.visible = state.position = nullptr;
    state.axis_raw = state.screen_width = state.screen_height = nullptr;
    g_android_pc_mouse_diagnostics_reset = true;
    SampleNow();
    assert(logs.back().find("lock=-1, visible=-1") != std::string::npos);
    assert(g_pc_ui_enabled && g_restore_input_type == -1);
    g_host = nullptr;
    std::cout << "PASS Android PC mouse diagnostics: gating, sample/log limits, edges, cursor transitions, exceptions and missing contracts\n";
}
