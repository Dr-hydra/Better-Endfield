// Endfield Poser 插件壳。
// 依赖: core/base.h, core/il2cpp_api.h, core/gui_overlay.h, config.h
// 负责：DLL 入口、Applepie 插件协议导出、IL2CPP 解析与 GUI 线程启动。
// 后续阶段的冻结/角色捕获/相机等通过 GameFrameTick() 接入（见 Task 2.1+）。

#include <cstdint>

#include "core/build_features.h"
#include "core/base.h"
#include "core/il2cpp_api.h"
#include "core/runtime_bootstrap.h"
#include "core/runtime_shutdown.h"
#include "core/gui_overlay.h"
#include "core/game_hooks.h"
#include "config.h"
#include "game/skeleton.h"
#include "game/accessory.h"
#include "game/freeze.h"
#include "game/char_state.h"
#include "game/morph.h"
#include "game/smc_morph.h"
#include "editor/selection.h"
#include "editor/rig_gizmo.h"
#include "editor/panel_bones.h"
#include "editor/ik_control.h"
static void PrepareCharacterHandoff(void *nextEntity=nullptr);
static void RebuildCapturedCharacter();
#include "game/roster.h"
#include "editor/panel_library.h"
#include "editor/panel_morph.h"
#include "editor/panel_mmd.h"
#include "editor/panel_mmd_squad.h"
#include "editor/panel_agreement.h"
#include "game/cloth_init.h"
#include "config.h"

// 手动刷新骨骼（面板按钮 / WebUI /api/refresh 共用）
// 面板里的「打开日志」：弹资源管理器并选中 poser_log.txt —— 让非技术用户
// 一步就能把日志拖给作者（路径：<游戏目录>\plugin\poser_log.txt）
static void OpenLogInExplorer() {
  char path[MAX_PATH] = {};
  HMODULE m = GetModuleHandleA("poser.dll");
  if (!m || !GetModuleFileNameA(m, path, MAX_PATH))
    return;
  char *slash = strrchr(path, '\\');
  if (!slash)
    return;
  *slash = 0; // ...\plugin
  char cmd[MAX_PATH + 64] = {};
  snprintf(cmd, sizeof(cmd), "explorer.exe /select,\"%s\\poser_log.txt\"", path);
  Log("[POSER] open log folder: %s", path);
  WinExec(cmd, SW_SHOWNORMAL);
}

static void RefreshCharacterBones();
static std::atomic<bool> g_gameSideReady{false};

#include "core/web_server.h"

// ---- Applepie 插件协议（与 {EIEM}/src/applepie_mgr.h 一致）----
#define APPLEPIE_PLUGIN_API_VERSION 1
#ifdef APPLEPIE_PLUGIN_IMPL
  #define APPLEPIE_PLUGIN_EXPORT extern "C" __declspec(dllexport)
#else
  #define APPLEPIE_PLUGIN_EXPORT extern "C" __declspec(dllimport)
#endif

struct AP_PluginInfo {
  int apiVersion;
  const char *id;
  const char *displayName;
  const char *description;
  const char *configFile;
  bool supportsHotDisable;
};
struct AP_HotkeyInfo {
  const char *name;
  const char *configKey;
  int currentVK;
};

static AP_PluginInfo g_info = {
    APPLEPIE_PLUGIN_API_VERSION, "poser", "Endfield Poser",
    "Game photography posing tool (FK/IK + morph keys + camera)",
    "plugin\\poser_config.txt", true};

APPLEPIE_PLUGIN_EXPORT AP_PluginInfo *AP_GetPluginInfo() { return &g_info; }
static std::mutex g_pluginLifecycleMutex;
static bool g_pluginEnabled = true;
static bool g_pluginInitialized = false;
APPLEPIE_PLUGIN_EXPORT bool AP_PluginEnable() {
  std::lock_guard<std::mutex> lock(g_pluginLifecycleMutex);
  if (RuntimeClosing()) return false;
  g_pluginEnabled = true;
  g_editorEnabled.store(true);
  if (g_pluginInitialized) StartGuiThread();
  return true;
}
APPLEPIE_PLUGIN_EXPORT bool AP_PluginDisable() {
  std::lock_guard<std::mutex> lock(g_pluginLifecycleMutex);
  g_pluginEnabled = false;
  g_editorEnabled.store(false);
  if (g_pluginInitialized) StopGuiThread();
  return true;
}
APPLEPIE_PLUGIN_EXPORT bool AP_ReloadConfig() {
  std::lock_guard<std::recursive_mutex> lock(g_poseMutex);
  bool ok = LoadPoserConfig();
  g_ikFeatureEnabled = g_ikEnabled;
  return ok;
}
APPLEPIE_PLUGIN_EXPORT int AP_GetHotkeys(AP_HotkeyInfo *out, int max) {
  // 向管理器声明的热键：管理器面板会列出它们，并按其 configKey 写回 poser_config.txt
  // （改完由管理器调用 AP_ReloadConfig 生效）。
  // 截图热键不声明：插件内没有实现（截图走 tools/screenshot.ps1），
  // 免得管理器里出现一个按了没反应的键；实现好了再加回来。
  const int n = 6;
  if (max < n) return n;
  out[0] = {"Toggle Poser GUI", "gui_toggle_key", g_guiToggleVK};
  out[1] = {"Freeze / Unfreeze", "freeze_key", g_freezeVK};
  const char *labels[] = {"MMD Play", "MMD Pause", "MMD Stop", "MMD Reset"};
  for(int i=0;i<4;++i) out[2+i]={labels[i],k_mmdHotkeyKeys[i],g_mmdHotkeyVK[i]};
  return n;
}
APPLEPIE_PLUGIN_EXPORT void AP_SetLanguage(const char *) {}

// ---- 光标状态：只读游戏状态（游戏自带 Alt 呼出光标），不再强行改写 ----

static int CursorLockState() {
  if (!g_cursor_get_lockState)
    return 0;
  __try {
    void *boxed = Invoke(g_cursor_get_lockState, nullptr);
    return boxed ? *(int *)((char *)boxed + 16) : 0;
  } __except (1) {
    return 0;
  }
}

static bool CursorVisible() {
  if (!g_cursor_get_visible)
    return true;
  __try {
    void *boxed = Invoke(g_cursor_get_visible, nullptr);
    return boxed ? *(bool *)((char *)boxed + 16) : true;
  } __except (1) {
    return true;
  }
}

