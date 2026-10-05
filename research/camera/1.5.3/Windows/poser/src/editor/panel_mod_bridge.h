#pragma once
#include "core/build_features.h"
#if !POSER_ENABLE_XXMI_BRIDGE
#error The XXMI bridge panel requires a separate opt-in build.
#endif
#include "config.h"
#include "game/mmd_player.h"
#include "imgui.h"

// Main panel: conflicts matter even while the bridge is disabled.
static void DrawModBridgeConflictNotice() {
  const auto conflicts = ModBridgeConflicts();
  if (!conflicts.empty())
    ImGui::TextDisabled(u8" %d 个 mod 快捷键与插件热键冲突，需要调整，详见 MMD 播放器中的“Mod 联动”",
                        int(conflicts.size()));
}
static std::string ModBridgeValueList(const std::vector<float> &values) {
  std::string text;
  for (float v : values) text += (text.empty() ? "" : u8"、") + mod_bridge::FormatValue(v);
  return text;
}
static void DrawModBridgeConflicts() {
  const auto conflicts = ModBridgeConflicts();
  if (conflicts.empty()) return;
  char title[96] = {};
  snprintf(title, sizeof(title), u8"快捷键冲突（%d）###modbridgeconflicts", int(conflicts.size()));
  if (!ImGui::TreeNodeEx(title, ImGuiTreeNodeFlags_DefaultOpen)) return;
  for (const auto &c : conflicts)
    ImGui::TextWrapped(u8"%s 的 [%s]（%s）与插件热键“%s”（%s）冲突", c.file.c_str(), c.section.c_str(),
                       c.key.c_str(), c.hotkey.c_str(), c.keys.c_str());
  ImGui::TextWrapped(u8"按下其中一个时都会响应。请在主面板“快捷键设置”中换用其他组合键，或修改该 mod 的按键。");
  ImGui::TreePop();
}
static void DrawModBridgeStatus() {
  const auto &b = g_modBridge;
  const ULONGLONG now = GetTickCount64();
  if (!b.hooked) {
    ImGui::TextWrapped(u8"Mod 联动未就绪：%s", b.hookError.empty() ? u8"等待 EFMI 加载" : b.hookError.c_str());
    return;
  }
  int total = int(b.slots.size()), live = 0;
  for (const auto &s : b.slots) live += ModBridgeLive(s.guard, now) && ModBridgeLive(s.set, now);
  if (!total) ImGui::TextDisabled(u8"尚未绑定表情轨道");
  else if (live == total) ImGui::Text(u8"联动已生效：%d 个取值", total);
  else if (s_modLastInput.load() + 1500 < now) ImGui::TextWrapped(u8"等待 3DMigoto 读取按键：游戏窗口需要处于前台。");
  else
    ImGui::TextWrapped(u8"%d 个取值尚未生效：请在停止播放后按一次 F10 或重启游戏。如果在 XXMI 中关闭了 mod 按键，也需要重新打开。",
                       total - live);
  ImGui::TextDisabled(u8"专用虚拟键 %d / %d", 2 * total, int(mod_bridge::CodePool().size()));
  if (b.missing)
    ImGui::TextWrapped(u8"虚拟键不足，%d 个取值未能绑定。请减少绑定的变量或取值。或进行问题报告。", int(b.missing));
}
static void DrawModBridgeTargets() {
  auto &b = g_modBridge;
  auto &m = g_mmd;
  static bool showAll = false;
  ImGui::Checkbox(u8"显示全部表情轨道", &showAll);
  if (ImGui::IsItemHovered())
    ImGui::SetTooltip(u8"默认只列出已绑定的轨道，以及没有对应角色表情的自定义轨道。");
  if (m.clip.morphs.empty())
    ImGui::TextWrapped(u8"打开含表情轨道的 VMD 后，可以在这里把轨道绑定到 mod 变量。");
  bool changed = false;
  ImGui::BeginDisabled(MmdOwnsPose() || !b.scanned);
  for (const auto &track : m.clip.morphs) {
    const std::string &name = track.first;
    auto bound = b.morphs.find(name);
    auto face = m.morphMap.find(name);
    bool faceMapped = face != m.morphMap.end() && (face->second.slider >= 0 || face->second.nativeSlider >= 0);
    if (!showAll && faceMapped && (bound == b.morphs.end() || bound->second.empty())) continue;
    ImGui::PushID(name.c_str());
    ImGui::TextUnformatted(name.c_str());
    auto &targets = b.morphs[name];
    int remove = -1;
    for (int i = 0; i < int(targets.size()); ++i) {
      auto &t = targets[size_t(i)];
      ImGui::PushID(i);
      ImGui::Bullet();
      ImGui::TextWrapped("%s%s", t.label.c_str(), b.scan.catalog.find(t.id) ? "" : u8"（mod 未加载）");
      ImGui::Indent();
      ImGui::TextDisabled(u8"权重从 0 到 1 依次对应：%s", ModBridgeValueList(t.values).c_str());
      if (t.values.size() > 1 && ImGui::SmallButton(u8"反转顺序")) {
        std::reverse(t.values.begin(), t.values.end());
        changed = true;
      }
      if (t.values.size() > 1) ImGui::SameLine();
      if (ImGui::SmallButton(u8"移除")) remove = i;
      ImGui::Unindent();
      ImGui::PopID();
    }
    if (remove >= 0) {
      targets.erase(targets.begin() + remove);
      changed = true;
    }
    ImGui::SetNextItemWidth(-1);
    if (ImGui::BeginCombo("##add", u8"添加 mod 变量…", ImGuiComboFlags_HeightLarge)) {
      std::string file;
      const auto &variables = b.scan.catalog.variables;
      for (size_t i = 0; i < variables.size(); ++i) {
        const auto &v = variables[i];
        if (v.file != file) ImGui::SeparatorText((file = v.file).c_str());
        bool added = std::any_of(targets.begin(), targets.end(), [&](const ModBridgeTarget &t) { return t.id == v.id; });
        std::string label = ModBridgeVariableLabel(v);
        ImGui::PushID(int(i));
        if (ImGui::Selectable(label.c_str(), added, added ? ImGuiSelectableFlags_Disabled : 0)) {
          targets.push_back({v.id, v.name, label, v.values});
          changed = true;
        }
        ImGui::PopID();
      }
      ImGui::EndCombo();
    }
    ImGui::PopID();
  }
  ImGui::EndDisabled();
  int elsewhere = 0;
  for (const auto &kv : b.morphs) elsewhere += !kv.second.empty() && !m.clip.morphs.count(kv.first);
  if (elsewhere) ImGui::TextDisabled(u8"另有 %d 条轨道的绑定不在当前动作中，已保留", elsewhere);
  if (MmdOwnsPose()) ImGui::TextDisabled(u8"停止并恢复后可以编辑绑定");
  if (changed) {
    ModBridgeSync();
    MmdReport();
  }
}
static void DrawModBridgePanel() {
  if (!ImGui::CollapsingHeader(u8"Mod 联动（实验）")) return;
  auto &b = g_modBridge;
  if (!b.module) {
    ImGui::TextWrapped(b.probes < 60 ? u8"正在检测 XXMI / EFMI…"
                                     : u8"本次运行没有检测到 EFMI。通过 XXMI 启动游戏后，这里会读取 Mods 中的 mod 设置。");
    if (b.probes >= 60 && ImGui::SmallButton(u8"重新检测")) b.probes = 0, b.nextProbe = 0;
    return;
  }
  ImGui::TextWrapped(u8"EFMI：%s", mmd::Utf8(b.root).c_str());
  if (b.scanning) ImGui::TextDisabled(u8"正在读取 mod 设置…");
  else if (b.scanned)
    ImGui::TextDisabled(u8"已读取 %d 个 INI，可切换的变量 %d 个", b.scan.files, int(b.scan.catalog.variables.size()));
  if (!b.scan.error.empty()) ImGui::TextWrapped(u8"读取失败：%s", b.scan.error.c_str());
  ImGui::SameLine();
  ImGui::BeginDisabled(b.scanning);
  if (ImGui::SmallButton(u8"重新读取")) ModBridgeStartScan();
  ImGui::EndDisabled();
  DrawModBridgeConflicts();
  ImGui::Separator();
  bool enabled = b.enabled;
  ImGui::BeginDisabled(MmdOwnsPose());
  if (ImGui::Checkbox(u8"用表情轨道切换 mod 变量", &enabled)) {
    ModBridgeSetEnabled(enabled);
    MmdReport();
  }
  ImGui::EndDisabled();
  if (ImGui::IsItemHovered(ImGuiHoveredFlags_AllowWhenDisabled))
    ImGui::SetTooltip(u8"在 Mods 中写入 EndfieldPoserBridge 文件夹，并只让 3DMigoto 读到专用的虚拟按键；游戏本身收不到这些按键。关闭后删除该文件夹。");
  if (!b.enabled) {
    ImGui::TextWrapped(u8"启用后，可以把 VMD 中的表情轨道绑定到 mod 的衣着开关等变量，播放时随关键帧切换。");
    return;
  }
  if (!b.error.empty()) ImGui::TextWrapped("%s", b.error.c_str());
  DrawModBridgeStatus();
  DrawModBridgeTargets();
  ImGui::TextWrapped(u8"在 MMD 或 Blender 中给自定义名称的表情打关键帧（例如“襟OFF”），再在这里绑定到 mod 变量。权重 0 对应第一个值，1 对应最后一个值，中间取最近的一档；只有两档时以 0.5 为界。");
  ImGui::TextWrapped(u8"开始播放时，3DMigoto 会记下这些变量当前的值，停止并恢复后还原。新增绑定后，需要在停止播放时按一次 F10 或重启游戏。");
  ImGui::TextWrapped(u8"在播放中按下 F10 会导致 XXMI 记忆的 mod 变量值被覆盖，不要在播放中按 F10。");
}
