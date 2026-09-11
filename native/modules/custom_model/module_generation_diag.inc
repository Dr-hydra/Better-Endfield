// Cached-generation diagnostic for the intermittent custom-mesh failure.
//
// This layer deliberately sits outside the normal F12/F11 replacement state.
// It answers one question only: does a fault follow a particular runtime Mesh
// object, or does the same Mesh change behaviour when rebound to the renderer?
//
// 9  : build a fresh generation and cache it without touching any renderer.
// -  : bind that exact cached generation; never rebuild/upload/destroy here.
// =  : restore the original meshes but retain the cached generation.
//
// The diagnostic is mesh-only. It does not install textures, toggle EFMI-hidden
// draws, or participate in the normal automatic refresh. That keeps the
// experiment focused on Mesh construction versus renderer/HG registration.

constexpr int kGenerationDiagBuildHotkey = '9';
constexpr int kGenerationDiagAttachHotkey = VK_OEM_MINUS;
// Windows names the physical '='/'+' key VK_OEM_PLUS. No Shift is required for
// the '=' character the tester presses.
constexpr int kGenerationDiagRestoreHotkey = VK_OEM_PLUS;

std::atomic_bool g_generation_diag_build_requested{false};
std::atomic_bool g_generation_diag_attach_requested{false};
std::atomic_bool g_generation_diag_restore_requested{false};
std::atomic_bool g_generation_diag_hotkey_stop{false};
HANDLE g_generation_diag_hotkey_thread = nullptr;
int32_t g_generation_diag_last_frame = -1;
uint64_t g_generation_diag_id = 0;
bool g_generation_diag_attached = false;

struct GenerationDiagRecord {
    uint32_t component_id = 0;
    uint32_t original_index_count = 0;
    void* renderer = nullptr;
    void* original_mesh = nullptr;
    void* custom_mesh = nullptr;
    uint32_t renderer_handle = 0;
    uint32_t original_mesh_handle = 0;
    uint32_t custom_mesh_handle = 0;
    std::string renderer_name;
    std::string path;
};
std::vector<GenerationDiagRecord> g_generation_diag_records;

const BE_HostApiV1* g_generation_diag_real_host = nullptr;
BE_HostApiV1 g_generation_diag_host_proxy{};
bool g_generation_diag_pump_intercepted = false;

std::string GenerationDiagPointer(void* value) {
    std::array<char, 32> text{};
    std::snprintf(text.data(), text.size(), "%p", value);
    return text.data();
}

void ReleaseGenerationDiagHandles(GenerationDiagRecord& record) {
    if (!g_host || !g_host->gchandle_free) return;
    if (record.renderer_handle) {
        g_host->gchandle_free(g_host->context, record.renderer_handle);
    }
    if (record.original_mesh_handle) {
        g_host->gchandle_free(g_host->context, record.original_mesh_handle);
    }
    if (record.custom_mesh_handle) {
        g_host->gchandle_free(g_host->context, record.custom_mesh_handle);
    }
    record.renderer_handle = 0;
    record.original_mesh_handle = 0;
    record.custom_mesh_handle = 0;
}

bool PinGenerationDiagRecord(GenerationDiagRecord& record) {
    if (!g_host || !g_host->gchandle_new || !g_host->gchandle_free ||
        !record.renderer || !record.original_mesh || !record.custom_mesh) {
        return false;
    }
    record.renderer_handle =
        g_host->gchandle_new(g_host->context, record.renderer, 0);
    record.original_mesh_handle =
        g_host->gchandle_new(g_host->context, record.original_mesh, 0);
    record.custom_mesh_handle =
        g_host->gchandle_new(g_host->context, record.custom_mesh, 0);
    if (!record.renderer_handle || !record.original_mesh_handle ||
        !record.custom_mesh_handle) {
        ReleaseGenerationDiagHandles(record);
        return false;
    }
    return true;
}

bool GenerationDiagOfficialPathIdle() {
    return !g_replacement_active && g_pending_apply_frame < 0;
}

bool GenerationDiagCustomStillInUse() {
    if (g_generation_diag_records.empty()) return false;
    std::vector<RendererSummary> live;
    if (!SnapshotRendererMeshes(live)) {
        // A destructive cleanup is never worth guessing after an incomplete
        // renderer snapshot.
        return true;
    }
    for (const auto& item : live) {
        if (!item.mesh) continue;
        for (const auto& record : g_generation_diag_records) {
            if (record.custom_mesh && item.mesh == record.custom_mesh) {
                return true;
            }
        }
    }
    return false;
}

