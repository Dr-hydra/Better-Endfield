    g_lod1.original_mesh = nullptr;
    g_lod1.custom_mesh = nullptr;
}

bool HasActiveReplacement() {
    return g_lod0.custom_mesh || g_lod1.custom_mesh;
}

struct VertexStreamView {
    void* buffer = nullptr;
    int count = 0;
    int stride = 0;
};

bool GetVertexStream(void* mesh, int stream, VertexStreamView& output) {
    output = {};
    if (!mesh || stream < 0) return false;

    int stream_index = stream;
    void* parameters[1]{&stream_index};
    void* buffer = Invoke(
        Contract("mesh.get_vertex_buffer_impl"), mesh, parameters, false);
    if (!buffer) return false;

    int count = 0;
    int stride = 0;
    if (!InvokeValue(
            Contract("graphics_buffer.get_count"), buffer, nullptr, count) ||
        !InvokeValue(
            Contract("graphics_buffer.get_stride"), buffer, nullptr, stride) ||
        count <= 0 || stride <= 0) {
        return false;
    }

    output.buffer = buffer;
    output.count = count;
    output.stride = stride;
    return true;
}

bool QueryVertexStreamLayout(
    void* mesh, int lod, uint32_t required_vertices,
    std::vector<VertexStreamView>& streams) {
    streams.clear();

    for (int stream = 0; stream < 8; ++stream) {
        VertexStreamView view{};
        if (!GetVertexStream(mesh, stream, view)) {
            break;
        }
        streams.push_back(view);
    }

    std::string line =
        "C9 LOD" + std::to_string(lod) +
        " GraphicsBuffer streams=" + std::to_string(streams.size()) +
        " [countxstride]=";
    for (size_t i = 0; i < streams.size(); ++i) {
        if (i) line += ",";
        line += std::to_string(streams[i].count) + "x" +
            std::to_string(streams[i].stride);
    }
    Log(line);

    if (streams.size() < 3 || streams.size() > 4) {
        Log("C9 raw bridge expects 3 or 4 vertex streams; refusing overwrite.");
        return false;
    }

    const int vb1_stride = lod == 1
        ? static_cast<int>(kExpectedVb1Lod1Stride)
        : static_cast<int>(kExpectedVb1Lod0Stride);
    const int vb2_stride = lod == 1
        ? static_cast<int>(kExpectedVb2Lod1Stride)
        : static_cast<int>(kExpectedVb2Lod0Stride);

    const bool stride_match =
        streams[0].stride == static_cast<int>(kExpectedVb0Stride) &&
        streams[1].stride == vb1_stride &&
        streams[2].stride == vb2_stride &&
        (streams.size() != 4 ||
            streams[3].stride == static_cast<int>(kExpectedVb0Stride));
    if (!stride_match) {
        Log("C9 GraphicsBuffer stride layout does not match EFMI VB0/VB1/VB2 expectation; refusing raw overwrite.");
        return false;
    }

    for (size_t i = 0; i < streams.size(); ++i) {
        if (streams[i].count < static_cast<int>(required_vertices)) {
            Log("C9 GraphicsBuffer stream" + std::to_string(i) +
                " capacity too small: count=" +
                std::to_string(streams[i].count) +
                " required=" + std::to_string(required_vertices));
            return false;
        }
    }
    return true;
}

bool WriteRawVertexStream(
    const VertexStreamView& stream,
    const std::vector<uint8_t>& bytes,
    int expected_stride,
    uint32_t vertex_count) {
    if (!stream.buffer || stream.stride != expected_stride ||
        stream.count < static_cast<int>(vertex_count) ||
        bytes.size() != static_cast<size_t>(expected_stride) * vertex_count) {
        return false;
    }

    void* data_ptr = const_cast<uint8_t*>(bytes.data());
    int native_start = 0;
    int graphics_start = 0;
    int count = static_cast<int>(vertex_count);
    int elem_size = expected_stride;
    void* parameters[5]{
        &data_ptr, &native_start, &graphics_start, &count, &elem_size};
    return InvokeVoid(
        Contract("graphics_buffer.internal_set_native_data"),
        stream.buffer, parameters);
}

