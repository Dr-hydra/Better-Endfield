#pragma once

// Task 2.3：Humanoid 骨骼列表 + 姿势快照/恢复。
// 依赖 game_hooks.h 的骨骼句柄封装。冻结时由 freeze.h 调 PinCurrentPose()
// 固化当前帧姿势作为可编辑基线；FK/IK/形态编辑在此基础上进行，
// ApplyPoseSnapshot() 用于"回到冻结帧"或复位。
//
// 锁定位（locked）默认为 false；Task 2.4 从骨锁定与 FK 面板会按需置位。

#include "core/game_hooks.h"
#include "math/quat_math.h"
#include "math/pose_file.h"

#include <vector>
#include <cstring>

struct BoneHandle {
  HumanBodyBones humanBone;
  void *transform;
  char name[128];
  bool locked; // 锁定后：ApplyPoseSnapshot 等操作跳过此骨
  Vec3 localPos;
  Quat localRot;
};

static BoneHandle s_humanBones[kHumanBoneCount];
static int s_humanBoneCount = 0;

// A-pose 基线（游戏最初姿态）：姿态库现已改为按"绝对 local 变换"保存/加载，
//   不再依赖此基线，避免角色重进/换场景后基线漂移导致姿态偏移；
//   下方 s_restRot/s_restPos 仅用于旧格式(restRel=true)文件的兼容还原。
static Quat s_restRot[kHumanBoneCount];
static Vec3 s_restPos[kHumanBoneCount];
static bool s_restCaptured = false;

// 捕获当前姿态为 A-pose 基线（角色首次出现时调用）
static void CaptureRestPose() {
  for (int i = 0; i < s_humanBoneCount; i++) {
    s_restRot[i] = GetBoneLocalRot(s_humanBones[i].transform);
    s_restPos[i] = GetBoneLocalPos(s_humanBones[i].transform);
  }
  s_restCaptured = true;
  Log("[POSER] A-pose baseline captured: %d bones", s_humanBoneCount);
}

