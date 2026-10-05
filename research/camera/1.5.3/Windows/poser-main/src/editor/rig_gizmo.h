#pragma once

// 游戏内简易摆姿（UE 风格旋转盘）：
//   冻结后角色上直接渲染骨骼线/关节点；点击关节点选中；选中骨上显示
//   ImGuizmo ROTATE 旋转盘，拖动 = FK 旋转该骨（localRotation 写回）。
// 只做旋转、不做 IK；仅冻结态可用；与 Blender 控制 Rig 并存（都走 /api/pose 语义）。
// 相机/矩阵代码沿用旧 gizmo.h（git a28d143^），简化去掉平移与诊断。

#include "imgui.h"
#include "ImGuizmo.h"
#include "math/quat_math.h"
#include "core/game_hooks.h"
#include "game/skeleton.h"
#include "game/freeze.h"
#include "editor/selection.h"

#include <cmath>
#include <cstring>

// 显示开关（主窗口复选框）
static bool g_showBones = true;

// 全量骨骼开关：勾选后在叠加层展示/可拾取所有骨骼（含手指等），用于精细微调
// 默认关 = 只显示主要(Humanoid)骨骼；全量收集始终进行（Blender 桥依赖），仅叠加层按此开关切换
static bool g_fullBones = false;
// 叠加层绘制/点选是否处理这根骨：humanoid 骨始终显示；非 humanoid 先按命名过滤掉
// 内部辅助骨（Nub/Twist/corrective/碰撞体/表情骨/inner/outer/wep…），其余只在
// "全量骨骼"模式下显示（非全量模式 = 只有 humanoid，画面干净）。
static bool RigShowBone(void *t, const char *name, bool useAll) {
  if (!t)
    return false;
  bool isHuman = FindTransformIndex(t) >= 0;
  if (!isHuman && IsNoisyBoneName(name))
    return false;
  return useAll || isHuman;
}

// 叠加层状态（面板直接显示，方便排查）
static char g_overlayStatus[128] = "off";

// 上一帧叠加层投影的关节屏幕坐标缓存（供 WM_NCHITTEST 命中测试，overlay 客户端坐标）
// 关节缓存：供输入路由做命中测试（hover → 覆盖层吃鼠标）。必须能装下整条骨架：
// humanoid 55 根 + 从骨链根上百根，实测角色约 190+，之前 128 会溢出，导致排在后面
// 的关节 hover 永远为 false、点击被路由给游戏（表现为"有些关节能点有些点不动"）。
static const int kMaxJointCache = 1024;
static float g_jointSx[kMaxJointCache] = {};
static float g_jointSy[kMaxJointCache] = {};
static void *g_jointTrans[kMaxJointCache] = {};
static int g_jointCount = 0;

// ---- 列主序 4x4 基础 ----
static void Mat4Identity(float m[16]) {
  for (int i = 0; i < 16; i++)
    m[i] = 0;
  m[0] = m[5] = m[10] = m[15] = 1;
}

static void Mat4Compose(const Vec3 &pos, const Quat &q, float m[16]) {
  float x = q.x, y = q.y, z = q.z, w = q.w;
  float x2 = x + x, y2 = y + y, z2 = z + z;
  float xx = x * x2, xy = x * y2, xz = x * z2;
  float yy = y * y2, yz = y * z2, zz = z * z2;
  float wx = w * x2, wy = w * y2, wz = w * z2;
  m[0] = 1 - (yy + zz); m[1] = xy + wz;     m[2] = xz - wy;     m[3] = 0;
  m[4] = xy - wz;       m[5] = 1 - (xx + zz); m[6] = yz + wx;   m[7] = 0;
  m[8] = xz + wy;       m[9] = yz - wx;     m[10] = 1 - (xx + yy); m[11] = 0;
  m[12] = pos.x;        m[13] = pos.y;      m[14] = pos.z;     m[15] = 1;
}

