// Persistent scene coverage for control-switch/AI renderers plus a gameplay
// shadow experiment.
//
// The production replacement owns the BEM Mesh objects. This layer never calls
// BuildMeshFromComponent: when the game spawns/reconfigures another renderer for
// the same component (for example after switching the controlled character), it
// only reuses an already-existing custom Mesh after proving the new renderer's
// source Mesh has the same declaration, strides and bindpose palette.
//
// Gameplay uses separate shadowProxy renderers while the detail-screen model
// casts from its normal SkinnedMeshRenderers. For components that already have
// BEM geometry, this layer mirrors the detail-screen arrangement: the normal
// renderer is set to ShadowCastingMode.On and the corresponding shadowProxy is
// disabled. Every changed renderer is recorded and restored on F11/F12 rollback
// and shutdown. If the shadowCastingMode setter cannot be resolved, shadow
// changes are skipped entirely; AI coverage still runs.

constexpr uint64_t kAiCoverageScanIntervalMs = 100;
constexpr int32_t kShadowCastingOff = 0;
constexpr int32_t kShadowCastingOn = 1;

const BE_HostApiV1* g_ai_coverage_real_host = nullptr;
BE_HostApiV1 g_ai_coverage_host_proxy{};
bool g_ai_coverage_pump_intercepted = false;
uint64_t g_ai_coverage_next_scan_tick = 0;
const void* g_ai_coverage_set_shadow_mode_method = nullptr;

struct AiCoverageShadowModeState {
    void* renderer = nullptr;
    uint32_t handle = 0;
    int32_t original_mode = kShadowCastingOff;
};

struct AiCoverageProxyState {
    void* renderer = nullptr;
    uint32_t handle = 0;
    bool original_enabled = true;
};

std::vector<AiCoverageShadowModeState> g_ai_coverage_shadow_modes;
std::vector<AiCoverageProxyState> g_ai_coverage_shadow_proxies;

std::string AiCoveragePointer(const void* pointer) {
    std::array<char, 32> buffer{};
    std::snprintf(buffer.data(), buffer.size(), "%p", pointer);
    return std::string(buffer.data());
}

std::string AiCoverageComponentStem(std::string name) {
    const size_t shadow = name.find("_shadowProxy");
    if (shadow != std::string::npos) name.resize(shadow);

    const size_t lod = name.rfind("_lod");
    if (lod != std::string::npos && lod + 4 == name.size() &&
        std::isdigit(static_cast<unsigned char>(name[lod + 3]))) {
        name.resize(lod);
    }
    return name;
}

bool AiCoverageBindposesEqual(void* left_mesh, void* right_mesh) {
    if (!left_mesh || !right_mesh) return false;
    void* left = Invoke(Contract("mesh.get_bindposes"), left_mesh, nullptr, false);
    void* right = Invoke(Contract("mesh.get_bindposes"), right_mesh, nullptr, false);
    const int left_count = ArrayLength(left);
    const int right_count = ArrayLength(right);
    if (left_count <= 0 || left_count != right_count) return false;
    const Matrix4x4Raw* left_data = ManagedArrayData<Matrix4x4Raw>(
        left, static_cast<size_t>(left_count));
    const Matrix4x4Raw* right_data = ManagedArrayData<Matrix4x4Raw>(
        right, static_cast<size_t>(right_count));
    return left_data && right_data &&
        std::memcmp(left_data, right_data,
            static_cast<size_t>(left_count) * sizeof(Matrix4x4Raw)) == 0;
}

