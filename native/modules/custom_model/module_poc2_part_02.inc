    summaries.reserve(static_cast<size_t>(count));

    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (renderer) summaries.push_back(SummarizeRenderer(renderer));
    }
    Log("SkinnedMeshRenderer objects discovered=" + std::to_string(raw_count));
    return summaries;
}

std::vector<RendererSummary> EnumerateLiveComponent9Renderers(
    bool log_discovered = false) {
    std::vector<RendererSummary> summaries;
    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* objects = Invoke(
        Contract("resources.find_objects_of_type_all"), nullptr, parameters);
    const int raw_count = ArrayLength(objects);
    const int count = std::max(raw_count, 0);

    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (!renderer || ObjectName(renderer) != kTargetRendererName) continue;

        const std::string path = BuildTransformPath(renderer);
        if (!IsLod0Path(path) || !IsSupportedLiveTargetPath(path)) continue;

        summaries.push_back(SummarizeRenderer(renderer));
    }

    if (log_discovered) {
        Log("Live Component9 renderer scan: allSMR=" +
            std::to_string(raw_count) +
            " liveC9=" + std::to_string(summaries.size()));
    }
    return summaries;
}

bool IsActiveSceneRenderer(void* renderer) {
    void* go = Invoke(Contract("component.get_game_object"), renderer, nullptr, false);
    uint8_t active = 0;
    // Scene's only unboxed field is its Int32 handle (validated in the dump).
    int32_t scene_handle = 0;
    return go && InvokeValue(Contract("game_object.get_active_in_hierarchy"),
            go, nullptr, active) && active &&
        InvokeValue(Contract("game_object.get_scene"), go, nullptr, scene_handle) &&
        scene_handle != 0;
}

// Every active scene lod0 renderer of the Endministrator model, not Component9 alone.
// Extending the replacement past C9 needs the whole model's declarations, and
// the join key onto EFMI's components is the original index count, so this
// filters on path alone and keeps whatever it finds.
std::vector<RendererSummary> EnumerateLiveEndminfRenderers() {
    std::vector<RendererSummary> summaries;
    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* objects = Invoke(
        Contract("resources.find_objects_of_type_all"), nullptr, parameters);
    const int raw_count = ArrayLength(objects);
    const int count = std::max(raw_count, 0);

    std::vector<std::string> live_roots;
    std::vector<std::string> rejected_roots;
    std::vector<std::string> outside_prefix;
    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (!renderer) continue;
        // FindObjectsOfTypeAll also hands back every loaded asset, which is
        // where nearly all of these objects come from. The name costs one
        // managed string read while the transform chain below costs up to 24,
        // so the name decides whether a path is worth building at all. This
        // scan runs about once a second while a replacement is active.
        const std::string renderer_name = ObjectName(renderer);
        if (renderer_name.find("endminf") == std::string::npos) continue;
        if (renderer_name.find(kActorMeshPrefix) != 0) {
            // Names the character but not in the form the payload was built
            // for. Nothing downstream looks at these, so a screen that draws
            // the model with one of them would silently keep its original
            // geometry; they are named so that screen can be identified
            // instead of guessed at.
            outside_prefix.push_back(renderer_name + " @ " +
                BuildTransformPath(renderer));
            continue;
        }
        const std::string path = BuildTransformPath(renderer);
        if (!IsEndminfLod0Draw(renderer_name, path)) {
            rejected_roots.push_back(PathRoot(path));
            continue;
        }
        // Leave templates and pooled/inactive instances on original meshes so
        // their next activation can complete the game's normal initialization.
        if (!IsActiveSceneRenderer(renderer)) continue;
        live_roots.push_back(PathRoot(path));
        summaries.push_back(SummarizeRenderer(renderer, path));
    }

    static int last_count = -1;
    static size_t last_live_count = static_cast<size_t>(-1);
    if (last_count != raw_count || last_live_count != summaries.size()) {
        last_count = raw_count;
        last_live_count = summaries.size();
        Log("Endminf lod0 renderer scan: allSMR=" + std::to_string(raw_count) +
            " scanned=" + std::to_string(count) +
            " live=" + std::to_string(summaries.size()));
    }
    // Logged only when the set changes. Now that every container is accepted,
    // this line is the record of where the character is drawn, including the
    // screens whose container name was not knowable in advance.
    const std::string containers = "live=[" + JoinRoots(live_roots) +
        "] rejected=[" + JoinRoots(rejected_roots) + "]";
    static std::string last_containers;
    if (containers != last_containers) {
        last_containers = containers;
        Log("Endminf containers: " + containers);
    }
    std::sort(outside_prefix.begin(), outside_prefix.end());
    outside_prefix.erase(
        std::unique(outside_prefix.begin(), outside_prefix.end()),
        outside_prefix.end());
    std::string outside_text;
    for (const std::string& entry : outside_prefix) {
        if (!outside_text.empty()) outside_text += " | ";
        outside_text += entry;
    }
    static std::string last_outside;
    if (outside_text != last_outside) {
        last_outside = outside_text;
        Log("Endminf-named draws outside the actor prefix: [" + outside_text +
            "]");
    }
    return summaries;
}

// Whether a recorded renderer is still in the scene.
//
// Records hold raw pointers, and the game destroys model instances under the
// mod: the character showcase's prefab proxy is rebuilt about a second after
// it is created. Writing through a record whose renderer is gone invokes a
// native method on a freed object, and what that lands on is whatever the
// allocator has since put there. That is a missing eye, a missing face, a
// garment drawn on the shadow, or most of the model dropping out, at random
// and reproducible by pressing the key again.
bool IsLiveRenderer(
    void* renderer, const std::vector<RendererSummary>& live) {
    if (!renderer) return false;
    return std::any_of(
        live.begin(), live.end(), [&](const RendererSummary& item) {
            return item.renderer == renderer;
        });
}

bool IsOriginalLiveComponent9(const RendererSummary& item) {
    return item.renderer && item.mesh &&
        item.renderer_name == kTargetRendererName &&
        item.mesh_name == kTargetMeshName &&
        item.c9_signature && item.lod0_path &&
        item.bones_count == 156 && item.bindpose_count == 156 &&
        IsSupportedLiveTargetPath(item.path);
}

struct TargetCounts {
    int total = 0;
    int gameplay = 0;
    int ui = 0;
};

void CountTarget(const RendererSummary& item, TargetCounts& counts) {
    ++counts.total;
    if (IsGameplayPostModelPath(item.path)) ++counts.gameplay;
    if (IsUiModelPath(item.path)) ++counts.ui;
}

std::vector<RendererSummary*> CollectOriginalLiveComponent9(
    std::vector<RendererSummary>& summaries) {
    std::vector<RendererSummary*> targets;
    for (auto& item : summaries) {
        if (IsOriginalLiveComponent9(item)) targets.push_back(&item);
    }
    return targets;
}

void LogComponent9Candidates(const std::vector<RendererSummary>& summaries) {
    int ordinal = 0;
    for (const auto& item : summaries) {
        if (item.renderer_name == kTargetRendererName ||
            item.mesh_name == kTargetMeshName || item.c9_signature) {
            LogRenderer(item, ordinal++);
        }
    }
}

bool IsNativeObjectAlive(void* object) {
    uint8_t alive = 0;
    void* parameters[1]{object};
    return object && InvokeValue(Contract("object.is_alive"), nullptr,
        parameters, alive) && alive != 0;
}

// A GC handle preserves the managed wrapper, not Unity's native asset during
// UnloadUnusedAssets. Protect assets before removing their last scene reference.
// https://docs.unity3d.com/2022.3/Documentation/ScriptReference/HideFlags.DontUnloadUnusedAsset.html
bool RetainAsset(void* object) {
    if (!IsNativeObjectAlive(object)) return false;
    for (const auto& retained : g_retained_assets) {
        if (retained.object == object) return true;
    }
    if (!g_host->gchandle_new) return false;
    int32_t flags = 0;
    if (!InvokeValue(Contract("object.get_hide_flags"), object, nullptr,
            flags)) return false;
    RetainedAsset retained;
    retained.object = object;
    retained.handle = g_host->gchandle_new(g_host->context, object, 0);
    if (!retained.handle) return false;
    retained.added_unload_protection = (flags & kDontUnloadUnusedAsset) == 0;
    if (retained.added_unload_protection) {
        flags |= kDontUnloadUnusedAsset;
        void* parameters[1]{&flags};
        int32_t read_back = 0;
        if (!InvokeVoid(Contract("object.set_hide_flags"), object, parameters) ||
            !InvokeValue(Contract("object.get_hide_flags"), object, nullptr,
                read_back) || (read_back & kDontUnloadUnusedAsset) == 0) {
            flags &= ~kDontUnloadUnusedAsset;
            InvokeVoid(Contract("object.set_hide_flags"), object, parameters);
            g_host->gchandle_free(g_host->context, retained.handle);
            Log("Asset unload protection failed; replacement refused.");
            return false;
        }
    }
    g_retained_assets.push_back(retained);
    return true;
}

void ReleaseRetainedAssets(bool restore_flags) {
    for (const auto& retained : g_retained_assets) {
        if (restore_flags && retained.added_unload_protection &&
            IsNativeObjectAlive(retained.object)) {
            int32_t flags = 0;
            if (InvokeValue(Contract("object.get_hide_flags"), retained.object,
                    nullptr, flags)) {
                flags &= ~kDontUnloadUnusedAsset;
                void* parameters[1]{&flags};
                InvokeVoid(Contract("object.set_hide_flags"), retained.object,
                    parameters);
            }
        }
        if (retained.handle && g_host && g_host->gchandle_free)
            g_host->gchandle_free(g_host->context, retained.handle);
    }
    g_retained_assets.clear();
}

bool SetSharedMesh(void* renderer, void* mesh) {
    // Do not clear sharedMesh or toggle enabled during rebinding: those extra
    // transitions also invalidate the game's renderer registration and bounds.
    if (!IsNativeObjectAlive(renderer) || !RetainAsset(mesh)) return false;
    void* previous = Invoke(Contract("skinned.get_shared_mesh"), renderer,
        nullptr, false);
    if (previous == mesh) return true;
    if (previous && !RetainAsset(previous)) return false;
    void* parameters[1]{mesh};
    if (InvokeVoid(Contract("skinned.set_shared_mesh"), renderer, parameters) &&
        Invoke(Contract("skinned.get_shared_mesh"), renderer, nullptr, false) == mesh)
        return true;
    parameters[0] = previous;
    const bool restored = InvokeVoid(Contract("skinned.set_shared_mesh"), renderer,
        parameters) && Invoke(Contract("skinned.get_shared_mesh"), renderer,
            nullptr, false) == previous;
    Log("Mesh assignment rejected: renderer=" + ObjectName(renderer) +
        " targetAlive=" + std::to_string(IsNativeObjectAlive(mesh)) +
        " previousRestored=" + std::to_string(restored));
    return false;
}

void DestroyUnityObject(void* object) {
    MethodContract* destroy = Contract("object.destroy");
    if (!object || !destroy || !destroy->resolved) return;
    void* parameters[1]{object};
    InvokeVoid(destroy, nullptr, parameters);
}

void DestroyCustomMesh(void* mesh) {
    DestroyUnityObject(mesh);
}

// Safe to call off the Unity main thread: it only releases GC handles and never
// touches a Unity API.
void ReleaseTextureHandles() {
    if (g_host && g_host->gchandle_free) {
        for (const InstalledTextureSlot& slot : g_installed_slots) {
            if (slot.material_handle) {
                g_host->gchandle_free(g_host->context, slot.material_handle);
            }
            if (slot.original_handle) {
                g_host->gchandle_free(g_host->context, slot.original_handle);
            }
        }
        for (const CreatedTexture& created : g_created_textures) {
            if (created.handle) {
                g_host->gchandle_free(g_host->context, created.handle);
            }
        }
    }
    g_installed_slots.clear();
    g_created_textures.clear();
}

void ReleaseReplacementHandles() {
    ReleaseTextureHandles();
    if (g_host && g_host->gchandle_free) {
        for (const ReplacedComponent& item : g_replaced) {
            if (item.renderer_handle) {
                g_host->gchandle_free(g_host->context, item.renderer_handle);
            }
            if (item.original_mesh_handle) {
                g_host->gchandle_free(
                    g_host->context, item.original_mesh_handle);
            }
            if (item.custom_mesh_handle) {
                g_host->gchandle_free(g_host->context, item.custom_mesh_handle);
            }
        }
    }
    for (ReplacedComponent& item : g_replaced) {
        item.renderer_handle = 0;
        item.original_mesh_handle = 0;
        item.custom_mesh_handle = 0;
    }
}

void ClearReplacementState() {
    ReleaseRetainedAssets(true);
    ReleaseReplacementHandles();
    g_replaced.clear();
    g_active_bem = {};
    g_replacement_active = false;
    g_refresh_suspended = false;
    g_next_refresh_tick = 0;
}

// UnityEngine.Rendering.VertexAttribute values are stable C++ ShaderChannel
// values. Keep the numbers here rather than depending on Unity headers in the
// standalone native module.
constexpr int32_t kVertexPosition = 0;
constexpr int32_t kVertexNormal = 1;
constexpr int32_t kVertexColor = 3;
constexpr int32_t kVertexTexCoord0 = 4;
constexpr int32_t kVertexBlendWeight = 12;
constexpr int32_t kVertexBlendIndices = 13;

// The packed character bytes EFMI reports as source semantic TEXCOORD4 land on
// UnityEngine.Rendering.VertexAttribute.TexCoord2 in the live client. The EFMI
// D3D semantic index and Unity's VertexAttribute index are separate numbering
// domains and must not be assumed equal: enumerating the live C9 declaration
// shows this channel as TexCoord2(6) SNorm8 x4 on stream 1, immediately after
// TexCoord0 Float32 x2 on the same stream. That matches the EFMI VB1 12-byte
// stride the converter reads, so only the destination channel differs.
constexpr int32_t kVertexPackedCharData = 6;

// UnityEngine.Rendering.VertexAttributeFormat values.
constexpr int32_t kFormatFloat32 = 0;
constexpr int32_t kFormatSNorm8 = 3;
constexpr int32_t kFormatUNorm16 = 4;
constexpr int32_t kFormatUInt8 = 6;

struct VertexChannelInfo {
    bool present = false;
    int32_t format = -1;
    int32_t dimension = -1;
    int32_t stream = -1;
};

