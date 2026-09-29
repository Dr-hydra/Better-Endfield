#pragma once
// Test double for the EIEM DirectVmd port. The production eiem_body.cpp needs
// a live IL2CPP runtime; the camera tests only check how the adapter drives it.
#include "../../modules/camera/eiem/eiem_body.h"
#include <string>
#include <vector>

namespace EiemFake {
using BetterEndfield::EiemBody::kMaxActors;
using BetterEndfield::EiemBody::LoadState;
struct Actor {
    bool ready = false, active = false;
    LoadState load_state = LoadState::Idle;
    std::string motion, face;
    void* entity = nullptr;
    void* animator = nullptr;
    void* movement = nullptr;
    int starts = 0, stops = 0, loads = 0;
    double duration = 12.5;
};
inline bool available = true;
inline bool start_result = true;
inline int initialize_calls = 0, shutdowns = 0;
inline BetterEndfield::EiemBody::Options options;
inline Actor actors[kMaxActors];
inline bool anchor_valid = false;
inline float anchor_position[3]{};
inline bool camera_ok = false;
inline BetterEndfield::EiemBody::CameraReference camera;
inline double clock_seconds = -1;
inline bool clock_playing = false;
inline void SetAllLoadStates(LoadState state) {
    for (auto& actor : actors) if (actor.load_state == LoadState::Loading) actor.load_state = state;
}
} // namespace EiemFake

namespace BetterEndfield::EiemBody {
bool Initialize(const BE_HostApiV1*, const std::wstring&) {++EiemFake::initialize_calls;return EiemFake::available;}
bool Available() {return EiemFake::available;}
bool EnsureActor(int actor) {
    if (!EiemFake::available || actor < 0 || actor >= kMaxActors) return false;
    EiemFake::actors[actor].ready = true;
    return true;
}
void SetOptions(const Options& options) {EiemFake::options=options;}
void Load(int actor,const std::string& motion,const std::string& face) {
    auto& a=EiemFake::actors[actor];
    ++a.loads;a.motion=motion;a.face=face;a.load_state=LoadState::Loading;
}
LoadState LoadStatus(int actor) {return EiemFake::actors[actor].load_state;}
double DurationSeconds(int actor) {return EiemFake::actors[actor].duration;}
void SetTarget(int actor,void* entity,void* animator,void* movement) {
    auto& a=EiemFake::actors[actor];a.entity=entity;a.animator=animator;a.movement=movement;
}
void SetSharedAnchor(bool valid,const float position[3],const float[4]) {
    EiemFake::anchor_valid=valid;
    if(valid)for(int i=0;i<3;++i)EiemFake::anchor_position[i]=position[i];
}
bool Start(int actor,const char*) {
    auto& a=EiemFake::actors[actor];++a.starts;a.active=EiemFake::start_result;return EiemFake::start_result;
}
void Stop(int actor,const char*) {auto& a=EiemFake::actors[actor];++a.stops;a.active=false;}
bool Active(int actor) {return EiemFake::actors[actor].active;}
bool GetCameraReference(int actor,CameraReference& reference) {
    if(!EiemFake::camera_ok||!EiemFake::actors[actor].active)return false;
    reference=EiemFake::camera;return true;
}
void PublishClock(double seconds,bool playing) {EiemFake::clock_seconds=seconds;EiemFake::clock_playing=playing;}
void Shutdown() {++EiemFake::shutdowns;}
} // namespace BetterEndfield::EiemBody
