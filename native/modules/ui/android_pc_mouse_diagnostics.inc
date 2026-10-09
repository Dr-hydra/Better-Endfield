// Read-only Android PC-layout diagnostics. Keep engine cursor state separate
// from gameplay sensitivity: a locked-state request is not proof that Android
// is delivering relative mouse motion.
struct AndroidPcMousePosition {
    float x = 0.0f, y = 0.0f, z = 0.0f;
};

struct AndroidPcMouseDiagnostics {
    const void* lock_state = nullptr;
    const void* visible = nullptr;
    const void* position = nullptr;
    const void* screen_width = nullptr;
    const void* screen_height = nullptr;
    const void* axis_raw = nullptr;
    bool axis_failed = false;
    uint64_t next_sample_tick = 0;
    uint64_t next_report_tick = 0;
    int32_t last_lock_state = -1;
    int32_t last_visible = -1;
    uint32_t samples = 0, edge_samples = 0, axis_samples = 0;
    float max_axis_x = 0.0f, max_axis_y = 0.0f;
    float min_x = 0.0f, max_x = 0.0f, min_y = 0.0f, max_y = 0.0f;
};

AndroidPcMouseDiagnostics g_android_pc_mouse_diagnostics;
std::atomic_bool g_android_pc_mouse_diagnostics_reset{true};

void ResolveAndroidPcMouseDiagnostics() {
    auto& state = g_android_pc_mouse_diagnostics;
    auto resolve = [&](const char* assembly, const char* class_name,
        const char* method, const char* parameters, const char* result,
        uint32_t count) -> const void* {
        const BE_MethodDescriptorV1 descriptor{assembly, "UnityEngine", class_name,
            method, parameters, result, count};
        BE_ResolvedMethodV1 resolved{};
        return g_host->resolve_method(g_host->context, &descriptor, &resolved) ==
            BE_Result_Ok ? resolved.method_info : nullptr;
    };
    state.lock_state = resolve("UnityEngine.CoreModule.dll", "Cursor",
        "get_lockState", nullptr, "UnityEngine.CursorLockMode", 0);
    state.visible = resolve("UnityEngine.CoreModule.dll", "Cursor",
        "get_visible", nullptr, "System.Boolean", 0);
    state.position = resolve("UnityEngine.InputLegacyModule.dll", "Input",
        "get_mousePosition", nullptr, "UnityEngine.Vector3", 0);
    state.screen_width = resolve("UnityEngine.CoreModule.dll", "Screen",
        "get_width", nullptr, "System.Int32", 0);
    state.screen_height = resolve("UnityEngine.CoreModule.dll", "Screen",
        "get_height", nullptr, "System.Int32", 0);
    state.axis_raw = resolve("UnityEngine.InputLegacyModule.dll", "Input",
        "GetAxisRaw", "System.String", "System.Single", 1);
    Log(std::string("Android PC mouse diagnostics: cursor=") +
        (state.lock_state && state.visible ? "ready" : "unavailable") +
        ", position=" + (state.position ? "ready" : "unavailable") +
        ", screen=" + (state.screen_width && state.screen_height ? "ready" : "unavailable") +
        ", legacy_axis=" + (state.axis_raw ? "ready" : "unavailable"));
}

void ResetAndroidPcMouseSamples(AndroidPcMouseDiagnostics& state) {
    state.samples = state.edge_samples = state.axis_samples = 0;
    state.max_axis_x = state.max_axis_y = 0.0f;
}

