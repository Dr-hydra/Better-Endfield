#pragma once
#include <array>
#include "cloth_bonecloth_limits.h"
constexpr int ClothBoneMaxSeparatedPanels=6;
struct ClothBoneAsset {
  const char *name,*parentName;
  int parent,attribute,column,depth;
  Vector3 position; Quaternion rotation; Vector3 scale;
};
struct ClothBoneBinding { const char *name,*parent; int cloth; float bind[16]; };
struct ClothBoneRendererAsset {
  const char *name,*mesh,*root;
  int vertices,submeshes;
  const ClothBoneBinding *bones; int boneCount;
  const char *parent=nullptr;
};
struct ClothBoneColliderAsset {
  const char *name,*parent;
  bool parentIsAnimator=false;
};
struct ClothBoneColliderGeometry {
  Vector3 center,size; int axis; bool reverse,separate,aligned;
};
struct ClothBoneColliderSource { const char *component; int index; };
struct ClothBoneNativeGraph {
  const std::array<int,3> *faces; int faceCount;
  const std::array<int,2> *lines; int lineCount;
};
struct ClothBoneOwnership { const char *component; int bone,attribute; bool zeroRotationAnchor=false; };
struct ClothBoneLocalRecipe;
namespace eiem_cloth_graph { struct OrderContract; }
struct ClothBoneProfile {
  const char *component,*signature,*prefabSha;
  bool loop; int depth;
  const ClothBoneAsset *bones; int boneCount;
  const int *roots,*originalRoots; int rootCount;
  const ClothBoneRendererAsset *renderers; int rendererCount;
  const ClothBoneColliderAsset *colliders; int colliderCount;
  const char *const *dependencies; int dependencyCount;
  const ClothBoneColliderSource *bodyColliderSources; int bodyColliderSourceCount;
  const int *candidateIgnored=nullptr; int candidateIgnoredCount=0;
  const ClothBoneNativeGraph *nativeGraphs=nullptr; int nativeGraphCount=0;
  const char *const *nativeProducers=nullptr; int nativeProducerCount=0;
  const ClothBoneColliderAsset *originalExcluded=nullptr; int originalExcludedCount=0;
  const ClothBoneLocalRecipe *generatedLocal=nullptr;
  bool runtimeGenerated=false;
  bool runtimeUnowned=false;
  float unownedRadius=0;
  const ClothBoneColliderGeometry *unownedGeometry=nullptr;
  bool ribbonSource=false;
  unsigned legRequiredMask=0,legListedMask=0;
  const char *prebuildId=nullptr;
  const ClothBoneColliderAsset *prebuildOmitted=nullptr;int prebuildOmittedCount=0;
  const int *inputAnchors=nullptr;int inputAnchorCount=0;
  bool InputAnchor(int n) const {for(int k=0;k<inputAnchorCount;++k)if(inputAnchors[k]==n)return true;return false;}
  const int *sourceBranches=nullptr;int sourceBranchCount=0;
  bool SourceBranch(int n) const {for(int k=0;k<sourceBranchCount;++k)if(sourceBranches[k]==n)return true;return false;}
  bool OutsideOriginal(int n) const {return InputAnchor(n)||SourceBranch(n);}
  int OriginalCount() const {return boneCount-inputAnchorCount-sourceBranchCount;}
  const ClothBoneColliderAsset *excludedBranches=nullptr; int excludedBranchCount=0;
  const int *foreignIgnored=nullptr; int foreignIgnoredCount=0;
  const ClothBoneOwnership *ownership=nullptr; int ownershipCount=0;
  bool runtimeFixedForks=false;
  bool runtimeForkCoat=false;
  bool runtimeSeparatedCoat=false;
  const int *releasedFixed=nullptr;int releasedFixedCount=0;
  bool ReleasedFixed(int n) const {for(int k=0;k<releasedFixedCount;++k)if(releasedFixed[k]==n)return true;return false;}
  bool runtimeBodyOnly=false;
  const eiem_cloth_graph::OrderContract *nativeGraphOrder=nullptr;
  const int *candidateAttributes=nullptr;
  int CandidateAttribute(int n) const {return candidateAttributes?candidateAttributes[n]:bones[n].attribute;}
  bool Foreign(int n) const {
    for(int k=0;k<foreignIgnoredCount;++k)if(foreignIgnored[k]==n)return true;return false;
  }
  bool Passive(int n) const {
    for(int k=0;k<candidateIgnoredCount;++k) if(candidateIgnored[k]==n) return true;
    return false;
  }
  int EffectiveCount() const {
    if(!nativeGraphCount) return rootCount*depth;
    int count=0;for(int n=0;n<boneCount;++n) if(CandidateAttribute(n) && !Passive(n)) ++count;return count;
  }
  int FaceCount() const { return nativeGraphCount?nativeGraphs[0].faceCount:(loop?rootCount:rootCount-1)*(depth-1)*2; }
  bool NativeLoopMode() const { return loop || rootCount==2; }
  bool CandidateAncestor(int n) const {
    if(n<0||n>=boneCount||bones[n].attribute) return false;
    for(int k=0;k<rootCount;++k) {
      int b=roots[k];
      for(int steps=0;b>=0&&b<boneCount&&steps<boneCount;++steps) {
        b=bones[b].parent;if(b==n)return true;
      }
    }
    return false;
  }
  bool ExcludedBranch(int root) const {
    if(!bones || boneCount<1 || boneCount>ClothBoneMaxIdentities || root<0 || root>=boneCount || bones[root].attribute!=0) return false;
    for(int n=0;n<boneCount;++n) {
      if(bones[n].parent < -1 || bones[n].parent>=n) return false;
      for(int p=n;p>=0;p=bones[p].parent)
        if(p==root && bones[n].attribute!=0) return false;
    }
    return true;
  }
};
