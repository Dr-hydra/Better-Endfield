#pragma once

#define EIEM_VERSION_MAJOR 0
#define EIEM_VERSION_MINOR 2
#define EIEM_VERSION_PATCH 2

#define EIEM_STRINGIFY2(x) #x
#define EIEM_STRINGIFY(x) EIEM_STRINGIFY2(x)
#define EIEM_VERSION EIEM_STRINGIFY(EIEM_VERSION_MAJOR) "." EIEM_STRINGIFY(EIEM_VERSION_MINOR) "." EIEM_STRINGIFY(EIEM_VERSION_PATCH)

static HANDLE g_logHandle = INVALID_HANDLE_VALUE;
static CRITICAL_SECTION g_logLock;

void Log(const char *fmt, ...) {
  if (g_logHandle == INVALID_HANDLE_VALUE)
    return;
  EnterCriticalSection(&g_logLock);
  char buf[4096];
  va_list args;
  va_start(args, fmt);
  int len = vsnprintf(buf, sizeof(buf) - 2, fmt, args);
  va_end(args);
  if (len < 0)
    len = 0;
  else if (len > static_cast<int>(sizeof(buf) - 2))
    len = static_cast<int>(sizeof(buf) - 2);
  buf[len] = '\0';

  if (len >= 5 && buf[0] == '[' && buf[1] == 'P') {
    int separator = 2;
    while (separator < len && buf[separator] >= '0' &&
           buf[separator] <= '9') {
      ++separator;
    }
    if (separator > 2 && separator < len && buf[separator] == '-') {
      memmove(buf + 1, buf + separator + 1,
              static_cast<size_t>(len - separator));
      len -= separator;
    }
  }
  buf[len] = '\n';
  len++;
  DWORD written;
  WriteFile(g_logHandle, buf, len, &written, NULL);
  LeaveCriticalSection(&g_logLock);
}


static MotionBackendStateMachine g_motionBackend;

static std::atomic<uint32_t> g_guiSelectedMotionBackend{
    static_cast<uint32_t>(MotionBackend::DirectVmd)};

static inline MotionBackend GuiSelectedMotionBackend() {
  const uint32_t selected =
      g_guiSelectedMotionBackend.load(std::memory_order_acquire);
  return selected == static_cast<uint32_t>(MotionBackend::Muscle)
             ? MotionBackend::Muscle
             : MotionBackend::DirectVmd;
}

static inline void GuiSelectMotionBackend(MotionBackend backend) {
  const MotionBackend selected = backend == MotionBackend::Muscle
                                     ? MotionBackend::Muscle
                                     : MotionBackend::DirectVmd;
  g_guiSelectedMotionBackend.store(static_cast<uint32_t>(selected),
                                   std::memory_order_release);
}

static void *g_transformClass = nullptr;
static void *g_animatorClass = nullptr;
static void *g_gameObjectClass = nullptr;
static void *g_componentClass = nullptr;

static void *g_transform_get_localRotation = nullptr;
static void *g_transform_set_localRotation = nullptr;
static void *g_transform_get_localPosition = nullptr;
static void *g_transform_set_localPosition = nullptr;
static void *g_transform_get_childCount = nullptr;
static void *g_transform_GetChild = nullptr;
static void *g_transform_Find = nullptr;
static void *g_transform_get_parent = nullptr;
static void *g_transform_get_position = nullptr;

static void *g_animator_GetBoneTransform = nullptr;
static void *g_animator_get_avatar = nullptr;
static void *g_animator_get_isHuman = nullptr;
static void *g_animator_get_enabled = nullptr;
static void *g_animator_set_enabled = nullptr;
static void *g_animator_Rebind = nullptr;
static void *g_animator_Update = nullptr;
static void *g_animator_SetBoneLocalRotation =
    nullptr;

static void *g_humanPoseHandlerClass = nullptr;
static void *g_humanPoseHandler_ctor = nullptr;
static void *g_humanPoseHandler_SetHumanPose = nullptr;
static void *g_humanPoseHandler_GetHumanPose = nullptr;
static void *g_humanPoseHandler_Dispose = nullptr;

typedef void (*fn_SetHumanPose_compiled)(void *self, void *humanPose,
                                         void *methodInfo);
