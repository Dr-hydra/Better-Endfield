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
  if (g_curCharKey.empty())
    return;
  if (!g_frozen) {
    g_charStates.erase(g_curCharKey); // 没冻结就不记（切回来保持默认）
    return;
  }
  CharFreezeState st;
  st.frozen = true;
  st.pose = BuildLivePoseDoc(g_curCharKey.c_str());
  g_charStates[g_curCharKey] = st;
  Log("[CHAR] saved frozen state for '%s' (%d bones, %d accessory)",
      g_curCharKey.c_str(), (int)st.pose.bones.size(),
      (int)st.pose.accBones.size());
}

// 角色切换：新角色已重建完成后调用；冻过就恢复，否则保持默认
static void RestoreCharStateOnSwitch() {
  g_curCharKey = CurrentCharModelKey();
  auto it = g_charStates.find(g_curCharKey);
  if (g_curCharKey.empty() || it == g_charStates.end() || !it->second.frozen) {
    if (g_frozen)
      Log("[CHAR] '%s' has no saved frozen state -> leaving it unfrozen",
          g_curCharKey.c_str());
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
