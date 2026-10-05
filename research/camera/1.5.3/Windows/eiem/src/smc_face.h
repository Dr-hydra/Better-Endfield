#pragma once

static bool GhostRig_GetDirectChannelIdentity(
    uint64_t *generation, uintptr_t *ownerCharacter);
static bool GhostRig_RequireMainThread(const char *stage, bool allowCapture);

typedef void (*NativeSetPos_t)(void *transform, float *vec3);
typedef void (*NativeSetRot_t)(void *transform, float *quat);
static NativeSetPos_t g_nativeSetPos = nullptr;
static NativeSetRot_t g_nativeSetRot = nullptr;
static bool g_directVmdMorphActive = false;
static void *g_confirmedSMC = nullptr;

static volatile bool g_applyBoneDone = false;
static void __fastcall Hooked_ApplyBoneTrans(void *__this, bool param1,
                                             float param2, uint64_t jobHandle,
                                             void *methodInfo) {
  if (g_origApplyBoneTrans)
    g_origApplyBoneTrans(__this, param1, param2, jobHandle, methodInfo);
  g_applyBoneDone = true;

  if (!(g_faceTestActive || g_directVmdMorphActive) ||
      !g_faceBonesCaptured || !g_nativeSetPos ||
      !g_nativeSetRot)
    return;

  __try {
    for (int i = 0; i < g_faceBoneCount; i++) {
      if (!g_faceBoneRefs[i])
        continue;
      if (g_directVmdMorphActive && !g_faceTestActive &&
          !g_faceBoneTouched[i])
        continue;
      float pos[3] = {g_faceBones[i].px, g_faceBones[i].py, g_faceBones[i].pz};
      float rot[4] = {g_faceBones[i].rx, g_faceBones[i].ry, g_faceBones[i].rz,
                      g_faceBones[i].rw};
      g_nativeSetPos(g_faceBoneRefs[i], pos);
      g_nativeSetRot(g_faceBoneRefs[i], rot);
    }
  } __except (1) {
  }
}

static void ResolveSMCOffsets(void *smcClass) {
  if (g_smcOffsetsResolved || !smcClass)
    return;
  g_smcOffsetsResolved = true;

  auto getOff = [&](const char *name) -> int {
    void *fiter = nullptr;
    void *field;
    while ((field = il2cpp_class_get_fields(smcClass, &fiter))) {
      const char *fn = il2cpp_field_get_name(field);
      if (fn && strcmp(fn, name) == 0) {
        return (int)il2cpp_field_get_offset(field);
      }
    }
    return -1;
  };

  OFF_allMorphs = getOff("m_allMorphs");
  OFF_poseCache = getOff("m_allMorphsPoseCache");
  OFF_bigList = getOff("m_morphNameHashToMorphDataBoneBigList");
  OFF_nativeHashMap = getOff("m_morphNameHashToMorphData");
  OFF_shaderProps = getOff("m_allShaderProps");
  OFF_baseShaderProps = getOff("m_baseShaderProps");
  OFF_dirtyShaderProps = getOff("m_dirtyShaderProps");
  OFF_morphBSDirty = getOff("m_morphBSDirty");
  OFF_allMorphBoneDirty = getOff("m_allMorphBoneDirty");
  OFF_avatarData = getOff("m_avatarData");
  if (OFF_avatarData < 0)
    OFF_avatarData = getOff("morphData");
  OFF_allBonesTransforms = getOff("m_allBonesTransforms");
  OFF_boneIDToIdx = getOff("m_boneIDToIdx");
  OFF_phonemesWeights = getOff("m_phonemesWeights");
  OFF_mainEmotion = getOff("m_mainEmotion");
  OFF_poseDictMorph = getOff("m_poseDictMorph");
  OFF_microExprWeights = getOff("m_microExpressionWeights");
  OFF_smcEyeLookAt = getOff("m_isEyeLookAtIKEnable");
  if (OFF_smcEyeLookAt < 0) OFF_smcEyeLookAt = getOff("enableEyeLookAtIK");
  if (OFF_smcEyeLookAt < 0) OFF_smcEyeLookAt = getOff("m_enableEyeLookAtIK");
  if (OFF_smcEyeLookAt < 0) OFF_smcEyeLookAt = getOff("_enableEyeLookAtIK");

  Log("[OFFSET] allMorphs=%d poseCache=%d bigList=%d hashMap=%d", OFF_allMorphs,
      OFF_poseCache, OFF_bigList, OFF_nativeHashMap);
  Log("[OFFSET] shaderProps=%d baseShaderProps=%d dirtyShaderProps=%d",
      OFF_shaderProps, OFF_baseShaderProps, OFF_dirtyShaderProps);
  Log("[OFFSET] morphBSDirty=%d allMorphBoneDirty=%d", OFF_morphBSDirty,
      OFF_allMorphBoneDirty);
  Log("[OFFSET] avatarData=%d bonesTransforms=%d boneIDToIdx=%d phonemes=%d "
      "mainEmo=%d",
      OFF_avatarData, OFF_allBonesTransforms, OFF_boneIDToIdx,
      OFF_phonemesWeights, OFF_mainEmotion);
  Log("[OFFSET] poseDictMorph=%d microExprWeights=%d", OFF_poseDictMorph,
      OFF_microExprWeights);
}

struct MouthShapeInfo {
  const char *name;
  int nameHash;
  int morphId;
  int startIdx;
  int count;
  int jobStartIdx;
  int jobCount;
  bool resolved;
};

static MouthShapeInfo g_mouthShapes[] = {
    {"A", 299073642, -1, -1, -1, -1, -1, false},
    {"I", 1271943943, -1, -1, -1, -1, -1, false},
    {"U", 1701661734, -1, -1, -1, -1, -1, false},
    {"E", -781522180, -1, -1, -1, -1, -1, false},
    {"O", -348812070, -1, -1, -1, -1, -1, false},
};
static const int NUM_MOUTH_SHAPES = 5;
static float g_mouthWeights[5] = {};
static bool g_mouthShapesResolved =
    false;

static bool g_bigListCaptured = false;
static bool g_hashCorrelationDone = false;
static bool g_smcResetRequested = false;

struct ExtraMorphTarget {
  const char *endfieldName;
  int nameHash;
  int startIdx;
  int count;
  int jobStartIdx;
  int jobCount;
  bool resolved;
};

struct ExtraMorph {
  const char *vmdNameUtf8;
  const char *label;
  ExtraMorphTarget targets[4];
  int targetCount;
  float weight;
  float prevWeight;
};


