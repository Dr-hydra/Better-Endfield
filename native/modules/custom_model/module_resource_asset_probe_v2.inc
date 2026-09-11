// One-shot startup resource probe for the global replacement design.
//
// Safety rules for this revision:
//   * wait until ResourceManager is initialized and no longer async-initializing;
//   * wait a short stabilization window after that point;
//   * call I18NAssetLoader.Load exactly once for the whole process;
//   * immediately root the boxed FAssetProxyHandle before unboxing it;
//   * call FAssetProxyHandle.Get exactly once;
//   * root the returned UnityEngine.Object as well;
//   * never run a cache recheck, never retry a failed load, and never free either
//     root while the process is alive.
//
// The previous probe rooted only the returned prefab.  Because
// FAssetProxyHandle is a value type whose boxed lifetime may participate in the
// resource proxy's ownership, allowing that temporary box to die was not a
// valid read-only experiment.  This probe intentionally leaks two GC roots for
// the remainder of the process so the resource system never observes us
// releasing anything it handed out.

constexpr uint64_t kResourceProbeEndminfPostmodelHash = 0x0C9AC03D143EA082ULL;
constexpr uint8_t kResourceProbeMainCategory = 0;
constexpr uint64_t kResourceProbeReadyStabilizeMs = 3000;
constexpr char kResourceProbeEndminfPostmodelPath[] =
    "assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/"
    "chr_0003_endminf_postmodel.prefab";

const BE_HostApiV1* g_resource_probe_real_host = nullptr;
BE_HostApiV1 g_resource_probe_host_proxy{};
bool g_resource_probe_pump_intercepted = false;

BE_ResolvedMethodV1 g_resource_probe_i18n_load{};
BE_ResolvedMethodV1 g_resource_probe_handle_get{};
BE_ResolvedMethodV1 g_resource_probe_initialized{};
BE_ResolvedMethodV1 g_resource_probe_async_initing{};
BE_ResolvedClassV1 g_resource_probe_game_object_class{};

// These are deliberately process-lifetime roots.  Do not free them from module
// shutdown: unloading the probe must not look like a resource release to the
// game.  A process restart is the cleanup boundary for this diagnostic build.
void* g_resource_probe_boxed_handle = nullptr;
uint32_t g_resource_probe_boxed_handle_root = 0;
void* g_resource_probe_prefab = nullptr;
uint32_t g_resource_probe_prefab_root = 0;

uint64_t g_resource_probe_ready_since = 0;
bool g_resource_probe_contract_ready = false;
bool g_resource_probe_attempted = false;
bool g_resource_probe_done = false;

std::string ResourceProbePointer(const void* pointer) {
    std::array<char, 32> buffer{};
    std::snprintf(buffer.data(), buffer.size(), "%p", pointer);
    return std::string(buffer.data());
}

std::string ResourceProbeHashText(uint64_t value) {
    std::array<char, 32> buffer{};
    std::snprintf(buffer.data(), buffer.size(), "0x%016llX",
        static_cast<unsigned long long>(value));
    return std::string(buffer.data());
}

bool ResourceProbeResolve(BE_ResolvedMethodV1& output,
    const char* assembly, const char* name_space, const char* class_name,
    const char* method_name, const char* parameters, const char* return_type,
    uint32_t parameter_count, const char* label) {
    output = {};
    if (!g_host || !g_host->resolve_method) return false;
    const BE_MethodDescriptorV1 descriptor{assembly, name_space, class_name,
        method_name, parameters, return_type, parameter_count};
    const BE_Result result = g_host->resolve_method(
        g_host->context, &descriptor, &output);
    if (result != BE_Result_Ok || !output.method_info || !output.method_pointer) {
        Log(std::string("[RESOURCE-PROBE] unresolved ") + label);
        output = {};
        return false;
    }
    Log(std::string("[RESOURCE-PROBE] resolved ") + label + " method=" +
        ResourceProbePointer(output.method_pointer));
    return true;
}

template <typename T>
bool ResourceProbeInvokeValue(const BE_ResolvedMethodV1& method,
    void* instance, void** parameters, T& value) {
    if (!method.method_info || !g_host || !g_host->runtime_invoke ||
        !g_host->object_unbox) {
        return false;
    }
    void* exception = nullptr;
    void* boxed = g_host->runtime_invoke(g_host->context, method.method_info,
        instance, parameters, &exception);
    if (exception || !boxed) return false;
    void* raw = g_host->object_unbox(g_host->context, boxed);
    if (!raw) return false;
    std::memcpy(&value, raw, sizeof(T));
    return true;
}

