// Each record owns distinct GC handles, even when several records share a Mesh.
void PinReplacement(ReplacedComponent& record) {
    record.renderer_handle = record.original_mesh_handle =
        record.custom_mesh_handle = 0;
    if (!g_host->gchandle_new) return;
    if (record.renderer) record.renderer_handle =
        g_host->gchandle_new(g_host->context, record.renderer, 0);
    if (record.original_mesh) record.original_mesh_handle =
        g_host->gchandle_new(g_host->context, record.original_mesh, 0);
    if (record.custom_mesh) record.custom_mesh_handle =
        g_host->gchandle_new(g_host->context, record.custom_mesh, 0);
}

const BemComponent* ActiveComponent(uint32_t id) {
    for (const auto& component : g_active_bem.components) {
        if (component.info.component_id == id) return &component;
    }
    return nullptr;
}

// A clone of a patched prefab already carries our custom Mesh, whose index
// count may differ from the original. Identity must be checked before counts.
const ReplacedComponent* ReplacementSource(void* mesh) {
    for (const auto& record : g_replaced) {
        if (record.custom_mesh && record.custom_mesh == mesh) return &record;
    }
    for (const auto& record : g_replaced) {
        if (record.original_mesh == mesh) return &record;
    }
    return nullptr;
}

bool PrepareReplacement(const RendererSummary& item, ReplacedComponent& record) {
    const ReplacedComponent* source = ReplacementSource(item.mesh);
    if (source) {
        record = *source;
        // A cloned custom Mesh must roll back to its source asset, never to
        // itself. The clone also inherits the template's hidden state.
        if (item.mesh != source->custom_mesh && !source->hidden_by_mod) {
            GetRendererEnabled(item.renderer, record.original_enabled);
        }
    } else {
        const BemComponent* component = nullptr;
        for (const auto& candidate : g_active_bem.components) {
            if (candidate.info.original_index_count == item.total_indices) {
                component = &candidate;
                break;
            }
        }
        if (!component) return false;
        record.component_id = component->info.component_id;
        record.original_index_count = component->info.original_index_count;
        record.original_mesh = item.mesh;
        record.hidden_by_mod = (component->info.flags &
            (kComponentFlagNoGeometry | kComponentFlagHidden)) != 0;
        GetRendererEnabled(item.renderer, record.original_enabled);
        if ((component->info.flags & kComponentFlagNoGeometry) == 0) {
            if (item.bones_count <= static_cast<int>(component->info.max_bone) ||
                !BuildMeshFromComponent(*component, item.mesh, record.custom_mesh)) {
                return false;
            }
        }
    }
    record.renderer = item.renderer;
    record.renderer_handle = record.original_mesh_handle =
        record.custom_mesh_handle = 0;
    const BemComponent* component = ActiveComponent(record.component_id);
    if (record.custom_mesh && component &&
        !ValidateRendererSkin(*component, item.renderer, record.original_mesh)) {
        if (!source) DestroyUnityObject(record.custom_mesh);
        return false;
    }
    if (record.custom_mesh && component &&
        item.bones_count <= static_cast<int>(component->info.max_bone)) {
        if (!source) DestroyUnityObject(record.custom_mesh);
        return false;
    }
    if (record.custom_mesh && item.mesh != record.custom_mesh) {
        if (!SetSharedMesh(item.renderer, record.custom_mesh) ||
            Invoke(Contract("skinned.get_shared_mesh"), item.renderer,
                nullptr, false) != record.custom_mesh) {
            SetSharedMesh(item.renderer, item.mesh);
            if (!source) DestroyUnityObject(record.custom_mesh);
            return false;
        }
    }
    PinReplacement(record);
    return true;
}

