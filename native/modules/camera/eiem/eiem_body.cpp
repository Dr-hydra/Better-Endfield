// Hook dispatcher and public API for the EIEM DirectVmd port (see eiem_slot.h).
//
// Each hooked method is installed once through the Host. Every callback is
// offered to the slots that are up; a slot acts only on the objects of the
// character it drives (EIEM checks ownership itself), so one BipedIK solve is
// wrapped by exactly the slot that owns it and all others pass through.
#include <windows.h>
#include <intrin.h>

#include <atomic>
#include <cmath>
#include <shared_mutex>
#include <string>
#include <unordered_map>

#include "eiem_slot.h"

namespace BetterEndfield::EiemBody {
namespace {
using EiemSlot::Api;

const BE_HostApiV1* g_host = nullptr;
constexpr char kModuleId[] = "betterendfield.camera";
bool g_initialized = false;
bool g_available = false;
std::wstring g_log_path;
Options g_options;
const Api* g_slots[kMaxActors]{};
bool g_tried[kMaxActors]{};
std::atomic<bool> g_ready[kMaxActors]{};
EiemSlot::Originals g_originals;
bool g_face_tried = false, g_face_hooked = false;

// SkeletalMorphCore -> actor (-1: none). Filled on the main thread by
// SkeletalMorphCore.Update; the morph jobs only read it.
std::shared_mutex g_smc_mutex;
std::unordered_map<void*, int> g_smc_owner;

void HostLog(const std::string& message) {
    if (g_host && g_host->log) g_host->log(g_host->context, kModuleId, ("EIEM body: " + message).c_str());
}

bool CreateHook(void* target, const char* label, void* detour, void** original) {
    if (!target) {
        HostLog(std::string("hook target missing: ") + label);
        return false;
    }
    if (g_host->create_hook(g_host->context, kModuleId, target, detour, original) != BE_Result_Ok) {
        HostLog(std::string("hook refused by Host: ") + label);
        return false;
    }
    return true;
}

template <class F> void ForEachReady(F&& f) {
    for (int i = 0; i < kMaxActors; ++i)
        if (g_ready[i].load(std::memory_order_acquire)) f(*g_slots[i]);
}

void ResetSmcOwners() {
    std::unique_lock lock(g_smc_mutex);
    g_smc_owner.clear();
}

// --- detours -----------------------------------------------------------------
void* g_orig_late_update = nullptr;
void* g_orig_update_solver = nullptr;
void* g_orig_cloth_weight = nullptr;

using VoidFn = void(__fastcall*)(void*, void*);
using SmcUpdateFn = void(__fastcall*)(void*, float, void*);
using JobFn = void(__fastcall*)(void*, void*, void*, void*);
using ClothWeightFn = void(__fastcall*)(void*, float, void*);

void __fastcall SolverManagerLateUpdate(void* self, void* method) {
    ForEachReady([&](const Api& slot) { slot.solver_manager_late_update(self); });
    if (g_orig_late_update) reinterpret_cast<VoidFn>(g_orig_late_update)(self, method);
}

void __fastcall BipedUpdateSolver(void* self, void* method) {
    ForEachReady([&](const Api& slot) { slot.before_final_ik(self); });
    bool suppress = false;
    ForEachReady([&](const Api& slot) { suppress = suppress || slot.suppress_final_ik(self); });
    if (suppress) return;
    if (g_orig_update_solver) reinterpret_cast<VoidFn>(g_orig_update_solver)(self, method);
    ForEachReady([&](const Api& slot) { slot.after_final_ik(self); });
}

// EIEM's Hooked_OnUpdate: SEH covers the pre-solve and the original solve.
void GuardedLegSolve(void* self, void* method) {
    __try {
        for (int i = 0; i < kMaxActors; ++i)
            if (g_ready[i].load(std::memory_order_acquire)) g_slots[i]->before_leg(self, method);
        if (g_originals.trig_on_update) reinterpret_cast<VoidFn>(g_originals.trig_on_update)(self, method);
    } __except (EXCEPTION_EXECUTE_HANDLER) {
    }
}

void __fastcall TrigOnUpdate(void* self, void* method) {
    GuardedLegSolve(self, method);
    ForEachReady([&](const Api& slot) { slot.after_leg(self); });
}

void __fastcall GrounderOnSolverUpdate(void* self, void* method) {
    for (int i = 0; i < kMaxActors; ++i) {
        if (g_ready[i].load(std::memory_order_acquire) && g_slots[i]->grounder_active(self)) {
            g_slots[i]->grounder_on_solver_update(self, method);
            return;
        }
    }
    reinterpret_cast<VoidFn>(g_originals.grounder_on_solver_update)(self, method);
}

void __fastcall GrounderOnPostSolverUpdate(void* self, void* method) {
    for (int i = 0; i < kMaxActors; ++i) {
        if (g_ready[i].load(std::memory_order_acquire) && g_slots[i]->grounder_active(self)) {
            g_slots[i]->grounder_on_post_solver_update(self, method);
            return;
        }
    }
    reinterpret_cast<VoidFn>(g_originals.grounder_on_post_solver_update)(self, method);
}

int ClaimingActor(void* smc) {
    for (int i = 0; i < kMaxActors; ++i)
        if (g_ready[i].load(std::memory_order_acquire) && g_slots[i]->claims_smc(smc)) return i;
    std::shared_lock lock(g_smc_mutex);
    const auto it = g_smc_owner.find(smc);
    return it == g_smc_owner.end() ? -1 : it->second;
}

void __fastcall SmcUpdate(void* self, float delta, void* method) {
    int actor = -1;
    bool known = false;
    for (int i = 0; i < kMaxActors && actor < 0; ++i)
        if (g_ready[i].load(std::memory_order_acquire) && g_slots[i]->claims_smc(self)) actor = i;
    if (actor < 0) {
        std::shared_lock lock(g_smc_mutex);
        const auto it = g_smc_owner.find(self);
        if (it != g_smc_owner.end()) {
            known = true;
            actor = it->second;
        }
    }
    if (actor < 0 && !known) {
        for (int i = 0; i < kMaxActors && actor < 0; ++i)
            if (g_ready[i].load(std::memory_order_acquire) && g_slots[i]->owns_smc(self)) actor = i;
        std::unique_lock lock(g_smc_mutex);
        if (g_smc_owner.size() > 4096) g_smc_owner.clear();
        g_smc_owner[self] = actor;
    }
    if (actor >= 0) g_slots[actor]->smc_update(self, delta, method);
    else reinterpret_cast<SmcUpdateFn>(g_originals.smc_update)(self, delta, method);
}

// The slot running a component's playback service claims its weight writes;
// _ReturnAddress is the game's caller, which EIEM's guard keys on.
__declspec(noinline) void __fastcall ClothSetWeight(void* self, float requested, void* method) {
    const uintptr_t caller = reinterpret_cast<uintptr_t>(_ReturnAddress());
    float forwarded = requested;
    int claimed = -1;
    for (int i = 0; i < kMaxActors && claimed < 0; ++i)
        if (g_ready[i].load(std::memory_order_acquire) &&
            g_slots[i]->cloth_weight_prepare(self, caller, requested, &forwarded))
            claimed = i;
    reinterpret_cast<ClothWeightFn>(g_orig_cloth_weight)(self, forwarded, method);
    if (claimed >= 0) g_slots[claimed]->cloth_weight_readback(self, caller, requested, forwarded);
}

void __fastcall MorphJob(void* self, void* smc, void* data, void* method) {
    const int actor = ClaimingActor(smc);
    if (actor >= 0) g_slots[actor]->morph_job(self, smc, data, method);
    else reinterpret_cast<JobFn>(g_originals.morph_job)(self, smc, data, method);
}

void __fastcall SpecialJob(void* self, void* smc, void* data, void* method) {
    const int actor = ClaimingActor(smc);
    if (actor >= 0) g_slots[actor]->special_job(self, smc, data, method);
    else reinterpret_cast<JobFn>(g_originals.special_job)(self, smc, data, method);
}

// --- setup ---------------------------------------------------------------------
std::wstring LogPath(int actor) {
    if (g_log_path.empty() || actor == 0) return g_log_path;
    std::wstring path = g_log_path;
    const size_t dot = path.rfind(L'.');
    const std::wstring suffix = L"." + std::to_wstring(actor + 1);
    return dot == std::wstring::npos ? path + suffix : path.insert(dot, suffix);
}

// What slots call as "the original". Grounder support needs both callbacks.
EiemSlot::Originals SlotOriginals() {
    EiemSlot::Originals originals = g_originals;
    if (!originals.grounder_on_solver_update || !originals.grounder_on_post_solver_update)
        originals.grounder_on_solver_update = originals.grounder_on_post_solver_update = nullptr;
    return originals;
}

void InstallFaceHooks() {
    if (g_face_tried || !g_available) return;
    g_face_tried = true;
    EiemSlot::FaceTargets targets;
    if (!g_slots[0]->prepare_face(&targets)) return;
    void* update = nullptr;
    void* job = nullptr;
    void* special = nullptr;
    const bool ok = CreateHook(targets.smc_update, "SkeletalMorphCore.Update", reinterpret_cast<void*>(&SmcUpdate), &update) &&
        CreateHook(targets.morph_job, "DoEvaluateMorphToBoneJob", reinterpret_cast<void*>(&MorphJob), &job) &&
        CreateHook(targets.special_job, "DoEvaluateSpecialMorphToBoneJob", reinterpret_cast<void*>(&SpecialJob), &special);
    // Trampolines are published before any slot can claim an SMC.
    g_originals.smc_update = update;
    g_originals.morph_job = job;
    g_originals.special_job = special;
    ForEachReady([](const Api& slot) { slot.set_originals(SlotOriginals()); });
    if (!ok) {
        HostLog("VMD facial morphs unavailable (SkeletalMorphCore hooks incomplete)");
        return;
    }
    g_face_hooked = true;
    for (int i = 1; i < kMaxActors; ++i)
        if (g_ready[i].load(std::memory_order_acquire)) g_slots[i]->prepare_face(nullptr);
}

bool ValidActor(int actor) {
    return actor >= 0 && actor < kMaxActors && g_ready[actor].load(std::memory_order_acquire);
}
} // namespace

bool Initialize(const BE_HostApiV1* host, const std::wstring& log_path) {
    if (g_initialized) return g_available;
    g_initialized = true;
    g_host = host;
    g_log_path = log_path;
    g_slots[0] = EiemSlot::Slot0();
    g_slots[1] = EiemSlot::Slot1();
    g_slots[2] = EiemSlot::Slot2();
    g_slots[3] = EiemSlot::Slot3();
    if (!host || !host->create_hook) return false;
    EiemSlot::HookTargets targets;
    g_tried[0] = true;
    if (!g_slots[0]->initialize(host, LogPath(0), &targets)) {
        HostLog("DirectVmd unavailable; character motion disabled");
        return false;
    }
    const bool required =
        CreateHook(targets.solver_manager_late_update, "SolverManager.LateUpdate",
                   reinterpret_cast<void*>(&SolverManagerLateUpdate), &g_orig_late_update) &&
        CreateHook(targets.biped_update_solver, "BipedIK.UpdateSolver",
                   reinterpret_cast<void*>(&BipedUpdateSolver), &g_orig_update_solver) &&
        CreateHook(targets.trig_on_update, "IKSolverTrigonometric.OnUpdate",
                   reinterpret_cast<void*>(&TrigOnUpdate), &g_originals.trig_on_update);
    // Grounder callbacks feed terrain follow only; their absence disables it.
    void* grounder = nullptr;
    void* post = nullptr;
    if (required &&
        CreateHook(targets.grounder_on_solver_update, "GrounderBipedIK.OnSolverUpdate",
                   reinterpret_cast<void*>(&GrounderOnSolverUpdate), &grounder) &&
        CreateHook(targets.grounder_on_post_solver_update, "GrounderBipedIK.OnPostSolverUpdate",
                   reinterpret_cast<void*>(&GrounderOnPostSolverUpdate), &post)) {
        g_originals.grounder_on_solver_update = grounder;
        g_originals.grounder_on_post_solver_update = post;
    } else if (required) {
        HostLog("terrain follow unavailable (GrounderBipedIK hooks)");
        // A half-installed pair must still forward to the game.
        g_originals.grounder_on_solver_update = grounder;
        g_originals.grounder_on_post_solver_update = post;
    }
    if (!required) {
        HostLog("DirectVmd unavailable; FinalIK hooks could not be installed");
        return false;
    }
    // Only the weight guard needs it; the cloth playback service runs without.
    if (!CreateHook(targets.cloth_set_weight, "BeyondBoneCloth.SetClothSimulateWeight",
                    reinterpret_cast<void*>(&ClothSetWeight), &g_orig_cloth_weight))
        HostLog("cloth weight guard unavailable");
    g_slots[0]->set_originals(SlotOriginals());
    g_slots[0]->set_options(g_options);
    g_ready[0].store(true, std::memory_order_release);
    g_available = true;
    HostLog("DirectVmd ready (diagnostics: BetterEndfield.EiemBody.log)");
    return true;
}

bool Available() { return g_available; }

bool EnsureActor(int actor) {
    if (!g_available || actor < 0 || actor >= kMaxActors) return false;
    if (g_ready[actor].load(std::memory_order_acquire)) return true;
    if (g_tried[actor]) return false;
    g_tried[actor] = true;
    const Api& slot = *g_slots[actor];
    if (!slot.initialize(g_host, LogPath(actor), nullptr)) {
        HostLog("DirectVmd actor " + std::to_string(actor + 1) + " unavailable");
        return false;
    }
    slot.set_originals(SlotOriginals());
    slot.set_options(g_options);
    if (g_face_hooked) slot.prepare_face(nullptr);
    g_ready[actor].store(true, std::memory_order_release);
    return true;
}

void SetOptions(const Options& options) {
    g_options = options;
    ForEachReady([&](const Api& slot) { slot.set_options(options); });
    if (options.face) InstallFaceHooks();
}

void Load(int actor, const std::string& motion_utf8, const std::string& face_utf8) {
    if (!EnsureActor(actor)) return;
    if (g_options.face) InstallFaceHooks();
    g_slots[actor]->load(motion_utf8, face_utf8);
}

LoadState LoadStatus(int actor) {
    return ValidActor(actor) ? g_slots[actor]->load_status() : LoadState::Failed;
}

double DurationSeconds(int actor) {
    return ValidActor(actor) ? g_slots[actor]->duration_seconds() : 0.0;
}

void SetTarget(int actor, void* entity, void* animator, void* movement) {
    if (!ValidActor(actor)) return;
    g_slots[actor]->set_target(entity, animator, movement);
    ResetSmcOwners();
}

void SetSharedAnchor(bool valid, const float position[3], const float rotation[4]) {
    ForEachReady([&](const Api& slot) { slot.set_shared_anchor(valid, position, rotation); });
}

bool Start(int actor, const char* reason) {
    if (!ValidActor(actor)) return false;
    ResetSmcOwners();
    return g_slots[actor]->start(reason);
}

void Stop(int actor, const char* reason) {
    if (ValidActor(actor)) g_slots[actor]->stop(reason);
}

bool Active(int actor) { return ValidActor(actor) && g_slots[actor]->active(); }

bool GetCameraReference(int actor, CameraReference& reference) {
    return ValidActor(actor) && g_slots[actor]->camera_reference(&reference);
}

void PublishClock(double seconds, bool playing) {
    ForEachReady([&](const Api& slot) { slot.publish_clock(seconds, playing); });
}

void Shutdown() {
    for (int i = 0; i < kMaxActors; ++i)
        if (g_slots[i] && g_tried[i]) g_slots[i]->shutdown();
}

} // namespace BetterEndfield::EiemBody
