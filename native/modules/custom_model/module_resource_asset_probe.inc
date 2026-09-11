// Read-only resource-layer probe for the global replacement design.
//
// It asks the game's own I18N resource loader for the Endminf postmodel prefab,
// enumerates the prefab's SkinnedMeshRenderers, and compares the prefab Mesh
// identities with loaded scene consumers.  No prefab, Mesh, renderer or handle
// contents are modified in this build.

constexpr uint64_t kResourceProbeEndminfPostmodelHash = 0x0C9AC03D143EA082ULL;
constexpr uint8_t kResourceProbeMainCategory = 0;
constexpr uint64_t kResourceProbeRetryMs = 1000;
constexpr uint64_t kResourceProbeCacheRecheckMs = 2000;
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
BE_ResolvedMethodV1 g_resource_probe_get_components{};
BE_ResolvedClassV1 g_resource_probe_game_object_class{};

void* g_resource_probe_prefab = nullptr;
uint32_t g_resource_probe_prefab_handle = 0;
uint64_t g_resource_probe_next_attempt = 0;
uint64_t g_resource_probe_recheck_at = 0;
bool g_resource_probe_contract_ready = false;
bool g_resource_probe_snapshot_done = false;
bool g_resource_probe_recheck_done = false;

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

void* ResourceProbeLoadPrefab() {
    if (!g_resource_probe_contract_ready || !g_host ||
        !g_host->runtime_invoke || !g_host->object_unbox ||
        !g_resource_probe_game_object_class.type_object) {
        return nullptr;
    }

    uint64_t hash = kResourceProbeEndminfPostmodelHash;
    uint8_t category = kResourceProbeMainCategory;
    void* parameters[3]{&hash, g_resource_probe_game_object_class.type_object,
        &category};
    void* exception = nullptr;
    void* boxed_handle = g_host->runtime_invoke(g_host->context,
        g_resource_probe_i18n_load.method_info, nullptr, parameters, &exception);
    if (exception || !boxed_handle) return nullptr;

    // FAssetProxyHandle is a value type.  This is the same pattern used by the
    // existing model module: unbox the handle, then call FAssetProxyHandle.Get.
    void* handle = g_host->object_unbox(g_host->context, boxed_handle);
    if (!handle) return nullptr;
    exception = nullptr;
    void* asset = g_host->runtime_invoke(g_host->context,
        g_resource_probe_handle_get.method_info, handle, nullptr, &exception);
    return exception ? nullptr : asset;
}

void ResourceProbeReleasePrefab() {
    if (g_resource_probe_prefab_handle && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, g_resource_probe_prefab_handle);
    }
    g_resource_probe_prefab_handle = 0;
    g_resource_probe_prefab = nullptr;
}

void ResourceProbeRetainPrefab(void* prefab) {
    if (prefab == g_resource_probe_prefab) return;
    ResourceProbeReleasePrefab();
    g_resource_probe_prefab = prefab;
    if (prefab && g_host && g_host->gchandle_new) {
        g_resource_probe_prefab_handle =
            g_host->gchandle_new(g_host->context, prefab, 0);
    }
}

struct ResourceProbePrefabRenderer {
    void* renderer = nullptr;
    void* mesh = nullptr;
    std::string renderer_name;
    std::string mesh_name;
    int vertex_count = -1;
    uint64_t total_indices = 0;
    int bones_count = 0;
};

