#pragma once
static bool ClothBoneApronLayerReadback(ClothBoneRuntime &s,void *process) {
  auto &l=s.local.apronLayer;
  if(!l.configured)return l.outerColliders.empty();
  if(!ClothOnMainThread() || !ClothOwns(s.owner) || !ClothBoneApronPointBody(s) ||
      l.producer<0 || size_t(l.producer)>=s.nativeProducers.size() ||
      l.omitted.size()!=2 || l.omitted[0]!=4 || l.omitted[1]!=5 || l.outerColliders.size()!=7 || s.bones.size()<6)return false;
  const auto &p=s.nativeProducers[l.producer];void *constraint=nullptr,*list=nullptr;
  if(!ClothBoneNativeProducerIdentity(s,p) || !CollisionList(CollisionGc(p.data),constraint,list) ||
      constraint!=CollisionGc(l.constraint) || list!=CollisionGc(l.list) || CollisionCount(list)!=7)return false;
  for(const auto &ref:l.outerColliders) {
    auto c=ClothTarget(ref);int hits=0;
    if(!c)return false;
    for(int n=0;n<7;++n)hits+=CollisionItem(list,n,"BeyondDynamicBone.ColliderComponent")==c;
    if(hits!=1)return false;
  }
  for(int side=0;side<2;++side) {
    const int index=l.response[side],bone=side?4:1;
    if(index<0 || index>=7)return false;
    auto c=ClothTarget(l.outerColliders[index]),t=CollisionTransform(c),go=static_cast<void*>(nullptr);
    bool enabled=false,active=false,member=false,listed=false;int count=-1;
    if(!t || t!=ClothTarget(s.bones[bone].bone) || CollisionParent(t)!=ClothTarget(s.bones[bone].parent) ||
        !ClothAnchorUnderOwner(t) || !ClothValue(s_clothUnity.getEnabled,c,enabled) || !enabled ||
        !ClothInvoke(s_clothUnity.getGO,c,nullptr,go) || !go || !ClothValue(s_clothUnity.active,go,active) || !active ||
        !CollisionTeams(c,p.team,member,count) || !member || count<1 ||
        !CollisionProcessContains(CollisionGc(p.process),c,listed) || !listed)return false;
  }
  if(process && process==CollisionGc(s.process[1]) && !l.confirmed) {
    l.confirmed=true;
    Log("[CLOTH-BONE-APRON-LAYERS] stage=registered innerTeam=%d outerTeam=%d innerBodyColliders=4 outerResponseColliders=2 outerList=7 rule=body-then-inner-then-outer reversePressureRemoved=2 outerProcessUnchanged=1 thicknessUnchanged=1 visualVerified=0",
        s.team[1],p.team);
  }
  return true;
}
static bool ClothBoneApronLayerConfigure() {
  auto &s=ClothBoneState();auto &l=s.local.apronLayer;
  if(!s.local.requested || !s.local.recipe || !s.local.recipe->sourceApronFit)return !l.configured;
  if(!ClothOnMainThread() || !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 || !ClothOwns(s.owner) ||
      s.stopRequested || s.tx.cancelled || !ClothBoneApronPointBody(s) || l.configured || !l.outerColliders.empty() ||
      s.colliders.size()!=6 || s.nativeProducers.size()!=1 || !s.profile->nativeProducers ||
      strcmp(s.profile->nativeProducers[0],"MC_frontCloth") || s.local.recipe->contactProducer ||
      !s.local.contactColliderOmissions.empty() || s.contactPartner>=0 || s.contactConsumer>=0)return false;
  const auto &p=s.nativeProducers[0];void *constraint=nullptr,*list=nullptr;
  const int omit[]{4,5};std::vector<int> selected;
  if(!ClothBoneSelectProducerVolumes(s,p,omit,2,selected) ||
      !CollisionList(CollisionGc(p.data),constraint,list) || CollisionCount(list)!=7)return false;
  std::vector<void*> values;std::array<int,2> response{{-1,-1}};
  for(int n=0;n<7;++n) {
    auto c=CollisionItem(list,n,"BeyondDynamicBone.ColliderComponent"),t=CollisionTransform(c);
    if(!c || !t || !ClothAnchorUnderOwner(t) || std::find(values.begin(),values.end(),c)!=values.end())return false;
    values.push_back(c);
    for(int side=0;side<2;++side)if(t==ClothTarget(s.bones[side?4:1].bone)) {
      if(response[side]>=0 || strcmp(il2cpp_class_get_name(il2cpp_object_get_class(c)),"BeyondBoneCapsuleCollider"))return false;
      response[side]=n;
    }
  }
  if(response[0]<0 || response[1]<0 || response[0]==response[1])return false;
  l.producer=0;l.response=response;l.omitted=std::move(selected);
  l.constraint=ClothBoneHold(constraint);l.list=ClothBoneHold(list);
  if(!l.constraint || !l.list)return false;
  for(auto c:values) {l.outerColliders.push_back(ClothProtect(c));if(!l.outerColliders.back().handle)return false;}
  l.configured=true;
  if(!ClothBoneApronLayerReadback(s,nullptr))return false;
  Log("[CLOTH-BONE-APRON-LAYERS] stage=configured generation=%llu command=%u inner=%s outer=MC_frontCloth rule=body-then-inner-then-outer omittedOuterVolumes=2 retainedBodyVolumes=4 retainedOuterResponse=2 outerConfigurationWrites=0 geometryWrites=0 nativeCandidateRegistration=pending visualVerified=0",
      s.owner.generation,s.command,s.profile->component);
  return true;
}
