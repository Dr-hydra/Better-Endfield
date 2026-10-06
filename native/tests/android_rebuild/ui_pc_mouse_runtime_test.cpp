#include "../../modules/ui/module.cpp"
#include <cassert>
#include <iostream>
using namespace BetterEndfield::UiModule;
static int manager, cursor, field, calculate;
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
    betterendfield::ResetAndroidPcMouse();
    ConfigurationChanged("enabled=true\npc_ui_enabled=true\ndiagnostics=false\n");
    assert(!betterendfield::AndroidPcMouseCaptureRequested());
    assert(Axis("HorizontalController") == 7 && refreshes == 0);
    assert(Axis("Mouse X") == 7 && refreshes == 1 && original_cursor_calls == 1);
    assert(!last_show && betterendfield::AndroidPcMouseCaptureRequested());
    assert(Axis("Mouse Y") == 7 && refreshes == 1); // capture has not been acknowledged
    betterendfield::SetAndroidPcMouseCaptured(true);
    betterendfield::AddAndroidPcMouseMotion(2.5f, -3.25f);
    const int fallback_calls = original_axis_calls;
    assert(Axis("Mouse X") == 2.5f && Axis("Mouse Y") == 3.25f);
    assert(Axis("Mouse X") == 2.5f && Axis("Mouse Y") == 3.25f);
    assert(original_axis_calls == fallback_calls);
    assert(Axis("View X") == 7 && original_axis_calls == fallback_calls + 1);
    betterendfield::DispatchAndroidFrame();
    assert(Axis("Mouse Y") == 0 && Axis("Mouse X") == 0);
    betterendfield::AddAndroidPcMouseMotion(9000, 5000);
    betterendfield::DispatchAndroidFrame();
    assert(Axis("Mouse X") == 9000 && Axis("Mouse Y") == -5000);
    DetourAndroidPcCursorToggle(&cursor, true, false, nullptr);
    assert(last_show && original_cursor_calls == 2 && !betterendfield::AndroidPcMouseCaptureRequested());
    assert(Axis("Mouse X") == 7 && Axis("Mouse Y") == 7); // menu uses original input
    DetourAndroidPcCursorToggle(&cursor, false, false, nullptr);
    betterendfield::SetAndroidPcMouseCaptured(true);
    assert(Axis("Mouse X") == 0); // no movement from before the menu survives
    ConfigurationChanged("enabled=true\npc_ui_enabled=false\ndiagnostics=false\n");
    assert(!betterendfield::AndroidPcMouseCaptureRequested() && Axis("Mouse X") == 7);
    g_android_pc_axis_hook_ready = false;
    ConfigurationChanged("enabled=true\npc_ui_enabled=true\ndiagnostics=false\n");
    assert(!betterendfield::AndroidPcMouseCaptureRequested() && Axis("Mouse Y") == 7);
    Shutdown();
    assert(!betterendfield::AndroidPcMouseCaptureRequested() && !g_android_pc_cursor_intent_known);
    std::cout << "PASS Android PC mouse runtime: original cursor lifecycle, lazy priority refresh, actual capture fallback, Mouse X/Y consumer, repeated reads, axis passthrough and shutdown\n";
}
