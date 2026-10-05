#pragma once
static bool ClothBoneForkCoat(const ClothBoneRuntime &s) {
  const auto p=s.profile;
  if(ClothBoneSeparatedCoatRecipe(s))return s.contactConsumer<0&&s.contactPartner<0&&!s.supportCreated;
  return p&&((p->runtimeForkCoat&&p->runtimeGenerated&&p->runtimeFixedForks&&p->nativeGraphOrder)||eiem_cloth_asset::SourceInactiveCoat(*p))&&
      p->nativeGraphCount>0&&!p->loop&&
      ((!p->generatedLocal&&!s.local.requested)||(s.local.requested&&s.local.recipe==p->generatedLocal&&
      p->generatedLocal&&p->generatedLocal->CoatWaistSkinOnly()&&eiem_cloth_asset::SourceInactiveCoat(*p)))&&
      !p->inputAnchorCount&&!p->sourceBranchCount&&
      s.contactConsumer<0&&s.contactPartner<0&&!s.supportCreated;
}
static bool ClothBoneLongPanelBending(const ClothBoneRuntime &s) {
  const auto p=s.profile;const auto r=s.local.recipe;
  return p&&s.local.requested&&r&&p->generatedLocal==r&&
      eiem_cloth_asset::SourceSeraphPanel(*p)&&eiem_cloth_asset::SourcePanelContract(*p)&&
      r->runtimeGenerated&&r->sourcePanelFit&&r->resampledPanel&&r->loop&&
      r->originalCount==40&&r->originalRoots==8&&r->depth==4&&r->addedCount==ClothLongPanelParticles&&r->rootCount==ClothLongPanelColumns&&
      r->bendingStiffness==0&&r->tetherStretch==ClothLongPanelTetherStretch&&
      r->distanceStiffness==ClothLongPanelDistanceStiffness&&
      !s.supportCreated&&s.oldLines.size()==32;
}
static bool ClothBoneSurfaceBendingRequested(const ClothBoneRuntime &s) {
  return s.profile&&(s.profile->runtimeForkCoat||s.profile->runtimeSeparatedCoat||s.profile->candidateAttributes||
      (s.local.requested&&s.local.recipe&&s.local.recipe->resampledPanel));
}
struct ClothBoneBendingParameters { int method=0;float stiffness=0; };
static bool ClothBoneBendingRead(void *box,ClothBoneBendingParameters &value,int &none) {
  constexpr auto type="BeyondDynamicBone.TriangleBendingConstraint.TriangleBendingConstraintParams";
  constexpr auto methodType="BeyondDynamicBone.TriangleBendingConstraint.Method";
  const auto f=box?CollisionFieldInfo(il2cpp_object_get_class(box),"triangleBendingConstraint",type):nullptr;
  const auto cls=f?il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;
  uint32_t align=0;
  if(!cls||!il2cpp_field_get_flags||(il2cpp_field_get_flags(f)&0x10)||
      il2cpp_class_value_size(cls,&align)!=sizeof(value))return false;
  const char *names[]{"method","stiffness"};const char *types[]{methodType,"System.Single"};
  for(int n=0;n<2;++n) {
    auto field=CollisionFieldInfo(cls,names[n],types[n]);
    auto nested=field?il2cpp_class_from_type(il2cpp_field_get_type(field)):nullptr;
    if(!field||(il2cpp_field_get_flags(field)&0x10)||!nested||il2cpp_class_value_size(nested,&align)!=4||
        ClothValueOffset(cls,names[n],types[n],sizeof(value),4)!=n*4)return false;
    if(!n&&!CollisionEnumValue(nested,"None",none))return false;
  }
  return ClothInputTeamField(box,"triangleBendingConstraint",type,value)&&
      std::isfinite(value.stiffness)&&value.stiffness>=0&&value.stiffness<=1;
}
static bool ClothBoneBendingFrom(void *data,ClothBoneBendingParameters &value,int &none) {
  void *box=nullptr;
  return data&&ClothInvoke(SurfaceMethod(il2cpp_object_get_class(data),"GetClothParameters",
      "BeyondDynamicBone.ClothParameters"),data,nullptr,box)&&ClothBoneBendingRead(box,value,none);
}
static bool ClothBoneSurfaceBendingConfigure(void *data,void *original) {
  if(!ClothOnMainThread())return false;
  auto &s=ClothBoneState();
  if(!ClothBoneSurfaceBendingRequested(s))return !s.surfaceBending&&!s.surfaceSourceBending;
  if((!ClothBoneForkCoat(s)&&!ClothBoneLongPanelBending(s))||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1||
      !ClothOwns(s.owner)||s.stopRequested||s.surfaceBending||s.surfaceSourceBending||
      !data||!original||data==original||data!=CollisionGc(s.candidateData)||original!=CollisionGc(s.data))return false;
  constexpr auto type="BeyondDynamicBone.TriangleBendingConstraint.SerializeData";
  void *source=nullptr,*copy=nullptr,*afterSource=nullptr;
  float stiffness=NAN,afterStiffness=NAN;int none=-1,afterNone=-2,actualNone=-3;
  ClothBoneBendingParameters before{},after{},actual{};
  if(!ClothField(original,"triangleBendingConstraint",type,source)||!source||
      !ClothField(source,"stiffness","System.Single",stiffness)||
      !ClothBoneBendingFrom(original,before,none)||before.stiffness!=stiffness||
      !SurfaceCloneField(data,original,"triangleBendingConstraint",type)||
      !ClothField(data,"triangleBendingConstraint",type,copy)||!copy||copy==source||
      !SurfaceScalar(copy,"stiffness","System.Single",0.0f)||
      !ClothBoneBendingFrom(data,actual,actualNone)||actualNone!=none||actual.method!=none||actual.stiffness!=0||
      !ClothField(original,"triangleBendingConstraint",type,afterSource)||afterSource!=source||
      !ClothField(source,"stiffness","System.Single",afterStiffness)||afterStiffness!=stiffness||
      !ClothBoneBendingFrom(original,after,afterNone)||afterNone!=none||
      after.method!=before.method||after.stiffness!=before.stiffness)return false;
  s.surfaceSourceBending=ClothBoneHold(source);s.surfaceBending=ClothBoneHold(copy);
  if(!s.surfaceSourceBending||!s.surfaceBending)return false;
  s.surfaceOriginalStiffness=stiffness;s.surfaceOriginalMethod=before.method;s.surfaceNoneMethod=none;
  if(ClothBoneForkCoat(s))Log("[CLOTH-BONE-COAT-POLICY] stage=configured component=%s frame=%d generation=%llu command=%u bodyCollision=Point originalStiffness=%g candidateStiffness=0 effectiveMethod=None sourceUntouched=1 originalRadiusCurve=1 originalSkin=%d waistSkinTransition=%d longitudinalShapeConstraints=retained nativeTeamReadback=pending visualVerified=0",
      s.profile->component,ClothFrame(),s.owner.generation,s.command,stiffness,
      !s.local.requested||(s.local.recipe&&s.local.recipe->NativeSkinRetained()),s.local.recipe&&s.local.recipe->CoatWaistSkinOnly());
  if(ClothBoneLongPanelBending(s))Log("[CLOTH-BONE-LONG-PANEL-POLICY] stage=configured component=%s frame=%d generation=%llu command=%u sourceTriangles=0 candidateTriangles=%d bodyCollision=Edge sourceStiffness=%g candidateStiffness=0 effectiveMethod=None sourceUntouched=1 contourMaterialReadback=pending sourceAngleRetained=1 colliderGeometryUnchanged=1 nativeTeamReadback=pending visualVerified=0",
      s.profile->component,ClothFrame(),s.owner.generation,s.command,ClothLongPanelFaces,stiffness);
  if(s.profile->runtimeSeparatedCoat)Log("[CLOTH-BONE-COAT-INPUT] stage=configured component=%s releasedInteriorFixed=%d retainedOriginalRoots=%d radiusCurveUnchanged=1 effectiveDepth=native-recomputed bodyGeometryUnchanged=1 skinWrites=0",
      s.profile->component,s.profile->releasedFixedCount,s.profile->rootCount);
  return true;
}
static bool ClothBoneSurfaceBendingMatches(const ClothBoneRuntime &s,int slot,void *box) {
  if(!ClothOnMainThread()||slot<0||slot>2)return false;
  if(!ClothBoneSurfaceBendingRequested(s))return !s.surfaceBending&&!s.surfaceSourceBending;
  if(!ClothBoneForkCoat(s)&&!ClothBoneLongPanelBending(s))return false;
  if(!s.surfaceBending||!s.surfaceSourceBending)return slot!=1;
  if(slot==1&&!ClothOwns(s.owner))return false;
  const auto data=CollisionGc(slot==1?s.candidateData:s.data);
  const auto expectedObject=CollisionGc(slot==1?s.surfaceBending:s.surfaceSourceBending);
  const float expected=slot==1?0:s.surfaceOriginalStiffness;
  void *object=nullptr;float serialized=NAN;int none=-1;ClothBoneBendingParameters actual{};
  return data&&expectedObject&&ClothField(data,"triangleBendingConstraint",
      "BeyondDynamicBone.TriangleBendingConstraint.SerializeData",object)&&object==expectedObject&&
      ClothField(object,"stiffness","System.Single",serialized)&&serialized==expected&&
      ClothBoneBendingRead(box,actual,none)&&none==s.surfaceNoneMethod&&actual.stiffness==expected&&
      actual.method==(slot==1?none:s.surfaceOriginalMethod);
}
