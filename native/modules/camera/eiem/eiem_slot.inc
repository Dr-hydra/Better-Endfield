#if defined(__ANDROID__)
#include "compat/android_slot.inc"
#else
// One EIEM DirectVmd instance ("slot"); see eiem_slot.h. Compiled once per
// slot by eiem_slot<N>.cpp with BE_EIEM_SLOT defined, each inside its own
// namespace so every slot has private copies of EIEM's file-level state.
//
// Portions copyright Sasye and EIEM contributors, https://github.com/Sasye/EIEM
// (AGPL-3.0, upstream commit 1bc9baa; see LICENSE.EIEM and UPSTREAM.md).
// The upstream/ headers are compiled in EIEM's own include order; this file
// replaces EIEM's init.h/trojan.h/gui.h with the parts DirectVmd needs:
//   - IL2CPP resolution by metadata name (no RVA or field-offset fallbacks),
//   - backend enter/leave and the FinalIK / SkeletalMorphCore callbacks
//     (hooks are installed once by eiem_body.cpp and forwarded here),
//   - a sampling worker that follows the Camera module's MMD director clock.
// Removed: MUS4 muscle mode, EIEM GUI/audio/camera, window subclassing,
// update checks and the clothing enhancement. EIEM's base cloth playback
// service is kept (upstream/cloth.h; modes and stubs in compat/cloth_be.h).
#ifndef BE_EIEM_SLOT
#error "Define BE_EIEM_SLOT (0..3) before including eiem_slot.inc"
#endif
#define _CRT_SECURE_NO_WARNINGS
// Every system/standard header EIEM uses is included here, outside the slot
// namespace; the includes inside upstream/ then resolve to no-ops.
#include <windows.h>
#include <bcrypt.h>
#include <commdlg.h>
#include <mmsystem.h>
#include <algorithm>
#include <array>
#include <atomic>
#include <climits>
#include <cmath>
#include <cstdarg>
#include <cstddef>
#include <cstdint>
#include <cstdio>
#include <cstring>
#include <filesystem>
#include <functional>
#include <intrin.h>
#include <iomanip>
#include <limits>
#include <locale>
#include <map>
#include <memory>
#include <new>
#include <set>
#include <sstream>
#include <stdexcept>
#include <string>
#include <type_traits>
#include <utility>
#include <vector>
#include "MinHook.h"

#include "eiem_slot.h"

#define BE_EIEM_CAT2(a, b) a##b
#define BE_EIEM_CAT(a, b) BE_EIEM_CAT2(a, b)
#define BE_EIEM_NS BE_EIEM_CAT(eiem_slot_, BE_EIEM_SLOT)
#define BE_EIEM_API BE_EIEM_CAT(Slot, BE_EIEM_SLOT)