// Reconcile both newly spawned instances and game write-backs. A different
// source asset is rebuilt using its own bindposes, not a count-matched donor.
void RefreshActiveReplacement(const std::vector<RendererSummary>& summaries) {
    if (!g_replacement_active || g_refresh_suspended || g_replaced.empty()) return;
    int applied = 0;
    int repaired = 0;
    int rehidden = 0;
    for (const RendererSummary& item : summaries) {
        if (!item.renderer || !item.mesh) continue;
        size_t known = 0;
        while (known < g_replaced.size() &&
            g_replaced[known].renderer != item.renderer) ++known;
        const bool existed = known < g_replaced.size();
        if (existed && g_replaced[known].custom_mesh &&
            item.mesh == g_replaced[known].original_mesh) {
            const auto* component = ActiveComponent(g_replaced[known].component_id);
            if (!component || !ValidateRendererSkin(*component, item.renderer, item.mesh)) continue;
            if (!SetSharedMesh(item.renderer, g_replaced[known].custom_mesh) ||
                Invoke(Contract("skinned.get_shared_mesh"), item.renderer,
                    nullptr, false) != g_replaced[known].custom_mesh) continue;
            ++repaired;
        } else if (!existed || (item.mesh != g_replaced[known].custom_mesh &&
            (g_replaced[known].custom_mesh ||
             item.mesh != g_replaced[known].original_mesh))) {
            ReplacedComponent record;
            if (!PrepareReplacement(item, record)) continue;
            if (existed) {
                // Keep old asset ownership until rollback: other prefab clones
                // may still reference this variant, even after this renderer
                // has been reconfigured by the game.
                ReplacedComponent retired = g_replaced[known];
                retired.renderer = nullptr;
                g_replaced.push_back(retired);
                g_replaced[known] = record;
                ++repaired;
            } else {
                known = g_replaced.size();
                g_replaced.push_back(record);
                ++applied;
            }
        }
        auto& record = g_replaced[known];
        bool enabled = false;
        if (record.hidden_by_mod && GetRendererEnabled(item.renderer, enabled) &&
            enabled != g_mask_visible &&
            SetRendererEnabled(item.renderer, g_mask_visible)) ++rehidden;
    }

    if (applied > 0 || repaired > 0 || rehidden > 0) {
        Log("CustomModel refresh: newRenderers=" + std::to_string(applied) +
            " meshesRestored=" + std::to_string(repaired) +
            " hiddenAgain=" + std::to_string(rehidden));
    }

    // A live renderer with no record that also matches no component is either
    // a draw the payload does not carry or a model the replacement has not
    // recognised. Naming the set when it changes is what turns "the showcase
    // is not replaced" into the list of draws the showcase actually has,
    // without repeating the same line once a second.
    std::vector<std::string> unknown;
    for (const RendererSummary& item : summaries) {
        if (!item.renderer || !item.mesh || item.total_indices == 0) continue;
        const bool recorded = std::any_of(
            g_replaced.begin(), g_replaced.end(),
            [&](const ReplacedComponent& record) {
                return record.renderer == item.renderer ||
                    (record.original_index_count != 0 &&
                     record.original_index_count == item.total_indices);
            });
        if (recorded) continue;
        unknown.push_back(item.renderer_name + " idx=" +
            std::to_string(item.total_indices));
    }
    std::sort(unknown.begin(), unknown.end());
    unknown.erase(std::unique(unknown.begin(), unknown.end()), unknown.end());
    std::string unknown_text;
    for (const std::string& entry : unknown) {
        if (!unknown_text.empty()) unknown_text += ", ";
        unknown_text += entry;
    }
    static std::string last_unknown;
    if (unknown_text != last_unknown) {
        last_unknown = unknown_text;
        Log("CustomModel refresh: live draws no component covers: [" +
            unknown_text + "]");
    }
}

// Rollback needs all renderers, including renamed clones and other LODs that
// may have inherited a custom Mesh. A filtered discovery set is not proof that
// a Mesh is unused. Refuse destructive cleanup if the snapshot is incomplete.
bool SnapshotRendererMeshes(std::vector<RendererSummary>& live) {
    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* objects = Invoke(Contract("resources.find_objects_of_type_all"),
        nullptr, parameters);
    int dimension = 0;
    int count = -1;
    void* length_parameters[1]{&dimension};
    if (!objects || !InvokeValue(Contract("array.get_length"), objects,
            length_parameters, count) || count < 0) return false;
    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (!renderer) return false;
        void* exception = nullptr;
        void* mesh = g_host->runtime_invoke(g_host->context,
            Contract("skinned.get_shared_mesh")->method_info, renderer,
            nullptr, &exception);
        if (exception) return false;
        RendererSummary item;
        item.renderer = renderer;
        item.mesh = mesh;
        live.push_back(std::move(item));
    }
    return true;
}

