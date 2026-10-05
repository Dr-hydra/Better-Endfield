#pragma once

// 四肢 IK 控制器（Blender / ARP 风格的"控制端"）：拖目标点 → 解析式 2-bone IK 解算，
// 结果写回 FK 骨（上臂/前臂、大腿/小腿）。
//
// 只在冻结态解算：冻结后游戏不再写骨，解算结果才不会被动画每帧覆盖——早期 ik_driver.h
// 被删掉就是这个原因（"一改就回弹"）。
// 目标点 / 肘膝参考点都是插件自己的世界坐标数据，不生成游戏对象；肘膝"参考点"直接用
// 当前中间骨位置，等价于"保持当前弯曲平面"，所以启用瞬间不会跳变。

#include "imgui.h"
#include "ImGuizmo.h"

#include "core/game_hooks.h"
#include "editor/rig_gizmo.h" // ComputeProjection / ProjectBone / Mat4* / g_viewM / g_projM
#include "editor/selection.h"
#include "game/freeze.h" // g_frozen
#include "game/skeleton.h"
#include "math/ik_two_bone.h"
#include "math/quat_math.h"

#include <cstring>

struct IkLimb {
  const char *label;
  HumanBodyBones upper, lower, end;
  void *upperT, *lowerT, *endT;
  Vec3 target; // 世界坐标
  Vec3 pole;   // 世界坐标（肘/膝参考点）
  bool enabled;
  bool active;
  bool hasTarget; // 是否已经吸附过（没吸附前坐标是原点，不能画/不能拖）
};

static IkLimb g_ikLimbs[4] = {
    {"左手 IK", LeftUpperArm, LeftLowerArm, LeftHand, nullptr, nullptr, nullptr, {}, {}, false, false},
    {"右手 IK", RightUpperArm, RightLowerArm, RightHand, nullptr, nullptr, nullptr, {}, {}, false, false},
    {"左脚 IK", LeftUpperLeg, LeftLowerLeg, LeftFoot, nullptr, nullptr, nullptr, {}, {}, false, false},
    {"右脚 IK", RightUpperLeg, RightLowerLeg, RightFoot, nullptr, nullptr, nullptr, {}, {}, false, false},
};
static int g_selectedIk = -1;

// 发布前状态：IK 控制器实测未生效（能选中目标点、拖动手柄，但骨骼不跟随），
// 先整体关闭（面板不显示、不解算、不绘制），代码保留待续。
// 想继续调试把它改成 true 即可。
static const bool g_ikFeatureEnabled = false;

static void *IkFindBone(HumanBodyBones b) {
  for (int i = 0; i < s_humanBoneCount; i++)
    if (s_humanBones[i].humanBone == b)
      return s_humanBones[i].transform;
  return nullptr;
}

static void IkRefresh() {
  for (IkLimb &L : g_ikLimbs) {
    L.upperT = IkFindBone(L.upper);
    L.lowerT = IkFindBone(L.lower);
    L.endT = IkFindBone(L.end);
    L.active = L.upperT && L.lowerT && L.endT;
  }
}

// 把目标点吸附到当前末端位置（FK→IK 不跳变）；pole 用当前中间骨位置
static void IkSnapToCurrent(IkLimb &L) {
  if (!L.active)
    return;
  L.target = GetBoneWorldPos(L.endT);
  L.pole = GetBoneWorldPos(L.lowerT);
  L.hasTarget = true;
}

static void IkSolveLimb(IkLimb &L) {
  if (!L.active || !L.enabled)
    return;
  Vec3 a = GetBoneWorldPos(L.upperT);
  Vec3 b = GetBoneWorldPos(L.lowerT);
  Vec3 c = GetBoneWorldPos(L.endT);
  Quat Ra = GetBoneWorldRot(L.upperT);
  Quat Rb = GetBoneWorldRot(L.lowerT);
  Vec3 a0 = a, b0 = b, c0 = c;
  SolveTwoBone(a, b, c, L.target, L.pole, true); // a 不变，b/c 为解算后的位置

  // 上段：当前方向 → 解算方向
  Vec3 dA0 = Norm(b0 - a0), dA1 = Norm(b - a);
  Quat q1{0, 0, 0, 1};
  if (Len(dA0) > 1e-6f && Len(dA1) > 1e-6f)
    q1 = Quat::FromTo(dA0, dA1);
  SetBoneWorldRot(L.upperT, q1 * Ra);

  // 下段：父级转完之后的方向 → 解算方向
  Vec3 dB0 = Norm(c0 - b0), dB1 = Norm(c - b);
  Vec3 dB0r = q1 * dB0;
  Quat q2{0, 0, 0, 1};
  if (Len(dB0r) > 1e-6f && Len(dB1) > 1e-6f)
    q2 = Quat::FromTo(dB0r, dB1);
  SetBoneWorldRot(L.lowerT, q2 * q1 * Rb);
}

static void IkSolveAll() {
  if (!g_ikFeatureEnabled)
    return;
  if (!g_frozen)
    return; // 未冻结时游戏每帧写骨，解算会被覆盖 → 只在冻结态工作
  IkRefresh();
  for (IkLimb &L : g_ikLimbs)
    IkSolveLimb(L);
}

