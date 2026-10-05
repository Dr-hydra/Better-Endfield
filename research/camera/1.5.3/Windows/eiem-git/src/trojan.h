#pragma once

static void DirectVmdCamera_UpdateMainThread(int frame);
static void DirectVmdCamera_ResetMainThread(const char *reason);
static void DirectVmdAudio_ResetMainThread(const char *reason,
                                           bool closeResource = false);

static bool s_poseReady;
static Il2CppHumanPose s_cachedPose;
static float *s_musclePtr;
static float s_savedIdleMuscles[128] =
    {};
static float s_savedIdleBodyPos[3] = {};
static float s_savedIdleBodyRot[4] = {0, 0, 0, 1};
static int s_actualMuscleCount = 95;

static int g_stdToGameMap[95];
static bool g_dynamicMapReady = false;

static void InitHardcodedMuscleMap() {
  for (int i = 0; i < 95; i++) {
    if (i <= 28)
      g_stdToGameMap[i] = i;
    else if (i <= 36)
      g_stdToGameMap[i] = i + 3;
    else
      g_stdToGameMap[i] = i + 6;
  }
}

static const char *g_muscleNames[] = {
    "Spine Front-Back",
    "Spine Left-Right",
    "Spine Twist L-R",
    "Chest Front-Back",
    "Chest Left-Right",
    "Chest Twist L-R",
    "UpperChest Front-Back",
    "UpperChest Left-Right",
    "UpperChest Twist L-R",
    "Neck Nod Down-Up",
    "Neck Tilt L-R",
    "Neck Turn L-R",
    "Head Nod Down-Up",
    "Head Tilt L-R",
    "Head Turn L-R",
    "Left Eye Down-Up",
    "Left Eye In-Out",
    "Right Eye Down-Up",
    "Right Eye In-Out",
    "Jaw Close",
    "Jaw Left-Right",
    "L UpperLeg Front-Back",
    "L UpperLeg In-Out",
    "L UpperLeg Twist",
    "L LowerLeg Stretch",
    "L LowerLeg Twist",
    "L Foot Up-Down",
    "L Foot Twist",
    "L Toes Up-Down",
    "R UpperLeg Front-Back",
    "R UpperLeg In-Out",
    "R UpperLeg Twist",
    "R LowerLeg Stretch",
    "R LowerLeg Twist",
    "R Foot Up-Down",
    "R Foot Twist",
    "R Toes Up-Down",
    "L Shoulder Down-Up",
    "L Shoulder Front-Back",
    "L Arm Down-Up",
    "L Arm Front-Back",
    "L Arm Twist",
    "L Forearm Stretch",
    "L Forearm Twist",
    "L Hand Down-Up",
    "L Hand In-Out",
    "R Shoulder Down-Up",
    "R Shoulder Front-Back",
    "R Arm Down-Up",
    "R Arm Front-Back",
    "R Arm Twist",
    "R Forearm Stretch",
    "R Forearm Twist",
    "R Hand Down-Up",
    "R Hand In-Out",
    "LF Thumb1 Stretch",
    "LF Thumb Spread",
    "LF Thumb2 Stretch",
    "LF Thumb3 Stretch",
    "LF Index1 Stretch",
    "LF Index Spread",
    "LF Index2 Stretch",
    "LF Index3 Stretch",
    "LF Middle1 Stretch",
    "LF Middle Spread",
    "LF Middle2 Stretch",
    "LF Middle3 Stretch",
    "LF Ring1 Stretch",
    "LF Ring Spread",
    "LF Ring2 Stretch",
    "LF Ring3 Stretch",
    "LF Little1 Stretch",
    "LF Little Spread",
    "LF Little2 Stretch",
    "LF Little3 Stretch",
    "RF Thumb1 Stretch",
    "RF Thumb Spread",
    "RF Thumb2 Stretch",
    "RF Thumb3 Stretch",
    "RF Index1 Stretch",
    "RF Index Spread",
    "RF Index2 Stretch",
    "RF Index3 Stretch",
    "RF Middle1 Stretch",
    "RF Middle Spread",
    "RF Middle2 Stretch",
    "RF Middle3 Stretch",
    "RF Ring1 Stretch",
    "RF Ring Spread",
    "RF Ring2 Stretch",
    "RF Ring3 Stretch",
    "RF Little1 Stretch",
    "RF Little Spread",
    "RF Little2 Stretch",
    "RF Little3 Stretch",
};

static void BuildDynamicMuscleMap() {
  void *domain = il2cpp_domain_get();
  if (!domain) {
    Log("[MUSCLE-MAP] No domain, keeping hardcoded map");
    return;
  }
  size_t ac = 0;
  void **asms = il2cpp_domain_get_assemblies(domain, &ac);
  if (!asms || ac == 0) {
    Log("[MUSCLE-MAP] No assemblies, keeping hardcoded map");
    return;
  }

  void *htClass = FindClass("UnityEngine", "HumanTrait", asms, ac);
  if (!htClass) {
    Log("[MUSCLE-MAP] HumanTrait class not found, keeping hardcoded map");
    return;
  }

  void *getMuscleNameMethod = FindMethod(htClass, "get_MuscleName", 0);
  if (!getMuscleNameMethod) {
    Log("[MUSCLE-MAP] get_MuscleName not found, keeping hardcoded map");
    return;
  }

  void *exc = nullptr;
  void *nameArray = il2cpp_runtime_invoke(getMuscleNameMethod, nullptr, nullptr, &exc);
  if (exc || !nameArray) {
    Log("[MUSCLE-MAP] get_MuscleName() failed: exc=%p ret=%p", exc, nameArray);
    return;
  }

  int gameMuscleCnt = 0;
  __try {
    gameMuscleCnt = *(int *)((char *)nameArray + 24);
  } __except (1) {
    Log("[MUSCLE-MAP] Failed to read array length");
    return;
  }

  Log("[MUSCLE-MAP] HumanTrait.MuscleName has %d entries (game muscles=%d)",
      gameMuscleCnt, s_actualMuscleCount);

  if (gameMuscleCnt < 95) {
    Log("[MUSCLE-MAP] Game has fewer than 95 muscles (%d), keeping hardcoded map",
        gameMuscleCnt);
    return;
  }

  void **elements = (void **)((char *)nameArray + 32);
  char gameNames[256][64] = {};
  int maxRead = (gameMuscleCnt > 255) ? 255 : gameMuscleCnt;

  for (int i = 0; i < maxRead; i++) {
    __try {
      void *strObj = elements[i];
      if (strObj)
        ReadStr(strObj, gameNames[i], 64);
      else
        gameNames[i][0] = 0;
    } __except (1) {
      gameNames[i][0] = 0;
    }
  }

  for (int i = 0; i < maxRead; i++) {
    Log("[MUSCLE-MAP]   game[%d] = \"%s\"%s", i, gameNames[i],
        i >= 95 ? " (EXTRA)" : "");
  }


  auto NormalizeMuscle = [](const char *src, char *dst, int dstSz) {
    char lower[128] = {};
    int li = 0;
    for (int i = 0; src[i] && li < 126; i++)
      lower[li++] = (src[i] >= 'A' && src[i] <= 'Z') ? (src[i] + 32) : src[i];
    lower[li] = 0;

    char *s = lower;
    int di = 0;

    while (*s && di < dstSz - 1) {
      if (strncmp(s, "left-right", 10) == 0) {
        dst[di++] = 'l'; dst[di++] = 'r';
        s += 10; continue;
      }
      if (strncmp(s, "l-r", 3) == 0) {
        dst[di++] = 'l'; dst[di++] = 'r';
        s += 3; continue;
      }
      if (strncmp(s, "stretched", 9) == 0) {
        memcpy(dst + di, "stretch", 7); di += 7;
        s += 9; continue;
      }
      if (strncmp(s, "left ", 5) == 0 && (s == lower || *(s - 1) == ' ')) {
        dst[di++] = 'l'; dst[di++] = ' ';
        s += 5; continue;
      }
      if (strncmp(s, "right ", 6) == 0 && (s == lower || *(s - 1) == ' ')) {
        dst[di++] = 'r'; dst[di++] = ' ';
        s += 6; continue;
      }
      if (strncmp(s, "upper leg", 9) == 0) {
        memcpy(dst + di, "upperleg", 8); di += 8;
        s += 9; continue;
      }
      if (strncmp(s, "lower leg", 9) == 0) {
        memcpy(dst + di, "lowerleg", 8); di += 8;
        s += 9; continue;
      }
      if (strncmp(s, "upper chest", 11) == 0) {
        memcpy(dst + di, "upperchest", 10); di += 10;
        s += 11; continue;
      }
      if (strncmp(s, "lf ", 3) == 0 && (s == lower || *(s - 1) == ' ')) {
        dst[di++] = 'l'; dst[di++] = ' ';
        s += 3; continue;
      }
      if (strncmp(s, "rf ", 3) == 0 && (s == lower || *(s - 1) == ' ')) {
        dst[di++] = 'r'; dst[di++] = ' ';
        s += 3; continue;
      }
      if (*s == ' ' && s[1] >= '0' && s[1] <= '9') {
        s++; continue;
      }
      if (strncmp(s, "twist in-out", 12) == 0) {
        memcpy(dst + di, "twist", 5); di += 5;
        s += 12; continue;
      }
      if (strncmp(s, "twist roll", 10) == 0) {
        memcpy(dst + di, "twist", 5); di += 5;
        s += 10; continue;
      }
      dst[di++] = *s++;
    }
    dst[di] = 0;
  };

  char gameNorm[256][64] = {};
  for (int i = 0; i < maxRead; i++) {
    NormalizeMuscle(gameNames[i], gameNorm[i], 64);
  }

  int mapped = 0;
  int mismatches = 0;
  for (int s = 0; s < 95; s++) {
    const char *stdName = g_muscleNames[s];
    char stdNorm[64] = {};
    NormalizeMuscle(stdName, stdNorm, 64);

    int found = -1;
    for (int g = 0; g < maxRead; g++) {
      if (gameNorm[g][0] && strcmp(stdNorm, gameNorm[g]) == 0) {
        found = g;
        break;
      }
    }
    if (found >= 0) {
      if (g_stdToGameMap[s] != found) {
        Log("[MUSCLE-MAP] Remap std[%d]=\"%s\" : hardcoded=%d -> dynamic=%d",
            s, stdName, g_stdToGameMap[s], found);
        mismatches++;
      }
      g_stdToGameMap[s] = found;
      mapped++;
    } else {
      Log("[MUSCLE-MAP] WARN: std[%d]=\"%s\" (norm=\"%s\") NOT FOUND! keeping=%d",
          s, stdName, stdNorm, g_stdToGameMap[s]);
    }
  }

  g_dynamicMapReady = true;
  Log("[MUSCLE-MAP] Dynamic map built: %d/95 matched, %d remapped vs hardcoded",
      mapped, mismatches);
  if (mismatches == 0) {
    Log("[MUSCLE-MAP] Hardcoded mapping matches dynamic — no changes needed");
  }
}

static volatile bool g_trojanReentrant = false;
static void *g_gameNativePtr =
    nullptr;

static void ResetFaceCache() {
  g_faceBonesCaptured = false;
  g_faceBoneRefs = nullptr;
  g_mouthShapesResolved = false;
  g_extraMorphsResolved = false;
  g_bigListCaptured = false;
  g_hashCorrelationDone = false;
  g_capturedLen = 0;
  g_smcResetRequested = true;

  for (int i = 0; i < NUM_MOUTH_SHAPES; i++) {
    g_mouthShapes[i].resolved = false;
  }
  for (int i = 0; i < NUM_EXTRA_MORPHS; i++) {
    for (int t = 0; t < g_extraMorphs[i].targetCount; t++) {
      g_extraMorphs[i].targets[t].resolved = false;
    }
  }
  Log("[RESET] Face cache cleared (including per-target resolved flags).");
}

static void CleanupPoseHandler() {
  if (g_poseHandleGC != 0) {
    void *managedObj = il2cpp_gchandle_get_target(g_poseHandleGC);
    if (managedObj && g_humanPoseHandler_Dispose) {
      __try {
        void *exc = nullptr;
        il2cpp_runtime_invoke(g_humanPoseHandler_Dispose, managedObj, nullptr,
                              &exc);
        Log("[CLEANUP] HumanPoseHandler.Dispose() called");
      } __except (1) {
        Log("[CLEANUP] Dispose failed (SEH)");
      }
    }
    il2cpp_gchandle_free(g_poseHandleGC);
    Log("[CLEANUP] Released HumanPoseHandler GC handle %u", g_poseHandleGC);
    g_poseHandleGC = 0;
  }
  if (g_musclesArrayGC != 0) {
    il2cpp_gchandle_free(g_musclesArrayGC);
    g_musclesArrayGC = 0;
  }
  g_cachedMPtr = nullptr;
  g_musclesArray = nullptr;
  g_gameNativePtr = nullptr;
  s_poseReady = false;
  if (g_trojanHookTarget) {
    MH_DisableHook(g_trojanHookTarget);
    Log("[CLEANUP] Trojan hook disabled");
  }
}

static void __cdecl Hooked_GetInternalAvatarPose(void *nativePtr, void *array,
                                                 int count) {
  if (orig_GetInternalAvatarPose) {
    orig_GetInternalAvatarPose(nativePtr, array, count);
  }

  if (ClothShoulderReadOnlyPoseActive()) return;

  if (!g_motionBackend.Is(MotionBackend::Muscle) ||
      !g_trojanActive || !g_mmdHasMuscles ||
      g_clothPlaybackGate.Holding(uint32_t(MotionBackend::Muscle),g_motionBackend.Generation()))
    return;
  if (g_trojanReentrant)
    return;

  if (g_cachedMPtr && nativePtr == g_cachedMPtr)
    return;

  if (!g_gameNativePtr) {
    g_gameNativePtr = nativePtr;
    Log("[TROJAN] Detected game nativePtr: %p (our m_Ptr: %p, count=%d)",
        nativePtr, g_cachedMPtr, count);
  }

  if (nativePtr != g_gameNativePtr)
    return;

  g_trojanReentrant = true;


  if (g_icall_SetInternalHumanPose && s_poseReady && s_musclePtr &&
      g_cachedMPtr) {
    memcpy(s_musclePtr, (void *)g_mmdMuscles, 95 * sizeof(float));

    float bodyPos[3] = {s_cachedPose.bodyPosX, s_cachedPose.bodyPosY,
                        s_cachedPose.bodyPosZ};
    float bodyRot[4] = {s_cachedPose.bodyRotX, s_cachedPose.bodyRotY,
                        s_cachedPose.bodyRotZ, s_cachedPose.bodyRotW};

    __try {
      g_icall_SetInternalHumanPose(g_cachedMPtr, bodyPos, bodyRot,
                                   s_cachedPose.muscles);

      if (orig_GetInternalAvatarPose) {
        orig_GetInternalAvatarPose(g_cachedMPtr, array, count);
      }
    } __except (EXCEPTION_EXECUTE_HANDLER) {
      static bool s_errLogged = false;
      if (!s_errLogged) {
        Log("[TROJAN] icall crashed: 0x%08X", GetExceptionCode());
        s_errLogged = true;
      }
    }

    static int s_logCount = 0;
    s_logCount++;

    float *avatarData = (float *)array;
    if (s_logCount == 1) {
      Log("[TROJAN-DUMP] Avatar pose buffer: array=%p count=%d (%.0f quats)",
          array, count, count / 4.0f);
      for (int i = 0; i < count && i < 80; i += 4) {
        Log("[TROJAN-DUMP] [%d-%d]: %.4f %.4f %.4f %.4f", i, i + 3,
            avatarData[i], avatarData[i + 1], avatarData[i + 2],
            avatarData[i + 3]);
      }
      for (int i = 80; i < count && i < 160; i += 4) {
        Log("[TROJAN-DUMP] [%d-%d]: %.4f %.4f %.4f %.4f", i, i + 3,
            avatarData[i], avatarData[i + 1], avatarData[i + 2],
            avatarData[i + 3]);
      }
      if (count > 160) {
        Log("[TROJAN-DUMP] ... total %d floats, showing range 160-%d:", count,
            count - 1);
        for (int i = 160; i < count; i += 4) {
          Log("[TROJAN-DUMP] [%d-%d]: %.4f %.4f %.4f %.4f", i, i + 3,
              avatarData[i], avatarData[i + 1], avatarData[i + 2],
              avatarData[i + 3]);
        }
      }
    }

    if (g_mmdHasArmBones && g_muscleAnim && g_muscleAnim->hasArmBones) {

      static const int armHBB[ARM_BONE_COUNT] = {13, 15, 17, 14, 16, 18};

      for (int i = 0; i < ARM_BONE_COUNT; i++) {
        float *mmdCur = (float *)&g_mmdArmBoneRots[i * 4];
        float *mmdRest = &g_muscleAnim->armRestRots[i * 4];

        int boneIdx = armHBB[i];
        int offset = boneIdx * 4;

        if (offset + 3 < count) {
          float invMR[4] = {-mmdRest[0], -mmdRest[1], -mmdRest[2], mmdRest[3]};
          float dx = invMR[3] * mmdCur[0] + invMR[0] * mmdCur[3] +
                     invMR[1] * mmdCur[2] - invMR[2] * mmdCur[1];
          float dy = invMR[3] * mmdCur[1] - invMR[0] * mmdCur[2] +
                     invMR[1] * mmdCur[3] + invMR[2] * mmdCur[0];
          float dz = invMR[3] * mmdCur[2] + invMR[0] * mmdCur[1] -
                     invMR[1] * mmdCur[0] + invMR[2] * mmdCur[3];
          float dw = invMR[3] * mmdCur[3] - invMR[0] * mmdCur[0] -
                     invMR[1] * mmdCur[1] - invMR[2] * mmdCur[2];

          float *gr =
              &avatarData[offset];
          static float s_origArm[ARM_BONE_COUNT * 4] = {};
          static bool s_origSaved = false;
          if (!s_origSaved && s_logCount == 1) {
            for (int j = 0; j < ARM_BONE_COUNT; j++) {
              int off = armHBB[j] * 4;
              if (off + 3 < count)
                memcpy(&s_origArm[j * 4], &avatarData[off], 16);
            }
            s_origSaved = true;
          }

          float ox = gr[0], oy = gr[1], oz = gr[2], ow = gr[3];
          gr[0] = ow * dx + ox * dw + oy * dz - oz * dy;
          gr[1] = ow * dy - ox * dz + oy * dw + oz * dx;
          gr[2] = ow * dz + ox * dy - oy * dx + oz * dw;
          gr[3] = ow * dw - ox * dx - oy * dy - oz * dz;
          float len = sqrtf(gr[0] * gr[0] + gr[1] * gr[1] + gr[2] * gr[2] +
                            gr[3] * gr[3]);
          if (len > 0.001f) {
            gr[0] /= len;
            gr[1] /= len;
            gr[2] /= len;
            gr[3] /= len;
          }
        }
      }
    }


    if (s_logCount <= 3 || (s_logCount % 300 == 0 && s_logCount <= 3000)) {
      Log("[TROJAN] #%d: direct icall applied! m[0]=%.3f m[42]=%.3f "
          "armOverride=%s",
          s_logCount, (float)g_mmdMuscles[0], (float)g_mmdMuscles[42],
          g_mmdHasArmBones ? "YES" : "no");
    }
  }

  g_trojanReentrant = false;
}

#define WM_MMD_APPLY_POSE (WM_USER + 1)

static WNDPROC g_origWndProc = nullptr;
static volatile bool g_mmdPendingApply = false;
static volatile bool g_mmdSetMode = false;

typedef void *(*InvokerFn)(void *methodPtr, void *method, void *obj,
                           void **params, void *retval);
static InvokerFn orig_Invoker = nullptr;
static void *__cdecl Hooked_Invoker(void *methodPtr, void *method, void *obj,
                                    void **params, void *retval) {
  return orig_Invoker(methodPtr, method, obj, params, retval);
}


typedef void(__cdecl *fn_SetHP_icall)(void *handler, void *bodyPos,
                                      void *bodyRot, void *muscles,
                                      void *methodInfo);
static fn_SetHP_icall s_directSetHP = nullptr;

static void InitMmdPoseOnMainThread() {
  if (s_poseReady)
    return;
  if (!g_muscleAnim || !g_muscleAnim->loaded)
    return;
  if (!g_poseHandleGC)
    return;
  if (!g_slotAddr || !g_slotSetFn)
    return;

  void *managedObj = il2cpp_gchandle_get_target(g_poseHandleGC);
  if (!managedObj)
    return;

  void *getArgs[] = {&s_cachedPose};
  void *getExc = nullptr;
  il2cpp_runtime_invoke(g_humanPoseHandler_GetHumanPose, managedObj, getArgs,
                        &getExc);
  if (getExc || !s_cachedPose.muscles) {
    Log("[MMD-INIT] GetHumanPose failed: exc=%p muscles=%p", getExc,
        s_cachedPose.muscles);
    return;
  }

  s_musclePtr = GetArrayData(s_cachedPose.muscles);
  if (!s_musclePtr)
    return;

  uint64_t arrayLen = *(uint64_t *)((char *)s_cachedPose.muscles + 24);
  s_actualMuscleCount = (int)arrayLen;
  if (s_actualMuscleCount > 128)
    s_actualMuscleCount = 128;
  Log("[MMD-INIT] Muscles array length = %llu (expected 95, got %d)", arrayLen,
      s_actualMuscleCount);

  if (!g_dynamicMapReady)
    BuildDynamicMuscleMap();

  if (*g_slotAddr) {
    g_slotOrigGet = *g_slotAddr;
    Log("[SLOT] Captured Get fn: %p", g_slotOrigGet);
  }

  uint32_t h = il2cpp_gchandle_new(s_cachedPose.muscles, true);

  s_directSetHP = (fn_SetHP_icall)g_slotSetFn;

  Log("[MMD-INIT] Ready! m_Ptr=%p muscles=%p setFn=%p", g_cachedMPtr,
      s_cachedPose.muscles, s_directSetHP);
  Log("[MMD-INIT] Rest: pos(%.3f,%.3f,%.3f) rot(%.3f,%.3f,%.3f,%.3f)",
      s_cachedPose.bodyPosX, s_cachedPose.bodyPosY, s_cachedPose.bodyPosZ,
      s_cachedPose.bodyRotX, s_cachedPose.bodyRotY, s_cachedPose.bodyRotZ,
      s_cachedPose.bodyRotW);

  memcpy(s_savedIdleMuscles, s_musclePtr, s_actualMuscleCount * sizeof(float));
  Log("[MMD-INIT] Saved %d idle muscles", s_actualMuscleCount);
  s_savedIdleBodyPos[0] = s_cachedPose.bodyPosX;
  s_savedIdleBodyPos[1] = s_cachedPose.bodyPosY;
  s_savedIdleBodyPos[2] = s_cachedPose.bodyPosZ;
  s_savedIdleBodyRot[0] = s_cachedPose.bodyRotX;
  s_savedIdleBodyRot[1] = s_cachedPose.bodyRotY;
  s_savedIdleBodyRot[2] = s_cachedPose.bodyRotZ;
  s_savedIdleBodyRot[3] = s_cachedPose.bodyRotW;
  Log("[MMD-INIT] Saved idle muscles: arm39=%.3f arm48=%.3f fore42=%.3f "
      "fore51=%.3f",
      s_savedIdleMuscles[39], s_savedIdleMuscles[48], s_savedIdleMuscles[42],
      s_savedIdleMuscles[51]);
  Log("[REST-MUSCLES] Spine[0-8]:  %.3f %.3f %.3f | %.3f %.3f %.3f | %.3f %.3f "
      "%.3f",
      s_musclePtr[0], s_musclePtr[1], s_musclePtr[2], s_musclePtr[3],
      s_musclePtr[4], s_musclePtr[5], s_musclePtr[6], s_musclePtr[7],
      s_musclePtr[8]);
  Log("[REST-MUSCLES] NeckHead[9-14]: %.3f %.3f %.3f | %.3f %.3f %.3f",
      s_musclePtr[9], s_musclePtr[10], s_musclePtr[11], s_musclePtr[12],
      s_musclePtr[13], s_musclePtr[14]);
  Log("[REST-MUSCLES] EyeJaw[15-20]: %.3f %.3f %.3f %.3f | %.3f %.3f",
      s_musclePtr[15], s_musclePtr[16], s_musclePtr[17], s_musclePtr[18],
      s_musclePtr[19], s_musclePtr[20]);
  Log("[REST-MUSCLES] LLeg[21-28]: %.3f %.3f %.3f | %.3f %.3f | %.3f %.3f | "
      "%.3f",
      s_musclePtr[21], s_musclePtr[22], s_musclePtr[23], s_musclePtr[24],
      s_musclePtr[25], s_musclePtr[26], s_musclePtr[27], s_musclePtr[28]);
  Log("[REST-MUSCLES] RLeg[29-36]: %.3f %.3f %.3f | %.3f %.3f | %.3f %.3f | "
      "%.3f",
      s_musclePtr[29], s_musclePtr[30], s_musclePtr[31], s_musclePtr[32],
      s_musclePtr[33], s_musclePtr[34], s_musclePtr[35], s_musclePtr[36]);
  Log("[REST-MUSCLES] LArm[37-45]: %.3f %.3f | %.3f %.3f %.3f | %.3f %.3f | "
      "%.3f %.3f",
      s_musclePtr[37], s_musclePtr[38], s_musclePtr[39], s_musclePtr[40],
      s_musclePtr[41], s_musclePtr[42], s_musclePtr[43], s_musclePtr[44],
      s_musclePtr[45]);
  Log("[REST-MUSCLES] RArm[46-54]: %.3f %.3f | %.3f %.3f %.3f | %.3f %.3f | "
      "%.3f %.3f",
      s_musclePtr[46], s_musclePtr[47], s_musclePtr[48], s_musclePtr[49],
      s_musclePtr[50], s_musclePtr[51], s_musclePtr[52], s_musclePtr[53],
      s_musclePtr[54]);
  Log("[REST-MUSCLES] LFinger[55-74]: %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f "
      "%.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f",
      s_musclePtr[55], s_musclePtr[56], s_musclePtr[57], s_musclePtr[58],
      s_musclePtr[59], s_musclePtr[60], s_musclePtr[61], s_musclePtr[62],
      s_musclePtr[63], s_musclePtr[64], s_musclePtr[65], s_musclePtr[66],
      s_musclePtr[67], s_musclePtr[68], s_musclePtr[69], s_musclePtr[70],
      s_musclePtr[71], s_musclePtr[72], s_musclePtr[73], s_musclePtr[74]);
  Log("[REST-MUSCLES] RFinger[75-94]: %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f "
      "%.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f %.2f",
      s_musclePtr[75], s_musclePtr[76], s_musclePtr[77], s_musclePtr[78],
      s_musclePtr[79], s_musclePtr[80], s_musclePtr[81], s_musclePtr[82],
      s_musclePtr[83], s_musclePtr[84], s_musclePtr[85], s_musclePtr[86],
      s_musclePtr[87], s_musclePtr[88], s_musclePtr[89], s_musclePtr[90],
      s_musclePtr[91], s_musclePtr[92], s_musclePtr[93], s_musclePtr[94]);

  memcpy(s_musclePtr, (void *)g_mmdMuscles, 95 * sizeof(float));
  *g_slotAddr = g_slotSetFn;
  void *setArgs[] = {&s_cachedPose};
  void *setExc = nullptr;
  il2cpp_runtime_invoke(g_humanPoseHandler_GetHumanPose, managedObj, setArgs,
                        &setExc);
  *g_slotAddr = g_slotOrigGet;
  Log("[MMD-INIT] Verification Set: exc=%s", setExc ? "ERR" : "OK");

  s_poseReady = true;
}

