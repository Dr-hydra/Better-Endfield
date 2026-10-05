#pragma once

// poser_config.txt 读写。参照 {EIEM}/src/eiem_config.h 的 key=value 解析套路。
#include <windows.h>
#include <cstdio>
#include <cstring>
#include "core/plugin_paths.h"

void Log(const char *fmt, ...);

// 呼出/隐藏 GUI：默认 L。
// 为什么不用 F11/F12（连 Ctrl+F12 也不行）：XXMI/3DMigoto 是**直接轮询 F11/F12 的
// 按键状态**，你按 Ctrl+F11 它照样会触发自己那份 —— 只有"根本不碰 F 键"才躲得掉。
// 用字母的代价：游戏内文本框/聊天里打字会误触发（插件自己面板的输入框已做屏蔽）。
static int g_guiToggleVK = 'L';
static bool g_guiToggleCtrl = false;    // 是否要求按住 Ctrl
static int g_screenshotVK = VK_F8;      // 截图
// 冻结 / 解冻：默认 P
static int g_freezeVK = 'P';
static bool g_freezeCtrl = false;
// MMD controls remain available when the overlay is hidden.
static const int k_mmdHotkeyDefaults[4] = {VK_F5, VK_F6, VK_F7, VK_F8};
static int g_mmdHotkeyVK[4] = {VK_F5, VK_F6, VK_F7, VK_F8};
static bool g_mmdHotkeyCtrl[4] = {true, true, true, true};
static const char *k_mmdHotkeyKeys[4] = {"mmd_play_key", "mmd_pause_key", "mmd_stop_key", "mmd_reset_key"};
static bool g_hotkeyConflict = false;   // 配置里还留着易冲突的 F10~F12 → 面板给提示
static char g_hotkeyConflictMsg[192] = "";
static char g_hotkeyRiskyMsg[192] = ""; // 绑成单键（字母/数字…）→ 打字会误触发，提示
static char g_defaultPoseDir[MAX_PATH] = "";
// click_through=1：覆盖层常驻显示，用 WS_EX_LAYERED|TRANSPARENT 做真穿透；
// 按住 Alt 时才取消穿透、由面板吃鼠标。默认 0 = 按住 Alt 才显示覆盖层。
static bool g_clickThrough = true; // 默认常驻 + 真穿透（实测手感更好）
// overlay_mode：0=auto（检测到 XXMI/3DMigoto 的 d3d11.dll 时用分层窗口，否则 DComp）
//               1=强制 DComp   2=强制分层窗口（UpdateLayeredWindow，兼容性最好）
static int g_overlayMode = 0;
// 分层窗口（XXMI/3DMigoto 走的路径）的呈现帧率上限。分层路径要每帧做一次
// GPU→CPU 回读，而 Map() 会等 GPU 队列跑完 —— mod 多的机器上等得久，会卡顿。
// 60 = 默认（够用且流畅）；0 = 不限制；减小它可显著降低对游戏的干扰。
static int g_overlayFps = 60;

// Default pose dir: prefer deriving from poser.dll location (...\plugin\poses)
// so presets work regardless of the game's working directory.
// poser_config.txt can override with default_pose_dir=<path>.
static void ResolveDefaultPoseDir() {
  HMODULE m = GetModuleHandleA("poser.dll");
  char p[MAX_PATH] = {};
  if (m && GetModuleFileNameA(m, p, MAX_PATH)) {
    char *slash = strrchr(p, '\\');
    if (slash)
      *slash = 0;
    snprintf(g_defaultPoseDir, sizeof(g_defaultPoseDir), "%s\\poses", p);
    return;
  }
  snprintf(g_defaultPoseDir, sizeof(g_defaultPoseDir), "plugin\\poses");
}

