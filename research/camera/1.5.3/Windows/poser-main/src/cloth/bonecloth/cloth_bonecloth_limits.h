#pragma once
#include <cstddef>
constexpr int ClothBoneMaxParticles=256,ClothBoneMaxIdentities=320;
constexpr int ClothBoneMaxFaces=512,ClothBoneMaxEdges=768;
constexpr int ClothBoneMaxFaceChoices=2*ClothBoneMaxFaces;
inline bool ClothBoneIdentityBudget(size_t identities,size_t active) {
  return identities>0&&identities<=ClothBoneMaxIdentities&&active>0&&
      active<=ClothBoneMaxParticles&&active<=identities;
}
