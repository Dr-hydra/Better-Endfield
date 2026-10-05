#pragma once

#include <atomic>
#include <cstdint>
#include <cstring>
#include <windows.h>

enum class DirectVmdRuntimeCommand : uint32_t {
  Stopped = 0,
  Playing,
  Paused,
};

static SRWLOCK s_directVmdCommandLock = SRWLOCK_INIT;
static SRWLOCK s_directVmdLifecycleLock = SRWLOCK_INIT;
static char s_directVmdRequestedPath[512] = {};
static char s_directVmdRequestedCameraOverridePath[512] = {};
static char s_directVmdRequestedMorphOverridePath[512] = {};
static double s_directVmdRequestedSeekFrame = 0.0;

static std::atomic<uint64_t> s_directVmdClipRequestRevision{1};
static std::atomic<uint64_t> s_directVmdCameraOverrideRequestRevision{1};
static std::atomic<uint64_t> s_directVmdMorphOverrideRequestRevision{1};
static std::atomic<uint64_t> s_directVmdPlaybackRequestRevision{1};
static std::atomic<uint64_t> s_directVmdSeekRequestRevision{0};
static std::atomic<uint32_t> s_directVmdPlaybackRequest{
    static_cast<uint32_t>(DirectVmdRuntimeCommand::Stopped)};
static std::atomic<bool> s_directVmdRuntimeActive{false};
static std::atomic<bool> s_directVmdLoop{false};
static std::atomic<float> s_directVmdSpeed{1.0f};
static std::atomic<float> s_directVmdMotionMultiplier{1.0f};
static std::atomic<uint64_t> s_directVmdTargetGeneration{0};
static std::atomic<uintptr_t> s_directVmdTargetOwner{0};

static SRWLOCK s_directVmdFrameLock = SRWLOCK_INIT;
static DirectVmdSampleFrame s_directVmdLatestFrame = {};

static std::atomic<bool> s_directVmdLoadedPublic{false};
static std::atomic<double> s_directVmdFramePublic{0.0};
static std::atomic<double> s_directVmdDurationPublic{0.0};
static std::atomic<uint32_t> s_directVmdPlaybackPublic{
    static_cast<uint32_t>(DirectVmdPlaybackState::Stopped)};
static std::atomic<uint64_t> s_directVmdClipGenerationPublic{0};
static std::atomic<bool> s_directVmdCameraOverrideLoadedPublic{false};
static std::atomic<bool> s_directVmdMorphOverrideLoadedPublic{false};
static std::atomic<uint64_t> s_directVmdPublishedSequence{0};

struct DirectVmdWorkerState {
  DirectVmdResource resource;
  DirectVmdResource cameraOverrideResource;
  DirectVmdResource morphOverrideResource;
  DirectVmdClock clock;
  uint64_t handledClipRevision = 0;
  uint64_t handledCameraOverrideRevision = 0;
  uint64_t handledMorphOverrideRevision = 0;
  uint64_t handledPlaybackRevision = 0;
  uint64_t handledSeekRevision = 0;
  uint64_t sequence = 0;
  bool previousActive = false;
  bool pausedForInactive = false;
  bool qpcReady = false;
  LARGE_INTEGER frequency = {};
  LARGE_INTEGER previousTick = {};
};

static DirectVmdWorkerState s_directVmdWorker;

static void DirectVmdRuntime_PublishFrame(
    const DirectVmdSampleFrame &frame) {
  AcquireSRWLockExclusive(&s_directVmdFrameLock);
  s_directVmdLatestFrame = frame;
  ReleaseSRWLockExclusive(&s_directVmdFrameLock);
  s_directVmdPublishedSequence.store(frame.sequence,
                                     std::memory_order_release);
  s_directVmdFramePublic.store(frame.sourceFrame,
                               std::memory_order_release);
  s_directVmdDurationPublic.store(frame.durationFrames,
                                  std::memory_order_release);
  s_directVmdPlaybackPublic.store(
      static_cast<uint32_t>(frame.playback), std::memory_order_release);
}