static int ParseVK(const char *s, int fallback) {
  if (!s || !*s) return fallback;
  const char *p = s;
  if (s[0] == 'V' && s[1] == 'K' && s[2] == '_')
    p = s + 3;
  // 单字母 / 单数字：直接就是 VK 码（A-Z = 0x41-0x5A，0-9 = 0x30-0x39）。
  // 这条必须和 VkName() 的写法对称，否则"面板改键 → 写配置 → 下次解析"会对不上。
  if (p[0] && !p[1]) {
    if (p[0] >= 'A' && p[0] <= 'Z') return p[0];
    if (p[0] >= 'a' && p[0] <= 'z') return p[0] - 'a' + 'A';
    if (p[0] >= '0' && p[0] <= '9') return p[0];
  }
  // 符号名（大小写不敏感）：VK_F12 / F12 / VK_INSERT / INSERT ...
  static const struct {
    const char *name;
    int vk;
  } kNames[] = {
      {"F1", VK_F1},   {"F2", VK_F2},   {"F3", VK_F3},
      {"F4", VK_F4},   {"F5", VK_F5},   {"F6", VK_F6},
      {"F7", VK_F7},   {"F8", VK_F8},   {"F9", VK_F9},
      {"F10", VK_F10}, {"F11", VK_F11}, {"F12", VK_F12},
      {"INSERT", VK_INSERT}, {"DELETE", VK_DELETE},
      {"HOME", VK_HOME},     {"END", VK_END},
      {"PGUP", VK_PRIOR},    {"PGDN", VK_NEXT},
      {"TAB", VK_TAB},       {"ESC", VK_ESCAPE},
  };
  for (const auto &e : kNames) {
    if (_stricmp(p, e.name) == 0)
      return e.vk;
  }
  // 数字/十六进制：0x2D、45 均可（base 0 自动识别前缀）
  return (int)strtoul(p, nullptr, 0);
}

// VK 码 → 可读名字（面板里显示快捷键用）。buf 由调用方提供。
static const char *VkName(int vk, char *buf, size_t sz) {
  if (vk >= VK_F1 && vk <= VK_F12) {
    snprintf(buf, sz, "F%d", vk - VK_F1 + 1);
    return buf;
  }
  const char *name = nullptr;
  switch (vk) {
  case VK_INSERT: name = "Insert"; break;
  case VK_DELETE: name = "Delete"; break;
  case VK_HOME:   name = "Home";   break;
  case VK_END:    name = "End";    break;
  case VK_PRIOR:  name = "PgUp";   break;
  case VK_NEXT:   name = "PgDn";   break;
  case VK_TAB:    name = "Tab";    break;
  case VK_ESCAPE: name = "Esc";    break;
  case VK_SPACE:  name = "Space";  break;
  default: break;
  }
  if (name)
    return name;
  if ((vk >= 'A' && vk <= 'Z') || (vk >= '0' && vk <= '9')) {
    snprintf(buf, sz, "%c", (char)vk);
    return buf;
  }
  snprintf(buf, sz, "0x%02X", vk);
  return buf;
}

static void StripBom(char *line) {
  // 去掉 UTF-8 BOM（EF BB BF），兼容 PowerShell/记事本写出的配置文件
  unsigned char *p = (unsigned char *)line;
  if (p[0] == 0xEF && p[1] == 0xBB && p[2] == 0xBF) {
    char *dst = line;
    char *src = line + 3;
    while ((*dst++ = *src++))
      ;
  }
}

// 解析热键：支持 "CTRL+F11" / "CTRL-F11" / "F11" / "0x7B"，大小写不敏感。
// 修饰键写在前面，返回时把主键写进 *vkOut、是否需要 Ctrl 写进 *ctrlOut。
static void ParseHotkey(const char *s, int *vkOut, bool *ctrlOut, int fallbackVk,
                        bool fallbackCtrl) {
  *vkOut = fallbackVk;
  *ctrlOut = fallbackCtrl;
  if (!s || !*s)
    return;
  char buf[64] = {};
  snprintf(buf, sizeof(buf), "%s", s);
  char *p = buf;
  while (*p == ' ')
    p++;
  bool ctrl = false;
  if (_strnicmp(p, "CTRL+", 5) == 0) {
    ctrl = true;
    p += 5;
  } else if (_strnicmp(p, "CTRL-", 5) == 0) {
    ctrl = true;
    p += 5;
  } else if (_strnicmp(p, "CTRL_", 5) == 0) {
    ctrl = true;
    p += 5;
  }
  *vkOut = ParseVK(p, fallbackVk);
  *ctrlOut = ctrl;
}

// 单独按就安全的热键（游戏/mod 少用、也不可能是打字内容）：
// F1~F12、Insert/Delete/Home/End/PgUp/PgDn/Pause/ScrollLock。
// 单字母/数字/空格这类会打字的键不算"安全"，绑了就给提示（不禁止）。
static bool IsSafeStandaloneKey(int vk) {
  if (vk >= VK_F1 && vk <= VK_F12)
    return true;
  switch (vk) {
  case VK_INSERT:
  case VK_DELETE:
  case VK_HOME:
  case VK_END:
  case VK_PRIOR:
  case VK_NEXT:
  case VK_PAUSE:
  case VK_SCROLL:
    return true;
  default:
    return false;
  }
}

