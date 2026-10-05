#pragma once
#include <cstring>
#include "cloth_bonecloth_profile.h"

namespace eiem_cloth_asset {
inline bool SourceLiinoPanel(const ClothBoneProfile &p) {
  return p.runtimeGenerated && p.signature && p.prefabSha &&
      !strcmp(p.signature,"10e2f518f5e7425e00f38fec9ce085b60e88014a513a310d422ffe299830a556") &&
      !strcmp(p.prefabSha,"9d7948269c79fb2ebf18dd5d9791740f67b00e6fe640c9f5994f49cffc515e71");
}
inline bool SourceSeraphPanel(const ClothBoneProfile &p) {
  return p.runtimeGenerated && p.signature && p.prefabSha &&
      !strcmp(p.signature,"6b76428721933a9570f28954f1064c28a88defd0fca8b156b996ce97b0388bd3") &&
      !strcmp(p.prefabSha,"d911c12a646d6f4c3f5068b63768c38b6a6a13c6fa2d58710c2b42ead69beda4");
}
inline bool SourceChenPanel(const ClothBoneProfile &p) {
  return p.runtimeGenerated && p.signature && p.prefabSha &&
      !strcmp(p.signature,"335d911e4023d9af7bac079f84ac54af629019d305c963b770244aa9a38b2bc0") &&
      !strcmp(p.prefabSha,"b17f6330b3fc9be83f5eb0c5f8bf98d634e3078844a2e68e0673f7636028fd29");
}
inline bool SourceChenWaist(const ClothBoneProfile &p,int n) {
  if(!SourceChenPanel(p)||!p.bones||p.boneCount!=33||p.rootCount!=8||p.depth!=4||
      (n!=0&&n!=4&&n!=9)||p.Passive(n)||p.Foreign(n))return false;
  const auto &b=p.bones[n],&child=p.bones[n+1];
  return b.attribute==0&&b.parent==-1&&b.column==-1&&b.depth==-1&&
      child.parent==n&&child.attribute==1&&child.depth==0;
}
inline bool SourcePanelFit(const ClothBoneProfile &p) {return SourceLiinoPanel(p)||SourceSeraphPanel(p)||SourceChenPanel(p);}
inline bool SourcePanelRelease(const ClothBoneProfile &p,int n) {
  if(SourceChenPanel(p))return (n==1||n==5||n==10)&&SourceChenWaist(p,n-1)&&!p.Passive(n)&&!p.Foreign(n);
  if(!SourceLiinoPanel(p)||!p.bones||p.boneCount!=28||p.rootCount!=6||p.depth!=4||
      (n!=3&&n!=16)||p.Passive(n)||p.Foreign(n))return false;
  const auto &b=p.bones[n];const int parent=n==3?0:15;
  return b.attribute==1&&b.depth==1&&b.parent==parent&&
      p.bones[parent].attribute==1&&p.bones[parent].depth==0&&p.bones[parent].parent<0;
}
inline int SourcePanelColumn(const ClothBoneProfile &p,int n) {
  return SourceChenWaist(p,n)?p.bones[n+1].column:p.bones[n].column;
}
inline int SourcePanelDepth(const ClothBoneProfile &p,int n) {
  if(SourceChenPanel(p)&&n>=0&&n<13&&n!=6)return p.bones[n].depth+1;
  return p.bones[n].depth;
}
inline int SourcePanelPromotedCount(const ClothBoneProfile &p) {return SourceChenPanel(p)?3:0;}
inline bool SourcePanelContract(const ClothBoneProfile &p) {
  if(!p.loop||!p.bones||p.depth!=4)return false;
  if(SourceLiinoPanel(p))return p.boneCount==28&&p.rootCount==6&&SourcePanelRelease(p,3)&&SourcePanelRelease(p,16);
  if(SourceChenPanel(p)) {
    if(p.boneCount!=33||p.rootCount!=8||!p.roots||!p.originalRoots||p.inputAnchorCount||p.sourceBranchCount)return false;
    constexpr int chains[8][4]{{0,1,2,3},{9,10,11,12},{4,5,7,8},{17,18,19,20},
        {25,26,27,28},{29,30,31,32},{21,22,23,24},{13,14,15,16}};
    for(int c=0;c<8;++c) {
      if(p.roots[c]!=chains[c][c<3?1:0])return false;
      int found=0;for(int k=0;k<8;++k)found+=p.originalRoots[k]==chains[c][0];if(found!=1)return false;
      for(int d=0;d<4;++d) {const int n=chains[c][d];const auto &b=p.bones[n];const int depth=d-(c<3?1:0);
        if(p.Passive(n)||p.Foreign(n)||b.parent!=(d?chains[c][d-1]:-1)||
            b.attribute!=(depth<0?0:depth==0?1:2)||b.depth!=depth||b.column!=(depth<0?-1:c))return false;
      }
    }
    const auto &bag=p.bones[6];return !p.Passive(6)&&!p.Foreign(6)&&bag.parent==5&&bag.attribute==0&&bag.column==-1&&bag.depth==-1;
  }
  if(!SourceSeraphPanel(p)||p.boneCount!=40||p.rootCount!=8||!p.prebuildId||strcmp(p.prebuildId,"3e358e5e"))return false;
  bool seen[8][4]{};int wrappers=0;
  for(int n=0;n<p.boneCount;++n){const auto &b=p.bones[n];if(p.Passive(n)||p.Foreign(n))return false;
    if(!b.attribute){if(b.parent>=0)return false;++wrappers;continue;}
    if(b.column<0||b.column>=8||b.depth<0||b.depth>=4||seen[b.column][b.depth]||b.attribute!=(b.depth?2:1)||b.parent<0||b.parent>=n)return false;
    seen[b.column][b.depth]=true;const auto &parent=p.bones[b.parent];
    if(b.depth?(parent.column!=b.column||parent.depth!=b.depth-1):(parent.attribute!=0))return false;
  }
  return wrappers==8;
}
}