static fn_SetHumanPose_compiled g_icall_SetHumanPose = nullptr;
typedef void (*fn_InternalAvatarPose)(void *nativePtr, void *array, int count);
static fn_InternalAvatarPose g_icall_SetInternalAvatarPose = nullptr;
static fn_InternalAvatarPose g_icall_GetInternalAvatarPose = nullptr;
typedef void (*fn_InternalHumanPose)(void *nativePtr, void *bodyPos,
                                     void *bodyRot, void *muscles);
static fn_InternalHumanPose g_icall_SetInternalHumanPose = nullptr;

static fn_InternalAvatarPose orig_GetInternalAvatarPose = nullptr;
static volatile bool g_trojanActive = false;
static void *g_trojanHookTarget = nullptr;
static volatile float g_mmdMuscles[95] = {};
static volatile float g_mmdBodyPos[3] = {};
static volatile float g_mmdBodyRot[4] = {0, 0, 0,
                                         1};
static volatile float g_mmdArmBoneRots[ARM_BONE_COUNT * 4] =
    {};
static volatile bool g_mmdHasArmBones = false;
static volatile float g_mmdFingerBoneRots[FINGER_BONE_COUNT * 4] =
    {};
static volatile bool g_mmdHasFingerBones = false;
static void *g_fingerTransforms[FINGER_BONE_COUNT] = {};
static bool g_fingerTransformsResolved = false;
static float g_gameFingerRest[FINGER_BONE_COUNT * 4] = {};
static bool g_fingerRestCaptured = false;
static volatile bool g_mmdHasMuscles = false;

static bool s_ikDisabled = false;
#define MAX_IK 4
static void *s_bipedIK[MAX_IK] = {};
static int s_bipedIKCount = 0;
static void *s_grounderIK[MAX_IK] = {};
static int s_grounderIKCount = 0;
static void *s_followDamper[4] = {};
static int s_followDamperCount = 0;
static void *s_animatorMono = nullptr;
static void *s_lookAt[MAX_IK] = {};
static int s_lookAtCount = 0;
static bool s_eyeIKDisabled = false;

static void *s_cinemachineBrain = nullptr;
static VmdFile *g_cameraVmd = nullptr;
static CameraPlayer g_cameraPlayer;
static bool g_cameraActive = false;
static bool g_cameraEnabled = true;
static bool g_footIKEnabled = true;
static float g_playbackSpeed = 1.0f;
static bool g_playbackLoop = false;
static bool g_cameraNeedsCapture = false;
static float g_origFov = 0.0f;
static Vec3 g_charWorldPos = {0, 0, 0};
static float g_charYaw = 0.0f;
static Vec3 g_camInitHipsWorldPos = {0,0,0};
static Vec3 g_hipsWorldDelta = {0,0,0};
static Vec3 g_camInitInterest = {0,0,0};
static float g_camInitHipsYaw = 0.0f;
static float g_camHipsYawDelta = 0.0f;
static float g_camSegmentYawOffset = 0.0f;
static Vec3 g_camPrevInterest = {0,0,0};
static bool g_camPrevInterestValid = false;
static float g_camInitHeadWorldYaw = 0.0f;
static Vec3 g_headWorldPos = {0,0,0};
static Vec3 g_headForward = {0,0,1};
static bool g_camTestMode = false;
static float g_charHeight = 0.0f;
static float g_camRefHeight = 0.0f;
static float g_camHeightScale = 1.0f;

static float g_posOffsetX = 0.0f;
static float g_posOffsetY = 0.0f;
static float g_posOffsetZ = 0.0f;
static float g_yawOffsetDeg = 0.0f;
static float g_camHeightBias = 0.0f;
static float g_motionScale = 1.0f;

static float g_scaleSpine = 1.0f;
static float g_scaleHead  = 1.0f;
static float g_scaleLArm  = 1.0f;
static float g_scaleRArm  = 1.0f;
static float g_scaleLegs  = 1.0f;
static float g_scaleFingers = 1.0f;
static float g_splayBlend = 0.7f;

static float GetMuscleScale(int stdIdx) {
  float part = 1.0f;
  if (stdIdx <= 8)                     part = g_scaleSpine;
  else if (stdIdx <= 14)               part = g_scaleHead;
  else if (stdIdx >= 21 && stdIdx <= 36) part = g_scaleLegs;
  else if (stdIdx >= 37 && stdIdx <= 44) part = g_scaleLArm;
  else if (stdIdx >= 45 && stdIdx <= 52) part = g_scaleRArm;
  return g_motionScale * part;
}

