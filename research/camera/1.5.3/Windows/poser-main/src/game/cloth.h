// Clothing enhancement adapted from Sasye/EIEM, AGPL-3.0.
// Upstream cloth fixes: 9e1145305f546720104beb8cc9a647c98ca18b7f (2026-09-28).
// Poser changes: lifecycle ownership, offline binding validation, UI/build integration.
#pragma once
#include "math/cloth_state.h"
#include "math/cloth_playback_start.h"
#include "core/game_hooks.h"
#include "game/skeleton.h"
#include <atomic>
#include <algorithm>
#include <cstring>
#include <memory>
#include <set>
#include <map>
#include "cloth/core/cloth_writer_state.h"
#include "cloth/core/cloth_actor_context.h"
#include "cloth/core/cloth_metadata_cache.h"
#include <cmath>

// Host accesses are serialized with g_poseMutex. Unity work is main-thread only.
static DWORD (*s_clothThreadId)() = nullptr;
struct ClothActorRequest {
  eiem_playback::StartGate gate;
  const char *preparationReason="idle";
  bool requested=false,allowAnchorCapture=false;
  uint64_t generation=0;
  float height=1.245f;
  void *entity=nullptr,*animator=nullptr;
};
static ClothActorBank<ClothActorRequest> s_ClothActorRequest;
#define g_clothPlaybackGate (s_ClothActorRequest.Get().gate)
static void ClothPlaybackService(bool completedBoundary);
static bool ClothUnderAnimator(void *transform,void *animator);
static bool (*s_clothHostIdle)() = nullptr;
static bool (*s_clothHostEnabled)() = nullptr;
static bool ClothHostEnabled() { return !RuntimeClosing() && (!s_clothHostEnabled || s_clothHostEnabled()); }
static bool ClothHostIdle() { return ClothHostEnabled() && s_clothHostIdle && s_clothHostIdle(); }
#define s_clothRequested (s_ClothActorRequest.Get().requested)
#define s_clothAllowAnchorCapture (s_ClothActorRequest.Get().allowAnchorCapture)
#define s_clothRequestGeneration (s_ClothActorRequest.Get().generation)
#define s_clothCharacterHeight (s_ClothActorRequest.Get().height)
static void *ClothHostEntity() { return s_clothActorIndex ? s_ClothActorRequest.Get().entity : (g_mainCharEntity ? g_mainCharEntity : g_charAnimator); }
static void *ClothHostAnimator() { return s_clothActorIndex ? s_ClothActorRequest.Get().animator : g_charAnimator; }
static bool ClothUnboxBool(void *boxed) {
  if (!boxed) return false;
  __try { return *reinterpret_cast<unsigned char *>((char *)boxed + 16) != 0; }
  __except (EXCEPTION_EXECUTE_HANDLER) { return false; }
}
static void *g_transformClass = nullptr;

static std::atomic<float> s_skirtHipRadiusDelta{0.124f};
static std::atomic<bool> s_skirtDirty{true};
static std::atomic<int> s_collisionGeometry{1};
static std::atomic<float> s_clothRibbonDamping{.3f};
// Detailed particle/input traces are opt-in, not part of normal playback.
static std::atomic<bool> s_clothVerboseDiagnostics{false};
static std::atomic<uint64_t> s_clothInvalidation{1};
using ClothFreeNameFn = void (*)(void *);
using ClothMethodFlagsFn = uint32_t (*)(void *, uint32_t *);
static ClothFreeNameFn s_clothFreeName = nullptr;
static ClothMethodFlagsFn s_clothMethodFlags = nullptr;

static bool ClothOnNativeThread() {
  DWORD tid = s_clothThreadId ? s_clothThreadId() : 0;
  return tid != 0 && tid == GetCurrentThreadId();
}
static bool ClothOnMainThread() {
  return !RuntimeClosing() && ClothOnNativeThread();
}
static void ClothRequestInvalidation() {
  s_clothInvalidation.fetch_add(1, std::memory_order_acq_rel);
}
static bool ClothTypeIs(void *type, const char *expected) {
  if (!s_clothFreeName)
    s_clothFreeName = reinterpret_cast<ClothFreeNameFn>(GetProcAddress(hGA, "il2cpp_free"));
  if (!type || !il2cpp_type_get_name || !s_clothFreeName) return false;
  const char *name = il2cpp_type_get_name(type);
  bool same = name && strcmp(name, expected) == 0;
  if (name) s_clothFreeName(const_cast<char *>(name));
  return same;
}
static void *ClothMethod(void *cls, const char *name, const char *ret,
                         const char *arg = nullptr, bool isStatic = false) {
  if (!s_clothMethodFlags)
    s_clothMethodFlags = reinterpret_cast<ClothMethodFlagsFn>(GetProcAddress(hGA, "il2cpp_method_get_flags"));
  if (!s_clothMethodFlags || !il2cpp_method_get_return_type || !il2cpp_method_get_param ||
      !il2cpp_class_get_parent) return nullptr;
  using namespace poser_cloth_metadata;
  const auto key=Lookup(cls,Kind::Method,name,ret,arg,nullptr,isStatic);
  if(auto hit=cache.Find(key)) return reinterpret_cast<void *>(hit);
  for (int depth = 0; cls && depth < 16; ++depth, cls = il2cpp_class_get_parent(cls)) {
    void *it = nullptr;
    while (void *m = il2cpp_class_get_methods(cls, &it)) {
      const char *n = il2cpp_method_get_name(m);
      uint32_t implementationFlags = 0;
      if (!n || strcmp(n, name) ||
          il2cpp_method_get_param_count(m) != (arg ? 1u : 0u) ||
          ((s_clothMethodFlags(m, &implementationFlags) & 0x10) != 0) != isStatic ||
          !ClothTypeIs(il2cpp_method_get_return_type(m), ret)) continue;
      if (!arg || ClothTypeIs(il2cpp_method_get_param(m, 0), arg))
        return reinterpret_cast<void *>(cache.Remember(key,uintptr_t(m)));
    }
  }
  return nullptr;
}
static int ClothFieldOffset(void *cls, const char *name, const char *type) {
  if (!il2cpp_field_get_type || !il2cpp_class_get_parent) return -1;
  using namespace poser_cloth_metadata;
  const auto key=Lookup(cls,Kind::FieldOffset,name,type);
  if(auto hit=cache.Find(key)) return int(hit);
  for (int depth = 0; cls && depth < 16; ++depth, cls = il2cpp_class_get_parent(cls)) {
    void *it = nullptr;
    while (void *f = il2cpp_class_get_fields(cls, &it)) {
      const char *n = il2cpp_field_get_name(f);
      if (n && !strcmp(n, name) && ClothTypeIs(il2cpp_field_get_type(f), type)) {
        size_t off = il2cpp_field_get_offset(f);
        return off >= 16 && off < 65536 ? int(cache.Remember(key,off)) : -1;
      }
    }
  }
  return -1;
}
static bool ClothInvoke(void *method, void *self, void **args, void *&result) {
  result = nullptr;
  if (RuntimeClosing() || !method || !il2cpp_runtime_invoke) return false;
  __try {
    void *exception = nullptr;
    result = il2cpp_runtime_invoke(method, self, args, &exception);
    if (exception) {
      void *cls = il2cpp_object_get_class(exception);
      Log("[CLOTH-EXCEPTION] method=%s exception=%p type=%s tid=%lu",
          il2cpp_method_get_name(method), exception,
          cls ? il2cpp_class_get_name(cls) : "?", GetCurrentThreadId());
    }
    return exception == nullptr;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    Log("[CLOTH-EXCEPTION] method=%p nativeException=1 tid=%lu", method, GetCurrentThreadId());
    return false;
  }
}
template <class T> static bool ClothValue(void *method, void *self, T &value) {
  void *boxed = nullptr;
  if (!ClothInvoke(method, self, nullptr, boxed) || !boxed) return false;
  __try { memcpy(&value, (char *)boxed + 16, sizeof(T)); return true; }
  __except (EXCEPTION_EXECUTE_HANDLER) { return false; }
}
template <class T> static bool ClothField(void *obj, const char *name,
                                         const char *type, T &value) {
  if (!obj) return false;
  __try {
    int off = ClothFieldOffset(il2cpp_object_get_class(obj), name, type);
    if (off < 0) return false;
    memcpy(&value, (char *)obj + off, sizeof(T));
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) { return false; }
}
struct ClothUnityApi {
  void *alive = nullptr, *instance = nullptr, *components = nullptr;
  void *frame = nullptr, *globalTime = nullptr, *active = nullptr;
  void *getEnabled = nullptr, *setEnabled = nullptr, *scene = nullptr;
  void *getGO = nullptr, *getTransform = nullptr, *childCount = nullptr, *child = nullptr;
  void *name = nullptr;
} static s_clothUnity;

