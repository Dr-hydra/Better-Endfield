#pragma once
static bool ClothBoneSurfacePartner(const ClothBoneRuntime &s) {
  return s.profile==ClothContactPartnerProfiles[0] && s.profile->signature &&
      !strcmp(s.profile->signature,ClothPartnerSurfaceSource) && s.contactConsumer>=0 &&
      s.contactConsumer<s_clothBoneCount && ClothBonePair(s,s_clothBoneSlots[s.contactConsumer]);
}
static bool ClothBoneSupportCreate() {
  auto &s=ClothBoneState();
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1)return false;
  if(!ClothBoneSurfacePartner(s))return s.contactConsumer<0||ClothBoneNativeLayerPartner(s);
  constexpr size_t count=std::size(ClothPartnerSurfaceBones);
  if(s.supportCreated)return true;
  if(!s.supportPreparing) {
    if(!s.supportObjects.empty() || s.profile->boneCount!=ClothPartnerSurfaceOriginal ||
        s.bones.size()!=size_t(ClothPartnerSurfaceOriginal) || count<1 || count>84 ||
        s.bones.size()+count>ClothContactParticles || !ClothOwns(s.owner))return false;
    auto parent=ClothTarget(s.bones[s.profile->roots[0]].parent);
    if(!parent || !ClothAnchorUnderOwner(parent))return false;
    for(int n=0;n<s.profile->rootCount;++n)if(parent!=ClothTarget(s.bones[s.profile->roots[n]].parent))return false;
    int children=0;if(!ClothValue(s_clothUnity.childCount,parent,children)||children<0||children>256)return false;
    for(int n=0;n<children;++n) {
      void *t=nullptr,*args[]{&n};char name[128]{};
      if(!ClothInvoke(s_clothUnity.child,parent,args,t)||!t)return false;CollisionName(t,name,sizeof(name));
      for(const auto &b:ClothPartnerSurfaceBones)if(!strcmp(name,b.name))return false;
  }
  s.shapeBones.assign(s.profile->bones,s.profile->bones+s.profile->boneCount);
  for(int n=0;n<s.profile->boneCount;++n) {s.shapeBones[n].attribute=0;s.supportIgnored.push_back(n);}
  s.shapeBones.insert(s.shapeBones.end(),std::begin(ClothPartnerSurfaceBones),std::end(ClothPartnerSurfaceBones));
  s.shapeProfile=*s.profile;s.shapeProfile.signature=ClothPartnerSurfaceSignature;s.shapeProfile.bones=s.shapeBones.data();
  s.shapeProfile.boneCount=int(s.shapeBones.size());s.shapeProfile.depth=ClothPartnerSurfaceDepth;
  s.shapeProfile.roots=ClothPartnerSurfaceRoots;s.shapeProfile.rootCount=int(std::size(ClothPartnerSurfaceRoots));
  s.shapeProfile.nativeGraphs=ClothPartnerSurfaceGraphs;s.shapeProfile.nativeGraphCount=int(std::size(ClothPartnerSurfaceGraphs));
  s.shapeProfile.candidateIgnored=s.supportIgnored.data();s.shapeProfile.candidateIgnoredCount=int(s.supportIgnored.size());
  if(s.shapeProfile.EffectiveCount()!=count)return false;
  s.bones.resize(s.shapeBones.size());
  s.supportPreparing=true;
  }
  auto parent=ClothTarget(s.bones[s.profile->roots[0]].parent);
  if(!parent || !ClothOwns(s.owner) || s.stopRequested)return false;
  const size_t end=(std::min)(count,s.supportNextBone+8);
  for(;s.supportNextBone<end;++s.supportNextBone) {
    const auto n=s.supportNextBone;
    const auto &a=ClothPartnerSurfaceBones[n];auto &b=s.bones[ClothPartnerSurfaceOriginal+n];
    void *go=il2cpp_object_new(g_gameObjectClass),*label=il2cpp_string_new(a.name),*unused=nullptr,*args[]{label};
    if(!go||!label||!ClothBoneHold(go)||!ClothBoneHold(label))return false;
    const bool made=ClothInvoke(ClothMethod(g_gameObjectClass,".ctor","System.Void","System.String"),go,args,unused);
    auto ref=ClothProtect(go);
    if(!ref.handle) {
      void *destroy[]{go};ClothInvoke(ClothMethod(SurfaceClass("UnityEngine","Object"),"Destroy","System.Void","UnityEngine.Object",true),nullptr,destroy,unused);return false;
    }
    s.supportObjects.push_back(ref);s.supportDestroyIssued.push_back(false);
    auto t=CollisionTransform(go),p=a.parent<0?parent:ClothTarget(s.bones[a.parent].bone);
    if(!made||!t||!p||!SurfaceTRS(t,p,a.position,a.rotation,a.scale))return false;
    b.bone=ClothProtect(t);b.parent=ClothProtect(p);b.local=a.position;b.rotation=a.rotation;
    b.referenceScale=s.bones[s.profile->roots[0]].referenceScale;
    if(!b.bone.handle||!b.parent.handle)return false;
  }
  if(s.supportNextBone<count)return true;
  s.supportCreated=true;
  const int fixed=int(std::count_if(std::begin(ClothPartnerSurfaceBones),std::end(ClothPartnerSurfaceBones),[](const ClothBoneAsset &b){return b.attribute==1;}));
  Log("[CLOTH-BONE-SURFACE] stage=prepared component=%s recipe=%s generation=%llu points=%zu faces=%d fixed=%d move=%zu waistResponse=%d originalBonesPreserved=%d bodyCapsuleWrites=0 runtimeProjection=0 nativeSolver=originalBBC renderPublished=0",
      s.profile->component,ClothPartnerSurfaceSignature,(unsigned long long)s.owner.generation,count,s.shapeProfile.FaceCount(),fixed,count-fixed,int(ClothPartnerSurfaceWaistResponse),s.profile->boneCount);
  return true;
}
static ClothRef ClothBoneLocalBindingRef(const ClothBoneRuntime &s,int id) {
  if(id>=0)return size_t(id)<s.bones.size()?s.bones[id].bone:ClothRef{};
  if(!ClothOnMainThread())return {};
  if(id < -128 || s.contactPartner<0 || s.contactPartner>=s_clothBoneCount)return {};
  const auto &p=s_clothBoneSlots[s.contactPartner];const int index=-id-1;
  if(!ClothBonePair(p,s)||!ClothBoneSurfacePartner(p)||!p.supportCreated||p.supportCleanup||!p.pending||!p.lease||
      p.tx.phase!=eiem_cloth_rebuild::Phase::Active||!p.teamModeConfirmed||!p.graph[1]||
      index<ClothPartnerSurfaceOriginal||size_t(index)>=p.bones.size()||
      !ClothBoneCandidate(p).bones[index].attribute)return {};
  const auto &b=p.bones[index];auto t=ClothTarget(b.bone);
  if(!t||!ClothOwns(s.owner)||!ClothAnchorUnderOwner(t)||CollisionParent(t)!=ClothTarget(b.parent))return {};
  return b.bone;
}
static bool ClothBoneLocalBindingMapFor(const ClothBoneRuntime &s,const ClothBoneLocalMeshConfig &c,const ClothBoneRendererAsset &a) {
  const bool retained=s.local.recipe&&s.local.recipe->CoatWaistSkinOnly()&&s.profile&&eiem_cloth_asset::SourceInactiveCoat(*s.profile);
  if(c.bindingCount<0||c.bindingCount>256||(c.bindingCount==0?!retained:!c.bindingNativeIndices))return false;
  bool paired=false;
  for(int n=0;n<c.bindingCount;++n)if(c.bindingNativeIndices[n]<0) {
    if(!ClothBoneLocalBindingRef(s,c.bindingNativeIndices[n]).handle)return false;paired=true;
  }
  if(paired) {
    const auto &p=s_clothBoneSlots[s.contactPartner];auto bbc=ClothTarget(p.bbc);void *process=nullptr,*data=nullptr;
    if(!bbc||!CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",process)||process!=CollisionGc(p.process[1])||
        !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data)||
        data!=CollisionGc(p.candidateData)||!ClothBoneTeamRegistered(process,p.team[1]))return false;
  }
  return ClothBoneLocalBindingMapValid(c,a,s.local.recipe->Total(),paired?ClothPartnerSurfaceOriginal:0,
      paired?int(std::size(ClothPartnerSurfaceBones)):0,retained);
}
static bool ClothBoneSupportRelease() {
  auto &s=ClothBoneState();
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1)return false;
  if(s.contactConsumer>=0&&s.contactConsumer<s_clothBoneCount) {
    const auto &c=s_clothBoneSlots[s.contactConsumer];
    if(c.lease||c.local.HasResources())return false;
  }
  if(s.tx.lease&&!s.supportOwnerDrained)return false;
  for(const auto &r:s.renderers) {
    auto renderer=ClothTarget(r.renderer);void *array=nullptr;
    if(!renderer) {void *unused=nullptr;if(ClothInspect(r.renderer,unused)!=ClothLife::Destroyed)return false;continue;}
    uintptr_t count=0;
    if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(renderer),"get_bones","UnityEngine.Transform[]"),renderer,nullptr,array)||
        !ClothArray(array,"UnityEngine.Transform[]",count)||count>256)return false;
    for(size_t n=0;n<count;++n)for(size_t k=ClothPartnerSurfaceOriginal;k<s.bones.size();++k) {
      auto own=ClothTarget(s.bones[k].bone);
      if(own&&reinterpret_cast<void**>((char*)array+32)[n]==own)return false;
    }
  }
  bool destroyed=true;
  for(size_t n=0;n<s.supportObjects.size();++n) {
    void *go=nullptr;auto life=ClothInspect(s.supportObjects[n],go);if(life==ClothLife::Destroyed)continue;
    destroyed=false;
    if(go&&!s.supportDestroyIssued[n]) {
      void *unused=nullptr,*args[]{go};s.supportDestroyIssued[n]=true;
      ClothInvoke(ClothMethod(SurfaceClass("UnityEngine","Object"),"Destroy","System.Void","UnityEngine.Object",true),nullptr,args,unused);
    }
  }
  if(!destroyed)return false;
  for(auto &r:s.supportObjects)ClothFree(r);s.supportObjects.clear();s.supportDestroyIssued.clear();
  s.supportCreated=s.supportCleanup=false;
  Log("[CLOTH-BONE-SURFACE] stage=released originalMeshAndRootListsRestored=1 generation=%llu",(unsigned long long)s.owner.generation);return true;
}
