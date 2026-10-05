#pragma once

// Task 2.2：角色冻结系统。
// 目标（用户需求）："摆动作时不能突然没了"——冻结 = 抑制动画/IK/形态/布料对
// 骨骼的覆盖，让摆好的姿势稳定。核心手段：
//   1. 关闭 Animator（主写者消失，骨骼停在当前帧姿势）
//   2. PinCurrentPose() 固化当前帧为可编辑基线（供复位/FK 参考）
//   3. 其余写者逐项抑制：从骨物理组件（Task 2.4 accessory.h）、FinalIK/
//      SkeletalMorph/布料（随各 Task 探针补全，见下方注释）
//
// 不做每帧快照重写——否则 FK 拖骨会被立刻打回。编辑直接写骨骼，
// 因为 Animator 已关，姿势自然稳定。

#include "game/accessory.h"
#include "game/cloth.h"
#include "game/frozen_grip.h"

#include <cstring>

static bool g_animatorWasEnabled = true;
// 冻结选项：勾选后连飘带/裙子/头发等从骨一起冻结；默认关 = 从骨保持实时演算。
static bool g_freezeAccessories = false;
static void (*g_onFreezeReleased)() = nullptr;

// ---- FinalIK / 游戏 IK 组件抑制（参照 {EIEM} trojan.h 采集逻辑，AGPL-3.0）----
// 冻结时把角色根上会写骨骼的 IK/动画组件一并禁用，解冻恢复。
#define POSER_MAX_IK_COMPS 8
static void *s_ikBiped[POSER_MAX_IK_COMPS];
static int s_ikBipedCount = 0;
static void *s_ikGrounder[POSER_MAX_IK_COMPS];
static int s_ikGrounderCount = 0;
static void *s_ikLookAt[POSER_MAX_IK_COMPS];
static int s_ikLookAtCount = 0;
static void *s_ikDamper[4];
static int s_ikDamperCount = 0;
static void *s_animatorMono = nullptr;

// ---- 已冻结角色的"写者把手" ----
// 冻结写者与当前实例绑定。切走保存姿态并释放旧把手，切回时重新捕获。
static void RegisterCurrentGrip() {
  if (!LiveBehaviour(g_charAnimator)) return;
  for (const auto &old : g_frozenGrips)
    if (old.animator == g_charAnimator) return;
  FrozenGrip grip;
  grip.animator = g_charAnimator;
  CaptureGripComponent(grip, g_charAnimator);
  CaptureGripComponent(grip, g_charAnimComp);
  CaptureGripComponent(grip, s_animatorMono);
  for (int i = 0; i < s_ikBipedCount; ++i) CaptureGripComponent(grip, s_ikBiped[i]);
  for (int i = 0; i < s_ikGrounderCount; ++i) CaptureGripComponent(grip, s_ikGrounder[i]);
  for (int i = 0; i < s_ikLookAtCount; ++i) CaptureGripComponent(grip, s_ikLookAt[i]);
  for (int i = 0; i < s_ikDamperCount; ++i) CaptureGripComponent(grip, s_ikDamper[i]);
  g_frozenGrips.push_back(std::move(grip));
  Log("[POSER] frozen grip registered (animator=%p, grips=%d)", g_charAnimator, int(g_frozenGrips.size()));
}
static void ResetFreezeWriters() {
  s_ikBipedCount = s_ikGrounderCount = s_ikLookAtCount = s_ikDamperCount = 0;
  memset(s_ikBiped, 0, sizeof(s_ikBiped));
  memset(s_ikGrounder, 0, sizeof(s_ikGrounder));
  memset(s_ikLookAt, 0, sizeof(s_ikLookAt));
  memset(s_ikDamper, 0, sizeof(s_ikDamper));
  s_animatorMono = nullptr;
}

