#pragma once
#include "../collision/cloth_collision_attributes.h"
struct CollisionCurve {
  bool known = false, useCurve = false;
  float value = NAN, samples[5]{NAN, NAN, NAN, NAN, NAN};
};
struct CollisionBinding {
  int index = -1, id = 0;
  char name[128]{};
};
struct CollisionReadOnlyDiagnostics {
  float gravity = NAN, gravityProperty = NAN, gravityFalloff = NAN;
  float effectiveGravity = NAN, effectiveGravityFalloff = NAN;
  Vector3 direction{NAN, NAN, NAN}, effectiveDirection{NAN, NAN, NAN};
  int bindingCount = -1;
  bool bindingsComplete = false;
  char bindingIssue[96]{"not-captured"};
  std::vector<CollisionBinding> bindings;
};
template <class T>
static bool CollisionDiagnosticField(void *object, const char *name, const char *type, T &value,
                                     bool boxed = false) {
  if (!ClothOnMainThread() || !object || !il2cpp_field_get_flags || !il2cpp_class_value_size)
    return false;
  void *cls = il2cpp_object_get_class(object), *field = CollisionFieldInfo(cls, name, type);
  if (!field || (il2cpp_field_get_flags(field) & 0x10))
    return false;
  uint32_t align = 0;
  if (sizeof(T) == sizeof(Vector3)) {
    auto vectorClass =
        il2cpp_class_from_type ? il2cpp_class_from_type(il2cpp_field_get_type(field)) : nullptr;
    if (!vectorClass || il2cpp_class_value_size(vectorClass, &align) != sizeof(Vector3) ||
        ClothValueOffset(vectorClass, "x", "System.Single", 12, 4) != 0 ||
        ClothValueOffset(vectorClass, "y", "System.Single", 12, 4) != 4 ||
        ClothValueOffset(vectorClass, "z", "System.Single", 12, 4) != 8)
      return false;
  }
  if (boxed) {
    const int bytes = il2cpp_class_value_size(cls, &align);
    const size_t offset = il2cpp_field_get_offset(field);
    if (bytes <= 0 || bytes > 65536 || offset < 16 || offset + sizeof(T) > size_t(bytes) + 16)
      return false;
  }
  T result{};
  if (!CollisionField(object, name, type, result))
    return false;
  float values[sizeof(T) / sizeof(float)]{};
  memcpy(values, &result, sizeof(result));
  for (float v : values)
    if (!std::isfinite(v))
      return false;
  value = result;
  return true;
}
static bool CollisionDiagnosticIdentity(ClothInstance &i, const ClothReadback &r) {
  if (!ClothOnMainThread() || !ClothOwns(s_cloth.owner) || !r.process || !r.serialize)
    return false;
  void *bbc = ClothTarget(i.ref), *process = nullptr, *serialize = nullptr;
  return bbc && ClothInvoke(i.api.process, bbc, nullptr, process) && process == r.process &&
         ClothInvoke(i.api.serialize, bbc, nullptr, serialize) && serialize == r.serialize;
}
static CollisionReadOnlyDiagnostics
CollisionReadDiagnostics(ClothInstance &i, const ClothReadback &r, unsigned &bindingBudget) {
  CollisionReadOnlyDiagnostics d{};
  if (!CollisionDiagnosticIdentity(i, r))
    return d;
  const auto owner = s_cloth.owner;
  CollisionDiagnosticField(r.serialize, "gravity", "System.Single", d.gravity);
  CollisionDiagnosticField(r.serialize, "gravityFalloff", "System.Single", d.gravityFalloff);
  CollisionDiagnosticField(r.serialize, "gravityDirection", "Unity.Mathematics.float3",
                           d.direction);
  CollisionDiagnosticField(ClothTarget(i.ref), "gravityProperty", "System.Single",
                           d.gravityProperty);
  void *box = nullptr;
  if (ClothInvoke(ClothMethod(il2cpp_object_get_class(r.process), "get_parameters",
                              "BeyondDynamicBone.ClothParameters"),
                  r.process, nullptr, box) &&
      box) {
    CollisionDiagnosticField(box, "gravity", "System.Single", d.effectiveGravity, true);
    CollisionDiagnosticField(box, "gravityFalloff", "System.Single", d.effectiveGravityFalloff,
                             true);
    CollisionDiagnosticField(box, "worldGravityDirection", "Unity.Mathematics.float3",
                             d.effectiveDirection, true);
  }
  auto getContainer = ClothMethod(il2cpp_object_get_class(r.process), "get_ProxyMeshContainer",
                                  "BeyondDynamicBone.VirtualMeshContainer");
  void *container = nullptr;
  strcpy_s(d.bindingIssue, "container-or-count-unavailable");
  if (ClothInvoke(getContainer, r.process, nullptr, container) && container) {
    void *unique = nullptr, *shared = nullptr;
    const bool viewKnown =
        CollisionField(container, "uniqueData",
                       "BeyondDynamicBone.VirtualMesh.UniqueSerializationData", unique) &&
        CollisionField(container, "shareVirtualMesh", "BeyondDynamicBone.VirtualMesh", shared);
    auto cls = il2cpp_object_get_class(container);
    auto getCount = ClothMethod(cls, "GetTransformCount", "System.Int32");
    auto getItem =
        ClothMethod(cls, "GetTransformFromIndex", "UnityEngine.Transform", "System.Int32");
    int count = -1;
    if (viewKnown && ClothValue(getCount, container, count) && count >= 0 && count <= 512 &&
        getItem) {
      d.bindingCount = count;
      d.bindingsComplete = count > 0;
      strcpy_s(d.bindingIssue, count ? "none" : "empty-binding-list");
      for (int n = 0; n < count; ++n) {
        if (!bindingBudget) {
          d.bindingsComplete = false;
          strcpy_s(d.bindingIssue, "snapshot-binding-budget-exhausted");
          break;
        }
        --bindingBudget;
        void *t = nullptr, *args[] = {&n};
        CollisionBinding entry{};
        entry.index = n;
        if (!ClothOwns(owner) || !ClothInvoke(getItem, container, args, t) || !t ||
            !ClothAlive(t) || !ClothAnchorUnderOwner(t) ||
            !ClothValue(s_clothUnity.instance, t, entry.id) || !entry.id) {
          d.bindingsComplete = false;
          strcpy_s(d.bindingIssue, "missing-or-outside-owner-transform");
        } else {
          CollisionName(t, entry.name, sizeof(entry.name));
          for (const auto &old : d.bindings)
            if (old.id == entry.id) {
              d.bindingsComplete = false;
              strcpy_s(d.bindingIssue, "duplicate-transform-binding");
            }
        }
        d.bindings.push_back(entry);
      }
    } else {
      strcpy_s(d.bindingIssue, "binding-count-or-getter-invalid");
    }
    void *after = nullptr, *afterUnique = nullptr, *afterShared = nullptr;
    int afterCount = -1;
    if (!ClothInvoke(getContainer, r.process, nullptr, after) || after != container ||
        !ClothValue(getCount, container, afterCount) || afterCount != count ||
        !CollisionField(container, "uniqueData",
                        "BeyondDynamicBone.VirtualMesh.UniqueSerializationData", afterUnique) ||
        afterUnique != unique ||
        !CollisionField(container, "shareVirtualMesh", "BeyondDynamicBone.VirtualMesh",
                        afterShared) ||
        afterShared != shared) {
      d.bindings.clear();
      d.bindingCount = -1;
      d.bindingsComplete = false;
      strcpy_s(d.bindingIssue, "container-or-count-changed");
    }
  }
  if (!ClothOwns(owner) || !CollisionDiagnosticIdentity(i, r)) {
    d = {};
    strcpy_s(d.bindingIssue, "owner-process-or-serialization-changed");
  }
  return d;
}
struct CollisionClothPod {
  CollisionAttributeReport attributes{};
  CollisionReadOnlyDiagnostics diagnostics{};
  int id = 0, team = -1, rootCount = -1, collisionBoneCount = -1;
  uintptr_t process = 0, serialize = 0;
  char name[96]{}, type[32]{}, processType[32]{}, mode[32]{}, connection[48]{};
  bool stateKnown = false, active = false, running = false, enabled = false, skip = false,
       culled = false;
  float weight = NAN, propertyWeight = NAN, ratio = NAN, blend = NAN, time = NAN, globalTime = NAN;
  bool motionKnown = false, maxDistance = false, backstop = false;
  bool restorationKnown = false, restorationEnabled = false, angleLimitKnown = false,
       angleLimitEnabled = false;
  bool effectiveAngleKnown = false;
  int effectiveAngle = 0;
  bool resetPolicyKnown = false, resetPolicyEnabled = false;
  float resetWeightThreshold = NAN;
  float backstopRadius = NAN, motionStiffness = NAN;
  CollisionCurve radius{}, limit{}, max{}, backstopDistance{}, angleRestoration{}, angleLimit{};
  CollisionTopology topology{};
  bool effectiveModeKnown = false;
  int effectiveMode = 0;
  char effectiveModeName[32]{"unknown"};
};
struct CollisionColliderPod {
  CollisionGeometry geometry{};
  int cloth = 0, teams = -1;
  bool serialized = false, process = false, team = false, registrationKnown = false, owned = false;
  Vector3 apiLocalDir{NAN, NAN, NAN};
  Vector3 apiSize{NAN, NAN, NAN};
  bool apiSizeKnown = false;
};
struct CollisionNodePod {
  CollisionBoneAttribute attribute{};
  int id = 0, parent = 0, cloth = 0;
  char name[128]{}, role[16]{};
  Vector3 position{};
  bool collisionSelected = false;
};
struct CollisionSnapshot {
  uint64_t session = 0, generation = 0;
  uintptr_t owner = 0;
  int frame = -1, scene = 0, geometryMode = 0;
  char backend[24]{}, stage[80]{};
  bool truncated = false, coherent = false;
  unsigned treeVisits = 0;
  unsigned bindingBudget = 2048;
  double captureMs = 0, unityFrameMs = NAN;
  ClothDiscoveryReport discovery{};
  bool enumApiReady = false;
  int useAnimatorTransform = -1, useCrossFrameJob = -1;
  std::vector<CollisionClothPod> cloth;
  std::vector<CollisionColliderPod> colliders;
  std::vector<CollisionNodePod> nodes;
};
static int CollisionReadStaticBool(void *cls, const char *name) {
  if (!ClothOnMainThread() || !il2cpp_field_get_flags || !il2cpp_field_static_get_value)
    return -1;
  void *f = CollisionFieldInfo(cls, name, "System.Boolean");
  if (!f || !(il2cpp_field_get_flags(f) & 0x10))
    return -1;
  unsigned char raw[8]{};
  __try {
    il2cpp_field_static_get_value(f, raw);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return -1;
  }
  return raw[0] <= 1 ? int(raw[0]) : -1;
}
static CollisionCurve CollisionReadCurve(void *parent, const char *field) {
  CollisionCurve c{};
  void *curve = nullptr;
  if (!CollisionField(parent, field, "BeyondDynamicBone.CurveSerializeData", curve) || !curve)
    return c;
  c.known = ClothField(curve, "value", "System.Single", c.value) &&
            ClothField(curve, "useCurve", "System.Boolean", c.useCurve);
  void *evaluate =
      ClothMethod(il2cpp_object_get_class(curve), "Evaluate", "System.Single", "System.Single");
  for (int n = 0; n < 5; ++n) {
    float depth = n * .25f;
    void *args[] = {&depth}, *r = nullptr;
    if (ClothInvoke(evaluate, curve, args, r) && r)
      memcpy(&c.samples[n], (char *)r + 16, sizeof(float));
  }
  return c;
}
static void CollisionSnapshotNode(CollisionSnapshot &snapshot, void *t, int cloth, const char *role,
                                  void *collisionBones,
                                  const CollisionAttributeReader *attributes = nullptr) {
  if (!t || !ClothAlive(t))
    return;
  int id = 0;
  if (!ClothValue(s_clothUnity.instance, t, id))
    return;
  for (const auto &n : snapshot.nodes)
    if (n.id == id && n.cloth == cloth)
      return;
  if (snapshot.nodes.size() >= 2048) {
    snapshot.truncated = true;
    return;
  }
  CollisionNodePod n{};
  n.id = id;
  n.cloth = cloth;
  strncpy_s(n.role, role, _TRUNCATE);
  if (!CollisionPosition(t, n.position))
    return;
  CollisionName(t, n.name, sizeof(n.name));
  void *parent = CollisionParent(t);
  if (parent)
    ClothValue(s_clothUnity.instance, parent, n.parent);
  CollisionContains(collisionBones, t, "UnityEngine.Transform", n.collisionSelected);
  if (attributes)
    n.attribute = CollisionReadBoneAttribute(*attributes, t);
  snapshot.nodes.push_back(n);
}
static void CollisionSnapshotTree(CollisionSnapshot &snapshot, void *root, int cloth,
                                  void *collisionBones,
                                  const CollisionAttributeReader *attributes = nullptr) {
  struct Pending {
    void *t;
    int depth;
  };
  std::vector<Pending> queue{{root, 0}};
  std::vector<void *> seen;
  for (size_t n = 0; n < queue.size() && n < 512; ++n) {
    if (snapshot.nodes.size() >= 2048 || snapshot.treeVisits >= 4096) {
      snapshot.truncated = true;
      return;
    }
    ++snapshot.treeVisits;
    auto node = queue[n];
    if (!node.t || std::find(seen.begin(), seen.end(), node.t) != seen.end())
      continue;
    seen.push_back(node.t);
    CollisionSnapshotNode(snapshot, node.t, cloth, "cloth", collisionBones, attributes);
    int count = 0;
    if (!ClothValue(s_clothUnity.childCount, node.t, count) || count < 0 || count > 128) {
      snapshot.truncated = true;
      continue;
    }
    if (node.depth >= 32 && count) {
      snapshot.truncated = true;
      continue;
    }
    for (int child = 0; child < count; ++child) {
      if (queue.size() >= 512) {
        snapshot.truncated = true;
        break;
      }
      void *t = nullptr, *args[] = {&child};
      if (ClothInvoke(s_clothUnity.child, node.t, args, t) && t)
        queue.push_back({t, node.depth + 1});
    }
  }
}
static CollisionSnapshot CollisionCollect(const char *stage) {
  CollisionSnapshot s{};
  s.enumApiReady =
      il2cpp_field_get_flags && il2cpp_field_static_get_value && il2cpp_class_from_type;
  const auto owner = s_cloth.owner;
  s.session = owner.session;
  s.generation = owner.generation;
  s.owner = owner.character;
  s.frame = ClothFrame();
  s.scene = s_cloth.scene;
  s.discovery = s_cloth.discovery;
  s.truncated = !s.discovery.complete;
  s.geometryMode = s_collisionGeometry.load(std::memory_order_acquire);
  strncpy_s(s.backend, MotionBackendName(static_cast<MotionBackend>(owner.backend)), _TRUNCATE);
  strncpy_s(s.stage, stage, _TRUNCATE);
  LARGE_INTEGER start{}, end{}, frequency{};
  QueryPerformanceFrequency(&frequency);
  QueryPerformanceCounter(&start);
  size_t assemblyCount = 0;
  void **asms = il2cpp_domain_get_assemblies(il2cpp_domain_get(), &assemblyCount);
  void *timeClass = FindClass("UnityEngine", "Time", asms, assemblyCount);
  void *managerClass = FindClass("BeyondDynamicBone", "MagicaManager", asms, assemblyCount);
  s.useAnimatorTransform = CollisionReadStaticBool(managerClass, "UseAnimatorTransform");
  s.useCrossFrameJob = CollisionReadStaticBool(managerClass, "UseCrossFrameJob");
  Log("[CLOTH-CONTACT-SAMPLING] session=%llu frame=%d useAnimatorTransform=%d useCrossFrameJob=%d "
      "source=static-configuration perColliderBufferBinding=unknown jobReadback=unavailable",
      (unsigned long long)s.session, s.frame, s.useAnimatorTransform, s.useCrossFrameJob);
  float dt = NAN;
  if (ClothValue(ClothMethod(timeClass, "get_unscaledDeltaTime", "System.Single", nullptr, true),
                 nullptr, dt))
    s.unityFrameMs = dt * 1000.0;
  void *animator = ClothTarget(s_cloth.animator);
  for (int n = 0; n < CollisionBodyCount; ++n)
    CollisionSnapshotNode(s, CollisionBody(animator, n), 0, "body", nullptr);
  for (int n = 0; n < s_cloth.count; ++n) {
    auto &i = s_cloth.instances[n];
    ClothReadback r{};
    ClothRead(i, r);
    CollisionClothPod c{};
    c.id = i.ref.id.instance;
    c.team = r.team;
    strncpy_s(c.name, i.name, _TRUNCATE);
    c.process = reinterpret_cast<uintptr_t>(r.process);
    c.serialize = reinterpret_cast<uintptr_t>(r.serialize);
    c.stateKnown = r.state.readable;
    c.active = r.state.active;
    c.running = r.state.running;
    c.enabled = r.state.enabled && r.state.processEnabled;
    c.skip = r.state.skip;
    c.culled = r.state.culled;
    c.weight = r.weight;
    c.propertyWeight = r.propertyWeight;
    c.ratio = r.ratio;
    c.blend = r.blend;
    c.time = r.time;
    c.globalTime = r.globalTime;
    c.diagnostics = CollisionReadDiagnostics(i, r, s.bindingBudget);
    if (!strcmp(c.diagnostics.bindingIssue, "snapshot-binding-budget-exhausted"))
      s.truncated = true;
    CollisionEnum(r.serialize, "clothType", "BeyondDynamicBone.ClothProcess.ClothType", c.type,
                  sizeof(c.type));
    CollisionEnum(r.process, "<clothType>k__BackingField",
                  "BeyondDynamicBone.ClothProcess.ClothType", c.processType, sizeof(c.processType));
    CollisionEnum(r.serialize, "connectionMode",
                  "BeyondDynamicBone.RenderSetupData.BoneConnectionMode", c.connection,
                  sizeof(c.connection));
    c.radius = CollisionReadCurve(r.serialize, "radius");
    void *constraint = nullptr, *serialized = nullptr, *process = nullptr, *bones = nullptr,
         *roots = nullptr, *motion = nullptr, *angle = nullptr;
    const bool configKnown = CollisionList(r.serialize, constraint, serialized);
    const bool processKnown = ClothField(r.process, "colliderList", CollisionListType, process);
    CollisionEnum(constraint, "mode", "BeyondDynamicBone.ColliderCollisionConstraint.Mode", c.mode,
                  sizeof(c.mode));
    c.topology = CollisionReadTopology(r.process);
    c.effectiveModeKnown = CollisionProcessMode(r.process, c.effectiveMode);
    c.effectiveAngleKnown = CollisionProcessAngle(r.process, c.effectiveAngle);
    c.resetPolicyKnown = ClothField(r.serialize, "resetSimulationToAnimationPoseWhenWeightLow",
                                    "System.Boolean", c.resetPolicyEnabled) &&
                         ClothField(r.serialize, "resetSimulationToAnimationPoseWeightThreshold",
                                    "System.Single", c.resetWeightThreshold) &&
                         std::isfinite(c.resetWeightThreshold);
    void *modeField = nullptr;
    int pointMode = 0, edgeMode = 0;
    if (constraint && c.effectiveModeKnown &&
        CollisionModeMetadata(il2cpp_object_get_class(constraint), modeField, pointMode,
                              edgeMode)) {
      if (c.effectiveMode == pointMode)
        strcpy_s(c.effectiveModeName, "Point");
      else if (c.effectiveMode == edgeMode)
        strcpy_s(c.effectiveModeName, "Edge");
    }
    c.limit = CollisionReadCurve(constraint, "limitDistance");
    if (ClothField(constraint, "collisionBones",
                   "System.Collections.Generic.List<UnityEngine.Transform>", bones))
      c.collisionBoneCount = CollisionCount(bones);
    if (ClothField(r.serialize, "rootBones",
                   "System.Collections.Generic.List<UnityEngine.Transform>", roots))
      c.rootCount = CollisionCount(roots);
    if (CollisionField(r.serialize, "motionConstraint",
                       "BeyondDynamicBone.MotionConstraint.SerializeData", motion) &&
        motion) {
      c.motionKnown = ClothField(motion, "useMaxDistance", "System.Boolean", c.maxDistance) &&
                      ClothField(motion, "useBackstop", "System.Boolean", c.backstop);
      ClothField(motion, "backstopRadius", "System.Single", c.backstopRadius);
      ClothField(motion, "stiffness", "System.Single", c.motionStiffness);
      c.max = CollisionReadCurve(motion, "maxDistance");
      c.backstopDistance = CollisionReadCurve(motion, "backstopDistance");
    }
    if (CollisionField(r.serialize, "angleRestorationConstraint",
                       "BeyondDynamicBone.AngleConstraint.RestorationSerializeData", angle)) {
      c.angleRestoration = CollisionReadCurve(angle, "stiffness");
      c.restorationKnown =
          ClothField(angle, "useAngleRestoration", "System.Boolean", c.restorationEnabled);
    }
    angle = nullptr;
    if (CollisionField(r.serialize, "angleLimitConstraint",
                       "BeyondDynamicBone.AngleConstraint.LimitSerializeData", angle)) {
      c.angleLimit = CollisionReadCurve(angle, "limitAngle");
      c.angleLimitKnown = ClothField(angle, "useAngleLimit", "System.Boolean", c.angleLimitEnabled);
    }
    const auto attributes = CollisionReadAttributes(ClothTarget(i.ref), c.attributes);
    s.cloth.push_back(c);
    std::vector<void *> colliders;
    for (void *list : {serialized, process}) {
      int count = CollisionCount(list);
      if (count < 0 || count > 128)
        s.truncated = true;
      for (int item = 0; item < (std::min)(count, 128); ++item) {
        void *o = CollisionItem(list, item, "BeyondDynamicBone.ColliderComponent");
        if (o && std::find(colliders.begin(), colliders.end(), o) == colliders.end())
          colliders.push_back(o);
      }
    }
    for (void *o : colliders) {
      if (s.colliders.size() >= 512) {
        s.truncated = true;
        break;
      }
      CollisionColliderPod p{};
      p.cloth = c.id;
      p.geometry = CollisionReadGeometry(o);
      p.registrationKnown =
          configKnown && processKnown &&
          CollisionContains(serialized, o, "BeyondDynamicBone.ColliderComponent", p.serialized) &&
          CollisionContains(process, o, "BeyondDynamicBone.ColliderComponent", p.process) &&
          CollisionTeams(o, c.team, p.team, p.teams);
      ClothValue(ClothMethod(il2cpp_object_get_class(o), "GetLocalDir", "UnityEngine.Vector3"), o,
                 p.apiLocalDir);
      p.apiSizeKnown =
          ClothValue(ClothMethod(il2cpp_object_get_class(o), "GetSize", "UnityEngine.Vector3"), o,
                     p.apiSize) &&
          ClothFinitePosition(p.apiSize);
      s.colliders.push_back(p);
    }
    if (c.rootCount > 128 || c.collisionBoneCount > 512)
      s.truncated = true;
    for (int item = 0; item < (std::min)(c.rootCount, 128); ++item)
      CollisionSnapshotTree(s, CollisionItem(roots, item, "UnityEngine.Transform"), c.id, bones,
                            &attributes);
    for (int item = 0; item < (std::min)(c.collisionBoneCount, 512); ++item)
      CollisionSnapshotNode(s, CollisionItem(bones, item, "UnityEngine.Transform"), c.id,
                            "selected", bones, &attributes);
    Log("[CLOTH-CONTACT-DIAGNOSTIC] session=%llu frame=%d bbc=%d gravity=%g property=%g "
        "processGravity=%g bindings=%d complete=%d issue=%s "
        "source=managed-parameters-and-transform-bindings-not-live-particle-state",
        (unsigned long long)s.session, s.frame, c.id, c.diagnostics.gravity,
        c.diagnostics.gravityProperty, c.diagnostics.effectiveGravity, c.diagnostics.bindingCount,
        c.diagnostics.bindingsComplete, c.diagnostics.bindingIssue);
    Log("[CLOTH-CONTACT-ATTRIBUTES] session=%llu frame=%d bbc=%d name='%s' dictionaryKnown=%d "
        "count=%d flagsKnown=%d prebuildKnown=%d usePrebuild=%d rawKnown=%d rawCount=%d "
        "rawLength=%d rawBytes=%zu selectionKnown=%d selectionCount=%zu "
        "source=managed-asset-not-live-job",
        (unsigned long long)s.session, s.frame, c.id, c.name, c.attributes.dictionaryKnown,
        c.attributes.dictionaryCount, c.attributes.flagsKnown, c.attributes.prebuildKnown,
        c.attributes.prebuild, c.attributes.rawKnown, c.attributes.rawCount, c.attributes.rawLength,
        c.attributes.rawBytes.size(), c.attributes.selectionKnown,
        c.attributes.selectionValues.size());
    auto kind = !strcmp(c.type, "BoneSpring")  ? eiem_collision::Kind::BoneSpring
                : !strcmp(c.type, "BoneCloth") ? eiem_collision::Kind::BoneCloth
                : !strcmp(c.type, "MeshCloth") ? eiem_collision::Kind::MeshCloth
                                               : eiem_collision::Kind::Unknown;
    Log("[CLOTH-CONTACT-CONFIG] session=%llu frame=%d bbc=%d name='%s' type=%s processType=%s "
        "mode=%s connection=%s roots=%d collisionBones=%d maxKnown=%d maxDistance=%d backstop=%d "
        "policy=%s effectiveMode=%s proxyCountsKnown=%d vertices=%d edges=%d lines=%d triangles=%d "
        "topology=count-getters-only-no-buffer-access",
        (unsigned long long)s.session, s.frame, c.id, c.name, c.type, c.processType, c.mode,
        c.connection, c.rootCount, c.collisionBoneCount, c.motionKnown, c.maxDistance, c.backstop,
        eiem_collision::Policy(kind, c.topology.known, c.topology.edges > 0,
                               c.collisionBoneCount >= 0, c.collisionBoneCount),
        c.effectiveModeName, c.topology.known, c.topology.vertices, c.topology.edges,
        c.topology.lines, c.topology.triangles);
  }
  QueryPerformanceCounter(&end);
  s.captureMs =
      frequency.QuadPart ? double(end.QuadPart - start.QuadPart) * 1000.0 / frequency.QuadPart : 0;
  s.coherent = ClothOwns(owner) && ClothFrame() == s.frame;
  return s;
}
static std::string CollisionJsonString(const char *v) {
  std::string s = "\"";
  for (const unsigned char *p = reinterpret_cast<const unsigned char *>(v); *p; ++p) {
    if (*p == '"' || *p == '\\') {
      s += '\\';
      s += char(*p);
    } else if (*p < 32) {
      char escaped[7];
      sprintf_s(escaped, "\\u%04x", *p);
      s += escaped;
    } else
      s += char(*p);
  }
  return s + '"';
}
static std::string CollisionNumber(double v) {
  if (!std::isfinite(v))
    return "null";
  std::ostringstream out;
  out << std::setprecision(10) << v;
  return out.str();
}
static std::string CollisionVector(CV v) {
  return "[" + CollisionNumber(v.x) + "," + CollisionNumber(v.y) + "," + CollisionNumber(v.z) + "]";
}
static std::string CollisionCurveJson(const CollisionCurve &c) {
  std::string s = "{\"known\":" + std::to_string(c.known) +
                  ",\"useCurve\":" + std::to_string(c.useCurve) +
                  ",\"value\":" + CollisionNumber(c.value) + ",\"samples\":[";
  for (int n = 0; n < 5; ++n) {
    if (n)
      s += ',';
    s += CollisionNumber(c.samples[n]);
  }
  return s + "]}";
}
template<class T> static void ClothInputJsonArray(std::ostream &out, const T *v, int count) {
  out << '[';
  for (int n = 0; n < count; ++n) { if (n) out << ','; out << CollisionNumber(v[n]); }
  out << ']';
}
static std::string ClothContactJson(const ClothInputBinding &binding = s_clothInput,
                                    const ClothContactTrace &s = s_clothContact) {
  const bool current = ClothInputIdentity(true, binding) && s.identity == binding.identity;
  std::ostringstream out; out << std::setprecision(12);
  out << "{\"schema\":1,\"readOnly\":true,\"currentIdentity\":" << current << ",\"failed\":" << s.failed
      << ",\"outputFailed\":" << s.outputFailed
      << ",\"issue\":" << CollisionJsonString(s.issue) << ",\"session\":" << s.identity.session
      << ",\"generation\":" << s.identity.generation << ",\"cloth\":" << s.identity.cloth << ",\"team\":" << s.identity.team
      << ",\"owner\":" << CollisionJsonString(std::to_string(s.identity.owner).c_str())
      << ",\"process\":" << CollisionJsonString(std::to_string(s.identity.process).c_str())
      << ",\"serialize\":" << CollisionJsonString(std::to_string(s.identity.serialize).c_str())
      << ",\"boundary\":\"previous cloth master completed; current ReadTransform and WriteDoubleBuffer completed; before current ValidPosition\""
      << ",\"semantics\":\"completed simulation arrays and last collider WorkData; output source frame/substep unattributed; current centers/sizes may precede next work update; no contact event or render depth\""
      << ",\"indices\":\"runtime particle/proxy chunks, not CAB indices or Transform binding indices\",\"samples\":[";
  if (current) for (size_t n = 0; n < s.samples.count; ++n) {
    if (n) out << ',';
    const auto &p = s.samples.At(n);
    out << "{\"frame\":" << p.frame << ",\"submission\":" << p.submission << ",\"epoch\":" << p.epoch
        << ",\"outputSourceFrame\":null,\"costMs\":" << CollisionNumber(p.costMs)
        << ",\"teamFlags\":" << p.teamFlags << ",\"simulateWeight\":" << CollisionNumber(p.simulateWeight)
        << ",\"blendWeight\":" << CollisionNumber(p.blendWeight) << ",\"lodWeight\":" << CollisionNumber(p.lodWeight)
        << ",\"validMask\":" << int(p.validMask) << ",\"enableMask\":" << int(p.enableMask)
        << ",\"moveMask\":" << int(p.moveMask) << ",\"disableCollisionMask\":" << int(p.disableMask)
        << ",\"colliderCount\":" << p.reportedColliders << ",\"colliders\":[";
    for (int k = 0; k < p.colliders; ++k) {
      if (k) out << ','; const auto &c = p.shapes[k];
      out << "{\"id\":" << c.id << ",\"transformId\":" << c.transform << ",\"slot\":" << c.slot
          << ",\"componentType\":" << CollisionJsonString(c.componentType)
          << ",\"transformSlot\":" << c.transformSlot << ",\"flags\":" << int(c.flags) << ",\"center\":";
      ClothInputJsonArray(out, c.center, 3); out << ",\"size\":"; ClothInputJsonArray(out, c.size, 3);
      out << ",\"workRadius\":"; ClothInputJsonArray(out, c.radius, 2);
      out << ",\"workOldEndpoints\":"; ClothInputJsonArray(out, c.oldEndpoints, 6);
      out << ",\"workNextEndpoints\":"; ClothInputJsonArray(out, c.nextEndpoints, 6); out << '}';
    }
    out << "],\"outputKnown\":" << p.outputKnown << ",\"worldWriteMask\":" << int(p.worldWriteMask)
        << ",\"localWriteMask\":" << int(p.localWriteMask) << ",\"outputEnableMask\":" << int(p.outputEnableMask)
        << ",\"outputs\":[";
    if (p.outputKnown) for (int k = 0; k < p.particles; ++k) {
      if (k) out << ','; const auto &v = p.outputs[k];
      out << "{\"id\":" << v.id << ",\"name\":" << CollisionJsonString(v.name)
          << ",\"parentId\":" << v.parentId << ",\"parentIndex\":" << v.parentIndex
          << ",\"slot\":" << v.slot << ",\"proxyIndex\":" << v.proxy << ",\"flags\":" << int(v.flags)
          << ",\"proxyPosition\":"; ClothInputJsonArray(out, v.proxyPosition, 3);
      out << ",\"proxyRotation\":"; ClothInputJsonArray(out, v.proxyRotation, 4);
      out << ",\"lastPosition\":"; ClothInputJsonArray(out, v.lastPosition, 3);
      out << ",\"lastRotation\":"; ClothInputJsonArray(out, v.lastRotation, 4);
      out << ",\"lastLocalPosition\":"; ClothInputJsonArray(out, v.lastLocalPosition, 3);
      out << ",\"lastLocalRotation\":"; ClothInputJsonArray(out, v.lastLocalRotation, 4);
      out << ",\"localInputKnown\":" << (v.localInputKnown?"true":"false");
      if (v.localInputKnown) {
        out << ",\"inputLocalPosition\":"; ClothInputJsonArray(out, v.inputLocalPosition, 3);
        out << ",\"inputLocalRotation\":"; ClothInputJsonArray(out, v.inputLocalRotation, 4);
      }
      out << ",\"visibleMatrix\":"; ClothInputJsonArray(out, v.visible.matrix, 16);
      out << ",\"localPosition\":"; ClothInputJsonArray(out, v.localPosition, 3);
      out << ",\"localRotation\":"; ClothInputJsonArray(out, v.localRotation, 4);
      out << ",\"worldRotation\":"; ClothInputJsonArray(out, v.worldRotation, 4); out << '}';
    }
    out << "],\"particles\":[";
    for (int k = 0; k < p.particles; ++k) {
      if (k) out << ','; const auto &v = p.points[k];
      out << "{\"particleIndex\":" << v.particle << ",\"proxyIndex\":" << v.proxy
          << ",\"attribute\":" << int(v.attribute) << ",\"depth\":" << CollisionNumber(v.depth)
          << ",\"friction\":" << CollisionNumber(v.friction) << ",\"normal\":";
      ClothInputJsonArray(out, v.normal, 3); out << ",\"nextPosition\":"; ClothInputJsonArray(out, v.next, 3);
      out << ",\"displayPosition\":"; ClothInputJsonArray(out, v.display, 3); out << '}';
    }
    out << "]}";
  }
  return out.str() + "]}";
}
static std::string ClothInputJson() {
  std::ostringstream out;
  const auto &s = s_clothInput;
  const bool current = ClothInputIdentity();
  out << "{\"schema\":1,\"readOnly\":true,\"hooksInstalled\":" << s_clothInputHooks
      << ",\"currentIdentity\":" << current << ",\"issue\":"
      << CollisionJsonString(s_clothInputHooks ? s.issue : s_clothInputHookIssue)
      << ",\"boundary\":\"native completed ReadTransform and WriteDoubleBufferTransform; before ValidPosition\""
      << ",\"branch\":\"TransformAccess-cross-frame\",\"semantics\":\"completed manager input, not particle output or render penetration; flags/culling can retain old slots\""
      << ",\"readMask\":" << s_clothInputReadMask << ",\"enableMask\":" << s_clothInputEnableMask
      << ",\"output\":\"visible transforms after previous double-buffer output; output source frame not yet established\""
      << ",\"phaseNames\":[\"backend-submit\",\"Muscle-after-owner-FinalIK\",\"completed-native-read\"]"
      << ",\"sourceFramePolicy\":\"DirectVmd committed frame; Muscle sourceFrame unknown (playhead is not an atomic pose ticket)\""
      << ",\"session\":" << s.identity.session << ",\"generation\":" << s.identity.generation
      << ",\"owner\":" << CollisionJsonString(std::to_string(s.identity.owner).c_str())
      << ",\"process\":" << CollisionJsonString(std::to_string(s.identity.process).c_str())
      << ",\"serialize\":" << CollisionJsonString(std::to_string(s.identity.serialize).c_str())
      << ",\"cloth\":" << s.identity.cloth << ",\"team\":" << s.identity.team
      << ",\"animator\":" << s_cloth.animator.id.instance
      << ",\"epoch\":" << s.mapping.epoch << ",\"slotCount\":" << s.mapping.length
      << ",\"scanCursor\":" << s.mapping.cursor << ",\"completedBoundaries\":" << s.completedBoundaries
      << ",\"rootCount\":" << s.rootCount << ",\"colliderListSlots\":" << s.colliderSlots
      << ",\"liveColliders\":" << s.liveColliders << ",\"emptyColliderSlots\":" << s.emptyColliderSlots
      << ",\"rootListVersion\":" << s.rootsVersion << ",\"colliderListVersion\":" << s.collidersVersion
      << ",\"failed\":" << s.failed << ",\"listFailure\":" << s.listFailure
      << ",\"lastSubmitStage\":" << CollisionJsonString(s.stage) << ",\"targets\":[";
  if (current) for (int n = 0; n < s.count; ++n) {
    const auto &t = s.targets[n]; if (n) out << ',';
    out << "{\"id\":" << t.transform.id.instance << ",\"name\":" << CollisionJsonString(t.name)
        << ",\"role\":" << CollisionJsonString(t.role) << ",\"slot\":" << t.slot
        << ",\"duplicate\":" << t.duplicate << '}';
  }
  out << "],\"samples\":[";
  bool first = true;
  auto emit = [&](const ClothInputSample &p) {
    if (!first) out << ','; first = false;
    out << "{\"seq\":" << p.sequence << ",\"submission\":" << p.submission
        << ",\"frame\":" << p.frame << ",\"submittedFrame\":" << p.submittedFrame
        << ",\"sourceFrame\":" << CollisionNumber(p.sourceFrame) << ",\"phase\":" << p.phase
        << ",\"playheadFrame\":" << CollisionNumber(p.playheadFrame)
        << ",\"epoch\":" << p.epoch << ",\"costMs\":" << CollisionNumber(p.costMs)
        << ",\"teamKnown\":" << p.teamKnown << ",\"culled\":" << p.culled
        << ",\"teamFlags\":" << CollisionJsonString(std::to_string(p.teamFlags).c_str())
        << ",\"useRelativeTransform\":" << p.relative << ",\"relativePosition\":";
    ClothInputJsonArray(out, p.relativePosition, 3); out << ",\"relativeRotation\":";
    ClothInputJsonArray(out, p.relativeRotation, 4); out << ",\"points\":[";
    for (int n = 0; n < p.count; ++n) {
      const auto &v = p.points[n]; if (n) out << ',';
      out << "{\"visibleMatrix\":";
      if (v.visible.known) ClothInputJsonArray(out, v.visible.matrix, 16); else out << "null";
      out << ",\"slot\":" << v.slot << ",\"flags\":" << v.flags << ",\"input\":";
      if (!v.inputKnown) out << "null";
      else {
        out << "{\"position\":"; ClothInputJsonArray(out, v.position, 3);
        out << ",\"rotation\":"; ClothInputJsonArray(out, v.rotation, 4);
        out << ",\"scale\":"; ClothInputJsonArray(out, v.scale, 3);
        out << ",\"matrix\":"; ClothInputJsonArray(out, v.matrix, 16); out << '}';
      }
      out << '}';
    }
    out << "]}";
  };
  if (current) {
    size_t body = 0, input = 0;
    while (body < s.body.count || input < s.inputs.count) {
      if (input == s.inputs.count || (body < s.body.count &&
          s.body.At(body).sequence < s.inputs.At(input).sequence)) emit(s.body.At(body++));
      else emit(s.inputs.At(input++));
    }
  }
  out << "]}";
  return out.str();
}
static std::string ClothBodyJson() {
  const auto &b = s_clothBody;
  const bool current = ClothBodyIdentity();
  std::ostringstream out;
  out << "{\"schema\":1,\"readOnly\":true,\"currentIdentity\":" << current
      << ",\"issue\":" << CollisionJsonString(b.issue)
      << ",\"space\":\"world-column-major\",\"renderer\":\"S_actor_wulfa_body_01_lod0\""
      << ",\"rendererId\":" << b.renderer.id.instance << ",\"meshId\":" << b.mesh.id.instance
      << ",\"mesh\":" << CollisionJsonString(b.meshName) << ",\"vertexCount\":" << b.vertices
      << ",\"session\":" << s_clothInput.identity.session << ",\"generation\":" << s_clothInput.identity.generation
      << ",\"owner\":" << CollisionJsonString(std::to_string(s_clothInput.identity.owner).c_str())
      << ",\"boundary\":\"backend body submit; not renderer bake or solver contact; compare submission with inputTrace\""
      << ",\"bones\":[";
  if (current) for (int n = 0; n < b.count; ++n) {
    if (n) out << ',';
    const auto &bone = b.bones[n];
    out << "{\"id\":" << bone.ref.id.instance << ",\"name\":" << CollisionJsonString(bone.name)
        << ",\"parentId\":" << bone.parent << ",\"parentName\":" << CollisionJsonString(bone.parentName) << '}';
  }
  out << "],\"samples\":[";
  if (current) for (size_t n = 0; n < b.samples.count; ++n) {
    if (n) out << ',';
    const auto &p = b.samples.At(n);
    out << "{\"submission\":" << p.submission << ",\"epoch\":" << p.epoch << ",\"frame\":" << p.frame
        << ",\"phase\":" << p.phase << ",\"sourceFrame\":" << CollisionNumber(p.sourceFrame)
        << ",\"playheadFrame\":" << CollisionNumber(p.playheadFrame)
        << ",\"costMs\":" << CollisionNumber(p.costMs) << ",\"matrices\":[";
    for (int k = 0; k < b.count; ++k) {
      if (k) out << ',';
      if (p.matrices[k].known) ClothInputJsonArray(out, p.matrices[k].matrix, 16); else out << "null";
    }
    out << "]}";
  }
  out << "]}";
  return out.str();
}
static std::string CollisionSnapshotJson(const CollisionSnapshot &s) {
  std::ostringstream out;
  out << "{\"schema\":2,\"geometryDefinition\":"
      << CollisionJsonString(eiem_collision::GeometryDefinition)
      << ",\"backend\":" << CollisionJsonString(s.backend)
      << ",\"session\":" << s.session << ",\"generation\":" << s.generation
      << ",\"owner\":" << CollisionJsonString(std::to_string(s.owner).c_str())
      << ",\"frame\":" << s.frame << ",\"scene\":" << s.scene
      << ",\"stage\":" << CollisionJsonString(s.stage) << ",\"coherent\":" << s.coherent
      << ",\"truncated\":" << s.truncated << ",\"geometryMode\":" << s.geometryMode
      << ",\"enumApiReady\":" << s.enumApiReady
      << ",\"inputTrace\":" << ClothInputJson()
      << ",\"solverTrace\":" << ClothContactJson()
      << ",\"bodyTrace\":" << ClothBodyJson()
      << ",\"originalBoneCloth\":" << ClothBoneJson()
      << ",\"boneClothSolver\":" << ClothBoneSolverJson()
      << ",\"captureMs\":" << CollisionNumber(s.captureMs)
      << ",\"discovery\":{\"complete\":" << s.discovery.complete
      << ",\"nodes\":" << s.discovery.nodes << ",\"maxDepth\":" << s.discovery.maxDepth
      << ",\"depthCuts\":" << s.discovery.depthCuts << ",\"nodeCuts\":" << s.discovery.nodeCuts
      << ",\"readFailures\":" << s.discovery.readFailures
      << ",\"capacityCuts\":" << s.discovery.capacityCuts
      << ",\"firstFailure\":" << CollisionJsonString(s.discovery.firstFailure) << "}"
      << ",\"unityFrameMs\":" << CollisionNumber(s.unityFrameMs)
      << ",\"samplingConfiguration\":{\"useAnimatorTransform\":"
      << (s.useAnimatorTransform < 0 ? "null"
          : s.useAnimatorTransform   ? "true"
                                     : "false")
      << ",\"useCrossFrameJob\":"
      << (s.useCrossFrameJob < 0 ? "null"
          : s.useCrossFrameJob   ? "true"
                                 : "false")
      << ",\"perColliderBufferBinding\":\"unknown\"}"
      << ",\"jobCost\":null,\"samplingOrder\":\"unverified\",\"geometrySource\":\"managed-"
         "transform; total-length endpoints follow inspected native formula; no live Job or "
         "rendered mesh depth\",\"cloth\":[";
  bool comma = false;
  for (const auto &c : s.cloth) {
    if (comma)
      out << ',';
    comma = true;
    out << "{\"id\":" << c.id << ",\"name\":" << CollisionJsonString(c.name)
        << ",\"team\":" << c.team
        << ",\"process\":" << CollisionJsonString(std::to_string(c.process).c_str())
        << ",\"serialize\":" << CollisionJsonString(std::to_string(c.serialize).c_str())
        << ",\"type\":" << CollisionJsonString(c.type)
        << ",\"processType\":" << CollisionJsonString(c.processType)
        << ",\"mode\":" << CollisionJsonString(c.mode)
        << ",\"connection\":" << CollisionJsonString(c.connection) << ",\"roots\":" << c.rootCount
        << ",\"collisionBones\":" << c.collisionBoneCount << ",\"stateKnown\":" << c.stateKnown
        << ",\"active\":" << c.active << ",\"running\":" << c.running
        << ",\"enabled\":" << c.enabled << ",\"skip\":" << c.skip << ",\"culled\":" << c.culled
        << ",\"weight\":" << CollisionNumber(c.weight)
        << ",\"propertyWeight\":" << CollisionNumber(c.propertyWeight)
        << ",\"ratio\":" << CollisionNumber(c.ratio) << ",\"blend\":" << CollisionNumber(c.blend)
        << ",\"time\":" << CollisionNumber(c.time)
        << ",\"globalTime\":" << CollisionNumber(c.globalTime)
        << ",\"radius\":" << CollisionCurveJson(c.radius)
        << ",\"limitDistance\":" << CollisionCurveJson(c.limit)
        << ",\"motionKnown\":" << c.motionKnown << ",\"useMaxDistance\":" << c.maxDistance
        << ",\"useBackstop\":" << c.backstop
        << ",\"backstopRadius\":" << CollisionNumber(c.backstopRadius)
        << ",\"motionStiffness\":" << CollisionNumber(c.motionStiffness)
        << ",\"maxDistance\":" << CollisionCurveJson(c.max)
        << ",\"backstopDistance\":" << CollisionCurveJson(c.backstopDistance)
        << ",\"restorationKnown\":" << c.restorationKnown << ",\"useAngleRestoration\":"
        << (c.restorationKnown ? std::to_string(c.restorationEnabled) : "null")
        << ",\"effectiveAngleRestoration\":"
        << (c.effectiveAngleKnown ? std::to_string(c.effectiveAngle) : "null")
        << ",\"weightResetPolicy\":{\"enabled\":"
        << (c.resetPolicyKnown ? std::to_string(c.resetPolicyEnabled) : "null") << ",\"threshold\":"
        << (c.resetPolicyKnown ? CollisionNumber(c.resetWeightThreshold) : "null")
        << ",\"source\":\"configuration-not-live-reset-flag\"}"
        << ",\"angleLimitKnown\":" << c.angleLimitKnown
        << ",\"useAngleLimit\":" << c.angleLimitEnabled
        << ",\"angleRestoration\":" << CollisionCurveJson(c.angleRestoration)
        << ",\"angleLimit\":" << CollisionCurveJson(c.angleLimit)
        << ",\"effectiveMode\":" << CollisionJsonString(c.effectiveModeName)
        << ",\"effectiveModeKnown\":" << c.effectiveModeKnown
        << ",\"proxyEdges\":" << (c.topology.known ? std::to_string(c.topology.edges) : "null")
        << ",\"proxyVertices\":"
        << (c.topology.known ? std::to_string(c.topology.vertices) : "null")
        << ",\"proxyLines\":" << (c.topology.known ? std::to_string(c.topology.lines) : "null")
        << ",\"proxyTriangles\":"
        << (c.topology.known ? std::to_string(c.topology.triangles) : "null")
        << ",\"proxySource\":\"public-count-getters-no-buffer-read\"";
    const auto &diag = c.diagnostics;
    out << ",\"gravityParameters\":{\"serialized\":" << CollisionNumber(diag.gravity)
        << ",\"property\":" << CollisionNumber(diag.gravityProperty)
        << ",\"direction\":" << CollisionVector(CollisionV(diag.direction))
        << ",\"falloff\":" << CollisionNumber(diag.gravityFalloff)
        << ",\"process\":" << CollisionNumber(diag.effectiveGravity)
        << ",\"processDirection\":" << CollisionVector(CollisionV(diag.effectiveDirection))
        << ",\"processFalloff\":" << CollisionNumber(diag.effectiveGravityFalloff)
        << ",\"source\":\"configuration-and-boxed-parameters-not-live-force\"}"
        << ",\"transformBindings\":{\"count\":"
        << (diag.bindingCount >= 0 ? std::to_string(diag.bindingCount) : "null")
        << ",\"complete\":" << diag.bindingsComplete
        << ",\"issue\":" << CollisionJsonString(diag.bindingIssue)
        << ",\"source\":\"ProxyMeshContainer-Transform-getters-not-particle-index\",\"entries\":[";
    for (size_t n = 0; n < diag.bindings.size(); ++n) {
      if (n)
        out << ',';
      const auto &b = diag.bindings[n];
      out << "{\"index\":" << b.index << ",\"id\":" << (b.id ? std::to_string(b.id) : "null")
          << ",\"name\":" << CollisionJsonString(b.name) << '}';
    }
    out << "]}";
    const auto &a = c.attributes;
    out << ",\"assetAttributes\":{\"dictionaryKnown\":" << a.dictionaryKnown
        << ",\"dictionaryCount\":"
        << (a.dictionaryKnown ? std::to_string(a.dictionaryCount) : "null")
        << ",\"flagsKnown\":" << a.flagsKnown
        << ",\"fixedMask\":" << (a.flagsKnown ? std::to_string(a.fixedFlag) : "null")
        << ",\"moveMask\":" << (a.flagsKnown ? std::to_string(a.moveFlag) : "null")
        << ",\"disableCollisionMask\":" << (a.flagsKnown ? std::to_string(a.disableFlag) : "null")
        << ",\"selectionValues\":";
    if (a.selectionKnown) {
      out << '[';
      for (size_t n = 0; n < a.selectionValues.size(); ++n) {
        if (n)
          out << ',';
        out << unsigned(a.selectionValues[n]);
      }
      out << ']';
    } else
      out << "null";
    out << ",\"selectionSource\":\"managed-SelectionData-no-bone-index-mapping-not-live-job\""
        << ",\"usePrebuild\":" << (a.prebuildKnown ? std::to_string(a.prebuild) : "null")
        << ",\"serializedCount\":" << (a.rawKnown ? std::to_string(a.rawCount) : "null")
        << ",\"serializedLength\":" << (a.rawKnown ? std::to_string(a.rawLength) : "null")
        << ",\"serializedBytes\":";
    if (a.rawKnown) {
      out << '[';
      for (size_t n = 0; n < a.rawBytes.size(); ++n) {
        if (n)
          out << ',';
        out << unsigned(a.rawBytes[n]);
      }
      out << ']';
    } else
      out << "null";
    out << ",\"source\":\"managed-authoring-not-live-job; serialized-bytes-opaque\"}}";
  }
  out << "],\"colliders\":[";
  comma = false;
  for (const auto &p : s.colliders) {
    if (comma)
      out << ',';
    comma = true;
    const auto &g = p.geometry;
    out << "{\"id\":" << g.id << ",\"cloth\":" << p.cloth
        << ",\"bone\":" << CollisionJsonString(g.bone)
        << ",\"type\":" << CollisionJsonString(g.type) << ",\"owned\":" << p.owned
        << ",\"parentId\":" << g.parentId << ",\"parentName\":" << CollisionJsonString(g.parentName)
        << ",\"serialized\":" << p.serialized << ",\"process\":" << p.process
        << ",\"team\":" << p.team << ",\"teams\":" << p.teams
        << ",\"registrationKnown\":" << p.registrationKnown << ",\"validGeometry\":" << g.valid
        << ",\"enabled\":" << g.enabled << ",\"active\":" << g.active
        << ",\"uniform\":" << g.uniform << ",\"scale\":" << CollisionVector(CollisionV(g.scale))
        << ",\"center\":" << CollisionVector(CollisionV(g.center))
        << ",\"worldCenter\":" << CollisionVector(CollisionV(g.worldCenter))
        << ",\"size\":" << CollisionVector(CollisionV(g.size))
        << ",\"apiSizeKnown\":" << p.apiSizeKnown
        << ",\"apiSize\":" << (p.apiSizeKnown ? CollisionVector(CollisionV(p.apiSize)) : "null")
        << ",\"direction\":" << CollisionJsonString(g.direction)
        << ",\"flagsKnown\":" << g.flagsKnown << ",\"reverse\":" << g.reverse
        << ",\"alignedOnCenter\":" << g.centered << ",\"separation\":" << g.separated
        << ",\"apiScale\":" << CollisionNumber(g.apiScale)
        << ",\"apiLocalDir\":" << CollisionVector(CollisionV(p.apiLocalDir))
        << ",\"a\":" << CollisionVector(g.world.a) << ",\"b\":" << CollisionVector(g.world.b)
        << ",\"ra\":" << CollisionNumber(g.world.ra) << ",\"rb\":" << CollisionNumber(g.world.rb)
        << "}";
  }
  out << "],\"nodes\":[";
  comma = false;
  for (const auto &n : s.nodes) {
    if (comma)
      out << ',';
    comma = true;
    out << "{\"id\":" << n.id << ",\"parent\":" << n.parent << ",\"cloth\":" << n.cloth
        << ",\"name\":" << CollisionJsonString(n.name)
        << ",\"role\":" << CollisionJsonString(n.role) << ",\"selected\":" << n.collisionSelected
        << ",\"p\":" << CollisionVector(CollisionV(n.position))
        << ",\"assetAttribute\":{\"queried\":" << n.attribute.queried
        << ",\"present\":" << n.attribute.present
        << ",\"value\":" << (n.attribute.known ? std::to_string(n.attribute.value) : "null")
        << "}}";
  }
  out << "]}";
  return out.str();
}
struct CollisionSnapshotFileResult {
  bool ok = false;
  DWORD error = 0;
  const char *phase = "resolve-path";
  char path[MAX_PATH * 4]{};
};
static bool CollisionSnapshotDirectory(wchar_t (&directory)[MAX_PATH], DWORD &error) {
  DWORD length = GetModuleFileNameW(nullptr, directory, MAX_PATH);
  if (!length || length >= MAX_PATH) {
    error = length ? ERROR_FILENAME_EXCED_RANGE : GetLastError();
    return false;
  }
  wchar_t *slash = wcsrchr(directory, L'\\');
  if (!slash) {
    error = ERROR_BAD_PATHNAME;
    return false;
  }
  slash[1] = 0;
  if (wcslen(directory) + 6 >= MAX_PATH) {
    error = ERROR_FILENAME_EXCED_RANGE;
    return false;
  }
  wcscat_s(directory, L"plugin");
  return true;
}
static CollisionSnapshotFileResult CollisionWriteSnapshotFile(const wchar_t *directory,
                                                              const std::string &json,
                                                              uint64_t session, int frame,
                                                              unsigned request) {
  CollisionSnapshotFileResult result{};
  wchar_t path[MAX_PATH]{};
  if (_snwprintf_s(path, _TRUNCATE, L"%s\\eiem_cloth_snapshot_%lu_%llu_%d_%u.json", directory,
                   GetCurrentProcessId(), (unsigned long long)session, frame, request) < 0) {
    result.error = ERROR_FILENAME_EXCED_RANGE;
    return result;
  }
  if (!WideCharToMultiByte(CP_UTF8, 0, path, -1, result.path, sizeof(result.path), nullptr,
                           nullptr)) {
    result.error = GetLastError();
    return result;
  }
  result.phase = "create-directory";
  if (!CreateDirectoryW(directory, nullptr) && GetLastError() != ERROR_ALREADY_EXISTS) {
    result.error = GetLastError();
    return result;
  }
  if (GetFileAttributesW(directory) == INVALID_FILE_ATTRIBUTES ||
      !(GetFileAttributesW(directory) & FILE_ATTRIBUTE_DIRECTORY)) {
    result.error = ERROR_DIRECTORY;
    return result;
  }
  result.phase = "create-file";
  HANDLE file = CreateFileW(path, GENERIC_WRITE, FILE_SHARE_READ, nullptr, CREATE_NEW,
                            FILE_ATTRIBUTE_NORMAL, nullptr);
  if (file == INVALID_HANDLE_VALUE) {
    result.error = GetLastError();
    return result;
  }
  result.phase = "write-file";
  DWORD written = 0;
  if (json.size() > MAXDWORD)
    result.error = ERROR_FILE_TOO_LARGE;
  else if (!WriteFile(file, json.data(), static_cast<DWORD>(json.size()), &written, nullptr))
    result.error = GetLastError();
  else if (written != json.size())
    result.error = ERROR_WRITE_FAULT;
  if (!CloseHandle(file) && !result.error)
    result.error = GetLastError();
  if (result.error) {
    DeleteFileW(path);
    return result;
  }
  result.ok = true;
  result.phase = "complete";
  return result;
}
static void CollisionExportSnapshot(const char *stage) {
  Log("[CLOTH-CONTACT-EXPORT] event=capture request=%u session=%llu frame=%d stage=%s",
      s_collisionExportSeen, (unsigned long long)s_cloth.owner.session, ClothFrame(), stage);
  auto snapshot = CollisionCollect(stage);
  if (!snapshot.coherent) {
    CollisionSetExportNote(-1, "incoherent-owner-or-frame");
    Log("[CLOTH-CONTACT-EXPORT] cancelled-incoherent-owner-or-frame");
    return;
  }
  std::string json = CollisionSnapshotJson(snapshot);
  wchar_t directory[MAX_PATH]{};
  CollisionSnapshotFileResult result{};
  if (CollisionSnapshotDirectory(directory, result.error)) {
    result = CollisionWriteSnapshotFile(directory, json, snapshot.session, snapshot.frame,
                                        s_collisionExportSeen);
  }
  CollisionSetExportNote(result.ok ? 1 : -1, result.phase, result.error, result.path);
  Log("[CLOTH-CONTACT-EXPORT] success=%d phase=%s win32Error=%lu path='%s' frame=%d coherent=%d "
      "truncated=%d cloth=%zu "
      "colliders=%zu nodes=%zu captureMs=%g unityFrameMs=%g jobMs=unknown samplingOrder=unverified",
      result.ok, result.phase, result.error, result.path, snapshot.frame, snapshot.coherent,
      snapshot.truncated, snapshot.cloth.size(), snapshot.colliders.size(), snapshot.nodes.size(),
      snapshot.captureMs, snapshot.unityFrameMs);
}
static void CollisionTryExportSnapshot(const char *stage) {
  __try {
    CollisionExportSnapshot(stage);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    CollisionSetExportNote(-1, "snapshot-read-or-encode-exception", GetExceptionCode());
    Log("[CLOTH-CONTACT-EXPORT] event=failed phase=read-or-encode exception=0x%08lX "
        "baseSimulation=retained",
        GetExceptionCode());
  }
}
