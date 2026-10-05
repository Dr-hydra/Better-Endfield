#pragma once

#include <algorithm>
#include <cmath>
#include <cstdint>
#include <cstring>
#include <limits>
#include <type_traits>

enum class DirectVmdBoneId : uint32_t {
  AllParent = 0,
  Center,
  Groove,
  LowerBody,
  UpperBody,
  UpperBody2,
  Neck,
  Head,
  LeftShoulder,
  LeftArm,
  LeftElbow,
  LeftWrist,
  RightShoulder,
  RightArm,
  RightElbow,
  RightWrist,
  LeftLeg,
  LeftKnee,
  LeftAnkle,
  LeftToe,
  RightLeg,
  RightKnee,
  RightAnkle,
  RightToe,
  LeftFootIkParent,
  LeftFootIk,
  LeftToeIk,
  RightFootIkParent,
  RightFootIk,
  RightToeIk,
  LeftThumb0,
  LeftThumb1,
  LeftThumb2,
  LeftIndex1,
  LeftIndex2,
  LeftIndex3,
  LeftMiddle1,
  LeftMiddle2,
  LeftMiddle3,
  LeftRing1,
  LeftRing2,
  LeftRing3,
  LeftLittle1,
  LeftLittle2,
  LeftLittle3,
  RightThumb0,
  RightThumb1,
  RightThumb2,
  RightIndex1,
  RightIndex2,
  RightIndex3,
  RightMiddle1,
  RightMiddle2,
  RightMiddle3,
  RightRing1,
  RightRing2,
  RightRing3,
  RightLittle1,
  RightLittle2,
  RightLittle3,
  BothEyes,
  LeftEye,
  RightEye,
  LeftArmTwist,
  LeftWristTwist,
  RightArmTwist,
  RightWristTwist,
  Count
};

static constexpr uint32_t DIRECT_VMD_BONE_COUNT =
    static_cast<uint32_t>(DirectVmdBoneId::Count);

struct DirectVmdBoneSpec {
  const char *name;
  const char *alternateName;
  int parent;
};

static constexpr DirectVmdBoneSpec
    kDirectVmdBoneSpecs[DIRECT_VMD_BONE_COUNT] = {
        {u8"全ての親", nullptr, -1},
        {u8"センター", nullptr,
         static_cast<int>(DirectVmdBoneId::AllParent)},
        {u8"グルーブ", nullptr,
         static_cast<int>(DirectVmdBoneId::Center)},
        {u8"下半身", nullptr,
         static_cast<int>(DirectVmdBoneId::Groove)},
        {u8"上半身", nullptr,
         static_cast<int>(DirectVmdBoneId::Groove)},
        {u8"上半身2", u8"上半身２",
         static_cast<int>(DirectVmdBoneId::UpperBody)},
        {u8"首", nullptr, static_cast<int>(DirectVmdBoneId::UpperBody2)},
        {u8"頭", nullptr, static_cast<int>(DirectVmdBoneId::Neck)},
        {u8"左肩", nullptr,
         static_cast<int>(DirectVmdBoneId::UpperBody2)},
        {u8"左腕", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftShoulder)},
        {u8"左ひじ", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftArm)},
        {u8"左手首", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftElbow)},
        {u8"右肩", nullptr,
         static_cast<int>(DirectVmdBoneId::UpperBody2)},
        {u8"右腕", nullptr,
         static_cast<int>(DirectVmdBoneId::RightShoulder)},
        {u8"右ひじ", nullptr,
         static_cast<int>(DirectVmdBoneId::RightArm)},
        {u8"右手首", nullptr,
         static_cast<int>(DirectVmdBoneId::RightElbow)},
        {u8"左足", nullptr,
         static_cast<int>(DirectVmdBoneId::LowerBody)},
        {u8"左ひざ", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftLeg)},
        {u8"左足首", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftKnee)},
        {u8"左つま先", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftAnkle)},
        {u8"右足", nullptr,
         static_cast<int>(DirectVmdBoneId::LowerBody)},
        {u8"右ひざ", nullptr,
         static_cast<int>(DirectVmdBoneId::RightLeg)},
        {u8"右足首", nullptr,
         static_cast<int>(DirectVmdBoneId::RightKnee)},
        {u8"右つま先", nullptr,
         static_cast<int>(DirectVmdBoneId::RightAnkle)},
        {u8"左足IK親", u8"左足ＩＫ親",
         static_cast<int>(DirectVmdBoneId::AllParent)},
        {u8"左足ＩＫ", u8"左足IK",
         static_cast<int>(DirectVmdBoneId::LeftFootIkParent)},
        {u8"左つま先ＩＫ", u8"左つま先IK",
         static_cast<int>(DirectVmdBoneId::LeftFootIk)},
        {u8"右足IK親", u8"右足ＩＫ親",
         static_cast<int>(DirectVmdBoneId::AllParent)},
        {u8"右足ＩＫ", u8"右足IK",
         static_cast<int>(DirectVmdBoneId::RightFootIkParent)},
        {u8"右つま先ＩＫ", u8"右つま先IK",
         static_cast<int>(DirectVmdBoneId::RightFootIk)},
        {u8"左親指０", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftWrist)},
        {u8"左親指１", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftThumb0)},
        {u8"左親指２", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftThumb1)},
        {u8"左人指１", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftWrist)},
        {u8"左人指２", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftIndex1)},
        {u8"左人指３", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftIndex2)},
        {u8"左中指１", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftWrist)},
        {u8"左中指２", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftMiddle1)},
        {u8"左中指３", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftMiddle2)},
        {u8"左薬指１", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftWrist)},
        {u8"左薬指２", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftRing1)},
        {u8"左薬指３", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftRing2)},
        {u8"左小指１", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftWrist)},
        {u8"左小指２", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftLittle1)},
        {u8"左小指３", nullptr,
         static_cast<int>(DirectVmdBoneId::LeftLittle2)},
        {u8"右親指０", nullptr,
         static_cast<int>(DirectVmdBoneId::RightWrist)},
        {u8"右親指１", nullptr,
         static_cast<int>(DirectVmdBoneId::RightThumb0)},
        {u8"右親指２", nullptr,
         static_cast<int>(DirectVmdBoneId::RightThumb1)},
        {u8"右人指１", nullptr,
         static_cast<int>(DirectVmdBoneId::RightWrist)},
        {u8"右人指２", nullptr,
         static_cast<int>(DirectVmdBoneId::RightIndex1)},
        {u8"右人指３", nullptr,
         static_cast<int>(DirectVmdBoneId::RightIndex2)},
        {u8"右中指１", nullptr,
         static_cast<int>(DirectVmdBoneId::RightWrist)},
        {u8"右中指２", nullptr,
         static_cast<int>(DirectVmdBoneId::RightMiddle1)},
        {u8"右中指３", nullptr,
         static_cast<int>(DirectVmdBoneId::RightMiddle2)},
        {u8"右薬指１", nullptr,
         static_cast<int>(DirectVmdBoneId::RightWrist)},
        {u8"右薬指２", nullptr,
         static_cast<int>(DirectVmdBoneId::RightRing1)},
        {u8"右薬指３", nullptr,
         static_cast<int>(DirectVmdBoneId::RightRing2)},
        {u8"右小指１", nullptr,
         static_cast<int>(DirectVmdBoneId::RightWrist)},
        {u8"右小指２", nullptr,
         static_cast<int>(DirectVmdBoneId::RightLittle1)},
        {u8"右小指３", nullptr,
         static_cast<int>(DirectVmdBoneId::RightLittle2)},
        {u8"両目", nullptr,
         static_cast<int>(DirectVmdBoneId::Head)},
        {u8"左目", nullptr,
         static_cast<int>(DirectVmdBoneId::BothEyes)},
        {u8"右目", nullptr,
         static_cast<int>(DirectVmdBoneId::BothEyes)},
        {u8"左腕捩", u8"左腕捩れ",
         static_cast<int>(DirectVmdBoneId::LeftArm)},
        {u8"左手捩", u8"左手捩れ",
         static_cast<int>(DirectVmdBoneId::LeftElbow)},
        {u8"右腕捩", u8"右腕捩れ",
         static_cast<int>(DirectVmdBoneId::RightArm)},
        {u8"右手捩", u8"右手捩れ",
         static_cast<int>(DirectVmdBoneId::RightElbow)},
};

static inline uint32_t DirectVmdBoneIndex(DirectVmdBoneId id) {
  return static_cast<uint32_t>(id);
}

static constexpr DirectVmdBoneId kDirectVmdPhase3FkBones[] = {
    DirectVmdBoneId::LowerBody,
    DirectVmdBoneId::UpperBody,
    DirectVmdBoneId::UpperBody2,
    DirectVmdBoneId::Neck,
    DirectVmdBoneId::Head,
    DirectVmdBoneId::LeftShoulder,
    DirectVmdBoneId::LeftArm,
    DirectVmdBoneId::LeftElbow,
    DirectVmdBoneId::LeftWrist,
    DirectVmdBoneId::RightShoulder,
    DirectVmdBoneId::RightArm,
    DirectVmdBoneId::RightElbow,
    DirectVmdBoneId::RightWrist,
};

static constexpr uint32_t DIRECT_VMD_PHASE3_FK_BONE_COUNT =
    static_cast<uint32_t>(sizeof(kDirectVmdPhase3FkBones) /
                          sizeof(kDirectVmdPhase3FkBones[0]));

static constexpr DirectVmdBoneId kDirectVmdPhase6FingerBones[] = {
    DirectVmdBoneId::LeftThumb0, DirectVmdBoneId::LeftThumb1,
    DirectVmdBoneId::LeftThumb2, DirectVmdBoneId::LeftIndex1,
    DirectVmdBoneId::LeftIndex2, DirectVmdBoneId::LeftIndex3,
    DirectVmdBoneId::LeftMiddle1, DirectVmdBoneId::LeftMiddle2,
    DirectVmdBoneId::LeftMiddle3, DirectVmdBoneId::LeftRing1,
    DirectVmdBoneId::LeftRing2, DirectVmdBoneId::LeftRing3,
    DirectVmdBoneId::LeftLittle1, DirectVmdBoneId::LeftLittle2,
    DirectVmdBoneId::LeftLittle3, DirectVmdBoneId::RightThumb0,
    DirectVmdBoneId::RightThumb1, DirectVmdBoneId::RightThumb2,
    DirectVmdBoneId::RightIndex1, DirectVmdBoneId::RightIndex2,
    DirectVmdBoneId::RightIndex3, DirectVmdBoneId::RightMiddle1,
    DirectVmdBoneId::RightMiddle2, DirectVmdBoneId::RightMiddle3,
    DirectVmdBoneId::RightRing1, DirectVmdBoneId::RightRing2,
    DirectVmdBoneId::RightRing3, DirectVmdBoneId::RightLittle1,
    DirectVmdBoneId::RightLittle2, DirectVmdBoneId::RightLittle3,
};

static constexpr uint32_t DIRECT_VMD_PHASE6_FINGER_BONE_COUNT =
    static_cast<uint32_t>(sizeof(kDirectVmdPhase6FingerBones) /
                          sizeof(kDirectVmdPhase6FingerBones[0]));

static constexpr bool DirectVmdIsFingerBone(DirectVmdBoneId id) {
  return id >= DirectVmdBoneId::LeftThumb0 &&
         id <= DirectVmdBoneId::RightLittle3;
}

static constexpr DirectVmdBoneId kDirectVmdPhase6EyeBones[] = {
    DirectVmdBoneId::LeftEye,
    DirectVmdBoneId::RightEye,
};

static constexpr uint32_t DIRECT_VMD_PHASE6_EYE_BONE_COUNT =
    static_cast<uint32_t>(sizeof(kDirectVmdPhase6EyeBones) /
                          sizeof(kDirectVmdPhase6EyeBones[0]));

struct DirectVmdTwistChannelSpec {
  DirectVmdBoneId control;
  DirectVmdBoneId sourceLimb;
  DirectVmdBoneId targetLimbEnd;
};

static constexpr DirectVmdTwistChannelSpec kDirectVmdPhase6TwistChannels[] = {
    {DirectVmdBoneId::LeftArmTwist, DirectVmdBoneId::LeftArm,
     DirectVmdBoneId::LeftElbow},
    {DirectVmdBoneId::LeftWristTwist, DirectVmdBoneId::LeftElbow,
     DirectVmdBoneId::LeftWrist},
    {DirectVmdBoneId::RightArmTwist, DirectVmdBoneId::RightArm,
     DirectVmdBoneId::RightElbow},
    {DirectVmdBoneId::RightWristTwist, DirectVmdBoneId::RightElbow,
     DirectVmdBoneId::RightWrist},
};

static constexpr uint32_t DIRECT_VMD_PHASE6_TWIST_CHANNEL_COUNT =
    static_cast<uint32_t>(sizeof(kDirectVmdPhase6TwistChannels) /
                          sizeof(kDirectVmdPhase6TwistChannels[0]));
static constexpr uint32_t DIRECT_VMD_PHASE6_TWIST_TARGETS_PER_CHANNEL = 2;

enum class DirectVmdLegSide : uint8_t {
  Left = 0,
  Right = 1,
};

static constexpr uint32_t DIRECT_VMD_LEG_SIDE_COUNT = 2;
static constexpr uint32_t DIRECT_VMD_LEG_FK_BONE_COUNT = 4;
static constexpr uint32_t DIRECT_VMD_LEG_IK_SEED_BONE_COUNT = 4;

static constexpr DirectVmdBoneId
    kDirectVmdPhase5LegFkBones[DIRECT_VMD_LEG_SIDE_COUNT]
                               [DIRECT_VMD_LEG_FK_BONE_COUNT] = {
        {DirectVmdBoneId::LeftLeg, DirectVmdBoneId::LeftKnee,
         DirectVmdBoneId::LeftAnkle, DirectVmdBoneId::LeftToe},
        {DirectVmdBoneId::RightLeg, DirectVmdBoneId::RightKnee,
         DirectVmdBoneId::RightAnkle, DirectVmdBoneId::RightToe},
};

struct DirectVmdLegControlDecision {
  uint8_t ikEnabled = 0;
  uint8_t fkFinalBoneCount = DIRECT_VMD_LEG_FK_BONE_COUNT;
  uint8_t fkSeedBoneCount = 0;
  uint8_t reserved = 0;
  float finalIkPositionWeight = 0.0f;
  float finalIkRotationWeight = 0.0f;
};

static constexpr DirectVmdLegControlDecision
DirectVmdDecideLegControl(bool ikEnabled) {
  return ikEnabled
             ? DirectVmdLegControlDecision{
                   1, 0, DIRECT_VMD_LEG_IK_SEED_BONE_COUNT, 0, 1.0f,
                   0.0f}
             : DirectVmdLegControlDecision{
                   0, DIRECT_VMD_LEG_FK_BONE_COUNT, 0, 0, 0.0f, 0.0f};
}

static constexpr bool DirectVmdIsPhase3FkBone(DirectVmdBoneId id) {
  switch (id) {
  case DirectVmdBoneId::LowerBody:
  case DirectVmdBoneId::UpperBody:
  case DirectVmdBoneId::UpperBody2:
  case DirectVmdBoneId::Neck:
  case DirectVmdBoneId::Head:
  case DirectVmdBoneId::LeftShoulder:
  case DirectVmdBoneId::LeftArm:
  case DirectVmdBoneId::LeftElbow:
  case DirectVmdBoneId::LeftWrist:
  case DirectVmdBoneId::RightShoulder:
  case DirectVmdBoneId::RightArm:
  case DirectVmdBoneId::RightElbow:
  case DirectVmdBoneId::RightWrist:
    return true;
  default:
    return false;
  }
}

struct DirectVmdBoneSamplePod {
  VmdVec3 position;
  VmdQuaternion rotation;
  uint8_t hasTrack = 0;
  uint8_t reserved[3] = {};
};

static constexpr uint32_t DIRECT_VMD_MAX_MORPH_CHANNELS = 512;
static constexpr uint32_t DIRECT_VMD_MORPH_NAME_BYTES = 64;

