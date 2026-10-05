#pragma once
#include "math/cloth_preparation_budget.h"
#include <chrono>
static cloth_schedule::PreparationBudget s_clothPreparationBudget;
static bool ClothPreparationTurn() {
  const auto now=std::chrono::steady_clock::now().time_since_epoch();
  return s_clothPreparationBudget.acquire(s_clothActorIndex,ClothFrame(),
      std::chrono::duration<double,std::milli>(now).count());
}
#include "cloth_bonecloth_rebuild_state.h"
#include "cloth_bonecloth_reference.h"
#include "../generated/cloth_bonecloth_generated.h"
#include "../generated/cloth_bonecloth_partner_generated.h"
#include "../generated/cloth_bonecloth_support_generated.h"
#include "cloth_bonecloth_batch.h"
#include "../resources/cloth_bonecloth_cache.h"
#include "cloth_bonecloth_apron_policy.h"
#include "cloth_bonecloth_short_policy.h"
#include "cloth_bonecloth_priority.h"
#include "../collision/cloth_layer_order_hook.h"
#include "../collision/cloth_display_state.h"
#include "../collision/cloth_contact_finish_state.h"
static ClothActorBank<eiem_cloth_display::View> s_clothDisplayViews;
static ClothActorBank<eiem_cloth_finish::View> s_clothFinishViews;
static ClothActorBank<eiem_cloth_finish::Chain> s_clothFinishChains;
#define s_clothDisplayView (s_clothDisplayViews.Get())
#define s_clothFinishView (s_clothFinishViews.Get())
#define s_clothFinishChain (s_clothFinishChains.Get())
using ClothDisplaySetCountFn=void(__fastcall*)(eiem_cloth_display::Job*,int,void*);
static ClothDisplaySetCountFn s_clothDisplaySetCount=nullptr;
static bool (*s_clothDisplayInstaller)()=nullptr;
static void *s_clothDisplayCallsite=nullptr;
static char s_clothDisplayIssue[128]{};
static uint64_t s_clothDisplaySubmissions=0;
static int s_clothDisplayReportFrame=-1000;
static eiem_cloth_cache::Catalog s_clothBoneCache;
static const char *s_clothBoneCatalogSource="built-in";
static std::vector<const ClothBoneProfile*> s_clothBoneCatalog(std::begin(ClothBoneProfiles),std::end(ClothBoneProfiles));
struct ClothBoneReference {
  ClothRef bone{},parent{};
  eiem_cloth_rebuild::ReferencePose expected{};
  Vector3 local{}, savedPosition{}, savedScale{}, referenceScale{};
  Quaternion rotation{}, savedRotation{};
  Vector3 inputPosition{},inputScale{};Quaternion inputRotation{};
  bool inputCaptured=false;
  bool touched=false;
};
struct ClothBoneRendererRef {
  ClothRef renderer{},mesh{},root{},qualifiedParent{};
  std::vector<ClothRef> bones,parents;
};
struct ClothBoneSharedTeam { ClothRef bbc{}; uint32_t process=0; int team=0; };
struct ClothBoneAttachment {
  ClothRef bbc{},root{},parent{};
  uint32_t process=0,data=0,roots=0;
  int index=-1,count=0;
};
struct ClothBoneAdditionalCollider { ClothRef ref{},transform{},parent{}; int donor=-1,index=-1; };
struct ClothBoneNativeProducer {
  ClothRef bbc{};
  uint32_t process=0,data=0,roots=0; int team=0;
  std::vector<ClothRef> rootRefs,chainRefs,chainParents;
};
struct ClothBoneOwnershipRef {
  ClothRef bbc{};uint32_t process=0,data=0,data2=0,roots=0;int team=0;
  std::vector<ClothRef> rootRefs;std::vector<ClothBoneOwnership> proofs;
};
struct ClothBoneExcludedRef { ClothRef bone{},parent{}; };
#include "cloth_bonecloth_local_state.h"
struct ClothBoneMotionParameters {
  uint8_t useMaxDistance=0;float maxDistance[16]{};
  uint8_t useBackstop=0;float backstopRadius=0,backstopDistance[16]{},stiffness=0;
};
struct ClothBonePrebuildState {
  uint32_t data=0,shared=0,unique=0,manager=0;
  bool captured=false,retired=false,restored=false;
};
struct ClothBoneOwnedState {
  ClothRef gameObject{};
  const char *preflightStage="not-started",*preflightDetail="none";
  int preflightIndex=-1;
  bool created=false,buildIssued=false,disposeIssued=false,destroyIssued=false,teleportIssued=false;
  bool active=false,poseReturned=false;
  unsigned disposeAttempts=0,destroyAttempts=0;
  eiem_cloth_rebuild::RetirementBarrier retired{};
  struct Pose { Vector3 position{},scale{};Quaternion rotation{};bool known=false; };
  std::vector<Pose> before,last;
};
struct ClothBoneRuntime {
  ClothBoneOwnedState owned{};
  bool ribbonDampingConfigured=false,ribbonDampingConfirmed=false;
  float ribbonDamping=0,ribbonDampingCurve[16]{};
  ClothBonePrebuildState prebuild{};
  ClothBoneLocalState local{};
  eiem_cloth_rebuild::Transaction tx{};
  eiem_cloth::Owner owner{};
  ClothRef bbc{},transform{};
  std::vector<uint32_t> holds;
  uint32_t process[3]{},data=0,data2=0,candidateData=0,candidateData2=0,restoreData2=0;
  uint32_t rootLists[2]{};
  const ClothBoneProfile *profile=nullptr;
  ClothBoneProfile shapeProfile{};
  std::vector<ClothBoneAsset> shapeBones;
  std::vector<ClothRef> supportObjects;
  std::vector<bool> supportDestroyIssued;
  std::vector<int> supportIgnored;
  bool supportCreated=false,supportCleanup=false,supportOwnerDrained=false;
  bool supportPointCollision=false;
  uint32_t supportMotion=0;
  ClothBoneMotionParameters supportOriginalMotion{};
  uint32_t surfaceSourceBending=0,surfaceBending=0;
  float surfaceOriginalStiffness=0;
  int surfaceOriginalMethod=0,surfaceNoneMethod=0;
  uint32_t supportTether=0;
  unsigned supportElasticConversions=0;
  float supportCompression=0,supportBending=0,supportDistanceAttenuation=0;
  std::vector<ClothBoneReference> bones;
  std::vector<ClothBoneRendererRef> renderers;
  std::vector<ClothBoneSharedTeam> sharedTeams;
  std::vector<ClothBoneAttachment> attachments;
  std::vector<ClothBoneAdditionalCollider> additionalColliders;
  std::vector<ClothBoneNativeProducer> nativeProducers;
  std::vector<ClothBoneOwnershipRef> ownership;
  std::vector<ClothBoneExcludedRef> originalExcluded;
  std::vector<ClothBoneExcludedRef> excludedBranches,prebuildOmitted;
  uint32_t originalExcludedList=0;
  int contactPartner=-1,contactConsumer=-1;
  std::vector<int> partnerColliderOmissions;
  std::vector<std::vector<int>> colliderTeams;
  std::vector<ClothRef> colliders,colliderTransforms,colliderParents;
  uint32_t dependencies=0;
  bool stopRequested=false;
  bool autoSelect=false;
  eiem_cloth_surface::OutputMatrix referenceInverse{};
  std::vector<std::array<int,2>> oldLines,registeredEdges;
  std::vector<int> registeredVertices;
  std::vector<std::array<int,3>> registeredFaces;
  int index=-1,frame=-1,team[3]{};
  bool pending=false,lease=false,originalEnabled=false,failed=false,referenceRestored=true;
  bool graph[3]{},graphChecked[3]{},reference[3]{};
  uint64_t cpuSamples=0;
  double cpuTotal=0,cpuMax=0;
  bool teamModeConfirmed=false;
  unsigned teamModeReads=0;
  int modeFrame=-1;
  uint64_t modeDeadline=0,nextModeAudit=0;
  unsigned command=0,enableAttempts=0,returnData2Attempts=0;
  unsigned prepareStage=0;
  bool prepareWait=false;
  eiem_playback::SourceAdmission sourceAdmission;
  bool supportPreparing=false;
  size_t supportNextBone=0;
  bool disposalResources[3]{},adopted[3]{};
  unsigned installAttempts[3]{};
  uint64_t queueDrainDeadline=0;
  unsigned queueWaitState=0,retirementState[3]{};
  uint64_t deadline=0,requestedAt=0;
  bool activationLogged=false;
  char issue[192]{"not-requested"};
  char failure[192]{};
};
static int ClothBoneSourceKind(const ClothBoneProfile *p) {
  return !p?0:!p->runtimeGenerated?1:!p->generatedLocal||p->generatedLocal->NativeSkinRetained()?2:p->generatedLocal->FittedSkin()?1:p->generatedLocal->partialSurface?4:3;
}
static const char *ClothBoneSourceLabel(const ClothBoneProfile *p) {
  return !p?"none":p->runtimeUnowned?"runtime-unowned-sheet-native-BBC":p->generatedLocal?(p->generatedLocal->sourceShortSkin?"source-fixed-short-native-skin":p->generatedLocal->NativePanelsOnly()?"runtime-separated-native-connections":p->generatedLocal->FittedSkin()?"content-fitted-runtime-skin":"runtime-bones-and-skin"):
      p->runtimeGenerated?"runtime-connections":ClothBoneLocalRecipeFor(*p)?"authored-skin":"catalog-connections";
}
static int ClothBoneAppliedKind(const ClothBoneRuntime &s,const eiem_cloth::Owner &owner) {
  if(!s.profile||!(s.owner==owner)||!s.pending||!s.lease||s.failed||s.stopRequested||s.tx.cancelled||
      s.local.cleanup||s.supportCleanup||s.tx.phase!=eiem_cloth_rebuild::Phase::Active||s.team[1]<=0||
      (s.local.requested&&!s.local.published))return 0;
  return ClothBoneSourceKind(s.profile);
}
struct ClothActorBones {
  std::array<ClothBoneRuntime,eiem_cloth_rebuild::BatchCapacity> slots{};
  int context=0,count=1,mutating=-1;
  bool dispatching=false,deferred=false,resolved=false,noMatch=false;
  std::vector<const ClothBoneProfile*> fallbacks;
};
static ClothActorBank<ClothActorBones> s_ClothActorBones;
#define s_clothBoneSlots (s_ClothActorBones.Get().slots)
#define s_clothBoneContext (s_ClothActorBones.Get().context)
#define s_clothBoneCount (s_ClothActorBones.Get().count)
#define s_clothBoneDispatching (s_ClothActorBones.Get().dispatching)
#define s_clothAutoDeferred (s_ClothActorBones.Get().deferred)
#define s_clothAutoFallbacks (s_ClothActorBones.Get().fallbacks)
#define s_clothBoneMutating (s_ClothActorBones.Get().mutating)
#define s_clothBone (s_clothBoneSlots[0])
#define s_clothBoneResolved (s_ClothActorBones.Get().resolved)
#define s_clothBoneNoMatch (s_ClothActorBones.Get().noMatch)
static ClothBoneRuntime &ClothBoneState() { return s_clothBoneSlots[s_clothBoneContext]; }
static const ClothBoneProfile &ClothBoneCandidate(const ClothBoneRuntime &s) {
  return s.local.created?s.local.profile:!s.shapeBones.empty()?s.shapeProfile:*s.profile;
}
static bool ClothBoneSupportCreate();
static bool ClothBoneLayerPreflight();
static bool ClothBoneLayerStart(ClothBoneRuntime &s);
static bool ClothBoneSupportRelease();
static bool ClothBoneSurfacePartner(const ClothBoneRuntime &s);
static bool ClothBoneRibbonPartner(const ClothBoneRuntime &s);
static bool ClothBoneNativeLayerPartner(const ClothBoneRuntime &s);
static ClothRef ClothBoneLocalBindingRef(const ClothBoneRuntime &s,int id);
static bool ClothBoneLocalBindingMapFor(const ClothBoneRuntime &s,const ClothBoneLocalMeshConfig &config,const ClothBoneRendererAsset &source);
static void ClothBoneInSlot(int slot,void (*fn)()) {
  const int previous=s_clothBoneContext; s_clothBoneContext=slot;
  __try { fn(); } __finally { s_clothBoneContext=previous; }
}
static bool ClothBonePending() {
  if(s_clothBoneRequest.load()!=s_clothBoneSeen || s_clothAutoDeferred) return true;
  for(auto &s:s_clothBoneSlots) if(s.pending || s.lease) return true;
  return false;
}
static bool ClothBoneLeased() {
  for(auto &s:s_clothBoneSlots) if(s.lease) return true; return false;
}
static bool ClothBoneOwnsPrivateBBC(const ClothRef &bbc) {
  for(const auto &s:s_clothBoneSlots)if(s.profile&&s.profile->runtimeUnowned&&s.pending&&s.lease&&
      !s.stopRequested&&!s.failed&&s.owned.active&&s.bbc.id==bbc.id&&s.bbc.handle==bbc.handle&&ClothOwns(s.owner))return true;
  return false;
}
static bool ClothBoneLeasedInstance(const ClothInstance &i) {
  for(auto &s:s_clothBoneSlots) if(s.lease && i.ref.id==s.bbc.id &&
      s.tx.phase!=eiem_cloth_rebuild::Phase::Active) return true;
  return false;
}
static bool ClothBoneOwnsAnchor(const ClothAnchor &anchor) {
  for (const auto &s : s_clothBoneSlots) {
    if (!s.pending || !s.lease || !s.tx.lease || !(s.owner == s_cloth.owner) ||
        s.index < 0 || s.index >= s_cloth.count || s.index >= ClothCapacity ||
        !(anchor.members & (uint64_t(1) << s.index)) ||
        !(s_cloth.instances[s.index].ref.id == s.bbc.id)) continue;
    for (const auto &b : s.bones)
      if (b.bone.handle && b.parent.handle && b.bone.id == anchor.ref.id &&
          b.parent.id == anchor.parent.id) return true;
  }
  return false;
}
static void ClothBoneNote(const char *reason) {
  auto &s=ClothBoneState();
  if(strcmp(s.issue,reason)) {
    strncpy_s(s.issue,reason,_TRUNCATE);
    Log("[CLOTH-BONE] session=%llu generation=%llu command=%u frame=%d phase=%d instance=%d lease=%d component=%s reason=%s",
      (unsigned long long)s.owner.session,(unsigned long long)s.owner.generation,s.command,ClothFrame(),int(s.tx.phase),s.bbc.id.instance,int(s.lease),s.profile?s.profile->component:"pending",reason);
  }
  s_collisionInspect.store(true,std::memory_order_release);
}
static uint32_t ClothBoneHold(void *p) {
  if(!p) return 0;
  auto h=il2cpp_gchandle_new(p,false); if(h) ClothBoneState().holds.push_back(h); return h;
}
static void *ClothBoneNew(const char *type) {
  auto cls=SurfaceClass("BeyondDynamicBone",type);
  void *p=cls ? il2cpp_object_new(cls):nullptr, *unused=nullptr;
  if(!ClothBoneHold(p) || !ClothInvoke(SurfaceMethod(cls,".ctor","System.Void"),p,nullptr,unused)) return nullptr;
  return p;
}
static bool ClothBoneCurrent(eiem_cloth_rebuild::Identity &id) {
  auto &s=ClothBoneState(); void *bbc=ClothTarget(s.bbc), *p=nullptr,*d=nullptr,*d2=nullptr;
  if(!bbc || !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_Process","BeyondDynamicBone.ClothProcess"),bbc,nullptr,p) ||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,d) ||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"GetSerializeData2","BeyondDynamicBone.ClothSerializeData2"),bbc,nullptr,d2)) return false;
  id={s.owner,uint64_t(uintptr_t(bbc)),uint64_t(uintptr_t(p)),uint64_t(uintptr_t(d)),uint64_t(uintptr_t(d2))};
  return id.Valid();
}
static bool ClothBoneNativeArray(void *owner,const char *field,const char *element,int stride,std::vector<unsigned char> &out) {
  char type[160]{},arrayType[128]{};
  _snprintf_s(type,_TRUNCATE,"Unity.Collections.NativeArray<%s>",element);
  _snprintf_s(arrayType,_TRUNCATE,"%s[]",element);
  auto f=owner ? CollisionFieldInfo(il2cpp_object_get_class(owner),field,type):nullptr;
  auto cls=f ? il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;
  const char *leaf=strrchr(element,'.');
  auto ec=leaf ? SurfaceClass("Unity.Mathematics",leaf+1):nullptr;
  struct Header { void *buffer; int length,allocator; } copy{};
  uint32_t align=0;
  if(!cls || !ec || !ClothInputLayout(ec,element,stride) || il2cpp_class_value_size(cls,&align)!=sizeof(copy) ||
      ClothValueOffset(cls,"m_Buffer","System.Void*",sizeof(copy),8)!=0 ||
      ClothValueOffset(cls,"m_Length","System.Int32",sizeof(copy),4)!=8 ||
      ClothValueOffset(cls,"m_AllocatorLabel","Unity.Collections.Allocator",sizeof(copy),4)!=12 ||
      !CollisionField(owner,field,type,copy) || !copy.buffer || copy.length<1 ||
      copy.length>(!strcmp(field,"edges")?ClothBoneMaxEdges:ClothBoneMaxParticles+1)) return false;
  void *array=nullptr;
  if(!ClothInvoke(SurfaceMethod(cls,"ToArray",arrayType),&copy,nullptr,array) || !array) return false;
  const auto h=il2cpp_gchandle_new(array,false); if(!h) return false;
  uintptr_t count=0;
  const bool ok=ClothArray(array,arrayType,count) && count==size_t(copy.length);
  if(ok) { out.resize(count*stride); memcpy(out.data(),(char*)array+32,out.size()); }
  il2cpp_gchandle_free(h); return ok;
}
static bool ClothBoneMethodFingerprint(const char *type,const char *name,const char *result,
    const char *argument,size_t bytes,uint64_t fingerprint,const char *second=nullptr,bool isStatic=false) {
  auto cls=SurfaceClass("BeyondDynamicBone",type);
  auto method=isStatic?(second?nullptr:ClothMethod(cls,name,result,argument,true)):SurfaceMethod(cls,name,result,argument,second);
  auto code=method ? *reinterpret_cast<const unsigned char **>(method):nullptr;
  const auto actual=code ? eiem_cloth_input::Fingerprint(code,bytes):0;
  if(!code || actual!=fingerprint) Log("[CLOTH-BONE-ABI] type=%s method=%s bytes=%zu actual=%llx expected=%llx",type,name,bytes,
      (unsigned long long)actual,(unsigned long long)fingerprint);
  return code && actual==fingerprint;
}
static bool ClothBoneCaptureABI() {
  auto cls=SurfaceClass("BeyondDynamicBone","RenderSetupData");
  auto method=SurfaceMethod(cls,"ReadTransformInformation","System.Void","System.Boolean");
  int run=-1;
  if(!method || !SurfaceMethod(SurfaceClass("BeyondDynamicBone","BeyondBoneCloth"),"DisposeTeleportResources","System.Void") ||
      !SurfaceMethod(SurfaceClass("BeyondDynamicBone","ClothProcess"),"Dispose","System.Void") ||
      !CollisionEnumValue(SurfaceClass("Unity.Jobs.LowLevel.Unsafe","ScheduleMode"),"Run",run) || run!=0) return false;
  return ClothBoneMethodFingerprint("VirtualMesh","ImportBoneType","System.Void","BeyondDynamicBone.RenderSetupData",12628,0x229ddc69cf1e43d9ULL,"System.Int32[]") &&
      ClothBoneMethodFingerprint("RenderSetupData","ReadTransformInformation","System.Void","System.Boolean",7010,0xaf84fc0d450cedc0ULL) &&
      ClothBoneMethodFingerprint("BeyondBoneCloth","Initialize","System.Void",nullptr,62,0x1e6b802ad034a390ULL) &&
      ClothBoneMethodFingerprint("BeyondBoneCloth","BuildAndRun","System.Boolean",nullptr,990,0x0ce0b84ac33e3a7eULL) &&
      ClothBoneMethodFingerprint("ClothProcess","GenerateInitialization","System.Boolean",nullptr,238,0xf2b6de0e7ade2da0ULL) &&
      ClothBoneMethodFingerprint("ClothProcess","Init","System.Void",nullptr,1616,0xf5e502c8721df4a9ULL) &&
      ClothBoneMethodFingerprint("BeyondBoneCloth","DisposeTeleportResources","System.Void",nullptr,84,0xc6461e0129da43b7ULL);
}
static int ClothBoneIndex(void *t) {
  for(int n=0;n<int(ClothBoneState().bones.size());++n) if(t && ClothTarget(ClothBoneState().bones[n].bone)==t) return n;
  return -1;
}
#include "cloth_bonecloth_input_anchor.h"
#include "cloth_bonecloth_prebuild.h"
static bool ClothBoneReadReference(void *process,bool capture) {
  auto &state=ClothBoneState();
  if(state.prebuild.captured && process!=CollisionGc(state.process[1]))
    return ClothBonePrebuildReference(process,capture);
  auto &s=ClothBoneState(); void *setup=nullptr,*list=nullptr;
  if(!CollisionField(process,"boneClothSetupData","BeyondDynamicBone.RenderSetupData",setup) || !setup ||
      !CollisionField(setup,"transformList","System.Collections.Generic.List<UnityEngine.Transform>",list)) return false;
  const int count=CollisionCount(list); if(!s.profile || count<3 || count>ClothBoneMaxParticles+1) return false;
  Log("[CLOTH-BONE-REFERENCE] captureOriginal=%d Process=%p setup=%p transformCount=%d",capture,process,setup,count);
  std::vector<unsigned char> positions,rotations,local,localQ,scales;
  if(!ClothBoneNativeArray(setup,"transformPositions","Unity.Mathematics.float3",12,positions) ||
      !ClothBoneNativeArray(setup,"transformRotations","Unity.Mathematics.quaternion",16,rotations) ||
      !ClothBoneNativeArray(setup,"transformLocalPositins","Unity.Mathematics.float3",12,local) ||
      !ClothBoneNativeArray(setup,"transformLocalRotations","Unity.Mathematics.quaternion",16,localQ) ||
      !ClothBoneNativeArray(setup,"transformScales","Unity.Mathematics.float3",12,scales) ||
      positions.size()!=size_t(count)*12 || rotations.size()!=size_t(count)*16 || local.size()!=positions.size() ||
      localQ.size()!=rotations.size() || scales.size()!=positions.size()) return false;
  float inverse[16]{}; Quaternion rotation{};
  if(!CollisionField(setup,"initRenderWorldtoLocal","Unity.Mathematics.float4x4",inverse) ||
      !CollisionField(setup,"initRenderRotation","Unity.Mathematics.quaternion",rotation)) return false;
  eiem_cloth_surface::OutputMatrix inv{}; for(int k=0;k<16;++k) inv.v[k]=inverse[k];
  double readRotation[]{rotation.x,rotation.y,rotation.z,rotation.w};
  if(!eiem_cloth_rebuild::UniformPositive(inv) || !eiem_cloth_rebuild::UnitQuaternion(readRotation)) return false;
  if(capture) {
    s.referenceInverse=inv;
  }
  std::vector<bool> found(s.bones.size()); int matched=0;
  for(int n=0;n<count;++n) {
    void *t=CollisionItem(list,n,"UnityEngine.Transform"); if(!t) return false;
    char name[128]{}; CollisionName(t,name,sizeof(name)); int b=-1;
    if(capture) { char parent[128]{};CollisionName(CollisionParent(t),parent,sizeof(parent));
      for(int k=0;k<s.profile->boneCount;++k) if(!strcmp(name,s.profile->bones[k].name)&&!strcmp(parent,s.profile->bones[k].parentName)){if(b>=0)return false;b=k;} }
    else b=ClothBoneIndex(t);
    if(b<0) { if(t!=ClothTarget(s.transform)) return false; continue; }
    if(found[b] || (capture && s.profile->OutsideOriginal(b))) return false;
    found[b]=true; ++matched; auto &r=s.bones[b];
    Vector3 lp{},wp{},scale{}; Quaternion lq{},wq{};
    memcpy(&lp,local.data()+12*n,12); memcpy(&wp,positions.data()+12*n,12);
    memcpy(&lq,localQ.data()+16*n,16); memcpy(&wq,rotations.data()+16*n,16); memcpy(&scale,scales.data()+12*n,12);
    double worldQ[]{wq.x,wq.y,wq.z,wq.w};
    if(!std::isfinite(wp.x) || !std::isfinite(wp.y) || !std::isfinite(wp.z) ||
        !eiem_cloth_rebuild::UnitQuaternion(worldQ) ||
        !(s.profile->Passive(b)?eiem_cloth_rebuild::PositiveAxes(scale.x,scale.y,scale.z):eiem_collision::UniformPositive(CollisionV(scale)))) return false;
    if(capture) {
      auto parent=CollisionParent(t); char pn[128]{}; CollisionName(parent,pn,sizeof(pn));
      const auto &asset=s.profile->bones[b];
      const char *expected=asset.parentName;
      if(!ClothAnchorUnderOwner(t) || strcmp(expected,pn) ||
          !ClothSameLocal(lp,lq,asset.position,asset.rotation)) {
        Log("[CLOTH-BONE-REFERENCE-MISMATCH] bone=%s parent=%s expectedParent=%s cachedLocal=%g,%g,%g cachedRotation=%g,%g,%g,%g",
            name,pn,expected,lp.x,lp.y,lp.z,lq.x,lq.y,lq.z,lq.w); return false;
      }
      Vector3 live{},localScale{}; Quaternion liveRotation{};
      if(!SurfaceVisiblePose(t,live,liveRotation,localScale) ||
          !(s.profile->Passive(b)?eiem_cloth_rebuild::PositiveAxes(localScale.x,localScale.y,localScale.z):eiem_collision::UniformPositive(CollisionV(localScale))) ||
          fabsf(localScale.x-asset.scale.x)>.0001f || fabsf(localScale.y-asset.scale.y)>.0001f || fabsf(localScale.z-asset.scale.z)>.0001f) {
        Log("[CLOTH-BONE-REFERENCE-MISMATCH] bone=%s localScale=%g,%g,%g expectedScale=%g,%g,%g",name,
            localScale.x,localScale.y,localScale.z,asset.scale.x,asset.scale.y,asset.scale.z); return false;
      }
      r.bone=ClothProtect(t); r.parent=ClothProtect(parent); if(!r.bone.handle || !r.parent.handle) return false;
      r.local=lp; r.rotation=lq; r.referenceScale=scale;
    } else {
      const eiem_cloth_rebuild::ReferencePose actual{{wp.x,wp.y,wp.z},{wq.x,wq.y,wq.z,wq.w}};
      if(CollisionParent(t)!=ClothTarget(r.parent) || !ClothSameLocal(lp,lq,r.local,r.rotation) ||
          !eiem_cloth_rebuild::SameReference(actual,r.expected,.0002)) {
        Log("[CLOTH-BONE-REFERENCE-MISMATCH] captureOriginal=0 bone=%s cachedLocal=%g,%g,%g expectedLocal=%g,%g,%g world=%g,%g,%g expectedWorld=%g,%g,%g worldQ=%g,%g,%g,%g expectedQ=%g,%g,%g,%g localAndWorldRequired=1",
            name,lp.x,lp.y,lp.z,r.local.x,r.local.y,r.local.z,wp.x,wp.y,wp.z,r.expected.p[0],r.expected.p[1],r.expected.p[2],
            wq.x,wq.y,wq.z,wq.w,r.expected.q[0],r.expected.q[1],r.expected.q[2],r.expected.q[3]); return false;
      }
    }
  }
  if(capture) {
    if(matched!=s.profile->OriginalCount() || count!=matched+1 || !ClothBoneCaptureInputAnchors()) return false;
    for(int b=0;b<s.profile->boneCount;++b) {
      int parent=s.profile->bones[b].parent;
      if(parent>=0 && (parent>=b || ClothTarget(s.bones[b].parent)!=ClothTarget(s.bones[parent].bone))) return false;
    }
    return true;
  }
  const bool candidate=s.tx.phase==eiem_cloth_rebuild::Phase::BuildCandidate;
  const auto &profile=candidate?ClothBoneCandidate(s):*s.profile;
  for(int b=0;b<int(found.size());++b)
    if(found[b]!=(b<profile.boneCount && (candidate?profile.bones[b].attribute!=0:!profile.OutsideOriginal(b)))) return false;
  return matched==(candidate?profile.EffectiveCount():profile.OriginalCount()) && count==matched+1;
}
static bool ClothBoneGraph(void *process,bool original,bool capture=false) {
  auto &s=ClothBoneState(); void *container=nullptr,*vm=nullptr;
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(process),"get_ProxyMeshContainer","BeyondDynamicBone.VirtualMeshContainer"),process,nullptr,container) ||
      !container || !CollisionField(container,"shareVirtualMesh","BeyondDynamicBone.VirtualMesh",vm) || !vm) return false;
  const auto *profile=original?s.profile:&ClothBoneCandidate(s);
  std::vector<unsigned char> attrs,refs,skin,triangles,lines;
  if(!SurfaceReadArray(vm,"attributes","BeyondDynamicBone.VertexAttribute",1,attrs) ||
      !SurfaceReadArray(vm,"referenceIndices","System.Int32",4,refs) ||
      !SurfaceReadArray(vm,"skinBoneTransformIndices","System.Int32",4,skin) ||
      !SurfaceReadArray(vm,"triangles","Unity.Mathematics.int3",12,triangles,ClothBoneMaxFaces) ||
      !SurfaceReadArray(vm,"lines","Unity.Mathematics.int2",8,lines)) return false;
  Log("[CLOTH-BONE-GRAPH] component=%s original=%d Process=%p vertices=%zu references=%zu skinTransforms=%zu triangles=%zu lines=%zu",
      s.profile?profile->component:"pending",original,process,attrs.size(),refs.size()/4,skin.size()/4,triangles.size()/12,lines.size()/8);
  if(refs.size()!=attrs.size()*4 || attrs.size()!=size_t(original?profile->OriginalCount():profile->EffectiveCount())) return false;
  unsigned char fixed=0,move=0,triangle=0; auto ac=SurfaceClass("BeyondDynamicBone","VertexAttribute");
  if(!CollisionByteFlag(ac,"Flag_Fixed",fixed) || !CollisionByteFlag(ac,"Flag_Move",move) || !CollisionByteFlag(ac,"Flag_Triangle",triangle)) return false;
  std::vector<bool> seen(s.bones.size());
  std::vector<int> bones;
  std::vector<std::array<int,2>> labels;
  auto get=SurfaceMethod(il2cpp_object_get_class(container),"GetTransformFromIndex","UnityEngine.Transform","System.Int32");
  for(size_t n=0;n<attrs.size();++n) {
    int ref=-1,ix=-1; memcpy(&ref,refs.data()+4*n,4);
    if(ref<0 || size_t(ref)*4+4>skin.size()) return false;
    memcpy(&ix,skin.data()+4*ref,4); if(ix<0 || ix>ClothBoneMaxParticles) return false;
    void *t=nullptr,*args[]{&ix}; if(!ClothInvoke(get,container,args,t)) return false;
    int b=ClothBoneIndex(t); if(b<0 || b>=profile->boneCount || size_t(b)>=seen.size() || seen[b] || (original&&profile->OutsideOriginal(b))) return false; seen[b]=true; bones.push_back(b);
    const auto &asset=profile->bones[b]; int attr=asset.attribute;
    if((attrs[n]&~triangle)!=(attr==1?fixed:attr==2?move:0) || (!original && !attr)) return false;
    labels.push_back({asset.column,asset.depth});
  }
  std::vector<std::array<int,2>> edges;
  for(size_t n=0;n<lines.size()/8;++n) {
    int e[2]{}; memcpy(e,lines.data()+8*n,8);
    if(e[0]<0 || e[1]<0 || size_t(e[0])>=bones.size() || size_t(e[1])>=bones.size()) return false;
    std::array<int,2> a{bones[e[0]],bones[e[1]]}; std::sort(a.begin(),a.end()); edges.push_back(a);
  }
  if(original) {
    if(!triangles.empty()) return false;
    if(capture) {
      std::vector<std::array<int,2>> expected;
      for(int b=0;b<profile->boneCount;++b) if(profile->bones[b].parent>=0 && !profile->OutsideOriginal(b) && !profile->OutsideOriginal(profile->bones[b].parent))
        expected.push_back({profile->bones[b].parent,b});
      if(!eiem_cloth_surface::SameSimplices(edges,expected)) return false;
      s.oldLines=edges; return true;
    }
    return eiem_cloth_surface::SameSimplices(edges,s.oldLines);
  }
  if((!profile->nativeGraphCount&&!lines.empty()) || triangles.size()%12) return false;
  std::vector<std::array<int,3>> faces(triangles.size()/12);
  if(!faces.empty()) memcpy(faces.data(),triangles.data(),triangles.size());
  bool graphOk=false;
  if(profile->nativeGraphCount) {
    std::vector<std::array<int,3>> labelled;
    for(auto f:faces) {
      for(auto &n:f) {if(n<0||size_t(n)>=bones.size())return false;n=bones[n];}labelled.push_back(f);
    }
    graphOk=eiem_cloth_rebuild::NativeGraphMatch(*profile,labelled,edges);
  } else graphOk=eiem_cloth_rebuild::ConnectedStrip(labels,faces,profile->rootCount,profile->depth,profile->loop);
  if(!graphOk) {
    std::ostringstream detail;
    detail << "[CLOTH-BONE-GRAPH-REFUSED] component=" << profile->component
        << " expectedFaces=" << profile->FaceCount()
        << " actualFaces=" << faces.size() << " rendererUnchanged=1 labels=";
    for(auto label:labels) detail << label[0] << ':' << label[1] << ',';
    detail << " profileBones=";for(int b:bones)detail << b << ',';
    detail << " faces=";
    for(size_t n=0;n<faces.size() && n<ClothBoneMaxFaces;++n) detail << faces[n][0] << ':' << faces[n][1] << ':' << faces[n][2] << ',';
    detail << " lineLabels=";
    for(auto e:edges)detail << profile->bones[e[0]].column << ':' << profile->bones[e[0]].depth << '-' << profile->bones[e[1]].column << ':' << profile->bones[e[1]].depth << ',';
    Log("%s",detail.str().c_str());return false;
  }
  s.registeredVertices=bones;s.registeredFaces=faces;
  for(auto &f:s.registeredFaces)for(auto &n:f)n=bones[n];
  if(!s.local.requested && !profile->nativeGraphCount) return true;
  std::vector<unsigned char> nativeEdges;
  if(!ClothBoneNativeArray(vm,"edges","Unity.Mathematics.int2",8,nativeEdges) || nativeEdges.size()%8) return false;
  std::vector<std::array<int,2>> expected,actual;
  for(const auto &f:faces) for(int k=0;k<3;++k) {
    std::array<int,2> e{bones[f[k]],bones[f[(k+1)%3]]};std::sort(e.begin(),e.end());
    if(std::find(expected.begin(),expected.end(),e)==expected.end()) expected.push_back(e);
  }
  for(auto e:edges) if(std::find(expected.begin(),expected.end(),e)==expected.end()) expected.push_back(e);
  for(size_t n=0;n<nativeEdges.size()/8;++n) {
    int ix[2]{};memcpy(ix,nativeEdges.data()+n*8,8);
    if(ix[0]<0 || ix[1]<0 || size_t(ix[0])>=bones.size() || size_t(ix[1])>=bones.size() || ix[0]==ix[1]) return false;
    std::array<int,2> e{bones[ix[0]],bones[ix[1]]};std::sort(e.begin(),e.end());
    if(std::find(expected.begin(),expected.end(),e)==expected.end() || std::find(actual.begin(),actual.end(),e)!=actual.end()) return false;
    actual.push_back(e);
  }
  for(auto e:expected) if((profile->bones[e[0]].attribute==2 || profile->bones[e[1]].attribute==2) &&
      std::find(actual.begin(),actual.end(),e)==actual.end()) return false;
  if(profile->nativeGraphCount&&!s.local.requested) {
    s.registeredEdges=actual;
    Log("[CLOTH-BONE-UNEQUAL] component=%s vertices=%zu faces=%zu tailLines=%zu edges=%zu ignoredAttachments=%d identityLabelled=1 originalRenderer=1 nativeConstraintFacesNotRenderSurface=1 nativeFixedForks=%d optimizerOrderProof=%d",
        profile->component,bones.size(),faces.size(),edges.size(),actual.size(),profile->candidateIgnoredCount,profile->runtimeFixedForks,profile->nativeGraphOrder!=nullptr);
    return true;
  }
  std::vector<unsigned char> naturalPositions;
  if(!SurfaceReadArray(vm,"localPositions","Unity.Mathematics.float3",12,naturalPositions) || naturalPositions.size()!=bones.size()*12) return false;
  const auto &recipe=*s.local.recipe;
  if(s.local.crossRest.size()!=size_t(recipe.crossCount)) return false;
  for(size_t k=0;k<s.local.crossRest.size();++k) {
    Vector3 a{},b{};bool haveA=false,haveB=false;
    for(size_t n=0;n<bones.size();++n) {
      if(bones[n]==recipe.cross[k][0]) {memcpy(&a,naturalPositions.data()+n*12,12);haveA=true;}
      if(bones[n]==recipe.cross[k][1]) {memcpy(&b,naturalPositions.data()+n*12,12);haveB=true;}
    }
    const float x=a.x-b.x,y=a.y-b.y,z=a.z-b.z;
    s.local.crossRest[k]=std::sqrt(x*x+y*y+z*z);
    if(!haveA || !haveB || !std::isfinite(s.local.crossRest[k]) || s.local.crossRest[k]<1e-6f) return false;
  }
  s.registeredEdges=actual;
  Log("[CLOTH-BONE-EDGES] component=%s vertices=%zu triangles=%zu actualEdges=%zu endpointIdentity=1 nativeManagerReadbackPending=1",
      profile->component,bones.size(),faces.size(),actual.size());
  return true;
}

