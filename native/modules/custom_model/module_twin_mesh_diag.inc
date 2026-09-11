// C9 four-way Mesh construction diagnostic.
//
// The previous twin test established that first-use order has no obvious
// correlation with the fault. This version keeps the same main-thread hook but
// builds four C9 variants in one pump frame so the creation path itself is the
// only intended variable:
//
//   Num7 : build all four variants, detached
//   Num8 : A = new Mesh + canonical BuildMeshFromComponent + UploadMeshData
//   Num9 : B = new Mesh + canonical BuildMeshFromComponent, but its final
//          UploadMeshDataImpl call is intercepted as a no-op
//   Num4 : C = clone original Mesh + overwrite BEM VB/IB/layout + UploadMeshData
//   Num5 : D = clone original Mesh + overwrite BEM VB/IB/layout, no final upload
//   Num0 : restore original C9 Mesh while retaining A/B/C/D
//
// Restore with Num0 before switching variants. F12/F11 and the BEM format are
// unchanged. GraphicsBuffer is not used.

constexpr int kTwinBuildHotkey = VK_NUMPAD7;
constexpr int kTwinAttachAHotkey = VK_NUMPAD8;
constexpr int kTwinAttachBHotkey = VK_NUMPAD9;
constexpr int kTwinAttachCHotkey = VK_NUMPAD4;
constexpr int kTwinAttachDHotkey = VK_NUMPAD5;
constexpr int kTwinRestoreHotkey = VK_NUMPAD0;
constexpr uint32_t kTwinComponentId = 9;
constexpr size_t kTwinVariantCount = 4;

enum class TwinVariant : size_t {
    A_NewUpload = 0,
    B_NewNoUpload = 1,
    C_CloneUpload = 2,
    D_CloneNoUpload = 3,
};

constexpr std::array<const char*, kTwinVariantCount> kTwinVariantNames{{
    "A:new+upload",
    "B:new+no-upload",
    "C:clone+overwrite+upload",
    "D:clone+overwrite+no-upload",
}};

std::atomic_bool g_twin_build_requested{false};
std::atomic_bool g_twin_attach_a_requested{false};
std::atomic_bool g_twin_attach_b_requested{false};
std::atomic_bool g_twin_attach_c_requested{false};
std::atomic_bool g_twin_attach_d_requested{false};
std::atomic_bool g_twin_restore_requested{false};
std::atomic_bool g_twin_hotkey_stop{false};
HANDLE g_twin_hotkey_thread = nullptr;
int32_t g_twin_last_frame = -1;

const BE_HostApiV1* g_twin_real_host = nullptr;
BE_HostApiV1 g_twin_host_proxy{};
bool g_twin_pump_intercepted = false;

const void* g_twin_clone_method_info = nullptr;
const char* g_twin_clone_method_name = nullptr;

struct TwinMeshDiagState {
    uint64_t generation = 0;
    void* renderer = nullptr;
    void* original_mesh = nullptr;
    std::array<void*, kTwinVariantCount> meshes{};
    uint32_t renderer_handle = 0;
    uint32_t original_handle = 0;
    std::array<uint32_t, kTwinVariantCount> mesh_handles{};
    std::array<uint32_t, kTwinVariantCount> attach_count{};
    std::array<uint32_t, kTwinVariantCount> first_use_order{};
    uint32_t first_use_sequence = 0;
    std::string path;
};
TwinMeshDiagState g_twin{};
uint64_t g_twin_next_generation = 0;

size_t TwinVariantIndex(TwinVariant variant) {
    return static_cast<size_t>(variant);
}

const char* TwinVariantName(TwinVariant variant) {
    return kTwinVariantNames[TwinVariantIndex(variant)];
}

void* TwinVariantMesh(TwinVariant variant) {
    return g_twin.meshes[TwinVariantIndex(variant)];
}

bool IsTwinVariantMesh(void* mesh) {
    if (!mesh) return false;
    return std::any_of(g_twin.meshes.begin(), g_twin.meshes.end(),
        [&](void* candidate) { return candidate == mesh; });
}

