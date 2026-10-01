#pragma once
#include "direct_vmd_source_pmx.h"
#include <iomanip>
#include <locale>
#include <sstream>

namespace eiem_source {
struct Reference {
  float legLength = 10.62420198f;
  VmdVec3 lowerFromCenter{0, 4.74919f, -0.51217f};
  VmdVec3 grooveFromCenter{0, 0.2f, 0};
  VmdVec3 footFromParent[2]{{0, 0.79506f, 0}, {0, 0.79506f, 0}};
  VmdVec3 footFromAnkle[2]{}, toeFromToe[2]{};
  VmdVec3 direction[DIRECT_VMD_BONE_COUNT]{};
  VmdVec3 wristForward[2]{}, wristLateral[2]{};
  uint8_t controlled[DIRECT_VMD_BONE_COUNT]{};
  Reference() {
    for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i) {
      controlled[i] = 1;
      DirectVmdGetCanonicalSourceChildDirection(DirectVmdBoneId(i),
                                                &direction[i]);
    }
    DirectVmdGetCanonicalSourceWristFrameHints(
        DirectVmdBoneId::LeftWrist, &wristForward[0], &wristLateral[0]);
    DirectVmdGetCanonicalSourceWristFrameHints(
        DirectVmdBoneId::RightWrist, &wristForward[1], &wristLateral[1]);
  }
};
static_assert(std::is_trivially_copyable<Reference>::value,
              "Source reference must be POD-copyable");
