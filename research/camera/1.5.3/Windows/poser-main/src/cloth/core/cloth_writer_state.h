#pragma once
#include <cmath>
#include <cstdint>
#include <array>
namespace eiem_cloth {
// Main-thread-only mailbox. The hook can record an unmodified native call when
// the UI owns the pose mutex, without inspecting or writing any game object.
struct DeferredWeightWrites {
  struct Entry {uintptr_t object=0,caller=0;float requested=NAN;uint64_t at=0;};
  std::array<Entry,64> entries{};size_t next=0;
  void Record(uintptr_t object,uintptr_t caller,float requested,uint64_t now) {
    if(!object||!caller||!std::isfinite(requested)||requested<0||requested>1)return;
    for(auto &e:entries)if(e.object==object){e={object,caller,requested,now};return;}
    entries[next++%entries.size()]={object,caller,requested,now};
  }
  Entry Take(uintptr_t object,uint64_t now) {
    for(auto &e:entries)if(e.object==object){const auto saved=e;e={};
      return now>=saved.at&&now-saved.at<=250?saved:Entry{};}
    return {};
  }
};
struct WeightWriterEvidence {
  bool ownWriteConfirmed = false, armed = false;
  uintptr_t caller = 0;
  int lastFrame = -1;
  unsigned observations = 0, intercepted = 0;
  float lastActual = NAN;
  bool recoveryUsed = false;
  uint64_t lastMissedRecovery = 0;
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
  bool RecoverMissed(const DeferredWeightWrites::Entry &missed,float actual,float property) {
    if(!missed.object||missed.at<=lastMissedRecovery||!ShouldOverride(missed.caller,missed.requested)||
        !std::isfinite(actual)||!std::isfinite(property)||std::fabs(actual-1)<=.001f||
        std::fabs(actual-missed.requested)>.000001f||std::fabs(property-actual)>.000001f)return false;
    lastMissedRecovery=missed.at;return true;
  }
};
}