bool AiCoverageMeshesCompatible(void* source_mesh, void* custom_mesh) {
    if (!source_mesh || !custom_mesh || !IsNativeObjectAlive(source_mesh) ||
        !IsNativeObjectAlive(custom_mesh)) {
        return false;
    }
    VertexDeclaration source_declaration;
    VertexDeclaration custom_declaration;
    if (!ReadVertexDeclaration(source_mesh, source_declaration) ||
        !ReadVertexDeclaration(custom_mesh, custom_declaration) ||
        !DeclarationsEqual(source_declaration, custom_declaration)) {
        return false;
    }
    std::vector<int32_t> source_strides;
    std::vector<int32_t> custom_strides;
    if (!ReadMeshStrides(source_mesh, source_strides) ||
        !ReadMeshStrides(custom_mesh, custom_strides) ||
        source_strides != custom_strides) {
        return false;
    }
    return AiCoverageBindposesEqual(source_mesh, custom_mesh);
}

const ReplacedComponent* AiCoverageFindReusableSource(
    const RendererSummary& item) {
    if (!item.renderer || !item.mesh) return nullptr;

    if (const ReplacedComponent* identity = ReplacementSource(item.mesh);
        identity && identity->custom_mesh &&
        IsNativeObjectAlive(identity->custom_mesh)) {
        return identity;
    }

    const std::string renderer_stem =
        AiCoverageComponentStem(item.renderer_name);
    const std::string mesh_stem = AiCoverageComponentStem(item.mesh_name);
    for (const ReplacedComponent& record : g_replaced) {
        if (!record.custom_mesh || !record.original_mesh ||
            !IsNativeObjectAlive(record.custom_mesh) ||
            !IsNativeObjectAlive(record.original_mesh)) {
            continue;
        }
        const std::string source_stem =
            AiCoverageComponentStem(ObjectName(record.original_mesh));
        if (source_stem.empty() ||
            (source_stem != renderer_stem && source_stem != mesh_stem)) {
            continue;
        }
        return &record;
    }
    return nullptr;
}

void AiCoverageFreeHandle(uint32_t& handle) {
    if (handle && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, handle);
    }
    handle = 0;
}

bool AiCoverageGetShadowMode(void* renderer, int32_t& mode) {
    mode = -1;
    return renderer &&
        InvokeValue(Contract("renderer.get_shadow_casting_mode"), renderer,
            nullptr, mode);
}

bool AiCoverageSetShadowMode(void* renderer, int32_t mode) {
    if (!renderer || !g_ai_coverage_set_shadow_mode_method || !g_host ||
        !g_host->runtime_invoke || !IsNativeObjectAlive(renderer)) {
        return false;
    }
    void* parameters[1]{&mode};
    void* exception = nullptr;
    g_host->runtime_invoke(g_host->context,
        g_ai_coverage_set_shadow_mode_method, renderer, parameters, &exception);
    if (exception) return false;
    int32_t read_back = -1;
    return AiCoverageGetShadowMode(renderer, read_back) && read_back == mode;
}

void AiCoverageRememberShadowMode(void* renderer, int32_t original_mode) {
    for (const auto& state : g_ai_coverage_shadow_modes) {
        if (state.renderer == renderer) return;
    }
    AiCoverageShadowModeState state;
    state.renderer = renderer;
    state.original_mode = original_mode;
    if (g_host && g_host->gchandle_new) {
        state.handle = g_host->gchandle_new(g_host->context, renderer, 0);
    }
    g_ai_coverage_shadow_modes.push_back(state);
}

void AiCoverageRememberProxy(void* renderer, bool original_enabled) {
    for (const auto& state : g_ai_coverage_shadow_proxies) {
        if (state.renderer == renderer) return;
    }
    AiCoverageProxyState state;
    state.renderer = renderer;
    state.original_enabled = original_enabled;
    if (g_host && g_host->gchandle_new) {
        state.handle = g_host->gchandle_new(g_host->context, renderer, 0);
    }
    g_ai_coverage_shadow_proxies.push_back(state);
}