void ReleaseTwinHandles() {
    if (!g_host || !g_host->gchandle_free) return;
    if (g_twin.renderer_handle) {
        g_host->gchandle_free(g_host->context, g_twin.renderer_handle);
    }
    if (g_twin.original_handle) {
        g_host->gchandle_free(g_host->context, g_twin.original_handle);
    }
    for (uint32_t handle : g_twin.mesh_handles) {
        if (handle) g_host->gchandle_free(g_host->context, handle);
    }
    g_twin.renderer_handle = 0;
    g_twin.original_handle = 0;
    g_twin.mesh_handles.fill(0);
}

bool PinTwinObjects() {
    if (!g_host || !g_host->gchandle_new || !g_host->gchandle_free ||
        !g_twin.renderer || !g_twin.original_mesh) {
        return false;
    }
    for (void* mesh : g_twin.meshes) {
        if (!mesh) return false;
    }

    g_twin.renderer_handle =
        g_host->gchandle_new(g_host->context, g_twin.renderer, 0);
    g_twin.original_handle =
        g_host->gchandle_new(g_host->context, g_twin.original_mesh, 0);
    for (size_t i = 0; i < kTwinVariantCount; ++i) {
        g_twin.mesh_handles[i] =
            g_host->gchandle_new(g_host->context, g_twin.meshes[i], 0);
    }

    if (!g_twin.renderer_handle || !g_twin.original_handle ||
        std::any_of(g_twin.mesh_handles.begin(), g_twin.mesh_handles.end(),
            [](uint32_t handle) { return handle == 0; })) {
        ReleaseTwinHandles();
        return false;
    }
    return true;
}

bool TwinSetExists() {
    return g_twin.renderer && g_twin.original_mesh &&
        std::all_of(g_twin.meshes.begin(), g_twin.meshes.end(),
            [](void* mesh) { return mesh != nullptr; });
}

bool TwinMeshesInUse() {
    bool any = false;
    for (void* mesh : g_twin.meshes) any = any || mesh != nullptr;
    if (!any) return false;

    std::vector<RendererSummary> live;
    if (!SnapshotRendererMeshes(live)) return true;
    for (const auto& item : live) {
        if (IsTwinVariantMesh(item.mesh)) return true;
    }
    return false;
}

void DestroyTwinMeshArray(std::array<void*, kTwinVariantCount>& meshes) {
    for (void*& mesh : meshes) {
        if (mesh && IsNativeObjectAlive(mesh)) DestroyUnityObject(mesh);
        mesh = nullptr;
    }
}

bool DestroyTwinSetIfDetached() {
    if (!TwinSetExists()) {
        ReleaseTwinHandles();
        g_twin = {};
        return true;
    }
    if (TwinMeshesInUse()) {
        Log("[VARIANT-DIAG] previous C9 variant set is still bound; press Num0 before Num7.");
        return false;
    }

    const uint64_t retired = g_twin.generation;
    std::array<void*, kTwinVariantCount> doomed = g_twin.meshes;
    // Keep the managed wrappers pinned until Destroy has been issued for every
    // native Mesh; releasing GC handles first would make this cleanup itself a
    // lifetime experiment.
    DestroyTwinMeshArray(doomed);
    ReleaseTwinHandles();
    g_twin = {};
    Log("[VARIANT-DIAG] retired detached generation=" +
        std::to_string(retired) + " meshesDestroyed=4.");
    return true;
}

const BemComponent* TwinFindC9(const BemPocData& bem) {
    for (const auto& component : bem.components) {
        if (component.info.component_id == kTwinComponentId &&
            (component.info.flags & kComponentFlagNoGeometry) == 0) {
            return &component;
        }
    }
    return nullptr;
}

const RendererSummary* TwinFindGameplayC9(
    const std::vector<RendererSummary>& summaries,
    const BemComponent& component) {
    const RendererSummary* candidate = nullptr;
    for (const auto& item : summaries) {
        if (!item.renderer || !item.mesh ||
            !IsGameplayPostModelPath(item.path) ||
            item.total_indices != component.info.original_index_count ||
            item.renderer_name != kTargetRendererName) {
            continue;
        }
        if (candidate) {
            Log("[VARIANT-DIAG] more than one gameplay C9 candidate is live; refusing ambiguous test.");
            return nullptr;
        }
        candidate = &item;
    }
    return candidate;
}

bool TwinBasePreflight(const char* action) {
    if (!GenerationDiagOfficialPathIdle()) {
        Log(std::string("[VARIANT-DIAG] ") + action +
            " refused: normal F12 replacement is active/pending; use F11 first.");
        return false;
    }
    if (g_generation_diag_attached) {
        Log(std::string("[VARIANT-DIAG] ") + action +
            " refused: cached-generation diagnostic is attached; press main '=' first.");
        return false;
    }
    return true;
}