struct DirectVmdMorphSamplePod {
  char name[DIRECT_VMD_MORPH_NAME_BYTES] = {};
  float weight = 0.0f;
};

struct DirectVmdCameraSamplePod {
  VmdVec3 interest;
  VmdVec3 rotationEuler;
  float distance = 0.0f;
  float fov = 45.0f;
  uint8_t valid = 0;
  uint8_t fromOverride = 0;
  uint8_t perspective = 0;
  uint8_t reserved = 0;
};

enum class DirectVmdPlaybackState : uint8_t {
  Stopped = 0,
  Playing,
  Paused,
  Ended,
};

static inline bool DirectVmdStartsFreshPlayback(
    DirectVmdPlaybackState previous,
    DirectVmdPlaybackState current) {
  return current == DirectVmdPlaybackState::Playing &&
         (previous == DirectVmdPlaybackState::Stopped ||
          previous == DirectVmdPlaybackState::Ended);
}

struct DirectVmdSampleFrame {
  uint32_t version = 5;
  uint32_t boneCount = DIRECT_VMD_BONE_COUNT;
  uint32_t morphCount = 0;
  uint32_t morphDroppedCount = 0;
  uint64_t sequence = 0;
  uint64_t rigGeneration = 0;
  uint64_t clipGeneration = 0;
  uint64_t playbackCycle = 0;
  uint64_t seekRevision = 0;
  uintptr_t ownerCharacter = 0;
  double sourceFrame = 0.0;
  double durationFrames = 0.0;
  DirectVmdPlaybackState playback = DirectVmdPlaybackState::Stopped;
  uint8_t valid = 0;
  uint8_t modelVisible = 1;
  uint8_t leftFootIkEnabled = 1;
  uint8_t rightFootIkEnabled = 1;
  uint8_t reserved[3] = {};
  DirectVmdBoneSamplePod bones[DIRECT_VMD_BONE_COUNT] = {};
  DirectVmdMorphSamplePod morphs[DIRECT_VMD_MAX_MORPH_CHANNELS] = {};
  DirectVmdCameraSamplePod camera;
};

static_assert(std::is_trivially_copyable<DirectVmdBoneSamplePod>::value,
              "DirectVmd bone sample must remain POD");
static_assert(std::is_trivially_copyable<DirectVmdMorphSamplePod>::value,
              "DirectVmd morph sample must remain POD");
static_assert(std::is_trivially_copyable<DirectVmdCameraSamplePod>::value,
              "DirectVmd camera sample must remain POD");
static_assert(std::is_trivially_copyable<DirectVmdSampleFrame>::value,
              "DirectVmd sample frame must remain POD");

enum class DirectVmdAudioRange : uint8_t {
  Delayed = 0,
  Audible,
  PastEnd,
};

struct DirectVmdAudioTimelineTarget {
  DirectVmdAudioRange range = DirectVmdAudioRange::Delayed;
  int mediaMilliseconds = 0;
};

static inline DirectVmdAudioTimelineTarget DirectVmdComputeAudioTarget(
    double sourceFrame, double offsetSeconds, int mediaLengthMilliseconds) {
  DirectVmdAudioTimelineTarget result;
  const double safeFrame =
      std::isfinite(sourceFrame) ? (std::max)(0.0, sourceFrame) : 0.0;
  const double safeOffset = std::isfinite(offsetSeconds) ? offsetSeconds : 0.0;
  const double desiredMilliseconds =
      safeFrame * (1000.0 / kVmdFramesPerSecond) + safeOffset * 1000.0;
  if (desiredMilliseconds < 0.0) {
    result.range = DirectVmdAudioRange::Delayed;
    result.mediaMilliseconds = 0;
    return result;
  }

  const double intLimit = static_cast<double>((std::numeric_limits<int>::max)());
  int desired = static_cast<int>(
      std::llround((std::min)(desiredMilliseconds, intLimit)));
  if (mediaLengthMilliseconds > 0 && desired >= mediaLengthMilliseconds) {
    result.range = DirectVmdAudioRange::PastEnd;
    result.mediaMilliseconds = mediaLengthMilliseconds;
    return result;
  }
  result.range = DirectVmdAudioRange::Audible;
  result.mediaMilliseconds = desired;
  return result;
}

static inline bool DirectVmdFinite(float value) {
  return std::isfinite(value) != 0;
}

static inline VmdVec3 DirectVmdAdd(VmdVec3 a, VmdVec3 b) {
  return {a.x + b.x, a.y + b.y, a.z + b.z};
}

static inline VmdVec3 DirectVmdSub(VmdVec3 a, VmdVec3 b) {
  return {a.x - b.x, a.y - b.y, a.z - b.z};
}

static inline VmdVec3 DirectVmdScale(VmdVec3 value, float scale) {
  return {value.x * scale, value.y * scale, value.z * scale};
}

static inline float DirectVmdLength(VmdVec3 value) {
  return std::sqrt(value.x * value.x + value.y * value.y +
                   value.z * value.z);
}

static inline float DirectVmdDot(VmdVec3 left, VmdVec3 right) {
  return left.x * right.x + left.y * right.y + left.z * right.z;
}

static inline VmdVec3 DirectVmdCross(VmdVec3 left, VmdVec3 right) {
  return {left.y * right.z - left.z * right.y,
          left.z * right.x - left.x * right.z,
          left.x * right.y - left.y * right.x};
}

static inline bool DirectVmdTryNormalizeVector(VmdVec3 value,
                                               VmdVec3 *normalized) {
  if (!normalized)
    return false;
  const float length = DirectVmdLength(value);
  if (!DirectVmdFinite(length) || length < 1.0e-6f) {
    *normalized = {0.0f, 0.0f, 0.0f};
    return false;
  }
  *normalized = DirectVmdScale(value, 1.0f / length);
  return true;
}

struct DirectVmdReachProjection {
  VmdVec3 origin = {0.0f, 0.0f, 0.0f};
  VmdVec3 desiredTarget = {0.0f, 0.0f, 0.0f};
  VmdVec3 solverTarget = {0.0f, 0.0f, 0.0f};
  float desiredDistance = 0.0f;
  float reachableDistance = 0.0f;
  float residual = 0.0f;
  float maximumReach = 0.0f;
  float limitMargin = 0.0f;
  uint8_t valid = 0;
  uint8_t clamped = 0;
  uint8_t reserved[2] = {};
};

static inline DirectVmdReachProjection DirectVmdProjectLegReach(
    VmdVec3 origin, VmdVec3 desiredTarget, float maximumReach,
    float limitMargin = 0.0f) {
  DirectVmdReachProjection result;
  result.origin = origin;
  result.desiredTarget = desiredTarget;
  result.solverTarget = desiredTarget;
  result.maximumReach = maximumReach;
  result.limitMargin = limitMargin;

  const bool finite = DirectVmdFinite(origin.x) &&
                      DirectVmdFinite(origin.y) &&
                      DirectVmdFinite(origin.z) &&
                      DirectVmdFinite(desiredTarget.x) &&
                      DirectVmdFinite(desiredTarget.y) &&
                      DirectVmdFinite(desiredTarget.z) &&
                      DirectVmdFinite(maximumReach) &&
                      DirectVmdFinite(limitMargin);
  if (!finite || maximumReach <= 0.0f || limitMargin < 0.0f ||
      limitMargin >= maximumReach)
    return result;

  const VmdVec3 direction = DirectVmdSub(desiredTarget, origin);
  const float desiredDistance = DirectVmdLength(direction);
  if (!DirectVmdFinite(desiredDistance))
    return result;

  const float usableReach = maximumReach - limitMargin;
  result.valid = 1;
  result.desiredDistance = desiredDistance;
  result.reachableDistance = (std::min)(desiredDistance, usableReach);
  result.residual = (std::max)(0.0f, desiredDistance - usableReach);
  if (desiredDistance <= usableReach || desiredDistance < 1.0e-6f)
    return result;

  result.solverTarget = DirectVmdAdd(
      origin, DirectVmdScale(direction, usableReach / desiredDistance));
  result.clamped = 1;
  return result;
}

static inline VmdQuaternion DirectVmdNormalizeQuaternion(
    VmdQuaternion value) {
  const float lengthSquared = value.x * value.x + value.y * value.y +
                              value.z * value.z + value.w * value.w;
  if (!DirectVmdFinite(lengthSquared) || lengthSquared < 1.0e-12f)
    return {0.0f, 0.0f, 0.0f, 1.0f};
  const float inverseLength = 1.0f / std::sqrt(lengthSquared);
  value = {value.x * inverseLength, value.y * inverseLength,
           value.z * inverseLength, value.w * inverseLength};

  const bool negate =
      value.w < 0.0f ||
      (value.w == 0.0f &&
       (value.x < 0.0f ||
        (value.x == 0.0f &&
         (value.y < 0.0f || (value.y == 0.0f && value.z < 0.0f)))));
  if (negate) {
    value.x = -value.x;
    value.y = -value.y;
    value.z = -value.z;
    value.w = -value.w;
  }
  return value;
}

static inline VmdQuaternion DirectVmdQuaternionMultiply(
    VmdQuaternion left, VmdQuaternion right) {
  return DirectVmdNormalizeQuaternion(
      {left.w * right.x + left.x * right.w + left.y * right.z -
           left.z * right.y,
       left.w * right.y - left.x * right.z + left.y * right.w +
           left.z * right.x,
       left.w * right.z + left.x * right.y - left.y * right.x +
           left.z * right.w,
       left.w * right.w - left.x * right.x - left.y * right.y -
           left.z * right.z});
}

static inline VmdQuaternion DirectVmdQuaternionInverse(
    VmdQuaternion value) {
  value = DirectVmdNormalizeQuaternion(value);
  return {-value.x, -value.y, -value.z, value.w};
}

static inline VmdQuaternion DirectVmdQuaternionFromAxisAngle(
    VmdVec3 axis, float angleRadians) {
  VmdVec3 unit = {};
  if (!DirectVmdTryNormalizeVector(axis, &unit) ||
      !DirectVmdFinite(angleRadians))
    return {0.0f, 0.0f, 0.0f, 1.0f};
  const float half = 0.5f * angleRadians;
  const float sine = std::sin(half);
  return DirectVmdNormalizeQuaternion(
      {unit.x * sine, unit.y * sine, unit.z * sine, std::cos(half)});
}

static inline bool DirectVmdExtractSignedTwistAngle(
    VmdQuaternion rotation, VmdVec3 axis, float *angleRadians) {
  if (!angleRadians)
    return false;
  *angleRadians = 0.0f;
  VmdVec3 unit = {};
  if (!DirectVmdTryNormalizeVector(axis, &unit))
    return false;
  rotation = DirectVmdNormalizeQuaternion(rotation);
  const float projected = rotation.x * unit.x +
                          rotation.y * unit.y +
                          rotation.z * unit.z;
  const float length = std::sqrt(projected * projected +
                                 rotation.w * rotation.w);
  if (!DirectVmdFinite(length) || length < 1.0e-6f)
    return false;
  float angle = 2.0f * std::atan2(projected / length,
                                  rotation.w / length);
  static constexpr float kPi = 3.14159265358979323846f;
  if (angle > kPi)
    angle -= 2.0f * kPi;
  else if (angle < -kPi)
    angle += 2.0f * kPi;
  if (!DirectVmdFinite(angle))
    return false;
  *angleRadians = angle;
  return true;
}

static inline VmdQuaternion DirectVmdQuaternionFromTo(VmdVec3 from,
                                                      VmdVec3 to) {
  VmdVec3 source = {};
  VmdVec3 target = {};
  if (!DirectVmdTryNormalizeVector(from, &source) ||
      !DirectVmdTryNormalizeVector(to, &target))
    return {0.0f, 0.0f, 0.0f, 1.0f};

  const float dot = (std::max)(-1.0f,
                               (std::min)(DirectVmdDot(source, target),
                                          1.0f));
  if (dot > 0.999999f)
    return {0.0f, 0.0f, 0.0f, 1.0f};
  if (dot < -0.999999f) {
    const float ax = std::fabs(source.x);
    const float ay = std::fabs(source.y);
    const float az = std::fabs(source.z);
    const VmdVec3 cardinal =
        ax <= ay && ax <= az ? VmdVec3{1.0f, 0.0f, 0.0f}
                            : (ay <= az ? VmdVec3{0.0f, 1.0f, 0.0f}
                                        : VmdVec3{0.0f, 0.0f, 1.0f});
    VmdVec3 axis = {};
    if (!DirectVmdTryNormalizeVector(DirectVmdCross(source, cardinal),
                                     &axis))
      return {0.0f, 0.0f, 0.0f, 1.0f};
    return DirectVmdNormalizeQuaternion(
        {axis.x, axis.y, axis.z, 0.0f});
  }

  const VmdVec3 cross = DirectVmdCross(source, target);
  return DirectVmdNormalizeQuaternion(
      {cross.x, cross.y, cross.z, 1.0f + dot});
}

struct DirectVmdToeAimResult {
  VmdVec3 pivot = {0.0f, 0.0f, 0.0f};
  VmdVec3 currentToe = {0.0f, 0.0f, 0.0f};
  VmdVec3 desiredToe = {0.0f, 0.0f, 0.0f};
  VmdVec3 solverToe = {0.0f, 0.0f, 0.0f};
  VmdQuaternion correction = {0.0f, 0.0f, 0.0f, 1.0f};
  VmdQuaternion solverAnkleWorldRotation =
      {0.0f, 0.0f, 0.0f, 1.0f};
  float currentDistance = 0.0f;
  float desiredDistance = 0.0f;
  float reachableDistance = 0.0f;
  float residual = 0.0f;
  uint8_t valid = 0;
  uint8_t reserved[3] = {};
};

static inline DirectVmdToeAimResult DirectVmdSolveToeAim(
    VmdVec3 anklePosition, VmdVec3 currentToePosition,
    VmdVec3 desiredToePosition,
    VmdQuaternion currentAnkleWorldRotation) {
  DirectVmdToeAimResult result;
  result.pivot = anklePosition;
  result.currentToe = currentToePosition;
  result.desiredToe = desiredToePosition;
  result.solverToe = currentToePosition;

  const bool finite =
      DirectVmdFinite(anklePosition.x) &&
      DirectVmdFinite(anklePosition.y) &&
      DirectVmdFinite(anklePosition.z) &&
      DirectVmdFinite(currentToePosition.x) &&
      DirectVmdFinite(currentToePosition.y) &&
      DirectVmdFinite(currentToePosition.z) &&
      DirectVmdFinite(desiredToePosition.x) &&
      DirectVmdFinite(desiredToePosition.y) &&
      DirectVmdFinite(desiredToePosition.z);
  if (!finite)
    return result;

  const VmdVec3 currentDirection =
      DirectVmdSub(currentToePosition, anklePosition);
  const VmdVec3 desiredDirection =
      DirectVmdSub(desiredToePosition, anklePosition);
  result.currentDistance = DirectVmdLength(currentDirection);
  result.desiredDistance = DirectVmdLength(desiredDirection);
  if (!DirectVmdFinite(result.currentDistance) ||
      !DirectVmdFinite(result.desiredDistance) ||
      result.currentDistance < 1.0e-6f ||
      result.desiredDistance < 1.0e-6f)
    return result;

  result.valid = 1;
  result.reachableDistance = result.currentDistance;
  result.residual =
      std::fabs(result.desiredDistance - result.currentDistance);
  result.correction = DirectVmdQuaternionFromTo(
      currentDirection, desiredDirection);
  result.solverAnkleWorldRotation = DirectVmdQuaternionMultiply(
      result.correction,
      DirectVmdNormalizeQuaternion(currentAnkleWorldRotation));
  result.solverToe = DirectVmdAdd(
      anklePosition,
      DirectVmdScale(desiredDirection,
                     result.currentDistance / result.desiredDistance));
  return result;
}

