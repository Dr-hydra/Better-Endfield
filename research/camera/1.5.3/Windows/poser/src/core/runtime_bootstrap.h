#pragma once
#include "il2cpp_api.h"

// Observing a completed managed call on an already registered game thread is a
// readiness signal. A loaded DLL, non-null domain or fixed delay is not one:
// domain_get can create the domain before GC permits external registration.
// This temporary hook forwards the exact exported runtime_invoke ABI. It never
// attaches a thread, calls managed code, logs or blocks the game's calling thread.
static t_il2cpp_runtime_invoke g_bootstrapOriginalInvoke = nullptr;
static std::atomic<bool> g_bootstrapObserved{false};
static void *BootstrapRuntimeInvoke(void *method, void *object, void **args,
                                   void **exception) {
  void *result = g_bootstrapOriginalInvoke(method, object, args, exception);
  if (!g_bootstrapObserved.load(std::memory_order_relaxed) && CurrentRuntimeThread())
    g_bootstrapObserved.store(true, std::memory_order_release);
  return result;
}

static bool WaitForRuntimeReady(DWORD timeoutMs = 180000) {
  if (g_runtimeReady.load(std::memory_order_acquire)) return true;
  if (!il2cpp_runtime_invoke || !il2cpp_thread_current || !il2cpp_domain_get ||
      !il2cpp_thread_attach || !il2cpp_thread_detach) {
    Log("[BOOT] Required runtime exports missing; plugin initialization skipped");
    return false;
  }
  auto target = reinterpret_cast<void *>(il2cpp_runtime_invoke);
  auto status = MH_CreateHook(target, reinterpret_cast<void *>(BootstrapRuntimeInvoke),
                             reinterpret_cast<void **>(&g_bootstrapOriginalInvoke));
  if (status != MH_OK) {
    Log("[BOOT] Cannot observe runtime startup (%d); plugin initialization skipped", status);
    return false;
  }
  if ((status = MH_EnableHook(target)) != MH_OK) {
    Log("[BOOT] Cannot enable runtime observer (%d); plugin initialization skipped", status);
    return false;
  }
  Log("[BOOT] Waiting for a managed call on a registered game thread...");
  ULONGLONG start = GetTickCount64();
  while (!g_bootstrapObserved.load(std::memory_order_acquire) &&
         GetTickCount64() - start < timeoutMs) Sleep(25);
  // Keep the trampoline allocated: another call may still be returning through
  // it. Disabling restores the export without freeing an in-flight trampoline.
  MH_DisableHook(target);
  if (!g_bootstrapObserved.load(std::memory_order_acquire)) {
    Log("[BOOT] Runtime readiness timed out; no plugin thread was attached");
    return false;
  }
  g_runtimeReady.store(true, std::memory_order_release);
  Log("[BOOT] Runtime ready after %llu ms; external threads may now attach",
      GetTickCount64() - start);
  return true;
}