static void Mat4Decompose(const float m[16], Vec3 &pos, Quat &q) {
  pos = {m[12], m[13], m[14]};
  float tr = m[0] + m[5] + m[10];
  float w, x, y, z;
  if (tr > 0.0f) {
    float s = std::sqrt(tr + 1.0f) * 2.0f;
    w = 0.25f * s;
    x = (m[6] - m[9]) / s;
    y = (m[8] - m[2]) / s;
    z = (m[1] - m[4]) / s;
  } else if (m[0] > m[5] && m[0] > m[10]) {
    float s = std::sqrt(1.0f + m[0] - m[5] - m[10]) * 2.0f;
    w = (m[6] - m[9]) / s;
    x = 0.25f * s;
    y = (m[1] + m[4]) / s;
    z = (m[8] + m[2]) / s;
  } else if (m[5] > m[10]) {
    float s = std::sqrt(1.0f + m[5] - m[0] - m[10]) * 2.0f;
    w = (m[8] - m[2]) / s;
    x = (m[1] + m[4]) / s;
    y = 0.25f * s;
    z = (m[6] + m[9]) / s;
  } else {
    float s = std::sqrt(1.0f + m[10] - m[0] - m[5]) * 2.0f;
    w = (m[1] - m[4]) / s;
    x = (m[8] + m[2]) / s;
    y = (m[6] + m[9]) / s;
    z = 0.25f * s;
  }
  q = NormQ(Quat{x, y, z, w});
}

static void Mat4Mul(const float a[16], const float b[16], float out[16]) {
  for (int c = 0; c < 4; c++)
    for (int r = 0; r < 4; r++)
      out[c * 4 + r] =
          a[0 * 4 + r] * b[c * 4 + 0] + a[1 * 4 + r] * b[c * 4 + 1] +
          a[2 * 4 + r] * b[c * 4 + 2] + a[3 * 4 + r] * b[c * 4 + 3];
}

// ---- row-major 矩阵（ImGuizmo 的 matrix_t 是 row-major，m[0..3]=row0=right 轴）----
static void Mat4MulRowMajor(const float a[16], const float b[16], float r[16]) {
  for (int row = 0; row < 4; row++)
    for (int col = 0; col < 4; col++)
      r[row * 4 + col] =
          a[row * 4 + 0] * b[0 * 4 + col] + a[row * 4 + 1] * b[1 * 4 + col] +
          a[row * 4 + 2] * b[2 * 4 + col] + a[row * 4 + 3] * b[3 * 4 + col];
}

// 从 row-major 4x4（第 3 行为 position）提取位置与旋转四元数
static void Mat4DecomposeRowMajor(const float m[16], Vec3 &pos, Quat &q) {
  pos = Vec3{m[12], m[13], m[14]};
  float m00 = m[0], m01 = m[1], m02 = m[2];
  float m10 = m[4], m11 = m[5], m12 = m[6];
  float m20 = m[8], m21 = m[9], m22 = m[10];
  float tr = m00 + m11 + m22;
  float w, x, y, z;
  if (tr > 0.0f) {
    float s = std::sqrt(tr + 1.0f) * 2.0f;
    w = 0.25f * s;
    x = (m21 - m12) / s;
    y = (m02 - m20) / s;
    z = (m10 - m01) / s;
  } else if (m00 > m11 && m00 > m22) {
    float s = std::sqrt(1.0f + m00 - m11 - m22) * 2.0f;
    w = (m21 - m12) / s;
    x = 0.25f * s;
    y = (m01 + m10) / s;
    z = (m02 + m20) / s;
  } else if (m11 > m22) {
    float s = std::sqrt(1.0f + m11 - m00 - m22) * 2.0f;
    w = (m02 - m20) / s;
    x = (m01 + m10) / s;
    y = 0.25f * s;
    z = (m12 + m21) / s;
  } else {
    float s = std::sqrt(1.0f + m22 - m00 - m11) * 2.0f;
    w = (m10 - m01) / s;
    x = (m02 + m20) / s;
    y = (m12 + m21) / s;
    z = 0.25f * s;
  }
  q = NormQ(Quat{x, y, z, w});
}

// 沿父链自根向下组合世界矩阵（local pos/rot 逐级相乘）
static bool GetBoneWorldMatrix(void *transform, float out[16]) {
  if (!transform || !g_transform_get_parent)
    return false;
  __try {
    void *chain[32];
    int n = 0;
    void *cur = transform;
    while (cur && n < 32) {
      chain[n++] = cur;
      cur = Invoke(g_transform_get_parent, cur);
    }
    Mat4Identity(out);
    for (int i = n - 1; i >= 0; i--) {
      float m[16], tmp[16];
      Mat4Compose(GetBoneLocalPos(chain[i]), GetBoneLocalRot(chain[i]), m);
      Mat4Mul(out, m, tmp);
      memcpy(out, tmp, sizeof(tmp));
    }
    return true;
  } __except (1) {
    return false;
  }
}