static bool ClothBoneTeleport(bool &pending,bool &created) {
  void *bbc=CollisionGc(ClothBoneState().bbc.handle);
  const char *type="Unity.Collections.NativeList<BeyondDynamicBone.ClothTeleport.TransformSlice>";
  auto f=bbc ? CollisionFieldInfo(il2cpp_object_get_class(bbc),"m_teleportSlices",type):nullptr;
  auto cls=f ? il2cpp_class_from_type(il2cpp_field_get_type(f)):nullptr;
  uint32_t alignment=0;
  const int bytes=cls ? il2cpp_class_value_size(cls,&alignment):0;
  const int offset=f ? int(il2cpp_field_get_offset(f)):-1;
  if(bytes<=0 || bytes>64 || offset<16 || offset>65536-bytes ||
      !CollisionField(bbc,"m_pendingTeleportApply","System.Boolean",pending)) return false;
  alignas(16) unsigned char copy[64]{}; memcpy(copy,(char*)bbc+offset,bytes);
  return ClothValue(SurfaceMethod(cls,"get_IsCreated","System.Boolean"),copy,created);
}
static bool ClothBoneSetEnabled(bool wanted) {
  auto bbc=ClothTarget(ClothBoneState().bbc); bool actual=!wanted; void *unused=nullptr,*args[]{&wanted};
  if(!bbc || !ClothValue(s_clothUnity.getEnabled,bbc,actual)) return false;
  if(actual!=wanted && !ClothInvoke(s_clothUnity.setEnabled,bbc,args,unused)) return false;
  return ClothValue(s_clothUnity.getEnabled,bbc,actual) && actual==wanted;
}
static void ClothBoneDropReferences() {
  s_clothPreparationBudget.cancel(s_clothActorIndex);
  auto &s=ClothBoneState();
  if(s.owned.gameObject.handle){s.stopRequested=true;s.pending=true;return;}
  s_clothLayerGate.Clear();
  s_clothDisplayView.Revoke();
  s_clothFinishView.Revoke();s_clothFinishChain.Clear();
  if(s.local.requested && s.local.HasResources()) {
    s.local.cleanup=true;s.pending=true;
    if(!s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 || !ClothBoneLocalCleanup()) {
      ClothBoneNote("local-owned-resources-awaiting-safe-cleanup");return;
    }
  }
  if(!s.supportObjects.empty()) {
    s.supportCleanup=true;s.pending=true;
    if(!s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 || !ClothBoneSupportRelease()) {
      ClothBoneNote("paired-surface-support-awaiting-consumer-and-native-retirement");return;
    }
  }
  if(!ClothBoneResponseRestore()){s.pending=true;ClothBoneNote("response-pose-return-pending-before-handle-release");return;}
  ClothBoneSolverClear(s_clothBoneContext);
  ClothBoneResponseFree();
  ClothFree(s.bbc); ClothFree(s.transform);
  for(auto &b:s.bones) { ClothFree(b.bone); ClothFree(b.parent); }
  for(auto &c:s.colliders) ClothFree(c);
  for(auto &t:s.colliderTransforms) ClothFree(t);
  for(auto &t:s.colliderParents) ClothFree(t);
  for(auto &c:s.local.apronLayer.outerColliders)ClothFree(c);
  s.local.apronLayer={};
  s.local.elasticTether=s.supportTether=0;
  s.prebuild={};
  for(auto h:s.holds) if(h) il2cpp_gchandle_free(h);
  for(auto &r:s.renderers) {
    ClothFree(r.renderer); ClothFree(r.mesh); ClothFree(r.root); ClothFree(r.qualifiedParent);
    for(auto &b:r.bones) ClothFree(b);
    for(auto &b:r.parents) ClothFree(b);
  }
  for(auto &t:s.sharedTeams) ClothFree(t.bbc);
  for(auto &p:s.nativeProducers) {
    ClothFree(p.bbc);for(auto &r:p.rootRefs)ClothFree(r);
    for(auto &r:p.chainRefs)ClothFree(r);for(auto &r:p.chainParents)ClothFree(r);
  }
  s.nativeProducers.clear();
  for(auto &r:s.ownership){ClothFree(r.bbc);for(auto &b:r.rootRefs)ClothFree(b);}s.ownership.clear();
  for(auto &r:s.originalExcluded) {ClothFree(r.bone);ClothFree(r.parent);}s.originalExcluded.clear();s.originalExcludedList=0;
  for(auto &r:s.excludedBranches) {ClothFree(r.bone);ClothFree(r.parent);}s.excludedBranches.clear();
  for(auto &r:s.prebuildOmitted) {ClothFree(r.bone);ClothFree(r.parent);}s.prebuildOmitted.clear();
  for(auto &a:s.attachments) { ClothFree(a.bbc);ClothFree(a.root);ClothFree(a.parent); }
  s.attachments.clear();
  for(auto &c:s.additionalColliders) { ClothFree(c.ref);ClothFree(c.transform);ClothFree(c.parent); }
  s.additionalColliders.clear();
  s.holds.clear(); s.colliders.clear(); s.renderers.clear(); s.sharedTeams.clear(); s.colliderTeams.clear(); s.colliderTransforms.clear(); s.colliderParents.clear();
  s.shapeBones.clear();s.shapeProfile={};
  s.pending=s.lease=false;
  s_collisionInspect.store(true,std::memory_order_release);
  Log("[CLOTH-BONE-CLEANUP] session=%llu generation=%llu component=%s complete=1 nativeAndResourcesReleased=1",
      s.owner.session,s.owner.generation,s.profile?s.profile->component:"pending");
}
static bool ClothBoneReject(const char *reason) {
  if(!ClothBoneState().failure[0]) strncpy_s(ClothBoneState().failure,reason,_TRUNCATE);
  ClothBoneState().failed=true; ClothBoneNote(reason); return false;
}
static bool ClothBoneAttributes(void *data2,bool candidate=false) {
  const auto *profile=candidate?&ClothBoneCandidate(ClothBoneState()):ClothBoneState().profile;
  void *dict=nullptr,*unused=nullptr,*selection=nullptr;
  unsigned char fixed=0,move=0;
  bool edited=true,valid=true;
  if(!CollisionField(data2,"selectionData","BeyondDynamicBone.SelectionData",selection) || !selection ||
      !CollisionField(selection,"userEdit","System.Boolean",edited) || edited ||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(selection),"IsValid","System.Boolean"),selection,valid) || valid ||
      !CollisionField(data2,"boneAttributeDict","System.Collections.Generic.Dictionary<UnityEngine.Transform,BeyondDynamicBone.VertexAttribute>",dict) ||
      !dict || CollisionCount(dict)!=0 ||
      !CollisionByteFlag(SurfaceClass("BeyondDynamicBone","VertexAttribute"),"Flag_Fixed",fixed) ||
      !CollisionByteFlag(SurfaceClass("BeyondDynamicBone","VertexAttribute"),"Flag_Move",move)) return false;
  auto add=SurfaceMethod(il2cpp_object_get_class(dict),"Add","System.Void","UnityEngine.Transform","BeyondDynamicBone.VertexAttribute");
  for(int n=0;n<profile->boneCount;++n) {
    if((candidate&&profile->Passive(n)) || (!candidate&&profile->OutsideOriginal(n)))continue;
    const int attr=profile->bones[n].attribute;
    unsigned char a=attr==1 ? fixed:attr==2 ? move:0;
    void *args[]{ClothTarget(ClothBoneState().bones[n].bone),&a};
    if(!args[0] || !ClothInvoke(add,dict,args,unused)) return false;
  }
  return CollisionCount(dict)==profile->boneCount-(candidate?profile->candidateIgnoredCount:profile->inputAnchorCount+profile->sourceBranchCount);
}
static bool ClothBoneLocalDistanceConfigure(void *data,void *original,float stiffness);
static bool ClothBoneLocalDistanceParameters(void *box,float (&curve)[16],float &attenuation);
#include "cloth_bonecloth_coat_policy.h"
#include "cloth_bonecloth_elastic.h"
#include "cloth_bonecloth_support_policy.h"
#include "cloth_bonecloth_attachment.h"
#include "cloth_bonecloth_damping.h"
static bool ClothBoneLocalDistanceConfigure(void *data,void *original,float stiffness=ClothLocalDistanceStiffness) {
  constexpr const char *type="BeyondDynamicBone.DistanceConstraint.SerializeData";
  constexpr const char *curveType="BeyondDynamicBone.CurveSerializeData";
  void *oldConstraint=nullptr,*oldCurve=nullptr,*constraint=nullptr,*curve=nullptr;
  float oldValue=0;bool oldUse=false;
  if(!data || data==original || !std::isfinite(stiffness) || stiffness<=0 || stiffness>1 ||
      !ClothField(original,"distanceConstraint",type,oldConstraint) || !oldConstraint ||
      !ClothField(oldConstraint,"stiffness",curveType,oldCurve) || !oldCurve ||
      !ClothField(oldCurve,"value","System.Single",oldValue) || !std::isfinite(oldValue) ||
      !ClothField(oldCurve,"useCurve","System.Boolean",oldUse) ||
      !SurfaceCloneField(data,original,"distanceConstraint",type) ||
      !ClothField(data,"distanceConstraint",type,constraint) || !constraint || constraint==oldConstraint ||
      !SurfaceCloneField(constraint,oldConstraint,"stiffness",curveType) ||
      !ClothField(constraint,"stiffness",curveType,curve) || !curve || curve==oldCurve ||
      !SurfaceScalar(curve,"value","System.Single",stiffness) ||
      !SurfaceScalar(curve,"useCurve","System.Boolean",false)) return false;
  float afterValue=0;bool afterUse=false;void *afterCurve=nullptr;
  if(!ClothField(oldConstraint,"stiffness",curveType,afterCurve) || afterCurve!=oldCurve ||
      !ClothField(oldCurve,"value","System.Single",afterValue) || afterValue!=oldValue ||
      !ClothField(oldCurve,"useCurve","System.Boolean",afterUse) || afterUse!=oldUse) return false;
  Log("[CLOTH-BONE-LOCAL] stage=distance-policy-configured sourceValue=%g sourceUseCurve=%d candidateValue=%g candidateUseCurve=0 originalCurveUntouched=1 nativeParameterReadbackPending=1",
      oldValue,int(oldUse),stiffness);
  return true;
}
static bool ClothBoneContactColliderPolicy();
static bool ClothBoneCandidateColliderOmitted(const ClothBoneRuntime &s,size_t n);
#include "cloth_bonecloth_panel_policy.h"
static bool ClothBoneLongPanelRadiusConfigure(void *data,void *original) {
  const auto &s=ClothBoneState();if(!s.local.requested||!s.local.recipe||!s.local.recipe->resampledPanel)return true;
  const auto &r=*s.local.recipe;constexpr const char *type="BeyondDynamicBone.CurveSerializeData";
  void *source=nullptr,*curve=nullptr,*sourceKeys=nullptr,*afterKeys=nullptr;float before=0,after=0;bool use=false,afterUse=false;
  if(!s.profile||!eiem_cloth_asset::SourceSeraphPanel(*s.profile)||!r.runtimeGenerated||!r.sourcePanelFit||
      !std::isfinite(r.fittedContactRadius)||r.fittedContactRadius<.002f||r.fittedContactRadius>=.04f||!r.radiusCurve||
      !std::isfinite(r.contactRadiusScale)||r.contactRadiusScale<=0||r.contactRadiusScale>=1||
      !data||data==original||!ClothField(original,"radius",type,source)||!source||
      !ClothField(source,"value","System.Single",before)||!std::isfinite(before)||
      !ClothField(source,"useCurve","System.Boolean",use)||!ClothField(source,"curve","UnityEngine.AnimationCurve",sourceKeys))return false;
  for(int n=0;n<16;++n)if(!std::isfinite(r.radiusCurve[n])||r.radiusCurve[n]<=0||r.radiusCurve[n]>r.fittedContactRadius+1e-6f)return false;
  if(!SurfaceCloneField(data,original,"radius",type)||!ClothField(data,"radius",type,curve)||!curve||curve==source||
      !SurfaceScalar(curve,"value","System.Single",before*r.contactRadiusScale)||
      !ClothField(source,"value","System.Single",after)||after!=before||!ClothField(source,"useCurve","System.Boolean",afterUse)||afterUse!=use||
      !ClothField(source,"curve","UnityEngine.AnimationCurve",afterKeys)||afterKeys!=sourceKeys)return false;
  Log("[CLOTH-BONE-LONG-PANEL] stage=contact-envelope-configured maxRadius=%g scale=%g basis=all-LOD-inward-skin-offset-plus-2mm sourceCurveUntouched=1 nativeParameterReadback=pending visualVerified=0",r.fittedContactRadius,r.contactRadiusScale);
  return true;
}
static bool ClothBonePrebuildOmittedIdentity(bool capture);
static bool ClothBoneConfigure() {
  auto &s=ClothBoneState(); const auto *profile=&ClothBoneCandidate(s);
  if(s.local.requested && s.local.recipe && s.local.recipe->sourceApronFit && !ClothBoneApronPointBody(s))
    return ClothBoneReject("fixed-apron-point-policy-source-unconfirmed-original-unchanged");
  if(s.local.requested && s.profile && eiem_cloth_asset::SourceChenPanel(*s.profile) && !ClothBonePanelPointBody(s))
    return ClothBoneReject("source-waist-point-policy-unconfirmed-original-unchanged");
  void *data=ClothBoneNew("ClothSerializeData"),*data2=ClothBoneNew("ClothSerializeData2"),
      *restore=s.prebuild.captured?CollisionGc(s.data2):ClothBoneNew("ClothSerializeData2");
  if(!data || !data2 || !restore) return false;
  s.candidateData=ClothBoneHold(data); s.candidateData2=ClothBoneHold(data2);
  s.restoreData2=ClothBoneHold(restore);
  if(!s.candidateData || !s.candidateData2 || !s.restoreData2 ||
      !ClothBoneAttributes(data2,true) || (!s.prebuild.captured&&!ClothBoneAttributes(restore)) ||
      (s.prebuild.captured&&!ClothBonePrebuildPrivate(data2)) || !SurfaceCopyConfiguration(data,CollisionGc(s.data))) return false;
  if(s.local.requested && !s.local.recipe->RetainsSourceReference() && (!SurfaceScalar(data,"animationPoseRatio","System.Single",0.0f) ||
      ((!s.local.recipe->distanceCurve || ClothBoneLongPanelBending(s)) &&
      !ClothBoneLocalDistanceConfigure(data,CollisionGc(s.data),s.local.recipe->distanceStiffness)))) return false;
  if(!ClothBoneLongPanelRadiusConfigure(data,CollisionGc(s.data)))return ClothBoneReject("long-panel-contact-envelope-unconfirmed-original-unchanged");
  if(s.contactConsumer>=0 && !SurfaceScalar(data,"animationPoseRatio","System.Single",0.0f))return false;
  if(!ClothBoneSurfaceBendingConfigure(data,CollisionGc(s.data)))
    return ClothBoneReject("native-surface-bending-policy-unconfirmed-original-unchanged");
  if(!ClothBoneElasticConfigure(data,CollisionGc(s.data)))
    return ClothBoneReject("cloth-elastic-native-parameters-unconfirmed-original-unchanged");
  if(!ClothBoneSupportPolicyConfigure(data,CollisionGc(s.data)))
    return ClothBoneReject("paired-surface-response-policy-unconfirmed-original-unchanged");
  if(!ClothBoneSupportElasticConfigure(data,CollisionGc(s.data)))
    return ClothBoneReject("paired-surface-elastic-parameters-unconfirmed-original-unchanged");
  if(!ClothBoneAttachmentConfigure(data,CollisionGc(s.data)))
    return ClothBoneReject("paired-waist-output-parameters-unconfirmed-original-unchanged");
  if(s.local.requested && !ClothBoneNativeContactConfigure(data,CollisionGc(s.data)))
    return ClothBoneReject("native-cloth-contact-preflight-unavailable-original-unchanged");
  if(!ClothBoneContactColliderPolicy()) return ClothBoneReject("native-contact-collider-source-unconfirmed-original-unchanged");
  std::vector<void*> roots,ignored,colliders;
  for(int k=0;k<profile->rootCount;++k) roots.push_back(ClothTarget(s.bones[profile->roots[k]].bone));
  for(int n=0;n<profile->boneCount;++n) if(!profile->bones[n].attribute&&!profile->CandidateAncestor(n))
    ignored.push_back(ClothTarget(s.bones[n].bone));
  for(const auto &r:s.originalExcluded) ignored.push_back(ClothTarget(r.bone));
  if(!s.prebuildOmitted.empty()&&!ClothBonePrebuildOmittedIdentity(false))return false;
  for(const auto &r:s.prebuildOmitted) ignored.push_back(ClothTarget(r.bone));
  for(size_t n=0;n<s.colliders.size();++n)
    if(!ClothBoneCandidateColliderOmitted(s,n))
      colliders.push_back(ClothTarget(s.colliders[n]));
  for(auto &c:s.additionalColliders) colliders.push_back(ClothTarget(c.ref));
  if(s.local.bodyCreated) {
    auto c=ClothTarget(s.local.bodyCollider);if(!c) return false;colliders.push_back(c);
  }
  for(const auto &r:s.local.fittedBody){auto c=ClothTarget(r.collider);if(!c||!s.local.fittedBodyCreated)return false;colliders.push_back(c);}
  if(!ClothBoneSideConfigure(colliders)) return ClothBoneReject("body-side-support-list-or-producer-unconfirmed");
  void *constraint=nullptr,*list=nullptr;
  if(!CollisionList(data,constraint,list) ||
      !SurfaceEnum(data,"connectionMode","BeyondDynamicBone.RenderSetupData.BoneConnectionMode",profile->runtimeBodyOnly?"Line":profile->NativeLoopMode()?"SequentialLoopMesh":"SequentialNonLoopMesh") ||
      !SurfaceEnum(constraint,"mode","BeyondDynamicBone.ColliderCollisionConstraint.Mode",ClothBoneBodyMode(s)) ||
      !SurfaceList(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>","UnityEngine.Transform",roots) ||
      !SurfaceList(data,"ignoreFromRootBones","System.Collections.Generic.List<UnityEngine.Transform>","UnityEngine.Transform",ignored) ||
      !SurfaceList(constraint,"colliderList",CollisionListType,"BeyondDynamicBone.ColliderComponent",colliders)) return false;
  void *installedRoots=nullptr;
  if(!CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",installedRoots)||
      CollisionCount(installedRoots)!=profile->rootCount||!(s.rootLists[1]=ClothBoneHold(installedRoots)))return false;
  if(ClothBoneApronPointBody(s))
    Log("[CLOTH-BONE-APRON-POLICY] stage=configured component=%s generation=%llu command=%u bodyCollision=Point fixedRoots=4 movePoints=8 structuralFaces=%d colliders=%zu waistEdgeCollision=0 sourceGeometryUnchanged=1 sourceConstraintsRetained=1 nativeReadback=pending visualVerified=0",
        s.profile->component,s.owner.generation,s.command,profile->FaceCount(),colliders.size());
  if(ClothBonePanelPointBody(s))Log("[CLOTH-BONE-PANEL-POLICY] stage=configured component=%s generation=%llu command=%u bodyCollision=Point verifiedSourceWaists=3 internalFixedToMove=3 fixedRoots=24 movePoints=72 structuralFaces=%d colliders=%zu sourceGeometryUnchanged=1 sourceConstraintsRetained=1 nativeReadback=pending visualVerified=0",
      s.profile->component,s.owner.generation,s.command,profile->FaceCount(),colliders.size());
  if(!ClothBoneDampingConfigure(data,CollisionGc(s.data)))
    return ClothBoneReject("ribbon-damping-private-parameters-unconfirmed-original-unchanged");
  return true;
}
#include "cloth_bonecloth_binding.h"
#include "cloth_bonecloth_auto.h"
#include "cloth_bonecloth_contact.h"
#include "cloth_bonecloth_display.h"
#include "cloth_bonecloth_contact_finish.h"
#include "../collision/cloth_layer_order_runtime.h"
#include "cloth_bonecloth_body.h"
#include "cloth_bonecloth_side.h"
#include "cloth_bonecloth_support.h"
#include "cloth_bonecloth_response.h"
#include "cloth_bonecloth_response_layer.h"
#include "cloth_bonecloth_local.h"
static bool ClothBoneConstructionChildren(void *process,bool candidate);
static bool ClothBonePrepare() {
  auto &s=ClothBoneState();
  s.prepareWait=false;
  if(s.prepareStage>0) {
    eiem_cloth_rebuild::Identity current{};
    const eiem_cloth_rebuild::Identity captured{s.owner,uint64_t(uintptr_t(ClothTarget(s.bbc))),
        uint64_t(uintptr_t(CollisionGc(s.process[0]))),uint64_t(uintptr_t(CollisionGc(s.data))),uint64_t(uintptr_t(CollisionGc(s.data2)))};
    if(s.stopRequested || !ClothOwns(s.owner) || !ClothBoneCurrent(current) || !(current==captured))
      return ClothBoneReject("source-replaced-during-staged-preparation-original-unchanged");
  }
  if(s.prepareStage==0) {
    if(!ClothBoneNativeContactPreflight()) return false;
    if(!ClothOwns(s.owner) || !s_cloth.discovery.complete || !s_clothSurfaceAtBoundary ||
        !ClothBoneCaptureABI()) return ClothBoneReject("owner-discovery-or-synchronous-reference-ABI-unconfirmed");
    const int index=s.index;
    if(!s.profile || index<0 || index>=s_cloth.count ||
        !ClothBoneRootNames(s_cloth.instances[index],*s.profile))
      return ClothBoneReject("queued-asset-replaced-original-unchanged");
    if(s.profile->inputAnchorCount&&(!s.local.requested||!s.local.recipe->sourceApronFit))
      return ClothBoneReject("fixed-apron-requires-matching-visible-skin-original-unchanged");
    if(s.profile->sourceBranchCount&&(!s.local.requested||!s.local.recipe->sourceShortSides||!eiem_cloth_asset::SourceShortSides(*s.profile)))return ClothBoneReject("short-side-source-lease-unconfirmed");
    s.bones.resize(s.profile->boneCount);
    auto &i=s_cloth.instances[index]; ClothReadback r{};
    const bool healthy=i.startup.phase==eiem_cloth::Phase::Ready && ClothRead(i,r) && r.state.valid && r.state.running &&
        r.state.active && r.state.enabled && !r.state.skip && !r.state.culled && !r.state.paused &&
        r.state.weightKnown && r.state.weightAtTarget;
    const auto admission=s.sourceAdmission.observe(healthy,ClothFrame(),GetTickCount64(),s.deadline);
    if(admission!=eiem_playback::SourceReadiness::Ready) {
      if(admission==eiem_playback::SourceReadiness::Expired)return ClothBoneReject("original-skirt-not-healthy-ready");
      s.prepareWait=true;ClothBoneNote("waiting-original-cloth-ready-before-capture");return true;
    }
    Log("[CLOTH-BONE-PROFILE] component=%s signature=%s prefab=%s roots=%d depth=%d loop=%d vertices=%d faces=%d nativeFixedForks=%d nativeGraphConfirmed=0",
        s.profile->component,s.profile->signature,s.profile->prefabSha,s.profile->rootCount,s.profile->depth,
        s.profile->loop,s.profile->EffectiveCount(),s.profile->FaceCount(),s.profile->runtimeFixedForks);
    i.last=r; s.index=index;
    if(s.bbc.handle && !(s.bbc.id==i.ref.id)) return ClothBoneReject("queued-BBC-identity-replaced");
    if(!s.bbc.handle) s.bbc=ClothProtect(ClothTarget(i.ref));
    s.transform=ClothProtect(CollisionTransform(ClothTarget(i.ref)));
    eiem_cloth_rebuild::Identity id{};
    if(!s.bbc.handle || !s.transform.handle || !ClothBoneCurrent(id)) return ClothBoneReject("source-identity-unreadable");
    s.process[0]=ClothBoneHold(r.process); s.data=ClothBoneHold(r.serialize); s.data2=ClothBoneHold((void*)uintptr_t(id.data2));
    s.team[0]=r.team; s.originalEnabled=r.state.enabled;
    bool building=true,pending=true,created=false; void *callback=nullptr;
    void *constraint=nullptr,*list=nullptr;
    char type[64]{},mode[64]{},connection[64]{};
    if(!s.process[0] || !s.data || !s.data2 || !CollisionField(r.process,"isBuild","System.Boolean",building) || building ||
        !ClothBoneTeleport(pending,created) || pending ||
        !CollisionField(ClothTarget(s.bbc),"OnBuildComplete","System.Action<System.Boolean>",callback) || callback ||
        !ClothBonePrebuildCapture())
      return ClothBoneReject("source-building-teleport-callback-or-prebuild-contract");
    if(!CollisionList(r.serialize,constraint,list) ||
        !CollisionEnum(r.serialize,"clothType","BeyondDynamicBone.ClothProcess.ClothType",type,sizeof(type)) || strcmp(type,"BoneCloth") ||
        !CollisionEnum(r.serialize,"connectionMode","BeyondDynamicBone.RenderSetupData.BoneConnectionMode",connection,sizeof(connection)) || strcmp(connection,"Line") ||
        !CollisionEnum(constraint,"mode","BeyondDynamicBone.ColliderCollisionConstraint.Mode",mode,sizeof(mode)) || strcmp(mode,"Point"))
      return ClothBoneReject("source-line-point-contract");
    if(!ClothBoneReadReference(r.process,true) || !ClothBoneGraph(r.process,true,true))
      return ClothBoneReject("asset-reference-or-labelled-original-graph-mismatch-original-unchanged");
    if(!ClothBoneCaptureOriginalExcluded()) return ClothBoneReject("authored-ignore-list-or-leaf-identity-unconfirmed-original-unchanged");
    void *roots=nullptr;
    if(!CollisionField(r.serialize,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots) ||
        CollisionCount(roots)!=s.profile->rootCount || !(s.rootLists[0]=ClothBoneHold(roots))) return ClothBoneReject("asset-root-list-changed");
    for(int n=0;n<s.profile->rootCount;++n)
      if(CollisionItem(roots,n,"UnityEngine.Transform")!=ClothTarget(s.bones[s.profile->originalRoots[n]].bone))
        return ClothBoneReject("asset-root-identity-mismatch");
    ++s.prepareStage;return true;
  }
  if(s.prepareStage==1) {
    void *constraint=nullptr,*list=nullptr;
    if(!CollisionList(CollisionGc(s.data),constraint,list) || !ClothBoneCaptureColliders(CollisionGc(s.process[0]),list,s.team[0]))
      return ClothBoneReject("collider-owner-sharing-or-output-overlap-unconfirmed-original-unchanged");
    ++s.prepareStage;return true;
  }
  if(s.prepareStage==2) {
    if(!ClothBoneCaptureRenderers()) return ClothBoneReject("asset-renderer-binding-mismatch-original-unchanged");
    ++s.prepareStage;return true;
  }
  if(s.prepareStage==3) {
    if(!ClothBoneCaptureAdditionalColliders()) return ClothBoneReject("body-collider-source-unconfirmed-original-unchanged");
    if(!ClothBoneConstructionChildren(CollisionGc(s.process[0]),false))
      return ClothBoneReject("original-child-closure-or-managed-list-ABI-unconfirmed-before-disposal");
    if(s.profile->nativeGraphCount) {
      if(!ClothBonePassiveAttachments() || (s.local.requested &&
          (!s.local.recipe->graphCount || (s.profile->candidateIgnoredCount&&!s.local.recipe->runtimeGenerated)))) return ClothBoneReject("unequal-passive-attachment-contract-unconfirmed-original-unchanged");
      s.shapeBones.assign(s.profile->bones,s.profile->bones+s.profile->boneCount);s.shapeProfile=*s.profile;
      if(s.profile->candidateAttributes) {
        if(!eiem_cloth_asset::SourceInactiveCoat(*s.profile))return ClothBoneReject("source-candidate-selection-unconfirmed");
        for(int n=0;n<s.profile->boneCount;++n)s.shapeBones[n].attribute=s.profile->CandidateAttribute(n);
        Log("[CLOTH-BONE-COAT-INPUT] stage=prepared component=%s frame=%d generation=%llu command=%u promotedInvalid=6 sourceFixed=6 sourceMove=12 sourceInvalid=6 candidateFixed=6 candidateMove=18 originalSelectionUnchanged=1 hierarchyAndSkinWrites=0 nativeAttributesReadback=pending",
            s.profile->component,ClothFrame(),s.owner.generation,s.command);
      }
      for(int n=0;n<s.profile->candidateIgnoredCount;++n) s.shapeBones[s.profile->candidateIgnored[n]].attribute=0;
      if(eiem_cloth_asset::SourceForkCoatFront(*s.profile)) {
        for(int n:{1,18,45,61})s.shapeBones[n].attribute=2;
        Log("[CLOTH-BONE-COAT-INPUT] stage=prepared frame=%d generation=%llu command=%u releasedInternalFixed=4 sourceFixed=9 candidateFixed=5 sourceMove=27 candidateMove=31 sourceSelectionUnchanged=1 topAttachmentsRetained=1 nativeAttributesReadback=pending",
            ClothFrame(),s.owner.generation,s.command);
      }
      s.shapeProfile.bones=s.shapeBones.data();s.shapeProfile.candidateAttributes=nullptr;
      for(int k=0;k<s.profile->rootCount;++k) {
        const int root=s.profile->roots[k];
        if(s.profile->bones[root].parent>=0)
          Log("[CLOTH-BONE-ROOT] component=%s candidateFixed=%s actualParent=%s originalAttributePreserved=1 hierarchyWrites=0",
              s.profile->component,s.profile->bones[root].name,s.profile->bones[root].parentName);
      }
  }
  ++s.prepareStage;return true;
  }
  if(s.prepareStage==4) {
    if(!ClothBoneSupportCreate())return ClothBoneReject("paired-surface-support-preflight-failed");
    if(s.supportPreparing && !s.supportCreated)return true;
    ++s.prepareStage;return true;
  }
  if(s.prepareStage==5) {
    if(s.local.requested && !ClothBoneLocalCreate()) return ClothBoneReject("local-recipe-or-owned-resource-preflight-failed");
    if(s.local.requested && !s.local.created)return true;
    ++s.prepareStage;return true;
  }
  if(!ClothBoneConfigure()) return ClothBoneReject("independent-native-configuration-unavailable");
  for(int k=1;k<3;++k) if(!(s.process[k]=ClothBoneHold(ClothBoneNew("ClothProcess"))))
    return ClothBoneReject("fresh-process-allocation-failed-before-retirement");
  eiem_cloth_rebuild::Preconditions p{};
  p.exactMainBoundary=p.exclusiveOwner=p.snapshotComplete=p.configurationIndependent=p.selectionPreserved=true;
  p.constructionReferenceVerified=p.restoreRecipeVerified=p.pendingTeleportKnown=p.sourceReady=p.sourceBuildingKnown=true;
  p.sourceBuilding=false;
  eiem_cloth_rebuild::Identity id{};
  if(!ClothBoneCurrent(id) || !s.tx.Begin(id,p,uint64_t(uintptr_t(CollisionGc(s.restoreData2))))) return ClothBoneReject("transaction-preflight-rejected");
  const auto &candidate=ClothBoneCandidate(s);
  int fixedCount=0;for(int n=0;n<candidate.boneCount;++n)if(candidate.bones[n].attribute==1)++fixedCount;
  Log("[CLOTH-BONE-PREPARED] backend=%u generation=%llu owner=%p instance=%d Process=%p config=%p data2=%p roots=%d fixed=%d move=%d ignored=%d oldLines=%zu candidateFaces=%d colliderCount=%zu loop=%d renderer=original solver=native",
      s.owner.backend,(unsigned long long)s.owner.generation,(void*)s.owner.character,s.bbc.id.instance,CollisionGc(s.process[0]),CollisionGc(s.data),(void*)uintptr_t(id.data2),
      candidate.rootCount,fixedCount,candidate.EffectiveCount()-fixedCount,
      candidate.boneCount-candidate.EffectiveCount(),s.oldLines.size(),candidate.FaceCount(),s.colliders.size(),candidate.loop);
  return true;
}

static bool ClothBoneSavePose() {
  auto &s=ClothBoneState();
  eiem_cloth_surface::OutputMatrix cloth{};
  if(!SurfaceOutputMatrix(ClothTarget(s.transform),cloth) || !eiem_cloth_rebuild::UniformPositive(cloth)) return false;
  double oldInverseScale2=0,currentScale2=0;
  for(int k=0;k<3;++k) { oldInverseScale2+=s.referenceInverse.v[k]*s.referenceInverse.v[k]; currentScale2+=cloth.v[k]*cloth.v[k]; }
  const double factor=std::sqrt(oldInverseScale2*currentScale2);
  for(size_t n=0;n<s.bones.size();++n) {
    auto &r=s.bones[n];
    auto t=ClothTarget(r.bone);
    if(!t || CollisionParent(t)!=ClothTarget(r.parent) ||
        !SurfaceVisiblePose(t,r.savedPosition,r.savedRotation,r.savedScale)) return false;
    eiem_cloth_surface::OutputMatrix m{};
    if(!SurfaceOutputMatrix(t,m) || !(n<size_t(s.profile->boneCount)&&s.profile->Passive(int(n))?
        eiem_cloth_rebuild::OrthogonalPositive(m):eiem_cloth_rebuild::UniformPositive(m))) return false;
    const double reference[]{r.referenceScale.x,r.referenceScale.y,r.referenceScale.z};
    for(int axis=0;axis<3;++axis) {
      const double expected=reference[axis]*factor;
      const double actual=std::sqrt(m.v[axis*4]*m.v[axis*4]+m.v[axis*4+1]*m.v[axis*4+1]+m.v[axis*4+2]*m.v[axis*4+2]);
      if(!std::isfinite(expected) || expected<=0 || std::fabs(actual-expected)>expected*.001) return false;
    }
  }
  return true;
}
#include "cloth_bonecloth_panel_restore.h"
static bool ClothBoneRestorePose() {
  bool ok=true;
  for(auto &r:ClothBoneState().bones) if(r.touched) {
    auto t=ClothTarget(r.bone); Vector3 p{},scale{}; Quaternion q{};
    const bool restored=t && CollisionParent(t)==ClothTarget(r.parent) &&
        SurfaceCall(t,"set_localPosition","UnityEngine.Vector3",&r.savedPosition) &&
        SurfaceCall(t,"set_localRotation","UnityEngine.Quaternion",&r.savedRotation) &&
        SurfaceCall(t,"set_localScale","UnityEngine.Vector3",&r.savedScale) &&
        SurfaceVisiblePose(t,p,q,scale) && SurfaceVisibleSame(p,r.savedPosition) &&
        SurfaceVisibleSame(q,r.savedRotation) && SurfaceVisibleSame(scale,r.savedScale);
    if(restored) r.touched=false;
    ok &= restored;
  }
  ClothBoneState().referenceRestored=ok; return ok;
}
static bool ClothBoneWorldReference(void *bone,eiem_cloth_rebuild::ReferencePose &out) {
  Vector3 p{};Quaternion q{};
  if(!bone||!ClothValue(SurfaceMethod(g_transformClass,"get_position","UnityEngine.Vector3"),bone,p)||
      !ClothValue(SurfaceMethod(g_transformClass,"get_rotation","UnityEngine.Quaternion"),bone,q)||
      !std::isfinite(p.x)||!std::isfinite(p.y)||!std::isfinite(p.z))return false;
  const eiem_cloth_rebuild::ReferencePose value{{p.x,p.y,p.z},{q.x,q.y,q.z,q.w}};
  if(!eiem_cloth_rebuild::UnitQuaternion(value.q))return false;
  out=value;return true;
}
static bool ClothBoneSeedPose() {
  auto &s=ClothBoneState();
  for(auto &r:s.bones) {
    r.touched=true; s.referenceRestored=false;
    auto bone=ClothTarget(r.bone);
    if(!bone || CollisionParent(bone)!=ClothTarget(r.parent) ||
        !SurfaceCall(bone,"set_localPosition","UnityEngine.Vector3",&r.local) ||
        !SurfaceCall(bone,"set_localRotation","UnityEngine.Quaternion",&r.rotation)) return false;
  }
  for(auto &r:s.bones) {
    auto bone=ClothTarget(r.bone);Vector3 local{},scale{};Quaternion localQ{};
    if(!bone || CollisionParent(bone)!=ClothTarget(r.parent) ||
        !SurfaceVisiblePose(bone,local,localQ,scale) ||
        !ClothSameLocal(local,localQ,r.local,r.rotation) || !SurfaceVisibleSame(scale,r.savedScale)) return false;
    // Initialize caches Unity's float world pose. A second parent-matrix
    // calculation can round differently after the outgoing model is moved or
    // hidden during a team change. Read the seeded old instance directly;
    // the strict parent/local/world checks after Initialize remain unchanged.
    if(!ClothBoneWorldReference(bone,r.expected)) return false;
  }
  return true;
}

static bool ClothBoneChildListRead(void *list,int index,std::vector<int> &out) {
  const char *type="Unity.Collections.FixedList512Bytes<System.Int32>";
  void *box=nullptr,*args[]{&index};
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(list),"get_Item",type,"System.Int32"),list,args,box) || !box) return false;
  auto cls=il2cpp_object_get_class(box); uint32_t alignment=0;
  if(!CollisionType(il2cpp_class_get_type(cls),type) || il2cpp_class_value_size(cls,&alignment)!=512) return false;
  alignas(16) unsigned char copy[512]{}; memcpy(copy,(char*)box+16,512);
  int count=-1;
  if(!ClothValue(SurfaceMethod(cls,"get_Length","System.Int32"),copy,count) || count<0 || count>127) return false;
  auto get=SurfaceMethod(cls,"get_Item","System.Int32","System.Int32"); out.clear();
  for(int n=0;n<count;++n) {
    int id=0; void *a[]{&n},*v=nullptr;
    if(!ClothInvoke(get,copy,a,v) || !ClothInputCopyBox(v,"System.Int32",&id,4)) return false;
    out.push_back(id);
  }
  return true;
}
static bool ClothBoneConstructionChildren(void *process,bool candidate) {
  if(!candidate&&ClothBoneState().prebuild.captured)return true;
  auto &s=ClothBoneState(); bool building=true; void *setup=nullptr,*list=nullptr,*transforms=nullptr;
  const char *element="Unity.Collections.FixedList512Bytes<System.Int32>";
  const char *type="System.Collections.Generic.List<Unity.Collections.FixedList512Bytes<System.Int32>>";
  if(!s_clothSurfaceAtBoundary || !CollisionField(process,"isBuild","System.Boolean",building) || building ||
      !CollisionField(process,"boneClothSetupData","BeyondDynamicBone.RenderSetupData",setup) || !setup ||
      !CollisionField(setup,"transformChildIdList",type,list) || !list ||
      !CollisionField(setup,"transformList","System.Collections.Generic.List<UnityEngine.Transform>",transforms)) return false;
  const int count=CollisionCount(transforms);
  if(!s.profile || count!=(candidate?ClothBoneCandidate(s).EffectiveCount()+1:s.profile->OriginalCount()+1) || CollisionCount(list)!=count) return false;
  auto lc=il2cpp_object_get_class(list);
  auto get=SurfaceMethod(lc,"get_Item",element,"System.Int32");
  auto set=SurfaceMethod(lc,"set_Item","System.Void","System.Int32",element);
  void *first=nullptr; int zero=0; void *firstArgs[]{&zero}; uint32_t alignment=0;
  if(!set || !ClothInvoke(get,list,firstArgs,first) || !first) return false;
  auto valueClass=il2cpp_object_get_class(first);
  auto add=SurfaceMethod(valueClass,"Add","System.Void","System.Int32&");
  if(!add || !CollisionType(il2cpp_class_get_type(valueClass),element) ||
      il2cpp_class_value_size(valueClass,&alignment)!=512) return false;
  std::vector<int> ids,excluded; std::vector<std::vector<int>> children(count),filtered;
  for(int n=0;n<count;++n) {
    auto t=CollisionItem(transforms,n,"UnityEngine.Transform"); int id=0;
    if(!t || !ClothValue(SurfaceMethod(il2cpp_object_get_class(t),"GetInstanceID","System.Int32"),t,id) ||
        !ClothBoneChildListRead(list,n,children[n])) return false;
    ids.push_back(id);
  }
  if(candidate) for(int n=0;n<s.profile->boneCount;++n) if(!ClothBoneCandidate(s).bones[n].attribute) excluded.push_back(s.bones[n].bone.id.instance);
  for(const auto &r:s.originalExcluded) {
    if(!ClothTarget(r.bone)||CollisionParent(ClothTarget(r.bone))!=ClothTarget(r.parent))return false;
    excluded.push_back(r.bone.id.instance);
  }
  if(candidate)for(const auto &r:s.prebuildOmitted){
    if(!s.prebuild.captured||!ClothTarget(r.bone)||CollisionParent(ClothTarget(r.bone))!=ClothTarget(r.parent))return false;
    excluded.push_back(r.bone.id.instance);
  }
  if(!eiem_cloth_rebuild::ConstructionChildren(ids,children,excluded,filtered)) {
    Log("[CLOTH-BONE-CHILDREN] candidate=%d Process=%p transforms=%d excluded=%zu closureConfirmed=0 childListWrites=0",
        candidate,process,count,excluded.size()); return false;
  }
  int removed=0;
  for(int n=0;n<count;++n) if(filtered[n]!=children[n]) {
    if(!candidate && !s.originalExcluded.empty()) continue;
    if(!candidate || process!=CollisionGc(s.process[1]) || !s.reference[1] ||
        s.tx.phase!=eiem_cloth_rebuild::Phase::BuildCandidate) return false;
    alignas(16) unsigned char value[512]{};
    void *unused=nullptr;
    for(int id:filtered[n]) { void *args[]{&id}; if(!ClothInvoke(add,value,args,unused)) return false; }
    void *args[]{&n,value};
    if(!ClothInvoke(set,list,args,unused)) return false;
    std::vector<int> actual;
    if(!ClothBoneChildListRead(list,n,actual) || actual!=filtered[n]) return false;
    removed+=int(children[n].size()-filtered[n].size());
  }
  Log("[CLOTH-BONE-CHILDREN] candidate=%d Process=%p transforms=%d excluded=%zu removedChildLinks=%d closureConfirmed=1 isBuild=0",
      candidate,process,count,excluded.size(),removed);
  return true;
}
static void ClothBoneBuildImpl(int slot) {
  auto &s=ClothBoneState(); bool known=false,accepted=false;
  if(slot==2 && s.prebuild.captured) {
    int before=-1,after=-1;
    auto bbc=ClothTarget(s.bbc);
    const bool initialized=ClothBonePrebuildIdentity()&&ClothBonePrebuildCount(before)&&
        SurfaceCall(bbc,"Initialize");
    s.prebuild.restored=initialized&&ClothBonePrebuildCount(after)&&
        eiem_cloth_rebuild::PrebuildReferenceDelta(before,after,1)&&ClothBonePrebuildFlag(CollisionGc(s.process[slot]),true);
    if(s.prebuild.restored)known=ClothValue(SurfaceMethod(il2cpp_object_get_class(bbc),"BuildAndRun","System.Boolean"),bbc,accepted);
    s.reference[slot]=s.prebuild.restored;s.tx.BuildResult(known,accepted);
    Log("[CLOTH-BONE-PREBUILD] stage=restore-build component=%s Process=%p source=%p before=%d after=%d nativeIncrementConfirmed=%d returnKnown=%d accepted=%d referencePending=1",
        s.profile->component,CollisionGc(s.process[slot]),CollisionGc(s.prebuild.shared),before,after,s.prebuild.restored,known,accepted);
    return;
  }
  if(ClothBoneSavePose() && ClothBoneSaveInputLease(slot) && ClothBonePlanPanelReturn(slot) && ClothBoneSeedPose()) {
    auto bbc=ClothTarget(s.bbc);
    s.reference[slot]=bbc && SurfaceCall(bbc,"Initialize") &&
        ClothBoneReadReference(CollisionGc(s.process[slot]),false);
    if(s.reference[slot] && ClothBoneConstructionChildren(CollisionGc(s.process[slot]),slot==1)) {
      known=ClothValue(SurfaceMethod(il2cpp_object_get_class(bbc),"BuildAndRun","System.Boolean"),bbc,accepted);
      s.reference[slot]=ClothBoneReadReference(CollisionGc(s.process[slot]),false);
    } else s.reference[slot]=false;
  }
  s.tx.BuildResult(known,accepted);
  Log("[CLOTH-BONE-BUILD] slot=%d frame=%d Process=%p returnKnown=%d accepted=%d referenceReadback=%d referenceSpace=actual-parent-local selection=fresh-positions-candidate-attributes-readback-required",
      slot,ClothFrame(),CollisionGc(s.process[slot]),known,accepted,s.reference[slot]);
}
static bool ClothBoneBuild(int slot) {
  __try { __try { ClothBoneBuildImpl(slot); }
    __finally { ClothBoneRestorePose(); }
  } __except(EXCEPTION_EXECUTE_HANDLER) { ClothBoneReject("native-reference-capture-exception"); }
  return ClothBoneState().referenceRestored && ClothBoneState().reference[slot];
}

