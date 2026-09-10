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
    const int count = std::min(std::max(raw_count, 0), kMaxRenderers);

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

bool IsOriginalLiveComponent9(const RendererSummary& item) {
    return item.renderer && item.mesh &&
        item.renderer_name == kTargetRendererName &&
        item.mesh_name == kTargetMeshName &&
        item.c9_signature && item.lod0_path &&
        item.bones_count == 156 && item.bindpose_count == 156 &&
        IsSupportedLiveTargetPath(item.path);
}

bool IsCustomLiveComponent9(const RendererSummary& item) {
    return item.renderer && item.mesh && g_custom_mesh &&
        item.renderer_name == kTargetRendererName &&
        item.mesh == g_custom_mesh && item.lod0_path &&
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

bool SetSharedMesh(void* renderer, void* mesh) {
    void* parameters[1]{mesh};
    return InvokeVoid(
        Contract("skinned.set_shared_mesh"), renderer, parameters);
}

void DestroyCustomMesh(void* mesh) {
    MethodContract* destroy = Contract("object.destroy");
    if (!mesh || !destroy || !destroy->resolved) return;
    void* parameters[1]{mesh};
    InvokeVoid(destroy, nullptr, parameters);
}

void ReleaseReplacementHandles() {
    if (!g_host || !g_host->gchandle_free) return;
    if (g_renderer_handle) {
        g_host->gchandle_free(g_host->context, g_renderer_handle);
    }
    if (g_original_mesh_handle) {
        g_host->gchandle_free(g_host->context, g_original_mesh_handle);
    }
    if (g_custom_mesh_handle) {
        g_host->gchandle_free(g_host->context, g_custom_mesh_handle);
    }
    g_renderer_handle = 0;
    g_original_mesh_handle = 0;
    g_custom_mesh_handle = 0;
}

void ClearReplacementState() {
    ReleaseReplacementHandles();
    g_replaced_renderer = nullptr;
    g_original_mesh = nullptr;
    g_custom_mesh = nullptr;
}

// UnityEngine.Rendering.VertexAttribute values are stable C++ ShaderChannel
// values. Keep the numbers here rather than depending on Unity headers in the
// standalone native module.
constexpr int32_t kVertexPosition = 0;
constexpr int32_t kVertexNormal = 1;
constexpr int32_t kVertexColor = 3;
constexpr int32_t kVertexTexCoord0 = 4;
constexpr int32_t kVertexTexCoord4 = 8;
constexpr int32_t kVertexBlendWeight = 12;
constexpr int32_t kVertexBlendIndices = 13;

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
    int32_t offset = -1;
};

const char* VertexFormatName(int32_t format) {
    switch (format) {
    case kFormatFloat32: return "Float32";
    case kFormatSNorm8: return "SNorm8";
    case kFormatUNorm16: return "UNorm16";
    case kFormatUInt8: return "UInt8";
    default: return "other";
    }
}

bool RawChannelContractsAvailable() {
    constexpr std::array<std::string_view, 6> keys{
        "mesh.has_vertex_attribute",
        "mesh.get_vertex_attribute_dimension",
        "mesh.get_vertex_attribute_format",
        "mesh.get_vertex_attribute_stream",
        "mesh.get_vertex_attribute_offset",
        "mesh.set_native_array_for_channel"};
    for (std::string_view key : keys) {
        const MethodContract* method = Contract(key);
        if (!method || !method->resolved) {
            Log("PoC-2.1 raw-channel contract unavailable: " + std::string(key));
            return false;
        }
    }
    return true;
}

bool QueryVertexChannel(void* mesh, int32_t attribute, VertexChannelInfo& info) {
    info = {};
    if (!mesh || !RawChannelContractsAvailable()) return false;

    void* p_attr[1]{&attribute};
    if (!InvokeValue(Contract("mesh.has_vertex_attribute"),
            mesh, p_attr, info.present)) {
        return false;
    }
    if (!info.present) return true;

    if (!InvokeValue(Contract("mesh.get_vertex_attribute_format"),
            mesh, p_attr, info.format) ||
        !InvokeValue(Contract("mesh.get_vertex_attribute_dimension"),
            mesh, p_attr, info.dimension) ||
        !InvokeValue(Contract("mesh.get_vertex_attribute_stream"),
            mesh, p_attr, info.stream) ||
        !InvokeValue(Contract("mesh.get_vertex_attribute_offset"),
            mesh, p_attr, info.offset)) {
        return false;
    }
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
        " stream=" + std::to_string(info.stream) +
        " offset=" + std::to_string(info.offset));
}

