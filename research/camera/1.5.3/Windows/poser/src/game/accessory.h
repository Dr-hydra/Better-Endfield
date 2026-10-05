#pragma once

// Task 2.4：从骨（头发/配饰/飘带/衣角等动态骨骼）采集 + 物理开关 + 锁定。
//
// 从骨 = 角色根下除 Humanoid 55 根外的其余骨骼链（通常是 DynamicBone/Cloth 类
// 程序化物理驱动的链）。本模块：
//   1. 递归遍历根 Transform 全部子骨，识别非 Humanoid 从骨并按父子连续链分组；
//   2. 对每条从骨的 GameObject 枚举组件，识别物理/布料组件（类名探针，见
//      kPhysicsClassSubstrings），提供逐链/逐骨禁用物理（Behaviour.set_enabled）；
//   3. 逐骨锁定：锁定的骨在快照恢复等操作中被跳过，并钉在当前姿势。
//
// [in-game] 探针说明：实际物理组件类名（DynamicBone/CommonDynamicBone/Cloth/
// MagicaCloth/SpringBone…）需在游戏内核对；命中即按 Behaviour 禁用，未命中则
// 走"钉在快照值"的兜底（ApplyAccessorySnapshot 跳过被禁物理链）。

#include "game/skeleton.h"

#include <cstring>
#include <string>
#include <vector>

// 已知的动态物理/布料组件类名子串（[in-game] 探针确认后增补）
static const char *const kPhysicsClassSubstrings[] = {
    "DynamicBone", "CommonDynamicBone", "Cloth", "MagicaCloth",
    "SpringBone", "BoneWind", nullptr};

struct AccessoryBone {
  void *transform;
  char name[128];
  int parentIdx;  // 原始遍历列表中的父骨下标（-1 = 根）
  int chainId;    // 所属从骨链 id
  bool locked;    // 锁定：钉在当前姿势，恢复等操作跳过
  Vec3 localPos;
  Quat localRot;
  Vec3 frozenPos; // 冻结瞬间姿态（"复位到冻结时刻"用，不随手动编辑改变）
  Quat frozenRot;
  std::vector<void *> physicsComps; // 该骨 GameObject 上的物理/布料组件实例
};

struct AccessoryChain {
  int rootBoneIdx;
  char name[128];
  bool physicsEnabled;
  std::vector<int> bones; // 链上骨在 s_accessoryBones 中的下标
};

struct RawAccessoryBone {
  void *transform;
  char name[128];
  int parent; // 遍历列表父下标
  bool humanoid;
};

static std::vector<AccessoryBone> s_accessoryBones;
static std::vector<AccessoryChain> s_accessoryChains;
static std::vector<RawAccessoryBone> s_rawBones;

// 内部辅助骨/表情驱动骨/物理碰撞体/挂点等：摆姿用不到，两种模式都不显示
// （humanoid 骨不参与这个过滤，见 rig_gizmo.h 的 RigShowBone）。
static bool IsNoisyBoneName(const char *n) {
  if (!n || !n[0])
    return true;
  char low[128];
  int i = 0;
  for (; n[i] && i < (int)sizeof(low) - 1; i++) {
    char c = n[i];
    low[i] = (c >= 'A' && c <= 'Z') ? (char)(c + 32) : c;
  }
  low[i] = 0;
  static const char *const kSkip[] = {"nub",      "twist",   "corrective",
                                      "collider", "brow",    "eye",
                                      "face",     "lip",     "line",
                                      "head_",    "local",   "(clone)",
                                      "inner",    "outer",   "wep",
                                      nullptr};
  for (int k = 0; kSkip[k]; k++)
    if (strstr(low, kSkip[k]))
      return true;
  return false;
}

// 取某根从骨"冻结瞬间"的姿态（复位用）。手动编辑只改 localPos/localRot，
// 不会动 frozenPos/frozenRot，所以随时能回到冻结那一刻。
static bool GetAccessoryFrozenPose(void *t, Vec3 *pos, Quat *rot) {
  if (!t)
    return false;
  for (const AccessoryBone &b : s_accessoryBones) {
    if (b.transform != t)
      continue;
    if (pos)
      *pos = b.frozenPos;
    if (rot)
      *rot = b.frozenRot;
    return true;
  }
  return false;
}

