// Balanced startup resource probe for the global replacement design.
//
// This revision mirrors the existing model module's ABI for FAssetProxyHandle:
// I18NAssetLoader.Load is invoked through IL2CPP, while value-type instance
// methods are called through their resolved native method pointers.  The proxy
// handle is kept alive only inside this one synchronous probe and is explicitly
// disposed before the frame returns.  The prefab itself is never GC-rooted.
//
// The experiment answers one question: does touching the postmodel remain safe
// when Better-Endfield does not carry resource ownership across the login ->
// main-interface transition?

constexpr uint64_t kResourceProbeEndminfPostmodelHash = 0x0C9AC03D143EA082ULL;
constexpr uint8_t kResourceProbeMainCategory = 0;
constexpr uint64_t kResourceProbeReadyStabilizeMs = 500;
constexpr uint64_t kResourceProbeObserve1Ms = 2000;
constexpr uint64_t kResourceProbeObserve2Ms = 6000;
constexpr char kResourceProbeEndminfPostmodelPath[] =
    "assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/"
    "chr_0003_endminf_postmodel.prefab";

const BE_HostApiV1* g_resource_probe_real_host = nullptr;
BE_HostApiV1 g_resource_probe_host_proxy{};
bool g_resource_probe_pump_intercepted = false;

BE_ResolvedMethodV1 g_resource_probe_i18n_load{};
BE_ResolvedMethodV1 g_resource_probe_handle_get{};
BE_ResolvedMethodV1 g_resource_probe_handle_load_immediate{};
BE_ResolvedMethodV1 g_resource_probe_handle_dispose{};
BE_ResolvedMethodV1 g_resource_probe_initialized{};
BE_ResolvedMethodV1 g_resource_probe_async_initing{};
BE_ResolvedClassV1 g_resource_probe_game_object_class{};

uint64_t g_resource_probe_ready_since = 0;
uint64_t g_resource_probe_observe1_at = 0;
uint64_t g_resource_probe_observe2_at = 0;
bool g_resource_probe_contract_ready = false;
bool g_resource_probe_attempted = false;
bool g_resource_probe_observe1_done = false;
bool g_resource_probe_observe2_done = false;

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

using ResourceProbeObjectInstanceFn = void*(__fastcall*)(void*, void*);
using ResourceProbeVoidInstanceFn = void(__fastcall*)(void*, void*);

