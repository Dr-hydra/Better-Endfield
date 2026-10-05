#pragma once
#include "il2cpp_api.h"

// Application.quitting is non-cancellable. Observe its native dispatcher, not
// wantsToQuit/WM_CLOSE (both can be cancelled). Validate the complete signature.
// Reference: Unity-Technologies/UnityCsReference, Application.cs.
using ApplicationQuitFn = void (*)(void *method);
using RuntimeShutdownFn = void (*)();
static ApplicationQuitFn g_originalApplicationQuit = nullptr;
static RuntimeShutdownFn g_originalRuntimeShutdown = nullptr;
static void (*g_runtimeQuitSignal)() = nullptr;
static void (*g_runtimeQuitFinalize)() = nullptr;
static DWORD g_runtimeDrainBudgetMs = 1500;
static void BeginRuntimeQuit(const char *source) {
  if (!g_runtimeAdmission.close()) return;
  g_runtimeReady.store(false,std::memory_order_release);
  Log("[EXIT] begin source=%s tid=%lu activeRuntimeScopes=%u",source,GetCurrentThreadId(),g_runtimeAdmission.active());
  if (g_runtimeQuitSignal) g_runtimeQuitSignal();
  const ULONGLONG begin=GetTickCount64();
  while (g_runtimeAdmission.active() && GetTickCount64()-begin<g_runtimeDrainBudgetMs) Sleep(1);
  Log("[EXIT] runtime scopes drained=%d active=%u elapsedMs=%llu",g_runtimeAdmission.active()==0,g_runtimeAdmission.active(),GetTickCount64()-begin);
  if (g_runtimeQuitFinalize) g_runtimeQuitFinalize();
}
static void HookedApplicationQuit(void *method) {
  BeginRuntimeQuit("Application.quitting");
  if (g_originalApplicationQuit) g_originalApplicationQuit(method);
  Log("[EXIT] Application.quitting returned");
}
static void HookedRuntimeShutdown() {
  BeginRuntimeQuit("il2cpp_shutdown");
  Log("[EXIT] entering il2cpp_shutdown");
  if (g_originalRuntimeShutdown) g_originalRuntimeShutdown();
  g_runtimeTornDown.store(true,std::memory_order_release);
  Log("[EXIT] il2cpp_shutdown returned");
}
static bool ApplicationQuitSignature(void *method) {
  if (!method || !il2cpp_method_get_flags || !il2cpp_method_get_param_count ||
      !il2cpp_method_get_return_type || !il2cpp_type_get_type) return false;
  uint32_t implementation=0;
  return (il2cpp_method_get_flags(method,&implementation)&0x10)!=0 &&
      il2cpp_method_get_param_count(method)==0 &&
      il2cpp_type_get_type(il2cpp_method_get_return_type(method))==1;
}
static void InstallRuntimeShutdownHooks() {
  size_t count=0;auto assemblies=il2cpp_domain_get_assemblies(il2cpp_domain_get(),&count);
  void *app=FindClass("UnityEngine","Application",assemblies,count);
  void *quit=app?FindMethod(app,"Internal_ApplicationQuit",0):nullptr;
  bool observed=ApplicationQuitSignature(quit) &&
      Hook(quit,"Application.Internal_ApplicationQuit",(void*)HookedApplicationQuit,(void**)&g_originalApplicationQuit);
  bool fallback=false;
  if (void *shutdown=(void*)GetProcAddress(hGA,"il2cpp_shutdown")) {
    if (MH_CreateHook(shutdown,(void*)HookedRuntimeShutdown,(void**)&g_originalRuntimeShutdown)==MH_OK)
      fallback=MH_EnableHook(shutdown)==MH_OK;
  }
  Log("[EXIT] hooks quitting=%d runtimeShutdown=%d",observed,fallback);
}
