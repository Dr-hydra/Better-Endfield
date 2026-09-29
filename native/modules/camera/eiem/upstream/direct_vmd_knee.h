#pragma once
#include "direct_vmd_pose.h"

static constexpr float DIRECT_VMD_KNEE_DEFAULT_WEIGHT = 0.35f;

static inline float DirectVmdKneeRamp(float value, float low, float high) {
  if (!DirectVmdFinite(value)) return 0.0f;
  const float t = (std::max)(0.0f, (std::min)(1.0f, (value-low)/(high-low)));
  return t*t*(3.0f-2.0f*t);
}

static inline DirectVmdKneeReferencePod DirectVmdSourceKneeReference(
    VmdVec3 upper, VmdVec3 lower, bool effectiveKneeFk) {
  DirectVmdKneeReferencePod result;
  VmdVec3 u{}, l{}, axis{};
  if (!effectiveKneeFk || !DirectVmdTryNormalizeVector(upper, &u) ||
      !DirectVmdTryNormalizeVector(lower, &l) ||
      !DirectVmdTryNormalizeVector(DirectVmdAdd(upper, lower), &axis))
    return result;
  const VmdVec3 normal = DirectVmdCross(u, l);
  result.confidence = DirectVmdKneeRamp(DirectVmdLength(normal), .035f, .174f);
  if (!DirectVmdTryNormalizeVector(DirectVmdCross(axis, normal), &result.direction))
    result.confidence = 0.0f;
  return result;
}

struct DirectVmdKneeMix {
  VmdVec3 normal{};
  float effectiveWeight = 0;
  bool applied = false;
};

static inline DirectVmdKneeMix DirectVmdMixKneeDirection(
    VmdVec3 hipToTarget, VmdVec3 animationNormal,
    DirectVmdKneeReferencePod sourceWorld, float maximumReach, float weight) {
  DirectVmdKneeMix out;
  VmdVec3 axis{}, current{}, source{};
  if (!DirectVmdFinite(weight) || weight <= 0 ||
      !DirectVmdFinite(sourceWorld.confidence) || sourceWorld.confidence <= 0 ||
      !DirectVmdFinite(maximumReach) || maximumReach <= 0 ||
      !DirectVmdTryNormalizeVector(hipToTarget, &axis) ||
      !DirectVmdTryNormalizeVector(DirectVmdCross(axis, animationNormal), &current))
    return out;
  const VmdVec3 projection = DirectVmdSub(sourceWorld.direction,
      DirectVmdScale(axis, DirectVmdDot(axis, sourceWorld.direction)));
  if (!DirectVmdTryNormalizeVector(projection, &source)) return out;
  const float dot = (std::max)(-1.0f, (std::min)(1.0f, DirectVmdDot(current, source)));
  const float opposition = DirectVmdKneeRamp(dot, -.98f, -.8f);
  const float extension = 1.0f - DirectVmdKneeRamp(
      DirectVmdLength(hipToTarget)/maximumReach, .995f, .99995f);
  out.effectiveWeight = (std::min)(weight, 1.0f) *
      (std::min)(sourceWorld.confidence, 1.0f) * opposition * extension *
      DirectVmdKneeRamp(DirectVmdLength(projection), .035f, .174f) *
      DirectVmdKneeRamp(DirectVmdLength(DirectVmdCross(axis, animationNormal)), .035f, .174f);
  if (out.effectiveWeight <= 0) return out;
  const float angle = std::atan2(DirectVmdDot(axis, DirectVmdCross(current, source)), dot)
                      * out.effectiveWeight;
  const VmdVec3 direction = DirectVmdAdd(DirectVmdScale(current, std::cos(angle)),
      DirectVmdScale(DirectVmdCross(axis, current), std::sin(angle)));
  out.applied = DirectVmdTryNormalizeVector(DirectVmdCross(direction, axis), &out.normal);
  return out;
}

struct DirectVmdKneeSolverOverride {
  VmdVec3 normal{}, backup{};
  float weight = 0;
  bool active = false;
  void Apply(VmdVec3 *normalField, VmdVec3 *backupField, float *weightField,
             VmdVec3 mixedNormal) {
    if (active) return;
    normal = *normalField; backup = *backupField; weight = *weightField;
    active = true;
    *normalField = mixedNormal;
    *weightField = 0.0f;
  }
  void Restore(VmdVec3 *normalField, VmdVec3 *backupField, float *weightField) {
    if (!active) return;
    *normalField = normal; *backupField = backup; *weightField = weight;
    active = false;
  }
};
