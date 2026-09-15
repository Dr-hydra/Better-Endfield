// Persistent scene coverage for control-switch/AI renderers plus a gameplay
// shadow experiment.
//
// The production replacement owns the BEM Mesh objects. This layer never calls
// BuildMeshFromComponent: when the game spawns/reconfigures another renderer for
// the same component (for example after switching the controlled character), it
// only reuses an already-existing custom Mesh after proving the new renderer's
// source Mesh has the same declaration, strides and bindpose palette.
//
// Gameplay uses separate shadowProxy renderers while the detail-screen model
// casts from its normal SkinnedMeshRenderers. For components that already have
// BEM geometry, this layer mirrors the detail-screen arrangement: the normal
// renderer is set to ShadowCastingMode.On and the corresponding shadowProxy is
// disabled. Every changed renderer is recorded and restored on F11/F12 rollback
// and shutdown. If the shadowCastingMode setter cannot be resolved, shadow
// changes are skipped entirely; AI coverage still runs.

constexpr uint64_t kAiCoverageScanIntervalMs = 100;
constexpr int32_t kShadowCastingOff = 0;
constexpr int32_t kShadowCastingOn = 1;

const BE_HostApiV1* g_ai_coverage_real_host = nullptr;
BE_HostApiV1 g_ai_coverage_host_proxy{};
bool g_ai_coverage_pump_intercepted = false;
uint64_t g_ai_coverage_next_scan_tick = 0;
const void* g_ai_coverage_set_shadow_mode_method = nullptr;

struct AiCoverageShadowModeState {
    void* renderer = nullptr;
    uint32_t handle = 0;
    int32_t original_mode = kShadowCastingOff;
};

struct AiCoverageProxyState {
    void* renderer = nullptr;
    uint32_t handle = 0;
    bool original_enabled = true;
};

struct AiCoverageHiddenVfxPartState {
    void* renderer = nullptr;
    uint32_t handle = 0;
    bool original_enabled = true;
};

std::vector<AiCoverageShadowModeState> g_ai_coverage_shadow_modes;
std::vector<AiCoverageProxyState> g_ai_coverage_shadow_proxies;
std::vector<AiCoverageHiddenVfxPartState> g_ai_coverage_hidden_vfxparts;

struct CharacterCoverageState {
    std::vector<AiCoverageShadowModeState> modes;
    std::vector<AiCoverageProxyState> proxies;
    std::vector<AiCoverageHiddenVfxPartState> hidden;
    uint64_t next_tick = 0;
};
std::array<CharacterCoverageState, 2> g_saved_coverage;
void SwapCoverage(size_t i) {
    g_ai_coverage_shadow_modes.swap(g_saved_coverage[i].modes);
    g_ai_coverage_shadow_proxies.swap(g_saved_coverage[i].proxies);
    g_ai_coverage_hidden_vfxparts.swap(g_saved_coverage[i].hidden);
    std::swap(g_ai_coverage_next_scan_tick, g_saved_coverage[i].next_tick);
}

std::string AiCoveragePointer(const void* pointer) {
    std::array<char, 32> buffer{};
    std::snprintf(buffer.data(), buffer.size(), "%p", pointer);
    return std::string(buffer.data());
}

std::string AiCoverageComponentStem(std::string name) {
    const size_t shadow = name.find("_shadowProxy");
    if (shadow != std::string::npos) name.resize(shadow);

    const size_t lod = name.rfind("_lod");
    if (lod != std::string::npos && lod + 5 == name.size() &&
        std::isdigit(static_cast<unsigned char>(name[lod + 4]))) {
        name.resize(lod);
    }
    return name;
}

bool AiCoverageBindposesEqual(void* left_mesh, void* right_mesh) {
    if (!left_mesh || !right_mesh) return false;
    void* left = Invoke(Contract("mesh.get_bindposes"), left_mesh, nullptr, false);
    void* right = Invoke(Contract("mesh.get_bindposes"), right_mesh, nullptr, false);
    const int left_count = ArrayLength(left);
    const int right_count = ArrayLength(right);
    if (left_count <= 0 || left_count != right_count) return false;
    const Matrix4x4Raw* left_data = ManagedArrayData<Matrix4x4Raw>(
        left, static_cast<size_t>(left_count));
    const Matrix4x4Raw* right_data = ManagedArrayData<Matrix4x4Raw>(
        right, static_cast<size_t>(right_count));
    return left_data && right_data &&
        std::memcmp(left_data, right_data,
            static_cast<size_t>(left_count) * sizeof(Matrix4x4Raw)) == 0;
}

