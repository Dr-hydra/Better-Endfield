#pragma once
// Shared loader for the EIEM-derived DX / Vulkan forwarding proxies (AGPL-3.0).
#include "plugin_paths.h"

static bool IsPluginDisabled(const std::wstring &directory, const wchar_t *name) {
  FILE *f = _wfopen((directory + L"\\applepie_manager_config.txt").c_str(), L"rt, ccs=UTF-8");
  if (!f) return false;
  bool inPlugins = false, disabled = false;
  wchar_t line[512];
  while (fgetws(line, 512, f)) {
    size_t n = wcslen(line);
    while (n && (line[n-1] == L'\n' || line[n-1] == L'\r' || line[n-1] == L' ')) line[--n] = 0;
    if (line[0] == L'[') { inPlugins = _wcsicmp(line, L"[plugins]") == 0; continue; }
    if (!inPlugins) continue;
    wchar_t *eq = wcschr(line, L'=');
    if (!eq) continue;
    *eq = 0;
    wchar_t *end = eq;
    while (end > line && end[-1] == L' ') *--end = 0;
    const wchar_t *value = eq + 1;
    while (*value == L' ') ++value;
    if (_wcsicmp(line, name) == 0 && wcscmp(value, L"0") == 0) { disabled = true; break; }
  }
  fclose(f);
  return disabled;
}

static void LoadPluginDirectory(const std::wstring &directory, const char *source) {
  if (directory.empty()) return;
  FILE *log = _wfopen((directory + L"\\poser_log.txt").c_str(), L"ab");
  if (log) {
    fprintf(log, "[PROXY] plugins loaded via %s (absolute executable directory)\n", source);
    fclose(log);
  }
  WIN32_FIND_DATAW data;
  HANDLE find = FindFirstFileW((directory + L"\\*.dll").c_str(), &data);
  if (find != INVALID_HANDLE_VALUE) {
    do {
      if ((data.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) ||
          _wcsicmp(data.cFileName, L"applepie_manager.dll") == 0 ||
          IsPluginDisabled(directory, data.cFileName)) continue;
      LoadLibraryW((directory + L"\\" + data.cFileName).c_str());
    } while (FindNextFileW(find, &data));
    FindClose(find);
  }
  LoadLibraryW((directory + L"\\applepie_manager.dll").c_str());
}

static DWORD WINAPI LoadPlugin(LPVOID source) {
  try { LoadPluginDirectory(ProxyPluginDirectory(), static_cast<const char *>(source)); }
  catch (...) { OutputDebugStringW(L"Endfield Poser: plugin loader failed\n"); }
  return 0;
}
static void StartPluginLoader(HMODULE module, const char *source) {
  DisableThreadLibraryCalls(module);
  HANDLE thread = CreateThread(nullptr, 0, LoadPlugin, const_cast<char *>(source), 0, nullptr);
  if (thread) CloseHandle(thread);
}