bool RollbackReplacement() {
    // A failed rollback must not be undone by the automatic apply refresh.
    g_refresh_suspended = true;
    std::vector<RendererSummary> live;
    if (!SnapshotRendererMeshes(live)) {
        Log("Model rollback deferred: renderer snapshot incomplete; assets retained.");
        return false;
    }
    int inherited = 0;
    for (const auto& item : live) {
        const bool known = std::any_of(g_replaced.begin(), g_replaced.end(),
            [&](const auto& record) { return record.renderer == item.renderer; });
        if (known || !item.mesh) continue;
        const auto* source = ReplacementSource(item.mesh);
        if (!source || (item.mesh != source->custom_mesh &&
            !source->hidden_by_mod)) continue;
        // Non-custom hidden draws still need the character filter; a shared
        // original asset may also be used outside this model's lod0 draws.
        if (item.mesh != source->custom_mesh && !IsEndminfLod0Draw(
                ObjectName(item.renderer), BuildTransformPath(item.renderer))) continue;
        ReplacedComponent record = *source;
        record.renderer = item.renderer;
        PinReplacement(record);
        g_replaced.push_back(record);
        ++inherited;
    }
    // Validate native assets before changing any renderer. Non-null managed
    // wrappers can survive an asset unload, and cannot be used for restoration.
    for (const auto& record : g_replaced) {
        const auto item = std::find_if(live.begin(), live.end(),
            [&](const auto& candidate) { return candidate.renderer == record.renderer; });
        if (item == live.end() || !record.custom_mesh ||
            item->mesh != record.custom_mesh) continue;
        if (!IsNativeObjectAlive(record.original_mesh)) {
            Log("Rollback blocked before changes: C" +
                std::to_string(record.component_id) + " original native mesh lost; renderer=" +
                ObjectName(record.renderer) + " path=" + BuildTransformPath(record.renderer));
            return false;
        }
    }
    int restored = 0;
    int re_enabled = 0;
    bool failed = false;
    std::vector<void*> doomed;
    for (const auto& record : g_replaced) {
        if (record.custom_mesh && std::find(doomed.begin(), doomed.end(),
                record.custom_mesh) == doomed.end()) doomed.push_back(record.custom_mesh);
        auto item = std::find_if(live.begin(), live.end(),
            [&](const auto& candidate) { return candidate.renderer == record.renderer; });
        if (item == live.end()) continue;
        // Respect a mesh installed by the game since our last reconciliation.
        if (item->mesh != record.custom_mesh && item->mesh != record.original_mesh) continue;
        if (record.hidden_by_mod) {
            if (SetRendererEnabled(record.renderer, record.original_enabled)) ++re_enabled;
            else failed = true;
        }
        if (!record.custom_mesh || item->mesh != record.custom_mesh) continue;
        if (SetSharedMesh(record.renderer, record.original_mesh) &&
            Invoke(Contract("skinned.get_shared_mesh"), record.renderer, nullptr,
                false) == record.original_mesh) ++restored;
        else failed = true;
    }
    if (failed) {
        Log("Model rollback deferred: a renderer rejected restoration; records and assets retained.");
        return false;
    }
    if (!RestoreOriginalTextures()) {
        Log("Rollback deferred: texture users could not all be restored; assets retained.");
        return false;
    }
    const size_t total = g_replaced.size();
    ClearReplacementState();
    for (void* mesh : doomed) DestroyUnityObject(mesh);
    int32_t frame = 0;
    if (InvokeValue(Contract("time.get_frame_count"), nullptr, nullptr, frame))
        g_earliest_apply_frame = frame + 2;
    Log("Model rollback complete: restored=" + std::to_string(restored) +
        " reEnabled=" + std::to_string(re_enabled) +
        " inheritedClones=" + std::to_string(inherited) +
        " meshesDestroyed=" + std::to_string(doomed.size()) +
        " records=" + std::to_string(total));
    return true;
}

