#pragma once
static bool ClothBonePrebuildFailure(const char *reason,void *process=nullptr) {
  const auto &s=ClothBoneState();
  Log("[CLOTH-BONE-PREBUILD] stage=unconfirmed component=%s generation=%llu frame=%d Process=%p reason=%s",
      s.profile?s.profile->component:"pending",s.owner.generation,ClothFrame(),process,reason);return false;
}
static bool ClothBonePrebuildFlag(void *process,bool wanted) {
  if(!process||!il2cpp_field_static_get_value||!il2cpp_field_get_flags)return false;
  auto cls=il2cpp_object_get_class(process);
  auto field=CollisionFieldInfo(cls,"State_UsePreBuild","System.Int32");int bit=-1;void *box=nullptr;
  if(!field||(il2cpp_field_get_flags(field)&0x50)!=0x50)return false;
  il2cpp_field_static_get_value(field,&bit);uint32_t alignment=0,flags=0;
  if(bit<0||bit>=32||!ClothInvoke(SurfaceMethod(cls,"GetStateFlag","Unity.Collections.BitField32"),process,nullptr,box)||!box||
      !CollisionType(il2cpp_class_get_type(il2cpp_object_get_class(box)),"Unity.Collections.BitField32")||
      il2cpp_class_value_size(il2cpp_object_get_class(box),&alignment)!=4)return false;
  memcpy(&flags,(char*)box+16,4);return bool(flags&(uint32_t(1)<<bit))==wanted;
}
static bool ClothBonePrebuildPrivate(void *data2) {
  void *pb=nullptr,*shared=nullptr;bool enabled=true;
  return CollisionField(data2,"preBuildData","BeyondDynamicBone.PreBuildSerializeData",pb)&&pb&&
      ClothValue(SurfaceMethod(il2cpp_object_get_class(pb),"UsePreBuild","System.Boolean"),pb,enabled)&&!enabled&&
      CollisionField(pb,"preBuildData","BeyondDynamicBone.SharePreBuildData",shared)&&!shared;
}
static bool ClothBonePrebuildIdentity() {
  auto &s=ClothBoneState();auto &p=s.prebuild;if(!p.captured)return !s.profile->prebuildId;
  void *pb=nullptr,*shared=nullptr,*unique=nullptr,*id=nullptr;bool enabled=false;char name[128]{};
  return CollisionField(CollisionGc(s.data2),"preBuildData","BeyondDynamicBone.PreBuildSerializeData",pb)&&pb==CollisionGc(p.data)&&
      ClothValue(SurfaceMethod(il2cpp_object_get_class(pb),"UsePreBuild","System.Boolean"),pb,enabled)&&enabled&&
      CollisionField(pb,"preBuildData","BeyondDynamicBone.SharePreBuildData",shared)&&shared==CollisionGc(p.shared)&&
      CollisionField(pb,"uniquePreBuildData","BeyondDynamicBone.UniquePreBuildData",unique)&&unique==CollisionGc(p.unique)&&
      CollisionField(shared,"buildId","System.String",id)&&id&&ReadStrUtf8(id,name,sizeof(name))>0&&
      s.profile->prebuildId&&!strcmp(name,s.profile->prebuildId);
}
static bool ClothBonePrebuildCount(int &count,void **proxy=nullptr) {
  auto &p=ClothBoneState().prebuild;void *manager=nullptr,*dict=nullptr,*value=nullptr,*box=nullptr;
  if(!p.captured||!ClothBonePrebuildIdentity()||
      !ClothContactManager("get_PreBuild","BeyondDynamicBone.PreBuildManager",manager)||manager!=CollisionGc(p.manager)||
      !CollisionField(manager,"deserializationDict","System.Collections.Generic.Dictionary<BeyondDynamicBone.SharePreBuildData,BeyondDynamicBone.PreBuildManager.ShareDeserializationData>",dict)||!dict)return false;
  auto cls=il2cpp_object_get_class(dict);void *args[]{CollisionGc(p.shared)};
  if(!args[0]||!ClothInvoke(SurfaceMethod(cls,"ContainsKey","System.Boolean","BeyondDynamicBone.SharePreBuildData"),dict,args,box)||!box)return false;
  if(!UnboxBool(box)){count=0;if(proxy)*proxy=nullptr;return true;}
  if(!ClothInvoke(SurfaceMethod(cls,"get_Item","BeyondDynamicBone.PreBuildManager.ShareDeserializationData","BeyondDynamicBone.SharePreBuildData"),dict,args,value)||!value||
      !CollisionField(value,"referenceCount","System.Int32",count)||count<0||count>100000)return false;
  return !proxy||CollisionField(value,"proxyMesh","BeyondDynamicBone.VirtualMesh",*proxy);
}
static bool ClothBonePrebuildCapture() {
  auto &s=ClothBoneState();void *pb=nullptr,*shared=nullptr,*unique=nullptr,*manager=nullptr,*box=nullptr;bool enabled=true;
  if(!CollisionField(CollisionGc(s.data2),"preBuildData","BeyondDynamicBone.PreBuildSerializeData",pb)||!pb||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(pb),"UsePreBuild","System.Boolean"),pb,enabled))return false;
  if(!enabled)return !s.profile->prebuildId;
  if(!s.profile->runtimeGenerated||!s.profile->prebuildId||s.profile->inputAnchorCount||
      !ClothBoneMethodFingerprint("ClothProcess","DisposeInternal","System.Void",nullptr,3879,0x7cb2111423aaa79dULL)||
      !ClothBoneMethodFingerprint("ClothProcess","PreBuildDataConstruction","System.Boolean",nullptr,9116,0x633489c10d63b621ULL)||
      !ClothBoneMethodFingerprint("TransformData","ShareDeserialize","BeyondDynamicBone.TransformData","BeyondDynamicBone.TransformData.ShareSerializationData",382,0x66e3bf2c6f3b93fbULL,nullptr,true)||
      !ClothBoneMethodFingerprint("PreBuildManager","RegisterPreBuildData","BeyondDynamicBone.PreBuildManager.ShareDeserializationData","BeyondDynamicBone.SharePreBuildData",318,0x7e1c14bbaae8fa7dULL,"System.Boolean")||
      !ClothBoneMethodFingerprint("PreBuildManager","UnregisterPreBuildData","System.Void","BeyondDynamicBone.SharePreBuildData",144,0x98c281b022fd8f9dULL)||
      !CollisionField(pb,"preBuildData","BeyondDynamicBone.SharePreBuildData",shared)||!shared||
      !CollisionField(pb,"uniquePreBuildData","BeyondDynamicBone.UniquePreBuildData",unique)||!unique||
      !ClothContactManager("get_PreBuild","BeyondDynamicBone.PreBuildManager",manager)||!manager||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(pb),"DataValidate","BeyondDynamicBone.ResultCode"),pb,nullptr,box)||!box)return false;
  uint32_t alignment=0;bool success=false;auto cls=il2cpp_object_get_class(box);
  if(!CollisionType(il2cpp_class_get_type(cls),"BeyondDynamicBone.ResultCode")||il2cpp_class_value_size(cls,&alignment)!=8||
      !ClothValue(SurfaceMethod(cls,"IsSuccess","System.Boolean"),(char*)box+16,success)||!success)return false;
  auto &p=s.prebuild;p.data=ClothBoneHold(pb);p.shared=ClothBoneHold(shared);p.unique=ClothBoneHold(unique);p.manager=ClothBoneHold(manager);
  p.captured=p.data&&p.shared&&p.unique&&p.manager;int references=-1;
  if(!p.captured||!ClothBonePrebuildIdentity()||!ClothBonePrebuildCount(references)||references<1||
      !ClothBonePrebuildFlag(CollisionGc(s.process[0]),true))return false;
  Log("[CLOTH-BONE-PREBUILD] stage=captured component=%s generation=%llu source=%p buildId=%s references=%d sourceImmutable=1 candidate=fresh-private restore=original-cache",
      s.profile->component,s.owner.generation,shared,s.profile->prebuildId,references);return true;
}
static bool ClothBonePrebuildLocals(void *td,int count,std::vector<unsigned char> &positions,std::vector<unsigned char> &rotations) {
  if(!td||count<1||count>129)return false;
  const bool p=SurfaceReadArray(td,"initLocalPositionArray","Unity.Mathematics.float3",12,positions);
  const bool q=SurfaceReadArray(td,"initLocalRotationArray","Unity.Mathematics.quaternion",16,rotations);
  const bool ok=p&&q&&positions.size()==size_t(count)*12&&rotations.size()==size_t(count)*16;
  if(!ok)Log("[CLOTH-BONE-PREBUILD-ARRAY] expectedTransforms=%d positionRead=%d positionBytes=%zu rotationRead=%d rotationBytes=%zu workScaleArrayRequired=0",
      count,p,positions.size(),q,rotations.size());
  return ok;
}
static bool ClothBonePrebuildReference(void *process,bool capture) {
  auto &s=ClothBoneState();void *container=nullptr,*vm=nullptr,*td=nullptr,*registered=nullptr;
  int count=0,references=0;
  if(!s_clothSurfaceAtBoundary||!ClothOnMainThread()||!ClothBonePrebuildIdentity()||
      !ClothBonePrebuildCount(references,&registered)||references<1||!registered||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(process),"get_ProxyMeshContainer","BeyondDynamicBone.VirtualMeshContainer"),process,nullptr,container)||!container||
      !CollisionField(container,"shareVirtualMesh","BeyondDynamicBone.VirtualMesh",vm)||vm!=registered||
      !CollisionField(vm,"transformData","BeyondDynamicBone.TransformData",td)||!td||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(container),"GetTransformCount","System.Int32"),container,count)||
      count!=s.profile->OriginalCount()+1||count>129)return ClothBonePrebuildFailure("native-proxy-registration-or-transform-count",process);
  std::vector<unsigned char> positions,rotations;
  if(!ClothBonePrebuildLocals(td,count,positions,rotations))return ClothBonePrebuildFailure("native-proxy-reference-array-layout",process);
  if(capture) {
    float inverse[16]{};
    if(!CollisionField(vm,"initWorldToLocal","Unity.Mathematics.float4x4",inverse))return false;
    for(int k=0;k<16;++k)s.referenceInverse.v[k]=inverse[k];
    if(!eiem_cloth_rebuild::UniformPositive(s.referenceInverse))return false;
  }
  eiem_cloth_surface::OutputMatrix cloth{};
  if(!SurfaceOutputMatrix(ClothTarget(s.transform),cloth)||!eiem_cloth_rebuild::UniformPositive(cloth))
    return ClothBonePrebuildFailure("native-proxy-current-cloth-scale",process);
  std::vector<bool> seen(s.profile->boneCount);int center=0;
  for(int n=0;n<count;++n) {
    void *bone=nullptr,*args[]{&n};
    if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(container),"GetTransformFromIndex","UnityEngine.Transform","System.Int32"),container,args,bone)||!bone)return false;
    if(bone==ClothTarget(s.transform)){++center;continue;}
    auto parent=CollisionParent(bone);char name[128]{},pn[128]{};CollisionName(bone,name,sizeof(name));CollisionName(parent,pn,sizeof(pn));int b=-1;
    for(int k=0;k<s.profile->boneCount;++k)if(!strcmp(name,s.profile->bones[k].name)&&!strcmp(pn,s.profile->bones[k].parentName)){if(b>=0)return false;b=k;}
    if(b<0||seen[b]||!ClothAnchorUnderOwner(bone))return false;seen[b]=true;auto &r=s.bones[b];const auto &asset=s.profile->bones[b];
    Vector3 lp{},scale{},live{},localScale{};Quaternion lq{},liveQ{};
    memcpy(&lp,positions.data()+12*n,12);memcpy(&lq,rotations.data()+16*n,16);
    eiem_cloth_surface::OutputMatrix world{};double normalized[3]{};
    if(!SurfaceOutputMatrix(bone,world)||!eiem_cloth_rebuild::PrebuildReferenceScale(s.referenceInverse,cloth,world,normalized))
      return ClothBonePrebuildFailure("native-proxy-current-bone-scale",process);
    scale={float(normalized[0]),float(normalized[1]),float(normalized[2])};
    if(!ClothSameLocal(lp,lq,asset.position,asset.rotation)||
        !SurfaceVisiblePose(bone,live,liveQ,localScale)||!SurfaceVisibleSame(localScale,asset.scale)) {
      Log("[CLOTH-BONE-PREBUILD-REFERENCE] bone=%s cachedLocal=%g,%g,%g assetLocal=%g,%g,%g normalizedWorldScale=%g,%g,%g localScale=%g,%g,%g",
          name,lp.x,lp.y,lp.z,asset.position.x,asset.position.y,asset.position.z,scale.x,scale.y,scale.z,localScale.x,localScale.y,localScale.z);
      return ClothBonePrebuildFailure("natural-local-pose-or-scale",process);
    }
    if(capture) {
      r.bone=ClothProtect(bone);r.parent=ClothProtect(parent);if(!r.bone.handle||!r.parent.handle)return false;
      r.local=lp;r.rotation=lq;r.referenceScale=scale;
    } else if(ClothTarget(r.bone)!=bone||ClothTarget(r.parent)!=parent||!ClothSameLocal(lp,lq,r.local,r.rotation)||
        !SurfaceVisibleSame(scale,r.referenceScale))return false;
  }
  if(center!=1||!std::all_of(seen.begin(),seen.end(),[](bool v){return v;}))return false;
  for(int b=0;b<s.profile->boneCount;++b){const int parent=s.profile->bones[b].parent;
    if(parent>=0&&(parent>=b||ClothTarget(s.bones[b].parent)!=ClothTarget(s.bones[parent].bone)))return false;}
  Log("[CLOTH-BONE-PREBUILD] stage=reference component=%s capture=%d Process=%p transforms=%d nativeSharedProxyConfirmed=1 cachedLocalPoseConfirmed=1 scaleSource=current-matrices-normalized-to-cached-BBC workScaleArrayRequired=0",
      s.profile->component,capture,process,count);return true;
}