// Unboxed payload of UnityEngine.Rendering.VertexAttributeDescriptor. The
// managed struct stores four Int32 backing fields in declaration order, so the
// boxed value the runtime hands back is exactly these 16 bytes.
struct VertexAttributeDescriptorRaw {
    int32_t attribute;
    int32_t format;
    int32_t dimension;
    int32_t stream;
};
static_assert(sizeof(VertexAttributeDescriptorRaw) == 16);

// A Mesh cannot declare more channels than the VertexAttribute enum has
// members. The bound only keeps a nonsense count from driving the read loop.
constexpr int32_t kMaxVertexAttributes = 14;

struct VertexDeclaration {
    std::array<VertexAttributeDescriptorRaw, kMaxVertexAttributes> entries{};
    int32_t count = 0;

    bool Find(int32_t attribute, VertexChannelInfo& info) const {
        info = {};
        for (int32_t i = 0; i < count; ++i) {
            if (entries[i].attribute != attribute) continue;
            info.present = true;
            info.format = entries[i].format;
            info.dimension = entries[i].dimension;
            info.stream = entries[i].stream;
            return true;
        }
        return false;
    }
};

const char* VertexFormatName(int32_t format) {
    switch (format) {
    case kFormatFloat32: return "Float32";
    case 1: return "Float16";
    case 2: return "UNorm8";
    case kFormatSNorm8: return "SNorm8";
    case kFormatUNorm16: return "UNorm16";
    case 5: return "SNorm16";
    case kFormatUInt8: return "UInt8";
    case 7: return "SInt8";
    case 8: return "UInt16";
    case 9: return "SInt16";
    case 10: return "UInt32";
    case 11: return "SInt32";
    default: return "other";
    }
}

const char* VertexAttributeName(int32_t attribute) {
    switch (attribute) {
    case kVertexPosition: return "Position";
    case kVertexNormal: return "Normal";
    case 2: return "Tangent";
    case kVertexColor: return "Color";
    case kVertexTexCoord0: return "TexCoord0";
    case 5: return "TexCoord1";
    case kVertexPackedCharData: return "TexCoord2";
    case 7: return "TexCoord3";
    case 8: return "TexCoord4";
    case 9: return "TexCoord5";
    case 10: return "TexCoord6";
    case 11: return "TexCoord7";
    case kVertexBlendWeight: return "BlendWeight";
    case kVertexBlendIndices: return "BlendIndices";
    default: return "other";
    }
}

bool RawChannelContractsAvailable() {
    constexpr std::array<std::string_view, 3> keys{
        "mesh.get_vertex_attribute_count",
        "mesh.get_vertex_attribute",
        "mesh.set_native_array_for_channel"};
    for (std::string_view key : keys) {
        const MethodContract* method = Contract(key);
        if (!method || !method->resolved) {
            Log("PoC-2.3 raw-channel contract unavailable: " + std::string(key));
            return false;
        }
    }
    return true;
}

// Reads the whole declaration in one pass. The validated client strips the
// per-attribute getters, so enumeration is the only metadata-only route left.
// It is also better evidence: an unexpected client dumps every channel it
// actually declares instead of only the ones this PoC probes for.
bool ReadVertexDeclaration(void* mesh, VertexDeclaration& declaration) {
    declaration = {};
    if (!mesh || !RawChannelContractsAvailable()) return false;

    int32_t count = 0;
    if (!InvokeValue(Contract("mesh.get_vertex_attribute_count"),
            mesh, nullptr, count)) {
        Log("Failed to read vertexAttributeCount from the source Mesh.");
        return false;
    }
    if (count <= 0 || count > kMaxVertexAttributes) {
        Log("Implausible vertexAttributeCount=" + std::to_string(count));
        return false;
    }

    for (int32_t i = 0; i < count; ++i) {
        int32_t index = i;
        void* p_index[1]{&index};
        VertexAttributeDescriptorRaw entry{};
        if (!InvokeValue(Contract("mesh.get_vertex_attribute"),
                mesh, p_index, entry)) {
            Log("Failed to read vertex attribute " + std::to_string(i));
            return false;
        }
        declaration.entries[static_cast<size_t>(i)] = entry;
    }
    declaration.count = count;
    return true;
}

void LogVertexChannel(
    const char* name, const VertexChannelInfo& info) {
    if (!info.present) {
        Log(std::string("  ") + name + ": absent");
        return;
    }
    Log(std::string("  ") + name + ": format=" +
        VertexFormatName(info.format) + "(" + std::to_string(info.format) +
        ") dim=" + std::to_string(info.dimension) +
        " stream=" + std::to_string(info.stream));
}

void LogVertexDeclaration(const VertexDeclaration& declaration) {
    for (int32_t i = 0; i < declaration.count; ++i) {
        const VertexAttributeDescriptorRaw& entry =
            declaration.entries[static_cast<size_t>(i)];
        Log("  [" + std::to_string(i) + "] " +
            VertexAttributeName(entry.attribute) + "(" +
            std::to_string(entry.attribute) + ") format=" +
            VertexFormatName(entry.format) + "(" +
            std::to_string(entry.format) + ") dim=" +
            std::to_string(entry.dimension) + " stream=" +
            std::to_string(entry.stream));
    }
}

// The one description of the Endfield character vertex layout, used both to
// gate the source mesh and to verify the mesh we build before it is ever
// handed to the renderer. Strides are included now that the client can be
// asked for them directly.
using ResolveIcallFn = void* (*)(const char*);

