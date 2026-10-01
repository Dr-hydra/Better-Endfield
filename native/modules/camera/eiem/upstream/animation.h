#pragma once

static const char *g_humanBoneNames[] = {
    "Hips",
    "LeftUpperLeg",
    "RightUpperLeg",
    "LeftLowerLeg",
    "RightLowerLeg",
    "LeftFoot",
    "RightFoot",
    "Spine",
    "Chest",
    "Neck",
    "Head",
    "LeftShoulder",
    "RightShoulder",
    "LeftUpperArm",
    "RightUpperArm",
    "LeftLowerArm",
    "RightLowerArm",
    "LeftHand",
    "RightHand",
    "LeftToes",
    "RightToes",
    "LeftEye",
    "RightEye",
    "Jaw",
    "LeftThumbProximal",
    "LeftThumbIntermediate",
    "LeftThumbDistal",
    "LeftIndexProximal",
    "LeftIndexIntermediate",
    "LeftIndexDistal",
    "LeftMiddleProximal",
    "LeftMiddleIntermediate",
    "LeftMiddleDistal",
    "LeftRingProximal",
    "LeftRingIntermediate",
    "LeftRingDistal",
    "LeftLittleProximal",
    "LeftLittleIntermediate",
    "LeftLittleDistal",
    "RightThumbProximal",
    "RightThumbIntermediate",
    "RightThumbDistal",
    "RightIndexProximal",
    "RightIndexIntermediate",
    "RightIndexDistal",
    "RightMiddleProximal",
    "RightMiddleIntermediate",
    "RightMiddleDistal",
    "RightRingProximal",
    "RightRingIntermediate",
    "RightRingDistal",
    "RightLittleProximal",
    "RightLittleIntermediate",
    "RightLittleDistal",
    "UpperChest",
};
static const int g_humanBoneCount = 55;

static void DiscoverSkeleton();
static void *g_playerController = nullptr;
static void *g_mainCharEntity = nullptr;
static void *g_cachedAnimator =
    nullptr;

static VmdFile *g_vmd = nullptr;
static std::vector<ResolvedBoneMapping> *g_resolvedMappings = nullptr;
static MmdPlayer *g_player = nullptr;

static void SafeRefreshEntity() {
  __try {
    int pcOff = SafeOff(OFF_pcEntity, 0x70, "pcEntity");
    void *entity = *(void **)((char *)g_playerController + pcOff);
    if (entity) {
      g_mainCharEntity = entity;
      int ecOff = SafeOff(OFF_entityComplexAnim, 0x110, "entityComplexAnim");
      void *complexAnimCom = *(void **)((char *)entity + ecOff);
      if (complexAnimCom) {
        if (OFF_complexAnimAnimator < 0) {
          __try {
            void *cacClass = il2cpp_object_get_class(complexAnimCom);
            if (cacClass) {
              const char *animNames[] = {"animator", "m_animator",
                                         "_animator",
                                         "<animator>k__BackingField"};
              const char *matchedName = nullptr;
              OFF_complexAnimAnimator = FindFieldInHierarchy(
                  cacClass, animNames, 4, &matchedName);
              if (OFF_complexAnimAnimator >= 0) {
                Log("[OFFSET] ComplexAnimComp.%s = %d (0x%X) [lazy resolve]",
                    matchedName, OFF_complexAnimAnimator,
                    OFF_complexAnimAnimator);
              } else {
                Log("[WARN] ComplexAnimComp animator not found in hierarchy "
                    "(lazy), fallback 0x148");
                DumpFieldsHierarchy(cacClass);
              }
            }
          } __except (1) {
          }
        }
        int caOff =
            SafeOff(OFF_complexAnimAnimator, 0x148, "complexAnimAnimator");
        void *animator = *(void **)((char *)complexAnimCom + caOff);
        if (animator)
          g_cachedAnimator = animator;
      }
    }
  } __except (1) {
  }
}

static void *SafeGetBoneTransform(int humanBone) {
  __try {
    void *params[] = {&humanBone};
    return Invoke(g_animator_GetBoneTransform, g_cachedAnimator, params);
  } __except (1) {
    return nullptr;
  }
}

static void SafeGetBoneName(void *transform, char *buf, int sz) {
  buf[0] = 0;
  __try {
    void *nameStr = Invoke(g_object_get_name, transform);
    ReadStrUtf8(nameStr, buf, sz);
  } __except (1) {
  }
}

