// Snapshots and explicit main-thread experiments for the verified player.
// No setters, buffer acquisition, or managed calls from the render hook.
struct MeshTraceIdentity {
    uintptr_t mesh = 0;
    int32_t id = 0;
    uint32_t component = 0;
    uint64_t generation = 0, birth = 0;
};
struct MeshTraceEvent {
    MeshTraceIdentity identity{};
    uintptr_t renderer = 0, matrix_ptr = 0;
    int32_t renderer_id = 0, result = 0;
    int32_t draw_influences = -1;
    uint32_t current = 0, previous = 0, native_influences = 0;
    uint64_t tick = 0;
};
std::array<MeshTraceIdentity, 512> g_mesh_trace_registry{};
size_t g_mesh_trace_registry_size = 0;
std::array<MeshTraceEvent, 512> g_mesh_trace_events{};
size_t g_mesh_trace_events_size = 0;
struct MeshTraceSeen { uintptr_t renderer = 0; uint64_t tick = 0; };
std::array<MeshTraceSeen, 128> g_mesh_trace_seen{};
SRWLOCK g_mesh_trace_lock = SRWLOCK_INIT;
std::atomic_bool g_mesh_trace_enabled{false};
std::atomic_uint64_t g_mesh_trace_until{0}, g_mesh_trace_dropped{0};
uint64_t g_mesh_trace_generation = 0, g_mesh_trace_birth = 0, g_mesh_trace_sample = 0;
int32_t g_mesh_trace_cached_offset = -1;
std::filesystem::path g_mesh_trace_directory;
// Verified RVA 119F794: renderer subobject=this+0x38, output list, context;
// returns the output record index or -1. Each record is 0x218 bytes.
using MeshTraceDrawFn = int32_t(__fastcall*)(void*, void*, void*);
MeshTraceDrawFn g_mesh_trace_original_draw = nullptr;
std::atomic_bool g_mesh_trace_field_requested{false};
struct MeshTraceFieldTest {
    MeshTraceIdentity identity{};
    uint32_t original = 0, corrected = 0;
    bool applied = false;
};
std::vector<MeshTraceFieldTest> g_mesh_trace_field_tests;
std::atomic_bool g_mesh_trace_rebind_requested{false};
struct MeshTraceRebind {
    MeshTraceIdentity identity{};
    void* renderer = nullptr;
    void* source = nullptr;
    void* custom = nullptr;
    uintptr_t renderer_native = 0;
    int32_t renderer_id = 0;
    bool detached = false;
};
std::vector<MeshTraceRebind> g_mesh_trace_rebinds;
int32_t g_mesh_trace_rebind_frame = -1;

