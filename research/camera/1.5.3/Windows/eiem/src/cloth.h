#pragma once


static float s_skirtRadiusA = 1.0f;
static float s_skirtRadiusB = 3.0f;
static float s_skirtHipRadiusDelta = 0.124f;
static float s_skirtLengthScale = 1.0f;
static bool  s_skirtTaperOn = true;
static float s_skirtCenterOfs[3] = {0,0,0};
static float s_skirtOrigSize[8][3] = {};
static float s_skirtOrigCenter[8][3] = {};
static int s_skirtColliderCount = 0;
static bool s_skirtScaleResolved = false;
static bool s_skirtDirty = true;
static int s_skirtRetryFrames = 0;
static int s_skirtCenterAxis = 0;
static void *s_coll_SetSize = nullptr;
static void *s_coll_UpdateParams = nullptr;
static void *s_cloth_SetParamChange = nullptr;
static int OFF_bbc_process = -1;
static int OFF_cp_colliderList = -1;
static int OFF_radiusSep = -1;

static void ResetSkirtState() {
  s_skirtScaleResolved = false;
  s_skirtDirty = true;
  s_skirtColliderCount = 0;
  memset(s_skirtOrigSize, 0, sizeof(s_skirtOrigSize));
  memset(s_skirtOrigCenter, 0, sizeof(s_skirtOrigCenter));
}


