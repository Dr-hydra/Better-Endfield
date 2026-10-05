#pragma once
#include "base.h"
#include "il2cpp_api.h"
#include "math/frame_cadence.h"
#include <atomic>
#include <chrono>
#include <cmath>

void GameFrameTick();
static double FrameNow() {
  return std::chrono::duration<double>(
             std::chrono::steady_clock::now().time_since_epoch())
      .count();
}
static std::atomic<bool> g_frameRunning{false};
static std::atomic<double> g_lastRenderTick{-1e30};
static std::atomic<double> g_nextIndependentTick{0};
static std::atomic<unsigned> g_frameBusy{0};
static std::atomic<DWORD> g_frameGameThreadId{0};
static HANDLE g_frameWorker = nullptr, g_frameStop = nullptr;
static void *g_frameCountMethod = nullptr;
static poser::FrameCadence g_frameCadence;
struct FrameDiagnostics {
  double hz = 0, maxGapMs = 0, maxCostMs = 0;
  bool gameDriven = false;
  int source = 0; // 0 independent, 1 camera, 2 SRP, 3 character update, 4 camera manager
  unsigned busy = 0;
};
static void (*g_gameMaintenance)() = nullptr; // main-thread release jobs, including while disabled
static FrameDiagnostics g_frameDiagnostics; // protected by g_poseMutex
static bool RunFrameTick(bool fromGame, int frame = -1, int source = 1) {
  if (RuntimeClosing() || (!g_frameRunning.load() && !(fromGame && g_gameMaintenance)))
    return false;
  double now = FrameNow();
  if (fromGame) {
    g_lastRenderTick.store(now);
    g_frameGameThreadId.store(GetCurrentThreadId());
  }
  // Never block Unity waiting for the editor or for worker IL2CPP invocations.
  std::unique_lock<std::recursive_mutex> lock(g_poseMutex, std::try_to_lock);
  if (!lock.owns_lock()) {
    if (fromGame)
      ++g_frameBusy;
    return false;
  }
  static thread_local bool nested = false;
  if (nested)
    return false;
  struct Guard {
    bool &v;
    Guard(bool &b) : v(b) { v = true; }
    ~Guard() { v = false; }
  } guard(nested);
  // Attach only during an actual fallback tick, after acquiring the lock.
  // Never keep a managed thread alive while the worker sleeps or waits.
  if (!fromGame && !g_frameCadence.fallbackDue(now,g_lastRenderTick.load())) return false;
  RuntimeThreadScope runtime;
  if (!runtime.ready) return false;
  if (fromGame && g_gameMaintenance) g_gameMaintenance();
  if (!g_frameRunning.load()) return false;
  if (fromGame ? !g_frameCadence.gameDue(frame)
               : !g_frameCadence.fallbackDue(now, g_lastRenderTick.load())) return false;
  static double window = 0, gap = 0, cost = 0;
  static unsigned count = 0;
  if (std::isfinite(g_frameCadence.lastStep))
    gap = (std::max)(gap, (now - g_frameCadence.lastStep) * 1000);
  g_frameCadence.stepped(now, fromGame ? frame : -1);
  g_nextIndependentTick.store(now + 1.0 / 120);
  GameFrameTick();
  cost = (std::max)(cost, (FrameNow() - now) * 1000);
  g_frameDiagnostics.gameDriven = fromGame;
  g_frameDiagnostics.source = fromGame ? source : 0;
  if (!window)
    window = now;
  ++count;
  if (now - window >= 1) {
    g_frameDiagnostics.hz = count / (now - window);
    g_frameDiagnostics.maxGapMs = gap;
    g_frameDiagnostics.maxCostMs = cost;
    g_frameDiagnostics.busy = g_frameBusy.exchange(0);
    window = now;
    count = 0;
    gap = cost = 0;
  }
  return true;
}
static int ReadUnityFrameCount() {
  __try {
    void *boxed = Invoke(g_frameCountMethod, nullptr);
    return boxed ? *reinterpret_cast<int *>(static_cast<char *>(boxed) + 16)
                 : -1;
  } __except (1) {
    return -1;
  }
}
using CameraPreCullFn = void(__fastcall *)(void *, void *);
static CameraPreCullFn g_originalPreCull = nullptr;
static void SampleGameRenderFrame(int source) {
  if (!g_frameRunning.load() && !g_gameMaintenance)
    return;
  try {
    int frame = ReadUnityFrameCount();
    if (frame >= 0 || !g_frameRunning.load())
      RunFrameTick(true, frame, source);
  } catch (...) {
    Log("[FRAME] game callback failed; keeping independent fallback");
  }
}
static void __fastcall FrameCameraPreCull(void *camera, void *method) {
  if (g_originalPreCull)
    g_originalPreCull(camera, method);
  SampleGameRenderFrame(1);
}
// Unity 2021/2022's AtomicSafetyHandle is a 16-byte aggregate. Preserve its
// complete value and the trailing MethodInfo argument; validate metadata first.
struct FrameSafetyHandle {
  uint64_t raw[2];
};
using SrpRenderLoopFn = void(__fastcall *)(void *, void *, void *,
                                           FrameSafetyHandle, void *);