bool ResolveTwinCloneMethod() {
    if (g_twin_clone_method_info) return true;
    if (!g_host || !g_host->resolve_method || !g_host->runtime_invoke) {
        Log("[VARIANT-DIAG] host method resolver/runtime_invoke is unavailable.");
        return false;
    }

    constexpr std::array<const char*, 2> methods{{
        "Internal_CloneSingle",
        "Instantiate",
    }};
    for (const char* method : methods) {
        BE_MethodDescriptorV1 descriptor{
            "UnityEngine.CoreModule.dll",
            "UnityEngine",
            "Object",
            method,
            "UnityEngine.Object",
            "UnityEngine.Object",
            1};
        BE_ResolvedMethodV1 resolved{};
        if (g_host->resolve_method(g_host->context, &descriptor, &resolved) ==
                BE_Result_Ok &&
            resolved.method_info && resolved.method_pointer) {
            g_twin_clone_method_info = resolved.method_info;
            g_twin_clone_method_name = method;
            Log(std::string("[VARIANT-DIAG] resolved UnityEngine.Object.") +
                method + "(Object) for Mesh clone variants.");
            return true;
        }
    }

    Log("[VARIANT-DIAG] neither Object.Internal_CloneSingle(Object) nor Object.Instantiate(Object) resolved.");
    return false;
}

void* CloneTwinMesh(void* source_mesh) {
    if (!source_mesh || !ResolveTwinCloneMethod()) return nullptr;

    void* parameters[1]{source_mesh};
    void* exception = nullptr;
    void* clone = g_host->runtime_invoke(
        g_host->context, g_twin_clone_method_info, nullptr, parameters,
        &exception);
    if (exception || !clone || !IsNativeObjectAlive(clone)) {
        Log(std::string("[VARIANT-DIAG] ") +
            (g_twin_clone_method_name ? g_twin_clone_method_name : "clone") +
            " failed for sourceMesh=" + GenerationDiagPointer(source_mesh));
        return nullptr;
    }
    return clone;
}

// BuildMeshFromComponent currently performs the canonical final
// UploadMeshDataImpl(false). For B only, temporarily replace that one binding
// with a same-signature no-op. Everything else in the canonical builder,
// including validation, declaration creation, VB/IB writes, submesh, bindposes
// and bounds, executes unchanged.
void TwinNoopUploadMeshData(void*, bool) {
}

bool BuildTwinNewNoUpload(
    const BemComponent& component, void* source_mesh, void*& new_mesh) {
    new_mesh = nullptr;
    if (!ResolveEngineBindings() || !g_engine.upload_mesh_data) {
        Log("[VARIANT-DIAG] B build failed: UploadMeshData binding unavailable.");
        return false;
    }

    const MeshUploadDataFn real_upload = g_engine.upload_mesh_data;
    g_engine.upload_mesh_data = &TwinNoopUploadMeshData;
    const bool ok = BuildMeshFromComponent(component, source_mesh, new_mesh);
    g_engine.upload_mesh_data = real_upload;

    if (!ok || !new_mesh) {
        Log("[VARIANT-DIAG] B build failed on canonical path with final upload suppressed.");
        return false;
    }
    Log("[VARIANT-DIAG] B final UploadMeshDataImpl intentionally suppressed; canonical builder otherwise completed.");
    return true;
}

bool BuildTwinCloneVariant(
    const BemComponent& component, void* source_mesh, bool final_upload,
    void*& new_mesh) {
    new_mesh = CloneTwinMesh(source_mesh);
    if (!new_mesh) return false;
    if (!ResolveEngineBindings()) {
        DestroyUnityObject(new_mesh);
        new_mesh = nullptr;
        return false;
    }

    GenerationDiagRecord record{};
    record.component_id = component.info.component_id;
    record.original_mesh = source_mesh;
    record.custom_mesh = new_mesh;

    if (!RebuildGenerationMeshInPlace(record, component)) {
        DestroyUnityObject(new_mesh);
        new_mesh = nullptr;
        return false;
    }
    if (final_upload && !SafeUploadMeshData(new_mesh)) {
        DestroyUnityObject(new_mesh);
        new_mesh = nullptr;
        return false;
    }
    return true;
}