static void ApplyMmdPoseDirect() {
  if(g_clothPlaybackGate.Holding(uint32_t(MotionBackend::Muscle),g_motionBackend.Generation()))return;
  if (!s_poseReady || !s_directSetHP || !g_cachedMPtr)
    return;

  SafeSetAnimatorEnabled(false);

  memcpy(s_musclePtr, (void *)g_mmdMuscles, 95 * sizeof(float));

  float bodyPos[3] = {s_cachedPose.bodyPosX, s_cachedPose.bodyPosY,
                      s_cachedPose.bodyPosZ};
  float bodyRot[4] = {s_cachedPose.bodyRotX, s_cachedPose.bodyRotY,
                      s_cachedPose.bodyRotZ, s_cachedPose.bodyRotW};

  __try {
    s_directSetHP(g_cachedMPtr, bodyPos, bodyRot, s_cachedPose.muscles,
                  nullptr);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    static bool s_errLogged = false;
    if (!s_errLogged) {
      Log("[MMD-DIRECT] icall crashed: 0x%08X —falling back to slot-patch",
          GetExceptionCode());
      s_errLogged = true;
    }
    if (g_gameHwnd && !g_mmdPendingApply) {
      g_mmdPendingApply = true;
      PostMessageW(g_gameHwnd, WM_MMD_APPLY_POSE, 0, 0);
    }
    return;
  }

  static int s_frameCount = 0;
  s_frameCount++;
  if (s_frameCount <= 3 || (s_frameCount % 300 == 0 && s_frameCount <= 3000)) {
    Log("[MMD-DIRECT] #%d: m[0]=%.3f m[21]=%.3f m[42]=%.3f", s_frameCount,
        (float)g_mmdMuscles[0], (float)g_mmdMuscles[21],
        (float)g_mmdMuscles[42]);
  }
}


#define MAX_HUMAN_BONES 55
struct CachedBoneState {
  void *transform;
  Quat rotation;
  bool valid;
};
static CachedBoneState g_cachedBones[MAX_HUMAN_BONES] = {};
static Vec3 g_cachedHipsPos = {};
static void *g_hipsTransform = nullptr;
static volatile bool g_bonesReady = false;
static volatile int g_boneGeneration = 0;

static bool Muscle_ClothMayOwnAnchor(void *transform) {
  if (!g_motionBackend.Is(MotionBackend::Muscle) || transform == g_hipsTransform) return false;
  for (const auto &bone : g_cachedBones)
    if (bone.valid && bone.transform == transform) return false;
  if (g_resolvedMappings)
    for (const auto &mapping : *g_resolvedMappings)
      if (mapping.transform == transform) return false;
  return true;
}

static volatile int g_debugMuscleIdx = 0;
static volatile float g_debugMuscleVal = 0.0f;
static volatile bool g_debugMode = false;
static volatile bool g_debugDirty = true;
static volatile int g_debugMaxIdx =
    95;


static int StandardToGame(int stdIdx) {
  if (stdIdx < 0 || stdIdx > 94)
    return -1;
  return g_stdToGameMap[stdIdx];
}

static float s_mmdFirstMuscles[95] = {};
static float s_mmdFirstBodyPos[3] = {};
static float s_mmdFirstBodyRot[4] = {};
static bool s_firstFrame = true;

static float s_initialRootPos[3] = {};
static bool s_initialRootCaptured = false;
static float g_initialRootQuat[4] = {0, 0, 0, 1};
static Vec3 s_firstCenterDisp = {0, 0, 0};


static float s_firstLeftFootIK[3] = {};
static float s_firstRightFootIK[3] = {};
static Quat s_firstLeftFootIKRot = {0, 0, 0, 1};
static Quat s_firstRightFootIKRot = {0, 0, 0, 1};
static bool s_footIKFirstCaptured = false;
static float s_gameFootInitL[3] = {}, s_gameFootInitR[3] = {};
static bool s_footIKCalibrated = false;
static float g_ikCenterWorldDelta[3] = {};
static Vec3 s_curVmdCenter = {0, 0, 0};
static bool s_vmdCenterSampled = false;
static float s_bodyMotDelta[3] = {};

struct FootIKSample {
  Vec3 leftPos, rightPos;
  Quat leftRot, rightRot;
  Vec3 centerPos;
  bool valid;
};

static FootIKSample SampleFootIK(float timeSec) {
  FootIKSample result = {};
  result.valid = false;
  VmdFile *vmd = g_footIkVmd ? g_footIkVmd : g_vmd;
  if (!vmd || !vmd->loaded) return result;

  float frameF = timeSec * 30.0f;

  auto itL = vmd->boneTimelines.find(
      "\xe5\xb7\xa6\xe8\xb6\xb3\xef\xbc\xa9\xef\xbc\xab");
  auto itR = vmd->boneTimelines.find(
      "\xe5\x8f\xb3\xe8\xb6\xb3\xef\xbc\xa9\xef\xbc\xab");

  if (itL == vmd->boneTimelines.end() ||
      itR == vmd->boneTimelines.end())
    return result;

  InterpResult irL = InterpolateBone(itL->second.keys, frameF, true);
  InterpResult irR = InterpolateBone(itR->second.keys, frameF, true);

  result.leftPos = irL.position;
  result.leftRot = irL.rotation;
  result.rightPos = irR.position;
  result.rightRot = irR.rotation;

  result.centerPos = {0, 0, 0};
  auto itC = vmd->boneTimelines.find(
      "\xe3\x82\xbb\xe3\x83\xb3\xe3\x82\xbf\xe3\x83\xbc");
  if (itC != vmd->boneTimelines.end()) {
    InterpResult irC = InterpolateBone(itC->second.keys, frameF, true);
    result.centerPos = irC.position;
  }
  auto itG = vmd->boneTimelines.find(
      "\xe3\x82\xb0\xe3\x83\xab\xe3\x83\xbc\xe3\x83\x96");
  if (itG != vmd->boneTimelines.end()) {
    InterpResult irG = InterpolateBone(itG->second.keys, frameF, true);
    result.centerPos.x += irG.position.x;
    result.centerPos.y += irG.position.y;
    result.centerPos.z += irG.position.z;
  }

  result.valid = true;
  return result;
}

static float g_ikDeltaLf[3] = {};
static float g_ikDeltaRf[3] = {};

static void *g_activeLfSolver = nullptr;
static void *g_activeRfSolver = nullptr;
static bool g_mmdIKActive = false;

static float s_baseGroundY = 0.0f;
static bool s_baseGroundCaptured = false;
static float s_smoothedGroundDelta = 0.0f;
static float s_initialFootPosL[3] = {};
static float s_initialFootPosR[3] = {};
static bool s_footPosBaseCaptured = false;
static float s_lfBaseGround = 0.0f;
static bool s_lfBaseCaptured = false;
static float s_rfBaseGround = 0.0f;
static bool s_rfBaseCaptured = false;
static float s_curFootGroundDeltaL = 0.0f;
static float s_curFootGroundDeltaR = 0.0f;
static float s_desiredFootTargetL[3] = {};
static float s_desiredFootTargetR[3] = {};
static float s_curFootTargetL[3] = {};
static float s_curFootTargetR[3] = {};
static float s_baseAnkleHeight = 0.12f;
static float s_curFootLiftL = 0.0f;
static float s_curFootLiftR = 0.0f;

static float s_legMaxL = 0.0f;
static float s_legMaxR = 0.0f;
static bool s_legLengthCaptured = false;

static const float FOOT_ANKLE_REF_HEIGHT = 0.065f;
static float s_baseAnkleHeightL = FOOT_ANKLE_REF_HEIGHT;
static float s_baseAnkleHeightR = FOOT_ANKLE_REF_HEIGHT;
static bool s_ankleHeightCaptured = false;
static const float GROUND_SMOOTH_TAU_ROOT = 0.08f;
static const float GROUND_SMOOTH_TAU_FOOT = 0.03f;

static bool s_groundSmoothingInitialized = false;
static float s_smoothedFootGroundDeltaL = 0.0f;
static float s_smoothedFootGroundDeltaR = 0.0f;

static float s_curFootRotTargetL[4] = {0, 0, 0, 1};
static float s_curFootRotTargetR[4] = {0, 0, 0, 1};
static float s_curFootIKRotL[4] = {0, 0, 0, 1};
static float s_curFootIKRotR[4] = {0, 0, 0, 1};
static float s_curFootRotWeightL = 1.0f;
static float s_curFootRotWeightR = 1.0f;

static inline float FootIKClamp01(float v) {
    if (v < 0.0f) return 0.0f;
    if (v > 1.0f) return 1.0f;
    return v;
}

static inline float FootIKLerp(float a, float b, float t) {
    return a + (b - a) * t;
}

static inline float FootIKSmoothStep(float edge0, float edge1, float value) {
    if (edge1 <= edge0)
        return value >= edge1 ? 1.0f : 0.0f;

    float t = FootIKClamp01((value - edge0) / (edge1 - edge0));
    return t * t * (3.0f - 2.0f * t);
}

static void ClearActiveFootIKSolverWeights() {
    __try {
        if (g_activeLfSolver) {
            *(float *)((char *)g_activeLfSolver +
                       OFF_IKSOLVER_IKPOS_WEIGHT) = 0.0f;
            *(float *)((char *)g_activeLfSolver +
                       OFF_IKSOLVER_IKROT_WEIGHT) = 0.0f;
        }

        if (g_activeRfSolver) {
            *(float *)((char *)g_activeRfSolver +
                       OFF_IKSOLVER_IKPOS_WEIGHT) = 0.0f;
            *(float *)((char *)g_activeRfSolver +
                       OFF_IKSOLVER_IKROT_WEIGHT) = 0.0f;
        }
    } __except (EXCEPTION_EXECUTE_HANDLER) {
    }

    g_activeLfSolver = nullptr;
    g_activeRfSolver = nullptr;
    g_mmdIKActive = false;
}

static void ResetFootIKRuntimeState() {
    memset(g_ikDeltaLf, 0, sizeof(g_ikDeltaLf));
    memset(g_ikDeltaRf, 0, sizeof(g_ikDeltaRf));

    memset(s_desiredFootTargetL, 0, sizeof(s_desiredFootTargetL));
    memset(s_desiredFootTargetR, 0, sizeof(s_desiredFootTargetR));
    memset(s_curFootTargetL, 0, sizeof(s_curFootTargetL));
    memset(s_curFootTargetR, 0, sizeof(s_curFootTargetR));

    s_curFootGroundDeltaL = 0.0f;
    s_curFootGroundDeltaR = 0.0f;
    s_smoothedFootGroundDeltaL = 0.0f;
    s_smoothedFootGroundDeltaR = 0.0f;

    s_baseGroundCaptured = false;
    s_baseGroundY = 0.0f;
    s_smoothedGroundDelta = 0.0f;
    s_groundSmoothingInitialized = false;

    s_lfBaseCaptured = false;
    s_lfBaseGround = 0.0f;
    s_rfBaseCaptured = false;
    s_rfBaseGround = 0.0f;

    s_curFootLiftL = 0.0f;
    s_curFootLiftR = 0.0f;

    s_curFootRotWeightL = 0.0f;
    s_curFootRotWeightR = 0.0f;

    s_curFootRotTargetL[0] = 0; s_curFootRotTargetL[1] = 0; s_curFootRotTargetL[2] = 0; s_curFootRotTargetL[3] = 1.0f;
    s_curFootRotTargetR[0] = 0; s_curFootRotTargetR[1] = 0; s_curFootRotTargetR[2] = 0; s_curFootRotTargetR[3] = 1.0f;
    s_curFootIKRotL[0] = 0; s_curFootIKRotL[1] = 0; s_curFootIKRotL[2] = 0; s_curFootIKRotL[3] = 1.0f;
    s_curFootIKRotR[0] = 0; s_curFootIKRotR[1] = 0; s_curFootIKRotR[2] = 0; s_curFootIKRotR[3] = 1.0f;

    s_legMaxL = 0.0f;
    s_legMaxR = 0.0f;
    s_legLengthCaptured = false;

    s_baseAnkleHeightL = FOOT_ANKLE_REF_HEIGHT;
    s_baseAnkleHeightR = FOOT_ANKLE_REF_HEIGHT;
    s_ankleHeightCaptured = false;

    s_footIKFirstCaptured = false;
    s_footIKCalibrated = false;
    s_footPosBaseCaptured = false;
    g_groundDeltaY = 0.0f;
}

static inline void QuatMultiply(const float *q1, const float *q2, float *out) {
  float x = q1[3]*q2[0] + q1[0]*q2[3] + q1[1]*q2[2] - q1[2]*q2[1];
  float y = q1[3]*q2[1] - q1[0]*q2[2] + q1[1]*q2[3] + q1[2]*q2[0];
  float z = q1[3]*q2[2] + q1[0]*q2[1] - q1[1]*q2[0] + q1[2]*q2[3];
  float w = q1[3]*q2[3] - q1[0]*q2[0] - q1[1]*q2[1] - q1[2]*q2[2];
  out[0] = x; out[1] = y; out[2] = z; out[3] = w;
}

static void *s_origMoveTick = nullptr;
static void *s_cachedMovementComp = nullptr;
static void *s_cachedEntity = nullptr;

static void *GhostRig_GetOwnerGrounderBipedIK(
    uintptr_t ownerCharacter, void *bipedIK) {
  if (!GhostRig_RequireMainThread("GrounderBipedIK.ResolveOwner", false) ||
      !g_finalIkGrounderLayoutOk || !ownerCharacter ||
      reinterpret_cast<uintptr_t>(g_mainCharEntity) != ownerCharacter ||
      reinterpret_cast<uintptr_t>(s_cachedEntity) != ownerCharacter ||
      !s_cachedMovementComp || g_offMovementGrounder < 0)
    return nullptr;
  void *grounder = nullptr;
  void *linkedBiped = nullptr;
  __try {
    grounder = *reinterpret_cast<void **>(
        (char *)s_cachedMovementComp + g_offMovementGrounder);
    if (!grounder || reinterpret_cast<uintptr_t>(grounder) < 0x10000)
      return nullptr;
    void *klass = il2cpp_object_get_class
        ? il2cpp_object_get_class(grounder)
        : nullptr;
    const char *name = klass && il2cpp_class_get_name
        ? il2cpp_class_get_name(klass)
        : nullptr;
    const char *nameSpace = klass && il2cpp_class_get_namespace
        ? il2cpp_class_get_namespace(klass)
        : nullptr;
    if (!name || !nameSpace || strcmp(name, "GrounderBipedIK") != 0 ||
        strcmp(nameSpace, "RootMotion.FinalIK") != 0)
      return nullptr;
    linkedBiped = *reinterpret_cast<void **>(
        (char *)grounder + OFF_GROUNDER_BIPED_IK);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return nullptr;
  }
  return (!bipedIK || linkedBiped == bipedIK) ? grounder : nullptr;
}

typedef void (__fastcall *FindFloorNative_t)(
  void *self, float *position, void *outResult, float stepDown, void *methodInfo);
static FindFloorNative_t s_findFloorNativeFn = nullptr;

typedef void (__fastcall *ComputeFloorDistNative_t)(
  void *self, float *position, float sweepDistance, void *outResult,
  float sweepRadius, bool forceWalkable, float stepDownOffset,
  bool checkClimbable, void *methodInfo);
static ComputeFloorDistNative_t s_computeFloorDistNativeFn = nullptr;

static bool GhostRig_QueryFindFloorSample(
    uintptr_t ownerCharacter, VmdVec3 queryPosition, float stepDown,
    DirectVmdTerrainProbeHit *sample) {
  static int s_lastUnavailableFrame = INT_MIN;
  static int s_lastRejectedFrame = INT_MIN;
  static bool s_layoutLogged = false;
  static bool s_sehLogged = false;
  if (!sample)
    return false;
  *sample = DirectVmdTerrainProbeHit();
  if (!GhostRig_RequireMainThread("FindFloor.DirectVmd", false))
    return false;
  const bool ownerMatches = ownerCharacter &&
      reinterpret_cast<uintptr_t>(g_mainCharEntity) == ownerCharacter &&
      reinterpret_cast<uintptr_t>(s_cachedEntity) == ownerCharacter;
  if (!ownerMatches || !s_cachedMovementComp ||
      !g_computeFloorDistMethod) {
    const int frame = GhostRig_GetFrameCount();
    if (s_lastUnavailableFrame == INT_MIN || frame < 0 ||
        frame - s_lastUnavailableFrame >= 120) {
      s_lastUnavailableFrame = frame;
      Log("[P7-FINDFLOOR-API] available=0 unityFrame=%d method=%p "
          "movement=%p cachedEntity=%p mainEntity=%p owner=%p "
          "action=hold-stable-plane-then-flat-air-target tid=%lu",
          frame, g_computeFloorDistMethod, s_cachedMovementComp,
          s_cachedEntity, g_mainCharEntity,
          reinterpret_cast<void *>(ownerCharacter),
          GetCurrentThreadId());
    }
    return false;
  }
  if (!DirectVmdFinite(queryPosition.x) ||
      !DirectVmdFinite(queryPosition.y) ||
      !DirectVmdFinite(queryPosition.z) ||
      !DirectVmdFinite(stepDown) || stepDown <= 0.0f)
    return false;

  if (!s_computeFloorDistNativeFn) {
    __try {
      s_computeFloorDistNativeFn =
          reinterpret_cast<ComputeFloorDistNative_t>(
              reinterpret_cast<MInfo *>(g_computeFloorDistMethod)->mp);
    } __except (EXCEPTION_EXECUTE_HANDLER) {
      s_computeFloorDistNativeFn = nullptr;
    }
  }
  if (!s_computeFloorDistNativeFn)
    return false;
  static constexpr float kDirectVmdSweepRadius = 0.010f;
  if (!s_layoutLogged) {
    s_layoutLogged = true;
    Log("[P7-FINDFLOOR-API] available=1 native=%p method=%p "
        "sampler=ComputeFloorDist sweepRadius=%.3f "
        "resultLayout=hit+0x00,walkable+0x01,dist+0x08,"
        "point+0x10,normal+0x1C mainThreadOnly=1 tid=%lu",
        s_computeFloorDistNativeFn, g_computeFloorDistMethod,
        kDirectVmdSweepRadius, GetCurrentThreadId());
  }

  float position[3] = {
      queryPosition.x, queryPosition.y, queryPosition.z};
  alignas(16) unsigned char floorResult[0xB0] = {};
  __try {
    s_computeFloorDistNativeFn(
        s_cachedMovementComp, position, stepDown, floorResult,
        kDirectVmdSweepRadius, false, stepDown, false,
        g_computeFloorDistMethod);
    const bool hit = *reinterpret_cast<bool *>(floorResult + 0x00);
    const bool walkable = *reinterpret_cast<bool *>(floorResult + 0x01);
    if (!hit || !walkable)
      return false;
    const float floorDistance =
        *reinterpret_cast<const float *>(floorResult + 0x08);
    const float *point =
        reinterpret_cast<const float *>(floorResult + 0x10);
    const float *normal =
        reinterpret_cast<const float *>(floorResult + 0x1C);
    sample->query = queryPosition;
    sample->point = {point[0], point[1], point[2]};
    sample->normal = {normal[0], normal[1], normal[2]};
    sample->floorDistance = floorDistance;
    if (!DirectVmdFinite(sample->point.x) ||
        !DirectVmdFinite(sample->point.y) ||
        !DirectVmdFinite(sample->point.z) ||
        !DirectVmdFinite(sample->normal.x) ||
        !DirectVmdFinite(sample->normal.y) ||
        !DirectVmdFinite(sample->normal.z) ||
        !DirectVmdFinite(sample->floorDistance) ||
        sample->floorDistance < 0.0f) {
      *sample = DirectVmdTerrainProbeHit();
      return false;
    }
    const float dx = sample->point.x - queryPosition.x;
    const float dz = sample->point.z - queryPosition.z;
    const float horizontalError = std::sqrt(dx * dx + dz * dz);
    static constexpr float kMaximumHorizontalError = 0.035f;
    if (!DirectVmdFinite(horizontalError) ||
        horizontalError > kMaximumHorizontalError) {
      const int frame = GhostRig_GetFrameCount();
      if (s_lastRejectedFrame == INT_MIN || frame < 0 ||
          frame - s_lastRejectedFrame >= 120) {
        s_lastRejectedFrame = frame;
        Log("[P7-FINDFLOOR-REJECT] unityFrame=%d reason=xz-error "
            "error=%.6f limit=%.6f query=(%.6f,%.6f,%.6f) "
            "point=(%.6f,%.6f,%.6f) floorDist=%.6f "
            "sampler=ComputeFloorDist tid=%lu",
            frame, horizontalError, kMaximumHorizontalError,
            queryPosition.x, queryPosition.y, queryPosition.z,
            sample->point.x, sample->point.y, sample->point.z,
            sample->floorDistance, GetCurrentThreadId());
      }
      *sample = DirectVmdTerrainProbeHit();
      return false;
    }
    sample->hit = 1;
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    *sample = DirectVmdTerrainProbeHit();
    if (!s_sehLogged) {
      s_sehLogged = true;
      Log("[P7-FINDFLOOR-API] SEH owner=%p movement=%p method=%p "
          "terrainSampleRejected=1 tid=%lu",
          reinterpret_cast<void *>(ownerCharacter),
          s_cachedMovementComp, g_computeFloorDistMethod,
          GetCurrentThreadId());
    }
    return false;
  }
}