static bool ClothResolveUnity() {
  if (s_clothUnity.frame) return true;
  if (!g_transformClass) { size_t n=0; auto a=il2cpp_domain_get_assemblies(il2cpp_domain_get(), &n); g_transformClass=FindClass("UnityEngine", "Transform", a, n); }
  if (!g_componentClass || !g_gameObjectClass || !g_transformClass ||
      !il2cpp_gchandle_new || !il2cpp_gchandle_get_target || !il2cpp_gchandle_free)
    return false;
  size_t count = 0;
  void **asms = il2cpp_domain_get_assemblies(il2cpp_domain_get(), &count);
  void *object = FindClass("UnityEngine", "Object", asms, count);
  void *time = FindClass("UnityEngine", "Time", asms, count);
  ClothUnityApi u{};
  u.alive = ClothMethod(object, "op_Implicit", "System.Boolean", "UnityEngine.Object", true);
  u.instance = ClothMethod(object, "GetInstanceID", "System.Int32");
  u.name = ClothMethod(object, "get_name", "System.String");
  u.components = ClothMethod(g_gameObjectClass, "GetComponents", "UnityEngine.Component[]", "System.Type");
  u.active = ClothMethod(g_gameObjectClass, "get_activeInHierarchy", "System.Boolean");
  u.scene = ClothMethod(g_gameObjectClass, "get_scene", "UnityEngine.SceneManagement.Scene");
  u.getEnabled = ClothMethod(g_animatorClass, "get_enabled", "System.Boolean");
  u.setEnabled = ClothMethod(g_animatorClass, "set_enabled", "System.Void", "System.Boolean");
  u.getGO = ClothMethod(g_componentClass, "get_gameObject", "UnityEngine.GameObject");
  u.getTransform = ClothMethod(g_componentClass, "get_transform", "UnityEngine.Transform");
  u.childCount = ClothMethod(g_transformClass, "get_childCount", "System.Int32");
  u.child = ClothMethod(g_transformClass, "GetChild", "UnityEngine.Transform", "System.Int32");
  u.frame = ClothMethod(time, "get_frameCount", "System.Int32", nullptr, true);
  u.globalTime = ClothMethod(time, "get_timeScale", "System.Single", nullptr, true);
  bool ok = u.alive && u.instance && u.name && u.components && u.active &&
      u.scene && u.getEnabled && u.setEnabled && u.getGO && u.getTransform &&
      u.childCount && u.child && u.frame && u.globalTime;
  Log("[CLOTH-ABI] unityComplete=%d frame=%p alive=%p components=%p scene=%p", ok, u.frame, u.alive, u.components, u.scene);
  if (ok) s_clothUnity = u;
  return ok;
}
static bool ClothAlive(void *object) {
  void *boxed = nullptr;
  void *args[] = {object};
  return object && ClothInvoke(s_clothUnity.alive, nullptr, args, boxed) && boxed && ClothUnboxBool(boxed);
}
struct ClothRef {
  uint32_t handle = 0;
  eiem_cloth::ObjectId id{};
};
enum class ClothLife { Alive, Destroyed, Unreadable };
static ClothLife ClothInspect(const ClothRef &r, void *&obj) {
  obj = r.handle ? il2cpp_gchandle_get_target(r.handle) : nullptr;
  if (!obj) return ClothLife::Destroyed;
  void *boxed = nullptr, *args[] = {obj};
  if (!ClothInvoke(s_clothUnity.alive, nullptr, args, boxed) || !boxed)
    return ClothLife::Unreadable;
  if (!ClothUnboxBool(boxed)) return ClothLife::Destroyed;
  int id = 0;
  if (!ClothValue(s_clothUnity.instance, obj, id)) return ClothLife::Unreadable;
  return id == r.id.instance ? ClothLife::Alive : ClothLife::Destroyed;
}
static ClothRef ClothProtect(void *object) {
  ClothRef r{};
  int id = 0;
  if (ClothAlive(object) && ClothValue(s_clothUnity.instance, object, id) && id) {
    r.handle = il2cpp_gchandle_new(object, false);
    if (r.handle) r.id = {reinterpret_cast<uintptr_t>(object), id};
  }
  return r;
}
static void *ClothTarget(const ClothRef &r) {
  void *obj = nullptr;
  return ClothInspect(r, obj) == ClothLife::Alive ? obj : nullptr;
}
static void ClothFree(ClothRef &r) {
  if (r.handle) il2cpp_gchandle_free(r.handle);
  r = {};
}
struct ClothApi {
  void *process = nullptr, *serialize = nullptr, *build = nullptr, *skip = nullptr;
  void *reset = nullptr, *softReset = nullptr, *weight = nullptr, *ratio = nullptr;
  void *timeGet = nullptr, *timeSet = nullptr, *changed = nullptr;
};
static ClothApi ClothResolve(void *object) {
  ClothApi a{};
  void *cls = il2cpp_object_get_class(object);
  a.process = ClothMethod(cls, "get_Process", "BeyondDynamicBone.ClothProcess");
  a.serialize = ClothMethod(cls, "get_SerializeData", "BeyondDynamicBone.ClothSerializeData");
  a.build = ClothMethod(cls, "BuildAndRun", "System.Boolean");
  a.skip = ClothMethod(cls, "SetSkipWriting", "System.Void", "System.Boolean");
  a.reset = ClothMethod(cls, "ResetCloth", "System.Void", "System.Boolean");
  a.softReset = ClothMethod(cls, "SoftResetCloth", "System.Void", "System.Boolean");
  a.weight = ClothMethod(cls, "SetClothSimulateWeight", "System.Void", "System.Single");
  a.ratio = ClothMethod(cls, "SetAnimationPoseRatio", "System.Void", "System.Single");
  a.timeGet = ClothMethod(cls, "GetTimeScale", "System.Single");
  a.timeSet = ClothMethod(cls, "SetTimeScale", "System.Void", "System.Single");
  a.changed = ClothMethod(cls, "SetParameterChange", "System.Void");
  Log("[CLOTH-ABI] class=%s process=%p buildBool=%p resetBool=%p softResetBool=%p skipBool=%p weightFloat=%p ratioFloat=%p timeGet=%p timeSet=%p changed=%p numericPolicy=playback-weight-1-paired-log-evidence ratioPolicy=low-weight-takeover-initial-reference-0 timePolicy=observe-only resetPolicy=unused",
      il2cpp_class_get_name(cls), a.process, a.build, a.reset, a.softReset, a.skip,
      a.weight, a.ratio, a.timeGet, a.timeSet, a.changed);
  return a;
}
struct ClothReadback {
  eiem_cloth::Observation state{};
  const char *readFailure = "none";
  void *process = nullptr;
  void *serialize = nullptr;
  bool camera = false, distance = false, lod = false, keep = false;
  int team = -1;
  float time = NAN, globalTime = NAN, weight = NAN, ratio = NAN, blend = NAN;
  float propertyWeight = NAN, propertyRatio = NAN;
  uint32_t flags = 0;
  bool flagsKnown = false;
  int result = 0, warning = 0;
  bool resultKnown = false;
};
constexpr int ClothCapacity = 64;
constexpr size_t ClothColliderCapacity = 128;
struct ClothPoseProbe {
  ClothRef ref{};
  char name[96]{};
};
struct ClothInstance {
  ClothRef ref{};
  ClothApi api{};
  eiem_cloth::Startup startup{};
  ClothReadback last{};
  char name[96]{};
  uint32_t processHandle = 0;
  bool originalEnabled = false, capturedEnabled = false, changedEnabled = false;
  bool originalSkip = false, capturedSkip = false, changedSkip = false;
  uint32_t weightSerializeHandle = 0;
  float originalWeight = NAN, originalPropertyWeight = NAN;
  bool changedWeight = false;
  eiem_cloth::WeightWriterEvidence weightWriter{};
  void *writerProcessClass = nullptr;
  void *writerStateMethods[7]{};
  float originalRatio = NAN, originalPropertyRatio = NAN;
  bool capturedRatio = false, changedRatio = false;
  bool poseProbesAttempted = false, poseSubmissionLogged = false;
  int poseProbeCount = 0;
  ClothPoseProbe poseProbes[8]{};
  bool anchorsCaptured = false, anchorsPolling = false;
  bool skirt = false;
  uint64_t nextPoll = 0, colliderDeadline = 0, lastObservedAt = 0;
  bool wasSuspended = false;
  unsigned processChanges = 0;
};
struct ClothColliderOriginal {
  ClothRef ref{};
  Vector3 size{}, center{};
  Vector3 appliedSize{}, appliedCenter{};
  bool appliedSeparation = false;
  bool separation = false, changed = false, applied = false;
  int sizeOff = -1, centerOff = -1, separationOff = -1;
  void *setSize = nullptr, *update = nullptr;
};
using ClothBoneGuard = bool (*)(void *transform);
struct ClothAnchor {
  ClothRef ref{}, parent{};
  Vector3 originalPosition{}, bindPosition{};
  Quaternion originalRotation{}, bindRotation{};
  Vector3 observedPosition{NAN, NAN, NAN};
  Quaternion observedRotation{NAN, NAN, NAN, NAN};
  const char *bindReason = "not-read";
  uint64_t members = 0;
  char name[128]{}, parentName[128]{};
  bool bindKnown = false, changed = false, skipped = false, enhancementLeased = false;
  bool sent = false, confirmed = false;
  unsigned stableReads = 0, confirmReads = 0;
  int lastFrame = -1;
};
constexpr size_t ClothAnchorCapacity = 128;
constexpr int ClothDiscoveryDepthLimit = 64, ClothDiscoveryNodeLimit = 4096;
struct ClothDiscoveryReport {
  int nodes = 0, maxDepth = 0, depthCuts = 0, nodeCuts = 0;
  int readFailures = 0, capacityCuts = 0, pending = 0;
  bool complete = false;
  const char *firstFailure = "none";
};
struct ClothSession {
  eiem_cloth::Owner owner{};
  ClothRef animator{};
  ClothBoneGuard bodyGuard = nullptr;
  uint64_t invalidation = 0, nextDiscovery = 0;
  unsigned scans = 0;
  int scene = 0, lastFrame = -1, count = 0;
  bool active = false, releasing = false, failed = false;
  bool emptySourceReported = false;
  float appliedHip=NAN;
  int appliedGeometry=-1;
  const char *failureReason = "none";
  ClothDiscoveryReport discovery{};
  unsigned restoreAttempts = 0;
  uint64_t nextRestore = 0;
  ClothInstance instances[ClothCapacity]{};
  eiem_cloth::Originals<ClothColliderOriginal, ClothColliderCapacity> colliders{};
  eiem_cloth::Originals<ClothAnchor, ClothAnchorCapacity> anchors{};
};
static ClothActorBank<ClothSession> s_clothActors;
#define s_cloth (s_clothActors.Get())
static bool ClothActorEngaged(unsigned slot) {
  const auto &p=s_clothActors.values[slot];
  return p && (p->active || p->releasing);
}
static bool ClothSquadRestoring();
static uint64_t s_clothSessionSerial = 0;
struct ClothActorBegin {
  unsigned attempts=0;
  uint64_t next=0,generation=0,invalidation=0;
  uintptr_t character=0;
};
static ClothActorBank<ClothActorBegin> s_ClothActorBegin;
#define s_clothBeginAttempts (s_ClothActorBegin.Get().attempts)
#define s_clothNextBegin (s_ClothActorBegin.Get().next)
#define s_clothBeginGeneration (s_ClothActorBegin.Get().generation)
#define s_clothBeginInvalidation (s_ClothActorBegin.Get().invalidation)
#define s_clothBeginCharacter (s_ClothActorBegin.Get().character)

static bool ClothOwns(const eiem_cloth::Owner &work) {
  return eiem_cloth::Accepts(s_cloth.active, s_cloth.owner, work) &&
      s_clothRequested && work.generation == s_clothRequestGeneration &&
      work.backend == 1u &&
      work.character == reinterpret_cast<uintptr_t>(ClothHostEntity()) &&
      s_cloth.invalidation == s_clothInvalidation.load(std::memory_order_acquire);
}

