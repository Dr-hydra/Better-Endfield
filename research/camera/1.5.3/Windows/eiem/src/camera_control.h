#pragma once

static void ListComponentsOnGameObject(void *go, const char *tag) {
  if (!go) return;
  __try {
    void *getCompMethod =
        FindMethod(il2cpp_object_get_class(go), "GetComponents", 1);
    if (!getCompMethod) {
      Log("[CAM-PROBE] %s: GetComponents method not found", tag);
      return;
    }
    void *compType = il2cpp_class_get_type(g_componentClass);
    void *typeObj = compType ? il2cpp_type_get_object(compType) : nullptr;
    if (!typeObj) {
      Log("[CAM-PROBE] %s: Component typeObj null", tag);
      return;
    }
    void *args[] = {typeObj};
    void *arr = Invoke(getCompMethod, go, args);
    if (!arr) {
      Log("[CAM-PROBE] %s: GetComponents returned null", tag);
      return;
    }
    int cnt = *(int *)((char *)arr + 24);
    void **data = (void **)((char *)arr + 32);
    Log("[CAM-PROBE] %s: %d components", tag, cnt);
    for (int i = 0; i < cnt; i++) {
      if (!data[i]) continue;
      void *cls = il2cpp_object_get_class(data[i]);
      const char *clsName = cls ? il2cpp_class_get_name(cls) : "?";
      const char *clsNs = cls ? il2cpp_class_get_namespace(cls) : "";
      Log("[CAM-PROBE]   [%d] %s.%s @ %p", i, (clsNs && clsNs[0]) ? clsNs : "-",
          clsName ? clsName : "?", data[i]);
    }
  } __except (1) {
    Log("[CAM-PROBE] %s: exception during component enumeration", tag);
  }
}

static void InvestigateCamera() {
  Log("[CAM-PROBE] ===== Camera system investigation start =====");
  if (!g_camera_get_main) {
    Log("[CAM-PROBE] Camera.get_main not resolved, abort");
    return;
  }

  void *mainCam = Invoke(g_camera_get_main, nullptr);
  if (!mainCam) {
    Log("[CAM-PROBE] Camera.main returned null (no main camera tagged?)");
    return;
  }
  Log("[CAM-PROBE] Camera.main = %p", mainCam);

  if (g_camera_get_fieldOfView) {
    void *fovBox = Invoke(g_camera_get_fieldOfView, mainCam);
    if (fovBox) {
      float fov = *(float *)((char *)fovBox + 16);
      Log("[CAM-PROBE] Current FOV = %.2f", fov);
    }
  }

  void *camGO = g_component_get_gameObject
                    ? Invoke(g_component_get_gameObject, mainCam)
                    : nullptr;
  if (camGO) {
    void *nameStr = g_object_get_name ? Invoke(g_object_get_name, camGO) : nullptr;
    char goName[128] = "";
    if (nameStr) ReadStrUtf8(nameStr, goName, sizeof(goName));
    Log("[CAM-PROBE] Camera GO name = '%s'", goName);
    ListComponentsOnGameObject(camGO, "CameraGO");
  }

  void *camTransform = g_component_get_transform
                           ? Invoke(g_component_get_transform, mainCam)
                           : nullptr;
  int depth = 0;
  void *cur = camTransform;
  while (cur && depth < 6) {
    void *parent = g_transform_get_parent ? Invoke(g_transform_get_parent, cur)
                                          : nullptr;
    if (!parent) break;
    void *parentGO = g_component_get_gameObject
                         ? Invoke(g_component_get_gameObject, parent)
                         : nullptr;
    if (parentGO) {
      void *nameStr =
          g_object_get_name ? Invoke(g_object_get_name, parentGO) : nullptr;
      char goName[128] = "";
      if (nameStr) ReadStrUtf8(nameStr, goName, sizeof(goName));
      char tag[160];
      snprintf(tag, sizeof(tag), "Parent[%d] '%s'", depth, goName);
      ListComponentsOnGameObject(parentGO, tag);
    }
    cur = parent;
    depth++;
  }
  Log("[CAM-PROBE] ===== Camera system investigation end =====");
}