void RestoreAiShadowCoverage() {
    int restored_modes = 0;
    int restored_proxies = 0;
    for (auto& state : g_ai_coverage_shadow_modes) {
        if (state.renderer && IsNativeObjectAlive(state.renderer) &&
            AiCoverageSetShadowMode(state.renderer, state.original_mode)) {
            ++restored_modes;
        }
        AiCoverageFreeHandle(state.handle);
    }
    for (auto& state : g_ai_coverage_shadow_proxies) {
        if (state.renderer && IsNativeObjectAlive(state.renderer) &&
            SetRendererEnabled(state.renderer, state.original_enabled)) {
            ++restored_proxies;
        }
        AiCoverageFreeHandle(state.handle);
    }
    if (!g_ai_coverage_shadow_modes.empty() ||
        !g_ai_coverage_shadow_proxies.empty()) {
        Log("[AI-SHADOW] restored shadow state: castingModes=" +
            std::to_string(restored_modes) + " proxies=" +
            std::to_string(restored_proxies));
    }
    g_ai_coverage_shadow_modes.clear();
    g_ai_coverage_shadow_proxies.clear();
}

bool AiCoverageHasRendererRecord(void* renderer, size_t& index) {
    index = 0;
    while (index < g_replaced.size() && g_replaced[index].renderer != renderer)
        ++index;
    return index < g_replaced.size();
}

bool AiCoverageAttachReuseOnly(const RendererSummary& item) {
    if (!item.renderer || !item.mesh ||
        item.renderer_name.find(kActorMeshPrefix) != 0 ||
        item.renderer_name.find("shadowProxy") != std::string::npos) {
        return false;
    }

    size_t known = 0;
    const bool existed = AiCoverageHasRendererRecord(item.renderer, known);
    if (existed && g_replaced[known].custom_mesh &&
        item.mesh == g_replaced[known].custom_mesh) {
        return false;
    }

    ReplacedComponent source_copy;
    const ReplacedComponent* source = nullptr;
    if (existed && g_replaced[known].custom_mesh &&
        item.mesh == g_replaced[known].original_mesh) {
        source = &g_replaced[known];
    } else {
        source = AiCoverageFindReusableSource(item);
    }
    if (!source || !source->custom_mesh) return false;
    source_copy = *source;

    const BemComponent* component = ActiveComponent(source_copy.component_id);
    if (!component ||
        item.bones_count <= static_cast<int>(component->info.max_bone)) {
        return false;
    }

    if (item.mesh != source_copy.custom_mesh &&
        !AiCoverageMeshesCompatible(item.mesh, source_copy.custom_mesh)) {
        return false;
    }
    if (!ValidateRendererSkin(*component, item.renderer,
            item.mesh == source_copy.custom_mesh
                ? source_copy.original_mesh
                : item.mesh)) {
        return false;
    }

    void* original_for_record = item.mesh == source_copy.custom_mesh
        ? source_copy.original_mesh
        : item.mesh;
    if (!original_for_record || !IsNativeObjectAlive(original_for_record)) {
        return false;
    }

    if (item.mesh != source_copy.custom_mesh &&
        (!SetSharedMesh(item.renderer, source_copy.custom_mesh) ||
         Invoke(Contract("skinned.get_shared_mesh"), item.renderer, nullptr,
             false) != source_copy.custom_mesh)) {
        return false;
    }

    ReplacedComponent record = source_copy;
    record.renderer = item.renderer;
    record.original_mesh = original_for_record;
    record.original_index_count = item.total_indices > 0
        ? static_cast<uint32_t>(item.total_indices)
        : source_copy.original_index_count;
    record.renderer_handle = 0;
    record.original_mesh_handle = 0;
    record.custom_mesh_handle = 0;
    GetRendererEnabled(item.renderer, record.original_enabled);
    PinReplacement(record);

    if (existed) {
        ReplacedComponent retired = g_replaced[known];
        retired.renderer = nullptr;
        g_replaced[known] = record;
        g_replaced.push_back(retired);
    } else {
        g_replaced.push_back(record);
    }

    if (record.hidden_by_mod) {
        bool enabled = false;
        if (GetRendererEnabled(item.renderer, enabled) &&
            enabled != g_mask_visible) {
            SetRendererEnabled(item.renderer, g_mask_visible);
        }
    }

    Log("[AI-COVERAGE] reused C" + std::to_string(record.component_id) +
        " renderer=" + AiCoveragePointer(item.renderer) +
        " sourceMesh=" + AiCoveragePointer(original_for_record) +
        " customMesh=" + AiCoveragePointer(record.custom_mesh) +
        " path=\"" + item.path + "\" (no Mesh build)");
    return true;
}