static inline VmdVec3 DirectVmdRotateVector(VmdQuaternion rotation,
                                            VmdVec3 value) {
  rotation = DirectVmdNormalizeQuaternion(rotation);
  const VmdVec3 q = {rotation.x, rotation.y, rotation.z};
  const VmdVec3 cross1 = {q.y * value.z - q.z * value.y,
                          q.z * value.x - q.x * value.z,
                          q.x * value.y - q.y * value.x};
  const VmdVec3 cross2 = {q.y * cross1.z - q.z * cross1.y,
                          q.z * cross1.x - q.x * cross1.z,
                          q.x * cross1.y - q.y * cross1.x};
  return {value.x + 2.0f * (rotation.w * cross1.x + cross2.x),
          value.y + 2.0f * (rotation.w * cross1.y + cross2.y),
          value.z + 2.0f * (rotation.w * cross1.z + cross2.z)};
}

struct DirectVmdOrthonormalFrame {
  VmdVec3 forward = {0.0f, 0.0f, 0.0f};
  VmdVec3 lateral = {0.0f, 0.0f, 0.0f};
  VmdVec3 normal = {0.0f, 0.0f, 0.0f};
  VmdQuaternion rotation = {0.0f, 0.0f, 0.0f, 1.0f};
};

static inline VmdQuaternion DirectVmdQuaternionFromOrthonormalBasis(
    VmdVec3 xAxis, VmdVec3 yAxis, VmdVec3 zAxis) {
  const float m00 = xAxis.x;
  const float m01 = yAxis.x;
  const float m02 = zAxis.x;
  const float m10 = xAxis.y;
  const float m11 = yAxis.y;
  const float m12 = zAxis.y;
  const float m20 = xAxis.z;
  const float m21 = yAxis.z;
  const float m22 = zAxis.z;
  const float trace = m00 + m11 + m22;
  VmdQuaternion result = {0.0f, 0.0f, 0.0f, 1.0f};
  if (trace > 0.0f) {
    const float scale = 2.0f * std::sqrt((std::max)(trace + 1.0f, 0.0f));
    if (scale < 1.0e-6f)
      return result;
    result = {(m21 - m12) / scale, (m02 - m20) / scale,
              (m10 - m01) / scale, 0.25f * scale};
  } else if (m00 > m11 && m00 > m22) {
    const float scale =
        2.0f * std::sqrt((std::max)(1.0f + m00 - m11 - m22, 0.0f));
    if (scale < 1.0e-6f)
      return result;
    result = {0.25f * scale, (m01 + m10) / scale,
              (m02 + m20) / scale, (m21 - m12) / scale};
  } else if (m11 > m22) {
    const float scale =
        2.0f * std::sqrt((std::max)(1.0f + m11 - m00 - m22, 0.0f));
    if (scale < 1.0e-6f)
      return result;
    result = {(m01 + m10) / scale, 0.25f * scale,
              (m12 + m21) / scale, (m02 - m20) / scale};
  } else {
    const float scale =
        2.0f * std::sqrt((std::max)(1.0f + m22 - m00 - m11, 0.0f));
    if (scale < 1.0e-6f)
      return result;
    result = {(m02 + m20) / scale, (m12 + m21) / scale,
              0.25f * scale, (m10 - m01) / scale};
  }
  return DirectVmdNormalizeQuaternion(result);
}

static inline bool DirectVmdBuildOrthonormalFrame(
    VmdVec3 forwardHint, VmdVec3 lateralHint,
    DirectVmdOrthonormalFrame *frame) {
  if (!frame)
    return false;
  *frame = DirectVmdOrthonormalFrame();

  VmdVec3 forward = {};
  if (!DirectVmdTryNormalizeVector(forwardHint, &forward))
    return false;
  const VmdVec3 lateralProjection = DirectVmdSub(
      lateralHint,
      DirectVmdScale(forward, DirectVmdDot(lateralHint, forward)));
  VmdVec3 lateral = {};
  if (!DirectVmdTryNormalizeVector(lateralProjection, &lateral))
    return false;
  VmdVec3 normal = {};
  if (!DirectVmdTryNormalizeVector(DirectVmdCross(forward, lateral),
                                   &normal))
    return false;
  if (!DirectVmdTryNormalizeVector(DirectVmdCross(normal, forward),
                                   &lateral))
    return false;

  frame->forward = forward;
  frame->lateral = lateral;
  frame->normal = normal;
  frame->rotation = DirectVmdQuaternionFromOrthonormalBasis(
      lateral, normal, forward);
  return true;
}

static inline bool DirectVmdBuildFrameAlignment(
    VmdVec3 sourceForward, VmdVec3 sourceLateral,
    VmdVec3 targetForward, VmdVec3 targetLateral,
    VmdQuaternion *sourceToTargetAlignment,
    DirectVmdOrthonormalFrame *sourceFrame = nullptr,
    DirectVmdOrthonormalFrame *targetFrame = nullptr) {
  if (!sourceToTargetAlignment)
    return false;
  *sourceToTargetAlignment = {0.0f, 0.0f, 0.0f, 1.0f};
  DirectVmdOrthonormalFrame source;
  DirectVmdOrthonormalFrame target;
  if (!DirectVmdBuildOrthonormalFrame(sourceForward, sourceLateral,
                                      &source) ||
      !DirectVmdBuildOrthonormalFrame(targetForward, targetLateral,
                                      &target))
    return false;
  *sourceToTargetAlignment = DirectVmdQuaternionMultiply(
      target.rotation, DirectVmdQuaternionInverse(source.rotation));
  if (sourceFrame)
    *sourceFrame = source;
  if (targetFrame)
    *targetFrame = target;
  return true;
}

struct SourceToGameBasis {
  static constexpr VmdQuaternion Rotation() {
    return {0.0f, 1.0f, 0.0f, 0.0f};
  }

  static VmdVec3 ConvertPosition(VmdVec3 source) {
    return DirectVmdRotateVector(Rotation(), source);
  }

  static VmdQuaternion ConvertRotation(VmdQuaternion source) {
    const VmdQuaternion basis = Rotation();
    return DirectVmdQuaternionMultiply(
        DirectVmdQuaternionMultiply(basis, source),
        DirectVmdQuaternionInverse(basis));
  }
};

static inline bool DirectVmdGetCanonicalSourceChildDirection(
    DirectVmdBoneId id, VmdVec3 *direction) {
  if (!direction)
    return false;
  switch (id) {
  case DirectVmdBoneId::UpperBody:
  case DirectVmdBoneId::UpperBody2:
    *direction = {0.0000f, 0.9990f, 0.0440f};
    return true;
  case DirectVmdBoneId::Neck:
    *direction = {0.0000f, 1.0000f, -0.0099f};
    return true;
  case DirectVmdBoneId::LeftShoulder:
    *direction = {0.9687f, -0.2480f, 0.0138f};
    return true;
  case DirectVmdBoneId::LeftArm:
    *direction = {0.7941f, -0.6076f, 0.0120f};
    return true;
  case DirectVmdBoneId::LeftElbow:
    *direction = {0.7962f, -0.6047f, -0.0182f};
    return true;
  case DirectVmdBoneId::LeftWrist:
    *direction = {0.79991301f, -0.59715137f, -0.05957701f};
    return true;
  case DirectVmdBoneId::RightShoulder:
    *direction = {-0.9687f, -0.2480f, 0.0138f};
    return true;
  case DirectVmdBoneId::RightArm:
    *direction = {-0.7941f, -0.6076f, 0.0120f};
    return true;
  case DirectVmdBoneId::RightElbow:
    *direction = {-0.7962f, -0.6047f, -0.0182f};
    return true;
  case DirectVmdBoneId::RightWrist:
    *direction = {-0.79991301f, -0.59715138f, -0.05957690f};
    return true;
  default:
    *direction = {0.0f, 0.0f, 0.0f};
    return false;
  }
}

static inline bool DirectVmdGetCanonicalSourceWristFrameHints(
    DirectVmdBoneId id, VmdVec3 *forward, VmdVec3 *lateral) {
  if (!forward || !lateral)
    return false;
  switch (id) {
  case DirectVmdBoneId::LeftWrist:
    *forward = {0.7215610f, -0.5386600f, -0.0537414f};
    *lateral = {0.1079320f, -0.0332500f, -0.5565567f};
    return true;
  case DirectVmdBoneId::RightWrist:
    *forward = {-0.7215610f, -0.5386600f, -0.0537413f};
    *lateral = {-0.1079320f, -0.0332500f, -0.5565567f};
    return true;
  default:
    *forward = {0.0f, 0.0f, 0.0f};
    *lateral = {0.0f, 0.0f, 0.0f};
    return false;
  }
}

static inline bool DirectVmdGetSemanticDirectionChild(
    DirectVmdBoneId id, DirectVmdBoneId *child) {
  if (!child)
    return false;
  switch (id) {
  case DirectVmdBoneId::UpperBody:
    *child = DirectVmdBoneId::UpperBody2;
    return true;
  case DirectVmdBoneId::UpperBody2:
    *child = DirectVmdBoneId::Neck;
    return true;
  case DirectVmdBoneId::Neck:
    *child = DirectVmdBoneId::Head;
    return true;
  case DirectVmdBoneId::LeftShoulder:
    *child = DirectVmdBoneId::LeftArm;
    return true;
  case DirectVmdBoneId::LeftArm:
    *child = DirectVmdBoneId::LeftElbow;
    return true;
  case DirectVmdBoneId::LeftElbow:
    *child = DirectVmdBoneId::LeftWrist;
    return true;
  case DirectVmdBoneId::RightShoulder:
    *child = DirectVmdBoneId::RightArm;
    return true;
  case DirectVmdBoneId::RightArm:
    *child = DirectVmdBoneId::RightElbow;
    return true;
  case DirectVmdBoneId::RightElbow:
    *child = DirectVmdBoneId::RightWrist;
    return true;
  default:
    return false;
  }
}

struct DirectVmdBindNodePod {
  VmdVec3 localPosition;
  VmdQuaternion localRotation;
  VmdVec3 worldPosition;
  VmdQuaternion worldRotation;
};

struct DirectVmdLocalPosePod {
  VmdVec3 position;
  VmdQuaternion rotation;
};

struct DirectVmdWorldPosePod {
  VmdVec3 position;
  VmdQuaternion rotation;
};

static inline DirectVmdLocalPosePod DirectVmdEvaluateLocalPose(
    const DirectVmdBindNodePod &bind,
    VmdQuaternion parentBindWorldRotation,
    const DirectVmdBoneSamplePod *sample, float motionScale) {
  DirectVmdLocalPosePod result = {bind.localPosition,
                                  bind.localRotation};
  if (!sample || !sample->hasTrack)
    return result;

  const VmdQuaternion parentInverse =
      DirectVmdQuaternionInverse(parentBindWorldRotation);
  const VmdVec3 rootOffset = DirectVmdScale(
      SourceToGameBasis::ConvertPosition(sample->position), motionScale);
  const VmdVec3 parentOffset =
      DirectVmdRotateVector(parentInverse, rootOffset);
  result.position = DirectVmdAdd(bind.localPosition, parentOffset);

  const VmdQuaternion rootDelta =
      SourceToGameBasis::ConvertRotation(sample->rotation);
  const VmdQuaternion parentDelta = DirectVmdQuaternionMultiply(
      DirectVmdQuaternionMultiply(parentInverse, rootDelta),
      parentBindWorldRotation);
  result.rotation = DirectVmdQuaternionMultiply(parentDelta,
                                                 bind.localRotation);
  return result;
}

static inline DirectVmdWorldPosePod DirectVmdComposeWorldPose(
    const DirectVmdWorldPosePod &parent,
    const DirectVmdLocalPosePod &local) {
  return {
      DirectVmdAdd(parent.position,
                   DirectVmdRotateVector(parent.rotation, local.position)),
      DirectVmdQuaternionMultiply(parent.rotation, local.rotation)};
}

static inline DirectVmdWorldPosePod DirectVmdInverseWorldPose(
    const DirectVmdWorldPosePod &pose) {
  const VmdQuaternion inverseRotation =
      DirectVmdQuaternionInverse(pose.rotation);
  return {DirectVmdRotateVector(
              inverseRotation,
              {-pose.position.x, -pose.position.y, -pose.position.z}),
          inverseRotation};
}

static inline DirectVmdWorldPosePod DirectVmdWorldPoseRelativeTo(
    const DirectVmdWorldPosePod &parent,
    const DirectVmdWorldPosePod &world) {
  const VmdQuaternion parentInverse =
      DirectVmdQuaternionInverse(parent.rotation);
  return {DirectVmdRotateVector(
              parentInverse,
              DirectVmdSub(world.position, parent.position)),
          DirectVmdQuaternionMultiply(parentInverse, world.rotation)};
}

static inline DirectVmdWorldPosePod DirectVmdEvaluateTargetRootFromGhostControl(
    const DirectVmdWorldPosePod &playbackAnchor,
    const DirectVmdWorldPosePod &controlBindOwner,
    const DirectVmdWorldPosePod &ghostControlWorld) {
  const DirectVmdWorldPosePod currentControlOwner =
      DirectVmdWorldPoseRelativeTo(playbackAnchor, ghostControlWorld);
  const DirectVmdWorldPosePod inverseBind =
      DirectVmdInverseWorldPose(controlBindOwner);
  const DirectVmdWorldPosePod controlDeltaOwner =
      DirectVmdComposeWorldPose(
          currentControlOwner,
          {inverseBind.position, inverseBind.rotation});
  return DirectVmdComposeWorldPose(
      playbackAnchor,
      {controlDeltaOwner.position, controlDeltaOwner.rotation});
}

static inline VmdVec3 DirectVmdBuildWorldTranslationPlacementOffset(
    const DirectVmdWorldPosePod &entryTargetRoot,
    const DirectVmdWorldPosePod &entryEvaluatedRoot) {
  return DirectVmdSub(entryTargetRoot.position,
                      entryEvaluatedRoot.position);
}

static inline DirectVmdWorldPosePod
DirectVmdApplyWorldTranslationPlacement(
    const DirectVmdWorldPosePod &evaluated,
    const VmdVec3 &worldTranslationOffset) {
  return {DirectVmdAdd(evaluated.position, worldTranslationOffset),
          evaluated.rotation};
}

static inline VmdQuaternion DirectVmdRetargetWorldRotation(
    VmdQuaternion ghostWorldRotation,
    VmdQuaternion ghostBindWorldRotation,
    VmdQuaternion targetBindWorldRotation) {
  const VmdQuaternion worldDelta = DirectVmdQuaternionMultiply(
      DirectVmdNormalizeQuaternion(ghostWorldRotation),
      DirectVmdQuaternionInverse(ghostBindWorldRotation));
  return DirectVmdQuaternionMultiply(
      worldDelta, DirectVmdNormalizeQuaternion(targetBindWorldRotation));
}

static inline VmdQuaternion DirectVmdRetargetLocalRotation(
    VmdQuaternion ghostLocalRotation,
    VmdQuaternion ghostBindLocalRotation,
    VmdQuaternion targetBindLocalRotation) {
  const VmdQuaternion localDelta = DirectVmdQuaternionMultiply(
      DirectVmdNormalizeQuaternion(ghostLocalRotation),
      DirectVmdQuaternionInverse(ghostBindLocalRotation));
  return DirectVmdQuaternionMultiply(
      localDelta, DirectVmdNormalizeQuaternion(targetBindLocalRotation));
}

static inline VmdQuaternion DirectVmdRetargetWorldRotationFromSourceStance(
    VmdQuaternion ghostWorldRotation,
    VmdQuaternion ghostBindWorldRotation,
    VmdQuaternion targetBindWorldRotation,
    VmdQuaternion sourceToTargetWorldAlignment) {
  const VmdQuaternion worldDelta = DirectVmdQuaternionMultiply(
      DirectVmdNormalizeQuaternion(ghostWorldRotation),
      DirectVmdQuaternionInverse(ghostBindWorldRotation));
  return DirectVmdQuaternionMultiply(
      DirectVmdQuaternionMultiply(
          worldDelta,
          DirectVmdQuaternionInverse(sourceToTargetWorldAlignment)),
      DirectVmdNormalizeQuaternion(targetBindWorldRotation));
}

static inline bool DirectVmdSampleBoneWithAlias(
    const VmdFile &clip, const DirectVmdBoneSpec &spec, double frame,
    VmdBoneSample *sample) {
  if (SampleVmdBone(clip, spec.name, frame, sample))
    return true;
  return spec.alternateName &&
         SampleVmdBone(clip, spec.alternateName, frame, sample);
}

