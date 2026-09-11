// Natural-load tracer for discovering the real global character-model seam.
//
// This diagnostic never calls I18NAssetLoader.Load on its own and never owns or
// disposes a game FAssetProxyHandle.  It observes the calls the game already
// makes, then correlates them with Unity instantiation and the live Endminf
// SkinnedMeshRenderer set.  The goal is to answer:
//
//   PrefabInfo/logical resource -> postmodel asset -> instantiated wrapper ->
//   runtime-assembled SkinnedMeshRenderers
//
// before choosing the final BetterEndfield.CustomModel replacement hook.

constexpr uint64_t kNaturalTraceEndminfPostmodelHash =
    0x0C9AC03D143EA082ULL;
constexpr char kNaturalTraceEndminfPostmodelName[] =
    "chr_0003_endminf_postmodel";
constexpr char kNaturalTraceEndminfPostmodelPath[] =
    "assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/"
    "chr_0003_endminf_postmodel.prefab";
constexpr size_t kNaturalTraceLoadRingSize = 512;
constexpr uint64_t kNaturalTraceRecentBeforeMs = 1500;
constexpr uint64_t kNaturalTraceRecentAfterMs = 3000;
constexpr uint64_t kNaturalTraceSnapshot1Ms = 150;
constexpr uint64_t kNaturalTraceSnapshot2Ms = 750;
constexpr uint64_t kNaturalTraceSnapshot3Ms = 2500;

const BE_HostApiV1* g_natural_trace_real_host = nullptr;
BE_HostApiV1 g_natural_trace_host_proxy{};
bool g_natural_trace_pump_intercepted = false;

BE_ResolvedMethodV1 g_natural_trace_i18n_load{};
BE_ResolvedMethodV1 g_natural_trace_hash_get_string{};
BE_ResolvedMethodV1 g_natural_trace_clone_with_parent{};
BE_ResolvedMethodV1 g_natural_trace_get_components{};
BE_ResolvedMethodV1 g_natural_trace_object_get_type{};
BE_ResolvedMethodV1 g_natural_trace_runtime_type_full_name{};
BE_ResolvedClassV1 g_natural_trace_game_object_class{};
BE_ResolvedClassV1 g_natural_trace_component_class{};

using NaturalTraceI18NLoadFn = void*(__fastcall*)(
    void* result, uint64_t path_hash, void* type_object, uint8_t category,
    void* method_info);
using NaturalTraceCloneWithParentFn = void*(__fastcall*)(
    void* original, void* parent, bool world_position_stays, void* method_info);

NaturalTraceI18NLoadFn g_natural_trace_original_i18n_load = nullptr;
NaturalTraceCloneWithParentFn g_natural_trace_original_clone = nullptr;

struct NaturalTraceLoadEvent {
    uint64_t tick = 0;
    uint64_t path_hash = 0;
    void* type_object = nullptr;
    uint32_t thread_id = 0;
    uint8_t category = 0;
};

std::array<NaturalTraceLoadEvent, kNaturalTraceLoadRingSize>
    g_natural_trace_load_ring{};
size_t g_natural_trace_load_write = 0;
size_t g_natural_trace_load_count = 0;
std::mutex g_natural_trace_load_mutex;

std::atomic_uint64_t g_natural_trace_epoch{0};
std::atomic_uint64_t g_natural_trace_trigger_tick{0};
std::atomic_uint64_t g_natural_trace_target_load_count{0};
std::atomic_uint64_t g_natural_trace_target_clone_count{0};
uint64_t g_natural_trace_pump_epoch = 0;
bool g_natural_trace_snapshot1_done = false;
bool g_natural_trace_snapshot2_done = false;
bool g_natural_trace_snapshot3_done = false;
bool g_natural_trace_recent_dump_done = false;

std::string NaturalTracePointer(const void* pointer) {
    std::array<char, 32> buffer{};
    std::snprintf(buffer.data(), buffer.size(), "%p", pointer);
    return std::string(buffer.data());
}

std::string NaturalTraceHash(uint64_t value) {
    std::array<char, 32> buffer{};
    std::snprintf(buffer.data(), buffer.size(), "0x%016llX",
        static_cast<unsigned long long>(value));
    return std::string(buffer.data());
}

