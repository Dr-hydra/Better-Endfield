#define _CRT_SECURE_NO_WARNINGS
#include <windows.h>
#include <commdlg.h>
#include <cstdint>
#include <cstdio>
#include <cstring>
#include <map>
#include <string>
#include <vector>

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
#include "eiem_config.h"
#include "update_check.h"

#include "audio.h"

#include "smc_face.h"

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
#include "trojan.h"
#include "gui.h"
#include "init.h"

#define APPLEPIE_PLUGIN_IMPL
#include "applepie_mgr.h"

static AP_HotkeyInfo s_apHotkeys[] = {
    { "\xe5\x91\xbc\xe5\x87\xba/\xe9\x9a\x90\xe8\x97\x8f GUI \xe9\x9d\xa2\xe6\x9d\xbf", "gui_toggle_key", VK_INSERT },
};

static AP_PluginInfo s_apPluginInfo = {
    APPLEPIE_PLUGIN_API_VERSION,
    "eiem",
    "EIEM",
    "Endfield MMD",
    "eiem_config.txt",
    true
};

APPLEPIE_PLUGIN_EXPORT AP_PluginInfo* AP_GetPluginInfo() {
  return &s_apPluginInfo;
}

APPLEPIE_PLUGIN_EXPORT bool AP_PluginEnable() {
  g_pluginActive = true;
  Log("[AP] Plugin enabled by manager");
  return true;
}

APPLEPIE_PLUGIN_EXPORT bool AP_PluginDisable() {
  const MotionBackend disabledBackend = g_motionBackend.Current();
  g_motionBackend.TransitionTo(MotionBackend::Native);
  Log("[P3-BACKEND-REQUEST] old=%s new=Native reason=plugin-disabled "
      "backendGeneration=%llu callerTid=%lu",
      MotionBackendName(disabledBackend),
      (unsigned long long)g_motionBackend.Generation(),
      GetCurrentThreadId());
  DirectVmdRuntime_RequestStop();
  GhostRig_RequestEnabled(false, GhostRigCleanupReason::PluginDisabled);
  if (g_gameHwnd) {
    DWORD_PTR cleanupResult = 0;
    SendMessageTimeoutW(g_gameHwnd, WM_MMD_GUI_PHASE0_GHOST, 0,
                        static_cast<LPARAM>(GhostRigCleanupReason::PluginDisabled),
                        SMTO_ABORTIFHUNG | SMTO_BLOCK, 1000, &cleanupResult);
  }
  g_pluginActive = false;
  if (g_guiVisible) ToggleGui();
  Log("[AP] Plugin disabled by manager");
  return true;
}

APPLEPIE_PLUGIN_EXPORT bool AP_ReloadConfig() {
  LoadEiemConfig();
  s_apHotkeys[0].currentVK = g_guiToggleVK;
  Log("[AP] Config reloaded: gui_toggle_key=%d (%s)", g_guiToggleVK,
      EiemVKToString(g_guiToggleVK));
  return true;
}

APPLEPIE_PLUGIN_EXPORT int AP_GetHotkeys(AP_HotkeyInfo* outArray, int maxCount) {
  s_apHotkeys[0].currentVK = g_guiToggleVK;
  int count = sizeof(s_apHotkeys) / sizeof(s_apHotkeys[0]);
  if (count > maxCount) count = maxCount;
  for (int i = 0; i < count; i++) outArray[i] = s_apHotkeys[i];
  return count;
}

APPLEPIE_PLUGIN_EXPORT void AP_SetLanguage(const char* langCode) {
  Log("[AP] Language set by manager: %s", langCode ? langCode : "null");
}

static HANDLE g_initThread = nullptr;

BOOL APIENTRY DllMain(HMODULE hModule, DWORD reason, LPVOID reserved) {
  if (reason == DLL_PROCESS_ATTACH) {
    DisableThreadLibraryCalls(hModule);
    g_initThread = CreateThread(NULL, 0, InitThread, NULL, 0, NULL);
  } else if (reason == DLL_PROCESS_DETACH) {
    g_motionBackend.RequestProcessDetach();
    GhostRig_RequestProcessDetach();
    DirectVmdRuntime_RequestProcessDetach();
  }
  return TRUE;
}
