#pragma once
#include <cstdint>
#include <cstddef>
#include <cmath>

namespace eiem_cloth {
constexpr uint64_t StartupBudgetMs = 8000;
constexpr uint64_t PollMs = 100;
constexpr uint64_t AuditMs = 1000;
constexpr uint64_t DiscoveryMs = 500;
constexpr unsigned DiscoveryAttempts = 8;
constexpr unsigned RestoreAttempts = 3;
constexpr unsigned ResolveAttempts = 3;
constexpr float PlaybackWeight = 1.0f;
constexpr float WeightTolerance = 0.001f;
constexpr float PlaybackPoseRatio = 0.0f;
static inline bool PoseRatioAtTarget(float value) {
  return std::isfinite(value) && std::fabs(value - PlaybackPoseRatio) <= 0.000001f;
}
static inline bool WeightAtTarget(float value) {
  return std::isfinite(value) && std::fabs(value - PlaybackWeight) <= WeightTolerance;
}

struct Owner {
  uint32_t backend = 0;
  uint64_t generation = 0;
  uintptr_t character = 0;
  uint64_t session = 0;
  bool operator==(const Owner &r) const {
    return backend == r.backend && generation == r.generation &&
           character == r.character && session == r.session;
  }
};

static inline bool Accepts(bool active, const Owner &lease, const Owner &work) {
  return active && lease == work;
}

struct ObjectId {
  uintptr_t managed = 0;
  int instance = 0;
  bool operator==(const ObjectId &r) const {
    return managed == r.managed && instance == r.instance;
  }
};

template <class T, size_t Capacity> struct Originals {
  struct Entry { ObjectId id{}; T value{}; } entries[Capacity]{};
  size_t count = 0;
  T *Find(ObjectId id) {
    for (size_t i = 0; i < count; ++i)
      if (entries[i].id == id) return &entries[i].value;
    return nullptr;
  }
  T *Capture(ObjectId id, const T &value) {
    if (T *old = Find(id)) return old;
    if (count == Capacity) return nullptr;
    entries[count] = {id, value};
    return &entries[count++].value;
  }
};

struct Observation {
  bool readable = false, alive = true, active = true, enabled = true;
  bool process = false, valid = false, running = false, processEnabled = false;
  bool skip = false, culled = false, paused = false;
  bool weightKnown = false, weightAtTarget = false;
  bool poseRatioKnown = false, poseRatioAtTarget = false;
};
enum class Phase { Waiting, Suspended, Verifying, Ready, Failed };
enum class Action { None, Enable, Build, ClearSkip, SetWeight, SetPoseRatio, ReadbackReady, Fail };

struct Startup {
  Phase phase = Phase::Waiting;
  uint64_t elapsed = 0, lastTime = 0;
  int lastFrame = -1;
  bool enableSent = false, buildSent = false, skipSent = false;
  bool weightSent = false, weightConfirmed = false;
  bool poseRatioSent = false, poseRatioConfirmed = false;
  bool commandFailed = false, seenObservation = false;
  bool wasSuspended = false;
  unsigned readyReads = 0;
  const char *reason = "waiting-for-readback";

  Action Step(const Observation &o, uint64_t now, int frame) {
    if (frame < 0 || frame == lastFrame || phase == Phase::Failed)
      return Action::None;
    lastFrame = frame;
    const bool suspended = o.readable && (!o.active || o.culled || o.paused);
    if (seenObservation && !wasSuspended && !suspended)
      elapsed += now >= lastTime ? now - lastTime : 0;
    lastTime = now;
    seenObservation = true;
    wasSuspended = suspended;
    if (!o.alive) return Fail("unity-object-destroyed");
    if (commandFailed) return Fail("command-failed-or-managed-exception");
    if (!o.readable) {
      phase = Phase::Waiting;
      readyReads = 0;
      if (elapsed >= StartupBudgetMs) return Fail("state-api-unavailable");
      reason = "state-api-unavailable";
      return Action::None;
    }
    if (suspended) {
      phase = Phase::Suspended;
      readyReads = 0;
      reason = !o.active ? "inactive-variant" : o.culled ? "native-culling" : "game-paused";
      return Action::None;
    }
    if (!o.enabled) {
      if (buildSent) return Fail("disabled-after-explicit-build-no-auto-build-reentry");
      if (enableSent) return Fail("enabled-readback-failed-or-overwritten");
      enableSent = true;
      phase = Phase::Verifying;
      reason = "enable-command-auto-build-may-be-pending";
      return Action::Enable;
    }
    if (!o.process) {
      readyReads = 0;
      if (elapsed >= StartupBudgetMs) return Fail("process-timeout");
      phase = Phase::Waiting;
      if (!enableSent && !buildSent && elapsed >= PollMs) {
        buildSent = true;
        reason = "build-command-awaiting-readback";
        return Action::Build;
      }
      reason = "waiting-for-process";
      return Action::None;
    }
    if (!o.valid || !o.running || !o.processEnabled) {
      readyReads = 0;
      if (elapsed >= StartupBudgetMs) return Fail("process-not-running-timeout");
      phase = Phase::Waiting;
      reason = "process-present-not-ready-no-rebuild";
      return Action::None;
    }
    if (o.skip) {
      readyReads = 0;
      if (skipSent) return Fail("skip-readback-failed-or-overwritten");
      skipSent = true;
      phase = Phase::Verifying;
      reason = "skip-command-awaiting-readback";
      return Action::ClearSkip;
    }
    if (!o.weightKnown || !o.weightAtTarget) {
      readyReads = 0;
      phase = Phase::Verifying;
      if (weightConfirmed) return Fail("weight-readback-lost-or-overwritten-no-reassert");
      if (elapsed >= StartupBudgetMs) return Fail("weight-readback-timeout");
      if (!o.weightKnown) {
        reason = "weight-readback-unavailable";
        return Action::None;
      }
      if (!weightSent) {
        weightSent = true;
        reason = "weight-command-awaiting-readback";
        return Action::SetWeight;
      }
      reason = "weight-command-pending-no-reassert";
      return Action::None;
    }
    if (weightSent && (!o.poseRatioKnown || !o.poseRatioAtTarget)) {
      readyReads = 0;
      phase = Phase::Verifying;
      if (poseRatioConfirmed) return Fail("pose-ratio-readback-lost-or-overwritten-no-reassert");
      if (elapsed >= StartupBudgetMs) return Fail("pose-ratio-readback-timeout");
      if (!o.poseRatioKnown) {
        reason = "pose-ratio-readback-unavailable";
        return Action::None;
      }
      if (!poseRatioSent) {
        poseRatioSent = true;
        reason = "pose-ratio-command-awaiting-readback";
        return Action::SetPoseRatio;
      }
      reason = "pose-ratio-command-pending-no-reassert";
      return Action::None;
    }
    phase = ++readyReads >= 2 ? Phase::Ready : Phase::Verifying;
    reason = phase == Phase::Ready ? "api-ready-simulation-unverified" : "confirm-next-frame";
    if (phase == Phase::Ready) {
      elapsed = 0;
      weightConfirmed = weightSent;
      poseRatioConfirmed = poseRatioSent;
    }
    return phase == Phase::Ready ? Action::ReadbackReady : Action::None;
  }
  Action Fail(const char *why) {
    phase = Phase::Failed;
    reason = why;
    return Action::Fail;
  }
  void CommandResult(bool invokeOk, bool returnedSuccess = true) {
    commandFailed = !invokeOk || !returnedSuccess;
  }
};
}