// 递归采集：遍历角色整个 Transform 层级，按类名收集会写骨骼的组件
static void CollectIKOnTransform(void *t, int depth) {
  if (!t || depth > 6)
    return;
  __try {
    if (g_component_get_gameObject && g_gameObject_GetComponents &&
        g_componentClass) {
      void *go = Invoke(g_component_get_gameObject, t);
      if (go) {
        void *type = il2cpp_class_get_type(g_componentClass);
        void *typeObj = type ? il2cpp_type_get_object(type) : nullptr;
        if (typeObj) {
          void *args[] = {typeObj};
          void *arr = Invoke(g_gameObject_GetComponents, go, args);
          if (arr) {
            int cnt = *(int *)((char *)arr + IL2CPP_ARRAY_LEN);
            void **data = (void **)((char *)arr + IL2CPP_ARRAY_DATA);
            for (int i = 0; i < cnt; i++) {
              if (!data[i])
                continue;
              void *cls = il2cpp_object_get_class(data[i]);
              const char *cn = cls ? il2cpp_class_get_name(cls) : "";
              if (!cn)
                continue;
              if (strcmp(cn, "BipedIK") == 0 &&
                  s_ikBipedCount < POSER_MAX_IK_COMPS)
                s_ikBiped[s_ikBipedCount++] = data[i];
              else if (strcmp(cn, "GrounderBipedIK") == 0 &&
                       s_ikGrounderCount < POSER_MAX_IK_COMPS)
                s_ikGrounder[s_ikGrounderCount++] = data[i];
              else if (strcmp(cn, "LookAtComponent") == 0 &&
                       s_ikLookAtCount < POSER_MAX_IK_COMPS)
                s_ikLookAt[s_ikLookAtCount++] = data[i];
              else if (strcmp(cn, "TransformFollowDamper") == 0 &&
                       s_ikDamperCount < 4)
                s_ikDamper[s_ikDamperCount++] = data[i];
              else if (strcmp(cn, "AnimatorMono") == 0)
                s_animatorMono = data[i];
            }
          }
        }
      }
    }
    // 子节点
    if (g_transform_get_childCount && g_transform_GetChild) {
      void *cntBoxed = Invoke(g_transform_get_childCount, t);
      int cnt = cntBoxed ? *(int *)((char *)cntBoxed + 16) : 0;
      for (int i = 0; i < cnt; i++) {
        void *params[] = {&i};
        void *ch = Invoke(g_transform_GetChild, t, params);
        if (ch)
          CollectIKOnTransform(ch, depth + 1);
      }
    }
  } __except (1) {
  }
}

static void DumpNativeIkOnce(); // 定义在下方（诊断用）

static void CollectIKComponents() {
  s_ikBipedCount = s_ikGrounderCount = s_ikLookAtCount = s_ikDamperCount = 0;
  s_animatorMono = nullptr;
  void *rootT = GetCharRootTransform();
  if (!rootT)
    return;
  CollectIKOnTransform(rootT, 0);
  Log("[POSER] IK comps: biped=%d grounder=%d lookAt=%d damper=%d mono=%p",
      s_ikBipedCount, s_ikGrounderCount, s_ikLookAtCount, s_ikDamperCount,
      s_animatorMono);
  DumpNativeIkOnce();
}

// [诊断] 一次性把游戏原生 BipedIK 的字段结构 + 当前字段值打进日志，
// 用来判断能否直接驱动游戏自带的 IK（FinalIK）：找 solvers / 目标 / 权重字段。
static void DumpNativeIkOnce() {
  static bool done = false;
  if (done || s_ikBipedCount == 0)
    return;
  done = true;
  void *biped = s_ikBiped[0];
  DumpFieldsHierarchy(il2cpp_object_get_class(biped));
  __try {
    void *cur = il2cpp_object_get_class(biped);
    int depth = 0;
    while (cur && depth < 6) {
      void *it = nullptr, *f;
      while ((f = il2cpp_class_get_fields(cur, &it))) {
        const char *fn = il2cpp_field_get_name(f);
        size_t fo = il2cpp_field_get_offset(f);
        if (fo >= 0x800)
          continue;
        void *v = *(void **)((char *)biped + fo);
        Log("[IKDUMP]   [0x%X] %s = %p", (int)fo, fn ? fn : "?", v);
      }
      cur = il2cpp_class_get_parent(cur);
      depth++;
    }
  } __except (1) {
    Log("[IKDUMP] fields dump exception");
  }
}

static void SetIKComponentsEnabled(bool on) {
  for (int i = 0; i < s_ikBipedCount; ++i) WriteBehaviourEnabled(s_ikBiped[i], on);
  for (int i = 0; i < s_ikGrounderCount; ++i) WriteBehaviourEnabled(s_ikGrounder[i], on);
  for (int i = 0; i < s_ikLookAtCount; ++i) WriteBehaviourEnabled(s_ikLookAt[i], on);
  for (int i = 0; i < s_ikDamperCount; ++i) WriteBehaviourEnabled(s_ikDamper[i], on);
  WriteBehaviourEnabled(s_animatorMono, on);
}