static void __fastcall Hooked_MovementComponent_Tick(void *self, float deltaTime, void *methodInfo) {
  typedef void (__fastcall *fn)(void *, float, void *);

  if (!s_cachedMovementComp && g_cachedAnimator) {
    __try {
      void *entity = *(void **)((char *)self + g_offBaseCompEntity);
      if (entity && (uintptr_t)entity > 0x10000) {
        if (entity == g_mainCharEntity || !g_mainCharEntity) {
          s_cachedMovementComp = self;
          s_cachedEntity = entity;
          Log("[GF2-HOOK] MATCH! MovementComponent=%p Entity=%p", self, entity);
        }
      }
    } __except(1) {}
  }

  if (s_origMoveTick)
    ((fn)s_origMoveTick)(self, deltaTime, methodInfo);
}

static void *s_origLateUpdate = nullptr;
static void *s_origUpdateSolver = nullptr;
static void __fastcall Hooked_IK_UpdateSolver(void *self, void *methodInfo);

static void __fastcall Hooked_SolverManager_LateUpdate(void *self, void *methodInfo) {
  typedef void (__fastcall *fn)(void *, void *);
  GhostRig_OnSolverManagerLateUpdate(self);
  if (s_origLateUpdate) {
    ((fn)s_origLateUpdate)(self, methodInfo);
  }
  ClothTick("SolverManager.after-LateUpdate");
  DirectVmdCamera_UpdateMainThread(GhostRig_GetFrameCount());
}

static void __fastcall Hooked_OnUpdate(void *self, void *methodInfo) {
  typedef void (__fastcall *fn)(void *, void *);

  __try {
    if (g_motionBackend.Is(MotionBackend::Muscle) && g_mmdIKActive &&
        !g_clothPlaybackGate.BlocksPose(uint32_t(MotionBackend::Muscle),g_motionBackend.Generation())) {
      if (self == g_activeLfSolver && g_activeLfSolver) {
        *(float *)((char *)self + OFF_IKSOLVER_IKPOS_X) = s_curFootTargetL[0];
        *(float *)((char *)self + OFF_IKSOLVER_IKPOS_Y) = s_curFootTargetL[1];
        *(float *)((char *)self + OFF_IKSOLVER_IKPOS_Z) = s_curFootTargetL[2];
        *(float *)((char *)self + OFF_IKSOLVER_IKPOS_WEIGHT) = 1.0f;

        *(float *)((char *)self + OFF_IKSOLVER_IKROT_WEIGHT) = s_curFootRotWeightL;
        *(float *)((char *)self + OFF_IKSOLVER_IKROT_X) = s_curFootRotTargetL[0];
        *(float *)((char *)self + OFF_IKSOLVER_IKROT_Y) = s_curFootRotTargetL[1];
        *(float *)((char *)self + OFF_IKSOLVER_IKROT_Z) = s_curFootRotTargetL[2];
        *(float *)((char *)self + OFF_IKSOLVER_IKROT_W) = s_curFootRotTargetL[3];
      }
      else if (self == g_activeRfSolver && g_activeRfSolver) {
        *(float *)((char *)self + OFF_IKSOLVER_IKPOS_X) = s_curFootTargetR[0];
        *(float *)((char *)self + OFF_IKSOLVER_IKPOS_Y) = s_curFootTargetR[1];
        *(float *)((char *)self + OFF_IKSOLVER_IKPOS_Z) = s_curFootTargetR[2];
        *(float *)((char *)self + OFF_IKSOLVER_IKPOS_WEIGHT) = 1.0f;

        *(float *)((char *)self + OFF_IKSOLVER_IKROT_WEIGHT) = s_curFootRotWeightR;
        *(float *)((char *)self + OFF_IKSOLVER_IKROT_X) = s_curFootRotTargetR[0];
        *(float *)((char *)self + OFF_IKSOLVER_IKROT_Y) = s_curFootRotTargetR[1];
        *(float *)((char *)self + OFF_IKSOLVER_IKROT_Z) = s_curFootRotTargetR[2];
        *(float *)((char *)self + OFF_IKSOLVER_IKROT_W) = s_curFootRotTargetR[3];
      }
    } else if (g_motionBackend.Is(MotionBackend::DirectVmd)) {
      GhostRig_BeforeLegSolverUpdate(self, methodInfo);
    }

    if (g_origIkTrigOnUpdate) {
      ((fn)g_origIkTrigOnUpdate)(self, methodInfo);
    }
  } __except (EXCEPTION_EXECUTE_HANDLER) {}
  GhostRig_AfterLegSolverUpdate(self);
}

static void __fastcall Hooked_IK_UpdateSolver(void *self, void *methodInfo) {
  typedef void (__fastcall *fn)(void *, void *);
  GhostRig_BeforeFinalIK(self);
  if (GhostRig_ShouldSuppressFinalIKPhase5Safety(self)) {
    GhostRig_LogFinalIKSuppressedPhase5Safety(self);
    return;
  }

  GhostRig_LogFinalIKEntry(self);
  if (s_origUpdateSolver) {
    ((fn)s_origUpdateSolver)(self, methodInfo);
  }
  GhostRig_AfterFinalIK(self);
  if (ClothOnMainThread() && g_motionBackend.Is(MotionBackend::Muscle) && g_footIKEnabled &&
      g_musclePlayer && ClothOwns(s_cloth.owner)) {
    for (int n = 0; n < s_bipedIKCount; ++n)
      if (self == s_bipedIK[n] && ClothAnchorUnderOwner(CollisionTransform(self))) {
        ClothInputSubmit("Muscle.after-owner-FinalIK", s_clothInput.playheadFrame);
        break;
      }
  }
}

static void PreSampleVmdCenter() {
  s_vmdCenterSampled = false;
  VmdFile *vmd = g_footIkVmd ? g_footIkVmd : g_vmd;
  if (g_musclePlayer && vmd && vmd->loaded) {
    float frameF = g_musclePlayer->currentTime * 30.0f;
    Vec3 center = {0, 0, 0};
    auto itC = vmd->boneTimelines.find(
        "\xe3\x82\xbb\xe3\x83\xb3\xe3\x82\xbf\xe3\x83\xbc");
    if (itC != vmd->boneTimelines.end()) {
      InterpResult irC = InterpolateBone(itC->second.keys, frameF, true);
      center = irC.position;
    }
    auto itG = vmd->boneTimelines.find(
        "\xe3\x82\xb0\xe3\x83\xab\xe3\x83\xbc\xe3\x83\x96");
    if (itG != vmd->boneTimelines.end()) {
      InterpResult irG = InterpolateBone(itG->second.keys, frameF, true);
      center.x += irG.position.x;
      center.y += irG.position.y;
      center.z += irG.position.z;
    }
    s_curVmdCenter = center;
    s_vmdCenterSampled = true;
  }
}

static void ConfigureIKComponents(bool footIKEnabled) {
  if (footIKEnabled && !g_finalIkSolverLayoutOk) {
    Log("[IK-CONFIG] FinalIK layout unconfirmed; using pure Muscle mode");
    footIKEnabled = false;
  }
  if (footIKEnabled) {
    for (int bi = 0; bi < s_bipedIKCount; bi++) {
      if (s_bipedIK[bi]) {
        if (g_animator_set_enabled) {
          int trueVal = 1; void *params[] = {&trueVal};
          __try { Invoke(g_animator_set_enabled, s_bipedIK[bi], params); } __except(1) {}
        }
        *(bool *)((char *)s_bipedIK[bi] + OFF_BIPEDIK_FIX_TRANSFORMS) = false;
        __try {
          void *solvers = *(void **)((char *)s_bipedIK[bi] + OFF_BIPEDIK_SOLVERS);
          if (solvers) {
            void *lh = *(void **)((char *)solvers + OFF_SOLVERS_LEFT_HAND);
            void *rh = *(void **)((char *)solvers + OFF_SOLVERS_RIGHT_HAND);
            void *sp = *(void **)((char *)solvers + OFF_SOLVERS_SPINE);
            void *la = *(void **)((char *)solvers + OFF_SOLVERS_LOOKAT);
            void *aim = *(void **)((char *)solvers + OFF_SOLVERS_AIM);
            void *pelvis = *(void **)((char *)solvers + OFF_SOLVERS_PELVIS);
            if (lh) {
              *(float *)((char *)lh + OFF_IKSOLVER_IKPOS_WEIGHT) = 0.0f;
            }
            if (rh) {
              *(float *)((char *)rh + OFF_IKSOLVER_IKPOS_WEIGHT) = 0.0f;
            }
            if (sp) {
              *(float *)((char *)sp + OFF_IKSOLVER_IKPOS_WEIGHT) = 0.0f;
            }
            if (la) {
              *(float *)((char *)la + OFF_IKSOLVER_IKPOS_WEIGHT) = 0.0f;
              *(float *)((char *)la + 0xA0) = 0.0f;
              *(float *)((char *)la + 0xA4) = 0.0f;
              *(float *)((char *)la + 0xA8) = 0.0f;
              *(float *)((char *)la + 0xAC) = 0.0f;
            }
            if (aim) {
              *(float *)((char *)aim + OFF_IKSOLVER_IKPOS_WEIGHT) = 0.0f;
              *(float *)((char *)aim + 0xB4) = 0.0f;
              *(float *)((char *)aim + 0xC0) = 0.0f;
            }
            if (pelvis) {
              *(float *)((char *)pelvis + OFF_BIPED_PELVIS_POS_WEIGHT) = 0.0f;
              *(float *)((char *)pelvis + OFF_BIPED_PELVIS_ROT_WEIGHT) = 0.0f;
            }
          }
        } __except (1) {}
      }
    }
    for (int gi = 0; gi < s_grounderIKCount; gi++) {
      if (s_grounderIK[gi]) {
        if (g_animator_set_enabled) {
          int enabledVal = g_finalIkGrounderLayoutOk ? 1 : 0;
          void *params[] = {&enabledVal};
          __try { Invoke(g_animator_set_enabled, s_grounderIK[gi], params); } __except(1) {}
        }
        __try {
          if (g_finalIkGrounderLayoutOk) {
            *(float *)((char *)s_grounderIK[gi] + OFF_GROUNDER_WEIGHT) = 0.0f;
            *(float *)((char *)s_grounderIK[gi] + OFF_GROUNDER_MAINTAIN_WEIGHT) = 0.0f;
            *(float *)((char *)s_grounderIK[gi] + OFF_GROUNDER_ADSORB_WEIGHT) = 0.0f;
          }
        } __except (1) {}
      }
    }
    Log("[IK-CONFIG] Configured for Native IK Mode (BipedIK enabled, Grounder weight=0)");
  } else {
    for (int bi = 0; bi < s_bipedIKCount; bi++) {
      if (s_bipedIK[bi] && g_animator_set_enabled) {
        int falseVal = 0; void *params[] = {&falseVal};
        __try {
          Invoke(g_animator_set_enabled, s_bipedIK[bi], params);
        } __except(1) {}
      }
    }
    for (int gi = 0; gi < s_grounderIKCount; gi++) {
      if (s_grounderIK[gi] && g_animator_set_enabled) {
        int falseVal = 0; void *params[] = {&falseVal};
        __try {
          Invoke(g_animator_set_enabled, s_grounderIK[gi], params);
        } __except(1) {}
      }
    }
    Log("[IK-CONFIG] Configured for Pure Muscle Mode (BipedIK & Grounder disabled)");
  }
}