bool DestroyGenerationDiagCache() {
    if (g_generation_diag_records.empty()) {
        g_generation_diag_attached = false;
        return true;
    }
    if (g_generation_diag_attached || GenerationDiagCustomStillInUse()) {
        Log("[GEN-DIAG] build refused: cached Mesh is still bound somewhere; press '=' first.");
        return false;
    }

    std::vector<void*> doomed;
    for (auto& record : g_generation_diag_records) {
        if (record.custom_mesh &&
            std::find(doomed.begin(), doomed.end(), record.custom_mesh) ==
                doomed.end()) {
            doomed.push_back(record.custom_mesh);
        }
    }
    for (void* mesh : doomed) DestroyUnityObject(mesh);
    for (auto& record : g_generation_diag_records) {
        ReleaseGenerationDiagHandles(record);
    }
    g_generation_diag_records.clear();
    Log("[GEN-DIAG] retired detached cache: meshesDestroyed=" +
        std::to_string(doomed.size()));
    return true;
}

void AbandonGenerationDiagPending(
    std::vector<GenerationDiagRecord>& pending) {
    std::vector<void*> doomed;
    for (auto& record : pending) {
        if (record.custom_mesh &&
            std::find(doomed.begin(), doomed.end(), record.custom_mesh) ==
                doomed.end()) {
            doomed.push_back(record.custom_mesh);
        }
    }
    for (void* mesh : doomed) DestroyUnityObject(mesh);
    for (auto& record : pending) ReleaseGenerationDiagHandles(record);
    pending.clear();
}

void BuildGenerationDiag() {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        Log("[GEN-DIAG] 9 ignored: CustomModel is busy.");
        return;
    }

    const auto unlock = []() {
        g_operation_in_progress.store(false, std::memory_order_release);
    };

    if (!GenerationDiagOfficialPathIdle()) {
        Log("[GEN-DIAG] 9 refused: the normal F12 replacement is active or pending; use F11 first.");
        unlock();
        return;
    }
    if (!DestroyGenerationDiagCache()) {
        unlock();
        return;
    }

    BemPocData bem;
    const std::filesystem::path bem_path = BemPath();
    if (bem_path.empty() || !LoadBemPoc(bem_path, bem)) {
        Log("[GEN-DIAG] 9 failed: BEM payload unavailable.");
        unlock();
        return;
    }

    const std::vector<RendererSummary> summaries =
        EnumerateLiveEndminfRenderers();
    std::vector<const RendererSummary*> gameplay;
    for (const auto& item : summaries) {
        if (item.renderer && item.mesh && IsGameplayPostModelPath(item.path)) {
            gameplay.push_back(&item);
        }
    }
    if (gameplay.empty()) {
        Log("[GEN-DIAG] 9 failed: no live gameplay postmodel renderer is available.");
        unlock();
        return;
    }

    const uint64_t candidate_generation = g_generation_diag_id + 1;
    std::vector<GenerationDiagRecord> pending;
    int geometry_components = 0;
    bool failed = false;

    Log("[GEN-DIAG] build generation=" +
        std::to_string(candidate_generation) +
        " begin; renderer state will remain unchanged.");

    for (const BemComponent& component : bem.components) {
        const auto& info = component.info;
        if ((info.flags & kComponentFlagNoGeometry) != 0) continue;
        ++geometry_components;

        std::vector<const RendererSummary*> targets;
        for (const RendererSummary* item : gameplay) {
            if (item->total_indices == info.original_index_count) {
                targets.push_back(item);
            }
        }
        if (targets.empty()) {
            Log("[GEN-DIAG] generation=" +
                std::to_string(candidate_generation) + " C" +
                std::to_string(info.component_id) +
                " has no gameplay source renderer; build aborted.");
            failed = true;
            break;
        }

        for (const RendererSummary* item : targets) {
            // The controlled experiment must start from the game's source Mesh,
            // never from a leftover BetterEndfield generation.
            if (item->mesh_name.rfind("BetterEndfield.", 0) == 0) {
                Log("[GEN-DIAG] source renderer already carries a runtime Mesh: " +
                    item->path + "; build aborted.");
                failed = true;
                break;
            }
            if (item->bones_count <= static_cast<int>(info.max_bone) ||
                !ValidateRendererSkin(component, item->renderer, item->mesh)) {
                Log("[GEN-DIAG] generation=" +
                    std::to_string(candidate_generation) + " C" +
                    std::to_string(info.component_id) +
                    " source skin validation failed; build aborted.");
                failed = true;
                break;
            }

            void* custom_mesh = nullptr;
            if (!BuildMeshFromComponent(component, item->mesh, custom_mesh) ||
                !custom_mesh) {
                Log("[GEN-DIAG] generation=" +
                    std::to_string(candidate_generation) + " C" +
                    std::to_string(info.component_id) +
                    " Mesh build failed; generation discarded.");
                failed = true;
                break;
            }

            GenerationDiagRecord record;
            record.component_id = info.component_id;
            record.original_index_count = info.original_index_count;
            record.renderer = item->renderer;
            record.original_mesh = item->mesh;
            record.custom_mesh = custom_mesh;
            record.renderer_name = item->renderer_name;
            record.path = item->path;
            if (!PinGenerationDiagRecord(record)) {
                DestroyUnityObject(custom_mesh);
                Log("[GEN-DIAG] failed to pin generation objects; generation discarded.");
                failed = true;
                break;
            }

            Log("[GEN-DIAG] generation=" +
                std::to_string(candidate_generation) + " C" +
                std::to_string(info.component_id) + " renderer=" +
                GenerationDiagPointer(record.renderer) + " originalMesh=" +
                GenerationDiagPointer(record.original_mesh) + " customMesh=" +
                GenerationDiagPointer(record.custom_mesh) + " path=\"" +
                record.path + "\"");
            pending.push_back(std::move(record));
        }
        if (failed) break;
    }

    if (failed || pending.empty() || geometry_components == 0) {
        AbandonGenerationDiagPending(pending);
        Log("[GEN-DIAG] build generation=" +
            std::to_string(candidate_generation) +
            " failed; renderer state unchanged.");
        unlock();
        return;
    }

    g_generation_diag_records = std::move(pending);
    g_generation_diag_id = candidate_generation;
    g_generation_diag_attached = false;
    Log("[GEN-DIAG] build generation=" +
        std::to_string(g_generation_diag_id) + " complete: cachedMeshes=" +
        std::to_string(g_generation_diag_records.size()) +
        " meshesAttached=0 meshesDestroyed=0; textures/visibility unchanged.");
    unlock();
}