bool AiCoverageMeshesCompatible(void* source_mesh, void* custom_mesh) {
    if (!source_mesh || !custom_mesh || !IsNativeObjectAlive(source_mesh) ||
        !IsNativeObjectAlive(custom_mesh)) {
        return false;
    }
    VertexDeclaration source_declaration;
    VertexDeclaration custom_declaration;
    if (!ReadVertexDeclaration(source_mesh, source_declaration) ||
        !ReadVertexDeclaration(custom_mesh, custom_declaration) ||
        !DeclarationsEqual(source_declaration, custom_declaration)) {
        return false;
    }
    std::vector<int32_t> source_strides;
    std::vector<int32_t> custom_strides;
    if (!ReadMeshStrides(source_mesh, source_strides) ||
        !ReadMeshStrides(custom_mesh, custom_strides) ||
        source_strides != custom_strides) {
        return false;
    }
    return AiCoverageBindposesEqual(source_mesh, custom_mesh);
}

const ReplacedComponent* AiCoverageFindReusableSource(
    const RendererSummary& item) {
    if (!item.renderer || !item.mesh) return nullptr;

    if (const ReplacedComponent* identity = ReplacementSource(item.mesh);
        identity && identity->custom_mesh &&
        IsNativeObjectAlive(identity->custom_mesh)) {
        return identity;
    }

    const std::string renderer_stem =
        AiCoverageComponentStem(item.renderer_name);
    const std::string mesh_stem = AiCoverageComponentStem(item.mesh_name);
    for (const ReplacedComponent& record : g_replaced) {
        if (!record.original_mesh ||
            !IsNativeObjectAlive(record.original_mesh)) {
            continue;
        }
        if (!record.custom_mesh && !record.hidden_by_mod) {
            continue;
        }
        if (record.custom_mesh && !IsNativeObjectAlive(record.custom_mesh)) {
            continue;
        }
        const std::string source_stem =
            AiCoverageComponentStem(ObjectName(record.original_mesh));
        if (source_stem.empty() ||
            (source_stem != renderer_stem && source_stem != mesh_stem)) {
            continue;
        }
        return &record;
    }
    return nullptr;
}

void AiCoverageFreeHandle(uint32_t& handle) {
    if (handle && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, handle);
    }
    handle = 0;
}

bool AiCoverageGetShadowMode(void* renderer, int32_t& mode) {
    mode = -1;
    return renderer &&
        InvokeValue(Contract("renderer.get_shadow_casting_mode"), renderer,
            nullptr, mode);
}

bool AiCoverageSetShadowMode(void* renderer, int32_t mode) {
    if (!renderer || !g_ai_coverage_set_shadow_mode_method || !g_host ||
        !g_host->runtime_invoke || !IsNativeObjectAlive(renderer)) {
        return false;
    }
    void* parameters[1]{&mode};
    void* exception = nullptr;
    g_host->runtime_invoke(g_host->context,
        g_ai_coverage_set_shadow_mode_method, renderer, parameters, &exception);
    if (exception) return false;
    int32_t read_back = -1;
    return AiCoverageGetShadowMode(renderer, read_back) && read_back == mode;
}

void AiCoverageRememberShadowMode(void* renderer, int32_t original_mode) {
    for (const auto& state : g_ai_coverage_shadow_modes) {
        if (state.renderer == renderer) return;
    }
    AiCoverageShadowModeState state;
    state.renderer = renderer;
    state.original_mode = original_mode;
    if (g_host && g_host->gchandle_new) {
        state.handle = g_host->gchandle_new(g_host->context, renderer, 0);
    }
    g_ai_coverage_shadow_modes.push_back(state);
}

