#pragma once
// One EIEM DirectVmd instance ("slot") per danced character. EIEM keeps all of
// its state in file-level statics, so eiem_slot.inc is compiled once per slot
// inside its own namespace (eiem_slot0.cpp .. eiem_slot3.cpp): every slot has
// its own ghost rig, sampling worker, clip and caches. Hooks are installed once
// by eiem_body.cpp, which forwards each callback to the slot(s) it concerns.
#include "eiem_body.h"

#include <string>

namespace BetterEndfield::EiemSlot {

// Code addresses (MethodInfo::methodPointer) of the methods eiem_body.cpp hooks.
struct HookTargets {
    void* solver_manager_late_update = nullptr;
    void* biped_update_solver = nullptr;
    void* trig_on_update = nullptr;
    void* grounder_on_solver_update = nullptr;      // optional (terrain)
    void* grounder_on_post_solver_update = nullptr; // optional (terrain)
    void* cloth_set_weight = nullptr; // optional: BeyondBoneCloth.SetClothSimulateWeight
};
struct FaceTargets {
    void* smc_update = nullptr;
    void* morph_job = nullptr;
    void* special_job = nullptr;
};
// Host trampolines; slots that call "the original" internally use these.
struct Originals {
    void* grounder_on_solver_update = nullptr;
    void* grounder_on_post_solver_update = nullptr;
    void* trig_on_update = nullptr;
    void* smc_update = nullptr;
    void* morph_job = nullptr;
    void* special_job = nullptr;
};

struct Api {
    // Resolves IL2CPP metadata and starts the slot's worker. `targets` may be null.
    bool (*initialize)(const BE_HostApiV1* host, const std::wstring& log_path, HookTargets* targets);
    // Resolves SkeletalMorphCore by name; true when every field EIEM reads exists.
    bool (*prepare_face)(FaceTargets* targets);
    void (*set_originals)(const Originals& originals);
    void (*set_options)(const EiemBody::Options& options);
    void (*set_shared_anchor)(bool valid, const float position[3], const float rotation[4]);
    void (*load)(const std::string& motion_utf8, const std::string& face_utf8);
    EiemBody::LoadState (*load_status)();
    double (*duration_seconds)();
    void (*set_target)(void* entity, void* animator, void* movement);
    bool (*start)(const char* reason);
    void (*stop)(const char* reason);
    bool (*active)();
    bool (*busy)(); // ghost rig requested, alive or pending cleanup
    void (*publish_clock)(double seconds, bool playing);
    void (*shutdown)();
    // Hook callbacks (Unity main thread).
    void (*solver_manager_late_update)(void* self);
    void (*before_final_ik)(void* bipedIK);
    bool (*suppress_final_ik)(void* bipedIK);
    void (*after_final_ik)(void* bipedIK);
    void (*before_leg)(void* solver, void* methodInfo);
    void (*after_leg)(void* solver);
    bool (*grounder_active)(void* grounder);
    void (*grounder_on_solver_update)(void* self, void* methodInfo);
    void (*grounder_on_post_solver_update)(void* self, void* methodInfo);
    // Face: owns_smc may call Unity (main thread); claims_smc is a pointer check.
    bool (*owns_smc)(void* smc);
    bool (*claims_smc)(void* smc);
    void (*smc_update)(void* self, float deltaTime, void* methodInfo);
    void (*morph_job)(void* self, void* smc, void* data, void* methodInfo);
    void (*special_job)(void* self, void* smc, void* data, void* methodInfo);
    bool (*camera_reference)(EiemBody::CameraReference* reference);
    // BeyondBoneCloth.SetClothSimulateWeight: EIEM's guard against game writers
    // lowering the playback weight. prepare claims the call when the component
    // is this slot's (and may change *forwarded); readback follows the original.
    bool (*cloth_weight_prepare)(void* cloth, uintptr_t caller, float requested, float* forwarded);
    void (*cloth_weight_readback)(void* cloth, uintptr_t caller, float requested, float forwarded);
    bool (*terrain_available)();
    const char* (*terrain_unavailable_reason)();
};

const Api* Slot0();
const Api* Slot1();
const Api* Slot2();
const Api* Slot3();

} // namespace BetterEndfield::EiemSlot
