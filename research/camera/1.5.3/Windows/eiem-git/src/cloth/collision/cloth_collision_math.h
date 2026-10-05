#pragma once
#include <algorithm>
#include <cmath>
#include <cstdint>

namespace eiem_collision {
struct V {
  double x = 0, y = 0, z = 0;
};
inline V operator+(V a, V b) { return {a.x + b.x, a.y + b.y, a.z + b.z}; }
inline V operator-(V a, V b) { return {a.x - b.x, a.y - b.y, a.z - b.z}; }
inline V operator*(V a, double b) { return {a.x * b, a.y * b, a.z * b}; }
inline double Dot(V a, V b) { return a.x * b.x + a.y * b.y + a.z * b.z; }
inline V Cross(V a, V b) {
  return {a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x};
}
inline double Length(V a) { return std::sqrt(Dot(a, a)); }
inline bool Finite(V a) { return std::isfinite(a.x) && std::isfinite(a.y) && std::isfinite(a.z); }
inline bool UniformPositive(V s) {
  return Finite(s) && s.x > 1e-6 && s.y > 1e-6 && s.z > 1e-6 && std::abs(s.x - s.y) <= s.x * .001 &&
         std::abs(s.x - s.z) <= s.x * .001;
}
inline bool UniformBasis(V x, V y, V z, double scale) {
  if (!Finite(x) || !Finite(y) || !Finite(z) || !std::isfinite(scale) || scale <= 1e-6)
    return false;
  x = x * (1 / scale);
  y = y * (1 / scale);
  z = z * (1 / scale);
  return std::abs(Length(x) - 1) < .002 && std::abs(Length(y) - 1) < .002 &&
         std::abs(Length(z) - 1) < .002 && std::abs(Dot(x, y)) < .002 &&
         std::abs(Dot(x, z)) < .002 && std::abs(Dot(y, z)) < .002 && Dot(Cross(x, y), z) > .998;
}
inline constexpr const char *GeometryDefinition = "beyond-capsule-total-length-v1";
struct Capsule {
  V a, b;
  double ra = 0, rb = 0;
  bool valid = false;
};
inline Capsule LocalCapsule(V center, V axis, V size, bool reverse, bool separated, bool centered) {
  Capsule c{};
  const double norm = Length(axis);
  if (!Finite(center) || !Finite(size) || !Finite(axis) || norm < .999 || norm > 1.001 ||
      size.x <= 0 || size.y < 0 || size.z < 0)
    return c;
  c.ra = size.x;
  c.rb = separated ? size.y : size.x;
  if (c.rb <= 0)
    return c;
  axis = axis * (reverse ? -1 : 1);
  if (centered) {
    c.a = center + axis * (std::max)(0.0, size.z * .5 - c.ra);
    c.b = center - axis * (std::max)(0.0, size.z * .5 - c.rb);
  } else {
    const double span = (std::max)(0.0, size.z - c.ra - c.rb);
    c.a = center;
    c.b = c.a - axis * span;
  }
  c.valid = true;
  return c;
}
inline double SegmentDistance(V p, V a, V b) {
  V d = b - a;
  const double dd = Dot(d, d);
  double t = dd > 1e-12 ? std::clamp(Dot(p - a, d) / dd, 0.0, 1.0) : 0;
  return Length(p - (a + d * t));
}
inline int HipEnd(const Capsule &c, V hip, V knee) {
  if (!c.valid || !Finite(hip) || !Finite(knee))
    return -1;
  const double len = Length(knee - hip), span = Length(c.b - c.a);
  if (len < .05 || len > 2 || span < .01 ||
      std::abs(Dot(c.b - c.a, knee - hip)) / (span * len) < .8)
    return -1;
  if (SegmentDistance(c.a, hip, knee) > len * .6 || SegmentDistance(c.b, hip, knee) > len * .6)
    return -1;
  double a = Length(c.a - hip), b = Length(c.b - hip);
  return std::abs(a - b) < len * .1 ? -1 : (a < b ? 0 : 1);
}
inline double HipRadius(double original, double margin, double worldScale) {
  if (!std::isfinite(original) || original <= 0 || !std::isfinite(margin) || margin < 0 ||
      !std::isfinite(worldScale) || worldScale <= 0)
    return original;
  return original + (std::min)({margin / worldScale, original * .25, .03 / worldScale});
}
enum class Kind { Unknown, MeshCloth, BoneCloth, BoneSpring };
inline const char *Policy(Kind kind, bool edgesKnown, bool edges, bool collisionBonesKnown,
                          int collisionBones) {
  if (kind == Kind::BoneSpring)
    return !collisionBonesKnown
               ? "spring-collision-bones-unknown"
               : (collisionBones == 0
                      ? "spring-no-selected-contact-bones"
                      : "spring-check-selected-bones-limit-distance-and-restoration");
  if (kind == Kind::BoneCloth || kind == Kind::MeshCloth)
    return !edgesKnown ? "proxy-edges-unknown-no-mode-change"
                       : (edges ? "edge-capable-candidate-contact-ab-required"
                                : "no-proxy-edges-no-edge-request");
  return "unknown-cloth-type-no-contact-policy-write";
}
enum class LeasePhase { Empty, Registering, Registered, Removing, Quarantined, Destroyable };
struct LeasePolicy {
  LeasePhase phase = LeasePhase::Empty;
  uint64_t started = 0;
  int lastFrame = -1;
  unsigned reads = 0;
  void Begin(uint64_t now) {
    phase = LeasePhase::Registering;
    started = now;
    lastFrame = -1;
    reads = 0;
  }
  void Cancel(uint64_t now) {
    if (phase != LeasePhase::Empty && phase != LeasePhase::Removing &&
        phase != LeasePhase::Quarantined && phase != LeasePhase::Destroyable) {
      phase = LeasePhase::Removing;
      started = now;
      reads = 0;
      lastFrame = -1;
    }
  }
  void Observe(uint64_t now, int frame, bool known, bool serialized, bool process, bool team,
               bool anyTeam, bool verifiedRetiredOwnerTeam = false) {
    if (frame == lastFrame)
      return;
    lastFrame = frame;
    if (phase == LeasePhase::Registering || phase == LeasePhase::Registered) {
      if (known && serialized && process && team) {
        if (++reads >= 2)
          phase = LeasePhase::Registered;
      } else {
        reads = 0;
        if (phase == LeasePhase::Registered || now - started >= 8000)
          Cancel(now);
      }
    } else if (phase == LeasePhase::Removing) {
      if (known && !serialized && !process && (!anyTeam || verifiedRetiredOwnerTeam)) {
        if (++reads >= 2)
          phase = LeasePhase::Destroyable;
      } else {
        reads = 0;
        if (now - started >= 8000)
          phase = LeasePhase::Quarantined;
      }
    }
  }
};
}
