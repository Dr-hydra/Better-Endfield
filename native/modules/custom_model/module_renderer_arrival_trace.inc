// Reverse trigger for the natural resource tracer.
//
// The first tracer waited for a known postmodel Load/Clone and then inspected
// the live model. The 2026-09-11 tests proved two important facts:
//   1. Endminf can appear normally without the postmodel Load/Clone firing.
//   2. The login/intro actor is naturally loaded as
//      Assets/Beyond/InitialAssets/Intro/Prefab/SK_actor_female.prefab.
//
// SK_actor_female is a special splash/intro actor. It may reuse Endministrator
// mesh/material naming or other character resources, but it is not evidence for
// the gameplay character assembly seam. Keep observing it at the low-level load
// tracer, but exclude its renderer container from renderer-arrival events.
//
// This layer therefore keeps observing renderer-set changes across scene
// transitions and only triggers correlation epochs for non-intro Endminf
// containers. Read-only policy remains strict: no resource is loaded, retained,
// disposed, instantiated, replaced or modified by this layer.

constexpr uint64_t kRendererArrivalScanIntervalMs = 50;
constexpr uint64_t kRendererArrivalHistoryMs = 10000;
constexpr uint64_t kRendererArrivalRepeatCooldownMs = 1000;
constexpr size_t kRendererArrivalMaxHistoryLines = 160;
constexpr char kRendererArrivalIgnoredIntroRoot[] =
    "SceneRoot/SK_actor_female(Clone)";

const BE_HostApiV1* g_renderer_arrival_real_host = nullptr;
BE_HostApiV1 g_renderer_arrival_host_proxy{};
bool g_renderer_arrival_pump_intercepted = false;
uint64_t g_renderer_arrival_next_scan_tick = 0;
uint64_t g_renderer_arrival_last_event_tick = 0;
size_t g_renderer_arrival_event_count = 0;
bool g_renderer_arrival_initial_scan_done = false;
bool g_renderer_arrival_intro_notice_logged = false;
std::vector<std::string> g_renderer_arrival_last_roots;
std::vector<uintptr_t> g_renderer_arrival_last_renderers;

std::string RendererArrivalContainerRoot(const RendererSummary& summary) {
    if (summary.path.empty()) return {};
    const std::string marker = "/Mesh_all/";
    const size_t position = summary.path.find(marker);
    if (position != std::string::npos) {
        return summary.path.substr(0, position);
    }
    const size_t slash = summary.path.rfind('/');
    return slash == std::string::npos ? summary.path
                                      : summary.path.substr(0, slash);
}

bool RendererArrivalIsIgnoredIntro(const RendererSummary& summary) {
    if (summary.path.empty()) return false;
    return summary.path.find(kRendererArrivalIgnoredIntroRoot) !=
        std::string::npos;
}

std::vector<RendererSummary> RendererArrivalGameplaySummaries(
    const std::vector<RendererSummary>& summaries, size_t& ignored_intro_count) {
    std::vector<RendererSummary> output;
    ignored_intro_count = 0;
    output.reserve(summaries.size());
    for (const RendererSummary& summary : summaries) {
        if (RendererArrivalIsIgnoredIntro(summary)) {
            ++ignored_intro_count;
            continue;
        }
        output.push_back(summary);
    }
    return output;
}

std::vector<std::string> RendererArrivalRoots(
    const std::vector<RendererSummary>& summaries) {
    std::vector<std::string> roots;
    for (const RendererSummary& summary : summaries) {
        std::string root = RendererArrivalContainerRoot(summary);
        if (root.empty()) root = "<unknown-root>";
        if (std::find(roots.begin(), roots.end(), root) == roots.end()) {
            roots.push_back(std::move(root));
        }
    }
    std::sort(roots.begin(), roots.end());
    return roots;
}

