#pragma once
#include "core/build_features.h"
#if !POSER_ENABLE_XXMI_BRIDGE
#error The XXMI bridge requires a separate opt-in build.
#endif
// EFMI mod bridge runtime (experimental). Reads the mod config EFMI loads,
// writes Mods\EndfieldPoserBridge\EndfieldPoserBridge.ini and answers
// GetAsyncKeyState for its virtual keys inside EFMI's d3d11.dll only, by
// patching that module's import table. The game never sees these keys.
// Everything except the hook runs under g_poseMutex.
#include "config.h"
#include "core/plugin_paths.h"
#include "game/mmd_io.h"
#include "math/mmd_motion.h"
#include "math/mod_bridge.h"
#include "nlohmann/json.hpp"
#include <chrono>
#include <filesystem>
#include <fstream>
#include <functional>
#include <future>
#include <tlhelp32.h>

static const wchar_t *k_modBridgeFolder = L"EndfieldPoserBridge";
struct ModBridgeTarget {
  std::string id, name, label;
  std::vector<float> values; // morph weight 0 selects the first, 1 the last
};
struct ModBridgeScan {
  std::wstring root;
  std::string modsDir = "Mods", error;
  mod_bridge::Catalog catalog;
  std::vector<std::string> warnings;
  int files = 0;
};
struct ModBridgeConflict {
  std::string file, section, key, hotkey, keys;
};
struct ModBridgeState {
  bool settingsLoaded = false, enabled = false;
  std::wstring root; // EFMI folder, which holds d3dx.ini
  std::string modsDir = "Mods";
  std::map<std::string, std::vector<ModBridgeTarget>> morphs; // VMD morph name -> targets
  std::vector<mod_bridge::Slot> slots;
  size_t missing = 0;
  // What each code does in any bridge file 3DMigoto may have loaded. A code
  // keeps its meaning until the loaded config is seen not to poll it.
  std::map<int, std::string> meanings;
  std::map<std::string, mod_bridge::Driver> drivers; // variable id -> driver
  bool syncPending = false;
  HMODULE module = nullptr;
  int probes = 0;
  ULONGLONG nextProbe = 0, nextSweep = 0;
  bool hooked = false, hookTried = false;
  void **importSlot = nullptr;
  std::string hookError, error;
  bool scanning = false, scanned = false;
  unsigned scanGeneration = 0;
  std::future<ModBridgeScan> scanner;
  ModBridgeScan scan;
};
// Process-lifetime owners, like g_mmd: the hook may run until the process ends.
static ModBridgeState &g_modBridge = *new ModBridgeState;
static mod_bridge::VirtualKeys &g_modKeys = *new mod_bridge::VirtualKeys;

