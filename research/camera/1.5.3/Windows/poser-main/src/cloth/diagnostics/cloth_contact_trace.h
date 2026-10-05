#pragma once
#include "../bonecloth/cloth_bonecloth_limits.h"
constexpr int ClothContactParticles = ClothBoneMaxParticles, ClothContactColliders = 16;
struct ClothContactParticle {
  int particle = -1, proxy = -1;
  unsigned char attribute = 0;
  float depth = 0, friction = 0, normal[3]{};
  double next[3]{}, display[3]{};
};
struct ClothContactCollider {
  int id = 0, transform = 0, slot = -1, transformSlot = -1;
  char componentType[64]{};
  unsigned char flags = 0;
  float center[3]{}, size[3]{}, radius[2]{};
  double oldEndpoints[6]{}, nextEndpoints[6]{};
};
struct ClothContactOutput {
  int id = 0, parentId = 0, parentIndex = -1, slot = -1, proxy = -1;
  unsigned char flags = 0;
  char name[96]{};
  double proxyPosition[3]{}, lastPosition[3]{};
  float proxyRotation[4]{}, lastRotation[4]{}, lastLocalPosition[3]{}, lastLocalRotation[4]{};
  float localPosition[3]{}, localRotation[4]{}, worldRotation[4]{};
  bool localInputKnown = false;
  float inputLocalPosition[3]{}, inputLocalRotation[4]{};
  ClothInputPose visible{};
};
struct ClothContactSample {
  int frame = -1, particles = 0, colliders = 0, reportedColliders = 0;
  uint64_t submission = 0, epoch = 0, teamFlags = 0;
  float simulateWeight = 0, blendWeight = 0, lodWeight = 0;
  double costMs = 0;
  unsigned char validMask = 0, enableMask = 0, moveMask = 0, disableMask = 0;
  bool outputKnown = false;
  unsigned char worldWriteMask = 0, localWriteMask = 0, outputEnableMask = 0;
  ClothContactOutput outputs[ClothContactParticles]{};
  ClothContactParticle points[ClothContactParticles]{};
  ClothContactCollider shapes[ClothContactColliders]{};
};
struct ClothContactTrace {
  eiem_cloth_input::Identity identity{};
  eiem_cloth_input::Ring<ClothContactSample, 32> samples;
  eiem_cloth_input::CostBudget budget{};
  int lastFrame = -1;
  bool failed = false, outputFailed = false;
  char issue[128]{"awaiting-support-and-completed-input"};
};
static ClothActorBank<ClothContactTrace> s_clothContactActors;
#define s_clothContact (s_clothContactActors.Get())
static void ClothContactClear() {
  if (!ClothOnMainThread()) return;
  auto &s = s_clothContact;
  s.identity = {}; s.samples.Clear(); s.budget = {}; s.lastFrame = -1; s.failed = false; s.outputFailed = false;
  strcpy_s(s.issue, "awaiting-support-and-completed-input");
}
static bool ClothContactFail(const char *reason, ClothContactTrace &s = s_clothContact,
                             const ClothInputBinding &binding = s_clothInput) {
  if (!s.failed) Log("[CLOTH-SOLVER-TRACE] session=%llu instance=%d team=%d frame=%d issue=%s readOnly=1 baseSimulationRetained=1",
      (unsigned long long)binding.identity.session, binding.identity.cloth, binding.identity.team, ClothFrame(), reason);
  s.failed = true; strncpy_s(s.issue, reason, _TRUNCATE);
  return false;
}
static bool ClothContactMask(void *cls, const char *name, unsigned char &value) {
  auto f = cls ? CollisionFieldInfo(cls, name, "System.Byte") : nullptr;
  if (!f || !il2cpp_field_get_flags || !il2cpp_field_static_get_value ||
      (il2cpp_field_get_flags(f) & 0x50) != 0x50) return false;
  il2cpp_field_static_get_value(f, &value);
  return value && !(value & (value - 1));
}
template<class T> static bool ClothContactField(void *box, const char *name, const char *type, T &value) {
  if (!box) return false;
  auto f = CollisionFieldInfo(il2cpp_object_get_class(box), name, type);
  auto cls = f ? il2cpp_class_from_type(il2cpp_field_get_type(f)) : nullptr;
  return cls && ClothInputLayout(cls, type, sizeof(T)) && ClothInputTeamField(box, name, type, value);
}
static bool ClothContactManager(const char *name, const char *type, void *&value) {
  return ClothInvoke(ClothMethod(s_clothInputManagerClass, name, type, nullptr, true), nullptr, nullptr, value) && value;
}
static bool ClothContactArrays(void *manager, ClothInputArray *arrays, const char *const *names,
                               const char *const *types, int count) {
  for (int n = 0; n < count; ++n)
    if (!ClothInputArrayOpen(manager, names[n], types[n], arrays[n])) return false;
  return true;
}
static bool ClothContactRange(const ClothInputChunk &chunk, const ClothInputArray *arrays,
                              int count, int maximum) {
  if (chunk.start < 0 || chunk.count <= 0 || chunk.count > maximum) return false;
  for (int n = 0; n < count; ++n)
    if (chunk.start > arrays[n].length || chunk.count > arrays[n].length - chunk.start) return false;
  return true;
}
static bool ClothContactRead(const ClothInputSample &input, void *teamBox, ClothContactSample &sample,
                             const ClothInputBinding &binding = s_clothInput,
                             ClothContactTrace &trace = s_clothContact) {
  sample.frame = input.frame; sample.submission = input.submission; sample.epoch = input.epoch;
  sample.teamFlags = input.teamFlags;
  void *simulation = nullptr, *mesh = nullptr, *collider = nullptr;
  if (!ClothContactManager("get_Simulation", "BeyondDynamicBone.SimulationManager", simulation) ||
      !ClothContactManager("get_VMesh", "BeyondDynamicBone.VirtualMeshManager", mesh) ||
      !ClothContactManager("get_Collider", "BeyondDynamicBone.ColliderManager", collider))
    return ClothContactFail("manager-getter-unavailable", trace, binding);
  ClothInputArray pa[5]{}, va[3]{}, ca[5]{};
  const char *pn[]{"teamIdArray", "nextPosArray", "dispPosArray", "frictionArray", "collisionNormalArray"};
  const char *pt[]{"System.Int16", "Unity.Mathematics.double3", "Unity.Mathematics.double3", "System.Single", "Unity.Mathematics.float3"};
  const char *vn[]{"teamIds", "attributes", "vertexDepths"};
  const char *vt[]{"System.Int16", "BeyondDynamicBone.VertexAttribute", "System.Single"};
  const char *cn[]{"teamIdArray", "flagArray", "centerArray", "sizeArray", "workDataArray"};
  const char *ct[]{"System.Int16", "BeyondDynamicBone.ExBitFlag8", "Unity.Mathematics.float3", "Unity.Mathematics.float3", "BeyondDynamicBone.ColliderManager.WorkData"};
  if (!ClothContactArrays(simulation, pa, pn, pt, 5) || !ClothContactArrays(mesh, va, vn, vt, 3) ||
      !ClothContactArrays(collider, ca, cn, ct, 5)) return ClothContactFail("completed-array-public-getter-unavailable", trace, binding);
  ClothInputChunk pc{}, vc{}, cc{}, tc{};
  if (!ClothInputChunkRead(teamBox, "particleChunk", pa[0].length, pc, ClothContactParticles) ||
      !ClothInputChunkRead(teamBox, "proxyCommonChunk", va[0].length, vc, ClothContactParticles) ||
      !ClothInputChunkRead(teamBox, "colliderChunk", ca[0].length, cc) ||
      !ClothInputChunkRead(teamBox, "colliderTransformChunk", binding.mapping.length, tc) ||
      !ClothContactRange(pc, pa, 5, ClothContactParticles) || !ClothContactRange(vc, va, 3, ClothContactParticles) ||
      !ClothContactRange(cc, ca, 5, 128) || pc.count != vc.count || cc.count != tc.count ||
      !ClothInputTeamField(teamBox, "colliderCount", "System.Int32", sample.reportedColliders) ||
      sample.reportedColliders != binding.liveColliders || sample.reportedColliders > ClothContactColliders)
    return ClothContactFail("owner-chunk-count-or-array-range-mismatch", trace, binding);
  if (!ClothInputTeamField(teamBox, "clothSimulateWeight", "System.Single", sample.simulateWeight) ||
      !ClothInputTeamField(teamBox, "blendWeight", "System.Single", sample.blendWeight) ||
      !ClothInputTeamField(teamBox, "clothLodFadeWeight", "System.Single", sample.lodWeight))
    return ClothContactFail("team-effective-weight-unavailable", trace, binding);
  void *attributeBox = ClothInputArrayBox(va[1], vc.start);
  void *attributeClass = attributeBox ? il2cpp_object_get_class(attributeBox) : nullptr;
  if (!ClothContactMask(il2cpp_object_get_class(collider), "Flag_Valid", sample.validMask) ||
      !ClothContactMask(il2cpp_object_get_class(collider), "Flag_Enable", sample.enableMask) ||
      !ClothContactMask(attributeClass, "Flag_Move", sample.moveMask) ||
      !ClothContactMask(attributeClass, "Flag_DisableCollision", sample.disableMask))
    return ClothContactFail("native-flag-literals-unavailable", trace, binding);
  for (int n = 0; n < pc.count; ++n) {
    auto &p = sample.points[n]; p.particle = pc.start+n; p.proxy = vc.start+n;
    int16_t particleTeam = 0, proxyTeam = 0;
    if (!ClothInputArrayValue(pa[0], p.particle, pt[0], &particleTeam, sizeof(particleTeam)) ||
        !ClothInputArrayValue(va[0], p.proxy, vt[0], &proxyTeam, sizeof(proxyTeam)) ||
        particleTeam != binding.identity.team || proxyTeam != particleTeam ||
        !ClothInputArrayValue(va[1], p.proxy, vt[1], &p.attribute, sizeof(p.attribute)) ||
        !ClothInputArrayValue(va[2], p.proxy, vt[2], &p.depth, sizeof(p.depth)) ||
        !ClothInputArrayValue(pa[1], p.particle, pt[1], p.next, sizeof(p.next)) ||
        !ClothInputArrayValue(pa[2], p.particle, pt[2], p.display, sizeof(p.display)) ||
        !ClothInputArrayValue(pa[3], p.particle, pt[3], &p.friction, sizeof(p.friction)) ||
        !ClothInputArrayValue(pa[4], p.particle, pt[4], p.normal, sizeof(p.normal)))
      return ClothContactFail("particle-proxy-identity-or-value-read-failed", trace, binding);
    ++sample.particles;
  }
  void *list = reinterpret_cast<void *>(binding.colliders);
  if (CollisionCount(list) != cc.count) return ClothContactFail("process-collider-slot-range-mismatch", trace, binding);
  auto item = ClothMethod(il2cpp_object_get_class(list), "get_Item", "BeyondDynamicBone.ColliderComponent", "System.Int32");
  for (int n = 0; n < cc.count; ++n) {
    void *c = nullptr, *args[]{&n};
    if (!ClothInvoke(item, list, args, c)) return ClothContactFail("process-collider-slot-read-failed", trace, binding);
    if (!c) continue;
    if (sample.colliders == ClothContactColliders) return ClothContactFail("live-collider-capacity-exceeded", trace, binding);
    auto &shape = sample.shapes[sample.colliders];
    strncpy_s(shape.componentType, il2cpp_class_get_name(il2cpp_object_get_class(c)), _TRUNCATE);
    auto transform = CollisionTransform(c);
    if (!ClothAlive(c) || !ClothAnchorUnderOwner(transform) ||
        !ClothValue(s_clothUnity.instance, c, shape.id) ||
        !ClothValue(s_clothUnity.instance, transform, shape.transform)) return ClothContactFail("collider-object-identity-unavailable", trace, binding);
    shape.slot = cc.start+n; shape.transformSlot = tc.start+n;
    bool mapped = false;
    for (int k = 0; k < binding.count; ++k) {
      const auto &target = binding.targets[k];
      if (!strcmp(target.role, "collider-input") && target.transform.id.instance == shape.transform &&
          ClothTarget(target.transform) == transform && !target.duplicate && input.points[k].inputKnown &&
          input.points[k].slot == shape.transformSlot) mapped = true;
    }
    int16_t colliderTeam = 0;
    if (!mapped || !ClothInputArrayValue(ca[0], shape.slot, ct[0], &colliderTeam, sizeof(colliderTeam)) ||
        colliderTeam != binding.identity.team ||
        !ClothInputArrayValue(ca[1], shape.slot, ct[1], &shape.flags, sizeof(shape.flags)) ||
        !ClothInputArrayValue(ca[2], shape.slot, ct[2], shape.center, sizeof(shape.center)) ||
        !ClothInputArrayValue(ca[3], shape.slot, ct[3], shape.size, sizeof(shape.size)))
      return ClothContactFail("collider-transform-team-or-value-mismatch", trace, binding);
    auto work = ClothInputArrayBox(ca[4], shape.slot);
    if (!ClothContactField(work, "radius", "Unity.Mathematics.float2", shape.radius) ||
        !ClothContactField(work, "oldPos", "Unity.Mathematics.double3x2", shape.oldEndpoints) ||
        !ClothContactField(work, "nextPos", "Unity.Mathematics.double3x2", shape.nextEndpoints))
      return ClothContactFail("collider-work-data-layout-or-read-unavailable", trace, binding);
    ++sample.colliders;
  }
  return sample.colliders == sample.reportedColliders && ClothInputIdentity(true, binding);
}

