#pragma once
#include <cmath>
#include <cstdint>
namespace eiem_cloth {
struct WeightWriterEvidence {
  bool ownWriteConfirmed = false, armed = false;
  uintptr_t caller = 0;
  int lastFrame = -1;
  unsigned observations = 0, intercepted = 0;
  float lastActual = NAN;
  bool recoveryUsed = false;
  bool Observe(int frame, uintptr_t source, float requested, float actual) {
    if (!ownWriteConfirmed || !source || frame < 0 || !std::isfinite(requested) || requested < 0 ||
        requested > 1 || std::fabs(requested - 1) <= .001f || !std::isfinite(actual) ||
        std::fabs(actual - requested) > .000001f)
      return false;
    if (caller != source) {
      caller = source;
      observations = 0;
      lastFrame = -1;
      armed = false;
    }
    if (frame > lastFrame) {
      ++observations;
      lastFrame = frame;
    }
    lastActual = actual;
    const bool newlyArmed = !armed && observations >= 2;
    armed |= newlyArmed;
    return newlyArmed;
  }
  bool RecoverPersistent(int frame, float actual, float property) {
    if (!ownWriteConfirmed || recoveryUsed || !caller || !observations || frame <= lastFrame ||
        !std::isfinite(actual) || !std::isfinite(property) || !std::isfinite(lastActual) ||
        std::fabs(actual - lastActual) > .000001f ||
        std::fabs(property - actual) > .000001f || std::fabs(actual - 1) <= .001f)
      return false;
    recoveryUsed = armed = true;
    return true;
  }
  bool ShouldOverride(uintptr_t source, float requested) const {
    return ownWriteConfirmed && armed && source == caller && std::isfinite(requested) &&
           requested >= 0 && requested <= 1;
  }
};
}