// 从主相机构建 view + projection（列主序）。Unity 矩阵优先，手动回退。
static bool GetCameraViewProj(float view[16], float proj[16]) {
  static bool s_camDiagLogged = false;
  auto CamLogOnce = [&](const char *why) {
    if (!s_camDiagLogged) {
      s_camDiagLogged = true;
      Log("[RIG] camera unavailable: %s (main=%p transform=%p findOfType=%p)",
          why, g_camera_get_main, g_component_get_transform,
          g_object_FindObjectOfType);
    }
  };
  if (!g_component_get_transform) {
    CamLogOnce("no get_transform");
    return false;
  }
  __try {
    void *cam = Invoke(g_camera_get_main, nullptr);
    if (!cam && g_object_FindObjectOfType && g_cameraClass) {
      // get_main 拿不到时兜底：FindObjectOfType<Camera>() 找任意相机
      void *type = il2cpp_class_get_type(g_cameraClass);
      void *typeObj = type ? il2cpp_type_get_object(type) : nullptr;
      if (typeObj) {
        void *args[] = {typeObj};
        cam = Invoke(g_object_FindObjectOfType, nullptr, args);
        if (cam)
          Log("[RIG] camera fallback FindObjectOfType -> %p", cam);
      }
    }
    if (!cam) {
      CamLogOnce("Camera.get_main returned null and no fallback");
      return false;
    }
    if (g_camera_get_worldToCameraMatrix && g_camera_get_projectionMatrix) {
      void *vbox = Invoke(g_camera_get_worldToCameraMatrix, cam);
      void *pbox = Invoke(g_camera_get_projectionMatrix, cam);
      if (vbox && pbox) {
        memcpy(view, (char *)vbox + 16, 16 * sizeof(float));
        memcpy(proj, (char *)pbox + 16, 16 * sizeof(float));
        return true;
      }
      CamLogOnce("camera matrices returned null");
    }
    void *ct = Invoke(g_component_get_transform, cam);
    if (!ct) {
      CamLogOnce("camera has no transform");
      return false;
    }
    Vec3 pos = GetBoneWorldPos(ct);
    Quat rot = GetBoneWorldRot(ct);
    float wm[16];
    Mat4Compose(pos, rot, wm);
    view[0] = wm[0];  view[1] = wm[4];  view[2] = wm[8];  view[3] = 0;
    view[4] = wm[1];  view[5] = wm[5];  view[6] = wm[9];  view[7] = 0;
    view[8] = wm[2];  view[9] = wm[6];  view[10] = wm[10]; view[11] = 0;
    view[12] = -(wm[0] * pos.x + wm[4] * pos.y + wm[8] * pos.z);
    view[13] = -(wm[1] * pos.x + wm[5] * pos.y + wm[9] * pos.z);
    view[14] = -(wm[2] * pos.x + wm[6] * pos.y + wm[10] * pos.z);
    view[15] = 1;
    float fov = 60.0f;
    if (g_camera_get_fieldOfView) {
      void *boxed = Invoke(g_camera_get_fieldOfView, cam);
      if (boxed)
        fov = *(float *)((char *)boxed + 16);
    }
    ImGuiIO &io = ImGui::GetIO();
    float aspect = io.DisplaySize.y > 1.0f ? io.DisplaySize.x / io.DisplaySize.y
                                           : 1.0f;
    const float nearP = 0.01f, farP = 1000.0f;
    float f = 1.0f / std::tan(fov * 0.5f * 3.14159265358979f / 180.0f);
    proj[0] = f / aspect; proj[1] = 0; proj[2] = 0; proj[3] = 0;
    proj[4] = 0; proj[5] = f; proj[6] = 0; proj[7] = 0;
    proj[8] = 0; proj[9] = 0;
    proj[10] = (farP + nearP) / (nearP - farP); proj[11] = -1.0f;
    proj[12] = 0; proj[13] = 0;
    proj[14] = (2.0f * farP * nearP) / (nearP - farP); proj[15] = 0;
    return true;
  } __except (1) {
    return false;
  }
}