static int ClothFrame() {
  int frame = -1;
  ClothValue(s_clothUnity.frame, nullptr, frame);
  return frame;
}
static void ClothLog(const char *event, const ClothInstance *i, const char *reason) {
  const ClothReadback r = i ? i->last : ClothReadback{};
  Log("[CLOTH-%s] backend=%s generation=%llu session=%llu owner=%p frame=%d ms=%llu tid=%lu name='%s' instance=%d component=%p process=%p valid=%d running=%d enabled=%d processEnabled=%d active=%d skip=%d camera=%d distance=%d lod=%d keep=%d team=%d flagsKnown=%d flags=0x%X localTime=%g globalTime=%g serializedWeight=%g serializedRatio=%g serializedBlend=%g propertyWeight=%g propertyRatio=%g weightKnown=%d weightTarget=%g weightSent=%d weightConfirmed=%d originalWeight=%g originalPropertyWeight=%g serialize=%p effectiveWeight=unknown effectiveRatio=unknown effectiveTeam=unverified readFailure=%s reason=%s",
      event, "MMD",
      (unsigned long long)s_cloth.owner.generation, (unsigned long long)s_cloth.owner.session,
      reinterpret_cast<void *>(s_cloth.owner.character), ClothFrame(),
      (unsigned long long)GetTickCount64(), GetCurrentThreadId(), i ? i->name : "session",
      i ? i->ref.id.instance : 0, i ? reinterpret_cast<void *>(i->ref.id.managed) : nullptr,
      r.process, r.state.valid, r.state.running, r.state.enabled, r.state.processEnabled,
      r.state.active, r.state.skip, r.camera, r.distance, r.lod, r.keep, r.team,
      r.flagsKnown, r.flags, r.time, r.globalTime, r.weight, r.ratio, r.blend,
      r.propertyWeight, r.propertyRatio, r.state.weightKnown, eiem_cloth::PlaybackWeight,
      i ? i->startup.weightSent : false, i ? i->startup.weightConfirmed : false,
      i ? i->originalWeight : NAN, i ? i->originalPropertyWeight : NAN,
      r.serialize, r.readFailure, reason);
  if (i)
    Log("[CLOTH-POSE-POLICY] event=%s session=%llu instance=%d frame=%d requested=%d target=%g sent=%d confirmed=%d originalRatio=%g originalPropertyRatio=%g serializedRatio=%g propertyRatio=%g effectiveReference=unverified reason=%s",
        event, (unsigned long long)s_cloth.owner.session, i->ref.id.instance, ClothFrame(),
        i->startup.weightSent, eiem_cloth::PlaybackPoseRatio, i->startup.poseRatioSent,
        i->startup.poseRatioConfirmed, i->originalRatio, i->originalPropertyRatio,
        r.ratio, r.propertyRatio, reason);
  if (i && r.resultKnown)
    Log("[CLOTH-RESULT] session=%llu instance=%d frame=%d result=%d warning=%d interpretation=opaque-runtime-enum",
        (unsigned long long)s_cloth.owner.session, i->ref.id.instance, ClothFrame(), r.result, r.warning);
}
static bool ClothScene(void *animator, int &scene) {
  void *go = nullptr, *boxed = nullptr;
  return ClothInvoke(s_clothUnity.getGO, animator, nullptr, go) && go &&
      ClothInvoke(s_clothUnity.scene, go, nullptr, boxed) && boxed &&
      ClothField(boxed, "m_Handle", "System.Int32", scene);
}
static bool ClothRead(ClothInstance &i, ClothReadback &r) {
  void *obj = nullptr, *go = nullptr;
  const auto life = ClothInspect(i.ref, obj);
  r.state.alive = life != ClothLife::Destroyed;
  if (life != ClothLife::Alive) { r.readFailure = "object-liveness"; return false; }
  bool ok = ClothInvoke(s_clothUnity.getGO, obj, nullptr, go) && go &&
      ClothValue(s_clothUnity.active, go, r.state.active) &&
      ClothValue(s_clothUnity.getEnabled, obj, r.state.enabled) &&
      ClothValue(s_clothUnity.globalTime, nullptr, r.globalTime) &&
      ClothInvoke(i.api.process, obj, nullptr, r.process);
  if (!ok) r.readFailure = "unity-state-or-get_Process";
  r.state.paused = std::isfinite(r.globalTime) && r.globalTime == 0.0f;
  r.state.process = r.process != nullptr;
  ClothValue(i.api.timeGet, obj, r.time);
  if (ClothInvoke(i.api.serialize, obj, nullptr, r.serialize) && r.serialize) {
    ClothField(r.serialize, "clothSimulateWeight", "System.Single", r.weight);
    ClothField(r.serialize, "animationPoseRatio", "System.Single", r.ratio);
    ClothField(r.serialize, "blendWeight", "System.Single", r.blend);
  }
  ClothField(obj, "clothSimulateWeightProperty", "System.Single", r.propertyWeight);
  ClothField(obj, "animationPoseRatioProperty", "System.Single", r.propertyRatio);
  r.state.weightKnown = std::isfinite(r.weight) && r.weight >= 0.0f && r.weight <= 1.0f;
  r.state.weightAtTarget = r.state.weightKnown && eiem_cloth::WeightAtTarget(r.weight);
  r.state.poseRatioKnown = std::isfinite(r.ratio) && r.ratio >= 0.0f && r.ratio <= 1.0f;
  r.state.poseRatioAtTarget = r.state.poseRatioKnown && eiem_cloth::PoseRatioAtTarget(r.ratio);
  if (r.process) {
    void *cls = il2cpp_object_get_class(r.process);
    auto boolean = [&](const char *name, bool &value) {
      const bool read = ClothValue(ClothMethod(cls, name, "System.Boolean"), r.process, value);
      if (!read) r.readFailure = name;
      return read;
    };
    ok &= boolean("IsValid", r.state.valid);
    ok &= boolean("IsRunning", r.state.running);
    ok &= boolean("get_IsEnable", r.state.processEnabled);
    ok &= boolean("IsSkipWriting", r.state.skip);
    ok &= boolean("IsCameraCullingInvisible", r.camera);
    ok &= boolean("IsDistanceCullingInvisible", r.distance);
    ok &= boolean("IsLodCulled", r.lod);
    ok &= boolean("IsCameraCullingKeep", r.keep);
    ok &= ClothValue(ClothMethod(cls, "get_TeamId", "System.Int32"), r.process, r.team);
    r.state.culled = r.camera || r.distance || r.lod;
    void *result = nullptr;
    if (ClothInvoke(ClothMethod(cls, "get_Result", "BeyondDynamicBone.ResultCode"), r.process, nullptr, result) && result)
      r.resultKnown = ClothField(result, "result", "BeyondDynamicBone.Define.Result", r.result) &&
          ClothField(result, "warning", "BeyondDynamicBone.Define.Result", r.warning);
    void *flags = nullptr;
    if (ClothInvoke(ClothMethod(cls, "GetStateFlag", "Unity.Collections.BitField32"), r.process, nullptr, flags) && flags) {
      uint32_t alignment = 0;
      if (il2cpp_class_value_size && il2cpp_class_value_size(il2cpp_object_get_class(flags), &alignment) == 4) {
        memcpy(&r.flags, (char *)flags + 16, 4);
        r.flagsKnown = true;
      }
    }
  }
  r.state.readable = ok;
  return ok;
}

#include "cloth/core/cloth_writer.h"

static bool ClothCaptureWeight(ClothInstance &i) {
  if (i.weightSerializeHandle) return true;
  const auto &r = i.last;
  if (!r.serialize || !r.state.weightKnown || !std::isfinite(r.propertyWeight) ||
      fabsf(r.weight - r.propertyWeight) > 0.000001f) return false;
  i.weightSerializeHandle = il2cpp_gchandle_new(r.serialize, false);
  if (!i.weightSerializeHandle) return false;
  i.originalWeight = r.weight;
  i.originalPropertyWeight = r.propertyWeight;
  return true;
}
static bool ClothWriteWeight(ClothInstance &i) {
  const auto work = s_cloth.owner;
  if (!ClothOwns(work) || !ClothCaptureWeight(i)) return false;
  void *obj = ClothTarget(i.ref), *sd = nullptr, *unused = nullptr;
  if (!obj || !i.api.weight || !i.api.changed ||
      !ClothInvoke(i.api.serialize, obj, nullptr, sd) || !sd ||
      sd != il2cpp_gchandle_get_target(i.weightSerializeHandle)) return false;
  float value = eiem_cloth::PlaybackWeight;
  void *args[] = {&value};
  i.changedWeight = true;
  const bool invoked = ClothInvokeWeightCommand(i.api.weight, obj, args, unused);
  const bool pushed = invoked && ClothOwns(work) && ClothInvoke(i.api.changed, obj, nullptr, unused);
  float actual = NAN, property = NAN;
  const bool read = ClothField(sd, "clothSimulateWeight", "System.Single", actual) &&
      ClothField(obj, "clothSimulateWeightProperty", "System.Single", property);
  i.weightWriter.ownWriteConfirmed = pushed && read && eiem_cloth::WeightAtTarget(actual) && eiem_cloth::WeightAtTarget(property);
  Log("[CLOTH-WRITER-OWN] session=%llu instance=%d frame=%d command=%d read=%d serialized=%g property=%g targetConfirmed=%d",
      (unsigned long long)work.session, i.ref.id.instance, ClothFrame(), pushed, read, actual,
      property, i.weightWriter.ownWriteConfirmed);
  ClothLog("WEIGHT-COMMAND", &i, pushed ? "setter-and-parameter-push-issued-awaiting-later-readback" : "setter-or-parameter-push-failed");
  return pushed;
}
static bool ClothCaptureRatio(ClothInstance &i) {
  if (i.capturedRatio) return true;
  if (!ClothCaptureWeight(i) || !i.last.state.poseRatioKnown ||
      !std::isfinite(i.last.propertyRatio) || fabsf(i.last.ratio - i.last.propertyRatio) > 0.000001f)
    return false;
  i.originalRatio = i.last.ratio;
  i.originalPropertyRatio = i.last.propertyRatio;
  i.capturedRatio = true;
  return true;
}
static bool ClothWriteRatio(ClothInstance &i) {
  const auto work = s_cloth.owner;
  if (!ClothOwns(work) || !i.startup.weightSent || !ClothCaptureRatio(i)) return false;
  void *obj = ClothTarget(i.ref), *sd = nullptr, *unused = nullptr;
  if (!obj || !i.api.ratio || !i.api.changed ||
      !ClothInvoke(i.api.serialize, obj, nullptr, sd) || !sd ||
      sd != il2cpp_gchandle_get_target(i.weightSerializeHandle)) return false;
  float value = eiem_cloth::PlaybackPoseRatio;
  void *args[] = {&value};
  i.changedRatio = true;
  const bool invoked = ClothInvoke(i.api.ratio, obj, args, unused);
  const bool pushed = invoked && ClothOwns(work) && ClothInvoke(i.api.changed, obj, nullptr, unused);
  ClothLog("POSE-COMMAND", &i, pushed ? "initial-reference-requested-awaiting-readback-no-reset" : "pose-setter-or-parameter-push-failed");
  return pushed;
}
static bool ClothRestoreRatio(ClothInstance &i, void *obj) {
  if (!i.changedRatio) return true;
  void *sd = nullptr, *unused = nullptr;
  if (!ClothInvoke(i.api.serialize, obj, nullptr, sd)) return false;
  if (!sd || !i.weightSerializeHandle || sd != il2cpp_gchandle_get_target(i.weightSerializeHandle)) {
    i.changedRatio = false;
    ClothLog("POSE-RESTORE", &i, "serialize-replaced-native-state-preserved");
    return true;
  }
  float value = i.originalRatio;
  void *args[] = {&value};
  const bool issued = ClothInvoke(i.api.ratio, obj, args, unused) &&
      ClothInvoke(i.api.changed, obj, nullptr, unused);
  float actual = NAN, property = NAN;
  const bool read = ClothField(sd, "animationPoseRatio", "System.Single", actual) &&
      ClothField(obj, "animationPoseRatioProperty", "System.Single", property);
  const bool ok = issued && read && fabsf(actual - i.originalRatio) <= 0.000001f &&
      fabsf(property - i.originalPropertyRatio) <= 0.000001f;
  i.last.ratio = actual; i.last.propertyRatio = property;
  if (ok) i.changedRatio = false;
  ClothLog("POSE-RESTORE", &i, ok ? "original-pose-ratio-inputs-read-back" : "restore-incomplete-snapshot-retained");
  return ok;
}
static bool ClothRestoreWeight(ClothInstance &i, void *obj) {
  if (!i.changedWeight) return true;
  void *sd = nullptr, *unused = nullptr;
  if (!ClothInvoke(i.api.serialize, obj, nullptr, sd)) return false;
  if (!sd || !i.weightSerializeHandle || sd != il2cpp_gchandle_get_target(i.weightSerializeHandle)) {
    i.changedWeight = false;
    ClothLog("WEIGHT-RESTORE", &i, "serialize-replaced-native-state-preserved");
    return true;
  }
  float value = i.originalWeight;
  void *args[] = {&value};
  const bool issued = ClothInvokeWeightCommand(i.api.weight, obj, args, unused) &&
      ClothInvoke(i.api.changed, obj, nullptr, unused);
  float actual = NAN, property = NAN;
  const bool read = ClothField(sd, "clothSimulateWeight", "System.Single", actual) &&
      ClothField(obj, "clothSimulateWeightProperty", "System.Single", property);
  const bool ok = issued && read && fabsf(actual - i.originalWeight) <= 0.000001f &&
      fabsf(property - i.originalPropertyWeight) <= 0.000001f;
  i.last.weight = actual;
  i.last.propertyWeight = property;
  if (ok) i.changedWeight = false;
  ClothLog("WEIGHT-RESTORE", &i, ok ? "original-inputs-read-back-native-update-unblocked-on-release" : "restore-incomplete-snapshot-retained");
  return ok;
}