static bool AnimatorIsEnabled() {
  bool enabled = true;
  ReadBehaviourEnabled(g_charAnimator, enabled);
  return enabled;
}

// 抑制除 Animator 外的骨骼写者。
// 说明（[in-game] 探针，随对应 Task 补全）：
//   - 从骨动态骨骼/布料物理 → SetAllPhysicsEnabled(false)（本模块）
//   - FinalIK（BipedIK/Grounder/LookAt）→ 按 {EIEM} s_bipedIK/s_grounderIK
//     收集逻辑，把 IKSolver weight 写 0 或禁用组件（Task 3.2 联动）
//   - SkeletalMorphCore.Update（写 m_allMorphBoneDirty=false 跳过）→ Task 4.2
//   - 角色 ParticleSystem.Pause → 可选
static void SuppressPoseWriters() {
  // 只禁用会写 Humanoid 肢体的 IK/动画组件（回弹根源）；
  // 从骨/布料物理保持开启（头发裙摆继续结算），它们不写人体骨骼。
  CollectIKComponents();         // 先采集角色身上的 FinalIK/动画组件
  SetIKComponentsEnabled(false); // 再禁用：BipedIK/Grounder/LookAt/Damper 等
}

static void RestorePoseWriters() {
  SetIKComponentsEnabled(true);
}

// 每帧维持冻结：游戏常会重新启用 Animator/动画组件/IK 组件，
// 只在冻结瞬间关一次不够（"四肢一改就回弹"的根源）。逐帧强制关闭全部写者。
static void MaintainFreeze() {
  if (CharacterSwitchInProgress()) return;
  MaintainFrozenGrips();
  if (!g_frozen || !CharAnimatorAlive() || g_captureEntity != g_mainCharEntity) return;
  if (g_freezeAccessories) {
    MaintainAccessoryPhysicsFreeze();
    ApplyAccessorySnapshot();
  }
}

static void FreezeCharacter() {
  if (CharacterSwitchInProgress() || !CharAnimatorAlive() || g_frozen ||
      g_captureEntity != g_mainCharEntity) return;
  g_animatorWasEnabled = AnimatorIsEnabled();
  // Capture enabled states BEFORE any component is disabled.
  CollectIKComponents();
  RegisterCurrentGrip();
  MaintainFrozenGrips();
  // 2. 固化当前帧姿势为编辑基线（含从骨）
  PinCurrentPose();
  if (g_freezeAccessories && s_accessoryChains.empty())
    RebuildAccessories(); // 先保证从骨表存在，快照才反映冻结瞬间姿势
  CaptureAccessorySnapshot();
  Log("[POSER] Freeze: pose pinned");
  // 3. 抑制其余骨骼写者：FinalIK/Grounder/LookAt/Damper + 从骨物理。
  //    （之前这个调用缺失，导致四肢一直被游戏 IK 写回、一改就弹回去）
  SetIKComponentsEnabled(false);
  Log("[POSER] Freeze: writers suppressed");
  if (g_freezeAccessories) {
    SetAllPhysicsEnabled(false);
    ApplyAccessorySnapshot(); // 物理禁用后立刻把从骨钉到冻结瞬间姿势，避免回落默认
    Log("[POSER] Freeze: accessory physics disabled (option ON)");
  }
  g_frozen = true;
  Log("[POSER] Frozen (animator was enabled=%d)", g_animatorWasEnabled ? 1 : 0);
}

static void UnfreezeCharacter() {
  if (!g_frozen) { ReleaseGripFor(g_charAnimator); return; }
  g_frozen = false; // Stop all maintenance before restoring component state.
  if (g_onFreezeReleased) g_onFreezeReleased();
  ReleaseGripFor(g_charAnimator);
  if (CharAnimatorAlive()) {
    if (g_freezeAccessories) SetAllPhysicsEnabled(true);
  }
  ResetFreezeWriters();
  Log("[POSER] Unfrozen");
}

// 换角色的冻结处理已改为"按角色记忆"：见 game/char_state.h 的
// SaveCharStateOnSwitch / RestoreCharStateOnSwitch（切走保存、切回恢复、
// 没冻过的角色保持默认不冻结）。