static inline bool DirectVmdSampleIkWithAlias(
    const VmdFile &clip, const DirectVmdBoneSpec &spec, double frame) {
  const auto primary = clip.ikTimelines.find(spec.name);
  if (primary != clip.ikTimelines.end())
    return SampleVmdIkEnabled(clip, spec.name, frame);
  if (spec.alternateName) {
    const auto alternate = clip.ikTimelines.find(spec.alternateName);
    if (alternate != clip.ikTimelines.end())
      return SampleVmdIkEnabled(clip, spec.alternateName, frame);
  }
  return true;
}

static inline bool DirectVmdFindMorphWeight(
    const DirectVmdSampleFrame &frame, const char *name, float *weight) {
  if (!name || !weight)
    return false;
  *weight = 0.0f;
  const uint32_t count =
      (std::min)(frame.morphCount, DIRECT_VMD_MAX_MORPH_CHANNELS);
  for (uint32_t index = 0; index < count; ++index) {
    if (std::strcmp(frame.morphs[index].name, name) == 0) {
      *weight = DirectVmdFinite(frame.morphs[index].weight)
                    ? frame.morphs[index].weight
                    : 0.0f;
      return true;
    }
  }
  return false;
}

static inline bool DirectVmdSampleCameraChannel(
    const VmdFile &clip, double frame, bool fromOverride,
    DirectVmdCameraSamplePod *output) {
  if (!output)
    return false;
  *output = DirectVmdCameraSamplePod();
  VmdCameraSample sample;
  if (!SampleVmdCamera(clip, frame, &sample) || !sample.valid)
    return false;
  output->interest = sample.interest;
  output->rotationEuler = sample.rotation;
  output->distance = sample.distance;
  output->fov = sample.fov;
  output->perspective = sample.perspective;
  output->fromOverride = fromOverride ? 1 : 0;
  output->valid = 1;
  return true;
}

static inline void DirectVmdSampleMorphChannels(
    const VmdFile &clip, double frame, DirectVmdSampleFrame *output) {
  if (!output)
    return;
  output->morphCount = 0;
  output->morphDroppedCount = 0;
  const float sampleFrame =
      std::isfinite(frame) ? static_cast<float>(frame) : 0.0f;
  for (const auto &entry : clip.morphTimelines) {
    if (output->morphCount >= DIRECT_VMD_MAX_MORPH_CHANNELS) {
      ++output->morphDroppedCount;
      continue;
    }
    DirectVmdMorphSamplePod &target =
        output->morphs[output->morphCount++];
    strncpy_s(target.name, sizeof(target.name), entry.first.c_str(),
              _TRUNCATE);
    const float sampled = entry.second.Sample(sampleFrame);
    target.weight = DirectVmdFinite(sampled) ? sampled : 0.0f;
  }
}

static inline void DirectVmdSampleClip(
    const VmdFile &clip, double frame, uint64_t sequence,
    uint64_t rigGeneration, uint64_t clipGeneration,
    uintptr_t ownerCharacter, DirectVmdPlaybackState playback,
    DirectVmdSampleFrame *output) {
  if (!output)
    return;
  *output = DirectVmdSampleFrame();
  output->sequence = sequence;
  output->rigGeneration = rigGeneration;
  output->clipGeneration = clipGeneration;
  output->ownerCharacter = ownerCharacter;
  output->sourceFrame = std::isfinite(frame) ? frame : 0.0;
  output->durationFrames = static_cast<double>(clip.totalFrames);
  output->playback = playback;
  output->modelVisible = SampleVmdModelVisible(clip, output->sourceFrame) ? 1 : 0;

  for (uint32_t index = 0; index < DIRECT_VMD_BONE_COUNT; ++index) {
    VmdBoneSample source;
    if (!DirectVmdSampleBoneWithAlias(
            clip, kDirectVmdBoneSpecs[index], output->sourceFrame, &source))
      continue;
    DirectVmdBoneSamplePod &target = output->bones[index];
    target.position = source.position;
    target.rotation = DirectVmdNormalizeQuaternion(source.rotation);
    target.hasTrack = 1;
  }

  DirectVmdSampleMorphChannels(clip, output->sourceFrame, output);
  DirectVmdSampleCameraChannel(clip, output->sourceFrame, false,
                               &output->camera);

  output->leftFootIkEnabled =
      DirectVmdSampleIkWithAlias(
          clip,
          kDirectVmdBoneSpecs[DirectVmdBoneIndex(
              DirectVmdBoneId::LeftFootIk)],
          output->sourceFrame)
          ? 1
          : 0;
  output->rightFootIkEnabled =
      DirectVmdSampleIkWithAlias(
          clip,
          kDirectVmdBoneSpecs[DirectVmdBoneIndex(
              DirectVmdBoneId::RightFootIk)],
          output->sourceFrame)
          ? 1
          : 0;
  output->valid = clip.loaded ? 1 : 0;
}

struct DirectVmdClock {
  double frame = 0.0;
  double durationFrames = 0.0;
  double speed = 1.0;
  uint64_t loopCycle = 0;
  bool loop = false;
  DirectVmdPlaybackState state = DirectVmdPlaybackState::Stopped;

  void Reset(double duration) {
    frame = 0.0;
    loopCycle = 0;
    durationFrames =
        std::isfinite(duration) && duration > 0.0 ? duration : 0.0;
    state = DirectVmdPlaybackState::Stopped;
  }

  void Play() {
    if (state == DirectVmdPlaybackState::Ended) {
      frame = 0.0;
      loopCycle = 0;
    }
    state = DirectVmdPlaybackState::Playing;
  }

  void Pause() {
    if (state == DirectVmdPlaybackState::Playing)
      state = DirectVmdPlaybackState::Paused;
  }

  void Stop() {
    frame = 0.0;
    loopCycle = 0;
    state = DirectVmdPlaybackState::Stopped;
  }

  void Seek(double targetFrame) {
    if (!std::isfinite(targetFrame))
      return;
    frame = (std::max)(0.0, (std::min)(targetFrame, durationFrames));
  }

  double AdvanceSeconds(double elapsedSeconds) {
    if (state != DirectVmdPlaybackState::Playing ||
        !std::isfinite(elapsedSeconds) || elapsedSeconds <= 0.0)
      return frame;
    const double safeSpeed =
        std::isfinite(speed) && speed > 0.0 ? speed : 1.0;
    frame += elapsedSeconds * kVmdFramesPerSecond * safeSpeed;
    if (durationFrames <= 0.0) {
      frame = 0.0;
      state = DirectVmdPlaybackState::Ended;
    } else if (frame >= durationFrames) {
      if (loop) {
        const double completed = std::floor(frame / durationFrames);
        if (completed >= 1.0)
          loopCycle += static_cast<uint64_t>(completed);
        frame = std::fmod(frame, durationFrames);
      } else {
        frame = durationFrames;
        state = DirectVmdPlaybackState::Ended;
      }
    }
    return frame;
  }
};


static constexpr uint32_t DIRECT_VMD_TERRAIN_FOOT_COUNT = 2;
static constexpr uint32_t DIRECT_VMD_TERRAIN_PROBE_COUNT = 5;

enum class DirectVmdTerrainContactState : uint8_t {
  Airborne = 0,
  Candidate,
  Contact,
};

enum class DirectVmdTerrainSupportState : uint8_t {
  None = 0,
  Left,
  Right,
  Double,
};

static inline const char *DirectVmdTerrainContactStateName(
    DirectVmdTerrainContactState state) {
  switch (state) {
  case DirectVmdTerrainContactState::Candidate: return "candidate";
  case DirectVmdTerrainContactState::Contact: return "contact";
  default: return "airborne";
  }
}

static inline const char *DirectVmdTerrainSupportStateName(
    DirectVmdTerrainSupportState state) {
  switch (state) {
  case DirectVmdTerrainSupportState::Left: return "left";
  case DirectVmdTerrainSupportState::Right: return "right";
  case DirectVmdTerrainSupportState::Double: return "double";
  default: return "none";
  }
}

struct DirectVmdTerrainProbeHit {
  VmdVec3 point = {0.0f, 0.0f, 0.0f};
  VmdVec3 normal = {0.0f, 1.0f, 0.0f};
  uint8_t hit = 0;
  uint8_t reserved[3] = {};
  VmdVec3 query = {0.0f, 0.0f, 0.0f};
  float floorDistance = 0.0f;
};

struct DirectVmdTerrainPlane {
  VmdVec3 point = {0.0f, 0.0f, 0.0f};
  VmdVec3 centerPoint = {0.0f, 0.0f, 0.0f};
  VmdVec3 normal = {0.0f, 1.0f, 0.0f};
  float height = 0.0f;
  float centerHorizontalError = 0.0f;
  float centerFloorDistance = 0.0f;
  uint8_t valid = 0;
  uint8_t hitCount = 0;
  uint8_t clusterCount = 0;
  uint8_t normalFromPointFit = 0;
  uint8_t centerHit = 0;
  uint8_t centerAnchored = 0;
  uint8_t reserved[2] = {};
};

struct DirectVmdDirectionalTerrainClearance {
  bool valid = false;
  uint8_t probeIndex = 0;
  uint8_t reserved[2] = {};
  float height = 0.0f;
  float forwardProjection = 0.0f;
  float motionDistance = 0.0f;
};

struct DirectVmdUpwardTerrainClearanceOutput {
  bool valid = false;
  bool applied = false;
  float platformY = 0.0f;
  float constraintY = 0.0f;
  float targetY = 0.0f;
  float correction = 0.0f;
};

struct DirectVmdGroundedSupportTarget {
  bool valid = false;
  float targetY = 0.0f;
};

struct DirectVmdRootReachCompensation {
  bool valid = false;
  bool needed = false;
  float correctionY = 0.0f;
  float horizontalDistance = 0.0f;
  float downwardDistance = 0.0f;
  float maximumVerticalDistance = 0.0f;
  float residual = 0.0f;
};

struct DirectVmdRootReachReleaseState {
  bool pending = false;
  float pendingOffset = 0.0f;
  float pendingSeconds = 0.0f;
};

struct DirectVmdRootReachTargetSelection {
  bool sampleValid = false;
  bool releaseHeld = false;
  bool releasePending = false;
  bool releaseConfirmed = false;
  bool clamped = false;
  float sampledOffset = 0.0f;
  float targetOffset = 0.0f;
  float pendingOffset = 0.0f;
  float pendingSeconds = 0.0f;
};

struct DirectVmdTerrainConfig {
  float contactEnterHeight = 0.035f;
  float contactExitHeight = 0.050f;
  float penetrationEnterHeight = 0.045f;
  float penetrationExitHeight = 0.100f;
  float contactEnterVelocity = 0.140f;
  float contactExitUpVelocity = 0.250f;
  float contactEnterSeconds = 0.050f;
  float contactExitSeconds = 0.035f;
  float hitLossHoldSeconds = 0.150f;
  float supportSwitchSeconds = 0.070f;
  float planeClusterHeight = 0.030f;
  float footHeightTimeConstant = 0.035f;
  float footNormalTimeConstant = 0.080f;
  float rootHeightTimeConstant = 0.085f;
  float maximumFootHeightSpeed = 2.50f;
  float maximumRootHeightSpeed = 2.00f;
  float minimumNormalY = 0.20f;
  float grounderContinuousHeightBand = 0.030f;
  float grounderRiseConfirmSeconds = 0.040f;
  float grounderDropConfirmSeconds = 0.015f;
  float grounderRawHeightTimeConstant = 0.012f;
  float grounderFootHeightTimeConstant = 0.020f;
  float grounderRootHeightTimeConstant = 0.100f;
  float grounderMaximumRawHeightSpeed = 8.00f;
  float grounderMaximumFootHeightSpeed = 5.00f;
  float grounderMaximumRootHeightSpeed = 1.50f;
  float grounderRootReachReleaseSeconds = 0.060f;
  float grounderRootReachReleaseDeadband = 0.010f;
  float grounderContactEdgeReleaseDistance = 0.032f;
};

static inline float DirectVmdTerrainClamp(float value, float minimum,
                                          float maximum) {
  return (std::max)(minimum, (std::min)(value, maximum));
}

static inline DirectVmdTerrainConfig DirectVmdBuildTerrainConfig(
    float legLength) {
  const float scale = DirectVmdFinite(legLength) && legLength > 0.05f
                          ? legLength
                          : 0.80f;
  DirectVmdTerrainConfig result;
  result.contactEnterHeight =
      DirectVmdTerrainClamp(scale * 0.040f, 0.025f, 0.055f);
  result.contactExitHeight =
      DirectVmdTerrainClamp(scale * 0.060f, 0.040f, 0.080f);
  result.penetrationEnterHeight =
      DirectVmdTerrainClamp(scale * 0.055f, 0.035f, 0.070f);
  result.penetrationExitHeight =
      DirectVmdTerrainClamp(scale * 0.140f, 0.080f, 0.160f);
  result.contactEnterVelocity =
      DirectVmdTerrainClamp(scale * 0.180f, 0.100f, 0.260f);
  result.contactExitUpVelocity =
      DirectVmdTerrainClamp(scale * 0.320f, 0.180f, 0.400f);
  result.planeClusterHeight =
      DirectVmdTerrainClamp(scale * 0.070f, 0.040f, 0.080f);
  result.grounderContinuousHeightBand =
      DirectVmdTerrainClamp(scale * 0.040f, 0.025f, 0.050f);
  result.grounderContactEdgeReleaseDistance =
      DirectVmdTerrainClamp(scale * 0.040f, 0.020f, 0.045f);
  return result;
}

