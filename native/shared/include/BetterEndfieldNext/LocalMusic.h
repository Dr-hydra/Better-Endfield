#pragma once
#include "ModuleApi.h"
// Optional named capability exported by BetterEndfieldNext.Music.dll. It plays one
// local WAV/MP3 file through the Music module's Wwise Audio Input channel, so a
// second module never hooks the same Wwise callbacks. While a track is open the
// OmniMix stream is not consumed and the game's own music stays paused.
// Every call is thread-safe and never touches Unity from the caller's thread.

enum {
    BE_LocalMusic_Idle = 0,
    BE_LocalMusic_Loading = 1,
    BE_LocalMusic_Ready = 2,   // decoded far enough to start, not playing
    BE_LocalMusic_Playing = 3,
    BE_LocalMusic_Paused = 4,
    BE_LocalMusic_Ended = 5,
    BE_LocalMusic_Error = 6,
};

typedef struct BE_LocalMusicStatusV1 {
    uint32_t size;               // caller sets sizeof(BE_LocalMusicStatusV1)
    int32_t state;               // BE_LocalMusic_*
    double position_seconds;     // audible position (consumed by Wwise)
    double duration_seconds;     // 0 while unknown
    uint32_t output_active;      // Wwise is consuming the channel
    uint32_t reserved;
    char error[128];             // UTF-8, empty unless state == Error
} BE_LocalMusicStatusV1;

typedef struct BE_LocalMusicApiV1 {
    uint32_t version; // 1
    uint32_t size;    // sizeof(BE_LocalMusicApiV1)
    // Opens a track, closing any other one. Returns a token, 0 on failure.
    uint64_t (BE_CALL* open)(const char* utf8_path, const char* owner);
    // Starts or resumes playback at position_seconds (< 0 keeps the position).
    int (BE_CALL* play)(uint64_t token, double position_seconds);
    int (BE_CALL* pause)(uint64_t token);
    int (BE_CALL* seek)(uint64_t token, double position_seconds);
    void (BE_CALL* close)(uint64_t token);
    int (BE_CALL* status)(uint64_t token, BE_LocalMusicStatusV1* out);
    int (BE_CALL* set_gain)(uint64_t token, float gain); // 0..2, linear
} BE_LocalMusicApiV1;
typedef const BE_LocalMusicApiV1* (BE_CALL* BE_GetLocalMusicApiV1Fn)(void);
