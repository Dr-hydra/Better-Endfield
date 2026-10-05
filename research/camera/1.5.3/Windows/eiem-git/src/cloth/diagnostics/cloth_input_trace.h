#pragma once
#include "../native/cloth_input_state.h"
#include "../native/cloth_native_math.h"

constexpr int ClothInputTargets = 24;
struct ClothInputPose {
  bool known = false;
  float matrix[16]{};
};
struct ClothInputPoint {
  ClothInputPose visible{};
  bool inputKnown = false;
  int slot = -1, flags = -1;
  double position[3]{};
  float rotation[4]{}, scale[3]{}, matrix[16]{};
};
struct ClothInputSample {
  uint64_t sequence = 0, submission = 0, epoch = 0;
  int frame = -1, submittedFrame = -1, phase = 0, count = 0, relative = -1;
  double sourceFrame = NAN, playheadFrame = NAN, costMs = 0;
  float relativePosition[3]{}, relativeRotation[4]{};
  bool teamKnown = false, culled = false;
  uint64_t teamFlags = 0;
  ClothInputPoint points[ClothInputTargets]{};
};
struct ClothInputTarget {
  ClothRef transform{};
  char name[128]{}, role[24]{};
  int slot = -1;
  bool duplicate = false;
};
struct ClothInputBinding {
  ClothRef privateBBC{};
  eiem_cloth_input::Identity identity{};
  eiem_cloth_input::Mapping mapping{};
  ClothInputTarget targets[ClothInputTargets]{};
  int count = 0, index = -1, rootsVersion = -1, collidersVersion = -1;
  uintptr_t roots = 0, colliders = 0;
  int rootCount = -1, colliderSlots = -1, liveColliders = 0, emptyColliderSlots = 0;
};
struct ClothInputTrace : ClothInputBinding {
  uint64_t sequence = 0, submission = 0;
  int submittedFrame = -1, readFrame = -1;
  double sourceFrame = NAN, playheadFrame = NAN;
  char stage[80]{}, issue[128]{"awaiting-owner-pose"};
  bool failed = false;
  bool listFailure = false;
  eiem_cloth_input::RemapBudget remapBudget{};
  eiem_cloth_input::CostBudget bodyBudget{}, inputBudget{};
  unsigned completedBoundaries = 0;
  eiem_cloth_input::Ring<ClothInputSample, 256> body;
  eiem_cloth_input::Ring<ClothInputSample, 64> inputs;
} static s_clothInput;
static void (*s_clothInputHookInstaller)() = nullptr;
static bool s_clothInputHooks = false;
static char s_clothInputHookIssue[128]{"not-attempted"};
static int s_clothInputReadMask = -1, s_clothInputEnableMask = -1;
static void *s_clothInputManagerClass = nullptr;
static unsigned char *s_clothInputUpdateCode = nullptr;
static void *s_clothInputCallsite = nullptr;
static uint64_t s_clothInputPatchedFingerprint = 0;
constexpr size_t ClothInputAuditedBytes = 809;
static thread_local unsigned s_clothInputUpdateDepth = 0;
static void ClothBodyClear();
static void ClothContactClear();
struct alignas(8) ClothInputJobHandle { uint64_t a, b; };
using ClothInputUpdateFn = void(__fastcall *)(void *, void *);
using ClothInputValidFn = ClothInputJobHandle *(__fastcall *)(
    ClothInputJobHandle *, void *, const ClothInputJobHandle *, void *);
static ClothInputUpdateFn s_clothInputOriginalUpdate = nullptr;
static ClothInputValidFn s_clothInputOriginalValid = nullptr;
static ClothInputUpdateFn s_clothSurfaceOriginalTeamUpdate = nullptr;
static bool s_clothSurfaceHook = false;
static void *s_clothSurfaceTeamCallsite = nullptr;
static unsigned char *s_clothSurfaceTeamCode = nullptr;
static uint64_t s_clothSurfaceTeamFingerprint = 0;
static thread_local bool s_clothSurfaceAtBoundary = false;