static void *SafeFindChildRecursive(void *transform, const char *targetName,
                                    int maxDepth) {
  if (!transform || maxDepth <= 0)
    return nullptr;
  __try {
    char name[256] = "";
    if (g_object_get_name) {
      void *nameStr = Invoke(g_object_get_name, transform);
      if (nameStr)
        ReadStrUtf8(nameStr, name, sizeof(name));
    }
    if (strcmp(name, targetName) == 0)
      return transform;

    void *countBoxed = Invoke(g_transform_get_childCount, transform);
    int count = countBoxed ? *(int *)((char *)countBoxed + 16) : 0;
    for (int i = 0; i < count; i++) {
      void *params[] = {&i};
      void *child = Invoke(g_transform_GetChild, transform, params);
      if (child) {
        void *found = SafeFindChildRecursive(child, targetName, maxDepth - 1);
        if (found)
          return found;
      }
    }
  } __except (1) {
  }
  return nullptr;
}

static void *SafeGetComponentTransform(void *component) {
  if (!component || !g_component_get_transform)
    return nullptr;
  __try {
    return Invoke(g_component_get_transform, component);
  } __except (1) {
    return nullptr;
  }
}

static void RefreshEntityAnimator() {
  if (!g_playerController)
    return;
  SafeRefreshEntity();
}

#include "cloth.h"

static void RestoreDisabledComponents() {
  ClothRelease("muscle-components-restore");
  if (!g_animator_set_enabled)
    return;

  void *trueArg = (void *)1;
  void **params = &trueArg;

  auto EnableComp = [&](void *comp, const char *name) {
    if (!comp)
      return;
    __try {
      Invoke(g_animator_set_enabled, comp, params);
      Log("[IK-RESTORE] %s RE-ENABLED", name);
    } __except (1) {
      Log("[IK-RESTORE] %s re-enable failed (exception)", name);
    }
  };

  for (int bi = 0; bi < s_bipedIKCount; bi++) {
    char name[64];
    snprintf(name, sizeof(name), "BipedIK #%d", bi + 1);
    EnableComp(s_bipedIK[bi], name);
  }
  for (int gi = 0; gi < s_grounderIKCount; gi++) {
    char name[64];
    snprintf(name, sizeof(name), "GrounderBipedIK #%d", gi + 1);
    EnableComp(s_grounderIK[gi], name);
  }
  for (int i = 0; i < s_followDamperCount; i++) {
    char name[64];
    snprintf(name, sizeof(name), "TransformFollowDamper #%d", i + 1);
    EnableComp(s_followDamper[i], name);
  }
  EnableComp(s_animatorMono, "AnimatorMono");

  s_ikDisabled = false;
  memset(s_bipedIK, 0, sizeof(s_bipedIK));
  s_bipedIKCount = 0;
  memset(s_grounderIK, 0, sizeof(s_grounderIK));
  s_grounderIKCount = 0;
  memset(s_followDamper, 0, sizeof(s_followDamper));
  s_followDamperCount = 0;
  s_animatorMono = nullptr;

  Log("[IK-RESTORE] All components restored");
}

static void SafeSetLocalRotation(void *transform, Quat q) {
  if (!transform || !g_transform_set_localRotation)
    return;
  __try {
    void *params[] = {&q};
    Invoke(g_transform_set_localRotation, transform, params);
  } __except (1) {
  }
}

static void SafeSetLocalPosition(void *transform, Vec3 p) {
  if (!transform || !g_transform_set_localPosition)
    return;
  __try {
    void *params[] = {&p};
    Invoke(g_transform_set_localPosition, transform, params);
  } __except (1) {
  }
}




static void SafeSetAnimatorEnabled(bool enabled) {
  if (!g_cachedAnimator || !g_animator_set_enabled)
    return;
  __try {
    void *params[] = {&enabled};
    Invoke(g_animator_set_enabled, g_cachedAnimator, params);
  } __except (1) {
  }
}

static bool ReadWorldPosition(void *transform, Vec3 &out) {
  if (!transform)
    return false;
  if (g_camGetPos) {
    float pos[3] = {};
    __try {
      g_camGetPos(transform, pos);
      out = {pos[0], pos[1], pos[2]};
      return true;
    } __except (1) {
    }
  }
  return false;
}