static void ApplyMmdPoseOnMainThread() {
  if(g_clothPlaybackGate.BlocksPose(uint32_t(MotionBackend::Muscle),g_motionBackend.Generation()))return;
  if (!s_poseReady || !s_musclePtr)
    return;
  if (!g_poseHandleGC || !g_slotAddr || !g_slotOrigGet)
    return;

  void *managedObj = il2cpp_gchandle_get_target(g_poseHandleGC);
  if (!managedObj)
    return;

  static bool s_lastConfiguredFootIKMode = true;
  if (s_ikDisabled && s_lastConfiguredFootIKMode != g_footIKEnabled) {
    s_lastConfiguredFootIKMode = g_footIKEnabled;
    ConfigureIKComponents(g_footIKEnabled);
  }

  SafeSetAnimatorEnabled(false);

  if (g_debugMode) {
    int mc = s_actualMuscleCount;
    for (int i = 0; i < mc; i++) {
      s_musclePtr[i] = s_savedIdleMuscles[i];
    }
    int idx = g_debugMuscleIdx;
    if (idx >= 0 && idx < mc) {
      s_musclePtr[idx] = s_savedIdleMuscles[idx] + g_debugMuscleVal;
    }
    s_cachedPose.bodyPosX = s_savedIdleBodyPos[0];
    s_cachedPose.bodyPosY = s_savedIdleBodyPos[1];
    s_cachedPose.bodyPosZ = s_savedIdleBodyPos[2];
    s_cachedPose.bodyRotX = s_savedIdleBodyRot[0];
    s_cachedPose.bodyRotY = s_savedIdleBodyRot[1];
    s_cachedPose.bodyRotZ = s_savedIdleBodyRot[2];
    s_cachedPose.bodyRotW = s_savedIdleBodyRot[3];

    *g_slotAddr = g_slotSetFn;
    void *setArgs[] = {&s_cachedPose};
    void *setExc = nullptr;
    il2cpp_runtime_invoke(g_humanPoseHandler_GetHumanPose, managedObj, setArgs,
                          &setExc);
    *g_slotAddr = g_slotOrigGet;

    if (g_debugDirty) {
      const char *name =
          (idx >= 0 && idx < 95) ? g_muscleNames[idx] : "(EXTRA)";
      Log("[DEBUG] muscle[%d/%d] = idle(%.3f) + %.1f = %.3f  \"%s\"", idx,
          s_actualMuscleCount, s_savedIdleMuscles[idx], (float)g_debugMuscleVal,
          s_musclePtr[idx], name);
      g_debugDirty = false;
    }
    if (!setExc) ClothTick("Muscle.WndProc.debug-after-pose", true, Muscle_ClothMayOwnAnchor);
    return;
  }


  if (s_firstFrame) {
    s_baseGroundCaptured = false;
    s_baseGroundY = 0.0f;
    s_lfBaseCaptured = false;
    s_lfBaseGround = 0.0f;
    s_rfBaseCaptured = false;
    s_rfBaseGround = 0.0f;
    s_footPosBaseCaptured = false;
    g_groundDeltaY = 0.0f;

    memcpy(s_mmdFirstMuscles, (void *)g_mmdMuscles, 95 * sizeof(float));
    s_mmdFirstBodyPos[0] = g_mmdBodyPos[0];
    s_mmdFirstBodyPos[1] = g_mmdBodyPos[1];
    s_mmdFirstBodyPos[2] = g_mmdBodyPos[2];
    Log("[VMD-INIT] firstBodyPos=(%.3f,%.3f,%.3f) firstFootIK_L=(%.3f,%.3f,%.3f)"
        " firstFootIK_R=(%.3f,%.3f,%.3f)",
        s_mmdFirstBodyPos[0], s_mmdFirstBodyPos[1], s_mmdFirstBodyPos[2],
        s_firstLeftFootIK[0], s_firstLeftFootIK[1], s_firstLeftFootIK[2],
        s_firstRightFootIK[0], s_firstRightFootIK[1], s_firstRightFootIK[2]);
    s_mmdFirstBodyRot[0] = g_mmdBodyRot[0];
    s_mmdFirstBodyRot[1] = g_mmdBodyRot[1];
    s_mmdFirstBodyRot[2] = g_mmdBodyRot[2];
    s_mmdFirstBodyRot[3] = g_mmdBodyRot[3];
    if (!s_initialRootCaptured) {
      void *rootT = SafeGetComponentTransform(g_cachedAnimator);
      if (rootT && g_camGetPos) {
        g_camGetPos(rootT, s_initialRootPos);
        s_initialRootCaptured = true;
        Log("[ROOT-MOVE] Initial root pos: (%.2f, %.2f, %.2f)",
            s_initialRootPos[0], s_initialRootPos[1], s_initialRootPos[2]);
        if (g_camGetRot) {
          g_camGetRot(rootT, g_initialRootQuat);
          float yawDeg = atan2f(2.0f * (g_initialRootQuat[0]*g_initialRootQuat[2] + g_initialRootQuat[1]*g_initialRootQuat[3]),
                                1.0f - 2.0f * (g_initialRootQuat[0]*g_initialRootQuat[0] + g_initialRootQuat[2]*g_initialRootQuat[2])) * 57.2958f;
          Log("[ROOT-MOVE] Initial root quat: (%.4f,%.4f,%.4f,%.4f) yaw=%.1f deg",
              g_initialRootQuat[0], g_initialRootQuat[1], g_initialRootQuat[2], g_initialRootQuat[3], yawDeg);
        }
      }
    }
    if (!s_footIKFirstCaptured && g_musclePlayer) {
      FootIKSample ik0 = SampleFootIK(g_musclePlayer->currentTime);
      if (ik0.valid) {
        s_firstLeftFootIK[0] = ik0.leftPos.x;
        s_firstLeftFootIK[1] = ik0.leftPos.y;
        s_firstLeftFootIK[2] = ik0.leftPos.z;
        s_firstRightFootIK[0] = ik0.rightPos.x;
        s_firstRightFootIK[1] = ik0.rightPos.y;
        s_firstRightFootIK[2] = ik0.rightPos.z;
        s_firstLeftFootIKRot = ik0.leftRot;
        s_firstRightFootIKRot = ik0.rightRot;
        s_firstCenterDisp = ik0.centerPos;
        s_footIKFirstCaptured = true;

        s_footPosBaseCaptured = true;
        s_footIKCalibrated = true;
        Log("[FOOT-IK] First frame: L=(%.3f,%.3f,%.3f) R=(%.3f,%.3f,%.3f)"
            " center=(%.3f,%.3f,%.3f) baseAnkleL=%.3f baseAnkleR=%.3f",
            ik0.leftPos.x, ik0.leftPos.y, ik0.leftPos.z,
            ik0.rightPos.x, ik0.rightPos.y, ik0.rightPos.z,
            ik0.centerPos.x, ik0.centerPos.y, ik0.centerPos.z, s_baseAnkleHeightL, s_baseAnkleHeightR);
      }
    }


    Log("[REMAP] 95—01 remapping active. First frame captured.");
    Log("[REMAP] MMD arm39→game%d arm48→game%d "
        "fore42→game%d",
        StandardToGame(39), StandardToGame(48), StandardToGame(42));
    s_firstFrame = false;
  }

  int mc = s_actualMuscleCount;
  for (int i = 0; i < mc; i++) {
    s_musclePtr[i] = 0.0f;
  }


  static const float SHOULDER_DU_OFFSET = 0.0f;
  static const float ARM_DU_OFFSET = -0.10f;
  static const float ARM_FB_OFFSET = 0.12f;
  static const float FOREARM_STRETCH_OFFSET = -0.08f;
  static const float NECK_NOD_OFFSET = 0.05f;

  for (int stdIdx = 0; stdIdx < 95; stdIdx++) {
    int gameIdx = StandardToGame(stdIdx);
    if (gameIdx < 0 || gameIdx >= mc)
      continue;

    float mmdCur = ((volatile float *)g_mmdMuscles)[stdIdx];

    if (g_footIKEnabled && stdIdx >= 21 && stdIdx <= 36) {
      s_musclePtr[gameIdx] = 0.0f;
      continue;
    }

    switch (stdIdx) {
    case 9:
      mmdCur += NECK_NOD_OFFSET;
      break;
    case 39:
      mmdCur += ARM_DU_OFFSET;
      break;
    case 48:
      mmdCur += ARM_DU_OFFSET;
      break;
    case 40:
      mmdCur += ARM_FB_OFFSET;
      break;
    case 49:
      mmdCur += ARM_FB_OFFSET;
      break;
    case 42:
      mmdCur += FOREARM_STRETCH_OFFSET;
      break;
    case 51:
      mmdCur += FOREARM_STRETCH_OFFSET;
      break;
    }

    float partScale = GetMuscleScale(stdIdx);
    if (partScale != 1.0f) {
      mmdCur = mmdCur * partScale;
    }

    s_musclePtr[gameIdx] = mmdCur;
  }

  if (g_musclePlayer) {
    FootIKSample ikSample = SampleFootIK(g_musclePlayer->currentTime);
    if (ikSample.valid) {
      s_curVmdCenter = ikSample.centerPos;
      s_vmdCenterSampled = true;

      if (s_footIKFirstCaptured && s_initialRootCaptured) {
        const float IK_SCALE = 0.08f * g_motionScale;
        const float PMX_REST_L_X = +1.25f;
        const float PMX_REST_R_X = -1.25f;

        float liftL = (ikSample.leftPos.y > 0.0f) ? ikSample.leftPos.y * IK_SCALE : 0.0f;
        float locL_y = s_baseAnkleHeightL + liftL;

        float liftR = (ikSample.rightPos.y > 0.0f) ? ikSample.rightPos.y * IK_SCALE : 0.0f;
        float locR_y = s_baseAnkleHeightR + liftR;

        float locL_x = -(PMX_REST_L_X + (ikSample.leftPos.x - s_firstCenterDisp.x)) * IK_SCALE;
        float locL_z = -(ikSample.leftPos.z - s_firstCenterDisp.z) * IK_SCALE;

        float locR_x = -(PMX_REST_R_X + (ikSample.rightPos.x - s_firstCenterDisp.x)) * IK_SCALE;
        float locR_z = -(ikSample.rightPos.z - s_firstCenterDisp.z) * IK_SCALE;

        s_curFootLiftL = liftL;
        s_curFootLiftR = liftR;
        s_curFootIKRotL[0] = ikSample.leftRot.x;
        s_curFootIKRotL[1] = ikSample.leftRot.y;
        s_curFootIKRotL[2] = ikSample.leftRot.z;
        s_curFootIKRotL[3] = ikSample.leftRot.w;
        s_curFootIKRotR[0] = ikSample.rightRot.x;
        s_curFootIKRotR[1] = ikSample.rightRot.y;
        s_curFootIKRotR[2] = ikSample.rightRot.z;
        s_curFootIKRotR[3] = ikSample.rightRot.w;

        float qx = g_initialRootQuat[0], qy = g_initialRootQuat[1];
        float qz = g_initialRootQuat[2], qw = g_initialRootQuat[3];

        float txL = 2.0f * (qy * locL_z);
        float tyL = 2.0f * (qz * locL_x - qx * locL_z);
        float tzL = 2.0f * (-qy * locL_x);
        g_ikDeltaLf[0] = locL_x + qw * txL + (qy * tzL);
        g_ikDeltaLf[1] = locL_y;
        g_ikDeltaLf[2] = locL_z + qw * tzL + (-qy * txL);

        float txR = 2.0f * (qy * locR_z);
        float tyR = 2.0f * (qz * locR_x - qx * locR_z);
        float tzR = 2.0f * (-qy * locR_x);
        g_ikDeltaRf[0] = locR_x + qw * txR + (qy * tzR);
        g_ikDeltaRf[1] = locR_y;
        g_ikDeltaRf[2] = locR_z + qw * tzR + (-qy * txR);
      }
    }
  }

  float motDx = 0, motDy = 0, motDz = 0;
  if (s_footIKFirstCaptured && s_vmdCenterSampled) {
    float cdx = s_curVmdCenter.x - s_firstCenterDisp.x;
    float cdy = s_curVmdCenter.y - s_firstCenterDisp.y;
    float cdz = s_curVmdCenter.z - s_firstCenterDisp.z;
    motDx = -cdx * 0.08f * g_motionScale;
    motDy =  cdy * 0.08f * g_motionScale;
    motDz = -cdz * 0.08f * g_motionScale;
  } else {
    motDx = (g_mmdBodyPos[0] - s_mmdFirstBodyPos[0]) * g_motionScale;
    motDy = (g_mmdBodyPos[1] - s_mmdFirstBodyPos[1]) * g_motionScale;
    motDz = (g_mmdBodyPos[2] - s_mmdFirstBodyPos[2]) * g_motionScale;
  }
  s_bodyMotDelta[0] = motDx;
  s_bodyMotDelta[1] = motDy;
  s_bodyMotDelta[2] = motDz;

  float worldDx = 0.0f, worldDz = 0.0f;
  if (s_initialRootCaptured) {
    float qx = g_initialRootQuat[0], qy = g_initialRootQuat[1];
    float qz = g_initialRootQuat[2], qw = g_initialRootQuat[3];
    float tx = 2.0f * (qy * motDz);
    float ty = 2.0f * (qz * motDx - qx * motDz);
    float tz = 2.0f * (-qy * motDx);
    worldDx = motDx + qw * tx + (qy * tz);
    worldDz = motDz + qw * tz + (-qy * tx);
  }

  if (g_footIKEnabled) {
    if (g_origSetPos && s_initialRootCaptured && g_cachedAnimator) {
      void *animTransform = Invoke(g_component_get_transform, g_cachedAnimator);
      if (animTransform) {
        float newRootPos[3] = {
          s_initialRootPos[0] + worldDx,
          s_initialRootPos[1] + g_groundDeltaY,
          s_initialRootPos[2] + worldDz
        };
        g_origSetPos(animTransform, newRootPos);
      }
    }
    s_cachedPose.bodyPosX = g_restBodyPos[0] + g_posOffsetX;
    s_cachedPose.bodyPosY = g_restBodyPos[1] + motDy + g_posOffsetY;
    s_cachedPose.bodyPosZ = g_restBodyPos[2] + g_posOffsetZ;
  } else {
    if (g_origSetPos && s_initialRootCaptured && g_cachedAnimator) {
      void *animTransform = Invoke(g_component_get_transform, g_cachedAnimator);
      if (animTransform) {
        g_origSetPos(animTransform, s_initialRootPos);
      }
    }
    float motDx_m = (g_mmdBodyPos[0] - s_mmdFirstBodyPos[0]) * g_motionScale;
    float motDy_m = (g_mmdBodyPos[1] - s_mmdFirstBodyPos[1]) * g_motionScale;
    float motDz_m = (g_mmdBodyPos[2] - s_mmdFirstBodyPos[2]) * g_motionScale;
    s_cachedPose.bodyPosX = g_restBodyPos[0] + motDx_m + g_posOffsetX;
    s_cachedPose.bodyPosY = g_restBodyPos[1] + motDy_m + g_posOffsetY;
    s_cachedPose.bodyPosZ = g_restBodyPos[2] + motDz_m + g_posOffsetZ;
  }

  s_cachedPose.bodyRotX = g_mmdBodyRot[0];
  s_cachedPose.bodyRotY = g_mmdBodyRot[1];
  s_cachedPose.bodyRotZ = g_mmdBodyRot[2];
  s_cachedPose.bodyRotW = g_mmdBodyRot[3];

  if (g_yawOffsetDeg != 0.0f) {
    float yawRad = g_yawOffsetDeg * 3.14159265f / 180.0f;
    float halfYaw = yawRad * 0.5f;
    float yqy = sinf(halfYaw), yqw = cosf(halfYaw);
    float bx = s_cachedPose.bodyRotX, by = s_cachedPose.bodyRotY;
    float bz = s_cachedPose.bodyRotZ, bw = s_cachedPose.bodyRotW;
    s_cachedPose.bodyRotX = yqw*bx + yqy*bz;
    s_cachedPose.bodyRotY = yqw*by + yqy*bw;
    s_cachedPose.bodyRotZ = yqw*bz - yqy*bx;
    s_cachedPose.bodyRotW = yqw*bw - yqy*by;
  }

  float qlen = sqrtf(s_cachedPose.bodyRotX * s_cachedPose.bodyRotX +
                     s_cachedPose.bodyRotY * s_cachedPose.bodyRotY +
                     s_cachedPose.bodyRotZ * s_cachedPose.bodyRotZ +
                     s_cachedPose.bodyRotW * s_cachedPose.bodyRotW);
  if (qlen > 0.001f) {
    s_cachedPose.bodyRotX /= qlen;
    s_cachedPose.bodyRotY /= qlen;
    s_cachedPose.bodyRotZ /= qlen;
    s_cachedPose.bodyRotW /= qlen;
  }

  *g_slotAddr = g_slotSetFn;
  void *setArgs[] = {&s_cachedPose};
  void *setExc = nullptr;
  il2cpp_runtime_invoke(g_humanPoseHandler_GetHumanPose, managedObj, setArgs,
                        &setExc);
  *g_slotAddr = g_slotOrigGet;


  if (g_trojanActive && g_cachedAnimator) {
    void *leftFootT = SafeGetBoneTransform(5);
    void *rightFootT = SafeGetBoneTransform(6);

    float qBody[4] = {s_cachedPose.bodyRotX, s_cachedPose.bodyRotY, s_cachedPose.bodyRotZ, s_cachedPose.bodyRotW};
    float qCharWorld[4] = {0, 0, 0, 1};
    if (s_initialRootCaptured) {
      QuatMultiply(g_initialRootQuat, qBody, qCharWorld);
    }

    float yawRad = atan2f(2.0f * (qCharWorld[1] * qCharWorld[3] + qCharWorld[0] * qCharWorld[2]),
                          1.0f - 2.0f * (qCharWorld[1] * qCharWorld[1] + qCharWorld[2] * qCharWorld[2]));
    float halfYaw = yawRad * 0.5f;
    float qHeading[4] = {0.0f, sinf(halfYaw), 0.0f, cosf(halfYaw)};

    const float qBoneRig[4] = {-0.5f, -0.5f, -0.5f, -0.5f};

    float qvmdL[4] = {-s_curFootIKRotL[0], s_curFootIKRotL[1], -s_curFootIKRotL[2], s_curFootIKRotL[3]};
    float qvmdR[4] = {-s_curFootIKRotR[0], s_curFootIKRotR[1], -s_curFootIKRotR[2], s_curFootIKRotR[3]};

    float qModelL[4] = {};
    float qModelR[4] = {};
    QuatMultiply(qvmdL, qBoneRig, qModelL);
    QuatMultiply(qvmdR, qBoneRig, qModelR);

    QuatMultiply(qHeading, qModelL, s_curFootRotTargetL);
    QuatMultiply(qHeading, qModelR, s_curFootRotTargetR);

    float lenL = sqrtf(s_curFootRotTargetL[0]*s_curFootRotTargetL[0] + s_curFootRotTargetL[1]*s_curFootRotTargetL[1] +
                       s_curFootRotTargetL[2]*s_curFootRotTargetL[2] + s_curFootRotTargetL[3]*s_curFootRotTargetL[3]);
    if (lenL > 0.0001f) {
      s_curFootRotTargetL[0] /= lenL; s_curFootRotTargetL[1] /= lenL;
      s_curFootRotTargetL[2] /= lenL; s_curFootRotTargetL[3] /= lenL;
    }

    float lenR = sqrtf(s_curFootRotTargetR[0]*s_curFootRotTargetR[0] + s_curFootRotTargetR[1]*s_curFootRotTargetR[1] +
                       s_curFootRotTargetR[2]*s_curFootRotTargetR[2] + s_curFootRotTargetR[3]*s_curFootRotTargetR[3]);
    if (lenR > 0.0001f) {
      s_curFootRotTargetR[0] /= lenR; s_curFootRotTargetR[1] /= lenR;
      s_curFootRotTargetR[2] /= lenR; s_curFootRotTargetR[3] /= lenR;
    }

    float rawLenL = sqrtf(s_curFootIKRotL[0]*s_curFootIKRotL[0] + s_curFootIKRotL[1]*s_curFootIKRotL[1] +
                          s_curFootIKRotL[2]*s_curFootIKRotL[2] + s_curFootIKRotL[3]*s_curFootIKRotL[3]);
    if (rawLenL > 0.0001f) {
      s_curFootIKRotL[0] /= rawLenL; s_curFootIKRotL[1] /= rawLenL;
      s_curFootIKRotL[2] /= rawLenL; s_curFootIKRotL[3] /= rawLenL;
    }

    float rawLenR = sqrtf(s_curFootIKRotR[0]*s_curFootIKRotR[0] + s_curFootIKRotR[1]*s_curFootIKRotR[1] +
                          s_curFootIKRotR[2]*s_curFootIKRotR[2] + s_curFootIKRotR[3]*s_curFootIKRotR[3]);
    if (rawLenR > 0.0001f) {
      s_curFootIKRotR[0] /= rawLenR; s_curFootIKRotR[1] /= rawLenR;
      s_curFootIKRotR[2] /= rawLenR; s_curFootIKRotR[3] /= rawLenR;
    }

    float rotDevL = sqrtf(s_curFootIKRotL[0]*s_curFootIKRotL[0] + s_curFootIKRotL[1]*s_curFootIKRotL[1] + s_curFootIKRotL[2]*s_curFootIKRotL[2]);
    float explicitFactorL = FootIKSmoothStep(0.01f, 0.05f, rotDevL);

    float rotDevR = sqrtf(s_curFootIKRotR[0]*s_curFootIKRotR[0] + s_curFootIKRotR[1]*s_curFootIKRotR[1] + s_curFootIKRotR[2]*s_curFootIKRotR[2]);
    float explicitFactorR = FootIKSmoothStep(0.01f, 0.05f, rotDevR);

    float airFadeL = FootIKClamp01(1.0f - (s_curFootLiftL - 0.03f) / 0.12f);
    float airFadeR = FootIKClamp01(1.0f - (s_curFootLiftR - 0.03f) / 0.12f);

    s_curFootRotWeightL = explicitFactorL + (1.0f - explicitFactorL) * airFadeL;
    s_curFootRotWeightR = explicitFactorR + (1.0f - explicitFactorR) * airFadeR;

    Vec3 curLfPos = {}, curRfPos = {};
    bool haveFootBones = false;
    if (leftFootT && rightFootT &&
        ReadWorldPosition(leftFootT, curLfPos) &&
        ReadWorldPosition(rightFootT, curRfPos)) {
      haveFootBones = true;
    }

    if (!s_legLengthCaptured && haveFootBones) {
      void *lThighT = SafeGetBoneTransform(1);
      void *rThighT = SafeGetBoneTransform(2);
      void *lCalfT  = SafeGetBoneTransform(3);
      void *rCalfT  = SafeGetBoneTransform(4);

      if (lThighT && lCalfT && rThighT && rCalfT) {
        Vec3 pThighL = {}, pCalfL = {}, pFootL = curLfPos;
        Vec3 pThighR = {}, pCalfR = {}, pFootR = curRfPos;
        if (ReadWorldPosition(lThighT, pThighL) && ReadWorldPosition(lCalfT, pCalfL) &&
            ReadWorldPosition(rThighT, pThighR) && ReadWorldPosition(rCalfT, pCalfR)) {
          float l1_L = sqrtf((pCalfL.x-pThighL.x)*(pCalfL.x-pThighL.x) + (pCalfL.y-pThighL.y)*(pCalfL.y-pThighL.y) + (pCalfL.z-pThighL.z)*(pCalfL.z-pThighL.z));
          float l2_L = sqrtf((pFootL.x-pCalfL.x)*(pFootL.x-pCalfL.x) + (pFootL.y-pCalfL.y)*(pFootL.y-pCalfL.y) + (pFootL.z-pCalfL.z)*(pFootL.z-pCalfL.z));
          float totalL = l1_L + l2_L;

          float l1_R = sqrtf((pCalfR.x-pThighR.x)*(pCalfR.x-pThighR.x) + (pCalfR.y-pThighR.y)*(pCalfR.y-pThighR.y) + (pCalfR.z-pThighR.z)*(pCalfR.z-pThighR.z));
          float l2_R = sqrtf((pFootR.x-pCalfR.x)*(pFootR.x-pCalfR.x) + (pFootR.y-pCalfR.y)*(pFootR.y-pCalfR.y) + (pFootR.z-pCalfR.z)*(pFootR.z-pCalfR.z));
          float totalR = l1_R + l2_R;

          if (l1_L > 0.15f && l2_L > 0.15f && totalL > 0.30f && totalL < 2.0f &&
              l1_R > 0.15f && l2_R > 0.15f && totalR > 0.30f && totalR < 2.0f) {
            s_legMaxL = totalL;
            s_legMaxR = totalR;
            s_legLengthCaptured = true;

            s_baseAnkleHeightL = FOOT_ANKLE_REF_HEIGHT * (s_legMaxL / 0.88f);
            s_baseAnkleHeightR = FOOT_ANKLE_REF_HEIGHT * (s_legMaxR / 0.88f);
            s_ankleHeightCaptured = true;

            Log("[FOOT-IK] Dynamic leg reach captured: L=%.3f (l1=%.3f, l2=%.3f) R=%.3f (l1=%.3f, l2=%.3f) -> baseAnkle: L=%.3f R=%.3f",
                s_legMaxL, l1_L, l2_L, s_legMaxR, l1_R, l2_R, s_baseAnkleHeightL, s_baseAnkleHeightR);
          }
        }
      }
    }

    float footX_L = s_initialRootPos[0] + g_ikDeltaLf[0];
    float footZ_L = s_initialRootPos[2] + g_ikDeltaLf[2];
    float footX_R = s_initialRootPos[0] + g_ikDeltaRf[0];
    float footZ_R = s_initialRootPos[2] + g_ikDeltaRf[2];

    if (g_findFloorMethod && s_cachedMovementComp &&
        g_camGetPos && g_camGetRot && g_component_get_transform) {
      if (!s_findFloorNativeFn) {
        s_findFloorNativeFn = (FindFloorNative_t)((MInfo *)g_findFloorMethod)->mp;
        Log("[GF2] FindFloor native ptr: %p", s_findFloorNativeFn);
      }

      if (s_findFloorNativeFn) {
        __try {
          void *animTransform = Invoke(g_component_get_transform, g_cachedAnimator);
          if (animTransform) {
            float rootPos[3], rootRot[4];
            g_camGetPos(animTransform, rootPos);
            g_camGetRot(animTransform, rootRot);

            float bx = s_cachedPose.bodyPosX;
            float by = s_cachedPose.bodyPosY;
            float bz = s_cachedPose.bodyPosZ;
            float qx = rootRot[0], qy = rootRot[1], qz = rootRot[2], qw = rootRot[3];
            float tx = 2.0f * (qy*bz - qz*by);
            float ty = 2.0f * (qz*bx - qx*bz);
            float tz = 2.0f * (qx*by - qy*bx);
            float wBx = bx + qw*tx + (qy*tz - qz*ty);
            float wBy = by + qw*ty + (qz*tx - qx*tz);
            float wBz = bz + qw*tz + (qx*ty - qy*tx);

            float hipsX = rootPos[0] + wBx;
            float hipsY = rootPos[1] + wBy;
            float hipsZ = rootPos[2] + wBz;

            bool hitL = false;
            float rawDeltaL = 0.0f;
            if (s_footIKCalibrated) {
              float qposL[3] = {footX_L, hipsY + 2.0f, footZ_L};
              alignas(16) char ffrL[128] = {};
              s_findFloorNativeFn(s_cachedMovementComp, qposL, ffrL, 20.0f, g_findFloorMethod);
              if (*(bool *)(ffrL + 0x00)) {
                float gndY_L = *(float *)(ffrL + 0x10 + 0x04);
                if (!s_lfBaseCaptured) {
                  s_lfBaseCaptured = true;
                  s_lfBaseGround = gndY_L;
                }
                rawDeltaL = gndY_L - s_lfBaseGround;
                hitL = true;
              }
            }

            bool hitR = false;
            float rawDeltaR = 0.0f;
            if (s_footIKCalibrated) {
              float qposR[3] = {footX_R, hipsY + 2.0f, footZ_R};
              alignas(16) char ffrR[128] = {};
              s_findFloorNativeFn(s_cachedMovementComp, qposR, ffrR, 20.0f, g_findFloorMethod);
              if (*(bool *)(ffrR + 0x00)) {
                float gndY_R = *(float *)(ffrR + 0x10 + 0x04);
                if (!s_rfBaseCaptured) {
                  s_rfBaseCaptured = true;
                  s_rfBaseGround = gndY_R;
                }
                rawDeltaR = gndY_R - s_rfBaseGround;
                hitR = true;
              }
            }

            bool hitHips = false;
            float dHips = 0.0f;
            {
              float queryPos[3] = {hipsX, hipsY + 2.0f, hipsZ};
              alignas(16) char floorResult[128] = {};
              s_findFloorNativeFn(s_cachedMovementComp, queryPos, floorResult, 20.0f, g_findFloorMethod);
              if (*(bool *)(floorResult + 0x00)) {
                float groundY = *(float *)(floorResult + 0x10 + 0x04);
                if (!s_baseGroundCaptured) {
                  s_baseGroundCaptured = true;
                  s_baseGroundY = groundY;
                  Log("[GF2] Base groundY=%.3f hips=(%.2f,%.2f,%.2f)",
                      s_baseGroundY, hipsX, hipsY, hipsZ);
                }
                dHips = groundY - s_baseGroundY;
                hitHips = true;
              }
            }

            if (!hitL) rawDeltaL = hitHips ? dHips : s_smoothedGroundDelta;
            if (!hitR) rawDeltaR = hitHips ? dHips : s_smoothedGroundDelta;

            s_curFootGroundDeltaL = rawDeltaL;
            s_curFootGroundDeltaR = rawDeltaR;

            float wL = FootIKClamp01(1.0f - (s_curFootLiftL / 0.08f));
            float wR = FootIKClamp01(1.0f - (s_curFootLiftR / 0.08f));
            float validWL = hitL ? wL : 0.0f;
            float validWR = hitR ? wR : 0.0f;
            float totalW = validWL + validWR;

            float baseDelta = hitHips ? dHips : s_smoothedGroundDelta;
            float targetDelta = baseDelta;

            if (totalW > 0.0001f) {
              float weightedAvg = (validWL * rawDeltaL + validWR * rawDeltaR) / totalW;
              float footSolution = weightedAvg;

              if (hitL && hitR) {
                float minSupport = fminf(validWL, validWR);
                float dualFactor = FootIKSmoothStep(0.60f, 0.95f, minSupport);
                float minDelta = fminf(rawDeltaL, rawDeltaR);
                footSolution = FootIKLerp(weightedAvg, minDelta, dualFactor);
              }

              float supportStrength = FootIKSmoothStep(0.10f, 0.70f, fmaxf(validWL, validWR));
              targetDelta = FootIKLerp(baseDelta, footSolution, supportStrength);
            }

            static LARGE_INTEGER s_lastGroundQPC = {};
            LARGE_INTEGER qpcNow, qpcFreq;
            QueryPerformanceCounter(&qpcNow);
            QueryPerformanceFrequency(&qpcFreq);
            float dt = 0.0166f;
            if (s_lastGroundQPC.QuadPart != 0 && qpcFreq.QuadPart != 0) {
              dt = (float)((double)(qpcNow.QuadPart - s_lastGroundQPC.QuadPart) / (double)qpcFreq.QuadPart);
              if (dt <= 0.0f || dt > 0.05f) dt = 0.0166f;
            }
            s_lastGroundQPC = qpcNow;

            if (!s_groundSmoothingInitialized) {
              s_smoothedGroundDelta = targetDelta;
              s_smoothedFootGroundDeltaL = rawDeltaL;
              s_smoothedFootGroundDeltaR = rawDeltaR;
              s_groundSmoothingInitialized = true;
            } else {
              float alphaRoot = 1.0f - expf(-dt / GROUND_SMOOTH_TAU_ROOT);
              float alphaFoot = 1.0f - expf(-dt / GROUND_SMOOTH_TAU_FOOT);
              s_smoothedGroundDelta += (targetDelta - s_smoothedGroundDelta) * alphaRoot;
              s_smoothedFootGroundDeltaL += (rawDeltaL - s_smoothedFootGroundDeltaL) * alphaFoot;
              s_smoothedFootGroundDeltaR += (rawDeltaR - s_smoothedFootGroundDeltaR) * alphaFoot;
            }
            g_groundDeltaY = s_smoothedGroundDelta;
            s_curFootGroundDeltaL = s_smoothedFootGroundDeltaL;
            s_curFootGroundDeltaR = s_smoothedFootGroundDeltaR;

            if (g_origSetPos && s_initialRootCaptured && g_cachedAnimator && g_footIKEnabled) {
              void *animTransform = Invoke(g_component_get_transform, g_cachedAnimator);
              if (animTransform) {
                float newRootPos[3] = {
                  s_initialRootPos[0] + worldDx,
                  s_initialRootPos[1] + g_groundDeltaY,
                  s_initialRootPos[2] + worldDz
                };
                g_origSetPos(animTransform, newRootPos);
              }
            }

            static int s_gfLog = 0;
            if (s_gfLog++ % 60 == 0) {
              Log("[GF2] f=%d haveBones=%d curLf=(%.2f,%.2f,%.2f) curRf=(%.2f,%.2f,%.2f) targetD=%.3f smoothD=%.3f dL=%.3f dR=%.3f wL=%.2f wR=%.2f rootY=%.2f",
                  s_gfLog, (int)haveFootBones, curLfPos.x, curLfPos.y, curLfPos.z, curRfPos.x, curRfPos.y, curRfPos.z,
                  targetDelta, g_groundDeltaY, s_curFootGroundDeltaL, s_curFootGroundDeltaR, validWL, validWR, rootPos[1]);
            }
          }
        } __except(1) {
          static bool s_sehLogged = false;
          if (!s_sehLogged) { s_sehLogged = true; Log("[GF2] FindFloor native SEH!"); }
        }
      }
    }

    s_desiredFootTargetL[0] = footX_L;
    s_desiredFootTargetL[1] = s_initialRootPos[1] + g_ikDeltaLf[1] + s_curFootGroundDeltaL;
    s_desiredFootTargetL[2] = footZ_L;

    s_desiredFootTargetR[0] = footX_R;
    s_desiredFootTargetR[1] = s_initialRootPos[1] + g_ikDeltaRf[1] + s_curFootGroundDeltaR;
    s_desiredFootTargetR[2] = footZ_R;

    void *lThighT = SafeGetBoneTransform(1);
    void *rThighT = SafeGetBoneTransform(2);
    Vec3 hipPosL = {}, hipPosR = {};
    bool haveThighL = (lThighT && ReadWorldPosition(lThighT, hipPosL));
    bool haveThighR = (rThighT && ReadWorldPosition(rThighT, hipPosR));

    float airFactorL = FootIKSmoothStep(0.02f, 0.06f, s_curFootLiftL);
    float daGroundL = s_legMaxL * 0.006f;
    if (daGroundL < 0.004f) daGroundL = 0.004f;
    if (daGroundL > 0.008f) daGroundL = 0.008f;

    float daAirL = s_legMaxL * 0.035f;
    if (daAirL < 0.020f) daAirL = 0.020f;
    if (daAirL > 0.035f) daAirL = 0.035f;

    float daL = FootIKLerp(daGroundL, daAirL, airFactorL);

    if (haveThighL && s_legLengthCaptured && s_legMaxL > 0.30f) {
      Vec3 v = {
        s_desiredFootTargetL[0] - hipPosL.x,
        s_desiredFootTargetL[1] - hipPosL.y,
        s_desiredFootTargetL[2] - hipPosL.z
      };
      float d = sqrtf(v.x*v.x + v.y*v.y + v.z*v.z);
      float dSoft = s_legMaxL - daL;

      if (d > dSoft && d > 0.0001f) {
        float dEff = dSoft + daL * (1.0f - expf(-(d - dSoft) / daL));
        float scale = dEff / d;
        s_curFootTargetL[0] = hipPosL.x + v.x * scale;
        s_curFootTargetL[1] = hipPosL.y + v.y * scale;
        s_curFootTargetL[2] = hipPosL.z + v.z * scale;
      } else {
        s_curFootTargetL[0] = s_desiredFootTargetL[0];
        s_curFootTargetL[1] = s_desiredFootTargetL[1];
        s_curFootTargetL[2] = s_desiredFootTargetL[2];
      }
    } else {
      s_curFootTargetL[0] = s_desiredFootTargetL[0];
      s_curFootTargetL[1] = s_desiredFootTargetL[1];
      s_curFootTargetL[2] = s_desiredFootTargetL[2];
    }

    float airFactorR = FootIKSmoothStep(0.02f, 0.06f, s_curFootLiftR);
    float daGroundR = s_legMaxR * 0.006f;
    if (daGroundR < 0.004f) daGroundR = 0.004f;
    if (daGroundR > 0.008f) daGroundR = 0.008f;

    float daAirR = s_legMaxR * 0.035f;
    if (daAirR < 0.020f) daAirR = 0.020f;
    if (daAirR > 0.035f) daAirR = 0.035f;

    float daR = FootIKLerp(daGroundR, daAirR, airFactorR);

    if (haveThighR && s_legLengthCaptured && s_legMaxR > 0.30f) {
      Vec3 v = {
        s_desiredFootTargetR[0] - hipPosR.x,
        s_desiredFootTargetR[1] - hipPosR.y,
        s_desiredFootTargetR[2] - hipPosR.z
      };
      float d = sqrtf(v.x*v.x + v.y*v.y + v.z*v.z);
      float dSoft = s_legMaxR - daR;

      if (d > dSoft && d > 0.0001f) {
        float dEff = dSoft + daR * (1.0f - expf(-(d - dSoft) / daR));
        float scale = dEff / d;
        s_curFootTargetR[0] = hipPosR.x + v.x * scale;
        s_curFootTargetR[1] = hipPosR.y + v.y * scale;
        s_curFootTargetR[2] = hipPosR.z + v.z * scale;
      } else {
        s_curFootTargetR[0] = s_desiredFootTargetR[0];
        s_curFootTargetR[1] = s_desiredFootTargetR[1];
        s_curFootTargetR[2] = s_desiredFootTargetR[2];
      }
    } else {
      s_curFootTargetR[0] = s_desiredFootTargetR[0];
      s_curFootTargetR[1] = s_desiredFootTargetR[1];
      s_curFootTargetR[2] = s_desiredFootTargetR[2];
    }

    static int s_softIKLog = 0;
    if (s_softIKLog++ % 60 == 0 && haveThighL) {
      float dL = sqrtf((s_desiredFootTargetL[0]-hipPosL.x)*(s_desiredFootTargetL[0]-hipPosL.x) +
                       (s_desiredFootTargetL[1]-hipPosL.y)*(s_desiredFootTargetL[1]-hipPosL.y) +
                       (s_desiredFootTargetL[2]-hipPosL.z)*(s_desiredFootTargetL[2]-hipPosL.z));
      float overreachL = (dL > s_legMaxL) ? (dL - s_legMaxL) : 0.0f;
      float corrL = sqrtf((s_desiredFootTargetL[0]-s_curFootTargetL[0])*(s_desiredFootTargetL[0]-s_curFootTargetL[0]) +
                          (s_desiredFootTargetL[1]-s_curFootTargetL[1])*(s_desiredFootTargetL[1]-s_curFootTargetL[1]) +
                          (s_desiredFootTargetL[2]-s_curFootTargetL[2])*(s_desiredFootTargetL[2]-s_curFootTargetL[2]));
      Log("[SOFT-IK] L: lift=%.3f air=%.2f da=%.4f hipD=%.3f legMax=%.3f over=%.3f corr=%.3f | des=(%.2f,%.2f,%.2f) cur=(%.2f,%.2f,%.2f)",
          s_curFootLiftL, airFactorL, daL, dL, s_legMaxL, overreachL, corrL,
          s_desiredFootTargetL[0], s_desiredFootTargetL[1], s_desiredFootTargetL[2],
          s_curFootTargetL[0], s_curFootTargetL[1], s_curFootTargetL[2]);
    }

    if (g_footIKEnabled && s_footIKCalibrated && s_bipedIKCount > 0 &&
        g_finalIkSolverLayoutOk) {
      void *bipedIK = s_bipedIK[0];
      if (bipedIK) {
        void *solvers = *(void **)((char *)bipedIK + OFF_BIPEDIK_SOLVERS);
        if (solvers) {
          void *lfSolver = *(void **)((char *)solvers + OFF_SOLVERS_LEFT_FOOT);
          void *rfSolver = *(void **)((char *)solvers + OFF_SOLVERS_RIGHT_FOOT);

          if (lfSolver) {
            *(void **)((char *)lfSolver + OFF_IKSOLVER_ON_PRE_UPDATE) = nullptr;
            *(void **)((char *)lfSolver + OFF_IKSOLVER_ON_POST_UPDATE) = nullptr;
            *(void **)((char *)lfSolver + OFF_IKTRIG_TARGET) = nullptr;
            *(int *)((char *)lfSolver + OFF_IKLIMB_BEND_MODIFIER) = 0;
            *(float *)((char *)lfSolver + OFF_IKLIMB_BEND_WEIGHT) = 1.0f;
          }
          if (rfSolver) {
            *(void **)((char *)rfSolver + OFF_IKSOLVER_ON_PRE_UPDATE) = nullptr;
            *(void **)((char *)rfSolver + OFF_IKSOLVER_ON_POST_UPDATE) = nullptr;
            *(void **)((char *)rfSolver + OFF_IKTRIG_TARGET) = nullptr;
            *(int *)((char *)rfSolver + OFF_IKLIMB_BEND_MODIFIER) = 0;
            *(float *)((char *)rfSolver + OFF_IKLIMB_BEND_WEIGHT) = 1.0f;
          }

          *(bool *)((char *)bipedIK + 0x30) = true;
          *(bool *)((char *)bipedIK + 0x32) = false;
          *(void **)((char *)bipedIK + 0x20) = nullptr;

          g_activeLfSolver = lfSolver;
          g_activeRfSolver = rfSolver;
          g_mmdIKActive = true;
        }
      }
    } else {
      g_mmdIKActive = false;
      __try {
        if (g_activeLfSolver) {
          *(float *)((char *)g_activeLfSolver + OFF_IKSOLVER_IKPOS_WEIGHT) = 0.0f;
          *(float *)((char *)g_activeLfSolver + OFF_IKSOLVER_IKROT_WEIGHT) = 0.0f;
        }
        if (g_activeRfSolver) {
          *(float *)((char *)g_activeRfSolver + OFF_IKSOLVER_IKPOS_WEIGHT) = 0.0f;
          *(float *)((char *)g_activeRfSolver + OFF_IKSOLVER_IKROT_WEIGHT) = 0.0f;
        }
      } __except (1) {}
    }
  }


  if (!s_ikDisabled && g_cachedAnimator) {
    s_ikDisabled = true;

      void *animatorGO = nullptr;
      __try {
        animatorGO = Invoke(g_component_get_gameObject, g_cachedAnimator);
      } __except (1) {
      }

      if (animatorGO) {
        void *rootTransform = SafeGetComponentTransform(g_cachedAnimator);
        if (rootTransform) {

          struct WalkEntry {
            void *t;
            int d;
          };
          WalkEntry stack[64];
          int top = 0;
          stack[top++] = {rootTransform, 0};

          while (top > 0) {
            WalkEntry e = stack[--top];

            void *go = nullptr;
            __try {
              go = Invoke(g_component_get_gameObject, e.t);
            } __except (1) {
            }

            if (go) {
              void *componentClass = nullptr;
              void *componentType = nullptr;
              {
                void *domain = il2cpp_domain_get();
                size_t ac;
                void **asms = il2cpp_domain_get_assemblies(domain, &ac);
                componentClass =
                    FindClass("UnityEngine", "Component", asms, ac);
              }
              if (componentClass)
                componentType = il2cpp_class_get_type(componentClass);

              if (componentType) {
                void *getCompMethod =
                    FindMethod(il2cpp_object_get_class(go), "GetComponents", 1);
                void *typeObj = nullptr;
                __try {
                  typeObj = il2cpp_type_get_object(componentType);
                } __except (1) {
                }

                if (getCompMethod && typeObj) {
                  void *exc = nullptr;
                  void *args[] = {typeObj};
                  void *arr = nullptr;
                  __try {
                    arr = il2cpp_runtime_invoke(getCompMethod, go, args, &exc);
                  } __except (1) {
                  }

                  if (arr && !exc) {
                    int cnt = *(int *)((char *)arr + 24);
                    void **data = (void **)((char *)arr + 32);

                    for (int i = 0; i < cnt; i++) {
                      if (!data[i])
                        continue;
                      void *cls = il2cpp_object_get_class(data[i]);
                      const char *clsName =
                          cls ? il2cpp_class_get_name(cls) : "";

                      if (strcmp(clsName, "BipedIK") == 0 &&
                          s_bipedIKCount < MAX_IK) {
                        char ikGoName[64] = "?";
                        __try {
                          if (g_object_get_name) {
                            void *ns = Invoke(g_object_get_name, go);
                            if (ns) ReadStrUtf8(ns, ikGoName, sizeof(ikGoName));
                          }
                        } __except (1) {}
                        s_bipedIK[s_bipedIKCount++] = data[i];
                        Log("[IK-DISABLE] Found BipedIK #%d: %p GO='%s'",
                            s_bipedIKCount, data[i], ikGoName);
                      }
                      if (strcmp(clsName, "LookAtComponent") == 0 &&
                          s_lookAtCount < MAX_IK) {
                        char laGoName[64] = "?";
                        __try {
                          if (g_object_get_name) {
                            void *ns = Invoke(g_object_get_name, go);
                            if (ns) ReadStrUtf8(ns, laGoName, sizeof(laGoName));
                          }
                        } __except (1) {}
                        s_lookAt[s_lookAtCount++] = data[i];
                        Log("[IK-DISABLE] Found LookAtComponent #%d: %p GO='%s'",
                            s_lookAtCount, data[i], laGoName);
                      }
                      if (strcmp(clsName, "GrounderBipedIK") == 0 &&
                          s_grounderIKCount < MAX_IK) {
                        char gkGoName[64] = "?";
                        __try {
                          if (g_object_get_name) {
                            void *ns = Invoke(g_object_get_name, go);
                            if (ns) ReadStrUtf8(ns, gkGoName, sizeof(gkGoName));
                          }
                        } __except (1) {}
                        s_grounderIK[s_grounderIKCount++] = data[i];
                        Log("[IK-DISABLE] Found GrounderBipedIK #%d: %p GO='%s'",
                            s_grounderIKCount, data[i], gkGoName);
                      }
                      if (strcmp(clsName, "TransformFollowDamper") == 0 &&
                          s_followDamperCount < 4) {
                        char fdGoName[64] = "?";
                        __try {
                          if (g_object_get_name) {
                            void *ns = Invoke(g_object_get_name, go);
                            if (ns) ReadStrUtf8(ns, fdGoName, sizeof(fdGoName));
                          }
                        } __except (1) {}
                        s_followDamper[s_followDamperCount++] = data[i];
                        Log("[IK-DISABLE] Found TransformFollowDamper #%d: %p GO='%s'",
                            s_followDamperCount, data[i], fdGoName);
                      }
                      if (strcmp(clsName, "AnimatorMono") == 0) {
                        char amGoName[64] = "?";
                        __try {
                          if (g_object_get_name) {
                            void *ns = Invoke(g_object_get_name, go);
                            if (ns) ReadStrUtf8(ns, amGoName, sizeof(amGoName));
                          }
                        } __except (1) {}
                        s_animatorMono = data[i];
                        Log("[IK-DISABLE] Found AnimatorMono: %p GO='%s'",
                            s_animatorMono, amGoName);
                      }
                    }
                  }
                }
              }
            }

            if (e.d < 2) {
              __try {
                void *ccResult = Invoke(g_transform_get_childCount, e.t);
                int cc = ccResult ? *(int *)((char *)ccResult + 16) : 0;
                for (int c = 0; c < cc && top < 64; c++) {
                  void *cp[] = {&c};
                  void *child = Invoke(g_transform_GetChild, e.t, cp);
                  if (child)
                    stack[top++] = {child, e.d + 1};
                }
              } __except (EXCEPTION_EXECUTE_HANDLER) {
              }
            }
          }

          ConfigureIKComponents(g_footIKEnabled);

          for (int fd = 0; fd < s_followDamperCount; fd++) {
            Log("[IK-DISABLE] TransformFollowDamper #%d KEPT ENABLED (decoration)", fd + 1);
          }

          if (s_animatorMono && g_animator_set_enabled) {
            __try {
              int falseVal = 0;
              void *params[] = {&falseVal};
              Invoke(g_animator_set_enabled, s_animatorMono, params);
              Log("[IK-DISABLE] AnimatorMono DISABLED!");
            } __except (EXCEPTION_EXECUTE_HANDLER) {
            }
          }

          for (int la = 0; la < s_lookAtCount; la++) {
            if (s_lookAt[la] && g_animator_set_enabled) {
              __try {
                int falseVal = 0;
                void *params[] = {&falseVal};
                Invoke(g_animator_set_enabled, s_lookAt[la], params);
                Log("[IK-DISABLE] LookAtComponent #%d DISABLED!", la + 1);
              } __except (EXCEPTION_EXECUTE_HANDLER) {
                Log("[IK-DISABLE] Failed to disable LookAtComponent #%d", la + 1);
              }
            }
          }

          if (g_confirmedSMC && !s_eyeIKDisabled) {
            s_eyeIKDisabled = true;
            __try {
              int eyeOff = SafeOff(OFF_smcEyeLookAt, 0x1dd, "enableEyeLookAtIK");
              *(bool *)((char *)g_confirmedSMC + eyeOff) = false;
              Log("[IK-DISABLE] SMC EyeLookAtIK DISABLED (offset 0x%X=false)", eyeOff);
            } __except (EXCEPTION_EXECUTE_HANDLER) {
              Log("[IK-DISABLE] Failed to disable SMC EyeLookAtIK");
            }
          }

          if (s_bipedIKCount == 0)
            Log("[IK-DISABLE] WARNING: BipedIK not found!");
          else
            ConfigureIKComponents(g_footIKEnabled);

        }
      }
    }
  if (g_mmdHasFingerBones && g_muscleAnim && g_muscleAnim->hasFingerBones) {
    if (!g_fingerTransformsResolved) {
      g_fingerTransformsResolved = true;
      static const char *fingerNames[FINGER_BONE_COUNT] = {
          "Bip001_L_Finger0",  "Bip001_L_Finger01", "Bip001_L_Finger02",
          "Bip001_L_Finger1",  "Bip001_L_Finger11", "Bip001_L_Finger12",
          "Bip001_L_Finger2",  "Bip001_L_Finger21", "Bip001_L_Finger22",
          "Bip001_L_Finger3",  "Bip001_L_Finger31", "Bip001_L_Finger32",
          "Bip001_L_Finger4",  "Bip001_L_Finger41", "Bip001_L_Finger42",
          "Bip001_R_Finger0",  "Bip001_R_Finger01", "Bip001_R_Finger02",
          "Bip001_R_Finger1",  "Bip001_R_Finger11", "Bip001_R_Finger12",
          "Bip001_R_Finger2",  "Bip001_R_Finger21", "Bip001_R_Finger22",
          "Bip001_R_Finger3",  "Bip001_R_Finger31", "Bip001_R_Finger32",
          "Bip001_R_Finger4",  "Bip001_R_Finger41", "Bip001_R_Finger42",
      };
      void *rootT = SafeGetComponentTransform(g_cachedAnimator);
      int found = 0;
      if (rootT) {
        for (int i = 0; i < FINGER_BONE_COUNT; i++) {
          g_fingerTransforms[i] =
              SafeFindChildRecursive(rootT, fingerNames[i], 15);
          if (g_fingerTransforms[i])
            found++;
        }
      }
      Log("[FINGER-ANIM] Discovered %d/%d finger bones", found,
          FINGER_BONE_COUNT);
    }

    if (!g_fingerRestCaptured) {
      g_fingerRestCaptured = true;
      for (int i = 0; i < FINGER_BONE_COUNT; i++) {
        if (!g_fingerTransforms[i]) {
          g_gameFingerRest[i * 4 + 0] = 0;
          g_gameFingerRest[i * 4 + 1] = 0;
          g_gameFingerRest[i * 4 + 2] = 0;
          g_gameFingerRest[i * 4 + 3] = 1;
          continue;
        }
        Quat gr = SafeGetLocalRotation(g_fingerTransforms[i]);
        g_gameFingerRest[i * 4 + 0] = gr.x;
        g_gameFingerRest[i * 4 + 1] = gr.y;
        g_gameFingerRest[i * 4 + 2] = gr.z;
        g_gameFingerRest[i * 4 + 3] = gr.w;
      }
      Log("[FINGER-ANIM] Captured game finger rest rotations");
      for (int d = 0; d < 6 && d < FINGER_BONE_COUNT; d++) {
        float *gr = &g_gameFingerRest[d * 4];
        float *mr = &g_muscleAnim->fingerRestRots[d * 4];
        Log("[FINGER-DIAG] bone[%d] gameRest=(%.3f,%.3f,%.3f,%.3f) mmdRest=(%.3f,%.3f,%.3f,%.3f)",
            d, gr[0], gr[1], gr[2], gr[3], mr[0], mr[1], mr[2], mr[3]);
      }
    }

    static int s_fingerDiag = 0;
    for (int i = 0; i < FINGER_BONE_COUNT; i++) {
      if (!g_fingerTransforms[i])
        continue;
      float *mmdCur = (float *)&g_mmdFingerBoneRots[i * 4];
      float *mmdRest = &g_muscleAnim->fingerRestRots[i * 4];

      float cx = mmdCur[0], cy = mmdCur[1], cz = mmdCur[2], cw = mmdCur[3];
      float bx = mmdRest[0], by = mmdRest[1], bz = mmdRest[2], bw = mmdRest[3];
      if (i < 15) {
        cx = -cx; cz = -cz;
        bx = -bx; bz = -bz;
      }

      float ir[4] = {-bx, -by, -bz, bw};
      float dx = ir[3]*cx + ir[0]*cw + ir[1]*cz - ir[2]*cy;
      float dy = ir[3]*cy - ir[0]*cz + ir[1]*cw + ir[2]*cx;
      float dz = ir[3]*cz + ir[0]*cy - ir[1]*cx + ir[2]*cw;
      float dw = ir[3]*cw - ir[0]*cx - ir[1]*cy - ir[2]*cz;

      float t = g_scaleFingers;
      float dot = dw;
      if (dot < 0) { dx=-dx; dy=-dy; dz=-dz; dw=-dw; dot=-dot; }
      float s0, s1;
      if (dot > 0.9995f) {
        s0 = 1.0f - t; s1 = t;
      } else {
        float theta = acosf(dot);
        float sinT = sinf(theta);
        s0 = sinf((1.0f-t)*theta)/sinT;
        s1 = sinf(t*theta)/sinT;
      }
      float ax = s1*dx, ay = s1*dy, az = s1*dz, aw = s0 + s1*dw;

      float rx, ry, rz, rw;
      bool isProximal = (i % 3 == 0);

      if (isProximal) {

        float *gr = &g_gameFingerRest[i * 4];
        float gx = gr[3]*ax + gr[0]*aw + gr[1]*az - gr[2]*ay;
        float gy = gr[3]*ay - gr[0]*az + gr[1]*aw + gr[2]*ax;
        float gz = gr[3]*az + gr[0]*ay - gr[1]*ax + gr[2]*aw;
        float gw = gr[3]*aw - gr[0]*ax - gr[1]*ay - gr[2]*az;

        float mx = bw*ax + bx*aw + by*az - bz*ay;
        float my = bw*ay - bx*az + by*aw + bz*ax;
        float mz = bw*az + bx*ay - by*ax + bz*aw;
        float mw = bw*aw - bx*ax - by*ay - bz*az;

        float bd = gx*mx + gy*my + gz*mz + gw*mw;
        if (bd < 0) { mx=-mx; my=-my; mz=-mz; mw=-mw; bd=-bd; }
        float sb = g_splayBlend, sa = 1.0f - sb;
        if (bd > 0.9995f) {
          rx = sa*gx + sb*mx;
          ry = sa*gy + sb*my;
          rz = sa*gz + sb*mz;
          rw = sa*gw + sb*mw;
        } else {
          float th = acosf(bd);
          float sn = sinf(th);
          float c0 = sinf(sa*th)/sn, c1 = sinf(sb*th)/sn;
          rx = c0*gx + c1*mx;
          ry = c0*gy + c1*my;
          rz = c0*gz + c1*mz;
          rw = c0*gw + c1*mw;
        }
      } else {
        rx = bw*ax + bx*aw + by*az - bz*ay;
        ry = bw*ay - bx*az + by*aw + bz*ax;
        rz = bw*az + bx*ay - by*ax + bz*aw;
        rw = bw*aw - bx*ax - by*ay - bz*az;
      }

      float len = sqrtf(rx*rx + ry*ry + rz*rz + rw*rw);
      if (len > 0.001f) { rx/=len; ry/=len; rz/=len; rw/=len; }

      SafeSetLocalRotation(g_fingerTransforms[i], {rx, ry, rz, rw});
    }
    s_fingerDiag++;
  }


  static int s_cnt = 0;
  s_cnt++;
  if (s_cnt <= 3 || (s_cnt % 300 == 0 && s_cnt <= 3000)) {
    int g39 = StandardToGame(39), g48 = StandardToGame(48),
        g42 = StandardToGame(42);
    float dxPos = g_mmdBodyPos[0] - s_mmdFirstBodyPos[0];
    float dyPos = g_mmdBodyPos[1] - s_mmdFirstBodyPos[1];
    float dzPos = g_mmdBodyPos[2] - s_mmdFirstBodyPos[2];
    Log("[MMD] #%d: arm39→g%d=%.2f arm48→g%d=%.2f "
        "fore42→g%d=%.2f bodyD=(%.3f,%.3f,%.3f) exc=%s",
        s_cnt, g39, s_musclePtr[g39], g48, s_musclePtr[g48], g42,
        s_musclePtr[g42], dxPos, dyPos, dzPos, setExc ? "ERR" : "OK");
  }

  if (g_cameraNeedsCapture && g_cameraPlayer.HasData()) {
    g_cameraNeedsCapture = false;
    __try {
      void *charTransform = SafeGetComponentTransform(g_cachedAnimator);
      if (charTransform && g_camGetPos) {
        float pos[3] = {};
        g_camGetPos(charTransform, pos);
        g_charWorldPos = {pos[0], pos[1], pos[2]};
        if (g_camGetRot) {
          float q[4] = {};
          g_camGetRot(charTransform, q);
          g_charYaw = atan2f(2.0f * (q[3] * q[1] + q[0] * q[2]),
                             1.0f - 2.0f * (q[1] * q[1] + q[2] * q[2]));
        }
        Log("[CAM] Character world pos: (%.3f, %.3f, %.3f) yaw=%.1fdeg",
            g_charWorldPos.x, g_charWorldPos.y, g_charWorldPos.z,
            g_charYaw * 57.2958f);

        g_camInitHipsWorldPos = g_charWorldPos;
        g_camInitHipsYaw = 0.0f;
        if (g_muscleAnim && g_muscleAnim->loaded &&
            !g_muscleAnim->frames.empty()) {
          const float *br = g_muscleAnim->frames[0].bodyRot;
          float fx0 = 2.0f * (br[3] * br[1] + br[0] * br[2]);
          float fz0 = 1.0f - 2.0f * (br[0] * br[0] + br[1] * br[1]);
          g_camInitHipsYaw = atan2f(fx0, fz0);
          Log("[CAM] Initial bodyRot yaw (frame0): %.1fdeg",
              g_camInitHipsYaw * 57.2958f);
        }
        if (g_animator_GetBoneTransform && g_cachedAnimator) {
          void *hipsT = SafeGetBoneTransform(0);
          Vec3 hipsPos;
          if (hipsT && ReadWorldPosition(hipsT, hipsPos)) {
            g_camInitHipsWorldPos = hipsPos;
            Log("[CAM] Initial Hips world pos: (%.3f, %.3f, %.3f)",
                hipsPos.x, hipsPos.y, hipsPos.z);
          }
          void *headT0 = SafeGetBoneTransform(10);
          if (headT0 && g_camGetRot) {
            float hq[4] = {};
            g_camGetRot(headT0, hq);
            float hfx = 2.0f * (hq[3] * hq[1] + hq[0] * hq[2]);
            float hfz = 1.0f - 2.0f * (hq[0] * hq[0] + hq[1] * hq[1]);
            float hfLen = sqrtf(hfx * hfx + hfz * hfz);
            if (hfLen > 0.01f) {
              g_camInitHeadWorldYaw = atan2f(hfx / hfLen, hfz / hfLen);
              Log("[CAM] Initial Head world yaw: %.1fdeg",
                  g_camInitHeadWorldYaw * 57.2958f);
            }
          }
        }

        g_charHeight = 0.0f;
        if (g_animator_GetBoneTransform && g_cachedAnimator) {
          void *headT = SafeGetBoneTransform(10);
          Vec3 headPos;
          if (headT && ReadWorldPosition(headT, headPos)) {
            float h = headPos.y - g_charWorldPos.y;
            if (h > 0.1f && h < 5.0f)
              g_charHeight = h;
          }
        }
        if (g_charHeight > 0.0f) {
          if (g_camRefHeight <= 0.0f)
            g_camRefHeight = (CAM_REF_HEIGHT > 0.0f) ? CAM_REF_HEIGHT
                                                     : g_charHeight;
          g_camHeightScale = g_charHeight / g_camRefHeight;
        } else {
          g_camHeightScale = 1.0f;
        }
        Log("[CAM] Char height=%.3f refHeight=%.3f heightScale=%.3f",
            g_charHeight, g_camRefHeight, g_camHeightScale);
      }
    } __except (1) {
      Log("[CAM] Failed to capture character world pos");
    }
    CaptureAndDisableCinemachine();
    g_cameraActive = true;
    g_camInitInterest = g_cameraPlayer.SampleInterest(0.0f);
    Log("[CAM] Initial VMD interest: (%.1f, %.1f, %.1f)",
        g_camInitInterest.x, g_camInitInterest.y, g_camInitInterest.z);
  }

  if (g_cameraActive && g_musclePlayer) {
    __try {
      if (g_animator_GetBoneTransform && g_cachedAnimator) {
        void *hipsT = SafeGetBoneTransform(0);
        Vec3 hipsNow;
        if (hipsT && ReadWorldPosition(hipsT, hipsNow)) {
          g_charWorldPos.x = hipsNow.x;
          g_charWorldPos.z = hipsNow.z;
          void *rootT = SafeGetComponentTransform(g_cachedAnimator);
          if (rootT && g_camGetPos) {
            float rootPos[3] = {};
            g_camGetPos(rootT, rootPos);
            g_charWorldPos.y = rootPos[1];
          }
        }
      }
    } __except (1) {}
    __try {
      if (g_animator_GetBoneTransform && g_cachedAnimator) {
        void *headT = SafeGetBoneTransform(10);
        Vec3 headPos;
        if (headT && ReadWorldPosition(headT, headPos)) {
          g_headWorldPos = headPos;
          if (g_camGetRot) {
            float hq[4] = {};
            g_camGetRot(headT, hq);
            g_headForward.x = 2.0f * (hq[3] * hq[1] + hq[0] * hq[2]);
            g_headForward.y = 2.0f * (hq[1] * hq[2] - hq[3] * hq[0]);
            g_headForward.z = 1.0f - 2.0f * (hq[0] * hq[0] + hq[1] * hq[1]);
          }
        }
      }
    } __except (1) {}
    if (g_muscleAnim && g_muscleAnim->loaded) {
      MuscleFrame mf = g_muscleAnim->GetFrame(g_musclePlayer->currentTime);
      float bx = mf.bodyRot[0], by = mf.bodyRot[1];
      float bz = mf.bodyRot[2], bw = mf.bodyRot[3];
      float fx = 2.0f * (bw * by + bx * bz);
      float fz = 1.0f - 2.0f * (bx * bx + by * by);
      float curYaw = atan2f(fx, fz);
      g_camHipsYawDelta = curYaw - g_camInitHipsYaw;
      while (g_camHipsYawDelta > 3.14159f) g_camHipsYawDelta -= 6.28318f;
      while (g_camHipsYawDelta < -3.14159f) g_camHipsYawDelta += 6.28318f;
    }
    ApplyCameraFrame(g_musclePlayer->currentTime);
  }
  if (!setExc) ClothTick("Muscle.WndProc.after-pose", true, Muscle_ClothMayOwnAnchor,
                         g_musclePlayer ? g_musclePlayer->currentTime * 30.0 : NAN);
}