static void CaptureAndDisableCinemachine();

static void ResetCameraState() {
  g_cameraActive = false;
  g_cameraNeedsCapture = false;
  g_charWorldPos = {0, 0, 0};
  g_charYaw = 0.0f;
  g_camInitHipsWorldPos = {0, 0, 0};
  g_hipsWorldDelta = {0, 0, 0};
  g_camInitInterest = {0, 0, 0};
  g_camInitHipsYaw = 0.0f;
  g_camHipsYawDelta = 0.0f;
  g_camSegmentYawOffset = 0.0f;
  g_camPrevInterest = {0, 0, 0};
  g_camPrevInterestValid = false;
  g_camInitHeadWorldYaw = 0.0f;
  g_headWorldPos = {0, 0, 0};
  g_headForward = {0, 0, 1};
  g_charHeight = 0.0f;
  g_camHeightScale = 1.0f;
  g_cameraPlayer.SetVmd(nullptr);
}
static void RestoreCinemachine();
static void ApplyCameraFrame(float timeSec);
static void ResetSkirtState();

#define MAX_BBC 16
static void *s_bbcInstances[MAX_BBC] = {};
static int s_bbcCount = 0;
static bool s_bbcMethodsResolved = false;
static int s_skirtBBCIndex = -1;
static void *s_bbc_ResetCloth = nullptr;
static void *s_bbc_SetAnimPoseRatio = nullptr;
static void *s_bbc_SetSimWeight = nullptr;
static void *s_bbc_BuildAndRun = nullptr;
static void *s_bbc_SetSkipWriting = nullptr;
static void *s_bbc_SetTimeScale = nullptr;

static void **g_slotAddr =
    nullptr;
static void *g_slotOrigGet = nullptr;
static void *g_slotSetFn = nullptr;

static void *g_gameObject_get_transform = nullptr;
static void *g_component_get_gameObject = nullptr;
static void *g_component_get_transform = nullptr;
static void *g_gameObject_GetComponent = nullptr;
static void *g_object_get_name = nullptr;

static HWND g_gameHwnd = nullptr;

static int g_guiToggleVK = VK_INSERT;
static bool g_pluginActive = true;

#define OFF_BIPEDIK_FIX_TRANSFORMS    0x18
#define OFF_BIPEDIK_SOLVERS           0x40

#define OFF_SOLVERS_LEFT_FOOT         0x10
#define OFF_SOLVERS_RIGHT_FOOT        0x18
#define OFF_SOLVERS_LEFT_HAND         0x20
#define OFF_SOLVERS_RIGHT_HAND        0x28
#define OFF_SOLVERS_SPINE             0x30
#define OFF_SOLVERS_LOOKAT            0x38
#define OFF_SOLVERS_AIM               0x40
#define OFF_SOLVERS_PELVIS             0x48

#define OFF_IKSOLVER_IKPOS_X          0x14
#define OFF_IKSOLVER_IKPOS_Y          0x18
#define OFF_IKSOLVER_IKPOS_Z          0x1C
#define OFF_IKSOLVER_IKPOS_WEIGHT     0x20
#define OFF_IKSOLVER_ON_PRE_UPDATE    0x38
#define OFF_IKSOLVER_ON_POST_UPDATE   0x40

#define OFF_IKSOLVER_IKROT_WEIGHT     0x60
#define OFF_IKSOLVER_IKROT_X          0x64
#define OFF_IKSOLVER_IKROT_Y          0x68
#define OFF_IKSOLVER_IKROT_Z          0x6C
#define OFF_IKSOLVER_IKROT_W          0x70

#define OFF_IKTRIG_TARGET             0x58
#define OFF_IKTRIG_BONE1              0x80
#define OFF_IKTRIG_BONE2              0x88
#define OFF_IKTRIG_BONE3              0x90
#define OFF_IKPOINT_TRANSFORM         0x10
#define OFF_IKLIMB_BEND_MODIFIER      0xAC
#define OFF_IKLIMB_BEND_WEIGHT        0xB4
#define OFF_IKLIMB_BEND_GOAL          0xB8