// F12 retires the old generation, then applies after its destruction boundary.
// Scene changes are reconciled per renderer without rolling the entire model back.
void ApplyOrRefreshReplacement() {
    if (g_pending_apply_frame >= 0 ||
        g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) return;
    if (!g_replacement_active || RollbackReplacement()) {
        g_pending_apply_frame = std::max(g_earliest_apply_frame, 0);
        Log("F12 queued: packed-stream skinning.");
    }
    g_operation_in_progress.store(false, std::memory_order_release);
}

void RollbackRequestedReplacement() {
    g_pending_apply_frame = -1;
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) return;
    if (g_replacement_active) RollbackReplacement();
    else Log("F11 restoration complete: no replacement is active.");
    g_operation_in_progress.store(false, std::memory_order_release);
}

void UpdateReplacement(int32_t frame) {
    if (g_pending_apply_frame < 0 && !g_replacement_active) return;
    const uint64_t now = GetTickCount64();
    if (g_pending_apply_frame >= 0) {
        if (frame < g_pending_apply_frame) return;
    } else if (g_refresh_suspended || now < g_next_refresh_tick) return;
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) return;
    const auto summaries = EnumerateLiveEndminfRenderers();
    if (g_pending_apply_frame >= 0) {
        g_pending_apply_frame = -1;
        ApplyReplacement(summaries);
    } else {
        RefreshActiveReplacement(summaries);
    }
    g_next_refresh_tick = now + kActiveRefreshIntervalMs;
    g_operation_in_progress.store(false, std::memory_order_release);
}

void ToggleMaskVisibility() {
    if (g_operation_in_progress.exchange(
            true, std::memory_order_acq_rel)) {
        return;
    }

    if (!g_replacement_active) {
        Log("F4 has nothing to toggle: press F12 first.");
    } else {
        const bool any = std::any_of(
            g_replaced.begin(), g_replaced.end(),
            [](const ReplacedComponent& item) { return item.hidden_by_mod; });
        if (!any) {
            Log("F4 has nothing to toggle: this mod hides no component by "
                "default.");
        } else {
            g_mask_visible = !g_mask_visible;
            ApplyMaskVisibility();
        }
    }

    g_operation_in_progress.store(false, std::memory_order_release);
}

void RunBufferProbe() {
    if (g_operation_in_progress.exchange(
            true, std::memory_order_acq_rel)) {
        return;
    }
    ProbeRendererBuffers();
    g_operation_in_progress.store(false, std::memory_order_release);
}

void RunAbiProbe() {
    if (g_operation_in_progress.exchange(
            true, std::memory_order_acq_rel)) {
        return;
    }
    ProbeEngineBindingAbi();
    g_operation_in_progress.store(false, std::memory_order_release);
}

void RunTextureProbe() {
    if (g_operation_in_progress.exchange(
            true, std::memory_order_acq_rel)) {
        return;
    }
    ProbeMaterialTextures();
    g_operation_in_progress.store(false, std::memory_order_release);
}

void RunModelProbe() {
    if (g_operation_in_progress.exchange(
            true, std::memory_order_acq_rel)) {
        return;
    }
    g_model_probe_captures_remaining = kModelProbeCaptureCount;
    g_model_probe_next_frame = -1;
    g_model_probe_sequence = 0;
    Log("0 skin diagnostic armed: " +
        std::to_string(kModelProbeCaptureCount) + " captures every " +
        std::to_string(kModelProbeCaptureIntervalFrames) +
        " frames; leave the game in the faulty state while it runs.");
    g_operation_in_progress.store(false, std::memory_order_release);
}

