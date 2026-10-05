#pragma once

#include <windows.h>
#include <mutex>
// Serialize GUI and HTTP pose access; recursive for shared control entrypoints.
static std::recursive_mutex g_poseMutex;
#include <cstdio>
#include <cstdarg>
#include <cstring>
#include "plugin_paths.h"

#include "core/version.h" // 版本号唯一来源（资源文件 src/poser.rc 也读它）

#define POSER_STRINGIFY2(x) #x
#define POSER_STRINGIFY(x) POSER_STRINGIFY2(x)
#define POSER_VERSION POSER_STRINGIFY(POSER_VERSION_MAJOR) "." POSER_STRINGIFY(POSER_VERSION_MINOR) "." POSER_STRINGIFY(POSER_VERSION_PATCH)

static HANDLE g_logHandle = INVALID_HANDLE_VALUE;
static CRITICAL_SECTION g_logLock;

static void OpenLog(const wchar_t *path) {
  InitializeCriticalSection(&g_logLock);
  g_logHandle = CreateFileW(path, GENERIC_WRITE, FILE_SHARE_READ, nullptr,
                            OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
  if (g_logHandle != INVALID_HANDLE_VALUE) {
    SetFilePointer(g_logHandle, 0, nullptr, FILE_END);
  }
}

void Log(const char *fmt, ...) {
  if (g_logHandle == INVALID_HANDLE_VALUE)
    return;
  EnterCriticalSection(&g_logLock);
  char buf[4096];
  va_list args;
  va_start(args, fmt);
  int len = vsnprintf(buf, sizeof(buf) - 2, fmt, args);
  va_end(args);
  if (len < 0)
    len = 0;
  buf[len] = '\n';
  len++;
  DWORD written;
  WriteFile(g_logHandle, buf, len, &written, NULL);
  LeaveCriticalSection(&g_logLock);
}

// ---- IL2CPP 布局常量（对象内常用偏移，需与版本匹配，探测失败时回落）----
#define IL2CPP_STR_LEN      0x10
#define IL2CPP_STR_CHARS    0x14
#define IL2CPP_ARRAY_LEN    0x18
#define IL2CPP_ARRAY_DATA   0x20
#define IL2CPP_LIST_ITEMS   0x10
#define IL2CPP_LIST_SIZE    0x18
#define IL2CPP_BOXED_DATA   16

// 字段偏移动态解析失败时回落的默认值；解析成功则用解析值
static int SafeOff(int resolved, int fallback, const char *name) {
  if (resolved >= 0)
    return resolved;
  static unsigned s_warnedMask = 0;
  unsigned h = 0;
  for (const char *p = name; *p; p++)
    h = h * 31 + (unsigned)*p;
  unsigned bit = 1u << (h & 31);
  if (!(s_warnedMask & bit)) {
    s_warnedMask |= bit;
    Log("[WARN] Using fallback offset 0x%X for %s (dynamic resolution failed)",
        fallback, name);
  }
  return fallback;
}