std::string NaturalTraceManagedString(void* value) {
    if (!value || !g_host || !g_host->copy_managed_string) return {};
    std::array<char, 768> buffer{};
    const int copied = g_host->copy_managed_string(
        g_host->context, value, buffer.data(), buffer.size());
    return copied > 0 ? std::string(buffer.data()) : std::string{};
}

bool NaturalTraceResolveMethod(BE_ResolvedMethodV1& output,
    const char* assembly, const char* name_space, const char* class_name,
    const char* method_name, const char* parameters, const char* return_type,
    uint32_t parameter_count, const char* label, bool required = true) {
    output = {};
    if (!g_natural_trace_real_host ||
        !g_natural_trace_real_host->resolve_method) {
        return false;
    }
    const BE_MethodDescriptorV1 descriptor{assembly, name_space, class_name,
        method_name, parameters, return_type, parameter_count};
    const BE_Result result = g_natural_trace_real_host->resolve_method(
        g_natural_trace_real_host->context, &descriptor, &output);
    if (result != BE_Result_Ok || !output.method_info || !output.method_pointer) {
        Log(std::string("[NATURAL-TRACE] ") +
            (required ? "required" : "optional") +
            " method unresolved: " + label);
        output = {};
        return false;
    }
    Log(std::string("[NATURAL-TRACE] resolved ") + label + " method=" +
        NaturalTracePointer(output.method_pointer));
    return true;
}

bool NaturalTraceResolveClass(BE_ResolvedClassV1& output,
    const char* assembly, const char* name_space, const char* class_name,
    const char* label) {
    output = {};
    if (!g_natural_trace_real_host ||
        !g_natural_trace_real_host->resolve_class) {
        return false;
    }
    const BE_Result result = g_natural_trace_real_host->resolve_class(
        g_natural_trace_real_host->context, assembly, name_space, class_name,
        &output);
    if (result != BE_Result_Ok || !output.type_object) {
        Log(std::string("[NATURAL-TRACE] class unresolved: ") + label);
        output = {};
        return false;
    }
    return true;
}

std::string NaturalTraceTypeName(void* type_object) {
    if (!type_object || !g_natural_trace_runtime_type_full_name.method_info ||
        !g_host || !g_host->runtime_invoke) {
        return {};
    }
    void* exception = nullptr;
    void* value = g_host->runtime_invoke(g_host->context,
        g_natural_trace_runtime_type_full_name.method_info, type_object, nullptr,
        &exception);
    return exception ? std::string{} : NaturalTraceManagedString(value);
}

std::string NaturalTraceObjectTypeName(void* object) {
    if (!object || !g_natural_trace_object_get_type.method_info || !g_host ||
        !g_host->runtime_invoke) {
        return {};
    }
    void* exception = nullptr;
    void* type = g_host->runtime_invoke(g_host->context,
        g_natural_trace_object_get_type.method_info, object, nullptr, &exception);
    return exception ? std::string{} : NaturalTraceTypeName(type);
}

std::string NaturalTracePathForHash(uint64_t path_hash) {
    if (path_hash == kNaturalTraceEndminfPostmodelHash) {
        return kNaturalTraceEndminfPostmodelPath;
    }
    if (!g_natural_trace_hash_get_string.method_info || !g_host ||
        !g_host->runtime_invoke) {
        return {};
    }
    void* parameters[1]{&path_hash};
    void* exception = nullptr;
    void* value = g_host->runtime_invoke(g_host->context,
        g_natural_trace_hash_get_string.method_info, nullptr, parameters,
        &exception);
    return exception ? std::string{} : NaturalTraceManagedString(value);
}

void NaturalTraceRecordLoad(
    uint64_t tick, uint64_t path_hash, void* type_object, uint8_t category) {
    NaturalTraceLoadEvent event;
    event.tick = tick;
    event.path_hash = path_hash;
    event.type_object = type_object;
    event.category = category;
#if defined(_WIN32)
    event.thread_id = static_cast<uint32_t>(GetCurrentThreadId());
#endif
    std::lock_guard<std::mutex> lock(g_natural_trace_load_mutex);
    g_natural_trace_load_ring[g_natural_trace_load_write] = event;
    g_natural_trace_load_write =
        (g_natural_trace_load_write + 1) % kNaturalTraceLoadRingSize;
    g_natural_trace_load_count = std::min(
        g_natural_trace_load_count + 1, kNaturalTraceLoadRingSize);
}