// 所有从骨写回"冻结瞬间"姿态（跳过锁定骨）——"全部重置"用
static void ApplyAccessoryFrozenPose() {
  for (AccessoryBone &b : s_accessoryBones) {
    if (b.locked)
      continue;
    SetBoneLocalPos(b.transform, b.frozenPos);
    SetBoneLocalRot(b.transform, b.frozenRot);
  }
}

// 所有从骨旋转清零（位置回冻结值）——"清空姿态"用
static void ClearAccessoryPose() {
  for (AccessoryBone &b : s_accessoryBones) {
    if (b.locked)
      continue;
    SetBoneLocalPos(b.transform, b.frozenPos);
    SetBoneLocalRot(b.transform, Quat{0, 0, 0, 1});
  }
}

// 姿态文件：采集所有从骨当前 local 姿态
static void CollectAccessoryPoseEntries(std::vector<PoseBone> &out) {
  for (const AccessoryBone &b : s_accessoryBones) {
    // 跳过表情/形态驱动的内部骨（brow / eye / face / lip …）：它们由 SMC 驱动，
    // 存进姿态文件再套到别的角色上会得到错位或夸张的脸。布料/头发/飘带照常保存。
    if (IsNoisyBoneName(b.name))
      continue;
    PoseBone pb;
    pb.name = b.name;
    pb.pos = GetBoneLocalPos(b.transform);
    pb.rot = GetBoneLocalRot(b.transform);
    out.push_back(pb);
  }
}

// 姿态文件：按名字应用从骨姿态；同时把新姿态写进冻结快照，
// 否则冻结维持的每帧回写会立刻把读进来的姿势覆盖掉。
static int ApplyAccessoryPoseEntries(const std::vector<PoseBone> &in) {
  int applied = 0;
  for (const PoseBone &pb : in) {
    for (AccessoryBone &b : s_accessoryBones) {
      if (strcmp(b.name, pb.name.c_str()) != 0)
        continue;
      // 旧姿态文件可能带着表情骨（brow/eye/face/lip…）：一律不套用，
      // 否则换个角色加载旧文件同样会把脸弄乱
      if (IsNoisyBoneName(b.name))
        break;
      SetBoneLocalPos(b.transform, pb.pos);
      SetBoneLocalRot(b.transform, pb.rot);
      b.localPos = pb.pos;
      b.localRot = pb.rot;
      applied++;
      break;
    }
  }
  return applied;
}

// 手动编辑从骨（旋转盘 / WebUI）后，把新姿势写回冻结快照 = 新的冻结基线。
// 不这么做的话 MaintainFreeze 每帧的 ApplyAccessorySnapshot 会把编辑立刻打回去，
// 表现为「能选中、能拖旋转盘，但骨一动不动」。
static void SyncAccessorySnapshotFromTransform(void *t) {
  if (!t)
    return;
  SyncHumanBoneSnapshot(t); // humanoid 骨也同步（换角色时保存"用户摆好的"姿态）
  if (s_accessoryBones.empty())
    return;
  for (AccessoryBone &b : s_accessoryBones) {
    if (b.transform != t)
      continue;
    b.localPos = GetBoneLocalPos(t);
    b.localRot = GetBoneLocalRot(t);
    return;
  }
}

static void InstallAccessoryWriteHook() {
  g_boneWriteHook = SyncAccessorySnapshotFromTransform;
}

static bool IsHumanoidBoneTransform(void *t) {
  for (int i = 0; i < s_humanBoneCount; i++)
    if (s_humanBones[i].transform == t)
      return true;
  return false;
}

static bool IsPhysicsComponent(void *comp) {
  if (!comp)
    return false;
  __try {
    void *cls = il2cpp_object_get_class(comp);
    const char *cn = cls ? il2cpp_class_get_name(cls) : "";
    for (int i = 0; kPhysicsClassSubstrings[i]; i++)
      if (strstr(cn, kPhysicsClassSubstrings[i]))
        return true;
  } __except (1) {
  }
  return false;
}