// ---- 每帧更新（阶段 2+：冻结维持、骨骼列表维护、IK 写回、相机）----
static void PrepareCharacterHandoff(void *nextEntity) {
  // Called before replacing any current actor handle, including delayed
  // captures. Save cached values; never query the outgoing skeleton for them.
  void *oldAnimator = g_charAnimator;
  bool oldAlive = CharAnimatorAlive();
  MmdCharacterChanging(nextEntity);
  SaveCharStateOnSwitch();
  if (g_preserveGripOnHandoff && g_frozen) {
    // The old actor remains posed through its retained writer grip. Physics and
    // facial automation return to the game before their per-target caches move.
    if (g_freezeAccessories && oldAlive) SetAllPhysicsEnabled(true);
    g_frozen = false;
  } else UnfreezeCharacter();
  if (!g_preserveGripOnHandoff) ReleaseGripFor(oldAnimator);
  ResetFreezeWriters();
  ResetSMCState(oldAlive);
  g_curCharKey.clear();
  s_humanBoneCount = 0;
  s_allBones.clear();
  s_accessoryBones.clear();
  s_accessoryChains.clear();
  s_blendShapes.clear();
  s_restCaptured = false;
  ++s_bonesRev;
  SelectTransform(nullptr, nullptr);
  Log("[CHAR] outgoing actor released: %p", oldAnimator);
}
static void RebuildCapturedCharacter() {
  if (CharacterSwitchInProgress() || g_captureEntity != g_mainCharEntity) return;
  const bool changed = g_charChanged;
  g_charChanged = false;
  s_restCaptured = false;
  g_mmd.profileRevision = -1;
  g_mmd.profileAnimator = nullptr;
  SelectTransform(nullptr, nullptr);
  RebuildAllBones();
  RebuildHumanBones();
  RebuildAccessories();
  RebuildBlendShapes();
  ResetSMCState();
  if (s_humanBoneCount > 0) {
    CaptureRestPose();
    IkOnCharacterChanged();
    if (changed)
      RestoreCharStateOnSwitch();
    g_mmd.status = u8"角色骨架已就绪，可校准或播放已导入的动作";
    g_mmd.calibrationStatus = u8"当前角色待校准；播放时优先读取该角色保存的校准";
  }
}
static void UpdateOverlayCursor() {
    // 光标完全交给游戏自己管：按 Alt 显示光标是游戏自带行为，插件不改它的
    // lockState/visible（强行改写会和系统按线程计数的 ShowCursor 状态打架，
    // 表现为"光标没了"）。我们只读取状态：lockState==None 视为光标可用，
    // 面板/关节才吃鼠标。
    {
      int lockNow = CursorLockState();
      bool visNow = CursorVisible();
      g_cursorFreeNow = (lockNow == 0); // 0 = CursorLockMode.None
      (void)visNow;
      // 系统光标被游戏隐藏时补一个软光标（Windows 的显示计数按线程算，指针压在
      // 游戏窗口上时可能画不出来）；游戏自己显示了就不重复画。
      bool osShown = false;
      __try {
        CURSORINFO ci;
        ci.cbSize = sizeof(ci);
        osShown = GetCursorInfo(&ci) && (ci.flags & CURSOR_SHOWING) != 0;
      } __except (1) {
      }
      ImGui::GetIO().MouseDrawCursor = g_cursorFreeNow && !osShown;
    }
}
static void GameFrameTickBody() {
  if (!poser_agreement::Allowed() || !g_gameSideReady.load()) {
    TakeHotkeyFreeze();
    InterlockedExchange(&g_mmdHotkeyRequests, 0);
    return;
  }
  __try {
    if (CharacterSwitchInProgress()) return;
    ConsumeCapturedCharacter();
    RosterService();
    if (g_charChanged)
      RebuildCapturedCharacter();
    // 角色捕获自愈：SetMainCharacter hook 漏触发/时机错过时，
    // 周期性从 PlayerController 补捞当前角色（每 250ms 一次）。
    // 骨骼数为 0 也要补捞：角色切换/场景变化后 g_charAnimator 可能残留
    // 失效指针（非空），此时重建出来是 0 根骨，必须强制重新捕获。
    static ULONGLONG s_nextCapture = 0;
    // 周期确认当前 Animator 还活着：场景切换/换实例后旧对象会被 Destroy，
    // 此时指针非空但已失效，骨骼列表里全是死变换 —— 画面就是"骨架钉在原地"。
    // 侦测到就丢掉捕获，交给下面的补捞逻辑重新抓当前角色。
    static ULONGLONG s_nextAliveCheck = 0;
    static int s_deadStrikes = 0;
    if (GetTickCount64() >= s_nextAliveCheck) {
      s_nextAliveCheck = GetTickCount64() + 1000;
      bool animatorDead = g_charAnimator && !CharAnimatorAlive();
      bool bonesDead = !animatorDead && !CachedBonesAlive();
      if (animatorDead || bonesDead) {
        // 连续两次（约 2 秒）都判死才动手，避免场景加载瞬间的误判
        if (++s_deadStrikes >= 2) {
          s_deadStrikes = 0;
          Log("[POSER] capture invalid (%s) -> unfreeze + drop capture",
              animatorDead ? "animator destroyed" : "bone transforms destroyed");
          // 关键：**先解冻**。否则冻结时关掉的 Animator/IK/物理不会被还原，
          // 游戏侧的角色会一直僵在原地（而插件又已经抓不到它，无法自救）。
          PrepareCharacterHandoff();
          g_editSelection.clearOverride(); // Other live roster actors keep their own grips.
          g_charAnimator = nullptr;
          g_mainCharEntity = nullptr;
          g_charAnimComp = nullptr;
          g_charChanged = false; // 死实例不需要保存状态，也别触发重建双消费
          s_humanBoneCount = 0;
          s_allBones.clear();
          ++s_bonesRev; // Invalidate dependent caches even without a new root.
          s_restCaptured = false;
          SelectTransform(nullptr, nullptr);
          s_nextCapture = 0;
        }
      } else {
        s_deadStrikes = 0;
      }
    }
    // Poll even when the previous actor is alive: a missed hook must not leave
    // us editing a background character. Retry delayed Animator construction.
    ULONGLONG captureNow = GetTickCount64();
    if (captureNow >= s_nextCapture) {
      s_nextCapture = captureNow + 250;
      TryCaptureFromPlayerController();
      if (!g_charChanged && s_humanBoneCount == 0 && CharAnimatorAlive())
        g_charChanged = true;
    }
    if (g_charChanged)
      RebuildCapturedCharacter();
    // 冻结热键（默认 P）：面板按钮万一点不到时的可靠通道（隐藏面板时也生效）。
    // 边沿检测在 HotkeyPollThread 里做——直接用 GetAsyncKeyState 的 bit0 会被
    // 游戏/XXMI 的同键轮询抢掉锁存位（"有时有用有时没用"的根因）。
    if (TakeHotkeyFreeze()) {
      Log("[CTRL] freeze hotkey -> toggle freeze");
      if (MmdOwnsPose() || g_mmd.session.active || g_mmd.preview) { MmdStop(); UnfreezeCharacter(); RestoreBlendShapes(); return; }
      if (g_frozen) {
        UnfreezeCharacter();
        RestoreBlendShapes();
      } else {
        FreezeCharacter();
      }
    }
    // 冻结态维持：每帧强制关闭 Animator/动画组件/IK 组件（游戏会重新启用）
    MaintainFreeze();
    LONG mmdKeys = InterlockedExchange(&g_mmdHotkeyRequests, 0);
    if(mmdKeys)Log("[INPUT] MMD hotkey mask=%ld target=%s preview=%d",mmdKeys,
      g_mmdSquadBridge.hotkeyTarget&&g_mmdSquadBridge.hotkeyTarget()?"squad":"single",g_mmd.preview);
    if (mmdKeys & (1 << 2)) MmdPlaybackCommand(2);
    else if (mmdKeys & (1 << 3)) MmdPlaybackCommand(3);
    else if (mmdKeys & (1 << 1)) MmdPlaybackCommand(1);
    else if (mmdKeys & 1) MmdPlaybackCommand(0);
    MmdTick();
  } __except (1) {
    Log("[POSER] GameFrameTick SEH exception caught");
  }
}

