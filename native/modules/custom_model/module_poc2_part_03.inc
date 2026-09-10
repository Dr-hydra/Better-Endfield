    g_lod1.original_mesh = nullptr;
    g_lod1.custom_mesh = nullptr;
}

bool HasActiveReplacement() {
    return g_lod0.custom_mesh || g_lod1.custom_mesh;
}

bool QueryVertexStreamLayout(void* mesh, int lod, std::vector<int>& strides) {
    strides.clear();
    int count = 0;
    if (!InvokeValue(Contract("mesh.get_vertex_buffer_count"), mesh, nullptr, count) ||
        count <= 0 || count > 4) {
        Log("Unexpected vertexBufferCount for C9 LOD" + std::to_string(lod) +
            ": " + std::to_string(count));
        return false;
    }
    for (int stream = 0; stream < count; ++stream) {
        int stride = 0;
        void* p[1]{&stream};
        if (!InvokeValue(Contract("mesh.get_vertex_buffer_stride"), mesh, p, stride)) {
            return false;
        }
        strides.push_back(stride);
    }

    std::string line = "C9 LOD" + std::to_string(lod) + " original vertex streams=" +
        std::to_string(count) + " strides=";
    for (size_t i = 0; i < strides.size(); ++i) {
        if (i) line += ",";
        line += std::to_string(strides[i]);
    }
    Log(line);

    const int vb1_stride = lod == 1 ? static_cast<int>(kExpectedVb1Lod1Stride)
                                    : static_cast<int>(kExpectedVb1Lod0Stride);
    const int vb2_stride = lod == 1 ? static_cast<int>(kExpectedVb2Lod1Stride)
                                    : static_cast<int>(kExpectedVb2Lod0Stride);
    if (strides.size() < 3 || strides[0] != static_cast<int>(kExpectedVb0Stride) ||
        strides[1] != vb1_stride || strides[2] != vb2_stride) {
        Log("C9 original Unity vertex stream layout does not match EFMI VB0/VB1/VB2 expectation; refusing raw overwrite.");
        return false;
    }
    if (strides.size() == 4 && strides[3] != static_cast<int>(kExpectedVb0Stride)) {
        Log("C9 stream3 exists but is not the EFMI-compatible VB0 alias stride; refusing raw overwrite.");
        return false;
    }
    return true;
}

bool WriteRawVertexStream(void* mesh, int stream, const std::vector<uint8_t>& bytes,
    int stride, uint32_t vertex_count) {
    if (!mesh || bytes.size() != static_cast<size_t>(stride) * vertex_count) return false;
    int data_start = 0;
    int mesh_start = 0;
    int count = static_cast<int>(vertex_count);
    int elem_size = stride;
    int flags = 0;
    void* data_ptr = const_cast<uint8_t*>(bytes.data());
    void* parameters[7]{&stream, &data_ptr, &data_start, &mesh_start, &count, &elem_size, &flags};
    return InvokeVoid(Contract("mesh.internal_set_vertex_buffer_data"), mesh, parameters);
}

bool BuildRawMeshFromBem(const BemPocData& bem, int lod, void* original_mesh, void*& new_mesh) {
    new_mesh = nullptr;
    if (!original_mesh || (lod != 0 && lod != 1)) return false;

    int original_vertices = 0;
    InvokeValue(Contract("mesh.get_vertex_count"), original_mesh, nullptr, original_vertices);
    if (original_vertices < static_cast<int>(bem.header.vertex_count)) {
        Log("Original C9 LOD" + std::to_string(lod) + " vertex capacity too small: original=" +
            std::to_string(original_vertices) + " custom=" + std::to_string(bem.header.vertex_count));
        return false;
    }

    std::vector<int> strides;
    if (!QueryVertexStreamLayout(original_mesh, lod, strides)) return false;

    void* instantiate_parameters[1]{original_mesh};
    void* mesh = Invoke(Contract("object.instantiate"), nullptr, instantiate_parameters);
    if (!mesh) {
        Log("Failed to clone original C9 LOD" + std::to_string(lod) + " Mesh.");
        return false;
    }

    if (auto* set_name = Contract("object.set_name"); set_name && set_name->resolved && g_host->string_new) {
        const std::string name_text = "BetterEndfield.C9.BEM.Raw.LOD" + std::to_string(lod);
        void* name = g_host->string_new(g_host->context, name_text.c_str());
        void* p[1]{name};
        InvokeVoid(set_name, mesh, p);
    }

    const std::vector<uint8_t>& vb1 = lod == 1 ? bem.vb1_lod1 : bem.vb1_lod0;
    const std::vector<uint8_t>& vb2 = lod == 1 ? bem.vb2_lod1 : bem.vb2_lod0;
    const int vb1_stride = lod == 1 ? static_cast<int>(bem.header.vb1_lod1_stride)
                                    : static_cast<int>(bem.header.vb1_lod0_stride);
    const int vb2_stride = lod == 1 ? static_cast<int>(bem.header.vb2_lod1_stride)
                                    : static_cast<int>(bem.header.vb2_lod0_stride);

    if (!WriteRawVertexStream(mesh, 0, bem.vb0, static_cast<int>(bem.header.vb0_stride), bem.header.vertex_count) ||
        !WriteRawVertexStream(mesh, 1, vb1, vb1_stride, bem.header.vertex_count) ||
        !WriteRawVertexStream(mesh, 2, vb2, vb2_stride, bem.header.vertex_count)) {
        Log("Failed writing EFMI raw VB0/VB1/VB2 into cloned C9 LOD" + std::to_string(lod) + " Mesh.");
        DestroyCustomMesh(mesh);
        return false;
    }
    if (strides.size() == 4 &&
        !WriteRawVertexStream(mesh, 3, bem.vb0, static_cast<int>(bem.header.vb0_stride), bem.header.vertex_count)) {
        Log("Failed writing EFMI VB0 alias into C9 stream3.");
        DestroyCustomMesh(mesh);
        return false;
    }

    void* triangles = CreateManagedArray(g_int32_class, static_cast<int>(bem.indices.size()));
    int32_t* triangle_data = ManagedArrayData<int32_t>(triangles, bem.indices.size());
    if (!triangles || !triangle_data) {
        Log("Failed to allocate combined C9 index array.");
        DestroyCustomMesh(mesh);
        return false;
    }
    for (size_t i = 0; i < bem.indices.size(); ++i) triangle_data[i] = bem.indices[i];

    int sub_mesh_count = 1;
    void* p_sub_mesh_count[1]{&sub_mesh_count};
    if (!InvokeVoid(Contract("mesh.set_sub_mesh_count"), mesh, p_sub_mesh_count)) {
        DestroyCustomMesh(mesh);
        return false;
    }
    int submesh = 0;
    bool calculate_bounds = false;
    int base_vertex = 0;
    void* p_triangles[4]{triangles, &submesh, &calculate_bounds, &base_vertex};
    if (!InvokeVoid(Contract("mesh.set_triangles"), mesh, p_triangles)) {
        Log("SetTriangles failed for raw C9 LOD" + std::to_string(lod) + " Mesh.");
        DestroyCustomMesh(mesh);
        return false;
    }

    // Intentionally preserve the cloned original bounds and bindposes. The
    // EFMI replacement stays near the original body, and retaining bounds also
    // rules out RecalculateBounds-induced culling during this validation step.
    uint32_t index_count = 0;
    void* p_index[1]{&submesh};
    InvokeValue(Contract("mesh.get_index_count"), mesh, p_index, index_count);
    Log("Built raw-layout C9 LOD" + std::to_string(lod) + " Mesh clone: capacityVertices=" +
        std::to_string(original_vertices) + " customVertices=" +
        std::to_string(bem.header.vertex_count) + " indexCount=" + std::to_string(index_count));
    new_mesh = mesh;
    return true;
}