static void *g_mainCamera = nullptr;
static void *g_mainCamTransform = nullptr;
static void *g_camRootTransform = nullptr;

static bool ResolveMainCamera() {
  if (!g_camera_get_main) return false;
  void *mainCam = Invoke(g_camera_get_main, nullptr);
  if (!mainCam) {
    Log("[CAM] Camera.main is null");
    return false;
  }
  g_mainCamera = mainCam;
  g_mainCamTransform = g_component_get_transform
                           ? Invoke(g_component_get_transform, mainCam)
                           : nullptr;

  g_camRootTransform = nullptr;
  if (g_mainCamTransform && g_transform_get_parent) {
    g_camRootTransform = Invoke(g_transform_get_parent, g_mainCamTransform);
    if (g_camRootTransform)
      Log("[CAM] CameraRoot transform: %p", g_camRootTransform);
  }

  s_cinemachineBrain = nullptr;
  __try {
    void *camGO = g_component_get_gameObject
                      ? Invoke(g_component_get_gameObject, mainCam)
                      : nullptr;
    if (camGO) {
      void *getCompMethod =
          FindMethod(il2cpp_object_get_class(camGO), "GetComponents", 1);
      void *compType = il2cpp_class_get_type(g_componentClass);
      void *typeObj = compType ? il2cpp_type_get_object(compType) : nullptr;
      if (getCompMethod && typeObj) {
        void *args[] = {typeObj};
        void *arr = Invoke(getCompMethod, camGO, args);
        if (arr) {
          int cnt = *(int *)((char *)arr + 24);
          void **data = (void **)((char *)arr + 32);
          for (int i = 0; i < cnt; i++) {
            if (!data[i]) continue;
            void *cls = il2cpp_object_get_class(data[i]);
            const char *cn = cls ? il2cpp_class_get_name(cls) : "";
            if (cn && strcmp(cn, "CinemachineBrain") == 0) {
              s_cinemachineBrain = data[i];
              Log("[CAM] CinemachineBrain found: %p", s_cinemachineBrain);
              break;
            }
          }
        }
      }
    }
  } __except (1) {
    Log("[CAM] exception finding CinemachineBrain");
  }
  return g_mainCamera != nullptr && g_mainCamTransform != nullptr;
}

static void CaptureAndDisableCinemachine() {
  if (!ResolveMainCamera()) {
    Log("[CAM] ResolveMainCamera failed, camera takeover aborted");
    return;
  }
  if (g_camera_get_fieldOfView) {
    void *fovBox = Invoke(g_camera_get_fieldOfView, g_mainCamera);
    if (fovBox) g_origFov = *(float *)((char *)fovBox + 16);
  }
  if (s_cinemachineBrain && g_animator_set_enabled) {
    __try {
      int falseVal = 0;
      void *params[] = {&falseVal};
      Invoke(g_animator_set_enabled, s_cinemachineBrain, params);
      Log("[CAM] CinemachineBrain DISABLED (origFov=%.2f)", g_origFov);
    } __except (1) {
      Log("[CAM] Failed to disable CinemachineBrain");
    }
  } else {
    Log("[CAM] CinemachineBrain not found — camera may be overridden each frame");
  }
}

static void RestoreCinemachine() {
  if (s_cinemachineBrain && g_animator_set_enabled) {
    __try {
      int trueVal = 1;
      void *params[] = {&trueVal};
      Invoke(g_animator_set_enabled, s_cinemachineBrain, params);
      Log("[CAM] CinemachineBrain RE-ENABLED");
    } __except (1) {
    }
  }
  if (g_mainCamera && g_camera_set_fieldOfView && g_origFov > 0.0f) {
    __try {
      void *params[] = {&g_origFov};
      Invoke(g_camera_set_fieldOfView, g_mainCamera, params);
    } __except (1) {
    }
  }
  s_cinemachineBrain = nullptr;
  g_mainCamera = nullptr;
  g_mainCamTransform = nullptr;
  g_camRootTransform = nullptr;
}


