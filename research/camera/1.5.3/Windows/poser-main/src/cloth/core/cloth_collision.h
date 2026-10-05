#pragma once
#include "../collision/cloth_collision_math.h"
#include <iomanip>
#include <sstream>
#include <string>
#include <type_traits>
#include <vector>

struct ClothActorCommands {
  std::atomic<bool> inspect{false},stop{false};
  std::atomic<unsigned> request{0};
  std::atomic<uint64_t> session{0},requestedAt{0};
  unsigned seen=0,exportSeen=0;
};
static ClothActorBank<ClothActorCommands> s_ClothActorCommands;
#define s_collisionInspect (s_ClothActorCommands.Get().inspect)
#define s_clothBoneRequest (s_ClothActorCommands.Get().request)
#define s_clothBoneStopRequested (s_ClothActorCommands.Get().stop)
#define s_clothBoneSession (s_ClothActorCommands.Get().session)
#define s_clothBoneRequestedAt (s_ClothActorCommands.Get().requestedAt)
#define s_clothBoneSeen (s_ClothActorCommands.Get().seen)
#define s_collisionExportSeen (s_ClothActorCommands.Get().exportSeen)
static std::atomic<bool> s_clothAutoEnabled{true};
static std::atomic<bool> s_clothSquadAutoEnabled{true};
static std::atomic<bool> &ClothEnhancementSetting() {return s_clothActorIndex?s_clothSquadAutoEnabled:s_clothAutoEnabled;}
static const char *CollisionListType = "System.Collections.Generic.List<BeyondDynamicBone.ColliderComponent>";
static void ClothBoneQueueCommand(uint64_t session,bool stop) {
  ClothEnhancementSetting().store(!stop,std::memory_order_release);
  s_clothBoneStopRequested.store(stop,std::memory_order_release);
  s_clothBoneSession.store(session,std::memory_order_release);
  s_clothBoneRequestedAt.store(GetTickCount64(),std::memory_order_release);
  s_clothBoneRequest.fetch_add(1,std::memory_order_acq_rel);
  s_collisionInspect.store(true,std::memory_order_release);
}
static void ClothSetEnhancementEnabled(bool enabled) {
  ClothActorScope scope(0);ClothBoneQueueCommand(s_cloth.owner.session,!enabled);
}
static void ClothSetSquadEnhancementEnabled(bool enabled) {
  // Keep the squad's default independent of single-player presets and toggles.
  // UI requests only queue commands; native restoration stays on the game thread.
  s_clothSquadAutoEnabled.store(enabled,std::memory_order_release);
  for(unsigned actor=1;actor<ClothActorCount;++actor) {
    if(!ClothActorEngaged(actor))continue;
    ClothActorScope scope(actor);ClothBoneQueueCommand(s_cloth.owner.session,!enabled);
  }
}
struct ClothActorExport {
  std::atomic<unsigned> request{0};
  std::atomic<uint64_t> session{0},requestedAt{0};
  std::atomic<int> status{0};
};
static ClothActorBank<ClothActorExport> s_ClothActorExport;
#define s_collisionExport (s_ClothActorExport.Get().request)
#define s_collisionExportSession (s_ClothActorExport.Get().session)
#define s_collisionExportRequestedAt (s_ClothActorExport.Get().requestedAt)
#define s_collisionExportStatus (s_ClothActorExport.Get().status)
struct CollisionUi {
  int count = 0;
  char names[ClothCapacity][112]{};
  eiem_cloth::ObjectId ids[ClothCapacity]{};
  uint64_t session = 0;
  bool failed = false;
  char issue[128]{};
  bool boneBusy=false;
  bool boneRestoring=false;
  int catalogCount=0;
  char catalogSource[48]{};
  int boneCount=0;
  int boneKinds[8]{};
  bool boneApplied[8]{};
  int authoredApplied=0,autoConnectionsApplied=0,autoSkinApplied=0,autoPartialApplied=0;
  int autoPreserved=0;
  bool autoPreparing=false,autoChecked=false;
  char boneNames[8][64]{};
  char boneIssues[8][192]{};
  char boneIssue[192]{};
};
static ClothActorBank<CollisionUi> s_collisionUiActors;
#define s_collisionUi (s_collisionUiActors.Get())
static SRWLOCK s_collisionUiLock = SRWLOCK_INIT;
static CollisionUi CollisionGetUi() {
  AcquireSRWLockShared(&s_collisionUiLock);
  CollisionUi ui = s_collisionUi;
  ReleaseSRWLockShared(&s_collisionUiLock);
  return ui;
}
struct CollisionExportNote {
  DWORD error = 0;
  char phase[64]{}, path[MAX_PATH * 4]{};
};
static ClothActorBank<CollisionExportNote> s_collisionExportNoteActors;
#define s_collisionExportNote (s_collisionExportNoteActors.Get())
static CollisionExportNote CollisionGetExportNote() {
  AcquireSRWLockShared(&s_collisionUiLock);
  auto note = s_collisionExportNote;
  ReleaseSRWLockShared(&s_collisionUiLock);
  return note;
}
static void CollisionSetExportNote(int status, const char *phase, DWORD error = 0,
                                   const char *path = "") {
  CollisionExportNote note{};
  note.error = error;
  strncpy_s(note.phase, phase, _TRUNCATE);
  strncpy_s(note.path, path, _TRUNCATE);
  AcquireSRWLockExclusive(&s_collisionUiLock);
  s_collisionExportNote = note;
  ReleaseSRWLockExclusive(&s_collisionUiLock);
  s_collisionExportStatus.store(status, std::memory_order_release);
}
static void CollisionQueueExport(uint64_t session) {
  s_clothVerboseDiagnostics.store(true);
  s_collisionExportSession.store(session, std::memory_order_release);
  s_collisionExportRequestedAt.store(GetTickCount64(), std::memory_order_release);
  CollisionSetExportNote(2, "awaiting-owner-pose");
  auto request = s_collisionExport.fetch_add(1, std::memory_order_acq_rel) + 1;
  Log("[CLOTH-CONTACT-EXPORT] event=request request=%u session=%llu tid=%lu", request,
      (unsigned long long)session, GetCurrentThreadId());
}
static const char *CollisionBodyNames[] = {
    "Hips",          "Spine",         "Chest",         "UpperChest",    "Neck",
    "Head",          "LeftUpperLeg",  "LeftLowerLeg",  "LeftFoot",      "RightUpperLeg",
    "RightLowerLeg", "RightFoot",     "LeftShoulder",  "LeftUpperArm",  "LeftLowerArm",
    "LeftHand",      "RightShoulder", "RightUpperArm", "RightLowerArm", "RightHand"};