// ---- Import table hook (3DMigoto's input thread) ----
using ModKeyStateFn = SHORT(WINAPI *)(int);
static std::atomic<ModKeyStateFn> s_modRealKeyState{nullptr};
static std::atomic<ULONGLONG> s_modLastInput{0};
static bool ModPoolCode(int vk) {
  static const std::array<bool, 256> pool = [] {
    std::array<bool, 256> codes{};
    for (int c : mod_bridge::CodePool()) codes[size_t(c)] = true;
    return codes;
  }();
  return vk > 0 && vk < 256 && pool[size_t(vk)];
}
static SHORT WINAPI ModBridgeKeyState(int vk) {
  ULONGLONG now = GetTickCount64();
  s_modLastInput.store(now, std::memory_order_relaxed);
  if (ModPoolCode(vk)) {
    bool down = g_modKeys.poll(vk, now); // polls also show which codes a config binds
    if (g_modKeys.owned(vk)) return down ? SHORT(-0x8000) : SHORT(0);
  }
  ModKeyStateFn real = s_modRealKeyState.load();
  return real ? real(vk) : SHORT(0);
}
// SEH leaf: no C++ objects (MSVC C2712). `target` matches the slot when the
// module has no import name table.
static void **ModFindImport(HMODULE module, const char *dll, const char *function, void *target) {
  __try {
    auto base = reinterpret_cast<BYTE *>(module);
    auto dos = reinterpret_cast<IMAGE_DOS_HEADER *>(base);
    if (dos->e_magic != IMAGE_DOS_SIGNATURE) return nullptr;
    auto nt = reinterpret_cast<IMAGE_NT_HEADERS *>(base + dos->e_lfanew);
    if (nt->Signature != IMAGE_NT_SIGNATURE) return nullptr;
    const auto &dir = nt->OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_IMPORT];
    if (!dir.VirtualAddress) return nullptr;
    for (auto imp = reinterpret_cast<IMAGE_IMPORT_DESCRIPTOR *>(base + dir.VirtualAddress); imp->Name; ++imp) {
      if (_stricmp(reinterpret_cast<char *>(base + imp->Name), dll)) continue;
      auto slot = reinterpret_cast<IMAGE_THUNK_DATA *>(base + imp->FirstThunk);
      auto name = imp->OriginalFirstThunk ? reinterpret_cast<IMAGE_THUNK_DATA *>(base + imp->OriginalFirstThunk) : nullptr;
      for (; slot->u1.Function; ++slot) {
        if (!name) {
          if (reinterpret_cast<void *>(slot->u1.Function) == target) return reinterpret_cast<void **>(&slot->u1.Function);
          continue;
        }
        if (!IMAGE_SNAP_BY_ORDINAL(name->u1.Ordinal) &&
            !strcmp(reinterpret_cast<char *>(reinterpret_cast<IMAGE_IMPORT_BY_NAME *>(base + name->u1.AddressOfData)->Name), function))
          return reinterpret_cast<void **>(&slot->u1.Function);
        ++name;
      }
    }
  } __except (EXCEPTION_EXECUTE_HANDLER) {
  }
  return nullptr;
}
static bool ModWriteImport(void **slot, void *expected, void *value) {
  __try {
    DWORD old = 0;
    if (!slot || !VirtualProtect(slot, sizeof(void *), PAGE_READWRITE, &old)) return false;
    const bool changed = InterlockedCompareExchangePointer(slot, value, expected) == expected;
    DWORD ignored = 0;
    VirtualProtect(slot, sizeof(void *), old, &ignored);
    return changed;
  } __except (EXCEPTION_EXECUTE_HANDLER) { return false; }
}
static bool ModBridgeInstallHook() {
  auto &b = g_modBridge;
  if (b.hooked) return true;
  HMODULE lifetime = nullptr;
  if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GET_MODULE_HANDLE_EX_FLAG_PIN,
                         reinterpret_cast<LPCWSTR>(&ModBridgeKeyState), &lifetime)) {
    b.hookError = u8"无法保持联动回调有效，已停用联动";
    return false;
  }
  HMODULE user32 = GetModuleHandleW(L"user32.dll");
  void *target = user32 ? reinterpret_cast<void *>(GetProcAddress(user32, "GetAsyncKeyState")) : nullptr;
  void **slot = b.module ? ModFindImport(b.module, "user32.dll", "GetAsyncKeyState", target) : nullptr;
  if (!slot) {
    b.hookError = u8"EFMI 的 d3d11.dll 没有导入 GetAsyncKeyState";
    Log("[MODS] hook failed: GetAsyncKeyState import not found");
    return false;
  }
  void *current = *slot;
  if (current != reinterpret_cast<void *>(&ModBridgeKeyState)) {
    s_modRealKeyState.store(reinterpret_cast<ModKeyStateFn>(current));
    if (!ModWriteImport(slot, current, reinterpret_cast<void *>(&ModBridgeKeyState))) {
      b.hookError = u8"无法修改 EFMI 的导入表";
      Log("[MODS] hook failed: VirtualProtect error %lu", GetLastError());
      return false;
    }
  }
  b.importSlot = slot;
  b.hooked = true;
  b.hookError.clear();
  Log("[MODS] EFMI key reads hooked (import slot %p)", slot);
  return true;
}
// Normal disable only. Do not touch another module's imports during teardown.
static void ModBridgeRemoveHook() {
  auto &b = g_modBridge;
  if (b.hooked && b.importSlot)
    ModWriteImport(b.importSlot, reinterpret_cast<void *>(&ModBridgeKeyState),
                   reinterpret_cast<void *>(s_modRealKeyState.load()));
  b.hooked = false;
  b.importSlot = nullptr;
}

