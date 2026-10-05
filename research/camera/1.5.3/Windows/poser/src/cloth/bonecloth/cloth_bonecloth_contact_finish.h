#pragma once
using ClothFinishEdgeFn=ClothInputJobHandle *(__fastcall*)(ClothInputJobHandle *,eiem_cloth_finish::EdgeJob *,int *,
    const eiem_cloth_finish::Ref *,int,const ClothInputJobHandle *,void *);
using ClothFinishReduceFn=ClothInputJobHandle *(__fastcall*)(ClothInputJobHandle *,eiem_cloth_finish::ReduceJob *,int *,
    const eiem_cloth_finish::Ref *,int,const ClothInputJobHandle *,void *);
static struct ClothFinishHooks {
  ClothInputValidFn distance=nullptr;ClothFinishEdgeFn edge=nullptr;ClothFinishReduceFn reduce=nullptr;
  void *firstSite=nullptr,*secondSite=nullptr,*edgeSite=nullptr,*reduceSite=nullptr;
  void *edgeMethod=nullptr,*reduceMethod=nullptr;
  eiem_cloth_finish::EdgeJob edgeInput{};eiem_cloth_finish::ReduceJob reduceInput{};
  int edgeBatch=0,reduceBatch=0;
  bool installed=false;
  uint64_t submissions=0;int reportFrame=-1000;
} s_clothFinishHooks;
static bool (*s_clothFinishInstaller)()=nullptr;
static bool (*s_clothFinishSignature)(void *,bool)=nullptr;
static char s_clothFinishIssue[128]{};
static eiem_cloth_finish::Handle ClothFinishHandle(ClothInputJobHandle h){return {h.a,h.b};}
static void ClothFinishInputIssue(const char *kind) {
  auto &h=s_clothFinishHooks;if(ClothFrame()-h.reportFrame<120)return;h.reportFrame=ClothFrame();
  Log("[CLOTH-CONTACT-FINISH] stage=producer-input-unavailable frame=%d kind=%s reason=%s baselineRetained=1",
      ClothFrame(),kind,s_clothFinishIssue[0]?s_clothFinishIssue:"array-identity-counter-or-step-chain-mismatch");
}
static bool ClothFinishOwned() {
  if(!ClothOnMainThread()||s_clothInputUpdateDepth!=1||!s_clothInputHooks||
      !s_clothFinishHooks.installed||!s_clothFinishView.Ready())return false;
  const auto &p=s_clothFinishView.Identity().owner;
  for(const auto &s:s_clothBoneSlots)if(ClothBoneDisplayEligible(s)&&s.owner.session==p.session&&
      s.owner.generation==p.generation&&s.command==p.command&&s.team[1]==p.team&&
      uintptr_t(CollisionGc(s.process[1]))==p.process&&uintptr_t(CollisionGc(s.candidateData))==p.data)return true;
  s_clothFinishView.Revoke();s_clothFinishChain.Clear();return false;
}
static bool ClothFinishArray(void *object,const char *name,const char *element,eiem_cloth_finish::Array &out) {
  char type[192]{};_snprintf_s(type,_TRUNCATE,"Unity.Collections.NativeArray<%s>",element);
  return object&&ClothField(object,name,type,out)&&eiem_cloth_finish::Buffer(out,196608);
}
static bool ClothFinishProcessing(void *simulation,const char *field,eiem_cloth_finish::Array &buffer,eiem_cloth_finish::Ref &counter) {
  void *list=nullptr;
  return ClothField(simulation,field,"BeyondDynamicBone.ExProcessingList<System.Int32>",list)&&list&&
      ClothFinishArray(list,"Buffer","System.Int32",buffer)&&
      ClothField(list,"Counter","Unity.Collections.NativeReference<System.Int32>",counter)&&counter.data;
}
static bool ClothBoneFinishPrepare() {
  using namespace eiem_cloth_finish;
  if(!s_clothDisplayView.Ready()||!s_clothFinishInstaller||!s_clothFinishInstaller())return false;
  Policy p{};p.owner=s_clothDisplayView.Identity();auto &e=p.edge;auto &r=p.reduce;
  void *team=nullptr,*mesh=nullptr,*simulation=nullptr,*colliders=nullptr,*constraint=nullptr,*distance=nullptr,*box=nullptr;
  ClothInputArray teams{};ClothInputChunk chunk{};int empty=-1;
  if(!ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",team)||
      !ClothContactManager("get_VMesh","BeyondDynamicBone.VirtualMeshManager",mesh)||
      !ClothContactManager("get_Simulation","BeyondDynamicBone.SimulationManager",simulation)||
      !ClothContactManager("get_Collider","BeyondDynamicBone.ColliderManager",colliders)||
      !ClothField(simulation,"colliderCollisionConstraint","BeyondDynamicBone.ColliderCollisionConstraint",constraint)||!constraint||
      !ClothField(simulation,"distanceConstraint","BeyondDynamicBone.DistanceConstraint",distance)||!distance||
      !ClothInputArrayOpen(team,"teamDataArray","BeyondDynamicBone.TeamManager.TeamData",teams)||
      !(box=ClothInputArrayBox(teams,0))||!ClothInputTeamField(box,"colliderCount","System.Int32",empty)||empty!=0||
      !(box=ClothInputArrayBox(teams,p.owner.team))||
      !ClothDisplayArray(mesh,"edgeTeamIdArray","System.Int16",e.teamIds)||
      !ClothDisplayArray(mesh,"edges","Unity.Mathematics.int2",e.edges)||
      !ClothInputChunkRead(box,"proxyEdgeChunk",e.edges.length,chunk,ClothBoneMaxEdges))return false;
  p.edgeStart=chunk.start;p.edgeCount=chunk.count;p.distance=uintptr_t(distance);p.emptyTeamZero=true;
  e.teams=p.owner.teams;e.attributes=p.owner.attributes;
  if(!ClothDisplayArray(team,"parameterArray","BeyondDynamicBone.ClothParameters",e.parameters)||
      !ClothDisplayArray(mesh,"vertexDepths","System.Single",e.depths)||
      !ClothDisplayArray(simulation,"nextPosArray","Unity.Mathematics.double3",e.next)||
      !ClothDisplayArray(simulation,"velocityPosArray","Unity.Mathematics.double3",e.velocity)||
      !ClothDisplayArray(simulation,"frictionArray","System.Single",e.friction)||
      !ClothDisplayArray(simulation,"collisionNormalArray","Unity.Mathematics.float3",e.normal)||
      !ClothDisplayArray(colliders,"flagArray","BeyondDynamicBone.ExBitFlag8",e.flags)||
      !ClothDisplayArray(colliders,"workDataArray","BeyondDynamicBone.ColliderManager.WorkData",e.colliders)||
      !ClothFinishArray(simulation,"countArray","System.Int32",e.count)||
      !ClothFinishArray(simulation,"sumArray","System.Int32",e.sum)||
      !ClothFinishArray(constraint,"tempFrictionArray","System.Int32",e.tempFriction)||
      !ClothFinishArray(constraint,"tempNormalArray","System.Int32",e.tempNormal)||
      !ClothFinishProcessing(simulation,"processingStepEdgeCollision",e.steps,e.counter)||
      !ClothFinishProcessing(simulation,"processingStepParticle",r.steps,r.counter))return false;
  void *config=nullptr,*list=nullptr,*modeField=nullptr;ClothInputArray parameters{};
  int point=-1,edge=-1,mode=-2,colliderCount=0;ClothInputChunk colliderChunk{};
  if(!CollisionList((void*)p.owner.data,config,list)||
      !CollisionModeMetadata(il2cpp_object_get_class(config),modeField,point,edge)||
      !ClothInputArrayOpen(team,"parameterArray","BeyondDynamicBone.ClothParameters",parameters)||
      !CollisionParameterMode(ClothInputArrayBox(parameters,p.owner.team),mode)||mode!=edge||
      !ClothInputTeamField(box,"colliderCount","System.Int32",colliderCount)||colliderCount<=0||
      !ClothInputChunkRead(box,"colliderChunk",e.flags.length,colliderChunk,128)||colliderCount>colliderChunk.count)return false;
  r.next=e.next;r.velocity=e.velocity;r.friction=e.friction;r.normal=e.normal;
  r.count=e.count;r.sum=e.sum;r.tempFriction=e.tempFriction;r.tempNormal=e.tempNormal;
  return s_clothFinishView.Publish(p);
}
static void ClothBoneFinishRefresh() {
  if(!s_clothDisplayView.Ready())return;
  if(!ClothBoneFinishPrepare()&&ClothFrame()-s_clothFinishHooks.reportFrame>=120) {
    s_clothFinishHooks.reportFrame=ClothFrame();
    Log("[CLOTH-CONTACT-FINISH] stage=input-unavailable frame=%d reason=%s baselineRetained=1",
        ClothFrame(),s_clothFinishIssue[0]?s_clothFinishIssue:"native-arrays-or-owner-unconfirmed");
  }
}
static void ClothFinishCaptureEdge(const eiem_cloth_finish::EdgeJob *job,int *count,const eiem_cloth_finish::Ref *ref,
    int batch,ClothInputJobHandle input,ClothInputJobHandle output,void *method,void *caller) {
  using namespace eiem_cloth_finish;
  if(caller!=s_clothFinishHooks.edgeSite||!ClothFinishOwned())return;
  const auto &expected=s_clothFinishView.Identity().edge;
  if(!job||!ref||!method||!s_clothFinishSignature||
      (method!=s_clothFinishHooks.edgeMethod&&!s_clothFinishSignature(method,false))||
      uintptr_t(count)!=ref->data||!Same(*ref,expected.counter)||batch<1||batch>256||
      memcmp(job,&expected,sizeof(*job))||!s_clothFinishChain.Edge(ClothFinishHandle(input),ClothFinishHandle(output))) {
    s_clothFinishChain.Clear();ClothFinishInputIssue("Edge");return;
  }
  s_clothFinishHooks.edgeInput=*job;s_clothFinishHooks.edgeMethod=method;s_clothFinishHooks.edgeBatch=batch;
}
static void ClothFinishCaptureReduce(const eiem_cloth_finish::ReduceJob *job,int *count,const eiem_cloth_finish::Ref *ref,
    int batch,ClothInputJobHandle input,ClothInputJobHandle output,void *method,void *caller) {
  using namespace eiem_cloth_finish;
  if(caller!=s_clothFinishHooks.reduceSite||!ClothFinishOwned())return;
  const auto &expected=s_clothFinishView.Identity().reduce;
  if(!job||!ref||!method||!s_clothFinishSignature||
      (method!=s_clothFinishHooks.reduceMethod&&!s_clothFinishSignature(method,true))||
      uintptr_t(count)!=ref->data||!Same(*ref,expected.counter)||batch<1||batch>256||
      memcmp(job,&expected,sizeof(*job))||!s_clothFinishChain.Reduce(ClothFinishHandle(input),ClothFinishHandle(output))) {
    s_clothFinishChain.Clear();ClothFinishInputIssue("Reduce");return;
  }
  s_clothFinishHooks.reduceInput=*job;s_clothFinishHooks.reduceMethod=method;s_clothFinishHooks.reduceBatch=batch;
}
static void ClothFinishCaptureProtected(bool reduce,const void *job,int *count,const eiem_cloth_finish::Ref *ref,
    int batch,ClothInputJobHandle input,ClothInputJobHandle output,void *method,void *caller) {
  __try {
    if(reduce)ClothFinishCaptureReduce(static_cast<const eiem_cloth_finish::ReduceJob*>(job),count,ref,batch,input,output,method,caller);
    else ClothFinishCaptureEdge(static_cast<const eiem_cloth_finish::EdgeJob*>(job),count,ref,batch,input,output,method,caller);
  } __except(EXCEPTION_EXECUTE_HANDLER){s_clothFinishView.Revoke();s_clothFinishChain.Clear();}
}
static void ClothFinishCaptureActors(bool reduce,const void *job,int *count,const eiem_cloth_finish::Ref *ref,
    int batch,ClothInputJobHandle input,ClothInputJobHandle output,void *method,void *caller) {
  if(!ClothOnMainThread()||s_clothInputUpdateDepth!=1)return;
  for(unsigned actor=0;actor<ClothActorCount;++actor) {
    const auto &view=s_clothFinishViews.values[actor];if(!view||!view->Ready())continue;
    ClothActorScope scope(actor);ClothFinishCaptureProtected(reduce,job,count,ref,batch,input,output,method,caller);
  }
}
static ClothInputJobHandle *__fastcall ClothFinishEdge(ClothInputJobHandle *result,eiem_cloth_finish::EdgeJob *job,int *count,
    const eiem_cloth_finish::Ref *ref,int batch,const ClothInputJobHandle *dependency,void *method) {
  const auto input=dependency?*dependency:ClothInputJobHandle{};
  auto returned=s_clothFinishHooks.edge(result,job,count,ref,batch,dependency,method);
  if(result)ClothFinishCaptureActors(false,job,count,ref,batch,input,*result,method,_ReturnAddress());
  return returned;
}
static ClothInputJobHandle *__fastcall ClothFinishReduce(ClothInputJobHandle *result,eiem_cloth_finish::ReduceJob *job,int *count,
    const eiem_cloth_finish::Ref *ref,int batch,const ClothInputJobHandle *dependency,void *method) {
  const auto input=dependency?*dependency:ClothInputJobHandle{};
  auto returned=s_clothFinishHooks.reduce(result,job,count,ref,batch,dependency,method);
  if(result)ClothFinishCaptureActors(true,job,count,ref,batch,input,*result,method,_ReturnAddress());
  return returned;
}
static bool ClothFinishBind(void *self,ClothInputJobHandle dependency,eiem_cloth_finish::EdgeJob &e,eiem_cloth_finish::ReduceJob &r) {
  if(!ClothFinishOwned()||uintptr_t(self)!=s_clothFinishView.Identity().distance||
      !s_clothFinishChain.Consume(ClothFinishHandle(dependency)))return false;
  return s_clothFinishView.Bind(s_clothFinishHooks.edgeInput,s_clothFinishHooks.reduceInput,e,r);
}
static void ClothFinishAppend(ClothInputJobHandle *result,eiem_cloth_finish::EdgeJob *edge,eiem_cloth_finish::ReduceJob *reduce) {
  auto &h=s_clothFinishHooks;
  for(int n=0;n<eiem_cloth_finish::Passes;++n) {
    auto input=*result;h.edge(result,edge,(int*)edge->counter.data,&edge->counter,h.edgeBatch,&input,h.edgeMethod);
    input=*result;h.reduce(result,reduce,(int*)reduce->counter.data,&reduce->counter,h.reduceBatch,&input,h.reduceMethod);
  }
  if(++h.submissions==1||ClothFrame()-h.reportFrame>=120) {
    h.reportFrame=ClothFrame();const auto &p=s_clothFinishView.Identity().owner;
    Log("[CLOTH-CONTACT-FINISH] stage=native-jobs-chained frame=%d session=%llu generation=%llu command=%llu team=%d Process=%p privateData=%p passes=%d edges=%d submissions=%llu position=after-second-distance fixedAndForeignPreserved=1 dynamicNativeStepList=1 privateScratch=1 nativeSolver=1 distanceStiffness=%g visualVerified=0",
        ClothFrame(),p.session,p.generation,p.command,p.team,(void*)p.process,(void*)p.data,eiem_cloth_finish::Passes,
        s_clothFinishView.OwnedEdges(),h.submissions,ClothLongPanelDistanceStiffness);
  }
}
static void ClothFinishDistanceActor(ClothInputJobHandle *result,void *self,ClothInputJobHandle input,void *caller) {
  eiem_cloth_finish::EdgeJob edge{};eiem_cloth_finish::ReduceJob reduce{};bool append=false;
  __try {
    if(caller==s_clothFinishHooks.firstSite&&ClothFinishOwned()&&uintptr_t(self)==s_clothFinishView.Identity().distance)
      s_clothFinishChain.First(ClothFinishHandle(*result));
    else if(caller==s_clothFinishHooks.secondSite)append=ClothFinishBind(self,input,edge,reduce);
  } __except(EXCEPTION_EXECUTE_HANDLER){s_clothFinishView.Revoke();s_clothFinishChain.Clear();}
  if(append)ClothFinishAppend(result,&edge,&reduce);
}
static ClothInputJobHandle *__fastcall ClothFinishDistance(ClothInputJobHandle *result,void *self,
    const ClothInputJobHandle *dependency,void *method) {
  auto caller=_ReturnAddress();const auto input=dependency?*dependency:ClothInputJobHandle{};
  auto returned=s_clothFinishHooks.distance(result,self,dependency,method);
  if(!result||!ClothOnMainThread()||s_clothInputUpdateDepth!=1)return returned;
  // Validate every actor against the same original dependency, but chain each
  // appended solver through the previous actor's output handle.
  for(unsigned actor=0;actor<ClothActorCount;++actor) {
    const auto &view=s_clothFinishViews.values[actor];if(!view||!view->Ready())continue;
    ClothActorScope scope(actor);ClothFinishDistanceActor(result,self,input,caller);
  }
  return returned;
}
