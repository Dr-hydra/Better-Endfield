#pragma once

#include <windows.h>
#include <cstdio>
#include <cstring>
#include <cstdlib>
#include <string>
#include <vector>

extern int g_guiToggleVK;
extern bool g_pluginActive;

struct EIEM_VKEntry {
  int vk;
  const char* name;
};

static const EIEM_VKEntry s_eiemVkTable[] = {
  { VK_F1, "F1" }, { VK_F2, "F2" }, { VK_F3, "F3" }, { VK_F4, "F4" },
  { VK_F5, "F5" }, { VK_F6, "F6" }, { VK_F7, "F7" }, { VK_F8, "F8" },
  { VK_F9, "F9" }, { VK_F10, "F10" }, { VK_F11, "F11" }, { VK_F12, "F12" },

  { VK_INSERT, "INSERT" }, { VK_DELETE, "DELETE" }, { VK_HOME, "HOME" },
  { VK_END, "END" }, { VK_PRIOR, "PAGEUP" }, { VK_NEXT, "PAGEDOWN" },

  { VK_LEFT, "LEFT" }, { VK_RIGHT, "RIGHT" }, { VK_UP, "UP" }, { VK_DOWN, "DOWN" },

  { VK_SPACE, "SPACE" }, { VK_RETURN, "ENTER" }, { VK_ESCAPE, "ESC" },
  { VK_TAB, "TAB" }, { VK_BACK, "BACKSPACE" }, { VK_CAPITAL, "CAPSLOCK" },
  { VK_OEM_3, "TILDE" },

  { VK_SHIFT, "SHIFT" }, { VK_CONTROL, "CTRL" }, { VK_MENU, "ALT" },

  { VK_NUMPAD0, "NUMPAD0" }, { VK_NUMPAD1, "NUMPAD1" },
  { VK_NUMPAD2, "NUMPAD2" }, { VK_NUMPAD3, "NUMPAD3" },
  { VK_NUMPAD4, "NUMPAD4" }, { VK_NUMPAD5, "NUMPAD5" },
  { VK_NUMPAD6, "NUMPAD6" }, { VK_NUMPAD7, "NUMPAD7" },
  { VK_NUMPAD8, "NUMPAD8" }, { VK_NUMPAD9, "NUMPAD9" },
  { VK_MULTIPLY, "NUMPAD*" }, { VK_ADD, "NUMPAD+" },
  { VK_SUBTRACT, "NUMPAD-" }, { VK_DECIMAL, "NUMPAD." },
  { VK_DIVIDE, "NUMPAD/" },

  { 0, nullptr }
};

static inline const char* EiemVKToString(int vk) {
  for (int i = 0; s_eiemVkTable[i].name; i++) {
    if (s_eiemVkTable[i].vk == vk)
      return s_eiemVkTable[i].name;
  }
  static char buf[16];
  if ((vk >= '0' && vk <= '9') || (vk >= 'A' && vk <= 'Z')) {
    buf[0] = (char)vk;
    buf[1] = '\0';
    return buf;
  }
  snprintf(buf, sizeof(buf), "VK_%02X", vk);
  return buf;
}

static inline int EiemStringToVK(const char* name) {
  if (!name || !name[0]) return 0;

  for (int i = 0; s_eiemVkTable[i].name; i++) {
    if (_stricmp(s_eiemVkTable[i].name, name) == 0)
      return s_eiemVkTable[i].vk;
  }
  if (name[1] == '\0') {
    char c = name[0];
    if (c >= '0' && c <= '9') return c;
    if (c >= 'a' && c <= 'z') return c - 'a' + 'A';
    if (c >= 'A' && c <= 'Z') return c;
  }
  if (_strnicmp(name, "VK_", 3) == 0) {
    return (int)strtol(name + 3, nullptr, 16);
  }
  if (_strnicmp(name, "0x", 2) == 0) {
    return (int)strtol(name + 2, nullptr, 16);
  }
  int dec = atoi(name);
  if (dec > 0) return dec;

  return 0;
}

static inline const char* GetEiemConfigPath() {
  if (GetFileAttributesA("plugin\\eiem_config.txt") != INVALID_FILE_ATTRIBUTES)
    return "plugin\\eiem_config.txt";
  if (GetFileAttributesA("eiem_config.txt") != INVALID_FILE_ATTRIBUTES)
    return "eiem_config.txt";
  return "plugin\\eiem_config.txt";
}

static inline void LoadEiemConfig() {
  const char* path = GetEiemConfigPath();
  FILE* f = fopen(path, "r");
  if (!f) {
    CreateDirectoryA("plugin", NULL);
    f = fopen("plugin\\eiem_config.txt", "w");
    if (f) {
      fprintf(f, "# ============================================================================\n");
      fprintf(f, "# EIEM Plugin Configuration (Endfield Ingame Expression & Motion)\n");
      fprintf(f, "# Managed by Applepie Manager or edited directly.\n");
      fprintf(f, "# ============================================================================\n\n");
      fprintf(f, "# GUI 呼出/隐藏快捷键 (默认: INSERT, 支持 F1-F12, INSERT, DELETE, HOME, END, A-Z, 0-9 等)\n");
      fprintf(f, "gui_toggle_key=INSERT\n");
      fclose(f);
    }
    return;
  }

  char line[512];
  while (fgets(line, sizeof(line), f)) {
    size_t len = strlen(line);
    while (len > 0 && (line[len - 1] == '\r' || line[len - 1] == '\n' || line[len - 1] == ' ' || line[len - 1] == '\t')) {
      line[--len] = '\0';
    }
    if (len == 0 || line[0] == '#' || line[0] == ';') continue;

    char* eq = strchr(line, '=');
    if (!eq) continue;
    *eq = '\0';
    char* key = line;
    char* val = eq + 1;

    while (*key == ' ' || *key == '\t') key++;
    while (*val == ' ' || *val == '\t') val++;

    if (_stricmp(key, "gui_toggle_key") == 0) {
      int vk = EiemStringToVK(val);
      if (vk > 0) {
        g_guiToggleVK = vk;
      }
    }
  }
  fclose(f);
}
