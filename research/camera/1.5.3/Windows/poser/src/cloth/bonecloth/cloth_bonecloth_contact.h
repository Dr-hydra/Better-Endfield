#pragma once
#include "../collision/cloth_contact_job_state.h"
static bool ClothBoneContactTeam(int team,void *expected,void *&box) {
  void *manager=nullptr;ClothInputArray teams{};box=nullptr;
  return ClothBoneTeamRegistered(expected,team) &&
      ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager) &&
      ClothInputArrayOpen(manager,"teamDataArray","BeyondDynamicBone.TeamManager.TeamData",teams) &&
      (box=ClothInputArrayBox(teams,team))!=nullptr;
}
static bool ClothBoneContactParents(void *box,std::vector<int> &parents) {
  constexpr const char *type="Unity.Collections.FixedList32Bytes<System.Int32>";
  auto f=box?CollisionFieldInfo(il2cpp_object_get_class(box),"syncParentTeamId",type):nullptr;
  auto cls=f?il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;
  uint32_t align=0;unsigned char bytes[32]{};
  if(!cls || il2cpp_class_value_size(cls,&align)!=32 || !ClothInputTeamField(box,"syncParentTeamId",type,bytes)) return false;
  const int length=ClothValueOffset(cls,"length","System.UInt16",32,2);
  const int buffer=ClothValueOffset(cls,"buffer","Unity.Collections.FixedBytes30",32,30);
  if(length<0 || buffer<0 || length+2>buffer || buffer+30>32) return false;
  static_assert(sizeof(int)==4,"native Int32 list requires 32-bit int");
  const int payload=(buffer+int(sizeof(int))-1)&~(int(sizeof(int))-1);
  const int capacity=(int(sizeof(bytes))-payload)/int(sizeof(int));
  uint16_t count=0;memcpy(&count,bytes+length,2);if(count>capacity) return false;parents.clear();
  for(unsigned n=0;n<count;++n) {
    int id=0;memcpy(&id,bytes+payload+n*sizeof(int),sizeof(id));
    if(id<=0 || std::find(parents.begin(),parents.end(),id)!=parents.end()) return false;parents.push_back(id);
  }
  return true;
}
static bool ClothBoneContactFlag(void *cls,const char *name,int &index) {
  auto f=cls?CollisionFieldInfo(cls,name,"System.Int32"):nullptr;
  if(!f || !il2cpp_field_get_flags || !il2cpp_field_static_get_value || (il2cpp_field_get_flags(f)&0x50)!=0x50) return false;
  il2cpp_field_static_get_value(f,&index);return index>=0 && index<64;
}
static bool ClothBoneContactParametersValid(const ClothBoneContactParameters &value) {
  if(!std::isfinite(value.mass) || value.mass<0 || value.mass>1) return false;
  for(float v:value.thickness) if(!std::isfinite(v) || v<=0) return false;
  return true;
}
static bool ClothBoneContactMaterialUnchanged(const ClothBoneContactParameters &original,const ClothBoneContactParameters &effective) {
  if(!ClothBoneContactParametersValid(original) || !ClothBoneContactParametersValid(effective) ||
      fabsf(original.mass-effective.mass)>1e-6f) return false;
  for(int n=0;n<16;++n) if(fabsf(original.thickness[n]-effective.thickness[n])>1e-6f) return false;
  return true;
}
static bool ClothBoneContactParameterRead(void *box,ClothBoneContactParameters &value) {
  constexpr const char *type="BeyondDynamicBone.SelfCollisionConstraint.SelfCollisionConstraintParams";
  auto f=box?CollisionFieldInfo(il2cpp_object_get_class(box),"selfCollisionConstraint",type):nullptr;
  auto cls=f?il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;uint32_t align=0;
  if(!cls || il2cpp_class_value_size(cls,&align)!=sizeof(value) ||
      ClothValueOffset(cls,"selfMode","BeyondDynamicBone.SelfCollisionConstraint.SelfCollisionMode",sizeof(value),4)!=offsetof(ClothBoneContactParameters,selfMode) ||
      ClothValueOffset(cls,"surfaceThicknessCurveData","Unity.Mathematics.float4x4",sizeof(value),64)!=offsetof(ClothBoneContactParameters,thickness) ||
      ClothValueOffset(cls,"syncMode","BeyondDynamicBone.SelfCollisionConstraint.SelfCollisionMode",sizeof(value),4)!=offsetof(ClothBoneContactParameters,syncMode) ||
      ClothValueOffset(cls,"clothMass","System.Single",sizeof(value),4)!=offsetof(ClothBoneContactParameters,mass) ||
      !ClothInputTeamField(box,"selfCollisionConstraint",type,value)) return false;
  return ClothBoneContactParametersValid(value);
}
static bool ClothBoneContactParametersFor(int team,ClothBoneContactParameters &value) {
  void *manager=nullptr;ClothInputArray parameters{};
  return ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager) &&
      ClothInputArrayOpen(manager,"parameterArray","BeyondDynamicBone.ClothParameters",parameters) &&
      ClothBoneContactParameterRead(ClothInputArrayBox(parameters,team),value);
}
static bool ClothBoneContactMode(void *data,const char *field,int &value,int &none,int &full) {
  constexpr const char *type="BeyondDynamicBone.SelfCollisionConstraint.SelfCollisionMode";
  auto f=data?CollisionFieldInfo(il2cpp_object_get_class(data),field,type):nullptr;
  auto cls=f?il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;
  return cls && CollisionEnumValue(cls,"None",none) && CollisionEnumValue(cls,"FullMesh",full) && none!=full &&
      ClothField(data,field,type,value);
}
static bool ClothBoneLayeredPair(const ClothBoneRuntime &s) {
  return s.contactPartner>=0 && s.contactPartner<s_clothBoneCount &&
      (s_clothBoneSlots[s.contactPartner].supportCreated||ClothBoneRibbonPair(s_clothBoneSlots[s.contactPartner],s)||ClothBoneGeneratedPair(s_clothBoneSlots[s.contactPartner],s)) && ClothBonePair(s_clothBoneSlots[s.contactPartner],s);
}
static bool ClothBoneResponseContact(const ClothBoneRuntime &s) {
  const auto *r=s.local.requested?s.local.recipe:nullptr;
  return s.profile&&eiem_cloth_asset::SourceSeraphPanel(*s.profile)&&r&&r->resampledPanel&&
      r->responsePointCount==5&&r->responsePoints&&r->responseFaceCount>0&&r->responseFaces&&
      r->contactProducer&&r->responseConsumer&&!strcmp(r->contactProducer,r->responseConsumer);
}
static ClothBoneContactParameters ClothBoneContactExpected(const ClothBoneLocalState &l) {
  auto expected=l.contactOriginal;
  if(l.contactLayered)expected.mass=.5f;
  if(l.contactResponse)expected.mass=1.f;
  if(l.contactEnvelope&&l.recipe&&l.recipe->radiusCurve)
    for(int n=0;n<16;++n)expected.thickness[n]=l.recipe->radiusCurve[n]*l.recipe->layerContactScale;
  return expected;
}
static bool ClothBoneContactEnvelopeRequested(const ClothBoneRuntime &s) {
  return s.local.requested&&s.local.recipe&&s.local.recipe->layerContactScale!=0;
}
static bool ClothBoneContactEnvelopeIdentity(const ClothBoneRuntime &s) {
  return s.profile&&s.local.recipe==s.profile->generatedLocal&&ClothBoneLayeredPair(s)&&
      s_clothBoneSlots[s.contactPartner].profile&&
      ClothGeneratedContactEnvelope(*s_clothBoneSlots[s.contactPartner].profile,*s.profile);
}
static bool ClothBoneContactEnvelopeConfigure(ClothBoneRuntime &s,void *copy,void *source,const ClothBoneContactParameters &original) {
  auto &l=s.local;if(!ClothBoneContactEnvelopeRequested(s))return !l.contactEnvelope;
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1||!ClothOwns(s.owner)||s.stopRequested||
      !ClothBoneContactEnvelopeIdentity(s)||l.contactEnvelope||!copy||!source||copy==source||
      !ClothBoneContactParametersValid(original))return false;
  constexpr const char *type="BeyondDynamicBone.CurveSerializeData";
  auto data=CollisionGc(s.data);auto candidate=CollisionGc(s.candidateData);
  void *radius=nullptr,*thickness=nullptr,*curve=nullptr,*keys=nullptr,*afterKeys=nullptr,*check=nullptr;
  float value=0,oldThickness=0,after=0;bool use=false,oldUse=false,afterUse=false;
  if(!data||!candidate||data==candidate||
      !ClothField(data,"selfCollisionConstraint","BeyondDynamicBone.SelfCollisionConstraint.SerializeData",check)||check!=source||
      !ClothField(candidate,"selfCollisionConstraint","BeyondDynamicBone.SelfCollisionConstraint.SerializeData",check)||check!=copy||
      !ClothField(data,"radius",type,radius)||!radius||
      !ClothField(source,"surfaceThickness",type,thickness)||!thickness||
      !ClothField(radius,"value","System.Single",value)||!std::isfinite(value)||value<=0||value>.1f||
      !ClothField(radius,"useCurve","System.Boolean",use)||!ClothField(radius,"curve","UnityEngine.AnimationCurve",keys)||
      !ClothField(thickness,"value","System.Single",oldThickness)||oldThickness!=.005f||
      !ClothField(thickness,"useCurve","System.Boolean",oldUse)||oldUse)return false;
  for(float v:original.thickness)if(fabsf(v-oldThickness)>1e-6f)return false;
  auto method=SurfaceMethod(il2cpp_object_get_class(radius),"Clone",type);
  if(!method||!ClothInvoke(method,radius,nullptr,curve)||!curve||curve==radius||curve==thickness||
      !SurfaceReference(copy,"surfaceThickness",type,curve)||
      !SurfaceScalar(curve,"value","System.Single",value*l.recipe->layerContactScale)||
      !ClothField(copy,"surfaceThickness",type,check)||check!=curve||
      !ClothField(radius,"value","System.Single",after)||after!=value||
      !ClothField(radius,"useCurve","System.Boolean",afterUse)||afterUse!=use||
      !ClothField(radius,"curve","UnityEngine.AnimationCurve",afterKeys)||afterKeys!=keys||
      !ClothField(source,"surfaceThickness",type,check)||check!=thickness||
      !ClothField(thickness,"value","System.Single",after)||after!=oldThickness||
      !ClothField(thickness,"useCurve","System.Boolean",afterUse)||afterUse!=oldUse)return false;
  void *box=nullptr;ClothBoneContactParameters actual{},unchanged{};
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(candidate),"GetClothParameters","BeyondDynamicBone.ClothParameters"),candidate,nullptr,box)||
      !ClothBoneContactParameterRead(box,actual)||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(data),"GetClothParameters","BeyondDynamicBone.ClothParameters"),data,nullptr,box)||
      !ClothBoneContactParameterRead(box,unchanged)||!ClothBoneContactMaterialUnchanged(original,unchanged))return false;
  for(int n=0;n<16;++n)if(fabsf(actual.thickness[n]-l.recipe->radiusCurve[n]*l.recipe->layerContactScale)>1e-6f)return false;
  l.contactOriginal=original;l.contactEnvelope=true;
  Log("[CLOTH-BONE-CONTACT-ENVELOPE] stage=private-configured generation=%llu command=%u component=%s sourceThickness=%g candidateEnds=%g/%g sourceRequired=%g scale=%g samples=%zu capped=%d clearanceLimit=%g clearanceGap=%g clearanceKind=%d clearanceChecks=%zu beforeClearancePreloaded=%zu naturalProxyClearance=bounded bodyRadiusUnchanged=1 sourceCurveUntouched=1 nativeTeamReadback=pending visualVerified=0",
      s.owner.generation,s.command,s.profile->component,oldThickness,actual.thickness[0],actual.thickness[15],
      l.recipe->layerContactRequired,l.recipe->layerContactScale,l.recipe->layerContactSamples,int(l.recipe->layerContactCapped),
      l.recipe->layerContactClearanceLimit,l.recipe->layerContactClearanceGap,l.recipe->layerContactClearanceKind,
      l.recipe->layerContactClearanceChecks,l.recipe->layerContactPreloaded);
  return true;
}
static bool ClothBoneContactEnvelopeMatches(ClothBoneRuntime &s,int slot,void *box) {
  if(!ClothOnMainThread()||slot<0||slot>2)return false;
  auto &l=s.local;if(!ClothBoneContactEnvelopeRequested(s))return !l.contactEnvelope;
  if(!l.contactEnvelope)return slot!=1;
  if(slot==1&&!ClothBoneContactEnvelopeIdentity(s))return false;
  ClothBoneContactParameters actual{};const auto expected=slot==1?ClothBoneContactExpected(l):l.contactOriginal;
  if(!ClothBoneContactParameterRead(box,actual)||!ClothBoneContactMaterialUnchanged(expected,actual)||
      (slot!=1&&(actual.selfMode!=expected.selfMode||actual.syncMode!=expected.syncMode)))return false;
  if(!l.contactEnvelopeReadback[slot]) {
    l.contactEnvelopeReadback[slot]=true;
    Log("[CLOTH-BONE-CONTACT-ENVELOPE] stage=%s generation=%llu command=%u team=%d thicknessEnds=%g/%g nativeEffective=1 visualVerified=0",
        slot==1?"candidate-Team-confirmed":"source-Team-confirmed",s.owner.generation,s.command,s.team[slot],actual.thickness[0],actual.thickness[15]);
  }
  return true;
}
static bool ClothBoneContactMassConfigure(ClothBoneRuntime &s,void *copy,void *source,const ClothBoneContactParameters &original) {
  if(!ClothOnMainThread() || !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 || !ClothOwns(s.owner) || s.stopRequested)return false;
  const bool layered=ClothBoneLayeredPair(s);
  float before=0,after=0,actual=0;
  if(!copy || !source || copy==source || !ClothBoneContactParametersValid(original) ||
      !ClothField(source,"clothMass","System.Single",before) || !std::isfinite(before) || fabsf(before-original.mass)>1e-6f)return false;
  const bool response=ClothBoneResponseContact(s);
  const float expected=response?1.f:layered?.5f:before;
  if((layered||response) && !SurfaceScalar(copy,"clothMass","System.Single",expected))return false;
  if(!ClothField(copy,"clothMass","System.Single",actual) || !std::isfinite(actual) || fabsf(actual-expected)>1e-6f ||
      !ClothField(source,"clothMass","System.Single",after) || after!=before)return false;
  s.local.contactLayered=layered;s.local.contactResponse=response;return true;
}
static bool ClothBoneContactPreflightReject(const char *reason) {
  const auto &s=ClothBoneState();
  Log("[CLOTH-BONE-CONTACT] stage=preflight-refused check=%s consumer=%s sourceTeam=%d producer=%s originalUntouched=1 renderPublished=0",
      reason,s.profile?s.profile->component:"unknown",s.team[0],s.local.recipe->contactProducer?s.local.recipe->contactProducer:"none");
  return ClothBoneReject(reason);
}
static bool ClothBoneContactPrimitives(void *team,std::array<int,3> &counts) {
  const char *fields[]{"selfPointChunk","selfEdgeChunk","selfTriangleChunk"};
  for(int k=0;k<3;++k) {
    ClothInputChunk chunk{};
    if(!ClothInputTeamField(team,fields[k],"BeyondDynamicBone.DataChunk",chunk)||chunk.count<0||
        chunk.count>1000000||(chunk.count && (chunk.start<0||chunk.start>INT32_MAX-chunk.count)))return false;
    counts[k]=chunk.count;
  }
  return true;
}
using ClothContactScheduleFn=ClothInputJobHandle *(__fastcall *)(ClothInputJobHandle *,
    const eiem_cloth_contact_job::Job *,const ClothInputJobHandle *,void *);
