#pragma once
#include "math/mmd_rig.h"
#include "math/mmd_amplitude.h"
#include "nlohmann/json.hpp"

namespace mmd {
struct NativeClothPreset { float hipRadius = .124f; bool enhancement = true; int geometry = 1; float ribbonDamping = .3f; };
inline nlohmann::json NativeClothJson(const NativeClothPreset &c) {
  return {{"hip_radius", c.hipRadius}, {"enhancement", c.enhancement}, {"geometry", c.geometry},
          {"ribbon_damping", c.ribbonDamping}};
}
inline NativeClothPreset ReadNativeCloth(const nlohmann::json &preset) {
  NativeClothPreset c;
  if (!preset.contains("native_cloth")) return c;
  const auto &j = preset.at("native_cloth");
  if (!j.is_object()) throw std::runtime_error(u8"无效的衣物物理设置");
  c.hipRadius = j.value("hip_radius", .124f);
  if (!std::isfinite(c.hipRadius) || c.hipRadius < 0 || c.hipRadius > .25f)
    throw std::runtime_error(u8"腿根半径补偿超出范围");
  c.enhancement = j.value("enhancement", true);
  c.geometry = j.value("geometry", 1);
  if (c.geometry < 0 || c.geometry > 1) throw std::runtime_error(u8"无效的碰撞体尺寸模式");
  c.ribbonDamping = j.value("ribbon_damping", .3f);
  if (!std::isfinite(c.ribbonDamping) || c.ribbonDamping < 0 || c.ribbonDamping > 1)
    throw std::runtime_error(u8"飘带减振必须在 0–100% 之间");
  return c;
}
inline const std::array<const char *, int(MotionPart::Count)> &MotionPartKeys() {
  static const std::array<const char *, int(MotionPart::Count)> keys = {
    "torso", "head", "left_arm", "right_arm", "left_hand", "right_hand",
    "left_leg", "right_leg", "left_foot", "right_foot"};
  return keys;
}
inline nlohmann::json AmplitudeJson(const MotionAmplitude &a) {
  nlohmann::json j = {{"master", a.master}};
  for (size_t i = 0; i < a.parts.size(); ++i) j[MotionPartKeys()[i]] = a.parts[i];
  return j;
}
inline MotionAmplitude ReadAmplitude(const nlohmann::json &preset) {
  MotionAmplitude a;
  if (!preset.contains("motion_amplitude")) return a;
  const auto &j = preset.at("motion_amplitude");
  if (!j.is_object()) throw std::runtime_error(u8"无效的动作幅度设置");
  auto value = [&](const char *key) {
    float f = j.value(key, 1.f);
    if (!std::isfinite(f) || f < 0 || f > 2)
      throw std::runtime_error(u8"动作幅度必须在 0–200% 之间");
    return f;
  };
  a.master = value("master");
  for (size_t i = 0; i < a.parts.size(); ++i) a.parts[i] = value(MotionPartKeys()[i]);
  return a;
}
// These are virtual source bones. Nothing is inserted into the game's rig.
struct ExtraBone {
  std::string name, parent, anchor, before, grant;
  Vec3 offset;
  float grantWeight = 1;
  bool grantRotation = false, grantPosition = false;
};
struct RigAdaptation {
  bool controlRoot = false, upperBody1 = false, waistCancel = false,
       legD = false;
  bool parentRoot = false, groove = false, upperBody2 = false,
       shoulderCancel = false, twists = false, ikParents = false;
  std::vector<ExtraBone> extra;
  // Destination source-bone -> input VMD track; empty explicitly mutes it.
  std::map<std::string, std::string> tracks;
  // Unity human role -> evaluated source-bone; empty leaves that role alone.
  std::map<int, std::string> roles;
};
inline RigAdaptation DefaultRigAdaptation() {
  RigAdaptation a;
  a.controlRoot=a.upperBody1=a.waistCancel=a.legD=true;
  a.parentRoot=a.groove=a.upperBody2=a.shoulderCancel=a.twists=a.ikParents=true;
  return a;
}
inline void ValidateAdaptation(const RigAdaptation &a) {
  if (a.extra.size() > 256 || a.tracks.size() > 8192 || a.roles.size() > 55)
    throw std::runtime_error(u8"适配配置项目过多");
  auto name = [](const std::string &s) {
    if (s.size() > 240 || s.find('\0') != s.npos)
      throw std::runtime_error(u8"骨骼或轨道名称过长或含空字符");
  };
  std::set<std::string> added;
  for (const auto &b : a.extra) {
    for (const auto *s : {&b.name, &b.parent, &b.anchor, &b.before, &b.grant})
      name(*s);
    if (b.name.empty() || !added.insert(Name(b.name)).second)
      throw std::runtime_error(u8"追加骨骼名称为空或重复");
    for (float v : {b.offset.x, b.offset.y, b.offset.z, b.grantWeight})
      if (!std::isfinite(v) || std::fabs(v) > 10000)
        throw std::runtime_error(u8"追加骨骼数值无效");
  }
  for (const auto &kv : a.tracks) {
    name(kv.first);
    name(kv.second);
    if (kv.first.empty())
      throw std::runtime_error(u8"轨道映射缺少目标源骨骼");
  }
  for (const auto &kv : a.roles) {
    name(kv.second);
    if (kv.first < 0 || kv.first >= 55)
      throw std::runtime_error(u8"角色部位编号无效");
  }
}
inline RigDefinition AdaptRig(const RigDefinition &base,
                              const RigAdaptation &a) {
  ValidateAdaptation(a);
  RigDefinition r = base;
  auto require = [&](const std::string &name) {
    int i = r.find(name);
    if (i < 0)
      throw std::runtime_error(u8"适配所需骨骼不存在：" + name);
    return i;
  };
  auto add = [&](std::string name, int parent, Vec3 rest) {
    int i = r.find(name);
    if (i >= 0)
      return i; // Existing PMX definitions take precedence.
    RigBone b;
    b.name = Name(name);
    b.parent = parent;
    b.rest = rest;
    i = int(r.bones.size());
    r.names[b.name] = i;
    r.bones.push_back(b);
    return i;
  };
  auto insert = [&](const std::string &name, const std::string &child,
                    Vec3 rest) {
    int i = r.find(name);
    if (i >= 0)
      return i;
    int c = require(child);
    i = add(name, r.bones[c].parent, rest);
    r.bones[c].parent = i;
    return i;
  };
  // A view-center bone is independent in the reference plugin. Do not make
  // it a motion root merely because its name contains "center".
  if (a.controlRoot)
    add(u8"操作中心", -1, {});
  if (a.parentRoot && r.find(u8"全ての親") < 0) {
    int root = add(u8"全ての親", -1, {});
    for (int i = 0; i < root; ++i)
      if (r.bones[i].parent < 0 && r.bones[i].name != u8"操作中心")
        r.bones[i].parent = root;
  }
  auto insertChildren = [&](const std::string &name, int parent, Vec3 rest) {
    if (r.find(name) >= 0)
      return;
    int n = add(name, parent, rest);
    for (int i = 0; i < n; ++i)
      if (r.bones[i].parent == parent)
        r.bones[i].parent = n;
  };
  if (a.groove && r.find(u8"グルーブ") < 0) {
    int center = require(u8"センター");
    insertChildren(u8"グルーブ", center,
                   r.bones[center].rest + Vec3{0, .2f, 0});
  }
  if (a.upperBody2 && r.find(u8"上半身2") < 0) {
    int body = require(u8"上半身"), neck = require(u8"首");
    insertChildren(u8"上半身2", body,
                   r.bones[body].rest +
                       (r.bones[neck].rest - r.bones[body].rest) * .35f);
  }
  if (a.waistCancel && r.find(u8"腰") < 0) {
    int lower = require(u8"下半身"), leg = require(u8"右足");
    Vec3 p = r.bones[lower].rest;
    p.y += (r.bones[leg].rest.y - p.y) * .6f;
    int parent = r.bones[lower].parent;
    int waist = add(u8"腰", parent, p);
    for (auto part : {u8"上半身", u8"下半身"}) {
      int i = require(part);
      if (r.bones[i].parent == parent)
        r.bones[i].parent = waist;
    }
  }
  if (a.upperBody1) {
    Vec3 p = (r.bones[require(u8"上半身")].rest +
              r.bones[require(u8"上半身2")].rest) *
             .5f;
    insert(u8"上半身1", u8"上半身2", p);
  }
  for (auto side : {std::string(u8"左"), std::string(u8"右")}) {
    if (a.shoulderCancel) {
      int shoulder = require(side + u8"肩"), arm = require(side + u8"腕");
      int p = insert(side + u8"肩P", side + u8"肩", r.bones[shoulder].rest);
      if (r.find(side + u8"肩C") < 0) {
        int c = insert(side + u8"肩C", side + u8"腕", r.bones[arm].rest);
        r.bones[c].grant = p;
        r.bones[c].grantWeight = -1;
        r.bones[c].grantRotation = true;
        r.bones[c].grantLocal = true;
      }
    }
    if (a.twists)
      for (int hand = 0; hand < 2; ++hand) {
        int p = require(side + (hand ? u8"ひじ" : u8"腕"));
        std::string child = side + (hand ? u8"手首" : u8"ひじ");
        int c = require(child);
        std::string n = side + (hand ? u8"手捩" : u8"腕捩");
        Vec3 delta = r.bones[c].rest - r.bones[p].rest;
        bool existed = r.find(n) >= 0;
        int twist = insert(n, child, r.bones[p].rest + delta * .6f);
        if (!existed) {
          r.bones[twist].fixed = true;
          r.bones[twist].axis = Norm(delta);
        }
        for (int j = 1; j <= 3; ++j) {
          std::string sub = n + std::to_string(j);
          if (r.find(sub) >= 0)
            continue;
          int i = add(sub, p, r.bones[p].rest + delta * (j * .25f));
          r.bones[i].grant = twist;
          r.bones[i].grantWeight = j * .25f;
          r.bones[i].grantRotation = true;
          r.bones[i].grantLocal = true;
        }
      }
    if (a.ikParents) {
      int ik = require(side + u8"足IK");
      Vec3 p = r.bones[ik].rest;
      p.y = 0;
      insert(side + u8"足IK親", side + u8"足IK", p);
    }
    if (a.waistCancel) {
      std::string n = u8"腰キャンセル" + side;
      if (r.find(n) < 0) {
        int waist = require(u8"腰");
        int i = insert(n, side + u8"足", r.bones[require(side + u8"足")].rest);
        r.bones[i].grant = waist;
        r.bones[i].grantWeight = -1;
        r.bones[i].grantRotation = true;
        r.bones[i].grantLocal = true;
      }
    }
    if (a.legD) {
      int parent = r.bones[require(side + u8"足")].parent;
      for (auto part : {u8"足", u8"ひざ", u8"足首"}) {
        std::string original = side + part, n = original + "D";
        int s = require(original), i = r.find(n);
        if (i < 0) {
          i = add(n, parent, r.bones[s].rest);
          r.bones[i].grant = s;
          r.bones[i].grantWeight = 1;
          r.bones[i].grantRotation = true;
          r.bones[i].grantLocal = true;
        }
        parent = i;
      }
      int toe = r.find(side + u8"つま先IK");
      if (toe < 0)
        toe = require(side + u8"つま先");
      Vec3 rest = r.bones[parent].rest +
                  (r.bones[toe].rest - r.bones[parent].rest) * (2.f / 3);
      add(side + u8"足先EX", parent, rest);
    }
  }
  // Resolve custom rest anchors recursively, permitting forward references.
  const size_t begin = r.bones.size();
  for (const auto &b : a.extra) {
    if (r.find(b.name) >= 0)
      throw std::runtime_error(u8"追加骨骼已存在：" + b.name);
    add(b.name, -1, {});
  }
  std::vector<uint8_t> marks(a.extra.size());
  std::function<void(size_t)> rest = [&](size_t k) {
    if (marks[k] == 2)
      return;
    if (marks[k] == 1)
      throw std::runtime_error(u8"静止位置参考存在循环");
    marks[k] = 1;
    const auto &e = a.extra[k];
    Vec3 p{};
    if (!e.anchor.empty()) {
      int i = require(e.anchor);
      if (size_t(i) >= begin)
        rest(size_t(i) - begin);
      p = r.bones[i].rest;
    }
    r.bones[begin + k].rest = p + e.offset;
    marks[k] = 2;
  };
  std::set<int> reparented;
  for (size_t k = 0; k < a.extra.size(); ++k) {
    const auto &e = a.extra[k];
    auto &b = r.bones[begin + k];
    b.parent = e.parent.empty() ? -1 : require(e.parent);
    if (!e.before.empty()) {
      int child = require(e.before);
      if (!reparented.insert(child).second)
        throw std::runtime_error(u8"同一骨骼不能同时插入多个父级");
      if (e.parent.empty())
        b.parent = r.bones[child].parent;
      r.bones[child].parent = int(begin + k);
    }
    if (!e.grant.empty()) {
      b.grant = require(e.grant);
      b.grantWeight = e.grantWeight;
      b.grantRotation = e.grantRotation;
      b.grantPosition = e.grantPosition;
      b.grantLocal = true;
    }
    rest(k);
  }
  r.finish(); // Reject parent/grant cycles before replacing a usable rig.
  for (const auto &kv : a.tracks)
    require(kv.first);
  for (const auto &kv : a.roles)
    if (!kv.second.empty())
      require(kv.second);
  return r;
}
inline std::map<int, std::string> AdaptedRoles(const RigAdaptation &a) {
  std::map<int, std::string> roles;
  if (a.legD) {
    for (int side = 0; side < 2; ++side) {
      std::string s = side ? u8"右" : u8"左";
      roles[1 + side] = s + u8"足D";
      roles[3 + side] = s + u8"ひざD";
      roles[5 + side] = s + u8"足首D";
      roles[19 + side] = s + u8"足先EX";
    }
  }
  for (const auto &kv : a.roles)
    roles[kv.first] = kv.second;
  return roles;
}
inline nlohmann::json AdaptationJson(const RigAdaptation &a, int sourcePreset,
                                     IkMode ik) {
  ValidateAdaptation(a);
  nlohmann::json j = {{"version", 1},
                      {"source_pose", sourcePreset},
                      {"ik_mode", int(ik)},
                      {"control_root", a.controlRoot},
                      {"upper_body_1", a.upperBody1},
                      {"waist_cancel", a.waistCancel},
                      {"leg_d_toe_ex", a.legD},
                      {"tracks", a.tracks},
                      {"parent_root", a.parentRoot},
                      {"groove", a.groove},
                      {"upper_body_2", a.upperBody2},
                      {"shoulder_cancel", a.shoulderCancel},
                      {"twists", a.twists},
                      {"ik_parents", a.ikParents},
                      {"roles", nlohmann::json::array()},
                      {"extra_bones", nlohmann::json::array()}};
  for (const auto &kv : a.roles)
    j["roles"].push_back({{"role", kv.first}, {"source", kv.second}});
  for (const auto &b : a.extra)
    j["extra_bones"].push_back(
        {{"name", b.name},
         {"parent", b.parent},
         {"anchor", b.anchor},
         {"before", b.before},
         {"offset", {b.offset.x, b.offset.y, b.offset.z}},
         {"grant", b.grant},
         {"grant_weight", b.grantWeight},
         {"grant_rotation", b.grantRotation},
         {"grant_position", b.grantPosition}});
  return j;
}
inline RigAdaptation ReadAdaptation(const nlohmann::json &j, int &sourcePreset,
                                    IkMode &ik) {
  if (!j.is_object() || j.at("version").get<int>() != 1)
    throw std::runtime_error(u8"不支持的适配预设格式");
  int pose = j.value("source_pose", 0), mode = j.value("ik_mode", 0);
  if (pose < 0 || pose > 1 || mode < 0 || mode > 2)
    throw std::runtime_error(u8"无效的姿态或 IK 模式");
  RigAdaptation a;
  a.controlRoot = j.value("control_root", false);
  a.upperBody1 = j.value("upper_body_1", false);
  a.waistCancel = j.value("waist_cancel", false);
  a.legD = j.value("leg_d_toe_ex", false);
  a.parentRoot = j.value("parent_root", false);
  a.groove = j.value("groove", false);
  a.upperBody2 = j.value("upper_body_2", false);
  a.shoulderCancel = j.value("shoulder_cancel", false);
  a.twists = j.value("twists", false);
  a.ikParents = j.value("ik_parents", false);
  if (j.contains("tracks")) {
    for (auto it = j.at("tracks").begin(); it != j.at("tracks").end(); ++it)
      if (!a.tracks.emplace(Name(it.key()), Name(it.value().get<std::string>()))
               .second)
        throw std::runtime_error(u8"重复的轨道映射");
  }
  if (j.contains("roles"))
    for (const auto &v : j.at("roles")) {
      if (!a.roles
               .emplace(v.at("role").get<int>(),
                        Name(v.at("source").get<std::string>()))
               .second)
        throw std::runtime_error(u8"重复的角色部位映射");
    }
  if (j.contains("extra_bones"))
    for (const auto &v : j.at("extra_bones")) {
      ExtraBone b;
      b.name = Name(v.at("name").get<std::string>());
      b.parent = Name(v.value("parent", ""));
      b.anchor = Name(v.value("anchor", ""));
      b.before = Name(v.value("before", ""));
      auto p = v.at("offset").get<std::array<float, 3>>();
      b.offset = {p[0], p[1], p[2]};
      b.grant = Name(v.value("grant", ""));
      b.grantWeight = v.value("grant_weight", 1.f);
      b.grantRotation = v.value("grant_rotation", false);
      b.grantPosition = v.value("grant_position", false);
      a.extra.push_back(b);
    }
  ValidateAdaptation(a);
  sourcePreset = pose;
  ik = static_cast<IkMode>(mode);
  return a;
}
} // namespace mmd
