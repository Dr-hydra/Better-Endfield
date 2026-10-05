#pragma once
#include "cloth_bonecloth_panel_policy.h"
static bool ClothBoneApronPointBody(const ClothBoneRuntime &s) {
  const auto r=s.local.recipe;
  return s.local.requested && s.local.created && r && r->runtimeGenerated && r->sourceApronFit &&
      s.profile && s.profile->generatedLocal==r && eiem_cloth_asset::SourceApronFit(*s.profile) &&
      eiem_cloth_asset::SourceApronRelease(*s.profile,1) && eiem_cloth_asset::SourceApronRelease(*s.profile,4) &&
      r->originalCount==6 && r->addedCount==6 && r->rootCount==4 && !r->loop;
}
static bool ClothBonePanelPointBody(const ClothBoneRuntime &s) {
  const auto r=s.local.recipe;
  return s.local.requested&&s.local.created&&r&&r->runtimeGenerated&&r->sourcePanelFit&&
      s.profile&&s.profile->generatedLocal==r&&eiem_cloth_asset::SourceChenPanel(*s.profile)&&
      eiem_cloth_asset::SourcePanelContract(*s.profile)&&r->originalCount==33&&r->addedCount==64&&
      r->rootCount==24&&r->depth==4&&r->loop&&!r->resampledPanel;
}
static bool ClothBonePointBody(const ClothBoneRuntime &s) {
  return s.supportPointCollision || ClothBoneApronPointBody(s) || ClothBonePanelPointBody(s) || ClothBoneForkCoat(s) ||
      (s.profile&&s.profile->runtimeBodyOnly&&s.local.requested&&s.local.recipe&&s.local.recipe->NativeBodyOnly());
}
static const char *ClothBoneBodyMode(const ClothBoneRuntime &s) {
  return ClothBonePointBody(s)?"Point":"Edge";
}
static int ClothBoneExpectedBodyMode(const ClothBoneRuntime &s,int slot,int point,int edge) {
  return slot!=1 || ClothBonePointBody(s)?point:edge;
}
static bool ClothBoneSupportMotionParameters(void *box,ClothBoneMotionParameters &value) {
  constexpr auto type="BeyondDynamicBone.MotionConstraint.MotionConstraintParams";
  auto f=box?CollisionFieldInfo(il2cpp_object_get_class(box),"motionConstraint",type):nullptr;
  auto cls=f?il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;uint32_t align=0;
  if(!cls || !il2cpp_field_get_flags || (il2cpp_field_get_flags(f)&0x10) ||
      il2cpp_class_value_size(cls,&align)!=sizeof(value))return false;
  const char *fields[]{"useMaxDistance","maxDistanceCurveData","useBackstop","backstopRadius","backstopDistanceCurveData","stiffness"};
  const char *types[]{"System.Boolean","Unity.Mathematics.float4x4","System.Boolean","System.Single","Unity.Mathematics.float4x4","System.Single"};
  const size_t offsets[]{offsetof(ClothBoneMotionParameters,useMaxDistance),offsetof(ClothBoneMotionParameters,maxDistance),
      offsetof(ClothBoneMotionParameters,useBackstop),offsetof(ClothBoneMotionParameters,backstopRadius),
      offsetof(ClothBoneMotionParameters,backstopDistance),offsetof(ClothBoneMotionParameters,stiffness)};
  const int widths[]{1,64,1,4,64,4};
  for(int n=0;n<6;++n) {
    auto field=CollisionFieldInfo(cls,fields[n],types[n]);
    auto nested=field?il2cpp_class_from_type(il2cpp_field_get_type(field)):nullptr;
    if(!field || (il2cpp_field_get_flags(field)&0x10) || !nested || il2cpp_class_value_size(nested,&align)!=widths[n] ||
        ClothValueOffset(cls,fields[n],types[n],sizeof(value),widths[n])!=int(offsets[n]))return false;
  }
  if(!ClothInputTeamField(box,"motionConstraint",type,value) || value.useMaxDistance>1 || value.useBackstop>1 ||
      !std::isfinite(value.backstopRadius) || !std::isfinite(value.stiffness))return false;
  for(int n=0;n<16;++n)if(!std::isfinite(value.maxDistance[n]) || !std::isfinite(value.backstopDistance[n]))return false;
  return true;
}
static bool ClothBoneSupportMotionSame(const ClothBoneMotionParameters &a,const ClothBoneMotionParameters &b) {
  if(a.useMaxDistance!=b.useMaxDistance || a.useBackstop!=b.useBackstop || a.backstopRadius!=b.backstopRadius || a.stiffness!=b.stiffness)return false;
  for(int n=0;n<16;++n)if(a.maxDistance[n]!=b.maxDistance[n] || a.backstopDistance[n]!=b.backstopDistance[n])return false;
  return true;
}
static bool ClothBoneSupportMotionFrom(void *data,ClothBoneMotionParameters &value) {
  void *box=nullptr;
  return data && ClothInvoke(SurfaceMethod(il2cpp_object_get_class(data),"GetClothParameters","BeyondDynamicBone.ClothParameters"),data,nullptr,box) &&
      ClothBoneSupportMotionParameters(box,value);
}
static bool ClothBoneSupportPolicyConfigure(void *data,void *original) {
  if(!ClothOnMainThread())return false;
  auto &s=ClothBoneState();
  if(s.contactConsumer<0||ClothBoneNativeLayerPartner(s))return true;
  if(!ClothOwns(s.owner) || !ClothBoneSurfacePartner(s) || !s.supportCreated ||
      s.local.requested || s.supportPointCollision || s.supportMotion || !data || data==original)return false;
  constexpr auto type="BeyondDynamicBone.MotionConstraint.SerializeData";
  void *source=nullptr,*copy=nullptr,*afterSource=nullptr;bool originalUse=false,afterUse=false;
  ClothBoneMotionParameters before{},after{},actual{};
  if(!ClothField(original,"motionConstraint",type,source) || !source ||
      !ClothField(source,"useMaxDistance","System.Boolean",originalUse) || !ClothBoneSupportMotionFrom(original,before) ||
      !SurfaceCloneField(data,original,"motionConstraint",type) || !ClothField(data,"motionConstraint",type,copy) || !copy || copy==source ||
      !SurfaceScalar(copy,"useMaxDistance","System.Boolean",false) || !ClothBoneSupportMotionFrom(data,actual) ||
      !ClothBoneSupportMotionFrom(original,after) || !ClothField(original,"motionConstraint",type,afterSource) || afterSource!=source ||
      !ClothField(source,"useMaxDistance","System.Boolean",afterUse) || afterUse!=originalUse || !ClothBoneSupportMotionSame(before,after))return false;
  auto expected=before;expected.useMaxDistance=0;
  if(!ClothBoneSupportMotionSame(expected,actual) || !(s.supportMotion=ClothBoneHold(copy)))return false;
  s.supportOriginalMotion=before;s.supportPointCollision=true;
  Log("[CLOTH-BONE-SUPPORT-POLICY] stage=configured component=%s generation=%llu bodyCollision=Point sourceMaxDistance=%d candidateMaxDistance=0 candidateAttachmentsPreserved=1 otherMotionFieldsUnchanged=1 shapeConstraintsUnchanged=1 sourceUntouched=1 TeamReadbackPending=1",
      s.profile->component,(unsigned long long)s.owner.generation,int(before.useMaxDistance));
  return true;
}
static bool ClothBoneSupportPolicyMatches(const ClothBoneRuntime &s,void *box) {
  if(!ClothOnMainThread())return false;
  if(s.contactConsumer<0||ClothBoneNativeLayerPartner(s))return !s.supportPointCollision;
  void *motion=nullptr;ClothBoneMotionParameters actual{};
  if(!s.supportPointCollision || !s.supportMotion || !s.supportCreated || !ClothBoneSurfacePartner(s) || !ClothOwns(s.owner) ||
      !ClothField(CollisionGc(s.candidateData),"motionConstraint","BeyondDynamicBone.MotionConstraint.SerializeData",motion) ||
      motion!=CollisionGc(s.supportMotion) || !ClothBoneSupportMotionParameters(box,actual))return false;
  auto expected=s.supportOriginalMotion;expected.useMaxDistance=0;
  return ClothBoneSupportMotionSame(expected,actual);
}
