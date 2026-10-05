#pragma once
#include "cloth_bonecloth_profile.h"
#include <cstring>
namespace eiem_cloth_asset {
// Verified original coat: retain its authored collider set and skin. Additional
// calf contact is fitted from the matching installed lower-body asset.
inline bool SourceOriginalCoverageCoat(const char *sha,const char *component) {
  return sha&&component&&!strcmp(sha,"df16c2a933748015a9ed62661054c56acfe53f7893a93cf82a8472a946dea3fc")&&
      !strcmp(component,"MC_Endminm_Coat");
}
inline bool OriginalCoverageCoatEligible(bool source,unsigned required,unsigned listed,unsigned covered) {
  return source&&(required&3)!=0&&(listed&covered&3)==3&&(required&~covered&~12u)==0;
}
inline bool SourceForkCoatBody(const char *sha,const char *component) {
  return sha&&component&&!strcmp(sha,"4f8e69c8fc479d98e3a6df946e5c3d2e88cc6fc296669f6ffc5207ae1a63a314")&&!strcmp(component,"MC_Coat");
}
// Validate the untouched authored selection before preparing a separate
// candidate. Its hanging points move one joint upward; the waist stays free.
inline bool SourceUpperCoatFront(const ClothBoneProfile &p) {
  if(!SourceOriginalCoverageCoat(p.prefabSha,p.component)||!p.runtimeGenerated||p.loop||
      p.runtimeFixedForks||p.runtimeForkCoat||p.runtimeSeparatedCoat||p.candidateAttributes||
      !p.bones||p.boneCount!=47||p.rootCount!=7||p.depth!=6||!p.roots||!p.originalRoots||
      p.inputAnchorCount||p.sourceBranchCount||p.candidateIgnoredCount||p.foreignIgnoredCount||
      p.ownershipCount||p.prebuildId||p.releasedFixedCount)return false;
  constexpr int starts[]{0,7,14,20,27,34,41},ends[]{7,14,20,27,34,41,47};
  constexpr int fixed[]{2,9,15,21,29,36,42};
  const char *names[]{"clothes_pifeng_L_a_0_jnt","clothes_pifeng_L_b_0_jnt","clothes_pifeng_L_c_0_jnt",
      "clothes_pifeng_M_a_0_jnt","clothes_pifeng_R_a_0_jnt","clothes_pifeng_R_b_0_jnt","clothes_pifeng_R_c_0_jnt"};
  for(int c=0;c<7;++c) {
    const auto &root=p.bones[starts[c]];
    if(p.originalRoots[c]!=starts[c]||!root.name||strcmp(root.name,names[c])||
        !root.parentName||strcmp(root.parentName,"Bip001_Spine2"))return false;
    int matches=0;for(int k=0;k<7;++k)matches+=p.roots[k]==fixed[c];if(matches!=1)return false;
    for(int n=starts[c];n<ends[c];++n) {
      const auto &b=p.bones[n];const int attribute=n<fixed[c]?0:(n==fixed[c]||n==3||n==30)?1:2;
      if(b.parent!=(n==starts[c]?-1:n-1)||b.attribute!=attribute||
          b.depth!=(attribute?n-fixed[c]:-1)||b.column!=(attribute?p.bones[fixed[c]].column:-1))return false;
      if(n>starts[c]&&(!b.parentName||!p.bones[n-1].name||strcmp(b.parentName,p.bones[n-1].name)))return false;
    }
  }
  return p.bones[3].name&&p.bones[30].name&&
      !strcmp(p.bones[3].name,"clothes_pifeng_L_a_3_jnt")&&!strcmp(p.bones[30].name,"clothes_pifeng_R_a_3_jnt");
}
inline bool SourceUpperCoatRelease(const ClothBoneProfile &p,int n) {
  return n>=0&&n<p.boneCount&&SourceUpperCoatFront(p)&&p.bones[n].attribute==1;
}
inline int SourceUpperCoatRoot(const ClothBoneProfile &p,int n) {
  if(!SourceUpperCoatFront(p))return -1;
  for(int c=0;c<p.rootCount;++c)if(p.bones[p.roots[c]].parent==n)return c;
  return -1;
}
inline void SourceUpperCoatSelection(const ClothBoneProfile &p,int n,ClothBoneAsset &bone) {
  if(!SourceUpperCoatFront(p)||n<0||n>=p.boneCount)return;
  const int column=SourceUpperCoatRoot(p,n);
  if(column>=0){bone.attribute=1;bone.column=column;bone.depth=0;}
  else if(bone.attribute){bone.attribute=2;++bone.depth;}
}
inline int SourceUpperCoatPromoted(const ClothBoneProfile &p) {return SourceUpperCoatFront(p)?7:0;}
inline bool SourceSeparatedCoat(const ClothBoneProfile &p) {
  if(!p.runtimeSeparatedCoat||!p.runtimeGenerated||p.loop||p.runtimeFixedForks||p.runtimeForkCoat||p.candidateAttributes||
      !p.bones||p.boneCount<12||p.boneCount>128||p.rootCount<4||p.rootCount>12||p.depth<3||p.depth>16||
      !p.roots||!p.originalRoots||!p.releasedFixed||p.releasedFixedCount<1||p.releasedFixedCount>6||
      p.inputAnchorCount||p.sourceBranchCount||p.candidateIgnoredCount||p.ownershipCount||p.prebuildId||
      p.foreignIgnoredCount||p.originalExcludedCount||p.excludedBranchCount||p.prebuildOmittedCount||p.runtimeUnowned||p.runtimeBodyOnly||
      (p.legListedMask&3)!=3)return false;
  for(int k=0;k<p.releasedFixedCount;++k) {
    const int n=p.releasedFixed[k];if(n<0||n>=p.boneCount)return false;
    for(int j=0;j<k;++j)if(p.releasedFixed[j]==n)return false;
    const auto &b=p.bones[n];if(b.attribute!=1||b.depth!=1||b.parent<0||b.parent>=p.boneCount)return false;
    const auto &root=p.bones[b.parent];if(root.attribute!=1||root.depth||root.parent!=-1||root.column!=b.column)return false;
    bool declared=false;for(int c=0;c<p.rootCount;++c)declared|=p.originalRoots[c]==b.parent;if(!declared)return false;
    int children=0,rootChildren=0;
    for(int j=0;j<p.boneCount;++j){rootChildren+=p.bones[j].parent==b.parent;if(p.bones[j].parent==n){++children;
        if(p.bones[j].attribute!=2||p.bones[j].depth!=2||p.bones[j].column!=b.column)return false;}}
    if(children!=1||rootChildren!=1)return false;
  }
  return true;
}
inline constexpr const char *InactiveCoatPrefab="956b1a30c1af649b824758d47b397c8d29caacddf7faf0081bb861ce40e4c960";
inline constexpr const char *InactiveCoatSignature="8444f74cec8f46527eb829435f18dc3267cc9b4a8d4326256280f9234806b2c6";
inline bool SourceInactiveCoat(const ClothBoneProfile &p) {
  if(!p.runtimeGenerated||!p.candidateAttributes||p.runtimeFixedForks||p.runtimeForkCoat||p.loop||
      !p.bones||p.boneCount!=24||p.rootCount!=6||p.depth!=4||!p.roots||!p.originalRoots||
      p.inputAnchorCount||p.sourceBranchCount||p.candidateIgnoredCount||p.ownershipCount||
      !p.prefabSha||!p.signature||!p.component||strcmp(p.component,"MC_Lifeng_Coat")||
      strcmp(p.prefabSha,InactiveCoatPrefab)||strcmp(p.signature,InactiveCoatSignature))return false;
  const char *names[]{"L_skirtA_01_jnt","L_skirtB_01_jnt","L_skirtC_01_jnt","R_skirtA_01_jnt","R_skirtB_01_jnt","R_skirtC_01_jnt"};
  for(int c=0;c<6;++c) {
    if(p.originalRoots[c]!=c*4||!p.bones[c*4].name||strcmp(p.bones[c*4].name,names[c]))return false;
    for(int d=0;d<4;++d){const int n=c*4+d;const auto &b=p.bones[n];
      if(b.parent!=(d?n-1:-1)||b.attribute!=(d?(c>=4?0:2):1)||p.candidateAttributes[n]!=(d?2:1)||
          b.depth!=d||b.column<0||b.column>=6||p.roots[b.column]!=c*4||
          (!d&&(!b.parentName||strcmp(b.parentName,"Bip001_Spine1"))))return false;
    }
  }
  return true;
}
inline bool SourceForkCoatFront(const ClothBoneProfile &p) {
  if(!p.runtimeGenerated||!p.runtimeForkCoat||!p.runtimeFixedForks||p.loop||p.candidateAttributes||
      !p.bones||p.boneCount!=70||p.rootCount!=5||p.depth!=6||!p.roots||!p.originalRoots||
      !p.nativeGraphOrder||p.nativeGraphCount<1||p.inputAnchorCount||p.sourceBranchCount||
      !p.prefabSha||!p.signature||!p.component||strcmp(p.component,"MC_Coat")||
      strcmp(p.prefabSha,"4f8e69c8fc479d98e3a6df946e5c3d2e88cc6fc296669f6ffc5207ae1a63a314")||
      strcmp(p.signature,"79c9b9bda399520e61eca22681b4085262ade5e8672e814873a04229908c9131"))return false;
  const int roots[]{60,44,27,0,17};for(int k=0;k<5;++k)if(p.roots[k]!=roots[k])return false;
  int fixed=0,move=0;for(int n=0;n<70;++n){fixed+=p.bones[n].attribute==1;move+=p.bones[n].attribute==2;}
  if(fixed!=9||move!=27)return false;
  for(int n:{1,45}) {
    const auto &b=p.bones[n];
    if(p.Passive(n)||p.Foreign(n)||b.attribute!=1||b.depth!=1||b.parent!=n-1||p.bones[n-1].attribute!=1||
        p.bones[n-1].depth||p.bones[n-1].parent!=-1||!b.name||
        strcmp(b.name,n==1?"clothes_pifeng_R_b_1_jnt":"clothes_pifeng_L_b_1_jnt"))return false;
    for(int child:{n+1,n+5})if(p.bones[child].parent!=n||p.bones[child].attribute!=2)return false;
  }
  for(int n:{18,61}) {
    const auto &b=p.bones[n],&parent=p.bones[n-1],&child=p.bones[n+1];
    if(p.Passive(n)||p.Foreign(n)||b.attribute!=1||b.depth!=1||b.parent!=n-1||
        parent.attribute!=1||parent.depth!=0||parent.parent!=n-2||p.bones[n-2].attribute!=0||
        child.parent!=n||child.attribute!=2||child.depth!=2||parent.column!=b.column||child.column!=b.column||
        !b.name||strcmp(b.name,n==18?"clothes_pifeng_R_a_2_jnt":"clothes_pifeng_L_a_2_jnt"))return false;
    int children=0;for(int k=0;k<70;++k)if(p.bones[k].parent==n)++children;if(children!=1)return false;
  }
  return true;
}
inline bool SourceForkCoatRelease(const ClothBoneProfile &p,int n) {
  return (n==1||n==18||n==45||n==61)&&SourceForkCoatFront(p);
}
inline int SourceCoatInputs(const ClothBoneProfile &p,int (&out)[16]) {
  if(SourceUpperCoatFront(p)){int count=0;for(int n=0;n<p.boneCount;++n)
      if(SourceUpperCoatRelease(p,n)||SourceUpperCoatRoot(p,n)>=0)out[count++]=n;return count;}
  if(SourceSeparatedCoat(p)){for(int k=0;k<p.releasedFixedCount;++k)out[k]=p.releasedFixed[k];return p.releasedFixedCount;}
  if(SourceForkCoatFront(p)){out[0]=1;out[1]=18;out[2]=45;out[3]=61;return 4;}
  if(SourceInactiveCoat(p)){int count=0;for(int n=16;n<24;++n)if(n%4)out[count++]=n;return count;}
  return 0;
}
}
