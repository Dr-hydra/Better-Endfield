// C9 pair-mode diagnostic layered over the earlier A/B twin experiment.
//
// The A(upload)/B(no-upload) test showed that rare visual failures occur as a
// pair. This layer changes the unit under test from one Mesh to one build batch:
//
//   Num7 : build U/U -- A and B both use the canonical final UploadMeshDataImpl
//   Num1 : build N/N -- A and B both suppress that final UploadMeshDataImpl
//   Num8 : attach A from the current pair
//   Num9 : attach B from the current pair
//   Num0 : restore original C9 while retaining the current pair
//
// Both Meshes are still built in the same Unity-main-thread pump frame. This
// test therefore asks whether the presence of any final UploadMeshDataImpl in
// the build batch correlates with the already-observed pairwise failure.
// GraphicsBuffer and Object clone APIs are not used.

constexpr int kPairBuildNoUploadHotkey = VK_NUMPAD1;

std::atomic_bool g_pair_build_nn_requested{false};
std::atomic_bool g_pair_hotkey_stop{false};
HANDLE g_pair_hotkey_thread = nullptr;

const BE_HostApiV1* g_pair_real_host = nullptr;
BE_HostApiV1 g_pair_host_proxy{};
bool g_pair_pump_intercepted = false;

enum class PairBuildMode {
    None,
    UploadUpload,
    NoUploadNoUpload,
};

PairBuildMode g_pair_build_mode = PairBuildMode::None;

const char* PairBuildModeName(PairBuildMode mode) {
    switch (mode) {
    case PairBuildMode::UploadUpload:
        return "U/U";
    case PairBuildMode::NoUploadNoUpload:
        return "N/N";
    default:
        return "none";
    }
}

const char* PairVariantName(bool use_a) {
    if (g_pair_build_mode == PairBuildMode::UploadUpload) {
        return use_a ? "A:new+upload" : "B:new+upload";
    }
    if (g_pair_build_mode == PairBuildMode::NoUploadNoUpload) {
        return use_a ? "A:new+no-upload" : "B:new+no-upload";
    }
    return use_a ? "A:unknown" : "B:unknown";
}

bool BuildPairMeshNoUpload(
    const BemComponent& component, void* source_mesh, const char* side,
    void*& new_mesh) {
    new_mesh = nullptr;
    if (!ResolveEngineBindings() || !g_engine.upload_mesh_data) {
        Log(std::string("[PAIR-MODE] ") + side +
            " N/N build failed: UploadMeshData binding unavailable.");
        return false;
    }

    const MeshUploadDataFn real_upload = g_engine.upload_mesh_data;
    g_engine.upload_mesh_data = &TwinNoopUploadMeshData;
    const bool ok = BuildMeshFromComponent(component, source_mesh, new_mesh);
    g_engine.upload_mesh_data = real_upload;

    if (!ok || !new_mesh) {
        Log(std::string("[PAIR-MODE] ") + side +
            " N/N build failed on canonical path with final upload suppressed.");
        return false;
    }

    Log(std::string("[PAIR-MODE] ") + side +
        " final UploadMeshDataImpl intentionally suppressed; canonical builder otherwise completed.");
    return true;
}

