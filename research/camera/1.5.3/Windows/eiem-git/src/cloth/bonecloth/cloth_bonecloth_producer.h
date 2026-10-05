#pragma once
static bool ClothBoneProducerPeer(const ClothBoneRuntime &s,const ClothBoneNativeProducer &p,const ClothBoneRuntime &peer,void *bbc,void *process,void *data,void *roots) {
  using Phase=eiem_cloth_rebuild::Phase;
  const bool restored=peer.tx.phase==Phase::Complete&&!peer.tx.lease;
  if(!ClothBonePair(s,peer)||!peer.pending||!peer.lease||(!peer.tx.lease&&!restored)||ClothTarget(peer.bbc)!=bbc)return false;
  bool retained=false;for(auto h:peer.process)if(h&&CollisionGc(h)==CollisionGc(p.process))retained=true;
  if(!retained)return false;
  void *data2=nullptr;
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"GetSerializeData2","BeyondDynamicBone.ClothSerializeData2"),bbc,nullptr,data2))return false;
  const eiem_cloth_rebuild::Identity id{peer.owner,uint64_t(uintptr_t(bbc)),uint64_t(uintptr_t(process)),uint64_t(uintptr_t(data)),uint64_t(uintptr_t(data2))};
  if(!(id==peer.tx.installed)&&!peer.tx.AllowsInstallRepair(id)&&!peer.tx.CanReturnData2(id))return false;
  if(restored && (data!=CollisionGc(peer.data)||data2!=CollisionGc(peer.data2)||
      (process!=CollisionGc(peer.process[2])&&process!=CollisionGc(peer.process[0]))))return false;
  const ClothBoneProfile *shape=nullptr;
  if(data==CollisionGc(peer.data)&&roots==CollisionGc(peer.rootLists[0]))shape=peer.profile;
  else if(data==CollisionGc(peer.candidateData)&&roots==CollisionGc(peer.rootLists[1]))shape=&ClothBoneCandidate(peer);
  if(!shape||CollisionCount(roots)!=shape->rootCount)return false;
  for(int k=0;k<shape->rootCount;++k) {
    const int n=data==CollisionGc(peer.data)?shape->originalRoots[k]:shape->roots[k];
    if(n<0||size_t(n)>=peer.bones.size()||CollisionItem(roots,k,"UnityEngine.Transform")!=ClothTarget(peer.bones[n].bone))return false;
  }
  const auto phase=peer.tx.phase==Phase::Retained?peer.tx.retainedFrom:peer.tx.phase;
  if(phase==Phase::Active||phase==Phase::Complete) {
    bool valid=false,running=false,enabled=false;int team=0;
    return ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"IsValid","System.Boolean"),process,valid)&&valid&&
        ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"IsRunning","System.Boolean"),process,running)&&running&&
        ClothValue(s_clothUnity.getEnabled,bbc,enabled)&&enabled&&
        ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"get_TeamId","System.Int32"),process,team)&&ClothBoneTeamRegistered(process,team);
  }
  return phase!=Phase::Idle && phase!=Phase::Retained;
}
static bool ClothBoneNativeProducerIdentity(const ClothBoneRuntime &s,const ClothBoneNativeProducer &p) {
  auto bbc=ClothTarget(p.bbc);void *process=nullptr,*data=nullptr,*roots=nullptr;
  bool valid=false,running=false,enabled=false;int team=0;
  if(!bbc || !ClothAnchorUnderOwner(CollisionTransform(bbc)) ||
      !CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",process) || !process ||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data) ||
      !CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots) || !roots ||
      p.rootRefs.empty() || p.chainRefs.empty() || p.chainRefs.size()!=p.chainParents.size())return false;
  const ClothBoneRuntime *peer=nullptr;
  for(const auto &slot:s_clothBoneSlots)if(slot.pending&&slot.bbc.id==p.bbc.id) {
    if(peer || !(ClothBonePair(s,slot)||ClothBonePair(slot,s)))return false;peer=&slot;
  }
  for(const auto &root:p.rootRefs)if(!ClothTarget(root)||!ClothAnchorUnderOwner(ClothTarget(root)))return false;
  for(size_t n=0;n<p.chainRefs.size();++n) {
    auto t=ClothTarget(p.chainRefs[n]),parent=ClothTarget(p.chainParents[n]);
    if(!t || !parent || !ClothAnchorUnderOwner(t) || CollisionParent(t)!=parent) return false;
  }
  if(peer&&peer->lease&&ClothBonePair(s,*peer))return ClothBoneProducerPeer(s,p,*peer,bbc,process,data,roots);
  if(peer&&ClothBonePair(*peer,s)&&(!peer->lease||!peer->teamModeConfirmed||peer->tx.phase!=eiem_cloth_rebuild::Phase::Active))return false;
  if(process!=CollisionGc(p.process)||data!=CollisionGc(p.data)||roots!=CollisionGc(p.roots)||CollisionCount(roots)!=int(p.rootRefs.size())||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"IsValid","System.Boolean"),process,valid) || !valid ||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"IsRunning","System.Boolean"),process,running) || !running ||
      !ClothValue(s_clothUnity.getEnabled,bbc,enabled) || !enabled ||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"get_TeamId","System.Int32"),process,team) || team!=p.team ||
      !ClothBoneTeamRegistered(process,p.team)) return false;
  for(size_t n=0;n<p.rootRefs.size();++n)
    if(!ClothTarget(p.rootRefs[n]) || CollisionItem(roots,int(n),"UnityEngine.Transform")!=ClothTarget(p.rootRefs[n])) return false;
  return true;
}
static bool ClothBoneNativeProducerIdentity(const ClothBoneNativeProducer &p) {
  return ClothBoneNativeProducerIdentity(ClothBoneState(),p);
}
static bool ClothBoneProducerAdopt(int which) {
  auto &s=ClothBoneState();
  if(s.contactPartner<0)return true;
  if(s.contactPartner>=s_clothBoneCount||which<1||which>2)return false;
  auto &observer=s_clothBoneSlots[s.contactPartner];
  if(!ClothBonePair(observer,s))return false;
  const auto process=CollisionGc(s.process[which]),data=CollisionGc(which==1?s.candidateData:s.data);void *roots=nullptr;
  if(!ClothBoneTeamRegistered(process,s.team[which])||!CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots))return false;
  const auto &shape=which==1?ClothBoneCandidate(s):*s.profile;
  if(roots!=CollisionGc(s.rootLists[which==1?1:0])||CollisionCount(roots)!=shape.rootCount)return false;
  for(auto &p:observer.nativeProducers)if(p.bbc.id==s.bbc.id) {
    bool owned=false;for(auto h:s.process)if(h&&CollisionGc(h)==CollisionGc(p.process))owned=true;if(!owned)return false;
    std::vector<ClothRef> refs;
    for(int k=0;k<shape.rootCount;++k) {
      const int n=which==1?shape.roots[k]:shape.originalRoots[k];auto t=CollisionItem(roots,k,"UnityEngine.Transform");
      if(n<0||size_t(n)>=s.bones.size()||t!=ClothTarget(s.bones[n].bone)){for(auto &r:refs)ClothFree(r);return false;}
      refs.push_back(ClothProtect(t));if(!refs.back().handle){for(auto &r:refs)ClothFree(r);return false;}
    }
    uint32_t handles[]{il2cpp_gchandle_new(process,false),il2cpp_gchandle_new(data,false),il2cpp_gchandle_new(roots,false)};
    if(!handles[0]||!handles[1]||!handles[2]) {for(auto h:handles)if(h)il2cpp_gchandle_free(h);for(auto &r:refs)ClothFree(r);return false;}
    for(auto h:handles)observer.holds.push_back(h);
    for(auto &r:p.rootRefs)ClothFree(r);p.rootRefs=std::move(refs);p.process=handles[0];p.data=handles[1];p.roots=handles[2];p.team=s.team[which];
    Log("[CLOTH-BONE-PRODUCER-HANDOFF] observer=%s producer=%s slot=%d Process=%p Team=%d roots=%d sameOwnerCommand=1 originalBoneParentsUnchanged=1",
        observer.profile->component,s.profile->component,which,process,p.team,shape.rootCount);
  }
  return true;
}
static bool ClothBoneNativeProducersIdentity() {
  const auto &s=ClothBoneState();
  if(!s.profile) return s.nativeProducers.empty();
  if(s.nativeProducers.size()!=size_t(s.profile->nativeProducerCount)) return false;
  for(auto &p:s.nativeProducers) if(!ClothBoneNativeProducerIdentity(p)) return false;
  return true;
}
static bool ClothBoneSelectProducerVolumes(const ClothBoneRuntime &s,const ClothBoneNativeProducer &p,
    const int *indices,size_t count,std::vector<int> &selected) {
  if(!indices || !count || count>16 || count>=s.colliders.size() || !ClothBoneNativeProducerIdentity(s,p))return false;
  std::vector<int> result;
  for(size_t k=0;k<count;++k) {
    const int index=indices[k];
    if(index<0 || size_t(index)>=s.colliders.size() || std::find(result.begin(),result.end(),index)!=result.end())return false;
    auto c=ClothTarget(s.colliders[index]),t=CollisionTransform(c);bool linked=false;
    if(!c || !t || !ClothAnchorUnderOwner(t))return false;
    for(const auto &ref:p.chainRefs)if(ClothTarget(ref)==t)linked=true;
    if(!linked)return false;
    result.push_back(index);
  }
  selected=std::move(result);return true;
}
static bool ClothBonePartnerVolumePolicy() {
  auto &s=ClothBoneState();
  if(s.contactConsumer<0||ClothBoneNativeLayerPartner(s))return s.partnerColliderOmissions.empty();
  if(!ClothOnMainThread() || !ClothOwns(s.owner) || s.stopRequested || s.tx.cancelled ||
      !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 || s.contactConsumer>=s_clothBoneCount)return false;
  const auto &consumer=s_clothBoneSlots[s.contactConsumer];
  if(!ClothBonePair(s,consumer) || !s.pending || !consumer.pending || consumer.stopRequested || consumer.tx.cancelled ||
      !consumer.profile->prefabSha || strcmp(s.profile->prefabSha,consumer.profile->prefabSha))return false;
  const auto bbc=ClothTarget(consumer.bbc);const ClothBoneNativeProducer *producer=nullptr;
  if(!bbc)return false;
  for(const auto &p:s.nativeProducers)if(ClothTarget(p.bbc)==bbc) {if(producer)return false;producer=&p;}
  if(!producer || !ClothBoneSelectProducerVolumes(s,*producer,ClothContactPartnerVolumeOmissions,
      std::size(ClothContactPartnerVolumeOmissions),s.partnerColliderOmissions))return false;
  Log("[CLOTH-BONE-CONTACT] stage=partner-volume-policy-configured consumer=%s producer=%s omitted=%zu originalColliders=%zu sourceListUntouched=1 producerGeometryUntouched=1 pairBuild=1 surfaceReadbackRequired=1",
      s.profile->component,consumer.profile->component,s.partnerColliderOmissions.size(),s.colliders.size());
  return true;
}
static bool ClothBonePartnerVolumePolicyMatches(const ClothBoneRuntime &s) {
  return s.partnerColliderOmissions.size()==std::size(ClothContactPartnerVolumeOmissions) &&
      std::equal(s.partnerColliderOmissions.begin(),s.partnerColliderOmissions.end(),std::begin(ClothContactPartnerVolumeOmissions));
}
#include "cloth_bonecloth_apron_layer.h"
static bool ClothBoneContactColliderPolicy() {
  auto &s=ClothBoneState();auto &l=s.local;
  if(!ClothBonePartnerVolumePolicy())return false;
  if(!ClothBoneApronLayerConfigure())return false;
  if(!l.requested)return l.contactColliderOmissions.empty();
  if(!l.recipe)return false;
  const auto &r=*l.recipe;
  if(!r.contactOmissionCount)return l.contactColliderOmissions.empty();
  if(!ClothOnMainThread() || !ClothOwns(s.owner) || s.stopRequested || !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 ||
      !s.profile || !r.baseSignature || !r.prefabSha || !s.profile->signature || !s.profile->prefabSha ||
      strcmp(r.baseSignature,s.profile->signature) || strcmp(r.prefabSha,s.profile->prefabSha) ||
      !r.contactProducer || !r.contactOmissions || r.contactOmissionCount<1 || r.contactOmissionCount>16 ||
      size_t(r.contactOmissionCount)>=s.colliders.size() || !l.contactConfigured ||
      l.contactProducer<0 || size_t(l.contactProducer)>=s.nativeProducers.size()) return false;
  const auto &p=s.nativeProducers[l.contactProducer];
  char name[128]{};CollisionName(ClothTarget(p.bbc),name,sizeof(name));if(strcmp(name,r.contactProducer)) return false;
  if(!ClothBoneSelectProducerVolumes(s,p,r.contactOmissions,size_t(r.contactOmissionCount),l.contactColliderOmissions))return false;
  Log("[CLOTH-BONE-CONTACT] stage=volume-policy-configured consumer=%s producer=%s omitted=%zu originalColliders=%zu sourceListUntouched=1 producerGeometryUntouched=1 surfaceReadbackRequired=1",
      s.profile->component,r.contactProducer,l.contactColliderOmissions.size(),s.colliders.size());
  return true;
}
static bool ClothBoneCandidateColliderOmitted(const ClothBoneRuntime &s,size_t n) {
  const auto contains=[n](const std::vector<int> &indices) {return std::find(indices.begin(),indices.end(),int(n))!=indices.end();};
  return contains(s.local.contactColliderOmissions)||contains(s.partnerColliderOmissions)||contains(s.local.apronLayer.omitted);
}
static bool ClothBoneOriginalColliderRequired(size_t n,void *process,bool restoring) {
  const auto &s=ClothBoneState();
  if(restoring || !process || process!=CollisionGc(s.process[1])) return true;
  return !ClothBoneCandidateColliderOmitted(s,n);
}
static bool ClothBoneCaptureNativeProducers() {
  auto &s=ClothBoneState();const auto &profile=*s.profile;
  if(profile.nativeProducerCount<0 || profile.nativeProducerCount>7 || !s.nativeProducers.empty()) return false;
  for(int k=0;k<profile.nativeProducerCount;++k) {
    void *bbc=nullptr;
    for(int n=0;n<s_cloth.count;++n) {
      auto candidate=ClothTarget(s_cloth.instances[n].ref);char name[128]{};
      if(!candidate) continue;CollisionName(candidate,name,sizeof(name));
      if(strcmp(name,profile.nativeProducers[k])) continue;
      if(bbc || candidate==ClothTarget(s.bbc)) return false;bbc=candidate;
    }
    void *process=nullptr,*data=nullptr,*roots=nullptr;
    if(!bbc || !CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",process) || !process ||
        !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data) ||
        !data || data==CollisionGc(s.data) || !CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots)) return false;
    const int count=CollisionCount(roots);if(count<1 || count>64) return false;
    s.nativeProducers.emplace_back();auto &p=s.nativeProducers.back();
    p.bbc=ClothProtect(bbc);p.process=ClothBoneHold(process);p.data=ClothBoneHold(data);p.roots=ClothBoneHold(roots);
    if(!p.bbc.handle || !p.process || !p.data || !p.roots ||
        !ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"get_TeamId","System.Int32"),process,p.team)) return false;
    for(int n=0;n<count;++n) {
      auto t=CollisionItem(roots,n,"UnityEngine.Transform");
      if(!t || !ClothAnchorUnderOwner(t)) return false;
      for(auto &r:p.rootRefs) if(ClothTarget(r)==t) return false;
      p.rootRefs.push_back(ClothProtect(t));if(!p.rootRefs.back().handle) return false;
    }
    std::vector<void*> ancestryRoots;
    for(const auto &r:p.rootRefs)ancestryRoots.push_back(ClothTarget(r));
    for(const auto &peer:s_clothBoneSlots)if(peer.pending&&peer.bbc.id==p.bbc.id&&peer.supportCreated) {
      if(!ClothBonePair(peer,s)||!peer.lease||peer.tx.phase!=eiem_cloth_rebuild::Phase::Active||
          !peer.teamModeConfirmed||process!=CollisionGc(peer.process[1])||data!=CollisionGc(peer.candidateData))return false;
      ancestryRoots.clear();
      for(int r=0;r<peer.profile->rootCount;++r) {
        const int index=peer.profile->originalRoots[r];
        if(index<0||index>=peer.profile->boneCount)return false;
        auto t=ClothTarget(peer.bones[index].bone);if(!t)return false;ancestryRoots.push_back(t);
      }
    }
    int linked=0;
    for(auto &c:s.colliders) {
      std::vector<void*> chain;auto t=CollisionTransform(ClothTarget(c));bool found=false;
      while(t && chain.size()<128 && ClothAnchorUnderOwner(t)) {
        if(std::find(chain.begin(),chain.end(),t)!=chain.end()) return false;
        chain.push_back(t);
        for(auto root:ancestryRoots) if(root==t) found=true;
        if(found) break;t=CollisionParent(t);
      }
      if(!found) continue;++linked;
      for(auto node:chain) {
        bool known=false;for(auto &r:p.chainRefs) if(ClothTarget(r)==node) known=true;
        if(known) continue;if(p.chainRefs.size()>=128) return false;
        p.chainRefs.push_back(ClothProtect(node));p.chainParents.push_back(ClothProtect(CollisionParent(node)));
        if(!p.chainRefs.back().handle || !p.chainParents.back().handle) return false;
      }
    }
    const auto *response=s.local.requested?s.local.recipe:nullptr;
    if(!linked&&response&&response->responsePointCount==5&&response->responseFaceCount>0&&
        response->responseConsumer&&response->contactProducer&&
        !strcmp(profile.nativeProducers[k],response->responseConsumer)&&
        !strcmp(response->contactProducer,response->responseConsumer)&&eiem_cloth_asset::SourceSeraphPanel(profile)) {
      if(!ClothBoneResponseCaptureOutputs(p))return false;
      for(const auto &r:s.local.response.outputBones){auto t=ClothTarget(r);p.chainRefs.push_back(ClothProtect(t));p.chainParents.push_back(ClothProtect(CollisionParent(t)));
        if(!p.chainRefs.back().handle||!p.chainParents.back().handle)return false;}
    }
    if(!linked&&p.chainRefs.empty()&&s.contactPartner>=0&&s.contactPartner<s_clothBoneCount) {
      const auto &peer=s_clothBoneSlots[s.contactPartner];
      if(ClothBoneGeneratedPair(peer,s)&&ClothBonePair(peer,s)&&peer.pending&&peer.lease&&peer.teamModeConfirmed&&
          peer.tx.phase==eiem_cloth_rebuild::Phase::Active&&peer.graph[1]&&peer.reference[1]&&
          peer.bbc.id==p.bbc.id&&CollisionGc(peer.process[1])==process&&CollisionGc(peer.candidateData)==data) {
        for(const auto &ref:peer.bones){auto t=ClothTarget(ref.bone);if(!t||!ClothAnchorUnderOwner(t)||p.chainRefs.size()>=128)return false;
          p.chainRefs.push_back(ClothProtect(t));p.chainParents.push_back(ClothProtect(CollisionParent(t)));
          if(!p.chainRefs.back().handle||!p.chainParents.back().handle)return false;}
      }
    }
    if((!linked&&p.chainRefs.empty()) || !ClothBoneNativeProducerIdentity(p)) return false;
    bool paired=false;for(const auto &peer:s_clothBoneSlots)if(peer.pending&&peer.bbc.id==p.bbc.id)
      paired=ClothBonePair(s,peer)||ClothBonePair(peer,s);
    Log("[CLOTH-BONE-NATIVE-PRODUCER] consumer=%s producer=%s Process=%p Team=%d linkedColliders=%d roots=%d protectedLinks=%zu captureWrites=0 leasedPair=%d",
        profile.component,profile.nativeProducers[k],process,p.team,linked,count,p.chainRefs.size(),int(paired));
  }
  return ClothBoneNativeProducersIdentity();
}
