#pragma once
#include "../resources/cloth_resource_limits.h"
#include "../core/cloth_skin_guard.h"
#include "../generated/cloth_bonecloth_local_generated.h"
#include "../generated/cloth_bonecloth_layer_generated.h"
#include "../generated/cloth_bonecloth_body_generated.h"
struct ClothSideShape {int bone;Vector3 center;float radius;};
#include "../generated/cloth_bonecloth_side_generated.h"
static bool ClothBoneSideRecipeFor(const ClothBoneProfile &p) {
  return p.signature && p.prefabSha && !strcmp(p.signature,ClothSideBaseSignature) && !strcmp(p.prefabSha,ClothSidePrefab);
}
struct ClothSideCollider {ClothRef collider{},transform{},parent{};};
struct ClothSideState {
  ClothBoneRendererRef renderer{};
  std::vector<ClothSideCollider> shapes;
  int producer=-1;
  uint32_t list=0,constraint=0;
  bool created=false,linked=false,notified=false,removeNotified=false,registrationLogged=false;
};
struct ClothApronLayerState {
  int producer=-1;
  uint32_t constraint=0,list=0;
  std::vector<ClothRef> outerColliders;
  std::array<int,2> response{{-1,-1}};
  std::vector<int> omitted;
  bool configured=false,confirmed=false;
};
#include "cloth_bonecloth_recipe.h"
struct ClothResponsePose {
  Vector3 originalP{},lastP{},previousP{},scale{};Quaternion originalQ{},lastQ{},previousQ{};
  bool position=false,rotation=false;
  bool previousPosition=false,previousRotation=false;
  unsigned restorePositionAttempts=0,restoreRotationAttempts=0;
};
struct ClothResponsePeer {ClothRef bbc{};uint32_t data=0,roots=0;std::vector<ClothRef> rootRefs;};
struct ClothResponseState {
  ClothRef consumer{};uint32_t process=0,data=0,data2=0,constraint=0,list=0;
  int team=0,frame=-1;unsigned updates=0;
  std::vector<ClothRef> colliders;
  std::vector<ClothResponsePeer> peers;
  std::vector<ClothRef> outputBones;
  std::array<int,5> outputVertices{},outputTransforms{};
  std::vector<unsigned char> outputAttributes;
  uint32_t outputContainer=0,outputMesh=0;
  bool outputConfirmed=false,layerArmed=false;
  std::array<ClothResponsePose,3> poses{};
  std::array<eiem_cloth_surface::OutputMatrix,3> appliedWorld{};
  int inputFrame=-1;
  bool attempted=false,prepared=false,captured=false,disabled=false;
  bool Dirty() const {for(const auto &p:poses)if(p.position||p.rotation)return true;return false;}
};
static const char *ClothBoneLocalResourceIssue(const ClothBoneLocalMeshConfig &config,const void *bytes,size_t size,size_t limit=eiem_cloth_resource::AuthoredBytes) {
  if(!bytes) return "local-resource-missing";
  if(!eiem_cloth_resource::Fits(config.bytes,limit)||!eiem_cloth_resource::Fits(size,limit)) return "local-resource-byte-budget-invalid";
  if(size!=config.bytes) return "local-resource-size-mismatch";
  if(eiem_cloth_surface::RenderHash(bytes,size)!=config.hash) return "local-resource-hash-mismatch";
  return nullptr;
}
static const ClothBoneLocalMeshConfig ClothLocalMeshes[]{
  {ClothLocalRenderer,ClothLocalSignature,ClothLocalBundleName,ClothLocalBundleAsset,4303,ClothLocalBundleBytes,ClothLocalBundleHash,ClothLocalSourceBoneCount,ClothLocalSkinVertexCount,ClothLocalAddedBindings,int(std::size(ClothLocalAddedBindings)),ClothLocalBindingNativeIndices},
  {ClothInnerRenderer,ClothInnerSignature,ClothInnerBundleName,ClothInnerBundleAsset,4304,ClothInnerBundleBytes,ClothInnerBundleHash,ClothInnerSourceBoneCount,ClothInnerSkinVertexCount,ClothInnerAddedBindings,int(std::size(ClothInnerAddedBindings)),ClothInnerBindingNativeIndices,ClothInnerSourceVertexCount,ClothInnerCandidateVertexCount,ClothInnerCandidateIndexCount,ClothInnerCandidateIndexHash}
};
static const auto ClothLocalCross=[]{
  std::array<std::array<int,2>,std::size(ClothLocalCrossPairs)> a{};
  for(size_t n=0;n<a.size();++n)a[n]={ClothLocalCrossPairs[n][0],ClothLocalCrossPairs[n][1]};return a;
}();
static const ClothBoneLocalRecipe ClothRingRecipe=[]{
  ClothBoneLocalRecipe r{};r.signature=ClothLocalSignature;r.baseSignature=ClothLocalBaseSignature;r.prefabSha=ClothBodyContactPrefab;
  r.bodyCoverage=true;r.originalCount=ClothLocalOriginalCount;r.originalRoots=ClothLocalOriginalRoots;r.depth=ClothLocalDepth;
  r.addedCount=ClothLocalAddedCount;r.rootCount=int(std::size(ClothLocalRoots));r.added=ClothLocalAddedBones;
  r.columns=ClothLocalOriginalColumns;r.roots=ClothLocalRoots;r.parents=ClothLocalParentSources;r.radii=ClothLocalExpectedRadius;
  r.cross=ClothLocalCross.data();r.crossCount=int(ClothLocalCross.size());r.meshes=ClothLocalMeshes;r.meshCount=int(std::size(ClothLocalMeshes));return r;
}();
#include "../generated/cloth_bonecloth_strip_generated.h"
static const ClothBoneLocalRecipe *ClothBoneLocalRecipeFor(const ClothBoneProfile &p) {
  if(p.runtimeGenerated && p.generatedLocal && p.generatedLocal->runtimeGenerated &&
      p.generatedLocal->baseSignature && p.generatedLocal->prefabSha && p.signature && p.prefabSha &&
      !strcmp(p.signature,p.generatedLocal->baseSignature) && !strcmp(p.prefabSha,p.generatedLocal->prefabSha) &&
      (!p.generatedLocal->sourceCoatWaist||(p.generatedLocal->CoatWaistSkinOnly()&&eiem_cloth_asset::SourceInactiveCoat(p)))&&
      (p.runtimeBodyOnly==p.generatedLocal->sourceBodyOnly)&&(!p.runtimeBodyOnly||
      (p.generatedLocal->NativeBodyOnly()&&eiem_cloth_asset::SourceBodyContact(p.prefabSha,p.component))))return p.generatedLocal;
  for(auto r:{&ClothRingRecipe,&ClothStripRecipe})
    if(p.signature && p.prefabSha && !strcmp(p.signature,r->baseSignature) && !strcmp(p.prefabSha,r->prefabSha))return r;
  return nullptr;
}
static bool ClothBoneLocalMeshShape(const ClothBoneLocalMeshConfig &config,const ClothBoneRendererAsset &source,int vertices,int submeshes) {
  if(submeshes!=source.submeshes || source.vertices<1) return false;
  if(!config.candidateVertices) return !config.sourceVertices && !config.candidateIndices && !config.candidateIndexHash && vertices==source.vertices;
  return config.sourceVertices==source.vertices && config.candidateVertices>config.sourceVertices &&
      config.candidateVertices<=65535 && config.candidateIndices>0 && config.candidateIndices<=65536 &&
      config.candidateIndices%3==0 && config.candidateIndexHash && submeshes==1 && vertices==config.candidateVertices;
}
static bool ClothBoneLocalIndexData(const ClothBoneLocalMeshConfig &config,const int *indices,size_t count) {
  if(!indices || !config.candidateVertices || count!=size_t(config.candidateIndices) || count>65536 || count%3) return false;
  for(size_t n=0;n<count;++n) if(indices[n]<0 || indices[n]>=config.candidateVertices) return false;
  return eiem_cloth_surface::RenderHash(indices,count*sizeof(int))==config.candidateIndexHash;
}
static bool ClothBoneLocalBindingMapValid(const ClothBoneLocalMeshConfig &config,const ClothBoneRendererAsset &source,int nativeCount,int partnerFirst=0,int partnerCount=0,bool retainedBindings=false) {
  if(config.bindingCount<0 || (config.bindingCount==0?!retainedBindings:(!config.bindingNativeIndices || !config.bindings)) || nativeCount<1 || nativeCount>ClothBoneMaxIdentities ||
      source.boneCount!=config.sourceBones || source.boneCount<1 || source.boneCount+config.bindingCount>256 || !source.bones) return false;
  if(partnerFirst<0 || partnerCount<0 || partnerFirst+partnerCount>128) return false;
  std::array<bool,ClothBoneMaxIdentities> seen{};std::array<bool,128> paired{};
  for(int n=0;n<source.boneCount;++n) if(source.bones[n].cloth>=0) {
    const int id=source.bones[n].cloth;if(id>=nativeCount || seen[id]) return false;seen[id]=true;
  }
  for(int n=0;n<config.bindingCount;++n) {
    const int id=config.bindingNativeIndices[n];
    if(id<0) {
      if(id < -128) return false;const int p=-id-1;
      if(!partnerCount || p<partnerFirst || p>=partnerFirst+partnerCount || paired[p])return false;paired[p]=true;
    } else {if(id>=nativeCount || seen[id])return false;seen[id]=true;}
    for(float v:config.bindings[n]) if(!std::isfinite(v)) return false;
  }
  return true;
}
struct ClothBoneLocalMeshState {
  bool ready=false,issued=false,completed=false,taken=false,unloadIssued=false;
  bool publishAttempted=false,published=false;
  int renderer=-1;
  size_t buildBinding=0;
  uint32_t scan=0,registry=0,request=0,pathString=0,oldArray=0,newArray=0;
  size_t scanCount=0,cursor=0,registryCount=0;
  std::vector<int> previousIds;
  bool namePresent=false;
  ClothRef mesh{},bundle{};
  SurfaceInputFile file{};
  std::vector<ClothRef> rendererBones;
};
struct ClothBoneContactParameters {int selfMode;float thickness[16];int syncMode;float mass;};
struct ClothBoneContactStart {
  enum class Phase { Waiting, Reserved, Requested, Ready, Failed } phase=Phase::Waiting;
  eiem_cloth_rebuild::Identity identity[2]{};
  int team[2]{},frame=-1;
  unsigned command=0;
  uint64_t deadline=0;
  bool Reserve(const eiem_cloth_rebuild::Identity &a,const eiem_cloth_rebuild::Identity &b,int ta,int tb,int f,uint64_t now,unsigned cmd) {
    if(phase!=Phase::Waiting || !a.Valid() || !b.Valid() || !(a.owner==b.owner) ||
        a.bbc==b.bbc || a.process==b.process || a.data==b.data || ta<=0 || tb<=0 || ta==tb || f<0 || !cmd)return false;
    identity[0]=a;identity[1]=b;team[0]=ta;team[1]=tb;frame=f;command=cmd;deadline=now+8000;phase=Phase::Reserved;return true;
  }
  bool Matches(const eiem_cloth_rebuild::Identity &a,const eiem_cloth_rebuild::Identity &b,int ta,int tb,unsigned cmd) const {
    return a==identity[0] && b==identity[1] && ta==team[0] && tb==team[1] && cmd==command;
  }
  bool Acknowledge(bool first,bool second) {
    if(phase!=Phase::Reserved)return false;
    phase=first&&second?Phase::Requested:Phase::Failed;return phase==Phase::Requested;
  }
  bool Observe(bool firstReset,bool secondReset,int completedFrame,uint64_t now) {
    if(phase==Phase::Ready)return true;
    if(phase!=Phase::Requested)return false;
    if(completedFrame>frame && !firstReset && !secondReset){phase=Phase::Ready;return true;}
    if(now>=deadline)phase=Phase::Failed;return false;
  }
};
struct ClothBoneLocalState {
  char cleanupIssue[128]{};
  uint64_t cleanupNextNote=0;
  int cleanupFrame=-1;
  const ClothBoneLocalRecipe *recipe=&ClothRingRecipe;
  bool requested=false,created=false,ready=false,cleanup=false;
  bool publishAttempted=false,published=false,solverConfirmed=false;
  unsigned solverFrames=0;
  bool colliderInputReady=false,colliderInputPending=false;
  unsigned colliderInputWaitFrames=0;
  int colliderInputWaitFrame=-1;
  uint64_t colliderInputWaitStart=0;
  std::vector<std::array<int,2>> colliderInputPopulated;
  bool panelReturnCaptured=false;
  int panelReturnCount=0;
  std::array<int,16> panelReturnIndices{};
  eiem_cloth::Owner panelReturnOwner{};unsigned panelReturnCommand=0;
  std::array<std::array<int,2>,16> panelReturnIds{};
  Vector3 panelReturnPosition[16]{},panelReturnScale[16]{};Quaternion panelReturnRotation[16]{};
  unsigned createStage=0;
  size_t createBone=0,createLayer=0,meshCursor=0;
  uint32_t registryScan=0;
  size_t registryCount=0,registryCursor=0;
  bool registryReady=false;
  std::vector<int> registryIds;
  uint32_t elasticTether=0;
  unsigned elasticConversions=0;
  float elasticCompression=0;
  bool attachmentConfigured=false,attachmentReadback[3]{};
  float sourceRootRotation=0,sourceRotationalInterpolation=0;
  bool distanceReadback=false;
  bool distanceParametersReadback=false;
  int contactProducer=-1;
  bool contactConfigured=false,contactConfirmed=false,contactReleased=false;
  bool contactRetireNotified=false;
  int contactRetireFrame=-1;
  unsigned contactRetireNotes=0,contactRetireAttempts=0;
  bool contactLayered=false;
  bool contactResponse=false;
  bool contactEnvelope=false;
  bool contactEnvelopeReadback[3]{};
  ClothBoneContactStart contactStart{};
  std::vector<int> contactColliderOmissions;
  ClothApronLayerState apronLayer;
  ClothResponseState response;
  bool contactPendingLogged=false;
  uintptr_t contactListHeaders[2]{};
  unsigned contactJobDeferredRepairs[2]{};
  uint32_t contactManager=0;
  unsigned contactJobRepairs[2]{};
  unsigned contactCompletedNotes=0;
  uint64_t contactNextNote=0;
  ClothBoneContactParameters contactOriginal{};
  const char *readbackIssue=nullptr;
  uint64_t surfaceLogMs=0;
  unsigned surfaceLogs=0;
  bool surfaceStretchLogged=false;
  std::vector<float> crossRest=std::vector<float>(ClothLocalCross.size());
  int solverFrame=-1;
  uint64_t deadline=0;
  std::vector<ClothBoneLocalMeshState> layers=std::vector<ClothBoneLocalMeshState>(std::size(ClothLocalMeshes));
  std::vector<ClothRef> objects;
  std::vector<bool> destroyIssued;
  ClothBoneRendererRef bodyRenderer{};
  ClothSideState side{};
  ClothRef bodyCollider{},bodyTransform{},bodyParent{};
  std::vector<ClothSideCollider> fittedBody;
  bool fittedBodyCreated=false,fittedBodyRegistered=false;
  bool bodyCreated=false,bodyRegisteredLogged=false;
  ClothBoneProfile profile{};
  std::vector<ClothBoneAsset> assets;
  std::vector<int> roots,ignored;
  bool HasResources() const {
    if(contactConfigured&&!contactReleased)return true;
    if(registryScan || created || !objects.empty() || bodyRenderer.renderer.handle || bodyCollider.handle || !fittedBody.empty() || side.renderer.renderer.handle || !side.shapes.empty()) return true;
    for(auto &l:layers) if(l.scan || l.issued || l.bundle.handle || l.mesh.handle || l.file.path[0]) return true;
    return false;
  }
  bool OwnsRenderer(size_t slot) const {
    for(auto &l:layers) if(l.publishAttempted && l.renderer==int(slot)) return true;
    return false;
  }
};
static bool ClothBoneLocalCreate();
static bool ClothBoneLocalCleanupWait(const char *reason,int item=-1);
static bool ClothBoneLocalCleanup();
static bool ClothBoneLocalReady();
static bool ClothBoneLocalRestore();
static bool ClothBoneResponseRestore();
static void ClothBoneResponseFree();
static bool ClothBoneResponseCaptureOutputs(const ClothBoneNativeProducer &);
struct ClothBoneRuntime;
static bool ClothBoneResponseLayerPrepare(ClothBoneRuntime &);
static bool ClothBoneSolverAccess(void *,uintptr_t &,int &,void *&);
static bool ClothBoneLocalPublish();
static bool ClothBoneLocalRendererIdentity(size_t slot,void *renderer,void *mesh);
static bool ClothBoneBodyCreate();
static bool ClothBoneBodyBindingIdentity();
static bool ClothBoneBodyRegistration(void *process,int team,bool restoring);
static bool ClothBoneBodyReleaseReady();
static void ClothBoneBodyFree();
static bool ClothBoneSideCreate();
static bool ClothBoneSideConfigure(std::vector<void*> &colliders);
static bool ClothBoneSideIdentity();
static bool ClothBoneSideRegistration(void *process,int team,bool restoring);
static bool ClothBoneSideReleaseReady();
static void ClothBoneSideFree();
static bool ClothBoneNativeContactConfigure(void *data,void *original);
static bool ClothBoneNativeContactReadback(int slot,void *team);
static bool ClothBoneNativeContactReleaseReady();