bool ValidateSourceVertexDeclaration(void* mesh) {
    if (!RawChannelContractsAvailable()) {
        Log("PoC-2.1 requires only Mesh CPU channel bindings; no GraphicsBuffer fallback will be attempted.");
        return false;
    }

    struct ExpectedChannel {
        const char* name;
        int32_t attribute;
        int32_t format;
        int32_t dimension;
    };
    constexpr std::array<ExpectedChannel, 6> expected{{
        {"Position", kVertexPosition, kFormatFloat32, 3},
        {"Normal(packed TBN)", kVertexNormal, kFormatFloat32, 1},
        {"TexCoord0", kVertexTexCoord0, kFormatFloat32, 2},
        {"TexCoord4(character packed data)", kVertexTexCoord4, kFormatSNorm8, 4},
        {"BlendWeight", kVertexBlendWeight, kFormatUNorm16, 4},
        {"BlendIndices", kVertexBlendIndices, kFormatUInt8, 4},
    }};

    Log("C9 source Unity vertex declaration:");
    bool matches = true;
    for (const auto& item : expected) {
        VertexChannelInfo info;
        if (!QueryVertexChannel(mesh, item.attribute, info)) {
            Log(std::string("  ") + item.name + ": query failed");
            matches = false;
            continue;
        }
        LogVertexChannel(item.name, info);
        if (!info.present || info.format != item.format ||
            info.dimension != item.dimension) {
            matches = false;
        }
    }

    // EFMI Tools remaps the source character TEXCOORD4/SNorm8 channel to its
    // COLOR0 abstraction for Blender convenience. Log actual Unity Color too
    // so an unexpected client declaration gives us useful evidence rather than
    // a guessed fallback.
    VertexChannelInfo color;
    if (QueryVertexChannel(mesh, kVertexColor, color)) {
        LogVertexChannel("Color(diagnostic)", color);
    }

    if (!matches) {
        Log("C9 source declaration does not match the validated EFMI semantic map; refusing raw-channel replacement.");
        return false;
    }
    Log("C9 source declaration matches EFMI: Normal=Float32x1 packed TBN, TexCoord4=SNorm8x4.");
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

bool ValidateWrittenRawChannels(void* mesh) {
    VertexChannelInfo normal;
    VertexChannelInfo texcoord4;
    if (!QueryVertexChannel(mesh, kVertexNormal, normal) ||
        !QueryVertexChannel(mesh, kVertexTexCoord4, texcoord4)) {
        return false;
    }
    Log("PoC-2.1 custom Mesh raw channels after write:");
    LogVertexChannel("Normal(packed TBN)", normal);
    LogVertexChannel("TexCoord4(character packed data)", texcoord4);
    return normal.present && normal.format == kFormatFloat32 &&
        normal.dimension == 1 && texcoord4.present &&
        texcoord4.format == kFormatSNorm8 && texcoord4.dimension == 4;
}

bool BuildMeshFromBem(
    const BemPocData& bem, void* original_mesh, void*& new_mesh) {
    new_mesh = nullptr;

    if (!ValidateSourceVertexDeclaration(original_mesh)) {
        return false;
    }
    if (!ValidateBoneWeightLayout()) {
        Log("Refusing replacement because BoneWeight layout validation failed.");
        return false;
    }

    void* bindposes = Invoke(
        Contract("mesh.get_bindposes"), original_mesh, nullptr);
    const int bindpose_count = ArrayLength(bindposes);
    if (!bindposes ||
        bindpose_count <= static_cast<int>(bem.header.max_bone)) {
        Log("Original C9 bindpose palette too small: bindposes=" +
            std::to_string(bindpose_count) +
            " maxBone=" + std::to_string(bem.header.max_bone));
        return false;
    }

    void* positions = CreateManagedArray(
        g_vector3_class, static_cast<int>(bem.positions.size()));
    void* uvs = CreateManagedArray(
        g_vector2_class, static_cast<int>(bem.uvs.size()));
    void* bone_weights = CreateManagedArray(
        g_bone_weight_class, static_cast<int>(bem.bone_weights.size()));
    void* triangles = CreateManagedArray(
        g_int32_class, static_cast<int>(bem.indices.size()));

    if (!positions || !uvs || !bone_weights || !triangles) {
        Log("Failed to allocate one or more managed Mesh arrays.");
        return false;
    }

    Float3* position_data =
        ManagedArrayData<Float3>(positions, bem.positions.size());
    Float2* uv_data =
        ManagedArrayData<Float2>(uvs, bem.uvs.size());
    BoneWeightPoc* weight_data =
        ManagedArrayData<BoneWeightPoc>(
            bone_weights, bem.bone_weights.size());
    int32_t* triangle_data =
        ManagedArrayData<int32_t>(triangles, bem.indices.size());

    if (!position_data || !uv_data || !weight_data || !triangle_data) {
        Log("IL2CPP managed array layout validation failed.");
        return false;
    }

    std::memcpy(
        position_data, bem.positions.data(),
        bem.positions.size() * sizeof(Float3));
    std::memcpy(
        uv_data, bem.uvs.data(),
        bem.uvs.size() * sizeof(Float2));
    std::memcpy(
        weight_data, bem.bone_weights.data(),
        bem.bone_weights.size() * sizeof(BoneWeightPoc));
    for (size_t i = 0; i < bem.indices.size(); ++i) {
        triangle_data[i] = bem.indices[i];
    }

    void* mesh = g_host->object_new(
        g_host->context, g_mesh_class.class_info);
    if (!mesh || !InvokeVoid(Contract("mesh.ctor"), mesh, nullptr)) {
        Log("Failed to construct UnityEngine.Mesh.");
        return false;
    }

    if (auto* set_name = Contract("object.set_name");
        set_name && set_name->resolved && g_host->string_new) {
        void* name = g_host->string_new(
            g_host->context, "BetterEndfield.C9.BEM.PoC21");
        void* parameters[1]{name};
        InvokeVoid(set_name, mesh, parameters);
    }

    // Keep Unity's proven high-level paths for position, UV and legacy
    // BoneWeight bookkeeping. Only the two channels whose exact bit-level
    // declaration matters to Endfield's shader use the raw CPU Mesh binding.
    void* p_vertices[1]{positions};
    void* p_uvs[1]{uvs};
    void* p_weights[1]{bone_weights};
    void* p_bindposes[1]{bindposes};

    if (!InvokeVoid(Contract("mesh.set_vertices"), mesh, p_vertices) ||
        !InvokeVoid(Contract("mesh.set_uv"), mesh, p_uvs) ||
        !InvokeVoid(Contract("mesh.set_bone_weights"), mesh, p_weights) ||
        !InvokeVoid(Contract("mesh.set_bindposes"), mesh, p_bindposes)) {
        Log("Failed assigning position/UV/skin/bindpose data.");
        DestroyCustomMesh(mesh);
        return false;
    }

    const int32_t vertex_count = static_cast<int32_t>(bem.header.vertex_count);
    if (!SetRawMeshChannel(
            mesh, kVertexNormal, kFormatFloat32, 1,
            bem.packed_normals.data(), vertex_count, "Normal Float32x1 packed TBN") ||
        !SetRawMeshChannel(
            mesh, kVertexTexCoord4, kFormatSNorm8, 4,
            bem.texcoord4_snorm.data(), vertex_count,
            "TexCoord4 SNorm8x4 character data") ||
        !ValidateWrittenRawChannels(mesh)) {
        Log("PoC-2.1 raw semantic reconstruction failed; original renderer is unchanged.");
        DestroyCustomMesh(mesh);
        return false;
    }

    int sub_mesh_count = 1;
    void* p_sub_mesh_count[1]{&sub_mesh_count};
    if (!InvokeVoid(
            Contract("mesh.set_sub_mesh_count"), mesh, p_sub_mesh_count)) {
        DestroyCustomMesh(mesh);
        return false;
    }

    int submesh = 0;
    bool calculate_bounds = false;
    int base_vertex = 0;
    void* p_triangles[4]{
        triangles, &submesh, &calculate_bounds, &base_vertex};
    if (!InvokeVoid(
            Contract("mesh.set_triangles"), mesh, p_triangles)) {
        Log("SetTriangles failed for combined Component9 index buffer.");
        DestroyCustomMesh(mesh);
        return false;
    }

    // Do not RecalculateNormals/Tangents here. Endfield stores normal, encoded
    // tangent and bitangent sign together in the 32-bit Normal channel and the
    // original shader decodes those bits itself.
    if (!InvokeVoid(
            Contract("mesh.recalculate_bounds"), mesh, nullptr)) {
        DestroyCustomMesh(mesh);
        return false;
    }

    int built_vertex_count = -1;
    uint32_t index_count = 0;
    InvokeValue(
        Contract("mesh.get_vertex_count"), mesh, nullptr, built_vertex_count);
    void* index_parameters[1]{&submesh};
    InvokeValue(
        Contract("mesh.get_index_count"), mesh, index_parameters, index_count);
    Log("Built PoC-2.1 Unity Mesh: vertexCount=" +
        std::to_string(built_vertex_count) +
        " subMeshCount=1 indexCount=" + std::to_string(index_count) +
        " packedNormal=preserved texCoord4SNorm=preserved");

    new_mesh = mesh;
    return true;
}

bool ApplyReplacement() {
    BemPocData bem;
    const std::filesystem::path path = BemPath();
    if (path.empty()) {
        Log("Catalog root unavailable; cannot locate BEM PoC.");
        return false;
    }
    if (!LoadBemPoc(path, bem)) {
        Log("Generate the BEM PoC-2.1 with tools/CustomModel/convert_efmi_poc.py.");
        return false;
    }

    std::vector<RendererSummary> summaries =
        EnumerateLiveComponent9Renderers(true);
    std::vector<RendererSummary*> targets =
        CollectOriginalLiveComponent9(summaries);
    if (targets.empty()) {
        Log("No original live gameplay/UI Component9 LOD0 renderer is currently available.");
        const std::vector<RendererSummary> diagnostics = EnumerateRenderers();
        LogComponent9Candidates(diagnostics);
        return false;
    }

    RendererSummary* source = targets.front();
    for (RendererSummary* item : targets) {
        if (IsGameplayPostModelPath(item->path)) {
            source = item;
            break;
        }
    }

    if (source->bones_count <= static_cast<int>(bem.header.max_bone)) {
        Log("Selected C9 bone palette too small: bones=" +
            std::to_string(source->bones_count) +
            " maxBone=" + std::to_string(bem.header.max_bone));
        LogRenderer(*source, 0);
        return false;
    }

    Log("Using Component9 source renderer:");
    LogRenderer(*source, 0);

    void* custom_mesh = nullptr;