static void ReapplyBoneTransforms() {
  if (!g_bonesReady)
    return;


  for (int b = 0; b < MAX_HUMAN_BONES; b++) {
    if (!g_cachedBones[b].valid || !g_cachedBones[b].transform)
      continue;
    SafeSetLocalRotation(g_cachedBones[b].transform, g_cachedBones[b].rotation);
  }

  if (g_hipsTransform) {
    SafeSetLocalPosition(g_hipsTransform, g_cachedHipsPos);
  }
}

static int SafeInvokeCursorAction(void* actionObj) {
  if (!actionObj || !g_actionInvokeMethod) return -1;
  __try {
    void *exc = nullptr;
    il2cpp_runtime_invoke(g_actionInvokeMethod, actionObj, nullptr, &exc);
    return exc ? -2 : 0;
  } __except (1) {
    return -3;
  }
}

static bool EnsureAudioLoaded() {
  if (!g_audioEnabled) return false;
  if (!g_audioPlayer) g_audioPlayer = new AudioPlayer();
  if (g_audioPlayer->loaded) return true;
  if (g_audioPathW[0] == L'\0') return false;
  return g_audioPlayer->Open(g_audioPathW);
}

static void AudioStartFresh() {
  if (!EnsureAudioLoaded()) { g_audioIsClock = false; return; }
  bool normalSpeed = !g_musclePlayer || fabsf(g_musclePlayer->speed - 1.0f) < 0.001f;
  if (!normalSpeed) { g_audioIsClock = false; return; }

  if (g_clothPlaybackGate.Holding(static_cast<uint32_t>(MotionBackend::Muscle),g_motionBackend.Generation())) {
    g_audioPendingStart = true;
    g_audioIsClock = false;
    Log("[AUDIO] Start deferred until cloth preparation completes");
    return;
  }

  if (g_audioOffset < 0.0f) {
    g_audioPendingStart = true;
    g_audioIsClock = false;
    Log("[AUDIO] Start deferred %.2f s (negative offset)", -g_audioOffset);
    return;
  }

  int startMs = (int)(g_audioOffset * 1000.0f);
  g_audioPlayer->PlayFrom(startMs);
  g_audioPlayer->SetVolume(g_audioVolume);
  g_audioIsClock = true;
  g_audioPendingStart = false;
  Log("[AUDIO] Start fresh from %d ms (offset=%.2f), isClock=%d",
      startMs, g_audioOffset, g_audioIsClock ? 1 : 0);
}

