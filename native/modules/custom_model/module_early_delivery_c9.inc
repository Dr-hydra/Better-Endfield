// Replace all supported LOD0 components at initial delivery, preserving the rig.
BemPocData g_early_bem;
bool g_early_payload_attempted = false;
thread_local bool g_early_busy = false;
std::unordered_set<void*> g_early_delivered_prefabs;
std::unordered_set<void*> g_early_custom_meshes;
std::vector<uint32_t> g_early_persistent_handles;

void EarlyDeliveryShutdown() {
    for (auto handle : g_early_persistent_handles) {
        if (g_host && g_host->gchandle_free) {
            g_host->gchandle_free(g_host->context, handle);
        }
    }
    g_early_persistent_handles.clear();
    g_early_delivered_prefabs.clear();
    g_early_custom_meshes.clear();
}

void EarlyDeliveryC9(void* asset) {
    if (!asset || g_early_busy || !g_early_main_thread.method_info) return;
    void* exception = nullptr;
    bool main_thread = false;
    void* boxed = g_host->runtime_invoke(g_host->context, g_early_main_thread.method_info, nullptr, nullptr, &exception);
    if (exception || !Unbox(boxed, main_thread) || !main_thread) return;
    const auto name = ObjectName(asset);
    if (name != "chr_0003_endminf_postmodel") return;
    if (NaturalTraceObjectTypeName(asset) != "UnityEngine.GameObject") return;
    if (g_early_delivered_prefabs.count(asset) > 0) return;
    g_early_busy = true;
    struct Reset { ~Reset() { g_early_busy = false; } } reset;
    // Pending arrays/objects live in native vectors; root them across managed allocations.
    struct Roots {
        std::vector<uint32_t> handles;
        bool keep(void* object) {
            if (!object || !g_host->gchandle_new || !g_host->gchandle_free) return false;
            const auto handle = g_host->gchandle_new(g_host->context, object, 0);
            if (!handle) return false;
            handles.push_back(handle);
            return true;
        }
        ~Roots() { for (auto handle : handles) g_host->gchandle_free(g_host->context, handle); }
    } roots;
    if (!roots.keep(asset)) return;
    if (!g_early_payload_attempted) {
        g_early_payload_attempted = true;
        if (!LoadBemPoc(BemPath(), g_early_bem)) { Log("[EARLY-FULL] payload failed; unchanged."); return; }
    }
    static const char* names[]{"hair_01","face_01","cloth_03","cloth_04","eyebrow_01","body_01","cloth_05","iris_01","eyeshadow_01","cloth_01","cloth_02"};
    static const uint32_t indices[]{27615,9000,4524,20577,1638,16524,117,1386,90,101994,2286};
    static const int bones[]{28,53,1,14,42,44,1,6,21,156,29};
    if (g_early_bem.components.size() != 11 || !TextureContractsAvailable()) { Log("[EARLY-FULL] contracts/payload unavailable."); return; }
    bool inactive = true;
    void* args[2]{g_skinned_renderer_class.type_object, &inactive};
    void* renderers = g_host->runtime_invoke(g_host->context, g_natural_trace_get_components.method_info, asset, args, &exception);
    const int count = exception ? -1 : ArrayLength(renderers);
    if (count < 0 || count > 128 || !roots.keep(renderers)) return;
    for (int i = 0; i < count; ++i) {
        void* candidate = ArrayValue(renderers, i);
        if (!candidate) continue;
        void* candidate_mesh = Invoke(Contract("skinned.get_shared_mesh"), candidate, nullptr, false);
        if (candidate_mesh && g_early_custom_meshes.count(candidate_mesh) > 0) {
            g_early_delivered_prefabs.insert(asset);
            Log("[EARLY-FULL] prefab=" + name + " asset=" + NaturalTracePointer(asset) +
                " already carries custom mesh; skipping re-delivery.");
            return;
        }
    }
    struct Pending {
        const BemComponent* component = nullptr;
        RendererSummary source{};
        void* mesh = nullptr;
        void* old_material = nullptr;
        void* material = nullptr;
        bool enabled = false, hidden = false;
    };
    std::vector<Pending> pending;
    std::vector<std::pair<void*, void*>> materials;
    const auto cleanup = [&]() {
        for (auto& p : pending) if (p.mesh) DestroyUnityObject(p.mesh);
        for (auto& p : materials) DestroyUnityObject(p.second);
    };
    const auto fail = [&](const std::string& why) { cleanup(); Log("[EARLY-FULL] preparation failed: " + why + "; prefab unchanged."); };
    std::array<bool, 11> seen{};
    for (const auto& component : g_early_bem.components) {
        const auto id = component.info.component_id;
        if (id >= 11 || seen[id] || component.info.original_index_count != indices[id]) { fail("component identity"); return; }
        seen[id] = true;
        const std::string expected = std::string("S_actor_endminf_") + names[id] + "_lod0";
        void* renderer = nullptr;
        int matches = 0;
        for (int i = 0; i < count; ++i) {
            void* candidate = ArrayValue(renderers, i);
            if (ObjectName(candidate) == expected) { renderer = candidate; ++matches; }
        }
        if (matches != 1) { fail(expected + " match"); return; }
        Pending item;
        item.component = &component;
        item.source = SummarizeRenderer(renderer);
        item.hidden = (component.info.flags & (kComponentFlagHidden | kComponentFlagNoGeometry)) != 0;
        void* shared = Invoke(Contract("renderer.get_shared_materials"), renderer, nullptr, false);
        const int material_count = ArrayLength(shared);
        const bool have_enabled = GetRendererEnabled(renderer, item.enabled);
        if (item.source.mesh && g_early_custom_meshes.count(item.source.mesh) > 0) {
            g_early_delivered_prefabs.insert(asset);
            Log("[EARLY-FULL] prefab=" + name + " asset=" + NaturalTracePointer(asset) +
                " component " + expected + " is already customized; skipping.");
            cleanup();
            return;
        }
        if (item.source.mesh_name != expected || item.source.total_indices != indices[id] ||
            item.source.bones_count != bones[id] || item.source.bindpose_count != bones[id] ||
            material_count <= 0 || material_count > 32 || !have_enabled) {
            fail(expected + " mesh=" + item.source.mesh_name + " indices=" + std::to_string(item.source.total_indices) +
                " bones=" + std::to_string(item.source.bones_count) + " bindposes=" + std::to_string(item.source.bindpose_count) +
                " materials=" + std::to_string(material_count) + " enabledReadable=" + (have_enabled ? "true" : "false")); return;
        }
        item.old_material = shared; // Keep complete arrays, including null slots.
        if (!roots.keep(shared) || !roots.keep(item.source.mesh)) { fail("source roots"); return; }
        pending.push_back(item);
        auto& p = pending.back();
        if (!item.hidden) {
            if (!ValidateRendererSkin(component, renderer, item.source.mesh) || !BuildMeshFromComponent(component, item.source.mesh, p.mesh)) {
                fail(expected + " mesh/skin"); return;
            }
            if (!roots.keep(p.mesh)) { fail("mesh root"); return; }
            void* managed_name = g_host->string_new(g_host->context, expected.c_str());
            void* name_args[1]{managed_name};
            if (!managed_name || !InvokeVoid(Contract("object.set_name"), p.mesh, name_args)) { fail("mesh name"); return; }
        }
        int array_size = material_count;
        void* create_args[2]{g_material_class.type_object, &array_size};
        p.material = Invoke(Contract("array.create_instance"), nullptr, create_args);
        if (!p.material || !roots.keep(p.material)) { fail("material array allocation"); return; }
        for (int slot = 0; slot < material_count; ++slot) {
            void* original = ArrayValue(shared, slot);
            void* copy = nullptr;
            for (auto& pair : materials) if (pair.first == original) copy = pair.second;
            if (original && !copy) {
                copy = g_host->object_new(g_host->context, g_material_class.class_info);
                void* ctor_args[1]{original};
                if (!copy || !EarlyInvoke(g_early_material_ctor, copy, ctor_args)) {
                    if (copy) DestroyUnityObject(copy);
                    fail("material copy"); return;
                }
                materials.emplace_back(original, copy);
                if (!roots.keep(copy)) { fail("material root"); return; }
            }
            void* set_args[2]{copy, &slot};
            if (!EarlyInvoke(g_early_array_set, p.material, set_args)) { fail("material array slot"); return; }
        }
    }
    const auto set_material = [&](void* renderer, void* material) {
        void* a[1]{material};
        if (!EarlyInvoke(g_early_set_material, renderer, a)) return false;
        void* actual = Invoke(Contract("renderer.get_shared_materials"), renderer, nullptr, false);
        const int size = ArrayLength(material);
        if (ArrayLength(actual) != size) return false;
        for (int i = 0; i < size; ++i) if (ArrayValue(actual, i) != ArrayValue(material, i)) return false;
        return true;
    };
    const auto set_mesh = [&](void* renderer, void* mesh) {
        return SetSharedMesh(renderer, mesh) && Invoke(Contract("skinned.get_shared_mesh"), renderer, nullptr, false) == mesh;
    };
    const auto set_enabled = [&](void* renderer, bool enabled) {
        bool actual = !enabled;
        return SetRendererEnabled(renderer, enabled) && GetRendererEnabled(renderer, actual) && actual == enabled;
    };
    // Prepare everything before committing. Restore all original assignments if any write fails.
    bool committed = true;
    for (auto& p : pending) {
        if (!set_material(p.source.renderer, p.material) ||
            (p.mesh && !set_mesh(p.source.renderer, p.mesh)) ||
            (p.hidden && !set_enabled(p.source.renderer, false))) { committed = false; break; }
    }
    if (!committed) {
        bool restored = true;
        for (auto& p : pending) {
            const bool a = set_material(p.source.renderer, p.old_material);
            const bool b = set_mesh(p.source.renderer, p.source.mesh);
            const bool c = set_enabled(p.source.renderer, p.enabled);
            restored = a && b && c && restored;
        }
        if (restored) cleanup(); // Preserve resources if a live assignment might remain.
        Log(std::string("[EARLY-FULL] commit failed; restored=") + (restored ? "true" : "false"));
        return;
    }
    std::vector<std::pair<uint32_t, void*>> targets;
    int meshes = 0, hidden = 0;
    for (auto& p : pending) {
        targets.emplace_back(p.component->info.component_id, p.source.renderer);
        meshes += p.mesh != nullptr;
        hidden += p.hidden;
    }
    // Existing texture matcher honors pins and leaves ambiguous slots original.
    InstallCustomTextures(g_early_bem, targets);

    g_early_delivered_prefabs.insert(asset);
    for (auto& p : pending) {
        if (p.mesh) {
            g_early_custom_meshes.insert(p.mesh);
            if (g_host->gchandle_new) {
                const auto h = g_host->gchandle_new(g_host->context, p.mesh, 0);
                if (h) g_early_persistent_handles.push_back(h);
            }
        }
        if (p.material && g_host->gchandle_new) {
            const auto h = g_host->gchandle_new(g_host->context, p.material, 0);
            if (h) g_early_persistent_handles.push_back(h);
        }
    }
    for (auto& pair : materials) {
        if (pair.second && g_host->gchandle_new) {
            const auto h = g_host->gchandle_new(g_host->context, pair.second, 0);
            if (h) g_early_persistent_handles.push_back(h);
        }
    }

    // Synchronize global replacement state for shadow & AI coverage
    g_active_bem = g_early_bem;
    g_replacement_active = true;
    for (auto& p : pending) {
        ReplacedComponent record;
        record.component_id = p.component->info.component_id;
        record.original_index_count = p.component->info.original_index_count;
        record.renderer = p.source.renderer;
        record.original_mesh = p.source.mesh;
        record.custom_mesh = p.mesh;
        record.custom_material = p.material;
        record.hidden_by_mod = p.hidden;
        record.original_enabled = p.enabled;
        PinReplacement(record);
        g_replaced.push_back(record);
    }

    // Shadow configuration for world character postmodel
    if (name == "chr_0003_endminf_postmodel") {
        int shadow_casting_on = 0;
        for (auto& p : pending) {
            if (p.mesh && AiCoverageSetShadowMode(p.source.renderer, kShadowCastingOn)) {
                ++shadow_casting_on;
            }
        }
        int proxies_disabled = 0;
        for (int i = 0; i < count; ++i) {
            void* candidate = ArrayValue(renderers, i);
            if (!candidate) continue;
            const std::string cand_name = ObjectName(candidate);
            if (cand_name.find("shadowProxy") != std::string::npos) {
                const std::string stem = AiCoverageComponentStem(cand_name);
                if (AiCoverageStemHasCustomMesh(stem)) {
                    if (SetRendererEnabled(candidate, false)) {
                        ++proxies_disabled;
                    }
                }
            }
        }
        Log("[EARLY-FULL] postmodel shadow state: castingOn=" +
            std::to_string(shadow_casting_on) + " proxiesDisabled=" + std::to_string(proxies_disabled));
    }

    Log("[EARLY-FULL] COMMITTED prefab=" + name + " asset=" + NaturalTracePointer(asset) +
        " components=11 meshes=" + std::to_string(meshes) + " hidden=" + std::to_string(hidden) +
        " privateMaterials=" + std::to_string(materials.size()) + "; LOD0, see texture install summary.");
}