// ---- Files ----
static std::string ModReadText(const std::filesystem::path &path) {
  std::ifstream f(path, std::ios::binary | std::ios::ate);
  if (!f) throw std::runtime_error("Cannot open " + path.u8string());
  auto size = f.tellg();
  if (size < 0 || size > 32 * 1024 * 1024) throw std::runtime_error("INI file too large: " + path.u8string());
  std::string text(size_t(size), '\0');
  f.seekg(0);
  if (size > 0 && !f.read(text.data(), size)) throw std::runtime_error("Cannot read " + path.u8string());
  return text;
}
static bool ModWriteIfChanged(const std::filesystem::path &path, const std::string &text) {
  try {
    if (ModReadText(path) == text) return true;
  } catch (...) {
  }
  std::error_code ec;
  std::filesystem::create_directories(path.parent_path(), ec);
  auto temp = path;
  temp += L".tmp";
  {
    std::ofstream f(temp, std::ios::binary | std::ios::trunc);
    f << text;
    f.flush();
    if (!f) return false;
  }
  return MoveFileExW(temp.c_str(), path.c_str(), MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH) != 0;
}
static std::filesystem::path ModBridgeSettingsPath() {
  auto dir = PoserFilePath(L"mmd");
  return dir.empty() ? std::filesystem::path() : std::filesystem::path(dir) / L"mod-bridge.json";
}
static std::filesystem::path ModBridgeFile() {
  const auto &b = g_modBridge;
  return std::filesystem::path(b.root) / std::filesystem::u8path(b.modsDir) / k_modBridgeFolder /
         (std::wstring(k_modBridgeFolder) + L".ini");
}
static void ModBridgeRemoveFile() {
  if (g_modBridge.root.empty()) return;
  std::error_code ec;
  auto path = ModBridgeFile();
  if (std::filesystem::remove(path, ec)) Log("[MODS] bridge file removed");
  std::filesystem::remove(path.parent_path(), ec); // only when empty
}
// Matches exclude_recursive: PCRE2 glob, case-insensitive; names only.
static bool ModGlob(const std::string &pattern, const std::string &name) {
  auto low = [](char c) { return c >= 'A' && c <= 'Z' ? char(c - 'A' + 'a') : c; };
  size_t p = 0, n = 0, star = std::string::npos, resume = 0;
  while (n < name.size()) {
    if (p < pattern.size() && pattern[p] == '*') {
      star = p++;
      resume = n;
    } else if (p < pattern.size() && (pattern[p] == '?' || low(pattern[p]) == low(name[n]))) {
      ++p, ++n;
    } else if (star != std::string::npos) {
      p = star + 1;
      n = ++resume;
    } else {
      return false;
    }
  }
  while (p < pattern.size() && pattern[p] == '*') ++p;
  return p == pattern.size();
}

