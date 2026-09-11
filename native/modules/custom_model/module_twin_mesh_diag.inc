// C9 twin-Mesh diagnostic for separating Mesh construction from first-use HG
// registration. Two byte-identical runtime Mesh objects are built from the same
// source renderer in the same pump frame, but neither is attached during build.
//
// Numpad 7 : build a fresh C9 pair A/B, both detached.
// Numpad 8 : attach A (requires the renderer to be on the original Mesh).
// Numpad 9 : attach B (same requirement).
// Numpad 0 : restore the original C9 Mesh while retaining A/B.
//
// Run one pair A-first and a fresh pair B-first. The first-use order is logged
// independently from object identity so the visual result can distinguish a
// per-Mesh construction fault from an HG first-use registration race.

constexpr int kTwinBuildHotkey = VK_NUMPAD7;
constexpr int kTwinAttachAHotkey = VK_NUMPAD8;
constexpr int kTwinAttachBHotkey = VK_NUMPAD9;
constexpr int kTwinRestoreHotkey = VK_NUMPAD0;
constexpr uint32_t kTwinComponentId = 9;

std::atomic_bool g_twin_build_requested{false};
std::atomic_bool g_twin_attach_a_requested{false};
std::atomic_bool g_twin_attach_b_requested{false};
std::atomic_bool g_twin_restore_requested{false};
std::atomic_bool g_twin_hotkey_stop{false};
HANDLE g_twin_hotkey_thread = nullptr;
int32_t g_twin_last_frame = -1;

const BE_HostApiV1* g_twin_real_host = nullptr;
BE_HostApiV1 g_twin_host_proxy{};
bool g_twin_pump_intercepted = false;

struct TwinMeshDiagState {
    uint64_t generation = 0;
    void* renderer = nullptr;
    void* original_mesh = nullptr;
    void* mesh_a = nullptr;
    void* mesh_b = nullptr;
    uint32_t renderer_handle = 0;
    uint32_t original_handle = 0;
    uint32_t mesh_a_handle = 0;
    uint32_t mesh_b_handle = 0;
    std::string path;
    uint32_t first_use_sequence = 0;
    uint32_t a_first_use_order = 0;
    uint32_t b_first_use_order = 0;
    uint32_t a_attach_count = 0;
    uint32_t b_attach_count = 0;
};
TwinMeshDiagState g_twin{};
uint64_t g_twin_next_generation = 0;

void ReleaseTwinHandles() {
    if (!g_host || !g_host->gchandle_free) return;
    if (g_twin.renderer_handle) {
        g_host->gchandle_free(g_host->context, g_twin.renderer_handle);
    }
    if (g_twin.original_handle) {
        g_host->gchandle_free(g_host->context, g_twin.original_handle);
    }
    if (g_twin.mesh_a_handle) {
        g_host->gchandle_free(g_host->context, g_twin.mesh_a_handle);
    }
    if (g_twin.mesh_b_handle) {
        g_host->gchandle_free(g_host->context, g_twin.mesh_b_handle);
    }
    g_twin.renderer_handle = 0;
    g_twin.original_handle = 0;
    g_twin.mesh_a_handle = 0;
    g_twin.mesh_b_handle = 0;
}

bool PinTwinObjects() {
    if (!g_host || !g_host->gchandle_new || !g_host->gchandle_free ||
        !g_twin.renderer || !g_twin.original_mesh || !g_twin.mesh_a ||
        !g_twin.mesh_b) {
        return false;
    }
    g_twin.renderer_handle =
        g_host->gchandle_new(g_host->context, g_twin.renderer, 0);
    g_twin.original_handle =
        g_host->gchandle_new(g_host->context, g_twin.original_mesh, 0);
    g_twin.mesh_a_handle =
        g_host->gchandle_new(g_host->context, g_twin.mesh_a, 0);
    g_twin.mesh_b_handle =
        g_host->gchandle_new(g_host->context, g_twin.mesh_b, 0);
    if (!g_twin.renderer_handle || !g_twin.original_handle ||
        !g_twin.mesh_a_handle || !g_twin.mesh_b_handle) {
        ReleaseTwinHandles();
        return false;
    }
    return true;
}

bool TwinPairExists() {
    return g_twin.renderer && g_twin.original_mesh &&
        g_twin.mesh_a && g_twin.mesh_b;
}

bool TwinMeshesInUse() {
    if (!g_twin.mesh_a && !g_twin.mesh_b) return false;
    std::vector<RendererSummary> live;
    if (!SnapshotRendererMeshes(live)) return true;
    for (const auto& item : live) {
        if (item.mesh == g_twin.mesh_a || item.mesh == g_twin.mesh_b) {
            return true;
        }
    }
    return false;
}

