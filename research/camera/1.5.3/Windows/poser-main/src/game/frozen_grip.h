#pragma once
// Retain managed wrappers while frozen, but never operate on destroyed native
// objects. Character handoff releases the old grip before adopting new handles.
#include "core/game_hooks.h"
#include <memory>
#include <vector>

static void FreeGripHandle(uint32_t handle) {
  __try {
    if (handle && !RuntimeClosing() && il2cpp_gchandle_free)
      il2cpp_gchandle_free(handle);
  } __except (1) {
  }
}
struct GripReferences {
  std::vector<uint32_t> handles;
  ~GripReferences() {
    for (auto h : handles)
      FreeGripHandle(h);
  }
};
struct GripComponent {
  void *object = nullptr;
  bool wasEnabled = false;
};
struct FrozenGrip {
  void *animator = nullptr;
  std::vector<GripComponent> components;
  std::shared_ptr<GripReferences> references =
      std::make_shared<GripReferences>();
};
static std::vector<FrozenGrip> g_frozenGrips;
static void CaptureGripComponent(FrozenGrip &grip, void *object) {
  for (const auto &c : grip.components)
    if (c.object == object)
      return;
  bool enabled;
  if (!ReadBehaviourEnabled(object, enabled))
    return;
  if (!il2cpp_gchandle_new || !il2cpp_gchandle_free)
    return;
  uint32_t handle = il2cpp_gchandle_new(object, false);
  if (!handle)
    return;
  grip.references->handles.push_back(handle);
  grip.components.push_back({object, enabled});
}
static void ApplyGripEnabled(const FrozenGrip &grip, bool restore) {
  if (CharacterSwitchInProgress() || !UnityObjAlive(grip.animator))
    return;
  for (const auto &c : grip.components)
    WriteBehaviourEnabled(c.object, restore ? c.wasEnabled : false);
}
static void MaintainFrozenGrips() {
  if (CharacterSwitchInProgress())
    return;
  for (size_t i = 0; i < g_frozenGrips.size();) {
    if (!UnityObjAlive(g_frozenGrips[i].animator)) {
      g_frozenGrips.erase(g_frozenGrips.begin() + i);
      continue;
    }
    ApplyGripEnabled(g_frozenGrips[i], false);
    ++i;
  }
}
static void ReleaseGripFor(void *animator) {
  for (size_t i = 0; i < g_frozenGrips.size(); ++i) {
    if (g_frozenGrips[i].animator != animator)
      continue;
    ApplyGripEnabled(g_frozenGrips[i], true);
    g_frozenGrips.erase(g_frozenGrips.begin() + i);
    Log("[POSER] frozen grip released (animator=%p, left=%d)", animator,
        int(g_frozenGrips.size()));
    return;
  }
}
static void ReleaseAllGrips() {
  for (const auto &g : g_frozenGrips)
    ApplyGripEnabled(g, true);
  g_frozenGrips.clear();
}