// 手动刷新：重跑角色骨骼/从骨/形态键重建链（某些场景无法切换角色时用）
static void RefreshCharacterBones() {
  MmdStop();
  ConsumeCapturedCharacter();
  TryCaptureFromPlayerController();
  RebuildCapturedCharacter();
  Log("[POSER] Manual bone refresh: human=%d", s_humanBoneCount);
}

// ---- 主面板：控制（冻结）+ 姿态编辑（Task 3.1）----
static void DrawPoserGuiBody() {
  ImGuizmo::BeginFrame(); // ImGuizmo 每帧初始化（draw list / 内部窗口），否则轮盘不绘制
  __try {
    DrawSkeletonOverlay();
  } __except (1) {
    Log("[POSER] DrawSkeletonOverlay exception code=0x%X", GetExceptionCode());
  }
  __try {
    HandleRigClick();
  } __except (1) {
    Log("[POSER] HandleRigClick exception code=0x%X", GetExceptionCode());
  }
  __try {
    if (!MmdOwnsPose()) IkSolveAll();        // 冻结态解算四肢 IK（启用中的控制器）
    if (!MmdOwnsPose()) DrawIkControllers(); // 目标点渲染 + 选中 + 命中标记
  } __except (1) {
    Log("[POSER] IK controllers exception code=0x%X", GetExceptionCode());
  }
  ImGui::SetNextWindowPos(ImVec2(10, 10), PanelPositionCondition());
  // 见 panel_bones.h：AlwaysAutoResize 与 SetNextItemWidth(-1) 并用时需要最小宽度，
  // 否则窗口宽度塌陷、右侧标签（步长等）被挤出可视区。
  ImGui::SetNextWindowSizeConstraints(ImVec2(340.0f, 100.0f),
                                      ImVec2(FLT_MAX, FLT_MAX));
  if (ImGui::Begin("Endfield Poser", nullptr,
                   ImGuiWindowFlags_NoCollapse |
                       ImGuiWindowFlags_AlwaysAutoResize |
                       (g_pinPanels ? ImGuiWindowFlags_NoMove : 0))) {
    ImGui::Text("v%s", POSER_VERSION);
    ImGui::SameLine();
    if (ImGui::SmallButton(u8"用户协议")) g_showUserAgreement = true;
    // 当前实际生效的热键（配置可能是老版本留下的值，别让用户以为"默认就是 L/P"）
    {
      char hk1[48] = {}, hk2[48] = {};
      HotkeyDisplay(g_guiToggleVK, g_guiToggleCtrl, hk1, sizeof(hk1));
      HotkeyDisplay(g_freezeVK, g_freezeCtrl, hk2, sizeof(hk2));
      ImGui::TextDisabled("\u547c\u51fa %s   \u51bb\u7ed3 %s", hk1, hk2);
      ImGui::SameLine();
      if (ImGui::SmallButton(u8"\u6253\u5f00\u65e5\u5fd7"))
        OpenLogInExplorer();
      if (ImGui::IsItemHovered())
        ImGui::SetTooltip(u8"\u5f39\u51fa\u8d44\u6e90\u7ba1\u7406\u5668\u5e76\u9009\u4e2d"
                          u8" plugin\\poser_log.txt\uff08\u53d1\u7ed9\u4f5c\u8005\u5c31"
                          u8"\u62d6\u8fd9\u4e2a\u6587\u4ef6\uff09");
    }
    // 只在真的装了 XXMI/3DMigoto 时才提示撞键，避免没装的用户被无谓打扰
    if (g_hotkeyConflict && g_xxmiDetected)
      ImGui::TextDisabled("\u26a0 %s", g_hotkeyConflictMsg);
    if (ImGui::CollapsingHeader(u8"快捷键设置")) {
      DrawHotkeySetting(u8"\u547c\u51fa / \u9690\u85cf\u9762\u677f",
                        "gui_toggle_key", &g_guiToggleVK, &g_guiToggleCtrl, 1);
      DrawHotkeySetting(u8"\u51bb\u7ed3 / \u89e3\u51bb", "freeze_key",
                        &g_freezeVK, &g_freezeCtrl, 2);
      const char *mmdLabels[] = {u8"MMD 播放", u8"MMD 暂停", u8"MMD 停止并恢复", u8"MMD 重置到首帧"};
      for (int i=0;i<4;++i)
        DrawHotkeySetting(mmdLabels[i],k_mmdHotkeyKeys[i],&g_mmdHotkeyVK[i],&g_mmdHotkeyCtrl[i],3+i);
      if (g_hotkeyRiskyMsg[0])
        ImGui::TextDisabled("\u26a0 %s", g_hotkeyRiskyMsg);
      ImGui::TextDisabled(u8"\u70b9\u201c\u6539\u952e\u201d\u540e\u6309\u4e0b"
                          u8"\u4f60\u60f3\u7528\u7684\u7ec4\u5408\uff08\u81ea\u52a8"
                          u8"\u5199\u56de poser_config.txt\uff09");
    }
    ImGui::Checkbox(u8"锁定窗口", &g_pinPanels);
    if (ImGui::IsItemHovered())
      ImGui::SetTooltip(u8"\u9501\u5b9a\u9762\u677f\u4f4d\u7f6e\uff1a\u62d6\u706b\u67f4\u4eba\u65f6\u7a97\u53e3\u4e0d\u8ddf\u7740\u52a8\uff1b\u53d6\u6d88\u540e\u53ef\u62d6\u6807\u9898\u79fb\u52a8");
    ImGui::SameLine();
    if (ImGui::SmallButton(u8"重排窗口")) {
      g_pinPanels = false;
      g_resetPanelLayoutFrames = 2;
    }
    ImGui::TextDisabled(u8"按住 Alt 操作面板；拖动标题栏调整布局");
    ImGui::Separator();
    ImGui::TextDisabled(g_charAnimator?u8"当前角色已就绪":u8"等待进入角色场景");
    ImGui::Checkbox(u8"\u663e\u793a\u9aa8\u9abc", &g_showBones);
    ImGui::SameLine();
    ImGui::TextDisabled("%s",
        g_selectedName[0] ? g_selectedName : u8"\u672a\u9009\u4e2d");
    ImGui::Checkbox(u8"\u5168\u91cf\u9aa8\u9abc(\u5fae\u8c03)", &g_fullBones);
    if (ImGui::IsItemHovered())
      ImGui::SetTooltip(u8"\u9ed8\u8ba4\u53ea\u663e\u793a\u4e3b\u8981\u9aa8\u9abc\uff1b\u52fe\u9009\u540e\u53e0\u52a0\u5c42\u5c55\u793a/\u53ef\u62fe\u53d6\u6240\u6709\u9aa8\u9abc\uff08\u542b\u624b\u6307\u7b49\uff09\uff0c\u7528\u4e8e\u7cbe\u7ec6\u5fae\u8c03\u3002");
    if (ImGui::Checkbox(u8"IK \u63a7\u5236\u5668\uff08\u5b9e\u9a8c\u6027\uff09",
                        &g_ikFeatureEnabled)) {
      g_ikEnabled = g_ikFeatureEnabled;
      SaveConfigValue("ik_enabled", g_ikFeatureEnabled ? "1" : "0");
      Log("[IK] feature %s (toggled from the panel)", g_ikFeatureEnabled ? "ON" : "OFF");
    }
    if (ImGui::IsItemHovered())
      ImGui::SetTooltip(u8"\u5b9e\u9a8c\u6027\uff1a\u53ef\u4ee5\u9009\u4e2d / \u62d6\u52a8"
                        u8"\u624b\u67c4\uff1b\u9aa8\u9abc\u8ddf\u968f\u5c1a\u672a\u5b8c\u6210");
    // ---- 窗口开关集中在这里 ----
    // 勾上就出现对应窗口；窗口右上角的 × 也能关（两者是同一个状态）。
    if (ImGui::CollapsingHeader(u8"\u7a97\u53e3")) {
      ImGui::Checkbox(u8"\u9aa8\u9abc\u53c2\u6570", &g_showBoneParams);
      if (ImGui::IsItemHovered())
        ImGui::SetTooltip(u8"\u9009\u4e2d\u9aa8\u7684\u65cb\u8f6c/\u4f4d\u7f6e\u53c2\u6570"
                          u8"\uff08\u6ed1\u6761\u3001\u6570\u503c\u3001\u590d\u4f4d\u3001"
                          u8"\u9501\u5b9a\uff09");
      ImGui::SameLine();
      ImGui::Checkbox(u8"\u59ff\u6001\u5e93", &g_showLibrary);
      if (ImGui::IsItemHovered())
        ImGui::SetTooltip(u8"\u59ff\u6001\u9884\u8bbe\u7684\u4fdd\u5b58 / \u8f7d\u5165 / "
                          u8"\u5220\u9664");
      ImGui::SameLine();
      ImGui::Checkbox(u8"\u5f62\u6001\u952e", &g_showMorph);
      if (ImGui::IsItemHovered())
        ImGui::SetTooltip(u8"\u9762\u90e8 BlendShape + \u6e38\u620f\u539f\u751f SMC "
                          u8"\u8868\u60c5\u6ed1\u6761");
      ImGui::Checkbox(u8"\u89d2\u8272\u5217\u8868", &g_showRoster);
      if (ImGui::IsItemHovered())
        ImGui::SetTooltip(u8"\u573a\u4e0a\u89d2\u8272\u5217\u8868\uff0c\u70b9\u300c"
                          u8"\u7f16\u8f91\u300d\u5207\u6362\u8981\u6446\u7684\u89d2\u8272");
    }
    if (g_fullBones && s_allBones.empty())
      RebuildAllBones();
    if (!g_fullBones && FindTransformIndex(g_selectedTransform) < 0)
      SelectTransform(nullptr, nullptr);
    ImGui::Separator();
    ImGui::Checkbox(u8"MMD 播放器", &g_mmd.show);
    ImGui::Checkbox(u8"MMD 多人播放器", &g_squad.show);
    if (ImGui::Button(g_frozen ? u8"解冻角色" : u8"冻结角色")) {
      bool wasPlaying = MmdOwnsPose();
      if (wasPlaying) MmdStop();
      Log("[GUI] Freeze button clicked (frozen=%d animator=%p bones=%d)",
          (int)g_frozen, g_charAnimator, s_humanBoneCount);
      if (g_frozen || wasPlaying) {
        UnfreezeCharacter();
        RestoreBlendShapes();     // 形态键恢复冻结前原始值
      } else {
        FreezeCharacter();
      }
    }
    ImGui::SameLine();
    if (ImGui::Button(u8"\u5237\u65b0\u9aa8\u9abc")) // 刷新骨骼
      RefreshCharacterBones();
    ImGui::BeginDisabled(MmdOwnsPose());
    if (ImGui::Button(u8"\u5168\u90e8\u91cd\u7f6e")) // 所有骨回到冻结瞬间
      PoseOpResetToFreeze();
    if (ImGui::IsItemHovered())
      ImGui::SetTooltip(u8"\u6240\u6709\u9aa8\uff08\u542b\u4ece\u9aa8\uff09\u56de\u5230\u51bb\u7ed3\u77ac\u95f4\u59ff\u6001\uff0c\u76f8\u5f53\u4e8e\u64a4\u9500\u5168\u90e8\u624b\u52a8\u6446\u59ff");
    bool accPrev = g_freezeAccessories;
    ImGui::Checkbox(u8"\u51bb\u7ed3\u98d8\u5e26/\u88d9\u5b50/\u5934\u53d1",
                    &g_freezeAccessories);
    if (ImGui::IsItemHovered())
      ImGui::SetTooltip(u8"\u9ed8\u8ba4\u5173\uff1a\u51bb\u7ed3\u65f6\u98d8\u5e26/\u88d9\u5b50/\u5934\u53d1\u8ddf\u7740\u4e00\u8d77\u51bb\u4f4f\uff1b\u53d6\u6d88\u52fe\u9009\u5219\u4ece\u9aa8\u4fdd\u6301\u5b9e\u65f6\u6f14\u7b97");
    if (g_frozen && accPrev != g_freezeAccessories) {
      if (g_freezeAccessories) {
        if (s_accessoryChains.empty())
          RebuildAccessories();
        SetAllPhysicsEnabled(false);
      } else {
        SetAllPhysicsEnabled(true);
      }
    }
    ImGui::EndDisabled();
    // 快捷键一览（默认展开，可折叠）
    ImGui::Separator();
    if (ImGui::CollapsingHeader(u8"\u5feb\u6377\u952e",
                                ImGuiTreeNodeFlags_DefaultOpen)) {
      char vkbuf[48];
      HotkeyDisplay(g_guiToggleVK, g_guiToggleCtrl, vkbuf, sizeof(vkbuf));
      ImGui::Text(u8"\u9762\u677f\u663e\u793a/\u9690\u85cf\uff1a%s",
                  vkbuf);
      char fbuf[48];
      HotkeyDisplay(g_freezeVK, g_freezeCtrl, fbuf, sizeof(fbuf));
      ImGui::Text(u8"\u51bb\u7ed3 / \u89e3\u51bb\uff1a%s",
                  fbuf);
      DrawMmdHotkeyHints();
      ImGui::Text(u8"\u9762\u677f\u4ea4\u4e92\uff1a\u6309\u4f4f Alt\uff08\u6216\u6e38\u620f\u653e\u5f00\u5149\u6807\u65f6\u76f4\u63a5\u70b9\uff09");
    }
    // 窗口开关变了就写回配置（点窗口右上角的 × 也会走到这里）
    {
      static bool s_savedWin[4] = {true, false, false, false};
      if (s_savedWin[0] != g_showBoneParams) {
        s_savedWin[0] = g_showBoneParams;
        SaveConfigValue("show_bone_params", g_showBoneParams ? "1" : "0");
      }
      if (s_savedWin[1] != g_showLibrary) {
        s_savedWin[1] = g_showLibrary;
        SaveConfigValue("show_library", g_showLibrary ? "1" : "0");
      }
      if (s_savedWin[2] != g_showMorph) {
        s_savedWin[2] = g_showMorph;
        SaveConfigValue("show_morph", g_showMorph ? "1" : "0");
      }
      if (s_savedWin[3] != g_showRoster) {
        s_savedWin[3] = g_showRoster;
        SaveConfigValue("show_roster", g_showRoster ? "1" : "0");
      }
    }
    if(ImGui::CollapsingHeader(u8"诊断信息")) {
      ImGui::Text("Animator=%p  Bones=%d",g_charAnimator,s_humanBoneCount);
      ImGui::TextWrapped("Overlay: %s",g_overlayStatus);
    }
  }
  ImGui::End();

  // 旋转盘：全屏无交互窗口内绘制，避免被小窗口裁剪
  {
    ImGuiIO &io = ImGui::GetIO();
    ImGui::SetNextWindowPos(ImVec2(0, 0));
    ImGui::SetNextWindowSize(io.DisplaySize);
    ImGui::SetNextWindowBgAlpha(0.0f);
    ImGui::PushStyleVar(ImGuiStyleVar_WindowPadding, ImVec2(0, 0));
    if (ImGui::Begin(u8"##rig_gizmo_layer", nullptr,
                     ImGuiWindowFlags_NoDecoration | ImGuiWindowFlags_NoMove |
                         ImGuiWindowFlags_NoSavedSettings |
                         ImGuiWindowFlags_NoInputs)) {
      __try {
        if (!MmdOwnsPose()) DrawBoneRotationGizmo();
      } __except (1) {
        Log("[POSER] DrawBoneRotationGizmo exception code=0x%X",
            GetExceptionCode());
      }
      __try {
        if (!MmdOwnsPose()) DrawIkGizmo(); // 控制器目标点的平移手柄
      } __except (1) {
        Log("[POSER] DrawIkGizmo exception code=0x%X", GetExceptionCode());
      }
    }
    ImGui::End();
    ImGui::PopStyleVar();
  }

  // 姿态预设库（独立窗口；由主面板「窗口」组开关控制）
  if (g_showLibrary) {
    ImGui::SetNextWindowPos(ImVec2(340, 500), LayoutCond());
    // 与主面板一致：宽度有下限、高度自适应，避免内容被截断
    ImGui::SetNextWindowSizeConstraints(ImVec2(340.0f, 120.0f),
                                        ImVec2(FLT_MAX, FLT_MAX));
    if (ImGui::Begin(u8"\u59ff\u6001\u5e93", &g_showLibrary,
                     ImGuiWindowFlags_NoCollapse |
                         ImGuiWindowFlags_AlwaysAutoResize |
                         0)) {
      DrawLibraryPanel();
    }
    ImGui::End();
  }

  // 形态键面板（面部 BlendShape；由主面板「窗口」组开关控制）
  if (g_showMorph) {
    ImGui::SetNextWindowPos(ImVec2(680, 10), LayoutCond());
    // 形态键面板内部用 BeginChild(size=(0,0)) 填满可用空间，和 AlwaysAutoResize 冲突
    // （子区域会塌成 0 → 内容看不见），所以这里保持固定初始尺寸、允许手动调整。
    ImGui::SetNextWindowSize(ImVec2(360, 320), LayoutCond());
    if (ImGui::Begin(u8"\u5f62\u6001\u952e", &g_showMorph,
                     ImGuiWindowFlags_NoCollapse)) {
      DrawMorphPanel();
    }
    ImGui::End();
  }

  // 骨骼层级面板（Blender 风格：树 + 搜索 + 选中骨参数）
  ImGui::BeginDisabled(MmdOwnsPose());
  DrawBoneTreePanel();
  ImGui::EndDisabled();
  DrawMmdPanel();
  DrawRosterPanel();
  DrawMmdSquadPanel();
  if (g_resetPanelLayoutFrames > 0) --g_resetPanelLayoutFrames;
}