struct DirectVmdAudioFollower {
  bool initialized = false;
  bool owns = false;
  bool locked = false;
  bool holding = false;
  uint64_t clipGeneration = 0;
  uint64_t seekRevision = 0;
  uint64_t loopCycle = 0;
  uint64_t resetRevision = 0;
  uint32_t loadGeneration = 0;
  uint32_t holdEpoch = 0;
  DirectVmdPlaybackState playback = DirectVmdPlaybackState::Stopped;
  DirectVmdAudioRange range = DirectVmdAudioRange::Delayed;
  float offsetSeconds = 0.0f;
  int targetMilliseconds = 0;
  double holdSeconds = 0.0;
  double heartbeatSeconds = 0.0;
  double maxAbsErrorSeconds = 0.0;
  uint32_t snaps = 0;
};

static DirectVmdAudioFollower s_directVmdAudioFollower;
static std::atomic<uint64_t> s_directVmdAudioResetRevision{0};
static std::atomic<bool> s_directVmdAudioOwnsDevice{false};

static const char *DirectVmdAudio_RangeName(DirectVmdAudioRange range) {
  switch (range) {
  case DirectVmdAudioRange::Audible: return "audible";
  case DirectVmdAudioRange::PastEnd: return "past-end";
  default: return "delayed";
  }
}

static void DirectVmdAudio_ResetMainThread(const char *reason,
                                           bool closeResource) {
  if (!GhostRig_RequireMainThread("P6.Audio.Reset", true))
    return;
  const bool hadOwnership =
      s_directVmdAudioOwnsDevice.exchange(false, std::memory_order_acq_rel);
  const bool wasLoaded = g_audioPlayer && g_audioPlayer->loaded;
  const bool wasPlaying = wasLoaded && g_audioPlayer->playing;
  if (hadOwnership && wasLoaded) {
    g_audioPlayer->SetPlaybackSpeed(1.0f);
    g_audioPlayer->Stop();
  }
  if (closeResource && g_audioPlayer)
    g_audioPlayer->Close();
  s_directVmdAudioResetRevision.fetch_add(1, std::memory_order_acq_rel);
  g_audioIsClock = false;
  g_audioPendingStart = false;
  Log("[P6-AUDIO-LIFECYCLE] event=reset reason=%s hadOwnership=%d "
      "wasLoaded=%d wasPlaying=%d closeResource=%d tid=%lu",
      reason ? reason : "unspecified", hadOwnership ? 1 : 0,
      wasLoaded ? 1 : 0, wasPlaying ? 1 : 0, closeResource ? 1 : 0,
      GetCurrentThreadId());
}

static void DirectVmdAudio_WorkerSync(DirectVmdClock *clock,
                                      const DirectVmdAudioWorkerTick &tick) {
  DirectVmdAudioFollower &f = s_directVmdAudioFollower;
  const uint64_t resetRevision =
      s_directVmdAudioResetRevision.load(std::memory_order_acquire);
  if (resetRevision != f.resetRevision) {
    f = DirectVmdAudioFollower{};
    f.resetRevision = resetRevision;
  }

  AudioPlayer *audio = g_audioPlayer;
  const bool directBackend = g_motionBackend.Is(MotionBackend::DirectVmd);
  const bool audioReady = g_audioEnabled && audio && audio->loaded;
  if (!clock || !directBackend || !tick.clipLoaded || !audioReady) {
    if (f.owns && audio &&
        s_directVmdAudioOwnsDevice.exchange(false,
                                            std::memory_order_acq_rel)) {
      audio->SetPlaybackSpeed(1.0f);
      audio->Stop();
      Log("[P6-AUDIO-SYNC] event=release reason=%s tid=%lu",
          !directBackend ? "backend-not-direct"
                         : (!tick.clipLoaded ? "clip-unloaded"
                                             : (g_audioEnabled
                                                    ? "audio-unloaded"
                                                    : "audio-disabled")),
          GetCurrentThreadId());
    }
    if (f.initialized) {
      f = DirectVmdAudioFollower{};
      f.resetRevision = resetRevision;
    }
    return;
  }

  const float offset = g_audioOffset;
  const float speed = static_cast<float>(clock->speed);
  const uint32_t loadGeneration = audio->LoadGeneration();
  const DirectVmdPlaybackState state = clock->state;
  const DirectVmdAudioTimelineTarget target = DirectVmdComputeAudioTarget(
      clock->frame, static_cast<double>(offset), audio->GetLengthMs());

  const bool identityChanged = !f.initialized ||
                               tick.clipGeneration != f.clipGeneration ||
                               loadGeneration != f.loadGeneration;
  const bool seekChanged =
      f.initialized && tick.seekRevision != f.seekRevision;
  const bool cycleChanged = f.initialized && clock->loopCycle != f.loopCycle;
  const bool playbackChanged = !f.initialized || state != f.playback;
  const bool offsetChanged =
      f.initialized && fabsf(offset - f.offsetSeconds) > 0.0005f;
  const bool rangeChanged = !f.initialized || target.range != f.range;
  const bool timelineJump = identityChanged || seekChanged || cycleChanged ||
                            offsetChanged || rangeChanged;

  auto claim = [&]() {
    f.owns = true;
    s_directVmdAudioOwnsDevice.store(true, std::memory_order_release);
  };
  auto record = [&]() {
    f.initialized = true;
    f.clipGeneration = tick.clipGeneration;
    f.seekRevision = tick.seekRevision;
    f.loopCycle = clock->loopCycle;
    f.playback = state;
    f.range = target.range;
    f.offsetSeconds = offset;
    f.targetMilliseconds = target.mediaMilliseconds;
    f.loadGeneration = loadGeneration;
  };

  g_audioIsClock = false;
  g_audioPendingStart = false;
  audio->SetPlaybackSpeed(speed);

  if (state == DirectVmdPlaybackState::Stopped) {
    if (f.owns) {
      audio->Stop();
      Log("[P6-AUDIO-SYNC] event=stop sourceFrame=%.6f tid=%lu",
          clock->frame, GetCurrentThreadId());
    }
    f.owns = false;
    s_directVmdAudioOwnsDevice.store(false, std::memory_order_release);
    f.locked = f.holding = false;
    record();
    return;
  }

  if (state == DirectVmdPlaybackState::Paused ||
      state == DirectVmdPlaybackState::Ended) {
    if (audio->playing)
      audio->Pause();
    const bool jump = timelineJump || playbackChanged;
    if (jump || target.mediaMilliseconds != f.targetMilliseconds)
      audio->SeekTo(target.mediaMilliseconds);
    if (jump) {
      Log("[P6-AUDIO-SYNC] event=%s sourceFrame=%.6f audioMs=%d range=%s "
          "cycle=%llu seekRevision=%llu tid=%lu",
          state == DirectVmdPlaybackState::Ended ? "ended-hold"
                                                 : "paused-seek",
          clock->frame, target.mediaMilliseconds,
          DirectVmdAudio_RangeName(target.range),
          (unsigned long long)clock->loopCycle,
          (unsigned long long)tick.seekRevision, GetCurrentThreadId());
    }
    claim();
    f.locked = f.holding = false;
    record();
    return;
  }

  if (target.range != DirectVmdAudioRange::Audible) {
    const bool jump = timelineJump || playbackChanged;
    if (audio->playing || jump)
      audio->SeekTo(target.mediaMilliseconds);
    if (jump) {
      Log("[P6-AUDIO-SYNC] event=%s sourceFrame=%.6f audioMs=%d "
          "cycle=%llu seekRevision=%llu clock=qpc tid=%lu",
          target.range == DirectVmdAudioRange::Delayed ? "offset-delay"
                                                       : "audio-past-end",
          clock->frame, target.mediaMilliseconds,
          (unsigned long long)clock->loopCycle,
          (unsigned long long)tick.seekRevision, GetCurrentThreadId());
    }
    claim();
    f.locked = f.holding = false;
    record();
    return;
  }

  const bool needsStart =
      timelineJump || playbackChanged || !f.owns ||
      (!audio->playing && !audio->EndedNaturally());
  if (needsStart) {
    audio->PlayFrom(target.mediaMilliseconds);
    audio->SetVolume(g_audioVolume);
    claim();
    f.locked = false;
    f.holding =
        identityChanged || seekChanged || cycleChanged || playbackChanged;
    f.holdSeconds = 0.0;
    AudioClockSample started;
    audio->QueryClock(&started);
    f.holdEpoch = started.commandEpoch;
    Log("[P6-AUDIO-SYNC] event=%s sourceFrame=%.6f audioMs=%d speed=%.3f "
        "cycle=%llu seekRevision=%llu hold=%d nonBlocking=1 tid=%lu",
        identityChanged ? "start"
                        : (cycleChanged ? "loop-restart"
                                        : (seekChanged ? "seek"
                                                       : (playbackChanged
                                                              ? "play-or-resume"
                                                              : "retarget"))),
        clock->frame, target.mediaMilliseconds, speed,
        (unsigned long long)clock->loopCycle,
        (unsigned long long)tick.seekRevision, f.holding ? 1 : 0,
        GetCurrentThreadId());
    record();
    return;
  }

  AudioClockSample sample;
  const bool clockValid = audio->QueryClock(&sample);
  const bool newestAudible = clockValid && sample.audibleMedia &&
                             sample.audibleEpoch == sample.commandEpoch;
  if (f.holding) {
    f.holdSeconds += tick.elapsedSeconds;
    if (newestAudible && sample.commandEpoch == f.holdEpoch) {
      f.holding = false;
      Log("[P6-AUDIO-SYNC] event=audible heldMs=%.1f latencyMs=%.1f "
          "audibleMs=%.1f sourceFrame=%.6f tid=%lu",
          f.holdSeconds * 1000.0, sample.latencySeconds * 1000.0,
          sample.mediaSeconds * 1000.0, clock->frame, GetCurrentThreadId());
    } else if ((!clockValid && !audio->DevicePending()) ||
               f.holdSeconds >= kDirectVmdAudioStartHoldSeconds) {
      f.holding = false;
      Log("[P6-AUDIO-SYNC] event=hold-abandoned reason=%s heldMs=%.1f "
          "clock=qpc tid=%lu",
          clockValid ? "timeout" : "no-device-clock",
          f.holdSeconds * 1000.0, GetCurrentThreadId());
    } else {
      if (clock->loopCycle == tick.cycleBeforeAdvance)
        clock->frame = tick.frameBeforeAdvance;
      record();
      return;
    }
  }

  if (newestAudible && clock->loopCycle == tick.cycleBeforeAdvance) {
    const DirectVmdAudioLockResult lock = DirectVmdAudioLockClockFrame(
        clock->frame, sample.mediaSeconds, static_cast<double>(offset),
        tick.elapsedSeconds, clock->durationFrames);
    clock->frame = lock.frame;
    if (!f.locked) {
      f.locked = true;
      Log("[P6-AUDIO-LOCK] event=acquired errorMs=%.1f latencyMs=%.1f "
          "sourceFrame=%.6f tid=%lu",
          lock.errorSeconds * 1000.0, sample.latencySeconds * 1000.0,
          clock->frame, GetCurrentThreadId());
    }
    if (lock.snapped) {
      if (++f.snaps <= 5 || f.snaps % 100 == 0)
        Log("[P6-AUDIO-LOCK] event=snap errorMs=%.1f snaps=%u "
            "sourceFrame=%.6f tid=%lu",
            lock.errorSeconds * 1000.0, f.snaps, clock->frame,
            GetCurrentThreadId());
    } else {
      f.maxAbsErrorSeconds =
          (std::max)(f.maxAbsErrorSeconds, std::fabs(lock.errorSeconds));
    }
  } else if (f.locked) {
    f.locked = false;
    Log("[P6-AUDIO-LOCK] event=released reason=%s sourceFrame=%.6f tid=%lu",
        !clockValid ? "no-device-clock"
                    : (!sample.audibleMedia ? "silence" : "command-pending"),
        clock->frame, GetCurrentThreadId());
  }

  f.heartbeatSeconds += tick.elapsedSeconds;
  if (f.heartbeatSeconds >= 5.0) {
    f.heartbeatSeconds = 0.0;
    Log("[P6-AUDIO-HEARTBEAT] sourceFrame=%.3f desiredMs=%d audibleMs=%.1f "
        "maxErrorMs=%.2f latencyMs=%.1f locked=%d snaps=%u speed=%.3f "
        "clock=%s tid=%lu",
        clock->frame, target.mediaMilliseconds,
        clockValid ? sample.mediaSeconds * 1000.0 : -1.0,
        f.maxAbsErrorSeconds * 1000.0,
        clockValid ? sample.latencySeconds * 1000.0 : -1.0,
        f.locked ? 1 : 0, f.snaps, speed,
        f.locked ? "audio-device" : "qpc", GetCurrentThreadId());
    f.maxAbsErrorSeconds = 0.0;
  }
  record();
}

static bool MotionBackend_RequireCommandThread(const char *stage) {
  return GhostRig_RequireMainThread(stage, true);
}

static bool MotionBackend_PublishMainThread(MotionBackend next,
                                            const char *reason) {
  if (!MotionBackend_RequireCommandThread("MotionBackend.Transition"))
    return false;
  const MotionBackend previous = g_motionBackend.Current();
  const bool changed = g_motionBackend.TransitionTo(next);
  if (changed) ClothRelease(reason);
  Log("[P3-BACKEND] frame=%d seq=%llu tid=%lu old=%s new=%s "
      "changed=%d backendGeneration=%llu ghostGeneration=%llu reason=%s",
      GhostRig_GetFrameCount(),
      (unsigned long long)GhostRig_NextOrderSequence(),
      GetCurrentThreadId(), MotionBackendName(previous),
      MotionBackendName(next), changed ? 1 : 0,
      (unsigned long long)g_motionBackend.Generation(),
      (unsigned long long)s_ghostRequestedGeneration.load(
          std::memory_order_acquire),
      reason ? reason : "unspecified");
  return true;
}

static bool MotionBackend_HasMuscleOwnership() {
  return g_trojanActive || s_poseReady || g_poseHandleGC != 0 ||
         g_musclesArrayGC != 0 || g_mmdIKActive || s_ikDisabled ||
         (g_musclePlayer &&
          (g_musclePlayer->playing || g_musclePlayer->currentTime > 0.0f ||
           g_musclePlayer->ended));
}

static void MotionBackend_ReleaseMuscleMainThread(const char *reason) {
  const bool hadOwnership = MotionBackend_HasMuscleOwnership();

  g_trojanActive = false;
  g_mmdPendingApply = false;
  g_mmdHasMuscles = false;
  g_mmdHasArmBones = false;
  g_mmdHasFingerBones = false;
  g_mouthWeightsFromMuscle = false;
  if (g_musclePlayer &&
      (g_musclePlayer->playing || g_musclePlayer->currentTime > 0.0f ||
       g_musclePlayer->ended)) {
    g_musclePlayer->Stop();
  }

  memset((void *)g_mmdMuscles, 0, sizeof(g_mmdMuscles));
  memset((void *)g_mmdArmBoneRots, 0, sizeof(g_mmdArmBoneRots));
  memset((void *)g_mmdFingerBoneRots, 0, sizeof(g_mmdFingerBoneRots));
  memset((void *)g_mouthWeights, 0, sizeof(g_mouthWeights));
  CleanupPoseHandler();
  RestoreBigList();
  if (g_confirmedSMC && OFF_allMorphBoneDirty > 0) {
    __try {
      *(bool *)((char *)g_confirmedSMC + OFF_allMorphBoneDirty) = true;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
    }
  }
  memset((void *)g_faceBoneTouched, 0, sizeof(g_faceBoneTouched));
  for (int i = 0; i < NUM_EXTRA_MORPHS; ++i) {
    g_extraMorphs[i].weight = 0.0f;
    g_extraMorphs[i].prevWeight = 0.0f;
  }

  g_mmdIKActive = false;
  ClearActiveFootIKSolverWeights();
  ResetFootIKRuntimeState();
  RestoreDisabledComponents();
  SafeSetAnimatorEnabled(true);

  if (g_cameraActive)
    RestoreCinemachine();
  ResetCameraState();
  s_firstFrame = true;
  if (s_initialRootCaptured && g_camSetPos && g_cachedAnimator) {
    void *rootTransform = SafeGetComponentTransform(g_cachedAnimator);
    if (rootTransform) {
      g_camSetPos(rootTransform, s_initialRootPos);
      Log("[P3-BACKEND] restored legacy MUS4 root before ownership release "
          "position=(%.3f,%.3f,%.3f)",
          s_initialRootPos[0], s_initialRootPos[1], s_initialRootPos[2]);
    }
  }
  s_initialRootCaptured = false;
  if (g_audioPlayer)
    g_audioPlayer->Stop();
  g_audioIsClock = false;
  g_audioPendingStart = false;

  Log("[P3-MUSCLE-RELEASE] hadOwnership=%d backend=%s reason=%s "
      "trojan=0 pendingPose=0 poseHandle=%u ikActive=%d tid=%lu",
      hadOwnership ? 1 : 0, MotionBackendName(g_motionBackend.Current()),
      reason ? reason : "unspecified", g_poseHandleGC,
      g_mmdIKActive ? 1 : 0, GetCurrentThreadId());
}