// 目标点在 3D 里画出来：选中(黄) / 悬停(白) / 启用(青) / 关闭(灰)
static void DrawIkControllers() {
  if (!g_ikFeatureEnabled)
    return;
  IkRefresh();
  if (!ComputeProjection())
    return;
  ImDrawList *dl = ImGui::GetBackgroundDrawList();
  ImGuiIO &io = ImGui::GetIO();
  int hover = -1;
  float hoverDist = 14.0f;
  for (int i = 0; i < 4; i++) {
    IkLimb &L = g_ikLimbs[i];
    if (!L.active || !L.hasTarget)
      continue;
    float sx, sy;
    if (!ProjectBone(L.target, sx, sy))
      continue;
    float dx = sx - io.MousePos.x, dy = sy - io.MousePos.y;
    float d = std::sqrt(dx * dx + dy * dy);
    bool isHover = d < hoverDist;
    if (isHover) {
      hoverDist = d;
      hover = i;
    }
    ImU32 col;
    if (i == g_selectedIk)
      col = IM_COL32(255, 200, 60, 255);
    else if (isHover)
      col = IM_COL32(255, 255, 255, 255);
    else if (L.enabled)
      col = IM_COL32(80, 220, 255, 255);
    else
      col = IM_COL32(140, 140, 140, 200);
    float r = (i == g_selectedIk) ? 9.0f : 7.0f;
    dl->AddCircle(ImVec2(sx, sy), r, col, 0, 2.0f);
    dl->AddLine(ImVec2(sx - r - 3, sy), ImVec2(sx + r + 3, sy), col, 1.0f);
    dl->AddLine(ImVec2(sx, sy - r - 3), ImVec2(sx, sy + r + 3), col, 1.0f);
    // 目标 → 末端骨 的连线，方便看是哪条肢
    float ex, ey;
    if (ProjectBone(GetBoneWorldPos(L.endT), ex, ey))
      dl->AddLine(ImVec2(sx, sy), ImVec2(ex, ey), IM_COL32(80, 220, 255, 120),
                  1.2f);
  }
  if (hover >= 0)
    g_inputHoverGizmo = true; // 交给输入路由：这点该由覆盖层吃鼠标

  if (ImGui::IsMouseClicked(0) && !io.WantCaptureMouse) {
    if (hover >= 0) {
      g_selectedIk = hover;
      SelectTransform(nullptr, nullptr); // 与普通骨选中互斥
      Log("[IK] selected '%s'", g_ikLimbs[hover].label);
    } else if (g_selectedIk >= 0) {
      g_selectedIk = -1; // 点空白取消控制器选中
    }
  }
}

// 选中控制器时的平移手柄（ImGuizmo TRANSLATE，世界坐标）
static void DrawIkGizmo() {
  if (!g_ikFeatureEnabled)
    return;
  if (g_selectedIk < 0 || g_selectedIk >= 4)
    return;
  IkLimb &L = g_ikLimbs[g_selectedIk];
  if (!L.active || !L.hasTarget)
    return;
  ImGuiIO &io = ImGui::GetIO();
  ImGuizmo::SetRect(0, 0, io.DisplaySize.x, io.DisplaySize.y);
  ImGuizmo::SetGizmoSizeClipSpace(0.15f);
  static int s_gizmoLimb = -1;
  static float s_mat[16];
  if (s_gizmoLimb != g_selectedIk) {
    Mat4Compose(L.target, Quat{0, 0, 0, 1}, s_mat);
    s_gizmoLimb = g_selectedIk;
  }
  s_mat[12] = L.target.x;
  s_mat[13] = L.target.y;
  s_mat[14] = L.target.z;
  float delta[16];
  Mat4Identity(delta);
  bool used = ImGuizmo::Manipulate(g_viewM, g_projM, ImGuizmo::TRANSLATE,
                                   ImGuizmo::WORLD, s_mat, delta);
  if (ImGuizmo::IsUsing())
    g_inputDragging = true; // 拖拽中必须持续吃鼠标，否则松开消息会丢
  if (used) {
    Vec3 np{s_mat[12], s_mat[13], s_mat[14]};
    Vec3 d = np - L.target;
    L.target = np;
    L.pole = L.pole + d; // 弯曲平面跟着平移，避免肘膝乱翻
  }
}

static void DrawIkPanel() {
  if (!g_ikFeatureEnabled)
    return;
  ImGui::Separator();
  ImGui::TextDisabled(u8"控制器（IK，仅冻结态生效）");
  if (!g_frozen)
    ImGui::TextDisabled(u8"先冻结再启用");
  for (int i = 0; i < 4; i++) {
    IkLimb &L = g_ikLimbs[i];
    ImGui::PushID(i);
    bool en = L.enabled;
    if (ImGui::Checkbox(L.label, &en)) {
      if (en && !L.enabled)
        IkSnapToCurrent(L); // 启用瞬间吸附到当前姿态，不跳变
      L.enabled = en;
    }
    if (!L.active) {
      ImGui::SameLine();
      ImGui::TextDisabled(u8"(缺骨)");
    }
    ImGui::SameLine();
    if (ImGui::SmallButton(u8"吸附"))
      IkSnapToCurrent(L);
    if (ImGui::IsItemHovered())
      ImGui::SetTooltip(u8"把目标点吸附到当前末端位置（FK→IK 不跳变）");
    ImGui::SameLine();
    if (ImGui::SmallButton(u8"选中"))
      g_selectedIk = i;
    ImGui::PopID();
  }
}