void PumpAndroidPcMouseDiagnostics() {
    auto& state = g_android_pc_mouse_diagnostics;
    if (g_android_pc_mouse_diagnostics_reset.exchange(false, std::memory_order_acq_rel)) {
        state.next_sample_tick = state.next_report_tick = 0;
        state.last_lock_state = state.last_visible = -1;
        state.axis_failed = false;
        ResetAndroidPcMouseSamples(state);
    }
    if (!g_pc_ui_enabled.load(std::memory_order_acquire) ||
        !g_diagnostics_enabled.load(std::memory_order_relaxed) ||
        !betterendfieldnext::AndroidForeground()) return;
    const uint64_t now = GetTickCount64();
    if (now < state.next_sample_tick) return;
    state.next_sample_tick = now + 250;

    int32_t lock_state = -1, width = 0, height = 0;
    bool visible = false;
    const bool have_lock = Unbox(Invoke(state.lock_state, nullptr, nullptr), lock_state);
    const bool have_visible = Unbox(Invoke(state.visible, nullptr, nullptr), visible);
    const int32_t cursor_visible = have_visible ? (visible ? 1 : 0) : -1;
    AndroidPcMousePosition position{};
    const bool have_position = Unbox(Invoke(state.position, nullptr, nullptr), position) &&
        std::isfinite(position.x) && std::isfinite(position.y);
    const bool have_screen = Unbox(Invoke(state.screen_width, nullptr, nullptr), width) &&
        Unbox(Invoke(state.screen_height, nullptr, nullptr), height) && width > 0 && height > 0;
    if (have_position) {
        if (state.samples == 0) {
            state.min_x = state.max_x = position.x;
            state.min_y = state.max_y = position.y;
        }
        state.min_x = std::min(state.min_x, position.x);
        state.max_x = std::max(state.max_x, position.x);
        state.min_y = std::min(state.min_y, position.y);
        state.max_y = std::max(state.max_y, position.y);
        ++state.samples;
        if (have_screen && (position.x <= 1.0f || position.x >= width - 1.0f ||
            position.y <= 1.0f || position.y >= height - 1.0f)) ++state.edge_samples;
    }
    // These are legacy engine axes, sampled four times per second. They are
    // not a complete movement counter and may differ from Rewired's input.
    if (state.axis_raw && !state.axis_failed && g_host && g_host->string_new) {
        auto axis = [&](const char* name, float& value) {
            void* managed = g_host->string_new(g_host->context, name);
            void* parameters[]{managed};
            return managed && Unbox(Invoke(state.axis_raw, nullptr, parameters), value) &&
                std::isfinite(value);
        };
        float x = 0.0f, y = 0.0f;
        if (axis("Mouse X", x) && axis("Mouse Y", y)) {
            ++state.axis_samples;
            state.max_axis_x = std::max(state.max_axis_x, std::fabs(x));
            state.max_axis_y = std::max(state.max_axis_y, std::fabs(y));
        } else {
            state.axis_failed = true; // missing axis mappings must not throw every sample
            Log("Android PC mouse: legacy axes unavailable; continuing cursor/position diagnostics.");
        }
    }
    const bool changed = (have_lock ? lock_state : -1) != state.last_lock_state ||
        cursor_visible != state.last_visible;
    if (!changed && now < state.next_report_tick) return;
    state.last_lock_state = have_lock ? lock_state : -1;
    state.last_visible = cursor_visible;
    state.next_report_tick = now + 5000;
    const auto relative = betterendfieldnext::InspectAndroidPcMouse();
    char buffer[850];
    std::snprintf(buffer, sizeof(buffer),
        "Android PC mouse: lock=%d, visible=%d, position=(%.1f,%.1f)%s, screen=%dx%d%s, "
        "sampled_range=(%.1f..%.1f,%.1f..%.1f), edge_samples=%u/%u, "
        "legacy_axis_abs_max=(%.4f,%.4f), axis_samples=%u (4Hz samples; not total movement), "
        "relative_requested=%s, captured=%s, relative_events=%llu, axis_reads=%llu, relative_snapshot=(%.3f,%.3f), "
        "absolute_hook_ready=%s, absolute_events=%llu, position_reads=%llu, absolute_valid=%s",
        state.last_lock_state, cursor_visible, position.x, position.y,
        have_position ? "" : " [unavailable]", width, height,
        have_screen ? "" : " [unavailable]", state.min_x, state.max_x,
        state.min_y, state.max_y, state.edge_samples, state.samples,
        state.max_axis_x, state.max_axis_y, state.axis_samples,
        relative.requested ? "true" : "false", relative.captured ? "true" : "false",
        static_cast<unsigned long long>(relative.motion_events),
        static_cast<unsigned long long>(relative.axis_reads), relative.x, relative.y,
        g_android_pc_absolute_hook_ready ? "true" : "false",
        static_cast<unsigned long long>(relative.absolute_events),
        static_cast<unsigned long long>(relative.position_reads), relative.absolute_valid ? "true" : "false");
    Log(buffer);
    ResetAndroidPcMouseSamples(state);
}
