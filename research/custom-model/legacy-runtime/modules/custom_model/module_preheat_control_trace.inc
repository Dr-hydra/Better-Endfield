// Read-only residency control for distinguishing targeted character preload
// from broad/global character warmup.
//
// Endministrator is the positive subject. Aglina (chr_0013_aglina) is the
// negative control for the current test because the user keeps her out of the
// active squad. We observe both the high-level I18N ring and Unity's loaded
// SkinnedMeshRenderer population. The latter matters because the gameplay
// Endministrator test already proved that postmodel residency can appear
// without an I18NAssetLoader.Load(targetHash) hit.

constexpr uint64_t kPreheatControlScanIntervalMs = 250;
constexpr uint64_t kPreheatEndminPostmodelHash = 0x0C9AC03D143EA082ULL;
constexpr uint64_t kPreheatAglinaPostmodelHash = 0x0C662A5986756356ULL;
constexpr char kPreheatEndminToken[] = "endminf";
constexpr char kPreheatAglinaToken[] = "aglina";
constexpr char kPreheatEndminPostmodel[] = "chr_0003_endminf_postmodel";
constexpr char kPreheatAglinaPostmodel[] = "chr_0013_aglina_postmodel";

struct PreheatResidencyState {
    size_t resident_renderers = 0;
    size_t active_scene_renderers = 0;
    size_t proxy_manager_renderers = 0;
    size_t template_renderers = 0;
    std::vector<std::string> roots;

    bool operator==(const PreheatResidencyState& other) const {
        return resident_renderers == other.resident_renderers &&
            active_scene_renderers == other.active_scene_renderers &&
            proxy_manager_renderers == other.proxy_manager_renderers &&
            template_renderers == other.template_renderers &&
            roots == other.roots;
    }
    bool operator!=(const PreheatResidencyState& other) const {
        return !(*this == other);
    }
};

const BE_HostApiV1* g_preheat_control_real_host = nullptr;
BE_HostApiV1 g_preheat_control_host_proxy{};
bool g_preheat_control_pump_intercepted = false;
uint64_t g_preheat_control_next_scan_tick = 0;
bool g_preheat_endmin_i18n_seen = false;
bool g_preheat_aglina_i18n_seen = false;
bool g_preheat_endmin_resident_seen = false;
bool g_preheat_aglina_resident_seen = false;
bool g_preheat_have_previous = false;
PreheatResidencyState g_preheat_previous_endmin{};
PreheatResidencyState g_preheat_previous_aglina{};

std::string PreheatJoinRoots(const std::vector<std::string>& roots) {
    if (roots.empty()) return "[]";
    std::string text = "[";
    for (size_t index = 0; index < roots.size(); ++index) {
        if (index) text += " | ";
        text += roots[index];
    }
    text += "]";
    return text;
}

void PreheatObserveI18NRing() {
    std::lock_guard<std::mutex> lock(g_natural_trace_load_mutex);
    const size_t start =
        (g_natural_trace_load_write + kNaturalTraceLoadRingSize -
            g_natural_trace_load_count) % kNaturalTraceLoadRingSize;
    for (size_t offset = 0; offset < g_natural_trace_load_count; ++offset) {
        const NaturalTraceLoadEvent& event =
            g_natural_trace_load_ring[
                (start + offset) % kNaturalTraceLoadRingSize];
        if (event.path_hash == kPreheatEndminPostmodelHash)
            g_preheat_endmin_i18n_seen = true;
        if (event.path_hash == kPreheatAglinaPostmodelHash)
            g_preheat_aglina_i18n_seen = true;
    }
}

void PreheatAddRoot(PreheatResidencyState& state, std::string root) {
    if (root.empty()) root = "<unknown-root>";
    if (std::find(state.roots.begin(), state.roots.end(), root) ==
        state.roots.end()) {
        state.roots.push_back(std::move(root));
    }
}

void PreheatClassifyRenderer(void* renderer, std::string_view token,
    std::string_view postmodel, PreheatResidencyState& state) {
    if (!renderer) return;
    const std::string renderer_name = ObjectName(renderer);
    if (renderer_name.find(token) == std::string::npos) return;

    const std::string path = BuildTransformPath(renderer);
    ++state.resident_renderers;
    if (IsActiveSceneRenderer(renderer)) ++state.active_scene_renderers;
    if (path.find("PrefabAssetProxyManager") != std::string::npos)
        ++state.proxy_manager_renderers;

    const std::string root = PathRoot(path);
    // Loaded prefab/template renderers are resident but are neither active
    // scene instances nor children of the proxy-manager staging area.
    if (!IsActiveSceneRenderer(renderer) &&
        path.find("PrefabAssetProxyManager") == std::string::npos &&
        root.find(postmodel) != std::string::npos) {
        ++state.template_renderers;
    }
    PreheatAddRoot(state, root);
}