static void DirectVmdRuntime_PublishInvalidWorkerFrame() {
  DirectVmdSampleFrame frame;
  frame.sequence = ++s_directVmdWorker.sequence;
  frame.rigGeneration =
      s_directVmdTargetGeneration.load(std::memory_order_acquire);
  frame.ownerCharacter =
      s_directVmdTargetOwner.load(std::memory_order_acquire);
  frame.clipGeneration = s_directVmdWorker.resource.generation;
  frame.playbackCycle = s_directVmdWorker.clock.loopCycle;
  frame.seekRevision = s_directVmdWorker.handledSeekRevision;
  frame.playback = s_directVmdWorker.clock.state;
  DirectVmdRuntime_PublishFrame(frame);
}

static bool DirectVmdRuntime_CopyLatestFrame(
    DirectVmdSampleFrame *output) {
  if (!output)
    return false;
  AcquireSRWLockShared(&s_directVmdFrameLock);
  *output = s_directVmdLatestFrame;
  ReleaseSRWLockShared(&s_directVmdFrameLock);
  return output->valid != 0;
}

static void DirectVmdRuntime_SetTarget(uint64_t generation,
                                       uintptr_t ownerCharacter) {
  s_directVmdTargetOwner.store(ownerCharacter, std::memory_order_release);
  s_directVmdTargetGeneration.store(generation,
                                     std::memory_order_release);
}

static void DirectVmdRuntime_SetActive(bool active) {
  s_directVmdRuntimeActive.store(active, std::memory_order_release);
}

static void DirectVmdRuntime_BeginLifecycleMutation() {
  AcquireSRWLockExclusive(&s_directVmdLifecycleLock);
}

static void DirectVmdRuntime_EndLifecycleMutation() {
  ReleaseSRWLockExclusive(&s_directVmdLifecycleLock);
}

static void DirectVmdRuntime_RequestLoad(const char *path) {
  AcquireSRWLockExclusive(&s_directVmdCommandLock);
  if (path) {
    strncpy_s(s_directVmdRequestedPath, sizeof(s_directVmdRequestedPath),
              path, _TRUNCATE);
  } else {
    s_directVmdRequestedPath[0] = '\0';
  }
  ReleaseSRWLockExclusive(&s_directVmdCommandLock);
  s_directVmdLoadedPublic.store(false, std::memory_order_release);
  s_directVmdPlaybackRequest.store(
      static_cast<uint32_t>(DirectVmdRuntimeCommand::Stopped),
      std::memory_order_release);
  s_directVmdPlaybackRequestRevision.fetch_add(1,
                                                std::memory_order_acq_rel);
  s_directVmdClipRequestRevision.fetch_add(1, std::memory_order_acq_rel);
}

static void DirectVmdRuntime_RequestUnload() {
  DirectVmdRuntime_RequestLoad(nullptr);
}

static void DirectVmdRuntime_RequestCameraOverride(const char *path) {
  AcquireSRWLockExclusive(&s_directVmdCommandLock);
  if (path && path[0]) {
    strncpy_s(s_directVmdRequestedCameraOverridePath,
              sizeof(s_directVmdRequestedCameraOverridePath), path,
              _TRUNCATE);
  } else {
    s_directVmdRequestedCameraOverridePath[0] = '\0';
  }
  ReleaseSRWLockExclusive(&s_directVmdCommandLock);
  s_directVmdCameraOverrideLoadedPublic.store(false,
                                               std::memory_order_release);
  s_directVmdCameraOverrideRequestRevision.fetch_add(
      1, std::memory_order_acq_rel);
}

static void DirectVmdRuntime_RequestMorphOverride(const char *path) {
  AcquireSRWLockExclusive(&s_directVmdCommandLock);
  if (path && path[0]) {
    strncpy_s(s_directVmdRequestedMorphOverridePath,
              sizeof(s_directVmdRequestedMorphOverridePath), path,
              _TRUNCATE);
  } else {
    s_directVmdRequestedMorphOverridePath[0] = '\0';
  }
  ReleaseSRWLockExclusive(&s_directVmdCommandLock);
  s_directVmdMorphOverrideLoadedPublic.store(false,
                                              std::memory_order_release);
  s_directVmdMorphOverrideRequestRevision.fetch_add(
      1, std::memory_order_acq_rel);
}

