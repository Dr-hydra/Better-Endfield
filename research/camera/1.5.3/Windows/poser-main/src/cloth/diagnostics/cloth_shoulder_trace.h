#pragma once
static void ClothShoulderDriverCancel();
struct ClothShoulderEvidence {
  eiem_cloth::Owner owner{};
  ClothRef renderer{},mesh{},root{};
  std::vector<ClothRef> bones,parents;
  eiem_cloth_asset::LiveRenderer source;
  unsigned command=0,record=0,pauseSeen=0,pauseRemaining=0,periodic=0;
  uint64_t nextMs=0,expiresMs=0;
  uint64_t nextPerfMs=0;
  int perfFrame=-1;
  int64_t lastBoundary=0;
  unsigned perfFrames=0;
  double totalServiceMs=0,maxServiceMs=0,maxCaptureMs=0,maxBoundaryGapMs=0;
  int visibleMesh=0;
  bool widthMesh=false;
  bool prepared=false;
};
static ClothActorBank<ClothShoulderEvidence> s_clothShoulderEvidenceActors;
#define s_clothShoulderEvidence (s_clothShoulderEvidenceActors.Get())
static void ClothShoulderEvidenceClear() {
  ClothShoulderDriverCancel();
  auto &s=s_clothShoulderEvidence;
  ClothFree(s.renderer);ClothFree(s.mesh);ClothFree(s.root);
  for(auto &r:s.bones)ClothFree(r);for(auto &r:s.parents)ClothFree(r);s={};
}
static bool ClothShoulderEvidenceWatching() {
  const auto &s=s_clothShoulderEvidence;
  return s.prepared&&ClothEnhancementSetting().load(std::memory_order_acquire)&&ClothOwns(s.owner);
}
static void ClothShoulderEvidencePrepare(const eiem_cloth_asset::Generated &generated,
    const eiem_cloth_asset::Query &query,const std::vector<ClothAutoMeshRef> &meshes) {
  if(!ClothOnMainThread()||generated.sourceHash!="95462411bcce0cc31914ad7f6cd0c37909eff18577f51cff319befbe4c349ab6"||
      query.renderers.size()!=meshes.size()||!ClothOwns(s_clothBone.owner))return;
  for(size_t n=0;n<meshes.size();++n) {
    const auto &meta=query.renderers[n];if(meta.name!="S_actor_aglina_cloth_01_lod0"||meta.vertices!=43316)continue;
    ClothShoulderEvidenceClear();auto &s=s_clothShoulderEvidence;const auto &r=meshes[n];
    if(meta.bones.empty()||meta.bones.size()>256||meta.bones.size()!=r.bones.size()||r.parents.size()!=r.bones.size())return;
    s.owner=s_clothBone.owner;s.command=s_clothBone.command;s.source=meta;
    s.renderer=ClothProtect(ClothTarget(r.renderer));s.mesh=ClothProtect(ClothTarget(r.mesh));s.root=ClothProtect(ClothTarget(r.root));
    bool valid=s.renderer.handle&&s.mesh.handle&&s.root.handle;
    for(size_t k=0;k<r.bones.size();++k){s.bones.push_back(ClothProtect(ClothTarget(r.bones[k])));s.parents.push_back(ClothProtect(ClothTarget(r.parents[k])));valid&=s.bones.back().handle&&s.parents.back().handle;}
    if(!valid){ClothShoulderEvidenceClear();return;}s.prepared=true;
    Log("[CLOTH-SHOULDER] stage=prepared session=%llu generation=%llu command=%u renderer=%s bones=%zu installedSourceMatched=1 GPUContentHashVerified=0 writes=0 capture=first-submitted-pose-and-explicit-pause periodicFullSnapshots=0",
        s.owner.session,s.owner.generation,s.command,meta.name.c_str(),s.bones.size());return;
  }
}
static void ClothShoulderEvidenceTryPrepare(const eiem_cloth_asset::Generated &generated,
    const eiem_cloth_asset::Query &query,const std::vector<ClothAutoMeshRef> &meshes) {
  __try {ClothShoulderEvidencePrepare(generated,query,meshes);}
  __except(EXCEPTION_EXECUTE_HANDLER){ClothShoulderEvidenceClear();Log("[CLOTH-SHOULDER] stage=unavailable reason=capture-fault simulationUnchanged=1");}
}
static bool ClothShoulderEvidenceMeshIdentity(void *renderer,void *mesh) {
  auto &s=s_clothShoulderEvidence;s.widthMesh=false;s.visibleMesh=s.mesh.id.instance;
  if(!renderer||!mesh)return false;
  if(mesh==ClothTarget(s.mesh))return SurfaceRenderSameReferences(renderer,"get_bones","UnityEngine.Transform[]",s.bones);
  for(int slot=0;slot<s_clothBoneCount;++slot){const auto &b=s_clothBoneSlots[slot];const auto &l=b.local;
    if(!(b.owner==s.owner)||b.command!=s.command||b.stopRequested||!l.recipe||!l.recipe->NativeRibbonWidth()||
        !b.profile||!eiem_cloth_asset::SourceLegRibbonWidth(*b.profile))continue;
    for(size_t k=0;k<l.layers.size();++k){const auto &layer=l.layers[k];
      if(!layer.published||k>=size_t(l.recipe->meshCount)||layer.renderer<0||size_t(layer.renderer)>=b.renderers.size()||
          ClothTarget(b.renderers[layer.renderer].renderer)!=renderer||ClothTarget(layer.mesh)!=mesh||
          l.recipe->meshes[k].sourceBones!=int(s.bones.size())||layer.rendererBones.size()<s.bones.size())continue;
      bool prefix=true;for(size_t n=0;n<s.bones.size();++n)prefix&=ClothTarget(layer.rendererBones[n])==ClothTarget(s.bones[n]);
      if(prefix&&SurfaceRenderSameReferences(renderer,"get_bones","UnityEngine.Transform[]",layer.rendererBones)){
        s.widthMesh=true;s.visibleMesh=layer.mesh.id.instance;return true;}
    }
  }return false;
}
static bool ClothShoulderEvidenceIdentity() {
  auto &s=s_clothShoulderEvidence;auto renderer=ClothTarget(s.renderer);void *mesh=nullptr,*root=nullptr;
  if(!ClothShoulderEvidenceWatching()||!renderer||!ClothAnchorUnderOwner(CollisionTransform(renderer))||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(renderer),"get_sharedMesh","UnityEngine.Mesh"),renderer,nullptr,mesh)||!ClothShoulderEvidenceMeshIdentity(renderer,mesh)||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(renderer),"get_rootBone","UnityEngine.Transform"),renderer,nullptr,root)||root!=ClothTarget(s.root)||
      s.bones.size()!=s.parents.size()||s.source.bones.size()!=s.bones.size())return false;
  for(size_t n=0;n<s.bones.size();++n)if(!ClothTarget(s.bones[n])||!ClothAnchorUnderOwner(ClothTarget(s.bones[n]))||CollisionParent(ClothTarget(s.bones[n]))!=ClothTarget(s.parents[n]))return false;
  return true;
}
#include "../shoulder/cloth_shoulder_drive.h"
static bool ClothShoulderEvidenceRead(unsigned pause,int poseFrame,double playhead,const char *stage) {
  auto &s=s_clothShoulderEvidence;
  if(!ClothOnMainThread()||s_clothInputUpdateDepth!=1||!s_clothInputHooks||!s.prepared)return true;
  if(!ClothShoulderEvidenceWatching()){ClothShoulderEvidenceClear();return true;}
  const auto now=GetTickCount64();
  if(!s.record)s.pauseSeen=pause;
  if(pause!=s.pauseSeen){s.pauseSeen=pause;s.pauseRemaining=2;s.expiresMs=now+12000;s.nextMs=now;}
  const bool paused=s.pauseRemaining&&now>=s.nextMs&&now<=s.expiresMs;
  const bool periodic=s.periodic==0;
  if(s.record>=33||(!paused&&!periodic)||poseFrame<0||!std::isfinite(playhead))return true;
  const int frame=ClothFrame();if(frame<0)return false;
  if(!ClothShoulderEvidenceIdentity()){ClothShoulderEvidenceClear();return false;}
  std::vector<ClothInputPose> poses(s.bones.size());std::vector<Vector3> local(s.bones.size());std::vector<Quaternion> rotation(s.bones.size());
  ClothInputPose rendererPose{};if(!ClothInputPoseRead(CollisionTransform(ClothTarget(s.renderer)),rendererPose))return false;
  for(size_t n=0;n<s.bones.size();++n)if(!ClothInputPoseRead(ClothTarget(s.bones[n]),poses[n])||!ClothReadLocal(ClothTarget(s.bones[n]),local[n],rotation[n]))return false;
  if(frame!=ClothFrame()||!ClothShoulderEvidenceIdentity())return false;
  ++s.record;if(paused){--s.pauseRemaining;s.nextMs=now+1000;}
  if(periodic)++s.periodic;
  Log("[CLOTH-SHOULDER-BEGIN] session=%llu generation=%llu command=%u record=%u frame=%d poseFrame=%d vmdFrame=%.9g poseStage=%s renderer=%s instance=%d meshInstance=%d bones=%zu completedBoundary=1 simulationTicketKnown=0 contactDepthMeasured=0 writes=0 capture=%s privateLowerSkin=%d shoulderSourceSkinRetained=1",
      s.owner.session,s.owner.generation,s.command,s.record,frame,poseFrame,playhead,stage,s.source.name.c_str(),s.renderer.id.instance,s.visibleMesh,s.bones.size(),paused?"pause":"first-submitted",s.widthMesh);
  for(size_t n=0;n<s.bones.size();++n) {
    const auto &b=s.source.bones[n];std::ostringstream out;out<<std::setprecision(12)<<"{\"record\":"<<s.record<<",\"frame\":"<<frame<<",\"index\":"<<n
        <<",\"name\":"<<CollisionJsonString(b.bone.name.c_str())<<",\"parent\":"<<CollisionJsonString(b.bone.parent.c_str())
        <<",\"instance\":"<<s.bones[n].id.instance<<",\"matrix\":";ClothInputJsonArray(out,poses[n].matrix,16);
    const float lp[]{local[n].x,local[n].y,local[n].z},lr[]{rotation[n].x,rotation[n].y,rotation[n].z,rotation[n].w};
    out<<",\"localPosition\":";ClothInputJsonArray(out,lp,3);out<<",\"localRotation\":";ClothInputJsonArray(out,lr,4);
    out<<",\"bind\":";ClothInputJsonArray(out,b.bind.data(),16);out<<'}';Log("[CLOTH-SHOULDER-BONE] %s",out.str().c_str());
  }
  std::ostringstream out;out<<std::setprecision(12);ClothInputJsonArray(out,rendererPose.matrix,16);
  Log("[CLOTH-SHOULDER-END] record=%u frame=%d count=%zu rendererMatrix=%s",s.record,frame,s.bones.size(),out.str().c_str());return true;
}
static void ClothShoulderEvidenceCompleted(unsigned pause,int poseFrame,double playhead,const char *stage) {
  if(!ClothOnMainThread())return;
  LARGE_INTEGER start{},driven{},end{},frequency{};
  QueryPerformanceFrequency(&frequency);QueryPerformanceCounter(&start);
  ClothShoulderDriverCompleted(poseFrame);
  QueryPerformanceCounter(&driven);
  bool ok=false;
  __try {ok=ClothShoulderEvidenceRead(pause,poseFrame,playhead,stage);}
  __except(EXCEPTION_EXECUTE_HANDLER){ok=false;}
  if(!ok){ClothShoulderEvidenceClear();Log("[CLOTH-SHOULDER] stage=unavailable reason=completed-pose-read-failed simulationUnchanged=1");}
  QueryPerformanceCounter(&end);
  auto &s=s_clothShoulderEvidence;const auto &driver=s_clothShoulderDriver;
  if(!s.prepared||!driver.active||frequency.QuadPart<=0||driver.frame==s.perfFrame)return;
  const double unit=1000./frequency.QuadPart;
  const double service=(end.QuadPart-start.QuadPart)*unit,capture=(end.QuadPart-driven.QuadPart)*unit;
  if(s.lastBoundary)s.maxBoundaryGapMs=(std::max)(s.maxBoundaryGapMs,(start.QuadPart-s.lastBoundary)*unit);
  s.lastBoundary=start.QuadPart;s.perfFrame=driver.frame;++s.perfFrames;s.totalServiceMs+=service;
  s.maxServiceMs=(std::max)(s.maxServiceMs,service);s.maxCaptureMs=(std::max)(s.maxCaptureMs,capture);
  const auto now=GetTickCount64();if(now<s.nextPerfMs)return;s.nextPerfMs=now+5000;
  Log("[CLOTH-SHOULDER-PERF] frame=%d updates=%u frames=%u meanServiceMs=%.3f maxServiceMs=%.3f maxCaptureAndLogMs=%.3f maxBoundaryGapMs=%.3f periodicFullSnapshots=0 combinedLocalGetter=1 serviceIncludesCaptureAndLogs=1 perfLineExcluded=1 boundaryGapIsNotAttribution=1",
      driver.frame,driver.updates,s.perfFrames,s.totalServiceMs/s.perfFrames,s.maxServiceMs,s.maxCaptureMs,s.maxBoundaryGapMs);
  s.perfFrames=0;s.totalServiceMs=s.maxServiceMs=s.maxCaptureMs=s.maxBoundaryGapMs=0;
}