// 世界点投影到屏幕；返回 false = 在相机后方或超出视口
static bool ProjectToScreen(const float view[16], const float proj[16],
                            const Vec3 &w, float &sx, float &sy) {
  float vx = view[0] * w.x + view[4] * w.y + view[8] * w.z + view[12];
  float vy = view[1] * w.x + view[5] * w.y + view[9] * w.z + view[13];
  float vz = view[2] * w.x + view[6] * w.y + view[10] * w.z + view[14];
  float vw = view[3] * w.x + view[7] * w.y + view[11] * w.z + view[15];
  float cx = proj[0] * vx + proj[4] * vy + proj[8] * vz + proj[12] * vw;
  float cy = proj[1] * vx + proj[5] * vy + proj[9] * vz + proj[13] * vw;
  float cw = proj[3] * vx + proj[7] * vy + proj[11] * vz + proj[15] * vw;
  if (fabsf(cw) < 1e-6f)
    return false;
  float nx = cx / cw, ny = cy / cw;
  if (nx < -1.5f || nx > 1.5f || ny < -1.5f || ny > 1.5f)
    return false;
  ImGuiIO &io = ImGui::GetIO();
  sx = (nx + 1.0f) * 0.5f * io.DisplaySize.x;
  sy = (1.0f - ny) * 0.5f * io.DisplaySize.y;
  return true;
}

// ---- 投影上下文：相机优先，失败回退正交前视图（保证骨骼一定能画出来）----
static float g_viewM[16], g_projM[16];
static bool g_useCamera = false;
static float g_fbMinX = 0, g_fbMaxX = 1, g_fbMinY = 0, g_fbMaxY = 1;

static bool ComputeProjection() {
  g_useCamera = GetCameraViewProj(g_viewM, g_projM);
  if (g_useCamera)
    return true;
  static bool s_fbLogged = false;
  g_fbMinX = g_fbMinY = 1e9f;
  g_fbMaxX = g_fbMaxY = -1e9f;
  auto GrowBounds = [&](void *t) {
    if (!t)
      return;
    Vec3 w = GetBoneWorldPos(t);
    if (w.x < g_fbMinX) g_fbMinX = w.x;
    if (w.x > g_fbMaxX) g_fbMaxX = w.x;
    if (w.y < g_fbMinY) g_fbMinY = w.y;
    if (w.y > g_fbMaxY) g_fbMaxY = w.y;
  };
  if (g_fullBones && !s_allBones.empty())
    for (const auto &b : s_allBones)
      GrowBounds(b.transform);
  else
    for (int i = 0; i < s_humanBoneCount; i++)
      GrowBounds(s_humanBones[i].transform);
  if (g_fbMaxX - g_fbMinX < 1e-4f || g_fbMaxY - g_fbMinY < 1e-4f)
    return false;
  // 兜底投影矩阵（正交前视图，与 ProjectBone 的屏幕映射一致）：
  // view = 单位矩阵（相机在原点朝 -Z 看）；proj = 正交 [minX,maxX]x[minY,maxY]
  Mat4Identity(g_viewM);
  // 相机放在角色前方，保证骨点在相机前方（view z < 0），否则 ImGuizmo 不画
  {
    float zSum = 0.0f;
    int zN = 0;
    if (g_fullBones && !s_allBones.empty()) {
      for (const auto &b : s_allBones) {
        if (!b.transform)
          continue;
        zSum += GetBoneWorldPos(b.transform).z;
        zN++;
      }
    } else {
      for (int i = 0; i < s_humanBoneCount; i++) {
        if (!s_humanBones[i].transform)
          continue;
        zSum += GetBoneWorldPos(s_humanBones[i].transform).z;
        zN++;
      }
    }
    if (zN > 0)
      g_viewM[14] = -(zSum / zN + 10.0f);
  }
  Mat4Identity(g_projM);
  float l = g_fbMinX, r = g_fbMaxX, b = g_fbMinY, t = g_fbMaxY;
  const float n = -1000.0f, f = 1000.0f;
  g_projM[0] = 2.0f / (r - l);
  g_projM[5] = 2.0f / (t - b);
  g_projM[10] = -2.0f / (f - n);
  g_projM[12] = -(r + l) / (r - l);
  g_projM[13] = -(t + b) / (t - b);
  g_projM[14] = -(f + n) / (f - n);
  if (!s_fbLogged) {
    s_fbLogged = true;
    Log("[RIG] camera unavailable, using ortho front-view fallback");
  }
  return true;
}