static bool ClothContactOutputMapping(const ClothContactSample &sample) {
  if (sample.particles <= 0 || sample.particles > ClothContactParticles) return false;
  const int start = sample.outputs[0].slot;
  if (start < 0 || start > INT_MAX-sample.particles) return false;
  for (int n = 0; n < sample.particles; ++n) {
    const auto &o = sample.outputs[n];
    if (!o.id || !o.parentId || o.slot != start+n || o.proxy != sample.points[n].proxy ||
        o.parentIndex < -1 || o.parentIndex >= sample.particles || o.parentIndex == n) return false;
    if (o.parentIndex >= 0 && sample.outputs[o.parentIndex].id != o.parentId) return false;
    for (int k = 0; k < n; ++k) if (sample.outputs[k].id == o.id) return false;
  }
  return true;
}
static bool ClothContactLocalInputs(void *manager, void *teamBox,
                                    const ClothInputChunk &chunk, ClothContactSample &sample) {
  if (!manager || sample.particles<1 || sample.particles>ClothContactParticles) return false;
  bool culled = true;
  if (!teamBox || !ClothValue(ClothInputMethod(il2cpp_object_get_class(teamBox),
      "get_IsLODCulled", "System.Boolean"), static_cast<char *>(teamBox)+16, culled)) return false;
  for (int n=0; n<sample.particles; ++n) sample.outputs[n].localInputKnown=false;
  if (culled) return true;
  ClothInputArray arrays[2]{};
  const char *names[]{"localPositionArray", "localRotationArray"};
  const char *types[]{"Unity.Mathematics.float3", "Unity.Mathematics.quaternion"};
  unsigned char readMask=0;
  if (!ClothContactMask(il2cpp_object_get_class(manager), "Flag_Read", readMask) ||
      !ClothContactArrays(manager, arrays, names, types, 2) ||
      !ClothContactRange(chunk, arrays, 2, ClothContactParticles+1)) return false;
  for (int n=0; n<sample.particles; ++n) {
    auto &o=sample.outputs[n];
    if (o.slot<chunk.start || o.slot-chunk.start>=chunk.count) return false;
    if (!(o.flags&readMask) || !(o.flags&sample.outputEnableMask)) continue;
    if (!ClothInputArrayValue(arrays[0], o.slot, types[0], o.inputLocalPosition, sizeof(o.inputLocalPosition)) ||
        !ClothInputArrayValue(arrays[1], o.slot, types[1], o.inputLocalRotation, sizeof(o.inputLocalRotation))) return false;
    o.localInputKnown=true;
  }
  return true;
}
struct ClothContactLocalPoseError {
  double rawDistanceSquared=0, distanceSquared=0, rotationDot=0;
};
static bool ClothContactLocalPoseMatches(const ClothContactOutput &o,float weight,
                                        ClothContactLocalPoseError &error) {
  error={};
  if (!std::isfinite(weight) || weight<0 || (weight<1 && !o.localInputKnown)) return false;
  const auto norm=[](const float *q) {
    double value=0;for(int k=0;k<4;++k)value+=double(q[k])*q[k];return value;
  };
  const auto unit=[](double n) {return std::isfinite(n) && n>=.99 && n<=1.01;};
  const double actualNorm=norm(o.localRotation),targetNorm=norm(o.lastLocalRotation);
  if (!unit(actualNorm) || !unit(targetNorm) || (weight<1 && !unit(norm(o.inputLocalRotation)))) return false;
  for(int k=0;k<3;++k) {
    const double actual=o.localPosition[k],target=o.lastLocalPosition[k];
    const double expected=weight>=1?target:double(o.inputLocalPosition[k])+(target-o.inputLocalPosition[k])*weight;
    if (!std::isfinite(actual) || !std::isfinite(target) || !std::isfinite(expected)) return false;
    error.rawDistanceSquared+=(actual-target)*(actual-target);
    error.distanceSquared+=(actual-expected)*(actual-expected);
  }
  double expected[4]{};
  if(weight>=1) for(int k=0;k<4;++k) expected[k]=o.lastLocalRotation[k];
  else {
    double dot=0;for(int k=0;k<4;++k)dot+=double(o.inputLocalRotation[k])*o.lastLocalRotation[k];
    const double sign=dot<0?-1:1;dot=std::abs(dot);
    double from=1-weight,to=weight;
    if(dot<.9995) {
      const double angle=std::acos(dot),denominator=std::sin(angle);
      from=std::sin((1-weight)*angle)/denominator;to=std::sin(weight*angle)/denominator;
    }
    for(int k=0;k<4;++k)expected[k]=from*o.inputLocalRotation[k]+to*sign*o.lastLocalRotation[k];
  }
  double dot=0,expectedNorm=0;
  for(int k=0;k<4;++k){dot+=double(o.localRotation[k])*expected[k];expectedNorm+=expected[k]*expected[k];}
  if(!std::isfinite(expectedNorm) || expectedNorm<=0) return false;
  error.rotationDot=std::abs(dot/std::sqrt(actualNorm*expectedNorm));
  return std::isfinite(error.rotationDot) && error.distanceSquared<=.0002*.0002 && error.rotationDot>=.99999;
}
static bool ClothContactOutputRead(void *teamBox, void *manager, void *access,
                                   void *getItem, ClothContactSample &sample,
                                   const ClothInputBinding &binding = s_clothInput) {
  if (!manager || !access || !getItem) return false;
  void *mesh = nullptr;
  if (!ClothContactManager("get_VMesh", "BeyondDynamicBone.VirtualMeshManager", mesh)) return false;
  ClothInputArray va[4]{}, ta[6]{};
  const char *vn[]{"teamIds", "positions", "rotations", "vertexParentIndices"};
  const char *vt[]{"System.Int16", "Unity.Mathematics.double3", "Unity.Mathematics.quaternion", "System.Int32"};
  const char *tn[]{"teamIdArray", "flagArray", "lastpositionArray", "lastrotationArray", "lastlocalPositionArray", "lastlocalRotationArray"};
  const char *tt[]{"System.Int16", "BeyondDynamicBone.ExBitFlag8", "Unity.Mathematics.double3", "Unity.Mathematics.quaternion", "Unity.Mathematics.float3", "Unity.Mathematics.quaternion"};
  ClothInputChunk vc{}, tc{};
  if (!ClothContactArrays(mesh, va, vn, vt, 4) || !ClothContactArrays(manager, ta, tn, tt, 6) ||
      !ClothInputChunkRead(teamBox, "proxyCommonChunk", va[0].length, vc, ClothContactParticles) ||
      !ClothInputChunkRead(teamBox, "proxyTransformChunk", binding.mapping.length, tc, ClothContactParticles+1) ||
      !ClothContactRange(vc, va, 4, ClothContactParticles) || vc.count != sample.particles ||
      tc.count < vc.count || tc.count > ClothContactParticles+1 ||
      !ClothContactRange(tc, ta, 6, ClothContactParticles+1)) return false;
  auto cls = il2cpp_object_get_class(manager);
  if (!ClothContactMask(cls, "Flag_WorldRotWrite", sample.worldWriteMask) ||
      !ClothContactMask(cls, "Flag_LocalPosRotWrite", sample.localWriteMask) ||
      !ClothContactMask(cls, "Flag_Enable", sample.outputEnableMask)) return false;
  const auto localPos = ClothMethod(g_transformClass, "get_localPosition", "UnityEngine.Vector3");
  const auto localRot = ClothMethod(g_transformClass, "get_localRotation", "UnityEngine.Quaternion");
  const auto worldRot = ClothMethod(g_transformClass, "get_rotation", "UnityEngine.Quaternion");
  for (int n = 0; n < vc.count; ++n) {
    auto &o = sample.outputs[n];
    o.slot = tc.start+n; o.proxy = vc.start+n;
    if (sample.points[n].proxy != o.proxy) return false;
    void *t = nullptr, *args[]{&o.slot}; int16_t a = 0, b = 0;
    if (!ClothInvoke(getItem, access, args, t) || !t || !ClothAlive(t) || !ClothAnchorUnderOwner(t) ||
        !ClothValue(s_clothUnity.instance, t, o.id) || !o.id ||
        !ClothInputArrayValue(va[0], o.proxy, vt[0], &a, sizeof(a)) ||
        !ClothInputArrayValue(ta[0], o.slot, tt[0], &b, sizeof(b)) ||
        a != binding.identity.team || a != b ||
        !ClothInputArrayValue(va[3], o.proxy, vt[3], &o.parentIndex, sizeof(o.parentIndex)) ||
        o.parentIndex < -1 || o.parentIndex >= vc.count) return false;
    for (int k = 0; k < n; ++k) if (sample.outputs[k].id == o.id) return false;
    void *parent = CollisionParent(t);
    if (!parent || !ClothValue(s_clothUnity.instance, parent, o.parentId)) return false;
    CollisionName(t, o.name, sizeof(o.name));
    if (!ClothInputArrayValue(va[1], o.proxy, vt[1], o.proxyPosition, sizeof(o.proxyPosition)) ||
        !ClothInputArrayValue(va[2], o.proxy, vt[2], o.proxyRotation, sizeof(o.proxyRotation)) ||
        !ClothInputArrayValue(ta[1], o.slot, tt[1], &o.flags, sizeof(o.flags)) ||
        !ClothInputArrayValue(ta[2], o.slot, tt[2], o.lastPosition, sizeof(o.lastPosition)) ||
        !ClothInputArrayValue(ta[3], o.slot, tt[3], o.lastRotation, sizeof(o.lastRotation)) ||
        !ClothInputArrayValue(ta[4], o.slot, tt[4], o.lastLocalPosition, sizeof(o.lastLocalPosition)) ||
        !ClothInputArrayValue(ta[5], o.slot, tt[5], o.lastLocalRotation, sizeof(o.lastLocalRotation)) ||
        !ClothInputPoseRead(t, o.visible)) return false;
    void *box = nullptr;
    if (!ClothInvoke(localPos, t, nullptr, box) || !ClothInputCopyBox(box, "UnityEngine.Vector3", o.localPosition, sizeof(o.localPosition)) ||
        !ClothInvoke(localRot, t, nullptr, box) || !ClothInputCopyBox(box, "UnityEngine.Quaternion", o.localRotation, sizeof(o.localRotation)) ||
        !ClothInvoke(worldRot, t, nullptr, box) || !ClothInputCopyBox(box, "UnityEngine.Quaternion", o.worldRotation, sizeof(o.worldRotation))) return false;
  }
  for (int n = 0; n < vc.count; ++n) {
    const auto &o = sample.outputs[n];
    if (o.parentIndex >= 0 && sample.outputs[o.parentIndex].id != o.parentId) return false;
    for (int k = 0; k < binding.count; ++k) {
      const auto &target = binding.targets[k];
      if (target.slot == o.slot && (target.duplicate || target.transform.id.instance != o.id)) return false;
    }
  }
  return ClothContactOutputMapping(sample) && ClothContactLocalInputs(manager, teamBox, tc, sample) &&
      ClothInputIdentity(true, binding);
}