static void ComputeStances(std::vector<ResolvedBoneMapping> &mappings) {
  for (auto &rm : mappings) {
    if (!rm.valid)
      continue;
    rm.mmdStance = LookupMmdStance(rm.mmdName);
  }

  if (!g_transform_get_position) {
    Log("[WARN] get_position not available, using identity Endfield stances");
    for (auto &rm : mappings) {
      rm.endfieldStance = {0, 0, 0, 1};
      rm.hasStance = true;
    }
    return;
  }

  static const struct {
    const char *bone;
    const char *child;
  } boneChain[] = {
      {"\xe5\xb7\xa6\xe8\x82\xa9",
       "\xe5\xb7\xa6\xe8\x85\x95"},
      {"\xe5\xb7\xa6\xe8\x85\x95",
       "\xe5\xb7\xa6\xe3\x81\xb2\xe3\x81\x98"},
      {"\xe5\xb7\xa6\xe3\x81\xb2\xe3\x81\x98",
       "\xe5\xb7\xa6\xe6\x89\x8b\xe9\xa6\x96"},
      {"\xe5\xb7\xa6\xe6\x89\x8b\xe9\xa6\x96",
       nullptr},
      {"\xe5\x8f\xb3\xe8\x82\xa9",
       "\xe5\x8f\xb3\xe8\x85\x95"},
      {"\xe5\x8f\xb3\xe8\x85\x95",
       "\xe5\x8f\xb3\xe3\x81\xb2\xe3\x81\x98"},
      {"\xe5\x8f\xb3\xe3\x81\xb2\xe3\x81\x98",
       "\xe5\x8f\xb3\xe6\x89\x8b\xe9\xa6\x96"},
      {"\xe5\x8f\xb3\xe6\x89\x8b\xe9\xa6\x96",
       nullptr},
      {"\xe4\xb8\x8a\xe5\x8d\x8a\xe8\xba\xab",
       "\xe4\xb8\x8a\xe5\x8d\x8a\xe8\xba\xab\x32"},
      {"\xe4\xb8\x8a\xe5\x8d\x8a\xe8\xba\xab\x32",
       "\xe9\xa6\x96"},
      {"\xe9\xa6\x96", "\xe9\xa0\xad"},
      {"\xe9\xa0\xad", nullptr},
      {"\xe5\xb7\xa6\xe8\xb6\xb3",
       "\xe5\xb7\xa6\xe3\x81\xb2\xe3\x81\x96"},
      {"\xe5\xb7\xa6\xe3\x81\xb2\xe3\x81\x96",
       "\xe5\xb7\xa6\xe8\xb6\xb3\xe9\xa6\x96"},
      {"\xe5\xb7\xa6\xe8\xb6\xb3\xe9\xa6\x96", nullptr},
      {"\xe5\x8f\xb3\xe8\xb6\xb3",
       "\xe5\x8f\xb3\xe3\x81\xb2\xe3\x81\x96"},
      {"\xe5\x8f\xb3\xe3\x81\xb2\xe3\x81\x96",
       "\xe5\x8f\xb3\xe8\xb6\xb3\xe9\xa6\x96"},
      {"\xe5\x8f\xb3\xe8\xb6\xb3\xe9\xa6\x96", nullptr},
  };
  static const int chainCount = sizeof(boneChain) / sizeof(boneChain[0]);

  std::map<std::string, size_t> nameToIdx;
  for (size_t i = 0; i < mappings.size(); i++) {
    if (mappings[i].valid)
      nameToIdx[mappings[i].mmdName] = i;
  }

  for (size_t i = 0; i < mappings.size(); i++) {
    auto &rm = mappings[i];
    if (!rm.valid || !rm.transform) {
      rm.hasStance = true;
      continue;
    }

    Vec3 defaultAxis = {0, 0, 0};
    bool inTable = false;
    for (int s = 0; s < g_mmdStanceCount; s++) {
      if (rm.mmdName == g_mmdStanceTable[s].mmdName) {
        defaultAxis = g_mmdStanceTable[s].defaultAxis;
        inTable = true;
        break;
      }
    }
    if (!inTable) {
      rm.endfieldStance = {0, 0, 0, 1};
      rm.hasStance = true;
      continue;
    }

    Vec3 thisPos;
    if (!ReadWorldPosition(rm.transform, thisPos)) {
      rm.endfieldStance = {0, 0, 0, 1};
      rm.hasStance = true;
      continue;
    }

    Vec3 childPos = thisPos;
    bool foundChild = false;
    for (int c = 0; c < chainCount; c++) {
      if (rm.mmdName == boneChain[c].bone && boneChain[c].child != nullptr) {
        auto it = nameToIdx.find(boneChain[c].child);
        if (it != nameToIdx.end()) {
          auto &childRm = mappings[it->second];
          if (childRm.valid && childRm.transform &&
              ReadWorldPosition(childRm.transform, childPos)) {
            float dx = childPos.x - thisPos.x;
            float dy = childPos.y - thisPos.y;
            float dz = childPos.z - thisPos.z;
            if (sqrtf(dx * dx + dy * dy + dz * dz) > 0.001f)
              foundChild = true;
          }
        }
        break;
      }
    }

    if (foundChild) {
      Vec3 dir = {childPos.x - thisPos.x, childPos.y - thisPos.y,
                  childPos.z - thisPos.z};
      rm.endfieldStance = RotationTo(defaultAxis, dir);
      Log("[STANCE] %s: dir(%.3f,%.3f,%.3f) efS(%.4f,%.4f,%.4f,%.4f)",
          rm.mmdName.c_str(), dir.x, dir.y, dir.z, rm.endfieldStance.x,
          rm.endfieldStance.y, rm.endfieldStance.z, rm.endfieldStance.w);
    } else {
      rm.endfieldStance = {0, 0, 0, 1};
    }
    rm.hasStance = true;
  }

  Log("[STANCE] Computed stances for %d bones", (int)mappings.size());
}