// 重建骨骼列表（角色切换后调用）
// ---- Humanoid 骨名回退 ----
// 本作 Avatar 只映射 22 根主干骨（Unity 55 根里的手指/眼/下巴共 33 根
// GetBoneTransform 一律返回 null，见 plugin/poser_bones.txt 的 humanoid 列）。
// 这批骨按骨架真实命名回退解析，命名取自角色实测骨架（Bip001 + face joints）。
static const char *const kHumanBoneFallback[55][3] = {
    /*  0 Hips          */ {"Bip001", "Bip001_Pelvis", nullptr},
    /*  1 LeftUpperLeg  */ {"Bip001_L_Thigh", nullptr, nullptr},
    /*  2 RightUpperLeg */ {"Bip001_R_Thigh", nullptr, nullptr},
    /*  3 LeftLowerLeg  */ {"Bip001_L_Calf", nullptr, nullptr},
    /*  4 RightLowerLeg */ {"Bip001_R_Calf", nullptr, nullptr},
    /*  5 LeftFoot      */ {"Bip001_L_Foot", nullptr, nullptr},
    /*  6 RightFoot     */ {"Bip001_R_Foot", nullptr, nullptr},
    /*  7 Spine         */ {"Bip001_Spine", nullptr, nullptr},
    /*  8 Chest         */ {"Bip001_Spine1", nullptr, nullptr},
    /*  9 Neck          */ {"Bip001_Neck", nullptr, nullptr},
    /* 10 Head          */ {"Bip001_Head", nullptr, nullptr},
    /* 11 LeftShoulder  */ {"Bip001_L_Clavicle", nullptr, nullptr},
    /* 12 RightShoulder */ {"Bip001_R_Clavicle", nullptr, nullptr},
    /* 13 LeftUpperArm  */ {"Bip001_L_UpperArm", nullptr, nullptr},
    /* 14 RightUpperArm */ {"Bip001_R_UpperArm", nullptr, nullptr},
    /* 15 LeftLowerArm  */ {"Bip001_L_Forearm", nullptr, nullptr},
    /* 16 RightLowerArm */ {"Bip001_R_Forearm", nullptr, nullptr},
    /* 17 LeftHand      */ {"Bip001_L_Hand", nullptr, nullptr},
    /* 18 RightHand     */ {"Bip001_R_Hand", nullptr, nullptr},
    /* 19 LeftToes      */ {"Bip001_L_Toe0", nullptr, nullptr},
    /* 20 RightToes     */ {"Bip001_R_Toe0", nullptr, nullptr},
    /* 21 LeftEye       */ {"eyeLfJoint", "Bip001_L_Eye", nullptr},
    /* 22 RightEye      */ {"eyeRtJoint", "Bip001_R_Eye", nullptr},
    /* 23 Jaw           */ {"jawJoint", "Bip001_Jaw", nullptr},
    /* 24 L ThumbProximal     */ {"Bip001_L_Finger0", nullptr, nullptr},
    /* 25 L ThumbIntermediate */ {"Bip001_L_Finger01", nullptr, nullptr},
    /* 26 L ThumbDistal       */ {"Bip001_L_Finger02", nullptr, nullptr},
    /* 27 L IndexProximal     */ {"Bip001_L_Finger1", nullptr, nullptr},
    /* 28 L IndexIntermediate */ {"Bip001_L_Finger11", nullptr, nullptr},
    /* 29 L IndexDistal       */ {"Bip001_L_Finger12", nullptr, nullptr},
    /* 30 L MiddleProximal    */ {"Bip001_L_Finger2", nullptr, nullptr},
    /* 31 L MiddleIntermediate*/ {"Bip001_L_Finger21", nullptr, nullptr},
    /* 32 L MiddleDistal      */ {"Bip001_L_Finger22", nullptr, nullptr},
    /* 33 L RingProximal      */ {"Bip001_L_Finger3", nullptr, nullptr},
    /* 34 L RingIntermediate  */ {"Bip001_L_Finger31", nullptr, nullptr},
    /* 35 L RingDistal        */ {"Bip001_L_Finger32", nullptr, nullptr},
    /* 36 L LittleProximal    */ {"Bip001_L_Finger4", nullptr, nullptr},
    /* 37 L LittleIntermediate*/ {"Bip001_L_Finger41", nullptr, nullptr},
    /* 38 L LittleDistal      */ {"Bip001_L_Finger42", nullptr, nullptr},
    /* 39 R ThumbProximal     */ {"Bip001_R_Finger0", nullptr, nullptr},
    /* 40 R ThumbIntermediate */ {"Bip001_R_Finger01", nullptr, nullptr},
    /* 41 R ThumbDistal       */ {"Bip001_R_Finger02", nullptr, nullptr},
    /* 42 R IndexProximal     */ {"Bip001_R_Finger1", nullptr, nullptr},
    /* 43 R IndexIntermediate */ {"Bip001_R_Finger11", nullptr, nullptr},
    /* 44 R IndexDistal       */ {"Bip001_R_Finger12", nullptr, nullptr},
    /* 45 R MiddleProximal    */ {"Bip001_R_Finger2", nullptr, nullptr},
    /* 46 R MiddleIntermediate*/ {"Bip001_R_Finger21", nullptr, nullptr},
    /* 47 R MiddleDistal      */ {"Bip001_R_Finger22", nullptr, nullptr},
    /* 48 R RingProximal      */ {"Bip001_R_Finger3", nullptr, nullptr},
    /* 49 R RingIntermediate  */ {"Bip001_R_Finger31", nullptr, nullptr},
    /* 50 R RingDistal        */ {"Bip001_R_Finger32", nullptr, nullptr},
    /* 51 R LittleProximal    */ {"Bip001_R_Finger4", nullptr, nullptr},
    /* 52 R LittleIntermediate*/ {"Bip001_R_Finger41", nullptr, nullptr},
    /* 53 R LittleDistal      */ {"Bip001_R_Finger42", nullptr, nullptr},
    /* 54 UpperChest    */ {"Bip001_Spine2", nullptr, nullptr},
};

static void *FindBoneByNameInAll(const char *name); // 定义在 s_allBones 之后
static void DumpBoneInventoryOnce();                // 定义在 s_allBones 之后

