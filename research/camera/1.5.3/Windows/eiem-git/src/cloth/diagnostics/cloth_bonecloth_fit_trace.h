#pragma once
struct ClothBoneFitReference {
  int rootParticle=-1;double basic[3]{},referenceDistance=0,outputDistance=0,ratio=0;
  bool ratioKnown=false;
};
static bool ClothBoneFitInitialScale(void *team,float (&scale)[3]) {
  float value[3]{};
  if(!ClothOnMainThread() || !team ||
      !ClothInputTeamField(team,"initScale","Unity.Mathematics.float3",value))return false;
  for(float v:value)if(!std::isfinite(v) || v==0)return false;
  memcpy(scale,value,sizeof(value));return true;
}
static bool ClothBoneFitRootDistances(const double (&point)[3],const double (&root)[3],
                                     const double (&basic)[3],const double (&rootBasic)[3],ClothBoneFitReference &out) {
  double current=0,reference=0;
  for(int n=0;n<3;++n) {
    if(!std::isfinite(point[n]) || !std::isfinite(root[n]) || !std::isfinite(basic[n]) || !std::isfinite(rootBasic[n]))return false;
    const double a=point[n]-root[n],b=basic[n]-rootBasic[n];current+=a*a;reference+=b*b;
  }
  if(!std::isfinite(current) || !std::isfinite(reference) || reference<1e-16)return false;
  out.outputDistance=sqrt(current);out.referenceDistance=sqrt(reference);out.ratio=out.outputDistance/out.referenceDistance;
  return std::isfinite(out.ratio);
}
static bool ClothBoneFitReferences(void *simulation,void *team,int expectedTeam,const ClothContactSample &sample,
                                   std::vector<ClothBoneFitReference> &result) {
  if(!ClothOnMainThread() || !s_clothInputHooks || s_clothInputUpdateDepth!=1 || !simulation || !team ||
      expectedTeam<=0 || !sample.outputKnown || sample.frame!=ClothFrame() || sample.particles<1 || sample.particles>ClothContactParticles)return false;
  void *mesh=nullptr;ClothInputArray teams{},roots{},particleTeams{};ClothInputChunk vc{},pc{};
  ClothBonePairContainer basic{};bool created=false;int relative=-1;
  if(!ClothInputTeamField(team,"useRelativeTransform","System.Int32",relative) || relative!=0 ||
      !ClothContactManager("get_VMesh","BeyondDynamicBone.VirtualMeshManager",mesh) ||
      !ClothInputArrayOpen(mesh,"teamIds","System.Int16",teams) ||
      !ClothInputArrayOpen(mesh,"vertexRootIndices","System.Int32",roots) ||
      !ClothInputArrayOpen(simulation,"teamIdArray","System.Int16",particleTeams) ||
      !ClothBonePairContainerOpen(simulation,"stepBasicPositionBuffer","Unity.Collections.NativeArray<Unity.Mathematics.double3>","Unity.Mathematics.double3",basic) ||
      !ClothValue(SurfaceMethod(basic.cls,"get_IsCreated","System.Boolean"),&basic.value,created) || !created ||
      !ClothInputChunkRead(team,"proxyCommonChunk",teams.length,vc,ClothContactParticles) || vc.count!=sample.particles ||
      vc.start>roots.length || vc.count>roots.length-vc.start ||
      !ClothInputChunkRead(team,"particleChunk",particleTeams.length,pc,ClothContactParticles) || pc.count!=vc.count ||
      pc.start>basic.length || pc.count>basic.length-pc.start)return false;
  std::vector<ClothBoneFitReference> values(pc.count);std::vector<int> rootIndices(pc.count);
  int16_t owner=0;
  for(int n=0;n<pc.count;++n) {
    int16_t proxyOwner=0,particleOwner=0;
    if(sample.points[n].proxy!=vc.start+n || sample.points[n].particle!=pc.start+n ||
        !ClothInputArrayValue(teams,vc.start+n,"System.Int16",&proxyOwner,2) ||
        !ClothInputArrayValue(particleTeams,pc.start+n,"System.Int16",&particleOwner,2) ||
        proxyOwner!=expectedTeam || proxyOwner!=particleOwner || (n && owner!=proxyOwner) ||
        !ClothInputArrayValue(roots,vc.start+n,"System.Int32",&rootIndices[n],4) ||
        rootIndices[n]<-1 || rootIndices[n]>=pc.count ||
        !ClothInputCopyBox(ClothBonePairContainerItem(basic,pc.start+n),"Unity.Mathematics.double3",values[n].basic,24))return false;
    owner=proxyOwner;for(double v:values[n].basic)if(!std::isfinite(v))return false;
  }
  for(int n=0;n<pc.count;++n) {
    const int root=rootIndices[n];if(root<0)continue;
    auto &v=values[n];v.rootParticle=pc.start+root;
    v.ratioKnown=ClothBoneFitRootDistances(sample.points[n].next,sample.points[root].next,v.basic,values[root].basic,v);
  }
  if(sample.frame!=ClothFrame())return false;
  result=std::move(values);return true;
}
template<class T> static bool ClothBoneFitNested(void *box,const char *field,const char *type,
                                                const char *member,const char *memberType,T &value) {
  if(!box) return false;
  auto outer=il2cpp_object_get_class(box);auto f=CollisionFieldInfo(outer,field,type);
  auto inner=f?il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;
  uint32_t align=0;
  const int outerSize=outer?il2cpp_class_value_size(outer,&align):0;
  const int innerSize=inner?il2cpp_class_value_size(inner,&align):0;
  if(outerSize<=0 || outerSize>2048 || innerSize<=0 || innerSize>512) return false;
  const int outerOffset=ClothValueOffset(outer,field,type,outerSize,innerSize);
  const int innerOffset=ClothValueOffset(inner,member,memberType,innerSize,sizeof(T));
  auto child=CollisionFieldInfo(inner,member,memberType);
  auto childClass=child?il2cpp_class_from_type(il2cpp_field_get_type(child)):nullptr;
  if(outerOffset<0 || innerOffset<0 || !childClass || il2cpp_class_value_size(childClass,&align)!=sizeof(T)) return false;
  memcpy(&value,(char*)box+16+outerOffset+innerOffset,sizeof(T));return true;
}
static bool ClothBoneFitTraceImpl(int slot,unsigned record,void *team,const ClothContactSample &sample) {
  if(!ClothOnMainThread() || slot<0 || slot>=s_clothBoneCount) return true;
  const auto &b=s_clothBoneSlots[slot];const auto &binding=s_clothBoneSolver[slot].binding;
  if(!ClothBoneSolverEligible(b) || !sample.outputKnown || sample.frame!=ClothFrame() ||
      binding.identity.team!=b.team[1] || !team) return false;
  void *manager=nullptr,*simulation=nullptr;ClothInputArray parameters{},arrays[3]{};
  if(!ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager) ||
      !ClothInputArrayOpen(manager,"parameterArray","BeyondDynamicBone.ClothParameters",parameters) ||
      !ClothContactManager("get_Simulation","BeyondDynamicBone.SimulationManager",simulation)) return false;
  const char *names[]{"teamIdArray","basePosArray","baseRotArray"};
  const char *types[]{"System.Int16","Unity.Mathematics.double3","Unity.Mathematics.quaternion"};
  ClothInputChunk chunk{};
  if(!ClothContactArrays(simulation,arrays,names,types,3) ||
      !ClothInputChunkRead(team,"particleChunk",arrays[0].length,chunk,ClothContactParticles) ||
      !ClothContactRange(chunk,arrays,3,ClothContactParticles) || chunk.count!=sample.particles) return false;
  auto box=ClothInputArrayBox(parameters,b.team[1]);
  float radius[16]{},distance[16]{},maxDistance[16]{},backstop[16]{},attenuation=0,ratio=0,scale=0,backstopRadius=0,stiffness=0;
  bool useMax=false,useBackstop=false;int normalAxis=0;
  constexpr auto motion="BeyondDynamicBone.MotionConstraint.MotionConstraintParams";
  if(!box || !ClothInputTeamField(team,"animationPoseRatio","System.Single",ratio) ||
      !ClothInputTeamField(team,"scaleRatio","System.Single",scale) ||
      !ClothInputTeamField(box,"normalAxis","BeyondDynamicBone.ClothNormalAxis",normalAxis) ||
      !ClothInputTeamField(box,"radiusCurveData","Unity.Mathematics.float4x4",radius) ||
      !ClothBoneLocalDistanceParameters(box,distance,attenuation) ||
      !ClothBoneFitNested(box,"motionConstraint",motion,"useMaxDistance","System.Boolean",useMax) ||
      !ClothBoneFitNested(box,"motionConstraint",motion,"maxDistanceCurveData","Unity.Mathematics.float4x4",maxDistance) ||
      !ClothBoneFitNested(box,"motionConstraint",motion,"useBackstop","System.Boolean",useBackstop) ||
      !ClothBoneFitNested(box,"motionConstraint",motion,"backstopRadius","System.Single",backstopRadius) ||
      !ClothBoneFitNested(box,"motionConstraint",motion,"backstopDistanceCurveData","Unity.Mathematics.float4x4",backstop) ||
      !ClothBoneFitNested(box,"motionConstraint",motion,"stiffness","System.Single",stiffness)) return false;
  for(float value:{ratio,scale,attenuation,backstopRadius,stiffness}) if(!std::isfinite(value)) return false;
  if(scale<=0 || ratio<0 || ratio>1) return false;
  const char *axisName=nullptr;
  for(const char *name:{"Right","Up","Forward","InverseRight","InverseUp","InverseForward"}) {
    int value=0;if(!CollisionEnumValue(SurfaceClass("BeyondDynamicBone","ClothNormalAxis"),name,value)) return false;
    if(value==normalAxis) {if(axisName) return false;axisName=name;}
  }
  if(!axisName) return false;
  for(int n=0;n<16;++n) if(!std::isfinite(radius[n]) || radius[n]<0 ||
      !std::isfinite(distance[n]) || !std::isfinite(maxDistance[n]) || !std::isfinite(backstop[n])) return false;
  void *evaluate=nullptr;auto utility=SurfaceClass("BeyondDynamicBone","DataUtility");
  if(!utility || !s_clothMethodFlags || !ClothInputLayout(SurfaceClass("Unity.Mathematics","float4x4"),"Unity.Mathematics.float4x4",64)) return false;
  for(void *it=nullptr,*method=nullptr;(method=il2cpp_class_get_methods(utility,&it));) {
    uint32_t impl=0;
    if(strcmp(il2cpp_method_get_name(method),"EvaluateCurve") || il2cpp_method_get_param_count(method)!=2 ||
        !(s_clothMethodFlags(method,&impl)&0x10) || !CollisionType(il2cpp_method_get_return_type(method),"System.Single") ||
        !CollisionType(il2cpp_method_get_param(method,0),"Unity.Mathematics.float4x4&") ||
        !CollisionType(il2cpp_method_get_param(method,1),"System.Single")) continue;
    if(evaluate) return false;evaluate=method;
  }
  if(!evaluate) return false;
  struct Point {int id=0;double base[3]{};float rotation[4]{},radius=0;};
  std::vector<Point> points;
  for(int n=0;n<sample.particles;++n) {
    int16_t owner=0;Point p{};p.id=sample.outputs[n].id;
    if(sample.points[n].particle!=chunk.start+n || !p.id ||
        !ClothInputArrayValue(arrays[0],chunk.start+n,types[0],&owner,2) || owner!=binding.identity.team ||
        !ClothInputArrayValue(arrays[1],chunk.start+n,types[1],p.base,sizeof(p.base)) ||
        !ClothInputArrayValue(arrays[2],chunk.start+n,types[2],p.rotation,sizeof(p.rotation))) return false;
    for(double v:p.base) if(!std::isfinite(v)) return false;
    float norm=0;for(float v:p.rotation) {if(!std::isfinite(v)) return false;norm+=v*v;}
    if(fabsf(norm-1)>0.01f) return false;
    float depth=sample.points[n].depth;void *value=nullptr,*args[]{radius,&depth};
    if(!std::isfinite(depth) || depth<0 || depth>1 || !ClothInvoke(evaluate,nullptr,args,value) ||
        !ClothInputCopyBox(value,"System.Single",&p.radius,4) || !std::isfinite(p.radius) || p.radius<0) return false;
    points.push_back(p);
  }
  std::vector<ClothBoneFitReference> references;
  const bool referenceKnown=ClothBoneFitReferences(simulation,team,binding.identity.team,sample,references);
  if(!ClothBoneSolverEligible(b) || !ClothInputIdentity(true,binding) || sample.frame!=ClothFrame()) return false;
  ClothBoneTetherParameters tether{};float bending=0;
  const bool elasticKnown=ClothBoneElasticParameters(box,tether,bending);
  float initialScale[3]{};const bool initialScaleKnown=ClothBoneFitInitialScale(team,initialScale);
  std::ostringstream out;out<<std::setprecision(12);
  out<<"{\"slot\":"<<slot<<",\"record\":"<<record<<",\"frame\":"<<sample.frame<<",\"team\":"<<binding.identity.team
     <<",\"particles\":"<<sample.particles<<",\"animationPoseRatio\":"<<ratio<<",\"scaleRatio\":"<<scale
     <<",\"useMaxDistance\":"<<int(useMax)<<",\"useBackstop\":"<<int(useBackstop)<<",\"backstopRadius\":"<<backstopRadius
     <<",\"motionStiffness\":"<<stiffness<<",\"normalAxisRaw\":"<<normalAxis<<",\"normalAxis\":"<<CollisionJsonString(axisName)
     <<",\"distanceAttenuation\":"<<attenuation<<",\"radiusCurve\":";
  ClothInputJsonArray(out,radius,16);out<<",\"distanceCurve\":";ClothInputJsonArray(out,distance,16);
  out<<",\"maxDistanceCurve\":";ClothInputJsonArray(out,maxDistance,16);
  out<<",\"backstopDistanceCurve\":";ClothInputJsonArray(out,backstop,16);
  out<<",\"elasticParametersKnown\":"<<(elasticKnown?"true":"false");
  if(elasticKnown) out<<",\"tetherCompression\":"<<tether.compression<<",\"tetherStretch\":"<<tether.stretch<<",\"bendingStiffness\":"<<bending;
  out<<",\"stepBasicReferenceKnown\":"<<(referenceKnown?"true":"false")
     <<",\"initialScaleKnown\":"<<(initialScaleKnown?"true":"false");
  if(initialScaleKnown) {out<<",\"initScale\":";ClothInputJsonArray(out,initialScale,3);}
  out<<",\"simulationTicketKnown\":false,\"renderDepthMeasured\":false}";
  Log("[CLOTH-BONE-FIT-BEGIN] %s",out.str().c_str());
  for(size_t n=0;n<points.size();++n) {
    const auto &p=points[n];
    std::ostringstream row;row<<std::setprecision(12);
    row<<"{\"slot\":"<<slot<<",\"record\":"<<record<<",\"frame\":"<<sample.frame<<",\"id\":"<<p.id<<",\"radiusUnscaled\":"<<p.radius<<",\"base\":";
    ClothInputJsonArray(row,p.base,3);row<<",\"rotation\":";ClothInputJsonArray(row,p.rotation,4);
    if(referenceKnown) {
      const auto &v=references[n];row<<",\"rootParticle\":"<<v.rootParticle<<",\"stepBasic\":";
      ClothInputJsonArray(row,v.basic,3);row<<",\"rootDistancesKnown\":"<<(v.ratioKnown?"true":"false");
      if(v.ratioKnown)row<<",\"referenceRootDistance\":"<<v.referenceDistance<<",\"outputRootDistance\":"<<v.outputDistance<<",\"rootStretchRatio\":"<<v.ratio;
    }
    row<<'}';
    Log("[CLOTH-BONE-FIT-DATA] %s",row.str().c_str());
  }
  Log("[CLOTH-BONE-FIT-END] slot=%d record=%u frame=%d count=%zu",slot,record,sample.frame,points.size());
  return true;
}
static void ClothBoneFitTrace(int slot,unsigned record,void *team,const ClothContactSample &sample) {
  bool ok=false;
  __try {ok=ClothBoneFitTraceImpl(slot,record,team,sample);}
  __except(EXCEPTION_EXECUTE_HANDLER) {ok=false;}
  if(!ok) Log("[CLOTH-BONE-FIT-ISSUE] slot=%d record=%u frame=%d reason=effective-reference-evidence-unavailable nativeSimulationRetained=1",slot,record,sample.frame);
}