static void CaptureBindPose(ResolvedBoneMapping &rm) {
  if (rm.hasBind)
    return;
  __try {
    if (g_transform_get_localRotation) {
      void *boxed = Invoke(g_transform_get_localRotation, rm.transform);
      if (boxed) {
        float *data = (float *)((char *)boxed + 16);
        rm.bindRot[0] = data[0];
        rm.bindRot[1] = data[1];
        rm.bindRot[2] = data[2];
        rm.bindRot[3] = data[3];
      }
    }
    if (g_transform_get_localPosition) {
      void *boxed = Invoke(g_transform_get_localPosition, rm.transform);
      if (boxed) {
        float *data = (float *)((char *)boxed + 16);
        rm.bindPos[0] = data[0];
        rm.bindPos[1] = data[1];
        rm.bindPos[2] = data[2];
      }
    }
    rm.hasBind = true;
  } __except (1) {
  }
}


struct MmdRestRot {
  int humanBone;
  float r[4];
};
static const MmdRestRot g_mmdRest[] = {
    {0, {0.054970f, -0.000550f, 0.009985f, 0.998438f}},
    {1, {-0.221591f, 0.253252f, -0.060742f, 0.939719f}},
    {2, {-0.193183f, -0.279992f, 0.066861f, 0.937984f}},
    {3, {0.391337f, 0.000000f, 0.000000f, 0.920247f}},
    {4, {0.352274f, 0.000000f, 0.000000f, 0.935897f}},
    {5, {-0.243333f, 0.027973f, 0.030674f, 0.969054f}},
    {6, {-0.250818f, -0.070884f, -0.077729f, 0.962301f}},
    {7, {-0.054961f, 0.001099f, 0.000000f, 0.998488f}},
    {8, {0.000000f, 0.000000f, 0.000000f, 1.000000f}},
    {9, {0.000000f, 0.000000f, 0.000000f, 1.000000f}},
    {10, {0.054966f, -0.000825f, 0.014977f, 0.998376f}},
    {11, {-0.005895f, 0.094641f, 0.009901f, 0.995445f}},
    {12, {-0.005895f, -0.094641f, -0.009901f, 0.995445f}},
    {13, {-0.323522f, 0.433520f, 0.147442f, 0.828043f}},
    {14, {-0.326467f, -0.442077f, -0.120765f, 0.826682f}},
    {15, {-0.591116f, 0.746363f, 0.000000f, 0.305818f}},
    {16, {0.591117f, 0.746363f, 0.000000f, -0.305818f}},
    {17, {0.260797f, 0.418563f, -0.479321f, 0.725976f}},
    {18, {0.221362f, -0.429079f, 0.463713f, 0.742873f}},
};
static const int g_mmdRestCount = sizeof(g_mmdRest) / sizeof(g_mmdRest[0]);

static Quat GetMmdRestRot(int hb) {
  for (int i = 0; i < g_mmdRestCount; i++) {
    if (g_mmdRest[i].humanBone == hb)
      return {g_mmdRest[i].r[0], g_mmdRest[i].r[1], g_mmdRest[i].r[2],
              g_mmdRest[i].r[3]};
  }
  return {0, 0, 0, 1};
}


static bool
CaptureRestPoseViaRebind(std::vector<ResolvedBoneMapping> &mappings) {
  if (!g_cachedAnimator || !g_animator_Rebind) {
    Log("[BIND] Rebind not available, using hardcoded fallback");
    return false;
  }

  SafeSetAnimatorEnabled(true);
  Sleep(50);

  __try {
    Invoke(g_animator_Rebind, g_cachedAnimator);
  } __except (1) {
    Log("[BIND] Rebind crashed, using hardcoded fallback");
    return false;
  }

  if (g_animator_Update) {
    __try {
      float dt = 0.0f;
      void *params[] = {&dt};
      Invoke(g_animator_Update, g_cachedAnimator, params);
    } __except (1) {
    }
  }
  Sleep(50);

  SafeSetAnimatorEnabled(false);

  Log("[BIND] Reading rest pose after Rebind...");
  for (auto &rm : mappings) {
    rm.hasBind = false;
    CaptureBindPose(rm);
  }

  for (auto &rm : mappings) {
    if (rm.humanBone >= 0 && rm.humanBone <= 18) {
      Log("  %-18s hb=%2d REST(%7.4f,%7.4f,%7.4f,%7.4f)", rm.mmdName.c_str(),
          rm.humanBone, rm.bindRot[0], rm.bindRot[1], rm.bindRot[2],
          rm.bindRot[3]);
    }
  }
  return true;
}