bool DestroyTwinPairIfDetached() {
    if (!TwinPairExists()) {
        ReleaseTwinHandles();
        g_twin = {};
        return true;
    }
    if (TwinMeshesInUse()) {
        Log("[TWIN-DIAG] old pair is still bound somewhere; press Num0 before building a new pair.");
        return false;
    }
    if (IsNativeObjectAlive(g_twin.mesh_a)) DestroyUnityObject(g_twin.mesh_a);
    if (IsNativeObjectAlive(g_twin.mesh_b)) DestroyUnityObject(g_twin.mesh_b);
    const uint64_t retired = g_twin.generation;
    ReleaseTwinHandles();
    g_twin = {};
    Log("[TWIN-DIAG] retired detached pair generation=" +
        std::to_string(retired) + " meshesDestroyed=2.");
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
            Log("[TWIN-DIAG] more than one gameplay C9 candidate is live; refusing an ambiguous twin test.");
            return nullptr;
        }
        candidate = &item;
    }
    return candidate;
}

bool TwinBasePreflight(const char* action) {
    if (!GenerationDiagOfficialPathIdle()) {
        Log(std::string("[TWIN-DIAG] ") + action +
            " refused: normal F12 replacement is active/pending; use F11 first.");
        return false;
    }
    if (g_generation_diag_attached) {
        Log(std::string("[TWIN-DIAG] ") + action +
            " refused: cached-generation diagnostic is attached; press main '=' first.");
        return false;
    }
    return true;
}

void BuildTwinPair() {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        Log("[TWIN-DIAG] Num7 ignored: CustomModel is busy.");
        return;
    }
    const auto unlock = []() {
        g_operation_in_progress.store(false, std::memory_order_release);
    };

    if (!TwinBasePreflight("Num7")) {
        unlock();
        return;
    }

    // Retire the older generation diagnostic cache so only one diagnostic owns
    // runtime custom Mesh objects for this renderer. DestroyGenerationDiagCache
    // is safe only while detached and verifies that condition itself.
    if (!g_generation_diag_records.empty() && !DestroyGenerationDiagCache()) {
        Log("[TWIN-DIAG] Num7 refused: previous generation cache could not be retired.");
        unlock();
        return;
    }
    if (!DestroyTwinPairIfDetached()) {
        unlock();
        return;
    }

    BemPocData bem;
    const std::filesystem::path path = BemPath();
    if (path.empty() || !LoadBemPoc(path, bem)) {
        Log("[TWIN-DIAG] Num7 failed: BEM payload unavailable.");
        unlock();
        return;
    }
    const BemComponent* component = TwinFindC9(bem);
    if (!component) {
        Log("[TWIN-DIAG] Num7 failed: C9 replacement geometry is absent.");
        unlock();
        return;
    }

    const std::vector<RendererSummary> summaries =
        EnumerateLiveEndminfRenderers();
    const RendererSummary* source = TwinFindGameplayC9(summaries, *component);
    if (!source) {
        Log("[TWIN-DIAG] Num7 failed: unique gameplay C9 renderer was not found.");
        unlock();
        return;
    }
    if (source->mesh_name.rfind("BetterEndfield.", 0) == 0) {
        Log("[TWIN-DIAG] Num7 refused: C9 source already carries a runtime BetterEndfield Mesh.");
        unlock();
        return;
    }
    if (source->bones_count <= static_cast<int>(component->info.max_bone) ||
        !ValidateRendererSkin(*component, source->renderer, source->mesh)) {
        Log("[TWIN-DIAG] Num7 failed: source C9 skin validation failed.");
        unlock();
        return;
    }

    void* mesh_a = nullptr;
    void* mesh_b = nullptr;
    if (!BuildMeshFromComponent(*component, source->mesh, mesh_a) || !mesh_a) {
        Log("[TWIN-DIAG] Num7 failed while building C9-A.");
        unlock();
        return;
    }
    if (!BuildMeshFromComponent(*component, source->mesh, mesh_b) || !mesh_b) {
        DestroyUnityObject(mesh_a);
        Log("[TWIN-DIAG] Num7 failed while building C9-B; C9-A discarded.");
        unlock();
        return;
    }

    g_twin = {};
    g_twin.generation = ++g_twin_next_generation;
    g_twin.renderer = source->renderer;
    g_twin.original_mesh = source->mesh;
    g_twin.mesh_a = mesh_a;
    g_twin.mesh_b = mesh_b;
    g_twin.path = source->path;
    if (!PinTwinObjects()) {
        DestroyUnityObject(mesh_a);
        DestroyUnityObject(mesh_b);
        g_twin = {};
        Log("[TWIN-DIAG] Num7 failed to pin A/B/source objects; pair discarded.");
        unlock();
        return;
    }

    Log("[TWIN-DIAG] build generation=" +
        std::to_string(g_twin.generation) + " complete frame=" +
        std::to_string(g_last_pump_frame) + " renderer=" +
        GenerationDiagPointer(g_twin.renderer) + " originalMesh=" +
        GenerationDiagPointer(g_twin.original_mesh) + " A=" +
        GenerationDiagPointer(g_twin.mesh_a) + " B=" +
        GenerationDiagPointer(g_twin.mesh_b) +
        " bothDetached=1 meshesBuilt=2 path=\"" + g_twin.path + "\"");
    Log("[TWIN-DIAG] choose first use explicitly: Num8=A or Num9=B; restore with Num0 before testing the other twin.");
    unlock();
}

