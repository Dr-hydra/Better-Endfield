#pragma once
static const ClothBoneRuntime *ClothBoneResponseSheet(const ClothBoneRuntime &s,const ClothBoneNativeProducer &p) {
  if(s.contactPartner<0||s.contactPartner>=s_clothBoneCount)return nullptr;
  const auto &peer=s_clothBoneSlots[s.contactPartner];
  return ClothBoneRibbonPair(peer,s)&&ClothBonePair(peer,s)&&peer.pending&&peer.lease&&!peer.stopRequested&&!peer.failed&&!peer.tx.cancelled&&peer.teamModeConfirmed&&
      peer.tx.phase==eiem_cloth_rebuild::Phase::Active&&peer.graph[1]&&peer.reference[1]&&peer.bbc.id==p.bbc.id&&
      CollisionGc(peer.process[1])==CollisionGc(p.process)&&CollisionGc(peer.candidateData)==CollisionGc(p.data)&&peer.team[1]==p.team?&peer:nullptr;
}
static bool ClothBoneResponseCaptureOutputs(const ClothBoneNativeProducer &p) {
  auto &s=ClothBoneState();auto &l=s.local.response;if(!s.local.recipe)return false;const auto &r=*s.local.recipe;
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1||
      !ClothOwns(s.owner)||s.stopRequested||r.responsePointCount!=5||!r.responsePoints||
      !l.outputBones.empty())return false;
  const auto *sheet=ClothBoneResponseSheet(s,p);if(p.rootRefs.size()!=size_t(sheet?5:1))return false;
  void *container=nullptr,*mesh=nullptr;
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(CollisionGc(p.process)),"get_ProxyMeshContainer","BeyondDynamicBone.VirtualMeshContainer"),CollisionGc(p.process),nullptr,container)||!container||
      !CollisionField(container,"shareVirtualMesh","BeyondDynamicBone.VirtualMesh",mesh)||!mesh)return false;
  std::vector<unsigned char> attributes,refs,skin;
  if(!SurfaceReadArray(mesh,"attributes","BeyondDynamicBone.VertexAttribute",1,attributes)||attributes.empty()||attributes.size()>32||
      !SurfaceReadArray(mesh,"referenceIndices","System.Int32",4,refs)||refs.size()!=attributes.size()*4||
      !SurfaceReadArray(mesh,"skinBoneTransformIndices","System.Int32",4,skin)||skin.size()>128)return false;
  auto ac=SurfaceClass("BeyondDynamicBone","VertexAttribute");unsigned char fixed=0,move=0,triangle=0;
  if(!CollisionByteFlag(ac,"Flag_Fixed",fixed)||!CollisionByteFlag(ac,"Flag_Move",move)||!CollisionByteFlag(ac,"Flag_Triangle",triangle))return false;
  l.outputBones.resize(5);l.outputAttributes=attributes;std::set<void*> seen;
  for(size_t n=0;n<attributes.size();++n){const int flag=attributes[n]&~triangle;if(!flag)continue;
    if(flag!=fixed&&flag!=move)return false;int ref=-1,index=-1;memcpy(&ref,refs.data()+4*n,4);
    if(ref<0||size_t(ref)*4+4>skin.size())return false;memcpy(&index,skin.data()+size_t(ref)*4,4);if(index<0||index>32)return false;
    void *t=nullptr,*args[]{&index};
    if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(container),"GetTransformFromIndex","UnityEngine.Transform","System.Int32"),container,args,t)||
        !t||!ClothAnchorUnderOwner(t)||!seen.insert(t).second)return false;
    char name[128]{},parent[128]{};CollisionName(t,name,sizeof(name));CollisionName(CollisionParent(t),parent,sizeof(parent));int found=-1;
    for(int k=0;k<5;++k)if(!strcmp(name,r.responsePoints[k].name)&&!strcmp(parent,r.responsePoints[k].parent))found=k;
    if(found<0){bool width=false;
      if(sheet)for(size_t j=0;j<sheet->bones.size();++j)if(t==ClothTarget(sheet->bones[j].bone)&&ClothBoneCandidate(*sheet).bones[j].attribute==(flag==fixed?1:2))width=true;
      if(!width)return false;continue;
    }
    if(l.outputBones[found].handle||r.responsePoints[found].attribute!=(flag==fixed?1:2))return false;
    l.outputBones[found]=ClothProtect(t);if(!l.outputBones[found].handle)return false;
    l.outputVertices[found]=int(n);l.outputTransforms[found]=index;
  }
  if(std::none_of(p.rootRefs.begin(),p.rootRefs.end(),[&](const ClothRef &root){return ClothTarget(l.outputBones[0])==ClothTarget(root);}))return false;
  for(int k=1;k<5;++k)if(!ClothTarget(l.outputBones[k])||CollisionParent(ClothTarget(l.outputBones[k]))!=ClothTarget(l.outputBones[k-1]))return false;
  l.outputContainer=ClothBoneHold(container);l.outputMesh=ClothBoneHold(mesh);l.outputConfirmed=l.outputContainer&&l.outputMesh;
  if(l.outputConfirmed)Log("[CLOTH-RIBBON-LAYER] stage=source-output-confirmed consumer=%s trackedCenterPoints=5 activeSourcePoints=%zu storageSlots=%zu pairedWidthSheet=%d sourceOutputWrites=0 outerTeam=%d",r.responseConsumer,seen.size(),l.outputAttributes.size(),int(sheet!=nullptr),p.team);
  return l.outputConfirmed;
}
static bool ClothBoneResponsePointSurface(const ClothBoneRuntime &s,const ClothBoneNativeProducer &p,void *team,eiem_cloth_layer::Surface &out,const char **issue=nullptr) {
  const auto stage=[&](const char *reason){if(issue)*issue=reason;};stage("ribbon-source-identity-unconfirmed");
  if(!ClothOnMainThread()||s_clothInputUpdateDepth!=1)return false;
  const auto &l=s.local.response;void *container=nullptr,*mesh=nullptr,*transforms=nullptr,*simulation=nullptr,*vm=nullptr;
  if(!l.outputConfirmed||l.outputBones.size()!=5||l.outputAttributes.size()<5||l.outputAttributes.size()>32||!ClothBoneNativeProducerIdentity(s,p)||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(CollisionGc(p.process)),"get_ProxyMeshContainer","BeyondDynamicBone.VirtualMeshContainer"),CollisionGc(p.process),nullptr,container)||container!=CollisionGc(l.outputContainer)||
      !CollisionField(container,"shareVirtualMesh","BeyondDynamicBone.VirtualMesh",mesh)||mesh!=CollisionGc(l.outputMesh)||
      !ClothContactManager("get_Bone","BeyondDynamicBone.DynamicBoneTransformManager",transforms)||
      !ClothContactManager("get_Simulation","BeyondDynamicBone.SimulationManager",simulation)||
      !ClothContactManager("get_VMesh","BeyondDynamicBone.VirtualMeshManager",vm))return false;
  uintptr_t access=0;int length=0;void *get=nullptr;ClothInputArray owners{},transformOwners{},proxyOwners{},attributes{};
  ClothInputChunk particles{},proxy{},tc{},selfPoints{},selfTriangles{};
  stage("ribbon-native-buffer-or-storage-chunk-unconfirmed");
  const int slots=int(l.outputAttributes.size());const auto *sheet=ClothBoneResponseSheet(s,p);
  if(!ClothBoneSolverAccess(transforms,access,length,get)||
      !ClothInputArrayOpen(simulation,"teamIdArray","System.Int16",owners)||
      !ClothInputArrayOpen(transforms,"teamIdArray","System.Int16",transformOwners)||
      !ClothInputArrayOpen(vm,"teamIds","System.Int16",proxyOwners)||
      !ClothInputArrayOpen(vm,"attributes","BeyondDynamicBone.VertexAttribute",attributes)||
      !ClothInputChunkRead(team,"particleChunk",owners.length,particles)||particles.count!=slots||
      !ClothInputChunkRead(team,"proxyCommonChunk",proxyOwners.length,proxy)||proxy.count!=slots||
      !ClothInputChunkRead(team,"proxyTransformChunk",length,tc,33)||tc.count<5||
      !ClothInputTeamField(team,"selfPointChunk","BeyondDynamicBone.DataChunk",selfPoints)||selfPoints.count!=slots||
      !ClothInputTeamField(team,"selfTriangleChunk","BeyondDynamicBone.DataChunk",selfTriangles)||selfTriangles.count!=(sheet?ClothBoneCandidate(*sheet).FaceCount():0))return false;
  unsigned char fixed=0,move=0,triangle=0;auto ac=SurfaceClass("BeyondDynamicBone","VertexAttribute");
  stage("ribbon-attribute-ABI-unconfirmed");
  if(!CollisionByteFlag(ac,"Flag_Fixed",fixed)||!CollisionByteFlag(ac,"Flag_Move",move)||!CollisionByteFlag(ac,"Flag_Triangle",triangle))return false;
  std::vector<unsigned char> sourceAttributes,refs,skin;
  stage("ribbon-source-arrays-changed");
  if(!SurfaceReadArray(mesh,"attributes","BeyondDynamicBone.VertexAttribute",1,sourceAttributes)||sourceAttributes!=l.outputAttributes||
      !SurfaceReadArray(mesh,"referenceIndices","System.Int32",4,refs)||refs.size()!=size_t(slots)*4||
      !SurfaceReadArray(mesh,"skinBoneTransformIndices","System.Int32",4,skin)||skin.size()>128)return false;
  if(sheet){
    stage("ribbon-sheet-native-output-map-unconfirmed");
    if(sheet->registeredVertices.size()!=size_t(slots)||slots!=ClothBoneCandidate(*sheet).EffectiveCount())return false;
    for(int n=0;n<slots;++n){int16_t a=0,b=0,c=0;unsigned char flag=0;int ref=-1,index=-1;
      const int asset=sheet->registeredVertices[n];if(asset<0||size_t(asset)>=sheet->bones.size())return false;
      const int expected=ClothBoneCandidate(*sheet).bones[asset].attribute;
      if(!ClothInputArrayValue(owners,particles.start+n,"System.Int16",&a,2)||a!=p.team||
          !ClothInputArrayValue(proxyOwners,proxy.start+n,"System.Int16",&c,2)||c!=a||
          !ClothInputArrayValue(attributes,proxy.start+n,"BeyondDynamicBone.VertexAttribute",&flag,1)||
          (flag&~triangle)!=(expected==1?fixed:expected==2?move:0)||!(flag&~triangle)||
          (flag&~triangle)!=(l.outputAttributes[n]&~triangle))return false;
      memcpy(&ref,refs.data()+size_t(n)*4,4);if(ref<0||size_t(ref)*4+4>skin.size())return false;
      memcpy(&index,skin.data()+size_t(ref)*4,4);if(index<0||index>=tc.count)return false;
      int slot=tc.start+index;void *t=nullptr,*args[]{&slot};
      if(!ClothInvoke(get,&access,args,t)||t!=ClothTarget(sheet->bones[asset].bone)||!t||CollisionParent(t)!=ClothTarget(sheet->bones[asset].parent)||
          !ClothInputArrayValue(transformOwners,slot,"System.Int16",&b,2)||b!=a)return false;
      for(int k=0;k<5;++k)if(l.outputVertices[k]==n&&(t!=ClothTarget(l.outputBones[k])||index!=l.outputTransforms[k]))return false;
    }
    std::vector<eiem_cloth_layer::Face> faces;const auto &r=*sheet->local.recipe;
    if(!r.responseFaces||r.responseFaceCount<1||r.responseFaceCount>ClothBoneMaxFaceChoices)return false;
    for(int n=0;n<r.responseFaceCount;++n)faces.push_back({r.responseFaces[n].ids,r.responseFaces[n].outside});
    if(!ClothBoneLayerSurface(*sheet,particles,faces.data(),faces.size(),out))return false;stage(nullptr);return true;
  }
  out={};out.team=p.team;out.start=particles.start;out.count=slots;out.pointsOnly=true;out.activeCount=5;
  std::array<bool,5> found{};
  for(int n=0;n<slots;++n){int16_t a=0,c=0;unsigned char flag=0;
    stage("ribbon-native-attribute-or-Team-mismatch");
    if(!ClothInputArrayValue(owners,particles.start+n,"System.Int16",&a,2)||a!=p.team||
        !ClothInputArrayValue(proxyOwners,proxy.start+n,"System.Int16",&c,2)||c!=a||
        !ClothInputArrayValue(attributes,proxy.start+n,"BeyondDynamicBone.VertexAttribute",&flag,1)||
        (flag&~triangle)!=(l.outputAttributes[n]&~triangle))return false;
    int k=-1;for(int j=0;j<5;++j)if(l.outputVertices[j]==n){if(k>=0)return false;k=j;}
    if(!(flag&~triangle)){if(k>=0)return false;continue;}
    if(k<0||found[k]||(flag&~triangle)!=(k?move:fixed))return false;
    int ref=-1,transform=-1;memcpy(&ref,refs.data()+size_t(n)*4,4);
    stage("ribbon-output-reference-map-mismatch");
    if(ref<0||size_t(ref)*4+4>skin.size())return false;memcpy(&transform,skin.data()+size_t(ref)*4,4);
    if(transform!=l.outputTransforms[k]||transform<0||transform>=tc.count)return false;
    int index=tc.start+transform;int16_t b=0;void *t=nullptr,*args[]{&index};
    if(!ClothInvoke(get,&access,args,t)||!t||t!=ClothTarget(l.outputBones[k])||
        !ClothInputArrayValue(transformOwners,index,"System.Int16",&b,2)||b!=a)return false;
    found[k]=true;out.active[k]=particles.start+n;
  }
  for(bool ok:found)if(!ok)return false;
  std::sort(out.active.begin(),out.active.begin()+5);if(!eiem_cloth_layer::Valid(out))return false;stage(nullptr);return true;
}
static bool ClothBoneResponseLayerPrepare(ClothBoneRuntime &s) {
  auto &l=s.local;auto &response=l.response;if(!l.recipe)return false;const auto &r=*l.recipe;
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1||!ClothOwns(s.owner)||
      s.stopRequested||s.tx.cancelled||!s_clothLayerInstalled.load(std::memory_order_acquire)||!l.contactConfirmed||
      l.contactProducer<0||size_t(l.contactProducer)>=s.nativeProducers.size()||!r.responseFaces||r.responseFaceCount<1||r.responseFaceCount>ClothBoneMaxFaceChoices)return false;
  const auto &p=s.nativeProducers[l.contactProducer];void *inner=nullptr,*outer=nullptr,*manager=CollisionGc(l.contactManager);
  ClothInputChunk chunk{};eiem_cloth_contact_job::Container list{};
  if(!ClothBoneContactTeam(s.team[1],CollisionGc(s.process[1]),inner)||!ClothBoneContactTeam(p.team,CollisionGc(p.process),outer)||
      !ClothInputTeamField(inner,"particleChunk","BeyondDynamicBone.DataChunk",chunk)||
      !manager||!ClothField(manager,"pointTriangleContactList","Unity.Collections.NativeList<BeyondDynamicBone.SelfCollisionConstraint.PointTriangleContact>",list)||list.data!=l.contactListHeaders[1])return false;
  eiem_cloth_layer::Policy policy{};
  policy.ticket={s.owner.session,s.owner.generation,s.command,{uint64_t(uintptr_t(CollisionGc(s.process[1]))),uint64_t(uintptr_t(CollisionGc(p.process)))},
      {uint64_t(uintptr_t(CollisionGc(s.candidateData))),uint64_t(uintptr_t(CollisionGc(p.data)))}};policy.list=list.data;
  std::array<eiem_cloth_layer::Face,ClothBoneMaxFaceChoices> faces{};for(int n=0;n<r.responseFaceCount;++n)faces[n]={r.responseFaces[n].ids,r.responseFaces[n].outside};
  if(!ClothBoneLayerSurface(s,chunk,faces.data(),r.responseFaceCount,policy.inner)||
      !ClothBoneResponsePointSurface(s,p,outer,policy.outer,&l.readbackIssue)||!s_clothLayerGate.Publish(policy))return false;
  if(!response.layerArmed)Log("[CLOTH-RIBBON-LAYER] stage=armed frame=%d session=%llu generation=%llu command=%u innerTeam=%d outerTeam=%d innerFaces=%d outerPoints=%d outerStorageSlots=%d direction=outer-points-outside-inner-surface outerFaces=%d outerBBC=native-original geometryWrites=0 innerContactMass=1 nativeContactReadback=confirmed visualVerified=0",
      ClothFrame(),s.owner.session,s.owner.generation,s.command,s.team[1],p.team,policy.inner.faceCount,policy.outer.pointsOnly?policy.outer.activeCount:policy.outer.count,policy.outer.count,policy.outer.faceCount);
  response.layerArmed=true;return true;
}