static Vec3 SampleCharDisplacement(float timeSec) {
  Vec3 disp = {0, 0, 0};
  VmdFile *vmd = g_footIkVmd ? g_footIkVmd : g_vmd;
  if (!vmd || !vmd->loaded) return disp;
  auto it = vmd->boneTimelines.find(
      "\xe3\x82\xbb\xe3\x83\xb3\xe3\x82\xbf\xe3\x83\xbc");
  if (it != vmd->boneTimelines.end()) {
    float frameF = timeSec * 30.0f;
    InterpResult ir = InterpolateBone(it->second.keys, frameF, true);
    disp = ir.position;
  }
  return disp;
}

static void ApplyCameraFrame(float timeSec) {
  if (!g_cameraActive || !g_mainCamera || !g_mainCamTransform)
    return;
  if (!g_camTestMode && !g_cameraPlayer.HasData())
    return;

  CameraState cs;
  if (g_camTestMode) {
    cs.position = {0, 0, 0};
    cs.rotation = {0, 0, 0, 1};
    cs.fov = 40.0f;
    cs.valid = true;
  } else {
    cs = g_cameraPlayer.Sample(timeSec);
  }
  if (!cs.valid) return;

  float effYaw = CAM_YAW_SIGN * g_charYaw + CAM_YAW_BIAS * 0.0174533f;

  Vec3 charDisp = SampleCharDisplacement(timeSec);

  float camScale = g_cameraPlayer.scale;
  float hs = g_camHeightScale + g_camHeightBias;
  float px = (cs.position.x - charDisp.x * camScale) * hs;
  float py = cs.position.y * hs;
  float pz = (cs.position.z - charDisp.z * camScale) * hs;
  float cy = cosf(effYaw), sy = sinf(effYaw);
  float rx = px * cy + pz * sy;
  float rz = -px * sy + pz * cy;
  float worldPos[3] = {rx + g_charWorldPos.x,
                       py + g_charWorldPos.y,
                       rz + g_charWorldPos.z};

  float toCharX = g_charWorldPos.x - worldPos[0];
  float toCharZ = g_charWorldPos.z - worldPos[2];
  float lookYaw = atan2f(toCharX, toCharZ);

  float pitch = CAM_SIGN_RX * cs.euler.x;
  float roll  = CAM_SIGN_RZ * cs.euler.z;
  float hp = pitch * 0.5f, hr = roll * 0.5f, hly = lookYaw * 0.5f;
  Quat qLookYaw = {0, sinf(hly), 0, cosf(hly)};
  Quat qPitch   = {sinf(hp), 0, 0, cosf(hp)};
  Quat qRoll    = {0, 0, sinf(hr), cosf(hr)};
  Quat finalRot = QuatMul(QuatMul(qLookYaw, qPitch), qRoll);
  float worldRot[4] = {finalRot.x, finalRot.y, finalRot.z, finalRot.w};

  static int s_camDiag = 0;

  if (g_camTestMode) {
    static LARGE_INTEGER s_freq = {}, s_start = {};
    if (s_freq.QuadPart == 0) {
      QueryPerformanceFrequency(&s_freq);
      QueryPerformanceCounter(&s_start);
    }
    LARGE_INTEGER now;
    QueryPerformanceCounter(&now);
    float elapsed = (float)(now.QuadPart - s_start.QuadPart) / s_freq.QuadPart;
    float ang = elapsed * 0.5f;
    float r = 5.0f;
    worldPos[0] = g_charWorldPos.x + r * sinf(ang);
    worldPos[1] = g_charWorldPos.y;
    worldPos[2] = g_charWorldPos.z + r * cosf(ang);
    float half = (ang + 3.14159f) * 0.5f;
    worldRot[0] = 0.0f;
    worldRot[1] = sinf(half);
    worldRot[2] = 0.0f;
    worldRot[3] = cosf(half);
  }

  g_camHookTransform = g_mainCamTransform;
  __try {
    g_camSelfWrite = true;
    if (g_origSetLocalPos) g_origSetLocalPos(g_mainCamTransform, worldPos);
    else if (g_nativeSetPos) g_nativeSetPos(g_mainCamTransform, worldPos);
    if (g_origSetLocalRot) g_origSetLocalRot(g_mainCamTransform, worldRot);
    else if (g_nativeSetRot) g_nativeSetRot(g_mainCamTransform, worldRot);
    g_camSelfWrite = false;
    if (g_camera_set_fieldOfView) {
      void *args[] = {&cs.fov};
      Invoke(g_camera_set_fieldOfView, g_mainCamera, args);
    }

    s_camDiag++;
    if (s_camDiag <= 3 || s_camDiag % 60 == 0) {
      float readPos[3] = {}, readRot[4] = {};
      if (g_camGetPos) g_camGetPos(g_mainCamTransform, readPos);
      if (g_camGetRot) g_camGetRot(g_mainCamTransform, readRot);
      Log("[CAM-DIAG] #%d t=%.1f pos=(%.2f,%.2f,%.2f) rot=(%.3f,%.3f,%.3f,%.3f) fov=%.1f",
          s_camDiag, timeSec,
          readPos[0], readPos[1], readPos[2],
          readRot[0], readRot[1], readRot[2], readRot[3], cs.fov);
      Log("[CAM-DIAG] #%d VMDrot=(%.3f,%.3f,%.3f,%.3f)",
          s_camDiag, worldRot[0], worldRot[1], worldRot[2], worldRot[3]);
    }
  } __except (1) {
  }
}

