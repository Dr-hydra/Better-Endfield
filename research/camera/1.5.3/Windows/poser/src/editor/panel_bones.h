#pragma once

// 骨骼参数面板：选中骨（3D 里点选）的旋转/位置参数 + 复位。
// 注：骨骼层级树、操作序列、撤销/重做、清空姿态、回 A-pose 都因实测有问题已移除
// （代码在 git 历史里）。

#include "imgui.h"

#include "editor/rig_gizmo.h"
#include "editor/selection.h"
#include "editor/ik_control.h"
#include "game/accessory.h"
#include "game/skeleton.h"
#include "math/quat_math.h"

#include <cstring>
#include <vector>

static bool g_showBoneParams = true;

// 单轴 ± 步进按钮（人物位置、骨骼参数都用它）
static bool AxisStepper(const char *id, float *v, float step) {
  bool changed = false;
  ImGui::PushID(id);
  if (ImGui::SmallButton("-")) {
    *v -= step;
    changed = true;
  }
  ImGui::SameLine();
  if (ImGui::SmallButton("+")) {
    *v += step;
    changed = true;
  }
  ImGui::PopID();
  return changed;
}

// ---- 姿态级操作：全部重置（所有骨回到冻结瞬间）----
static void PoseOpResetToFreeze() {
  ApplyPoseSnapshot();        // humanoid → 冻结帧快照（跳过锁定骨）
  ApplyAccessoryFrozenPose(); // 从骨 → 冻结瞬间
}
// 复位：humanoid 骨回到 A-pose 基线（s_rest*），从骨回到冻结瞬间姿态
static void ResetBoneRot(void *t) {
  int hi = FindTransformIndex(t);
  if (hi >= 0 && s_restCaptured) {
    SetBoneLocalRot(t, s_restRot[hi]);
    return;
  }
  Quat q;
  if (GetAccessoryFrozenPose(t, nullptr, &q)) {
    SetBoneLocalRot(t, q);
    return;
  }
  SetBoneLocalRot(t, Quat{0, 0, 0, 1});
}

static void ResetBonePos(void *t) {
  int hi = FindTransformIndex(t);
  if (hi >= 0 && s_restCaptured) {
    SetBoneLocalPos(t, s_restPos[hi]);
    return;
  }
  Vec3 p;
  if (GetAccessoryFrozenPose(t, &p, nullptr))
    SetBoneLocalPos(t, p);
}