bool AiCoverageStemHasCustomMesh(const std::string& stem) {
    if (stem.empty()) return false;
    for (const ReplacedComponent& record : g_replaced) {
        if (!record.custom_mesh || !record.original_mesh ||
            !IsNativeObjectAlive(record.custom_mesh) ||
            !IsNativeObjectAlive(record.original_mesh)) {
            continue;
        }
        if (AiCoverageComponentStem(ObjectName(record.original_mesh)) == stem)
            return true;
    }
    return false;
}

void AiCoverageApplyShadowState(
    const std::vector<void*>& normal_renderers,
    const std::vector<void*>& shadow_proxies) {
    if (!g_ai_coverage_set_shadow_mode_method) return;

    int casting_changed = 0;
    for (void* renderer : normal_renderers) {
        if (!renderer || !IsNativeObjectAlive(renderer)) continue;
        void* mesh = Invoke(Contract("skinned.get_shared_mesh"), renderer,
            nullptr, false);
        const ReplacedComponent* source = ReplacementSource(mesh);
        if (!source || !source->custom_mesh || mesh != source->custom_mesh)
            continue;
        int32_t mode = -1;
        if (!AiCoverageGetShadowMode(renderer, mode)) continue;
        if (mode != kShadowCastingOn) {
            AiCoverageRememberShadowMode(renderer, mode);
            if (AiCoverageSetShadowMode(renderer, kShadowCastingOn))
                ++casting_changed;
        }
    }

    int proxies_disabled = 0;
    for (void* renderer : shadow_proxies) {
        if (!renderer || !IsNativeObjectAlive(renderer)) continue;
        const std::string stem =
            AiCoverageComponentStem(ObjectName(renderer));
        if (!AiCoverageStemHasCustomMesh(stem)) continue;
        bool enabled = false;
        if (!GetRendererEnabled(renderer, enabled) || !enabled) continue;
        AiCoverageRememberProxy(renderer, enabled);
        if (SetRendererEnabled(renderer, false)) ++proxies_disabled;
    }

    if (casting_changed > 0 || proxies_disabled > 0) {
        Log("[AI-SHADOW] BEM shadow coverage updated: castingOn=" +
            std::to_string(casting_changed) + " proxiesDisabled=" +
            std::to_string(proxies_disabled));
    }
}

void PumpAiShadowCoverage() {
    if (!g_replacement_active || g_refresh_suspended || g_replaced.empty()) {
        if (!g_ai_coverage_shadow_modes.empty() ||
            !g_ai_coverage_shadow_proxies.empty()) {
            RestoreAiShadowCoverage();
        }
        return;
    }

    const uint64_t now = GetTickCount64();
    if (now < g_ai_coverage_next_scan_tick) return;
    g_ai_coverage_next_scan_tick = now + kAiCoverageScanIntervalMs;
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel))
        return;

    std::vector<void*> normal_renderers;
    std::vector<void*> shadow_proxies;
    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* objects = Invoke(Contract("resources.find_objects_of_type_all"),
        nullptr, parameters);
    const int count = std::max(ArrayLength(objects), 0);
    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (!renderer || !IsActiveSceneRenderer(renderer)) continue;
        const std::string name = ObjectName(renderer);
        if (name.find("endminf") == std::string::npos) continue;

        if (name.find("shadowProxy") != std::string::npos) {
            shadow_proxies.push_back(renderer);
            continue;
        }
        if (name.find(kActorMeshPrefix) != 0) continue;

        RendererSummary item = SummarizeRenderer(renderer);
        if (!item.renderer || !item.mesh) continue;
        normal_renderers.push_back(renderer);
        AiCoverageAttachReuseOnly(item);
    }

    AiCoverageApplyShadowState(normal_renderers, shadow_proxies);
    g_operation_in_progress.store(false, std::memory_order_release);
}

