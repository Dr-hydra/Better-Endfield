// In-place recommit diagnostic for cached custom Mesh generations.
//
// This layer extends module_generation_diag.inc without changing F12/F11 or
// the BEM format. The cached generation must be restored to the original model
// before any operation below, so the same runtime Mesh object can be mutated
// while detached and then rebound with the normal '-' diagnostic key.
//
// Numpad / : UploadMeshData(false) only.
// Numpad * : Mesh.MarkModified + UploadMeshData(false).
// Numpad - : rewrite the same VB0/VB1/VB2 + IB bytes, then MarkModified+Upload.
// Numpad + : rebuild layout/index/submesh/bindposes in-place on the same Mesh,
//            then MarkModified+Upload.

constexpr int kGenerationRecommitUploadHotkey = VK_DIVIDE;
constexpr int kGenerationRecommitMarkUploadHotkey = VK_MULTIPLY;
constexpr int kGenerationRecommitRewriteHotkey = VK_SUBTRACT;
constexpr int kGenerationRecommitRebuildHotkey = VK_ADD;

std::atomic_bool g_generation_recommit_upload_requested{false};
std::atomic_bool g_generation_recommit_mark_upload_requested{false};
std::atomic_bool g_generation_recommit_rewrite_requested{false};
std::atomic_bool g_generation_recommit_rebuild_requested{false};
std::atomic_bool g_generation_recommit_hotkey_stop{false};
HANDLE g_generation_recommit_hotkey_thread = nullptr;
int32_t g_generation_recommit_last_frame = -1;

const BE_HostApiV1* g_generation_recommit_real_host = nullptr;
BE_HostApiV1 g_generation_recommit_host_proxy{};
bool g_generation_recommit_pump_intercepted = false;

using Il2CppResolveIcallFn = void*(__cdecl*)(const char* name);
using MeshMarkModifiedFn = void(*)(void* mesh);
MeshMarkModifiedFn g_generation_recommit_mark_modified = nullptr;

bool ResolveGenerationMarkModified() {
    if (g_generation_recommit_mark_modified) return true;
    HMODULE game_assembly = GetModuleHandleW(L"GameAssembly.dll");
    if (!game_assembly) {
        Log("[GEN-RECOMMIT] GameAssembly.dll is unavailable; MarkModified cannot resolve.");
        return false;
    }
    auto resolve = reinterpret_cast<Il2CppResolveIcallFn>(
        GetProcAddress(game_assembly, "il2cpp_resolve_icall"));
    if (!resolve) {
        Log("[GEN-RECOMMIT] il2cpp_resolve_icall export is unavailable.");
        return false;
    }
    constexpr std::array<const char*, 2> names{{
        "UnityEngine.Mesh::MarkModified",
        "UnityEngine.Mesh::MarkModified()"}};
    for (const char* name : names) {
        void* pointer = resolve(name);
        if (!pointer) continue;
        g_generation_recommit_mark_modified =
            reinterpret_cast<MeshMarkModifiedFn>(pointer);
        Log(std::string("[GEN-RECOMMIT] resolved ") + name + " -> " +
            GenerationDiagPointer(pointer));
        return true;
    }
    Log("[GEN-RECOMMIT] UnityEngine.Mesh::MarkModified did not resolve.");
    return false;
}

