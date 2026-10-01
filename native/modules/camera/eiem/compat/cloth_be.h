#pragma once
// Better Endfield side of EIEM's cloth playback service (upstream/cloth.h).
//
// Kept from EIEM: the base service. While a slot's DirectVmd session runs it
// finds the character's BeyondBoneCloth components, forces simulate weight 1
// and animation pose ratio 0 (so cloth is not pulled back towards the native
// animation's cloth pose), keeps them enabled and built, parks non-humanoid
// root anchors at their Avatar pose, and restores everything on release.
//
// Not vendored: the clothing enhancement (cloth/core/cloth_collision.h,
// bonecloth/, collision/, assets/, generated/). It only acts on hand-authored
// or file-derived profiles of the original outfits and swaps renderer meshes,
// which conflicts with custom models. The stubs below are its "nothing
// leased, nothing pending, disabled" state, so the playback gate stays idle.
//
// Added here: the cloth mode (g_beClothMode, set from EiemBody::Options):
//   0 game original: the service does not start (pre-port behaviour),
//   1 stable: EIEM's base service,
//   2 freeze: components are disabled for the session and bones follow the
//     body rigidly; restore re-enables them.
// plus a one-time report of every component and its colliders.

// --- enhancement stubs ---------------------------------------------------------
static std::atomic<bool> s_clothAutoEnabled{false};
struct BeClothBoneSlotStub {
  bool failed = false, stopRequested = false, teamModeConfirmed = false;
  struct { bool cancelled = false; } tx;
  struct { bool requested = false, solverConfirmed = false; } local;
  char failure[1]{}, issue[1]{};
};
static BeClothBoneSlotStub s_clothBoneSlots[1];
static int s_clothBoneCount = 0;
static struct { bool pending = false; char failure[1]{}; } s_clothBone;
static uint64_t s_clothAutoTriedSession = 0, s_clothAutoTriedGeneration = 0;
static bool s_clothBoneNoMatch = false, s_clothAutoDeferred = false;
static bool s_clothAutoWaiting = false, s_clothBoneResolved = false;
static bool ClothBoneAppliedKind(const BeClothBoneSlotStub &, const eiem_cloth::Owner &) { return false; }
static bool ClothBonePending() { return false; }
static bool ClothBoneLeased() { return false; }
static bool ClothBoneLeasedInstance(const ClothInstance &) { return false; }
static bool ClothBoneOwnsAnchor(const ClothAnchor &) { return false; }
static bool ClothOwnedRoot(void *) { return false; }
static void ClothShoulderDriverMaintenance() {}
static void ClothCollisionServiceUi() {}
static void ClothCollisionMaintenance() {}
static void ClothCollisionAfterPose(const char *, int) {}
static void ClothCollisionRelease(const char *) {}
static void ClothInputClear() {}
static void ClothInputSubmit(const char *, double) {}

// --- cloth mode ------------------------------------------------------------------
static bool BeClothServiceEnabled() {
  return g_beClothMode.load(std::memory_order_acquire) != 0;
}
static bool BeClothFreezeRequested() {
  return g_beClothMode.load(std::memory_order_acquire) == 2;
}
// Re-asserted every audit poll in case the game re-enables the component;
// ClothRestore puts the original enabled state back (changedEnabled).
static void BeClothFreeze(ClothInstance &i) {
  if (!i.last.state.readable || !i.capturedEnabled || !i.last.state.enabled) return;
  void *obj = ClothTarget(i.ref), *unused = nullptr;
  bool value = false;
  void *args[] = {&value};
  i.changedEnabled = true;
  const bool ok = obj && ClothInvoke(s_clothUnity.setEnabled, obj, args, unused);
  ClothLog("FREEZE", &i, ok ? "component-disabled-until-release" : "disable-failed");
}

// --- report ------------------------------------------------------------------------
// Session that last reported instance n; one report per component and session.
static uint64_t s_beClothDescribed[ClothCapacity]{};
static void BeClothName(void *object, char *text, int capacity) {
  text[0] = 0;
  void *name = nullptr;
  if (object && ClothInvoke(s_clothUnity.name, object, nullptr, name))
    ReadStrUtf8(name, text, capacity);
}
static void BeClothDescribe(ClothInstance &i, const ClothReadback &r) {
  const ptrdiff_t n = &i - s_cloth.instances;
  if (n < 0 || n >= ClothCapacity || s_beClothDescribed[n] == s_cloth.owner.session ||
      !r.state.readable || !r.process)
    return;
  s_beClothDescribed[n] = s_cloth.owner.session;
  __try {
    int roots = -1;
    void *list = nullptr;
    if (r.serialize &&
        ClothField(r.serialize, "rootBones", "System.Collections.Generic.List<UnityEngine.Transform>", list) && list)
      ClothValue(ClothMethod(il2cpp_object_get_class(list), "get_Count", "System.Int32"), list, roots);
    int colliders = -1;
    void *colliderList = nullptr, *get = nullptr;
    if (ClothField(r.process, "colliderList",
                   "System.Collections.Generic.List<BeyondDynamicBone.ColliderComponent>", colliderList) &&
        colliderList) {
      void *cls = il2cpp_object_get_class(colliderList);
      ClothValue(ClothMethod(cls, "get_Count", "System.Int32"), colliderList, colliders);
      get = ClothMethod(cls, "get_Item", "BeyondDynamicBone.ColliderComponent", "System.Int32");
    }
    Log("[BE-CLOTH] session=%llu slot=%d instance=%d name='%s' team=%d weight=%g ratio=%g blend=%g "
        "roots=%d colliders=%d valid=%d running=%d enabled=%d skip=%d culled=%d mode=%d",
        (unsigned long long)s_cloth.owner.session, BE_EIEM_SLOT, i.ref.id.instance, i.name, r.team,
        r.weight, r.ratio, r.blend, roots, colliders, r.state.valid, r.state.running,
        r.state.enabled, r.state.skip, r.state.culled, g_beClothMode.load(std::memory_order_acquire));
    for (int c = 0; get && c < colliders && c < 32; ++c) {
      void *collider = nullptr, *args[] = {&c};
      if (!ClothInvoke(get, colliderList, args, collider) || !ClothAlive(collider)) continue;
      Vector3 size{NAN, NAN, NAN}, center{NAN, NAN, NAN};
      bool separation = false;
      ClothField(collider, "size", "UnityEngine.Vector3", size);
      ClothField(collider, "center", "UnityEngine.Vector3", center);
      ClothField(collider, "radiusSeparation", "System.Boolean", separation);
      char name[96], parentName[96];
      BeClothName(collider, name, sizeof(name));
      void *transform = nullptr, *parent = nullptr;
      if (ClothInvoke(s_clothUnity.getTransform, collider, nullptr, transform) && transform)
        ClothInvoke(ClothMethod(il2cpp_object_get_class(transform), "get_parent", "UnityEngine.Transform"),
                    transform, nullptr, parent);
      BeClothName(parent, parentName, sizeof(parentName));
      const char *type = il2cpp_class_get_name(il2cpp_object_get_class(collider));
      Log("[BE-CLOTH-COLLIDER] session=%llu instance=%d index=%d type=%s name='%s' parent='%s' "
          "size=(%g,%g,%g) center=(%g,%g,%g) radiusSeparation=%d",
          (unsigned long long)s_cloth.owner.session, i.ref.id.instance, c, type ? type : "?", name,
          parentName, size.x, size.y, size.z, center.x, center.y, center.z, separation);
    }
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    Log("[BE-CLOTH] session=%llu instance=%d report=failed", (unsigned long long)s_cloth.owner.session,
        i.ref.id.instance);
  }
}