static void DirectVmdRuntime_RequestPlayback(
    DirectVmdRuntimeCommand command) {
  s_directVmdPlaybackRequest.store(static_cast<uint32_t>(command),
                                    std::memory_order_release);
  s_directVmdPlaybackRequestRevision.fetch_add(1,
                                                std::memory_order_acq_rel);
}

static void DirectVmdRuntime_RequestPlay() {
  DirectVmdRuntime_RequestPlayback(DirectVmdRuntimeCommand::Playing);
}

static void DirectVmdRuntime_RequestPause() {
  DirectVmdRuntime_RequestPlayback(DirectVmdRuntimeCommand::Paused);
}

static void DirectVmdRuntime_RequestStop() {
  DirectVmdRuntime_RequestPlayback(DirectVmdRuntimeCommand::Stopped);
}

static void DirectVmdRuntime_RequestSeekFrame(double frame) {
  AcquireSRWLockExclusive(&s_directVmdCommandLock);
  s_directVmdRequestedSeekFrame = frame;
  ReleaseSRWLockExclusive(&s_directVmdCommandLock);
  s_directVmdSeekRequestRevision.fetch_add(1, std::memory_order_acq_rel);
}

static void DirectVmdRuntime_SetLoop(bool loop) {
  s_directVmdLoop.store(loop, std::memory_order_release);
}

static void DirectVmdRuntime_SetSpeed(float speed) {
  if (!std::isfinite(speed))
    return;
  s_directVmdSpeed.store((std::max)(0.05f, (std::min)(speed, 4.0f)),
                         std::memory_order_release);
}

static void DirectVmdRuntime_SetMotionMultiplier(float multiplier) {
  if (!std::isfinite(multiplier))
    return;
  s_directVmdMotionMultiplier.store(
      (std::max)(0.05f, (std::min)(multiplier, 5.0f)),
      std::memory_order_release);
}

static float DirectVmdRuntime_GetMotionMultiplier() {
  return s_directVmdMotionMultiplier.load(std::memory_order_acquire);
}

static float DirectVmdRuntime_GetSpeed() {
  return s_directVmdSpeed.load(std::memory_order_acquire);
}

static bool DirectVmdRuntime_IsLoaded() {
  return s_directVmdLoadedPublic.load(std::memory_order_acquire);
}

static double DirectVmdRuntime_PublicFrame() {
  return s_directVmdFramePublic.load(std::memory_order_acquire);
}

static double DirectVmdRuntime_PublicDurationFrames() {
  return s_directVmdDurationPublic.load(std::memory_order_acquire);
}

static DirectVmdPlaybackState DirectVmdRuntime_PublicPlayback() {
  return static_cast<DirectVmdPlaybackState>(
      s_directVmdPlaybackPublic.load(std::memory_order_acquire));
}

static bool DirectVmdRuntime_WantsRealtimeWorkerCadence() {
  return s_directVmdRuntimeActive.load(std::memory_order_acquire) &&
         s_directVmdLoadedPublic.load(std::memory_order_acquire) &&
         DirectVmdRuntime_PublicPlayback() ==
             DirectVmdPlaybackState::Playing;
}

static bool DirectVmdRuntime_WantsResponsiveWorkerCadence() {
  if (!s_directVmdRuntimeActive.load(std::memory_order_acquire) ||
      !s_directVmdLoadedPublic.load(std::memory_order_acquire))
    return false;
  const DirectVmdPlaybackState state =
      DirectVmdRuntime_PublicPlayback();
  return state == DirectVmdPlaybackState::Paused ||
         state == DirectVmdPlaybackState::Ended;
}

static uint64_t DirectVmdRuntime_PublicClipGeneration() {
  return s_directVmdClipGenerationPublic.load(std::memory_order_acquire);
}

static uint64_t DirectVmdRuntime_PublicTargetGeneration() {
  return s_directVmdTargetGeneration.load(std::memory_order_acquire);
}

static bool DirectVmdRuntime_CameraOverrideLoaded() {
  return s_directVmdCameraOverrideLoadedPublic.load(
      std::memory_order_acquire);
}