#if defined(_MSC_VER)
// Kept free of unwindable objects so __try is legal here.
static bool SafeInvokeMeshMarkModified(MeshMarkModifiedFn fn, void* mesh) {
    __try {
        fn(mesh);
        return true;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}
#endif

bool SafeGenerationMarkModified(void* mesh) {
    if (!mesh || !ResolveGenerationMarkModified()) return false;
#if defined(_MSC_VER)
    if (!SafeInvokeMeshMarkModified(g_generation_recommit_mark_modified, mesh)) {
        Log("[GEN-RECOMMIT] Mesh.MarkModified native call faulted.");
        return false;
    }
    return true;
#else
    g_generation_recommit_mark_modified(mesh);
    return true;
#endif
}

const BemComponent* GenerationRecommitComponent(
    const BemPocData& bem, uint32_t component_id) {
    for (const auto& component : bem.components) {
        if (component.info.component_id == component_id) return &component;
    }
    return nullptr;
}

bool GenerationRecommitPreflight(const char* label) {
    if (!GenerationDiagOfficialPathIdle()) {
        Log(std::string("[GEN-RECOMMIT] ") + label +
            " refused: normal F12 replacement is active/pending.");
        return false;
    }
    if (g_generation_diag_records.empty()) {
        Log(std::string("[GEN-RECOMMIT] ") + label +
            " has no cached generation; press 9 first.");
        return false;
    }
    if (g_generation_diag_attached) {
        Log(std::string("[GEN-RECOMMIT] ") + label +
            " refused: cached generation is attached; press '=' first.");
        return false;
    }
    for (const auto& record : g_generation_diag_records) {
        if (!IsNativeObjectAlive(record.renderer) ||
            !IsNativeObjectAlive(record.original_mesh) ||
            !IsNativeObjectAlive(record.custom_mesh)) {
            Log(std::string("[GEN-RECOMMIT] ") + label +
                " refused: generation contains a dead object; build a fresh generation with 9.");
            return false;
        }
        void* current = Invoke(Contract("skinned.get_shared_mesh"),
            record.renderer, nullptr, false);
        if (current != record.original_mesh) {
            Log(std::string("[GEN-RECOMMIT] ") + label + " refused: C" +
                std::to_string(record.component_id) + " renderer is not on its original Mesh; press '=' first.");
            return false;
        }
    }
    if (!ResolveEngineBindings()) {
        Log(std::string("[GEN-RECOMMIT] ") + label +
            " refused: raw Mesh engine bindings are unavailable.");
        return false;
    }
    return true;
}

bool LoadGenerationRecommitBem(BemPocData& bem) {
    const std::filesystem::path path = BemPath();
    if (path.empty() || !LoadBemPoc(path, bem)) {
        Log("[GEN-RECOMMIT] BEM payload could not be reloaded for in-place rewrite.");
        return false;
    }
    for (const auto& record : g_generation_diag_records) {
        const BemComponent* component =
            GenerationRecommitComponent(bem, record.component_id);
        if (!component ||
            (component->info.flags & kComponentFlagNoGeometry) != 0) {
            Log("[GEN-RECOMMIT] cached C" +
                std::to_string(record.component_id) +
                " has no matching geometry payload.");
            return false;
        }
    }
    return true;
}

bool RewriteGenerationBuffers(
    const GenerationDiagRecord& record, const BemComponent& component) {
    for (int32_t stream = 0;
         stream < static_cast<int32_t>(kBemStreamCount); ++stream) {
        const auto& bytes = component.streams[static_cast<size_t>(stream)];
        if (!SafeSetVertexBufferData(record.custom_mesh, stream, bytes.data(),
                static_cast<int32_t>(bytes.size()))) {
            Log("[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
                " VB rewrite faulted on stream " + std::to_string(stream));
            return false;
        }
    }
    if (!SafeSetIndexBufferData(record.custom_mesh, component.indices.data(),
            static_cast<int32_t>(component.indices.size()))) {
        Log("[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
            " IB rewrite faulted.");
        return false;
    }
    return true;
}

bool RebuildGenerationMeshInPlace(
    const GenerationDiagRecord& record, const BemComponent& component) {
    const auto& info = component.info;
    VertexDeclaration declaration;
    std::vector<int32_t> source_strides;
    if (!ReadVertexDeclaration(record.original_mesh, declaration) ||
        !ReadMeshStrides(record.original_mesh, source_strides)) {
        Log("[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
            " source declaration/strides are unreadable.");
        return false;
    }
    const std::vector<int32_t> payload_strides{
        static_cast<int32_t>(info.stride0),
        static_cast<int32_t>(info.stride1),
        static_cast<int32_t>(info.stride2)};
    if (source_strides != payload_strides) {
        Log("[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
            " source/payload strides changed; full in-place rebuild refused.");
        return false;
    }

    std::array<VertexAttributeDescriptorRaw, kMaxVertexAttributes> attributes{};
    for (int32_t i = 0; i < declaration.count; ++i) {
        attributes[static_cast<size_t>(i)] =
            declaration.entries[static_cast<size_t>(i)];
    }
    const int32_t vertex_count = static_cast<int32_t>(info.vertex_count);
    const int32_t index_count = static_cast<int32_t>(info.index_count);
    if (!SafeSetVertexBufferParams(record.custom_mesh, vertex_count,
            attributes.data(), declaration.count)) {
        Log("[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
            " SetVertexBufferParams faulted during in-place rebuild.");
        return false;
    }
    if (!RewriteGenerationBuffers(record, component)) return false;
    if (!SafeSetIndexBufferParams(
            record.custom_mesh, index_count, kIndexFormatUInt16)) {
        Log("[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
            " SetIndexBufferParams faulted during in-place rebuild.");
        return false;
    }
    // SetIndexBufferParams may reallocate the backing store, so write indices
    // once more after the parameter call.
    if (!SafeSetIndexBufferData(record.custom_mesh, component.indices.data(),
            static_cast<int32_t>(component.indices.size()))) {
        Log("[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
            " final IB write faulted during in-place rebuild.");
        return false;
    }

    int sub_mesh_count = 1;
    void* p_sub_mesh_count[1]{&sub_mesh_count};
    if (!InvokeVoid(Contract("mesh.set_sub_mesh_count"), record.custom_mesh,
            p_sub_mesh_count)) {
        Log("[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
            " submesh-count restore failed.");
        return false;
    }
    SubMeshDescriptorRaw descriptor{};
    descriptor.topology = kTopologyTriangles;
    descriptor.index_start = 0;
    descriptor.index_count = index_count;
    descriptor.base_vertex = 0;
    descriptor.first_vertex = 0;
    descriptor.vertex_count = vertex_count;
    if (!SafeSetSubMesh(record.custom_mesh, 0, &descriptor)) {
        Log("[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
            " SetSubMesh faulted during in-place rebuild.");
        return false;
    }

    void* bindposes = Invoke(
        Contract("mesh.get_bindposes"), record.original_mesh, nullptr);
    if (!bindposes || ArrayLength(bindposes) <= static_cast<int>(info.max_bone)) {
        Log("[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
            " original bindpose palette became unavailable.");
        return false;
    }
    void* p_bindposes[1]{bindposes};
    if (!InvokeVoid(Contract("mesh.set_bindposes"), record.custom_mesh,
            p_bindposes) ||
        !InvokeVoid(Contract("mesh.recalculate_bounds"), record.custom_mesh,
            nullptr)) {
        Log("[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
            " bindpose/bounds restore failed during in-place rebuild.");
        return false;
    }

    if (!MatchesDeclaration(record.custom_mesh, declaration, source_strides,
            "[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
            " rebuilt")) {
        Log("[GEN-RECOMMIT] C" + std::to_string(record.component_id) +
            " declaration changed after in-place rebuild.");
        return false;
    }
    return true;
}

enum class GenerationRecommitMode {
    UploadOnly,
    MarkAndUpload,
    RewriteAndCommit,
    RebuildAndCommit,
};

const char* GenerationRecommitModeName(GenerationRecommitMode mode) {
    switch (mode) {
    case GenerationRecommitMode::UploadOnly: return "upload-only";
    case GenerationRecommitMode::MarkAndUpload: return "mark+upload";
    case GenerationRecommitMode::RewriteAndCommit: return "rewrite+mark+upload";
    case GenerationRecommitMode::RebuildAndCommit: return "rebuild+mark+upload";
    }
    return "unknown";
}

void RunGenerationRecommit(GenerationRecommitMode mode) {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) {
        Log(std::string("[GEN-RECOMMIT] ") +
            GenerationRecommitModeName(mode) + " ignored: CustomModel is busy.");
        return;
    }
    const auto unlock = []() {
        g_operation_in_progress.store(false, std::memory_order_release);
    };
    const char* label = GenerationRecommitModeName(mode);
    if (!GenerationRecommitPreflight(label)) {
        unlock();
        return;
    }

    const bool needs_mark = mode != GenerationRecommitMode::UploadOnly;
    if (needs_mark && !ResolveGenerationMarkModified()) {
        Log(std::string("[GEN-RECOMMIT] ") + label +
            " refused because MarkModified is unavailable.");
        unlock();
        return;
    }

    BemPocData bem;
    const bool needs_payload =
        mode == GenerationRecommitMode::RewriteAndCommit ||
        mode == GenerationRecommitMode::RebuildAndCommit;
    if (needs_payload && !LoadGenerationRecommitBem(bem)) {
        unlock();
        return;
    }

    int touched = 0;
    for (const auto& record : g_generation_diag_records) {
        const BemComponent* component = needs_payload ?
            GenerationRecommitComponent(bem, record.component_id) : nullptr;
        bool ok = true;
        if (mode == GenerationRecommitMode::RewriteAndCommit) {
            ok = component && RewriteGenerationBuffers(record, *component);
        } else if (mode == GenerationRecommitMode::RebuildAndCommit) {
            ok = component && RebuildGenerationMeshInPlace(record, *component);
        }
        if (ok && needs_mark) ok = SafeGenerationMarkModified(record.custom_mesh);
        if (ok) ok = SafeUploadMeshData(record.custom_mesh);
        if (!ok) {
            Log(std::string("[GEN-RECOMMIT] ") + label + " failed on C" +
                std::to_string(record.component_id) + " customMesh=" +
                GenerationDiagPointer(record.custom_mesh) +
                "; generation may now be partially recommitted. Restore with '=' and build a new generation with 9 before comparing further.");
            unlock();
            return;
        }
        ++touched;
        Log(std::string("[GEN-RECOMMIT] ") + label + " generation=" +
            std::to_string(g_generation_diag_id) + " C" +
            std::to_string(record.component_id) + " customMesh=" +
            GenerationDiagPointer(record.custom_mesh));
    }

    Log(std::string("[GEN-RECOMMIT] ") + label + " generation=" +
        std::to_string(g_generation_diag_id) + " complete: meshesTouched=" +
        std::to_string(touched) +
        " meshesBuilt=0 meshesDestroyed=0. Press '-' to reattach these exact Mesh pointers and compare their previous behaviour.");
    unlock();
}