void AttachTwin(bool use_a) {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        Log(std::string("[TWIN-DIAG] ") + (use_a ? "Num8" : "Num9") +
            " ignored: CustomModel is busy.");
        return;
    }
    const auto unlock = []() {
        g_operation_in_progress.store(false, std::memory_order_release);
    };
    const char side = use_a ? 'A' : 'B';
    if (!TwinBasePreflight(use_a ? "Num8" : "Num9") || !TwinPairExists()) {
        if (!TwinPairExists()) {
            Log("[TWIN-DIAG] no C9 twin pair exists; press Num7 first.");
        }
        unlock();
        return;
    }
    if (!IsNativeObjectAlive(g_twin.renderer) ||
        !IsNativeObjectAlive(g_twin.original_mesh) ||
        !IsNativeObjectAlive(g_twin.mesh_a) ||
        !IsNativeObjectAlive(g_twin.mesh_b)) {
        Log("[TWIN-DIAG] pair contains a dead object; build a fresh pair with Num7.");
        unlock();
        return;
    }

    void* current = Invoke(Contract("skinned.get_shared_mesh"),
        g_twin.renderer, nullptr, false);
    if (current != g_twin.original_mesh) {
        Log("[TWIN-DIAG] attach refused: C9 is not on originalMesh (current=" +
            GenerationDiagPointer(current) + "); press Num0 before switching twins.");
        unlock();
        return;
    }

    void* target = use_a ? g_twin.mesh_a : g_twin.mesh_b;
    if (!SetSharedMesh(g_twin.renderer, target) ||
        Invoke(Contract("skinned.get_shared_mesh"), g_twin.renderer,
            nullptr, false) != target) {
        Log(std::string("[TWIN-DIAG] attach ") + side +
            " failed; restoring original C9 Mesh.");
        SetSharedMesh(g_twin.renderer, g_twin.original_mesh);
        unlock();
        return;
    }

    uint32_t& attach_count = use_a ? g_twin.a_attach_count : g_twin.b_attach_count;
    uint32_t& first_order = use_a ? g_twin.a_first_use_order : g_twin.b_first_use_order;
    ++attach_count;
    if (first_order == 0) {
        first_order = ++g_twin.first_use_sequence;
    }

    Log(std::string("[TWIN-DIAG] attach generation=") +
        std::to_string(g_twin.generation) + " side=" + side + " mesh=" +
        GenerationDiagPointer(target) + " attachCount=" +
        std::to_string(attach_count) + " firstUseOrder=" +
        std::to_string(first_order) + " pairFirstUseSequence=" +
        std::to_string(g_twin.first_use_sequence) +
        " meshesBuilt=0 meshesDestroyed=0.");
    unlock();
}

void RestoreTwin() {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        Log("[TWIN-DIAG] Num0 ignored: CustomModel is busy.");
        return;
    }
    const auto unlock = []() {
        g_operation_in_progress.store(false, std::memory_order_release);
    };
    if (!TwinPairExists()) {
        Log("[TWIN-DIAG] Num0: no pair exists; C9 left unchanged.");
        unlock();
        return;
    }
    if (!IsNativeObjectAlive(g_twin.renderer) ||
        !IsNativeObjectAlive(g_twin.original_mesh)) {
        Log("[TWIN-DIAG] Num0 failed: renderer/original Mesh is dead.");
        unlock();
        return;
    }
    void* current = Invoke(Contract("skinned.get_shared_mesh"),
        g_twin.renderer, nullptr, false);
    if (current == g_twin.original_mesh) {
        Log("[TWIN-DIAG] Num0: C9 already uses originalMesh; A/B retained.");
        unlock();
        return;
    }
    if (current != g_twin.mesh_a && current != g_twin.mesh_b) {
        Log("[TWIN-DIAG] Num0 refused: renderer owns a foreign Mesh=" +
            GenerationDiagPointer(current) + ".");
        unlock();
        return;
    }
    if (!SetSharedMesh(g_twin.renderer, g_twin.original_mesh) ||
        Invoke(Contract("skinned.get_shared_mesh"), g_twin.renderer,
            nullptr, false) != g_twin.original_mesh) {
        Log("[TWIN-DIAG] Num0 failed to restore original C9 Mesh.");
        unlock();
        return;
    }
    Log("[TWIN-DIAG] restore generation=" +
        std::to_string(g_twin.generation) +
        " complete: originalMesh restored, A/B retained, meshesBuilt=0 meshesDestroyed=0.");
    unlock();
}