static int g_corrMode = 0;
static const int g_corrCount = 8;
static const char *g_modeNames[] = {
    "retarget",     "retarget-negZ", "retarget-negXY", "retarget-post",
    "retarget-inv", "retarget+sim",  "sim+bind(old)",  "restPose"};
static bool s_dumpedFrame0 = false;

static bool g_calibMode = false;
static int g_calibBone = 0;
static int g_calibRot = 0;

struct CalibRot {
  Quat q;
  const char *name;
};
static const CalibRot g_calibRots[] = {
    {{0, 0, 0, 1}, "identity"},           {{0.3827f, 0, 0, 0.9239f}, "+45 X"},
    {{-0.3827f, 0, 0, 0.9239f}, "-45 X"}, {{0, 0.3827f, 0, 0.9239f}, "+45 Y"},
    {{0, -0.3827f, 0, 0.9239f}, "-45 Y"}, {{0, 0, 0.3827f, 0.9239f}, "+45 Z"},
    {{0, 0, -0.3827f, 0.9239f}, "-45 Z"}, {{0.7071f, 0, 0, 0.7071f}, "+90 X"},
    {{0, 0.7071f, 0, 0.7071f}, "+90 Y"},  {{0, 0, 0.7071f, 0.7071f}, "+90 Z"},
};
static const int g_calibRotCount = sizeof(g_calibRots) / sizeof(g_calibRots[0]);

static void CalibrationTick() {
  if (!g_resolvedMappings || g_resolvedMappings->empty())
    return;
  std::vector<int> validIdx;
  for (int i = 0; i < (int)g_resolvedMappings->size(); i++) {
    auto &rm = (*g_resolvedMappings)[i];
    if (!rm.valid || !rm.transform || rm.isFingerBone)
      continue;
    validIdx.push_back(i);
  }
  if (validIdx.empty())
    return;
  int bi = g_calibBone % (int)validIdx.size();
  int ri = g_calibRot % g_calibRotCount;
  auto &rm = (*g_resolvedMappings)[validIdx[bi]];
  CaptureBindPose(rm);
  Quat bind = {rm.bindRot[0], rm.bindRot[1], rm.bindRot[2], rm.bindRot[3]};
  Quat finalRot = QuatMul(bind, g_calibRots[ri].q);
  SafeSetLocalRotation(rm.transform, finalRot);

  static int lastBi = -1, lastRi = -1;
  if (bi != lastBi || ri != lastRi) {
    lastBi = bi;
    lastRi = ri;
    Log("[CALIB] Bone %d/%d '%s' (%s) | Rot %d: %s | bind(%.3f,%.3f,%.3f,%.3f)",
        bi, (int)validIdx.size(), rm.mmdName.c_str(), rm.transformName.c_str(),
        ri, g_calibRots[ri].name, bind.x, bind.y, bind.z, bind.w);
  }
}

static MuscleAnim *g_muscleAnim = nullptr;
static MmdPlayer *g_musclePlayer = nullptr;
static uint32_t g_poseHandleGC = 0;
static void *g_cachedMPtr = nullptr;
static void *g_musclesArray = nullptr;
static uint32_t g_musclesArrayGC = 0;

static float g_restBodyPos[3] = {0};
static float g_restBodyRot[4] = {0, 0, 0, 1};

static BoneAnim *g_boneAnim = nullptr;
static MmdPlayer *g_bonePlayer = nullptr;

struct Il2CppHumanPose {
  float bodyPosX, bodyPosY, bodyPosZ;
  float bodyRotX, bodyRotY, bodyRotZ, bodyRotW;
  void *muscles;
};

static void *CreateFloatArray(int count) {
  if (!il2cpp_array_new_specific)
    return nullptr;
  void *domain = il2cpp_domain_get();
  size_t ac;
  void **asms = il2cpp_domain_get_assemblies(domain, &ac);
  void *floatClass = FindClass("System", "Single", asms, ac);
  if (!floatClass) {
    Log("[MUSCLE] System.Single class not found!");
    return nullptr;
  }
  void *arr = il2cpp_array_new_specific(floatClass, count);
  Log("[MUSCLE] Created float[%d] array: %p", count, arr);
  return arr;
}

static float *GetArrayData(void *arr) {
  if (!arr)
    return nullptr;
  return (float *)((char *)arr + 32);
}