static inline DirectVmdTerrainPlane DirectVmdAggregateTerrainPlane(
    const DirectVmdTerrainProbeHit *samples, uint32_t sampleCount,
    float clusterHeight, bool previousHeightValid = false,
    float previousHeight = 0.0f, float minimumNormalY = 0.20f) {
  DirectVmdTerrainPlane result;
  if (!samples || sampleCount == 0 || !DirectVmdFinite(clusterHeight) ||
      clusterHeight <= 0.0f)
    return result;
  sampleCount = (std::min)(sampleCount, DIRECT_VMD_TERRAIN_PROBE_COUNT);

  uint32_t validIndices[DIRECT_VMD_TERRAIN_PROBE_COUNT] = {};
  bool validSample[DIRECT_VMD_TERRAIN_PROBE_COUNT] = {};
  uint32_t validCount = 0;
  for (uint32_t index = 0; index < sampleCount; ++index) {
    const DirectVmdTerrainProbeHit &sample = samples[index];
    if (!sample.hit || !DirectVmdFinite(sample.point.x) ||
        !DirectVmdFinite(sample.point.y) ||
        !DirectVmdFinite(sample.point.z) ||
        !DirectVmdFinite(sample.normal.x) ||
        !DirectVmdFinite(sample.normal.y) ||
        !DirectVmdFinite(sample.normal.z))
      continue;
    VmdVec3 normal = {};
    if (!DirectVmdTryNormalizeVector(sample.normal, &normal))
      continue;
    if (normal.y < 0.0f)
      normal = DirectVmdScale(normal, -1.0f);
    if (normal.y < minimumNormalY)
      continue;
    validIndices[validCount] = index;
    validSample[index] = true;
    ++validCount;
  }
  result.hitCount = static_cast<uint8_t>(validCount);
  result.centerHit = validSample[0] ? 1 : 0;
  if (validSample[0]) {
    result.centerPoint = samples[0].point;
    result.centerFloorDistance = samples[0].floorDistance;
    const float dx = samples[0].point.x - samples[0].query.x;
    const float dz = samples[0].point.z - samples[0].query.z;
    result.centerHorizontalError = std::sqrt(dx * dx + dz * dz);
  }
  if (validCount == 0)
    return result;

  uint32_t bestMembers[DIRECT_VMD_TERRAIN_PROBE_COUNT] = {};
  uint32_t bestCount = 0;
  float bestContinuity = std::numeric_limits<float>::infinity();
  float bestCenterDistance = std::numeric_limits<float>::infinity();
  if (validSample[0]) {
    const float centerHeight = samples[0].point.y;
    for (uint32_t index = 0; index < validCount; ++index) {
      const float height = samples[validIndices[index]].point.y;
      if (std::fabs(height - centerHeight) <= clusterHeight) {
        bestMembers[bestCount++] = index;
      }
    }
    result.centerAnchored = bestCount ? 1 : 0;
  } else {
    for (uint32_t candidate = 0; candidate < validCount; ++candidate) {
      const float candidateHeight =
          samples[validIndices[candidate]].point.y;
      uint32_t members[DIRECT_VMD_TERRAIN_PROBE_COUNT] = {};
      uint32_t count = 0;
      float sumHeight = 0.0f;
      for (uint32_t index = 0; index < validCount; ++index) {
        const float height = samples[validIndices[index]].point.y;
        if (std::fabs(height - candidateHeight) <= clusterHeight) {
          members[count++] = index;
          sumHeight += height;
        }
      }
      const float meanHeight = count ? sumHeight / count
                                     : candidateHeight;
      const float continuity = previousHeightValid
                                   ? std::fabs(meanHeight - previousHeight)
                                   : 0.0f;
      const float centerDistance = previousHeightValid
          ? continuity
          : static_cast<float>(candidate);
      const bool better =
          count > bestCount ||
          (count == bestCount &&
           (continuity + 1.0e-6f < bestContinuity ||
            (std::fabs(continuity - bestContinuity) <= 1.0e-6f &&
             centerDistance < bestCenterDistance)));
      if (!better)
        continue;
      bestCount = count;
      bestContinuity = continuity;
      bestCenterDistance = centerDistance;
      for (uint32_t index = 0; index < count; ++index)
        bestMembers[index] = members[index];
    }
  }
  if (bestCount == 0)
    return result;

  float heights[DIRECT_VMD_TERRAIN_PROBE_COUNT] = {};
  VmdVec3 pointSum = {};
  for (uint32_t member = 0; member < bestCount; ++member) {
    const uint32_t validIndex = bestMembers[member];
    const DirectVmdTerrainProbeHit &sample =
        samples[validIndices[validIndex]];
    heights[member] = sample.point.y;
    pointSum = DirectVmdAdd(pointSum, sample.point);
  }
  std::sort(heights, heights + bestCount);
  const float median = bestCount & 1
                           ? heights[bestCount / 2]
                           : 0.5f * (heights[bestCount / 2 - 1] +
                                     heights[bestCount / 2]);
  result.point = DirectVmdScale(pointSum, 1.0f / bestCount);
  result.point.y = median;

  VmdVec3 fittedNormal = {0.0f, 1.0f, 0.0f};
  bool normalFromPointFit = false;
  if (bestCount >= 3) {
    const double meanX = static_cast<double>(pointSum.x) / bestCount;
    const double meanY = static_cast<double>(pointSum.y) / bestCount;
    const double meanZ = static_cast<double>(pointSum.z) / bestCount;
    double xx = 0.0;
    double xz = 0.0;
    double zz = 0.0;
    double xy = 0.0;
    double zy = 0.0;
    for (uint32_t member = 0; member < bestCount; ++member) {
      const DirectVmdTerrainProbeHit &sample =
          samples[validIndices[bestMembers[member]]];
      const double dx = static_cast<double>(sample.point.x) - meanX;
      const double dy = static_cast<double>(sample.point.y) - meanY;
      const double dz = static_cast<double>(sample.point.z) - meanZ;
      xx += dx * dx;
      xz += dx * dz;
      zz += dz * dz;
      xy += dx * dy;
      zy += dz * dy;
    }
    const double determinant = xx * zz - xz * xz;
    const double horizontalVariance = xx + zz;
    const double determinantThreshold = (std::max)(
        1.0e-14, horizontalVariance * horizontalVariance * 1.0e-6);
    if (determinant > determinantThreshold) {
      const double slopeX = (xy * zz - zy * xz) / determinant;
      const double slopeZ = (zy * xx - xy * xz) / determinant;
      const VmdVec3 candidate = {
          static_cast<float>(-slopeX), 1.0f,
          static_cast<float>(-slopeZ)};
      VmdVec3 unit = {};
      if (DirectVmdTryNormalizeVector(candidate, &unit) &&
          unit.y >= minimumNormalY) {
        fittedNormal = unit;
        normalFromPointFit = true;
      }
    }
  }
  result.normal = fittedNormal;
  result.height = median;
  result.clusterCount = static_cast<uint8_t>(bestCount);
  result.normalFromPointFit = normalFromPointFit ? 1 : 0;
  result.valid = 1;
  return result;
}

static inline DirectVmdDirectionalTerrainClearance
DirectVmdSelectDirectionalTerrainClearance(
    const DirectVmdTerrainProbeHit *samples, uint32_t sampleCount,
    const DirectVmdTerrainPlane &supportPlane,
    const VmdVec3 &horizontalMotion, float minimumMotionDistance,
    float heightSeparation, float minimumNormalY = 0.20f) {
  DirectVmdDirectionalTerrainClearance result;
  if (!samples || sampleCount < 2 || !supportPlane.valid ||
      !DirectVmdFinite(horizontalMotion.x) ||
      !DirectVmdFinite(horizontalMotion.z) ||
      !DirectVmdFinite(minimumMotionDistance) ||
      minimumMotionDistance < 0.0f ||
      !DirectVmdFinite(heightSeparation) || heightSeparation <= 0.0f ||
      !DirectVmdFinite(minimumNormalY))
    return result;
  sampleCount = (std::min)(sampleCount, DIRECT_VMD_TERRAIN_PROBE_COUNT);
  const float motionDistance = std::sqrt(
      horizontalMotion.x * horizontalMotion.x +
      horizontalMotion.z * horizontalMotion.z);
  result.motionDistance = motionDistance;
  if (!DirectVmdFinite(motionDistance) ||
      motionDistance < (std::max)(minimumMotionDistance, 1.0e-6f) ||
      !DirectVmdFinite(samples[0].query.x) ||
      !DirectVmdFinite(samples[0].query.z))
    return result;

  const float directionX = horizontalMotion.x / motionDistance;
  const float directionZ = horizontalMotion.z / motionDistance;
  float bestProjection = 0.0f;
  float bestHeight = -std::numeric_limits<float>::infinity();
  uint32_t bestProbe = 0;
  for (uint32_t index = 1; index < sampleCount; ++index) {
    const DirectVmdTerrainProbeHit &sample = samples[index];
    if (!sample.hit || !DirectVmdFinite(sample.point.y) ||
        !DirectVmdFinite(sample.normal.x) ||
        !DirectVmdFinite(sample.normal.y) ||
        !DirectVmdFinite(sample.normal.z) ||
        !DirectVmdFinite(sample.query.x) ||
        !DirectVmdFinite(sample.query.z))
      continue;
    VmdVec3 normal = {};
    if (!DirectVmdTryNormalizeVector(sample.normal, &normal))
      continue;
    if (normal.y < 0.0f)
      normal = DirectVmdScale(normal, -1.0f);
    if (normal.y < minimumNormalY)
      continue;
    const float offsetX = sample.query.x - samples[0].query.x;
    const float offsetZ = sample.query.z - samples[0].query.z;
    const float projection = offsetX * directionX + offsetZ * directionZ;
    if (!DirectVmdFinite(projection) || projection <= 1.0e-6f)
      continue;
    const bool betterDirection = projection > bestProjection + 1.0e-5f;
    const bool equalDirection =
        std::fabs(projection - bestProjection) <= 1.0e-5f;
    if (!betterDirection && !(equalDirection && sample.point.y > bestHeight))
      continue;
    bestProjection = projection;
    bestHeight = sample.point.y;
    bestProbe = index;
  }
  if (bestProbe == 0 ||
      bestHeight <= supportPlane.height + heightSeparation)
    return result;
  result.valid = true;
  result.probeIndex = static_cast<uint8_t>(bestProbe);
  result.height = bestHeight;
  result.forwardProjection = bestProjection;
  return result;
}

static inline DirectVmdUpwardTerrainClearanceOutput
DirectVmdComputeUpwardTerrainClearance(
    float supportPlatformTargetY, float terrainGroundedAnkleY) {
  DirectVmdUpwardTerrainClearanceOutput result;
  if (!DirectVmdFinite(supportPlatformTargetY) ||
      !DirectVmdFinite(terrainGroundedAnkleY))
    return result;
  result.platformY = supportPlatformTargetY;
  result.constraintY = terrainGroundedAnkleY;
  result.targetY = (std::max)(result.platformY, result.constraintY);
  result.correction = result.targetY - result.platformY;
  result.valid = DirectVmdFinite(result.platformY) &&
      DirectVmdFinite(result.constraintY) &&
      DirectVmdFinite(result.targetY) &&
      DirectVmdFinite(result.correction);
  result.applied = result.valid && result.correction > 1.0e-5f;
  return result;
}

static inline bool DirectVmdShouldApplyUpwardTerrainClearance(
    DirectVmdTerrainContactState contact,
    const DirectVmdUpwardTerrainClearanceOutput &clearance,
    float baseTargetY) {
  return contact != DirectVmdTerrainContactState::Contact &&
      clearance.applied && DirectVmdFinite(baseTargetY) &&
      clearance.targetY > baseTargetY + 1.0e-5f;
}

static inline DirectVmdGroundedSupportTarget
DirectVmdComputeGroundedSupportTarget(
    float referenceGroundY, float rootTerrainOffset,
    float ankleClearance, float authoredLift) {
  DirectVmdGroundedSupportTarget result;
  if (!DirectVmdFinite(referenceGroundY) ||
      !DirectVmdFinite(rootTerrainOffset) ||
      !DirectVmdFinite(ankleClearance) || ankleClearance < 0.0f ||
      !DirectVmdFinite(authoredLift))
    return result;
  result.targetY = referenceGroundY + rootTerrainOffset +
      ankleClearance + (std::max)(authoredLift, 0.0f);
  result.valid = DirectVmdFinite(result.targetY);
  return result;
}

static inline DirectVmdRootReachCompensation
DirectVmdComputeDownwardRootReachCompensation(
    VmdVec3 origin, VmdVec3 plantedTarget, float maximumReach) {
  DirectVmdRootReachCompensation result;
  if (!DirectVmdFinite(origin.x) || !DirectVmdFinite(origin.y) ||
      !DirectVmdFinite(origin.z) ||
      !DirectVmdFinite(plantedTarget.x) ||
      !DirectVmdFinite(plantedTarget.y) ||
      !DirectVmdFinite(plantedTarget.z) ||
      !DirectVmdFinite(maximumReach) || maximumReach <= 0.0f)
    return result;

  const float dx = plantedTarget.x - origin.x;
  const float dz = plantedTarget.z - origin.z;
  const float horizontalSquared = dx * dx + dz * dz;
  const float reachSquared = maximumReach * maximumReach;
  if (!DirectVmdFinite(horizontalSquared) ||
      horizontalSquared >= reachSquared)
    return result;

  result.horizontalDistance = std::sqrt(
      (std::max)(horizontalSquared, 0.0f));
  result.maximumVerticalDistance = std::sqrt(
      (std::max)(reachSquared - horizontalSquared, 0.0f));
  result.downwardDistance = origin.y - plantedTarget.y;
  result.valid = DirectVmdFinite(result.horizontalDistance) &&
      DirectVmdFinite(result.maximumVerticalDistance) &&
      DirectVmdFinite(result.downwardDistance);
  if (!result.valid || result.downwardDistance <=
                           result.maximumVerticalDistance)
    return result;

  result.residual = result.downwardDistance -
      result.maximumVerticalDistance;
  result.correctionY = -result.residual;
  result.needed = DirectVmdFinite(result.residual) &&
      DirectVmdFinite(result.correctionY) &&
      result.residual > 1.0e-5f;
  if (!result.needed) {
    result.residual = 0.0f;
    result.correctionY = 0.0f;
  }
  return result;
}

static inline DirectVmdRootReachTargetSelection
DirectVmdSelectRootReachTarget(
    const bool usable[DIRECT_VMD_TERRAIN_FOOT_COUNT],
    const DirectVmdRootReachCompensation
        compensation[DIRECT_VMD_TERRAIN_FOOT_COUNT],
    DirectVmdRootReachReleaseState *releaseState,
    float previousTargetOffset, float maximumDownwardOffset,
    float releaseConfirmSeconds, float releaseDeadband,
    float deltaSeconds, bool advanceState) {
  DirectVmdRootReachTargetSelection result;
  if (!usable || !compensation || !releaseState ||
      !DirectVmdFinite(previousTargetOffset) ||
      !DirectVmdFinite(maximumDownwardOffset) ||
      maximumDownwardOffset <= 0.0f ||
      !DirectVmdFinite(releaseConfirmSeconds) ||
      !DirectVmdFinite(releaseDeadband))
    return result;

  result.targetOffset = DirectVmdTerrainClamp(
      previousTargetOffset, -maximumDownwardOffset, 0.0f);
  const float dt = advanceState && DirectVmdFinite(deltaSeconds)
      ? DirectVmdTerrainClamp(deltaSeconds, 0.0f, 0.100f)
      : 0.0f;
  const float confirmSeconds =
      (std::max)(releaseConfirmSeconds, 0.0f);
  const float deadband = (std::max)(releaseDeadband, 1.0e-5f);

  const auto publishPending = [&]() {
    result.releasePending = releaseState->pending;
    result.pendingOffset = releaseState->pendingOffset;
    result.pendingSeconds = releaseState->pendingSeconds;
  };
  const auto clearPending = [&]() {
    releaseState->pending = false;
    releaseState->pendingOffset = 0.0f;
    releaseState->pendingSeconds = 0.0f;
  };

  float sampled = 0.0f;
  for (uint32_t index = 0;
       index < DIRECT_VMD_TERRAIN_FOOT_COUNT; ++index) {
    if (!usable[index] || !compensation[index].valid)
      continue;
    result.sampleValid = true;
    sampled = (std::min)(sampled,
                         compensation[index].correctionY);
  }
  if (!result.sampleValid) {
    if (dt > 0.0f)
      clearPending();
    publishPending();
    return result;
  }

  const float limited = DirectVmdTerrainClamp(
      sampled, -maximumDownwardOffset, 0.0f);
  result.sampledOffset = sampled;
  result.clamped = std::fabs(limited - sampled) > 1.0e-5f;
  const bool needsMoreDownward =
      limited < result.targetOffset - 1.0e-5f;
  if (dt <= 0.0f) {
    result.releaseHeld =
        limited > result.targetOffset + deadband;
    publishPending();
    return result;
  }

  if (needsMoreDownward) {
    result.targetOffset = limited;
    clearPending();
  } else if (limited > result.targetOffset + deadband) {
    if (!releaseState->pending) {
      releaseState->pending = true;
      releaseState->pendingSeconds = dt;
    } else {
      releaseState->pendingSeconds += dt;
    }
    releaseState->pendingOffset = limited;
    result.releaseHeld = true;
    if (releaseState->pendingSeconds + 1.0e-6f >= confirmSeconds) {
      result.targetOffset = limited;
      result.releaseHeld = false;
      result.releaseConfirmed = true;
      result.pendingOffset = releaseState->pendingOffset;
      result.pendingSeconds = releaseState->pendingSeconds;
      clearPending();
      result.releasePending = false;
      return result;
    }
  } else {
    clearPending();
  }
  publishPending();
  return result;
}

static inline float DirectVmdTerrainFilteredStep(
    float current, float target, float timeConstant, float maximumSpeed,
    float deltaSeconds) {
  if (!DirectVmdFinite(current) || !DirectVmdFinite(target) ||
      !DirectVmdFinite(deltaSeconds) || deltaSeconds <= 0.0f)
    return current;
  const float safeTau = (std::max)(timeConstant, 1.0e-4f);
  const float alpha = 1.0f - std::exp(-deltaSeconds / safeTau);
  float step = (target - current) * alpha;
  if (DirectVmdFinite(maximumSpeed) && maximumSpeed > 0.0f) {
    const float maximumStep = maximumSpeed * deltaSeconds;
    step = DirectVmdTerrainClamp(step, -maximumStep, maximumStep);
  }
  return current + step;
}