#define OFF_BIPED_PELVIS_POS_WEIGHT   0x38
#define OFF_BIPED_PELVIS_ROT_WEIGHT   0x54
#define OFF_BIPED_PELVIS_POS_OFFSET_X 0x20
#define OFF_BIPED_PELVIS_POS_OFFSET_Y 0x24
#define OFF_BIPED_PELVIS_POS_OFFSET_Z 0x28
#define OFF_BIPED_PELVIS_ROT_OFFSET_X 0x3C
#define OFF_BIPED_PELVIS_ROT_OFFSET_Y 0x40
#define OFF_BIPED_PELVIS_ROT_OFFSET_Z 0x44

#define OFF_GROUNDER_WEIGHT           0x18
#define OFF_GROUNDER_MAINTAIN_WEIGHT  0x1C
#define OFF_GROUNDER_ADSORB_WEIGHT    0x20
#define OFF_GROUNDER_SOLVER           0x28
#define OFF_GROUNDER_INITIATED        0x48
#define OFF_GROUNDER_BIPED_IK         0x50
#define OFF_GROUNDER_SPINE_BEND       0x58
#define OFF_GROUNDER_SPINE_SPEED      0x5C
#define OFF_GROUNDER_LAST_WEIGHT      0x94
#define OFF_GROUNDER_LAST_ADSORB      0x98
#define OFF_GROUNDER_RIGHT_FOOT_Y     0x9C
#define OFF_GROUNDER_LEFT_FOOT_Y      0xA0
#define OFF_GROUNDER_RIGHT_FOOT_ORI   0xA4
#define OFF_GROUNDER_LEFT_FOOT_ORI    0xA8

#define OFF_GROUNDING_HEIGHT_OFFSET   0x70
#define OFF_GROUNDING_LEGS            0xC8
#define OFF_GROUNDING_IS_GROUNDED     0xD8
#define OFF_GROUNDING_LEG_IS_GROUNDED 0x10
#define OFF_GROUNDING_LEG_IK_POSITION 0x14
#define OFF_GROUNDING_LEG_HEIGHT_FROM_GROUND 0x34
#define OFF_GROUNDING_LEG_HEEL_HIT_POINT 0x298
#define OFF_GROUNDING_LEG_CALCULATED_FOOT 0x2D8
#define OFF_GROUNDING_LEG_LAST_HIT_POINT 0x2E4
#define OFF_GROUNDING_LEG_LAST_HIT_NORMAL 0x300
#define OFF_GROUNDING_LEG_IS_IN_STAIR 0x30C

static void *g_origIkTrigOnUpdate = nullptr;

struct EnumWindowCtx { DWORD pid; HWND result; };

static BOOL CALLBACK EnumWindowProc(HWND hwnd, LPARAM lParam) {
  auto *ctx = reinterpret_cast<EnumWindowCtx *>(lParam);
  DWORD wndPid = 0;
  GetWindowThreadProcessId(hwnd, &wndPid);
  if (wndPid != ctx->pid) return TRUE;

  char cls[64] = {};
  GetClassNameA(hwnd, cls, sizeof(cls));
  if (strcmp(cls, "UnityWndClass") == 0 && IsWindowVisible(hwnd)) {
    ctx->result = hwnd;
    return FALSE;
  }
  return TRUE;
}

static inline HWND FindGameWindow() {
  EnumWindowCtx ctx = {};
  ctx.pid = GetCurrentProcessId();
  ctx.result = nullptr;
  EnumWindows(EnumWindowProc, reinterpret_cast<LPARAM>(&ctx));
  return ctx.result;
}

static char g_muscleAnimPath[512] = "";
static char g_cameraVmdPath[512] = "";
static bool g_cameraOverrideExplicit = false;
static char g_morphVmdPath[512] = "";
static char g_footIkVmdPath[512] = "";
static VmdFile *g_footIkVmd = nullptr;
static bool g_footIkResolved = false;
static char g_directVmdPath[512] = "";

struct AudioPlayer;
static AudioPlayer *g_audioPlayer = nullptr;
static char g_audioPath[512] = "";
static wchar_t g_audioPathW[512] = L"";
static bool g_audioEnabled = true;
static bool g_audioIsClock = false;
static float g_audioOffset = 0.0f;
static int g_audioVolume = 1000;
static bool g_audioPendingStart = false;

