#pragma once
#include <windows.h>
#include <cstdio>
#include <string>

// Resolve against the executable / DLL, never against the launcher or shortcut's
// working directory. Do not change the process-wide current directory.
static std::wstring ModuleDirectory(HMODULE module) {
  wchar_t path[32768] = {};
  DWORD n = GetModuleFileNameW(module, path, 32768);
  if (!n || n >= 32768) return {};
  std::wstring result(path, n);
  auto slash = result.find_last_of(L"\\/");
  return slash == std::wstring::npos ? std::wstring{} : result.substr(0, slash);
}
static std::wstring ProxyPluginDirectory() {
  auto root = ModuleDirectory(nullptr);
  return root.empty() ? std::wstring{} : root + L"\\plugin";
}
static std::wstring PoserFilePath(const wchar_t *name) {
  HMODULE module = GetModuleHandleW(L"poser.dll");
  auto dir = module ? ModuleDirectory(module) : ProxyPluginDirectory();
  return dir.empty() ? std::wstring{} : dir + L"\\" + name;
}
static FILE *OpenPoserFile(const wchar_t *name, const wchar_t *mode) {
  auto path = PoserFilePath(name);
  return path.empty() ? nullptr : _wfopen(path.c_str(), mode);
}