void AttachGenerationDiag() {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        Log("[GEN-DIAG] '-' ignored: CustomModel is busy.");
        return;
    }
    const auto unlock = []() {
        g_operation_in_progress.store(false, std::memory_order_release);
    };

    if (!GenerationDiagOfficialPathIdle()) {
        Log("[GEN-DIAG] '-' refused: normal F12 replacement is active or pending.");
        unlock();
        return;
    }
    if (g_generation_diag_records.empty()) {
        Log("[GEN-DIAG] '-' has no cached generation; press 9 first.");
        unlock();
        return;
    }

    // Preflight the entire set before touching one renderer. If the game has
    // replaced/recreated a renderer since build, that is a different experiment
    // and must get a fresh generation with 9.
    for (const auto& record : g_generation_diag_records) {
        if (!IsNativeObjectAlive(record.renderer) ||
            !IsNativeObjectAlive(record.original_mesh) ||
            !IsNativeObjectAlive(record.custom_mesh)) {
            Log("[GEN-DIAG] '-' refused: generation=" +
                std::to_string(g_generation_diag_id) + " C" +
                std::to_string(record.component_id) +
                " contains a dead object; restore/rebuild the generation.");
            unlock();
            return;
        }
        void* current = Invoke(Contract("skinned.get_shared_mesh"),
            record.renderer, nullptr, false);
        if (current != record.original_mesh && current != record.custom_mesh) {
            Log("[GEN-DIAG] '-' refused: generation=" +
                std::to_string(g_generation_diag_id) + " C" +
                std::to_string(record.component_id) + " renderer now owns mesh=" +
                GenerationDiagPointer(current) + " instead of cached original/custom; press 9 for a fresh snapshot.");
            unlock();
            return;
        }
    }

    std::vector<GenerationDiagRecord*> changed;
    int already_custom = 0;
    for (auto& record : g_generation_diag_records) {
        void* before = Invoke(Contract("skinned.get_shared_mesh"),
            record.renderer, nullptr, false);
        if (before == record.custom_mesh) ++already_custom;
        if (!SetSharedMesh(record.renderer, record.custom_mesh) ||
            Invoke(Contract("skinned.get_shared_mesh"), record.renderer,
                nullptr, false) != record.custom_mesh) {
            Log("[GEN-DIAG] attach rejected on C" +
                std::to_string(record.component_id) +
                "; reverting renderers changed by this attach attempt.");
            for (GenerationDiagRecord* prior : changed) {
                SetSharedMesh(prior->renderer, prior->original_mesh);
            }
            unlock();
            return;
        }
        if (before == record.original_mesh) changed.push_back(&record);
    }

    g_generation_diag_attached = true;
    Log("[GEN-DIAG] attach generation=" +
        std::to_string(g_generation_diag_id) + " complete: renderers=" +
        std::to_string(g_generation_diag_records.size()) +
        " alreadyCustom=" + std::to_string(already_custom) +
        " meshesBuilt=0 meshesDestroyed=0. Repeating '-' reuses these exact Mesh pointers.");
    unlock();
}