// 撞键后两边同时响应（表现为"F11 有时不灵""F12 触发了别的东西"）。
// 这里只在配置里还留着这些键时给一条提示，不擅自改用户的配置。
static void CheckHotkeyConflicts() {
  // 1) 单键（字母/数字/空格这类"会打字"的键）：不算错，但打字时会误触发，提示一下
  g_hotkeyRiskyMsg[0] = 0;
  char rb[32] = {};
  const char *riskName = nullptr;
  const char *riskWhich = nullptr;
  if (!g_guiToggleCtrl && !IsSafeStandaloneKey(g_guiToggleVK)) {
    riskName = VkName(g_guiToggleVK, rb, sizeof(rb));
    riskWhich = "\u547c\u51fa\u952e";
  } else if (!g_freezeCtrl && !IsSafeStandaloneKey(g_freezeVK)) {
    riskName = VkName(g_freezeVK, rb, sizeof(rb));
    riskWhich = "\u51bb\u7ed3\u952e";
  }
  if (riskName) {
    snprintf(g_hotkeyRiskyMsg, sizeof(g_hotkeyRiskyMsg),
             "%s %s \u662f\u5355\u952e\uff1a\u6e38\u620f\u5185\u6587\u672c"
             "\u6846/\u804a\u5929\u91cc\u6253\u5b57\u4f1a\u8bef\u89e6\u53d1"
             "\uff08\u5efa\u8bae\u6309\u4f4f Ctrl \u91cd\u8bbe\uff09",
             riskWhich, riskName);
  }
  // 2) 裸 F10~F12：和 XXMI/3DMigoto、Steam 截图撞键（只在真装了 XXMI 时提示）
  char b1[32] = {}, b2[32] = {};
  const char *n1 = VkName(g_guiToggleVK, b1, sizeof(b1));
  const char *n2 = VkName(g_freezeVK, b2, sizeof(b2));
  const char *bad = nullptr;
  // 加了 Ctrl 就不再和 XXMI/Steam 的裸 F 键撞了
  if (!g_guiToggleCtrl && g_guiToggleVK >= VK_F10 && g_guiToggleVK <= VK_F12)
    bad = n1;
  else if (!g_freezeCtrl && g_freezeVK >= VK_F10 && g_freezeVK <= VK_F12)
    bad = n2;
  if (!bad) {
    g_hotkeyConflict = false;
    g_hotkeyConflictMsg[0] = 0;
    return;
  }
  g_hotkeyConflict = true;
  snprintf(g_hotkeyConflictMsg, sizeof(g_hotkeyConflictMsg),
           "\u70ed\u952e %s \u53ef\u80fd\u4e0e XXMI/3DMigoto\u3001Steam "
           "\u622a\u56fe\u51b2\u7a81\uff1a\u53ef\u5728\u4e0b\u9762 "
           "\u300c\u5feb\u6377\u952e\u300d\u91cc\u6539\u6210 CTRL+%s",
           bad, bad);
  Log("[CFG] WARN: hotkey '%s' may conflict with XXMI/3DMigoto or Steam "
      "(F10-F12); change gui_toggle_key/freeze_key in poser_config.txt",
      bad);
}

// 热键显示名（含修饰键）
static void HotkeyDisplay(int vk, bool ctrl, char *buf, size_t sz) {
  char tmp[32] = {};
  const char *n = VkName(vk, tmp, sizeof(tmp));
  snprintf(buf, sz, "%s%s", ctrl ? "Ctrl+" : "", n);
}