static void ClothAddPoseProbe(ClothInstance &i, void *transform) {
  if (!transform || i.poseProbeCount == 8) return;
  ClothRef ref = ClothProtect(transform);
  if (!ref.handle) return;
  for (int n = 0; n < i.poseProbeCount; ++n) {
    if (i.poseProbes[n].ref.id == ref.id) { ClothFree(ref); return; }
  }
  auto &p = i.poseProbes[i.poseProbeCount++];
  p.ref = ref;
  void *name = nullptr;
  if (ClothInvoke(s_clothUnity.name, transform, nullptr, name))
    ReadStrUtf8(name, p.name, sizeof(p.name));
}
static void ClothCapturePoseProbes(ClothInstance &i) {
  if (i.poseProbesAttempted || !ClothOwns(s_cloth.owner)) return;
  i.poseProbesAttempted = true;
  __try {
    void *roots = nullptr;
    int count = 0;
    bool ok = ClothField(i.last.serialize, "rootBones", "System.Collections.Generic.List<UnityEngine.Transform>", roots) && roots;
    void *cls = ok ? il2cpp_object_get_class(roots) : nullptr;
    ok = ok && ClothValue(ClothMethod(cls, "get_Count", "System.Int32"), roots, count) && count >= 0 && count <= 1024;
    void *item = ok ? ClothMethod(cls, "get_Item", "UnityEngine.Transform", "System.Int32") : nullptr;
    for (int n = 0; ok && n < count && n < 4; ++n) {
      void *root = nullptr, *args[] = {&n};
      ok = ClothInvoke(item, roots, args, root) && ClothAlive(root);
      if (!ok) break;
      ClothAddPoseProbe(i, root);
      int children = 0;
      if (ClothValue(s_clothUnity.childCount, root, children) && children > 0) {
        int index = 0; void *child = nullptr, *childArgs[] = {&index};
        if (ClothInvoke(s_clothUnity.child, root, childArgs, child)) ClothAddPoseProbe(i, child);
      }
    }
    Log("[CLOTH-POSE-PROBES] session=%llu instance=%d rootCount=%d samples=%d readable=%d coverage=sample-only-max4roots-first-child",
        (unsigned long long)s_cloth.owner.session, i.ref.id.instance, count, i.poseProbeCount, ok);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    Log("[CLOTH-POSE-PROBES] session=%llu instance=%d reason=diagnostic-read-failed",
        (unsigned long long)s_cloth.owner.session, i.ref.id.instance);
  }
}
static void ClothLogPoseProbes(ClothInstance &i, const char *stage) {
  __try {
    for (int n = 0; n < i.poseProbeCount; ++n) {
      const auto &p = i.poseProbes[n];
      void *transform = ClothTarget(p.ref);
      if (!transform) continue;
      void *cls = il2cpp_object_get_class(transform);
      Vector3 local{NAN, NAN, NAN}, world{NAN, NAN, NAN};
      Quaternion rotation{NAN, NAN, NAN, NAN};
      bool ok = ClothValue(ClothMethod(cls, "get_localPosition", "UnityEngine.Vector3"), transform, local);
      ok &= ClothValue(ClothMethod(cls, "get_localRotation", "UnityEngine.Quaternion"), transform, rotation);
      ok &= ClothValue(ClothMethod(cls, "get_position", "UnityEngine.Vector3"), transform, world);
      Log("[CLOTH-POSE-SAMPLE] backend=%s generation=%llu session=%llu owner=%p frame=%d clothInstance=%d boneInstance=%d bone='%s' stage=%s readOk=%d local=(%g,%g,%g) rotation=(%g,%g,%g,%g) world=(%g,%g,%g) source=live-transform-effective-animation-buffer-unknown",
          "MMD",
          (unsigned long long)s_cloth.owner.generation, (unsigned long long)s_cloth.owner.session,
          reinterpret_cast<void *>(s_cloth.owner.character), ClothFrame(), i.ref.id.instance,
          p.ref.id.instance, p.name, stage, ok, local.x, local.y, local.z,
          rotation.x, rotation.y, rotation.z, rotation.w, world.x, world.y, world.z);
    }
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    Log("[CLOTH-POSE-SAMPLE] session=%llu instance=%d stage=%s reason=diagnostic-read-failed",
        (unsigned long long)s_cloth.owner.session, i.ref.id.instance, stage);
  }
}

#include "cloth/core/cloth_anchor.h"