void AiCoverageRememberProxy(void* renderer, bool original_enabled) {
    for (const auto& state : g_ai_coverage_shadow_proxies) {
        if (state.renderer == renderer) return;
    }
    AiCoverageProxyState state;
    state.renderer = renderer;
    state.original_enabled = original_enabled;
    if (g_host && g_host->gchandle_new) {
        state.handle = g_host->gchandle_new(g_host->context, renderer, 0);
    }
    g_ai_coverage_shadow_proxies.push_back(state);
}

void AiCoverageRememberHiddenVfxPart(void* renderer, bool original_enabled) {
    for (const auto& state : g_ai_coverage_hidden_vfxparts) {
        if (state.renderer == renderer) return;
    }
    AiCoverageHiddenVfxPartState state;
    state.renderer = renderer;
    state.original_enabled = original_enabled;
    if (g_host && g_host->gchandle_new) {
        state.handle = g_host->gchandle_new(g_host->context, renderer, 0);
    }
    g_ai_coverage_hidden_vfxparts.push_back(state);
    Log(std::string("[AI-SHADOW] hiding unmatched vfxpart renderer: ") + ObjectName(renderer));
}

void RestoreAiCoverageHiddenVfxParts() {
    int restored = 0;
    for (auto& state : g_ai_coverage_hidden_vfxparts) {
        if (state.renderer && IsNativeObjectAlive(state.renderer)) {
            if (SetRendererEnabled(state.renderer, state.original_enabled)) {
                ++restored;
            }
        }
        AiCoverageFreeHandle(state.handle);
    }
    if (!g_ai_coverage_hidden_vfxparts.empty()) {
        Log("[AI-SHADOW] restored hidden vfxpart renderers: " + std::to_string(restored));
    }
    g_ai_coverage_hidden_vfxparts.clear();
}

void RestoreAiShadowCoverage() {
    int restored_modes = 0;
    int restored_proxies = 0;
    for (auto& state : g_ai_coverage_shadow_modes) {
        if (state.renderer && IsNativeObjectAlive(state.renderer) &&
            AiCoverageSetShadowMode(state.renderer, state.original_mode)) {
            ++restored_modes;
        }
        AiCoverageFreeHandle(state.handle);
    }
    for (auto& state : g_ai_coverage_shadow_proxies) {
        if (state.renderer && IsNativeObjectAlive(state.renderer) &&
            SetRendererEnabled(state.renderer, state.original_enabled)) {
            ++restored_proxies;
        }
        AiCoverageFreeHandle(state.handle);
    }
    RestoreAiCoverageHiddenVfxParts();
    if (!g_ai_coverage_shadow_modes.empty() ||
        !g_ai_coverage_shadow_proxies.empty()) {
        Log("[AI-SHADOW] restored shadow state: castingModes=" +
            std::to_string(restored_modes) + " proxies=" +
            std::to_string(restored_proxies));
    }
    g_ai_coverage_shadow_modes.clear();
    g_ai_coverage_shadow_proxies.clear();
}

bool AiCoverageHasRendererRecord(void* renderer, size_t& index) {
    index = 0;
    while (index < g_replaced.size() && g_replaced[index].renderer != renderer)
        ++index;
    return index < g_replaced.size();
}

void* AiCoverageFindDonorBones(
    const RendererSummary& receiver,
    const BemComponent& component,
    const std::vector<RendererSummary>& donor_pool,
    void** out_root_bone) {
    if (out_root_bone) *out_root_bone = nullptr;

    const std::string receiver_root = PathRoot(receiver.path);
    const std::string stem = AiCoverageComponentStem(receiver.renderer_name);

    // 1. First priority: Check g_replaced records on the same character instance
    for (const ReplacedComponent& record : g_replaced) {
        if (!record.renderer || !IsNativeObjectAlive(record.renderer)) continue;
        const std::string record_path = BuildTransformPath(record.renderer);
        if (PathRoot(record_path) != receiver_root) continue;

        std::string record_stem;
        if (record.original_mesh && IsNativeObjectAlive(record.original_mesh)) {
            record_stem = AiCoverageComponentStem(ObjectName(record.original_mesh));
        } else {
            record_stem = AiCoverageComponentStem(ObjectName(record.renderer));
        }
        if (record_stem != stem) continue;

        void* bones = Invoke(Contract("skinned.get_bones"), record.renderer, nullptr, false);
        const int bone_count = ArrayLength(bones);
        if (bone_count > static_cast<int>(component.info.max_bone)) {
            if (out_root_bone) {
                *out_root_bone = Invoke(Contract("skinned.get_root_bone"), record.renderer, nullptr, false);
            }
            return bones;
        }
    }

    // 2. Second priority: Search donor_pool for sibling renderer on the same character instance
    for (const RendererSummary& donor : donor_pool) {
        if (!donor.renderer || donor.renderer == receiver.renderer) continue;
        if (PathRoot(donor.path) != receiver_root) continue;

        const std::string donor_stem = AiCoverageComponentStem(donor.renderer_name);
        if (donor_stem != stem) continue;

        void* bones = Invoke(Contract("skinned.get_bones"), donor.renderer, nullptr, false);
        const int bone_count = ArrayLength(bones);
        if (bone_count > static_cast<int>(component.info.max_bone)) {
            if (out_root_bone) {
                *out_root_bone = Invoke(Contract("skinned.get_root_bone"), donor.renderer, nullptr, false);
            }
            return bones;
        }
    }

    return nullptr;
}

