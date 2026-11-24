#pragma once

#include <atomic>
#include <cmath>
#include <cstdint>
#include <climits>
#include <algorithm>
#include <map>
#include <string>
#include <vector>

enum class GhostRigCleanupReason : uint32_t {
  None = 0,
  UserDisabled,
  Stop,
  CharacterSwitch,
  SceneChanged,
  PluginDisabled,
  PluginUnload,
  WindowClosing,
  Recapture,
  VmdReload,
  GenerationMismatch,
  CreateFailed,
};

static const char *GhostRig_CleanupReasonName(GhostRigCleanupReason reason) {
  switch (reason) {
  case GhostRigCleanupReason::UserDisabled: return "user-disabled";
  case GhostRigCleanupReason::Stop: return "stop";
  case GhostRigCleanupReason::CharacterSwitch: return "character-switch";
  case GhostRigCleanupReason::SceneChanged: return "scene-changed";
  case GhostRigCleanupReason::PluginDisabled: return "plugin-disabled";
  case GhostRigCleanupReason::PluginUnload: return "plugin-unload";
  case GhostRigCleanupReason::WindowClosing: return "window-closing";
  case GhostRigCleanupReason::Recapture: return "recapture";
  case GhostRigCleanupReason::VmdReload: return "vmd-reload";
  case GhostRigCleanupReason::GenerationMismatch: return "generation-mismatch";
  case GhostRigCleanupReason::CreateFailed: return "create-failed";
  default: return "none";
  }
}

static void *s_ghostGameObjectCtor = nullptr;
static void *s_ghostTransformSetParent = nullptr;
static void *s_ghostObjectGetHideFlags = nullptr;
static void *s_ghostObjectSetHideFlags = nullptr;
static void *s_ghostObjectGetInstanceId = nullptr;
static void *s_ghostObjectDestroy = nullptr;
static void *s_ghostObjectImplicit = nullptr;
static void *s_ghostGameObjectGetComponents = nullptr;
static void *s_ghostSceneGetActiveScene = nullptr;
static void *s_ghostTimeGetFrameCount = nullptr;
static void *s_ghostAvatarGetHumanDescription = nullptr;
static void *s_ghostHumanDescriptionClass = nullptr;
static void *s_ghostSkeletonBoneClass = nullptr;
static void *s_ghostGrounderUpdate = nullptr;
static void *s_ghostGrounderResetPosition = nullptr;
static void *s_ghostOrigGrounderOnSolverUpdate = nullptr;
static void *s_ghostOrigGrounderOnPostSolverUpdate = nullptr;
static int s_ghostHumanSkeletonOffset = -1;
static int s_ghostHumanDescriptionSize = 0;
static int s_ghostSkeletonNameOffset = -1;
static int s_ghostSkeletonPositionOffset = -1;
static int s_ghostSkeletonRotationOffset = -1;
static int s_ghostSkeletonScaleOffset = -1;
static int s_ghostSkeletonStride = 0;

static constexpr int GHOST_NODE_COUNT =
    1 + static_cast<int>(DIRECT_VMD_BONE_COUNT);
static constexpr int GHOST_HIDE_FLAGS = 1 | 4 | 16;

static const char *GhostRig_NodeName(int index) {
  return index == 0 ? "GhostRoot"
                    : kDirectVmdBoneSpecs[index - 1].name;
}

static int GhostRig_NodeParent(int index) {
  if (index <= 0)
    return -1;
  const int semanticParent = kDirectVmdBoneSpecs[index - 1].parent;
  return semanticParent < 0 ? 0 : semanticParent + 1;
}

static int GhostRig_NodeIndex(DirectVmdBoneId id) {
  return 1 + static_cast<int>(id);
}

struct GhostRigNode {
  uint32_t gameObjectHandle;
  uint32_t transformHandle;
  DirectVmdBindNodePod bind;
};

struct GhostRigTargetBone {
  uint32_t transformHandle;
  VmdQuaternion bindOwnerRotation;
  VmdQuaternion bindLocalRotation;
  VmdQuaternion sourceToTargetOwnerAlignment;
  VmdQuaternion lastDesiredWorldRotation;
  int lastDesiredFrame;
  int humanBone;
  bool resolved;
  bool missingLogged;
  bool hasStanceAlignment;
  bool hasFullFrameAlignment;
  bool lastDesiredValid;
};

struct GhostRigLegRuntime {
  uint32_t solverHandle;
  uint32_t savedTargetHandle;
  uint32_t savedOnPreUpdateHandle;
  uint32_t savedOnPostUpdateHandle;
  int preparedFrame;
  int innerWriteFrame;
  int postTerrainResolveFrame;
  void *onUpdateMethodInfo;
  bool modeKnown;
  bool requestedIkEnabled;
  bool effectiveIkEnabled;
  bool toeAimPending;
  bool toeAimWritten;
  bool postSolveDiagnosticPending;
  bool savedBendStateValid;
  bool savedExternalStateValid;
  bool savedTargetPresent;
  bool savedOnPreUpdatePresent;
  bool savedOnPostUpdatePresent;
  bool externalOwnershipIsolated;
  int savedBendModifier;
  float savedBendWeight;
  int kneeNormalOffset;
  int kneeBackupOffset;
  bool kneeReferenceReady;
  VmdVec3 kneeDefaultLocalNormal;
  DirectVmdKneeReferencePod sourceKnee;
  float kneeMixWeight;
  DirectVmdKneeMix kneeMix;
  DirectVmdKneeSolverOverride kneeOverride;
  DirectVmdReachProjection reach;
  DirectVmdToeAimResult toeAim;
  VmdQuaternion footWorldRotation;
  VmdQuaternion toeWorldRotation;
  VmdQuaternion footControlWorldRotation;
  VmdQuaternion toeControlWorldRotation;
  VmdQuaternion terrainNormalRotation;
  VmdQuaternion footSeedLocalRotation;
  VmdQuaternion toeSeedLocalRotation;
  VmdVec3 footIkParentWorldPosition;
  VmdVec3 toeIkWorldPosition;
  VmdVec3 bendDirection;
  bool terrainContactApplied;
  uint64_t modeSwitchCount;
  uint64_t solverWriteCount;
  uint64_t weightClearCount;
  uint64_t fkRotationWriteCount;
  uint64_t toeAimWriteCount;
  uint64_t postTerrainResolveCount;
};

enum class GhostRigAuxSolverId : uint32_t {
  LeftHand = 0,
  RightHand,
  Spine,
  LookAt,
  Aim,
  Pelvis,
  Count,
};

static constexpr uint32_t GHOST_AUX_SOLVER_COUNT =
    static_cast<uint32_t>(GhostRigAuxSolverId::Count);

struct GhostRigAuxSolverRuntime {
  uint32_t handle;
  int positionWeightOffset;
  int rotationWeightOffset;
  int positionOffsetOffset;
  int rotationOffsetOffset;
  float savedPositionWeight;
  float savedRotationWeight;
  VmdVec3 savedPositionOffset;
  VmdVec3 savedRotationOffset;
  bool savedPositionValid;
  bool savedRotationValid;
  bool savedPositionOffsetValid;
  bool savedRotationOffsetValid;
};

struct GhostRigPelvisNodeRuntime {
  uint32_t handle;
  VmdVec3 bindLocalPosition;
  VmdVec3 restoreLocalPosition;
  bool bindFromAvatar;
  bool restoreValid;
  uint64_t writeCount;
  uint64_t postCorrectionCount;
  float lastCorrectionDistance;
  int lastCorrectionFrame;
  int lastLogFrame;
  char name[96];
};

struct GhostRigGrounderRuntime {
  uint32_t handle = 0;
  float savedWeight = 0.0f;
  float savedMaintainWeight = 0.0f;
  float savedAdsorbWeight = 0.0f;
  float savedSpineBend = 0.0f;
  float savedSpineSpeed = 0.0f;
  bool savedEnabled = false;
  bool savedEnabledValid = false;
  bool savedFieldsValid = false;
  bool ownershipCaptured = false;
  bool active = false;
  bool unavailableLogged = false;
  int activatedFrame = INT_MIN;
  int lastSolverEnterFrame = INT_MIN;
  int lastSolverExitFrame = INT_MIN;
  int lastPostSolverFrame = INT_MIN;
  int lastLogFrame = INT_MIN;
  uint64_t activationCount = 0;
  uint64_t solverUpdateCount = 0;
  uint64_t postSolverUpdateCount = 0;
  VmdVec3 cachedTarget[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  VmdQuaternion cachedRotation[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float cachedPositionWeight[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float cachedRotationWeight[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool cachedTargetValid[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  int cachedTargetFrame[DIRECT_VMD_LEG_SIDE_COUNT] = {INT_MIN, INT_MIN};
  VmdVec3 cachedLastHitPoint[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  VmdVec3 cachedHeelHitPoint[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  VmdVec3 cachedCalculatedFoot[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  VmdVec3 cachedLegIkPosition[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  VmdVec3 cachedLastHitNormal[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float cachedHeightFromGround[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool cachedLastHitValid[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool cachedHeelHitValid[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool cachedCalculatedFootValid[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool cachedLegIkPositionValid[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool cachedLastHitNormalValid[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool cachedRawLegGrounded[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool cachedLegInStair[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  int cachedRawGroundFrame[DIRECT_VMD_LEG_SIDE_COUNT] = {INT_MIN, INT_MIN};
  DirectVmdTerrainPlane
      cachedFootPhysicsPlane[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  DirectVmdDirectionalTerrainClearance
      cachedFootDirectionalClearance[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  int cachedFootPhysicsPlaneFrame[DIRECT_VMD_LEG_SIDE_COUNT] = {
      INT_MIN, INT_MIN};
  VmdVec3 previousFootPhysicsQuery[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool previousFootPhysicsQueryValid[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool footPhysicsReferenceValid[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float footPhysicsReferenceGroundY[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float footPhysicsMissSeconds[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  uint64_t footPhysicsQueryCount = 0;
  uint64_t footPhysicsHitCount = 0;
  uint64_t footRaycastMissCount = 0;
  uint64_t footRaycastInvokeFailureCount = 0;
  void *groundingRaycastDelegateClass = nullptr;
  void *groundingRaycastInvokeMethod = nullptr;
  bool footPhysicsApiFailureLogged = false;
  bool footRaycastBackendLogged = false;
  bool footRaycastFirstSampleLogged = false;
  float cachedGroundHeightOffset = 0.0f;
  bool cachedGroundHeightOffsetValid = false;
  void *groundingLegClass = nullptr;
  bool rawFieldStatusLogged = false;
  double cachedSourceFrame = 0.0;
  uint64_t cachedPlaybackCycle = 0;
  bool hybridTargetValid[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool hybridUsesGrounder[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool upwardClearanceApplied[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float hybridTargetY[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float upwardClearanceTargetY[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float nativeCorrectionY[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float authoredLift[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float authoredVerticalVelocity[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  DirectVmdGrounderHeightState
      stableNativeHeight[DIRECT_VMD_LEG_SIDE_COUNT];
  DirectVmdGrounderContactEdgeState
      contactEdge[DIRECT_VMD_LEG_SIDE_COUNT];
  DirectVmdGrounderHeightState stableRootOffset;
  float rootEnvironmentTargetOffset = 0.0f;
  float rootReachTargetOffset = 0.0f;
  DirectVmdRootReachReleaseState rootReachRelease;
  DirectVmdGrounderHeightSource
      nativeHeightSource[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float directNativeTargetY[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  int contactFrame = INT_MIN;
  int lastContactLogFrame = INT_MIN;
  bool contactTimelineValid = false;
  double contactSourceFrame = 0.0;
  uint64_t contactPlaybackCycle = 0;
};

struct GhostRigHandBindFrame {
  VmdQuaternion sourceToTargetOwnerAlignment = {0.0f, 0.0f, 0.0f, 1.0f};
  DirectVmdOrthonormalFrame sourceFrameGame;
  DirectVmdOrthonormalFrame targetFrameOwner;
  bool hasDirectionAlignment = false;
  bool hasFullFrameAlignment = false;
};

struct GhostRigTwistTargetRuntime {
  uint32_t transformHandle = 0;
  VmdQuaternion bindLocalRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  VmdVec3 axisParentLocal = {0.0f, 0.0f, 0.0f};
  bool bindAvailable = false;
  bool resolved = false;
  char transformName[96] = {};
};

struct GhostRigTwistChannelRuntime {
  GhostRigTwistTargetRuntime
      targets[DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL];
  uint64_t writeCount = 0;
};

enum class GhostRigState : uint32_t {
  Empty = 0,
  Creating,
  Alive,
  Destroying,
};

enum class GhostBindCaptureState : uint32_t {
  Empty = 0,
  WaitingForAvatar,
  ReadingAvatarMetadata,
  Ready,
  Blocked,
};

struct GhostRigRuntime {
  GhostRigState state;
  uint64_t generation;
  uintptr_t ownerCharacter;
  uint32_t ownerRootHandle;
  int sceneHandle;
  int lastAppliedFrame;
  int lastPoseLogFrame;
  int lastTargetAppliedFrame;
  int lastFingerAppliedFrame;
  int lastEyeAppliedFrame;
  int lastTwistAppliedFrame;
  int lastTargetLogFrame;
  int lastFingerLogFrame;
  int lastEyeLogFrame;
  int lastTwistLogFrame;
  int lastPostFinalIkLogFrame;
  int lastFinalIkSuppressedLogFrame;
  int lastRootAppliedFrame;
  int lastRootLogFrame;
  int lastTerrainFrame;
  int lastTerrainLogFrame;
  int lastTerrainFinalIkResolveLogFrame;
  int lastLegPreparedFrame;
  int lastLegLogFrame;
  uint64_t applyCount;
  uint64_t targetApplyCount;
  uint64_t targetRotationWriteCount;
  uint64_t fingerRotationWriteCount;
  uint64_t eyeRotationWriteCount;
  uint64_t twistRotationWriteCount;
  uint64_t postFinalIkDiagnosticReadCount;
  uint64_t finalIkSuppressedCount;
  uint64_t rootApplyCount;
  uint64_t rootWorldWriteCount;
  uint64_t rootRestoreCount;
  uint64_t hipsBindPositionWriteCount;
  uint64_t hipsPostCorrectionCount;
  uint64_t finalIkOriginalRunCount;
  uint64_t finalIkSafetySuppressCount;
  uint64_t cadenceUnityFrames;
  uint64_t cadenceSampleAdvances;
  uint64_t cadenceRepeatedSamples;
  double cadenceMaxSourceFrameStep;
  bool destroyIssued;
  bool anchorValid;
  bool playbackAnchorCaptured;
  bool restoreAnchorValid;
  bool rootPlacementValid;
  bool rootMotionOwned;
  bool terrainEnabledLast;
  bool terrainFrameValid;
  bool lastSampleAccepted;
  bool rootCycleSeen;
  VmdVec3 anchorPosition;
  VmdQuaternion anchorRotation;
  VmdVec3 restoreAnchorPosition;
  VmdQuaternion restoreAnchorRotation;
  VmdVec3 rootPlacementOffset;
  VmdVec3 lastDesiredRootPosition;
  VmdQuaternion lastDesiredRootRotation;
  bool lastDesiredRootValid;
  VmdVec3 lastHipsPositionBeforeCorrection;
  float lastHipsCorrectionDistance;
  int lastHipsCorrectionFrame;
  int lastHipsCorrectionLogFrame;
  GhostBindCaptureState bindState;
  uint64_t bindGeneration;
  uint64_t bindSourceRevision;
  eiem_source::Reference sourceReference;
  uintptr_t bindOwnerCharacter;
  int bindAttemptFrame;
  float leftLegLength;
  float rightLegLength;
  float targetLegLength;
  float targetNaturalBindHeight;
  float baseMotionScale;
  float motionScale;
  DirectVmdTerrainConfig terrainConfig;
  DirectVmdTerrainState terrainState;
  DirectVmdTerrainFrameOutput terrainFrame;
  DirectVmdTerrainPlane
      terrainCachedRawPlane[DIRECT_VMD_TERRAIN_FOOT_COUNT];
  float terrainAnkleClearance[DIRECT_VMD_TERRAIN_FOOT_COUNT];
  float terrainProbeAccumulator;
  bool terrainProbeCacheValid;
  int64_t terrainLastQpc;
  double terrainLastSourceFrame;
  uint64_t terrainLastPlaybackCycle;
  uint64_t terrainQueryCount;
  uint64_t terrainHitCount;
  uint64_t terrainFinalIkResolveCount;
  uint64_t lastSampleSequence;
  uint64_t lastPlaybackCycle;
  uint64_t lastAppliedRootCycle;
  double lastSourceFrame;
  DirectVmdPlaybackState lastSamplePlayback;
  bool lastLeftFootIkEnabled;
  bool lastRightFootIkEnabled;
  uint32_t finalIkBipedHandle;
  uint32_t eyeSmcHandle;
  bool eyeLookAtSaved;
  bool eyeLookAtSavedValue;
  char bindStatus[160];
  GhostRigHandBindFrame leftWristBindFrame;
  GhostRigHandBindFrame rightWristBindFrame;
  GhostRigTwistChannelRuntime
      twistChannels[DIRECT_VMD_PHASE6_TWIST_CHANNEL_COUNT];
  GhostRigNode nodes[GHOST_NODE_COUNT];
  GhostRigTargetBone targets[DIRECT_VMD_BONE_COUNT];
  bool targetNaturalBindAvailable[DIRECT_VMD_BONE_COUNT];
  GhostRigLegRuntime legs[DIRECT_VMD_LEG_SIDE_COUNT];
  GhostRigAuxSolverRuntime auxSolvers[GHOST_AUX_SOLVER_COUNT];
  GhostRigPelvisNodeRuntime pelvisNode;
  GhostRigGrounderRuntime grounder;
};

static GhostRigRuntime s_ghostRig = {};

static bool GhostRig_ClothMayOwnAnchor(void *transform) {
  if (!g_motionBackend.Is(MotionBackend::DirectVmd)) return false;
  if (s_ghostRig.ownerRootHandle && il2cpp_gchandle_get_target(s_ghostRig.ownerRootHandle) == transform) return false;
  for (const auto &target : s_ghostRig.targets)
    if (target.transformHandle && il2cpp_gchandle_get_target(target.transformHandle) == transform) return false;
  for (const auto &channel : s_ghostRig.twistChannels)
    for (const auto &target : channel.targets)
      if (target.transformHandle && il2cpp_gchandle_get_target(target.transformHandle) == transform) return false;
  if (s_ghostRig.pelvisNode.handle && il2cpp_gchandle_get_target(s_ghostRig.pelvisNode.handle) == transform) return false;
  return true;
}

static std::atomic<bool> s_ghostDesiredEnabled{false};
static std::atomic<bool> s_directVmdTerrainDesiredEnabled{true};
static std::atomic<bool> s_ghostOwnerKnown{false};
static std::atomic<uintptr_t> s_ghostRequestedOwnerId{0};
static std::atomic<uint64_t> s_ghostRequestedGeneration{1};
static std::atomic<uint32_t> s_ghostRequestedCleanupReason{
    static_cast<uint32_t>(GhostRigCleanupReason::None)};

static std::atomic<DWORD> s_ghostMainThreadId{0};
static std::atomic<bool> s_ghostAlivePublic{false};
static std::atomic<uint64_t> s_ghostPublicGeneration{0};
static std::atomic<uint64_t> s_ghostOrderSequence{0};
static std::atomic<uint64_t> s_ghostWrongThreadCount{0};
static std::atomic<uint64_t> s_ghostSnapshotRequest{0};
static std::atomic<uint32_t> s_ghostBindStatePublic{
    static_cast<uint32_t>(GhostBindCaptureState::Empty)};
static std::atomic<float> s_ghostMotionScalePublic{0.0f};

static int s_ghostObservedSceneHandle = INT_MIN;
static int s_ghostLastMaintenanceFrame = INT_MIN;
static int s_ghostOrderBudget = 0;
static uint64_t s_ghostHandledSnapshotRequest = 0;

static void *s_ghostOrigAnimatorPreLateTick = nullptr;
static void *s_ghostOrigAnimatorMove = nullptr;

static bool GhostRig_QueryFindFloorSample(
    uintptr_t ownerCharacter, VmdVec3 queryPosition, float stepDown,
    DirectVmdTerrainProbeHit *sample);
static void *GhostRig_GetOwnerGrounderBipedIK(
    uintptr_t ownerCharacter, void *bipedIK);
static bool GhostRig_ReadGhostWorldPose(
    DirectVmdBoneId id, VmdVec3 *position, VmdQuaternion *rotation);

static bool GhostRig_IsRequestedEnabled() {
  return s_ghostDesiredEnabled.load(std::memory_order_acquire);
}

static bool GhostRig_IsAliveForGui() {
  return s_ghostAlivePublic.load(std::memory_order_acquire);
}

static bool GhostRig_IsTerrainEnabledForGui() {
  return s_directVmdTerrainDesiredEnabled.load(
      std::memory_order_acquire);
}

static void GhostRig_SetTerrainEnabled(bool enabled) {
  const bool previous = s_directVmdTerrainDesiredEnabled.exchange(
      enabled, std::memory_order_acq_rel);
  if (previous != enabled) {
    Log("[P7-TERRAIN-REQUEST] enabled=%d previous=%d callerTid=%lu "
        "unityObjectsTouched=0",
        enabled ? 1 : 0, previous ? 1 : 0, GetCurrentThreadId());
  }
}

static uint64_t GhostRig_PublicGeneration() {
  return s_ghostPublicGeneration.load(std::memory_order_acquire);
}

static const char *GhostRig_PublicBindStateName() {
  switch (static_cast<GhostBindCaptureState>(
      s_ghostBindStatePublic.load(std::memory_order_acquire))) {
  case GhostBindCaptureState::WaitingForAvatar: return "waiting-avatar";
  case GhostBindCaptureState::ReadingAvatarMetadata: return "reading-metadata";
  case GhostBindCaptureState::Ready: return "ready";
  case GhostBindCaptureState::Blocked: return "blocked";
  default: return "empty";
  }
}

static float GhostRig_PublicMotionScale() {
  return s_ghostMotionScalePublic.load(std::memory_order_acquire);
}

static bool GhostRig_GetDirectChannelIdentity(
    uint64_t *generation, uintptr_t *ownerCharacter) {
  if (!generation || !ownerCharacter ||
      s_ghostRig.state != GhostRigState::Alive ||
      !s_ghostRig.lastSampleAccepted ||
      s_ghostRig.bindSourceRevision != s_directVmdSourceRevision.load(std::memory_order_acquire))
    return false;
  if (s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
          s_ghostRig.generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) !=
          s_ghostRig.ownerCharacter)
    return false;
  *generation = s_ghostRig.generation;
  *ownerCharacter = s_ghostRig.ownerCharacter;
  return *generation != 0 && *ownerCharacter != 0;
}

static VmdVec3 GhostRig_ApplyRootPlacementToWorldPosition(
    const VmdVec3 &position) {
  if (!s_ghostRig.rootPlacementValid)
    return position;
  return DirectVmdAdd(position, s_ghostRig.rootPlacementOffset);
}

static bool GhostRig_GetDirectCameraReference(
    DirectVmdWorldPosePod *anchor, float *motionScale,
    float *naturalBindHeight,
    VmdVec3 *targetSideWorldOffset, bool *terrainFollowActive,
    uint64_t *generation, uintptr_t *ownerCharacter) {
  if (!anchor || !motionScale || !naturalBindHeight ||
      !targetSideWorldOffset ||
      !terrainFollowActive || !generation || !ownerCharacter ||
      !s_ghostRig.anchorValid)
    return false;
  if (!GhostRig_GetDirectChannelIdentity(generation, ownerCharacter))
    return false;
  anchor->position = GhostRig_ApplyRootPlacementToWorldPosition(
      s_ghostRig.anchorPosition);
  anchor->rotation = s_ghostRig.anchorRotation;
  *targetSideWorldOffset = VmdVec3{};
  *terrainFollowActive = s_ghostRig.terrainEnabledLast;
  if (*terrainFollowActive) {
    const float rootOffset = s_ghostRig.terrainState.rootOffset;
    if (!DirectVmdFinite(rootOffset))
      return false;
    targetSideWorldOffset->y = rootOffset;
  }
  *motionScale = s_ghostRig.motionScale;
  *naturalBindHeight = s_ghostRig.targetNaturalBindHeight;
  return DirectVmdFinite(*motionScale) && *motionScale > 0.0f &&
         DirectVmdFinite(*naturalBindHeight) && *naturalBindHeight >= 0.0f;
}

static void GhostRig_RequestEnabled(bool enabled,
                                    GhostRigCleanupReason reason) {
  s_ghostDesiredEnabled.store(enabled, std::memory_order_release);
  s_ghostRequestedCleanupReason.store(static_cast<uint32_t>(reason),
                                      std::memory_order_release);
  uint64_t generation =
      s_ghostRequestedGeneration.fetch_add(1, std::memory_order_acq_rel) + 1;
  const uintptr_t owner =
      s_ghostRequestedOwnerId.load(std::memory_order_acquire);
  DirectVmdRuntime_SetTarget(generation, owner);
  DirectVmdRuntime_SetActive(enabled);
  Log("[P0-GHOST-REQUEST] enabled=%d reason=%s generation=%llu callerTid=%lu",
      enabled ? 1 : 0, GhostRig_CleanupReasonName(reason),
      (unsigned long long)generation, GetCurrentThreadId());
}

static uint64_t GhostRig_RequestVmdReload(const char *path) {
  ClothRequestInvalidation();
  DirectVmdRuntime_BeginLifecycleMutation();
  DirectVmdRuntime_RequestLoad(path);
  const GhostRigCleanupReason reason = GhostRigCleanupReason::VmdReload;
  s_ghostRequestedCleanupReason.store(static_cast<uint32_t>(reason),
                                      std::memory_order_release);
  const uint64_t generation =
      s_ghostRequestedGeneration.fetch_add(1, std::memory_order_acq_rel) + 1;
  const uintptr_t owner =
      s_ghostRequestedOwnerId.load(std::memory_order_acquire);
  DirectVmdRuntime_SetTarget(generation, owner);
  DirectVmdRuntime_EndLifecycleMutation();
  Log("[P2-GHOST-REQUEST] reload reason=%s generation=%llu owner=%p "
      "callerTid=%lu",
      GhostRig_CleanupReasonName(reason),
      (unsigned long long)generation, reinterpret_cast<void *>(owner),
      GetCurrentThreadId());
  return generation;
}

static void GhostRig_RequestSnapshot() {
  s_ghostSnapshotRequest.fetch_add(1, std::memory_order_acq_rel);
}

static void GhostRig_RequestOwnerChange(void *ownerCharacter) {
  const uint64_t backendGeneration = g_motionBackend.InvalidateOwner();
  uintptr_t ownerId = reinterpret_cast<uintptr_t>(ownerCharacter);
  s_ghostRequestedOwnerId.store(ownerId, std::memory_order_release);
  s_ghostOwnerKnown.store(true, std::memory_order_release);
  s_ghostRequestedCleanupReason.store(
      static_cast<uint32_t>(GhostRigCleanupReason::CharacterSwitch),
      std::memory_order_release);
  uint64_t generation =
      s_ghostRequestedGeneration.fetch_add(1, std::memory_order_acq_rel) + 1;
  DirectVmdRuntime_SetTarget(generation, ownerId);
  Log("[P3-OWNER-INVALIDATE] owner=%p reason=character-switch "
      "backendGeneration=%llu ghostGeneration=%llu callerTid=%lu "
      "oldTargetInvalidatedBeforeCleanup=1",
      ownerCharacter, (unsigned long long)backendGeneration,
      (unsigned long long)generation, GetCurrentThreadId());
}

static void GhostRig_RequestProcessDetach() {
  s_ghostDesiredEnabled.store(false, std::memory_order_release);
  s_directVmdTerrainDesiredEnabled.store(false,
                                         std::memory_order_release);
  s_ghostRequestedCleanupReason.store(
      static_cast<uint32_t>(GhostRigCleanupReason::PluginUnload),
      std::memory_order_release);
  s_ghostRequestedGeneration.fetch_add(1, std::memory_order_acq_rel);
}

static bool GhostRig_RequireMainThread(const char *stage, bool allowCapture) {
  DWORD tid = GetCurrentThreadId();
  DWORD expected = s_ghostMainThreadId.load(std::memory_order_acquire);
  if (expected == 0 && allowCapture) {
    DWORD zero = 0;
    if (s_ghostMainThreadId.compare_exchange_strong(
            zero, tid, std::memory_order_acq_rel, std::memory_order_acquire)) {
      expected = tid;
      Log("[P0-THREAD] captured mainTid=%lu stage=%s", tid, stage);
    } else {
      expected = zero;
    }
  }
  if (expected != 0 && expected == tid)
    return true;

  uint64_t count = s_ghostWrongThreadCount.fetch_add(1) + 1;
  if (count <= 8 || (count % 120) == 0) {
    Log("[P0-THREAD-REJECT] stage=%s currentTid=%lu capturedMainTid=%lu "
        "count=%llu",
        stage, tid, expected, (unsigned long long)count);
  }
  return false;
}

static int GhostRig_GetFrameCount() {
  if (!s_ghostTimeGetFrameCount)
    return -1;
  void *boxed = Invoke(s_ghostTimeGetFrameCount, nullptr);
  return boxed ? UnboxInt(boxed) : -1;
}

static int GhostRig_GetActiveSceneHandle() {
  if (!s_ghostSceneGetActiveScene)
    return INT_MIN;
  void *boxedScene = Invoke(s_ghostSceneGetActiveScene, nullptr);
  if (!boxedScene)
    return INT_MIN;
  __try {
    return *reinterpret_cast<int *>((char *)boxedScene + 16);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return INT_MIN;
  }
}

static uint64_t GhostRig_NextOrderSequence() {
  return s_ghostOrderSequence.fetch_add(1, std::memory_order_acq_rel) + 1;
}

static bool GhostRig_ShouldLogOrder() {
  return GhostRig_IsRequestedEnabled() && s_ghostOrderBudget > 0;
}

static void GhostRig_LogOrder(const char *stage, int frame,
                              uintptr_t ownerCharacter, uint64_t generation) {
  if (!GhostRig_ShouldLogOrder())
    return;
  --s_ghostOrderBudget;
  uint64_t seq = GhostRig_NextOrderSequence();
  Log("[P0-ORDER] frame=%d seq=%llu tid=%lu generation=%llu owner=%p "
      "stage=%s",
      frame, (unsigned long long)seq, GetCurrentThreadId(),
      (unsigned long long)generation,
      reinterpret_cast<void *>(ownerCharacter), stage);
}

static bool GhostRig_IsUnityObjectAlive(void *object) {
  if (!object)
    return false;
  if (!s_ghostObjectImplicit)
    return true;
  void *params[] = {object};
  void *boxed = Invoke(s_ghostObjectImplicit, nullptr, params);
  return boxed ? UnboxBool(boxed) : false;
}

static int GhostRig_GetUnityInstanceId(void *object) {
  if (!object || !s_ghostObjectGetInstanceId)
    return 0;
  void *boxed = Invoke(s_ghostObjectGetInstanceId, object);
  return boxed ? UnboxInt(boxed) : 0;
}

static bool GhostRig_SameUnityObject(void *left, void *right) {
  if (left == right)
    return true;
  if (!left || !right)
    return false;
  int leftId = GhostRig_GetUnityInstanceId(left);
  int rightId = GhostRig_GetUnityInstanceId(right);
  return leftId != 0 && leftId == rightId;
}

static void *GhostRig_GetHandleTarget(uint32_t handle) {
  if (!handle || !il2cpp_gchandle_get_target)
    return nullptr;
  __try {
    return il2cpp_gchandle_get_target(handle);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return nullptr;
  }
}

static void *GhostRig_GetGameObject(int index) {
  if (index < 0 || index >= GHOST_NODE_COUNT)
    return nullptr;
  return GhostRig_GetHandleTarget(s_ghostRig.nodes[index].gameObjectHandle);
}

static void *GhostRig_GetTransform(int index) {
  if (index < 0 || index >= GHOST_NODE_COUNT)
    return nullptr;
  return GhostRig_GetHandleTarget(s_ghostRig.nodes[index].transformHandle);
}

static void *GhostRig_GetTargetTransform(DirectVmdBoneId id) {
  const uint32_t index = DirectVmdBoneIndex(id);
  if (index >= DIRECT_VMD_BONE_COUNT)
    return nullptr;
  return GhostRig_GetHandleTarget(
      s_ghostRig.targets[index].transformHandle);
}

static void *GhostRig_GetRetainedOwnerRoot() {
  return GhostRig_GetHandleTarget(s_ghostRig.ownerRootHandle);
}

static GhostRigLegRuntime &GhostRig_LegRuntime(DirectVmdLegSide side) {
  return s_ghostRig.legs[static_cast<uint32_t>(side)];
}

static const char *GhostRig_LegSideName(DirectVmdLegSide side) {
  return side == DirectVmdLegSide::Left ? "L" : "R";
}

static const char *GhostRig_TwistTargetName(uint32_t channel,
                                            uint32_t target) {
  static constexpr const char *names
      [DIRECT_VMD_PHASE6_TWIST_CHANNEL_COUNT]
      [DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL] = {
          {"Bip001_LUpArmTwist", "Bip001_LUpArmTwist1"},
          {"Bip001_L_ForeTwist", "Bip001_L_ForeTwist1"},
          {"Bip001_RUpArmTwist", "Bip001_RUpArmTwist1"},
          {"Bip001_R_ForeTwist", "Bip001_R_ForeTwist1"},
      };
  return channel < DIRECT_VMD_PHASE6_TWIST_CHANNEL_COUNT &&
                 target < DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL
             ? names[channel][target]
             : nullptr;
}

static const char *GhostRig_AuxSolverName(GhostRigAuxSolverId id) {
  switch (id) {
  case GhostRigAuxSolverId::LeftHand: return "left-hand";
  case GhostRigAuxSolverId::RightHand: return "right-hand";
  case GhostRigAuxSolverId::Spine: return "spine";
  case GhostRigAuxSolverId::LookAt: return "look-at";
  case GhostRigAuxSolverId::Aim: return "aim";
  case GhostRigAuxSolverId::Pelvis: return "pelvis";
  default: return "unknown";
  }
}

static void *GhostRig_GetFinalIkBiped() {
  return GhostRig_GetHandleTarget(s_ghostRig.finalIkBipedHandle);
}

static void *GhostRig_GetLegSolver(DirectVmdLegSide side) {
  return GhostRig_GetHandleTarget(GhostRig_LegRuntime(side).solverHandle);
}

static void *GhostRig_GetAuxSolver(GhostRigAuxSolverId id) {
  return GhostRig_GetHandleTarget(
      s_ghostRig.auxSolvers[static_cast<uint32_t>(id)].handle);
}

static bool GhostRig_ZeroAuxSolverWeights(
    GhostRigAuxSolverRuntime &aux, void *solver) {
  if (!solver || aux.positionWeightOffset <= 0 ||
      !aux.savedPositionValid ||
      (aux.positionOffsetOffset > 0 &&
       !aux.savedPositionOffsetValid) ||
      (aux.rotationOffsetOffset > 0 &&
       !aux.savedRotationOffsetValid))
    return false;
  __try {
    *reinterpret_cast<float *>((char *)solver +
                               aux.positionWeightOffset) = 0.0f;
    if (aux.rotationWeightOffset > 0 && aux.savedRotationValid) {
      *reinterpret_cast<float *>((char *)solver +
                                 aux.rotationWeightOffset) = 0.0f;
    }
    if (aux.positionOffsetOffset > 0) {
      *reinterpret_cast<float *>((char *)solver +
                                  aux.positionOffsetOffset + 0) = 0.0f;
      *reinterpret_cast<float *>((char *)solver +
                                  aux.positionOffsetOffset + 4) = 0.0f;
      *reinterpret_cast<float *>((char *)solver +
                                  aux.positionOffsetOffset + 8) = 0.0f;
    }
    if (aux.rotationOffsetOffset > 0) {
      *reinterpret_cast<float *>((char *)solver +
                                  aux.rotationOffsetOffset + 0) = 0.0f;
      *reinterpret_cast<float *>((char *)solver +
                                  aux.rotationOffsetOffset + 4) = 0.0f;
      *reinterpret_cast<float *>((char *)solver +
                                  aux.rotationOffsetOffset + 8) = 0.0f;
    }
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static int GhostRig_ZeroAllAuxSolverWeights() {
  int zeroed = 0;
  for (uint32_t index = 0; index < GHOST_AUX_SOLVER_COUNT; ++index) {
    GhostRigAuxSolverRuntime &aux = s_ghostRig.auxSolvers[index];
    if (GhostRig_ZeroAuxSolverWeights(
            aux, GhostRig_GetHandleTarget(aux.handle)))
      ++zeroed;
  }
  return zeroed;
}

static bool GhostRig_WriteLegSolverFields(
    void *solver, float positionWeight, VmdVec3 position,
    float rotationWeight, VmdQuaternion rotation) {
  if (!solver || !DirectVmdFinite(positionWeight) ||
      !DirectVmdFinite(rotationWeight) || !DirectVmdFinite(position.x) ||
      !DirectVmdFinite(position.y) || !DirectVmdFinite(position.z) ||
      !DirectVmdFinite(rotation.x) || !DirectVmdFinite(rotation.y) ||
      !DirectVmdFinite(rotation.z) || !DirectVmdFinite(rotation.w))
    return false;
  rotation = DirectVmdNormalizeQuaternion(rotation);
  __try {
    *reinterpret_cast<float *>((char *)solver + OFF_IKSOLVER_IKPOS_X) =
        position.x;
    *reinterpret_cast<float *>((char *)solver + OFF_IKSOLVER_IKPOS_Y) =
        position.y;
    *reinterpret_cast<float *>((char *)solver + OFF_IKSOLVER_IKPOS_Z) =
        position.z;
    *reinterpret_cast<float *>((char *)solver +
                                OFF_IKSOLVER_IKPOS_WEIGHT) =
        positionWeight;
    *reinterpret_cast<float *>((char *)solver +
                                OFF_IKSOLVER_IKROT_WEIGHT) =
        rotationWeight;
    *reinterpret_cast<float *>((char *)solver + OFF_IKSOLVER_IKROT_X) =
        rotation.x;
    *reinterpret_cast<float *>((char *)solver + OFF_IKSOLVER_IKROT_Y) =
        rotation.y;
    *reinterpret_cast<float *>((char *)solver + OFF_IKSOLVER_IKROT_Z) =
        rotation.z;
    *reinterpret_cast<float *>((char *)solver + OFF_IKSOLVER_IKROT_W) =
        rotation.w;
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static bool GhostRig_ClearLegSolverFields(void *solver) {
  return GhostRig_WriteLegSolverFields(
      solver, 0.0f, {0.0f, 0.0f, 0.0f}, 0.0f,
      {0.0f, 0.0f, 0.0f, 1.0f});
}

static bool GhostRig_RetainOptionalManagedObject(
    void *object, uint32_t *handle, bool *present) {
  if (!handle || !present)
    return false;
  *handle = 0;
  *present = object != nullptr;
  if (!object)
    return true;
  if (!il2cpp_gchandle_new)
    return false;
  *handle = il2cpp_gchandle_new(object, false);
  return *handle != 0;
}

static int GhostRig_FreeLegExternalStateHandles(
    GhostRigLegRuntime &leg) {
  int freed = 0;
  if (!il2cpp_gchandle_free)
    return freed;
  uint32_t *handles[] = {
      &leg.savedTargetHandle,
      &leg.savedOnPreUpdateHandle,
      &leg.savedOnPostUpdateHandle,
  };
  for (uint32_t *handle : handles) {
    if (!*handle)
      continue;
    il2cpp_gchandle_free(*handle);
    *handle = 0;
    ++freed;
  }
  return freed;
}

static bool GhostRig_SaveAndIsolateLegSolverExternalOwnership(
    DirectVmdLegSide side, void *solver) {
  if (!solver)
    return false;
  GhostRigLegRuntime &leg = GhostRig_LegRuntime(side);
  if (leg.savedExternalStateValid) {
    __try {
      *reinterpret_cast<void **>((char *)solver +
                                  OFF_IKTRIG_TARGET) = nullptr;
      *reinterpret_cast<void **>((char *)solver +
                                  OFF_IKSOLVER_ON_PRE_UPDATE) = nullptr;
      *reinterpret_cast<void **>((char *)solver +
                                  OFF_IKSOLVER_ON_POST_UPDATE) = nullptr;
      leg.externalOwnershipIsolated = true;
      return true;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
      leg.externalOwnershipIsolated = false;
      return false;
    }
  }

  void *target = nullptr;
  void *onPreUpdate = nullptr;
  void *onPostUpdate = nullptr;
  __try {
    target = *reinterpret_cast<void **>((char *)solver +
                                         OFF_IKTRIG_TARGET);
    onPreUpdate = *reinterpret_cast<void **>(
        (char *)solver + OFF_IKSOLVER_ON_PRE_UPDATE);
    onPostUpdate = *reinterpret_cast<void **>(
        (char *)solver + OFF_IKSOLVER_ON_POST_UPDATE);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }

  const bool retained =
      GhostRig_RetainOptionalManagedObject(
          target, &leg.savedTargetHandle,
          &leg.savedTargetPresent) &&
      GhostRig_RetainOptionalManagedObject(
          onPreUpdate, &leg.savedOnPreUpdateHandle,
          &leg.savedOnPreUpdatePresent) &&
      GhostRig_RetainOptionalManagedObject(
          onPostUpdate, &leg.savedOnPostUpdateHandle,
          &leg.savedOnPostUpdatePresent);
  if (!retained) {
    const int freed = GhostRig_FreeLegExternalStateHandles(leg);
    leg.savedTargetPresent = false;
    leg.savedOnPreUpdatePresent = false;
    leg.savedOnPostUpdatePresent = false;
    Log("[P5-FINALIK-ISOLATE] side=%s isolated=0 "
        "reason=managed-reference-retain-failed freedHandles=%d "
        "generation=%llu owner=%p tid=%lu",
        GhostRig_LegSideName(side), freed,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    return false;
  }

  bool isolated = false;
  __try {
    *reinterpret_cast<void **>((char *)solver +
                                OFF_IKTRIG_TARGET) = nullptr;
    *reinterpret_cast<void **>((char *)solver +
                                OFF_IKSOLVER_ON_PRE_UPDATE) = nullptr;
    *reinterpret_cast<void **>((char *)solver +
                                OFF_IKSOLVER_ON_POST_UPDATE) = nullptr;
    isolated = true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    isolated = false;
  }
  if (!isolated) {
    __try {
      *reinterpret_cast<void **>((char *)solver +
                                  OFF_IKTRIG_TARGET) = target;
      *reinterpret_cast<void **>((char *)solver +
                                  OFF_IKSOLVER_ON_PRE_UPDATE) = onPreUpdate;
      *reinterpret_cast<void **>((char *)solver +
                                  OFF_IKSOLVER_ON_POST_UPDATE) = onPostUpdate;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
    }
    GhostRig_FreeLegExternalStateHandles(leg);
    leg.savedTargetPresent = false;
    leg.savedOnPreUpdatePresent = false;
    leg.savedOnPostUpdatePresent = false;
    Log("[P5-FINALIK-ISOLATE] side=%s isolated=0 "
        "reason=solver-field-write-failed generation=%llu owner=%p "
        "tid=%lu",
        GhostRig_LegSideName(side),
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    return false;
  }

  leg.savedExternalStateValid = true;
  leg.externalOwnershipIsolated = true;
  Log("[P5-FINALIK-ISOLATE] side=%s isolated=1 targetDetached=%d "
      "preUpdateDetached=%d postUpdateDetached=%d "
      "restoreOnCleanup=1 generation=%llu owner=%p tid=%lu",
      GhostRig_LegSideName(side), leg.savedTargetPresent ? 1 : 0,
      leg.savedOnPreUpdatePresent ? 1 : 0,
      leg.savedOnPostUpdatePresent ? 1 : 0,
      (unsigned long long)s_ghostRig.generation,
      reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
      GetCurrentThreadId());
  return true;
}

static bool GhostRig_RestoreLegSolverExternalOwnership(
    GhostRigLegRuntime &leg, void *solver) {
  if (!solver || !leg.savedExternalStateValid)
    return false;
  void *target = leg.savedTargetPresent
                     ? GhostRig_GetHandleTarget(leg.savedTargetHandle)
                     : nullptr;
  if (target && !GhostRig_IsUnityObjectAlive(target))
    target = nullptr;
  void *onPreUpdate = leg.savedOnPreUpdatePresent
                          ? GhostRig_GetHandleTarget(
                                leg.savedOnPreUpdateHandle)
                          : nullptr;
  void *onPostUpdate = leg.savedOnPostUpdatePresent
                           ? GhostRig_GetHandleTarget(
                                 leg.savedOnPostUpdateHandle)
                           : nullptr;
  __try {
    *reinterpret_cast<void **>((char *)solver +
                                OFF_IKTRIG_TARGET) = target;
    *reinterpret_cast<void **>((char *)solver +
                                OFF_IKSOLVER_ON_PRE_UPDATE) = onPreUpdate;
    *reinterpret_cast<void **>((char *)solver +
                                OFF_IKSOLVER_ON_POST_UPDATE) = onPostUpdate;
    leg.externalOwnershipIsolated = false;
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static bool GhostRig_ReleaseLegIsolationForGrounder(
    DirectVmdLegSide side, void *solver) {
  GhostRigLegRuntime &leg = GhostRig_LegRuntime(side);
  bool restored = true;
  if (leg.savedExternalStateValid)
    restored = GhostRig_RestoreLegSolverExternalOwnership(leg, solver);
  const int freed = GhostRig_FreeLegExternalStateHandles(leg);
  leg.savedExternalStateValid = false;
  leg.savedTargetPresent = false;
  leg.savedOnPreUpdatePresent = false;
  leg.savedOnPostUpdatePresent = false;
  leg.externalOwnershipIsolated = false;
  if (!restored) {
    Log("[P7-GROUNDER-CALLBACKS] side=%s restored=0 freedHandles=%d "
        "generation=%llu owner=%p tid=%lu",
        GhostRig_LegSideName(side), freed,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
  return restored;
}

static bool GhostRig_DetachLegTargetOnly(void *solver) {
  if (!solver)
    return false;
  __try {
    *reinterpret_cast<void **>((char *)solver + OFF_IKTRIG_TARGET) =
        nullptr;
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static void *GhostRig_GetGrounder() {
  return GhostRig_GetHandleTarget(s_ghostRig.grounder.handle);
}

static bool GhostRig_ReadBehaviourEnabled(void *component,
                                           bool *enabled) {
  if (!component || !enabled || !g_animator_get_enabled)
    return false;
  __try {
    void *boxed = Invoke(g_animator_get_enabled, component);
    if (!boxed)
      return false;
    *enabled = UnboxBool(boxed);
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static bool GhostRig_WriteBehaviourEnabled(void *component,
                                            bool enabled) {
  if (!component || !g_animator_set_enabled)
    return false;
  __try {
    void *params[] = {&enabled};
    Invoke(g_animator_set_enabled, component, params);
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static bool GhostRig_ResetGrounderSolver(void *grounder) {
  if (!grounder || !s_ghostGrounderResetPosition)
    return false;
  __try {
    Invoke(s_ghostGrounderResetPosition, grounder);
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static bool GhostRig_WriteGrounderFields(void *grounder, bool active) {
  if (!grounder)
    return false;
  __try {
    *reinterpret_cast<float *>((char *)grounder +
                               OFF_GROUNDER_WEIGHT) =
        active ? 1.0f : 0.0f;
    *reinterpret_cast<float *>((char *)grounder +
                               OFF_GROUNDER_MAINTAIN_WEIGHT) =
        active ? 1.0f : 0.0f;
    *reinterpret_cast<float *>((char *)grounder +
                               OFF_GROUNDER_ADSORB_WEIGHT) =
        active ? 1.0f : 0.0f;
    *reinterpret_cast<float *>((char *)grounder +
                               OFF_GROUNDER_SPINE_BEND) = 0.0f;
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static void GhostRig_RestoreGrounderOwnership(
    GhostRigCleanupReason reason) {
  GhostRigGrounderRuntime &state = s_ghostRig.grounder;
  void *grounder = GhostRig_GetGrounder();
  bool fieldsRestored = false;
  bool enabledRestored = false;
  bool reset = false;
  if (grounder && GhostRig_IsUnityObjectAlive(grounder)) {
    reset = GhostRig_ResetGrounderSolver(grounder);
    if (state.ownershipCaptured && state.savedFieldsValid) {
      __try {
        *reinterpret_cast<float *>((char *)grounder +
                                   OFF_GROUNDER_WEIGHT) =
            state.savedWeight;
        *reinterpret_cast<float *>((char *)grounder +
                                   OFF_GROUNDER_MAINTAIN_WEIGHT) =
            state.savedMaintainWeight;
        *reinterpret_cast<float *>((char *)grounder +
                                   OFF_GROUNDER_ADSORB_WEIGHT) =
            state.savedAdsorbWeight;
        *reinterpret_cast<float *>((char *)grounder +
                                   OFF_GROUNDER_SPINE_BEND) =
            state.savedSpineBend;
        *reinterpret_cast<float *>((char *)grounder +
                                   OFF_GROUNDER_SPINE_SPEED) =
            state.savedSpineSpeed;
        fieldsRestored = true;
      } __except (EXCEPTION_EXECUTE_HANDLER) {
        fieldsRestored = false;
      }
    }
    if (state.savedEnabledValid)
      enabledRestored = GhostRig_WriteBehaviourEnabled(
          grounder, state.savedEnabled);
  }
  const bool hadOwnership = state.ownershipCaptured || state.handle != 0;
  const uint64_t activations = state.activationCount;
  const uint64_t solverUpdates = state.solverUpdateCount;
  const uint64_t postUpdates = state.postSolverUpdateCount;
  if (state.handle && il2cpp_gchandle_free)
    il2cpp_gchandle_free(state.handle);
  state = GhostRigGrounderRuntime();
  if (hadOwnership) {
    Log("[P7-GROUNDER-RESTORE] reason=%s grounder=%p reset=%d "
        "fieldsRestored=%d enabledRestored=%d activations=%llu "
        "solverUpdates=%llu postSolverUpdates=%llu generation=%llu "
        "owner=%p duplicateReleaseSafe=1 tid=%lu",
        GhostRig_CleanupReasonName(reason), grounder, reset ? 1 : 0,
        fieldsRestored ? 1 : 0, enabledRestored ? 1 : 0,
        (unsigned long long)activations,
        (unsigned long long)solverUpdates,
        (unsigned long long)postUpdates,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
}

static void GhostRig_SuppressGrounderForFlatMode(int frame) {
  GhostRigGrounderRuntime &state = s_ghostRig.grounder;
  void *grounder = GhostRig_GetGrounder();
  if (!state.ownershipCaptured || !grounder)
    return;
  const bool wasActive = state.active;
  const bool fieldsZeroed = GhostRig_WriteGrounderFields(grounder, false);
  const bool reset = wasActive
      ? GhostRig_ResetGrounderSolver(grounder)
      : false;
  state.active = false;
  if (wasActive) {
    Log("[P7-GROUNDER-TOGGLE] enabled=0 unityFrame=%d grounder=%p "
        "weightsZeroed=%d reset=%d flatDirectVmdRestored=1 "
        "generation=%llu owner=%p tid=%lu",
        frame, grounder, fieldsZeroed ? 1 : 0, reset ? 1 : 0,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
}

static bool GhostRig_ActivateGrounderForTerrain(
    void *bipedIK, int frame) {
  GhostRigGrounderRuntime &state = s_ghostRig.grounder;
  void *grounder = GhostRig_GetOwnerGrounderBipedIK(
      s_ghostRig.ownerCharacter, bipedIK);
  if (!grounder || !GhostRig_IsUnityObjectAlive(grounder) ||
      !s_ghostGrounderUpdate || !s_ghostGrounderResetPosition ||
      !s_ghostOrigGrounderOnSolverUpdate ||
      !s_ghostOrigGrounderOnPostSolverUpdate) {
    if (!state.unavailableLogged) {
      state.unavailableLogged = true;
      Log("[P7-GROUNDER-ACQUIRE] active=0 unityFrame=%d "
          "grounder=%p biped=%p update=%p reset=%p onSolver=%p "
          "onPost=%p action=fail-flat-no-custom-height generation=%llu "
          "owner=%p tid=%lu",
          frame, grounder, bipedIK, s_ghostGrounderUpdate,
          s_ghostGrounderResetPosition,
          s_ghostOrigGrounderOnSolverUpdate,
          s_ghostOrigGrounderOnPostSolverUpdate,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    }
    return false;
  }

  void *retained = GhostRig_GetGrounder();
  if (retained && retained != grounder)
    GhostRig_RestoreGrounderOwnership(GhostRigCleanupReason::Recapture);

  if (state.active && GhostRig_GetGrounder() == grounder) {
    const bool enabled = GhostRig_WriteBehaviourEnabled(grounder, true);
    const bool fieldsWritten = GhostRig_WriteGrounderFields(grounder, true);
    if (enabled && fieldsWritten) {
      state.unavailableLogged = false;
      return true;
    }
    state.active = false;
    const bool suppressed = GhostRig_WriteGrounderFields(grounder, false);
    const bool reset = GhostRig_ResetGrounderSolver(grounder);
    if (!state.unavailableLogged) {
      Log("[P7-GROUNDER-ACQUIRE] active=0 unityFrame=%d grounder=%p "
          "reason=per-frame-enforcement-failed enabled=%d fields=%d "
          "suppressed=%d reset=%d action=fail-flat generation=%llu "
          "owner=%p tid=%lu",
          frame, grounder, enabled ? 1 : 0, fieldsWritten ? 1 : 0,
          suppressed ? 1 : 0, reset ? 1 : 0,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    }
    state.unavailableLogged = true;
    return false;
  }

  if (!state.ownershipCaptured) {
    state.handle = il2cpp_gchandle_new
        ? il2cpp_gchandle_new(grounder, false)
        : 0;
    if (!state.handle)
      return false;
    state.savedEnabledValid = GhostRig_ReadBehaviourEnabled(
        grounder, &state.savedEnabled);
    __try {
      state.savedWeight = *reinterpret_cast<float *>(
          (char *)grounder + OFF_GROUNDER_WEIGHT);
      state.savedMaintainWeight = *reinterpret_cast<float *>(
          (char *)grounder + OFF_GROUNDER_MAINTAIN_WEIGHT);
      state.savedAdsorbWeight = *reinterpret_cast<float *>(
          (char *)grounder + OFF_GROUNDER_ADSORB_WEIGHT);
      state.savedSpineBend = *reinterpret_cast<float *>(
          (char *)grounder + OFF_GROUNDER_SPINE_BEND);
      state.savedSpineSpeed = *reinterpret_cast<float *>(
          (char *)grounder + OFF_GROUNDER_SPINE_SPEED);
      state.savedFieldsValid =
          DirectVmdFinite(state.savedWeight) &&
          DirectVmdFinite(state.savedMaintainWeight) &&
          DirectVmdFinite(state.savedAdsorbWeight) &&
          DirectVmdFinite(state.savedSpineBend) &&
          DirectVmdFinite(state.savedSpineSpeed);
    } __except (EXCEPTION_EXECUTE_HANDLER) {
      state.savedFieldsValid = false;
    }
    if (!state.savedFieldsValid) {
      GhostRig_RestoreGrounderOwnership(
          GhostRigCleanupReason::Recapture);
      return false;
    }
    state.ownershipCaptured = true;
  }

  bool callbacksRestored = true;
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT; ++index) {
    const DirectVmdLegSide side =
        static_cast<DirectVmdLegSide>(index);
    callbacksRestored =
        GhostRig_ReleaseLegIsolationForGrounder(
            side, GhostRig_GetLegSolver(side)) && callbacksRestored;
  }
  const bool enabled = GhostRig_WriteBehaviourEnabled(grounder, true);
  const bool fieldsWritten = GhostRig_WriteGrounderFields(grounder, true);
  bool updateInvoked = false;
  __try {
    Invoke(s_ghostGrounderUpdate, grounder);
    updateInvoked = true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    updateInvoked = false;
  }
  bool initiated = false;
  void *linkedBiped = nullptr;
  __try {
    initiated = *reinterpret_cast<bool *>(
        (char *)grounder + OFF_GROUNDER_INITIATED);
    linkedBiped = *reinterpret_cast<void **>(
        (char *)grounder + OFF_GROUNDER_BIPED_IK);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    initiated = false;
    linkedBiped = nullptr;
  }
  const bool reset = initiated
      ? GhostRig_ResetGrounderSolver(grounder)
      : false;
  const bool ready = callbacksRestored && enabled && fieldsWritten &&
                      updateInvoked && initiated && linkedBiped == bipedIK;
  const bool transition = ready && !state.active;
  const bool failureTransition = !ready && !state.unavailableLogged;
  bool failFlatSuppressed = false;
  bool failFlatReset = false;
  if (!ready) {
    failFlatSuppressed = GhostRig_WriteGrounderFields(grounder, false);
    failFlatReset = GhostRig_ResetGrounderSolver(grounder);
  }
  state.active = ready;
  state.unavailableLogged = !ready;
  if (transition) {
    state.activatedFrame = frame;
    ++state.activationCount;
  }
  if (transition || failureTransition) {
    Log("[P7-GROUNDER-ACQUIRE] active=%d unityFrame=%d grounder=%p "
        "biped=%p linkedBiped=%p callbacksRestored=%d enabled=%d "
        "fieldsWritten=%d updateInvoked=%d initiated=%d reset=%d "
        "failFlatSuppressed=%d failFlatReset=%d "
        "savedEnabled=%d savedEnabledValid=%d "
        "savedWeights=(%.6f,%.6f,%.6f) savedSpine=(%.6f,%.6f) "
        "runtimeWeights=(1,1,1) runtimeSpineBend=0 "
        "heightBackend=Grounding.Raycast-delegate-foot-xz "
        "grounderRole=callback-collision-query-and-layer-provider "
        "customFindFloorRuntime=0 "
        "generation=%llu owner=%p tid=%lu",
        ready ? 1 : 0, frame, grounder, bipedIK, linkedBiped,
        callbacksRestored ? 1 : 0, enabled ? 1 : 0,
        fieldsWritten ? 1 : 0, updateInvoked ? 1 : 0,
        initiated ? 1 : 0, reset ? 1 : 0,
        failFlatSuppressed ? 1 : 0, failFlatReset ? 1 : 0,
        state.savedEnabled ? 1 : 0,
        state.savedEnabledValid ? 1 : 0, state.savedWeight,
        state.savedMaintainWeight, state.savedAdsorbWeight,
        state.savedSpineBend, state.savedSpineSpeed,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
  return ready;
}

static bool GhostRig_WriteLocalPosition(void *transform, VmdVec3 value) {
  if (!transform || !g_transform_set_localPosition ||
      !DirectVmdFinite(value.x) || !DirectVmdFinite(value.y) ||
      !DirectVmdFinite(value.z))
    return false;
  Vec3 data = {value.x, value.y, value.z};
  __try {
    void *params[] = {&data};
    Invoke(g_transform_set_localPosition, transform, params);
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static bool GhostRig_RestorePelvisNodeOwnership(
    GhostRigCleanupReason reason) {
  GhostRigPelvisNodeRuntime &node = s_ghostRig.pelvisNode;
  if (!node.handle)
    return false;
  void *transform = GhostRig_GetHandleTarget(node.handle);
  const bool alive = transform && GhostRig_IsUnityObjectAlive(transform);
  const bool restored = alive && node.restoreValid &&
      GhostRig_WriteLocalPosition(transform, node.restoreLocalPosition);
  Log("[P5-PELVIS-NODE] event=restore reason=%s node='%s' alive=%d "
      "restored=%d writes=%llu postCorrections=%llu "
      "generation=%llu owner=%p tid=%lu",
      GhostRig_CleanupReasonName(reason), node.name, alive ? 1 : 0,
      restored ? 1 : 0, (unsigned long long)node.writeCount,
      (unsigned long long)node.postCorrectionCount,
      (unsigned long long)s_ghostRig.generation,
      reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
      GetCurrentThreadId());
  return restored;
}

static int GhostRig_FreePhase5FinalIkHandles() {
  int freed = 0;
  if (il2cpp_gchandle_free) {
    if (s_ghostRig.pelvisNode.handle) {
      il2cpp_gchandle_free(s_ghostRig.pelvisNode.handle);
      s_ghostRig.pelvisNode.handle = 0;
      ++freed;
    }
    for (uint32_t side = 0; side < DIRECT_VMD_LEG_SIDE_COUNT; ++side) {
      GhostRigLegRuntime &leg = s_ghostRig.legs[side];
      freed += GhostRig_FreeLegExternalStateHandles(leg);
      if (leg.solverHandle) {
        il2cpp_gchandle_free(leg.solverHandle);
        leg.solverHandle = 0;
        ++freed;
      }
    }
    for (uint32_t index = 0; index < GHOST_AUX_SOLVER_COUNT; ++index) {
      GhostRigAuxSolverRuntime &aux = s_ghostRig.auxSolvers[index];
      if (aux.handle) {
        il2cpp_gchandle_free(aux.handle);
        aux.handle = 0;
        ++freed;
      }
    }
    if (s_ghostRig.finalIkBipedHandle) {
      il2cpp_gchandle_free(s_ghostRig.finalIkBipedHandle);
      s_ghostRig.finalIkBipedHandle = 0;
      ++freed;
    }
  }
  return freed;
}

static void GhostRig_ResetPhase5LegState() {
  for (uint32_t side = 0; side < DIRECT_VMD_LEG_SIDE_COUNT; ++side) {
    s_ghostRig.legs[side] = GhostRigLegRuntime();
    s_ghostRig.legs[side].preparedFrame = INT_MIN;
    s_ghostRig.legs[side].innerWriteFrame = INT_MIN;
    s_ghostRig.legs[side].postTerrainResolveFrame = INT_MIN;
    s_ghostRig.legs[side].footWorldRotation =
        {0.0f, 0.0f, 0.0f, 1.0f};
    s_ghostRig.legs[side].toeWorldRotation =
        {0.0f, 0.0f, 0.0f, 1.0f};
    s_ghostRig.legs[side].footControlWorldRotation =
        {0.0f, 0.0f, 0.0f, 1.0f};
    s_ghostRig.legs[side].toeControlWorldRotation =
        {0.0f, 0.0f, 0.0f, 1.0f};
    s_ghostRig.legs[side].terrainNormalRotation =
        {0.0f, 0.0f, 0.0f, 1.0f};
    s_ghostRig.legs[side].footSeedLocalRotation =
        {0.0f, 0.0f, 0.0f, 1.0f};
    s_ghostRig.legs[side].toeSeedLocalRotation =
        {0.0f, 0.0f, 0.0f, 1.0f};
  }
  for (uint32_t index = 0; index < GHOST_AUX_SOLVER_COUNT; ++index)
    s_ghostRig.auxSolvers[index] = GhostRigAuxSolverRuntime();
  s_ghostRig.pelvisNode = GhostRigPelvisNodeRuntime();
  s_ghostRig.pelvisNode.lastCorrectionFrame = INT_MIN;
  s_ghostRig.pelvisNode.lastLogFrame = INT_MIN;
  s_ghostRig.grounder = GhostRigGrounderRuntime();
  s_ghostRig.lastLegPreparedFrame = INT_MIN;
  s_ghostRig.lastLegLogFrame = INT_MIN;
}

static void GhostRig_ResetPhase7TerrainState() {
  s_ghostRig.terrainState = DirectVmdTerrainState();
  s_ghostRig.terrainFrame = DirectVmdTerrainFrameOutput();
  memset(s_ghostRig.terrainCachedRawPlane, 0,
         sizeof(s_ghostRig.terrainCachedRawPlane));
  s_ghostRig.terrainProbeAccumulator = 0.0f;
  s_ghostRig.terrainProbeCacheValid = false;
  s_ghostRig.terrainFrameValid = false;
  s_ghostRig.lastTerrainFrame = INT_MIN;
  s_ghostRig.lastTerrainLogFrame = INT_MIN;
  s_ghostRig.lastTerrainFinalIkResolveLogFrame = INT_MIN;
  s_ghostRig.terrainLastQpc = 0;
  s_ghostRig.terrainLastSourceFrame = 0.0;
  s_ghostRig.terrainLastPlaybackCycle = 0;
  s_ghostRig.terrainQueryCount = 0;
  s_ghostRig.terrainHitCount = 0;
  s_ghostRig.terrainFinalIkResolveCount = 0;
  s_ghostRig.grounder.contactFrame = INT_MIN;
  s_ghostRig.grounder.lastContactLogFrame = INT_MIN;
  s_ghostRig.grounder.contactTimelineValid = false;
  s_ghostRig.grounder.contactSourceFrame = 0.0;
  s_ghostRig.grounder.contactPlaybackCycle = 0;
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT;
       ++index) {
    s_ghostRig.grounder.cachedTargetValid[index] = false;
    s_ghostRig.grounder.cachedTargetFrame[index] = INT_MIN;
    s_ghostRig.grounder.cachedLastHitValid[index] = false;
    s_ghostRig.grounder.cachedHeelHitValid[index] = false;
    s_ghostRig.grounder.cachedCalculatedFootValid[index] = false;
    s_ghostRig.grounder.cachedLegIkPositionValid[index] = false;
    s_ghostRig.grounder.cachedLastHitNormalValid[index] = false;
    s_ghostRig.grounder.cachedRawLegGrounded[index] = false;
    s_ghostRig.grounder.cachedLegInStair[index] = false;
    s_ghostRig.grounder.cachedRawGroundFrame[index] = INT_MIN;
    s_ghostRig.grounder.cachedFootPhysicsPlane[index] =
        DirectVmdTerrainPlane();
    s_ghostRig.grounder.cachedFootDirectionalClearance[index] =
        DirectVmdDirectionalTerrainClearance();
    s_ghostRig.grounder.cachedFootPhysicsPlaneFrame[index] = INT_MIN;
    s_ghostRig.grounder.previousFootPhysicsQuery[index] = VmdVec3{};
    s_ghostRig.grounder.previousFootPhysicsQueryValid[index] = false;
    s_ghostRig.grounder.footPhysicsReferenceValid[index] = false;
    s_ghostRig.grounder.footPhysicsReferenceGroundY[index] = 0.0f;
    s_ghostRig.grounder.footPhysicsMissSeconds[index] = 0.0f;
    s_ghostRig.grounder.hybridTargetValid[index] = false;
    s_ghostRig.grounder.hybridUsesGrounder[index] = false;
    s_ghostRig.grounder.upwardClearanceApplied[index] = false;
    s_ghostRig.grounder.hybridTargetY[index] = 0.0f;
    s_ghostRig.grounder.upwardClearanceTargetY[index] = 0.0f;
    s_ghostRig.grounder.nativeCorrectionY[index] = 0.0f;
    s_ghostRig.grounder.authoredLift[index] = 0.0f;
    s_ghostRig.grounder.authoredVerticalVelocity[index] = 0.0f;
    s_ghostRig.grounder.stableNativeHeight[index] =
        DirectVmdGrounderHeightState();
    s_ghostRig.grounder.contactEdge[index] =
        DirectVmdGrounderContactEdgeState();
    s_ghostRig.grounder.nativeHeightSource[index] =
        DirectVmdGrounderHeightSource::None;
    s_ghostRig.grounder.directNativeTargetY[index] = 0.0f;
  }
  s_ghostRig.grounder.stableRootOffset =
      DirectVmdGrounderHeightState();
  s_ghostRig.grounder.rootEnvironmentTargetOffset = 0.0f;
  s_ghostRig.grounder.rootReachTargetOffset = 0.0f;
  s_ghostRig.grounder.rootReachRelease =
      DirectVmdRootReachReleaseState();
  s_ghostRig.grounder.footPhysicsQueryCount = 0;
  s_ghostRig.grounder.footPhysicsHitCount = 0;
  s_ghostRig.grounder.footRaycastMissCount = 0;
  s_ghostRig.grounder.footRaycastInvokeFailureCount = 0;
  s_ghostRig.grounder.groundingRaycastDelegateClass = nullptr;
  s_ghostRig.grounder.groundingRaycastInvokeMethod = nullptr;
  s_ghostRig.grounder.footPhysicsApiFailureLogged = false;
  s_ghostRig.grounder.footRaycastBackendLogged = false;
  s_ghostRig.grounder.footRaycastFirstSampleLogged = false;
}

static void GhostRig_RestoreKneeOverride(GhostRigLegRuntime &leg, void *solver) {
  if (!solver || !leg.kneeOverride.active) return;
  __try {
    leg.kneeOverride.Restore(
        reinterpret_cast<VmdVec3 *>((char *)solver + leg.kneeNormalOffset),
        reinterpret_cast<VmdVec3 *>((char *)solver + leg.kneeBackupOffset),
        reinterpret_cast<float *>((char *)solver + OFF_IKLIMB_BEND_WEIGHT));
  } __except (EXCEPTION_EXECUTE_HANDLER) {}
}

static void GhostRig_AfterLegSolverUpdate(void *solver) {
  if (!solver || !GhostRig_RequireMainThread("KneeMix.Restore", false)) return;
  for (auto &leg : s_ghostRig.legs)
    if (leg.kneeOverride.active && GhostRig_GetHandleTarget(leg.solverHandle) == solver)
      GhostRig_RestoreKneeOverride(leg, solver);
}

static void GhostRig_ClearPhase5FinalIkOwnership(
    GhostRigCleanupReason reason) {
  GhostRig_RestoreGrounderOwnership(reason);
  GhostRig_RestorePelvisNodeOwnership(reason);
  int solversCleared = 0;
  int bendStatesRestored = 0;
  int externalStatesRestored = 0;
  int auxStatesRestored = 0;
  uint64_t solverWrites = 0;
  uint64_t fkWrites = 0;
  uint64_t toeWrites = 0;
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT; ++index) {
    GhostRigLegRuntime &leg = s_ghostRig.legs[index];
    void *solver = GhostRig_GetHandleTarget(leg.solverHandle);
    GhostRig_RestoreKneeOverride(leg, solver);
    solverWrites += leg.solverWriteCount;
    fkWrites += leg.fkRotationWriteCount;
    toeWrites += leg.toeAimWriteCount;
    if (solver && GhostRig_ClearLegSolverFields(solver)) {
      ++solversCleared;
      ++leg.weightClearCount;
    }
    if (solver && leg.savedBendStateValid) {
      __try {
        *reinterpret_cast<int *>((char *)solver +
                                 OFF_IKLIMB_BEND_MODIFIER) =
            leg.savedBendModifier;
        *reinterpret_cast<float *>((char *)solver +
                                   OFF_IKLIMB_BEND_WEIGHT) =
            leg.savedBendWeight;
        ++bendStatesRestored;
      } __except (EXCEPTION_EXECUTE_HANDLER) {
      }
    }
    if (GhostRig_RestoreLegSolverExternalOwnership(leg, solver))
      ++externalStatesRestored;
  }
  for (uint32_t index = 0; index < GHOST_AUX_SOLVER_COUNT; ++index) {
    GhostRigAuxSolverRuntime &aux = s_ghostRig.auxSolvers[index];
    void *solver = GhostRig_GetHandleTarget(aux.handle);
    if (!solver || !aux.savedPositionValid)
      continue;
    __try {
      *reinterpret_cast<float *>((char *)solver +
                                 aux.positionWeightOffset) =
          aux.savedPositionWeight;
      if (aux.rotationWeightOffset > 0 && aux.savedRotationValid) {
        *reinterpret_cast<float *>((char *)solver +
                                   aux.rotationWeightOffset) =
            aux.savedRotationWeight;
      }
      if (aux.positionOffsetOffset > 0 &&
          aux.savedPositionOffsetValid) {
        *reinterpret_cast<float *>((char *)solver +
                                    aux.positionOffsetOffset + 0) =
            aux.savedPositionOffset.x;
        *reinterpret_cast<float *>((char *)solver +
                                    aux.positionOffsetOffset + 4) =
            aux.savedPositionOffset.y;
        *reinterpret_cast<float *>((char *)solver +
                                    aux.positionOffsetOffset + 8) =
            aux.savedPositionOffset.z;
      }
      if (aux.rotationOffsetOffset > 0 &&
          aux.savedRotationOffsetValid) {
        *reinterpret_cast<float *>((char *)solver +
                                    aux.rotationOffsetOffset + 0) =
            aux.savedRotationOffset.x;
        *reinterpret_cast<float *>((char *)solver +
                                    aux.rotationOffsetOffset + 4) =
            aux.savedRotationOffset.y;
        *reinterpret_cast<float *>((char *)solver +
                                    aux.rotationOffsetOffset + 8) =
            aux.savedRotationOffset.z;
      }
      ++auxStatesRestored;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
    }
  }
  const int handlesFreed = GhostRig_FreePhase5FinalIkHandles();
  if (handlesFreed || solversCleared || solverWrites || fkWrites ||
      toeWrites) {
    Log("[P5-FINALIK-CLEAR] reason=%s solversCleared=%d "
        "bendStatesRestored=%d externalStatesRestored=%d "
        "auxStatesRestored=%d handlesFreed=%d "
        "solverWrites=%llu "
        "fkRotationWrites=%llu toeAimWrites=%llu footWeightsNowZero=1 "
        "targetsErased=1 nativeTargetsAndCallbacksRestored=1 "
        "generation=%llu owner=%p tid=%lu",
        GhostRig_CleanupReasonName(reason), solversCleared,
        bendStatesRestored, externalStatesRestored, auxStatesRestored,
        handlesFreed,
        (unsigned long long)solverWrites, (unsigned long long)fkWrites,
        (unsigned long long)toeWrites,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
  GhostRig_ResetPhase5LegState();
}

static Quat GhostRig_Normalize(Quat q) {
  float mag2 = q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
  if (mag2 < 1.0e-12f)
    return {0.0f, 0.0f, 0.0f, 1.0f};
  float inv = 1.0f / sqrtf(mag2);
  return {q.x * inv, q.y * inv, q.z * inv, q.w * inv};
}

static bool GhostRig_ReadLocalPosition(void *transform, Vec3 &out) {
  if (!transform || !g_transform_get_localPosition)
    return false;
  void *boxed = Invoke(g_transform_get_localPosition, transform);
  if (!boxed)
    return false;
  __try {
    out = *reinterpret_cast<Vec3 *>((char *)boxed + 16);
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static bool GhostRig_ReadLocalRotation(void *transform, Quat &out) {
  if (!transform || !g_transform_get_localRotation)
    return false;
  void *boxed = Invoke(g_transform_get_localRotation, transform);
  if (!boxed)
    return false;
  __try {
    out = GhostRig_Normalize(
        *reinterpret_cast<Quat *>((char *)boxed + 16));
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static bool GhostRig_WriteLocalRotation(void *transform, Quat value) {
  if (!transform || !g_transform_set_localRotation)
    return false;
  value = GhostRig_Normalize(value);
  __try {
    void *params[] = {&value};
    Invoke(g_transform_set_localRotation, transform, params);
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static bool GhostRig_ReadWorldPosition(void *transform, Vec3 &out) {
  if (!transform || !g_camGetPos)
    return false;
  float value[3] = {};
  __try {
    g_camGetPos(transform, value);
    out = {value[0], value[1], value[2]};
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static bool GhostRig_ReadWorldRotation(void *transform, Quat &out) {
  if (!transform || !g_camGetRot)
    return false;
  float value[4] = {};
  __try {
    g_camGetRot(transform, value);
    out = GhostRig_Normalize({value[0], value[1], value[2], value[3]});
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static bool GhostRig_WriteWorldPosition(void *transform, Vec3 value) {
  if (!transform)
    return false;
  float data[3] = {value.x, value.y, value.z};
  __try {
    if (g_origSetPos) {
      g_origSetPos(transform, data);
      return true;
    }
    if (g_camSetPos) {
      g_camSetPos(transform, data);
      return true;
    }
  } __except (EXCEPTION_EXECUTE_HANDLER) {
  }
  return false;
}

static bool GhostRig_WriteWorldRotation(void *transform, Quat value) {
  if (!transform)
    return false;
  value = GhostRig_Normalize(value);
  float data[4] = {value.x, value.y, value.z, value.w};
  __try {
    if (g_origSetRot) {
      g_origSetRot(transform, data);
      return true;
    }
    if (g_camSetRot) {
      g_camSetRot(transform, data);
      return true;
    }
  } __except (EXCEPTION_EXECUTE_HANDLER) {
  }
  return false;
}

static bool GhostRig_SetParent(void *transform, void *parent) {
  if (!transform || !s_ghostTransformSetParent)
    return false;
  bool worldPositionStays = false;
  void *params[] = {parent, &worldPositionStays};
  Invoke(s_ghostTransformSetParent, transform, params);
  return true;
}

static int GhostRig_GetHideFlags(void *object) {
  if (!object || !s_ghostObjectGetHideFlags)
    return INT_MIN;
  void *boxed = Invoke(s_ghostObjectGetHideFlags, object);
  return boxed ? UnboxInt(boxed) : INT_MIN;
}

static int GhostRig_GetComponentCount(void *gameObject) {
  if (!gameObject || !s_ghostGameObjectGetComponents || !g_componentClass ||
      !il2cpp_class_get_type || !il2cpp_type_get_object)
    return -1;
  void *componentType =
      il2cpp_type_get_object(il2cpp_class_get_type(g_componentClass));
  if (!componentType)
    return -1;
  void *params[] = {componentType};
  void *array = Invoke(s_ghostGameObjectGetComponents, gameObject, params);
  if (!array)
    return -1;
  __try {
    return static_cast<int>(
        *reinterpret_cast<uintptr_t *>((char *)array + 24));
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return -1;
  }
}

static void GhostRig_FreeNodeHandles() {
  if (!il2cpp_gchandle_free)
    return;
  for (int i = GHOST_NODE_COUNT - 1; i >= 0; --i) {
    if (s_ghostRig.nodes[i].transformHandle) {
      il2cpp_gchandle_free(s_ghostRig.nodes[i].transformHandle);
      s_ghostRig.nodes[i].transformHandle = 0;
    }
    if (s_ghostRig.nodes[i].gameObjectHandle) {
      il2cpp_gchandle_free(s_ghostRig.nodes[i].gameObjectHandle);
      s_ghostRig.nodes[i].gameObjectHandle = 0;
    }
  }
}

static int GhostRig_FreeTargetHandles() {
  int freed = 0;
  if (il2cpp_gchandle_free) {
    for (uint32_t index = 0; index < DIRECT_VMD_BONE_COUNT; ++index) {
      if (s_ghostRig.targets[index].transformHandle) {
        il2cpp_gchandle_free(
            s_ghostRig.targets[index].transformHandle);
        s_ghostRig.targets[index].transformHandle = 0;
        ++freed;
      }
    }
  }
  memset(s_ghostRig.targets, 0, sizeof(s_ghostRig.targets));
  return freed;
}

static int GhostRig_FreeTwistTargetHandles() {
  int freed = 0;
  for (uint32_t channel = 0;
       channel < DIRECT_VMD_PHASE6_TWIST_CHANNEL_COUNT; ++channel) {
    for (uint32_t target = 0;
         target < DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL; ++target) {
      GhostRigTwistTargetRuntime &runtime =
          s_ghostRig.twistChannels[channel].targets[target];
      if (runtime.transformHandle && il2cpp_gchandle_free) {
        il2cpp_gchandle_free(runtime.transformHandle);
        ++freed;
      }
      runtime.transformHandle = 0;
      runtime.resolved = false;
    }
  }
  return freed;
}

static int GhostRig_FreeOwnerRootHandle() {
  if (!s_ghostRig.ownerRootHandle || !il2cpp_gchandle_free)
    return 0;
  il2cpp_gchandle_free(s_ghostRig.ownerRootHandle);
  s_ghostRig.ownerRootHandle = 0;
  return 1;
}

static bool GhostRig_IsComponentUnderOwner(void *component,
                                           void *ownerRoot);

static bool GhostRig_AcquireEyeLookAtOwnership(void *ownerRoot) {
  if (!GhostRig_RequireMainThread("GhostRig.AcquireEyeLookAt", false) ||
      !ownerRoot)
    return false;

  void *smc = GhostRig_GetHandleTarget(s_ghostRig.eyeSmcHandle);
  if (smc && GhostRig_IsUnityObjectAlive(smc)) {
    __try {
      const int offset =
          SafeOff(OFF_smcEyeLookAt, 0x1dd, "enableEyeLookAtIK");
      *(bool *)((char *)smc + offset) = false;
      return true;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
      return false;
    }
  }

  if (!g_confirmedSMC || !GhostRig_IsUnityObjectAlive(g_confirmedSMC) ||
      !GhostRig_IsComponentUnderOwner(g_confirmedSMC, ownerRoot))
    return false;
  __try {
    const int offset =
        SafeOff(OFF_smcEyeLookAt, 0x1dd, "enableEyeLookAtIK");
    const bool saved = *(bool *)((char *)g_confirmedSMC + offset);
    const uint32_t handle = il2cpp_gchandle_new(g_confirmedSMC, false);
    if (!handle)
      return false;
    s_ghostRig.eyeSmcHandle = handle;
    s_ghostRig.eyeLookAtSaved = true;
    s_ghostRig.eyeLookAtSavedValue = saved;
    *(bool *)((char *)g_confirmedSMC + offset) = false;
    Log("[P6-EYE-OWNER] acquired=1 smc=%p handle=%u savedEyeLookAt=%d "
        "offset=0x%X generation=%llu owner=%p mainThreadOnly=1 tid=%lu",
        g_confirmedSMC, handle, saved ? 1 : 0, offset,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static void GhostRig_RestoreEyeLookAtOwnership(
    GhostRigCleanupReason reason) {
  if (!GhostRig_RequireMainThread("GhostRig.RestoreEyeLookAt", false))
    return;
  void *smc = GhostRig_GetHandleTarget(s_ghostRig.eyeSmcHandle);
  bool restored = false;
  if (smc && GhostRig_IsUnityObjectAlive(smc) &&
      s_ghostRig.eyeLookAtSaved) {
    __try {
      const int offset =
          SafeOff(OFF_smcEyeLookAt, 0x1dd, "enableEyeLookAtIK");
      *(bool *)((char *)smc + offset) = s_ghostRig.eyeLookAtSavedValue;
      restored = true;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
    }
  }
  const bool hadHandle = s_ghostRig.eyeSmcHandle != 0;
  if (s_ghostRig.eyeSmcHandle && il2cpp_gchandle_free)
    il2cpp_gchandle_free(s_ghostRig.eyeSmcHandle);
  s_ghostRig.eyeSmcHandle = 0;
  s_ghostRig.eyeLookAtSaved = false;
  s_ghostRig.eyeLookAtSavedValue = false;
  if (hadHandle) {
    Log("[P6-EYE-OWNER] restored=%d reason=%s smc=%p "
        "generation=%llu owner=%p duplicateReleaseSafe=1 tid=%lu",
        restored ? 1 : 0, GhostRig_CleanupReasonName(reason), smc,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
}

static bool GhostRig_RestoreTargetRootAnchor(
    GhostRigCleanupReason reason) {
  if (!s_ghostRig.rootMotionOwned)
    return true;

  void *ownerRoot = GhostRig_GetRetainedOwnerRoot();
  const bool alive = ownerRoot && GhostRig_IsUnityObjectAlive(ownerRoot);
  bool positionRestored = false;
  bool rotationRestored = false;
  if (alive && s_ghostRig.restoreAnchorValid) {
    rotationRestored = GhostRig_WriteWorldRotation(
        ownerRoot, {s_ghostRig.restoreAnchorRotation.x,
                    s_ghostRig.restoreAnchorRotation.y,
                    s_ghostRig.restoreAnchorRotation.z,
                    s_ghostRig.restoreAnchorRotation.w});
    positionRestored = GhostRig_WriteWorldPosition(
        ownerRoot, {s_ghostRig.restoreAnchorPosition.x,
                    s_ghostRig.restoreAnchorPosition.y,
                    s_ghostRig.restoreAnchorPosition.z});
  }
  const bool restored = positionRestored && rotationRestored;
  if (restored)
    ++s_ghostRig.rootRestoreCount;
  Log("[P4-ROOT-RESTORE] restored=%d positionRestored=%d "
      "rotationRestored=%d reason=%s root=%p alive=%d "
      "restoreAnchorValid=%d "
      "generation=%llu owner=%p tid=%lu "
      "duplicateDestroyPrevented=1",
      restored ? 1 : 0, positionRestored ? 1 : 0,
      rotationRestored ? 1 : 0, GhostRig_CleanupReasonName(reason),
      ownerRoot, alive ? 1 : 0,
      s_ghostRig.restoreAnchorValid ? 1 : 0,
      (unsigned long long)s_ghostRig.generation,
      reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
      GetCurrentThreadId());
  s_ghostRig.rootMotionOwned = false;
  s_ghostRig.lastDesiredRootValid = false;
  return restored;
}

static void GhostRig_DestroyMainThread(GhostRigCleanupReason reason) {
  if (!GhostRig_RequireMainThread("GhostRig.Destroy", false))
    return;

  if (s_cloth.owner.backend == static_cast<uint32_t>(MotionBackend::DirectVmd))
    ClothRelease("ghost-lifecycle-cleanup");
  if (s_ghostRig.state == GhostRigState::Empty) {
    GhostRig_ClearPhase5FinalIkOwnership(reason);
    GhostRig_RestoreEyeLookAtOwnership(reason);
    GhostRig_RestoreTargetRootAnchor(reason);
    const int targetHandles = GhostRig_FreeTargetHandles();
    const int twistHandles = GhostRig_FreeTwistTargetHandles();
    const int rootHandles = GhostRig_FreeOwnerRootHandle();
    s_ghostRig.anchorValid = false;
    s_ghostRig.playbackAnchorCaptured = false;
    s_ghostRig.restoreAnchorValid = false;
    s_ghostRig.rootPlacementValid = false;
    GhostRig_ResetPhase7TerrainState();
    s_ghostAlivePublic.store(false, std::memory_order_release);
    Log("[P0-GHOST-CLEANUP] no-op state=empty reason=%s tid=%lu "
        "generation=%llu owner=%p targetHandlesFreed=%d twistHandlesFreed=%d "
        "rootHandlesFreed=%d",
        GhostRig_CleanupReasonName(reason), GetCurrentThreadId(),
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter), targetHandles,
        twistHandles, rootHandles);
    return;
  }
  if (s_ghostRig.state == GhostRigState::Destroying)
    return;

  GhostRigState previousState = s_ghostRig.state;
  GhostRig_ClearPhase5FinalIkOwnership(reason);
  GhostRig_RestoreEyeLookAtOwnership(reason);
  GhostRig_RestoreTargetRootAnchor(reason);
  s_ghostRig.state = GhostRigState::Destroying;
  void *root = GhostRig_GetGameObject(0);
  int destroyCalls = 0;
  if (!s_ghostRig.destroyIssued && s_ghostObjectDestroy) {
    if (previousState == GhostRigState::Creating) {
      for (int i = GHOST_NODE_COUNT - 1; i >= 0; --i) {
        void *object = GhostRig_GetGameObject(i);
        if (!object || !GhostRig_IsUnityObjectAlive(object))
          continue;
        void *params[] = {object};
        Invoke(s_ghostObjectDestroy, nullptr, params);
        ++destroyCalls;
      }
    } else if (root && GhostRig_IsUnityObjectAlive(root)) {
      void *params[] = {root};
      Invoke(s_ghostObjectDestroy, nullptr, params);
      ++destroyCalls;
    }
    s_ghostRig.destroyIssued = true;
  }

  Log("[P0-GHOST-CLEANUP] destroyCalls=%d previousState=%u reason=%s "
      "tid=%lu generation=%llu owner=%p root=%p",
      destroyCalls, static_cast<unsigned>(previousState),
      GhostRig_CleanupReasonName(reason),
      GetCurrentThreadId(), (unsigned long long)s_ghostRig.generation,
      reinterpret_cast<void *>(s_ghostRig.ownerCharacter), root);

  const int targetHandlesFreed = GhostRig_FreeTargetHandles();
  const int twistHandlesFreed = GhostRig_FreeTwistTargetHandles();
  const int rootHandlesFreed = GhostRig_FreeOwnerRootHandle();
  Log("[P3-TARGET-CLEANUP] reason=%s handlesFreed=%d "
      "rotationWrites=%llu feedbackPoseReads=0 diagnosticPoseReads=%llu "
      "finalIkSafetySuppressed=%llu finalIkOriginalRuns=%llu "
      "rootWorldWrites=%llu rootRestores=%llu "
      "hipsBindPositionWrites=%llu hipsPostCorrections=%llu "
      "rootHandlesFreed=%d twistHandlesFreed=%d "
      "tid=%lu generation=%llu owner=%p",
      GhostRig_CleanupReasonName(reason), targetHandlesFreed,
      (unsigned long long)s_ghostRig.targetRotationWriteCount,
      (unsigned long long)s_ghostRig.postFinalIkDiagnosticReadCount,
      (unsigned long long)s_ghostRig.finalIkSuppressedCount,
      (unsigned long long)s_ghostRig.finalIkOriginalRunCount,
      (unsigned long long)s_ghostRig.rootWorldWriteCount,
      (unsigned long long)s_ghostRig.rootRestoreCount,
      (unsigned long long)s_ghostRig.hipsBindPositionWriteCount,
      (unsigned long long)s_ghostRig.hipsPostCorrectionCount,
      rootHandlesFreed, twistHandlesFreed,
      GetCurrentThreadId(), (unsigned long long)s_ghostRig.generation,
      reinterpret_cast<void *>(s_ghostRig.ownerCharacter));
  if (s_ghostRig.terrainEnabledLast || s_ghostRig.terrainFrameValid ||
      s_ghostRig.terrainQueryCount != 0) {
    Log("[P7-TERRAIN-CLEAR] reason=%s enabled=%d frameValid=%d "
        "support=%s rootOffset=%.6f queries=%llu hits=%llu "
        "generation=%llu owner=%p stateCleared=1 managedHandles=0 "
        "tid=%lu",
        GhostRig_CleanupReasonName(reason),
        s_ghostRig.terrainEnabledLast ? 1 : 0,
        s_ghostRig.terrainFrameValid ? 1 : 0,
        DirectVmdTerrainSupportStateName(
            s_ghostRig.terrainState.support),
        s_ghostRig.terrainState.rootOffset,
        (unsigned long long)s_ghostRig.terrainQueryCount,
        (unsigned long long)s_ghostRig.terrainHitCount,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
  GhostRig_FreeNodeHandles();
  memset(s_ghostRig.nodes, 0, sizeof(s_ghostRig.nodes));
  s_ghostRig.state = GhostRigState::Empty;
  s_ghostRig.ownerCharacter = 0;
  s_ghostRig.sceneHandle = INT_MIN;
  s_ghostRig.lastAppliedFrame = INT_MIN;
  s_ghostRig.lastPoseLogFrame = INT_MIN;
  s_ghostRig.lastTargetAppliedFrame = INT_MIN;
  s_ghostRig.lastFingerAppliedFrame = INT_MIN;
  s_ghostRig.lastEyeAppliedFrame = INT_MIN;
  s_ghostRig.lastTwistAppliedFrame = INT_MIN;
  s_ghostRig.lastTargetLogFrame = INT_MIN;
  s_ghostRig.lastFingerLogFrame = INT_MIN;
  s_ghostRig.lastEyeLogFrame = INT_MIN;
  s_ghostRig.lastTwistLogFrame = INT_MIN;
  s_ghostRig.lastPostFinalIkLogFrame = INT_MIN;
  s_ghostRig.lastFinalIkSuppressedLogFrame = INT_MIN;
  s_ghostRig.lastRootAppliedFrame = INT_MIN;
  s_ghostRig.lastRootLogFrame = INT_MIN;
  s_ghostRig.lastTerrainFrame = INT_MIN;
  s_ghostRig.lastTerrainLogFrame = INT_MIN;
  s_ghostRig.lastHipsCorrectionFrame = INT_MIN;
  s_ghostRig.lastHipsCorrectionLogFrame = INT_MIN;
  s_ghostRig.lastLegPreparedFrame = INT_MIN;
  s_ghostRig.lastLegLogFrame = INT_MIN;
  s_ghostRig.applyCount = 0;
  s_ghostRig.targetApplyCount = 0;
  s_ghostRig.targetRotationWriteCount = 0;
  s_ghostRig.fingerRotationWriteCount = 0;
  s_ghostRig.eyeRotationWriteCount = 0;
  s_ghostRig.twistRotationWriteCount = 0;
  s_ghostRig.postFinalIkDiagnosticReadCount = 0;
  s_ghostRig.finalIkSuppressedCount = 0;
  s_ghostRig.rootApplyCount = 0;
  s_ghostRig.rootWorldWriteCount = 0;
  s_ghostRig.rootRestoreCount = 0;
  s_ghostRig.hipsBindPositionWriteCount = 0;
  s_ghostRig.hipsPostCorrectionCount = 0;
  s_ghostRig.finalIkOriginalRunCount = 0;
  s_ghostRig.finalIkSafetySuppressCount = 0;
  s_ghostRig.cadenceUnityFrames = 0;
  s_ghostRig.cadenceSampleAdvances = 0;
  s_ghostRig.cadenceRepeatedSamples = 0;
  s_ghostRig.cadenceMaxSourceFrameStep = 0.0;
  s_ghostRig.destroyIssued = false;
  s_ghostRig.anchorValid = false;
  s_ghostRig.playbackAnchorCaptured = false;
  s_ghostRig.restoreAnchorValid = false;
  s_ghostRig.rootPlacementValid = false;
  s_ghostRig.rootMotionOwned = false;
  s_ghostRig.terrainEnabledLast = false;
  s_ghostRig.terrainFrameValid = false;
  s_ghostRig.lastSampleAccepted = false;
  s_ghostRig.rootCycleSeen = false;
  s_ghostRig.anchorPosition = {0.0f, 0.0f, 0.0f};
  s_ghostRig.anchorRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  s_ghostRig.restoreAnchorPosition = {0.0f, 0.0f, 0.0f};
  s_ghostRig.restoreAnchorRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  s_ghostRig.rootPlacementOffset = {0.0f, 0.0f, 0.0f};
  s_ghostRig.lastDesiredRootPosition = {0.0f, 0.0f, 0.0f};
  s_ghostRig.lastDesiredRootRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  s_ghostRig.lastDesiredRootValid = false;
  s_ghostRig.lastHipsPositionBeforeCorrection = {0.0f, 0.0f, 0.0f};
  s_ghostRig.lastHipsCorrectionDistance = 0.0f;
  s_ghostRig.bindState = GhostBindCaptureState::Empty;
  s_ghostRig.bindGeneration = 0;
  s_ghostRig.bindOwnerCharacter = 0;
  s_ghostRig.bindAttemptFrame = INT_MIN;
  s_ghostRig.leftLegLength = 0.0f;
  s_ghostRig.rightLegLength = 0.0f;
  s_ghostRig.targetLegLength = 0.0f;
  s_ghostRig.targetNaturalBindHeight = 0.0f;
  s_ghostRig.baseMotionScale = 0.0f;
  s_ghostRig.motionScale = 0.0f;
  s_ghostRig.terrainConfig = DirectVmdTerrainConfig();
  s_ghostRig.terrainAnkleClearance[0] = 0.0f;
  s_ghostRig.terrainAnkleClearance[1] = 0.0f;
  s_ghostRig.terrainEnabledLast = false;
  GhostRig_ResetPhase7TerrainState();
  s_ghostRig.lastSampleSequence = 0;
  s_ghostRig.lastPlaybackCycle = 0;
  s_ghostRig.lastAppliedRootCycle = 0;
  s_ghostRig.lastSourceFrame = 0.0;
  s_ghostRig.lastSamplePlayback = DirectVmdPlaybackState::Stopped;
  s_ghostRig.lastLeftFootIkEnabled = true;
  s_ghostRig.lastRightFootIkEnabled = true;
  s_ghostRig.finalIkBipedHandle = 0;
  s_ghostRig.eyeSmcHandle = 0;
  s_ghostRig.eyeLookAtSaved = false;
  s_ghostRig.eyeLookAtSavedValue = false;
  s_ghostRig.bindStatus[0] = '\0';
  s_ghostRig.leftWristBindFrame = GhostRigHandBindFrame();
  s_ghostRig.rightWristBindFrame = GhostRigHandBindFrame();
  memset(s_ghostRig.twistChannels, 0,
         sizeof(s_ghostRig.twistChannels));
  memset(s_ghostRig.targetNaturalBindAvailable, 0,
         sizeof(s_ghostRig.targetNaturalBindAvailable));
  s_ghostBindStatePublic.store(
      static_cast<uint32_t>(GhostBindCaptureState::Empty),
      std::memory_order_release);
  s_ghostMotionScalePublic.store(0.0f, std::memory_order_release);
  s_ghostAlivePublic.store(false, std::memory_order_release);
  DirectVmdSource_ReleaseReference();

  if (reason == GhostRigCleanupReason::CreateFailed) {
    g_motionBackend.TransitionTo(MotionBackend::Native);
    DirectVmdRuntime_SetActive(false);
    DirectVmdRuntime_RequestStop();
    SafeSetAnimatorEnabled(true);
    s_ghostDesiredEnabled.store(false, std::memory_order_release);
    s_ghostRequestedCleanupReason.store(
        static_cast<uint32_t>(GhostRigCleanupReason::CreateFailed),
        std::memory_order_release);
    uint64_t failedGeneration =
        s_ghostRequestedGeneration.fetch_add(1,
                                              std::memory_order_acq_rel) + 1;
    s_ghostRig.generation = failedGeneration;
    s_ghostPublicGeneration.store(failedGeneration,
                                  std::memory_order_release);
    Log("[P4-GHOST-CREATE] backend=Native disabled-after-failure "
        "ghostGeneration=%llu backendGeneration=%llu nativeAnimator=1 "
        "tid=%lu",
        (unsigned long long)failedGeneration,
        (unsigned long long)g_motionBackend.Generation(),
        GetCurrentThreadId());
  }
}

static void *GhostRig_GetOwnerRoot(uintptr_t ownerCharacter) {
  if (!ownerCharacter ||
      reinterpret_cast<uintptr_t>(g_mainCharEntity) != ownerCharacter ||
      !g_cachedAnimator)
    return nullptr;
  void *root = SafeGetComponentTransform(g_cachedAnimator);
  return GhostRig_IsUnityObjectAlive(root) ? root : nullptr;
}

static bool GhostRig_IsComponentUnderOwner(void *component,
                                           void *ownerRoot) {
  if (!component || !ownerRoot || !g_component_get_transform)
    return false;
  void *current = SafeGetComponentTransform(component);
  for (int depth = 0; current && depth < 32; ++depth) {
    if (GhostRig_SameUnityObject(current, ownerRoot))
      return true;
    if (!g_transform_get_parent)
      break;
    current = Invoke(g_transform_get_parent, current);
  }
  return false;
}

// BE-PATCH(squad-anchor): in squad playback every member shares one stage
// origin (the leader's pose at start) and no first-frame placement, so the
// formation authored in multi-dancer VMDs is kept. Set by eiem_body.cpp.
static bool g_beFixedAnchorValid = false;
static Vec3 g_beFixedAnchorPosition = {};
static Quat g_beFixedAnchorRotation = {0, 0, 0, 1};

static bool GhostRig_ReadAnchorPose(void *ownerRoot, Vec3 &position,
                                    Quat &rotation) {
  if (g_beFixedAnchorValid) {
    position = g_beFixedAnchorPosition;
    rotation = g_beFixedAnchorRotation;
    return true;
  }
  return GhostRig_ReadWorldPosition(ownerRoot, position) &&
         GhostRig_ReadWorldRotation(ownerRoot, rotation);
}

static bool GhostRig_AlignRoot(void *ownerRoot) {
  void *ghostRoot = GhostRig_GetTransform(0);
  if (!ghostRoot || !ownerRoot)
    return false;
  if (!s_ghostRig.anchorValid) {
    Vec3 position = {};
    Quat rotation = {0, 0, 0, 1};
    if (!GhostRig_ReadAnchorPose(ownerRoot, position, rotation))
      return false;
    s_ghostRig.anchorPosition = {position.x, position.y, position.z};
    s_ghostRig.anchorRotation = DirectVmdNormalizeQuaternion(
        {rotation.x, rotation.y, rotation.z, rotation.w});
    s_ghostRig.anchorValid = true;
    Log("[P4-ROOT-PROVISIONAL] captured=1 playbackCaptured=0 "
        "generation=%llu owner=%p "
        "position=(%.6f,%.6f,%.6f) rotation=(%.7f,%.7f,%.7f,%.7f) "
        "ghostRootParent=null realRootWrites=0 tid=%lu",
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        s_ghostRig.anchorPosition.x, s_ghostRig.anchorPosition.y,
        s_ghostRig.anchorPosition.z, s_ghostRig.anchorRotation.x,
        s_ghostRig.anchorRotation.y, s_ghostRig.anchorRotation.z,
        s_ghostRig.anchorRotation.w, GetCurrentThreadId());
  }
  const Vec3 position = {s_ghostRig.anchorPosition.x,
                         s_ghostRig.anchorPosition.y,
                         s_ghostRig.anchorPosition.z};
  const Quat rotation = {s_ghostRig.anchorRotation.x,
                         s_ghostRig.anchorRotation.y,
                         s_ghostRig.anchorRotation.z,
                         s_ghostRig.anchorRotation.w};
  return GhostRig_WriteWorldPosition(ghostRoot, position) &&
         GhostRig_WriteWorldRotation(ghostRoot, rotation);
}

struct GhostSkeletonBoneRecord {
  VmdVec3 position;
  VmdQuaternion rotation;
  VmdVec3 scale;
};

struct GhostTargetBindPose {
  VmdVec3 position;
  VmdQuaternion rotation;
  VmdVec3 scale = {1.0f, 1.0f, 1.0f};
  bool valid = false;
};

static Vec3 GhostRig_ToVec3(VmdVec3 value) {
  return {value.x, value.y, value.z};
}

static Quat GhostRig_ToQuat(VmdQuaternion value) {
  value = DirectVmdNormalizeQuaternion(value);
  return {value.x, value.y, value.z, value.w};
}

static bool GhostRig_ReadPointerAt(void *base, size_t offset, void **output) {
  if (!base || !output)
    return false;
  __try {
    *output = *reinterpret_cast<void **>((char *)base + offset);
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    *output = nullptr;
    return false;
  }
}

static bool GhostRig_ReadArrayLength(void *array, uintptr_t *length) {
  if (!array || !length)
    return false;
  __try {
    *length = *reinterpret_cast<uintptr_t *>((char *)array + 24);
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    *length = 0;
    return false;
  }
}

static bool GhostRig_IsSkeletonBoneArray(void *array) {
  if (!array || !il2cpp_object_get_class || !il2cpp_class_get_name)
    return false;
  __try {
    void *klass = il2cpp_object_get_class(array);
    const char *name = klass ? il2cpp_class_get_name(klass) : nullptr;
    return name && strstr(name, "SkeletonBone") != nullptr;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return false;
  }
}

static bool GhostRig_ReadSkeletonRaw(void *element, int nameOffset,
                                     int positionOffset,
                                     int rotationOffset, int scaleOffset,
                                     void **name,
                                     GhostSkeletonBoneRecord *record) {
  if (!element || !name || !record)
    return false;
  __try {
    *name = *reinterpret_cast<void **>((char *)element + nameOffset);
    record->position =
        *reinterpret_cast<VmdVec3 *>((char *)element + positionOffset);
    record->rotation =
        *reinterpret_cast<VmdQuaternion *>((char *)element + rotationOffset);
    record->scale =
        *reinterpret_cast<VmdVec3 *>((char *)element + scaleOffset);
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    *name = nullptr;
    return false;
  }
}

static bool GhostRig_IsFiniteVector(VmdVec3 value) {
  return DirectVmdFinite(value.x) && DirectVmdFinite(value.y) &&
         DirectVmdFinite(value.z);
}

static void GhostRig_ClearBindCapture() {
  DirectVmdSource_ReleaseReference();
  s_ghostRig.bindSourceRevision = 0;
  for (int i = 0; i < GHOST_NODE_COUNT; ++i)
    s_ghostRig.nodes[i].bind = DirectVmdBindNodePod();
  memset(s_ghostRig.targetNaturalBindAvailable, 0,
         sizeof(s_ghostRig.targetNaturalBindAvailable));
  s_ghostRig.bindState = GhostBindCaptureState::Empty;
  s_ghostRig.bindGeneration = 0;
  s_ghostRig.bindOwnerCharacter = 0;
  s_ghostRig.bindAttemptFrame = INT_MIN;
  s_ghostRig.leftLegLength = 0.0f;
  s_ghostRig.rightLegLength = 0.0f;
  s_ghostRig.targetLegLength = 0.0f;
  s_ghostRig.targetNaturalBindHeight = 0.0f;
  s_ghostRig.baseMotionScale = 0.0f;
  s_ghostRig.motionScale = 0.0f;
  s_ghostRig.lastSampleSequence = 0;
  s_ghostRig.lastSourceFrame = 0.0;
  s_ghostRig.bindStatus[0] = '\0';
  s_ghostRig.leftWristBindFrame = GhostRigHandBindFrame();
  s_ghostRig.rightWristBindFrame = GhostRigHandBindFrame();
  memset(s_ghostRig.twistChannels, 0,
         sizeof(s_ghostRig.twistChannels));
  s_ghostBindStatePublic.store(
      static_cast<uint32_t>(GhostBindCaptureState::Empty),
      std::memory_order_release);
  s_ghostMotionScalePublic.store(0.0f, std::memory_order_release);
}

static void GhostRig_SetBindStatus(GhostBindCaptureState state,
                                   const char *status) {
  s_ghostRig.bindState = state;
  s_ghostBindStatePublic.store(static_cast<uint32_t>(state),
                               std::memory_order_release);
  strncpy_s(s_ghostRig.bindStatus, sizeof(s_ghostRig.bindStatus),
            status ? status : "unknown", _TRUNCATE);
}

static GhostTargetBindPose GhostRig_ComposeSkeletonPose(
    const GhostTargetBindPose &parent,
    const GhostSkeletonBoneRecord &local) {
  const VmdVec3 scaled = {local.position.x * parent.scale.x,
                          local.position.y * parent.scale.y,
                          local.position.z * parent.scale.z};
  GhostTargetBindPose result;
  result.position = DirectVmdAdd(
      parent.position, DirectVmdRotateVector(parent.rotation, scaled));
  result.rotation = DirectVmdQuaternionMultiply(parent.rotation,
                                                 local.rotation);
  result.scale = {parent.scale.x * local.scale.x,
                  parent.scale.y * local.scale.y,
                  parent.scale.z * local.scale.z};
  result.valid = true;
  return result;
}

static bool GhostRig_ReadSkeletonMetadata(
    std::map<std::string, GhostSkeletonBoneRecord> *records,
    std::map<std::string, int> *nameCounts, char *status,
    size_t statusSize) {
  if (!records || !nameCounts || !g_cachedAnimator ||
      !g_animator_get_avatar || !s_ghostAvatarGetHumanDescription ||
      s_ghostHumanSkeletonOffset < 0 || s_ghostSkeletonStride <= 0) {
    strncpy_s(status, statusSize, "Avatar skeleton metadata APIs unavailable",
              _TRUNCATE);
    return false;
  }

  if (g_animator_get_isHuman) {
    void *boxedHuman = Invoke(g_animator_get_isHuman, g_cachedAnimator);
    if (!boxedHuman || !UnboxBool(boxedHuman)) {
      strncpy_s(status, statusSize, "Animator avatar is not Humanoid",
                _TRUNCATE);
      return false;
    }
  }

  void *avatar = Invoke(g_animator_get_avatar, g_cachedAnimator);
  if (!avatar) {
    strncpy_s(status, statusSize, "Animator avatar is not ready", _TRUNCATE);
    return false;
  }
  void *boxedDescription =
      Invoke(s_ghostAvatarGetHumanDescription, avatar);
  if (!boxedDescription) {
    strncpy_s(status, statusSize, "Avatar.humanDescription returned null",
              _TRUNCATE);
    return false;
  }

  void *skeletonArray = nullptr;
  uintptr_t skeletonCount = 0;
  const size_t unboxedOffset =
      16 + static_cast<size_t>(s_ghostHumanSkeletonOffset);
  if (!GhostRig_ReadPointerAt(boxedDescription, unboxedOffset,
                              &skeletonArray) ||
      !GhostRig_ReadArrayLength(skeletonArray, &skeletonCount) ||
      skeletonCount == 0 || skeletonCount > 8192 ||
      !GhostRig_IsSkeletonBoneArray(skeletonArray)) {
    skeletonArray = nullptr;
    skeletonCount = 0;
    if (!GhostRig_ReadPointerAt(
            boxedDescription,
            static_cast<size_t>(s_ghostHumanSkeletonOffset),
            &skeletonArray) ||
        !GhostRig_ReadArrayLength(skeletonArray, &skeletonCount) ||
        skeletonCount == 0 || skeletonCount > 8192 ||
        !GhostRig_IsSkeletonBoneArray(skeletonArray)) {
      strncpy_s(status, statusSize,
                "HumanDescription.skeleton array is missing or invalid",
                _TRUNCATE);
      return false;
    }
    Log("[P2-BIND-METADATA] humanDescriptionOffsetMode=boxed "
        "fieldOffset=0x%X",
        s_ghostHumanSkeletonOffset);
  } else {
    Log("[P2-BIND-METADATA] humanDescriptionOffsetMode=unboxed "
        "fieldOffset=0x%X",
        s_ghostHumanSkeletonOffset);
  }

  size_t validRecords = 0;
  for (uintptr_t index = 0; index < skeletonCount; ++index) {
    void *element = (char *)skeletonArray + 32 +
                    index * static_cast<size_t>(s_ghostSkeletonStride);
    void *nameObject = nullptr;
    GhostSkeletonBoneRecord record = {};
    if (!GhostRig_ReadSkeletonRaw(
            element, s_ghostSkeletonNameOffset,
            s_ghostSkeletonPositionOffset, s_ghostSkeletonRotationOffset,
            s_ghostSkeletonScaleOffset, &nameObject, &record))
      continue;
    char name[512] = {};
    if (ReadStrUtf8(nameObject, name, sizeof(name)) <= 0)
      continue;
    const float rotationLengthSquared =
        record.rotation.x * record.rotation.x +
        record.rotation.y * record.rotation.y +
        record.rotation.z * record.rotation.z +
        record.rotation.w * record.rotation.w;
    if (!GhostRig_IsFiniteVector(record.position) ||
        !GhostRig_IsFiniteVector(record.scale) ||
        !DirectVmdFinite(rotationLengthSquared) ||
        rotationLengthSquared < 1.0e-12f ||
        record.scale.x <= 1.0e-6f || record.scale.x > 100.0f ||
        record.scale.y <= 1.0e-6f || record.scale.y > 100.0f ||
        record.scale.z <= 1.0e-6f || record.scale.z > 100.0f)
      continue;
    record.rotation = DirectVmdNormalizeQuaternion(record.rotation);
    ++(*nameCounts)[name];
    (*records)[name] = record;
    ++validRecords;
  }

  if (validRecords == 0) {
    strncpy_s(status, statusSize,
              "Avatar skeleton metadata contained no valid records",
              _TRUNCATE);
    return false;
  }
  Log("[P2-BIND-METADATA] skeletonCount=%llu valid=%zu stride=%d "
      "offsets=name:0x%X,pos:0x%X,rot:0x%X,scale:0x%X tid=%lu",
      (unsigned long long)skeletonCount, validRecords,
      s_ghostSkeletonStride, s_ghostSkeletonNameOffset,
      s_ghostSkeletonPositionOffset, s_ghostSkeletonRotationOffset,
      s_ghostSkeletonScaleOffset, GetCurrentThreadId());
  return true;
}

static bool GhostRig_BuildBindPoseFromTransform(
    void *transform, void *ownerRoot,
    const std::map<std::string, GhostSkeletonBoneRecord> &records,
    const std::map<std::string, int> &nameCounts,
    GhostTargetBindPose *output, char *transformName,
    size_t transformNameSize, char *status, size_t statusSize) {
  if (!transform || !ownerRoot || !output || !g_transform_get_parent) {
    strncpy_s(status, statusSize, "Humanoid Transform is unavailable",
              _TRUNCATE);
    return false;
  }

  std::vector<std::string> path;
  void *current = transform;
  bool reachedOwnerRoot = false;
  for (int depth = 0; current && depth < 128; ++depth) {
    if (GhostRig_SameUnityObject(current, ownerRoot)) {
      reachedOwnerRoot = true;
      break;
    }
    char name[512] = {};
    SafeGetBoneName(current, name, sizeof(name));
    if (name[0] == '\0') {
      strncpy_s(status, statusSize,
                "Transform parent path contains an unnamed node",
                _TRUNCATE);
      return false;
    }
    path.emplace_back(name);
    current = Invoke(g_transform_get_parent, current);
  }
  if (!reachedOwnerRoot || path.empty()) {
    strncpy_s(status, statusSize,
              "Humanoid Transform is not below Animator.transform",
              _TRUNCATE);
    return false;
  }

  GhostTargetBindPose pose;
  pose.position = {0.0f, 0.0f, 0.0f};
  pose.rotation = {0.0f, 0.0f, 0.0f, 1.0f};
  pose.scale = {1.0f, 1.0f, 1.0f};
  pose.valid = true;
  for (auto iterator = path.rbegin(); iterator != path.rend(); ++iterator) {
    const auto count = nameCounts.find(*iterator);
    const auto record = records.find(*iterator);
    if (count == nameCounts.end() || count->second != 1 ||
        record == records.end()) {
      _snprintf_s(status, statusSize, _TRUNCATE,
                  "Skeleton metadata path missing/ambiguous node '%s'",
                  iterator->c_str());
      return false;
    }
    pose = GhostRig_ComposeSkeletonPose(pose, record->second);
  }
  *output = pose;
  if (transformName && transformNameSize > 0)
    strncpy_s(transformName, transformNameSize, path.front().c_str(),
              _TRUNCATE);
  return true;
}

static bool GhostRig_CaptureHumanBindPose(
    int humanBone, const char *semanticName, void *ownerRoot,
    const std::map<std::string, GhostSkeletonBoneRecord> &records,
    const std::map<std::string, int> &nameCounts,
    GhostTargetBindPose *output, char *status, size_t statusSize) {
  void *transform = SafeGetBoneTransform(humanBone);
  if (!transform) {
    _snprintf_s(status, statusSize, _TRUNCATE,
                "Humanoid mapping missing required bone '%s' (id=%d)",
                semanticName, humanBone);
    return false;
  }
  char transformName[512] = {};
  if (!GhostRig_BuildBindPoseFromTransform(
          transform, ownerRoot, records, nameCounts, output,
          transformName, sizeof(transformName), status, statusSize))
    return false;
  Log("[P2-BIND-MAP] semantic='%s' humanBone=%d transform='%s' "
      "source=Avatar.humanDescription.skeleton "
      "ownerLocalP=(%.6f,%.6f,%.6f) ownerLocalR=(%.7f,%.7f,%.7f,%.7f)",
      semanticName, humanBone, transformName, output->position.x,
      output->position.y, output->position.z, output->rotation.x,
      output->rotation.y, output->rotation.z, output->rotation.w);
  return true;
}

static bool GhostRig_TryCaptureAuxiliaryBindPose(
    int humanBone, void *ownerRoot,
    const std::map<std::string, GhostSkeletonBoneRecord> &records,
    const std::map<std::string, int> &nameCounts,
    GhostTargetBindPose *output, char *reason, size_t reasonSize) {
  if (!output || !reason || reasonSize == 0)
    return false;
  *output = GhostTargetBindPose();
  reason[0] = '\0';
  void *transform = SafeGetBoneTransform(humanBone);
  if (!transform) {
    _snprintf_s(reason, reasonSize, _TRUNCATE,
                "Humanoid mapping missing (id=%d)", humanBone);
    return false;
  }
  char transformName[512] = {};
  if (!GhostRig_BuildBindPoseFromTransform(
          transform, ownerRoot, records, nameCounts, output,
          transformName, sizeof(transformName), reason, reasonSize))
    return false;
  _snprintf_s(reason, reasonSize, _TRUNCATE, "ok:%s", transformName);
  return true;
}

static bool GhostRig_CaptureNamedBindPose(
    const char *transformName, const char *semanticName, void *ownerRoot,
    const std::map<std::string, GhostSkeletonBoneRecord> &records,
    const std::map<std::string, int> &nameCounts,
    GhostTargetBindPose *output, char *status, size_t statusSize) {
  if (!transformName || !transformName[0] || !ownerRoot || !output) {
    strncpy_s(status, statusSize, "Named Transform mapping is unavailable",
              _TRUNCATE);
    return false;
  }
  void *transform = SafeFindChildRecursive(ownerRoot, transformName, 64);
  if (!transform || !GhostRig_IsUnityObjectAlive(transform) ||
      !GhostRig_IsComponentUnderOwner(transform, ownerRoot)) {
    _snprintf_s(status, statusSize, _TRUNCATE,
                "Target Transform '%s' is missing or outside owner",
                transformName);
    return false;
  }
  char resolvedName[512] = {};
  if (!GhostRig_BuildBindPoseFromTransform(
          transform, ownerRoot, records, nameCounts, output,
          resolvedName, sizeof(resolvedName), status, statusSize))
    return false;
  Log("[P6-BIND-MAP] semantic='%s' transform='%s' "
      "source=Avatar.humanDescription.skeleton "
      "ownerLocalP=(%.6f,%.6f,%.6f) "
      "ownerLocalR=(%.7f,%.7f,%.7f,%.7f) readsLivePose=0",
      semanticName ? semanticName : "unknown", resolvedName,
      output->position.x, output->position.y, output->position.z,
      output->rotation.x, output->rotation.y, output->rotation.z,
      output->rotation.w);
  return true;
}

static int GhostRig_Phase3HumanBone(DirectVmdBoneId id);

static int GhostRig_ChildCount(void *transform) {
  void *boxed = transform && g_transform_get_childCount
                    ? Invoke(g_transform_get_childCount, transform)
                    : nullptr;
  const int count = boxed ? UnboxInt(boxed) : 0;
  return count >= 0 && count <= 4096 ? count : 0;
}

static bool GhostRig_NameContainsTwist(const char *name) {
  for (const char *p = name; p && *p; ++p)
    if (_strnicmp(p, "twist", 5) == 0)
      return true;
  return false;
}

static bool GhostRig_ResolveTwistTransformNames(
    uint32_t channel, void *ownerRoot,
    const std::map<std::string, GhostSkeletonBoneRecord> &records,
    const std::map<std::string, int> &nameCounts,
    char (&names)[DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL][96],
    const char **source, char *reason, size_t reasonSize) {
  *source = "unresolved";
  reason[0] = '\0';
  for (auto &name : names)
    name[0] = '\0';

  bool legacyComplete = true;
  for (uint32_t index = 0;
       index < DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL; ++index) {
    const char *legacy = GhostRig_TwistTargetName(channel, index);
    legacyComplete = legacyComplete && legacy &&
                     SafeFindChildRecursive(ownerRoot, legacy, 64);
  }
  if (legacyComplete) {
    for (uint32_t index = 0;
         index < DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL; ++index)
      strncpy_s(names[index], GhostRig_TwistTargetName(channel, index),
                _TRUNCATE);
    *source = "legacy-name";
    return true;
  }

  const DirectVmdTwistChannelSpec &spec =
      kDirectVmdPhase6TwistChannels[channel];
  void *limbStart =
      SafeGetBoneTransform(GhostRig_Phase3HumanBone(spec.sourceLimb));
  void *limbEnd =
      SafeGetBoneTransform(GhostRig_Phase3HumanBone(spec.targetLimbEnd));
  GhostTargetBindPose startPose;
  GhostTargetBindPose endPose;
  char status[160] = {};
  if (!limbStart || !limbEnd ||
      !GhostRig_BuildBindPoseFromTransform(limbStart, ownerRoot, records,
                                           nameCounts, &startPose, nullptr,
                                           0, status, sizeof(status)) ||
      !GhostRig_BuildBindPoseFromTransform(limbEnd, ownerRoot, records,
                                           nameCounts, &endPose, nullptr, 0,
                                           status, sizeof(status))) {
    _snprintf_s(reason, reasonSize, _TRUNCATE,
                "limb bind unavailable: %s", status);
    return false;
  }
  const VmdVec3 axis = DirectVmdSub(endPose.position, startPose.position);
  const float limbLength = DirectVmdLength(axis);
  VmdVec3 axisUnit = {};
  if (!DirectVmdTryNormalizeVector(axis, &axisUnit) || limbLength < 1.0e-3f) {
    strncpy_s(reason, reasonSize, "degenerate limb axis", _TRUNCATE);
    return false;
  }

  std::vector<void *> humanoid;
  for (int bone = 0; bone <= HB_UpperChest; ++bone)
    if (void *transform = SafeGetBoneTransform(bone))
      humanoid.push_back(transform);
  auto isHumanoid = [&](void *transform) {
    for (void *other : humanoid)
      if (GhostRig_SameUnityObject(transform, other))
        return true;
    return false;
  };

  struct Candidate {
    void *transform;
    void *parent;
    float along;
    char name[96];
  };
  std::vector<Candidate> candidates;
  std::vector<std::pair<void *, int>> pending = {{limbStart, 0}};
  while (!pending.empty() && candidates.size() < 32) {
    const auto [node, depth] = pending.back();
    pending.pop_back();
    const int count = GhostRig_ChildCount(node);
    for (int child = 0; child < count; ++child) {
      void *args[] = {&child};
      void *next = Invoke(g_transform_GetChild, node, args);
      if (!next || GhostRig_SameUnityObject(next, limbEnd) ||
          isHumanoid(next))
        continue;
      if (depth + 1 < 3)
        pending.push_back({next, depth + 1});
      char name[96] = {};
      SafeGetBoneName(next, name, sizeof(name));
      GhostTargetBindPose pose;
      char ignored[160] = {};
      if (!GhostRig_NameContainsTwist(name) ||
          !GhostRig_BuildBindPoseFromTransform(next, ownerRoot, records,
                                               nameCounts, &pose, nullptr, 0,
                                               ignored, sizeof(ignored)))
        continue;
      const VmdVec3 offset = DirectVmdSub(pose.position, startPose.position);
      const float along = DirectVmdDot(offset, axisUnit) / limbLength;
      const float across = DirectVmdLength(DirectVmdSub(
          offset, DirectVmdScale(axisUnit, along * limbLength)));
      if (along < -0.05f || along > 0.95f || across > 0.1f * limbLength)
        continue;
      Candidate candidate = {next, node, along, {}};
      strncpy_s(candidate.name, name, _TRUNCATE);
      candidates.push_back(candidate);
    }
  }

  void *parent = limbStart;
  for (uint32_t index = 0;
       index < DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL; ++index) {
    const Candidate *best = nullptr;
    for (const Candidate &candidate : candidates)
      if (GhostRig_SameUnityObject(candidate.parent, parent) &&
          (!best || candidate.along < best->along))
        best = &candidate;
    if (!best) {
      _snprintf_s(reason, reasonSize, _TRUNCATE,
                  "structural chain incomplete at target %u "
                  "(onAxisTwistCandidates=%zu)",
                  index, candidates.size());
      return false;
    }
    strncpy_s(names[index], best->name, _TRUNCATE);
    parent = best->transform;
  }
  for (const auto &name : names) {
    const auto count = nameCounts.find(name);
    if (count == nameCounts.end() || count->second != 1) {
      _snprintf_s(reason, reasonSize, _TRUNCATE,
                  "structural candidate '%s' is not unique", name);
      return false;
    }
  }
  *source = "structural";
  return true;
}

static bool GhostRig_SourceDirection(DirectVmdBoneId id, VmdVec3 *out) {
  *out = s_ghostRig.sourceReference.direction[DirectVmdBoneIndex(id)];
  return DirectVmdLength(*out) > 1e-8f;
}

static bool GhostRig_SourceWrist(DirectVmdBoneId id, VmdVec3 *forward,
                                  VmdVec3 *lateral) {
  const int side = id == DirectVmdBoneId::LeftWrist ? 0 : 1;
  *forward = s_ghostRig.sourceReference.wristForward[side];
  *lateral = s_ghostRig.sourceReference.wristLateral[side];
  return true;
}

static void GhostRig_CaptureWristBindFrame(
    DirectVmdBoneId wristId, const GhostTargetBindPose &wrist,
    const GhostTargetBindPose &middle, bool hasMiddle,
    const GhostTargetBindPose &index, bool hasIndex,
    const GhostTargetBindPose &little, bool hasLittle,
    uint64_t generation, uintptr_t ownerCharacter,
    GhostRigHandBindFrame *output) {
  if (!output)
    return;
  *output = GhostRigHandBindFrame();
  const uint32_t semantic = DirectVmdBoneIndex(wristId);

  VmdVec3 sourceForward = {};
  VmdVec3 sourceLateral = {};
  if (!wrist.valid ||
      !GhostRig_SourceWrist(
          wristId, &sourceForward, &sourceLateral) ||
      !DirectVmdBuildOrthonormalFrame(
          SourceToGameBasis::ConvertPosition(sourceForward),
          SourceToGameBasis::ConvertPosition(sourceLateral),
          &output->sourceFrameGame)) {
    Log("[P3-WRIST-BIND] semantic='%s' mode=none "
        "reason=invalid-source-frame source=active-preset "
        "target=Avatar.humanDescription.skeleton readsLivePose=0 "
        "firstFrameCalibration=0 generation=%llu owner=%p tid=%lu",
        kDirectVmdBoneSpecs[semantic].name,
        (unsigned long long)generation,
        reinterpret_cast<void *>(ownerCharacter), GetCurrentThreadId());
    return;
  }

  const VmdVec3 targetForward =
      hasMiddle ? DirectVmdSub(middle.position, wrist.position)
                : VmdVec3{0.0f, 0.0f, 0.0f};
  const VmdVec3 targetLateral =
      hasIndex && hasLittle
          ? DirectVmdSub(index.position, little.position)
          : VmdVec3{0.0f, 0.0f, 0.0f};
  if (hasMiddle && hasIndex && hasLittle &&
      DirectVmdBuildFrameAlignment(
          SourceToGameBasis::ConvertPosition(sourceForward),
          SourceToGameBasis::ConvertPosition(sourceLateral),
          targetForward, targetLateral,
          &output->sourceToTargetOwnerAlignment,
          &output->sourceFrameGame, &output->targetFrameOwner)) {
    output->hasDirectionAlignment = true;
    output->hasFullFrameAlignment = true;
  } else {
    VmdVec3 targetForwardUnit = {};
    if (hasMiddle &&
        DirectVmdTryNormalizeVector(targetForward, &targetForwardUnit)) {
      output->targetFrameOwner.forward = targetForwardUnit;
      output->sourceToTargetOwnerAlignment = DirectVmdQuaternionFromTo(
          output->sourceFrameGame.forward, targetForwardUnit);
      output->hasDirectionAlignment = true;
    }
  }

  const char *mode = output->hasFullFrameAlignment
                         ? "full-frame"
                         : (output->hasDirectionAlignment
                                ? "direction-only"
                                : "none");
  Log("[P3-WRIST-LANDMARKS] semantic='%s' middle=%d index=%d little=%d "
      "source=Avatar."
      "humanDescription.skeleton readsLivePose=0 generation=%llu "
      "owner=%p tid=%lu",
      kDirectVmdBoneSpecs[semantic].name, hasMiddle ? 1 : 0,
      hasIndex ? 1 : 0, hasLittle ? 1 : 0,
      (unsigned long long)generation,
      reinterpret_cast<void *>(ownerCharacter), GetCurrentThreadId());
  Log("[P3-WRIST-BIND] semantic='%s' mode=%s source=active-preset "
      "sourceForward=(%.6f,%.6f,%.6f) "
      "sourceLateral=(%.6f,%.6f,%.6f) "
      "sourcePalmNormal=(%.6f,%.6f,%.6f) "
      "targetForward=(%.6f,%.6f,%.6f) "
      "targetLateral=(%.6f,%.6f,%.6f) "
      "targetPalmNormal=(%.6f,%.6f,%.6f) "
      "alignmentOwner=(%.7f,%.7f,%.7f,%.7f) "
      "target=Avatar.humanDescription.skeleton fingerWrites=0 "
      "readsLivePose=0 firstFrameCalibration=0 generation=%llu "
      "owner=%p tid=%lu",
      kDirectVmdBoneSpecs[semantic].name, mode,
      output->sourceFrameGame.forward.x,
      output->sourceFrameGame.forward.y,
      output->sourceFrameGame.forward.z,
      output->sourceFrameGame.lateral.x,
      output->sourceFrameGame.lateral.y,
      output->sourceFrameGame.lateral.z,
      output->sourceFrameGame.normal.x,
      output->sourceFrameGame.normal.y,
      output->sourceFrameGame.normal.z,
      output->targetFrameOwner.forward.x,
      output->targetFrameOwner.forward.y,
      output->targetFrameOwner.forward.z,
      output->targetFrameOwner.lateral.x,
      output->targetFrameOwner.lateral.y,
      output->targetFrameOwner.lateral.z,
      output->targetFrameOwner.normal.x,
      output->targetFrameOwner.normal.y,
      output->targetFrameOwner.normal.z,
      output->sourceToTargetOwnerAlignment.x,
      output->sourceToTargetOwnerAlignment.y,
      output->sourceToTargetOwnerAlignment.z,
      output->sourceToTargetOwnerAlignment.w,
      (unsigned long long)generation,
      reinterpret_cast<void *>(ownerCharacter), GetCurrentThreadId());
}

static bool GhostRig_CaptureNaturalBindPose(
    uint64_t generation, uintptr_t ownerCharacter, void *ownerRoot,
    int frame) {
  if (!GhostRig_RequireMainThread("GhostRig.CaptureNaturalBind", false))
    return false;
  s_ghostRig.bindSourceRevision =
      DirectVmdSource_AcquireReference(&s_ghostRig.sourceReference);
  Log("[SOURCE-BIND] revision=%llu generation=%llu owner=%p referenceLeg=%.8f",
      (unsigned long long)s_ghostRig.bindSourceRevision,
      (unsigned long long)generation, reinterpret_cast<void *>(ownerCharacter),
      s_ghostRig.sourceReference.legLength);
  GhostRig_SetBindStatus(GhostBindCaptureState::ReadingAvatarMetadata,
                         "Reading Avatar natural-bind metadata");
  s_ghostRig.bindGeneration = generation;
  s_ghostRig.bindOwnerCharacter = ownerCharacter;
  s_ghostRig.bindAttemptFrame = frame;
  s_ghostRig.leftWristBindFrame = GhostRigHandBindFrame();
  s_ghostRig.rightWristBindFrame = GhostRigHandBindFrame();
  memset(s_ghostRig.twistChannels, 0,
         sizeof(s_ghostRig.twistChannels));
  memset(s_ghostRig.targetNaturalBindAvailable, 0,
         sizeof(s_ghostRig.targetNaturalBindAvailable));

  std::map<std::string, GhostSkeletonBoneRecord> records;
  std::map<std::string, int> nameCounts;
  char status[160] = {};
  if (!GhostRig_ReadSkeletonMetadata(&records, &nameCounts, status,
                                     sizeof(status))) {
    GhostRig_SetBindStatus(GhostBindCaptureState::WaitingForAvatar, status);
    Log("[P2-BIND-WAIT] frame=%d generation=%llu owner=%p reason='%s' "
        "readsCurrentPose=0 tid=%lu",
        frame, (unsigned long long)generation,
        reinterpret_cast<void *>(ownerCharacter), status,
        GetCurrentThreadId());
    return false;
  }

  GhostTargetBindPose target[DIRECT_VMD_BONE_COUNT] = {};
  const auto capture = [&](DirectVmdBoneId id, int humanBone) {
    const uint32_t index = DirectVmdBoneIndex(id);
    return GhostRig_CaptureHumanBindPose(
        humanBone, kDirectVmdBoneSpecs[index].name, ownerRoot, records,
        nameCounts, &target[index], status, sizeof(status));
  };

  GhostTargetBindPose hips;
  if (!GhostRig_CaptureHumanBindPose(
          HB_Hips, u8"下半身/Hips", ownerRoot, records, nameCounts, &hips,
          status, sizeof(status)) ||
      !capture(DirectVmdBoneId::UpperBody, HB_Spine) ||
      !capture(DirectVmdBoneId::LeftLeg, HB_LeftUpperLeg) ||
      !capture(DirectVmdBoneId::LeftKnee, HB_LeftLowerLeg) ||
      !capture(DirectVmdBoneId::LeftAnkle, HB_LeftFoot) ||
      !capture(DirectVmdBoneId::LeftToe, HB_LeftToes) ||
      !capture(DirectVmdBoneId::RightLeg, HB_RightUpperLeg) ||
      !capture(DirectVmdBoneId::RightKnee, HB_RightLowerLeg) ||
      !capture(DirectVmdBoneId::RightAnkle, HB_RightFoot) ||
      !capture(DirectVmdBoneId::RightToe, HB_RightToes)) {
    GhostRig_SetBindStatus(GhostBindCaptureState::Blocked, status);
    Log("[P2-BIND-BLOCKED] frame=%d generation=%llu owner=%p "
        "reason='%s' fallbackToCurrentPose=0 tid=%lu",
        frame, (unsigned long long)generation,
        reinterpret_cast<void *>(ownerCharacter), status,
        GetCurrentThreadId());
    return false;
  }

  void *chestTransform = SafeGetBoneTransform(HB_Chest);
  int upperBody2HumanBone = HB_Chest;
  if (!chestTransform) {
    chestTransform = SafeGetBoneTransform(HB_UpperChest);
    upperBody2HumanBone = HB_UpperChest;
  }
  if (chestTransform) {
    char transformName[512] = {};
    if (!GhostRig_BuildBindPoseFromTransform(
            chestTransform, ownerRoot, records, nameCounts,
            &target[DirectVmdBoneIndex(DirectVmdBoneId::UpperBody2)],
            transformName, sizeof(transformName), status,
            sizeof(status))) {
      GhostRig_SetBindStatus(GhostBindCaptureState::Blocked, status);
      Log("[P2-BIND-BLOCKED] frame=%d generation=%llu owner=%p "
          "reason='%s' fallbackToCurrentPose=0 tid=%lu",
          frame, (unsigned long long)generation,
          reinterpret_cast<void *>(ownerCharacter), status,
          GetCurrentThreadId());
      return false;
    }
    Log("[P2-BIND-MAP] semantic='%s' humanBone=%d transform='%s' "
        "source=Avatar.humanDescription.skeleton",
        kDirectVmdBoneSpecs[DirectVmdBoneIndex(
            DirectVmdBoneId::UpperBody2)].name,
        upperBody2HumanBone, transformName);
  } else {
    target[DirectVmdBoneIndex(DirectVmdBoneId::UpperBody2)] =
        target[DirectVmdBoneIndex(DirectVmdBoneId::UpperBody)];
    Log("[P2-BIND-MAP] semantic='%s' source=explicit-spine-fallback "
        "reason=no-Chest-or-UpperChest",
        kDirectVmdBoneSpecs[DirectVmdBoneIndex(
            DirectVmdBoneId::UpperBody2)].name);
  }

  const auto capturePhase3Optional = [&](DirectVmdBoneId id, int humanBone,
                                         DirectVmdBoneId parentId) {
    const uint32_t index = DirectVmdBoneIndex(id);
    if (!SafeGetBoneTransform(humanBone)) {
      target[index] = target[DirectVmdBoneIndex(parentId)];
      target[index].valid = true;
      Log("[P3-BIND-MISSING] semantic='%s' humanBone=%d "
          "fallback=parent-natural-bind parent='%s' readsLivePose=0 "
          "action=target-will-skip",
          kDirectVmdBoneSpecs[index].name, humanBone,
          kDirectVmdBoneSpecs[DirectVmdBoneIndex(parentId)].name);
      return true;
    }
    return capture(id, humanBone);
  };
  if (!capturePhase3Optional(DirectVmdBoneId::Neck, HB_Neck,
                             DirectVmdBoneId::UpperBody2) ||
      !capturePhase3Optional(DirectVmdBoneId::Head, HB_Head,
                             DirectVmdBoneId::Neck) ||
      !capturePhase3Optional(DirectVmdBoneId::LeftShoulder,
                             HB_LeftShoulder,
                             DirectVmdBoneId::UpperBody2) ||
      !capturePhase3Optional(DirectVmdBoneId::LeftArm, HB_LeftUpperArm,
                             DirectVmdBoneId::LeftShoulder) ||
      !capturePhase3Optional(DirectVmdBoneId::LeftElbow, HB_LeftLowerArm,
                             DirectVmdBoneId::LeftArm) ||
      !capturePhase3Optional(DirectVmdBoneId::LeftWrist, HB_LeftHand,
                             DirectVmdBoneId::LeftElbow) ||
      !capturePhase3Optional(DirectVmdBoneId::RightShoulder,
                             HB_RightShoulder,
                             DirectVmdBoneId::UpperBody2) ||
      !capturePhase3Optional(DirectVmdBoneId::RightArm, HB_RightUpperArm,
                             DirectVmdBoneId::RightShoulder) ||
      !capturePhase3Optional(DirectVmdBoneId::RightElbow, HB_RightLowerArm,
                             DirectVmdBoneId::RightArm) ||
      !capturePhase3Optional(DirectVmdBoneId::RightWrist, HB_RightHand,
                             DirectVmdBoneId::RightElbow)) {
    GhostRig_SetBindStatus(GhostBindCaptureState::Blocked, status);
    Log("[P3-BIND-BLOCKED] frame=%d generation=%llu owner=%p "
        "reason='%s' fallbackToCurrentPose=0 tid=%lu",
        frame, (unsigned long long)generation,
        reinterpret_cast<void *>(ownerCharacter), status,
        GetCurrentThreadId());
      return false;
  }

  for (uint32_t order = 0; order < DIRECT_VMD_PHASE6_FINGER_BONE_COUNT;
       ++order) {
    const DirectVmdBoneId id = kDirectVmdPhase6FingerBones[order];
    const uint32_t semantic = DirectVmdBoneIndex(id);
    const int parentSemantic = kDirectVmdBoneSpecs[semantic].parent;
    const bool fingerParentMissing =
        parentSemantic >= static_cast<int>(DirectVmdBoneId::LeftThumb0) &&
        !s_ghostRig.targetNaturalBindAvailable[
            static_cast<uint32_t>(parentSemantic)];
    const char *transformName =
        LookupFingerMapping(kDirectVmdBoneSpecs[semantic].name);
    char fingerStatus[192] = {};
    if (!fingerParentMissing && transformName &&
        GhostRig_CaptureNamedBindPose(
            transformName, kDirectVmdBoneSpecs[semantic].name, ownerRoot,
            records, nameCounts, &target[semantic], fingerStatus,
            sizeof(fingerStatus))) {
      s_ghostRig.targetNaturalBindAvailable[semantic] = true;
      continue;
    }

    const uint32_t fallbackSemantic =
        parentSemantic >= 0 ? static_cast<uint32_t>(parentSemantic)
                            : DirectVmdBoneIndex(DirectVmdBoneId::LeftWrist);
    target[semantic] = target[fallbackSemantic];
    target[semantic].valid = true;
    Log("[P6-FINGER-BIND-MISSING] semantic='%s' transform='%s' "
        "reason='%s' parentAvailable=%d fallback=parent-natural-bind "
        "readsLivePose=0 action=target-will-skip generation=%llu owner=%p "
        "tid=%lu",
        kDirectVmdBoneSpecs[semantic].name,
        transformName ? transformName : "unmapped",
        fingerParentMissing ? "parent finger bind unavailable"
                            : (fingerStatus[0] ? fingerStatus
                                               : "mapping unavailable"),
        fingerParentMissing ? 0 : 1,
        (unsigned long long)generation,
        reinterpret_cast<void *>(ownerCharacter), GetCurrentThreadId());
  }

  GhostRig_CaptureWristBindFrame(
      DirectVmdBoneId::LeftWrist,
      target[DirectVmdBoneIndex(DirectVmdBoneId::LeftWrist)],
      target[DirectVmdBoneIndex(DirectVmdBoneId::LeftMiddle1)],
      s_ghostRig.targetNaturalBindAvailable[
          DirectVmdBoneIndex(DirectVmdBoneId::LeftMiddle1)],
      target[DirectVmdBoneIndex(DirectVmdBoneId::LeftIndex1)],
      s_ghostRig.targetNaturalBindAvailable[
          DirectVmdBoneIndex(DirectVmdBoneId::LeftIndex1)],
      target[DirectVmdBoneIndex(DirectVmdBoneId::LeftLittle1)],
      s_ghostRig.targetNaturalBindAvailable[
          DirectVmdBoneIndex(DirectVmdBoneId::LeftLittle1)],
      generation, ownerCharacter, &s_ghostRig.leftWristBindFrame);
  GhostRig_CaptureWristBindFrame(
      DirectVmdBoneId::RightWrist,
      target[DirectVmdBoneIndex(DirectVmdBoneId::RightWrist)],
      target[DirectVmdBoneIndex(DirectVmdBoneId::RightMiddle1)],
      s_ghostRig.targetNaturalBindAvailable[
          DirectVmdBoneIndex(DirectVmdBoneId::RightMiddle1)],
      target[DirectVmdBoneIndex(DirectVmdBoneId::RightIndex1)],
      s_ghostRig.targetNaturalBindAvailable[
          DirectVmdBoneIndex(DirectVmdBoneId::RightIndex1)],
      target[DirectVmdBoneIndex(DirectVmdBoneId::RightLittle1)],
      s_ghostRig.targetNaturalBindAvailable[
          DirectVmdBoneIndex(DirectVmdBoneId::RightLittle1)],
      generation, ownerCharacter, &s_ghostRig.rightWristBindFrame);

  const uint32_t bothEyesIndex =
      DirectVmdBoneIndex(DirectVmdBoneId::BothEyes);
  const uint32_t leftEyeIndex =
      DirectVmdBoneIndex(DirectVmdBoneId::LeftEye);
  const uint32_t rightEyeIndex =
      DirectVmdBoneIndex(DirectVmdBoneId::RightEye);
  const char *leftEyeTransformName =
      LookupFingerMapping(kDirectVmdBoneSpecs[leftEyeIndex].name);
  const char *rightEyeTransformName =
      LookupFingerMapping(kDirectVmdBoneSpecs[rightEyeIndex].name);
  char leftEyeStatus[192] = {};
  char rightEyeStatus[192] = {};
  const bool hasLeftEye = leftEyeTransformName &&
      GhostRig_CaptureNamedBindPose(
          leftEyeTransformName, kDirectVmdBoneSpecs[leftEyeIndex].name,
          ownerRoot, records, nameCounts, &target[leftEyeIndex],
          leftEyeStatus, sizeof(leftEyeStatus));
  const bool hasRightEye = rightEyeTransformName &&
      GhostRig_CaptureNamedBindPose(
          rightEyeTransformName, kDirectVmdBoneSpecs[rightEyeIndex].name,
          ownerRoot, records, nameCounts, &target[rightEyeIndex],
          rightEyeStatus, sizeof(rightEyeStatus));

  GhostTargetBindPose bothEyes =
      target[DirectVmdBoneIndex(DirectVmdBoneId::Head)];
  if (hasLeftEye && hasRightEye) {
    bothEyes.position = DirectVmdScale(
        DirectVmdAdd(target[leftEyeIndex].position,
                     target[rightEyeIndex].position),
        0.5f);
  }
  bothEyes.valid = true;
  target[bothEyesIndex] = bothEyes;
  s_ghostRig.targetNaturalBindAvailable[leftEyeIndex] = hasLeftEye;
  s_ghostRig.targetNaturalBindAvailable[rightEyeIndex] = hasRightEye;
  if (!hasLeftEye) {
    target[leftEyeIndex] = bothEyes;
    Log("[P6-EYE-BIND-MISSING] semantic='%s' transform='%s' reason='%s' "
        "fallback=both-eyes-natural-bind readsLivePose=0 action=skip-target "
        "generation=%llu owner=%p tid=%lu",
        kDirectVmdBoneSpecs[leftEyeIndex].name,
        leftEyeTransformName ? leftEyeTransformName : "unmapped",
        leftEyeStatus[0] ? leftEyeStatus : "mapping unavailable",
        (unsigned long long)generation,
        reinterpret_cast<void *>(ownerCharacter), GetCurrentThreadId());
  }
  if (!hasRightEye) {
    target[rightEyeIndex] = bothEyes;
    Log("[P6-EYE-BIND-MISSING] semantic='%s' transform='%s' reason='%s' "
        "fallback=both-eyes-natural-bind readsLivePose=0 action=skip-target "
        "generation=%llu owner=%p tid=%lu",
        kDirectVmdBoneSpecs[rightEyeIndex].name,
        rightEyeTransformName ? rightEyeTransformName : "unmapped",
        rightEyeStatus[0] ? rightEyeStatus : "mapping unavailable",
        (unsigned long long)generation,
        reinterpret_cast<void *>(ownerCharacter), GetCurrentThreadId());
  }
  Log("[P6-EYE-BIND] bothEyesSynthetic=1 left=%d right=%d "
      "position=(%.6f,%.6f,%.6f) rotationSource=head-natural-bind "
      "readsLivePose=0 generation=%llu owner=%p tid=%lu",
      hasLeftEye ? 1 : 0, hasRightEye ? 1 : 0,
      bothEyes.position.x, bothEyes.position.y, bothEyes.position.z,
      (unsigned long long)generation,
      reinterpret_cast<void *>(ownerCharacter), GetCurrentThreadId());

  for (uint32_t channel = 0;
       channel < DIRECT_VMD_PHASE6_TWIST_CHANNEL_COUNT; ++channel) {
    const DirectVmdTwistChannelSpec &spec =
        kDirectVmdPhase6TwistChannels[channel];
    const uint32_t controlSemantic = DirectVmdBoneIndex(spec.control);
    const GhostTargetBindPose &limbStart =
        target[DirectVmdBoneIndex(spec.sourceLimb)];
    const GhostTargetBindPose &limbEnd =
        target[DirectVmdBoneIndex(spec.targetLimbEnd)];
    target[controlSemantic] = limbStart;
    target[controlSemantic].valid = true;

    VmdVec3 limbDirectionOwner = {};
    const bool hasLimbDirection = DirectVmdTryNormalizeVector(
        DirectVmdSub(limbEnd.position, limbStart.position),
        &limbDirectionOwner);
    GhostTargetBindPose previous = limbStart;
    bool parentAvailable = hasLimbDirection;
    char twistNames[DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL][96] = {};
    const char *twistSource = "unresolved";
    char twistReason[192] = {};
    const bool twistNamesResolved = GhostRig_ResolveTwistTransformNames(
        channel, ownerRoot, records, nameCounts, twistNames, &twistSource,
        twistReason, sizeof(twistReason));
    Log("[P6-TWIST-DISCOVERY] channel=%u semantic='%s' resolved=%d "
        "source=%s target0='%s' target1='%s' reason='%s' readsLivePose=0 "
        "generation=%llu owner=%p tid=%lu",
        channel, kDirectVmdBoneSpecs[controlSemantic].name,
        twistNamesResolved ? 1 : 0, twistSource, twistNames[0],
        twistNames[1], twistReason, (unsigned long long)generation,
        reinterpret_cast<void *>(ownerCharacter), GetCurrentThreadId());
    for (uint32_t targetIndex = 0;
         targetIndex < DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL;
         ++targetIndex) {
      GhostRigTwistTargetRuntime &runtime =
          s_ghostRig.twistChannels[channel].targets[targetIndex];
      strncpy_s(runtime.transformName, twistNames[targetIndex], _TRUNCATE);
      const char *transformName =
          twistNamesResolved ? runtime.transformName : nullptr;
      GhostTargetBindPose twistPose;
      char twistStatus[192] = {};
      const bool captured = parentAvailable && transformName &&
          GhostRig_CaptureNamedBindPose(
              transformName, kDirectVmdBoneSpecs[controlSemantic].name,
              ownerRoot, records, nameCounts, &twistPose, twistStatus,
              sizeof(twistStatus));
      VmdVec3 axisParentLocal = {};
      if (captured) {
        const VmdQuaternion parentInverse =
            DirectVmdQuaternionInverse(previous.rotation);
        runtime.bindLocalRotation = DirectVmdQuaternionMultiply(
            parentInverse, twistPose.rotation);
        runtime.axisParentLocal = DirectVmdRotateVector(
            parentInverse, limbDirectionOwner);
        runtime.bindAvailable = DirectVmdTryNormalizeVector(
            runtime.axisParentLocal, &axisParentLocal);
        if (runtime.bindAvailable)
          runtime.axisParentLocal = axisParentLocal;
      }
      if (!captured || !runtime.bindAvailable) {
        runtime.bindAvailable = false;
        parentAvailable = false;
        Log("[P6-TWIST-BIND-MISSING] channel=%u semantic='%s' "
            "targetIndex=%u transform='%s' reason='%s' "
            "parentAvailable=%d readsLivePose=0 action=skip-channel "
            "generation=%llu owner=%p tid=%lu",
            channel, kDirectVmdBoneSpecs[controlSemantic].name,
            targetIndex, transformName ? transformName : "unmapped",
            twistStatus[0] ? twistStatus
                           : (hasLimbDirection ? "capture unavailable"
                                               : "invalid limb direction"),
            parentAvailable ? 1 : 0,
            (unsigned long long)generation,
            reinterpret_cast<void *>(ownerCharacter),
            GetCurrentThreadId());
        continue;
      }
      previous = twistPose;
      Log("[P6-TWIST-BIND] channel=%u semantic='%s' targetIndex=%u "
          "transform='%s' bindLocalR=(%.7f,%.7f,%.7f,%.7f) "
          "axisParentLocal=(%.6f,%.6f,%.6f) "
          "source=Avatar.humanDescription.skeleton readsLivePose=0 "
          "generation=%llu owner=%p tid=%lu",
          channel, kDirectVmdBoneSpecs[controlSemantic].name, targetIndex,
          transformName, runtime.bindLocalRotation.x,
          runtime.bindLocalRotation.y, runtime.bindLocalRotation.z,
          runtime.bindLocalRotation.w, runtime.axisParentLocal.x,
          runtime.axisParentLocal.y, runtime.axisParentLocal.z,
          (unsigned long long)generation,
          reinterpret_cast<void *>(ownerCharacter), GetCurrentThreadId());
    }
  }

  const bool naturalHeadMapped = SafeGetBoneTransform(HB_Head) != nullptr;
  const GhostTargetBindPose &naturalHead =
      target[DirectVmdBoneIndex(DirectVmdBoneId::Head)];
  const float naturalBindHeight = naturalHead.position.y;
  s_ghostRig.targetNaturalBindHeight =
      naturalHeadMapped && DirectVmdFinite(naturalBindHeight) &&
              naturalBindHeight > 0.1f && naturalBindHeight < 5.0f
          ? naturalBindHeight
          : 0.0f;
  Log("[P6-CAMERA-BIND] generation=%llu owner=%p "
      "naturalBindHeight=%.6f available=%d "
      "source=Avatar.humanDescription.skeleton readsCurrentPose=0 "
      "firstFrameCalibration=0 policy=legacy-override-only tid=%lu",
      (unsigned long long)generation,
      reinterpret_cast<void *>(ownerCharacter),
      s_ghostRig.targetNaturalBindHeight,
      s_ghostRig.targetNaturalBindHeight > 0.0f ? 1 : 0,
      GetCurrentThreadId());

  const auto &leftLeg =
      target[DirectVmdBoneIndex(DirectVmdBoneId::LeftLeg)];
  const auto &leftKnee =
      target[DirectVmdBoneIndex(DirectVmdBoneId::LeftKnee)];
  const auto &leftAnkle =
      target[DirectVmdBoneIndex(DirectVmdBoneId::LeftAnkle)];
  const auto &rightLeg =
      target[DirectVmdBoneIndex(DirectVmdBoneId::RightLeg)];
  const auto &rightKnee =
      target[DirectVmdBoneIndex(DirectVmdBoneId::RightKnee)];
  const auto &rightAnkle =
      target[DirectVmdBoneIndex(DirectVmdBoneId::RightAnkle)];
  s_ghostRig.leftLegLength =
      DirectVmdLength(DirectVmdSub(leftKnee.position, leftLeg.position)) +
      DirectVmdLength(DirectVmdSub(leftAnkle.position, leftKnee.position));
  s_ghostRig.rightLegLength =
      DirectVmdLength(DirectVmdSub(rightKnee.position, rightLeg.position)) +
      DirectVmdLength(DirectVmdSub(rightAnkle.position, rightKnee.position));
  s_ghostRig.targetLegLength =
      0.5f * (s_ghostRig.leftLegLength + s_ghostRig.rightLegLength);
  if (!DirectVmdFinite(s_ghostRig.targetLegLength) ||
      s_ghostRig.targetLegLength < 0.05f ||
      s_ghostRig.targetLegLength > 10.0f) {
    GhostRig_SetBindStatus(GhostBindCaptureState::Blocked,
                           "Target natural-bind leg length is invalid");
    Log("[P2-BIND-BLOCKED] generation=%llu owner=%p leftLeg=%.6f "
        "rightLeg=%.6f reason=invalid-leg-length",
        (unsigned long long)generation,
        reinterpret_cast<void *>(ownerCharacter),
        s_ghostRig.leftLegLength, s_ghostRig.rightLegLength);
    return false;
  }

  const auto deriveAnkleClearance = [](
      const GhostTargetBindPose &ankle,
      const GhostTargetBindPose &toe, float legLength) {
    const float minimum =
        DirectVmdTerrainClamp(legLength * 0.015f, 0.010f, 0.035f);
    const float maximum =
        DirectVmdTerrainClamp(legLength * 0.250f, 0.080f, 0.240f);
    float clearance = ankle.position.y;
    if (!DirectVmdFinite(clearance) || clearance < minimum ||
        clearance > maximum) {
      clearance = std::fabs(ankle.position.y - toe.position.y) +
                  legLength * 0.025f;
    }
    return DirectVmdTerrainClamp(clearance, minimum, maximum);
  };
  s_ghostRig.terrainAnkleClearance[0] = deriveAnkleClearance(
      leftAnkle,
      target[DirectVmdBoneIndex(DirectVmdBoneId::LeftToe)],
      s_ghostRig.leftLegLength);
  s_ghostRig.terrainAnkleClearance[1] = deriveAnkleClearance(
      rightAnkle,
      target[DirectVmdBoneIndex(DirectVmdBoneId::RightToe)],
      s_ghostRig.rightLegLength);
  s_ghostRig.terrainConfig =
      DirectVmdBuildTerrainConfig(s_ghostRig.targetLegLength);
  Log("[P7-TERRAIN-BIND] generation=%llu owner=%p "
      "source=Avatar.humanDescription.skeleton firstFrameCalibration=0 "
      "leftAnkleOwnerY=%.6f leftToeOwnerY=%.6f leftClearance=%.6f "
      "rightAnkleOwnerY=%.6f rightToeOwnerY=%.6f rightClearance=%.6f "
      "legLength=%.6f contactEnter=%.6f contactExit=%.6f "
      "clusterHeight=%.6f footTau=%.3f rootTau=%.3f "
      "grounderBand=%.6f grounderRiseConfirm=%.3f "
      "grounderDropConfirm=%.3f grounderFootTau=%.3f "
      "grounderRootTau=%.3f grounderFootMaxSpeed=%.3f "
      "grounderRootMaxSpeed=%.3f reachReleaseConfirm=%.3f "
      "reachReleaseDeadband=%.6f contactEdgeRelease=%.6f "
      "contactEdgeReleasePolicy=spatial-only tid=%lu",
      (unsigned long long)generation,
      reinterpret_cast<void *>(ownerCharacter), leftAnkle.position.y,
      target[DirectVmdBoneIndex(DirectVmdBoneId::LeftToe)].position.y,
      s_ghostRig.terrainAnkleClearance[0], rightAnkle.position.y,
      target[DirectVmdBoneIndex(DirectVmdBoneId::RightToe)].position.y,
      s_ghostRig.terrainAnkleClearance[1], s_ghostRig.targetLegLength,
      s_ghostRig.terrainConfig.contactEnterHeight,
      s_ghostRig.terrainConfig.contactExitHeight,
      s_ghostRig.terrainConfig.planeClusterHeight,
      s_ghostRig.terrainConfig.footHeightTimeConstant,
      s_ghostRig.terrainConfig.rootHeightTimeConstant,
      s_ghostRig.terrainConfig.grounderContinuousHeightBand,
      s_ghostRig.terrainConfig.grounderRiseConfirmSeconds,
      s_ghostRig.terrainConfig.grounderDropConfirmSeconds,
      s_ghostRig.terrainConfig.grounderFootHeightTimeConstant,
      s_ghostRig.terrainConfig.grounderRootHeightTimeConstant,
      s_ghostRig.terrainConfig.grounderMaximumFootHeightSpeed,
      s_ghostRig.terrainConfig.grounderMaximumRootHeightSpeed,
      s_ghostRig.terrainConfig.grounderRootReachReleaseSeconds,
      s_ghostRig.terrainConfig.grounderRootReachReleaseDeadband,
      s_ghostRig.terrainConfig.grounderContactEdgeReleaseDistance,
      GetCurrentThreadId());

  const float kReferenceMmdLegLength = s_ghostRig.sourceReference.legLength;
  const VmdVec3 kReferenceLowerFromCenter = s_ghostRig.sourceReference.lowerFromCenter;
  const VmdVec3 kReferenceGrooveFromCenter = s_ghostRig.sourceReference.grooveFromCenter;
  s_ghostRig.baseMotionScale =
      s_ghostRig.targetLegLength / kReferenceMmdLegLength;

  const GhostTargetBindPose identity = {
      {0.0f, 0.0f, 0.0f}, {0.0f, 0.0f, 0.0f, 1.0f},
      {1.0f, 1.0f, 1.0f}, true};
  target[DirectVmdBoneIndex(DirectVmdBoneId::AllParent)] = identity;
  GhostTargetBindPose center = identity;
  center.position = DirectVmdSub(
      hips.position,
      DirectVmdScale(
          SourceToGameBasis::ConvertPosition(kReferenceLowerFromCenter),
          s_ghostRig.baseMotionScale));
  target[DirectVmdBoneIndex(DirectVmdBoneId::Center)] = center;
  GhostTargetBindPose groove = center;
  groove.position = DirectVmdAdd(
      center.position,
      DirectVmdScale(
          SourceToGameBasis::ConvertPosition(kReferenceGrooveFromCenter),
          s_ghostRig.baseMotionScale));
  target[DirectVmdBoneIndex(DirectVmdBoneId::Groove)] = groove;
  target[DirectVmdBoneIndex(DirectVmdBoneId::LowerBody)] = hips;
  Log("[P2-BIND-SYNTHETIC] center=(%.6f,%.6f,%.6f) "
      "groove=(%.6f,%.6f,%.6f) lowerBody=(%.6f,%.6f,%.6f) "
      "baseMotionScale=%.8f source=active-preset",
      center.position.x, center.position.y, center.position.z,
      groove.position.x, groove.position.y, groove.position.z,
      hips.position.x, hips.position.y, hips.position.z,
      s_ghostRig.baseMotionScale);

  auto leftToe =
      target[DirectVmdBoneIndex(DirectVmdBoneId::LeftToe)];
  auto rightToe =
      target[DirectVmdBoneIndex(DirectVmdBoneId::RightToe)];
  auto leftFoot = leftAnkle;
  auto rightFoot = rightAnkle;
  const auto addSourceOffset = [&](GhostTargetBindPose &pose, VmdVec3 offset) {
    pose.position = DirectVmdAdd(pose.position, DirectVmdScale(
        SourceToGameBasis::ConvertPosition(offset), s_ghostRig.baseMotionScale));
  };
  addSourceOffset(leftFoot, s_ghostRig.sourceReference.footFromAnkle[0]);
  addSourceOffset(rightFoot, s_ghostRig.sourceReference.footFromAnkle[1]);
  addSourceOffset(leftToe, s_ghostRig.sourceReference.toeFromToe[0]);
  addSourceOffset(rightToe, s_ghostRig.sourceReference.toeFromToe[1]);
  GhostTargetBindPose leftIkParent = identity;
  const VmdVec3 footIkParentOffset = DirectVmdScale(
      SourceToGameBasis::ConvertPosition(s_ghostRig.sourceReference.footFromParent[0]),
      s_ghostRig.baseMotionScale);
  leftIkParent.position =
      DirectVmdSub(leftFoot.position, footIkParentOffset);
  GhostTargetBindPose rightIkParent = identity;
  rightIkParent.position =
      DirectVmdSub(rightFoot.position, DirectVmdScale(
          SourceToGameBasis::ConvertPosition(s_ghostRig.sourceReference.footFromParent[1]),
          s_ghostRig.baseMotionScale));
  target[DirectVmdBoneIndex(DirectVmdBoneId::LeftFootIkParent)] =
      leftIkParent;
  target[DirectVmdBoneIndex(DirectVmdBoneId::LeftFootIk)] = leftFoot;
  target[DirectVmdBoneIndex(DirectVmdBoneId::LeftToeIk)] = leftToe;
  target[DirectVmdBoneIndex(DirectVmdBoneId::RightFootIkParent)] =
      rightIkParent;
  target[DirectVmdBoneIndex(DirectVmdBoneId::RightFootIk)] = rightFoot;
  target[DirectVmdBoneIndex(DirectVmdBoneId::RightToeIk)] = rightToe;

  s_ghostRig.nodes[0].bind = DirectVmdBindNodePod();
  s_ghostRig.nodes[0].bind.localRotation = {0, 0, 0, 1};
  s_ghostRig.nodes[0].bind.worldRotation = {0, 0, 0, 1};
  for (uint32_t semantic = 0; semantic < DIRECT_VMD_BONE_COUNT;
       ++semantic) {
    if (!target[semantic].valid) {
      _snprintf_s(status, sizeof(status), _TRUNCATE,
                  "Internal semantic bind missing '%s'",
                  kDirectVmdBoneSpecs[semantic].name);
      GhostRig_SetBindStatus(GhostBindCaptureState::Blocked, status);
      return false;
    }
    const int nodeIndex = 1 + static_cast<int>(semantic);
    const int parentIndex = GhostRig_NodeParent(nodeIndex);
    const GhostTargetBindPose parent =
        parentIndex == 0
            ? identity
            : target[static_cast<uint32_t>(parentIndex - 1)];
    const VmdQuaternion parentInverse =
        DirectVmdQuaternionInverse(parent.rotation);
    DirectVmdBindNodePod &bind = s_ghostRig.nodes[nodeIndex].bind;
    bind.localPosition = DirectVmdRotateVector(
        parentInverse,
        DirectVmdSub(target[semantic].position, parent.position));
    bind.localRotation = DirectVmdQuaternionMultiply(
        parentInverse, target[semantic].rotation);
    bind.worldPosition = target[semantic].position;
    bind.worldRotation = target[semantic].rotation;
  }

  s_ghostRig.motionScale = s_ghostRig.baseMotionScale *
      DirectVmdRuntime_GetMotionMultiplier();
  s_ghostMotionScalePublic.store(s_ghostRig.motionScale,
                                 std::memory_order_release);

  s_ghostRig.bindGeneration = generation;
  s_ghostRig.bindOwnerCharacter = ownerCharacter;
  GhostRig_SetBindStatus(GhostBindCaptureState::Ready,
                         "Avatar natural bind captured");
  Log("[P2-MOTION-SCALE] generation=%llu owner=%p source=active-preset "
      "referenceLeg=%.8f targetLeft=%.6f targetRight=%.6f "
      "targetAverage=%.6f userMultiplier=%.6f motionScale=%.8f",
      (unsigned long long)generation,
      reinterpret_cast<void *>(ownerCharacter), kReferenceMmdLegLength,
      s_ghostRig.leftLegLength, s_ghostRig.rightLegLength,
      s_ghostRig.targetLegLength,
      DirectVmdRuntime_GetMotionMultiplier(), s_ghostRig.motionScale);
  Log("[P2-BIND-READY] frame=%d generation=%llu owner=%p nodes=%d "
      "source=Avatar.humanDescription.skeleton readsCurrentPose=0 "
      "firstVmdFrameCalibration=0 tid=%lu",
      frame, (unsigned long long)generation,
      reinterpret_cast<void *>(ownerCharacter), GHOST_NODE_COUNT,
      GetCurrentThreadId());
  return true;
}

static bool GhostRig_ApplyDirectPose(void *ownerRoot) {
  if (s_ghostRig.state != GhostRigState::Alive || !ownerRoot ||
      s_ghostRig.bindState != GhostBindCaptureState::Ready)
    return false;
  if (!GhostRig_AlignRoot(ownerRoot))
    return false;

  DirectVmdSampleFrame frame;
  const bool copied = DirectVmdRuntime_CopyLatestFrame(&frame);
  const bool accepted =
      copied && frame.valid &&
      frame.rigGeneration == s_ghostRig.generation &&
      frame.sourceRevision == s_ghostRig.bindSourceRevision &&
      frame.ownerCharacter == s_ghostRig.ownerCharacter;
  for (int side = 0; side < 2; ++side)
    s_ghostRig.legs[side].sourceKnee = accepted
        ? frame.knees[side] : DirectVmdKneeReferencePod{};
  s_ghostRig.motionScale = s_ghostRig.baseMotionScale *
      DirectVmdRuntime_GetMotionMultiplier();
  s_ghostMotionScalePublic.store(s_ghostRig.motionScale,
                                 std::memory_order_release);

  for (int nodeIndex = 1; nodeIndex < GHOST_NODE_COUNT; ++nodeIndex) {
    void *transform = GhostRig_GetTransform(nodeIndex);
    if (!transform)
      return false;
    const int parentIndex = GhostRig_NodeParent(nodeIndex);
    const VmdQuaternion parentBindWorldRotation =
        parentIndex <= 0
            ? VmdQuaternion{0, 0, 0, 1}
            : s_ghostRig.nodes[parentIndex].bind.worldRotation;
    const DirectVmdBoneSamplePod *sample =
        accepted ? &frame.bones[nodeIndex - 1] : nullptr;
    const DirectVmdLocalPosePod pose = DirectVmdEvaluateLocalPose(
        s_ghostRig.nodes[nodeIndex].bind, parentBindWorldRotation, sample,
        s_ghostRig.motionScale);
    SafeSetLocalPosition(transform, GhostRig_ToVec3(pose.position));
    SafeSetLocalRotation(transform, GhostRig_ToQuat(pose.rotation));
  }

  if (accepted) {
    const DirectVmdPlaybackState previousPlayback =
        s_ghostRig.lastSamplePlayback;
    const bool freshPlaybackStart = DirectVmdStartsFreshPlayback(
        previousPlayback, frame.playback);
    if (freshPlaybackStart && s_ghostRig.playbackAnchorCaptured) {
      s_ghostRig.playbackAnchorCaptured = false;
      s_ghostRig.rootPlacementValid = false;
      s_ghostRig.rootPlacementOffset = {0.0f, 0.0f, 0.0f};
      s_ghostRig.lastDesiredRootValid = false;
      s_ghostRig.rootCycleSeen = false;
      Log("[P4-ROOT-PLACEMENT] event=rearm previousPlayback=%u "
          "vmdFrame=%.6f cycle=%llu generation=%llu owner=%p "
          "reason=fresh-play-not-pause-resume tid=%lu",
          static_cast<unsigned>(previousPlayback), frame.sourceFrame,
          (unsigned long long)frame.playbackCycle,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    }
    ++s_ghostRig.cadenceUnityFrames;
    if (s_ghostRig.lastSampleSequence != 0 &&
        frame.sequence == s_ghostRig.lastSampleSequence) {
      ++s_ghostRig.cadenceRepeatedSamples;
    } else {
      ++s_ghostRig.cadenceSampleAdvances;
      if (s_ghostRig.lastSampleSequence != 0) {
        const double sourceStep =
            std::fabs(frame.sourceFrame - s_ghostRig.lastSourceFrame);
        s_ghostRig.cadenceMaxSourceFrameStep =
            (std::max)(s_ghostRig.cadenceMaxSourceFrameStep, sourceStep);
      }
    }
    s_ghostRig.lastSampleSequence = frame.sequence;
    s_ghostRig.lastPlaybackCycle = frame.playbackCycle;
    s_ghostRig.lastSourceFrame = frame.sourceFrame;
    s_ghostRig.lastSamplePlayback = frame.playback;
    s_ghostRig.lastLeftFootIkEnabled = frame.leftFootIkEnabled != 0;
    s_ghostRig.lastRightFootIkEnabled = frame.rightFootIkEnabled != 0;
    s_ghostRig.lastSampleAccepted = true;
  } else {
    s_ghostRig.lastSampleSequence = 0;
    s_ghostRig.lastPlaybackCycle = 0;
    s_ghostRig.lastSourceFrame = 0.0;
    s_ghostRig.lastSamplePlayback = DirectVmdPlaybackState::Stopped;
    s_ghostRig.lastLeftFootIkEnabled = true;
    s_ghostRig.lastRightFootIkEnabled = true;
    s_ghostRig.lastSampleAccepted = false;
  }
  return true;
}

static bool GhostRig_CapturePlaybackAnchor(void *ownerRoot) {
  if (!GhostRig_RequireMainThread("GhostRig.CapturePlaybackAnchor", false) ||
      !ownerRoot || !s_ghostRig.lastSampleAccepted ||
      s_ghostRig.lastSamplePlayback != DirectVmdPlaybackState::Playing)
    return false;

  Vec3 position = {};
  Quat rotation = {0.0f, 0.0f, 0.0f, 1.0f};
  if (!GhostRig_ReadAnchorPose(ownerRoot, position, rotation) ||
      !DirectVmdFinite(position.x) || !DirectVmdFinite(position.y) ||
      !DirectVmdFinite(position.z) || !DirectVmdFinite(rotation.x) ||
      !DirectVmdFinite(rotation.y) || !DirectVmdFinite(rotation.z) ||
      !DirectVmdFinite(rotation.w))
    return false;

  s_ghostRig.anchorPosition = {position.x, position.y, position.z};
  s_ghostRig.anchorRotation = DirectVmdNormalizeQuaternion(
      {rotation.x, rotation.y, rotation.z, rotation.w});
  s_ghostRig.anchorValid = true;
  if (!s_ghostRig.restoreAnchorValid) {
    s_ghostRig.restoreAnchorPosition = s_ghostRig.anchorPosition;
    s_ghostRig.restoreAnchorRotation = s_ghostRig.anchorRotation;
    s_ghostRig.restoreAnchorValid = true;
  }
  s_ghostRig.rootPlacementOffset = {0.0f, 0.0f, 0.0f};
  s_ghostRig.rootPlacementValid = false;
  s_ghostRig.rootCycleSeen = false;
  if (!GhostRig_AlignRoot(ownerRoot)) {
    return false;
  }

  Log("[P4-ROOT-ANCHOR] captured=1 vmdFrame=%.6f cycle=%llu "
      "playback=%u anchorP=(%.6f,%.6f,%.6f) "
      "anchorR=(%.7f,%.7f,%.7f,%.7f) generation=%llu owner=%p "
      "root=%p restoreAnchorCaptured=%d placementPending=1 "
      "tid=%lu firstKeySubtraction=0 currentPoseFeedback=0",
      s_ghostRig.lastSourceFrame,
      (unsigned long long)s_ghostRig.lastPlaybackCycle,
      static_cast<unsigned>(s_ghostRig.lastSamplePlayback),
      s_ghostRig.anchorPosition.x, s_ghostRig.anchorPosition.y,
      s_ghostRig.anchorPosition.z, s_ghostRig.anchorRotation.x,
      s_ghostRig.anchorRotation.y, s_ghostRig.anchorRotation.z,
      s_ghostRig.anchorRotation.w,
      (unsigned long long)s_ghostRig.generation,
      reinterpret_cast<void *>(s_ghostRig.ownerCharacter), ownerRoot,
      s_ghostRig.restoreAnchorValid ? 1 : 0, GetCurrentThreadId());
  return true;
}

static float GhostRig_Phase7DeltaSeconds(bool advanceState) {
  LARGE_INTEGER now = {};
  LARGE_INTEGER frequency = {};
  if (!QueryPerformanceCounter(&now) ||
      !QueryPerformanceFrequency(&frequency) ||
      frequency.QuadPart <= 0) {
    return advanceState ? (1.0f / 60.0f) : 0.0f;
  }
  float deltaSeconds = 0.0f;
  if (advanceState && s_ghostRig.terrainLastQpc != 0) {
    deltaSeconds = static_cast<float>(
        static_cast<double>(now.QuadPart -
                            s_ghostRig.terrainLastQpc) /
        static_cast<double>(frequency.QuadPart));
  } else if (advanceState) {
    deltaSeconds = 1.0f / 60.0f;
  }
  s_ghostRig.terrainLastQpc = now.QuadPart;
  if (!advanceState)
    return 0.0f;
  if (!DirectVmdFinite(deltaSeconds) || deltaSeconds <= 0.0f)
    return 1.0f / 60.0f;
  return DirectVmdTerrainClamp(deltaSeconds, 1.0f / 300.0f, 0.100f);
}

static bool GhostRig_UpdatePhase7Terrain(
    int frame, VmdVec3 flatRootPosition, VmdVec3 *terrainRootPosition) {
  if (!terrainRootPosition ||
      !GhostRig_RequireMainThread("GhostRig.UpdatePhase7Terrain", false) ||
      s_ghostRig.state != GhostRigState::Alive)
    return false;
  *terrainRootPosition = flatRootPosition;

  const bool enabled = s_directVmdTerrainDesiredEnabled.load(
      std::memory_order_acquire);
  if (enabled != s_ghostRig.terrainEnabledLast) {
    GhostRig_ResetPhase7TerrainState();
    s_ghostRig.terrainEnabledLast = enabled;
    if (!enabled)
      GhostRig_SuppressGrounderForFlatMode(frame);
    Log("[P7-TERRAIN-TOGGLE] enabled=%d unityFrame=%d vmdFrame=%.6f "
        "generation=%llu owner=%p stateReset=1 flatPathExactWhenOff=1 "
        "samplesAndGhostUntouched=1 heightBackend=%s "
        "customFindFloorRuntime=0 tid=%lu",
        enabled ? 1 : 0, frame, s_ghostRig.lastSourceFrame,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        enabled ? "Grounding.Raycast-delegate-foot-xz"
                : "flat-DirectVmd",
        GetCurrentThreadId());
  }

  s_ghostRig.terrainFrame = DirectVmdTerrainFrameOutput();
  s_ghostRig.terrainFrameValid = false;
  s_ghostRig.lastTerrainFrame = frame;
  if (!enabled) {
    GhostRig_SuppressGrounderForFlatMode(frame);
    return true;
  }

  terrainRootPosition->y += s_ghostRig.terrainState.rootOffset;

  const bool periodic = s_ghostRig.lastTerrainLogFrame == INT_MIN ||
      frame < 0 || frame - s_ghostRig.lastTerrainLogFrame >= 120;
  if (periodic) {
    s_ghostRig.lastTerrainLogFrame = frame;
    Log("[P7-GROUNDER-ROOT] unityFrame=%d vmdFrame=%.6f "
        "flatRoot=(%.6f,%.6f,%.6f) targetRoot=(%.6f,%.6f,%.6f) "
        "rootOffset=%.6f rootTargetOffset=%.6f "
        "rootOwner=DirectVmd-Grounder-hybrid terrainDatumSource=%s "
        "finalPoseOwner=DirectVmd-FinalIK "
        "customFloorQueries=0 vmdSampleWrites=0 ghostWrites=0 "
        "generation=%llu owner=%p tid=%lu",
        frame, s_ghostRig.lastSourceFrame, flatRootPosition.x,
        flatRootPosition.y, flatRootPosition.z,
        terrainRootPosition->x, terrainRootPosition->y,
        terrainRootPosition->z,
        s_ghostRig.terrainState.rootOffset,
        s_ghostRig.terrainState.rootTargetOffset,
        s_ghostRig.grounder.active ? "Grounding.Raycast-delegate-foot-xz"
                                   : "pending-or-fail-flat",
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
  return true;
}

static bool GhostRig_UpdatePhase7CustomTerrainLegacy(
    int frame, VmdVec3 flatRootPosition, VmdVec3 *terrainRootPosition) {
  if (!terrainRootPosition ||
      !GhostRig_RequireMainThread("GhostRig.UpdatePhase7Terrain", false) ||
      s_ghostRig.state != GhostRigState::Alive ||
      !s_ghostRig.rootPlacementValid || !s_ghostRig.lastSampleAccepted)
    return false;
  *terrainRootPosition = flatRootPosition;

  const bool enabled = s_directVmdTerrainDesiredEnabled.load(
      std::memory_order_acquire);
  if (enabled != s_ghostRig.terrainEnabledLast) {
    GhostRig_ResetPhase7TerrainState();
    s_ghostRig.terrainEnabledLast = enabled;
    Log("[P7-TERRAIN-TOGGLE] enabled=%d unityFrame=%d vmdFrame=%.6f "
        "generation=%llu owner=%p stateReset=1 flatPathExactWhenOff=1 "
        "samplesAndGhostUntouched=1 tid=%lu",
        enabled ? 1 : 0, frame, s_ghostRig.lastSourceFrame,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
  if (!enabled) {
    s_ghostRig.terrainFrame = DirectVmdTerrainFrameOutput();
    s_ghostRig.terrainFrameValid = false;
    s_ghostRig.lastTerrainFrame = frame;
    return true;
  }

  if (frame >= 0 && s_ghostRig.terrainFrameValid &&
      s_ghostRig.lastTerrainFrame == frame) {
    terrainRootPosition->y += s_ghostRig.terrainFrame.rootOffset;
    return true;
  }

  DirectVmdTerrainFrameInput input;
  const DirectVmdBoneId footIds[DIRECT_VMD_TERRAIN_FOOT_COUNT] = {
      DirectVmdBoneId::LeftFootIk, DirectVmdBoneId::RightFootIk};
  const bool ikEnabled[DIRECT_VMD_TERRAIN_FOOT_COUNT] = {
      s_ghostRig.lastLeftFootIkEnabled,
      s_ghostRig.lastRightFootIkEnabled};
  bool flatTargetsValid = true;
  for (uint32_t side = 0; side < DIRECT_VMD_TERRAIN_FOOT_COUNT;
       ++side) {
    VmdVec3 flatTarget = {};
    if (!GhostRig_ReadGhostWorldPose(footIds[side], &flatTarget,
                                     nullptr)) {
      flatTargetsValid = false;
      continue;
    }
    input.flatFootTarget[side] =
        GhostRig_ApplyRootPlacementToWorldPosition(flatTarget);
    input.ankleClearance[side] =
        s_ghostRig.terrainAnkleClearance[side];
    input.ikEnabled[side] = ikEnabled[side] ? 1 : 0;
  }
  if (!flatTargetsValid) {
    s_ghostRig.terrainFrameValid = false;
    return false;
  }

  const bool advanceState =
      s_ghostRig.lastSamplePlayback ==
      DirectVmdPlaybackState::Playing;
  const float deltaSeconds =
      GhostRig_Phase7DeltaSeconds(advanceState);
  input.deltaSeconds = deltaSeconds;
  input.advanceState = advanceState ? 1 : 0;

  if (advanceState && s_ghostRig.terrainFrameValid) {
    const bool cycleChanged =
        s_ghostRig.lastPlaybackCycle !=
        s_ghostRig.terrainLastPlaybackCycle;
    const double sourceStep = std::fabs(
        s_ghostRig.lastSourceFrame -
        s_ghostRig.terrainLastSourceFrame);
    const double seekThreshold = (std::max)(
        8.0, static_cast<double>(deltaSeconds) *
                 kVmdFramesPerSecond * 4.0);
    if (cycleChanged) {
      DirectVmdTerrainResetKinematics(&s_ghostRig.terrainState,
                                      false);
      Log("[P7-TERRAIN-TIMELINE] event=loop unityFrame=%d "
          "oldCycle=%llu newCycle=%llu velocityHistoryReset=1 "
          "contactHistoryPreserved=1 rootAccumulation=0 generation=%llu "
          "owner=%p tid=%lu",
          frame,
          (unsigned long long)s_ghostRig.terrainLastPlaybackCycle,
          (unsigned long long)s_ghostRig.lastPlaybackCycle,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    } else if (sourceStep > seekThreshold) {
      DirectVmdTerrainResetKinematics(&s_ghostRig.terrainState,
                                      true);
      Log("[P7-TERRAIN-TIMELINE] event=seek unityFrame=%d "
          "sourceStep=%.6f threshold=%.6f contactsReset=1 "
          "stablePlanesPreserved=1 rootOffsetPreserved=1 generation=%llu "
          "owner=%p tid=%lu",
          frame, sourceStep, seekThreshold,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    }
  }

  if (advanceState) {
    static constexpr float kProbeIntervalSeconds = 1.0f / 60.0f;
    s_ghostRig.terrainProbeAccumulator += deltaSeconds;
    const bool queryNow = !s_ghostRig.terrainProbeCacheValid ||
        s_ghostRig.terrainProbeAccumulator >= kProbeIntervalSeconds;
    if (queryNow) {
      s_ghostRig.terrainProbeAccumulator =
          s_ghostRig.terrainProbeCacheValid
              ? std::fmod(s_ghostRig.terrainProbeAccumulator,
                          kProbeIntervalSeconds)
              : 0.0f;
      const float legLength[DIRECT_VMD_TERRAIN_FOOT_COUNT] = {
          s_ghostRig.leftLegLength, s_ghostRig.rightLegLength};
      static constexpr float
          kOffsetX[DIRECT_VMD_TERRAIN_PROBE_COUNT] = {
              0.0f, 1.0f, -1.0f, 0.0f, 0.0f};
      static constexpr float
          kOffsetZ[DIRECT_VMD_TERRAIN_PROBE_COUNT] = {
              0.0f, 0.0f, 0.0f, 1.0f, -1.0f};
      for (uint32_t side = 0;
           side < DIRECT_VMD_TERRAIN_FOOT_COUNT; ++side) {
        s_ghostRig.terrainCachedRawPlane[side] =
            DirectVmdTerrainPlane();
        if (!input.ikEnabled[side])
          continue;
        const float probeRadius = DirectVmdTerrainClamp(
            legLength[side] * 0.09f, 0.055f, 0.130f);
        const float queryY = (std::max)(
            flatRootPosition.y + legLength[side] * 1.75f,
            input.flatFootTarget[side].y + legLength[side]);
        DirectVmdTerrainProbeHit
            samples[DIRECT_VMD_TERRAIN_PROBE_COUNT] = {};
        for (uint32_t probe = 0;
             probe < DIRECT_VMD_TERRAIN_PROBE_COUNT; ++probe) {
          const VmdVec3 query = {
              input.flatFootTarget[side].x +
                  kOffsetX[probe] * probeRadius,
              queryY,
              input.flatFootTarget[side].z +
                  kOffsetZ[probe] * probeRadius};
          ++s_ghostRig.terrainQueryCount;
          if (GhostRig_QueryFindFloorSample(
                  s_ghostRig.ownerCharacter, query, 20.0f,
                  &samples[probe]))
            ++s_ghostRig.terrainHitCount;
        }
        const DirectVmdTerrainFootState &history =
            s_ghostRig.terrainState.feet[side];
        s_ghostRig.terrainCachedRawPlane[side] =
            DirectVmdAggregateTerrainPlane(
                samples, DIRECT_VMD_TERRAIN_PROBE_COUNT,
                s_ghostRig.terrainConfig.planeClusterHeight,
                history.stablePlaneValid, history.stableHeight,
                s_ghostRig.terrainConfig.minimumNormalY);
      }
      s_ghostRig.terrainProbeCacheValid = true;
    }
    if (s_ghostRig.terrainProbeCacheValid) {
      for (uint32_t side = 0;
           side < DIRECT_VMD_TERRAIN_FOOT_COUNT; ++side) {
        input.rawPlane[side] =
            s_ghostRig.terrainCachedRawPlane[side];
      }
    }
  }

  bool floorReferenceBefore[DIRECT_VMD_TERRAIN_FOOT_COUNT] = {};
  for (uint32_t side = 0;
       side < DIRECT_VMD_TERRAIN_FOOT_COUNT; ++side) {
    floorReferenceBefore[side] =
        s_ghostRig.terrainState.feet[side].floorReferenceValid;
  }
  DirectVmdUpdateTerrainState(
      &s_ghostRig.terrainState, s_ghostRig.terrainConfig, input,
      &s_ghostRig.terrainFrame);
  for (uint32_t side = 0;
       side < DIRECT_VMD_TERRAIN_FOOT_COUNT; ++side) {
    const DirectVmdTerrainFootState &foot =
        s_ghostRig.terrainState.feet[side];
    if (!foot.floorReferenceValid)
      continue;
    if (!floorReferenceBefore[side]) {
      Log("[P7-TERRAIN-REFERENCE] side=%s unityFrame=%d vmdFrame=%.6f "
          "floorReference=%.6f "
          "source=ComputeFloorDist-small-sweep-world-height "
          "vmdFirstKeySubtraction=0 bindCalibration=0 "
          "flatTargetPreserved=1 generation=%llu owner=%p tid=%lu",
          side == 0 ? "L" : "R", frame,
          s_ghostRig.lastSourceFrame, foot.floorReferenceHeight,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    }
  }
  s_ghostRig.terrainFrameValid = true;
  s_ghostRig.lastTerrainFrame = frame;
  if (advanceState) {
    s_ghostRig.terrainLastSourceFrame = s_ghostRig.lastSourceFrame;
    s_ghostRig.terrainLastPlaybackCycle =
        s_ghostRig.lastPlaybackCycle;
  }
  terrainRootPosition->y += s_ghostRig.terrainFrame.rootOffset;

  bool periodicLog = s_ghostRig.lastTerrainLogFrame == INT_MIN;
  if (frame >= 0 && s_ghostRig.lastTerrainLogFrame != INT_MIN)
    periodicLog = frame - s_ghostRig.lastTerrainLogFrame >= 120;
  if (periodicLog) {
    s_ghostRig.lastTerrainLogFrame = frame;
    Log("[P7-TERRAIN-ROOT] unityFrame=%d vmdFrame=%.6f "
        "enabled=1 playback=%u dt=%.6f flatRoot=(%.6f,%.6f,%.6f) "
        "terrainRoot=(%.6f,%.6f,%.6f) rootOffset=%.6f "
        "rootTargetOffset=%.6f support=%s rootSupport=%s "
        "pendingSupport=%s pendingRootSupport=%s "
        "footTau=%.3f rootTau=%.3f "
        "queries=%llu hits=%llu generation=%llu owner=%p tid=%lu "
        "terrainDatum=per-foot-small-sweep-floor-delta "
        "vmdSampleWrites=0 ghostWrites=0 previousRealPoseInput=0",
        frame, s_ghostRig.lastSourceFrame,
        static_cast<unsigned>(s_ghostRig.lastSamplePlayback),
        deltaSeconds, flatRootPosition.x, flatRootPosition.y,
        flatRootPosition.z, terrainRootPosition->x,
        terrainRootPosition->y, terrainRootPosition->z,
        s_ghostRig.terrainFrame.rootOffset,
        s_ghostRig.terrainFrame.rootTargetOffset,
        DirectVmdTerrainSupportStateName(
            s_ghostRig.terrainFrame.support),
        DirectVmdTerrainSupportStateName(
            s_ghostRig.terrainFrame.rootSupport),
        DirectVmdTerrainSupportStateName(
            s_ghostRig.terrainState.pendingSupport),
        DirectVmdTerrainSupportStateName(
            s_ghostRig.terrainFrame.pendingRootSupport),
        s_ghostRig.terrainConfig.footHeightTimeConstant,
        s_ghostRig.terrainConfig.rootHeightTimeConstant,
        (unsigned long long)s_ghostRig.terrainQueryCount,
        (unsigned long long)s_ghostRig.terrainHitCount,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
  return true;
}

static bool GhostRig_ApplyPhase7LegTarget(
    DirectVmdLegSide side, int frame, VmdVec3 *footIkParent,
    VmdVec3 *footTarget, VmdVec3 *toeTarget,
    VmdQuaternion *normalRotation, bool *contactApplied) {
  if (!footIkParent || !footTarget || !toeTarget || !normalRotation ||
      !contactApplied)
    return false;
  *normalRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  *contactApplied = false;
  if (!s_ghostRig.terrainEnabledLast ||
      !s_ghostRig.terrainFrameValid ||
      s_ghostRig.lastTerrainFrame != frame)
    return false;
  const uint32_t index = static_cast<uint32_t>(side);
  if (index >= DIRECT_VMD_TERRAIN_FOOT_COUNT)
    return false;
  const DirectVmdTerrainFootOutput &terrain =
      s_ghostRig.terrainFrame.feet[index];
  if (!terrain.ikEnabled)
    return false;

  const VmdVec3 flatFoot = *footTarget;
  const float appliedOffset =
      terrain.desiredTarget.y - flatFoot.y;
  footTarget->y = terrain.desiredTarget.y;
  footIkParent->y += appliedOffset;
  toeTarget->y += appliedOffset;
  if (terrain.contact == DirectVmdTerrainContactState::Contact &&
      terrain.stablePlane) {
    *normalRotation =
        DirectVmdTerrainNormalRotation(terrain.normal);
    *contactApplied = true;
  }
  return true;
}

static void GhostRig_LogPhase7Foot(
    DirectVmdLegSide side, int frame, bool vmdIkEnabled,
    float finalIkWeight, const GhostRigLegRuntime &leg) {
  if (!s_ghostRig.terrainEnabledLast ||
      !s_ghostRig.terrainFrameValid ||
      s_ghostRig.lastTerrainFrame != frame)
    return;
  const uint32_t index = static_cast<uint32_t>(side);
  if (index >= DIRECT_VMD_TERRAIN_FOOT_COUNT)
    return;
  const DirectVmdTerrainFootOutput &terrain =
      s_ghostRig.terrainFrame.feet[index];
  Log("[P7-TERRAIN-FOOT] side=%s unityFrame=%d vmdFrame=%.6f "
      "hit=%d heldHit=%d stablePlane=%d contact=%s "
      "probeHits=%u clusterHits=%u centerHit=%d centerAnchored=%d "
      "pointFit=%d "
      "rawHeight=%.6f stableHeight=%.6f floorReference=%.6f "
      "contactPlane=%d contactHeight=%.6f swingArmed=%d "
      "planePending=%d pendingHeight=%.6f pendingSeconds=%.6f "
      "planeSwitches=%u "
      "centerPoint=(%.6f,%.6f,%.6f) centerQueryXZError=%.6f "
      "centerFloorDist=%.6f "
      "normal=(%.6f,%.6f,%.6f) lift=%.6f verticalVelocity=%.6f "
      "surfaceGap=%.6f surfaceOffset=%.6f appliedOffset=%.6f "
      "flatTarget=(%.6f,%.6f,%.6f) "
      "desiredTarget=(%.6f,%.6f,%.6f) "
      "solverTarget=(%.6f,%.6f,%.6f) "
      "support=%s rootSupport=%s ikEnabled=%d finalIkWeight=%.1f "
      "reachResidual=%.6f footRotation=(%.7f,%.7f,%.7f,%.7f) "
      "terrainNormalRotation=(%.7f,%.7f,%.7f,%.7f) "
      "bendDirection=(%.6f,%.6f,%.6f) generation=%llu owner=%p "
      "tid=%lu pipeline=flat-contact-terrain-reach-finalik",
      GhostRig_LegSideName(side), frame, s_ghostRig.lastSourceFrame,
      terrain.hit ? 1 : 0, terrain.heldHit ? 1 : 0,
      terrain.stablePlane ? 1 : 0,
      DirectVmdTerrainContactStateName(terrain.contact),
      static_cast<unsigned>(terrain.rawHitCount),
      static_cast<unsigned>(terrain.rawClusterCount),
      terrain.centerHit ? 1 : 0,
      terrain.centerAnchored ? 1 : 0,
      terrain.normalFromPointFit ? 1 : 0,
      terrain.rawHeight, terrain.stableHeight,
      terrain.floorReferenceHeight, terrain.contactPlane ? 1 : 0,
      terrain.contactPlaneHeight, terrain.swingArmed ? 1 : 0,
      terrain.pendingPlane ? 1 : 0, terrain.pendingPlaneHeight,
      terrain.planeSwitchSeconds,
      static_cast<unsigned>(terrain.planeSwitchCount),
      terrain.centerPoint.x, terrain.centerPoint.y,
      terrain.centerPoint.z, terrain.centerHorizontalError,
      terrain.centerFloorDistance,
      terrain.normal.x,
      terrain.normal.y, terrain.normal.z, terrain.lift,
      terrain.verticalVelocity, terrain.surfaceGap,
      terrain.surfaceOffset, terrain.appliedOffset,
      terrain.flatTarget.x, terrain.flatTarget.y, terrain.flatTarget.z,
      leg.reach.desiredTarget.x, leg.reach.desiredTarget.y,
      leg.reach.desiredTarget.z, leg.reach.solverTarget.x,
      leg.reach.solverTarget.y, leg.reach.solverTarget.z,
      DirectVmdTerrainSupportStateName(s_ghostRig.terrainFrame.support),
      DirectVmdTerrainSupportStateName(
          s_ghostRig.terrainFrame.rootSupport),
      vmdIkEnabled ? 1 : 0, finalIkWeight, leg.reach.residual,
      leg.footWorldRotation.x, leg.footWorldRotation.y,
      leg.footWorldRotation.z, leg.footWorldRotation.w,
      leg.terrainNormalRotation.x, leg.terrainNormalRotation.y,
      leg.terrainNormalRotation.z, leg.terrainNormalRotation.w,
      leg.bendDirection.x, leg.bendDirection.y, leg.bendDirection.z,
      (unsigned long long)s_ghostRig.generation,
      reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
      GetCurrentThreadId());
}

static bool GhostRig_ApplyPhase4TargetRoot(int frame, void *ownerRoot) {
  if (!GhostRig_RequireMainThread("GhostRig.ApplyPhase4TargetRoot", false) ||
      s_ghostRig.state != GhostRigState::Alive || !ownerRoot ||
      !g_motionBackend.Is(MotionBackend::DirectVmd))
    return false;

  const uint64_t generation = s_ghostRig.generation;
  const uintptr_t owner = s_ghostRig.ownerCharacter;
  if (s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
          generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) != owner ||
      !GhostRig_SameUnityObject(ownerRoot,
                               GhostRig_GetRetainedOwnerRoot()))
    return false;

  bool capturePlacement = false;
  if (!s_ghostRig.playbackAnchorCaptured) {
    if (!s_ghostRig.lastSampleAccepted ||
        s_ghostRig.lastSamplePlayback != DirectVmdPlaybackState::Playing)
      return true;
    if (!GhostRig_CapturePlaybackAnchor(ownerRoot))
      return false;
    capturePlacement = true;
  }
  if (!s_ghostRig.anchorValid || !s_ghostRig.lastSampleAccepted)
    return true;

  void *ghostRootTransform = GhostRig_GetTransform(0);
  void *allParentTransform = GhostRig_GetTransform(
      GhostRig_NodeIndex(DirectVmdBoneId::AllParent));
  void *centerTransform = GhostRig_GetTransform(
      GhostRig_NodeIndex(DirectVmdBoneId::Center));
  void *grooveTransform = GhostRig_GetTransform(
      GhostRig_NodeIndex(DirectVmdBoneId::Groove));
  if (!ghostRootTransform || !allParentTransform || !centerTransform ||
      !grooveTransform)
    return false;

  Vec3 ghostRootPosition = {};
  Quat ghostRootRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  Vec3 allParentPosition = {};
  Quat allParentRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  Vec3 centerPosition = {};
  Quat centerRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  Vec3 groovePosition = {};
  Quat grooveRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  if (!GhostRig_ReadWorldPosition(ghostRootTransform, ghostRootPosition) ||
      !GhostRig_ReadWorldRotation(ghostRootTransform, ghostRootRotation) ||
      !GhostRig_ReadWorldPosition(allParentTransform, allParentPosition) ||
      !GhostRig_ReadWorldRotation(allParentTransform, allParentRotation) ||
      !GhostRig_ReadWorldPosition(centerTransform, centerPosition) ||
      !GhostRig_ReadWorldRotation(centerTransform, centerRotation) ||
      !GhostRig_ReadWorldPosition(grooveTransform, groovePosition) ||
      !GhostRig_ReadWorldRotation(grooveTransform, grooveRotation))
    return false;

  const DirectVmdWorldPosePod anchor = {
      s_ghostRig.anchorPosition,
      DirectVmdNormalizeQuaternion(s_ghostRig.anchorRotation)};
  const DirectVmdBindNodePod &grooveBind =
      s_ghostRig.nodes[GhostRig_NodeIndex(DirectVmdBoneId::Groove)].bind;
  const DirectVmdWorldPosePod grooveBindOwner = {
      grooveBind.worldPosition,
      DirectVmdNormalizeQuaternion(grooveBind.worldRotation)};
  const DirectVmdWorldPosePod grooveWorld = {
      {groovePosition.x, groovePosition.y, groovePosition.z},
      DirectVmdNormalizeQuaternion(
          {grooveRotation.x, grooveRotation.y, grooveRotation.z,
           grooveRotation.w})};
  const DirectVmdWorldPosePod unplacedDesired =
      DirectVmdEvaluateTargetRootFromGhostControl(
          anchor, grooveBindOwner, grooveWorld);
  if (capturePlacement && g_beFixedAnchorValid) {
    // BE-PATCH(squad-anchor): shared origin, VMD positions kept as authored.
    s_ghostRig.rootPlacementOffset = {0.0f, 0.0f, 0.0f};
    s_ghostRig.rootPlacementValid = true;
    s_ghostRig.playbackAnchorCaptured = true;
    Log("[P4-ROOT-PLACEMENT] captured=1 squadSharedAnchor=1 offset=0 "
        "generation=%llu owner=%p",
        (unsigned long long)generation, reinterpret_cast<void *>(owner));
  } else if (capturePlacement) {
    s_ghostRig.rootPlacementOffset =
        DirectVmdBuildWorldTranslationPlacementOffset(
            anchor, unplacedDesired);
    if (!DirectVmdFinite(s_ghostRig.rootPlacementOffset.x) ||
        !DirectVmdFinite(s_ghostRig.rootPlacementOffset.y) ||
        !DirectVmdFinite(s_ghostRig.rootPlacementOffset.z)) {
      s_ghostRig.rootPlacementOffset = {0.0f, 0.0f, 0.0f};
      s_ghostRig.rootPlacementValid = false;
      Log("[P4-ROOT-PLACEMENT] rejected-nonfinite unityFrame=%d "
          "vmdFrame=%.6f generation=%llu owner=%p tid=%lu",
          frame, s_ghostRig.lastSourceFrame,
          (unsigned long long)generation,
          reinterpret_cast<void *>(owner), GetCurrentThreadId());
      return false;
    }
    s_ghostRig.rootPlacementValid = true;
    s_ghostRig.playbackAnchorCaptured = true;
    Log("[P4-ROOT-PLACEMENT] captured=1 unityFrame=%d vmdFrame=%.6f "
        "cycle=%llu entryRootP=(%.6f,%.6f,%.6f) "
        "evaluatedFirstRootP=(%.6f,%.6f,%.6f) "
        "worldOffset=(%.6f,%.6f,%.6f) offsetLength=%.6f "
        "positionAligned=1 rotationAligned=0 firstSamplePreserved=1 "
        "keySubtraction=0 sharedWithFootIkAndCamera=1 "
        "generation=%llu owner=%p tid=%lu",
        frame, s_ghostRig.lastSourceFrame,
        (unsigned long long)s_ghostRig.lastPlaybackCycle,
        anchor.position.x, anchor.position.y, anchor.position.z,
        unplacedDesired.position.x, unplacedDesired.position.y,
        unplacedDesired.position.z, s_ghostRig.rootPlacementOffset.x,
        s_ghostRig.rootPlacementOffset.y,
        s_ghostRig.rootPlacementOffset.z,
        DirectVmdLength(s_ghostRig.rootPlacementOffset),
        (unsigned long long)generation,
        reinterpret_cast<void *>(owner), GetCurrentThreadId());
  }
  if (!s_ghostRig.rootPlacementValid)
    return false;
  const DirectVmdWorldPosePod flatDesired =
      DirectVmdApplyWorldTranslationPlacement(
          unplacedDesired, s_ghostRig.rootPlacementOffset);
  DirectVmdWorldPosePod desired = flatDesired;
  GhostRig_UpdatePhase7Terrain(frame, flatDesired.position,
                               &desired.position);
  if (!DirectVmdFinite(desired.position.x) ||
      !DirectVmdFinite(desired.position.y) ||
      !DirectVmdFinite(desired.position.z) ||
      !DirectVmdFinite(desired.rotation.x) ||
      !DirectVmdFinite(desired.rotation.y) ||
      !DirectVmdFinite(desired.rotation.z) ||
      !DirectVmdFinite(desired.rotation.w)) {
    Log("[P4-ROOT-WRITE] rejected-nonfinite unityFrame=%d "
        "vmdFrame=%.6f generation=%llu owner=%p tid=%lu",
        frame, s_ghostRig.lastSourceFrame,
        (unsigned long long)generation, reinterpret_cast<void *>(owner),
        GetCurrentThreadId());
    return false;
  }

  if (!s_ghostRig.sourceReference.controlled[DirectVmdBoneIndex(DirectVmdBoneId::AllParent)] &&
      !s_ghostRig.sourceReference.controlled[DirectVmdBoneIndex(DirectVmdBoneId::Center)] &&
      !s_ghostRig.sourceReference.controlled[DirectVmdBoneIndex(DirectVmdBoneId::Groove)]) {
    s_ghostRig.lastDesiredRootPosition = s_ghostRig.anchorPosition;
    s_ghostRig.lastDesiredRootRotation = s_ghostRig.anchorRotation;
    s_ghostRig.lastDesiredRootValid = true;
    return true;
  }
  const bool rotationWritten = GhostRig_WriteWorldRotation(
      ownerRoot, {desired.rotation.x, desired.rotation.y,
                  desired.rotation.z, desired.rotation.w});
  const bool positionWritten = GhostRig_WriteWorldPosition(
      ownerRoot, {desired.position.x, desired.position.y,
                  desired.position.z});
  if (rotationWritten || positionWritten)
    s_ghostRig.rootMotionOwned = true;
  if (!rotationWritten || !positionWritten) {
    Log("[P4-ROOT-WRITE] failed unityFrame=%d vmdFrame=%.6f "
        "positionWritten=%d rotationWritten=%d generation=%llu owner=%p "
        "tid=%lu",
        frame, s_ghostRig.lastSourceFrame, positionWritten ? 1 : 0,
        rotationWritten ? 1 : 0, (unsigned long long)generation,
        reinterpret_cast<void *>(owner), GetCurrentThreadId());
    return false;
  }

  ++s_ghostRig.rootApplyCount;
  ++s_ghostRig.rootWorldWriteCount;
  s_ghostRig.lastDesiredRootPosition = desired.position;
  s_ghostRig.lastDesiredRootRotation = desired.rotation;
  s_ghostRig.lastDesiredRootValid = true;

  if (!s_ghostRig.rootCycleSeen) {
    s_ghostRig.rootCycleSeen = true;
    s_ghostRig.lastAppliedRootCycle = s_ghostRig.lastPlaybackCycle;
  } else if (s_ghostRig.lastPlaybackCycle !=
             s_ghostRig.lastAppliedRootCycle) {
    Log("[P4-ROOT-LOOP] unityFrame=%d vmdFrame=%.6f oldCycle=%llu "
        "newCycle=%llu anchorRecaptured=0 absoluteEvaluation=1 "
        "previousTargetIsInput=0 generation=%llu owner=%p tid=%lu",
        frame, s_ghostRig.lastSourceFrame,
        (unsigned long long)s_ghostRig.lastAppliedRootCycle,
        (unsigned long long)s_ghostRig.lastPlaybackCycle,
        (unsigned long long)generation, reinterpret_cast<void *>(owner),
        GetCurrentThreadId());
    s_ghostRig.lastAppliedRootCycle = s_ghostRig.lastPlaybackCycle;
  }

  bool periodicLog = s_ghostRig.lastRootLogFrame == INT_MIN;
  if (frame >= 0 && s_ghostRig.lastRootLogFrame != INT_MIN)
    periodicLog = frame - s_ghostRig.lastRootLogFrame >= 120;
  else if (frame < 0)
    periodicLog = (s_ghostRig.rootApplyCount % 120) == 1;
  if (periodicLog) {
    s_ghostRig.lastRootLogFrame = frame;
    Log("[P4-ROOT-POSE] unityFrame=%d vmdFrame=%.6f cycle=%llu "
        "playback=%u generation=%llu owner=%p motionScale=%.8f "
        "GhostRootP=(%.6f,%.6f,%.6f) "
        "GhostRootR=(%.7f,%.7f,%.7f,%.7f) "
        "unplacedTargetRootP=(%.6f,%.6f,%.6f) "
        "placementOffset=(%.6f,%.6f,%.6f) "
        "targetRootP=(%.6f,%.6f,%.6f) "
        "targetRootR=(%.7f,%.7f,%.7f,%.7f) "
        "anchorP=(%.6f,%.6f,%.6f) "
        "anchorR=(%.7f,%.7f,%.7f,%.7f) tid=%lu",
        frame, s_ghostRig.lastSourceFrame,
        (unsigned long long)s_ghostRig.lastPlaybackCycle,
        static_cast<unsigned>(s_ghostRig.lastSamplePlayback),
        (unsigned long long)generation, reinterpret_cast<void *>(owner),
        s_ghostRig.motionScale, ghostRootPosition.x, ghostRootPosition.y,
        ghostRootPosition.z, ghostRootRotation.x, ghostRootRotation.y,
        ghostRootRotation.z, ghostRootRotation.w,
        unplacedDesired.position.x, unplacedDesired.position.y,
        unplacedDesired.position.z, s_ghostRig.rootPlacementOffset.x,
        s_ghostRig.rootPlacementOffset.y,
        s_ghostRig.rootPlacementOffset.z, desired.position.x,
        desired.position.y, desired.position.z, desired.rotation.x,
        desired.rotation.y, desired.rotation.z, desired.rotation.w,
        s_ghostRig.anchorPosition.x, s_ghostRig.anchorPosition.y,
        s_ghostRig.anchorPosition.z, s_ghostRig.anchorRotation.x,
        s_ghostRig.anchorRotation.y, s_ghostRig.anchorRotation.z,
        s_ghostRig.anchorRotation.w, GetCurrentThreadId());
    Log("[P4-ROOT-HIERARCHY] unityFrame=%d vmdFrame=%.6f "
        "AllParentP=(%.6f,%.6f,%.6f) "
        "AllParentR=(%.7f,%.7f,%.7f,%.7f) "
        "CenterP=(%.6f,%.6f,%.6f) "
        "CenterR=(%.7f,%.7f,%.7f,%.7f) "
        "GrooveP=(%.6f,%.6f,%.6f) "
        "GrooveR=(%.7f,%.7f,%.7f,%.7f) "
        "ownerMap=Root(AllParent+Center+Groove),Hips(LowerBodyRotation) "
        "hipsMotionPositionWrites=0 "
        "hipsBindPositionOwner=Phase5Stabilizer "
        "firstKeySubtraction=0 feedbackReads=0 "
        "tid=%lu",
        frame, s_ghostRig.lastSourceFrame, allParentPosition.x,
        allParentPosition.y, allParentPosition.z, allParentRotation.x,
        allParentRotation.y, allParentRotation.z, allParentRotation.w,
        centerPosition.x, centerPosition.y, centerPosition.z,
        centerRotation.x, centerRotation.y, centerRotation.z,
        centerRotation.w, groovePosition.x, groovePosition.y,
        groovePosition.z, grooveRotation.x, grooveRotation.y,
        grooveRotation.z, grooveRotation.w, GetCurrentThreadId());
  }
  return true;
}

static int GhostRig_Phase3HumanBone(DirectVmdBoneId id) {
  switch (id) {
  case DirectVmdBoneId::LowerBody: return HB_Hips;
  case DirectVmdBoneId::UpperBody: return HB_Spine;
  case DirectVmdBoneId::UpperBody2: return HB_Chest;
  case DirectVmdBoneId::Neck: return HB_Neck;
  case DirectVmdBoneId::Head: return HB_Head;
  case DirectVmdBoneId::LeftShoulder: return HB_LeftShoulder;
  case DirectVmdBoneId::LeftArm: return HB_LeftUpperArm;
  case DirectVmdBoneId::LeftElbow: return HB_LeftLowerArm;
  case DirectVmdBoneId::LeftWrist: return HB_LeftHand;
  case DirectVmdBoneId::RightShoulder: return HB_RightShoulder;
  case DirectVmdBoneId::RightArm: return HB_RightUpperArm;
  case DirectVmdBoneId::RightElbow: return HB_RightLowerArm;
  case DirectVmdBoneId::RightWrist: return HB_RightHand;
  case DirectVmdBoneId::LeftLeg: return HB_LeftUpperLeg;
  case DirectVmdBoneId::LeftKnee: return HB_LeftLowerLeg;
  case DirectVmdBoneId::LeftAnkle: return HB_LeftFoot;
  case DirectVmdBoneId::LeftToe: return HB_LeftToes;
  case DirectVmdBoneId::RightLeg: return HB_RightUpperLeg;
  case DirectVmdBoneId::RightKnee: return HB_RightLowerLeg;
  case DirectVmdBoneId::RightAnkle: return HB_RightFoot;
  case DirectVmdBoneId::RightToe: return HB_RightToes;
  default: return -1;
  }
}

static bool GhostRig_BuildPhase3StanceAlignment(
    DirectVmdBoneId id, VmdQuaternion *alignment,
    VmdVec3 *sourceDirectionGame, VmdVec3 *targetDirectionOwner) {
  if (!alignment || !sourceDirectionGame || !targetDirectionOwner)
    return false;
  *alignment = {0.0f, 0.0f, 0.0f, 1.0f};
  *sourceDirectionGame = {0.0f, 0.0f, 0.0f};
  *targetDirectionOwner = {0.0f, 0.0f, 0.0f};

  const GhostRigHandBindFrame *wristFrame = nullptr;
  if (id == DirectVmdBoneId::LeftWrist)
    wristFrame = &s_ghostRig.leftWristBindFrame;
  else if (id == DirectVmdBoneId::RightWrist)
    wristFrame = &s_ghostRig.rightWristBindFrame;
  if (wristFrame) {
    if (!wristFrame->hasDirectionAlignment)
      return false;
    *alignment = wristFrame->sourceToTargetOwnerAlignment;
    *sourceDirectionGame = wristFrame->sourceFrameGame.forward;
    *targetDirectionOwner = wristFrame->targetFrameOwner.forward;
    return true;
  }

  VmdVec3 sourceDirection = {};
  DirectVmdBoneId child = DirectVmdBoneId::Count;
  if (!GhostRig_SourceDirection(id, &sourceDirection) ||
      !eiem_source::DirectionChild(id, &child))
    return false;

  const DirectVmdBindNodePod &boneBind =
      s_ghostRig.nodes[GhostRig_NodeIndex(id)].bind;
  const DirectVmdBindNodePod &childBind =
      s_ghostRig.nodes[GhostRig_NodeIndex(child)].bind;
  VmdVec3 sourceUnit = {};
  VmdVec3 targetUnit = {};
  if (!DirectVmdTryNormalizeVector(
          SourceToGameBasis::ConvertPosition(sourceDirection),
          &sourceUnit) ||
      !DirectVmdTryNormalizeVector(
          DirectVmdSub(childBind.worldPosition, boneBind.worldPosition),
          &targetUnit))
    return false;

  *sourceDirectionGame = sourceUnit;
  *targetDirectionOwner = targetUnit;
  *alignment = DirectVmdQuaternionFromTo(sourceUnit, targetUnit);
  return true;
}

static void GhostRig_ResolvePhase3Targets(void *ownerRoot) {
  if (!GhostRig_RequireMainThread("GhostRig.ResolvePhase3Targets", false) ||
      !ownerRoot)
    return;

  GhostRig_FreeTargetHandles();
  GhostRig_FreeTwistTargetHandles();
  int resolvedCount = 0;
  int missingCount = 0;
  for (uint32_t order = 0; order < DIRECT_VMD_PHASE3_FK_BONE_COUNT;
       ++order) {
    const DirectVmdBoneId id = kDirectVmdPhase3FkBones[order];
    const uint32_t semantic = DirectVmdBoneIndex(id);
    GhostRigTargetBone &target = s_ghostRig.targets[semantic];
    target.humanBone = GhostRig_Phase3HumanBone(id);
    void *transform = target.humanBone >= 0
                          ? SafeGetBoneTransform(target.humanBone)
                          : nullptr;
    if (id == DirectVmdBoneId::UpperBody2 && !transform) {
      target.humanBone = HB_UpperChest;
      transform = SafeGetBoneTransform(target.humanBone);
    }

    if (!transform || !GhostRig_IsUnityObjectAlive(transform) ||
        !GhostRig_IsComponentUnderOwner(transform, ownerRoot)) {
      target.missingLogged = true;
      ++missingCount;
      Log("[P3-FK-MISSING] semantic='%s' humanBone=%d generation=%llu "
          "owner=%p reason=missing-or-outside-owner action=skip tid=%lu",
          kDirectVmdBoneSpecs[semantic].name, target.humanBone,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
      continue;
    }

    target.transformHandle = il2cpp_gchandle_new(transform, false);
    if (!target.transformHandle) {
      target.missingLogged = true;
      ++missingCount;
      Log("[P3-FK-MISSING] semantic='%s' humanBone=%d generation=%llu "
          "owner=%p reason=gchandle-failed action=skip tid=%lu",
          kDirectVmdBoneSpecs[semantic].name, target.humanBone,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
      continue;
    }

    target.bindOwnerRotation =
        s_ghostRig.nodes[GhostRig_NodeIndex(id)].bind.worldRotation;
    target.sourceToTargetOwnerAlignment = {0.0f, 0.0f, 0.0f, 1.0f};
    VmdVec3 sourceDirectionGame = {};
    VmdVec3 targetDirectionOwner = {};
    target.hasStanceAlignment = GhostRig_BuildPhase3StanceAlignment(
        id, &target.sourceToTargetOwnerAlignment, &sourceDirectionGame,
        &targetDirectionOwner);
    target.hasFullFrameAlignment =
        id == DirectVmdBoneId::LeftWrist
            ? s_ghostRig.leftWristBindFrame.hasFullFrameAlignment
            : (id == DirectVmdBoneId::RightWrist
                   ? s_ghostRig.rightWristBindFrame.hasFullFrameAlignment
                   : false);
    target.lastDesiredFrame = INT_MIN;
    target.lastDesiredValid = false;
    target.resolved = true;
    char transformName[256] = {};
    SafeGetBoneName(transform, transformName, sizeof(transformName));
    Log("[P3-TARGET-MAP] semantic='%s' humanBone=%d transform='%s' "
        "handle=%u bindOwnerR=(%.7f,%.7f,%.7f,%.7f) "
        "source=Avatar.humanDescription.skeleton readsLivePose=0 "
        "generation=%llu owner=%p tid=%lu",
        kDirectVmdBoneSpecs[semantic].name, target.humanBone,
        transformName, target.transformHandle,
        target.bindOwnerRotation.x, target.bindOwnerRotation.y,
        target.bindOwnerRotation.z, target.bindOwnerRotation.w,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    const char *alignmentMode =
        target.hasFullFrameAlignment
            ? "full-frame"
            : (target.hasStanceAlignment ? "direction-only" : "none");
    Log("[P3-STANCE-MAP] semantic='%s' enabled=%d mode=%s "
        "source=active-preset sourceGameDir=(%.6f,%.6f,%.6f) "
        "targetBindDir=(%.6f,%.6f,%.6f) "
        "alignmentOwner=(%.7f,%.7f,%.7f,%.7f) "
        "readsLivePose=0 firstFrameCalibration=0 generation=%llu "
        "owner=%p tid=%lu",
        kDirectVmdBoneSpecs[semantic].name,
        target.hasStanceAlignment ? 1 : 0, alignmentMode,
        sourceDirectionGame.x,
        sourceDirectionGame.y, sourceDirectionGame.z,
        targetDirectionOwner.x, targetDirectionOwner.y,
        targetDirectionOwner.z,
        target.sourceToTargetOwnerAlignment.x,
        target.sourceToTargetOwnerAlignment.y,
        target.sourceToTargetOwnerAlignment.z,
        target.sourceToTargetOwnerAlignment.w,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    ++resolvedCount;
  }

  for (uint32_t side = 0; side < DIRECT_VMD_LEG_SIDE_COUNT; ++side) {
    for (uint32_t order = 0; order < DIRECT_VMD_LEG_FK_BONE_COUNT;
         ++order) {
      const DirectVmdBoneId id = kDirectVmdPhase5LegFkBones[side][order];
      const uint32_t semantic = DirectVmdBoneIndex(id);
      GhostRigTargetBone &target = s_ghostRig.targets[semantic];
      target.humanBone = GhostRig_Phase3HumanBone(id);
      void *transform = target.humanBone >= 0
                            ? SafeGetBoneTransform(target.humanBone)
                            : nullptr;
      if (!transform || !GhostRig_IsUnityObjectAlive(transform) ||
          !GhostRig_IsComponentUnderOwner(transform, ownerRoot)) {
        target.missingLogged = true;
        ++missingCount;
        Log("[P5-LEG-MAP-MISSING] side=%s semantic='%s' humanBone=%d "
            "generation=%llu owner=%p reason=missing-or-outside-owner "
            "action=skip tid=%lu",
            side == 0 ? "L" : "R", kDirectVmdBoneSpecs[semantic].name,
            target.humanBone, (unsigned long long)s_ghostRig.generation,
            reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
            GetCurrentThreadId());
        continue;
      }

      target.transformHandle = il2cpp_gchandle_new(transform, false);
      if (!target.transformHandle) {
        target.missingLogged = true;
        ++missingCount;
        Log("[P5-LEG-MAP-MISSING] side=%s semantic='%s' humanBone=%d "
            "generation=%llu owner=%p reason=gchandle-failed "
            "action=skip tid=%lu",
            side == 0 ? "L" : "R", kDirectVmdBoneSpecs[semantic].name,
            target.humanBone, (unsigned long long)s_ghostRig.generation,
            reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
            GetCurrentThreadId());
        continue;
      }

      target.bindOwnerRotation =
          s_ghostRig.nodes[GhostRig_NodeIndex(id)].bind.worldRotation;
      target.sourceToTargetOwnerAlignment =
          {0.0f, 0.0f, 0.0f, 1.0f};
      target.lastDesiredWorldRotation =
          {0.0f, 0.0f, 0.0f, 1.0f};
      target.lastDesiredFrame = INT_MIN;
      target.hasStanceAlignment = false;
      target.hasFullFrameAlignment = false;
      target.lastDesiredValid = false;
      target.resolved = true;
      VmdVec3 sourceDirection = {}, targetDirection = {};
      target.hasStanceAlignment = GhostRig_BuildPhase3StanceAlignment(
          id, &target.sourceToTargetOwnerAlignment, &sourceDirection,
          &targetDirection);
      char transformName[256] = {};
      SafeGetBoneName(transform, transformName, sizeof(transformName));
      Log("[P5-LEG-MAP] side=%s semantic='%s' humanBone=%d "
          "transform='%s' handle=%u bindOwnerR=(%.7f,%.7f,%.7f,%.7f) "
          "source=Avatar.humanDescription.skeleton readsLivePose=0 "
          "generation=%llu owner=%p tid=%lu",
          side == 0 ? "L" : "R", kDirectVmdBoneSpecs[semantic].name,
          target.humanBone, transformName, target.transformHandle,
          target.bindOwnerRotation.x, target.bindOwnerRotation.y,
          target.bindOwnerRotation.z, target.bindOwnerRotation.w,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
      ++resolvedCount;
    }
  }

  for (uint32_t order = 0; order < DIRECT_VMD_PHASE6_FINGER_BONE_COUNT;
       ++order) {
    const DirectVmdBoneId id = kDirectVmdPhase6FingerBones[order];
    const uint32_t semantic = DirectVmdBoneIndex(id);
    GhostRigTargetBone &target = s_ghostRig.targets[semantic];
    target.humanBone = HB_None;
    const char *transformName =
        LookupFingerMapping(kDirectVmdBoneSpecs[semantic].name);
    void *transform =
        transformName ? SafeFindChildRecursive(ownerRoot, transformName, 64)
                      : nullptr;
    if (!s_ghostRig.targetNaturalBindAvailable[semantic] || !transform ||
        !GhostRig_IsUnityObjectAlive(transform) ||
        !GhostRig_IsComponentUnderOwner(transform, ownerRoot)) {
      target.missingLogged = true;
      ++missingCount;
      Log("[P6-FINGER-MAP-MISSING] semantic='%s' transform='%s' "
          "naturalBind=%d generation=%llu owner=%p "
          "reason=missing-bind-or-transform action=skip tid=%lu",
          kDirectVmdBoneSpecs[semantic].name,
          transformName ? transformName : "unmapped",
          s_ghostRig.targetNaturalBindAvailable[semantic] ? 1 : 0,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
      continue;
    }

    target.transformHandle = il2cpp_gchandle_new(transform, false);
    if (!target.transformHandle) {
      target.missingLogged = true;
      ++missingCount;
      Log("[P6-FINGER-MAP-MISSING] semantic='%s' transform='%s' "
          "generation=%llu owner=%p reason=gchandle-failed action=skip "
          "tid=%lu",
          kDirectVmdBoneSpecs[semantic].name, transformName,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
      continue;
    }
    target.bindOwnerRotation =
        s_ghostRig.nodes[GhostRig_NodeIndex(id)].bind.worldRotation;
    target.bindLocalRotation =
        s_ghostRig.nodes[GhostRig_NodeIndex(id)].bind.localRotation;
    target.sourceToTargetOwnerAlignment = {0.0f, 0.0f, 0.0f, 1.0f};
    target.lastDesiredWorldRotation = {0.0f, 0.0f, 0.0f, 1.0f};
    target.lastDesiredFrame = INT_MIN;
    target.hasStanceAlignment = false;
    target.hasFullFrameAlignment = false;
    target.lastDesiredValid = false;
    target.resolved = true;
    ++resolvedCount;
    Log("[P6-FINGER-MAP] semantic='%s' transform='%s' handle=%u "
        "bindLocalR=(%.7f,%.7f,%.7f,%.7f) "
        "source=Avatar.humanDescription.skeleton readsLivePose=0 "
        "generation=%llu owner=%p tid=%lu",
        kDirectVmdBoneSpecs[semantic].name, transformName,
        target.transformHandle, target.bindLocalRotation.x,
        target.bindLocalRotation.y, target.bindLocalRotation.z,
        target.bindLocalRotation.w,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }

  for (uint32_t order = 0; order < DIRECT_VMD_PHASE6_EYE_BONE_COUNT;
       ++order) {
    const DirectVmdBoneId id = kDirectVmdPhase6EyeBones[order];
    const uint32_t semantic = DirectVmdBoneIndex(id);
    GhostRigTargetBone &target = s_ghostRig.targets[semantic];
    target.humanBone = HB_None;
    const char *transformName =
        LookupFingerMapping(kDirectVmdBoneSpecs[semantic].name);
    void *transform =
        transformName ? SafeFindChildRecursive(ownerRoot, transformName, 64)
                      : nullptr;
    if (!s_ghostRig.targetNaturalBindAvailable[semantic] || !transform ||
        !GhostRig_IsUnityObjectAlive(transform) ||
        !GhostRig_IsComponentUnderOwner(transform, ownerRoot)) {
      target.missingLogged = true;
      ++missingCount;
      Log("[P6-EYE-MAP-MISSING] semantic='%s' transform='%s' "
          "naturalBind=%d generation=%llu owner=%p action=skip tid=%lu",
          kDirectVmdBoneSpecs[semantic].name,
          transformName ? transformName : "unmapped",
          s_ghostRig.targetNaturalBindAvailable[semantic] ? 1 : 0,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
      continue;
    }
    target.transformHandle = il2cpp_gchandle_new(transform, false);
    if (!target.transformHandle) {
      target.missingLogged = true;
      ++missingCount;
      continue;
    }
    target.bindOwnerRotation =
        s_ghostRig.nodes[GhostRig_NodeIndex(id)].bind.worldRotation;
    target.bindLocalRotation =
        s_ghostRig.nodes[GhostRig_NodeIndex(id)].bind.localRotation;
    target.sourceToTargetOwnerAlignment = {0.0f, 0.0f, 0.0f, 1.0f};
    target.lastDesiredFrame = INT_MIN;
    target.lastDesiredValid = false;
    target.resolved = true;
    ++resolvedCount;
    Log("[P6-EYE-MAP] semantic='%s' transform='%s' handle=%u "
        "bindOwnerR=(%.7f,%.7f,%.7f,%.7f) "
        "source=Avatar.humanDescription.skeleton readsLivePose=0 "
        "generation=%llu owner=%p tid=%lu",
        kDirectVmdBoneSpecs[semantic].name, transformName,
        target.transformHandle, target.bindOwnerRotation.x,
        target.bindOwnerRotation.y, target.bindOwnerRotation.z,
        target.bindOwnerRotation.w,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }

  for (uint32_t channel = 0;
       channel < DIRECT_VMD_PHASE6_TWIST_CHANNEL_COUNT; ++channel) {
    const DirectVmdBoneId control =
        kDirectVmdPhase6TwistChannels[channel].control;
    const uint32_t semantic = DirectVmdBoneIndex(control);
    for (uint32_t targetIndex = 0;
         targetIndex < DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL;
         ++targetIndex) {
      GhostRigTwistTargetRuntime &runtime =
          s_ghostRig.twistChannels[channel].targets[targetIndex];
      const char *transformName =
          runtime.transformName[0] ? runtime.transformName : nullptr;
      void *transform = transformName
                            ? SafeFindChildRecursive(ownerRoot,
                                                     transformName, 64)
                            : nullptr;
      if (!runtime.bindAvailable || !transform ||
          !GhostRig_IsUnityObjectAlive(transform) ||
          !GhostRig_IsComponentUnderOwner(transform, ownerRoot)) {
        ++missingCount;
        Log("[P6-TWIST-MAP-MISSING] channel=%u semantic='%s' "
            "targetIndex=%u transform='%s' bindAvailable=%d "
            "generation=%llu owner=%p action=skip-channel tid=%lu",
            channel, kDirectVmdBoneSpecs[semantic].name, targetIndex,
            transformName ? transformName : "unmapped",
            runtime.bindAvailable ? 1 : 0,
            (unsigned long long)s_ghostRig.generation,
            reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
            GetCurrentThreadId());
        continue;
      }
      runtime.transformHandle = il2cpp_gchandle_new(transform, false);
      if (!runtime.transformHandle) {
        ++missingCount;
        continue;
      }
      runtime.resolved = true;
      ++resolvedCount;
      Log("[P6-TWIST-MAP] channel=%u semantic='%s' targetIndex=%u "
          "transform='%s' handle=%u distribution=0.5 "
          "bindLocalR=(%.7f,%.7f,%.7f,%.7f) "
          "axisParentLocal=(%.6f,%.6f,%.6f) generation=%llu "
          "owner=%p tid=%lu",
          channel, kDirectVmdBoneSpecs[semantic].name, targetIndex,
          transformName, runtime.transformHandle,
          runtime.bindLocalRotation.x, runtime.bindLocalRotation.y,
          runtime.bindLocalRotation.z, runtime.bindLocalRotation.w,
          runtime.axisParentLocal.x, runtime.axisParentLocal.y,
          runtime.axisParentLocal.z,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    }
  }

  Log("[P3/P5/P6-TARGET-MAP] complete resolved=%d missing=%d allowed=%u "
      "generation=%llu owner=%p ordinaryFkPositionWrites=0 "
      "hipsBindPositionWritesDeferred=1 "
      "phase4RootOwner=Animator.transform rootWritesDeferred=1 "
      "legFinalIkWritesDeferred=1 tid=%lu",
      resolvedCount, missingCount,
      DIRECT_VMD_PHASE3_FK_BONE_COUNT +
          DIRECT_VMD_LEG_SIDE_COUNT * DIRECT_VMD_LEG_FK_BONE_COUNT +
          DIRECT_VMD_PHASE6_FINGER_BONE_COUNT +
          DIRECT_VMD_PHASE6_EYE_BONE_COUNT +
          DIRECT_VMD_PHASE6_TWIST_CHANNEL_COUNT *
              DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL,
      (unsigned long long)s_ghostRig.generation,
      reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
      GetCurrentThreadId());
}

static bool GhostRig_ApplyPhase3TargetFk(int frame) {
  if (!GhostRig_RequireMainThread("GhostRig.ApplyPhase3TargetFk", false) ||
      s_ghostRig.state != GhostRigState::Alive ||
      !g_motionBackend.Is(MotionBackend::DirectVmd) ||
      !s_ghostRig.anchorValid)
    return false;

  const uint64_t generation = s_ghostRig.generation;
  const uintptr_t owner = s_ghostRig.ownerCharacter;
  if (s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
          generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) != owner)
    return false;

  bool periodicLog = s_ghostRig.lastTargetLogFrame == INT_MIN;
  if (frame >= 0 && s_ghostRig.lastTargetLogFrame != INT_MIN)
    periodicLog = frame - s_ghostRig.lastTargetLogFrame >= 120;
  else if (frame < 0)
    periodicLog = (s_ghostRig.targetApplyCount % 120) == 0;

  const VmdQuaternion anchorRotation =
      DirectVmdNormalizeQuaternion(s_ghostRig.anchorRotation);
  int writes = 0;
  int missing = 0;
  for (uint32_t order = 0; order < DIRECT_VMD_PHASE3_FK_BONE_COUNT;
       ++order) {
    if (!g_motionBackend.Is(MotionBackend::DirectVmd) ||
        s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
            generation ||
        s_ghostRequestedOwnerId.load(std::memory_order_acquire) != owner) {
      Log("[P3-FK-ABORT] frame=%d generation=%llu owner=%p order=%u "
          "reason=ownership-invalidated tid=%lu",
          frame, (unsigned long long)generation,
          reinterpret_cast<void *>(owner), order, GetCurrentThreadId());
      return false;
    }

    const DirectVmdBoneId id = kDirectVmdPhase3FkBones[order];
    const uint32_t semantic = DirectVmdBoneIndex(id);
    GhostRigTargetBone &target = s_ghostRig.targets[semantic];
    void *targetTransform = GhostRig_GetTargetTransform(id);
    if (!s_ghostRig.sourceReference.controlled[semantic]) continue;
    void *ghostTransform = GhostRig_GetTransform(GhostRig_NodeIndex(id));
    if (!target.resolved || !targetTransform || !ghostTransform ||
        !GhostRig_IsUnityObjectAlive(targetTransform) ||
        !GhostRig_IsUnityObjectAlive(ghostTransform)) {
      ++missing;
      if (!target.missingLogged || periodicLog) {
        Log("[P3-FK-MISSING] semantic='%s' humanBone=%d frame=%d "
            "generation=%llu owner=%p reason=target-or-ghost-unavailable "
            "action=skip tid=%lu",
            kDirectVmdBoneSpecs[semantic].name, target.humanBone, frame,
            (unsigned long long)generation,
            reinterpret_cast<void *>(owner), GetCurrentThreadId());
        target.missingLogged = true;
      }
      continue;
    }

    Quat ghostWorld = {0, 0, 0, 1};
    if (!GhostRig_ReadWorldRotation(ghostTransform, ghostWorld)) {
      ++missing;
      if (!target.missingLogged || periodicLog) {
        Log("[P3-FK-MISSING] semantic='%s' frame=%d generation=%llu "
            "owner=%p reason=ghost-world-rotation-read-failed "
            "action=skip tid=%lu",
            kDirectVmdBoneSpecs[semantic].name, frame,
            (unsigned long long)generation,
            reinterpret_cast<void *>(owner), GetCurrentThreadId());
        target.missingLogged = true;
      }
      continue;
    }

    const VmdQuaternion ghostWorldRotation =
        DirectVmdNormalizeQuaternion(
            {ghostWorld.x, ghostWorld.y, ghostWorld.z, ghostWorld.w});
    const VmdQuaternion ghostBindWorldRotation =
        DirectVmdQuaternionMultiply(
            anchorRotation,
            s_ghostRig.nodes[GhostRig_NodeIndex(id)].bind.worldRotation);
    const VmdQuaternion targetBindWorldRotation =
        DirectVmdQuaternionMultiply(anchorRotation,
                                    target.bindOwnerRotation);
    const VmdQuaternion alignmentWorld = DirectVmdQuaternionMultiply(
        DirectVmdQuaternionMultiply(
            anchorRotation, target.sourceToTargetOwnerAlignment),
        DirectVmdQuaternionInverse(anchorRotation));
    const VmdQuaternion desired =
        DirectVmdRetargetWorldRotationFromSourceStance(
            ghostWorldRotation, ghostBindWorldRotation,
            targetBindWorldRotation, alignmentWorld);
    if (!GhostRig_WriteWorldRotation(
            targetTransform,
            {desired.x, desired.y, desired.z, desired.w})) {
      ++missing;
      if (!target.missingLogged || periodicLog) {
        Log("[P3-FK-MISSING] semantic='%s' frame=%d generation=%llu "
            "owner=%p reason=target-world-rotation-write-failed "
            "action=skip tid=%lu",
            kDirectVmdBoneSpecs[semantic].name, frame,
            (unsigned long long)generation,
            reinterpret_cast<void *>(owner), GetCurrentThreadId());
        target.missingLogged = true;
      }
      continue;
    }

    target.lastDesiredWorldRotation = desired;
    target.lastDesiredFrame = frame;
    target.lastDesiredValid = true;
    ++writes;
    ++s_ghostRig.targetRotationWriteCount;
    if (periodicLog) {
      Log("[P3-FK-BONE] frame=%d sourceFrame=%.6f semantic='%s' "
          "humanBone=%d generation=%llu owner=%p "
          "ghostWorldR=(%.7f,%.7f,%.7f,%.7f) "
          "desiredWorldR=(%.7f,%.7f,%.7f,%.7f) "
          "stanceAligned=%d alignmentMode=%s feedbackPoseReads=0 "
          "ordinaryTargetPositionWrites=0 "
          "tid=%lu",
          frame, s_ghostRig.lastSourceFrame,
          kDirectVmdBoneSpecs[semantic].name, target.humanBone,
          (unsigned long long)generation,
          reinterpret_cast<void *>(owner), ghostWorldRotation.x,
          ghostWorldRotation.y, ghostWorldRotation.z,
          ghostWorldRotation.w, desired.x, desired.y, desired.z,
          desired.w, target.hasStanceAlignment ? 1 : 0,
          target.hasFullFrameAlignment ? "full-frame"
                                       : (target.hasStanceAlignment
                                              ? "direction-only"
                                              : "none"),
          GetCurrentThreadId());
    }
  }

  ++s_ghostRig.targetApplyCount;
  if (periodicLog) {
    s_ghostRig.lastTargetLogFrame = frame;
    Log("[P3-FK-WRITE] frame=%d sourceFrame=%.6f sampleSeq=%llu "
        "writes=%d missing=%d applyCount=%llu totalRotationWrites=%llu "
        "generation=%llu owner=%p backendGeneration=%llu "
        "feedbackPoseReads=0 ordinaryFkPositionWrites=0 "
        "hipsBindPositionWrites=%llu rootWorldWrites=%llu "
        "setHumanPoseCalls=0 phase5LegTargetsDeferred=1 tid=%lu",
        frame, s_ghostRig.lastSourceFrame,
        (unsigned long long)s_ghostRig.lastSampleSequence, writes, missing,
        (unsigned long long)s_ghostRig.targetApplyCount,
        (unsigned long long)s_ghostRig.targetRotationWriteCount,
        (unsigned long long)generation,
        reinterpret_cast<void *>(owner),
        (unsigned long long)g_motionBackend.Generation(),
        (unsigned long long)s_ghostRig.hipsBindPositionWriteCount,
        (unsigned long long)s_ghostRig.rootWorldWriteCount,
        GetCurrentThreadId());
    Log("[P3-SAMPLE-CADENCE] frame=%d sourceFrame=%.6f "
        "latestSampleSeq=%llu unityApplies=%llu sampleAdvances=%llu "
        "repeatedSamples=%llu maxSourceFrameStep=%.6f playback=%u "
        "workerCadence=%s generation=%llu owner=%p tid=%lu",
        frame, s_ghostRig.lastSourceFrame,
        (unsigned long long)s_ghostRig.lastSampleSequence,
        (unsigned long long)s_ghostRig.cadenceUnityFrames,
        (unsigned long long)s_ghostRig.cadenceSampleAdvances,
        (unsigned long long)s_ghostRig.cadenceRepeatedSamples,
        s_ghostRig.cadenceMaxSourceFrameStep,
        static_cast<unsigned>(DirectVmdRuntime_PublicPlayback()),
        DirectVmdRuntime_WantsRealtimeWorkerCadence() ? "realtime"
                                                       : "idle/hold",
        (unsigned long long)generation, reinterpret_cast<void *>(owner),
        GetCurrentThreadId());
    s_ghostRig.cadenceUnityFrames = 0;
    s_ghostRig.cadenceSampleAdvances = 0;
    s_ghostRig.cadenceRepeatedSamples = 0;
    s_ghostRig.cadenceMaxSourceFrameStep = 0.0;
  }
  return true;
}

static bool GhostRig_ApplyPhase6FingerFk(int frame) {
  if (!GhostRig_RequireMainThread("GhostRig.ApplyPhase6FingerFk", false) ||
      s_ghostRig.state != GhostRigState::Alive ||
      !g_motionBackend.Is(MotionBackend::DirectVmd) ||
      !s_ghostRig.anchorValid)
    return false;

  const uint64_t generation = s_ghostRig.generation;
  const uintptr_t owner = s_ghostRig.ownerCharacter;
  if (s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
          generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) != owner)
    return false;

  bool periodicLog = s_ghostRig.lastFingerLogFrame == INT_MIN;
  if (frame >= 0 && s_ghostRig.lastFingerLogFrame != INT_MIN)
    periodicLog = frame - s_ghostRig.lastFingerLogFrame >= 120;

  int writes = 0;
  int missing = 0;
  for (uint32_t order = 0; order < DIRECT_VMD_PHASE6_FINGER_BONE_COUNT;
       ++order) {
    if (!g_motionBackend.Is(MotionBackend::DirectVmd) ||
        s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
            generation ||
        s_ghostRequestedOwnerId.load(std::memory_order_acquire) != owner) {
      Log("[P6-FINGER-ABORT] frame=%d generation=%llu owner=%p order=%u "
          "reason=ownership-invalidated tid=%lu",
          frame, (unsigned long long)generation,
          reinterpret_cast<void *>(owner), order, GetCurrentThreadId());
      return false;
    }

    const DirectVmdBoneId id = kDirectVmdPhase6FingerBones[order];
    const uint32_t semantic = DirectVmdBoneIndex(id);
    GhostRigTargetBone &target = s_ghostRig.targets[semantic];
    void *targetTransform = GhostRig_GetTargetTransform(id);
    if (!s_ghostRig.sourceReference.controlled[semantic]) continue;
    void *ghostTransform = GhostRig_GetTransform(GhostRig_NodeIndex(id));
    if (!target.resolved || !targetTransform || !ghostTransform ||
        !GhostRig_IsUnityObjectAlive(targetTransform) ||
        !GhostRig_IsUnityObjectAlive(ghostTransform)) {
      ++missing;
      if (!target.missingLogged || periodicLog) {
        Log("[P6-FINGER-MISSING] semantic='%s' frame=%d "
            "generation=%llu owner=%p reason=target-or-ghost-unavailable "
            "action=skip tid=%lu",
            kDirectVmdBoneSpecs[semantic].name, frame,
            (unsigned long long)generation,
            reinterpret_cast<void *>(owner), GetCurrentThreadId());
        target.missingLogged = true;
      }
      continue;
    }

    Quat ghostLocal = {0.0f, 0.0f, 0.0f, 1.0f};
    if (!GhostRig_ReadLocalRotation(ghostTransform, ghostLocal)) {
      ++missing;
      continue;
    }
    const VmdQuaternion ghostLocalRotation =
        DirectVmdNormalizeQuaternion(
            {ghostLocal.x, ghostLocal.y, ghostLocal.z, ghostLocal.w});
    const VmdQuaternion desiredLocal = DirectVmdRetargetLocalRotation(
        ghostLocalRotation,
        s_ghostRig.nodes[GhostRig_NodeIndex(id)].bind.localRotation,
        target.bindLocalRotation);
    if (!GhostRig_WriteLocalRotation(
            targetTransform,
            {desiredLocal.x, desiredLocal.y, desiredLocal.z,
             desiredLocal.w})) {
      ++missing;
      if (!target.missingLogged || periodicLog) {
        Log("[P6-FINGER-MISSING] semantic='%s' frame=%d "
            "generation=%llu owner=%p reason=target-local-write-failed "
            "action=skip tid=%lu",
            kDirectVmdBoneSpecs[semantic].name, frame,
            (unsigned long long)generation,
            reinterpret_cast<void *>(owner), GetCurrentThreadId());
        target.missingLogged = true;
      }
      continue;
    }

    target.lastDesiredWorldRotation = desiredLocal;
    target.lastDesiredFrame = frame;
    target.lastDesiredValid = true;
    ++writes;
    ++s_ghostRig.fingerRotationWriteCount;
    if (periodicLog &&
        (id == DirectVmdBoneId::LeftThumb0 ||
         id == DirectVmdBoneId::LeftIndex1 ||
         id == DirectVmdBoneId::RightThumb0 ||
         id == DirectVmdBoneId::RightIndex1)) {
      Log("[P6-FINGER-BONE] frame=%d sourceFrame=%.6f semantic='%s' "
          "ghostLocalR=(%.7f,%.7f,%.7f,%.7f) "
          "desiredLocalR=(%.7f,%.7f,%.7f,%.7f) "
          "source=ghost-bind-delta rawVmdQuaternionWrite=0 "
          "feedbackPoseReads=0 targetPositionWrites=0 generation=%llu "
          "owner=%p tid=%lu",
          frame, s_ghostRig.lastSourceFrame,
          kDirectVmdBoneSpecs[semantic].name, ghostLocalRotation.x,
          ghostLocalRotation.y, ghostLocalRotation.z,
          ghostLocalRotation.w, desiredLocal.x, desiredLocal.y,
          desiredLocal.z, desiredLocal.w,
          (unsigned long long)generation, reinterpret_cast<void *>(owner),
          GetCurrentThreadId());
    }
  }

  if (periodicLog) {
    s_ghostRig.lastFingerLogFrame = frame;
    Log("[P6-FINGER-WRITE] frame=%d sourceFrame=%.6f sampleSeq=%llu "
        "writes=%d missing=%d totalRotationWrites=%llu "
        "generation=%llu owner=%p mainThreadOnly=1 "
        "rawVmdQuaternionWrite=0 feedbackPoseReads=0 positionWrites=0 "
        "tid=%lu",
        frame, s_ghostRig.lastSourceFrame,
        (unsigned long long)s_ghostRig.lastSampleSequence, writes, missing,
        (unsigned long long)s_ghostRig.fingerRotationWriteCount,
        (unsigned long long)generation, reinterpret_cast<void *>(owner),
        GetCurrentThreadId());
  }
  return true;
}

static bool GhostRig_ApplyPhase6Eyes(int frame, void *ownerRoot) {
  if (!GhostRig_RequireMainThread("GhostRig.ApplyPhase6Eyes", false) ||
      s_ghostRig.state != GhostRigState::Alive || !ownerRoot ||
      !g_motionBackend.Is(MotionBackend::DirectVmd) ||
      !s_ghostRig.anchorValid)
    return false;
  const uint64_t generation = s_ghostRig.generation;
  const uintptr_t owner = s_ghostRig.ownerCharacter;
  if (s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
          generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) != owner)
    return false;

  const bool eyeLookAtOwned =
      (s_ghostRig.sourceReference.controlled[DirectVmdBoneIndex(DirectVmdBoneId::LeftEye)] ||
       s_ghostRig.sourceReference.controlled[DirectVmdBoneIndex(DirectVmdBoneId::RightEye)]) &&
      GhostRig_AcquireEyeLookAtOwnership(ownerRoot);
  bool periodicLog = s_ghostRig.lastEyeLogFrame == INT_MIN;
  if (frame >= 0 && s_ghostRig.lastEyeLogFrame != INT_MIN)
    periodicLog = frame - s_ghostRig.lastEyeLogFrame >= 120;
  const VmdQuaternion anchorRotation =
      DirectVmdNormalizeQuaternion(s_ghostRig.anchorRotation);
  int writes = 0;
  int missing = 0;
  for (uint32_t order = 0; order < DIRECT_VMD_PHASE6_EYE_BONE_COUNT;
       ++order) {
    const DirectVmdBoneId id = kDirectVmdPhase6EyeBones[order];
    const uint32_t semantic = DirectVmdBoneIndex(id);
    GhostRigTargetBone &target = s_ghostRig.targets[semantic];
    void *targetTransform = GhostRig_GetTargetTransform(id);
    if (!s_ghostRig.sourceReference.controlled[semantic]) continue;
    void *ghostTransform = GhostRig_GetTransform(GhostRig_NodeIndex(id));
    if (!target.resolved || !targetTransform || !ghostTransform ||
        !GhostRig_IsUnityObjectAlive(targetTransform) ||
        !GhostRig_IsUnityObjectAlive(ghostTransform)) {
      ++missing;
      if (!target.missingLogged || periodicLog) {
        Log("[P6-EYE-MISSING] semantic='%s' frame=%d generation=%llu "
            "owner=%p action=skip tid=%lu",
            kDirectVmdBoneSpecs[semantic].name, frame,
            (unsigned long long)generation,
            reinterpret_cast<void *>(owner), GetCurrentThreadId());
        target.missingLogged = true;
      }
      continue;
    }
    Quat ghostWorld = {0.0f, 0.0f, 0.0f, 1.0f};
    if (!GhostRig_ReadWorldRotation(ghostTransform, ghostWorld)) {
      ++missing;
      continue;
    }
    const VmdQuaternion ghostWorldRotation =
        DirectVmdNormalizeQuaternion(
            {ghostWorld.x, ghostWorld.y, ghostWorld.z, ghostWorld.w});
    const VmdQuaternion ghostBindWorldRotation =
        DirectVmdQuaternionMultiply(
            anchorRotation,
            s_ghostRig.nodes[GhostRig_NodeIndex(id)].bind.worldRotation);
    const VmdQuaternion targetBindWorldRotation =
        DirectVmdQuaternionMultiply(anchorRotation,
                                    target.bindOwnerRotation);
    const VmdQuaternion desired = DirectVmdRetargetWorldRotation(
        ghostWorldRotation, ghostBindWorldRotation,
        targetBindWorldRotation);
    if (!GhostRig_WriteWorldRotation(
            targetTransform,
            {desired.x, desired.y, desired.z, desired.w})) {
      ++missing;
      continue;
    }
    target.lastDesiredWorldRotation = desired;
    target.lastDesiredFrame = frame;
    target.lastDesiredValid = true;
    ++writes;
    ++s_ghostRig.eyeRotationWriteCount;
    if (periodicLog) {
      Log("[P6-EYE-BONE] frame=%d sourceFrame=%.6f semantic='%s' "
          "ghostWorldR=(%.7f,%.7f,%.7f,%.7f) "
          "desiredWorldR=(%.7f,%.7f,%.7f,%.7f) "
          "source=ghost-world-bind-delta rawVmdQuaternionWrite=0 "
          "feedbackPoseReads=0 generation=%llu owner=%p tid=%lu",
          frame, s_ghostRig.lastSourceFrame,
          kDirectVmdBoneSpecs[semantic].name, ghostWorldRotation.x,
          ghostWorldRotation.y, ghostWorldRotation.z,
          ghostWorldRotation.w, desired.x, desired.y, desired.z,
          desired.w, (unsigned long long)generation,
          reinterpret_cast<void *>(owner), GetCurrentThreadId());
    }
  }
  if (periodicLog) {
    s_ghostRig.lastEyeLogFrame = frame;
    Log("[P6-EYE-WRITE] frame=%d sourceFrame=%.6f sampleSeq=%llu "
        "writes=%d missing=%d eyeLookAtOwned=%d totalRotationWrites=%llu "
        "generation=%llu owner=%p mainThreadOnly=1 "
        "rawVmdQuaternionWrite=0 feedbackPoseReads=0 tid=%lu",
        frame, s_ghostRig.lastSourceFrame,
        (unsigned long long)s_ghostRig.lastSampleSequence, writes, missing,
        eyeLookAtOwned ? 1 : 0,
        (unsigned long long)s_ghostRig.eyeRotationWriteCount,
        (unsigned long long)generation, reinterpret_cast<void *>(owner),
        GetCurrentThreadId());
  }
  return true;
}

static bool GhostRig_ApplyPhase6Twist(int frame) {
  if (!GhostRig_RequireMainThread("GhostRig.ApplyPhase6Twist", false) ||
      s_ghostRig.state != GhostRigState::Alive ||
      !g_motionBackend.Is(MotionBackend::DirectVmd) ||
      !s_ghostRig.anchorValid)
    return false;
  const uint64_t generation = s_ghostRig.generation;
  const uintptr_t owner = s_ghostRig.ownerCharacter;
  if (s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
          generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) != owner)
    return false;

  bool periodicLog = s_ghostRig.lastTwistLogFrame == INT_MIN;
  if (frame >= 0 && s_ghostRig.lastTwistLogFrame != INT_MIN)
    periodicLog = frame - s_ghostRig.lastTwistLogFrame >= 120;
  int writes = 0;
  int missing = 0;
  for (uint32_t channel = 0;
       channel < DIRECT_VMD_PHASE6_TWIST_CHANNEL_COUNT; ++channel) {
    const DirectVmdTwistChannelSpec &spec =
        kDirectVmdPhase6TwistChannels[channel];
    const uint32_t semantic = DirectVmdBoneIndex(spec.control);
    if (!s_ghostRig.sourceReference.controlled[semantic]) continue;
    GhostRigTwistChannelRuntime &runtime =
        s_ghostRig.twistChannels[channel];
    bool channelReady = true;
    for (uint32_t targetIndex = 0;
         targetIndex < DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL;
         ++targetIndex) {
      const GhostRigTwistTargetRuntime &target =
          runtime.targets[targetIndex];
      void *transform = GhostRig_GetHandleTarget(target.transformHandle);
      if (!target.bindAvailable || !target.resolved || !transform ||
          !GhostRig_IsUnityObjectAlive(transform)) {
        channelReady = false;
        break;
      }
    }
    void *ghostTransform = GhostRig_GetTransform(
        GhostRig_NodeIndex(spec.control));
    if (!channelReady || !ghostTransform ||
        !GhostRig_IsUnityObjectAlive(ghostTransform)) {
      ++missing;
      if (periodicLog) {
        Log("[P6-TWIST-MISSING] channel=%u semantic='%s' frame=%d "
            "targetChainReady=%d generation=%llu owner=%p "
            "action=skip-channel tid=%lu",
            channel, kDirectVmdBoneSpecs[semantic].name, frame,
            channelReady ? 1 : 0, (unsigned long long)generation,
            reinterpret_cast<void *>(owner), GetCurrentThreadId());
      }
      continue;
    }

    Quat ghostLocal = {0.0f, 0.0f, 0.0f, 1.0f};
    if (!GhostRig_ReadLocalRotation(ghostTransform, ghostLocal)) {
      ++missing;
      continue;
    }
    const VmdQuaternion currentLocal = DirectVmdNormalizeQuaternion(
        {ghostLocal.x, ghostLocal.y, ghostLocal.z, ghostLocal.w});
    const VmdQuaternion bindLocal =
        s_ghostRig.nodes[GhostRig_NodeIndex(spec.control)]
            .bind.localRotation;
    const VmdQuaternion localDelta = DirectVmdQuaternionMultiply(
        currentLocal, DirectVmdQuaternionInverse(bindLocal));
    const int parentSemantic = kDirectVmdBoneSpecs[semantic].parent;
    const VmdQuaternion parentBindOwner =
        parentSemantic >= 0
            ? s_ghostRig.nodes[1 + parentSemantic].bind.worldRotation
            : VmdQuaternion{0.0f, 0.0f, 0.0f, 1.0f};
    const VmdQuaternion sourceWorldDelta = DirectVmdQuaternionMultiply(
        DirectVmdQuaternionMultiply(parentBindOwner, localDelta),
        DirectVmdQuaternionInverse(parentBindOwner));
    VmdVec3 sourceAxis = {};
    VmdVec3 sourceAxisGame = {};
    float twistAngle = 0.0f;
    if (!GhostRig_SourceDirection(spec.sourceLimb,
                                                   &sourceAxis) ||
        !DirectVmdTryNormalizeVector(
            SourceToGameBasis::ConvertPosition(sourceAxis),
            &sourceAxisGame) ||
        !DirectVmdExtractSignedTwistAngle(
            sourceWorldDelta, sourceAxisGame, &twistAngle)) {
      ++missing;
      if (periodicLog) {
        Log("[P6-TWIST-MISSING] channel=%u semantic='%s' frame=%d "
            "reason=twist-extraction-failed generation=%llu owner=%p "
            "tid=%lu",
            channel, kDirectVmdBoneSpecs[semantic].name, frame,
            (unsigned long long)generation,
            reinterpret_cast<void *>(owner), GetCurrentThreadId());
      }
      continue;
    }

    bool channelWritten = true;
    for (uint32_t targetIndex = 0;
         targetIndex < DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL;
         ++targetIndex) {
      GhostRigTwistTargetRuntime &target = runtime.targets[targetIndex];
      void *transform = GhostRig_GetHandleTarget(target.transformHandle);
      const VmdQuaternion distributed =
          DirectVmdQuaternionFromAxisAngle(target.axisParentLocal,
                                           twistAngle * 0.5f);
      const VmdQuaternion desiredLocal = DirectVmdQuaternionMultiply(
          distributed, target.bindLocalRotation);
      if (!GhostRig_WriteLocalRotation(
              transform,
              {desiredLocal.x, desiredLocal.y, desiredLocal.z,
               desiredLocal.w})) {
        channelWritten = false;
        break;
      }
      ++writes;
      ++runtime.writeCount;
      ++s_ghostRig.twistRotationWriteCount;
    }
    if (!channelWritten) {
      ++missing;
      continue;
    }
    if (periodicLog) {
      Log("[P6-TWIST-CHANNEL] frame=%d sourceFrame=%.6f channel=%u "
          "semantic='%s' signedAngleRad=%.7f distribution=(0.5,0.5) "
          "sourceAxisGame=(%.6f,%.6f,%.6f) "
          "source=ghost-bind-delta target=natural-bind-local-axis "
          "rawVmdQuaternionWrite=0 eulerConversion=0 feedbackPoseReads=0 "
          "generation=%llu owner=%p tid=%lu",
          frame, s_ghostRig.lastSourceFrame, channel,
          kDirectVmdBoneSpecs[semantic].name, twistAngle,
          sourceAxisGame.x, sourceAxisGame.y, sourceAxisGame.z,
          (unsigned long long)generation,
          reinterpret_cast<void *>(owner), GetCurrentThreadId());
    }
  }
  if (periodicLog) {
    s_ghostRig.lastTwistLogFrame = frame;
    Log("[P6-TWIST-WRITE] frame=%d sourceFrame=%.6f sampleSeq=%llu "
        "writes=%d missingChannels=%d totalRotationWrites=%llu "
        "generation=%llu owner=%p mainThreadOnly=1 tid=%lu",
        frame, s_ghostRig.lastSourceFrame,
        (unsigned long long)s_ghostRig.lastSampleSequence, writes, missing,
        (unsigned long long)s_ghostRig.twistRotationWriteCount,
        (unsigned long long)generation, reinterpret_cast<void *>(owner),
        GetCurrentThreadId());
  }
  return true;
}

static DirectVmdBoneId GhostRig_FootIkParentId(DirectVmdLegSide side) {
  return side == DirectVmdLegSide::Left
             ? DirectVmdBoneId::LeftFootIkParent
             : DirectVmdBoneId::RightFootIkParent;
}

static DirectVmdBoneId GhostRig_FootIkId(DirectVmdLegSide side) {
  return side == DirectVmdLegSide::Left ? DirectVmdBoneId::LeftFootIk
                                        : DirectVmdBoneId::RightFootIk;
}

static DirectVmdBoneId GhostRig_ToeIkId(DirectVmdLegSide side) {
  return side == DirectVmdLegSide::Left ? DirectVmdBoneId::LeftToeIk
                                        : DirectVmdBoneId::RightToeIk;
}

static float GhostRig_LegMaximumReach(DirectVmdLegSide side) {
  return side == DirectVmdLegSide::Left ? s_ghostRig.leftLegLength
                                        : s_ghostRig.rightLegLength;
}

static bool GhostRig_EvaluateLegTargetWorldRotation(
    DirectVmdBoneId id, VmdQuaternion *desired,
    VmdQuaternion *ghostWorldResult = nullptr) {
  if (!desired || !s_ghostRig.anchorValid)
    return false;
  const uint32_t semantic = DirectVmdBoneIndex(id);
  if (semantic >= DIRECT_VMD_BONE_COUNT)
    return false;
  GhostRigTargetBone &target = s_ghostRig.targets[semantic];
  void *targetTransform = GhostRig_GetTargetTransform(id);
  void *ghostTransform = GhostRig_GetTransform(GhostRig_NodeIndex(id));
  if (!target.resolved || !targetTransform || !ghostTransform ||
      !GhostRig_IsUnityObjectAlive(targetTransform) ||
      !GhostRig_IsUnityObjectAlive(ghostTransform))
    return false;

  Quat ghostWorld = {0.0f, 0.0f, 0.0f, 1.0f};
  if (!GhostRig_ReadWorldRotation(ghostTransform, ghostWorld))
    return false;
  const VmdQuaternion anchorRotation =
      DirectVmdNormalizeQuaternion(s_ghostRig.anchorRotation);
  const VmdQuaternion ghostWorldRotation =
      DirectVmdNormalizeQuaternion(
          {ghostWorld.x, ghostWorld.y, ghostWorld.z, ghostWorld.w});
  const VmdQuaternion ghostBindWorldRotation =
      DirectVmdQuaternionMultiply(
          anchorRotation,
          s_ghostRig.nodes[GhostRig_NodeIndex(id)].bind.worldRotation);
  const VmdQuaternion targetBindWorldRotation =
      DirectVmdQuaternionMultiply(anchorRotation,
                                  target.bindOwnerRotation);
  *desired = target.hasStanceAlignment
      ? DirectVmdRetargetWorldRotationFromSourceStance(
          ghostWorldRotation, ghostBindWorldRotation, targetBindWorldRotation,
          DirectVmdQuaternionMultiply(
              DirectVmdQuaternionMultiply(anchorRotation, target.sourceToTargetOwnerAlignment),
              DirectVmdQuaternionInverse(anchorRotation)))
      : DirectVmdRetargetWorldRotation(
          ghostWorldRotation, ghostBindWorldRotation, targetBindWorldRotation);
  if (ghostWorldResult)
    *ghostWorldResult = ghostWorldRotation;
  return true;
}

static bool GhostRig_EvaluateControlToTargetWorldRotation(
    DirectVmdBoneId controlId, DirectVmdBoneId targetId,
    VmdQuaternion *desired) {
  if (!desired || !s_ghostRig.anchorValid)
    return false;
  const uint32_t targetSemantic = DirectVmdBoneIndex(targetId);
  GhostRigTargetBone &target = s_ghostRig.targets[targetSemantic];
  void *targetTransform = GhostRig_GetTargetTransform(targetId);
  void *controlTransform =
      GhostRig_GetTransform(GhostRig_NodeIndex(controlId));
  if (!target.resolved || !targetTransform || !controlTransform ||
      !GhostRig_IsUnityObjectAlive(targetTransform) ||
      !GhostRig_IsUnityObjectAlive(controlTransform))
    return false;
  Quat controlWorld = {0.0f, 0.0f, 0.0f, 1.0f};
  if (!GhostRig_ReadWorldRotation(controlTransform, controlWorld))
    return false;
  const VmdQuaternion anchorRotation =
      DirectVmdNormalizeQuaternion(s_ghostRig.anchorRotation);
  const VmdQuaternion ghostBindWorldRotation =
      DirectVmdQuaternionMultiply(
          anchorRotation,
          s_ghostRig.nodes[GhostRig_NodeIndex(controlId)].bind.worldRotation);
  const VmdQuaternion targetBindWorldRotation =
      DirectVmdQuaternionMultiply(anchorRotation,
                                  target.bindOwnerRotation);
  *desired = DirectVmdRetargetWorldRotation(
      DirectVmdNormalizeQuaternion(
          {controlWorld.x, controlWorld.y, controlWorld.z,
           controlWorld.w}),
      ghostBindWorldRotation, targetBindWorldRotation);
  return true;
}

static bool GhostRig_WritePhase5LegFkBone(
    DirectVmdLegSide side, DirectVmdBoneId id, int frame,
    const char *ownershipRole) {
  if (!s_ghostRig.sourceReference.controlled[DirectVmdBoneIndex(id)])
    return true;
  if (s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
          s_ghostRig.generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) !=
          s_ghostRig.ownerCharacter)
    return false;
  VmdQuaternion desired = {0.0f, 0.0f, 0.0f, 1.0f};
  VmdQuaternion ghostWorld = {0.0f, 0.0f, 0.0f, 1.0f};
  GhostRigLegRuntime &leg = GhostRig_LegRuntime(side);
  GhostRigTargetBone &target =
      s_ghostRig.targets[DirectVmdBoneIndex(id)];
  void *targetTransform = GhostRig_GetTargetTransform(id);
  if (!GhostRig_EvaluateLegTargetWorldRotation(
          id, &desired, &ghostWorld) || !targetTransform ||
      !GhostRig_WriteWorldRotation(
          targetTransform,
          {desired.x, desired.y, desired.z, desired.w})) {
    if (!target.missingLogged || frame == s_ghostRig.lastLegLogFrame) {
      Log("[P5-LEG-FK-MISSING] side=%s semantic='%s' frame=%d "
          "generation=%llu owner=%p role=%s action=skip tid=%lu",
          GhostRig_LegSideName(side),
          kDirectVmdBoneSpecs[DirectVmdBoneIndex(id)].name, frame,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          ownershipRole ? ownershipRole : "unknown", GetCurrentThreadId());
      target.missingLogged = true;
    }
    return false;
  }

  target.lastDesiredWorldRotation = desired;
  target.lastDesiredFrame = frame;
  target.lastDesiredValid = true;
  ++leg.fkRotationWriteCount;
  ++s_ghostRig.targetRotationWriteCount;
  return true;
}

static void GhostRig_LogFinalIkSolverClass(DirectVmdLegSide side,
                                           void *solver,
                                           uint32_t handle) {
  const char *className = "unknown";
  const char *classNamespace = "unknown";
  if (solver && il2cpp_object_get_class) {
    void *klass = il2cpp_object_get_class(solver);
    if (klass) {
      const char *name = il2cpp_class_get_name(klass);
      const char *nameSpace = il2cpp_class_get_namespace(klass);
      if (name)
        className = name;
      if (nameSpace)
        classNamespace = nameSpace;
    }
  }
  Log("[P5-FINALIK-MAP] side=%s solver=%p handle=%u class='%s.%s' "
      "generation=%llu owner=%p tid=%lu",
      GhostRig_LegSideName(side), solver, handle, classNamespace,
      className, (unsigned long long)s_ghostRig.generation,
      reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
      GetCurrentThreadId());
}

static void *GhostRig_ReadFinalIkPelvisReference(void *bipedIK,
                                                 void *pelvisConstraints,
                                                 const char **source) {
  static const char *kReferences[] = {"references"};
  static const char *kPelvis[] = {"pelvis"};
  static const char *kTransform[] = {"transform"};
  *source = "unresolved";
  void *pelvis = nullptr;
  if (!il2cpp_object_get_class)
    return nullptr;
  __try {
    const char *field = nullptr;
    void *bipedClass = il2cpp_object_get_class(bipedIK);
    const int referencesOffset =
        FindFieldInHierarchy(bipedClass, kReferences, 1, &field);
    void *references = referencesOffset > 0
        ? *reinterpret_cast<void **>((char *)bipedIK + referencesOffset)
        : nullptr;
    if (references) {
      const int pelvisOffset = FindFieldInHierarchy(
          il2cpp_object_get_class(references), kPelvis, 1, &field);
      if (pelvisOffset > 0)
        pelvis = *reinterpret_cast<void **>(
            (char *)references + pelvisOffset);
      if (pelvis)
        *source = "BipedIK.references.pelvis";
    }
    if (!pelvis && pelvisConstraints) {
      const int transformOffset = FindFieldInHierarchy(
          il2cpp_object_get_class(pelvisConstraints), kTransform, 1,
          &field);
      if (transformOffset > 0)
        pelvis = *reinterpret_cast<void **>(
            (char *)pelvisConstraints + transformOffset);
      if (pelvis)
        *source = "BipedIK.solvers.pelvis.transform";
    }
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    pelvis = nullptr;
    *source = "read-fault";
  }
  return pelvis;
}

static bool GhostRig_CapturePelvisNodeOwnership(void *bipedIK,
                                                void *pelvisConstraints) {
  GhostRigPelvisNodeRuntime &node = s_ghostRig.pelvisNode;
  if (node.handle && il2cpp_gchandle_free)
    il2cpp_gchandle_free(node.handle);
  node = GhostRigPelvisNodeRuntime();
  node.lastCorrectionFrame = INT_MIN;
  node.lastLogFrame = INT_MIN;

  const char *source = "unresolved";
  void *pelvis = GhostRig_ReadFinalIkPelvisReference(
      bipedIK, pelvisConstraints, &source);
  void *hips = GhostRig_GetTargetTransform(DirectVmdBoneId::LowerBody);
  void *ownerRoot = GhostRig_GetOwnerRoot(s_ghostRig.ownerCharacter);
  char name[96] = {};
  if (pelvis)
    SafeGetBoneName(pelvis, name, sizeof(name));

  const char *role = nullptr;
  if (!pelvis || !GhostRig_IsUnityObjectAlive(pelvis))
    role = "unresolved";
  else if (hips && GhostRig_SameUnityObject(pelvis, hips))
    role = "hips";
  else if (!ownerRoot || GhostRig_SameUnityObject(pelvis, ownerRoot) ||
           !GhostRig_IsComponentUnderOwner(pelvis, ownerRoot))
    role = "root-or-outside-owner";
  if (role) {
    Log("[P5-PELVIS-NODE] event=capture role=%s node='%s' source=%s "
        "owned=0 generation=%llu owner=%p tid=%lu",
        role, name, source, (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    return true;
  }

  VmdVec3 bindLocal = {};
  bool fromAvatar = false;
  char status[160] = {};
  std::map<std::string, GhostSkeletonBoneRecord> records;
  std::map<std::string, int> nameCounts;
  if (name[0] &&
      GhostRig_ReadSkeletonMetadata(&records, &nameCounts, status,
                                    sizeof(status))) {
    const auto count = nameCounts.find(name);
    const auto record = records.find(name);
    if (count != nameCounts.end() && count->second == 1 &&
        record != records.end()) {
      bindLocal = record->second.position;
      fromAvatar = true;
    }
  }
  Vec3 entryValue = {};
  const bool entryOk = GhostRig_ReadLocalPosition(pelvis, entryValue) &&
                       DirectVmdFinite(entryValue.x) &&
                       DirectVmdFinite(entryValue.y) &&
                       DirectVmdFinite(entryValue.z);
  const VmdVec3 entryLocal = {entryValue.x, entryValue.y, entryValue.z};
  if (!fromAvatar && !entryOk) {
    Log("[P5-PELVIS-NODE] event=capture role=intermediate node='%s' "
        "source=%s owned=0 reason=no-bind-or-entry-value "
        "action=suppress-biped generation=%llu owner=%p tid=%lu",
        name, source, (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    return false;
  }
  node.handle = il2cpp_gchandle_new(pelvis, false);
  if (!node.handle) {
    Log("[P5-PELVIS-NODE] event=capture role=intermediate node='%s' "
        "source=%s owned=0 reason=gchandle-failed action=suppress-biped "
        "generation=%llu owner=%p tid=%lu",
        name, source, (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    return false;
  }
  node.bindLocalPosition = fromAvatar ? bindLocal : entryLocal;
  node.bindFromAvatar = fromAvatar;
  node.restoreLocalPosition = entryLocal;
  node.restoreValid = entryOk;
  strncpy_s(node.name, sizeof(node.name), name, _TRUNCATE);
  Log("[P5-PELVIS-NODE] event=capture role=intermediate node='%s' "
      "source=%s owned=1 handle=%u bindSource=%s "
      "bindLocalP=(%.6f,%.6f,%.6f) entryLocalP=(%.6f,%.6f,%.6f) "
      "entryValid=%d metadataStatus='%s' writesLocalPositionOnly=1 "
      "generation=%llu owner=%p mainThreadOnly=1 tid=%lu",
      node.name, source, node.handle,
      fromAvatar ? "Avatar.humanDescription.skeleton"
                 : "entry-local-fallback",
      node.bindLocalPosition.x, node.bindLocalPosition.y,
      node.bindLocalPosition.z, entryLocal.x, entryLocal.y, entryLocal.z,
      entryOk ? 1 : 0, status, (unsigned long long)s_ghostRig.generation,
      reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
      GetCurrentThreadId());
  return true;
}

static bool GhostRig_StabilizePelvisNodeBindPosition(int frame,
                                                     const char *stage) {
  GhostRigPelvisNodeRuntime &node = s_ghostRig.pelvisNode;
  if (!node.handle)
    return true;
  void *transform = GhostRig_GetHandleTarget(node.handle);
  const bool alive = transform && GhostRig_IsUnityObjectAlive(transform);
  Vec3 beforeValue = {};
  const bool measured =
      alive && GhostRig_ReadLocalPosition(transform, beforeValue);
  const VmdVec3 before = measured
      ? VmdVec3{beforeValue.x, beforeValue.y, beforeValue.z}
      : node.bindLocalPosition;
  const float correctionDistance =
      measured ? DirectVmdLength(DirectVmdSub(before,
                                              node.bindLocalPosition))
               : -1.0f;
  const bool written = alive &&
      GhostRig_WriteLocalPosition(transform, node.bindLocalPosition);
  if (written)
    ++node.writeCount;

  const bool postFinalIk =
      stage && strcmp(stage, "post-finalik") == 0;
  const bool significant = measured && correctionDistance > 1.0e-4f;
  if (postFinalIk) {
    node.lastCorrectionDistance =
        measured && written ? correctionDistance : 0.0f;
    node.lastCorrectionFrame = frame;
    if (significant && written)
      ++node.postCorrectionCount;
  }

  const bool periodic =
      frame < 0 || node.lastLogFrame == INT_MIN ||
      frame - node.lastLogFrame >= 120;
  if (!written || (significant && periodic)) {
    node.lastLogFrame = frame;
    Log("[P5-PELVIS-NODE] event=stabilize stage=%s unityFrame=%d "
        "vmdFrame=%.6f node='%s' beforeLocalP=(%.6f,%.6f,%.6f) "
        "bindLocalP=(%.6f,%.6f,%.6f) localCorrectionDistance=%.6f "
        "measured=%d written=%d alive=%d totalWrites=%llu "
        "postCorrections=%llu currentPoseTargetInput=0 "
        "generation=%llu owner=%p tid=%lu",
        stage ? stage : "unknown", frame, s_ghostRig.lastSourceFrame,
        node.name, before.x, before.y, before.z,
        node.bindLocalPosition.x, node.bindLocalPosition.y,
        node.bindLocalPosition.z, correctionDistance, measured ? 1 : 0,
        written ? 1 : 0, alive ? 1 : 0,
        (unsigned long long)node.writeCount,
        (unsigned long long)node.postCorrectionCount,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
  return written;
}

static bool GhostRig_ResolvePhase5FinalIkSolvers(void *bipedIK) {
  if (!bipedIK || !il2cpp_gchandle_new)
    return false;
  if (!g_finalIkSolverLayoutOk) {
    static bool s_logged = false;
    if (!s_logged) {
      s_logged = true;
      Log("[P5-FINALIK-MAP] failed reason=finalik-layout-unconfirmed "
          "action=suppress-biped generation=%llu owner=%p tid=%lu",
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    }
    return false;
  }
  void *retained = GhostRig_GetFinalIkBiped();
  const bool hasAnyLegSolver =
      GhostRig_LegRuntime(DirectVmdLegSide::Left).solverHandle != 0 ||
      GhostRig_LegRuntime(DirectVmdLegSide::Right).solverHandle != 0;
  if (retained == bipedIK && s_ghostRig.finalIkBipedHandle &&
      hasAnyLegSolver)
    return true;

  if (s_ghostRig.finalIkBipedHandle ||
      GhostRig_LegRuntime(DirectVmdLegSide::Left).solverHandle ||
      GhostRig_LegRuntime(DirectVmdLegSide::Right).solverHandle)
    GhostRig_ClearPhase5FinalIkOwnership(GhostRigCleanupReason::Recapture);

  void *solvers = nullptr;
  void *leftSolver = nullptr;
  void *rightSolver = nullptr;
  void *auxObjects[GHOST_AUX_SOLVER_COUNT] = {};
  __try {
    solvers = *reinterpret_cast<void **>(
        (char *)bipedIK + OFF_BIPEDIK_SOLVERS);
    if (solvers) {
      leftSolver = *reinterpret_cast<void **>(
          (char *)solvers + OFF_SOLVERS_LEFT_FOOT);
      rightSolver = *reinterpret_cast<void **>(
          (char *)solvers + OFF_SOLVERS_RIGHT_FOOT);
      auxObjects[static_cast<uint32_t>(GhostRigAuxSolverId::LeftHand)] =
          *reinterpret_cast<void **>(
              (char *)solvers + OFF_SOLVERS_LEFT_HAND);
      auxObjects[static_cast<uint32_t>(GhostRigAuxSolverId::RightHand)] =
          *reinterpret_cast<void **>(
              (char *)solvers + OFF_SOLVERS_RIGHT_HAND);
      auxObjects[static_cast<uint32_t>(GhostRigAuxSolverId::Spine)] =
          *reinterpret_cast<void **>((char *)solvers + OFF_SOLVERS_SPINE);
      auxObjects[static_cast<uint32_t>(GhostRigAuxSolverId::LookAt)] =
          *reinterpret_cast<void **>((char *)solvers + OFF_SOLVERS_LOOKAT);
      auxObjects[static_cast<uint32_t>(GhostRigAuxSolverId::Aim)] =
          *reinterpret_cast<void **>((char *)solvers + OFF_SOLVERS_AIM);
      auxObjects[static_cast<uint32_t>(GhostRigAuxSolverId::Pelvis)] =
          *reinterpret_cast<void **>((char *)solvers + OFF_SOLVERS_PELVIS);
    }
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    solvers = nullptr;
    leftSolver = nullptr;
    rightSolver = nullptr;
    memset(auxObjects, 0, sizeof(auxObjects));
  }
  if (!solvers) {
    Log("[P5-FINALIK-MAP] failed reason=solver-container-null "
        "biped=%p generation=%llu owner=%p tid=%lu",
        bipedIK, (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    return false;
  }

  s_ghostRig.finalIkBipedHandle =
      il2cpp_gchandle_new(bipedIK, false);
  if (!s_ghostRig.finalIkBipedHandle)
    return false;

  const int auxPositionOffsets[GHOST_AUX_SOLVER_COUNT] = {
      OFF_IKSOLVER_IKPOS_WEIGHT, OFF_IKSOLVER_IKPOS_WEIGHT,
      OFF_IKSOLVER_IKPOS_WEIGHT, OFF_IKSOLVER_IKPOS_WEIGHT,
      OFF_IKSOLVER_IKPOS_WEIGHT, OFF_BIPED_PELVIS_POS_WEIGHT};
  const int auxRotationOffsets[GHOST_AUX_SOLVER_COUNT] = {
      OFF_IKSOLVER_IKROT_WEIGHT, OFF_IKSOLVER_IKROT_WEIGHT,
      0, 0, 0, OFF_BIPED_PELVIS_ROT_WEIGHT};
  int auxiliaryResolved = 0;
  bool pelvisIsolationReady = false;
  for (uint32_t index = 0; index < GHOST_AUX_SOLVER_COUNT; ++index) {
    void *solver = auxObjects[index];
    if (!solver)
      continue;
    GhostRigAuxSolverRuntime &aux = s_ghostRig.auxSolvers[index];
    aux.handle = il2cpp_gchandle_new(solver, false);
    if (!aux.handle)
      continue;
    aux.positionWeightOffset = auxPositionOffsets[index];
    aux.rotationWeightOffset = auxRotationOffsets[index];
    if (index == static_cast<uint32_t>(GhostRigAuxSolverId::Pelvis)) {
      aux.positionOffsetOffset = OFF_BIPED_PELVIS_POS_OFFSET_X;
      aux.rotationOffsetOffset = OFF_BIPED_PELVIS_ROT_OFFSET_X;
    }
    __try {
      aux.savedPositionWeight = *reinterpret_cast<float *>(
          (char *)solver + aux.positionWeightOffset);
      aux.savedPositionValid =
          DirectVmdFinite(aux.savedPositionWeight);
      if (aux.rotationWeightOffset > 0) {
        aux.savedRotationWeight = *reinterpret_cast<float *>(
            (char *)solver + aux.rotationWeightOffset);
        aux.savedRotationValid =
            DirectVmdFinite(aux.savedRotationWeight);
      }
      if (aux.positionOffsetOffset > 0) {
        aux.savedPositionOffset = {
            *reinterpret_cast<float *>(
                (char *)solver + aux.positionOffsetOffset + 0),
            *reinterpret_cast<float *>(
                (char *)solver + aux.positionOffsetOffset + 4),
            *reinterpret_cast<float *>(
                (char *)solver + aux.positionOffsetOffset + 8)};
        aux.savedPositionOffsetValid =
            DirectVmdFinite(aux.savedPositionOffset.x) &&
            DirectVmdFinite(aux.savedPositionOffset.y) &&
            DirectVmdFinite(aux.savedPositionOffset.z);
      }
      if (aux.rotationOffsetOffset > 0) {
        aux.savedRotationOffset = {
            *reinterpret_cast<float *>(
                (char *)solver + aux.rotationOffsetOffset + 0),
            *reinterpret_cast<float *>(
                (char *)solver + aux.rotationOffsetOffset + 4),
            *reinterpret_cast<float *>(
                (char *)solver + aux.rotationOffsetOffset + 8)};
        aux.savedRotationOffsetValid =
            DirectVmdFinite(aux.savedRotationOffset.x) &&
            DirectVmdFinite(aux.savedRotationOffset.y) &&
            DirectVmdFinite(aux.savedRotationOffset.z);
      }
    } __except (EXCEPTION_EXECUTE_HANDLER) {
      aux.savedPositionValid = false;
      aux.savedRotationValid = false;
      aux.savedPositionOffsetValid = false;
      aux.savedRotationOffsetValid = false;
    }
    const bool zeroed = GhostRig_ZeroAuxSolverWeights(aux, solver);
    if (index == static_cast<uint32_t>(GhostRigAuxSolverId::Pelvis)) {
      pelvisIsolationReady =
          zeroed && aux.savedPositionValid && aux.savedRotationValid &&
          aux.savedPositionOffsetValid && aux.savedRotationOffsetValid;
    }
    void *klass = il2cpp_object_get_class
                      ? il2cpp_object_get_class(solver)
                      : nullptr;
    const char *className =
        klass && il2cpp_class_get_name
            ? il2cpp_class_get_name(klass)
            : "unknown";
    Log("[P5-FINALIK-AUX] role=%s solver=%p handle=%u class='%s' "
        "savedPositionWeight=%.6f savedRotationWeight=%.6f "
        "savedPositionOffset=(%.6f,%.6f,%.6f) "
        "savedRotationOffset=(%.6f,%.6f,%.6f) "
        "positionSaved=%d rotationSaved=%d positionOffsetSaved=%d "
        "rotationOffsetSaved=%d forcedZero=%d additiveOffsetsForcedZero=%d "
        "restoreOnCleanup=1 generation=%llu owner=%p tid=%lu",
        GhostRig_AuxSolverName(
            static_cast<GhostRigAuxSolverId>(index)),
        solver, aux.handle, className ? className : "unknown",
        aux.savedPositionWeight, aux.savedRotationWeight,
        aux.savedPositionOffset.x, aux.savedPositionOffset.y,
        aux.savedPositionOffset.z, aux.savedRotationOffset.x,
        aux.savedRotationOffset.y, aux.savedRotationOffset.z,
        aux.savedPositionValid ? 1 : 0,
        aux.savedRotationValid ? 1 : 0,
        aux.savedPositionOffsetValid ? 1 : 0,
        aux.savedRotationOffsetValid ? 1 : 0, zeroed ? 1 : 0,
        aux.positionOffsetOffset > 0 || aux.rotationOffsetOffset > 0
            ? (zeroed ? 1 : 0)
            : 0,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    ++auxiliaryResolved;
  }

  if (!pelvisIsolationReady) {
    Log("[P5-FINALIK-AUX] role=pelvis isolationReady=0 "
        "reason=weights-or-additive-offsets-not-readable "
        "action=suppress-biped generation=%llu owner=%p tid=%lu",
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    GhostRig_ClearPhase5FinalIkOwnership(
        GhostRigCleanupReason::Recapture);
    return false;
  }

  void *solverObjects[DIRECT_VMD_LEG_SIDE_COUNT] = {
      leftSolver, rightSolver};
  int resolved = 0;
  bool isolationFailed = false;
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT; ++index) {
    const DirectVmdLegSide side =
        static_cast<DirectVmdLegSide>(index);
    GhostRigLegRuntime &leg = s_ghostRig.legs[index];
    void *solver = solverObjects[index];
    if (!solver) {
      Log("[P5-FINALIK-MAP] side=%s failed reason=foot-solver-null "
          "generation=%llu owner=%p tid=%lu",
          GhostRig_LegSideName(side),
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
      continue;
    }
    leg.solverHandle = il2cpp_gchandle_new(solver, false);
    if (!leg.solverHandle)
      continue;
    leg.kneeReferenceReady = false;
    if (il2cpp_object_get_class) {
      __try {
        static const char *normalName[] = {"bendNormal"};
        static const char *backupName[] = {"_bendNormal"};
        static const char *localName[] = {"defaultLocalBendNormal"};
        const char *matched = nullptr;
        void *klass = il2cpp_object_get_class(solver);
        leg.kneeNormalOffset = FindFieldInHierarchy(klass, normalName, 1, &matched);
        leg.kneeBackupOffset = FindFieldInHierarchy(klass, backupName, 1, &matched);
        void *bone = *reinterpret_cast<void **>((char *)solver + OFF_IKTRIG_BONE1);
        const int localOffset = bone ? FindFieldInHierarchy(
            il2cpp_object_get_class(bone), localName, 1, &matched) : -1;
        if (leg.kneeNormalOffset > 0 && leg.kneeBackupOffset > 0 && localOffset > 0) {
          VmdVec3 normal = *reinterpret_cast<VmdVec3 *>((char *)bone + localOffset);
          leg.kneeReferenceReady = DirectVmdTryNormalizeVector(normal, &leg.kneeDefaultLocalNormal);
        }
      } __except (EXCEPTION_EXECUTE_HANDLER) {
        leg.kneeReferenceReady = false;
      }
    }
    Log("[P5-KNEE-BIND] side=%s ready=%d input=Animation-local-normal offsets=%d/%d",
        GhostRig_LegSideName(side), leg.kneeReferenceReady ? 1 : 0,
        leg.kneeNormalOffset, leg.kneeBackupOffset);
    __try {
      leg.savedBendModifier = *reinterpret_cast<int *>(
          (char *)solver + OFF_IKLIMB_BEND_MODIFIER);
      leg.savedBendWeight = *reinterpret_cast<float *>(
          (char *)solver + OFF_IKLIMB_BEND_WEIGHT);
      leg.savedBendStateValid =
          DirectVmdFinite(leg.savedBendWeight);
    } __except (EXCEPTION_EXECUTE_HANDLER) {
      leg.savedBendStateValid = false;
    }
    if (!GhostRig_SaveAndIsolateLegSolverExternalOwnership(side,
                                                            solver)) {
      isolationFailed = true;
      Log("[P5-FINALIK-MAP] side=%s failed "
          "reason=external-ownership-isolation-failed "
          "action=suppress-biped generation=%llu owner=%p tid=%lu",
          GhostRig_LegSideName(side),
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
      continue;
    }
    GhostRig_LogFinalIkSolverClass(side, solver, leg.solverHandle);
    ++resolved;
  }

  if (isolationFailed) {
    GhostRig_ClearPhase5FinalIkOwnership(
        GhostRigCleanupReason::Recapture);
    return false;
  }

  if (!GhostRig_CapturePelvisNodeOwnership(
          bipedIK,
          auxObjects[static_cast<uint32_t>(GhostRigAuxSolverId::Pelvis)])) {
    GhostRig_ClearPhase5FinalIkOwnership(
        GhostRigCleanupReason::Recapture);
    return false;
  }

  Log("[P5-FINALIK-MAP] complete biped=%p handle=%u resolvedLegs=%d "
      "auxiliaryResolved=%d externalFootOwnershipIsolated=%d "
      "generation=%llu owner=%p mainThreadOnly=1 tid=%lu",
      bipedIK, s_ghostRig.finalIkBipedHandle, resolved,
      auxiliaryResolved, resolved,
      (unsigned long long)s_ghostRig.generation,
      reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
      GetCurrentThreadId());
  return resolved > 0;
}

static bool GhostRig_ReadGhostWorldPose(
    DirectVmdBoneId id, VmdVec3 *position, VmdQuaternion *rotation) {
  void *transform = GhostRig_GetTransform(GhostRig_NodeIndex(id));
  if (!transform || !GhostRig_IsUnityObjectAlive(transform))
    return false;
  Vec3 worldPosition = {};
  Quat worldRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  if (position && !GhostRig_ReadWorldPosition(transform, worldPosition))
    return false;
  if (rotation && !GhostRig_ReadWorldRotation(transform, worldRotation))
    return false;
  if (position)
    *position = {worldPosition.x, worldPosition.y, worldPosition.z};
  if (rotation)
    *rotation = DirectVmdNormalizeQuaternion(
        {worldRotation.x, worldRotation.y, worldRotation.z,
         worldRotation.w});
  return true;
}

static VmdVec3 GhostRig_EvaluateBendDirection(
    VmdVec3 hip, VmdVec3 knee, VmdVec3 solverTarget) {
  VmdVec3 targetDirection = {};
  if (!DirectVmdTryNormalizeVector(
          DirectVmdSub(solverTarget, hip), &targetDirection))
    return {0.0f, 0.0f, 0.0f};
  const VmdVec3 hipToKnee = DirectVmdSub(knee, hip);
  const VmdVec3 projected = DirectVmdSub(
      hipToKnee,
      DirectVmdScale(targetDirection,
                     DirectVmdDot(hipToKnee, targetDirection)));
  VmdVec3 bendDirection = {};
  DirectVmdTryNormalizeVector(projected, &bendDirection);
  return bendDirection;
}

static bool GhostRig_EvaluateExpectedLowerBodyWorldPosition(
    VmdVec3 *lowerPosition) {
  if (!lowerPosition || !s_ghostRig.anchorValid)
    return false;
  const DirectVmdBindNodePod &lowerBind =
      s_ghostRig.nodes[GhostRig_NodeIndex(
          DirectVmdBoneId::LowerBody)].bind;
  const VmdVec3 rootPosition = s_ghostRig.lastDesiredRootValid
                                   ? s_ghostRig.lastDesiredRootPosition
                                   : s_ghostRig.anchorPosition;
  const VmdQuaternion rootRotation = DirectVmdNormalizeQuaternion(
      s_ghostRig.lastDesiredRootValid
          ? s_ghostRig.lastDesiredRootRotation
          : s_ghostRig.anchorRotation);
  *lowerPosition = DirectVmdAdd(
      rootPosition,
      DirectVmdRotateVector(rootRotation, lowerBind.worldPosition));
  return DirectVmdFinite(lowerPosition->x) &&
         DirectVmdFinite(lowerPosition->y) &&
         DirectVmdFinite(lowerPosition->z);
}

static bool GhostRig_StabilizeTargetHipsBindPosition(
    int frame, const char *stage) {
  if (!s_ghostRig.sourceReference.controlled[DirectVmdBoneIndex(DirectVmdBoneId::LowerBody)])
    return true;
  if (!s_ghostRig.anchorValid ||
      s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
          s_ghostRig.generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) !=
          s_ghostRig.ownerCharacter)
    return false;
  void *hips = GhostRig_GetTargetTransform(DirectVmdBoneId::LowerBody);
  VmdVec3 expected = {};
  if (!hips || !GhostRig_IsUnityObjectAlive(hips) ||
      !GhostRig_EvaluateExpectedLowerBodyWorldPosition(&expected))
    return false;

  Vec3 measuredValue = {};
  const bool measured =
      GhostRig_ReadWorldPosition(hips, measuredValue);
  const VmdVec3 before = measured
                             ? VmdVec3{measuredValue.x, measuredValue.y,
                                       measuredValue.z}
                             : expected;
  const float correctionDistance =
      measured
          ? DirectVmdLength(DirectVmdSub(before, expected))
          : -1.0f;
  const bool written = GhostRig_WriteWorldPosition(
      hips, {expected.x, expected.y, expected.z});
  if (written)
    ++s_ghostRig.hipsBindPositionWriteCount;

  const bool postFinalIk =
      stage && strcmp(stage, "post-finalik") == 0;
  const bool significant =
      measured && correctionDistance > 1.0e-4f;
  if (postFinalIk && significant && written)
    ++s_ghostRig.hipsPostCorrectionCount;
  s_ghostRig.lastHipsPositionBeforeCorrection = before;
  s_ghostRig.lastHipsCorrectionDistance = correctionDistance;
  s_ghostRig.lastHipsCorrectionFrame = frame;

  const bool periodic =
      frame < 0 || s_ghostRig.lastHipsCorrectionLogFrame == INT_MIN ||
      frame - s_ghostRig.lastHipsCorrectionLogFrame >= 120;
  if (!written || (significant && periodic)) {
    s_ghostRig.lastHipsCorrectionLogFrame = frame;
    Log("[P5-HIPS-STABILIZE] stage=%s unityFrame=%d vmdFrame=%.6f "
        "before=(%.6f,%.6f,%.6f) expected=(%.6f,%.6f,%.6f) "
        "correctionDistance=%.6f measured=%d written=%d "
        "totalWrites=%llu postCorrections=%llu "
        "source=Avatar-natural-bind currentPoseTargetInput=0 "
        "firstKeySubtraction=0 generation=%llu owner=%p tid=%lu",
        stage ? stage : "unknown", frame, s_ghostRig.lastSourceFrame,
        before.x, before.y, before.z, expected.x, expected.y,
        expected.z, correctionDistance, measured ? 1 : 0,
        written ? 1 : 0,
        (unsigned long long)s_ghostRig.hipsBindPositionWriteCount,
        (unsigned long long)s_ghostRig.hipsPostCorrectionCount,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
  return written;
}

static bool GhostRig_EvaluateExpectedLegSeedPositions(
    DirectVmdLegSide side, int frame, VmdVec3 *hipPosition,
    VmdVec3 *kneePosition) {
  if (!hipPosition || !kneePosition || !s_ghostRig.anchorValid)
    return false;
  const uint32_t sideIndex = static_cast<uint32_t>(side);
  const DirectVmdBoneId thighId =
      kDirectVmdPhase5LegFkBones[sideIndex][0];
  const DirectVmdBoneId kneeId =
      kDirectVmdPhase5LegFkBones[sideIndex][1];
  const DirectVmdBindNodePod &thighBind =
      s_ghostRig.nodes[GhostRig_NodeIndex(thighId)].bind;
  const DirectVmdBindNodePod &kneeBind =
      s_ghostRig.nodes[GhostRig_NodeIndex(kneeId)].bind;

  const VmdQuaternion rootRotation = DirectVmdNormalizeQuaternion(
      s_ghostRig.lastDesiredRootValid
          ? s_ghostRig.lastDesiredRootRotation
          : s_ghostRig.anchorRotation);
  VmdVec3 lowerPosition = {};
  if (!GhostRig_EvaluateExpectedLowerBodyWorldPosition(
          &lowerPosition))
    return false;

  const GhostRigTargetBone &lowerTarget =
      s_ghostRig.targets[DirectVmdBoneIndex(
          DirectVmdBoneId::LowerBody)];
  const GhostRigTargetBone &thighTarget =
      s_ghostRig.targets[DirectVmdBoneIndex(thighId)];
  const VmdQuaternion lowerRotation =
      lowerTarget.lastDesiredValid &&
              lowerTarget.lastDesiredFrame == frame
          ? DirectVmdNormalizeQuaternion(
                lowerTarget.lastDesiredWorldRotation)
          : DirectVmdQuaternionMultiply(rootRotation,
              s_ghostRig.nodes[GhostRig_NodeIndex(
                  DirectVmdBoneId::LowerBody)].bind.worldRotation);
  const VmdQuaternion thighRotation =
      thighTarget.lastDesiredValid &&
              thighTarget.lastDesiredFrame == frame
          ? DirectVmdNormalizeQuaternion(
                thighTarget.lastDesiredWorldRotation)
          : DirectVmdQuaternionMultiply(lowerRotation,
                                        thighBind.localRotation);

  *hipPosition = DirectVmdAdd(
      lowerPosition,
      DirectVmdRotateVector(lowerRotation, thighBind.localPosition));
  *kneePosition = DirectVmdAdd(
      *hipPosition,
      DirectVmdRotateVector(thighRotation, kneeBind.localPosition));
  return DirectVmdFinite(hipPosition->x) &&
         DirectVmdFinite(hipPosition->y) &&
         DirectVmdFinite(hipPosition->z) &&
         DirectVmdFinite(kneePosition->x) &&
         DirectVmdFinite(kneePosition->y) &&
         DirectVmdFinite(kneePosition->z);
}

static bool GhostRig_PreparePhase5Leg(
    DirectVmdLegSide side, int frame, bool playbackOwnsPose,
    bool vmdIkEnabled, bool periodicLog) {
  GhostRigLegRuntime &leg = GhostRig_LegRuntime(side);
  void *solver = GhostRig_GetLegSolver(side);
  const bool requestedIk = playbackOwnsPose && vmdIkEnabled;
  bool effectiveIk = requestedIk && solver != nullptr;
  DirectVmdLegControlDecision decision =
      DirectVmdDecideLegControl(effectiveIk);

  if (leg.modeKnown && leg.effectiveIkEnabled != effectiveIk) {
    const bool cleared = solver && GhostRig_ClearLegSolverFields(solver);
    if (cleared)
      ++leg.weightClearCount;
    ++leg.modeSwitchCount;
    leg.postSolveDiagnosticPending = true;
    Log("[P5-LEG-MODE] side=%s unityFrame=%d vmdFrame=%.6f "
        "oldEffectiveIk=%d newEffectiveIk=%d vmdIkEnabled=%d "
        "immediateOldWeightsCleared=%d oldTargetErased=%d "
        "generation=%llu owner=%p tid=%lu",
        GhostRig_LegSideName(side), frame, s_ghostRig.lastSourceFrame,
        leg.effectiveIkEnabled ? 1 : 0, effectiveIk ? 1 : 0,
        vmdIkEnabled ? 1 : 0, cleared ? 1 : 0, cleared ? 1 : 0,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }

  leg.modeKnown = true;
  leg.requestedIkEnabled = vmdIkEnabled;
  leg.effectiveIkEnabled = effectiveIk;
  leg.toeAimPending = false;
  leg.toeAimWritten = false;
  leg.preparedFrame = frame;
  leg.innerWriteFrame = INT_MIN;
  leg.reach = DirectVmdReachProjection();
  leg.toeAim = DirectVmdToeAimResult();
  leg.footWorldRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  leg.toeWorldRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  leg.footControlWorldRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  leg.toeControlWorldRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  leg.terrainNormalRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  leg.terrainContactApplied = false;
  leg.footSeedLocalRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  leg.toeSeedLocalRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  leg.footIkParentWorldPosition = {0.0f, 0.0f, 0.0f};
  leg.toeIkWorldPosition = {0.0f, 0.0f, 0.0f};
  leg.bendDirection = {0.0f, 0.0f, 0.0f};
  leg.kneeMix = {};
  leg.kneeMixWeight = s_directVmdKneeMixEnabled.load(std::memory_order_acquire)
      ? s_directVmdKneeMixWeight.load(std::memory_order_acquire) : 0.0f;
  if (periodicLog)
    Log("[P5-KNEE-REFERENCE] side=%s frame=%d requestedWeight=%.4f sourceConfidence=%.4f "
        "solverReferenceReady=%d effectiveIk=%d sourceRevision=%llu",
        GhostRig_LegSideName(side), frame, leg.kneeMixWeight, leg.sourceKnee.confidence,
        leg.kneeReferenceReady ? 1 : 0, effectiveIk ? 1 : 0,
        (unsigned long long)s_ghostRig.bindSourceRevision);

  const uint32_t sideIndex = static_cast<uint32_t>(side);
  if (!effectiveIk) {
    if (solver && GhostRig_ClearLegSolverFields(solver))
      ++leg.weightClearCount;
    for (uint32_t order = 0; order < decision.fkFinalBoneCount;
         ++order) {
      GhostRig_WritePhase5LegFkBone(
          side, kDirectVmdPhase5LegFkBones[sideIndex][order], frame,
          requestedIk ? "ik-fallback-final-fk" : "ik-off-final-fk");
    }
    if (requestedIk && !solver) {
      Log("[P5-LEG-IK-FALLBACK] side=%s unityFrame=%d vmdFrame=%.6f "
          "reason=solver-unavailable action=fk weights=0 generation=%llu "
          "owner=%p tid=%lu",
          GhostRig_LegSideName(side), frame, s_ghostRig.lastSourceFrame,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    }
  } else {
    bool seedsOk = true;
    for (uint32_t order = 0; order < decision.fkSeedBoneCount; ++order) {
      const bool written = GhostRig_WritePhase5LegFkBone(
          side, kDirectVmdPhase5LegFkBones[sideIndex][order], frame,
          order < 2 ? "finalik-bend-seed"
                    : "finalik-terminal-local-seed");
      seedsOk = written && seedsOk;
    }

    const DirectVmdBoneId ankleId =
        kDirectVmdPhase5LegFkBones[sideIndex][2];
    const DirectVmdBoneId toeId =
        kDirectVmdPhase5LegFkBones[sideIndex][3];
    VmdVec3 hipPosition = {};
    VmdVec3 kneePosition = {};
    VmdVec3 desiredFootPosition = {};
    const bool ghostPoseOk =
        GhostRig_EvaluateExpectedLegSeedPositions(
            side, frame, &hipPosition, &kneePosition) &&
        GhostRig_ReadGhostWorldPose(GhostRig_FootIkParentId(side),
                                    &leg.footIkParentWorldPosition,
                                    nullptr) &&
        GhostRig_ReadGhostWorldPose(GhostRig_FootIkId(side),
                                    &desiredFootPosition, nullptr) &&
        GhostRig_ReadGhostWorldPose(GhostRig_ToeIkId(side),
                                    &leg.toeIkWorldPosition, nullptr);
    if (ghostPoseOk) {
      leg.footIkParentWorldPosition =
          GhostRig_ApplyRootPlacementToWorldPosition(
              leg.footIkParentWorldPosition);
      desiredFootPosition = GhostRig_ApplyRootPlacementToWorldPosition(
          desiredFootPosition);
      leg.toeIkWorldPosition =
          GhostRig_ApplyRootPlacementToWorldPosition(
              leg.toeIkWorldPosition);
      GhostRig_ApplyPhase7LegTarget(
          side, frame, &leg.footIkParentWorldPosition,
          &desiredFootPosition, &leg.toeIkWorldPosition,
          &leg.terrainNormalRotation,
          &leg.terrainContactApplied);
    }
    leg.reach = DirectVmdProjectLegReach(
        hipPosition, desiredFootPosition,
        GhostRig_LegMaximumReach(side), 0.0f);

    VmdQuaternion footSeedWorldRotation = {0.0f, 0.0f, 0.0f, 1.0f};
    VmdQuaternion toeSeedWorldRotation = {0.0f, 0.0f, 0.0f, 1.0f};
    Quat footSeedLocal = {0.0f, 0.0f, 0.0f, 1.0f};
    Quat toeSeedLocal = {0.0f, 0.0f, 0.0f, 1.0f};
    void *ankleTransform = GhostRig_GetTargetTransform(ankleId);
    void *toeTransform = GhostRig_GetTargetTransform(toeId);
    const bool seedPoseOk =
        seedsOk &&
        GhostRig_EvaluateLegTargetWorldRotation(
            ankleId, &footSeedWorldRotation) &&
        GhostRig_EvaluateLegTargetWorldRotation(
            toeId, &toeSeedWorldRotation) &&
        GhostRig_EvaluateControlToTargetWorldRotation(
            GhostRig_FootIkId(side), ankleId,
            &leg.footControlWorldRotation) &&
        GhostRig_EvaluateControlToTargetWorldRotation(
            GhostRig_ToeIkId(side), toeId,
            &leg.toeControlWorldRotation) &&
        ankleTransform && toeTransform &&
        GhostRig_ReadLocalRotation(ankleTransform, footSeedLocal) &&
        GhostRig_ReadLocalRotation(toeTransform, toeSeedLocal);
    if (!ghostPoseOk || !leg.reach.valid || !seedPoseOk) {
      GhostRig_ClearLegSolverFields(solver);
      ++leg.weightClearCount;
      leg.effectiveIkEnabled = false;
      decision = DirectVmdDecideLegControl(false);
      for (uint32_t order = 0; order < decision.fkFinalBoneCount;
           ++order) {
        GhostRig_WritePhase5LegFkBone(
            side, kDirectVmdPhase5LegFkBones[sideIndex][order], frame,
            "invalid-target-fallback-final-fk");
      }
      Log("[P5-LEG-IK-FALLBACK] side=%s unityFrame=%d vmdFrame=%.6f "
          "reason=ghost-or-bind-target-invalid ghostPoseOk=%d "
          "reachValid=%d seedPoseOk=%d action=fk weights=0 "
          "generation=%llu owner=%p tid=%lu",
          GhostRig_LegSideName(side), frame, s_ghostRig.lastSourceFrame,
          ghostPoseOk ? 1 : 0, leg.reach.valid ? 1 : 0,
          seedPoseOk ? 1 : 0,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    } else {
      leg.footWorldRotation = footSeedWorldRotation;
      leg.toeWorldRotation = toeSeedWorldRotation;
      leg.footSeedLocalRotation = DirectVmdNormalizeQuaternion(
          {footSeedLocal.x, footSeedLocal.y, footSeedLocal.z,
           footSeedLocal.w});
      leg.toeSeedLocalRotation = DirectVmdNormalizeQuaternion(
          {toeSeedLocal.x, toeSeedLocal.y, toeSeedLocal.z,
           toeSeedLocal.w});
      leg.bendDirection = GhostRig_EvaluateBendDirection(
          hipPosition, kneePosition, leg.reach.solverTarget);
      leg.toeAimPending = true;
      __try {
        *reinterpret_cast<int *>((char *)solver +
                                 OFF_IKLIMB_BEND_MODIFIER) = 0;
        *reinterpret_cast<float *>((char *)solver +
                                   OFF_IKLIMB_BEND_WEIGHT) = 1.0f;
      } __except (EXCEPTION_EXECUTE_HANDLER) {
      }
      if (GhostRig_WriteLegSolverFields(
              solver, decision.finalIkPositionWeight,
              leg.reach.solverTarget,
              decision.finalIkRotationWeight,
              leg.footWorldRotation))
        ++leg.solverWriteCount;
    }
  }

  if (periodicLog) {
    decision = DirectVmdDecideLegControl(leg.effectiveIkEnabled);
    const float positionWeight = decision.finalIkPositionWeight;
    const float rotationWeight = decision.finalIkRotationWeight;
    Log("[P5-LEG-POSE] side=%s unityFrame=%d vmdFrame=%.6f "
        "ikEnabled=%d effectiveFinalIk=%d playbackOwnsPose=%d "
        "finalIkPositionWeight=%.1f finalIkRotationWeight=%.1f "
        "desiredFootTarget=(%.6f,%.6f,%.6f) "
        "solverFootTarget=(%.6f,%.6f,%.6f) "
        "desiredDistance=%.6f reachableDistance=%.6f residual=%.6f "
        "maximumReach=%.6f limitMargin=%.6f clamped=%d "
        "footRotation=(%.7f,%.7f,%.7f,%.7f) "
        "footControlRotation=(%.7f,%.7f,%.7f,%.7f) "
        "footSeedLocalRotation=(%.7f,%.7f,%.7f,%.7f) "
        "toeRotation=(%.7f,%.7f,%.7f,%.7f) "
        "toeControlRotation=(%.7f,%.7f,%.7f,%.7f) "
        "toeSeedLocalRotation=(%.7f,%.7f,%.7f,%.7f) "
        "bendDirection=(%.6f,%.6f,%.6f) "
        "footIkParentWorld=(%.6f,%.6f,%.6f) "
        "toeIkWorld=(%.6f,%.6f,%.6f) motionScale=%.8f "
        "placementOffset=(%.6f,%.6f,%.6f) "
        "targetSource=ghost-natural-bind toeTargetSource=ghost-toe-ik-position "
        "rootAndFootPlacementShared=1 "
        "anklePositionOwner=FinalIK ankleRotationOwner=post-solve-toe-aim "
        "previousTargetInput=0 "
        "liveThighInput=0 firstKeySubtraction=0 floorClamp=0 softIk=0 "
        "generation=%llu owner=%p tid=%lu",
        GhostRig_LegSideName(side), frame, s_ghostRig.lastSourceFrame,
        vmdIkEnabled ? 1 : 0, leg.effectiveIkEnabled ? 1 : 0,
        playbackOwnsPose ? 1 : 0, positionWeight, rotationWeight,
        leg.reach.desiredTarget.x, leg.reach.desiredTarget.y,
        leg.reach.desiredTarget.z, leg.reach.solverTarget.x,
        leg.reach.solverTarget.y, leg.reach.solverTarget.z,
        leg.reach.desiredDistance, leg.reach.reachableDistance,
        leg.reach.residual, leg.reach.maximumReach,
        leg.reach.limitMargin, leg.reach.clamped ? 1 : 0,
        leg.footWorldRotation.x, leg.footWorldRotation.y,
        leg.footWorldRotation.z, leg.footWorldRotation.w,
        leg.footControlWorldRotation.x,
        leg.footControlWorldRotation.y,
        leg.footControlWorldRotation.z,
        leg.footControlWorldRotation.w,
        leg.footSeedLocalRotation.x,
        leg.footSeedLocalRotation.y,
        leg.footSeedLocalRotation.z,
        leg.footSeedLocalRotation.w,
        leg.toeWorldRotation.x, leg.toeWorldRotation.y,
        leg.toeWorldRotation.z, leg.toeWorldRotation.w,
        leg.toeControlWorldRotation.x,
        leg.toeControlWorldRotation.y,
        leg.toeControlWorldRotation.z,
        leg.toeControlWorldRotation.w,
        leg.toeSeedLocalRotation.x,
        leg.toeSeedLocalRotation.y,
        leg.toeSeedLocalRotation.z,
        leg.toeSeedLocalRotation.w,
        leg.bendDirection.x, leg.bendDirection.y,
        leg.bendDirection.z, leg.footIkParentWorldPosition.x,
        leg.footIkParentWorldPosition.y,
        leg.footIkParentWorldPosition.z, leg.toeIkWorldPosition.x,
        leg.toeIkWorldPosition.y, leg.toeIkWorldPosition.z,
        s_ghostRig.motionScale, s_ghostRig.rootPlacementOffset.x,
        s_ghostRig.rootPlacementOffset.y,
        s_ghostRig.rootPlacementOffset.z,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    GhostRig_LogPhase7Foot(side, frame, vmdIkEnabled,
                           positionWeight, leg);
  }
  return true;
}

static bool GhostRig_GrounderCachedTargetFresh(uint32_t index,
                                                int frame) {
  if (index >= DIRECT_VMD_LEG_SIDE_COUNT)
    return false;
  const GhostRigGrounderRuntime &grounder = s_ghostRig.grounder;
  return grounder.cachedTargetValid[index] &&
      grounder.cachedPlaybackCycle == s_ghostRig.lastPlaybackCycle &&
      std::fabs(grounder.cachedSourceFrame -
                s_ghostRig.lastSourceFrame) <= 8.0 &&
      (frame < 0 || grounder.cachedTargetFrame[index] < 0 ||
       frame - grounder.cachedTargetFrame[index] <= 2);
}

static bool GhostRig_GrounderDirectFieldsFresh(uint32_t index,
                                                int frame) {
  if (index >= DIRECT_VMD_LEG_SIDE_COUNT)
    return false;
  const GhostRigGrounderRuntime &grounder = s_ghostRig.grounder;
  const bool hasDirectField =
      grounder.cachedLastHitValid[index] ||
      grounder.cachedHeelHitValid[index] ||
      grounder.cachedCalculatedFootValid[index] ||
      grounder.cachedLegIkPositionValid[index];
  return hasDirectField &&
      grounder.cachedRawLegGrounded[index] &&
      grounder.cachedPlaybackCycle == s_ghostRig.lastPlaybackCycle &&
      std::fabs(grounder.cachedSourceFrame -
                s_ghostRig.lastSourceFrame) <= 8.0 &&
      (frame < 0 || grounder.cachedRawGroundFrame[index] < 0 ||
       frame - grounder.cachedRawGroundFrame[index] <= 2);
}

static bool GhostRig_GrounderFootPhysicsPlaneFresh(uint32_t index,
                                                    int frame) {
  if (index >= DIRECT_VMD_LEG_SIDE_COUNT)
    return false;
  const GhostRigGrounderRuntime &grounder = s_ghostRig.grounder;
  return grounder.cachedFootPhysicsPlane[index].valid &&
      grounder.cachedPlaybackCycle == s_ghostRig.lastPlaybackCycle &&
      std::fabs(grounder.cachedSourceFrame -
                s_ghostRig.lastSourceFrame) <= 8.0 &&
      (frame < 0 || grounder.cachedFootPhysicsPlaneFrame[index] < 0 ||
       frame - grounder.cachedFootPhysicsPlaneFrame[index] <= 2);
}

static void GhostRig_UpdateGrounderHybridTerrain(
    int frame, bool grounderActive, bool playbackOwnsPose) {
  GhostRigGrounderRuntime &grounder = s_ghostRig.grounder;
  if (!grounderActive) {
    for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT;
         ++index) {
      grounder.hybridTargetValid[index] = false;
      grounder.hybridUsesGrounder[index] = false;
      grounder.upwardClearanceApplied[index] = false;
      grounder.previousFootPhysicsQueryValid[index] = false;
      grounder.cachedFootDirectionalClearance[index] =
          DirectVmdDirectionalTerrainClearance();
      grounder.stableNativeHeight[index] =
          DirectVmdGrounderHeightState();
      grounder.contactEdge[index] =
          DirectVmdGrounderContactEdgeState();
    }
    grounder.stableRootOffset = DirectVmdGrounderHeightState();
    grounder.rootReachRelease = DirectVmdRootReachReleaseState();
    return;
  }

  const bool playing = playbackOwnsPose &&
      s_ghostRig.lastSamplePlayback == DirectVmdPlaybackState::Playing;
  bool timelineReacquire = false;
  bool forwardGapPreserved = false;
  double sourceStep = 0.0;
  if (grounder.contactTimelineValid) {
    sourceStep = s_ghostRig.lastSourceFrame -
                 grounder.contactSourceFrame;
    timelineReacquire = DirectVmdGrounderTimelineNeedsReacquire(
        grounder.contactPlaybackCycle, s_ghostRig.lastPlaybackCycle,
        sourceStep);
    forwardGapPreserved = !timelineReacquire && sourceStep > 8.0;
  }
  if (timelineReacquire) {
    const float preservedRootOffset =
        s_ghostRig.terrainState.rootOffset;
    const float preservedEnvironmentOffset =
        grounder.rootEnvironmentTargetOffset;
    const float preservedReachOffset =
        grounder.rootReachTargetOffset;
    DirectVmdTerrainResetKinematics(&s_ghostRig.terrainState, true);
    s_ghostRig.terrainState.rootOffset = preservedRootOffset;
    s_ghostRig.terrainState.rootTargetOffset = preservedRootOffset;
    for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT;
         ++index) {
      grounder.hybridTargetValid[index] = false;
      grounder.hybridUsesGrounder[index] = false;
      grounder.upwardClearanceApplied[index] = false;
      grounder.previousFootPhysicsQueryValid[index] = false;
      grounder.cachedFootDirectionalClearance[index] =
          DirectVmdDirectionalTerrainClearance();
      grounder.stableNativeHeight[index] =
          DirectVmdGrounderHeightState();
      grounder.contactEdge[index] =
          DirectVmdGrounderContactEdgeState();
    }
    grounder.stableRootOffset = DirectVmdGrounderHeightState();
    grounder.stableRootOffset.stableValid = true;
    grounder.stableRootOffset.stableHeight =
        preservedEnvironmentOffset;
    grounder.rootReachRelease = DirectVmdRootReachReleaseState();
    Log("[P7-GROUNDER-TIMELINE] event=reacquire unityFrame=%d "
        "sourceStep=%.6f oldCycle=%llu newCycle=%llu "
        "contactsReset=1 rootOffsetPreserved=1 "
        "preservedRootOffset=%.6f preservedEnvironmentOffset=%.6f "
        "preservedReachOffset=%.6f generation=%llu owner=%p "
        "tid=%lu",
        frame, sourceStep,
        (unsigned long long)grounder.contactPlaybackCycle,
        (unsigned long long)s_ghostRig.lastPlaybackCycle,
        preservedRootOffset, preservedEnvironmentOffset,
        preservedReachOffset,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  } else if (forwardGapPreserved) {
    Log("[P7-GROUNDER-TIMELINE] event=forward-gap unityFrame=%d "
        "sourceStep=%.6f cycle=%llu contactsReset=0 "
        "rootOffsetPreserved=1 dtWillClamp=1 generation=%llu owner=%p "
        "tid=%lu",
        frame, sourceStep,
        (unsigned long long)s_ghostRig.lastPlaybackCycle,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }

  float deltaSeconds = 0.0f;
  if (playing) {
    if (!grounder.contactTimelineValid || timelineReacquire) {
      deltaSeconds = 1.0f / 60.0f;
    } else if (sourceStep > 1.0e-6) {
      deltaSeconds = DirectVmdTerrainClamp(
          static_cast<float>(sourceStep / kVmdFramesPerSecond),
          1.0f / 300.0f, 0.100f);
    }
  }
  grounder.contactTimelineValid = true;
  grounder.contactSourceFrame = s_ghostRig.lastSourceFrame;
  grounder.contactPlaybackCycle = s_ghostRig.lastPlaybackCycle;

  float lowestFlatY = std::numeric_limits<float>::infinity();
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT;
       ++index) {
    const GhostRigLegRuntime &leg = s_ghostRig.legs[index];
    if (leg.effectiveIkEnabled && leg.reach.valid &&
        DirectVmdFinite(leg.reach.desiredTarget.y)) {
      lowestFlatY = (std::min)(lowestFlatY,
                               leg.reach.desiredTarget.y);
    }
  }

  DirectVmdTerrainFrameOutput output;
  DirectVmdGrounderHeightOutput
      stableHeightOutput[DIRECT_VMD_LEG_SIDE_COUNT];
  DirectVmdGrounderContactEdgeOutput
      contactEdgeOutput[DIRECT_VMD_LEG_SIDE_COUNT];
  DirectVmdGrounderHeightSelection
      heightSelection[DIRECT_VMD_LEG_SIDE_COUNT];
  bool rawFreshForLog[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool solverFreshForLog[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float sourceClearanceForLog[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float expectedAnkleForLog[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float maximumTreadDistanceForLog[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float rawFootCorrectionForLog[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float stableNativeTargetForLog[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  float supportPlatformForLog[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool supportPlatformValidForLog[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool physicsFreshForLog[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool physicsHeldForLog[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  bool transitioned = false;
  bool usableContact[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT;
       ++index) {
    GhostRigLegRuntime &leg = s_ghostRig.legs[index];
    DirectVmdTerrainFootState &contactState =
        s_ghostRig.terrainState.feet[index];
    const VmdVec3 flatTarget = leg.reach.valid
                                   ? leg.reach.desiredTarget
                                   : VmdVec3{};
    const DirectVmdAuthoredFootContactOutput contact =
        DirectVmdUpdateAuthoredFootContact(
            &contactState, s_ghostRig.terrainConfig,
            playbackOwnsPose && leg.effectiveIkEnabled,
            flatTarget.y, lowestFlatY, deltaSeconds,
            deltaSeconds > 0.0f);
    transitioned = transitioned || contact.transitioned != 0;
    grounder.authoredLift[index] = contact.lift;
    grounder.authoredVerticalVelocity[index] =
        contact.verticalVelocity;

    const bool solverFresh =
        GhostRig_GrounderCachedTargetFresh(index, frame);
    const bool rawFresh =
        GhostRig_GrounderDirectFieldsFresh(index, frame);
    const bool physicsFresh =
        GhostRig_GrounderFootPhysicsPlaneFresh(index, frame);
    const float bindClearance =
        s_ghostRig.terrainAnkleClearance[index];
    const float sideLegLength = index == 0
        ? s_ghostRig.leftLegLength
        : s_ghostRig.rightLegLength;
    const float sourceClearance =
        DirectVmdGrounderEffectiveAnkleClearance(
            bindClearance,
            grounder.cachedGroundHeightOffsetValid,
            grounder.cachedGroundHeightOffset,
            sideLegLength);
    const float expectedBaselineAnkleY = flatTarget.y - contact.lift +
        s_ghostRig.terrainState.rootOffset;
    const float maximumTreadDistance = DirectVmdTerrainClamp(
        sideLegLength * 0.65f, 0.30f, 0.55f);
    const DirectVmdTerrainPlane &physicsPlane =
        grounder.cachedFootPhysicsPlane[index];
    if (physicsFresh && playbackOwnsPose &&
        !grounder.footPhysicsReferenceValid[index]) {
      grounder.footPhysicsReferenceValid[index] = true;
      grounder.footPhysicsReferenceGroundY[index] =
          physicsPlane.height;
      Log("[P7-GROUNDING-REFERENCE] side=%s unityFrame=%d "
          "vmdFrame=%.6f rootReferenceGroundY=%.6f "
          "flatTargetY=%.6f authoredBaselineY=%.6f "
          "ankleClearance=%.6f groundedAnkleY=%.6f "
          "initialFootCorrection=%.6f "
          "source=Grounding.Raycast-delegate-authored-foot-xz "
          "rootEnvironmentDeltaOnly=1 footAbsoluteGrounding=1 "
          "vmdFirstKeySubtraction=0 animationFrameCalibration=0 "
          "generation=%llu owner=%p tid=%lu",
          index == 0 ? "L" : "R", frame,
          s_ghostRig.lastSourceFrame, physicsPlane.height,
          flatTarget.y, flatTarget.y - contact.lift,
          sourceClearance, physicsPlane.height + sourceClearance,
          physicsPlane.height + sourceClearance -
              (flatTarget.y - contact.lift),
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    }
    DirectVmdFootPhysicsHeightSelection physicsSelection;
    if (physicsFresh &&
        grounder.footPhysicsReferenceValid[index]) {
      physicsSelection = DirectVmdSelectFootPhysicsTerrainHeight(
          flatTarget.y, contact.lift, physicsPlane.height,
          grounder.footPhysicsReferenceGroundY[index],
          sourceClearance, expectedBaselineAnkleY,
          maximumTreadDistance);
    }
    DirectVmdFootPhysicsHeightSelection clearancePhysicsSelection;
    const DirectVmdDirectionalTerrainClearance &directionalClearance =
        grounder.cachedFootDirectionalClearance[index];
    const float clearanceGroundY =
        physicsFresh && directionalClearance.valid
            ? directionalClearance.height
            : physicsPlane.height;
    if (physicsFresh && grounder.footPhysicsReferenceValid[index]) {
      clearancePhysicsSelection =
          DirectVmdSelectFootPhysicsTerrainHeight(
              flatTarget.y, contact.lift, clearanceGroundY,
              grounder.footPhysicsReferenceGroundY[index],
              sourceClearance, expectedBaselineAnkleY,
              maximumTreadDistance);
    }
    const bool nativeSampleValid = physicsSelection.valid;
    if (nativeSampleValid) {
      heightSelection[index].valid = true;
      heightSelection[index].directHit = true;
      heightSelection[index].ankleY = physicsSelection.ankleY;
      heightSelection[index].groundY = physicsPlane.height;
      heightSelection[index].referenceAnkleY =
          expectedBaselineAnkleY;
      heightSelection[index].referenceError =
          physicsSelection.referenceError;
      heightSelection[index].acceptedGroundCandidateCount =
          physicsPlane.clusterCount;
      heightSelection[index].source =
          DirectVmdGrounderHeightSource::FootGroundingRaycast;
    }
    if (deltaSeconds > 0.0f) {
      if (nativeSampleValid)
        grounder.footPhysicsMissSeconds[index] = 0.0f;
      else
        grounder.footPhysicsMissSeconds[index] += deltaSeconds;
    }
    const float nativeSampleDelta = nativeSampleValid
        ? physicsSelection.groundDelta
        : 0.0f;
    contactEdgeOutput[index] =
        DirectVmdGateGrounderContactEdgeHeight(
            &grounder.contactEdge[index], contact.contact,
            nativeSampleValid, nativeSampleDelta,
            grounder.stableNativeHeight[index].stableValid,
            grounder.stableNativeHeight[index].stableHeight,
            flatTarget,
            s_ghostRig.terrainConfig.grounderContinuousHeightBand,
            s_ghostRig.terrainConfig.grounderContactEdgeReleaseDistance,
            deltaSeconds,
            deltaSeconds > 0.0f);
    stableHeightOutput[index] = DirectVmdUpdateGrounderHeight(
        &grounder.stableNativeHeight[index],
        s_ghostRig.terrainConfig,
        contactEdgeOutput[index].sampleValid,
        contactEdgeOutput[index].sampleHeight,
        deltaSeconds, deltaSeconds > 0.0f);
    transitioned = transitioned ||
        contactEdgeOutput[index].transitioned ||
        stableHeightOutput[index].committed;
    const float stableTerrainDelta = stableHeightOutput[index].valid
        ? stableHeightOutput[index].stableHeight
        : s_ghostRig.terrainState.rootOffset;
    const bool stableGroundValid = stableHeightOutput[index].valid &&
        grounder.footPhysicsReferenceValid[index];
    const float stableGroundY = stableGroundValid
        ? grounder.footPhysicsReferenceGroundY[index] +
              stableTerrainDelta
        : 0.0f;
    const float stableFootCorrection = stableGroundValid
        ? stableGroundY + sourceClearance -
              (flatTarget.y - contact.lift)
        : s_ghostRig.terrainState.rootOffset;
    const float nativeY = stableGroundValid
        ? flatTarget.y + stableFootCorrection
        : flatTarget.y + s_ghostRig.terrainState.rootOffset;
    rawFootCorrectionForLog[index] = nativeSampleValid
        ? physicsSelection.footCorrection
        : 0.0f;
    stableNativeTargetForLog[index] = nativeY;
    float correctionY = stableTerrainDelta;
    const bool correctionValid = stableHeightOutput[index].valid &&
        DirectVmdFinite(correctionY) &&
        std::fabs(correctionY - s_ghostRig.terrainState.rootOffset) <=
            maximumTreadDistance;
    if (!correctionValid)
      correctionY = s_ghostRig.terrainState.rootOffset;
    const bool physicsHeld = !nativeSampleValid &&
        stableHeightOutput[index].valid &&
        grounder.footPhysicsMissSeconds[index] <=
            s_ghostRig.terrainConfig.hitLossHoldSeconds;
    rawFreshForLog[index] = rawFresh;
    solverFreshForLog[index] = solverFresh;
    physicsFreshForLog[index] = physicsFresh;
    physicsHeldForLog[index] = physicsHeld;
    sourceClearanceForLog[index] = sourceClearance;
    expectedAnkleForLog[index] = expectedBaselineAnkleY;
    maximumTreadDistanceForLog[index] = maximumTreadDistance;
    grounder.directNativeTargetY[index] = nativeSampleValid
        ? physicsSelection.ankleY
        : nativeY;
    grounder.nativeHeightSource[index] =
        heightSelection[index].source;
    grounder.nativeCorrectionY[index] = correctionY;
    usableContact[index] = correctionValid &&
        (nativeSampleValid || physicsHeld) &&
        contact.contact == DirectVmdTerrainContactState::Contact &&
        leg.effectiveIkEnabled;
    grounder.hybridUsesGrounder[index] = usableContact[index];

    const bool supportPlatformValid =
        grounder.footPhysicsReferenceValid[index] &&
        contactState.flatBaselineValid;
    const DirectVmdGroundedSupportTarget supportPlatform =
        supportPlatformValid
            ? DirectVmdComputeGroundedSupportTarget(
                  grounder.footPhysicsReferenceGroundY[index],
                  s_ghostRig.terrainState.rootOffset,
                  sourceClearance, contact.lift)
            : DirectVmdGroundedSupportTarget();
    const float platformY = supportPlatform.valid
        ? supportPlatform.targetY
        : flatTarget.y + s_ghostRig.terrainState.rootOffset;
    supportPlatformForLog[index] = platformY;
    supportPlatformValidForLog[index] = supportPlatform.valid;
    DirectVmdUpwardTerrainClearanceOutput upwardClearance;
    if (leg.effectiveIkEnabled && contactState.flatBaselineValid &&
        clearancePhysicsSelection.valid) {
      upwardClearance = DirectVmdComputeUpwardTerrainClearance(
          platformY, clearancePhysicsSelection.groundedAnkleY);
    }
    const float baseTargetY = usableContact[index] ? nativeY : platformY;
    const bool applyUpwardClearance =
        DirectVmdShouldApplyUpwardTerrainClearance(
            contact.contact, upwardClearance, baseTargetY);
    const bool clearanceTransition =
        grounder.upwardClearanceApplied[index] !=
            applyUpwardClearance;
    transitioned = transitioned || clearanceTransition;
    grounder.upwardClearanceApplied[index] =
        applyUpwardClearance;
    grounder.upwardClearanceTargetY[index] =
        applyUpwardClearance ? upwardClearance.targetY : baseTargetY;
    const bool applyTerrainTarget =
        usableContact[index] || applyUpwardClearance;
    const float targetY = applyUpwardClearance
        ? (std::max)(baseTargetY, upwardClearance.targetY)
        : baseTargetY;
    if (!grounder.hybridTargetValid[index])
      grounder.hybridTargetY[index] = baseTargetY;
    if (deltaSeconds > 0.0f) {
      if (applyTerrainTarget) {
        grounder.hybridTargetY[index] = DirectVmdTerrainFilteredStep(
            grounder.hybridTargetY[index], targetY,
            s_ghostRig.terrainConfig.grounderFootHeightTimeConstant,
            s_ghostRig.terrainConfig.grounderMaximumFootHeightSpeed,
            deltaSeconds);
      } else {
        grounder.hybridTargetY[index] = platformY;
      }
    }
    grounder.hybridTargetValid[index] =
        leg.reach.valid && DirectVmdFinite(grounder.hybridTargetY[index]);

    DirectVmdTerrainFootOutput &foot = output.feet[index];
    foot.flatTarget = flatTarget;
    foot.desiredTarget = flatTarget;
    foot.desiredTarget.y = grounder.hybridTargetY[index];
    foot.rawHeight = physicsFresh
        ? physicsPlane.height
        : stableGroundY;
    foot.stableHeight = stableGroundY;
    foot.contactPlaneHeight = foot.stableHeight;
    foot.verticalVelocity = contact.verticalVelocity;
    foot.lift = contact.lift;
    foot.surfaceOffset = correctionY;
    foot.appliedOffset = foot.desiredTarget.y - flatTarget.y;
    foot.contact = contact.contact;
    foot.hit = nativeSampleValid ? 1 : 0;
    foot.heldHit = physicsHeld ? 1 : 0;
    foot.stablePlane = correctionValid ? 1 : 0;
    foot.contactPlane = usableContact[index] ? 1 : 0;
    foot.ikEnabled = leg.effectiveIkEnabled ? 1 : 0;
    foot.normal = {0.0f, 1.0f, 0.0f};
    if (physicsFresh) {
      foot.normal = physicsPlane.normal;
      foot.rawHitCount = physicsPlane.hitCount;
      foot.rawClusterCount = physicsPlane.clusterCount;
      foot.centerHit = physicsPlane.centerHit;
      foot.centerAnchored = physicsPlane.centerAnchored;
      foot.normalFromPointFit = physicsPlane.normalFromPointFit;
      foot.centerPoint = physicsPlane.centerPoint;
      foot.centerHorizontalError =
          physicsPlane.centerHorizontalError;
      foot.centerFloorDistance = physicsPlane.centerFloorDistance;
    }
  }

  DirectVmdTerrainSupportState desiredSupport =
      DirectVmdTerrainSupportState::None;
  if (usableContact[0] && usableContact[1])
    desiredSupport = DirectVmdTerrainSupportState::Double;
  else if (usableContact[0])
    desiredSupport = DirectVmdTerrainSupportState::Left;
  else if (usableContact[1])
    desiredSupport = DirectVmdTerrainSupportState::Right;
  DirectVmdTerrainState &terrain = s_ghostRig.terrainState;
  const DirectVmdGrounderRootTarget rootCandidate =
      DirectVmdSelectGrounderRootTarget(
          usableContact, grounder.nativeCorrectionY);
  const DirectVmdGrounderHeightOutput stableRootOutput =
      DirectVmdUpdateGrounderHeight(
          &grounder.stableRootOffset, s_ghostRig.terrainConfig,
          rootCandidate.valid, rootCandidate.offset, deltaSeconds,
          deltaSeconds > 0.0f);
  const float environmentTargetOffset = stableRootOutput.valid
      ? stableRootOutput.stableHeight
      : grounder.rootEnvironmentTargetOffset;
  DirectVmdRootReachCompensation
      rootReachCompensation[DIRECT_VMD_LEG_SIDE_COUNT];
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT;
       ++index) {
    if (!usableContact[index])
      continue;
    VmdVec3 hipPosition = {};
    VmdVec3 kneePosition = {};
    if (!GhostRig_EvaluateExpectedLegSeedPositions(
            static_cast<DirectVmdLegSide>(index), frame,
            &hipPosition, &kneePosition))
      continue;
    hipPosition.y += environmentTargetOffset - terrain.rootOffset;
    rootReachCompensation[index] =
        DirectVmdComputeDownwardRootReachCompensation(
            hipPosition, output.feet[index].desiredTarget,
            GhostRig_LegMaximumReach(
                static_cast<DirectVmdLegSide>(index)));
  }
  const float averageLegLength = 0.5f *
      (s_ghostRig.leftLegLength + s_ghostRig.rightLegLength);
  const float maximumRootReachOffset = DirectVmdTerrainClamp(
      averageLegLength * 0.45f, 0.20f, 0.40f);
  const DirectVmdRootReachTargetSelection rootReachSelection =
      DirectVmdSelectRootReachTarget(
          usableContact, rootReachCompensation,
          &grounder.rootReachRelease,
          grounder.rootReachTargetOffset, maximumRootReachOffset,
          s_ghostRig.terrainConfig.grounderRootReachReleaseSeconds,
          s_ghostRig.terrainConfig.grounderRootReachReleaseDeadband,
          deltaSeconds, deltaSeconds > 0.0f);
  if (deltaSeconds > 0.0f) {
    terrain.support = desiredSupport;
    terrain.pendingSupport = desiredSupport;
    terrain.pendingSupportSeconds = 0.0f;

    terrain.rootSupport = rootCandidate.support;
    terrain.pendingRootSupport = stableRootOutput.pending
        ? rootCandidate.support
        : terrain.rootSupport;
    terrain.pendingRootSupportSeconds =
        stableRootOutput.pendingSeconds;
    grounder.rootEnvironmentTargetOffset =
        environmentTargetOffset;
    grounder.rootReachTargetOffset =
        rootReachSelection.targetOffset;
    terrain.rootTargetOffset =
        grounder.rootEnvironmentTargetOffset +
        grounder.rootReachTargetOffset;
    terrain.rootOffset = DirectVmdTerrainFilteredStep(
        terrain.rootOffset, terrain.rootTargetOffset,
        s_ghostRig.terrainConfig.grounderRootHeightTimeConstant,
        s_ghostRig.terrainConfig.grounderMaximumRootHeightSpeed,
        deltaSeconds);
  }

  output.support = terrain.support;
  output.rootSupport = terrain.rootSupport;
  output.pendingRootSupport = terrain.pendingRootSupport;
  output.rootOffset = terrain.rootOffset;
  output.rootTargetOffset = terrain.rootTargetOffset;
  s_ghostRig.terrainFrame = output;
  s_ghostRig.terrainFrameValid = true;
  s_ghostRig.lastTerrainFrame = frame;
  grounder.contactFrame = frame;
  s_ghostRig.terrainLastSourceFrame = s_ghostRig.lastSourceFrame;
  s_ghostRig.terrainLastPlaybackCycle = s_ghostRig.lastPlaybackCycle;

  const bool periodic = grounder.lastContactLogFrame == INT_MIN ||
      frame < 0 || frame - grounder.lastContactLogFrame >= 120;
  if (transitioned || periodic ||
      rootReachSelection.releaseConfirmed) {
    grounder.lastContactLogFrame = frame;
    Log("[P7-ROOT-REACH] unityFrame=%d vmdFrame=%.6f "
        "support=%s leftUsable=%d leftValid=%d leftNeeded=%d "
        "leftCorrection=%.6f leftResidual=%.6f "
        "leftHorizontal=%.6f leftDownward=%.6f "
        "leftVerticalLimit=%.6f "
        "rightUsable=%d rightValid=%d rightNeeded=%d "
        "rightCorrection=%.6f rightResidual=%.6f "
        "rightHorizontal=%.6f rightDownward=%.6f "
        "rightVerticalLimit=%.6f "
        "sampleValid=%d sampledReachOffset=%.6f "
        "reachTargetOffset=%.6f releaseHeld=%d "
        "releasePending=%d releasePendingOffset=%.6f "
        "releasePendingSeconds=%.6f releaseConfirmed=%d "
        "releaseConfirmSeconds=%.6f releaseDeadband=%.6f "
        "clamped=%d maximumDownwardOffset=%.6f "
        "environmentTargetOffset=%.6f totalRootTargetOffset=%.6f "
        "totalRootOffset=%.6f contactOnly=1 airborneInput=0 "
        "expectedHipFromImmutablePose=1 liveBoneFeedback=0 "
        "generation=%llu owner=%p tid=%lu",
        frame, s_ghostRig.lastSourceFrame,
        DirectVmdTerrainSupportStateName(desiredSupport),
        usableContact[0] ? 1 : 0,
        rootReachCompensation[0].valid ? 1 : 0,
        rootReachCompensation[0].needed ? 1 : 0,
        rootReachCompensation[0].correctionY,
        rootReachCompensation[0].residual,
        rootReachCompensation[0].horizontalDistance,
        rootReachCompensation[0].downwardDistance,
        rootReachCompensation[0].maximumVerticalDistance,
        usableContact[1] ? 1 : 0,
        rootReachCompensation[1].valid ? 1 : 0,
        rootReachCompensation[1].needed ? 1 : 0,
        rootReachCompensation[1].correctionY,
        rootReachCompensation[1].residual,
        rootReachCompensation[1].horizontalDistance,
        rootReachCompensation[1].downwardDistance,
        rootReachCompensation[1].maximumVerticalDistance,
        rootReachSelection.sampleValid ? 1 : 0,
        rootReachSelection.sampledOffset,
        grounder.rootReachTargetOffset,
        rootReachSelection.releaseHeld ? 1 : 0,
        rootReachSelection.releasePending ? 1 : 0,
        rootReachSelection.pendingOffset,
        rootReachSelection.pendingSeconds,
        rootReachSelection.releaseConfirmed ? 1 : 0,
        s_ghostRig.terrainConfig.grounderRootReachReleaseSeconds,
        s_ghostRig.terrainConfig.grounderRootReachReleaseDeadband,
        rootReachSelection.clamped ? 1 : 0,
        maximumRootReachOffset,
        grounder.rootEnvironmentTargetOffset,
        terrain.rootTargetOffset, terrain.rootOffset,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    Log("[P7-GROUNDER-HYBRID] unityFrame=%d vmdFrame=%.6f dt=%.6f "
        "leftContact=%s leftLift=%.6f leftVelocity=%.6f "
        "leftFlatY=%.6f leftSolverY=%.6f leftRawGroundY=%.6f "
        "leftRawAnkleY=%.6f leftRawFootCorrection=%.6f "
        "leftStableNativeY=%.6f leftClearance=%.6f "
        "leftBindClearance=%.6f leftGrounderHeightFromGround=%.6f "
        "leftRaycastFresh=%d leftRaycastHeld=%d "
        "leftRaycastGroundY=%.6f leftRaycastReferenceGroundY=%.6f "
        "leftRootTerrainDelta=%.6f leftRaycastHits=%u "
        "leftRaycastNormal=(%.6f,%.6f,%.6f) "
        "leftExpectedBaselineAnkleY=%.6f leftHeightReferenceError=%.6f "
        "leftGroundCandidates=%u leftMaxTreadDistance=%.6f "
        "leftHybridY=%.6f leftSupportPlatformY=%.6f "
        "leftSupportPlatformValid=%d leftUpClear=%d leftUpClearY=%.6f "
        "leftLeadProbe=%u leftLeadGroundY=%.6f leftProbeTravel=%.6f "
        "leftHeightSource=%s leftUseNative=%d "
        "leftRawGrounded=%d leftRawFresh=%d leftSolverFresh=%d "
        "leftLastHitY=%.6f leftHeelHitY=%.6f "
        "leftCalculatedFootY=%.6f leftLegIkY=%.6f "
        "leftInStair=%d "
        "leftHitNormal=(%.6f,%.6f,%.6f) "
        "leftHeightPending=%d leftPendingY=%.6f "
        "leftPendingSeconds=%.6f leftHeightSwitches=%u "
        "leftEdgeHeld=%d leftEdgeCandidate=%d leftEdgeReleased=%d "
        "leftEdgeCandidateY=%.6f leftEdgeTravel=%.6f "
        "leftEdgeSeconds=%.6f leftEdgeByTravel=%d "
        "leftEdgeReleaseDistance=%.6f leftEdgeHeldSamples=%u "
        "rightContact=%s rightLift=%.6f rightVelocity=%.6f "
        "rightFlatY=%.6f rightSolverY=%.6f rightRawGroundY=%.6f "
        "rightRawAnkleY=%.6f rightRawFootCorrection=%.6f "
        "rightStableNativeY=%.6f rightClearance=%.6f "
        "rightBindClearance=%.6f rightGrounderHeightFromGround=%.6f "
        "rightRaycastFresh=%d rightRaycastHeld=%d "
        "rightRaycastGroundY=%.6f rightRaycastReferenceGroundY=%.6f "
        "rightRootTerrainDelta=%.6f rightRaycastHits=%u "
        "rightRaycastNormal=(%.6f,%.6f,%.6f) "
        "rightExpectedBaselineAnkleY=%.6f rightHeightReferenceError=%.6f "
        "rightGroundCandidates=%u rightMaxTreadDistance=%.6f "
        "rightHybridY=%.6f rightSupportPlatformY=%.6f "
        "rightSupportPlatformValid=%d rightUpClear=%d rightUpClearY=%.6f "
        "rightLeadProbe=%u rightLeadGroundY=%.6f rightProbeTravel=%.6f "
        "rightHeightSource=%s rightUseNative=%d "
        "rightRawGrounded=%d rightRawFresh=%d rightSolverFresh=%d "
        "rightLastHitY=%.6f rightHeelHitY=%.6f "
        "rightCalculatedFootY=%.6f rightLegIkY=%.6f "
        "rightInStair=%d "
        "rightHitNormal=(%.6f,%.6f,%.6f) "
        "rightHeightPending=%d rightPendingY=%.6f "
        "rightPendingSeconds=%.6f rightHeightSwitches=%u "
        "rightEdgeHeld=%d rightEdgeCandidate=%d rightEdgeReleased=%d "
        "rightEdgeCandidateY=%.6f rightEdgeTravel=%.6f "
        "rightEdgeSeconds=%.6f rightEdgeByTravel=%d "
        "rightEdgeReleaseDistance=%.6f rightEdgeHeldSamples=%u "
        "groundHeightOffset=%.6f groundHeightOffsetValid=%d "
        "support=%s rootSupport=%s "
        "rootOffset=%.6f rootTargetOffset=%.6f "
        "rootRawCandidateValid=%d rootRawCandidate=%.6f "
        "rootLevelPending=%d rootPendingOffset=%.6f "
        "rootPendingSeconds=%.6f rootLevelSwitches=%u "
        "vmdXZOwner=1 airborneVmdYOwner=1 upwardCollisionConstraint=1 "
        "nativeFullTargetCopy=0 "
        "terrainDatum=Grounding.Raycast-delegate-authored-foot-xz "
        "rootDatum=entry-surface-delta footDatum=absolute-bind-clearance "
        "rootReachCompensation=contact-only "
        "grounderPredictionFieldsDiagnosticOnly=1 "
        "solverTargetFallback=0 supportGate=authored-contact-only "
        "terrainQueries=%llu terrainHits=%llu terrainMisses=%llu "
        "terrainInvokeFailures=%llu "
        "generation=%llu owner=%p tid=%lu",
        frame, s_ghostRig.lastSourceFrame, deltaSeconds,
        DirectVmdTerrainContactStateName(output.feet[0].contact),
        grounder.authoredLift[0],
        grounder.authoredVerticalVelocity[0],
        output.feet[0].flatTarget.y,
        grounder.cachedTarget[0].y,
        heightSelection[0].groundY,
        grounder.directNativeTargetY[0],
        rawFootCorrectionForLog[0],
        stableNativeTargetForLog[0],
        sourceClearanceForLog[0],
        s_ghostRig.terrainAnkleClearance[0],
        grounder.cachedHeightFromGround[0],
        physicsFreshForLog[0] ? 1 : 0,
        physicsHeldForLog[0] ? 1 : 0,
        grounder.cachedFootPhysicsPlane[0].height,
        grounder.footPhysicsReferenceGroundY[0],
        stableHeightOutput[0].stableHeight,
        static_cast<unsigned>(
            grounder.cachedFootPhysicsPlane[0].hitCount),
        output.feet[0].normal.x,
        output.feet[0].normal.y,
        output.feet[0].normal.z,
        expectedAnkleForLog[0],
        heightSelection[0].referenceError,
        heightSelection[0].acceptedGroundCandidateCount,
        maximumTreadDistanceForLog[0],
        grounder.hybridTargetY[0],
        supportPlatformForLog[0],
        supportPlatformValidForLog[0] ? 1 : 0,
        grounder.upwardClearanceApplied[0] ? 1 : 0,
        grounder.upwardClearanceTargetY[0],
        static_cast<unsigned>(
            grounder.cachedFootDirectionalClearance[0].probeIndex),
        grounder.cachedFootDirectionalClearance[0].height,
        grounder.cachedFootDirectionalClearance[0].motionDistance,
        DirectVmdGrounderHeightSourceName(
            grounder.nativeHeightSource[0]),
        grounder.hybridUsesGrounder[0] ? 1 : 0,
        grounder.cachedRawLegGrounded[0] ? 1 : 0,
        rawFreshForLog[0] ? 1 : 0,
        solverFreshForLog[0] ? 1 : 0,
        grounder.cachedLastHitPoint[0].y,
        grounder.cachedHeelHitPoint[0].y,
        grounder.cachedCalculatedFoot[0].y,
        grounder.cachedLegIkPosition[0].y,
        grounder.cachedLegInStair[0] ? 1 : 0,
        grounder.cachedLastHitNormal[0].x,
        grounder.cachedLastHitNormal[0].y,
        grounder.cachedLastHitNormal[0].z,
        stableHeightOutput[0].pending ? 1 : 0,
        stableHeightOutput[0].pendingHeight,
        stableHeightOutput[0].pendingSeconds,
        stableHeightOutput[0].switchCount,
        contactEdgeOutput[0].held ? 1 : 0,
        contactEdgeOutput[0].candidate ? 1 : 0,
        contactEdgeOutput[0].released ? 1 : 0,
        contactEdgeOutput[0].candidateHeight,
        contactEdgeOutput[0].candidateTravel,
        contactEdgeOutput[0].candidateSeconds,
        contactEdgeOutput[0].releasedByTravel ? 1 : 0,
        s_ghostRig.terrainConfig.grounderContactEdgeReleaseDistance,
        static_cast<unsigned>(
            contactEdgeOutput[0].heldSampleCount),
        DirectVmdTerrainContactStateName(output.feet[1].contact),
        grounder.authoredLift[1],
        grounder.authoredVerticalVelocity[1],
        output.feet[1].flatTarget.y,
        grounder.cachedTarget[1].y,
        heightSelection[1].groundY,
        grounder.directNativeTargetY[1],
        rawFootCorrectionForLog[1],
        stableNativeTargetForLog[1],
        sourceClearanceForLog[1],
        s_ghostRig.terrainAnkleClearance[1],
        grounder.cachedHeightFromGround[1],
        physicsFreshForLog[1] ? 1 : 0,
        physicsHeldForLog[1] ? 1 : 0,
        grounder.cachedFootPhysicsPlane[1].height,
        grounder.footPhysicsReferenceGroundY[1],
        stableHeightOutput[1].stableHeight,
        static_cast<unsigned>(
            grounder.cachedFootPhysicsPlane[1].hitCount),
        output.feet[1].normal.x,
        output.feet[1].normal.y,
        output.feet[1].normal.z,
        expectedAnkleForLog[1],
        heightSelection[1].referenceError,
        heightSelection[1].acceptedGroundCandidateCount,
        maximumTreadDistanceForLog[1],
        grounder.hybridTargetY[1],
        supportPlatformForLog[1],
        supportPlatformValidForLog[1] ? 1 : 0,
        grounder.upwardClearanceApplied[1] ? 1 : 0,
        grounder.upwardClearanceTargetY[1],
        static_cast<unsigned>(
            grounder.cachedFootDirectionalClearance[1].probeIndex),
        grounder.cachedFootDirectionalClearance[1].height,
        grounder.cachedFootDirectionalClearance[1].motionDistance,
        DirectVmdGrounderHeightSourceName(
            grounder.nativeHeightSource[1]),
        grounder.hybridUsesGrounder[1] ? 1 : 0,
        grounder.cachedRawLegGrounded[1] ? 1 : 0,
        rawFreshForLog[1] ? 1 : 0,
        solverFreshForLog[1] ? 1 : 0,
        grounder.cachedLastHitPoint[1].y,
        grounder.cachedHeelHitPoint[1].y,
        grounder.cachedCalculatedFoot[1].y,
        grounder.cachedLegIkPosition[1].y,
        grounder.cachedLegInStair[1] ? 1 : 0,
        grounder.cachedLastHitNormal[1].x,
        grounder.cachedLastHitNormal[1].y,
        grounder.cachedLastHitNormal[1].z,
        stableHeightOutput[1].pending ? 1 : 0,
        stableHeightOutput[1].pendingHeight,
        stableHeightOutput[1].pendingSeconds,
        stableHeightOutput[1].switchCount,
        contactEdgeOutput[1].held ? 1 : 0,
        contactEdgeOutput[1].candidate ? 1 : 0,
        contactEdgeOutput[1].released ? 1 : 0,
        contactEdgeOutput[1].candidateHeight,
        contactEdgeOutput[1].candidateTravel,
        contactEdgeOutput[1].candidateSeconds,
        contactEdgeOutput[1].releasedByTravel ? 1 : 0,
        s_ghostRig.terrainConfig.grounderContactEdgeReleaseDistance,
        static_cast<unsigned>(
            contactEdgeOutput[1].heldSampleCount),
        grounder.cachedGroundHeightOffset,
        grounder.cachedGroundHeightOffsetValid ? 1 : 0,
        DirectVmdTerrainSupportStateName(output.support),
        DirectVmdTerrainSupportStateName(output.rootSupport),
        output.rootOffset, output.rootTargetOffset,
        rootCandidate.valid ? 1 : 0, rootCandidate.offset,
        stableRootOutput.pending ? 1 : 0,
        stableRootOutput.pendingHeight,
        stableRootOutput.pendingSeconds,
        stableRootOutput.switchCount,
        (unsigned long long)grounder.footPhysicsQueryCount,
        (unsigned long long)grounder.footPhysicsHitCount,
        (unsigned long long)grounder.footRaycastMissCount,
        (unsigned long long)grounder.footRaycastInvokeFailureCount,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
}

static bool GhostRig_PreparePhase5Legs(int frame, void *bipedIK) {
  if (!GhostRig_RequireMainThread("GhostRig.PreparePhase5Legs", false) ||
      s_ghostRig.state != GhostRigState::Alive ||
      !g_motionBackend.Is(MotionBackend::DirectVmd) ||
      s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
          s_ghostRig.generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) !=
          s_ghostRig.ownerCharacter ||
      !GhostRig_ResolvePhase5FinalIkSolvers(bipedIK))
    return false;

  const bool grounderRequested = s_ghostRig.terrainEnabledLast &&
      s_directVmdTerrainDesiredEnabled.load(std::memory_order_acquire);
  const bool grounderActive = grounderRequested
      ? GhostRig_ActivateGrounderForTerrain(bipedIK, frame)
      : false;
  if (!grounderRequested)
    GhostRig_SuppressGrounderForFlatMode(frame);

  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT; ++index) {
    const DirectVmdLegSide side =
        static_cast<DirectVmdLegSide>(index);
    void *solver = GhostRig_GetLegSolver(side);
    const bool ownershipReady = !solver ||
        (grounderActive
             ? GhostRig_DetachLegTargetOnly(solver)
             : GhostRig_SaveAndIsolateLegSolverExternalOwnership(
                   side, solver));
    if (!ownershipReady) {
      Log("[P5-FINALIK-ISOLATE] side=%s isolated=0 "
          "reason=per-frame-enforcement-failed frame=%d "
          "grounderActive=%d action=suppress-biped generation=%llu "
          "owner=%p tid=%lu",
          GhostRig_LegSideName(side), frame,
          grounderActive ? 1 : 0,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
      return false;
    }
  }

  const int auxiliaryZeroed = GhostRig_ZeroAllAuxSolverWeights();
  if (!GhostRig_StabilizeTargetHipsBindPosition(frame,
                                                "pre-finalik")) {
    Log("[P5-HIPS-STABILIZE] stage=pre-finalik unityFrame=%d "
        "written=0 action=suppress-biped generation=%llu owner=%p "
        "tid=%lu",
        frame, (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    return false;
  }
  if (!GhostRig_StabilizePelvisNodeBindPosition(frame, "pre-finalik")) {
    Log("[P5-PELVIS-NODE] stage=pre-finalik unityFrame=%d written=0 "
        "action=suppress-biped generation=%llu owner=%p tid=%lu",
        frame, (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    return false;
  }

  const bool playbackOwnsPose =
      s_ghostRig.playbackAnchorCaptured && s_ghostRig.lastSampleAccepted &&
      s_ghostRig.lastSamplePlayback != DirectVmdPlaybackState::Stopped;
  const bool periodicLog =
      frame >= 0
          ? (s_ghostRig.lastLegLogFrame == INT_MIN ||
             frame - s_ghostRig.lastLegLogFrame >= 120)
          : (s_ghostRig.lastLegPreparedFrame == INT_MIN);
  const bool leftOk = GhostRig_PreparePhase5Leg(
      DirectVmdLegSide::Left, frame, playbackOwnsPose,
      s_ghostRig.lastLeftFootIkEnabled, periodicLog);
  const bool rightOk = GhostRig_PreparePhase5Leg(
      DirectVmdLegSide::Right, frame, playbackOwnsPose,
      s_ghostRig.lastRightFootIkEnabled, periodicLog);
  GhostRig_UpdateGrounderHybridTerrain(
      frame, grounderActive, playbackOwnsPose);
  if (s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
          s_ghostRig.generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) !=
          s_ghostRig.ownerCharacter)
    return false;
  if (periodicLog)
    s_ghostRig.lastLegLogFrame = frame;
  if (periodicLog) {
    Log("[P5-FINALIK-AUX] unityFrame=%d zeroed=%d expectedMax=%u "
        "upperBodyOwner=DirectVmd pelvisMotionOwner=%s "
        "hipsBindPositionOwner=%s "
        "nativeWeightsRestoreOnCleanup=1 generation=%llu owner=%p "
        "tid=%lu",
        frame, auxiliaryZeroed, GHOST_AUX_SOLVER_COUNT,
        grounderActive ? "DirectVmd-Grounder-root-offset" : "Root",
        "Phase5Stabilizer",
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
  if (!leftOk && !rightOk)
    return false;
  s_ghostRig.lastLegPreparedFrame = frame;
  GhostRig_LogOrder("GhostRig.Phase5LegTargets.complete", frame,
                    s_ghostRig.ownerCharacter, s_ghostRig.generation);
  return true;
}

static void GhostRig_ApplyKneeSolverInput(DirectVmdLegSide side, void *solver, int frame) {
  GhostRigLegRuntime &leg = GhostRig_LegRuntime(side);
  if (!solver || !GhostRig_RequireMainThread("KneeMix.Apply", false) ||
      !g_motionBackend.Is(MotionBackend::DirectVmd) ||
      s_ghostRequestedGeneration.load(std::memory_order_acquire) != s_ghostRig.generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) != s_ghostRig.ownerCharacter ||
      !s_ghostRig.lastSampleAccepted || !leg.effectiveIkEnabled ||
      leg.preparedFrame != frame || !leg.kneeReferenceReady ||
      !leg.reach.valid || leg.kneeMixWeight <= 0 || leg.sourceKnee.confidence <= 0)
    return;
  const auto thigh = kDirectVmdPhase5LegFkBones[static_cast<uint32_t>(side)][0];
  const auto &target = s_ghostRig.targets[DirectVmdBoneIndex(thigh)];
  VmdVec3 hip{}, knee{};
  if (!target.lastDesiredValid || target.lastDesiredFrame != frame ||
      !GhostRig_EvaluateExpectedLegSeedPositions(side, frame, &hip, &knee)) return;
  auto source = leg.sourceKnee;
  source.direction = DirectVmdRotateVector(s_ghostRig.anchorRotation,
      SourceToGameBasis::ConvertPosition(source.direction));
  const auto animationNormal = DirectVmdRotateVector(
      target.lastDesiredWorldRotation, leg.kneeDefaultLocalNormal);
  leg.kneeMix = DirectVmdMixKneeDirection(DirectVmdSub(leg.reach.solverTarget, hip),
      animationNormal, source, GhostRig_LegMaximumReach(side), leg.kneeMixWeight);
  if (!leg.kneeMix.applied) return;
  __try {
    leg.kneeOverride.Apply(
        reinterpret_cast<VmdVec3 *>((char *)solver + leg.kneeNormalOffset),
        reinterpret_cast<VmdVec3 *>((char *)solver + leg.kneeBackupOffset),
        reinterpret_cast<float *>((char *)solver + OFF_IKLIMB_BEND_WEIGHT),
        leg.kneeMix.normal);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    GhostRig_RestoreKneeOverride(leg, solver);
    leg.kneeMix.applied = false;
  }
  if (frame == s_ghostRig.lastLegLogFrame)
    Log("[P5-KNEE-MIX] side=%s frame=%d sourceConfidence=%.4f weight=%.4f effective=%.4f "
        "injected=%d consumer=bendNormal modifierWeight=0 footTargetUnchanged=1 sourceRevision=%llu",
        GhostRig_LegSideName(side), frame, source.confidence, leg.kneeMixWeight,
        leg.kneeMix.effectiveWeight, leg.kneeOverride.active ? 1 : 0,
        (unsigned long long)s_ghostRig.bindSourceRevision);
}

static bool GhostRig_BeforeLegSolverUpdate(void *solver,
                                            void *methodInfo) {
  if (!solver || !GhostRig_RequireMainThread(
                     "IKSolverTrigonometric.OnUpdate.DirectVmd", false) ||
      !GhostRig_IsRequestedEnabled() ||
      !g_motionBackend.Is(MotionBackend::DirectVmd) ||
      s_ghostRig.state != GhostRigState::Alive ||
      g_clothPlaybackGate.BlocksPose(uint32_t(MotionBackend::DirectVmd),g_motionBackend.Generation()))
    return false;
  const bool generationCurrent =
      s_ghostRequestedGeneration.load(std::memory_order_acquire) ==
          s_ghostRig.generation &&
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) ==
          s_ghostRig.ownerCharacter;
  if (!generationCurrent) {
    for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT; ++index) {
      if (GhostRig_GetLegSolver(
              static_cast<DirectVmdLegSide>(index)) == solver)
        GhostRig_ClearLegSolverFields(solver);
    }
    return false;
  }
  const int frame = GhostRig_GetFrameCount();
  if (frame != s_ghostRig.lastLegPreparedFrame)
    return false;
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT; ++index) {
    const DirectVmdLegSide side =
        static_cast<DirectVmdLegSide>(index);
    GhostRigLegRuntime &leg = s_ghostRig.legs[index];
    if (GhostRig_GetLegSolver(side) != solver ||
        leg.preparedFrame != frame)
      continue;
    leg.onUpdateMethodInfo = methodInfo;
    const bool grounderOwnsTerrain =
        s_ghostRig.grounder.active && s_ghostRig.terrainEnabledLast &&
        s_directVmdTerrainDesiredEnabled.load(
            std::memory_order_acquire);
    if (grounderOwnsTerrain) {
      if (!GhostRig_DetachLegTargetOnly(solver)) {
        GhostRig_ClearLegSolverFields(solver);
        return false;
      }
      bool written = false;
      if (leg.effectiveIkEnabled) {
        GhostRigGrounderRuntime &grounder = s_ghostRig.grounder;
        const VmdVec3 flatDesired = leg.reach.desiredTarget;
        VmdVec3 hybridDesired = flatDesired;
        const bool hybridReady = grounder.contactFrame == frame &&
            grounder.hybridTargetValid[index];
        hybridDesired.y = hybridReady
            ? grounder.hybridTargetY[index]
            : flatDesired.y + s_ghostRig.terrainState.rootOffset;
        const float verticalCorrection =
            hybridDesired.y - flatDesired.y;
        leg.footIkParentWorldPosition.y += verticalCorrection;
        leg.toeIkWorldPosition.y += verticalCorrection;
        leg.terrainContactApplied = hybridReady &&
            grounder.hybridUsesGrounder[index];
        VmdVec3 hipPosition = {};
        VmdVec3 kneePosition = {};
        const bool expectedPoseOk =
            GhostRig_EvaluateExpectedLegSeedPositions(
                side, frame, &hipPosition, &kneePosition);
        DirectVmdReachProjection projected = expectedPoseOk
            ? DirectVmdProjectLegReach(
                  hipPosition, hybridDesired,
                  GhostRig_LegMaximumReach(side), 0.0f)
            : DirectVmdReachProjection();
        if (projected.valid) {
          leg.reach = projected;
          leg.bendDirection = GhostRig_EvaluateBendDirection(
              hipPosition, kneePosition, projected.solverTarget);
          written = GhostRig_WriteLegSolverFields(
              solver, 1.0f, projected.solverTarget,
              0.0f, leg.footWorldRotation);
        }
      } else {
        written = GhostRig_ClearLegSolverFields(solver);
        if (written)
          ++leg.weightClearCount;
      }
      if (written) {
        leg.innerWriteFrame = frame;
        ++leg.solverWriteCount;
        GhostRig_ApplyKneeSolverInput(side, solver, frame);
      }
      GhostRig_LogOrder(
          leg.effectiveIkEnabled
              ? (s_ghostRig.grounder.hybridUsesGrounder[index]
                     ? "Grounder.HybridY.vmdXZ.contact.inner"
                     : "Grounder.HybridY.vmdAir.inner")
              : "Grounder.LegTarget.ikOffClear.inner",
          frame, s_ghostRig.ownerCharacter, s_ghostRig.generation);
      return written;
    }
    if (!GhostRig_SaveAndIsolateLegSolverExternalOwnership(side,
                                                            solver)) {
      GhostRig_ClearLegSolverFields(solver);
      return false;
    }
    bool written = false;
    if (leg.effectiveIkEnabled) {
      __try {
        *reinterpret_cast<int *>((char *)solver +
                                 OFF_IKLIMB_BEND_MODIFIER) = 0;
        *reinterpret_cast<float *>((char *)solver +
                                   OFF_IKLIMB_BEND_WEIGHT) = 1.0f;
      } __except (EXCEPTION_EXECUTE_HANDLER) {
      }
      written = GhostRig_WriteLegSolverFields(
          solver, 1.0f, leg.reach.solverTarget, 0.0f,
          leg.footWorldRotation);
    } else {
      written = GhostRig_ClearLegSolverFields(solver);
      if (written)
        ++leg.weightClearCount;
    }
    if (written) {
      leg.innerWriteFrame = frame;
      ++leg.solverWriteCount;
      GhostRig_ApplyKneeSolverInput(side, solver, frame);
    }
    GhostRig_LogOrder(
        leg.effectiveIkEnabled
            ? "FinalIK.LegSolver.innerWrite.ikOn"
            : "FinalIK.LegSolver.innerWrite.ikOff",
        frame, s_ghostRig.ownerCharacter, s_ghostRig.generation);
    return written;
  }
  return false;
}

static bool GhostRig_ResolvePhase7LegsAfterHipsCorrection(int frame) {
  const bool hipsCorrected =
      s_ghostRig.lastHipsCorrectionFrame == frame &&
      DirectVmdFinite(s_ghostRig.lastHipsCorrectionDistance) &&
      s_ghostRig.lastHipsCorrectionDistance > 1.0e-3f;
  const GhostRigPelvisNodeRuntime &pelvisNode = s_ghostRig.pelvisNode;
  const float pelvisNodeCorrection =
      pelvisNode.handle && pelvisNode.lastCorrectionFrame == frame &&
              DirectVmdFinite(pelvisNode.lastCorrectionDistance)
          ? pelvisNode.lastCorrectionDistance
          : 0.0f;
  if (!s_ghostRig.terrainEnabledLast ||
      !s_ghostRig.terrainFrameValid ||
      s_ghostRig.lastTerrainFrame != frame ||
      (!hipsCorrected && pelvisNodeCorrection <= 1.0e-3f))
    return true;

  typedef void (__fastcall *OnUpdateNativeFn)(void *, void *);
  OnUpdateNativeFn onUpdate =
      reinterpret_cast<OnUpdateNativeFn>(g_origIkTrigOnUpdate);
  int attempted = 0;
  int solved = 0;
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT; ++index) {
    GhostRigLegRuntime &leg = s_ghostRig.legs[index];
    if (!leg.effectiveIkEnabled || leg.preparedFrame != frame)
      continue;
    ++attempted;
    void *solver = GhostRig_GetLegSolver(
        static_cast<DirectVmdLegSide>(index));
    bool completed = false;
    if (solver && onUpdate) {
      __try {
        *reinterpret_cast<int *>((char *)solver +
                                 OFF_IKLIMB_BEND_MODIFIER) = 0;
        *reinterpret_cast<float *>((char *)solver +
                                   OFF_IKLIMB_BEND_WEIGHT) = 1.0f;
        if (GhostRig_WriteLegSolverFields(
                solver, 1.0f, leg.reach.solverTarget, 0.0f,
                leg.footWorldRotation)) {
          ++leg.solverWriteCount;
          GhostRig_ApplyKneeSolverInput(static_cast<DirectVmdLegSide>(index), solver, frame);
          onUpdate(solver, leg.onUpdateMethodInfo);
          completed = true;
        }
      } __except (EXCEPTION_EXECUTE_HANDLER) {
        completed = false;
      }
      GhostRig_RestoreKneeOverride(leg, solver);
    }
    if (completed) {
      leg.postTerrainResolveFrame = frame;
      ++leg.postTerrainResolveCount;
      ++solved;
    }
  }

  s_ghostRig.terrainFinalIkResolveCount += solved;
  const bool periodic =
      s_ghostRig.lastTerrainFinalIkResolveLogFrame == INT_MIN ||
      frame < 0 ||
      frame - s_ghostRig.lastTerrainFinalIkResolveLogFrame >= 120;
  if (periodic || solved != attempted) {
    s_ghostRig.lastTerrainFinalIkResolveLogFrame = frame;
    Log("[P7-FINALIK-RESOLVE] unityFrame=%d vmdFrame=%.6f "
        "hipsCorrectionDistance=%.6f pelvisNodeCorrectionDistance=%.6f "
        "attempted=%d solved=%d "
        "leftSolved=%d rightSolved=%d rootOffset=%.6f "
        "support=%s rootSupport=%s totalSolves=%llu "
        "legOnly=1 spine=0 look=0 aim=0 pelvis=0 "
        "sameFrameFinalOutput=1 nextFrameFeedback=0 "
        "generation=%llu owner=%p tid=%lu",
        frame, s_ghostRig.lastSourceFrame,
        s_ghostRig.lastHipsCorrectionDistance, pelvisNodeCorrection,
        attempted, solved,
        s_ghostRig.legs[0].postTerrainResolveFrame == frame ? 1 : 0,
        s_ghostRig.legs[1].postTerrainResolveFrame == frame ? 1 : 0,
        s_ghostRig.terrainFrame.rootOffset,
        DirectVmdTerrainSupportStateName(
            s_ghostRig.terrainFrame.support),
        DirectVmdTerrainSupportStateName(
            s_ghostRig.terrainFrame.rootSupport),
        (unsigned long long)s_ghostRig.terrainFinalIkResolveCount,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }
  return solved == attempted;
}

static void GhostRig_ApplyPhase5ToeAimAfterFinalIk(int frame) {
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT; ++index) {
    const DirectVmdLegSide side =
        static_cast<DirectVmdLegSide>(index);
    GhostRigLegRuntime &leg = s_ghostRig.legs[index];
    if (!leg.effectiveIkEnabled || !leg.toeAimPending ||
        leg.preparedFrame != frame)
      continue;
    leg.toeAimPending = false;
    const DirectVmdBoneId ankleId =
        kDirectVmdPhase5LegFkBones[index][2];
    const DirectVmdBoneId toeId =
        kDirectVmdPhase5LegFkBones[index][3];
    void *ankleTransform = GhostRig_GetTargetTransform(ankleId);
    void *toeTransform = GhostRig_GetTargetTransform(toeId);
    Vec3 anklePositionUnity = {};
    Vec3 toePositionUnity = {};
    Quat ankleRotation = {0.0f, 0.0f, 0.0f, 1.0f};
    const bool poseOk =
        ankleTransform && toeTransform &&
        GhostRig_ReadWorldPosition(ankleTransform,
                                   anklePositionUnity) &&
        GhostRig_ReadWorldPosition(toeTransform, toePositionUnity) &&
        GhostRig_ReadWorldRotation(ankleTransform, ankleRotation);
    const VmdVec3 anklePosition = {anklePositionUnity.x,
                                   anklePositionUnity.y,
                                   anklePositionUnity.z};
    const VmdVec3 toePosition = {toePositionUnity.x,
                                 toePositionUnity.y,
                                 toePositionUnity.z};
    leg.toeAim = poseOk
        ? DirectVmdSolveToeAim(
              anklePosition, toePosition, leg.toeIkWorldPosition,
              {ankleRotation.x, ankleRotation.y, ankleRotation.z,
               ankleRotation.w})
        : DirectVmdToeAimResult();
    if (leg.toeAim.valid && leg.terrainContactApplied) {
      leg.toeAim.solverAnkleWorldRotation =
          DirectVmdQuaternionMultiply(
              leg.terrainNormalRotation,
              leg.toeAim.solverAnkleWorldRotation);
    }
    const bool written =
        leg.toeAim.valid &&
        GhostRig_WriteWorldRotation(
            ankleTransform,
            {leg.toeAim.solverAnkleWorldRotation.x,
             leg.toeAim.solverAnkleWorldRotation.y,
             leg.toeAim.solverAnkleWorldRotation.z,
             leg.toeAim.solverAnkleWorldRotation.w});
    leg.toeAimWritten = written;
    if (written) {
      ++leg.toeAimWriteCount;
      GhostRigTargetBone &target =
          s_ghostRig.targets[DirectVmdBoneIndex(ankleId)];
      target.lastDesiredWorldRotation =
          leg.toeAim.solverAnkleWorldRotation;
      target.lastDesiredFrame = frame;
      target.lastDesiredValid = true;
    } else {
      Log("[P5-TOE-AIM] side=%s unityFrame=%d vmdFrame=%.6f "
          "written=0 poseOk=%d aimValid=%d "
          "reason=toe-position-target-unavailable generation=%llu "
          "owner=%p tid=%lu",
          GhostRig_LegSideName(side), frame, s_ghostRig.lastSourceFrame,
          poseOk ? 1 : 0, leg.toeAim.valid ? 1 : 0,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    }
  }
}

static void GhostRig_LogNodeSnapshot(int frame, const char *eventName) {
  if (s_ghostRig.state != GhostRigState::Alive)
    return;
  for (int i = 0; i < GHOST_NODE_COUNT; ++i) {
    const bool fullExport = eventName && strcmp(eventName, "created") == 0;
    const int semantic = i - 1;
    const bool selected =
        semantic == static_cast<int>(DirectVmdBoneId::Center) ||
        semantic == static_cast<int>(DirectVmdBoneId::Groove) ||
        semantic == static_cast<int>(DirectVmdBoneId::LeftArm) ||
        semantic == static_cast<int>(DirectVmdBoneId::RightArm) ||
        semantic == static_cast<int>(DirectVmdBoneId::LeftLeg) ||
        semantic == static_cast<int>(DirectVmdBoneId::RightLeg) ||
        semantic == static_cast<int>(DirectVmdBoneId::LeftAnkle) ||
        semantic == static_cast<int>(DirectVmdBoneId::RightAnkle) ||
        semantic == static_cast<int>(DirectVmdBoneId::LeftFootIk) ||
        semantic == static_cast<int>(DirectVmdBoneId::RightFootIk);
    if (!fullExport && !selected)
      continue;
    void *gameObject = GhostRig_GetGameObject(i);
    void *transform = GhostRig_GetTransform(i);
    void *actualParent = transform && g_transform_get_parent
                             ? Invoke(g_transform_get_parent, transform)
                             : nullptr;
    const int expectedParentIndex = GhostRig_NodeParent(i);
    void *expectedParent = expectedParentIndex >= 0
                               ? GhostRig_GetTransform(expectedParentIndex)
                               : nullptr;
    Vec3 localPosition = {};
    Quat localRotation = {0, 0, 0, 1};
    Vec3 worldPosition = {};
    Quat worldRotation = {0, 0, 0, 1};
    bool localPositionOk =
        GhostRig_ReadLocalPosition(transform, localPosition);
    bool localRotationOk =
        GhostRig_ReadLocalRotation(transform, localRotation);
    bool worldPositionOk =
        GhostRig_ReadWorldPosition(transform, worldPosition);
    bool worldRotationOk =
        GhostRig_ReadWorldRotation(transform, worldRotation);
    int components = GhostRig_GetComponentCount(gameObject);
    int hideFlags = GhostRig_GetHideFlags(gameObject);
    uint64_t seq = GhostRig_NextOrderSequence();
    Log("[P2-GHOST-POSE] event=%s unityFrame=%d sourceFrame=%.6f "
        "sampleSeq=%llu logSeq=%llu tid=%lu generation=%llu owner=%p "
        "motionScale=%.8f index=%d name='%s' go=%p transform=%p "
        "parentExpected=%p parentActual=%p parentOk=%d components=%d "
        "emptyOk=%d hideFlags=0x%X localOk=%d/%d "
        "localP=(%.5f,%.5f,%.5f) localR=(%.6f,%.6f,%.6f,%.6f) "
        "worldOk=%d/%d worldP=(%.5f,%.5f,%.5f) "
        "worldR=(%.6f,%.6f,%.6f,%.6f)",
        eventName, frame, s_ghostRig.lastSourceFrame,
        (unsigned long long)s_ghostRig.lastSampleSequence,
        (unsigned long long)seq, GetCurrentThreadId(),
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        s_ghostRig.motionScale, i, GhostRig_NodeName(i), gameObject,
        transform, expectedParent,
        actualParent, GhostRig_SameUnityObject(actualParent, expectedParent)
                          ? 1
                          : 0,
        components,
        components == 1 ? 1 : 0, hideFlags, localPositionOk ? 1 : 0,
        localRotationOk ? 1 : 0, localPosition.x, localPosition.y,
        localPosition.z, localRotation.x, localRotation.y, localRotation.z,
        localRotation.w, worldPositionOk ? 1 : 0,
        worldRotationOk ? 1 : 0, worldPosition.x, worldPosition.y,
        worldPosition.z, worldRotation.x, worldRotation.y, worldRotation.z,
        worldRotation.w);
  }
}

static bool GhostRig_CreateMainThread(uint64_t generation,
                                      uintptr_t ownerCharacter,
                                      int sceneHandle, void *ownerRoot) {
  if (!GhostRig_RequireMainThread("GhostRig.Create", false))
    return false;
  if (s_ghostRig.state != GhostRigState::Empty || !ownerRoot)
    return false;
  if (s_ghostRig.bindState != GhostBindCaptureState::Ready ||
      s_ghostRig.bindGeneration != generation ||
      s_ghostRig.bindOwnerCharacter != ownerCharacter)
    return false;

  if (!g_gameObjectClass || !s_ghostGameObjectCtor ||
      !g_gameObject_get_transform || !s_ghostTransformSetParent ||
      !g_transform_get_parent || !g_transform_get_localPosition ||
      !g_transform_get_localRotation || !g_transform_set_localPosition ||
      !g_transform_set_localRotation || !s_ghostObjectGetHideFlags ||
      !s_ghostObjectSetHideFlags || !s_ghostObjectDestroy ||
      !s_ghostGameObjectGetComponents || !g_camGetPos || !g_camGetRot ||
      (!g_origSetPos && !g_camSetPos) ||
      (!g_origSetRot && !g_camSetRot) ||
      !il2cpp_object_new || !il2cpp_string_new || !il2cpp_gchandle_new ||
      !il2cpp_gchandle_get_target || !il2cpp_gchandle_free) {
    Log("[P0-GHOST-CREATE] blocked reason=missing-api tid=%lu generation=%llu "
        "owner=%p ctor=%p setParent=%p hide=%p destroy=%p",
        GetCurrentThreadId(), (unsigned long long)generation,
        reinterpret_cast<void *>(ownerCharacter), s_ghostGameObjectCtor,
        s_ghostTransformSetParent, s_ghostObjectSetHideFlags,
        s_ghostObjectDestroy);
    g_motionBackend.TransitionTo(MotionBackend::Native);
    GhostRig_RequestEnabled(false, GhostRigCleanupReason::CreateFailed);
    DirectVmdRuntime_RequestStop();
    return false;
  }

  s_ghostRig.state = GhostRigState::Creating;
  s_ghostRig.generation = generation;
  s_ghostRig.ownerCharacter = ownerCharacter;
  GhostRig_FreeOwnerRootHandle();
  s_ghostRig.ownerRootHandle = 0;
  s_ghostRig.sceneHandle = sceneHandle;
  s_ghostRig.lastAppliedFrame = INT_MIN;
  s_ghostRig.lastPoseLogFrame = INT_MIN;
  s_ghostRig.lastTargetAppliedFrame = INT_MIN;
  s_ghostRig.lastFingerAppliedFrame = INT_MIN;
  s_ghostRig.lastEyeAppliedFrame = INT_MIN;
  s_ghostRig.lastTwistAppliedFrame = INT_MIN;
  s_ghostRig.lastTargetLogFrame = INT_MIN;
  s_ghostRig.lastFingerLogFrame = INT_MIN;
  s_ghostRig.lastEyeLogFrame = INT_MIN;
  s_ghostRig.lastTwistLogFrame = INT_MIN;
  s_ghostRig.lastPostFinalIkLogFrame = INT_MIN;
  s_ghostRig.lastFinalIkSuppressedLogFrame = INT_MIN;
  s_ghostRig.lastRootAppliedFrame = INT_MIN;
  s_ghostRig.lastRootLogFrame = INT_MIN;
  s_ghostRig.lastHipsCorrectionFrame = INT_MIN;
  s_ghostRig.lastHipsCorrectionLogFrame = INT_MIN;
  s_ghostRig.lastLegPreparedFrame = INT_MIN;
  s_ghostRig.lastLegLogFrame = INT_MIN;
  s_ghostRig.applyCount = 0;
  s_ghostRig.targetApplyCount = 0;
  s_ghostRig.targetRotationWriteCount = 0;
  s_ghostRig.fingerRotationWriteCount = 0;
  s_ghostRig.eyeRotationWriteCount = 0;
  s_ghostRig.twistRotationWriteCount = 0;
  s_ghostRig.postFinalIkDiagnosticReadCount = 0;
  s_ghostRig.finalIkSuppressedCount = 0;
  s_ghostRig.rootApplyCount = 0;
  s_ghostRig.rootWorldWriteCount = 0;
  s_ghostRig.rootRestoreCount = 0;
  s_ghostRig.hipsBindPositionWriteCount = 0;
  s_ghostRig.hipsPostCorrectionCount = 0;
  s_ghostRig.finalIkOriginalRunCount = 0;
  s_ghostRig.finalIkSafetySuppressCount = 0;
  s_ghostRig.cadenceUnityFrames = 0;
  s_ghostRig.cadenceSampleAdvances = 0;
  s_ghostRig.cadenceRepeatedSamples = 0;
  s_ghostRig.cadenceMaxSourceFrameStep = 0.0;
  s_ghostRig.destroyIssued = false;
  s_ghostRig.anchorValid = false;
  s_ghostRig.playbackAnchorCaptured = false;
  s_ghostRig.restoreAnchorValid = false;
  s_ghostRig.rootPlacementValid = false;
  s_ghostRig.rootMotionOwned = false;
  s_ghostRig.lastSampleAccepted = false;
  s_ghostRig.rootCycleSeen = false;
  s_ghostRig.anchorPosition = {0.0f, 0.0f, 0.0f};
  s_ghostRig.anchorRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  s_ghostRig.restoreAnchorPosition = {0.0f, 0.0f, 0.0f};
  s_ghostRig.restoreAnchorRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  s_ghostRig.rootPlacementOffset = {0.0f, 0.0f, 0.0f};
  s_ghostRig.lastDesiredRootPosition = {0.0f, 0.0f, 0.0f};
  s_ghostRig.lastDesiredRootRotation = {0.0f, 0.0f, 0.0f, 1.0f};
  s_ghostRig.lastDesiredRootValid = false;
  s_ghostRig.lastHipsPositionBeforeCorrection = {0.0f, 0.0f, 0.0f};
  s_ghostRig.lastHipsCorrectionDistance = 0.0f;
  s_ghostRig.lastSampleSequence = 0;
  s_ghostRig.lastPlaybackCycle = 0;
  s_ghostRig.lastAppliedRootCycle = 0;
  s_ghostRig.lastSourceFrame = 0.0;
  s_ghostRig.lastSamplePlayback = DirectVmdPlaybackState::Stopped;
  s_ghostRig.lastLeftFootIkEnabled = true;
  s_ghostRig.lastRightFootIkEnabled = true;
  s_ghostRig.finalIkBipedHandle = 0;
  s_ghostRig.eyeSmcHandle = 0;
  s_ghostRig.eyeLookAtSaved = false;
  s_ghostRig.eyeLookAtSavedValue = false;
  s_ghostRig.terrainEnabledLast = false;
  GhostRig_ResetPhase5LegState();
  GhostRig_ResetPhase7TerrainState();
  memset(s_ghostRig.targets, 0, sizeof(s_ghostRig.targets));
  for (int i = 0; i < GHOST_NODE_COUNT; ++i) {
    s_ghostRig.nodes[i].gameObjectHandle = 0;
    s_ghostRig.nodes[i].transformHandle = 0;
  }

  char ownerRootName[256] = {};
  SafeGetBoneName(ownerRoot, ownerRootName, sizeof(ownerRootName));
  Log("[P2-GHOST-CREATE] begin tid=%lu generation=%llu owner=%p "
      "scene=%d alignmentSource=Animator.transform ownerRoot=%p "
      "ownerRootName='%s' ghostRootParent=null hideFlags=0x%X",
      GetCurrentThreadId(), (unsigned long long)generation,
      reinterpret_cast<void *>(ownerCharacter), sceneHandle, ownerRoot,
      ownerRootName, GHOST_HIDE_FLAGS);

  s_ghostRig.ownerRootHandle = il2cpp_gchandle_new(ownerRoot, false);
  if (!s_ghostRig.ownerRootHandle) {
    Log("[P4-ROOT-OWNER] failed reason=gchandle root=%p generation=%llu "
        "owner=%p tid=%lu",
        ownerRoot, (unsigned long long)generation,
        reinterpret_cast<void *>(ownerCharacter), GetCurrentThreadId());
    GhostRig_DestroyMainThread(GhostRigCleanupReason::CreateFailed);
    return false;
  }
  Log("[P4-ROOT-OWNER] retained=1 handle=%u root=%p generation=%llu "
      "owner=%p mainThreadOnly=1 workerUnityPointers=0 tid=%lu",
      s_ghostRig.ownerRootHandle, ownerRoot,
      (unsigned long long)generation,
      reinterpret_cast<void *>(ownerCharacter), GetCurrentThreadId());

  for (int i = 0; i < GHOST_NODE_COUNT; ++i) {
    void *gameObject = il2cpp_object_new(g_gameObjectClass);
    const char *nodeName = GhostRig_NodeName(i);
    void *name = il2cpp_string_new(nodeName);
    if (!gameObject || !name) {
      Log("[P0-GHOST-CREATE] node-failed index=%d name='%s' reason=alloc",
          i, nodeName);
      GhostRig_DestroyMainThread(GhostRigCleanupReason::CreateFailed);
      return false;
    }
    void *ctorParams[] = {name};
    Invoke(s_ghostGameObjectCtor, gameObject, ctorParams);
    if (!GhostRig_IsUnityObjectAlive(gameObject)) {
      Log("[P0-GHOST-CREATE] node-failed index=%d name='%s' reason=ctor",
          i, nodeName);
      GhostRig_DestroyMainThread(GhostRigCleanupReason::CreateFailed);
      return false;
    }

    int hideFlagsValue = GHOST_HIDE_FLAGS;
    void *hideParams[] = {&hideFlagsValue};
    Invoke(s_ghostObjectSetHideFlags, gameObject, hideParams);
    uint32_t gameObjectHandle = il2cpp_gchandle_new(gameObject, false);
    void *transform = Invoke(g_gameObject_get_transform, gameObject);
    uint32_t transformHandle =
        transform ? il2cpp_gchandle_new(transform, false) : 0;
    if (!gameObjectHandle || !transformHandle) {
      Log("[P0-GHOST-CREATE] node-failed index=%d name='%s' "
          "reason=gchandle goHandle=%u transformHandle=%u",
          i, nodeName, gameObjectHandle, transformHandle);
      if (transformHandle) il2cpp_gchandle_free(transformHandle);
      if (gameObjectHandle) il2cpp_gchandle_free(gameObjectHandle);
      if (s_ghostObjectDestroy) {
        void *destroyParams[] = {gameObject};
        Invoke(s_ghostObjectDestroy, nullptr, destroyParams);
      }
      GhostRig_DestroyMainThread(GhostRigCleanupReason::CreateFailed);
      return false;
    }
    s_ghostRig.nodes[i].gameObjectHandle = gameObjectHandle;
    s_ghostRig.nodes[i].transformHandle = transformHandle;

    int parentIndex = GhostRig_NodeParent(i);
    if (parentIndex >= 0) {
      void *parent = GhostRig_GetTransform(parentIndex);
      if (!parent || !GhostRig_SetParent(transform, parent)) {
        Log("[P0-GHOST-CREATE] node-failed index=%d name='%s' "
            "reason=set-parent parentIndex=%d",
            i, nodeName, parentIndex);
        GhostRig_DestroyMainThread(GhostRigCleanupReason::CreateFailed);
        return false;
      }
    }
    if (i == 0) {
      SafeSetLocalPosition(transform, {0, 0, 0});
      SafeSetLocalRotation(transform, {0, 0, 0, 1});
    } else {
      SafeSetLocalPosition(
          transform, GhostRig_ToVec3(s_ghostRig.nodes[i].bind.localPosition));
      SafeSetLocalRotation(
          transform, GhostRig_ToQuat(s_ghostRig.nodes[i].bind.localRotation));
    }

    int componentCount = GhostRig_GetComponentCount(gameObject);
    int hideFlags = GhostRig_GetHideFlags(gameObject);
    void *actualParent = Invoke(g_transform_get_parent, transform);
    void *expectedParent = parentIndex >= 0
                               ? GhostRig_GetTransform(parentIndex)
                               : nullptr;
    if (!GhostRig_SameUnityObject(actualParent, expectedParent)) {
      Log("[P0-GHOST-CREATE] node-failed index=%d name='%s' "
          "reason=parent-mismatch expected=%p actual=%p",
          i, nodeName, expectedParent, actualParent);
      GhostRig_DestroyMainThread(GhostRigCleanupReason::CreateFailed);
      return false;
    }
    if (hideFlags != GHOST_HIDE_FLAGS) {
      Log("[P0-GHOST-CREATE] node-failed index=%d name='%s' "
          "reason=hide-flags expected=0x%X actual=0x%X",
          i, nodeName, GHOST_HIDE_FLAGS, hideFlags);
      GhostRig_DestroyMainThread(GhostRigCleanupReason::CreateFailed);
      return false;
    }
    if (componentCount != 1) {
      Log("[P0-GHOST-CREATE] node-failed index=%d name='%s' "
          "reason=unexpected-components count=%d",
          i, nodeName, componentCount);
      GhostRig_DestroyMainThread(GhostRigCleanupReason::CreateFailed);
      return false;
    }
    Log("[P2-GHOST-CREATE-NODE] index=%d name='%s' go=%p transform=%p "
        "parentIndex=%d components=%d hideFlags=0x%X tid=%lu "
        "generation=%llu owner=%p",
        i, nodeName, gameObject, transform, parentIndex,
        componentCount, hideFlags, GetCurrentThreadId(),
        (unsigned long long)generation,
        reinterpret_cast<void *>(ownerCharacter));
  }

  if (!GhostRig_AlignRoot(ownerRoot)) {
    Log("[P0-GHOST-CREATE] failed reason=root-align");
    GhostRig_DestroyMainThread(GhostRigCleanupReason::CreateFailed);
    return false;
  }

  s_ghostRig.state = GhostRigState::Alive;
  GhostRig_ResolvePhase3Targets(ownerRoot);
  if (!GhostRig_ApplyDirectPose(ownerRoot)) {
    Log("[P2-GHOST-CREATE] failed reason=initial-direct-write");
    GhostRig_DestroyMainThread(GhostRigCleanupReason::CreateFailed);
    return false;
  }

  s_ghostAlivePublic.store(true, std::memory_order_release);
  s_ghostPublicGeneration.store(generation, std::memory_order_release);
  s_ghostOrderBudget = 720;
  int frame = GhostRig_GetFrameCount();
  s_ghostRig.lastAppliedFrame = frame;
  s_ghostRig.lastPoseLogFrame = frame;
  s_ghostRig.applyCount = 1;
  Log("[P4-GHOST-CREATE] complete frame=%d tid=%lu generation=%llu "
      "owner=%p scene=%d nodes=%d targetWritesDeferredToPreFinalIK=1 "
      "ordinaryFkPositionWrites=0 hipsBindPositionWritesDeferred=1 "
      "rootWritesDeferredUntilPlayingSample=1 "
      "dontDestroyOnLoad=0 animatorCreated=0 animationClipCreated=0",
      frame, GetCurrentThreadId(), (unsigned long long)generation,
      reinterpret_cast<void *>(ownerCharacter), sceneHandle,
      GHOST_NODE_COUNT);
  GhostRig_LogNodeSnapshot(frame, "created");
  return true;
}

static void GhostRig_ProcessLifecycleMainThread() {
  uint64_t requestedGeneration =
      s_ghostRequestedGeneration.load(std::memory_order_acquire);
  GhostRigCleanupReason requestedReason = static_cast<GhostRigCleanupReason>(
      s_ghostRequestedCleanupReason.load(std::memory_order_acquire));
  bool desired = s_ghostDesiredEnabled.load(std::memory_order_acquire);

  int sceneHandle = GhostRig_GetActiveSceneHandle();
  if (sceneHandle != INT_MIN) {
    if (s_ghostObservedSceneHandle == INT_MIN) {
      s_ghostObservedSceneHandle = sceneHandle;
      Log("[P0-SCENE] initial scene=%d tid=%lu", sceneHandle,
          GetCurrentThreadId());
    } else if (sceneHandle != s_ghostObservedSceneHandle) {
      int oldScene = s_ghostObservedSceneHandle;
      s_ghostObservedSceneHandle = sceneHandle;
      const uint64_t backendGeneration =
          g_motionBackend.InvalidateOwner();
      s_ghostRequestedCleanupReason.store(
          static_cast<uint32_t>(GhostRigCleanupReason::SceneChanged),
          std::memory_order_release);
      requestedGeneration =
          s_ghostRequestedGeneration.fetch_add(1,
                                                std::memory_order_acq_rel) + 1;
      requestedReason = GhostRigCleanupReason::SceneChanged;
      DirectVmdRuntime_SetTarget(
          requestedGeneration,
          s_ghostRequestedOwnerId.load(std::memory_order_acquire));
      Log("[P3-SCENE-INVALIDATE] changed old=%d new=%d tid=%lu "
          "backendGeneration=%llu ghostGeneration=%llu "
          "oldTargetInvalidatedBeforeCleanup=1",
          oldScene, sceneHandle, GetCurrentThreadId(),
          (unsigned long long)backendGeneration,
          (unsigned long long)requestedGeneration);
    }
  }

  if (s_ghostRig.state != GhostRigState::Empty &&
      (s_ghostRig.generation != requestedGeneration || !desired)) {
    GhostRigCleanupReason destroyReason = requestedReason;
    if (destroyReason == GhostRigCleanupReason::None)
      destroyReason = GhostRigCleanupReason::GenerationMismatch;
    GhostRig_DestroyMainThread(destroyReason);
  }

  if (!desired && s_ghostRig.state != GhostRigState::Empty)
    GhostRig_DestroyMainThread(requestedReason);

  const uintptr_t requestedOwner =
      s_ghostRequestedOwnerId.load(std::memory_order_acquire);
  if (s_ghostRig.state == GhostRigState::Empty &&
      s_ghostRig.bindState != GhostBindCaptureState::Empty &&
      (!desired || s_ghostRig.bindGeneration != requestedGeneration ||
       s_ghostRig.bindOwnerCharacter != requestedOwner)) {
    Log("[P2-BIND-CLEAR] reason=%s oldGeneration=%llu newGeneration=%llu "
        "oldOwner=%p newOwner=%p tid=%lu",
        GhostRig_CleanupReasonName(requestedReason),
        (unsigned long long)s_ghostRig.bindGeneration,
        (unsigned long long)requestedGeneration,
        reinterpret_cast<void *>(s_ghostRig.bindOwnerCharacter),
        reinterpret_cast<void *>(requestedOwner), GetCurrentThreadId());
    GhostRig_ClearBindCapture();
  }

  if (s_ghostRig.state == GhostRigState::Empty) {
    s_ghostRig.generation = requestedGeneration;
    s_ghostRig.sceneHandle = sceneHandle;
    s_ghostPublicGeneration.store(requestedGeneration,
                                  std::memory_order_release);
  }
}

static void GhostRig_OnSolverManagerLateUpdate(void *solverManager) {
  (void)solverManager;
  if (!GhostRig_RequireMainThread("SolverManager.LateUpdate", true))
    return;

  bool hasWork = GhostRig_IsRequestedEnabled() || GhostRig_IsAliveForGui() ||
      s_ghostRig.generation !=
          s_ghostRequestedGeneration.load(std::memory_order_acquire);
  if (!hasWork)
    return;

  int frame = GhostRig_GetFrameCount();
  if (frame >= 0 && frame == s_ghostLastMaintenanceFrame)
    return;
  s_ghostLastMaintenanceFrame = frame;
  GhostRig_ProcessLifecycleMainThread();

  uintptr_t owner = s_ghostOwnerKnown.load(std::memory_order_acquire)
                        ? s_ghostRequestedOwnerId.load(std::memory_order_acquire)
                        : reinterpret_cast<uintptr_t>(g_mainCharEntity);
  GhostRig_LogOrder("SolverManager.LateUpdate.enter", frame, owner,
                    s_ghostRequestedGeneration.load(
                        std::memory_order_acquire));
}

static void GhostRig_BeforeFinalIK(void *bipedIK) {
  if (!GhostRig_RequireMainThread("BipedIK.UpdateSolver.pre", true))
    return;
  if (!GhostRig_IsRequestedEnabled())
    return;

  GhostRig_ProcessLifecycleMainThread();
  if (!GhostRig_IsRequestedEnabled() ||
      !g_motionBackend.Is(MotionBackend::DirectVmd))
    return;

  if (g_playerController)
    SafeRefreshEntity();

  uintptr_t owner = s_ghostOwnerKnown.load(std::memory_order_acquire)
                        ? s_ghostRequestedOwnerId.load(std::memory_order_acquire)
                        : reinterpret_cast<uintptr_t>(g_mainCharEntity);
  if (!s_ghostOwnerKnown.load(std::memory_order_acquire) && owner) {
    s_ghostRequestedOwnerId.store(owner, std::memory_order_release);
  }
  if (!owner || reinterpret_cast<uintptr_t>(g_mainCharEntity) != owner)
    return;

  ClothBegin(MotionBackend::DirectVmd);

  SafeSetAnimatorEnabled(false);

  void *ownerRoot = GhostRig_GetOwnerRoot(owner);
  if (!ownerRoot || !GhostRig_IsComponentUnderOwner(bipedIK, ownerRoot))
    return;

  uint64_t generation =
      s_ghostRequestedGeneration.load(std::memory_order_acquire);
  DirectVmdRuntime_SetTarget(generation, owner);
  int sceneHandle = GhostRig_GetActiveSceneHandle();
  int frame = GhostRig_GetFrameCount();
  GhostRig_LogOrder("UnityAnimator.disabled.DirectVmdOwner", frame, owner,
                    generation);
  bool wrotePose = false;
  if (s_ghostRig.state == GhostRigState::Empty) {
    const bool bindMatches =
        s_ghostRig.bindState == GhostBindCaptureState::Ready &&
        s_ghostRig.bindGeneration == generation &&
        s_ghostRig.bindSourceRevision == s_directVmdSourceRevision.load(std::memory_order_acquire) &&
        s_ghostRig.bindOwnerCharacter == owner;
    if (!bindMatches) {
      const bool retryDue =
          s_ghostRig.bindState == GhostBindCaptureState::Empty ||
          frame < 0 || s_ghostRig.bindAttemptFrame == INT_MIN ||
          frame - s_ghostRig.bindAttemptFrame >= 120;
      if (!retryDue)
        return;
      if (!GhostRig_CaptureNaturalBindPose(generation, owner, ownerRoot,
                                           frame))
        return;
    }
    if (!GhostRig_CreateMainThread(generation, owner, sceneHandle, ownerRoot))
      return;
    wrotePose = true;
  }
  if (s_ghostRig.state != GhostRigState::Alive ||
      s_ghostRig.generation != generation ||
      s_ghostRig.ownerCharacter != owner)
    return;

  if(ClothBlockFirstBodyPose("DirectVmd.held-native-before-first-body",GhostRig_ClothMayOwnAnchor))
    return;

  if (frame < 0 || frame != s_ghostRig.lastAppliedFrame) {
    if (!GhostRig_ApplyDirectPose(ownerRoot)) {
      Log("[P2-GHOST-WRITE] failed frame=%d tid=%lu generation=%llu "
          "owner=%p",
          frame, GetCurrentThreadId(),
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(owner));
      return;
    }
    s_ghostRig.lastAppliedFrame = frame;
    ++s_ghostRig.applyCount;
    wrotePose = true;
  }

  GhostRig_LogOrder("GhostRig.Phase4Update.preFinalIK", frame, owner,
                    s_ghostRig.generation);
  if (wrotePose) {
    GhostRig_LogOrder("GhostRig.DirectVmdPoseWrite.complete", frame, owner,
                      s_ghostRig.generation);
  }

  if (frame < 0 || frame != s_ghostRig.lastRootAppliedFrame) {
    GhostRig_LogOrder("GhostRig.Phase4TargetRootWrite.begin", frame, owner,
                      s_ghostRig.generation);
    if (GhostRig_ApplyPhase4TargetRoot(frame, ownerRoot)) {
      s_ghostRig.lastRootAppliedFrame = frame;
      GhostRig_LogOrder("GhostRig.Phase4TargetRootWrite.complete", frame,
                        owner, s_ghostRig.generation);
    } else {
      Log("[P4-ROOT-WRITE] failed frame=%d generation=%llu owner=%p "
          "backend=%s tid=%lu",
          frame, (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(owner),
          MotionBackendName(g_motionBackend.Current()),
          GetCurrentThreadId());
    }
  }

  if (frame < 0 || frame != s_ghostRig.lastTargetAppliedFrame) {
    GhostRig_LogOrder("GhostRig.Phase3TargetFkWrite.begin", frame, owner,
                      s_ghostRig.generation);
    if (GhostRig_ApplyPhase3TargetFk(frame)) {
      s_ghostRig.lastTargetAppliedFrame = frame;
      GhostRig_LogOrder("GhostRig.Phase3TargetFkWrite.complete", frame,
                        owner, s_ghostRig.generation);
    } else {
      Log("[P3-FK-WRITE] failed frame=%d generation=%llu owner=%p "
          "backend=%s tid=%lu",
          frame, (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(owner),
          MotionBackendName(g_motionBackend.Current()),
          GetCurrentThreadId());
    }
  }

  if (frame < 0 || frame != s_ghostRig.lastFingerAppliedFrame) {
    GhostRig_LogOrder("GhostRig.Phase6FingerFkWrite.begin", frame, owner,
                      s_ghostRig.generation);
    if (GhostRig_ApplyPhase6FingerFk(frame)) {
      s_ghostRig.lastFingerAppliedFrame = frame;
      GhostRig_LogOrder("GhostRig.Phase6FingerFkWrite.complete", frame,
                        owner, s_ghostRig.generation);
    } else {
      Log("[P6-FINGER-WRITE] failed frame=%d generation=%llu owner=%p "
          "backend=%s tid=%lu",
          frame, (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(owner),
          MotionBackendName(g_motionBackend.Current()),
          GetCurrentThreadId());
    }
  }

  if (frame < 0 || frame != s_ghostRig.lastEyeAppliedFrame) {
    GhostRig_LogOrder("GhostRig.Phase6EyeWrite.begin", frame, owner,
                      s_ghostRig.generation);
    if (GhostRig_ApplyPhase6Eyes(frame, ownerRoot)) {
      s_ghostRig.lastEyeAppliedFrame = frame;
      GhostRig_LogOrder("GhostRig.Phase6EyeWrite.complete", frame, owner,
                        s_ghostRig.generation);
    } else {
      Log("[P6-EYE-WRITE] failed frame=%d generation=%llu owner=%p "
          "backend=%s tid=%lu",
          frame, (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(owner),
          MotionBackendName(g_motionBackend.Current()),
          GetCurrentThreadId());
    }
  }

  if (frame < 0 || frame != s_ghostRig.lastTwistAppliedFrame) {
    GhostRig_LogOrder("GhostRig.Phase6TwistWrite.begin", frame, owner,
                      s_ghostRig.generation);
    if (GhostRig_ApplyPhase6Twist(frame)) {
      s_ghostRig.lastTwistAppliedFrame = frame;
      GhostRig_LogOrder("GhostRig.Phase6TwistWrite.complete", frame,
                        owner, s_ghostRig.generation);
    } else {
      Log("[P6-TWIST-WRITE] failed frame=%d generation=%llu owner=%p "
          "backend=%s tid=%lu",
          frame, (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(owner),
          MotionBackendName(g_motionBackend.Current()),
          GetCurrentThreadId());
    }
  }

  if (frame < 0 || frame != s_ghostRig.lastLegPreparedFrame) {
    GhostRig_LogOrder("GhostRig.Phase5LegTargets.begin", frame, owner,
                      s_ghostRig.generation);
    if (!GhostRig_PreparePhase5Legs(frame, bipedIK)) {
      Log("[P5-LEG-PREPARE] failed frame=%d vmdFrame=%.6f "
          "generation=%llu owner=%p biped=%p action=suppress-finalik "
          "tid=%lu",
          frame, s_ghostRig.lastSourceFrame,
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(owner), bipedIK,
          GetCurrentThreadId());
    }
  }

  bool periodicLog = false;
  if (frame >= 0) {
    periodicLog = s_ghostRig.lastPoseLogFrame == INT_MIN ||
                  frame - s_ghostRig.lastPoseLogFrame >= 120;
  } else {
    periodicLog = (s_ghostRig.applyCount % 120) == 0;
  }
  if (periodicLog) {
    s_ghostRig.lastPoseLogFrame = frame;
    GhostRig_LogNodeSnapshot(frame, "periodic");
  }
  const uint64_t snapshotRequest =
      s_ghostSnapshotRequest.load(std::memory_order_acquire);
  if (snapshotRequest != s_ghostHandledSnapshotRequest) {
    s_ghostHandledSnapshotRequest = snapshotRequest;
    GhostRig_LogNodeSnapshot(frame, "manual");
  }
}

static bool GhostRig_ShouldSuppressFinalIKPhase5Safety(void *bipedIK) {
  if (!bipedIK || !GhostRig_IsRequestedEnabled() ||
      !g_motionBackend.Is(MotionBackend::DirectVmd) ||
      !GhostRig_RequireMainThread("BipedIK.UpdateSolver.safety", false))
    return false;

  const uint64_t requestedGeneration =
      s_ghostRequestedGeneration.load(std::memory_order_acquire);
  const uintptr_t requestedOwner =
      s_ghostRequestedOwnerId.load(std::memory_order_acquire);
  if (!requestedOwner ||
      reinterpret_cast<uintptr_t>(g_mainCharEntity) != requestedOwner)
    return false;

  void *ownerRoot = GhostRig_GetOwnerRoot(requestedOwner);
  if (!ownerRoot || !GhostRig_IsComponentUnderOwner(bipedIK, ownerRoot))
    return false;

  if (s_ghostRig.state != GhostRigState::Alive ||
      s_ghostRig.generation != requestedGeneration ||
      s_ghostRig.ownerCharacter != requestedOwner ||
      g_clothPlaybackGate.BlocksPose(uint32_t(MotionBackend::DirectVmd),g_motionBackend.Generation()))
    return true;

  const int frame = GhostRig_GetFrameCount();
  const bool prepared =
      GhostRig_GetFinalIkBiped() == bipedIK &&
      s_ghostRig.lastLegPreparedFrame == frame &&
      GhostRig_LegRuntime(DirectVmdLegSide::Left).preparedFrame == frame &&
      GhostRig_LegRuntime(DirectVmdLegSide::Right).preparedFrame == frame;
  return !prepared;
}

static void GhostRig_LogFinalIKSuppressedPhase5Safety(void *bipedIK) {
  (void)bipedIK;
  const int frame = GhostRig_GetFrameCount();
  const uintptr_t owner =
      s_ghostRequestedOwnerId.load(std::memory_order_acquire);
  const uint64_t generation =
      s_ghostRequestedGeneration.load(std::memory_order_acquire);
  ++s_ghostRig.finalIkSuppressedCount;
  ++s_ghostRig.finalIkSafetySuppressCount;
  GhostRig_LogOrder("FinalIK.BipedIK.UpdateSolver.suppressed.phase5-safety", frame,
                    owner, generation);

  const bool periodic = frame >= 0
      ? (s_ghostRig.lastFinalIkSuppressedLogFrame == INT_MIN ||
         frame - s_ghostRig.lastFinalIkSuppressedLogFrame >= 120)
      : (s_ghostRig.finalIkSuppressedCount == 1 ||
         (s_ghostRig.finalIkSuppressedCount % 120) == 1);
  if (!periodic)
    return;

  s_ghostRig.lastFinalIkSuppressedLogFrame = frame;
  Log("[P5-FINALIK-SAFETY-SUPPRESS] frame=%d sourceFrame=%.6f "
      "playback=%u originalCalled=0 legTargetsPrepared=%d "
      "suppressedCalls=%llu generation=%llu owner=%p component=%p "
      "tid=%lu policy=stage5-fail-closed-stale-target-protection",
      frame, s_ghostRig.lastSourceFrame,
      static_cast<unsigned>(s_ghostRig.lastSamplePlayback),
      s_ghostRig.lastLegPreparedFrame == frame ? 1 : 0,
      (unsigned long long)s_ghostRig.finalIkSuppressedCount,
      (unsigned long long)generation, reinterpret_cast<void *>(owner),
      bipedIK, GetCurrentThreadId());
}

static void GhostRig_LogFinalIKEntry(void *bipedIK) {
  if (!GhostRig_IsRequestedEnabled() ||
      !g_motionBackend.Is(MotionBackend::DirectVmd) ||
      !GhostRig_RequireMainThread("BipedIK.UpdateSolver.log", false))
    return;
  uintptr_t owner = s_ghostOwnerKnown.load(std::memory_order_acquire)
                        ? s_ghostRequestedOwnerId.load(std::memory_order_acquire)
                        : reinterpret_cast<uintptr_t>(g_mainCharEntity);
  void *ownerRoot = GhostRig_GetOwnerRoot(owner);
  if (!ownerRoot || !GhostRig_IsComponentUnderOwner(bipedIK, ownerRoot))
    return;
  GhostRig_LogOrder("FinalIK.BipedIK.UpdateSolver.enter",
                    GhostRig_GetFrameCount(), owner,
                     s_ghostRequestedGeneration.load(
                         std::memory_order_acquire));
}

static void *GhostRig_GetFinalIkChainTransform(void *solver,
                                                int pointOffset) {
  if (!solver)
    return nullptr;
  __try {
    void *point = *reinterpret_cast<void **>((char *)solver +
                                              pointOffset);
    return point ? *reinterpret_cast<void **>(
                       (char *)point + OFF_IKPOINT_TRANSFORM)
                 : nullptr;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return nullptr;
  }
}

static bool GhostRig_ReadWorldPositionVmd(void *transform,
                                          VmdVec3 *position) {
  if (!position)
    return false;
  Vec3 value = {};
  if (!GhostRig_ReadWorldPosition(transform, value))
    return false;
  *position = {value.x, value.y, value.z};
  return DirectVmdFinite(value.x) && DirectVmdFinite(value.y) &&
         DirectVmdFinite(value.z);
}

static void GhostRig_LogPhase5PostSolverChains(int frame,
                                                void *ownerRoot,
                                                bool transitionLog) {
  VmdVec3 rootPosition = {};
  const bool rootOk =
      GhostRig_ReadWorldPositionVmd(ownerRoot, &rootPosition);
  const float rootError =
      rootOk && s_ghostRig.lastDesiredRootValid
          ? DirectVmdLength(DirectVmdSub(
                rootPosition, s_ghostRig.lastDesiredRootPosition))
          : -1.0f;
  VmdVec3 hipsPosition = {};
  VmdVec3 expectedHipsPosition = {};
  void *hipsTransform =
      GhostRig_GetTargetTransform(DirectVmdBoneId::LowerBody);
  const bool hipsOk =
      GhostRig_ReadWorldPositionVmd(hipsTransform, &hipsPosition) &&
      GhostRig_EvaluateExpectedLowerBodyWorldPosition(
          &expectedHipsPosition);
  const float hipsError =
      hipsOk ? DirectVmdLength(DirectVmdSub(
                   hipsPosition, expectedHipsPosition))
             : -1.0f;
  const int pointOffsets[3] = {
      OFF_IKTRIG_BONE1, OFF_IKTRIG_BONE2, OFF_IKTRIG_BONE3};

  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT; ++index) {
    const DirectVmdLegSide side =
        static_cast<DirectVmdLegSide>(index);
    GhostRigLegRuntime &leg = s_ghostRig.legs[index];
    void *solver = GhostRig_GetLegSolver(side);
    void *solverTransforms[3] = {};
    VmdVec3 solverPositions[3] = {};
    bool chainOk = solver != nullptr;
    bool chainUnderOwner = solver != nullptr;
    for (uint32_t point = 0; point < 3; ++point) {
      solverTransforms[point] =
          GhostRig_GetFinalIkChainTransform(solver,
                                             pointOffsets[point]);
      chainUnderOwner =
          chainUnderOwner && solverTransforms[point] &&
          GhostRig_IsComponentUnderOwner(solverTransforms[point],
                                          ownerRoot);
      chainOk = chainOk &&
                GhostRig_ReadWorldPositionVmd(
                    solverTransforms[point],
                    &solverPositions[point]);
    }

    const DirectVmdBoneId thighId =
        kDirectVmdPhase5LegFkBones[index][0];
    const DirectVmdBoneId kneeId =
        kDirectVmdPhase5LegFkBones[index][1];
    const DirectVmdBoneId ankleId =
        kDirectVmdPhase5LegFkBones[index][2];
    void *mappedTransforms[3] = {
        GhostRig_GetTargetTransform(thighId),
        GhostRig_GetTargetTransform(kneeId),
        GhostRig_GetTargetTransform(ankleId),
    };
    const bool mappingMatches =
        solverTransforms[0] == mappedTransforms[0] &&
        solverTransforms[1] == mappedTransforms[1] &&
        solverTransforms[2] == mappedTransforms[2];
    const float upperLength =
        chainOk ? DirectVmdLength(DirectVmdSub(
                      solverPositions[1], solverPositions[0]))
                : -1.0f;
    const float lowerLength =
        chainOk ? DirectVmdLength(DirectVmdSub(
                      solverPositions[2], solverPositions[1]))
                : -1.0f;
    const float targetError =
        chainOk && leg.effectiveIkEnabled && leg.reach.valid
            ? DirectVmdLength(DirectVmdSub(
                  solverPositions[2], leg.reach.solverTarget))
            : -1.0f;
    VmdVec3 expectedThighPosition = {};
    VmdVec3 expectedKneePosition = {};
    const float thighSeedError =
        chainOk && GhostRig_EvaluateExpectedLegSeedPositions(
                       side, frame, &expectedThighPosition,
                       &expectedKneePosition)
            ? DirectVmdLength(DirectVmdSub(solverPositions[0],
                                           expectedThighPosition))
            : -1.0f;
    Quat measuredAnkleRotation = {0.0f, 0.0f, 0.0f, 1.0f};
    const bool ankleRotationOk =
        solverTransforms[2] &&
        GhostRig_ReadWorldRotation(solverTransforms[2],
                                   measuredAnkleRotation);
    const DirectVmdBoneId toeId =
        kDirectVmdPhase5LegFkBones[index][3];
    VmdVec3 measuredToePosition = {};
    const bool toePositionOk = GhostRig_ReadWorldPositionVmd(
        GhostRig_GetTargetTransform(toeId), &measuredToePosition);
    const float toeAimSolverError =
        toePositionOk && leg.toeAimWritten && leg.toeAim.valid
            ? DirectVmdLength(DirectVmdSub(
                  measuredToePosition, leg.toeAim.solverToe))
            : -1.0f;
    const float toeAimDesiredError =
        toePositionOk && leg.toeAim.valid
            ? DirectVmdLength(DirectVmdSub(
                  measuredToePosition, leg.toeAim.desiredToe))
            : -1.0f;
    float ankleRotationErrorDegrees = -1.0f;
    if (ankleRotationOk && leg.toeAimWritten && leg.toeAim.valid) {
      const VmdQuaternion measured = DirectVmdNormalizeQuaternion(
          {measuredAnkleRotation.x, measuredAnkleRotation.y,
           measuredAnkleRotation.z, measuredAnkleRotation.w});
      const VmdQuaternion desired = DirectVmdNormalizeQuaternion(
          leg.toeAim.solverAnkleWorldRotation);
      const float rotationDot = std::fabs(
          measured.x * desired.x + measured.y * desired.y +
          measured.z * desired.z + measured.w * desired.w);
      const float clampedRotationDot =
          (std::max)(0.0f, (std::min)(rotationDot, 1.0f));
      ankleRotationErrorDegrees =
          2.0f * std::acos(clampedRotationDot) *
          57.29577951308232f;
    }

    Log("[P5-FINALIK-CHAIN] phase=post unityFrame=%d vmdFrame=%.6f "
        "side=%s transitionLog=%d ikEnabled=%d chainOk=%d "
        "chainUnderOwner=%d solverMappingMatches=%d "
        "rootP=(%.6f,%.6f,%.6f) desiredRootError=%.6f "
        "hipsP=(%.6f,%.6f,%.6f) expectedHipsP=(%.6f,%.6f,%.6f) "
        "hipsError=%.6f hipsPreCorrectionError=%.6f "
        "thighP=(%.6f,%.6f,%.6f) thighSeedError=%.6f "
        "kneeP=(%.6f,%.6f,%.6f) "
        "ankleP=(%.6f,%.6f,%.6f) solverTarget=(%.6f,%.6f,%.6f) "
        "solverTargetError=%.6f upperLength=%.6f lowerLength=%.6f "
        "ankleRotationOk=%d ankleRotationTarget=(%.7f,%.7f,%.7f,%.7f) "
        "ankleRotationActual=(%.7f,%.7f,%.7f,%.7f) "
        "ankleRotationErrorDeg=%.6f "
        "toeAimValid=%d toeAimWritten=%d "
        "toeAimCurrent=(%.6f,%.6f,%.6f) "
        "toeAimDesired=(%.6f,%.6f,%.6f) "
        "toeAimSolver=(%.6f,%.6f,%.6f) "
        "toeAimActual=(%.6f,%.6f,%.6f) "
        "toeAimCurrentDistance=%.6f toeAimDesiredDistance=%.6f "
        "toeAimReachableDistance=%.6f toeAimResidual=%.6f "
        "toeAimSolverError=%.6f toeAimDesiredError=%.6f "
        "expectedTotalLength=%.6f targetDetached=%d "
        "preUpdateDetached=%d postUpdateDetached=%d "
        "externalOwnershipIsolated=%d diagnosticOnly=1 "
        "previousFrameFeedbackInput=0 sameFrameFinalIkOutputInput=1 "
        "ankleRotationOwner=post-solve-toe-aim "
        "generation=%llu owner=%p tid=%lu",
        frame, s_ghostRig.lastSourceFrame,
        GhostRig_LegSideName(side), transitionLog ? 1 : 0,
        leg.effectiveIkEnabled ? 1 : 0, chainOk ? 1 : 0,
        chainUnderOwner ? 1 : 0, mappingMatches ? 1 : 0,
        rootPosition.x, rootPosition.y, rootPosition.z, rootError,
        hipsPosition.x, hipsPosition.y, hipsPosition.z,
        expectedHipsPosition.x, expectedHipsPosition.y,
        expectedHipsPosition.z, hipsError,
        s_ghostRig.lastHipsCorrectionDistance,
        solverPositions[0].x, solverPositions[0].y,
        solverPositions[0].z, thighSeedError, solverPositions[1].x,
        solverPositions[1].y, solverPositions[1].z,
        solverPositions[2].x, solverPositions[2].y,
        solverPositions[2].z, leg.reach.solverTarget.x,
        leg.reach.solverTarget.y, leg.reach.solverTarget.z,
        targetError, upperLength, lowerLength,
        ankleRotationOk ? 1 : 0,
        leg.toeAim.solverAnkleWorldRotation.x,
        leg.toeAim.solverAnkleWorldRotation.y,
        leg.toeAim.solverAnkleWorldRotation.z,
        leg.toeAim.solverAnkleWorldRotation.w,
        measuredAnkleRotation.x, measuredAnkleRotation.y,
        measuredAnkleRotation.z, measuredAnkleRotation.w,
        ankleRotationErrorDegrees,
        leg.toeAim.valid ? 1 : 0, leg.toeAimWritten ? 1 : 0,
        leg.toeAim.currentToe.x, leg.toeAim.currentToe.y,
        leg.toeAim.currentToe.z, leg.toeAim.desiredToe.x,
        leg.toeAim.desiredToe.y, leg.toeAim.desiredToe.z,
        leg.toeAim.solverToe.x, leg.toeAim.solverToe.y,
        leg.toeAim.solverToe.z, measuredToePosition.x,
        measuredToePosition.y, measuredToePosition.z,
        leg.toeAim.currentDistance, leg.toeAim.desiredDistance,
        leg.toeAim.reachableDistance, leg.toeAim.residual,
        toeAimSolverError, toeAimDesiredError,
        GhostRig_LegMaximumReach(side),
        leg.savedTargetPresent ? 1 : 0,
        leg.savedOnPreUpdatePresent ? 1 : 0,
        leg.savedOnPostUpdatePresent ? 1 : 0,
        leg.externalOwnershipIsolated ? 1 : 0,
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
    leg.postSolveDiagnosticPending = false;
  }
}

static void GhostRig_AfterFinalIK(void *bipedIK) {
  if (!GhostRig_IsRequestedEnabled() ||
      !g_motionBackend.Is(MotionBackend::DirectVmd) ||
      !GhostRig_RequireMainThread("BipedIK.UpdateSolver.post", false) ||
      s_ghostRig.state != GhostRigState::Alive ||
      g_clothPlaybackGate.BlocksPose(uint32_t(MotionBackend::DirectVmd),g_motionBackend.Generation()))
    return;

  const uintptr_t owner = s_ghostRig.ownerCharacter;
  const uint64_t generation = s_ghostRig.generation;
  if (!owner ||
      s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
          generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) != owner)
    return;
  void *ownerRoot = GhostRig_GetOwnerRoot(owner);
  if (!ownerRoot || !GhostRig_IsComponentUnderOwner(bipedIK, ownerRoot))
    return;

  const int frame = GhostRig_GetFrameCount();
  GhostRig_LogOrder("FinalIK.BipedIK.UpdateSolver.exit", frame, owner,
                    generation);
  ++s_ghostRig.finalIkOriginalRunCount;
  const bool grounderActive = s_ghostRig.grounder.active &&
      s_ghostRig.terrainEnabledLast &&
      s_directVmdTerrainDesiredEnabled.load(
          std::memory_order_acquire);
  if (grounderActive) {
    GhostRig_StabilizePelvisNodeBindPosition(frame, "post-finalik");
    GhostRig_StabilizeTargetHipsBindPosition(frame,
                                             "post-finalik");
    GhostRig_ResolvePhase7LegsAfterHipsCorrection(frame);
    GhostRig_ApplyPhase5ToeAimAfterFinalIk(frame);
    for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT;
         ++index) {
      GhostRigLegRuntime &leg = s_ghostRig.legs[index];
      if (leg.effectiveIkEnabled || leg.preparedFrame != frame)
        continue;
      const DirectVmdLegSide side =
          static_cast<DirectVmdLegSide>(index);
      for (uint32_t order = 0;
           order < DIRECT_VMD_LEG_FK_BONE_COUNT; ++order) {
        GhostRig_WritePhase5LegFkBone(
            side, kDirectVmdPhase5LegFkBones[index][order], frame,
            "grounder-post-ikoff-final-fk");
      }
    }
    GhostRig_LogOrder("Grounder.hybridFinalPose.restored", frame, owner,
                      generation);
  } else {
    GhostRig_StabilizePelvisNodeBindPosition(frame, "post-finalik");
    GhostRig_StabilizeTargetHipsBindPosition(frame,
                                             "post-finalik");
    GhostRig_ResolvePhase7LegsAfterHipsCorrection(frame);
    GhostRig_ApplyPhase5ToeAimAfterFinalIk(frame);
  }
  if (frame != s_ghostRig.lastTargetAppliedFrame)
    return;
  ClothTick("DirectVmd.after-owner-FinalIK", true, GhostRig_ClothMayOwnAnchor,
            s_ghostRig.lastSourceFrame);
  const bool periodic = frame >= 0
      ? (s_ghostRig.lastPostFinalIkLogFrame == INT_MIN ||
         frame - s_ghostRig.lastPostFinalIkLogFrame >= 120)
      : (s_ghostRig.targetApplyCount <= 1 ||
         (s_ghostRig.targetApplyCount % 120) == 1);
  const bool transitionLog =
      s_ghostRig.legs[0].postSolveDiagnosticPending ||
      s_ghostRig.legs[1].postSolveDiagnosticPending;
  if (!periodic && !transitionLog)
    return;

  float positionWeights[DIRECT_VMD_LEG_SIDE_COUNT] = {-1.0f, -1.0f};
  float rotationWeights[DIRECT_VMD_LEG_SIDE_COUNT] = {-1.0f, -1.0f};
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT; ++index) {
    void *solver = GhostRig_GetLegSolver(
        static_cast<DirectVmdLegSide>(index));
    if (!solver)
      continue;
    __try {
      positionWeights[index] = *reinterpret_cast<float *>(
          (char *)solver + OFF_IKSOLVER_IKPOS_WEIGHT);
      rotationWeights[index] = *reinterpret_cast<float *>(
          (char *)solver + OFF_IKSOLVER_IKROT_WEIGHT);
    } __except (EXCEPTION_EXECUTE_HANDLER) {
      positionWeights[index] = -1.0f;
      rotationWeights[index] = -1.0f;
    }
  }
  Log("[P5-FINALIK-RESULT] unityFrame=%d vmdFrame=%.6f "
      "originalCalled=1 originalCalls=%llu "
      "leftIk=%d leftInnerWrite=%d leftPositionWeight=%.3f "
      "leftRotationWeight=%.3f leftResidual=%.6f "
      "rightIk=%d rightInnerWrite=%d rightPositionWeight=%.3f "
      "rightRotationWeight=%.3f rightResidual=%.6f "
      "leftToeAimWritten=%d rightToeAimWritten=%d "
      "toeWorldRotationOverwrite=0 previousFrameFeedbackInput=0 "
      "sameFrameFinalIkOutputInput=1 generation=%llu "
      "owner=%p tid=%lu",
      frame, s_ghostRig.lastSourceFrame,
      (unsigned long long)s_ghostRig.finalIkOriginalRunCount,
      s_ghostRig.legs[0].effectiveIkEnabled ? 1 : 0,
      s_ghostRig.legs[0].innerWriteFrame == frame ? 1 : 0,
      positionWeights[0], rotationWeights[0],
      s_ghostRig.legs[0].reach.residual,
      s_ghostRig.legs[1].effectiveIkEnabled ? 1 : 0,
      s_ghostRig.legs[1].innerWriteFrame == frame ? 1 : 0,
      positionWeights[1], rotationWeights[1],
      s_ghostRig.legs[1].reach.residual,
      s_ghostRig.legs[0].toeAimWritten ? 1 : 0,
      s_ghostRig.legs[1].toeAimWritten ? 1 : 0,
      (unsigned long long)generation, reinterpret_cast<void *>(owner),
      GetCurrentThreadId());

  GhostRig_LogPhase5PostSolverChains(frame, ownerRoot,
                                     transitionLog);

  int checked = 0;
  float maxDeltaDegrees = 0.0f;
  float leftArmDeltaDegrees = -1.0f;
  float rightArmDeltaDegrees = -1.0f;
  float leftWristDeltaDegrees = -1.0f;
  float rightWristDeltaDegrees = -1.0f;
  const char *maxSemantic = "none";
  for (uint32_t order = 0; order < DIRECT_VMD_PHASE3_FK_BONE_COUNT;
       ++order) {
    const DirectVmdBoneId id = kDirectVmdPhase3FkBones[order];
    const uint32_t semantic = DirectVmdBoneIndex(id);
    GhostRigTargetBone &target = s_ghostRig.targets[semantic];
    if (!target.resolved || !target.lastDesiredValid ||
        target.lastDesiredFrame != frame)
      continue;
    void *transform = GhostRig_GetTargetTransform(id);
    Quat actual = {0.0f, 0.0f, 0.0f, 1.0f};
    if (!transform || !GhostRig_ReadWorldRotation(transform, actual))
      continue;

    const VmdQuaternion desired = DirectVmdNormalizeQuaternion(
        target.lastDesiredWorldRotation);
    const VmdQuaternion measured = DirectVmdNormalizeQuaternion(
        {actual.x, actual.y, actual.z, actual.w});
    const float dot = std::fabs(
        desired.x * measured.x + desired.y * measured.y +
        desired.z * measured.z + desired.w * measured.w);
    const float clamped = (std::max)(0.0f, (std::min)(dot, 1.0f));
    const float deltaDegrees =
        2.0f * std::acos(clamped) * 57.29577951308232f;
    if (deltaDegrees > maxDeltaDegrees) {
      maxDeltaDegrees = deltaDegrees;
      maxSemantic = kDirectVmdBoneSpecs[semantic].name;
    }
    if (id == DirectVmdBoneId::LeftArm)
      leftArmDeltaDegrees = deltaDegrees;
    else if (id == DirectVmdBoneId::RightArm)
      rightArmDeltaDegrees = deltaDegrees;
    else if (id == DirectVmdBoneId::LeftWrist)
      leftWristDeltaDegrees = deltaDegrees;
    else if (id == DirectVmdBoneId::RightWrist)
      rightWristDeltaDegrees = deltaDegrees;
    ++checked;
    ++s_ghostRig.postFinalIkDiagnosticReadCount;
  }

  s_ghostRig.lastPostFinalIkLogFrame = frame;
  Log("[P3-POST-FINALIK] frame=%d sourceFrame=%.6f checked=%d "
      "maxDeltaDeg=%.6f maxBone='%s' leftArmDeltaDeg=%.6f "
      "rightArmDeltaDeg=%.6f leftWristDeltaDeg=%.6f "
      "rightWristDeltaDeg=%.6f diagnosticPoseReads=%d "
      "feedbackInputReads=0 phase5LegTargetsOrWeightsWritten=1 "
      "generation=%llu owner=%p tid=%lu",
      frame, s_ghostRig.lastSourceFrame, checked, maxDeltaDegrees,
      maxSemantic, leftArmDeltaDegrees, rightArmDeltaDegrees,
      leftWristDeltaDegrees, rightWristDeltaDegrees, checked,
      (unsigned long long)generation, reinterpret_cast<void *>(owner),
      GetCurrentThreadId());
}

static void GhostRig_LogAnimatorStage(void *animatorMono,
                                      const char *stage) {
  if (!GhostRig_IsRequestedEnabled() ||
      !g_motionBackend.Is(MotionBackend::DirectVmd) ||
      !GhostRig_RequireMainThread(stage, false))
    return;
  uintptr_t owner = s_ghostOwnerKnown.load(std::memory_order_acquire)
                        ? s_ghostRequestedOwnerId.load(std::memory_order_acquire)
                        : reinterpret_cast<uintptr_t>(g_mainCharEntity);
  void *ownerRoot = GhostRig_GetOwnerRoot(owner);
  if (!ownerRoot ||
      !GhostRig_IsComponentUnderOwner(animatorMono, ownerRoot))
    return;
  GhostRig_LogOrder(stage, GhostRig_GetFrameCount(), owner,
                    s_ghostRequestedGeneration.load(
                        std::memory_order_acquire));
}

static bool GhostRig_IsActiveGrounderCallback(void *grounder) {
  if (!grounder || !GhostRig_IsRequestedEnabled() ||
      !g_motionBackend.Is(MotionBackend::DirectVmd) ||
      s_ghostRig.state != GhostRigState::Alive ||
      !s_ghostRig.grounder.active ||
      !s_ghostRig.terrainEnabledLast)
    return false;
  if (s_ghostRequestedGeneration.load(std::memory_order_acquire) !=
          s_ghostRig.generation ||
      s_ghostRequestedOwnerId.load(std::memory_order_acquire) !=
          s_ghostRig.ownerCharacter)
    return false;
  return GhostRig_GetGrounder() == grounder;
}

static bool GhostRig_QueryFootTerrainRaycast(
    void *queryReceiver, void *queryMethod, const VmdVec3 &query,
    float maximumDistance, int layerMask,
    DirectVmdTerrainProbeHit *sample, bool *invokeSucceeded) {
  if (invokeSucceeded)
    *invokeSucceeded = false;
  if (!sample)
    return false;
  *sample = DirectVmdTerrainProbeHit();
  sample->query = query;
  if (!GhostRig_RequireMainThread(
          "Grounding.Raycast.DirectVmdFoot", false) ||
      !queryMethod || !il2cpp_runtime_invoke ||
      !DirectVmdFinite(query.x) || !DirectVmdFinite(query.y) ||
      !DirectVmdFinite(query.z) ||
      !DirectVmdFinite(maximumDistance) || maximumDistance <= 0.0f ||
      layerMask == 0)
    return false;

  VmdVec3 origin = query;
  VmdVec3 direction = {0.0f, -1.0f, 0.0f};
  alignas(16) unsigned char hitData[0x40] = {};
  float distance = maximumDistance;
  int mask = layerMask;
  int queryTriggerInteraction = 1;
  void *arguments[6] = {
      &origin, &direction, hitData, &distance, &mask,
      &queryTriggerInteraction};
  void *exception = nullptr;
  void *boxedResult = nullptr;
  __try {
    boxedResult = il2cpp_runtime_invoke(
        queryMethod, queryReceiver, arguments, &exception);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    boxedResult = nullptr;
    exception = reinterpret_cast<void *>(1);
  }
  if (exception || !boxedResult)
    return false;
  if (invokeSucceeded)
    *invokeSucceeded = true;
  if (!UnboxBool(boxedResult))
    return false;

  VmdVec3 point = {};
  VmdVec3 normal = {};
  float hitDistance = 0.0f;
  memcpy(&point, hitData + 0x00, sizeof(point));
  memcpy(&normal, hitData + 0x0C, sizeof(normal));
  memcpy(&hitDistance, hitData + 0x1C, sizeof(hitDistance));
  if (!DirectVmdFinite(point.x) || !DirectVmdFinite(point.y) ||
      !DirectVmdFinite(point.z) || !DirectVmdFinite(normal.x) ||
      !DirectVmdFinite(normal.y) || !DirectVmdFinite(normal.z) ||
      !DirectVmdFinite(hitDistance) || hitDistance < 0.0f ||
      hitDistance > maximumDistance + 0.01f)
    return false;
  VmdVec3 unitNormal = {};
  if (!DirectVmdTryNormalizeVector(normal, &unitNormal))
    return false;
  if (unitNormal.y < 0.0f)
    unitNormal = DirectVmdScale(unitNormal, -1.0f);
  if (unitNormal.y < s_ghostRig.terrainConfig.minimumNormalY)
    return false;

  sample->hit = 1;
  sample->point = point;
  sample->normal = unitNormal;
  sample->floorDistance = hitDistance;
  return true;
}

static void GhostRig_CaptureFootPhysicsPlanes(void *grounding,
                                               int frame) {
  GhostRigGrounderRuntime &state = s_ghostRig.grounder;
  if (!grounding) {
    if (!state.footPhysicsApiFailureLogged) {
      state.footPhysicsApiFailureLogged = true;
      Log("[P7-GROUNDING-RAYCAST] available=0 grounding=%p "
          "action=hold-stable-then-flat generation=%llu owner=%p "
          "tid=%lu",
          grounding, (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    }
    return;
  }

  int layerMask = 0;
  void *raycastDelegate = nullptr;
  __try {
    layerMask = *reinterpret_cast<int *>(
        (char *)grounding + 0x68);
    raycastDelegate = *reinterpret_cast<void **>(
        (char *)grounding + 0x128);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    layerMask = 0;
    raycastDelegate = nullptr;
  }
  if (raycastDelegate && il2cpp_object_get_class) {
    void *delegateClass = il2cpp_object_get_class(raycastDelegate);
    if (delegateClass &&
        delegateClass != state.groundingRaycastDelegateClass) {
      state.groundingRaycastDelegateClass = delegateClass;
      state.groundingRaycastInvokeMethod =
          FindMethod(delegateClass, "Invoke", 6);
      state.footRaycastBackendLogged = false;
      state.footRaycastFirstSampleLogged = false;
    }
  }
  void *queryReceiver = nullptr;
  void *queryMethod = nullptr;
  const char *backend = "none";
  if (raycastDelegate && state.groundingRaycastInvokeMethod) {
    queryReceiver = raycastDelegate;
    queryMethod = state.groundingRaycastInvokeMethod;
    backend = "Grounding.Raycast-delegate";
  } else if (g_physicsRaycastMethod) {
    queryMethod = g_physicsRaycastMethod;
    backend = "Unity.Physics.Raycast-fallback";
  }
  if (layerMask == 0 || !queryMethod) {
    if (!state.footPhysicsApiFailureLogged) {
      state.footPhysicsApiFailureLogged = true;
      Log("[P7-GROUNDING-RAYCAST] available=0 reason=%s "
          "grounding=%p delegate=%p delegateInvoke=%p physicsFallback=%p "
          "layerMask=0x%08X generation=%llu owner=%p tid=%lu",
          layerMask == 0 ? "zero-layer-mask" : "no-query-method",
          grounding, raycastDelegate,
          state.groundingRaycastInvokeMethod, g_physicsRaycastMethod,
          static_cast<unsigned>(layerMask),
          (unsigned long long)s_ghostRig.generation,
          reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
          GetCurrentThreadId());
    }
    return;
  }
  state.footPhysicsApiFailureLogged = false;
  if (!state.footRaycastBackendLogged) {
    state.footRaycastBackendLogged = true;
    Log("[P7-GROUNDING-RAYCAST] available=1 backend=%s grounding=%p "
        "delegate=%p delegateClass=%p invoke=%p layerMask=0x%08X "
        "managedDelegateRetained=0 mainThreadOnly=1 generation=%llu "
        "owner=%p tid=%lu",
        backend, grounding, raycastDelegate,
        state.groundingRaycastDelegateClass, queryMethod,
        static_cast<unsigned>(layerMask),
        (unsigned long long)s_ghostRig.generation,
        reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
        GetCurrentThreadId());
  }

  static constexpr float kOffsetX[DIRECT_VMD_TERRAIN_PROBE_COUNT] = {
      0.0f, 1.0f, -1.0f, 0.0f, 0.0f};
  static constexpr float kOffsetZ[DIRECT_VMD_TERRAIN_PROBE_COUNT] = {
      0.0f, 0.0f, 0.0f, 1.0f, -1.0f};
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT;
       ++index) {
    if (frame >= 0 &&
        state.cachedFootPhysicsPlaneFrame[index] != INT_MIN &&
        frame - state.cachedFootPhysicsPlaneFrame[index] < 2)
      continue;
    state.cachedFootPhysicsPlane[index] = DirectVmdTerrainPlane();
    state.cachedFootDirectionalClearance[index] =
        DirectVmdDirectionalTerrainClearance();
    state.cachedFootPhysicsPlaneFrame[index] = frame;

    const GhostRigLegRuntime &leg = s_ghostRig.legs[index];
    const VmdVec3 flatTarget = leg.reach.valid
        ? leg.reach.desiredTarget
        : VmdVec3{};
    if (!leg.reach.valid || !DirectVmdFinite(flatTarget.x) ||
        !DirectVmdFinite(flatTarget.y) ||
        !DirectVmdFinite(flatTarget.z)) {
      state.previousFootPhysicsQueryValid[index] = false;
      continue;
    }
    const float legLength = index == 0
        ? s_ghostRig.leftLegLength
        : s_ghostRig.rightLegLength;
    const float footprintRadius = DirectVmdTerrainClamp(
        legLength * 0.070f, 0.040f, 0.070f);
    const float castRise = DirectVmdTerrainClamp(
        legLength * 0.90f, 0.55f, 0.85f);
    const float castDrop = DirectVmdTerrainClamp(
        legLength * 1.20f, 0.70f, 1.20f);
    const float expectedPlatformY = flatTarget.y -
        state.authoredLift[index] +
        s_ghostRig.terrainState.rootOffset;
    const float queryY = (std::max)(flatTarget.y, expectedPlatformY) +
        castRise;
    DirectVmdTerrainProbeHit
        samples[DIRECT_VMD_TERRAIN_PROBE_COUNT] = {};
    for (uint32_t probe = 0;
         probe < DIRECT_VMD_TERRAIN_PROBE_COUNT; ++probe) {
      const VmdVec3 query = {
          flatTarget.x + kOffsetX[probe] * footprintRadius,
          queryY,
          flatTarget.z + kOffsetZ[probe] * footprintRadius};
      ++state.footPhysicsQueryCount;
      bool invokeSucceeded = false;
      const bool hit = GhostRig_QueryFootTerrainRaycast(
          queryReceiver, queryMethod, query, castRise + castDrop,
          layerMask, &samples[probe], &invokeSucceeded);
      if (hit)
        ++state.footPhysicsHitCount;
      else if (invokeSucceeded)
        ++state.footRaycastMissCount;
      else
        ++state.footRaycastInvokeFailureCount;
      if (!state.footRaycastFirstSampleLogged) {
        state.footRaycastFirstSampleLogged = true;
        Log("[P7-GROUNDING-RAYCAST-SAMPLE] backend=%s side=%s "
            "unityFrame=%d query=(%.6f,%.6f,%.6f) maxDistance=%.6f "
            "layerMask=0x%08X invokeSucceeded=%d hit=%d "
            "point=(%.6f,%.6f,%.6f) normal=(%.6f,%.6f,%.6f) "
            "floorDistance=%.6f generation=%llu owner=%p tid=%lu",
            backend, index == 0 ? "L" : "R", frame,
            query.x, query.y, query.z, castRise + castDrop,
            static_cast<unsigned>(layerMask),
            invokeSucceeded ? 1 : 0, hit ? 1 : 0,
            samples[probe].point.x, samples[probe].point.y,
            samples[probe].point.z, samples[probe].normal.x,
            samples[probe].normal.y, samples[probe].normal.z,
            samples[probe].floorDistance,
            (unsigned long long)s_ghostRig.generation,
            reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
            GetCurrentThreadId());
      }
    }
    const bool previousValid =
        state.footPhysicsReferenceValid[index];
    const float previousGroundY = previousValid
        ? state.footPhysicsReferenceGroundY[index] +
              s_ghostRig.terrainState.rootOffset
        : 0.0f;
    state.cachedFootPhysicsPlane[index] =
        DirectVmdAggregateTerrainPlane(
            samples, DIRECT_VMD_TERRAIN_PROBE_COUNT,
            s_ghostRig.terrainConfig.planeClusterHeight,
            previousValid, previousGroundY,
            s_ghostRig.terrainConfig.minimumNormalY);
    VmdVec3 horizontalMotion = {};
    if (state.previousFootPhysicsQueryValid[index]) {
      horizontalMotion.x = flatTarget.x -
          state.previousFootPhysicsQuery[index].x;
      horizontalMotion.z = flatTarget.z -
          state.previousFootPhysicsQuery[index].z;
    }
    state.previousFootPhysicsQuery[index] = flatTarget;
    const bool motionValid =
        state.previousFootPhysicsQueryValid[index];
    state.previousFootPhysicsQueryValid[index] = true;
    if (motionValid) {
      state.cachedFootDirectionalClearance[index] =
          DirectVmdSelectDirectionalTerrainClearance(
              samples, DIRECT_VMD_TERRAIN_PROBE_COUNT,
              state.cachedFootPhysicsPlane[index], horizontalMotion,
              footprintRadius * 0.020f,
              s_ghostRig.terrainConfig.planeClusterHeight,
              s_ghostRig.terrainConfig.minimumNormalY);
    }
  }
}

static void GhostRig_CaptureGrounderTargets(void *grounder, int frame) {
  GhostRigGrounderRuntime &state = s_ghostRig.grounder;
  state.cachedSourceFrame = s_ghostRig.lastSourceFrame;
  state.cachedPlaybackCycle = s_ghostRig.lastPlaybackCycle;

  void *grounding = nullptr;
  void *legs = nullptr;
  uintptr_t legCount = 0;
  state.cachedGroundHeightOffsetValid = false;
  __try {
    grounding = grounder
        ? *reinterpret_cast<void **>(
              (char *)grounder + OFF_GROUNDER_SOLVER)
        : nullptr;
    if (grounding && g_finalIkGroundingLayoutOk) {
      const float heightOffset = *reinterpret_cast<float *>(
          (char *)grounding + OFF_GROUNDING_HEIGHT_OFFSET);
      if (DirectVmdFinite(heightOffset) && heightOffset >= -0.10f &&
          heightOffset <= 0.50f) {
        state.cachedGroundHeightOffset = heightOffset;
        state.cachedGroundHeightOffsetValid = true;
      }
      legs = *reinterpret_cast<void **>(
          (char *)grounding + OFF_GROUNDING_LEGS);
      if (legs) {
        legCount = *reinterpret_cast<uintptr_t *>(
            (char *)legs + IL2CPP_ARRAY_LEN);
        if (legCount > 8)
          legCount = 0;
      }
    }
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    grounding = nullptr;
    legs = nullptr;
    legCount = 0;
  }

  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT; ++index) {
    void *solver = GhostRig_GetLegSolver(
        static_cast<DirectVmdLegSide>(index));
    state.cachedTargetValid[index] = false;
    state.cachedLastHitValid[index] = false;
    state.cachedHeelHitValid[index] = false;
    state.cachedCalculatedFootValid[index] = false;
    state.cachedLegIkPositionValid[index] = false;
    state.cachedLastHitNormalValid[index] = false;
    state.cachedRawLegGrounded[index] = false;
    state.cachedLegInStair[index] = false;
    state.cachedRawGroundFrame[index] = frame;

    void *groundingLeg = nullptr;
    if (legs && index < legCount) {
      __try {
        groundingLeg = *reinterpret_cast<void **>(
            (char *)legs + IL2CPP_ARRAY_DATA +
            index * sizeof(void *));
      } __except (EXCEPTION_EXECUTE_HANDLER) {
        groundingLeg = nullptr;
      }
    }
    if (groundingLeg && il2cpp_object_get_class) {
      void *legClass = il2cpp_object_get_class(groundingLeg);
      if (legClass && state.groundingLegClass != legClass) {
        state.groundingLegClass = legClass;
        state.rawFieldStatusLogged = false;
      }
      bool legGrounded = false;
      VmdVec3 lastHitPoint = {};
      VmdVec3 heelHitPoint = {};
      VmdVec3 calculatedFoot = {};
      VmdVec3 legIkPosition = {};
      VmdVec3 lastHitNormal = {};
      float heightFromGround = 0.0f;
      bool legInStair = false;
      bool directFieldsRead = false;
      __try {
        legGrounded = *reinterpret_cast<bool *>(
            (char *)groundingLeg +
            OFF_GROUNDING_LEG_IS_GROUNDED);
        legIkPosition = *reinterpret_cast<VmdVec3 *>(
            (char *)groundingLeg +
            OFF_GROUNDING_LEG_IK_POSITION);
        heightFromGround = *reinterpret_cast<float *>(
            (char *)groundingLeg +
            OFF_GROUNDING_LEG_HEIGHT_FROM_GROUND);
        heelHitPoint = *reinterpret_cast<VmdVec3 *>(
            (char *)groundingLeg +
            OFF_GROUNDING_LEG_HEEL_HIT_POINT);
        calculatedFoot = *reinterpret_cast<VmdVec3 *>(
            (char *)groundingLeg +
            OFF_GROUNDING_LEG_CALCULATED_FOOT);
        lastHitPoint = *reinterpret_cast<VmdVec3 *>(
            (char *)groundingLeg +
            OFF_GROUNDING_LEG_LAST_HIT_POINT);
        lastHitNormal = *reinterpret_cast<VmdVec3 *>(
            (char *)groundingLeg +
            OFF_GROUNDING_LEG_LAST_HIT_NORMAL);
        legInStair = *reinterpret_cast<bool *>(
            (char *)groundingLeg +
            OFF_GROUNDING_LEG_IS_IN_STAIR);
        directFieldsRead = true;
      } __except (EXCEPTION_EXECUTE_HANDLER) {
        legGrounded = false;
        directFieldsRead = false;
      }
      state.cachedRawLegGrounded[index] = legGrounded;
      state.cachedLegInStair[index] = legInStair;
      state.cachedLastHitPoint[index] = lastHitPoint;
      state.cachedHeelHitPoint[index] = heelHitPoint;
      state.cachedCalculatedFoot[index] = calculatedFoot;
      state.cachedLegIkPosition[index] = legIkPosition;
      state.cachedLastHitNormal[index] = lastHitNormal;
      state.cachedHeightFromGround[index] = heightFromGround;
      state.cachedLastHitValid[index] = directFieldsRead &&
          DirectVmdFinite(lastHitPoint.x) &&
          DirectVmdFinite(lastHitPoint.y) &&
          DirectVmdFinite(lastHitPoint.z);
      state.cachedHeelHitValid[index] = directFieldsRead &&
          DirectVmdFinite(heelHitPoint.x) &&
          DirectVmdFinite(heelHitPoint.y) &&
          DirectVmdFinite(heelHitPoint.z);
      state.cachedCalculatedFootValid[index] = directFieldsRead &&
          DirectVmdFinite(calculatedFoot.x) &&
          DirectVmdFinite(calculatedFoot.y) &&
          DirectVmdFinite(calculatedFoot.z);
      state.cachedLegIkPositionValid[index] = directFieldsRead &&
          DirectVmdFinite(legIkPosition.x) &&
          DirectVmdFinite(legIkPosition.y) &&
          DirectVmdFinite(legIkPosition.z);
      state.cachedLastHitNormalValid[index] = directFieldsRead &&
          DirectVmdFinite(lastHitNormal.x) &&
          DirectVmdFinite(lastHitNormal.y) &&
          DirectVmdFinite(lastHitNormal.z);
      if (!state.rawFieldStatusLogged) {
        const char *className = il2cpp_class_get_name
                                    ? il2cpp_class_get_name(legClass)
                                    : nullptr;
        const char *classNamespace = il2cpp_class_get_namespace
                                         ? il2cpp_class_get_namespace(
                                               legClass)
                                         : nullptr;
        Log("[P7-GROUNDER-RAW-FIELDS] class=%s namespace=%s "
            "legCount=%llu heightOffset=%.6f "
            "heightOffsetValid=%d "
            "diagnosticFields=lastCurHitPoint,m_heelHit,"
            "curFeetCalculatePos,Leg.IKPosition,solver "
            "heightOwnership=Grounding.Raycast-delegate-foot-xz "
            "getHitPointPolled=0 "
            "fieldOffsets=last:0x%X,heel:0x%X,calculated:0x%X,"
            "normal:0x%X,stair:0x%X "
            "access=main-thread-read-only-snapshot generation=%llu "
            "owner=%p tid=%lu",
            className ? className : "?",
            classNamespace ? classNamespace : "?",
            (unsigned long long)legCount,
            state.cachedGroundHeightOffset,
            state.cachedGroundHeightOffsetValid ? 1 : 0,
            OFF_GROUNDING_LEG_LAST_HIT_POINT,
            OFF_GROUNDING_LEG_HEEL_HIT_POINT,
            OFF_GROUNDING_LEG_CALCULATED_FOOT,
            OFF_GROUNDING_LEG_LAST_HIT_NORMAL,
            OFF_GROUNDING_LEG_IS_IN_STAIR,
            (unsigned long long)s_ghostRig.generation,
            reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
            GetCurrentThreadId());
        state.rawFieldStatusLogged = true;
      }
    }
    if (!solver)
      continue;
    VmdVec3 target = {};
    VmdQuaternion rotation = {0.0f, 0.0f, 0.0f, 1.0f};
    float positionWeight = 0.0f;
    float rotationWeight = 0.0f;
    __try {
      target = {
          *reinterpret_cast<float *>(
              (char *)solver + OFF_IKSOLVER_IKPOS_X),
          *reinterpret_cast<float *>(
              (char *)solver + OFF_IKSOLVER_IKPOS_Y),
          *reinterpret_cast<float *>(
              (char *)solver + OFF_IKSOLVER_IKPOS_Z)};
      positionWeight = *reinterpret_cast<float *>(
          (char *)solver + OFF_IKSOLVER_IKPOS_WEIGHT);
      rotationWeight = *reinterpret_cast<float *>(
          (char *)solver + OFF_IKSOLVER_IKROT_WEIGHT);
      rotation = DirectVmdNormalizeQuaternion({
          *reinterpret_cast<float *>(
              (char *)solver + OFF_IKSOLVER_IKROT_X),
          *reinterpret_cast<float *>(
              (char *)solver + OFF_IKSOLVER_IKROT_Y),
          *reinterpret_cast<float *>(
              (char *)solver + OFF_IKSOLVER_IKROT_Z),
          *reinterpret_cast<float *>(
              (char *)solver + OFF_IKSOLVER_IKROT_W)});
    } __except (EXCEPTION_EXECUTE_HANDLER) {
      continue;
    }
    if (!DirectVmdFinite(target.x) || !DirectVmdFinite(target.y) ||
        !DirectVmdFinite(target.z) ||
        !DirectVmdFinite(positionWeight) ||
        !DirectVmdFinite(rotationWeight) ||
        !DirectVmdFinite(rotation.x) || !DirectVmdFinite(rotation.y) ||
        !DirectVmdFinite(rotation.z) || !DirectVmdFinite(rotation.w))
      continue;
    state.cachedTarget[index] = target;
    state.cachedRotation[index] = rotation;
    state.cachedPositionWeight[index] = positionWeight;
    state.cachedRotationWeight[index] = rotationWeight;
    state.cachedTargetValid[index] = true;
    state.cachedTargetFrame[index] = frame;
  }
  GhostRig_CaptureFootPhysicsPlanes(grounding, frame);
}

static void GhostRig_LogGrounderSolverState(void *grounder, int frame) {
  GhostRigGrounderRuntime &state = s_ghostRig.grounder;
  const bool periodic = state.lastLogFrame == INT_MIN || frame < 0 ||
      frame - state.lastLogFrame >= 120;
  if (!periodic || !grounder)
    return;
  state.lastLogFrame = frame;

  float weight = -1.0f;
  float maintainWeight = -1.0f;
  float adsorbWeight = -1.0f;
  float lastWeight = -1.0f;
  float lastAdsorb = -1.0f;
  float leftFootOffsetY = 0.0f;
  float rightFootOffsetY = 0.0f;
  float leftFootOrientation = 0.0f;
  float rightFootOrientation = 0.0f;
  bool initiated = false;
  bool grounded = false;
  void *grounding = nullptr;
  __try {
    weight = *reinterpret_cast<float *>(
        (char *)grounder + OFF_GROUNDER_WEIGHT);
    maintainWeight = *reinterpret_cast<float *>(
        (char *)grounder + OFF_GROUNDER_MAINTAIN_WEIGHT);
    adsorbWeight = *reinterpret_cast<float *>(
        (char *)grounder + OFF_GROUNDER_ADSORB_WEIGHT);
    lastWeight = *reinterpret_cast<float *>(
        (char *)grounder + OFF_GROUNDER_LAST_WEIGHT);
    lastAdsorb = *reinterpret_cast<float *>(
        (char *)grounder + OFF_GROUNDER_LAST_ADSORB);
    leftFootOffsetY = *reinterpret_cast<float *>(
        (char *)grounder + OFF_GROUNDER_LEFT_FOOT_Y);
    rightFootOffsetY = *reinterpret_cast<float *>(
        (char *)grounder + OFF_GROUNDER_RIGHT_FOOT_Y);
    leftFootOrientation = *reinterpret_cast<float *>(
        (char *)grounder + OFF_GROUNDER_LEFT_FOOT_ORI);
    rightFootOrientation = *reinterpret_cast<float *>(
        (char *)grounder + OFF_GROUNDER_RIGHT_FOOT_ORI);
    initiated = *reinterpret_cast<bool *>(
        (char *)grounder + OFF_GROUNDER_INITIATED);
    grounding = *reinterpret_cast<void **>(
        (char *)grounder + OFF_GROUNDER_SOLVER);
    if (grounding && g_finalIkGroundingLayoutOk)
      grounded = *reinterpret_cast<bool *>(
          (char *)grounding + OFF_GROUNDING_IS_GROUNDED);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    grounding = nullptr;
  }

  float positionWeight[DIRECT_VMD_LEG_SIDE_COUNT] = {-1.0f, -1.0f};
  float rotationWeight[DIRECT_VMD_LEG_SIDE_COUNT] = {-1.0f, -1.0f};
  VmdVec3 solverTarget[DIRECT_VMD_LEG_SIDE_COUNT] = {};
  for (uint32_t index = 0; index < DIRECT_VMD_LEG_SIDE_COUNT; ++index) {
    void *solver = GhostRig_GetLegSolver(
        static_cast<DirectVmdLegSide>(index));
    if (!solver)
      continue;
    __try {
      solverTarget[index] = {
          *reinterpret_cast<float *>(
              (char *)solver + OFF_IKSOLVER_IKPOS_X),
          *reinterpret_cast<float *>(
              (char *)solver + OFF_IKSOLVER_IKPOS_Y),
          *reinterpret_cast<float *>(
              (char *)solver + OFF_IKSOLVER_IKPOS_Z)};
      positionWeight[index] = *reinterpret_cast<float *>(
          (char *)solver + OFF_IKSOLVER_IKPOS_WEIGHT);
      rotationWeight[index] = *reinterpret_cast<float *>(
          (char *)solver + OFF_IKSOLVER_IKROT_WEIGHT);
    } __except (EXCEPTION_EXECUTE_HANDLER) {
      positionWeight[index] = -1.0f;
      rotationWeight[index] = -1.0f;
    }
  }
  Log("[P7-GROUNDER-FRAME] unityFrame=%d vmdFrame=%.6f "
      "active=1 initiated=%d grounded=%d grounder=%p grounding=%p "
      "weight=%.3f maintainWeight=%.3f adsorbWeight=%.3f "
      "lastWeight=%.3f lastAdsorbWeight=%.3f "
      "leftIk=%d leftTarget=(%.6f,%.6f,%.6f) leftPosWeight=%.3f "
      "leftRotWeight=%.3f leftOffsetY=%.6f leftOrientation=%.6f "
      "leftResidual=%.6f rightIk=%d "
      "rightTarget=(%.6f,%.6f,%.6f) rightPosWeight=%.3f "
      "rightRotWeight=%.3f rightOffsetY=%.6f rightOrientation=%.6f "
      "rightResidual=%.6f solverUpdates=%llu postSolverUpdates=%llu "
      "rootOwner=DirectVmd-Grounder-hybrid "
      "grounderRole=main-thread-probe-scheduler-and-layer-provider "
      "finalPoseOwner=DirectVmd-FinalIK "
      "capturedTargetUse=grounding-raycast-foot-xz-primary-next-frame "
      "predictionFieldsDiagnosticOnly=1 "
      "customFloorQueries=0 "
      "generation=%llu owner=%p tid=%lu",
      frame, s_ghostRig.lastSourceFrame, initiated ? 1 : 0,
      grounded ? 1 : 0, grounder, grounding, weight,
      maintainWeight, adsorbWeight, lastWeight, lastAdsorb,
      s_ghostRig.legs[0].effectiveIkEnabled ? 1 : 0,
      solverTarget[0].x, solverTarget[0].y, solverTarget[0].z,
      positionWeight[0], rotationWeight[0], leftFootOffsetY,
      leftFootOrientation, s_ghostRig.legs[0].reach.residual,
      s_ghostRig.legs[1].effectiveIkEnabled ? 1 : 0,
      solverTarget[1].x, solverTarget[1].y, solverTarget[1].z,
      positionWeight[1], rotationWeight[1], rightFootOffsetY,
      rightFootOrientation, s_ghostRig.legs[1].reach.residual,
      (unsigned long long)state.solverUpdateCount,
      (unsigned long long)state.postSolverUpdateCount,
      (unsigned long long)s_ghostRig.generation,
      reinterpret_cast<void *>(s_ghostRig.ownerCharacter),
      GetCurrentThreadId());
}

static void __fastcall GhostRig_HookedGrounderOnSolverUpdate(
    void *self, void *methodInfo) {
  typedef void(__fastcall *fn)(void *, void *);
  const bool active = GhostRig_IsActiveGrounderCallback(self);
  const int frame = active ? GhostRig_GetFrameCount() : INT_MIN;
  if (active) {
    s_ghostRig.grounder.lastSolverEnterFrame = frame;
    ++s_ghostRig.grounder.solverUpdateCount;
    GhostRig_LogOrder("GrounderBipedIK.OnSolverUpdate.enter", frame,
                      s_ghostRig.ownerCharacter,
                      s_ghostRig.generation);
  }
  if (s_ghostOrigGrounderOnSolverUpdate)
    reinterpret_cast<fn>(s_ghostOrigGrounderOnSolverUpdate)(
        self, methodInfo);
  if (active) {
    s_ghostRig.grounder.lastSolverExitFrame = frame;
    GhostRig_CaptureGrounderTargets(self, frame);
    GhostRig_LogOrder("GrounderBipedIK.OnSolverUpdate.exit", frame,
                      s_ghostRig.ownerCharacter,
                      s_ghostRig.generation);
    GhostRig_LogGrounderSolverState(self, frame);
  }
}

static void __fastcall GhostRig_HookedGrounderOnPostSolverUpdate(
    void *self, void *methodInfo) {
  typedef void(__fastcall *fn)(void *, void *);
  const bool active = GhostRig_IsActiveGrounderCallback(self);
  const int frame = active ? GhostRig_GetFrameCount() : INT_MIN;
  if (active)
    GhostRig_LogOrder("GrounderBipedIK.OnPostSolverUpdate.enter", frame,
                      s_ghostRig.ownerCharacter,
                      s_ghostRig.generation);
  if (s_ghostOrigGrounderOnPostSolverUpdate)
    reinterpret_cast<fn>(s_ghostOrigGrounderOnPostSolverUpdate)(
        self, methodInfo);
  if (active) {
    s_ghostRig.grounder.lastPostSolverFrame = frame;
    ++s_ghostRig.grounder.postSolverUpdateCount;
    GhostRig_LogOrder("GrounderBipedIK.OnPostSolverUpdate.exit", frame,
                      s_ghostRig.ownerCharacter,
                      s_ghostRig.generation);
  }
}

static void __fastcall GhostRig_HookedAnimatorPreLateTick(
    void *self, float deltaTime, void *methodInfo) {
  typedef void(__fastcall *fn)(void *, float, void *);
  GhostRig_LogAnimatorStage(self, "AnimatorMono.PreLateTick.enter");
  if (s_ghostOrigAnimatorPreLateTick)
    reinterpret_cast<fn>(s_ghostOrigAnimatorPreLateTick)(self, deltaTime,
                                                         methodInfo);
  GhostRig_LogAnimatorStage(self, "AnimatorMono.PreLateTick.exit");
}

static void __fastcall GhostRig_HookedAnimatorMove(void *self,
                                                   void *methodInfo) {
  typedef void(__fastcall *fn)(void *, void *);
  GhostRig_LogAnimatorStage(self, "AnimatorMono.OnAnimatorMove.enter");
  if (s_ghostOrigAnimatorMove)
    reinterpret_cast<fn>(s_ghostOrigAnimatorMove)(self, methodInfo);
  GhostRig_LogAnimatorStage(self, "AnimatorMono.OnAnimatorMove.exit");
}

static bool GhostRig_TryImmediateCleanupOnCurrentThread(
    GhostRigCleanupReason reason) {
  if (!GhostRig_RequireMainThread("GhostRig.ImmediateCleanup", false)) {
    Log("[P0-GHOST-CLEANUP] deferred reason=%s currentTid=%lu mainTid=%lu",
        GhostRig_CleanupReasonName(reason), GetCurrentThreadId(),
        s_ghostMainThreadId.load(std::memory_order_acquire));
    return false;
  }
  GhostRig_ProcessLifecycleMainThread();
  if (s_ghostRig.state != GhostRigState::Empty)
    GhostRig_DestroyMainThread(reason);
  else
    Log("[P0-GHOST-CLEANUP] idempotent-empty reason=%s tid=%lu "
        "generation=%llu",
        GhostRig_CleanupReasonName(reason), GetCurrentThreadId(),
        (unsigned long long)s_ghostRig.generation);
  return s_ghostRig.state == GhostRigState::Empty;
}

static void *GhostRig_FindMethodByFirstParameter(
    void *klass, const char *methodName, int parameterCount,
    const char *parameterNamespace, const char *parameterClassName) {
  if (!klass || !il2cpp_method_get_param || !il2cpp_class_from_type)
    return nullptr;
  void *iterator = nullptr;
  void *method = nullptr;
  while ((method = il2cpp_class_get_methods(klass, &iterator)) != nullptr) {
    const char *name = il2cpp_method_get_name(method);
    if (!name || strcmp(name, methodName) != 0 ||
        static_cast<int>(il2cpp_method_get_param_count(method)) !=
            parameterCount)
      continue;
    void *parameterType = il2cpp_method_get_param(method, 0);
    void *parameterClass = parameterType
        ? il2cpp_class_from_type(parameterType)
        : nullptr;
    if (!parameterClass)
      continue;
    const char *className = il2cpp_class_get_name(parameterClass);
    const char *classNamespace = il2cpp_class_get_namespace(parameterClass);
    if (className && classNamespace &&
        strcmp(className, parameterClassName) == 0 &&
        strcmp(classNamespace, parameterNamespace) == 0)
      return method;
  }
  return nullptr;
}

static void GhostRig_ResolveUnityApis(void **assemblies,
                                      size_t assemblyCount) {
  s_ghostGameObjectCtor = GhostRig_FindMethodByFirstParameter(
      g_gameObjectClass, ".ctor", 1, "System", "String");
  s_ghostTransformSetParent =
      g_transformClass ? FindMethod(g_transformClass, "SetParent", 2) : nullptr;

  void *objectClass = FindClass("UnityEngine", "Object", assemblies,
                                assemblyCount);
  if (objectClass) {
    s_ghostObjectGetHideFlags =
        FindMethod(objectClass, "get_hideFlags", 0);
    s_ghostObjectSetHideFlags =
        FindMethod(objectClass, "set_hideFlags", 1);
    s_ghostObjectGetInstanceId =
        FindMethod(objectClass, "GetInstanceID", 0);
    s_ghostObjectDestroy = GhostRig_FindMethodByFirstParameter(
        objectClass, "Destroy", 1, "UnityEngine", "Object");
    s_ghostObjectImplicit = GhostRig_FindMethodByFirstParameter(
        objectClass, "op_Implicit", 1, "UnityEngine", "Object");
  }
  s_ghostGameObjectGetComponents = GhostRig_FindMethodByFirstParameter(
      g_gameObjectClass, "GetComponents", 1, "System", "Type");

  void *sceneManagerClass = FindClass("UnityEngine.SceneManagement",
                                      "SceneManager", assemblies,
                                      assemblyCount);
  if (sceneManagerClass) {
    s_ghostSceneGetActiveScene =
        FindMethod(sceneManagerClass, "GetActiveScene", 0);
  }
  void *timeClass = FindClass("UnityEngine", "Time", assemblies,
                              assemblyCount);
  if (timeClass) {
    s_ghostTimeGetFrameCount =
        FindMethod(timeClass, "get_frameCount", 0);
  }

  void *grounderClass = FindClass(
      "RootMotion.FinalIK", "GrounderBipedIK", assemblies,
      assemblyCount);
  if (grounderClass) {
    s_ghostGrounderUpdate = FindMethod(grounderClass, "Update", 0);
    s_ghostGrounderResetPosition =
        FindMethod(grounderClass, "ResetPosition", 0);
  }

  void *avatarClass = FindClass("UnityEngine", "Avatar", assemblies,
                                assemblyCount);
  s_ghostHumanDescriptionClass =
      FindClass("UnityEngine", "HumanDescription", assemblies,
                assemblyCount);
  s_ghostSkeletonBoneClass =
      FindClass("UnityEngine", "SkeletonBone", assemblies,
                assemblyCount);
  if (avatarClass) {
    s_ghostAvatarGetHumanDescription =
        FindMethod(avatarClass, "get_humanDescription", 0);
  }
  if (s_ghostHumanDescriptionClass) {
    const char *names[] = {"m_Skeleton", "skeleton"};
    s_ghostHumanSkeletonOffset = FindFieldInHierarchy(
        s_ghostHumanDescriptionClass, names,
        static_cast<int>(sizeof(names) / sizeof(names[0])), nullptr);
    if (il2cpp_class_value_size) {
      uint32_t alignment = 0;
      s_ghostHumanDescriptionSize = il2cpp_class_value_size(
          s_ghostHumanDescriptionClass, &alignment);
      if (s_ghostHumanDescriptionSize > 0 &&
          s_ghostHumanSkeletonOffset >= 16 &&
          s_ghostHumanSkeletonOffset +
                  static_cast<int>(sizeof(void *)) >
              s_ghostHumanDescriptionSize &&
          s_ghostHumanSkeletonOffset - 16 +
                  static_cast<int>(sizeof(void *)) <=
              s_ghostHumanDescriptionSize) {
        s_ghostHumanSkeletonOffset -= 16;
        Log("[P2-BIND-API] normalized HumanDescription boxed field "
            "offset by -0x10");
      }
    }
  }
  if (s_ghostSkeletonBoneClass) {
    const char *nameFields[] = {"m_Name", "name"};
    const char *positionFields[] = {"m_Position", "position"};
    const char *rotationFields[] = {"m_Rotation", "rotation"};
    const char *scaleFields[] = {"m_Scale", "scale"};
    s_ghostSkeletonNameOffset = FindFieldInHierarchy(
        s_ghostSkeletonBoneClass, nameFields,
        static_cast<int>(sizeof(nameFields) / sizeof(nameFields[0])),
        nullptr);
    s_ghostSkeletonPositionOffset = FindFieldInHierarchy(
        s_ghostSkeletonBoneClass, positionFields,
        static_cast<int>(sizeof(positionFields) /
                         sizeof(positionFields[0])),
        nullptr);
    s_ghostSkeletonRotationOffset = FindFieldInHierarchy(
        s_ghostSkeletonBoneClass, rotationFields,
        static_cast<int>(sizeof(rotationFields) /
                         sizeof(rotationFields[0])),
        nullptr);
    s_ghostSkeletonScaleOffset = FindFieldInHierarchy(
        s_ghostSkeletonBoneClass, scaleFields,
        static_cast<int>(sizeof(scaleFields) / sizeof(scaleFields[0])),
        nullptr);
    if (il2cpp_class_value_size) {
      uint32_t alignment = 0;
      s_ghostSkeletonStride = il2cpp_class_value_size(
          s_ghostSkeletonBoneClass, &alignment);
      Log("[P2-BIND-API] SkeletonBone valueSize=%d alignment=%u",
          s_ghostSkeletonStride, alignment);
    }
    if (s_ghostSkeletonNameOffset < 0 ||
        s_ghostSkeletonPositionOffset < 0 ||
        s_ghostSkeletonRotationOffset < 0 ||
        s_ghostSkeletonScaleOffset < 0) {
      s_ghostSkeletonStride = 0;
    }
    if (s_ghostSkeletonStride <= 0 &&
        s_ghostSkeletonNameOffset >= 0 &&
        s_ghostSkeletonPositionOffset >= 0 &&
        s_ghostSkeletonRotationOffset >= 0 &&
        s_ghostSkeletonScaleOffset >= 0) {
      if (s_ghostSkeletonNameOffset == 16 &&
          s_ghostSkeletonPositionOffset >= 16 &&
          s_ghostSkeletonRotationOffset >= 16 &&
          s_ghostSkeletonScaleOffset >= 16) {
        s_ghostSkeletonNameOffset -= 16;
        s_ghostSkeletonPositionOffset -= 16;
        s_ghostSkeletonRotationOffset -= 16;
        s_ghostSkeletonScaleOffset -= 16;
      }
      const int derivedEnd = (std::max)(
          (std::max)(s_ghostSkeletonNameOffset +
                         static_cast<int>(sizeof(void *)),
                     s_ghostSkeletonPositionOffset +
                         static_cast<int>(sizeof(VmdVec3))),
          (std::max)(s_ghostSkeletonRotationOffset +
                         static_cast<int>(sizeof(VmdQuaternion)),
                     s_ghostSkeletonScaleOffset +
                         static_cast<int>(sizeof(VmdVec3))));
      const int derivedStride = (derivedEnd + 7) & ~7;
      if (s_ghostSkeletonNameOffset == 0 && derivedStride >= 40 &&
          derivedStride <= 128) {
        s_ghostSkeletonStride = derivedStride;
        Log("[P2-BIND-API] SkeletonBone stride=%d derived from verified "
            "field layout (class_value_size unavailable)",
            derivedStride);
      }
    }
    if (s_ghostSkeletonStride > 0) {
      const int maximumEnd = (std::max)(
          (std::max)(s_ghostSkeletonNameOffset +
                         static_cast<int>(sizeof(void *)),
                     s_ghostSkeletonPositionOffset +
                         static_cast<int>(sizeof(VmdVec3))),
          (std::max)(s_ghostSkeletonRotationOffset +
                         static_cast<int>(sizeof(VmdQuaternion)),
                     s_ghostSkeletonScaleOffset +
                         static_cast<int>(sizeof(VmdVec3))));
      if (maximumEnd > s_ghostSkeletonStride &&
          s_ghostSkeletonNameOffset >= 16 &&
          s_ghostSkeletonPositionOffset >= 16 &&
          s_ghostSkeletonRotationOffset >= 16 &&
          s_ghostSkeletonScaleOffset >= 16 &&
          maximumEnd - 16 <= s_ghostSkeletonStride) {
        s_ghostSkeletonNameOffset -= 16;
        s_ghostSkeletonPositionOffset -= 16;
        s_ghostSkeletonRotationOffset -= 16;
        s_ghostSkeletonScaleOffset -= 16;
        Log("[P2-BIND-API] normalized SkeletonBone boxed field offsets "
            "by -0x10");
      } else if (maximumEnd > s_ghostSkeletonStride) {
        s_ghostSkeletonStride = 0;
      }
    }
  }

  Log("[P0-GHOST-API] ctor=%p setParent=%p getHide=%p setHide=%p "
      "instanceId=%p destroy=%p implicit=%p getComponents=%p activeScene=%p "
      "frameCount=%p defaultEnabled=%d dontDestroyOnLoadResolved=0",
      s_ghostGameObjectCtor, s_ghostTransformSetParent,
      s_ghostObjectGetHideFlags, s_ghostObjectSetHideFlags,
      s_ghostObjectGetInstanceId, s_ghostObjectDestroy,
      s_ghostObjectImplicit,
      s_ghostGameObjectGetComponents, s_ghostSceneGetActiveScene,
      s_ghostTimeGetFrameCount, GhostRig_IsRequestedEnabled() ? 1 : 0);
  Log("[P2-BIND-API] Avatar.get_humanDescription=%p HumanDescription=%p "
      "SkeletonBone=%p humanDescriptionSize=%d skeletonOffset=0x%X stride=%d "
      "fieldOffsets=%d/%d/%d/%d classValueSize=%p",
      s_ghostAvatarGetHumanDescription, s_ghostHumanDescriptionClass,
      s_ghostSkeletonBoneClass, s_ghostHumanDescriptionSize,
      s_ghostHumanSkeletonOffset,
      s_ghostSkeletonStride, s_ghostSkeletonNameOffset,
      s_ghostSkeletonPositionOffset, s_ghostSkeletonRotationOffset,
      s_ghostSkeletonScaleOffset, il2cpp_class_value_size);
  Log("[P7-GROUNDER-API] Update=%p ResetPosition=%p "
      "componentSource=MovementComponent.m_bipedIK mainThreadOnly=1",
      s_ghostGrounderUpdate, s_ghostGrounderResetPosition);
}