static bool ClothWriteCollider(ClothColliderOriginal &c, bool restore) {
  if (!restore && !ClothOwns(s_cloth.owner)) return false;
  void *obj = nullptr;
  const auto life = ClothInspect(c.ref, obj);
  if (life == ClothLife::Unreadable) return false;
  if (life == ClothLife::Destroyed) { c.changed = false; return true; }
  Vector3 size = c.size, center = c.center;
  bool separation = c.separation;
  if (!restore) {
    if (s_collisionGeometry.load(std::memory_order_acquire) == 1) {
      if (!c.changed) { c.applied = true; return true; }
    } else {
      if (fabsf(c.center.x) <= 0.1f && !c.changed) { c.applied = true; return true; }
      if (fabsf(c.center.x) > 0.1f) {
        const float hs = s_clothCharacterHeight > 0.1f ? s_clothCharacterHeight / 1.245f : 1.0f;
        float hip = c.size.x + s_skirtHipRadiusDelta.load(std::memory_order_acquire) * hs;
        size.y = (hip < c.size.x * 3.0f) ? hip : c.size.x * 3.0f;
        separation = true;
      }
    }
  }
  c.changed = true;
  __try {
    memcpy((char *)obj + c.centerOff, &center, sizeof(center));
    memcpy((char *)obj + c.separationOff, &separation, sizeof(separation));
  } __except (EXCEPTION_EXECUTE_HANDLER) { return false; }
  void *args[] = {&size}, *unused = nullptr;
  if (!ClothInvoke(c.setSize, obj, args, unused) ||
      !ClothInvoke(c.update, obj, nullptr, unused)) return false;
  Vector3 actualSize{}, actualCenter{};
  bool actualSeparation = false;
  bool ok = ClothField(obj, "size", "UnityEngine.Vector3", actualSize) &&
      ClothField(obj, "center", "UnityEngine.Vector3", actualCenter) &&
      ClothField(obj, "radiusSeparation", "System.Boolean", actualSeparation) &&
      !memcmp(&actualSize, &size, sizeof(size)) &&
      !memcmp(&actualCenter, &center, sizeof(center)) && actualSeparation == separation;
  Log("[CLOTH-COLLIDER] session=%llu frame=%d instance=%d operation=%s readback=%d originalSeparation=%d managedGeometryOnly=1",
      (unsigned long long)s_cloth.owner.session, ClothFrame(), c.ref.id.instance,
      restore ? "restore" : "apply", ok, c.separation);
  c.applied = ok && !restore;
  if (c.applied) { c.appliedSize = size; c.appliedCenter = center; c.appliedSeparation = separation; }
  return ok;
}
static bool ClothColliderRegisteredToOwner(void *collider, int team) {
  void *set = nullptr;
  if (!ClothField(collider, "teamIdSet", "System.Collections.Generic.HashSet<System.Int32>", set) || !set)
    return false;
  void *cls = il2cpp_object_get_class(set);
  void *contains = ClothMethod(cls, "Contains", "System.Boolean", "System.Int32");
  void *boxed = nullptr, *args[] = {&team};
  int count = 0, known = 0;
  if (!ClothValue(ClothMethod(cls, "get_Count", "System.Int32"), set, count) || count <= 0 ||
      !ClothInvoke(contains, set, args, boxed) || !boxed || !ClothUnboxBool(boxed)) return false;
  for (int n = 0; n < s_cloth.count; ++n) {
    int candidate = s_cloth.instances[n].last.team;
    bool duplicate = false;
    for (int j = 0; j < n; ++j) duplicate |= s_cloth.instances[j].last.team == candidate;
    if (duplicate) continue;
    void *candidateArgs[] = {&candidate};
    if (!ClothInvoke(contains, set, candidateArgs, boxed) || !boxed) return false;
    if (ClothUnboxBool(boxed)) ++known;
  }
  return known == count;
}
static bool ClothApplyColliders(ClothInstance &i, bool dirty, uint64_t now) {
  if (!i.skirt) return true;
  if (!i.colliderDeadline) i.colliderDeadline = now + eiem_cloth::StartupBudgetMs;
  void *obj = ClothTarget(i.ref), *list = nullptr;
  if (!obj || !i.api.changed) return false;
  if (!ClothField(i.last.process, "colliderList",
                  "System.Collections.Generic.List<BeyondDynamicBone.ColliderComponent>", list) || !list)
    return now < i.colliderDeadline;
  void *cls = il2cpp_object_get_class(list);
  int count = 0;
  if (!ClothValue(ClothMethod(cls, "get_Count", "System.Int32"), list, count) ||
      count < 0 || count > static_cast<int>(ClothColliderCapacity)) return false;
  if (!count) return now < i.colliderDeadline;
  void *get = ClothMethod(cls, "get_Item", "BeyondDynamicBone.ColliderComponent", "System.Int32");
  void *registered[ClothColliderCapacity]{};
  for (int n = 0; n < count; ++n) {
    void *args[] = {&n};
    if (!ClothInvoke(get, list, args, registered[n])) return false;
    if (!ClothAlive(registered[n])) { registered[n] = nullptr; continue; }
    if (!ClothColliderRegisteredToOwner(registered[n], i.last.team)) {
      ClothLog("COLLIDER-WAIT", &i, "registration-missing-or-shared-outside-owner");
      return now < i.colliderDeadline;
    }
  }
  bool wrote = false;
  for (int n = 0; n < count; ++n) {
    void *collider = registered[n];
    if (!collider) continue;
    int id = 0;
    if (!ClothValue(s_clothUnity.instance, collider, id)) return false;
    eiem_cloth::ObjectId key{reinterpret_cast<uintptr_t>(collider), id};
    ClothColliderOriginal *c = s_cloth.colliders.Find(key);
    if (!c) {
      ClothColliderOriginal original{};
      cls = il2cpp_object_get_class(collider);
      original.separationOff = ClothFieldOffset(cls, "radiusSeparation", "System.Boolean");
      if (original.separationOff < 0) continue;
      original.sizeOff = ClothFieldOffset(cls, "size", "UnityEngine.Vector3");
      original.centerOff = ClothFieldOffset(cls, "center", "UnityEngine.Vector3");
      original.setSize = ClothMethod(cls, "SetSize", "System.Void", "UnityEngine.Vector3");
      original.update = ClothMethod(cls, "UpdateParameters", "System.Void");
      if (!original.setSize || !original.update || original.sizeOff < 0 || original.centerOff < 0 ||
          !ClothField(collider, "size", "UnityEngine.Vector3", original.size) ||
          !ClothField(collider, "center", "UnityEngine.Vector3", original.center) ||
          !ClothField(collider, "radiusSeparation", "System.Boolean", original.separation)) return false;
      if (!std::isfinite(original.size.x) || !std::isfinite(original.size.y) ||
          !std::isfinite(original.size.z) || !std::isfinite(original.center.x) ||
          !std::isfinite(original.center.y) || !std::isfinite(original.center.z)) return false;
      original.ref = ClothProtect(collider);
      if (!original.ref.handle) return false;
      c = s_cloth.colliders.Capture(key, original);
      if (!c) { ClothFree(original.ref); return false; }
      Log("[CLOTH-SNAPSHOT] session=%llu collider=%d size=(%g,%g,%g) center=(%g,%g,%g) radiusSeparation=%d capturedOnce=1",
          (unsigned long long)s_cloth.owner.session, id, original.size.x, original.size.y,
          original.size.z, original.center.x, original.center.y, original.center.z, original.separation);
    }
    if (!c->applied || dirty) {
      if (!ClothWriteCollider(*c, false)) return false;
      wrote = true;
    } else if (c->changed) {
      Vector3 actualSize{}, actualCenter{};
      bool actualSeparation = false;
      if (!ClothField(collider, "size", "UnityEngine.Vector3", actualSize) ||
          !ClothField(collider, "center", "UnityEngine.Vector3", actualCenter) ||
          !ClothField(collider, "radiusSeparation", "System.Boolean", actualSeparation)) return false;
      if (memcmp(&actualSize, &c->appliedSize, sizeof(actualSize)) ||
          memcmp(&actualCenter, &c->appliedCenter, sizeof(actualCenter)) || actualSeparation != c->appliedSeparation) {
        ClothLog("COLLIDER-OVERWRITE", &i, "managed-geometry-changed-source-unconfirmed-no-reassert");
        return false;
      }
    }
  }
  if (wrote) {
    void *unused = nullptr;
    for (int n = 0; n < s_cloth.count; ++n) {
      ClothInstance &owner = s_cloth.instances[n];
      void *target = ClothTarget(owner.ref);
      if (target && owner.last.state.running &&
          !ClothInvoke(owner.api.changed, target, nullptr, unused)) return false;
    }
    ClothLog("COMMAND", &i, "collider-parameters-pushed-job-readback-unavailable");
  }
  i.colliderDeadline = 0;
  return true;
}

#include "cloth/core/cloth_collision.h"

static bool ClothRestore() {
  if (ClothBoneLeased()) return false;
  bool ok = true, parametersChanged = false;
  for (size_t n = 0; n < s_cloth.anchors.count; ++n)
    ok &= ClothRestoreAnchor(s_cloth.anchors.entries[n].value);
  for (size_t n = 0; n < s_cloth.colliders.count; ++n) {
    ClothColliderOriginal &c = s_cloth.colliders.entries[n].value;
    if (c.changed) { parametersChanged = true; ok &= ClothWriteCollider(c, true); }
  }
  for (int n = 0; n < s_cloth.count; ++n) {
    ClothInstance &i = s_cloth.instances[n];
    void *obj = nullptr, *unused = nullptr;
    const auto life = ClothInspect(i.ref, obj);
    if (life == ClothLife::Unreadable) { ok = false; continue; }
    if (life == ClothLife::Destroyed) continue;
    ok &= ClothRestoreRatio(i, obj);
    ok &= ClothRestoreWeight(i, obj);
    if (parametersChanged) ok &= ClothInvoke(i.api.changed, obj, nullptr, unused);
    if (i.changedSkip) {
      void *process = nullptr;
      bool gotProcess = ClothInvoke(i.api.process, obj, nullptr, process);
      if (!gotProcess) { ok = false; continue; }
      if (process && i.processHandle && process == il2cpp_gchandle_get_target(i.processHandle)) {
        void *args[] = {&i.originalSkip};
        bool actual = !i.originalSkip;
        bool restored = ClothInvoke(i.api.skip, obj, args, unused) &&
            ClothValue(ClothMethod(il2cpp_object_get_class(process), "IsSkipWriting", "System.Boolean"), process, actual) &&
            actual == i.originalSkip;
        ok &= restored;
        if (restored) { i.changedSkip = false; i.last.state.skip = actual; }
      } else {
        ClothLog("RESTORE", &i, "skip-process-replaced-native-state-preserved");
        i.changedSkip = false;
      }
    }
    if (i.changedEnabled) {
      void *args[] = {&i.originalEnabled};
      bool actual = !i.originalEnabled;
      bool restored = ClothInvoke(s_clothUnity.setEnabled, obj, args, unused) &&
          ClothValue(s_clothUnity.getEnabled, obj, actual) && actual == i.originalEnabled;
      ok &= restored;
      if (restored) { i.changedEnabled = false; i.last.state.enabled = actual; }
    }
    ClothLog("RESTORE", &i, ok ? "commands-and-readbacks-complete" : "restore-incomplete");
  }
  if (ok) {
    for (size_t n = 0; n < s_cloth.anchors.count; ++n) {
      ClothFree(s_cloth.anchors.entries[n].value.ref);
      ClothFree(s_cloth.anchors.entries[n].value.parent);
    }
    for (size_t n = 0; n < s_cloth.colliders.count; ++n)
      ClothFree(s_cloth.colliders.entries[n].value.ref);
    for (int n = 0; n < s_cloth.count; ++n) {
      ClothFree(s_cloth.instances[n].ref);
      if (s_cloth.instances[n].processHandle)
        il2cpp_gchandle_free(s_cloth.instances[n].processHandle);
      if (s_cloth.instances[n].weightSerializeHandle)
        il2cpp_gchandle_free(s_cloth.instances[n].weightSerializeHandle);
      for (int p = 0; p < s_cloth.instances[n].poseProbeCount; ++p)
        ClothFree(s_cloth.instances[n].poseProbes[p].ref);
    }
    ClothFree(s_cloth.animator);
    ClothLog("RELEASED", nullptr, "restored-before-cache-clear-native-producers-unblocked-no-reset");
    const auto owner = s_cloth.owner;
    const bool failed = s_cloth.failed;
    const char *failureReason = s_cloth.failureReason;
    const auto discovery = s_cloth.discovery;
    const uint64_t invalidation = s_cloth.invalidation;
    s_cloth = {};
    s_cloth.owner = owner;
    s_cloth.failed = failed;
    s_cloth.failureReason = failureReason;
    s_cloth.discovery = discovery;
    s_cloth.invalidation = invalidation;
  }
  return ok;
}
static void ClothReleaseImpl(const char *reason) {
  if (!ClothOnMainThread()) { ClothRequestInvalidation(); return; }
  ClothInputClear();
  ClothCollisionRelease(reason);
  if (!s_cloth.active) return;
  s_cloth.active = false;
  s_cloth.releasing = true;
  s_cloth.restoreAttempts = 1;
  s_cloth.nextRestore = GetTickCount64() + eiem_cloth::PollMs;
  ClothLog("RELEASE", nullptr, reason);
  if (!ClothRestore()) ClothLog("RESTORE-PENDING", nullptr, "handles-retained-new-acquisition-blocked");
}
static void ClothFail(const char *reason) {
  s_cloth.failed = true;
  s_cloth.failureReason = reason;
  ClothLog("FAILED", nullptr, reason);
  ClothReleaseImpl(reason);
}