static bool DirectVmdRuntime_MorphOverrideLoaded() {
  return s_directVmdMorphOverrideLoadedPublic.load(
      std::memory_order_acquire);
}

static uintptr_t DirectVmdRuntime_PublicTargetOwner() {
  return s_directVmdTargetOwner.load(std::memory_order_acquire);
}

static void DirectVmdRuntime_ResetQpcWorker() {
  if (!s_directVmdWorker.frequency.QuadPart)
    QueryPerformanceFrequency(&s_directVmdWorker.frequency);
  QueryPerformanceCounter(&s_directVmdWorker.previousTick);
  s_directVmdWorker.qpcReady =
      s_directVmdWorker.frequency.QuadPart > 0;
}

static void DirectVmdRuntime_HandleClipCommandWorker() {
  const uint64_t revision =
      s_directVmdClipRequestRevision.load(std::memory_order_acquire);
  if (revision == s_directVmdWorker.handledClipRevision)
    return;
  s_directVmdWorker.handledClipRevision = revision;

  char path[sizeof(s_directVmdRequestedPath)] = {};
  AcquireSRWLockShared(&s_directVmdCommandLock);
  memcpy(path, s_directVmdRequestedPath, sizeof(path));
  ReleaseSRWLockShared(&s_directVmdCommandLock);
  path[sizeof(path) - 1] = '\0';

  s_directVmdWorker.resource.Reset();
  s_directVmdWorker.clock.Reset(0.0);
  s_directVmdLoadedPublic.store(false, std::memory_order_release);
  s_directVmdDurationPublic.store(0.0, std::memory_order_release);
  DirectVmdRuntime_ResetQpcWorker();

  if (path[0] == '\0') {
    Log("[P2-VMD-WORKER] unloaded clipGeneration=%llu tid=%lu",
        (unsigned long long)s_directVmdWorker.resource.generation,
        GetCurrentThreadId());
    s_directVmdClipGenerationPublic.store(
        s_directVmdWorker.resource.generation, std::memory_order_release);
    DirectVmdRuntime_PublishInvalidWorkerFrame();
    return;
  }

  const bool loaded = s_directVmdWorker.resource.Load(path);
  const VmdFile *clip = s_directVmdWorker.resource.Get();
  s_directVmdClipGenerationPublic.store(
      s_directVmdWorker.resource.generation, std::memory_order_release);
  if (!loaded || !clip) {
    Log("[P2-VMD-WORKER] load-failed path='%s' error='%s' "
        "clipGeneration=%llu tid=%lu",
        path, clip ? clip->error.c_str() : "no-resource",
        (unsigned long long)s_directVmdWorker.resource.generation,
        GetCurrentThreadId());
    DirectVmdRuntime_PublishInvalidWorkerFrame();
    return;
  }

  s_directVmdWorker.clock.Reset(static_cast<double>(clip->totalFrames));
  s_directVmdLoadedPublic.store(true, std::memory_order_release);
  s_directVmdDurationPublic.store(static_cast<double>(clip->totalFrames),
                                  std::memory_order_release);
  Log("[P2-VMD-WORKER] loaded path='%s' bones=%zu morphs=%zu cameras=%zu "
      "frames=%u morphPodLimit=%u morphDropped=%zu "
      "clipGeneration=%llu tid=%lu unityObjects=0",
      path, clip->boneTimelines.size(), clip->morphTimelines.size(),
      clip->cameraKeys.size(), clip->totalFrames,
      DIRECT_VMD_MAX_MORPH_CHANNELS,
      clip->morphTimelines.size() > DIRECT_VMD_MAX_MORPH_CHANNELS
          ? clip->morphTimelines.size() - DIRECT_VMD_MAX_MORPH_CHANNELS
          : 0,
      (unsigned long long)s_directVmdWorker.resource.generation,
      GetCurrentThreadId());
}