void NaturalTraceTrigger(uint64_t tick, const char* source) {
    g_natural_trace_trigger_tick.store(tick, std::memory_order_release);
    const uint64_t epoch =
        g_natural_trace_epoch.fetch_add(1, std::memory_order_acq_rel) + 1;
    Log("[NATURAL-TRACE] correlation window armed epoch=" +
        std::to_string(epoch) + " source=" +
        std::string(source ? source : "unknown"));
}

void* __fastcall NaturalTraceI18NLoadHook(
    void* result, uint64_t path_hash, void* type_object, uint8_t category,
    void* method_info) {
    const uint64_t now = GetTickCount64();
    NaturalTraceRecordLoad(now, path_hash, type_object, category);

    if (path_hash == kNaturalTraceEndminfPostmodelHash) {
        g_natural_trace_target_load_count.fetch_add(1,
            std::memory_order_acq_rel);
        NaturalTraceTrigger(now, "game I18NAssetLoader.Load(postmodel)");
    }

    if (!g_natural_trace_original_i18n_load) return result;
    return g_natural_trace_original_i18n_load(
        result, path_hash, type_object, category, method_info);
}

std::vector<std::string> NaturalTraceComponentTypes(void* game_object) {
    std::vector<std::string> output;
    if (!game_object || !g_natural_trace_component_class.type_object ||
        !g_natural_trace_get_components.method_info || !g_host ||
        !g_host->runtime_invoke) {
        return output;
    }
    bool include_inactive = true;
    void* parameters[2]{g_natural_trace_component_class.type_object,
        &include_inactive};
    void* exception = nullptr;
    void* components = g_host->runtime_invoke(g_host->context,
        g_natural_trace_get_components.method_info, game_object, parameters,
        &exception);
    if (exception || !components) return output;

    const int count = std::max(ArrayLength(components), 0);
    for (int index = 0; index < count; ++index) {
        void* component = ArrayValue(components, index);
        if (!component) continue;
        std::string type = NaturalTraceObjectTypeName(component);
        if (type.empty()) type = "<unknown>";
        if (std::find(output.begin(), output.end(), type) == output.end()) {
            output.push_back(std::move(type));
        }
    }
    std::sort(output.begin(), output.end());
    return output;
}

std::vector<RendererSummary> NaturalTraceChildRenderers(void* game_object) {
    std::vector<RendererSummary> output;
    if (!game_object || !g_skinned_renderer_class.type_object ||
        !g_natural_trace_get_components.method_info || !g_host ||
        !g_host->runtime_invoke) {
        return output;
    }
    bool include_inactive = true;
    void* parameters[2]{g_skinned_renderer_class.type_object,
        &include_inactive};
    void* exception = nullptr;
    void* components = g_host->runtime_invoke(g_host->context,
        g_natural_trace_get_components.method_info, game_object, parameters,
        &exception);
    if (exception || !components) return output;

    const int count = std::max(ArrayLength(components), 0);
    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(components, index);
        if (!renderer || !IsNativeObjectAlive(renderer)) continue;
        RendererSummary summary = SummarizeRenderer(renderer);
        if (summary.renderer) output.push_back(std::move(summary));
    }
    return output;
}

void NaturalTraceInspectClone(void* original, void* clone) {
    const auto types = NaturalTraceComponentTypes(clone);
    const auto renderers = NaturalTraceChildRenderers(clone);
    Log("[NATURAL-TRACE] POSTMODEL-CLONE original=" +
        NaturalTracePointer(original) + " clone=" + NaturalTracePointer(clone) +
        " originalName=\"" + ObjectName(original) + "\" cloneName=\"" +
        ObjectName(clone) + "\" componentTypes=" +
        std::to_string(types.size()) + " childSMR=" +
        std::to_string(renderers.size()));
    for (const std::string& type : types) {
        Log("[NATURAL-TRACE] POSTMODEL-COMPONENT-TYPE " + type);
    }
    for (size_t index = 0; index < renderers.size(); ++index) {
        const RendererSummary& item = renderers[index];
        Log("[NATURAL-TRACE] POSTMODEL-SMR #" + std::to_string(index) +
            " renderer=" + NaturalTracePointer(item.renderer) + " name=\"" +
            item.renderer_name + "\" mesh=" + NaturalTracePointer(item.mesh) +
            " meshName=\"" + item.mesh_name + "\" idx=" +
            std::to_string(item.total_indices) + " bones=" +
            std::to_string(item.bones_count) + " path=\"" + item.path + "\"");
    }
}