static volatile bool g_guiVisible = false;
static HWND g_guiHwnd = nullptr;
static volatile bool g_guiRunning = false;

static volatile bool g_updateAvailable = false;
static volatile bool g_updateDismissed = false;
static volatile bool g_updateChecking = false;
static volatile bool g_updateCheckFailed = false;
static volatile bool g_updateIsLatest = false;
static volatile DWORD g_updateResultTime = 0;
static char g_latestVersion[32] = {};
static char g_updateUrl[512] = {};
static char g_updateChangelog[2048] = {};

static bool g_disclaimerAccepted = false;

static void* g_cursorShowAction = nullptr;
static void* g_cursorHideAction = nullptr;
static void* g_actionInvokeMethod = nullptr;

static void *g_skinnedMeshRendererClass = nullptr;
static void *g_smr_get_sharedMesh = nullptr;
static void *g_mesh_get_blendShapeCount = nullptr;
static void *g_mesh_GetBlendShapeName = nullptr;
static void *g_smr_GetBlendShapeWeight = nullptr;
static void *g_smr_SetBlendShapeWeight = nullptr;
static void *g_smr_get_bones =
    nullptr;

static void *g_cameraClass = nullptr;
static void *g_camera_get_main = nullptr;
static void *g_camera_get_fieldOfView = nullptr;
static void *g_camera_set_fieldOfView = nullptr;

typedef void (*CamGetPosRot_t)(void *, float *);
static CamGetPosRot_t g_camGetPos = nullptr;
static CamGetPosRot_t g_camGetRot = nullptr;
static void (*g_camSetPos)(void *, float *) = nullptr;
static void (*g_camSetRot)(void *, float *) = nullptr;

typedef void (*TransformSet_t)(void *, float *);
static TransformSet_t g_origSetPos = nullptr;
static TransformSet_t g_origSetRot = nullptr;
static TransformSet_t g_origSetLocalPos = nullptr;
static TransformSet_t g_origSetLocalRot = nullptr;
static volatile bool g_camSelfWrite = false;
static void *g_camHookTransform = nullptr;

static void Hook_SetPos(void *t, float *v) {
  if (g_cameraActive && t == g_camHookTransform && !g_camSelfWrite) return;
  g_origSetPos(t, v);
}
static void Hook_SetRot(void *t, float *v) {
  if (g_cameraActive && t == g_camHookTransform && !g_camSelfWrite) return;
  g_origSetRot(t, v);
}
static void Hook_SetLocalPos(void *t, float *v) {
  if (g_cameraActive && t == g_camHookTransform && !g_camSelfWrite) return;
  g_origSetLocalPos(t, v);
}
static void Hook_SetLocalRot(void *t, float *v) {
  if (g_cameraActive && t == g_camHookTransform && !g_camSelfWrite) return;
  g_origSetLocalRot(t, v);
}

static void *g_skeletalMorphCore = nullptr;
static void *g_skeletalMorphCoreClass = nullptr;
typedef void(__fastcall *SMCUpdate_t)(void *__this, float deltaTime,
                                      void *methodInfo);
static SMCUpdate_t g_origSMCUpdate = nullptr;
static float g_testMorphWeight = 1.0f;

typedef void(__fastcall *ApplyBoneTrans_t)(void *__this, bool param1,
                                           float param2, uint64_t jobHandle,
                                           void *methodInfo);
static ApplyBoneTrans_t g_origApplyBoneTrans = nullptr;
static void __fastcall Hooked_ApplyBoneTrans(void *__this, bool param1,
                                             float param2, uint64_t jobHandle,
                                             void *methodInfo);

static void SafeSetLocalRotation(void *transform, Quat q);
static Quat SafeGetLocalRotation(void *transform);
static void SafeSetLocalPosition(void *transform, Vec3 p);
static Vec3 SafeGetLocalPosition(void *transform);

#pragma pack(push, 1)
struct MorphBoneEntry {
  int32_t boneNameHash;
  int32_t boneID;
  float deltaPosX;
  float deltaPosY;
  float deltaPosZ;
  float deltaRotX;
  float deltaRotY;
  float deltaRotZ;
  float pad[3];
};
#pragma pack(pop)
static_assert(sizeof(MorphBoneEntry) == 44, "MorphBoneEntry must be 44 bytes");