static void ApplySkirtColliderScale() {
  if (s_skirtBBCIndex < 0 || s_skirtBBCIndex >= s_bbcCount) return;
  if (!s_bbcInstances[s_skirtBBCIndex]) return;
  if (!s_skirtDirty) return;
  s_skirtDirty = false;

  void *skirt = s_bbcInstances[s_skirtBBCIndex];
  int offProc = (OFF_bbc_process >= 0) ? OFF_bbc_process : 0xA8;
  int offCL   = (OFF_cp_colliderList >= 0) ? OFF_cp_colliderList : 0x3A0;
  int offRS   = (OFF_radiusSep >= 0) ? OFF_radiusSep : 0x45;
  __try {
    void *process = *(void **)((char *)skirt + offProc);
    if (!process) return;
    void *colliderList = *(void **)((char *)process + offCL);
    if (!colliderList || (uintptr_t)colliderList < 0x10000) return;
    void *items = *(void **)((char *)colliderList + 0x10);
    int count = *(int *)((char *)colliderList + 0x18);
    if (!items || count <= 0 || count > 8) return;

    if (!s_skirtScaleResolved) {
      void *coll0 = *(void **)((char *)items + 0x20);
      void *collCls = coll0 ? il2cpp_object_get_class(coll0) : nullptr;
      if (collCls) {
        s_coll_SetSize = FindMethodInHierarchy(collCls, "SetSize", 1);
        s_coll_UpdateParams = FindMethodInHierarchy(collCls, "UpdateParameters", 0);
        if (OFF_radiusSep < 0) {
          const char *rsNames[] = {"radiusSeparation", "m_radiusSeparation"};
          OFF_radiusSep = FindFieldInHierarchy(collCls, rsNames, 2, nullptr);
          if (OFF_radiusSep >= 0)
            Log("[SKIRT] Resolved radiusSeparation offset = 0x%X", OFF_radiusSep);
          offRS = SafeOff(OFF_radiusSep, 0x45, "radiusSep");
        }
      }
      void *clothCls = il2cpp_object_get_class(skirt);
      if (clothCls) {
        s_cloth_SetParamChange = FindMethodInHierarchy(clothCls, "SetParameterChange", 0);
        if (OFF_bbc_process < 0) {
          const char *procNames[] = {"process", "m_process", "clothProcess"};
          OFF_bbc_process = FindFieldInHierarchy(clothCls, procNames, 3, nullptr);
          if (OFF_bbc_process >= 0)
            Log("[SKIRT] Resolved BBC->process offset = 0x%X", OFF_bbc_process);
        }
        if (OFF_cp_colliderList < 0 && process) {
          void *procCls = il2cpp_object_get_class(process);
          if (procCls) {
            const char *clNames[] = {"colliderList", "m_colliderList", "colliders"};
            OFF_cp_colliderList = FindFieldInHierarchy(procCls, clNames, 3, nullptr);
            if (OFF_cp_colliderList >= 0)
              Log("[SKIRT] Resolved ClothProcess->colliderList offset = 0x%X", OFF_cp_colliderList);
          }
        }
      }
      s_skirtColliderCount = count;
      for (int i = 0; i < count; i++) {
        void *c = *(void **)((char *)items + 0x20 + i * 8);
        if (!c) continue;
        float *sz = (float *)((char *)c + 0x2C);
        float *ct = (float *)((char *)c + 0x20);
        for (int k = 0; k < 3; k++) {
          s_skirtOrigSize[i][k] = sz[k];
          s_skirtOrigCenter[i][k] = ct[k];
        }
      }
      s_skirtScaleResolved = true;
      Log("[SKIRT] Resolved: SetSize=%p UpdateParams=%p SetParamChange=%p, %d colliders",
          s_coll_SetSize, s_coll_UpdateParams, s_cloth_SetParamChange, count);
      for (int i = 0; i < count; i++)
        Log("[SKIRT] orig[%d] center=(%.3f,%.3f,%.3f) size=(%.4f,%.4f,%.4f)", i,
            s_skirtOrigCenter[i][0], s_skirtOrigCenter[i][1], s_skirtOrigCenter[i][2],
            s_skirtOrigSize[i][0], s_skirtOrigSize[i][1], s_skirtOrigSize[i][2]);
    }

    if (!s_coll_SetSize || !s_coll_UpdateParams) return;

    for (int i = 0; i < s_skirtColliderCount; i++) {
      void *c = *(void **)((char *)items + 0x20 + i * 8);
      if (!c) continue;

      bool isThighCapsule = fabsf(s_skirtOrigCenter[i][0]) > 0.1f;

      if (!isThighCapsule) {
        *(unsigned char *)((char *)c + offRS) = 0;
        float *ctR = (float *)((char *)c + 0x20);
        ctR[0] = s_skirtOrigCenter[i][0];
        ctR[1] = s_skirtOrigCenter[i][1];
        ctR[2] = s_skirtOrigCenter[i][2];
        Vector3 origSz = { s_skirtOrigSize[i][0], s_skirtOrigSize[i][1],
                           s_skirtOrigSize[i][2] };
        void *excR = nullptr;
        void *argsR[] = { &origSz };
        il2cpp_runtime_invoke(s_coll_SetSize, c, argsR, &excR);
        il2cpp_runtime_invoke(s_coll_UpdateParams, c, nullptr, &excR);
        continue;
      }

      *(unsigned char *)((char *)c + offRS) = s_skirtTaperOn ? 1 : 0;
      float *ct = (float *)((char *)c + 0x20);
      ct[0] = s_skirtOrigCenter[i][0] + s_skirtCenterOfs[0];
      ct[1] = s_skirtOrigCenter[i][1] + s_skirtCenterOfs[1];
      ct[2] = s_skirtOrigCenter[i][2] + s_skirtCenterOfs[2];
      float baseR = s_skirtOrigSize[i][0];
      float hipR;
      if (s_skirtHipRadiusDelta >= 0.0f) {
        float hs = (g_charHeight > 0.1f) ? (g_charHeight / 1.245f) : 1.0f;
        float scaledDelta = s_skirtHipRadiusDelta * hs;
        hipR = baseR + scaledDelta;
        float maxR = baseR * 3.0f;
        if (hipR > maxR) hipR = maxR;
      } else {
        hipR = baseR * s_skirtRadiusB;
      }
      Vector3 newSize = {
        baseR * s_skirtRadiusA,
        hipR,
        s_skirtOrigSize[i][2] * s_skirtLengthScale
      };
      void *exc = nullptr;
      void *args[] = { &newSize };
      il2cpp_runtime_invoke(s_coll_SetSize, c, args, &exc);
      il2cpp_runtime_invoke(s_coll_UpdateParams, c, nullptr, &exc);
    }
    if (s_cloth_SetParamChange) {
      void *exc = nullptr;
      il2cpp_runtime_invoke(s_cloth_SetParamChange, skirt, nullptr, &exc);
    }
    Log("[SKIRT] taper=%d rA=x%.1f rB=x%.1f l=x%.1f center+=(%.2f,%.2f,%.2f)",
        s_skirtTaperOn ? 1 : 0, s_skirtRadiusA, s_skirtRadiusB, s_skirtLengthScale,
        s_skirtCenterOfs[0], s_skirtCenterOfs[1], s_skirtCenterOfs[2]);
  } __except (1) {
    Log("[SKIRT] exception in ApplySkirtColliderScale");
  }
}





