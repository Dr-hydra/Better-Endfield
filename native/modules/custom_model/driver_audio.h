#pragma once
// Driver sound effects for BEM 1.5 dynamic drivers.
//
// This backend is deliberately independent of the game's Wwise graph and of
// BetterEndfieldNext.Music. Voices are mixed by the OS (WASAPI shared mode on
// Windows, MediaPlayer on Android), so the game keeps playing every one of its
// own sounds, including BGM. Audio is always optional: a missing device or a
// bad file must never affect the model or the animation clock.
#include <cstdint>
#include <functional>
#include <string>

namespace BetterEndfieldNext::CustomModel {

struct DriverSoundSpec {
    std::string path;     // decoded audio file on disk (wav/mp3/m4a/flac/ogg)
    float gain = 1.0f;    // linear, clamped to [0,2]
    bool loop = true;
    double offset_seconds = 0.0;
};

// Starts or retargets the looping voice identified by `key`. Starting an
// already-playing key with the same path is a no-op, so callers may invoke this
// every frame without restarting the sound.
bool StartDriverSound(const std::string& key, const DriverSoundSpec& spec);
void StopDriverSound(const std::string& key);
void StopAllDriverSounds();
// True when the platform backend initialised and can accept voices.
bool DriverAudioAvailable();
// Routes backend diagnostics through the module logger. Optional.
void SetDriverAudioLogger(std::function<void(const std::string&)> sink);
// One-shot diagnostic snapshot for the log; empty when nothing is playing.
std::string DriverAudioStatus();

#if defined(__ANDROID__)
// Called once from JNI_OnLoad after the bridge class is resolved. The arguments
// are JavaVM*, JNIEnv* and jclass; they stay void* here so this header never has
// to pull in <jni.h> for the Windows build.
bool InitializeAndroidDriverAudio(void* vm, void* env, void* bridge_class);
#endif

}  // namespace BetterEndfieldNext::CustomModel