// 面板里改键后写回 plugin\poser_config.txt：只替换对应那一行，其它行原样保留
static bool SaveHotkeyConfig(const char *keyName, int vk, bool ctrl) {
  const auto path = PoserFilePath(L"poser_config.txt");
  static char lines[80][256];
  int count = 0;
  FILE *f = _wfopen(path.c_str(), L"r");
  if (f) {
    while (count < 80 && fgets(lines[count], sizeof(lines[count]), f))
      count++;
    fclose(f);
  }
  char vkbuf[32] = {};
  const char *vn = VkName(vk, vkbuf, sizeof(vkbuf));
  char newLine[160] = {};
  snprintf(newLine, sizeof(newLine), "%s=%s%s\n", keyName, ctrl ? "CTRL+" : "",
           vn);
  bool replaced = false;
  for (int i = 0; i < count; i++) {
    char *eq = strchr(lines[i], '=');
    if (!eq)
      continue;
    size_t klen = (size_t)(eq - lines[i]);
    while (klen > 0 &&
           (lines[i][klen - 1] == ' ' || lines[i][klen - 1] == '\t'))
      klen--; // 键名后面的空格不算
    if (klen == strlen(keyName) && strncmp(lines[i], keyName, klen) == 0) {
      snprintf(lines[i], sizeof(lines[i]), "%s", newLine);
      replaced = true;
      break;
    }
  }
  if (!replaced && count < 80)
    snprintf(lines[count++], sizeof(lines[0]), "%s", newLine);
  FILE *o = _wfopen(path.c_str(), L"wb");
  if (!o)
    return false;
  for (int i = 0; i < count; i++)
    fwrite(lines[i], 1, strlen(lines[i]), o);
  fclose(o);
  return true;
}

// 往配置末尾追加一行（迁移标记用）
static void AppendConfigLine(const char *line) {
  FILE *f = OpenPoserFile(L"poser_config.txt", L"ab");
  if (!f)
    return;
  fwrite(line, 1, strlen(line), f);
  fclose(f);
}

// 一次性迁移老默认热键：旧版本默认 gui_toggle_key=VK_F12 / freeze_key=VK_F11，
// 而 F11/F12 会被 XXMI/3DMigoto、Steam 截图占用（撞键），0.3.3 起默认改成 L / P。
// 但安装向导**不会覆盖已有配置**（那是为了防止重置用户自定的热键），于是老用户升级后
// 拿到的还是 F12/F11 —— 反馈"按 L 呼不出面板"就是这个原因。
// 这里只迁移"值正好等于旧默认"的那一项，且写一个标记行，之后不再重复迁移
// （用户要是把键改回 F12，标记在，插件就不会再动它）。
static void MigrateLegacyHotkeys() {
  const auto path = PoserFilePath(L"poser_config.txt");
  static char lines[80][256];
  int count = 0;
  FILE *f = _wfopen(path.c_str(), L"r");
  if (!f)
    return;
  while (count < 80 && fgets(lines[count], sizeof(lines[count]), f))
    count++;
  fclose(f);
  bool hasMarker = false, legacyToggle = false, legacyFreeze = false;
  for (int i = 0; i < count; i++) {
    if (strstr(lines[i], "hotkey-migrated")) {
      hasMarker = true;
      continue;
    }
    char *eq = strchr(lines[i], '=');
    if (!eq)
      continue;
    size_t klen = (size_t)(eq - lines[i]);
    while (klen > 0 &&
           (lines[i][klen - 1] == ' ' || lines[i][klen - 1] == '\t'))
      klen--;
    char val[64] = {};
    snprintf(val, sizeof(val), "%s", eq + 1);
    for (char *p = val; *p; p++) {
      if (*p == '\r' || *p == '\n') {
        *p = 0;
        break;
      }
    }
    while (val[0] == ' ')
      memmove(val, val + 1, strlen(val));
    int vk = 0;
    bool ctrl = false;
    if (klen == strlen("gui_toggle_key") &&
        strncmp(lines[i], "gui_toggle_key", klen) == 0) {
      ParseHotkey(val, &vk, &ctrl, VK_F12, false);
      legacyToggle = (!ctrl && vk == VK_F12);
    } else if (klen == strlen("freeze_key") &&
               strncmp(lines[i], "freeze_key", klen) == 0) {
      ParseHotkey(val, &vk, &ctrl, VK_F11, false);
      legacyFreeze = (!ctrl && vk == VK_F11);
    }
  }
  if (hasMarker || (!legacyToggle && !legacyFreeze))
    return;
  if (legacyToggle) {
    g_guiToggleVK = 'L';
    g_guiToggleCtrl = false;
    SaveHotkeyConfig("gui_toggle_key", 'L', false);
  }
  if (legacyFreeze) {
    g_freezeVK = 'P';
    g_freezeCtrl = false;
    SaveHotkeyConfig("freeze_key", 'P', false);
  }
  AppendConfigLine("# hotkey-migrated: F12/F11 -> L/P (XXMI/3DMigoto polls the bare "
                   "F keys; put your own value back here if you prefer it)\n");
  Log("[CFG] migrated legacy hotkeys -> toggle=%s freeze=%s (F11/F12 clash with "
      "XXMI/3DMigoto)",
      legacyToggle ? "L" : "kept", legacyFreeze ? "P" : "kept");
}

