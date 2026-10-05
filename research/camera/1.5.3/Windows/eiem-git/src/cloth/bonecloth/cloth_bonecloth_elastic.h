#pragma once
struct ClothBoneTetherParameters {float compression,stretch;};
using ClothBoneTetherConvertFn=void(__fastcall*)(ClothBoneTetherParameters*,void*,int,void*);
static ClothBoneTetherConvertFn s_clothTetherConvert=nullptr;
static void *s_clothTetherCallsite=nullptr;
static int s_clothTetherBoneType=-1;
static bool (*s_clothElasticInstaller)()=nullptr;
static bool ClothBoneElasticRequested(const ClothBoneRuntime &s) {
  return s.local.requested && s.local.recipe && s.local.recipe->tetherStretch>0;
}
static constexpr float ClothBoneSupportDistanceStiffness=.45f;
static constexpr float ClothBoneSupportTetherStretch=.15f;
static bool ClothBoneSupportElasticRequested(const ClothBoneRuntime &s) {
  return ClothPartnerSurfaceWaistResponse && !s.local.requested && s.supportCreated &&
      s.supportPointCollision && s.supportMotion && ClothBoneSurfacePartner(s);
}
static bool ClothBoneElasticPolicyValid(const ClothBoneLocalRecipe &r) {
  return !r.loop && r.rootSkinTransition && r.contactProducer &&
      std::isfinite(r.distanceStiffness) && r.distanceStiffness>0 && r.distanceStiffness<1 &&
      std::isfinite(r.tetherStretch) && r.tetherStretch>.03f && r.tetherStretch<=.75f &&
      std::isfinite(r.bendingStiffness) && r.bendingStiffness>0 && r.bendingStiffness<=1;
}
static bool ClothBoneElasticPolicyValid(const ClothBoneRuntime &s) {
  return s.local.recipe && (s.local.recipe->resampledPanel ? ClothBoneLongPanelBending(s) :
      ClothBoneElasticPolicyValid(*s.local.recipe));
}
static bool ClothBoneElasticAdjust(ClothBoneTetherParameters &value,float stretch,bool longPanel=false) {
  if(!std::isfinite(value.compression) || value.compression<0 || value.compression>1 ||
      !std::isfinite(value.stretch) || fabsf(value.stretch-.03f)>1e-6f ||
      !std::isfinite(stretch) || stretch<=.03f ||
      (longPanel ? stretch!=ClothLongPanelTetherStretch : stretch>.75f)) return false;
  value.stretch=stretch;return true;
}
static void ClothBoneElasticConvertAt(ClothBoneTetherParameters *result,void *data,int type,void *method,void *caller) {
  s_clothTetherConvert(result,data,type,method);
  if(!ClothOnMainThread() || !result || !data || caller!=s_clothTetherCallsite || type!=s_clothTetherBoneType) return;
  int found=-1;bool support=false;
  for(size_t n=0;n<s_clothBoneSlots.size();++n) {
    const auto &s=s_clothBoneSlots[n];
    if(!s.pending && !s.lease)continue;
    const bool inner=s.local.elasticTether && CollisionGc(s.local.elasticTether)==data;
    const bool outer=s.supportTether && CollisionGc(s.supportTether)==data;
    if(!inner && !outer)continue;
    if(found>=0 || inner==outer || !ClothOwns(s.owner) ||
        (inner && (!ClothBoneElasticRequested(s) || !ClothBoneElasticPolicyValid(s))) ||
        (outer && !ClothBoneSupportElasticRequested(s)))return;
    found=int(n);support=outer;
  }
  if(found<0)return;
  auto &s=s_clothBoneSlots[found];
  if(ClothBoneElasticAdjust(*result,support?ClothBoneSupportTetherStretch:s.local.recipe->tetherStretch,
      !support&&ClothBoneLongPanelBending(s))) {
    if(support)++s.supportElasticConversions;else ++s.local.elasticConversions;
  }
}
static void __fastcall ClothBoneElasticConvert(ClothBoneTetherParameters *result,void *data,int type,void *method) {
  ClothBoneElasticConvertAt(result,data,type,method,_ReturnAddress());
}
static bool ClothBoneElasticParameters(void *box,ClothBoneTetherParameters &tether,float &bend) {
  constexpr auto type="BeyondDynamicBone.TetherConstraint.TetherConstraintParams";
  constexpr auto bendType="BeyondDynamicBone.TriangleBendingConstraint.TriangleBendingConstraintParams";
  auto cls=box?il2cpp_object_get_class(box):nullptr;
  auto field=cls?CollisionFieldInfo(cls,"tetherConstraint",type):nullptr;
  auto nested=field?il2cpp_class_from_type(il2cpp_field_get_type(field)):nullptr;
  auto bf=cls?CollisionFieldInfo(cls,"triangleBendingConstraint",bendType):nullptr;
  auto bc=bf?il2cpp_class_from_type(il2cpp_field_get_type(bf)):nullptr;
  struct Bend {int method;float stiffness;} bending{};uint32_t align=0;
  if(!nested || !bc || il2cpp_class_value_size(nested,&align)!=sizeof(tether) ||
      il2cpp_class_value_size(bc,&align)!=sizeof(bending) ||
      ClothValueOffset(nested,"compressionLimit","System.Single",8,4)!=0 ||
      ClothValueOffset(nested,"stretchLimit","System.Single",8,4)!=4 ||
      ClothValueOffset(bc,"method","BeyondDynamicBone.TriangleBendingConstraint.Method",8,4)!=0 ||
      ClothValueOffset(bc,"stiffness","System.Single",8,4)!=4 ||
      !ClothInputTeamField(box,"tetherConstraint",type,tether) ||
      !ClothInputTeamField(box,"triangleBendingConstraint",bendType,bending)) return false;
  bend=bending.stiffness;
  return std::isfinite(tether.compression) && tether.compression>=0 && tether.compression<=1 &&
      std::isfinite(tether.stretch) && tether.stretch>=0 && std::isfinite(bend) && bend>=0 && bend<=1;
}
static bool ClothBoneElasticMatches(const ClothBoneRuntime &s,void *box) {
  ClothBoneTetherParameters actual{};float bend=0;
  return s.local.elasticTether && s.local.elasticConversions && ClothBoneElasticParameters(box,actual,bend) &&
      fabsf(actual.compression-s.local.elasticCompression)<1e-6f &&
      fabsf(actual.stretch-s.local.recipe->tetherStretch)<1e-6f &&
      fabsf(bend-s.local.recipe->bendingStiffness)<1e-6f;
}
static bool ClothBoneLongPanelMaterialMatches(const ClothBoneRuntime &s,int slot,void *box) {
  if(!s.local.requested||!s.local.recipe||!s.local.recipe->resampledPanel)return true;
  if(!ClothBoneLongPanelBending(s))return false;
  if(!s.local.elasticTether)return slot!=1;
  ClothBoneTetherParameters actual{};float bend=0,curve[16]{},attenuation=0;
  if(slot<0||slot>2||!ClothBoneElasticParameters(box,actual,bend)||
      !ClothBoneLocalDistanceParameters(box,curve,attenuation)||!std::isfinite(attenuation)||
      fabsf(actual.compression-s.local.elasticCompression)>1e-6f||
      fabsf(actual.stretch-(slot==1?ClothLongPanelTetherStretch:.03f))>1e-6f)return false;
  for(float v:curve)if(!std::isfinite(v)||fabsf(v-(slot==1?ClothLongPanelDistanceStiffness:1.f))>1e-6f)return false;
  return slot!=1||ClothBoneElasticMatches(s,box);
}
static bool ClothBoneElasticConfigure(void *data,void *original) {
  auto &s=ClothBoneState();auto &l=s.local;
  if(!ClothBoneElasticRequested(s))return true;
  constexpr auto type="BeyondDynamicBone.TetherConstraint.SerializeData";
  constexpr auto bendType="BeyondDynamicBone.TriangleBendingConstraint.SerializeData";
  void *oldTether=nullptr,*tether=nullptr,*oldBend=nullptr,*bend=nullptr;
  float compression=0,originalBend=0,after=0;
  if(!ClothOnMainThread() || !ClothOwns(s.owner) || !data || data==original ||
      !ClothBoneElasticPolicyValid(s) || !s_clothElasticInstaller || !s_clothElasticInstaller() ||
      !ClothField(original,"tetherConstraint",type,oldTether) || !oldTether ||
      !ClothField(oldTether,"distanceCompression","System.Single",compression) ||
      !std::isfinite(compression) || compression<0 || compression>1 ||
      !SurfaceCloneField(data,original,"tetherConstraint",type) ||
      !ClothField(data,"tetherConstraint",type,tether) || !tether || tether==oldTether ||
      !ClothField(original,"triangleBendingConstraint",bendType,oldBend) || !oldBend ||
      !ClothField(oldBend,"stiffness","System.Single",originalBend) || !std::isfinite(originalBend))return false;
  const bool longPanel=ClothBoneLongPanelBending(s);
  if((!longPanel && !SurfaceCloneField(data,original,"triangleBendingConstraint",bendType)) ||
      !ClothField(data,"triangleBendingConstraint",bendType,bend) || !bend || bend==oldBend ||
      (longPanel ? bend!=CollisionGc(s.surfaceBending) :
      !SurfaceScalar(bend,"stiffness","System.Single",l.recipe->bendingStiffness)) ||
      !ClothField(oldBend,"stiffness","System.Single",after) || after!=originalBend) return false;
  l.elasticTether=ClothBoneHold(tether);l.elasticCompression=compression;
  if(!l.elasticTether)return false;
  void *box=nullptr;
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(data),"GetClothParameters","BeyondDynamicBone.ClothParameters"),data,nullptr,box) ||
      !ClothBoneElasticMatches(s,box) || !ClothBoneLongPanelMaterialMatches(s,1,box) ||
      !ClothField(oldTether,"distanceCompression","System.Single",after) || after!=compression)return false;
  if(longPanel && (!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(original),"GetClothParameters",
      "BeyondDynamicBone.ClothParameters"),original,nullptr,box) || !ClothBoneLongPanelMaterialMatches(s,0,box)))return false;
  Log("[CLOTH-BONE-ELASTIC] stage=private-parameters-confirmed generation=%llu session=%llu sourceTether=%p privateTether=%p distance=%g stretchThreshold=%g sourceStretchThreshold=0.03 bending=%g sourceBending=%g compression=%g waistFixedPreserved=1 colliderGeometryUnchanged=1 TeamReadbackPending=1",
      s.owner.generation,s.owner.session,oldTether,tether,l.recipe->distanceStiffness,l.recipe->tetherStretch,
      l.recipe->bendingStiffness,originalBend,compression);
  return true;
}
static bool ClothBoneSupportElasticMatches(const ClothBoneRuntime &s,void *box) {
  if(!ClothOnMainThread())return false;
  if(s.contactConsumer<0||ClothBoneNativeLayerPartner(s))return !s.supportTether;
  void *tether=nullptr;ClothBoneTetherParameters actual{};float bending=0,curve[16]{},attenuation=0;
  if(!ClothOwns(s.owner) || !ClothBoneSupportElasticRequested(s) || !s.supportTether || !s.supportElasticConversions ||
      !ClothField(CollisionGc(s.candidateData),"tetherConstraint","BeyondDynamicBone.TetherConstraint.SerializeData",tether) ||
      tether!=CollisionGc(s.supportTether) || !ClothBoneElasticParameters(box,actual,bending) ||
      !ClothBoneLocalDistanceParameters(box,curve,attenuation) || !std::isfinite(attenuation) ||
      fabsf(actual.compression-s.supportCompression)>1e-6f || fabsf(actual.stretch-ClothBoneSupportTetherStretch)>1e-6f ||
      fabsf(bending-s.supportBending)>1e-6f || fabsf(attenuation-s.supportDistanceAttenuation)>1e-6f)return false;
  for(float v:curve)if(!std::isfinite(v) || fabsf(v-ClothBoneSupportDistanceStiffness)>1e-6f)return false;
  return true;
}
static bool ClothBoneSupportElasticConfigure(void *data,void *original) {
  if(!ClothOnMainThread())return false;
  auto &s=ClothBoneState();
  if(s.contactConsumer<0||ClothBoneNativeLayerPartner(s))return !s.supportTether;
  constexpr auto type="BeyondDynamicBone.TetherConstraint.SerializeData";
  void *source=nullptr,*copy=nullptr,*box=nullptr,*afterSource=nullptr;float compression=0,after=0;
  ClothBoneTetherParameters before{},afterParameters{};float beforeCurve[16]{},afterCurve[16]{},beforeAttenuation=0,afterAttenuation=0,bending=0,afterBending=0;
  if(!ClothOwns(s.owner) || !ClothBoneSupportElasticRequested(s) || s.supportTether || !data || data==original ||
      CollisionGc(s.candidateData)!=data || CollisionGc(s.data)!=original || !s_clothElasticInstaller || !s_clothElasticInstaller() ||
      !ClothField(original,"tetherConstraint",type,source) || !source ||
      !ClothField(source,"distanceCompression","System.Single",compression) || !std::isfinite(compression) || compression<0 || compression>1 ||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(original),"GetClothParameters","BeyondDynamicBone.ClothParameters"),original,nullptr,box) ||
      !ClothBoneElasticParameters(box,before,bending) || fabsf(before.stretch-.03f)>1e-6f || fabsf(before.compression-compression)>1e-6f ||
      !ClothBoneLocalDistanceParameters(box,beforeCurve,beforeAttenuation) || !std::isfinite(beforeAttenuation))return false;
  for(float v:beforeCurve)if(!std::isfinite(v) || v<=0 || v>1)return false;
  if(!SurfaceCloneField(data,original,"tetherConstraint",type) || !ClothField(data,"tetherConstraint",type,copy) || !copy || copy==source ||
      !ClothBoneLocalDistanceConfigure(data,original,ClothBoneSupportDistanceStiffness) || !(s.supportTether=ClothBoneHold(copy)))return false;
  s.supportCompression=compression;s.supportBending=bending;s.supportDistanceAttenuation=beforeAttenuation;
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(data),"GetClothParameters","BeyondDynamicBone.ClothParameters"),data,nullptr,box) ||
      !ClothBoneSupportElasticMatches(s,box) ||
      !ClothField(original,"tetherConstraint",type,afterSource) || afterSource!=source ||
      !ClothField(source,"distanceCompression","System.Single",after) || after!=compression ||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(original),"GetClothParameters","BeyondDynamicBone.ClothParameters"),original,nullptr,box) ||
      !ClothBoneElasticParameters(box,afterParameters,afterBending) ||
      !ClothBoneLocalDistanceParameters(box,afterCurve,afterAttenuation) ||
      before.compression!=afterParameters.compression || before.stretch!=afterParameters.stretch || bending!=afterBending || beforeAttenuation!=afterAttenuation)return false;
  for(int n=0;n<16;++n)if(beforeCurve[n]!=afterCurve[n])return false;
  Log("[CLOTH-BONE-SUPPORT-ELASTIC] stage=private-parameters-confirmed component=%s generation=%llu session=%llu sourceTether=%p privateTether=%p sourceDistance=%g distance=%g sourceStretchThreshold=%g stretchThreshold=%g compression=%g bending=%g velocityAttenuation=%g shapeReferenceUnchanged=1 sourceUntouched=1 TeamReadbackPending=1",
      s.profile->component,s.owner.generation,s.owner.session,source,copy,beforeCurve[0],ClothBoneSupportDistanceStiffness,
      before.stretch,ClothBoneSupportTetherStretch,compression,bending,beforeAttenuation);
  return true;
}
