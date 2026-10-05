#pragma once

// 选中骨骼状态：由 WebUI /api/select 与外部控制通道共享。
// 摆姿编辑 UI 已迁往 Blender，游戏侧只保留选中标记（供 WebUI 高亮/拖拽）。

#include "core/game_hooks.h"
#include "game/skeleton.h"
#include "math/quat_math.h"

#include <cstdio>
#include <cstring>

static int g_selectedBone = -1;
static void *g_selectedTransform = nullptr; // 任意骨骼选中（含非 humanoid）
static char g_selectedName[128] = "";

// 选中任意骨骼 transform；若是 humanoid 骨则同时映射 g_selectedBone
static void SelectTransform(void *t, const char *name) {
  g_selectedTransform = t;
  g_selectedBone = -1;
  if (name && name[0])
    snprintf(g_selectedName, sizeof(g_selectedName), "%s", name);
  else
    g_selectedName[0] = 0;
  if (t) {
    for (int i = 0; i < s_humanBoneCount; i++)
      if (s_humanBones[i].transform == t) {
        g_selectedBone = i;
        break;
      }
  }
}

// 在 s_humanBones 中查找 transform 对应的骨骼下标（WebUI 父-子连线用）
static int FindTransformIndex(void *t) {
  if (!t)
    return -1;
  for (int i = 0; i < s_humanBoneCount; i++)
    if (s_humanBones[i].transform == t)
      return i;
  return -1;
}

// 把世界旋转增量 d 作用到 transform 的局部旋转（考虑父系链）
static void ApplyWorldRotDelta(void *t, Quat d) {
  if (!t)
    return;
  Quat curLocal = GetBoneLocalRot(t);
  Quat localDelta = d;
  if (g_transform_get_parent) {
    __try {
      void *parent = Invoke(g_transform_get_parent, t);
      if (parent) {
        Quat pw = GetBoneWorldRot(parent);
        localDelta = NormQ(Conj(pw) * d * pw);
      }
    } __except (1) {
    }
  }
  SetBoneLocalRot(t, NormQ(localDelta * curLocal));
}

static bool SelectBoneByName(const char *name) {
  if (!name || !*name)
    return false;
  for (int i = 0; i < s_humanBoneCount; i++) {
    if (strcmp(s_humanBones[i].name, name) == 0) {
      g_selectedBone = i;
      return true;
    }
  }
  for (int i = 0; i < s_humanBoneCount; i++) {
    const char *hn = HumanBoneName(s_humanBones[i].humanBone);
    if (hn && strcmp(hn, name) == 0) {
      g_selectedBone = i;
      return true;
    }
  }
  return false;
}