static bool ClothDiscover(uint64_t now) {
  auto &report = s_cloth.discovery;
  report = {};
  auto failure = [&](const char *reason) {
    if (!strcmp(report.firstFailure, "none")) report.firstFailure = reason;
  };
  void *animator = ClothTarget(s_cloth.animator), *root = nullptr;
  if (!animator || !ClothInvoke(s_clothUnity.getTransform, animator, nullptr, root) || !root) {
    ++report.readFailures; failure("owner-root-unreadable"); return false;
  }
  struct Node { void *transform; int depth; } stack[ClothDiscoveryNodeLimit]{};
  int top = 1, visited = 0, added = 0;
  bool complete = true;
  stack[0] = {root, 0};
  void *type = il2cpp_type_get_object(il2cpp_class_get_type(g_componentClass));
  if (!type) { ++report.readFailures; failure("component-type-unavailable"); return false; }
  while (top && visited < ClothDiscoveryNodeLimit) {
    const Node node = stack[--top];
    ++visited;
    if (ClothOwnedRoot(node.transform)) continue;
    report.maxDepth = (std::max)(report.maxDepth, node.depth);
    void *go = nullptr, *array = nullptr, *args[] = {type};
    if (!ClothInvoke(s_clothUnity.getGO, node.transform, nullptr, go) || !go ||
        !ClothInvoke(s_clothUnity.components, go, args, array) || !array) {
      complete = false; ++report.readFailures; failure("components-unreadable"); continue;
    }
    uintptr_t count = *reinterpret_cast<uintptr_t *>((char *)array + 24);
    if (count > 256) {
      complete = false; ++report.capacityCuts; failure("component-array-limit"); continue;
    }
    void **data = reinterpret_cast<void **>((char *)array + 32);
    for (uintptr_t n = 0; n < count; ++n) {
      void *obj = data[n];
      if (!obj) continue;
      void *cls = il2cpp_object_get_class(obj);
      const char *name = il2cpp_class_get_name(cls), *ns = il2cpp_class_get_namespace(cls);
      if (!name || !ns || strcmp(name, "BeyondBoneCloth") || strcmp(ns, "BeyondDynamicBone")) continue;
      int id = 0;
      if (!ClothAlive(obj) || !ClothValue(s_clothUnity.instance, obj, id)) continue;
      bool seen = false;
      for (int k = 0; k < s_cloth.count; ++k) seen |= s_cloth.instances[k].ref.id.instance == id;
      if (seen) continue;
      if (s_cloth.count == ClothCapacity) { complete = false; ++report.capacityCuts; failure("cloth-capacity-limit"); continue; }
      ClothInstance &i = s_cloth.instances[s_cloth.count];
      i.ref = ClothProtect(obj);
      if (!i.ref.handle) { complete = false; ++report.readFailures; failure("cloth-identity-unreadable"); continue; }
      ++s_cloth.count;
      ++added;
      i.api = ClothResolve(obj);
      if (s_clothWeightHookInstaller) s_clothWeightHookInstaller(i.api.weight);
      void *str = nullptr;
      if (ClothInvoke(s_clothUnity.name, go, nullptr, str)) ReadStrUtf8(str, i.name, sizeof(i.name));
      i.skirt = strstr(i.name, "Skirt") || strstr(i.name, "skirt");
      i.lastObservedAt = now;
      ClothRead(i, i.last);
      ClothCaptureWeight(i);
      ClothCaptureRatio(i);
      ClothCapturePoseProbes(i);
      ClothLogPoseProbes(i, s_cloth.scans == 0 && s_clothAllowAnchorCapture ? "begin-before-animation-suppression" : "late-discovery");
      ClothCaptureAnchors(i, s_cloth.count - 1, s_cloth.scans == 0 && s_clothAllowAnchorCapture);
      ClothLog("SNAPSHOT", &i, s_cloth.scans == 0 && s_clothAllowAnchorCapture ? "begin-before-animation-suppression" : "late-discovery-original-at-first-observation");
      if (i.last.state.readable) {
        i.originalEnabled = i.last.state.enabled;
        i.capturedEnabled = true;
        if (i.last.process) {
          i.processHandle = il2cpp_gchandle_new(i.last.process, false);
          i.originalSkip = i.last.state.skip;
          i.capturedSkip = i.processHandle != 0;
        }
      }
    }
    int children = 0;
    if (!ClothValue(s_clothUnity.childCount, node.transform, children) || children < 0) {
      complete = false; ++report.readFailures; failure("child-count-unreadable"); continue;
    }
    if (node.depth >= ClothDiscoveryDepthLimit && children) {
      complete = false; ++report.depthCuts; failure("hierarchy-depth-limit"); continue;
    }
    for (int n = 0; n < children; ++n) {
      void *child = nullptr, *childArgs[] = {&n};
      if (top == ClothDiscoveryNodeLimit) { complete = false; ++report.nodeCuts; failure("pending-node-limit"); break; }
      if (!ClothInvoke(s_clothUnity.child, node.transform, childArgs, child) || !child) {
        complete = false; ++report.readFailures; failure("child-unreadable"); continue;
      }
      stack[top++] = {child, node.depth + 1};
    }
  }
  complete &= top == 0;
  if (top) { ++report.nodeCuts; failure("visited-node-limit"); }
  report.nodes = visited; report.pending = top; report.complete = complete;
  ++s_cloth.scans;
  s_cloth.nextDiscovery = now + eiem_cloth::DiscoveryMs;
  ClothLog("DISCOVERY", nullptr, !complete ? "truncated-or-read-failure-not-complete" : s_cloth.count ? "bounded-owner-hierarchy-scanned" : "no-cloth-retry-pending");
  Log("[CLOTH-COVERAGE] session=%llu scan=%u nodes=%d added=%d total=%d complete=%d maxDepth=%d depthCuts=%d nodeCuts=%d readFailures=%d capacityCuts=%d pending=%d firstFailure=%s limits=depth%d/nodes%d/cloth%zu",
      (unsigned long long)s_cloth.owner.session, s_cloth.scans, visited, added, s_cloth.count, complete,
      report.maxDepth, report.depthCuts, report.nodeCuts, report.readFailures, report.capacityCuts,
      report.pending, report.firstFailure, ClothDiscoveryDepthLimit, ClothDiscoveryNodeLimit, ClothCapacity);
  return complete;
}