static void RebuildHumanBones() {
  s_humanBoneCount = 0;
  int fallbackHits = 0;
  char missing[1024] = "";
  size_t mlen = 0;
  for (int b = 0; b < kHumanBoneCount; b++) {
    void *t = GetHumanoidBone((HumanBodyBones)b);
    // Avatar 没映射到的骨（手指/眼/下巴等）按骨架命名回退
    if (!t && kHumanBoneFallback[b][0]) {
      for (int c = 0; c < 3 && kHumanBoneFallback[b][c]; c++) {
        t = FindBoneByNameInAll(kHumanBoneFallback[b][c]);
        if (t) {
          fallbackHits++;
          break;
        }
      }
    }
    if (!t) {
      const char *mn = HumanBoneName((HumanBodyBones)b);
      if (mn) {
        size_t n = strlen(mn);
        if (mlen + n + 2 < sizeof(missing)) {
          if (mlen)
            missing[mlen++] = ',';
          memcpy(missing + mlen, mn, n);
          mlen += n;
          missing[mlen] = 0;
        }
      }
      continue;
    }
    BoneHandle &bh = s_humanBones[s_humanBoneCount];
    bh.humanBone = (HumanBodyBones)b;
    bh.transform = t;
    bh.locked = false;
    bh.localPos = GetBoneLocalPos(t);
    bh.localRot = GetBoneLocalRot(t);
    GetBoneName(t, bh.name, sizeof(bh.name));
    if (bh.name[0] == 0)
      snprintf(bh.name, sizeof(bh.name), "%s", HumanBoneName(b));
    s_humanBoneCount++;
  }
  Log("[POSER] Skeleton rebuilt: %d humanoid bones (%d via name fallback)",
      s_humanBoneCount, fallbackHits);
  // 只在数量变化（或最前面 3 次）时打详细映射，避免重试循环刷屏
  static int s_lastLoggedCount = -1;
  static int s_detailDumps = 0;
  if (s_humanBoneCount != s_lastLoggedCount || s_detailDumps < 3) {
    s_lastLoggedCount = s_humanBoneCount;
    s_detailDumps++;
    Log("[POSER] Humanoid map: found=%d missing=[%s]", s_humanBoneCount,
        missing);
  }
  DumpBoneInventoryOnce(); // 全骨列表按 humanoid 标记一次性导出（诊断用）
}

// 捕获当前帧全部骨骼 local pos/rot 到快照
static void CapturePoseSnapshot() {
  for (int i = 0; i < s_humanBoneCount; i++) {
    s_humanBones[i].localPos = GetBoneLocalPos(s_humanBones[i].transform);
    s_humanBones[i].localRot = GetBoneLocalRot(s_humanBones[i].transform);
  }
}

// 把快照写回骨骼（复位/回到冻结帧），跳过锁定骨
static void ApplyPoseSnapshot() {
  for (int i = 0; i < s_humanBoneCount; i++) {
    if (s_humanBones[i].locked)
      continue;
    SetBoneLocalPos(s_humanBones[i].transform, s_humanBones[i].localPos);
    SetBoneLocalRot(s_humanBones[i].transform, s_humanBones[i].localRot);
  }
}

// 写骨钩子用：把 humanoid 骨的手动改动同步进内存快照——换角色时要存的是
// "用户摆好的姿态"，而快照默认只有冻结瞬间的值。O(55) 开销可忽略。
static void SyncHumanBoneSnapshot(void *t) {
  if (!t)
    return;
  for (int i = 0; i < s_humanBoneCount; i++) {
    if (s_humanBones[i].transform != t)
      continue;
    s_humanBones[i].localPos = GetBoneLocalPos(t);
    s_humanBones[i].localRot = GetBoneLocalRot(t);
    return;
  }
}

// 固化当前姿势为编辑基线：重建列表 + 采集快照
static void PinCurrentPose() {
  RebuildHumanBones();
  CapturePoseSnapshot();
  Log("[POSER] Pose pinned: %d bones", s_humanBoneCount);
}