static bool ClothContactOutputSafe(void *teamBox, void *manager, void *access,
                                   void *getItem, ClothContactSample &sample,
                                   const ClothInputBinding &binding = s_clothInput) {
  __try { return ClothContactOutputRead(teamBox, manager, access, getItem, sample, binding); }
  __except (EXCEPTION_EXECUTE_HANDLER) { return false; }
}

static void ClothContactCaptureImpl(const ClothInputSample &input, void *teamBox, void *manager, void *access, void *getItem) {
  if (!ClothOnMainThread() || !s_clothInputHooks || s_clothInputUpdateDepth != 1 ||
      !ClothInputIdentity() ||
      s_clothInput.failed || s_clothInput.inputs.count < 2 || !input.teamKnown || input.culled || input.relative != 0) return;
  auto &s = s_clothContact;
  if (s.failed || (s.lastFrame >= 0 && input.frame-s.lastFrame < 8)) return;
  s.lastFrame = input.frame;
  LARGE_INTEGER begin{}, end{}, frequency{};
  QueryPerformanceCounter(&begin); QueryPerformanceFrequency(&frequency);
  ClothContactSample sample{};
  if (!ClothContactRead(input, teamBox, sample)) return;
  if (!s.outputFailed) {
    sample.outputKnown = ClothContactOutputSafe(teamBox, manager, access, getItem, sample);
    if (!sample.outputKnown) {
      s.outputFailed = true;
      Log("[CLOTH-SOLVER-OUTPUT] session=%llu frame=%d issue=output-mapping-layout-or-read-rejected contactTraceRetained=1",
          (unsigned long long)s_clothInput.identity.session, input.frame);
    }
  }
  if (!ClothInputIdentity() || input.epoch != s_clothInput.mapping.epoch) return;
  QueryPerformanceCounter(&end);
  sample.costMs = double(end.QuadPart-begin.QuadPart)*1000/frequency.QuadPart;
  s.identity = s_clothInput.identity; s.samples.Push(sample);
  if (s.budget.Observe(sample.costMs)) { ClothContactFail("completed-state-observer-cost-budget-exceeded"); return; }
  if (strcmp(s.issue, "completed-state-captured-output-frame-unattributed"))
    Log("[CLOTH-SOLVER-TRACE] session=%llu frame=%d particles=%d colliders=%d issue=completed-state-captured-output-frame-unattributed readOnly=1",
        (unsigned long long)s.identity.session, input.frame, sample.particles, sample.colliders);
  strcpy_s(s.issue, "completed-state-captured-output-frame-unattributed");
}
static void ClothContactCapture(const ClothInputSample &input, void *teamBox, void *manager = nullptr, void *access = nullptr, void *getItem = nullptr) {
  __try { ClothContactCaptureImpl(input, teamBox, manager, access, getItem); }
  __except (EXCEPTION_EXECUTE_HANDLER) { ClothContactFail("completed-state-observer-fault"); }
}
