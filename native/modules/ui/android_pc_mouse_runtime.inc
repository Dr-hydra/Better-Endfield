using AndroidPcCursorToggleFn = void(__fastcall*)(void*, bool, bool, void*);
using AndroidPcGetAxisFn = float(__fastcall*)(void*, void*, void*);
AndroidPcCursorToggleFn g_original_android_pc_cursor_toggle = nullptr;
AndroidPcGetAxisFn g_original_android_pc_get_axis = nullptr;
const void* g_android_pc_cursor_calc_state_method = nullptr;
const void* g_android_pc_real_cursor_field = nullptr;
std::atomic_bool g_android_pc_cursor_hook_ready{false};
std::atomic_bool g_android_pc_axis_hook_ready{false};
std::atomic_bool g_android_pc_cursor_intent_known{false};
std::atomic_uint64_t g_next_android_pc_cursor_refresh_tick{0};
using AndroidPcPositionFn = void(*)(AndroidPcMousePosition*);
using AndroidPcBindingEnabledFn = bool(__fastcall*)(void*, void*);
AndroidPcPositionFn g_original_android_pc_mouse_position = nullptr;
AndroidPcBindingEnabledFn g_original_android_pc_binding_enabled = nullptr;
GetInt32Fn g_android_pc_screen_width = nullptr, g_android_pc_screen_height = nullptr;
const void* g_android_pc_screen_width_method = nullptr;
const void* g_android_pc_screen_height_method = nullptr;
const void* g_android_pc_binding_action_field = nullptr;

void DetourAndroidPcMousePosition(AndroidPcMousePosition* position) {
    if (g_original_android_pc_mouse_position) g_original_android_pc_mouse_position(position);
    if (!position || !g_pc_ui_enabled.load(std::memory_order_acquire) ||
        !g_android_pc_screen_width || !g_android_pc_screen_height) return;
    float x = 0.0f, y = 0.0f;
    if (!betterendfieldnext::ReadAndroidPcMouseAbsolute(x, y)) return;
    const int32_t width = g_android_pc_screen_width(const_cast<void*>(g_android_pc_screen_width_method));
    const int32_t height = g_android_pc_screen_height(const_cast<void*>(g_android_pc_screen_height_method));
    if (width <= 0 || height <= 0) return;
    // Java observes coordinates local to Unity's root view dispatch, normalizes
    // against the live view, and excludes our synthetic capture-center input.
    // Unity screen pixels point up, Android view coordinates point down.
    *position = {x * width, (1.0f - y) * height, 0.0f};
}

void InstallAndroidPcMousePositionHook() {
    if (!g_host || !g_host->create_hook || !g_android_pc_screen_width || !g_android_pc_screen_height) return;
    using ResolveIcallFn = void*(*)(const char*);
    const auto resolve = reinterpret_cast<ResolveIcallFn>(GetProcAddress(
        GetModuleHandleW(L"GameAssembly.dll"), "il2cpp_resolve_icall"));
    void* target = resolve ? resolve("UnityEngine.Input::get_mousePosition_Injected(UnityEngine.Vector3&)") : nullptr;
    // GetMousePos in the Android sample resolves and calls this icall directly,
    // bypassing the managed Input wrapper. Hook the shared native boundary so
    // Rewired and the game's mouse-position readers observe the same sample.
    if (target && g_host->create_hook(g_host->context, kModuleId, target,
            reinterpret_cast<void*>(&DetourAndroidPcMousePosition),
            reinterpret_cast<void**>(&g_original_android_pc_mouse_position)) == BE_Result_Ok &&
            g_original_android_pc_mouse_position) {
        g_android_pc_absolute_hook_ready = true;
        Log("Successfully hooked: android_pc_mouse.position_icall");
    } else Log("Optional mouse-position icall hook unavailable; retaining Unity absolute input");
}