static void DirectVmdRuntime_HandleCameraOverrideCommandWorker() {
  const uint64_t revision =
      s_directVmdCameraOverrideRequestRevision.load(
          std::memory_order_acquire);
  if (revision == s_directVmdWorker.handledCameraOverrideRevision)
    return;
  s_directVmdWorker.handledCameraOverrideRevision = revision;

  char path[sizeof(s_directVmdRequestedCameraOverridePath)] = {};
  AcquireSRWLockShared(&s_directVmdCommandLock);
  memcpy(path, s_directVmdRequestedCameraOverridePath, sizeof(path));
  ReleaseSRWLockShared(&s_directVmdCommandLock);
  path[sizeof(path) - 1] = '\0';

  s_directVmdWorker.cameraOverrideResource.Reset();
  s_directVmdCameraOverrideLoadedPublic.store(false,
                                               std::memory_order_release);
  if (!path[0]) {
    Log("[P6-CAMERA-OVERRIDE] unloaded generation=%llu "
        "fallback=direct-vmd-camera tid=%lu",
        (unsigned long long)
            s_directVmdWorker.cameraOverrideResource.generation,
        GetCurrentThreadId());
    return;
  }

  const bool loaded =
      s_directVmdWorker.cameraOverrideResource.Load(path);
  const VmdFile *clip =
      s_directVmdWorker.cameraOverrideResource.Get();
  if (!loaded || !clip || clip->cameraKeys.empty()) {
    Log("[P6-CAMERA-OVERRIDE] load-failed path='%s' error='%s' "
        "cameraKeys=%zu generation=%llu "
        "fallback=direct-vmd-camera tid=%lu",
        path, clip ? clip->error.c_str() : "no-resource",
        clip ? clip->cameraKeys.size() : 0,
        (unsigned long long)
            s_directVmdWorker.cameraOverrideResource.generation,
        GetCurrentThreadId());
    s_directVmdWorker.cameraOverrideResource.Reset();
    return;
  }

  s_directVmdCameraOverrideLoadedPublic.store(true,
                                               std::memory_order_release);
  Log("[P6-CAMERA-OVERRIDE] loaded path='%s' cameraKeys=%zu "
      "frames=%u generation=%llu sharedClock=1 independentTime=0 tid=%lu",
      path, clip->cameraKeys.size(), clip->totalFrames,
      (unsigned long long)
          s_directVmdWorker.cameraOverrideResource.generation,
      GetCurrentThreadId());
}

static void DirectVmdRuntime_HandleMorphOverrideCommandWorker() {
  const uint64_t revision =
      s_directVmdMorphOverrideRequestRevision.load(
          std::memory_order_acquire);
  if (revision == s_directVmdWorker.handledMorphOverrideRevision)
    return;
  s_directVmdWorker.handledMorphOverrideRevision = revision;

  char path[sizeof(s_directVmdRequestedMorphOverridePath)] = {};
  AcquireSRWLockShared(&s_directVmdCommandLock);
  memcpy(path, s_directVmdRequestedMorphOverridePath, sizeof(path));
  ReleaseSRWLockShared(&s_directVmdCommandLock);
  path[sizeof(path) - 1] = '\0';

  s_directVmdWorker.morphOverrideResource.Reset();
  s_directVmdMorphOverrideLoadedPublic.store(false,
                                              std::memory_order_release);
  if (!path[0]) {
    Log("[P6-MORPH-OVERRIDE] unloaded generation=%llu "
        "fallback=direct-vmd-morph tid=%lu",
        (unsigned long long)
            s_directVmdWorker.morphOverrideResource.generation,
        GetCurrentThreadId());
    return;
  }

  const bool loaded = s_directVmdWorker.morphOverrideResource.Load(path);
  const VmdFile *clip = s_directVmdWorker.morphOverrideResource.Get();
  if (!loaded || !clip || clip->morphTimelines.empty()) {
    Log("[P6-MORPH-OVERRIDE] load-failed path='%s' error='%s' "
        "morphTracks=%zu generation=%llu "
        "fallback=direct-vmd-morph tid=%lu",
        path, clip ? clip->error.c_str() : "no-resource",
        clip ? clip->morphTimelines.size() : 0,
        (unsigned long long)
            s_directVmdWorker.morphOverrideResource.generation,
        GetCurrentThreadId());
    s_directVmdWorker.morphOverrideResource.Reset();
    return;
  }

  s_directVmdMorphOverrideLoadedPublic.store(true,
                                              std::memory_order_release);
  Log("[P6-MORPH-OVERRIDE] loaded path='%s' morphTracks=%zu "
      "frames=%u generation=%llu sharedClock=1 independentTime=0 tid=%lu",
      path, clip->morphTimelines.size(), clip->totalFrames,
      (unsigned long long)
          s_directVmdWorker.morphOverrideResource.generation,
      GetCurrentThreadId());
}