static bool LoadPoserConfig() {
  ResolveDefaultPoseDir();
  MigrateLegacyHotkeys();
  FILE *f = OpenPoserFile(L"poser_config.txt", L"r");
  if (!f) return false;
  char line[512];
  while (fgets(line, sizeof(line), f)) {
    StripBom(line);
    char *e = line + strlen(line) - 1;
    while (e > line && (*e == '\n' || *e == '\r' || *e == ' ')) *e-- = 0;
    char *eq = strchr(line, '=');
    if (!eq) continue;
    *eq = 0;
    const char *key = line;
    const char *val = eq + 1;
    char *kend = eq - 1;
    while (kend > key && *kend == ' ') *kend-- = 0;
    while (*val == ' ') val++;

    if (strcmp(key, "gui_toggle_key") == 0)
      ParseHotkey(val, &g_guiToggleVK, &g_guiToggleCtrl, 'L', false);
    else if (strcmp(key, "screenshot_key") == 0)  g_screenshotVK = ParseVK(val, VK_F8);
    else if (strcmp(key, "freeze_key") == 0)
      ParseHotkey(val, &g_freezeVK, &g_freezeCtrl, 'P', false);
    else if (strncmp(key, "mmd_", 4) == 0) {
      for (int i=0;i<4;++i)
        if (strcmp(key,k_mmdHotkeyKeys[i])==0)
          ParseHotkey(val,&g_mmdHotkeyVK[i],&g_mmdHotkeyCtrl[i],k_mmdHotkeyDefaults[i],true);
    }
    else if (strcmp(key, "click_through") == 0)   g_clickThrough = (strtoul(val, nullptr, 0) != 0);
    else if (strcmp(key, "overlay_mode") == 0)    g_overlayMode = (int)strtoul(val, nullptr, 0);
    else if (strcmp(key, "overlay_fps") == 0) {
      int v = (int)strtoul(val, nullptr, 0);
      g_overlayFps = (v < 0) ? 0 : (v > 240 ? 240 : v);
    }
    else if (strcmp(key, "default_pose_dir") == 0) {
      if (val[0] == '\0') {
        ResolveDefaultPoseDir();
      } else if (val[1] == ':' ||
                 (val[0] == '\\' && val[1] == '\\')) {
        snprintf(g_defaultPoseDir, sizeof(g_defaultPoseDir), "%s", val);
      } else {
        // 相对路径按游戏根目录（poser.dll 的上一级）解析，不依赖工作目录
        HMODULE m = GetModuleHandleA("poser.dll");
        char base[MAX_PATH] = {};
        if (m && GetModuleFileNameA(m, base, MAX_PATH)) {
          char *s = strrchr(base, '\\');
          if (s)
            *s = 0; // ...\plugin
          s = strrchr(base, '\\');
          if (s)
            *s = 0; // game root
          snprintf(g_defaultPoseDir, sizeof(g_defaultPoseDir), "%s\\%s", base,
                   val);
        } else {
          snprintf(g_defaultPoseDir, sizeof(g_defaultPoseDir), "%s", val);
        }
      }
    }
  }
  fclose(f);
  CheckHotkeyConflicts();
  Log("[CFG] gui_toggle_key=%s%d (0x%X) freeze_key=%s%d (0x%X) "
      "overlay_mode=%d overlay_fps=%d",
      g_guiToggleCtrl ? "CTRL+" : "", g_guiToggleVK, g_guiToggleVK,
      g_freezeCtrl ? "CTRL+" : "", g_freezeVK, g_freezeVK, g_overlayMode,
      g_overlayFps);
  // 配置是老版本留下的值时，用户容易以为"默认键没生效"（旧版默认 F12/F11），
  // 这里把"实际生效的键"连同提示一起打出来
  {
    char hk1[32] = {}, hk2[32] = {};
    Log("[CFG] effective hotkeys: toggle=%s%s  freeze=%s%s "
        "(config file wins over built-in defaults; delete poser_config.txt to "
        "get the new defaults L/P)",
        g_guiToggleCtrl ? "CTRL+" : "", VkName(g_guiToggleVK, hk1, sizeof(hk1)),
        g_freezeCtrl ? "CTRL+" : "", VkName(g_freezeVK, hk2, sizeof(hk2)));
  }
  return true;
}