static bool InitMusclePoseHandler() {
  if (g_poseHandleGC != 0)
    return true;
  if (!g_cachedAnimator || !g_humanPoseHandlerClass ||
      !g_humanPoseHandler_ctor) {
    Log("[MUSCLE] Missing: Animator=%p HPHClass=%p ctor=%p", g_cachedAnimator,
        g_humanPoseHandlerClass, g_humanPoseHandler_ctor);
    return false;
  }

  void *avatar = Invoke(g_animator_get_avatar, g_cachedAnimator);
  void *rootTransform = SafeGetComponentTransform(g_cachedAnimator);
  if (!avatar || !rootTransform) {
    Log("[MUSCLE] No avatar=%p or rootTransform=%p", avatar, rootTransform);
    return false;
  }

  void *poseHandler = nullptr;

  __try {
    poseHandler = il2cpp_object_new(g_humanPoseHandlerClass);
    if (!poseHandler) {
      Log("[MUSCLE] Failed to allocate HumanPoseHandler");
      return false;
    }
    void *ctorParams[] = {avatar, rootTransform};
    void *exc = nullptr;
    il2cpp_runtime_invoke(g_humanPoseHandler_ctor, poseHandler, ctorParams,
                          &exc);
    if (exc) {
      Log("[MUSCLE] HumanPoseHandler constructor exception");
      return false;
    }
  } __except (1) {
    Log("[MUSCLE] HumanPoseHandler creation crashed");
    return false;
  }

  g_poseHandleGC = il2cpp_gchandle_new(poseHandler, true);
  Log("[MUSCLE] GC pinned handler: handle=%u obj=%p", g_poseHandleGC,
      poseHandler);

  g_cachedMPtr = *(void **)((char *)poseHandler + 16);
  Log("[MUSCLE] m_Ptr = %p", g_cachedMPtr);
  if (!g_cachedMPtr) {
    Log("[MUSCLE] ERROR: m_Ptr is NULL after construction!");
    return false;
  }

  g_musclesArray = CreateFloatArray(95);
  if (!g_musclesArray) {
    Log("[MUSCLE] Failed to create float[95] array");
    return false;
  }
  g_musclesArrayGC = il2cpp_gchandle_new(g_musclesArray, true);
  Log("[MUSCLE] GC pinned muscles array: handle=%u arr=%p", g_musclesArrayGC,
      g_musclesArray);

  void *mPtrCheck = *(void **)((char *)poseHandler + 16);
  Log("[MUSCLE] m_Ptr after array alloc = %p (was %p)", mPtrCheck,
      g_cachedMPtr);

  if (g_humanPoseHandler_GetHumanPose) {
    Il2CppHumanPose testPose = {};
    __try {
      void *params[] = {&testPose};
      void *exc = nullptr;
      il2cpp_runtime_invoke(g_humanPoseHandler_GetHumanPose, poseHandler,
                            params, &exc);
      if (exc) {
        Log("[MUSCLE] GetHumanPose exception");
      } else {
        g_restBodyPos[0] = 0.0f;
        g_restBodyPos[1] = testPose.bodyPosY;
        g_restBodyPos[2] = 0.0f;
        g_restBodyRot[0] = 0.0f;
        g_restBodyRot[1] = 0.0f;
        g_restBodyRot[2] = 0.0f;
        g_restBodyRot[3] = 1.0f;
        Log("[MUSCLE] Canonical Reference Pose: hipsY=%.3f", testPose.bodyPosY);

        if (g_animator_GetBoneTransform && g_cachedAnimator) {
          void *rootT = SafeGetComponentTransform(g_cachedAnimator);
          void *headT = SafeGetBoneTransform(10);
          Vec3 rootPos, headPos;
          if (rootT && headT && ReadWorldPosition(rootT, rootPos) &&
              ReadWorldPosition(headT, headPos)) {
            float h = headPos.y - rootPos.y;
            if (h > 0.1f && h < 5.0f) {
              g_charHeight = h;
              Log("[HEIGHT] Character height measured: %.3f m", g_charHeight);
            }
          }
        }
      }
    } __except (1) {
      Log("[MUSCLE] GetHumanPose crashed");
    }
  }

  if (g_humanPoseHandler_GetHumanPose) {
    void **slots = (void **)g_humanPoseHandler_GetHumanPose;
    Log("[MI-DUMP] GetHumanPose MethodInfo at %p:",
        g_humanPoseHandler_GetHumanPose);
    for (int i = 0; i < 10; i++) {
      Log("[MI-DUMP]   [%d] = %p", i, slots[i]);
    }
    void *mp = ((MInfo *)g_humanPoseHandler_GetHumanPose)->mp;
    Log("[MI-DUMP]   MInfo::mp = %p (this is what Hook() uses)", mp);
  }

  Log("[MUSCLE] HumanPoseHandler init complete, g_cachedAnimator=%p",
      g_cachedAnimator);
  return true;
}

