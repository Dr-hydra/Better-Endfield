#pragma once

namespace poser {
// Separate the game-controlled actor from a manually selected scene actor.
// Only a real game selection change (or loss of the selected object) cancels
// the override; periodic recovery polls must not steal the editing target.
class EditSelection {
  void *controller_ = nullptr;
  void *entity_ = nullptr;
  bool manual_ = false;
public:
  void observe(void *controller, void *entity, bool authoritative = false) {
    const bool changed = (controller && controller != controller_) ||
                         ((entity || authoritative) && entity != entity_);
    if (changed) manual_ = false;
    if (controller) controller_ = controller;
    if (entity || authoritative) entity_ = entity;
  }
  void selectManual() { manual_ = true; }
  void clearOverride() { manual_ = false; }
  bool keepManual(bool alive) {
    if (!alive) manual_ = false;
    return manual_;
  }
};
} // namespace poser
