#pragma once
static bool ClothDisplayReject(const char *reason) {
  strncpy_s(s_clothDisplayIssue,reason,_TRUNCATE);
  Log("[CLOTH-DISPLAY] stage=install-refused reason=%s originalDisplayRetained=1",reason);return false;
}
static void *ClothDisplayNested(void *parent,const char *name) {
  if(!parent||!il2cpp_class_get_nested_types)return nullptr;
  char full[192]{};_snprintf_s(full,_TRUNCATE,"BeyondDynamicBone.SimulationManager.%s",name);
  void *found=nullptr;
  for(void *it=nullptr,*c=nullptr;(c=il2cpp_class_get_nested_types(parent,&it));)
    if(!strcmp(il2cpp_class_get_name(c),name)&&CollisionType(il2cpp_class_get_type(c),full)) {
      if(found)return nullptr;found=c;
    }
  return found;
}
static bool ClothDisplayLayout(void *job) {
  if(!ClothContactSize(job,192)||!ClothContactOffset(job,"simulationDeltaTime","System.Single",192,4,0)||
      !ClothContactOffset(job,"_indexCount","System.Int32",192,4,184))return false;
  const char *names[]{"teamDataArray","teamIdArray","oldPosArray","oldPositionArray","dispPosArray","positions",
      "realVelocityArray","oldRotationArray","attributes","rotations","vertexRootIndices"};
  const char *elements[]{"BeyondDynamicBone.TeamManager.TeamData","System.Int16","Unity.Mathematics.double3",
      "Unity.Mathematics.double3","Unity.Mathematics.double3","Unity.Mathematics.double3","Unity.Mathematics.float3",
      "Unity.Mathematics.quaternion","BeyondDynamicBone.VertexAttribute","Unity.Mathematics.quaternion","System.Int32"};
  for(int n=0;n<11;++n) {
    char type[160]{};_snprintf_s(type,_TRUNCATE,"Unity.Collections.NativeArray<%s>",elements[n]);
    const auto f=CollisionFieldInfo(job,names[n],type);const auto array=f?il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;
    if(!ClothContactOffset(job,names[n],type,192,16,8+n*16)||!ClothContactSize(array,16)||
        !ClothContactOffset(array,"m_Buffer","System.Void*",16,8,0)||
        !ClothContactOffset(array,"m_Length","System.Int32",16,4,8)||
        !ClothContactOffset(array,"m_AllocatorLabel","Unity.Collections.Allocator",16,4,12))return false;
  }
  return true;
}
static void *ClothDisplayKernel(void *cls) {
  const char *args[]{"System.Single","BeyondDynamicBone.TeamManager.TeamData*","System.Int16*","Unity.Mathematics.double3*",
      "Unity.Mathematics.float3*","Unity.Mathematics.double3*","Unity.Mathematics.quaternion*","Unity.Mathematics.double3*",
      "BeyondDynamicBone.VertexAttribute*","Unity.Mathematics.double3*","Unity.Mathematics.quaternion*","System.Int32*","System.Int32"};
  void *found=nullptr;
  for(void *it=nullptr,*m=nullptr;cls&&(m=il2cpp_class_get_methods(cls,&it));) {
    uint32_t extra=0;
    if(strcmp(il2cpp_method_get_name(m),"CalcDisplayPositionKernel$BurstManaged")||il2cpp_method_get_param_count(m)!=13||
        !(s_clothMethodFlags(m,&extra)&0x10)||!CollisionType(il2cpp_method_get_return_type(m),"System.Void"))continue;
    bool ok=true;for(unsigned n=0;n<13;++n)ok=ok&&CollisionType(il2cpp_method_get_param(m,n),args[n]);
    if(ok){if(found)return nullptr;found=m;}
  }
  return found;
}
static bool ClothDisplayBurstVerified(HMODULE module) {
  static constexpr ClothLayerCode bodies[]{
    {1521,0xe8b5a3df4e82663dULL},{1252,0x821a567fdde942dfULL},{1573,0xba606218704ffb6eULL},{1358,0xf28d5e80efe4ac41ULL},{1673,0xa7a75bd91cd3de27ULL},{1483,0x4a011d7a3a8bb059ULL}};
  auto base=(unsigned char*)module;
  if(!ClothLayerSpan(module,base,sizeof(IMAGE_DOS_HEADER)))return false;
  auto dos=(const IMAGE_DOS_HEADER*)base;
  if(dos->e_magic!=IMAGE_DOS_SIGNATURE||dos->e_lfanew<0||dos->e_lfanew>4096)return false;
  auto nt=(const IMAGE_NT_HEADERS64*)(base+dos->e_lfanew);
  if(!ClothLayerSpan(module,nt,sizeof(*nt))||nt->Signature!=IMAGE_NT_SIGNATURE||nt->FileHeader.Machine!=IMAGE_FILE_MACHINE_AMD64||
      nt->OptionalHeader.Magic!=IMAGE_NT_OPTIONAL_HDR64_MAGIC||nt->OptionalHeader.NumberOfRvaAndSizes<=IMAGE_DIRECTORY_ENTRY_EXCEPTION)return false;
  const size_t size=nt->OptionalHeader.SizeOfImage;const auto dir=nt->OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_EXCEPTION];
  if(size<4096||size>64*1024*1024||!dir.VirtualAddress||dir.VirtualAddress>=size||dir.Size>size-dir.VirtualAddress||
      dir.Size<sizeof(RUNTIME_FUNCTION)||dir.Size%sizeof(RUNTIME_FUNCTION)||dir.Size/sizeof(RUNTIME_FUNCTION)>65536||
      !ClothLayerSpan(module,base+dir.VirtualAddress,dir.Size))return false;
  const auto entries=(const RUNTIME_FUNCTION*)(base+dir.VirtualAddress);unsigned matches[6]{};
  for(size_t n=0;n<dir.Size/sizeof(RUNTIME_FUNCTION);++n) {
    const auto &e=entries[n];if(e.BeginAddress>=e.EndAddress||e.EndAddress>size)return false;
    for(unsigned k=0;k<6;++k)if(e.EndAddress-e.BeginAddress==bodies[k].bytes&&ClothLayerBody(module,base+e.BeginAddress,bodies[k]))++matches[k];
  }
  for(unsigned m:matches)if(m!=1)return false;
  return true;
}
struct ClothDisplayInstallState {
  bool attempted=false;unsigned char *producer=nullptr,*target=nullptr;uint64_t patched=0;
} static s_clothDisplayInstall;
static bool ClothInstallDisplayMainThread() {
  if(!ClothOnMainThread()||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1)return false;
  auto &s=s_clothDisplayInstall;
  if(s_clothDisplaySetCount)return ClothContactCode(s.target,7,s.patched);
  if(s.attempted)return false;s.attempted=true;
  auto cls=SurfaceClass("BeyondDynamicBone","SimulationManager"),job=ClothDisplayNested(cls,"CalcDisplayPositionJob");
  auto producer=SurfaceMethod(cls,"CalcDisplayPosition","Unity.Jobs.JobHandle","Unity.Jobs.JobHandle");
  auto setter=SurfaceMethod(job,"SetIndexCount","System.Void","System.Int32");
  auto kernel=ClothDisplayKernel(ClothDisplayNested(cls,"CalcDisplayPositionJobKernels"));
  s.producer=producer?(unsigned char*)((MInfo*)producer)->mp:nullptr;
  s.target=setter?(unsigned char*)((MInfo*)setter)->mp:nullptr;
  if(!ClothDisplayLayout(job)||!ClothContactRequireCode("display-producer",s.producer,1081,0xb587141af3068e66ULL)||
      !ClothContactRequireCode("display-counter-setter",s.target,7,0xef625ca43c061f4cULL)||
      !ClothContactRequireCode("display-managed-kernel",kernel?((MInfo*)kernel)->mp:nullptr,1621,0x51880b9d60834732ULL)||
      ClothContactUniqueCall(s.producer,1081,7,0xef625ca43c061f4cULL,"display-counter-call",&s_clothDisplayCallsite)!=s.target||
      !ClothDisplayBurstVerified(GetModuleHandleW(L"lib_burst_generated.dll")))return ClothDisplayReject("producer-or-consumer-ABI-unconfirmed");
  auto status=MH_CreateHook(s.target,(void*)ClothDisplaySetCount,(void**)&s_clothDisplaySetCount);const bool created=status==MH_OK;
  if(created)status=MH_EnableHook(s.target);
  if(status!=MH_OK) {
    if(created){MH_DisableHook(s.target);MH_RemoveHook(s.target);}s_clothDisplaySetCount=nullptr;
    return ClothDisplayReject("producer-hook-install-failed");
  }
  s.patched=eiem_cloth_input::Fingerprint(s.target,7);
  Log("[CLOTH-DISPLAY] stage=installed scope=exact-main-thread-producer nativeConsumers=6 managedSignature=verified borrowedView=until-master-complete workerHook=0 schedulerChanges=0 sourceArrayWrites=0");
  return true;
}