bool ResourceProbeManagerReady() {
    bool initialized = false;
    bool async_initing = true;
    return ResourceProbeInvokeValue(g_resource_probe_initialized, nullptr,
               nullptr, initialized) &&
        ResourceProbeInvokeValue(g_resource_probe_async_initing, nullptr,
            nullptr, async_initing) &&
        initialized && !async_initing;
}

struct ResourceProbeLiveSnapshot {
    int loaded_endminf_smr = 0;
    int active_endminf_smr = 0;
    std::vector<std::string> samples;
};

ResourceProbeLiveSnapshot ResourceProbeCaptureLiveSnapshot() {
    ResourceProbeLiveSnapshot snapshot;
    if (!g_skinned_renderer_class.type_object) return snapshot;

    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* objects = Invoke(Contract("resources.find_objects_of_type_all"),
        nullptr, parameters, false);
    const int count = std::max(ArrayLength(objects), 0);
    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (!renderer || !IsNativeObjectAlive(renderer)) continue;
        const std::string renderer_name = ObjectName(renderer);
        if (renderer_name.find("endminf") == std::string::npos) continue;

        ++snapshot.loaded_endminf_smr;
        const bool active = IsActiveSceneRenderer(renderer);
        if (active) ++snapshot.active_endminf_smr;

        if (snapshot.samples.size() < 8) {
            void* mesh = Invoke(Contract("skinned.get_shared_mesh"), renderer,
                nullptr, false);
            snapshot.samples.push_back(
                std::string(active ? "active " : "inactive ") +
                "renderer=" + ResourceProbePointer(renderer) +
                " name=\"" + renderer_name + "\" mesh=" +
                ResourceProbePointer(mesh) + " meshName=\"" +
                ObjectName(mesh) + "\"");
        }
    }
    return snapshot;
}

void ResourceProbeLogLiveSnapshot(
    const char* stage, const ResourceProbeLiveSnapshot& snapshot) {
    Log(std::string("[RESOURCE-PROBE] ") + stage +
        " loadedEndminfSMR=" + std::to_string(snapshot.loaded_endminf_smr) +
        " activeEndminfSMR=" + std::to_string(snapshot.active_endminf_smr));
    for (const std::string& sample : snapshot.samples) {
        Log(std::string("[RESOURCE-PROBE] ") + stage + "-SAMPLE " + sample);
    }
}