static void ClothInputIssue(const char *text, bool fail = false) {
  if (strcmp(s_clothInput.issue, text))
    Log("[CLOTH-INPUT] session=%llu frame=%d issue=%s readOnly=1",
        (unsigned long long)s_cloth.owner.session, ClothFrame(), text);
  strncpy_s(s_clothInput.issue, text, _TRUNCATE);
  if (!s_clothInputHooks) strncpy_s(s_clothInputHookIssue, text, _TRUNCATE);
  s_clothInput.failed |= fail;
  if (fail) s_clothInput.listFailure = false;
}
static void ClothInputClear() {
  if (!ClothOnMainThread()) return;
  ClothBodyClear();
  ClothContactClear();
  for (auto &t : s_clothInput.targets) ClothFree(t.transform);
  s_clothInput.body.Clear(); s_clothInput.inputs.Clear();
  s_clothInput.identity = {}; s_clothInput.mapping.Invalidate();
  s_clothInput.count = 0; s_clothInput.index = -1;
  s_clothInput.failed = false; s_clothInput.completedBoundaries = 0;
  s_clothInput.listFailure = false; s_clothInput.remapBudget = {};
  s_clothInput.rootCount = s_clothInput.colliderSlots = -1;
  s_clothInput.liveColliders = s_clothInput.emptyColliderSlots = 0;
  s_clothInput.roots = s_clothInput.colliders = 0;
  s_clothInput.rootsVersion = s_clothInput.collidersVersion = -1;
  s_clothInput.bodyBudget = {}; s_clothInput.inputBudget = {};
  s_clothInput.sequence = s_clothInput.submission = 0;
  s_clothInput.submittedFrame = s_clothInput.readFrame = -1;
  strcpy_s(s_clothInput.issue, "awaiting-owner-pose");
}
static void *ClothInputMethod(void *cls, const char *name, const char *ret,
                              const char *arg = nullptr) {
  for (void *it = nullptr, *m = nullptr; (m = il2cpp_class_get_methods(cls, &it));) {
    uint32_t impl = 0;
    if (!strcmp(il2cpp_method_get_name(m), name) &&
        il2cpp_method_get_param_count(m) == (arg ? 1u : 0u) &&
        !(s_clothMethodFlags(m, &impl) & 0x10) &&
        CollisionType(il2cpp_method_get_return_type(m), ret) &&
        (!arg || CollisionType(il2cpp_method_get_param(m, 0), arg))) return m;
  }
  return nullptr;
}
static bool ClothInputLayout(void *cls, const char *type, size_t bytes) {
  uint32_t align = 0;
  if (il2cpp_class_value_size(cls, &align) != int(bytes)) return false;
  auto field = [&](const char *name, const char *fieldType, int offset, int size) {
    return ClothValueOffset(cls, name, fieldType, int(bytes), size) == offset;
  };
  if (!strcmp(type, "UnityEngine.Matrix4x4")) {
    for (int column = 0; column < 4; ++column) for (int row = 0; row < 4; ++row) {
      char name[8]{}; _snprintf_s(name, _TRUNCATE, "m%d%d", row, column);
      if (!field(name, "System.Single", (column * 4 + row) * 4, 4)) return false;
    }
    return true;
  }
  if (!strcmp(type, "Unity.Mathematics.float4x4")) {
    for (int c = 0; c < 4; ++c) {
      char name[8]{}; _snprintf_s(name, _TRUNCATE, "c%d", c);
      if (!field(name, "Unity.Mathematics.float4", c * 16, 16)) return false;
    }
    return true;
  }
  if (!strcmp(type, "Unity.Mathematics.double3"))
    return field("x", "System.Double", 0, 8) && field("y", "System.Double", 8, 8) && field("z", "System.Double", 16, 8);
  if (!strcmp(type, "Unity.Mathematics.float2"))
    return field("x", "System.Single", 0, 4) && field("y", "System.Single", 4, 4);
  if (!strcmp(type, "Unity.Mathematics.double3x2")) {
    auto f = CollisionFieldInfo(cls, "c0", "Unity.Mathematics.double3");
    auto nested = f ? il2cpp_class_from_type(il2cpp_field_get_type(f)) : nullptr;
    return field("c0", "Unity.Mathematics.double3", 0, 24) &&
        field("c1", "Unity.Mathematics.double3", 24, 24) && nested &&
        ClothInputLayout(nested, "Unity.Mathematics.double3", 24);
  }
  if (!strcmp(type, "UnityEngine.Quaternion"))
    return field("x", "System.Single", 0, 4) && field("y", "System.Single", 4, 4) &&
        field("z", "System.Single", 8, 4) && field("w", "System.Single", 12, 4);
  if (!strcmp(type, "Unity.Mathematics.int2"))
    return field("x", "System.Int32", 0, 4) && field("y", "System.Int32", 4, 4);
  if (!strcmp(type, "Unity.Mathematics.float3") || !strcmp(type, "UnityEngine.Vector3"))
    return field("x", "System.Single", 0, 4) && field("y", "System.Single", 4, 4) && field("z", "System.Single", 8, 4);
  if (!strcmp(type, "Unity.Mathematics.quaternion")) {
    auto f = CollisionFieldInfo(cls, "value", "Unity.Mathematics.float4");
    auto nested = f ? il2cpp_class_from_type(il2cpp_field_get_type(f)) : nullptr;
    return field("value", "Unity.Mathematics.float4", 0, 16) && nested &&
        ClothValueOffset(nested, "x", "System.Single", 16, 4) == 0 &&
        ClothValueOffset(nested, "y", "System.Single", 16, 4) == 4 &&
        ClothValueOffset(nested, "z", "System.Single", 16, 4) == 8 &&
        ClothValueOffset(nested, "w", "System.Single", 16, 4) == 12;
  }
  if (!strcmp(type, "BeyondDynamicBone.ExBitFlag8")) return field("Value", "System.Byte", 0, 1);
  if (!strcmp(type, "BeyondDynamicBone.VertexAttribute")) return field("Value", "System.Byte", 0, 1);
  if (!strcmp(type, "System.Boolean")) return bytes == 1;
  if (!strcmp(type, "System.Int16") || !strcmp(type, "System.UInt16")) return bytes == 2;
  return bytes == 4 && (!strcmp(type, "System.Int32") ||
      !strcmp(type, "System.UInt32") || !strcmp(type, "System.Single"));
}
static bool ClothInputCopyBox(void *box, const char *type, void *dest, size_t bytes) {
  if (!box) return false;
  void *cls = il2cpp_object_get_class(box);
  struct LayoutCache { void *cls; const char *type; size_t bytes; bool valid; };
  static LayoutCache cache[16]{};
  static unsigned count = 0;
  bool valid = false, found = false;
  for (unsigned n = 0; n < count; ++n)
    if (cache[n].cls == cls && cache[n].bytes == bytes && !strcmp(cache[n].type, type)) {
      found = true; valid = cache[n].valid; break;
    }
  if (!found) {
    valid = cls && CollisionType(il2cpp_class_get_type(cls), type) && ClothInputLayout(cls, type, bytes);
    if (count < 16) cache[count++] = {cls, type, bytes, valid};
  }
  if (!valid || (!strcmp(type, "System.Boolean") &&
      *reinterpret_cast<const unsigned char *>((char *)box + 16) > 1)) return false;
  memcpy(dest, (char *)box + 16, bytes);
  return true;
}
static bool ClothInputPoseRead(void *t, ClothInputPose &pose) {
  static void *method = nullptr;
  if (!method) method = ClothMethod(g_transformClass, "get_localToWorldMatrix", "UnityEngine.Matrix4x4");
  void *box = nullptr;
  pose.known = ClothInvoke(method, t, nullptr, box) &&
      ClothInputCopyBox(box, "UnityEngine.Matrix4x4", pose.matrix, sizeof(pose.matrix));
  if (pose.known)
    for (float value : pose.matrix) if (!std::isfinite(value)) pose.known = false;
  return pose.known;
}
template<class T> static bool ClothInputTeamField(void *box, const char *name,
                                                 const char *type, T &value) {
  auto cls = il2cpp_object_get_class(box);
  uint32_t align = 0;
  const int bytes = il2cpp_class_value_size(cls, &align);
  auto f = CollisionFieldInfo(cls, name, type);
  if (!f || bytes <= 0 || bytes > 2048) return false;
  const auto offset = il2cpp_field_get_offset(f);
  auto valueClass = il2cpp_class_from_type(il2cpp_field_get_type(f));
  if (offset < 16 || offset + sizeof(T) > size_t(bytes) + 16 || !valueClass ||
      il2cpp_class_value_size(valueClass, &align) != sizeof(T)) return false;
  return CollisionField(box, name, type, value);
}
static bool ClothInputAdd(void *t, const char *role) {
  if (!t || !ClothAnchorUnderOwner(t)) return false;
  int id = 0;
  if (!ClothValue(s_clothUnity.instance, t, id) || !id) return false;
  for (int n = 0; n < s_clothInput.count; ++n)
    if (s_clothInput.targets[n].transform.id.instance == id) return true;
  if (s_clothInput.count == ClothInputTargets) return false;
  auto &target = s_clothInput.targets[s_clothInput.count];
  target = {}; target.transform = ClothProtect(t);
  if (!target.transform.handle) return false;
  CollisionName(t, target.name, sizeof(target.name));
  strncpy_s(target.role, role, _TRUNCATE);
  ++s_clothInput.count;
  return true;
}
static bool ClothBoneOwnsPrivateBBC(const ClothRef &bbc);
static bool ClothInputIdentity(bool checkLists = true, const ClothInputBinding &s = s_clothInput) {
  if (!ClothOnMainThread() || !ClothOwns(s_cloth.owner) || (!s.privateBBC.handle&&(s.index<0||s.index>=s_cloth.count)) ||
      (s.privateBBC.handle&&!ClothBoneOwnsPrivateBBC(s.privateBBC)) || s.identity.session != s_cloth.owner.session ||
      s.identity.generation != s_cloth.owner.generation ||
      s.identity.owner != s_cloth.owner.character) return false;
  void *animator = ClothTarget(s_cloth.animator);
  int scene = 0;
  if (!animator || animator != g_cachedAnimator || !ClothScene(animator, scene) ||
      scene != s.identity.scene) return false;
  const auto &ref=s.privateBBC.handle?s.privateBBC:s_cloth.instances[s.index].ref;
  void *bbc = ClothTarget(ref), *process = nullptr, *serialize = nullptr;
  if(!bbc)return false;
  auto processMethod=s.privateBBC.handle?ClothMethod(il2cpp_object_get_class(bbc),"get_Process","BeyondDynamicBone.ClothProcess"):s_cloth.instances[s.index].api.process;
  auto serializeMethod=s.privateBBC.handle?ClothMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"):s_cloth.instances[s.index].api.serialize;
  int team = -1;
  if (!bbc || ref.id.instance != s.identity.cloth ||
      !ClothInvoke(processMethod, bbc, nullptr, process) ||
      !ClothInvoke(serializeMethod, bbc, nullptr, serialize) ||
      uintptr_t(process) != s.identity.process || uintptr_t(serialize) != s.identity.serialize ||
      !ClothValue(ClothMethod(il2cpp_object_get_class(process), "get_TeamId", "System.Int32"), process, team) ||
      team != s.identity.team) return false;
  if (!checkLists) return true;
  void *roots = nullptr, *colliders = nullptr;
  int rv = -1, cv = -1;
  return ClothField(serialize, "rootBones", "System.Collections.Generic.List<UnityEngine.Transform>", roots) &&
      ClothField(process, "colliderList", CollisionListType, colliders) &&
      uintptr_t(roots) == s.roots && uintptr_t(colliders) == s.colliders &&
      ClothField(roots, "_version", "System.Int32", rv) && rv == s.rootsVersion &&
      ClothField(colliders, "_version", "System.Int32", cv) && cv == s.collidersVersion;
}
#include "cloth_body_trace.h"
static bool ClothInputListFailure(const char *reason, int slot = -1) {
  auto &s = s_clothInput;
  Log("[CLOTH-INPUT-LIST] session=%llu frame=%d process=%p roots=%d slots=%d live=%d empty=%d "
      "rootVersion=%d colliderVersion=%d relatedSlot=%d reason=%s",
      (unsigned long long)s.identity.session, ClothFrame(), reinterpret_cast<void *>(s.identity.process),
      s.rootCount, s.colliderSlots, s.liveColliders, s.emptyColliderSlots,
      s.rootsVersion, s.collidersVersion, slot, reason);
  ClothInputIssue(reason, true); s.listFailure = true;
  return false;
}
static bool ClothInputCollectColliders(void *list) {
  auto &s = s_clothInput;
  s.colliderSlots = CollisionCount(list);
  s.liveColliders = s.emptyColliderSlots = 0;
  if (!list || s.colliderSlots < 0 || s.colliderSlots > 128)
    return ClothInputListFailure("collider-slot-list-unreadable-or-over-capacity");
  auto item = ClothMethod(il2cpp_object_get_class(list), "get_Item",
                          "BeyondDynamicBone.ColliderComponent", "System.Int32");
  if (!item) return ClothInputListFailure("collider-slot-getter-unavailable");
  for (int n = 0; n < s.colliderSlots; ++n) {
    void *c = nullptr, *args[] = {&n};
    if (!ClothInvoke(item, list, args, c))
      return ClothInputListFailure("collider-slot-read-failed", n);
    if (!c) { ++s.emptyColliderSlots; continue; }
    if (++s.liveColliders > 8)
      return ClothInputListFailure("live-collider-target-capacity-exceeded", n);
    if (!ClothAlive(c) || !ClothInputAdd(CollisionTransform(c), "collider-input"))
      return ClothInputListFailure("collider-outside-owner-or-invalid", n);
  }
  return true;
}
static bool ClothInputPrepare() {
  if (s_clothInput.identity.session && ClothInputIdentity()) return !s_clothInput.failed;
  const bool sameOwner = s_clothInput.identity.session && ClothInputIdentity(false);
  if (sameOwner) {
    if (s_clothInput.failed && !s_clothInput.listFailure) return false;
    if (!s_clothInput.remapBudget.Take(GetTickCount64())) {
      s_clothInput.listFailure = false;
      ClothInputIssue("list-remap-budget-exceeded", true); return false;
    }
  }
  const auto budget = sameOwner ? s_clothInput.remapBudget : eiem_cloth_input::RemapBudget{};
  ClothInputClear();
  s_clothInput.remapBudget = budget;
  if (!ClothOwns(s_cloth.owner) || !s_cloth.discovery.complete) return false;
  int selected = -1;
  for (int n = 0; n < s_cloth.count; ++n)
    if (!strcmp(s_cloth.instances[n].name, "MC_skirt")) {
      if (selected >= 0) { ClothInputIssue("ambiguous-MC_skirt"); return false; }
      selected = n;
    }
  if (selected < 0) return false;
  auto &i = s_cloth.instances[selected];
  ClothReadback r{};
  ClothRead(i, r);
  if (!r.process || !r.serialize || r.team <= 0 || !r.state.running) return false;
  s_clothInput.identity = {s_cloth.owner.session, s_cloth.owner.generation,
      s_cloth.owner.character, uintptr_t(r.process), uintptr_t(r.serialize),
      i.ref.id.instance, r.team, s_cloth.scene};
  s_clothInput.index = selected;
  void *roots = nullptr, *colliders = nullptr;
  if (!ClothField(r.serialize, "rootBones", "System.Collections.Generic.List<UnityEngine.Transform>", roots) ||
      !ClothField(r.process, "colliderList", CollisionListType, colliders) ||
      !ClothField(roots, "_version", "System.Int32", s_clothInput.rootsVersion) ||
      !ClothField(colliders, "_version", "System.Int32", s_clothInput.collidersVersion)) {
    ClothInputIssue("list-version-unavailable", true); return false;
  }
  s_clothInput.roots = uintptr_t(roots); s_clothInput.colliders = uintptr_t(colliders);
  const int rootCount = s_clothInput.rootCount = CollisionCount(roots);
  if (rootCount <= 0 || rootCount > 8)
    return ClothInputListFailure("root-list-empty-unreadable-or-over-capacity");
  for (int n = 0; n < rootCount; ++n)
    if (!ClothInputAdd(CollisionItem(roots, n, "UnityEngine.Transform"), "root-input")) {
      return ClothInputListFailure("root-outside-owner-or-invalid", n);
    }
  if (!ClothInputCollectColliders(colliders)) return false;
  void *animator = ClothTarget(s_cloth.animator);
  for (int n : {0, 1, 6, 7, 9, 10, 15, 19})
    ClothInputAdd(CollisionBody(animator, n), "body-reference");
  for (int n = 0; n < rootCount && n < 6 && s_clothInput.count < ClothInputTargets; ++n) {
    void *root = CollisionItem(roots, n, "UnityEngine.Transform");
    int children = -1, index = 0;
    void *child = nullptr, *args[] = {&index};
    if (root && ClothValue(s_clothUnity.childCount, root, children) && children == 1 &&
        ClothInvoke(s_clothUnity.child, root, args, child))
      ClothInputAdd(child, "cloth-output-observed");
  }
  ClothInputIssue("mapping-pending");
  Log("[CLOTH-INPUT-LIST] session=%llu frame=%d process=%p roots=%d slots=%d live=%d empty=%d "
      "rootVersion=%d colliderVersion=%d targets=%d reason=prepared-awaiting-native-input",
      (unsigned long long)s_clothInput.identity.session, ClothFrame(), r.process, rootCount,
      s_clothInput.colliderSlots, s_clothInput.liveColliders, s_clothInput.emptyColliderSlots,
      s_clothInput.rootsVersion, s_clothInput.collidersVersion, s_clothInput.count);
  return ClothInputIdentity();
}
static void ClothInputSubmitImpl(const char *stage, double sourceFrame) {
  if (!ClothOnMainThread() || !ClothOwns(s_cloth.owner)) return;
  ClothBoneSolverPoseSubmitted(stage,sourceFrame);
  LARGE_INTEGER begin{}, end{}, frequency{};
  QueryPerformanceFrequency(&frequency); QueryPerformanceCounter(&begin);
  if (s_clothInputHookInstaller) s_clothInputHookInstaller();
  if (!s_clothInputHooks || !ClothInputPrepare()) return;
  auto &s = s_clothInput;
  s.playheadFrame = sourceFrame;
  s.sourceFrame = g_motionBackend.Is(MotionBackend::DirectVmd) ? sourceFrame : NAN;
  s.submittedFrame = ClothFrame(); ++s.submission;
  strncpy_s(s.stage, stage, _TRUNCATE);
  ClothInputSample sample{};
  sample.sequence = ++s.sequence; sample.submission = s.submission;
  sample.frame = sample.submittedFrame = s.submittedFrame;
  sample.sourceFrame = s.sourceFrame; sample.playheadFrame = s.playheadFrame;
  sample.phase = !strcmp(stage, "Muscle.after-owner-FinalIK") ? 1 : 0;
  sample.count = s.count; sample.epoch = s.mapping.epoch;
  for (int n = 0; n < s.count; ++n) {
    void *t = ClothTarget(s.targets[n].transform);
    if (!t || !ClothInputPoseRead(t, sample.points[n].visible)) {
      ClothInputIssue("submitted-transform-invalid", true); return;
    }
  }
  if (!ClothInputIdentity()) return;
  QueryPerformanceCounter(&end);
  sample.costMs = double(end.QuadPart - begin.QuadPart) * 1000 / frequency.QuadPart;
  s.body.Push(sample);
  ClothBodyCapture(sample);
  if (s.bodyBudget.Observe(sample.costMs)) ClothInputIssue("observer-cost-budget-exceeded", true);
}
static void ClothInputSubmit(const char *stage, double sourceFrame) {
  __try { ClothInputSubmitImpl(stage, sourceFrame); }
  __except (EXCEPTION_EXECUTE_HANDLER) { ClothInputIssue("submission-observer-fault", true); }
}