std::vector<ResourceProbePrefabRenderer> ResourceProbePrefabRenderers(
    void* prefab) {
    std::vector<ResourceProbePrefabRenderer> output;
    if (!prefab || !g_resource_probe_get_components.method_info ||
        !g_skinned_renderer_class.type_object || !g_host ||
        !g_host->runtime_invoke) {
        return output;
    }

    bool include_inactive = true;
    void* parameters[2]{g_skinned_renderer_class.type_object, &include_inactive};
    void* exception = nullptr;
    void* components = g_host->runtime_invoke(g_host->context,
        g_resource_probe_get_components.method_info, prefab, parameters,
        &exception);
    if (exception || !components) return output;

    const int count = std::max(ArrayLength(components), 0);
    output.reserve(static_cast<size_t>(count));
    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(components, index);
        if (!renderer || !IsNativeObjectAlive(renderer)) continue;
        RendererSummary summary = SummarizeRenderer(renderer);
        if (!summary.mesh || !IsNativeObjectAlive(summary.mesh)) continue;

        ResourceProbePrefabRenderer item;
        item.renderer = renderer;
        item.mesh = summary.mesh;
        item.renderer_name = summary.renderer_name;
        item.mesh_name = summary.mesh_name;
        item.total_indices = summary.total_indices;
        item.bones_count = summary.bones_count;
        InvokeValue(Contract("mesh.get_vertex_count"), summary.mesh, nullptr,
            item.vertex_count);
        output.push_back(std::move(item));
    }
    return output;
}

void ResourceProbeLogSnapshot(void* prefab) {
    const auto prefab_renderers = ResourceProbePrefabRenderers(prefab);
    std::vector<void*> prefab_renderer_ptrs;
    std::vector<void*> prefab_meshes;
    for (const auto& item : prefab_renderers) {
        prefab_renderer_ptrs.push_back(item.renderer);
        if (std::find(prefab_meshes.begin(), prefab_meshes.end(), item.mesh) ==
            prefab_meshes.end()) {
            prefab_meshes.push_back(item.mesh);
        }
    }

    int loaded_endminf_smr = 0;
    int exact_mesh_consumers = 0;
    int active_consumers = 0;
    int inactive_consumers = 0;
    std::vector<std::string> active_samples;

    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* objects = Invoke(Contract("resources.find_objects_of_type_all"),
        nullptr, parameters, false);
    const int count = std::max(ArrayLength(objects), 0);
    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (!renderer || !IsNativeObjectAlive(renderer)) continue;
        const std::string name = ObjectName(renderer);
        if (name.find("endminf") == std::string::npos) continue;
        ++loaded_endminf_smr;

        void* mesh = Invoke(Contract("skinned.get_shared_mesh"), renderer,
            nullptr, false);
        if (!mesh || std::find(prefab_meshes.begin(), prefab_meshes.end(), mesh) ==
                prefab_meshes.end()) {
            continue;
        }
        if (std::find(prefab_renderer_ptrs.begin(), prefab_renderer_ptrs.end(),
                renderer) != prefab_renderer_ptrs.end()) {
            continue;
        }

        ++exact_mesh_consumers;
        if (IsActiveSceneRenderer(renderer)) {
            ++active_consumers;
            if (active_samples.size() < 8) {
                active_samples.push_back(name + " mesh=\"" + ObjectName(mesh) +
                    "\" renderer=" + ResourceProbePointer(renderer));
            }
        } else {
            ++inactive_consumers;
        }
    }

    Log("[RESOURCE-PROBE] PREFAB hash=" +
        ResourceProbeHashText(kResourceProbeEndminfPostmodelHash) + " object=" +
        ResourceProbePointer(prefab) + " name=\"" + ObjectName(prefab) +
        "\" childSMR=" + std::to_string(prefab_renderers.size()) +
        " uniqueMeshes=" + std::to_string(prefab_meshes.size()));

    for (size_t index = 0; index < prefab_renderers.size(); ++index) {
        const auto& item = prefab_renderers[index];
        Log("[RESOURCE-PROBE] PREFAB-SMR #" + std::to_string(index) +
            " renderer=" + ResourceProbePointer(item.renderer) + " name=\"" +
            item.renderer_name + "\" mesh=" + ResourceProbePointer(item.mesh) +
            " meshName=\"" + item.mesh_name + "\" vtx=" +
            std::to_string(item.vertex_count) + " idx=" +
            std::to_string(item.total_indices) + " bones=" +
            std::to_string(item.bones_count));
    }

    Log("[RESOURCE-PROBE] IDENTITY loadedEndminfSMR=" +
        std::to_string(loaded_endminf_smr) + " exactMeshConsumers=" +
        std::to_string(exact_mesh_consumers) + " activeSceneConsumers=" +
        std::to_string(active_consumers) + " inactiveConsumers=" +
        std::to_string(inactive_consumers));
    for (const std::string& sample : active_samples) {
        Log("[RESOURCE-PROBE] ACTIVE-CONSUMER " + sample);
    }

    if (!prefab_renderers.empty()) {
        Log("[RESOURCE-PROBE] patch-window candidate confirmed: the game's loader returned a prefab whose renderer/Mesh references are directly inspectable. This build intentionally does not patch them.");
    } else {
        Log("[RESOURCE-PROBE] target object loaded, but no readable child SkinnedMeshRenderer was found.");
    }
}