bool PreparePairBuildSource(
    PairBuildMode mode, const BemComponent*& component,
    const RendererSummary*& source, BemPocData& bem,
    std::vector<RendererSummary>& summaries) {
    component = nullptr;
    source = nullptr;

    const char* mode_name = PairBuildModeName(mode);
    if (!TwinBasePreflight(mode_name)) return false;

    if (!g_generation_diag_records.empty() && !DestroyGenerationDiagCache()) {
        Log(std::string("[PAIR-MODE] ") + mode_name +
            " refused: previous generation cache could not be retired.");
        return false;
    }
    if (!DestroyTwinPairIfDetached()) return false;
    g_pair_build_mode = PairBuildMode::None;

    if (!g_retained_assets.empty()) {
        const size_t retained = g_retained_assets.size();
        ReleaseRetainedAssets(true);
        Log("[PAIR-MODE] cleared detached asset-retention records=" +
            std::to_string(retained) + " before fresh pair.");
    }

    const std::filesystem::path path = BemPath();
    if (path.empty() || !LoadBemPoc(path, bem)) {
        Log(std::string("[PAIR-MODE] ") + mode_name +
            " build failed: BEM payload unavailable.");
        return false;
    }

    component = TwinFindC9(bem);
    if (!component) {
        Log(std::string("[PAIR-MODE] ") + mode_name +
            " build failed: C9 replacement geometry is absent.");
        return false;
    }

    summaries = EnumerateLiveEndminfRenderers();
    source = TwinFindGameplayC9(summaries, *component);
    if (!source) {
        Log(std::string("[PAIR-MODE] ") + mode_name +
            " build failed: unique gameplay C9 renderer was not found.");
        return false;
    }
    if (source->mesh_name.rfind("BetterEndfield.", 0) == 0) {
        Log(std::string("[PAIR-MODE] ") + mode_name +
            " refused: C9 source already carries a runtime BetterEndfield Mesh.");
        return false;
    }
    if (source->bones_count <= static_cast<int>(component->info.max_bone) ||
        !ValidateRendererSkin(*component, source->renderer, source->mesh)) {
        Log(std::string("[PAIR-MODE] ") + mode_name +
            " build failed: source C9 skin validation failed.");
        return false;
    }
    return true;
}

void BuildPairMode(PairBuildMode mode, const char* request_key) {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        Log(std::string("[PAIR-MODE] ") + request_key +
            " ignored: CustomModel is busy.");
        return;
    }
    const auto unlock = []() {
        g_operation_in_progress.store(false, std::memory_order_release);
    };

    BemPocData bem;
    std::vector<RendererSummary> summaries;
    const BemComponent* component = nullptr;
    const RendererSummary* source = nullptr;
    if (!PreparePairBuildSource(mode, component, source, bem, summaries)) {
        unlock();
        return;
    }

    void* mesh_a = nullptr;
    void* mesh_b = nullptr;
    bool a_ok = false;
    bool b_ok = false;

    if (mode == PairBuildMode::UploadUpload) {
        a_ok = BuildMeshFromComponent(*component, source->mesh, mesh_a) && mesh_a;
        if (a_ok) {
            b_ok = BuildMeshFromComponent(*component, source->mesh, mesh_b) && mesh_b;
        }
    } else if (mode == PairBuildMode::NoUploadNoUpload) {
        a_ok = BuildPairMeshNoUpload(*component, source->mesh, "A", mesh_a);
        if (a_ok) {
            b_ok = BuildPairMeshNoUpload(*component, source->mesh, "B", mesh_b);
        }
    }

    if (!a_ok || !b_ok) {
        if (mesh_a && IsNativeObjectAlive(mesh_a)) DestroyUnityObject(mesh_a);
        if (mesh_b && IsNativeObjectAlive(mesh_b)) DestroyUnityObject(mesh_b);
        Log(std::string("[PAIR-MODE] ") + PairBuildModeName(mode) +
            " build failed; partial pair discarded.");
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
    g_pair_build_mode = mode;

    if (!PinTwinObjects()) {
        if (IsNativeObjectAlive(mesh_a)) DestroyUnityObject(mesh_a);
        if (IsNativeObjectAlive(mesh_b)) DestroyUnityObject(mesh_b);
        g_twin = {};
        g_pair_build_mode = PairBuildMode::None;
        Log(std::string("[PAIR-MODE] ") + PairBuildModeName(mode) +
            " failed to pin pair/source objects; pair discarded.");
        unlock();
        return;
    }

    Log(std::string("[PAIR-MODE] build generation=") +
        std::to_string(g_twin.generation) + " mode=" +
        PairBuildModeName(mode) + " frame=" +
        std::to_string(g_last_pump_frame) + " renderer=" +
        GenerationDiagPointer(g_twin.renderer) + " originalMesh=" +
        GenerationDiagPointer(g_twin.original_mesh) + " A=" +
        GenerationDiagPointer(g_twin.mesh_a) + " B=" +
        GenerationDiagPointer(g_twin.mesh_b) +
        " bothDetached=1 meshesBuilt=2 path=\"" + g_twin.path + "\"");
    Log(std::string("[PAIR-MODE] generation=") +
        std::to_string(g_twin.generation) + " mode=" + PairBuildModeName(mode) +
        (mode == PairBuildMode::UploadUpload
            ? " finalUploadsExecuted=2."
            : " finalUploadsExecuted=0 finalUploadsSuppressed=2."));
    unlock();
}