void* __fastcall NaturalTraceCloneWithParentHook(
    void* original, void* parent, bool world_position_stays, void* method_info) {
    if (!g_natural_trace_original_clone) return nullptr;
    void* clone = g_natural_trace_original_clone(
        original, parent, world_position_stays, method_info);

    if (!original) return clone;
    const std::string original_name = ObjectName(original);
    if (original_name.find("_postmodel") == std::string::npos) return clone;

    Log("[NATURAL-TRACE] UNITY-CLONE postmodel originalName=\"" +
        original_name + "\" original=" + NaturalTracePointer(original) +
        " clone=" + NaturalTracePointer(clone) + " parent=" +
        NaturalTracePointer(parent));

    if (original_name.find(kNaturalTraceEndminfPostmodelName) !=
        std::string::npos) {
        g_natural_trace_target_clone_count.fetch_add(1,
            std::memory_order_acq_rel);
        NaturalTraceTrigger(GetTickCount64(),
            "Unity Internal_CloneSingleWithParent(endminf postmodel)");
        if (clone && IsNativeObjectAlive(clone)) {
            NaturalTraceInspectClone(original, clone);
        }
    }
    return clone;
}

void NaturalTraceLogLiveSnapshot(const char* stage) {
    const std::vector<RendererSummary> summaries =
        EnumerateLiveEndminfRenderers();
    Log(std::string("[NATURAL-TRACE] LIVE-") + stage +
        " renderers=" + std::to_string(summaries.size()));
    for (size_t index = 0; index < summaries.size(); ++index) {
        const RendererSummary& item = summaries[index];
        Log(std::string("[NATURAL-TRACE] LIVE-") + stage + " #" +
            std::to_string(index) + " renderer=" +
            NaturalTracePointer(item.renderer) + " name=\"" +
            item.renderer_name + "\" mesh=" + NaturalTracePointer(item.mesh) +
            " meshName=\"" + item.mesh_name + "\" idx=" +
            std::to_string(item.total_indices) + " bones=" +
            std::to_string(item.bones_count) + " path=\"" + item.path + "\"");
    }
}

void NaturalTraceDumpRecentLoads(uint64_t trigger_tick) {
    std::vector<NaturalTraceLoadEvent> events;
    {
        std::lock_guard<std::mutex> lock(g_natural_trace_load_mutex);
        events.reserve(g_natural_trace_load_count);
        const size_t start =
            (g_natural_trace_load_write + kNaturalTraceLoadRingSize -
                g_natural_trace_load_count) % kNaturalTraceLoadRingSize;
        for (size_t offset = 0; offset < g_natural_trace_load_count; ++offset) {
            const NaturalTraceLoadEvent& event =
                g_natural_trace_load_ring[
                    (start + offset) % kNaturalTraceLoadRingSize];
            if (event.tick + kNaturalTraceRecentBeforeMs < trigger_tick ||
                event.tick > trigger_tick + kNaturalTraceRecentAfterMs) {
                continue;
            }
            events.push_back(event);
        }
    }

    std::vector<std::string> lines;
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
        if (std::find(lines.begin(), lines.end(), line) == lines.end()) {
            lines.push_back(std::move(line));
        }
    }

    Log("[NATURAL-TRACE] LOAD-WINDOW entries=" +
        std::to_string(lines.size()) + " range=-" +
        std::to_string(kNaturalTraceRecentBeforeMs) + "/+" +
        std::to_string(kNaturalTraceRecentAfterMs) + "ms");
    for (const std::string& line : lines) {
        Log("[NATURAL-TRACE] LOAD " + line);
    }
}