void ResourceProbeRunOnce() {
    // Set this before invoking managed code so re-entrancy can never issue a
    // second Load call.
    if (g_resource_probe_attempted) return;
    g_resource_probe_attempted = true;

    if (!g_host || !g_host->runtime_invoke || !g_host->object_unbox ||
        !g_host->gchandle_new || !g_resource_probe_game_object_class.type_object) {
        Log("[RESOURCE-PROBE] ONE-SHOT aborted before Load: required rooting/runtime API unavailable; no retry will be attempted.");
        g_resource_probe_done = true;
        return;
    }

    const ResourceProbeLiveSnapshot before = ResourceProbeCaptureLiveSnapshot();
    ResourceProbeLogLiveSnapshot("BEFORE-LOAD", before);

    uint64_t hash = kResourceProbeEndminfPostmodelHash;
    uint8_t category = kResourceProbeMainCategory;
    void* parameters[3]{&hash, g_resource_probe_game_object_class.type_object,
        &category};
    void* exception = nullptr;

    Log("[RESOURCE-PROBE] LOAD begin hash=" +
        ResourceProbeHashText(kResourceProbeEndminfPostmodelHash));
    void* boxed_handle = g_host->runtime_invoke(g_host->context,
        g_resource_probe_i18n_load.method_info, nullptr, parameters, &exception);
    if (exception || !boxed_handle) {
        Log("[RESOURCE-PROBE] LOAD failed; one-shot probe stops here and will not retry.");
        g_resource_probe_done = true;
        return;
    }

    // Root the BOX, not only the asset.  The unboxed value is used only while
    // this strong root is already alive.
    g_resource_probe_boxed_handle = boxed_handle;
    g_resource_probe_boxed_handle_root =
        g_host->gchandle_new(g_host->context, boxed_handle, 0);
    Log("[RESOURCE-PROBE] HOLD boxedHandle=" +
        ResourceProbePointer(boxed_handle) + " gcRoot=" +
        std::to_string(g_resource_probe_boxed_handle_root));
    if (!g_resource_probe_boxed_handle_root) {
        Log("[RESOURCE-PROBE] HOLD failed for boxed FAssetProxyHandle; refusing Get and never retrying Load.");
        g_resource_probe_done = true;
        return;
    }

    void* handle = g_host->object_unbox(g_host->context, boxed_handle);
    if (!handle) {
        Log("[RESOURCE-PROBE] GET aborted: boxed FAssetProxyHandle could not be unboxed.");
        g_resource_probe_done = true;
        return;
    }

    exception = nullptr;
    Log("[RESOURCE-PROBE] GET begin handlePayload=" +
        ResourceProbePointer(handle));
    void* prefab = g_host->runtime_invoke(g_host->context,
        g_resource_probe_handle_get.method_info, handle, nullptr, &exception);
    if (exception || !prefab) {
        Log("[RESOURCE-PROBE] GET failed; boxed handle remains rooted for process lifetime.");
        g_resource_probe_done = true;
        return;
    }

    g_resource_probe_prefab = prefab;
    g_resource_probe_prefab_root =
        g_host->gchandle_new(g_host->context, prefab, 0);
    Log("[RESOURCE-PROBE] HOLD prefab=" + ResourceProbePointer(prefab) +
        " gcRoot=" + std::to_string(g_resource_probe_prefab_root) +
        " alive=" + std::to_string(IsNativeObjectAlive(prefab) ? 1 : 0) +
        " name=\"" + ObjectName(prefab) + "\"");

    // Intentionally do NOT call GetComponentsInChildren on the loaded prefab in
    // this revision.  The previous child enumeration was another independent
    // suspect and is not needed to validate resource lifetime.
    const ResourceProbeLiveSnapshot after = ResourceProbeCaptureLiveSnapshot();
    ResourceProbeLogLiveSnapshot("AFTER-GET", after);
    Log("[RESOURCE-PROBE] OBSERVE deltaLoaded=" +
        std::to_string(after.loaded_endminf_smr - before.loaded_endminf_smr) +
        " deltaActive=" +
        std::to_string(after.active_endminf_smr - before.active_endminf_smr));
    Log("[RESOURCE-PROBE] ONE-SHOT complete: Load=1 Get=1 cacheRecheck=0 childEnumeration=0 releases=0. Roots are retained until process exit.");
    g_resource_probe_done = true;
}

void PumpResourceAssetProbe() {
    if (!g_resource_probe_contract_ready || g_resource_probe_done ||
        g_resource_probe_attempted) {
        return;
    }

    const uint64_t now = GetTickCount64();
    if (!ResourceProbeManagerReady()) {
        g_resource_probe_ready_since = 0;
        return;
    }

    if (!g_resource_probe_ready_since) {
        g_resource_probe_ready_since = now;
        Log("[RESOURCE-PROBE] ResourceManager ready; one-shot probe will run after " +
            std::to_string(kResourceProbeReadyStabilizeMs) +
            " ms stabilization window.");
        return;
    }

    if (now - g_resource_probe_ready_since < kResourceProbeReadyStabilizeMs) {
        return;
    }

    ResourceProbeRunOnce();
}

bool ResolveResourceAssetProbeContracts() {
    if (!g_host || !g_host->resolve_class) return false;
    if (g_host->resolve_class(g_host->context, "UnityEngine.CoreModule.dll",
            "UnityEngine", "GameObject", &g_resource_probe_game_object_class) !=
            BE_Result_Ok || !g_resource_probe_game_object_class.type_object) {
        Log("[RESOURCE-PROBE] unresolved UnityEngine.GameObject class.");
        return false;
    }

    bool ok = true;
    ok &= ResourceProbeResolve(g_resource_probe_i18n_load,
        "Common.Beyond.dll", "Beyond.Resource", "I18NAssetLoader", "Load",
        "Beyond.Resource.StringPathHash|System.Type|Beyond.Resource.RootCategory",
        "Beyond.Resource.FAssetProxyHandle", 3, "I18NAssetLoader.Load");
    ok &= ResourceProbeResolve(g_resource_probe_handle_get,
        "Common.Beyond.dll", "Beyond.Resource", "FAssetProxyHandle", "Get",
        nullptr, "UnityEngine.Object", 0, "FAssetProxyHandle.Get");
    ok &= ResourceProbeResolve(g_resource_probe_initialized,
        "Common.Beyond.dll", "Beyond.Resource", "ResourceManager",
        "IsInitialized", nullptr, "System.Boolean", 0,
        "ResourceManager.IsInitialized");
    ok &= ResourceProbeResolve(g_resource_probe_async_initing,
        "Common.Beyond.dll", "Beyond.Resource", "ResourceManager",
        "IsAsyncIniting", nullptr, "System.Boolean", 0,
        "ResourceManager.IsAsyncIniting");
    return ok;
}

