#pragma once
#include "mmd_motion.h"

namespace mmd {
enum class AudioAction { Silent, Keep, Restart };
struct AudioPlan {
  AudioAction action = AudioAction::Silent;
  double seconds = 0;
};
// The monotonic motion clock is authoritative. Positive offset delays music.
// Do not seek at each graphics frame: audio is independently sample-clocked.
inline AudioPlan PlanAudio(const Timeline &t, bool active, bool enabled,
                           double duration, double offset, bool running,
                           double playedSeconds, bool wrapped) {
  AudioPlan p;
  p.seconds = t.seconds - offset;
  if (!active || !enabled || t.clockHeld || t.state != PlayState::Playing ||
      !std::isfinite(p.seconds) || p.seconds < 0 || p.seconds >= duration)
    return p;
  p.action = !running || wrapped || std::abs(playedSeconds - p.seconds) > .1
                 ? AudioAction::Restart
                 : AudioAction::Keep;
  return p;
}
} // namespace mmd