void BuildTwinPair() {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        Log("[VARIANT-DIAG] Num7 ignored: CustomModel is busy.");
        return;
    }
    const auto unlock = []() {
        g_operation_in_progress.store(false, std::memory_order_release);
    };

    if (!TwinBasePreflight("Num7")) {
        unlock();
        return;
    }

    if (!g_generation_diag_records.empty() && !DestroyGenerationDiagCache()) {
        Log("[VARIANT-DIAG] Num7 refused: previous generation cache could not be retired.");
        unlock();
        return;
    }
    if (!DestroyTwinSetIfDetached()) {
        unlock();
        return;
    }

    // SetSharedMesh retains diagnostic Meshes through the normal CustomModel
    // asset-retention path. At this point every diagnostic renderer is back on
    // an original Mesh, so clear that bookkeeping before starting a fresh
    // probability trial. Otherwise destroyed wrappers from older trials could
    // remain in g_retained_assets and pointer reuse would become a confounder.
    if (!g_retained_assets.empty()) {
        const size_t retained = g_retained_assets.size();
        ReleaseRetainedAssets(true);
        Log("[VARIANT-DIAG] cleared detached asset-retention records=" +
            std::to_string(retained) + " before fresh generation.");
    }

    BemPocData bem;
    const std::filesystem::path path = BemPath();
    if (path.empty() || !LoadBemPoc(path, bem)) {
        Log("[VARIANT-DIAG] Num7 failed: BEM payload unavailable.");
        unlock();
        return;
    }
    const BemComponent* component = TwinFindC9(bem);
    if (!component) {
        Log("[VARIANT-DIAG] Num7 failed: C9 replacement geometry is absent.");
        unlock();
        return;
    }

    const std::vector<RendererSummary> summaries =
        EnumerateLiveEndminfRenderers();
    const RendererSummary* source = TwinFindGameplayC9(summaries, *component);
    if (!source) {
        Log("[VARIANT-DIAG] Num7 failed: unique gameplay C9 renderer was not found.");
        unlock();
        return;
    }
    if (source->mesh_name.rfind("BetterEndfield.", 0) == 0) {
        Log("[VARIANT-DIAG] Num7 refused: C9 source already carries a runtime BetterEndfield Mesh.");
        unlock();
        return;
    }
    if (source->bones_count <= static_cast<int>(component->info.max_bone) ||
        !ValidateRendererSkin(*component, source->renderer, source->mesh)) {
        Log("[VARIANT-DIAG] Num7 failed: source C9 skin validation failed.");
        unlock();
        return;
    }

    std::array<void*, kTwinVariantCount> built{};

    // A: exact current production builder.
    if (!BuildMeshFromComponent(
            *component, source->mesh,
            built[TwinVariantIndex(TwinVariant::A_NewUpload)]) ||
        !built[TwinVariantIndex(TwinVariant::A_NewUpload)]) {
        Log("[VARIANT-DIAG] Num7 failed while building A:new+upload.");
        DestroyTwinMeshArray(built);
        unlock();
        return;
    }

    // B: exact same builder, except its final UploadMeshDataImpl is a no-op.
    if (!BuildTwinNewNoUpload(
            *component, source->mesh,
            built[TwinVariantIndex(TwinVariant::B_NewNoUpload)])) {
        DestroyTwinMeshArray(built);
        unlock();
        return;
    }

    // C/D: clone the original C9 Mesh object first, then overwrite the clone
    // with exactly the BEM declaration/streams/submesh/bindposes.
    if (!BuildTwinCloneVariant(
            *component, source->mesh, true,
            built[TwinVariantIndex(TwinVariant::C_CloneUpload)])) {
        Log("[VARIANT-DIAG] Num7 failed while building C:clone+overwrite+upload.");
        DestroyTwinMeshArray(built);
        unlock();
        return;
    }
    if (!BuildTwinCloneVariant(
            *component, source->mesh, false,
            built[TwinVariantIndex(TwinVariant::D_CloneNoUpload)])) {
        Log("[VARIANT-DIAG] Num7 failed while building D:clone+overwrite+no-upload.");
        DestroyTwinMeshArray(built);
        unlock();
        return;
    }

    g_twin = {};
    g_twin.generation = ++g_twin_next_generation;
    g_twin.renderer = source->renderer;
    g_twin.original_mesh = source->mesh;
    g_twin.meshes = built;
    g_twin.path = source->path;

    if (!PinTwinObjects()) {
        std::array<void*, kTwinVariantCount> doomed = g_twin.meshes;
        g_twin = {};
        DestroyTwinMeshArray(doomed);
        Log("[VARIANT-DIAG] Num7 failed to pin source/variant objects; set discarded.");
        unlock();
        return;
    }

    Log("[VARIANT-DIAG] build generation=" +
        std::to_string(g_twin.generation) + " complete frame=" +
        std::to_string(g_last_pump_frame) + " renderer=" +
        GenerationDiagPointer(g_twin.renderer) + " originalMesh=" +
        GenerationDiagPointer(g_twin.original_mesh) +
        " A=" + GenerationDiagPointer(g_twin.meshes[0]) +
        " B=" + GenerationDiagPointer(g_twin.meshes[1]) +
        " C=" + GenerationDiagPointer(g_twin.meshes[2]) +
        " D=" + GenerationDiagPointer(g_twin.meshes[3]) +
        " allDetached=1 meshesBuilt=4 path=\"" + g_twin.path + "\"");
    Log("[VARIANT-DIAG] Num8=A:new+upload, Num9=B:new+no-upload, Num4=C:clone+overwrite+upload, Num5=D:clone+overwrite+no-upload; Num0 restores original.");
    unlock();
}