DWORD WINAPI HotkeyThread(void*) {
    struct Latch {
        int key;
        std::atomic_bool* flag;
        bool held;
    };
    std::array<Latch, 7> latches{{
        {kToggleHotkey, &g_toggle_requested, false},
        {kRollbackHotkey, &g_rollback_requested, false},
        {kMaskHotkey, &g_mask_requested, false},
        {kBufferProbeHotkey, &g_buffer_probe_requested, false},
        {kAbiProbeHotkey, &g_abi_probe_requested, false},
        {kTextureProbeHotkey, &g_texture_probe_requested, false},
        {kModelProbeHotkey, &g_model_probe_requested, false}}};

    while (!g_hotkey_thread_stop.load(std::memory_order_acquire)) {
        for (Latch& latch : latches) {
            const bool down =
                (GetAsyncKeyState(latch.key) & 0x8000) != 0;
            if (down && !latch.held) {
                latch.flag->store(true, std::memory_order_release);
            }
            latch.held = down;
        }
        Sleep(15);
    }
    return 0;
}

void PumpCustomModel() {
    // Claim the frame before invoking managed APIs that may reenter us.
    // Do not consume queued keys if the frame counter cannot be read.
    int32_t pump_frame = -1;
    if (!InvokeValue(Contract("time.get_frame_count"), nullptr, nullptr,
            pump_frame) || pump_frame == g_last_pump_frame) return;
    g_last_pump_frame = pump_frame;
    const uint64_t hit =
        g_pump_hits.fetch_add(1, std::memory_order_relaxed) + 1;
    if (hit == 1) {
        Log("CustomModel main-thread pump observed first runtime call.");
    }

    // Restoration wins if both keys arrived before the same frame.
    if (g_rollback_requested.exchange(false, std::memory_order_acq_rel)) {
        g_toggle_requested.store(false, std::memory_order_release);
        Log("F11 rollback request consumed on Unity main thread.");
        RollbackRequestedReplacement();
        return;
    }

    if (g_toggle_requested.exchange(
            false, std::memory_order_acq_rel)) {
        Log("F12 apply/refresh request consumed on Unity main thread.");
        ApplyOrRefreshReplacement();
        return;
    }

    if (g_pending_apply_frame >= 0) {
        UpdateReplacement(pump_frame);
        return;
    }

    if (g_mask_requested.exchange(
            false, std::memory_order_acq_rel)) {
        Log("F4 mod-hidden component toggle consumed on Unity main thread.");
        ToggleMaskVisibility();
        return;
    }

    if (g_buffer_probe_requested.exchange(
            false, std::memory_order_acq_rel)) {
        Log("F8 renderer buffer probe consumed on Unity main thread.");
        RunBufferProbe();
        return;
    }

    if (g_abi_probe_requested.exchange(
            false, std::memory_order_acq_rel)) {
        Log("F7 engine binding ABI probe consumed on Unity main thread.");
        RunAbiProbe();
        return;
    }

    if (g_texture_probe_requested.exchange(
            false, std::memory_order_acq_rel)) {
        Log("F6 material texture probe consumed on Unity main thread.");
        RunTextureProbe();
        return;
    }

    if (g_model_probe_requested.exchange(
            false, std::memory_order_acq_rel)) {
        Log("0 skin diagnostic request consumed on Unity main thread.");
        RunModelProbe();
        return;
    }

    if (g_model_probe_captures_remaining > 0 &&
        (g_model_probe_next_frame < 0 ||
         pump_frame >= g_model_probe_next_frame)) {
        ++g_model_probe_sequence;
        Log("0 skin diagnostic sample " +
            std::to_string(g_model_probe_sequence) + "/" +
            std::to_string(kModelProbeCaptureCount) +
            " at pump frame=" + std::to_string(pump_frame));
        ProbeSkinningState(g_model_probe_sequence,
            kModelProbeCaptureCount);
        --g_model_probe_captures_remaining;
        g_model_probe_next_frame = pump_frame +
            kModelProbeCaptureIntervalFrames;
        if (g_model_probe_captures_remaining == 0) {
            Log("0 skin diagnostic window complete.");
        }
        return;
    }

    UpdateReplacement(pump_frame);
}