bool AiCoverageAttachReuseOnly(
    const RendererSummary& item,
    const std::vector<RendererSummary>& donor_pool) {
    if (!item.renderer || !item.mesh ||
        item.renderer_name.find(kActorMeshPrefix) != 0 ||
        item.renderer_name.find("shadowProxy") != std::string::npos ||
        item.path.find("SK_actor_female") != std::string::npos) {
        return false;
    }

    // Pelica uses the main LOD0 refresh; alternate LOD palette reuse is unverified.
    if (g_target_pelica) return false;

    size_t known = 0;
    const bool existed = AiCoverageHasRendererRecord(item.renderer, known);
    if (existed) {
        const auto& existing = g_replaced[known];
        const bool mesh_matches = existing.custom_mesh
            ? (item.mesh == existing.custom_mesh)
            : (item.mesh == existing.original_mesh);
        if (mesh_matches) {
            void* mat_to_apply = existing.custom_material;
            if (!mat_to_apply) {
                if (const auto* s = AiCoverageFindReusableSource(item)) {
                    mat_to_apply = s->custom_material;
                }
            }
            if (mat_to_apply &&
                !RendererMaterialsMatch(item.renderer, mat_to_apply)) {
                ApplyRendererMaterials(item.renderer, mat_to_apply);
                if (!existing.custom_material) {
                    g_replaced[known].custom_material = mat_to_apply;
                    if (g_host && g_host->gchandle_new) {
                        g_replaced[known].custom_material_handle =
                            g_host->gchandle_new(g_host->context, mat_to_apply, 0);
                    }
                }
            }
            if (existing.hidden_by_mod) {
                bool enabled = false;
                if (GetRendererEnabled(item.renderer, enabled) &&
                    enabled != g_mask_visible) {
                    SetRendererEnabled(item.renderer, g_mask_visible);
                }
            }
            return false;
        }
    }

    ReplacedComponent source_copy;
    const ReplacedComponent* source = nullptr;
    if (existed && g_replaced[known].custom_mesh &&
        item.mesh == g_replaced[known].original_mesh) {
        source = &g_replaced[known];
    } else {
        source = AiCoverageFindReusableSource(item);
    }
    if (!source || (!source->custom_mesh && !source->hidden_by_mod)) return false;
    source_copy = *source;

    const BemComponent* component = ActiveComponent(source_copy.component_id);
    if (!component) return false;

    void* original_bones_to_restore = nullptr;
    void* original_root_bone_to_restore = nullptr;
    void* original_material_to_restore = nullptr;
    if (existed && g_replaced[known].original_bones) {
        original_bones_to_restore = g_replaced[known].original_bones;
        original_root_bone_to_restore = g_replaced[known].original_root_bone;
    }
    if (existed && g_replaced[known].original_material) {
        original_material_to_restore = g_replaced[known].original_material;
    } else {
        original_material_to_restore = Invoke(Contract("renderer.get_shared_materials"), item.renderer, nullptr, false);
    }

    if (source_copy.custom_mesh) {
        int effective_bones = item.bones_count;
        if (effective_bones <= static_cast<int>(component->info.max_bone)) {
            void* donor_root = nullptr;
            void* donor_bones = AiCoverageFindDonorBones(item, *component, donor_pool, &donor_root);
            if (!donor_bones) {
                return false;
            }
            const int donor_count = ArrayLength(donor_bones);
            if (donor_count <= static_cast<int>(component->info.max_bone)) {
                return false;
            }

            if (!original_bones_to_restore) {
                original_bones_to_restore = Invoke(Contract("skinned.get_bones"), item.renderer, nullptr, false);
                original_root_bone_to_restore = Invoke(Contract("skinned.get_root_bone"), item.renderer, nullptr, false);
            }

            void* bones_param[1]{donor_bones};
            Invoke(Contract("skinned.set_bones"), item.renderer, bones_param, true);
            if (donor_root) {
                void* root_param[1]{donor_root};
                Invoke(Contract("skinned.set_root_bone"), item.renderer, root_param, true);
            }

            effective_bones = donor_count;
            Log("[AI-COVERAGE] elevated bone palette on " + item.renderer_name +
                " from " + std::to_string(item.bones_count) + " to " + std::to_string(donor_count) +
                " bones (path=\"" + item.path + "\")");
        }

        const bool is_lod_variant =
            item.renderer_name.find("_lod") != std::string::npos ||
            item.path.find("/lod1/") != std::string::npos ||
            item.path.find("/lod2/") != std::string::npos ||
            item.path.find("/lod3/") != std::string::npos;

        if (!is_lod_variant && item.mesh != source_copy.custom_mesh &&
            !AiCoverageMeshesCompatible(item.mesh, source_copy.custom_mesh)) {
            if (original_bones_to_restore) {
                void* p[1]{original_bones_to_restore};
                Invoke(Contract("skinned.set_bones"), item.renderer, p, true);
                if (original_root_bone_to_restore) {
                    void* pr[1]{original_root_bone_to_restore};
                    Invoke(Contract("skinned.set_root_bone"), item.renderer, pr, true);
                }
            }
            return false;
        }

        void* bindpose_mesh = (source_copy.original_mesh &&
                               IsNativeObjectAlive(source_copy.original_mesh))
            ? source_copy.original_mesh
            : source_copy.custom_mesh;

        if (!ValidateRendererSkin(*component, item.renderer, bindpose_mesh)) {
            if (original_bones_to_restore) {
                void* p[1]{original_bones_to_restore};
                Invoke(Contract("skinned.set_bones"), item.renderer, p, true);
                if (original_root_bone_to_restore) {
                    void* pr[1]{original_root_bone_to_restore};
                    Invoke(Contract("skinned.set_root_bone"), item.renderer, pr, true);
                }
            }
            return false;
        }

        if (item.mesh != source_copy.custom_mesh &&
            (!SetSharedMesh(item.renderer, source_copy.custom_mesh) ||
             Invoke(Contract("skinned.get_shared_mesh"), item.renderer, nullptr,
                 false) != source_copy.custom_mesh)) {
            if (original_bones_to_restore) {
                void* p[1]{original_bones_to_restore};
                Invoke(Contract("skinned.set_bones"), item.renderer, p, true);
                if (original_root_bone_to_restore) {
                    void* pr[1]{original_root_bone_to_restore};
                    Invoke(Contract("skinned.set_root_bone"), item.renderer, pr, true);
                }
            }
            return false;
        }

        if (source_copy.custom_material) {
            ApplyRendererMaterials(item.renderer, source_copy.custom_material);
        }
    }

    void* original_for_record = item.mesh == source_copy.custom_mesh
        ? source_copy.original_mesh
        : item.mesh;
    if (!original_for_record || !IsNativeObjectAlive(original_for_record)) {
        return false;
    }

    ReplacedComponent record = source_copy;
    record.renderer = item.renderer;
    record.original_mesh = original_for_record;
    record.original_index_count = item.total_indices > 0
        ? static_cast<uint32_t>(item.total_indices)
        : source_copy.original_index_count;
    record.renderer_handle = 0;
    record.original_mesh_handle = 0;
    record.custom_mesh_handle = 0;
    record.original_material = original_material_to_restore;
    record.original_material_handle = 0;
    record.custom_material_handle = 0;
    record.original_bones = original_bones_to_restore;
    record.original_root_bone = original_root_bone_to_restore;
    record.original_bones_handle = 0;
    record.original_root_bone_handle = 0;
    GetRendererEnabled(item.renderer, record.original_enabled);
    PinReplacement(record);

    if (existed) {
        ReplacedComponent retired = g_replaced[known];
        retired.renderer = nullptr;
        g_replaced[known] = record;
        g_replaced.push_back(retired);
    } else {
        g_replaced.push_back(record);
    }

    if (record.hidden_by_mod) {
        bool enabled = false;
        if (GetRendererEnabled(item.renderer, enabled) &&
            enabled != g_mask_visible) {
            SetRendererEnabled(item.renderer, g_mask_visible);
        }
    }

    Log("[AI-COVERAGE] reused C" + std::to_string(record.component_id) +
        " renderer=" + AiCoveragePointer(item.renderer) +
        " (" + item.renderer_name + ")" +
        " sourceMesh=" + AiCoveragePointer(original_for_record) +
        " customMesh=" + AiCoveragePointer(record.custom_mesh) +
        " elevatedBones=" + std::to_string(original_bones_to_restore != nullptr) +
        " hidden=" + std::to_string(record.hidden_by_mod) +
        " path=\"" + item.path + "\" (no Mesh build)");
    return true;
}

