#pragma once

// Task 4.1：形态键——面部 BlendShape 采集与读写。
// 从角色根递归遍历全部 Transform，对每个挂有 SkinnedMeshRenderer 的节点枚举
// BlendShape（名称 + 当前权重），冻结态下用 SetBlendShapeWeight 实时写入，
// 不受动画覆盖。Task 4.2 的 SkeletalMorph（身体骨骼形态）也在此扩展。
//
// 单一 TU（poser.cpp）设计：全部 static 全局在该 TU 内共享。

#include <cstdio>
#include <cstring>
#include <string>
#include <vector>

#include "core/game_hooks.h"

struct BlendShapeSlot {
  void *smr; // SkinnedMeshRenderer
  char meshName[128];
  int index;
  char name[128];
  float value;     // 面板当前值（0-100）
  float origValue; // 冻结前原始值（解冻恢复用）
};

static std::vector<BlendShapeSlot> s_blendShapes;

// 写单个 BlendShape 权重（冻结态下直接生效）
static void SetBlendShapeWeight(BlendShapeSlot &s, float v) {
  if (v < 0.0f)
    v = 0.0f;
  if (v > 100.0f)
    v = 100.0f;
  s.value = v;
  if (CharacterSwitchInProgress() || !UnityObjAlive(s.smr) || !g_smr_SetBlendShapeWeight)
    return;
  __try {
    void *params[] = {&s.index, &v};
    Invoke(g_smr_SetBlendShapeWeight, s.smr, params);
  } __except (1) {
  }
}

static void ResetBlendShapeWeight(BlendShapeSlot &s) {
  SetBlendShapeWeight(s, s.origValue);
}

// 解冻/重置时恢复全部原始权重
static void RestoreBlendShapes() {
  for (BlendShapeSlot &s : s_blendShapes)
    ResetBlendShapeWeight(s);
}

// 递归遍历角色根，收集所有 SkinnedMeshRenderer 上的 BlendShape
static void WalkForBlendShapes(void *t, int depth) {
  if (!t || depth > 24)
    return;
  __try {
    if (g_component_get_gameObject && g_smr_get_sharedMesh &&
        g_mesh_get_blendShapeCount && g_mesh_GetBlendShapeName &&
        g_skinnedMeshRendererClass) {
      void *go = Invoke(g_component_get_gameObject, t);
      if (go) {
        void *smrType = il2cpp_class_get_type(g_skinnedMeshRendererClass);
        void *typeObj = smrType ? il2cpp_type_get_object(smrType) : nullptr;
        if (typeObj) {
          void *args[] = {typeObj};
          void *smr = Invoke(g_gameObject_GetComponent, go, args);
          if (smr) {
            void *mesh = Invoke(g_smr_get_sharedMesh, smr);
            if (mesh) {
              void *cntBoxed = Invoke(g_mesh_get_blendShapeCount, mesh);
              int bsCount = cntBoxed ? *(int *)((char *)cntBoxed + 16) : 0;
              if (bsCount > 0) {
                char meshName[128] = "?";
                GetBoneName(t, meshName, sizeof(meshName));
                for (int i = 0; i < bsCount; i++) {
                  BlendShapeSlot slot;
                  slot.smr = smr;
                  slot.index = i;
                  snprintf(slot.meshName, sizeof(slot.meshName), "%s",
                           meshName);
                  void *np[] = {&i};
                  void *nameStr = Invoke(g_mesh_GetBlendShapeName, mesh, np);
                  if (nameStr)
                    ReadStrUtf8(nameStr, slot.name, sizeof(slot.name));
                  else
                    snprintf(slot.name, sizeof(slot.name), "#%d", i);
                  slot.value = 0.0f;
                  slot.origValue = 0.0f;
                  if (g_smr_GetBlendShapeWeight) {
                    void *wp[] = {&i};
                    void *wBoxed =
                        Invoke(g_smr_GetBlendShapeWeight, smr, wp);
                    if (wBoxed)
                      slot.origValue = slot.value =
                          *(float *)((char *)wBoxed + 16);
                  }
                  s_blendShapes.push_back(slot);
                }
              }
            }
          }
        }
      }
    }
  } __except (1) {
  }
  // 递归子骨
  __try {
    void *cntBoxed = Invoke(g_transform_get_childCount, t);
    int cnt = cntBoxed ? *(int *)((char *)cntBoxed + 16) : 0;
    for (int i = 0; i < cnt; i++) {
      void *params[] = {&i};
      void *child = Invoke(g_transform_GetChild, t, params);
      if (child)
        WalkForBlendShapes(child, depth + 1);
    }
  } __except (1) {
  }
}

// 重建形态键列表（角色切换后调用）
static void RebuildBlendShapes() {
  s_blendShapes.clear();
  void *root = GetCharRootTransform();
  if (!root)
    return;
  WalkForBlendShapes(root, 0);
  Log("[POSER] BlendShapes rebuilt: %zu slots", s_blendShapes.size());
}