static int s_muscleLogCounter = 0;

static Quat QMul(Quat a, Quat b) {
  Quat r;
  r.x = a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y;
  r.y = a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x;
  r.z = a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w;
  r.w = a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z;
  return r;
}

static Quat SafeGetLocalRotation(void *transform) {
  Quat q = {0, 0, 0, 1};
  if (!transform || !g_transform_get_localRotation)
    return q;
  __try {
    void *boxed = Invoke(g_transform_get_localRotation, transform);
    if (boxed)
      q = *(Quat *)((char *)boxed + 16);
  } __except (1) {
  }
  return q;
}

static Vec3 SafeGetLocalPosition(void *transform) {
  Vec3 p = {0, 0, 0};
  if (!transform || !g_transform_get_localPosition)
    return p;
  __try {
    void *boxed = Invoke(g_transform_get_localPosition, transform);
    if (boxed)
      p = *(Vec3 *)((char *)boxed + 16);
  } __except (1) {
  }
  return p;
}

static bool g_boneRestCaptured = false;
static Quat g_boneRestRot[55];
static Vec3 g_boneRestHipsPos;

static void BoneAnimationTick() {
  if (!g_bonePlayer || !g_bonePlayer->playing)
    return;
  if (!g_boneAnim || !g_boneAnim->loaded)
    return;
  if (!g_cachedAnimator || !g_animator_GetBoneTransform)
    return;

  if (!g_boneRestCaptured) {
    Log("[BONE] Forcing T-pose and capturing game rest pose...");

    SafeSetAnimatorEnabled(false);
    if (g_animator_Rebind) {
      __try {
        Invoke(g_animator_Rebind, g_cachedAnimator);
        Log("[BONE] Animator.Rebind() called");
      } __except (1) {
        Log("[BONE] Rebind() crashed");
      }
    }
    Sleep(50);

    static const char *boneNames[] = {"Hips",
                                      "LeftUpperLeg",
                                      "RightUpperLeg",
                                      "LeftLowerLeg",
                                      "RightLowerLeg",
                                      "LeftFoot",
                                      "RightFoot",
                                      "Spine",
                                      "Chest",
                                      "UpperChest",
                                      "Neck",
                                      "Head",
                                      "LeftShoulder",
                                      "RightShoulder",
                                      "LeftUpperArm",
                                      "RightUpperArm",
                                      "LeftLowerArm",
                                      "RightLowerArm",
                                      "LeftHand",
                                      "RightHand",
                                      "LeftToes",
                                      "RightToes",
                                      "LeftEye",
                                      "RightEye",
                                      "Jaw",
                                      "LeftThumbProximal",
                                      "LeftThumbIntermediate",
                                      "LeftThumbDistal",
                                      "LeftIndexProximal",
                                      "LeftIndexIntermediate",
                                      "LeftIndexDistal",
                                      "LeftMiddleProximal",
                                      "LeftMiddleIntermediate",
                                      "LeftMiddleDistal",
                                      "LeftRingProximal",
                                      "LeftRingIntermediate",
                                      "LeftRingDistal",
                                      "LeftLittleProximal",
                                      "LeftLittleIntermediate",
                                      "LeftLittleDistal",
                                      "RightThumbProximal",
                                      "RightThumbIntermediate",
                                      "RightThumbDistal",
                                      "RightIndexProximal",
                                      "RightIndexIntermediate",
                                      "RightIndexDistal",
                                      "RightMiddleProximal",
                                      "RightMiddleIntermediate",
                                      "RightMiddleDistal",
                                      "RightRingProximal",
                                      "RightRingIntermediate",
                                      "RightRingDistal",
                                      "RightLittleProximal",
                                      "RightLittleIntermediate",
                                      "RightLittleDistal"};

    for (int b = 0; b < 55; b++) {
      void *bt = SafeGetBoneTransform(b);
      if (bt) {
        g_boneRestRot[b] = SafeGetLocalRotation(bt);
      } else {
        g_boneRestRot[b] = {0, 0, 0, 1};
      }
    }
    void *hips = SafeGetBoneTransform(0);
    if (hips)
      g_boneRestHipsPos = SafeGetLocalPosition(hips);

    g_boneRestCaptured = true;

    FILE *df = fopen("plugin\\bone_rest_compare.txt", "w");
    if (df) {
      fprintf(df, "=== Bone Rest Pose Comparison ===\n\n");
      fprintf(df, "Game Hips localPos: (%.4f, %.4f, %.4f)\n\n",
              g_boneRestHipsPos.x, g_boneRestHipsPos.y, g_boneRestHipsPos.z);

      fprintf(df, "%-4s %-30s | %-45s | %-45s | %-45s\n", "Idx", "Bone",
              "Game Rest (x,y,z,w)", "Unity Rest (x,y,z,w)",
              "Frame0 Delta (x,y,z,w)");
      fprintf(df, "------------------------------------------------------------"
                  "-------------------------------------------------\n");

      BoneFrame f0 = g_boneAnim->GetFrame(0);

      for (int b = 0; b < 55; b++) {
        void *bt = SafeGetBoneTransform(b);
        const char *name = (b < 55) ? boneNames[b] : "?";

        fprintf(df,
                "[%2d] %-30s | (%7.4f,%7.4f,%7.4f,%7.4f) | "
                "(%7.4f,%7.4f,%7.4f,%7.4f) | (%7.4f,%7.4f,%7.4f,%7.4f) %s\n",
                b, name, g_boneRestRot[b].x, g_boneRestRot[b].y,
                g_boneRestRot[b].z, g_boneRestRot[b].w,
                g_boneAnim->restPose[b][0], g_boneAnim->restPose[b][1],
                g_boneAnim->restPose[b][2], g_boneAnim->restPose[b][3],
                f0.bones[b][0], f0.bones[b][1], f0.bones[b][2], f0.bones[b][3],
                bt ? "" : "(MISSING in game)");
      }
      fclose(df);
      Log("[BONE] Diagnostic dump written to plugin\\bone_rest_compare.txt");
    }

    Log("[BONE] Rest pose captured. Hips rot=(%.3f,%.3f,%.3f,%.3f) "
        "pos=(%.3f,%.3f,%.3f)",
        g_boneRestRot[0].x, g_boneRestRot[0].y, g_boneRestRot[0].z,
        g_boneRestRot[0].w, g_boneRestHipsPos.x, g_boneRestHipsPos.y,
        g_boneRestHipsPos.z);
  }

  SafeSetAnimatorEnabled(
      false);

  float time = g_bonePlayer->Tick();
  BoneFrame bf = g_boneAnim->GetFrame(time);

  auto qMul = [](const float a[4], const float b[4], float out[4]) {
    out[0] = a[3] * b[0] + a[0] * b[3] + a[1] * b[2] - a[2] * b[1];
    out[1] = a[3] * b[1] - a[0] * b[2] + a[1] * b[3] + a[2] * b[0];
    out[2] = a[3] * b[2] + a[0] * b[1] - a[1] * b[0] + a[2] * b[3];
    out[3] = a[3] * b[3] - a[0] * b[0] - a[1] * b[1] - a[2] * b[2];
  };

  int applied = 0;
  for (int b = 0; b < 55; b++) {
    void *boneT = SafeGetBoneTransform(b);
    if (!boneT)
      continue;

    float *delta = bf.bones[b];

    float gameRest[4] = {g_boneRestRot[b].x, g_boneRestRot[b].y,
                         g_boneRestRot[b].z, g_boneRestRot[b].w};
    float finalRot[4];
    qMul(gameRest, delta, finalRot);

    SafeSetLocalRotation(boneT,
                         {finalRot[0], finalRot[1], finalRot[2], finalRot[3]});
    applied++;
  }

  void *hipsT = SafeGetBoneTransform(0);
  if (hipsT) {
    Vec3 hipsPos;
    hipsPos.x = g_boneRestHipsPos.x + bf.hipsDeltaPos[0];
    hipsPos.y = g_boneRestHipsPos.y + bf.hipsDeltaPos[1];
    hipsPos.z = g_boneRestHipsPos.z + bf.hipsDeltaPos[2];
    SafeSetLocalPosition(hipsT, hipsPos);
  }

  static int s_logCount = 0;
  if (s_logCount < 3) {
    BoneFrame f0 = g_boneAnim->GetFrame(0);
    Log("[BONE] t=%.2f applied=%d | Hips gameR(%.3f,%.3f,%.3f,%.3f) "
        "d(%.3f,%.3f,%.3f,%.3f)",
        time, applied, g_boneRestRot[0].x, g_boneRestRot[0].y,
        g_boneRestRot[0].z, g_boneRestRot[0].w, f0.bones[0][0], f0.bones[0][1],
        f0.bones[0][2], f0.bones[0][3]);
    Log("[BONE] LeftUpperArm[14] gameR(%.3f,%.3f,%.3f,%.3f) "
        "d(%.3f,%.3f,%.3f,%.3f)",
        g_boneRestRot[14].x, g_boneRestRot[14].y, g_boneRestRot[14].z,
        g_boneRestRot[14].w, f0.bones[14][0], f0.bones[14][1], f0.bones[14][2],
        f0.bones[14][3]);
    s_logCount++;
  }
}

