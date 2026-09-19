// Two explicit experimental entry points. Resource mode never calls the scene
// scan as compensation: missed/off-thread delivery remains visible in results.
bool MultiMainThread() {
    if (!g_early_main_thread.method_info) return false;
    void* exception = nullptr;
    bool main = false;
    void* boxed = g_host->runtime_invoke(g_host->context, g_early_main_thread.method_info,
        nullptr, nullptr, &exception);
    return !exception && Unbox(boxed, main) && main;
}

int MultiCharacterForAsset(std::string_view name) {
    if (name == "chr_0003_endminf_postmodel" || name == "chr_0003_endminf_uimodel" ||
        name == "chr_0003_endminf_postmodel(Clone)" || name == "chr_0003_endminf_uimodel(Clone)") return 0;
    if (name == "chr_0004_pelica_postmodel" || name == "chr_0004_pelica_uimodel" ||
        name == "chr_0004_pelica_postmodel(Clone)" || name == "chr_0004_pelica_uimodel(Clone)") return 1;
    return -1;
}

void MultiApplyModel(void* model, const char* entry, bool resource) {
    if (!g_multi_character || g_resource_path != resource || !model) return;
    if (!MultiMainThread()) {
        static std::atomic_uint32_t missed{0};
        if (resource && missed.fetch_add(1) < 8)
            Log("[MULTI] RESOURCE off-thread delivery skipped (asset identity not read); no instance fallback.");
        return;
    }
    if (g_operation_in_progress.exchange(true)) return;
    struct Guard {
        ~Guard() { g_template_materials = false; g_operation_in_progress.store(false); }
    } guard;
    const int character = MultiCharacterForAsset(ObjectName(model));
    if (character < 0 || !g_character_sessions[character].armed) return;
    CharacterScope scope(static_cast<size_t>(character));
    if (g_refresh_suspended || !g_natural_trace_get_components.method_info) return;
    int32_t frame = -1;
    if (!InvokeValue(Contract("time.get_frame_count"), nullptr, nullptr, frame) ||
        frame < g_earliest_apply_frame) return;
    const uint32_t handle = g_host->gchandle_new(g_host->context, model, 0);
    if (!handle) return;
    struct Root {
        uint32_t handle;
        ~Root() { g_host->gchandle_free(g_host->context, handle); }
    } root{handle};
    bool inactive = true;
    void* args[2]{g_skinned_renderer_class.type_object, &inactive};
    void* exception = nullptr;
    void* renderers = g_host->runtime_invoke(g_host->context,
        g_natural_trace_get_components.method_info, model, args, &exception);
    if (exception || !renderers) return;
    const uint32_t array_handle = g_host->gchandle_new(g_host->context, renderers, 0);
    if (!array_handle) return;
    Root array_root{array_handle};
    std::vector<RendererSummary> summaries;
    for (int i = 0; i < ArrayLength(renderers); ++i) {
        void* renderer = ArrayValue(renderers, i);
        if (!renderer) continue;
        const std::string path = BuildTransformPath(renderer);
        if (!IsTargetLod0Draw(ObjectName(renderer), path)) continue;
        if (!resource && !IsActiveSceneRenderer(renderer)) continue;
        summaries.push_back(SummarizeRenderer(renderer, path));
    }
    if (summaries.empty()) return;
    g_template_materials = resource;
    const size_t before = g_replaced.size();
    if (!g_replacement_active) ApplyReplacement(summaries);
    else RefreshActiveReplacement(summaries);
    if (resource) {
        for (const auto& record : g_replaced) {
            if (record.hidden_by_mod && IsNativeObjectAlive(record.renderer))
                SetRendererEnabled(record.renderer, g_mask_visible);
        }
    }
    Log(std::string("[MULTI] ") + entry + " asset=" + ObjectName(model) + " character=" + g_target_postmodel +
        " candidates=" + std::to_string(summaries.size()) +
        " recordsBefore=" + std::to_string(before) +
        " recordsAfter=" + std::to_string(g_replaced.size()) +
        " active=" + (g_replacement_active ? "1" : "0"));
}

void MultiAssemblyComplete(void* part, const char* entry) {
    if (!g_multi_character || g_resource_path || !MultiMainThread()) return;
    void* exception = nullptr;
    void* model = g_host->runtime_invoke(g_host->context, g_delivery_part_model.method_info,
        part, nullptr, &exception);
    if (!exception) MultiApplyModel(model, entry, false);
}