void __fastcall DetourPumpWithResourceAssetProbe(void* method_info) {
    DetourPumpWithAiShadowCoverage(method_info);
    PumpResourceAssetProbe();
}

BE_Result BE_CALL ResourceAssetProbeCreateHook(
    void* context, const char* module_id, void* target, void* detour,
    void** original) {
    if (!g_resource_probe_real_host || !g_resource_probe_real_host->create_hook)
        return BE_Result_NotReady;
    if (detour == reinterpret_cast<void*>(&DetourPumpWithAiShadowCoverage)) {
        g_resource_probe_pump_intercepted = true;
        return g_resource_probe_real_host->create_hook(context, module_id,
            target, reinterpret_cast<void*>(&DetourPumpWithResourceAssetProbe),
            original);
    }
    return g_resource_probe_real_host->create_hook(
        context, module_id, target, detour, original);
}

BE_Result BE_CALL InitializeWithResourceAssetProbe(const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;
    g_resource_probe_real_host = host;
    g_resource_probe_host_proxy = *host;
    g_resource_probe_host_proxy.create_hook = &ResourceAssetProbeCreateHook;
    g_resource_probe_pump_intercepted = false;
    g_resource_probe_ready_since = 0;
    g_resource_probe_contract_ready = false;

    // If the module is reinitialized in the same process after a shutdown, do
    // not issue another Load.  Existing process-lifetime roots are proof that
    // this diagnostic already ran.
    const bool already_rooted = g_resource_probe_boxed_handle_root != 0;
    g_resource_probe_attempted = already_rooted;
    g_resource_probe_done = already_rooted;

    const BE_Result result =
        InitializeWithAiShadowCoverage(&g_resource_probe_host_proxy);
    if (result != BE_Result_Ok) {
        g_resource_probe_real_host = nullptr;
        g_resource_probe_host_proxy = {};
        return result;
    }
    if (!g_resource_probe_pump_intercepted) {
        Log("[RESOURCE-PROBE] failed to intercept CustomModel pump.");
        ShutdownWithAiShadowCoverage();
        g_resource_probe_real_host = nullptr;
        g_resource_probe_host_proxy = {};
        return BE_Result_Failed;
    }

    g_resource_probe_contract_ready = ResolveResourceAssetProbeContracts();
    if (g_resource_probe_contract_ready) {
        if (already_rooted) {
            Log("[RESOURCE-PROBE] existing process-lifetime handle root detected; probe will not run again in this process.");
        } else {
            Log("[RESOURCE-PROBE] automatic one-shot probe armed: path=\"" +
                std::string(kResourceProbeEndminfPostmodelPath) + "\" hash=" +
                ResourceProbeHashText(kResourceProbeEndminfPostmodelHash));
            Log("[RESOURCE-PROBE] policy: wait-ready + 3000ms, Load once, root boxed handle, Get once, root asset, no retries/rechecks/releases.");
        }
    } else {
        Log("[RESOURCE-PROBE] contracts incomplete; probe disabled while production F12/AI/shadow behavior remains active.");
    }
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithResourceAssetProbe() {
    // Deliberately do not free g_resource_probe_boxed_handle_root or
    // g_resource_probe_prefab_root.  Releasing them during a live game process
    // would reintroduce the exact ownership experiment this revision removes.
    if (g_resource_probe_boxed_handle_root || g_resource_probe_prefab_root) {
        Log("[RESOURCE-PROBE] shutdown: process-lifetime resource roots intentionally retained; cleanup boundary is process exit.");
    }
    g_resource_probe_contract_ready = false;
    g_resource_probe_ready_since = 0;
    ShutdownWithAiShadowCoverage();
    g_resource_probe_real_host = nullptr;
    g_resource_probe_host_proxy = {};
}