void AttachTwin(TwinVariant variant) {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        Log(std::string("[VARIANT-DIAG] attach ignored: CustomModel is busy; variant=") +
            TwinVariantName(variant));
        return;
    }
    const auto unlock = []() {
        g_operation_in_progress.store(false, std::memory_order_release);
    };

    if (!TwinBasePreflight(TwinVariantName(variant)) || !TwinSetExists()) {
        if (!TwinSetExists()) {
            Log("[VARIANT-DIAG] no C9 variant set exists; press Num7 first.");
        }
        unlock();
        return;
    }
    if (!IsNativeObjectAlive(g_twin.renderer) ||
        !IsNativeObjectAlive(g_twin.original_mesh) ||
        std::any_of(g_twin.meshes.begin(), g_twin.meshes.end(),
            [](void* mesh) { return !IsNativeObjectAlive(mesh); })) {
        Log("[VARIANT-DIAG] variant set contains a dead object; build a fresh set with Num7.");
        unlock();
        return;
    }

    void* current = Invoke(
        Contract("skinned.get_shared_mesh"), g_twin.renderer, nullptr, false);
    if (current != g_twin.original_mesh) {
        Log("[VARIANT-DIAG] attach refused: C9 is not on originalMesh (current=" +
            GenerationDiagPointer(current) + "); press Num0 first.");
        unlock();
        return;
    }

    const size_t index = TwinVariantIndex(variant);
    void* target = g_twin.meshes[index];
    if (!SetSharedMesh(g_twin.renderer, target) ||
        Invoke(Contract("skinned.get_shared_mesh"), g_twin.renderer,
            nullptr, false) != target) {
        Log(std::string("[VARIANT-DIAG] attach failed for ") +
            TwinVariantName(variant) + "; restoring original C9 Mesh.");
        SetSharedMesh(g_twin.renderer, g_twin.original_mesh);
        unlock();
        return;
    }

    ++g_twin.attach_count[index];
    if (g_twin.first_use_order[index] == 0) {
        g_twin.first_use_order[index] = ++g_twin.first_use_sequence;
    }

    Log("[VARIANT-DIAG] attach generation=" +
        std::to_string(g_twin.generation) + " variant=" +
        TwinVariantName(variant) + " mesh=" + GenerationDiagPointer(target) +
        " attachCount=" + std::to_string(g_twin.attach_count[index]) +
        " firstUseOrder=" + std::to_string(g_twin.first_use_order[index]) +
        " setFirstUseSequence=" + std::to_string(g_twin.first_use_sequence) +
        " meshesBuilt=0 meshesDestroyed=0.");
    unlock();
}