static void ClothBeginImpl(bool explicitPlay) {
  if (!ClothOnMainThread() || !s_clothRequested) return;
  const uint64_t generation = s_clothRequestGeneration;
  const uintptr_t character = reinterpret_cast<uintptr_t>(ClothHostEntity());
  const bool same = s_cloth.owner.backend == 1u &&
      s_cloth.owner.generation == generation && s_cloth.owner.character == character &&
      s_cloth.invalidation == s_clothInvalidation.load(std::memory_order_acquire);
  if (same && s_cloth.active) return;
  if (same && s_cloth.failed && !explicitPlay) return;
  if (s_cloth.active) ClothReleaseImpl("begin-owner-change");
  if (s_cloth.releasing) {
    if (ClothBoneLeased()) return;
    if (!explicitPlay) return;
    s_cloth.restoreAttempts = 1;
    s_cloth.nextRestore = GetTickCount64() + eiem_cloth::PollMs;
    if (!ClothRestore()) return;
  }
  const uint64_t now = GetTickCount64();
  const uint64_t invalidation = s_clothInvalidation.load(std::memory_order_acquire);
  if (generation != s_clothBeginGeneration || character != s_clothBeginCharacter ||
      invalidation != s_clothBeginInvalidation || explicitPlay) {
    s_clothBeginGeneration = generation;
    s_clothBeginCharacter = character;
    s_clothBeginInvalidation = invalidation;
    s_clothBeginAttempts = 0;
    s_clothNextBegin = 0;
  }
  if (now < s_clothNextBegin || s_clothBeginAttempts >= eiem_cloth::ResolveAttempts) return;
  ++s_clothBeginAttempts;
  s_clothNextBegin = now + eiem_cloth::DiscoveryMs;
  if (!ClothResolveUnity()) {
    Log("[CLOTH-BEGIN-FAILED] backend=%s generation=%llu owner=%p attempt=%u reason=unity-abi-unavailable",
        "MMD", (unsigned long long)generation, ClothHostEntity(), s_clothBeginAttempts);
    return;
  }
  ClothRef animator = ClothProtect(ClothHostAnimator());
  int scene = 0;
  if (!animator.handle || !character || !ClothScene(ClothHostAnimator(), scene)) {
    ClothFree(animator);
    Log("[CLOTH-BEGIN-FAILED] attempt=%u reason=owner-or-scene-unavailable", s_clothBeginAttempts);
    return;
  }
  s_cloth = {};
  s_cloth.owner = {1u, generation, character, ++s_clothSessionSerial};
  s_cloth.animator = animator;
  s_cloth.scene = scene;
  s_cloth.invalidation = s_clothInvalidation.load(std::memory_order_acquire);
  s_cloth.active = true;
  s_clothBeginAttempts = 0;
  ClothLog("BEGIN", nullptr, "snapshot-before-owner-pose-cloth-preparation-gates-body-and-timeline");
  if (!ClothDiscover(GetTickCount64())) ClothFail("discovery-incomplete");
}
static bool ClothSameFloat(float a, float b) {
  return a == b || (std::isnan(a) && std::isnan(b));
}
static void ClothTickImpl(const char *stage, bool poseSubmitted, ClothBoneGuard boneGuard, bool preparationOnly=false) {
  if (!ClothOnMainThread()) return;
  ClothShoulderDriverMaintenance();
  ClothCollisionServiceUi();
  ClothCollisionMaintenance();
  const uint64_t now = GetTickCount64();
  if (s_cloth.releasing) {
    if (ClothBoneLeased()) return;
    if (s_cloth.restoreAttempts < eiem_cloth::RestoreAttempts && now >= s_cloth.nextRestore) {
      ++s_cloth.restoreAttempts;
      s_cloth.nextRestore = now + eiem_cloth::PollMs;
      if (!ClothRestore()) ClothLog("RESTORE-PENDING", nullptr, "retry-budgeted-handles-retained");
    }
    return;
  }
  if (!s_cloth.active) return;
  const auto work = s_cloth.owner;
  void *animator = ClothTarget(s_cloth.animator);
  int scene = 0;
  if (!s_clothRequested || s_cloth.owner.generation != s_clothRequestGeneration ||
      s_cloth.owner.backend != 1u ||
      s_cloth.owner.character != reinterpret_cast<uintptr_t>(ClothHostEntity()) ||
      s_cloth.invalidation != s_clothInvalidation.load(std::memory_order_acquire) ||
      !animator || animator != ClothHostAnimator() || !ClothScene(animator, scene) || scene != s_cloth.scene) {
    ClothReleaseImpl("owner-generation-scene-or-plugin-invalidated");
    return;
  }
  if (!poseSubmitted) return;
  s_cloth.bodyGuard = boneGuard;
  int frame = ClothFrame();
  if (frame < 0) { ClothFail("unity-frame-unavailable"); return; }
  if (frame == s_cloth.lastFrame) return;
  s_cloth.lastFrame = frame;
  if (s_cloth.scans < eiem_cloth::DiscoveryAttempts && now >= s_cloth.nextDiscovery) {
    if (!ClothDiscover(now)) { ClothFail("discovery-incomplete"); return; }
  }
  if (!s_cloth.count && s_cloth.scans == eiem_cloth::DiscoveryAttempts) {
    if (!s_cloth.discovery.complete) { ClothFail("discovery-incomplete"); return; }
    if (!s_cloth.emptySourceReported) {
      s_cloth.emptySourceReported = true;
      ClothLog("DISCOVERY", nullptr, "no-source-BBC-owner-retained-for-runtime-asset-discovery");
    }
  }
  const float hip=s_skirtHipRadiusDelta.load();const int geometry=s_collisionGeometry.load();
  const bool dirty = s_skirtDirty.exchange(false, std::memory_order_acq_rel) ||
      s_cloth.appliedHip!=hip || s_cloth.appliedGeometry!=geometry;
  s_cloth.appliedHip=hip;s_cloth.appliedGeometry=geometry;
  for (int n = 0; n < s_cloth.count; ++n) {
    if (!ClothOwns(work)) { ClothReleaseImpl("stale-session-during-tick"); return; }
    ClothInstance &i = s_cloth.instances[n];
    const auto missed=s_clothDeferredWeights.Take(i.ref.id.managed,now);
    if (!dirty && !missed.object && now < i.nextPoll) continue;
    const auto oldPhase = i.startup.phase;
    const char *oldReason = i.startup.reason;
    ClothReadback r{};
    ClothRead(i, r);
    if (ClothBoneLeasedInstance(i)) {
      i.last=r;
      i.nextPoll=now+eiem_cloth::AuditMs; continue;
    }
    if (r.serialize && i.weightSerializeHandle &&
        r.serialize != il2cpp_gchandle_get_target(i.weightSerializeHandle)) {
      ClothLog("WEIGHT-IDENTITY", &i, "serialize-replaced-cancel-old-weight-transaction");
      il2cpp_gchandle_free(i.weightSerializeHandle);
      i.weightSerializeHandle = 0;
      i.changedWeight = false;
      i.changedRatio = false;
      i.originalWeight = i.originalPropertyWeight = NAN;
      ClothFail("serialize-replaced-requires-new-cloth-session");
      return;
    }
    const bool suspended = r.state.readable && (!r.state.active || r.state.culled || r.state.paused);
    if (i.colliderDeadline && (suspended || i.wasSuspended) && now >= i.lastObservedAt)
      i.colliderDeadline += now - i.lastObservedAt;
    i.lastObservedAt = now;
    i.wasSuspended = suspended;
    void *oldProcess = i.processHandle ? il2cpp_gchandle_get_target(i.processHandle) : nullptr;
    if (r.state.readable && r.process != oldProcess) {
      if (++i.processChanges > 4) { ClothFail("process-replacement-budget-exhausted"); return; }
      if (i.processHandle) il2cpp_gchandle_free(i.processHandle);
      i.processHandle = r.process ? il2cpp_gchandle_new(r.process, false) : 0;
      i.changedSkip = i.capturedSkip = false;
      i.weightWriter = {};
      i.writerProcessClass = nullptr;
      i.startup.readyReads = 0;
      i.startup.skipSent = false;
      for (size_t c = 0; c < s_cloth.colliders.count; ++c)
        s_cloth.colliders.entries[c].value.applied = false;
      i.colliderDeadline = 0;
      i.last = r;
      ClothLog("PROCESS", &i, "identity-changed-revalidate-no-recapture-existing-colliders");
    }
    const bool numericChange = !ClothSameFloat(r.weight, i.last.weight) || !ClothSameFloat(r.ratio, i.last.ratio) ||
        !ClothSameFloat(r.time, i.last.time) || !ClothSameFloat(r.propertyWeight, i.last.propertyWeight) ||
        !ClothSameFloat(r.propertyRatio, i.last.propertyRatio);
    i.last = r;
    ClothCaptureWeight(i);
    ClothCaptureRatio(i);
    if (!i.poseSubmissionLogged) {
      i.poseSubmissionLogged = true;
      ClothLogPoseProbes(i, preparationOnly ? "held-native-pose-before-first-motion-write" : "first-owner-pose-before-cloth-commands");
    }
    if (!ClothUpdateAnchors(i, n, frame, boneGuard)) {
      ClothFail("anchor-command-readback-or-ownership-failed"); return;
    }
    if (r.state.readable && !i.capturedEnabled) { i.originalEnabled = r.state.enabled; i.capturedEnabled = true; }
    if (r.state.readable && r.process && !i.capturedSkip) { i.originalSkip = r.state.skip; i.capturedSkip = true; }
    bool missedRecovery=false;
    if (r.state.readable && !r.state.weightAtTarget && !i.startup.commandFailed &&
        i.startup.phase != eiem_cloth::Phase::Failed && ClothWriterFind(ClothTarget(i.ref)) == n &&
        ((missedRecovery=i.weightWriter.RecoverMissed(missed,r.weight,r.propertyWeight)) ||
         (i.startup.elapsed<eiem_cloth::StartupBudgetMs&&i.weightWriter.RecoverPersistent(frame,r.weight,r.propertyWeight)))) {
      const bool repaired = ClothWriteWeight(i);
      Log("[CLOTH-WRITER-RECOVERY] session=%llu instance=%d frame=%d observedFrame=%d caller=%p "
          "observations=%u overwritten=%g command=%d targetReadback=%d budget=1 laterReadback=pending",
          (unsigned long long)work.session, i.ref.id.instance, frame, i.weightWriter.lastFrame,
          reinterpret_cast<void *>(i.weightWriter.caller), i.weightWriter.observations, r.weight,
          repaired, i.weightWriter.ownWriteConfirmed);
      i.startup.CommandResult(repaired);
      if (!repaired) { ClothFail("observed-weight-recovery-command-failed"); return; }
      i.startup.readyReads = 0;
      i.startup.phase = eiem_cloth::Phase::Verifying;
      i.startup.reason = "observed-native-weight-repair-awaiting-later-readback";
      if(missedRecovery)Log("[CLOTH-WRITER-RECOVERY] actorSlot=%u instance=%d skippedByPoseMutex=1 knownProducer=1 valueReadback=1 observedCallConsumed=1",s_clothActorIndex,i.ref.id.instance);
      i.nextPoll = now + eiem_cloth::PollMs;
      continue;
    }
    const auto action = i.startup.Step(r.state, now, frame);
    i.nextPoll = now + (i.startup.phase == eiem_cloth::Phase::Ready && !i.anchorsPolling ? eiem_cloth::AuditMs :
        g_clothPlaybackGate.Holding(work.backend,work.generation) ? 0 : eiem_cloth::PollMs);
    if (oldPhase != i.startup.phase || oldReason != i.startup.reason || numericChange)
      ClothLog("STATE", &i, i.startup.reason);
    if (action == eiem_cloth::Action::ReadbackReady &&
        (oldPhase != eiem_cloth::Phase::Ready || numericChange)) {
      ClothLog("WEIGHT-READBACK", &i, i.startup.weightSent ? "target-confirmed-across-frames-simulation-unverified" : "already-near-target-no-weight-write");
      ClothLog("POSE-READBACK", &i, i.startup.weightSent ? "initial-reference-input-confirmed-game-visuals-unverified" : "native-reference-retained-normal-start");
      ClothLogPoseProbes(i, "startup-inputs-confirmed");
    }
    if (action == eiem_cloth::Action::Fail) {
      ClothLog("FAILED-INSTANCE", &i, i.startup.reason);
      ClothFail(i.startup.reason); return;
    }
    void *obj = ClothTarget(i.ref), *result = nullptr;
    if (!ClothOwns(work)) { ClothReleaseImpl("stale-before-command"); return; }
    bool ok = true;
    if (action == eiem_cloth::Action::Enable) {
      bool value = true; void *args[] = {&value};
      i.changedEnabled = true;
      ok = i.capturedEnabled && ClothInvoke(s_clothUnity.setEnabled, obj, args, result);
    } else if (action == eiem_cloth::Action::ClearSkip) {
      bool value = false; void *args[] = {&value};
      i.changedSkip = true;
      ok = i.capturedSkip && i.processHandle && ClothInvoke(i.api.skip, obj, args, result);
    } else if (action == eiem_cloth::Action::Build) {
      ok = ClothInvoke(i.api.build, obj, nullptr, result) && result;
      const bool accepted = ok && ClothUnboxBool(result);
      Log("[CLOTH-BUILD] session=%llu instance=%d frame=%d invokeOk=%d returnedBool=%d confirmedRunning=0",
          (unsigned long long)s_cloth.owner.session, i.ref.id.instance, frame, ok, accepted);
      ok &= accepted;
    } else if (action == eiem_cloth::Action::SetWeight) {
      ok = ClothWriteWeight(i);
      if (!ok) ClothLog("WEIGHT-FAILED", &i, "snapshot-input-mismatch-identity-or-api-failure");
    } else if (action == eiem_cloth::Action::SetPoseRatio) {
      ok = ClothWriteRatio(i);
      if (!ok) ClothLog("POSE-FAILED", &i, "snapshot-input-mismatch-identity-or-api-failure");
    } else if (action == eiem_cloth::Action::ReadbackReady) {
      if (!ClothApplyColliders(i, dirty, now)) { ClothFail("collider-api-readback-or-registration-timeout"); return; }
      continue;
    } else continue;
    i.startup.CommandResult(ok);
    ClothLog("COMMAND", &i, ok ? "issued-awaiting-readback" : "failed");
    if (!ok) { ClothFail("command-failed-or-managed-exception"); return; }
  }
  ClothCollisionAfterPose(stage, frame);
  if (s_cloth.lastFrame == frame && frame % 300 == 0)
    ClothLog("SCHEDULE", nullptr, stage);
}