static void DirectVmdRuntime_HandlePlaybackCommandWorker() {
  const uint64_t revision =
      s_directVmdPlaybackRequestRevision.load(std::memory_order_acquire);
  if (revision == s_directVmdWorker.handledPlaybackRevision)
    return;
  s_directVmdWorker.handledPlaybackRevision = revision;
  const DirectVmdRuntimeCommand command =
      static_cast<DirectVmdRuntimeCommand>(
          s_directVmdPlaybackRequest.load(std::memory_order_acquire));
  switch (command) {
  case DirectVmdRuntimeCommand::Playing:
    if (s_directVmdRuntimeActive.load(std::memory_order_acquire) &&
        s_directVmdWorker.resource.IsLoaded()) {
      s_directVmdWorker.clock.Play();
      s_directVmdWorker.pausedForInactive = false;
    }
    break;
  case DirectVmdRuntimeCommand::Paused:
    s_directVmdWorker.clock.Pause();
    s_directVmdWorker.pausedForInactive = false;
    break;
  default:
    s_directVmdWorker.clock.Stop();
    s_directVmdWorker.pausedForInactive = false;
    break;
  }
  DirectVmdRuntime_ResetQpcWorker();
  Log("[P2-VMD-CLOCK] command=%u state=%u frame=%.6f cycle=%llu "
      "active=%d clipGeneration=%llu tid=%lu",
      static_cast<unsigned>(command),
      static_cast<unsigned>(s_directVmdWorker.clock.state),
      s_directVmdWorker.clock.frame,
      (unsigned long long)s_directVmdWorker.clock.loopCycle,
      s_directVmdRuntimeActive.load(std::memory_order_acquire) ? 1 : 0,
      (unsigned long long)s_directVmdWorker.resource.generation,
      GetCurrentThreadId());
}

static void DirectVmdRuntime_HandleSeekWorker() {
  const uint64_t revision =
      s_directVmdSeekRequestRevision.load(std::memory_order_acquire);
  if (revision == s_directVmdWorker.handledSeekRevision)
    return;
  s_directVmdWorker.handledSeekRevision = revision;
  double frame = 0.0;
  AcquireSRWLockShared(&s_directVmdCommandLock);
  frame = s_directVmdRequestedSeekFrame;
  ReleaseSRWLockShared(&s_directVmdCommandLock);
  s_directVmdWorker.clock.Seek(frame);
  DirectVmdRuntime_ResetQpcWorker();
  Log("[P2-VMD-CLOCK] seek requested=%.6f applied=%.6f cycle=%llu "
      "tid=%lu",
      frame, s_directVmdWorker.clock.frame,
      (unsigned long long)s_directVmdWorker.clock.loopCycle,
      GetCurrentThreadId());
}

