#include "../../modules/ui/module.cpp"
#include <cassert>
#include <iostream>
using namespace BetterEndfieldNext::UiModule;
static int manager, cursor, field, calculate;
static int action_field, binding;
static const char* binding_action = nullptr;
static int binding_calls = 0;
static bool binding_enabled = true;
static bool __fastcall OriginalBinding(void*, void*) { ++binding_calls; return binding_enabled; }
static void OriginalPosition(AndroidPcMousePosition* position) { *position = {12, 34, 56}; }
static int __fastcall ScreenWidth(void*) { return 1280; }
static int __fastcall ScreenHeight(void*) { return 720; }
static int original_axis_calls = 0, original_cursor_calls = 0, refreshes = 0;
static bool last_show = true;
static float __fastcall OriginalAxis(void*, void*, void*) {
    ++original_axis_calls;
    return 7.0f;
}
static void __fastcall OriginalCursor(void*, bool show, bool, void*) {
    ++original_cursor_calls;
    last_show = show;
}
static float Axis(const char* name) {
    return DetourAndroidPcGetAxis(&manager, const_cast<char*>(name), nullptr);
}
int main() {
    BE_HostApiV1 host{};
    host.copy_managed_string = [](void*, const void* text, char* output, size_t capacity) -> int {
        const auto length = std::strlen(static_cast<const char*>(text));
        std::snprintf(output, capacity, "%s", static_cast<const char*>(text));
        return static_cast<int>(length);
    };
    host.field_get_value_object = [](void*, const void* descriptor, void* instance) -> void* {
        if (descriptor == &action_field) {
            assert(instance == &binding);
            return const_cast<char*>(binding_action);
        }
        assert(descriptor == &field && instance == &manager);
        return &cursor;
    };
    host.runtime_invoke = [](void*, const void* method, void* instance, void** parameters, void** exception) -> void* {
        assert(method == &calculate && instance == &cursor && *static_cast<bool*>(parameters[0]));
        ++refreshes;
        *exception = nullptr;
        DetourAndroidPcCursorToggle(&cursor, false, true, nullptr);
        return nullptr;
    };
    g_host = &host;
    g_original_android_pc_get_axis = OriginalAxis;
    g_original_android_pc_cursor_toggle = OriginalCursor;
    g_android_pc_cursor_hook_ready = g_android_pc_axis_hook_ready = true;
    g_android_pc_real_cursor_field = &field;
    g_android_pc_cursor_calc_state_method = &calculate;
    g_keyboard_input_type = 0;
    betterendfieldnext::ResetAndroidPcMouse();
    ConfigurationChanged("enabled=true\npc_ui_enabled=true\ndiagnostics=false\n");
    assert(!betterendfieldnext::AndroidPcMouseCaptureRequested());
    assert(Axis("HorizontalController") == 7 && refreshes == 0);
    assert(Axis("Mouse X") == 7 && refreshes == 1 && original_cursor_calls == 1);
    assert(!last_show && betterendfieldnext::AndroidPcMouseCaptureRequested());
    assert(Axis("Mouse Y") == 7 && refreshes == 1); // capture has not been acknowledged
    betterendfieldnext::SetAndroidPcMouseCaptured(true);
    betterendfieldnext::AddAndroidPcMouseMotion(2.5f, -3.25f);
    const int fallback_calls = original_axis_calls;
    assert(Axis("Mouse X") == 2.5f && Axis("Mouse Y") == 3.25f);
    assert(Axis("Mouse X") == 2.5f && Axis("Mouse Y") == 3.25f);
    assert(original_axis_calls == fallback_calls);
    assert(Axis("View X") == 7 && original_axis_calls == fallback_calls + 1);
    betterendfieldnext::DispatchAndroidFrame();
    assert(Axis("Mouse Y") == 0 && Axis("Mouse X") == 0);
    betterendfieldnext::AddAndroidPcMouseMotion(9000, 5000);
    betterendfieldnext::DispatchAndroidFrame();
    assert(Axis("Mouse X") == 9000 && Axis("Mouse Y") == -5000);
    DetourAndroidPcCursorToggle(&cursor, true, false, nullptr);
    assert(last_show && original_cursor_calls == 2 && !betterendfieldnext::AndroidPcMouseCaptureRequested());
    assert(Axis("Mouse X") == 7 && Axis("Mouse Y") == 7); // menu uses original input
    g_original_android_pc_mouse_position = OriginalPosition;
    g_android_pc_screen_width = ScreenWidth;
    g_android_pc_screen_height = ScreenHeight;
    AndroidPcMousePosition position{};
    DetourAndroidPcMousePosition(&position);
    assert(position.x == 12 && position.y == 34 && position.z == 56); // no real mouse sample
    betterendfieldnext::AddAndroidPcMouseAbsolute(0.2f, 0.25f);
    DetourAndroidPcMousePosition(&position);
    assert(position.x == 256 && position.y == 540 && position.z == 0);
    betterendfieldnext::DispatchAndroidFrame();
    betterendfieldnext::PublishAndroidPcMouse(true, true);
    DetourAndroidPcMousePosition(&position);
    assert(position.x == 256 && position.y == 540); // stable frames/publication never recenter a held click
    betterendfieldnext::SetAndroidPcDirectTouch(true);
    DetourAndroidPcMousePosition(&position);
    assert(position.x == 12 && position.y == 34);
    betterendfieldnext::SetAndroidPcDirectTouch(false);
    DetourAndroidPcMousePosition(&position);
    assert(position.x == 12 && position.y == 34); // no stale mouse after finger release
    g_original_android_pc_binding_enabled = OriginalBinding;
    g_android_pc_binding_action_field = &action_field;
    binding_action = "common_quit_game";
    assert(!DetourAndroidPcBindingEnabled(&binding, nullptr) && binding_calls == 0);
    binding_action = "common_quit_game_extra";
    assert(DetourAndroidPcBindingEnabled(&binding, nullptr) && binding_calls == 1);
    binding_action = "common_back";
    assert(DetourAndroidPcBindingEnabled(&binding, nullptr) && binding_calls == 2);
    binding_action = nullptr; binding_enabled = false;
    assert(!DetourAndroidPcBindingEnabled(&binding, nullptr) && binding_calls == 3);
    DetourAndroidPcCursorToggle(&cursor, false, false, nullptr);
    betterendfieldnext::SetAndroidPcMouseCaptured(true);
    assert(Axis("Mouse X") == 0); // no movement from before the menu survives
    ConfigurationChanged("enabled=true\npc_ui_enabled=false\ndiagnostics=false\n");
    assert(!betterendfieldnext::AndroidPcMouseCaptureRequested() && Axis("Mouse X") == 7);
    binding_action = "common_quit_game"; binding_enabled = true;
    assert(DetourAndroidPcBindingEnabled(&binding, nullptr) && binding_calls == 4); // Android quit restored when PCUI closes
    g_android_pc_axis_hook_ready = false;
    ConfigurationChanged("enabled=true\npc_ui_enabled=true\ndiagnostics=false\n");
    assert(!betterendfieldnext::AndroidPcMouseCaptureRequested() && Axis("Mouse Y") == 7);
    Shutdown();
    assert(!betterendfieldnext::AndroidPcMouseCaptureRequested() && !g_android_pc_cursor_intent_known);
    std::cout << "PASS Android PC mouse runtime: cursor lifecycle, relative axes, real absolute position scaling, touch fallback, quit binding eligibility and shutdown\n";
}