void PumpNaturalResourceTrace() {
    const uint64_t epoch = g_natural_trace_epoch.load(std::memory_order_acquire);
    if (epoch == 0) return;
    if (epoch != g_natural_trace_pump_epoch) {
        g_natural_trace_pump_epoch = epoch;
        g_natural_trace_snapshot1_done = false;
        g_natural_trace_snapshot2_done = false;
        g_natural_trace_snapshot3_done = false;
        g_natural_trace_recent_dump_done = false;
        Log("[NATURAL-TRACE] observing epoch=" + std::to_string(epoch) +
            " targetLoads=" +
            std::to_string(g_natural_trace_target_load_count.load()) +
            " targetClones=" +
            std::to_string(g_natural_trace_target_clone_count.load()));
    }

    const uint64_t trigger_tick =
        g_natural_trace_trigger_tick.load(std::memory_order_acquire);
    if (!trigger_tick) return;
    const uint64_t now = GetTickCount64();
    const uint64_t elapsed = now >= trigger_tick ? now - trigger_tick : 0;

    if (!g_natural_trace_snapshot1_done && elapsed >= kNaturalTraceSnapshot1Ms) {
        g_natural_trace_snapshot1_done = true;
        NaturalTraceLogLiveSnapshot("+150MS");
    }
    if (!g_natural_trace_snapshot2_done && elapsed >= kNaturalTraceSnapshot2Ms) {
        g_natural_trace_snapshot2_done = true;
        NaturalTraceLogLiveSnapshot("+750MS");
    }
    if (!g_natural_trace_snapshot3_done && elapsed >= kNaturalTraceSnapshot3Ms) {
        g_natural_trace_snapshot3_done = true;
        NaturalTraceLogLiveSnapshot("+2500MS");
    }
    if (!g_natural_trace_recent_dump_done &&
        elapsed >= kNaturalTraceRecentAfterMs) {
        g_natural_trace_recent_dump_done = true;
        NaturalTraceDumpRecentLoads(trigger_tick);
        Log("[NATURAL-TRACE] epoch complete. No game resource was acquired, retained, released or replaced by this tracer.");
    }
}

void __fastcall DetourPumpWithNaturalResourceTrace(void* method_info) {
    DetourPumpWithAiShadowCoverage(method_info);
    PumpNaturalResourceTrace();
}

BE_Result BE_CALL NaturalResourceTraceCreateHook(
    void* context, const char* module_id, void* target, void* detour,
    void** original) {
    if (!g_natural_trace_real_host || !g_natural_trace_real_host->create_hook)
        return BE_Result_NotReady;
    if (detour == reinterpret_cast<void*>(&DetourPumpWithAiShadowCoverage)) {
        g_natural_trace_pump_intercepted = true;
        return g_natural_trace_real_host->create_hook(context, module_id, target,
            reinterpret_cast<void*>(&DetourPumpWithNaturalResourceTrace),
            original);
    }
    return g_natural_trace_real_host->create_hook(
        context, module_id, target, detour, original);
}

bool ResolveNaturalResourceTraceContracts() {
    bool required = true;
    required &= NaturalTraceResolveClass(g_natural_trace_game_object_class,
        "UnityEngine.CoreModule.dll", "UnityEngine", "GameObject",
        "UnityEngine.GameObject");
    required &= NaturalTraceResolveClass(g_natural_trace_component_class,
        "UnityEngine.CoreModule.dll", "UnityEngine", "Component",
        "UnityEngine.Component");

    required &= NaturalTraceResolveMethod(g_natural_trace_i18n_load,
        "Common.Beyond.dll", "Beyond.Resource", "I18NAssetLoader", "Load",
        "Beyond.Resource.StringPathHash|System.Type|Beyond.Resource.RootCategory",
        "Beyond.Resource.FAssetProxyHandle", 3, "I18NAssetLoader.Load");
    required &= NaturalTraceResolveMethod(g_natural_trace_clone_with_parent,
        "UnityEngine.CoreModule.dll", "UnityEngine", "Object",
        "Internal_CloneSingleWithParent",
        "UnityEngine.Object|UnityEngine.Transform|System.Boolean",
        "UnityEngine.Object", 3, "Object.Internal_CloneSingleWithParent");
    required &= NaturalTraceResolveMethod(g_natural_trace_get_components,
        "UnityEngine.CoreModule.dll", "UnityEngine", "GameObject",
        "GetComponentsInChildren", "System.Type|System.Boolean",
        "UnityEngine.Component[]", 2,
        "GameObject.GetComponentsInChildren");
    required &= NaturalTraceResolveMethod(g_natural_trace_object_get_type,
        "mscorlib.dll", "System", "Object", "GetType", nullptr,
        "System.Type", 0, "System.Object.GetType");
    required &= NaturalTraceResolveMethod(g_natural_trace_runtime_type_full_name,
        "mscorlib.dll", "System", "RuntimeType", "get_FullName", nullptr,
        "System.String", 0, "System.RuntimeType.get_FullName");

    NaturalTraceResolveMethod(g_natural_trace_hash_get_string,
        "Common.Beyond.dll", "Beyond.Resource", "HashStringPathProcessor",
        "GetString", "Beyond.Resource.StringPathHash", "System.String", 1,
        "HashStringPathProcessor.GetString", false);
    return required;
}

