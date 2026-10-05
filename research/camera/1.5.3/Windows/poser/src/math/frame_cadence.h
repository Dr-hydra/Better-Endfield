#pragma once
#include <cstdint>
#include <limits>

namespace poser {
// Callers serialize this gate. A camera callback can occur many times per
// frame.
struct FrameCadence {
  int lastFrame = -1;
  double lastStep = -std::numeric_limits<double>::infinity();
  bool gameDue(int frame) const { return frame != lastFrame; }
  bool fallbackDue(double now, double lastRender) const {
    return now - lastRender > .25 && now - lastStep >= 1.0 / 120;
  }
  void stepped(double now, int frame = -1) {
    lastStep = now;
    if (frame >= 0)
      lastFrame = frame;
  }
};
} // namespace poser