// ---- Scan: follows d3dx.ini [Include] like LoadConfigFile ----
static ModBridgeScan ModBridgeScanFiles(std::wstring root) {
  namespace fs = std::filesystem;
  ModBridgeScan result;
  result.root = root;
  try {
    const fs::path base(root);
    mod_bridge::CatalogBuilder builder;
    const auto d3dx = mod_bridge::ParseIni(ModReadText(base / L"d3dx.ini"));
    builder.add(d3dx, "d3dx.ini", "", false);
    std::vector<std::string> exclude;
    struct Include {
      std::string dir; // folder of the including file, relative, with "\"
      mod_bridge::IniSection section;
      bool mod = false; // included by a file from a recursive folder
    };
    std::vector<Include> queue;
    auto enqueue = [&](const mod_bridge::IniDocument &doc, const std::string &dir, bool mod) {
      for (const auto &s : doc.sections)
        if (mod_bridge::HasPrefix(s.name, "include")) queue.push_back({dir, s, mod});
    };
    for (const auto &s : d3dx.sections)
      if (mod_bridge::Lower(s.name) == "include")
        for (const auto &line : s.lines)
          if (line.assignment && mod_bridge::Lower(line.key) == "exclude_recursive") exclude.push_back(line.value);
    enqueue(d3dx, "", false);
    auto warn = [&](const std::string &text) {
      if (result.warnings.size() < 20) result.warnings.push_back(text);
    };
    auto parse = [&](const std::string &rel, bool mod) {
      if (++result.files > 4096) throw std::runtime_error("Too many INI files");
      try {
        auto doc = mod_bridge::ParseIni(ModReadText(base / fs::u8path(rel)));
        builder.add(doc, rel, doc.ns.empty() ? rel : doc.ns, mod);
        size_t slash = rel.find_last_of('\\');
        enqueue(doc, slash == std::string::npos ? std::string() : rel.substr(0, slash + 1), mod);
      } catch (const std::exception &e) {
        warn(e.what());
      }
    };
    // ParseIniFilesRecursive: sorted names, files before folders; the namespace
    // of a file without "namespace =" is its path relative to the EFMI folder.
    const std::string ownFolder = mod_bridge::Lower(mmd::Utf8(k_modBridgeFolder));
    std::function<void(const std::string &, int)> walk = [&](const std::string &rel, int depth) {
      if (depth > 16) return;
      std::vector<std::string> files, dirs;
      std::error_code ec;
      for (fs::directory_iterator it(base / fs::u8path(rel), fs::directory_options::skip_permission_denied, ec), end;
           !ec && it != end; it.increment(ec)) {
        const std::string name = it->path().filename().u8string();
        bool excluded = std::any_of(exclude.begin(), exclude.end(), [&](const std::string &p) { return ModGlob(p, name); });
        if (excluded || mod_bridge::Lower(name) == ownFolder) continue;
        std::error_code type;
        if (it->is_directory(type)) dirs.push_back(name);
        else if (name.size() > 4 && name.compare(name.size() - 4, 4, ".ini") == 0) files.push_back(name);
      }
      auto order = [](const std::string &a, const std::string &b) { return mod_bridge::Lower(a) < mod_bridge::Lower(b); };
      std::sort(files.begin(), files.end(), order);
      std::sort(dirs.begin(), dirs.end(), order);
      for (const auto &f : files) parse(rel + "\\" + f, true);
      for (const auto &d : dirs) walk(rel + "\\" + d, depth + 1);
    };
    std::set<std::string> seen;
    bool modsDir = false;
    for (size_t i = 0; i < queue.size(); ++i) {
      const Include item = queue[i]; // parse() may grow the queue
      for (const auto &line : item.section.lines) {
        if (!line.assignment) continue;
        const std::string key = mod_bridge::Lower(line.key), rel = item.dir + line.value;
        if ((key != "include" && key != "include_recursive") || !seen.insert(mod_bridge::Lower(rel)).second) continue;
        if (key == "include") {
          parse(rel, item.mod);
          continue;
        }
        if (!modsDir) result.modsDir = rel, modsDir = true;
        walk(rel, 0);
      }
    }
    result.catalog = builder.finish();
  } catch (const std::exception &e) {
    result.error = e.what();
  }
  return result;
}

// ---- Settings ----
// Written only once the bridge was used: scanning alone leaves no file.
static void ModBridgeSaveSettings() {
  const auto &b = g_modBridge;
  try {
    const auto path = ModBridgeSettingsPath();
    bool used = b.enabled || std::any_of(b.morphs.begin(), b.morphs.end(), [](const auto &kv) { return !kv.second.empty(); });
    std::error_code ec;
    if (path.empty() || (!used && !std::filesystem::exists(path, ec))) return;
    nlohmann::json j = {{"version", 1}, {"enabled", b.enabled}, {"efmi", mmd::Utf8(b.root)}, {"modsDir", b.modsDir}};
    nlohmann::json morphs = nlohmann::json::object(), slots = nlohmann::json::array();
    for (const auto &kv : b.morphs) {
      if (kv.second.empty()) continue;
      auto &list = morphs[kv.first] = nlohmann::json::array();
      for (const auto &t : kv.second) list.push_back({{"id", t.id}, {"name", t.name}, {"label", t.label}, {"values", t.values}});
    }
    for (const auto &s : b.slots)
      slots.push_back({{"id", s.id}, {"name", s.name}, {"value", s.value}, {"guard", s.guard}, {"set", s.set}});
    j["morphs"] = morphs;
    j["slots"] = slots;
    if (!ModWriteIfChanged(path, j.dump(2))) g_modBridge.error = "Could not write mod-bridge.json";
  } catch (const std::exception &e) {
    g_modBridge.error = e.what();
  }
}
static void ModBridgeLoadSettings() {
  auto &b = g_modBridge;
  if (b.settingsLoaded) return;
  b.settingsLoaded = true;
  try {
    const auto path = ModBridgeSettingsPath();
    if (path.empty()) return;
    std::ifstream f(path, std::ios::binary);
    if (!f) return;
    nlohmann::json j;
    f >> j;
    b.enabled = j.value("enabled", false);
    b.root = mmd::Wide(j.value("efmi", std::string()));
    b.modsDir = j.value("modsDir", std::string("Mods"));
    const auto morphs = j.value("morphs", nlohmann::json::object());
    for (const auto &kv : morphs.items())
      for (const auto &t : kv.value()) {
        ModBridgeTarget target{t.value("id", std::string()), t.value("name", std::string()),
                               t.value("label", std::string()), t.value("values", std::vector<float>())};
        if (!target.id.empty() && !target.name.empty() && !target.values.empty()) b.morphs[kv.key()].push_back(target);
      }
    const auto slots = j.value("slots", nlohmann::json::array());
    for (const auto &s : slots) {
      mod_bridge::Slot slot{s.value("id", std::string()), s.value("name", std::string()), s.value("value", 0.f),
                            s.value("guard", 0), s.value("set", 0)};
      if (!slot.id.empty() && ModPoolCode(slot.guard) && ModPoolCode(slot.set)) b.slots.push_back(slot);
    }
  } catch (const std::exception &e) {
    b.error = e.what();
    Log("[MODS] settings load failed: %s", e.what());
  }
}