struct Config {
  std::string name = "Tda Miku (default)", pmxPath;
  Reference reference;
  std::map<std::string, std::string> tracks, semantics;
};
inline int Semantic(const std::string &name) {
  for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i)
    if (name == kDirectVmdBoneSpecs[i].name ||
        (kDirectVmdBoneSpecs[i].alternateName &&
         name == kDirectVmdBoneSpecs[i].alternateName))
      return int(i);
  return -1;
}
inline bool DirectionChild(DirectVmdBoneId id, DirectVmdBoneId *child) {
  if (DirectVmdGetSemanticDirectionChild(id, child))
    return true;
  switch (id) {
  case DirectVmdBoneId::LeftLeg:
    *child = DirectVmdBoneId::LeftKnee;
    return true;
  case DirectVmdBoneId::LeftKnee:
    *child = DirectVmdBoneId::LeftAnkle;
    return true;
  case DirectVmdBoneId::LeftAnkle:
    *child = DirectVmdBoneId::LeftToe;
    return true;
  case DirectVmdBoneId::RightLeg:
    *child = DirectVmdBoneId::RightKnee;
    return true;
  case DirectVmdBoneId::RightKnee:
    *child = DirectVmdBoneId::RightAnkle;
    return true;
  case DirectVmdBoneId::RightAnkle:
    *child = DirectVmdBoneId::RightToe;
    return true;
  default:
    return false;
  }
}
inline void ValidateReference(const Reference &r) {
  Require(std::isfinite(r.legLength) && r.legLength > 0.001f &&
              r.legLength < 100000,
          "Invalid source leg length");
  for (VmdVec3 p : {r.lowerFromCenter, r.grooveFromCenter, r.footFromParent[0],
                    r.footFromParent[1], r.footFromAnkle[0], r.footFromAnkle[1],
                    r.toeFromToe[0], r.toeFromToe[1]})
    Require(Finite(p) && DirectVmdLength(p) < 100000,
            "Invalid source control offset");
  for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i) {
    Require(Finite(r.direction[i]), "Non-finite source direction");
    VmdVec3 canonical{}, unit{};
    if (r.controlled[i] && DirectVmdGetCanonicalSourceChildDirection(
                               DirectVmdBoneId(i), &canonical))
      Require(DirectVmdTryNormalizeVector(r.direction[i], &unit),
              "Missing source direction: " +
                  std::string(kDirectVmdBoneSpecs[i].name));
  }
  for (int side = 0; side < 2; ++side) {
    DirectVmdOrthonormalFrame f;
    const auto wrist =
        side ? DirectVmdBoneId::RightWrist : DirectVmdBoneId::LeftWrist;
    Require(!r.controlled[DirectVmdBoneIndex(wrist)] ||
                DirectVmdBuildOrthonormalFrame(r.wristForward[side],
                                               r.wristLateral[side], &f),
            "Degenerate source wrist frame");
  }
}
inline Config ParseConfig(const std::string &text) {
  Require(text.size() <= 65536, "Preset exceeds 64 KiB");
  Wide(text);
  Config c;
  std::istringstream in(text);
  in.imbue(std::locale::classic());
  std::string line;
  bool header = false;
  int lineNo = 0;
  std::set<std::string> fields;
  while (std::getline(in, line)) {
    ++lineNo;
    std::istringstream row(line);
    row.imbue(std::locale::classic());
    std::string op;
    row >> op;
    if (op.empty() || op[0] == '#')
      continue;
    if (!header) {
      int version = 0;
      row >> version;
      Require(op == "EIEM_SOURCE" && version == 1, "Expected EIEM_SOURCE 1");
      header = true;
    } else if (op == "track" || op == "map") {
      std::string a, b;
      row >> std::quoted(a) >> std::quoted(b);
      Require(!row.fail() && !a.empty() && !b.empty() && a.size() <= 512 &&
                  b.size() <= 512,
              "Invalid mapping rule");
      if (op == "map") {
        int i = Semantic(a);
        Require(i >= 0, "Unknown EIEM semantic: " + a);
        a = kDirectVmdBoneSpecs[i].name;
      }
      auto &rules = op == "track" ? c.tracks : c.semantics;
      Require(rules.emplace(a, b).second, "Duplicate mapping rule: " + a);
      Require(rules.size() <= 512, "Mapping rule budget exceeded");
    } else {
      std::string key = op;
      if (op == "name")
        row >> std::quoted(c.name);
      else if (op == "pmx")
        row >> std::quoted(c.pmxPath);
      else if (op == "leg_length")
        row >> c.reference.legLength;
      else if (op == "lower_from_center")
        row >> c.reference.lowerFromCenter.x >> c.reference.lowerFromCenter.y >>
            c.reference.lowerFromCenter.z;
      else if (op == "groove_from_center")
        row >> c.reference.grooveFromCenter.x >>
            c.reference.grooveFromCenter.y >> c.reference.grooveFromCenter.z;
      else if (op == "foot_from_parent" || op == "foot_from_ankle" ||
               op == "toe_from_toe" || op == "wrist_forward" ||
               op == "wrist_lateral") {
        int side = -1;
        row >> side;
        Require(side == 0 || side == 1, "Expected side 0/1");
        key += std::to_string(side);
        VmdVec3 &v =
            op == "foot_from_parent"
                ? c.reference.footFromParent[side]
                : (op == "foot_from_ankle"
                       ? c.reference.footFromAnkle[side]
                       : (op == "toe_from_toe"
                              ? c.reference.toeFromToe[side]
                              : (op == "wrist_forward"
                                     ? c.reference.wristForward[side]
                                     : c.reference.wristLateral[side])));
        row >> v.x >> v.y >> v.z;
      } else if (op == "direction") {
        std::string name;
        row >> std::quoted(name);
        int i = Semantic(name);
        Require(i >= 0, "Unknown direction semantic");
        key += std::to_string(i);
        auto &v = c.reference.direction[i];
        row >> v.x >> v.y >> v.z;
      } else
        Require(false, "Unknown preset field: " + op);
      Require(fields.insert(key).second, "Duplicate preset field: " + key);
    }
    Require(!row.fail(),
            "Invalid preset value at line " + std::to_string(lineNo));
    std::string extra;
    Require(!(row >> extra), "Unexpected trailing preset tokens at line " +
                                 std::to_string(lineNo));
  }
  Require(header, "Empty preset");
  Require(c.name.size() <= 256 && c.pmxPath.size() <= 4096,
          "Preset name/path too long");
  ValidateReference(c.reference);
  return c;
}
inline std::string Serialize(const Config &c) {
  std::ostringstream o;
  o.imbue(std::locale::classic());
  o << std::setprecision(9);
  o << "EIEM_SOURCE 1\nname " << std::quoted(c.name) << "\npmx "
    << std::quoted(c.pmxPath) << "\n";
  auto vec = [&](const std::string &key, VmdVec3 v) {
    o << key << ' ' << v.x << ' ' << v.y << ' ' << v.z << '\n';
  };
  o << "leg_length " << c.reference.legLength << '\n';
  vec("lower_from_center", c.reference.lowerFromCenter);
  vec("groove_from_center", c.reference.grooveFromCenter);
  for (int side = 0; side < 2; ++side) {
    const auto n = std::to_string(side);
    vec("foot_from_parent " + n, c.reference.footFromParent[side]);
    vec("foot_from_ankle " + n, c.reference.footFromAnkle[side]);
    vec("toe_from_toe " + n, c.reference.toeFromToe[side]);
    vec("wrist_forward " + n, c.reference.wristForward[side]);
    vec("wrist_lateral " + n, c.reference.wristLateral[side]);
  }
  for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i)
    if (DirectVmdLength(c.reference.direction[i]) > 0)
      vec("direction \"" + std::string(kDirectVmdBoneSpecs[i].name) + "\"",
          c.reference.direction[i]);
  for (const auto &r : c.tracks)
    o << "track " << std::quoted(r.first) << ' ' << std::quoted(r.second)
      << '\n';
  for (const auto &r : c.semantics)
    o << "map " << std::quoted(r.first) << ' ' << std::quoted(r.second) << '\n';
  return o.str();
}
struct Prepared {
  Config config;
  Reference reference;
  PmxSkeleton skeleton;
  std::map<std::string, int> names;
  int semantic[DIRECT_VMD_BONE_COUNT]{};
  int landmark[DIRECT_VMD_BONE_COUNT]{};
  std::vector<uint8_t> needed;
  std::string report;
  uint64_t revision = 1;
  bool pmx = false;
};
inline Prepared Prepare(const Config &config) {
  Prepared p;
  p.config = config;
  p.reference = config.reference;
  p.pmx = !config.pmxPath.empty();
  if (p.pmx) {
    p.skeleton = ParsePmx(ReadBytes(config.pmxPath, 256ull * 1024 * 1024));
    for (size_t i = 0; i < p.skeleton.bones.size(); ++i)
      p.names.emplace(p.skeleton.bones[i].name, int(i));
    for (size_t i = 0; i < p.skeleton.bones.size(); ++i) {
      const auto &n = p.skeleton.bones[i].english;
      if (n.empty() || n == p.skeleton.bones[i].name)
        continue;
      auto a = p.names.emplace(n, int(i));
      if (!a.second && a.first->second != int(i))
        a.first->second = -2;
    }
  } else {
    for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i) {
      p.names.emplace(kDirectVmdBoneSpecs[i].name, int(i));
      if (kDirectVmdBoneSpecs[i].alternateName)
        p.names.emplace(kDirectVmdBoneSpecs[i].alternateName, int(i));
    }
  }
  std::set<int> outputs;
  for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i) {
    const auto &spec = kDirectVmdBoneSpecs[i];
    const auto rule = config.semantics.find(spec.name);
    const bool disabled = rule != config.semantics.end() && rule->second == "-";
    std::string name =
        rule == config.semantics.end() || disabled ? spec.name : rule->second;
    auto found = p.names.find(name);
    if (found == p.names.end() &&
        (rule == config.semantics.end() || disabled) && spec.alternateName)
      found = p.names.find(spec.alternateName);
    if (found == p.names.end()) {
      Require(rule == config.semantics.end() || disabled,
              "Unknown source bone: " + name);
      p.semantic[i] = p.landmark[i] = -1;
      p.reference.controlled[i] = 0;
      continue;
    }
    Require(found->second >= 0, "Ambiguous source alias: " + name);
    p.landmark[i] = found->second;
    if (disabled) {
      p.semantic[i] = -1;
      p.reference.controlled[i] = 0;
      continue;
    }
    p.semantic[i] = found->second;
    Require(outputs.insert(found->second).second,
            "One source bone mapped to multiple EIEM outputs: " + name);
  }
  for (const auto &r : config.tracks) {
    if (r.second == "-")
      continue;
    auto found = p.names.find(r.second);
    bool semi = false;
    if (!p.pmx)
      for (const auto &s : kDirectVmdSemiStandardSpecs)
        if (r.second == s.name ||
            (s.alternateName && r.second == s.alternateName))
          semi = true;
    Require(semi || (found != p.names.end() && found->second >= 0),
            "Unknown/ambiguous track destination: " + r.second);
  }
  if (!p.pmx) {
    const Reference original = p.reference;
    for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i) {
      const int source = p.semantic[i];
      if (source < 0 || source == int(i))
        continue;
      const bool control = i <= uint32_t(DirectVmdBoneId::Groove) ||
                           (i >= uint32_t(DirectVmdBoneId::LeftFootIkParent) &&
                            i <= uint32_t(DirectVmdBoneId::RightToeIk));
      Require(!control,
              "Canonical Root/IK remapping requires a PMX skeleton reference");
      p.reference.direction[i] = original.direction[source];
      if (i == uint32_t(DirectVmdBoneId::LeftWrist) ||
          i == uint32_t(DirectVmdBoneId::RightWrist)) {
        Require(source == int(DirectVmdBoneId::LeftWrist) ||
                    source == int(DirectVmdBoneId::RightWrist),
                "Mapped wrist source has no complete canonical hand frame");
        const int side = i == uint32_t(DirectVmdBoneId::LeftWrist) ? 0 : 1;
        const int from = source == int(DirectVmdBoneId::LeftWrist) ? 0 : 1;
        p.reference.wristForward[side] = original.wristForward[from];
        p.reference.wristLateral[side] = original.wristLateral[from];
      }
    }
    ValidateReference(p.reference);
    p.report = "Canonical source; existing semi-standard folding retained.";
    return p;
  }
  auto &bones = p.skeleton.bones;
  p.needed.resize(bones.size());
  for (int i : p.semantic)
    if (i >= 0)
      p.needed[size_t(i)] = 1;
  for (auto it = p.skeleton.order.rbegin(); it != p.skeleton.order.rend(); ++it)
    if (p.needed[*it])
      for (int d : {bones[*it].parent, bones[*it].append})
        if (d >= 0)
          p.needed[d] = 1;
  size_t fixedAxes = 0;
  for (size_t i = 0; i < bones.size(); ++i)
    if (p.needed[i]) {
      auto &b = bones[i];
      Require(!(b.flags & 0x3000),
              "Unsupported physics-after/external-parent bone: " + b.name);
      if (b.flags & 0x400) {
        VmdVec3 axis{};
        Require(Finite(b.fixedAxis) &&
                    DirectVmdTryNormalizeVector(b.fixedAxis, &axis),
                "Invalid fixed-axis direction: " + b.name);
        b.fixedAxis = axis;
        ++fixedAxes;
      }
      Require(!p.skeleton.physicsBones.count(int(i)),
              "Physics-driven output dependency: " + b.name);
      Require(!p.skeleton.morphBones.count(int(i)),
              "Bone morph affects output dependency: " + b.name);
    }
  auto at = [&](DirectVmdBoneId id) -> int {
    return p.landmark[DirectVmdBoneIndex(id)];
  };
  auto pos = [&](DirectVmdBoneId id) -> VmdVec3 {
    const int i = at(id);
    Require(i >= 0,
            "PMX reference requires landmark: " +
                std::string(kDirectVmdBoneSpecs[DirectVmdBoneIndex(id)].name));
    return bones[i].position;
  };
  const auto L = DirectVmdBoneId::LeftLeg, R = DirectVmdBoneId::RightLeg;
  for (int side = 0; side < 2; ++side) {
    const auto &chain = kDirectVmdPhase5LegFkBones[side];
    for (int segment = 0; segment < 2; ++segment)
      Require(DirectVmdLength(DirectVmdSub(pos(chain[segment + 1]),
                                           pos(chain[segment]))) > 1e-5f,
              "Degenerate source leg segment");
  }
  p.reference.legLength =
      0.5f *
      (DirectVmdLength(DirectVmdSub(pos(L), pos(DirectVmdBoneId::LeftKnee))) +
       DirectVmdLength(DirectVmdSub(pos(DirectVmdBoneId::LeftKnee),
                                    pos(DirectVmdBoneId::LeftAnkle))) +
       DirectVmdLength(DirectVmdSub(pos(R), pos(DirectVmdBoneId::RightKnee))) +
       DirectVmdLength(DirectVmdSub(pos(DirectVmdBoneId::RightKnee),
                                    pos(DirectVmdBoneId::RightAnkle))));
  p.reference.lowerFromCenter = DirectVmdSub(pos(DirectVmdBoneId::LowerBody),
                                             pos(DirectVmdBoneId::Center));
  p.reference.grooveFromCenter =
      at(DirectVmdBoneId::Groove) >= 0
          ? DirectVmdSub(pos(DirectVmdBoneId::Groove),
                         pos(DirectVmdBoneId::Center))
          : VmdVec3{};
  for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i) {
    DirectVmdBoneId child;
    if (p.reference.controlled[i] && DirectionChild(DirectVmdBoneId(i), &child))
      p.reference.direction[i] =
          DirectVmdSub(pos(child), pos(DirectVmdBoneId(i)));
  }
  for (int side = 0; side < 2; ++side) {
    const auto wrist =
        side ? DirectVmdBoneId::RightWrist : DirectVmdBoneId::LeftWrist;
    if (p.reference.controlled[DirectVmdBoneIndex(wrist)]) {
      p.reference.wristForward[side] =
          DirectVmdSub(pos(side ? DirectVmdBoneId::RightMiddle1
                                : DirectVmdBoneId::LeftMiddle1),
                       pos(wrist));
      p.reference.wristLateral[side] =
          DirectVmdSub(pos(side ? DirectVmdBoneId::RightIndex1
                                : DirectVmdBoneId::LeftIndex1),
                       pos(side ? DirectVmdBoneId::RightLittle1
                                : DirectVmdBoneId::LeftLittle1));
      p.reference.direction[DirectVmdBoneIndex(wrist)] =
          p.reference.wristForward[side];
    }
    const auto ik =
        side ? DirectVmdBoneId::RightFootIk : DirectVmdBoneId::LeftFootIk;
    const auto parent = side ? DirectVmdBoneId::RightFootIkParent
                             : DirectVmdBoneId::LeftFootIkParent;
    p.reference.footFromParent[side] = at(parent) >= 0 && at(ik) >= 0
                                           ? DirectVmdSub(pos(ik), pos(parent))
                                           : VmdVec3{};
    const auto toe =
        side ? DirectVmdBoneId::RightToeIk : DirectVmdBoneId::LeftToeIk;
    p.reference.footFromAnkle[side] =
        at(ik) >= 0
            ? DirectVmdSub(pos(ik), pos(side ? DirectVmdBoneId::RightAnkle
                                             : DirectVmdBoneId::LeftAnkle))
            : VmdVec3{};
    p.reference.toeFromToe[side] =
        at(toe) >= 0
            ? DirectVmdSub(pos(toe), pos(side ? DirectVmdBoneId::RightToe
                                              : DirectVmdBoneId::LeftToe))
            : VmdVec3{};
  }
  int delegated = 0;
  for (size_t i = 0; i < bones.size(); ++i)
    if (bones[i].flags & 0x20) {
      const auto &b = bones[i];
      bool affects = p.needed[i] != 0;
      for (const auto &l : b.links)
        affects = affects || p.needed[l.bone] != 0;
      if (!affects)
        continue;
      bool allowed = false;
      for (int side = 0; side < 2; ++side) {
        const int leg = at(side ? DirectVmdBoneId::RightLeg
                                : DirectVmdBoneId::LeftLeg),
                  knee = at(side ? DirectVmdBoneId::RightKnee
                                 : DirectVmdBoneId::LeftKnee),
                  ankle = at(side ? DirectVmdBoneId::RightAnkle
                                  : DirectVmdBoneId::LeftAnkle);
        if (int(i) == at(side ? DirectVmdBoneId::RightFootIk
                              : DirectVmdBoneId::LeftFootIk))
          allowed = b.ikTarget == ankle && b.links.size() == 2 &&
                    b.links[0].bone == knee && b.links[1].bone == leg;
        if (int(i) ==
            at(side ? DirectVmdBoneId::RightToeIk : DirectVmdBoneId::LeftToeIk))
          allowed = b.ikTarget == at(side ? DirectVmdBoneId::RightToe
                                          : DirectVmdBoneId::LeftToe) &&
                    b.links.size() == 1 && b.links[0].bone == ankle;
      }
      Require(allowed, "Unsupported PMX IK affecting output: " + b.name);
      ++delegated;
      for (size_t j = 0; j < bones.size(); ++j)
        if (p.needed[j] && bones[j].append >= 0)
          for (const auto &l : b.links)
            Require(bones[j].append != l.bone,
                    "Append depends on source IK result: " + bones[j].name);
    }
  ValidateReference(p.reference);
  p.report = "PMX FK/append evaluated once; " + std::to_string(delegated) +
             " standard IK controllers delegated to FinalIK/Toe Aim (PMX "
             "iteration/limits not reproduced); " + std::to_string(fixedAxes) +
             " fixed-axis bones supported by source key axis conversion. "
             "Local editing axes do not change VMD rotation basis. "
             "Bone morph/physics output dependencies rejected.";
  return p;
}
}
