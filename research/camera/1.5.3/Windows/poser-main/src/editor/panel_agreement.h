#pragma once

// 首次启动的「用户协议」弹窗（v0.3.6 起）。
//
// 行为：
//   - 没同意之前，插件不启用编辑或播放功能
//     （见 poser.cpp 的 InitThread 与 GameFrameTick 里的闸门）；
//   - 条款必须滚到底，「同意并继续」才可点；
//   - 同意后把 terms_version=<当前版本> 写进 plugin\poser_config.txt，之后不再打扰；
//   - 改了条款就把 POSER_TERMS_VERSION 加一，老用户会再看一次。
//
// 文案口径与仓库 README 的《用户协议与免责声明》一致，这里只列要点。
// 注意：游戏内字体只含 ImGui 的「常用简体字」表（2500 字），文案请只用常用字，
// 否则会显示成方框。加字时可先在 imgui_draw.cpp 的常用字表里对一下。

#include "imgui.h"
#include "config.h" // g_termsAcceptedVersion / SaveConfigValue

#define POSER_TERMS_VERSION 1

static bool g_termsDeclined = false; // 本次运行里点了「不同意」（或按热键收起了弹窗）
// 指针是否落在协议窗口上（每帧由 DrawAgreementDialog 更新）。
// gui_overlay 靠它决定"要不要吃这次鼠标" —— 只在弹窗上吃，别的地方照旧给游戏，
// 否则游戏自己的菜单/退出按钮会被我们吞掉（实测：整个游戏变成点不动）。
static bool g_termsHover = false;

static inline bool TermsAccepted() {
  return g_termsAcceptedVersion >= POSER_TERMS_VERSION;
}
// TermsPending() / TermsReopen() 的定义在 poser.cpp（gui_overlay.h 要用，
// 但它比本文件先被包含，所以这两个走普通函数声明）。

// 条款正文，一行一条，空行分段。
static const char *kTermsLines[] = {
    "一、非官方工具",
    "本插件是第三方工具，与《明日方舟：终末地》的开发商 / 发行商无关，"
    "也未经其授权或认可。",
    "",
    "二、账号风险",
    "本插件与游戏客户端在同一进程中运行，并修改其运行时的数据。这类使用方式可能"
    "违反游戏服务条款，存在账号被限制或封禁的风险。请自行评估并承担全部后果，"
    "建议先用小号 / 测试账号体验，并且只在单人 / 摄影模式下使用。",
    "",
    "三、游戏资产",
    "本插件不包含任何游戏美术资产。游戏内置的动画、场景、模型等资产，版权完全"
    "属于其权利人，并不适用 AGPL-3.0 协议。",
    "",
    "四、内容与行为约束",
    "不得利用本插件或游戏内置资产，制作、播放或传播任何不合适的动作 / 动画"
    "（包括但不限于色情、暴力、政治敏感等违反法律或引起社区不适的内容）。",
    "不得用于多人联机、竞技或任何会影响他人体验的场景；不得修改或传播任何"
    "付费内容与游戏资源。",
    "",
    "五、许可与分发",
    "本项目在 GitHub 上免费开源，整体按 AGPL-3.0 发布。如果您是通过付费渠道获得"
    "本插件的，可以直接去仓库免费获取；请不要售卖本插件本体却不提供仓库地址与售后支持。"
    "完整条款见仓库 README 的「用户协议与免责声明」。",
    "",
    "六、免责",
    "作者不对使用本插件造成的任何损失负责（包括但不限于账号封禁、数据丢失、"
    "设备异常）。不接受以上任何一条，请不要继续使用。",
};