static bool ProjectBone(const Vec3 &w, float &sx, float &sy) {
  if (g_useCamera)
    return ProjectToScreen(g_viewM, g_projM, w, sx, sy);
  ImGuiIO &io = ImGui::GetIO();
  float ww = g_fbMaxX - g_fbMinX, hh = g_fbMaxY - g_fbMinY;
  if (ww < 1e-4f || hh < 1e-4f || io.DisplaySize.x < 1 ||
      io.DisplaySize.y < 1)
    return false;
  sx = (w.x - g_fbMinX) / ww * io.DisplaySize.x;
  sy = (g_fbMaxY - w.y) / hh * io.DisplaySize.y;
  return true;
}

// 渲染骨骼线 + 关节点（世界 → 屏幕，画在背景层）
static void DrawSkeletonOverlay() {
  g_inputHoverGizmo = false; // 输入路由：本帧是否指向可拾取关节（下面重算）
  if (!g_showBones) {
    snprintf(g_overlayStatus, sizeof(g_overlayStatus), "off (checkbox)");
    return;
  }
  static bool s_noBonesLogged = false;
  if (s_humanBoneCount <= 0) {
    snprintf(g_overlayStatus, sizeof(g_overlayStatus),
             "no bones (capture=%d)", s_humanBoneCount);
    if (!s_noBonesLogged) {
      s_noBonesLogged = true;
      Log("[RIG] overlay: no human bones captured yet");
    }
    return;
  }
  g_jointCount = 0;
  static bool s_okLogged = false;
  if (!s_okLogged) {
    s_okLogged = true;
    Log("[RIG] overlay active: bones=%d", s_humanBoneCount);
  }
  if (!ComputeProjection()) {
    snprintf(g_overlayStatus, sizeof(g_overlayStatus), "projection failed");
    return;
  }
  snprintf(g_overlayStatus, sizeof(g_overlayStatus),
           g_useCamera ? "camera ok (bones=%d)" : "ortho fallback (bones=%d)",
           s_humanBoneCount);
  ImDrawList *dl = ImGui::GetBackgroundDrawList();
  bool useAll = g_fullBones && !s_allBones.empty();
  // 父子连线（全骨骼，父关节 → 子关节）
  for (size_t i = 0; i < s_allBones.size(); i++) {
    if (!s_allBones[i].transform)
      continue;
    if (!RigShowBone(s_allBones[i].transform, s_allBones[i].name, useAll))
      continue;
    int pi = s_allBones[i].parentIdx;
    if (pi < 0 || pi >= (int)s_allBones.size())
      continue;
    if (!RigShowBone(s_allBones[pi].transform, s_allBones[pi].name, useAll))
      continue;
    float a[2], b[2];
    if (!ProjectBone(GetBoneWorldPos(s_allBones[pi].transform), a[0], a[1]))
      continue;
    if (!ProjectBone(GetBoneWorldPos(s_allBones[i].transform), b[0], b[1]))
      continue;
    dl->AddLine(ImVec2(a[0], a[1]), ImVec2(b[0], b[1]),
                IM_COL32(150, 170, 200, 160), 1.2f);
  }
  // 关节点（黄=选中，蓝=humanoid，青=其它骨骼，红=锁定）
  for (size_t i = 0; i < s_allBones.size(); i++) {
    void *t = s_allBones[i].transform;
    if (!t)
      continue;
    if (!RigShowBone(t, s_allBones[i].name, useAll))
      continue;
    float sx, sy;
    if (!ProjectBone(GetBoneWorldPos(t), sx, sy))
      continue;
    bool sel = (t == g_selectedTransform) ||
               (g_selectedBone >= 0 && g_selectedBone < s_humanBoneCount &&
                t == s_humanBones[g_selectedBone].transform);
    int hi = FindTransformIndex(t);
    ImU32 col;
    if (sel)
      col = IM_COL32(255, 200, 60, 255);
    else if (hi >= 0)
      col = s_humanBones[hi].locked ? IM_COL32(255, 90, 90, 255)
                                    : IM_COL32(120, 200, 255, 255);
    else
      col = IM_COL32(110, 220, 200, 255); // 非 humanoid（手指等）
    dl->AddCircleFilled(ImVec2(sx, sy), sel ? 6.0f : (useAll ? 3.5f : 4.0f),
                        col);
    if (sel)
      dl->AddCircle(ImVec2(sx, sy), 9.0f, IM_COL32(255, 220, 80, 255), 0,
                    2.0f);
    if (g_jointCount < kMaxJointCache) {
      g_jointSx[g_jointCount] = sx;
      g_jointSy[g_jointCount] = sy;
      g_jointTrans[g_jointCount] = t;
      g_jointCount++;
    }
  }
  // 悬停高亮：光标附近的关节画白色外圈，提示可点击
  ImGuiIO &io = ImGui::GetIO();
  // 输入路由：指针靠近任一关节 → 这片区域归覆盖层（否则点击会被路由给游戏，
  // 3D 视图里就选不中骨头）。半径与 HandleRigClick 的拾取半径保持一致。
  {
    const float pickR = 14.0f;
    for (int i = 0; i < g_jointCount; i++) {
      float dx = g_jointSx[i] - io.MousePos.x;
      float dy = g_jointSy[i] - io.MousePos.y;
      if (dx * dx + dy * dy <= pickR * pickR) {
        g_inputHoverGizmo = true;
        break;
      }
    }
  }
  int hover = -1;
  float hd = 18.0f;
  for (int i = 0; i < g_jointCount; i++) {
    float dx = g_jointSx[i] - io.MousePos.x, dy = g_jointSy[i] - io.MousePos.y;
    float d = std::sqrt(dx * dx + dy * dy);
    if (d < hd) {
      hd = d;
      hover = i;
    }
  }
  bool hoverIsSelected = hover >= 0 && g_jointTrans[hover] &&
                         (g_jointTrans[hover] == g_selectedTransform ||
                          (g_selectedBone >= 0 &&
                           g_jointTrans[hover] ==
                               s_humanBones[g_selectedBone].transform));
  if (hover >= 0 && !hoverIsSelected)
    dl->AddCircle(ImVec2(g_jointSx[hover], g_jointSy[hover]), 10.0f,
                  IM_COL32(255, 255, 255, 230), 0, 1.5f);
}

