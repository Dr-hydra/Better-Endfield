#pragma once

// 按角色记住「冻结 + 姿态」。
//   * 切走：把当前角色的姿态存进内存表（用骨名，不存 Transform 指针）；
//     释放旧实例的冻结写者，避免继续访问被销毁或回收的组件；
//   * 切到没冻过的角色：保持游戏默认表现（不冻结）——这是期望的"新角色是默认状态"；
//   * 切回冻过的角色：重新采集并压制它的写者，再把存下的姿态按骨名应用回去。
// 姿态取自内存中的快照值（s_humanBones/s_accessoryBones 的 localPos/localRot），
// 由写骨钩子保持更新，所以切换时不依赖旧角色的 Transform 是否还活着。

#include "game/accessory.h"
#include "game/freeze.h"
#include "game/skeleton.h"
#include "math/pose_file.h"

#include <map>
#include <string>

struct CharFreezeState {
  bool frozen = false;
  PoseDoc pose;
};

static std::map<std::string, CharFreezeState> g_charStates;
static std::string g_curCharKey; // 当前角色 key（切走时用它存表）

// 角色标识：取 Animator 所在物体名，去掉 "(Clone)#NN" 这类实例后缀
// （实例编号每次生成都变，不能作为身份；模型名在同一角色间稳定）
// __try 不能和需要对象展开的 C++ 对象共存（C2712），所以读取单独放这个小函数里
static void ReadAnimatorGoName(char *buf, int sz) {
  buf[0] = 0;
  if (!g_charAnimator || !g_component_get_gameObject || !g_object_get_name)
    return;
  __try {
    void *go = Invoke(g_component_get_gameObject, g_charAnimator);
    void *ns = go ? Invoke(g_object_get_name, go) : nullptr;
    if (ns)
      ReadStrUtf8(ns, buf, sz);
  } __except (1) {
  }
}

static std::string CurrentCharModelKey() {
  char buf[128] = "";
  ReadAnimatorGoName(buf, sizeof(buf));
  std::string s = buf;
  size_t p = s.find('(');
  if (p != std::string::npos)
    s = s.substr(0, p);
  while (!s.empty() && (s.back() == ' ' || s.back() == '_' || s.back() == '#'))
    s.pop_back();
  return s;
}

// 角色实例级 key：模型名 + Animator 指针。
// 只用模型名会在"同屏两个同名角色"时串味（两个实例共用一份冻结状态/姿势），
// 加上实例指针就能把它们分开。状态表只在内存里，不落盘，所以不担心跨版本兼容。
static std::string CurrentCharKey() {
  std::string k = CurrentCharModelKey();
  if (g_charAnimator) {
    char b[32] = {};
    snprintf(b, sizeof(b), "@%p", g_charAnimator);
    k += b;
  }
  return k;
}

// 把内存里的快照打包成 PoseDoc（不访问游戏对象，安全用于换角色时）
static PoseDoc BuildLivePoseDoc(const char *name) {
  PoseDoc doc;
  doc.name = name ? name : "";
  doc.restRel = false;
  for (int i = 0; i < s_humanBoneCount; i++) {
    if (g_poseSkipFaceBones && IsFaceExpressionBone(s_humanBones[i].humanBone))
      continue; // 表情骨不入姿态（跨角色会乱）
    PoseBone pb;
    pb.name = s_humanBones[i].name;
    pb.pos = s_humanBones[i].localPos; // 由写骨钩子保持更新
    pb.rot = s_humanBones[i].localRot;
    doc.bones.push_back(pb);
  }
  for (const AccessoryBone &b : s_accessoryBones) {
    if (IsNoisyBoneName(b.name))
      continue; // 内部辅助骨同理
    PoseBone pb;
    pb.name = b.name;
    pb.pos = b.localPos;
    pb.rot = b.localRot;
    doc.accBones.push_back(pb);
  }
  return doc;
}

// 角色切换：先保存"旧角色"的状态（必须在重建骨骼列表之前调用）
static void SaveCharStateOnSwitch() {
  // 存"正在离开的那个角色"：优先用上次恢复时记下的实例 key（那时 g_charAnimator 还是它），
  // 没记过就用当前指针现算（切换是我们主动发起时仍指向旧角色）。
  std::string key = !g_curCharKey.empty() ? g_curCharKey : CurrentCharKey();
  Log("[CHAR] switch-save: key='%s' frozen=%d states=%d", key.c_str(),
      (int)g_frozen, (int)g_charStates.size());
  if (key.empty())
    return;
  if (!g_frozen) {
    g_charStates.erase(key); // 没冻结就不记（切回来保持默认）
    return;
  }
  CharFreezeState st;
  st.frozen = true;
  st.pose = BuildLivePoseDoc(key.c_str());
  g_charStates[key] = st;
  Log("[CHAR] saved frozen state for '%s' (%d bones, %d accessory)",
      key.c_str(), (int)st.pose.bones.size(), (int)st.pose.accBones.size());
}

// 角色切换：新角色已重建完成后调用；冻过就恢复，否则保持默认
static void RestoreCharStateOnSwitch() {
  g_curCharKey = CurrentCharKey(); // 实例级 key（模型名 + Animator 指针）
  auto it = g_charStates.find(g_curCharKey);
  Log("[CHAR] switch-restore: key='%s' found=%d tableFrozen=%d g_frozen(before)=%d",
      g_curCharKey.c_str(), it != g_charStates.end() ? 1 : 0,
      (it != g_charStates.end() && it->second.frozen) ? 1 : 0, (int)g_frozen);
  // 状态表全量（最多 8 条）：用来确认"每个角色的独立状态"到底存了谁
  {
    int shown = 0;
    for (auto &kv : g_charStates) {
      if (shown++ >= 8)
        break;
      Log("[CHAR]   table['%s'] frozen=%d bones=%d", kv.first.c_str(),
          (int)kv.second.frozen, (int)kv.second.pose.bones.size());
    }
  }
  if (g_curCharKey.empty() || it == g_charStates.end() || !it->second.frozen) {
    if (g_frozen)
      Log("[CHAR] '%s' has no saved frozen state -> leaving it unfrozen",
          g_curCharKey.c_str());
    // 保险：新目标不该被"残留的压制"按着。
    // 场景：A 冻结时登记了 grip（压制它的 Animator/动画组件/IK），切到 B 时如果
    // B 的指针出现在 grip 表里（同名模型复用同一批组件、或之前误登记过），
    // B 就会被压住 —— 表现就是"切过去 B 也冻住了/像继承了 A 的状态"。
    // 这里显式把该目标的 grip 释放掉（没有就什么都不做）。
    ReleaseGripFor(g_charAnimator);
    g_frozen = false;
    return;
  }
  Log("[CHAR] restoring frozen state for '%s'", g_curCharKey.c_str());
  FreezeCharacter(); // Capture this instance before suppressing its writers.
  ApplyPoseDoc(it->second.pose); // 按骨名恢复（表情骨按策略跳过）
  PinCurrentPose();       // 以恢复后的姿态作为新的冻结基线
  if (g_freezeAccessories) {
    if (s_accessoryChains.empty())
      RebuildAccessories();
    CaptureAccessorySnapshot();
    SetAllPhysicsEnabled(false);
    ApplyAccessorySnapshot();
  }
  g_frozen = true;
  Log("[CHAR] frozen state restored ('%s')", g_curCharKey.c_str());
}
