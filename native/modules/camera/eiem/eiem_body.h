#pragma once
// Narrow interface between the Camera module and the EIEM DirectVmd port.
// EIEM's code lives in its own translation units (eiem_slot*.cpp) so its
// globals and helper names never meet module.cpp.
//
// Up to kMaxActors characters dance at once; each actor index is an
// independent EIEM instance with its own clip, ghost rig and worker thread.
// Actor 0 is the controlled character.
//
// Threading: everything except Shutdown runs on the Unity main thread (the
// Camera module's TailLateTick or unscaled heartbeat). File I/O and sampling
// happen on each actor's worker.
#include "BetterEndfield/ModuleApi.h"

#include <cstdint>
#include <string>

namespace BetterEndfield::EiemBody {

constexpr int kMaxActors = 4;

// Cloth, hair and tail simulation (BeyondBoneCloth) during playback.
enum class ClothMode : int {
    Game = 0,   // left exactly as the game runs it
    Stable = 1, // EIEM's playback service: full simulation, no native pose pull
    Freeze = 2, // simulation off; cloth follows the body rigidly
};

struct Options {
    bool face = true;          // SkeletalMorphCore expressions from VMD morphs
    bool terrain = false;      // EIEM grounder terrain follow
    bool knee_mix = true;      // EIEM knee bend direction blend
    float knee_weight = -1.0f; // < 0 keeps EIEM's default
    float motion_scale = 1.0f; // EIEM displacement multiplier (0.05..5)
    ClothMode cloth = ClothMode::Stable;
};

enum class LoadState : int { Idle = 0, Loading = 1, Ready = 2, Failed = 3 };

// Resolves IL2CPP metadata and installs the shared hooks through the Host.
// Safe to call repeatedly; returns false (and logs why) when a required
// contract is missing. log_path names actor 0's log; actor N uses ".N+1".
bool Initialize(const BE_HostApiV1* host, const std::wstring& log_path);
bool Available();
// Brings up actor `actor` (worker + resolution) on first use.
bool EnsureActor(int actor);

void SetOptions(const Options& options);

// Worker-side load of a motion VMD plus an optional face (morph) VMD.
// Paths are UTF-8. Poll LoadStatus() for completion.
void Load(int actor, const std::string& motion_utf8, const std::string& face_utf8);
LoadState LoadStatus(int actor);
double DurationSeconds(int actor);

// The character an actor drives: entity (Beyond.Gameplay.Core.Entity), its
// Animator and MovementComponent. Pass nulls to clear.
void SetTarget(int actor, void* entity, void* animator, void* movement);

// Squad playback: every actor uses this world pose as the stage origin and
// keeps the positions authored in its VMD. valid=false restores EIEM's
// single-character placement (first frame lands on the character).
void SetSharedAnchor(bool valid, const float position[3], const float rotation[4]);

// Enter/leave EIEM's DirectVmd backend for one actor. Start creates the ghost
// rig on the next BipedIK solve of the target; Stop restores the Animator.
bool Start(int actor, const char* reason);
void Stop(int actor, const char* reason);
bool Active(int actor);

// EIEM's stage reference for a VMD camera that follows actor 0: the root
// anchor with first-frame placement applied, the character's motion scale
// (VMD units -> metres, from its leg length), its natural standing height and
// the terrain offset EIEM applies to the body.
struct CameraReference {
    float position[3]{};
    float rotation[4]{0, 0, 0, 1};
    float motion_scale = 0;
    float natural_height = 0;
    float offset[3]{};
};
bool GetCameraReference(int actor, CameraReference& reference);

// The director's clock: seconds on the shared timeline and whether it runs.
void PublishClock(double seconds, bool playing);

// Process shutdown on a non-game thread: stops the workers only.
void Shutdown();

} // namespace BetterEndfield::EiemBody