void ResolveAiShadowSetter() {
    g_ai_coverage_set_shadow_mode_method = nullptr;
    if (!g_host || !g_host->resolve_method) return;

    const std::array<const char*, 2> parameter_types{
        "UnityEngine.Rendering.ShadowCastingMode", "System.Int32"};
    for (const char* parameter_type : parameter_types) {
        const BE_MethodDescriptorV1 descriptor{
            "UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "set_shadowCastingMode", parameter_type, "System.Void", 1};
        BE_ResolvedMethodV1 method{};
        if (g_host->resolve_method(g_host->context, &descriptor, &method) ==
                BE_Result_Ok && method.method_info) {
            g_ai_coverage_set_shadow_mode_method = method.method_info;
            Log(std::string("[AI-SHADOW] resolved Renderer.set_shadowCastingMode parameter=") +
                parameter_type);
            return;
        }
    }
    Log("[AI-SHADOW] Renderer.set_shadowCastingMode unavailable; shadow proxy experiment disabled, AI Mesh coverage remains active.");
}

void __fastcall DetourPumpWithAiShadowCoverage(void* method_info) {
    // Preserve the production pump and the GOOD/BAD diagnostic, but run scene
    // coverage between them so POST_APPLY/TAG dumps observe the final bindings.
    DetourPump(method_info);
    PumpAiShadowCoverage();
    PumpF12StateDump();
}

BE_Result BE_CALL AiShadowCoverageCreateHook(
    void* context, const char* module_id, void* target, void* detour,
    void** original) {
    if (!g_ai_coverage_real_host || !g_ai_coverage_real_host->create_hook)
        return BE_Result_NotReady;
    if (detour == reinterpret_cast<void*>(&DetourPumpWithF12Dump)) {
        g_ai_coverage_pump_intercepted = true;
        return g_ai_coverage_real_host->create_hook(
            context, module_id, target,
            reinterpret_cast<void*>(&DetourPumpWithAiShadowCoverage), original);
    }
    return g_ai_coverage_real_host->create_hook(
        context, module_id, target, detour, original);
}

BE_Result BE_CALL InitializeWithAiShadowCoverage(const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;
    g_ai_coverage_real_host = host;
    g_ai_coverage_host_proxy = *host;
    g_ai_coverage_host_proxy.create_hook = &AiShadowCoverageCreateHook;
    g_ai_coverage_pump_intercepted = false;
    g_ai_coverage_next_scan_tick = 0;
    g_ai_coverage_shadow_modes.clear();
    g_ai_coverage_shadow_proxies.clear();

    const BE_Result result = InitializeWithF12Dump(&g_ai_coverage_host_proxy);
    if (result != BE_Result_Ok) {
        g_ai_coverage_real_host = nullptr;
        g_ai_coverage_host_proxy = {};
        return result;
    }
    if (!g_ai_coverage_pump_intercepted) {
        Log("[AI-COVERAGE] failed to intercept F12 diagnostic pump; coverage disabled.");
        ShutdownWithF12Dump();
        g_ai_coverage_real_host = nullptr;
        g_ai_coverage_host_proxy = {};
        return BE_Result_Failed;
    }

    ResolveAiShadowSetter();
    Log("[AI-COVERAGE] active: control-switch/AI renderers reuse existing BEM Meshes only; this layer never builds a Mesh.");
    Log("[AI-SHADOW] active when setter is available: BEM renderers cast, matching shadowProxy renderers are disabled and restored on rollback.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithAiShadowCoverage() {
    RestoreAiShadowCoverage();
    g_ai_coverage_next_scan_tick = 0;
    g_ai_coverage_set_shadow_mode_method = nullptr;

    ShutdownWithF12Dump();
    g_ai_coverage_real_host = nullptr;
    g_ai_coverage_host_proxy = {};
}
