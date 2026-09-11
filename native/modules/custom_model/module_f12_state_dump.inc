// Tagged state dump for the production F12 replacement path.
//
// This replaces the probability/A-B diagnostic layers. F12/F11 keep using the
// normal production replacement state machine. A completed F12 generation is
// dumped automatically; the user then labels the exact active generation:
//
//   Num2 : label current F12 generation GOOD and dump it again
//   Num3 : label current F12 generation BAD  and dump it again
//
// The dump is read-only. It does not call BakeMesh or any GraphicsBuffer API.
// In addition to managed Mesh/renderer metadata it attempts to resolve
// UnityEngine.Object.m_CachedPtr and records the first 0x100 bytes of the native
// Unity object as qwords. This is intended for GOOD/BAD native-state diffs only;
// no native field is modified or dereferenced recursively.

constexpr int kF12DumpGoodHotkey = VK_NUMPAD2;
constexpr int kF12DumpBadHotkey = VK_NUMPAD3;
constexpr size_t kF12NativeDumpBytes = 0x100;

std::atomic_bool g_f12_dump_good_requested{false};
std::atomic_bool g_f12_dump_bad_requested{false};
std::atomic_bool g_f12_dump_hotkey_stop{false};
HANDLE g_f12_dump_hotkey_thread = nullptr;

const BE_HostApiV1* g_f12_dump_real_host = nullptr;
BE_HostApiV1 g_f12_dump_host_proxy{};
bool g_f12_dump_pump_intercepted = false;

uint64_t g_f12_dump_generation = 0;
uint64_t g_f12_dump_next_generation = 0;
bool g_f12_dump_waiting_for_apply = false;
int32_t g_f12_dump_last_frame = -1;
int32_t g_f12_dump_cached_ptr_offset = -1;
const void* g_f12_dump_get_instance_id_method = nullptr;

std::string F12DumpPointer(const void* pointer) {
    std::array<char, 32> buffer{};
    std::snprintf(buffer.data(), buffer.size(), "%p", pointer);
    return std::string(buffer.data());
}

