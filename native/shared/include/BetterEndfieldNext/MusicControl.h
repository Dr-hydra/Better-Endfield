#pragma once
#include "ModuleApi.h"

// Independent of LocalMusic playback ownership. A pause lease suppresses native
// BGM and OmniMix replacement; it does not open/replace any local audio track.
// Acquire/release only update thread-safe state. Game calls run in Music's pump.
typedef struct BE_MusicControlApiV1 {
    uint32_t version;
    uint32_t size;
    uint64_t (BE_CALL* acquire_bgm_pause)(const char* owner);
    void (BE_CALL* release_bgm_pause)(uint64_t token);
} BE_MusicControlApiV1;
typedef const BE_MusicControlApiV1* (BE_CALL* BE_GetMusicControlApiV1Fn)(void);