void PreheatCollectResidency(
    PreheatResidencyState& endmin, PreheatResidencyState& aglina) {
    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* objects = Invoke(
        Contract("resources.find_objects_of_type_all"), nullptr, parameters);
    const int count = std::max(ArrayLength(objects), 0);
    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (!renderer) continue;
        const std::string renderer_name = ObjectName(renderer);
        if (renderer_name.find(kPreheatEndminToken) != std::string::npos) {
            PreheatClassifyRenderer(renderer, kPreheatEndminToken,
                kPreheatEndminPostmodel, endmin);
        } else if (renderer_name.find(kPreheatAglinaToken) !=
            std::string::npos) {
            PreheatClassifyRenderer(renderer, kPreheatAglinaToken,
                kPreheatAglinaPostmodel, aglina);
        }
    }
    std::sort(endmin.roots.begin(), endmin.roots.end());
    std::sort(aglina.roots.begin(), aglina.roots.end());
}

void PreheatLogState(const char* label, const PreheatResidencyState& state,
    bool i18n_seen) {
    Log(std::string("[PREHEAT-CONTROL] ") + label +
        " resident=" + std::to_string(state.resident_renderers) +
        " active=" + std::to_string(state.active_scene_renderers) +
        " proxyManager=" + std::to_string(state.proxy_manager_renderers) +
        " template=" + std::to_string(state.template_renderers) +
        " i18nSeen=" + std::to_string(i18n_seen ? 1 : 0) +
        " roots=" + PreheatJoinRoots(state.roots));
}

void PumpPreheatControlTrace() {
    const uint64_t now = GetTickCount64();
    if (now < g_preheat_control_next_scan_tick) return;
    g_preheat_control_next_scan_tick = now + kPreheatControlScanIntervalMs;

    PreheatObserveI18NRing();
    PreheatResidencyState endmin;
    PreheatResidencyState aglina;
    PreheatCollectResidency(endmin, aglina);

    const bool changed = !g_preheat_have_previous ||
        endmin != g_preheat_previous_endmin ||
        aglina != g_preheat_previous_aglina;
    if (changed) {
        PreheatLogState("ENDMIN", endmin, g_preheat_endmin_i18n_seen);
        PreheatLogState("AGLINA-CONTROL", aglina,
            g_preheat_aglina_i18n_seen);
        g_preheat_previous_endmin = endmin;
        g_preheat_previous_aglina = aglina;
        g_preheat_have_previous = true;
    }

    if (!g_preheat_endmin_resident_seen &&
        endmin.resident_renderers != 0) {
        g_preheat_endmin_resident_seen = true;
        Log("[PREHEAT-CONTROL] ENDMIN first became resident. Compare this timestamp with AGLINA-CONTROL and the later gameplay attach event.");
    }
    if (!g_preheat_aglina_resident_seen &&
        aglina.resident_renderers != 0) {
        g_preheat_aglina_resident_seen = true;
        Log("[PREHEAT-CONTROL] AGLINA-CONTROL first became resident while serving as the non-squad control. If she is still not in the squad, this is evidence for preload broader than the active team.");
    }
}

void __fastcall DetourPumpWithPreheatControlTrace(void* method_info) {
    DetourPumpWithRendererArrivalTrace(method_info);
    PumpPreheatControlTrace();
}

BE_Result BE_CALL PreheatControlCreateHook(
    void* context, const char* module_id, void* target, void* detour,
    void** original) {
    if (!g_preheat_control_real_host ||
        !g_preheat_control_real_host->create_hook) {
        return BE_Result_NotReady;
    }
    if (detour == reinterpret_cast<void*>(&DetourPumpWithRendererArrivalTrace)) {
        g_preheat_control_pump_intercepted = true;
        return g_preheat_control_real_host->create_hook(
            context, module_id, target,
            reinterpret_cast<void*>(&DetourPumpWithPreheatControlTrace),
            original);
    }
    return g_preheat_control_real_host->create_hook(
        context, module_id, target, detour, original);
}

BE_Result BE_CALL InitializeWithPreheatControlTrace(
    const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;
    g_preheat_control_real_host = host;
    g_preheat_control_host_proxy = *host;
    g_preheat_control_host_proxy.create_hook = &PreheatControlCreateHook;
    g_preheat_control_pump_intercepted = false;
    g_preheat_control_next_scan_tick = 0;
    g_preheat_endmin_i18n_seen = false;
    g_preheat_aglina_i18n_seen = false;
    g_preheat_endmin_resident_seen = false;
    g_preheat_aglina_resident_seen = false;
    g_preheat_have_previous = false;
    g_preheat_previous_endmin = {};
    g_preheat_previous_aglina = {};

    const BE_Result result =
        InitializeWithRendererArrivalTrace(&g_preheat_control_host_proxy);
    if (result != BE_Result_Ok) return result;
    if (!g_preheat_control_pump_intercepted) {
        Log("[PREHEAT-CONTROL] failed to intercept renderer-arrival main-thread pump.");
        return BE_Result_Failed;
    }

    Log("[PREHEAT-CONTROL] armed: ENDMIN is the gameplay subject; chr_0013_aglina is the non-squad preload control.");
    Log("[PREHEAT-CONTROL] Aglina target hash=0x0C662A5986756356 postmodel=chr_0013_aglina_postmodel. Observation is read-only and issues no resource load.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithPreheatControlTrace() {
    ShutdownWithRendererArrivalTrace();
    g_preheat_control_next_scan_tick = 0;
    g_preheat_have_previous = false;
    g_preheat_previous_endmin = {};
    g_preheat_previous_aglina = {};
    g_preheat_control_pump_intercepted = false;
    g_preheat_control_real_host = nullptr;
    g_preheat_control_host_proxy = {};
}