void GameFrameTick() { std::lock_guard<std::recursive_mutex> lock(g_poseMutex); GameFrameTickBody(); }
void DrawPoserGui() {
  std::lock_guard<std::recursive_mutex> lock(g_poseMutex);
  RuntimeThreadScope runtime;
  if (!runtime.ready) return;
  UpdateOverlayCursor();
  if (DrawUserAgreement()) {
    g_inputHoverGizmo = false;
    TakeLeftClick();
    return;
  }
  if (!g_gameSideReady.load()) {
    ImGui::Begin(u8"Endfield Poser");
    ImGui::TextUnformatted(u8"正在等待游戏功能就绪…");
    ImGui::End();
    return;
  }
  if(g_overlayPanelsDraw)DrawPoserGuiBody();
  else {g_inputHoverGizmo=false;g_inputDragging=false;TakeLeftClick();}
  const int countdown=g_mmdCountdownDisplay.load(std::memory_order_acquire);
  if(countdown>0) {
    char label[16]{};snprintf(label,sizeof(label),"%d",countdown);
    auto font=ImGui::GetFont();const float size=ImGui::GetFontSize()*4;
    auto extent=font->CalcTextSizeA(size,FLT_MAX,0,label);
    auto screen=ImGui::GetIO().DisplaySize;
    ImVec2 at((screen.x-extent.x)*.5f,screen.y*.25f);
    auto draw=ImGui::GetForegroundDrawList();
    draw->AddText(font,size,ImVec2(at.x+3,at.y+3),IM_COL32(0,0,0,220),label);
    draw->AddText(font,size,at,IM_COL32(255,255,255,255),label);
  }
}

