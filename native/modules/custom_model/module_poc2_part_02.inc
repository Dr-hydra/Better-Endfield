            Log("BEM PoC-3 index references a vertex outside custom vertex range.");
            return false;
        }
    }

    Log("Loaded BEM PoC-3 raw streams: vertices=" +
        std::to_string(output.header.vertex_count) +
        " indices=" + std::to_string(output.header.index_count) +
        " lod0MaxBone=" + std::to_string(output.header.max_bone_lod0) +
        " lod1MaxBone=" + std::to_string(output.header.max_bone_lod1));
    return true;
}

void* CreateManagedArray(
    const BE_ResolvedClassV1& element_class, int count) {
    if (!element_class.type_object || count < 0) return nullptr;
    void* parameters[2]{element_class.type_object, &count};
    void* array = Invoke(
        Contract("array.create_instance"), nullptr, parameters);
    if (!array || ArrayLength(array) != count) return nullptr;
    return array;
}

template <typename T>
T* ManagedArrayData(void* array, size_t expected_count) {
    if (!array) return nullptr;
    __try {
        const uintptr_t length = *reinterpret_cast<const uintptr_t*>(
            static_cast<const uint8_t*>(array) + kIl2CppArrayLengthOffset);
        if (length != expected_count) return nullptr;
        return reinterpret_cast<T*>(
            static_cast<uint8_t*>(array) + kIl2CppArrayDataOffset);
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

bool ValidateBoneWeightLayout() {
    for (const auto& field : kBoneWeightFields) {
        BE_ResolvedFieldV1 result{};
        const BE_Result status = g_host->resolve_field(
            g_host->context, &field.descriptor, &result);
        if (status != BE_Result_Ok || !result.field_info ||
            result.offset != field.expected_offset) {
            Log(std::string("BoneWeight layout mismatch: ") + field.key +
                " expected=" + std::to_string(field.expected_offset) +
                " actual=" + std::to_string(result.offset));
            return false;
        }
    }
    Log("Validated UnityEngine.BoneWeight 32-byte PoC layout.");
    return true;
}

struct RendererSummary {
    void* renderer = nullptr;
    void* mesh = nullptr;
    std::string renderer_name;
    std::string mesh_name;
    std::string path;
    uint64_t total_indices = 0;
    int vertex_count = 0;
    int bones_count = 0;
    int bindpose_count = 0;
    int lod = -1;
    bool c9_signature = false;
    bool exact_name = false;
    bool live_clone = false;
};

const char* TargetNameForLod(int lod) {
    return lod == 1 ? kTargetRendererNameLod1 : kTargetRendererNameLod0;
}

uint32_t OriginalIndexCountForLod(int lod) {
    return lod == 1 ? kComponent9OriginalIndexCountLod1 : kComponent9OriginalIndexCountLod0;
}

RendererSummary SummarizeRenderer(void* renderer) {
    RendererSummary summary;
    summary.renderer = renderer;
    summary.renderer_name = ObjectName(renderer);
    summary.path = BuildTransformPath(renderer);
    summary.lod = PathLod(summary.path);
    summary.mesh = Invoke(Contract("skinned.get_shared_mesh"), renderer, nullptr, false);
    summary.mesh_name = ObjectName(summary.mesh);
    summary.bones_count = ArrayLength(Invoke(Contract("skinned.get_bones"), renderer, nullptr, false));

    if (summary.mesh) {
        InvokeValue(Contract("mesh.get_vertex_count"), summary.mesh, nullptr, summary.vertex_count);
        int sub_mesh_count = 0;
        if (InvokeValue(Contract("mesh.get_sub_mesh_count"), summary.mesh, nullptr, sub_mesh_count)) {
            const int count = std::min(std::max(sub_mesh_count, 0), kMaxSubMeshes);
            for (int submesh = 0; submesh < count; ++submesh) {
                uint32_t index_count = 0;
                void* parameters[1]{&submesh};
                if (InvokeValue(Contract("mesh.get_index_count"), summary.mesh, parameters, index_count)) {
                    summary.total_indices += index_count;
                }
            }
        }
        summary.bindpose_count = ArrayLength(Invoke(Contract("mesh.get_bindposes"), summary.mesh, nullptr, false));
    }

    if (summary.lod == 0 || summary.lod == 1) {
        summary.c9_signature = summary.total_indices == OriginalIndexCountForLod(summary.lod);
        summary.exact_name =
            summary.renderer_name == TargetNameForLod(summary.lod) &&
            summary.mesh_name == TargetNameForLod(summary.lod);
    }
    summary.live_clone = IsLiveClonePath(summary.path);
    return summary;
}

void LogRenderer(const RendererSummary& item, int ordinal) {
    std::string line = "  candidate[" + std::to_string(ordinal) + "] renderer=\"" +
        item.renderer_name + "\" mesh=\"" + item.mesh_name +
        "\" lod=" + std::to_string(item.lod) +
        " vertices=" + std::to_string(item.vertex_count) +
        " totalIndices=" + std::to_string(item.total_indices) +
        " bones=" + std::to_string(item.bones_count) +
        " bindposes=" + std::to_string(item.bindpose_count);
    if (item.c9_signature) line += " C9_SIGNATURE";
    if (item.exact_name) line += " EXACT_NAME";
    if (item.live_clone) line += " LIVE_CLONE";
    Log(line);
    if (!item.path.empty()) Log("    path=\"" + item.path + "\"");
}

std::vector<RendererSummary> EnumerateRenderers() {
    std::vector<RendererSummary> summaries;
    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* objects = Invoke(Contract("resources.find_objects_of_type_all"), nullptr, parameters);
    const int raw_count = ArrayLength(objects);
    const int count = std::min(std::max(raw_count, 0), kMaxRenderers);
    summaries.reserve(static_cast<size_t>(count));
    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (renderer) summaries.push_back(SummarizeRenderer(renderer));
    }
    Log("SkinnedMeshRenderer objects discovered=" + std::to_string(raw_count));
    return summaries;
}

std::vector<RendererSummary> EnumerateLiveComponent9Renderers(bool log_discovered = false) {
    std::vector<RendererSummary> summaries;
    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* objects = Invoke(Contract("resources.find_objects_of_type_all"), nullptr, parameters);
    const int raw_count = ArrayLength(objects);
    const int count = std::min(std::max(raw_count, 0), kMaxRenderers);

    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (!renderer) continue;
        const std::string name = ObjectName(renderer);
        if (name != kTargetRendererNameLod0 && name != kTargetRendererNameLod1) continue;
        const std::string path = BuildTransformPath(renderer);
        if (PathLod(path) < 0 || !IsSupportedLiveTargetPath(path)) continue;
        summaries.push_back(SummarizeRenderer(renderer));
    }

    if (log_discovered) {
        int lod0 = 0, lod1 = 0;
        for (const auto& item : summaries) {
            if (item.lod == 0) ++lod0;
            if (item.lod == 1) ++lod1;
        }
        Log("Live Component9 renderer scan: allSMR=" + std::to_string(raw_count) +
            " liveC9=" + std::to_string(summaries.size()) +
            " lod0=" + std::to_string(lod0) + " lod1=" + std::to_string(lod1));
    }
    return summaries;
}

