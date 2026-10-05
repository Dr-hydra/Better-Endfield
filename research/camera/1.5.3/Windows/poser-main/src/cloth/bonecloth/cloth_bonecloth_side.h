#pragma once
static bool ClothBoneSideSource() {
  auto &r=ClothBoneState().local.side.renderer;auto renderer=ClothTarget(r.renderer);bool enabled=false;
  return renderer && ClothValue(SurfaceMethod(il2cpp_object_get_class(renderer),"get_enabled","System.Boolean"),renderer,enabled) && enabled &&
      ClothBoneRendererAssetCheck(r,ClothSideBodyAsset,false);
}
static bool ClothBoneSideGeometry(size_t n) {
  const auto &l=ClothBoneState().local.side;
  if(n>=l.shapes.size() || n>=std::size(ClothSideShapes)) return false;
  const auto &r=l.shapes[n];const auto &fit=ClothSideShapes[n];
  auto c=ClothTarget(r.collider),t=ClothTarget(r.transform),p=ClothTarget(r.parent);
  Vector3 position{},scale{};Quaternion rotation{};
  if(!c || !t || !p || fit.bone<0 || size_t(fit.bone)>=l.renderer.bones.size() ||
      p!=ClothTarget(l.renderer.bones[fit.bone]) || CollisionTransform(c)!=t || CollisionParent(t)!=p || !ClothAnchorUnderOwner(p) ||
      !SurfaceVisiblePose(t,position,rotation,scale) || !ClothSameLocal(position,rotation,fit.center,Quaternion{0,0,0,1}) ||
      fabsf(scale.x-1)>1e-5f || fabsf(scale.y-1)>1e-5f || fabsf(scale.z-1)>1e-5f) return false;
  const auto g=CollisionReadGeometry(c);
  return g.valid && g.enabled && g.active && g.uniform && !strcmp(g.type,"BeyondBoneSphereCollider") &&
      ClothFinitePosition(g.center) && eiem_collision::Length(CollisionV(g.center))<1e-6 &&
      std::isfinite(g.size.x) && fabsf(g.size.x-fit.radius)<1e-6f;
}
static bool ClothBoneSideProducerList(bool healthy) {
  auto &s=ClothBoneState();const auto &l=s.local.side;
  if(l.producer<0 || size_t(l.producer)>=s.nativeProducers.size()) return false;
  const auto &p=s.nativeProducers[l.producer];auto bbc=ClothTarget(p.bbc);
  void *process=nullptr,*data=nullptr,*constraint=nullptr,*list=nullptr;
  if(!bbc || !CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",process) || process!=CollisionGc(p.process) ||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data) ||
      data!=CollisionGc(p.data) || !CollisionList(data,constraint,list) ||
      constraint!=CollisionGc(l.constraint) || list!=CollisionGc(l.list)) return false;
  return !healthy || ClothBoneNativeProducerIdentity(p);
}
static bool ClothBoneSideIdentity() {
  auto &l=ClothBoneState().local.side;
  if(!l.created) return true;
  if(l.shapes.size()!=std::size(ClothSideShapes) || !ClothBoneSideSource() || !ClothBoneSideProducerList(true)) return false;
  for(size_t n=0;n<l.shapes.size();++n) if(!ClothBoneSideGeometry(n)) return false;
  return true;
}
static bool ClothBoneSideCreate() {
  if(!ClothOnMainThread()) return false;
  auto &s=ClothBoneState();auto &local=s.local;auto &l=local.side;
  if(!local.requested || !s.profile || !ClothBoneSideRecipeFor(*s.profile) || l.created || !l.shapes.empty() ||
      !ClothOwns(s.owner) || s.stopRequested || !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 ||
      !s_cloth.discovery.complete || !ClothBoneSideSource() || !local.recipe->contactProducer) return false;
  for(size_t n=0;n<s.nativeProducers.size();++n) {
    char name[128]{};CollisionName(ClothTarget(s.nativeProducers[n].bbc),name,sizeof(name));
    if(!strcmp(name,local.recipe->contactProducer)) {if(l.producer>=0) return false;l.producer=int(n);}
  }
  if(l.producer<0) return false;
  auto &producer=s.nativeProducers[l.producer];void *constraint=nullptr,*list=nullptr;
  if(!ClothBoneNativeProducerIdentity(producer) || !CollisionList(CollisionGc(producer.data),constraint,list) ||
      CollisionCount(list)<0 || CollisionCount(list)>64 ||
      !SurfaceMethod(il2cpp_object_get_class(ClothTarget(producer.bbc)),"SetParameterChange","System.Void")) return false;
  for(int n=0;n<s_cloth.count;++n) {
    auto &i=s_cloth.instances[n];auto bbc=ClothTarget(i.ref);void *data=nullptr,*otherConstraint=nullptr,*otherList=nullptr;
    if(!bbc || bbc==ClothTarget(producer.bbc)) continue;
    if(!ClothInvoke(i.api.serialize,bbc,nullptr,data) || !CollisionList(data,otherConstraint,otherList) ||
        data==CollisionGc(producer.data) || otherConstraint==constraint || otherList==list) return false;
  }
  l.constraint=ClothBoneHold(constraint);l.list=ClothBoneHold(list);
  if(!l.constraint || !l.list) return false;
  auto cls=SurfaceClass("BeyondDynamicBone","BeyondBoneSphereCollider");
  auto ctor=ClothMethod(g_gameObjectClass,".ctor","System.Void","System.String");
  auto add=ClothMethod(g_gameObjectClass,"AddComponent","UnityEngine.Component","System.Type");
  auto size=ClothMethod(cls,"SetSize","System.Void","System.Single"),update=ClothMethod(cls,"UpdateParameters","System.Void");
  if(!cls || !ctor || !add || !size || !update) return false;
  for(size_t n=0;n<std::size(ClothSideShapes);++n) {
    const auto &fit=ClothSideShapes[n];
    if(fit.bone<0 || size_t(fit.bone)>=l.renderer.bones.size()) return false;
    auto parent=ClothTarget(l.renderer.bones[fit.bone]);Vector3 scale{};
    if(!parent || !ClothAnchorUnderOwner(parent) || !CollisionScale(parent,scale) ||
        !eiem_collision::UniformPositive(CollisionV(scale)) || !CollisionUniformFrame(parent,scale.x)) return false;
    char name[96]{};snprintf(name,sizeof(name),"EIEM_BoneCloth_Side_%zu",n);
    void *go=il2cpp_object_new(g_gameObjectClass),*label=il2cpp_string_new(name),*unused=nullptr,*args[]{label};
    if(!go || !label || !ClothBoneHold(go) || !ClothBoneHold(label)) return false;
    const bool made=ClothInvoke(ctor,go,args,unused);auto ref=ClothProtect(go);
    if(!ref.handle) {
      void *destroy[]{go};ClothInvoke(ClothMethod(SurfaceClass("UnityEngine","Object"),"Destroy","System.Void","UnityEngine.Object",true),nullptr,destroy,unused);return false;
    }
    local.objects.push_back(ref);local.destroyIssued.push_back(false);
    auto t=CollisionTransform(go);
    if(!made || !t || !SurfaceTRS(t,parent,fit.center,Quaternion{0,0,0,1},Vector3{1,1,1})) return false;
    l.shapes.emplace_back();auto &r=l.shapes.back();r.transform=ClothProtect(t);r.parent=ClothProtect(parent);
    void *sceneBox=nullptr;int scene=0;
    if(!r.transform.handle || !r.parent.handle || !ClothInvoke(s_clothUnity.scene,go,nullptr,sceneBox) ||
        !ClothField(sceneBox,"m_Handle","System.Int32",scene) || scene!=s_cloth.scene) return false;
    void *type=il2cpp_type_get_object(il2cpp_class_get_type(cls)),*component=nullptr,*addArgs[]{type};
    if(!type || !ClothBoneHold(type) || !ClothOwns(s.owner) || !ClothInvoke(add,go,addArgs,component) || !component) return false;
    r.collider=ClothProtect(component);float radius=fit.radius;void *sizeArgs[]{&radius};
    if(!r.collider.handle || !ClothInvoke(size,component,sizeArgs,unused) || !ClothInvoke(update,component,nullptr,unused) ||
        !ClothBoneSideGeometry(n)) return false;
    bool member=false;int count=-1;
    if(!CollisionTeams(component,s.team[0],member,count) || member || count!=0) return false;
    Log("[CLOTH-BONE-SIDE] stage=created generation=%llu index=%zu collider=%d parent=%d center=%g,%g,%g radius=%g scale=%g originalCapsuleWrites=0",
        (unsigned long long)s.owner.generation,n,r.collider.id.instance,r.parent.id.instance,fit.center.x,fit.center.y,fit.center.z,radius,scale.x);
  }
  l.created=true;return true;
}
static bool ClothBoneSideConfigure(std::vector<void*> &colliders) {
  auto &s=ClothBoneState();auto &l=s.local.side;
  if(!l.created) return l.shapes.empty();
  if(!ClothOnMainThread() || !ClothOwns(s.owner) || s.stopRequested || !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 ||
      l.linked || !ClothBoneSideIdentity()) return false;
  auto list=CollisionGc(l.list);
  l.linked=true;
  for(const auto &r:l.shapes) {
    auto c=ClothTarget(r.collider);bool has=false;void *unused=nullptr,*args[]{c};
    if(!c || !CollisionContains(list,c,"BeyondDynamicBone.ColliderComponent",has) || has ||
        !ClothInvoke(ClothMethod(il2cpp_object_get_class(list),"Add","System.Void","BeyondDynamicBone.ColliderComponent"),list,args,unused) ||
        !CollisionContains(list,c,"BeyondDynamicBone.ColliderComponent",has) || !has) return false;
    colliders.push_back(c);
  }
  auto bbc=ClothTarget(s.nativeProducers[l.producer].bbc);void *unused=nullptr;
  l.notified=ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"SetParameterChange","System.Void"),bbc,nullptr,unused);
  Log("[CLOTH-BONE-SIDE] stage=parameter-command producer=%s count=%zu success=%d recipe=%s registered=0 contactVerified=0 visualVerified=0",
      s.local.recipe->contactProducer,l.shapes.size(),int(l.notified),ClothSideSignature);
  return l.notified;
}
static bool ClothBoneSidePairMembership(void *c,void *process,int team,void *producer,int producerTeam) {
  if(!c || !process || !producer || process==producer || team<=0 || producerTeam<=0 || team==producerTeam) return false;
  for(const auto &slot:std::array<std::pair<void*,int>,2>{{{process,team},{producer,producerTeam}}}) {
    bool member=false,listed=false;int count=-1;
    if(!CollisionTeams(c,slot.second,member,count) || !member || count!=2 ||
        !CollisionProcessContains(slot.first,c,listed) || !listed) return false;
  }
  return true;
}
static bool ClothBoneSideRegistration(void *process,int team,bool restoring) {
  auto &s=ClothBoneState();auto &l=s.local.side;
  if(!l.created) return true;
  const bool candidate=!restoring && process==CollisionGc(s.process[1]);
  if(candidate && (!l.notified || !ClothBoneSideProducerList(true))) return false;
  for(const auto &r:l.shapes) {
    void *c=nullptr;const auto life=ClothInspect(r.collider,c);
    if(life==ClothLife::Destroyed && !candidate) continue;
    bool member=false,listed=false;int count=-1;
    if(life!=ClothLife::Alive || !process || team<=0 || !CollisionTeams(c,team,member,count) ||
        !CollisionProcessContains(process,c,listed) || member!=candidate || listed!=candidate) return false;
    if(candidate) {
      const auto &p=s.nativeProducers[l.producer];
      if(!ClothBoneSidePairMembership(c,process,team,CollisionGc(p.process),p.team)) return false;
    }
  }
  if(candidate && !l.registrationLogged) {
    l.registrationLogged=true;
    Log("[CLOTH-BONE-SIDE] stage=registered generation=%llu Process=%p skirtTeam=%d coatTeam=%d count=%zu listAndTeamReadback=1 contactVerified=0 visualVerified=0",
        (unsigned long long)s.owner.generation,process,team,s.nativeProducers[l.producer].team,l.shapes.size());
  }
  return true;
}
static bool ClothBoneSideRemoveOwned(void *list) {
  if(!list) return false;
  for(const auto &r:ClothBoneState().local.side.shapes) {
    auto c=CollisionGc(r.collider.handle);if(!c) continue;
    bool has=false;
    for(int attempts=0;;++attempts) {
      if(!CollisionContains(list,c,"BeyondDynamicBone.ColliderComponent",has)) return false;
      if(!has) break;if(attempts>=128) return false;
      void *unused=nullptr,*args[]{c};
      if(!ClothInvoke(ClothMethod(il2cpp_object_get_class(list),"Remove","System.Boolean","BeyondDynamicBone.ColliderComponent"),list,args,unused)) return false;
    }
  }
  return true;
}
static bool ClothBoneSideReleaseReady() {
  auto &s=ClothBoneState();auto &l=s.local.side;
  if(l.shapes.empty()) return true;
  if(l.linked && !l.removeNotified) {
    if(s.contactPartner>=0) {
      if(s.contactPartner>=s_clothBoneCount || l.producer<0 || size_t(l.producer)>=s.nativeProducers.size())
        return ClothBoneLocalCleanupWait("side-paired-retirement-identity-unreadable");
      const auto &partner=s_clothBoneSlots[s.contactPartner];const auto &p=s.nativeProducers[l.producer];
      if(!(partner.owner==s.owner) || partner.command!=s.command || !(partner.bbc.id==p.bbc.id) ||
          CollisionGc(partner.process[1])!=CollisionGc(p.process) || partner.team[1]!=p.team)
        return ClothBoneLocalCleanupWait("side-paired-retirement-identity-changed");
      if(partner.tx.lease && !partner.supportOwnerDrained)
        return ClothBoneLocalCleanupWait("side-awaiting-paired-native-retirement-before-list-remove",p.team);
    }
    if(!ClothBoneSideRemoveOwned(CollisionGc(l.list))) return ClothBoneLocalCleanupWait("side-producer-list-remove-pending");
    if(ClothBoneSideProducerList(false)) {
      auto bbc=ClothTarget(s.nativeProducers[l.producer].bbc);void *unused=nullptr;
      if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"SetParameterChange","System.Void"),bbc,nullptr,unused)) return ClothBoneLocalCleanupWait("side-producer-notify-pending");
    }
    l.removeNotified=true;
    Log("[CLOTH-BONE-SIDE] stage=remove-command ownedOnly=1 count=%zu unregisterPending=1",l.shapes.size());
  }
  if(auto data=CollisionGc(s.candidateData)) {
    void *constraint=nullptr,*list=nullptr;
    if(!CollisionList(data,constraint,list) || !ClothBoneSideRemoveOwned(list)) return ClothBoneLocalCleanupWait("side-private-list-remove-pending");
  }
  for(const auto &r:l.shapes) {
    if(!r.collider.handle) continue;void *c=nullptr;const auto life=ClothInspect(r.collider,c);
    if(life==ClothLife::Destroyed) continue;
    bool member=false;int count=-1;
    if(life!=ClothLife::Alive || !CollisionTeams(c,s.team[1],member,count) || member || count!=0) return ClothBoneLocalCleanupWait("side-native-Team-detach-pending",r.collider.id.instance);
    if(l.producer>=0 && size_t(l.producer)<s.nativeProducers.size()) {
      bool has=false;auto process=CollisionGc(s.nativeProducers[l.producer].process);
      if(!process || !CollisionProcessContains(process,c,has) || has) {
        bool disposed=false,building=true;
        if(!process || !CollisionField(process,"isDestoryInternal","System.Boolean",disposed) || !disposed ||
            !CollisionField(process,"isBuild","System.Boolean",building) || building ||
            !SurfaceUnregistered(process,s.nativeProducers[l.producer].team))
          return ClothBoneLocalCleanupWait("side-producer-Process-detach-pending",r.collider.id.instance);
      }
    }
    for(int n=0;n<s_cloth.count;++n) {
      auto &i=s_cloth.instances[n];void *bbc=nullptr,*data=nullptr,*constraint=nullptr,*list=nullptr;
      auto state=ClothInspect(i.ref,bbc);if(state==ClothLife::Destroyed) continue;
      bool has=false;
      if(state!=ClothLife::Alive || !ClothInvoke(i.api.serialize,bbc,nullptr,data) ||
          !CollisionList(data,constraint,list) || !CollisionContains(list,c,"BeyondDynamicBone.ColliderComponent",has) || has)
        return ClothBoneLocalCleanupWait("side-foreign-serialized-reference-or-unreadable",n);
    }
  }
  return true;
}
static void ClothBoneSideFree() {
  auto &l=ClothBoneState().local.side;auto &r=l.renderer;
  for(auto &shape:l.shapes) {ClothFree(shape.collider);ClothFree(shape.transform);ClothFree(shape.parent);}
  ClothFree(r.renderer);ClothFree(r.mesh);ClothFree(r.root);ClothFree(r.qualifiedParent);
  for(auto &b:r.bones) ClothFree(b);for(auto &p:r.parents) ClothFree(p);
  if(!l.shapes.empty()) Log("[CLOTH-BONE-SIDE] stage=released count=%zu originalCapsuleWrites=0",l.shapes.size());
  l={};
}