static bool ClothBoneAbsent(void *manager,const char *field,const char *type,void *process) {
  void *list=nullptr; bool member=true;
  return CollisionField(manager,field,type,list) && list &&
      CollisionContains(list,process,"BeyondDynamicBone.ClothProcess",member) && !member;
}
struct ClothBoneQueueRead {
  unsigned known=0,present=0;
  bool Empty() const { return known==15 && !present; }
};
static ClothBoneQueueRead ClothBoneReadQueues(void *process,void *manager=nullptr) {
  ClothBoneQueueRead r{};
  if(!process || (!manager && !ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager))) return r;
  const char *fields[]{"parameterDirtyList","skipWritingDirtyList","codeParameterDirtyList","disposeProcessList"};
  for(unsigned n=0;n<4;++n) {
    void *list=nullptr;bool member=true;
    const char *type=n==2?"System.Collections.Generic.HashSet<BeyondDynamicBone.ClothProcess>":
        "System.Collections.Generic.List<BeyondDynamicBone.ClothProcess>";
    if(!CollisionField(manager,fields[n],type,list) || !list ||
        !CollisionContains(list,process,"BeyondDynamicBone.ClothProcess",member)) continue;
    r.known|=1u<<n;if(member)r.present|=1u<<n;
  }
  return r;
}
static int ClothBoneQueueOccurrences(void *process,const char *field) {
  void *manager=nullptr,*list=nullptr;
  if(!ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager)||
      !CollisionField(manager,field,"System.Collections.Generic.List<BeyondDynamicBone.ClothProcess>",list)||!list)return -1;
  const int count=CollisionCount(list);if(count<0||count>4096)return -1;int matches=0;
  for(int n=0;n<count;++n){auto entry=CollisionItem(list,n,"BeyondDynamicBone.ClothProcess");if(!entry)return -1;matches+=entry==process;}
  return matches;
}
static bool ClothBoneRegistered(void *process,int &team,bool restoring);
static bool ClothBoneDisabledCandidateQueues(void *process,const ClothBoneQueueRead &r) {
  auto &s=ClothBoneState();bool enabled=true,building=true;
  if(process!=CollisionGc(s.process[1])||!s.lease||!s.tx.candidate.issued||s.tx.disposeCandidateIssued||
      CollisionGc(s.candidateData)==CollisionGc(s.data)||!CollisionGc(s.candidateData)||
      !ClothValue(s_clothUnity.getEnabled,ClothTarget(s.bbc),enabled)||
      !CollisionField(process,"isBuild","System.Boolean",building))return false;
  if(enabled||building)return false;
  int team=-1;void *clothManager=nullptr;bool managerValid=false;
  if(!ClothBoneRegistered(process,team,false)||team!=s.team[1]||
      !ClothContactManager("get_Cloth","BeyondDynamicBone.ClothManager",clothManager)||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(clothManager),"IsValid","System.Boolean"),clothManager,managerValid)||!managerValid||
      !ClothBoneMethodFingerprint("ClothManager","RemoveCloth","System.Void","BeyondDynamicBone.ClothProcess",307,0xb4a61138e616cc25ULL))return false;
  const int parameters=ClothBoneQueueOccurrences(process,"parameterDirtyList"),skip=ClothBoneQueueOccurrences(process,"skipWritingDirtyList");
  if(!eiem_cloth_rebuild::DisabledCandidateQueueRetirable(true,!enabled,!building,r.known,r.present,parameters,skip))return false;
  Log("[CLOTH-BONE-QUEUE-RETIRE] component=%s Process=%p frame=%d disabled=1 parameterEntries=%d skipEntries=%d path=Dispose-RemoveCloth-single-entry queueWrites=0 retirementReadback=pending",
      s.profile?s.profile->component:"pending",process,ClothFrame(),parameters,skip);
  return true;
}
static eiem_cloth_rebuild::Verdict ClothBoneDrainBeforeDisable(void *process,uint64_t now,bool candidate=false) {
  auto &s=ClothBoneState();const auto r=ClothBoneReadQueues(process);
  if(r.Empty()) {s.queueDrainDeadline=0;s.queueWaitState=0;return eiem_cloth_rebuild::Verdict::Ready;}
  if(candidate&&ClothBoneDisabledCandidateQueues(process,r)){s.queueDrainDeadline=0;s.queueWaitState=0;return eiem_cloth_rebuild::Verdict::Ready;}
  if(!s.queueDrainDeadline)s.queueDrainDeadline=now+750;
  const unsigned state=1u|(r.known<<1)|(r.present<<5);
  if(state!=s.queueWaitState) {
    s.queueWaitState=state;
    Log("[CLOTH-BONE-QUEUE-DRAIN] component=%s Process=%p frame=%d known=0x%x pending=0x%x originalEnabledRetained=1 globalQueueWrites=0",
        s.profile?s.profile->component:"pending",process,ClothFrame(),r.known,r.present);
  }
  return now>=s.queueDrainDeadline?eiem_cloth_rebuild::Verdict::Failed:eiem_cloth_rebuild::Verdict::Waiting;
}
static eiem_cloth_rebuild::Retirement ClothBoneRetirement(int slot) {
  auto &s=ClothBoneState(); eiem_cloth_rebuild::Retirement r{};
  auto process=CollisionGc(s.process[slot]); r.process=uint64_t(uintptr_t(process));
  r.frame=ClothFrame(); r.exactMainBoundary=s_clothSurfaceAtBoundary;
  bool building=true,pending=true,created=true; void *manager=nullptr;
  int actualTeam=-1;
  r.readable=process && CollisionField(process,"isBuild","System.Boolean",building) &&
      CollisionField(process,"isDestoryInternal","System.Boolean",r.disposed) &&
      ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"get_TeamId","System.Int32"),process,actualTeam);
  if(actualTeam>0) s.team[slot]=actualTeam;
  r.buildEnded=!building; r.teamAbsent=SurfaceUnregistered(process,s.team[slot]);
  if(ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager)) {
    r.monitoringAbsent=ClothBoneAbsent(manager,"monitoringProcessSet","System.Collections.Generic.HashSet<BeyondDynamicBone.ClothProcess>",process);
    r.dirtyQueuesAbsent=ClothBoneReadQueues(process,manager).Empty();
  }
  r.colliderTeamsAbsent=ClothBoneColliderTeamsAbsent(s.team[slot]);
  r.teleportReleased=ClothBoneTeleport(pending,created) && !created;
  r.pendingTeleportCleared=!pending;
  if(slot==0&&s.prebuild.captured)r.readable=r.readable&&s.prebuild.retired;
  const unsigned state=1u|unsigned(r.readable)<<1|unsigned(r.buildEnded)<<2|unsigned(r.disposed)<<3|
      unsigned(r.teamAbsent)<<4|unsigned(r.monitoringAbsent)<<5|unsigned(r.dirtyQueuesAbsent)<<6|
      unsigned(r.colliderTeamsAbsent)<<7|unsigned(r.teleportReleased)<<8|unsigned(r.pendingTeleportCleared)<<9;
  if(s.retirementState[slot]!=state) {
    s.retirementState[slot]=state;
    const auto q=ClothBoneReadQueues(process,manager);
    Log("[CLOTH-BONE-RETIRE-READBACK] component=%s slot=%d Process=%p frame=%d team=%d readable=%d buildEnded=%d disposed=%d teamAbsent=%d monitoringAbsent=%d queueKnown=0x%x queuePending=0x%x colliderTeamsAbsent=%d teleportReleased=%d pendingTeleportCleared=%d complete=%d",
        s.profile?s.profile->component:"pending",slot,process,r.frame,s.team[slot],r.readable,r.buildEnded,r.disposed,
        r.teamAbsent,r.monitoringAbsent,q.known,q.present,r.colliderTeamsAbsent,r.teleportReleased,r.pendingTeleportCleared,r.Complete());
  }
  return r;
}
static bool ClothBoneRegistered(void *process,int &team,bool restoring=false) {
  void *manager=nullptr,*dict=nullptr,*registered=nullptr;
  if(!process || !ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"get_TeamId","System.Int32"),process,team) || team<=0 ||
      !ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager) ||
      !CollisionField(manager,"clothProcessDict","System.Collections.Generic.Dictionary<System.Int32,BeyondDynamicBone.ClothProcess>",dict) || !dict) return false;
  void *args[]{&team};
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(dict),"get_Item","BeyondDynamicBone.ClothProcess","System.Int32"),dict,args,registered) || registered!=process) return false;
  return ClothBoneColliderIdentity(process,team,restoring);
}
static bool ClothBoneMode(int slot,bool teamReadback) {
  auto &s=ClothBoneState(); void *constraint=nullptr,*list=nullptr,*field=nullptr;
  if(slot==1 && s.local.requested && s.local.recipe && s.local.recipe->sourceApronFit && !ClothBoneApronPointBody(s))return false;
  if(slot==1 && s.local.requested && s.profile && eiem_cloth_asset::SourceChenPanel(*s.profile) && !ClothBonePanelPointBody(s))return false;
  int point=-1,edge=-1,processMode=-2,actual=-3;
  if(!CollisionList(CollisionGc(slot==1?s.candidateData:s.data),constraint,list) ||
      !CollisionModeMetadata(il2cpp_object_get_class(constraint),field,point,edge) ||
      !CollisionProcessMode(CollisionGc(s.process[slot]),processMode) ||
      processMode!=ClothBoneExpectedBodyMode(s,slot,point,edge)) return false;
  if(!teamReadback) return true;
  if(slot==1 && s.contactConsumer>=0) {
    void *team=nullptr;float ratio=NAN;
    if(!ClothBoneContactTeam(s.team[slot],CollisionGc(s.process[slot]),team)||
        !ClothInputTeamField(team,"animationPoseRatio","System.Single",ratio)||!std::isfinite(ratio)||fabsf(ratio)>1e-6f)return false;
  }
  void *manager=nullptr; ClothInputArray parameters{};
  if(!ClothContactManager("get_Team","BeyondDynamicBone.TeamManager",manager) ||
      !ClothInputArrayOpen(manager,"parameterArray","BeyondDynamicBone.ClothParameters",parameters))return false;
  auto box=ClothInputArrayBox(parameters,s.team[slot]);
  return CollisionParameterMode(box,actual) && actual==processMode &&
      (slot!=1 || (ClothBoneSupportPolicyMatches(s,box) && ClothBoneSupportElasticMatches(s,box))) &&
      ClothBoneAttachmentMatches(s,slot,box)&&ClothBoneContactEnvelopeMatches(s,slot,box)&&ClothBoneSurfaceBendingMatches(s,slot,box)&&ClothBoneLongPanelMaterialMatches(s,slot,box);
}
static bool ClothBoneAdopt(int slot) {
  auto &s=ClothBoneState();
  if(s.adopted[slot]) return true;
  if(s.index<0 || s.index>=s_cloth.count || !(s.owner==s_cloth.owner)) return false;
  auto &i=s_cloth.instances[s.index];
  if(!(i.ref.id==s.bbc.id)) return false;
  if(!ClothBoneShareAdopt(CollisionGc(s.process[slot]),s.team[slot]))return false;
  if(!ClothBoneProducerAdopt(slot))return false;
  auto p=il2cpp_gchandle_new(CollisionGc(s.process[slot]),false);
  auto d=il2cpp_gchandle_new(CollisionGc(slot==1?s.candidateData:s.data),false);
  if(!p || !d) { if(p) il2cpp_gchandle_free(p); if(d) il2cpp_gchandle_free(d); return false; }
  if(i.processHandle) il2cpp_gchandle_free(i.processHandle);
  if(i.weightSerializeHandle) il2cpp_gchandle_free(i.weightSerializeHandle);
  i.processHandle=p; i.weightSerializeHandle=d;
  i.weightWriter={}; i.writerProcessClass=nullptr;
  i.changedSkip=i.capturedSkip=false;
  i.startup={}; i.startup.buildSent=true; i.startup.enableSent=true;
  i.nextPoll=0; i.colliderDeadline=0; s.adopted[slot]=true;
  ClothInputClear();
  return true;
}
static void ClothBoneReleaseOne(const char *reason) {
  if(!ClothOnMainThread() || !ClothBoneState().pending) return;
  ClothBoneState().tx.Cancel(GetTickCount64()); ClothBoneNote(reason);
  if(!ClothBoneState().lease) { ClothBoneState().pending=false; ClothBoneDropReferences(); }
}
static bool ClothBoneDestroyed() {
  auto &s=ClothBoneState(); void *obj=nullptr;
  if(ClothInspect(s.bbc,obj)!=ClothLife::Destroyed) return false;
  bool drained=true;
  for(int slot=0;slot<3;++slot) {
    if(!s.process[slot]) continue;
    const auto token=uint64_t(uintptr_t(CollisionGc(s.process[slot])));
    if(slot && token!=s.tx.installed.process &&
        token!=s.tx.candidate.identity.process && token!=s.tx.restoration.identity.process) continue;
    const auto r=ClothBoneRetirement(slot);
    drained &= r.Complete();
  }
  if(!drained) { ClothBoneNote("destroyed-owner-awaiting-native-registration-drain"); return true; }
  s.supportOwnerDrained=true;
  ClothBoneNote("destroyed-owner-drained-no-new-character-write"); ClothBoneDropReferences(); return true;
}
#include "cloth_bonecloth_owned.h"
static void ClothBoneBoundaryImpl() {
  using namespace eiem_cloth_rebuild;
  auto &s=ClothBoneState(); const auto now=GetTickCount64(); const int frame=ClothFrame();
  if(!s.pending || !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 || frame<0 || s.frame==frame) return;
  if(!s.stopRequested&&ClothOwns(s.owner)&&!s.local.cleanup&&!s.supportCleanup&&
      (s.tx.phase==Phase::Idle||s.tx.phase==Phase::Prepared)&&!ClothPreparationTurn())return;
  s.frame=frame;
  if(s.profile&&s.profile->runtimeUnowned){ClothBoneOwnedBoundary();return;}
  if(s.local.cleanup || s.supportCleanup) { ClothBoneDropReferences();return; }
  if(s.stopRequested) s.tx.Cancel(now);
  if(s.bbc.handle && ClothBoneDestroyed()) return;
  if(!s.referenceRestored && !ClothBoneRestorePose()) { ClothBoneNote("temporary-reference-restore-pending"); return; }
  if(!ClothOwns(s.owner)) s.tx.Cancel(now);
  if(s.tx.phase==Phase::Idle) {
    if(!ClothOwns(s.owner)) {
      ClothBoneReject("owner-ended-original-unchanged"); ClothBoneDropReferences(); return;
    }
    LARGE_INTEGER start{},clock{},frequency{};
    QueryPerformanceFrequency(&frequency);QueryPerformanceCounter(&start);
    s.prepareWait=false;
    const bool prepared=AdvancePrivatePreparation(ClothBonePrepare,[&]{return s.tx.phase==Phase::Idle&&!s.prepareWait;},[&]{
      QueryPerformanceCounter(&clock);
      return frequency.QuadPart?1000.*double(clock.QuadPart-start.QuadPart)/frequency.QuadPart:4.;
    });
    if(!prepared){ClothBoneDropReferences();return;}
    if(s.tx.phase==Phase::Idle)return;
  }
  Identity current{};
  if(!ClothBoneCurrent(current)) { ClothBoneNote("source-unreadable-retaining-lease"); return; }
  if(s.tx.phase==Phase::Prepared) {
    if(!(current==s.tx.original)) { ClothBoneReject("source-replaced-before-disable-original-untouched"); ClothBoneDropReferences(); return; }
    if(s.stopRequested || !ClothOwns(s.owner)) {
      ClothBoneNote("cancelled-during-preflight-original-unchanged");ClothBoneDropReferences();return;
    }
    if(s.local.requested && !s.local.ready) {
      if(!ClothBoneLocalReady()) { ClothBoneReject("local-private-input-failed-original-retained");ClothBoneDropReferences();return; }
      if(!s.local.ready) { ClothBoneNote("local-private-input-loading-original-simulation-retained");return; }
    }
    if(!ClothBoneBindingIdentity() || !ClothBoneColliderIdentity(CollisionGc(s.process[0]),s.team[0])) {
      ClothBoneReject("source-replaced-before-disable-original-untouched");ClothBoneDropReferences();return;
    }
    const auto drain=ClothBoneDrainBeforeDisable(CollisionGc(s.process[0]),now);
    if(drain!=Verdict::Ready) {
      if(drain==Verdict::Failed) {ClothBoneReject("source-parameter-queue-not-drained-original-retained");ClothBoneDropReferences();}
      else ClothBoneNote("awaiting-source-parameter-consumption-before-disable");
      return;
    }
    s.lease=true;
    if(!ClothBoneSetEnabled(false)) {
      if(++s.enableAttempts>=3) { s.tx.Cancel(now); ClothBoneNote("disable-unconfirmed-restoring-enabled"); }
      return;
    }
    if(!s.tx.Disabled(current,true,true,now)) { ClothBoneNote("disabled-source-identity-mismatch"); return; }
    if(s.stopRequested || !ClothOwns(s.owner)) s.tx.Cancel(now);
    ClothBoneNote("retiring-original-process-preserving-component-and-renderer");
  }
  if(s.tx.phase==Phase::Complete) {
    if(current==s.tx.original && ClothBoneSetEnabled(s.originalEnabled)) { ClothBoneNote("cancelled-before-disposal-original-restored"); ClothBoneDropReferences(); }
    return;
  }
  const auto phase=s.tx.phase==Phase::Retained?s.tx.retainedFrom:s.tx.phase;
  if(phase==Phase::RetireOriginal || phase==Phase::RetireCandidate) {
    const int slot=phase==Phase::RetireOriginal?0:1;
    if(slot==1 && !s.tx.disposeCandidateIssued) {
      if(!(current==s.tx.installed)) {ClothBoneNote("retirement-source-replaced-retaining-old-lease");return;}
      const auto drain=ClothBoneDrainBeforeDisable(CollisionGc(s.process[slot]),now,true);
      if(drain!=Verdict::Ready) {
        if(drain==Verdict::Failed)s.tx.Retain();
        ClothBoneNote("awaiting-candidate-parameter-consumption-before-disable");return;
      }
      if(s.tx.phase==Phase::Retained)s.tx.phase=Phase::RetireCandidate;
    }
    if(slot==1 && !ClothBoneLocalRestore()) { ClothBoneNote("local-renderer-restore-pending-before-native-retirement");return; }
    if(!(current==s.tx.installed)) { ClothBoneNote("retirement-source-replaced-retaining-old-lease"); return; }
    if(phase==Phase::RetireOriginal && s.tx.cancelled && !s.tx.disposeOriginalIssued) {
      if(ClothBoneSetEnabled(s.originalEnabled) && s.tx.FinishRestore(current,true,true)) {
        ClothBoneNote("cancelled-before-disposal-original-restored"); ClothBoneDropReferences();
      } return;
    }
    bool pending=true,created=true;
    if(!ClothBoneSetEnabled(false) || !ClothBoneTeleport(pending,created) || pending) {
      ClothBoneNote("retirement-awaiting-disabled-and-no-pending-teleport"); return;
    }
    if(created && !s.disposalResources[slot]) {
      s.disposalResources[slot]=true;
      if(!SurfaceCall(ClothTarget(s.bbc),"DisposeTeleportResources")) ClothBoneNote("teleport-dispose-command-unconfirmed");
    }
    if(!ClothBoneTeleport(pending,created) || pending || created) return;
    if(slot==1&&!s.tx.disposeCandidateIssued&&!ClothBoneCapturePanelReturn()) {ClothBoneNote("source-panel-return-capture-pending");return;}
    int prebuildBefore=-1;
    if(slot==0&&s.prebuild.captured&&!s.tx.disposeOriginalIssued) {
      bool building=true;
      if(!CollisionField(CollisionGc(s.process[0]),"isBuild","System.Boolean",building)||building||
          !ClothBonePrebuildIdentity()||!ClothBonePrebuildCount(prebuildBefore)||prebuildBefore<1) {
        if(now>=s.tx.retireDeadline)s.tx.Retain();
        ClothBoneNote("source-prebuild-awaiting-idle-and-registration-before-retirement");return;
      }
    }
    if(s.tx.ReserveDispose(current,true,true,true)) {
      const bool sent=SurfaceCall(CollisionGc(s.process[slot]),"Dispose");
      if(slot==0&&s.prebuild.captured) {
        int after=-1;
        s.prebuild.retired=ClothBonePrebuildCount(after)&&eiem_cloth_rebuild::PrebuildReferenceDelta(prebuildBefore,after,-1);
        Log("[CLOTH-BONE-PREBUILD] stage=retired component=%s Process=%p source=%p before=%d after=%d nativeDecrementConfirmed=%d sourceData2StillInstalled=1",
            s.profile->component,CollisionGc(s.process[slot]),CollisionGc(s.prebuild.shared),prebuildBefore,after,s.prebuild.retired);
      }
      Log("[CLOTH-BONE-DISPOSE] slot=%d Process=%p frame=%d sent=%d complete=0",slot,CollisionGc(s.process[slot]),frame,sent);
    }
    const auto read=ClothBoneRetirement(slot);
    if(!s.tx.ObserveRetirement(read,now)) {
      if(s.tx.phase==Phase::Retained) {
        ClothBoneNote("retirement-timeout-handles-retained");
      } return;
    }
    ClothBoneNote("old-registration-drained-on-two-frames");
  }
  if(s.tx.phase==Phase::InstallCandidate || s.tx.phase==Phase::InstallOriginal ||
      (s.tx.phase==Phase::Retained && (phase==Phase::InstallCandidate || phase==Phase::InstallOriginal))) {
    const int slot=(s.tx.phase==Phase::InstallOriginal || phase==Phase::InstallOriginal)?2:1;
    const auto data=CollisionGc(slot==1?s.candidateData:s.data), data2=CollisionGc(slot==1?s.candidateData2:s.restoreData2);
    auto process=CollisionGc(s.process[slot]); auto bbc=ClothTarget(s.bbc);
    Identity planned{s.owner,uint64_t(uintptr_t(bbc)),uint64_t(uintptr_t(process)),uint64_t(uintptr_t(data)),uint64_t(uintptr_t(data2))};
    if(!s.tx.installIssued && !s.tx.ReserveInstall(planned,current,true)) { ClothBoneNote("install-reservation-rejected"); return; }
    if(!s.tx.AllowsInstallRepair(current)) {
      ClothBoneNote("install-slot-modified-by-another-owner-retaining-lease"); return;
    }
    if(!(current==planned) && s.installAttempts[slot]>=3) { ClothBoneNote("partial-install-budget-exhausted-lease-retained"); return; }
    ++s.installAttempts[slot];
    if(!SurfaceReference(bbc,"serializeData","BeyondDynamicBone.ClothSerializeData",data) ||
        !SurfaceReference(bbc,"serializeData2","BeyondDynamicBone.ClothSerializeData2",data2) ||
        !SurfaceReference(bbc,"process","BeyondDynamicBone.ClothProcess",process) ||
        !ClothBoneCurrent(current) || !s.tx.Installed(current,true)) {
      s.tx.Retain(); ClothBoneNote("partial-install-awaiting-exact-readback"); return;
    }
    ClothBoneNote(slot==1?"candidate-installed-awaiting-native-build":"original-config-installed-awaiting-native-build");
  }
  if(s.tx.phase==Phase::BuildCandidate || s.tx.phase==Phase::BuildOriginal ||
      (s.tx.phase==Phase::Retained && s.tx.retainedFrom==Phase::BuildOriginal)) {
    const int slot=s.tx.phase==Phase::BuildCandidate?1:2;
    if(!(current==s.tx.installed)) { s.tx.Retain(); ClothBoneNote("build-identity-replaced-retaining-lease"); return; }
    if(s.tx.ReserveBuild(true,now,frame)) {
      if(!ClothBoneBuild(slot)) {
        ClothBoneReject("reference-capture-or-current-pose-restoration-failed");
        if(slot==1) s.tx.Cancel(now); else s.tx.Retain();
      } return;
    }
    BuildObservation o{}; o.identity=current; o.frame=frame; o.exactMainBoundary=true;
    auto process=CollisionGc(s.process[slot]); auto cls=il2cpp_object_get_class(process);
    o.readable=CollisionField(process,"isBuild","System.Boolean",o.building) &&
        ClothValue(SurfaceMethod(cls,"IsValid","System.Boolean"),process,o.valid) &&
        ClothValue(SurfaceMethod(cls,"IsRunning","System.Boolean"),process,o.running);
    auto result=SurfaceReadBuildResult(process); o.readable &= result.known;
    o.error=result.known && (result.error || result.cancelled);
    if(o.readable && !o.building && o.valid && o.running) {
      o.registered=ClothBoneRegistered(process,s.team[slot],slot==2);
      if(slot==2&&s.prebuild.captured) {
        s.reference[slot]=s.prebuild.restored&&ClothBonePrebuildFlag(process,true)&&ClothBoneReadReference(process,false);
        o.registered=o.registered&&s.reference[slot];
      }
      if(!s.graphChecked[slot]) { s.graphChecked[slot]=true; s.graph[slot]=ClothBoneGraph(process,slot==2); }
      o.error |= !s.graph[slot];
      o.graphVerified=s.graph[slot] && ClothBoneMode(slot,false) &&
          (!s.prebuild.captured || (ClothBonePrebuildFlag(process,slot==2) &&
              (slot!=1 || ClothBonePrebuildPrivate(CollisionGc(s.candidateData2)))));
      o.selectionVerified=s.graph[slot];
      o.referenceVerified=s.reference[slot] && s.referenceRestored;
    }
    const auto v=s.tx.ObserveBuild(o,now);
    if(v==Verdict::Failed) {
      Log("[CLOTH-BONE-BUILD-FAILED] slot=%d readable=%d building=%d valid=%d running=%d registered=%d graph=%d reference=%d resultKnown=%d result=%s",
          slot,o.readable,o.building,o.valid,o.running,o.registered,o.graphVerified,o.referenceVerified,result.known,result.name);
      ClothBoneReject(slot==1?"candidate-build-failed-restoring-original":"original-rebuild-unconfirmed-lease-retained");
      return;
    }
    if(v!=Verdict::Ready) return;
    s.enableAttempts=0;
    if(slot==2) s.modeDeadline=now+8000;
  }
  const bool lateRestore=s.tx.phase==Phase::Retained && s.tx.retainedFrom==Phase::RestoreEnabled;
  if(s.tx.phase==Phase::ActivateCandidate || s.tx.phase==Phase::RestoreEnabled || lateRestore) {
    const int slot=s.tx.phase==Phase::ActivateCandidate?1:2;
    if(slot==1 && !ClothBoneBindingIdentity()) { s.tx.Cancel(now); ClothBoneReject("asset-binding-changed-during-build-restoring"); return; }
    if(slot==2) {
      if(!s.tx.CanReturnData2(current)) { ClothBoneNote("original-data2-return-identity-mismatch-retaining-lease"); return; }
      if(current.data2!=s.tx.original.data2) {
        if(s.returnData2Attempts>=3) { ClothBoneNote("original-data2-return-budget-exhausted-retaining-lease"); return; }
        ++s.returnData2Attempts;
        if(!SurfaceReference(ClothTarget(s.bbc),"serializeData2","BeyondDynamicBone.ClothSerializeData2",CollisionGc(s.data2)) ||
            !ClothBoneCurrent(current)) { ClothBoneNote("original-data2-return-readback-pending"); return; }
      }
      if(!s.tx.ReturnedData2(current,true)) return;
    }
    bool enabled=false;
    bool componentEnabled=false;
    const bool adopted=ClothBoneAdopt(slot);
    const bool componentRestored=adopted && (lateRestore ?
        (ClothValue(s_clothUnity.getEnabled,ClothTarget(s.bbc),componentEnabled) && componentEnabled==s.originalEnabled):
        ClothBoneSetEnabled(s.originalEnabled));
    if(!componentRestored ||
        !ClothValue(SurfaceMethod(il2cpp_object_get_class(CollisionGc(s.process[slot])),"get_IsEnable","System.Boolean"),CollisionGc(s.process[slot]),enabled) || enabled!=s.originalEnabled) {
      if(!lateRestore && ++s.enableAttempts>=3) { if(slot==1) s.tx.Cancel(now); else s.tx.Retain(); }
      ClothBoneNote("native-enable-readback-pending"); return;
    }
    if(slot==1 && s.tx.Activated(current,true,true,enabled,s.graph[1] && s.reference[1])) {
      s.modeDeadline=now+8000;
      ClothBoneNote("original-BBC-enabled-checking-native-Team-parameters");
    } else if(slot==2) {
      if(!ClothBoneMode(2,true)) {
        if(now>=s.modeDeadline) { s.tx.Retain(); ClothBoneNote("original-Team-Point-readback-timeout-lease-retained"); }
        return;
      }
      if(!s.tx.FinishRestore(current,true,true)) return;
      ClothBoneNote(s.failed?"candidate-failed-original-Line-Point-local-reference-restored":
          "original-Line-Point-local-reference-selection-and-enabled-restored"); ClothBoneDropReferences(); return;
    }
  }
  if(s.tx.phase==Phase::Active && (!(current==s.tx.installed) || !ClothOwns(s.owner))) {
    s.tx.Cancel(now); ClothBoneNote("active-owner-or-exclusive-command-changed-restoring");
  }
  if(s.tx.phase==Phase::Active && s.local.requested) {
    if(!ClothBoneLocalPublish()) { s.tx.Cancel(now);ClothBoneReject("local-output-identity-or-visibility-unconfirmed-restoring"); }
  }
  if(s.tx.phase==Phase::Active && (!s.teamModeConfirmed || now>=s.nextModeAudit)) {
    s.nextModeAudit=now+100;
    const bool confirmed=ClothBoneBindingIdentity() && ClothBoneRegistered(CollisionGc(s.process[1]),s.team[1]) && ClothBoneMode(1,true);
    if(confirmed && frame!=s.modeFrame) { s.modeFrame=frame; if(s.teamModeReads<2) ++s.teamModeReads; }
    if(!confirmed) s.teamModeReads=0;
    if(!s.teamModeConfirmed && s.teamModeReads>=2) {
      s.teamModeConfirmed=true;
      if(eiem_cloth_asset::SourceInactiveCoat(*s.profile))Log("[CLOTH-BONE-COAT-INPUT] stage=Team-confirmed component=%s frame=%d generation=%llu command=%u Process=%p team=%d promotedInvalid=6 fixed=6 move=18 originalSelectionUnchanged=1 hierarchyWrites=0 waistPrivateSkin=%d visualVerified=0",
          s.profile->component,frame,s.owner.generation,s.command,CollisionGc(s.process[1]),s.team[1],s.local.requested&&s.local.recipe->CoatWaistSkinOnly());
      if(eiem_cloth_asset::SourceForkCoatFront(*s.profile))Log("[CLOTH-BONE-COAT-INPUT] stage=Team-confirmed frame=%d generation=%llu command=%u Process=%p team=%d releasedInternalFixed=4 fixed=5 move=31 hierarchyAndSkinWrites=0 sourceSelectionUnchanged=1 depthCurveSampling=native-recomputed visualVerified=0",
          frame,s.owner.generation,s.command,CollisionGc(s.process[1]),s.team[1]);
      if(eiem_cloth_asset::SourceSeparatedCoat(*s.profile))Log("[CLOTH-BONE-COAT-INPUT] stage=Team-confirmed component=%s frame=%d generation=%llu command=%u Process=%p team=%d releasedInternalFixed=%d retainedOriginalRoots=%d points=%d sourceSelectionUnchanged=1 hierarchyAndSkinWrites=0 depthCurveSampling=native-recomputed visualVerified=0",
          s.profile->component,frame,s.owner.generation,s.command,CollisionGc(s.process[1]),s.team[1],s.profile->releasedFixedCount,s.profile->rootCount,ClothBoneCandidate(s).EffectiveCount());
      if(ClothBoneForkCoat(s))Log("[CLOTH-BONE-COAT-POLICY] stage=Team-confirmed component=%s frame=%d generation=%llu command=%u Process=%p team=%d bodyCollision=%s triangleBending=0 originalStiffness=%g originalRadiusCurve=1 originalSkin=%d waistSkinTransition=%d longitudinalShapeConstraints=retained visualVerified=0",
          s.profile->component,frame,s.owner.generation,s.command,CollisionGc(s.process[1]),s.team[1],ClothBoneBodyMode(s),s.surfaceOriginalStiffness,
          !s.local.requested||(s.local.recipe&&s.local.recipe->NativeSkinRetained()),s.local.recipe&&s.local.recipe->CoatWaistSkinOnly());
      if(ClothBoneLongPanelBending(s))Log("[CLOTH-BONE-LONG-PANEL-POLICY] stage=Team-confirmed component=%s frame=%d generation=%llu command=%u Process=%p team=%d bodyCollision=Edge triangleBending=0 originalStiffness=%g points=%d faces=%d contourDistance=%g contourTetherStretch=%g sourceAngleRetained=1 colliderGeometryUnchanged=1 visualVerified=0",
          s.profile->component,frame,s.owner.generation,s.command,CollisionGc(s.process[1]),s.team[1],s.surfaceOriginalStiffness,ClothBoneCandidate(s).EffectiveCount(),ClothBoneCandidate(s).FaceCount(),
          s.local.recipe->distanceStiffness,s.local.recipe->tetherStretch);
      if(s.contactConsumer>=0)Log("[CLOTH-BONE-PARTNER] stage=native-graph-and-body-mode-confirmed component=%s points=%d faces=%d bodyCollision=%s effectiveAnimationPoseRatio=0 originalSkinRetained=1 mutualContactPending=1",
          s.profile->component,ClothBoneCandidate(s).EffectiveCount(),ClothBoneCandidate(s).FaceCount(),ClothBoneBodyMode(s));
      if(s.supportPointCollision)Log("[CLOTH-BONE-SUPPORT-POLICY] stage=Team-confirmed component=%s team=%d generation=%llu bodyCollision=Point maxDistance=0 candidateAttachmentsPreserved=1 mutualContact=FullMesh visualVerified=0",
          s.profile->component,s.team[1],(unsigned long long)s.owner.generation);
      if(ClothBoneApronPointBody(s))Log("[CLOTH-BONE-APRON-POLICY] stage=Team-confirmed component=%s team=%d generation=%llu command=%u bodyCollision=Point fixedRoots=4 movePoints=8 structuralFaces=%d waistEdgeCollision=0 nativeOutputReadbackPending=1 visualVerified=0",
          s.profile->component,s.team[1],s.owner.generation,s.command,ClothBoneCandidate(s).FaceCount());
      if(ClothBonePanelPointBody(s))Log("[CLOTH-BONE-PANEL-POLICY] stage=Team-confirmed component=%s team=%d generation=%llu command=%u bodyCollision=Point fixedRoots=24 movePoints=72 structuralFaces=%d nativeOutputReadbackPending=1 visualVerified=0",
          s.profile->component,s.team[1],s.owner.generation,s.command,ClothBoneCandidate(s).FaceCount());
      if(s.supportTether)Log("[CLOTH-BONE-SUPPORT-ELASTIC] stage=Team-confirmed component=%s team=%d generation=%llu session=%llu distance=%g stretchThreshold=%g compression=%g bending=%g velocityAttenuation=%g conversions=%u geometryUnchanged=1 visualVerified=0",
          s.profile->component,s.team[1],s.owner.generation,s.owner.session,ClothBoneSupportDistanceStiffness,
          ClothBoneSupportTetherStretch,s.supportCompression,s.supportBending,s.supportDistanceAttenuation,s.supportElasticConversions);
      if(!s.additionalColliders.empty()) Log("[CLOTH-BONE-BODY-COLLIDERS] component=%s added=%zu Process=%p team=%d registrationConfirmed=1 bodyCollision=%s contactAndVisualConfirmed=0",
          s.profile->component,s.additionalColliders.size(),CollisionGc(s.process[1]),s.team[1],ClothBoneBodyMode(s));
      ClothBoneNote(s.supportPointCollision?"active-original-BBC-paired-surface-Point-body-FullMesh-layers-visual-verification-required":
          s.profile->runtimeBodyOnly?"active-original-BBC-Line-Point-fitted-thigh-contact-visual-verification-required":
          ClothBoneApronPointBody(s)?"active-original-BBC-fixed-apron-native-Team-Point-visual-verification-required":
          ClothBonePanelPointBody(s)?"active-original-BBC-source-waist-native-Team-Point-visual-verification-required":
          ClothBoneOriginalCoverageCoat(s)?"active-original-BBC-male-coat-native-Team-Point-flexible-faces-visual-verification-required":
          ClothBoneForkCoat(s)?"active-original-BBC-fork-coat-native-Team-Point-flexible-faces-visual-verification-required":
          "active-original-BBC-asset-connections-native-Team-Edge-visual-verification-required");
    } else if(!confirmed && (s.teamModeConfirmed || now>=s.modeDeadline)) {
      s.tx.Cancel(now); ClothBoneReject("native-Team-policy-or-registration-unconfirmed-restoring-original");
    }
  }
}
static void ClothBoneActivationReadback(ClothBoneRuntime &s) {
  if(s.activationLogged || !s.requestedAt || !s.pending || s.stopRequested || !s.teamModeConfirmed ||
      s.tx.phase!=eiem_cloth_rebuild::Phase::Active || (s.local.requested && !s.local.published))return;
  s.activationLogged=true;const auto elapsed=GetTickCount64()-s.requestedAt;
  Log("[CLOTH-AUTO-LATENCY] component=%s session=%llu generation=%llu command=%u elapsedMs=%llu targetMs=1000 withinTarget=%d rendererPublished=%d nativeTeamConfirmed=1 visualVerified=0",
      s.profile?s.profile->component:"unknown",(unsigned long long)s.owner.session,(unsigned long long)s.owner.generation,
      s.command,(unsigned long long)elapsed,int(elapsed<=1000),int(s.local.published));
}
static void ClothBoneBoundaryOne() {
  if(!ClothOnMainThread()) return;
  LARGE_INTEGER begin{},end{},frequency{};
  QueryPerformanceFrequency(&frequency); QueryPerformanceCounter(&begin);
  __try { ClothBoneBoundaryImpl(); ClothBoneResponseTick(); ClothBoneActivationReadback(ClothBoneState()); }
  __except(EXCEPTION_EXECUTE_HANDLER) {
    ClothBoneRestorePose(); ClothBoneReleaseOne("native-boundary-exception-restoring-original");
  }
  QueryPerformanceCounter(&end);
  if(frequency.QuadPart) {
    const double ms=1000.0*double(end.QuadPart-begin.QuadPart)/double(frequency.QuadPart);
    auto &s=ClothBoneState(); s.cpuTotal+=ms; s.cpuMax=(std::max)(s.cpuMax,ms); ++s.cpuSamples;
    if(ms>10 || s.cpuSamples%600==0) Log("[CLOTH-BONE-COST] component=%s samples=%llu meanMs=%g maxMs=%g currentMs=%g scope=adapter-not-native-solver prepareStage=%u localStage=%u",
        s.profile?s.profile->component:"pending",(unsigned long long)s.cpuSamples,s.cpuTotal/s.cpuSamples,s.cpuMax,ms,s.prepareStage,s.local.createStage);
  }
}
static std::string ClothBoneJsonOne(const ClothBoneRuntime &s) {
  std::ostringstream out;
  out << "{\"phase\":" << int(s.tx.phase) << ",\"pending\":" << s.pending << ",\"lease\":" << s.lease
      << ",\"session\":" << s.owner.session << ",\"generation\":" << s.owner.generation
      << ",\"instance\":" << s.bbc.id.instance << ",\"issue\":" << CollisionJsonString(s.issue)
      << ",\"profile\":" << CollisionJsonString(s.profile?s.profile->signature:"")
      << ",\"component\":" << CollisionJsonString(s.profile?s.profile->component:"")
      << ",\"runtimeGenerated\":" << (s.profile&&s.profile->runtimeGenerated)
      << ",\"enhancementSource\":" << CollisionJsonString(ClothBoneSourceLabel(s.profile))
      << ",\"dependencies\":" << s.dependencies << ",\"nativeAttachments\":" << s.attachments.size()
      << ",\"additionalBodyColliders\":" << s.additionalColliders.size()
      << ",\"closedLoop\":" << (s.profile&&s.profile->loop)
      << ",\"rootCount\":" << (s.profile?s.profile->rootCount:0)
      << ",\"expectedFaces\":" << (s.profile?s.profile->FaceCount():0)
      << ",\"failure\":" << CollisionJsonString(s.failure)
      << ",\"localSkinRequested\":" << s.local.requested << ",\"localSkinPublished\":" << s.local.published
      << ",\"localNativeOutputConfirmed\":" << s.local.solverConfirmed << ",\"localRecipe\":" << CollisionJsonString(s.local.created?s.local.recipe->signature:"")
      << ",\"ribbonDamping\":" << s.ribbonDamping
      << ",\"ribbonDampingConfigured\":" << s.ribbonDampingConfigured
      << ",\"ribbonDampingConfirmed\":" << s.ribbonDampingConfirmed
      << ",\"localAddedBones\":" << (s.local.created?s.local.recipe->addedCount:0) << ",\"actualNativeEdges\":" << s.registeredEdges.size()
      << ",\"candidateGraphConfirmed\":" << s.graph[1] << ",\"restoreGraphConfirmed\":" << s.graph[2]
      << ",\"referenceRestored\":" << s.referenceRestored << ",\"visualAccepted\":false"
      << ",\"teamModeConfirmed\":" << s.teamModeConfirmed
      << ",\"teamCollisionMode\":" << CollisionJsonString(ClothBoneBodyMode(s))
      << ",\"teamEdgeModeConfirmed\":" << (s.teamModeConfirmed&&!ClothBonePointBody(s))
      << ",\"adapterSamples\":" << s.cpuSamples << ",\"adapterMeanMs\":" << (s.cpuSamples?s.cpuTotal/s.cpuSamples:0)
      << ",\"adapterMaxMs\":" << s.cpuMax << ",\"localRenderLayers\":[";
  for(size_t k=0;k<s.local.layers.size();++k) {
    auto &l=s.local.layers[k];if(k) out<<",";
    out<<"{\"renderer\":"<<CollisionJsonString(s.local.recipe->meshes[k].renderer)
       <<",\"recipe\":"<<CollisionJsonString(s.local.recipe->meshes[k].signature)
       <<",\"appendedBindings\":"<<s.local.recipe->meshes[k].bindingCount
       <<",\"borrowedOriginalTips\":"<<(s.local.recipe->meshes[k].bindingCount-s.local.recipe->addedCount)
       <<",\"ready\":"<<l.ready<<",\"published\":"<<l.published
       <<",\"restorePending\":"<<l.publishAttempted<<",\"mesh\":"<<l.mesh.id.instance<<"}";
  }
  out<<"]}";
  return out.str();
}

