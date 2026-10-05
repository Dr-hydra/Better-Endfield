#pragma once
#include "cloth_bonecloth_profile.h"
#include <cstring>
namespace eiem_cloth_asset {
inline bool SourceShortSkin(const char *prefab,const char *component) {
  return prefab&&component&&!strcmp(prefab,"b43214b58fa424b28b4d03fa186c36ee847b917fcc842cef6a9f751ebd78c14e")&&
      !strcmp(component,"MC_SkirtShort");
}
inline bool SourceShortContract(const ClothBoneProfile &p) {
  if(!p.runtimeGenerated||!SourceShortSkin(p.prefabSha,p.component)||p.loop||!p.bones||!p.roots||!p.originalRoots||
      (p.boneCount!=12&&p.boneCount!=18)||p.rootCount!=4||p.depth!=2||p.inputAnchorCount||p.candidateIgnoredCount||p.ownershipCount)return false;
  if(p.boneCount==12&&p.sourceBranchCount)return false;
  if(p.boneCount==18){if(p.sourceBranchCount!=6||!p.sourceBranches||p.originalRoots[0]<0||p.originalRoots[0]>=12||!p.bones[p.originalRoots[0]].parentName)return false;
    const char *names[]{"L_shortskirtB_01_jnt","L_shortskirtB_02_jnt","L_shortskirtB_03_jnt","R_shortskirtB_01_jnt","R_shortskirtB_02_jnt","R_shortskirtB_03_jnt"};
    for(int k=0;k<6;++k){const int n=12+k;const auto &b=p.bones[n];
      if(p.sourceBranches[k]!=n||b.attribute||b.column!=-1||b.depth!=-1||b.parent!=(k%3?n-1:-1)||
          !b.name||!b.parentName||strcmp(b.name,names[k]))return false;
      if(k%3?strcmp(b.parentName,p.bones[n-1].name):strcmp(b.parentName,p.bones[p.originalRoots[0]].parentName))return false;}}
  for(int k=0;k<4;++k){const int fixed=p.roots[k];
    if(fixed<1||fixed>=12)return false;const auto &b=p.bones[fixed];const int root=b.parent;
    if(root<0||root>=fixed||b.attribute!=1||b.depth!=0||b.column!=k)return false;
    const auto &a=p.bones[root];if(a.attribute||a.parent!=-1||a.column!=-1||a.depth!=-1)return false;
    int children=0,tips=0;for(int n=0;n<12;++n){const auto &t=p.bones[n];children+=t.parent==root;
      if(t.parent==fixed){if(t.attribute!=2||t.depth!=1||t.column!=k)return false;++tips;}}
    if(children!=1||tips!=1)return false;
    int found=0;for(int c=0;c<4;++c)found+=p.originalRoots[c]==root;if(found!=1)return false;
  }
  return true;
}
inline bool SourceShortSides(const ClothBoneProfile &p) {return p.boneCount==18&&SourceShortContract(p);}
inline bool SourceShortRelease(const ClothBoneProfile &p,int n) {
  return SourceShortContract(p)&&n>=0&&n<p.boneCount&&(p.bones[n].attribute==1||(n>=12&&n%3!=0));
}
inline bool SourceShortWaist(const ClothBoneProfile &p,int n) {
  return SourceShortContract(p)&&n>=0&&n<p.boneCount&&(n>=12?n%3==0:p.bones[n].attribute==0);
}
inline int SourceShortColumn(const ClothBoneProfile &p,int n) {
  if(p.bones[n].attribute)return p.bones[n].column;
  for(int k=0;k<p.boneCount;++k)if(p.bones[k].parent==n)return p.bones[k].column;return -1;
}
inline int SourceShortDepth(const ClothBoneProfile &p,int n) {return n>=12?n%3:p.bones[n].attribute?p.bones[n].depth+1:0;}
inline bool SourceLongLegPanels(const ClothBoneProfile &p) {
  if(!p.runtimeGenerated||!SourceShortSkin(p.prefabSha,"MC_SkirtShort")||!p.component||strcmp(p.component,"MC_SkirtLong")||
      p.loop||!p.bones||!p.roots||p.boneCount!=24||p.rootCount!=6||p.depth!=4||p.ownershipCount||p.inputAnchorCount)return false;
  for(int c=0;c<6;++c)for(int d=0;d<4;++d){int found=-1;for(int n=0;n<24;++n)if(p.bones[n].column==c&&p.bones[n].depth==d){if(found>=0)return false;found=n;}
    if(found<0||p.bones[found].attribute!=(d?2:1))return false;
    if(!d){if(p.roots[c]!=found||p.bones[found].parent!=-1)return false;}
    else {const int parent=p.bones[found].parent;if(parent<0||parent>=24||p.bones[parent].column!=c||p.bones[parent].depth!=d-1)return false;}}
  return true;
}
}