DWORD WINAPI TwinHotkeyThread(void*) {
    struct Latch {
        int key;
        std::atomic_bool* flag;
        bool held = false;
    };
    std::array<Latch, 4> latches{{
        {kTwinBuildHotkey, &g_twin_build_requested, false},
        {kTwinAttachAHotkey, &g_twin_attach_a_requested, false},
        {kTwinAttachBHotkey, &g_twin_attach_b_requested, false},
        {kTwinRestoreHotkey, &g_twin_restore_requested, false}}};
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
        Log("[TWIN-DIAG] Num0 restore request consumed on Unity main thread.");
        RestoreTwin();
        return;
    }
    if (g_twin_build_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[TWIN-DIAG] Num7 build-pair request consumed on Unity main thread.");
        BuildTwinPair();
        return;
    }
    if (g_twin_attach_a_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[TWIN-DIAG] Num8 attach-A request consumed on Unity main thread.");
        AttachTwin(true);
        return;
    }
    if (g_twin_attach_b_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[TWIN-DIAG] Num9 attach-B request consumed on Unity main thread.");
        AttachTwin(false);
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
    g_twin_build_requested.store(false, std::memory_order_release);
    g_twin_attach_a_requested.store(false, std::memory_order_release);
    g_twin_attach_b_requested.store(false, std::memory_order_release);
    g_twin_restore_requested.store(false, std::memory_order_release);

    const BE_Result result =
        InitializeWithGenerationRecommitDiag(&g_twin_host_proxy);
    if (result != BE_Result_Ok) {
        g_twin_real_host = nullptr;
        return result;
    }
    if (!g_twin_pump_intercepted) {
        Log("[TWIN-DIAG] failed to intercept recommit diagnostic pump; twin controls disabled.");
        ShutdownWithGenerationRecommitDiag();
        g_twin_real_host = nullptr;
        return BE_Result_Failed;
    }
    if (!StartTwinHotkeys()) {
        Log("[TWIN-DIAG] failed to start twin diagnostic hotkey thread.");
        ShutdownWithGenerationRecommitDiag();
        g_twin_real_host = nullptr;
        return BE_Result_Failed;
    }

    Log("[TWIN-DIAG] C9 twin controls ready: Num7=build detached A/B, Num8=attach A, Num9=attach B, Num0=restore original while retaining pair.");
    Log("[TWIN-DIAG] test one fresh pair A-first and the next fresh pair B-first; first-use order is logged for each Mesh identity.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithTwinDiag() {
    StopTwinHotkeys();
    g_twin_build_requested.store(false, std::memory_order_release);
    g_twin_attach_a_requested.store(false, std::memory_order_release);
    g_twin_attach_b_requested.store(false, std::memory_order_release);
    g_twin_restore_requested.store(false, std::memory_order_release);

    if (TwinPairExists() && IsNativeObjectAlive(g_twin.renderer) &&
        IsNativeObjectAlive(g_twin.original_mesh)) {
        void* current = Invoke(Contract("skinned.get_shared_mesh"),
            g_twin.renderer, nullptr, false);
        if (current == g_twin.mesh_a || current == g_twin.mesh_b) {
            SetSharedMesh(g_twin.renderer, g_twin.original_mesh);
        }
    }
    if (TwinPairExists() && !TwinMeshesInUse()) {
        if (IsNativeObjectAlive(g_twin.mesh_a)) DestroyUnityObject(g_twin.mesh_a);
        if (IsNativeObjectAlive(g_twin.mesh_b)) DestroyUnityObject(g_twin.mesh_b);
    }
    ReleaseTwinHandles();
    g_twin = {};
    g_twin_last_frame = -1;

    ShutdownWithGenerationRecommitDiag();
    g_twin_real_host = nullptr;
    g_twin_host_proxy = {};
}