struct ClothActorBusy {
  bool busy=false,releasePending=false;
};
static ClothActorBank<ClothActorBusy> s_ClothActorBusy;
#define s_clothBusy (s_ClothActorBusy.Get().busy)
#define s_clothReleasePending (s_ClothActorBusy.Get().releasePending)
static void ClothRuntimeFault() {
  s_cloth.active = false;
  s_cloth.releasing = true;
  s_cloth.failed = true;
  s_cloth.failureReason = "native-access-exception";
  s_cloth.nextRestore = GetTickCount64() + eiem_cloth::PollMs;
  Log("[CLOTH-FAILED] session=%llu reason=native-access-exception snapshotsRetained=1 restoreAttempts=%u",
      (unsigned long long)s_cloth.owner.session, s_cloth.restoreAttempts);
}
static void ClothDrainCancellation() {
  if (s_clothReleasePending || (s_cloth.active && !ClothOwns(s_cloth.owner))) {
    s_clothReleasePending = false;
    ClothReleaseImpl("cancelled-during-lifecycle-call");
  }
}
static void ClothBegin(bool explicitPlay = false) {
  if (!ClothOnMainThread() || s_clothBusy) return;
  if(s_clothRequested && ClothEnhancementSetting().load() &&
      (!s_cloth.active || !ClothOwns(s_cloth.owner))) {
    auto &gate=g_clothPlaybackGate;
    if(gate.State()==eiem_playback::Preparation::Ready || gate.State()==eiem_playback::Preparation::Prepared)gate.Cancel();
    const auto previous=gate.Ticket();
    const auto ticket=gate.Arm(1u,s_clothRequestGeneration,uintptr_t(ClothHostEntity()),GetTickCount64());
    if(ticket!=previous)Log("[CLOTH-PLAYBACK-START] actorSlot=%u stage=waiting generation=%llu ticket=%llu bodyHeld=1 timelineHeld=1 audioHeld=1",s_clothActorIndex,s_clothRequestGeneration,ticket);
  }
  s_clothBusy = true;
  __try { ClothBeginImpl(explicitPlay); ClothDrainCancellation(); }
  __except (EXCEPTION_EXECUTE_HANDLER) { ClothRuntimeFault(); }
  s_clothBusy = false;
}
static void ClothTick(const char *stage, bool poseSubmitted = false, ClothBoneGuard boneGuard = nullptr,
                      double sourceFrame = NAN, bool preparationOnly = false) {
  if (!ClothOnMainThread() || s_clothBusy) return;
  s_clothBusy = true;
  __try {
    ClothTickImpl(stage, poseSubmitted, boneGuard, preparationOnly);
    if (poseSubmitted) ClothInputSubmit(stage, preparationOnly ? NAN : sourceFrame);
    ClothDrainCancellation();
    ClothPlaybackService(false);
    if(poseSubmitted&&!preparationOnly&&ClothOwns(s_cloth.owner)) {
      auto &gate=g_clothPlaybackGate;const auto &owner=s_cloth.owner;const auto ticket=gate.Ticket();
      const int frame=ClothFrame(),prepared=gate.PreparedFrame();
      if(gate.PoseSubmitted(ticket,owner.backend,owner.generation,owner.character,frame))
        Log("[CLOTH-PLAYBACK-START] stage=ready backend=%s generation=%llu session=%llu ticket=%llu elapsedMs=%llu preparedUnityFrame=%d firstMotionUnityFrame=%d firstMotionSourceFrame=%.6f source=%s bodySubmittedAfterClothReady=1 bodyAndAudioMayAdvance=1 visualVerified=0",
            "MMD",owner.generation,owner.session,ticket,gate.Elapsed(GetTickCount64()),prepared,frame,sourceFrame,stage);
    }
  }
  __except (EXCEPTION_EXECUTE_HANDLER) { ClothRuntimeFault(); }
  s_clothBusy = false;
}
static bool ClothBlockFirstBodyPose(const char *stage,ClothBoneGuard guard) {
  const auto backend=1u;const auto generation=s_clothRequestGeneration;
  if(g_clothPlaybackGate.StateFor(backend,generation)==eiem_playback::Preparation::Prepared)
    ClothPlaybackService(false);
  if(!g_clothPlaybackGate.BlocksPose(backend,generation))return false;
  if(ClothOnMainThread()&&g_clothPlaybackGate.Matches(backend,generation,uintptr_t(ClothHostEntity()))&&
      g_clothPlaybackGate.State()==eiem_playback::Preparation::Waiting)
    ClothTick(stage,true,guard,NAN,true);
  return true;
}
static void ClothPlaybackService(bool completedBoundary) {
  using eiem_playback::Preparation;
  if(!ClothOnMainThread())return;
  const uint32_t backend=1u;
  const uint64_t generation=s_clothRequestGeneration;
  const auto character=reinterpret_cast<uintptr_t>(ClothHostEntity());
  auto &gate=g_clothPlaybackGate;
  if(gate.Matches(backend,generation,character) && gate.State()==Preparation::Failed &&
      !ClothEnhancementSetting().load() && !ClothBonePending() && !ClothBoneLeased()) {
    gate.Cancel();return;
  }
  if(!gate.Matches(backend,generation,character)||
      (gate.State()!=Preparation::Waiting&&gate.State()!=Preparation::Prepared))return;
  const auto ticket=gate.Ticket();const auto elapsed=gate.Elapsed(GetTickCount64());
  const bool enabled=ClothEnhancementSetting().load(std::memory_order_acquire);
  bool ready=false,failed=false;const char *reason="waiting-for-owner-startup";
  if(!enabled && !ClothBonePending() && !ClothBoneLeased()) {
    ready=true;reason="enhancement-disabled-original-restored";
  } else if(s_cloth.owner.generation==generation && s_cloth.owner.character==character && s_cloth.failed) {
    failed=true;reason=s_cloth.failureReason;
  } else if(ClothOwns(s_cloth.owner) && s_cloth.discovery.complete) {
    if(!enabled) {
      ready=!ClothBonePending()&&!ClothBoneLeased();reason="enhancement-disabled-original-restored";
    } else if(s_clothAutoTriedSession==s_cloth.owner.session && s_clothAutoTriedGeneration==generation) {
      if(s_clothBoneNoMatch && !ClothBonePending()) {ready=true;reason="no-supported-garment-original-retained";}
      else if(s_clothAutoDeferred || s_clothAutoWaiting)reason="waiting-for-source-generation";
      else if(s_clothBoneResolved) {
        ready=true;reason="native-Teams-renderers-and-completed-output-confirmed";
        for(int n=0;n<s_clothBoneCount;++n) {
          const auto &s=s_clothBoneSlots[n];
          if(s.failed||s.stopRequested||s.tx.cancelled) {failed=true;reason=s.failure[0]?s.failure:s.issue;break;}
          if(!ClothBoneAppliedKind(s,s_cloth.owner)||!s.teamModeConfirmed||
              (s.local.requested&&!s.local.solverConfirmed)) {ready=false;reason="waiting-for-native-registration-and-output";}
        }
      } else if(!s_clothBone.pending) {failed=true;reason=s_clothBone.failure[0]?s_clothBone.failure:"enhancement-preparation-failed";}
    }
    if(ready)for(int n=0;n<s_cloth.count;++n) {
      const auto &i=s_cloth.instances[n];if(ClothBoneLeasedInstance(i))continue;
      if(i.startup.phase==eiem_cloth::Phase::Waiting||i.startup.phase==eiem_cloth::Phase::Verifying)ready=false;
    }
  }
  if(elapsed>=120000) {failed=true;reason="preparation-timeout-stop-or-retry";}
  s_ClothActorRequest.Get().preparationReason=reason;
  if(failed) {
    gate.Observe(ticket,false,true,-1);
    Log("[CLOTH-PLAYBACK-START] stage=failed backend=%s generation=%llu ticket=%llu elapsedMs=%llu reason=%s timelineHeld=1 automaticFallbackPlay=0",
        "MMD",generation,ticket,elapsed,reason);
  } else if(completedBoundary || (!enabled&&!ClothBonePending()) || s_clothBoneNoMatch ||
      (gate.State()==Preparation::Prepared&&!ready)) {
    if(gate.Observe(ticket,ready,false,ClothFrame()))
      Log("[CLOTH-PLAYBACK-START] stage=prepared backend=%s generation=%llu session=%llu ticket=%llu elapsedMs=%llu targetMs=1000 withinTarget=%d reason=%s preparedUnityFrame=%d firstMotionWritten=0 firstMotionMayWrite=1 timelineHeld=1 audioHeld=1 visualVerified=0",
          "MMD",generation,s_cloth.owner.session,ticket,elapsed,int(elapsed<=1000),reason,gate.PreparedFrame());
  }
}
static void ClothRelease(const char *reason) {
  if (!ClothOnMainThread()) { ClothRequestInvalidation(); return; }
  if (s_clothBusy) {
    ClothRequestInvalidation();
    s_clothReleasePending = true;
    return;
  }
  s_clothBusy = true;
  __try { ClothReleaseImpl(reason); ClothDrainCancellation(); }
  __except (EXCEPTION_EXECUTE_HANDLER) { ClothRuntimeFault(); }
  s_clothBusy = false;
}

// No new session may replace retained restore snapshots. A play/pause cycle keeps
// its original lease; stop, actor changes and physics freeze end that lease.
static void ClothRequestPlayback(bool wanted, bool beforeSuppression = false) {
  if (s_clothRequested == wanted) return;
  s_clothRequested = wanted;
  ++s_clothRequestGeneration;
  s_clothAllowAnchorCapture = wanted && beforeSuppression && ClothOnMainThread();
  if (!wanted) {g_clothPlaybackGate.Cancel();s_ClothActorRequest.Get().preparationReason="idle";ClothRelease("playback-stopped-or-physics-frozen");}
  else ClothBegin();
}
static void ClothService(bool poseSubmitted = false, ClothBoneGuard guard = nullptr, double sourceFrame = NAN) {
  if (!ClothOnMainThread()) return;
  ClothTick("native-game-frame", false);
  if (s_clothRequested) ClothBegin();
  if (poseSubmitted) ClothTick("after-mmd-pose", true, guard, sourceFrame);
  s_clothAllowAnchorCapture = false;
}

static void ClothServiceActors() {
  if(!ClothOnMainThread())return;
  for(unsigned slot=0;slot<ClothActorCount;++slot) {
    if(slot && !s_ClothActorRequest.values[slot])continue;
    ClothActorScope scope(slot);ClothService();
    if(slot==0)ClothPrefetchPulse();
  }
}
static bool ClothSquadRestoring() {
  for(unsigned slot=1;slot<ClothActorCount;++slot) {
    if(ClothActorEngaged(slot))return true;
    const auto &bones=s_ClothActorBones.values[slot];
    if(bones)for(const auto &bone:bones->slots)if(bone.pending||bone.lease)return true;
    const auto &shoulder=s_clothShoulderDriverActors.values[slot];
    if(shoulder&&shoulder->stopping)return true;
  }
  return false;
}