static const int MAX_BIGLIST = 8192;
static MorphBoneEntry g_bigList[MAX_BIGLIST];
static int g_bigListLen = 0;
static bool g_bigListReady = false;

static int g_boneIdOffset =
    0;
static bool g_maskReady = false;

static const int MAX_GROUPS = 300;
struct MorphGroup {
  int startIdx;
  int count;
};
static MorphGroup g_morphGroups[MAX_GROUPS];
static int g_morphGroupCount = 0;

static bool g_expressionCaptured = false;
static MorphBoneEntry g_capturedExpression[MAX_BIGLIST];
static int g_capturedLen = 0;

struct FaceBoneSnapshot {
  float px, py, pz;
  float rx, ry, rz, rw;
  void *transform;
};
static const int MAX_FACE_BONES = 256;
static FaceBoneSnapshot g_faceBones[MAX_FACE_BONES];
static FaceBoneSnapshot g_faceRestPose[256];
static int g_faceBoneCount = 0;
static bool g_faceBonesCaptured = false;
static bool g_faceBoneTouched[MAX_FACE_BONES] = {};
static volatile bool g_faceTestActive = false;
static int g_faceTestFrame = 0;
static void *g_faceGetLocalPos = nullptr;
static void *g_faceSetLocalPos = nullptr;
static void *g_faceGetLocalRot = nullptr;
static void *g_faceSetLocalRot = nullptr;
static void **g_faceBoneRefs = nullptr;

static int
    g_boneIDToIdx[512];
static int g_boneIDMapCount = 0;
static bool g_boneMapReady = false;

static int OFF_allMorphs = -1;
static int OFF_poseCache = -1;
static int OFF_bigList = -1;
static int OFF_nativeHashMap = -1;
static int OFF_shaderProps = -1;
static int OFF_baseShaderProps = -1;
static int OFF_dirtyShaderProps = -1;
static int OFF_morphBSDirty = -1;
static int OFF_allMorphBoneDirty = -1;
static int OFF_avatarData = -1;
static int OFF_allBonesTransforms = -1;
static int OFF_boneIDToIdx = -1;
static int OFF_phonemesWeights = -1;
static int OFF_mainEmotion = -1;
static int OFF_poseDictMorph = -1;
static int OFF_microExprWeights =
    -1;
static bool g_smcOffsetsResolved = false;

static int OFF_pcEntity = -1;
static int OFF_morphMappingNames =
    -1;
static int OFF_entityComplexAnim = -1;
static int OFF_complexAnimAnimator = -1;
static int OFF_smcEyeLookAt = -1;
static int OFF_skMorphCompCore = -1;

static void *g_findFloorMethod = nullptr;
static void *g_computeFloorDistMethod = nullptr;
static void *g_physicsRaycastMethod = nullptr;
static int g_offCurrentFloor = 0x2E8;
static int g_offBaseCompEntity = 0x50;
static int g_offMovementGrounder = 0x410;
static float g_groundDeltaY = 0.0f;


#define IL2CPP_STR_LEN      0x10
#define IL2CPP_STR_CHARS    0x14
#define IL2CPP_ARRAY_LEN    0x18
#define IL2CPP_ARRAY_DATA   0x20
#define IL2CPP_LIST_ITEMS   0x10
#define IL2CPP_LIST_SIZE    0x18
#define IL2CPP_BOXED_DATA   16

static int OFF_emoPose = -1;
static int OFF_poseMouth = -1;
static int OFF_poseBrowL = -1;
static int OFF_mcvCtrlName = -1;
static int OFF_mcvValue = -1;

static int SafeOff(int resolved, int fallback, const char *name) {
  if (resolved >= 0)
    return resolved;
  static unsigned s_warnedMask = 0;
  unsigned h = 0;
  for (const char *p = name; *p; p++)
    h = h * 31 + (unsigned)*p;
  unsigned bit = 1u << (h & 31);
  if (!(s_warnedMask & bit)) {
    s_warnedMask |= bit;
    Log("[WARN] Using fallback offset 0x%X for %s (dynamic resolution failed)",
        fallback, name);
  }
  return fallback;
}
