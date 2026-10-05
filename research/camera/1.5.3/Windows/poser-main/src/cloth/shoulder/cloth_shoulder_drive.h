#pragma once
#include "../generated/cloth_shoulder_aglina_data.h"
#include "../generated/cloth_shoulder_contact_aglina.h"
#include <memory>
struct ClothShoulderContactRuntime;
static void ClothShoulderContactRelease();
static thread_local unsigned s_clothShoulderPoseReadDepth=0;
static bool ClothShoulderReadOnlyPoseActive(){return s_clothShoulderPoseReadDepth!=0;}
static bool ClothShoulderInvokePoseReader(void *method,void *self,void **args,void *&result) {
  if(!ClothOnMainThread())return false;
  ++s_clothShoulderPoseReadDepth;bool ok=false;
  __try {ok=ClothInvoke(method,self,args,result);}
  __finally {--s_clothShoulderPoseReadDepth;}
  return ok;
}
struct ClothShoulderControl {
  ClothRef bone{},parent{};
  eiem_cloth_shoulder::Lease lease{};
};
struct ClothShoulderDriver {
  eiem_cloth::Owner owner{};
  std::array<ClothShoulderControl,10> controls{};
  ClothRef animator{},avatar{},root{},renderer{},visibleMesh{};
  uint32_t handler=0,pose=0,referenceDescription=0;
  void *read=nullptr,*dispose=nullptr,*getMesh=nullptr,*getAvatar=nullptr,*getLocal=nullptr,*setPosition=nullptr,*setRotation=nullptr;
  int muscleOffset=-1,muscles=0,feature[2][7]{},frame=-1,scene=0;
  unsigned command=0,updates=0;
  uint64_t lastMs=0,nextAudit=0,nextLog=0,nextRestore=0;
  double lastCostMs=0,maxCostMs=0,totalCostMs=0,prepareCostMs=0;
  const char *prepareStep="not-started";
  int prepareControl=-1;
  bool active=false,stopping=false,attempted=false;
};
static ClothActorBank<ClothShoulderDriver> s_clothShoulderDriverActors;
#define s_clothShoulderDriver (s_clothShoulderDriverActors.Get())
static eiem_cloth_shoulder::Pose ClothShoulderPose(Vector3 p,Quaternion q){return {{p.x,p.y,p.z},{q.x,q.y,q.z,q.w}};}
static bool ClothShoulderRead(void *t,eiem_cloth_shoulder::Pose &p){
  Vector3 v{};Quaternion q{};auto method=s_clothShoulderDriver.getLocal;
  if(method){void *unused=nullptr,*args[]{&v,&q};if(!t||!ClothInvoke(method,t,args,unused))return false;}
  else if(!ClothReadLocal(t,v,q))return false;
  p=ClothShoulderPose(v,q);return eiem_cloth_shoulder::Valid(p);
}
static bool ClothShoulderSetPosition(void *t,const eiem_cloth_shoulder::Point &p){Vector3 v{p[0],p[1],p[2]};void *unused=nullptr,*args[]{&v};return ClothInvoke(s_clothShoulderDriver.setPosition,t,args,unused);}
static bool ClothShoulderSetRotation(void *t,const eiem_cloth_shoulder::Rotation &q){Quaternion v{q[0],q[1],q[2],q[3]};void *unused=nullptr,*args[]{&v};return ClothInvoke(s_clothShoulderDriver.setRotation,t,args,unused);}
static bool ClothShoulderRestore() {
  if(!ClothOnMainThread())return false;auto &s=s_clothShoulderDriver;bool ok=true;unsigned restored=0,preserved=0;
  for(auto &c:s.controls){if(!c.lease.position&&!c.lease.rotation&&!c.lease.pendingPosition&&!c.lease.pendingRotation)continue;void *t=nullptr,*parent=nullptr;
    const auto life=ClothInspect(c.bone,t),plife=ClothInspect(c.parent,parent);
    if(life==ClothLife::Unreadable||plife==ClothLife::Unreadable){ok=false;continue;}
    if(life==ClothLife::Destroyed||plife==ClothLife::Destroyed){c.lease.position=c.lease.rotation=c.lease.pendingPosition=c.lease.pendingRotation=false;++preserved;continue;}
    void *actualParent=nullptr;if(!ClothInvoke(ClothMethod(g_transformClass,"get_parent","UnityEngine.Transform"),t,nullptr,actualParent)){ok=false;continue;}
    if(actualParent!=parent){c.lease.position=c.lease.rotation=c.lease.pendingPosition=c.lease.pendingRotation=false;++preserved;continue;}
    eiem_cloth_shoulder::Pose p{};if(!ClothShoulderRead(t,p)){ok=false;continue;}
    if(c.lease.position||c.lease.pendingPosition){if(!c.lease.RestorePosition(p)){c.lease.position=c.lease.pendingPosition=false;++preserved;}
      else if(ClothShoulderSetPosition(t,c.lease.original.position)&&ClothShoulderRead(t,p)&&eiem_cloth_shoulder::Same(p.position,c.lease.original.position)){c.lease.position=c.lease.pendingPosition=false;++restored;}
      else ok=false;}
    if(c.lease.rotation||c.lease.pendingRotation){if(!c.lease.RestoreRotation(p)){c.lease.rotation=c.lease.pendingRotation=false;++preserved;}
      else if(ClothShoulderSetRotation(t,c.lease.original.rotation)&&ClothShoulderRead(t,p)&&eiem_cloth_shoulder::Same(p.rotation,c.lease.original.rotation)){c.lease.rotation=c.lease.pendingRotation=false;++restored;}
      else ok=false;}
  }
  if(!ok)return false;
  ClothShoulderContactRelease();
  if(s.handler){void *unused=nullptr;if(!ClothShoulderInvokePoseReader(s.dispose,CollisionGc(s.handler),nullptr,unused))return false;il2cpp_gchandle_free(s.handler);s.handler=0;}
  if(s.pose)il2cpp_gchandle_free(s.pose);
  if(s.referenceDescription)il2cpp_gchandle_free(s.referenceDescription);
  for(auto &c:s.controls){ClothFree(c.bone);ClothFree(c.parent);}
  ClothFree(s.animator);ClothFree(s.avatar);ClothFree(s.root);ClothFree(s.renderer);ClothFree(s.visibleMesh);
  if(s.updates)Log("[CLOTH-SHOULDER-DRIVE] stage=restored session=%llu generation=%llu command=%u updates=%u restoredFields=%u preservedExternalOrReplaced=%u nativeAnimationMayResume=1",
      s.owner.session,s.owner.generation,s.command,s.updates,restored,preserved);
  const bool attempted=s.attempted;s={};s.attempted=attempted;return true;
}
static void ClothShoulderDriverCancel() {
  if(!ClothOnMainThread())return;
  auto &s=s_clothShoulderDriver;s.active=false;s.stopping=true;s.attempted=false;
  if(!ClothShoulderRestore()){s.nextRestore=GetTickCount64()+100;Log("[CLOTH-SHOULDER-DRIVE] stage=restore-pending retainedOwnReferences=1 lowerClothUnchanged=1");}
}
static void ClothShoulderDriverMaintenance() {
  auto &s=s_clothShoulderDriver;if(!ClothOnMainThread()||!s.stopping||GetTickCount64()<s.nextRestore)return;
  s.nextRestore=GetTickCount64()+100;ClothShoulderRestore();
}
static void ClothShoulderDriverFail(const char *reason){auto &s=s_clothShoulderDriver;
  Log("[CLOTH-SHOULDER-DRIVE] stage=unavailable reason=%s prepareStep=%s controlIndex=%d session=%llu generation=%llu command=%u updates=%u lowerClothUnchanged=1",reason,s.prepareStep,s.prepareControl,s.owner.session,s.owner.generation,s.command,s.updates);
  s.active=false;s.stopping=true;s.attempted=true;s.nextRestore=GetTickCount64()+100;ClothShoulderRestore();
}
static bool ClothShoulderOutsideBbc() {
  std::set<void*> roots;
  for(int k=0;k<s_cloth.count;++k){auto &i=s_cloth.instances[k];auto bbc=ClothTarget(i.ref);void *data=nullptr,*list=nullptr;int count=0;
    if(!bbc||!ClothInvoke(i.api.serialize,bbc,nullptr,data)||!data||!ClothField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",list)||!list||
        !ClothValue(ClothMethod(il2cpp_object_get_class(list),"get_Count","System.Int32"),list,count)||count<0||count>128)return false;
    for(int n=0;n<count;++n){auto t=CollisionItem(list,n,"UnityEngine.Transform");if(!t)return false;roots.insert(t);}}
  auto animatorRoot=ClothTarget(s_clothShoulderDriver.root);if(!animatorRoot)return false;
  for(const auto &c:s_clothShoulderDriver.controls){auto t=ClothTarget(c.bone);int depth=0;
    for(;t&&t!=animatorRoot&&depth<128;++depth,t=CollisionParent(t))if(roots.count(t))return false;
    if(t!=animatorRoot||roots.count(t))return false;}
  return true;
}
struct ClothShoulderFrameRefs {
  struct Entry {eiem_cloth::ObjectId id{};void *object=nullptr;};
  std::array<Entry,96> entries{};size_t count=0;
  void *Get(const ClothRef &ref){
    auto object=ref.handle?il2cpp_gchandle_get_target(ref.handle):nullptr;if(!object)return nullptr;
    for(size_t n=0;n<count;++n)if(entries[n].id==ref.id&&entries[n].object==object)return object;
    if(ClothInspect(ref,object)!=ClothLife::Alive)return nullptr;
    if(count<entries.size())entries[count++]={ref.id,object};return object;
  }
};
#include "cloth_shoulder_contact_runtime.h"
static ClothActorBank<std::unique_ptr<ClothShoulderContactRuntime>> s_clothShoulderContactActors;
#define s_clothShoulderContact (s_clothShoulderContactActors.Get())
static void ClothShoulderContactRelease(){
  if(s_clothShoulderContact){ClothShoulderContactClear(*s_clothShoulderContact);s_clothShoulderContact.reset();}
}
static void ClothShoulderContactDisable(const char *reason){
  Log("[CLOTH-SHOULDER-CONTACT] stage=unavailable reason=%s nativeShapeDriverRetained=1 bodySetters=0",reason);
  ClothShoulderContactRelease();
}
static bool ClothShoulderDriverIdentity(bool audit,std::array<void*,10> *resolved=nullptr) {
  auto &s=s_clothShoulderDriver;ClothShoulderFrameRefs refs;
  auto animator=refs.Get(s.animator),renderer=refs.Get(s.renderer);void *avatar=nullptr,*mesh=nullptr;int scene=0;
  if(!ClothOwns(s.owner)||!ClothEnhancementSetting().load(std::memory_order_acquire)||!s_cloth.bodyGuard||
      !animator||animator!=refs.Get(s_cloth.animator)||!ClothScene(animator,scene)||scene!=s.scene||scene!=s_cloth.scene||
      !renderer||!ClothInvoke(s.getAvatar,animator,nullptr,avatar)||avatar!=refs.Get(s.avatar)||
      CollisionTransform(animator)!=refs.Get(s.root)||!ClothInvoke(s.getMesh,renderer,nullptr,mesh)||!mesh)return false;
  if(audit&&!ClothShoulderEvidenceIdentity())return false;
  if(mesh!=refs.Get(s.visibleMesh)){if(!audit&&!ClothShoulderEvidenceMeshIdentity(renderer,mesh))return false;
    auto replacement=ClothProtect(mesh);if(!replacement.handle)return false;ClothFree(s.visibleMesh);s.visibleMesh=replacement;}
  for(size_t n=0;n<s.controls.size();++n){auto &c=s.controls[n];auto t=refs.Get(c.bone);
    if(!t||CollisionParent(t)!=refs.Get(c.parent)||!s_cloth.bodyGuard(t))return false;if(resolved)(*resolved)[n]=t;}
  return !audit||ClothShoulderOutsideBbc();
}
static bool ClothShoulderReaderPrepare() {
  auto &s=s_clothShoulderDriver;s.prepareControl=-1;s.prepareStep="reader-classes";
  auto cls=SurfaceClass("UnityEngine","HumanPoseHandler"),poseClass=SurfaceClass("UnityEngine","HumanPose"),trait=SurfaceClass("UnityEngine","HumanTrait");
  if(!cls||!poseClass||!trait||!il2cpp_class_value_size||!il2cpp_object_new)return false;
  auto ctor=SurfaceMethod(cls,".ctor","System.Void","UnityEngine.Avatar","UnityEngine.Transform");
  s.read=SurfaceMethod(cls,"GetHumanPose","System.Void","UnityEngine.HumanPose&");s.dispose=SurfaceMethod(cls,"Dispose","System.Void");
  auto getNames=ClothMethod(trait,"get_MuscleName","System.String[]",nullptr,true);void *names=nullptr;uintptr_t count=0;uint32_t align=0;
  s.prepareStep="reader-pose-value-size";const int stride=il2cpp_class_value_size(poseClass,&align);if(stride<32||stride>128)return false;
  s.muscleOffset=ClothValueOffset(poseClass,"muscles","System.Single[]",stride,sizeof(void*));
  s.prepareStep="reader-method-or-muscle-field-ABI";if(!ctor||!s.read||!s.dispose||s.muscleOffset<0)return false;
  s.prepareStep="reader-muscle-names-array";if(!ClothInvoke(getNames,nullptr,nullptr,names)||!ClothArray(names,"System.String[]",count)||count<55||count>128)return false;
  s.muscles=int(count);for(auto &side:s.feature)for(int &id:side)id=-1;
  s.prepareStep="reader-muscle-names-match";
  for(size_t n=0;n<count;++n){void *name=nullptr;memcpy(&name,(char*)names+32+n*sizeof(void*),sizeof(void*));char value[128]{};
    if(ReadStrUtf8(name,value,sizeof(value))<=0)return false;
    for(int side=0;side<2;++side)for(int k=0;k<7;++k){const std::string wanted=std::string(side?"Right ":"Left ")+eiem_cloth_shoulder::FeatureNames[k];
      if(wanted==value){if(s.feature[side][k]>=0)return false;s.feature[side][k]=int(n);}}}
  for(auto &side:s.feature)for(int id:side)if(id<0)return false;
  s.prepareStep="reader-handler-allocation";auto handler=il2cpp_object_new(cls);if(!handler||!(s.handler=il2cpp_gchandle_new(handler,false)))return false;
  s.prepareStep="reader-handler-constructor";void *unused=nullptr,*args[]{ClothTarget(s.avatar),ClothTarget(s.root)};if(!ClothShoulderInvokePoseReader(ctor,handler,args,unused))return false;
  s.prepareStep="reader-pose-allocation";auto pose=il2cpp_object_new(poseClass);if(!pose||!(s.pose=il2cpp_gchandle_new(pose,false)))return false;
  Log("[CLOTH-SHOULDER-DRIVE] stage=reader-ready count=%d leftChannels=%d,%d,%d,%d,%d,%d,%d rightChannels=%d,%d,%d,%d,%d,%d,%d separateReadOnlyHandler=1 bodySetters=0",
      s.muscles,s.feature[0][0],s.feature[0][1],s.feature[0][2],s.feature[0][3],s.feature[0][4],s.feature[0][5],s.feature[0][6],
      s.feature[1][0],s.feature[1][1],s.feature[1][2],s.feature[1][3],s.feature[1][4],s.feature[1][5],s.feature[1][6]);return true;
}
static bool ClothShoulderDriverPrepare() {
  auto &s=s_clothShoulderDriver;const auto &e=s_clothShoulderEvidence;s.attempted=true;s.owner=e.owner;s.command=e.command;
  s.prepareStep="source-identity-and-animation-table";s.prepareControl=-1;
  if(!e.prepared||!ClothShoulderEvidenceIdentity()||!eiem_cloth_shoulder::Validate(eiem_cloth_shoulder::AglinaSamples,eiem_cloth_shoulder::AglinaSampleCount))return false;
  s.prepareStep="owner-avatar-and-scene";auto animator=ClothTarget(s_cloth.animator);if(!animator)return false;
  void *avatar=nullptr;s.getAvatar=ClothMethod(il2cpp_object_get_class(animator),"get_avatar","UnityEngine.Avatar");
  if(!ClothInvoke(s.getAvatar,animator,nullptr,avatar)||!avatar||!ClothScene(animator,s.scene)||s.scene!=s_cloth.scene)return false;
  s.animator=ClothProtect(animator);s.avatar=ClothProtect(avatar);s.root=ClothProtect(CollisionTransform(animator));
  s.renderer=ClothProtect(ClothTarget(e.renderer));s.visibleMesh=ClothProtect(ClothTarget(e.mesh));
  s.getMesh=SurfaceMethod(il2cpp_object_get_class(ClothTarget(s.renderer)),"get_sharedMesh","UnityEngine.Mesh");
  s.setPosition=SurfaceMethod(g_transformClass,"set_localPosition","System.Void","UnityEngine.Vector3");
  s.setRotation=SurfaceMethod(g_transformClass,"set_localRotation","System.Void","UnityEngine.Quaternion");
  s.getLocal=SurfaceMethod(g_transformClass,"GetLocalPositionAndRotation","System.Void","UnityEngine.Vector3&","UnityEngine.Quaternion&");
  s.prepareStep="object-handles-and-transform-ABI";if(!s.animator.handle||!s.avatar.handle||!s.root.handle||!s.renderer.handle||!s.visibleMesh.handle||!s.getMesh||!s.getLocal||!s.setPosition||!s.setRotation)return false;
  s.prepareStep="linked-avatar-description";void *description=nullptr,*skeleton=nullptr,*human=nullptr;
  if(!ClothInvoke(ClothMethod(il2cpp_object_get_class(avatar),"get_humanDescription","UnityEngine.HumanDescription"),avatar,nullptr,description)||!description||
      !(s.referenceDescription=il2cpp_gchandle_new(description,false)))return false;
  s.prepareStep="linked-avatar-array-metadata";
  auto boneClass=SurfaceClass("UnityEngine","SkeletonBone"),humanClass=SurfaceClass("UnityEngine","HumanBone");
  if(!boneClass||!humanClass||!ClothField(description,"skeleton","UnityEngine.SkeletonBone[]",skeleton)||
      !ClothField(description,"human","UnityEngine.HumanBone[]",human))return false;
  std::set<void*> unique;
  for(int side=0;side<2;++side)for(int k=0;k<5;++k){const std::string name=std::string(side?"R":"L")+"_Bone_Chain_"+std::to_string(k+1);size_t index=e.bones.size();
    s.prepareControl=side*5+k;s.prepareStep="control-unique-source-name";
    for(size_t n=0;n<e.source.bones.size();++n)if(e.source.bones[n].bone.name==name){if(index!=e.bones.size())return false;index=n;}
    if(index==e.bones.size())return false;auto &c=s.controls[side*5+k];auto t=ClothTarget(e.bones[index]);
    s.prepareStep="control-current-body-ownership";if(!t||!s_cloth.bodyGuard||!s_cloth.bodyGuard(t)||!unique.insert(t).second)return false;
    ClothAnchor anchor{};strncpy_s(anchor.name,name.c_str(),_TRUNCATE);strncpy_s(anchor.parentName,e.source.bones[index].bone.parent.c_str(),_TRUNCATE);
    s.prepareStep="control-avatar-metadata";if(!ClothFindBindRecord(skeleton,human,boneClass,humanClass,anchor)){
      Log("[CLOTH-SHOULDER-PREPARE] control=%s reason=%s",name.c_str(),anchor.bindReason);return false;}
    s.prepareStep="control-linked-avatar-reference";
    const auto bind=ClothShoulderPose(anchor.bindPosition,anchor.bindRotation),&expected=eiem_cloth_shoulder::AglinaAvatarBind[side*5+k];
    if(!eiem_cloth_shoulder::Same(bind.position,expected.position)||!eiem_cloth_shoulder::Same(bind.rotation,expected.rotation)){
      Log("[CLOTH-SHOULDER-PREPARE] control=%s reason=linked-avatar-reference-mismatch actualP=(%g,%g,%g) actualQ=(%g,%g,%g,%g) expectedP=(%g,%g,%g) expectedQ=(%g,%g,%g,%g)",name.c_str(),
          bind.position[0],bind.position[1],bind.position[2],bind.rotation[0],bind.rotation[1],bind.rotation[2],bind.rotation[3],
          expected.position[0],expected.position[1],expected.position[2],expected.rotation[0],expected.rotation[1],expected.rotation[2],expected.rotation[3]);return false;}
    s.prepareStep="control-live-snapshot";
    c.bone=ClothProtect(t);c.parent=ClothProtect(ClothTarget(e.parents[index]));eiem_cloth_shoulder::Pose original{};
    if(!c.bone.handle||!c.parent.handle||!ClothShoulderRead(t,original))return false;c.lease.Capture(original);
  }
  il2cpp_gchandle_free(s.referenceDescription);s.referenceDescription=0;
  s.prepareControl=-1;s.prepareStep="owner-identity-and-BBC-exclusion";
  if(!ClothShoulderDriverIdentity(true)||!ClothShoulderReaderPrepare())return false;
  s_clothShoulderContact=std::make_unique<ClothShoulderContactRuntime>();
  if(!ClothShoulderContactPrepare(*s_clothShoulderContact))ClothShoulderContactDisable(s_clothShoulderContact->reason);
  else Log("[CLOTH-SHOULDER-CONTACT] stage=prepared source=original-sleeve-skin bones=%zu samplesPerSide=192 inheritedOverlapAllowance=1 readbackMatrixAgreement=1 matrixGetterNoBox=1 controlledBones=10 bodySetters=0",
      s_clothShoulderContact->bones.size());
  s.prepareStep="complete";
  s.active=true;s.nextAudit=GetTickCount64()+250;
  Log("[CLOTH-SHOULDER-DRIVE] stage=prepared session=%llu generation=%llu command=%u controls=10 samples=%zu source=verified-native-animation avatarReference=separate-linked-source compiledAvatarCab=%s skin=original fixedChestPositions=4 bbcParticleOverlap=0",
      s.owner.session,s.owner.generation,s.command,eiem_cloth_shoulder::AglinaSampleCount,eiem_cloth_shoulder::AglinaAvatarCabHash);return true;
}
static bool ClothShoulderDriverUpdate(int poseFrame) {
  auto &s=s_clothShoulderDriver;if(!ClothOnMainThread()||!s_clothInputHooks||s_clothInputUpdateDepth!=1||poseFrame<0)return true;
  if(!ClothShoulderEvidenceWatching()){if(s.active)ClothShoulderDriverCancel();return true;}
  if(s.stopping||(!s.active&&s.attempted)||!s_cloth.bodyGuard)return true;
  if(!s.active){LARGE_INTEGER begin{},end{},frequency{};QueryPerformanceFrequency(&frequency);QueryPerformanceCounter(&begin);
    if(!ClothShoulderDriverPrepare()){ClothShoulderDriverFail("source-ownership-bind-or-pose-reader-unavailable");return true;}
    QueryPerformanceCounter(&end);if(frequency.QuadPart>0)s.prepareCostMs=double(end.QuadPart-begin.QuadPart)*1000/frequency.QuadPart;}
  const int frame=ClothFrame();if(frame<0||s.frame==frame)return true;
  LARGE_INTEGER begin{},end{},frequency{};QueryPerformanceFrequency(&frequency);QueryPerformanceCounter(&begin);
  const auto now=GetTickCount64();const bool audit=now>=s.nextAudit;
  std::array<void*,10> controls{};
  if(!ClothShoulderDriverIdentity(audit,&controls)){ClothShoulderDriverFail("owner-avatar-renderer-parent-or-BBC-ownership-changed");return true;}
  if(audit)s.nextAudit=now+250;
  std::array<eiem_cloth_shoulder::Pose,10> current{};for(size_t n=0;n<10;++n){if(!ClothShoulderRead(controls[n],current[n])||!s.controls[n].lease.Uncontested(current[n])){
      ClothShoulderDriverFail("shoulder-control-written-by-another-owner");return true;}}
  auto pose=CollisionGc(s.pose),handler=CollisionGc(s.handler);void *unused=nullptr,*args[]{pose?(char*)pose+16:nullptr};
  if(!pose||!handler||!ClothShoulderInvokePoseReader(s.read,handler,args,unused))return false;
  void *muscles=nullptr;memcpy(&muscles,(char*)pose+16+s.muscleOffset,sizeof(void*));uintptr_t count=0;
  if(!ClothArray(muscles,"System.Single[]",count)||count!=size_t(s.muscles))return false;
  std::array<eiem_cloth_shoulder::Feature,2> feature{};eiem_cloth_shoulder::Result results[2];
  for(int side=0;side<2;++side){for(int k=0;k<7;++k)memcpy(&feature[side][k],(char*)muscles+32+s.feature[side][k]*sizeof(float),sizeof(float));
    if(!eiem_cloth_shoulder::Evaluate(eiem_cloth_shoulder::AglinaSamples,eiem_cloth_shoulder::AglinaSampleCount,feature[side],side!=0,results[side]))return false;}
  std::array<eiem_cloth_shoulder::Pose,10> targets{};
  for(size_t n=0;n<10;++n){targets[n]=results[n/5].pose[n%5];if(n%5==0||n%5==4)targets[n].position=s.controls[n].lease.original.position;}
  if(s_clothShoulderContact&&!ClothShoulderContactApply(*s_clothShoulderContact,audit,targets))ClothShoulderContactDisable(s_clothShoulderContact->reason);
  const float dt=s.lastMs?float((std::min)(now-s.lastMs,uint64_t(100)))*.001f:1.f/60;
  const float alpha=1.f-std::exp(-dt/.06f);float maxMove=0;
  for(size_t n=0;n<10;++n){auto &c=s.controls[n];auto target=targets[n];
    target=eiem_cloth_shoulder::Blend(current[n],target,alpha);auto t=controls[n];
    if(!t||!ClothOwns(s.owner))return false;
    for(size_t k=0;k<3;++k)maxMove=(std::max)(maxMove,std::abs(target.position[k]-c.lease.original.position[k]));
    if(!eiem_cloth_shoulder::Same(current[n].position,target.position)){
      c.lease.attempt.position=target.position;c.lease.pendingPosition=true;if(!ClothShoulderSetPosition(t,target.position))return false;
      c.lease.last.position=target.position;c.lease.position=true;c.lease.pendingPosition=false;}
    if(!eiem_cloth_shoulder::Same(current[n].rotation,target.rotation)){
      c.lease.attempt.rotation=target.rotation;c.lease.pendingRotation=true;if(!ClothShoulderSetRotation(t,target.rotation))return false;
      c.lease.last.rotation=target.rotation;c.lease.rotation=true;c.lease.pendingRotation=false;}
    eiem_cloth_shoulder::Pose actual{};if(!ClothShoulderRead(t,actual)||!c.lease.Uncontested(actual))return false;
  }
  s.frame=frame;s.lastMs=now;++s.updates;
  QueryPerformanceCounter(&end);if(frequency.QuadPart>0)s.lastCostMs=double(end.QuadPart-begin.QuadPart)*1000/frequency.QuadPart;
  s.totalCostMs+=s.lastCostMs;s.maxCostMs=(std::max)(s.maxCostMs,s.lastCostMs);
  if(s.updates<=2||now>=s.nextLog){s.nextLog=now+5000;
    if(s_clothShoulderContact){const auto &c=*s_clothShoulderContact;const auto &l=c.stats[0],&r=c.stats[1];
      Log("[CLOTH-SHOULDER-CONTACT] stage=active frame=%d solves=%u reused=%u activeSamples=(%u,%u) envelopeDeficitBeforeM=(%g,%g) envelopeDeficitAfterM=(%g,%g) maxCorrectionM=(%g,%g) maxCorrectionRadians=(%g,%g) stepMs=%.3f meanMs=%.3f maxMs=%.3f renderedDepthKnown=0 bodySetters=0",
          frame,c.solves,c.reused,l.active,r.active,l.before,r.before,l.after,r.after,l.maxTranslation,r.maxTranslation,l.maxRotation,r.maxRotation,c.lastMs,c.totalMs/(c.solves+c.reused),c.maxMs);}
    Log("[CLOTH-SHOULDER-DRIVE] stage=active backend=%s session=%llu generation=%llu command=%u frame=%d submittedPoseFrame=%d updates=%u readbackControls=10 maxPositionDeltaM=%g nearestNativePose=(%g,%g) samples=(%zu,%zu) prepareMs=%.3f stepMs=%.3f meanStepMs=%.3f maxStepMs=%.3f loggingCostExcluded=1 bodySetters=0 originalSkin=1 collisionGuarantee=0",
        "MMD",s.owner.session,s.owner.generation,s.command,frame,poseFrame,s.updates,maxMove,
        results[0].nearest,results[1].nearest,results[0].sample,results[1].sample,s.prepareCostMs,s.lastCostMs,s.totalCostMs/s.updates,s.maxCostMs);
    Log("[CLOTH-SHOULDER-DRIVE-INPUT] frame=%d left=(%g,%g,%g,%g,%g,%g,%g) right=(%g,%g,%g,%g,%g,%g,%g)",frame,
        feature[0][0],feature[0][1],feature[0][2],feature[0][3],feature[0][4],feature[0][5],feature[0][6],
        feature[1][0],feature[1][1],feature[1][2],feature[1][3],feature[1][4],feature[1][5],feature[1][6]);}
  return true;
}
static void ClothShoulderDriverCompleted(int poseFrame) {
  bool ok=false;__try {ok=ClothShoulderDriverUpdate(poseFrame);}__except(EXCEPTION_EXECUTE_HANDLER){ok=false;}
  if(!ok)ClothShoulderDriverFail("pose-read-or-control-write-readback-failed");
}