// ---- Bridge state ----
static bool ModBridgeLive(int code, ULONGLONG now) { return g_modKeys.lastPoll(code) + 2000 >= now; }
static bool ModBridgeActive() {
  for (const auto &kv : g_modBridge.drivers)
    if (kv.second.guard) return true;
  return false;
}
// Plans codes for every bound value, writes the bridge and the settings.
// Never while a session holds keys: codes must not change under it.
static void ModBridgeSync() {
  auto &b = g_modBridge;
  if (ModBridgeActive()) {
    b.syncPending = true;
    return;
  }
  std::vector<mod_bridge::Pair> pairs;
  std::set<std::pair<std::string, float>> seen;
  for (const auto &kv : b.morphs)
    for (const auto &t : kv.second) {
      if (b.scanned && !b.scan.catalog.find(t.id)) continue; // mod removed: avoid "Undeclared variable"
      for (float v : t.values)
        if (seen.insert({t.id, v}).second) pairs.push_back({t.id, t.name, v});
    }
  auto usable = [&](int code, const std::string &meaning) {
    if (b.scanned && b.scan.catalog.keys.count(code)) return false;
    auto it = b.meanings.find(code);
    return it == b.meanings.end() || it->second == meaning;
  };
  b.slots = mod_bridge::Plan(b.slots, pairs, usable, &b.missing);
  std::array<bool, 256> current{};
  for (const auto &s : b.slots) {
    b.meanings[s.guard] = mod_bridge::Meaning(true, s.id, s.value);
    b.meanings[s.set] = mod_bridge::Meaning(false, s.id, s.value);
    current[size_t(s.guard)] = current[size_t(s.set)] = true;
  }
  for (int c : mod_bridge::CodePool()) g_modKeys.own(c, b.enabled && current[size_t(c)]);
  if (!b.root.empty()) {
    if (!b.enabled) ModBridgeRemoveFile();
    else if (!ModWriteIfChanged(ModBridgeFile(), mod_bridge::BridgeIni(b.slots))) b.error = u8"无法写入桥接文件";
  }
  ModBridgeSaveSettings();
}
// The bridge file as it is now is what 3DMigoto loads (or has loaded) from
// this EFMI folder. Read it before anything rewrites it.
static void ModBridgeReadLoadedFile() {
  auto &b = g_modBridge;
  b.meanings.clear();
  if (b.root.empty()) return;
  try {
    for (const auto &kv : mod_bridge::BridgeMeanings(mod_bridge::ParseIni(ModReadText(ModBridgeFile()))))
      b.meanings[kv.first] = kv.second;
  } catch (...) {
  }
}
static void ModBridgeStartup() {
  std::lock_guard<std::recursive_mutex> lock(g_poseMutex);
  ModBridgeLoadSettings();
  ModBridgeReadLoadedFile();
  if (!g_modBridge.root.empty()) ModBridgeSync();
}
static bool ModBridgeProbe() {
  auto &b = g_modBridge;
  wchar_t system[MAX_PATH] = {};
  size_t systemLength = GetSystemDirectoryW(system, MAX_PATH);
  HANDLE snap = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE, GetCurrentProcessId());
  if (snap == INVALID_HANDLE_VALUE) return false;
  MODULEENTRY32W me = {};
  me.dwSize = sizeof(me);
  bool found = false;
  for (BOOL ok = Module32FirstW(snap, &me); ok && !found; ok = Module32NextW(snap, &me)) {
    if (_wcsicmp(me.szModule, L"d3d11.dll") || !_wcsnicmp(me.szExePath, system, systemLength)) continue;
    std::error_code ec;
    auto dir = std::filesystem::path(me.szExePath).parent_path();
    if (!std::filesystem::exists(dir / L"d3dx.ini", ec)) continue;
    if (b.root != dir.wstring()) { // another EFMI install than last time
      b.root = dir.wstring();
      b.modsDir = "Mods";
      ModBridgeReadLoadedFile();
    }
    b.module = me.hModule;
    found = true;
  }
  CloseHandle(snap);
  if (found) Log("[MODS] EFMI found: %s", mmd::Utf8(b.root).c_str());
  return found;
}
static void ModBridgeStartScan() {
  auto &b = g_modBridge;
  if (b.scanning || b.root.empty()) return;
  b.scanning = true;
  b.scanner = std::async(std::launch::async, ModBridgeScanFiles, b.root);
}
static std::vector<ModBridgeConflict> ModBridgeConflicts() {
  static std::vector<ModBridgeConflict> cached;
  static std::string cacheKey;
  const auto &b = g_modBridge;
  if (!b.scanned) return {};
  const struct { const char *label; int vk; bool ctrl; } hotkeys[] = {
      {u8"呼出 / 隐藏面板", g_guiToggleVK, g_guiToggleCtrl}, {u8"冻结 / 解冻", g_freezeVK, g_freezeCtrl},
      {u8"MMD 播放", g_mmdHotkeyVK[0], g_mmdHotkeyCtrl[0]}, {u8"MMD 暂停", g_mmdHotkeyVK[1], g_mmdHotkeyCtrl[1]},
      {u8"MMD 停止并恢复", g_mmdHotkeyVK[2], g_mmdHotkeyCtrl[2]}, {u8"MMD 重置到首帧", g_mmdHotkeyVK[3], g_mmdHotkeyCtrl[3]}};
  std::string key = std::to_string(b.scanGeneration);
  for (const auto &h : hotkeys) key += "," + std::to_string(h.vk) + (h.ctrl ? "c" : "");
  if (key == cacheKey) return cached;
  cacheKey = key;
  cached.clear();
  for (const auto &binding : b.scan.catalog.bindings)
    for (const auto &h : hotkeys)
      if (mod_bridge::Collides(binding.chord, {h.vk, h.ctrl})) {
        char keys[48] = {};
        HotkeyDisplay(h.vk, h.ctrl, keys, sizeof(keys));
        cached.push_back({binding.file, binding.section, binding.key, h.label, keys});
      }
  return cached;
}
static void ModBridgeAfterScan() {
  auto &b = g_modBridge;
  if (!b.scan.error.empty()) {
    Log("[MODS] scan failed: %s", b.scan.error.c_str());
    return;
  }
  b.modsDir = b.scan.modsDir;
  Log("[MODS] %d INI files, %zu switchable variables, %zu key bindings", b.scan.files,
      b.scan.catalog.variables.size(), b.scan.catalog.bindings.size());
  for (const auto &w : b.scan.warnings) Log("[MODS] %s", w.c_str());
  for (const auto &c : ModBridgeConflicts())
    Log("[MODS] hotkey conflict: %s [%s] key = %s with poser %s (%s)", c.file.c_str(), c.section.c_str(),
        c.key.c_str(), c.hotkey.c_str(), c.keys.c_str());
  ModBridgeSync();
}
// Codes outside the plan: nothing may hold them, and once the loaded config
// stops polling them while it polls the bridge, they may take a new meaning.
static void ModBridgeSweep(ULONGLONG now) {
  auto &b = g_modBridge;
  std::array<bool, 256> current{};
  bool polled = false;
  for (const auto &s : b.slots) {
    current[size_t(s.guard)] = current[size_t(s.set)] = true;
    polled |= ModBridgeLive(s.guard, now);
  }
  for (int c : mod_bridge::CodePool()) {
    if (current[size_t(c)]) continue;
    g_modKeys.reset(c);
    if (polled && g_modKeys.lastPoll(c) + 3000 < now) b.meanings.erase(c);
  }
}
static void ModBridgeSetEnabled(bool on) {
  auto &b = g_modBridge;
  if (on == b.enabled || ModBridgeActive()) return;
  b.enabled = on;
  b.hookTried = false;
  if (!on) ModBridgeRemoveHook();
  Log("[MODS] bridge %s", on ? "enabled" : "disabled");
  ModBridgeSync();
}
// Game thread, every frame (MmdTick).
static void ModBridgeService() {
  auto &b = g_modBridge;
  ModBridgeLoadSettings();
  const ULONGLONG now = GetTickCount64();
  if (!b.module && b.probes < 60 && now >= b.nextProbe) {
    ++b.probes;
    b.nextProbe = now + 2000;
    if (ModBridgeProbe()) ModBridgeStartScan();
  }
  if (b.scanning && b.scanner.wait_for(std::chrono::seconds(0)) == std::future_status::ready) {
    b.scan = b.scanner.get();
    b.scanning = false;
    b.scanned = b.scan.error.empty();
    ++b.scanGeneration;
    ModBridgeAfterScan();
  }
  if (b.enabled && b.module && !b.hooked && !b.hookTried) {
    b.hookTried = true;
    ModBridgeInstallHook();
  }
  if (b.hooked && now >= b.nextSweep) {
    b.nextSweep = now + 1000;
    ModBridgeSweep(now);
  }
  if (b.syncPending && !ModBridgeActive()) {
    b.syncPending = false;
    ModBridgeSync();
  }
}
// Called from MmdApplyFrame: drives every bound variable to the value its
// morph track selects at `frame`. Codes 3DMigoto does not poll yet (bridge
// not reloaded, game in the background) are skipped.
static void ModBridgeFrame(const mmd::MotionClip &clip, double frame) {
  auto &b = g_modBridge;
  if (!b.enabled || !b.hooked || b.morphs.empty()) return;
  const ULONGLONG now = GetTickCount64();
  std::map<std::string, float> desired; // first morph (by name) wins a variable
  for (const auto &kv : b.morphs) {
    auto track = clip.morphs.find(kv.first);
    if (track == clip.morphs.end() || track->second.empty()) continue;
    float weight = mmd::SampleMorph(track->second, frame);
    for (const auto &t : kv.second) desired.emplace(t.id, mod_bridge::Select(t.values, weight));
  }
  for (const auto &kv : desired) {
    auto find = [&](float value) -> const mod_bridge::Slot * {
      for (const auto &s : b.slots)
        if (s.id == kv.first && s.value == value)
          return ModBridgeLive(s.guard, now) && ModBridgeLive(s.set, now) ? &s : nullptr;
      return nullptr;
    };
    mod_bridge::Drive(g_modKeys, b.drivers[kv.first], kv.second, find);
  }
}
// Called from MmdStop: 3DMigoto restores every variable to its value from
// before playback once it polls the released guards.
static void ModBridgeRelease() {
  auto &b = g_modBridge;
  for (auto &kv : b.drivers) mod_bridge::Release(g_modKeys, kv.second, b.slots, kv.first);
}
static bool ModBridgeUsesMorph(const std::string &morph) {
  const auto &b = g_modBridge;
  auto it = b.morphs.find(morph);
  return b.enabled && it != b.morphs.end() && !it->second.empty();
}
static std::string ModBridgeVariableLabel(const mod_bridge::Variable &v) {
  std::string file = v.file;
  const std::string prefix = g_modBridge.modsDir + "\\";
  if (mod_bridge::HasPrefix(file, mod_bridge::Lower(prefix).c_str())) file.erase(0, prefix.size());
  return file + u8" · " + v.local + (v.label.empty() ? "" : u8" · " + v.label);
}