// T-pose：所有骨 localRotation 置 0（保留位置），跳过锁定骨
// 把世界旋转增量 d 作用到骨的 localRotation（考虑父系），仅供 T-pose 内部使用
static void SetBoneWorldDelta(void *t, Quat d) {
  if (!t)
    return;
  Quat curLocal = GetBoneLocalRot(t);
  Quat localDelta = d;
  void *parent = g_transform_get_parent ? Invoke(g_transform_get_parent, t)
                                        : nullptr;
  if (parent) {
    Quat pw = GetBoneWorldRot(parent);
    localDelta = NormQ(Conj(pw) * d * pw);
  }
  SetBoneLocalRot(t, NormQ(localDelta * curLocal));
}

static Vec3 RotateVec(Quat q, Vec3 v) {
  Quat p{v.x, v.y, v.z, 0};
  Quat qc = Conj(q);
  Quat r = q * p * qc;
  return {r.x, r.y, r.z};
}

// 真正的 T-pose：所有骨 localRotation 归零 + 四肢沿世界轴摆直 + 脊柱朝上。
// （旧实现只把 localRotation 归零，但游戏 rig 绑定姿势非单位四元数，会鬼畜）
static void ApplyTPose() {
  __try {
    for (int i = 0; i < s_humanBoneCount; i++) {
      if (s_humanBones[i].locked || !s_humanBones[i].transform)
        continue;
      SetBoneLocalRot(s_humanBones[i].transform, Quat{0, 0, 0, 1});
    }
    // 四肢：根→中→末 的方向对齐到世界轴（左臂+X 右臂-X 双腿-Y）
    struct TLimbs { HumanBodyBones root, mid, end; Vec3 axis; };
    const TLimbs limbs[] = {
        {LeftUpperArm, LeftLowerArm, LeftHand, {1, 0, 0}},
        {RightUpperArm, RightLowerArm, RightHand, {-1, 0, 0}},
        {LeftUpperLeg, LeftLowerLeg, LeftFoot, {0, -1, 0}},
        {RightUpperLeg, RightLowerLeg, RightFoot, {0, -1, 0}},
    };
    for (const auto &lm : limbs) {
      void *rt = GetHumanoidBone(lm.root);
      void *mt = GetHumanoidBone(lm.mid);
      void *et = GetHumanoidBone(lm.end);
      if (!rt || !mt || !et)
        continue;
      Vec3 u0 = Norm(GetBoneWorldPos(mt) - GetBoneWorldPos(rt));
      Vec3 u1 = Norm(GetBoneWorldPos(et) - GetBoneWorldPos(mt));
      Vec3 ax = Norm(lm.axis);
      if (Len(u0) > 1e-4f)
        SetBoneWorldDelta(rt, Quat::FromTo(u0, ax));
      if (Len(u1) > 1e-4f)
        SetBoneWorldDelta(mt, Quat::FromTo(u1, ax));
    }
    // 脊柱朝上：把 Hips 的世界 up 对齐到 +Y
    void *hips = GetHumanoidBone(Hips);
    if (hips) {
      Vec3 up0 = Norm(RotateVec(GetBoneWorldRot(hips), Vec3{0, 1, 0}));
      if (Len(up0) > 1e-4f)
        SetBoneWorldDelta(hips, Quat::FromTo(up0, Vec3{0, 1, 0}));
    }
  } __except (1) {
    Log("[POSER] ApplyTPose exception");
  }
}

// 按 HumanBodyBones 找骨骼在列表中的下标（找不到返回 -1）
static int FindHumanBoneIndex(HumanBodyBones bone) {
  for (int i = 0; i < s_humanBoneCount; i++)
    if (s_humanBones[i].humanBone == bone)
      return i;
  return -1;
}

// 镜像功能实验失败、已整体移除（骨骼局部轴约定不明，实测会拉散骨架）。
// 需要时从 git 历史里挑回来重做，别再用"按局部轴翻 x"那套假设。

// ---- 姿态操作（Task 3.3）：存取 ----
// 采集当前全部 Humanoid 骨到位姿文档（存盘用）。
// 存绝对 local pos/rot：读回时无需 A-pose 基线，跨会话/换角色都稳定。
// 姿态文件的扩展钩子：从骨（accessory.h）与形态键（morph.h）由上层注册补进来
static void (*g_poseCaptureExtras)(PoseDoc &doc) = nullptr;
static void (*g_poseApplyExtras)(const PoseDoc &doc) = nullptr;