static eiem_cloth_rebuild::BatchNodes ClothBoneBatchNodes() {
  eiem_cloth_rebuild::BatchNodes nodes{};
  for(int n=0;n<s_clothBoneCount;++n) {
    const auto &s=s_clothBoneSlots[n];
    nodes[n]={s.dependencies,s.pending,s.lease,s.tx.phase==eiem_cloth_rebuild::Phase::Active,
        s.teamModeConfirmed,s.stopRequested||s.tx.cancelled||s.local.cleanup||s.supportCleanup,s.failed};
    nodes[n].resourcesOnly=(s.local.cleanup||s.supportCleanup) && (!s.tx.lease||s.supportOwnerDrained) &&
        s.referenceRestored && !s.local.publishAttempted &&
        (!s.lease || s.tx.phase==eiem_cloth_rebuild::Phase::Complete || s.supportOwnerDrained);
  }
  return nodes;
}
static void ClothBonePollResourceCleanup(int selected=-1) {
  const auto nodes=ClothBoneBatchNodes();
  for(int n=0;n<s_clothBoneCount;++n) if(n!=selected && nodes[n].pending && nodes[n].resourcesOnly)
    ClothBoneInSlot(n,ClothBoneBoundaryOne);
}
static void ClothBoneRelease(const char *reason) {
  s_clothPreparationBudget.cancel(s_clothActorIndex);
  if(!ClothOnMainThread()) return;
  ClothAutoCancel();
  s_clothLayerGate.Clear();
  s_clothDisplayView.Revoke();
  s_clothFinishView.Revoke();s_clothFinishChain.Clear();
  for(int n=0;n<s_clothBoneCount;++n) {
    auto &s=s_clothBoneSlots[n]; if(!s.pending && !s.lease) continue;
    s.stopRequested=true;
    if(!s_clothBoneDispatching && !s.lease) {
      const int previous=s_clothBoneContext;s_clothBoneContext=n;
      __try { ClothBoneReleaseOne(reason); } __finally { s_clothBoneContext=previous; }
    }
  }
  s_collisionInspect.store(true,std::memory_order_release);
}
static void ClothBoneService(bool pose,int frame) {
  if(!ClothOnMainThread()) return;
  if(ClothEnhancementSetting().load(std::memory_order_acquire) && !ClothBonePending() && !s_clothBoneDispatching &&
      s_clothBoneRequest.load(std::memory_order_acquire)==s_clothBoneSeen &&
      s_cloth.discovery.complete && ClothOwns(s_cloth.owner) &&
      (s_clothAutoTriedSession!=s_cloth.owner.session || s_clothAutoTriedGeneration!=s_cloth.owner.generation)) {
    s_clothBoneSession.store(s_cloth.owner.session,std::memory_order_release);
    s_clothBoneStopRequested.store(false,std::memory_order_release);
    s_clothBoneRequestedAt.store(GetTickCount64(),std::memory_order_release);
    s_clothBoneRequest.fetch_add(1,std::memory_order_acq_rel);
  }
  const auto request=s_clothBoneRequest.load(std::memory_order_acquire);
  if(request!=s_clothBoneSeen) {
    s_clothBoneSeen=request;
    bool pending=s_clothAutoDeferred;for(auto &s:s_clothBoneSlots) pending|=s.pending||s.lease;
    const bool stop=s_clothBoneStopRequested.load(std::memory_order_acquire);
    if(stop) {
      if(pending && s_clothBoneSession.load(std::memory_order_acquire)==s_clothBone.owner.session)
        ClothBoneRelease("cancel-requested-restoring-original");
    }
    else if(pending || s_clothBoneDispatching) {
      s_clothAutoTriedSession=s_clothAutoTriedGeneration=0;
      ClothBoneRelease("cancel-requested-restoring-original");
    }
    else if(s_clothBoneSession.load(std::memory_order_acquire)!=s_cloth.owner.session || !ClothOwns(s_cloth.owner)) {
      if(!ClothOwns(s_cloth.owner)) {
        s_clothAutoTriedSession=s_clothAutoTriedGeneration=0;
        ClothBoneInSlot(0,[] { ClothBoneNote("automatic-enabled-for-next-playback"); });
      } else ClothBoneInSlot(0,[] { ClothBoneNote("stale-command-or-no-active-owner"); });
    } else {
      ClothAutoCancel();s_clothAutoLease.reset();s_clothAutoSkipped=false;
      s_clothBoneSlots={};s_clothBoneCount=1;s_clothBoneResolved=false;s_clothBoneNoMatch=false;s_clothBoneMutating=-1;
      auto &s=s_clothBone;s.command=request;s.owner=s_cloth.owner;s.pending=true;s.deadline=GetTickCount64()+8000;s.local.requested=false;
      s.autoSelect=true;
      s.requestedAt=s_clothBoneRequestedAt.load(std::memory_order_acquire);
      if(s.autoSelect){s.deadline=GetTickCount64()+90000;s_clothAutoTriedSession=s.owner.session;s_clothAutoTriedGeneration=s.owner.generation;}
      ClothBoneInSlot(0,[] { ClothBoneNote("waiting-for-verified-pre-team-boundary"); });
    }
  }
  for(int n=0;n<s_clothBoneCount;++n) {
    auto &s=s_clothBoneSlots[n];if(!s.pending) continue;
    if(!ClothOwns(s.owner)) { ClothBoneRelease("owner-ended-restoring-retained-original");break; }
    if(s.tx.phase==eiem_cloth_rebuild::Phase::Idle && GetTickCount64()>=s.deadline) {
      if(s_clothAutoWaiting){ClothAutoCancel();s_clothAutoSkipped=true;s.deadline=GetTickCount64()+8000;
        Log("[CLOTH-AUTO] stage=timeout originalUnchanged=1 authoredFallback=1");continue;}
      s.stopRequested=true;
      if(!s_clothBoneDispatching) ClothBoneInSlot(n,[] { ClothBoneReleaseOne("boundary-or-dependency-timeout-original-unchanged"); });
    }
  }
  if(s_clothAutoDeferred && (!ClothOwns(s_clothBone.owner) ||
      (s_clothBone.requestedAt && GetTickCount64()-s_clothBone.requestedAt>=90000))) {
    ClothAutoCancel();Log("[CLOTH-AUTO] stage=deferred-cancelled reason=owner-ended-or-worker-deadline authoredPreserved=1");
  }
  (void)pose;(void)frame;
}
static bool ClothBoneResolveBatch() {
  if(!s_clothBone.pending || s_clothBone.stopRequested || !ClothOwns(s_clothBone.owner) || !s_cloth.discovery.complete) return false;
  if(s_clothBone.autoSelect && s_clothAutoJob &&
      eiem_cloth_asset::JobMatches(*s_clothAutoJob,s_clothBone.owner.session,s_clothBone.owner.generation,
          s_clothBone.command,s_clothBone.owner.backend,s_clothBone.owner.character) &&
      !s_clothAutoJob->done.load(std::memory_order_acquire)) {s_clothAutoWaiting=true;return false;}
  const auto owner=s_clothBone.owner;
  const auto command=s_clothBone.command;
  const auto requestedAt=s_clothBone.requestedAt;
  auto deadline=s_clothBone.deadline;
  bool localRequested=s_clothBone.local.requested;
  const bool autoSelect=s_clothBone.autoSelect;
  struct Match { int index;const ClothBoneProfile *profile; };
  std::vector<Match> found;
  for(int n=0;n<s_cloth.count;++n) for(auto profile:s_clothBoneCatalog) {
    if(!ClothBoneRootNames(s_cloth.instances[n],*profile)) continue;
    for(auto &m:found) if(m.index==n || (!strcmp(m.profile->component,profile->component) &&
        !strcmp(m.profile->prefabSha,profile->prefabSha))) return false;
    found.push_back({n,profile});if(found.size()>s_clothBoneSlots.size()) return false;
  }
  if(autoSelect) {
    std::set<std::string> priority;
    for(const auto &m:found)if(ClothBoneAcceptedConnections(*m.profile)||ClothBoneLocalRecipeFor(*m.profile))priority.insert(m.profile->component);
    for(size_t pass=0;pass<found.size();++pass)for(const auto &m:found)if(priority.count(m.profile->component))for(int k=0;k<m.profile->dependencyCount;++k)priority.insert(m.profile->dependencies[k]);
    std::vector<Match> fallback;s_clothAutoFallbacks.clear();
    for(auto it=found.begin();it!=found.end();)if(!priority.count(it->profile->component)){fallback.push_back(*it);s_clothAutoFallbacks.push_back(it->profile);it=found.erase(it);}else ++it;
    std::set<int> reserved;std::vector<const ClothBoneProfile*> authored;
    for(const auto &m:found){reserved.insert(m.index);authored.push_back(m.profile);}
    for(const auto &m:found)if(!strcmp(m.profile->signature,ClothContactPartnerConsumer)) {
      const auto partner=ClothContactPartnerProfiles[0];authored.push_back(partner);
      for(int n=0;n<s_cloth.count;++n)if(ClothBoneRootNames(s_cloth.instances[n],*partner))reserved.insert(n);
    }
    bool startupReady=true,captureReady=true;
    if(!s_clothAutoSkipped)for(int n=0;n<s_cloth.count;++n) {
      if(!found.empty() && !reserved.count(n))continue;
      const auto phase=s_cloth.instances[n].startup.phase;
      if(phase==eiem_cloth::Phase::Waiting || phase==eiem_cloth::Phase::Verifying) {
        startupReady=false;
        const auto &native=s_cloth.instances[n].last.state;
        captureReady&=native.readable&&native.process&&native.valid&&native.running&&native.processEnabled;
      }
    }
    try {
      if(!startupReady) {
        if(captureReady&&found.empty())ClothAutoPrepare(reserved,authored,false);
        s_clothAutoWaiting=true;ClothBoneNote("waiting-for-original-cloth-startup-source-preparation-overlapped");return false;
      }
      if(!ClothAutoPrepare(reserved,authored)) {
        if(found.empty())return false;
        s_clothAutoDeferred=true;
        Log("[CLOTH-AUTO] stage=authored-first matched=%zu genericWorkerDeferred=1 authoredWaitsForWorker=0",found.size());
      }
    }
    catch(const std::exception &e){ClothAutoCancel();s_clothAutoLease.reset();s_clothAutoSkipped=true;Log("[CLOTH-AUTO] stage=bridge-failed reason=%s authoredPreserved=1",e.what());}
    if(s_clothAutoLease)for(const auto &profile:s_clothAutoLease->profiles) {
      if(profile->view.runtimeUnowned){if(found.size()<s_clothBoneSlots.size())found.push_back({-1,&profile->view});continue;}
      int match=-1;for(int n=0;n<s_cloth.count;++n)if(!reserved.count(n)&&ClothBoneRootNames(s_cloth.instances[n],profile->view)){if(match>=0)return false;match=n;}
      if(match<0)continue;if(found.size()>=s_clothBoneSlots.size()){Log("[CLOTH-AUTO] component=%s decision=batch-capacity-original-retained",profile->view.component);continue;}
      reserved.insert(match);found.push_back({match,&profile->view});
    }
    if(!s_clothAutoDeferred)for(const auto &m:fallback)if(!reserved.count(m.index)){reserved.insert(m.index);found.push_back(m);}
    deadline=GetTickCount64()+30000;
  }
  if(found.empty()) {
    s_clothBoneNoMatch=s_clothAutoNoRenderableSources || (s_clothAutoJob &&
        s_clothAutoJob->done.load(std::memory_order_acquire) &&
        s_clothAutoJob->error.empty() && s_clothAutoLease && !s_clothAutoDeferred);
    Log("[CLOTH-CATALOG] action=no-match session=%llu generation=%llu prepared=%zu ownerBBCs=%d original-components-unchanged=1",
        (unsigned long long)owner.session,(unsigned long long)owner.generation,s_clothBoneCatalog.size(),s_cloth.count);
    for(int n=0;n<s_cloth.count;++n) Log("[CLOTH-CATALOG] unmatched-component=%s",s_cloth.instances[n].name);
  }
  if(found.empty() || !s_clothBone.pending || s_clothBone.stopRequested ||
      s_clothBone.command!=command || !(s_clothBone.owner==owner) || !ClothOwns(owner)) return false;
  std::set<const ClothBoneProfile*> orphanRibbons;
  for(const auto &m:found)if(m.profile->ribbonSource) {
    int peers=0;for(const auto &q:found)if(const auto r=ClothBoneLocalRecipeFor(*q.profile))
      peers+=r->resampledPanel&&r->responseConsumer&&!strcmp(r->responseConsumer,m.profile->component)&&!strcmp(q.profile->prefabSha,m.profile->prefabSha);
    if((autoSelect||localRequested)&&peers==1)continue;
    orphanRibbons.insert(m.profile);Log("[CLOTH-BONE-PAIR] component=%s decision=unpaired-ribbon-original-retained",m.profile->component);
  }
  found.erase(std::remove_if(found.begin(),found.end(),[&](const Match &m){return orphanRibbons.count(m.profile)!=0;}),found.end());
  std::set<const ClothBoneProfile*> orphanLayers;
  for(const auto &m:found)if(const auto r=ClothBoneLocalRecipeFor(*m.profile))if(r->nativeLayer==1) {
    int peers=0;for(const auto &q:found)peers+=ClothGeneratedLayerPair(*q.profile,*m.profile);
    if((autoSelect||localRequested)&&peers==1)continue;
    orphanLayers.insert(m.profile);Log("[CLOTH-BONE-PAIR] component=%s decision=unpaired-inner-layer-original-retained",m.profile->component);
  }
  found.erase(std::remove_if(found.begin(),found.end(),[&](const Match &m){return orphanLayers.count(m.profile)!=0;}),found.end());
  if(found.empty())return false;
  const bool fullRecipe=std::any_of(found.begin(),found.end(),[](const Match &m){return ClothBoneLocalRecipeFor(*m.profile)!=nullptr;});
  if(autoSelect) localRequested=fullRecipe;
  if(localRequested && !fullRecipe) return false;
  int pairedConsumer=-1,pairedPartner=-1;
  if(localRequested) for(int k=0;k<int(found.size());++k) {
    const auto *recipe=ClothBoneLocalRecipeFor(*found[k].profile);
    if(!recipe || strcmp(found[k].profile->signature,ClothContactPartnerConsumer) || strcmp(recipe->signature,ClothContactPartnerRecipe))continue;
    if(pairedConsumer>=0)return false;
    pairedConsumer=k;const auto *partner=ClothContactPartnerProfiles[0];
    if(strcmp(partner->prefabSha,found[k].profile->prefabSha))return false;
    for(const auto &m:found)if(!strcmp(m.profile->component,partner->component))return false;
    int match=-1;
    for(int n=0;n<s_cloth.count;++n)if(ClothBoneRootNames(s_cloth.instances[n],*partner)) {if(match>=0)return false;match=n;}
    if(match<0 || found.size()>=s_clothBoneSlots.size())return false;
    pairedPartner=int(found.size());found.push_back({match,partner});break;
  }
  if(localRequested&&pairedConsumer<0)for(int k=0;k<int(found.size());++k){const auto *r=ClothBoneLocalRecipeFor(*found[k].profile);
    if(!r||!r->resampledPanel||!r->responseConsumer)continue;
    for(int j=0;j<int(found.size());++j){const auto *p=ClothBoneLocalRecipeFor(*found[j].profile);
      if(p&&p->ribbonSurface&&found[j].profile->ribbonSource&&!strcmp(found[j].profile->component,r->responseConsumer)&&
          !strcmp(found[j].profile->prefabSha,found[k].profile->prefabSha)) {if(pairedConsumer>=0)return false;pairedConsumer=k;pairedPartner=j;}}
  }
  if(localRequested&&pairedConsumer<0)for(int k=0;k<int(found.size());++k)for(int j=0;j<int(found.size());++j)
    if(ClothGeneratedLayerPair(*found[j].profile,*found[k].profile)){if(pairedConsumer>=0)return false;pairedConsumer=k;pairedPartner=j;}
  Log("[CLOTH-CATALOG] action=select session=%llu generation=%llu matched=%zu automatic=%d fullRecipe=%d cache=%s livePreflight=pending",
      (unsigned long long)owner.session,(unsigned long long)owner.generation,found.size(),autoSelect,localRequested,
      s_clothBoneCache.key.empty()?"built-in":s_clothBoneCache.key.c_str());
  s_clothBoneCount=int(found.size());
  for(int n=0;n<s_clothBoneCount;++n) {
    auto &s=s_clothBoneSlots[n];s={};s.owner=owner;s.command=command;s.deadline=deadline;s.pending=true;
    s.index=found[n].index;s.profile=found[n].profile;
    s.autoSelect=autoSelect;s.requestedAt=requestedAt;
    Log("[CLOTH-AUTO] stage=selected component=%s source=%s livePreflight=pending",s.profile->component,ClothBoneSourceLabel(s.profile));
    const auto recipe=ClothBoneLocalRecipeFor(*s.profile);s.local.requested=localRequested && recipe;
    if(s.local.requested) {s.local.recipe=recipe;s.local.layers.resize(recipe->meshCount);s.local.crossRest.assign(recipe->crossCount,0);}
    if(!s.profile->runtimeUnowned){s.bbc=ClothProtect(ClothTarget(s_cloth.instances[s.index].ref));if(!s.bbc.handle)return false;}
    if(s_clothBone.stopRequested || !ClothOwns(owner)) return false;
    for(int d=0;d<s.profile->dependencyCount;++d) {
      int producer=-1;
      for(int k=0;k<int(found.size());++k) if(!strcmp(found[k].profile->component,s.profile->dependencies[d]) &&
          !strcmp(found[k].profile->prefabSha,s.profile->prefabSha)) producer=k;
      if(producer<0) return false;s.dependencies|=1u<<producer;
    }
  }
  if(pairedConsumer>=0) {
    auto &consumer=s_clothBoneSlots[pairedConsumer];auto &partner=s_clothBoneSlots[pairedPartner];
    consumer.contactPartner=pairedPartner;partner.contactConsumer=pairedConsumer;
    consumer.dependencies|=1u<<pairedPartner;consumer.deadline=deadline+8000;
    Log("[CLOTH-BONE-PAIR] consumer=%s partner=%s buildOrder=partner-then-consumer restoreOrder=consumer-then-partner originalFixedSkinRetained=1 partnerExpectedPoints=%d partnerExpectedFaces=%d support=%s",
        consumer.profile->component,partner.profile->component,partner.local.requested?partner.local.recipe->addedCount+partner.profile->EffectiveCount():int(std::size(ClothPartnerSurfaceBones)),partner.local.requested?partner.local.recipe->graphs[0].faceCount:ClothPartnerSurfaceGraphs[0].faceCount,ClothBoneGeneratedPair(partner,consumer)?"source-native-short-long-layers":ClothBoneRibbonPair(partner,consumer)?"source-fitted-ribbon-width":"authored-surface");
  }
  if(!eiem_cloth_rebuild::BatchGraphValid(ClothBoneBatchNodes(),s_clothBoneCount)) return false;
  s_clothBoneResolved=true;
  for(int n=0;n<s_clothBoneCount;++n) {
    const auto &s=s_clothBoneSlots[n];
    ClothBoneInSlot(n,[] { ClothBoneNote(ClothBoneState().dependencies?
        "waiting-for-confirmed-cloth-dependency":"waiting-for-serialized-build-turn"); });
    Log("[CLOTH-BONE-BATCH] slot=%d component=%s instance=%d dependencies=0x%x count=%d simultaneousLifecycle=0",
        n,s.profile->component,s.bbc.id.instance,s.dependencies,s_clothBoneCount);
  }
  return true;
}
static void ClothBoneAutoAppendBoundary() {
  if(!s_clothAutoDeferred)return;
  const auto owner=s_clothBone.owner;const auto command=s_clothBone.command;
  if(!ClothOwns(owner) || !ClothEnhancementSetting().load(std::memory_order_acquire) ||
      s_clothBoneRequest.load(std::memory_order_acquire)!=command) {ClothAutoCancel();return;}
  if(!s_clothAutoJob || !s_clothAutoJob->done.load(std::memory_order_acquire))return;
  try {
    std::set<int> reserved;std::vector<const ClothBoneProfile*> authored;
    for(int k=0;k<s_clothBoneCount;++k)if(s_clothBoneSlots[k].profile) {
      reserved.insert(s_clothBoneSlots[k].index);authored.push_back(s_clothBoneSlots[k].profile);
    }
    if(!ClothAutoPrepare(reserved,authored))return;
    s_clothAutoDeferred=false;
    std::vector<const ClothBoneProfile*> candidates;
    if(s_clothAutoLease)for(const auto &p:s_clothAutoLease->profiles)candidates.push_back(&p->view);
    candidates.insert(candidates.end(),s_clothAutoFallbacks.begin(),s_clothAutoFallbacks.end());
    for(const auto *candidate:candidates) {
      if(!ClothOwns(owner) || s_clothBoneRequest.load(std::memory_order_acquire)!=command ||
          !ClothEnhancementSetting().load(std::memory_order_acquire))return;
      const auto &profile=*candidate;
      if(profile.runtimeUnowned){
        bool duplicate=false;for(int k=0;k<s_clothBoneCount;++k)duplicate|=s_clothBoneSlots[k].profile==candidate;
        if(duplicate||s_clothBoneCount>=int(s_clothBoneSlots.size()))continue;
        auto &slot=s_clothBoneSlots[s_clothBoneCount++];slot={};slot.owner=owner;slot.command=command;
        slot.requestedAt=s_clothBone.requestedAt;slot.deadline=GetTickCount64()+8000;slot.pending=slot.autoSelect=true;slot.profile=candidate;
        Log("[CLOTH-AUTO] stage=append-unowned-sheet component=%s sourceBBCsChanged=0",profile.component);continue;
      }
      if(profile.generatedLocal&&profile.generatedLocal->nativeLayer) {
        Log("[CLOTH-AUTO] component=%s decision=layer-pair-requires-joint-batch original-retained=1",profile.component);continue;
      }
      if(profile.dependencyCount || profile.ribbonSource || (ClothBoneLocalRecipeFor(profile)&&!profile.generatedLocal))continue;
      int match=-1;bool duplicate=false;
      for(int k=0;k<s_cloth.count;++k)if(ClothBoneRootNames(s_cloth.instances[k],profile)) {
        if(match>=0){duplicate=true;break;}match=k;
      }
      for(int k=0;k<s_clothBoneCount;++k)duplicate|=s_clothBoneSlots[k].index==match;
      if(match<0 || duplicate || s_clothBoneCount>=int(s_clothBoneSlots.size()))continue;
      auto ref=ClothProtect(ClothTarget(s_cloth.instances[match].ref));
      if(!ref.handle || !ClothOwns(owner) || s_clothBoneRequest.load(std::memory_order_acquire)!=command) {ClothFree(ref);return;}
      auto &slot=s_clothBoneSlots[s_clothBoneCount++];slot={};slot.owner=owner;slot.command=command;
      slot.requestedAt=s_clothBone.requestedAt;slot.deadline=GetTickCount64()+8000;
      slot.pending=slot.autoSelect=true;slot.index=match;slot.profile=&profile;slot.bbc=ref;
      if(profile.generatedLocal){slot.local.requested=true;slot.local.recipe=profile.generatedLocal;slot.local.layers.resize(profile.generatedLocal->meshCount);slot.local.crossRest.assign(profile.generatedLocal->crossCount,0);}
      Log("[CLOTH-AUTO] stage=append-independent component=%s slot=%d authoredRestarted=0 livePreflight=pending",profile.component,s_clothBoneCount-1);
    }
  } catch(const std::exception &e) {
    ClothAutoCancel();Log("[CLOTH-AUTO] stage=deferred-failed reason=%s authoredPreserved=1",e.what());
  }
}
static void ClothBoneBatchBoundaryImpl() {
  using namespace eiem_cloth_rebuild;
  if(!s_clothBoneResolved) {
    if(!s_clothBone.stopRequested&&ClothOwns(s_clothBone.owner)&&!ClothPreparationTurn())return;
    if(!ClothBoneResolveBatch()) {
      if(s_clothAutoWaiting && s_clothBone.pending && !s_clothBone.stopRequested && ClothOwns(s_clothBone.owner))return;
      for(int n=0;n<s_clothBoneCount;++n) ClothBoneInSlot(n,[] {
        ClothBoneReject("batch-asset-missing-ambiguous-or-dependency-unconfirmed-original-unchanged");ClothBoneDropReferences();
      });return;
    }
  }
  ClothBoneAutoAppendBoundary();
  auto nodes=ClothBoneBatchNodes();
  for(int n=0;n<s_clothBoneCount;++n) {
    auto &s=s_clothBoneSlots[n];if(!s.pending) continue;
    if(!s.stopRequested && ClothBonePartnerNeedsRestore(s)) {
      s.stopRequested=true;
      Log("[CLOTH-BONE-PAIR] stage=partner-restore-queued slot=%d consumerSlot=%d consumerEnded=1 waitForConsumerDrain=1 originalVolumesWillReturn=1",n,s.contactConsumer);
    }
    if(!ClothOwns(s.owner) || BatchDependenciesLost(nodes,n,s_clothBoneCount)) s.stopRequested=true;
  }
  nodes=ClothBoneBatchNodes();const int selected=BatchSelectMutation(nodes,s_clothBoneCount,s_clothBoneMutating);
  if(selected>=0) {
    s_clothBoneMutating=selected;
    if(s_clothBoneSlots[selected].local.cleanup || s_clothBoneSlots[selected].supportCleanup)
      ClothBoneInSlot(selected,ClothBoneBoundaryOne);
    else if(!s_clothBoneSlots[selected].lease && nodes[selected].cancelling)
      ClothBoneInSlot(selected,[] { ClothBoneReleaseOne("dependency-ended-before-build-original-unchanged"); });
    else ClothBoneInSlot(selected,ClothBoneBoundaryOne);
    const auto &s=s_clothBoneSlots[selected];
    if(!s.pending || !s.lease || s.tx.phase==Phase::Active || ClothBoneBatchNodes()[selected].resourcesOnly) s_clothBoneMutating=-1;
  }
  ClothBonePollResourceCleanup(selected);
  nodes=ClothBoneBatchNodes();
  for(int n=0;n<s_clothBoneCount;++n) if(n!=selected && nodes[n].pending && nodes[n].active && !nodes[n].cancelling) {
    if(BatchDependenciesLost(nodes,n,s_clothBoneCount)) { s_clothBoneSlots[n].stopRequested=true;continue; }
    ClothBoneInSlot(n,ClothBoneBoundaryOne);
  }
}
static void ClothBoneBoundary() {
  if(!ClothOnMainThread() || !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 ||
      s_clothBoneDispatching) return;
  if(!ClothBonePending()){ClothPlaybackService(true);return;}
  s_clothBoneDispatching=true;
  s_clothLayerGate.Clear();
  s_clothDisplayView.CompletedBoundary();
  s_clothFinishView.CompletedBoundary();s_clothFinishChain.Clear();
  __try {
    __try { ClothBoneBatchBoundaryImpl(); ClothBoneLayerRefresh(); ClothBoneDisplayRefresh(); ClothBoneFinishRefresh(); ClothPlaybackService(true); }
    __except(EXCEPTION_EXECUTE_HANDLER) { ClothBoneRelease("batch-boundary-exception-restoring"); }
  } __finally { s_clothBoneDispatching=false; }
}
static std::string ClothBoneJson() {
  std::string out;
  for(int n=0;n<s_clothBoneCount;++n) {
    const auto item=ClothBoneJsonOne(s_clothBoneSlots[n]);
    if(n==0) { out=item;out.pop_back();out+=",\"components\":["; }
    if(n) out+=",";out+=item;
  }
  out+="]}";return out;
}