struct DirectVmdGrounderContactEdgeState {
  bool candidateValid = false;
  bool released = false;
  bool releasedByTravel = false;
  float candidateHeight = 0.0f;
  float candidateTravel = 0.0f;
  float candidateSeconds = 0.0f;
  VmdVec3 candidateOrigin = {0.0f, 0.0f, 0.0f};
  uint32_t heldSampleCount = 0;
};

struct DirectVmdGrounderContactEdgeOutput {
  bool sampleValid = false;
  bool held = false;
  bool candidate = false;
  bool released = false;
  bool releasedByTravel = false;
  bool transitioned = false;
  float sampleHeight = 0.0f;
  float candidateHeight = 0.0f;
  float candidateTravel = 0.0f;
  float candidateSeconds = 0.0f;
  uint32_t heldSampleCount = 0;
};

static inline DirectVmdGrounderContactEdgeOutput
DirectVmdGateGrounderContactEdgeHeight(
    DirectVmdGrounderContactEdgeState *state,
    DirectVmdTerrainContactState contact, bool sampleValid,
    float sampleHeight, bool stableValid, float stableHeight,
    const VmdVec3 &authoredTarget, float heightBand,
    float releaseDistance, float deltaSeconds, bool advanceState) {
  DirectVmdGrounderContactEdgeOutput output;
  if (!state)
    return output;

  const bool finiteSample = sampleValid && DirectVmdFinite(sampleHeight);
  output.sampleValid = finiteSample;
  output.sampleHeight = finiteSample ? sampleHeight : 0.0f;
  const bool beforeHeld = state->candidateValid && !state->released;
  const auto clearCandidate = [&]() {
    state->candidateValid = false;
    state->released = false;
    state->releasedByTravel = false;
    state->candidateHeight = 0.0f;
    state->candidateTravel = 0.0f;
    state->candidateSeconds = 0.0f;
    state->candidateOrigin = VmdVec3{};
    state->heldSampleCount = 0;
  };
  const auto publishState = [&]() {
    output.candidate = state->candidateValid;
    output.released = state->released;
    output.releasedByTravel = state->releasedByTravel;
    output.candidateHeight = state->candidateHeight;
    output.candidateTravel = state->candidateTravel;
    output.candidateSeconds = state->candidateSeconds;
    output.heldSampleCount = state->heldSampleCount;
    output.held = state->candidateValid && !state->released;
    output.transitioned = beforeHeld != output.held;
  };

  const bool validInputs = stableValid && DirectVmdFinite(stableHeight) &&
      DirectVmdFinite(authoredTarget.x) &&
      DirectVmdFinite(authoredTarget.z) &&
      DirectVmdFinite(heightBand) && heightBand > 0.0f &&
      DirectVmdFinite(releaseDistance) && releaseDistance > 0.0f;
  if (contact != DirectVmdTerrainContactState::Contact ||
      !finiteSample || !validInputs) {
    if (advanceState)
      clearCandidate();
    publishState();
    return output;
  }

  if (!advanceState) {
    publishState();
    if (output.held)
      output.sampleHeight = stableHeight;
    return output;
  }

  if (std::fabs(sampleHeight - stableHeight) <= heightBand) {
    clearCandidate();
    publishState();
    return output;
  }

  if (!state->candidateValid ||
      std::fabs(sampleHeight - state->candidateHeight) > heightBand) {
    state->candidateValid = true;
    state->released = false;
    state->releasedByTravel = false;
    state->candidateHeight = sampleHeight;
    state->candidateTravel = 0.0f;
    state->candidateSeconds = DirectVmdFinite(deltaSeconds)
        ? DirectVmdTerrainClamp(deltaSeconds, 0.0f, 0.100f)
        : 0.0f;
    state->candidateOrigin = authoredTarget;
    state->heldSampleCount = 0;
  } else {
    state->candidateHeight = sampleHeight;
    if (DirectVmdFinite(deltaSeconds)) {
      state->candidateSeconds +=
          DirectVmdTerrainClamp(deltaSeconds, 0.0f, 0.100f);
    }
    const float dx = authoredTarget.x - state->candidateOrigin.x;
    const float dz = authoredTarget.z - state->candidateOrigin.z;
    state->candidateTravel = std::sqrt(
        (std::max)(dx * dx + dz * dz, 0.0f));
    if (DirectVmdFinite(state->candidateTravel) &&
        state->candidateTravel + 1.0e-6f >= releaseDistance) {
      state->released = true;
      state->releasedByTravel = true;
    }
  }

  if (!state->released) {
    output.sampleHeight = stableHeight;
    ++state->heldSampleCount;
  }
  publishState();
  return output;
}

struct DirectVmdGrounderHeightState {
  bool stableValid = false;
  bool pendingValid = false;
  float stableHeight = 0.0f;
  float pendingHeight = 0.0f;
  float pendingSeconds = 0.0f;
  uint32_t switchCount = 0;
};

struct DirectVmdGrounderHeightOutput {
  bool valid = false;
  bool pending = false;
  bool initialized = false;
  bool committed = false;
  float stableHeight = 0.0f;
  float pendingHeight = 0.0f;
  float pendingSeconds = 0.0f;
  uint32_t switchCount = 0;
};

enum class DirectVmdGrounderHeightSource : uint8_t {
  None = 0,
  FootGroundingRaycast,
  LastHitPoint,
  HeelHitPoint,
  CalculatedFoot,
  LegIkPosition,
  SolverFallback,
};

static inline const char *DirectVmdGrounderHeightSourceName(
    DirectVmdGrounderHeightSource source) {
  switch (source) {
  case DirectVmdGrounderHeightSource::FootGroundingRaycast:
    return "foot-xz-grounding-raycast";
  case DirectVmdGrounderHeightSource::LastHitPoint:
    return "last-hit-point";
  case DirectVmdGrounderHeightSource::HeelHitPoint:
    return "heel-hit-point";
  case DirectVmdGrounderHeightSource::CalculatedFoot:
    return "calculated-foot";
  case DirectVmdGrounderHeightSource::LegIkPosition:
    return "leg-ik-position";
  case DirectVmdGrounderHeightSource::SolverFallback:
    return "solver-fallback";
  default:
    return "none";
  }
}

struct DirectVmdGrounderHeightCandidates {
  bool lastHitValid = false;
  bool heelHitValid = false;
  bool calculatedFootValid = false;
  bool legIkValid = false;
  bool solverValid = false;
  float lastHitY = 0.0f;
  float heelHitY = 0.0f;
  float calculatedFootY = 0.0f;
  float legIkY = 0.0f;
  float solverY = 0.0f;
};

struct DirectVmdGrounderHeightSelection {
  bool valid = false;
  bool directHit = false;
  float ankleY = 0.0f;
  float groundY = 0.0f;
  float referenceAnkleY = 0.0f;
  float referenceError = 0.0f;
  uint32_t acceptedGroundCandidateCount = 0;
  DirectVmdGrounderHeightSource source =
      DirectVmdGrounderHeightSource::None;
};

static inline float DirectVmdGrounderEffectiveAnkleClearance(
    float bindClearance, bool additionalOffsetValid,
    float additionalOffset, float legLength) {
  if (!DirectVmdFinite(bindClearance) || bindClearance < 0.0f)
    return 0.0f;
  const float scale = DirectVmdFinite(legLength) && legLength > 0.05f
                          ? legLength
                          : 0.80f;
  const float maximum = DirectVmdTerrainClamp(
      scale * 0.50f, 0.15f, 0.50f);
  const float extra = additionalOffsetValid &&
                              DirectVmdFinite(additionalOffset)
                          ? additionalOffset
                          : 0.0f;
  return DirectVmdTerrainClamp(bindClearance + extra, 0.0f, maximum);
}

struct DirectVmdFootPhysicsHeightSelection {
  bool valid = false;
  float groundDelta = 0.0f;
  float footCorrection = 0.0f;
  float groundedAnkleY = 0.0f;
  float ankleY = 0.0f;
  float referenceError = 0.0f;
};

static inline DirectVmdFootPhysicsHeightSelection
DirectVmdSelectFootPhysicsTerrainHeight(
    float flatTargetY, float authoredLift, float groundY,
    float referenceGroundY, float ankleClearance,
    float expectedBaselineAnkleY, float maximumTreadDistance) {
  DirectVmdFootPhysicsHeightSelection output;
  if (!DirectVmdFinite(flatTargetY) || !DirectVmdFinite(authoredLift) ||
      !DirectVmdFinite(groundY) ||
      !DirectVmdFinite(referenceGroundY) ||
      !DirectVmdFinite(ankleClearance) || ankleClearance < 0.0f ||
      !DirectVmdFinite(expectedBaselineAnkleY) ||
      !DirectVmdFinite(maximumTreadDistance) ||
      maximumTreadDistance <= 0.0f)
    return output;
  const float safeLift = (std::max)(authoredLift, 0.0f);
  const float authoredBaselineY = flatTargetY - safeLift;
  output.groundDelta = groundY - referenceGroundY;
  output.groundedAnkleY = groundY + ankleClearance;
  output.footCorrection = output.groundedAnkleY - authoredBaselineY;
  output.ankleY = flatTargetY + output.footCorrection;
  output.referenceError = std::fabs(
      output.groundedAnkleY - expectedBaselineAnkleY);
  output.valid = DirectVmdFinite(output.groundDelta) &&
      DirectVmdFinite(output.footCorrection) &&
      DirectVmdFinite(output.groundedAnkleY) &&
      DirectVmdFinite(output.ankleY) &&
      DirectVmdFinite(output.referenceError) &&
      output.referenceError <= maximumTreadDistance;
  return output;
}

static inline DirectVmdGrounderHeightSelection
DirectVmdSelectGrounderHeight(
    const DirectVmdGrounderHeightCandidates &candidates,
    float flatTargetY, float ankleClearance,
    float expectedAnkleY, float maximumTreadDistance,
    float maximumCorrection) {
  DirectVmdGrounderHeightSelection output;
  if (!DirectVmdFinite(flatTargetY) ||
      !DirectVmdFinite(ankleClearance) ||
      !DirectVmdFinite(expectedAnkleY) ||
      !DirectVmdFinite(maximumTreadDistance) ||
      maximumTreadDistance <= 0.0f ||
      !DirectVmdFinite(maximumCorrection) || maximumCorrection <= 0.0f)
    return output;

  output.referenceAnkleY = expectedAnkleY;
  float bestGroundError = std::numeric_limits<float>::infinity();

  const auto considerGround = [&](bool available, float groundY,
                                  DirectVmdGrounderHeightSource source) {
    if (!available || !DirectVmdFinite(groundY))
      return false;
    const float ankleY = groundY + ankleClearance;
    if (!DirectVmdFinite(ankleY) ||
        std::fabs(ankleY - flatTargetY) > maximumCorrection)
      return false;
    const float referenceError = std::fabs(ankleY - expectedAnkleY);
    if (!DirectVmdFinite(referenceError) ||
        referenceError > maximumTreadDistance)
      return false;
    ++output.acceptedGroundCandidateCount;
    if (output.valid && referenceError >= bestGroundError - 1.0e-5f)
      return true;
    output.valid = true;
    output.directHit = true;
    output.ankleY = ankleY;
    output.groundY = groundY;
    output.referenceError = referenceError;
    output.source = source;
    bestGroundError = referenceError;
    return true;
  };
  const auto acceptAnkle = [&](bool available, float ankleY,
                               DirectVmdGrounderHeightSource source,
                               bool direct) {
    if (!available || !DirectVmdFinite(ankleY) ||
        std::fabs(ankleY - flatTargetY) > maximumCorrection ||
        std::fabs(ankleY - expectedAnkleY) > maximumTreadDistance)
      return false;
    output.valid = true;
    output.directHit = direct;
    output.ankleY = ankleY;
    output.groundY = ankleY - ankleClearance;
    output.referenceError = std::fabs(ankleY - expectedAnkleY);
    output.source = source;
    return true;
  };

  considerGround(candidates.lastHitValid, candidates.lastHitY,
                 DirectVmdGrounderHeightSource::LastHitPoint);
  considerGround(candidates.heelHitValid, candidates.heelHitY,
                 DirectVmdGrounderHeightSource::HeelHitPoint);
  if (output.valid)
    return output;

  if (acceptAnkle(candidates.calculatedFootValid,
                  candidates.calculatedFootY,
                  DirectVmdGrounderHeightSource::CalculatedFoot, true) ||
      acceptAnkle(candidates.legIkValid, candidates.legIkY,
                  DirectVmdGrounderHeightSource::LegIkPosition, true) ||
      acceptAnkle(candidates.solverValid, candidates.solverY,
                  DirectVmdGrounderHeightSource::SolverFallback, false)) {
    return output;
  }
  return output;
}

struct DirectVmdGrounderRootTarget {
  bool valid = false;
  float offset = 0.0f;
  DirectVmdTerrainSupportState support =
      DirectVmdTerrainSupportState::None;
};

static inline DirectVmdGrounderRootTarget
DirectVmdSelectGrounderRootTarget(const bool usable[2],
                                  const float correction[2]) {
  DirectVmdGrounderRootTarget output;
  const bool left = usable && usable[0] && correction &&
      DirectVmdFinite(correction[0]);
  const bool right = usable && usable[1] && correction &&
      DirectVmdFinite(correction[1]);
  if (left && right) {
    output.valid = true;
    output.offset = 0.5f * (correction[0] + correction[1]);
    output.support = DirectVmdTerrainSupportState::Double;
  } else if (left) {
    output.valid = true;
    output.offset = correction[0];
    output.support = DirectVmdTerrainSupportState::Left;
  } else if (right) {
    output.valid = true;
    output.offset = correction[1];
    output.support = DirectVmdTerrainSupportState::Right;
  }
  return output;
}

static inline bool DirectVmdGrounderTimelineNeedsReacquire(
    uint64_t previousCycle, uint64_t currentCycle, double sourceStep) {
  return previousCycle != currentCycle || !std::isfinite(sourceStep) ||
      sourceStep < -0.25;
}

static inline DirectVmdGrounderHeightOutput
DirectVmdUpdateGrounderHeight(
    DirectVmdGrounderHeightState *state,
    const DirectVmdTerrainConfig &config, bool sampleValid,
    float sampleHeight, float deltaSeconds, bool advanceState) {
  DirectVmdGrounderHeightOutput output;
  if (!state)
    return output;

  const float dt = advanceState && DirectVmdFinite(deltaSeconds)
                       ? DirectVmdTerrainClamp(deltaSeconds, 0.0f, 0.100f)
                       : 0.0f;
  const bool finiteSample = sampleValid && DirectVmdFinite(sampleHeight);
  if (dt > 0.0f) {
    if (!finiteSample) {
      state->pendingValid = false;
      state->pendingSeconds = 0.0f;
    } else if (!state->stableValid) {
      state->stableValid = true;
      state->stableHeight = sampleHeight;
      state->pendingValid = false;
      state->pendingSeconds = 0.0f;
      output.initialized = true;
    } else {
      const float band = (std::max)(
          config.grounderContinuousHeightBand, 1.0e-4f);
      const float delta = sampleHeight - state->stableHeight;
      if (std::fabs(delta) <= band) {
        state->stableHeight = DirectVmdTerrainFilteredStep(
            state->stableHeight, sampleHeight,
            config.grounderRawHeightTimeConstant,
            config.grounderMaximumRawHeightSpeed, dt);
        state->pendingValid = false;
        state->pendingSeconds = 0.0f;
      } else {
        if (!state->pendingValid ||
            std::fabs(sampleHeight - state->pendingHeight) > band) {
          state->pendingValid = true;
          state->pendingHeight = sampleHeight;
          state->pendingSeconds = dt;
        } else {
          state->pendingHeight = DirectVmdTerrainFilteredStep(
              state->pendingHeight, sampleHeight,
              config.grounderRawHeightTimeConstant,
              config.grounderMaximumRawHeightSpeed, dt);
          state->pendingSeconds += dt;
        }
        const float requiredSeconds =
            state->pendingHeight >= state->stableHeight
                ? config.grounderRiseConfirmSeconds
                : config.grounderDropConfirmSeconds;
        if (state->pendingSeconds + 1.0e-6f >=
            (std::max)(requiredSeconds, 0.0f)) {
          state->stableHeight = state->pendingHeight;
          state->pendingValid = false;
          state->pendingSeconds = 0.0f;
          ++state->switchCount;
          output.committed = true;
        }
      }
    }
  }

  output.valid = state->stableValid;
  output.pending = state->pendingValid;
  output.stableHeight = state->stableHeight;
  output.pendingHeight = state->pendingHeight;
  output.pendingSeconds = state->pendingSeconds;
  output.switchCount = state->switchCount;
  return output;
}

