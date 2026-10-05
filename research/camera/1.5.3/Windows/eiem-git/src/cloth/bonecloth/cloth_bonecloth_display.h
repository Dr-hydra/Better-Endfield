#pragma once
static bool ClothBoneDisplayEligible(const ClothBoneRuntime &s) {
  return s.pending&&s.lease&&s.tx.lease&&!s.stopRequested&&!s.failed&&!s.tx.cancelled&&
      s.tx.phase==eiem_cloth_rebuild::Phase::Active&&s.local.published&&!s.local.cleanup&&
      s.graph[1]&&s.reference[1]&&s.team[1]>0&&ClothOwns(s.owner)&&ClothBoneLongPanelBending(s);
}
static bool ClothDisplayArray(void *manager,const char *field,const char *type,eiem_cloth_display::Array &out) {
  char wrapper[192]{},array[192]{};void *object=nullptr;
  _snprintf_s(wrapper,_TRUNCATE,"BeyondDynamicBone.ExNativeArray<%s>",type);
  _snprintf_s(array,_TRUNCATE,"Unity.Collections.NativeArray<%s>",type);
  return manager&&ClothField(manager,field,wrapper,object)&&object&&
      ClothField(object,"nativeArray",array,out)&&out.data&&out.length>0&&out.length<=65536;
}
static bool ClothBoneDisplayPrepare(ClothBoneRuntime &s) {
  using namespace eiem_cloth_display;
  if(!ClothBoneDisplayEligible(s)||!s_clothDisplayInstaller||!s_clothDisplayInstaller())return false;
  void *team=nullptr,*mesh=nullptr,*simulation=nullptr,*box=nullptr,*parameters=nullptr,*actualProcess=nullptr,*actualData=nullptr;
  Policy p{};p.session=s.owner.session;p.generation=s.owner.generation;p.command=s.command;
  p.process=uintptr_t(CollisionGc(s.process[1]));p.data=uintptr_t(CollisionGc(s.candidateData));p.team=s.team[1];
  ClothInputChunk vc{},pc{};ClothInputArray params{};Array proxyIds{};
  auto bbc=ClothTarget(s.bbc);
  if(!p.process||!p.data||p.data==uintptr_t(CollisionGc(s.data))||
      !bbc||!ClothField(bbc,"process","BeyondDynamicBone.ClothProcess",actualProcess)||uintptr_t(actualProcess)!=p.process||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,actualData)||uintptr_t(actualData)!=p.data||
      !ClothBoneContactTeam(p.team,reinterpret_cast<void*>(p.process),box)||
      !ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",team)||
      !ClothContactManager("get_VMesh","BeyondDynamicBone.VirtualMeshManager",mesh)||
      !ClothContactManager("get_Simulation","BeyondDynamicBone.SimulationManager",simulation)||
      !ClothDisplayArray(team,"teamDataArray","BeyondDynamicBone.TeamManager.TeamData",p.teams)||
      !ClothDisplayArray(simulation,"teamIdArray","System.Int16",p.ids)||
      !ClothDisplayArray(mesh,"vertexRootIndices","System.Int32",p.roots)||
      !ClothDisplayArray(mesh,"attributes","BeyondDynamicBone.VertexAttribute",p.attributes)||
      !ClothDisplayArray(mesh,"teamIds","System.Int16",proxyIds)||
      !ClothInputChunkRead(box,"proxyCommonChunk",p.roots.length,vc,ClothBoneMaxParticles)||
      !ClothInputChunkRead(box,"particleChunk",p.ids.length,pc,ClothBoneMaxParticles)||
      vc.count!=ClothLongPanelParticles||pc.count!=vc.count||s.registeredVertices.size()!=size_t(pc.count)||
      !Range(p.attributes,vc.start,vc.count)||!Range(proxyIds,vc.start,vc.count)||
      !CollisionByteFlag(SurfaceClass("BeyondDynamicBone","VertexAttribute"),"Flag_Move",p.move)||
      !CollisionByteFlag(SurfaceClass("BeyondDynamicBone","VertexAttribute"),"Flag_Fixed",p.fixed)||
      !ClothInputArrayOpen(team,"parameterArray","BeyondDynamicBone.ClothParameters",params)||
      !(parameters=ClothInputArrayBox(params,p.team))||!ClothBoneLongPanelMaterialMatches(s,1,parameters))return false;
  p.proxyStart=vc.start;p.particleStart=pc.start;p.count=pc.count;
  memcpy(p.originalRoots.data(),reinterpret_cast<const int*>(p.roots.data)+vc.start,vc.count*sizeof(int));
  memcpy(p.originalAttributes.data(),reinterpret_cast<const uint8_t*>(p.attributes.data)+vc.start,vc.count);
  const auto ids=reinterpret_cast<const int16_t*>(p.ids.data);const auto &candidate=ClothBoneCandidate(s);
  for(int n=0;n<p.count;++n) {
    const int asset=s.registeredVertices[n];
    if(ids[pc.start+n]!=p.team||reinterpret_cast<const int16_t*>(proxyIds.data)[vc.start+n]!=p.team||
        asset<0||asset>=candidate.boneCount||
        bool(p.originalAttributes[n]&p.move)!=(candidate.bones[asset].attribute==2)||
        bool(p.originalAttributes[n]&p.fixed)!=(candidate.bones[asset].attribute==1))return false;
  }
  return ClothBoneDisplayEligible(s)&&s_clothDisplayView.Publish(p);
}
static void ClothBoneDisplayRefresh() {
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1)return;
  for(int n=0;n<s_clothBoneCount;++n)if(ClothBoneDisplayEligible(s_clothBoneSlots[n])) {
    const bool ok=ClothBoneDisplayPrepare(s_clothBoneSlots[n]);
    if(!ok&&s_clothDisplayReportFrame<ClothFrame()-120) {
      s_clothDisplayReportFrame=ClothFrame();
      Log("[CLOTH-DISPLAY] stage=input-unavailable component=%s generation=%llu reason=%s healthySimulationRetained=1",
          s_clothBoneSlots[n].profile->component,s_clothBoneSlots[n].owner.generation,s_clothDisplayIssue[0]?s_clothDisplayIssue:"identity-or-parameters-unconfirmed");
    }
    break;
  }
}
static void ClothDisplayAdaptAt(eiem_cloth_display::Job *job,int count,void *caller) {
  if(!job||!ClothOnMainThread()||!s_clothInputHooks||s_clothInputUpdateDepth!=1||
      caller!=s_clothDisplayCallsite||!s_clothDisplayView.Ready())return;
  const auto &p=s_clothDisplayView.Identity();bool owned=false;
  for(const auto &s:s_clothBoneSlots)if(ClothBoneDisplayEligible(s)&&s.owner.session==p.session&&
      s.owner.generation==p.generation&&s.command==p.command&&s.team[1]==p.team&&
      uintptr_t(CollisionGc(s.process[1]))==p.process&&uintptr_t(CollisionGc(s.candidateData))==p.data) {owned=true;break;}
  if(!owned){s_clothDisplayView.Revoke();return;}
  int changed=0;const auto result=s_clothDisplayView.Bind(*job,count,changed);
  if(result==eiem_cloth_display::Result::Bound) {
    ++s_clothDisplaySubmissions;
    if(s_clothDisplaySubmissions==1||ClothFrame()-s_clothDisplayReportFrame>=120) {
      s_clothDisplayReportFrame=ClothFrame();
      Log("[CLOTH-DISPLAY] stage=owned-input-bound frame=%d session=%llu generation=%llu command=%llu team=%d Process=%p privateData=%p moves=%d submissions=%llu displayOnlyRootLimit=omitted nativeTetherStretch=%g fixedAndOtherTeamsPreserved=1 sourceArrayWrites=0 dependencyChanges=0 nativeOutput=1 visualVerified=0",
          ClothFrame(),p.session,p.generation,p.command,p.team,(void*)p.process,(void*)p.data,changed,s_clothDisplaySubmissions,ClothLongPanelTetherStretch);
    }
  } else if(result==eiem_cloth_display::Result::Mismatch) {
    s_clothDisplayView.Revoke();
    Log("[CLOTH-DISPLAY] stage=producer-mismatch frame=%d generation=%llu originalInputRetained=1",ClothFrame(),p.generation);
  }
}
static void __fastcall ClothDisplaySetCount(eiem_cloth_display::Job *job,int count,void *method) {
  s_clothDisplaySetCount(job,count,method);
  __try { ClothDisplayAdaptAt(job,count,_ReturnAddress()); }
  __except(EXCEPTION_EXECUTE_HANDLER) {s_clothDisplayView.Revoke();}
}
