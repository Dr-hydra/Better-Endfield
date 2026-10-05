#pragma once
static bool ClothFinishReject(const char *why) {
  strncpy_s(s_clothFinishIssue,why,_TRUNCATE);
  Log("[CLOTH-CONTACT-FINISH] stage=install-refused reason=%s baselineRetained=1",why);return false;
}
static void *ClothFinishNested(void *parent,const char *name) {
  if(!parent||!il2cpp_class_get_nested_types)return nullptr;
  char full[192]{};_snprintf_s(full,_TRUNCATE,"BeyondDynamicBone.ColliderCollisionConstraint.%s",name);
  void *found=nullptr;
  for(void *it=nullptr,*c=nullptr;(c=il2cpp_class_get_nested_types(parent,&it));)
    if(!strcmp(il2cpp_class_get_name(c),name)&&CollisionType(il2cpp_class_get_type(c),full)) {
      if(found)return nullptr;found=c;
    }
  return found;
}
static void *ClothFinishMethod(void *cls,const char *name,const char *ret,bool isStatic,const char *const *args,unsigned count) {
  void *found=nullptr;
  for(void *it=nullptr,*m=nullptr;cls&&(m=il2cpp_class_get_methods(cls,&it));) {
    uint32_t extra=0;
    if(strcmp(il2cpp_method_get_name(m),name)||il2cpp_method_get_param_count(m)!=count||
        bool(s_clothMethodFlags(m,&extra)&0x10)!=isStatic||!CollisionType(il2cpp_method_get_return_type(m),ret))continue;
    bool ok=true;for(unsigned n=0;n<count;++n)ok=ok&&CollisionType(il2cpp_method_get_param(m,n),args[n]);
    if(ok){if(found)return nullptr;found=m;}
  }
  return found;
}
static bool ClothFinishLayout(void *parent,bool reduce) {
  auto job=ClothFinishNested(parent,reduce?"SolveEdgeBufferAndClearJob":"EdgeColliderCollisionConstraintJob");
  const char *edgeNames[]{"stepEdgeCollisionIndexArray","teamDataArray","parameterArray","attributes","vertexDepths",
      "edgeTeamIdArray","edges","nextPosArray","velocityPosArray","frictionArray","collisionNormalArray",
      "colliderFlagArray","colliderWorkDataArray","countArray","sumArray","tempFrictionArray","tempNormalArray"};
  const char *edgeTypes[]{"System.Int32","BeyondDynamicBone.TeamManager.TeamData","BeyondDynamicBone.ClothParameters",
      "BeyondDynamicBone.VertexAttribute","System.Single","System.Int16","Unity.Mathematics.int2","Unity.Mathematics.double3",
      "Unity.Mathematics.double3","System.Single","Unity.Mathematics.float3","BeyondDynamicBone.ExBitFlag8",
      "BeyondDynamicBone.ColliderManager.WorkData","System.Int32","System.Int32","System.Int32","System.Int32"};
  const char *reduceNames[]{"jobParticleIndexList","frictionArray","collisionNormalArray","nextPosArray","velocityPosArray",
      "countArray","sumArray","tempFrictionArray","tempNormalArray"};
  const char *reduceTypes[]{"System.Int32","System.Single","Unity.Mathematics.float3","Unity.Mathematics.double3",
      "Unity.Mathematics.double3","System.Int32","System.Int32","System.Int32","System.Int32"};
  const int count=reduce?9:17,bytes=reduce?160:288;
  if(!ClothContactSize(job,bytes))return false;
  for(int n=0;n<count;++n) {
    char type[192]{};_snprintf_s(type,_TRUNCATE,"Unity.Collections.NativeArray<%s>",reduce?reduceTypes[n]:edgeTypes[n]);
    auto name=reduce?reduceNames[n]:edgeNames[n];auto field=CollisionFieldInfo(job,name,type);
    auto array=field?il2cpp_class_from_type(il2cpp_field_get_type(field)):nullptr;
    if(!ClothContactOffset(job,name,type,bytes,16,n*16)||!ClothContactSize(array,16)||
        !ClothContactOffset(array,"m_Buffer","System.Void*",16,8,0)||
        !ClothContactOffset(array,"m_Length","System.Int32",16,4,8)||
        !ClothContactOffset(array,"m_AllocatorLabel","Unity.Collections.Allocator",16,4,12))return false;
  }
  const char *type="Unity.Collections.NativeReference<System.Int32>";auto field=CollisionFieldInfo(job,"_indexCount",type);
  auto ref=field?il2cpp_class_from_type(il2cpp_field_get_type(field)):nullptr;
  return ClothContactOffset(job,"_indexCount",type,bytes,16,count*16)&&ClothContactSize(ref,16)&&
      ClothContactOffset(ref,"m_Data","System.Void*",16,8,0)&&
      ClothContactOffset(ref,"m_AllocatorLabel","Unity.Collections.AllocatorManager.AllocatorHandle",16,4,8);
}
static bool ClothFinishConsumers(void *parent) {
  const char *edgeArgs[]{"System.Int32*","BeyondDynamicBone.TeamManager.TeamData*","BeyondDynamicBone.ClothParameters*",
      "BeyondDynamicBone.VertexAttribute*","System.Single*","System.Int16*","Unity.Mathematics.int2*","Unity.Mathematics.double3*",
      "System.Single*","Unity.Mathematics.float3*","Unity.Mathematics.double3*","BeyondDynamicBone.ExBitFlag8*",
      "BeyondDynamicBone.ColliderManager.WorkData*","System.Int32*","System.Int32*","System.Int32*","System.Int32*","System.Int32"};
  const char *reduceArgs[]{"System.Int32*","Unity.Mathematics.double3*","System.Single*","Unity.Mathematics.float3*",
      "Unity.Mathematics.double3*","System.Int32*","System.Int32*","System.Int32*","System.Int32*","System.Int32"};
  auto e=ClothFinishMethod(ClothFinishNested(parent,"EdgeColliderCollisionConstraintJobKernels"),
      "EdgeColliderCollisionConstraintKernel$BurstManaged","System.Void",true,edgeArgs,18);
  auto r=ClothFinishMethod(ClothFinishNested(parent,"SolveEdgeBufferAndClearJobKernels"),
      "SolveEdgeBufferAndClearKernel$BurstManaged","System.Void",true,reduceArgs,10);
  if(!ClothContactRequireCode("finish-edge-managed",e?((MInfo*)e)->mp:nullptr,3342,0x43342eb86afd45ebULL)||
      !ClothContactRequireCode("finish-reduce-managed",r?((MInfo*)r)->mp:nullptr,427,0xcd936d8d13b83a22ULL))return false;
  static constexpr ClothLayerCode bodies[]{
    {5753,0x6d5f2a130fef3ba6ULL},{4296,0xd195d01a6ff4401bULL},
    {6292,0xa1c1ede584e67f89ULL},{4794,0xb0f6af98256e5403ULL},
    {6388,0x92750918626533d6ULL},{4922,0x205cdeefd5ea9cdbULL},
    {391,0xf2dca4ccc7db291aULL},{399,0xba22afcaf33b9c40ULL},
    {493,0x6ecb37e1ab227219ULL},{490,0xf437fe6cc89b05f0ULL},
    {614,0xba8679d9c0b72dfaULL},{610,0xac2994c4cf02be85ULL}};
  auto module=GetModuleHandleW(L"lib_burst_generated.dll");auto base=(unsigned char*)module;
  if(!ClothLayerSpan(module,base,sizeof(IMAGE_DOS_HEADER)))return false;auto dos=(const IMAGE_DOS_HEADER*)base;
  if(dos->e_magic!=IMAGE_DOS_SIGNATURE||dos->e_lfanew<0||dos->e_lfanew>4096)return false;
  auto nt=(const IMAGE_NT_HEADERS64*)(base+dos->e_lfanew);
  if(!ClothLayerSpan(module,nt,sizeof(*nt))||nt->Signature!=IMAGE_NT_SIGNATURE||nt->FileHeader.Machine!=IMAGE_FILE_MACHINE_AMD64||
      nt->OptionalHeader.Magic!=IMAGE_NT_OPTIONAL_HDR64_MAGIC||nt->OptionalHeader.NumberOfRvaAndSizes<=IMAGE_DIRECTORY_ENTRY_EXCEPTION)return false;
  const size_t size=nt->OptionalHeader.SizeOfImage;const auto dir=nt->OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_EXCEPTION];
  if(size<4096||size>64*1024*1024||!dir.VirtualAddress||dir.VirtualAddress>=size||dir.Size>size-dir.VirtualAddress||
      dir.Size<sizeof(RUNTIME_FUNCTION)||dir.Size%sizeof(RUNTIME_FUNCTION)||dir.Size/sizeof(RUNTIME_FUNCTION)>65536||
      !ClothLayerSpan(module,base+dir.VirtualAddress,dir.Size))return false;
  const auto entries=(const RUNTIME_FUNCTION*)(base+dir.VirtualAddress);unsigned matches[12]{};
  for(size_t n=0;n<dir.Size/sizeof(RUNTIME_FUNCTION);++n) {
    const auto &f=entries[n];if(f.BeginAddress>=f.EndAddress||f.EndAddress>size)return false;
    for(unsigned k=0;k<12;++k)if(f.EndAddress-f.BeginAddress==bodies[k].bytes&&ClothLayerBody(module,base+f.BeginAddress,bodies[k]))++matches[k];
  }
  for(auto n:matches)if(n!=1)return false;return true;
}
static struct ClothFinishInstallState {bool attempted=false;void *targets[3]{};uint64_t patched[3]{};} s_clothFinishInstall;
static bool ClothFinishScheduleSignature(void *method,bool reduce) {
  static void *attempted[2]{};static bool accepted[2]{};
  const int slot=reduce?1:0;if(method==attempted[slot])return accepted[slot];
  attempted[slot]=method;accepted[slot]=false;
  uint32_t extra=0;const char *name=method?il2cpp_method_get_name(method):nullptr;
  if(!name||(strcmp(name,"Schedule2")&&strcmp(name,"Schedule"))||
      ((MInfo*)method)->mp!=s_clothFinishInstall.targets[reduce?2:1]||
      !(s_clothMethodFlags(method,&extra)&0x10)||il2cpp_method_get_param_count(method)!=5||
      !CollisionType(il2cpp_method_get_return_type(method),"Unity.Jobs.JobHandle"))return ClothFinishReject("closed-schedule-signature-unconfirmed");
  const char *type=reduce?"BeyondDynamicBone.ColliderCollisionConstraint.SolveEdgeBufferAndClearJob&":
      "BeyondDynamicBone.ColliderCollisionConstraint.EdgeColliderCollisionConstraintJob&";
  auto first=il2cpp_method_get_param(method,0);
  if(!CollisionType(first,type)&&!CollisionType(first,"T&"))return ClothFinishReject("schedule-job-type-unconfirmed");
  const char *rest[]{"System.Int32*","Unity.Collections.NativeReference<System.Int32>","System.Int32","Unity.Jobs.JobHandle"};
  for(unsigned n=0;n<4;++n)if(!CollisionType(il2cpp_method_get_param(method,n+1),rest[n]))return ClothFinishReject("schedule-argument-type-unconfirmed");
  accepted[slot]=true;return true;
}
static bool ClothInstallFinishMainThread() {
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1)return false;
  auto &i=s_clothFinishInstall;auto &h=s_clothFinishHooks;
  if(h.installed) {
    for(int n=0;n<3;++n)if(!ClothContactCode(i.targets[n],42,i.patched[n]))return false;
    return true;
  }
  if(i.attempted)return false;i.attempted=true;
  auto simulation=SurfaceClass("BeyondDynamicBone","SimulationManager");
  auto collision=SurfaceClass("BeyondDynamicBone","ColliderCollisionConstraint");
  const char *stepArgs[]{"System.Int32","System.Int32","Unity.Jobs.JobHandle"};
  auto step=ClothFinishMethod(simulation,"SimulationStepUpdate","Unity.Jobs.JobHandle",false,stepArgs,3);
  auto distance=SurfaceMethod(SurfaceClass("BeyondDynamicBone","DistanceConstraint"),"SolverConstraint","Unity.Jobs.JobHandle","Unity.Jobs.JobHandle");
  auto collider=SurfaceMethod(collision,"SolverConstraint","Unity.Jobs.JobHandle","Unity.Jobs.JobHandle");
  auto s=step?(unsigned char*)((MInfo*)step)->mp:nullptr;
  auto c=collider?(unsigned char*)((MInfo*)collider)->mp:nullptr;
  i.targets[0]=distance?((MInfo*)distance)->mp:nullptr;
  if(!ClothFinishLayout(collision,false)||!ClothFinishLayout(collision,true)||!ClothFinishConsumers(collision)||
      !ClothContactRequireCode("finish-step-prefix",s,5053,0x62a059b98962ddb5ULL)||
      !ClothContactRequireCode("finish-distance",i.targets[0],1162,0x35520ec184c8f111ULL)||
      !ClothContactRequireCode("finish-collision",c,1166,0x85eddce644e55ee3ULL))return ClothFinishReject("native-producer-or-consumer-unconfirmed");
  unsigned count=0;void *sites[2]{};
  for(size_t n=0;n+5<=5053;++n)if(s[n]==0xe8) {
    int32_t rel=0;memcpy(&rel,s+n+1,4);
    if(uintptr_t(s+n+5)+intptr_t(rel)==uintptr_t(i.targets[0])) {if(count<2)sites[count]=s+n+5;++count;}
  }
  void *colliderSite=nullptr;
  if(count!=2||ClothContactUniqueCall(s,5053,1166,0x85eddce644e55ee3ULL,"finish-collision-call",&colliderSite)!=c||
      uintptr_t(colliderSite)<=uintptr_t(sites[0])||uintptr_t(colliderSite)>=uintptr_t(sites[1]))return ClothFinishReject("native-constraint-order-unconfirmed");
  unsigned char *cold=nullptr;
  for(size_t n=0;n+6<=1166;++n)if(c[n]==0x0f&&c[n+1]>=0x80&&c[n+1]<=0x8f) {
    int32_t rel=0;memcpy(&rel,c+n+2,4);auto target=(unsigned char*)(uintptr_t(c+n+6)+intptr_t(rel));
    if(ClothContactCode(target,958,0x6b0b76dad1b387a5ULL)){if(cold)return ClothFinishReject("ambiguous-collision-branch");cold=target;}
  }
  if(!cold)return ClothFinishReject("edge-producer-branch-unconfirmed");
  i.targets[1]=ClothContactUniqueCall(cold,958,624,0x81d4688df79737d7ULL,"finish-edge-schedule",&h.edgeSite);
  i.targets[2]=ClothContactUniqueCall(cold,958,584,0xfd85bfc35118a0e9ULL,"finish-reduce-schedule",&h.reduceSite);
  if(!i.targets[1]||!i.targets[2]||uintptr_t(h.edgeSite)>=uintptr_t(h.reduceSite))return ClothFinishReject("native-schedule-ABI-unconfirmed");
  h.firstSite=sites[0];h.secondSite=sites[1];
  void *hooks[]{(void*)ClothFinishDistance,(void*)ClothFinishEdge,(void*)ClothFinishReduce};
  void **originals[]{(void**)&h.distance,(void**)&h.edge,(void**)&h.reduce};int created=0;
  for(;created<3;++created)if(MH_CreateHook(i.targets[created],hooks[created],originals[created])!=MH_OK)break;
  bool ok=created==3;
  if(ok)for(int n=0;n<3;++n)if(MH_QueueEnableHook(i.targets[n])!=MH_OK){ok=false;break;}
  if(ok)ok=MH_ApplyQueued()==MH_OK;
  if(!ok) {
    for(int n=0;n<created;++n){MH_DisableHook(i.targets[n]);MH_RemoveHook(i.targets[n]);}
    h.distance=nullptr;h.edge=nullptr;h.reduce=nullptr;return ClothFinishReject("native-producer-hook-failed");
  }
  for(int n=0;n<3;++n)i.patched[n]=eiem_cloth_input::Fingerprint((unsigned char*)i.targets[n],42);
  s_clothFinishSignature=ClothFinishScheduleSignature;h.installed=true;
  Log("[CLOTH-CONTACT-FINISH] stage=installed nativeConsumers=12 producerSignatures=verified passes=%d scope=owned-long-panel-Move-edges originalDistancePasses=2 workerHook=0 globalFrequencyChange=0",eiem_cloth_finish::Passes);
  return true;
}
