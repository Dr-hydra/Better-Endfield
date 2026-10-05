#pragma once
#include "cloth_bonecloth_profile.h"
#include <cstring>
namespace eiem_cloth_asset {
constexpr const char *ApronPrefab="b8f4227deb946c5705324ba70d7bbf0d763b109c4e394ad76b71edcb562331c8";
constexpr const char *ApronSignature="329ff7d23433e6c5b0a312f6d9695f80a70d082a871b36ad2a99dea10b347565";
inline bool SourceApronFit(const ClothBoneProfile &p) {
  return p.runtimeGenerated&&p.signature&&!strcmp(p.signature,ApronSignature)&&p.prefabSha&&!strcmp(p.prefabSha,ApronPrefab)&&p.component&&!strcmp(p.component,"MC_frontSkirt")&&
      p.boneCount==6&&p.rootCount==2&&p.depth==3&&!p.loop&&p.inputAnchorCount==2&&p.inputAnchors&&p.inputAnchors[0]==0&&p.inputAnchors[1]==3&&
      p.roots&&p.roots[0]==0&&p.roots[1]==3&&p.originalRoots&&p.originalRoots[0]==1&&p.originalRoots[1]==4;
}
inline bool SourceApronRelease(const ClothBoneProfile &p,int n) {
  if(!SourceApronFit(p)||!p.bones||(n!=1&&n!=4)||p.Foreign(n)||p.Passive(n))return false;
  return p.bones[n].attribute==1&&p.bones[n].parent==n-1&&p.bones[n].depth==1&&p.bones[n-1].attribute==1&&p.bones[n-1].parent<0;
}
}
