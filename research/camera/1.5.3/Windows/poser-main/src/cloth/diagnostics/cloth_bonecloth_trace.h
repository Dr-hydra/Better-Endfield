#pragma once
static std::atomic<unsigned> s_clothBonePauseEvidence{0};
static void ClothBoneRequestPauseEvidence() {
  if(s_clothVerboseDiagnostics.load()) s_clothBonePauseEvidence.fetch_add(1,std::memory_order_release);
}
struct ClothBonePoseTicket {
  eiem_cloth::Owner owner{};
  int frame=-1;
  double playhead=NAN;
  char stage[80]{};
};
static ClothActorBank<ClothBonePoseTicket> s_clothBonePoseTicketActors;
#define s_clothBonePoseTicket (s_clothBonePoseTicketActors.Get())
static bool ClothBoneSolverEligible(const ClothBoneRuntime &s);
static void ClothBoneSolverPoseSubmitted(const char *stage,double playhead) {
  if(!ClothOnMainThread() || !ClothOwns(s_cloth.owner)) return;
  bool observing=ClothShoulderEvidenceWatching();
  for(int n=0;n<s_clothBoneCount;++n) observing=observing || ClothBoneSolverEligible(s_clothBoneSlots[n]);
  if(!observing) return;
  auto &t=s_clothBonePoseTicket;const int frame=ClothFrame();
  if(!(t.owner==s_cloth.owner) || t.frame!=frame || std::isfinite(playhead)) t.playhead=playhead;
  t.owner=s_cloth.owner;t.frame=frame;strncpy_s(t.stage,stage?stage:"unknown",_TRUNCATE);
}
struct ClothBoneSolverTrace {
  ClothInputBinding binding{};
  ClothContactTrace contact{};
  eiem_cloth_input::CostBudget budget{};
  unsigned command=0, captures=0, logged=0;
  unsigned pollIntervalMs=250;
  bool fullTextSuppressed=false;
  bool blendedOutputLogged=false;
  unsigned pauseSeen=0,pauseRemaining=0,pauseRecords=0;
  uint64_t pauseNextMs=0,pauseExpiresMs=0,lastPauseLogMs=0;
  double lastReadbackMs=0,lastObserverMs=0,windowMaxObserverMs=0;
  uint64_t pollMs=0, logMs=0, healthMs=0;
  int frame=-1;
};
static ClothActorBank<std::array<ClothBoneSolverTrace,eiem_cloth_rebuild::BatchCapacity>> s_clothBoneSolverActors;
#define s_clothBoneSolver (s_clothBoneSolverActors.Get())
static bool ClothBoneSolverEligible(const ClothBoneRuntime &s) {
  return s.lease && s.pending && !s.stopRequested && !s.failed &&
      s.tx.phase==eiem_cloth_rebuild::Phase::Active && s.teamModeConfirmed &&
      s.graph[1] && s.reference[1] && s.profile && ClothOwns(s.owner);
}
static void ClothBoneSolverClear(int slot) {
  if(!ClothOnMainThread() || slot<0 || slot>=int(s_clothBoneSolver.size())) return;
  auto &s=s_clothBoneSolver[slot];
  s.binding={}; s.contact.identity={}; s.contact.samples.Clear();
  s.contact.budget={};s.contact.failed=s.contact.outputFailed=false;s.contact.lastFrame=-1;
  strcpy_s(s.contact.issue,"awaiting-current-profile-completed-state");
  s.command=s.captures=s.logged=0;s.pollMs=s.logMs=s.healthMs=0;s.frame=-1;s.budget={};
  s.pollIntervalMs=250;s.fullTextSuppressed=false;s.lastReadbackMs=s.lastObserverMs=s.windowMaxObserverMs=0;
  s.blendedOutputLogged=false;
  s.pauseSeen=s_clothBonePauseEvidence.load(std::memory_order_acquire);
  s.pauseRemaining=s.pauseRecords=0;s.pauseNextMs=s.pauseExpiresMs=s.lastPauseLogMs=0;
}
static bool ClothBoneSolverInitialLogDue(const ClothBoneSolverTrace &s) {
  return s_clothVerboseDiagnostics.load() && !s.logged;
}
static void ClothBoneSolverRefreshPause(ClothBoneSolverTrace &s,uint64_t now) {
  const auto requested=s_clothBonePauseEvidence.load(std::memory_order_acquire);
  if(requested!=s.pauseSeen) {
    s.pauseSeen=requested;s.pauseRemaining=2;s.pauseExpiresMs=now+12000;
    s.pauseNextMs=(std::max)(now,s.lastPauseLogMs?s.lastPauseLogMs+1000:0);
  }
  if(now>s.pauseExpiresMs || s.pauseRecords>=32) s.pauseRemaining=0;
}
static bool ClothBoneSolverPauseLogDue(const ClothBoneSolverTrace &s,uint64_t now) {
  return s_clothVerboseDiagnostics.load() && s.pauseRemaining && s.pauseRecords<32 && now>=s.pauseNextMs && now<=s.pauseExpiresMs;
}
static void ClothBoneSolverPauseLogged(ClothBoneSolverTrace &s,uint64_t now) {
  if(!ClothBoneSolverPauseLogDue(s,now)) return;
  --s.pauseRemaining;++s.pauseRecords;s.lastPauseLogMs=now;s.pauseNextMs=now+1000;
}
static bool ClothBoneSolverCaptureDue(const ClothBoneSolverTrace &s,int frame,uint64_t now,bool activating=false) {
  if(frame<0 || frame==s.frame) return false;
  return activating || s.frame<0 || ClothBoneSolverPauseLogDue(s,now) || now-s.pollMs>=s.pollIntervalMs;
}
static void ClothBoneSolverCost(int slot,double readbackMs,double observerMs) {
  auto &s=s_clothBoneSolver[slot];const auto &b=s_clothBoneSlots[slot];
  s.lastReadbackMs=readbackMs;s.lastObserverMs=observerMs;
  if(std::isfinite(observerMs))s.windowMaxObserverMs=(std::max)(s.windowMaxObserverMs,observerMs);
  if(!s.budget.Observe(observerMs)) return;
  s.budget.overruns=0;
  const unsigned previous=s.pollIntervalMs;
  s.fullTextSuppressed=true;
  s.pollIntervalMs=previous<1000?1000:(std::min)(4000u,previous*2);
  if(previous==s.pollIntervalMs) return;
  Log("[CLOTH-BONE-SOLVER-THROTTLE] component=%s instance=%d session=%llu generation=%llu frame=%d readbackAndChecksMs=%g observerIncludingLogMs=%g pollIntervalMs=%u fullTextSuppressed=1 nativeSimulationRetained=1 localRestoreRequested=0",
      b.profile?b.profile->component:"unknown",b.bbc.id.instance,(unsigned long long)b.owner.session,
      (unsigned long long)b.owner.generation,s.frame,readbackMs,observerMs,s.pollIntervalMs);
}
static bool ClothBoneSolverFail(int slot,const char *reason) {
  auto &s=s_clothBoneSolver[slot];auto &b=s_clothBoneSlots[slot];
  if(!s.contact.failed) Log("[CLOTH-BONE-SOLVER-ISSUE] component=%s instance=%d session=%llu generation=%llu reason=%s diagnosticOnly=%d localRestoreRequested=%d",
      b.profile?b.profile->component:"unknown",b.bbc.id.instance,(unsigned long long)b.owner.session,
      (unsigned long long)b.owner.generation,reason,int(!b.local.requested),int(b.local.requested));
  if(b.local.requested) {
    b.stopRequested=true;b.failed=true;
    if(!b.failure[0]) strncpy_s(b.failure,reason,_TRUNCATE);
  }
  s.contact.failed=true;strncpy_s(s.contact.issue,reason,_TRUNCATE);return false;
}
static bool ClothBoneSolverAccess(void *manager,uintptr_t &access,int &length,void *&getItem) {
  auto cls=manager?il2cpp_object_get_class(manager):nullptr;
  auto f=cls?CollisionFieldInfo(cls,"transformAccessArray","UnityEngine.Jobs.TransformAccessArray"):nullptr;
  auto c=f?il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;uint32_t align=0;
  if(!c || il2cpp_class_value_size(c,&align)!=sizeof(access) ||
      ClothValueOffset(c,"m_TransformArray","System.IntPtr",sizeof(access),sizeof(access))!=0 ||
      !CollisionField(manager,"transformAccessArray","UnityEngine.Jobs.TransformAccessArray",access) || !access) return false;
  getItem=ClothMethod(c,"get_Item","UnityEngine.Transform","System.Int32");
  return getItem && ClothValue(ClothMethod(c,"get_length","System.Int32"),&access,length) && length>0 && length<=8192;
}
static bool ClothBoneSolverBind(int slot,void *manager,uintptr_t &access,void *&getItem,
                                void *&teamBox,ClothInputSample &input) {
  auto &b=s_clothBoneSlots[slot];auto &s=s_clothBoneSolver[slot];auto &v=s.binding;
  void *process=CollisionGc(b.process[1]),*data=CollisionGc(b.candidateData);
  v.privateBBC=b.profile&&b.profile->runtimeUnowned?b.bbc:ClothRef{};
  v.identity={b.owner.session,b.owner.generation,b.owner.character,uintptr_t(process),uintptr_t(data),
      b.bbc.id.instance,b.team[1],s_cloth.scene};v.index=b.index;v.count=0;
  void *roots=nullptr,*list=nullptr;
  if(!process || !data || !ClothField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots) ||
      !ClothField(process,"colliderList",CollisionListType,list) ||
      !ClothField(roots,"_version","System.Int32",v.rootsVersion) ||
      !ClothField(list,"_version","System.Int32",v.collidersVersion)) return false;
  v.roots=uintptr_t(roots);v.colliders=uintptr_t(list);v.rootCount=CollisionCount(roots);
  v.colliderSlots=CollisionCount(list);v.liveColliders=v.emptyColliderSlots=0;
  if(v.rootCount!=ClothBoneCandidate(b).rootCount || v.colliderSlots<=0 || v.colliderSlots>128 || !ClothInputIdentity(true,v)) return false;
  int length=0;
  if(!ClothBoneSolverAccess(manager,access,length,getItem)) return false;
  v.mapping.Refresh(uintptr_t(manager),access,length);
  void *team=nullptr,*registered=nullptr;int id=v.identity.team;void *args[]{&id};
  ClothInputArray teams{};
  if(!ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",team) ||
      !ClothInvoke(ClothMethod(il2cpp_object_get_class(team),"GetClothProcess","BeyondDynamicBone.ClothProcess","System.Int32"),team,args,registered) ||
      registered!=process || !ClothInputArrayOpen(team,"teamDataArray","BeyondDynamicBone.TeamManager.TeamData",teams) ||
      !(teamBox=ClothInputArrayBox(teams,id))) return false;
  input.frame=ClothFrame();input.phase=2;input.epoch=v.mapping.epoch;
  input.teamKnown=ClothInputTeamField(teamBox,"useRelativeTransform","System.Int32",input.relative) &&
      ClothInputTeamField(teamBox,"flag","Unity.Collections.BitField64",input.teamFlags) &&
      ClothValue(ClothInputMethod(il2cpp_object_get_class(teamBox),"get_IsCullingInvisible","System.Boolean"),(char*)teamBox+16,input.culled);
  if(!input.teamKnown || input.culled || input.relative!=0) return true;
  ClothInputChunk chunk{};ClothInputArray transformTeams{};
  if(!ClothInputChunkRead(teamBox,"colliderTransformChunk",length,chunk) ||
      chunk.count!=v.colliderSlots || !ClothInputArrayOpen(manager,"teamIdArray","System.Int16",transformTeams)) return false;
  auto get=ClothMethod(il2cpp_object_get_class(list),"get_Item","BeyondDynamicBone.ColliderComponent","System.Int32");
  for(int n=0;n<v.colliderSlots;++n) {
    void *c=nullptr,*a[]{&n};if(!ClothInvoke(get,list,a,c)) return false;
    if(!c) { ++v.emptyColliderSlots;continue; }
    if(v.count>=ClothContactColliders) return false;
    ClothRef ref{};
    for(size_t k=0;k<b.colliders.size();++k)
      if(ClothTarget(b.colliders[k])==c && k<b.colliderTransforms.size()) ref=b.colliderTransforms[k];
    for(const auto &extra:b.additionalColliders) if(ClothTarget(extra.ref)==c) ref=extra.transform;
    if(b.local.bodyCreated && ClothTarget(b.local.bodyCollider)==c) ref=b.local.bodyTransform;
    for(const auto &r:b.local.fittedBody)if(ClothTarget(r.collider)==c)ref=r.transform;
    for(const auto &side:b.local.side.shapes) if(ClothTarget(side.collider)==c) ref=side.transform;
    if(b.contactConsumer>=0&&b.contactConsumer<s_clothBoneCount) {
      const auto &consumer=s_clothBoneSlots[b.contactConsumer];
      if(!ClothBonePair(b,consumer))return false;
      for(const auto &side:consumer.local.side.shapes)if(ClothTarget(side.collider)==c)ref=side.transform;
    }
    auto t=ClothTarget(ref);if(!t || CollisionTransform(c)!=t || !ClothAnchorUnderOwner(t)) return false;
    int index=chunk.start+n,actualId=0;int16_t ownerTeam=0;void *actual=nullptr,*ia[]{&index};
    if(!ClothInvoke(getItem,&access,ia,actual) || actual!=t ||
        !ClothValue(s_clothUnity.instance,actual,actualId) || actualId!=ref.id.instance ||
        !ClothInputArrayValue(transformTeams,index,"System.Int16",&ownerTeam,sizeof(ownerTeam)) || ownerTeam!=id) return false;
    auto &target=v.targets[v.count];target={};target.transform=ref;target.slot=index;
    strcpy_s(target.role,"collider-input");
    input.points[v.count].slot=index;input.points[v.count].inputKnown=true;
    ++v.count;++v.liveColliders;
  }
  input.count=v.count;v.mapping.cursor=length;
  return ClothInputIdentity(true,v);
}
static bool ClothBoneSolverOutputIdentity(const ClothBoneRuntime &b,const ClothContactSample &p) {
  if(!b.profile || p.particles<=0 || p.particles>ClothContactParticles || p.particles!=ClothBoneCandidate(b).EffectiveCount() || b.bones.size()!=size_t(ClothBoneCandidate(b).boneCount)) return false;
  for(int n=0;n<p.particles;++n) {
    int found=0;
    for(size_t k=0;k<b.bones.size();++k)
      if(ClothBoneCandidate(b).bones[k].attribute!=0 && !ClothBoneCandidate(b).Passive(int(k)) && b.bones[k].bone.id.instance==p.outputs[n].id) ++found;
    if(found!=1) return false;
    for(int k=0;k<n;++k) if(p.outputs[k].id==p.outputs[n].id) return false;
  }
  return true;
}
static bool ClothBoneSolverEdges(int slot,void *team,const ClothContactSample &sample) {
  const auto &b=s_clothBoneSlots[slot];void *mesh=nullptr;
  ClothInputArray edges{},teams{};ClothInputChunk chunk{};
  if(!sample.outputKnown || b.registeredEdges.empty() ||
      !ClothContactManager("get_VMesh","BeyondDynamicBone.VirtualMeshManager",mesh) ||
      !ClothInputArrayOpen(mesh,"edges","Unity.Mathematics.int2",edges) ||
      !ClothInputArrayOpen(mesh,"edgeTeamIdArray","System.Int16",teams) ||
      !ClothInputChunkRead(team,"proxyEdgeChunk",edges.length,chunk,ClothBoneMaxEdges) ||
      chunk.count!=int(b.registeredEdges.size()) || chunk.start>teams.length || chunk.count>teams.length-chunk.start) return false;
  for(int n=0;n<chunk.count;++n) {
    int pair[2]{};int16_t owner=0;
    if(!ClothInputArrayValue(edges,chunk.start+n,"Unity.Mathematics.int2",pair,8) ||
        !ClothInputArrayValue(teams,chunk.start+n,"System.Int16",&owner,2) || owner!=b.team[1]) return false;
    std::array<int,2> actual{-1,-1};
    for(int k=0;k<2;++k) {
      if(pair[k]<0 || pair[k]>=sample.particles) return false;
      const auto id=sample.outputs[pair[k]].id;
      for(size_t j=0;j<b.bones.size();++j) if(b.bones[j].bone.id.instance==id) actual[k]=int(j);
    }
    std::sort(actual.begin(),actual.end());if(actual!=b.registeredEdges[n]) return false;
  }
  return true;
}
static bool ClothBoneLocalRadius(int slot,const ClothContactSample &sample) {
  const auto &b=s_clothBoneSlots[slot];void *manager=nullptr;ClothInputArray parameters{};
  const auto &recipe=*b.local.recipe;
  if(!ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager) ||
      !ClothInputArrayOpen(manager,"parameterArray","BeyondDynamicBone.ClothParameters",parameters)) return false;
  auto box=ClothInputArrayBox(parameters,b.team[1]);float curve[16]{};
  if(!box || !ClothInputTeamField(box,"radiusCurveData","Unity.Mathematics.float4x4",curve)) return false;
  if(recipe.radiusCurve) for(int n=0;n<16;++n)
    if(!std::isfinite(curve[n]) || fabsf(curve[n]-recipe.radiusCurve[n])>1e-5f) return false;
  void *evaluate=nullptr;auto cls=SurfaceClass("BeyondDynamicBone","DataUtility");
  if(!cls || !s_clothMethodFlags || !ClothInputLayout(SurfaceClass("Unity.Mathematics","float4x4"),"Unity.Mathematics.float4x4",64)) return false;
  for(void *it=nullptr,*m=nullptr;(m=il2cpp_class_get_methods(cls,&it));) {
    uint32_t impl=0;
    if(strcmp(il2cpp_method_get_name(m),"EvaluateCurve") || il2cpp_method_get_param_count(m)!=2 ||
        !(s_clothMethodFlags(m,&impl)&0x10) || !CollisionType(il2cpp_method_get_return_type(m),"System.Single") ||
        !CollisionType(il2cpp_method_get_param(m,0),"Unity.Mathematics.float4x4&") ||
        !CollisionType(il2cpp_method_get_param(m,1),"System.Single")) continue;
    if(evaluate) return false;evaluate=m;
  }
  if(!evaluate) return false;
  float maxError=0;int checked=0,fixed=0;
  for(int n=0;n<sample.particles;++n) {
    int added=-1;
    for(int k=0;k<recipe.addedCount;++k) if(b.bones[recipe.originalCount+k].bone.id.instance==sample.outputs[n].id) added=k;
    if(added<0) continue;
    float depth=sample.points[n].depth,actual=0;void *value=nullptr,*args[]{curve,&depth};
    if(!std::isfinite(depth) || depth<0 || depth>1 || !ClothInvoke(evaluate,nullptr,args,value) ||
        !ClothInputCopyBox(value,"System.Single",&actual,4) || !std::isfinite(actual) || actual<=0) return false;
    const float expected=recipe.radii[added],error=fabsf(actual-expected);
    if(!std::isfinite(expected) || expected<=0 || error>.001f) {
      Log("[CLOTH-BONE-LOCAL] stage=radius-envelope-refused bone=%d assetBone=%d attribute=%d depth=%g effectiveRadius=%g expectedRadius=%g rendererUnchanged=1",
          added,recipe.originalCount+added,recipe.added[added].attribute,depth,actual,expected);return false;
    }
    maxError=(std::max)(maxError,error);++checked;fixed+=recipe.added[added].attribute==1;
  }
  if(!b.local.published && !b.local.solverFrames) Log("[CLOTH-BONE-LOCAL] stage=radius-readback-confirmed frame=%d team=%d checked=%d fixed=%d move=%d maxErrorM=%g nativeCurveConfirmed=1 visualVerified=0",
      sample.frame,b.team[1],checked,fixed,checked-fixed,maxError);
  return true;
}
static bool ClothBoneLocalRefuse(int slot,const char *reason) {
  auto &b=s_clothBoneSlots[slot];b.local.readbackIssue=reason;
  Log("[CLOTH-BONE-LOCAL] stage=readback-refused team=%d reason=%s renderPublished=%d",
      b.team[1],reason,int(b.local.published));
  return false;
}
static bool ClothBoneLocalDistanceParameters(void *box,float (&curve)[16],float &attenuation) {
  constexpr const char *type="BeyondDynamicBone.DistanceConstraint.DistanceConstraintParams";
  auto f=box?CollisionFieldInfo(il2cpp_object_get_class(box),"distanceConstraint",type):nullptr;
  auto cls=f?il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;
  uint32_t align=0;float values[17]{};
  if(!cls || il2cpp_class_value_size(cls,&align)!=sizeof(values) ||
      ClothValueOffset(cls,"restorationStiffness","Unity.Mathematics.float4x4",sizeof(values),sizeof(curve))!=0 ||
      ClothValueOffset(cls,"velocityAttenuation","System.Single",sizeof(values),4)!=64 ||
      !ClothInputTeamField(box,"distanceConstraint",type,values)) return false;
  memcpy(curve,values,sizeof(curve));attenuation=values[16];
  return true;
}
static bool ClothBoneLocalDistancePolicy(int slot,const ClothContactSample &sample) {
  auto &b=s_clothBoneSlots[slot];void *manager=nullptr;ClothInputArray parameters{};
  if(!ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager) ||
      !ClothInputArrayOpen(manager,"parameterArray","BeyondDynamicBone.ClothParameters",parameters))
    return ClothBoneLocalRefuse(slot,"local-distance-effective-parameters-unavailable");
  float curve[16]{},attenuation=NAN;
  auto box=ClothInputArrayBox(parameters,b.team[1]);
  if(!ClothBoneLocalDistanceParameters(box,curve,attenuation))
    return ClothBoneLocalRefuse(slot,"local-distance-effective-parameter-layout-unconfirmed");
  for(int k=0;k<16;++k) if(!std::isfinite(curve[k]) || fabsf(curve[k]-(b.local.recipe->distanceCurve?b.local.recipe->distanceCurve[k]:b.local.recipe->distanceStiffness))>1e-6f)
    return ClothBoneLocalRefuse(slot,"local-distance-effective-stiffness-mismatch");
  if(!std::isfinite(attenuation)) return ClothBoneLocalRefuse(slot,"local-distance-effective-attenuation-invalid");
  if(ClothBoneElasticRequested(b) && !ClothBoneElasticMatches(b,box))
    return ClothBoneLocalRefuse(slot,"cloth-elastic-effective-tether-or-bending-mismatch");
  if((ClothBoneAttachmentRequested(b) || b.local.attachmentConfigured) && !ClothBoneAttachmentMatches(b,1,box))
    return ClothBoneLocalRefuse(slot,"paired-waist-effective-output-policy-mismatch");
  if(!b.local.distanceParametersReadback) {
    b.local.distanceParametersReadback=true;
    if(b.local.recipe->distanceCurve)Log("[CLOTH-AUTO] stage=distance-policy-confirmed frame=%d team=%d sourceCurveRetained=%d samples=16 nativeVelocityAttenuation=%g reference=%s",sample.frame,b.team[1],!b.local.recipe->resampledPanel,attenuation,b.local.recipe->RetainsSourceReference()?"source-retained":"natural");
    else Log("[CLOTH-BONE-LOCAL] stage=distance-policy-confirmed frame=%d team=%d effectiveStiffness=%g samples=16 nativeVelocityAttenuation=%g reference=natural",
        sample.frame,b.team[1],b.local.recipe->distanceStiffness,attenuation);
    if(ClothBoneElasticRequested(b)) Log("[CLOTH-BONE-ELASTIC] stage=Team-confirmed frame=%d team=%d generation=%llu session=%llu distance=%g stretchThreshold=%g bending=%g compression=%g conversions=%u visualVerified=0",
        sample.frame,b.team[1],b.owner.generation,b.owner.session,b.local.recipe->distanceStiffness,
        b.local.recipe->tetherStretch,b.local.recipe->bendingStiffness,b.local.elasticCompression,b.local.elasticConversions);
  }
  return true;
}
static bool ClothBoneLocalDistances(int slot,void *team,const ClothContactSample &sample) {
  auto &b=s_clothBoneSlots[slot];auto &l=b.local;
  if(l.distanceReadback) return true;
  void *simulation=nullptr,*distance=nullptr;ClothInputArray starts{},data{},lengths{};
  ClothInputChunk sc{},dc{};
  if(!ClothContactManager("get_Simulation","BeyondDynamicBone.SimulationManager",simulation) ||
      !ClothField(simulation,"distanceConstraint","BeyondDynamicBone.DistanceConstraint",distance) || !distance ||
      !ClothInputArrayOpen(distance,"indexArray","System.UInt32",starts) ||
      !ClothInputArrayOpen(distance,"dataArray","System.UInt16",data) ||
      !ClothInputArrayOpen(distance,"distanceArray","System.Single",lengths) ||
      !ClothInputChunkRead(team,"distanceStartChunk",starts.length,sc,ClothContactParticles) || sc.count!=sample.particles ||
      !ClothInputChunkRead(team,"distanceDataChunk",data.length,dc,4096) || dc.count<=0 || dc.count>4096 ||
      dc.start>lengths.length || dc.count>lengths.length-dc.start) {
    Log("[CLOTH-BONE-LOCAL] stage=distance-array-refused frame=%d team=%d indexLength=%d dataLength=%d lengthLength=%d indexChunk=%d/%d dataChunk=%d/%d",
        sample.frame,b.team[1],starts.length,data.length,lengths.length,sc.start,sc.count,dc.start,dc.count);
    return ClothBoneLocalRefuse(slot,"local-distance-arrays-or-chunks-unconfirmed");
  }
  std::vector<int> cross(l.recipe->crossCount);int negative=0,entries=0;
  const bool traceDistances=ClothBoneLongPanelBending(b);
  std::vector<std::vector<std::pair<uint16_t,float>>> distanceRows;
  if(traceDistances)distanceRows.resize(sc.count);
  if(cross.size()!=l.crossRest.size()) return ClothBoneLocalRefuse(slot,"local-cross-distance-recipe-mismatch");
  for(int n=0;n<sc.count;++n) {
    uint32_t packed=0;
    if(!ClothInputArrayValue(starts,sc.start+n,"System.UInt32",&packed,4)) {
      Log("[CLOTH-BONE-LOCAL] stage=distance-element-refused frame=%d array=indexArray index=%d type=System.UInt32",sample.frame,sc.start+n);
      return ClothBoneLocalRefuse(slot,"local-distance-packed-index-unreadable");
    }
    const unsigned count=packed>>20,offset=packed&0xfffff;
    if(offset>unsigned(dc.count) || count>unsigned(dc.count)-offset) {
      Log("[CLOTH-BONE-LOCAL] stage=distance-range-refused frame=%d particle=%d packed=%u offset=%u count=%u length=%d",sample.frame,n,packed,offset,count,dc.count);
      return ClothBoneLocalRefuse(slot,"local-distance-packed-range-invalid");
    }
    for(unsigned k=0;k<count;++k) {
      uint16_t neighbor=0;float rest=0;const int index=dc.start+int(offset+k);
      if(!ClothInputArrayValue(data,index,"System.UInt16",&neighbor,2)) {
        Log("[CLOTH-BONE-LOCAL] stage=distance-element-refused frame=%d array=dataArray index=%d type=System.UInt16",sample.frame,index);
        return ClothBoneLocalRefuse(slot,"local-distance-neighbor-unreadable");
      }
      if(!ClothInputArrayValue(lengths,index,"System.Single",&rest,4)) {
        Log("[CLOTH-BONE-LOCAL] stage=distance-element-refused frame=%d array=distanceArray index=%d type=System.Single",sample.frame,index);
        return ClothBoneLocalRefuse(slot,"local-distance-length-unreadable");
      }
      if(neighbor>=sample.particles || !std::isfinite(rest) || fabsf(rest)<1e-8f) {
        Log("[CLOTH-BONE-LOCAL] stage=distance-value-refused frame=%d particle=%d neighbor=%u rest=%g index=%d",sample.frame,n,unsigned(neighbor),rest,index);
        return ClothBoneLocalRefuse(slot,"local-distance-neighbor-or-length-invalid");
      }
      ++entries;if(rest<0) ++negative;
      if(traceDistances)distanceRows[n].push_back({neighbor,rest});
      for(size_t pair=0;pair<cross.size();++pair) {
        const int a=b.bones[l.recipe->cross[pair][0]].bone.id.instance,c=b.bones[l.recipe->cross[pair][1]].bone.id.instance;
        if((sample.outputs[n].id==a && sample.outputs[neighbor].id==c) ||
            (sample.outputs[n].id==c && sample.outputs[neighbor].id==a)) {
          if(rest>=0 || fabsf(fabsf(rest)-l.crossRest[pair])>.0002f) {
            Log("[CLOTH-BONE-LOCAL] stage=distance-length-refused frame=%d pair=%zu registered=%g construction=%g",sample.frame,pair,rest,l.crossRest[pair]);
            return ClothBoneLocalRefuse(slot,"local-cross-distance-length-mismatch");
          }
          ++cross[pair];
        }
      }
    }
  }
  const auto missing=std::find_if(cross.begin(),cross.end(),[](int count){return count!=2;});
  if(entries!=dc.count || missing!=cross.end()) {
    Log("[CLOTH-BONE-LOCAL] stage=distance-pairs-refused frame=%d entries=%d expectedEntries=%d requiredPairs=%zu firstMissing=%d reciprocalEntries=%d",
        sample.frame,entries,dc.count,cross.size(),missing==cross.end()?-1:int(missing-cross.begin()),missing==cross.end()?0:*missing);
    return ClothBoneLocalRefuse(slot,"local-cross-distance-pairs-mismatch");
  }
  l.distanceReadback=true;
  Log("[CLOTH-BONE-LOCAL] stage=distance-registered frame=%d team=%d entries=%d nonParentEntries=%d newCrossPairs=%zu reciprocal=1 restLengthsConfirmed=1 nativeArraysReadOnly=1",
      sample.frame,b.team[1],entries,negative,cross.size());
  if(traceDistances) {
    Log("[CLOTH-BONE-DISTANCE-BEGIN] slot=%d frame=%d session=%llu generation=%llu command=%u instance=%d team=%d Process=%p privateData=%p particles=%d entries=%d indexChunk=%d/%d dataChunk=%d/%d oncePerRegistration=1 nativeArraysReadOnly=1",
        slot,sample.frame,b.owner.session,b.owner.generation,b.command,b.bbc.id.instance,b.team[1],
        CollisionGc(b.process[1]),CollisionGc(b.candidateData),sample.particles,entries,sc.start,sc.count,dc.start,dc.count);
    for(int n=0;n<sc.count;++n) {
      std::ostringstream row;row<<std::setprecision(12);
      row<<"{\"slot\":"<<slot<<",\"frame\":"<<sample.frame<<",\"index\":"<<n<<",\"id\":"<<sample.outputs[n].id
         <<",\"particle\":"<<sample.points[n].particle<<",\"attribute\":"<<unsigned(sample.points[n].attribute)<<",\"links\":[";
      for(size_t k=0;k<distanceRows[n].size();++k) {
        if(k)row<<',';const auto &edge=distanceRows[n][k];
        row<<'['<<edge.first<<','<<edge.second<<']';
      }
      row<<"]}";Log("[CLOTH-BONE-DISTANCE-DATA] %s",row.str().c_str());
    }
    Log("[CLOTH-BONE-DISTANCE-END] slot=%d frame=%d particles=%d entries=%d",slot,sample.frame,sample.particles,entries);
  }
  return true;
}
static bool ClothBoneLocalAcceptSurface(int slot,const eiem_cloth_skin::Result &result) {
  if(!result.valid) return ClothBoneLocalRefuse(slot,"local-surface-correspondence-invalid");
  if(result.seamSeparated) return ClothBoneLocalRefuse(slot,"local-surface-seam-separated");
  return true;
}
static bool ClothBoneLocalSurfaceLogDue(ClothBoneLocalState &l,const eiem_cloth_skin::Result &result,uint64_t now) {
  const bool firstStretch=result.exceededEdges>0 && !l.surfaceStretchLogged;
  if(!result.RestoreRequired() && !firstStretch &&
      (l.surfaceLogs>=128 || (l.surfaceLogs && now-l.surfaceLogMs<2000))) return false;
  l.surfaceLogMs=now;++l.surfaceLogs;
  if(result.exceededEdges>0) l.surfaceStretchLogged=true;
  return true;
}
static eiem_cloth_skin::Result ClothBoneLocalMeshSurface(const ClothBoneRuntime &b,size_t layer,
    const std::vector<eiem_cloth_skin::Matrix> &native) {
  eiem_cloth_skin::Result invalid{};invalid.valid=false;
  const auto &config=b.local.recipe->meshes[layer];const auto &l=b.local.layers[layer];
  if(l.renderer<0 || size_t(l.renderer)>=b.renderers.size() || config.foreignCount<0 ||
      native.size()+config.foreignCount>eiem_cloth_skin::MaxMatrices || (config.foreignCount && !config.foreignBindings)) return invalid;
  const auto &r=b.renderers[l.renderer];std::vector<eiem_cloth_skin::Matrix> matrices=native;
  for(int n=0;n<config.foreignCount;++n) {
    const int k=config.foreignBindings[n];ClothInputPose pose{};
    if(k<0)return invalid;void *bone=nullptr,*parent=nullptr;
    if(size_t(k)<r.bones.size()) {
      if(size_t(k)>=r.parents.size()||b.profile->renderers[l.renderer].bones[k].cloth>=0)return invalid;
      bone=ClothTarget(r.bones[k]);parent=ClothTarget(r.parents[k]);
    } else {
      const int appended=k-config.sourceBones;
      if(appended<0||appended>=config.bindingCount||config.bindingNativeIndices[appended]>=0)return invalid;
      const auto ref=ClothBoneLocalBindingRef(b,config.bindingNativeIndices[appended]);
      bone=ClothTarget(ref);parent=CollisionParent(bone);
    }
    if(!bone || !parent || CollisionParent(bone)!=parent || !ClothInputPoseRead(bone,pose)) return invalid;
    eiem_cloth_skin::Matrix m{};for(int j=0;j<16;++j)m[j]=pose.matrix[j];matrices.push_back(m);
  }
  return eiem_cloth_skin::Check(config.samples,config.sampleCount,config.edges,config.edgeCount,
      config.seams,config.seamCount,matrices.data(),matrices.size());
}
static bool ClothBoneLocalSurfaceMatrices(int slot,const ClothContactSample &sample,
    std::vector<eiem_cloth_skin::Matrix> &matrices) {
  if(!ClothOnMainThread() || slot<0 || slot>=s_clothBoneCount) return false;
  auto &b=s_clothBoneSlots[slot];auto &l=b.local;
  if(!ClothOwns(b.owner) || b.stopRequested || !l.created || !l.recipe || !sample.outputKnown ||
      sample.frame!=ClothFrame() || !ClothBoneSolverOutputIdentity(b,sample))
    return ClothBoneLocalRefuse(slot,"local-surface-effective-output-mismatch");
  const auto &profile=ClothBoneCandidate(b);
  if(l.recipe->Total()!=profile.boneCount || !ClothBoneIdentityBudget(profile.boneCount,profile.EffectiveCount()) ||
      ((!l.recipe->meshes||l.recipe->meshCount<1)&&!l.recipe->NativeSkinRetained()) || l.recipe->meshCount>16)
    return ClothBoneLocalRefuse(slot,"local-surface-source-layout-mismatch");
  matrices.resize(l.recipe->Total());std::vector<bool> found(matrices.size()),referenced(matrices.size());
  for(auto &matrix:matrices)matrix.fill(NAN);
  for(int n=0;n<sample.particles;++n) for(size_t k=0;k<matrices.size();++k)
    if(profile.bones[k].attribute && !profile.Passive(int(k)) && b.bones[k].bone.id.instance==sample.outputs[n].id) {
      if(found[k]) return ClothBoneLocalRefuse(slot,"local-surface-output-duplicate");found[k]=true;
      for(int j=0;j<16;++j) {
        if(!std::isfinite(sample.outputs[n].visible.matrix[j]))return ClothBoneLocalRefuse(slot,"local-surface-output-matrix-invalid");
        matrices[k][j]=sample.outputs[n].visible.matrix[j];
      }
    }
  auto references=[&](const eiem_cloth_skin::Sample *points,size_t count,int foreign) {
    if(!points || !count || count>8192 || foreign<0 || matrices.size()+foreign>eiem_cloth_skin::MaxMatrices)return false;
    for(size_t n=0;n<count;++n)for(const auto *skin:{points[n].original,points[n].candidate})for(int k=0;k<4;++k) {
      const auto &v=skin[k];if(!std::isfinite(v.weight)||v.weight<0)return false;if(!v.weight)continue;
      if(v.bone<0||size_t(v.bone)>=matrices.size()+foreign)return false;
      if(size_t(v.bone)<matrices.size())referenced[v.bone]=true;
    }
    return true;
  };
  if(l.recipe==&ClothRingRecipe) {
    if(!references(ClothLocalSurfaceSamples,std::size(ClothLocalSurfaceSamples),0))
      return ClothBoneLocalRefuse(slot,"local-surface-correspondence-invalid");
  } else for(int n=0;n<l.recipe->meshCount;++n) {
    const auto &mesh=l.recipe->meshes[n];
    if(!references(mesh.samples,mesh.sampleCount,mesh.foreignCount))
      return ClothBoneLocalRefuse(slot,"local-surface-correspondence-invalid");
  }
  int retainedInputs=0,unused=0;
  for(size_t k=0;k<matrices.size();++k) if(!found[k]) {
    if(profile.bones[k].attribute && !profile.Passive(int(k)))
      return ClothBoneLocalRefuse(slot,"local-surface-output-missing");
    if(!referenced[k]){++unused;continue;}
    auto bone=ClothTarget(b.bones[k].bone),parent=ClothTarget(b.bones[k].parent);ClothInputPose pose{};
    if(!bone || !parent || CollisionParent(bone)!=parent || !ClothInputPoseRead(bone,pose) ||
        !ClothOwns(b.owner) || b.stopRequested || ClothTarget(b.bones[k].bone)!=bone ||
        ClothTarget(b.bones[k].parent)!=parent || CollisionParent(bone)!=parent || sample.frame!=ClothFrame())
      return ClothBoneLocalRefuse(slot,"local-surface-retained-binding-unavailable");
    for(int j=0;j<16;++j)matrices[k][j]=pose.matrix[j];++retainedInputs;
  }
  if(l.recipe->runtimeGenerated && !l.solverFrames)
    Log("[CLOTH-AUTO] stage=surface-output-map frame=%d team=%d sourceIdentities=%zu effective=%d nativeOutputs=%d retainedBindingInputs=%d unusedExcludedSlots=%d missingEffectiveOutputs=0",
        sample.frame,b.team[1],matrices.size(),profile.EffectiveCount(),sample.particles,retainedInputs,unused);
  return true;
}
static bool ClothBoneLocalSurface(int slot,void *team,const ClothContactSample &sample) {
  auto &b=s_clothBoneSlots[slot];auto &l=b.local;float ratio=NAN;
  float expected=0;
  if(l.recipe->RetainsSourceReference()&&(!ClothField(CollisionGc(b.data),"animationPoseRatio","System.Single",expected)||!std::isfinite(expected)||expected<0||expected>1))
    return ClothBoneLocalRefuse(slot,l.recipe->NativeBodyOnly()?"body-contact-source-reference-unavailable":"coat-waist-source-reference-unavailable");
  if(!ClothInputTeamField(team,"animationPoseRatio","System.Single",ratio) || !std::isfinite(ratio) || fabsf(ratio-expected)>.000001f) {
    Log("[CLOTH-BONE-LOCAL] stage=effective-reference-refused frame=%d effectiveRatio=%g expected=%g",sample.frame,ratio,expected);
    return ClothBoneLocalRefuse(slot,"local-effective-animation-reference-mismatch");
  }
  if(!ClothBoneLocalDistancePolicy(slot,sample) || !ClothBoneLocalDistances(slot,team,sample)) return false;
  std::vector<eiem_cloth_skin::Matrix> matrices;
  if(!ClothBoneLocalSurfaceMatrices(slot,sample,matrices)) return false;
  if(l.recipe->NativeSkinRetained())return true;
  eiem_cloth_skin::Result result{};
  size_t sampleCount=0,edgeCount=0,seamCount=0;
  if(l.recipe==&ClothRingRecipe) {
    sampleCount=std::size(ClothLocalSurfaceSamples);edgeCount=std::size(ClothLocalSurfaceEdges);seamCount=std::size(ClothLocalSurfaceSeams);
    result=eiem_cloth_skin::Check(ClothLocalSurfaceSamples,sampleCount,ClothLocalSurfaceEdges,edgeCount,
        ClothLocalSurfaceSeams,seamCount,matrices.data(),matrices.size());
  } else for(size_t k=0;k<l.layers.size();++k) {
    const auto &c=l.recipe->meshes[k];const auto r=ClothBoneLocalMeshSurface(b,k,matrices);
    sampleCount+=c.sampleCount;edgeCount+=c.edgeCount;seamCount+=c.seamCount;
    if(!r.valid || r.seamSeparated) {
      Log("[CLOTH-BONE-LOCAL] stage=mesh-surface-refused renderer=%s valid=%d seamGap=%g",c.renderer,int(r.valid),r.seamGap);
      return ClothBoneLocalAcceptSurface(slot,r);
    }
    if(r.stretch>result.stretch) {result.stretch=r.stretch;result.relative=r.relative;result.a=r.a;result.b=r.b;}
    result.seamGap=(std::max)(result.seamGap,r.seamGap);result.exceeded=result.exceeded||r.exceeded;
    result.exceededEdges+=r.exceededEdges;
    if(r.exceededEdges && r.failureCurrent-r.failureSource>result.failureCurrent-result.failureSource) {
      result.failureA=r.failureA;result.failureB=r.failureB;result.failureRest=r.failureRest;
      result.failureSource=r.failureSource;result.failureCurrent=r.failureCurrent;
    }
  }
  const auto now=GetTickCount64();
  if(ClothBoneLocalSurfaceLogDue(l,result,now)) {
    Log("[CLOTH-BONE-LOCAL] stage=surface-correspondence frame=%d effectiveRatio=%g samples=%zu edges=%zu seamPairs=%zu maxStretch=%g relativeToSource=%g edge=%d/%d seamGap=%g valid=%d exceeded=%d stretchPolicy=diagnostic-only restoreRequired=%d renderDepth=unmeasured",
        sample.frame,ratio,sampleCount,edgeCount,seamCount,
        result.stretch,result.relative,result.a,result.b,result.seamGap,int(result.valid),int(result.exceeded),int(result.RestoreRequired()));
    if(result.exceededEdges>0) Log("[CLOTH-BONE-LOCAL] stage=surface-stretch-warning frame=%d exceededEdges=%d edge=%d/%d rest=%g source=%g candidate=%g seamGap=%g action=%s",
        sample.frame,result.exceededEdges,result.failureA,result.failureB,result.failureRest,result.failureSource,result.failureCurrent,result.seamGap,
        result.RestoreRequired()?"restore-invalid-or-separated-surface":"continue-native-simulation");
    double worst=0,display=0;int worstPair=-1;
    for(size_t pair=0;pair<l.crossRest.size();++pair) {
      const double *a=nullptr,*c=nullptr;
      for(int n=0;n<sample.particles;++n) {
        if(sample.outputs[n].id==b.bones[l.recipe->cross[pair][0]].bone.id.instance) a=sample.points[n].display;
        if(sample.outputs[n].id==b.bones[l.recipe->cross[pair][1]].bone.id.instance) c=sample.points[n].display;
      }
      if(a && c && l.crossRest[pair]>0) {
        double distance=0;for(int k=0;k<3;++k) distance+=(a[k]-c[k])*(a[k]-c[k]);distance=std::sqrt(distance);
        if(distance/l.crossRest[pair]>worst) {worst=distance/l.crossRest[pair];display=distance;worstPair=int(pair);}
      }
    }
    Log("[CLOTH-BONE-LOCAL] stage=cross-distance-observed frame=%d pairs=%zu worstPair=%d rest=%g display=%g ratio=%g sourceStepUnknown=1",
        sample.frame,l.crossRest.size(),worstPair,worstPair>=0?l.crossRest[worstPair]:0,display,worst);
  }
  return ClothBoneLocalAcceptSurface(slot,result);
}
static bool ClothBoneBodyInput(const ClothBoneRuntime &b,const ClothContactSample &sample,bool completed=true) {
  if(b.local.fittedBodyCreated){const auto &l=b.local;
    if(l.fittedBody.size()!=size_t(l.recipe->bodySphereCount))return false;
    for(size_t k=0;k<l.fittedBody.size();++k){const auto &r=l.fittedBody[k];const ClothContactCollider *shape=nullptr;
      for(int n=0;n<sample.colliders;++n)if(sample.shapes[n].id==r.collider.id.instance){if(shape)return false;shape=&sample.shapes[n];}
      const auto &fit=l.recipe->bodySpheres[k];
      if(!shape||shape->transform!=r.transform.id.instance||strcmp(shape->componentType,fit.Capsule()?"BeyondBoneCapsuleCollider":"BeyondBoneSphereCollider")||!sample.validMask||!sample.enableMask||
          (shape->flags&(sample.validMask|sample.enableMask))!=(sample.validMask|sample.enableMask)||!std::isfinite(shape->size[0])||
          fabsf(shape->size[0]-l.recipe->bodySpheres[k].radius)>1e-6f||!std::isfinite(shape->radius[0])||shape->radius[0]<0||(completed&&shape->radius[0]==0))return false;
      for(int j=0;j<3;++j)if(!std::isfinite(shape->center[j])||fabsf(shape->center[j])>1e-6f||!std::isfinite(shape->oldEndpoints[j])||!std::isfinite(shape->nextEndpoints[j]))return false;
      if(fit.Capsule()){
        if(!std::isfinite(shape->size[1])||!std::isfinite(shape->size[2])||fabsf(shape->size[1]-fit.endRadius)>1e-6f||fabsf(shape->size[2]-fit.length)>1e-6f||
            !std::isfinite(shape->radius[1])||shape->radius[1]<0||(completed&&shape->radius[1]==0))return false;
        for(int j=3;j<6;++j)if(!std::isfinite(shape->oldEndpoints[j])||!std::isfinite(shape->nextEndpoints[j]))return false;
        if(completed){double span=0;for(int j=0;j<3;++j)span+=std::pow(shape->nextEndpoints[j]-shape->nextEndpoints[j+3],2);
          const double scale=shape->radius[0]/fit.radius;
          if(fabs(shape->radius[1]-fit.endRadius*scale)>.001||fabs(std::sqrt(span)-(fit.length-fit.radius-fit.endRadius)*scale)>.002)return false;}
      }
    }
  }
  if(!b.local.bodyCreated) return true;
  const ClothContactCollider *shape=nullptr;
  for(int n=0;n<sample.colliders;++n) if(sample.shapes[n].id==b.local.bodyCollider.id.instance) {
    if(shape) return false;shape=&sample.shapes[n];
  }
  if(!shape || shape->transform!=b.local.bodyTransform.id.instance || strcmp(shape->componentType,"BeyondBoneSphereCollider") ||
      !sample.validMask || !sample.enableMask ||
      (shape->flags&(sample.validMask|sample.enableMask))!=(sample.validMask|sample.enableMask) ||
      !std::isfinite(shape->size[0]) || fabsf(shape->size[0]-ClothBodyContactRadius)>1e-6f ||
      !std::isfinite(shape->radius[0]) || shape->radius[0]<0 || (completed && shape->radius[0]==0)) return false;
  for(float v:shape->center) if(!std::isfinite(v) || fabsf(v)>1e-6f) return false;
  for(int k=0;k<3;++k) if(!std::isfinite(shape->oldEndpoints[k]) || !std::isfinite(shape->nextEndpoints[k])) return false;
  return true;
}
static bool ClothBoneSideInput(const ClothBoneRuntime &b,const ClothContactSample &sample,bool completed=true) {
  const auto &l=b.local.side;if(!l.created) return true;
  if(l.shapes.size()!=std::size(ClothSideShapes)) return false;
  for(size_t k=0;k<l.shapes.size();++k) {
    const auto &r=l.shapes[k];const ClothContactCollider *shape=nullptr;
    for(int n=0;n<sample.colliders;++n) if(sample.shapes[n].id==r.collider.id.instance) {
      if(shape) return false;shape=&sample.shapes[n];
    }
    if(!shape || shape->transform!=r.transform.id.instance || strcmp(shape->componentType,"BeyondBoneSphereCollider") ||
        !sample.validMask || !sample.enableMask ||
        (shape->flags&(sample.validMask|sample.enableMask))!=(sample.validMask|sample.enableMask) ||
        !std::isfinite(shape->size[0]) || fabsf(shape->size[0]-ClothSideShapes[k].radius)>1e-6f ||
        !std::isfinite(shape->radius[0]) || shape->radius[0]<0 || (completed && shape->radius[0]==0)) return false;
    for(int j=0;j<3;++j) if(!std::isfinite(shape->center[j]) || fabsf(shape->center[j])>1e-6f ||
        !std::isfinite(shape->oldEndpoints[j]) || !std::isfinite(shape->nextEndpoints[j])) return false;
  }
  return true;
}
static bool ClothBoneColliderWorkPending(ClothBoneLocalState &local,const ClothContactSample &sample,
                                        int &empty,int &populated) {
  empty=populated=0;
  if(!sample.outputKnown || sample.frame<0 || sample.colliders<1 || sample.colliders>ClothContactColliders ||
      sample.reportedColliders!=sample.colliders || !std::isfinite(sample.blendWeight) ||
      !std::isfinite(sample.simulateWeight) || sample.simulateWeight<=0 ||
      !std::isfinite(sample.lodWeight) || sample.lodWeight<=0 || !sample.validMask || !sample.enableMask)return false;
  std::array<std::array<int,2>,ClothContactColliders> filled{};
  for(int n=0;n<sample.colliders;++n) {
    const auto &c=sample.shapes[n];
    if(!c.id || !c.transform || (c.flags&(sample.validMask|sample.enableMask))!=(sample.validMask|sample.enableMask))return false;
    for(int k=0;k<n;++k)if(sample.shapes[k].id==c.id)return false;
    for(float v:c.size)if(!std::isfinite(v))return false;
    if(c.size[0]<=0)return false;
    for(float v:c.center)if(!std::isfinite(v))return false;
    bool zero=true;
    for(float v:c.radius){if(!std::isfinite(v)||v<0)return false;zero&=v==0;}
    for(double v:c.oldEndpoints){if(!std::isfinite(v))return false;zero&=v==0;}
    for(double v:c.nextEndpoints){if(!std::isfinite(v))return false;zero&=v==0;}
    if(zero) {++empty;continue;}
    const bool sphere=!strcmp(c.componentType,"BeyondBoneSphereCollider");
    const bool capsule=!strcmp(c.componentType,"BeyondBoneCapsuleCollider");
    if((!sphere&&!capsule)||c.radius[0]<=0||(capsule&&c.radius[1]<=0))return false;
    filled[populated++]={c.id,c.transform};
  }
  if(!empty)return false;
  for(const auto &prior:local.colliderInputPopulated)
    if(std::find(filled.begin(),filled.begin()+populated,prior)==filled.begin()+populated)return false;
  local.colliderInputPopulated.assign(filled.begin(),filled.begin()+populated);
  return true;
}
enum class ClothBoneInputReadiness { Failed, Waiting, Ready };
static ClothBoneInputReadiness ClothBoneLocalInputReadiness(int slot,const ClothContactSample &sample,uint64_t now) {
  auto &b=s_clothBoneSlots[slot];auto &l=b.local;l.colliderInputPending=false;
  const auto refuse=[&](const char *reason){ClothBoneLocalRefuse(slot,reason);return ClothBoneInputReadiness::Failed;};
  if(!ClothBoneSolverEligible(b))return refuse("local-collider-input-owner-or-lease-changed");
  if(!ClothBoneBodyInput(b,sample,false))return refuse("local-body-collider-native-input-unconfirmed");
  if(!ClothBoneSideInput(b,sample,false))return refuse("body-side-collider-native-input-unconfirmed");
  const bool body=ClothBoneBodyInput(b,sample),side=ClothBoneSideInput(b,sample);
  if(body && side) {
    if(!l.colliderInputReady && l.colliderInputWaitFrames)
      Log("[CLOTH-BONE-INPUT] stage=first-workdata-ready component=%s session=%llu generation=%llu command=%u frame=%d team=%d waitFrames=%u waitMs=%llu renderPublished=%d contactVerified=0",
          b.profile->component,b.owner.session,b.owner.generation,b.command,sample.frame,b.team[1],l.colliderInputWaitFrames,
          now-l.colliderInputWaitStart,int(l.published));
    l.colliderInputReady=true;return ClothBoneInputReadiness::Ready;
  }
  int empty=0,populated=0;
  if(l.colliderInputReady || l.solverFrames || l.published || !ClothBoneColliderWorkPending(l,sample,empty,populated))
    return refuse(!body?"local-body-collider-native-input-unconfirmed":"body-side-collider-native-input-unconfirmed");
  if(!l.colliderInputWaitFrames) {
    l.colliderInputWaitStart=now;
    Log("[CLOTH-BONE-INPUT] stage=awaiting-first-workdata component=%s session=%llu generation=%llu command=%u frame=%d team=%d colliders=%d pendingColliders=%d populatedColliders=%d blendWeight=%g renderPublished=0 maxWaitMs=750 maxFrames=120",
        b.profile->component,b.owner.session,b.owner.generation,b.command,sample.frame,b.team[1],sample.colliders,empty,populated,sample.blendWeight);
  }
  if(now>=b.modeDeadline || now-l.colliderInputWaitStart>=750 ||
      (sample.frame!=l.colliderInputWaitFrame && l.colliderInputWaitFrames>=120))
    return refuse("local-collider-first-workdata-timeout");
  if(sample.frame!=l.colliderInputWaitFrame) {l.colliderInputWaitFrame=sample.frame;++l.colliderInputWaitFrames;}
  l.colliderInputPending=true;return ClothBoneInputReadiness::Waiting;
}
static bool ClothBoneLocalOutputs(int slot,const ClothContactSample &sample) {
  const float weight=sample.simulateWeight*sample.lodWeight;
  if(!sample.outputKnown || sample.particles<1 || sample.particles>ClothContactParticles ||
      !std::isfinite(sample.simulateWeight) || sample.simulateWeight<0 ||
      !std::isfinite(sample.lodWeight) || sample.lodWeight<0 || !std::isfinite(weight))
    return ClothBoneLocalRefuse(slot,"local-output-weight-or-mapping-unavailable");
  int localCount=0;double maxRaw=0,maxError=0;
  for(int n=0;n<sample.particles;++n) {
    const auto &p=sample.points[n];const auto &o=sample.outputs[n];
    if(!(p.attribute&sample.moveMask)) continue;
    if((p.attribute&sample.disableMask) || !(o.flags&sample.outputEnableMask) ||
        !(o.flags&(sample.worldWriteMask|sample.localWriteMask))) {
      Log("[CLOTH-BONE-LOCAL] stage=output-flags-refused frame=%d particle=%d instance=%d attribute=%u flags=%u",sample.frame,n,o.id,unsigned(p.attribute),unsigned(o.flags));
      return ClothBoneLocalRefuse(slot,"local-move-output-disabled");
    }
    if((o.flags&sample.localWriteMask) && !(o.flags&sample.worldWriteMask)) {
      ClothContactLocalPoseError error{};
      if(!ClothContactLocalPoseMatches(o,weight,error)) {
        Log("[CLOTH-BONE-LOCAL] stage=output-pose-refused frame=%d particle=%d instance=%d inputKnown=%d outputWeight=%.9g rawDistanceSquared=%g expectedDistanceSquared=%g rotationDot=%g expectedPose=native-read-write-interpolation",
            sample.frame,n,o.id,int(o.localInputKnown),weight,error.rawDistanceSquared,error.distanceSquared,error.rotationDot);
        return ClothBoneLocalRefuse(slot,"local-completed-output-pose-mismatch");
      }
      ++localCount;maxRaw=(std::max)(maxRaw,error.rawDistanceSquared);maxError=(std::max)(maxError,error.distanceSquared);
    }
  }
  auto &trace=s_clothBoneSolver[slot];
  if(weight>0 && weight<1 && localCount && !trace.blendedOutputLogged) {
    trace.blendedOutputLogged=true;
    Log("[CLOTH-BONE-LOCAL] stage=blended-output-confirmed frame=%d team=%d localOutputs=%d outputWeight=%.9g maxRawDistanceM=%g maxExpectedErrorM=%g source=native-ReadTransform localToleranceM=0.0002 visualVerified=0",
        sample.frame,s_clothBoneSlots[slot].team[1],localCount,weight,std::sqrt(maxRaw),std::sqrt(maxError));
  }
  return true;
}
static bool ClothBoneLocalObserve(int slot,void *team,const ClothContactSample &sample) {
  auto &b=s_clothBoneSlots[slot];auto &l=b.local;
  if(!l.requested) return true;
  l.readbackIssue=nullptr;
  const auto input=ClothBoneLocalInputReadiness(slot,sample,GetTickCount64());
  if(input!=ClothBoneInputReadiness::Ready)return input==ClothBoneInputReadiness::Waiting;
  if(!ClothBoneSolverEdges(slot,team,sample)) return ClothBoneLocalRefuse(slot,"local-registered-edge-readback-mismatch");
  if(!ClothBoneLocalRadius(slot,sample)) return ClothBoneLocalRefuse(slot,"local-radius-readback-mismatch");
  if(!ClothBoneLocalOutputs(slot,sample)) return false;
  if(!ClothBoneLocalSurface(slot,team,sample)) return false;
  if(!ClothBoneNativeContactReadback(slot,team)) return ClothBoneLocalRefuse(slot,
      l.readbackIssue?l.readbackIssue:"native-cloth-contact-registration-or-partner-changed");
  if(!ClothBoneContactStartObserve(b,sample.frame))return ClothBoneLocalRefuse(slot,l.readbackIssue?l.readbackIssue:"paired-start-unconfirmed");
  if(l.contactLayered && l.contactStart.phase!=ClothBoneContactStart::Phase::Waiting &&
      l.contactStart.phase!=ClothBoneContactStart::Phase::Ready)return true;
  if(sample.simulateWeight*sample.lodWeight<=0) return true;
  if(sample.frame!=l.solverFrame) {l.solverFrame=sample.frame;if(l.solverFrames<2) ++l.solverFrames;}
  l.solverConfirmed=l.solverFrames>=2 && (!l.contactConfigured || l.contactConfirmed);
  if(l.bodyCreated && l.solverFrames<=2 && !l.published) Log("[CLOTH-BONE-BODY] stage=native-input-readback frame=%d team=%d collider=%d radius=%g enabled=1 transformInputMapped=1 completedState=1 contactVerified=0 visualVerified=0",
      sample.frame,b.team[1],l.bodyCollider.id.instance,ClothBodyContactRadius);
  if(l.fittedBodyCreated&&l.solverFrames<=2&&!l.published)Log("[CLOTH-BONE-FITTED-BODY] stage=native-input-readback frame=%d team=%d shapes=%zu enabled=1 transformInputMapped=1 completedState=1 contactVerified=0 visualVerified=0",sample.frame,b.team[1],l.fittedBody.size());
  if(l.side.created && l.solverFrames<=2 && !l.published) Log("[CLOTH-BONE-SIDE] stage=native-input-readback frame=%d team=%d count=%zu enabled=1 transformInputMapped=1 completedState=1 coatJobInputUnobserved=1 contactVerified=0 visualVerified=0",
      sample.frame,b.team[1],l.side.shapes.size());
  if(l.solverFrames<=2 && !l.published) Log("[CLOTH-BONE-LOCAL] stage=completed-output frame=%d edges=%zu endpointIdentity=1 colliderInputIdentity=1 localOutputReadback=1 frames=%u sourceStepUnknown=1",
      sample.frame,b.registeredEdges.size(),l.solverFrames);
  return true;
}
static bool ClothBoneSolverRenderInputsImpl(int slot,unsigned record,int frame) {
  if(!ClothOnMainThread() || slot<0 || slot>=s_clothBoneCount) return true;
  const auto &b=s_clothBoneSlots[slot];
  if(!ClothBoneSolverEligible(b)) return true;
  struct Input {
    int renderer,bone,id,parent;bool bindingParent=false;ClothInputPose pose{};
    char name[128]{},parentName[128]{};
  };
  struct Space {int renderer,id;ClothInputPose pose{};};
  std::vector<Input> points;std::vector<Space> spaces;
  const auto append=[&](int renderer,int bone,void *t,void *parent,int id,int parentId,
                        const char *name,const char *parentName,bool bindingParent) {
    for(size_t n=0;n<b.bones.size();++n)
      if(ClothBoneCandidate(b).bones[n].attribute&&b.bones[n].bone.id.instance==id)return true;
    if(std::any_of(points.begin(),points.end(),[&](const Input &v){return v.id==id;})) return true;
    if(points.size()>=512 || !t || !parent || CollisionParent(t)!=parent) return false;
    Input v{renderer,bone,id,parentId,bindingParent};
    strncpy_s(v.name,name,_TRUNCATE);strncpy_s(v.parentName,parentName,_TRUNCATE);
    if(!ClothInputPoseRead(t,v.pose)) return false;
    points.push_back(v);return true;
  };
  if(!b.profile || b.profile->rendererCount<1 || b.renderers.size()!=size_t(b.profile->rendererCount)) return false;
  for(int rendererIndex=0;rendererIndex<b.profile->rendererCount;++rendererIndex) {
    const auto &r=b.renderers[rendererIndex];const auto &a=b.profile->renderers[rendererIndex];
    const ClothBoneLocalMeshState *layer=nullptr;
    if(b.local.published) {
      for(const auto &candidate:b.local.layers) if(candidate.published && candidate.renderer==rendererIndex) layer=&candidate;
      if(!layer&&!(b.local.recipe&&b.profile->generatedLocal==b.local.recipe&&
          b.local.recipe->NativeSkinRetained()&&b.local.layers.empty()))continue;
    }
    auto renderer=ClothTarget(r.renderer);void *mesh=nullptr;
    if(!renderer) return false;
    if(!layer) {
      bool enabled=false,visible=false;
      if(!ClothValue(SurfaceMethod(il2cpp_object_get_class(renderer),"get_enabled","System.Boolean"),renderer,enabled) ||
          !ClothValue(SurfaceMethod(il2cpp_object_get_class(renderer),"get_isVisible","System.Boolean"),renderer,visible)) return false;
      if(!enabled || !visible) continue;
    }
    if(!renderer || r.bones.size()!=size_t(a.boneCount) || r.parents.size()!=r.bones.size() ||
        !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(renderer),"get_sharedMesh","UnityEngine.Mesh"),renderer,nullptr,mesh) ||
        mesh!=ClothTarget(layer?layer->mesh:r.mesh) ||
        !SurfaceRenderSameReferences(renderer,"get_bones","UnityEngine.Transform[]",layer?layer->rendererBones:r.bones)) return false;
    auto transform=CollisionTransform(renderer);int transformId=0;Space space{rendererIndex,0};
    if(!transform || !ClothAnchorUnderOwner(transform) || !ClothValue(s_clothUnity.instance,transform,transformId) ||
        !ClothInputPoseRead(transform,space.pose)) return false;
    space.id=transformId;spaces.push_back(space);
    if(layer) {
      const ClothBoneLocalMeshConfig *config=nullptr;
      for(size_t k=0;k<b.local.layers.size();++k)if(&b.local.layers[k]==layer)config=&b.local.recipe->meshes[k];
      if(!config)return false;
      for(int n=0;n<config->bindingCount;++n)if(config->bindingNativeIndices[n]<0) {
        const auto ref=ClothBoneLocalBindingRef(b,config->bindingNativeIndices[n]);
        const int index=-config->bindingNativeIndices[n]-1;
        if(!ref.handle||b.contactPartner<0||b.contactPartner>=s_clothBoneCount)return false;
        const auto &partner=s_clothBoneSlots[b.contactPartner];const auto &bone=partner.bones[index];
        const auto &asset=ClothBoneCandidate(partner).bones[index];
        if(!append(rendererIndex,config->sourceBones+n,ClothTarget(ref),ClothTarget(bone.parent),ref.id.instance,
            bone.parent.id.instance,asset.name,asset.parentName,false))return false;
      }
    }
    for(int n=0;n<a.boneCount;++n) {
      if(a.bones[n].cloth>=0) continue;
      const int id=r.bones[n].id.instance;
      auto bone=ClothTarget(r.bones[n]);auto parent=ClothTarget(r.parents[n]);
      if(!bone || !parent || CollisionParent(bone)!=parent ||
          !append(rendererIndex,n,bone,parent,id,r.parents[n].id.instance,a.bones[n].name,a.bones[n].parent,false)) return false;
      const int parentId=r.parents[n].id.instance;
      if(std::any_of(points.begin(),points.end(),[&](const Input &v){return v.id==parentId;}) ||
          std::any_of(b.bones.begin(),b.bones.end(),[&](const ClothBoneReference &v){return v.bone.id.instance==parentId;})) continue;
      auto grandparent=CollisionParent(parent);int grandparentId=0;char grandparentName[128]{};
      if(!grandparent || !ClothAnchorUnderOwner(parent) || !ClothValue(s_clothUnity.instance,grandparent,grandparentId)) return false;
      CollisionName(grandparent,grandparentName,sizeof(grandparentName));
      if(!grandparentName[0] || !strcmp(grandparentName,"unknown") ||
          !append(rendererIndex,n,parent,grandparent,parentId,grandparentId,a.bones[n].parent,grandparentName,true)) return false;
    }
  }
  if(!ClothBoneSolverEligible(b) || ClothFrame()!=frame) return false;
  Log("[CLOTH-BONE-RENDER-BEGIN] slot=%d record=%u frame=%d count=%zu readOnly=1 sourceStepUnknown=1 pointCapacity=512",slot,record,frame,points.size());
  for(const auto &v:points) {
    const auto &a=b.profile->renderers[v.renderer];
    std::ostringstream out;out<<std::setprecision(12);
    out<<"{\"slot\":"<<slot<<",\"record\":"<<record<<",\"frame\":"<<frame<<",\"renderer\":"<<CollisionJsonString(a.name)
       <<",\"rendererIndex\":"<<v.renderer<<",\"bone\":"<<v.bone<<",\"id\":"<<v.id<<",\"parentId\":"<<v.parent
       <<",\"source\":"<<CollisionJsonString(v.bindingParent?"binding-parent":"binding")
       <<",\"name\":"<<CollisionJsonString(v.name)<<",\"parentName\":"<<CollisionJsonString(v.parentName)<<",\"matrix\":";
    ClothInputJsonArray(out,v.pose.matrix,16);out<<'}';
    Log("[CLOTH-BONE-RENDER-DATA] %s",out.str().c_str());
  }
  for(const auto &v:spaces) {
    std::ostringstream out;out<<std::setprecision(12);
    out<<"{\"slot\":"<<slot<<",\"record\":"<<record<<",\"frame\":"<<frame<<",\"renderer\":"<<CollisionJsonString(b.profile->renderers[v.renderer].name)
       <<",\"rendererIndex\":"<<v.renderer<<",\"id\":"<<v.id<<",\"gpuWeightPolicyKnown\":false,\"matrix\":";
    ClothInputJsonArray(out,v.pose.matrix,16);out<<'}';
    Log("[CLOTH-BONE-RENDER-SPACE] %s",out.str().c_str());
  }
  Log("[CLOTH-BONE-RENDER-END] slot=%d record=%u frame=%d count=%zu",slot,record,frame,points.size());
  return true;
}
static void ClothBoneSolverRenderInputs(int slot,unsigned record,int frame) {
  bool ok=false;
  __try {ok=ClothBoneSolverRenderInputsImpl(slot,record,frame);}
  __except(EXCEPTION_EXECUTE_HANDLER) {ok=false;}
  if(!ok) Log("[CLOTH-BONE-RENDER-ISSUE] slot=%d record=%u frame=%d reason=body-binding-evidence-unavailable nativeSimulationRetained=1",slot,record,frame);
}
#include "cloth_bonecloth_contact_trace.h"
#include "cloth_bonecloth_response_trace.h"
#include "cloth_bonecloth_fit_trace.h"
static void ClothBoneSolverLogEdges(int slot,unsigned record,int frame,const std::vector<std::array<int,2>> &edges) {
  for(size_t first=0;first<edges.size();first+=32) {
    std::ostringstream out;out<<"{\"kind\":\"edges\",\"slot\":"<<slot<<",\"record\":"<<record
      <<",\"frame\":"<<frame<<",\"index\":"<<first<<",\"pairs\":[";
    const size_t end=(std::min)(first+32,edges.size());
    for(size_t n=first;n<end;++n){if(n!=first)out<<',';const auto &e=edges[n];out<<'['<<e[0]<<','<<e[1]<<']';}
    out<<"]}";Log("[CLOTH-BONE-SOLVER-DATA] %s",out.str().c_str());
  }
}
static void ClothBoneSolverLog(int slot,const ClothContactSample &p,void *team=nullptr) {
  auto &s=s_clothBoneSolver[slot];const auto &b=s_clothBoneSlots[slot];
  const unsigned record=++s.logged;
  const bool edgesKnown=team && ClothBoneSolverEdges(slot,team,p);
  Log("[CLOTH-BONE-SOLVER-BEGIN] slot=%d record=%u session=%llu generation=%llu instance=%d team=%d component=%s profile=%s frame=%d particles=%d colliders=%d outputKnown=%d simulateWeight=%.9g blendWeight=%.9g lodWeight=%.9g moveMask=%u disableMask=%u validMask=%u enableMask=%u worldWriteMask=%u localWriteMask=%u outputEnableMask=%u costMs=%.6g backend=%s owner=%p process=%p serialize=%p scene=%d command=%u edgesKnown=%d edges=%zu sourceFrame=unknown renderDepth=unmeasured",
      slot,record,(unsigned long long)s.binding.identity.session,(unsigned long long)s.binding.identity.generation,
      s.binding.identity.cloth,s.binding.identity.team,b.profile->component,ClothBoneCandidate(b).signature,p.frame,p.particles,p.colliders,
      int(p.outputKnown),p.simulateWeight,p.blendWeight,p.lodWeight,p.moveMask,p.disableMask,p.validMask,p.enableMask,
      p.worldWriteMask,p.localWriteMask,p.outputEnableMask,p.costMs,"MMD",
      reinterpret_cast<void*>(s.binding.identity.owner),reinterpret_cast<void*>(s.binding.identity.process),
      reinterpret_cast<void*>(s.binding.identity.serialize),s.binding.identity.scene,s.command,
      int(edgesKnown),edgesKnown?b.registeredEdges.size():size_t(0));
  const auto &pose=s_clothBonePoseTicket;
  const bool poseKnown=pose.owner==b.owner && pose.frame>=0 && pose.frame<=p.frame && p.frame-pose.frame<=1;
  Log("[CLOTH-BONE-SUBMISSION] slot=%d record=%u clothFrame=%d bodyFrame=%d lastSubmittedPlayhead=%.9g bodyStage=%s bodySubmissionIsSimulationTicket=0",
      slot,record,p.frame,poseKnown?pose.frame:-1,poseKnown?pose.playhead:NAN,poseKnown?pose.stage:"unknown");
  for(int n=0;n<p.colliders;++n) {
    const auto &c=p.shapes[n];std::ostringstream out;out<<std::setprecision(12);
    out<<"{\"kind\":\"collider\",\"slot\":"<<slot<<",\"record\":"<<record<<",\"index\":"<<n
       <<",\"id\":"<<c.id<<",\"transform\":"<<c.transform<<",\"type\":"<<CollisionJsonString(c.componentType)
       <<",\"flags\":"<<int(c.flags)<<",\"radius\":";
    ClothInputJsonArray(out,c.radius,2);out<<",\"old\":";ClothInputJsonArray(out,c.oldEndpoints,6);
    out<<",\"next\":";ClothInputJsonArray(out,c.nextEndpoints,6);out<<",\"center\":";ClothInputJsonArray(out,c.center,3);
    out<<",\"size\":";ClothInputJsonArray(out,c.size,3);out<<'}';
    Log("[CLOTH-BONE-SOLVER-DATA] %s",out.str().c_str());
  }
  for(int n=0;n<p.particles;++n) {
    const auto &v=p.points[n];const auto &o=p.outputs[n];std::ostringstream out;out<<std::setprecision(12);
    out<<"{\"kind\":\"particle\",\"slot\":"<<slot<<",\"record\":"<<record<<",\"index\":"<<n
       <<",\"proxy\":"<<v.proxy<<",\"particle\":"<<v.particle<<",\"attribute\":"<<int(v.attribute)<<",\"depth\":"<<CollisionNumber(v.depth)
       <<",\"friction\":"<<CollisionNumber(v.friction)<<",\"normal\":";ClothInputJsonArray(out,v.normal,3);
    out<<",\"next\":";ClothInputJsonArray(out,v.next,3);out<<",\"display\":";ClothInputJsonArray(out,v.display,3);
    if(p.outputKnown) {
      int asset=-1;for(size_t k=0;k<b.bones.size();++k) if(b.bones[k].bone.id.instance==o.id) asset=int(k);
      out<<",\"assetBone\":"<<asset<<",\"id\":"<<o.id<<",\"parentId\":"<<o.parentId
         <<",\"name\":"<<CollisionJsonString(o.name)<<",\"outputFlags\":"<<int(o.flags)<<",\"proxyPosition\":";
      ClothInputJsonArray(out,o.proxyPosition,3);out<<",\"last\":";ClothInputJsonArray(out,o.lastPosition,3);
      out<<",\"visibleMatrix\":";ClothInputJsonArray(out,o.visible.matrix,16);
      out<<",\"localPosition\":";ClothInputJsonArray(out,o.localPosition,3);
      out<<",\"lastLocalPosition\":";ClothInputJsonArray(out,o.lastLocalPosition,3);
      out<<",\"localRotation\":";ClothInputJsonArray(out,o.localRotation,4);
      out<<",\"lastLocalRotation\":";ClothInputJsonArray(out,o.lastLocalRotation,4);
      out<<",\"localInputKnown\":"<<(o.localInputKnown?"true":"false");
      if(o.localInputKnown) {
        out<<",\"inputLocalPosition\":";ClothInputJsonArray(out,o.inputLocalPosition,3);
        out<<",\"inputLocalRotation\":";ClothInputJsonArray(out,o.inputLocalRotation,4);
      }
    }
    out<<'}';Log("[CLOTH-BONE-SOLVER-DATA] %s",out.str().c_str());
  }
  if(edgesKnown)ClothBoneSolverLogEdges(slot,record,p.frame,b.registeredEdges);
  Log("[CLOTH-BONE-SOLVER-END] slot=%d record=%u frame=%d sourceStepUnknown=1",slot,record,p.frame);
  ClothBoneSolverRenderInputs(slot,record,p.frame);
  if(team) ClothBoneFitTrace(slot,record,team,p);
  ClothBonePairTrace(slot,record,p.frame);
  ClothBoneResponseTrace(slot,record,p.frame);
}
static bool ClothBoneDampingReadback(int slot) {
  auto &b=s_clothBoneSlots[slot];
  if(!b.ribbonDampingConfigured || b.ribbonDampingConfirmed)return true;
  void *manager=nullptr;ClothInputArray parameters{};float curve[16]{};
  if(!ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager)||
      !ClothInputArrayOpen(manager,"parameterArray","BeyondDynamicBone.ClothParameters",parameters))return false;
  auto box=ClothInputArrayBox(parameters,b.team[1]);
  if(!box||!ClothInputTeamField(box,"dampingCurveData","Unity.Mathematics.float4x4",curve))return false;
  for(int n=0;n<16;++n)
    if(!std::isfinite(curve[n])||fabsf(curve[n]-b.ribbonDampingCurve[n])>1e-6f)return false;
  b.ribbonDampingConfirmed=true;
  Log("[CLOTH-RIBBON] stage=native-confirmed component=%s strength=%g team=%d samples=16 visualVerified=0",
      b.profile->component,b.ribbonDamping,b.team[1]);
  return true;
}
static void ClothBoneSolverCapture(int slot,void *manager) {
  const auto &b=s_clothBoneSlots[slot];auto &s=s_clothBoneSolver[slot];
  if(!ClothBoneSolverEligible(b)) return;
  if(s.command!=b.command) { ClothBoneSolverClear(slot);s.command=b.command; }
  if(s.contact.failed) return;
  const auto now=GetTickCount64();const int frame=ClothFrame();
  ClothBoneSolverRefreshPause(s,now);
  const bool activating=b.local.requested && !b.local.published && now<b.modeDeadline;
  if(!ClothBoneSolverCaptureDue(s,frame,now,activating)) return;
  s.frame=frame;s.pollMs=now;
  LARGE_INTEGER begin{},end{},frequency{};QueryPerformanceFrequency(&frequency);QueryPerformanceCounter(&begin);
  uintptr_t access=0;void *getItem=nullptr,*team=nullptr;ClothInputSample input{};
  if(!ClothBoneSolverBind(slot,manager,access,getItem,team,input)) { ClothBoneSolverFail(slot,"current-owner-Team-collider-map-unconfirmed");return; }
  bool lodCulled=true;
  const bool lodKnown=team && ClothValue(ClothInputMethod(il2cpp_object_get_class(team),"get_IsLODCulled","System.Boolean"),
      static_cast<char *>(team)+16,lodCulled);
  if(!input.teamKnown || !lodKnown || input.culled || lodCulled || input.relative!=0) {
    const char *reason=!input.teamKnown || !lodKnown?"effective-Team-state-unavailable":input.culled?"culled-sample-skipped":
        lodCulled?"lod-culled-sample-skipped":"relative-coordinate-sample-skipped";
    if(strcmp(s.contact.issue,reason)) Log("[CLOTH-BONE-SOLVER-SKIP] component=%s frame=%d reason=%s",b.profile->component,frame,reason);
    strncpy_s(s.contact.issue,reason,_TRUNCATE);return;
  }
  if(!ClothBoneDampingReadback(slot)) {
    auto &mutableBone=s_clothBoneSlots[slot];mutableBone.stopRequested=mutableBone.failed=true;
    ClothBoneSolverFail(slot,"ribbon-damping-native-parameters-mismatch-restoring");return;
  }
  ClothContactSample sample{};
  if(!ClothContactRead(input,team,sample,s.binding,s.contact)) return;
  if(!s.contact.outputFailed) {
    sample.outputKnown=ClothContactOutputSafe(team,manager,&access,getItem,sample,s.binding) && ClothBoneSolverOutputIdentity(b,sample);
    if(!sample.outputKnown) { s.contact.outputFailed=true;Log("[CLOTH-BONE-SOLVER-OUTPUT] component=%s instance=%d frame=%d issue=effective-bone-output-map-unconfirmed particlesRetained=1",b.profile->component,b.bbc.id.instance,frame); }
  }
  if(!ClothBoneSolverEligible(b) || !ClothInputIdentity(true,s.binding)) { ClothBoneSolverFail(slot,"identity-changed-during-observation");return; }
  if(b.profile->nativeGraphCount&&(!s.captures||ClothBoneSolverPauseLogDue(s,now))) {
    const bool registered=ClothBoneSolverEdges(slot,team,sample);
    Log("[CLOTH-BONE-UNEQUAL-REGISTERED] component=%s frame=%d Team=%d known=%d edges=%zu completedBoundary=1 outputIdentity=%d visualContactUnconfirmed=1",
        b.profile->component,frame,b.team[1],int(registered),b.registeredEdges.size(),int(sample.outputKnown));
  }
  if(b.local.requested && !ClothBoneLocalObserve(slot,team,sample)) {
    auto &mutableBone=s_clothBoneSlots[slot];mutableBone.local.solverConfirmed=false;mutableBone.stopRequested=true;
    ClothBoneSolverLog(slot,sample,team);
    ClothBoneSolverFail(slot,mutableBone.local.readbackIssue?mutableBone.local.readbackIssue:
        "local-native-reference-or-surface-readback-mismatch-restoring");return;
  }
  if(b.local.requested && b.local.colliderInputPending) {
    strcpy_s(s.contact.issue,"awaiting-first-native-collider-workdata");return;
  }
  QueryPerformanceCounter(&end);sample.costMs=double(end.QuadPart-begin.QuadPart)*1000/frequency.QuadPart;
  s.contact.identity=s.binding.identity;s.contact.samples.Push(sample);++s.captures;
  strcpy_s(s.contact.issue,"completed-state-captured-source-step-unattributed");
  const bool pauseLog=ClothBoneSolverPauseLogDue(s,now);
  if(pauseLog || (!activating && ClothBoneSolverInitialLogDue(s))) {
    Log("[CLOTH-BONE-CHECKPOINT] component=%s frame=%d reason=%s pauseRequest=%u fullTextSuppressed=%d nativeSimulationRetained=1",
        b.profile->component,frame,pauseLog?"pause-request":"initial-ready",s.pauseSeen,int(s.fullTextSuppressed));
    s.logMs=now;ClothBoneSolverLog(slot,sample,team);
    if(pauseLog) ClothBoneSolverPauseLogged(s,now);
  }
  QueryPerformanceCounter(&end);
  ClothBoneSolverCost(slot,sample.costMs,double(end.QuadPart-begin.QuadPart)*1000/frequency.QuadPart);
  if(!activating && (!s.healthMs || now-s.healthMs>=5000)) {
    s.healthMs=now;
    Log("[CLOTH-BONE-SOLVER-HEALTH] component=%s session=%llu generation=%llu frame=%d captures=%u particles=%d outputKnown=%d published=%d pollIntervalMs=%u fullTextSuppressed=%d periodicFullLog=0 readbackMs=%g observerMs=%g windowMaxObserverMs=%g nativeSimulationRetained=1",
        b.profile->component,(unsigned long long)b.owner.session,(unsigned long long)b.owner.generation,
        frame,s.captures,sample.particles,int(sample.outputKnown),int(b.local.published),s.pollIntervalMs,int(s.fullTextSuppressed),
        s.lastReadbackMs,s.lastObserverMs,s.windowMaxObserverMs);
    s.windowMaxObserverMs=0;
  }
}
static bool ClothBoneResponseInputRead(int slot,void *manager,double &positionError,double &rotationDot) {
  auto &s=s_clothBoneSlots[slot];auto &l=s.local.response;auto bbc=ClothTarget(l.consumer);void *process=nullptr,*data=nullptr,*list=nullptr;
  if(!bbc||!ClothOwns(s.owner)||!CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",process)||process!=CollisionGc(l.process)||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data)||data!=CollisionGc(l.data)||
      !ClothField(process,"colliderList",CollisionListType,list)||!list||!ClothBoneTeamRegistered(process,l.team))return false;
  uintptr_t access=0;int length=0;void *get=nullptr,*tm=nullptr;
  if(!ClothBoneSolverAccess(manager,access,length,get)||!ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",tm))return false;
  ClothInputArray teams{},owners{},positions{},rotations{};
  if(!ClothInputArrayOpen(tm,"teamDataArray","BeyondDynamicBone.TeamManager.TeamData",teams)||
      !ClothInputArrayOpen(manager,"teamIdArray","System.Int16",owners)||
      !ClothInputArrayOpen(manager,"positionArray","Unity.Mathematics.double3",positions)||
      !ClothInputArrayOpen(manager,"rotationArray","Unity.Mathematics.quaternion",rotations))return false;
  auto team=ClothInputArrayBox(teams,l.team);int relative=-1;bool culled=true;ClothInputChunk chunk{};
  if(!team||!ClothInputTeamField(team,"useRelativeTransform","System.Int32",relative)||relative!=0||
      !ClothValue(ClothInputMethod(il2cpp_object_get_class(team),"get_IsCullingInvisible","System.Boolean"),(char*)team+16,culled)||culled||
      !ClothInputChunkRead(team,"colliderTransformChunk",length,chunk)||chunk.count!=CollisionCount(list)||chunk.count>32)return false;
  std::array<bool,3> found{};positionError=0;rotationDot=1;
  for(int n=0;n<chunk.count;++n){auto c=CollisionItem(list,n,"BeyondDynamicBone.ColliderComponent");if(!c)continue;int k=-1;
    for(int j=0;j<3;++j)if(c==ClothTarget(l.colliders[j]))k=j;
    if(k<0||found[k])return false;found[k]=true;
    int index=chunk.start+n;void *actual=nullptr,*args[]{&index};int16_t owner=0;double p[3]{},expected[4]{};float q[4]{};
    if(!ClothInvoke(get,&access,args,actual)||actual!=ClothTarget(s.bones[s.local.recipe->responses[k].frame.target].bone)||
        !ClothInputArrayValue(owners,index,"System.Int16",&owner,sizeof(owner))||owner!=l.team||
        !ClothInputArrayValue(positions,index,"Unity.Mathematics.double3",p,sizeof(p))||
        !ClothInputArrayValue(rotations,index,"Unity.Mathematics.quaternion",q,sizeof(q))||!eiem_cloth_response::Rotation(l.appliedWorld[k],expected))return false;
    double error=0,dot=0,norm=0;for(int j=0;j<3;++j){if(!std::isfinite(p[j]))return false;error+=(p[j]-l.appliedWorld[k].v[12+j])*(p[j]-l.appliedWorld[k].v[12+j]);}
    for(int j=0;j<4;++j){if(!std::isfinite(q[j]))return false;dot+=q[j]*expected[j];norm+=q[j]*q[j];}
    if(std::abs(norm-1)>.001)return false;positionError=(std::max)(positionError,std::sqrt(error));rotationDot=(std::min)(rotationDot,std::abs(dot));
  }
  return found[0]&&found[1]&&found[2];
}
static void ClothBoneResponseInput(int slot,void *manager) {
  auto &s=s_clothBoneSlots[slot];auto &l=s.local.response;const int frame=ClothFrame();
  if(!l.prepared||l.disabled||!l.updates||!s.pending||s.stopRequested||s.tx.cancelled||!s.local.published||
      s.tx.phase!=eiem_cloth_rebuild::Phase::Active||l.frame!=frame||l.inputFrame==frame||(l.updates>3&&l.updates%600))return;
  l.inputFrame=frame;double error=NAN,dot=NAN;bool known=false;
  __try {known=ClothBoneResponseInputRead(slot,manager,error,dot);}
  __except(EXCEPTION_EXECUTE_HANDLER){known=false;}
  Log("[CLOTH-BONE-RESPONSE] stage=native-input-readback consumer=%s session=%llu generation=%llu command=%u frame=%d innerTeam=%d outerTeam=%d known=%d maxPositionError=%g minRotationDot=%g mappedInputConfirmed=%d observationOnly=1 contactAndVisualConfirmed=0",
      s.local.recipe->responseConsumer,s.owner.session,s.owner.generation,s.command,frame,s.team[1],l.team,int(known),error,dot,int(known&&error<.001&&dot>.9999));
}
#include "cloth_ribbon_motion_trace.h"
static void ClothBoneSolverCompleted(void *manager) {
  if(!ClothOnMainThread() || !s_cloth.active || !s_clothInputHooks || s_clothInputUpdateDepth!=1) return;
  const auto &pose=s_clothBonePoseTicket;
  ClothShoulderEvidenceCompleted(s_clothBonePauseEvidence.load(std::memory_order_acquire),pose.owner==s_cloth.owner?pose.frame:-1,
      pose.owner==s_cloth.owner?pose.playhead:NAN,pose.owner==s_cloth.owner?pose.stage:"unknown");
  for(int slot=0;slot<s_clothBoneCount;++slot) {
    ClothBoneResponseInput(slot,manager);
    __try { ClothBoneSolverCapture(slot,manager); }
    __except(EXCEPTION_EXECUTE_HANDLER) { ClothBoneSolverFail(slot,"completed-state-observer-fault"); }
  }
  if(s_clothVerboseDiagnostics.load())ClothRibbonMotionTrace();
}
static std::string ClothBoneSolverJson() {
  std::ostringstream out;out<<"{\"schema\":1,\"readOnly\":true,\"components\":[";bool first=true;
  if(ClothOnMainThread()) for(int slot=0;slot<s_clothBoneCount;++slot) {
    const auto &b=s_clothBoneSlots[slot];const auto &s=s_clothBoneSolver[slot];
    if(!ClothBoneSolverEligible(b) || b.command!=s.command) continue;
    if(!first) out<<',';first=false;
    out<<"{\"component\":"<<CollisionJsonString(b.profile->component)<<",\"profile\":"<<CollisionJsonString(ClothBoneCandidate(b).signature)
       <<",\"captures\":"<<s.captures<<",\"loggedRecords\":"<<s.logged
       <<",\"pollIntervalMs\":"<<s.pollIntervalMs<<",\"fullTextSuppressed\":"<<s.fullTextSuppressed
       <<",\"fullTextIntervalMs\":0,\"fullTextPolicy\":\"initial-pause-or-failure\""
       <<",\"pauseCheckpoints\":"<<s.pauseRecords<<",\"pausePending\":"<<s.pauseRemaining
       <<",\"lastReadbackAndChecksMs\":"<<CollisionNumber(s.lastReadbackMs)
       <<",\"lastObserverIncludingLogMs\":"<<CollisionNumber(s.lastObserverMs)
       <<",\"trace\":"<<ClothContactJson(s.binding,s.contact)<<'}';
  }
  return out.str()+"]}";
}