// 是否把"表情骨"（眼/下巴）写进姿态文件并在套用时应用。默认 = 跳过：
// 本作眼/下巴由 SMC 骨骼变形驱动，跨角色套用会把 A 的脸部变换写到 B 上（脸会错乱）；
// 跳过之后姿态只管身体与从骨，表情各自在新角色上调。
static bool g_poseSkipFaceBones = true;
static bool IsFaceExpressionBone(HumanBodyBones b) {
  return b == LeftEye || b == RightEye || b == Jaw;
}

static PoseDoc CapturePoseDoc(const char *name) {
  PoseDoc doc;
  doc.name = name ? name : "";
  doc.restRel = false; // 新格式：绝对 local 变换
  for (int i = 0; i < s_humanBoneCount; i++) {
    if (g_poseSkipFaceBones && IsFaceExpressionBone(s_humanBones[i].humanBone))
      continue;
    PoseBone pb;
    pb.name = s_humanBones[i].name;
    pb.pos = GetBoneLocalPos(s_humanBones[i].transform);
    pb.rot = GetBoneLocalRot(s_humanBones[i].transform);
    doc.bones.push_back(pb);
  }
  // 从骨 / 形态键由上层注册的钩子补进来（skeleton.h 是底层，反向包含
  // accessory.h / morph.h 会循环包含）
  if (g_poseCaptureExtras)
    g_poseCaptureExtras(doc);
  return doc;
}

// 应用位姿文档（按名称匹配；跳过锁定骨与未知骨）。
// 新格式(restRel=false)直接写绝对 local；旧格式(restRel=true)按基线增量还原（兼容）。
static void ApplyPoseDoc(const PoseDoc &doc) {
  int applied = 0;
  int skippedFace = 0;
  for (size_t bi = 0; bi < doc.bones.size(); bi++) {
    const PoseBone &pb = doc.bones[bi];
    int idx = -1;
    for (int i = 0; i < s_humanBoneCount; i++)
      if (strcmp(s_humanBones[i].name, pb.name.c_str()) == 0) {
        idx = i;
        break;
      }
    if (idx < 0 || s_humanBones[idx].locked)
      continue;
    if (g_poseSkipFaceBones && IsFaceExpressionBone(s_humanBones[idx].humanBone)) {
      skippedFace++;
      continue;
    }
    if (doc.restRel && s_restCaptured) { // 旧格式兼容：相对 A-pose 基线还原
      SetBoneLocalPos(s_humanBones[idx].transform, s_restPos[idx] + pb.pos);
      SetBoneLocalRot(s_humanBones[idx].transform,
                      NormQ(pb.rot * s_restRot[idx]));
    } else { // 新格式：绝对 local 直接写入
      SetBoneLocalPos(s_humanBones[idx].transform, pb.pos);
      SetBoneLocalRot(s_humanBones[idx].transform, pb.rot);
    }
    applied++;
  }
  Log("[POSER] Applied pose '%s': %d/%d bones", doc.name.c_str(), applied,
      (int)doc.bones.size());
  if (skippedFace > 0)
    Log("[POSER] pose: skipped %d face bone(s) (eye/jaw)",
        skippedFace);
  if (g_poseApplyExtras)
    g_poseApplyExtras(doc);
}

// ---- 全骨骼采集（供 Blender 桥接/完整摆姿；不止 Humanoid 22 根）----
struct AllBone {
  void *transform = nullptr;
  void *parent = nullptr;
  char name[128] = {0};
  int parentIdx = -1; // -1 = 根
};
static std::vector<AllBone> s_allBones;
static void *FindBoneByNameInAll(const char *name) {
  if (!name)
    return nullptr;
  for (const AllBone &b : s_allBones)
    if (b.name[0] && _stricmp(b.name, name) == 0)
      return b.transform;
  return nullptr;
}
static int s_bonesRev = 0; // 骨骼列表版本号（角色切换重建时 +1，Blender 桥接据此自动刷新）