static bool s_directVmdCameraOwned = false;
static int s_directVmdCameraLastUnityFrame = INT_MIN;
static uint64_t s_directVmdCameraLastSequence = 0;
static uint64_t s_directVmdCameraGeneration = 0;
static uintptr_t s_directVmdCameraOwner = 0;
static int s_directVmdCameraLogFrame = INT_MIN;

static void DirectVmdCamera_ResetMainThread(const char *reason) {
  if (!GhostRig_RequireMainThread("P6.Camera.Reset", true))
    return;
  const bool wasOwned = s_directVmdCameraOwned;
  if (s_directVmdCameraOwned) {
    RestoreCinemachine();
    ResetCameraState();
  }
  s_directVmdCameraOwned = false;
  s_directVmdCameraLastUnityFrame = INT_MIN;
  s_directVmdCameraLastSequence = 0;
  s_directVmdCameraGeneration = 0;
  s_directVmdCameraOwner = 0;
  s_directVmdCameraLogFrame = INT_MIN;
  g_camHookTransform = nullptr;
  g_camSelfWrite = false;
  if (wasOwned) {
    Log("[P6-CAMERA-RESET] reason=%s cinemachineRestored=1 "
        "fovRestored=1 ownershipCleared=1 tid=%lu",
        reason ? reason : "unspecified", GetCurrentThreadId());
  }
}