void RestoreTwin() {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        Log("[VARIANT-DIAG] Num0 ignored: CustomModel is busy.");
        return;
    }
    const auto unlock = []() {
        g_operation_in_progress.store(false, std::memory_order_release);
    };

    if (!TwinSetExists()) {
        Log("[VARIANT-DIAG] Num0: no variant set exists; C9 left unchanged.");
        unlock();
        return;
    }
    if (!IsNativeObjectAlive(g_twin.renderer) ||
        !IsNativeObjectAlive(g_twin.original_mesh)) {
        Log("[VARIANT-DIAG] Num0 failed: renderer/original Mesh is dead.");
        unlock();
        return;
    }

    void* current = Invoke(
        Contract("skinned.get_shared_mesh"), g_twin.renderer, nullptr, false);
    if (current == g_twin.original_mesh) {
        Log("[VARIANT-DIAG] Num0: C9 already uses originalMesh; A/B/C/D retained.");
        unlock();
        return;
    }
    if (!IsTwinVariantMesh(current)) {
        Log("[VARIANT-DIAG] Num0 refused: renderer carries a foreign Mesh current=" +
            GenerationDiagPointer(current));
        unlock();
        return;
    }

    if (!SetSharedMesh(g_twin.renderer, g_twin.original_mesh) ||
        Invoke(Contract("skinned.get_shared_mesh"), g_twin.renderer,
            nullptr, false) != g_twin.original_mesh) {
        Log("[VARIANT-DIAG] Num0 failed to restore original C9 Mesh.");
        unlock();
        return;
    }

    Log("[VARIANT-DIAG] restore generation=" +
        std::to_string(g_twin.generation) +
        " complete: originalMesh restored, A/B/C/D retained, meshesBuilt=0 meshesDestroyed=0.");
    unlock();
}

DWORD WINAPI TwinHotkeyThread(void*) {
    struct Latch {
        int key;
        std::atomic_bool* flag;
        bool held = false;
    };
    std::array<Latch, 6> latches{{
        {kTwinBuildHotkey, &g_twin_build_requested, false},
        {kTwinAttachAHotkey, &g_twin_attach_a_requested, false},
        {kTwinAttachBHotkey, &g_twin_attach_b_requested, false},
        {kTwinAttachCHotkey, &g_twin_attach_c_requested, false},
        {kTwinAttachDHotkey, &g_twin_attach_d_requested, false},
        {kTwinRestoreHotkey, &g_twin_restore_requested, false},
    }};

    while (!g_twin_hotkey_stop.load(std::memory_order_acquire)) {
        for (auto& latch : latches) {
            const bool down = (GetAsyncKeyState(latch.key) & 0x8000) != 0;
            if (down && !latch.held) {
                latch.flag->store(true, std::memory_order_release);
            }
            latch.held = down;
        }
        Sleep(15);
    }
    return 0;
}

bool StartTwinHotkeys() {
    g_twin_hotkey_stop.store(false, std::memory_order_release);
    g_twin_hotkey_thread =
        CreateThread(nullptr, 0, &TwinHotkeyThread, nullptr, 0, nullptr);
    return g_twin_hotkey_thread != nullptr;
}

void StopTwinHotkeys() {
    g_twin_hotkey_stop.store(true, std::memory_order_release);
    if (!g_twin_hotkey_thread) return;
    WaitForSingleObject(g_twin_hotkey_thread, 1000);
    CloseHandle(g_twin_hotkey_thread);
    g_twin_hotkey_thread = nullptr;
}

void PumpTwinDiag() {
    const int32_t frame = g_last_pump_frame;
    if (frame < 0 || frame == g_twin_last_frame) return;
    g_twin_last_frame = frame;

    if (g_twin_restore_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[VARIANT-DIAG] Num0 restore request consumed on Unity main thread.");
        RestoreTwin();
        return;
    }
    if (g_twin_build_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[VARIANT-DIAG] Num7 build-variants request consumed on Unity main thread.");
        BuildTwinPair();
        return;
    }
    if (g_twin_attach_a_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[VARIANT-DIAG] Num8 attach A:new+upload request consumed on Unity main thread.");
        AttachTwin(TwinVariant::A_NewUpload);
        return;
    }
    if (g_twin_attach_b_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[VARIANT-DIAG] Num9 attach B:new+no-upload request consumed on Unity main thread.");
        AttachTwin(TwinVariant::B_NewNoUpload);
        return;
    }
    if (g_twin_attach_c_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[VARIANT-DIAG] Num4 attach C:clone+overwrite+upload request consumed on Unity main thread.");
        AttachTwin(TwinVariant::C_CloneUpload);
        return;
    }
    if (g_twin_attach_d_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[VARIANT-DIAG] Num5 attach D:clone+overwrite+no-upload request consumed on Unity main thread.");
        AttachTwin(TwinVariant::D_CloneNoUpload);
    }
}