static bool (*s_clothContactJobInstaller)()=nullptr;
static char s_clothContactInstallIssue[128]{};
struct ClothContactJobAdapter {
  bool installed=false;
  size_t lengthOffset=0;
  void *callsites[2]{};
  ClothContactScheduleFn original[2]{};
} static s_clothContactJobs;
static ClothInputJobHandle *ClothContactSchedule(int kind,ClothInputJobHandle *result,
    const eiem_cloth_contact_job::Job *job,const ClothInputJobHandle *dependency,void *method,void *caller) {
  auto original=s_clothContactJobs.original[kind];
  // Scheduling safety is independent of the editor's maintenance lock/depth.
  // ClothInputNativeUpdate must still run the native update when the GUI owns
  // that lock; forwarding a null indexCount on that path crashes a Burst job
  // and leaves Unity waiting for it. These leases are written only on the
  // native game thread; UI/HTTP requests use atomic mailboxes, never these slots.
  // No Unity calls here, including during shutdown while native jobs drain.
  if(!s_clothContactJobs.installed || !ClothOnNativeThread() ||
      caller!=s_clothContactJobs.callsites[kind]) return original(result,job,dependency,method);
  ClothBoneRuntime *matched=nullptr;bool any=false;
  for(auto &actor:s_ClothActorBones.values)if(actor)for(auto &s:actor->slots) {
    auto &l=s.local;
    if(!s.pending || !s.lease || !l.contactConfigured || l.contactReleased || !l.contactManager)continue;
    any=true;if(job && l.contactListHeaders[kind]==job->list.data)matched=&s;
  }
  if(!any)return original(result,job,dependency,method);
  eiem_cloth_contact_job::Job copy{};
  const auto patch=!matched || !job ? eiem_cloth_contact_job::Patch::Invalid :
      eiem_cloth_contact_job::BindCount(*job,matched->local.contactListHeaders[kind],s_clothContactJobs.lengthOffset,copy);
  if(patch==eiem_cloth_contact_job::Patch::Unchanged) return original(result,job,dependency,method);
  if(patch==eiem_cloth_contact_job::Patch::Repaired) {
    auto &s=*matched;auto &l=s.local;
    if(s_clothInputUpdateDepth!=1 && ++l.contactJobDeferredRepairs[kind]==1)
      Log("[CLOTH-BONE-CONTACT-JOB] stage=counter-bound-maintenance-deferred kind=%d nativeList=%p editorLockRequired=0 nativeJobsProtected=1",
          kind,(void*)job->list.data);
    if(++l.contactJobRepairs[kind]==1)
      Log("[CLOTH-BONE-CONTACT-JOB] stage=counter-bound kind=%s generation=%llu team=%d nativeList=%p liveLengthAlias=1 dependencyUnchanged=1 nativeSolver=1",
          kind?"PointTriangle":"EdgeEdge",(unsigned long long)s.owner.generation,s.team[1],(void*)job->list.data);
    return original(result,&copy,dependency,method);
  }
  bool report=false;
  for(auto &actor:s_ClothActorBones.values)if(actor)for(auto &s:actor->slots) {
    if(!s.pending || !s.lease || !s.local.contactConfigured || s.local.contactReleased) continue;
    if(!s.failure[0]) {strncpy_s(s.failure,"native-contact-job-input-changed-restoring",_TRUNCATE);report=true;}
    s.failed=true;s.stopRequested=true;
  }
  if(report)Log("[CLOTH-BONE-CONTACT-JOB] stage=input-refused kind=%d restoreQueued=1 unsafeJobNotScheduled=1",kind);
  if(result && dependency)*result=*dependency;
  return result;
}
static ClothInputJobHandle *__fastcall ClothContactScheduleEdge(ClothInputJobHandle *result,
    const eiem_cloth_contact_job::Job *job,const ClothInputJobHandle *dependency,void *method) {
  return ClothContactSchedule(0,result,job,dependency,method,_ReturnAddress());
}
static ClothInputJobHandle *__fastcall ClothContactSchedulePoint(ClothInputJobHandle *result,
    const eiem_cloth_contact_job::Job *job,const ClothInputJobHandle *dependency,void *method) {
  return ClothContactSchedule(1,result,job,dependency,method,_ReturnAddress());
}
static bool ClothBoneNativeContactPreflight() {
  const auto &l=ClothBoneState().local;
  if(!l.requested || !l.recipe->contactProducer) return true;
  if(ClothOnMainThread() && s_clothSurfaceAtBoundary && s_clothInputUpdateDepth==1 &&
      s_clothContactJobInstaller && s_clothContactJobInstaller()) return ClothBoneLayerPreflight();
  return ClothBoneContactPreflightReject(s_clothContactInstallIssue[0] ? s_clothContactInstallIssue :
      "native-contact-counter-adapter-unavailable-original-unchanged");
}
static bool ClothBoneNativeContactConfigure(void *data,void *original) {
  if(!ClothBoneNativeContactPreflight()) return false;
  auto &s=ClothBoneState();auto &l=s.local;const char *name=l.recipe->contactProducer;
  if(!l.requested || !name) return true;
  if(!ClothOnMainThread() || !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 ||
      !ClothOwns(s.owner) || s.stopRequested || !data || !original || data==original || l.contactConfigured)
    return ClothBoneContactPreflightReject("native-contact-owner-boundary-or-private-data-unconfirmed");
  int producer=-1;
  for(size_t n=0;n<s.nativeProducers.size();++n) {
    char actual[128]{};CollisionName(ClothTarget(s.nativeProducers[n].bbc),actual,sizeof(actual));
    if(!strcmp(actual,name)) {if(producer>=0)return ClothBoneContactPreflightReject("native-contact-producer-ambiguous");producer=int(n);}
  }
  if(producer<0 || !ClothBoneNativeProducerIdentity(s.nativeProducers[producer]))
    return ClothBoneContactPreflightReject("native-contact-producer-identity-unconfirmed");
  const auto &p=s.nativeProducers[producer];void *partner=ClothTarget(p.bbc),*source=nullptr,*copy=nullptr,*oldPartner=nullptr,*otherPartner=nullptr,*team=nullptr;
  constexpr const char *type="BeyondDynamicBone.SelfCollisionConstraint.SerializeData";
  int mode=-1,none=-2,full=-3,sync=-1,otherSync=-1;
  ClothBoneContactParameters existing{};
  std::vector<int> parents;
  if(!ClothBoneContactTeam(s.team[0],CollisionGc(s.process[0]),team))
    return ClothBoneContactPreflightReject("native-contact-source-Team-unreadable");
  if(!ClothInputTeamField(team,"syncTeamId","System.Int32",otherSync) ||
      !ClothBoneContactParents(team,parents))
    return ClothBoneContactPreflightReject("native-contact-source-sync-layout-unconfirmed");
  if(otherSync!=0 || !parents.empty())
    return ClothBoneContactPreflightReject("native-contact-source-existing-sync-relation");
  if(!ClothField(original,"selfCollisionConstraint",type,source) || !source ||
      !ClothBoneContactMode(source,"selfMode",mode,none,full) || mode!=none ||
      !ClothBoneContactMode(source,"syncMode",sync,none,full) || sync!=none ||
      !ClothField(source,"syncPartner","BeyondDynamicBone.BeyondBoneCloth",oldPartner) || oldPartner)
    return ClothBoneContactPreflightReject("native-contact-source-serialized-policy-unconfirmed");
  if(!ClothBoneContactParametersFor(s.team[0],existing))
    return ClothBoneContactPreflightReject("native-contact-source-effective-parameters-unreadable-or-invalid");
  if(existing.selfMode!=none || existing.syncMode!=none)
    return ClothBoneContactPreflightReject("native-contact-source-effective-policy-mismatch");
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(partner),"get_SyncPartnerCloth","BeyondDynamicBone.BeyondBoneCloth"),partner,nullptr,otherPartner) || otherPartner)
    return ClothBoneContactPreflightReject("native-contact-producer-sync-partner-unconfirmed");
  if(!ClothBoneContactTeam(p.team,CollisionGc(p.process),team))
    return ClothBoneContactPreflightReject("native-contact-producer-Team-unreadable");
  if(!ClothInputTeamField(team,"syncTeamId","System.Int32",otherSync) || !ClothBoneContactParents(team,parents))
    return ClothBoneContactPreflightReject("native-contact-producer-sync-layout-unconfirmed");
  if(otherSync!=0 || !parents.empty())
    return ClothBoneContactPreflightReject("native-contact-producer-existing-sync-relation");
  if(!SurfaceMethod(il2cpp_object_get_class(partner),"SetParameterChange","System.Void"))
    return ClothBoneContactPreflightReject("native-contact-producer-retire-notification-unavailable");
  std::array<int,3> primitives{};
  if(!ClothBoneContactPrimitives(team,primitives)||primitives!=std::array<int,3>{})
    return ClothBoneContactPreflightReject("native-contact-producer-existing-primitives-unconfirmed");
  void *simulation=nullptr,*manager=nullptr;
  if(!ClothContactManager("get_Simulation","BeyondDynamicBone.SimulationManager",simulation) ||
      !ClothField(simulation,"selfCollisionConstraint","BeyondDynamicBone.SelfCollisionConstraint",manager) || !manager)
    return ClothBoneContactPreflightReject("native-contact-manager-unavailable");
  const char *fields[]{"edgeEdgeContactList","pointTriangleContactList"};
  const char *types[]{"Unity.Collections.NativeList<BeyondDynamicBone.SelfCollisionConstraint.EdgeEdgeContact>",
                     "Unity.Collections.NativeList<BeyondDynamicBone.SelfCollisionConstraint.PointTriangleContact>"};
  for(int n=0;n<2;++n) {
    eiem_cloth_contact_job::Container list{};
    if(!ClothField(manager,fields[n],types[n],list) || !list.data || (list.data&7))
      return ClothBoneContactPreflightReject("native-contact-list-header-unavailable");
    l.contactListHeaders[n]=list.data;
  }
  if(!(l.contactManager=ClothBoneHold(manager))) return ClothBoneContactPreflightReject("native-contact-manager-hold-failed");
  if(!SurfaceCloneField(data,original,"selfCollisionConstraint",type) ||
      !ClothField(data,"selfCollisionConstraint",type,copy) || !copy || copy==source ||
      !SurfaceReference(copy,"syncPartner","BeyondDynamicBone.BeyondBoneCloth",partner) ||
      !SurfaceEnum(copy,"syncMode","BeyondDynamicBone.SelfCollisionConstraint.SelfCollisionMode","FullMesh") ||
      !ClothBoneContactMassConfigure(s,copy,source,existing)||
      !ClothBoneContactEnvelopeConfigure(s,copy,source,existing))
    return ClothBoneContactPreflightReject("native-contact-private-clone-or-write-unconfirmed");
  void *check=nullptr;int after=-1;
  if(!ClothField(source,"syncPartner","BeyondDynamicBone.BeyondBoneCloth",check) || check!=oldPartner ||
      !ClothBoneContactMode(source,"syncMode",after,none,full) || after!=sync)
    return ClothBoneContactPreflightReject("native-contact-original-policy-changed");
  l.contactProducer=producer;l.contactOriginal=existing;l.contactConfigured=true;
  Log("[CLOTH-BONE-CONTACT] stage=configured consumer=%s producer=%s partnerTeam=%d partnerProcess=%p sourceSyncUntouched=1 privateSync=FullMesh thickness=%s sourceClothMass=%g candidateClothMass=%g pairedStartRequired=%d nativeRegistrationPending=1",
      s.profile->component,name,p.team,CollisionGc(p.process),l.contactEnvelope?"source-skin-envelope":"original",existing.mass,ClothBoneContactExpected(l).mass,int(l.contactLayered));return true;
}
static bool ClothBoneNativeContactReadback(int slot,void *team) {
  if(slot<0 || slot>=s_clothBoneCount) return false;
  auto &s=s_clothBoneSlots[slot];auto &l=s.local;
  const auto refuse=[&](const char *reason) {
    l.readbackIssue=reason;
    Log("[CLOTH-BONE-CONTACT] stage=readback-refused check=%s slot=%d consumer=%s Team=%d partnerSlot=%d contextSlot=%d renderPublished=%d",
        reason,slot,s.profile?s.profile->component:"unknown",s.team[1],s.contactPartner,s_clothBoneContext,int(l.published));
    return false;
  };
  if(!l.contactConfigured) return (!l.recipe || !l.recipe->contactProducer) || refuse("native-contact-not-configured");
  if(l.contactProducer<0 || size_t(l.contactProducer)>=s.nativeProducers.size()) return refuse("native-contact-producer-not-retained");
  const auto &p=s.nativeProducers[l.contactProducer];void *other=nullptr;
  if(!ClothBoneNativeProducerIdentity(s,p)) return refuse("native-contact-producer-identity-changed");
  if(!team || !ClothBoneContactTeam(p.team,CollisionGc(p.process),other)) return refuse("native-contact-Team-unreadable");
  int sync=0,otherSync=0;std::vector<int> parents;
  ClothBoneContactParameters effective{};
  ClothInputChunk points{},triangles{},otherPoints{},otherTriangles{};
  uint64_t flags=0,otherFlags=0;
  if(!ClothBoneContactParametersFor(s.team[1],effective) ||
      !ClothInputTeamField(team,"syncTeamId","System.Int32",sync) ||
      !ClothInputTeamField(other,"syncTeamId","System.Int32",otherSync) || !ClothBoneContactParents(other,parents) ||
      !ClothInputTeamField(team,"selfPointChunk","BeyondDynamicBone.DataChunk",points) ||
      !ClothInputTeamField(team,"selfTriangleChunk","BeyondDynamicBone.DataChunk",triangles) ||
      !ClothInputTeamField(other,"selfPointChunk","BeyondDynamicBone.DataChunk",otherPoints) ||
      !ClothInputTeamField(other,"selfTriangleChunk","BeyondDynamicBone.DataChunk",otherTriangles) ||
      !ClothInputTeamField(team,"flag","Unity.Collections.BitField64",flags) ||
      !ClothInputTeamField(other,"flag","Unity.Collections.BitField64",otherFlags)) return refuse("native-contact-effective-state-unreadable");
  int sourceFlag=-1,targetFlag=-1;auto cls=SurfaceClass("BeyondDynamicBone","TeamManager");
  if(!ClothBoneContactFlag(cls,"Flag_Sync_TrianglePoint",sourceFlag) ||
      !ClothBoneContactFlag(cls,"Flag_PSync_PointTriangle",targetFlag)) return refuse("native-contact-triangle-point-flags-unresolved");
  bool reciprocalFaces=true;
  if(s.contactPartner>=0) {
    if(s.contactPartner>=s_clothBoneCount)return refuse("native-contact-partner-slot-invalid");
    const auto &partner=s_clothBoneSlots[s.contactPartner];int sourcePoint=-1,targetTriangle=-1,sourceEdge=-1,targetEdge=-1;
    if(!ClothBonePair(partner,s)||!partner.graph[1]||!partner.teamModeConfirmed||partner.team[1]!=p.team)
      return refuse("native-contact-partner-graph-or-Team-changed");
    if(!ClothBoneNativeLayerPartner(partner)&&!ClothBonePartnerVolumePolicyMatches(partner))return refuse("native-contact-partner-volume-policy-unconfirmed");
    if(!ClothBoneContactFlag(cls,"Flag_Sync_PointTriangle",sourcePoint)||!ClothBoneContactFlag(cls,"Flag_PSync_TrianglePoint",targetTriangle)||
        !ClothBoneContactFlag(cls,"Flag_Sync_EdgeEdge",sourceEdge)||!ClothBoneContactFlag(cls,"Flag_PSync_EdgeEdge",targetEdge))
      return refuse("native-contact-reciprocal-flags-unresolved");
    reciprocalFaces=otherTriangles.count==ClothBoneCandidate(partner).FaceCount()&&otherPoints.count==ClothBoneCandidate(partner).EffectiveCount()&&
        points.count==ClothBoneCandidate(s).EffectiveCount()&&(flags&(uint64_t(1)<<sourcePoint))&&(otherFlags&(uint64_t(1)<<targetTriangle))&&
        (flags&(uint64_t(1)<<sourceEdge))&&(otherFlags&(uint64_t(1)<<targetEdge));
  }
  void *configuration=nullptr;int configured=-1,none=-2,full=-3;
  if(!ClothField(CollisionGc(s.candidateData),"selfCollisionConstraint","BeyondDynamicBone.SelfCollisionConstraint.SerializeData",configuration) ||
      !ClothBoneContactMode(configuration,"syncMode",configured,none,full) || configured!=full) return refuse("native-contact-private-policy-changed");
  if(otherSync!=0 || std::any_of(parents.begin(),parents.end(),[&](int id){return id!=s.team[1];}))
    return refuse("native-contact-foreign-sync-relation");
  if(l.contactLayered && !ClothBoneLayeredPair(s))return refuse("native-contact-layered-pair-changed");
  if(l.contactResponse && !ClothBoneResponseContact(s))return refuse("native-contact-ribbon-pair-changed");
  if(l.contactEnvelope!=ClothBoneContactEnvelopeRequested(s)||(l.contactEnvelope&&!ClothBoneContactEnvelopeIdentity(s)))
    return refuse("native-contact-skin-envelope-pair-changed");
  if(!ClothBoneContactMaterialUnchanged(ClothBoneContactExpected(l),effective)) return refuse("native-contact-material-changed");
  const bool ready=reciprocalFaces && sync==p.team && parents.size()==1 && parents[0]==s.team[1] &&
      effective.selfMode==none && effective.syncMode==full &&
      triangles.count>0 && triangles.count<=ClothBoneMaxFaces && otherPoints.count>0 && otherPoints.count<=ClothBoneMaxParticles &&
      (flags&(uint64_t(1)<<sourceFlag)) && (otherFlags&(uint64_t(1)<<targetFlag));
  if(!ready) {
    if(!l.contactPendingLogged) {
      l.contactPendingLogged=true;
      Log("[CLOTH-BONE-CONTACT] stage=registration-pending consumerTeam=%d producerTeam=%d actualSyncTeam=%d effectiveSync=%d expectedSync=%d reciprocalParents=%zu triangles=%d producerPoints=%d producerTriangles=%d points=%d reciprocalFaces=%d flags=%llu/%llu syncBits=%d/%d rendererUnchanged=1",
          s.team[1],p.team,sync,effective.syncMode,full,parents.size(),triangles.count,otherPoints.count,otherTriangles.count,points.count,int(reciprocalFaces),
          (unsigned long long)flags,(unsigned long long)otherFlags,sourceFlag,targetFlag);
    }
    if(l.contactConfirmed || GetTickCount64()>=s.modeDeadline) return refuse(l.contactConfirmed?"native-contact-registration-lost":"native-contact-registration-timeout");
    l.contactConfirmed=false;return true;
  }
  if(!l.contactConfirmed) Log("[CLOTH-BONE-CONTACT] stage=native-registration-readback consumerTeam=%d producerTeam=%d triangles=%d producerPoints=%d producerTriangles=%d reciprocalFacesRequired=%d reciprocalParent=1 nativeFlags=1 effectiveSync=FullMesh thicknessEnds=%g/%g clothMass=%g replacedCoarseVolumes=%zu partnerReplacedCoarseVolumes=%zu surfaceContactAndVisualUnconfirmed=1",
      s.team[1],p.team,triangles.count,otherPoints.count,otherTriangles.count,int(s.contactPartner>=0),effective.thickness[0],effective.thickness[15],effective.mass,l.contactColliderOmissions.size(),
      s.contactPartner>=0?s_clothBoneSlots[s.contactPartner].partnerColliderOmissions.size():size_t(0));
  const auto now=GetTickCount64();
  if(l.contactCompletedNotes<24 && now>=l.contactNextNote) {
    int contacts[2]{-1,-1};void *manager=CollisionGc(l.contactManager);
    const char *fields[]{"edgeEdgeContactList","pointTriangleContactList"};
    const char *types[]{"Unity.Collections.NativeList<BeyondDynamicBone.SelfCollisionConstraint.EdgeEdgeContact>",
                       "Unity.Collections.NativeList<BeyondDynamicBone.SelfCollisionConstraint.PointTriangleContact>"};
    for(int kind=0;kind<2;++kind) {
      auto field=manager?CollisionFieldInfo(il2cpp_object_get_class(manager),fields[kind],types[kind]):nullptr;
      auto cls=field?il2cpp_class_from_type(il2cpp_field_get_type(field)):nullptr;
      eiem_cloth_contact_job::Container list{};
      if(cls && ClothField(manager,fields[kind],types[kind],list) && list.data==l.contactListHeaders[kind])
        ClothValue(SurfaceMethod(cls,"get_Length","System.Int32"),&list,contacts[kind]);
    }
    Log("[CLOTH-BONE-CONTACT-JOB] stage=completed-readback consumerTeam=%d producerTeam=%d counterJobs=%u/%u nativeListCounts=%d/%d countsScope=shared-native-manager renderedContactUnconfirmed=1",
        s.team[1],p.team,l.contactJobRepairs[0],l.contactJobRepairs[1],contacts[0],contacts[1]);
    ++l.contactCompletedNotes;l.contactNextNote=now+2000;
  }
  l.contactConfirmed=true;return true;
}
static bool ClothBoneContactResetABI() {
  return ClothBoneMethodFingerprint("BeyondBoneCloth","ResetCloth","System.Void","System.Boolean",261,0x6dbc5c84e05cdeb7ULL);
}
static bool (*s_clothPairResetABI)()=ClothBoneContactResetABI;
static bool ClothBoneContactPairIdentity(ClothBoneRuntime &s,eiem_cloth_rebuild::Identity (&identity)[2],uint64_t (&flags)[2]) {
  if(!ClothOnMainThread() || !ClothBoneLayeredPair(s) || !ClothOwns(s.owner) || !s.local.contactConfirmed)return false;
  auto &p=s_clothBoneSlots[s.contactPartner];ClothBoneRuntime *pair[]{&s,&p};
  for(int n=0;n<2;++n) {
    auto &b=*pair[n];void *bbc=ClothTarget(b.bbc),*process=nullptr,*data=nullptr,*data2=nullptr,*team=nullptr;
    if(!b.pending || !b.lease || b.stopRequested || b.failed || b.tx.cancelled ||
        b.tx.phase!=eiem_cloth_rebuild::Phase::Active || !b.graph[1] || !b.teamModeConfirmed || !bbc ||
        !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_Process","BeyondDynamicBone.ClothProcess"),bbc,nullptr,process) || process!=CollisionGc(b.process[1]) ||
        !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data) || data!=CollisionGc(b.candidateData) ||
        !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"GetSerializeData2","BeyondDynamicBone.ClothSerializeData2"),bbc,nullptr,data2) || data2!=CollisionGc(b.candidateData2) ||
        !ClothBoneContactTeam(b.team[1],process,team) || !ClothInputTeamField(team,"flag","Unity.Collections.BitField64",flags[n]))return false;
    identity[n]={b.owner,uint64_t(uintptr_t(bbc)),uint64_t(uintptr_t(process)),uint64_t(uintptr_t(data)),uint64_t(uintptr_t(data2))};
  }
  return true;
}
static bool ClothBoneContactStartFailure(ClothBoneRuntime &s,const char *reason) {
  s.local.contactStart.phase=ClothBoneContactStart::Phase::Failed;s.local.readbackIssue=reason;
  if(!s.failure[0])strncpy_s(s.failure,reason,_TRUNCATE);
  Log("[CLOTH-BONE-PAIR-START] stage=failed reason=%s generation=%llu command=%u retry=0 originalRestoreRequired=1",
      reason,(unsigned long long)s.owner.generation,s.command);return false;
}
static bool ClothBoneContactStartRequest(ClothBoneRuntime &s) {
  using Phase=ClothBoneContactStart::Phase;auto &l=s.local;auto &start=l.contactStart;
  if(!l.contactLayered)return true;
  if(!ClothOnMainThread() || !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1)return false;
  if(start.phase!=Phase::Waiting)return start.phase!=Phase::Failed;
  if(!l.solverConfirmed || !l.contactConfirmed)return true;
  eiem_cloth_rebuild::Identity before[2]{};uint64_t flags[2]{};int reset=-1;
  auto managerClass=SurfaceClass("BeyondDynamicBone","TeamManager");
  if(!s_clothPairResetABI || !s_clothPairResetABI() || !ClothBoneContactFlag(managerClass,"Flag_Reset",reset) ||
      !ClothBoneContactPairIdentity(s,before,flags))return ClothBoneContactStartFailure(s,"paired-start-ABI-or-identity-unconfirmed");
  auto &p=s_clothBoneSlots[s.contactPartner];
  ClothBoneContactParameters outer{},inner{};
  if(!ClothBoneContactParametersFor(p.team[1],outer) || !ClothBoneContactParametersFor(s.team[1],inner) ||
      !ClothBoneContactMaterialUnchanged(ClothBoneContactExpected(l),inner) || outer.mass>=inner.mass)
    return ClothBoneContactStartFailure(s,"paired-start-contact-material-unconfirmed");
  if(!ClothBoneLayerStart(s))return ClothBoneContactStartFailure(s,"paired-start-layer-order-unconfirmed");
  const int frame=ClothFrame();
  if(!start.Reserve(before[0],before[1],s.team[1],p.team[1],frame,GetTickCount64(),s.command))return false;
  l.solverConfirmed=false;l.solverFrames=0;l.solverFrame=-1;
  const bool keepPose=false;bool sent[2]{};ClothBoneRuntime *pair[]{&s,&p};
  for(int n=0;n<2;++n) {
    eiem_cloth_rebuild::Identity current[2]{};uint64_t state[2]{};
    if(!ClothBoneContactPairIdentity(s,current,state) || !start.Matches(current[0],current[1],s.team[1],p.team[1],s.command))break;
    void *unused=nullptr,*args[]{const_cast<bool*>(&keepPose)};auto bbc=ClothTarget(pair[n]->bbc);
    sent[n]=ClothInvoke(ClothMethod(il2cpp_object_get_class(bbc),"ResetCloth","System.Void","System.Boolean"),bbc,args,unused);
    Log("[CLOTH-BONE-PAIR-START] stage=reset-command component=%s frame=%d Team=%d sent=%d keepPose=0 oneShot=1 bodyClockUnchanged=1",
        pair[n]->profile->component,frame,pair[n]->team[1],int(sent[n]));
    if(!sent[n])break;
  }
  eiem_cloth_rebuild::Identity after[2]{};uint64_t state[2]{};
  const bool same=ClothBoneContactPairIdentity(s,after,state) && start.Matches(after[0],after[1],s.team[1],p.team[1],s.command);
  if(!start.Acknowledge(sent[0]&&same&&(state[0]&(uint64_t(1)<<reset)),sent[1]&&same&&(state[1]&(uint64_t(1)<<reset))))
    return ClothBoneContactStartFailure(s,"paired-start-reset-command-or-readback-failed");
  Log("[CLOTH-BONE-PAIR-START] stage=request-readback frame=%d innerTeam=%d outerTeam=%d innerMass=%g outerMass=%g bothResetFlags=1 renderPublished=0 oneShot=1",
      frame,s.team[1],p.team[1],inner.mass,outer.mass);return true;
}
static bool ClothBoneContactStartObserve(ClothBoneRuntime &s,int frame) {
  using Phase=ClothBoneContactStart::Phase;auto &l=s.local;auto &start=l.contactStart;
  if(!l.contactLayered)return true;
  if(start.phase==Phase::Waiting || start.phase==Phase::Ready)return true;
  if(start.phase!=Phase::Requested)return false;
  eiem_cloth_rebuild::Identity current[2]{};uint64_t flags[2]{};int reset=-1;
  if(!ClothBoneContactPairIdentity(s,current,flags) ||
      !start.Matches(current[0],current[1],s.team[1],s_clothBoneSlots[s.contactPartner].team[1],s.command) ||
      !ClothBoneContactFlag(SurfaceClass("BeyondDynamicBone","TeamManager"),"Flag_Reset",reset))
    return ClothBoneContactStartFailure(s,"paired-start-identity-changed");
  if(start.Observe(bool(flags[0]&(uint64_t(1)<<reset)),bool(flags[1]&(uint64_t(1)<<reset)),frame,GetTickCount64()))
    Log("[CLOTH-BONE-PAIR-START] stage=consumed frame=%d requestFrame=%d innerTeam=%d outerTeam=%d bothNativeResetFlagsCleared=1 geometrySeparationVerified=0 visualVerified=0",
        frame,start.frame,start.team[0],start.team[1]);
  if(start.phase==Phase::Failed)return ClothBoneContactStartFailure(s,"paired-start-reset-consumption-timeout");
  return true;
}
static bool ClothBoneNativeContactReleaseReady() {
  auto &s=ClothBoneState();auto &l=s.local;
  if(!l.contactConfigured || l.contactReleased) return true;
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1)return false;
  if(s.team[1]<=0){l.contactReleased=true;return true;}
  if(l.contactProducer<0 || size_t(l.contactProducer)>=s.nativeProducers.size()) return false;
  const auto &p=s.nativeProducers[l.contactProducer];void *manager=nullptr,*partnerTeam=nullptr;bool exists=false;
  if(!ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager)) return false;
  void *box=nullptr,*args[]{const_cast<int*>(&p.team)};
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(manager),"ContainsTeamData","System.Boolean","System.Int32"),manager,args,box) ||
      !ClothInputCopyBox(box,"System.Boolean",&exists,sizeof(exists))) return false;
  if(exists && !SurfaceUnregistered(CollisionGc(p.process),p.team)) {
    std::vector<int> parents;
    if(!ClothBoneContactTeam(p.team,CollisionGc(p.process),partnerTeam) || !ClothBoneContactParents(partnerTeam,parents) ||
        std::find(parents.begin(),parents.end(),s.team[1])!=parents.end()) return false;
    std::array<int,3> counts{};
    if(!ClothBoneContactPrimitives(partnerTeam,counts))return false;
    if(counts!=std::array<int,3>{}) {
      int sync=0;void *bbc=ClothTarget(p.bbc),*process=nullptr,*data=nullptr,*roots=nullptr,*unused=nullptr;
      if(!parents.empty()||!ClothInputTeamField(partnerTeam,"syncTeamId","System.Int32",sync)||sync!=0||!bbc||
          !CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",process)||process!=CollisionGc(p.process)||
          !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data)||data!=CollisionGc(p.data)||
          !CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots)||roots!=CollisionGc(p.roots))return false;
      if(!l.contactRetireNotified) {
        if(l.contactRetireAttempts>=3)return false;
        ++l.contactRetireAttempts;
        if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"SetParameterChange","System.Void"),bbc,nullptr,unused)) {
          Log("[CLOTH-BONE-CONTACT] stage=partner-retire-notify-failed producerTeam=%d attempt=%u counterLeaseRetained=1",p.team,l.contactRetireAttempts);return false;
        }
        l.contactRetireNotified=true;l.contactRetireFrame=ClothFrame();
        Log("[CLOTH-BONE-CONTACT] stage=partner-retire-requested candidateTeam=%d producerTeam=%d primitiveCounts=%d/%d/%d frame=%d publicParameterRefresh=1 counterLeaseRetained=1",
            s.team[1],p.team,counts[0],counts[1],counts[2],l.contactRetireFrame);
      } else if(l.contactRetireNotes<2 && ClothFrame()>l.contactRetireFrame+120*int(l.contactRetireNotes+1)) {
        ++l.contactRetireNotes;
        Log("[CLOTH-BONE-CONTACT] stage=partner-retire-pending producerTeam=%d primitiveCounts=%d/%d/%d refreshRepeated=0 counterLeaseRetained=1",p.team,counts[0],counts[1],counts[2]);
      }
      return false;
    }
    if(l.contactRetireNotified && ClothFrame()<=l.contactRetireFrame)return false;
  }
  l.contactReleased=true;
  Log("[CLOTH-BONE-CONTACT] stage=retired candidateTeam=%d producerTeam=%d incomingReferenceRemoved=1 producerPrimitivesRetiredOrTeamGone=1 publicParameterRefresh=%d producerConfigurationWrites=0 counterJobs=%u/%u",s.team[1],p.team,int(l.contactRetireNotified),l.contactJobRepairs[0],l.contactJobRepairs[1]);
  return true;
}