bool InstallNaturalResourceTraceHooks() {
    if (!g_natural_trace_real_host || !g_natural_trace_real_host->create_hook ||
        !g_natural_trace_i18n_load.method_pointer ||
        !g_natural_trace_clone_with_parent.method_pointer) {
        return false;
    }

    BE_Result result = g_natural_trace_real_host->create_hook(
        g_natural_trace_real_host->context, kModuleId,
        g_natural_trace_i18n_load.method_pointer,
        reinterpret_cast<void*>(&NaturalTraceI18NLoadHook),
        reinterpret_cast<void**>(&g_natural_trace_original_i18n_load));
    if (result != BE_Result_Ok || !g_natural_trace_original_i18n_load) {
        Log("[NATURAL-TRACE] failed to hook I18NAssetLoader.Load");
        return false;
    }

    result = g_natural_trace_real_host->create_hook(
        g_natural_trace_real_host->context, kModuleId,
        g_natural_trace_clone_with_parent.method_pointer,
        reinterpret_cast<void*>(&NaturalTraceCloneWithParentHook),
        reinterpret_cast<void**>(&g_natural_trace_original_clone));
    if (result != BE_Result_Ok || !g_natural_trace_original_clone) {
        Log("[NATURAL-TRACE] failed to hook Object.Internal_CloneSingleWithParent");
        return false;
    }

    Log("[NATURAL-TRACE] game-owned resource flow hooks installed. No synthetic Load call will be issued.");
    return true;
}

BE_Result BE_CALL InitializeWithNaturalResourceTrace(const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;

    g_natural_trace_real_host = host;
    g_natural_trace_host_proxy = *host;
    g_natural_trace_host_proxy.create_hook = &NaturalResourceTraceCreateHook;
    g_natural_trace_pump_intercepted = false;
    g_natural_trace_epoch.store(0, std::memory_order_release);
    g_natural_trace_trigger_tick.store(0, std::memory_order_release);
    g_natural_trace_target_load_count.store(0, std::memory_order_release);
    g_natural_trace_target_clone_count.store(0, std::memory_order_release);
    g_natural_trace_pump_epoch = 0;
    g_natural_trace_snapshot1_done = false;
    g_natural_trace_snapshot2_done = false;
    g_natural_trace_snapshot3_done = false;
    g_natural_trace_recent_dump_done = false;
    {
        std::lock_guard<std::mutex> lock(g_natural_trace_load_mutex);
        g_natural_trace_load_ring = {};
        g_natural_trace_load_write = 0;
        g_natural_trace_load_count = 0;
    }

    const BE_Result base =
        InitializeWithAiShadowCoverage(&g_natural_trace_host_proxy);
    if (base != BE_Result_Ok) return base;
    if (!g_natural_trace_pump_intercepted) {
        Log("[NATURAL-TRACE] failed to intercept the CustomModel main-thread pump.");
        return BE_Result_Failed;
    }
    if (!ResolveNaturalResourceTraceContracts() ||
        !InstallNaturalResourceTraceHooks()) {
        Log("[NATURAL-TRACE] natural resource tracer contract is incomplete.");
        return BE_Result_Failed;
    }

    Log(std::string("[NATURAL-TRACE] armed for game-owned Endminf flow: hash=") +
        NaturalTraceHash(kNaturalTraceEndminfPostmodelHash) + " path=\"" +
        kNaturalTraceEndminfPostmodelPath + "\"");
    Log("[NATURAL-TRACE] scope: observe I18N loads, postmodel clone, component types and live renderer assembly. F12/F11 remain manual and are not invoked automatically.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithNaturalResourceTrace() {
    ShutdownWithAiShadowCoverage();
    g_natural_trace_epoch.store(0, std::memory_order_release);
    g_natural_trace_trigger_tick.store(0, std::memory_order_release);
    g_natural_trace_pump_intercepted = false;
    // The host owns hook lifetime. Keep trampoline pointers intact until process
    // teardown so an in-flight game call can always reach its original target.
}
