#pragma once
static bool ClothBoneAttachmentRequested(const ClothBoneRuntime &s) {
  return s.local.requested && s.local.recipe && s.local.recipe->rootRotation!=-1;
}
static bool ClothBoneAttachmentValues(void *box,float &root,float &interpolation) {
  return box && ClothInputTeamField(box,"rootRotation","System.Single",root) &&
      ClothInputTeamField(box,"rotationalInterpolation","System.Single",interpolation) &&
      std::isfinite(root) && root>=0 && root<=1 && std::isfinite(interpolation) && interpolation>=0 && interpolation<=1;
}
static bool ClothBoneAttachmentConfigure(void *data,void *original) {
  if(!ClothOnMainThread())return false;
  auto &s=ClothBoneState();auto &l=s.local;
  if(!ClothBoneAttachmentRequested(s))return !l.attachmentConfigured;
  if(!s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 || !ClothOwns(s.owner) || s.stopRequested ||
      l.attachmentConfigured || l.recipe->rootRotation!=0 || !l.recipe->rootSkinTransition || l.recipe->loop ||
      s.contactPartner<0 || s.contactPartner>=s_clothBoneCount ||
      !ClothBoneSurfacePartner(s_clothBoneSlots[s.contactPartner]) ||
      !data || !original || data==original || data!=CollisionGc(s.candidateData) || original!=CollisionGc(s.data))return false;
  float root=0,interpolation=0,actualRoot=0,actualInterpolation=0,afterRoot=0,afterInterpolation=0;
  void *box=nullptr;
  if(!ClothField(original,"rootRotation","System.Single",root) ||
      !ClothField(original,"rotationalInterpolation","System.Single",interpolation) ||
      !std::isfinite(root) || root<0 || root>1 || !std::isfinite(interpolation) || interpolation<0 || interpolation>1 ||
      !ClothField(data,"rootRotation","System.Single",actualRoot) || actualRoot!=root ||
      !ClothField(data,"rotationalInterpolation","System.Single",actualInterpolation) || actualInterpolation!=interpolation ||
      !SurfaceScalar(data,"rootRotation","System.Single",l.recipe->rootRotation) ||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(data),"GetClothParameters","BeyondDynamicBone.ClothParameters"),data,nullptr,box) ||
      !ClothBoneAttachmentValues(box,actualRoot,actualInterpolation) || actualRoot!=l.recipe->rootRotation || actualInterpolation!=interpolation ||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(original),"GetClothParameters","BeyondDynamicBone.ClothParameters"),original,nullptr,box) ||
      !ClothBoneAttachmentValues(box,afterRoot,afterInterpolation) || afterRoot!=root || afterInterpolation!=interpolation ||
      !ClothField(original,"rootRotation","System.Single",afterRoot) || afterRoot!=root ||
      !ClothField(original,"rotationalInterpolation","System.Single",afterInterpolation) || afterInterpolation!=interpolation)return false;
  l.sourceRootRotation=root;l.sourceRotationalInterpolation=interpolation;l.attachmentConfigured=true;
  Log("[CLOTH-BONE-ATTACHMENT] stage=configured generation=%llu command=%u component=%s sourceRootRotation=%g candidateRootRotation=%g freeRotation=%g sourceUntouched=1 fixedPositionsUnchanged=1 TeamReadbackPending=1",
      s.owner.generation,s.command,s.profile->component,root,l.recipe->rootRotation,interpolation);
  return true;
}
static bool ClothBoneAttachmentMatches(ClothBoneRuntime &s,int slot,void *box) {
  if(!ClothOnMainThread() || slot<0 || slot>2)return false;
  auto &l=s.local;
  if(!ClothBoneAttachmentRequested(s))return !l.attachmentConfigured;
  if(!l.attachmentConfigured)return slot!=1;
  float root=0,interpolation=0,serializedRoot=0,serializedInterpolation=0;
  const auto data=CollisionGc(slot==1?s.candidateData:s.data);
  const float expected=slot==1?l.recipe->rootRotation:l.sourceRootRotation;
  if(!l.attachmentConfigured || !data || !ClothBoneAttachmentValues(box,root,interpolation) ||
      root!=expected || interpolation!=l.sourceRotationalInterpolation ||
      !ClothField(data,"rootRotation","System.Single",serializedRoot) || serializedRoot!=root ||
      !ClothField(data,"rotationalInterpolation","System.Single",serializedInterpolation) || serializedInterpolation!=interpolation)return false;
  if(!l.attachmentReadback[slot]) {
    l.attachmentReadback[slot]=true;
    Log("[CLOTH-BONE-ATTACHMENT] stage=%s frame=%d generation=%llu command=%u team=%d rootRotation=%g freeRotation=%g nativeOutput=1 visualVerified=0",
        slot==1?"Team-confirmed":"source-Team-confirmed",ClothFrame(),s.owner.generation,s.command,s.team[slot],root,interpolation);
  }
  return true;
}