// 枚举某骨 GameObject 上的物理组件到 AccessoryBone
static void CollectPhysicsComponents(AccessoryBone &b) {
  if (!g_component_get_gameObject || !g_gameObject_GetComponents ||
      !g_componentClass)
    return;
  __try {
    void *go = Invoke(g_component_get_gameObject, b.transform);
    if (!go)
      return;
    void *compType = il2cpp_class_get_type(g_componentClass);
    void *typeObj = compType ? il2cpp_type_get_object(compType) : nullptr;
    if (!typeObj)
      return;
    void *args[] = {typeObj};
    void *arr = Invoke(g_gameObject_GetComponents, go, args);
    if (!arr)
      return;
    int cnt = *(int *)((char *)arr + 24);
    void **data = (void **)((char *)arr + 32);
    for (int i = 0; i < cnt; i++)
      if (data[i] && IsPhysicsComponent(data[i]))
        b.physicsComps.push_back(data[i]);
    if (!b.physicsComps.empty())
      Log("[POSER] Bone '%s': %zu physics comp(s)", b.name,
          b.physicsComps.size());
  } __except (1) {
  }
}

static void WalkTransforms(void *t, int parentIdx, int depth) {
  if (!t || depth > 24)
    return;
  RawAccessoryBone rb;
  rb.transform = t;
  rb.parent = parentIdx;
  rb.humanoid = IsHumanoidBoneTransform(t);
  GetBoneName(t, rb.name, sizeof(rb.name));
  int idx = (int)s_rawBones.size();
  s_rawBones.push_back(rb);
  __try {
    void *cntBoxed = Invoke(g_transform_get_childCount, t);
    int cnt = cntBoxed ? *(int *)((char *)cntBoxed + 16) : 0;
    for (int i = 0; i < cnt; i++) {
      void *params[] = {&i};
      void *child = Invoke(g_transform_GetChild, t, params);
      if (child)
        WalkTransforms(child, idx, depth + 1);
    }
  } __except (1) {
  }
}

// 重建从骨列表 + 链分组（角色切换/冻结后调用）
static void RebuildAccessories() {
  s_accessoryBones.clear();
  s_accessoryChains.clear();
  s_rawBones.clear();

  RebuildHumanBones(); // 确保 humanoid 列表最新（IsHumanoidBoneTransform 依赖）

  void *root = GetCharRootTransform();
  if (!root || !g_transform_get_childCount || !g_transform_GetChild) {
    Log("[POSER] Accessory: root transform unavailable, skipped");
    return;
  }
  WalkTransforms(root, -1, 0);
  int n = (int)s_rawBones.size();

  std::vector<int> chainOf(n, -1); // raw idx -> chain id
  for (int i = 0; i < n; i++) {
    if (s_rawBones[i].humanoid)
      continue;
    // 向上找最近的非 humanoid 祖先，若它已入链则并入该链，否则新开链
    int p = s_rawBones[i].parent;
    while (p >= 0) {
      if (!s_rawBones[p].humanoid && chainOf[p] >= 0)
        break;
      if (s_rawBones[p].humanoid) {
        p = -1;
        break;
      }
      p = s_rawBones[p].parent;
    }
    int cid = (p >= 0) ? chainOf[p] : -1;
    if (cid < 0) {
      cid = (int)s_accessoryChains.size();
      AccessoryChain c;
      c.rootBoneIdx = -1;
      c.physicsEnabled = true;
      snprintf(c.name, sizeof(c.name), "%s",
               s_rawBones[i].name[0] ? s_rawBones[i].name : "chain");
      s_accessoryChains.push_back(c);
    }

    AccessoryBone b;
    b.transform = s_rawBones[i].transform;
    b.parentIdx = s_rawBones[i].parent;
    b.chainId = cid;
    b.locked = false;
    snprintf(b.name, sizeof(b.name), "%s",
             s_rawBones[i].name[0] ? s_rawBones[i].name : "bone");
    b.localPos = GetBoneLocalPos(b.transform);
    b.localRot = GetBoneLocalRot(b.transform);
    b.frozenPos = b.localPos;
    b.frozenRot = b.localRot;
    CollectPhysicsComponents(b);

    int bidx = (int)s_accessoryBones.size();
    s_accessoryBones.push_back(b);
    chainOf[i] = cid;
    if (s_accessoryChains[cid].rootBoneIdx < 0)
      s_accessoryChains[cid].rootBoneIdx = bidx;
    s_accessoryChains[cid].bones.push_back(bidx);
  }
  Log("[POSER] Accessories rebuilt: %zu chains, %zu bones",
      s_accessoryChains.size(), s_accessoryBones.size());
  InstallAccessoryWriteHook(); // 从骨列表就绪后接管手动写骨 → 同步冻结快照
}

