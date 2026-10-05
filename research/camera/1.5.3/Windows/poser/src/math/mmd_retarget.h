#pragma once
#include "math/ik_two_bone.h"
#include "math/mmd_rig.h"
#include "math/mmd_amplitude.h"

namespace mmd {
// Column-major, like Unity's Matrix4x4 value type.
struct Matrix {
  float m[16] = {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1};
  Vec3 position() const { return {m[12], m[13], m[14]}; }
};
inline Matrix operator*(const Matrix &a, const Matrix &b) {
  Matrix o;
  for (int c = 0; c < 4; c++)
    for (int r = 0; r < 4; r++) {
      o.m[c * 4 + r] = 0;
      for (int k = 0; k < 4; k++)
        o.m[c * 4 + r] += a.m[k * 4 + r] * b.m[c * 4 + k];
    }
  return o;
}
inline bool Inverse(const Matrix &a, Matrix &out) {
  double t[4][8] = {};
  for (int r = 0; r < 4; r++)
    for (int c = 0; c < 4; c++) {
      t[r][c] = a.m[c * 4 + r];
      t[r][c + 4] = r == c ? 1 : 0;
    }
  for (int c = 0; c < 4; c++) {
    int p = c;
    for (int r = c + 1; r < 4; r++)
      if (std::fabs(t[r][c]) > std::fabs(t[p][c]))
        p = r;
    if (std::fabs(t[p][c]) < 1e-10)
      return false;
    for (int j = 0; j < 8; j++)
      std::swap(t[p][j], t[c][j]);
    double d = t[c][c];
    for (int j = 0; j < 8; j++)
      t[c][j] /= d;
    for (int r = 0; r < 4; r++)
      if (r != c) {
        double s = t[r][c];
        for (int j = 0; j < 8; j++)
          t[r][j] -= s * t[c][j];
      }
  }
  for (int r = 0; r < 4; r++)
    for (int c = 0; c < 4; c++) {
      out.m[c * 4 + r] = float(t[r][c + 4]);
      if (!std::isfinite(out.m[c * 4 + r]))
        return false;
    }
  return true;
}
inline Quat Basis(Vec3 x, Vec3 y, Vec3 z) {
  float trace = x.x + y.y + z.z;
  Quat q;
  if (trace > 0) {
    float s = std::sqrt(trace + 1) * 2;
    q = {(y.z - z.y) / s, (z.x - x.z) / s, (x.y - y.x) / s, .25f * s};
  } else if (x.x > y.y && x.x > z.z) {
    float s = std::sqrt(1 + x.x - y.y - z.z) * 2;
    q = {.25f * s, (y.x + x.y) / s, (z.x + x.z) / s, (y.z - z.y) / s};
  } else if (y.y > z.z) {
    float s = std::sqrt(1 + y.y - x.x - z.z) * 2;
    q = {(y.x + x.y) / s, .25f * s, (z.y + y.z) / s, (z.x - x.z) / s};
  } else {
    float s = std::sqrt(1 + z.z - x.x - y.y) * 2;
    q = {(z.x + x.z) / s, (z.y + y.z) / s, .25f * s, (x.y - y.x) / s};
  }
  return NormQ(q);
}
inline Quat Rotation(const Matrix &m) {
  Vec3 x = Norm({m.m[0], m.m[1], m.m[2]}), y = Norm({m.m[4], m.m[5], m.m[6]});
  Vec3 z = Norm(Cross(x, y));
  y = Norm(Cross(z, x));
  return Basis(x, y, z);
}
inline Matrix TRS(Vec3 p, Quat q, Vec3 scale = {1, 1, 1}) {
  Matrix m;
  Vec3 axes[] = {q * Vec3{scale.x, 0, 0}, q * Vec3{0, scale.y, 0},
                 q * Vec3{0, 0, scale.z}};
  for (int c = 0; c < 3; ++c) {
    m.m[c * 4] = axes[c].x;
    m.m[c * 4 + 1] = axes[c].y;
    m.m[c * 4 + 2] = axes[c].z;
  }
  m.m[12] = p.x;
  m.m[13] = p.y;
  m.m[14] = p.z;
  return m;
}
inline Quat BodyBasis(Vec3 left, Vec3 right, Vec3 hip, Vec3 head) {
  Vec3 x = Norm(left - right), y = Norm(head - hip), z = Norm(Cross(x, y));
  y = Norm(Cross(z, x));
  return Basis(x, y, z);
}
struct TargetBone {
  std::string name;
  int parent = -1, role = -1;
  Vec3 localPos, restPos;
  Vec3 localScale{1, 1, 1};
  Quat localRot, restRot;
  Matrix restMatrix;
  bool calibrated = false;
};
struct RetargetProfile {
  std::string model, fingerprint;
  std::vector<TargetBone> bones;
  std::array<int, 55> roles;
  RetargetProfile() { roles.fill(-1); }
  void globals() {
    roles.fill(-1);
    for (size_t i = 0; i < bones.size(); i++) {
      auto &b = bones[i];
      if (b.parent >= int(i) || b.parent < -1)
        throw std::runtime_error("Invalid target hierarchy");
      if (b.role >= 0 && b.role < 55)
        roles[b.role] = int(i);
      if (b.parent >= 0) {
        auto &p = bones[b.parent];
        b.restMatrix = p.restMatrix * TRS(b.localPos, b.localRot, b.localScale);
        b.restRot = NormQ(p.restRot * b.localRot);
      } else {
        b.restMatrix = TRS(b.localPos, b.localRot, b.localScale);
        b.restRot = b.localRot;
      }
      b.restPos = b.restMatrix.position();
    }
  }
  bool valid() const {
    for (int role : {0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 13, 14, 15, 16, 17, 18}) {
      int i = roles[role];
      if (i < 0 || !bones[i].calibrated)
        return false;
    }
    Vec3 across = bones[roles[13]].restPos - bones[roles[14]].restPos;
    Vec3 up = bones[roles[10]].restPos - bones[roles[0]].restPos;
    return Len(across) > 1e-4f && Len(up) > 1e-4f &&
           Len(Cross(across, up)) > 1e-5f;
  }
};
// Manual calibration starts from the actual pose, whose local axes need not be
// Unity's axes. Only apply world-space deltas, converted through the real
// parent. Never reset local rotations: a Biped pelvis may be rotated 90 degrees
// at rest.
inline bool MakeCalibrationTPose(RetargetProfile &profile,
                                 Vec3 up = {0, 1, 0}) {
  auto p =
      profile; // Reject incomplete/degenerate rigs without partially editing.
  p.globals();
  for (int role : {0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 13, 14, 15, 16, 17, 18})
    if (p.roles[role] < 0)
      return false;
  up = Norm(up);
  if (Len(up) < .9f)
    return false;
  auto position = [&](int role) { return p.bones[p.roles[role]].restPos; };
  Vec3 left = position(13) - position(14);
  left = Norm(left - up * Dot(left, up));
  if (Len(left) < .9f)
    return false;
  const Vec3 forward = Norm(Cross(up, left));
  auto setWorld = [&](int i, Quat rotation) {
    auto &b = p.bones[i];
    b.localRot = NormQ(
        (b.parent < 0 ? Quat{} : Conj(p.bones[b.parent].restRot)) * rotation);
    p.globals();
  };
  auto align = [&](int role, int childRole, Vec3 direction) {
    int i = p.roles[role], j = p.roles[childRole];
    if (i < 0 || j < 0)
      return;
    Vec3 from = p.bones[j].restPos - p.bones[i].restPos;
    if (Len(from) > 1e-5f)
      setWorld(i, Quat::FromTo(from, direction) * p.bones[i].restRot);
  };
  // Establish the pelvis from anatomical landmarks, not the pelvis' local Y.
  Vec3 across = position(1) - position(2), spine = position(7) - position(0);
  if (Len(Cross(across, spine)) < 1e-6f)
    return false;
  Quat pelvisBasis =
      BodyBasis(position(1), position(2), position(0), position(7));
  int hip = p.roles[0];
  setWorld(hip, Basis(left, up, forward * -1) * Conj(pelvisBasis) *
                    p.bones[hip].restRot);
  // Resolve from parents toward children. Every alignment reads updated
  // globals.
  std::vector<int> spineRoles{7};
  for (int role : {8, 54, 9, 10})
    if (p.roles[role] >= 0)
      spineRoles.push_back(role);
  for (size_t i = 1; i < spineRoles.size(); ++i)
    align(spineRoles[i - 1], spineRoles[i], up);
  for (int side = 0; side < 2; ++side) {
    Vec3 arm = left * (side ? -1.f : 1.f);
    int shoulder = 11 + side, upper = 13 + side, lower = 15 + side,
        hand = 17 + side;
    align(shoulder, upper, arm);
    align(upper, lower, arm);
    align(lower, hand, arm);
    // Aim the hand along its middle finger and keep the palm facing down.
    int index = 27 + side * 15, middle = 30 + side * 15,
        little = 36 + side * 15;
    align(hand, middle, arm);
    if (p.roles[index] >= 0 && p.roles[little] >= 0) {
      Vec3 width = position(index) - position(little);
      width = Norm(width - arm * Dot(width, arm));
      if (Len(width) > .9f) {
        float angle =
            std::atan2(Dot(arm, Cross(width, forward)), Dot(width, forward));
        int h = p.roles[hand];
        setWorld(h, Quat::AxisAngle(arm, angle) * p.bones[h].restRot);
      }
    }
    for (int finger = 0; finger < 4; ++finger) {
      int proximal = index + finger * 3;
      align(proximal, proximal + 1, arm);
      align(proximal + 1, proximal + 2, arm);
      int distal = p.roles[proximal + 2];
      if (distal >= 0) {
        // A terminal marker is useful for the last phalanx; never guess between
        // multiple children (some game rigs have corrective bones here).
        int child = -1, count = 0;
        for (size_t j = 0; j < p.bones.size(); ++j)
          if (p.bones[j].parent == distal) {
            child = int(j);
            ++count;
          }
        if (count == 1) {
          Vec3 from = p.bones[child].restPos - p.bones[distal].restPos;
          if (Len(from) > 1e-5f)
            setWorld(distal, Quat::FromTo(from, arm) * p.bones[distal].restRot);
        }
      }
    }
    // Preserve foot pitch and roll while straightening the legs; resetting a
    // foot to identity makes heels point sideways on rigs with Biped axes.
    int foot = p.roles[5 + side];
    Quat footWorld = p.bones[foot].restRot;
    align(1 + side, 3 + side, up * -1);
    align(3 + side, 5 + side, up * -1);
    setWorld(foot, footWorld);
    int toe = 19 + side;
    if (p.roles[toe] >= 0) {
      Vec3 v = position(toe) - position(5 + side);
      float vertical = Dot(v, up);
      Vec3 flat = v - up * vertical;
      if (Len(flat) > 1e-5f)
        align(5 + side, toe, forward * Len(flat) + up * vertical);
    }
  }
  profile = std::move(p);
  return true;
}
inline bool CalibrationTPoseValid(const RetargetProfile &p,
                                  Vec3 up = {0, 1, 0}) {
  if (!p.valid())
    return false;
  auto direction = [&](int a, int b) {
    return Norm(p.bones[p.roles[b]].restPos - p.bones[p.roles[a]].restPos);
  };
  up = Norm(up);
  Vec3 left = direction(14, 13);
  if (Dot(direction(0, 10), up) < .94f || std::fabs(Dot(left, up)) > .2f)
    return false;
  for (int side = 0; side < 2; ++side) {
    Vec3 arm = left * (side ? -1.f : 1.f);
    if (Dot(direction(13 + side, 15 + side), arm) < .94f ||
        Dot(direction(15 + side, 17 + side), arm) < .94f ||
        Dot(direction(1 + side, 3 + side), up * -1) < .94f ||
        Dot(direction(3 + side, 5 + side), up * -1) < .94f)
      return false;
  }
  return true;
}
struct SampledPose {
  std::vector<Quat> localRot, worldRot;
  std::vector<Vec3> worldPos;
  std::vector<Matrix> worldMatrix;
  std::vector<bool> write;
  Vec3 rootOffset;
  std::array<bool, 2> legIkActive{false, false};
};
class Retargeter {
  const RigDefinition *source_ = nullptr;
  const MotionClip *clip_ = nullptr;
  const RetargetProfile *target_ = nullptr;
  RigEvaluator eval_;
  std::array<int, 55> sourceRole_;
  std::array<Quat, 55> alignedRest_;
  std::array<bool, 55> affected_;
  std::vector<Quat> neutralLocal_;
  Quat basis_;
  void world() {
    for (size_t i = 0; i < target_->bones.size(); i++) {
      auto &b = target_->bones[i];
      if (b.parent < 0) {
        output.worldRot[i] = output.localRot[i];
        output.worldMatrix[i] =
            TRS(b.localPos, output.localRot[i], b.localScale);
      } else {
        output.worldRot[i] =
            NormQ(output.worldRot[b.parent] * output.localRot[i]);
        output.worldMatrix[i] =
            output.worldMatrix[b.parent] *
            TRS(b.localPos, output.localRot[i], b.localScale);
      }
      output.worldPos[i] = output.worldMatrix[i].position();
    }
  }
  void setWorld(int i, Quat q) {
    int p = target_->bones[i].parent;
    output.localRot[i] =
        NormQ((p >= 0 ? Conj(output.worldRot[p]) : Quat{}) * q);
    output.write[i] = true;
    world();
  }
  void applyAmplitude(const MotionAmplitude &amplitude) {
    bool changed = false;
    for (size_t i = 0; i < target_->bones.size(); ++i) {
      float factor = amplitude.factor(target_->bones[i].role);
      if (output.write[i] && factor != 1) {
        output.localRot[i] = ScaleMotionRotation(neutralLocal_[i], output.localRot[i], factor);
        changed = true;
      }
    }
    if (changed) world();
  }

public:
  SampledPose output;
  float suggestedScale = .08f;
  Quat sourceBasis() const { return basis_; }
  const RigPose &sourcePose() const { return eval_.pose; }
  const std::vector<std::string> &unmapped() const { return eval_.unmapped; }
  void bind(const RigDefinition &source, const MotionClip &clip,
            const RetargetProfile &target,
            const std::map<int, std::string> &roleBindings = {},
            const std::map<std::string, std::string> &trackBindings = {}) {
    source_ = &source;
    clip_ = &clip;
    target_ = &target;
    sourceRole_.fill(-1);
    affected_.fill(false);
    bool bodyMotion = false;
    for (const auto &track : clip.bones)
      bodyMotion = bodyMotion || !EyeBone(track.first);
    for (int r = 0; r < 55; r++)
      sourceRole_[r] = source.find(RoleNames()[r]);
    if (sourceRole_[0] < 0)
      sourceRole_[0] = source.find(u8"センター");
    if (sourceRole_[8] < 0)
      sourceRole_[8] = sourceRole_[7];
    if (sourceRole_[54] < 0)
      sourceRole_[54] = sourceRole_[8];
    for (int side = 0; side < 2; side++) {
      int role = side ? 39 : 24;
      std::string s = side ? u8"右" : u8"左";
      if (sourceRole_[role] < 0) {
        sourceRole_[role] = source.find(s + u8"親指1");
        sourceRole_[role + 1] = source.find(s + u8"親指2");
        sourceRole_[role + 2] = -1;
      }
    }
    auto geometryRoles = sourceRole_;
    for (const auto &kv : roleBindings) {
      if (kv.first < 0 || kv.first >= 55)
        throw std::runtime_error("Invalid retarget role");
      int i = kv.second.empty() ? -1 : source.find(kv.second);
      if (!kv.second.empty() && i < 0)
        throw std::runtime_error("Mapped source bone does not exist: " + kv.second);
      sourceRole_[kv.first] = i;
      if (i >= 0) geometryRoles[kv.first] = i;
    }
    eval_.bind(source, clip, trackBindings, std::vector<int>(sourceRole_.begin(), sourceRole_.end()));
    auto sp = [&](int r) {
      int i = geometryRoles[r];
      return i >= 0 ? source.bones[i].rest : Vec3{};
    };
    auto tp = [&](int r) {
      int i = target.roles[r];
      return i >= 0 ? target.bones[i].restPos : Vec3{};
    };
    basis_ = NormQ(BodyBasis(tp(13), tp(14), tp(0), tp(10)) *
                   Conj(BodyBasis(sp(13), sp(14), sp(0), sp(10))));
    float sl=0,tl=0;
    for(int side=0;side<2;++side) {
      sl+=Len(sp(1+side)-sp(3+side))+Len(sp(3+side)-sp(5+side));
      tl+=Len(tp(1+side)-tp(3+side))+Len(tp(3+side)-tp(5+side));
    }
    suggestedScale=sl>1e-5f&&tl>1e-5f?Clamp(tl/sl,.001f,.3f):.08f;
    const int child[55] = {7,  3,  4,  5,  6,  19, 20, 8,  54, 10, -1,
                           13, 14, 15, 16, 17, 18, 30, 45, -1, -1, -1,
                           -1, -1, 25, 26, -1, 28, 29, -1, 31, 32, -1,
                           34, 35, -1, 37, 38, -1, 40, 41, -1, 43, 44,
                           -1, 46, 47, -1, 49, 50, -1, 52, 53, -1, 9};
    for (int r = 0; r < 55; r++) {
      int si = sourceRole_[r], ti = target.roles[r];
      if (si < 0 || ti < 0)
        continue;
      Quat rest = target.bones[ti].restRot;
      int cr = child[r];
      // An ankle-to-toe vector describes the shoe's pivot geometry, not its
      // sole orientation. Aiming it at the source vector adds a constant tilt
      // even for neutral motion (different heels/toe heights). Transfer the
      // source foot rotation onto the calibrated game foot frame instead.
      const bool foot = r == 5 || r == 6;
      if (!foot && cr >= 0 && sourceRole_[cr] >= 0 && target.roles[cr] >= 0) {
        Vec3 a = tp(cr) - tp(r), b = basis_ * (sp(cr) - sp(r));
        if (Len(a) > 1e-5f && Len(b) > 1e-5f)
          rest = NormQ(Quat::FromTo(a, b) * rest);
      }
      // A finger's last phalanx has no next mapped joint to aim at. Keeping
      // its unaligned world rest frame cancels its parent's A/T correction,
      // creating an artificial backward bend (about 45 degrees for A-pose).
      // Carry the previous phalanx's REST correction through the real chain;
      // the sampled distal rotation is still applied independently below.
      bool terminalFinger = r >= 24 && r <= 53 && (r - 24) % 3 != 0 &&
          (cr < 0 || sourceRole_[cr] < 0 || target.roles[cr] < 0);
      if (terminalFinger && sourceRole_[r - 1] >= 0 && target.roles[r - 1] >= 0) {
        int sourceParent = source.bones[si].parent;
        while (sourceParent >= 0 && sourceParent != sourceRole_[r - 1])
          sourceParent = source.bones[sourceParent].parent;
        int targetParent = target.bones[ti].parent;
        while (targetParent >= 0 && targetParent != target.roles[r - 1])
          targetParent = target.bones[targetParent].parent;
        // Explicit mappings can point at unrelated bones. Do not treat such
        // mappings as an anatomical finger chain or guess from child names.
        if (sourceParent >= 0 && targetParent >= 0)
          rest = NormQ(alignedRest_[r - 1] *
                       Conj(target.bones[targetParent].restRot) * rest);
      }
      alignedRest_[r] = rest;
      std::set<int> seen;
      std::function<bool(int)> affects = [&](int i) {
        if (i < 0 || !seen.insert(i).second)
          return false;
        auto &b = source.bones[i];
        return eval_.hasTrack(i) || affects(b.parent) ||
               affects(b.grant);
      };
      // VMD omits neutral bones. A body clip still needs a coherent neutral
      // parent chain, including links driven exclusively by IK controllers.
      affected_[r] =
          affects(si) || (bodyMotion && r != 21 && r != 22 && r != 23);
    }
    output.localRot.resize(target.bones.size());
    output.worldRot.resize(target.bones.size());
    output.worldPos.resize(target.bones.size());
    output.worldMatrix.resize(target.bones.size());
    output.write.resize(target.bones.size());
    // Use the aligned source rest pose, not arbitrary game idle/calibration
    // rotations. Convert to local space through the real parent hierarchy.
    neutralLocal_.resize(target.bones.size());
    for (size_t i = 0; i < target.bones.size(); ++i) {
      const auto &b = target.bones[i];
      Quat parent = b.parent >= 0 ? output.worldRot[b.parent] : Quat{};
      bool mapped = b.role >= 0 && b.role < 55 &&
                    sourceRole_[b.role] >= 0 && affected_[b.role];
      Quat rest = mapped ? alignedRest_[b.role] : NormQ(parent * b.localRot);
      neutralLocal_[i] = mapped ? NormQ(Conj(parent) * rest) : b.localRot;
      output.worldRot[i] = rest;
    }
  }
  void sample(double frame, float scale, bool inPlace, float height,
              IkMode mode = IkMode::FollowMotion,
              const MotionAmplitude &amplitude = {}) {
    eval_.sample(frame, mode);
    output.legIkActive = {false, false};
    output.rootOffset = {};
    int hip = sourceRole_[0];
    if (hip >= 0 && affected_[0])
      output.rootOffset =
          basis_ * (eval_.pose.positions[hip] - source_->bones[hip].rest) *
          scale;
    if (inPlace) {
      output.rootOffset.x = 0;
      output.rootOffset.z = 0;
    }
    output.rootOffset.y += height;
    for (size_t i = 0; i < target_->bones.size(); i++) {
      auto &b = target_->bones[i];
      output.localRot[i] = b.localRot;
      output.write[i] = false;
      int r = b.role;
      if (r >= 0 && sourceRole_[r] >= 0 && affected_[r]) {
        Quat desired = NormQ(basis_ * eval_.pose.rotations[sourceRole_[r]] *
                             Conj(basis_) * alignedRest_[r]);
        Quat parent = b.parent >= 0 ? output.worldRot[b.parent] : Quat{};
        output.localRot[i] = NormQ(Conj(parent) * desired);
        output.write[i] = true;
      }
      output.worldRot[i] =
          b.parent >= 0 ? NormQ(output.worldRot[b.parent] * output.localRot[i])
                        : output.localRot[i];
      output.worldMatrix[i] =
          (b.parent >= 0 ? output.worldMatrix[b.parent] : Matrix{}) *
          TRS(b.localPos, output.localRot[i], b.localScale);
      output.worldPos[i] = output.worldMatrix[i].position();
    }
    if (hip < 0) {
      applyAmplitude(amplitude);
      return;
    }
    for (int side = 0; side < 2; side++) {
      int ar = side ? 2 : 1, br = side ? 4 : 3, cr = side ? 6 : 5;
      int a = target_->roles[ar], b = target_->roles[br],
          c = target_->roles[cr], sc = sourceRole_[cr], sb = sourceRole_[br],
          saIndex = sourceRole_[ar];
      std::string controller = side ? u8"右足IK" : u8"左足IK";
      if (a < 0 || b < 0 || c < 0 || sc < 0 || sb < 0 || saIndex < 0 ||
          !affected_[ar] ||
          !eval_.ikEnabled(source_->find(controller), frame, mode))
        continue;
      Vec3 pa = output.worldPos[a], pb = output.worldPos[b],
           pc = output.worldPos[c];
      Vec3 sourceUpper = eval_.pose.positions[sb] - eval_.pose.positions[saIndex];
      Vec3 sourceLower = eval_.pose.positions[sc] - eval_.pose.positions[sb];
      Vec3 sourceReach = sourceUpper + sourceLower;
      float upperLength = Len(pb - pa), lowerLength = Len(pc - pb);
      if (upperLength < 1e-5f || lowerLength < 1e-5f ||
          Len(sourceUpper) < 1e-5f || Len(sourceLower) < 1e-5f || Len(sourceReach) < 1e-5f)
        continue;
      // Transfer the source knee angle and hip-to-ankle direction onto the
      // target's actual thigh/shin lengths. Scaling the whole pelvis-to-foot
      // vector by root-motion units made targets unreachable and erased bends.
      float bendCos = Clamp(Dot(Norm(sourceUpper), Norm(sourceLower)), -1, 1);
      float reach = std::sqrt((std::max)(0.f, upperLength * upperLength +
          lowerLength * lowerLength + 2 * upperLength * lowerLength * bendCos));
      Vec3 goal = pa + (basis_ * Norm(sourceReach)) * reach;
      Vec3 pole = pa + (basis_ * Norm(sourceUpper)) * (upperLength + lowerLength);
      output.legIkActive[side] = true;
      Quat foot = output.worldRot[c];
      Vec3 sa = pa, sbpos = pb, scpos = pc;
      SolveTwoBone(sa, sbpos, scpos, goal, pole, true);
      setWorld(a,
               NormQ(Quat::FromTo(pb - pa, sbpos - sa) * output.worldRot[a]));
      setWorld(b, NormQ(Quat::FromTo(output.worldPos[c] - output.worldPos[b],
                                     scpos - sbpos) *
                        output.worldRot[b]));
      setWorld(c, foot);
    }
    applyAmplitude(amplitude);
  }
};
} // namespace mmd