DWORD WINAPI GenerationRecommitHotkeyThread(void*) {
    struct Latch {
        int key;
        std::atomic_bool* flag;
        bool held = false;
    };
    std::array<Latch, 4> latches{{
        {kGenerationRecommitUploadHotkey,
            &g_generation_recommit_upload_requested, false},
        {kGenerationRecommitMarkUploadHotkey,
            &g_generation_recommit_mark_upload_requested, false},
        {kGenerationRecommitRewriteHotkey,
            &g_generation_recommit_rewrite_requested, false},
        {kGenerationRecommitRebuildHotkey,
            &g_generation_recommit_rebuild_requested, false}}};
    while (!g_generation_recommit_hotkey_stop.load(std::memory_order_acquire)) {
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

bool StartGenerationRecommitHotkeys() {
    g_generation_recommit_hotkey_stop.store(false, std::memory_order_release);
    g_generation_recommit_hotkey_thread = CreateThread(
        nullptr, 0, &GenerationRecommitHotkeyThread, nullptr, 0, nullptr);
    return g_generation_recommit_hotkey_thread != nullptr;
}

void StopGenerationRecommitHotkeys() {
    g_generation_recommit_hotkey_stop.store(true, std::memory_order_release);
    if (!g_generation_recommit_hotkey_thread) return;
    WaitForSingleObject(g_generation_recommit_hotkey_thread, 1000);
    CloseHandle(g_generation_recommit_hotkey_thread);
    g_generation_recommit_hotkey_thread = nullptr;
}

void PumpGenerationRecommitDiag() {
    const int32_t frame = g_last_pump_frame;
    if (frame < 0 || frame == g_generation_recommit_last_frame) return;
    g_generation_recommit_last_frame = frame;

    // Strongest mutation wins when multiple numpad keys land in one frame.
    if (g_generation_recommit_rebuild_requested.exchange(
            false, std::memory_order_acq_rel)) {
        g_generation_recommit_rewrite_requested.store(false,
            std::memory_order_release);
        g_generation_recommit_mark_upload_requested.store(false,
            std::memory_order_release);
        g_generation_recommit_upload_requested.store(false,
            std::memory_order_release);
        Log("[GEN-RECOMMIT] Numpad+ full in-place rebuild request consumed on Unity main thread.");
        RunGenerationRecommit(GenerationRecommitMode::RebuildAndCommit);
        return;
    }
    if (g_generation_recommit_rewrite_requested.exchange(
            false, std::memory_order_acq_rel)) {
        g_generation_recommit_mark_upload_requested.store(false,
            std::memory_order_release);
        g_generation_recommit_upload_requested.store(false,
            std::memory_order_release);
        Log("[GEN-RECOMMIT] Numpad- raw buffer rewrite request consumed on Unity main thread.");
        RunGenerationRecommit(GenerationRecommitMode::RewriteAndCommit);
        return;
    }
    if (g_generation_recommit_mark_upload_requested.exchange(
            false, std::memory_order_acq_rel)) {
        g_generation_recommit_upload_requested.store(false,
            std::memory_order_release);
        Log("[GEN-RECOMMIT] Numpad* MarkModified+Upload request consumed on Unity main thread.");
        RunGenerationRecommit(GenerationRecommitMode::MarkAndUpload);
        return;
    }
    if (g_generation_recommit_upload_requested.exchange(
            false, std::memory_order_acq_rel)) {
        Log("[GEN-RECOMMIT] Numpad/ UploadMeshData-only request consumed on Unity main thread.");
        RunGenerationRecommit(GenerationRecommitMode::UploadOnly);
    }
}

void __fastcall DetourPumpWithGenerationRecommitDiag(void* method_info) {
    // This wrapper replaces DetourPumpWithGenerationDiag at hook installation,
    // so calling it directly here invokes the engine callback exactly once and
    // then runs the base + cached-generation pumps before our extra controls.
    DetourPumpWithGenerationDiag(method_info);
    PumpGenerationRecommitDiag();
}

BE_Result BE_CALL GenerationRecommitCreateHook(
    void* context, const char* module_id, void* target, void* detour,
    void** original) {
    if (!g_generation_recommit_real_host ||
        !g_generation_recommit_real_host->create_hook) {
        return BE_Result_NotReady;
    }
    if (detour == reinterpret_cast<void*>(&DetourPumpWithGenerationDiag)) {
        g_generation_recommit_pump_intercepted = true;
        return g_generation_recommit_real_host->create_hook(
            context, module_id, target,
            reinterpret_cast<void*>(&DetourPumpWithGenerationRecommitDiag),
            original);
    }
    return g_generation_recommit_real_host->create_hook(
        context, module_id, target, detour, original);
}

BE_Result BE_CALL InitializeWithGenerationRecommitDiag(
    const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;
    g_generation_recommit_real_host = host;
    g_generation_recommit_host_proxy = *host;
    g_generation_recommit_host_proxy.create_hook =
        &GenerationRecommitCreateHook;
    g_generation_recommit_pump_intercepted = false;
    g_generation_recommit_last_frame = -1;
    g_generation_recommit_mark_modified = nullptr;
    g_generation_recommit_upload_requested.store(false,
        std::memory_order_release);
    g_generation_recommit_mark_upload_requested.store(false,
        std::memory_order_release);
    g_generation_recommit_rewrite_requested.store(false,
        std::memory_order_release);
    g_generation_recommit_rebuild_requested.store(false,
        std::memory_order_release);

    const BE_Result result =
        InitializeWithGenerationDiag(&g_generation_recommit_host_proxy);
    if (result != BE_Result_Ok) {
        g_generation_recommit_real_host = nullptr;
        return result;
    }
    if (!g_generation_recommit_pump_intercepted) {
        Log("[GEN-RECOMMIT] failed to intercept generation diagnostic pump; numpad controls disabled.");
        ShutdownWithGenerationDiag();
        g_generation_recommit_real_host = nullptr;
        return BE_Result_Failed;
    }
    if (!StartGenerationRecommitHotkeys()) {
        Log("[GEN-RECOMMIT] failed to start numpad diagnostic hotkey thread.");
        ShutdownWithGenerationDiag();
        g_generation_recommit_real_host = nullptr;
        return BE_Result_Failed;
    }

    Log("[GEN-RECOMMIT] in-place cached-Mesh controls ready: Num/=Upload only, Num*=MarkModified+Upload, Num-=rewrite VB/IB+commit, Num+=full in-place layout rebuild+commit.");
    Log("[GEN-RECOMMIT] always press '=' first; every operation keeps the same custom Mesh object pointers and requires '-' afterwards to compare behaviour.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithGenerationRecommitDiag() {
    StopGenerationRecommitHotkeys();
    g_generation_recommit_upload_requested.store(false,
        std::memory_order_release);
    g_generation_recommit_mark_upload_requested.store(false,
        std::memory_order_release);
    g_generation_recommit_rewrite_requested.store(false,
        std::memory_order_release);
    g_generation_recommit_rebuild_requested.store(false,
        std::memory_order_release);
    g_generation_recommit_last_frame = -1;
    g_generation_recommit_mark_modified = nullptr;

    ShutdownWithGenerationDiag();
    g_generation_recommit_real_host = nullptr;
    g_generation_recommit_host_proxy = {};
}
