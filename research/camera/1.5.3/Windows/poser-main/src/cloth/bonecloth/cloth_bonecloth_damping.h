#pragma once
#include "../core/cloth_ribbon_damping.h"

static bool ClothBoneDampingParameters(void *data,float (&curve)[16]) {
  void *box=nullptr;
  return data && ClothInvoke(SurfaceMethod(il2cpp_object_get_class(data),"GetClothParameters",
      "BeyondDynamicBone.ClothParameters"),data,nullptr,box) && box &&
      ClothInputTeamField(box,"dampingCurveData","Unity.Mathematics.float4x4",curve);
}
static bool ClothBoneDampingConfigure(void *candidate,void *source) {
  auto &s=ClothBoneState();const float strength=s_clothRibbonDamping.load();
  const auto *recipe=s.local.recipe;
  const bool ribbon=s.profile && (poser_cloth_ribbon::Component(s.profile->component?s.profile->component:"") ||
      (recipe && (recipe->ribbonSurface || recipe->NativeRibbonWidth())));
  if(!ribbon || strength==0)return true;
  constexpr const char *type="BeyondDynamicBone.CurveSerializeData";
  void *original=nullptr,*copy=nullptr,*keys=nullptr,*afterKeys=nullptr,*afterCurve=nullptr;
  float value=0,adjusted=0,afterValue=0,before[16]{},after[16]{},unchanged[16]{};
  bool use=false,afterUse=false;
  if(!source || !candidate || source==candidate ||
      !ClothField(source,"damping",type,original)||!original||
      !ClothField(original,"value","System.Single",value)||!poser_cloth_ribbon::Value(value,strength,adjusted)||
      !ClothField(original,"useCurve","System.Boolean",use)||
      !ClothField(original,"curve","UnityEngine.AnimationCurve",keys)||
      !ClothBoneDampingParameters(source,before)||!poser_cloth_ribbon::Curve(before,before)||
      !SurfaceCloneField(candidate,source,"damping",type)||
      !ClothField(candidate,"damping",type,copy)||!copy||copy==original||
      !SurfaceScalar(copy,"value","System.Single",adjusted)||
      !ClothBoneDampingParameters(candidate,after)||!poser_cloth_ribbon::Curve(before,after)||
      !ClothBoneDampingParameters(source,unchanged)||memcmp(before,unchanged,sizeof(before))||
      !ClothField(source,"damping",type,afterCurve)||afterCurve!=original||
      !ClothField(original,"value","System.Single",afterValue)||afterValue!=value||
      !ClothField(original,"useCurve","System.Boolean",afterUse)||afterUse!=use||
      !ClothField(original,"curve","UnityEngine.AnimationCurve",afterKeys)||afterKeys!=keys)return false;
  s.ribbonDamping=strength;s.ribbonDampingConfigured=true;
  memcpy(s.ribbonDampingCurve,after,sizeof(after));
  Log("[CLOTH-RIBBON] stage=configured component=%s strength=%g sourceValue=%g value=%g curveEnds=%g/%g originalUntouched=1 nativeReadback=pending visualVerified=0",
      s.profile->component,strength,value,adjusted,after[0],after[15]);
  return true;
}