static SrpRenderLoopFn g_originalSrpLoop = nullptr;
static void __fastcall FrameSrpLoop(void *pipeline, void *loop, void *requests,
                                    FrameSafetyHandle safety, void *method) {
  // Render-request passes can be probes/off-screen work; sample the normal
  // loop.
  if (!requests)
    SampleGameRenderFrame(2);
  if (g_originalSrpLoop)
    g_originalSrpLoop(pipeline, loop, requests, safety, method);
}
static bool FrameParameterClass(void *method, int index, const char *space,
                                const char *name) {
  if (!il2cpp_method_get_param || !il2cpp_class_from_type ||
      !il2cpp_class_get_namespace)
    return false;
  void *klass = il2cpp_class_from_type(il2cpp_method_get_param(method, index));
  if (!klass)
    return false;
  const char *actualName = il2cpp_class_get_name(klass);
  const char *actualSpace = il2cpp_class_get_namespace(klass);
  return actualName && actualSpace && !strcmp(actualName, name) &&
         !strcmp(actualSpace, space);
}
static bool ValidateSrpSignature(void *method) {
  if (!method || sizeof(void *) != 8 || !il2cpp_method_get_return_type ||
      !il2cpp_type_get_type)
    return false;
  using ValueSizeFn = int32_t (*)(void *, uint32_t *);
  using MethodFlagsFn = uint32_t (*)(void *, uint32_t *);
  auto valueSize = reinterpret_cast<ValueSizeFn>(
      GetProcAddress(hGA, "il2cpp_class_value_size"));
  auto flags = reinterpret_cast<MethodFlagsFn>(
      GetProcAddress(hGA, "il2cpp_method_get_flags"));
  uint32_t implementationFlags = 0;
  if (!valueSize || !flags || !(flags(method, &implementationFlags) & 0x10) ||
      il2cpp_type_get_type(il2cpp_method_get_return_type(method)) != 1 ||
      !FrameParameterClass(method, 0, "UnityEngine.Rendering",
                           "RenderPipelineAsset") ||
      !FrameParameterClass(method, 1, "System", "IntPtr") ||
      !FrameParameterClass(method, 3, "Unity.Collections.LowLevel.Unsafe",
                           "AtomicSafetyHandle"))
    return false;
  int requestType = il2cpp_type_get_type(il2cpp_method_get_param(method, 2));
  if (requestType != 0x12 && requestType != 0x15 && requestType != 0x1c)
    return false; // class, generic List<...> or System.Object, never a value
  uint32_t alignment = 0;
  void *safety = il2cpp_class_from_type(il2cpp_method_get_param(method, 3));
  return valueSize(safety, &alignment) == sizeof(FrameSafetyHandle);
}
static void InstallFrameHook() {
  size_t count = 0;
  void **assemblies = il2cpp_domain_get_assemblies(il2cpp_domain_get(), &count);
  void *camera = FindClass("UnityEngine", "Camera", assemblies, count);
  void *time = FindClass("UnityEngine", "Time", assemblies, count);
  g_frameCountMethod = FindMethod(time, "get_frameCount", 0);
  bool ok = g_frameCountMethod &&
            Hook(FindMethod(camera, "FireOnPreCull", 1), "Camera.FireOnPreCull",
                 (void *)FrameCameraPreCull, (void **)&g_originalPreCull);
  void *manager = FindClass("UnityEngine.Rendering", "RenderPipelineManager",
                            assemblies, count);
  void *srpMethod = FindMethod(manager, "DoRenderLoop_Internal", 4);
  bool srp = g_frameCountMethod && ValidateSrpSignature(srpMethod) &&
             Hook(srpMethod, "RenderPipelineManager.DoRenderLoop_Internal",
                  (void *)FrameSrpLoop, (void **)&g_originalSrpLoop);
  Log("[FRAME] hooks: built-in=%d SRP=%d; independent 120 Hz fallback "
      "available",
      int(ok), int(srp));
}
static DWORD WINAPI FrameWorker(LPVOID) {
  HANDLE timer = CreateWaitableTimerExW(
      nullptr, nullptr, 0x2 /* high resolution */, TIMER_ALL_ACCESS);
  if (!timer)
    timer = CreateWaitableTimerW(nullptr, FALSE, nullptr);
  while (g_frameRunning.load() && !RuntimeClosing()) {
    try {
      RunFrameTick(false);
    } catch (...) {
      Log("[FRAME] independent tick failed");
    }
    if (timer) {
      double now = FrameNow();
      double delay =
          now - g_lastRenderTick.load() <= .25
              ? 1.0 / 120
              : (std::max)(.0001, g_nextIndependentTick.load() - now);
      LARGE_INTEGER due;
      due.QuadPart = -LONGLONG(delay * 1e7);
      if (SetWaitableTimer(timer, &due, 0, nullptr, nullptr, FALSE)) {
        HANDLE waits[] = {g_frameStop, timer};
        if (WaitForMultipleObjects(2, waits, FALSE, INFINITE) == WAIT_OBJECT_0)
          break;
      } else if (WaitForSingleObject(g_frameStop, 1) == WAIT_OBJECT_0)
        break;
    } else if (WaitForSingleObject(g_frameStop, 1) == WAIT_OBJECT_0)
      break;
  }
  if (timer)
    CloseHandle(timer);
  return 0;
}
static void StartGameFrameDriver() {
  if (RuntimeClosing() || g_frameWorker || g_frameRunning.load())
    return;
  g_frameStop = CreateEventW(nullptr, TRUE, FALSE, nullptr);
  if (!g_frameStop) {
    Log("[FRAME] cannot create stop event");
    return;
  }
  g_frameCadence = {};
  g_lastRenderTick.store(-1e30);
  g_frameRunning.store(true);
  g_frameWorker = CreateThread(nullptr, 0, FrameWorker, nullptr, 0, nullptr);
  if (!g_frameWorker)
    Log("[FRAME] cannot create fallback worker");
}
static void StopGameFrameDriver() {
  g_frameRunning.store(false);
  if (g_frameStop)
    SetEvent(g_frameStop);
  if (g_frameWorker) {
    DWORD joined=WaitForSingleObject(g_frameWorker,RuntimeClosing()?1500:INFINITE);
    if (joined!=WAIT_OBJECT_0) {
      Log("[EXIT] frame worker still finishing; handles retained, no forced termination");
      return;
    }
    CloseHandle(g_frameWorker);
    g_frameWorker = nullptr;
  }
  if (g_frameStop) {
    CloseHandle(g_frameStop);
    g_frameStop = nullptr;
  }
  // A callback that already entered must finish before restoring the actor.
  if (!RuntimeClosing()) { std::lock_guard<std::recursive_mutex> barrier(g_poseMutex); }
}
