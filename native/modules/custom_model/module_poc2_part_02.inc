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

bool BuildMeshFromBem(
    const BemPocData& bem, void* original_mesh, void*& new_mesh) {
    new_mesh = nullptr;

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
    void* normals = CreateManagedArray(
        g_vector3_class, static_cast<int>(bem.normals.size()));
    void* colors = CreateManagedArray(
        g_color_class, static_cast<int>(bem.colors.size()));
    void* bone_weights = CreateManagedArray(
        g_bone_weight_class, static_cast<int>(bem.bone_weights.size()));
    void* triangles = CreateManagedArray(
        g_int32_class, static_cast<int>(bem.indices.size()));

    if (!positions || !uvs || !normals || !colors ||
        !bone_weights || !triangles) {
        Log("Failed to allocate one or more managed Mesh arrays.");
        return false;
    }

    Float3* position_data =
        ManagedArrayData<Float3>(positions, bem.positions.size());
    Float2* uv_data =
        ManagedArrayData<Float2>(uvs, bem.uvs.size());
    Float3* normal_data =
        ManagedArrayData<Float3>(normals, bem.normals.size());
    Float4* color_data =
        ManagedArrayData<Float4>(colors, bem.colors.size());
    BoneWeightPoc* weight_data =
        ManagedArrayData<BoneWeightPoc>(
            bone_weights, bem.bone_weights.size());
    int32_t* triangle_data =
        ManagedArrayData<int32_t>(triangles, bem.indices.size());

    if (!position_data || !uv_data || !normal_data || !color_data ||
        !weight_data || !triangle_data) {
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
        normal_data, bem.normals.data(),
        bem.normals.size() * sizeof(Float3));
    std::memcpy(
        color_data, bem.colors.data(),
        bem.colors.size() * sizeof(Float4));
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
            g_host->context, "BetterEndfield.C9.BEM.PoC");
        void* parameters[1]{name};
        InvokeVoid(set_name, mesh, parameters);
    }

    void* p_vertices[1]{positions};
    void* p_uvs[1]{uvs};
    void* p_normals[1]{normals};
    void* p_colors[1]{colors};
    void* p_weights[1]{bone_weights};
    void* p_bindposes[1]{bindposes};

    if (!InvokeVoid(Contract("mesh.set_vertices"), mesh, p_vertices) ||
        !InvokeVoid(Contract("mesh.set_uv"), mesh, p_uvs) ||
        !InvokeVoid(Contract("mesh.set_normals"), mesh, p_normals) ||
        !InvokeVoid(Contract("mesh.set_colors"), mesh, p_colors) ||
        !InvokeVoid(Contract("mesh.set_bone_weights"), mesh, p_weights) ||
        !InvokeVoid(Contract("mesh.set_bindposes"), mesh, p_bindposes)) {
        Log("Failed assigning vertex/UV/normal/color/skin/bindpose data.");
        DestroyCustomMesh(mesh);
        return false;
    }

    // Original C9 is one submesh with one material. For PoC-1 all seven EFMI
    // draw ranges are visible by default, so collapse the complete 59,073-index
    // buffer back into one Unity submesh. Draw ranges stay an importer concern
    // until visibility/toggle groups are implemented.
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

    // Preserve EFMI's decoded packed normals. RecalculateTangents is still
    // acceptable for this PoC because EFMI Tools itself reconstructs tangents
    // from normals/UVs during its Blender round-trip.
    if (auto* tangents = Contract("mesh.recalculate_tangents");
        tangents && tangents->resolved) {
        InvokeVoid(tangents, mesh, nullptr);
    }
    if (!InvokeVoid(
            Contract("mesh.recalculate_bounds"), mesh, nullptr)) {
        DestroyCustomMesh(mesh);
        return false;
    }

    int vertex_count = -1;
    uint32_t index_count = 0;
    InvokeValue(
        Contract("mesh.get_vertex_count"), mesh, nullptr, vertex_count);
    void* index_parameters[1]{&submesh};
    InvokeValue(
        Contract("mesh.get_index_count"), mesh, index_parameters, index_count);
    Log("Built custom Unity Mesh: vertexCount=" +
        std::to_string(vertex_count) +
        " subMeshCount=1 indexCount=" + std::to_string(index_count));

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
        Log("Generate the BEM PoC with tools/CustomModel/convert_efmi_poc.py.");
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

    // Prefer gameplay as the canonical source when both gameplay and the
    // character UI model are alive. Both validated instances use the same
    // 156-entry palette/signature, but this keeps the source deterministic.
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