bool BuildRawMeshFromBem(
    const BemPocData& bem, int lod, void* original_mesh, void*& new_mesh) {
    new_mesh = nullptr;
    if (!original_mesh || (lod != 0 && lod != 1)) return false;

    int original_vertices = 0;
    InvokeValue(
        Contract("mesh.get_vertex_count"), original_mesh, nullptr,
        original_vertices);
    if (original_vertices < static_cast<int>(bem.header.vertex_count)) {
        Log("Original C9 LOD" + std::to_string(lod) +
            " vertex capacity too small: original=" +
            std::to_string(original_vertices) + " custom=" +
            std::to_string(bem.header.vertex_count));
        return false;
    }

    void* instantiate_parameters[1]{original_mesh};
    void* mesh = Invoke(
        Contract("object.instantiate"), nullptr, instantiate_parameters);
    if (!mesh) {
        Log("Failed to clone original C9 LOD" +
            std::to_string(lod) + " Mesh.");
        return false;
    }

    if (auto* set_name = Contract("object.set_name");
        set_name && set_name->resolved && g_host->string_new) {
        const std::string name_text =
            "BetterEndfield.C9.BEM.Raw.GB.LOD" + std::to_string(lod);
        void* name = g_host->string_new(g_host->context, name_text.c_str());
        void* p[1]{name};
        InvokeVoid(set_name, mesh, p);
    }

    // Set the custom index list first. Some Mesh mutators can upload/refresh
    // mesh CPU data, so the raw GraphicsBuffer write is deliberately last.
    void* triangles = CreateManagedArray(
        g_int32_class, static_cast<int>(bem.indices.size()));
    int32_t* triangle_data =
        ManagedArrayData<int32_t>(triangles, bem.indices.size());
    if (!triangles || !triangle_data) {
        Log("Failed to allocate combined C9 index array.");
        DestroyCustomMesh(mesh);
        return false;
    }
    for (size_t i = 0; i < bem.indices.size(); ++i) {
        triangle_data[i] = bem.indices[i];
    }

    int sub_mesh_count = 1;
    void* p_sub_mesh_count[1]{&sub_mesh_count};
    if (!InvokeVoid(
            Contract("mesh.set_sub_mesh_count"), mesh,
            p_sub_mesh_count)) {
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
        Log("SetTriangles failed for raw C9 LOD" +
            std::to_string(lod) + " Mesh.");
        DestroyCustomMesh(mesh);
        return false;
    }

    std::vector<VertexStreamView> streams;
    if (!QueryVertexStreamLayout(
            mesh, lod, bem.header.vertex_count, streams)) {
        DestroyCustomMesh(mesh);
        return false;
    }

    const std::vector<uint8_t>& vb1 =
        lod == 1 ? bem.vb1_lod1 : bem.vb1_lod0;
    const std::vector<uint8_t>& vb2 =
        lod == 1 ? bem.vb2_lod1 : bem.vb2_lod0;
    const int vb1_stride = lod == 1
        ? static_cast<int>(bem.header.vb1_lod1_stride)
        : static_cast<int>(bem.header.vb1_lod0_stride);
    const int vb2_stride = lod == 1
        ? static_cast<int>(bem.header.vb2_lod1_stride)
        : static_cast<int>(bem.header.vb2_lod0_stride);

    if (!WriteRawVertexStream(
            streams[0], bem.vb0,
            static_cast<int>(bem.header.vb0_stride),
            bem.header.vertex_count) ||
        !WriteRawVertexStream(
            streams[1], vb1, vb1_stride,
            bem.header.vertex_count) ||
        !WriteRawVertexStream(
            streams[2], vb2, vb2_stride,
            bem.header.vertex_count)) {
        Log("Failed writing EFMI raw VB0/VB1/VB2 through GraphicsBuffer for C9 LOD" +
            std::to_string(lod) + ".");
        DestroyCustomMesh(mesh);
        return false;
    }

    if (streams.size() == 4 &&
        !WriteRawVertexStream(
            streams[3], bem.vb0,
            static_cast<int>(bem.header.vb0_stride),
            bem.header.vertex_count)) {
        Log("Failed writing EFMI VB0 alias into C9 GraphicsBuffer stream3.");
        DestroyCustomMesh(mesh);
        return false;
    }

    uint32_t index_count = 0;
    void* p_index[1]{&submesh};
    InvokeValue(
        Contract("mesh.get_index_count"), mesh, p_index, index_count);
    Log("Built GraphicsBuffer raw C9 LOD" + std::to_string(lod) +
        " Mesh clone: capacityVertices=" +
        std::to_string(original_vertices) + " customVertices=" +
        std::to_string(bem.header.vertex_count) + " indexCount=" +
        std::to_string(index_count));
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