// 外部控制（PostMessage WM_APP+90 触发，绕过反作弊输入拦截）：
// 1=冻结/解冻 2=T-pose
static void ExtControl(int code) {
  std::lock_guard<std::recursive_mutex> lock(g_poseMutex);
  RuntimeThreadScope runtime;
  if (!runtime.ready) return;
  if (!poser_agreement::Allowed()) return;
  if(MmdOwnsPose() || g_mmd.session.active || g_mmd.preview) { if(code==1){MmdStop();UnfreezeCharacter();} return; }
  switch (code) {
  case 1:
    if (g_frozen) {
      UnfreezeCharacter();
      RestoreBlendShapes();
    } else {
      FreezeCharacter();
    }
    break;
  case 2:
    ApplyTPose();
    break;
  default:
    break;
  }
}

// GUI 线程退出前收尾（在已 attach IL2CPP 的线程上执行）：
// 冻结状态下禁用插件/卸载时，把 Animator、IK、布料物理、形态键都还原回去，
// 否则头发布料会一直僵在冻结姿态。
static void OnGuiShutdownRestore() {
  // During game shutdown its objects are being destroyed, not handed back to
  // gameplay. Avoid restoration, import joins or reattaching to a closing VM.
  if (RuntimeClosing()) {Log("[EXIT] editor stopped; skipped game-object restoration");return;}
  {
  std::lock_guard<std::recursive_mutex> lock(g_poseMutex);
  RuntimeThreadScope runtime;
  if (!runtime.ready) return;
  MmdStop();
  s_mmdClosing.store(true);
  if(HWND dialog=s_mmdDialog.load()) PostMessageW(dialog,WM_CLOSE,0,0);
  if(g_mmd.loading) { g_mmd.loader.wait(); g_mmd.loading=false; }
  if(g_squad.loading) {g_squad.loader.wait();g_squad.loading=false;}
  if(g_mmd.faceLibraryLoading) { g_mmd.faceLoader.wait(); g_mmd.faceLibraryLoading=false; }
  if (g_frozen) {
    Log("[POSER] shutdown: unfreeze + restore (frozen=%d)", (int)g_frozen);
    UnfreezeCharacter();
    RestoreBlendShapes();
  }
  ReleaseAllGrips(); // 后台还冻结着的角色也要把写者还回去
  SMCReleaseFreeze();
  }
  // Let the normal game callback finish cloth restoration without holding the
  // pose lock or issuing Unity cloth commands from the overlay thread.
  const ULONGLONG deadline=GetTickCount64()+1000;
  while (GetTickCount64()<deadline) {
    { std::lock_guard<std::recursive_mutex> lock(g_poseMutex);
      if (!s_cloth.active && !s_cloth.releasing && !ClothSquadRestoring()) return; }
    Sleep(10);
  }
  Log("[CLOTH-RESTORE-PENDING] game callback has not completed shutdown restoration");
}

