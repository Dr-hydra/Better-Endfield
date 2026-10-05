#pragma once
static void ClothBoneOwnedStep(const char *stage,int index=-1) {
  auto &o=ClothBoneState().owned;o.preflightStage=stage;o.preflightIndex=index;o.preflightDetail="none";
}
static void ClothBoneOwnedPreflightFailure() {
  auto &s=ClothBoneState();int captured=0;for(const auto &b:s.bones)captured+=b.bone.handle!=0;
  Log("[CLOTH-UNOWNED-PREFLIGHT] component=%s backend=%u generation=%llu session=%llu owner=%p frame=%d stage=%s index=%d detail=%s capturedBones=%d expectedBones=%d privateObject=%d",
      s.profile->component,s.owner.backend,s.owner.generation,s.owner.session,reinterpret_cast<void*>(s.owner.character),ClothFrame(),
      s.owned.preflightStage,s.owned.preflightIndex,s.owned.preflightDetail,captured,s.profile->boneCount,int(s.owned.gameObject.handle!=0));
  char reason[128]{};_snprintf_s(reason,_TRUNCATE,"unowned-preflight-%s",s.owned.preflightStage);ClothBoneReject(reason);
}
static bool ClothBoneOwnedPeers() {
  auto &s=ClothBoneState();
  if(!s_cloth.discovery.complete)return false;
  for(int k=0;k<s_cloth.count;++k){const auto &i=s_cloth.instances[k];void *data=nullptr,*roots=nullptr;auto bbc=ClothTarget(i.ref);
    if(!bbc||!ClothInvoke(i.api.serialize,bbc,nullptr,data)||!CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots))return false;
    const int count=CollisionCount(roots);if(count<0||count>128)return false;
    for(int n=0;n<count;++n){auto root=CollisionItem(roots,n,"UnityEngine.Transform");if(!root)return false;
      for(int c=0;c<s.profile->rootCount;++c){const auto &b=s.bones[s.profile->roots[c]];auto t=ClothTarget(b.bone);void *box=nullptr,*args[]{root};
        if(!t||t==root||!ClothInvoke(SurfaceMethod(g_transformClass,"IsChildOf","System.Boolean","UnityEngine.Transform"),t,args,box)||!box||UnboxBool(box))return false;
        args[0]=t;if(!ClothInvoke(SurfaceMethod(g_transformClass,"IsChildOf","System.Boolean","UnityEngine.Transform"),root,args,box)||!box||UnboxBool(box))return false;}}
  }
  for(const auto &peer:s_clothBoneSlots)if(&peer!=&s&&peer.pending&&peer.profile)
    for(const auto &a:s.bones)for(const auto &b:peer.bones)if(a.bone.id==b.bone.id)return false;
  return true;
}
static bool ClothBoneOwnedVolumes(bool registered) {
  auto &s=ClothBoneState();auto process=CollisionGc(s.process[1]);const auto &p=*s.profile;
  if(s.colliders.size()!=size_t(p.colliderCount)||!p.unownedGeometry)return false;
  for(int n=0;n<p.colliderCount;++n){auto c=ClothTarget(s.colliders[n]);auto t=ClothTarget(s.colliderTransforms[n]);
    if(!c||!t||CollisionTransform(c)!=t||CollisionParent(t)!=ClothTarget(s.colliderParents[n]))return false;
    const auto geometry=CollisionReadGeometry(c);const auto &expected=p.unownedGeometry[n];const char *axes[]{"X","Y","Z"};
    if(!geometry.valid||!geometry.enabled||!geometry.active||expected.axis<0||expected.axis>2||strcmp(geometry.direction,axes[expected.axis])||
        !SurfaceVisibleSame(geometry.center,expected.center)||!SurfaceVisibleSame(geometry.size,expected.size)||
        geometry.reverse!=expected.reverse||geometry.separated!=expected.separate||geometry.centered!=expected.aligned)return false;
    bool member=false,listed=false;int count=-1,known=0;
    if(!CollisionTeams(c,s.team[1],member,count)||member!=registered)return false;
    if(process&&(!CollisionProcessContains(process,c,listed)||listed!=registered))return false;
    for(int k=0;k<s_cloth.count;++k){auto bbc=ClothTarget(s_cloth.instances[k].ref);void *other=nullptr;int team=0;
      if(!bbc||!CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",other))return false;if(!other)continue;
      if(!ClothValue(SurfaceMethod(il2cpp_object_get_class(other),"get_TeamId","System.Int32"),other,team))return false;if(team<=0)continue;
      bool shared=false,used=false;int total=-1;
      if(!CollisionTeams(c,team,shared,total))return false;if(shared){if(!ClothBoneTeamRegistered(other,team)||!CollisionProcessContains(other,c,used)||!used)return false;++known;}}
    for(const auto &peer:s_clothBoneSlots)if(&peer!=&s&&peer.profile&&peer.profile->runtimeUnowned&&peer.lease){auto other=CollisionGc(peer.process[1]);if(!other||peer.team[1]<=0)continue;
      bool shared=false,used=false;int total=-1;if(!CollisionTeams(c,peer.team[1],shared,total))return false;
      if(shared){if(!ClothBoneTeamRegistered(other,peer.team[1])||!CollisionProcessContains(other,c,used)||!used)return false;++known;}}
    if(count!=known+int(registered))return false;
  }
  return true;
}
static bool ClothBoneOwnedBindings(bool capture) {
  auto &s=ClothBoneState();const auto &p=*s.profile;
  ClothBoneOwnedStep("owner-permission-callback");
  if(!ClothOwns(s.owner)||!s_cloth.bodyGuard)return false;
  if(capture){s.bones.resize(p.boneCount);s.owned.before.resize(p.boneCount);s.owned.last.resize(p.boneCount);
    struct Node{void *t;int depth;};std::vector<Node> nodes{{CollisionTransform(ClothTarget(s_cloth.animator)),0}};
    for(size_t at=0;at<nodes.size();++at){ClothBoneOwnedStep("owner-hierarchy",int(at));if(nodes.size()>2048)return false;const auto node=nodes[at];if(!node.t)return false;if(ClothOwnedRoot(node.t))continue;
      char name[128]{},parentName[128]{};CollisionName(node.t,name,sizeof(name));auto parent=CollisionParent(node.t);CollisionName(parent,parentName,sizeof(parentName));
      for(int n=0;n<p.boneCount;++n)if(!strcmp(name,p.bones[n].name)&&!strcmp(parentName,p.bones[n].parentName)){
        ClothBoneOwnedStep("bone-owner-permission",n);
        auto &b=s.bones[n];if(b.bone.handle||!parent||!s_cloth.bodyGuard(node.t))return false;
        ClothBoneOwnedStep("avatar-natural-bind",n);
        ClothAnchor bind{};strcpy_s(bind.name,name);strcpy_s(bind.parentName,parentName);
        if(!ClothResolveAnchorBind(bind)||!ClothSameLocal(bind.bindPosition,bind.bindRotation,p.bones[n].position,p.bones[n].rotation)){s.owned.preflightDetail=bind.bindReason;return false;}
        ClothBoneOwnedStep("bone-current-pose",n);
        b.bone=ClothProtect(node.t);b.parent=ClothProtect(parent);b.local=bind.bindPosition;b.rotation=bind.bindRotation;
        auto &before=s.owned.before[n];before.known=SurfaceVisiblePose(node.t,before.position,before.rotation,before.scale);
        if(!b.bone.handle||!b.parent.handle||!before.known||!SurfaceVisibleSame(before.scale,p.bones[n].scale))return false;
      }
      for(int n=0;n<p.colliderCount;++n)if(!strcmp(name,p.colliders[n].name)&&!strcmp(parentName,p.colliders[n].parent)){
        ClothBoneOwnedStep("native-capsule-component",n);
        if(s.colliders.empty()){s.colliders.resize(p.colliderCount);s.colliderTransforms.resize(p.colliderCount);s.colliderParents.resize(p.colliderCount);}
        if(s.colliders[n].handle)return false;void *go=nullptr,*array=nullptr;auto cls=SurfaceClass("BeyondDynamicBone","BeyondBoneCapsuleCollider");auto type=cls?il2cpp_type_get_object(il2cpp_class_get_type(cls)):nullptr;void *args[]{type};uintptr_t count=0;
        if(!type||!ClothInvoke(s_clothUnity.getGO,node.t,nullptr,go)||!ClothInvoke(s_clothUnity.components,go,args,array)||!ClothBoneHold(array)||!ClothArray(array,"UnityEngine.Component[]",count)||count!=1)return false;
        auto c=reinterpret_cast<void**>((char*)array+32)[0];s.colliders[n]=ClothProtect(c);s.colliderTransforms[n]=ClothProtect(node.t);s.colliderParents[n]=ClothProtect(parent);
        if(!s.colliders[n].handle||!s.colliderTransforms[n].handle||!s.colliderParents[n].handle)return false;
      }
      ClothBoneOwnedStep("owner-children",int(at));
      int count=0;if(!ClothValue(s_clothUnity.childCount,node.t,count)||count<0||count>128||(node.depth>=32&&count))return false;
      for(int k=0;k<count;++k){void *child=nullptr,*args[]{&k};if(!ClothInvoke(s_clothUnity.child,node.t,args,child)||!child)return false;nodes.push_back({child,node.depth+1});}
    }
  }
  for(int n=0;n<p.boneCount;++n){ClothBoneOwnedStep("bone-current-identity-and-permission",n);
    auto t=ClothTarget(s.bones[n].bone),parent=ClothTarget(s.bones[n].parent);if(!t||!parent||CollisionParent(t)!=parent||!s_cloth.bodyGuard(t))return false;
    ClothBoneOwnedStep("bone-uniform-world-scale",n);
    eiem_cloth_surface::OutputMatrix world{};if(!SurfaceOutputMatrix(t,world)||!eiem_cloth_rebuild::UniformPositive(world))return false;
    ClothBoneOwnedStep("bone-parent-child-closure",n);
    if(p.bones[n].parent>=0&&parent!=ClothTarget(s.bones[p.bones[n].parent].bone))return false;
    int expected=0,count=0;for(int j=0;j<p.boneCount;++j)expected+=p.bones[j].parent==n;
    if(!ClothValue(s_clothUnity.childCount,t,count)||count!=expected)return false;
    for(int k=0;k<count;++k){void *child=nullptr,*args[]{&k};if(!ClothInvoke(s_clothUnity.child,t,args,child)||ClothBoneIndex(child)<0)return false;}}
  ClothBoneOwnedStep("other-BBC-bone-ownership");if(!ClothBoneOwnedPeers())return false;
  ClothBoneOwnedStep("source-renderer-bindings");
  if(capture){if(!ClothBoneCaptureRenderers())return false;ClothBoneOwnedStep("native-capsule-geometry-and-registration");return ClothBoneOwnedVolumes(false);}
  for(size_t n=0;n<s.renderers.size();++n)if(!ClothBoneRendererCheck(n,false))return false;
  return true;
}
static bool ClothBoneOwnedConfigure() {
  auto &s=ClothBoneState();auto bbc=ClothTarget(s.bbc);void *data=nullptr,*data2=nullptr,*prebuild=nullptr,*constraint=nullptr,*list=nullptr,*radius=nullptr;
  ClothBoneOwnedStep("new-BBC-serialization");
  bool prebuilt=true;
  if(!SurfaceCall(bbc,"DisableAutoBuild")||!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data)||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"GetSerializeData2","BeyondDynamicBone.ClothSerializeData2"),bbc,nullptr,data2)||
      !(s.candidateData=ClothBoneHold(data))||!(s.candidateData2=ClothBoneHold(data2))||
      !CollisionField(data2,"preBuildData","BeyondDynamicBone.PreBuildSerializeData",prebuild)||!prebuild||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(prebuild),"UsePreBuild","System.Boolean"),prebuild,prebuilt)||prebuilt)return false;
  std::vector<void*> roots,colliders;for(int n=0;n<s.profile->rootCount;++n)roots.push_back(ClothTarget(s.bones[s.profile->roots[n]].bone));for(const auto &r:s.colliders)colliders.push_back(ClothTarget(r));
  ClothBoneOwnedStep("new-BBC-native-configuration");
  if(!SurfaceEnum(data,"clothType","BeyondDynamicBone.ClothProcess.ClothType","BoneCloth")||
      !SurfaceEnum(data,"connectionMode","BeyondDynamicBone.RenderSetupData.BoneConnectionMode","SequentialLoopMesh")||
      !SurfaceList(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>","UnityEngine.Transform",roots)||
      !SurfaceList(data,"ignoreFromRootBones","System.Collections.Generic.List<UnityEngine.Transform>","UnityEngine.Transform",{})||
      !ClothBoneAttributes(data2,true)||!CollisionList(data,constraint,list)||
      !SurfaceEnum(constraint,"mode","BeyondDynamicBone.ColliderCollisionConstraint.Mode","Edge")||
      !SurfaceList(constraint,"colliderList",CollisionListType,"BeyondDynamicBone.ColliderComponent",colliders)||
      !SurfaceScalar(data,"animationPoseRatio","System.Single",0.f)||!SurfaceScalar(data,"clothSimulateWeight","System.Single",1.f)||!SurfaceScalar(data,"blendWeight","System.Single",1.f)||
      !CollisionField(data,"radius","BeyondDynamicBone.CurveSerializeData",radius)||!radius||
      !SurfaceScalar(radius,"useCurve","System.Boolean",false)||!SurfaceScalar(radius,"value","System.Single",s.profile->unownedRadius))return false;
  ClothBoneOwnedStep("new-BBC-native-default-constraints");
  ClothBoneMotionParameters motion{};if(!ClothBoneSupportMotionFrom(data,motion))return false;
  Log("[CLOTH-UNOWNED-CONFIG] component=%s roots=%d points=%d fixed=%d move=%d faces=%d colliders=%zu radius=%g referenceRatio=0 nativeDefaultConstraints=1 maxDistance=%d backstop=%d originalBBCWrites=0 originalColliderWrites=0 rendererWrites=0",
      s.profile->component,s.profile->rootCount,s.profile->boneCount,s.profile->rootCount,s.profile->boneCount-s.profile->rootCount,s.profile->FaceCount(),colliders.size(),s.profile->unownedRadius,int(motion.useMaxDistance),int(motion.useBackstop));
  return true;
}
static bool ClothBoneOwnedConstruct() {
  auto &s=ClothBoneState();ClothBoneOwnedStep("synchronous-native-ABI");if(!ClothBoneCaptureABI()||!ClothBoneOwnedBindings(true))return false;
  ClothBoneOwnedStep("owner-before-create");
  if(!ClothOwns(s.owner)||s.stopRequested)return false;
  ClothBoneOwnedStep("private-inactive-object");
  void *go=il2cpp_object_new(g_gameObjectClass),*unused=nullptr,*label=il2cpp_string_new(s.profile->component),*args[]{label};
  if(!go||!label)return false;
  const bool constructed=ClothInvoke(ClothMethod(g_gameObjectClass,".ctor","System.Void","System.String"),go,args,unused);
  s.owned.gameObject=ClothProtect(go);s.lease=s.owned.gameObject.handle!=0;
  if(!s.lease){void *destroyArgs[]{go};ClothInvoke(ClothMethod(SurfaceClass("UnityEngine","Object"),"Destroy","System.Void","UnityEngine.Object",true),nullptr,destroyArgs,unused);return false;}
  bool off=false;if(!constructed||!SurfaceCall(go,"SetActive","System.Boolean",&off))return false;
  auto transform=CollisionTransform(go);s.transform=ClothProtect(transform);if(!s.transform.handle)return false;
  s_clothOwnedRoots.push_back(s.transform);
  ClothBoneOwnedStep("private-object-parent-and-scene");
  if(!SurfaceTRS(transform,CollisionTransform(ClothTarget(s_cloth.animator)),{},{0,0,0,1},{1,1,1}))return false;
  int scene=0;if(!ClothScene(transform,scene)||scene!=s_cloth.scene)return false;
  ClothBoneOwnedStep("private-BBC-and-Process");
  auto cls=SurfaceClass("BeyondDynamicBone","BeyondBoneCloth");auto type=cls?il2cpp_type_get_object(il2cpp_class_get_type(cls)):nullptr;void *bbc=nullptr,*addArgs[]{type};
  if(!type||!ClothInvoke(ClothMethod(g_gameObjectClass,"AddComponent","UnityEngine.Component","System.Type"),go,addArgs,bbc)||!bbc)return false;
  s.bbc=ClothProtect(bbc);void *process=nullptr;
  if(!s.bbc.handle||!ClothInvoke(SurfaceMethod(cls,"get_Process","BeyondDynamicBone.ClothProcess"),bbc,nullptr,process)||!(s.process[1]=ClothBoneHold(process)))return false;
  if(!ClothBoneOwnedConfigure())return false;s.owned.created=true;return true;
}
static bool ClothBoneOwnedBuildImpl() {
  auto &s=ClothBoneState();
  eiem_cloth_surface::OutputMatrix world{};
  if(!SurfaceOutputMatrix(ClothTarget(s.transform),world)||!eiem_cloth_rebuild::UniformPositive(world))return false;
  for(auto &b:s.bones)if(!SurfaceVisiblePose(ClothTarget(b.bone),b.savedPosition,b.savedRotation,b.savedScale))return false;
  if(!ClothBoneSeedPose())return false;
  auto bbc=ClothTarget(s.bbc);bool accepted=false;
  if(!SurfaceCall(bbc,"Initialize")||!ClothBoneReadReference(CollisionGc(s.process[1]),false)||!ClothBoneConstructionChildren(CollisionGc(s.process[1]),true))return false;
  const bool known=ClothValue(SurfaceMethod(il2cpp_object_get_class(bbc),"BuildAndRun","System.Boolean"),bbc,accepted);
  s.reference[1]=ClothBoneReadReference(CollisionGc(s.process[1]),false);
  Log("[CLOTH-UNOWNED-BUILD] component=%s session=%llu generation=%llu command=%u frame=%d Process=%p sent=1 returnKnown=%d accepted=%d referenceReadback=%d",
      s.profile->component,s.owner.session,s.owner.generation,s.command,ClothFrame(),CollisionGc(s.process[1]),known,accepted,s.reference[1]);
  return known&&accepted&&s.reference[1];
}
static bool ClothBoneOwnedBuild() {
  bool ok=false;__try{__try{ok=ClothBoneOwnedBuildImpl();}__finally{ClothBoneRestorePose();}}
  __except(EXCEPTION_EXECUTE_HANDLER){ok=false;}return ok&&ClothBoneState().referenceRestored;
}
static bool ClothBoneOwnedIdentity() {
  auto &s=ClothBoneState();eiem_cloth_rebuild::Identity id{};
  return ClothBoneCurrent(id)&&id.process==uint64_t(uintptr_t(CollisionGc(s.process[1])))&&
      id.data==uint64_t(uintptr_t(CollisionGc(s.candidateData)))&&id.data2==uint64_t(uintptr_t(CollisionGc(s.candidateData2)));
}
static bool ClothBoneOwnedPolicy(bool active) {
  auto &s=ClothBoneState();auto process=CollisionGc(s.process[1]);void *team=nullptr,*manager=nullptr;ClothInputArray parameters{};int edge=-1,mode=-2;float ratio=NAN;bool enabled=false,skip=true;
  void *constraint=nullptr,*list=nullptr,*field=nullptr;int point=-1;
  if(!CollisionList(CollisionGc(s.candidateData),constraint,list)||!CollisionModeMetadata(il2cpp_object_get_class(constraint),field,point,edge))return false;
  if(!ClothBoneTeamRegistered(process,s.team[1])||!ClothBoneOwnedVolumes(true)||!CollisionProcessMode(process,mode)||mode!=edge||
      !ClothBoneContactTeam(s.team[1],process,team)||!ClothInputTeamField(team,"animationPoseRatio","System.Single",ratio)||!std::isfinite(ratio)||fabsf(ratio)>1e-6f||
      !ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager)||!ClothInputArrayOpen(manager,"parameterArray","BeyondDynamicBone.ClothParameters",parameters))return false;
  auto box=ClothInputArrayBox(parameters,s.team[1]);if(!box||!CollisionParameterMode(box,mode)||mode!=edge)return false;
  if(!active)return true;
  auto cls=il2cpp_object_get_class(process);
  float weight=NAN,blend=NAN;
  if(!ClothInputTeamField(team,"clothSimulateWeight","System.Single",weight)||!ClothInputTeamField(team,"blendWeight","System.Single",blend)||
      !std::isfinite(weight)||weight<.99f||!std::isfinite(blend)||blend<.99f)return false;
  return ClothValue(SurfaceMethod(cls,"get_IsEnable","System.Boolean"),process,enabled)&&enabled&&
      ClothValue(SurfaceMethod(cls,"IsSkipWriting","System.Boolean"),process,skip)&&!skip;
}
static void ClothBoneOwnedReturnPose() {
  auto &s=ClothBoneState();if(s.owned.poseReturned)return;
  int restored=0,preserved=0;
  for(size_t n=0;n<s.bones.size();++n){auto &b=s.bones[n];auto t=ClothTarget(b.bone);Vector3 p{},scale{};Quaternion q{};
    if(!t||CollisionParent(t)!=ClothTarget(b.parent)||!SurfaceVisiblePose(t,p,q,scale))continue;
    const auto &last=s.owned.last[n],&before=s.owned.before[n];
    if(!last.known||!before.known||!SurfaceVisibleSame(p,last.position)||!SurfaceVisibleSame(q,last.rotation)||!SurfaceVisibleSame(scale,last.scale)){++preserved;continue;}
    restored+=SurfaceCall(t,"set_localPosition","UnityEngine.Vector3",(void*)&before.position)&&SurfaceCall(t,"set_localRotation","UnityEngine.Quaternion",(void*)&before.rotation);
  }
  s.owned.poseReturned=true;
  Log("[CLOTH-UNOWNED-RETURN] component=%s restoredOwnLastPose=%d nativeOrExternalPosePreserved=%d originalMeshesUnchanged=1 nativeMayRecalculate=1",s.profile->component,restored,preserved);
}
static bool ClothBoneOwnedStillAttached() {
  auto &s=ClothBoneState();if(!s.bbc.handle)return true;void *bbc=nullptr;
  const auto life=ClothInspect(s.bbc,bbc);if(life!=ClothLife::Alive)return life==ClothLife::Destroyed;
  struct Field {const char *name,*type;uint32_t handle;};
  const Field fields[]{{"get_Process","BeyondDynamicBone.ClothProcess",s.process[1]},
      {"get_SerializeData","BeyondDynamicBone.ClothSerializeData",s.candidateData},
      {"GetSerializeData2","BeyondDynamicBone.ClothSerializeData2",s.candidateData2}};
  for(const auto &f:fields)if(f.handle){void *actual=nullptr;
    if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),f.name,f.type),bbc,nullptr,actual)||actual!=CollisionGc(f.handle))return false;}
  return true;
}
static bool ClothBoneOwnedDisabledQueues(void *process,const ClothBoneQueueRead &r,bool building) {
  auto &s=ClothBoneState();bool enabled=true;void *manager=nullptr;bool valid=false;
  if(building||!s.lease||!s.owned.created||!ClothBoneOwnedStillAttached()||
      !ClothValue(s_clothUnity.getEnabled,ClothTarget(s.bbc),enabled)||enabled||
      !ClothBoneTeamRegistered(process,s.team[1])||!ClothBoneOwnedVolumes(true)||
      !ClothContactManager("get_Cloth","BeyondDynamicBone.ClothManager",manager)||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(manager),"IsValid","System.Boolean"),manager,valid)||!valid||
      !ClothBoneMethodFingerprint("ClothManager","RemoveCloth","System.Void","BeyondDynamicBone.ClothProcess",307,0xb4a61138e616cc25ULL))return false;
  const int parameters=ClothBoneQueueOccurrences(process,"parameterDirtyList"),skip=ClothBoneQueueOccurrences(process,"skipWritingDirtyList");
  if(!eiem_cloth_rebuild::DisabledCandidateQueueRetirable(true,true,true,r.known,r.present,parameters,skip))return false;
  Log("[CLOTH-UNOWNED-QUEUE-RETIRE] component=%s parameterEntries=%d skipEntries=%d path=verified-Dispose-RemoveCloth queueWrites=0",s.profile->component,parameters,skip);
  return true;
}
static void ClothBoneOwnedCleanup() {
  auto &s=ClothBoneState();auto &o=s.owned;auto process=CollisionGc(s.process[1]);auto bbc=ClothTarget(s.bbc);void *unused=nullptr;
  s.tx.phase=eiem_cloth_rebuild::Phase::RetireCandidate;
  if(!s.referenceRestored&&!ClothBoneRestorePose())return;
  if(!ClothBoneOwnedStillAttached()){ClothBoneNote("unowned-identity-replaced-resources-retained-no-replacement-write");return;}
  if(process){bool building=true,disposed=false,pending=false,created=false;
    if(!CollisionField(process,"isBuild","System.Boolean",building)||!CollisionField(process,"isDestoryInternal","System.Boolean",disposed))return;
    if(!o.disposeIssued){
      if(bbc){
        if(o.active&&!building){bool enabled=false;
          if(!ClothValue(s_clothUnity.getEnabled,bbc,enabled))return;
          if(enabled&&ClothBoneDrainBeforeDisable(process,GetTickCount64())==eiem_cloth_rebuild::Verdict::Waiting)return;}
        if(!ClothBoneSetEnabled(false)||!ClothBoneTeleport(pending,created)||pending)return;
        if(created&&!o.teleportIssued){o.teleportIssued=SurfaceCall(bbc,"DisposeTeleportResources");}
        if(!ClothBoneTeleport(pending,created)||pending||created)return;
      }
      const auto queues=ClothBoneReadQueues(process);
      if(!queues.Empty()&&!ClothBoneOwnedDisabledQueues(process,queues,building)){ClothBoneNote("unowned-queue-retirement-unconfirmed-retaining-resources");return;}
      if(o.disposeAttempts>=3){ClothBoneNote("unowned-dispose-command-failed-retaining-resources");return;}
      ++o.disposeAttempts;o.disposeIssued=SurfaceCall(process,"Dispose");
      Log("[CLOTH-UNOWNED-DISPOSE] component=%s Process=%p commandAccepted=%d buildPending=%d completed=0",s.profile->component,process,o.disposeIssued,building);
      if(!o.disposeIssued)return;
    }
    auto retirement=ClothBoneRetirement(1);
    if(!bbc){retirement.teleportReleased=retirement.pendingTeleportCleared=true;}
    if(!o.retired.Observe(retirement,uint64_t(uintptr_t(process)))){ClothBoneNote("unowned-awaiting-native-registration-drain");return;}
  }
  ClothBoneOwnedReturnPose();
  void *go=nullptr;const auto life=ClothInspect(o.gameObject,go);
  if(life==ClothLife::Unreadable)return;
  if(life==ClothLife::Alive){if(!o.destroyIssued&&o.destroyAttempts<3){++o.destroyAttempts;void *args[]{go};o.destroyIssued=ClothInvoke(ClothMethod(SurfaceClass("UnityEngine","Object"),"Destroy","System.Void","UnityEngine.Object",true),nullptr,args,unused);}return;}
  const auto id=s.transform.id;s_clothOwnedRoots.erase(std::remove_if(s_clothOwnedRoots.begin(),s_clothOwnedRoots.end(),[&](const ClothRef &r){return r.id==id;}),s_clothOwnedRoots.end());
  ClothFree(o.gameObject);s.tx.phase=eiem_cloth_rebuild::Phase::Complete;
  ClothBoneNote(s.failed?"unowned-candidate-failed-private-BBC-removed":"unowned-BBC-removed-native-control-returned");ClothBoneDropReferences();
}
static void ClothBoneOwnedBoundary() {
  using Phase=eiem_cloth_rebuild::Phase;auto &s=ClothBoneState();auto &o=s.owned;const auto now=GetTickCount64();
  if(!ClothOwns(s.owner)||s.tx.cancelled)s.stopRequested=true;
  if(s.stopRequested){ClothBoneOwnedCleanup();return;}
  if(!o.created){if(!ClothBoneOwnedConstruct()){ClothBoneOwnedPreflightFailure();s.stopRequested=true;ClothBoneOwnedCleanup();return;}}
  if(s.stopRequested||!ClothOwns(s.owner)){s.stopRequested=true;ClothBoneOwnedCleanup();return;}
  if(!ClothBoneOwnedIdentity()){ClothBoneReject("unowned-Process-or-SerializeData-replaced");s.stopRequested=true;ClothBoneOwnedCleanup();return;}
  if(!o.buildIssued){o.buildIssued=true;s.tx.phase=Phase::BuildCandidate;s.deadline=now+8000;
    if(!ClothBoneOwnedBuild()){ClothBoneReject("unowned-native-build-or-reference-capture-failed");s.stopRequested=true;ClothBoneOwnedCleanup();}return;}
  auto process=CollisionGc(s.process[1]);bool building=true,valid=false,running=false;auto cls=il2cpp_object_get_class(process);
  if(!CollisionField(process,"isBuild","System.Boolean",building)||!ClothValue(SurfaceMethod(cls,"IsValid","System.Boolean"),process,valid)||
      !ClothValue(SurfaceMethod(cls,"IsRunning","System.Boolean"),process,running)||!ClothValue(SurfaceMethod(cls,"get_TeamId","System.Int32"),process,s.team[1])){
    ClothBoneReject("unowned-native-state-unreadable");s.stopRequested=true;return;}
  const auto result=SurfaceReadBuildResult(process);
  if(!o.active){
    if(!building&&result.known&&(result.error||result.cancelled)){ClothBoneReject("unowned-native-build-rejected");s.stopRequested=true;return;}
    if(building||!valid||!running||s.team[1]<=0){if(now>=s.deadline){ClothBoneReject("unowned-native-build-timeout");s.stopRequested=true;}return;}
    if(!s.graph[1]){s.graph[1]=ClothBoneGraph(process,false);if(!s.graph[1]||!ClothBoneOwnedBindings(false)||!ClothBoneOwnedPolicy(false)){ClothBoneReject("unowned-graph-binding-or-registration-unconfirmed");s.stopRequested=true;return;}}
    bool on=true,skip=false;float weight=1,ratio=0;auto bbc=ClothTarget(s.bbc);
    if(!SurfaceCall(ClothTarget(o.gameObject),"SetActive","System.Boolean",&on)){ClothBoneReject("unowned-activation-command-failed");s.stopRequested=true;return;}
    o.active=true;
    if(!SurfaceCall(bbc,"SetClothSimulateWeight","System.Single",&weight)||!SurfaceCall(bbc,"SetAnimationPoseRatio","System.Single",&ratio)||
        !SurfaceCall(bbc,"SetSkipWriting","System.Boolean",&skip)){
      ClothBoneReject("unowned-enable-command-failed");s.stopRequested=true;return;}
    s.tx.phase=Phase::ActivateCandidate;s.modeDeadline=now+2000;ClothBoneNote("unowned-native-graph-registered-awaiting-effective-output");return;
  }
  float globalTime=NAN;bool camera=false,distance=false,lod=false;
  if(ClothValue(s_clothUnity.globalTime,nullptr,globalTime)&&
      ClothValue(SurfaceMethod(cls,"IsCameraCullingInvisible","System.Boolean"),process,camera)&&
      ClothValue(SurfaceMethod(cls,"IsDistanceCullingInvisible","System.Boolean"),process,distance)&&
      ClothValue(SurfaceMethod(cls,"IsLodCulled","System.Boolean"),process,lod)&&
      (globalTime==0||camera||distance||lod)){s.modeDeadline=now+2000;return;}
  if(s.tx.phase==Phase::ActivateCandidate||now>=s.nextModeAudit){s.nextModeAudit=now+250;
    const bool ready=valid&&running&&!building&&ClothBoneOwnedPolicy(true)&&ClothBoneOwnedBindings(false);
    if(ready){s.teamModeConfirmed=true;s.tx.phase=Phase::Active;ClothBoneNote("active-new-native-BBC-unowned-waist-sheet-visual-verification-required");}
    else if(s.teamModeConfirmed||now>=s.modeDeadline){ClothBoneReject("unowned-active-state-or-binding-changed");s.stopRequested=true;return;}}
  if(s.teamModeConfirmed)for(size_t n=0;n<s.bones.size();++n){auto &pose=o.last[n];pose.known=SurfaceVisiblePose(ClothTarget(s.bones[n].bone),pose.position,pose.rotation,pose.scale);}
}