void __fastcall DetourPump(void* method_info) {
    if (g_original_pump) {
        g_original_pump(method_info);
    }
    PumpCustomModel();
}

bool ResolveClass(
    const char* assembly, const char* namespc, const char* klass,
    BE_ResolvedClassV1& output) {
    output = {};
    const BE_Result status = g_host->resolve_class(
        g_host->context, assembly, namespc, klass, &output);
    if (status != BE_Result_Ok ||
        !output.class_info || !output.type_object) {
        Log(std::string("Required class not found: ") +
            namespc + "." + klass);
        return false;
    }
    return true;
}

bool ResolveContracts() {
    bool ready = true;
    for (auto& method : g_methods) {
        BE_ResolvedMethodV1 resolved{};
        const BE_Result result = g_host->resolve_method(
            g_host->context, &method.descriptor, &resolved);
        if (result == BE_Result_Ok &&
            resolved.method_info && resolved.method_pointer) {
            method.pointer = resolved.method_pointer;
            method.method_info = resolved.method_info;
            method.resolved = true;
        } else {
            Log(std::string(method.required ? "Required" : "Optional") +
                " method not found: " + method.key);
            if (method.required) ready = false;
        }
    }

    ready &= ResolveClass(
        "UnityEngine.CoreModule.dll", "UnityEngine",
        "SkinnedMeshRenderer", g_skinned_renderer_class);
    ready &= ResolveClass(
        "UnityEngine.CoreModule.dll", "UnityEngine",
        "Mesh", g_mesh_class);
    ready &= ResolveClass(
        "UnityEngine.CoreModule.dll", "UnityEngine",
        "Material", g_material_class);
    ready &= ResolveClass(
        "UnityEngine.CoreModule.dll", "UnityEngine",
        "Vector3", g_vector3_class);
    ready &= ResolveClass(
        "UnityEngine.CoreModule.dll", "UnityEngine",
        "Vector2", g_vector2_class);
    ready &= ResolveClass(
        "UnityEngine.CoreModule.dll", "UnityEngine",
        "BoneWeight", g_bone_weight_class);
    ready &= ResolveClass(
        "mscorlib.dll", "System", "Int32", g_int32_class);

    // Texture installation is optional: without it F12 still replaces the
    // geometry and keeps the original material.
    ResolveClass(
        "UnityEngine.CoreModule.dll", "UnityEngine", "Texture2D",
        g_texture2d_class);
    return ready;
}

bool InstallPumpHook() {
    constexpr std::array<std::string_view, 1> candidates{
        "pump.canvas_will_render"};

    for (std::string_view key : candidates) {
        MethodContract* method = Contract(key);
        if (!method || !method->resolved || !method->pointer) continue;

        void* original = nullptr;
        const BE_Result result = g_host->create_hook(
            g_host->context, kModuleId, method->pointer,
            reinterpret_cast<void*>(&DetourPump), &original);
        if (result == BE_Result_Ok) {
            g_original_pump =
                reinterpret_cast<VoidStaticFn>(original);
            Log(std::string("CustomModel main-thread pump installed: ") +
                method->key + " (after canvas callback, once per frame)");
            return true;
        }

        Log(std::string("CustomModel pump unavailable: ") +
            method->key + " result=" +
            std::to_string(static_cast<int>(result)));
    }
    return false;
}

bool StartHotkeyThread() {
    g_hotkey_thread_stop.store(false, std::memory_order_release);
    g_hotkey_thread = CreateThread(
        nullptr, 0, &HotkeyThread, nullptr, 0, nullptr);
    if (!g_hotkey_thread) {
        Log("Failed to start F12 hotkey latch thread.");
        return false;
    }
    return true;
}