// 姿态文件扩展：从骨 + 形态键（skeleton.h 通过钩子调用，避免底层反向包含）
static void PoseCaptureExtras(PoseDoc &doc) {
  CollectAccessoryPoseEntries(doc.accBones);
  for (const BlendShapeSlot &s : s_blendShapes) {
    PoseMorph pm;
    pm.name = s.name;
    pm.value = s.value;
    doc.morphs.push_back(pm);
  }
}

static void PoseApplyExtras(const PoseDoc &doc) {
  int accApplied = ApplyAccessoryPoseEntries(doc.accBones);
  int morphApplied = 0;
  for (const PoseMorph &pm : doc.morphs) {
    for (BlendShapeSlot &s : s_blendShapes) {
      if (strcmp(s.name, pm.name.c_str()) != 0)
        continue;
      SetBlendShapeWeight(s, pm.value);
      morphApplied++;
      break;
    }
  }
  if (!doc.accBones.empty() || !doc.morphs.empty())
    Log("[POSER] Applied pose extras: %d/%d accessory bones, %d/%d morphs",
        accApplied, (int)doc.accBones.size(), morphApplied,
        (int)doc.morphs.size());
}

// 控制文件通道：外部（Codex）往 plugin\poser_control.txt 写命令，每帧执行后清空。
// 命令：toggle / freeze / tpose / reset
static void ProcessControlFileBody() {
  // File polling itself must not keep a managed runtime thread registered.
  if (RuntimeClosing() || GetFileAttributesW(PoserFilePath(L"poser_control.txt").c_str())==INVALID_FILE_ATTRIBUTES) return;
  std::lock_guard<std::recursive_mutex> lock(g_poseMutex);
  RuntimeThreadScope runtime;
  if (!runtime.ready) return;
  FILE *f = OpenPoserFile(L"poser_control.txt", L"r");
  if (!f)
    return;
  char line[32768];
  while (fgets(line, sizeof(line), f)) {
    if(strlen(line)>=3 && memcmp(line,"\xef\xbb\xbf",3)==0)
      memmove(line,line+3,strlen(line+3)+1);
    char *e = line + strlen(line) - 1;
    while (e > line && (*e == '\n' || *e == '\r' || *e == ' '))
      *e-- = 0;
    if (!*line)
      continue;
    if (!poser_agreement::Allowed() && strcmp(line, "toggle") != 0) {
      Log("[CTRL] command ignored: user agreement required");
      continue;
    }
    if (strncmp(line, "squad_load ", 11) == 0) {
      char *path=nullptr;int slot=int(strtol(line+11,&path,10));
      if(path&&*path==' '&&slot>=1&&slot<=4)MmdSquadLoad(slot-1,false,std::filesystem::u8path(path+1));
    } else if (strncmp(line,"squad_copy ",11)==0) {
      MmdSquadCopyToAll(atoi(line+11)-1);
    } else if (strncmp(line,"squad_seek ",11)==0) {
      MmdSquadSeek(atof(line+11)/30.);
    } else if (strcmp(line,"squad_play")==0) {
      g_squad.hotkeys=true;MmdSquadCommand(0);
    } else if (strcmp(line,"squad_pause")==0) {
      MmdSquadCommand(1);
    } else if (strcmp(line,"squad_stop")==0) {
      g_squad.hotkeys=true;MmdSquadCommand(2);
    } else if (strcmp(line,"squad_refresh")==0) {
      g_squad.refresh=true;g_squad.show=true;
    } else if (strcmp(line,"squad_line")==0) {
      for(int i=0;i<4;++i)g_squad.slots[i].offset={float(i)-1.5f,0,0};
    } else if (strncmp(line, "mmd_load ", 9) == 0) {
      MmdBeginLoad(0, std::filesystem::u8path(line + 9));
    } else if (strncmp(line, "mmd_append ", 11) == 0) {
      MmdBeginLoad(1, std::filesystem::u8path(line + 11));
    } else if (strncmp(line, "mmd_pmx ", 8) == 0) {
      MmdBeginLoad(2, std::filesystem::u8path(line + 8));
    } else if (strncmp(line, "mmd_rig_load ", 13) == 0) {
      MmdBeginLoad(3, std::filesystem::u8path(line + 13));
    } else if (strncmp(line, "mmd_rig_save ", 13) == 0) {
      MmdBeginLoad(4, std::filesystem::u8path(line + 13));
    } else if (strncmp(line, "mmd_music ", 10) == 0) {
      MmdBeginLoad(5, std::filesystem::u8path(line + 10));
    } else if (strcmp(line, "mmd_play") == 0) {
      MmdPlaybackCommand(0);
    } else if (strcmp(line, "mmd_pause") == 0) {
      MmdPlaybackCommand(1);
    } else if (strcmp(line, "mmd_stop") == 0) {
      MmdPlaybackCommand(2);
    } else if (strcmp(line, "mmd_reset") == 0) {
      MmdPlaybackCommand(3);
    } else if (strcmp(line, "mmd_calibrate_preview") == 0) {
      MmdBeginCalibration();
    } else if (strcmp(line, "mmd_calibrate_confirm") == 0) {
      MmdConfirmCalibration();
    } else if (strcmp(line, "mmd_calibrate_auto") == 0) {
      if (!g_mmd.session.active && !g_mmd.loading && !MmdSquadBusy()) {
        g_mmd.profileRevision = -1;
        MmdPrepareProfile();
      }
    } else if (strncmp(line, "mmd_seek ", 9) == 0) {
      if(MmdSquadBusy()) {MmdSquadSeek(atof(line+9)/30.);continue;}
      if(!g_mmd.session.active) MmdStart();
      if(MmdOwnsPose()) {MmdSeek(atof(line+9)/30.);MmdApplyFrame();}
    } else if (strcmp(line, "toggle") == 0) {
      g_guiVisible = !g_guiVisible;
      Log("[CTRL] file toggle -> %d", (int)g_guiVisible);
    } else if (strcmp(line, "freeze") == 0) {
      if(MmdOwnsPose() || g_mmd.session.active || g_mmd.preview) {MmdStop();UnfreezeCharacter();RestoreBlendShapes();continue;}
      if (g_frozen) {
        UnfreezeCharacter();
        RestoreBlendShapes();
      } else {
        FreezeCharacter();
      }
    } else if (strcmp(line, "tpose") == 0) {
      if(!MmdOwnsPose()) ApplyTPose();
    } else if (strcmp(line, "reset") == 0) {
      if(!MmdOwnsPose()) ApplyPoseSnapshot();
    } else if (strncmp(line, "select ", 7) == 0) {
      const char *boneName = line + 7;
      if (SelectBoneByName(boneName))
        Log("[CTRL] selected bone '%s' -> idx %d", boneName, g_selectedBone);
      else
        Log("[CTRL] bone not found: '%s'", boneName);
    } else if (strcmp(line, "bones") == 0) {
      Log("[CTRL] %d human bones:", s_humanBoneCount);
      for (int i = 0; i < s_humanBoneCount; i++)
        Log("[CTRL]   [%d] name='%s' human=%s", i, s_humanBones[i].name,
            HumanBoneName(s_humanBones[i].humanBone));
    } else {
      Log("[CTRL] unknown command: %s", line);
    }
  }
  fclose(f);
  _wremove(PoserFilePath(L"poser_control.txt").c_str());
}