std::vector<uintptr_t> RendererArrivalRendererIds(
    const std::vector<RendererSummary>& summaries) {
    std::vector<uintptr_t> ids;
    ids.reserve(summaries.size());
    for (const RendererSummary& summary : summaries) {
        if (!summary.renderer) continue;
        ids.push_back(reinterpret_cast<uintptr_t>(summary.renderer));
    }
    std::sort(ids.begin(), ids.end());
    ids.erase(std::unique(ids.begin(), ids.end()), ids.end());
    return ids;
}

std::string RendererArrivalJoinRoots(const std::vector<std::string>& roots) {
    if (roots.empty()) return "[]";
    std::string text = "[";
    for (size_t index = 0; index < roots.size(); ++index) {
        if (index) text += ", ";
        text += roots[index];
    }
    text += "]";
    return text;
}

void RendererArrivalDumpLoadHistory(uint64_t trigger_tick, size_t event_index) {
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

    Log("[RENDERER-ARRIVAL] EVENT #" + std::to_string(event_index) +
        " I18N ring: total=" + std::to_string(ring_count) + " recent=" +
        std::to_string(events.size()) + " targetLoads=" +
        std::to_string(g_natural_trace_target_load_count.load()) +
        " targetClones=" +
        std::to_string(g_natural_trace_target_clone_count.load()));

    if (events.empty()) {
        Log("[RENDERER-ARRIVAL] EVENT #" + std::to_string(event_index) +
            " no I18NAssetLoader.Load call was observed in the preceding 10 seconds. The character assembly path is below or beside this high-level loader hook.");
        return;
    }

    if (events.size() > kRendererArrivalMaxHistoryLines) {
        events.erase(events.begin(),
            events.end() - static_cast<std::ptrdiff_t>(
                kRendererArrivalMaxHistoryLines));
        Log("[RENDERER-ARRIVAL] EVENT #" + std::to_string(event_index) +
            " history truncated to newest " +
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
        Log("[RENDERER-ARRIVAL] EVENT #" + std::to_string(event_index) +
            " I18N-HISTORY " + line);
    }
}

void RendererArrivalTriggerEvent(uint64_t now,
    const std::vector<RendererSummary>& summaries,
    const std::vector<std::string>& roots, const char* reason) {
    ++g_renderer_arrival_event_count;
    g_renderer_arrival_last_event_tick = now;

    Log("[RENDERER-ARRIVAL] EVENT #" +
        std::to_string(g_renderer_arrival_event_count) + " reason=" +
        std::string(reason ? reason : "renderer-set-change") + " renderers=" +
        std::to_string(summaries.size()) + " roots=" +
        RendererArrivalJoinRoots(roots));

    RendererArrivalDumpLoadHistory(now, g_renderer_arrival_event_count);
    NaturalTraceTrigger(now, "gameplay Endminf renderer container/set changed");
    NaturalTraceLogLiveSnapshot(
        ("EVENT-" + std::to_string(g_renderer_arrival_event_count)).c_str());
}

void PumpRendererArrivalTrace() {
    const uint64_t now = GetTickCount64();
    if (now < g_renderer_arrival_next_scan_tick) return;
    g_renderer_arrival_next_scan_tick = now + kRendererArrivalScanIntervalMs;

    const std::vector<RendererSummary> all_summaries =
        EnumerateLiveEndminfRenderers();
    size_t ignored_intro_count = 0;
    const std::vector<RendererSummary> summaries =
        RendererArrivalGameplaySummaries(all_summaries, ignored_intro_count);

    if (ignored_intro_count > 0 && !g_renderer_arrival_intro_notice_logged) {
        g_renderer_arrival_intro_notice_logged = true;
        Log("[RENDERER-ARRIVAL] ignoring splash-only SceneRoot/SK_actor_female(Clone) as a trigger; low-level load tracing remains active so shared Endministrator resources are still observable.");
    }

    const std::vector<std::string> roots = RendererArrivalRoots(summaries);
    const std::vector<uintptr_t> renderer_ids =
        RendererArrivalRendererIds(summaries);

    if (!g_renderer_arrival_initial_scan_done) {
        g_renderer_arrival_initial_scan_done = true;
        g_renderer_arrival_last_roots = roots;
        g_renderer_arrival_last_renderers = renderer_ids;
        Log("[RENDERER-ARRIVAL] gameplay baseline Endminf renderers=" +
            std::to_string(summaries.size()) + " roots=" +
            RendererArrivalJoinRoots(roots) + " ignoredIntro=" +
            std::to_string(ignored_intro_count));
        if (!summaries.empty()) {
            RendererArrivalTriggerEvent(
                now, summaries, roots, "already-live-at-first-scan");
        }
        return;
    }

    const bool roots_changed = roots != g_renderer_arrival_last_roots;
    const bool renderer_set_changed =
        renderer_ids != g_renderer_arrival_last_renderers;
    if (!roots_changed && !renderer_set_changed) return;

    const bool was_empty = g_renderer_arrival_last_renderers.empty();
    const bool is_empty = renderer_ids.empty();
    const std::vector<std::string> previous_roots = g_renderer_arrival_last_roots;
    g_renderer_arrival_last_roots = roots;
    g_renderer_arrival_last_renderers = renderer_ids;

    Log("[RENDERER-ARRIVAL] gameplay renderer set changed oldRoots=" +
        RendererArrivalJoinRoots(previous_roots) + " newRoots=" +
        RendererArrivalJoinRoots(roots) + " newRendererCount=" +
        std::to_string(summaries.size()) + " ignoredIntro=" +
        std::to_string(ignored_intro_count));

    if (is_empty) {
        Log("[RENDERER-ARRIVAL] gameplay Endminf renderer set departed; tracer remains armed for the next scene/container.");
        return;
    }

    // A new container is always interesting. A pointer-only rebuild in the same
    // container is also useful, but suppress rapid assembly churn for one second.
    if (!roots_changed && !was_empty &&
        g_renderer_arrival_last_event_tick != 0 &&
        now - g_renderer_arrival_last_event_tick <
            kRendererArrivalRepeatCooldownMs) {
        return;
    }

    RendererArrivalTriggerEvent(now, summaries, roots,
        was_empty ? "0-to-live" :
        (roots_changed ? "container-changed" : "renderer-set-rebuilt"));
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
    g_renderer_arrival_last_event_tick = 0;
    g_renderer_arrival_event_count = 0;
    g_renderer_arrival_initial_scan_done = false;
    g_renderer_arrival_intro_notice_logged = false;
    g_renderer_arrival_last_roots.clear();
    g_renderer_arrival_last_renderers.clear();

    const BE_Result result =
        InitializeWithNaturalResourceTrace(&g_renderer_arrival_host_proxy);
    if (result != BE_Result_Ok) return result;
    if (!g_renderer_arrival_pump_intercepted) {
        Log("[RENDERER-ARRIVAL] failed to intercept natural-trace main-thread pump.");
        return BE_Result_Failed;
    }

    Log("[RENDERER-ARRIVAL] gameplay trace armed: splash SK_actor_female is ignored as an event source; every later non-intro Endminf renderer container will dump the preceding 10 seconds of naturally observed resource requests.");
    Log("[RENDERER-ARRIVAL] low-level observation still records intro loads because they may reveal resource reuse, but intro evidence is not treated as the global character seam.");
    Log("[RENDERER-ARRIVAL] read-only: no Load/Get/Dispose/Instantiate/replacement calls are issued by this layer.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithRendererArrivalTrace() {
    ShutdownWithNaturalResourceTrace();
    g_renderer_arrival_next_scan_tick = 0;
    g_renderer_arrival_last_event_tick = 0;
    g_renderer_arrival_event_count = 0;
    g_renderer_arrival_initial_scan_done = false;
    g_renderer_arrival_intro_notice_logged = false;
    g_renderer_arrival_last_roots.clear();
    g_renderer_arrival_last_renderers.clear();
    g_renderer_arrival_pump_intercepted = false;
    g_renderer_arrival_real_host = nullptr;
    g_renderer_arrival_host_proxy = {};
}