bool AiCoverageStemHasCustomMesh(const std::string& stem) {
    if (stem.empty()) return false;
    for (const ReplacedComponent& record : g_replaced) {
        if (!record.custom_mesh || !record.original_mesh ||
            !IsNativeObjectAlive(record.custom_mesh) ||
            !IsNativeObjectAlive(record.original_mesh)) {
            continue;
        }
        if (AiCoverageComponentStem(ObjectName(record.original_mesh)) == stem)
            return true;
    }
    return false;
}

void AiCoverageApplyShadowState(
    const std::vector<void*>& normal_renderers,
    const std::vector<void*>& shadow_proxies) {
    if (!g_ai_coverage_set_shadow_mode_method) return;

    int casting_changed = 0;
    for (void* renderer : normal_renderers) {
        if (!renderer || !IsNativeObjectAlive(renderer)) continue;
        void* mesh = Invoke(Contract("skinned.get_shared_mesh"), renderer,
            nullptr, false);
        const ReplacedComponent* source = ReplacementSource(mesh);
        if (!source || !source->custom_mesh || mesh != source->custom_mesh)
            continue;
        int32_t mode = -1;
        if (!AiCoverageGetShadowMode(renderer, mode)) continue;
        if (mode != kShadowCastingOn) {
            AiCoverageRememberShadowMode(renderer, mode);
            if (AiCoverageSetShadowMode(renderer, kShadowCastingOn))
                ++casting_changed;
        }
    }

    int proxies_disabled = 0;
    for (void* renderer : shadow_proxies) {
        if (!renderer || !IsNativeObjectAlive(renderer)) continue;
        const std::string stem =
            AiCoverageComponentStem(ObjectName(renderer));
        if (!AiCoverageStemHasCustomMesh(stem)) continue;
        bool enabled = false;
        if (!GetRendererEnabled(renderer, enabled) || !enabled) continue;
        AiCoverageRememberProxy(renderer, enabled);
        if (SetRendererEnabled(renderer, false)) ++proxies_disabled;
    }

    if (casting_changed > 0 || proxies_disabled > 0) {
        Log("[AI-SHADOW] BEM shadow coverage updated: castingOn=" +
            std::to_string(casting_changed) + " proxiesDisabled=" +
            std::to_string(proxies_disabled));
    }
}