static void ProcessControlFile() {
  try { ProcessControlFileBody(); }
  catch(const std::exception &e) { MmdStop();g_mmd.status=e.what();Log("[CTRL] command failed: %s",e.what()); }
}

static void SignalGameQuit() {
  // No pose mutex, thread joins or Unity calls on the shutdown notification.
  g_guiRunning=false;g_hotkeyPollRun=0;g_webRunning=false;
  g_frameRunning.store(false);
  // The fallback timer wakes at least every 9ms; avoid racing the GUI closing
  // its event handle by signalling it from another thread here.
  s_mmdClosing.store(true);
  if (HWND dialog=s_mmdDialog.load()) PostMessageW(dialog,WM_CLOSE,0,0);
}
static void FinalizeGameQuit() {
  std::unique_lock<std::recursive_mutex> lock(g_poseMutex,std::try_to_lock);
  if (!lock.owns_lock()) {Log("[EXIT] audio cleanup deferred; editor operation still finishing");return;}
  g_mmd.audio.close();
  Log("[EXIT] music stopped; runtime teardown may continue");
}

static DWORD WINAPI InitThread(LPVOID) {
  OpenLog(PoserFilePath(L"poser_log.txt").c_str());
  Log("[POSER] === Endfield Poser v%s attached (build %s %s) ===",
      POSER_VERSION, __DATE__, __TIME__);
  Log("[BUILD] %s", POSER_BUILD_DESCRIPTION);
  LoadPoserConfig();
  g_ikFeatureEnabled = g_ikEnabled;
  poser_agreement::accepted.store(TermsAccepted());
  poser_gaze::LoadProfiles();
  Log("[AGREEMENT] revision %d: %s", poser_agreement::kRevision,
      poser_agreement::Allowed() ? "already accepted" : "confirmation required");
  MmdSquadInstall();
  g_beforeCharacterChange = PrepareCharacterHandoff;
  g_onFreezeReleased=SMCReleaseFreeze;
  // 注册外部控制回调：PostMessage 通道（绕过反作弊对合成输入的拦截）
  SetExtControl(ExtControl);
  SetExtPollFn(ProcessControlFile);
  SetGuiShutdownFn(OnGuiShutdownRestore);
  g_poseCaptureExtras = PoseCaptureExtras;
  g_poseApplyExtras = PoseApplyExtras;
  ULONGLONG start = GetTickCount64();
  while (!GetModuleHandleW(L"GameAssembly.dll")) {
    if (GetTickCount64() - start >= 180000) {
      Log("[BOOT] GameAssembly.dll load timed out; plugin initialization skipped");
      return 0;
    }
    Sleep(100);
  }
  Log("[POSER] Resolving IL2CPP...");
  if (!Resolve()) {
    Log("[POSER] ERROR: GameAssembly.dll not found or exports missing");
    return 0;
  }
  auto hookStatus = MH_Initialize();
  if (hookStatus != MH_OK && hookStatus != MH_ERROR_ALREADY_INITIALIZED) {
    Log("[BOOT] MinHook initialization failed (%d)", hookStatus);
    return 0;
  }
  if (!WaitForRuntimeReady()) return 0;
  {
    RuntimeThreadScope runtime;
    if (!runtime.ready) return 0;
    g_runtimeQuitSignal = SignalGameQuit;
    g_runtimeQuitFinalize = FinalizeGameQuit;
    InstallRuntimeShutdownHooks();
  }
  {
    std::lock_guard<std::mutex> lock(g_pluginLifecycleMutex);
    g_pluginInitialized = true;
    if (g_pluginEnabled) StartGuiThread();
  }
  // No managed thread is retained while waiting for user input.
  while (!poser_agreement::Allowed() && !RuntimeClosing()) Sleep(50);
  if (RuntimeClosing()) return 0;
  // Prepare offline payloads without keeping a managed thread attached or
  // blocking the UI pose lock while the installed asset index is read.
  ClothInitializeHost();
  std::lock_guard<std::recursive_mutex> poseLock(g_poseMutex);
  RuntimeThreadScope runtime;
  if (!runtime.ready) return 0;
  Log("[POSER] Runtime ready and agreement accepted. Initializing game features.");
  InitGameHooks(); // Task 2.1：SetMainCharacter hook → 捕获 Animator/Entity
  g_characterFramePulse=[](){SampleGameRenderFrame(3);};
  mmd_camera::framePulse=[](){SampleGameRenderFrame(4);};
  mmd_camera::needsCamera=[](){return poser_gaze::motionLock||poser_gaze::settings.mode==eye_gaze::Mode::Camera;};
  mmd_camera::afterCamera=SMCGazeCameraTick;
  InstallSMCFaceHooks(); // Task 4.2：SkeletalMorph 表情 hook（参照 EIEM smc_face.h）
  s_clothThreadId = []() -> DWORD {
    DWORD observed=g_frameGameThreadId.load();
    return observed?observed:(g_gameHwnd?GetWindowThreadProcessId(g_gameHwnd,nullptr):0);
  };
  s_clothHostEnabled=[](){return g_pluginEnabled;};
  s_clothHostIdle=[](){return !MmdOwnsPose()&&!MmdSquadBusy()&&!g_mmd.preview;};
  g_gameMaintenance = []() { ClothServiceActors(); };
  InstallFrameHook();
  mmd_camera::Initialize();
  StartWebServer(); // 独立 UI：localhost HTTP 服务器（浏览器打开控制窗口）
  g_gameSideReady.store(true);

  return 0;
}

BOOL APIENTRY DllMain(HMODULE module, DWORD reason, LPVOID) {
  if (reason == DLL_PROCESS_ATTACH) {
    DisableThreadLibraryCalls(module);
    HANDLE thread = CreateThread(nullptr, 0, InitThread, nullptr, 0, nullptr);
    if (thread) CloseHandle(thread);
  }
  return TRUE;
}