namespace BE_EIEM_NS {
// Read by upstream/cloth.h (BE-PATCH) and compat/cloth_be.h.
static std::atomic<DWORD> g_beMainThreadId{0};
static std::atomic<int> g_beClothMode{1}; // EiemBody::ClothMode
#include "vmd_parser.h"
#include "direct_vmd_pose.h"
#include "motion_backend.h"
#include "mmd_player.h"
#include "bone_anim_player.h"
#include "bone_map.h"
#include "il2cpp_api.h"
#include "muscle_player.h"
#include "camera_player.h"
#include "globals.h"
#include "direct_vmd_runtime.h"
#include "smc_face.h"

// eiem.cpp prelude (1bc9baa).
static int32_t UnboxInt(void *boxed) {
  __try {
    return boxed ? *(int32_t *)((char *)boxed + 16) : 0;
  } __except (1) {
    return 0;
  }
}
static bool UnboxBool(void *boxed) {
  __try {
    return boxed ? *(bool *)((char *)boxed + 16) : false;
  } __except (1) {
    return false;
  }
}

#include "animation.h"
#include "ghost_rig.h"

// ===========================================================================
// Extracted from EIEM trojan.h (1bc9baa): DirectVmd paths only.
// ===========================================================================
static void *s_origMoveTick = nullptr;
static void *s_cachedMovementComp = nullptr;
static void *s_cachedEntity = nullptr;
// BE: ComputeFloorDist writes a FindFloorResult whose layout EIEM hardcodes;
// the layout is verified from metadata before the native call is allowed.
static bool g_beFloorResultLayoutOk = false;

static void *GhostRig_GetOwnerGrounderBipedIK(
    uintptr_t ownerCharacter, void *bipedIK) {
  if (!GhostRig_RequireMainThread("GrounderBipedIK.ResolveOwner", false) ||
      !g_finalIkGrounderLayoutOk || !ownerCharacter ||
      reinterpret_cast<uintptr_t>(g_mainCharEntity) != ownerCharacter ||
      reinterpret_cast<uintptr_t>(s_cachedEntity) != ownerCharacter ||
      !s_cachedMovementComp || g_offMovementGrounder < 0)
    return nullptr;
  void *grounder = nullptr;
  void *linkedBiped = nullptr;
  __try {
    grounder = *reinterpret_cast<void **>(
        (char *)s_cachedMovementComp + g_offMovementGrounder);
    if (!grounder || reinterpret_cast<uintptr_t>(grounder) < 0x10000)
      return nullptr;
    void *klass = il2cpp_object_get_class
        ? il2cpp_object_get_class(grounder)
        : nullptr;
    const char *name = klass && il2cpp_class_get_name
        ? il2cpp_class_get_name(klass)
        : nullptr;
    const char *nameSpace = klass && il2cpp_class_get_namespace
        ? il2cpp_class_get_namespace(klass)
        : nullptr;
    if (!name || !nameSpace || strcmp(name, "GrounderBipedIK") != 0 ||
        strcmp(nameSpace, "RootMotion.FinalIK") != 0)
      return nullptr;
    linkedBiped = *reinterpret_cast<void **>(
        (char *)grounder + OFF_GROUNDER_BIPED_IK);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return nullptr;
  }
  return (!bipedIK || linkedBiped == bipedIK) ? grounder : nullptr;
}

typedef void (__fastcall *FindFloorNative_t)(
  void *self, float *position, void *outResult, float stepDown, void *methodInfo);
static FindFloorNative_t s_findFloorNativeFn = nullptr;

typedef void (__fastcall *ComputeFloorDistNative_t)(
  void *self, float *position, float sweepDistance, void *outResult,
  float sweepRadius, bool forceWalkable, float stepDownOffset,
  bool checkClimbable, void *methodInfo);
static ComputeFloorDistNative_t s_computeFloorDistNativeFn = nullptr;

static bool GhostRig_QueryFindFloorSample(
    uintptr_t ownerCharacter, VmdVec3 queryPosition, float stepDown,
    DirectVmdTerrainProbeHit *sample) {
  static int s_lastUnavailableFrame = INT_MIN;
  static int s_lastRejectedFrame = INT_MIN;
  static bool s_layoutLogged = false;
  static bool s_sehLogged = false;
  if (!sample)
    return false;
  *sample = DirectVmdTerrainProbeHit();
  if (!GhostRig_RequireMainThread("FindFloor.DirectVmd", false))
    return false;
  const bool ownerMatches = ownerCharacter &&
      reinterpret_cast<uintptr_t>(g_mainCharEntity) == ownerCharacter &&
      reinterpret_cast<uintptr_t>(s_cachedEntity) == ownerCharacter;
  if (!ownerMatches || !s_cachedMovementComp ||
      !g_computeFloorDistMethod || !g_beFloorResultLayoutOk) {
    const int frame = GhostRig_GetFrameCount();
    if (s_lastUnavailableFrame == INT_MIN || frame < 0 ||
        frame - s_lastUnavailableFrame >= 120) {
      s_lastUnavailableFrame = frame;
      Log("[P7-FINDFLOOR-API] available=0 unityFrame=%d method=%p "
          "layoutOk=%d movement=%p cachedEntity=%p mainEntity=%p owner=%p "
          "action=hold-stable-plane-then-flat-air-target tid=%lu",
          frame, g_computeFloorDistMethod, g_beFloorResultLayoutOk ? 1 : 0,
          s_cachedMovementComp, s_cachedEntity, g_mainCharEntity,
          reinterpret_cast<void *>(ownerCharacter),
          GetCurrentThreadId());
    }
    return false;
  }
  if (!DirectVmdFinite(queryPosition.x) ||
      !DirectVmdFinite(queryPosition.y) ||
      !DirectVmdFinite(queryPosition.z) ||
      !DirectVmdFinite(stepDown) || stepDown <= 0.0f)
    return false;

  if (!s_computeFloorDistNativeFn) {
    __try {
      s_computeFloorDistNativeFn =
          reinterpret_cast<ComputeFloorDistNative_t>(
              reinterpret_cast<MInfo *>(g_computeFloorDistMethod)->mp);
    } __except (EXCEPTION_EXECUTE_HANDLER) {
      s_computeFloorDistNativeFn = nullptr;
    }
  }
  if (!s_computeFloorDistNativeFn)
    return false;
  static constexpr float kDirectVmdSweepRadius = 0.010f;
  if (!s_layoutLogged) {
    s_layoutLogged = true;
    Log("[P7-FINDFLOOR-API] available=1 native=%p method=%p "
        "sampler=ComputeFloorDist sweepRadius=%.3f "
        "resultLayout=hit+0x00,walkable+0x01,dist+0x08,"
        "point+0x10,normal+0x1C mainThreadOnly=1 tid=%lu",
        s_computeFloorDistNativeFn, g_computeFloorDistMethod,
        kDirectVmdSweepRadius, GetCurrentThreadId());
  }

  float position[3] = {
      queryPosition.x, queryPosition.y, queryPosition.z};
  alignas(16) unsigned char floorResult[0xB0] = {};
  __try {
    s_computeFloorDistNativeFn(
        s_cachedMovementComp, position, stepDown, floorResult,
        kDirectVmdSweepRadius, false, stepDown, false,
        g_computeFloorDistMethod);
    const bool hit = *reinterpret_cast<bool *>(floorResult + 0x00);
    const bool walkable = *reinterpret_cast<bool *>(floorResult + 0x01);
    if (!hit || !walkable)
      return false;
    const float floorDistance =
        *reinterpret_cast<const float *>(floorResult + 0x08);
    const float *point =
        reinterpret_cast<const float *>(floorResult + 0x10);
    const float *normal =
        reinterpret_cast<const float *>(floorResult + 0x1C);
    sample->query = queryPosition;
    sample->point = {point[0], point[1], point[2]};
    sample->normal = {normal[0], normal[1], normal[2]};
    sample->floorDistance = floorDistance;
    if (!DirectVmdFinite(sample->point.x) ||
        !DirectVmdFinite(sample->point.y) ||
        !DirectVmdFinite(sample->point.z) ||
        !DirectVmdFinite(sample->normal.x) ||
        !DirectVmdFinite(sample->normal.y) ||
        !DirectVmdFinite(sample->normal.z) ||
        !DirectVmdFinite(sample->floorDistance) ||
        sample->floorDistance < 0.0f) {
      *sample = DirectVmdTerrainProbeHit();
      return false;
    }
    const float dx = sample->point.x - queryPosition.x;
    const float dz = sample->point.z - queryPosition.z;
    const float horizontalError = std::sqrt(dx * dx + dz * dz);
    static constexpr float kMaximumHorizontalError = 0.035f;
    if (!DirectVmdFinite(horizontalError) ||
        horizontalError > kMaximumHorizontalError) {
      const int frame = GhostRig_GetFrameCount();
      if (s_lastRejectedFrame == INT_MIN || frame < 0 ||
          frame - s_lastRejectedFrame >= 120) {
        s_lastRejectedFrame = frame;
        Log("[P7-FINDFLOOR-REJECT] unityFrame=%d reason=xz-error "
            "error=%.6f limit=%.6f query=(%.6f,%.6f,%.6f) "
            "point=(%.6f,%.6f,%.6f) floorDist=%.6f "
            "sampler=ComputeFloorDist tid=%lu",
            frame, horizontalError, kMaximumHorizontalError,
            queryPosition.x, queryPosition.y, queryPosition.z,
            sample->point.x, sample->point.y, sample->point.z,
            sample->floorDistance, GetCurrentThreadId());
      }
      *sample = DirectVmdTerrainProbeHit();
      return false;
    }
    sample->hit = 1;
    return true;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    *sample = DirectVmdTerrainProbeHit();
    if (!s_sehLogged) {
      s_sehLogged = true;
      Log("[P7-FINDFLOOR-API] SEH owner=%p movement=%p method=%p "
          "terrainSampleRejected=1 tid=%lu",
          reinterpret_cast<void *>(ownerCharacter),
          s_cachedMovementComp, g_computeFloorDistMethod,
          GetCurrentThreadId());
    }
    return false;
  }
}
// EIEM's own audio/camera followers are replaced by the Camera module's
// director, which owns music (Music module) and the VMD camera.
static void DirectVmdAudio_WorkerSync(DirectVmdClock *,
                                      const DirectVmdAudioWorkerTick &) {}
static void DirectVmdCamera_UpdateMainThread(int) {}
static void DirectVmdCamera_ResetMainThread(const char *) {}
static void DirectVmdAudio_ResetMainThread(const char *, bool) {}

static bool MotionBackend_RequireCommandThread(const char *stage) {
  return GhostRig_RequireMainThread(stage, true);
}

static bool MotionBackend_PublishMainThread(MotionBackend next,
                                            const char *reason) {
  if (!MotionBackend_RequireCommandThread("MotionBackend.Transition"))
    return false;
  const MotionBackend previous = g_motionBackend.Current();
  const bool changed = g_motionBackend.TransitionTo(next);
  if (changed) ClothRelease(reason);
  Log("[P3-BACKEND] frame=%d seq=%llu tid=%lu old=%s new=%s "
      "changed=%d backendGeneration=%llu ghostGeneration=%llu reason=%s",
      GhostRig_GetFrameCount(),
      (unsigned long long)GhostRig_NextOrderSequence(),
      GetCurrentThreadId(), MotionBackendName(previous),
      MotionBackendName(next), changed ? 1 : 0,
      (unsigned long long)g_motionBackend.Generation(),
      (unsigned long long)s_ghostRequestedGeneration.load(
          std::memory_order_acquire),
      reason ? reason : "unspecified");
  return true;
}

static void MotionBackend_DisableDirectMainThread(
    GhostRigCleanupReason cleanupReason, const char *reason) {
  ClothRelease(reason ? reason : "direct-release");
  DirectVmdRuntime_RequestStop();
  DirectVmdMorph_ResetMainThread(reason ? reason : "direct-release");
  if (GhostRig_IsRequestedEnabled() || GhostRig_IsAliveForGui())
    GhostRig_RequestEnabled(false, cleanupReason);
  else
    DirectVmdRuntime_SetActive(false);
  GhostRig_TryImmediateCleanupOnCurrentThread(cleanupReason);
  Log("[P3-DIRECT-RELEASE] backend=%s reason=%s ghostAlive=%d "
      "generation=%llu targetHandlesRetained=0 tid=%lu",
      MotionBackendName(g_motionBackend.Current()),
      reason ? reason : "unspecified",
      GhostRig_IsAliveForGui() ? 1 : 0,
      (unsigned long long)s_ghostRequestedGeneration.load(
          std::memory_order_acquire),
      GetCurrentThreadId());
}

static void MotionBackend_EnterNativeMainThread(
    GhostRigCleanupReason cleanupReason, const char *reason) {
  if (!MotionBackend_RequireCommandThread("MotionBackend.EnterNative"))
    return;
  g_clothPlaybackGate.Cancel();
  const MotionBackend previous = g_motionBackend.Current();
  if (!MotionBackend_PublishMainThread(MotionBackend::Native, reason))
    return;
  if (previous == MotionBackend::DirectVmd ||
      GhostRig_IsRequestedEnabled() || GhostRig_IsAliveForGui()) {
    MotionBackend_DisableDirectMainThread(cleanupReason, reason);
  }
  ClothRelease(reason);
  SafeSetAnimatorEnabled(true);
  if (previous == MotionBackend::DirectVmd) {
    Log("[P4-NATIVE-ANIMATOR] enabled=1 route=native-restore "
        "reason=%s entity=%p animator=%p tid=%lu",
        reason ? reason : "unspecified", g_mainCharEntity,
        g_cachedAnimator, GetCurrentThreadId());
  }
}

static bool MotionBackend_EnterDirectMainThread(bool play,
                                                const char *reason) {
  if (!g_pluginActive || !DirectVmdRuntime_IsLoaded()) {
    Log("[P3-DIRECT-ENTER] rejected pluginActive=%d loaded=%d reason=%s",
        g_pluginActive ? 1 : 0, DirectVmdRuntime_IsLoaded() ? 1 : 0,
        reason ? reason : "unspecified");
    return false;
  }
  if (!MotionBackend_RequireCommandThread("MotionBackend.EnterDirect"))
    return false;
  if (!g_mainCharEntity || !g_cachedAnimator) {
    Log("[P3-DIRECT-ENTER] rejected reason=no-owner entity=%p animator=%p",
        g_mainCharEntity, g_cachedAnimator);
    return false;
  }
  if (g_motionBackend.Is(MotionBackend::DirectVmd) &&
      GhostRig_IsRequestedEnabled()) {
    SafeSetAnimatorEnabled(false);
    if (play)
      DirectVmdRuntime_RequestPlay();
    return true;
  }
  if (!g_motionBackend.Is(MotionBackend::DirectVmd) &&
      !MotionBackend_PublishMainThread(MotionBackend::DirectVmd, reason))
    return false;
  GhostRig_RequestOwnerChange(g_mainCharEntity);
  ClothBegin(MotionBackend::DirectVmd, play);
  SafeSetAnimatorEnabled(false);
  GhostRig_RequestEnabled(true, GhostRigCleanupReason::None);
  DirectVmdRuntime_SetSpeed(1.0f);
  DirectVmdRuntime_SetLoop(false);
  if (play)
    DirectVmdRuntime_RequestPlay();
  Log("[P4-DIRECT-ENTER] accepted play=%d entity=%p animator=%p "
      "backendGeneration=%llu ghostGeneration=%llu clock=BetterEndfield.MmdDirector "
      "camera=BetterEndfield audio=BetterEndfield.Music tid=%lu",
      play ? 1 : 0, g_mainCharEntity, g_cachedAnimator,
      (unsigned long long)g_motionBackend.Generation(),
      (unsigned long long)s_ghostRequestedGeneration.load(
          std::memory_order_acquire),
      GetCurrentThreadId());
  return true;
}

// ===========================================================================
// Better Endfield slot driver
// ===========================================================================
namespace {
using namespace BetterEndfield;

const BE_HostApiV1 *g_beHost = nullptr;
constexpr char kBeModuleId[] = "betterendfield.camera";
bool g_beInitialized = false;
bool g_beAvailable = false;
bool g_beFaceReady = false;
EiemBody::Options g_beOptions;
std::string g_beLoadedMotion;

void BeHostLog(const std::string &message) {
  if (g_beHost && g_beHost->log)
    g_beHost->log(g_beHost->context, kBeModuleId,
                  ("EIEM body[" + std::to_string(BE_EIEM_SLOT + 1) + "]: " + message).c_str());
}

// --- director clock ---------------------------------------------------------
std::atomic<double> g_clockSeconds{0.0};
std::atomic<int64_t> g_clockStamp{0};
std::atomic<bool> g_clockPlaying{false};
LARGE_INTEGER g_qpcFrequency{};

int64_t Qpc() {
  LARGE_INTEGER now{};
  QueryPerformanceCounter(&now);
  return now.QuadPart;
}

double DirectorSecondsNow(bool &playing) {
  playing = g_clockPlaying.load(std::memory_order_acquire);
  double seconds = g_clockSeconds.load(std::memory_order_acquire);
  if (playing && g_qpcFrequency.QuadPart > 0) {
    const double elapsed = double(Qpc() - g_clockStamp.load(std::memory_order_acquire)) /
                           double(g_qpcFrequency.QuadPart);
    seconds += std::clamp(elapsed, 0.0, 0.1);
  }
  return seconds;
}

// --- worker -------------------------------------------------------------------
std::atomic<bool> g_workerRun{false};
std::atomic<uint64_t> g_workerHandledClipRevision{0};
uint64_t g_beRequestedClipRevision = 0;
HANDLE g_worker = nullptr;
double g_workerLastTarget = -1.0;
int64_t g_workerLastQpc = 0;
bool g_workerLastPlaying = false;

// DirectVmdRuntime_WorkerTick (1bc9baa) with the free-running QPC clock
// replaced by the director's timeline. Seeks and loop wraps on the timeline
// are forwarded as EIEM seek requests / loop cycles so its root, terrain and
// cadence state resets exactly like EIEM's own transport.
void WorkerTick() {
  DirectVmdSource_HandleCommandWorker();
  AcquireSRWLockShared(&s_directVmdLifecycleLock);
  DirectVmdRuntime_HandleClipCommandWorker();
  DirectVmdRuntime_HandleCameraOverrideCommandWorker();
  DirectVmdRuntime_HandleMorphOverrideCommandWorker();
  DirectVmdRuntime_HandlePlaybackCommandWorker();
  DirectVmdRuntime_HandleSeekWorker();

  auto &clock = s_directVmdWorker.clock;
  const bool active = s_directVmdRuntimeActive.load(std::memory_order_acquire);
  s_directVmdWorker.previousActive = active;
  clock.loop = false;
  clock.speed = 1.0;

  const VmdFile *clip = s_directVmdWorker.resource.Get();
  if (!clip || !clip->loaded || !s_directVmdSourceClipReady) {
    s_directVmdPlaybackPublic.store(static_cast<uint32_t>(clock.state),
                                    std::memory_order_release);
    ReleaseSRWLockShared(&s_directVmdLifecycleLock);
    return;
  }

  if (active && clock.state != DirectVmdPlaybackState::Stopped) {
    bool playing = false;
    const double seconds = DirectorSecondsNow(playing);
    const double target =
        std::clamp(seconds * kVmdFramesPerSecond, 0.0, clock.durationFrames);
    const int64_t now = Qpc();
    if (g_workerLastTarget >= 0.0) {
      const double elapsed = g_qpcFrequency.QuadPart > 0
          ? double(now - g_workerLastQpc) / double(g_qpcFrequency.QuadPart)
          : 0.0;
      const double expected = g_workerLastTarget +
          (g_workerLastPlaying ? elapsed * kVmdFramesPerSecond : 0.0);
      const bool wrapped = playing && target + 15.0 < g_workerLastTarget &&
          g_workerLastTarget > clock.durationFrames * 0.5 &&
          target < clock.durationFrames * 0.5;
      if (wrapped) {
        ++clock.loopCycle;
      } else if (std::fabs(target - expected) > 3.0) {
        // A timeline jump is published like an EIEM seek so the ghost rig
        // re-anchors root/terrain instead of blending across the jump.
        clock.Seek(target);
        s_directVmdWorker.handledSeekRevision =
            s_directVmdSeekRequestRevision.fetch_add(1, std::memory_order_acq_rel) + 1;
      }
    }
    g_workerLastTarget = target;
    g_workerLastQpc = now;
    g_workerLastPlaying = playing;
    clock.frame = target;
    clock.state = playing ? DirectVmdPlaybackState::Playing
                          : (target >= clock.durationFrames && clock.durationFrames > 0.0
                                 ? DirectVmdPlaybackState::Ended
                                 : DirectVmdPlaybackState::Paused);
  } else {
    g_workerLastTarget = -1.0;
  }

  DirectVmdSampleFrame frame;
  DirectVmdSampleClip(
      *clip, clock.frame, ++s_directVmdWorker.sequence,
      s_directVmdTargetGeneration.load(std::memory_order_acquire),
      s_directVmdWorker.resource.generation,
      s_directVmdTargetOwner.load(std::memory_order_acquire),
      clock.state, &frame, false);
  frame.backendGeneration = g_motionBackend.Generation();
  try {
    eiem_source::Sample(s_directVmdSourcePrepared, s_directVmdSourceClip,
                        *clip, clock.frame, &frame);
  } catch (const std::exception &e) {
    Log("[SOURCE-EVALUATE] rejected: %s", e.what());
    s_directVmdSourceClipReady = false;
    s_directVmdLoadedPublic.store(false, std::memory_order_release);
    clock.Stop();
    DirectVmdRuntime_PublishInvalidWorkerFrame();
    ReleaseSRWLockShared(&s_directVmdLifecycleLock);
    return;
  }
  const VmdFile *morphOverride = s_directVmdWorker.morphOverrideResource.Get();
  if (morphOverride && morphOverride->loaded &&
      !morphOverride->morphTimelines.empty()) {
    DirectVmdSampleMorphChannels(*morphOverride, clock.frame, &frame);
  }
  frame.playbackCycle = clock.loopCycle;
  frame.seekRevision = s_directVmdWorker.handledSeekRevision;
  DirectVmdRuntime_PublishFrame(frame);
  ReleaseSRWLockShared(&s_directVmdLifecycleLock);
}

DWORD WINAPI WorkerMain(LPVOID) {
  bool timerActive = false;
  while (g_workerRun.load(std::memory_order_acquire)) {
    WorkerTick();
    // WorkerTick loads synchronously, so once it returns the handled request
    // is complete (loaded or failed).
    g_workerHandledClipRevision.store(s_directVmdWorker.handledClipRevision,
                                      std::memory_order_release);
    const bool playing = DirectVmdRuntime_WantsRealtimeWorkerCadence();
    const bool holding = DirectVmdRuntime_WantsResponsiveWorkerCadence();
    if (playing && !timerActive) timerActive = timeBeginPeriod(1) == TIMERR_NOERROR;
    else if (!playing && timerActive) {timeEndPeriod(1); timerActive = false;}
    Sleep(playing ? 1 : holding ? 2 : 20);
  }
  if (timerActive) timeEndPeriod(1);
  DirectVmdRuntime_WorkerShutdown();
  return 0;
}

void StartWorker() {
  if (g_worker) return;
  g_workerRun.store(true, std::memory_order_release);
  g_worker = CreateThread(nullptr, 0, WorkerMain, nullptr, 0, nullptr);
  if (!g_worker) g_workerRun.store(false, std::memory_order_release);
}

// --- resolution -----------------------------------------------------------------
const char *TypeName(void *field) {
  void *type = field && il2cpp_field_get_type ? il2cpp_field_get_type(field) : nullptr;
  return type && il2cpp_type_get_name ? il2cpp_type_get_name(type) : nullptr;
}

// FindFloorResult as used by EIEM: bool@0, bool@1, float@8, Vector3@0x10,
// Vector3@0x1C (unboxed). Accept the native call only when metadata agrees.
bool VerifyFloorResultLayout(void *method) {
  if (!method || !il2cpp_method_get_param || !il2cpp_class_from_type ||
      il2cpp_method_get_param_count(method) != 7)
    return false;
  void *type = il2cpp_method_get_param(method, 2);
  void *klass = type ? il2cpp_class_from_type(type) : nullptr;
  if (!klass) return false;
  if (il2cpp_class_value_size) {
    uint32_t alignment = 0;
    const int32_t size = il2cpp_class_value_size(klass, &alignment);
    if (size <= 0x28 || size > 0xB0) return false;
  }
  struct Expect { int offset; const char *type; bool seen; } expect[] = {
      {0x00, "System.Boolean", false}, {0x01, "System.Boolean", false},
      {0x08, "System.Single", false}, {0x10, "UnityEngine.Vector3", false},
      {0x1C, "UnityEngine.Vector3", false}};
  void *it = nullptr;
  while (void *field = il2cpp_class_get_fields(klass, &it)) {
    if (il2cpp_field_get_flags && (il2cpp_field_get_flags(field) & 0x10)) continue;
    const int offset = static_cast<int>(il2cpp_field_get_offset(field)) - 16;
    const char *name = TypeName(field);
    for (auto &e : expect)
      if (e.offset == offset && name && !strcmp(name, e.type)) e.seen = true;
  }
  for (const auto &e : expect)
    if (!e.seen) return false;
  return true;
}

void *CodeOf(void *method) {
  if (!method) return nullptr;
  __try {
    return reinterpret_cast<MInfo *>(method)->mp;
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return nullptr;
  }
}

void **Assemblies(size_t &count) {
  count = 0;
  void *domain = il2cpp_domain_get ? il2cpp_domain_get() : nullptr;
  return domain ? il2cpp_domain_get_assemblies(domain, &count) : nullptr;
}

bool ResolveAll(EiemSlot::HookTargets *targets) {
  if (!Resolve()) {
    BeHostLog("IL2CPP exports unavailable");
    return false;
  }
  size_t ac = 0;
  void **asms = Assemblies(ac);
  if (!asms || !ac) {
    BeHostLog("IL2CPP domain has no assemblies yet");
    return false;
  }
  // EIEM defaults assume one client build; unresolved means unavailable.
  g_offMovementGrounder = -1;
  g_offCurrentFloor = -1;
  g_offBaseCompEntity = -1;

  g_transformClass = FindClass("UnityEngine", "Transform", asms, ac);
  if (g_transformClass) {
    g_transform_get_localRotation = FindMethod(g_transformClass, "get_localRotation", 0);
    g_transform_set_localRotation = FindMethod(g_transformClass, "set_localRotation", 1);
    g_transform_get_localPosition = FindMethod(g_transformClass, "get_localPosition", 0);
    g_transform_set_localPosition = FindMethod(g_transformClass, "set_localPosition", 1);
    g_transform_get_childCount = FindMethod(g_transformClass, "get_childCount", 0);
    g_transform_GetChild = FindMethod(g_transformClass, "GetChild", 1);
    g_transform_Find = FindMethod(g_transformClass, "Find", 1);
    g_transform_get_parent = FindMethod(g_transformClass, "get_parent", 0);
    g_transform_get_position = FindMethod(g_transformClass, "get_position", 0);
  }
  if (void *objectClass = FindClass("UnityEngine", "Object", asms, ac))
    g_object_get_name = FindMethod(objectClass, "get_name", 0);
  if (void *physicsClass = FindClass("UnityEngine", "Physics", asms, ac)) {
    void *raycast = FindMethod(physicsClass, "Raycast", 6);
    const char *p0 = nullptr, *p2 = nullptr, *p5 = nullptr;
    if (raycast && il2cpp_method_get_param && il2cpp_type_get_name) {
      void *t0 = il2cpp_method_get_param(raycast, 0);
      void *t2 = il2cpp_method_get_param(raycast, 2);
      void *t5 = il2cpp_method_get_param(raycast, 5);
      p0 = t0 ? il2cpp_type_get_name(t0) : nullptr;
      p2 = t2 ? il2cpp_type_get_name(t2) : nullptr;
      p5 = t5 ? il2cpp_type_get_name(t5) : nullptr;
    }
    const bool signatureOk = p0 && p2 && p5 && strstr(p0, "Vector3") &&
        strstr(p2, "RaycastHit") && strstr(p5, "QueryTriggerInteraction");
    g_physicsRaycastMethod = signatureOk ? raycast : nullptr;
  }
  g_animatorClass = FindClass("UnityEngine", "Animator", asms, ac);
  if (g_animatorClass) {
    g_animator_GetBoneTransform = FindMethod(g_animatorClass, "GetBoneTransform", 1);
    g_animator_get_avatar = FindMethod(g_animatorClass, "get_avatar", 0);
    g_animator_get_isHuman = FindMethod(g_animatorClass, "get_isHuman", 0);
    if (void *behaviourClass = FindClass("UnityEngine", "Behaviour", asms, ac)) {
      g_animator_get_enabled = FindMethod(behaviourClass, "get_enabled", 0);
      g_animator_set_enabled = FindMethod(behaviourClass, "set_enabled", 1);
    }
  }
  g_gameObjectClass = FindClass("UnityEngine", "GameObject", asms, ac);
  if (g_gameObjectClass) {
    g_gameObject_get_transform = FindMethod(g_gameObjectClass, "get_transform", 0);
    g_gameObject_GetComponent = FindMethod(g_gameObjectClass, "GetComponent", 1);
  }
  g_componentClass = FindClass("UnityEngine", "Component", asms, ac);
  if (g_componentClass) {
    g_component_get_gameObject = FindMethod(g_componentClass, "get_gameObject", 0);
    g_component_get_transform = FindMethod(g_componentClass, "get_transform", 0);
  }
  GhostRig_ResolveUnityApis(asms, ac);
  g_skinnedMeshRendererClass = FindClass("UnityEngine", "SkinnedMeshRenderer", asms, ac);
  if (g_skinnedMeshRendererClass) {
    g_smr_get_sharedMesh = FindMethod(g_skinnedMeshRendererClass, "get_sharedMesh", 0);
    g_smr_GetBlendShapeWeight = FindMethod(g_skinnedMeshRendererClass, "GetBlendShapeWeight", 1);
    g_smr_SetBlendShapeWeight = FindMethod(g_skinnedMeshRendererClass, "SetBlendShapeWeight", 2);
    g_smr_get_bones = FindMethod(g_skinnedMeshRendererClass, "get_bones", 0);
  }
  if (void *meshClass = FindClass("UnityEngine", "Mesh", asms, ac)) {
    g_mesh_get_blendShapeCount = FindMethod(meshClass, "get_blendShapeCount", 0);
    g_mesh_GetBlendShapeName = FindMethod(meshClass, "GetBlendShapeName", 1);
  }
  g_camSetPos = (void (*)(void *, float *))il2cpp_resolve_icall(
      "UnityEngine.Transform::set_position_Injected(UnityEngine.Vector3&)");
  g_camSetRot = (void (*)(void *, float *))il2cpp_resolve_icall(
      "UnityEngine.Transform::set_rotation_Injected(UnityEngine.Quaternion&)");
  g_camGetPos = (CamGetPosRot_t)il2cpp_resolve_icall(
      "UnityEngine.Transform::get_position_Injected(UnityEngine.Vector3&)");
  g_camGetRot = (CamGetPosRot_t)il2cpp_resolve_icall(
      "UnityEngine.Transform::get_rotation_Injected(UnityEngine.Quaternion&)");

  ResolveFinalIkLayout(asms, ac);
  if (void *moveCompClass = FindClass("Beyond.Gameplay.Core", "MovementComponent", asms, ac)) {
    const char *floorNames[] = {"currentFloor"};
    g_offCurrentFloor = FindFieldInHierarchy(moveCompClass, floorNames, 1, nullptr);
    g_findFloorMethod = FindMethod(moveCompClass, "FindFloor", 3);
    g_computeFloorDistMethod = FindMethod(moveCompClass, "ComputeFloorDist", 7);
    g_beFloorResultLayoutOk = VerifyFloorResultLayout(g_computeFloorDistMethod);
    const char *grounderNames[] = {"m_bipedIK"};
    g_offMovementGrounder = FindFieldInHierarchy(moveCompClass, grounderNames, 1, nullptr);
  }

  const bool core = g_transform_get_localRotation && g_transform_set_localRotation &&
      g_transform_get_localPosition && g_transform_set_localPosition &&
      g_transform_get_parent && g_animator_GetBoneTransform && g_animator_set_enabled &&
      g_object_get_name && g_component_get_transform && g_camGetPos && g_camGetRot &&
      g_camSetPos && g_camSetRot && s_ghostGameObjectCtor && g_finalIkSolverLayoutOk;
  char summary[512];
  snprintf(summary, sizeof(summary),
           "resolved core=%d finalIkSolver=%d grounder=%d grounding=%d "
           "movementGrounder=%d floorLayout=%d physicsRaycast=%d",
           core ? 1 : 0, g_finalIkSolverLayoutOk ? 1 : 0,
           g_finalIkGrounderLayoutOk ? 1 : 0, g_finalIkGroundingLayoutOk ? 1 : 0,
           g_offMovementGrounder >= 0 ? 1 : 0, g_beFloorResultLayoutOk ? 1 : 0,
           g_physicsRaycastMethod ? 1 : 0);
  if (BE_EIEM_SLOT == 0) BeHostLog(summary);
  Log("[BE-RESOLVE] slot=%d %s", BE_EIEM_SLOT, summary);
  if (!core) return false;

  if (targets) {
    void *solverMgrClass = FindClass("RootMotion", "SolverManager", asms, ac);
    if (!solverMgrClass)
      solverMgrClass = FindClass("RootMotion.FinalIK", "SolverManager", asms, ac);
    void *bipedIKClass = FindClass("RootMotion.FinalIK", "BipedIK", asms, ac);
    void *grounderClass = FindClass("RootMotion.FinalIK", "GrounderBipedIK", asms, ac);
    void *ikTrigClass = FindClass("RootMotion.FinalIK", "IKSolverTrigonometric", asms, ac);
    targets->solver_manager_late_update =
        CodeOf(solverMgrClass ? FindMethod(solverMgrClass, "LateUpdate", 0) : nullptr);
    targets->biped_update_solver =
        CodeOf(bipedIKClass ? FindMethod(bipedIKClass, "UpdateSolver", 0) : nullptr);
    targets->trig_on_update =
        CodeOf(ikTrigClass ? FindMethod(ikTrigClass, "OnUpdate", 0) : nullptr);
    targets->grounder_on_solver_update =
        CodeOf(grounderClass ? FindMethod(grounderClass, "OnSolverUpdate", 0) : nullptr);
    targets->grounder_on_post_solver_update =
        CodeOf(grounderClass ? FindMethod(grounderClass, "OnPostSolverUpdate", 0) : nullptr);
    void *clothClass = FindClass("BeyondDynamicBone", "BeyondBoneCloth", asms, ac);
    void *setWeight = clothClass ? FindMethod(clothClass, "SetClothSimulateWeight", 1) : nullptr;
    void *weightType = setWeight && il2cpp_method_get_param ? il2cpp_method_get_param(setWeight, 0) : nullptr;
    const char *weightName = weightType && il2cpp_type_get_name ? il2cpp_type_get_name(weightType) : nullptr;
    if (weightName && !strcmp(weightName, "System.Single")) targets->cloth_set_weight = CodeOf(setWeight);
  }
  return true;
}

bool PrepareFace(EiemSlot::FaceTargets *targets) {
  if (g_beFaceReady) return true;
  size_t ac = 0;
  void **asms = Assemblies(ac);
  if (!asms) return false;
  void *smcClass = FindClass("Beyond.Gameplay.View.SkeletalMorph", "SkeletalMorphCore", asms, ac);
  if (!smcClass) smcClass = FindClass("Beyond.Gameplay.View", "SkeletalMorphCore", asms, ac);
  if (!smcClass) smcClass = FindClass("Beyond.Gameplay.Core", "SkeletalMorphCore", asms, ac);
  if (!smcClass) {
    BeHostLog("SkeletalMorphCore not found; VMD facial morphs unavailable");
    return false;
  }
  ResolveSMCOffsets(smcClass);
  // EIEM falls back to fixed offsets (SafeOff) when a field is missing; here a
  // missing field disables the face path instead.
  const struct { int offset; const char *name; } required[] = {
      {OFF_allMorphs, "m_allMorphs"}, {OFF_bigList, "m_morphNameHashToMorphDataBoneBigList"},
      {OFF_nativeHashMap, "m_morphNameHashToMorphData"}, {OFF_avatarData, "m_avatarData"},
      {OFF_allBonesTransforms, "m_allBonesTransforms"}, {OFF_boneIDToIdx, "m_boneIDToIdx"},
      {OFF_phonemesWeights, "m_phonemesWeights"}, {OFF_mainEmotion, "m_mainEmotion"},
      {OFF_poseDictMorph, "m_poseDictMorph"}, {OFF_microExprWeights, "m_microExpressionWeights"},
      {OFF_smcEyeLookAt, "enableEyeLookAtIK"}};
  for (const auto &field : required) {
    if (field.offset < 0) {
      BeHostLog(std::string("SkeletalMorphCore.") + field.name +
                " not found; VMD facial morphs unavailable");
      return false;
    }
  }
  g_skeletalMorphCoreClass = smcClass;
  if (targets) {
    void *it = nullptr;
    while (void *m = il2cpp_class_get_methods(smcClass, &it)) {
      const char *name = il2cpp_method_get_name(m);
      if (!name) continue;
      const int count = static_cast<int>(il2cpp_method_get_param_count(m));
      if (!targets->smc_update && !strcmp(name, "Update") && count == 1)
        targets->smc_update = CodeOf(m);
      if (!targets->morph_job && !strcmp(name, "DoEvaluateMorphToBoneJob") && count == 2)
        targets->morph_job = CodeOf(m);
      if (!targets->special_job && !strcmp(name, "DoEvaluateSpecialMorphToBoneJob") && count == 2)
        targets->special_job = CodeOf(m);
    }
  }
  g_beFaceReady = true;
  return true;
}

void ResetCharacterCaches() {
  s_cachedMovementComp = nullptr;
  s_cachedEntity = nullptr;
  g_fingerTransformsResolved = false;
  g_fingerRestCaptured = false;
  memset(g_fingerTransforms, 0, sizeof(g_fingerTransforms));
  g_confirmedSMC = nullptr;
  g_skeletalMorphCore = nullptr;
  g_boneMapReady = false;
  // trojan.h ResetFaceCache (1bc9baa).
  g_faceBonesCaptured = false;
  g_faceBoneRefs = nullptr;
  g_mouthShapesResolved = false;
  g_extraMorphsResolved = false;
  g_bigListCaptured = false;
  g_hashCorrelationDone = false;
  g_capturedLen = 0;
  g_smcResetRequested = true;
  for (int i = 0; i < NUM_MOUTH_SHAPES; i++) g_mouthShapes[i].resolved = false;
  for (int i = 0; i < NUM_EXTRA_MORPHS; i++)
    for (int t = 0; t < g_extraMorphs[i].targetCount; t++)
      g_extraMorphs[i].targets[t].resolved = false;
}

void ApplyOptions() {
  s_directVmdKneeMixEnabled.store(g_beOptions.knee_mix, std::memory_order_release);
  if (g_beOptions.knee_weight >= 0.0f && std::isfinite(g_beOptions.knee_weight))
    s_directVmdKneeMixWeight.store(std::clamp(g_beOptions.knee_weight, 0.0f, 1.0f),
                                   std::memory_order_release);
  s_directVmdTerrainDesiredEnabled.store(g_beOptions.terrain, std::memory_order_release);
  DirectVmdRuntime_SetMotionMultiplier(g_beOptions.motion_scale);
  g_beClothMode.store(static_cast<int>(g_beOptions.cloth), std::memory_order_release);
}

void *FirstFaceBone(void *smc) {
  __try {
    void *array = *reinterpret_cast<void **>((char *)smc + OFF_allBonesTransforms);
    if (!array || *reinterpret_cast<int *>((char *)array + 24) <= 0) return nullptr;
    return *reinterpret_cast<void **>((char *)array + 32);
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return nullptr;
  }
}

// --- Api ------------------------------------------------------------------------
bool ApiInitialize(const BE_HostApiV1 *host, const std::wstring &log_path,
                   EiemSlot::HookTargets *targets) {
  if (g_beInitialized) return g_beAvailable;
  g_beInitialized = true;
  g_beHost = host;
  QueryPerformanceFrequency(&g_qpcFrequency);
  InitializeCriticalSection(&g_logLock);
  if (!log_path.empty())
    g_logHandle = CreateFileW(log_path.c_str(), GENERIC_WRITE, FILE_SHARE_READ, nullptr,
                              CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
  Log("=== Better Endfield EIEM DirectVmd port (upstream 1bc9baa) slot %d ===", BE_EIEM_SLOT);
  g_beAvailable = ResolveAll(targets);
  if (g_beAvailable) {
    ApplyOptions();
    StartWorker();
  }
  return g_beAvailable;
}

bool ApiPrepareFace(EiemSlot::FaceTargets *targets) {
  return g_beAvailable && PrepareFace(targets);
}

void ApiSetOriginals(const EiemSlot::Originals &originals) {
  s_ghostOrigGrounderOnSolverUpdate = originals.grounder_on_solver_update;
  s_ghostOrigGrounderOnPostSolverUpdate = originals.grounder_on_post_solver_update;
  g_origIkTrigOnUpdate = originals.trig_on_update;
  g_origSMCUpdate = reinterpret_cast<SMCUpdate_t>(originals.smc_update);
  g_origMorphToBoneJob = reinterpret_cast<MorphToBoneJob_t>(originals.morph_job);
  g_origSpecialMorphJob = reinterpret_cast<SpecialMorphJob_t>(originals.special_job);
}

void ApiSetOptions(const EiemBody::Options &options) {
  g_beOptions = options;
  if (g_beAvailable) ApplyOptions();
}

void ApiSetSharedAnchor(bool valid, const float position[3], const float rotation[4]) {
  g_beFixedAnchorValid = valid && position && rotation;
  if (!g_beFixedAnchorValid) return;
  g_beFixedAnchorPosition = {position[0], position[1], position[2]};
  g_beFixedAnchorRotation = {rotation[0], rotation[1], rotation[2], rotation[3]};
}

void ApiLoad(const std::string &motion_utf8, const std::string &face_utf8) {
  if (!g_beAvailable) return;
  g_beLoadedMotion = motion_utf8;
  GhostRig_RequestVmdReload(motion_utf8.c_str());
  g_beRequestedClipRevision = s_directVmdClipRequestRevision.load(std::memory_order_acquire);
  DirectVmdRuntime_RequestMorphOverride(face_utf8.empty() ? nullptr : face_utf8.c_str());
}

EiemBody::LoadState ApiLoadStatus() {
  using EiemBody::LoadState;
  if (!g_beAvailable || g_beLoadedMotion.empty()) return LoadState::Idle;
  if (g_workerHandledClipRevision.load(std::memory_order_acquire) < g_beRequestedClipRevision)
    return LoadState::Loading;
  return DirectVmdRuntime_IsLoaded() ? LoadState::Ready : LoadState::Failed;
}

double ApiDurationSeconds() {
  return DirectVmdRuntime_PublicDurationFrames() / kVmdFramesPerSecond;
}

void ApiSetTarget(void *entity, void *animator, void *movement) {
  g_beMainThreadId.store(GetCurrentThreadId(), std::memory_order_release);
  if (entity != g_mainCharEntity) ResetCharacterCaches();
  g_mainCharEntity = entity;
  g_cachedAnimator = animator;
  s_cachedEntity = movement ? entity : nullptr;
  s_cachedMovementComp = movement;
}

bool ApiStart(const char *reason) {
  if (!g_beAvailable) return false;
  g_beMainThreadId.store(GetCurrentThreadId(), std::memory_order_release);
  ApplyOptions();
  return MotionBackend_EnterDirectMainThread(true, reason);
}

void ApiStop(const char *reason) {
  if (!g_beAvailable) return;
  if (g_motionBackend.Is(MotionBackend::DirectVmd) || GhostRig_IsRequestedEnabled() ||
      GhostRig_IsAliveForGui())
    MotionBackend_EnterNativeMainThread(GhostRigCleanupReason::Stop, reason);
  g_clockPlaying.store(false, std::memory_order_release);
}

bool ApiActive() {
  return g_beAvailable && g_motionBackend.Is(MotionBackend::DirectVmd) &&
         GhostRig_IsRequestedEnabled();
}

bool ApiBusy() {
  return g_beAvailable &&
         (GhostRig_IsRequestedEnabled() || GhostRig_IsAliveForGui() ||
          s_ghostRig.generation != s_ghostRequestedGeneration.load(std::memory_order_acquire));
}

void ApiPublishClock(double seconds, bool playing) {
  if (!std::isfinite(seconds)) seconds = 0.0;
  g_clockSeconds.store(seconds, std::memory_order_release);
  g_clockStamp.store(Qpc(), std::memory_order_release);
  g_clockPlaying.store(playing, std::memory_order_release);
}

void ApiShutdown() {
  if (!g_beInitialized) return;
  GhostRig_RequestProcessDetach();
  DirectVmdRuntime_RequestProcessDetach();
  g_workerRun.store(false, std::memory_order_release);
  if (g_worker) {
    WaitForSingleObject(g_worker, 2000);
    CloseHandle(g_worker);
    g_worker = nullptr;
  }
}

void ApiSolverManagerLateUpdate(void *self) { GhostRig_OnSolverManagerLateUpdate(self); }
void ApiBeforeFinalIk(void *bipedIK) { GhostRig_BeforeFinalIK(bipedIK); }
bool ApiSuppressFinalIk(void *bipedIK) {
  if (!GhostRig_ShouldSuppressFinalIKPhase5Safety(bipedIK)) return false;
  GhostRig_LogFinalIKSuppressedPhase5Safety(bipedIK);
  return true;
}
void ApiAfterFinalIk(void *bipedIK) { GhostRig_AfterFinalIK(bipedIK); }
void ApiBeforeLeg(void *solver, void *methodInfo) {
  if (g_motionBackend.Is(MotionBackend::DirectVmd))
    GhostRig_BeforeLegSolverUpdate(solver, methodInfo);
}
void ApiAfterLeg(void *solver) { GhostRig_AfterLegSolverUpdate(solver); }
bool ApiGrounderActive(void *grounder) { return GhostRig_IsActiveGrounderCallback(grounder); }
void ApiGrounderOnSolverUpdate(void *self, void *methodInfo) {
  GhostRig_HookedGrounderOnSolverUpdate(self, methodInfo);
}
void ApiGrounderOnPostSolverUpdate(void *self, void *methodInfo) {
  GhostRig_HookedGrounderOnPostSolverUpdate(self, methodInfo);
}

// An SMC belongs to this slot when its first face bone sits under the rig root.
bool ApiOwnsSmc(void *smc) {
  if (!g_beFaceReady || !smc || !GhostRig_IsRequestedEnabled() || !g_mainCharEntity)
    return false;
  void *root = GhostRig_GetOwnerRoot(reinterpret_cast<uintptr_t>(g_mainCharEntity));
  void *bone = root ? FirstFaceBone(smc) : nullptr;
  return bone && GhostRig_IsComponentUnderOwner(bone, root);
}
bool ApiClaimsSmc(void *smc) {
  return smc && (smc == g_confirmedSMC || smc == g_skeletalMorphCore);
}
void ApiSmcUpdate(void *self, float deltaTime, void *methodInfo) {
  Hooked_SMCUpdate(self, deltaTime, methodInfo);
}
void ApiMorphJob(void *self, void *smc, void *data, void *methodInfo) {
  Hooked_MorphToBoneJob(self, smc, data, methodInfo);
}
void ApiSpecialJob(void *self, void *smc, void *data, void *methodInfo) {
  Hooked_SpecialMorphJob(self, smc, data, methodInfo);
}

bool ApiCameraReference(EiemBody::CameraReference *reference) {
  if (!reference || !g_beAvailable || !g_motionBackend.Is(MotionBackend::DirectVmd))
    return false;
  DirectVmdWorldPosePod anchor;
  float motionScale = 0.0f, naturalBindHeight = 0.0f;
  VmdVec3 sideOffset = {};
  bool terrainFollow = false;
  uint64_t generation = 0;
  uintptr_t owner = 0;
  if (!GhostRig_GetDirectCameraReference(&anchor, &motionScale, &naturalBindHeight,
                                         &sideOffset, &terrainFollow, &generation, &owner))
    return false;
  reference->position[0] = anchor.position.x;
  reference->position[1] = anchor.position.y;
  reference->position[2] = anchor.position.z;
  reference->rotation[0] = anchor.rotation.x;
  reference->rotation[1] = anchor.rotation.y;
  reference->rotation[2] = anchor.rotation.z;
  reference->rotation[3] = anchor.rotation.w;
  reference->motion_scale = motionScale;
  reference->natural_height = naturalBindHeight;
  reference->offset[0] = sideOffset.x;
  reference->offset[1] = sideOffset.y;
  reference->offset[2] = sideOffset.z;
  return true;
}

// cloth_writer.h's ClothNativeWeightWriter, split around eiem_body.cpp's
// single detour. The claim is kept for the readback of the same call.
int g_beWeightIndex = -1;
eiem_cloth::Owner g_beWeightOwner{};
bool ApiClothWeightPrepare(void *cloth, uintptr_t caller, float requested, float *forwarded) {
  float value = requested;
  eiem_cloth::Owner owner{};
  const int index = ClothWriterPrepare(cloth, caller, requested, value, owner);
  if (index < 0) return false;
  g_beWeightIndex = index;
  g_beWeightOwner = owner;
  *forwarded = value;
  return true;
}
void ApiClothWeightReadback(void *cloth, uintptr_t caller, float requested, float forwarded) {
  const int index = std::exchange(g_beWeightIndex, -1);
  ClothWriterReadback(index, g_beWeightOwner, cloth, caller, requested, forwarded);
}
bool ApiTerrainAvailable() {
  return g_beAvailable && g_beFloorResultLayoutOk && g_computeFloorDistMethod && g_finalIkGrounderLayoutOk;
}
const char* ApiTerrainReason() { return ApiTerrainAvailable()?"":"PC floor/grounder metadata contract unavailable"; }

const EiemSlot::Api kApi{
    ApiInitialize, ApiPrepareFace, ApiSetOriginals, ApiSetOptions, ApiSetSharedAnchor,
    ApiLoad, ApiLoadStatus, ApiDurationSeconds, ApiSetTarget, ApiStart, ApiStop,
    ApiActive, ApiBusy, ApiPublishClock, ApiShutdown,
    ApiSolverManagerLateUpdate, ApiBeforeFinalIk, ApiSuppressFinalIk, ApiAfterFinalIk,
    ApiBeforeLeg, ApiAfterLeg, ApiGrounderActive, ApiGrounderOnSolverUpdate,
    ApiGrounderOnPostSolverUpdate, ApiOwnsSmc, ApiClaimsSmc, ApiSmcUpdate, ApiMorphJob,
    ApiSpecialJob, ApiCameraReference, ApiClothWeightPrepare, ApiClothWeightReadback,ApiTerrainAvailable,ApiTerrainReason};
} // namespace
} // namespace BE_EIEM_NS

const BetterEndfield::EiemSlot::Api *BetterEndfield::EiemSlot::BE_EIEM_API() {
  return &BE_EIEM_NS::kApi;
}

#endif // Android managed adapter / PC EIEM runtime
