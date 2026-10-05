#pragma once
template<size_t N> static int ClothBonePanelReturnTargets(const ClothBoneRuntime &s,std::array<int,N> &indices) {
  int coatInputs[6]{};const int coatCount=s.profile?eiem_cloth_asset::SourceCoatInputs(*s.profile,coatInputs):0;
  if(coatCount) {
    if(N<size_t(coatCount)||!ClothBoneCoatPoseSize(s))return -1;
    for(int k=0;k<coatCount;++k)indices[k]=coatInputs[k];return coatCount;
  }
  if(!s.profile || !s.profile->bones || !s.local.recipe || s.bones.size()<size_t(s.profile->boneCount))return -1;
  const auto &p=*s.profile;const auto &r=*s.local.recipe;
  if(r.sourceShortSkin?!eiem_cloth_asset::SourceShortContract(p):r.sourceApronFit?!eiem_cloth_asset::SourceApronFit(p):!eiem_cloth_asset::SourcePanelContract(p))return -1;
  int count=0;
  for(int n=0;n<p.boneCount;++n) {
    const bool leased=r.sourceShortSkin?(eiem_cloth_asset::SourceShortWaist(p,n)||eiem_cloth_asset::SourceShortRelease(p,n)):r.sourceApronFit?(p.InputAnchor(n)||eiem_cloth_asset::SourceApronRelease(p,n)):
        (eiem_cloth_asset::SourceChenWaist(p,n)||eiem_cloth_asset::SourcePanelRelease(p,n));
    if(!leased)continue;
    if(count==int(indices.size()))return -1;
    indices[count++]=n;
  }
  return count;
}
static bool ClothBonePanelReturnMember(const ClothBoneRuntime &s,int n) {
  if(!s.profile||n<0||s.bones.size()<=size_t(n))return false;
  int coatInputs[6]{};const int coatCount=eiem_cloth_asset::SourceCoatInputs(*s.profile,coatInputs);
  if(coatCount){for(int k=0;k<coatCount;++k)if(n==coatInputs[k])return s.bones[n].inputCaptured;return false;}
  if(!s.local.recipe)return false;
  if(s.local.recipe->sourceShortSkin)return (eiem_cloth_asset::SourceShortWaist(*s.profile,n)||eiem_cloth_asset::SourceShortRelease(*s.profile,n))&&s.bones[n].inputCaptured;
  if(s.local.recipe->sourceApronFit)return eiem_cloth_asset::SourceApronFit(*s.profile)&&
      (s.profile->InputAnchor(n)?s.bones[n].inputCaptured:eiem_cloth_asset::SourceApronRelease(*s.profile,n));
  if(eiem_cloth_asset::SourceChenPanel(*s.profile))return (eiem_cloth_asset::SourceChenWaist(*s.profile,n)||eiem_cloth_asset::SourcePanelRelease(*s.profile,n))&&s.bones[n].inputCaptured;
  return eiem_cloth_asset::SourcePanelRelease(*s.profile,n);
}
static bool ClothBoneCapturePanelReturn() {
  auto &s=ClothBoneState();auto &l=s.local;
  int coatInputs[6]{};const int coat=s.profile?eiem_cloth_asset::SourceCoatInputs(*s.profile,coatInputs):0;
  if((!coat&&(!l.created||!l.recipe||!l.recipe->SourcePoseLease()))||!s.tx.candidate.issued||l.panelReturnCaptured)return true;
  if(coat){if(!ClothBoneCoatPoseSize(s))return false;bool captured=false;for(int k=0;k<coat;++k)captured|=s.bones[coatInputs[k]].inputCaptured;if(!captured)return true;}
  else if((l.recipe->sourceApronFit||l.recipe->sourceShortSkin||(s.profile&&eiem_cloth_asset::SourceChenPanel(*s.profile)))&&!s.bones.empty()&&!s.bones[0].inputCaptured)return true;
  std::array<int,16> indices{};const int count=ClothBonePanelReturnTargets(s,indices);
  if(count<=0)return count==0;
  using Phase=eiem_cloth_rebuild::Phase;
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||!s.profile||
      (s.tx.phase!=Phase::RetireCandidate&&!(s.tx.phase==Phase::Retained&&s.tx.retainedFrom==Phase::RetireCandidate))||s.tx.disposeCandidateIssued)return false;
  for(int k=0;k<count;++k){const int n=indices[k];
    if(!ClothBonePanelReturnMember(s,n))return false;
    const auto &b=s.bones[n];auto t=ClothTarget(b.bone);
    if(!t||!ClothTarget(b.parent)||CollisionParent(t)!=ClothTarget(b.parent)||
        !SurfaceVisiblePose(t,l.panelReturnPosition[k],l.panelReturnRotation[k],l.panelReturnScale[k]))return false;
    l.panelReturnIds[k]={b.bone.id.instance,b.parent.id.instance};
  }
  l.panelReturnCount=count;l.panelReturnIndices=indices;
  l.panelReturnOwner=s.owner;l.panelReturnCommand=s.command;l.panelReturnCaptured=true;return true;
}
static bool ClothBonePlanPanelReturn(int slot) {
  auto &s=ClothBoneState();auto &l=s.local;
  int coatInputs[6]{};const int coat=s.profile?eiem_cloth_asset::SourceCoatInputs(*s.profile,coatInputs):0;
  if(slot!=2||(!coat&&(!l.created||!l.recipe||!l.recipe->SourcePoseLease())))return true;
  if(!s.tx.candidate.issued||!s.tx.disposeCandidateIssued)return true;
  if(coat){if(!ClothBoneCoatPoseSize(s))return false;bool captured=false;for(int k=0;k<coat;++k)captured|=s.bones[coatInputs[k]].inputCaptured;if(!captured)return true;}
  else if((l.recipe->sourceApronFit||l.recipe->sourceShortSkin||(s.profile&&eiem_cloth_asset::SourceChenPanel(*s.profile)))&&!s.bones.empty()&&!s.bones[0].inputCaptured)return true;
  std::array<int,16> indices{};const int count=ClothBonePanelReturnTargets(s,indices);
  if(count<=0)return count==0;
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||s.tx.phase!=eiem_cloth_rebuild::Phase::BuildOriginal||
      !l.panelReturnCaptured||!(l.panelReturnOwner==s.owner)||l.panelReturnCommand!=s.command||!s.profile||
      l.panelReturnCount!=count||l.panelReturnIndices!=indices)return false;
  bool restore[16]{};
  for(int k=0;k<count;++k){const int n=indices[k];
    if(!ClothBonePanelReturnMember(s,n))return false;
    const auto &b=s.bones[n];auto t=ClothTarget(b.bone);
    if(!t||!ClothTarget(b.parent)||CollisionParent(t)!=ClothTarget(b.parent)||
        l.panelReturnIds[k]!=std::array<int,2>{b.bone.id.instance,b.parent.id.instance})return false;
    restore[k]=ClothSameLocal(b.savedPosition,b.savedRotation,l.panelReturnPosition[k],l.panelReturnRotation[k])&&
        SurfaceVisibleSame(b.savedScale,l.panelReturnScale[k]);
  }
  int restored=0;
  for(int k=0;k<count;++k)if(restore[k]){const int n=indices[k];auto &b=s.bones[n];++restored;
    if(coat||s.profile->InputAnchor(n)||l.recipe->sourceShortSkin||eiem_cloth_asset::SourceChenPanel(*s.profile)){b.savedPosition=b.inputPosition;b.savedRotation=b.inputRotation;b.savedScale=b.inputScale;}
    else {b.savedPosition=b.local;b.savedRotation=b.rotation;}}
  Log("[CLOTH-AUTO] stage=source-panel-return-plan restoredAttachmentTargets=%d externalPoseRetained=%d inputAnchors=%d candidateRetired=1 UnityWrite=reference-scope-finally",
      restored,count-restored,s.profile->inputAnchorCount);
  return true;
}
