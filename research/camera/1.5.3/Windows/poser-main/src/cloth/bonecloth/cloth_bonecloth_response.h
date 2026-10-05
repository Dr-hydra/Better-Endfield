#pragma once
static bool ClothBoneResponseRestore() {
  auto &s=ClothBoneState();auto &l=s.local.response;if(!l.Dirty())return true;
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1||!s.local.recipe)return false;
  const auto &r=*s.local.recipe;bool restored=true;
  for(int k=0;k<r.responseCount&&k<3;++k){auto &p=l.poses[k];if(!p.position&&!p.rotation)continue;
    const int n=r.responses[k].frame.target;if(n<0||size_t(n)>=s.bones.size()){restored=false;continue;}
    auto &b=s.bones[n];void *t=nullptr;const auto life=ClothInspect(b.bone,t);
    if(life==ClothLife::Destroyed){p.position=p.rotation=false;continue;}if(!t){restored=false;continue;}
    if(CollisionParent(t)!=ClothTarget(b.parent)){p.position=p.rotation=false;continue;}
    Vector3 pos{},scale{};Quaternion q{};if(!SurfaceVisiblePose(t,pos,q,scale)){restored=false;continue;}
    if(p.position){if(!SurfaceVisibleSame(pos,p.lastP)&&!(p.previousPosition&&SurfaceVisibleSame(pos,p.previousP)))p.position=false;
      else if(p.restorePositionAttempts<3){++p.restorePositionAttempts;if(SurfaceCall(t,"set_localPosition","UnityEngine.Vector3",&p.originalP)&&SurfaceVisiblePose(t,pos,q,scale)&&SurfaceVisibleSame(pos,p.originalP))p.position=false;}}
    if(p.rotation){if(!SurfaceVisibleSame(q,p.lastQ)&&!(p.previousRotation&&SurfaceVisibleSame(q,p.previousQ)))p.rotation=false;
      else if(p.restoreRotationAttempts<3){++p.restoreRotationAttempts;if(SurfaceCall(t,"set_localRotation","UnityEngine.Quaternion",&p.originalQ)&&SurfaceVisiblePose(t,pos,q,scale)&&SurfaceVisibleSame(q,p.originalQ))p.rotation=false;}}
    restored&=!p.position&&!p.rotation;
  }
  if(restored)Log("[CLOTH-BONE-RESPONSE] stage=restored consumer=%s session=%llu generation=%llu command=%u updates=%u sourceHierarchyAndColliderGeometryUnchanged=1 foreignPosePreserved=1",
      r.responseConsumer,s.owner.session,s.owner.generation,s.command,l.updates);
  return restored;
}
static void ClothBoneResponseFree() {
  auto &l=ClothBoneState().local.response;ClothFree(l.consumer);for(auto &r:l.colliders)ClothFree(r);
  for(auto &r:l.outputBones)ClothFree(r);
  for(auto &p:l.peers){ClothFree(p.bbc);for(auto &r:p.rootRefs)ClothFree(r);}l={};
}
static bool ClothBoneResponseRelation(bool capture) {
  auto &s=ClothBoneState();auto &l=s.local.response;const auto &r=*s.local.recipe;
  auto bbc=ClothTarget(l.consumer);void *process=nullptr,*data=nullptr,*data2=nullptr,*constraint=nullptr,*list=nullptr;
  bool enabled=false,valid=false,running=false;int team=0;
  if(!bbc||!ClothAnchorUnderOwner(CollisionTransform(bbc))||
      !CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",process)||!process||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data)||!data||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"GetSerializeData2","BeyondDynamicBone.ClothSerializeData2"),bbc,nullptr,data2)||!data2||
      !CollisionList(data,constraint,list)||CollisionCount(list)!=3||
      !ClothValue(s_clothUnity.getEnabled,bbc,enabled)||!enabled||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"IsValid","System.Boolean"),process,valid)||!valid||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"IsRunning","System.Boolean"),process,running)||!running||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"get_TeamId","System.Int32"),process,team)||!ClothBoneTeamRegistered(process,team))return false;
  if(capture){l.process=ClothBoneHold(process);l.data=ClothBoneHold(data);l.data2=ClothBoneHold(data2);l.constraint=ClothBoneHold(constraint);l.list=ClothBoneHold(list);l.team=team;
    if(!l.process||!l.data||!l.data2||!l.constraint||!l.list)return false;}
  else if(process!=CollisionGc(l.process)||data!=CollisionGc(l.data)||data2!=CollisionGc(l.data2)||
      constraint!=CollisionGc(l.constraint)||list!=CollisionGc(l.list)||team!=l.team||l.colliders.size()!=3)return false;
  for(int k=0;k<3;++k){const auto &a=r.responses[k];const int n=a.frame.target;auto t=ClothTarget(s.bones[n].bone);void *collider=nullptr;
    for(int j=0;j<3;++j){auto c=CollisionItem(list,j,"BeyondDynamicBone.ColliderComponent");if(c&&CollisionTransform(c)==t){if(collider)return false;collider=c;}}
    bool member=false,listed=false,reverse=false,separated=false,centered=false;int count=-1;char direction[24]{};Vector3 center{},size{};
    if(!collider||strcmp(il2cpp_class_get_name(il2cpp_object_get_class(collider)),"BeyondBoneCapsuleCollider")||
        CollisionParent(t)!=ClothTarget(s.bones[n].parent)||!ClothAnchorUnderOwner(t)||
        !CollisionTeams(collider,team,member,count)||!member||count!=1||!CollisionProcessContains(process,collider,listed)||!listed||
        !CollisionField(collider,"center","UnityEngine.Vector3",center)||!SurfaceVisibleSame(center,a.center)||
        !CollisionField(collider,"size","UnityEngine.Vector3",size)||!SurfaceVisibleSame(size,a.size)||
        !CollisionEnum(collider,"direction","BeyondDynamicBone.BeyondBoneCapsuleCollider.Direction",direction,sizeof(direction))||strcmp(direction,"X")||
        !CollisionField(collider,"reverseDirection","System.Boolean",reverse)||reverse!=a.reverse||
        !CollisionField(collider,"radiusSeparation","System.Boolean",separated)||separated!=a.separated||
        !CollisionField(collider,"alignedOnCenter","System.Boolean",centered)||centered!=a.centered)return false;
    if(capture){l.colliders.push_back(ClothProtect(collider));if(!l.colliders.back().handle)return false;}
    else if(collider!=ClothTarget(l.colliders[k]))return false;
  }
  return true;
}
static bool ClothBoneResponsePeers(bool capture) {
  auto &s=ClothBoneState();auto &l=s.local.response;const auto &r=*s.local.recipe;
  if(capture){for(int k=0;k<s_cloth.count;++k){auto bbc=ClothTarget(s_cloth.instances[k].ref);if(bbc==ClothTarget(s.bbc))continue;
      void *data=nullptr,*roots=nullptr;if(!bbc||!ClothInvoke(s_cloth.instances[k].api.serialize,bbc,nullptr,data)||!data||
          !CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots))return false;
      const int count=CollisionCount(roots);if(count<0||count>64)return false;
      ClothResponsePeer peer;peer.bbc=ClothProtect(bbc);peer.data=ClothBoneHold(data);peer.roots=ClothBoneHold(roots);l.peers.push_back(std::move(peer));auto &p=l.peers.back();
      if(!p.bbc.handle||!p.data||!p.roots)return false;
      for(int n=0;n<count;++n){auto root=CollisionItem(roots,n,"UnityEngine.Transform");if(!root||!ClothAnchorUnderOwner(root))return false;
        for(int j=0;j<3;++j){void *box=nullptr,*args[]{root};auto t=ClothTarget(s.bones[r.responses[j].frame.target].bone);
          if(!ClothInvoke(SurfaceMethod(g_transformClass,"IsChildOf","System.Boolean","UnityEngine.Transform"),t,args,box)||!box||ClothUnboxBool(box))return false;}
        p.rootRefs.push_back(ClothProtect(root));if(!p.rootRefs.back().handle)return false;}
      if(bbc==ClothTarget(l.consumer)){int matched=0;for(const auto &ref:p.rootRefs){char name[128]{};CollisionName(ClothTarget(ref),name,sizeof(name));matched+=!strcmp(name,r.responseRoot);}
        const auto *sheet=s.contactPartner>=0&&s.contactPartner<s_clothBoneCount?&s_clothBoneSlots[s.contactPartner]:nullptr;
        if(matched!=1||(count!=1&&(!sheet||!ClothBoneRibbonPair(*sheet,s)||!ClothBonePair(*sheet,s)||count!=ClothBoneCandidate(*sheet).rootCount||roots!=CollisionGc(sheet->rootLists[1]))))return false;}
    }
  }
  if(l.peers.size()+1!=size_t(s_cloth.count))return false;
  for(const auto &p:l.peers){void *data=nullptr,*roots=nullptr;auto bbc=ClothTarget(p.bbc);
    if(!bbc||!ClothAnchorUnderOwner(CollisionTransform(bbc))||
        !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data)||data!=CollisionGc(p.data)||
        !CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots)||roots!=CollisionGc(p.roots)||CollisionCount(roots)!=int(p.rootRefs.size()))return false;
    for(size_t n=0;n<p.rootRefs.size();++n)if(!ClothTarget(p.rootRefs[n])||CollisionItem(roots,int(n),"UnityEngine.Transform")!=ClothTarget(p.rootRefs[n]))return false;
  }
  return true;
}
static bool ClothBoneResponsePrepare() {
  auto &s=ClothBoneState();auto &l=s.local.response;const auto &r=*s.local.recipe;
  if(!r.responseConsumer||!r.responseRoot||!r.responses||r.responseCount!=3||!r.resampledPanel||!eiem_cloth_asset::SourceSeraphPanel(*s.profile)||
      !s.tx.disposeOriginalIssued||!s.graph[1]||s.bones.size()!=size_t(r.Total()))return false;
  for(int k=0;k<3;++k){const auto &f=r.responses[k].frame;
    if(!eiem_cloth_response::Valid(f,r.originalCount,r.Total())||!ClothBoneCandidate(s).Passive(f.target))return false;
    for(int j=0;j<k;++j)if(r.responses[j].frame.target==f.target)return false;
    for(int j=0;j<f.count;++j)if(ClothBoneCandidate(s).Passive(f.controls[j]))return false;}
  for(int n=0;n<s_cloth.count;++n)if(!strcmp(s_cloth.instances[n].name,r.responseConsumer)){
    if(l.consumer.handle)return false;l.consumer=ClothProtect(ClothTarget(s_cloth.instances[n].ref));if(!l.consumer.handle)return false;}
  if(!ClothBoneResponseRelation(true)||!ClothBoneResponsePeers(true))return false;l.prepared=true;
  Log("[CLOTH-BONE-RESPONSE] stage=prepared consumer=%s outerTeam=%d sourceFrames=3 innerTeam=%d frame=%d mapping=private-skin-natural-chart nativeListUnchanged=1 nativeGeometryUnchanged=1 originalProcessRetired=1 visualVerified=0",
      r.responseConsumer,l.team,s.team[1],ClothFrame());return true;
}
static bool ClothBoneResponseApply() {
  auto &s=ClothBoneState();auto &l=s.local.response;const auto &r=*s.local.recipe;
  if(!ClothBoneResponseRelation(false)||!ClothBoneResponsePeers(false))return false;
  using namespace eiem_cloth_response;
  std::vector<OutputMatrix> world(s.bones.size());std::set<int> read;
  std::array<OutputMatrix,3> wanted;std::array<Vector3,3> positions;std::array<Quaternion,3> rotations;
  for(int k=0;k<3;++k){const auto &f=r.responses[k].frame;
    for(int j=0;j<f.count;++j){const int n=f.controls[j];if(!read.insert(n).second)continue;const auto &b=s.bones[n];auto t=ClothTarget(b.bone);
      if(!t||CollisionParent(t)!=ClothTarget(b.parent)||!ClothAnchorUnderOwner(t)||!SurfaceOutputMatrix(t,world[n]))return false;}
    if(!Map(f,r.originalCount,world,wanted[k]))return false;}
  for(int k=0;k<3;++k){const auto &b=s.bones[r.responses[k].frame.target];auto t=ClothTarget(b.bone);auto &p=l.poses[k];Vector3 current{},scale{};Quaternion q{};
    if(!t||CollisionParent(t)!=ClothTarget(b.parent)||!SurfaceVisiblePose(t,current,q,scale))return false;
    if(!l.captured){p.originalP=current;p.originalQ=q;p.scale=scale;}
    else if(!SurfaceVisibleSame(scale,p.scale)||(p.position&&!SurfaceVisibleSame(current,p.lastP))||(p.rotation&&!SurfaceVisibleSame(q,p.lastQ)))return false;
    OutputMatrix parent{},local{};bool mappedParent=false;
    for(int j=0;j<3;++j)if(ClothTarget(s.bones[r.responses[j].frame.target].bone)==ClothTarget(b.parent)){parent=wanted[j];mappedParent=true;}
    if(!mappedParent&&!SurfaceOutputMatrix(ClothTarget(b.parent),parent))return false;
    double rotation[4]{};if(!Relative(parent,wanted[k],local)||!Rotation(local,rotation))return false;
    positions[k]={float(local.v[12]),float(local.v[13]),float(local.v[14])};rotations[k]={float(rotation[0]),float(rotation[1]),float(rotation[2]),float(rotation[3])};}
  l.captured=true;
  for(int k=0;k<3;++k){auto &p=l.poses[k];auto t=ClothTarget(s.bones[r.responses[k].frame.target].bone);
    if(!ClothOwns(s.owner)||s.stopRequested||s.tx.cancelled)return false;
    p.previousP=p.lastP;p.previousPosition=p.position;p.lastP=positions[k];p.position=true;
    if(!SurfaceCall(t,"set_localPosition","UnityEngine.Vector3",&p.lastP))return false;p.previousPosition=false;
    if(!ClothOwns(s.owner)||s.stopRequested||s.tx.cancelled)return false;
    p.previousQ=p.lastQ;p.previousRotation=p.rotation;p.lastQ=rotations[k];p.rotation=true;
    if(!SurfaceCall(t,"set_localRotation","UnityEngine.Quaternion",&p.lastQ))return false;p.previousRotation=false;
    Vector3 pos{},scale{};Quaternion q{};
    if(!SurfaceVisiblePose(t,pos,q,scale)||!SurfaceVisibleSame(pos,p.lastP)||!SurfaceVisibleSame(q,p.lastQ)||!SurfaceVisibleSame(scale,p.scale))return false;}
  for(int k=0;k<3;++k)if(!SurfaceOutputMatrix(ClothTarget(s.bones[r.responses[k].frame.target].bone),l.appliedWorld[k]))return false;
  ++l.updates;
  if(l.updates==1||l.updates%600==0)Log("[CLOTH-BONE-RESPONSE] stage=following consumer=%s session=%llu generation=%llu command=%u frame=%d innerTeam=%d outerTeam=%d updates=%u mappedFrames=3 registeredExistingColliders=3 phase=completed-pre-Team nativeSampling=next-input-pass zeroLatencyClaim=0 geometryWrites=0 bodyWrites=0 innerFeedback=0 visualVerified=0",
      r.responseConsumer,s.owner.session,s.owner.generation,s.command,ClothFrame(),s.team[1],l.team,l.updates);
  return true;
}
static void ClothBoneResponseTick() {
  auto &s=ClothBoneState();auto &l=s.local.response;
  if(!s.local.recipe||!s.local.recipe->responseCount||!ClothOnMainThread()||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1)return;
  const int frame=ClothFrame();if(frame<0||l.frame==frame)return;l.frame=frame;
  eiem_cloth_rebuild::Identity id{};
  if(l.disabled||s.stopRequested||s.tx.cancelled||!ClothOwns(s.owner)||!s.local.published||
      s.tx.phase!=eiem_cloth_rebuild::Phase::Active||!s.teamModeConfirmed||!ClothBoneCurrent(id)||!(id==s.tx.installed)){
    ClothBoneResponseRestore();return;}
  bool ok=false;
  __try {if(!l.attempted){l.attempted=true;ok=ClothBoneResponsePrepare();}else ok=l.prepared;if(ok)ok=ClothBoneResponseApply();}
  __except(EXCEPTION_EXECUTE_HANDLER){ok=false;}
  if(!ok){l.disabled=true;const bool restored=ClothBoneResponseRestore();
    Log("[CLOTH-BONE-RESPONSE] stage=disabled consumer=%s reason=source-consumer-membership-or-pose-unconfirmed restored=%d acceptedInnerSimulationRetained=1 generation=%llu command=%u",
        s.local.recipe->responseConsumer,int(restored),s.owner.generation,s.command);}
}