void PumpAiShadowCoverage() {
    PumpNpcLodLock();
    if (!g_replacement_active || g_refresh_suspended || g_replaced.empty()) {
        if (!g_ai_coverage_shadow_modes.empty() ||
            !g_ai_coverage_shadow_proxies.empty() ||
            !g_ai_coverage_hidden_vfxparts.empty()) {
            RestoreAiShadowCoverage();
        }
        return;
    }

    const uint64_t now = GetTickCount64();
    if (now < g_ai_coverage_next_scan_tick) return;
    g_ai_coverage_next_scan_tick = now + kAiCoverageScanIntervalMs;
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel))
        return;

    std::vector<void*> normal_renderers;
    std::vector<void*> shadow_proxies;
    std::vector<RendererSummary> candidate_items;

    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* objects = Invoke(Contract("resources.find_objects_of_type_all"),
        nullptr, parameters);
    const int count = std::max(ArrayLength(objects), 0);
    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (!renderer) continue;
        const std::string name = ObjectName(renderer);
        if (name.find(kActorMeshPrefix) != 0) continue;

        // 0. When custom model is active, hide unmatched vfxpart renderers (e.g. S_actor_endminf_vfxpart_01_lod0):
        if (!g_target_pelica && name.find("vfxpart") != std::string::npos) {
            bool has_custom_replacement = false;
            for (const auto& record : g_replaced) {
                if (record.custom_mesh && record.renderer == renderer) {
                    has_custom_replacement = true;
                    break;
                }
            }
            if (!has_custom_replacement) {
                bool enabled = false;
                if (GetRendererEnabled(renderer, enabled) && enabled) {
                    AiCoverageRememberHiddenVfxPart(renderer, enabled);
                    SetRendererEnabled(renderer, false);
                }
                continue;
            }
        }

        // Maintain hidden status for companion coat parts (cloth_02, cloth_03, cloth_05) across all LOD levels:
        if (name.find(kActorMeshPrefix) == 0 && name.find("shadowProxy") == std::string::npos) {
            const std::string stem = AiCoverageComponentStem(name);
            bool is_hidden_by_mod = false;
            for (const auto& record : g_replaced) {
                if (record.hidden_by_mod && record.original_mesh) {
                    if (AiCoverageComponentStem(ObjectName(record.original_mesh)) == stem) {
                        is_hidden_by_mod = true;
                        break;
                    }
                }
            }
            if (is_hidden_by_mod) {
                bool enabled = true;
                if (GetRendererEnabled(renderer, enabled) && enabled != g_mask_visible) {
                    SetRendererEnabled(renderer, g_mask_visible);
                }
            }
        }

        if (!IsActiveSceneRenderer(renderer)) continue;

        if (name.find("shadowProxy") != std::string::npos) {
            shadow_proxies.push_back(renderer);
            continue;
        }
        if (name.find(kActorMeshPrefix) != 0) continue;

        RendererSummary item = SummarizeRenderer(renderer);
        if (!item.renderer || !item.mesh ||
            item.path.find("SK_actor_female") != std::string::npos) {
            continue;
        }
        normal_renderers.push_back(renderer);
        candidate_items.push_back(std::move(item));
    }

    auto GetLodRank = [](const RendererSummary& s) -> int {
        if (s.renderer_name.find("_lod0") != std::string::npos ||
            s.path.find("/lod0/") != std::string::npos) return 0;
        if (s.renderer_name.find("_lod1") != std::string::npos ||
            s.path.find("/lod1/") != std::string::npos) return 1;
        if (s.renderer_name.find("_lod2") != std::string::npos ||
            s.path.find("/lod2/") != std::string::npos) return 2;
        if (s.renderer_name.find("_lod3") != std::string::npos ||
            s.path.find("/lod3/") != std::string::npos) return 3;
        if (s.renderer_name.find("_lod4") != std::string::npos ||
            s.path.find("/lod4/") != std::string::npos) return 4;
        return 0;
    };

    std::stable_sort(candidate_items.begin(), candidate_items.end(),
        [&](const RendererSummary& a, const RendererSummary& b) {
            return GetLodRank(a) < GetLodRank(b);
        });

    for (const auto& item : candidate_items) {
        AiCoverageAttachReuseOnly(item, candidate_items);
    }

    AiCoverageApplyShadowState(normal_renderers, shadow_proxies);
    g_operation_in_progress.store(false, std::memory_order_release);
}

