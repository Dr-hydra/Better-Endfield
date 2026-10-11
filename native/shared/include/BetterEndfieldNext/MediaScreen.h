#pragma once
#include "ModuleApi.h"
BE_EXPORT void BE_CALL BetterEndfieldNext_MediaScreenForegroundV1(int visible);
// Commands only enqueue work. Texture/scene operations run on the Unity frame.
// 0 toggle, 1 stop, 2 pause/resume, 3 seek (seconds).
BE_EXPORT int BE_CALL BetterEndfieldNext_MediaScreenCommandV1(unsigned action, double value);
// Copies a UTF-8 status string; never returns a pointer into mutable storage.
BE_EXPORT int BE_CALL BetterEndfieldNext_MediaScreenStatusV1(char* destination, size_t capacity);