void RestoreGenerationDiag() {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        Log("[GEN-DIAG] '=' ignored: CustomModel is busy.");
        return;
    }
    const auto unlock = []() {
        g_operation_in_progress.store(false, std::memory_order_release);
    };

    if (g_generation_diag_records.empty()) {
        Log("[GEN-DIAG] '=' has no cached generation; original model is untouched.");
        g_generation_diag_attached = false;
        unlock();
        return;
    }

    int restored = 0;
    int already_original = 0;
    int foreign = 0;
    int failed = 0;
    for (auto& record : g_generation_diag_records) {
        if (!IsNativeObjectAlive(record.renderer)) {
            ++foreign;
            continue;
        }
        void* current = Invoke(Contract("skinned.get_shared_mesh"),
            record.renderer, nullptr, false);
        if (current == record.original_mesh) {
            ++already_original;
            continue;
        }
        if (current != record.custom_mesh) {
            ++foreign;
            continue;
        }
        if (!IsNativeObjectAlive(record.original_mesh) ||
            !SetSharedMesh(record.renderer, record.original_mesh) ||
            Invoke(Contract("skinned.get_shared_mesh"), record.renderer,
                nullptr, false) != record.original_mesh) {
            ++failed;
            continue;
        }
        ++restored;
    }

    bool any_custom = false;
    for (const auto& record : g_generation_diag_records) {
        if (!IsNativeObjectAlive(record.renderer)) continue;
        if (Invoke(Contract("skinned.get_shared_mesh"), record.renderer,
                nullptr, false) == record.custom_mesh) {
            any_custom = true;
            break;
        }
    }
    g_generation_diag_attached = any_custom;

    Log("[GEN-DIAG] restore generation=" +
        std::to_string(g_generation_diag_id) + " complete: restored=" +
        std::to_string(restored) + " alreadyOriginal=" +
        std::to_string(already_original) + " foreign=" +
        std::to_string(foreign) + " failed=" + std::to_string(failed) +
        " cachedMeshesRetained=" +
        std::to_string(g_generation_diag_records.size()) +
        " meshesBuilt=0 meshesDestroyed=0.");
    if (any_custom) {
        Log("[GEN-DIAG] warning: at least one cached Mesh is still bound; do not press 9 yet.");
    }
    unlock();
}