struct DirectVmdTerrainFootState {
  DirectVmdTerrainContactState contact =
      DirectVmdTerrainContactState::Airborne;
  bool previousFlatTargetValid = false;
  bool flatBaselineValid = false;
  bool floorReferenceValid = false;
  bool stablePlaneValid = false;
  bool contactPlaneValid = false;
  bool candidatePlaneValid = false;
  bool swingArmed = false;
  bool rawHit = false;
  bool holdingLostHit = false;
  uint8_t rawHitCount = 0;
  uint8_t rawClusterCount = 0;
  uint8_t normalFromPointFit = 0;
  uint8_t centerHit = 0;
  uint8_t centerAnchored = 0;
  uint32_t planeSwitchCount = 0;
  float previousFlatTargetY = 0.0f;
  float flatBaselineY = 0.0f;
  float floorReferenceHeight = 0.0f;
  float contactPlaneHeight = 0.0f;
  float candidatePlaneHeight = 0.0f;
  float verticalVelocity = 0.0f;
  float contactTimer = 0.0f;
  float releaseTimer = 0.0f;
  float planeSwitchTimer = 0.0f;
  float missingHitSeconds = 0.0f;
  float rawHeight = 0.0f;
  float stableHeight = 0.0f;
  float appliedOffset = 0.0f;
  float centerHorizontalError = 0.0f;
  float centerFloorDistance = 0.0f;
  VmdVec3 centerPoint = {0.0f, 0.0f, 0.0f};
  VmdVec3 stableNormal = {0.0f, 1.0f, 0.0f};
  VmdVec3 contactPlaneNormal = {0.0f, 1.0f, 0.0f};
  VmdVec3 candidatePlaneNormal = {0.0f, 1.0f, 0.0f};
};

struct DirectVmdTerrainState {
  DirectVmdTerrainFootState feet[DIRECT_VMD_TERRAIN_FOOT_COUNT];
  DirectVmdTerrainSupportState support =
      DirectVmdTerrainSupportState::None;
  DirectVmdTerrainSupportState pendingSupport =
      DirectVmdTerrainSupportState::None;
  DirectVmdTerrainSupportState rootSupport =
      DirectVmdTerrainSupportState::None;
  DirectVmdTerrainSupportState pendingRootSupport =
      DirectVmdTerrainSupportState::None;
  float pendingSupportSeconds = 0.0f;
  float pendingRootSupportSeconds = 0.0f;
  float rootOffset = 0.0f;
  float rootTargetOffset = 0.0f;
};

struct DirectVmdTerrainFrameInput {
  VmdVec3 flatFootTarget[DIRECT_VMD_TERRAIN_FOOT_COUNT] = {};
  float ankleClearance[DIRECT_VMD_TERRAIN_FOOT_COUNT] = {};
  DirectVmdTerrainPlane rawPlane[DIRECT_VMD_TERRAIN_FOOT_COUNT];
  uint8_t ikEnabled[DIRECT_VMD_TERRAIN_FOOT_COUNT] = {};
  float deltaSeconds = 0.0f;
  uint8_t advanceState = 0;
  uint8_t reserved[3] = {};
};

struct DirectVmdTerrainFootOutput {
  VmdVec3 flatTarget = {0.0f, 0.0f, 0.0f};
  VmdVec3 desiredTarget = {0.0f, 0.0f, 0.0f};
  VmdVec3 normal = {0.0f, 1.0f, 0.0f};
  float rawHeight = 0.0f;
  float stableHeight = 0.0f;
  float floorReferenceHeight = 0.0f;
  float contactPlaneHeight = 0.0f;
  float verticalVelocity = 0.0f;
  float lift = 0.0f;
  float surfaceGap = 0.0f;
  float surfaceOffset = 0.0f;
  float appliedOffset = 0.0f;
  float centerHorizontalError = 0.0f;
  float centerFloorDistance = 0.0f;
  float pendingPlaneHeight = 0.0f;
  float planeSwitchSeconds = 0.0f;
  VmdVec3 centerPoint = {0.0f, 0.0f, 0.0f};
  uint32_t planeSwitchCount = 0;
  DirectVmdTerrainContactState contact =
      DirectVmdTerrainContactState::Airborne;
  uint8_t hit = 0;
  uint8_t stablePlane = 0;
  uint8_t heldHit = 0;
  uint8_t ikEnabled = 0;
  uint8_t rawHitCount = 0;
  uint8_t rawClusterCount = 0;
  uint8_t normalFromPointFit = 0;
  uint8_t centerHit = 0;
  uint8_t centerAnchored = 0;
  uint8_t contactPlane = 0;
  uint8_t swingArmed = 0;
  uint8_t pendingPlane = 0;
};

struct DirectVmdTerrainFrameOutput {
  DirectVmdTerrainFootOutput feet[DIRECT_VMD_TERRAIN_FOOT_COUNT];
  DirectVmdTerrainSupportState support =
      DirectVmdTerrainSupportState::None;
  DirectVmdTerrainSupportState rootSupport =
      DirectVmdTerrainSupportState::None;
  DirectVmdTerrainSupportState pendingRootSupport =
      DirectVmdTerrainSupportState::None;
  float rootOffset = 0.0f;
  float rootTargetOffset = 0.0f;
};

static inline void DirectVmdTerrainResetKinematics(
    DirectVmdTerrainState *state, bool clearContacts) {
  if (!state)
    return;
  for (uint32_t index = 0; index < DIRECT_VMD_TERRAIN_FOOT_COUNT;
       ++index) {
    DirectVmdTerrainFootState &foot = state->feet[index];
    foot.previousFlatTargetValid = false;
    foot.verticalVelocity = 0.0f;
    foot.contactTimer = 0.0f;
    foot.releaseTimer = 0.0f;
    foot.planeSwitchTimer = 0.0f;
    if (clearContacts) {
      foot.contact = DirectVmdTerrainContactState::Airborne;
      foot.flatBaselineValid = false;
      foot.contactPlaneValid = false;
      foot.candidatePlaneValid = false;
      foot.swingArmed = false;
    }
  }
  if (clearContacts) {
    state->support = DirectVmdTerrainSupportState::None;
    state->pendingSupport = DirectVmdTerrainSupportState::None;
    state->rootSupport = DirectVmdTerrainSupportState::None;
    state->pendingRootSupport = DirectVmdTerrainSupportState::None;
    state->pendingSupportSeconds = 0.0f;
    state->pendingRootSupportSeconds = 0.0f;
    state->rootTargetOffset = state->rootOffset;
  }
}

static inline DirectVmdTerrainSupportState
DirectVmdTerrainDesiredSupport(const DirectVmdTerrainState &state) {
  const bool left = state.feet[0].contact ==
                    DirectVmdTerrainContactState::Contact;
  const bool right = state.feet[1].contact ==
                     DirectVmdTerrainContactState::Contact;
  if (left && right)
    return DirectVmdTerrainSupportState::Double;
  if (left)
    return DirectVmdTerrainSupportState::Left;
  if (right)
    return DirectVmdTerrainSupportState::Right;
  return DirectVmdTerrainSupportState::None;
}

struct DirectVmdAuthoredFootContactOutput {
  DirectVmdTerrainContactState contact =
      DirectVmdTerrainContactState::Airborne;
  float verticalVelocity = 0.0f;
  float lift = 0.0f;
  uint8_t transitioned = 0;
};

static inline DirectVmdAuthoredFootContactOutput
DirectVmdUpdateAuthoredFootContact(
    DirectVmdTerrainFootState *foot,
    const DirectVmdTerrainConfig &config, bool ikEnabled,
    float flatTargetY, float lowestEnabledFlatY, float deltaSeconds,
    bool advanceState) {
  DirectVmdAuthoredFootContactOutput result;
  if (!foot || !DirectVmdFinite(flatTargetY))
    return result;

  const DirectVmdTerrainContactState before = foot->contact;
  const float dt = advanceState && DirectVmdFinite(deltaSeconds)
                       ? DirectVmdTerrainClamp(deltaSeconds, 0.0f, 0.100f)
                       : 0.0f;
  if (dt > 0.0f) {
    if (foot->previousFlatTargetValid) {
      foot->verticalVelocity =
          (flatTargetY - foot->previousFlatTargetY) / dt;
      if (!DirectVmdFinite(foot->verticalVelocity))
        foot->verticalVelocity = 0.0f;
    } else {
      foot->verticalVelocity = 0.0f;
    }
    foot->previousFlatTargetY = flatTargetY;
    foot->previousFlatTargetValid = true;

    if (!ikEnabled) {
      foot->contact = DirectVmdTerrainContactState::Airborne;
      foot->flatBaselineValid = false;
      foot->contactTimer = 0.0f;
      foot->releaseTimer = 0.0f;
      foot->swingArmed = false;
    } else {
      const bool nearLowest = DirectVmdFinite(lowestEnabledFlatY) &&
          flatTargetY - lowestEnabledFlatY <= config.contactEnterHeight;
      if (!foot->flatBaselineValid && nearLowest &&
          std::fabs(foot->verticalVelocity) <=
              config.contactEnterVelocity) {
        foot->flatBaselineY = flatTargetY;
        foot->flatBaselineValid = true;
      }

      const float authoredLift = foot->flatBaselineValid
          ? flatTargetY - foot->flatBaselineY
          : std::numeric_limits<float>::infinity();
      const bool canEnter = foot->flatBaselineValid && nearLowest &&
          authoredLift <= config.contactEnterHeight &&
          authoredLift >= -config.penetrationEnterHeight &&
          std::fabs(foot->verticalVelocity) <=
              config.contactEnterVelocity;
      const bool fastLift =
          foot->verticalVelocity > config.contactExitUpVelocity;
      const bool outsideContactBand =
          authoredLift > config.contactExitHeight ||
          authoredLift < -config.penetrationExitHeight;

      switch (foot->contact) {
      case DirectVmdTerrainContactState::Airborne:
        foot->releaseTimer = 0.0f;
        if (canEnter) {
          foot->contact = DirectVmdTerrainContactState::Candidate;
          foot->contactTimer = dt;
        } else {
          foot->contactTimer = 0.0f;
        }
        break;
      case DirectVmdTerrainContactState::Candidate:
        foot->releaseTimer = 0.0f;
        if (!canEnter) {
          foot->contact = DirectVmdTerrainContactState::Airborne;
          foot->contactTimer = 0.0f;
        } else {
          foot->contactTimer += dt;
          if (foot->contactTimer >= config.contactEnterSeconds) {
            foot->contact = DirectVmdTerrainContactState::Contact;
            foot->flatBaselineY = flatTargetY;
            foot->contactTimer = config.contactEnterSeconds;
            foot->swingArmed = false;
          }
        }
        break;
      case DirectVmdTerrainContactState::Contact:
        foot->contactTimer = config.contactEnterSeconds;
        if (fastLift) {
          foot->contact = DirectVmdTerrainContactState::Airborne;
          foot->contactTimer = 0.0f;
          foot->releaseTimer = 0.0f;
          foot->swingArmed = true;
        } else if (outsideContactBand) {
          foot->releaseTimer += dt;
          if (foot->releaseTimer >= config.contactExitSeconds) {
            foot->contact = DirectVmdTerrainContactState::Airborne;
            foot->contactTimer = 0.0f;
            foot->releaseTimer = 0.0f;
            if (authoredLift > config.contactExitHeight)
              foot->swingArmed = true;
          }
        } else {
          foot->releaseTimer = 0.0f;
        }
        break;
      }
    }
  }

  result.contact = foot->contact;
  result.verticalVelocity = foot->verticalVelocity;
  result.lift = foot->flatBaselineValid
                    ? (std::max)(0.0f,
                                 flatTargetY - foot->flatBaselineY)
                    : 0.0f;
  result.transitioned = before != foot->contact ? 1 : 0;
  return result;
}