// 点击拾取：全骨骼（含手指等），点最近的关节点即选中（冻结只限制旋转盘）
static void HandleRigClick() {
  if (!g_showBones)
    return;
  // 点空白处取消选中：轮询到的"左键短按"（见 HotkeyPollThread）。
  // 因为覆盖层平时是可穿透的，点在远处空白处时这次点击不会进我们的窗口，
  // 靠事件根本收不到 —— 只有轮询才判断得到。
  // 排除三种"不该取消"的情况：面板上的点击、旋转盘/关节上的点击、拖拽。
  if (TakeLeftClick()) {
    bool overPanel = ImGui::GetIO().WantCaptureMouse || g_inputTakeMouse;
    bool overGizmoOrJoint = g_inputHoverGizmo || g_inputDragging ||
                            (g_selectedTransform &&
                             ImGuizmo::IsOver(ImGuizmo::ROTATE));
    if (!overPanel && !overGizmoOrJoint) {
      if (g_selectedTransform) {
        Log("[RIG] empty-space click -> deselect");
        SelectTransform(nullptr, nullptr);
      }
      return;
    }
  }
  if (!ImGui::IsMouseClicked(0))
    return;
  if (ImGui::GetIO().WantCaptureMouse)
    return;
  // 鼠标已悬在选中骨的旋转环上：交给 ImGuizmo 拖拽，别当作"点空白"取消选中，
  // 否则想点环会先把骨骼取消、轮盘瞬间消失（造成"线不好点"）。
  if (g_selectedTransform && ImGuizmo::IsOver(ImGuizmo::ROTATE))
    return;
  if (!ComputeProjection())
    return;
  ImGuiIO &io = ImGui::GetIO();
  void *bestT = nullptr;
  const char *bestName = nullptr;
  float bd = 14.0f;
  auto Pick = [&](void *t, const char *nm) {
    if (!t)
      return;
    float sx, sy;
    if (!ProjectBone(GetBoneWorldPos(t), sx, sy))
      return;
    float dx = sx - io.MousePos.x, dy = sy - io.MousePos.y;
    float d = std::sqrt(dx * dx + dy * dy);
    if (d < bd) {
      bd = d;
      bestT = t;
      bestName = nm;
    }
  };
  bool useAll = g_fullBones && !s_allBones.empty();
  if (!s_allBones.empty()) {
    // 与叠加层用同一套过滤：humanoid +（默认开）从骨链根，全量模式下全部
    for (const auto &b : s_allBones)
      if (RigShowBone(b.transform, b.name, useAll))
        Pick(b.transform, b.name);
  } else {
    for (int i = 0; i < s_humanBoneCount; i++)
      Pick(s_humanBones[i].transform, s_humanBones[i].name);
  }
  // 诊断：点击是否到达覆盖层、选中了哪根骨（只在真正点击时打一行）
  Log("[RIG] click -> %s (dist=%.1f, joints=%d)",
      bestT ? (bestName && bestName[0] ? bestName : "(unnamed)") : "none",
      (double)bd, g_jointCount);
  SelectTransform(bestT, bestName); // 点空处 bestT=null → 取消
}