DWORD WINAPI GenerationDiagHotkeyThread(void*) {
    struct Latch {
        int key;
        std::atomic_bool* flag;
        bool held = false;
    };
    std::array<Latch, 3> latches{{
        {kGenerationDiagBuildHotkey, &g_generation_diag_build_requested, false},
        {kGenerationDiagAttachHotkey, &g_generation_diag_attach_requested, false},
        {kGenerationDiagRestoreHotkey, &g_generation_diag_restore_requested, false}}};

    while (!g_generation_diag_hotkey_stop.load(std::memory_order_acquire)) {
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

bool StartGenerationDiagHotkeys() {
    g_generation_diag_hotkey_stop.store(false, std::memory_order_release);
    g_generation_diag_hotkey_thread = CreateThread(
        nullptr, 0, &GenerationDiagHotkeyThread, nullptr, 0, nullptr);
    return g_generation_diag_hotkey_thread != nullptr;
}

void StopGenerationDiagHotkeys() {
    g_generation_diag_hotkey_stop.store(true, std::memory_order_release);
    if (!g_generation_diag_hotkey_thread) return;
    WaitForSingleObject(g_generation_diag_hotkey_thread, 1000);
    CloseHandle(g_generation_diag_hotkey_thread);
    g_generation_diag_hotkey_thread = nullptr;
}

void PumpGenerationDiag() {
    // Base PumpCustomModel runs first and records the current Unity frame.
    const int32_t frame = g_last_pump_frame;
    if (frame < 0 || frame == g_generation_diag_last_frame) return;
    g_generation_diag_last_frame = frame;

    // Restoration wins if the tester presses more than one diagnostic key in
    // the same frame; build wins over attach after that.
    if (g_generation_diag_restore_requested.exchange(
            false, std::memory_order_acq_rel)) {
        g_generation_diag_build_requested.store(false, std::memory_order_release);
        g_generation_diag_attach_requested.store(false, std::memory_order_release);
        Log("[GEN-DIAG] '=' restore request consumed on Unity main thread.");
        RestoreGenerationDiag();
        return;
    }
    if (g_generation_diag_build_requested.exchange(
            false, std::memory_order_acq_rel)) {
        g_generation_diag_attach_requested.store(false, std::memory_order_release);
        Log("[GEN-DIAG] 9 build request consumed on Unity main thread.");
        BuildGenerationDiag();
        return;
    }
    if (g_generation_diag_attach_requested.exchange(
            false, std::memory_order_acq_rel)) {
        Log("[GEN-DIAG] '-' attach request consumed on Unity main thread.");
        AttachGenerationDiag();
    }
}

void __fastcall DetourPumpWithGenerationDiag(void* method_info) {
    if (g_original_pump) g_original_pump(method_info);

    // Do not let the normal F12 path overwrite an attached diagnostic
    // generation; that would destroy the one-variable experiment.
    if (g_generation_diag_attached &&
        g_toggle_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("[GEN-DIAG] F12 ignored while cached generation is attached; press '=' first.");
    }

    // This is the original CustomModel main-thread pump body, without invoking
    // its detour wrapper (which would call the engine callback a second time).
    PumpCustomModel();
    PumpGenerationDiag();
}

BE_Result BE_CALL GenerationDiagCreateHook(
    void* context, const char* module_id, void* target, void* detour,
    void** original) {
    if (!g_generation_diag_real_host ||
        !g_generation_diag_real_host->create_hook) {
        return BE_Result_NotReady;
    }
    if (detour == reinterpret_cast<void*>(&DetourPump)) {
        g_generation_diag_pump_intercepted = true;
        return g_generation_diag_real_host->create_hook(
            context, module_id, target,
            reinterpret_cast<void*>(&DetourPumpWithGenerationDiag), original);
    }
    return g_generation_diag_real_host->create_hook(
        context, module_id, target, detour, original);
}

BE_Result BE_CALL InitializeWithGenerationDiag(const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;

    g_generation_diag_real_host = host;
    g_generation_diag_host_proxy = *host;
    g_generation_diag_host_proxy.create_hook = &GenerationDiagCreateHook;
    g_generation_diag_pump_intercepted = false;
    g_generation_diag_build_requested.store(false, std::memory_order_release);
    g_generation_diag_attach_requested.store(false, std::memory_order_release);
    g_generation_diag_restore_requested.store(false, std::memory_order_release);
    g_generation_diag_last_frame = -1;
    g_generation_diag_attached = false;
    g_generation_diag_records.clear();

    const BE_Result result = Initialize(&g_generation_diag_host_proxy);
    if (result != BE_Result_Ok) {
        g_generation_diag_real_host = nullptr;
        return result;
    }
    if (!g_generation_diag_pump_intercepted) {
        Log("[GEN-DIAG] failed to intercept the CustomModel pump hook; diagnostic hotkeys disabled.");
        Shutdown();
        g_generation_diag_real_host = nullptr;
        return BE_Result_Failed;
    }
    if (!StartGenerationDiagHotkeys()) {
        Log("[GEN-DIAG] failed to start 9/-/= hotkey latch thread.");
        Shutdown();
        g_generation_diag_real_host = nullptr;
        return BE_Result_Failed;
    }

    Log("[GEN-DIAG] cached-generation test ready: 9=build without attach, -=attach same cached Mesh pointers, ==restore originals while retaining cache.");
    Log("[GEN-DIAG] diagnostic path is mesh-only: textures and mod-hidden visibility are intentionally unchanged.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithGenerationDiag() {
    StopGenerationDiagHotkeys();

    // Like the base shutdown, this can run outside the Unity main thread. Do
    // not Destroy() engine objects here; only release our managed handles and
    // forget the cache. Scene teardown owns any remaining native objects.
    for (auto& record : g_generation_diag_records) {
        ReleaseGenerationDiagHandles(record);
    }
    g_generation_diag_records.clear();
    g_generation_diag_attached = false;
    g_generation_diag_last_frame = -1;
    g_generation_diag_build_requested.store(false, std::memory_order_release);
    g_generation_diag_attach_requested.store(false, std::memory_order_release);
    g_generation_diag_restore_requested.store(false, std::memory_order_release);

    Shutdown();
    g_generation_diag_real_host = nullptr;
    g_generation_diag_host_proxy = {};
}
