// Reverse trigger for the natural resource tracer.
//
// The first tracer waited for a known postmodel Load/Clone and then inspected
// the live model.  The 2026-09-11 test proved that the character can appear
// normally without either of those high-level hooks firing.  This layer turns
// the experiment around: observe the first game-owned Endminf renderer set,
// then dump what the already-installed I18N hook saw immediately before it.
//
// Read-only policy remains strict: no resource is loaded, retained, disposed,
// instantiated, replaced or modified by this layer.

constexpr uint64_t kRendererArrivalScanIntervalMs = 50;
constexpr uint64_t kRendererArrivalHistoryMs = 10000;
constexpr size_t kRendererArrivalMaxHistoryLines = 160;

const BE_HostApiV1* g_renderer_arrival_real_host = nullptr;
BE_HostApiV1 g_renderer_arrival_host_proxy{};
bool g_renderer_arrival_pump_intercepted = false;
uint64_t g_renderer_arrival_next_scan_tick = 0;
size_t g_renderer_arrival_last_count = 0;
bool g_renderer_arrival_initial_scan_done = false;
bool g_renderer_arrival_triggered = false;

void RendererArrivalDumpLoadHistory(uint64_t trigger_tick) {
    std::vector<NaturalTraceLoadEvent> events;
    size_t ring_count = 0;
    {
        std::lock_guard<std::mutex> lock(g_natural_trace_load_mutex);
        ring_count = g_natural_trace_load_count;
        events.reserve(ring_count);
        const size_t start =
            (g_natural_trace_load_write + kNaturalTraceLoadRingSize -
                g_natural_trace_load_count) % kNaturalTraceLoadRingSize;
        for (size_t offset = 0; offset < g_natural_trace_load_count; ++offset) {
            const NaturalTraceLoadEvent& event =
                g_natural_trace_load_ring[
                    (start + offset) % kNaturalTraceLoadRingSize];
            if (event.tick > trigger_tick ||
                event.tick + kRendererArrivalHistoryMs < trigger_tick) {
                continue;
            }
            events.push_back(event);
        }
    }

    Log("[RENDERER-ARRIVAL] I18N ring at first live Endminf set: total=" +
        std::to_string(ring_count) + " recent=" +
        std::to_string(events.size()) + " targetLoads=" +
        std::to_string(g_natural_trace_target_load_count.load()) +
        " targetClones=" +
        std::to_string(g_natural_trace_target_clone_count.load()));

    if (events.empty()) {
        Log("[RENDERER-ARRIVAL] no I18NAssetLoader.Load call was observed in the preceding 10 seconds. If the character is visible, its assembly path is below or beside this high-level loader hook.");
        return;
    }

    if (events.size() > kRendererArrivalMaxHistoryLines) {
        events.erase(events.begin(),
            events.end() - static_cast<std::ptrdiff_t>(
                kRendererArrivalMaxHistoryLines));
        Log("[RENDERER-ARRIVAL] history truncated to newest " +
            std::to_string(kRendererArrivalMaxHistoryLines) + " entries.");
    }

    std::vector<std::string> emitted;
    for (const NaturalTraceLoadEvent& event : events) {
        std::string path = NaturalTracePathForHash(event.path_hash);
        std::string type = NaturalTraceTypeName(event.type_object);
        if (path.empty()) path = "<unresolved>";
        if (type.empty()) type = "<unknown-type>";
        const int64_t delta = static_cast<int64_t>(event.tick) -
            static_cast<int64_t>(trigger_tick);
        std::string line = "dt=" + std::to_string(delta) + "ms hash=" +
            NaturalTraceHash(event.path_hash) + " category=" +
            std::to_string(event.category) + " thread=" +
            std::to_string(event.thread_id) + " type=\"" + type +
            "\" path=\"" + path + "\"";
        if (std::find(emitted.begin(), emitted.end(), line) != emitted.end())
            continue;
        emitted.push_back(line);
        Log("[RENDERER-ARRIVAL] I18N-HISTORY " + line);
    }
}