static ExtraMorph g_extraMorphs[] = {
    {"\xe3\x81\xbe\xe3\x81\xb0\xe3\x81\x9f\xe3\x81\x8d",
     "blink",
     {{"eye_thinkcloseeyes_a_R_ctrl", 0, -1, -1, false},
      {"eye_thinkcloseeyes_a_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
    {"\xe7\xac\x91\xe3\x81\x84",
     "smile_eye",
     {{"eye_relax_a_R_ctrl", 0, -1, -1, false},
      {"eye_relax_a_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
    {"\xe3\x82\xa6\xe3\x82\xa3\xe3\x83\xb3\xe3\x82\xaf",
     "wink_L",
     {{"eye_thinkcloseeyes_a_L_ctrl", 0, -1, -1, false}},
     1,
     0,
     0},
    {"\xe3\x82\xa6\xe3\x82\xa3\xe3\x83\xb3\xe3\x82\xaf\xe5\x8f\xb3",
     "wink_R",
     {{"eye_thinkcloseeyes_a_R_ctrl", 0, -1, -1, false}},
     1,
     0,
     0},
    {"\xe3\x81\xaa\xe3\x81\x94\xe3\x81\xbf",
     "nagomi",
     {{"eye_relax_a_R_ctrl", 0, -1, -1, false},
      {"eye_relax_a_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
    {"\xe3\x81\xb3\xe3\x81\xa3\xe3\x81\x8f\xe3\x82\x8a",
     "surprise_eye",
     {{"eye_relax_a_R_ctrl", 0, -1, -1, false},
      {"eye_relax_a_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},

    {"\xe4\xb8\x8a",
     "brow_up",
     {{"brow_offset_u_R_ctrl", 0, -1, -1, false},
      {"brow_offset_u_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
    {"\xe4\xb8\x8b",
     "brow_down",
     {{"brow_offset_d_R_ctrl", 0, -1, -1, false},
      {"brow_offset_d_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
    {"\xe6\x80\x92\xe3\x82\x8a",
     "brow_angry",
     {{"brow_attack_a_R_ctrl", 0, -1, -1, false},
      {"brow_attack_a_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
    {"\xe5\x9b\xb0\xe3\x82\x8b",
     "brow_sad",
     {{"brow_relax_a_R_ctrl", 0, -1, -1, false},
      {"brow_relax_a_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
    {"\xe3\x81\xab\xe3\x81\x93\xe3\x82\x8a",
     "brow_smile",
     {{"brow_relax_a_R_ctrl", 0, -1, -1, false},
      {"brow_relax_a_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
    {"\xe3\x82\xa6\xe3\x82\xa3\xe3\x83\xb3\xe3\x82\xaf\xef\xbc\x92",
     "wink2_L",
     {{"eye_thinkcloseeyes_a_L_ctrl", 0, -1, -1, false}},
     1,
     0,
     0},
    {"\xe3\x82\xa6\xe3\x82\xa3\xe3\x83\xb3\xe3\x82\xaf\xef\xbc\x92\xe5\x8f\xb3",
     "wink2_R",
     {{"eye_thinkcloseeyes_a_R_ctrl", 0, -1, -1, false}},
     1,
     0,
     0},
    {"\xef\xbd\xb3\xef\xbd\xa8\xef\xbe\x9d\xef\xbd\xb8\xef\xbc\x92\xe5\x8f\xb3",
     "wink2_R_half",
     {{"eye_thinkcloseeyes_a_R_ctrl", 0, -1, -1, false}},
     1,
     0,
     0},
    {"\xe6\x82\xb2\xe3\x81\x97\xe3\x81\x84",
     "sad_eye",
     {{"eye_relax_a_R_ctrl", 0, -1, -1, false},
      {"eye_relax_a_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
    {"\xe7\x9c\x9f\xe9\x9d\xa2\xe7\x9b\xae",
     "serious",
     {{"brow_attack_a_R_ctrl", 0, -1, -1, false},
      {"brow_attack_a_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
    {"\xe5\x89\x8d",
     "forward",
     {{"brow_offset_d_R_ctrl", 0, -1, -1, false},
      {"brow_offset_d_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
    {"\xe3\x81\x98\xe3\x83\xbc\xe3\x81\xa3",
     "stare",
     {{"eye_attack_a_R_ctrl", 0, -1, -1, false},
      {"eye_attack_a_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
    {"\xe3\x81\xaf\xe3\x81\x85",
     "hau",
     {{"eye_thinkcloseeyes_a_R_ctrl", 0, -1, -1, false},
      {"eye_thinkcloseeyes_a_L_ctrl", 0, -1, -1, false}},
     2,
     0,
     0},
};
static const int NUM_EXTRA_MORPHS =
    sizeof(g_extraMorphs) / sizeof(g_extraMorphs[0]);
static bool g_extraMorphsResolved = false;

static int g_bsIdxA = -1, g_bsIdxI = -1, g_bsIdxU = -1, g_bsIdxE = -1,
           g_bsIdxO = -1;
static bool g_bsIndicesResolved = false;
static volatile bool g_mouthWeightsFromMuscle =
    false;

static bool FaceMorphOwnershipActive() {
  return g_faceTestActive || g_mouthWeightsFromMuscle ||
         g_directVmdMorphActive;
}

static float DirectVmdMorph_ClampWeight(float weight) {
  if (!std::isfinite(weight))
    return 0.0f;
  return (std::max)(0.0f, (std::min)(weight, 1.0f));
}

static void DirectVmdMorph_ResetMainThread(const char *reason) {
  if (!GhostRig_RequireMainThread("P6.Morph.Reset", true))
    return;
  const bool wasActive = g_directVmdMorphActive;
  g_directVmdMorphActive = false;
  memset(g_mouthWeights, 0, sizeof(g_mouthWeights));
  for (int index = 0; index < NUM_EXTRA_MORPHS; ++index) {
    g_extraMorphs[index].weight = 0.0f;
    g_extraMorphs[index].prevWeight = 0.0f;
  }
  memset(g_faceBoneTouched, 0, sizeof(g_faceBoneTouched));
  if (g_confirmedSMC && OFF_allMorphBoneDirty > 0) {
    __try {
      *(bool *)((char *)g_confirmedSMC + OFF_allMorphBoneDirty) = true;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
    }
  }
  if (wasActive) {
    Log("[P6-MORPH-RESET] reason=%s weightsCleared=1 touchedCleared=1 "
        "tid=%lu",
        reason ? reason : "unspecified", GetCurrentThreadId());
  }
}

static bool DirectVmdMorph_IsKnownName(const char *name) {
  if (!name)
    return false;
  static constexpr const char *mouthNames[NUM_MOUTH_SHAPES] = {
      u8"あ", u8"い", u8"う", u8"え", u8"お"};
  for (int index = 0; index < NUM_MOUTH_SHAPES; ++index) {
    if (strcmp(name, mouthNames[index]) == 0)
      return true;
  }
  for (int index = 0; index < NUM_EXTRA_MORPHS; ++index) {
    if (strcmp(name, g_extraMorphs[index].vmdNameUtf8) == 0)
      return true;
  }
  return false;
}

static void DirectVmdMorph_UpdateFromRuntime(int smcFrame) {
  if (!g_motionBackend.Is(MotionBackend::DirectVmd) ||
      !DirectVmdRuntime_IsLoaded()) {
    if (g_directVmdMorphActive)
      DirectVmdMorph_ResetMainThread("backend-or-resource-inactive");
    return;
  }
  if (!GhostRig_RequireMainThread("P6.Morph.Update", false))
    return;

  DirectVmdSampleFrame frame;
  const bool copied = DirectVmdRuntime_CopyLatestFrame(&frame);
  const uint64_t targetGeneration =
      DirectVmdRuntime_PublicTargetGeneration();
  const uintptr_t targetOwner = DirectVmdRuntime_PublicTargetOwner();
  uint64_t ghostGeneration = 0;
  uintptr_t ghostOwner = 0;
  const bool ghostIdentityValid = GhostRig_GetDirectChannelIdentity(
      &ghostGeneration, &ghostOwner);
  const bool accepted = copied && frame.valid && ghostIdentityValid &&
      frame.rigGeneration == targetGeneration &&
      frame.ownerCharacter == targetOwner && targetOwner != 0 &&
      frame.clipGeneration == DirectVmdRuntime_PublicClipGeneration() &&
      frame.rigGeneration == ghostGeneration &&
      frame.ownerCharacter == ghostOwner;
  if (!accepted) {
    if (g_directVmdMorphActive)
      DirectVmdMorph_ResetMainThread("sample-ownership-mismatch");
    return;
  }

  static constexpr const char *mouthNames[NUM_MOUTH_SHAPES] = {
      u8"あ", u8"い", u8"う", u8"え", u8"お"};
  int mappedTracks = 0;
  for (int index = 0; index < NUM_MOUTH_SHAPES; ++index) {
    float weight = 0.0f;
    if (DirectVmdFindMorphWeight(frame, mouthNames[index], &weight))
      ++mappedTracks;
    g_mouthWeights[index] = DirectVmdMorph_ClampWeight(weight);
  }
  for (int index = 0; index < NUM_EXTRA_MORPHS; ++index) {
    float weight = 0.0f;
    if (DirectVmdFindMorphWeight(
            frame, g_extraMorphs[index].vmdNameUtf8, &weight))
      ++mappedTracks;
    const float exact = DirectVmdMorph_ClampWeight(weight);
    g_extraMorphs[index].weight = exact;
    g_extraMorphs[index].prevWeight = exact;
  }

  int unknownTracks = 0;
  const uint32_t morphCount =
      (std::min)(frame.morphCount, DIRECT_VMD_MAX_MORPH_CHANNELS);
  for (uint32_t index = 0; index < morphCount; ++index) {
    if (!DirectVmdMorph_IsKnownName(frame.morphs[index].name))
      ++unknownTracks;
  }
  const bool wasActive = g_directVmdMorphActive;
  g_directVmdMorphActive = mappedTracks > 0;
  if (!g_directVmdMorphActive && wasActive) {
    memset(g_faceBoneTouched, 0, sizeof(g_faceBoneTouched));
    if (g_confirmedSMC && OFF_allMorphBoneDirty > 0) {
      __try {
        *(bool *)((char *)g_confirmedSMC + OFF_allMorphBoneDirty) = true;
      } __except (EXCEPTION_EXECUTE_HANDLER) {
      }
    }
  }

  if (smcFrame <= 1 || smcFrame % 120 == 0) {
    Log("[P6-MORPH-SAMPLE] smcFrame=%d sourceFrame=%.6f sampleSeq=%llu "
        "playback=%u tracks=%u mapped=%d unknown=%d dropped=%u active=%d "
        "generation=%llu owner=%p exactFrameSampling=1 muscleTimeline=0 "
        "smoothing=0 tid=%lu",
        smcFrame, frame.sourceFrame,
        (unsigned long long)frame.sequence,
        static_cast<unsigned>(frame.playback), frame.morphCount,
        mappedTracks, unknownTracks, frame.morphDroppedCount,
        g_directVmdMorphActive ? 1 : 0,
        (unsigned long long)frame.rigGeneration,
        reinterpret_cast<void *>(frame.ownerCharacter),
        GetCurrentThreadId());
  }
}

static void ResolveMouthShapes(char *smcBase) {
  if (g_mouthShapesResolved)
    return;

  __try {
    int hmOff = SafeOff(OFF_nativeHashMap, 0xF8, "nativeHashMap");
    void *hmBuffer = *(void **)(smcBase + hmOff);
    if (!hmBuffer) {
      Log("[MOUTH-RESOLVE] No hmBuffer at SMC+0x%X", hmOff);
      return;
    }

    int *hmData = (int *)hmBuffer;
    int keyCapacity = hmData[8];
    int bucketCap = hmData[9];
    int allocLen = hmData[10];

    if (keyCapacity <= 0 || keyCapacity > 1000 || bucketCap <= 0) {
      Log("[MOUTH-RESOLVE] Invalid hashmap: keyCap=%d bucketCap=%d",
          keyCapacity, bucketCap);
      return;
    }

    if ((bucketCap & (bucketCap + 1)) != 0) {
      Log("[MOUTH-RESOLVE] WARNING: bucketCap=%d is not a power-of-2 bitmask! "
          "NativeHashMap layout may have changed.",
          bucketCap);
      return;
    }

    if (allocLen <= 0 || allocLen > keyCapacity) {
      Log("[MOUTH-RESOLVE] Invalid allocLen=%d (keyCap=%d)", allocLen,
          keyCapacity);
      return;
    }

    char *values = *(char **)(&hmData[0]);
    int *keys = *(int **)(&hmData[2]);
    int *nextArr = *(int **)(&hmData[4]);
    int *buckets = *(int **)(&hmData[6]);

    if (!values || !keys || !nextArr || !buckets) {
      Log("[MOUTH-RESOLVE] Null pointers in hashmap");
      return;
    }

    uintptr_t vAddr = (uintptr_t)values;
    uintptr_t kAddr = (uintptr_t)keys;
    int valStride = (int)((kAddr - vAddr) / keyCapacity);

    if (valStride < 8 || valStride > 200) {
      Log("[MOUTH-RESOLVE] Invalid valStride=%d", valStride);
      return;
    }

    Log("[MOUTH-RESOLVE] hashmap: keyCap=%d bucketCap=%d valStride=%d "
        "allocLen=%d",
        keyCapacity, bucketCap, valStride, allocLen);

    if (valStride != 40) {
      Log("[MOUTH-RESOLVE] WARNING: valStride=%d (expected 40), "
          "NativeHashMap value struct may have changed!",
          valStride);
    }

    for (int m = 0; m < NUM_MOUTH_SHAPES; m++) {
      int targetHash = g_mouthShapes[m].nameHash;
      int bucket = ((unsigned int)targetHash) & ((unsigned int)bucketCap);
      int entryIdx = buckets[bucket];
      int depth = 0;

      while (entryIdx >= 0 && entryIdx < keyCapacity && depth < 100) {
        if (keys[entryIdx] == targetHash) {
          int *vi = (int *)(values + entryIdx * valStride);
          g_mouthShapes[m].morphId = vi[0];
          g_mouthShapes[m].startIdx = vi[4];
          g_mouthShapes[m].count = vi[5];
          g_mouthShapes[m].jobStartIdx = vi[4];
          g_mouthShapes[m].jobCount = vi[5];
          g_mouthShapes[m].resolved = true;
          Log("[MOUTH-RESOLVE] '%s' entry[%d]: morphId=%d smcStartIdx=%d "
              "smcCount=%d partType=%d",
              g_mouthShapes[m].name, entryIdx, vi[0], vi[4], vi[5], vi[3]);

          if (vi[4] < 0 || vi[4] > 10000 || vi[5] <= 0 || vi[5] > 500) {
            Log("[MOUTH-RESOLVE] WARNING: '%s' has suspicious "
                "startIdx=%d/count=%d, NativeHashMap value layout may have "
                "changed!",
                g_mouthShapes[m].name, vi[4], vi[5]);
            g_mouthShapes[m].resolved = false;
          }
          break;
        }
        entryIdx = nextArr[entryIdx];
        depth++;
      }
    }

    Log("[MOUTH-RESOLVE] NativeHashMap info collected (offsets will come from "
        "GROUPSCAN)");

    int adOff = SafeOff(OFF_avatarData, 0x58, "avatarData");
    void *avatarData = *(void **)(smcBase + adOff);

    if (avatarData && OFF_morphMappingNames < 0) {
      void *adClass = il2cpp_object_get_class(avatarData);
      if (adClass) {
        void *fiter2 = nullptr;
        void *field2;
        while ((field2 = il2cpp_class_get_fields(adClass, &fiter2))) {
          const char *fn2 = il2cpp_field_get_name(field2);
          if (fn2 && strcmp(fn2, "morphMappingNames") == 0) {
            OFF_morphMappingNames = (int)il2cpp_field_get_offset(field2);
            Log("[OFFSET] AvatarData.morphMappingNames = %d (0x%X)",
                OFF_morphMappingNames, OFF_morphMappingNames);
            break;
          }
        }
        if (OFF_morphMappingNames < 0)
          Log("[WARN] morphMappingNames field not found, using fallback 0x38");
      }
    }

    int mnOff = SafeOff(OFF_morphMappingNames, 0x38, "morphMappingNames");
    void *nameArr =
        avatarData ? *(void **)((char *)avatarData + mnOff) : nullptr;
    int nameArrLen = nameArr ? *(int *)((char *)nameArr + 0x18) : 0;

    if (nameArr && nameArrLen > 0 && nameArrLen <= 500) {
      int extraResolved = 0;
      for (int e = 0; e < allocLen && e < keyCapacity; e++) {
        int *vi = (int *)(values + e * valStride);
        int morphId = vi[0];
        int smcStart = vi[4];
        int smcCount = vi[5];
        if (morphId < 0 || morphId >= nameArrLen)
          continue;

        void *strObj = *(void **)((char *)nameArr + 0x20 + morphId * 8);
        if (!strObj)
          continue;
        int strLen = *(int *)((char *)strObj + 0x10);
        wchar_t *strChars = (wchar_t *)((char *)strObj + 0x14);
        if (strLen <= 0 || strLen > 200)
          continue;

        char narrowName[256] = {};
        for (int c = 0; c < strLen && c < 255; c++)
          narrowName[c] = (char)strChars[c];

        for (int em = 0; em < NUM_EXTRA_MORPHS; em++) {
          for (int t = 0; t < g_extraMorphs[em].targetCount; t++) {
            ExtraMorphTarget &tgt = g_extraMorphs[em].targets[t];
            if (!tgt.resolved && strcmp(narrowName, tgt.endfieldName) == 0) {
              tgt.nameHash = keys[e];
              tgt.startIdx = smcStart;
              tgt.count = smcCount;
              tgt.jobStartIdx = smcStart;
              tgt.jobCount = smcCount;
              tgt.jobStartIdx = smcStart;
              tgt.jobCount = smcCount;
              tgt.resolved = true;
              extraResolved++;

              if (smcStart < 0 || smcStart > 10000 || smcCount <= 0 ||
                  smcCount > 500) {
                Log("[EXTRA-RESOLVE] WARNING: %s has suspicious "
                    "start=%d/count=%d",
                    tgt.endfieldName, smcStart, smcCount);
                tgt.resolved = false;
                extraResolved--;
              } else {
                Log("[EXTRA-RESOLVE] %s -> %s: start=%d count=%d",
                    g_extraMorphs[em].label, tgt.endfieldName, smcStart,
                    smcCount);
              }
            }
          }
        }
      }
      int totalTargets = 0;
      for (int i = 0; i < NUM_EXTRA_MORPHS; i++)
        totalTargets += g_extraMorphs[i].targetCount;
      Log("[EXTRA-RESOLVE] Resolved %d/%d extra morph targets", extraResolved,
          totalTargets);
      g_extraMorphsResolved = (extraResolved > 0);

      Log("[MORPH-DUMP] All morphs with bone delta (count>0):");
      for (int e = 0; e < allocLen && e < keyCapacity; e++) {
        int *vi = (int *)(values + e * valStride);
        int morphId = vi[0], smcStart = vi[4], smcCount = vi[5];
        if (morphId < 0 || morphId >= nameArrLen || smcCount <= 0)
          continue;
        void *strObj = *(void **)((char *)nameArr + 0x20 + morphId * 8);
        if (!strObj)
          continue;
        int strLen = *(int *)((char *)strObj + 0x10);
        wchar_t *strChars = (wchar_t *)((char *)strObj + 0x14);
        char nn[128] = {};
        for (int c = 0; c < strLen && c < 127; c++)
          nn[c] = (char)strChars[c];
        Log("[MORPH-DUMP]   [%d] %s: start=%d count=%d", morphId, nn, smcStart,
            smcCount);
      }
    }
  } __except (1) {
    Log("[MOUTH-RESOLVE] Exception during hashmap traversal");
  }
  g_mouthShapesResolved = true;
}

static Quat EulerToQuat(float degX, float degY, float degZ) {
  const float D2R = 3.14159265358979f / 180.0f;
  float rx = degX * D2R * 0.5f;
  float ry = degY * D2R * 0.5f;
  float rz = degZ * D2R * 0.5f;
  float cx = cosf(rx), sx = sinf(rx);
  float cy = cosf(ry), sy = sinf(ry);
  float cz = cosf(rz), sz = sinf(rz);
  Quat q;
  q.x = cx * sy * sz + sx * cy * cz;
  q.y = cx * sy * cz - sx * cy * sz;
  q.z = cx * cy * sz - sx * sy * cz;
  q.w = cx * cy * cz + sx * sy * sz;
  return q;
}

static Quat MorphQuatMul(Quat a, Quat b) {
  Quat r;
  r.x = a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y;
  r.y = a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x;
  r.z = a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w;
  r.w = a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z;
  return r;
}

typedef void(__fastcall *MorphToBoneJob_t)(void *__this, void *param1,
                                           void *param2, void *methodInfo);
static MorphToBoneJob_t g_origMorphToBoneJob = nullptr;
static int s_jobCallCount = 0;


static void __fastcall Hooked_MorphToBoneJob(void *__this, void *param1,
                                             void *param2, void *methodInfo) {
  s_jobCallCount++;

  if (!g_smcOffsetsResolved && g_skeletalMorphCoreClass) {
    ResolveSMCOffsets(g_skeletalMorphCoreClass);
  }

  if (!g_confirmedSMC && param1) {
    g_confirmedSMC = param1;
    Log("[JOB] Confirmed face SMC from MorphToBoneJob: %p", param1);

    if (!s_eyeIKDisabled) {
      s_eyeIKDisabled = true;
      __try {
        int eyeOff = SafeOff(OFF_smcEyeLookAt, 0x1dd, "enableEyeLookAtIK");
        *(bool *)((char *)g_confirmedSMC + eyeOff) = false;
        Log("[IK-DISABLE] SMC EyeLookAtIK DISABLED at SMC confirm (offset 0x%X=false)", eyeOff);
      } __except (EXCEPTION_EXECUTE_HANDLER) {
        Log("[IK-DISABLE] Failed to disable SMC EyeLookAtIK at confirm");
      }
    }
  }

  if (s_jobCallCount <= 3) {
    Log("[JOB] DoEvaluateMorphToBoneJob #%d", s_jobCallCount);
    Log("[JOB]   this=%p param1(SMC)=%p", __this, param1);
  }


  if (!g_bigListCaptured && param1 && param1 == g_confirmedSMC) {
    __try {
      int blOff = SafeOff(OFF_bigList, 0x120, "bigList");
      void *bigBuf = *(void **)((char *)param1 + blOff);
      int bigLen = *(int *)((char *)param1 + blOff + 8);

      if (bigBuf && bigLen > 0 && bigLen < 10000 && !g_bigListCaptured) {
        if (bigLen > (int)(sizeof(g_capturedExpression) /
                          sizeof(MorphBoneEntry))) {
          Log("[BIGLIST] bigLen=%d exceeds buffer, clamping", bigLen);
          bigLen = (int)(sizeof(g_capturedExpression) / sizeof(MorphBoneEntry));
        }
        MorphBoneEntry *entries = (MorphBoneEntry *)bigBuf;
        memcpy(g_capturedExpression, entries, bigLen * sizeof(MorphBoneEntry));
        g_capturedLen = bigLen;
        g_bigListCaptured = true;

        int nonZeroCount = 0;
        for (int i = 0; i < bigLen; i++) {
          if (entries[i].deltaPosX != 0 || entries[i].deltaPosY != 0 ||
              entries[i].deltaPosZ != 0 || entries[i].deltaRotX != 0 ||
              entries[i].deltaRotY != 0 || entries[i].deltaRotZ != 0) {
            nonZeroCount++;
          }
        }

        Log("[BIGLIST] Captured %d entries x %d bytes = %d total (nonZero=%d)",
            bigLen, (int)sizeof(MorphBoneEntry),
            bigLen * (int)sizeof(MorphBoneEntry), nonZeroCount);

        for (int i = 0; i < 5 && i < bigLen; i++) {
          Log("[BIGLIST] [%d] hash=%d boneID=%d pos=(%.3f,%.3f,%.3f) "
              "rot=(%.3f,%.3f,%.3f)",
              i, entries[i].boneNameHash, entries[i].boneID,
              entries[i].deltaPosX, entries[i].deltaPosY,
              entries[i].deltaPosZ, entries[i].deltaRotX,
              entries[i].deltaRotY, entries[i].deltaRotZ);
        }
      }
    } __except (1) {
    }
  }

  if (FaceMorphOwnershipActive() && g_faceBonesCaptured &&
      g_boneMapReady && param1 && param1 == g_confirmedSMC) {
    __try {
      int blOff2 = SafeOff(OFF_bigList, 0x120, "bigList");
      void *bigBuf = *(void **)((char *)param1 + blOff2);
      int bigLen = *(int *)((char *)param1 + blOff2 + 8);

      if (bigBuf && bigLen > 0 && bigLen == g_capturedLen) {
        MorphBoneEntry *live = (MorphBoneEntry *)bigBuf;
        for (int i = 0; i < bigLen; i++) {
          int boneID = live[i].boneID;
          int arrIdx = (boneID >= 0 && boneID < 512) ? g_boneIDToIdx[boneID] : -1;
          if (arrIdx < 0 || arrIdx >= g_faceBoneCount)
            continue;
          live[i].deltaPosX = 0;
          live[i].deltaPosY = 0;
          live[i].deltaPosZ = 0;
          live[i].deltaRotX = 0;
          live[i].deltaRotY = 0;
          live[i].deltaRotZ = 0;
        }
      } else if (bigBuf && bigLen > 0 && bigLen != g_capturedLen) {
        Log("[JOB] WARNING: bigLen=%d != capturedLen=%d, skipping delta zero",
            bigLen, g_capturedLen);
      }
    } __except (1) {
    }
  }

  if (g_origMorphToBoneJob)
    g_origMorphToBoneJob(__this, param1, param2, methodInfo);
}

typedef void(__fastcall *SpecialMorphJob_t)(void *__this, void *param1,
                                            void *param2, void *methodInfo);
static SpecialMorphJob_t g_origSpecialMorphJob = nullptr;

static void __fastcall Hooked_SpecialMorphJob(void *__this, void *param1,
                                              void *param2, void *methodInfo) {
  if (g_origSpecialMorphJob)
    g_origSpecialMorphJob(__this, param1, param2, methodInfo);
}

static void RestoreBigList() {
  if (!g_confirmedSMC || g_capturedLen <= 0)
    return;
  __try {
    int blOff3 = SafeOff(OFF_bigList, 0x120, "bigList");
    void *bigBuf = *(void **)((char *)g_confirmedSMC + blOff3);
    int bigLen = *(int *)((char *)g_confirmedSMC + blOff3 + 8);
    if (bigBuf && bigLen == g_capturedLen) {
      memcpy(bigBuf, g_capturedExpression, bigLen * sizeof(MorphBoneEntry));
      Log("[RESTORE] BigList restored (%d entries)", bigLen);
    }
  } __except (1) {
    Log("[RESTORE] BigList restore failed");
  }
}

static void CleanupPoseHandler();

static void __fastcall Hooked_SMCUpdate(void *__this, float deltaTime,
                                        void *methodInfo) {
  static int s_frame = 0;
  if (!g_skeletalMorphCore) {
    if (g_confirmedSMC && __this == g_confirmedSMC) {
      g_skeletalMorphCore = __this;
      s_frame = 0;
      g_faceGetLocalPos = nullptr;
      Log("[FACE] Locked SMC (confirmed by MorphToBoneJob): %p", __this);
    } else {
      if (g_origSMCUpdate)
        g_origSMCUpdate(__this, deltaTime, methodInfo);
      return;
    }
  }
  if (__this != g_skeletalMorphCore) {
    static int s_otherCount = 0;
    if (s_otherCount < 3) {
      s_otherCount++;
      __try {
        char *otherBase = (char *)__this;
        int btOff =
            SafeOff(OFF_allBonesTransforms, 0x60, "allBonesTransforms");
        void *otherBonesArr = *(void **)(otherBase + btOff);
        int otherBoneCount = 0;
        if (otherBonesArr)
          otherBoneCount = *(int *)((char *)otherBonesArr + 24);
        Log("[FACE] OTHER SMC instance: %p, bonesArr=%p, boneCount=%d", __this,
            otherBonesArr, otherBoneCount);
      } __except (1) {
      }
    }
    if (g_origSMCUpdate)
      g_origSMCUpdate(__this, deltaTime, methodInfo);
    return;
  }

  s_frame++;

  DirectVmdMorph_UpdateFromRuntime(s_frame);

  if (!g_trojanActive && g_mouthWeightsFromMuscle) {
    g_mouthWeightsFromMuscle = false;
    memset(g_mouthWeights, 0, sizeof(g_mouthWeights));
    memset(g_faceBoneTouched, 0, sizeof(g_faceBoneTouched));
    if (g_confirmedSMC && OFF_allMorphBoneDirty > 0) {
      *(bool *)((char *)g_confirmedSMC + OFF_allMorphBoneDirty) = true;
    }
    Log("[FACE] Self-healed: cleared mouthWeightsFromMuscle (race fix)");
  }

  if (g_smcResetRequested) {
    s_frame = 1;
    g_smcResetRequested = false;
    Log("[FACE] SMC reset: s_frame reset to 1");
  }

  if (FaceMorphOwnershipActive() && g_faceBonesCaptured &&
      g_faceSetLocalPos && g_faceSetLocalRot && s_frame > 5) {
    __try {
      for (int i = 0; i < g_faceBoneCount; i++) {
        if (!g_faceBoneRefs[i])
          continue;
        float pos[3] = {g_faceBones[i].px, g_faceBones[i].py,
                        g_faceBones[i].pz};
        float rot[4] = {g_faceBones[i].rx, g_faceBones[i].ry, g_faceBones[i].rz,
                        g_faceBones[i].rw};
        void *posParams[] = {&pos};
        void *rotParams[] = {&rot};
        il2cpp_runtime_invoke(g_faceSetLocalPos, g_faceBoneRefs[i], posParams,
                              nullptr);
        il2cpp_runtime_invoke(g_faceSetLocalRot, g_faceBoneRefs[i], rotParams,
                              nullptr);
      }
    } __except (1) {
    }
  }

  if (s_frame == 1 && !g_nativeSetPos) {
    g_nativeSetPos = (NativeSetPos_t)il2cpp_resolve_icall(
        "UnityEngine.Transform::set_localPosition_Injected(UnityEngine.Vector3&"
        ")");
    g_nativeSetRot = (NativeSetRot_t)il2cpp_resolve_icall(
        "UnityEngine.Transform::set_localRotation_Injected(UnityEngine."
        "Quaternion&)");
    Log("[ICALL] Resolved nativeSetPos=%p nativeSetRot=%p", g_nativeSetPos,
        g_nativeSetRot);
  }

  if (s_frame == 1) {
    if (g_skeletalMorphCoreClass) {
      DumpClassFields(g_skeletalMorphCoreClass, "SkeletalMorphCore");
      ResolveSMCOffsets(g_skeletalMorphCoreClass);
      ResolveMouthShapes((char *)__this);

      int blOff = SafeOff(OFF_bigList, 0x120, "bigList");
      __try {
        char *blBase = (char *)__this + blOff;
        void *blBuf = *(void **)blBase;
        int blLen = *(int *)(blBase + 8);

        if (blBuf && blLen > 0 && blLen < MAX_BIGLIST) {
          MorphBoneEntry *entries = (MorphBoneEntry *)blBuf;
          Log("[BIGLIST-SMC] SMC BigList: %d entries (ratio to Job=%d: %.2f)",
              blLen, g_capturedLen,
              g_capturedLen > 0 ? (float)blLen / g_capturedLen : 0.0f);
          Log("[BIGLIST-SMC] Captured FULL BigList from SMC: %d entries",
              blLen);

          if (blLen > 1710) {
            for (int d = 1704; d < 1704 + 5 && d < blLen; d++) {
              Log("[BIGLIST-SMC]   [%d] boneHash=%d boneID=%d "
                  "dP=(%.4f,%.4f,%.4f) dR=(%.1f,%.1f,%.1f)",
                  d, entries[d].boneNameHash, entries[d].boneID,
                  entries[d].deltaPosX, entries[d].deltaPosY,
                  entries[d].deltaPosZ, entries[d].deltaRotX,
                  entries[d].deltaRotY, entries[d].deltaRotZ);
            }
          }
          if (blLen > 433) {
            Log("[BIGLIST-SMC] Comparison at old offset 428:");
            for (int d = 428; d < 428 + 3 && d < blLen; d++) {
              Log("[BIGLIST-SMC]   [%d] boneHash=%d boneID=%d "
                  "dP=(%.4f,%.4f,%.4f) dR=(%.1f,%.1f,%.1f)",
                  d, entries[d].boneNameHash, entries[d].boneID,
                  entries[d].deltaPosX, entries[d].deltaPosY,
                  entries[d].deltaPosZ, entries[d].deltaRotX,
                  entries[d].deltaRotY, entries[d].deltaRotZ);
            }
          }
          Log("[BIGLIST-SMC] Raw ints at entry[1704]:");
          int *rawE = (int *)&entries[1704];
          Log("[BIGLIST-SMC]   ints: %d %d %08X %08X %08X %08X %08X %08X %08X "
              "%08X %08X",
              rawE[0], rawE[1], rawE[2], rawE[3], rawE[4], rawE[5], rawE[6],
              rawE[7], rawE[8], rawE[9], rawE[10]);
        } else {
          Log("[BIGLIST-SMC] Invalid: buf=%p len=%d", blBuf, blLen);
        }
      } __except (1) {
        Log("[BIGLIST-SMC] Exception reading SMC BigList");
      }
    }

    __try {
      int adOff = SafeOff(OFF_avatarData, 0x58, "avatarData");
      void *avatarData = *(void **)((char *)__this + adOff);
      if (avatarData) {
        void *adClass = il2cpp_object_get_class(avatarData);
        if (adClass) {
          DumpClassFields(adClass, "SkeletalMorphAvatarData");

          Log("[FACE] === AvatarData object exploration ===");
          void *fiter = nullptr;
          void *field;
          while ((field = il2cpp_class_get_fields(adClass, &fiter))) {
            const char *fname = il2cpp_field_get_name(field);
            size_t foff = il2cpp_field_get_offset(field);
            if (!fname || foff < 0x10)
              continue;

            __try {
              void *fval = *(void **)((char *)avatarData + foff);
              if (!fval)
                continue;
              uintptr_t addr = (uintptr_t)fval;
              if (addr < 0x10000 || addr > 0x7FFFFFFFFFFF)
                continue;

              __try {
                void *arrClass = il2cpp_object_get_class(fval);
                const char *arrName =
                    arrClass ? il2cpp_class_get_name(arrClass) : "?";
                int arrLen = *(int *)((char *)fval + 24);
                if (arrLen > 0 && arrLen <= 500) {
                  Log("[FACE]   %s (0x%X): array class=%s len=%d", fname,
                      (int)foff, arrName ? arrName : "?", arrLen);

                  if (arrLen > 0) {
                    void **elems = (void **)((char *)fval + 32);
                    if (elems[0]) {
                      __try {
                        void *elemClass = il2cpp_object_get_class(elems[0]);
                        const char *elemName =
                            elemClass ? il2cpp_class_get_name(elemClass) : "?";
                        Log("[FACE]     elem[0] class: %s",
                            elemName ? elemName : "?");
                      } __except (1) {
                      }
                    }
                  }
                }
              } __except (1) {
              }
            } __except (1) {
            }
          }
        }
      }
    } __except (1) {
    }


    __try {
      int pdOff = SafeOff(OFF_poseDictMorph, 0x158, "poseDictMorph");
      void *poseDict = *(void **)((char *)__this + pdOff);
      if (poseDict) {
        int dictCount = *(int *)((char *)poseDict + 0x20);
        Log("[FACE] m_poseDictMorph: count=%d", dictCount);

        void *keys = *(void **)((char *)poseDict + 0x38);
        void *vals = *(void **)((char *)poseDict + 0x40);

        if (keys && vals) {
          int kLen = *(int *)((char *)keys + 24);
          int vLen = *(int *)((char *)vals + 24);
          void *kClass = il2cpp_object_get_class(keys);
          void *vClass = il2cpp_object_get_class(vals);
          const char *kName = kClass ? il2cpp_class_get_name(kClass) : "?";
          const char *vName = vClass ? il2cpp_class_get_name(vClass) : "?";
          Log("[FACE]   _keys: class=%s len=%d", kName, kLen);
          Log("[FACE]   _values: class=%s len=%d", vName, vLen);
        }

        void *entries = *(void **)((char *)poseDict + 0x18);
        if (entries) {
          int eLen = *(int *)((char *)entries + 24);
          Log("[FACE]   _entries: len=%d", eLen);

          char *eData = (char *)entries + 32;
          for (int i = 0; i < 5 && i < dictCount; i++) {
            for (int stride : {16, 24, 32}) {
              int *ints = (int *)(eData + i * stride);
              float *flts = (float *)(eData + i * stride);
              Log("[FACE]   entry[%d] @stride%d: int{%d,%d,%d,%d,%d,%d} "
                  "float{%.3f,%.3f,%.3f,%.3f}",
                  i, stride, ints[0], ints[1], ints[2], ints[3], ints[4],
                  ints[5], flts[0], flts[1], flts[2], flts[3]);
            }
          }
        }
      }
    } __except (1) {
    }

    __try {
      int amOff = SafeOff(OFF_allMorphs, 0x210, "allMorphs");
      void *allMorphs = *(void **)((char *)__this + amOff);
      if (allMorphs) {
        void *amClass = il2cpp_object_get_class(allMorphs);
        const char *amName = amClass ? il2cpp_class_get_name(amClass) : "?";
        int amLen = *(int *)((char *)allMorphs + 24);
        Log("[FACE] m_allMorphs (off=%d): class=%s len=%d", amOff, amName,
            amLen);

        if (amLen > 0) {
          void **elems = (void **)((char *)allMorphs + 32);
          for (int i = 0; i < 3 && i < amLen; i++) {
            if (!elems[i])
              continue;
            void *eClass = il2cpp_object_get_class(elems[i]);
            const char *eName = eClass ? il2cpp_class_get_name(eClass) : "?";
            Log("[FACE]   elem[%d]: class=%s", i, eName);
          }
        }
      }
    } __except (1) {
    }

    __try {
      int phOff = SafeOff(OFF_phonemesWeights, 0x320, "phonemesWeights");
      void *phBuf = *(void **)((char *)__this + phOff);
      int phLen = *(int *)((char *)__this + phOff + 8);
      int phAlloc = *(int *)((char *)__this + phOff + 12);
      Log("[FACE] m_phonemesWeights: buf=%p len=%d alloc=%d", phBuf, phLen,
          phAlloc);
      if (phBuf && phLen > 0 && phLen <= 20) {
        float *phData = (float *)phBuf;
        Log("[FACE]   values: %.4f %.4f %.4f %.4f %.4f %.4f %.4f %.4f",
            phData[0], phData[1], phData[2], phData[3], phData[4], phData[5],
            phData[6], phData[7]);
      }
    } __except (1) {
    }

    __try {
      int mewOff = SafeOff(OFF_microExprWeights, 0x330, "microExprWeights");
      void *meBuf = *(void **)((char *)__this + mewOff);
      int meLen = *(int *)((char *)__this + mewOff + 8);
      Log("[FACE] m_microExpressionWeights: buf=%p len=%d", meBuf, meLen);
      if (meBuf && meLen > 0 && meLen <= 20) {
        float *meData = (float *)meBuf;
        Log("[FACE]   values: %.4f %.4f %.4f %.4f %.4f", meData[0], meData[1],
            meData[2], meData[3], meData[4]);
      }
    } __except (1) {
    }

    __try {
      int adOff2 = SafeOff(OFF_avatarData, 0x58, "avatarData");
      void *avatarData = *(void **)((char *)__this + adOff2);
      if (avatarData) {
        Log("[FACE] === SkeletalMorphAvatarData ===");

        int mnOff2 =
            SafeOff(OFF_morphMappingNames, 0x38, "morphMappingNames");
        void *nameArr = *(void **)((char *)avatarData + mnOff2);
        if (nameArr) {
          int nameCount = *(int *)((char *)nameArr + 24);
          Log("[FACE] Morph names (%d):", nameCount);

          void **names = (void **)((char *)nameArr + 32);
          for (int i = 0; i < nameCount && i < 250; i++) {
            void *str = names[i];
            if (!str)
              continue;
            __try {
              int slen = *(int *)((char *)str + 16);
              wchar_t *wchars = (wchar_t *)((char *)str + 20);
              if (slen > 0 && slen < 200) {
                char ascii[200] = {};
                for (int c = 0; c < slen && c < 199; c++) {
                  ascii[c] = (wchars[c] < 128) ? (char)wchars[c] : '?';
                }
                Log("[FACE]   [%d]: \"%s\"", i, ascii);
              }
            } __except (1) {
            }
          }
        }

        void *mappingArr = *(void **)((char *)avatarData + 0x20);
        if (mappingArr) {
          int mCount = *(int *)((char *)mappingArr + 24);
          void **mappings = (void **)((char *)mappingArr + 32);

          if (mCount > 0 && mappings[0]) {
            void *md0 = mappings[0];
            Log("[FACE] === First MappingData (%p) ===", md0);

            for (int off = 0x10; off <= 0xA0; off += 0x08) {
              __try {
                void *subPtr = *(void **)((char *)md0 + off);
                if (!subPtr)
                  continue;
                uintptr_t subAddr = (uintptr_t)subPtr;
                if (subAddr < 0x10000 || subAddr > 0x7FFFFFFFFFFF)
                  continue;

                __try {
                  int subLen = *(int *)((char *)subPtr + 24);
                  if (subLen > 0 && subLen <= 200) {
                    void *subKlass = *(void **)subPtr;
                    const char *subName =
                        subKlass ? il2cpp_class_get_name(subKlass) : "?";
                    Log("[FACE]   MD+0x%02X: arr len=%d, type=%s", off, subLen,
                        subName ? subName : "?");

                    float *sf = (float *)((char *)subPtr + 32);
                    int *si = (int *)((char *)subPtr + 32);
                    Log("[FACE]     floats[0-5]: %.4f %.4f %.4f %.4f %.4f %.4f",
                        sf[0], sf[1], sf[2], sf[3], sf[4], sf[5]);
                    Log("[FACE]     ints[0-5]:   %d %d %d %d %d %d", si[0],
                        si[1], si[2], si[3], si[4], si[5]);
                  }
                } __except (1) {
                }
              } __except (1) {
              }
            }
          }
        }
      }
    } __except (1) {
      Log("[FACE] Exception reading morph data");
    }
  }
  {
    static bool s_exprOffsetsResolved = false;
    if (!s_exprOffsetsResolved && s_frame >= 3 && g_skeletalMorphCoreClass) {
      s_exprOffsetsResolved = true;
      __try {
        int meOff = SafeOff(OFF_mainEmotion, 0x3C0, "mainEmotion");
        void *mainEmo = *(void **)((char *)__this + meOff);
        if (mainEmo) {
          if (OFF_emoPose < 0) {
            void *emoCls = il2cpp_object_get_class(mainEmo);
            if (emoCls) {
              const char *poseNames[] = {"_pose", "m_pose", "pose"};
              OFF_emoPose = FindFieldInHierarchy(emoCls, poseNames, 3, nullptr);
              if (OFF_emoPose >= 0)
                Log("[FACE] Resolved MainEmotion._pose offset = 0x%X", OFF_emoPose);
            }
          }
          int epOff = (OFF_emoPose >= 0) ? OFF_emoPose : 0x18;
          void *emoPose = *(void **)((char *)mainEmo + epOff);
          if (emoPose) {
            void *poseCls = il2cpp_object_get_class(emoPose);
            if (poseCls && OFF_poseMouth < 0) {
              const char *mouthNames[] = {"_mouthValue", "m_mouthValue", "mouthValue"};
              OFF_poseMouth = FindFieldInHierarchy(poseCls, mouthNames, 3, nullptr);
              if (OFF_poseMouth >= 0)
                Log("[FACE] Resolved Pose._mouthValue offset = 0x%X", OFF_poseMouth);
              const char *browNames[] = {"_browValueL", "m_browValueL", "browValueL", "_browValue"};
              OFF_poseBrowL = FindFieldInHierarchy(poseCls, browNames, 4, nullptr);
              if (OFF_poseBrowL >= 0)
                Log("[FACE] Resolved Pose._browValueL offset = 0x%X", OFF_poseBrowL);
            }
            int pmOff = (OFF_poseMouth >= 0) ? OFF_poseMouth : 0x30;
            void *mouthList = *(void **)((char *)emoPose + pmOff);
            if (mouthList && OFF_mcvCtrlName < 0) {
              int mSize = *(int *)((char *)mouthList + IL2CPP_LIST_SIZE);
              void *items = *(void **)((char *)mouthList + IL2CPP_LIST_ITEMS);
              if (items && mSize > 0) {
                void *elem0 = *(void **)((char *)items + IL2CPP_ARRAY_DATA);
                if (elem0) {
                  void *mcvCls = il2cpp_object_get_class(elem0);
                  if (mcvCls) {
                    const char *cnNames[] = {"_ctrlName", "m_ctrlName", "ctrlName"};
                    OFF_mcvCtrlName = FindFieldInHierarchy(mcvCls, cnNames, 3, nullptr);
                    if (OFF_mcvCtrlName >= 0)
                      Log("[FACE] Resolved MCV._ctrlName offset = 0x%X", OFF_mcvCtrlName);
                    const char *vNames[] = {"_value", "m_value", "value"};
                    OFF_mcvValue = FindFieldInHierarchy(mcvCls, vNames, 3, nullptr);
                    if (OFF_mcvValue >= 0)
                      Log("[FACE] Resolved MCV._value offset = 0x%X", OFF_mcvValue);
                  }
                }
              }
            }
          }
        }
      } __except (1) {
        Log("[FACE] Exception resolving ExpressionPose offsets");
      }
    }
  }


  if (s_frame == 15) {
    Log("[HASH] Frame 15: capturedLen=%d, correlDone=%d", g_capturedLen,
        (int)g_hashCorrelationDone);
  }
  if (g_bigListCaptured && !g_boneMapReady && s_frame >= 20) {
    g_boneMapReady = true;
    memset(g_boneIDToIdx, -1, sizeof(g_boneIDToIdx));

    __try {
      char *smcBase = (char *)__this;

      int biOff = SafeOff(OFF_boneIDToIdx, 0xE0, "boneIDToIdx");
      void *buf = *(void **)(smcBase + biOff);
      int len = *(int *)(smcBase + biOff + 8);
      Log("[BONEMAP] buf=%p len=%d", buf, len);

      if (buf && len > 0 && len < 500) {
        int *data = (int *)buf;
        Log("[BONEMAP]   data[0-7]: %d %d %d %d %d %d %d %d", data[0], data[1],
            data[2], data[3], data[4], data[5], data[6], data[7]);

        bool allNeg = true;
        for (int i = 0; i < 8 && i < len * 2; i++) {
          if (data[i] != -1) {
            allNeg = false;
            break;
          }
        }

        if (!allNeg) {
          int mapped = 0;
          for (int i = 0; i < len; i++) {
            int boneID = data[i * 2];
            int arrIdx = data[i * 2 + 1];
            if (boneID >= 0 && boneID < 512 && arrIdx >= 0 && arrIdx < 256) {
              g_boneIDToIdx[boneID] = arrIdx;
              mapped++;
              if (i < 3 || (boneID >= 88 && boneID <= 130)) {
                Log("[BONEMAP]   [%d] boneID=%d —arrIdx=%d", i, boneID,
                    arrIdx);
              }
            }
          }
          g_boneIDMapCount = mapped;
          Log("[BONEMAP]   Mapped %d bones!", mapped);
        } else {
          Log("[BONEMAP]   All -1! Trying as flat lookup array...");
          int mapped = 0;
          for (int i = 0; i < len; i++) {
            if (data[i] >= 0 && data[i] < 256) {
              g_boneIDToIdx[i] = data[i];
              mapped++;
              if (i < 3 || (i >= 88 && i <= 130)) {
                Log("[BONEMAP]   [boneID=%d] —arrIdx=%d", i, data[i]);
              }
            }
          }
          if (mapped > 0) {
            g_boneIDMapCount = mapped;
            Log("[BONEMAP]   Mapped %d bones (flat)!", mapped);
          } else {
            Log("[BONEMAP]   Still all -1 at frame %d, forcing dirty", s_frame);
            g_boneMapReady = false;
            if (OFF_allMorphBoneDirty > 0) {
              *(bool *)((char *)__this + OFF_allMorphBoneDirty) = true;
            }
          }
        }
      }
    } __except (1) {
      Log("[BONEMAP] Exception");
    }
  }


  if (g_faceTestActive && s_frame > 20 && !g_mouthWeightsFromMuscle &&
      !g_directVmdMorphActive) {
    float t = g_faceTestFrame / 180.0f;
    int baseShape = ((int)t) % NUM_MOUTH_SHAPES;
    int nextShape = (baseShape + 1) % NUM_MOUTH_SHAPES;
    float blend =
        (1.0f - cosf((t - (int)t) * 3.14159265f)) * 0.5f;

    for (int s = 0; s < NUM_MOUTH_SHAPES; s++)
      g_mouthWeights[s] = 0.0f;
    g_mouthWeights[baseShape] = 1.0f - blend;
    g_mouthWeights[nextShape] = blend;
  }

  bool faceActive = FaceMorphOwnershipActive() && s_frame > 20;
  if (s_frame == 25 || s_frame == 50 || s_frame == 100) {
    Log("[DIAG] s_frame=%d faceActive=%d boneMapReady=%d boneIDMapCount=%d "
        "capturedLen=%d mouthResolved=%d bigListCaptured=%d "
        "faceBonesCaptured=%d",
        s_frame, (int)faceActive, (int)g_boneMapReady, g_boneIDMapCount,
        g_capturedLen, (int)g_mouthShapesResolved, (int)g_bigListCaptured,
        (int)g_faceBonesCaptured);
  }
  if (faceActive && g_boneMapReady && g_boneIDMapCount > 0 &&
      g_capturedLen > 0 && g_mouthShapesResolved) {
    __try {
      float totalWeight = 0;
      for (int s = 0; s < NUM_MOUTH_SHAPES; s++)
        totalWeight += g_mouthWeights[s];
      if (totalWeight > 1.0f) {
        float scale = 1.0f / totalWeight;
        for (int s = 0; s < NUM_MOUTH_SHAPES; s++)
          g_mouthWeights[s] *= scale;
      }

      memcpy(g_faceBones, g_faceRestPose, sizeof(g_faceBones));

      float deltaPosAccum[MAX_FACE_BONES][3] = {};
      float deltaRotAccum[MAX_FACE_BONES][3] = {};
      memset(g_faceBoneTouched, 0, sizeof(g_faceBoneTouched));

      int applied = 0;
      for (int s = 0; s < NUM_MOUTH_SHAPES; s++) {
        float w = g_mouthWeights[s];
        if (w < 0.001f)
          continue;
        int startIdx = g_mouthShapes[s].jobStartIdx;
        int cnt = g_mouthShapes[s].jobCount;
        if (startIdx < 0 || startIdx + cnt > g_capturedLen)
          continue;

        for (int i = startIdx; i < startIdx + cnt; i++) {
          int boneID = g_capturedExpression[i].boneID;
          int arrIdx =
              (boneID >= 0 && boneID < 512) ? g_boneIDToIdx[boneID] : -1;
          if (arrIdx < 0 || arrIdx >= g_faceBoneCount)
            continue;

          float dpx = g_capturedExpression[i].deltaPosX;
          float dpy = g_capturedExpression[i].deltaPosY;
          float dpz = g_capturedExpression[i].deltaPosZ;
          if (fabsf(dpx) > 1.0f || fabsf(dpy) > 1.0f || fabsf(dpz) > 1.0f)
            continue;

          deltaPosAccum[arrIdx][0] += dpx * w;
          deltaPosAccum[arrIdx][1] += dpy * w;
          deltaPosAccum[arrIdx][2] += dpz * w;
          float drx = g_capturedExpression[i].deltaRotX;
          float dry = g_capturedExpression[i].deltaRotY;
          float drz = g_capturedExpression[i].deltaRotZ;
          if (fabsf(drx) < 30.0f && fabsf(dry) < 30.0f && fabsf(drz) < 30.0f) {
            deltaRotAccum[arrIdx][0] += drx * w;
            deltaRotAccum[arrIdx][1] += dry * w;
            deltaRotAccum[arrIdx][2] += drz * w;
          }
          g_faceBoneTouched[arrIdx] = true;
          applied++;
        }
      }

      if (g_extraMorphsResolved) {
        for (int em = 0; em < NUM_EXTRA_MORPHS; em++) {
          float w = g_extraMorphs[em].weight;
          if (w < 0.001f)
            continue;

          for (int t = 0; t < g_extraMorphs[em].targetCount; t++) {
            ExtraMorphTarget &tgt = g_extraMorphs[em].targets[t];
            if (!tgt.resolved || tgt.startIdx < 0)
              continue;
            if (tgt.startIdx + tgt.count > g_capturedLen)
              continue;

            for (int i = tgt.startIdx; i < tgt.startIdx + tgt.count; i++) {
              int boneID = g_capturedExpression[i].boneID;
              int arrIdx =
                  (boneID >= 0 && boneID < 512) ? g_boneIDToIdx[boneID] : -1;
              if (arrIdx < 0 || arrIdx >= g_faceBoneCount)
                continue;

              float dpx = g_capturedExpression[i].deltaPosX;
              float dpy = g_capturedExpression[i].deltaPosY;
              float dpz = g_capturedExpression[i].deltaPosZ;
              if (fabsf(dpx) > 1.0f || fabsf(dpy) > 1.0f || fabsf(dpz) > 1.0f)
                continue;

              deltaPosAccum[arrIdx][0] += dpx * w;
              deltaPosAccum[arrIdx][1] += dpy * w;
              deltaPosAccum[arrIdx][2] += dpz * w;

              bool isEyeMorph =
                  (strncmp(tgt.endfieldName, "eye_", 4) == 0);
              if (!isEyeMorph) {
                deltaRotAccum[arrIdx][0] +=
                    g_capturedExpression[i].deltaRotX * w;
                deltaRotAccum[arrIdx][1] +=
                    g_capturedExpression[i].deltaRotY * w;
                deltaRotAccum[arrIdx][2] +=
                    g_capturedExpression[i].deltaRotZ * w;
              }
              g_faceBoneTouched[arrIdx] = true;
              applied++;
            }
          }
        }
      }

      for (int b = 0; b < g_faceBoneCount; b++) {
        if (!g_faceBoneTouched[b])
          continue;

        float dx = deltaPosAccum[b][0], dy = deltaPosAccum[b][1],
              dz = deltaPosAccum[b][2];
        float erx = deltaRotAccum[b][0], ery = deltaRotAccum[b][1],
              erz = deltaRotAccum[b][2];

        g_faceBones[b].px = g_faceRestPose[b].px + dx;
        g_faceBones[b].py = g_faceRestPose[b].py + dy;
        g_faceBones[b].pz = g_faceRestPose[b].pz + dz;

        Quat dq = EulerToQuat(erx, ery, erz);
        Quat rq = {g_faceRestPose[b].rx, g_faceRestPose[b].ry,
                   g_faceRestPose[b].rz, g_faceRestPose[b].rw};
        Quat res;
        res.w = dq.w * rq.w - dq.x * rq.x - dq.y * rq.y - dq.z * rq.z;
        res.x = dq.w * rq.x + dq.x * rq.w + dq.y * rq.z - dq.z * rq.y;
        res.y = dq.w * rq.y - dq.x * rq.z + dq.y * rq.w + dq.z * rq.x;
        res.z = dq.w * rq.z + dq.x * rq.y - dq.y * rq.x + dq.z * rq.w;
        float len = sqrtf(res.x * res.x + res.y * res.y + res.z * res.z +
                          res.w * res.w);
        if (len > 1e-6f) {
          res.x /= len;
          res.y /= len;
          res.z /= len;
          res.w /= len;
        }
        g_faceBones[b].rx = res.x;
        g_faceBones[b].ry = res.y;
        g_faceBones[b].rz = res.z;
        g_faceBones[b].rw = res.w;

        void *boneTransform = (void *)g_faceBones[b].transform;
        if (boneTransform) {
          if (!g_nativeSetPos) {
            g_nativeSetPos = (NativeSetPos_t)il2cpp_resolve_icall(
                "UnityEngine.Transform::set_localPosition_Injected(UnityEngine."
                "Vector3&)");
            g_nativeSetRot = (NativeSetRot_t)il2cpp_resolve_icall(
                "UnityEngine.Transform::set_localRotation_Injected(UnityEngine."
                "Quaternion&)");
          }
          if (g_nativeSetPos && g_nativeSetRot) {
            float pos[3] = {g_faceBones[b].px, g_faceBones[b].py,
                            g_faceBones[b].pz};
            float rot[4] = {g_faceBones[b].rx, g_faceBones[b].ry,
                            g_faceBones[b].rz, g_faceBones[b].rw};
            g_nativeSetPos(boneTransform, pos);
            g_nativeSetRot(boneTransform, rot);
          }
        }
      }

      if (g_faceTestFrame % 300 == 0) {
        Log("[MOUTH] Blended: A=%.2f I=%.2f U=%.2f E=%.2f O=%.2f, total "
            "deltas=%d",
            g_mouthWeights[0], g_mouthWeights[1], g_mouthWeights[2],
            g_mouthWeights[3], g_mouthWeights[4], applied);
      }
    } __except (1) {
      Log("[MOUTH] Exception");
    }
  }
  if (FaceMorphOwnershipActive() && s_frame >= 15) {
    g_faceTestFrame++;
  }

  if (FaceMorphOwnershipActive() && g_faceBonesCaptured &&
      OFF_allMorphBoneDirty > 0) {
    char *smcBase = (char *)__this;
    *(bool *)(smcBase + OFF_allMorphBoneDirty) = false;
  }


  if (!g_faceBoneRefs && s_frame >= 1) {
    __try {
      int btOff =
          SafeOff(OFF_allBonesTransforms, 0x60, "allBonesTransforms");
      void *bonesArr = *(void **)((char *)__this + btOff);
      if (bonesArr) {
        int boneLen = *(int *)((char *)bonesArr + 24);
        if (boneLen > 0 && boneLen <= 256) {
          g_faceBoneRefs = (void **)((char *)bonesArr + 32);
          g_faceBoneCount = boneLen;
          Log("[FACE] Bone refs resolved: %d bones (pre-Update frame %d)",
              g_faceBoneCount, s_frame);

          if (!g_faceGetLocalPos && g_faceBoneRefs[0]) {
            void *tc = il2cpp_object_get_class(g_faceBoneRefs[0]);
            g_faceGetLocalPos =
                FindMethod(tc, "get_localPosition_Injected", 1);
            g_faceSetLocalPos =
                FindMethod(tc, "set_localPosition_Injected", 1);
            g_faceGetLocalRot =
                FindMethod(tc, "get_localRotation_Injected", 1);
            g_faceSetLocalRot =
                FindMethod(tc, "set_localRotation_Injected", 1);
            Log("[FACE] Transform methods init: gP=%p sP=%p gR=%p sR=%p",
                g_faceGetLocalPos, g_faceSetLocalPos, g_faceGetLocalRot,
                g_faceSetLocalRot);
          }
        }
      }
    } __except (1) {
    }
  }

  if (g_faceGetLocalPos && g_faceGetLocalRot && g_faceBoneRefs &&
      !g_faceBonesCaptured && s_frame >= 1) {
    int captured = 0;
    for (int i = 0; i < g_faceBoneCount; i++) {
      if (!g_faceBoneRefs[i])
        continue;
      __try {
        float pos[3] = {}, rot[4] = {};
        void *posParams[] = {&pos};
        void *rotParams[] = {&rot};
        il2cpp_runtime_invoke(g_faceGetLocalPos, g_faceBoneRefs[i], posParams,
                              nullptr);
        il2cpp_runtime_invoke(g_faceGetLocalRot, g_faceBoneRefs[i], rotParams,
                              nullptr);
        g_faceBones[i].px = pos[0];
        g_faceBones[i].py = pos[1];
        g_faceBones[i].pz = pos[2];
        g_faceBones[i].rx = rot[0];
        g_faceBones[i].ry = rot[1];
        g_faceBones[i].rz = rot[2];
        g_faceBones[i].rw = rot[3];
        captured++;
      } __except (1) {
      }
    }
    memcpy(g_faceRestPose, g_faceBones, sizeof(g_faceBones));
    g_faceBonesCaptured = true;
    Log("[FACE] CAPTURED %d bone transforms (rest pose, pre-Update frame %d)!",
        captured, s_frame);
    for (int i = 0; i < 3 && i < g_faceBoneCount; i++) {
      Log("[FACE]   bone[%d]: pos=(%.4f,%.4f,%.4f) rot=(%.4f,%.4f,%.4f,%.4f)",
          i, g_faceBones[i].px, g_faceBones[i].py, g_faceBones[i].pz,
          g_faceBones[i].rx, g_faceBones[i].ry, g_faceBones[i].rz,
          g_faceBones[i].rw);
    }
  }

  if (g_origSMCUpdate) {
    g_origSMCUpdate(__this, deltaTime, methodInfo);
  }

  {

    if (FaceMorphOwnershipActive() && g_faceBonesCaptured &&
        g_faceSetLocalPos && g_faceSetLocalRot && s_frame > 5) {
      __try {
        for (int i = 0; i < g_faceBoneCount; i++) {
          if (!g_faceBoneRefs[i])
            continue;
          if (!g_faceBoneTouched[i])
            continue;
          float pos[3] = {g_faceBones[i].px, g_faceBones[i].py,
                          g_faceBones[i].pz};
          float rot[4] = {g_faceBones[i].rx, g_faceBones[i].ry,
                          g_faceBones[i].rz, g_faceBones[i].rw};
          void *posParams[] = {&pos};
          void *rotParams[] = {&rot};
          il2cpp_runtime_invoke(g_faceSetLocalPos, g_faceBoneRefs[i], posParams,
                                nullptr);
          il2cpp_runtime_invoke(g_faceSetLocalRot, g_faceBoneRefs[i], rotParams,
                                nullptr);
        }
      } __except (1) {
      }
    }
  }
}
