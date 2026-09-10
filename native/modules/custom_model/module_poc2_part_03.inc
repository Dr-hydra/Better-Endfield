    if (!BuildMeshFromBem(bem, source->mesh, custom_mesh)) {
        return false;
    }

    TargetCounts applied{};
    for (RendererSummary* item : targets) {
        if (!SetSharedMesh(item->renderer, custom_mesh)) {
            Log("Failed to assign custom C9 Mesh to live target:");
            LogRenderer(*item, applied.total);
            continue;
        }
        void* observed = Invoke(
            Contract("skinned.get_shared_mesh"), item->renderer, nullptr, false);
        if (observed != custom_mesh) {
            Log("sharedMesh read-back mismatch on live C9 target; restoring it.");
            SetSharedMesh(item->renderer, item->mesh);
            continue;
        }
        CountTarget(*item, applied);
    }

    if (applied.total == 0) {
        Log("Custom C9 Mesh was built, but no live renderer accepted it.");
        DestroyCustomMesh(custom_mesh);
        return false;
    }

    g_replaced_renderer = source->renderer;
    g_original_mesh = source->mesh;
    g_custom_mesh = custom_mesh;

    if (g_host->gchandle_new) {
        g_renderer_handle = g_host->gchandle_new(
            g_host->context, g_replaced_renderer, 0);
        g_original_mesh_handle = g_host->gchandle_new(
            g_host->context, g_original_mesh, 0);
        g_custom_mesh_handle = g_host->gchandle_new(
            g_host->context, g_custom_mesh, 0);
    }

    Log("F9 replacement active: applied=" + std::to_string(applied.total) +
        " gameplay=" + std::to_string(applied.gameplay) +
        " ui=" + std::to_string(applied.ui) +
        ". New live gameplay/UI clones will be refreshed automatically.");
    return true;
}

void RefreshActiveReplacement() {
    if (!g_custom_mesh || !g_original_mesh) return;

    std::vector<RendererSummary> summaries =
        EnumerateLiveComponent9Renderers(false);
    TargetCounts applied{};
    for (auto& item : summaries) {
        if (!IsOriginalLiveComponent9(item)) continue;
        if (!SetSharedMesh(item.renderer, g_custom_mesh)) continue;
        void* observed = Invoke(
            Contract("skinned.get_shared_mesh"), item.renderer, nullptr, false);
        if (observed == g_custom_mesh) {
            CountTarget(item, applied);
        }
    }

    if (applied.total > 0) {
        Log("Active CustomModel refresh applied to new live C9 instances: total=" +
            std::to_string(applied.total) +
            " gameplay=" + std::to_string(applied.gameplay) +
            " ui=" + std::to_string(applied.ui));
    }
}

bool RollbackReplacement() {
    if (!g_custom_mesh || !g_original_mesh) {
        ClearReplacementState();
        return true;
    }

    std::vector<RendererSummary> summaries =
        EnumerateLiveComponent9Renderers(true);
    TargetCounts restored{};
    for (auto& item : summaries) {
        if (!IsCustomLiveComponent9(item)) continue;
        if (!SetSharedMesh(item.renderer, g_original_mesh)) continue;
        void* observed = Invoke(
            Contract("skinned.get_shared_mesh"), item.renderer, nullptr, false);
        if (observed == g_original_mesh) {
            CountTarget(item, restored);
        }
    }

    void* doomed = g_custom_mesh;
    ClearReplacementState();
    DestroyCustomMesh(doomed);
    Log("F9 rollback complete: restored=" + std::to_string(restored.total) +
        " gameplay=" + std::to_string(restored.gameplay) +
        " ui=" + std::to_string(restored.ui));
    return true;
}

void ToggleReplacement() {
    if (g_operation_in_progress.exchange(
            true, std::memory_order_acq_rel)) {
        return;
    }

    if (g_custom_mesh) {
        RollbackReplacement();
    } else {
        ApplyReplacement();
    }

    g_operation_in_progress.store(false, std::memory_order_release);
}

DWORD WINAPI HotkeyThread(void*) {
    bool held = false;
    while (!g_hotkey_thread_stop.load(std::memory_order_acquire)) {
        const bool down =
            (GetAsyncKeyState(kToggleHotkey) & 0x8000) != 0;
        if (down && !held) {
            g_toggle_requested.store(true, std::memory_order_release);
        }
        held = down;
        Sleep(15);
    }
    return 0;
}