static void MotionBackend_DisableDirectMainThread(
    GhostRigCleanupReason cleanupReason, const char *reason) {
  ClothRelease(reason ? reason : "direct-release");
  DirectVmdRuntime_RequestStop();
  DirectVmdMorph_ResetMainThread(reason ? reason : "direct-release");
  DirectVmdCamera_ResetMainThread(reason ? reason : "direct-release");
  const bool closeAudioResource =
      cleanupReason == GhostRigCleanupReason::PluginDisabled ||
      cleanupReason == GhostRigCleanupReason::PluginUnload ||
      cleanupReason == GhostRigCleanupReason::WindowClosing;
  DirectVmdAudio_ResetMainThread(reason ? reason : "direct-release",
                                 closeAudioResource);
  DirectVmdRuntime_RequestCameraOverride(nullptr);
  DirectVmdRuntime_RequestMorphOverride(nullptr);
  if (GhostRig_IsRequestedEnabled() || GhostRig_IsAliveForGui())
    GhostRig_RequestEnabled(false, cleanupReason);
  else
    DirectVmdRuntime_SetActive(false);
  GhostRig_TryImmediateCleanupOnCurrentThread(cleanupReason);
  Log("[P3-DIRECT-RELEASE] backend=%s reason=%s ghostAlive=%d "
      "generation=%llu targetHandlesRetained=0 tid=%lu",
      MotionBackendName(g_motionBackend.Current()),
      reason ? reason : "unspecified",
      GhostRig_IsAliveForGui() ? 1 : 0,
      (unsigned long long)s_ghostRequestedGeneration.load(
          std::memory_order_acquire),
      GetCurrentThreadId());
}

static void MotionBackend_EnterNativeMainThread(
    GhostRigCleanupReason cleanupReason, const char *reason,
    bool releaseMuscle = true) {
  if (!MotionBackend_RequireCommandThread("MotionBackend.EnterNative"))
    return;
  g_clothPlaybackGate.Cancel();
  if(cleanupReason==GhostRigCleanupReason::PluginDisabled||cleanupReason==GhostRigCleanupReason::PluginUnload||
      cleanupReason==GhostRigCleanupReason::WindowClosing)ClothPrefetchCancel();
  const MotionBackend previous = g_motionBackend.Current();
  if (!MotionBackend_PublishMainThread(MotionBackend::Native, reason))
    return;
  if (previous == MotionBackend::DirectVmd ||
      GhostRig_IsRequestedEnabled() || GhostRig_IsAliveForGui()) {
    MotionBackend_DisableDirectMainThread(cleanupReason, reason);
  }
  if (releaseMuscle &&
      (previous == MotionBackend::Muscle ||
       MotionBackend_HasMuscleOwnership())) {
    MotionBackend_ReleaseMuscleMainThread(reason);
  }
  if ((cleanupReason == GhostRigCleanupReason::PluginDisabled ||
       cleanupReason == GhostRigCleanupReason::PluginUnload ||
       cleanupReason == GhostRigCleanupReason::WindowClosing) &&
      g_audioPlayer)
    g_audioPlayer->Close();
  ClothRelease(reason);
  SafeSetAnimatorEnabled(true);
  if (previous == MotionBackend::DirectVmd) {
    Log("[P4-NATIVE-ANIMATOR] enabled=1 route=native-restore "
        "reason=%s entity=%p animator=%p tid=%lu",
        reason ? reason : "unspecified", g_mainCharEntity,
        g_cachedAnimator, GetCurrentThreadId());
  }
}

static bool MotionBackend_EnterMuscleMainThread(const char *reason) {
  if (!g_pluginActive ||
      !MotionBackend_RequireCommandThread("MotionBackend.EnterMuscle"))
    return false;
  if(g_clothPlaybackGate.State()==eiem_playback::Preparation::Failed)
    MotionBackend_EnterNativeMainThread(GhostRigCleanupReason::Stop,"cloth-start-retry");
  const MotionBackend previous = g_motionBackend.Current();
  if (!MotionBackend_PublishMainThread(MotionBackend::Muscle, reason))
    return false;
  if (previous == MotionBackend::DirectVmd ||
      GhostRig_IsRequestedEnabled() || GhostRig_IsAliveForGui()) {
    MotionBackend_DisableDirectMainThread(GhostRigCleanupReason::Stop,
                                          "switch-to-muscle");
  }
  RefreshEntityAnimator();
  ClothBegin(MotionBackend::Muscle, true);
  return true;
}

static bool MotionBackend_EnterDirectMainThread(bool play,
                                                const char *reason) {
  if (!g_pluginActive || !DirectVmdRuntime_IsLoaded()) {
    Log("[P3-DIRECT-ENTER] rejected pluginActive=%d loaded=%d reason=%s",
        g_pluginActive ? 1 : 0, DirectVmdRuntime_IsLoaded() ? 1 : 0,
        reason ? reason : "unspecified");
    return false;
  }
  if (!MotionBackend_RequireCommandThread("MotionBackend.EnterDirect"))
    return false;
  if(play && g_clothPlaybackGate.State()==eiem_playback::Preparation::Failed)
    MotionBackend_EnterNativeMainThread(GhostRigCleanupReason::Stop,"cloth-start-retry");

  if (g_motionBackend.Is(MotionBackend::DirectVmd)) {
    if (!GhostRig_IsRequestedEnabled()) {
      RefreshEntityAnimator();
      if (!g_mainCharEntity || !g_cachedAnimator) {
        MotionBackend_PublishMainThread(MotionBackend::Native,
                                        "direct-retry-no-owner");
        DirectVmdRuntime_RequestStop();
        DirectVmdRuntime_SetActive(false);
        SafeSetAnimatorEnabled(true);
        return false;
      }
      GhostRig_RequestOwnerChange(g_mainCharEntity);
      GhostRig_RequestEnabled(true, GhostRigCleanupReason::None);
      DirectVmdRuntime_RequestCameraOverride(
          g_cameraOverrideExplicit && g_cameraVmdPath[0]
              ? g_cameraVmdPath
              : nullptr);
      DirectVmdRuntime_RequestMorphOverride(
          g_morphVmdPath[0] ? g_morphVmdPath : nullptr);
    }
    ClothBegin(MotionBackend::DirectVmd, play);
    SafeSetAnimatorEnabled(false);
    DirectVmdRuntime_SetSpeed(g_playbackSpeed);
    DirectVmdRuntime_SetLoop(g_playbackLoop);
    if (play) {
      if (EnsureAudioLoaded())
        g_audioPlayer->SetVolume(g_audioVolume);
      DirectVmdRuntime_RequestPlay();
    }
    return true;
  }

  const MotionBackend previous = g_motionBackend.Current();
  if (!MotionBackend_PublishMainThread(MotionBackend::DirectVmd, reason))
    return false;
  if (previous == MotionBackend::Muscle ||
      MotionBackend_HasMuscleOwnership()) {
    MotionBackend_ReleaseMuscleMainThread("switch-to-direct-vmd");
  } else {
    g_trojanActive = false;
    g_mmdPendingApply = false;
  }

  RefreshEntityAnimator();
  if (!g_mainCharEntity || !g_cachedAnimator) {
    Log("[P3-DIRECT-ENTER] rejected reason=no-owner entity=%p animator=%p",
        g_mainCharEntity, g_cachedAnimator);
    MotionBackend_PublishMainThread(MotionBackend::Native,
                                    "direct-no-owner");
    DirectVmdRuntime_RequestStop();
    DirectVmdRuntime_SetActive(false);
    SafeSetAnimatorEnabled(true);
    return false;
  }

  GhostRig_RequestOwnerChange(g_mainCharEntity);
  ClothBegin(MotionBackend::DirectVmd, play);

  SafeSetAnimatorEnabled(false);
  Log("[P4-NATIVE-ANIMATOR] enabled=0 "
      "route=Muscle.SafeSetAnimatorEnabled reason=%s entity=%p "
      "animator=%p fingerOwnership=DirectVmd-hold "
      "finalIkComponentDisabled=0 finalIkSolverPolicy=stage5-per-leg "
      "tid=%lu",
      reason ? reason : "unspecified", g_mainCharEntity,
      g_cachedAnimator, GetCurrentThreadId());

  GhostRig_RequestEnabled(true, GhostRigCleanupReason::None);
  DirectVmdRuntime_RequestCameraOverride(
      g_cameraOverrideExplicit && g_cameraVmdPath[0]
          ? g_cameraVmdPath
          : nullptr);
  DirectVmdRuntime_RequestMorphOverride(
      g_morphVmdPath[0] ? g_morphVmdPath : nullptr);
  DirectVmdRuntime_SetSpeed(g_playbackSpeed);
  DirectVmdRuntime_SetLoop(g_playbackLoop);
  if (play) {
    if (EnsureAudioLoaded())
      g_audioPlayer->SetVolume(g_audioVolume);
    DirectVmdRuntime_RequestPlay();
  }
  Log("[P4-DIRECT-ENTER] accepted play=%d entity=%p animator=%p "
      "backendGeneration=%llu ghostGeneration=%llu setHumanPose=0 "
      "camera=DirectVmd audio=DirectVmd morph=DirectVmd "
      "cameraOverrideExplicit=%d finalIkTargetsDeferredToHook=1 "
      "finalIkSolverPolicy=stage5-per-leg nativeAnimator=0 tid=%lu",
      play ? 1 : 0, g_mainCharEntity, g_cachedAnimator,
      (unsigned long long)g_motionBackend.Generation(),
      (unsigned long long)s_ghostRequestedGeneration.load(
          std::memory_order_acquire),
      g_cameraOverrideExplicit ? 1 : 0,
      GetCurrentThreadId());
  return true;
}