void ResolveAiShadowSetter() {
    g_ai_coverage_set_shadow_mode_method = nullptr;
    if (!g_host || !g_host->resolve_method) return;

    const std::array<const char*, 2> parameter_types{
        "UnityEngine.Rendering.ShadowCastingMode", "System.Int32"};
    for (const char* parameter_type : parameter_types) {
        const BE_MethodDescriptorV1 descriptor{
            "UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "set_shadowCastingMode", parameter_type, "System.Void", 1};
        BE_ResolvedMethodV1 method{};
        if (g_host->resolve_method(g_host->context, &descriptor, &method) ==
                BE_Result_Ok && method.method_info) {
            g_ai_coverage_set_shadow_mode_method = method.method_info;
            Log(std::string("[AI-SHADOW] resolved Renderer.set_shadowCastingMode parameter=") +
                parameter_type);
            return;
        }
    }
    Log("[AI-SHADOW] Renderer.set_shadowCastingMode unavailable; shadow proxy experiment disabled, AI Mesh coverage remains active.");
}

void __fastcall DetourPumpWithAiShadowCoverage(void* method_info) {
    // Preserve the production pump and the GOOD/BAD diagnostic, but run scene
    // coverage between them so POST_APPLY/TAG dumps observe the final bindings.
    DetourPump(method_info);
    if (g_multi_character) {
        for (size_t i = 0; i < 2; ++i) {
            CharacterScope scope(i);
            SwapCoverage(i);
            if (g_resource_path) { PumpNpcLodLock(); if (!g_replacement_active) RestoreAiShadowCoverage(); }
            else PumpAiShadowCoverage();
            SwapCoverage(i);
        }
    } else { PumpAiShadowCoverage(); PumpF12StateDump(); }
}

BE_Result BE_CALL AiShadowCoverageCreateHook(
    void* context, const char* module_id, void* target, void* detour,
    void** original) {
    if (!g_ai_coverage_real_host || !g_ai_coverage_real_host->create_hook)
        return BE_Result_NotReady;
    if (detour == reinterpret_cast<void*>(&DetourPumpWithF12Dump)) {
        g_ai_coverage_pump_intercepted = true;
        return g_ai_coverage_real_host->create_hook(
            context, module_id, target,
            reinterpret_cast<void*>(&DetourPumpWithAiShadowCoverage), original);
    }
    return g_ai_coverage_real_host->create_hook(
        context, module_id, target, detour, original);
}

BE_Result BE_CALL InitializeWithAiShadowCoverage(const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;
    g_ai_coverage_real_host = host;
    g_ai_coverage_host_proxy = *host;
    g_ai_coverage_host_proxy.create_hook = &AiShadowCoverageCreateHook;
    g_ai_coverage_pump_intercepted = false;
    g_ai_coverage_next_scan_tick = 0;
    g_ai_coverage_shadow_modes.clear();
    g_ai_coverage_shadow_proxies.clear();
    g_ai_coverage_hidden_vfxparts.clear();

    const BE_Result result = InitializeWithF12Dump(&g_ai_coverage_host_proxy);
    if (result != BE_Result_Ok) {
        g_ai_coverage_real_host = nullptr;
        g_ai_coverage_host_proxy = {};
        return result;
    }
    if (!g_ai_coverage_pump_intercepted) {
        Log("[AI-COVERAGE] failed to intercept F12 diagnostic pump; coverage disabled.");
        ShutdownWithF12Dump();
        g_ai_coverage_real_host = nullptr;
        g_ai_coverage_host_proxy = {};
        return BE_Result_Failed;
    }

    ResolveAiShadowSetter();
    ResolveNpcLodLockBindings(host);
    InstallNpcLodLockHooks(host);
    Log("[AI-COVERAGE] active: control-switch/AI renderers reuse existing BEM Meshes only; this layer never builds a Mesh.");
    Log("[AI-SHADOW] active when setter is available: BEM renderers cast, matching shadowProxy renderers are disabled and restored on rollback.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithAiShadowCoverage() {
    if (g_multi_character) {
        for (size_t i = 0; i < 2; ++i) {
            CharacterScope scope(i); SwapCoverage(i); RestoreAiShadowCoverage(); SwapCoverage(i);
        }
    } else RestoreAiShadowCoverage();
    // Shutdown will clear both ownership states; no early release while another is active.
    ResumeLockedCrowdLODs();
    g_ai_coverage_next_scan_tick = 0;
    g_ai_coverage_set_shadow_mode_method = nullptr;

    ShutdownWithF12Dump();
    g_ai_coverage_real_host = nullptr;
    g_ai_coverage_host_proxy = {};
}