bool F12DumpSafeCopy(const void* source, void* destination, size_t size) {
    if (!source || !destination || size == 0) return false;
    __try {
        std::memcpy(destination, source, size);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

void* F12DumpNativePointer(void* object) {
    if (!object || g_f12_dump_cached_ptr_offset < 0) return nullptr;
    void* native = nullptr;
    const auto* field = reinterpret_cast<const uint8_t*>(object) +
        static_cast<size_t>(g_f12_dump_cached_ptr_offset);
    return F12DumpSafeCopy(field, &native, sizeof(native)) ? native : nullptr;
}

bool F12DumpInstanceId(void* object, int32_t& output) {
    output = -1;
    if (!object || !g_f12_dump_get_instance_id_method || !g_host ||
        !g_host->runtime_invoke || !g_host->object_unbox) {
        return false;
    }
    void* exception = nullptr;
    void* boxed = g_host->runtime_invoke(g_host->context,
        g_f12_dump_get_instance_id_method, object, nullptr, &exception);
    if (exception || !boxed) return false;
    void* value = g_host->object_unbox(g_host->context, boxed);
    return value && F12DumpSafeCopy(value, &output, sizeof(output));
}

void F12DumpNativeWords(
    const char* kind, uint32_t component_id, void* managed_object) {
    const void* native = F12DumpNativePointer(managed_object);
    int32_t instance_id = -1;
    F12DumpInstanceId(managed_object, instance_id);

    std::array<uint64_t, kF12NativeDumpBytes / sizeof(uint64_t)> words{};
    if (!native || !F12DumpSafeCopy(native, words.data(), kF12NativeDumpBytes)) {
        Log("[F12-NATIVE] generation=" + std::to_string(g_f12_dump_generation) +
            " kind=" + kind + " C" + std::to_string(component_id) +
            " managed=" + F12DumpPointer(managed_object) +
            " instanceId=" + std::to_string(instance_id) +
            " native=" + F12DumpPointer(native) + " bytes=unavailable");
        return;
    }

    std::string text;
    text.reserve(words.size() * 22);
    for (size_t i = 0; i < words.size(); ++i) {
        std::array<char, 40> part{};
        std::snprintf(part.data(), part.size(), "%s%03zX:%016llX",
            i ? "," : "", i * sizeof(uint64_t),
            static_cast<unsigned long long>(words[i]));
        text += part.data();
    }
    Log("[F12-NATIVE] generation=" + std::to_string(g_f12_dump_generation) +
        " kind=" + kind + " C" + std::to_string(component_id) +
        " managed=" + F12DumpPointer(managed_object) +
        " instanceId=" + std::to_string(instance_id) +
        " native=" + F12DumpPointer(native) + " qwords=" + text);
}

std::string F12DumpDeclarationText(void* mesh) {
    VertexDeclaration declaration;
    if (!mesh || !ReadVertexDeclaration(mesh, declaration)) return "unreadable";
    std::string text;
    for (int32_t i = 0; i < declaration.count; ++i) {
        const auto& entry = declaration.entries[static_cast<size_t>(i)];
        if (!text.empty()) text += ";";
        text += "a" + std::to_string(entry.attribute) +
            ":f" + std::to_string(entry.format) +
            ":d" + std::to_string(entry.dimension) +
            ":s" + std::to_string(entry.stream);
    }
    return text;
}

void F12DumpMeshState(uint32_t component_id, void* mesh, const char* role) {
    if (!mesh) {
        Log("[F12-MESH] generation=" + std::to_string(g_f12_dump_generation) +
            " C" + std::to_string(component_id) + " role=" + role +
            " mesh=null");
        return;
    }

    int32_t instance_id = -1;
    F12DumpInstanceId(mesh, instance_id);
    void* native = F12DumpNativePointer(mesh);
    int32_t vertex_count = -1;
    int32_t sub_mesh_count = -1;
    int32_t influences = -1;
    uint8_t has_skin = 0;
    int32_t hide_flags = -1;
    InvokeValue(Contract("mesh.get_vertex_count"), mesh, nullptr, vertex_count);
    InvokeValue(Contract("mesh.get_sub_mesh_count"), mesh, nullptr,
        sub_mesh_count);
    InvokeValue(Contract("mesh.get_bones_per_vertex_value"), mesh, nullptr,
        influences);
    InvokeValue(Contract("mesh.has_bone_weights"), mesh, nullptr, has_skin);
    InvokeValue(Contract("object.get_hide_flags"), mesh, nullptr, hide_flags);

    uint64_t total_indices = 0;
    const int bounded_submeshes =
        std::min(std::max(sub_mesh_count, 0), kMaxSubMeshes);
    for (int i = 0; i < bounded_submeshes; ++i) {
        int32_t submesh = i;
        uint32_t count = 0;
        void* parameters[1]{&submesh};
        if (InvokeValue(Contract("mesh.get_index_count"), mesh, parameters,
                count)) {
            total_indices += count;
        }
    }

    void* bindposes = Invoke(Contract("mesh.get_bindposes"), mesh, nullptr,
        false);
    const int bindpose_count = ArrayLength(bindposes);

    std::vector<int32_t> strides;
    const bool strides_ok = ReadMeshStrides(mesh, strides);
    BoundsRaw bounds{};
    const bool bounds_ok = InvokeValue(Contract("mesh.get_bounds"), mesh,
        nullptr, bounds);

    Log("[F12-MESH] generation=" + std::to_string(g_f12_dump_generation) +
        " C" + std::to_string(component_id) + " role=" + role +
        " managed=" + F12DumpPointer(mesh) +
        " instanceId=" + std::to_string(instance_id) +
        " native=" + F12DumpPointer(native) +
        " alive=" + std::to_string(IsNativeObjectAlive(mesh)) +
        " name=\"" + ObjectName(mesh) + "\" vertexCount=" +
        std::to_string(vertex_count) + " totalIndices=" +
        std::to_string(total_indices) + " subMeshes=" +
        std::to_string(sub_mesh_count) + " bindposes=" +
        std::to_string(bindpose_count) + " hasSkin=" +
        std::to_string(has_skin) + " influences=" +
        std::to_string(influences) + " hideFlags=" +
        std::to_string(hide_flags) + " strides=" +
        (strides_ok ? StrideText(strides) : "unreadable") +
        " declaration=" + F12DumpDeclarationText(mesh) +
        (bounds_ok
            ? " boundsCenter=(" + FormatFloat3(bounds.center) +
                  ") boundsExtents=(" + FormatFloat3(bounds.extents) + ")"
            : " bounds=unreadable"));
}

const RendererSummary* F12DumpFindSummary(
    const std::vector<RendererSummary>& summaries, void* renderer) {
    for (const auto& item : summaries) {
        if (item.renderer == renderer) return &item;
    }
    return nullptr;
}

void F12DumpReplacementState(const char* phase, const char* label) {
    int32_t frame = -1;
    InvokeValue(Contract("time.get_frame_count"), nullptr, nullptr, frame);
    const std::vector<RendererSummary> summaries = EnumerateLiveEndminfRenderers();

    Log("[F12-DUMP] BEGIN generation=" +
        std::to_string(g_f12_dump_generation) + " phase=" + phase +
        " label=" + label + " frame=" + std::to_string(frame) +
        " replacementActive=" + std::to_string(g_replacement_active) +
        " records=" + std::to_string(g_replaced.size()) +
        " liveRenderers=" + std::to_string(summaries.size()) +
        " pendingApplyFrame=" + std::to_string(g_pending_apply_frame));

    std::vector<void*> dumped_custom_meshes;
    std::vector<void*> dumped_original_meshes;
    int live_records = 0;
    for (const ReplacedComponent& record : g_replaced) {
        if (!record.renderer) continue;
        ++live_records;
        const RendererSummary* summary =
            F12DumpFindSummary(summaries, record.renderer);
        void* current = IsNativeObjectAlive(record.renderer)
            ? Invoke(Contract("skinned.get_shared_mesh"), record.renderer,
                  nullptr, false)
            : nullptr;

        int32_t renderer_instance_id = -1;
        F12DumpInstanceId(record.renderer, renderer_instance_id);
        void* renderer_native = F12DumpNativePointer(record.renderer);
        int32_t current_instance_id = -1;
        F12DumpInstanceId(current, current_instance_id);

        Log("[F12-RENDERER] generation=" +
            std::to_string(g_f12_dump_generation) + " C" +
            std::to_string(record.component_id) + " renderer=" +
            F12DumpPointer(record.renderer) + " rendererInstanceId=" +
            std::to_string(renderer_instance_id) + " rendererNative=" +
            F12DumpPointer(renderer_native) + " rendererAlive=" +
            std::to_string(IsNativeObjectAlive(record.renderer)) +
            " currentMesh=" + F12DumpPointer(current) +
            " currentInstanceId=" + std::to_string(current_instance_id) +
            " originalMesh=" + F12DumpPointer(record.original_mesh) +
            " customMesh=" + F12DumpPointer(record.custom_mesh) +
            " currentIsOriginal=" +
            std::to_string(current == record.original_mesh) +
            " currentIsCustom=" + std::to_string(current == record.custom_mesh) +
            " hiddenByMod=" + std::to_string(record.hidden_by_mod) +
            " originalEnabled=" + std::to_string(record.original_enabled) +
            " liveSummary=" + std::to_string(summary != nullptr) +
            (summary ? " path=\"" + summary->path + "\"" : ""));

        F12DumpNativeWords("renderer", record.component_id, record.renderer);

        if (summary) {
            // Read-only binding/matrix/bone aggregate. Deliberately does not
            // call ProbeSkinnedSnapshot/BakeMesh.
            LogSkinProbeBinding(*summary);
        }

        if (record.custom_mesh &&
            std::find(dumped_custom_meshes.begin(), dumped_custom_meshes.end(),
                record.custom_mesh) == dumped_custom_meshes.end()) {
            dumped_custom_meshes.push_back(record.custom_mesh);
            F12DumpMeshState(record.component_id, record.custom_mesh, "custom");
            F12DumpNativeWords("customMesh", record.component_id,
                record.custom_mesh);
        }
        if (record.original_mesh &&
            std::find(dumped_original_meshes.begin(),
                dumped_original_meshes.end(), record.original_mesh) ==
                dumped_original_meshes.end()) {
            dumped_original_meshes.push_back(record.original_mesh);
            F12DumpMeshState(record.component_id, record.original_mesh,
                "original");
        }
    }

    Log("[F12-DUMP] END generation=" +
        std::to_string(g_f12_dump_generation) + " phase=" + phase +
        " label=" + label + " liveRecords=" +
        std::to_string(live_records) + " uniqueCustomMeshes=" +
        std::to_string(dumped_custom_meshes.size()) +
        " uniqueOriginalMeshes=" +
        std::to_string(dumped_original_meshes.size()));
}

void F12DumpTagCurrent(const char* label) {
    if (!g_replacement_active || g_replaced.empty()) {
        Log(std::string("[F12-LABEL] ") + label +
            " ignored: no active F12 replacement.");
        return;
    }
    if (g_f12_dump_generation == 0) {
        g_f12_dump_generation = ++g_f12_dump_next_generation;
        Log("[F12-DUMP] recovered an already-active replacement as generation=" +
            std::to_string(g_f12_dump_generation));
    }
    int32_t frame = -1;
    InvokeValue(Contract("time.get_frame_count"), nullptr, nullptr, frame);
    Log(std::string("[F12-LABEL] generation=") +
        std::to_string(g_f12_dump_generation) + " label=" + label +
        " frame=" + std::to_string(frame));
    F12DumpReplacementState("TAG", label);
}

DWORD WINAPI F12DumpHotkeyThread(void*) {
    bool good_held = false;
    bool bad_held = false;
    while (!g_f12_dump_hotkey_stop.load(std::memory_order_acquire)) {
        const bool good = (GetAsyncKeyState(kF12DumpGoodHotkey) & 0x8000) != 0;
        const bool bad = (GetAsyncKeyState(kF12DumpBadHotkey) & 0x8000) != 0;
        if (good && !good_held) {
            g_f12_dump_good_requested.store(true, std::memory_order_release);
        }
        if (bad && !bad_held) {
            g_f12_dump_bad_requested.store(true, std::memory_order_release);
        }
        good_held = good;
        bad_held = bad;
        Sleep(15);
    }
    return 0;
}

bool StartF12DumpHotkeys() {
    g_f12_dump_hotkey_stop.store(false, std::memory_order_release);
    g_f12_dump_hotkey_thread = CreateThread(
        nullptr, 0, &F12DumpHotkeyThread, nullptr, 0, nullptr);
    return g_f12_dump_hotkey_thread != nullptr;
}

void StopF12DumpHotkeys() {
    g_f12_dump_hotkey_stop.store(true, std::memory_order_release);
    if (!g_f12_dump_hotkey_thread) return;
    WaitForSingleObject(g_f12_dump_hotkey_thread, 1000);
    CloseHandle(g_f12_dump_hotkey_thread);
    g_f12_dump_hotkey_thread = nullptr;
}

void ResolveF12DumpIntrospection() {
    g_f12_dump_cached_ptr_offset = -1;
    g_f12_dump_get_instance_id_method = nullptr;
    if (!g_host) return;

    const BE_FieldDescriptorV1 cached_ptr{
        "UnityEngine.CoreModule.dll", "UnityEngine", "Object",
        "m_CachedPtr", "System.IntPtr"};
    BE_ResolvedFieldV1 field{};
    if (g_host->resolve_field &&
        g_host->resolve_field(g_host->context, &cached_ptr, &field) ==
            BE_Result_Ok && field.field_info && field.offset >= 0) {
        g_f12_dump_cached_ptr_offset = field.offset;
        Log("[F12-DUMP] resolved UnityEngine.Object.m_CachedPtr offset=" +
            std::to_string(field.offset));
    } else {
        Log("[F12-DUMP] UnityEngine.Object.m_CachedPtr unavailable; native byte dump disabled.");
    }

    const BE_MethodDescriptorV1 instance_id{
        "UnityEngine.CoreModule.dll", "UnityEngine", "Object",
        "GetInstanceID", nullptr, "System.Int32", 0};
    BE_ResolvedMethodV1 method{};
    if (g_host->resolve_method &&
        g_host->resolve_method(g_host->context, &instance_id, &method) ==
            BE_Result_Ok && method.method_info) {
        g_f12_dump_get_instance_id_method = method.method_info;
        Log("[F12-DUMP] resolved UnityEngine.Object.GetInstanceID().");
    } else {
        Log("[F12-DUMP] Object.GetInstanceID unavailable; pointer identities remain enabled.");
    }
}

void PumpF12StateDump() {
    const int32_t frame = g_last_pump_frame;
    if (frame < 0 || frame == g_f12_dump_last_frame) return;
    g_f12_dump_last_frame = frame;

    // The production F12 path queues an apply and returns. Seeing a new pending
    // apply therefore marks the generation before any BuildMeshFromComponent
    // call for that generation runs on a later pump frame.
    if (g_pending_apply_frame >= 0 && !g_f12_dump_waiting_for_apply) {
        g_f12_dump_generation = ++g_f12_dump_next_generation;
        g_f12_dump_waiting_for_apply = true;
        Log("[F12-DUMP] generation=" +
            std::to_string(g_f12_dump_generation) + " QUEUED frame=" +
            std::to_string(frame) + " pendingApplyFrame=" +
            std::to_string(g_pending_apply_frame));
    }

    if (g_f12_dump_waiting_for_apply && g_pending_apply_frame < 0) {
        if (g_replacement_active && !g_replaced.empty()) {
            Log("[F12-DUMP] generation=" +
                std::to_string(g_f12_dump_generation) +
                " APPLY_COMPLETE frame=" + std::to_string(frame));
            F12DumpReplacementState("POST_APPLY", "UNLABELED");
        } else {
            Log("[F12-DUMP] generation=" +
                std::to_string(g_f12_dump_generation) +
                " APPLY_CANCELLED_OR_FAILED frame=" +
                std::to_string(frame));
        }
        g_f12_dump_waiting_for_apply = false;
    }

    // BAD wins if both keys were latched before the same Unity frame.
    if (g_f12_dump_bad_requested.exchange(false,
            std::memory_order_acq_rel)) {
        g_f12_dump_good_requested.store(false, std::memory_order_release);
        F12DumpTagCurrent("BAD");
        return;
    }
    if (g_f12_dump_good_requested.exchange(false,
            std::memory_order_acq_rel)) {
        F12DumpTagCurrent("GOOD");
    }
}

void __fastcall DetourPumpWithF12Dump(void* method_info) {
    DetourPump(method_info);
    PumpF12StateDump();
}

BE_Result BE_CALL F12DumpCreateHook(
    void* context, const char* module_id, void* target, void* detour,
    void** original) {
    if (!g_f12_dump_real_host || !g_f12_dump_real_host->create_hook) {
        return BE_Result_NotReady;
    }
    if (detour == reinterpret_cast<void*>(&DetourPump)) {
        g_f12_dump_pump_intercepted = true;
        return g_f12_dump_real_host->create_hook(
            context, module_id, target,
            reinterpret_cast<void*>(&DetourPumpWithF12Dump), original);
    }
    return g_f12_dump_real_host->create_hook(
        context, module_id, target, detour, original);
}

BE_Result BE_CALL InitializeWithF12Dump(const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;
    g_f12_dump_real_host = host;
    g_f12_dump_host_proxy = *host;
    g_f12_dump_host_proxy.create_hook = &F12DumpCreateHook;
    g_f12_dump_pump_intercepted = false;
    g_f12_dump_generation = 0;
    g_f12_dump_next_generation = 0;
    g_f12_dump_waiting_for_apply = false;
    g_f12_dump_last_frame = -1;
    g_f12_dump_good_requested.store(false, std::memory_order_release);
    g_f12_dump_bad_requested.store(false, std::memory_order_release);

    const BE_Result result = Initialize(&g_f12_dump_host_proxy);
    if (result != BE_Result_Ok) {
        g_f12_dump_real_host = nullptr;
        return result;
    }
    if (!g_f12_dump_pump_intercepted) {
        Log("[F12-DUMP] failed to intercept production pump; state dump disabled.");
        Shutdown();
        g_f12_dump_real_host = nullptr;
        return BE_Result_Failed;
    }

    ResolveF12DumpIntrospection();
    if (!StartF12DumpHotkeys()) {
        Log("[F12-DUMP] failed to start GOOD/BAD tag hotkey thread.");
        Shutdown();
        g_f12_dump_real_host = nullptr;
        return BE_Result_Failed;
    }

    Log("[F12-DUMP] production F12 dump ready: F12=normal rebuild, F11=normal rollback, Num2=GOOD tag+dump, Num3=BAD tag+dump.");
    Log("[F12-DUMP] probability A/B, recommit and cached-generation diagnostics are not active in this build.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithF12Dump() {
    StopF12DumpHotkeys();
    g_f12_dump_good_requested.store(false, std::memory_order_release);
    g_f12_dump_bad_requested.store(false, std::memory_order_release);
    g_f12_dump_waiting_for_apply = false;
    g_f12_dump_last_frame = -1;
    g_f12_dump_cached_ptr_offset = -1;
    g_f12_dump_get_instance_id_method = nullptr;

    Shutdown();
    g_f12_dump_real_host = nullptr;
    g_f12_dump_host_proxy = {};
}
