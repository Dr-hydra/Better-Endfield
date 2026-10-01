// Managed MovementComponent floor queries. All result storage is an actual
// pinned managed value object; no native FindFloorResult layout is assumed.
struct TerrainRuntime {
  bool resolved=false, available=false, initialized=false, baselineValid=false;
  std::string reason="terrain metadata not prepared";
  Pin result;
  const void *compute=nullptr,*find=nullptr,*clear=nullptr,*walkable=nullptr,*normal=nullptr;
  const void *hitField=nullptr,*distanceField=nullptr,*rayField=nullptr,*pointField=nullptr;
  DirectVmdTerrainState state{};
  DirectVmdTerrainConfig config{};
  float rootOffset=0,baselineHeight=0;
  uint64_t sequence=0;
  double sourceFrame=0;
} terrain;
bool ValueCall(const void* method,const Pin& box,void** args,void** result=nullptr) {
  if (!method || !box || !OnMainThread()) return false;
  void* payload=host->object_unbox(host->context,box.object);
  if (!payload) return false;
  void* exception=nullptr;
  void* value=host->runtime_invoke(host->context,method,payload,args,&exception);
  if (exception) return false;
  if (result) *result=value;
  return true;
}
template<class T> bool ValueRead(const void* method,const Pin& box,T& out,void** args=nullptr) {
  void* value=nullptr;
  if (!ValueCall(method,box,args,&value)) return false;
  Pin result(value);
  if (!result) return false;
  const void* payload=host->object_unbox(host->context,result.object);
  if (!payload) return false;
  memcpy(&out,payload,sizeof(T));return true;
}
bool TerrainFail(const std::string& reason) {
  terrain.available=false;terrain.reason=reason;terrain.rootOffset=0;
  Log("terrain unavailable: "+reason);return false;
}
bool PrepareTerrain() {
  if (terrain.resolved) return terrain.available;
  terrain.resolved=true;
  if (!host->resolve_class || !host->object_new || !host->resolve_field || !host->field_get_value_object)
    return TerrainFail("host boxed-result/class/field API missing");
  const char* floorClass="MovementComponent.FindFloorResult";
  BE_ResolvedClassV1 klass{};
  if (host->resolve_class(host->context,kGame,kSmcNs,floorClass,&klass)!=BE_Result_Ok || !klass.class_info)
    return TerrainFail("MovementComponent.FindFloorResult class not resolved");
  if (!metadata.valueType || !metadata.valueType(const_cast<void*>(klass.class_info)))
    return TerrainFail("FindFloorResult value-type metadata cannot be verified");
  // Android host signatures are exact; IL2CPP versions use these three known
  // spellings for a nested type. Every remaining argument stays fully typed.
  for (const char* floorType:{"Beyond.Gameplay.Core.MovementComponent.FindFloorResult",
      "Beyond.Gameplay.Core.MovementComponent/FindFloorResult","Beyond.Gameplay.Core.MovementComponent+FindFloorResult"}) {
    const std::string params=std::string("UnityEngine.Vector3|System.Single|")+floorType+"&|System.Single|System.Boolean|System.Single|System.Boolean";
    terrain.compute=ResolveMethod(kGame,kSmcNs,"MovementComponent","ComputeFloorDist",params.c_str(),"System.Void",7);
    if (terrain.compute) break;
  }
  if (!terrain.compute) for (const char* floorType:{"Beyond.Gameplay.Core.MovementComponent.FindFloorResult",
      "Beyond.Gameplay.Core.MovementComponent/FindFloorResult","Beyond.Gameplay.Core.MovementComponent+FindFloorResult"}) {
    const std::string params=std::string("UnityEngine.Vector3|")+floorType+"&|System.Single";
    terrain.find=ResolveMethod(kGame,kSmcNs,"MovementComponent","FindFloor",params.c_str(),"System.Void",3);
    if (terrain.find) break;
  }
  if (!terrain.compute && !terrain.find) return TerrainFail("TerrainMethodUnavailable: ComputeFloorDist/FindFloor signature unavailable; flat body + camera fallback");
  terrain.clear=ResolveMethod(kGame,kSmcNs,floorClass,"Clear","","System.Void",0);
  terrain.walkable=ResolveMethod(kGame,kSmcNs,floorClass,"IsWalkableFloor","","System.Boolean",0);
  terrain.normal=ResolveMethod(kGame,kSmcNs,floorClass,"get_floorNormal","","UnityEngine.Vector3",0);
  terrain.hitField=ResolveField(kGame,kSmcNs,floorClass,"isHit","System.Boolean");
  terrain.distanceField=ResolveField(kGame,kSmcNs,floorClass,"floorDist","System.Single");
  terrain.rayField=ResolveField(kGame,kSmcNs,floorClass,"floorHit","UnityEngine.RaycastHit");
  terrain.pointField=ResolveField("UnityEngine.PhysicsModule.dll","UnityEngine","RaycastHit","m_Point","UnityEngine.Vector3");
  if (!terrain.clear || !terrain.walkable || !terrain.normal || !terrain.hitField || !terrain.distanceField || !terrain.rayField || !terrain.pointField)
    return TerrainFail("named FindFloorResult/RaycastHit field or value-method contract missing");
  terrain.result=Pin(host->object_new(host->context,klass.class_info));
  if (!terrain.result || !host->object_unbox(host->context,terrain.result.object))
    return TerrainFail("host cannot allocate/pin/unbox FindFloorResult");
  terrain.available=true;terrain.reason.clear();return true;
}
void ResetTerrain() {
  terrain.state={};terrain.initialized=false;terrain.baselineValid=false;
  terrain.rootOffset=0;terrain.sequence=0;terrain.sourceFrame=0;
}
bool FloorSample(VmdVec3 query,float distance,DirectVmdTerrainProbeHit& out) {
  out={};out.query=query;
  if (!terrain.available || !movement || !OnMainThread() || !eiem_source::Finite(query)) return false;
  if (!ValueCall(terrain.clear,terrain.result,nullptr)) return TerrainFail("FindFloorResult.Clear managed exception");
  void* payload=host->object_unbox(host->context,terrain.result.object);
  if (!payload) return TerrainFail("FindFloorResult payload unavailable");
  float radius=.010f,stepDown=distance;
  bool forceWalkable=false,checkClimbable=false;
  void* computeArgs[]={&query,&distance,payload,&radius,&forceWalkable,&stepDown,&checkClimbable};
  void* findArgs[]={&query,payload,&stepDown};
  if (!ManagedCall(terrain.compute?terrain.compute:terrain.find,movement,terrain.compute?computeArgs:findArgs))
    return TerrainFail("MovementComponent floor query managed exception");
  bool hit=false,walkable=false;
  if (!FieldValue(terrain.hitField,terrain.result,hit) || !ValueRead(terrain.walkable,terrain.result,walkable))
    return TerrainFail("FindFloorResult hit/walkable result unreadable");
  if (!hit || !walkable) return false; // A real miss is not an API failure.
  Pin ray=Field(terrain.rayField,terrain.result);
  if (!ray || !FieldValue(terrain.pointField,ray,out.point) || !FieldValue(terrain.distanceField,terrain.result,out.floorDistance) ||
      !ValueRead(terrain.normal,terrain.result,out.normal)) return TerrainFail("FindFloorResult point/distance/normal unreadable");
  VmdVec3 normal{};
  if (!eiem_source::Finite(out.point) || !DirectVmdFinite(out.floorDistance) ||
      !DirectVmdTryNormalizeVector(out.normal,&normal) || normal.y<terrain.config.minimumNormalY) return false;
  out.normal=normal;out.hit=1;return true;
}
VmdVec3 StagePoint(VmdVec3 source) {
  return DirectVmdAdd(DirectVmdAdd(anchor.position,DirectVmdRotateVector(anchor.rotation,source)),placement);
}
VmdVec3 SourcePoint(VmdVec3 world) {
  return DirectVmdRotateVector(DirectVmdQuaternionInverse(anchor.rotation),DirectVmdSub(DirectVmdSub(world,placement),anchor.position));
}
void ApplyTerrain(const DirectVmdSampleFrame& frame,DirectVmdWorldPosePod& pelvis) {
  if (!options.terrain || !PrepareTerrain() || !movement) return;
  const double step=frame.sourceFrame-terrain.sourceFrame;
  if (terrain.initialized && (step<-.001 || step>8)) ResetTerrain();
  terrain.config=DirectVmdBuildTerrainConfig(baseScale*eiem_source::Reference().legLength);
  const float raise=std::clamp(baseScale*eiem_source::Reference().legLength,.5f,2.0f);
  const float sweep=raise*2;
  if (!terrain.baselineValid) {
    DirectVmdTerrainProbeHit baseline{};
    auto position=entryRoot.position;position.y+=raise;
    if (FloorSample(position,sweep,baseline)) {
      terrain.baselineHeight=baseline.point.y;terrain.baselineValid=true;
    }
  }
  DirectVmdTerrainFrameInput input{};
  input.advanceState=(!terrain.initialized || (frame.playback==DirectVmdPlaybackState::Playing && frame.sequence!=terrain.sequence))?1:0;
  input.deltaSeconds=terrain.initialized?float(std::clamp(step/kVmdFramesPerSecond,0.0,.1)):1.0f/60;
  input.ikEnabled[0]=frame.leftFootIkEnabled && frame.bones[size_t(Id::LeftFootIk)].hasTrack;
  input.ikEnabled[1]=frame.rightFootIkEnabled && frame.bones[size_t(Id::RightFootIk)].hasTrack;
  const float radius=std::clamp(baseScale*eiem_source::Reference().legLength*.08f,.025f,.10f);
  const VmdVec3 offsets[]={{0,0,0},{radius,0,0},{-radius,0,0},{0,0,radius},{0,0,-radius}};
  for(int side=0;side<2;++side) {
    const auto foot=size_t(side?Id::RightFootIk:Id::LeftFootIk);
    input.flatFootTarget[side]=StagePoint(poses[foot].position);
    if (!input.ikEnabled[side]) continue;
    DirectVmdTerrainProbeHit samples[DIRECT_VMD_TERRAIN_PROBE_COUNT]{};
    for(size_t index=0;index<DIRECT_VMD_TERRAIN_PROBE_COUNT;++index) {
      auto query=DirectVmdAdd(input.flatFootTarget[side],offsets[index]);query.y+=raise;
      FloorSample(query,sweep,samples[index]);
      if (!terrain.available) return;
    }
    const auto& prior=terrain.state.feet[side];
    input.rawPlane[side]=DirectVmdAggregateTerrainPlane(samples,DIRECT_VMD_TERRAIN_PROBE_COUNT,terrain.config.planeClusterHeight,
        prior.stablePlaneValid,prior.stableHeight,terrain.config.minimumNormalY);
    if (!terrain.initialized && terrain.baselineValid) {
      terrain.state.feet[side].floorReferenceValid=true;
      terrain.state.feet[side].floorReferenceHeight=terrain.baselineHeight;
    }
  }
  DirectVmdTerrainFrameOutput output{};
  DirectVmdUpdateTerrainState(&terrain.state,terrain.config,input,&output);
  terrain.initialized=true;terrain.sequence=frame.sequence;terrain.sourceFrame=frame.sourceFrame;
  terrain.rootOffset=output.rootOffset;
  const auto rootDelta=DirectVmdRotateVector(DirectVmdQuaternionInverse(anchor.rotation),{0,output.rootOffset,0});
  for(auto& pose:poses) pose.position=DirectVmdAdd(pose.position,rootDelta);
  pelvis.position.y+=output.rootOffset;
  for(int side=0;side<2;++side) {
    if (!input.ikEnabled[side]) continue;
    const auto foot=size_t(side?Id::RightFootIk:Id::LeftFootIk);
    poses[foot].position=SourcePoint(output.feet[side].desiredTarget);
    auto toe=StagePoint(DirectVmdSub(poses[foot+1].position,rootDelta));
    toe.y+=output.feet[side].appliedOffset;
    if (input.rawPlane[side].valid) toe.y+=DirectVmdTerrainPlaneHeightAt(input.rawPlane[side].height,
        input.rawPlane[side].normal,input.rawPlane[side].point,toe)-input.rawPlane[side].height;
    poses[foot+1].position=SourcePoint(toe);
    const auto tilt=DirectVmdQuaternionMultiply(DirectVmdQuaternionMultiply(DirectVmdQuaternionInverse(anchor.rotation),
        DirectVmdTerrainNormalRotation(output.feet[side].normal)),anchor.rotation);
    poses[foot].rotation=DirectVmdQuaternionMultiply(tilt,poses[foot].rotation);
  }
}