constexpr int CollisionBodyCount = sizeof(CollisionBodyNames) / sizeof(CollisionBodyNames[0]);
using CV = eiem_collision::V;
static CV CollisionV(Vector3 v) { return {v.x, v.y, v.z}; }
static Vector3 CollisionV(CV v) { return {float(v.x), float(v.y), float(v.z)}; }

static bool CollisionType(void *type, const char *expected) {
  if (!type || !s_clothFreeName)
    return false;
  const char *raw = il2cpp_type_get_name(type);
  if (!raw)
    return false;
  char name[256]{};
  strncpy_s(name, raw, _TRUNCATE);
  s_clothFreeName(const_cast<char *>(raw));
  for (char *p = name; *p; ++p)
    if (*p == '/')
      *p = '.';
  return !strcmp(name, expected);
}
static void *CollisionFieldInfo(void *cls, const char *name, const char *type) {
  using namespace poser_cloth_metadata;
  const auto key=Lookup(cls,Kind::NormalizedField,name,type);
  if(auto hit=cache.Find(key)) return reinterpret_cast<void *>(hit);
  for (int depth = 0; cls && depth < 16; ++depth, cls = il2cpp_class_get_parent(cls)) {
    void *it = nullptr;
    while (void *f = il2cpp_class_get_fields(cls, &it))
      if (!strcmp(il2cpp_field_get_name(f), name) && CollisionType(il2cpp_field_get_type(f), type))
        return reinterpret_cast<void *>(cache.Remember(key,uintptr_t(f)));
  }
  return nullptr;
}
template <class T>
static bool CollisionField(void *obj, const char *name, const char *type, T &value) {
  if (!obj)
    return false;
  __try {
    void *f = CollisionFieldInfo(il2cpp_object_get_class(obj), name, type);
    if (!f)
      return false;
    size_t off = il2cpp_field_get_offset(f);
    if (off < 16 || off > 65536 - sizeof(T))
      return false;
    memcpy(&value, (char *)obj + off, sizeof(T));
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}
static bool CollisionEnumValue(void *cls, const char *name, int &value) {
  if (!cls || !il2cpp_field_static_get_value || !il2cpp_field_get_flags || !il2cpp_class_from_type)
    return false;
  void *underlying = CollisionFieldInfo(cls, "value__", "System.Int32");
  if (!underlying || (il2cpp_field_get_flags(underlying) & 0x10))
    return false;
  void *it = nullptr;
  while (void *f = il2cpp_class_get_fields(cls, &it)) {
    if (!strcmp(il2cpp_field_get_name(f), name) && (il2cpp_field_get_flags(f) & 0x50) == 0x50 &&
        il2cpp_class_from_type(il2cpp_field_get_type(f)) == cls) {
      il2cpp_field_static_get_value(f, &value);
      return true;
    }
  }
  return false;
}
static bool CollisionEnum(void *obj, const char *field, const char *type, char *name, size_t size) {
  strcpy_s(name, size, "unknown");
  int value = 0;
  if (!CollisionField(obj, field, type, value) || !il2cpp_class_from_type)
    return false;
  void *f = CollisionFieldInfo(il2cpp_object_get_class(obj), field, type);
  void *cls = il2cpp_class_from_type(il2cpp_field_get_type(f));
  if (!cls)
    return false;
  void *it = nullptr;
  while (void *entry = il2cpp_class_get_fields(cls, &it)) {
    int candidate = 0;
    const char *label = il2cpp_field_get_name(entry);
    if (CollisionEnumValue(cls, label, candidate) && candidate == value) {
      strncpy_s(name, size, label, _TRUNCATE);
      return true;
    }
  }
  return false;
}
static bool CollisionContains(void *list, void *object, const char *type, bool &contains) {
  contains = false;
  if (!list)
    return true;
  void *r = nullptr, *args[] = {object};
  if (!ClothInvoke(ClothMethod(il2cpp_object_get_class(list), "Contains", "System.Boolean", type),
                   list, args, r) ||
      !r)
    return false;
  contains = ClothUnboxBool(r);
  return true;
}
static int CollisionCount(void *list) {
  if (!list)
    return 0;
  int count = -1;
  return ClothValue(ClothMethod(il2cpp_object_get_class(list), "get_Count", "System.Int32"), list,
                    count) &&
                 count >= 0 && count <= 2048
             ? count
             : -1;
}
static void *CollisionItem(void *list, int index, const char *type) {
  void *r = nullptr, *args[] = {&index};
  return list && ClothInvoke(
                     ClothMethod(il2cpp_object_get_class(list), "get_Item", type, "System.Int32"),
                     list, args, r)
             ? r
             : nullptr;
}
static bool CollisionList(void *serialize, void *&constraint, void *&list) {
  constraint = list = nullptr;
  return CollisionField(serialize, "colliderCollisionConstraint",
                        "BeyondDynamicBone.ColliderCollisionConstraint.SerializeData",
                        constraint) &&
         constraint &&
         ClothField(constraint, "colliderList",
                    "System.Collections.Generic.List<BeyondDynamicBone.ColliderComponent>", list) &&
         list;
}
static void CollisionName(void *obj, char *name, size_t capacity) {
  strcpy_s(name, capacity, "unknown");
  void *str = nullptr;
  if (obj && ClothInvoke(s_clothUnity.name, obj, nullptr, str) && str)
    ReadStrUtf8(str, name, int(capacity));
}
static void *CollisionTransform(void *obj) {
  void *t = nullptr;
  return obj && ClothInvoke(ClothMethod(il2cpp_object_get_class(obj), "get_transform",
                                        "UnityEngine.Transform"),
                            obj, nullptr, t)
             ? t
             : nullptr;
}
static void *CollisionParent(void *t) {
  void *p = nullptr;
  return t && ClothInvoke(ClothMethod(g_transformClass, "get_parent", "UnityEngine.Transform"), t,
                          nullptr, p)
             ? p
             : nullptr;
}
static bool CollisionPosition(void *t, Vector3 &v) {
  return t &&
         ClothValue(ClothMethod(g_transformClass, "get_position", "UnityEngine.Vector3"), t, v) &&
         ClothFinitePosition(v);
}
static bool CollisionScale(void *t, Vector3 &v) {
  return t &&
         ClothValue(ClothMethod(g_transformClass, "get_lossyScale", "UnityEngine.Vector3"), t, v) &&
         ClothFinitePosition(v);
}
static bool CollisionPoint(void *t, Vector3 local, Vector3 &world) {
  void *r = nullptr, *args[] = {&local};
  if (!ClothInvoke(ClothMethod(g_transformClass, "TransformPoint", "UnityEngine.Vector3",
                               "UnityEngine.Vector3"),
                   t, args, r) ||
      !r)
    return false;
  memcpy(&world, (char *)r + 16, sizeof(world));
  return ClothFinitePosition(world);
}
static bool CollisionUniformFrame(void *t, float scale, void *method = nullptr) {
  if (!method)
    method = ClothMethod(g_transformClass, "TransformVector", "UnityEngine.Vector3",
                         "UnityEngine.Vector3");
  Vector3 basis[3]{{1, 0, 0}, {0, 1, 0}, {0, 0, 1}};
  for (auto &v : basis) {
    void *args[] = {&v}, *r = nullptr;
    if (!ClothInvoke(method, t, args, r) || !r)
      return false;
    memcpy(&v, (char *)r + 16, sizeof(v));
  }
  return eiem_collision::UniformBasis(CollisionV(basis[0]), CollisionV(basis[1]),
                                      CollisionV(basis[2]), scale);
}
static void *CollisionBody(void *animator, int index) {
  if (!animator || index < 0 || index >= CollisionBodyCount)
    return nullptr;
  size_t count = 0;
  void **asms = il2cpp_domain_get_assemblies(il2cpp_domain_get(), &count);
  void *cls = FindClass("UnityEngine", "HumanBodyBones", asms, count);
  int value = 0;
  if (!CollisionEnumValue(cls, CollisionBodyNames[index], value))
    return nullptr;
  void *r = nullptr, *args[] = {&value};
  if (!ClothInvoke(ClothMethod(il2cpp_object_get_class(animator), "GetBoneTransform",
                               "UnityEngine.Transform", "UnityEngine.HumanBodyBones"),
                   animator, args, r) ||
      !r)
    return nullptr;
  return ClothUnderAnimator(r,animator) ? r : nullptr;
}

struct CollisionGeometry {
  char type[80]{}, bone[128]{}, parentName[128]{}, direction[24]{};
  int id = 0, parentId = 0;
  bool valid = false, enabled = false, active = false, uniform = false, reverse = false,
       separated = false, centered = false, flagsKnown = false;
  Vector3 center{}, size{}, scale{}, axis{}, worldCenter{};
  float apiScale = NAN;
  eiem_collision::Capsule world{};
};
static bool CollisionWorldCapsule(void *t, eiem_collision::Capsule local, float scale,
                                  eiem_collision::Capsule &out) {
  if (!local.valid)
    return false;
  Vector3 a{}, b{};
  if (!CollisionPoint(t, CollisionV(local.a), a) || !CollisionPoint(t, CollisionV(local.b), b))
    return false;
  out = {CollisionV(a), CollisionV(b), local.ra * scale, local.rb * scale, true};
  return true;
}
static CollisionGeometry CollisionReadGeometry(void *obj) {
  CollisionGeometry g{};
  if (!ClothAlive(obj))
    return g;
  void *cls = il2cpp_object_get_class(obj), *t = CollisionTransform(obj), *go = nullptr;
  strncpy_s(g.type, il2cpp_class_get_name(cls), _TRUNCATE);
  CollisionName(t, g.bone, sizeof(g.bone));
  void *parent = CollisionParent(t);
  CollisionName(parent, g.parentName, sizeof(g.parentName));
  if (parent)
    ClothValue(s_clothUnity.instance, parent, g.parentId);
  ClothValue(s_clothUnity.instance, obj, g.id);
  ClothValue(s_clothUnity.getEnabled, obj, g.enabled);
  if (ClothInvoke(s_clothUnity.getGO, obj, nullptr, go) && go)
    ClothValue(s_clothUnity.active, go, g.active);
  if (!t || !ClothField(obj, "center", "UnityEngine.Vector3", g.center) ||
      !ClothField(obj, "size", "UnityEngine.Vector3", g.size) || !CollisionScale(t, g.scale) ||
      !CollisionPoint(t, g.center, g.worldCenter))
    return g;
  g.uniform =
      eiem_collision::UniformPositive(CollisionV(g.scale)) && CollisionUniformFrame(t, g.scale.x);
  ClothValue(ClothMethod(cls, "GetScale", "System.Single"), obj, g.apiScale);
  if (!strcmp(g.type, "BeyondBoneSphereCollider")) {
    if (g.uniform && std::isfinite(g.size.x) && g.size.x > 0) {
      g.world = {CollisionV(g.worldCenter), CollisionV(g.worldCenter), g.size.x * g.scale.x,
                 g.size.x * g.scale.x, true};
      g.valid = true;
    }
    return g;
  }
  if (strcmp(g.type, "BeyondBoneCapsuleCollider"))
    return g;
  g.flagsKnown = ClothField(obj, "reverseDirection", "System.Boolean", g.reverse) &&
                 ClothField(obj, "radiusSeparation", "System.Boolean", g.separated) &&
                 ClothField(obj, "alignedOnCenter", "System.Boolean", g.centered);
  if (!CollisionEnum(obj, "direction", "BeyondDynamicBone.BeyondBoneCapsuleCollider.Direction",
                     g.direction, sizeof(g.direction)) ||
      !g.flagsKnown)
    return g;
  if (!strcmp(g.direction, "X"))
    g.axis = {1, 0, 0};
  else if (!strcmp(g.direction, "Y"))
    g.axis = {0, 1, 0};
  else if (!strcmp(g.direction, "Z"))
    g.axis = {0, 0, 1};
  else
    return g;
  auto local = eiem_collision::LocalCapsule(CollisionV(g.center), CollisionV(g.axis),
                                           CollisionV(g.size), g.reverse, g.separated, g.centered);
  g.valid = g.uniform && CollisionWorldCapsule(t, local, g.scale.x, g.world);
  return g;
}
static void *CollisionGc(uint32_t handle) {
  return handle ? il2cpp_gchandle_get_target(handle) : nullptr;
}

static bool CollisionProcessContains(void *process, void *component, bool &contains) {
  contains = false;
  if (!process)
    return true;
  void *list = nullptr;
  return ClothField(process, "colliderList", CollisionListType, list) &&
         CollisionContains(list, component, "BeyondDynamicBone.ColliderComponent", contains);
}

static bool CollisionTeams(void *component, int team, bool &member, int &count) {
  void *set = nullptr;
  member = false;
  count = -1;
  if (!ClothField(component, "teamIdSet", "System.Collections.Generic.HashSet<System.Int32>",
                  set) ||
      !set)
    return false;
  count = CollisionCount(set);
  void *r = nullptr, *args[] = {&team};
  return count >= 0 &&
         ClothInvoke(ClothMethod(il2cpp_object_get_class(set), "Contains", "System.Boolean",
                                 "System.Int32"),
                     set, args, r) &&
         r && (member = ClothUnboxBool(r), true);
}
static void *CollisionCapsuleSizeMethod(void *cls) {
  if (!s_clothMethodFlags || !il2cpp_method_get_return_type || !il2cpp_method_get_param)
    return nullptr;
  for (int depth = 0; cls && depth < 16; ++depth, cls = il2cpp_class_get_parent(cls)) {
    void *it = nullptr;
    while (void *m = il2cpp_class_get_methods(cls, &it)) {
      uint32_t impl = 0;
      if (strcmp(il2cpp_method_get_name(m), "SetSize") || il2cpp_method_get_param_count(m) != 3 ||
          (s_clothMethodFlags(m, &impl) & 0x10) ||
          !ClothTypeIs(il2cpp_method_get_return_type(m), "System.Void"))
        continue;
      bool matched = true;
      for (unsigned n = 0; n < 3; ++n)
        matched &= ClothTypeIs(il2cpp_method_get_param(m, n), "System.Single");
      if (matched)
        return m;
    }
  }
  return nullptr;
}
#include "../collision/cloth_collision_parameters.h"
static bool ClothBonePending();
static bool ClothBoneLeased();
static void ClothBoneRelease(const char *reason);
static void ClothBoneService(bool pose,int frame);
static void ClothBoneBoundary();
static std::string ClothBoneJson();
static void ClothBoneSolverCompleted(void *manager);
static void ClothBoneSolverPoseSubmitted(const char *stage,double playhead);
static void ClothBoneSolverClear(int slot);
static std::string ClothBoneSolverJson();
static bool ClothPrefetchNeedsHooks();
static void ClothPrefetchBoundary();

static void ClothCollisionRelease(const char *reason) {
  __try { ClothBoneRelease(reason); }
  __except (EXCEPTION_EXECUTE_HANDLER) {
    Log("[CLOTH-CONTACT-FAULT] release-retains-owned-references");
  }
}
static bool ClothCollisionNeedsMaintenance() { return ClothBonePending(); }
#include "../diagnostics/cloth_input_trace.h"
#include "../diagnostics/cloth_collision_snapshot.h"
#include "../native/cloth_native_runtime.h"
#include "../bonecloth/cloth_bonecloth_runtime.h"
#include "../bonecloth/cloth_bonecloth_prefetch.h"
#include "../diagnostics/cloth_bonecloth_trace.h"
static void CollisionPublishUi() {
  CollisionUi ui{};
  ui.session = s_cloth.active ? s_cloth.owner.session : 0;
  ui.count = s_cloth.active ? std::clamp(s_cloth.count, 0, static_cast<int>(ClothCapacity)) : 0;
  ui.failed = s_cloth.failed;
  if (ui.failed)
    _snprintf_s(ui.issue, _TRUNCATE, "%s: %s", s_cloth.failureReason,
                s_cloth.discovery.firstFailure);
  ui.boneBusy=ClothBonePending();
  ui.catalogCount=int(s_clothBoneCatalog.size());
  strncpy_s(ui.catalogSource,s_clothBoneCatalogSource,_TRUNCATE);
  strncpy_s(ui.boneIssue,s_clothBone.issue,_TRUNCATE);
  ui.boneCount=s_clothBoneCount;
  ui.autoPreparing=s_clothAutoWaiting && !s_clothAutoDeferred;
  if(s_clothAutoJob&&s_clothAutoJob->done.load(std::memory_order_acquire)&&s_clothAutoJob->session==s_cloth.owner.session&&s_clothAutoJob->generation==s_cloth.owner.generation) {
    ui.autoChecked=true;std::set<std::string> generated;
    if(s_clothAutoLease)for(const auto &p:s_clothAutoLease->profiles)generated.insert(p->view.component);
    for(const auto &c:s_clothAutoJob->query.cloths)if(!generated.count(c.name))++ui.autoPreserved;
  }
  for(int n=0;n<ui.boneCount;++n) {
    const auto &s=s_clothBoneSlots[n];
    ui.boneRestoring|=(s.pending||s.lease) && (s.stopRequested||s.tx.cancelled||s.local.cleanup||s.supportCleanup);
    ui.boneKinds[n]=ClothBoneSourceKind(s.profile);const int applied=ClothBoneAppliedKind(s,s_cloth.owner);ui.boneApplied[n]=applied!=0;
    if(applied==1)++ui.authoredApplied;else if(applied==2)++ui.autoConnectionsApplied;else if(applied==3)++ui.autoSkinApplied;else if(applied==4)++ui.autoPartialApplied;
    strncpy_s(ui.boneNames[n],s.profile?s.profile->component:"pending",_TRUNCATE);
    if(s.failure[0])
      _snprintf_s(ui.boneIssues[n],_TRUNCATE,"%s%s",s.pending||s.lease?"failed, restoring: ":"failed: ",s.failure);
    else strncpy_s(ui.boneIssues[n],s.issue,_TRUNCATE);
  }
  for (int n = 0; n < ui.count; ++n) {
    ui.ids[n] = s_cloth.instances[n].ref.id;
    _snprintf_s(ui.names[n], _TRUNCATE, "%d: %.*s", n,
                static_cast<int>(sizeof(s_cloth.instances[n].name)), s_cloth.instances[n].name);
  }
  AcquireSRWLockExclusive(&s_collisionUiLock);
  s_collisionUi = ui;
  ReleaseSRWLockExclusive(&s_collisionUiLock);
}
static void ClothCollisionServiceUi() {
  if (!ClothOnMainThread())
    return;
  if (s_collisionInspect.exchange(false, std::memory_order_acq_rel))
    CollisionPublishUi();
  const auto request = s_collisionExport.load(std::memory_order_acquire);
  if (request == s_collisionExportSeen)
    return;
  const char *reason = nullptr;
  if (!s_cloth.active || !ClothOwns(s_cloth.owner))
    reason = s_cloth.failed ? "cloth-session-failed" : "no-active-cloth-session";
  else if (s_collisionExportSession.load(std::memory_order_acquire) != s_cloth.owner.session)
    reason = "stale-session-request-discarded";
  else if (GetTickCount64() - s_collisionExportRequestedAt.load(std::memory_order_acquire) >= 8000)
    reason = "owner-pose-timeout";
  if (reason) {
    s_collisionExportSeen = request;
    CollisionSetExportNote(-1, reason);
    Log("[CLOTH-CONTACT-EXPORT] event=rejected request=%u reason=%s clothFailure=%s "
        "discoveryFailure=%s",
        request, reason, s_cloth.failureReason, s_cloth.discovery.firstFailure);
  }
}
static void ClothCollisionMaintenance() {
  if (!ClothOnMainThread() || !ClothCollisionNeedsMaintenance()) return;
  __try { ClothBoneService(false, ClothFrame()); }
  __except (EXCEPTION_EXECUTE_HANDLER) {
    ClothBoneRelease("maintenance-native-exception");
  }
}
static void CollisionAfterPoseImpl(const char *stage, int frame) {
  ClothBoneService(true, frame);
  if (s_collisionInspect.exchange(false, std::memory_order_acq_rel)) CollisionPublishUi();
  const unsigned request = s_collisionExport.load(std::memory_order_acquire);
  if (request != s_collisionExportSeen) {
    s_collisionExportSeen = request;
    if (s_collisionExportSession.load(std::memory_order_acquire) == s_cloth.owner.session)
      CollisionTryExportSnapshot(stage);
    else {
      CollisionSetExportNote(-1, "stale-session-request-discarded");
      Log("[CLOTH-CONTACT-EXPORT] stale-session-request-discarded");
    }
  }
}
static void ClothCollisionAfterPose(const char *stage, int frame) {
  if (!ClothOnMainThread()) return;
  __try { CollisionAfterPoseImpl(stage, frame); }
  __except (EXCEPTION_EXECUTE_HANDLER) {
    ClothBoneRelease("enhancement-or-diagnostic-native-exception");
    Log("[CLOTH-CONTACT-FAULT] enhancement-operation-failed-base-simulation-retained");
  }
}