// Kept free of unwindable objects so __try is legal here.
bool SafeResolveIcall(
    ResolveIcallFn resolve, const char* name, void** output) {
    *output = nullptr;
    __try {
        *output = resolve(name);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

// Each raw engine call gets its own __try wrapper. These stay free of
// unwindable objects so __try is legal, and a wrong ABI shows up as a caught
// access violation in the log rather than as a dead client.
bool SafeQueryByAttribute(
    MeshQueryByAttributeFn fn, void* mesh, int32_t argument, int32_t* output) {
    *output = -1;
    __try {
        *output = fn(mesh, argument);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool SafeQuery(MeshQueryFn fn, void* mesh, int32_t* output) {
    *output = -1;
    __try {
        *output = fn(mesh);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool SafeSetNativeChannel(
    MeshSetNativeChannelFn fn, void* mesh, int32_t channel, int32_t format,
    int32_t dimension, void* values, int32_t count) {
    __try {
        fn(mesh, channel, format, dimension, values, count, 0, count, 0);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool SafeSetVertexBufferParams(
    void* mesh, int32_t vertex_count, const void* attributes,
    int32_t attribute_count) {
    __try {
        g_engine.set_vertex_buffer_params(
            mesh, vertex_count, attributes, attribute_count);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool SafeSetVertexBufferData(
    void* mesh, int32_t stream, const void* data, int32_t byte_count) {
    __try {
        // count/elementSize are expressed in bytes: the payload is opaque here
        // and the declaration already fixes the real element layout.
        g_engine.set_vertex_buffer_data(
            mesh, stream, data, 0, 0, byte_count, 1, kMeshUpdateDefault);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool SafeSetIndexBufferParams(
    void* mesh, int32_t index_count, int32_t index_format) {
    __try {
        g_engine.set_index_buffer_params(mesh, index_count, index_format);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool SafeSetIndexBufferData(
    void* mesh, const void* data, int32_t byte_count) {
    __try {
        g_engine.set_index_buffer_data(
            mesh, data, 0, 0, byte_count, 1, kMeshUpdateDefault);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool SafeSetSubMesh(
    void* mesh, int32_t index, const SubMeshDescriptorRaw* descriptor) {
    __try {
        g_engine.set_sub_mesh(mesh, index, descriptor, kMeshUpdateDefault);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool SafeUploadMeshData(void* mesh) {
    __try {
        // Keep the CPU copy for declaration checks and subsequent discovery.
        g_engine.upload_mesh_data(mesh, false);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool ResolveEngineBindings() {
    if (g_engine.resolved) return true;

    using ResolveIcallFn = void* (*)(const char*);
    HMODULE game = GetModuleHandleW(L"GameAssembly.dll");
    auto resolve = reinterpret_cast<ResolveIcallFn>(
        game ? reinterpret_cast<void*>(
                   GetProcAddress(game, "il2cpp_resolve_icall"))
             : nullptr);
    if (!resolve) {
        Log("il2cpp_resolve_icall unavailable; engine bindings cannot be bound.");
        return false;
    }

    struct Binding {
        const char* name;
        void** slot;
    };
    const std::array<Binding, 12> bindings{{
        {"UnityEngine.Mesh::GetVertexAttributeDimension",
            reinterpret_cast<void**>(&g_engine.get_attribute_dimension)},
        {"UnityEngine.Mesh::GetVertexAttributeFormat",
            reinterpret_cast<void**>(&g_engine.get_attribute_format)},
        {"UnityEngine.Mesh::GetVertexAttributeStream",
            reinterpret_cast<void**>(&g_engine.get_attribute_stream)},
        {"UnityEngine.Mesh::GetVertexBufferStride",
            reinterpret_cast<void**>(&g_engine.get_vertex_buffer_stride)},
        {"UnityEngine.Mesh::get_vertexBufferCount",
            reinterpret_cast<void**>(&g_engine.get_vertex_buffer_count)},
        {"UnityEngine.Mesh::SetNativeArrayForChannelImpl",
            reinterpret_cast<void**>(&g_engine.set_native_array_for_channel)},
        {"UnityEngine.Mesh::SetVertexBufferParamsFromPtr",
            reinterpret_cast<void**>(&g_engine.set_vertex_buffer_params)},
        {"UnityEngine.Mesh::InternalSetVertexBufferData",
            reinterpret_cast<void**>(&g_engine.set_vertex_buffer_data)},
        {"UnityEngine.Mesh::SetIndexBufferParams",
            reinterpret_cast<void**>(&g_engine.set_index_buffer_params)},
        {"UnityEngine.Mesh::InternalSetIndexBufferData",
            reinterpret_cast<void**>(&g_engine.set_index_buffer_data)},
        {"UnityEngine.Mesh::SetSubMesh_Injected",
            reinterpret_cast<void**>(&g_engine.set_sub_mesh)},
        {"UnityEngine.Mesh::UploadMeshDataImpl",
            reinterpret_cast<void**>(&g_engine.upload_mesh_data)},
    }};

    bool complete = true;
    for (const Binding& binding : bindings) {
        void* pointer = nullptr;
        if (!SafeResolveIcall(resolve, binding.name, &pointer) || !pointer) {
            Log(std::string("Engine binding unresolved: ") + binding.name);
            complete = false;
            continue;
        }
        *binding.slot = pointer;
    }
    g_engine.resolved = complete;
    return complete;
}

// The Endfield character layout is not one declaration. Across the validated
// sample's components the live meshes use six of them: some carry no
// BlendWeight, some no packed TexCoord2, and the stream strides run 16/12/12,
// 16/12/4, 16/8/12 and 16/8/4. So there is no constant to compare against.
//
// Instead the source mesh's own declaration is the specification. It is read
// off the live mesh, reproduced verbatim on the mesh we build, and the built
// result is compared back against it before anything is assigned. The payload
// only has to agree about the stream strides, which is what makes its bytes
// land on the channels the shader reads.
bool DeclarationsEqual(
    const VertexDeclaration& left, const VertexDeclaration& right) {
    if (left.count != right.count) return false;
    for (int32_t i = 0; i < left.count; ++i) {
        const VertexAttributeDescriptorRaw& a =
            left.entries[static_cast<size_t>(i)];
        const VertexAttributeDescriptorRaw& b =
            right.entries[static_cast<size_t>(i)];
        if (a.attribute != b.attribute || a.format != b.format ||
            a.dimension != b.dimension || a.stream != b.stream) {
            return false;
        }
    }
    return true;
}

bool ReadMeshStrides(void* mesh, std::vector<int32_t>& strides) {
    strides.clear();
    if (!ResolveEngineBindings()) return false;
    int32_t buffer_count = -1;
    if (!SafeQuery(g_engine.get_vertex_buffer_count, mesh, &buffer_count) ||
        buffer_count <= 0 || buffer_count > 8) {
        return false;
    }
    for (int32_t stream = 0; stream < buffer_count; ++stream) {
        int32_t stride = -1;
        if (!SafeQueryByAttribute(
                g_engine.get_vertex_buffer_stride, mesh, stream, &stride) ||
            stride <= 0) {
            return false;
        }
        strides.push_back(stride);
    }
    return true;
}

std::string StrideText(const std::vector<int32_t>& strides) {
    std::string text;
    for (int32_t stride : strides) {
        if (!text.empty()) text += "/";
        text += std::to_string(stride);
    }
    return text;
}

// Confirms a mesh reproduces the expected declaration and stream strides.
bool MatchesDeclaration(
    void* mesh, const VertexDeclaration& expected,
    const std::vector<int32_t>& expected_strides, const std::string& label) {
    VertexDeclaration actual;
    if (!ReadVertexDeclaration(mesh, actual)) {
        Log("  " + label + ": declaration unreadable");
        return false;
    }
    if (!DeclarationsEqual(actual, expected)) {
        Log("  " + label + ": declaration differs from the source");
        Log("    expected:");
        LogVertexDeclaration(expected);
        Log("    actual:");
        LogVertexDeclaration(actual);
        return false;
    }

    std::vector<int32_t> actual_strides;
    if (!ReadMeshStrides(mesh, actual_strides)) {
        Log("  " + label + ": strides unreadable");
        return false;
    }
    if (actual_strides != expected_strides) {
        Log("  " + label + ": strides " + StrideText(actual_strides) +
            " differ from the source's " + StrideText(expected_strides));
        return false;
    }
    return true;
}

bool SetRawMeshChannel(
    void* mesh, int32_t attribute, int32_t format, int32_t dimension,
    const void* values, int32_t count, const char* label) {
    MethodContract* method = Contract("mesh.set_native_array_for_channel");
    if (!mesh || !values || count <= 0 || !method || !method->resolved) {
        Log(std::string("Cannot write raw Mesh channel: ") + label);
        return false;
    }

    void* data = const_cast<void*>(values);
    int32_t array_size = count;
    int32_t values_start = 0;
    int32_t values_count = count;
    int32_t flags = 0; // MeshUpdateFlags.Default
    void* parameters[8]{
        &attribute, &format, &dimension, &data,
        &array_size, &values_start, &values_count, &flags};
    if (!InvokeVoid(method, mesh, parameters)) {
        Log(std::string("Mesh CPU channel write failed: ") + label);
        return false;
    }
    return true;
}


std::string FormatFloat(float value) {
    std::array<char, 32> buffer{};
    std::snprintf(buffer.data(), buffer.size(), "%.4f", value);
    return std::string(buffer.data());
}

std::string FormatFloat3(const Float3& value) {
    return FormatFloat(value.x) + "," + FormatFloat(value.y) + "," +
        FormatFloat(value.z);
}

// Mesh.bounds survives isReadable=false, so this is the only comparison we can
// make between the original geometry's extent and the BEM replacement's.
void LogMeshBounds(const char* label, void* mesh) {
    MethodContract* method = Contract("mesh.get_bounds");
    if (!mesh || !method || !method->resolved) {
        Log(std::string("  ") + label + " bounds: unavailable");
        return;
    }
    BoundsRaw bounds{};
    if (!InvokeValue(method, mesh, nullptr, bounds)) {
        Log(std::string("  ") + label + " bounds: read failed");
        return;
    }
    const Float3 size{
        bounds.extents.x * 2.0f,
        bounds.extents.y * 2.0f,
        bounds.extents.z * 2.0f};
    Log(std::string("  ") + label + " bounds: center=(" +
        FormatFloat3(bounds.center) + ") size=(" + FormatFloat3(size) + ")");
}

// The bindpose palette is the one value this PoC takes straight off a mesh
// whose CPU data was discarded, installs on the replacement, and never checks
// beyond its length. A zeroed or non-finite palette collapses every skinned
// vertex to a single point, which is indistinguishable from the geometry
// simply not rendering.
void LogBindposeDiagnostics(void* bindposes, int count) {
    if (!bindposes || count <= 0) {
        Log("  bindposes: absent or empty");
        return;
    }
    const Matrix4x4Raw* data =
        ManagedArrayData<Matrix4x4Raw>(bindposes, static_cast<size_t>(count));
    if (!data) {
        Log("  bindposes: array layout validation failed; contents unreadable");
        return;
    }

    int all_zero = 0;
    int identity = 0;
    int non_finite = 0;
    for (int i = 0; i < count; ++i) {
        bool zero = true;
        bool unit = true;
        bool finite = true;
        for (int k = 0; k < 16; ++k) {
            const float value = data[i].m[k];
            if (value != 0.0f) zero = false;
            if (!std::isfinite(value)) finite = false;
            if (value != ((k % 5 == 0) ? 1.0f : 0.0f)) unit = false;
        }
        if (zero) ++all_zero;
        if (unit) ++identity;
        if (!finite) ++non_finite;
    }
    Log("  bindposes: count=" + std::to_string(count) +
        " allZero=" + std::to_string(all_zero) +
        " identity=" + std::to_string(identity) +
        " nonFinite=" + std::to_string(non_finite));

    const std::array<int, 3> samples{0, count / 2, count - 1};
    for (int index : samples) {
        if (index < 0 || index >= count) continue;
        const float* m = data[static_cast<size_t>(index)].m;
        std::string text;
        for (int column = 0; column < 4; ++column) {
            text += " [";
            for (int row = 0; row < 4; ++row) {
                if (row) text += ",";
                text += FormatFloat(m[column * 4 + row]);
            }
            text += "]";
        }
        Log("    bindpose[" + std::to_string(index) + "] columns:" + text);
    }
}

// Mesh skin metadata is initialized through Unity's dedicated weight API.
// BoneWeight1's unboxed layout is Single weight, Int32 boneIndex.
struct BoneWeight1Raw {
    float weight;
    int32_t bone_index;
};
static_assert(sizeof(BoneWeight1Raw) == 8);

bool DecodeComponentSkin(const BemComponent& component,
    std::vector<uint8_t>& counts, std::vector<BoneWeight1Raw>& weights) {
    const auto& info = component.info;
    if ((info.stride2 != 4 && info.stride2 != 12) ||
        component.streams[2].size() != static_cast<size_t>(info.vertex_count) * info.stride2)
        return false;
    counts.reserve(info.vertex_count);
    weights.reserve(static_cast<size_t>(info.vertex_count) * 4);
    for (uint32_t v = 0; v < info.vertex_count; ++v) {
        const uint8_t* data = component.streams[2].data() + static_cast<size_t>(v) * info.stride2;
        const uint8_t* indices = data + info.stride2 - 4;
        std::array<BoneWeight1Raw, 4> influences{};
        uint8_t count = 0;
        float sum = 0;
        for (int k = 0; k < (info.stride2 == 4 ? 1 : 4); ++k) {
            uint16_t packed = 65535;
            if (info.stride2 == 12) std::memcpy(&packed, data + k * 2, sizeof(packed));
            if (!packed) continue;
            const float weight = static_cast<float>(packed) / 65535.0f;
            influences[count++] = {weight, indices[k]};
            sum += weight;
        }
        if (!count || std::abs(sum - 1.0f) > 0.01f) {
            Log("C" + std::to_string(info.component_id) + " invalid skin weights at vertex " +
                std::to_string(v) + " sum=" + std::to_string(sum));
            return false;
        }
        std::sort(influences.begin(), influences.begin() + count,
            [](const auto& a, const auto& b) { return a.weight > b.weight; });
        for (int k = 0; k < count; ++k) {
            influences[k].weight /= sum;
            weights.push_back(influences[k]);
        }
        counts.push_back(count);
    }
    return true;
}

bool InitializeMeshSkin(void* mesh, const BemComponent& component) {
    std::vector<uint8_t> counts;
    std::vector<BoneWeight1Raw> weights;
    if (!DecodeComponentSkin(component, counts, weights)) return false;
    void* count_data = counts.data();
    void* weight_data = weights.data();
    int32_t count_size = static_cast<int32_t>(counts.size());
    int32_t weight_size = static_cast<int32_t>(weights.size());
    void* parameters[4]{&count_data, &count_size, &weight_data, &weight_size};
    if (!InvokeVoid(Contract("mesh.set_bone_weights_native"), mesh, parameters)) return false;
    int32_t stored = 0;
    const bool read = InvokeValue(Contract("mesh.get_skin_weight_count"), mesh, nullptr, stored);
    Log("C" + std::to_string(component.info.component_id) + " skin weights initialized: expected=" +
        std::to_string(weight_size) + " stored=" + std::to_string(stored));
    return read && stored >= count_size && stored <= count_size * 4;
}

bool ValidateRendererSkin(const BemComponent& component, void* renderer, void* source_mesh) {
    std::vector<uint8_t> counts;
    std::vector<BoneWeight1Raw> weights;
    if (!DecodeComponentSkin(component, counts, weights)) return false;
    void* bones = Invoke(Contract("skinned.get_bones"), renderer, nullptr, false);
    void* poses = Invoke(Contract("mesh.get_bindposes"), source_mesh, nullptr, false);
    const int bone_count = ArrayLength(bones), pose_count = ArrayLength(poses);
    const Matrix4x4Raw* matrices = pose_count > 0 ?
        ManagedArrayData<Matrix4x4Raw>(poses, static_cast<size_t>(pose_count)) : nullptr;
    std::array<bool, 256> checked{};
    for (const auto& weight : weights) {
        const int index = weight.bone_index;
        if (checked[index]) continue;
        checked[index] = true;
        bool valid = matrices && index < bone_count && index < pose_count &&
            IsNativeObjectAlive(ArrayValue(bones, index));
        if (valid) {
            const float* values = matrices[index].m;
            float magnitude = 0;
            for (int n = 0; n < 16; ++n) {
                valid = valid && std::isfinite(values[n]);
                magnitude += std::abs(values[n]);
            }
            valid = valid && magnitude > 0;
        }
        if (!valid) {
            Log("Skin palette incomplete: C" + std::to_string(component.info.component_id) +
                " bone=" + std::to_string(index) + " renderer=" + ObjectName(renderer));
            return false;
        }
    }
    return true;
}

// Builds one component's replacement mesh.
//
// The declaration comes from the live source mesh, not from the payload: that
// is the only description guaranteed to match what the shader reads, and it
// removes any need to map EFMI's semantics onto Unity channels. EFMI cannot
// even name two of the sample's channels (it reports VB0's second field as
// UNKNOWN0 for two components), yet the bytes line up exactly because the
// strides do.
//
// The payload's contribution is therefore only its stream strides, which must
// equal the source's, plus the bytes themselves.
bool BuildMeshFromComponent(
    const BemComponent& component, void* source_mesh, void*& new_mesh) {
    new_mesh = nullptr;
    const BemComponentHeaderRaw& info = component.info;
    const std::string label = "C" + std::to_string(info.component_id);

    if (!ResolveEngineBindings()) {
        Log(label + " needs the engine Mesh layout bindings; refusing.");
        return false;
    }

    VertexDeclaration declaration;
    if (!ReadVertexDeclaration(source_mesh, declaration)) {
        Log(label + " source declaration is unreadable; refusing.");
        return false;
    }
    std::vector<int32_t> source_strides;
    if (!ReadMeshStrides(source_mesh, source_strides)) {
        Log(label + " source strides are unreadable; refusing.");
        return false;
    }

    const std::vector<int32_t> payload_strides{
        static_cast<int32_t>(info.stride0),
        static_cast<int32_t>(info.stride1),
        static_cast<int32_t>(info.stride2)};
    if (source_strides != payload_strides) {
        Log(label + " payload strides " + StrideText(payload_strides) +
            " do not match the live mesh's " + StrideText(source_strides) +
            "; refusing so the bytes cannot land on the wrong channels.");
        LogVertexDeclaration(declaration);
        return false;
    }

    void* bindposes = Invoke(
        Contract("mesh.get_bindposes"), source_mesh, nullptr);
    const int bindpose_count = ArrayLength(bindposes);
    if (!bindposes || bindpose_count <= static_cast<int>(info.max_bone)) {
        Log(label + " bindpose palette too small: bindposes=" +
            std::to_string(bindpose_count) + " maxBone=" +
            std::to_string(info.max_bone));
        return false;
    }

    void* mesh = g_host->object_new(g_host->context, g_mesh_class.class_info);
    if (!mesh || !InvokeVoid(Contract("mesh.ctor"), mesh, nullptr)) {
        Log(label + " failed to construct UnityEngine.Mesh.");
        return false;
    }

    if (auto* set_name = Contract("object.set_name");
        set_name && set_name->resolved && g_host->string_new) {
        const std::string name =
            "BetterEndfield.C" + std::to_string(info.component_id) + ".BEM";
        void* managed = g_host->string_new(g_host->context, name.c_str());
        void* parameters[1]{managed};
        InvokeVoid(set_name, mesh, parameters);
    }

    std::array<VertexAttributeDescriptorRaw, kMaxVertexAttributes> attributes{};
    for (int32_t i = 0; i < declaration.count; ++i) {
        attributes[static_cast<size_t>(i)] =
            declaration.entries[static_cast<size_t>(i)];
    }
    const int32_t vertex_count = static_cast<int32_t>(info.vertex_count);
    if (!SafeSetVertexBufferParams(
            mesh, vertex_count, attributes.data(), declaration.count)) {
        Log(label + " SetVertexBufferParams faulted.");
        DestroyUnityObject(mesh);
        return false;
    }

    uint8_t raw_has_skin = 0;
    InvokeValue(Contract("mesh.has_bone_weights"), mesh, nullptr, raw_has_skin);
    // The source declaration already exposes the packed BlendWeight and
    // BlendIndices fields in stream 2. Keep that one authoritative storage:
    // InternalSetBoneWeights creates a second Unity skinning representation,
    // then the former second SetVertexBufferParams call replaced its layout
    // again. HG can retain either representation across a renderer refresh,
    // which leaves the draw in bind pose when they disagree. Upload the exact
    // source layout and its raw skin stream once instead.

    for (int32_t stream = 0; stream < static_cast<int32_t>(kBemStreamCount);
            ++stream) {
        const std::vector<uint8_t>& data =
            component.streams[static_cast<size_t>(stream)];
        if (!SafeSetVertexBufferData(
                mesh, stream, data.data(), static_cast<int32_t>(data.size()))) {
            Log(label + " SetVertexBufferData faulted on stream " +
                std::to_string(stream));
            DestroyUnityObject(mesh);
            return false;
        }
    }

    const int32_t index_count = static_cast<int32_t>(info.index_count);
    if (!SafeSetIndexBufferParams(mesh, index_count, kIndexFormatUInt16) ||
        !SafeSetIndexBufferData(
            mesh, component.indices.data(),
            static_cast<int32_t>(component.indices.size()))) {
        Log(label + " index buffer upload faulted.");
        DestroyUnityObject(mesh);
        return false;
    }

    // EFMI's index buffer holds the component's draw ranges back to back over
    // one shared vertex range with base vertex 0, so a single submesh covering
    // the whole list draws exactly what the mod draws.
    int sub_mesh_count = 1;
    void* p_sub_mesh_count[1]{&sub_mesh_count};
    if (!InvokeVoid(
            Contract("mesh.set_sub_mesh_count"), mesh, p_sub_mesh_count)) {
        Log(label + " failed to set the submesh count.");
        DestroyUnityObject(mesh);
        return false;
    }

    SubMeshDescriptorRaw descriptor{};
    descriptor.topology = kTopologyTriangles;
    descriptor.index_start = 0;
    descriptor.index_count = index_count;
    descriptor.base_vertex = 0;
    descriptor.first_vertex = 0;
    descriptor.vertex_count = vertex_count;
    if (!SafeSetSubMesh(mesh, 0, &descriptor)) {
        Log(label + " SetSubMesh faulted.");
        DestroyUnityObject(mesh);
        return false;
    }

    void* p_bindposes[1]{bindposes};
    if (!InvokeVoid(Contract("mesh.set_bindposes"), mesh, p_bindposes)) {
        Log(label + " failed to install the original bindpose palette.");
        DestroyUnityObject(mesh);
        return false;
    }

    // Do not RecalculateNormals/Tangents. Endfield packs normal, encoded
    // tangent and bitangent sign into a single channel the shader unpacks.
    if (!InvokeVoid(Contract("mesh.recalculate_bounds"), mesh, nullptr)) {
        Log(label + " RecalculateBounds failed.");
        DestroyUnityObject(mesh);
        return false;
    }

    int built_vertex_count = -1;
    uint32_t built_index_count = 0;
    int32_t submesh = 0;
    void* index_parameters[1]{&submesh};
    InvokeValue(
        Contract("mesh.get_vertex_count"), mesh, nullptr, built_vertex_count);
    InvokeValue(
        Contract("mesh.get_index_count"), mesh, index_parameters,
        built_index_count);
    if (built_vertex_count != vertex_count ||
        built_index_count != static_cast<uint32_t>(index_count)) {
        Log(label + " built geometry counts disagree with the payload: vtx=" +
            std::to_string(built_vertex_count) + " idx=" +
            std::to_string(built_index_count));
        DestroyUnityObject(mesh);
        return false;
    }

    // Verify before assigning. The writer signatures are inferred from Unity's
    // published bindings, so a wrong one shows up here as a refusal rather
    // than as a corrupted renderer.
    if (!MatchesDeclaration(mesh, declaration, source_strides, label +
            " built")) {
        Log(label + " does not reproduce the source declaration; refusing.");
        DestroyUnityObject(mesh);
        return false;
    }

    // Submit the completed mesh before any renderer can consume it. The raw
    // setters populate CPU mesh storage; otherwise upload is left until a
    // later render while F12 can already retire another generation.
    uint8_t has_skin = 0;
    int32_t source_influences = -1, built_influences = -1;
    InvokeValue(Contract("mesh.get_bones_per_vertex_value"), source_mesh, nullptr, source_influences);
    InvokeValue(Contract("mesh.get_bones_per_vertex_value"), mesh, nullptr, built_influences);
    const bool skin_ready = InvokeValue(Contract("mesh.has_bone_weights"), mesh, nullptr, has_skin) && has_skin;
    Log(label + " packed skin: layoutOnly=" + std::to_string(raw_has_skin) +
        " active=" + std::to_string(has_skin) +
        " sourceInfluences=" + std::to_string(source_influences) +
        " builtInfluences=" + std::to_string(built_influences));
    if (!skin_ready) {
        Log(label + " packed stream did not activate skin metadata; original mesh retained.");
        DestroyUnityObject(mesh);
        return false;
    }
    if (!SafeUploadMeshData(mesh)) {
        Log(label + " UploadMeshData(false) faulted; original mesh retained.");
        DestroyUnityObject(mesh);
        return false;
    }

    Log("  " + label + " built: vtx=" + std::to_string(built_vertex_count) +
        " idx=" + std::to_string(built_index_count) + " strides=" +
        StrideText(source_strides) + " attributes=" +
        std::to_string(declaration.count) + " uploaded=1");
    new_mesh = mesh;
    return true;
}

void LogBufferTarget(const char* label, const char* key, void* instance) {
    MethodContract* method = Contract(key);
    if (!instance || !method || !method->resolved) {
        Log(std::string("  ") + label + " vertexBufferTarget: unavailable");
        return;
    }
    int32_t target = -1;
    if (!InvokeValue(method, instance, nullptr, target)) {
        Log(std::string("  ") + label + " vertexBufferTarget: read failed");
        return;
    }
    // GraphicsBuffer.Target is a flags enum: Vertex=1, Index=2, CopySource=4,
    // CopyDestination=8, Structured=16, Raw=32.
    std::string flags;
    const std::array<std::pair<int32_t, const char*>, 6> names{{
        {1, "Vertex"}, {2, "Index"}, {4, "CopySource"},
        {8, "CopyDestination"}, {16, "Structured"}, {32, "Raw"}}};
    for (const auto& [bit, name] : names) {
        if (target & bit) {
            if (!flags.empty()) flags += "|";
            flags += name;
        }
    }
    Log(std::string("  ") + label + " vertexBufferTarget=" +
        std::to_string(target) + (flags.empty() ? "" : " (" + flags + ")"));
}

// Reports the extent of a readable Mesh's position array directly, rather than
// trusting the cached bounds value.
void LogPositionExtent(const char* label, void* mesh, int vertex_count) {
    MethodContract* method = Contract("mesh.get_vertices");
    if (!mesh || vertex_count <= 0 || !method || !method->resolved) {
        Log(std::string("  ") + label + " positions: unavailable");
        return;
    }
    void* array = Invoke(method, mesh, nullptr);
    const int length = ArrayLength(array);
    if (!array || length != vertex_count) {
        Log(std::string("  ") + label + " positions: length=" +
            std::to_string(length) + " expected=" +
            std::to_string(vertex_count));
        return;
    }
    const Float3* data =
        ManagedArrayData<Float3>(array, static_cast<size_t>(length));
    if (!data) {
        Log(std::string("  ") + label + " positions: layout validation failed");
        return;
    }
    Float3 lo = data[0];
    Float3 hi = data[0];
    for (int i = 1; i < length; ++i) {
        lo.x = std::min(lo.x, data[i].x);
        lo.y = std::min(lo.y, data[i].y);
        lo.z = std::min(lo.z, data[i].z);
        hi.x = std::max(hi.x, data[i].x);
        hi.y = std::max(hi.y, data[i].y);
        hi.z = std::max(hi.z, data[i].z);
    }
    const Float3 extent{hi.x - lo.x, hi.y - lo.y, hi.z - lo.z};
    Log(std::string("  ") + label + " positions: min=(" + FormatFloat3(lo) +
        ") max=(" + FormatFloat3(hi) + ") extent=(" + FormatFloat3(extent) +
        ")");
    Log(std::string("  ") + label + " positions[0]=(" +
        FormatFloat3(data[0]) + ") [" + std::to_string(length / 2) + "]=(" +
        FormatFloat3(data[length / 2]) + ")");
}

// Identity control. BakeMesh copies the live renderer's own skinned geometry
// and topology into a Mesh we create, so every value comes from the game
// itself and the only synthetic thing left is that the Mesh object is ours.
// If even this does not draw, no runtime-created Mesh will.
void ProbeRendererBuffers() {
    std::vector<RendererSummary> summaries =
        EnumerateLiveComponent9Renderers(true);
    std::vector<RendererSummary*> targets =
        CollectOriginalLiveComponent9(summaries);
    if (targets.empty()) {
        Log("No live Component9 renderer available for the buffer probe.");
        return;
    }
    RendererSummary* source = targets.front();
    for (RendererSummary* item : targets) {
        if (IsGameplayPostModelPath(item->path)) {
            source = item;
            break;
        }
    }

    Log("Renderer GraphicsBuffer probe (read-only):");
    LogRenderer(*source, 0);
    LogBufferTarget("renderer", "skinned.get_vertex_buffer_target",
        source->renderer);
    LogBufferTarget("mesh", "mesh.get_vertex_buffer_target", source->mesh);

    const std::array<std::pair<const char*, const char*>, 2> buffers{{
        {"GetVertexBuffer", "skinned.get_vertex_buffer"},
        {"GetPreviousVertexBuffer", "skinned.get_previous_vertex_buffer"}}};
    for (const auto& [label, key] : buffers) {
        MethodContract* method = Contract(key);
        if (!method || !method->resolved) {
            Log(std::string("  ") + label + ": contract unavailable");
            continue;
        }
        Log(std::string("  calling ") + label + " ...");
        void* buffer = Invoke(method, source->renderer, nullptr);
        if (!buffer) {
            Log(std::string("  ") + label + ": returned null");
            continue;
        }
        int32_t count = -1;
        int32_t stride = -1;
        InvokeValue(Contract("graphics_buffer.get_count"), buffer, nullptr, count);
        InvokeValue(
            Contract("graphics_buffer.get_stride"), buffer, nullptr, stride);
        Log(std::string("  ") + label + ": count=" + std::to_string(count) +
            " stride=" + std::to_string(stride) +
            " bytes=" + std::to_string(
                static_cast<int64_t>(count) * static_cast<int64_t>(stride)));
        int32_t target = -1;
        if (InvokeValue(
                Contract("graphics_buffer.get_target"), buffer, nullptr,
                target)) {
            Log(std::string("  ") + label + ": target=" +
                std::to_string(target));
        }
    }
    Log("Renderer GraphicsBuffer probe finished without crashing.");
}

// IL2CPP managed metadata stripping removes the C# signature of an engine
// binding but not the binding itself: UnityPlayer.dll registers every engine
// icall by name into the il2cpp icall table, independently of what the game's
// managed code references. So the Mesh layout API this PoC needs may still be
// reachable as a raw function pointer even though the resolver reports the
// managed methods as missing.
//
// This probe only looks names up. It never calls what it finds.

void ProbeIcallTable() {
    HMODULE game = GetModuleHandleW(L"GameAssembly.dll");
    auto resolve = reinterpret_cast<ResolveIcallFn>(
        game ? reinterpret_cast<void*>(
                   GetProcAddress(game, "il2cpp_resolve_icall"))
             : nullptr);
    if (!resolve) {
        Log("il2cpp_resolve_icall is unavailable; the icall route cannot be "
            "evaluated.");
        return;
    }

    Log("IL2CPP icall table probe (lookup only, nothing is invoked):");

    // Grouped so the log reads as evidence: the negative control must come back
    // null and the positive controls must not, otherwise the rest means nothing.
    struct Entry {
        const char* group;
        const char* name;
    };
    const std::array<Entry, 22> entries{{
        {"negative control", "UnityEngine.Mesh::BetterEndfieldNoSuchIcall"},
        {"positive control", "UnityEngine.Mesh::get_vertexCount"},
        {"positive control", "UnityEngine.Mesh::get_bindposes"},
        {"positive control", "UnityEngine.Mesh::SetNativeArrayForChannelImpl"},

        {"layout", "UnityEngine.Mesh::SetVertexBufferParamsFromPtr"},
        {"layout", "UnityEngine.Mesh::SetVertexBufferParamsFromArray"},
        {"layout", "UnityEngine.Mesh::SetIndexBufferParams"},

        {"upload", "UnityEngine.Mesh::InternalSetVertexBufferData"},
        {"upload", "UnityEngine.Mesh::InternalSetVertexBufferDataFromArray"},
        {"upload", "UnityEngine.Mesh::InternalSetIndexBufferData"},
        {"upload", "UnityEngine.Mesh::InternalSetIndexBufferDataFromArray"},

        {"submesh", "UnityEngine.Mesh::SetSubMesh_Injected"},
        {"submesh", "UnityEngine.Mesh::SetAllSubMeshesAtOnceFromArray"},

        {"commit", "UnityEngine.Mesh::UploadMeshDataImpl"},
        {"commit", "UnityEngine.Mesh::MarkModified"},
        {"commit", "UnityEngine.Mesh::RecalculateBoundsImpl"},

        {"stripped getters", "UnityEngine.Mesh::GetVertexAttributeDimension"},
        {"stripped getters", "UnityEngine.Mesh::GetVertexAttributeFormat"},
        {"stripped getters", "UnityEngine.Mesh::GetVertexAttributeStream"},
        {"stripped getters", "UnityEngine.Mesh::GetVertexAttributeOffset"},
        {"stripped getters", "UnityEngine.Mesh::GetVertexBufferStride"},
        {"stripped getters", "UnityEngine.Mesh::get_vertexBufferCount"},
    }};

    int resolved = 0;
    for (const Entry& entry : entries) {
        void* pointer = nullptr;
        if (!SafeResolveIcall(resolve, entry.name, &pointer)) {
            Log(std::string("  [") + entry.group + "] " + entry.name +
                " -> lookup raised an exception");
            continue;
        }

        std::string owner = "unattributed";
        if (pointer) {
            ++resolved;
            HMODULE owning = nullptr;
            if (GetModuleHandleExW(
                    GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
                        GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
                    reinterpret_cast<LPCWSTR>(pointer), &owning) &&
                owning) {
                std::array<wchar_t, MAX_PATH> path{};
                if (GetModuleFileNameW(owning, path.data(), MAX_PATH)) {
                    const wchar_t* base = path.data();
                    for (const wchar_t* scan = path.data(); *scan; ++scan) {
                        if (*scan == L'\\' || *scan == L'/') base = scan + 1;
                    }
                    owner.clear();
                    for (; *base; ++base) {
                        owner.push_back(
                            *base < 128 ? static_cast<char>(*base) : '?');
                    }
                }
            }
        }

        std::array<char, 32> address{};
        std::snprintf(
            address.data(), address.size(), "0x%016llX",
            static_cast<unsigned long long>(
                reinterpret_cast<uintptr_t>(pointer)));
        Log(std::string("  [") + entry.group + "] " + entry.name + " -> " +
            address.data() + (pointer ? " in " + owner : " NULL"));
    }
    Log("icall probe: " + std::to_string(resolved) + "/" +
        std::to_string(entries.size()) + " names resolved to a pointer.");
}


// Compares the raw getters against the managed enumeration API, whose answers
// for the live C9 mesh are already recorded. Read-only on the game's objects.
bool ValidateGetterAbi(void* mesh) {
    VertexDeclaration reference;
    if (!ReadVertexDeclaration(mesh, reference)) {
        Log("  cannot read the reference declaration; getter ABI unverifiable.");
        return false;
    }

    Log("  raw getters vs managed enumeration (" +
        std::to_string(reference.count) + " attributes):");
    int matches = 0;
    int mismatches = 0;
    for (int32_t i = 0; i < reference.count; ++i) {
        const VertexAttributeDescriptorRaw& expected =
            reference.entries[static_cast<size_t>(i)];
        int32_t dimension = -1;
        int32_t format = -1;
        int32_t stream = -1;
        const bool ok =
            SafeQueryByAttribute(
                g_engine.get_attribute_dimension, mesh, expected.attribute,
                &dimension) &&
            SafeQueryByAttribute(
                g_engine.get_attribute_format, mesh, expected.attribute,
                &format) &&
            SafeQueryByAttribute(
                g_engine.get_attribute_stream, mesh, expected.attribute,
                &stream);
        if (!ok) {
            Log(std::string("    ") + VertexAttributeName(expected.attribute) +
                ": raw call faulted");
            ++mismatches;
            continue;
        }
        const bool agrees = dimension == expected.dimension &&
            format == expected.format && stream == expected.stream;
        if (agrees) {
            ++matches;
        } else {
            ++mismatches;
        }
        Log(std::string("    ") + VertexAttributeName(expected.attribute) +
            ": raw format=" + std::to_string(format) + " dim=" +
            std::to_string(dimension) + " stream=" + std::to_string(stream) +
            " | managed format=" + std::to_string(expected.format) + " dim=" +
            std::to_string(expected.dimension) + " stream=" +
            std::to_string(expected.stream) + (agrees ? "  MATCH" : "  DIFFER"));
    }

    // Strides have never been observed from inside the client. The EFMI buffers
    // imply 16/12/12; confirming that here also confirms the argument order.
    int32_t buffer_count = -1;
    if (SafeQuery(g_engine.get_vertex_buffer_count, mesh, &buffer_count)) {
        Log("  vertexBufferCount=" + std::to_string(buffer_count));
        for (int32_t stream = 0;
             stream < buffer_count && stream < 8; ++stream) {
            int32_t stride = -1;
            if (SafeQueryByAttribute(
                    g_engine.get_vertex_buffer_stride, mesh, stream, &stride)) {
                Log("    stream " + std::to_string(stream) + " stride=" +
                    std::to_string(stride));
            } else {
                Log("    stream " + std::to_string(stream) +
                    " stride: raw call faulted");
            }
        }
    } else {
        Log("  vertexBufferCount: raw call faulted");
    }

    Log("  getter ABI: " + std::to_string(matches) + " match, " +
        std::to_string(mismatches) + " differ");
    return mismatches == 0 && matches > 0;
}

// Writer ABI check. Runs entirely on a scratch Mesh we own; the game's objects
// are never touched. The same channel write is issued through the managed
// contract and through the raw pointer, on two different unused attributes, and
// the resulting declarations are compared.
bool ValidateWriterAbi() {
    constexpr int kScratchVertices = 4;
    constexpr int32_t kManagedTarget = 9;   // TexCoord5
    constexpr int32_t kRawTarget = 10;      // TexCoord6
    // Dimension 3 is distinctive: a garbled argument is very unlikely to land
    // on exactly 3, whereas Unity's default UV dimension is 2.
    constexpr int32_t kDimension = 3;

    void* positions = CreateManagedArray(g_vector3_class, kScratchVertices);
    if (!positions) {
        Log("  writer ABI: scratch array allocation failed");
        return false;
    }
    Float3* position_data =
        ManagedArrayData<Float3>(positions, kScratchVertices);
    if (!position_data) {
        Log("  writer ABI: scratch array layout validation failed");
        return false;
    }
    for (int i = 0; i < kScratchVertices; ++i) {
        position_data[i] = Float3{
            static_cast<float>(i), 0.0f, 0.0f};
    }

    void* mesh = g_host->object_new(g_host->context, g_mesh_class.class_info);
    if (!mesh || !InvokeVoid(Contract("mesh.ctor"), mesh, nullptr)) {
        Log("  writer ABI: scratch Mesh construction failed");
        return false;
    }
    void* p_positions[1]{positions};
    if (!InvokeVoid(Contract("mesh.set_vertices"), mesh, p_positions)) {
        Log("  writer ABI: scratch Mesh vertex assignment failed");
        DestroyCustomMesh(mesh);
        return false;
    }

    std::array<Float3, kScratchVertices> payload{};
    for (int i = 0; i < kScratchVertices; ++i) {
        payload[static_cast<size_t>(i)] = Float3{1.0f, 0.0f, 0.0f};
    }

    const bool managed_ok = SetRawMeshChannel(
        mesh, kManagedTarget, kFormatFloat32, kDimension, payload.data(),
        kScratchVertices, "writer ABI managed path");
    const bool raw_ok = SafeSetNativeChannel(
        g_engine.set_native_array_for_channel, mesh, kRawTarget,
        kFormatFloat32, kDimension, payload.data(), kScratchVertices);
    if (!raw_ok) {
        Log("  writer ABI: the raw call faulted; the assumed signature is wrong");
        DestroyCustomMesh(mesh);
        return false;
    }

    VertexDeclaration declaration;
    if (!ReadVertexDeclaration(mesh, declaration)) {
        Log("  writer ABI: scratch declaration unreadable");
        DestroyCustomMesh(mesh);
        return false;
    }
    LogVertexDeclaration(declaration);

    VertexChannelInfo managed_channel;
    VertexChannelInfo raw_channel;
    declaration.Find(kManagedTarget, managed_channel);
    declaration.Find(kRawTarget, raw_channel);
    LogVertexChannel("TexCoord5 (managed contract)", managed_channel);
    LogVertexChannel("TexCoord6 (raw icall)", raw_channel);

    const bool agrees = managed_ok && managed_channel.present &&
        raw_channel.present &&
        managed_channel.format == raw_channel.format &&
        managed_channel.dimension == raw_channel.dimension;
    Log(std::string("  writer ABI: ") +
        (agrees ? "the raw call reproduced the managed call exactly"
                : "the two paths disagree; do not trust the raw writers"));

    DestroyCustomMesh(mesh);
    return agrees;
}

void ProbeEngineBindingAbi() {
    Log("Engine binding ABI probe:");
    if (!ResolveEngineBindings()) {
        Log("  not all bindings resolved; aborting before any raw call.");
        return;
    }

    std::vector<RendererSummary> summaries =
        EnumerateLiveComponent9Renderers(true);
    std::vector<RendererSummary*> targets =
        CollectOriginalLiveComponent9(summaries);
    if (targets.empty()) {
        Log("  no live Component9 renderer; load Endministrator (F) first.");
        return;
    }
    RendererSummary* source = targets.front();
    for (RendererSummary* item : targets) {
        if (IsGameplayPostModelPath(item->path)) {
            source = item;
            break;
        }
    }

    const bool getters = ValidateGetterAbi(source->mesh);
    const bool writers = ValidateWriterAbi();

    if (getters && writers) {
        Log("ABI probe PASSED. The stripped Mesh layout API can be driven "
            "directly; PoC-2.3 may reproduce the exact source declaration.");
    } else {
        Log("ABI probe FAILED. Do not build on these pointers until the "
            "signatures are corrected.");
    }
}

// Reports every texture slot on the C9 material with the values needed to join
// the EFMI DDS set against it. Property names are unavailable — this client
// strips Shader.GetPropertyName — so each slot is identified by its property ID
// plus the asset name of the texture currently bound there, which in practice
// says more about the slot's role than the property name would.
void ProbeMaterialTextures() {
    std::vector<RendererSummary> summaries =
        EnumerateLiveComponent9Renderers(true);
    std::vector<RendererSummary*> targets =
        CollectOriginalLiveComponent9(summaries);
    if (targets.empty()) {
        Log("No live Component9 renderer; load Endministrator (F) first.");
        return;
    }
    RendererSummary* source = targets.front();
    for (RendererSummary* item : targets) {
        if (IsGameplayPostModelPath(item->path)) {
            source = item;
            break;
        }
    }

    Log("C9 material texture slot probe (read-only):");
    LogRenderer(*source, 0);

    MethodContract* get_materials = Contract("renderer.get_shared_materials");
    MethodContract* get_ids = Contract("material.get_texture_property_ids");
    MethodContract* get_texture = Contract("material.get_texture_by_id");
    if (!get_materials || !get_materials->resolved || !get_ids ||
        !get_ids->resolved || !get_texture || !get_texture->resolved) {
        Log("  material texture contracts unavailable.");
        return;
    }

    void* materials = Invoke(get_materials, source->renderer, nullptr);
    const int material_count = ArrayLength(materials);
    Log("  materials=" + std::to_string(material_count));

    for (int m = 0; m < material_count; ++m) {
        void* material = ArrayValue(materials, m);
        if (!material) continue;
        Log("  material[" + std::to_string(m) + "] name=\"" +
            ObjectName(material) + "\"");

        void* ids = Invoke(get_ids, material, nullptr);
        const int id_count = ArrayLength(ids);
        if (!ids || id_count <= 0) {
            Log("    no texture properties reported");
            continue;
        }
        const int32_t* id_data =
            ManagedArrayData<int32_t>(ids, static_cast<size_t>(id_count));
        if (!id_data) {
            Log("    texture property id array layout validation failed");
            continue;
        }
        Log("    textureProperties=" + std::to_string(id_count));

        for (int i = 0; i < id_count; ++i) {
            const int32_t name_id = id_data[i];
            std::array<char, 24> id_text{};
            std::snprintf(
                id_text.data(), id_text.size(), "0x%08X",
                static_cast<unsigned>(name_id));

            int32_t id_argument = name_id;
            void* parameters[1]{&id_argument};
            void* texture = Invoke(get_texture, material, parameters, false);
            if (!texture) {
                Log(std::string("    slot ") + id_text.data() + " unbound");
                continue;
            }

            int32_t width = -1;
            int32_t height = -1;
            int32_t graphics_format = -1;
            int32_t texture_format = -1;
            InvokeValue(Contract("texture.get_width"), texture, nullptr, width);
            InvokeValue(
                Contract("texture.get_height"), texture, nullptr, height);
            InvokeValue(
                Contract("texture.get_graphics_format"), texture, nullptr,
                graphics_format);
            // Only valid when the bound texture really is a Texture2D; a
            // managed exception here is expected for other texture types and
            // is logged rather than treated as a failure.
            InvokeValue(
                Contract("texture2d.get_format"), texture, nullptr,
                texture_format);

            Log(std::string("    slot ") + id_text.data() + " name=\"" +
                ObjectName(texture) + "\" " + std::to_string(width) + "x" +
                std::to_string(height) + " graphicsFormat=" +
                std::to_string(graphics_format) + " textureFormat=" +
                std::to_string(texture_format));
        }
    }
    Log("Material texture slot probe finished.");
}

const char* TextureFormatName(int32_t format) {
    switch (format) {
    case 10: return "DXT1";
    case 12: return "DXT5";
    case 25: return "BC7";
    case 26: return "BC4";
    case 27: return "BC5";
    default: return "?";
    }
}

struct MaterialTextureSlot {
    int32_t slot_id = 0;
    void* texture = nullptr;
    int32_t width = -1;
    int32_t height = -1;
    int32_t graphics_format = -1;
};

bool TextureContractsAvailable() {
    constexpr std::array<std::string_view, 8> required{
        "renderer.get_shared_materials",
        "material.get_texture_property_ids",
        "material.get_texture_by_id",
        "material.set_texture_by_id",
        "texture2d.ctor",
        "texture2d.load_raw_texture_data",
        "texture2d.apply",
        "graphics_format_utility.get_graphics_format"};
    for (std::string_view key : required) {
        MethodContract* method = Contract(key);
        if (!method || !method->resolved) {
            Log("Texture install needs a missing contract: " +
                std::string(key));
            return false;
        }
    }
    return g_texture2d_class.class_info != nullptr;
}

// Reads every bound texture slot of a material along with the join key the
// payload is matched against.
std::vector<MaterialTextureSlot> ReadMaterialTextureSlots(void* material) {
    std::vector<MaterialTextureSlot> slots;
    void* ids = Invoke(Contract("material.get_texture_property_ids"),
        material, nullptr);
    const int id_count = ArrayLength(ids);
    if (!ids || id_count <= 0) return slots;
    const int32_t* id_data =
        ManagedArrayData<int32_t>(ids, static_cast<size_t>(id_count));
    if (!id_data) return slots;

    MethodContract* get_texture = Contract("material.get_texture_by_id");
    for (int i = 0; i < id_count; ++i) {
        MaterialTextureSlot slot;
        slot.slot_id = id_data[i];
        int32_t id_argument = slot.slot_id;
        void* parameters[1]{&id_argument};
        slot.texture = Invoke(get_texture, material, parameters, false);
        if (!slot.texture) continue;
        InvokeValue(Contract("texture.get_width"), slot.texture, nullptr,
            slot.width);
        InvokeValue(Contract("texture.get_height"), slot.texture, nullptr,
            slot.height);
        InvokeValue(Contract("texture.get_graphics_format"), slot.texture,
            nullptr, slot.graphics_format);
        slots.push_back(slot);
    }
    return slots;
}

// Turns a payload slot key into the GraphicsFormat value the live material
// reports, using the engine's own conversion table.
bool SlotGraphicsFormat(int32_t texture_format, int32_t srgb, int32_t& output) {
    int32_t format_argument = texture_format;
    uint8_t srgb_argument = srgb != 0 ? 1 : 0;
    void* parameters[2]{&format_argument, &srgb_argument};
    return InvokeValue(
        Contract("graphics_format_utility.get_graphics_format"), nullptr,
        parameters, output);
}

void CopySamplerState(void* source, void* destination) {
    const auto copy_enum = [&](const char* getter, const char* setter) {
        int32_t value = 0;
        if (!InvokeValue(Contract(getter), source, nullptr, value)) return;
        void* parameters[1]{&value};
        InvokeVoid(Contract(setter), destination, parameters);
    };
    copy_enum("texture.get_wrap_mode", "texture.set_wrap_mode");
    copy_enum("texture.get_filter_mode", "texture.set_filter_mode");
    copy_enum("texture.get_aniso_level", "texture.set_aniso_level");

    float bias = 0.0f;
    if (InvokeValue(Contract("texture.get_mip_map_bias"), source, nullptr,
            bias)) {
        void* parameters[1]{&bias};
        InvokeVoid(Contract("texture.set_mip_map_bias"), destination,
            parameters);
    }
}

// Rebuilds one replacement surface as a Texture2D. The bytes are uploaded as
// stored, mip chain included; only the dimensions and format the payload
// declares are used, and the result is read back to confirm what the engine
// built.
void* CreateTextureFromBem(const BemTexture& texture) {
    const BemTextureEntryRaw& info = texture.info;
    void* object =
        g_host->object_new(g_host->context, g_texture2d_class.class_info);
    if (!object) {
        Log("  failed to allocate a Texture2D for t=" + texture.name);
        return nullptr;
    }

    int32_t width = static_cast<int32_t>(info.width);
    int32_t height = static_cast<int32_t>(info.height);
    int32_t format = info.create_format;
    int32_t mip_count = static_cast<int32_t>(info.mip_count);
    uint8_t linear = info.create_srgb != 0 ? 0 : 1;
    void* ctor_parameters[5]{&width, &height, &format, &mip_count, &linear};
    if (!InvokeVoid(Contract("texture2d.ctor"), object, ctor_parameters)) {
        Log("  Texture2D construction failed for t=" + texture.name);
        return nullptr;
    }

    void* data_pointer = const_cast<uint8_t*>(texture.data.data());
    int32_t data_size = static_cast<int32_t>(texture.data.size());
    void* upload_parameters[2]{&data_pointer, &data_size};
    if (!InvokeVoid(Contract("texture2d.load_raw_texture_data"), object,
            upload_parameters)) {
        Log("  LoadRawTextureData failed for t=" + texture.name);
        DestroyUnityObject(object);
        return nullptr;
    }

    // Do not regenerate mipmaps: the payload already carries whatever chain the
    // source had. Release the CPU copy, which is several megabytes per surface.
    uint8_t update_mipmaps = 0;
    uint8_t no_longer_readable = 1;
    void* apply_parameters[2]{&update_mipmaps, &no_longer_readable};
    if (!InvokeVoid(Contract("texture2d.apply"), object, apply_parameters)) {
        Log("  Apply failed for t=" + texture.name);
        DestroyUnityObject(object);
        return nullptr;
    }

    int32_t built_width = -1;
    int32_t built_height = -1;
    int32_t built_graphics_format = -1;
    InvokeValue(Contract("texture.get_width"), object, nullptr, built_width);
    InvokeValue(Contract("texture.get_height"), object, nullptr, built_height);
    InvokeValue(Contract("texture.get_graphics_format"), object, nullptr,
        built_graphics_format);
    if (built_width != width || built_height != height) {
        Log("  built texture t=" + texture.name + " is " +
            std::to_string(built_width) + "x" + std::to_string(built_height) +
            ", expected " + std::to_string(width) + "x" +
            std::to_string(height) + "; refusing it.");
        DestroyUnityObject(object);
        return nullptr;
    }

    if (auto* set_name = Contract("object.set_name");
        set_name && set_name->resolved && g_host->string_new) {
        const std::string name = "BetterEndfield.C9.T." + texture.name;
        void* managed = g_host->string_new(g_host->context, name.c_str());
        void* parameters[1]{managed};
        InvokeVoid(set_name, object, parameters);
    }

    Log("  built t=" + texture.name + " " + std::to_string(built_width) + "x" +
        std::to_string(built_height) + " mips=" +
        std::to_string(info.mip_count) + " " +
        TextureFormatName(info.create_format) +
        (info.create_srgb != 0 ? " sRGB" : " linear") + " graphicsFormat=" +
        std::to_string(built_graphics_format));
    return object;
}

// A live texture slot, together with the set of components whose materials
// bind the same texture object.
struct LiveTextureSlot {
    void* material = nullptr;
    int32_t slot_id = 0;
    void* texture = nullptr;
    std::string texture_name;
    int32_t width = -1;
    int32_t height = -1;
    int32_t graphics_format = -1;
    uint32_t component_mask = 0;
};

// A shared asset: replacing it would change every other character that uses
// it, so those slots are reported and left alone whatever the join says.
bool IsSharedAssetName(const std::string& name) {
    return name.find("_common_") != std::string::npos;
}

// `reserved` value that marks a pin as the CRC-32 of the original texture's
// object name rather than a material property id. The name is what the slot is
// really identified by, and unlike a property id it survives a shader revision
// that renumbers its properties.
constexpr uint32_t kTexturePinName = 2;

// CRC-32 (the IEEE polynomial), matching zlib.crc32 that the converter uses.
uint32_t Crc32(std::string_view text) {
    uint32_t crc = 0xFFFFFFFFu;
    for (char character : text) {
        crc ^= static_cast<unsigned char>(character);
        for (int bit = 0; bit < 8; ++bit) {
            crc = (crc >> 1) ^ (0xEDB88320u & (0u - (crc & 1u)));
        }
    }
    return ~crc;
}

// Builds the model-wide slot table.
//
// The component set is computed by texture object identity, not by name, so two
// materials binding the same asset produce one entry with both components set.
// That set is exactly what EFMI recorded per texture, which is what makes the
// join possible at all.
std::vector<LiveTextureSlot> BuildLiveTextureSlots(
    const std::vector<std::pair<uint32_t, void*>>& component_renderers) {
    std::vector<LiveTextureSlot> slots;
    for (const auto& entry : component_renderers) {
        const uint32_t bit = entry.first < 32u ? 1u << entry.first : 0u;
        void* materials = Invoke(
            Contract("renderer.get_shared_materials"), entry.second, nullptr);
        const int material_count = ArrayLength(materials);
        for (int m = 0; m < material_count; ++m) {
            void* material = ArrayValue(materials, m);
            if (!material) continue;
            for (const MaterialTextureSlot& slot :
                    ReadMaterialTextureSlots(material)) {
                auto found = std::find_if(
                    slots.begin(), slots.end(),
                    [&](const LiveTextureSlot& item) {
                        return item.material == material &&
                            item.slot_id == slot.slot_id;
                    });
                if (found != slots.end()) {
                    found->component_mask |= bit;
                    continue;
                }
                LiveTextureSlot record;
                record.material = material;
                record.slot_id = slot.slot_id;
                record.texture = slot.texture;
                record.texture_name = ObjectName(slot.texture);
                record.width = slot.width;
                record.height = slot.height;
                record.graphics_format = slot.graphics_format;
                record.component_mask = bit;
                slots.push_back(record);
            }
        }
    }

    // Union the sets across every slot that holds the same texture object: a
    // texture bound by both the face and the eyebrow material has component
    // set {1,4} however many slots reference it.
    for (LiveTextureSlot& slot : slots) {
        uint32_t mask = slot.component_mask;
        for (const LiveTextureSlot& other : slots) {
            if (other.texture == slot.texture) mask |= other.component_mask;
        }
        slot.component_mask = mask;
    }
    return slots;
}

// Installs the payload's textures across the whole model.
//
// EFMI records no slot, so the slot is derived: a payload texture matches a
// slot when the components that sampled it are exactly the components whose
// materials bind what that slot holds, and the slot's current format is the
// format EFMI recorded for the original. Where that still leaves more than one
// candidate the slot is left alone and the candidates are reported, because a
// guess here silently puts the wrong map on the character.
void InstallCustomTextures(
    const BemPocData& bem,
    const std::vector<std::pair<uint32_t, void*>>& component_renderers) {
    if (bem.textures.empty() || component_renderers.empty()) return;
    Log("Installing PoC-2.5 textures:");
    if (!TextureContractsAvailable()) {
        Log("  texture installation is unavailable in this client; geometry "
            "only, the original materials are kept.");
        return;
    }

    std::vector<int32_t> slot_keys(bem.textures.size(), -1);
    for (size_t i = 0; i < bem.textures.size(); ++i) {
        const BemTextureEntryRaw& info = bem.textures[i].info;
        SlotGraphicsFormat(info.slot_format, info.slot_srgb, slot_keys[i]);
    }

    std::vector<LiveTextureSlot> slots =
        BuildLiveTextureSlots(component_renderers);
    std::vector<void*> created(bem.textures.size(), nullptr);
    std::vector<bool> refused(bem.textures.size(), false);
    std::vector<bool> retained(bem.textures.size(), false);
    MethodContract* set_texture = Contract("material.set_texture_by_id");
    int installed = 0;
    int ambiguous = 0;
    int shared_skipped = 0;

    for (const LiveTextureSlot& slot : slots) {
        std::array<char, 24> id_text{};
        std::snprintf(id_text.data(), id_text.size(), "0x%08X",
            static_cast<unsigned>(slot.slot_id));
        const std::string slot_label = std::string("  slot ") +
            id_text.data() + " \"" + slot.texture_name + "\" " +
            std::to_string(slot.width) + "x" + std::to_string(slot.height) +
            " components=" + ComponentMaskText(slot.component_mask);

        std::vector<size_t> candidates;
        for (size_t i = 0; i < bem.textures.size(); ++i) {
            const BemTextureEntryRaw& info = bem.textures[i].info;
            if (info.explicit_slot != 0) {
                // A pin names the slot outright, so it is honoured even when
                // the object bound to it is a shared asset. The component set
                // must still intersect, so a pin can never land on a material
                // that sampled no draw the texture belongs to.
                if ((slot.component_mask & info.component_mask) == 0) continue;
                const bool hit =
                    info.reserved == kTexturePinName
                        ? Crc32(slot.texture_name) ==
                              static_cast<uint32_t>(info.explicit_slot)
                        : info.explicit_slot == slot.slot_id;
                if (hit) candidates.push_back(i);
                continue;
            }
            if (IsSharedAssetName(slot.texture_name)) continue;
            if (slot_keys[i] < 0 || slot_keys[i] != slot.graphics_format) {
                continue;
            }
            if (info.component_mask != slot.component_mask) continue;
            candidates.push_back(i);
        }

        // Dimensions break a tie the component set cannot: a slot holds one
        // size, so a lone candidate that matches it exactly is the one the mod
        // replaced. This can only narrow the set and never invents a
        // candidate, so the refusal below still covers everything else.
        if (candidates.size() > 1) {
            std::vector<size_t> sized;
            for (size_t i : candidates) {
                const BemTextureEntryRaw& info = bem.textures[i].info;
                if (static_cast<int32_t>(info.width) == slot.width &&
                    static_cast<int32_t>(info.height) == slot.height) {
                    sized.push_back(i);
                }
            }
            if (sized.size() == 1) candidates = sized;
        }

        if (candidates.empty()) {
            if (IsSharedAssetName(slot.texture_name)) ++shared_skipped;
            else {
                Log(slot_label + " -> no payload texture claims it; unchanged.");
            }
            continue;
        }
        if (candidates.size() > 1) {
            ++ambiguous;
            std::string names;
            for (size_t i : candidates) {
                if (!names.empty()) names += ", ";
                names += bem.textures[i].name;
            }
            Log(slot_label + " -> " + std::to_string(candidates.size()) +
                " payload textures claim it (" + names +
                "); unchanged rather than guessed. Pin it in bem-slots.json.");
            continue;
        }

        const size_t index = candidates.front();
        const BemTexture& texture = bem.textures[index];
        if (!created[index] && !refused[index]) {
            created[index] = CreateTextureFromBem(texture);
            refused[index] = created[index] == nullptr;
        }
        if (!created[index]) {
            Log(slot_label + " -> t=" + texture.name +
                " could not be rebuilt; unchanged.");
            continue;
        }

        CopySamplerState(slot.texture, created[index]);

        if (!RetainAsset(slot.texture)) {
            Log(slot_label + " -> original texture cannot be retained; unchanged.");
            continue;
        }
        int32_t slot_id = slot.slot_id;
        void* parameters[2]{&slot_id, created[index]};
        if (!InvokeVoid(set_texture, slot.material, parameters)) {
            Log(slot_label + " -> assignment raised a managed exception.");
            continue;
        }
        int32_t read_back_id = slot.slot_id;
        void* read_back_parameters[1]{&read_back_id};
        if (Invoke(Contract("material.get_texture_by_id"), slot.material,
                read_back_parameters, false) != created[index]) {
            Log(slot_label + " -> did not take the new texture; restoring.");
            void* restore[2]{&slot_id, slot.texture};
            InvokeVoid(set_texture, slot.material, restore);
            continue;
        }

        InstalledTextureSlot record;
        record.material = slot.material;
        record.slot_id = slot.slot_id;
        record.original = slot.texture;
        record.replacement = created[index];
        if (g_host->gchandle_new) {
            record.material_handle =
                g_host->gchandle_new(g_host->context, slot.material, 0);
            record.original_handle =
                g_host->gchandle_new(g_host->context, slot.texture, 0);
        }
        g_installed_slots.push_back(record);
        ++installed;

        if (!retained[index]) {
            retained[index] = true;
            CreatedTexture keep;
            keep.texture = created[index];
            if (g_host->gchandle_new) {
                keep.handle =
                    g_host->gchandle_new(g_host->context, created[index], 0);
            }
            g_created_textures.push_back(keep);
        }
        Log(slot_label + " <- t=" + texture.name);
    }

    for (size_t i = 0; i < created.size(); ++i) {
        if (created[i] && !retained[i]) DestroyUnityObject(created[i]);
    }

    Log("PoC-2.5 texture install: slots=" + std::to_string(installed) +
        " ambiguous=" + std::to_string(ambiguous) + " sharedSkipped=" +
        std::to_string(shared_skipped) + " candidateSlots=" +
        std::to_string(slots.size()));
}

bool RestoreOriginalTextures() {
    if (g_installed_slots.empty() && g_created_textures.empty()) return true;

    // Material instances created after installation inherit our textures. Find
    // every user before destroying those textures, just as we do for Mesh clones.
    void* parameters[1]{g_material_class.type_object};
    void* materials = Invoke(Contract("resources.find_objects_of_type_all"),
        nullptr, parameters);
    int32_t dimension = 0;
    void* length_parameters[1]{&dimension};
    int32_t material_count = -1;
    if (!materials || !InvokeValue(Contract("array.get_length"), materials,
            length_parameters, material_count) || material_count < 0) return false;

    std::vector<InstalledTextureSlot> restores;
    int inherited = 0;
    for (int32_t i = 0; i < material_count; ++i) {
        void* material = ArrayValue(materials, i);
        if (!material) return false;
        void* ids = Invoke(Contract("material.get_texture_property_ids"), material, nullptr);
        int32_t count = -1;
        if (!ids || !InvokeValue(Contract("array.get_length"), ids,
                length_parameters, count) || count < 0) return false;
        if (count == 0) continue;
        const int32_t* data = ManagedArrayData<int32_t>(ids, static_cast<size_t>(count));
        if (!data) return false;
        for (int32_t j = 0; j < count; ++j) {
            int32_t id = data[j];
            void* texture_parameters[1]{&id};
            void* exception = nullptr;
            void* current = g_host->runtime_invoke(g_host->context,
                Contract("material.get_texture_by_id")->method_info, material,
                texture_parameters, &exception);
            if (exception) return false;
            const bool custom = std::any_of(g_created_textures.begin(),
                g_created_textures.end(), [&](const auto& item) { return item.texture == current; });
            if (!custom) continue;

            const InstalledTextureSlot* source = nullptr;
            for (const auto& slot : g_installed_slots) {
                if (slot.material == material && slot.slot_id == id &&
                    slot.replacement == current) { source = &slot; break; }
            }
            const bool clone = source == nullptr;
            if (!source) {
                for (const auto& slot : g_installed_slots) {
                    if (slot.replacement != current || slot.slot_id != id) continue;
                    if (source && source->original != slot.original) {
                        Log("Texture rollback ambiguous: material=" + ObjectName(material) +
                            " slot=" + std::to_string(id) + "; assets retained.");
                        return false;
                    }
                    source = &slot;
                }
            }
            if (!source || !IsNativeObjectAlive(source->original)) {
                Log("Texture rollback has no live original: material=" + ObjectName(material) +
                    " slot=" + std::to_string(id) + "; assets retained.");
                return false;
            }
            InstalledTextureSlot restore = *source;
            restore.material = material;
            restores.push_back(restore);
            if (clone) ++inherited;
        }
    }

    int restored = 0;
    for (const auto& slot : restores) {
        int32_t id = slot.slot_id;
        void* set_parameters[2]{&id, slot.original};
        void* get_parameters[1]{&id};
        if (!InvokeVoid(Contract("material.set_texture_by_id"), slot.material, set_parameters) ||
            Invoke(Contract("material.get_texture_by_id"), slot.material,
                get_parameters, false) != slot.original) {
            Log("Texture restore rejected: material=" + ObjectName(slot.material) +
                " slot=" + std::to_string(id) + "; assets retained.");
            return false;
        }
        ++restored;
    }

    std::vector<void*> doomed;
    for (const auto& created : g_created_textures) doomed.push_back(created.texture);
    ReleaseTextureHandles();
    for (void* texture : doomed) DestroyUnityObject(texture);
    Log("Texture rollback: restored=" + std::to_string(restored) +
        " inheritedSlots=" + std::to_string(inherited) +
        " destroyed=" + std::to_string(doomed.size()));
    return true;
}

void LogSubMeshRanges(void* mesh) {
    int sub_mesh_count = 0;
    if (!InvokeValue(Contract("mesh.get_sub_mesh_count"), mesh, nullptr,
            sub_mesh_count)) {
        return;
    }
    const int count = std::min(std::max(sub_mesh_count, 0), kMaxSubMeshes);
    std::string text;
    for (int i = 0; i < count; ++i) {
        int32_t submesh = i;
        uint32_t index_count = 0;
        void* parameters[1]{&submesh};
        InvokeValue(Contract("mesh.get_index_count"), mesh, parameters,
            index_count);
        if (!text.empty()) text += ",";
        text += std::to_string(index_count);
    }
    Log("    subMeshes=" + std::to_string(count) + " indexCounts=[" + text +
        "]");
}

void LogMeshDeclaration(void* mesh) {
    VertexDeclaration declaration;
    if (!ReadVertexDeclaration(mesh, declaration)) {
        Log("    declaration unreadable");
        return;
    }
    Log("    attributes=" + std::to_string(declaration.count));
    LogVertexDeclaration(declaration);

    if (!ResolveEngineBindings()) {
        Log("    strides unavailable: engine bindings unresolved");
        return;
    }
    int32_t buffer_count = -1;
    if (!SafeQuery(g_engine.get_vertex_buffer_count, mesh, &buffer_count) ||
        buffer_count < 0 || buffer_count > 8) {
        Log("    vertexBufferCount=" + std::to_string(buffer_count));
        return;
    }
    std::string strides;
    for (int32_t stream = 0; stream < buffer_count; ++stream) {
        int32_t stride = -1;
        SafeQueryByAttribute(
            g_engine.get_vertex_buffer_stride, mesh, stream, &stride);
        if (!strides.empty()) strides += "/";
        strides += std::to_string(stride);
    }
    Log("    vertexBuffers=" + std::to_string(buffer_count) + " strides=" +
        strides);
}

void LogRendererMaterials(void* renderer) {
    MethodContract* get_materials = Contract("renderer.get_shared_materials");
    if (!get_materials || !get_materials->resolved) return;
    void* materials = Invoke(get_materials, renderer, nullptr);
    const int material_count = ArrayLength(materials);
    for (int m = 0; m < material_count; ++m) {
        void* material = ArrayValue(materials, m);
        if (!material) continue;
        Log("    material[" + std::to_string(m) + "] \"" +
            ObjectName(material) + "\"");
        for (const MaterialTextureSlot& slot :
                ReadMaterialTextureSlots(material)) {
            std::array<char, 24> id_text{};
            std::snprintf(id_text.data(), id_text.size(), "0x%08X",
                static_cast<unsigned>(slot.slot_id));
            Log(std::string("      slot ") + id_text.data() + " \"" +
                ObjectName(slot.texture) + "\" " +
                std::to_string(slot.width) + "x" +
                std::to_string(slot.height) + " graphicsFormat=" +
                std::to_string(slot.graphics_format));
        }
    }
}

// Read-only visibility and shadow diagnostics. Assignment readback alone
// cannot explain a missing draw or geometry appearing on a shadow proxy.
void LogRendererDrawState(void* renderer) {
    uint8_t enabled = 0, forced_off = 0, force_matrix = 0;
    int32_t shadow_mode = -1;
    const bool enabled_read = InvokeValue(Contract("renderer.get_enabled"),
        renderer, nullptr, enabled);
    const bool forced_read = InvokeValue(Contract("renderer.get_force_rendering_off"),
        renderer, nullptr, forced_off);
    InvokeValue(Contract("renderer.get_shadow_casting_mode"), renderer,
        nullptr, shadow_mode);
    const bool matrix_read = InvokeValue(Contract("skinned.get_force_matrix"),
        renderer, nullptr, force_matrix);
    Log("    drawState enabled=" + (enabled_read ? std::to_string(enabled) : "?") +
        " forceOff=" + (forced_read ? std::to_string(forced_off) : "?") +
        " forceMatrix=" + (matrix_read ? std::to_string(force_matrix) : "?") +
        " shadowMode=" + std::to_string(shadow_mode));
    BoundsRaw bounds{};
    if (InvokeValue(Contract("renderer.get_local_bounds"), renderer, nullptr, bounds)) {
        Log("    localBounds center=(" + FormatFloat3(bounds.center) +
            ") extents=(" + FormatFloat3(bounds.extents) + ")");
    }
    void* proxy = Invoke(Contract("renderer.get_shadow_proxy_mesh"), renderer,
        nullptr, false);
    if (proxy) Log("    shadowProxyMesh=\"" + ObjectName(proxy) + "\"");
}

void ProbeShadowTextureUsers() {
    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* renderers = Invoke(Contract("resources.find_objects_of_type_all"),
        nullptr, parameters);
    const int renderer_count = ArrayLength(renderers);
    if (!renderers) {
        Log("Shadow asset audit: renderer snapshot unavailable.");
        return;
    }
    int proxies = 0, custom_meshes = 0, custom_slots = 0;
    for (int i = 0; i < renderer_count; ++i) {
        void* renderer = ArrayValue(renderers, i);
        const std::string name = ObjectName(renderer);
        if (name.find("endminf") == std::string::npos ||
            name.find("shadowProxy") == std::string::npos) continue;
        ++proxies;
        void* mesh = Invoke(Contract("skinned.get_shared_mesh"), renderer, nullptr, false);
        const bool custom = std::any_of(g_replaced.begin(), g_replaced.end(),
            [&](const auto& record) { return record.custom_mesh && record.custom_mesh == mesh; });
        if (custom) ++custom_meshes;
        int slots = 0;
        void* materials = Invoke(Contract("renderer.get_shared_materials"), renderer, nullptr);
        for (int m = 0; m < ArrayLength(materials); ++m) {
            void* material = ArrayValue(materials, m);
            if (!material) continue;
            for (const auto& slot : ReadMaterialTextureSlots(material)) {
                if (std::any_of(g_created_textures.begin(), g_created_textures.end(),
                        [&](const auto& texture) { return texture.texture == slot.texture; })) ++slots;
            }
        }
        custom_slots += slots;
        if (custom || slots > 0) {
            Log("Shadow proxy uses replacement assets: " + BuildTransformPath(renderer) +
                " customMesh=" + std::to_string(custom) + " textureSlots=" + std::to_string(slots));
            LogRendererDrawState(renderer);
        }
    }
    Log("Shadow asset audit: proxies=" + std::to_string(proxies) +
        " customMeshes=" + std::to_string(custom_meshes) +
        " customTextureSlots=" + std::to_string(custom_slots));
}

// Reports the whole Endministrator model, one renderer at a time, with
// everything needed to extend the replacement past Component9: the original
// index count that joins onto EFMI's per-component match_index_count, the exact
// vertex declaration and stream strides, and the material's texture slots.
//
// This exists because the EFMI sample's ten components use six different vertex
// declarations, two of which carry a VB0 channel EFMI itself cannot name. The
// destination Unity channel for those cannot be derived from the mod; it has to
// be read off the live mesh. Read-only.
void ProbeFullModel() {
    Log("Endminf full-model declaration probe (read-only):");
    ProbeShadowTextureUsers();
    std::vector<RendererSummary> summaries = EnumerateLiveEndminfRenderers();
    if (summaries.empty()) {
        Log("  no live Endministrator lod0 renderer; load the character first.");
        return;
    }

    int detailed = 0;
    for (const RendererSummary& item : summaries) {
        const bool gameplay = IsGameplayPostModelPath(item.path);
        if (!gameplay) continue;
        ++detailed;

        int vertex_count = -1;
        InvokeValue(Contract("mesh.get_vertex_count"), item.mesh, nullptr,
            vertex_count);
        Log("  renderer \"" + item.renderer_name + "\" mesh=\"" +
            item.mesh_name + "\" totalIndices=" +
            std::to_string(item.total_indices) + " vertexCount=" +
            std::to_string(vertex_count) + " bones=" +
            std::to_string(item.bones_count) + " bindposes=" +
            std::to_string(item.bindpose_count));
        Log("    path=\"" + item.path + "\"");
        LogSubMeshRanges(item.mesh);
        LogRendererDrawState(item.renderer);
        LogMeshDeclaration(item.mesh);
        LogRendererMaterials(item.renderer);
    }

    // The UI clone is expected to mirror the gameplay model, so it is only
    // summarized. A divergence here would matter, so the counts are still shown.
    for (const RendererSummary& item : summaries) {
        if (IsGameplayPostModelPath(item.path)) continue;
        Log("  [ui] \"" + item.renderer_name + "\" totalIndices=" +
            std::to_string(item.total_indices) + " bones=" +
            std::to_string(item.bones_count));
        LogRendererDrawState(item.renderer);
    }

    Log("Full-model probe finished: detailed=" + std::to_string(detailed) +
        " total=" + std::to_string(summaries.size()));
}

struct SkinProbeExtent {
    Float3 lo{}, hi{};
    int finite = 0, non_finite = 0;
    void Add(Float3 p) {
        if (!std::isfinite(p.x) || !std::isfinite(p.y) || !std::isfinite(p.z)) {
            ++non_finite;
            return;
        }
        if (!finite++) lo = hi = p;
        else {
            lo = {std::min(lo.x, p.x), std::min(lo.y, p.y), std::min(lo.z, p.z)};
            hi = {std::max(hi.x, p.x), std::max(hi.y, p.y), std::max(hi.z, p.z)};
        }
    }
    void Report(const std::string& label) const {
        Log(label + " finite=" + std::to_string(finite) +
            " nonFinite=" + std::to_string(non_finite) + (finite ?
            " min=(" + FormatFloat3(lo) + ") max=(" + FormatFloat3(hi) +
            ") center=(" + FormatFloat3({lo.x * .5f + hi.x * .5f,
                lo.y * .5f + hi.y * .5f, lo.z * .5f + hi.z * .5f}) +
            ") size=(" + FormatFloat3({hi.x-lo.x, hi.y-lo.y, hi.z-lo.z}) + ")" : ""));
    }
};

Float3 SkinProbeTransformPoint(const Matrix4x4Raw& matrix, Float3 p) {
    const float* m = matrix.m;
    return {m[0]*p.x + m[4]*p.y + m[8]*p.z + m[12],
        m[1]*p.x + m[5]*p.y + m[9]*p.z + m[13],
        m[2]*p.x + m[6]*p.y + m[10]*p.z + m[14]};
}

bool SkinProbeFiniteMatrix(const Matrix4x4Raw& matrix) {
    for (float value : matrix.m) if (!std::isfinite(value)) return false;
    return true;
}

float SkinProbeDeterminant(const Matrix4x4Raw& matrix) {
    const float* m = matrix.m;
    return m[0]*(m[5]*m[10]-m[9]*m[6]) -
        m[4]*(m[1]*m[10]-m[9]*m[2]) + m[8]*(m[1]*m[6]-m[5]*m[2]);
}

void LogSkinProbeMatrix(const char* label, const char* key, void* object) {
    Matrix4x4Raw matrix{};
    if (!object || !InvokeValue(Contract(key), object, nullptr, matrix)) {
        Log(std::string("    ") + label + " matrix unavailable");
        return;
    }
    std::string columns;
    for (int c = 0; c < 4; ++c) {
        const float* m = matrix.m + c * 4;
        std::array<char, 100> buffer{};
        std::snprintf(buffer.data(), buffer.size(), " [%.6g,%.6g,%.6g,%.6g]",
            m[0], m[1], m[2], m[3]);
        columns += buffer.data();
    }
    Log(std::string("    ") + label + " finite=" +
        std::to_string(SkinProbeFiniteMatrix(matrix)) + " det3=" +
        std::to_string(SkinProbeDeterminant(matrix)) + " columns=" + columns);
}

void LogSkinProbeBinding(const RendererSummary& item) {
    std::array<char, 80> identity{};
    std::snprintf(identity.data(), identity.size(), "renderer=%p mesh=%p",
        item.renderer, item.mesh);
    Log(std::string("  binding ") + identity.data() + " name=\"" +
        item.renderer_name + "\" mesh=\"" + item.mesh_name + "\" path=\"" + item.path + "\"");
    LogRendererDrawState(item.renderer);
    LogMeshBounds("source mesh", item.mesh);
    void* poses = Invoke(Contract("mesh.get_bindposes"), item.mesh, nullptr);
    LogBindposeDiagnostics(poses, ArrayLength(poses));
    LogSkinProbeMatrix("renderer.localToWorld", "renderer.get_local_to_world", item.renderer);
    void* transform = Invoke(Contract("component.get_transform"), item.renderer, nullptr);
    LogSkinProbeMatrix("transform.localToWorld", "transform.get_local_to_world", transform);
    for (const char* key : {"skinned.get_root_bone", "skinned.get_skinning_root"}) {
        if (!Contract(key)->resolved) { Log(std::string("    ") + key + " unavailable"); continue; }
        void* root = Invoke(Contract(key), item.renderer, nullptr);
        std::array<char, 40> pointer{};
        std::snprintf(pointer.data(), pointer.size(), "%p", root);
        Log(std::string("    ") + key + " ptr=" + pointer.data() +
            " alive=" + std::to_string(IsNativeObjectAlive(root)) +
            " path=\"" + (IsNativeObjectAlive(root) ? BuildTransformPath(root) : "") + "\"");
        if (IsNativeObjectAlive(root))
            LogSkinProbeMatrix(key, "transform.get_local_to_world", root);
    }
    void* bones = Invoke(Contract("skinned.get_bones"), item.renderer, nullptr);
    const int count = ArrayLength(bones);
    SkinProbeExtent origins;
    int missing = 0, unreadable = 0, singular = 0, invalid = 0;
    std::string bad;
    // Inspect all bones, but only list problematic indices (bounded log size).
    for (int i = 0; i < count && i < 4096; ++i) {
        void* bone = ArrayValue(bones, i);
        Matrix4x4Raw matrix{};
        bool problem = true;
        if (!IsNativeObjectAlive(bone)) ++missing;
        else if (!InvokeValue(Contract("transform.get_local_to_world"), bone, nullptr, matrix)) ++unreadable;
        else if (!SkinProbeFiniteMatrix(matrix)) ++invalid;
        else {
            origins.Add({matrix.m[12], matrix.m[13], matrix.m[14]});
            problem = std::abs(SkinProbeDeterminant(matrix)) < 1e-10f;
            if (problem) ++singular;
        }
        if (problem && bad.size() < 180) bad += std::to_string(i) + ",";
    }
    Log("    bones count=" + std::to_string(count) + " bindposes=" +
        std::to_string(item.bindpose_count) + " missing=" + std::to_string(missing) +
        " unreadable=" + std::to_string(unreadable) + " nonFinite=" +
        std::to_string(invalid) + " singular=" + std::to_string(singular) +
        " badIndices=" + bad + (count > 4096 ? " TRUNCATED" : ""));
    origins.Report("    boneOrigins.world");
}

// Scratch meshes are never installed on a renderer. Pin for the entire managed
// call sequence, then destroy only the scratch asset and release its handle.
struct SkinProbeScratch {
    void* mesh = nullptr;
    uint32_t handle = 0;
    ~SkinProbeScratch() {
        if (mesh) DestroyUnityObject(mesh);
        if (handle) g_host->gchandle_free(g_host->context, handle);
    }
};

void ProbeSkinnedSnapshot(const RendererSummary& item) {
    Log("  CPU snapshot name=\"" + item.renderer_name + "\" path=\"" + item.path + "\"");
    if (!Contract("skinned.bake_mesh_no_scale")->resolved ||
        !Contract("mesh.get_vertices")->resolved || !g_host->gchandle_new || !g_host->gchandle_free) {
        Log("    snapshot unavailable: diagnostic bindings/handles missing");
        return;
    }
    if (!IsNativeObjectAlive(item.renderer) || !IsNativeObjectAlive(item.mesh) ||
        Invoke(Contract("skinned.get_shared_mesh"), item.renderer, nullptr) != item.mesh) {
        Log("    snapshot skipped: renderer/mesh changed during probe");
        return;
    }
    // BakeMesh(useScale=false) returns renderer Transform-local positions.
    // Log both this local extent and the extent transformed into world space.
    void* transform = Invoke(Contract("component.get_transform"), item.renderer, nullptr);
    Matrix4x4Raw world{};
    const bool world_read = transform && InvokeValue(Contract("transform.get_local_to_world"),
        transform, nullptr, world) && SkinProbeFiniteMatrix(world);
    int expected = -1;
    InvokeValue(Contract("mesh.get_vertex_count"), item.mesh, nullptr, expected);
    SkinProbeScratch scratch;
    scratch.mesh = g_host->object_new(g_host->context, g_mesh_class.class_info);
    if (!scratch.mesh) return;
    scratch.handle = g_host->gchandle_new(g_host->context, scratch.mesh, 0);
    if (!scratch.handle || !InvokeVoid(Contract("mesh.ctor"), scratch.mesh, nullptr)) {
        Log("    snapshot scratch construction failed");
        return;
    }
    uint8_t use_scale = 0;
    void* parameters[2]{scratch.mesh, &use_scale};
    Log("    BakeMesh begin useScale=0");
    if (!InvokeVoid(Contract("skinned.bake_mesh_no_scale"), item.renderer, parameters)) return;
    void* vertices = Invoke(Contract("mesh.get_vertices"), scratch.mesh, nullptr);
    const int count = ArrayLength(vertices);
    Log("    BakeMesh end expectedVertices=" + std::to_string(expected) +
        " actualVertices=" + std::to_string(count));
    if (count <= 0 || count > 1000000) { Log("    snapshot positions unavailable/oversized"); return; }
    const Float3* data = ManagedArrayData<Float3>(vertices, static_cast<size_t>(count));
    if (!data) { Log("    snapshot array layout validation failed"); return; }
    SkinProbeExtent local_extent, world_extent;
    for (int i = 0; i < count; ++i) {
        local_extent.Add(data[i]);
        if (world_read) world_extent.Add(SkinProbeTransformPoint(world, data[i]));
    }
    local_extent.Report("    baked.local");
    if (world_read) world_extent.Report("    baked.world");
    else Log("    baked.world unavailable: transform matrix unreadable");
}

void ProbeSkinningState(int32_t sample, int32_t sample_count) {
    int32_t frame = -1;
    InvokeValue(Contract("time.get_frame_count"), nullptr, nullptr, frame);
    auto summaries = EnumerateLiveEndminfRenderers();
    Log("0 skin diagnostic begin sample=" + std::to_string(sample) + "/" +
        std::to_string(sample_count) + " frame=" + std::to_string(frame) +
        " replacementActive=" + std::to_string(g_replacement_active) +
        " activeRenderers=" + std::to_string(summaries.size()));
    // Capture every binding before any BakeMesh call, which can perturb skinning.
    for (const auto& item : summaries) LogSkinProbeBinding(item);
    for (const auto& item : summaries) {
        if (item.bones_count > 0 && item.bindpose_count > 0) ProbeSkinnedSnapshot(item);
    }
    Log("0 skin diagnostic end; CPU snapshots do not verify HG/GPU draw buffers.");
}

bool SetRendererEnabled(void* renderer, bool enabled) {
    MethodContract* setter = Contract("renderer.set_enabled");
    if (!setter || !setter->resolved) return false;
    uint8_t value = enabled ? 1 : 0;
    void* parameters[1]{&value};
    return InvokeVoid(setter, renderer, parameters);
}

bool GetRendererEnabled(void* renderer, bool& enabled) {
    uint8_t value = 0;
    if (!InvokeValue(Contract("renderer.get_enabled"), renderer, nullptr,
            value)) {
        return false;
    }
    enabled = value != 0;
    return true;
}

// Applies the mod's default visibility. In the validated sample the two mask
// draws are off by default, which is the look the mod is for; F4 brings them
// back.
void ApplyMaskVisibility() {
    const std::vector<RendererSummary> live = EnumerateLiveEndminfRenderers();
    int changed = 0;
    for (const ReplacedComponent& item : g_replaced) {
        if (!item.hidden_by_mod) continue;
        if (!IsLiveRenderer(item.renderer, live)) continue;
        if (SetRendererEnabled(item.renderer, g_mask_visible)) ++changed;
    }
    if (changed > 0) {
        Log(std::string("Mod-hidden components are now ") +
            (g_mask_visible ? "visible" : "hidden") + ": renderers=" +
            std::to_string(changed));
    }
}

bool ApplyReplacement(const std::vector<RendererSummary>& summaries) {
    BemPocData bem;
    const std::filesystem::path path = BemPath();
    if (path.empty()) {
        Log("Catalog root unavailable; cannot locate the BEM payload.");
        return false;
    }
    if (!LoadBemPoc(path, bem)) {
        Log("Regenerate the payload with tools/CustomModel/convert_efmi_poc.py.");
        return false;
    }

    if (summaries.empty()) {
        Log("No live Endministrator lod0 renderer; load the character first.");
        return false;
    }

    // Join every live renderer onto a payload component by original index
    // count. Several instances of the model can be live at once and all of
    // them are replaced, so this is a multimap rather than a lookup.
    std::vector<std::pair<uint32_t, void*>> component_renderers;
    std::vector<ReplacedComponent> pending;
    int unmatched_renderers = 0;

    for (const BemComponent& component : bem.components) {
        const BemComponentHeaderRaw& info = component.info;
        const std::string label = "C" + std::to_string(info.component_id);

        std::vector<const RendererSummary*> targets;
        for (const RendererSummary& item : summaries) {
            if (!item.renderer || !item.mesh) continue;
            if (item.total_indices != info.original_index_count) continue;
            targets.push_back(&item);
        }
        if (targets.empty()) {
            Log("  " + label + " origIdx=" +
                std::to_string(info.original_index_count) +
                " matched no live renderer; skipped.");
            continue;
        }
        if ((info.flags & kComponentFlagNoGeometry) != 0) {
            // A component the mod carries without geometry is one the mod
            // replaces by omission: EFMI leaves its draw call commented out,
            // which hides the original mesh. There is no replacement to
            // assign, so the renderers are disabled instead and recorded like
            // any other mod-hidden component, which makes F4 bring them back
            // and an Model rollback restore them exactly as they were.
            for (const RendererSummary* item : targets) {
                ReplacedComponent record;
                record.component_id = info.component_id;
                record.original_index_count = info.original_index_count;
                record.renderer = item->renderer;
                record.original_mesh = item->mesh;
                GetRendererEnabled(item->renderer, record.original_enabled);
                record.hidden_by_mod = true;
                pending.push_back(record);
                component_renderers.emplace_back(info.component_id, item->renderer);
            }
            Log("  " + label + " origIdx=" +
                std::to_string(info.original_index_count) +
                " has no replacement geometry; hiding " +
                std::to_string(targets.size()) + " original renderer(s).");
            continue;
        }

        // Bindposes belong to the source Mesh, not its vertex declaration.
        // Two meshes with identical formats can have different bone palettes.
        // Reuse only for the exact same source asset.
        struct BuiltMesh {
            void* source_mesh = nullptr;
            VertexDeclaration declaration{};
            std::vector<int32_t> strides;
            void* mesh = nullptr;
            int assigned = 0;
        };
        std::vector<BuiltMesh> built;
        int applied = 0;

        for (const RendererSummary* item : targets) {
            if (!ValidateRendererSkin(component, item->renderer, item->mesh)) continue;
            if (item->bones_count <= static_cast<int>(info.max_bone)) {
                Log("  " + label + " bone palette too small on a target: bones=" +
                    std::to_string(item->bones_count) + " maxBone=" +
                    std::to_string(info.max_bone) + "; skipped it.");
                continue;
            }

            VertexDeclaration target_declaration;
            std::vector<int32_t> target_strides;
            if (!ReadVertexDeclaration(item->mesh, target_declaration) ||
                !ReadMeshStrides(item->mesh, target_strides)) {
                Log("  " + label +
                    " target declaration is unreadable; skipped one renderer.");
                continue;
            }

            BuiltMesh* match = nullptr;
            for (BuiltMesh& candidate : built) {
                if (candidate.source_mesh == item->mesh && DeclarationsEqual(
                        candidate.declaration, target_declaration) &&
                    candidate.strides == target_strides) {
                    match = &candidate;
                    break;
                }
            }
            if (!match) {
                void* custom_mesh = nullptr;
                if (!BuildMeshFromComponent(
                        component, item->mesh, custom_mesh)) {
                    continue;
                }
                BuiltMesh fresh;
                fresh.source_mesh = item->mesh;
                fresh.declaration = target_declaration;
                fresh.strides = target_strides;
                fresh.mesh = custom_mesh;
                built.push_back(std::move(fresh));
                match = &built.back();
            }

            if (!SetSharedMesh(item->renderer, match->mesh)) continue;
            if (Invoke(Contract("skinned.get_shared_mesh"), item->renderer,
                    nullptr, false) != match->mesh) {
                Log("  " + label + " sharedMesh read-back mismatch; restored.");
                SetSharedMesh(item->renderer, item->mesh);
                continue;
            }
            ReplacedComponent record;
            record.component_id = info.component_id;
            record.original_index_count = info.original_index_count;
            record.renderer = item->renderer;
            record.original_mesh = item->mesh;
            record.custom_mesh = match->mesh;
            record.hidden_by_mod = (info.flags & kComponentFlagHidden) != 0;
            GetRendererEnabled(item->renderer, record.original_enabled);
            pending.push_back(record);
            component_renderers.emplace_back(info.component_id, item->renderer);
            ++match->assigned;
            ++applied;
        }

        for (const BuiltMesh& entry : built) {
            if (entry.assigned == 0) DestroyUnityObject(entry.mesh);
        }

        if (applied == 0) {
            Log("  " + label + " was built but no renderer accepted it.");
            continue;
        }
        Log("  " + label + " applied to " + std::to_string(applied) +
            " renderer(s)" +
            (built.size() > 1
                ? " over " + std::to_string(built.size()) +
                      " distinct declarations"
                : ""));
    }

    for (const RendererSummary& item : summaries) {
        const bool known = std::any_of(
            bem.components.begin(), bem.components.end(),
            [&](const BemComponent& component) {
                return component.info.original_index_count ==
                    item.total_indices;
            });
        if (!known) ++unmatched_renderers;
    }

    if (pending.empty()) {
        ReleaseRetainedAssets(true);
        Log("No component could be replaced.");
        return false;
    }

    g_replaced = std::move(pending);
    g_replacement_active = true;

    if (g_host->gchandle_new) {
        for (ReplacedComponent& item : g_replaced) {
            item.renderer_handle =
                g_host->gchandle_new(g_host->context, item.renderer, 0);
            item.original_mesh_handle =
                g_host->gchandle_new(g_host->context, item.original_mesh, 0);
            // A component carried without geometry has no replacement mesh to
            // keep alive.
            if (item.custom_mesh) {
                item.custom_mesh_handle =
                    g_host->gchandle_new(g_host->context, item.custom_mesh, 0);
            }
        }
    }

    InstallCustomTextures(bem, component_renderers);
    ProbeShadowTextureUsers();
    ApplyMaskVisibility();
    g_active_bem = std::move(bem);

    int distinct_components = 0;
    uint32_t seen = 0;
    for (const ReplacedComponent& item : g_replaced) {
        if (item.component_id < 32u && (seen >> item.component_id & 1u) == 0) {
            seen |= 1u << item.component_id;
            ++distinct_components;
        }
    }
    Log("F12 PoC-2.5 replacement active: components=" +
        std::to_string(distinct_components) + " renderers=" +
        std::to_string(g_replaced.size()) + " liveRenderersNotInPayload=" +
        std::to_string(unmatched_renderers) +
        ". F12 rebuilds this from scratch, F11 rolls back, F4 toggles the "
        "mod's hidden parts.");
    return true;
}