void AttachPairVariant(bool use_a) {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        Log(std::string("[PAIR-MODE] ") + (use_a ? "Num8" : "Num9") +
            " ignored: CustomModel is busy.");
        return;
    }
    const auto unlock = []() {
        g_operation_in_progress.store(false, std::memory_order_release);
    };

    if (!TwinBasePreflight(use_a ? "Num8" : "Num9") || !TwinPairExists() ||
        g_pair_build_mode == PairBuildMode::None) {
        if (!TwinPairExists() || g_pair_build_mode == PairBuildMode::None) {
            Log("[PAIR-MODE] no pair-mode C9 pair exists; press Num7 (U/U) or Num1 (N/N) first.");
        }
        unlock();
        return;
    }
    if (!IsNativeObjectAlive(g_twin.renderer) ||
        !IsNativeObjectAlive(g_twin.original_mesh) ||
        !IsNativeObjectAlive(g_twin.mesh_a) ||
        !IsNativeObjectAlive(g_twin.mesh_b)) {
        Log("[PAIR-MODE] pair contains a dead object; build a fresh pair.");
        unlock();
        return;
    }

    void* current = Invoke(Contract("skinned.get_shared_mesh"),
        g_twin.renderer, nullptr, false);
    if (current != g_twin.original_mesh) {
        Log("[PAIR-MODE] attach refused: C9 is not on originalMesh (current=" +
            GenerationDiagPointer(current) + "); press Num0 before switching A/B.");
        unlock();
        return;
    }

    void* target = use_a ? g_twin.mesh_a : g_twin.mesh_b;
    const char* variant = PairVariantName(use_a);
    if (!SetSharedMesh(g_twin.renderer, target) ||
        Invoke(Contract("skinned.get_shared_mesh"), g_twin.renderer,
            nullptr, false) != target) {
        Log(std::string("[PAIR-MODE] attach failed for ") + variant +
            "; restoring original C9 Mesh.");
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

    Log(std::string("[PAIR-MODE] attach generation=") +
        std::to_string(g_twin.generation) + " mode=" +
        PairBuildModeName(g_pair_build_mode) + " variant=" + variant +
        " mesh=" + GenerationDiagPointer(target) + " attachCount=" +
        std::to_string(attach_count) + " firstUseOrder=" +
        std::to_string(first_order) + " pairFirstUseSequence=" +
        std::to_string(g_twin.first_use_sequence) +
        " meshesBuilt=0 meshesDestroyed=0.");
    unlock();
}

DWORD WINAPI PairModeHotkeyThread(void*) {
    bool held = false;
    while (!g_pair_hotkey_stop.load(std::memory_order_acquire)) {
        const bool down =
            (GetAsyncKeyState(kPairBuildNoUploadHotkey) & 0x8000) != 0;
        if (down && !held) {
            g_pair_build_nn_requested.store(true, std::memory_order_release);
        }
        held = down;
        Sleep(15);
    }
    return 0;
}

bool StartPairModeHotkeys() {
    g_pair_hotkey_stop.store(false, std::memory_order_release);
    g_pair_hotkey_thread =
        CreateThread(nullptr, 0, &PairModeHotkeyThread, nullptr, 0, nullptr);
    return g_pair_hotkey_thread != nullptr;
}

void StopPairModeHotkeys() {
    g_pair_hotkey_stop.store(true, std::memory_order_release);
    if (!g_pair_hotkey_thread) return;
    WaitForSingleObject(g_pair_hotkey_thread, 1000);
    CloseHandle(g_pair_hotkey_thread);
    g_pair_hotkey_thread = nullptr;
}