void __fastcall DetourPumpWithTwinDiag(void* method_info) {
    DetourPumpWithGenerationRecommitDiag(method_info);
    PumpTwinDiag();
}

BE_Result BE_CALL TwinCreateHook(
    void* context, const char* module_id, void* target, void* detour,
    void** original) {
    if (!g_twin_real_host || !g_twin_real_host->create_hook) {
        return BE_Result_NotReady;
    }
    if (detour == reinterpret_cast<void*>(&DetourPumpWithGenerationRecommitDiag)) {
        g_twin_pump_intercepted = true;
        return g_twin_real_host->create_hook(
            context, module_id, target,
            reinterpret_cast<void*>(&DetourPumpWithTwinDiag), original);
    }
    return g_twin_real_host->create_hook(
        context, module_id, target, detour, original);
}

BE_Result BE_CALL InitializeWithTwinDiag(const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;
    g_twin_real_host = host;
    g_twin_host_proxy = *host;
    g_twin_host_proxy.create_hook = &TwinCreateHook;
    g_twin_pump_intercepted = false;
    g_twin_last_frame = -1;
    g_twin_clone_method_info = nullptr;
    g_twin_clone_method_name = nullptr;

    g_twin_build_requested.store(false, std::memory_order_release);
    g_twin_attach_a_requested.store(false, std::memory_order_release);
    g_twin_attach_b_requested.store(false, std::memory_order_release);
    g_twin_attach_c_requested.store(false, std::memory_order_release);
    g_twin_attach_d_requested.store(false, std::memory_order_release);
    g_twin_restore_requested.store(false, std::memory_order_release);

    const BE_Result result =
        InitializeWithGenerationRecommitDiag(&g_twin_host_proxy);
    if (result != BE_Result_Ok) {
        g_twin_real_host = nullptr;
        return result;
    }
    if (!g_twin_pump_intercepted) {
        Log("[VARIANT-DIAG] failed to intercept recommit diagnostic pump; controls disabled.");
        ShutdownWithGenerationRecommitDiag();
        g_twin_real_host = nullptr;
        return BE_Result_Failed;
    }
    if (!StartTwinHotkeys()) {
        Log("[VARIANT-DIAG] failed to start variant diagnostic hotkey thread.");
        ShutdownWithGenerationRecommitDiag();
        g_twin_real_host = nullptr;
        return BE_Result_Failed;
    }

    Log("[VARIANT-DIAG] C9 construction controls ready: Num7=build four detached variants; Num8=A new+upload; Num9=B new+no-upload; Num4=C clone+overwrite+upload; Num5=D clone+overwrite+no-upload; Num0=restore original.");
    Log("[VARIANT-DIAG] restore with Num0 before switching variants; repeat several fresh Num7 generations to compare failure rates by construction path.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithTwinDiag() {
    StopTwinHotkeys();
    g_twin_build_requested.store(false, std::memory_order_release);
    g_twin_attach_a_requested.store(false, std::memory_order_release);
    g_twin_attach_b_requested.store(false, std::memory_order_release);
    g_twin_attach_c_requested.store(false, std::memory_order_release);
    g_twin_attach_d_requested.store(false, std::memory_order_release);
    g_twin_restore_requested.store(false, std::memory_order_release);

    if (TwinSetExists() && IsNativeObjectAlive(g_twin.renderer) &&
        IsNativeObjectAlive(g_twin.original_mesh)) {
        void* current = Invoke(
            Contract("skinned.get_shared_mesh"), g_twin.renderer, nullptr, false);
        if (IsTwinVariantMesh(current)) {
            SetSharedMesh(g_twin.renderer, g_twin.original_mesh);
        }
    }
    if (TwinSetExists() && !TwinMeshesInUse()) {
        std::array<void*, kTwinVariantCount> doomed = g_twin.meshes;
        DestroyTwinMeshArray(doomed);
    }
    ReleaseTwinHandles();
    g_twin = {};
    g_twin_last_frame = -1;
    g_twin_clone_method_info = nullptr;
    g_twin_clone_method_name = nullptr;

    ShutdownWithGenerationRecommitDiag();
    g_twin_real_host = nullptr;
    g_twin_host_proxy = {};
}
