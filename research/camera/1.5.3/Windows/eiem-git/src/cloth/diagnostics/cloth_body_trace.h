#pragma once
constexpr int ClothBodyBones = 128;
struct ClothBodyBone {
  ClothRef ref{};
  int parent = 0;
  char name[128]{}, parentName[128]{};
};
struct ClothBodySample {
  uint64_t submission = 0, epoch = 0;
  int frame = -1, phase = 0;
  double sourceFrame = NAN, playheadFrame = NAN, costMs = 0;
  ClothInputPose matrices[ClothBodyBones]{};
};
struct ClothBodyTrace {
  ClothRef renderer{}, mesh{};
  ClothBodyBone bones[ClothBodyBones]{};
  int count = 0, vertices = 0, lastFrame = -1, lastPhase = -1;
  bool attempted = false, failed = false;
  char meshName[128]{}, issue[128]{"awaiting-body-renderer"};
  void *getBones = nullptr, *getMesh = nullptr;
  eiem_cloth_input::CostBudget budget{};
  eiem_cloth_input::Ring<ClothBodySample, 32> samples;
} static s_clothBody;

static void ClothBodyClear() {
  if (!ClothOnMainThread()) return;
  auto &b = s_clothBody;
  ClothFree(b.renderer); ClothFree(b.mesh);
  for (auto &bone : b.bones) ClothFree(bone.ref);
  b.samples.Clear(); b.count = b.vertices = 0; b.lastFrame = b.lastPhase = -1;
  b.attempted = b.failed = false; b.budget = {};
  strcpy_s(b.issue, "awaiting-body-renderer");
}
static bool ClothBodyFail(const char *reason) {
  auto &b = s_clothBody;
  if (!b.failed) Log("[CLOTH-BODY] session=%llu frame=%d issue=%s readOnly=1",
      (unsigned long long)s_cloth.owner.session, ClothFrame(), reason);
  b.failed = true; strncpy_s(b.issue, reason, _TRUNCATE);
  b.samples.Clear();
  return false;
}
static bool ClothBodyArray(void *renderer, void *&array, uintptr_t &count) {
  return ClothInvoke(s_clothBody.getBones, renderer, nullptr, array) &&
      ClothArray(array, "UnityEngine.Transform[]", count) && count <= ClothBodyBones;
}
static bool ClothBodyPrepare() {
  auto &b = s_clothBody;
  if (b.attempted) return !b.failed && b.count > 0;
  b.attempted = true;
  void *root = CollisionTransform(ClothTarget(s_cloth.animator));
  if (!root) return ClothBodyFail("owner-root-unavailable");
  struct Node { void *t; int depth; };
  std::vector<Node> pending{{root, 0}};
  void *match = nullptr;
  for (size_t n = 0; n < pending.size(); ++n) {
    if (pending.size() > 2048) return ClothBodyFail("owner-hierarchy-over-budget");
    const auto node = pending[n];
    char name[128]{}; CollisionName(node.t, name, sizeof(name));
    if (!strcmp(name, "S_actor_wulfa_body_01_lod0")) {
      if (match) return ClothBodyFail("ambiguous-body-renderer-name");
      match = node.t;
    }
    int children = -1;
    if (!ClothValue(s_clothUnity.childCount, node.t, children) || children < 0 || children > 128 ||
        (node.depth >= 32 && children)) return ClothBodyFail("incomplete-owner-hierarchy");
    for (int k = 0; k < children; ++k) {
      void *child = nullptr, *args[] = {&k};
      if (!ClothInvoke(s_clothUnity.child, node.t, args, child) || !child)
        return ClothBodyFail("owner-child-unreadable");
      pending.push_back({child, node.depth + 1});
    }
  }
  if (!match) return ClothBodyFail("Rossi-body-renderer-not-found");
  void *type = il2cpp_type_get_object(il2cpp_class_get_type(g_componentClass));
  void *go = nullptr, *components = nullptr, *args[] = {type}, *renderer = nullptr;
  uintptr_t count = 0;
  if (!type || !ClothInvoke(s_clothUnity.getGO, match, nullptr, go) ||
      !ClothInvoke(s_clothUnity.components, go, args, components) ||
      !ClothArray(components, "UnityEngine.Component[]", count) || count > 32)
    return ClothBodyFail("renderer-components-unavailable");
  for (uintptr_t n = 0; n < count; ++n) {
    void *c = reinterpret_cast<void **>((char *)components + 32)[n];
    if (!c || !ClothTypeIs(il2cpp_class_get_type(il2cpp_object_get_class(c)), "UnityEngine.SkinnedMeshRenderer")) continue;
    if (renderer) return ClothBodyFail("ambiguous-skinned-renderer");
    renderer = c;
  }
  if (!renderer) return ClothBodyFail("skinned-renderer-unavailable");
  auto cls = il2cpp_object_get_class(renderer);
  b.getBones = ClothMethod(cls, "get_bones", "UnityEngine.Transform[]");
  b.getMesh = ClothMethod(cls, "get_sharedMesh", "UnityEngine.Mesh");
  void *mesh = nullptr, *array = nullptr;
  if (!ClothInvoke(b.getMesh, renderer, nullptr, mesh) || !mesh ||
      !ClothValue(ClothMethod(il2cpp_object_get_class(mesh), "get_vertexCount", "System.Int32"), mesh, b.vertices) ||
      b.vertices <= 0 || !ClothBodyArray(renderer, array, count))
    return ClothBodyFail("body-mesh-or-bone-array-unavailable");
  b.renderer = ClothProtect(renderer); b.mesh = ClothProtect(mesh);
  if (!b.renderer.handle || !b.mesh.handle) return ClothBodyFail("body-reference-protection-failed");
  CollisionName(mesh, b.meshName, sizeof(b.meshName));
  b.count = int(count);
  for (int n = 0; n < b.count; ++n) {
    void *t = reinterpret_cast<void **>((char *)array + 32)[n];
    if (!t || !ClothAnchorUnderOwner(t)) return ClothBodyFail("body-bone-outside-owner");
    auto &bone = b.bones[n]; bone.ref = ClothProtect(t);
    void *parent = CollisionParent(t);
    if (!bone.ref.handle || !parent || !ClothValue(s_clothUnity.instance, parent, bone.parent))
      return ClothBodyFail("body-bone-parent-unavailable");
    CollisionName(t, bone.name, sizeof(bone.name));
    CollisionName(parent, bone.parentName, sizeof(bone.parentName));
  }
  strcpy_s(b.issue, "body-matrices-awaiting-capture");
  Log("[CLOTH-BODY] session=%llu bones=%d vertices=%d mesh='%s' readOnly=1 rendererOrder=1",
      (unsigned long long)s_cloth.owner.session, b.count, b.vertices, b.meshName);
  return true;
}
static bool ClothBodyIdentity() {
  auto &b = s_clothBody;
  if (!ClothInputIdentity() || b.failed || !b.count) return false;
  void *r = ClothTarget(b.renderer), *mesh = nullptr, *array = nullptr;
  uintptr_t count = 0;
  if (!r || !ClothAnchorUnderOwner(CollisionTransform(r)) ||
      !ClothInvoke(b.getMesh, r, nullptr, mesh) || mesh != ClothTarget(b.mesh) ||
      !ClothBodyArray(r, array, count) || count != uintptr_t(b.count)) return false;
  for (int n = 0; n < b.count; ++n) {
    void *t = reinterpret_cast<void **>((char *)array + 32)[n];
    int parent = 0;
    if (!t || t != ClothTarget(b.bones[n].ref) || !ClothAnchorUnderOwner(t) ||
        !ClothValue(s_clothUnity.instance, CollisionParent(t), parent) || parent != b.bones[n].parent)
      return false;
  }
  return true;
}
static void ClothBodyCapture(const ClothInputSample &input) {
  auto &b = s_clothBody;
  const bool finalIK = input.phase == 1 && b.lastPhase == 0 && input.frame == b.lastFrame;
  if (b.failed || (!finalIK && b.lastFrame >= 0 && input.frame - b.lastFrame < 8)) return;
  LARGE_INTEGER begin{}, end{}, frequency{};
  QueryPerformanceFrequency(&frequency); QueryPerformanceCounter(&begin);
  if (!ClothBodyPrepare()) return;
  if (!ClothBodyIdentity()) { ClothBodyFail("body-identity-replaced"); return; }
  ClothBodySample sample{};
  sample.frame = input.frame; sample.submission = input.submission; sample.phase = input.phase;
  sample.sourceFrame = input.sourceFrame; sample.playheadFrame = input.playheadFrame; sample.epoch = input.epoch;
  for (int n = 0; n < b.count; ++n)
    if (!ClothInputPoseRead(ClothTarget(b.bones[n].ref), sample.matrices[n])) {
      ClothBodyFail("body-matrix-unavailable"); return;
    }
  if (!ClothInputIdentity() || ClothFrame() != input.frame) {
    ClothBodyFail("body-capture-identity-or-frame-changed"); return;
  }
  QueryPerformanceCounter(&end);
  sample.costMs = double(end.QuadPart - begin.QuadPart) * 1000 / frequency.QuadPart;
  b.lastFrame = input.frame; b.lastPhase = input.phase; b.samples.Push(sample);
  strcpy_s(b.issue, "body-matrices-captured-contact-unverified");
  if (b.budget.Observe(sample.costMs)) ClothBodyFail("body-observer-cost-budget-exceeded");
}
