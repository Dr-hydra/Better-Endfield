#pragma once

#include <cstdint>

#define APPLEPIE_PLUGIN_API_VERSION 1

#ifdef APPLEPIE_PLUGIN_IMPL
  #define APPLEPIE_PLUGIN_EXPORT extern "C" __declspec(dllexport)
#else
  #define APPLEPIE_PLUGIN_EXPORT extern "C" __declspec(dllimport)
#endif


struct AP_PluginInfo {
    int         apiVersion;
    const char* id;
    const char* displayName;
    const char* description;
    const char* configFile;
    bool        supportsHotDisable;
};

struct AP_HotkeyInfo {
    const char* name;
    const char* configKey;
    int         currentVK;
};


typedef AP_PluginInfo*  (*pfn_AP_GetPluginInfo)();

typedef bool            (*pfn_AP_PluginEnable)();

typedef bool            (*pfn_AP_PluginDisable)();

typedef bool            (*pfn_AP_ReloadConfig)();

typedef int             (*pfn_AP_GetHotkeys)(AP_HotkeyInfo* outArray, int maxCount);

typedef void            (*pfn_AP_SetLanguage)(const char* langCode);