void PumpResourceAssetProbe() {
    if (!g_resource_probe_contract_ready) return;
    const uint64_t now = GetTickCount64();

    if (g_resource_probe_snapshot_done) {
        if (!g_resource_probe_recheck_done && g_resource_probe_recheck_at &&
            now >= g_resource_probe_recheck_at) {
            g_resource_probe_recheck_done = true;
            void* second = ResourceProbeLoadPrefab();
            Log("[RESOURCE-PROBE] CACHE-RECHECK first=" +
                ResourceProbePointer(g_resource_probe_prefab) + " second=" +
                ResourceProbePointer(second) + " sameObject=" +
                std::to_string(second && second == g_resource_probe_prefab ? 1 : 0));
        }
        return;
    }

    if (now < g_resource_probe_next_attempt) return;
    g_resource_probe_next_attempt = now + kResourceProbeRetryMs;
    if (!ResourceProbeManagerReady()) return;

    void* prefab = ResourceProbeLoadPrefab();
    if (!prefab || !IsNativeObjectAlive(prefab)) {
        Log("[RESOURCE-PROBE] target prefab not ready; retrying hash=" +
            ResourceProbeHashText(kResourceProbeEndminfPostmodelHash));
        return;
    }

    ResourceProbeRetainPrefab(prefab);
    ResourceProbeLogSnapshot(prefab);
    g_resource_probe_snapshot_done = true;
    g_resource_probe_recheck_at = now + kResourceProbeCacheRecheckMs;
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
    ok &= ResourceProbeResolve(g_resource_probe_get_components,
        "UnityEngine.CoreModule.dll", "UnityEngine", "GameObject",
        "GetComponentsInChildren", "System.Type|System.Boolean",
        "UnityEngine.Component[]", 2, "GameObject.GetComponentsInChildren");
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
    g_resource_probe_next_attempt = 0;
    g_resource_probe_recheck_at = 0;
    g_resource_probe_contract_ready = false;
    g_resource_probe_snapshot_done = false;
    g_resource_probe_recheck_done = false;
    ResourceProbeReleasePrefab();

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
        Log("[RESOURCE-PROBE] read-only global-resource probe armed: path=\"" +
            std::string(kResourceProbeEndminfPostmodelPath) + "\" hash=" +
            ResourceProbeHashText(kResourceProbeEndminfPostmodelHash));
        Log("[RESOURCE-PROBE] this build only proves loader/cache/prefab identity; no resource mutation is performed.");
    } else {
        Log("[RESOURCE-PROBE] contracts incomplete; probe disabled while production F12/AI/shadow behavior remains active.");
    }
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithResourceAssetProbe() {
    ResourceProbeReleasePrefab();
    g_resource_probe_contract_ready = false;
    g_resource_probe_snapshot_done = false;
    g_resource_probe_recheck_done = false;
    g_resource_probe_next_attempt = 0;
    g_resource_probe_recheck_at = 0;
    ShutdownWithAiShadowCoverage();
    g_resource_probe_real_host = nullptr;
    g_resource_probe_host_proxy = {};
}