static void CollectAllBonesRecursive(void *t, void *parent, int depth) {
  if (!t || depth > 64 || s_allBones.size() >= 4096)
    return;
  AllBone b;
  b.transform = t;
  b.parent = parent;
  __try {
    if (g_component_get_gameObject && g_object_get_name) {
      void *go = Invoke(g_component_get_gameObject, t);
      if (go) {
        void *nb = Invoke(g_object_get_name, go);
        if (nb)
          ReadStr(nb, b.name, sizeof(b.name));
      }
    }
  } __except (1) {
  }
  s_allBones.push_back(b);
  __try {
    if (g_transform_get_childCount && g_transform_GetChild) {
      void *boxed = Invoke(g_transform_get_childCount, t);
      int n = boxed ? *(int *)((char *)boxed + 16) : 0;
      for (int i = 0; i < n && i < 4096 && s_allBones.size() < 4096; i++) {
        int idx = i;
        void *params[] = {&idx};
        void *child = Invoke(g_transform_GetChild, t, params);
        if (child)
          CollectAllBonesRecursive(child, t, depth + 1);
      }
    }
  } __except (1) {
  }
}

// 判断某 transform 是否在 humanoid 列表里（skeleton.h 内自带，避免依赖后置头文件）
// 抽检缓存里的骨骼变换是否还活着：换实例时旧 Animator 可能还没销毁，
// 但列表里的骨 transform 已经死了 —— 画面表现同样是"骨架钉在原地"。
static bool CachedBonesAlive() {
  if (s_humanBoneCount > 0) {
    if (!UnityObjAlive(s_humanBones[0].transform))
      return false;
    if (!UnityObjAlive(s_humanBones[s_humanBoneCount - 1].transform))
      return false;
  }
  if (!s_allBones.empty()) {
    if (!UnityObjAlive(s_allBones.front().transform))
      return false;
    if (!UnityObjAlive(s_allBones.back().transform))
      return false;
  }
  return true;
}

static bool IsHumanBoneInList(void *t) {
  for (int i = 0; i < s_humanBoneCount; i++)
    if (s_humanBones[i].transform == t)
      return true;
  return false;
}

// 一次性把角色全部 Transform 导出到 plugin\poser_bones.txt（层级 + 是否 humanoid），
// 用于核对 Avatar 映射不到的骨（GetBoneTransform 返回 null 的那批，例如手指/脸骨），
// 也是后续"按骨名回退解析"的命名依据。
static void DumpBoneInventoryOnce() {
  static bool dumped = false;
  if (dumped || s_allBones.empty())
    return;
  dumped = true;
  FILE *f = OpenPoserFile(L"poser_bones.txt", L"wb");
  if (!f) {
    Log("[POSER] WARN: cannot write plugin/poser_bones.txt");
    return;
  }
  fprintf(f, "# idx\tparentIdx\tdepth\thumanoid\tname\n");
  for (size_t i = 0; i < s_allBones.size(); i++) {
    int depth = 0;
    for (int p = s_allBones[i].parentIdx; p >= 0 && depth < 64; depth++)
      p = s_allBones[p].parentIdx;
    fprintf(f, "%d\t%d\t%d\t%d\t%s\n", (int)i, s_allBones[i].parentIdx, depth,
            IsHumanBoneInList(s_allBones[i].transform) ? 1 : 0,
            s_allBones[i].name);
  }
  fclose(f);
  Log("[POSER] Bone inventory dumped: %d entries -> plugin/poser_bones.txt",
      (int)s_allBones.size());
}

static void RebuildAllBones() {
  s_allBones.clear();
  void *root = GetCharRootTransform();
  if (!root)
    return;
  CollectAllBonesRecursive(root, nullptr, 0);
  for (size_t i = 0; i < s_allBones.size(); i++) {
    if (s_allBones[i].parent) {
      for (size_t j = 0; j < s_allBones.size(); j++) {
        if (s_allBones[j].transform == s_allBones[i].parent) {
          s_allBones[i].parentIdx = (int)j;
          break;
        }
      }
    }
  }
  Log("[POSER] All bones rebuilt: %d", (int)s_allBones.size());
  s_bonesRev++;
}
