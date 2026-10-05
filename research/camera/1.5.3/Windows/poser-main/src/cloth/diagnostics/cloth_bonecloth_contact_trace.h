#pragma once
struct ClothBonePairRow {
  int kind=0,index=0,team[2]{},particle[4]{};
  uint32_t flags[2]{};
  float thickness=0,sign=0,mass[4]{},normal[3]{},edge[2]{};
};
static float ClothBonePairHalf(uint16_t bits) {
  const int exponent=(bits>>10)&31, mantissa=bits&1023;
  const float value=exponent==31?(mantissa?NAN:INFINITY):
      std::ldexp(float(exponent?1024+mantissa:mantissa),exponent?exponent-25:-24);
  return bits&0x8000?-value:value;
}
static bool ClothBonePairScalarLayout(void *cls,const char *type,int bytes) {
  uint32_t align=0;
  if(!cls || !CollisionType(il2cpp_class_get_type(cls),type) || il2cpp_class_value_size(cls,&align)!=bytes)return false;
  if(!strcmp(type,"Unity.Mathematics.half")) {
    auto f=CollisionFieldInfo(cls,"value","System.UInt16");
    return bytes==2 && f && il2cpp_field_get_flags && !(il2cpp_field_get_flags(f)&0x10) &&
        ClothValueOffset(cls,"value","System.UInt16",bytes,2)==0;
  }
  return bytes==4 && (!strcmp(type,"System.Int32") || !strcmp(type,"System.UInt32"));
}
static bool ClothBonePairVector(void *box,const char *field,const char *type,
                                const char *scalar,int width,int count,void *dest) {
  if(!box || !dest || width<1 || count<1 || count>3)return false;
  auto outer=il2cpp_object_get_class(box);auto f=CollisionFieldInfo(outer,field,type);
  auto cls=f?il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;
  uint32_t align=0;const int bytes=outer?il2cpp_class_value_size(outer,&align):0;
  if(bytes<1 || bytes>256 || !cls || !il2cpp_field_get_flags || (il2cpp_field_get_flags(f)&0x10) ||
      il2cpp_class_value_size(cls,&align)!=width*count)return false;
  const int offset=ClothValueOffset(outer,field,type,bytes,width*count);
  if(offset<0)return false;
  if(count==1) {
    if(!ClothBonePairScalarLayout(cls,scalar,width))return false;
    memcpy(dest,(char*)box+16+offset,width);return true;
  }
  const char *names[]{"x","y","z"};
  for(int n=0;n<count;++n) {
    auto child=CollisionFieldInfo(cls,names[n],scalar);
    auto childClass=child?il2cpp_class_from_type(il2cpp_field_get_type(child)):nullptr;
    if(!child || (il2cpp_field_get_flags(child)&0x10) || ClothValueOffset(cls,names[n],scalar,width*count,width)!=n*width ||
        !ClothBonePairScalarLayout(childClass,scalar,width))return false;
    memcpy((char*)dest+n*width,(char*)box+16+offset+n*width,width);
  }
  return true;
}
static bool ClothBonePairRecord(void *box,int kind,int index,ClothBonePairRow &out) {
  const char *types[]{"BeyondDynamicBone.SelfCollisionConstraint.EdgeEdgeContact",
                      "BeyondDynamicBone.SelfCollisionConstraint.PointTriangleContact"};
  if(kind<0 || kind>1 || !box || !CollisionType(il2cpp_class_get_type(il2cpp_object_get_class(box)),types[kind]))return false;
  ClothBonePairRow r{};r.kind=kind;r.index=index;
  uint16_t thickness=0,mass[4]{},normal[3]{},edge[2]{},sign=0;
  const auto half=[&](const char *field,int count,void *dest) {
    return ClothBonePairVector(box,field,count==1?"Unity.Mathematics.half":count==2?"Unity.Mathematics.half2":"Unity.Mathematics.half3",
        "Unity.Mathematics.half",2,count,dest);
  };
  const auto ints=[&](const char *field,int count,void *dest) {
    return ClothBonePairVector(box,field,count==1?"System.Int32":count==2?"Unity.Mathematics.int2":"Unity.Mathematics.int3",
        "System.Int32",4,count,dest);
  };
  if(!ClothBonePairVector(box,"flagAndTeamId0","System.UInt32","System.UInt32",4,1,&r.flags[0]) ||
      !ClothBonePairVector(box,"flagAndTeamId1","System.UInt32","System.UInt32",4,1,&r.flags[1]) || !half("thickness",1,&thickness))return false;
  if(kind) {
    if(!ints("pointParticleIndex",1,r.particle) || !ints("triangleParticleIndex",3,r.particle+1) ||
        !half("sign",1,&sign) || !half("pointInvMass",1,mass) || !half("triangleInvMass",3,mass+1))return false;
  } else if(!ints("edgeParticleIndex0",2,r.particle) || !ints("edgeParticleIndex1",2,r.particle+2) ||
      !half("edgeInvMass0",2,mass) || !half("edgeInvMass1",2,mass+2) || !half("s",1,edge) ||
      !half("t",1,edge+1) || !half("n",3,normal))return false;
  r.thickness=ClothBonePairHalf(thickness);r.sign=ClothBonePairHalf(sign);
  if(!std::isfinite(r.thickness) || r.thickness<0 || !std::isfinite(r.sign))return false;
  for(int n=0;n<4;++n){r.mass[n]=ClothBonePairHalf(mass[n]);if(!std::isfinite(r.mass[n]) || r.mass[n]<0 || r.particle[n]<0)return false;}
  for(int n=0;n<3;++n){r.normal[n]=ClothBonePairHalf(normal[n]);if(!std::isfinite(r.normal[n]))return false;}
  for(int n=0;n<2;++n){r.edge[n]=ClothBonePairHalf(edge[n]);if(!std::isfinite(r.edge[n]))return false;}
  out=r;return true;
}
static bool ClothBonePairMask(void *cls,const char *name,uint32_t &mask) {
  auto f=cls?CollisionFieldInfo(cls,name,"System.UInt32"):nullptr;
  if(!f || !il2cpp_field_get_flags || !il2cpp_field_static_get_value || (il2cpp_field_get_flags(f)&0x50)!=0x50)return false;
  il2cpp_field_static_get_value(f,&mask);return mask && !(mask&(mask-1));
}
struct ClothBonePairContainer {
  alignas(16) eiem_cloth_contact_job::Container value{};
  void *cls=nullptr,*item=nullptr;int length=0;
};
static bool ClothBonePairContainerOpen(void *manager,const char *field,const char *type,const char *element,
                                     ClothBonePairContainer &out) {
  auto f=manager?CollisionFieldInfo(il2cpp_object_get_class(manager),field,type):nullptr;
  auto cls=f?il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;uint32_t align=0;
  ClothBonePairContainer v{};v.cls=cls;
  if(!cls || il2cpp_class_value_size(cls,&align)!=sizeof(v.value) || !ClothField(manager,field,type,v.value))return false;
  v.item=SurfaceMethod(cls,"get_Item",element,"System.Int32");
  if(!v.item || !ClothValue(SurfaceMethod(cls,"get_Length","System.Int32"),&v.value,v.length) ||
      v.length<0 || v.length>65536 || (v.length && !v.value.data))return false;
  out=v;return true;
}
static void *ClothBonePairContainerItem(ClothBonePairContainer &v,int index) {
  void *box=nullptr,*args[]{&index};
  return index>=0 && index<v.length && ClothInvoke(v.item,&v.value,args,box)?box:nullptr;
}
static bool ClothBonePairRange(const ClothBonePairRow &r,const int (&teams)[2],const ClothInputChunk (&chunks)[2]) {
  if(r.kind<0 || r.kind>1 || teams[0]<=0 || teams[1]<=0 || teams[0]==teams[1] ||
      !((r.team[0]==teams[0] && r.team[1]==teams[1]) || (r.team[0]==teams[1] && r.team[1]==teams[0])))return false;
  for(int n=0;n<4;++n) {
    const int side=n<(r.kind?1:2)?0:1;const auto &c=chunks[r.team[side]==teams[0]?0:1];
    if(c.start<0 || c.count<=0 || c.count>ClothContactParticles || r.particle[n]<c.start || r.particle[n]-c.start>=c.count)return false;
  }
  for(int n=0;n<4;++n)for(int k=0;k<n;++k)if(r.particle[n]==r.particle[k])return false;
  return true;
}
static bool ClothBonePairTraceImpl(int slot,unsigned record,int frame,const char *&issue) {
  issue="context-unavailable";
  if(!ClothOnMainThread() || !s_clothInputHooks || s_clothInputUpdateDepth!=1 || slot<0 || slot>=s_clothBoneCount)return false;
  auto &b=s_clothBoneSlots[slot];const auto &l=b.local;
  if(!l.contactConfigured || !l.contactConfirmed || !ClothBoneLayeredPair(b) || !ClothBoneSolverEligible(b))return true;
  const auto &p=s_clothBoneSlots[b.contactPartner];
  if(!ClothBoneSolverEligible(p) || frame!=ClothFrame())return false;
  const auto owner=b.owner;const unsigned command=b.command;const int teams[]{b.team[1],p.team[1]};
  const uint32_t process[]{b.process[1],p.process[1]},data[]{b.candidateData,p.candidateData};
  eiem_cloth_rebuild::Identity beforeIdentity[2]{};uint64_t beforeFlags[2]{};
  issue="pair-installed-identity-unconfirmed";
  if(!ClothBoneContactPairIdentity(b,beforeIdentity,beforeFlags))return false;
  const auto stable=[&] {
    return ClothFrame()==frame && ClothBoneSolverEligible(b) && ClothBoneSolverEligible(p) &&
        b.owner==owner && p.owner==owner && b.command==command && p.command==command &&
        b.team[1]==teams[0] && p.team[1]==teams[1] && b.process[1]==process[0] && p.process[1]==process[1] &&
        b.candidateData==data[0] && p.candidateData==data[1] && ClothBoneLayeredPair(b);
  };
  void *team[2]{},*simulation=nullptr,*manager=CollisionGc(l.contactManager);
  issue="pair-Process-or-manager-unconfirmed";
  if(!manager || !ClothBoneContactTeam(teams[0],CollisionGc(process[0]),team[0]) ||
      !ClothBoneContactTeam(teams[1],CollisionGc(process[1]),team[1]) ||
      !ClothContactManager("get_Simulation","BeyondDynamicBone.SimulationManager",simulation))return false;
  void *actual=nullptr;
  if(!ClothField(simulation,"selfCollisionConstraint","BeyondDynamicBone.SelfCollisionConstraint",actual) || actual!=manager)return false;
  uint32_t enable=0,ignore=0,fixed[3]{};auto cls=il2cpp_object_get_class(manager);
  issue="contact-flags-unavailable";
  if(!ClothBonePairMask(cls,"Flag_Enable",enable) || !ClothBonePairMask(cls,"Flag_Ignore",ignore) ||
      !ClothBonePairMask(cls,"Flag_Fix0",fixed[0]) || !ClothBonePairMask(cls,"Flag_Fix1",fixed[1]) ||
      !ClothBonePairMask(cls,"Flag_Fix2",fixed[2]))return false;
  const uint32_t masks[]{enable,ignore,fixed[0],fixed[1],fixed[2]};
  for(int n=0;n<5;++n)for(int k=0;k<n;++k)if(masks[n]==masks[k])return false;
  ClothInputArray arrays[4]{},primitive{};
  const char *names[]{"teamIdArray","nextPosArray","oldPosArray","dispPosArray"};
  const char *types[]{"System.Int16","Unity.Mathematics.double3","Unity.Mathematics.double3","Unity.Mathematics.double3"};
  ClothInputChunk chunks[2]{};
  issue="pair-particle-ranges-unavailable";
  if(!ClothContactArrays(simulation,arrays,names,types,4) ||
      !ClothInputArrayOpen(manager,"primitiveArray","BeyondDynamicBone.SelfCollisionConstraint.Primitive",primitive))return false;
  auto primitiveBox=ClothInputArrayBox(primitive,0);auto primitiveClass=primitiveBox?il2cpp_object_get_class(primitiveBox):nullptr;
  if(!primitiveClass || !CollisionType(il2cpp_class_get_type(primitiveClass),"BeyondDynamicBone.SelfCollisionConstraint.Primitive"))return false;
  uint32_t align=0;const int primitiveSize=primitiveClass?il2cpp_class_value_size(primitiveClass,&align):0;
  const int flagOffset=primitiveClass?ClothValueOffset(primitiveClass,"flagAndTeamId","System.UInt32",primitiveSize,4):-1;
  auto getTeam=primitiveClass?SurfaceMethod(primitiveClass,"GetTeamId","System.Int32"):nullptr;
  if(primitiveSize<4 || primitiveSize>256 || flagOffset<0 || !getTeam)return false;
  ClothBonePairContainer intersections{};
  const bool intersectsKnown=ClothBonePairContainerOpen(manager,"intersectFlagArray","Unity.Collections.NativeArray<System.Byte>","System.Byte",intersections);
  struct Point {int team,index;double next[3]{},old[3]{},display[3]{};int intersect=-1;};
  std::vector<Point> points;
  for(int side=0;side<2;++side) {
    int relative=-1;
    if(!ClothInputTeamField(team[side],"useRelativeTransform","System.Int32",relative) || relative!=0 ||
        !ClothInputChunkRead(team[side],"particleChunk",arrays[0].length,chunks[side],ClothContactParticles) ||
        !ClothContactRange(chunks[side],arrays,4,ClothContactParticles) ||
        chunks[side].count!=ClothBoneCandidate(side?p:b).EffectiveCount())return false;
    for(int n=0;n<chunks[side].count;++n) {
      Point v{teams[side],chunks[side].start+n};int16_t ownerTeam=0;
      if(!ClothInputArrayValue(arrays[0],v.index,types[0],&ownerTeam,2) || ownerTeam!=v.team ||
          !ClothInputArrayValue(arrays[1],v.index,types[1],v.next,sizeof(v.next)) ||
          !ClothInputArrayValue(arrays[2],v.index,types[2],v.old,sizeof(v.old)) ||
          !ClothInputArrayValue(arrays[3],v.index,types[3],v.display,sizeof(v.display)))return false;
      for(int k=0;k<3;++k)if(!std::isfinite(v.next[k]) || !std::isfinite(v.old[k]) || !std::isfinite(v.display[k]))return false;
      if(intersectsKnown) {
        auto box=ClothBonePairContainerItem(intersections,v.index);uint32_t byteAlign=0;
        if(box && CollisionType(il2cpp_class_get_type(il2cpp_object_get_class(box)),"System.Byte") &&
            il2cpp_class_value_size(il2cpp_object_get_class(box),&byteAlign)==1)v.intersect=*(unsigned char*)((char*)box+16);
      }
      points.push_back(v);
    }
  }
  if(chunks[0].start<chunks[1].start+chunks[1].count && chunks[1].start<chunks[0].start+chunks[0].count)return false;
  issue="contact-list-or-record-unavailable";
  std::vector<ClothBonePairRow> rows;int totals[2]{},scanned[2]{},pairCounts[2]{},enabled[2]{};
  for(int kind=0;kind<2;++kind) {
    const char *field=kind?"pointTriangleContactList":"edgeEdgeContactList";
    const char *element=kind?"BeyondDynamicBone.SelfCollisionConstraint.PointTriangleContact":"BeyondDynamicBone.SelfCollisionConstraint.EdgeEdgeContact";
    std::string type="Unity.Collections.NativeList<";type+=element;type+='>';
    ClothBonePairContainer list{};
    if(!ClothBonePairContainerOpen(manager,field,type.c_str(),element,list) || list.value.data!=l.contactListHeaders[kind])return false;
    totals[kind]=list.length;
    for(int n=0;n<(std::min)(list.length,2048);++n) {
      ClothBonePairRow r{};auto box=ClothBonePairContainerItem(list,n);
      if(!box || !CollisionType(il2cpp_class_get_type(il2cpp_object_get_class(box)),element) ||
          !ClothBonePairVector(box,"flagAndTeamId0","System.UInt32","System.UInt32",4,1,&r.flags[0]) ||
          !ClothBonePairVector(box,"flagAndTeamId1","System.UInt32","System.UInt32",4,1,&r.flags[1]))return false;
      ++scanned[kind];
      for(int side=0;side<2;++side) {
        alignas(16) unsigned char value[256]{};memcpy(value+flagOffset,&r.flags[side],4);
        if(!ClothValue(getTeam,value,r.team[side]))return false;
      }
      if(!((r.team[0]==teams[0] && r.team[1]==teams[1]) || (r.team[0]==teams[1] && r.team[1]==teams[0])))continue;
      const int decoded[]{r.team[0],r.team[1]};
      if(!ClothBonePairRecord(box,kind,n,r))return false;
      r.team[0]=decoded[0];r.team[1]=decoded[1];
      if(!ClothBonePairRange(r,teams,chunks))return false;
      ++pairCounts[kind];if(r.flags[0]&enable)++enabled[kind];
      if(rows.size()<512)rows.push_back(r);
    }
    ClothBonePairContainer after{};
    if(!ClothBonePairContainerOpen(manager,field,type.c_str(),element,after) ||
        after.value.data!=list.value.data || after.length!=list.length)return false;
  }
  issue="identity-changed-during-contact-read";
  eiem_cloth_rebuild::Identity afterIdentity[2]{};uint64_t afterFlags[2]{};
  if(!stable() || !ClothBoneContactPairIdentity(b,afterIdentity,afterFlags) ||
      !(beforeIdentity[0]==afterIdentity[0]) || !(beforeIdentity[1]==afterIdentity[1]) || !stable())return false;
  eiem_cloth_layer::Policy layerPolicy{};eiem_cloth_layer::Counts layerCounts{};
  const bool layerKnown=s_clothLayerGate.Snapshot(layerPolicy,layerCounts) &&
      layerPolicy.ticket==ClothBoneLayerTicket(b,p) && layerPolicy.list==l.contactListHeaders[1];
  unsigned layerChecked=0,layerOpposed=0,layerUnknown=0;
  if(layerKnown)for(const auto &r:rows)if(r.kind==1) {
    eiem_cloth_layer::PointContact value{},copy{};
    std::copy(r.flags,r.flags+2,value.flags);value.point=r.particle[0];
    std::copy(r.particle+1,r.particle+4,value.triangle);
    value.sign=r.sign==1?0x3c00:r.sign==-1?0xbc00:0;
    const auto result=eiem_cloth_layer::Correct(layerPolicy,layerPolicy.list,value,copy);
    if(result==eiem_cloth_layer::Result::Kept || result==eiem_cloth_layer::Result::Changed) {
      ++layerChecked;layerOpposed+=result==eiem_cloth_layer::Result::Changed;
    } else ++layerUnknown;
  }
  if(layerKnown)Log("[CLOTH-LAYER-ORDER] stage=native-contact-readback frame=%d generation=%llu command=%u checked=%u opposed=%u unknown=%u completeScan=%d allPairRowsRetained=%d sourceHistoryOnly=1 effectiveSolverSign=see-solver-inputs geometryLayerOrderUnconfirmed=1",
      frame,owner.generation,command,layerChecked,layerOpposed,layerUnknown,int(totals[0]==scanned[0]&&totals[1]==scanned[1]),int(rows.size()==size_t(pairCounts[0]+pairCounts[1])));
  std::ostringstream begin;begin<<"{\"slot\":"<<slot<<",\"record\":"<<record<<",\"frame\":"<<frame
      <<",\"generation\":"<<owner.generation<<",\"command\":"<<command<<",\"teams\":["<<teams[0]<<','<<teams[1]
      <<"],\"particleStarts\":["<<chunks[0].start<<','<<chunks[1].start<<"],\"particleCounts\":["<<chunks[0].count<<','<<chunks[1].count
      <<"],\"listTotals\":["<<totals[0]<<','<<totals[1]<<"],\"scanned\":["<<scanned[0]<<','<<scanned[1]
      <<"],\"pairCounts\":["<<pairCounts[0]<<','<<pairCounts[1]<<"],\"enabledCounts\":["<<enabled[0]<<','<<enabled[1]
      <<"],\"enableMask\":"<<enable<<",\"ignoreMask\":"<<ignore<<",\"fixedMasks\":["<<fixed[0]<<','<<fixed[1]<<','<<fixed[2]
      <<"],\"rowsRetained\":"<<rows.size()<<",\"simulationTicketKnown\":false,\"readOnly\":true}";
  Log("[CLOTH-BONE-PAIR-BEGIN] %s",begin.str().c_str());
  for(const auto &v:points) {
    std::ostringstream out;out<<std::setprecision(12)<<"{\"slot\":"<<slot<<",\"record\":"<<record<<",\"frame\":"<<frame
        <<",\"team\":"<<v.team<<",\"particle\":"<<v.index<<",\"intersectFlag\":"<<v.intersect<<",\"next\":";
    ClothInputJsonArray(out,v.next,3);out<<",\"old\":";ClothInputJsonArray(out,v.old,3);out<<",\"display\":";ClothInputJsonArray(out,v.display,3);out<<'}';
    Log("[CLOTH-BONE-PAIR-PARTICLE] %s",out.str().c_str());
  }
  for(const auto &r:rows) {
    std::ostringstream out;out<<std::setprecision(12)<<"{\"slot\":"<<slot<<",\"record\":"<<record<<",\"frame\":"<<frame
        <<",\"kind\":"<<r.kind<<",\"index\":"<<r.index<<",\"teams\":["<<r.team[0]<<','<<r.team[1]
        <<"],\"flags\":["<<r.flags[0]<<','<<r.flags[1]<<"],\"particle\":";
    ClothInputJsonArray(out,r.particle,4);out<<",\"inverseMass\":";ClothInputJsonArray(out,r.mass,4);
    out<<",\"thickness\":"<<r.thickness<<",\"sign\":"<<r.sign<<",\"edgeParameters\":";ClothInputJsonArray(out,r.edge,2);
    out<<",\"normal\":";ClothInputJsonArray(out,r.normal,3);out<<'}';Log("[CLOTH-BONE-PAIR-CONTACT] %s",out.str().c_str());
  }
  Log("[CLOTH-BONE-PAIR-END] slot=%d record=%u frame=%d points=%zu rows=%zu completedBoundary=1 sourceStepUnknown=1",slot,record,frame,points.size(),rows.size());
  return true;
}
static void ClothBonePairTrace(int slot,unsigned record,int frame) {
  if(!ClothOnMainThread() || slot<0 || slot>=s_clothBoneCount)return;
  const auto &b=s_clothBoneSlots[slot];
  if(!b.local.contactConfigured || !b.local.contactConfirmed || !ClothBoneLayeredPair(b) || !ClothBoneSolverEligible(b))return;
  const char *issue="read-fault";bool ok=false;
  __try {ok=ClothBonePairTraceImpl(slot,record,frame,issue);}
  __except(EXCEPTION_EXECUTE_HANDLER) {ok=false;issue="read-fault";}
  if(!ok)Log("[CLOTH-BONE-PAIR-ISSUE] slot=%d record=%u frame=%d reason=%s nativeSimulationRetained=1 diagnosticOnly=1",slot,record,frame,issue);
}