MeshVariantState& VariantForLod(int lod) {
    return lod == 1 ? g_lod1 : g_lod0;
}

bool IsOriginalLiveComponent9(const RendererSummary& item) {
    if (!item.renderer || !item.mesh || (item.lod != 0 && item.lod != 1) ||
        !item.exact_name || !item.c9_signature || item.bones_count != 156 ||
        item.bindpose_count != 156 || !IsSupportedLiveTargetPath(item.path)) {
        return false;
    }
    return true;
}

bool IsCustomLiveComponent9(const RendererSummary& item) {
    if (!item.renderer || !item.mesh || (item.lod != 0 && item.lod != 1) ||
        item.renderer_name != TargetNameForLod(item.lod) || item.bones_count != 156 ||
        item.bindpose_count != 156 || !IsSupportedLiveTargetPath(item.path)) {
        return false;
    }
    return item.mesh == VariantForLod(item.lod).custom_mesh;
}

struct TargetCounts {
    int total = 0;
    int gameplay = 0;
    int ui = 0;
    int lod0 = 0;
    int lod1 = 0;
};

void CountTarget(const RendererSummary& item, TargetCounts& counts) {
    ++counts.total;
    if (IsGameplayPostModelPath(item.path)) ++counts.gameplay;
    if (IsUiModelPath(item.path)) ++counts.ui;
    if (item.lod == 0) ++counts.lod0;
    if (item.lod == 1) ++counts.lod1;
}

void LogComponent9Candidates(const std::vector<RendererSummary>& summaries) {
    int ordinal = 0;
    for (const auto& item : summaries) {
        if (item.renderer_name == kTargetRendererNameLod0 ||
            item.renderer_name == kTargetRendererNameLod1 || item.c9_signature) {
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

void ReleaseVariantHandle(MeshVariantState& state) {
    if (!g_host || !g_host->gchandle_free) return;
    if (state.original_handle) g_host->gchandle_free(g_host->context, state.original_handle);
    if (state.custom_handle) g_host->gchandle_free(g_host->context, state.custom_handle);
    state.original_handle = 0;
    state.custom_handle = 0;
}

void ReleaseReplacementHandles() {
    ReleaseVariantHandle(g_lod0);
    ReleaseVariantHandle(g_lod1);
}

void ClearReplacementState() {
    ReleaseReplacementHandles();
    g_lod0.original_mesh = nullptr;
    g_lod0.custom_mesh = nullptr;