void StopHotkeyThread() {
    g_hotkey_thread_stop.store(true, std::memory_order_release);
    if (g_hotkey_thread) {
        WaitForSingleObject(g_hotkey_thread, 1000);
        CloseHandle(g_hotkey_thread);
        g_hotkey_thread = nullptr;
    }
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host ||
        host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->resolve_method ||
        !host->resolve_field ||
        !host->resolve_class ||
        !host->object_new ||
        !host->runtime_invoke ||
        !host->object_unbox ||
        !host->gchandle_new ||
        !host->gchandle_free ||
        !host->copy_managed_string ||
        !host->copy_catalog_root ||
        !host->create_hook ||
        !host->release_module_hooks ||
        !host->log) {
        return BE_Result_InvalidArgument;
    }

    g_host = host;
    g_toggle_requested.store(false, std::memory_order_release);
    g_rollback_requested.store(false, std::memory_order_release);
    g_operation_in_progress.store(false, std::memory_order_release);
    g_pump_hits.store(0, std::memory_order_release);
    g_last_pump_frame = -1;
    g_pending_apply_frame = -1;
    ClearReplacementState();
    g_earliest_apply_frame = -1;

    if (!ResolveContracts()) {
        Log("CustomModel PoC-2.3 contract resolution failed.");
        return BE_Result_ContractMismatch;
    }
    if (!InstallPumpHook()) {
        Log("CustomModel PoC-2.3 could not install a main-thread pump.");
        return BE_Result_Conflict;
    }
    if (!StartHotkeyThread()) {
        g_host->release_module_hooks(g_host->context, kModuleId);
        g_original_pump = nullptr;
        return BE_Result_Failed;
    }

    const std::filesystem::path path = BemPath();
    Log("BetterEndfield.CustomModel 0.0.17 packed-skin ready. F12 replaces every "
        "component the payload carries and rebuilds the whole replacement on "
        "request; F11 cancels and rolls back. "
        "Each component reproduces its own live mesh's declaration and is "
        "verified before assignment. GraphicsBuffer is not used.");
    Log("F4 toggles the components the mod hides by default; in this sample "
        "that is its mask, which starts hidden.");
    Log("F8 reads renderer GraphicsBuffer metadata. This is the API family "
        "that crashed the client during PoC-3; press it deliberately.");
    Log("F7 validates the native ABI of the stripped engine bindings against "
        "the managed enumeration API. Run it before trusting raw writers.");
    Log("F6 reports the C9 material's texture slots with the size and format "
        "needed to map the EFMI DDS set onto them.");
    Log("0 arms 12 skin captures over roughly 6 seconds. Each capture records "
        "active gameplay/UI renderer roots and matrices, then CPU BakeMesh "
        "snapshots. Leave the faulty state untouched until the window completes; "
        "BakeMesh can perturb skinning timing.");
    if (!path.empty()) {
        Log("Expected BEM PoC-2.3 path: " + path.string());
    }
    if (!RawChannelContractsAvailable()) {
        Log("PoC-2.3 raw-channel capability is unavailable in this client; F12 will refuse safely.");
    } else {
        Log("PoC-2.3 Mesh CPU raw-channel contracts resolved.");
    }
    ProbeIcallTable();
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char*) {
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    StopHotkeyThread();
    if (g_host && g_host->release_module_hooks) {
        g_host->release_module_hooks(g_host->context, kModuleId);
    }

    // Shutdown may run outside the Unity main thread. Do not call Unity APIs:
    // this releases GC handles and forgets our bookkeeping, but leaves the
    // replacement standing until the scene reloads.
    ReleaseReplacementHandles();
    ReleaseRetainedAssets(false);
    g_replaced.clear();
    g_replacement_active = false;
    g_refresh_suspended = false;
    g_pending_apply_frame = -1;
    g_original_pump = nullptr;
    g_earliest_apply_frame = -1;
    g_last_pump_frame = -1;
    g_active_bem = {};
    g_next_refresh_tick = 0;
    g_toggle_requested.store(false, std::memory_order_release);
    g_rollback_requested.store(false, std::memory_order_release);
    g_operation_in_progress.store(false, std::memory_order_release);
    g_pump_hits.store(0, std::memory_order_release);
    g_host = nullptr;
