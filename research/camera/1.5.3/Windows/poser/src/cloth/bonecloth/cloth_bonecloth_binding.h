#pragma once
#include "cloth_bonecloth_partner.h"
static bool ClothBoneRootNames(ClothInstance &i,const ClothBoneProfile &p,void *animator=nullptr) {
  if(!animator)animator=ClothTarget(s_cloth.animator);
  void *data=nullptr,*list=nullptr,*bbc=ClothTarget(i.ref);
  if(strcmp(i.name,p.component) || !bbc || !ClothInvoke(i.api.serialize,bbc,nullptr,data) ||
      !CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",list) ||
      CollisionCount(list)!=p.rootCount) return false;
  for(int n=0;n<p.rootCount;++n) {
    auto t=CollisionItem(list,n,"UnityEngine.Transform"); char name[128]{},parent[128]{};
    CollisionName(t,name,sizeof(name)); CollisionName(CollisionParent(t),parent,sizeof(parent));
    const auto &b=p.bones[p.originalRoots[n]];
    if(!t || strcmp(name,b.name) || strcmp(parent,b.parentName) || !ClothUnderAnimator(t,animator)) return false;
  }
  return true;
}
static bool ClothBoneRendererAssetCheck(ClothBoneRendererRef &r,const ClothBoneRendererAsset &p,bool capture,int slot=-1) {
  auto &s=ClothBoneState();
  auto renderer=ClothTarget(r.renderer); void *mesh=nullptr,*root=nullptr;
  if(p.parent) {
    if(!renderer)return false;
    auto object=CollisionParent(CollisionTransform(renderer));char parent[128]{};CollisionName(object,parent,sizeof(parent));
    if(strcmp(parent,p.parent)||!ClothAnchorUnderOwner(object))return false;
    if(capture)r.qualifiedParent=ClothProtect(object);
    if(!r.qualifiedParent.handle||ClothTarget(r.qualifiedParent)!=object)return false;
  }
  if(!renderer || !ClothAnchorUnderOwner(CollisionTransform(renderer)) ||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(renderer),"get_sharedMesh","UnityEngine.Mesh"),renderer,nullptr,mesh) || !mesh ||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(renderer),"get_rootBone","UnityEngine.Transform"),renderer,nullptr,root) || !root) return false;
  if(capture) {
    char name[128]{},rootName[128]{}; CollisionName(mesh,name,sizeof(name)); CollisionName(root,rootName,sizeof(rootName));
    int vertices=0,submeshes=0;
    if(strcmp(name,p.mesh) || strcmp(rootName,p.root) || !ClothAnchorUnderOwner(root) ||
        !ClothValue(SurfaceMethod(il2cpp_object_get_class(mesh),"get_vertexCount","System.Int32"),mesh,vertices) || vertices!=p.vertices ||
        !ClothValue(SurfaceMethod(il2cpp_object_get_class(mesh),"get_subMeshCount","System.Int32"),mesh,submeshes) || submeshes!=p.submeshes) return false;
    r.mesh=ClothProtect(mesh); r.root=ClothProtect(root);
    if(!r.mesh.handle || !r.root.handle ||
        !SurfaceRenderReferences(renderer,"get_bones","UnityEngine.Transform[]",p.boneCount,p.boneCount,r.bones)) return false;
    for(int n=0;n<p.boneCount;++n) {
      auto t=ClothTarget(r.bones[n]); auto parent=CollisionParent(t); char bn[128]{},pn[128]{};
      CollisionName(t,bn,sizeof(bn)); CollisionName(parent,pn,sizeof(pn));
      const auto &b=p.bones[n];
      if(!t || !parent || !ClothAnchorUnderOwner(t) || strcmp(bn,b.name) || strcmp(pn,b.parent) ||
          (b.cloth>=0 && (size_t(b.cloth)>=s.bones.size() || t!=ClothTarget(s.bones[b.cloth].bone)))) return false;
      auto ref=ClothProtect(parent); if(!ref.handle) return false; r.parents.push_back(ref);
    }
    // Bind poses come from the version-checked installed assets/catalog. The
    // complete renderer/bone topology above must match before any skin swap.
    // Do not read bindposes/vertices/indices/weights from the original live mesh.
    for(int n=0;n<p.boneCount;++n)
      for(int k=0;k<16;++k)if(!std::isfinite(p.bones[n].bind[k]))return false;
  }
  if(!capture && slot>=0 && s.local.OwnsRenderer(size_t(slot)))
    return root==ClothTarget(r.root) && ClothBoneLocalRendererIdentity(slot,renderer,mesh);
  if(!capture && mesh!=ClothTarget(r.mesh))return ClothBonePartnerRenderer(r,renderer,mesh,root);
  if(mesh!=ClothTarget(r.mesh) || root!=ClothTarget(r.root) ||
      !SurfaceRenderSameReferences(renderer,"get_bones","UnityEngine.Transform[]",r.bones) || r.parents.size()!=r.bones.size()) return false;
  for(size_t n=0;n<r.bones.size();++n)
    if(CollisionParent(ClothTarget(r.bones[n]))!=ClothTarget(r.parents[n])) return false;
  return true;
}
static bool ClothBoneRendererCheck(size_t slot,bool capture) {
  auto &s=ClothBoneState();
  return ClothBoneRendererAssetCheck(s.renderers[slot],s.profile->renderers[slot],capture,int(slot));
}
static bool ClothBoneCaptureRenderers() {
  auto &s=ClothBoneState(); s.renderers.resize(s.profile->rendererCount);
  const bool bodyCoverage=s.local.requested && s.local.recipe->bodyCoverage;
  const bool sideCoverage=s.local.requested && ClothBoneSideRecipeFor(*s.profile);
  if(bodyCoverage && sideCoverage) return false;
  if(bodyCoverage && !s.local.recipe->bodyAsset && strcmp(s.profile->prefabSha,ClothBodyContactPrefab)) return false;
  auto root=CollisionTransform(ClothTarget(s_cloth.animator)); if(!root) return false;
  struct Node { void *t; int depth; }; std::vector<Node> nodes{{root,0}};
  for(size_t n=0;n<nodes.size();++n) {
    if(nodes.size()>2048) return false;
    const auto node=nodes[n]; if(ClothOwnedRoot(node.t)) continue;
    if(s.profile->candidateIgnoredCount || s.profile->prebuildOmittedCount || (s.profile->originalExcludedCount&&!s.profile->excludedBranchCount)) {
      void *type=il2cpp_type_get_object(il2cpp_class_get_type(g_componentClass)),*go=nullptr,*array=nullptr,*args[]{type};uintptr_t count=0;
      if(!type||!ClothInvoke(s_clothUnity.getGO,node.t,nullptr,go)||!ClothInvoke(s_clothUnity.components,go,args,array)||
          !array||!ClothBoneHold(array)||!ClothArray(array,"UnityEngine.Component[]",count)||count>32)return false;
      for(size_t k=0;k<count;++k) {
        auto c=reinterpret_cast<void**>((char*)array+32)[k];
        if(!c||!ClothTypeIs(il2cpp_class_get_type(il2cpp_object_get_class(c)),"UnityEngine.SkinnedMeshRenderer"))continue;
        void *bones=nullptr;uintptr_t length=0;
        if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(c),"get_bones","UnityEngine.Transform[]"),c,nullptr,bones)||
            !bones||!ClothBoneHold(bones)||!ClothArray(bones,"UnityEngine.Transform[]",length)||length>512)return false;
        for(size_t b=0;b<length;++b) {
          auto t=reinterpret_cast<void**>((char*)bones+32)[b];
          for(int j=0;j<s.profile->candidateIgnoredCount;++j)
            if(!s.profile->Foreign(s.profile->candidateIgnored[j])&&t==ClothTarget(s.bones[s.profile->candidateIgnored[j]].bone))return false;
          if(!s.profile->excludedBranchCount)for(const auto &r:s.originalExcluded)if(t==ClothTarget(r.bone))return false;
          for(const auto &r:s.prebuildOmitted)if(t==ClothTarget(r.bone))return false;
        }
      }
    }
    char name[128]{}; CollisionName(node.t,name,sizeof(name));
    for(int k=0;k<s.profile->rendererCount+int(bodyCoverage || sideCoverage);++k) {
      const bool body=k==s.profile->rendererCount;
      const auto &asset=body?(sideCoverage?ClothSideBodyAsset:s.local.recipe->bodyAsset?*s.local.recipe->bodyAsset:ClothBodyContactAsset):s.profile->renderers[k];
      if(strcmp(name,asset.name)) continue;
      if(asset.parent) {
        char parent[128]{};CollisionName(CollisionParent(node.t),parent,sizeof(parent));
        if(strcmp(parent,asset.parent))continue;
      }
      auto &r=body?(sideCoverage?s.local.side.renderer:s.local.bodyRenderer):s.renderers[k]; if(r.renderer.handle) return false;
      void *type=il2cpp_type_get_object(il2cpp_class_get_type(g_componentClass)),*go=nullptr,*array=nullptr,*args[]{type}; uintptr_t count=0;
      if(!type || !ClothInvoke(s_clothUnity.getGO,node.t,nullptr,go) ||
          !ClothInvoke(s_clothUnity.components,go,args,array) || !array || !ClothBoneHold(array) ||
          !ClothArray(array,"UnityEngine.Component[]",count) || count>32) return false;
      for(size_t j=0;j<count;++j) {
        auto c=reinterpret_cast<void**>((char*)array+32)[j];
        if(!c || !ClothTypeIs(il2cpp_class_get_type(il2cpp_object_get_class(c)),"UnityEngine.SkinnedMeshRenderer")) continue;
        if(r.renderer.handle) return false; r.renderer=ClothProtect(c); if(!r.renderer.handle) return false;
      }
      if(!ClothBoneRendererAssetCheck(r,asset,true,body?-1:k)) {
        Log("[CLOTH-BONE-BINDING] renderer=%s match=0 phase=capture",name); return false;
      }
    }
    int children=-1;
    if(!ClothValue(s_clothUnity.childCount,node.t,children) || children<0 || children>128 || (node.depth>=32 && children)) return false;
    for(int k=0;k<children;++k) {
      void *child=nullptr,*args[]{&k}; if(!ClothInvoke(s_clothUnity.child,node.t,args,child) || !child) return false;
      nodes.push_back({child,node.depth+1});
    }
  }
  for(auto &r:s.renderers) if(!r.renderer.handle) return false;
  if(bodyCoverage && !s.local.bodyRenderer.renderer.handle) return false;
  if(sideCoverage && !s.local.side.renderer.renderer.handle) return false;
  Log("[CLOTH-BONE-BINDING] profile=%s renderers=%zu naturalReference=1 bindposes=1 GPUContentHashVerified=0",
      s.profile->signature,s.renderers.size()); return true;
}
static bool ClothBonePassiveAttachments() {
  auto &s=ClothBoneState();
  for(int k=0;k<s.profile->candidateIgnoredCount;++k) {
    const int n=s.profile->candidateIgnored[k];if(s.profile->Foreign(n))continue;const auto &b=s.profile->bones[n];auto t=ClothTarget(s.bones[n].bone);
    int children=-1;void *go=nullptr,*array=nullptr;uintptr_t count=0;
    void *type=il2cpp_type_get_object(il2cpp_class_get_type(g_componentClass)),*args[]{type};
    if(!t||b.parent<0||(!b.attribute)||!ClothValue(s_clothUnity.childCount,t,children)||children||
        !type||!ClothInvoke(s_clothUnity.getGO,t,nullptr,go)||!ClothInvoke(s_clothUnity.components,go,args,array)||
        !array||!ClothBoneHold(array)||!ClothArray(array,"UnityEngine.Component[]",count)||(count!=2&&!(s.profile->runtimeGenerated&&count==1)))return false;
    bool transform=false,capsule=false;
    for(size_t j=0;j<count;++j) {
      auto c=reinterpret_cast<void**>((char*)array+32)[j];if(!c)return false;
      const auto typeInfo=il2cpp_class_get_type(il2cpp_object_get_class(c));
      if(c==t&&ClothTypeIs(typeInfo,"UnityEngine.Transform"))transform=true;
      else if(ClothTypeIs(typeInfo,"BeyondDynamicBone.BeyondBoneCapsuleCollider"))capsule=true;
      else if(s.profile->runtimeGenerated&&(ClothTypeIs(typeInfo,"BeyondDynamicBone.BeyondBoneSphereCollider")||ClothTypeIs(typeInfo,"BeyondDynamicBone.BeyondBonePlaneCollider")))capsule=true;
      else return false;
    }
    if(!transform||(!capsule&&!(s.profile->runtimeGenerated&&count==1)))return false;
    Log("[CLOTH-BONE-PASSIVE] component=%s bone=%s parent=%s originalAttribute=%d candidateExcluded=1 skinned=0 children=0 colliderComponentUnchanged=1",
        s.profile->component,b.name,b.parentName,b.attribute);
  }
  return true;
}
static bool ClothBoneAttachmentsIdentity() {
  for(auto &a:ClothBoneState().attachments) {
    auto bbc=ClothTarget(a.bbc),root=ClothTarget(a.root);
    void *data=nullptr,*process=nullptr,*roots=nullptr;
    if(!bbc || !root || CollisionParent(root)!=ClothTarget(a.parent) ||
        !CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",process) || process!=CollisionGc(a.process) ||
        !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data) ||
        data!=CollisionGc(a.data) || !CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots) ||
        roots!=CollisionGc(a.roots) || CollisionCount(roots)!=a.count ||
        CollisionItem(roots,a.index,"UnityEngine.Transform")!=root) return false;
  }
  return true;
}
static bool ClothBoneCaptureAttachment(void *bbc,void *process,void *data,void *roots,int index,int count,void *root) {
  auto &s=ClothBoneState();const int bone=ClothBoneIndex(root);
  const bool sourceExcluded=s.profile&&s.profile->runtimeGenerated&&
      std::any_of(s.excludedBranches.begin(),s.excludedBranches.end(),[&](const ClothBoneExcludedRef &r){return ClothTarget(r.bone)==root;});
  if(!s.profile || (!sourceExcluded&&!s.profile->ExcludedBranch(bone))) return false;
  for(auto &a:s.attachments) if(ClothTarget(a.bbc)==bbc && a.index==index)
    return ClothTarget(a.root)==root;
  ClothBoneAttachment a{};a.bbc=ClothProtect(bbc);a.root=ClothProtect(root);a.parent=ClothProtect(CollisionParent(root));
  a.process=process?ClothBoneHold(process):0;a.data=ClothBoneHold(data);a.roots=ClothBoneHold(roots);a.index=index;a.count=count;
  s.attachments.push_back(a);
  if(!a.bbc.handle || !a.root.handle || !a.parent.handle || (process && !a.process) || !a.data || !a.roots) return false;
  Log("[CLOTH-BONE-ATTACHMENT] component=%s nativeBBC=%p root=%s excludedBranch=1 otherEnabledWrites=0 otherSimulationConfigWrites=0",
      s.profile->component,bbc,sourceExcluded?"source-excluded-subtree":s.profile->bones[bone].name);
  return true;
}
static bool ClothBoneBindingIdentity() {
  auto &s=ClothBoneState();
  const auto reject=[&](const char *part,int index=-1) {
    Log("[CLOTH-BONE-BINDING] component=%s actorSlot=%u frame=%d part=%s index=%d identity=0 restoring=1",
        s.profile?s.profile->component:"unknown",s_clothActorIndex,ClothFrame(),part,index);return false;
  };
  if(s.prebuild.captured&&!ClothBonePrebuildIdentity())return reject("prebuild");
  if(!s.profile || s.renderers.size()!=size_t(s.profile->rendererCount))return reject("profile");
  if(!ClothBoneAttachmentsIdentity())return reject("attachment");
  if(!ClothBoneOriginalExcludedIdentity())return reject("excluded");
  for(size_t n=0;n<s.renderers.size();++n) if(!ClothBoneRendererCheck(n,false)) return reject("renderer",int(n));
  for(size_t n=0;n<s.bones.size();++n) {auto &b=s.bones[n];
    if(!ClothTarget(b.bone) || CollisionParent(ClothTarget(b.bone))!=ClothTarget(b.parent))return reject("bone-parent",int(n));}
  if(!ClothBoneBodyBindingIdentity())return reject("body-collider");
  return ClothBoneSideIdentity()||reject("side-collider");
}
static bool ClothBoneTeamRegistered(void *process,int team) {
  void *manager=nullptr,*dict=nullptr,*registered=nullptr;
  if(!process || team<=0 || !ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager) ||
      !CollisionField(manager,"clothProcessDict","System.Collections.Generic.Dictionary<System.Int32,BeyondDynamicBone.ClothProcess>",dict) || !dict) return false;
  void *args[]{&team};
  return ClothInvoke(SurfaceMethod(il2cpp_object_get_class(dict),"get_Item","BeyondDynamicBone.ClothProcess","System.Int32"),dict,args,registered) && registered==process;
}
#include "cloth_bonecloth_ownership.h"
static bool ClothBoneControlledMembers(void *collider,int ownTeam,int &extra) {
  extra=0;
  for(int k=0;k<s_clothBoneCount;++k) {
    const auto &consumer=s_clothBoneSlots[k];bool uses=false;
    if(!consumer.lease || !consumer.tx.candidate.issued || !(consumer.owner==ClothBoneState().owner)) continue;
    const auto phase=consumer.tx.phase==eiem_cloth_rebuild::Phase::Retained?consumer.tx.retainedFrom:consumer.tx.phase;
    if(phase==eiem_cloth_rebuild::Phase::InstallOriginal || phase==eiem_cloth_rebuild::Phase::BuildOriginal ||
        phase==eiem_cloth_rebuild::Phase::RestoreEnabled || phase==eiem_cloth_rebuild::Phase::Complete) continue;
    for(auto &c:consumer.additionalColliders) if(ClothTarget(c.ref)==collider) uses=true;
    if(!uses) continue;
    auto process=CollisionGc(consumer.process[1]);int team=0;bool member=false,listed=false;int count=-1;
    if(!process || !ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"get_TeamId","System.Int32"),process,team)) return false;
    if(team<=0 || team==ownTeam) continue;
    if(!CollisionTeams(collider,team,member,count)) return false;
    if(!member) continue;
    void *current=nullptr;
    auto bbc=ClothTarget(consumer.bbc);
    if(!bbc || !CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",current) || current!=process ||
        !ClothBoneTeamRegistered(process,team) || !CollisionProcessContains(process,collider,listed) || !listed) return false;
    ++extra;
  }
  return true;
}
static bool ClothBoneAdditionalIdentity(void *process,int team,bool restoring);
static bool ClothBoneSharedMembership(const ClothBoneSharedTeam &snapshot,void *collider,std::vector<int> &members) {
  auto &s=ClothBoneState();auto bbc=ClothTarget(snapshot.bbc);void *current=nullptr;
  if(!bbc||!ClothAnchorUnderOwner(CollisionTransform(bbc))||
      !CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",current)||!current)return false;
  const ClothBoneRuntime *peer=nullptr;
  for(const auto &p:s_clothBoneSlots) if(p.lease&&p.tx.lease&&p.owner==s.owner&&p.command==s.command&&
      p.bbc.id==snapshot.bbc.id&&ClothTarget(p.bbc)==bbc) {
    if(peer)return false;peer=&p;
  }
  auto append=[&](int team) {
    if(team<=0||std::find(members.begin(),members.end(),team)!=members.end())return false;
    members.push_back(team);return true;
  };
  if(!peer) {
    bool member=false,listed=false;int count=-1;
    return current==CollisionGc(snapshot.process)&&ClothBoneTeamRegistered(current,snapshot.team)&&
        CollisionTeams(collider,snapshot.team,member,count)&&member&&CollisionProcessContains(current,collider,listed)&&listed&&append(snapshot.team);
  }
  const auto &p=*peer;
  bool captured=false;for(auto handle:p.process)if(handle&&CollisionGc(handle)==CollisionGc(snapshot.process))captured=true;
  if(!captured)return false;
  eiem_cloth_rebuild::Identity identity{p.owner,uint64_t(uintptr_t(bbc)),uint64_t(uintptr_t(current)),0,0};
  void *data=nullptr,*data2=nullptr;
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data)||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"GetSerializeData2","BeyondDynamicBone.ClothSerializeData2"),bbc,nullptr,data2))return false;
  identity.data=uint64_t(uintptr_t(data));identity.data2=uint64_t(uintptr_t(data2));
  if(!(identity==p.tx.installed)&&!p.tx.AllowsInstallRepair(identity)&&!p.tx.CanReturnData2(identity))return false;
  const auto phase=p.tx.phase==eiem_cloth_rebuild::Phase::Retained?p.tx.retainedFrom:p.tx.phase;
  const bool stable=phase==eiem_cloth_rebuild::Phase::Active||phase==eiem_cloth_rebuild::Phase::Complete;
  if(stable) {
    int team=0,count=-1;bool member=false,listed=false;
    return ClothValue(SurfaceMethod(il2cpp_object_get_class(current),"get_TeamId","System.Int32"),current,team)&&
        ClothBoneTeamRegistered(current,team)&&CollisionTeams(collider,team,member,count)&&member&&
        CollisionProcessContains(current,collider,listed)&&listed&&append(team);
  }
  void *manager=nullptr,*dict=nullptr;
  if(!ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager)||
      !CollisionField(manager,"clothProcessDict","System.Collections.Generic.Dictionary<System.Int32,BeyondDynamicBone.ClothProcess>",dict)||!dict)return false;
  const auto dc=il2cpp_object_get_class(dict);std::vector<int> teams{snapshot.team};
  std::vector<void*> allowed;
  for(int slot=0;slot<3;++slot) {
    auto process=CollisionGc(p.process[slot]);
    const bool issued=slot==0|| (slot==1?p.tx.candidate.issued:p.tx.restoration.issued);
    if(!process||!issued)continue;allowed.push_back(process);
    int team=0;if(!ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"get_TeamId","System.Int32"),process,team))return false;
    if(team>0&&std::find(teams.begin(),teams.end(),team)==teams.end())teams.push_back(team);
  }
  for(int team:teams) {
    bool member=false,present=false;int count=-1;void *args[]{&team},*boxed=nullptr;
    if(team<=0||!CollisionTeams(collider,team,member,count)||
        !ClothInvoke(SurfaceMethod(dc,"ContainsKey","System.Boolean","System.Int32"),dict,args,boxed)||!boxed)return false;
    present=ClothUnboxBool(boxed);
    if(present&&member) {
      void *registered=nullptr;
      if(!ClothInvoke(SurfaceMethod(dc,"get_Item","BeyondDynamicBone.ClothProcess","System.Int32"),dict,args,registered)||
          std::find(allowed.begin(),allowed.end(),registered)==allowed.end())return false;
      if(member) {bool listed=false;if(!CollisionProcessContains(registered,collider,listed)||!listed)return false;}
    } else if(member&&!p.tx.disposeOriginalIssued&&!p.tx.disposeCandidateIssued) return false;
    if(member&&!append(team))return false;
  }
  return true;
}
static bool ClothBoneShareAdopt(void *process,int team) {
  auto &s=ClothBoneState();if(!process)return false;
  for(auto &observer:s_clothBoneSlots) if(&observer!=&s&&observer.owner==s.owner&&observer.command==s.command) {
    for(auto &known:observer.sharedTeams) if(known.bbc.id==s.bbc.id&&ClothTarget(known.bbc)==ClothTarget(s.bbc)) {
      if(CollisionGc(known.process)==process&&known.team==team)continue;
      if(!ClothBoneTeamRegistered(process,team))return false;
      bool owned=false;for(auto h:s.process)if(h&&CollisionGc(h)==CollisionGc(known.process))owned=true;
      if(!owned)return false;
      auto handle=il2cpp_gchandle_new(process,false);if(!handle)return false;
      observer.holds.push_back(handle);known.process=handle;known.team=team;
      Log("[CLOTH-BONE-SHARED-HANDOFF] observer=%s peer=%s Process=%p Team=%d sameOwnerCommand=1 colliderGeometryWrites=0",
          observer.profile?observer.profile->component:"pending",s.profile?s.profile->component:"pending",process,team);
    }
  }
  return true;
}
#include "cloth_bonecloth_producer.h"
static bool ClothBoneColliderIdentity(void *process,int team,bool restoring=false) {
  auto &s=ClothBoneState();
  if(s.colliderTeams.size()!=s.colliders.size()) return false;
  if(!restoring && (!ClothBoneNativeProducersIdentity()||!ClothBoneOwnershipIdentity())) return false;
  for(size_t n=0;n<s.colliders.size();++n) {
    auto c=ClothTarget(s.colliders[n]); bool member=false,listed=false; int count=-1;
    const bool required=ClothBoneOriginalColliderRequired(n,process,restoring);
    int controlled=0;
    if(!restoring && !ClothBoneControlledMembers(c,team,controlled)) return false;
    std::vector<int> shared;
    if(!restoring) for(int ix:s.colliderTeams[n]) {
      if(ix<0||size_t(ix)>=s.sharedTeams.size()||!ClothBoneSharedMembership(s.sharedTeams[ix],c,shared))return false;
    }
    if(!c || !ClothAnchorUnderOwner(CollisionTransform(c)) || !CollisionTeams(c,team,member,count) || member!=required ||
        count<int(required) || (!restoring && count!=int(shared.size())+int(required)+controlled) ||
        !CollisionProcessContains(process,c,listed) || listed!=required) return false;
    if(!s.colliderTransforms.empty() && (s.colliderTransforms.size()!=s.colliders.size() || s.colliderParents.size()!=s.colliders.size() ||
        CollisionTransform(c)!=ClothTarget(s.colliderTransforms[n]) ||
        CollisionParent(ClothTarget(s.colliderTransforms[n]))!=ClothTarget(s.colliderParents[n]))) return false;
  }
  return (restoring || ClothBoneApronLayerReadback(s,process)) &&
      ClothBoneAdditionalIdentity(process,team,restoring) && ClothBoneBodyRegistration(process,team,restoring) && ClothBoneSideRegistration(process,team,restoring);
}
static bool ClothBoneAdditionalIdentity(void *process,int team,bool restoring) {
  auto &s=ClothBoneState();
  const bool candidate=process==CollisionGc(s.process[1]);
  bool checked[eiem_cloth_rebuild::BatchCapacity]{};
  for(auto &c:s.additionalColliders) {
    auto collider=ClothTarget(c.ref);bool member=false,listed=false;int count=-1;
    if(!collider || !ClothAnchorUnderOwner(CollisionTransform(collider)) ||
        CollisionTransform(collider)!=ClothTarget(c.transform) || CollisionParent(ClothTarget(c.transform))!=ClothTarget(c.parent) ||
        !CollisionTeams(collider,team,member,count) || !CollisionProcessContains(process,collider,listed) ||
        member!=(candidate&&!restoring) || listed!=(candidate&&!restoring)) return false;
    if(restoring) continue;
    if(c.donor<0 || c.donor>=s_clothBoneCount || !(s.dependencies&(1u<<c.donor))) return false;
    const auto &donor=s_clothBoneSlots[c.donor];
    if(!donor.pending || !donor.lease || donor.stopRequested || donor.tx.cancelled ||
        donor.tx.phase!=eiem_cloth_rebuild::Phase::Active || !donor.teamModeConfirmed || !(donor.owner==s.owner) ||
        c.index<0 || c.index>=int(donor.colliders.size()) || ClothTarget(donor.colliders[c.index])!=collider) return false;
    if(checked[c.donor]) continue;
    const int previous=s_clothBoneContext;s_clothBoneContext=c.donor;
    bool valid=false;
    __try { valid=ClothBoneColliderIdentity(CollisionGc(donor.process[1]),donor.team[1]); }
    __finally { s_clothBoneContext=previous; }
    if(!valid) return false;checked[c.donor]=true;
  }
  return true;
}
static bool ClothBoneCaptureAdditionalColliders() {
  auto &s=ClothBoneState();const auto &p=*s.profile;
  if(p.bodyColliderSourceCount<0 || p.bodyColliderSourceCount>16) return false;
  for(int n=0;n<p.bodyColliderSourceCount;++n) {
    const auto &source=p.bodyColliderSources[n];int slot=-1;
    for(int k=0;k<s_clothBoneCount;++k) if((s.dependencies&(1u<<k)) && s_clothBoneSlots[k].profile &&
        !strcmp(s_clothBoneSlots[k].profile->component,source.component) &&
        !strcmp(s_clothBoneSlots[k].profile->prefabSha,p.prefabSha)) { if(slot>=0) return false;slot=k; }
    if(slot<0) return false;const auto &donor=s_clothBoneSlots[slot];
    if(source.index<0 || source.index>=int(donor.colliders.size())) return false;
    auto collider=ClothTarget(donor.colliders[source.index]);if(!collider) return false;
    for(auto &c:s.colliders) if(ClothTarget(c)==collider) return false;
    for(auto &c:s.additionalColliders) if(ClothTarget(c.ref)==collider) return false;
    ClothBoneAdditionalCollider c{};c.donor=slot;c.index=source.index;
    c.ref=ClothProtect(collider);c.transform=ClothProtect(CollisionTransform(collider));c.parent=ClothProtect(CollisionParent(ClothTarget(c.transform)));
    s.additionalColliders.push_back(c);
    if(!c.ref.handle || !c.transform.handle || !c.parent.handle) return false;
  }
  if(!ClothBoneAdditionalIdentity(CollisionGc(s.process[0]),s.team[0],false)) return false;
  if(!s.additionalColliders.empty()) Log("[CLOTH-BONE-BODY-COLLIDERS] component=%s originals=%zu added=%zu geometryWrites=0 registrationPending=1 source=confirmed-dependency",
      p.component,s.colliders.size(),s.additionalColliders.size());
  return true;
}
static bool ClothBoneColliderTeamsAbsent(int team) {
  auto absent=[team](const ClothRef &ref) {
    bool member=true;int count=-1;void *obj=nullptr;auto life=ClothInspect(ref,obj);
    return life==ClothLife::Destroyed || (life==ClothLife::Alive && CollisionTeams(obj,team,member,count) && !member);
  };
  for(auto &c:ClothBoneState().colliders) if(!absent(c)) return false;
  for(auto &c:ClothBoneState().additionalColliders) if(!absent(c.ref)) return false;
  if(ClothBoneState().local.bodyCollider.handle && !absent(ClothBoneState().local.bodyCollider)) return false;
  for(const auto &r:ClothBoneState().local.fittedBody)if(r.collider.handle&&!absent(r.collider))return false;
  return true;
}
static bool ClothBoneDependencyCollider(const ClothBoneRuntime &consumer,const ClothBoneRuntime &producer,int collider,void *transform) {
  if(!producer.profile || !transform)return false;
  const auto &active=ClothBoneCandidate(producer);
  if(!ClothBoneIdentityBudget(active.boneCount,active.EffectiveCount())||producer.bones.size()!=size_t(active.boneCount))return false;
  bool originalVolume=false;
  if(producer.supportCreated && ClothBonePair(producer,consumer) && consumer.local.recipe) {
    const auto &r=*consumer.local.recipe;
    for(int n=0;n<r.contactOmissionCount;++n)if(r.contactOmissions[n]==collider)originalVolume=true;
  }
  for(int n=0;n<active.boneCount;++n) {
    const bool authored=originalVolume&&n<producer.profile->boneCount&&producer.profile->bones[n].attribute==2;
    if((active.bones[n].attribute==2||authored)&&ClothTarget(producer.bones[n].bone)==transform)return true;
  }
  return false;
}
static bool ClothBoneCapturePeerRoots(const ClothInstance &peer,void *bbc,void *process,void *data,void *roots) {
  auto &s=ClothBoneState();const auto &p=*s.profile;
  int count=CollisionCount(roots); if(count<0 || count>64) return false;
  for(int n=0;n<count;++n) {
    auto root=CollisionItem(roots,n,"UnityEngine.Transform"); if(!root) return false;
    const bool authoredInput=ClothBoneOwnershipInputRoot(bbc,root);
    int inputAncestors=0;
    for(size_t b=0;b<s.bones.size()&&b<size_t(s.profile->boneCount);++b) if(!s.profile->Foreign(int(b))&&
        (p.CandidateAttribute(int(b))==2 || (eiem_cloth_asset::SourceSeparatedCoat(p)&&p.ReleasedFixed(int(b))) || eiem_cloth_asset::SourceForkCoatRelease(p,int(b)) || eiem_cloth_asset::SourceUpperCoatRelease(p,int(b)) || eiem_cloth_asset::SourceUpperCoatRoot(p,int(b))>=0 || p.InputAnchor(int(b)) || eiem_cloth_asset::SourceShortRelease(p,int(b)) || eiem_cloth_asset::SourceShortWaist(p,int(b)) || eiem_cloth_asset::SourceApronRelease(p,int(b)))) {
      if(ClothBoneOwnershipProved(bbc,int(b)))continue;
      auto t=ClothTarget(s.bones[b].bone);
      void *a=nullptr,*bResult=nullptr,*argsA[]{t},*argsB[]{root};
      auto method=SurfaceMethod(g_transformClass,"IsChildOf","System.Boolean","UnityEngine.Transform");
      if(!ClothInvoke(method,root,argsA,a) || !a ||
          !ClothInvoke(method,t,argsB,bResult) || !bResult || ClothUnboxBool(bResult)) {
        Log("[CLOTH-BONE-COLLIDER-GATE] component=%s peer=%s bone=%s reason=unproved-peer-subtree-or-ancestry-read",p.component,peer.name,s.profile->bones[b].name);return false;
      }
      if(ClothUnboxBool(a)) {
        if(authoredInput)++inputAncestors;
        else if(!ClothBoneCaptureAttachment(bbc,process,data,roots,n,count,root)) {
          Log("[CLOTH-BONE-COLLIDER-GATE] component=%s peer=%s bone=%s reason=unproved-descendant-attachment",p.component,peer.name,s.profile->bones[b].name);return false;
        }
      }
    }
    if(inputAncestors)Log("[CLOTH-BONE-COLLIDER-GATE] component=%s peer=%s rootIndex=%d moveAncestors=%d reason=verified-existing-fixed-input-root peerSubtreeOutputsStillChecked=1 peerWrites=0",p.component,peer.name,n,inputAncestors);
  }
  return true;
}
static bool ClothBoneColliderSourceMatch(const ClothBoneProfile &p,const ClothBoneColliderAsset &asset,void *t) {
  if(!t||!asset.name||!asset.parent||!ClothAnchorUnderOwner(t))return false;
  char name[128]{},parentName[128]{};auto parent=CollisionParent(t);
  CollisionName(t,name,sizeof(name));CollisionName(parent,parentName,sizeof(parentName));
  if(!parent||strcmp(name,asset.name))return false;
  if(asset.parentIsAnimator) {
    auto animator=ClothTarget(s_cloth.animator);
    return p.runtimeGenerated&&animator&&parent==CollisionTransform(animator);
  }
  return !strcmp(parentName,asset.parent);
}
static bool ClothBoneCaptureColliders(void *process,void *list,int team) {
  auto &s=ClothBoneState(); const auto &p=*s.profile;
  const auto fail=[&](const char *reason,int collider=-1,int peer=-1){
    Log("[CLOTH-BONE-COLLIDER-GATE] session=%llu generation=%llu command=%u frame=%d component=%s instance=%d Process=%p Team=%d collider=%d peer=%s reason=%s originalUnchanged=1",
        s.owner.session,s.owner.generation,s.command,ClothFrame(),p.component,s.bbc.id.instance,process,team,collider,
        peer>=0&&peer<s_cloth.count?s_cloth.instances[peer].name:"none",reason);return false;};
  if(CollisionCount(list)!=p.colliderCount || p.colliderCount<0 || p.colliderCount>=ClothColliderCapacity ||
      !ClothBoneTeamRegistered(process,team)) return fail("source-list-count-or-Team-registry");
  void *processOwner=nullptr;
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(process),"get_cloth","BeyondDynamicBone.BeyondBoneCloth"),process,nullptr,processOwner) ||
      processOwner!=ClothTarget(s.bbc)) return fail("source-Process-cloth-identity");
  if(!ClothBoneCaptureOwnership())return fail("peer-output-ownership");
  s.colliderTeams.resize(p.colliderCount);
  uint32_t linkedDependencies=0;
  for(int n=0;n<p.colliderCount;++n) {
    auto c=CollisionItem(list,n,"BeyondDynamicBone.ColliderComponent"); auto t=CollisionTransform(c);
    if(!c || !ClothBoneColliderSourceMatch(p,p.colliders[n],t)) {
      char name[128]{},parent[128]{};CollisionName(c,name,sizeof(name));CollisionName(CollisionParent(t),parent,sizeof(parent));
      Log("[CLOTH-BONE-COLLIDER-SOURCE] component=%s collider=%d expected=%s/%s actual=%s/%s parentIsAnimator=%d confirmed=0",
          p.component,n,p.colliders[n].parent,p.colliders[n].name,parent,name,p.colliders[n].parentIsAnimator);
      return fail("source-collider-name-parent-or-owner",n);
    }
    for(auto &old:s.colliders) if(ClothTarget(old)==c) return fail("duplicate-source-collider",n);
    auto ref=ClothProtect(c); if(!ref.handle) return fail("collider-handle",n); s.colliders.push_back(ref);
    auto tr=ClothProtect(t),pr=ClothProtect(CollisionParent(t));
    s.colliderTransforms.push_back(tr);s.colliderParents.push_back(pr);
    if(!tr.handle || !pr.handle) return fail("collider-transform-parent-handle",n);
    if(p.colliders[n].parentIsAnimator)Log("[CLOTH-BONE-COLLIDER-SOURCE] component=%s collider=%d name=%s parentIsAnimator=1 confirmed=1 sourceParentRole=selected-Animator liveParentIdentity=1 geometryWrites=0",
        p.component,n,p.colliders[n].name);
    for(int k=0;k<s_clothBoneCount;++k) if(s.dependencies&(1u<<k)) {
      const auto &producer=s_clothBoneSlots[k];
      if(!producer.pending || !producer.lease || !producer.teamModeConfirmed || producer.stopRequested ||
          producer.tx.cancelled || producer.tx.phase!=eiem_cloth_rebuild::Phase::Active || !(producer.owner==s.owner) || producer.command!=s.command) return fail("dependency-not-active",n);
      if(ClothBoneDependencyCollider(s,producer,n,t))linkedDependencies|=1u<<k;
    }
  }
  for(int k=0;k<s_clothBoneCount;++k)if((s.dependencies&(1u<<k))&&(ClothBoneRibbonPair(s_clothBoneSlots[k],s)||ClothBoneGeneratedPair(s_clothBoneSlots[k],s))) {
    const auto &peer=s_clothBoneSlots[k];
    if(!ClothBonePair(peer,s)||!peer.lease||!peer.teamModeConfirmed||peer.tx.phase!=eiem_cloth_rebuild::Phase::Active||peer.stopRequested)return fail("surface-dependency-not-active");
    linkedDependencies|=1u<<k;
  }
  if(linkedDependencies!=s.dependencies)return fail("dependency-collider-links");
  for(int k=0;k<s_cloth.count;++k) {
    auto &i=s_cloth.instances[k]; if(i.ref.id==s.bbc.id) continue;
    auto bbc=ClothTarget(i.ref); void *op=nullptr,*data=nullptr,*constraint=nullptr,*otherList=nullptr;
    if(!bbc || !ClothInvoke(i.api.serialize,bbc,nullptr,data) || !data || data==CollisionGc(s.data) ||
        !CollisionList(data,constraint,otherList) || otherList==list ||
        !CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",op)) return fail("peer-config-or-Process-read",-1,k);
    void *roots=nullptr;
    if(!CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots)) return fail("peer-root-list-read",-1,k);
    if(!ClothBoneCapturePeerRoots(i,bbc,op,data,roots))return fail("peer-root-ownership",-1,k);
    if(!op) continue;
    int otherTeam=0;
    if(!ClothValue(SurfaceMethod(il2cpp_object_get_class(op),"get_TeamId","System.Int32"),op,otherTeam)) return fail("peer-Team-read",-1,k);
    if(otherTeam<=0) continue;
    std::vector<int> shared;
    for(int n=0;n<p.colliderCount;++n) {
      bool member=false; int total=-1;
      if(!CollisionTeams(ClothTarget(s.colliders[n]),otherTeam,member,total)) return fail("peer-collider-membership-read",n,k);
      if(member) shared.push_back(n);
    }
    if(shared.empty()) continue;
    for(auto &other:s.sharedTeams) if(other.team==otherTeam || CollisionGc(other.process)==op) return fail("duplicate-peer-Team-or-Process",-1,k);
    auto ref=ClothProtect(bbc); auto handle=ClothBoneHold(op);
    if(!ref.handle || !handle) { ClothFree(ref); return fail("peer-reference-handle",-1,k); }
    const int ix=int(s.sharedTeams.size()); s.sharedTeams.push_back({ref,handle,otherTeam});
    for(int n:shared) s.colliderTeams[n].push_back(ix);
  }
  if(!ClothBoneCaptureNativeProducers())return fail("native-producer-identity");
  if(!ClothBoneColliderIdentity(process,team))return fail("registered-membership-or-identity");
  if(!ClothBoneAttachmentsIdentity())return fail("attachment-identity");
  Log("[CLOTH-BONE-COLLIDERS] count=%zu knownOtherOwnerTeams=%zu geometryWrites=0 otherBBCWrites=0 unknownSharing=0 emptyListMeansNoBodyContact=1",
      s.colliders.size(),s.sharedTeams.size()); return true;
}
