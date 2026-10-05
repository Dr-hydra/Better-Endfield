#pragma once
#include "cloth_layer_dispatch_code.h"
static bool ClothLayerInstallReject(const char *reason) {
  _snprintf_s(s_clothLayerInstallIssue,_TRUNCATE,"native-layer-order-%s",reason);
  Log("[CLOTH-LAYER-ORDER] stage=install-refused reason=%s originalContactUnchanged=1",reason);return false;
}
static bool ClothLayerContactLayout(void **asms,size_t count) {
  const char *type="BeyondDynamicBone.SelfCollisionConstraint.PointTriangleContact";
  auto cls=ClothContactValueType("PointTriangleContact",type,asms,count);
  if(!ClothContactSize(cls,36))return false;
  struct Field {const char *name,*type;int bytes,offset;};
  const Field fields[]{
    {"flagAndTeamId0","System.UInt32",4,0},{"flagAndTeamId1","System.UInt32",4,4},
    {"thickness","Unity.Mathematics.half",2,8},{"sign","Unity.Mathematics.half",2,10},
    {"pointParticleIndex","System.Int32",4,12},{"triangleParticleIndex","Unity.Mathematics.int3",12,16},
    {"pointInvMass","Unity.Mathematics.half",2,28},{"triangleInvMass","Unity.Mathematics.half3",6,30}};
  for(const auto &f:fields)if(!ClothContactOffset(cls,f.name,f.type,36,f.bytes,f.offset))return false;
  auto half=FindClassDirect("Unity.Mathematics","half",asms,count);
  auto half3=FindClassDirect("Unity.Mathematics","half3",asms,count),int3=FindClassDirect("Unity.Mathematics","int3",asms,count);
  if(!ClothContactSize(half,2) || !ClothContactSize(half3,6) || !ClothContactSize(int3,12) ||
      !ClothContactOffset(half,"value","System.UInt16",2,2,0))return false;
  const char *axes[]{"x","y","z"};
  for(int n=0;n<3;++n)if(!ClothContactOffset(half3,axes[n],"Unity.Mathematics.half",6,2,n*2) ||
      !ClothContactOffset(int3,axes[n],"System.Int32",12,4,n*4))return false;
  auto primitive=ClothContactValueType("Primitive","BeyondDynamicBone.SelfCollisionConstraint.Primitive",asms,count);
  auto team=SurfaceMethod(primitive,"GetTeamId","System.Int32");
  return ClothContactRequireCode("Primitive.GetTeamId",team?((MInfo*)team)->mp:nullptr,8,0xb559f73fbeda78e2ULL);
}
static void *ClothLayerKernelMethod(void *cls) {
  const char *args[]{"Unity.Mathematics.double3*","BeyondDynamicBone.SelfCollisionConstraint.PointTriangleContact*",
      "System.Int32*","System.Int32*","System.Int32"};
  void *found=nullptr;
  for(void *it=nullptr,*m=nullptr;cls && (m=il2cpp_class_get_methods(cls,&it));) {
    uint32_t extra=0;
    if(strcmp(il2cpp_method_get_name(m),"SolverPointTriangleCrossFrameKernel$BurstManaged") ||
        il2cpp_method_get_param_count(m)!=5 || !(s_clothMethodFlags(m,&extra)&0x10) ||
        !CollisionType(il2cpp_method_get_return_type(m),"System.Void"))continue;
    bool ok=true;for(unsigned n=0;n<5;++n)ok=ok && CollisionType(il2cpp_method_get_param(m,n),args[n]);
    if(ok){if(found)return nullptr;found=m;}
  }
  return found;
}
static bool ClothLayerSolverLayout(void **asms,size_t count,unsigned char *(&managed)[2]) {
  auto cls=ClothContactValueType("SolverPointTriangleCrossFrameJob",
      "BeyondDynamicBone.SelfCollisionConstraint.SolverPointTriangleCrossFrameJob",asms,count);
  auto kernels=ClothContactValueType("SolverPointTriangleCrossFrameJobKernels",
      "BeyondDynamicBone.SelfCollisionConstraint.SolverPointTriangleCrossFrameJobKernels",asms,count);
  auto untyped=FindClassDirect("Unity.Collections.LowLevel.Unsafe","UntypedUnsafeList",asms,count);
  if(!ClothContactSize(untyped,32) || !ClothContactOffset(untyped,"Ptr","System.Void*",32,8,0) ||
      !ClothContactOffset(untyped,"m_length","System.Int32",32,4,8) ||
      !ClothContactOffset(untyped,"m_capacity","System.Int32",32,4,12) || !ClothContactSize(cls,64))return false;
  const char *fields[]{"nextPosArray","pointTriangleContactArray","countArray","sumArray"};
  const char *types[]{"Unity.Collections.NativeArray<Unity.Mathematics.double3>",
    "Unity.Collections.NativeArray<BeyondDynamicBone.SelfCollisionConstraint.PointTriangleContact>",
    "Unity.Collections.NativeArray<System.Int32>","Unity.Collections.NativeArray<System.Int32>"};
  for(unsigned n=0;n<4;++n) {
    if(!ClothContactOffset(cls,fields[n],types[n],64,16,n*16))return false;
    auto field=CollisionFieldInfo(cls,fields[n],types[n]);
    auto array=il2cpp_class_from_type(il2cpp_field_get_type(field));
    if(!ClothContactSize(array,16) || !ClothContactOffset(array,"m_Buffer","System.Void*",16,8,0) ||
        !ClothContactOffset(array,"m_Length","System.Int32",16,4,8))return false;
  }
  auto kernel=ClothLayerKernelMethod(kernels),index=SurfaceMethod(cls,"Execute","System.Void","System.Int32");
  auto whole=SurfaceMethod(cls,"Execute","System.Void");
  managed[0]=kernel?(unsigned char*)((MInfo*)kernel)->mp:nullptr;
  managed[1]=index?(unsigned char*)((MInfo*)index)->mp:nullptr;
  auto code=whole?(unsigned char*)((MInfo*)whole)->mp:nullptr;
  return ClothContactRequireCode("point-solver-kernel",managed[0],1766,0x4ef36188cbdd058aULL) &&
      ClothContactRequireCode("point-solver-index",managed[1],1789,0x96bd6a931bd13a17ULL) &&
      ClothContactRequireCode("point-solver-whole",code,62,0x6dde1006dd25ed56ULL) &&
      ClothContactUniqueCall(code,62,1789,0x96bd6a931bd13a17ULL,"point-solver-index-caller")==managed[1];
}
struct ClothLayerInstallState {
  bool attempted=false;
  HMODULE burst=nullptr;
  unsigned char *bodies[8]{},*targets[6]{};
  uint64_t patched[6]{};
} static s_clothLayerInstall;
static bool ClothInstallLayerOrderMainThread() {
  if(!ClothOnMainThread() || !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 || !s_clothInputHooks)return false;
  auto &s=s_clothLayerInstall;
  const size_t lengths[]{2644,1708,2643,1733,1766,1789};
  if(s_clothLayerInstalled.load(std::memory_order_acquire)) {
    for(unsigned n=0;n<6;++n)if(!ClothLayerBody(n<4?s.burst:hGA,s.targets[n],{lengths[n],s.patched[n]}))
      return ClothLayerInstallReject("installed-code-changed");
    return true;
  }
  if(s.attempted)return false;s.attempted=true;
  const double started=ClothInstallClockMs();
  size_t count=0;auto asms=il2cpp_domain_get_assemblies(il2cpp_domain_get(),&count);
  unsigned char *managed[2]{};
  if(!ClothLayerContactLayout(asms,count) || !ClothLayerSolverLayout(asms,count,managed))
    return ClothLayerInstallReject("typed-solver-ABI-unconfirmed");
  s.burst=GetModuleHandleW(L"lib_burst_generated.dll");
  if(!ClothLayerBurstLocate(s.burst,s.bodies))return ClothLayerInstallReject("Burst-consumer-bodies-unconfirmed");
  for(unsigned n=0;n<4;++n)s.targets[n]=s.bodies[n];
  s.targets[4]=managed[0];s.targets[5]=managed[1];
  for(unsigned n=0;n<6;++n)for(unsigned k=0;k<n;++k)if(s.targets[n]==s.targets[k])
    return ClothLayerInstallReject("consumer-alias-unconfirmed");
  const double validated=ClothInstallClockMs();
  void *detours[]{(void*)ClothLayerKernelHook<0>,(void*)ClothLayerKernelHook<1>,
      (void*)ClothLayerJobHook<2>,(void*)ClothLayerJobHook<3>,(void*)ClothLayerManagedKernelHook,(void*)ClothLayerManagedJobHook};
  unsigned created=0;bool ok=true;
  for(unsigned n=0;n<6 && ok;++n) {
    ok=MH_CreateHook(s.targets[n],detours[n],&s_clothLayerOriginal[n])==MH_OK;
    if(ok)++created;
  }
  const double createdAt=ClothInstallClockMs();
  if(ok)ok=ClothEnableHookGroup(s.targets,6);
  if(!ok) {
    for(unsigned n=0;n<created;++n){MH_DisableHook(s.targets[n]);MH_RemoveHook(s.targets[n]);}
    std::fill(std::begin(s_clothLayerOriginal),std::end(s_clothLayerOriginal),nullptr);
    return ClothLayerInstallReject("consumer-hook-install-failed");
  }
  for(unsigned n=0;n<6;++n)s.patched[n]=eiem_cloth_input::Fingerprint(s.targets[n],lengths[n]);
  s_clothLayerInstalled.store(true,std::memory_order_release);
  Log("[CLOTH-INSTALL-COST] adapter=layer validateMs=%g createMs=%g enableMs=%g enableBatches=1",
      validated-started,createdAt-validated,ClothInstallClockMs()-createdAt);
  Log("[CLOTH-LAYER-ORDER] stage=installed scope=owned-point-solver-input BurstConsumers=4 managedFallbacks=2 nativeContactListWrites=0 workerUnityCalls=0 originalSolver=1 dispatchObserved=0");
  return true;
}