bool __fastcall DetourAndroidPcBindingEnabled(void* instance, void* method) {
    if (instance && g_pc_ui_enabled.load(std::memory_order_acquire) &&
        g_android_pc_binding_action_field && g_host && g_host->field_get_value_object &&
        g_host->copy_managed_string) {
        void* action = g_host->field_get_value_object(g_host->context, g_android_pc_binding_action_field, instance);
        char name[32]{};
        if (action && g_host->copy_managed_string(g_host->context, action, name, sizeof(name)) > 0 &&
            std::string_view(name) == "common_quit_game") {
            // The Android MainHud adds this binding in addition to the normal
            // UI back/terminal binding. Disable it at eligibility checking,
            // before the checker chooses/blocks lower-priority actions.
            return false;
        }
    }
    return g_original_android_pc_binding_enabled ? g_original_android_pc_binding_enabled(instance, method) : false;
}

void __fastcall DetourAndroidPcCursorToggle(
    void* instance, bool show_cursor, bool force_update, void* method) {
    if (g_original_android_pc_cursor_toggle)
        g_original_android_pc_cursor_toggle(instance, show_cursor, force_update, method);
    g_android_pc_cursor_intent_known.store(true, std::memory_order_release);
    betterendfieldnext::SetAndroidPcCursorRequest(show_cursor);
    if (g_pc_ui_enabled.load(std::memory_order_acquire) &&
        g_diagnostics_enabled.load(std::memory_order_relaxed)) {
        static thread_local int32_t last_request = -1;
        const int32_t request = show_cursor ? 1 : 0;
        if (request != last_request) {
            last_request = request;
            Log(std::string("Android PC mouse cursor request: show=") +
                (show_cursor ? "true" : "false") + ", relative_capture=" +
                (betterendfieldnext::AndroidPcMouseCaptureRequested() ? "requested" : "off"));
        }
    }
}

// The module may attach after InputManager initialized its cursor. Use its
// existing priority/request calculation once, from the first live mouse-axis
// read, rather than guessing menu state from an unsupported native Cursor lock.
void PrimeAndroidPcCursorIntent(void* input_manager) {
    if (!input_manager || g_android_pc_cursor_intent_known.load(std::memory_order_acquire) ||
        !g_android_pc_cursor_calc_state_method || !g_android_pc_real_cursor_field ||
        !g_host || !g_host->field_get_value_object || !g_host->runtime_invoke) return;
    static thread_local bool refreshing = false;
    if (refreshing) return;
    const uint64_t now = GetTickCount64();
    if (now < g_next_android_pc_cursor_refresh_tick.load(std::memory_order_acquire)) return;
    g_next_android_pc_cursor_refresh_tick.store(now + 250, std::memory_order_release);
    void* cursor_manager = g_host->field_get_value_object(
        g_host->context, g_android_pc_real_cursor_field, input_manager);
    if (!cursor_manager) return;
    refreshing = true;
    bool force_update = true;
    void* parameters[]{&force_update};
    Invoke(g_android_pc_cursor_calc_state_method, cursor_manager, parameters);
    refreshing = false;
}

float __fastcall DetourAndroidPcGetAxis(void* instance, void* axis_name, void* method) {
    if (g_pc_ui_enabled.load(std::memory_order_acquire) &&
        g_android_pc_cursor_hook_ready && g_android_pc_axis_hook_ready &&
        g_host && g_host->copy_managed_string && axis_name) {
        char name[32]{};
        if (g_host->copy_managed_string(g_host->context, axis_name, name, sizeof(name)) > 0) {
            const std::string_view axis(name);
            if (axis == "Mouse X" || axis == "Mouse Y") {
                PrimeAndroidPcCursorIntent(instance);
                float x = 0.0f, y = 0.0f;
                if (betterendfieldnext::ReadAndroidPcMouseMotion(x, y)) {
                    // Captured Android coordinates point down; Rewired's Mouse
                    // Y and the game's normalized-screen camera path point up.
                    // The game retains its downstream Lua/camera gains. This
                    // supplies pixel deltas at the named-axis boundary rather
                    // than emulating Rewired's per-device axis calibration.
                    return axis == "Mouse X" ? x : -y;
                }
            }
        }
    }
    return g_original_android_pc_get_axis ?
        g_original_android_pc_get_axis(instance, axis_name, method) : 0.0f;
}
