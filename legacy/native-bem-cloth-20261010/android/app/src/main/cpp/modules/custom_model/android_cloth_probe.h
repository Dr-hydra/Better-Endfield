#pragma once
#include "core/runtime.h"
#include <string>
#include <vector>
#include <memory>

namespace betterendfieldnext {
struct AndroidClothProbeRoot {
    void* game_object = nullptr;
    std::string character_id;
    std::string resource;
};
void ConfigureAndroidClothProbe(Il2CppRuntime& runtime);
// Called only from the existing Unity-thread model maintenance boundary.
// An explicit private-cache request is required. Live-pair mode owns a display
// clone; laboratory modes remain invisible. Original solvers are not written.
void PollAndroidClothProbe(const std::vector<AndroidClothProbeRoot>& roots);
// Unity-thread cleanup participates in the model shutdown acknowledgement.
bool ShutdownAndroidClothProbe();
// Read-only lineage alias; use only for identity matching, never rollback writes.
void* AndroidClothSourceMesh(void* mesh);
}
namespace BetterEndfieldNext::CustomModel {
// Internal core lease; shares the native unused-asset pin with saved Originals.
std::shared_ptr<void> PinModelMeshForCloth(void* mesh);
struct BemComponent;
// Builds an owned skinned Mesh through the loader's verified path with the
// supplied Matrix4x4[] palette (original bindposes plus physics joints).
void* BuildModelMeshForCloth(const BemComponent& component, void* source_mesh, void* bindposes);
}
