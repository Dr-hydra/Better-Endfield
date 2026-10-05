#pragma once
// Adapted from Sasye/EIEM (AGPL-3.0), commit 94aa8391ef9677146e3e5c456b57dddbd0cc8546.
// Source: https://github.com/Sasye/EIEM/tree/94aa8391ef9677146e3e5c456b57dddbd0cc8546/src


static bool ClothFinitePosition(Vec3 v) {
  return std::isfinite(v.x) && std::isfinite(v.y) && std::isfinite(v.z);
}
static bool ClothSameLocal(Vec3 a, Quat q, Vec3 b, Quat r) {
  const double qn = double(q.x)*q.x + double(q.y)*q.y + double(q.z)*q.z + double(q.w)*q.w;
  const double rn = double(r.x)*r.x + double(r.y)*r.y + double(r.z)*r.z + double(r.w)*r.w;
  const double dot = double(q.x)*r.x + double(q.y)*r.y + double(q.z)*r.z + double(q.w)*r.w;
  return ClothFinitePosition(a) && ClothFinitePosition(b) && qn > 1e-10 && rn > 1e-10 &&
      std::isfinite(qn) && std::isfinite(rn) && fabs(a.x-b.x) <= 0.00001 &&
      fabs(a.y-b.y) <= 0.00001 && fabs(a.z-b.z) <= 0.00001 &&
      fabs(dot) / sqrt(qn*rn) >= 0.999999;
}
static bool ClothReadLocal(void *transform, Vec3 &position, Quat &rotation) {
  if (!transform) return false;
  void *cls = il2cpp_object_get_class(transform);
  return ClothValue(ClothMethod(cls, "get_localPosition", "UnityEngine.Vector3"), transform, position) &&
      ClothValue(ClothMethod(cls, "get_localRotation", "UnityEngine.Quaternion"), transform, rotation) &&
      ClothSameLocal(position, rotation, position, rotation);
}
static void ClothAnchorLog(const ClothAnchor &a, const char *event, const char *reason) {
  Log("[CLOTH-ANCHOR-%s] backend=%s generation=%llu session=%llu owner=%p frame=%d instance=%d bone='%s' parent='%s' bindKnown=%d sent=%d confirmed=%d originalLocal=(%g,%g,%g) originalRotation=(%g,%g,%g,%g) targetLocal=(%g,%g,%g) targetRotation=(%g,%g,%g,%g) observedLocal=(%g,%g,%g) observedRotation=(%g,%g,%g,%g) bindReason=%s reason=%s",
      event, "mmd",
      (unsigned long long)s_cloth.owner.generation, (unsigned long long)s_cloth.owner.session,
      reinterpret_cast<void *>(s_cloth.owner.character), ClothFrame(), a.ref.id.instance,
      a.name, a.parentName, a.bindKnown, a.sent, a.confirmed,
      a.originalPosition.x, a.originalPosition.y, a.originalPosition.z,
      a.originalRotation.x, a.originalRotation.y, a.originalRotation.z, a.originalRotation.w,
      a.bindPosition.x, a.bindPosition.y, a.bindPosition.z,
      a.bindRotation.x, a.bindRotation.y, a.bindRotation.z, a.bindRotation.w,
      a.observedPosition.x, a.observedPosition.y, a.observedPosition.z,
      a.observedRotation.x, a.observedRotation.y, a.observedRotation.z, a.observedRotation.w,
      a.bindReason, reason);
}
static bool ClothArray(void *array, const char *type, uintptr_t &count) {
  if (!array || !ClothTypeIs(il2cpp_class_get_type(il2cpp_object_get_class(array)), type)) return false;
  memcpy(&count, (char *)array + 24, sizeof(count));
  return count > 0 && count <= 8192;
}
static bool ClothRecordString(void *record, int offset, char *text, size_t capacity, bool *absent = nullptr) {
  void *str = nullptr;
  memcpy(&str, (char *)record + offset, sizeof(str));
  int32_t length = 0;
  if (str) memcpy(&length, (char *)str + 16, sizeof(length));
  if (absent) *absent = !str || length == 0;
  return ReadStrUtf8(str, text, static_cast<int>(capacity)) > 0;
}
static int ClothValueOffset(void *cls, const char *name, const char *type, int stride, int size) {
  int offset = ClothFieldOffset(cls, name, type) - 16;
  return offset >= 0 && offset + size <= stride ? offset : -1;
}
static bool ClothFindBindRecord(void *skeleton, void *human, void *boneClass, void *humanClass,
                                ClothAnchor &a) {
  uintptr_t count = 0, humanCount = 0;
  a.bindReason = "avatar-array-type-or-count-unavailable";
  if (!ClothArray(skeleton, "UnityEngine.SkeletonBone[]", count) ||
      !ClothArray(human, "UnityEngine.HumanBone[]", humanCount)) return false;
  uint32_t align = 0;
  const int stride = il2cpp_class_value_size(boneClass, &align);
  const int humanStride = il2cpp_class_value_size(humanClass, &align);
  a.bindReason = "avatar-value-size-out-of-bounds";
  if (stride < 40 || stride > 256 || humanStride < 16 || humanStride > 256) return false;
  const int name = ClothValueOffset(boneClass, "name", "System.String", stride, sizeof(void *));
  const int parent = ClothValueOffset(boneClass, "parentName", "System.String", stride, sizeof(void *));
  const int pos = ClothValueOffset(boneClass, "position", "UnityEngine.Vector3", stride, sizeof(Vec3));
  const int rot = ClothValueOffset(boneClass, "rotation", "UnityEngine.Quaternion", stride, sizeof(Quat));
  const int humanName = ClothValueOffset(humanClass, "m_BoneName", "System.String", humanStride, sizeof(void *));
  a.bindReason = "avatar-field-type-or-boxed-layout-unavailable";
  if (name < 0 || parent < 0 || pos < 0 || rot < 0 || humanName < 0) return false;
  for (uintptr_t n = 0; n < humanCount; ++n) {
    char value[128]{};
    a.bindReason = "human-bone-name-unreadable";
    if (!ClothRecordString((char *)human + 32 + n*humanStride, humanName, value, sizeof(value))) return false;
    if (!strcmp(value, a.name)) { a.bindReason = "humanoid-owned-no-anchor-write"; return false; }
  }
  unsigned matches = 0;
  for (uintptr_t n = 0; n < count; ++n) {
    void *record = (char *)skeleton + 32 + n*stride;
    char value[128]{}, parentValue[128]{};
    bool absent = false;
    if (!ClothRecordString(record, name, value, sizeof(value), &absent)) {
      if (absent) continue;
      a.bindReason = "skeleton-name-unreadable-uniqueness-unproven"; return false;
    }
    if (strcmp(value, a.name)) continue;
    ++matches;
    a.bindReason = "skeleton-parent-name-unavailable-or-mismatch";
    if (!ClothRecordString(record, parent, parentValue, sizeof(parentValue)) || strcmp(parentValue, a.parentName))
      return false;
    memcpy(&a.bindPosition, (char *)record + pos, sizeof(a.bindPosition));
    memcpy(&a.bindRotation, (char *)record + rot, sizeof(a.bindRotation));
  }
  a.bindReason = matches == 0 ? "skeleton-name-not-found" : "skeleton-name-ambiguous";
  if (matches != 1) return false;
  a.bindReason = "skeleton-local-pose-invalid";
  if (!ClothSameLocal(a.bindPosition, a.bindRotation, a.bindPosition, a.bindRotation)) return false;
  a.bindReason = "unique-avatar-local-pose-parent-matched-nonhuman";
  return true;
}
static bool ClothResolveAnchorBind(ClothAnchor &a) {
  void *animator = ClothTarget(s_cloth.animator), *avatar = nullptr, *description = nullptr;
  a.bindReason = "animator-avatar-api-or-value-size-api-unavailable";
  if (!animator || !il2cpp_class_value_size ||
      !ClothInvoke(ClothMethod(il2cpp_object_get_class(animator), "get_avatar", "UnityEngine.Avatar"), animator, nullptr, avatar) || !avatar)
    return false;
  a.bindReason = "avatar-human-description-api-unavailable";
  if (!ClothInvoke(ClothMethod(il2cpp_object_get_class(avatar), "get_humanDescription", "UnityEngine.HumanDescription"), avatar, nullptr, description) || !description)
    return false;
  void *skeleton = nullptr, *human = nullptr;
  a.bindReason = "human-description-array-fields-unavailable";
  if (!ClothField(description, "skeleton", "UnityEngine.SkeletonBone[]", skeleton) ||
      !ClothField(description, "human", "UnityEngine.HumanBone[]", human)) return false;
  size_t count = 0;
  void **assemblies = il2cpp_domain_get_assemblies(il2cpp_domain_get(), &count);
  void *boneClass = FindClass("UnityEngine", "SkeletonBone", assemblies, count);
  void *humanClass = FindClass("UnityEngine", "HumanBone", assemblies, count);
  a.bindReason = "skeleton-or-human-value-class-unavailable";
  if (!boneClass || !humanClass) return false;
  return ClothFindBindRecord(skeleton, human, boneClass, humanClass, a);
}
static bool ClothAnchorUnderOwner(void *transform) {
  void *root = nullptr, *animator = ClothTarget(s_cloth.animator);
  if (!animator || !ClothInvoke(s_clothUnity.getTransform, animator, nullptr, root) || !root || transform == root) return false;
  for (int depth = 0; transform && depth < 128; ++depth) {
    if (transform == root) return true;
    void *parent = nullptr;
    if (!ClothInvoke(ClothMethod(il2cpp_object_get_class(transform), "get_parent", "UnityEngine.Transform"), transform, nullptr, parent)) return false;
    transform = parent;
  }
  return false;
}
static ClothLife ClothAnchorParentState(const ClothAnchor &a, void *transform) {
  void *parent = nullptr, *saved = nullptr;
  auto life = ClothInspect(a.parent, saved);
  if (life != ClothLife::Alive) return life;
  if (!ClothInvoke(ClothMethod(il2cpp_object_get_class(transform), "get_parent", "UnityEngine.Transform"), transform, nullptr, parent))
    return ClothLife::Unreadable;
  return parent == saved ? ClothLife::Alive : ClothLife::Destroyed;
}
static void ClothCaptureAnchors(ClothInstance &i, int member, bool beforeSuppression) {
  if (i.anchorsCaptured) return;
  i.anchorsCaptured = true;
  if (!beforeSuppression) { ClothLog("ANCHOR-COVERAGE", &i, "late-discovery-no-native-pose-snapshot-no-anchor-write"); return; }
  __try {
    void *roots = nullptr;
    if (!ClothField(i.last.serialize, "rootBones", "System.Collections.Generic.List<UnityEngine.Transform>", roots) || !roots) {
      ClothLog("ANCHOR-COVERAGE", &i, "root-list-unavailable-no-anchor-write"); return;
    }
    void *cls = il2cpp_object_get_class(roots);
    int count = 0;
    if (!ClothValue(ClothMethod(cls, "get_Count", "System.Int32"), roots, count) || count < 0 || count > 128) {
      ClothLog("ANCHOR-COVERAGE", &i, "root-count-unavailable-or-over-budget-no-anchor-write"); return;
    }
    void *item = ClothMethod(cls, "get_Item", "UnityEngine.Transform", "System.Int32");
    int captured = 0, bound = 0;
    for (int n = 0; n < count; ++n) {
      void *root = nullptr, *args[] = {&n};
      if (!ClothInvoke(item, roots, args, root) || !root || !ClothAnchorUnderOwner(root)) continue;
      ClothAnchor value{};
      value.ref = ClothProtect(root);
      if (!value.ref.handle) continue;
      if (auto *existing = s_cloth.anchors.Find(value.ref.id)) {
        existing->members |= uint64_t(1) << member; ClothFree(value.ref); ++captured; bound += existing->bindKnown ? 1 : 0; continue;
      }
      void *parent = nullptr, *name = nullptr;
      if (ClothInvoke(ClothMethod(il2cpp_object_get_class(root), "get_parent", "UnityEngine.Transform"), root, nullptr, parent))
        value.parent = ClothProtect(parent);
      bool ok = value.parent.handle && ClothReadLocal(root, value.originalPosition, value.originalRotation);
      value.observedPosition = value.originalPosition; value.observedRotation = value.originalRotation;
      if (ClothInvoke(s_clothUnity.name, root, nullptr, name)) ReadStrUtf8(name, value.name, sizeof(value.name));
      name = nullptr;
      if (ClothInvoke(s_clothUnity.name, parent, nullptr, name)) ReadStrUtf8(name, value.parentName, sizeof(value.parentName));
      if (!ok || !value.name[0] || !value.parentName[0]) { ClothFree(value.ref); ClothFree(value.parent); continue; }
      value.members = uint64_t(1) << member;
      auto *saved = s_cloth.anchors.Capture(value.ref.id, value);
      if (!saved) { ClothFree(value.ref); ClothFree(value.parent); break; }
      ++captured;
      saved->bindKnown = ClothResolveAnchorBind(*saved);
      bound += saved->bindKnown ? 1 : 0;
      ClothAnchorLog(*saved, "SNAPSHOT", saved->bindKnown ? "unique-avatar-local-pose-parent-matched-nonhuman" : "bind-missing-ambiguous-parent-mismatch-or-human-no-write");
    }
    Log("[CLOTH-ANCHOR-COVERAGE] session=%llu instance=%d declared=%d captured=%d bindMatched=%d discoveryComplete=%d limits=128-shared-root-identities",
        (unsigned long long)s_cloth.owner.session, i.ref.id.instance, count, captured, bound, captured == count);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    ClothLog("ANCHOR-COVERAGE", &i, "metadata-exception-no-unknown-bind-writes");
  }
}
static bool ClothWriteAnchorLocal(void *transform, Vec3 position, Quat rotation, bool restore = false) {
  if (!restore && !ClothOwns(s_cloth.owner)) return false;
  void *cls = il2cpp_object_get_class(transform), *unused = nullptr;
  void *pos = ClothMethod(cls, "set_localPosition", "System.Void", "UnityEngine.Vector3");
  void *rot = ClothMethod(cls, "set_localRotation", "System.Void", "UnityEngine.Quaternion");
  if (!pos || !rot) return false;
  void *args[] = {&position}, *rotArgs[] = {&rotation};
  return ClothInvoke(pos, transform, args, unused) && (restore || ClothOwns(s_cloth.owner)) &&
      ClothInvoke(rot, transform, rotArgs, unused);
}
static bool ClothRestoreAnchor(ClothAnchor &a) {
  if (!a.changed) return true;
  void *obj = nullptr;
  const auto life = ClothInspect(a.ref, obj);
  if (life == ClothLife::Unreadable) return false;
  if (life == ClothLife::Destroyed) { a.changed = false; return true; }
  const auto parentState = ClothAnchorParentState(a, obj);
  if (parentState == ClothLife::Unreadable) return false;
  if (parentState == ClothLife::Destroyed) {
    a.changed = false; ClothAnchorLog(a, "RESTORE", "parent-changed-preserve-native-hierarchy"); return true;
  }
  Vec3 p{}; Quat q{};
  bool ok = ClothWriteAnchorLocal(obj, a.originalPosition, a.originalRotation, true) &&
      ClothReadLocal(obj, p, q) && ClothSameLocal(p, q, a.originalPosition, a.originalRotation);
  if (ok) { a.observedPosition = p; a.observedRotation = q; }
  if (ok) a.changed = false;
  ClothAnchorLog(a, "RESTORE", ok ? "original-local-pose-read-back" : "failed-snapshot-retained");
  return ok;
}
static bool ClothUpdateAnchors(ClothInstance &i, int member, int frame, ClothBoneGuard guard) {
  i.anchorsPolling = false;
  if (!i.last.state.readable || !i.last.state.active || !i.last.state.running || !i.last.state.valid ||
      !i.last.state.enabled || !i.last.state.processEnabled || i.last.state.skip ||
      i.last.state.culled || i.last.state.paused || !std::isfinite(i.originalWeight) ||
      poser_cloth::WeightAtTarget(i.originalWeight)) return true;
  for (size_t n = 0; n < s_cloth.anchors.count; ++n) {
    auto &a = s_cloth.anchors.entries[n].value;
    if (!(a.members & (uint64_t(1) << member)) || a.skipped || !a.bindKnown || a.lastFrame == frame) continue;
    a.lastFrame = frame;
    void *root = nullptr;
    const auto rootState = ClothInspect(a.ref, root);
    if (rootState == ClothLife::Unreadable) return false;
    if (rootState == ClothLife::Destroyed) root = nullptr;
    const auto parentState = root ? ClothAnchorParentState(a, root) : ClothLife::Destroyed;
    if (parentState == ClothLife::Unreadable) return false;
    if (!root || parentState == ClothLife::Destroyed || !ClothAnchorUnderOwner(root)) {
      a.skipped = true; ClothAnchorLog(a, "SKIP", "root-destroyed-reparented-or-owner-mismatch"); continue;
    }
    if (!guard || !guard(root)) {
      if (a.changed) return false;
      a.skipped = true; ClothAnchorLog(a, "SKIP", "motion-owner-exclusion-or-no-owner-guard"); continue;
    }
    void *roots = nullptr, *result = nullptr, *args[] = {root};
    bool stillMember = ClothField(i.last.serialize, "rootBones", "System.Collections.Generic.List<UnityEngine.Transform>", roots) && roots &&
        ClothInvoke(ClothMethod(il2cpp_object_get_class(roots), "Contains", "System.Boolean", "UnityEngine.Transform"), roots, args, result) && result && ClothUnboxBool(result);
    if (!stillMember) {
      ClothAnchorLog(a, "SKIP", "root-membership-unconfirmed");
      if (a.changed) return false;
      a.skipped = true; continue;
    }
    Vec3 p{}; Quat q{};
    if (!ClothReadLocal(root, p, q)) return false;
    a.observedPosition = p; a.observedRotation = q;
    if (a.sent) {
      if (!ClothSameLocal(p, q, a.bindPosition, a.bindRotation)) {
        ClothAnchorLog(a, "OVERWRITE", "target-local-readback-differs-no-reassert"); return false;
      }
      if (!a.confirmed && ++a.confirmReads >= 2) {
        a.confirmed = true; ClothAnchorLog(a, "READBACK", "avatar-local-pose-confirmed-visual-unverified");
      }
      i.anchorsPolling |= !a.confirmed;
      continue;
    }
    if (!ClothSameLocal(p, q, a.originalPosition, a.originalRotation)) {
      a.skipped = true; ClothAnchorLog(a, "SKIP", "root-already-updated-after-owner-pose"); continue;
    }
    if (ClothSameLocal(p, q, a.bindPosition, a.bindRotation)) {
      a.skipped = true; ClothAnchorLog(a, "SKIP", "root-already-at-avatar-local-pose"); continue;
    }
    i.anchorsPolling = true;
    if (++a.stableReads < 2) continue;
    if (!ClothOwns(s_cloth.owner)) return false;
    a.sent = a.changed = true;
    bool ok = ClothWriteAnchorLocal(root, a.bindPosition, a.bindRotation);
    ClothAnchorLog(a, "COMMAND", ok ? "frozen-root-local-pose-issued-awaiting-readback" : "setter-failed-rollback-pending");
    if (!ok) return false;
  }
  return true;
}
