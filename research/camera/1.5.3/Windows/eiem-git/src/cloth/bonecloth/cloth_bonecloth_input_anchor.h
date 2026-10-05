#pragma once
#include "cloth_bonecloth_panel_policy.h"
#include "cloth_bonecloth_coat_source.h"
static bool ClothBoneCaptureSourceBranches() {
  auto &s=ClothBoneState();const auto &p=*s.profile;if(!p.sourceBranchCount)return true;
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||!ClothOwns(s.owner)||s.stopRequested||!eiem_cloth_asset::SourceShortSides(p)||s.bones.size()!=18)return false;
  const int root=p.originalRoots[0];auto waist=ClothTarget(s.bones[root].parent);if(!waist)return false;
  for(int n=12;n<18;++n){const auto &a=p.bones[n];auto parent=a.parent<0?waist:ClothTarget(s.bones[a.parent].bone);int count=-1;void *t=nullptr;
    if(!parent||s.bones[n].bone.handle||!ClothValue(s_clothUnity.childCount,parent,count)||count<0||count>128)return false;
    for(int k=0;k<count;++k){void *child=nullptr,*args[]{&k};char name[128]{};
      if(!ClothInvoke(s_clothUnity.child,parent,args,child)||!child)return false;CollisionName(child,name,sizeof(name));
      if(!strcmp(name,a.name)){if(t)return false;t=child;}}
    ClothAnchor bind{};strncpy_s(bind.name,a.name,_TRUNCATE);strncpy_s(bind.parentName,a.parentName,_TRUNCATE);
    Vector3 live{},scale{};Quaternion q{};int children=-1;
    if(!t||!ClothAnchorUnderOwner(t)||CollisionParent(t)!=parent||!ClothValue(s_clothUnity.childCount,t,children)||children!=(n%3==2?0:1)||
        !ClothResolveAnchorBind(bind)||!ClothSameLocal(bind.bindPosition,bind.bindRotation,a.position,a.rotation)||
        !SurfaceVisiblePose(t,live,q,scale)||!SurfaceVisibleSame(scale,a.scale)||!eiem_collision::UniformPositive(CollisionV(scale)))return false;
    auto &r=s.bones[n];r.bone=ClothProtect(t);r.parent=ClothProtect(parent);r.local=bind.bindPosition;r.rotation=bind.bindRotation;
    const auto base=a.parent<0?s.bones[root].referenceScale:s.bones[a.parent].referenceScale;
    const auto divisor=a.parent<0?p.bones[root].scale:Vector3{1,1,1};
    r.referenceScale={base.x*a.scale.x/divisor.x,base.y*a.scale.y/divisor.y,base.z*a.scale.z/divisor.z};
    if(!r.bone.handle||!r.parent.handle||!eiem_collision::UniformPositive(CollisionV(r.referenceScale)))return false;
  }
  Log("[CLOTH-BONE-INPUT] sourceSideBranches=2 sourceSideBones=6 originalNativePoints=12 bind=unique-avatar-nonhuman peerOwnership=pending originalHierarchyWrites=0");return true;
}
static bool ClothBoneCaptureInputAnchors() {
  auto &s=ClothBoneState();const auto &p=*s.profile;
  if(!ClothBoneCaptureSourceBranches())return false;
  if(!p.inputAnchorCount)return true;
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||!eiem_cloth_asset::SourceApronFit(p)||s.bones.size()!=size_t(p.boneCount))return false;
  for(int k=0;k<p.inputAnchorCount;++k) {
    const int n=p.inputAnchors[k],child=p.originalRoots[k];
    if(n<0||n>=p.boneCount||child<0||child>=p.boneCount||p.bones[child].parent!=n||s.bones[n].bone.handle)return false;
    auto &r=s.bones[n];const auto &a=p.bones[n];
    auto t=ClothTarget(s.bones[child].parent),parent=CollisionParent(t),ct=ClothTarget(s.bones[child].bone);
    ClothAnchor bind{};CollisionName(t,bind.name,sizeof(bind.name));CollisionName(parent,bind.parentName,sizeof(bind.parentName));
    int children=-1,zero=0;void *only=nullptr,*args[]{&zero};Vector3 live{},scale{};Quaternion q{};
    if(!t||!parent||!ct||!ClothAnchorUnderOwner(t)||strcmp(bind.name,a.name)||strcmp(bind.parentName,a.parentName)||
        !ClothValue(s_clothUnity.childCount,t,children)||children!=1||!ClothInvoke(s_clothUnity.child,t,args,only)||only!=ct||
        !ClothResolveAnchorBind(bind)||!ClothSameLocal(bind.bindPosition,bind.bindRotation,a.position,a.rotation)||
        !SurfaceVisiblePose(t,live,q,scale)||!SurfaceVisibleSame(scale,a.scale)||!eiem_collision::UniformPositive(CollisionV(scale))) {
      Log("[CLOTH-BONE-INPUT] bone=%s child=%s captured=0 bindReason=%s originalUnchanged=1",a.name,p.bones[child].name,bind.bindReason);return false;
    }
    const auto childScale=p.bones[child].scale;const auto worldScale=s.bones[child].referenceScale;
    if(!eiem_collision::UniformPositive(CollisionV(childScale)))return false;
    r.referenceScale={worldScale.x/childScale.x,worldScale.y/childScale.y,worldScale.z/childScale.z};
    if(!eiem_collision::UniformPositive(CollisionV(r.referenceScale)))return false;
    r.bone=ClothProtect(t);r.parent=ClothProtect(parent);r.local=bind.bindPosition;r.rotation=bind.bindRotation;
    if(!r.bone.handle||!r.parent.handle)return false;
  }
  Log("[CLOTH-BONE-INPUT] originalNativePoints=%d inputAnchors=%d bind=unique-avatar-nonhuman source=verified-child-parent peerOwnership=pending hierarchyWrites=0",p.OriginalCount(),p.inputAnchorCount);
  return true;
}
static bool ClothBoneSeparatedCoatRecipe(const ClothBoneRuntime &s) {
  const auto p=s.profile;const auto r=s.local.recipe;
  return p&&eiem_cloth_asset::SourceSeparatedCoat(*p)&&s.local.requested&&r&&r==p->generatedLocal&&
      r->NativePanelsOnly()&&r->originalCount==p->boneCount&&r->originalRoots==p->rootCount&&
      !r->bodyCoverage&&!r->nativeLayer&&!r->sourcePanelFit&&!r->sourceApronFit&&!r->sourceShortSkin&&
      !r->meshCount&&s.local.layers.empty();
}
static bool ClothBoneCoatPoseSize(const ClothBoneRuntime &s) {
  if(!s.profile)return false;
  if(s.profile->runtimeSeparatedCoat)return ClothBoneSeparatedCoatRecipe(s)&&s.bones.size()==size_t(s.local.recipe->Total());
  return s.bones.size()==size_t(s.profile->boneCount);
}
static bool ClothBoneSaveInputLease(int slot) {
  auto &s=ClothBoneState();if(slot!=1)return true;
  int coatInputs[6]{};const int coatCount=s.profile?eiem_cloth_asset::SourceCoatInputs(*s.profile,coatInputs):0;
  if(coatCount) {
    if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||!ClothOwns(s.owner)||
        (s.local.requested&&(!s.local.recipe||(!s.local.recipe->CoatWaistSkinOnly()&&!ClothBoneSeparatedCoatRecipe(s))))||!ClothBoneCoatPoseSize(s))return false;
    for(int k=0;k<coatCount;++k)if(s.bones[coatInputs[k]].inputCaptured)return false;
    for(int k=0;k<coatCount;++k){auto &b=s.bones[coatInputs[k]];b.inputPosition=b.savedPosition;b.inputRotation=b.savedRotation;b.inputScale=b.savedScale;b.inputCaptured=true;}
    Log("[CLOTH-BONE-COAT-INPUT] stage=captured component=%s frame=%d generation=%llu command=%u sourceInputs=%d hierarchyAndSkinWrites=0 returnPose=boundary-snapshot",
        s.profile->component,ClothFrame(),s.owner.generation,s.command,coatCount);
    return true;
  }
  const bool chen=s.profile&&s.local.recipe&&s.local.recipe->sourcePanelFit&&eiem_cloth_asset::SourceChenPanel(*s.profile);
  if(s.local.recipe&&(s.local.recipe->sourceShortSkin||chen)) {
    if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||!s.profile||
        !(chen?eiem_cloth_asset::SourcePanelContract(*s.profile):eiem_cloth_asset::SourceShortContract(*s.profile))||s.bones.size()<size_t(s.profile->boneCount))return false;
    const auto waist=[&](int n){return chen?eiem_cloth_asset::SourceChenWaist(*s.profile,n):eiem_cloth_asset::SourceShortWaist(*s.profile,n);};
    const auto released=[&](int n){return chen?eiem_cloth_asset::SourcePanelRelease(*s.profile,n):eiem_cloth_asset::SourceShortRelease(*s.profile,n);};
    for(int n=0;n<s.profile->boneCount;++n)if(waist(n)||released(n)) {
      auto &b=s.bones[n];if(b.inputCaptured)return false;
      if(waist(n)) {
        ClothAnchor bind{};strncpy_s(bind.name,s.profile->bones[n].name,_TRUNCATE);strncpy_s(bind.parentName,s.profile->bones[n].parentName,_TRUNCATE);
        if(!ClothResolveAnchorBind(bind)||!ClothSameLocal(bind.bindPosition,bind.bindRotation,b.local,b.rotation)){
          Log("[CLOTH-BONE-INPUT] sourceWaist=%s captured=0 bindReason=%s naturalReferenceConfirmed=0",bind.name,bind.bindReason);return false;}
      }
    }
    for(int n=0;n<s.profile->boneCount;++n)if(waist(n)||released(n)) {
      auto &b=s.bones[n];b.inputPosition=b.savedPosition;b.inputRotation=b.savedRotation;b.inputScale=b.savedScale;b.inputCaptured=true;
      if(waist(n)){b.savedPosition=b.local;b.savedRotation=b.rotation;}
    }
    if(chen)Log("[CLOTH-BONE-PANEL-INPUT] stage=captured sourceWaists=3 releasedUpperPanel=3 originalIdentities=33 avatarBindConfirmed=1 originalHierarchyWrites=0 returnPose=boundary-snapshot");
    return true;
  }
  if(!s.profile->inputAnchorCount)return true;
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||!eiem_cloth_asset::SourceApronFit(*s.profile))return false;
  for(int k=0;k<s.profile->inputAnchorCount;++k) {
    const int n=s.profile->inputAnchors[k];
    if(n<0||size_t(n)>=s.bones.size()||s.bones[n].inputCaptured)return false;
  }
  for(int k=0;k<s.profile->inputAnchorCount;++k) {
    auto &b=s.bones[s.profile->inputAnchors[k]];
    b.inputPosition=b.savedPosition;b.inputRotation=b.savedRotation;b.inputScale=b.savedScale;b.inputCaptured=true;
  }
  return true;
}