RendererSummary* FindSourceForLod(std::vector<RendererSummary>& summaries, int lod) {
    RendererSummary* fallback = nullptr;
    for (auto& item : summaries) {
        if (item.lod != lod || !IsOriginalLiveComponent9(item)) continue;
        if (!fallback) fallback = &item;
        if (IsGameplayPostModelPath(item.path)) return &item;
    }
    return fallback;
}

bool ApplyReplacement() {
    BemPocData bem;
    const std::filesystem::path path = BemPath();
    if (path.empty()) {
        Log("Catalog root unavailable; cannot locate BEM PoC.");
        return false;
    }
    if (!LoadBemPoc(path, bem)) {
        Log("Regenerate it with the current tools/CustomModel/convert_efmi_poc.py (PoC-3).");
        return false;
    }

    std::vector<RendererSummary> summaries = EnumerateLiveComponent9Renderers(true);
    RendererSummary* source0 = FindSourceForLod(summaries, 0);
    RendererSummary* source1 = FindSourceForLod(summaries, 1);
    if (!source0 || !source1) {
        Log("Both live C9 LOD0 and LOD1 source renderers are required for PoC-3.");
        LogComponent9Candidates(EnumerateRenderers());
        return false;
    }

    Log("Using C9 LOD0 source:");
    LogRenderer(*source0, 0);
    Log("Using C9 LOD1 source:");
    LogRenderer(*source1, 1);

    void* custom0 = nullptr;
    void* custom1 = nullptr;
    if (!BuildRawMeshFromBem(bem, 0, source0->mesh, custom0) ||
        !BuildRawMeshFromBem(bem, 1, source1->mesh, custom1)) {
        if (custom0) DestroyCustomMesh(custom0);
        if (custom1) DestroyCustomMesh(custom1);
        return false;
    }

    g_lod0.original_mesh = source0->mesh;
    g_lod0.custom_mesh = custom0;
    g_lod1.original_mesh = source1->mesh;
    g_lod1.custom_mesh = custom1;
    if (g_host->gchandle_new) {
        g_lod0.original_handle = g_host->gchandle_new(g_host->context, g_lod0.original_mesh, 0);
        g_lod0.custom_handle = g_host->gchandle_new(g_host->context, g_lod0.custom_mesh, 0);
        g_lod1.original_handle = g_host->gchandle_new(g_host->context, g_lod1.original_mesh, 0);
        g_lod1.custom_handle = g_host->gchandle_new(g_host->context, g_lod1.custom_mesh, 0);
    }

    TargetCounts applied{};
    for (auto& item : summaries) {
        if (!IsOriginalLiveComponent9(item)) continue;
        MeshVariantState& variant = VariantForLod(item.lod);
        if (!SetSharedMesh(item.renderer, variant.custom_mesh)) continue;
        void* observed = Invoke(Contract("skinned.get_shared_mesh"), item.renderer, nullptr, false);
        if (observed == variant.custom_mesh) CountTarget(item, applied);
    }

    if (applied.total == 0) {
        void* doomed0 = g_lod0.custom_mesh;
        void* doomed1 = g_lod1.custom_mesh;
        ClearReplacementState();
        DestroyCustomMesh(doomed0);
        DestroyCustomMesh(doomed1);
        return false;
    }

    Log("F9 PoC-3 raw replacement active: applied=" + std::to_string(applied.total) +