static void RestoreSkirtColliders() {
  if (!s_skirtScaleResolved) return;
  if (s_skirtBBCIndex < 0 || s_skirtBBCIndex >= s_bbcCount) return;
  if (!s_bbcInstances[s_skirtBBCIndex]) return;
  void *skirt = s_bbcInstances[s_skirtBBCIndex];
  int offProc = (OFF_bbc_process >= 0) ? OFF_bbc_process : 0xA8;
  int offCL   = (OFF_cp_colliderList >= 0) ? OFF_cp_colliderList : 0x3A0;
  int offRS   = (OFF_radiusSep >= 0) ? OFF_radiusSep : 0x45;
  __try {
    void *process = *(void **)((char *)skirt + offProc);
    if (!process) { Log("[SKIRT] restore: process null"); return; }
    void *colliderList = *(void **)((char *)process + offCL);
    if (!colliderList || (uintptr_t)colliderList < 0x10000) return;
    void *items = *(void **)((char *)colliderList + 0x10);
    int count = *(int *)((char *)colliderList + 0x18);
    if (!items || count <= 0 || count > 8) return;

    for (int i = 0; i < count && i < s_skirtColliderCount; i++) {
      void *c = *(void **)((char *)items + 0x20 + i * 8);
      if (!c) continue;
      *(unsigned char *)((char *)c + offRS) = 0;
      float *ct = (float *)((char *)c + 0x20);
      ct[0] = s_skirtOrigCenter[i][0];
      ct[1] = s_skirtOrigCenter[i][1];
      ct[2] = s_skirtOrigCenter[i][2];
      if (s_coll_SetSize) {
        Vector3 origSize = {
          s_skirtOrigSize[i][0], s_skirtOrigSize[i][1], s_skirtOrigSize[i][2]
        };
        void *exc = nullptr;
        void *args[] = { &origSize };
        il2cpp_runtime_invoke(s_coll_SetSize, c, args, &exc);
        if (s_coll_UpdateParams)
          il2cpp_runtime_invoke(s_coll_UpdateParams, c, nullptr, &exc);
      }
    }
    if (s_cloth_SetParamChange) {
      void *exc = nullptr;
      il2cpp_runtime_invoke(s_cloth_SetParamChange, skirt, nullptr, &exc);
    }
    Log("[SKIRT] Restored %d colliders to original state (radiusSeparation=false)",
        s_skirtColliderCount);
  } __except (1) {
    Log("[SKIRT] exception in RestoreSkirtColliders");
  }
  s_skirtScaleResolved = false;
  s_skirtDirty = true;
  s_skirtRadiusA = 1.0f;
  s_skirtRadiusB = 3.0f;
  s_skirtLengthScale = 1.0f;
  s_skirtCenterOfs[0] = s_skirtCenterOfs[1] = s_skirtCenterOfs[2] = 0;
}

static void ApplySkirtExtremeTest() {
  if (s_skirtBBCIndex < 0 || s_skirtBBCIndex >= s_bbcCount || !s_bbcInstances[s_skirtBBCIndex]) {
    Log("[SKIRT-X] MC_skirt not available"); return;
  }
  void *skirt = s_bbcInstances[s_skirtBBCIndex];
  int offProc = (OFF_bbc_process >= 0) ? OFF_bbc_process : 0xA8;
  int offCL   = (OFF_cp_colliderList >= 0) ? OFF_cp_colliderList : 0x3A0;
  __try {
    void *process = *(void **)((char *)skirt + offProc);
    if (!process) { Log("[SKIRT-X] process null"); return; }
    void *colliderList = *(void **)((char *)process + offCL);
    if (!colliderList || (uintptr_t)colliderList < 0x10000) {
      Log("[SKIRT-X] colliderList invalid"); return;
    }
    void *items = *(void **)((char *)colliderList + 0x10);
    int count = *(int *)((char *)colliderList + 0x18);
    if (!items || count <= 0 || count > 8) {
      Log("[SKIRT-X] bad count %d", count); return;
    }
    if (!s_coll_SetSize || !s_coll_UpdateParams) {
      void *coll0 = *(void **)((char *)items + 0x20);
      void *collCls = coll0 ? il2cpp_object_get_class(coll0) : nullptr;
      if (collCls) {
        s_coll_SetSize = FindMethodInHierarchy(collCls, "SetSize", 1);
        s_coll_UpdateParams = FindMethodInHierarchy(collCls, "UpdateParameters", 0);
      }
      void *clothCls = il2cpp_object_get_class(skirt);
      if (clothCls)
        s_cloth_SetParamChange = FindMethodInHierarchy(clothCls, "SetParameterChange", 0);
    }
    if (!s_coll_SetSize) { Log("[SKIRT-X] SetSize unresolved"); return; }

    for (int i = 0; i < count; i++) {
      void *c = *(void **)((char *)items + 0x20 + i * 8);
      if (!c) continue;
      float *center = (float *)((char *)c + 0x20);
      float *sz = (float *)((char *)c + 0x2C);
      Log("[SKIRT-X] coll[%d] BEFORE center=(%.3f,%.3f,%.3f) size=(%.4f,%.4f,%.4f)",
          i, center[0], center[1], center[2], sz[0], sz[1], sz[2]);
      Vector3 newSize = { 0.25f, 0.25f, 1.0f };
      void *exc = nullptr;
      void *args[] = { &newSize };
      il2cpp_runtime_invoke(s_coll_SetSize, c, args, &exc);
      il2cpp_runtime_invoke(s_coll_UpdateParams, c, nullptr, &exc);
      Log("[SKIRT-X] coll[%d] AFTER  size=(%.4f,%.4f,%.4f)", i, sz[0], sz[1], sz[2]);
    }
    if (s_cloth_SetParamChange) {
      void *exc = nullptr;
      il2cpp_runtime_invoke(s_cloth_SetParamChange, skirt, nullptr, &exc);
    }
    Log("[SKIRT-X] Applied EXTREME size (0.25,0.25,1.0) to %d colliders", count);
  } __except (1) {
    Log("[SKIRT-X] exception");
  }
}