static void DirectVmdCamera_UpdateMainThread(int frameNumber) {
  if (!GhostRig_RequireMainThread("P6.Camera.Update", false))
    return;
  if (!g_motionBackend.Is(MotionBackend::DirectVmd) || !g_cameraEnabled ||
      !DirectVmdRuntime_IsLoaded()) {
    if (s_directVmdCameraOwned)
      DirectVmdCamera_ResetMainThread("backend-camera-or-resource-inactive");
    return;
  }

  DirectVmdSampleFrame frame;
  if (!DirectVmdRuntime_CopyLatestFrame(&frame) || !frame.valid ||
      !frame.camera.valid ||
      frame.rigGeneration != DirectVmdRuntime_PublicTargetGeneration() ||
      frame.ownerCharacter != DirectVmdRuntime_PublicTargetOwner() ||
      frame.clipGeneration != DirectVmdRuntime_PublicClipGeneration()) {
    if (s_directVmdCameraOwned)
      DirectVmdCamera_ResetMainThread("camera-sample-unavailable");
    return;
  }
  if ((frameNumber >= 0 &&
       frameNumber == s_directVmdCameraLastUnityFrame) ||
      (frameNumber < 0 &&
       frame.sequence == s_directVmdCameraLastSequence))
    return;

  DirectVmdWorldPosePod anchor;
  float motionScale = 0.0f;
  float naturalBindHeight = 0.0f;
  VmdVec3 targetSideWorldOffset = {};
  bool terrainFollowActive = false;
  uint64_t generation = 0;
  uintptr_t owner = 0;
  if (!GhostRig_GetDirectCameraReference(
          &anchor, &motionScale, &naturalBindHeight,
          &targetSideWorldOffset,
          &terrainFollowActive, &generation, &owner) ||
      generation != frame.rigGeneration || owner != frame.ownerCharacter) {
    if (s_directVmdCameraOwned)
      DirectVmdCamera_ResetMainThread("ghost-reference-unavailable");
    return;
  }

  DirectVmdCameraFraming framing;
  if (!DirectVmdResolveCameraFraming(
          frame.camera.fromOverride != 0, motionScale,
          naturalBindHeight, g_camHeightBias, frame.camera.fov,
          &framing)) {
    if (s_directVmdCameraOwned)
      DirectVmdCamera_ResetMainThread("camera-framing-invalid");
    return;
  }

  if (!s_directVmdCameraOwned ||
      s_directVmdCameraGeneration != generation ||
      s_directVmdCameraOwner != owner) {
    if (s_directVmdCameraOwned)
      DirectVmdCamera_ResetMainThread("camera-generation-change");
    CaptureAndDisableCinemachine();
    if (!g_mainCamera || !g_mainCamTransform) {
      Log("[P6-CAMERA-ACQUIRE] failed generation=%llu owner=%p "
          "frame=%d tid=%lu",
          (unsigned long long)generation,
          reinterpret_cast<void *>(owner), frameNumber,
          GetCurrentThreadId());
      return;
    }
    s_directVmdCameraOwned = true;
    s_directVmdCameraGeneration = generation;
    s_directVmdCameraOwner = owner;
    g_cameraActive = true;
    Log("[P6-CAMERA-ACQUIRE] success generation=%llu owner=%p "
        "source=%s framing=%s sameClock=1 mainThreadOnly=1 tid=%lu",
        (unsigned long long)generation, reinterpret_cast<void *>(owner),
        frame.camera.fromOverride ? "override" : "direct-vmd",
        framing.legacyOverride ? "legacy-override" : "direct-shared",
        GetCurrentThreadId());
  }

  DirectVmdWorldPosePod desired;
  if (!DirectVmdBuildCameraWorldPose(
          frame.camera, anchor, framing.positionScale,
          targetSideWorldOffset,
          &desired)) {
    if (s_directVmdCameraOwned)
      DirectVmdCamera_ResetMainThread("camera-conversion-failed");
    return;
  }
  const float fov = framing.fov;
  float worldPosition[3] = {desired.position.x, desired.position.y,
                            desired.position.z};
  float worldRotation[4] = {desired.rotation.x, desired.rotation.y,
                            desired.rotation.z, desired.rotation.w};
  bool positionWritten = false;
  bool rotationWritten = false;
  bool fovWritten = false;
  g_camHookTransform = g_mainCamTransform;
  __try {
    g_camSelfWrite = true;
    if (g_origSetPos) {
      g_origSetPos(g_mainCamTransform, worldPosition);
      positionWritten = true;
    } else if (g_camSetPos) {
      g_camSetPos(g_mainCamTransform, worldPosition);
      positionWritten = true;
    }
    if (g_origSetRot) {
      g_origSetRot(g_mainCamTransform, worldRotation);
      rotationWritten = true;
    } else if (g_camSetRot) {
      g_camSetRot(g_mainCamTransform, worldRotation);
      rotationWritten = true;
    }
    if (g_camera_set_fieldOfView) {
      void *arguments[] = {const_cast<float *>(&fov)};
      Invoke(g_camera_set_fieldOfView, g_mainCamera, arguments);
      fovWritten = true;
    }
    g_camSelfWrite = false;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    g_camSelfWrite = false;
  }

  if (!positionWritten || !rotationWritten) {
    Log("[P6-CAMERA-WRITE] failed frame=%d sourceFrame=%.6f "
        "position=%d rotation=%d fov=%d generation=%llu owner=%p "
        "tid=%lu",
        frameNumber, frame.sourceFrame, positionWritten ? 1 : 0,
        rotationWritten ? 1 : 0, fovWritten ? 1 : 0,
        (unsigned long long)generation, reinterpret_cast<void *>(owner),
        GetCurrentThreadId());
    DirectVmdCamera_ResetMainThread("camera-transform-write-failed");
    return;
  }

  s_directVmdCameraLastUnityFrame = frameNumber;
  s_directVmdCameraLastSequence = frame.sequence;
  bool periodicLog = s_directVmdCameraLogFrame == INT_MIN;
  if (frameNumber >= 0 && s_directVmdCameraLogFrame != INT_MIN)
    periodicLog = frameNumber - s_directVmdCameraLogFrame >= 120;
  if (periodicLog) {
    s_directVmdCameraLogFrame = frameNumber;
    const VmdVec3 flatWorldPosition = DirectVmdSub(
        desired.position, targetSideWorldOffset);
    Log("[P6-CAMERA-WRITE] frame=%d sourceFrame=%.6f sampleSeq=%llu "
        "source=%s interest=(%.4f,%.4f,%.4f) distance=%.4f "
        "flatWorldP=(%.4f,%.4f,%.4f) "
        "targetSideRootOffset=(%.4f,%.4f,%.4f) "
        "worldP=(%.4f,%.4f,%.4f) worldR=(%.7f,%.7f,%.7f,%.7f) "
        "fovRaw=%.3f fovBias=%.3f fov=%.3f perspective=%u "
        "motionScale=%.8f cameraScale=%.8f naturalBindHeight=%.6f "
        "heightScaleAuto=%.6f heightBias=%.6f heightScale=%.6f "
        "framing=%s "
        "anchorP=(%.4f,%.4f,%.4f) generation=%llu owner=%p "
        "terrainFollow=%d sameClock=1 muscleTime=0 currentTime=0 "
        "stage=postFinalIK tid=%lu",
        frameNumber, frame.sourceFrame,
        (unsigned long long)frame.sequence,
        frame.camera.fromOverride ? "override" : "direct-vmd",
        frame.camera.interest.x, frame.camera.interest.y,
        frame.camera.interest.z, frame.camera.distance,
        flatWorldPosition.x, flatWorldPosition.y,
        flatWorldPosition.z,
        targetSideWorldOffset.x, targetSideWorldOffset.y,
        targetSideWorldOffset.z,
        desired.position.x, desired.position.y, desired.position.z,
        desired.rotation.x, desired.rotation.y, desired.rotation.z,
        desired.rotation.w, frame.camera.fov, framing.fovBias, fov,
        static_cast<unsigned>(frame.camera.perspective), motionScale,
        framing.positionScale, naturalBindHeight,
        framing.automaticHeightScale, g_camHeightBias,
        framing.effectiveHeightScale,
        framing.legacyOverride ? "legacy-override" : "direct-shared",
        anchor.position.x, anchor.position.y, anchor.position.z,
        (unsigned long long)generation, reinterpret_cast<void *>(owner),
        terrainFollowActive ? 1 : 0,
        GetCurrentThreadId());
  }
}

