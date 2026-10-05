#pragma once
static bool ClothBoneResponseTraceImpl(int slot,unsigned record,int frame,const char *&issue) {
  const auto &s=s_clothBoneSlots[slot];const auto &l=s.local;
  issue="ribbon-owner-or-policy-unconfirmed";
  if(!ClothOnMainThread()||!s_clothInputHooks||s_clothInputUpdateDepth!=1||!ClothBoneSolverEligible(s)||
      !ClothOwns(s.owner)||s.stopRequested||!ClothBoneResponseContact(s)||!l.contactConfirmed||
      l.contactProducer<0||size_t(l.contactProducer)>=s.nativeProducers.size())return false;
  const auto &p=s.nativeProducers[l.contactProducer];
  eiem_cloth_layer::Policy policy{};eiem_cloth_layer::Counts counts{};
  const auto stable=[&]{return ClothFrame()==frame&&ClothBoneSolverEligible(s)&&!s.stopRequested&&ClothOwns(s.owner)&&
      policy.ticket.session==s.owner.session&&policy.ticket.generation==s.owner.generation&&policy.ticket.command==s.command&&
      policy.ticket.process[0]==uint64_t(uintptr_t(CollisionGc(s.process[1])))&&policy.ticket.process[1]==uint64_t(uintptr_t(CollisionGc(p.process)))&&
      policy.ticket.data[0]==uint64_t(uintptr_t(CollisionGc(s.candidateData)))&&policy.ticket.data[1]==uint64_t(uintptr_t(CollisionGc(p.data)))&&
      policy.inner.team==s.team[1]&&policy.outer.team==p.team&&policy.list==l.contactListHeaders[1]&&ClothBoneNativeProducerIdentity(s,p);};
  if(!s_clothLayerGate.Snapshot(policy,counts)||!stable())return false;
  void *team=nullptr,*sim=nullptr,*transforms=nullptr;uintptr_t access=0;int length=0;void *get=nullptr;
  eiem_cloth_layer::Surface surface{};ClothInputChunk tc{};
  issue="ribbon-native-output-map-unconfirmed";
  if(!ClothBoneContactTeam(p.team,CollisionGc(p.process),team)||!ClothBoneResponsePointSurface(s,p,team,surface)||
      surface.start!=policy.outer.start||surface.count!=policy.outer.count||surface.activeCount!=policy.outer.activeCount||surface.active!=policy.outer.active||
      !ClothContactManager("get_Simulation","BeyondDynamicBone.SimulationManager",sim)||
      !ClothContactManager("get_Bone","BeyondDynamicBone.DynamicBoneTransformManager",transforms)||
      !ClothBoneSolverAccess(transforms,access,length,get)||!ClothInputChunkRead(team,"proxyTransformChunk",length,tc,33))return false;
  int relative=-1;if(!ClothInputTeamField(team,"useRelativeTransform","System.Int32",relative)||relative!=0){issue="ribbon-relative-space-unconfirmed";return false;}
  ClothInputArray arrays[3]{};const char *names[]{"teamIdArray","nextPosArray","dispPosArray"};
  const char *types[]{"System.Int16","Unity.Mathematics.double3","Unity.Mathematics.double3"};
  if(!ClothContactArrays(sim,arrays,names,types,3))return false;
  struct Point {int index=-1,asset=-1;double next[3]{},display[3]{};eiem_cloth_surface::OutputMatrix visible{};};
  std::array<Point,5> points{};
  for(int n=0;n<5;++n){auto &v=points[n];v.index=surface.start+l.response.outputVertices[n];int index=tc.start+l.response.outputTransforms[n];void *t=nullptr,*args[]{&index};int16_t owner=0;
    if(!ClothInvoke(get,&access,args,t)||!t||!ClothInputArrayValue(arrays[0],v.index,types[0],&owner,2)||owner!=p.team||
        !ClothInputArrayValue(arrays[1],v.index,types[1],v.next,sizeof(v.next))||
        !ClothInputArrayValue(arrays[2],v.index,types[2],v.display,sizeof(v.display))||!SurfaceOutputMatrix(t,v.visible))return false;
    for(int k=0;k<5;++k)if(t==ClothTarget(l.response.outputBones[k]))v.asset=k;if(v.asset<0)return false;
    for(int k=0;k<3;++k)if(!std::isfinite(v.next[k])||!std::isfinite(v.display[k]))return false;
  }
  void *manager=CollisionGc(l.contactManager),*actual=nullptr;ClothBonePairContainer list{};
  constexpr const char *element="BeyondDynamicBone.SelfCollisionConstraint.PointTriangleContact";
  constexpr const char *listType="Unity.Collections.NativeList<BeyondDynamicBone.SelfCollisionConstraint.PointTriangleContact>";
  issue="ribbon-contact-list-unavailable";
  if(!manager||!CollisionField(sim,"selfCollisionConstraint","BeyondDynamicBone.SelfCollisionConstraint",actual)||actual!=manager||
      !ClothBonePairContainerOpen(manager,"pointTriangleContactList",listType,element,list)||list.value.data!=policy.list)return false;
  uint32_t enable=0;if(!ClothBonePairMask(il2cpp_object_get_class(manager),"Flag_Enable",enable))return false;
  const int teams[]{policy.inner.team,policy.outer.team};const ClothInputChunk chunks[]{{policy.inner.start,policy.inner.count},{policy.outer.start,policy.outer.count}};
  std::vector<ClothBonePairRow> rows;int scanned=0,pairRows=0,enabled=0;
  for(int n=0;n<(std::min)(list.length,2048);++n){auto box=ClothBonePairContainerItem(list,n);uint32_t flags[2]{};
    if(!ClothBonePairVector(box,"flagAndTeamId0","System.UInt32","System.UInt32",4,1,&flags[0])||
        !ClothBonePairVector(box,"flagAndTeamId1","System.UInt32","System.UInt32",4,1,&flags[1]))return false;
    ++scanned;
    const int a=int(flags[0]&0xffffff),b=int(flags[1]&0xffffff);
    if(!((a==teams[0]&&b==teams[1])||(a==teams[1]&&b==teams[0])))continue;
    ClothBonePairRow row{};if(!ClothBonePairRecord(box,1,n,row))return false;row.team[0]=a;row.team[1]=b;
    if(!ClothBonePairRange(row,teams,chunks))return false;++pairRows;enabled+=(flags[0]&enable)!=0;
    if(rows.size()<64)rows.push_back(row);
  }
  ClothBonePairContainer after{};eiem_cloth_layer::Policy current{};eiem_cloth_layer::Counts currentCounts{};
  issue="ribbon-identity-changed-during-read";
  if(!stable()||!ClothBonePairContainerOpen(manager,"pointTriangleContactList",listType,element,after)||
      after.value.data!=list.value.data||after.length!=list.length||!s_clothLayerGate.Snapshot(current,currentCounts)||!(policy.ticket==current.ticket))return false;
  Log("[CLOTH-RIBBON-TRACE-BEGIN] slot=%d record=%u frame=%d generation=%llu command=%u innerTeam=%d outerTeam=%d points=5 pairRows=%d enabled=%d listTotal=%d scanned=%d completeScan=%d retained=%zu solverCalls=%llu solverCorrected=%llu enableMask=%u sourceRowsAreHistory=1 renderDepth=unmeasured",
      slot,record,frame,s.owner.generation,s.command,teams[0],teams[1],pairRows,enabled,list.length,scanned,int(scanned==list.length),rows.size(),counts.enabled,counts.enabledChanged,enable);
  for(const auto &v:points){std::ostringstream out;out<<std::setprecision(12)<<"{\"slot\":"<<slot<<",\"record\":"<<record<<",\"frame\":"<<frame
      <<",\"team\":"<<p.team<<",\"particle\":"<<v.index<<",\"asset\":"<<v.asset<<",\"name\":"<<CollisionJsonString(l.recipe->responsePoints[v.asset].name)<<",\"next\":";
    ClothInputJsonArray(out,v.next,3);out<<",\"display\":";ClothInputJsonArray(out,v.display,3);out<<",\"visibleMatrix\":";ClothInputJsonArray(out,v.visible.v,16);out<<'}';
    Log("[CLOTH-RIBBON-TRACE-POINT] %s",out.str().c_str());}
  for(const auto &r:rows){std::ostringstream out;out<<std::setprecision(12)<<"{\"slot\":"<<slot<<",\"record\":"<<record<<",\"frame\":"<<frame
      <<",\"index\":"<<r.index<<",\"teams\":["<<r.team[0]<<','<<r.team[1]<<"],\"flags\":["<<r.flags[0]<<','<<r.flags[1]<<"],\"particles\":";
    ClothInputJsonArray(out,r.particle,4);out<<",\"inverseMass\":";ClothInputJsonArray(out,r.mass,4);out<<",\"thickness\":"<<r.thickness<<",\"sign\":"<<r.sign<<'}';
    Log("[CLOTH-RIBBON-TRACE-CONTACT] %s",out.str().c_str());}
  Log("[CLOTH-RIBBON-TRACE-END] slot=%d record=%u frame=%d nativeRowsAndOriginalOutputReadOnly=1 sourceStepUnknown=1",slot,record,frame);return true;
}
static void ClothBoneResponseTrace(int slot,unsigned record,int frame) {
  if(slot<0||slot>=s_clothBoneCount||!ClothBoneResponseContact(s_clothBoneSlots[slot])||!s_clothBoneSlots[slot].local.contactConfirmed)return;
  bool ok=false;const char *issue="read-fault";
  __try {ok=ClothBoneResponseTraceImpl(slot,record,frame,issue);}
  __except(EXCEPTION_EXECUTE_HANDLER) {ok=false;issue="read-fault";}
  if(!ok)Log("[CLOTH-RIBBON-TRACE-ISSUE] slot=%d record=%u frame=%d reason=%s diagnosticOnly=1 nativeSimulationRetained=1",slot,record,frame,issue);
}