static void *s_cloth_getSerializeData = nullptr;
static void DumpSkirtClothConstraints() {
  if (s_skirtBBCIndex < 0 || s_skirtBBCIndex >= s_bbcCount || !s_bbcInstances[s_skirtBBCIndex]) {
    Log("[SKIRT-SD] MC_skirt not available"); return;
  }
  void *skirt = s_bbcInstances[s_skirtBBCIndex];
  __try {
    void *clothCls = il2cpp_object_get_class(skirt);
    if (!s_cloth_getSerializeData && clothCls)
      s_cloth_getSerializeData =
          FindMethodInHierarchy(clothCls, "get_SerializeData", 0);
    if (!s_cloth_getSerializeData) {
      Log("[SKIRT-SD] get_SerializeData unresolved"); return;
    }
    void *exc = nullptr;
    void *sd = il2cpp_runtime_invoke(s_cloth_getSerializeData, skirt, nullptr, &exc);
    if (!sd) { Log("[SKIRT-SD] SerializeData null"); return; }
    Log("[SKIRT-SD] SerializeData=%p", sd);

    void *sdCls = il2cpp_object_get_class(sd);
    if (!sdCls) { Log("[SKIRT-SD] sd class null"); return; }
    const char *sdName = il2cpp_class_get_name(sdCls);
    Log("[SKIRT-SD] SerializeData class = %s", sdName ? sdName : "?");

    struct { int off; const char *name; } targets[] = {
      { 0xC0, "tetherConstraint" },
      { 0xC8, "distanceConstraint" },
      { 0xE0, "angleLimitConstraint" },
      { 0xE8, "motionConstraint" },
    };
    for (int t = 0; t < 4; t++) {
      void *obj = *(void **)((char *)sd + targets[t].off);
      if (!obj || (uintptr_t)obj < 0x10000) {
        Log("[SKIRT-SD] %s @0x%X = null/invalid", targets[t].name, targets[t].off);
        continue;
      }
      void *cls = il2cpp_object_get_class(obj);
      const char *cn = cls ? il2cpp_class_get_name(cls) : "?";
      Log("[SKIRT-SD] === %s -> %s @%p ===", targets[t].name, cn ? cn : "?", obj);
      if (cls) DumpClassFields(cls, targets[t].name);
      for (int off = 0x10; off <= 0x60; off += 4) {
        float f = *(float *)((char *)obj + off);
        int   iv = *(int *)((char *)obj + off);
        Log("[SKIRT-SD]   %s+0x%02X: float=%.5f int=%d", targets[t].name, off, f, iv);
      }
    }

    int offProc2 = (OFF_bbc_process >= 0) ? OFF_bbc_process : 0xA8;
    int offCL2   = (OFF_cp_colliderList >= 0) ? OFF_cp_colliderList : 0x3A0;
    void *process = *(void **)((char *)skirt + offProc2);
    void *colliderList = process ? *(void **)((char *)process + offCL2) : nullptr;
    if (colliderList && (uintptr_t)colliderList > 0x10000) {
      void *items = *(void **)((char *)colliderList + 0x10);
      int ccount = *(int *)((char *)colliderList + 0x18);
      void *coll0 = (items && ccount > 0) ? *(void **)((char *)items + 0x20) : nullptr;
      if (coll0) {
        void *collCls = il2cpp_object_get_class(coll0);
        const char *ccn = collCls ? il2cpp_class_get_name(collCls) : "?";
        Log("[SKIRT-SD] === collider[0] -> %s @%p ===", ccn ? ccn : "?", coll0);
        if (collCls) DumpFieldsHierarchy(collCls);
        for (int off = 0x18; off <= 0x50; off += 4) {
          float f = *(float *)((char *)coll0 + off);
          int   iv = *(int *)((char *)coll0 + off);
          Log("[SKIRT-SD]   coll0+0x%02X: float=%.5f int=%d", off, f, iv);
        }
      }
    }
  } __except (1) {
    Log("[SKIRT-SD] exception");
  }
}