void* ResourceProbeDirectObjectCall(
    const BE_ResolvedMethodV1& method, void* instance) {
    if (!method.method_pointer || !method.method_info || !instance) return nullptr;
    __try {
        return reinterpret_cast<ResourceProbeObjectInstanceFn>(method.method_pointer)(
            instance, const_cast<void*>(method.method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

bool ResourceProbeDirectVoidCall(
    const BE_ResolvedMethodV1& method, void* instance) {
    if (!method.method_pointer || !method.method_info || !instance) return false;
    __try {
        reinterpret_cast<ResourceProbeVoidInstanceFn>(method.method_pointer)(
            instance, const_cast<void*>(method.method_info));
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
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

void ResourceProbeRunBalancedOnce() {
    if (g_resource_probe_attempted) return;
    g_resource_probe_attempted = true;

    if (!g_host || !g_host->runtime_invoke || !g_host->object_unbox ||
        !g_resource_probe_game_object_class.type_object) {
        Log("[RESOURCE-PROBE] BALANCED aborted before Load: required runtime API unavailable.");
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
        Log("[RESOURCE-PROBE] LOAD failed; balanced probe stops without retry.");
        return;
    }

    // Root only for this synchronous scope.  The root is not the resource
    // ownership mechanism; FAssetProxyHandle.Dispose is.  This simply keeps the
    // boxed value stable while native instance methods execute.
    const uint32_t box_root = g_host->gchandle_new
        ? g_host->gchandle_new(g_host->context, boxed_handle, 0)
        : 0;
    void* handle = g_host->object_unbox(g_host->context, boxed_handle);
    Log("[RESOURCE-PROBE] HANDLE box=" + ResourceProbePointer(boxed_handle) +
        " payload=" + ResourceProbePointer(handle) + " tempRoot=" +
        std::to_string(box_root));

    if (!handle) {
        if (box_root && g_host->gchandle_free) {
            g_host->gchandle_free(g_host->context, box_root);
        }
        Log("[RESOURCE-PROBE] HANDLE unbox failed; temporary GC root released.");
        return;
    }

    // Match native/modules/model/module.cpp: value-type Get is a native instance
    // call, not runtime_invoke(instance=unboxed-value).
    void* prefab = ResourceProbeDirectObjectCall(g_resource_probe_handle_get, handle);
    Log("[RESOURCE-PROBE] GET(native) result=" + ResourceProbePointer(prefab));

    if (!prefab) {
        Log("[RESOURCE-PROBE] GET not ready; calling FAssetProxyHandle.LoadImmediate once.");
        const bool load_immediate_ok = ResourceProbeDirectVoidCall(
            g_resource_probe_handle_load_immediate, handle);
        Log("[RESOURCE-PROBE] LOAD-IMMEDIATE returned=" +
            std::to_string(load_immediate_ok ? 1 : 0));
        prefab = ResourceProbeDirectObjectCall(g_resource_probe_handle_get, handle);
        Log("[RESOURCE-PROBE] GET(native)-AFTER-IMMEDIATE result=" +
            ResourceProbePointer(prefab));
    }

    if (prefab) {
        Log("[RESOURCE-PROBE] PREFAB transient object=" +
            ResourceProbePointer(prefab) + " alive=" +
            std::to_string(IsNativeObjectAlive(prefab) ? 1 : 0) + " name=\"" +
            ObjectName(prefab) + "\"; prefab is intentionally NOT GC-rooted.");
        const ResourceProbeLiveSnapshot during = ResourceProbeCaptureLiveSnapshot();
        ResourceProbeLogLiveSnapshot("BEFORE-DISPOSE", during);
        Log("[RESOURCE-PROBE] OBSERVE beforeDispose deltaLoaded=" +
            std::to_string(during.loaded_endminf_smr - before.loaded_endminf_smr) +
            " deltaActive=" +
            std::to_string(during.active_endminf_smr - before.active_endminf_smr));
    } else {
        Log("[RESOURCE-PROBE] PREFAB unavailable even after LoadImmediate; handle will still be disposed.");
    }

    const bool dispose_ok = ResourceProbeDirectVoidCall(
        g_resource_probe_handle_dispose, handle);
    if (box_root && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, box_root);
    }
    Log("[RESOURCE-PROBE] RELEASE handleDispose=" +
        std::to_string(dispose_ok ? 1 : 0) + " tempRootFreed=" +
        std::to_string(box_root ? 1 : 0) +
        " retainedHandleRoots=0 retainedPrefabRoots=0");

    const ResourceProbeLiveSnapshot released = ResourceProbeCaptureLiveSnapshot();
    ResourceProbeLogLiveSnapshot("AFTER-DISPOSE", released);

    const uint64_t now = GetTickCount64();
    g_resource_probe_observe1_at = now + kResourceProbeObserve1Ms;
    g_resource_probe_observe2_at = now + kResourceProbeObserve2Ms;
    Log("[RESOURCE-PROBE] BALANCED complete: one Load, native Get, optional LoadImmediate, explicit Dispose in same frame; delayed observations armed.");
}

void ResourceProbeRunDelayedObservations(uint64_t now) {
    if (!g_resource_probe_attempted) return;
    if (!g_resource_probe_observe1_done && g_resource_probe_observe1_at &&
        now >= g_resource_probe_observe1_at) {
        g_resource_probe_observe1_done = true;
        ResourceProbeLogLiveSnapshot(
            "POST-RELEASE+2S", ResourceProbeCaptureLiveSnapshot());
    }
    if (!g_resource_probe_observe2_done && g_resource_probe_observe2_at &&
        now >= g_resource_probe_observe2_at) {
        g_resource_probe_observe2_done = true;
        ResourceProbeLogLiveSnapshot(
            "POST-RELEASE+6S", ResourceProbeCaptureLiveSnapshot());
        Log("[RESOURCE-PROBE] observation window complete; no resource handles or prefab roots are retained by the probe.");
    }
}

void PumpResourceAssetProbe() {
    if (!g_resource_probe_contract_ready) return;
    const uint64_t now = GetTickCount64();

    if (g_resource_probe_attempted) {
        ResourceProbeRunDelayedObservations(now);
        return;
    }

    if (!ResourceProbeManagerReady()) {
        g_resource_probe_ready_since = 0;
        return;
    }

    if (!g_resource_probe_ready_since) {
        g_resource_probe_ready_since = now;
        Log("[RESOURCE-PROBE] ResourceManager ready; balanced probe will run after " +
            std::to_string(kResourceProbeReadyStabilizeMs) +
            " ms stabilization window.");
        return;
    }

    if (now - g_resource_probe_ready_since < kResourceProbeReadyStabilizeMs) {
        return;
    }

    ResourceProbeRunBalancedOnce();
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
    ok &= ResourceProbeResolve(g_resource_probe_handle_load_immediate,
        "Common.Beyond.dll", "Beyond.Resource", "FAssetProxyHandle",
        "LoadImmediate", nullptr, "System.Void", 0,
        "FAssetProxyHandle.LoadImmediate");
    ok &= ResourceProbeResolve(g_resource_probe_handle_dispose,
        "Common.Beyond.dll", "Beyond.Resource", "FAssetProxyHandle", "Dispose",
        nullptr, "System.Void", 0, "FAssetProxyHandle.Dispose");
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
    g_resource_probe_observe1_at = 0;
    g_resource_probe_observe2_at = 0;
    g_resource_probe_observe1_done = false;
    g_resource_probe_observe2_done = false;
    g_resource_probe_contract_ready = false;

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
        if (g_resource_probe_attempted) {
            Log("[RESOURCE-PROBE] probe already ran in this process; it will not acquire another resource handle.");
        } else {
            Log("[RESOURCE-PROBE] automatic balanced-lifetime probe armed: path=\"" +
                std::string(kResourceProbeEndminfPostmodelPath) + "\" hash=" +
                ResourceProbeHashText(kResourceProbeEndminfPostmodelHash));
            Log("[RESOURCE-PROBE] policy: wait-ready + 500ms, Load once, native Get, optional LoadImmediate, explicit Dispose in same frame, retain nothing across scene transition.");
        }
    } else {
        Log("[RESOURCE-PROBE] contracts incomplete; probe disabled while production F12/AI/shadow behavior remains active.");
    }
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithResourceAssetProbe() {
    Log("[RESOURCE-PROBE] shutdown: no probe-owned FAssetProxyHandle or prefab GC root is retained.");
    g_resource_probe_contract_ready = false;
    g_resource_probe_ready_since = 0;
    g_resource_probe_observe1_at = 0;
    g_resource_probe_observe2_at = 0;
    ShutdownWithAiShadowCoverage();
    g_resource_probe_real_host = nullptr;
    g_resource_probe_host_proxy = {};
}