bool MeshTraceCopy(const void* source, void* destination, size_t size) {
    if (!source || !destination || !size) return false;
    __try { std::memcpy(destination, source, size); return true; }
    __except (EXCEPTION_EXECUTE_HANDLER) { return false; }
}
template<class T> bool MeshTraceRead(uintptr_t base, size_t offset, T& value) {
    value = {};
    return base && MeshTraceCopy(reinterpret_cast<void*>(base + offset), &value, sizeof(value));
}
uintptr_t MeshTraceNative(void* object) {
    uintptr_t ptr = 0;
    if (g_mesh_trace_cached_offset >= 0)
        MeshTraceRead(reinterpret_cast<uintptr_t>(object), g_mesh_trace_cached_offset, ptr);
    return ptr;
}
std::string MeshTracePtr(uintptr_t ptr) {
    char text[24]{};
    std::snprintf(text, sizeof(text), "%llx", static_cast<unsigned long long>(ptr));
    return text;
}
MeshTraceIdentity MeshTraceFind(uintptr_t mesh, int32_t id) {
    MeshTraceIdentity found{};
    AcquireSRWLockShared(&g_mesh_trace_lock);
    for (size_t i = g_mesh_trace_registry_size; i > 0; --i) {
        const auto& r = g_mesh_trace_registry[i - 1];
        if (r.mesh == mesh && r.id == id) { found = r; break; }
    }
    ReleaseSRWLockShared(&g_mesh_trace_lock);
    return found;
}
std::string MeshTraceLabel(const MeshTraceIdentity& r) {
    return "G" + std::to_string(r.generation) + "-B" + std::to_string(r.birth) +
        "-C" + std::to_string(r.component);
}
bool MeshTraceFile(const std::string& name, const void* bytes, size_t size) {
    std::ofstream file(g_mesh_trace_directory / name, std::ios::binary);
    if (size) file.write(static_cast<const char*>(bytes), static_cast<std::streamsize>(size));
    return file.good();
}
void MeshTraceSnapshot(const MeshTraceIdentity& r, const BemComponent* expected,
                       uintptr_t mesh, const std::string& stage) {
    if (!g_mesh_trace_enabled.load() || !mesh) return;
    std::array<uint8_t, 0x200> header{};
    std::array<uint8_t, 0x1B0> data_header{};
    uintptr_t data = 0, vertex_bytes = 0, submeshes = 0;
    int32_t vertices = 0;
    uint32_t native_influences = 0, dirty = 0, channel_mask = 0;
    const auto label = MeshTraceLabel(r) + "-" + stage;
    if (!MeshTraceCopy(reinterpret_cast<void*>(mesh), header.data(), header.size()) ||
        !MeshTraceRead(mesh, 0x38, data) ||
        !MeshTraceCopy(reinterpret_cast<void*>(data), data_header.data(), data_header.size())) {
        Log("[MESH-TRACE] " + label + " unreadable native mesh/data"); return;
    }
    MeshTraceRead(mesh, 0x1C8, native_influences);
    MeshTraceRead(mesh, 0x80, dirty);
    MeshTraceRead(data, 0x78, vertices);
    MeshTraceRead(data, 0x74, channel_mask);
    MeshTraceRead(data, 0xA0, vertex_bytes);
    MeshTraceRead(data, 0xD0, submeshes);
    bool saved = MeshTraceFile(label + "-mesh.bin", header.data(), header.size());
    saved &= MeshTraceFile(label + "-data.bin", data_header.data(), data_header.size());
    std::string detail;
    if (vertices < 0 || vertices > 1000000) {
        Log("[MESH-TRACE] " + label + " native vertex count rejected=" + std::to_string(vertices)); return;
    }
    for (uint32_t stream = 0; stream < 4; ++stream) {
        uint32_t mask = 0, offset = 0, stride = 0;
        MeshTraceRead(data, 0x40 + stream * 12, mask);
        MeshTraceRead(data, 0x44 + stream * 12, offset);
        MeshTraceRead(data, 0x48 + stream * 12, stride);
        if (!mask) continue;
        const size_t size = static_cast<size_t>(vertices) * stride;
        if (!stride || stride > 256 || size > 16 * 1024 * 1024 || offset > 64 * 1024 * 1024) {
            detail += " VB" + std::to_string(stream) + "=invalid-range"; continue;
        }
        std::vector<uint8_t> bytes(size);
        if (!size || !vertex_bytes || !MeshTraceCopy(reinterpret_cast<void*>(vertex_bytes + offset), bytes.data(), size)) {
            detail += " VB" + std::to_string(stream) + "=unreadable"; continue;
        }
        saved &= MeshTraceFile(label + "-vb" + std::to_string(stream) + ".bin", bytes.data(), bytes.size());
        detail += " VB" + std::to_string(stream) + "=" + std::to_string(size) + "/stride" + std::to_string(stride);
        if (expected && stream < kBemStreamCount) {
            const auto& payload = expected->streams[stream];
            if (payload.size() != bytes.size()) detail += "/size-diff";
            else {
                size_t differences = 0, first = size;
                for (size_t i = 0; i < size; ++i) if (bytes[i] != payload[i]) {
                    ++differences; if (first == size) first = i;
                }
                detail += "/diff=" + std::to_string(differences);
                if (differences) detail += "/first=" + std::to_string(first);
            }
        }
    }
    uint64_t submesh_count = 0;
    MeshTraceRead(data, 0xE0, submesh_count);
    BoundsRaw bounds{};
    if (submeshes && submesh_count && submesh_count <= 256 && MeshTraceRead(submeshes, 8, bounds)) {
        saved &= MeshTraceFile(label + "-submesh0-bounds.bin", &bounds, sizeof(bounds));
        const float* values = reinterpret_cast<const float*>(&bounds);
        detail += " subBounds=";
        for (int i = 0; i < 6; ++i) detail += (i ? "," : "") + std::to_string(values[i]);
    }
    // Files intentionally retain exact bytes. No artifact hashes are calculated.
    Log("[MESH-TRACE] " + label + " mesh=" + MeshTracePtr(mesh) + " id=" + std::to_string(r.id) +
        " native1c8=" + std::to_string(native_influences) + " dirty=" + std::to_string(dirty) +
        " channels=" + std::to_string(channel_mask) + " vertices=" + std::to_string(vertices) +
        " saved=" + std::to_string(saved) + detail);
}
void MeshTraceBuild(const BemComponent& component, void* source, void* built, const char* stage) {
    if (!g_mesh_trace_enabled.load()) return;
    try {
        const uintptr_t mesh = MeshTraceNative(built);
        int32_t id = 0;
        if (!mesh || !MeshTraceRead(mesh, 8, id)) return;
        auto r = MeshTraceFind(mesh, id);
        if (!r.mesh) {
            r = {mesh, id, component.info.component_id, g_mesh_trace_generation, ++g_mesh_trace_birth};
            AcquireSRWLockExclusive(&g_mesh_trace_lock);
            if (g_mesh_trace_registry_size == g_mesh_trace_registry.size()) {
                // Retain recent identities; files preserve older generations.
                std::move(g_mesh_trace_registry.begin() + 256, g_mesh_trace_registry.end(), g_mesh_trace_registry.begin());
                g_mesh_trace_registry_size = 256;
            }
            g_mesh_trace_registry[g_mesh_trace_registry_size++] = r;
            ReleaseSRWLockExclusive(&g_mesh_trace_lock);
            MeshTraceSnapshot(r, nullptr, MeshTraceNative(source), "SOURCE");
        }
        MeshTraceSnapshot(r, &component, mesh, stage);
    } catch (...) { Log("[MESH-TRACE] snapshot failed; replacement unchanged."); }
}
int32_t __fastcall MeshTraceDrawHook(void* renderer_subobject, void* output, void* context) {
    const int32_t result = g_mesh_trace_original_draw(renderer_subobject, output, context);
    if (!g_mesh_trace_enabled.load(std::memory_order_relaxed)) return result;
    const auto now = GetTickCount64();
    if (now >= g_mesh_trace_until.load(std::memory_order_relaxed)) return result;
    if (result < 0 || result > 1000000) return result;
    const auto subobject = reinterpret_cast<uintptr_t>(renderer_subobject);
    if (subobject < 0x38) return result;
    const auto ptr = subobject - 0x38;
    uintptr_t records = 0;
    uint8_t packed = 0;
    if (!MeshTraceRead(reinterpret_cast<uintptr_t>(output), 0, records) ||
        !MeshTraceRead(records, static_cast<size_t>(result) * 0x218 + 0x160, packed)) return result;
    uintptr_t mesh = 0;
    int32_t id = 0;
    if (!MeshTraceRead(ptr, 0x298, mesh) || !MeshTraceRead(mesh, 8, id)) return result;
    // Never wait for the main thread, log, allocate, or call managed code here.
    if (!TryAcquireSRWLockExclusive(&g_mesh_trace_lock)) { ++g_mesh_trace_dropped; return result; }
    MeshTraceIdentity identity{};
    for (size_t i = g_mesh_trace_registry_size; i > 0; --i)
        if (g_mesh_trace_registry[i-1].mesh == mesh && g_mesh_trace_registry[i-1].id == id) {
            identity = g_mesh_trace_registry[i-1]; break;
        }
    if (identity.mesh) {
        auto* seen = &g_mesh_trace_seen[(ptr >> 4) % g_mesh_trace_seen.size()];
        if (seen->renderer != ptr || now - seen->tick >= 500) {
            *seen = {ptr, now};
            if (g_mesh_trace_events_size < g_mesh_trace_events.size()) {
                auto& e = g_mesh_trace_events[g_mesh_trace_events_size++];
                e = {}; e.identity = identity; e.renderer = ptr; e.result = result; e.tick = now;
                e.draw_influences = (packed >> 1) & 7;
                MeshTraceRead(ptr, 8, e.renderer_id);
                MeshTraceRead(ptr, 0x374, e.current);
                MeshTraceRead(ptr, 0x370, e.previous);
                MeshTraceRead(ptr, 0x388, e.matrix_ptr);
                MeshTraceRead(mesh, 0x1C8, e.native_influences);
            } else ++g_mesh_trace_dropped;
        }
    }
    ReleaseSRWLockExclusive(&g_mesh_trace_lock);
    return result;
}
void MeshTraceDrain() {
    if (!g_mesh_trace_enabled.load()) return;
    std::array<MeshTraceEvent, 512> events{};
    AcquireSRWLockExclusive(&g_mesh_trace_lock);
    const size_t count = g_mesh_trace_events_size;
    std::copy_n(g_mesh_trace_events.begin(), count, events.begin());
    g_mesh_trace_events_size = 0;
    ReleaseSRWLockExclusive(&g_mesh_trace_lock);
    for (size_t i = 0; i < count; ++i) {
        const auto& e = events[i];
        Log("[MESH-TRACE] DRAW " + MeshTraceLabel(e.identity) + " tick=" + std::to_string(e.tick) +
            " renderer=" + MeshTracePtr(e.renderer) + " rid=" + std::to_string(e.renderer_id) +
            " mesh=" + MeshTracePtr(e.identity.mesh) + " native1c8=" + std::to_string(e.native_influences) +
            " matrixOffset=" + std::to_string(e.current) + " previous=" + std::to_string(e.previous) +
            " matrixPtr=" + MeshTracePtr(e.matrix_ptr) + " drawInfluences=" + std::to_string(e.draw_influences) +
            " recordIndex=" + std::to_string(e.result));
    }
    static uint64_t next_drop_report = 0;
    if (GetTickCount64() >= next_drop_report) {
        next_drop_report = GetTickCount64() + 5000;
        const auto dropped = g_mesh_trace_dropped.exchange(0);
        if (dropped) Log("[MESH-TRACE] dropped hook events in interval=" + std::to_string(dropped));
    }
}
void MeshTraceCapture(const char* tag) {
    if (!g_mesh_trace_enabled.load()) return;
    g_mesh_trace_until.store(GetTickCount64() + 6000);
    const std::string stage = std::string(tag) + "-S" + std::to_string(++g_mesh_trace_sample);
    Log("[MESH-TRACE] SAMPLE " + stage + " latestGeneration=" + std::to_string(g_mesh_trace_generation));
    std::unordered_set<uintptr_t> captured;
    for (const auto& replacement : g_replaced) {
        if (!replacement.renderer || !IsNativeObjectAlive(replacement.renderer)) continue;
        void* current = Invoke(Contract("skinned.get_shared_mesh"), replacement.renderer, nullptr, false);
        const uintptr_t mesh = MeshTraceNative(current);
        int32_t id = 0;
        if (!MeshTraceRead(mesh, 8, id)) continue;
        const auto r = MeshTraceFind(mesh, id);
        if (!r.mesh) continue;
        Log("[MESH-TRACE] LINK " + stage + " " + MeshTraceLabel(r) + " renderer=" +
            MeshTracePtr(MeshTraceNative(replacement.renderer)) + " name=" + ObjectName(replacement.renderer));
        if (captured.insert(mesh).second) {
            const BemComponent* expected = nullptr;
            for (const auto& c : g_active_bem.components) if (c.info.component_id == r.component) { expected = &c; break; }
            MeshTraceSnapshot(r, expected, mesh, stage);
        }
    }
}
void MeshTraceBegin() {
    if (!g_mesh_trace_enabled.load()) return;
    ++g_mesh_trace_generation;
    g_mesh_trace_field_tests.clear();
    g_mesh_trace_until.store(GetTickCount64() + 6000);
    Log("[MESH-TRACE] BEGIN G" + std::to_string(g_mesh_trace_generation));
}
bool MeshTraceWriteInfluences(uintptr_t mesh, uint32_t value) {
    // One aligned 32-bit field only. Never applies to a source asset.
    __try { *reinterpret_cast<volatile uint32_t*>(mesh + 0x1C8) = value; return true; }
    __except (EXCEPTION_EXECUTE_HANDLER) { return false; }
}
void MeshTraceToggleInfluences() {
    if (!g_mesh_trace_enabled.load() || !g_replacement_active) {
        Log("[MESH-TRACE] FIELD-TEST skipped: no active diagnostic replacement."); return;
    }
    std::unordered_set<uintptr_t> seen;
    const auto base = reinterpret_cast<uintptr_t>(GetModuleHandleW(L"UnityPlayer.dll"));
    size_t changed = 0;
    for (const auto& replacement : g_replaced) {
        if (!replacement.renderer ||
            !IsNativeObjectAlive(replacement.renderer) || !IsNativeObjectAlive(replacement.original_mesh)) continue;
        void* current = Invoke(Contract("skinned.get_shared_mesh"), replacement.renderer, nullptr, false);
        if (!current || current != replacement.custom_mesh || current == replacement.original_mesh) continue;
        const auto mesh = MeshTraceNative(current), source = MeshTraceNative(replacement.original_mesh);
        if (!mesh || !source || mesh == source || !seen.insert(mesh).second) continue;
        uintptr_t mesh_type = 0, source_type = 0;
        int32_t id = 0;
        uint32_t value = 0, source_value = 0;
        if (!MeshTraceRead(mesh, 0, mesh_type) || !MeshTraceRead(source, 0, source_type) ||
            mesh_type != base + 0x19C8230 || source_type != mesh_type ||
            !MeshTraceRead(mesh, 8, id) || !MeshTraceRead(mesh, 0x1C8, value) ||
            !MeshTraceRead(source, 0x1C8, source_value) || (source_value != 1 && source_value != 2 && source_value != 4)) {
            Log("[MESH-TRACE] FIELD-TEST refused: source/type validation failed."); continue;
        }
        const auto identity = MeshTraceFind(mesh, id);
        if (!identity.mesh || identity.component != replacement.component_id) continue;
        auto found = std::find_if(g_mesh_trace_field_tests.begin(), g_mesh_trace_field_tests.end(),
            [&](const auto& t) { return t.identity.mesh == mesh && t.identity.id == id; });
        if (found == g_mesh_trace_field_tests.end()) {
            // Allocate the restoration record before writing anything.
            g_mesh_trace_field_tests.push_back({identity, value, source_value, false});
            found = g_mesh_trace_field_tests.end() - 1;
        }
        const uint32_t expected = found->applied ? found->corrected : found->original;
        const uint32_t desired = found->applied ? found->original : found->corrected;
        if (value != expected) {
            Log("[MESH-TRACE] FIELD-TEST refused: field changed externally."); continue;
        }
        uint32_t readback = 0;
        if (!MeshTraceWriteInfluences(mesh, desired) || !MeshTraceRead(mesh, 0x1C8, readback) || readback != desired) {
            // Best-effort restore on a failed write verification.
            MeshTraceWriteInfluences(mesh, value);
            Log("[MESH-TRACE] FIELD-TEST write/readback failed."); continue;
        }
        found->applied = !found->applied;
        ++changed;
        Log("[MESH-TRACE] FIELD-TEST " + MeshTraceLabel(identity) + " action=" +
            (found->applied ? "source-value" : "original-value") + " mesh=" + MeshTracePtr(mesh) +
            " before=" + std::to_string(value) + " after=" + std::to_string(readback) +
            " source=" + std::to_string(source_value) + " meshIdentityUnchanged=1");
    }
    if (!changed) Log("[MESH-TRACE] FIELD-TEST: no eligible current custom mesh.");
    MeshTraceCapture("FIELD-TEST");
}
void MeshTraceStartRebind(int32_t frame) {
    if (!g_mesh_trace_enabled.load() || !g_replacement_active ||
        g_mesh_trace_rebind_frame >= 0) return;
    MeshTraceCapture("REBIND-BEFORE");
    std::vector<MeshTraceRebind> plan;
    plan.reserve(g_replaced.size());
    std::unordered_set<void*> seen;
    for (const auto& r : g_replaced) {
        if (!r.custom_mesh || !IsNativeObjectAlive(r.renderer) ||
            !IsNativeObjectAlive(r.original_mesh) || !IsNativeObjectAlive(r.custom_mesh) ||
            r.custom_mesh == r.original_mesh || !seen.insert(r.renderer).second) continue;
        if (Invoke(Contract("skinned.get_shared_mesh"), r.renderer, nullptr, false) != r.custom_mesh) continue;
        const auto mesh = MeshTraceNative(r.custom_mesh), renderer = MeshTraceNative(r.renderer);
        int32_t id = 0, renderer_id = 0;
        if (!MeshTraceRead(mesh, 8, id) || !MeshTraceRead(renderer, 8, renderer_id)) continue;
        const auto identity = MeshTraceFind(mesh, id);
        if (!identity.mesh || identity.component != r.component_id) continue;
        // Existing replacement records and retained assets keep all three alive.
        // Allocate every plan entry before the first setter.
        plan.push_back({identity, r.renderer, r.original_mesh, r.custom_mesh, renderer, renderer_id, false});
    }
    if (plan.empty()) { Log("[MESH-TRACE] REBIND: no eligible current custom meshes."); return; }
    g_mesh_trace_rebinds = std::move(plan);
    g_mesh_trace_rebind_frame = frame + 2;
    for (auto& r : g_mesh_trace_rebinds) {
        // Mark before invoking: the pump can still recover if an invocation throws.
        r.detached = true;
        const bool ok = SetSharedMesh(r.renderer, r.source);
        Log("[MESH-TRACE] REBIND-DETACH " + MeshTraceLabel(r.identity) +
            " renderer=" + MeshTracePtr(r.renderer_native) + " ok=" + std::to_string(ok) +
            " frame=" + std::to_string(frame) + " meshRetained=1");
    }
}
bool MeshTracePumpRebind(int32_t frame) {
    if (g_mesh_trace_rebind_frame < 0) return false;
    if (frame < g_mesh_trace_rebind_frame) return true;
    // The main pump defers reconciliation, F12, F11 and diagnostic keys until
    // this finishes, so replacement handles cannot be released in the gap.
    for (auto& r : g_mesh_trace_rebinds) {
        if (!r.detached) continue;
        r.detached = false;
        try {
            int32_t mesh_id = 0, renderer_id = 0;
            if (!IsNativeObjectAlive(r.renderer) || !IsNativeObjectAlive(r.custom) ||
                MeshTraceNative(r.renderer) != r.renderer_native ||
                MeshTraceNative(r.custom) != r.identity.mesh ||
                !MeshTraceRead(r.renderer_native, 8, renderer_id) || renderer_id != r.renderer_id ||
                !MeshTraceRead(r.identity.mesh, 8, mesh_id) || mesh_id != r.identity.id) {
                Log("[MESH-TRACE] REBIND skipped stale identity " + MeshTraceLabel(r.identity)); continue;
            }
            void* current = Invoke(Contract("skinned.get_shared_mesh"), r.renderer, nullptr, false);
            if (current != r.source && current != r.custom) {
                Log("[MESH-TRACE] REBIND skipped external mesh change " + MeshTraceLabel(r.identity)); continue;
            }
            const bool was_source = current == r.source;
            const bool ok = SetSharedMesh(r.renderer, r.custom);
            uint32_t value = 0;
            const bool field_read = MeshTraceRead(r.identity.mesh, 0x1C8, value);
            Log("[MESH-TRACE] REBIND-ATTACH " + MeshTraceLabel(r.identity) +
                " renderer=" + MeshTracePtr(r.renderer_native) + " ok=" + std::to_string(ok) +
                " wasSource=" + std::to_string(was_source) + " frame=" + std::to_string(frame) +
                " native1c8=" + std::to_string(value) + " fieldRead=" + std::to_string(field_read) +
                " meshIdentityUnchanged=1");
        } catch (...) { Log("[MESH-TRACE] REBIND attach exception; normal reconciliation will resume."); }
    }
    g_mesh_trace_rebind_frame = -1;
    g_mesh_trace_rebinds.clear();
    MeshTraceCapture("REBIND-AFTER");
    return true;
}
void MeshTraceInitialize(const BE_HostApiV1* host) {
    // Verify six independent icall RVAs before using version-specific native fields.
    const uintptr_t base = reinterpret_cast<uintptr_t>(GetModuleHandleW(L"UnityPlayer.dll"));
    using Resolve = void*(*)(const char*);
    const auto resolve = reinterpret_cast<Resolve>(GetProcAddress(GetModuleHandleW(L"GameAssembly.dll"), "il2cpp_resolve_icall"));
    struct Expected { const char* name; uintptr_t rva; };
    const Expected expected[]{{"UnityEngine.Mesh::InternalSetVertexBufferData", 0xF993B0},
        {"UnityEngine.Mesh::SetSubMesh_Injected", 0xF99F9C}, {"UnityEngine.Mesh::UploadMeshDataImpl", 0x19BCC0},
        {"UnityEngine.Mesh::HasBoneWeights", 0xF9901C}, {"UnityEngine.Mesh::GetBonesPerVertexValue", 0x545490},
        {"UnityEngine.SkinnedMeshRenderer::get_sharedMesh", 0x199BD0}};
    if (!base || !resolve) return;
    for (const auto& e : expected) {
        void* address = nullptr;
        if (!SafeResolveIcall(resolve, e.name, &address) || reinterpret_cast<uintptr_t>(address) != base + e.rva) {
            Log("[MESH-TRACE] unsupported UnityPlayer; native diagnostics disabled."); return;
        }
    }
    BE_ResolvedFieldV1 field{};
    const BE_FieldDescriptorV1 desc{"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "m_CachedPtr", "System.IntPtr"};
    if (!host->resolve_field || host->resolve_field(host->context, &desc, &field) != BE_Result_Ok || field.offset < 0) return;
    g_mesh_trace_cached_offset = field.offset;
    try {
        wchar_t local[MAX_PATH]{};
        const DWORD length = GetEnvironmentVariableW(L"LOCALAPPDATA", local, MAX_PATH);
        if (!length || length >= MAX_PATH) return;
        g_mesh_trace_directory = std::filesystem::path(local) / L"BetterEndfield" / L"logs" / L"mesh-trace" /
            (std::to_wstring(GetCurrentProcessId()) + L"-" + std::to_wstring(GetTickCount64()));
        std::filesystem::create_directories(g_mesh_trace_directory);
    } catch (...) { Log("[MESH-TRACE] cannot create snapshot directory; diagnostics disabled."); return; }
    const uint8_t draw_prefix[]{0x48,0x89,0x5c,0x24,0x10,0x48,0x89,0x6c,0x24,0x18};
    std::array<uint8_t, sizeof(draw_prefix)> live_prefix{};
    if (!MeshTraceCopy(reinterpret_cast<void*>(base + 0x119F794), live_prefix.data(), live_prefix.size()) ||
        std::memcmp(live_prefix.data(), draw_prefix, sizeof(draw_prefix)) != 0) {
        Log("[MESH-TRACE] draw entry validation failed; field experiment disabled."); return;
    }
    const auto result = host->create_hook(host->context, kModuleId, reinterpret_cast<void*>(base + 0x119F794),
        reinterpret_cast<void*>(&MeshTraceDrawHook), reinterpret_cast<void**>(&g_mesh_trace_original_draw));
    // Keep any installed detour resident even when optional diagnostics fail.
    if (result != BE_Result_Ok || !g_mesh_trace_original_draw) {
        Log("[MESH-TRACE] draw hook unavailable; native snapshots and field experiment remain enabled.");
    }
    g_mesh_trace_enabled.store(true);
    Log("[MESH-TRACE] v0.0.44: native snapshots, actual draw records; 9 toggles all current custom m_BonesPerVertex; 8 rebinds the same meshes across two frames; files=" + g_mesh_trace_directory.string());
}
