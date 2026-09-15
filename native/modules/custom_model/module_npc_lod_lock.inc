#pragma once

#include <string>

// Production Native LOD0 & NPC Crowd Lock Implementation:
// Instead of hooking individual Unity LODGroup instances (which causes pointer instability
// and bone mismatch crashes when custom models only provide LOD0), this implementation
// uses HyperGryph's native pipeline-level switch:
//   HGRenderPipeline.EnableForceLOD0()
//   UnityEngine.QualitySettings.maximumLODLevel = 0
// and adjusts Beyond.NPC.Lod.NPCCrowdLODSetting static fields to eliminate NPC distance culling.

BE_ResolvedClassV1 g_npc_lod_setting_class{};
bool g_npc_lod_setting_resolved = false;
void* g_npc_lod_static_data = nullptr;

bool g_pipeline_lod0_locked = false;
bool g_npc_crowd_lod_locked = false;

float g_orig_visible_model_distance_x = 0.0f;
float g_orig_visible_model_distance_y = 0.0f;
bool g_orig_enable_camera_physic_cull = true;
int32_t g_orig_camera_cull_render_idx = 0;
int32_t g_orig_max_npc_render_num = 30;

using ClassGetStaticFieldDataFn = void*(*)(const void*);
ClassGetStaticFieldDataFn g_il2cpp_class_get_static_field_data = nullptr;

static bool RawWriteNpcLodFields(void* base, float dist_x, float dist_y, int32_t max_npc, int32_t cull_idx, bool cull_enable) {
    if (!base) return false;
    __try {
        char* ptr = reinterpret_cast<char*>(base);
        *reinterpret_cast<float*>(ptr + 0x0) = dist_x;
        *reinterpret_cast<float*>(ptr + 0x4) = dist_y;
        *reinterpret_cast<float*>(ptr + 0x8) = dist_x;
        *reinterpret_cast<float*>(ptr + 0xc) = dist_y;
        *reinterpret_cast<int32_t*>(ptr + 0x1c) = max_npc;
        *reinterpret_cast<int32_t*>(ptr + 0x34) = cull_idx;
        *reinterpret_cast<bool*>(ptr + 0x38) = cull_enable;
        return true;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

static bool RawReadNpcLodFields(void* base, float& dist_x, float& dist_y, int32_t& max_npc, int32_t& cull_idx, bool& cull_enable) {
    if (!base) return false;
    __try {
        char* ptr = reinterpret_cast<char*>(base);
        dist_x = *reinterpret_cast<float*>(ptr + 0x0);
        dist_y = *reinterpret_cast<float*>(ptr + 0x4);
        max_npc = *reinterpret_cast<int32_t*>(ptr + 0x1c);
        cull_idx = *reinterpret_cast<int32_t*>(ptr + 0x34);
        cull_enable = *reinterpret_cast<bool*>(ptr + 0x38);
        return true;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

static bool ResolveNpcLodLockBindings(const BE_HostApiV1* host) {
    if (!host) return false;

    HMODULE game_assembly = GetModuleHandleA("GameAssembly.dll");
    if (game_assembly) {
        g_il2cpp_class_get_static_field_data = reinterpret_cast<ClassGetStaticFieldDataFn>(
            GetProcAddress(game_assembly, "il2cpp_class_get_static_field_data"));
    }

    if (host->resolve_class) {
        if (host->resolve_class(host->context, "Gameplay.Beyond.dll",
                "Beyond.NPC.Lod", "NPCCrowdLODSetting", &g_npc_lod_setting_class) == BE_Result_Ok &&
            g_npc_lod_setting_class.class_info) {
            g_npc_lod_setting_resolved = true;
            if (g_il2cpp_class_get_static_field_data) {
                g_npc_lod_static_data = g_il2cpp_class_get_static_field_data(
                    g_npc_lod_setting_class.class_info);
            }
            Log("[NPC-LOD-LOCK] Resolved Beyond.NPC.Lod.NPCCrowdLODSetting class.");
        } else {
            Log("[NPC-LOD-LOCK] Warning: NPCCrowdLODSetting class not resolved.");
        }
    }
    return true;
}

static bool InstallNpcLodLockHooks(const BE_HostApiV1* host) {
    (void)host;
    Log("[NPC-LOD-LOCK] Native LOD pipeline control initialized.");
    return true;
}

void ApplyLockedCrowdLODs() {
    if (g_pipeline_lod0_locked && g_npc_crowd_lod_locked) return;

    // 1. Pipeline Level: HGRenderPipeline.EnableForceLOD0()
    void* pipeline = Invoke(Contract("pipeline.current"), nullptr, nullptr, false);
    if (pipeline) {
        Invoke(Contract("pipeline.enable_force_lod0"), pipeline, nullptr, true);
        int32_t max_lod = 0;
        void* max_lod_param[1]{&max_lod};
        Invoke(Contract("quality.set_max_lod"), nullptr, max_lod_param, true);
        g_pipeline_lod0_locked = true;
        Log("[NPC-LOD-LOCK] Pipeline EnableForceLOD0() applied, QualitySettings.maximumLODLevel=0.");
    }

    // 2. NPC Crowd Settings: modify static fields
    if (g_npc_lod_static_data && !g_npc_crowd_lod_locked) {
        if (RawReadNpcLodFields(g_npc_lod_static_data,
                g_orig_visible_model_distance_x, g_orig_visible_model_distance_y,
                g_orig_max_npc_render_num, g_orig_camera_cull_render_idx,
                g_orig_enable_camera_physic_cull)) {
            if (RawWriteNpcLodFields(g_npc_lod_static_data, 1000.0f, 1000.0f, 100, 0, false)) {
                g_npc_crowd_lod_locked = true;
                Log("[NPC-LOD-LOCK] NPCCrowdLODSetting overrides applied (distance=1000m, cull=false).");
            } else {
                Log("[NPC-LOD-LOCK] Failed to write NPCCrowdLODSetting static fields.");
            }
        }
    }
}

void ResumeLockedCrowdLODs() {
    if (!g_pipeline_lod0_locked && !g_npc_crowd_lod_locked) return;

    // 1. Pipeline Level: HGRenderPipeline.DisableForceLOD0()
    if (g_pipeline_lod0_locked) {
        void* pipeline = Invoke(Contract("pipeline.current"), nullptr, nullptr, false);
        if (pipeline) {
            Invoke(Contract("pipeline.disable_force_lod0"), pipeline, nullptr, true);
            Log("[NPC-LOD-LOCK] Pipeline DisableForceLOD0() restored.");
        }
        g_pipeline_lod0_locked = false;
    }

    // 2. Restore NPC Crowd Settings
    if (g_npc_crowd_lod_locked && g_npc_lod_static_data) {
        if (RawWriteNpcLodFields(g_npc_lod_static_data,
                g_orig_visible_model_distance_x, g_orig_visible_model_distance_y,
                g_orig_max_npc_render_num, g_orig_camera_cull_render_idx,
                g_orig_enable_camera_physic_cull)) {
            Log("[NPC-LOD-LOCK] NPCCrowdLODSetting static fields restored to original.");
        } else {
            Log("[NPC-LOD-LOCK] Failed to restore NPCCrowdLODSetting static fields.");
        }
        g_npc_crowd_lod_locked = false;
    }
}

void PumpNpcLodLock() {
    if (g_replacement_active) {
        if (!g_pipeline_lod0_locked || !g_npc_crowd_lod_locked) {
            ApplyLockedCrowdLODs();
        }
    } else {
        if (g_pipeline_lod0_locked || g_npc_crowd_lod_locked) {
            ResumeLockedCrowdLODs();
        }
    }
}