static inline void DirectVmdUpdateTerrainState(
    DirectVmdTerrainState *state, const DirectVmdTerrainConfig &config,
    const DirectVmdTerrainFrameInput &input,
    DirectVmdTerrainFrameOutput *output) {
  if (!state || !output)
    return;
  *output = DirectVmdTerrainFrameOutput();
  float dt = input.advanceState && DirectVmdFinite(input.deltaSeconds)
                 ? DirectVmdTerrainClamp(input.deltaSeconds, 0.0f, 0.100f)
                 : 0.0f;
  float lowestEnabledFlatY = std::numeric_limits<float>::infinity();
  for (uint32_t index = 0; index < DIRECT_VMD_TERRAIN_FOOT_COUNT;
       ++index) {
    if (input.ikEnabled[index] &&
        DirectVmdFinite(input.flatFootTarget[index].y)) {
      lowestEnabledFlatY = (std::min)(
          lowestEnabledFlatY, input.flatFootTarget[index].y);
    }
  }

  for (uint32_t index = 0; index < DIRECT_VMD_TERRAIN_FOOT_COUNT;
       ++index) {
    DirectVmdTerrainFootState &foot = state->feet[index];
    const bool ikEnabled = input.ikEnabled[index] != 0;
    const VmdVec3 flatTarget = input.flatFootTarget[index];

    if (dt > 0.0f) {
      if (foot.previousFlatTargetValid) {
        foot.verticalVelocity =
            (flatTarget.y - foot.previousFlatTargetY) / dt;
        if (!DirectVmdFinite(foot.verticalVelocity))
          foot.verticalVelocity = 0.0f;
      } else {
        foot.verticalVelocity = 0.0f;
      }
      foot.previousFlatTargetY = flatTarget.y;
      foot.previousFlatTargetValid = true;

      const DirectVmdTerrainPlane &raw = input.rawPlane[index];
      foot.rawHit = raw.valid != 0;
      foot.holdingLostHit = false;
      if (raw.valid) {
        foot.rawHeight = raw.height;
        foot.rawHitCount = raw.hitCount;
        foot.rawClusterCount = raw.clusterCount;
        foot.normalFromPointFit = raw.normalFromPointFit;
        foot.centerHit = raw.centerHit;
        foot.centerAnchored = raw.centerAnchored;
        foot.centerHorizontalError = raw.centerHorizontalError;
        foot.centerFloorDistance = raw.centerFloorDistance;
        foot.centerPoint = raw.centerPoint;
        foot.missingHitSeconds = 0.0f;
        if (!foot.floorReferenceValid) {
          foot.floorReferenceHeight = raw.height;
          foot.floorReferenceValid = true;
        }
        if (!foot.stablePlaneValid) {
          foot.stableHeight = raw.height;
          foot.stableNormal = {0.0f, 1.0f, 0.0f};
          foot.stablePlaneValid = true;
        } else {
          foot.stableHeight = DirectVmdTerrainFilteredStep(
              foot.stableHeight, raw.height,
              config.footHeightTimeConstant,
              config.maximumFootHeightSpeed, dt);
        }
        const float normalAlpha = 1.0f - std::exp(
            -dt / (std::max)(config.footNormalTimeConstant, 1.0e-4f));
        VmdVec3 blended = DirectVmdAdd(
            DirectVmdScale(foot.stableNormal, 1.0f - normalAlpha),
            DirectVmdScale(raw.normal, normalAlpha));
        if (!DirectVmdTryNormalizeVector(blended,
                                         &foot.stableNormal))
          foot.stableNormal = {0.0f, 1.0f, 0.0f};
      } else if (foot.stablePlaneValid) {
        foot.missingHitSeconds += dt;
        if (foot.missingHitSeconds <= config.hitLossHoldSeconds) {
          foot.holdingLostHit = true;
        } else {
          foot.stablePlaneValid = false;
          foot.stableNormal = {0.0f, 1.0f, 0.0f};
          foot.rawHitCount = 0;
          foot.rawClusterCount = 0;
          foot.normalFromPointFit = 0;
          foot.centerHit = 0;
          foot.centerAnchored = 0;
          foot.centerHorizontalError = 0.0f;
          foot.centerFloorDistance = 0.0f;
          foot.centerPoint = {0.0f, 0.0f, 0.0f};
        }
      }

      if (!ikEnabled) {
        foot.contact = DirectVmdTerrainContactState::Airborne;
        foot.contactTimer = 0.0f;
        foot.releaseTimer = 0.0f;
        foot.contactPlaneValid = false;
        foot.candidatePlaneValid = false;
        foot.planeSwitchTimer = 0.0f;
        foot.swingArmed = false;
      } else {
        const float sensedHeight =
            foot.contact == DirectVmdTerrainContactState::Contact &&
                    foot.contactPlaneValid
                ? foot.contactPlaneHeight
                : foot.stableHeight;
        const float terrainOffset =
            foot.stablePlaneValid && foot.floorReferenceValid
                ? sensedHeight - foot.floorReferenceHeight
                : state->rootOffset;
        const float gap = state->rootOffset - terrainOffset;
        const bool nearLowestAuthoredFoot =
            DirectVmdFinite(lowestEnabledFlatY) &&
            flatTarget.y - lowestEnabledFlatY <=
                config.contactEnterHeight;
        if (!foot.flatBaselineValid && foot.stablePlaneValid &&
            nearLowestAuthoredFoot &&
            gap <= config.penetrationExitHeight &&
            std::fabs(foot.verticalVelocity) <=
                config.contactEnterVelocity) {
          foot.flatBaselineY = flatTarget.y;
          foot.flatBaselineValid = true;
        }
        const float authoredLift = foot.flatBaselineValid
            ? flatTarget.y - foot.flatBaselineY
            : std::numeric_limits<float>::infinity();
        const bool canEnter =
            foot.stablePlaneValid && foot.flatBaselineValid &&
            authoredLift <= config.contactEnterHeight &&
            authoredLift >= -config.penetrationEnterHeight &&
            std::fabs(foot.verticalVelocity) <=
                config.contactEnterVelocity;
        const bool fastLift =
            foot.verticalVelocity > config.contactExitUpVelocity;
        const bool mustLeave =
            !foot.stablePlaneValid ||
            authoredLift > config.contactExitHeight ||
            authoredLift < -config.penetrationExitHeight;

        const float candidateHeight = raw.valid
            ? raw.height
            : foot.stableHeight;
        const VmdVec3 candidateNormal = raw.valid
            ? raw.normal
            : foot.stableNormal;
        const auto beginCandidate = [&]() {
          foot.contact = DirectVmdTerrainContactState::Candidate;
          foot.contactTimer = dt;
          foot.planeSwitchTimer = 0.0f;
          foot.candidatePlaneHeight = candidateHeight;
          foot.candidatePlaneNormal = candidateNormal;
          foot.candidatePlaneValid = foot.stablePlaneValid;
        };
        const auto commitContact = [&]() {
          foot.contact = DirectVmdTerrainContactState::Contact;
          foot.contactTimer = config.contactEnterSeconds;
          foot.flatBaselineY = flatTarget.y;
          foot.flatBaselineValid = true;
          foot.contactPlaneHeight = foot.candidatePlaneValid
              ? foot.candidatePlaneHeight
              : candidateHeight;
          foot.contactPlaneNormal = foot.candidatePlaneValid
              ? foot.candidatePlaneNormal
              : candidateNormal;
          foot.contactPlaneValid = foot.stablePlaneValid;
          foot.swingArmed = false;
          foot.candidatePlaneValid = false;
          foot.planeSwitchTimer = 0.0f;
        };
        const auto updatePlantedPlane = [&]() {
          if (!raw.valid || !foot.contactPlaneValid)
            return;
          const float currentDelta = std::fabs(
              candidateHeight - foot.contactPlaneHeight);
          if (currentDelta <= config.planeClusterHeight) {
            foot.contactPlaneHeight = DirectVmdTerrainFilteredStep(
                foot.contactPlaneHeight, candidateHeight,
                config.footHeightTimeConstant,
                config.maximumFootHeightSpeed, dt);
            foot.contactPlaneNormal = foot.stableNormal;
            foot.candidatePlaneValid = false;
            foot.planeSwitchTimer = 0.0f;
            return;
          }
          const bool samePendingLayer =
              foot.candidatePlaneValid &&
              std::fabs(candidateHeight -
                        foot.candidatePlaneHeight) <=
                  config.planeClusterHeight;
          if (!samePendingLayer) {
            foot.candidatePlaneHeight = candidateHeight;
            foot.candidatePlaneNormal = candidateNormal;
            foot.candidatePlaneValid = true;
            foot.planeSwitchTimer = dt;
          } else {
            foot.candidatePlaneHeight = candidateHeight;
            foot.candidatePlaneNormal = candidateNormal;
            foot.planeSwitchTimer += dt;
          }
          if (foot.planeSwitchTimer >=
              config.supportSwitchSeconds) {
            foot.contactPlaneHeight = foot.candidatePlaneHeight;
            foot.contactPlaneNormal = foot.candidatePlaneNormal;
            foot.candidatePlaneValid = false;
            foot.planeSwitchTimer = 0.0f;
            ++foot.planeSwitchCount;
          }
        };

        switch (foot.contact) {
        case DirectVmdTerrainContactState::Airborne:
          foot.releaseTimer = 0.0f;
          if (canEnter) {
            beginCandidate();
            if (foot.contactTimer >= config.contactEnterSeconds) {
              commitContact();
            }
          } else {
            foot.contactTimer = 0.0f;
            foot.candidatePlaneValid = false;
            foot.planeSwitchTimer = 0.0f;
          }
          break;
        case DirectVmdTerrainContactState::Candidate:
          foot.releaseTimer = 0.0f;
          if (!canEnter) {
            foot.contact = DirectVmdTerrainContactState::Airborne;
            foot.contactTimer = 0.0f;
            foot.candidatePlaneValid = false;
            foot.planeSwitchTimer = 0.0f;
          } else {
            const bool sameCandidatePlane =
                foot.candidatePlaneValid &&
                std::fabs(candidateHeight -
                          foot.candidatePlaneHeight) <=
                    config.planeClusterHeight;
            if (!sameCandidatePlane) {
              beginCandidate();
            } else {
              foot.contactTimer += dt;
            }
            if (foot.contactTimer >= config.contactEnterSeconds) {
              commitContact();
            }
          }
          break;
        case DirectVmdTerrainContactState::Contact:
          foot.contactTimer = config.contactEnterSeconds;
          if (fastLift) {
            foot.contact = DirectVmdTerrainContactState::Airborne;
            foot.contactTimer = 0.0f;
            foot.releaseTimer = 0.0f;
            foot.candidatePlaneValid = false;
            foot.planeSwitchTimer = 0.0f;
            foot.swingArmed = true;
          } else if (mustLeave) {
            foot.releaseTimer += dt;
            if (foot.releaseTimer >= config.contactExitSeconds) {
              foot.contact = DirectVmdTerrainContactState::Airborne;
              foot.contactTimer = 0.0f;
              foot.releaseTimer = 0.0f;
              foot.candidatePlaneValid = false;
              foot.planeSwitchTimer = 0.0f;
              if (authoredLift > config.contactExitHeight)
                foot.swingArmed = true;
            }
          } else {
            foot.releaseTimer = 0.0f;
            updatePlantedPlane();
          }
          break;
        }
      }
    }
  }

  if (dt > 0.0f) {
    const DirectVmdTerrainSupportState desiredSupport =
        DirectVmdTerrainDesiredSupport(*state);
    if (desiredSupport == state->support) {
      state->pendingSupport = desiredSupport;
      state->pendingSupportSeconds = 0.0f;
    } else {
      if (desiredSupport != state->pendingSupport) {
        state->pendingSupport = desiredSupport;
        state->pendingSupportSeconds = dt;
      } else {
        state->pendingSupportSeconds += dt;
      }
      if (state->pendingSupportSeconds >=
          config.supportSwitchSeconds) {
        state->support = desiredSupport;
        state->pendingSupportSeconds = 0.0f;
      }
    }

    float targetRootOffset = state->rootOffset;
    const auto terrainOffset = [&](uint32_t index) {
      const DirectVmdTerrainFootState &foot = state->feet[index];
      const float supportHeight =
          foot.contact == DirectVmdTerrainContactState::Contact &&
                  foot.contactPlaneValid
              ? foot.contactPlaneHeight
              : foot.stableHeight;
      return foot.stablePlaneValid && foot.floorReferenceValid
          ? supportHeight - foot.floorReferenceHeight
          : state->rootOffset;
    };
    if (state->support == DirectVmdTerrainSupportState::Left &&
        state->feet[0].contact ==
            DirectVmdTerrainContactState::Contact) {
      state->rootSupport = DirectVmdTerrainSupportState::Left;
      state->pendingRootSupport = state->rootSupport;
      state->pendingRootSupportSeconds = 0.0f;
      targetRootOffset = terrainOffset(0);
    } else if (state->support == DirectVmdTerrainSupportState::Right &&
               state->feet[1].contact ==
                   DirectVmdTerrainContactState::Contact) {
      state->rootSupport = DirectVmdTerrainSupportState::Right;
      state->pendingRootSupport = state->rootSupport;
      state->pendingRootSupportSeconds = 0.0f;
      targetRootOffset = terrainOffset(1);
    } else if (state->support == DirectVmdTerrainSupportState::Double &&
               state->feet[0].contact ==
                   DirectVmdTerrainContactState::Contact &&
               state->feet[1].contact ==
                   DirectVmdTerrainContactState::Contact) {
      const float leftOffset = terrainOffset(0);
      const float rightOffset = terrainOffset(1);
      DirectVmdTerrainSupportState desiredRootSupport =
          state->rootSupport;
      if (desiredRootSupport != DirectVmdTerrainSupportState::Left &&
          desiredRootSupport != DirectVmdTerrainSupportState::Right) {
        desiredRootSupport =
            leftOffset <= rightOffset
                ? DirectVmdTerrainSupportState::Left
                : DirectVmdTerrainSupportState::Right;
        state->rootSupport = desiredRootSupport;
        state->pendingRootSupport = desiredRootSupport;
        state->pendingRootSupportSeconds = 0.0f;
      } else {
        if (state->rootSupport == DirectVmdTerrainSupportState::Left &&
            rightOffset + config.planeClusterHeight < leftOffset) {
          desiredRootSupport = DirectVmdTerrainSupportState::Right;
        } else if (
            state->rootSupport == DirectVmdTerrainSupportState::Right &&
            leftOffset + config.planeClusterHeight < rightOffset) {
          desiredRootSupport = DirectVmdTerrainSupportState::Left;
        }
        if (desiredRootSupport == state->rootSupport) {
          state->pendingRootSupport = state->rootSupport;
          state->pendingRootSupportSeconds = 0.0f;
        } else {
          if (state->pendingRootSupport != desiredRootSupport) {
            state->pendingRootSupport = desiredRootSupport;
            state->pendingRootSupportSeconds = dt;
          } else {
            state->pendingRootSupportSeconds += dt;
          }
          if (state->pendingRootSupportSeconds >=
              config.supportSwitchSeconds) {
            state->rootSupport = desiredRootSupport;
            state->pendingRootSupportSeconds = 0.0f;
          }
        }
      }
      targetRootOffset =
          state->rootSupport == DirectVmdTerrainSupportState::Left
              ? leftOffset
              : rightOffset;
    }
    state->rootOffset = DirectVmdTerrainFilteredStep(
        state->rootOffset, targetRootOffset,
        config.rootHeightTimeConstant,
        config.maximumRootHeightSpeed, dt);
    state->rootTargetOffset = targetRootOffset;
  }

  output->support = state->support;
  output->rootSupport = state->rootSupport;
  output->pendingRootSupport = state->pendingRootSupport;
  output->rootOffset = state->rootOffset;
  output->rootTargetOffset = state->rootTargetOffset;
  for (uint32_t index = 0; index < DIRECT_VMD_TERRAIN_FOOT_COUNT;
       ++index) {
    DirectVmdTerrainFootState &foot = state->feet[index];
    DirectVmdTerrainFootOutput &result = output->feet[index];
    result.flatTarget = input.flatFootTarget[index];
    result.desiredTarget = input.flatFootTarget[index];
    result.rawHeight = foot.rawHeight;
    result.stableHeight = foot.stableHeight;
    result.floorReferenceHeight = foot.floorReferenceHeight;
    result.contactPlaneHeight = foot.contactPlaneHeight;
    result.verticalVelocity = foot.verticalVelocity;
    result.contact = foot.contact;
    result.hit = foot.rawHit ? 1 : 0;
    result.stablePlane = foot.stablePlaneValid ? 1 : 0;
    result.heldHit = foot.holdingLostHit ? 1 : 0;
    result.ikEnabled = input.ikEnabled[index];
    result.rawHitCount = foot.rawHitCount;
    result.rawClusterCount = foot.rawClusterCount;
    result.normalFromPointFit = foot.normalFromPointFit;
    result.centerHit = foot.centerHit;
    result.centerAnchored = foot.centerAnchored;
    result.centerHorizontalError = foot.centerHorizontalError;
    result.centerFloorDistance = foot.centerFloorDistance;
    result.centerPoint = foot.centerPoint;
    result.contactPlane = foot.contactPlaneValid ? 1 : 0;
    result.swingArmed = foot.swingArmed ? 1 : 0;
    result.pendingPlane = foot.candidatePlaneValid ? 1 : 0;
    result.pendingPlaneHeight = foot.candidatePlaneHeight;
    result.planeSwitchSeconds = foot.planeSwitchTimer;
    result.planeSwitchCount = foot.planeSwitchCount;
    result.normal =
        foot.contact == DirectVmdTerrainContactState::Contact &&
                foot.contactPlaneValid
            ? foot.contactPlaneNormal
            : (foot.stablePlaneValid
                   ? foot.stableNormal
                   : VmdVec3{0.0f, 1.0f, 0.0f});
    if (foot.stablePlaneValid && foot.floorReferenceValid) {
      const float targetHeight =
          foot.contact == DirectVmdTerrainContactState::Contact &&
                  foot.contactPlaneValid
              ? foot.contactPlaneHeight
              : foot.stableHeight;
      result.surfaceOffset =
          targetHeight - foot.floorReferenceHeight;
      result.surfaceGap = state->rootOffset - result.surfaceOffset;
    }
    result.lift = foot.flatBaselineValid
        ? (std::max)(0.0f,
                     input.flatFootTarget[index].y - foot.flatBaselineY)
        : 0.0f;
    const bool grounded = input.ikEnabled[index] &&
        foot.contact == DirectVmdTerrainContactState::Contact &&
        foot.stablePlaneValid && foot.contactPlaneValid;
    if (dt > 0.0f) {
      if (grounded) {
        foot.appliedOffset = DirectVmdTerrainFilteredStep(
            foot.appliedOffset, result.surfaceOffset,
            config.footHeightTimeConstant,
            config.maximumFootHeightSpeed, dt);
      } else {
        foot.appliedOffset = state->rootOffset;
      }
    }
    result.desiredTarget.y += foot.appliedOffset;
    result.appliedOffset =
        result.desiredTarget.y - input.flatFootTarget[index].y;
  }
}

static inline float DirectVmdTerrainPlaneHeightAt(
    float originHeight, VmdVec3 normal, VmdVec3 origin,
    VmdVec3 point) {
  VmdVec3 unit = {};
  if (!DirectVmdTryNormalizeVector(normal, &unit) ||
      std::fabs(unit.y) < 1.0e-4f)
    return originHeight;
  return originHeight -
      (unit.x * (point.x - origin.x) +
       unit.z * (point.z - origin.z)) / unit.y;
}

static inline VmdQuaternion DirectVmdTerrainNormalRotation(
    VmdVec3 normal) {
  VmdVec3 unit = {};
  if (!DirectVmdTryNormalizeVector(normal, &unit) || unit.y <= 0.0f)
    return {0.0f, 0.0f, 0.0f, 1.0f};
  return DirectVmdQuaternionFromTo({0.0f, 1.0f, 0.0f}, unit);
}