struct ClothInputArray { void *object = nullptr, *item = nullptr; int length = 0; };
struct ClothInputChunk { int start = 0, count = 0; };
static bool ClothInputChunkRead(void *box, const char *name, int length, ClothInputChunk &chunk, int maximum=128) {
  auto field = CollisionFieldInfo(il2cpp_object_get_class(box), name, "BeyondDynamicBone.DataChunk");
  auto cls = field ? il2cpp_class_from_type(il2cpp_field_get_type(field)) : nullptr;
  uint32_t alignment = 0;
  return cls && il2cpp_class_value_size(cls, &alignment) == sizeof(chunk) &&
      ClothValueOffset(cls, "startIndex", "System.Int32", 8, 4) == 0 &&
      ClothValueOffset(cls, "dataLength", "System.Int32", 8, 4) == 4 &&
      CollisionField(box, name, "BeyondDynamicBone.DataChunk", chunk) &&
      chunk.start >= 0 && maximum>0 && maximum<=4096 && chunk.count >= 0 && chunk.count <= maximum &&
      chunk.start <= length && chunk.count <= length - chunk.start;
}
static bool ClothInputArrayOpen(void *manager, const char *field, const char *type, ClothInputArray &a) {
  char container[192]{};
  _snprintf_s(container, _TRUNCATE, "BeyondDynamicBone.ExNativeArray<%s>", type);
  if (!CollisionField(manager, field, container, a.object) || !a.object) return false;
  void *cls = il2cpp_object_get_class(a.object);
  bool valid = false;
  a.item = ClothInputMethod(cls, "get_Item", type, "System.Int32");
  return a.item && ClothValue(ClothMethod(cls, "get_IsValid", "System.Boolean"), a.object, valid) && valid &&
      ClothValue(ClothMethod(cls, "get_Length", "System.Int32"), a.object, a.length) &&
      a.length > 0 && a.length <= 65536;
}
static void *ClothInputArrayBox(const ClothInputArray &a, int index) {
  if (index < 0 || index >= a.length) return nullptr;
  void *box = nullptr, *args[] = {&index};
  return ClothInvoke(a.item, a.object, args, box) ? box : nullptr;
}
static bool ClothInputArrayValue(const ClothInputArray &a, int index, const char *type,
                                 void *data, size_t bytes) {
  return ClothInputCopyBox(ClothInputArrayBox(a, index), type, data, bytes);
}
static int ClothInputStaticBool(const char *name) {
  auto f = CollisionFieldInfo(s_clothInputManagerClass, name, "System.Boolean");
  if (!f || !(il2cpp_field_get_flags(f) & 0x10)) return -1;
  unsigned char data[8]{}; il2cpp_field_static_get_value(f, data);
  return data[0] <= 1 ? data[0] : -1;
}
#include "cloth_contact_trace.h"
static void ClothInputCompletedImpl(void *manager) {
  auto &s = s_clothInput;
  if (!ClothInputIdentity() || s.failed || !s.count) return;
  const int frame = ClothFrame();
  if (frame < 0 || (s.readFrame >= 0 && frame - s.readFrame < 4)) return;
  s.readFrame = frame;
  LARGE_INTEGER begin{}, end{}, frequency{};
  QueryPerformanceFrequency(&frequency); QueryPerformanceCounter(&begin);
  void *cls = il2cpp_object_get_class(manager);
  void *field = CollisionFieldInfo(cls, "transformAccessArray", "UnityEngine.Jobs.TransformAccessArray");
  void *accessClass = field ? il2cpp_class_from_type(il2cpp_field_get_type(field)) : nullptr;
  uint32_t align = 0;
  uintptr_t access = 0;
  if (!accessClass || il2cpp_class_value_size(accessClass, &align) != sizeof(access) ||
      ClothValueOffset(accessClass, "m_TransformArray", "System.IntPtr", sizeof(access), sizeof(access)) != 0 ||
      !CollisionField(manager, "transformAccessArray", "UnityEngine.Jobs.TransformAccessArray", access) || !access) {
    ClothInputIssue("TransformAccessArray-ABI-unavailable", true); return;
  }
  void *getLength = ClothMethod(accessClass, "get_length", "System.Int32");
  void *getItem = ClothMethod(accessClass, "get_Item", "UnityEngine.Transform", "System.Int32");
  int length = 0;
  if (!getItem || !ClothValue(getLength, &access, length) || length <= 0 || length > 8192) {
    ClothInputIssue("TransformAccessArray-count-or-getter-unavailable", true); return;
  }
  const auto oldMapping = s.mapping;
  if (s.mapping.Refresh(uintptr_t(manager), access, length)) {
    Log("[CLOTH-INPUT-MAPPING] session=%llu frame=%d reason=storage-changed manager=%p->%p access=%p->%p length=%d->%d epoch=%llu->%llu ownerIdentity=1",
        (unsigned long long)s.identity.session, frame, (void *)oldMapping.manager, manager,
        (void *)oldMapping.access, (void *)access, oldMapping.length, length,
        (unsigned long long)oldMapping.epoch, (unsigned long long)s.mapping.epoch);
    for (auto &t : s.targets) { t.slot = -1; t.duplicate = false; }
  }
  ClothInputArray teams{}, flags{}, positions{}, rotations{}, scales{}, matrices{};
  if (!ClothInputArrayOpen(manager, "teamIdArray", "System.Int16", teams) ||
      !ClothInputArrayOpen(manager, "flagArray", "BeyondDynamicBone.ExBitFlag8", flags) ||
      !ClothInputArrayOpen(manager, "positionArray", "Unity.Mathematics.double3", positions) ||
      !ClothInputArrayOpen(manager, "rotationArray", "Unity.Mathematics.quaternion", rotations) ||
      !ClothInputArrayOpen(manager, "scaleArray", "Unity.Mathematics.float3", scales) ||
      !ClothInputArrayOpen(manager, "localToWorldMatrixArray", "Unity.Mathematics.float4x4", matrices)) {
    ClothInputIssue("completed-input-public-getter-unavailable", true); return;
  }
  void *teamManager = nullptr, *process = nullptr;
  ClothInputArray teamData{};
  int team = s.identity.team;
  void *teamArgs[] = {&team};
  if (!ClothInvoke(ClothMethod(s_clothInputManagerClass, "get_Team", "BeyondDynamicBone.TeamManager", nullptr, true), nullptr, nullptr, teamManager) ||
      !teamManager || !ClothInvoke(ClothMethod(il2cpp_object_get_class(teamManager), "GetClothProcess", "BeyondDynamicBone.ClothProcess", "System.Int32"), teamManager, teamArgs, process) ||
      uintptr_t(process) != s.identity.process ||
      !ClothInputArrayOpen(teamManager, "teamDataArray", "BeyondDynamicBone.TeamManager.TeamData", teamData)) {
    ClothInputIssue("team-process-identity-or-getter-unavailable", true); return;
  }
  void *teamBox = ClothInputArrayBox(teamData, team);
  if (!teamBox) { ClothInputIssue("team-read-failed", true); return; }
  ClothInputChunk chunks[2]{};
  if (!ClothInputChunkRead(teamBox, "proxyTransformChunk", length, chunks[0]) ||
      !ClothInputChunkRead(teamBox, "colliderTransformChunk", length, chunks[1]) ||
      chunks[0].count + chunks[1].count > 128) {
    ClothInputIssue("owner-transform-chunks-unavailable-or-over-budget", true); return;
  }
  int previous[ClothInputTargets]{};
  bool previousDuplicate[ClothInputTargets]{};
  for (int n = 0; n < s.count; ++n) {
    previous[n] = s.targets[n].slot; previousDuplicate[n] = s.targets[n].duplicate;
    s.targets[n].slot = -1; s.targets[n].duplicate = false;
  }
  for (const auto &chunk : chunks) for (int offset = 0; offset < chunk.count; ++offset) {
    const int slot = chunk.start + offset;
    int16_t slotTeam = 0;
    if (!ClothInputArrayValue(teams, slot, "System.Int16", &slotTeam, sizeof(slotTeam))) {
      ClothInputIssue("slot-team-read-failed", true); return;
    }
    if (slotTeam != s.identity.team) continue;
    int idx = slot;
    void *t = nullptr, *args[] = {&idx};
    if (!ClothInvoke(getItem, &access, args, t) || !t) continue;
    int id = 0;
    if (!ClothValue(s_clothUnity.instance, t, id)) continue;
    for (int n = 0; n < s.count; ++n) {
      auto &target = s.targets[n];
      if (!strcmp(target.role, "cloth-output-observed")) continue;
      if (id != target.transform.id.instance) continue;
      eiem_cloth_input::MatchSlot(s.identity.team, slotTeam,
          uintptr_t(ClothTarget(target.transform)), target.transform.id.instance,
          uintptr_t(t), id, slot, target.slot, target.duplicate);
    }
  }
  for (int n = 0; n < s.count; ++n)
    if (previous[n] != s.targets[n].slot || previousDuplicate[n] != s.targets[n].duplicate) {
      ++s.mapping.epoch;
      Log("[CLOTH-INPUT-MAPPING] session=%llu frame=%d reason=owner-slot-map-revalidated target=%s slot=%d->%d duplicate=%d->%d epoch=%llu",
          (unsigned long long)s.identity.session, frame, s.targets[n].name, previous[n], s.targets[n].slot,
          previousDuplicate[n], s.targets[n].duplicate, (unsigned long long)s.mapping.epoch);
      break;
    }
  s.mapping.cursor = length;
  ClothInputSample sample{};
  sample.sequence = ++s.sequence; sample.submission = s.submission;
  sample.frame = frame; sample.submittedFrame = s.submittedFrame;
  sample.sourceFrame = s.sourceFrame; sample.playheadFrame = s.playheadFrame;
  sample.epoch = s.mapping.epoch;
  sample.phase = 2; sample.count = s.count;
  sample.teamKnown = ClothInputTeamField(teamBox, "useRelativeTransform", "System.Int32", sample.relative) &&
      ClothInputTeamField(teamBox, "relativeTransformPos", "Unity.Mathematics.float3", sample.relativePosition) &&
      ClothInputTeamField(teamBox, "relativeTransformRot", "Unity.Mathematics.quaternion", sample.relativeRotation) &&
      ClothInputTeamField(teamBox, "flag", "Unity.Collections.BitField64", sample.teamFlags) &&
      ClothValue(ClothInputMethod(il2cpp_object_get_class(teamBox), "get_IsCullingInvisible", "System.Boolean"), (char *)teamBox + 16, sample.culled);
  for (int n = 0; n < s.count; ++n) {
    auto &target = s.targets[n]; auto &point = sample.points[n];
    void *t = ClothTarget(target.transform);
    if (!t || !ClothAnchorUnderOwner(t) || !ClothInputPoseRead(t, point.visible)) {
      ClothInputIssue("target-invalid-at-completed-read", true); return;
    }
    if (target.slot < 0 || target.duplicate || s.mapping.cursor < length) continue;
    int idx = target.slot;
    void *current = nullptr, *args[] = {&idx}; int16_t slotTeam = 0;
    if (!ClothInvoke(getItem, &access, args, current) || current != t ||
        !ClothInputArrayValue(teams, idx, "System.Int16", &slotTeam, sizeof(slotTeam)) || slotTeam != team) {
      s.mapping.Invalidate();
      ClothInputIssue("slot-replaced-remapping"); return;
    }
    unsigned char flag = 0;
    point.slot = idx;
    point.inputKnown = ClothInputArrayValue(flags, idx, "BeyondDynamicBone.ExBitFlag8", &flag, sizeof(flag)) &&
        ClothInputArrayValue(positions, idx, "Unity.Mathematics.double3", point.position, sizeof(point.position)) &&
        ClothInputArrayValue(rotations, idx, "Unity.Mathematics.quaternion", point.rotation, sizeof(point.rotation)) &&
        ClothInputArrayValue(scales, idx, "Unity.Mathematics.float3", point.scale, sizeof(point.scale)) &&
        ClothInputArrayValue(matrices, idx, "Unity.Mathematics.float4x4", point.matrix, sizeof(point.matrix));
    point.flags = flag;
    if (!point.inputKnown) { ClothInputIssue("mapped-input-read-failed", true); return; }
  }
  if (!ClothInputIdentity()) { ClothInputIssue("identity-changed-during-read", true); return; }
  QueryPerformanceCounter(&end);
  sample.costMs = double(end.QuadPart - begin.QuadPart) * 1000 / frequency.QuadPart;
  s.inputs.Push(sample); ++s.completedBoundaries;
  if (s.inputBudget.Observe(sample.costMs)) {
    ClothInputIssue("observer-cost-budget-exceeded", true); return;
  }
  ClothInputIssue(s.mapping.cursor < length ? "mapping-pending" : "completed-input-captured-semantics-pending");
  ClothContactCapture(sample, teamBox, manager, &access, getItem);
}
static void ClothInputCompleted(void *manager, void *caller) {
  if (!ClothOnMainThread() || !s_clothInputHooks || !s_clothInputUpdateDepth ||
      !s_cloth.active) return;
  __try {
    const bool fingerprint = s_clothInputUpdateCode &&
        eiem_cloth_input::Fingerprint(s_clothInputUpdateCode, ClothInputAuditedBytes) == s_clothInputPatchedFingerprint;
    if (eiem_cloth_input::CanRead(true, ClothOwns(s_cloth.owner), s_clothInputUpdateDepth == 1,
            caller == s_clothInputCallsite, ClothInputStaticBool("UseAnimatorTransform"),
            ClothInputStaticBool("UseCrossFrameJob"), fingerprint)) {
      ClothBoneSolverCompleted(manager);
      if (!s_clothInput.identity.session || s_clothInput.failed) return;
      ClothInputCompletedImpl(manager);
    }
    else if (s_clothInput.identity.session)
      ClothInputIssue("native-branch-callsite-or-version-not-eligible");
  } __except (EXCEPTION_EXECUTE_HANDLER) { ClothInputIssue("completed-input-observer-fault", true); }
}
static void ClothSurfaceBeforeTeam(void *self, void *caller) {
  if (!ClothOnMainThread() || !s_clothSurfaceHook || !s_clothInputHooks ||
      s_clothInputUpdateDepth != 1 || s_clothSurfaceAtBoundary ||
      caller != s_clothSurfaceTeamCallsite || (!ClothBonePending()&&!ClothPrefetchNeedsHooks())) return;
  __try {
    const bool fingerprint = s_clothInputUpdateCode && s_clothSurfaceTeamCode &&
        eiem_cloth_input::Fingerprint(s_clothInputUpdateCode, ClothInputAuditedBytes) == s_clothInputPatchedFingerprint &&
        eiem_cloth_input::Fingerprint(s_clothSurfaceTeamCode, 128) == s_clothSurfaceTeamFingerprint;
    void *team = nullptr;
    if (!eiem_cloth_surface::CanMutate(true, s_clothInputUpdateDepth, true,
          ClothInputStaticBool("UseAnimatorTransform"), ClothInputStaticBool("UseCrossFrameJob"),
          fingerprint, self != nullptr)) return;
    if (!ClothInvoke(ClothMethod(s_clothInputManagerClass, "get_Team", "BeyondDynamicBone.TeamManager", nullptr, true),
                     nullptr, nullptr, team) || team != self) return;
    s_clothSurfaceAtBoundary = true;
    __try { ClothPrefetchBoundary();if(ClothBonePending())ClothBoneBoundary(); }
    __finally { s_clothSurfaceAtBoundary = false; }
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    ClothBoneRelease("lifetime-boundary-native-exception");
  }
}
static void __fastcall ClothSurfaceNativeTeamUpdate(void *self, void *method) {
  ClothSurfaceBeforeTeam(self, _ReturnAddress());
  s_clothSurfaceOriginalTeamUpdate(self, method);
}
static void __fastcall ClothInputNativeUpdate(void *self, void *method) {
  if (!ClothOnMainThread()) { s_clothInputOriginalUpdate(self, method); return; }
  ++s_clothInputUpdateDepth;
  __try { s_clothInputOriginalUpdate(self, method); }
  __finally { --s_clothInputUpdateDepth; }
}
static ClothInputJobHandle *__fastcall ClothInputNativeValid(ClothInputJobHandle *result,
    void *self, const ClothInputJobHandle *dependency, void *method) {
  ClothInputCompleted(self, _ReturnAddress());
  return s_clothInputOriginalValid(result, self, dependency, method);
}
