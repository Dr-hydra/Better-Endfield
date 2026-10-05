#pragma once
// EFMI (XXMI 3DMigoto) mod bridge: parsing and the virtual-key protocol.
// Mirrors SpectrumQT/XXMI-Libs-Package where behaviour matters: key names
// (vkeys.h ParseVKey), key chords (input.cpp), namespaced variables and
// includes (IniHandler.cpp), and hold/activate value saving (Override.cpp).
// Portable: no Windows headers, so the protocol can be tested offline.
#include <algorithm>
#include <array>
#include <atomic>
#include <cmath>
#include <cstdint>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <map>
#include <set>
#include <string>
#include <vector>

namespace mod_bridge {
inline std::string Trim(const std::string &s) {
  size_t a = s.find_first_not_of(" \t\r\n"), b = s.find_last_not_of(" \t\r\n");
  return a == std::string::npos ? std::string() : s.substr(a, b - a + 1);
}
// ASCII only. 3DMigoto lowercases variable names itself, so bridge files keep
// the original spelling and let it apply the same conversion to both sides.
inline std::string Lower(std::string s) {
  for (char &c : s)
    if (c >= 'A' && c <= 'Z') c = char(c - 'A' + 'a');
  return s;
}
inline bool HasPrefix(const std::string &s, const char *lowerPrefix) {
  size_t n = std::strlen(lowerPrefix);
  return s.size() >= n && Lower(s.substr(0, n)) == lowerPrefix;
}
inline std::string FormatValue(float v) {
  char b[32];
  std::snprintf(b, sizeof(b), "%.9g", v);
  return b;
}

// ---- Key names and chords --------------------------------------------------
struct KeyName { const char *name; int vk; };
// XXMI VKMappings, in lookup order (first match wins, e.g. "-" is the keypad).
inline const KeyName *KeyNames(size_t *count) {
  static const KeyName names[] = {
    {"LBUTTON", 0x01}, {"RBUTTON", 0x02}, {"CANCEL", 0x03}, {"MBUTTON", 0x04}, {"XBUTTON1", 0x05},
    {"XBUTTON2", 0x06}, {"BACK", 0x08}, {"BACKSPACE", 0x08}, {"BACK_SPACE", 0x08}, {"TAB", 0x09},
    {"CLEAR", 0x0C}, {"RETURN", 0x0D}, {"ENTER", 0x0D}, {"SHIFT", 0x10}, {"CONTROL", 0x11},
    {"CTRL", 0x11}, {"MENU", 0x12}, {"ALT", 0x12}, {"PAUSE", 0x13}, {"CAPITAL", 0x14},
    {"CAPS", 0x14}, {"CAPSLOCK", 0x14}, {"CAPS_LOCK", 0x14}, {"KANA", 0x15}, {"HANGUEL", 0x15},
    {"HANGUL", 0x15}, {"JUNJA", 0x17}, {"FINAL", 0x18}, {"HANJA", 0x19}, {"KANJI", 0x19},
    {"ESCAPE", 0x1B}, {"CONVERT", 0x1C}, {"NONCONVERT", 0x1D}, {"ACCEPT", 0x1E},
    {"MODECHANGE", 0x1F}, {"SPACE", 0x20}, {"PRIOR", 0x21}, {"PGUP", 0x21}, {"PAGEUP", 0x21},
    {"PAGE_UP", 0x21}, {"NEXT", 0x22}, {"PGDN", 0x22}, {"PAGEDOWN", 0x22}, {"PAGE_DOWN", 0x22},
    {"END", 0x23}, {"HOME", 0x24}, {"LEFT", 0x25}, {"UP", 0x26}, {"RIGHT", 0x27}, {"DOWN", 0x28},
    {"SELECT", 0x29}, {"PRINT", 0x2A}, {"EXECUTE", 0x2B}, {"SNAPSHOT", 0x2C}, {"PRSCR", 0x2C},
    {"PRINTSCREEN", 0x2C}, {"PRINT_SCREEN", 0x2C}, {"INSERT", 0x2D}, {"DELETE", 0x2E},
    {"HELP", 0x2F}, {"LWIN", 0x5B}, {"LEFT_WIN", 0x5B}, {"LEFT_WINDOWS", 0x5B}, {"RWIN", 0x5C},
    {"RIGHT_WIN", 0x5C}, {"RIGHT_WINDOWS", 0x5C}, {"APPS", 0x5D}, {"SLEEP", 0x5F},
    {"NUMPAD0", 0x60}, {"NUMPAD1", 0x61}, {"NUMPAD2", 0x62}, {"NUMPAD3", 0x63}, {"NUMPAD4", 0x64},
    {"NUMPAD5", 0x65}, {"NUMPAD6", 0x66}, {"NUMPAD7", 0x67}, {"NUMPAD8", 0x68}, {"NUMPAD9", 0x69},
    {"MULTIPLY", 0x6A}, {"ADD", 0x6B}, {"SEPARATOR", 0x6C}, {"SUBTRACT", 0x6D}, {"DECIMAL", 0x6E},
    {"DIVIDE", 0x6F}, {"F1", 0x70}, {"F2", 0x71}, {"F3", 0x72}, {"F4", 0x73}, {"F5", 0x74},
    {"F6", 0x75}, {"F7", 0x76}, {"F8", 0x77}, {"F9", 0x78}, {"F10", 0x79}, {"F11", 0x7A},
    {"F12", 0x7B}, {"F13", 0x7C}, {"F14", 0x7D}, {"F15", 0x7E}, {"F16", 0x7F}, {"F17", 0x80},
    {"F18", 0x81}, {"F19", 0x82}, {"F20", 0x83}, {"F21", 0x84}, {"F22", 0x85}, {"F23", 0x86},
    {"F24", 0x87}, {"NUMLOCK", 0x90}, {"SCROLL", 0x91}, {"LSHIFT", 0xA0}, {"LEFT_SHIFT", 0xA0},
    {"RSHIFT", 0xA1}, {"RIGHT_SHIFT", 0xA1}, {"LCONTROL", 0xA2}, {"LEFT_CONTROL", 0xA2},
    {"LCTRL", 0xA2}, {"LEFT_CTRL", 0xA2}, {"RCONTROL", 0xA3}, {"RIGHT_CONTROL", 0xA3},
    {"RCTRL", 0xA3}, {"RIGHT_CTRL", 0xA3}, {"LMENU", 0xA4}, {"LEFT_MENU", 0xA4}, {"LALT", 0xA4},
    {"LEFT_ALT", 0xA4}, {"RMENU", 0xA5}, {"RIGHT_MENU", 0xA5}, {"RALT", 0xA5}, {"RIGHT_ALT", 0xA5},
    {"BROWSER_BACK", 0xA6}, {"BROWSER_FORWARD", 0xA7}, {"BROWSER_REFRESH", 0xA8},
    {"BROWSER_STOP", 0xA9}, {"BROWSER_SEARCH", 0xAA}, {"BROWSER_FAVORITES", 0xAB},
    {"BROWSER_HOME", 0xAC}, {"VOLUME_MUTE", 0xAD}, {"VOLUME_DOWN", 0xAE}, {"VOLUME_UP", 0xAF},
    {"MEDIA_NEXT_TRACK", 0xB0}, {"MEDIA_PREV_TRACK", 0xB1}, {"MEDIA_STOP", 0xB2},
    {"MEDIA_PLAY_PAUSE", 0xB3}, {"LAUNCH_MAIL", 0xB4}, {"LAUNCH_MEDIA_SELECT", 0xB5},
    {"LAUNCH_APP1", 0xB6}, {"LAUNCH_APP2", 0xB7}, {"OEM_1", 0xBA}, {";", 0xBA}, {":", 0xBA},
    {"COLON", 0xBA}, {"SEMICOLON", 0xBA}, {"SEMI_COLON", 0xBA}, {"OEM_PLUS", 0xBB}, {"=", 0xBB},
    {"PLUS", 0xBB}, {"EQUALS", 0xBB}, {"OEM_COMMA", 0xBC}, {",", 0xBC}, {"<", 0xBC},
    {"COMMA", 0xBC}, {"OEM_MINUS", 0xBD}, {"MINUS", 0xBD}, {"UNDERSCORE", 0xBD}, {"_", 0xBD},
    {"OEM_PERIOD", 0xBE}, {".", 0xBE}, {">", 0xBE}, {"PERIOD", 0xBE}, {"OEM_2", 0xBF}, {"/", 0xBF},
    {"?", 0xBF}, {"SLASH", 0xBF}, {"FORWARD_SLASH", 0xBF}, {"QUESTION", 0xBF},
    {"QUESTION_MARK", 0xBF}, {"OEM_3", 0xC0}, {"`", 0xC0}, {"~", 0xC0}, {"TILDE", 0xC0},
    {"GRAVE", 0xC0}, {"OEM_4", 0xDB}, {"[", 0xDB}, {"{", 0xDB}, {"OEM_5", 0xDC}, {"\\", 0xDC},
    {"|", 0xDC}, {"BACKSLASH", 0xDC}, {"BACK_SLASH", 0xDC}, {"PIPE", 0xDC}, {"VERTICAL_BAR", 0xDC},
    {"OEM_6", 0xDD}, {"]", 0xDD}, {"}", 0xDD}, {"OEM_7", 0xDE}, {"'", 0xDE}, {"\"", 0xDE},
    {"QUOTE", 0xDE}, {"DOUBLE_QUOTE", 0xDE}, {"OEM_8", 0xDF}, {"OEM_102", 0xE2},
    {"PROCESSKEY", 0xE5}, {"ATTN", 0xF6}, {"CRSEL", 0xF7}, {"EXSEL", 0xF8}, {"EREOF", 0xF9},
    {"PLAY", 0xFA}, {"ZOOM", 0xFB}, {"NONAME", 0xFC}, {"PA1", 0xFD}, {"OEM_CLEAR", 0xFE},
    {"Num 1", 0x61}, {"Num 2", 0x62}, {"Num 3", 0x63}, {"Num 4", 0x64}, {"Num 5", 0x65},
    {"Num 6", 0x66}, {"Num 7", 0x67}, {"Num 8", 0x68}, {"Num 9", 0x69}, {"*", 0x6A},
    {"Num /", 0x6F}, {"-", 0x6D}, {"+", 0x6B}, {"Prnt Scrn", 0x2C},
  };
  *count = sizeof(names) / sizeof(names[0]);
  return names;
}
// Same order as ParseVKey: a letter or digit, "0x" hex, then names (VK_ optional).
inline int ParseKeyName(std::string name) {
  if (name.size() == 1) {
    char c = name[0];
    if (c >= 'a' && c <= 'z') c = char(c - 'a' + 'A');
    if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z')) return c;
  }
  if (name.compare(0, 2, "0x") == 0) {
    char *end = nullptr;
    unsigned long vk = std::strtoul(name.c_str() + 2, &end, 16);
    return end != name.c_str() + 2 && vk <= 0xFF ? int(vk) : -1;
  }
  if (HasPrefix(name, "vk_")) name.erase(0, 3);
  const std::string lower = Lower(name);
  size_t count = 0;
  const KeyName *names = KeyNames(&count);
  for (size_t i = 0; i < count; ++i)
    if (Lower(names[i].name) == lower) return names[i].vk;
  return -1;
}
constexpr int kVkShift = 0x10, kVkCtrl = 0x11, kVkAlt = 0x12, kVkLeftCtrl = 0xA2,
              kVkRightCtrl = 0xA3, kVkLeftWin = 0x5B, kVkRightWin = 0x5C;
// Keys that must be down / up. Gamepad buttons are only flagged: they never
// collide with keyboard hotkeys.
struct Chord {
  std::vector<int> down, up;
  bool controller = false;
};
inline bool ParseKeyButton(std::string token, Chord &chord) {
  bool invert = HasPrefix(token, "no_");
  if (invert) token.erase(0, 3);
  int vk = ParseKeyName(token);
  if (vk >= 0) {
    (invert ? chord.up : chord.down).push_back(vk);
    return true;
  }
  // XInputButton: XB[1-4]_NAME.
  if (!HasPrefix(token, "xb")) return false;
  size_t at = 2;
  if (at < token.size() && token[at] >= '1' && token[at] <= '4') ++at;
  if (at >= token.size() || token[at] != '_') return false;
  chord.controller = true;
  return true;
}
// The whole value is tried as one key first ("Num 1"), then split at spaces.
inline bool ParseChord(const std::string &binding, Chord &chord) {
  chord = {};
  const std::string text = Trim(binding);
  if (text.empty()) return false;
  if (ParseKeyButton(text, chord)) return true;
  chord = {};
  for (size_t i = 0; i < text.size();) {
    while (i < text.size() && text[i] == ' ') ++i;
    size_t end = text.find(' ', i);
    if (end == std::string::npos) end = text.size();
    if (end == i) break;
    std::string token = text.substr(i, end - i);
    i = end;
    if (Lower(token) == "no_modifiers") {
      for (int vk : {kVkCtrl, kVkAlt, kVkShift, kVkLeftWin, kVkRightWin}) chord.up.push_back(vk);
    } else if (!ParseKeyButton(token, chord)) {
      chord = {};
      return false;
    }
  }
  return !chord.down.empty() || !chord.up.empty() || chord.controller;
}
// A plugin hotkey: `vk`, optionally with Ctrl, polled without other checks.
struct Hotkey { int vk = 0; bool ctrl = false; };
// True when pressing either one also triggers the other.
inline bool Collides(const Chord &chord, Hotkey hotkey) {
  if (chord.controller || chord.down.empty() || hotkey.vk <= 0) return false;
  auto has = [](const std::vector<int> &keys, int vk) {
    return std::find(keys.begin(), keys.end(), vk) != keys.end();
  };
  std::vector<int> pressed{hotkey.vk};
  if (hotkey.ctrl) pressed.insert(pressed.end(), {kVkCtrl, kVkLeftCtrl});
  bool bindingFires =
      std::all_of(chord.down.begin(), chord.down.end(), [&](int vk) { return has(pressed, vk); }) &&
      std::none_of(chord.up.begin(), chord.up.end(), [&](int vk) { return has(pressed, vk); });
  bool hotkeyFires = has(chord.down, hotkey.vk) &&
                     (!hotkey.ctrl || has(chord.down, kVkCtrl) || has(chord.down, kVkLeftCtrl) ||
                      has(chord.down, kVkRightCtrl));
  return bindingFires || hotkeyFires;
}

// ---- INI text -----------------------------------------------------------------
struct IniLine {
  std::string key, value;
  bool assignment = false;
};
struct IniSection {
  std::string name, comment; // comment: a ";" line right below the header
  std::vector<IniLine> lines;
};
struct IniDocument {
  std::string ns; // "namespace =" from the preamble; empty when absent
  std::vector<IniSection> sections;
};
// Comments between sections usually introduce the next one, and dividers
// like "; General ------" are not labels.
inline std::string CommentLabel(const std::string &line) {
  std::string text = Trim(line.substr(1));
  size_t a = text.find_first_not_of("-=*#~ \t"), b = text.find_last_not_of("-=*#~ \t");
  return a == std::string::npos ? std::string() : text.substr(a, b - a + 1);
}
// Line rules follow ParseIniStream: trimmed lines, ";" comments, [sections],
// the first "=" splits key and value, and "namespace" before any section.
inline IniDocument ParseIni(const std::string &text) {
  IniDocument doc;
  IniSection *section = nullptr;
  size_t at = text.compare(0, 3, "\xEF\xBB\xBF") == 0 ? 3 : 0;
  while (at < text.size()) {
    size_t end = text.find('\n', at);
    if (end == std::string::npos) end = text.size();
    std::string line = Trim(text.substr(at, end - at));
    at = end + 1;
    if (line.empty()) continue;
    if (line[0] == ';') {
      if (section && section->lines.empty() && section->comment.empty()) section->comment = CommentLabel(line);
      continue;
    }
    if (line[0] == '[') {
      size_t close = line.find(']');
      doc.sections.push_back({Trim(line.substr(1, close == std::string::npos ? std::string::npos : close - 1)), {}, {}});
      section = &doc.sections.back();
      continue;
    }
    IniLine entry;
    size_t eq = line.find('=');
    entry.assignment = eq != std::string::npos;
    entry.key = entry.assignment ? Trim(line.substr(0, eq)) : line;
    if (entry.assignment) entry.value = Trim(line.substr(eq + 1));
    if (section) section->lines.push_back(entry);
    else if (entry.assignment && Lower(entry.key) == "namespace") doc.ns = entry.value;
  }
  return doc;
}
inline std::vector<float> ParseValues(const std::string &text) {
  std::vector<float> values;
  for (size_t i = 0; i <= text.size();) {
    size_t end = text.find(',', i);
    if (end == std::string::npos) end = text.size();
    std::string token = Trim(text.substr(i, end - i));
    char *stop = nullptr;
    float v = std::strtof(token.c_str(), &stop);
    if (!token.empty() && stop == token.c_str() + token.size() && std::isfinite(v)) values.push_back(v);
    i = end + 1;
  }
  return values;
}

// ---- Mod catalog -------------------------------------------------------------
// A global variable that some [Key] section assigns, i.e. a mod switch.
struct Variable {
  std::string id;    // lowercase full name, the identity
  std::string name;  // full name as written, e.g. $\Mods\A\a.ini\Collar
  std::string file;  // INI path relative to the EFMI folder
  std::string local; // declared name, e.g. $Collar
  std::string label; // comment of the first key section that sets it
  std::vector<float> values;
  bool persist = false;
};
struct Binding {
  std::string file, section, key;
  Chord chord;
};
struct Catalog {
  std::vector<Variable> variables;
  std::vector<Binding> bindings;
  std::set<int> keys; // every key code some binding reads, down or up
  const Variable *find(const std::string &id) const {
    for (const auto &v : variables)
      if (v.id == id) return &v;
    return nullptr;
  }
};
inline std::string FullName(const std::string &ns, const std::string &local) {
  return ns.empty() ? local : "$\\" + ns + "\\" + local.substr(1);
}
// 3DMigoto declares every global before parsing [Key] sections, so uses are
// resolved in finish(), after all documents have been added. Bindings of
// every file count as conflicts; only variables from mod files (`mod`) with
// at least two values are offered as switches.
class CatalogBuilder {
public:
  void add(const IniDocument &doc, const std::string &file, const std::string &ns, bool mod = true) {
    for (const auto &section : doc.sections) {
      if (Lower(section.name) == "constants") {
        for (const auto &line : section.lines) declare(line, file, ns, mod);
        continue;
      }
      if (!HasPrefix(section.name, "key")) continue;
      for (const auto &line : section.lines) {
        if (!line.assignment) continue;
        std::string key = Lower(line.key);
        if (key == "key" || key == "back") {
          Binding binding{file, section.name, line.value, {}};
          if (!ParseChord(line.value, binding.chord)) continue;
          for (int vk : binding.chord.down) catalog_.keys.insert(vk);
          for (int vk : binding.chord.up) catalog_.keys.insert(vk);
          catalog_.bindings.push_back(binding);
        } else if (line.key.size() > 1 && line.key[0] == '$') {
          uses_.push_back({ns, line.key, section.comment, ParseValues(line.value)});
        }
      }
    }
  }
  Catalog finish() {
    Catalog result = catalog_;
    std::map<std::string, size_t> index;
    for (const auto &use : uses_) {
      const Declared *declared = resolve(use.ns, use.name);
      if (!declared || !declared->mod || use.values.empty()) continue;
      auto found = index.find(declared->id);
      if (found == index.end()) {
        found = index.emplace(declared->id, result.variables.size()).first;
        result.variables.push_back({declared->id, declared->name, declared->file, declared->local,
                                    use.label, {}, declared->persist});
      }
      auto &v = result.variables[found->second];
      if (v.label.empty()) v.label = use.label;
      for (float x : use.values)
        if (std::find(v.values.begin(), v.values.end(), x) == v.values.end()) v.values.push_back(x);
    }
    result.variables.erase(std::remove_if(result.variables.begin(), result.variables.end(),
                                          [](const Variable &v) { return v.values.size() < 2; }),
                           result.variables.end());
    std::stable_sort(result.variables.begin(), result.variables.end(),
                     [](const Variable &a, const Variable &b) { return Lower(a.file) < Lower(b.file); });
    return result;
  }

private:
  struct Declared { std::string id, name, file, local; bool persist = false, mod = true; };
  struct Use { std::string ns, name, label; std::vector<float> values; };
  void declare(const IniLine &line, const std::string &file, const std::string &ns, bool mod) {
    bool global = false, persist = false;
    std::string local;
    for (size_t i = 0; i < line.key.size();) {
      size_t end = line.key.find_first_of(" \t", i);
      if (end == std::string::npos) end = line.key.size();
      std::string token = line.key.substr(i, end - i);
      i = line.key.find_first_not_of(" \t", end);
      if (i == std::string::npos) i = line.key.size();
      std::string lower = Lower(token);
      if (lower == "global") global = true;
      else if (lower == "persist") persist = true;
      else if (token.size() > 1 && token[0] == '$') local = token;
    }
    if (!global || local.empty() || local.find('\\') != std::string::npos) return;
    std::string name = FullName(ns, local);
    declared_[Lower(name)] = {Lower(name), name, file, local, persist, mod};
  }
  // parse_command_list_var_name: the namespaced name first, then as written.
  const Declared *resolve(const std::string &ns, const std::string &name) const {
    if (name.compare(0, 2, "$\\") != 0 && !ns.empty()) {
      auto it = declared_.find(Lower(FullName(ns, name)));
      if (it != declared_.end()) return &it->second;
    }
    auto it = declared_.find(Lower(name));
    return it == declared_.end() ? nullptr : &it->second;
  }
  Catalog catalog_;
  std::map<std::string, Declared> declared_;
  std::vector<Use> uses_;
};

// ---- Bridge plan -------------------------------------------------------------
// Morph weight 0 selects the first value and 1 the last; between, the nearest.
inline float Select(const std::vector<float> &values, float weight) {
  if (values.empty()) return 0;
  if (!std::isfinite(weight)) weight = 0;
  weight = (std::max)(0.f, (std::min)(1.f, weight));
  size_t i = size_t(std::lround(weight * float(values.size() - 1)));
  return values[(std::min)(i, values.size() - 1)];
}
// Virtual-key codes that keyboards do not send: reserved or unassigned in
// WinUser.h. 3DMigoto accepts them as "0x.." key names.
inline const std::vector<int> &CodePool() {
  static const std::vector<int> pool = [] {
    std::vector<int> codes{0x07, 0x0A, 0x0B, 0x0E, 0x0F, 0x5E, 0xB8, 0xB9, 0xE0, 0xE8};
    for (int vk = 0x3A; vk <= 0x40; ++vk) codes.push_back(vk);
    for (int vk = 0x88; vk <= 0x8F; ++vk) codes.push_back(vk);
    for (int vk = 0x97; vk <= 0x9F; ++vk) codes.push_back(vk);
    for (int vk = 0xC3; vk <= 0xDA; ++vk) codes.push_back(vk);
    std::sort(codes.begin(), codes.end());
    return codes;
  }();
  return pool;
}
// One variable value. The hold key saves the variable and sets the value;
// the activate key only sets it.
struct Slot {
  std::string id, name;
  float value = 0;
  int guard = 0, set = 0;
};
struct Pair {
  std::string id, name;
  float value = 0;
};
// What a code does in a bridge file. A code must keep its meaning while a
// loaded 3DMigoto config may still bind it.
inline std::string Meaning(bool hold, const std::string &id, float value) {
  return std::string(hold ? "hold " : "set ") + id + "=" + FormatValue(value);
}
// Keeps the codes of existing slots when `usable` allows and gives new values
// unused codes. Values without two usable codes are counted in `missing`.
template <class Usable>
inline std::vector<Slot> Plan(const std::vector<Slot> &old, const std::vector<Pair> &pairs, Usable usable,
                              size_t *missing) {
  std::vector<Slot> slots;
  std::set<int> taken;
  auto claim = [&](int code, const std::string &meaning) {
    if (code <= 0 || taken.count(code) || !usable(code, meaning)) return 0;
    taken.insert(code);
    return code;
  };
  for (const auto &p : pairs) {
    Slot slot{p.id, p.name, p.value, 0, 0};
    for (const auto &o : old)
      if (o.id == p.id && o.value == p.value) {
        slot.guard = claim(o.guard, Meaning(true, p.id, p.value));
        slot.set = claim(o.set, Meaning(false, p.id, p.value));
      }
    slots.push_back(slot);
  }
  for (auto &slot : slots)
    for (int *code : {&slot.guard, &slot.set})
      for (int c : CodePool()) {
        if (*code) break;
        *code = claim(c, Meaning(code == &slot.guard, slot.id, slot.value));
      }
  *missing = size_t(std::count_if(slots.begin(), slots.end(), [](const Slot &s) { return !s.guard || !s.set; }));
  slots.erase(std::remove_if(slots.begin(), slots.end(), [](const Slot &s) { return !s.guard || !s.set; }),
              slots.end());
  return slots;
}
inline std::string KeyCode(int vk) {
  char b[8];
  std::snprintf(b, sizeof(b), "0x%02X", vk);
  return b;
}
inline std::string BridgeIni(const std::vector<Slot> &slots) {
  std::string s =
      "; Endfield Poser mod bridge. Generated file: edits are overwritten.\n"
      "; Lets MMD morph tracks set mod variables through virtual keys that only\n"
      "; the poser presses. Disable the bridge in the poser to remove this folder.\n"
      "namespace = EndfieldPoserBridge\n";
  for (size_t i = 0; i < slots.size(); ++i) {
    const Slot &p = slots[i];
    std::string assign = p.name + " = " + FormatValue(p.value) + "\n";
    s += "\n[KeyGuard" + std::to_string(i) + "]\nkey = " + KeyCode(p.guard) + "\ntype = hold\n" + assign;
    s += "\n[KeySet" + std::to_string(i) + "]\nkey = " + KeyCode(p.set) + "\ntype = activate\n" + assign;
  }
  return s;
}
// Reads code meanings back from a file written by BridgeIni.
inline std::map<int, std::string> BridgeMeanings(const IniDocument &doc) {
  std::map<int, std::string> meanings;
  for (const auto &section : doc.sections) {
    bool hold = HasPrefix(section.name, "keyguard");
    if (!hold && !HasPrefix(section.name, "keyset")) continue;
    int code = -1;
    std::string id;
    float value = 0;
    for (const auto &line : section.lines) {
      if (!line.assignment) continue;
      if (Lower(line.key) == "key") {
        code = ParseKeyName(line.value);
      } else if (line.key[0] == '$') {
        auto values = ParseValues(line.value);
        if (values.size() == 1) id = Lower(line.key), value = values[0];
      }
    }
    if (code > 0 && !id.empty()) meanings[code] = Meaning(hold, id, value);
  }
  return meanings;
}

// ---- Key protocol ------------------------------------------------------------
// Key states shared with the 3DMigoto input thread, which polls each bound
// key once per frame and acts only on changes between polls. A tap reads
// down for exactly one poll; taps and releases then read up for one poll
// before the code accepts a new press, so no edge is ever merged.
class VirtualKeys {
public:
  enum : uint8_t { Up, Press, Held, Settle };
  void own(int vk, bool on) {
    if (Valid(vk)) owned_[size_t(vk)].store(on);
  }
  bool owned(int vk) const { return Valid(vk) && owned_[size_t(vk)].load(); }
  // Input thread. Counts before reading the state: a poll counted after
  // hold() returned has seen the hold (sequentially consistent atomics).
  bool poll(int vk, uint64_t now) {
    if (!Valid(vk)) return false;
    auto &state = state_[size_t(vk)];
    polls_[size_t(vk)].fetch_add(1);
    last_[size_t(vk)].store(now);
    uint8_t s = state.load();
    if (s == Held) return true;
    if (s == Press) return state.compare_exchange_strong(s, Settle) || s == Held;
    if (s == Settle) state.compare_exchange_strong(s, Up);
    return false;
  }
  uint64_t lastPoll(int vk) const { return Valid(vk) ? last_[size_t(vk)].load() : 0; }
  bool tap(int vk) { return Change(vk, Up, Press); }
  bool hold(int vk, uint64_t *mark) {
    if (!Change(vk, Up, Held)) return false;
    *mark = polls_[size_t(vk)].load();
    return true;
  }
  bool seenSince(int vk, uint64_t mark) const { return Valid(vk) && polls_[size_t(vk)].load() > mark; }
  bool idle(int vk) const { return !Valid(vk) || state_[size_t(vk)].load() == Up; }
  void release(int vk) {
    if (!Change(vk, Held, Settle)) cancel(vk);
  }
  bool cancel(int vk) { return Change(vk, Press, Up); }
  bool pending(int vk) const { return Valid(vk) && state_[size_t(vk)].load() == Press; }
  // For codes the loaded config no longer binds: nothing will poll them.
  void reset(int vk) {
    if (Valid(vk)) state_[size_t(vk)].store(Up);
  }

private:
  static bool Valid(int vk) { return vk > 0 && vk < 256; }
  bool Change(int vk, uint8_t from, uint8_t to) {
    return Valid(vk) && state_[size_t(vk)].compare_exchange_strong(from, to);
  }
  std::array<std::atomic<uint8_t>, 256> state_{};
  std::array<std::atomic<bool>, 256> owned_{};
  std::array<std::atomic<uint64_t>, 256> polls_{}, last_{};
};
// One variable across playback sessions; keep it after Release().
struct Driver {
  int guard = 0, released = 0, pending = 0;
  uint64_t mark = 0;
  bool seen = false;
  float applied = 0, pendingValue = 0;
};
// The first value is set by a held guard key: 3DMigoto saves the variable
// and applies the value. Later values tap activate keys, which leave that
// save alone, so releasing the guard restores the value from before playback.
// A tap waits until the guard has been polled, so it can never be applied
// first and be mistaken for the original value. A new session likewise waits
// until the previous guard was polled up: if 3DMigoto saw a new hold before
// that release, the release would restore over the new value.
template <class Find>
inline void Drive(VirtualKeys &keys, Driver &d, float value, Find find) {
  if (!d.guard) {
    if (d.released && !keys.idle(d.released)) return;
    d.released = 0;
    const Slot *slot = find(value);
    if (!slot || !keys.hold(slot->guard, &d.mark)) return;
    d.guard = slot->guard;
    d.applied = value;
    d.seen = false;
    return;
  }
  if (!d.seen && !(d.seen = keys.seenSince(d.guard, d.mark))) return;
  // At most one unconsumed value per variable. Otherwise a fast seek can
  // leave several taps queued; EFMI's INI order, rather than the playhead,
  // decides which value wins. Only acknowledge a tap once it was polled.
  if (d.pending) {
    if (keys.pending(d.pending)) {
      if (value == d.pendingValue) return;
      if (!keys.cancel(d.pending)) d.applied = d.pendingValue;
    } else d.applied = d.pendingValue;
    d.pending = 0;
  }
  if (value == d.applied) return;
  const Slot *slot = find(value);
  if (slot && keys.tap(slot->set)) { d.pending = slot->set; d.pendingValue = value; }
}
// Pending taps are dropped: one delivered after the release would override
// the restored value.
inline void Release(VirtualKeys &keys, Driver &d, const std::vector<Slot> &slots, const std::string &id) {
  for (const auto &slot : slots)
    if (slot.id == id) keys.cancel(slot.set);
  int released = d.guard ? d.guard : d.released;
  if (d.guard) keys.release(d.guard);
  d = {};
  d.released = released;
}
} // namespace mod_bridge