// 画协议弹窗。toggleHotkey = 当前生效的面板热键（写成 "L" / "Ctrl+Insert" 这样）。
// 返回 1 = 本帧点了「同意」，-1 = 本帧点了「不同意」，0 = 还在看。
// reviewMode = 用户**已经同意过**、从面板里点「用户协议」回看：
// 只读展示，按钮变成「关闭」，也不要求滚到底。
static int DrawAgreementDialog(const char *toggleHotkey, bool reviewMode) {
  static bool s_reachedBottom = false;
  int result = 0;

  ImGuiIO &io = ImGui::GetIO();
  ImGui::SetNextWindowPos(ImVec2(io.DisplaySize.x * 0.5f, io.DisplaySize.y * 0.5f),
                          ImGuiCond_Always, ImVec2(0.5f, 0.5f));
  ImGui::SetNextWindowSize(ImVec2(620.0f, 480.0f), ImGuiCond_Always);
  ImGui::Begin(reviewMode ? u8"用户协议与免责声明（已同意）"
                          : u8"用户协议与免责声明",
               nullptr,
               ImGuiWindowFlags_NoCollapse | ImGuiWindowFlags_NoResize |
                   ImGuiWindowFlags_NoSavedSettings |
                   ImGuiWindowFlags_NoMove);
  g_termsHover = ImGui::IsWindowHovered(ImGuiHoveredFlags_ChildWindows |
                                        ImGuiHoveredFlags_AllowWhenBlockedByActiveItem);

  // 开头这段固定显示（不随条款滚动），把「同意 / 不同意分别会发生什么」讲清楚。
  // 热键用实际生效的那个：用户可能早就把默认的 L 改掉了。
  {
    char intro[320] = {};
    if (reviewMode)
      snprintf(intro, sizeof(intro),
               u8"以下是你此前同意的条款，可随时回看。点「关闭」回到面板"
               u8"（按 %s 也可以直接关掉本窗口）。",
               toggleHotkey);
    else
      snprintf(intro, sizeof(intro),
               u8"请读完下面的条款再决定。点「同意并继续」后，本插件才会加载与游戏相关的"
               u8"功能；点「不同意」则本次不加载任何功能（之后按 %s 可以把本窗口叫回来）。",
               toggleHotkey);
    ImGui::PushTextWrapPos(0.0f);
    ImGui::TextUnformatted(intro);
    ImGui::PopTextWrapPos();
    ImGui::Separator();
  }

  // 高度留出下面那几行（输入提示 / 读到底提示 / 按钮行）
  ImGui::BeginChild("##terms", ImVec2(0, -104.0f), ImGuiChildFlags_Borders);
  ImGui::PushTextWrapPos(0.0f);
  for (const char *line : kTermsLines) {
    if (line[0] == 0) {
      ImGui::Spacing();
      continue;
    }
    ImGui::TextUnformatted(line);
  }
  ImGui::PopTextWrapPos();
  // 滚到底（内容不够长时 ScrollMaxY == 0，同样算读完）
  if (ImGui::GetScrollY() >= ImGui::GetScrollMaxY() - 1.0f)
    s_reachedBottom = true;
  ImGui::EndChild();

  // 输入提示：游戏平时把系统光标藏起来（鼠标锁在窗口里转视角），那种状态下鼠标
  // 停在屏幕中间动不了 —— 要按游戏自带的 Alt 才会呼出光标。这里按实际情况提示。
  {
    CURSORINFO ci;
    ci.cbSize = sizeof(ci);
    bool osCursorShown = GetCursorInfo(&ci) && (ci.flags & CURSOR_SHOWING) != 0;
    if (!osCursorShown)
      ImGui::TextColored(ImVec4(1.00f, 0.78f, 0.25f, 1.0f),
                         u8"看不到鼠标？按住 Alt 呼出游戏光标，再点下面的按钮。");
    else if (reviewMode)
      ImGui::TextDisabled(u8"鼠标可用：看完点「关闭」即可。");
    else
      ImGui::TextDisabled(u8"鼠标可用：先读到底，再点「同意并继续」。");
  }

  if (!reviewMode && !s_reachedBottom)
    ImGui::TextDisabled(u8"请把上面的条款读到底，「同意并继续」才会变成可点。");

  if (reviewMode) {
    if (ImGui::Button(u8"关闭", ImVec2(160.0f, 0.0f)))
      result = 1;
  } else {
    if (!s_reachedBottom)
      ImGui::BeginDisabled();
    if (ImGui::Button(u8"同意并继续", ImVec2(160.0f, 0.0f)))
      result = 1;
    if (!s_reachedBottom)
      ImGui::EndDisabled();

    ImGui::SameLine();
    if (ImGui::Button(u8"不同意（本次不加载）", ImVec2(180.0f, 0.0f)))
      result = -1;
  }

  ImGui::SameLine();
  ImGui::TextDisabled(u8"（v%d）", POSER_TERMS_VERSION);

  ImGui::End();
  return result;
}

static bool g_showUserAgreement = false;
static bool DrawUserAgreement() {
  const bool required = !poser_agreement::Allowed();
  if (!required && !g_showUserAgreement) return false;
  char hotkey[48] = {};
  HotkeyDisplay(g_guiToggleVK, g_guiToggleCtrl, hotkey, sizeof(hotkey));
  const int result = DrawAgreementDialog(hotkey, !required);
  if (result == 1 && required) {
    char version[16] = {};
    snprintf(version, sizeof(version), "%d", POSER_TERMS_VERSION);
    if (SaveConfigValue("terms_version", version)) {
      g_termsAcceptedVersion = POSER_TERMS_VERSION;
      poser_agreement::accepted.store(true);
      InterlockedExchange(&g_hotkeyFreezeReq, 0);
      InterlockedExchange(&g_mmdHotkeyRequests, 0);
      InterlockedExchange(&g_leftClickReq, 0);
    }
  } else if (result != 0) {
    g_showUserAgreement = false;
    if (required) g_guiVisible = false;
  }
  return true;
}
