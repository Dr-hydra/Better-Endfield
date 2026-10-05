#pragma once
static bool ClothBoneOwnershipGraph(const ClothBoneOwnershipRef &r) {
  auto &s=ClothBoneState();auto process=CollisionGc(r.process);void *container=nullptr,*mesh=nullptr;
  if(!s_clothSurfaceAtBoundary||!process||!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(process),"get_ProxyMeshContainer","BeyondDynamicBone.VirtualMeshContainer"),process,nullptr,container)||
      !container||!CollisionField(container,"shareVirtualMesh","BeyondDynamicBone.VirtualMesh",mesh)||!mesh)return false;
  std::vector<unsigned char> attributes,refs,skin;
  if(!SurfaceReadArray(mesh,"attributes","BeyondDynamicBone.VertexAttribute",1,attributes)||attributes.empty()||attributes.size()>512||
      !SurfaceReadArray(mesh,"referenceIndices","System.Int32",4,refs)||refs.size()!=attributes.size()*4||
      !SurfaceReadArray(mesh,"skinBoneTransformIndices","System.Int32",4,skin)||skin.size()>2048)return false;
  unsigned char fixed=0,move=0,triangle=0;auto ac=SurfaceClass("BeyondDynamicBone","VertexAttribute");
  if(!CollisionByteFlag(ac,"Flag_Fixed",fixed)||!CollisionByteFlag(ac,"Flag_Move",move)||!CollisionByteFlag(ac,"Flag_Triangle",triangle))return false;
  std::vector<int> actual(s.profile->boneCount,-1);std::set<void*> seen;
  const auto get=SurfaceMethod(il2cpp_object_get_class(container),"GetTransformFromIndex","UnityEngine.Transform","System.Int32");
  for(size_t n=0;n<attributes.size();++n){int ref=-1,index=-1;memcpy(&ref,refs.data()+n*4,4);if(ref<0||size_t(ref)*4+4>skin.size())return false;
    memcpy(&index,skin.data()+size_t(ref)*4,4);if(index<0||index>512)return false;void *t=nullptr,*args[]{&index};
    if(!ClothInvoke(get,container,args,t)||!t||!ClothAnchorUnderOwner(t)||!seen.insert(t).second)return false;
    const int b=ClothBoneIndex(t);if(b<0)continue;if(b>=s.profile->boneCount)return false;
    const int flag=attributes[n]&~triangle;if(flag!=0&&flag!=fixed&&flag!=move)return false;actual[b]=flag==fixed?1:flag==move?2:0;
  }
  for(const auto &p:r.proofs)if(p.bone<0||p.bone>=s.profile->boneCount||actual[p.bone]!=p.attribute){
    Log("[CLOTH-AUTO-OWNERSHIP] component=%s peer=%s bone=%d expected=%d actual=%d confirmed=0",s.profile->component,p.component,p.bone,p.attribute,p.bone>=0&&p.bone<s.profile->boneCount?actual[p.bone]:-9);return false;}
  return true;
}
static bool ClothBoneOwnershipIdentity() {
  auto &s=ClothBoneState();if(!s.profile)return s.ownership.empty();
  size_t proofs=0;
  for(const auto &r:s.ownership){auto bbc=ClothTarget(r.bbc);void *process=nullptr,*data=nullptr,*data2=nullptr,*roots=nullptr;int team=0;
    if(!bbc||!ClothAnchorUnderOwner(CollisionTransform(bbc))||
        !CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",process)||process!=CollisionGc(r.process)||
        !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data)||data!=CollisionGc(r.data)||
        !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"GetSerializeData2","BeyondDynamicBone.ClothSerializeData2"),bbc,nullptr,data2)||data2!=CollisionGc(r.data2)||
        !CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots)||roots!=CollisionGc(r.roots)||CollisionCount(roots)!=int(r.rootRefs.size())||
        !ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"get_TeamId","System.Int32"),process,team)||team!=r.team||!ClothBoneTeamRegistered(process,team))return false;
    for(size_t n=0;n<r.rootRefs.size();++n)if(CollisionItem(roots,int(n),"UnityEngine.Transform")!=ClothTarget(r.rootRefs[n]))return false;
    for(const auto &proof:r.proofs)if(proof.zeroRotationAnchor){
      if(proof.bone<0||proof.bone>=s.profile->boneCount||proof.attribute!=1)return false;
      const auto t=ClothTarget(s.bones[proof.bone].bone);bool root=false;for(const auto &v:r.rootRefs)root|=ClothTarget(v)==t;
      float serialized=-1,effective=-1;void *manager=nullptr,*parameters=nullptr;ClothInputArray values{};char mode[32]{};
      if(!root||!ClothField(data,"rootRotation","System.Single",serialized)||serialized!=0||
          !CollisionEnum(data,"connectionMode","BeyondDynamicBone.RenderSetupData.BoneConnectionMode",mode,sizeof(mode))||strcmp(mode,"Line")||
          !ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager)||
          !ClothInputArrayOpen(manager,"parameterArray","BeyondDynamicBone.ClothParameters",values)||!(parameters=ClothInputArrayBox(values,team))||
          !ClothInputTeamField(parameters,"rootRotation","System.Single",effective)||effective!=0)return false;
    }
    if(!ClothBoneOwnershipGraph(r))return false;proofs+=r.proofs.size();
  }
  return proofs==size_t(s.profile->ownershipCount);
}
static bool ClothBoneCaptureOwnership() {
  auto &s=ClothBoneState();const auto &profile=*s.profile;if(!s.ownership.empty())return false;
  if(profile.ownershipCount<0||profile.ownershipCount>1024||(!profile.runtimeGenerated&&profile.ownershipCount))return false;
  for(int n=0;n<profile.ownershipCount;++n){const auto &proof=profile.ownership[n];size_t slot=0;
    while(slot<s.ownership.size()&&strcmp(s.ownership[slot].proofs.front().component,proof.component))++slot;
    if(slot==s.ownership.size()){
      if(slot>=16)return false;void *bbc=nullptr;for(int k=0;k<s_cloth.count;++k)if(!strcmp(s_cloth.instances[k].name,proof.component)){if(bbc)return false;bbc=ClothTarget(s_cloth.instances[k].ref);}
      void *process=nullptr,*data=nullptr,*data2=nullptr,*roots=nullptr;
      if(!bbc||bbc==ClothTarget(s.bbc)||!CollisionField(bbc,"process","BeyondDynamicBone.ClothProcess",process)||!process||
          !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data)||!data||
          !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"GetSerializeData2","BeyondDynamicBone.ClothSerializeData2"),bbc,nullptr,data2)||!data2||
          !CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots))return false;
      const int count=CollisionCount(roots);if(count<1||count>64)return false;s.ownership.emplace_back();auto &r=s.ownership.back();
      r.bbc=ClothProtect(bbc);r.process=ClothBoneHold(process);r.data=ClothBoneHold(data);r.data2=ClothBoneHold(data2);r.roots=ClothBoneHold(roots);
      if(!r.bbc.handle||!r.process||!r.data||!r.data2||!r.roots||!ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"get_TeamId","System.Int32"),process,r.team))return false;
      for(int k=0;k<count;++k){auto t=CollisionItem(roots,k,"UnityEngine.Transform");if(!t||!ClothAnchorUnderOwner(t))return false;r.rootRefs.push_back(ClothProtect(t));if(!r.rootRefs.back().handle)return false;}
    }
    s.ownership[slot].proofs.push_back(proof);
  }
  if(!ClothBoneOwnershipIdentity())return false;
  for(const auto &r:s.ownership){int inputs=0;for(const auto &proof:r.proofs)inputs+=proof.zeroRotationAnchor;
    Log("[CLOTH-AUTO-OWNERSHIP] component=%s peer=%s Process=%p Team=%d identities=%zu confirmed=1 zeroDirectionFixedInputs=%d effectiveRootRotationConfirmed=%d peerConfigWrites=0 fixedNativeRotationOutputRetained=1",profile.component,r.proofs.front().component,CollisionGc(r.process),r.team,r.proofs.size(),inputs,inputs>0);}
  return true;
}
static bool ClothBoneOwnershipProved(void *bbc,int bone) {
  for(const auto &r:ClothBoneState().ownership)if(ClothTarget(r.bbc)==bbc)for(const auto &p:r.proofs)if(p.bone==bone)return true;
  return false;
}
static bool ClothBoneOwnershipInputRoot(void *bbc,void *root) {
  const auto &s=ClothBoneState();
  if(!bbc||!root||!s.profile||!s.profile->runtimeGenerated)return false;
  for(const auto &r:s.ownership)if(ClothTarget(r.bbc)==bbc) {
    if(std::none_of(r.rootRefs.begin(),r.rootRefs.end(),[&](const ClothRef &v){return ClothTarget(v)==root;}))return false;
    for(const auto &p:r.proofs)if(p.zeroRotationAnchor&&p.attribute==1&&p.bone>=0&&p.bone<s.profile->boneCount&&
        size_t(p.bone)<s.bones.size()&&s.profile->bones[p.bone].attribute==2&&ClothTarget(s.bones[p.bone].bone)==root)return true;
  }
  return false;
}