static LRESULT CALLBACK MmdWndProc(HWND hwnd, UINT msg, WPARAM wParam,
                                   LPARAM lParam) {
  ClothCollisionServiceUi();
  if (ClothCollisionNeedsMaintenance() || s_cloth.releasing || (s_cloth.active &&
      (s_cloth.invalidation != s_clothInvalidation.load(std::memory_order_acquire) ||
       s_cloth.owner.generation != g_motionBackend.Generation())))
    ClothTick("WndProc.lifecycle");
  if(msg==WM_USER+126) {
    s_clothPrefetchPosted.store(false,std::memory_order_release);
    if(!ClothOnMainThread())return 0;
    if(g_pluginActive&&g_motionBackend.Is(MotionBackend::Native))RefreshEntityAnimator();
    ClothPrefetchPulse();return 0;
  }
  if (msg == WM_CLOSE || msg == WM_DESTROY) {
    Log("[WNDPROC] Game window closing (msg=0x%X), signaling threads to exit",
        msg);
    MotionBackend_EnterNativeMainThread(
        GhostRigCleanupReason::WindowClosing, "window-closing");
    g_guiRunning = false;
  }

  if (msg == WM_MMD_APPLY_POSE) {
    if (!g_motionBackend.Is(MotionBackend::Muscle)) {
      static uint64_t rejectedPoseMessages = 0;
      ++rejectedPoseMessages;
      if (rejectedPoseMessages <= 8 ||
          (rejectedPoseMessages % 120) == 0) {
        Log("[P3-SET-HUMAN-POSE-REJECT] backend=%s count=%llu tid=%lu "
            "message=WM_MMD_APPLY_POSE",
            MotionBackendName(g_motionBackend.Current()),
            (unsigned long long)rejectedPoseMessages,
            GetCurrentThreadId());
      }
      g_mmdPendingApply = false;
      return 0;
    }
    static bool s_firstLog = false;
    if (!s_firstLog) {
      Log("[WNDPROC] WM_MMD_APPLY_POSE received on main thread!");
      s_firstLog = true;
    }
    ClothBegin(MotionBackend::Muscle);
    SafeSetAnimatorEnabled(false);
    if(ClothBlockFirstBodyPose("Muscle.held-native-before-first-body",Muscle_ClothMayOwnAnchor)) {
      g_mmdPendingApply=false;return 0;
    }
    if (!s_poseReady) {
      InitMmdPoseOnMainThread();
    }
    if (s_poseReady) {
      PreSampleVmdCenter();
      ApplyMmdPoseOnMainThread();
    }
    g_mmdPendingApply = false;
    return 0;
  }
  if (msg >= (WM_USER + 100) && msg <= (WM_USER + 120)) {
    switch (msg) {
    case (WM_USER + 100):
      Log("[GUI-CMD] Play selectedBackend=%s",
          MotionBackendName(GuiSelectedMotionBackend()));
      if (GuiSelectedMotionBackend() == MotionBackend::DirectVmd) {
        if (!g_directVmdPath[0]) {
          Log("[GUI-CMD] DirectVmd play rejected: no action VMD selected");
          return 0;
        }
        if (!DirectVmdRuntime_IsLoaded()) {
          GhostRig_RequestVmdReload(g_directVmdPath);
          Log("[GUI-CMD] DirectVmd play waiting for background load: %s",
              g_directVmdPath);
          return 0;
        }
        MotionBackend_EnterDirectMainThread(true, "gui-direct-play");
        return 0;
      }
      if (!g_muscleAnimPath[0]) {
        Log("[GUI-CMD] Muscle play rejected: no muscle data selected");
        return 0;
      }
      if (!g_muscleAnim) g_muscleAnim = new MuscleAnim();
      if (!g_muscleAnim->loaded)
        g_muscleAnim->Load(g_muscleAnimPath);
      if (g_muscleAnim->loaded) {
        if (!MotionBackend_EnterMuscleMainThread("muscle-play"))
          return 0;
        if (!g_musclePlayer) {
          g_musclePlayer = new MmdPlayer();
          g_musclePlayer->speed = g_playbackSpeed;
          g_musclePlayer->loop  = g_playbackLoop;
        }
        if (g_musclePlayer->playing) {
        } else if (g_musclePlayer->ended) {
          g_musclePlayer->Start(g_muscleAnim->Duration());
          AudioStartFresh();
        } else if (g_musclePlayer->currentTime > 0 && !g_musclePlayer->ended) {
          g_musclePlayer->TogglePause();
          if (g_audioIsClock && g_audioPlayer) g_audioPlayer->Resume();
        } else {
          RefreshEntityAnimator();
          if (InitMusclePoseHandler()) {
            if (g_trojanHookTarget) MH_EnableHook(g_trojanHookTarget);
            g_trojanActive = true;
            g_musclePlayer->Start(g_muscleAnim->Duration());
            if (g_cameraEnabled && g_cameraVmdPath[0]) {
              Log("[CAM-LOAD] begin reason=play path='%s'",
                  g_cameraVmdPath);
              if (!g_cameraVmd) g_cameraVmd = LoadVmd(g_cameraVmdPath);
              if (g_cameraVmd && g_cameraVmd->loaded &&
                  !g_cameraVmd->cameraKeys.empty()) {
                g_cameraPlayer.SetVmd(g_cameraVmd);
                g_cameraNeedsCapture = true;
                Log("[CAM-LOAD] ready keys=%zu frames=%u warnings=%zu",
                    g_cameraVmd->cameraKeys.size(),
                    g_cameraVmd->totalFrames,
                    g_cameraVmd->warnings.size());
              } else {
                const char *error =
                    (g_cameraVmd && !g_cameraVmd->error.empty())
                        ? g_cameraVmd->error.c_str()
                        : "no camera keyframes";
                Log("[CAM-LOAD] failed path='%s' error='%s'",
                    g_cameraVmdPath, error);
              }
            } else {
              Log("[CAM-LOAD] skipped reason=%s",
                  g_cameraEnabled ? "no-camera-file" : "camera-disabled");
            }
            AudioStartFresh();
          } else {
            MotionBackend_EnterNativeMainThread(
                GhostRigCleanupReason::Stop, "muscle-init-failed");
          }
        }
      }
      return 0;
    case (WM_USER + 101):
      Log("[GUI-CMD] Pause");
      if (g_motionBackend.Is(MotionBackend::DirectVmd)) {
        DirectVmdRuntime_RequestPause();
        ClothBoneRequestPauseEvidence();
      } else if (g_motionBackend.Is(MotionBackend::Muscle) &&
          g_musclePlayer && g_musclePlayer->playing) {
        g_musclePlayer->TogglePause();
        ClothBoneRequestPauseEvidence();
        if (g_audioIsClock && g_audioPlayer) g_audioPlayer->Pause();
      }
      return 0;
    case (WM_USER + 102):
      Log("[GUI-CMD] Stop");
      MotionBackend_EnterNativeMainThread(GhostRigCleanupReason::Stop,
                                          "user-stop");
      return 0;
    case (WM_USER + 103):
      Log("[GUI-CMD] Load: %s", g_muscleAnimPath);
      if (!g_muscleAnim) g_muscleAnim = new MuscleAnim();
      g_muscleAnim->loaded = false;
      if (g_muscleAnim->Load(g_muscleAnimPath)) {
        Log("[GUI-CMD] Loaded: %d frames, %.1f sec",
            g_muscleAnim->frameCount, g_muscleAnim->Duration());
      } else {
        Log("[GUI-CMD] Load FAILED: %s", g_muscleAnimPath);
      }
      return 0;
    case (WM_USER + 107):
      Log("[GUI-CMD] Load audio: %s", g_audioPath);
      if (!g_audioPlayer) g_audioPlayer = new AudioPlayer();
      DirectVmdAudio_ResetMainThread("audio-reload", false);
      g_audioPlayer->Close();
      if (g_audioPathW[0] != L'\0') {
        if (g_audioPlayer->Open(g_audioPathW)) {
          g_audioPlayer->SetVolume(g_audioVolume);
          Log("[GUI-CMD] Audio loaded: %d ms", g_audioPlayer->GetLengthMs());
        } else
          Log("[GUI-CMD] Audio load FAILED: %s", g_audioPath);
      }
      return 0;
    case (WM_USER + 108): {
      int motionMs = (int)wParam;
      if (g_motionBackend.Is(MotionBackend::DirectVmd)) {
        const double directFrame =
            static_cast<double>(motionMs) *
            (kVmdFramesPerSecond / 1000.0);
        DirectVmdRuntime_RequestSeekFrame(directFrame);
        Log("[P6-AUDIO-SYNC] event=seek-command-forwarded "
            "motionMs=%d directFrame=%.6f authority=DirectVmd tid=%lu",
            motionMs, directFrame, GetCurrentThreadId());
        return 0;
      }
      int audioMs = motionMs + (int)(g_audioOffset * 1000.0f);
      if (audioMs < 0) audioMs = 0;
      Log("[GUI-CMD] Seek audio: motion=%d ms, audio=%d ms (offset=%.2f)",
          motionMs, audioMs, g_audioOffset);
      if (g_audioPlayer && g_audioPlayer->loaded && g_audioEnabled) {
        bool motionPlaying = g_musclePlayer && g_musclePlayer->playing &&
            !g_clothPlaybackGate.Holding(static_cast<uint32_t>(MotionBackend::Muscle),g_motionBackend.Generation());
        g_audioPlayer->Stop();
        if (motionPlaying) {
          g_audioPlayer->PlayFrom(audioMs);
          g_audioPlayer->SetVolume(g_audioVolume);
          g_audioIsClock = true;
        }
      }
      return 0;
    }
    case (WM_USER + 109):
      if (g_audioPlayer && g_audioPlayer->loaded)
        g_audioPlayer->SetVolume((int)wParam);
      return 0;
    case (WM_USER + 104):
      SafeInvokeCursorAction(g_cursorShowAction);
      return 0;
    case (WM_USER + 105):
      SafeInvokeCursorAction(g_cursorHideAction);
      return 0;
    case (WM_USER + 106):
      Log("[GUI-CMD] Re-capture character");
      GhostRig_RequestOwnerChange(nullptr);
      MotionBackend_EnterNativeMainThread(
          GhostRigCleanupReason::Recapture, "character-recapture");
      g_cachedAnimator = nullptr;
      s_poseReady = false;
      g_mmdHasMuscles = false;
      g_mmdHasArmBones = false;
      g_mmdHasFingerBones = false;
      g_fingerTransformsResolved = false;
      g_fingerRestCaptured = false;
      memset(g_fingerTransforms, 0, sizeof(g_fingerTransforms));
      g_mouthWeightsFromMuscle = false;
      g_bsIndicesResolved = false;
      g_groundDeltaY = 0.0f;
      memset(g_mouthWeights, 0, sizeof(g_mouthWeights));
      RefreshEntityAnimator();
      GhostRig_RequestOwnerChange(g_mainCharEntity);
      if (g_cachedAnimator) {
        Log("[GUI-CMD] New character captured: animator=%p", g_cachedAnimator);
      } else {
        Log("[GUI-CMD] No character found. Switch to a character first.");
      }
      return 0;
    case (WM_USER + 118): {
      bool enable = wParam != 0;
      GhostRigCleanupReason reason = GhostRigCleanupReason::UserDisabled;
      if (lParam != 0) {
        reason = static_cast<GhostRigCleanupReason>(lParam);
      }
      if (enable)
        reason = GhostRigCleanupReason::None;
      if (enable) {
        MotionBackend_EnterDirectMainThread(false, "direct-toggle-on");
      } else if (reason == GhostRigCleanupReason::PluginDisabled ||
                 reason == GhostRigCleanupReason::PluginUnload ||
                 reason == GhostRigCleanupReason::WindowClosing) {
        MotionBackend_EnterNativeMainThread(reason,
                                            "direct-global-disable");
      } else if (g_motionBackend.Is(MotionBackend::DirectVmd) ||
                 GhostRig_IsRequestedEnabled() ||
                 GhostRig_IsAliveForGui()) {
        MotionBackend_EnterNativeMainThread(reason,
                                            "direct-toggle-off", false);
      }
      Log("[P3-DIRECT-CMD] enable=%d backend=%s reason=%s "
          "wndTid=%lu mainTid=%lu",
          enable ? 1 : 0, MotionBackendName(g_motionBackend.Current()),
          GhostRig_CleanupReasonName(reason),
          GetCurrentThreadId(),
          s_ghostMainThreadId.load(std::memory_order_acquire));
      return 0;
    }
    case (WM_USER + 119): {
      if (wParam == 1) {
        MotionBackend_EnterDirectMainThread(true, "direct-play");
      } else if (wParam == 2) {
        if (g_motionBackend.Is(MotionBackend::DirectVmd)) {
          DirectVmdRuntime_RequestPause();
          ClothBoneRequestPauseEvidence();
          Log("[P3-DIRECT-CMD] pause backend=DirectVmd tid=%lu",
              GetCurrentThreadId());
        }
      } else if (g_motionBackend.Is(MotionBackend::DirectVmd) ||
                 GhostRig_IsRequestedEnabled() ||
                 GhostRig_IsAliveForGui()) {
        MotionBackend_EnterNativeMainThread(
            GhostRigCleanupReason::Stop, "direct-stop", false);
      } else {
        DirectVmdRuntime_RequestStop();
      }
      return 0;
    }
    case (WM_USER + 120): {
      const MotionBackend selected =
          wParam == static_cast<WPARAM>(MotionBackend::Muscle)
              ? MotionBackend::Muscle
              : MotionBackend::DirectVmd;
      GuiSelectMotionBackend(selected);
      const MotionBackend active = g_motionBackend.Current();
      if ((active != MotionBackend::Native && active != selected) ||
          (selected == MotionBackend::Muscle &&
           (GhostRig_IsRequestedEnabled() || GhostRig_IsAliveForGui()))) {
        MotionBackend_EnterNativeMainThread(
            GhostRigCleanupReason::Stop, "gui-backend-selection-change");
      }
      if (selected == MotionBackend::DirectVmd) {
        if (g_directVmdPath[0] && !DirectVmdRuntime_IsLoaded())
          GhostRig_RequestVmdReload(g_directVmdPath);
        DirectVmdRuntime_RequestCameraOverride(
            g_cameraOverrideExplicit && g_cameraVmdPath[0]
                ? g_cameraVmdPath
                : nullptr);
        DirectVmdRuntime_RequestMorphOverride(
            g_morphVmdPath[0] ? g_morphVmdPath : nullptr);
      }
      Log("[GUI-MODE] selected=%s active=%s defaultDirect=1 tid=%lu",
          MotionBackendName(selected),
          MotionBackendName(g_motionBackend.Current()),
          GetCurrentThreadId());
      return 0;
    }
    case (WM_USER + 110): {
      Log("[GUI-CMD] Unload muscle animation");
      if (g_motionBackend.Is(MotionBackend::Muscle) ||
          MotionBackend_HasMuscleOwnership()) {
        MotionBackend_EnterNativeMainThread(
            GhostRigCleanupReason::Stop, "unload-muscle-animation");
      }
      if (g_musclePlayer && (g_musclePlayer->playing || g_musclePlayer->currentTime > 0)) {
        if (g_musclePlayer->playing) g_musclePlayer->TogglePause();
        g_musclePlayer->Stop();
        g_trojanActive = false;
        g_mouthWeightsFromMuscle = false;
        memset((void *)g_mouthWeights, 0, sizeof(g_mouthWeights));
        CleanupPoseHandler();
        RestoreBigList();
        if (g_confirmedSMC && OFF_allMorphBoneDirty > 0) {
          *(bool *)((char *)g_confirmedSMC + OFF_allMorphBoneDirty) = true;
        }
        memset((void *)g_faceBoneTouched, 0, sizeof(g_faceBoneTouched));
        for (int i = 0; i < NUM_EXTRA_MORPHS; i++) {
          g_extraMorphs[i].weight = 0;
          g_extraMorphs[i].prevWeight = 0;
        }
        g_mmdIKActive = false;
        ClearActiveFootIKSolverWeights();
        ResetFootIKRuntimeState();
        RestoreDisabledComponents();
        SafeSetAnimatorEnabled(true);
        if (g_cameraActive) {
          RestoreCinemachine();
        }
        ResetCameraState();
        s_firstFrame = true;
        if (s_initialRootCaptured && g_camSetPos && g_cachedAnimator) {
          void *rootT = SafeGetComponentTransform(g_cachedAnimator);
          if (rootT) {
            g_camSetPos(rootT, s_initialRootPos);
          }
        }
        s_initialRootCaptured = false;
        if (g_audioPlayer) g_audioPlayer->Stop();
        g_audioIsClock = false;
        g_audioPendingStart = false;
      }
      g_muscleAnimPath[0] = '\0';
      if (g_muscleAnim) g_muscleAnim->loaded = false;
      g_mmdHasMuscles = false;
      g_mmdHasArmBones = false;
      g_mmdHasFingerBones = false;
      return 0;
    }
    case (WM_USER + 111): {
      Log("[GUI-CMD] Unload camera VMD");
      g_cameraVmdPath[0] = '\0';
      g_cameraOverrideExplicit = false;
      if (g_motionBackend.Is(MotionBackend::DirectVmd)) {
        if (g_cameraVmd) {
          FreeVmd(g_cameraVmd);
          g_cameraVmd = nullptr;
        }
        g_cameraPlayer.SetVmd(nullptr);
        DirectVmdRuntime_RequestCameraOverride(nullptr);
        Log("[P6-CAMERA-OVERRIDE] request=clear "
            "fallback=direct-vmd-camera tid=%lu",
            GetCurrentThreadId());
        return 0;
      }
      if (g_cameraVmd) {
        FreeVmd(g_cameraVmd);
        g_cameraVmd = nullptr;
      }
      g_cameraPlayer.SetVmd(nullptr);
      if (g_cameraActive) {
        RestoreCinemachine();
      }
      ResetCameraState();
      return 0;
    }
    case (WM_USER + 112): {
      Log("[GUI-CMD] Unload Foot IK VMD");
      g_footIkVmdPath[0] = '\0';
      if (g_footIkVmd) {
        FreeVmd(g_footIkVmd);
        g_footIkVmd = nullptr;
      }
      g_footIkResolved = true;
      ClearActiveFootIKSolverWeights();
      ResetFootIKRuntimeState();
      s_footIKFirstCaptured = false;
      s_initialRootCaptured = false;
      s_footIKCalibrated = false;
      return 0;
    }
    case (WM_USER + 113): {
      Log("[GUI-CMD] Unload morph/face VMD");
      g_morphVmdPath[0] = '\0';
      if (GuiSelectedMotionBackend() == MotionBackend::DirectVmd ||
          g_motionBackend.Is(MotionBackend::DirectVmd)) {
        DirectVmdRuntime_RequestMorphOverride(nullptr);
        Log("[P6-MORPH-OVERRIDE] request=clear "
            "fallback=direct-vmd-morph tid=%lu",
            GetCurrentThreadId());
      }
      if (g_vmd) {
        FreeVmd(g_vmd);
        g_vmd = nullptr;
      }
      g_bsIndicesResolved = true;
      g_mouthWeightsFromMuscle = false;
      memset((void *)g_mouthWeights, 0, sizeof(g_mouthWeights));
      for (int i = 0; i < NUM_EXTRA_MORPHS; i++) {
        g_extraMorphs[i].weight = 0;
        g_extraMorphs[i].prevWeight = 0;
      }
      RestoreBigList();
      memset((void *)g_faceBoneTouched, 0, sizeof(g_faceBoneTouched));
      if (g_confirmedSMC && OFF_allMorphBoneDirty > 0) {
        *(bool *)((char *)g_confirmedSMC + OFF_allMorphBoneDirty) = true;
      }
      return 0;
    }
    case (WM_USER + 114): {
      Log("[GUI-CMD] Unload audio");
      g_audioPath[0] = '\0';
      g_audioPathW[0] = L'\0';
      DirectVmdAudio_ResetMainThread("audio-unload", false);
      if (g_audioPlayer) {
        g_audioPlayer->Stop();
        g_audioPlayer->Close();
      }
      g_audioIsClock = false;
      g_audioPendingStart = false;
      return 0;
    }
    case (WM_USER + 115): {
      Log("[GUI-CMD] Load camera VMD: %s", g_cameraVmdPath);
      if (GuiSelectedMotionBackend() == MotionBackend::DirectVmd ||
          g_motionBackend.Is(MotionBackend::DirectVmd)) {
        g_cameraOverrideExplicit = g_cameraVmdPath[0] != '\0';
        DirectVmdRuntime_RequestCameraOverride(
            g_cameraOverrideExplicit ? g_cameraVmdPath : nullptr);
        Log("[P6-CAMERA-OVERRIDE] request=%s path='%s' tid=%lu",
            g_cameraOverrideExplicit ? "load" : "clear",
            g_cameraVmdPath, GetCurrentThreadId());
        return 0;
      }
      if (g_cameraVmd) {
        FreeVmd(g_cameraVmd);
        g_cameraVmd = nullptr;
      }
      if (g_cameraVmdPath[0] != '\0') {
        g_cameraVmd = LoadVmd(g_cameraVmdPath);
        if (g_cameraVmd && g_cameraVmd->loaded && !g_cameraVmd->cameraKeys.empty()) {
          g_cameraPlayer.SetVmd(g_cameraVmd);
          Log("[GUI-CMD] Camera VMD loaded: %zu keys, %zu warning(s)",
              g_cameraVmd->cameraKeys.size(), g_cameraVmd->warnings.size());
        } else {
          Log("[GUI-CMD] Camera VMD load failed: %s error='%s'",
              g_cameraVmdPath,
              (g_cameraVmd && !g_cameraVmd->error.empty())
                  ? g_cameraVmd->error.c_str()
                  : "no camera keyframes");
        }
      }
      return 0;
    }
    case (WM_USER + 116): {
      Log("[GUI-CMD] Load Foot IK VMD: %s", g_footIkVmdPath);
      if (g_footIkVmd) {
        FreeVmd(g_footIkVmd);
        g_footIkVmd = nullptr;
      }
      g_footIkResolved = false;
      s_footIKFirstCaptured = false;
      s_initialRootCaptured = false;
      s_footIKCalibrated = false;
      if (g_footIkVmdPath[0] != '\0') {
        g_footIkVmd = LoadVmd(g_footIkVmdPath);
        if (g_footIkVmd && g_footIkVmd->loaded && !g_footIkVmd->boneTimelines.empty()) {
          g_footIkResolved = true;
          Log("[GUI-CMD] Foot IK VMD loaded: %zu bone timelines", g_footIkVmd->boneTimelines.size());
        } else {
          Log("[GUI-CMD] Foot IK VMD load failed: %s", g_footIkVmdPath);
        }
      }
      return 0;
    }
    case (WM_USER + 117): {
      Log("[GUI-CMD] Load morph/face VMD: %s", g_morphVmdPath);
      if (GuiSelectedMotionBackend() == MotionBackend::DirectVmd ||
          g_motionBackend.Is(MotionBackend::DirectVmd)) {
        DirectVmdRuntime_RequestMorphOverride(
            g_morphVmdPath[0] ? g_morphVmdPath : nullptr);
        Log("[P6-MORPH-OVERRIDE] request=%s path='%s' tid=%lu",
            g_morphVmdPath[0] ? "load" : "clear", g_morphVmdPath,
            GetCurrentThreadId());
        return 0;
      }
      if (g_vmd) {
        FreeVmd(g_vmd);
        g_vmd = nullptr;
      }
      g_bsIndicesResolved = false;
      if (g_morphVmdPath[0] != '\0') {
        g_vmd = LoadVmd(g_morphVmdPath);
        if (g_vmd && g_vmd->loaded && !g_vmd->morphTimelines.empty()) {
          g_bsIndicesResolved = true;
          Log("[GUI-CMD] Morph VMD loaded: %zu morph timelines", g_vmd->morphTimelines.size());
        } else {
          Log("[GUI-CMD] Morph VMD load failed: %s", g_morphVmdPath);
        }
      }
      return 0;
    }
    }
  }
  LRESULT r = CallWindowProcW(g_origWndProc, hwnd, msg, wParam, lParam);

  if (g_guiVisible) {
    if (msg == WM_LBUTTONDOWN || msg == WM_RBUTTONDOWN ||
        msg == WM_MBUTTONDOWN || msg == WM_ACTIVATE ||
        msg == WM_SETFOCUS || msg == WM_KILLFOCUS) {
      if (g_guiHwnd) {
        SetWindowPos(g_guiHwnd, HWND_TOPMOST, 0, 0, 0, 0,
                     SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
      }
      if (g_cursorShowAction && g_actionInvokeMethod) {
        SafeInvokeCursorAction(g_cursorShowAction);
      }
    }
  }

  return r;
}

static void MuscleAnimationTick() {
  if (!g_motionBackend.Is(MotionBackend::Muscle) ||
      !g_musclePlayer ||
      !g_trojanActive)
    return;
  if (!g_muscleAnim || !g_muscleAnim->loaded)
    return;

  const bool clothHold = g_clothPlaybackGate.Holding(
      static_cast<uint32_t>(MotionBackend::Muscle),g_motionBackend.Generation());
  static eiem_playback::TimelineAdmission clothTimeline;
  if(clothHold!=clothTimeline.wasHeld)
    Log("[CLOTH-PLAYBACK-CLOCK] backend=Muscle held=%d frame=%.6f generation=%llu playing=%d elapsedCatchup=0",
        int(clothHold),g_musclePlayer->currentTime*30.,g_motionBackend.Generation(),int(g_musclePlayer->playing));
  if (clothTimeline.HoldElapsed(clothHold)) QueryPerformanceCounter(&g_musclePlayer->lastTick);

  if (g_mmdPendingApply)
    return;

  float prevTime = g_musclePlayer->currentTime;
  float frameNum = 0.0f;
  if (g_musclePlayer->playing && !clothHold) {
    frameNum = g_musclePlayer->Tick();
  } else {
    frameNum = g_musclePlayer->currentTime * 30.0f;
  }

  if (!clothHold && g_musclePlayer->playing && g_audioPendingStart && g_audioPlayer && g_audioPlayer->loaded) {
    float expectedAudio = g_musclePlayer->currentTime + g_audioOffset;
    if (expectedAudio >= 0.0f) {
      g_audioPendingStart = false;
      if (g_gameHwnd) {
        int motionMs = (int)(g_musclePlayer->currentTime * 1000.0f);
        PostMessageW(g_gameHwnd, WM_USER + 108, (WPARAM)motionMs, 0);
        Log("[AUDIO] Deferred start: posting seek motion=%d ms", motionMs);
      }
    }
  }

  if (!clothHold && g_audioIsClock && g_audioPlayer && g_audioPlayer->loaded) {
    if (fabsf(g_musclePlayer->speed - 1.0f) > 0.001f) {
      g_audioPlayer->Pause();
      g_audioIsClock = false;
      Log("[AUDIO] Speed != 1.0, audio sync disabled");
    } else {
      int posMs = g_audioPlayer->GetPositionMs();
      if (posMs > 0) {
        float audioSec = posMs / 1000.0f - g_audioOffset;
        float drift = audioSec - g_musclePlayer->currentTime;

        if (fabsf(drift) > 0.15f) {
          g_musclePlayer->currentTime = audioSec;
        } else if (fabsf(drift) > 0.005f) {
          g_musclePlayer->currentTime += drift * 0.1f;
        }
        if (g_musclePlayer->currentTime < 0) g_musclePlayer->currentTime = 0;
        frameNum = g_musclePlayer->currentTime * 30.0f;
      }

      if (g_musclePlayer->loop && prevTime > 0.5f &&
          g_musclePlayer->currentTime < 0.1f) {
        if (g_audioOffset < 0.0f) {
          g_audioPendingStart = true;
          g_audioIsClock = false;
          Log("[AUDIO] Loop restart deferred (negative offset)");
        } else if (g_gameHwnd) {
          PostMessageW(g_gameHwnd, WM_USER + 108, (WPARAM)0, 0);
          Log("[AUDIO] Loop restart posted");
        }
      }
    }
  }

  float timeSec =
      frameNum / g_muscleAnim->fps;
  MuscleFrame mf = g_muscleAnim->GetFrame(timeSec);

  memcpy((void *)g_mmdMuscles, mf.muscles, 95 * sizeof(float));
  memcpy((void *)g_mmdBodyPos, mf.bodyPos, 3 * sizeof(float));
  memcpy((void *)g_mmdBodyRot, mf.bodyRot, 4 * sizeof(float));
  if (g_muscleAnim->hasArmBones) {
    memcpy((void *)g_mmdArmBoneRots, mf.armBoneRots,
           ARM_BONE_COUNT * 4 * sizeof(float));
    g_mmdHasArmBones = true;
  }
  if (g_muscleAnim->hasFingerBones) {
    memcpy((void *)g_mmdFingerBoneRots, mf.fingerBoneRots,
           FINGER_BONE_COUNT * 4 * sizeof(float));
    g_mmdHasFingerBones = true;
  }
  g_mmdHasMuscles = true;

  if (!g_footIkVmd && !g_footIkResolved) {
    g_footIkResolved = true;

    if (g_footIkVmdPath[0] != '\0') {
      VmdFile *v = LoadVmd(g_footIkVmdPath);
      if (v && v->loaded && !v->boneTimelines.empty()) {
        g_footIkVmd = v;
        Log("[FOOT-IK] User Foot IK VMD loaded: %s (%zu bone timelines)",
            g_footIkVmdPath, v->boneTimelines.size());
      } else {
        Log("[FOOT-IK] User Foot IK VMD load failed: %s", g_footIkVmdPath);
        if (v) FreeVmd(v);
      }
    }
  }

  if (!g_vmd && !g_bsIndicesResolved) {
    g_bsIndicesResolved = true;

    if (g_morphVmdPath[0] != '\0') {
      VmdFile *v = LoadVmd(g_morphVmdPath);
      if (v && v->loaded && !v->morphTimelines.empty()) {
        g_vmd = v;
        Log("[MOUTH] User morph VMD loaded: %s (%d morph timelines)",
            g_morphVmdPath, (int)v->morphTimelines.size());
      } else {
        Log("[MOUTH] User morph VMD load failed or no morphs: %s", g_morphVmdPath);
        if (v) FreeVmd(v);
      }
    }
  }

  if (g_vmd && g_vmd->loaded && !g_vmd->morphTimelines.empty()) {
    static const char *morphNames[5] = {
        "\xe3\x81\x82",
        "\xe3\x81\x84",
        "\xe3\x81\x86",
        "\xe3\x81\x88",
        "\xe3\x81\x8a",
    };

    static bool s_morphMapped = false;
    if (!s_morphMapped) {
      s_morphMapped = true;
      static const char *labelNames[5] = {"A", "I", "U", "E", "O"};
      Log("[MOUTH] VMD morph timelines: %d total",
          (int)g_vmd->morphTimelines.size());
      for (int i = 0; i < 5; i++) {
        auto it = g_vmd->morphTimelines.find(morphNames[i]);
        if (it != g_vmd->morphTimelines.end()) {
          float peak = 0;
          for (auto &k : it->second.keys)
            if (k.weight > peak)
              peak = k.weight;
          Log("[MOUTH]   %s: %d keys (peak=%.2f)", labelNames[i],
              (int)it->second.keys.size(), peak);
        } else {
          Log("[MOUTH]   %s: NOT FOUND in VMD", labelNames[i]);
        }
      }

      for (auto &pair : g_vmd->morphTimelines) {
        float peak = 0;
        float firstW = pair.second.keys.empty() ? 0 : pair.second.keys.front().weight;
        for (auto &k : pair.second.keys)
          if (k.weight > peak) peak = k.weight;
        Log("[MOUTH-ALL]   %s: %d keys (peak=%.2f, frame0=%.2f)",
            pair.first.c_str(), (int)pair.second.keys.size(), peak, firstW);
      }
    }

    static float s_prevMouthWeights[5] = {};
    static float s_mouthAlpha =
        0.25f;

    for (int i = 0; i < 5; i++) {
      float target = 0.0f;
      auto it = g_vmd->morphTimelines.find(morphNames[i]);
      if (it != g_vmd->morphTimelines.end()) {
        target = it->second.Sample(frameNum);
      }
      float smoothed = s_prevMouthWeights[i] +
                       s_mouthAlpha * (target - s_prevMouthWeights[i]);
      s_prevMouthWeights[i] = smoothed;
      g_mouthWeights[i] = smoothed;
    }
    g_mouthWeightsFromMuscle = true;

    static float s_extraAlpha = 0.25f;
    for (int em = 0; em < NUM_EXTRA_MORPHS; em++) {
      float target = 0.0f;
      auto it = g_vmd->morphTimelines.find(g_extraMorphs[em].vmdNameUtf8);
      if (it != g_vmd->morphTimelines.end()) {
        target = it->second.Sample(frameNum);
      }
      float smoothed = g_extraMorphs[em].prevWeight +
                       s_extraAlpha * (target - g_extraMorphs[em].prevWeight);
      g_extraMorphs[em].prevWeight = smoothed;
      g_extraMorphs[em].weight = smoothed;
    }
  }

  if (g_gameHwnd) {
    g_mmdPendingApply = true;
    PostMessageW(g_gameHwnd, WM_MMD_APPLY_POSE, 0, 0);
  }

  static bool s_logged = false;
  if (!s_logged) {
    Log("[MUSCLE] Hotkey thread: pure SetHumanPose mode. m[0]=%.3f t=%.2f "
        "ready=%d",
        mf.muscles[0], timeSec, s_poseReady ? 1 : 0);
    s_logged = true;
  }
}

static void AnimationTick() {
  if (!g_motionBackend.Is(MotionBackend::Native))
    return;
  if (g_calibMode) {
    SafeSetAnimatorEnabled(false);
    CalibrationTick();
    return;
  }
  if (!g_player || !g_player->playing)
    return;
  if (!g_vmd || !g_vmd->loaded)
    return;
  if (!g_resolvedMappings || g_resolvedMappings->empty())
    return;

  SafeSetAnimatorEnabled(false);

  float frame = g_player->Tick();

  FILE *dumpF = nullptr;
  if (!s_dumpedFrame0 && frame < 1.0f) {
    s_dumpedFrame0 = true;
    dumpF = fopen("plugin\\eiem_frame0.txt", "w");
    if (dumpF)
      fprintf(dumpF, "=== VMD Frame 0 Dump ===\n\n");
  }

  for (auto &rm : *g_resolvedMappings) {
    if (!rm.valid || !rm.transform)
      continue;
    CaptureBindPose(rm);
    auto it = g_vmd->boneTimelines.find(rm.mmdName);
    if (it == g_vmd->boneTimelines.end())
      continue;
    InterpResult interp =
        InterpolateBone(it->second.keys, frame, rm.isPositionBone);

    Quat raw = interp.rotation;
    Quat R_bip = rm.hasBind ? Quat{rm.bindRot[0], rm.bindRot[1], rm.bindRot[2],
                                   rm.bindRot[3]}
                            : Quat{0, 0, 0, 1};
    Quat R_mmd = GetMmdRestRot(rm.humanBone);

    Quat result;
    int mode = g_corrMode % g_corrCount;
    switch (mode) {
    case 0: {
      result = QuatMul(QuatMul(R_bip, QuatInv(R_mmd)), raw);
    } break;
    case 1: {
      Quat vmd_flipped = {raw.x, raw.y, -raw.z, -raw.w};
      result = QuatMul(QuatMul(R_bip, QuatInv(R_mmd)), vmd_flipped);
    } break;
    case 2: {
      Quat vmd_flipped = {-raw.x, -raw.y, raw.z, raw.w};
      result = QuatMul(QuatMul(R_bip, QuatInv(R_mmd)), vmd_flipped);
    } break;
    case 3: {
      result = QuatMul(raw, QuatMul(R_bip, QuatInv(R_mmd)));
    } break;
    case 4: {
      result = QuatMul(QuatMul(QuatInv(R_mmd), R_bip), raw);
    } break;
    case 5: {
      static const Quat G = {0.5f, -0.5f, 0.5f, 0.5f};
      static const Quat Ginv = {-0.5f, 0.5f, -0.5f, 0.5f};
      Quat sim_raw = QuatMul(QuatMul(G, raw), Ginv);
      result = QuatMul(QuatMul(R_bip, QuatInv(R_mmd)), sim_raw);
    } break;
    case 6: {
      static const Quat G = {0.5f, -0.5f, 0.5f, 0.5f};
      static const Quat Ginv = {-0.5f, 0.5f, -0.5f, 0.5f};
      result = QuatMul(R_bip, QuatMul(QuatMul(G, raw), Ginv));
    } break;
    case 7: {
      result = R_bip;
    } break;
    default:
      result = QuatMul(QuatMul(R_bip, QuatInv(R_mmd)), raw);
      break;
    }
    SafeSetLocalRotation(rm.transform, result);

    if (interp.hasPosition) {
      Vec3 vmdPos = MmdPosToUnity(interp.position);
      Vec3 fp = {rm.bindPos[0] + vmdPos.x, rm.bindPos[1] + vmdPos.y,
                 rm.bindPos[2] + vmdPos.z};
      SafeSetLocalPosition(rm.transform, fp);
    }
    if (dumpF) {
      fprintf(dumpF,
              "%-20s hb=%2d  vmd(%7.4f,%7.4f,%7.4f,%7.4f)  "
              "R_bip(%7.4f,%7.4f,%7.4f,%7.4f)  R_mmd(%7.4f,%7.4f,%7.4f,%7.4f)  "
              "out(%7.4f,%7.4f,%7.4f,%7.4f)  mode=%d\n",
              rm.mmdName.c_str(), rm.humanBone, raw.x, raw.y, raw.z, raw.w,
              R_bip.x, R_bip.y, R_bip.z, R_bip.w, R_mmd.x, R_mmd.y, R_mmd.z,
              R_mmd.w, result.x, result.y, result.z, result.w, mode);
    }
  }
  if (dumpF) {
    fclose(dumpF);
    dumpF = nullptr;
  }
}