void PumpCustomModel() {
    const uint64_t hit =
        g_pump_hits.fetch_add(1, std::memory_order_relaxed) + 1;
    if (hit == 1) {
        Log("CustomModel main-thread pump observed first runtime call.");
    }

    if (g_toggle_requested.exchange(
            false, std::memory_order_acq_rel)) {
        Log("F9 CustomModel request consumed on Unity main thread.");
        ToggleReplacement();
        return;
    }

    // Character detail/party UI creates chr_0003_endminf_uimodel(Clone)
    // after gameplay is already running. While F9 is active, periodically
    // discover newly-created live instances and attach the same custom mesh.
    if (g_custom_mesh && hit % kActiveRefreshInterval == 0 &&
        !g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        RefreshActiveReplacement();
        g_operation_in_progress.store(false, std::memory_order_release);
    }
}

void __fastcall DetourPump(void* instance, void* method_info) {
    if (g_original_pump) {
        g_original_pump(instance, method_info);
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
        "Vector3", g_vector3_class);
    ready &= ResolveClass(
        "UnityEngine.CoreModule.dll", "UnityEngine",
        "Vector2", g_vector2_class);
    ready &= ResolveClass(
        "UnityEngine.CoreModule.dll", "UnityEngine",
        "Color", g_color_class);
    ready &= ResolveClass(
        "UnityEngine.CoreModule.dll", "UnityEngine",
        "BoneWeight", g_bone_weight_class);
    ready &= ResolveClass(
        "mscorlib.dll", "System", "Int32", g_int32_class);
    return ready;
}

bool InstallPumpHook() {
    constexpr std::array<std::string_view, 3> candidates{
        "pump.process_dither_pitch",
        "pump.evaluate_touched_entities",
        "pump.process_dither_trace"};

    for (std::string_view key : candidates) {
        MethodContract* method = Contract(key);
        if (!method || !method->resolved || !method->pointer) continue;

        void* original = nullptr;
        const BE_Result result = g_host->create_hook(
            g_host->context, kModuleId, method->pointer,
            reinterpret_cast<void*>(&DetourPump), &original);
        if (result == BE_Result_Ok) {
            g_original_pump =
                reinterpret_cast<VoidInstanceFn>(original);
            Log(std::string("CustomModel main-thread pump installed: ") +
                method->key);
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
        Log("Failed to start F9 hotkey latch thread.");
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
        !host->copy_managed_string ||
        !host->copy_catalog_root ||
        !host->create_hook ||
        !host->release_module_hooks ||
        !host->log) {
        return BE_Result_InvalidArgument;
    }

    g_host = host;
    g_toggle_requested.store(false, std::memory_order_release);
    g_operation_in_progress.store(false, std::memory_order_release);
    g_pump_hits.store(0, std::memory_order_release);
    ClearReplacementState();

    if (!ResolveContracts()) {
        Log("CustomModel PoC-2 contract resolution failed.");
        return BE_Result_ContractMismatch;
    }
    if (!InstallPumpHook()) {
        Log("CustomModel PoC-2 could not install a main-thread pump.");
        return BE_Result_Conflict;
    }
    if (!StartHotkeyThread()) {
        g_host->release_module_hooks(g_host->context, kModuleId);
        g_original_pump = nullptr;
        return BE_Result_Failed;
    }

    const std::filesystem::path path = BemPath();
    Log("BetterEndfield.CustomModel PoC-2 ready. "
        "Load Endministrator (F), then press F9.");
    if (!path.empty()) {
        Log("Expected BEM PoC path: " + path.string());
    }
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

    // Shutdown may run outside the Unity main thread. Do not call Unity APIs.
    ReleaseReplacementHandles();
    g_replaced_renderer = nullptr;
    g_original_mesh = nullptr;
    g_custom_mesh = nullptr;
    g_original_pump = nullptr;
    g_toggle_requested.store(false, std::memory_order_release);
    g_operation_in_progress.store(false, std::memory_order_release);
    g_pump_hits.store(0, std::memory_order_release);
    g_host = nullptr;
