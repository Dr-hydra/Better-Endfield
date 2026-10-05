#pragma once
#include "math/quat_math.h"
#include <algorithm>
#include <array>

namespace mmd {
enum class MotionPart {
  Torso,
  Head,
  LeftArm,
  RightArm,
  LeftHand,
  RightHand,
  LeftLeg,
  RightLeg,
  LeftFoot,
  RightFoot,
  Count
};
inline int MotionPartForRole(int role) {
  switch (role) {
  case 0:
  case 7:
  case 8:
  case 54:
    return int(MotionPart::Torso);
  case 9:
  case 10:
    return int(MotionPart::Head);
  case 11:
  case 13:
  case 15:
    return int(MotionPart::LeftArm);
  case 12:
  case 14:
  case 16:
    return int(MotionPart::RightArm);
  case 17:
    return int(MotionPart::LeftHand);
  case 18:
    return int(MotionPart::RightHand);
  case 1:
  case 3:
    return int(MotionPart::LeftLeg);
  case 2:
  case 4:
    return int(MotionPart::RightLeg);
  case 5:
  case 19:
    return int(MotionPart::LeftFoot);
  case 6:
  case 20:
    return int(MotionPart::RightFoot);
  default:
    if (role >= 24 && role <= 38)
      return int(MotionPart::LeftHand);
    if (role >= 39 && role <= 53)
      return int(MotionPart::RightHand);
    return -1; // Eyes, jaw and unmapped accessories keep their own animation.
  }
}
struct MotionAmplitude {
  float master = 1;
  std::array<float, int(MotionPart::Count)> parts;
  MotionAmplitude() { parts.fill(1); }
  static float safe(float value) {
    return std::isfinite(value) ? (std::max)(0.f, (std::min)(2.f, value)) : 1.f;
  }
  float factor(int role) const {
    int part = MotionPartForRole(role);
    return part < 0 ? 1.f : safe(master) * safe(parts[part]);
  }
};
// Scale the shortest local joint delta, after IK and retargeting. Multiplying
// source FK keys before IK lets the solver erase the requested leg adjustment.
inline Quat ScaleMotionRotation(Quat neutral, Quat animated, float factor) {
  if (factor == 1)
    return animated;
  if (factor == 0)
    return neutral;
  Quat delta = NormQ(Conj(neutral) * animated);
  if (delta.w < 0)
    delta = {-delta.x, -delta.y, -delta.z, -delta.w};
  float sine = Len({delta.x, delta.y, delta.z});
  if (sine < 1e-7f)
    return neutral;
  float angle = 2.f * std::atan2(sine, delta.w);
  return NormQ(neutral *
               Quat::AxisAngle({delta.x, delta.y, delta.z}, angle * factor));
}
} // namespace mmd