static void DirectVmdRuntime_WorkerTick() {
  AcquireSRWLockShared(&s_directVmdLifecycleLock);
  DirectVmdRuntime_HandleClipCommandWorker();
  DirectVmdRuntime_HandleCameraOverrideCommandWorker();
  DirectVmdRuntime_HandleMorphOverrideCommandWorker();
  DirectVmdRuntime_HandlePlaybackCommandWorker();
  DirectVmdRuntime_HandleSeekWorker();

  const bool active =
      s_directVmdRuntimeActive.load(std::memory_order_acquire);
  if (active != s_directVmdWorker.previousActive) {
    if (!active &&
        s_directVmdWorker.clock.state == DirectVmdPlaybackState::Playing) {
      s_directVmdWorker.clock.Pause();
      s_directVmdWorker.pausedForInactive = true;
    } else if (active && s_directVmdWorker.pausedForInactive &&
               s_directVmdWorker.resource.IsLoaded() &&
               static_cast<DirectVmdRuntimeCommand>(
                   s_directVmdPlaybackRequest.load(
                       std::memory_order_acquire)) ==
                   DirectVmdRuntimeCommand::Playing) {
      s_directVmdWorker.clock.Play();
      s_directVmdWorker.pausedForInactive = false;
    }
    s_directVmdWorker.previousActive = active;
    DirectVmdRuntime_ResetQpcWorker();
  }

  s_directVmdWorker.clock.loop =
      s_directVmdLoop.load(std::memory_order_acquire);
  s_directVmdWorker.clock.speed =
      s_directVmdSpeed.load(std::memory_order_acquire);

  LARGE_INTEGER now = {};
  QueryPerformanceCounter(&now);
  if (!s_directVmdWorker.qpcReady) {
    DirectVmdRuntime_ResetQpcWorker();
  } else {
    double elapsed = static_cast<double>(now.QuadPart -
                                         s_directVmdWorker.previousTick.QuadPart) /
                     static_cast<double>(s_directVmdWorker.frequency.QuadPart);
    s_directVmdWorker.previousTick = now;
    elapsed = (std::max)(0.0, (std::min)(elapsed, 0.25));
    if (active)
      s_directVmdWorker.clock.AdvanceSeconds(elapsed);
  }

  const VmdFile *clip = s_directVmdWorker.resource.Get();
  if (!clip || !clip->loaded) {
    s_directVmdPlaybackPublic.store(
        static_cast<uint32_t>(s_directVmdWorker.clock.state),
        std::memory_order_release);
    ReleaseSRWLockShared(&s_directVmdLifecycleLock);
    return;
  }

  DirectVmdSampleFrame frame;
  DirectVmdSampleClip(
      *clip, s_directVmdWorker.clock.frame, ++s_directVmdWorker.sequence,
      s_directVmdTargetGeneration.load(std::memory_order_acquire),
      s_directVmdWorker.resource.generation,
      s_directVmdTargetOwner.load(std::memory_order_acquire),
      s_directVmdWorker.clock.state, &frame);
  const VmdFile *cameraOverride =
      s_directVmdWorker.cameraOverrideResource.Get();
  if (cameraOverride && cameraOverride->loaded &&
      !cameraOverride->cameraKeys.empty()) {
    DirectVmdCameraSamplePod overrideSample;
    if (DirectVmdSampleCameraChannel(
            *cameraOverride, s_directVmdWorker.clock.frame, true,
            &overrideSample))
      frame.camera = overrideSample;
  }
  const VmdFile *morphOverride =
      s_directVmdWorker.morphOverrideResource.Get();
  if (morphOverride && morphOverride->loaded &&
      !morphOverride->morphTimelines.empty()) {
    DirectVmdSampleMorphChannels(*morphOverride,
                                 s_directVmdWorker.clock.frame, &frame);
  }
  frame.playbackCycle = s_directVmdWorker.clock.loopCycle;
  frame.seekRevision = s_directVmdWorker.handledSeekRevision;
  DirectVmdRuntime_PublishFrame(frame);
  ReleaseSRWLockShared(&s_directVmdLifecycleLock);
}

static void DirectVmdRuntime_WorkerShutdown() {
  s_directVmdWorker.resource.Reset();
  s_directVmdWorker.cameraOverrideResource.Reset();
  s_directVmdWorker.morphOverrideResource.Reset();
  s_directVmdWorker.clock.Stop();
  s_directVmdLoadedPublic.store(false, std::memory_order_release);
  s_directVmdCameraOverrideLoadedPublic.store(false,
                                               std::memory_order_release);
  s_directVmdMorphOverrideLoadedPublic.store(false,
                                              std::memory_order_release);
  DirectVmdRuntime_PublishInvalidWorkerFrame();
  Log("[P2-VMD-WORKER] shutdown tid=%lu", GetCurrentThreadId());
}

static void DirectVmdRuntime_RequestProcessDetach() {
  s_directVmdRuntimeActive.store(false, std::memory_order_release);
  s_directVmdPlaybackRequest.store(
      static_cast<uint32_t>(DirectVmdRuntimeCommand::Stopped),
      std::memory_order_release);
  s_directVmdTargetGeneration.fetch_add(1, std::memory_order_acq_rel);
}