// ---- 物理开关 ----
static void SetPhysicsEnabled(int chainId, bool enabled, bool quiet = false) {
  if (chainId < 0 || chainId >= (int)s_accessoryChains.size())
    return;
  AccessoryChain &c = s_accessoryChains[chainId];
  c.physicsEnabled = enabled;
  for (int bidx : c.bones) {
    AccessoryBone &b = s_accessoryBones[bidx];
    for (void *comp : b.physicsComps)
      WriteBehaviourEnabled(comp, enabled);
  }

  if (!quiet)
    Log("[POSER] Chain '%s' physics=%s", c.name, enabled ? "ON" : "OFF");
}

static void SetAllPhysicsEnabled(bool enabled, bool quiet = false) {
  for (int i = 0; i < (int)s_accessoryChains.size(); i++)
    SetPhysicsEnabled(i, enabled, quiet);
}

// 每帧维持：勾选"冻结飘带/裙子/头发"时反复禁用从骨物理组件，
// 防止游戏重新启用（同 MaintainFreeze 的思路，安静版不刷日志）。
static void MaintainAccessoryPhysicsFreeze() {
  if (!g_animator_set_enabled)
    return;
  for (int i = 0; i < (int)s_accessoryChains.size(); i++) {
    AccessoryChain &c = s_accessoryChains[i];
    for (int bidx : c.bones) {
      AccessoryBone &b = s_accessoryBones[bidx];
      for (void *comp : b.physicsComps)
        WriteBehaviourEnabled(comp, false);
    }
  }
}

// ---- 锁定（钉在当前姿势；恢复等操作跳过）----
static void SetAccessoryBoneLocked(int boneIdx, bool locked) {
  if (boneIdx < 0 || boneIdx >= (int)s_accessoryBones.size())
    return;
  AccessoryBone &b = s_accessoryBones[boneIdx];
  if (locked) {
    b.localPos = GetBoneLocalPos(b.transform); // 钉在当前姿势
    b.localRot = GetBoneLocalRot(b.transform);
  }
  b.locked = locked;
  Log("[POSER] Bone '%s' locked=%d", b.name, locked ? 1 : 0);
}

static void SetChainLocked(int chainId, bool locked) {
  if (chainId < 0 || chainId >= (int)s_accessoryChains.size())
    return;
  for (int bidx : s_accessoryChains[chainId].bones)
    SetAccessoryBoneLocked(bidx, locked);
}

// ---- 快照（恢复"冻结帧姿态"，跳过锁定骨）----
static void CaptureAccessorySnapshot() {
  for (AccessoryBone &b : s_accessoryBones) {
    b.localPos = GetBoneLocalPos(b.transform);
    b.localRot = GetBoneLocalRot(b.transform);
    b.frozenPos = b.localPos; // 冻结瞬间姿态单独留一份，供"复位"用
    b.frozenRot = b.localRot;
  }
}

static void ApplyAccessorySnapshot() {
  // 回写期间挂起钩子：避免自己写自己（多一次 O(n) 扫描），也避免把回写当成手动编辑
  void (*prevHook)(void *) = g_boneWriteHook;
  void (*prevHook2)(void *) = g_boneWriteHook2;
  g_boneWriteHook = nullptr;
  g_boneWriteHook2 = nullptr; // 冻结维持的每帧回写不算"编辑"，不能进撤销栈
  for (AccessoryBone &b : s_accessoryBones) {
    if (b.locked)
      continue; // 锁定骨保持钉住姿势
    SetBoneLocalPos(b.transform, b.localPos);
    SetBoneLocalRot(b.transform, b.localRot);
  }
  g_boneWriteHook = prevHook;
  g_boneWriteHook2 = prevHook2;
}

// 每帧维护：角色切换时重建从骨列表（供主循环调用）
static void AccessoryFrameTick() {
  if (g_charChanged) {
    g_charChanged = false;
    RebuildAccessories();
  }
}
