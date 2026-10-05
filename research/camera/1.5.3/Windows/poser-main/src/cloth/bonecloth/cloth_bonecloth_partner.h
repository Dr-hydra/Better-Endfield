#pragma once
static bool ClothBoneRibbonPartner(const ClothBoneRuntime &s) {
  return s.profile&&s.profile->ribbonSource&&s.profile->runtimeGenerated&&s.local.requested&&s.local.recipe&&
      s.local.recipe==s.profile->generatedLocal&&s.local.recipe->ribbonSurface;
}
static bool ClothBoneRibbonPair(const ClothBoneRuntime &partner,const ClothBoneRuntime &consumer) {
  return ClothBoneRibbonPartner(partner)&&consumer.profile&&consumer.local.requested&&consumer.local.recipe&&
      consumer.local.recipe==consumer.profile->generatedLocal&&consumer.local.recipe->resampledPanel&&
      !strcmp(partner.profile->prefabSha,consumer.profile->prefabSha)&&consumer.local.recipe->responseConsumer&&
      !strcmp(consumer.local.recipe->responseConsumer,partner.profile->component);
}
static bool ClothBoneGeneratedPair(const ClothBoneRuntime &partner,const ClothBoneRuntime &consumer) {
  return partner.profile&&consumer.profile&&partner.local.requested&&consumer.local.requested&&
      partner.local.recipe==partner.profile->generatedLocal&&consumer.local.recipe==consumer.profile->generatedLocal&&
      ClothGeneratedLayerPair(*partner.profile,*consumer.profile);
}
static bool ClothBoneNativeLayerPartner(const ClothBoneRuntime &s) {
  if(ClothBoneRibbonPartner(s))return true;
  return s.contactConsumer>=0&&s.contactConsumer<s_clothBoneCount&&ClothBoneGeneratedPair(s,s_clothBoneSlots[s.contactConsumer]);
}
static bool ClothBonePair(const ClothBoneRuntime &partner,const ClothBoneRuntime &consumer) {
  return (ClothBoneGeneratedPair(partner,consumer)||ClothBoneRibbonPair(partner,consumer)||(partner.profile==ClothContactPartnerProfiles[0] && consumer.profile &&
      consumer.local.requested && consumer.local.recipe &&
      !strcmp(consumer.profile->signature,ClothContactPartnerConsumer) &&
      !strcmp(consumer.local.recipe->signature,ClothContactPartnerRecipe))) &&
      partner.contactConsumer>=0 && partner.contactConsumer<s_clothBoneCount &&
      consumer.contactPartner>=0 && consumer.contactPartner<s_clothBoneCount &&
      &s_clothBoneSlots[partner.contactConsumer]==&consumer && &s_clothBoneSlots[consumer.contactPartner]==&partner &&
      partner.owner==consumer.owner && partner.command==consumer.command;
}
static bool ClothBonePartnerNeedsRestore(const ClothBoneRuntime &partner) {
  if(partner.contactConsumer<0)return false;
  if(partner.contactConsumer>=s_clothBoneCount)return true;
  const auto &consumer=s_clothBoneSlots[partner.contactConsumer];
  return !ClothBonePair(partner,consumer)||!consumer.pending||consumer.stopRequested||
      consumer.failed||consumer.tx.cancelled||consumer.local.cleanup;
}
static bool ClothBonePrebuildOmittedIdentity(bool capture=false) {
  auto &s=ClothBoneState();if(!s.profile)return false;const auto &p=*s.profile;
  if(!p.prebuildOmittedCount)return s.prebuildOmitted.empty();
  if(!s_clothSurfaceAtBoundary||!p.runtimeGenerated||!p.prebuildId||!s.prebuild.captured||p.prebuildOmittedCount<0||p.prebuildOmittedCount>16)return false;
  if(capture){if(!s.prebuildOmitted.empty())return false;
    for(int k=0;k<p.prebuildOmittedCount;++k){const auto &a=p.prebuildOmitted[k];void *found=nullptr,*parent=nullptr;
      for(int n=0;n<p.boneCount;++n)if(!strcmp(p.bones[n].name,a.parent)&&!p.OutsideOriginal(n)){
        auto t=ClothTarget(s.bones[n].bone);int count=-1;if(!t||!ClothValue(s_clothUnity.childCount,t,count)||count<0||count>128)return false;
        for(int c=0;c<count;++c){void *child=nullptr,*args[]{&c};char name[128]{};if(!ClothInvoke(s_clothUnity.child,t,args,child)||!child)return false;CollisionName(child,name,sizeof(name));
          if(!strcmp(name,a.name)){if(found)return false;found=child;parent=t;}}}
      int children=-1;if(!found||!parent||!ClothAnchorUnderOwner(found)||ClothBoneIndex(found)>=0||!ClothValue(s_clothUnity.childCount,found,children)||children!=0)return false;
      void *type=il2cpp_type_get_object(il2cpp_class_get_type(g_componentClass)),*go=nullptr,*array=nullptr,*args[]{type};uintptr_t count=0;
      if(!type||!ClothInvoke(s_clothUnity.getGO,found,nullptr,go)||!ClothInvoke(s_clothUnity.components,go,args,array)||!array||!ClothBoneHold(array)||
          !ClothArray(array,"UnityEngine.Component[]",count)||count!=1||reinterpret_cast<void**>((char*)array+32)[0]!=found)return false;
      s.prebuildOmitted.push_back({ClothProtect(found),ClothProtect(parent)});if(!s.prebuildOmitted.back().bone.handle||!s.prebuildOmitted.back().parent.handle)return false;
    }
    Log("[CLOTH-BONE-PREBUILD] stage=omitted-leaves component=%s leaves=%zu sourceCacheClosure=1 unboundTransformOnly=1 hierarchyWrites=0",p.component,s.prebuildOmitted.size());
  }
  if(s.prebuildOmitted.size()!=size_t(p.prebuildOmittedCount))return false;
  for(const auto &r:s.prebuildOmitted){auto t=ClothTarget(r.bone);int children=-1;if(!t||!ClothAnchorUnderOwner(t)||CollisionParent(t)!=ClothTarget(r.parent)||
      !ClothValue(s_clothUnity.childCount,t,children)||children!=0)return false;}
  return true;
}
static bool ClothBoneOriginalExcludedIdentity() {
  const auto &s=ClothBoneState();
  if(!s.profile || !ClothBonePrebuildOmittedIdentity() || s.originalExcluded.size()!=size_t(s.profile->originalExcludedCount))return false;
  if(s.originalExcluded.empty())return true;
  auto list=CollisionGc(s.originalExcludedList);void *actual=nullptr;
  if(!list || !CollisionField(CollisionGc(s.data),"ignoreFromRootBones","System.Collections.Generic.List<UnityEngine.Transform>",actual) ||
      actual!=list || CollisionCount(list)!=int(s.originalExcluded.size()))return false;
  for(size_t n=0;n<s.originalExcluded.size();++n) {
    const auto &r=s.originalExcluded[n];auto t=ClothTarget(r.bone),p=ClothTarget(r.parent);int count=-1;
    if(!t||!p||!ClothAnchorUnderOwner(t)||CollisionParent(t)!=p||
        CollisionItem(list,int(n),"UnityEngine.Transform")!=t||!ClothValue(s_clothUnity.childCount,t,count)||
        (s.excludedBranches.empty()&&count))return false;
  }
  if(s.profile->runtimeGenerated&&s.profile->excludedBranchCount) {
    if(s.excludedBranches.size()!=size_t(s.profile->excludedBranchCount))return false;
    for(const auto &r:s.excludedBranches) {
      auto t=ClothTarget(r.bone);int count=-1,expected=0;
      if(!t||!ClothAnchorUnderOwner(t)||CollisionParent(t)!=ClothTarget(r.parent)||ClothBoneIndex(t)>=0||
          !ClothValue(s_clothUnity.childCount,t,count)||count<0||count>128)return false;
      for(const auto &child:s.excludedBranches)expected+=ClothTarget(child.parent)==t;
      if(count!=expected)return false;
      for(int k=0;k<count;++k){void *child=nullptr,*args[]{&k};if(!ClothInvoke(s_clothUnity.child,t,args,child)||!child)return false;
        if(std::none_of(s.excludedBranches.begin(),s.excludedBranches.end(),[&](const ClothBoneExcludedRef &c){return ClothTarget(c.bone)==child&&ClothTarget(c.parent)==t;}))return false;}
    }
  }
  return true;
}
static bool ClothBoneCaptureOriginalExcluded() {
  auto &s=ClothBoneState();const auto &p=*s.profile;
  if(!ClothBonePrebuildOmittedIdentity(true))return false;
  if(!p.originalExcludedCount)return true;
  const bool branch=p.runtimeGenerated&&p.excludedBranchCount>0;
  if(!p.originalExcluded || p.originalExcludedCount<1 || p.originalExcludedCount>(branch?128:16) || !s.originalExcluded.empty())return false;
  void *list=nullptr;
  if(!CollisionField(CollisionGc(s.data),"ignoreFromRootBones","System.Collections.Generic.List<UnityEngine.Transform>",list) ||
      CollisionCount(list)!=p.originalExcludedCount || !(s.originalExcludedList=ClothBoneHold(list)))return false;
  for(int n=0;n<p.originalExcludedCount;++n) {
    auto t=CollisionItem(list,n,"UnityEngine.Transform"),parent=CollisionParent(t);char name[128]{},pn[128]{};
    CollisionName(t,name,sizeof(name));CollisionName(parent,pn,sizeof(pn));int children=-1;
    if(!t||!parent||!ClothAnchorUnderOwner(t)||strcmp(name,p.originalExcluded[n].name)||strcmp(pn,p.originalExcluded[n].parent)||
        ClothBoneIndex(t)>=0||(!branch&&ClothBoneIndex(parent)<0)||!ClothValue(s_clothUnity.childCount,t,children)||children<0||(!branch&&children))return false;
    for(const auto &r:s.originalExcluded)if(ClothTarget(r.bone)==t)return false;
    if(branch) {
      bool under=false;for(int k=0;k<p.rootCount;++k){auto ancestor=t;for(int step=0;ancestor&&step<128;++step){if(ancestor==ClothTarget(s.bones[p.originalRoots[k]].bone)){under=true;break;}ancestor=CollisionParent(ancestor);}}
      if(!under)return false;
      s.originalExcluded.push_back({ClothProtect(t),ClothProtect(parent)});
      if(!s.originalExcluded.back().bone.handle||!s.originalExcluded.back().parent.handle)return false;
      continue;
    }
    void *type=il2cpp_type_get_object(il2cpp_class_get_type(g_componentClass)),*go=nullptr,*array=nullptr,*args[]{type};uintptr_t count=0;
    if(!type||!ClothInvoke(s_clothUnity.getGO,t,nullptr,go)||!ClothInvoke(s_clothUnity.components,go,args,array)||
        !array||!ClothBoneHold(array)||!ClothArray(array,"UnityEngine.Component[]",count)||count!=2)return false;
    bool transform=false,capsule=false;
    for(size_t k=0;k<count;++k) {
      auto c=reinterpret_cast<void**>((char*)array+32)[k];if(!c)return false;
      const auto ct=il2cpp_class_get_type(il2cpp_object_get_class(c));
      if(c==t&&ClothTypeIs(ct,"UnityEngine.Transform"))transform=true;
      else if(ClothTypeIs(ct,"BeyondDynamicBone.BeyondBoneCapsuleCollider"))capsule=true;
      else return false;
    }
    if(!transform||!capsule)return false;
    s.originalExcluded.push_back({ClothProtect(t),ClothProtect(parent)});
    if(!s.originalExcluded.back().bone.handle||!s.originalExcluded.back().parent.handle)return false;
  }
  if(branch) {
    if(!p.excludedBranches||p.excludedBranchCount>256)return false;
    std::vector<void*> pending;for(const auto &r:s.originalExcluded)pending.push_back(ClothTarget(r.bone));
    for(size_t at=0;at<pending.size();++at) {
      auto t=pending[at];if(std::any_of(s.excludedBranches.begin(),s.excludedBranches.end(),[&](const ClothBoneExcludedRef &r){return ClothTarget(r.bone)==t;}))continue;
      auto parent=CollisionParent(t);char name[128]{},pn[128]{};CollisionName(t,name,sizeof(name));CollisionName(parent,pn,sizeof(pn));int matches=0;
      for(int k=0;k<p.excludedBranchCount;++k)matches+=!strcmp(name,p.excludedBranches[k].name)&&!strcmp(pn,p.excludedBranches[k].parent);
      if(!t||!parent||!ClothAnchorUnderOwner(t)||ClothBoneIndex(t)>=0||matches!=1||s.excludedBranches.size()>=size_t(p.excludedBranchCount))return false;
      s.excludedBranches.push_back({ClothProtect(t),ClothProtect(parent)});if(!s.excludedBranches.back().bone.handle||!s.excludedBranches.back().parent.handle)return false;
      int children=-1;if(!ClothValue(s_clothUnity.childCount,t,children)||children<0||children>128)return false;
      for(int k=0;k<children;++k){void *child=nullptr,*args[]{&k};if(!ClothInvoke(s_clothUnity.child,t,args,child)||!child||pending.size()>=512)return false;pending.push_back(child);}
    }
    Log("[CLOTH-AUTO-EXCLUDED] component=%s declared=%d descendants=%zu sourceAndLiveClosure=1 excludedBoneWrites=0 foreignSkinPreserved=1",p.component,p.originalExcludedCount,s.excludedBranches.size());
  }
  return ClothBoneOriginalExcludedIdentity();
}
static bool ClothBonePartnerRenderer(const ClothBoneRendererRef &r,void *renderer,void *mesh,void *root) {
  const auto &s=ClothBoneState();const int consumer=s.contactConsumer;
  if(consumer<0||consumer>=s_clothBoneCount)return false;
  const auto &p=s_clothBoneSlots[consumer];
  if(!ClothBonePair(s,p)||!p.pending||!p.lease||!p.local.published||root!=ClothTarget(r.root))return false;
  int match=-1;
  for(size_t n=0;n<p.renderers.size();++n)if(ClothTarget(p.renderers[n].renderer)==renderer) {
    if(match>=0)return false;match=int(n);
    const auto &other=p.renderers[n];
    if(ClothTarget(other.mesh)!=ClothTarget(r.mesh)||other.bones.size()!=r.bones.size())return false;
    for(size_t b=0;b<r.bones.size();++b)
      if(ClothTarget(other.bones[b])!=ClothTarget(r.bones[b])||ClothTarget(other.parents[b])!=ClothTarget(r.parents[b]))return false;
  }
  if(match<0)return false;
  const int previous=s_clothBoneContext;s_clothBoneContext=consumer;bool valid=false;
  __try {valid=ClothBoneLocalRendererIdentity(size_t(match),renderer,mesh);}
  __finally {s_clothBoneContext=previous;}
  return valid;
}