void PumpRendererArrivalTrace() {
    if (g_renderer_arrival_triggered) return;
    const uint64_t now = GetTickCount64();
    if (now < g_renderer_arrival_next_scan_tick) return;
    g_renderer_arrival_next_scan_tick = now + kRendererArrivalScanIntervalMs;

    const std::vector<RendererSummary> summaries =
        EnumerateLiveEndminfRenderers();
    const size_t count = summaries.size();

    if (!g_renderer_arrival_initial_scan_done) {
        g_renderer_arrival_initial_scan_done = true;
        g_renderer_arrival_last_count = count;
        Log("[RENDERER-ARRIVAL] baseline live Endminf renderers=" +
            std::to_string(count));
        if (count > 0) {
            // Still useful: this distinguishes a genuinely late hook install
            // from a later 0 -> N assembly transition.
            Log("[RENDERER-ARRIVAL] Endminf renderers already existed at the first main-thread scan; natural-resource hooks may have installed after the relevant assembly work.");
            RendererArrivalDumpLoadHistory(now);
            g_renderer_arrival_triggered = true;
        }
        return;
    }

    if (count == g_renderer_arrival_last_count) return;
    Log("[RENDERER-ARRIVAL] live Endminf renderer count changed " +
        std::to_string(g_renderer_arrival_last_count) + " -> " +
        std::to_string(count));
    const size_t previous = g_renderer_arrival_last_count;
    g_renderer_arrival_last_count = count;

    if (previous != 0 || count == 0) return;
    g_renderer_arrival_triggered = true;

    Log("[RENDERER-ARRIVAL] FIRST-LIVE-ENDMINF detected; using renderer assembly as the correlation trigger instead of assuming a postmodel Load/Clone entry point.");
    RendererArrivalDumpLoadHistory(now);

    // Reuse the natural tracer's post-trigger snapshots and +3s load-window
    // report. Only arm it here when neither high-level hook has already done so.
    if (g_natural_trace_epoch.load(std::memory_order_acquire) == 0) {
        NaturalTraceTrigger(now, "first live Endminf renderer set");
    }
    NaturalTraceLogLiveSnapshot("ARRIVAL");
}

void __fastcall DetourPumpWithRendererArrivalTrace(void* method_info) {
    DetourPumpWithNaturalResourceTrace(method_info);
    PumpRendererArrivalTrace();
}

BE_Result BE_CALL RendererArrivalTraceCreateHook(
    void* context, const char* module_id, void* target, void* detour,
    void** original) {
    if (!g_renderer_arrival_real_host ||
        !g_renderer_arrival_real_host->create_hook) {
        return BE_Result_NotReady;
    }
    if (detour == reinterpret_cast<void*>(&DetourPumpWithNaturalResourceTrace)) {
        g_renderer_arrival_pump_intercepted = true;
        return g_renderer_arrival_real_host->create_hook(
            context, module_id, target,
            reinterpret_cast<void*>(&DetourPumpWithRendererArrivalTrace),
            original);
    }
    return g_renderer_arrival_real_host->create_hook(
        context, module_id, target, detour, original);
}

BE_Result BE_CALL InitializeWithRendererArrivalTrace(
    const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;

    g_renderer_arrival_real_host = host;
    g_renderer_arrival_host_proxy = *host;
    g_renderer_arrival_host_proxy.create_hook = &RendererArrivalTraceCreateHook;
    g_renderer_arrival_pump_intercepted = false;
    g_renderer_arrival_next_scan_tick = 0;
    g_renderer_arrival_last_count = 0;
    g_renderer_arrival_initial_scan_done = false;
    g_renderer_arrival_triggered = false;

    const BE_Result result =
        InitializeWithNaturalResourceTrace(&g_renderer_arrival_host_proxy);
    if (result != BE_Result_Ok) return result;
    if (!g_renderer_arrival_pump_intercepted) {
        Log("[RENDERER-ARRIVAL] failed to intercept natural-trace main-thread pump.");
        return BE_Result_Failed;
    }

    Log("[RENDERER-ARRIVAL] armed: first 0 -> N live Endminf renderer transition will dump the preceding 10 seconds of naturally observed resource requests.");
    Log("[RENDERER-ARRIVAL] read-only: no Load/Get/Dispose/Instantiate/replacement calls are issued by this layer.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithRendererArrivalTrace() {
    g_renderer_arrival_triggered = true;
    ShutdownWithNaturalResourceTrace();
    g_renderer_arrival_next_scan_tick = 0;
    g_renderer_arrival_last_count = 0;
    g_renderer_arrival_initial_scan_done = false;
    g_renderer_arrival_pump_intercepted = false;
    g_renderer_arrival_real_host = nullptr;
    g_renderer_arrival_host_proxy = {};
}