// 选中骨上的旋转盘（ImGuizmo ROTATE / LOCAL）；拖拽 = FK 旋转写回
static bool DrawBoneRotationGizmo() {
  g_inputDragging = false;
  // g_inputHoverGizmo 由 DrawSkeletonOverlay 先算好（关节命中），这里只做叠加
  void *t = g_selectedTransform;
  if (!t && g_selectedBone >= 0 && g_selectedBone < s_humanBoneCount)
    t = s_humanBones[g_selectedBone].transform;
  if (!t)
    return false;
  // 与骨架叠加层共用投影上下文：相机可用用相机，否则用正交前视图兜底，
  // 保证旋转盘在相机不通时也能显示（位置与骨架一致）。
  if (!ComputeProjection())
    return false;
  float delta[16];
  // 拖拽期间持续累加模型矩阵（标准 ImGuizmo 用法），保证矩阵式写法在整个朝向都自洽，
  // 避免"水平时对、转一下就偏"。仅骨选中变化时重新初始化一次。
  static void *s_gizmoT = nullptr;
  static float s_gizmoObj[16];
  if (s_gizmoT != t) {
    Mat4Compose(GetBoneWorldPos(t), GetBoneWorldRot(t), s_gizmoObj);
    s_gizmoT = t;
  }
  static bool s_thicknessSet = false;
  if (!s_thicknessSet) {
    s_thicknessSet = true;
    ImGuizmo::SetRotationLineThickness(4.0f); // 加粗旋转环
  }
  Mat4Identity(delta);
  ImGuiIO &io = ImGui::GetIO();
  ImGuizmo::SetRect(0, 0, io.DisplaySize.x, io.DisplaySize.y);
  ImGuizmo::SetGizmoSizeClipSpace(0.15f);
  bool used = ImGuizmo::Manipulate(g_viewM, g_projM, ImGuizmo::ROTATE,
                                   ImGuizmo::LOCAL, s_gizmoObj, delta);
  // 输入路由用：悬停在旋转环上、或正在拖拽时，覆盖层必须吃掉鼠标
  g_inputHoverGizmo = g_inputHoverGizmo || ImGuizmo::IsOver(ImGuizmo::ROTATE);
  g_inputDragging = ImGuizmo::IsUsing();
  if (used) {
    // 注意：Manipulate 在 LOCAL 模式下已就地更新 s_gizmoObj（matrix = deltaRot * matrix）。
    // 这里绝不能再用返回的 delta 额外累乘，否则每帧应用两遍 → 转圈/闪动。
    Vec3 np;
    Quat newWorld;
    Mat4DecomposeRowMajor(s_gizmoObj, np, newWorld);
    (void)np; // 只做旋转，平移忽略
    // Mat4Compose 与本函数的读法互为转置：compose 用列、decompose 按行，
    // 直接读出的四元数是真实世界旋转的共轭（轴/方向相反），须取共轭还原。
    newWorld = Conj(newWorld);
    if (fabsf(newWorld.x) + fabsf(newWorld.y) + fabsf(newWorld.z) +
            fabsf(newWorld.w - 1.0f) >
        1e-5f) {
      Quat parentWorld{0, 0, 0, 1};
      if (g_transform_get_parent) {
        __try {
          void *parent = Invoke(g_transform_get_parent, t);
          if (parent)
            parentWorld = GetBoneWorldRot(parent);
        } __except (1) {
        }
      }
      // world = parentWorld * local  =>  local = conj(parentWorld) * world
      SetBoneLocalRot(t, NormQ(Conj(parentWorld) * newWorld));
    }
  } else {
    s_gizmoT = nullptr; // 结束拖拽：下次选中该骨时重新初始化
  }
  return used;
}