// 选中骨的参数：滑条可拖也可填（每个轴都能 Ctrl+点击数值直接输入），
// 另有输入框用于精确键入；写回走 SetBoneLocalRot/Pos，从骨自动同步冻结快照。
static void DrawBoneParams() {
  void *t = g_selectedTransform;
  if (!t) {
    ImGui::TextDisabled(u8"\u672a\u9009\u4e2d\u9aa8\u9abc");
    return;
  }
  ImGui::Text("%s", g_selectedName[0] ? g_selectedName : "(unnamed)");
  Vec3 e = GetBoneLocalRot(t).ToEulerDeg();
  ImGui::TextDisabled(u8"\u65cb\u8f6c (XYZ \u03a6)");
  ImGui::SetNextItemWidth(-1);
  if (ImGui::SliderFloat3(u8"##rot", &e.x, -180.0f, 180.0f, "%.1f")) {
    SetBoneLocalRot(t, Quat::FromEulerDeg(e));
  }
  ImGui::SetNextItemWidth(-1);
  if (ImGui::InputFloat3(u8"##rotin", &e.x, "%.2f")) {
    SetBoneLocalRot(t, Quat::FromEulerDeg(e));
  }
  static float s_rotStep = 1.0f;
  ImGui::PushID("rotstep");
  ImGui::TextDisabled(u8"步长");
  ImGui::SameLine();
  ImGui::SetNextItemWidth(90);
  ImGui::InputFloat(u8"##v", &s_rotStep, 0.0f, 0.0f, "%.2f");
  ImGui::PopID();
  ImGui::TextDisabled("X");
  ImGui::SameLine();
  {
    Vec3 ee = e;
    if (AxisStepper("rx", &ee.x, s_rotStep))
      SetBoneLocalRot(t, Quat::FromEulerDeg(ee));
    ImGui::SameLine();
    ImGui::TextDisabled("Y");
    ImGui::SameLine();
    if (AxisStepper("ry", &ee.y, s_rotStep)) {
      SetBoneLocalRot(t, Quat::FromEulerDeg(ee));
    }
    ImGui::SameLine();
    ImGui::TextDisabled("Z");
    ImGui::SameLine();
    if (AxisStepper("rz", &ee.z, s_rotStep)) {
      SetBoneLocalRot(t, Quat::FromEulerDeg(ee));
    }
  }
  Vec3 p = GetBoneLocalPos(t);
  ImGui::TextDisabled(u8"\u4f4d\u7f6e (XYZ)");
  ImGui::SetNextItemWidth(-1);
  if (ImGui::SliderFloat3(u8"##pos", &p.x, -1.0f, 1.0f, "%.3f")) {
    SetBoneLocalPos(t, p);
  }
  ImGui::SetNextItemWidth(-1);
  if (ImGui::InputFloat3(u8"##posin", &p.x, "%.4f")) {
    SetBoneLocalPos(t, p);
  }
  static float s_posStep = 0.01f;
  ImGui::PushID("posstep");
  ImGui::TextDisabled(u8"步长");
  ImGui::SameLine();
  ImGui::SetNextItemWidth(90);
  ImGui::InputFloat(u8"##v", &s_posStep, 0.0f, 0.0f, "%.3f");
  ImGui::PopID();
  ImGui::TextDisabled("X");
  ImGui::SameLine();
  {
    Vec3 pp = p;
    if (AxisStepper("px", &pp.x, s_posStep)) {
      SetBoneLocalPos(t, pp);
    }
    ImGui::SameLine();
    ImGui::TextDisabled("Y");
    ImGui::SameLine();
    if (AxisStepper("py", &pp.y, s_posStep)) {
      SetBoneLocalPos(t, pp);
    }
    ImGui::SameLine();
    ImGui::TextDisabled("Z");
    ImGui::SameLine();
    if (AxisStepper("pz", &pp.z, s_posStep)) {
      SetBoneLocalPos(t, pp);
    }
  }
  if (ImGui::Button(u8"\u590d\u4f4d\u65cb\u8f6c"))
    ResetBoneRot(t);
  ImGui::SameLine();
  if (ImGui::Button(u8"\u590d\u4f4d\u4f4d\u7f6e"))
    ResetBonePos(t);
  ImGui::SameLine();
  if (ImGui::Button(u8"\u5168\u90e8\u590d\u4f4d")) {
    ResetBoneRot(t);
    ResetBonePos(t);
  }
  int hi = FindTransformIndex(t);
  if (hi >= 0) {
    ImGui::SameLine();
    bool lk = s_humanBones[hi].locked;
    if (ImGui::Checkbox(u8"\u9501\u5b9a", &lk))
      s_humanBones[hi].locked = lk;
  } else {
    ImGui::SameLine();
    ImGui::TextDisabled(u8"(\u4ece\u9aa8)");
  }
}

static void DrawBoneTreePanel() {
  if (!g_showBoneParams)
    return;
  // 默认放主面板右侧：主面板高度随状态变化（冻结后会多出 root 滑条），放左下会重叠
  ImGui::SetNextWindowPos(ImVec2(380, 10), PanelPositionCondition());
  // AlwaysAutoResize 配 SetNextItemWidth(-1) 会让窗口宽度塌得很窄（标签被挤出可视区），
  // 所以给个最小宽度约束：高度自适应、宽度不低于 340。
  ImGui::SetNextWindowSizeConstraints(ImVec2(340.0f, 100.0f),
                                      ImVec2(FLT_MAX, FLT_MAX));
  if (!ImGui::Begin(u8"\u9aa8\u9abc\u53c2\u6570", &g_showBoneParams,
                    ImGuiWindowFlags_NoCollapse |
                        ImGuiWindowFlags_AlwaysAutoResize |
                        (g_pinPanels ? ImGuiWindowFlags_NoMove : 0))) {
    ImGui::End();
    return;
  }
  ImGui::TextDisabled(u8"\u5728 3D \u89c6\u56fe\u91cc\u70b9\u9009\u9aa8\u9abc\uff08\u9700\u52fe\u9009\u5168\u91cf\u9aa8\u9abc\uff09");
  ImGui::Separator();
  DrawBoneParams();
  DrawIkPanel();
  ImGui::End();
}