void PumpPairModeDiag() {
    const int32_t frame = g_last_pump_frame;
    if (frame < 0 || frame == g_twin_last_frame) return;
    g_twin_last_frame = frame;

    if (g_twin_restore_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[PAIR-MODE] Num0 restore request consumed on Unity main thread.");
        RestoreTwin();
        return;
    }
    if (g_twin_build_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[PAIR-MODE] Num7 U/U build request consumed on Unity main thread.");
        BuildPairMode(PairBuildMode::UploadUpload, "Num7");
        return;
    }
    if (g_pair_build_nn_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[PAIR-MODE] Num1 N/N build request consumed on Unity main thread.");
        BuildPairMode(PairBuildMode::NoUploadNoUpload, "Num1");
        return;
    }
    if (g_twin_attach_a_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[PAIR-MODE] Num8 attach-A request consumed on Unity main thread.");
        AttachPairVariant(true);
        return;
    }
    if (g_twin_attach_b_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[PAIR-MODE] Num9 attach-B request consumed on Unity main thread.");
        AttachPairVariant(false);
    }
}

void __fastcall DetourPumpWithPairModeDiag(void* method_info) {
    // Deliberately bypass DetourPumpWithTwinDiag: that older wrapper would
    // consume Num7 and create the mixed U/N pair before this layer can classify
    // the batch. Keep the lower generation/recommit pump, then run our own C9
    // pair-mode pump once per frame.
    DetourPumpWithGenerationRecommitDiag(method_info);
    PumpPairModeDiag();
}

BE_Result BE_CALL PairModeCreateHook(
    void* context, const char* module_id, void* target, void* detour,
    void** original) {
    if (!g_pair_real_host || !g_pair_real_host->create_hook) {
        return BE_Result_NotReady;
    }
    if (detour == reinterpret_cast<void*>(&DetourPumpWithTwinDiag)) {
        g_pair_pump_intercepted = true;
        return g_pair_real_host->create_hook(
            context, module_id, target,
            reinterpret_cast<void*>(&DetourPumpWithPairModeDiag), original);
    }
    return g_pair_real_host->create_hook(
        context, module_id, target, detour, original);
}

BE_Result BE_CALL InitializeWithPairModeDiag(const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;
    g_pair_real_host = host;
    g_pair_host_proxy = *host;
    g_pair_host_proxy.create_hook = &PairModeCreateHook;
    g_pair_pump_intercepted = false;
    g_pair_build_mode = PairBuildMode::None;
    g_pair_build_nn_requested.store(false, std::memory_order_release);

    const BE_Result result = InitializeWithTwinDiag(&g_pair_host_proxy);
    if (result != BE_Result_Ok) {
        g_pair_real_host = nullptr;
        return result;
    }
    if (!g_pair_pump_intercepted) {
        Log("[PAIR-MODE] failed to intercept twin diagnostic pump; controls disabled.");
        ShutdownWithTwinDiag();
        g_pair_real_host = nullptr;
        return BE_Result_Failed;
    }
    if (!StartPairModeHotkeys()) {
        Log("[PAIR-MODE] failed to start Num1 diagnostic hotkey thread.");
        ShutdownWithTwinDiag();
        g_pair_real_host = nullptr;
        return BE_Result_Failed;
    }

    Log("[PAIR-MODE] overrides the older UPLOAD-AB experiment: Num7=build U/U pair, Num1=build N/N pair, Num8=attach A, Num9=attach B, Num0=restore original.");
    Log("[PAIR-MODE] U/U executes two final UploadMeshDataImpl calls; N/N executes zero. Build and compare several generations of each mode.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithPairModeDiag() {
    StopPairModeHotkeys();
    g_pair_build_nn_requested.store(false, std::memory_order_release);
    g_pair_build_mode = PairBuildMode::None;

    ShutdownWithTwinDiag();
    g_pair_real_host = nullptr;
    g_pair_host_proxy = {};
    g_pair_pump_intercepted = false;
}
