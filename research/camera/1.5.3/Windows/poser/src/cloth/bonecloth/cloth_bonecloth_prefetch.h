#pragma once

struct ClothPrefetchState {
  std::shared_ptr<eiem_cloth_asset::Job> job;
  ClothRef animator{};
  uint32_t entity=0;
  uintptr_t character=0;
  uint64_t invalidation=0,nextTry=0,nextPathCheck=0;
  int scene=0;
  unsigned attempts=0,hookMask=0;
  bool reported=false,busy=false;
  std::set<void*> body;
  std::string model;
};
static ClothActorBank<ClothPrefetchState> s_clothPrefetchActors;
#define s_clothPrefetch (s_clothPrefetchActors.Get())
#define s_clothPrefetchBody (s_clothPrefetch.body)
static bool ClothPrefetchReadBranch(void *t){return t&&!s_clothPrefetchBody.count(t);}
static void ClothPrefetchCancel() {
  auto &p=s_clothPrefetch;
  if(p.job)p.job->cancel.store(true,std::memory_order_release);
  if(p.job&&p.job->done.load(std::memory_order_acquire))p.job.reset();
  ClothFree(p.animator);if(p.entity)il2cpp_gchandle_free(p.entity);
  p.entity=0;p.character=0;p.hookMask=0;p.model.clear();p.attempts=0;p.reported=false;
  p.nextTry=p.nextPathCheck=0;s_clothPrefetchBody.clear();
}
static bool ClothPrefetchIdentity() {
  auto &p=s_clothPrefetch;int scene=0;
  return p.character&&p.character==uintptr_t(ClothHostEntity())&&p.invalidation==s_clothInvalidation.load()&&
      p.entity&&il2cpp_gchandle_get_target(p.entity)==ClothHostEntity()&&
      ClothTarget(p.animator)==ClothHostAnimator()&&ClothScene(ClothHostAnimator(),scene)&&scene==p.scene;
}
static bool ClothPrefetchWorkerPending() {
  auto &p=s_clothPrefetch;
  if(!p.job||p.job->done.load(std::memory_order_acquire))return false;
  if(!ClothHostEnabled()||!ClothEnhancementSetting().load()||!ClothPrefetchIdentity())p.job->cancel.store(true,std::memory_order_release);
  return true;
}
static bool ClothPrefetchNeedsHooks() {
  return ClothHostEnabled()&&ClothEnhancementSetting().load()&&ClothHostIdle()&&
      !s_cloth.active&&!s_cloth.releasing&&!ClothBonePending()&&s_clothPrefetch.hookMask!=0;
}
static void ClothPrefetchBoundary() {
  if(!ClothPrefetchNeedsHooks()||!ClothOnMainThread()||!s_clothSurfaceAtBoundary||
      s_clothInputUpdateDepth!=1||!ClothPrefetchIdentity())return;
  const struct {unsigned bit;const char *name;bool (*install)();} hooks[]{
      {1,"contact",s_clothContactJobInstaller},{2,"layer",s_clothLayerInstaller},
      {4,"elastic",s_clothElasticInstaller},{8,"display",s_clothDisplayInstaller},{16,"finish",s_clothFinishInstaller}};
  for(const auto &h:hooks)if(s_clothPrefetch.hookMask&h.bit) {
    s_clothPrefetch.hookMask&=~h.bit;
    const auto start=GetTickCount64();const bool ok=h.install&&h.install();
    Log("[CLOTH-PREFETCH] stage=native-interface name=%s ok=%d elapsedMs=%llu ownerWrites=0 nativeParametersChanged=0",h.name,int(ok),GetTickCount64()-start);
    break;
  }
}
struct ClothPrefetchSources {
  std::vector<ClothInstance> instances;
  ~ClothPrefetchSources(){for(auto &i:instances)ClothFree(i.ref);}
};
static bool ClothPrefetchDiscover(void *animator,ClothAutoCaptureHolds &holds,ClothPrefetchSources &out) {
  struct Node {void *t;int depth;};std::vector<Node> stack;
  auto root=CollisionTransform(animator);if(!root||!holds.Add(root))return false;stack.push_back({root,0});
  auto type=il2cpp_type_get_object(il2cpp_class_get_type(g_componentClass));if(!holds.Add(type))return false;
  std::set<int> identities;int visited=0;
  while(!stack.empty()) {
    const auto n=stack.back();stack.pop_back();if(++visited>4096)return false;if(ClothOwnedRoot(n.t))continue;
    void *go=nullptr,*array=nullptr,*args[]{type};uintptr_t count=0;
    if(!ClothInvoke(s_clothUnity.getGO,n.t,nullptr,go)||!holds.Add(go)||
        !ClothInvoke(s_clothUnity.components,go,args,array)||!holds.Add(array)||
        !ClothArray(array,"UnityEngine.Component[]",count)||count>256)return false;
    for(size_t k=0;k<count;++k) {
      auto obj=reinterpret_cast<void**>((char*)array+32)[k];if(!obj)continue;auto cls=il2cpp_object_get_class(obj);
      if(!ClothTypeIs(il2cpp_class_get_type(cls),"BeyondDynamicBone.BeyondBoneCloth"))continue;
      auto ref=ClothProtect(obj);if(!ref.handle)return false;
      if(!identities.insert(ref.id.instance).second){ClothFree(ref);continue;}
      if(out.instances.size()>=ClothCapacity){ClothFree(ref);return false;}
      out.instances.emplace_back();auto &i=out.instances.back();i.ref=ref;
      i.api.process=ClothMethod(cls,"get_Process","BeyondDynamicBone.ClothProcess");
      i.api.serialize=ClothMethod(cls,"get_SerializeData","BeyondDynamicBone.ClothSerializeData");
      if(!i.api.process||!i.api.serialize)return false;CollisionName(go,i.name,sizeof(i.name));
    }
    int children=0;if(!ClothValue(s_clothUnity.childCount,n.t,children)||children<0||children>128||(n.depth>=64&&children))return false;
    for(int k=0;k<children;++k){void *child=nullptr,*a[]{&k};if(!ClothInvoke(s_clothUnity.child,n.t,a,child)||!holds.Add(child))return false;stack.push_back({child,n.depth+1});}
  }
  return true;
}
static bool ClothPrefetchReservations(ClothPrefetchSources &sources,void *animator,std::set<int> &reserved,
    std::vector<const ClothBoneProfile*> &authored) {
  struct Match{int index;const ClothBoneProfile *profile;};std::vector<Match> found;std::set<std::string> priority;
  for(int n=0;n<int(sources.instances.size());++n)for(auto p:s_clothBoneCatalog)if(ClothBoneRootNames(sources.instances[n],*p,animator)) {
    for(const auto &m:found)if(m.index==n||(!strcmp(m.profile->component,p->component)&&!strcmp(m.profile->prefabSha,p->prefabSha)))return false;
    found.push_back({n,p});if(found.size()>s_clothBoneSlots.size())return false;
    if(ClothBoneAcceptedConnections(*p)||ClothBoneLocalRecipeFor(*p))priority.insert(p->component);
  }
  for(size_t pass=0;pass<found.size();++pass)for(const auto &m:found)if(priority.count(m.profile->component))
    for(int k=0;k<m.profile->dependencyCount;++k)priority.insert(m.profile->dependencies[k]);
  for(const auto &m:found)if(priority.count(m.profile->component)){reserved.insert(m.index);authored.push_back(m.profile);}
  for(const auto &m:found)if(priority.count(m.profile->component)&&!strcmp(m.profile->signature,ClothContactPartnerConsumer)) {
    const auto p=ClothContactPartnerProfiles[0];authored.push_back(p);
    for(int n=0;n<int(sources.instances.size());++n)if(ClothBoneRootNames(sources.instances[n],*p,animator))reserved.insert(n);
  }
  return true;
}
static bool ClothPrefetchCapture() {
  auto &p=s_clothPrefetch;auto animator=ClothTarget(p.animator);if(!animator||!ClothPrefetchIdentity())return false;
  ClothAutoCaptureHolds holds;ClothAutoCaptureRefs refs;ClothPrefetchSources sources;
  if(!ClothPrefetchDiscover(animator,holds,sources))return false;
  std::set<int> reserved;std::vector<const ClothBoneProfile*> authored;
  if(!ClothPrefetchReservations(sources,animator,reserved,authored))return false;
  s_clothPrefetchBody.clear();for(int n=0;n<CollisionBodyCount;++n)if(auto t=CollisionBody(animator,n))s_clothPrefetchBody.insert(t);
  auto job=std::make_shared<eiem_cloth_asset::Job>();job->dataRoot=ClothAutoDataRoot();job->character=p.character;
  const char *issue="idle-owner-source-unavailable";
  const bool captured=ClothAutoCaptureInput(reinterpret_cast<void*>(p.character),animator,sources.instances.data(),int(sources.instances.size()),
      reserved,authored,job->query,refs,holds,ClothPrefetchReadBranch,&issue);
  s_clothPrefetchBody.clear();
  if(!captured||job->dataRoot.empty()||job->query.modelPath.empty()||!ClothPrefetchIdentity()) {
    Log("[CLOTH-PREFETCH] stage=skipped reason=%s normalPlaybackPreparationRetained=1",issue);return false;
  }
  p.model=job->query.modelPath;
  if(!eiem_cloth_asset::StartJob(job))return false;p.job=std::move(job);p.reported=false;
  Log("[CLOTH-PREFETCH] stage=start owner=%p scene=%d model=%s components=%zu renderers=%zu UnityWrites=0 workerUnityCalls=0",
      reinterpret_cast<void*>(p.character),p.scene,p.model.c_str(),p.job->query.cloths.size(),p.job->query.renderers.size());
  return true;
}
static void ClothPrefetchPulseImpl() {
  auto &p=s_clothPrefetch;const auto now=GetTickCount64();
  if(!ClothHostEnabled()||!ClothEnhancementSetting().load()){ClothPrefetchCancel();return;}
  if(p.character&&!ClothPrefetchIdentity())ClothPrefetchCancel();
  if(!ClothHostIdle()||s_cloth.active||s_cloth.releasing||ClothBonePending())return;
  if(s_clothAutoJob&&!s_clothAutoJob->done.load(std::memory_order_acquire))return;
  if(p.job&&!p.job->done.load(std::memory_order_acquire))return;
  if(!ClothHostEntity()||!ClothHostAnimator()||!ClothResolveUnity())return;
  if(!p.character) {
    if(now<p.nextTry)return;
    if(!ClothScene(ClothHostAnimator(),p.scene))return;
    p.animator=ClothProtect(ClothHostAnimator());p.entity=il2cpp_gchandle_new(ClothHostEntity(),false);
    if(!p.animator.handle||!p.entity){ClothPrefetchCancel();return;}
    p.character=uintptr_t(ClothHostEntity());p.invalidation=s_clothInvalidation.load();
    p.nextTry=now+250;return;
  }
  if(p.job&&!p.job->cancel.load()&&!p.reported) {
    p.reported=true;
    const auto result=p.job->result;
    Log("[CLOTH-PREFETCH] stage=ready elapsedMs=%llu cache=%d cacheKey=%s generated=%zu reason=%s mainCaptureOnly=1 nativeCandidatesCreated=0",
        p.job->elapsedMs,int(p.job->cacheHit),p.job->cacheKey.c_str(),result?result->profiles.size():0,p.job->error.empty()?"complete":p.job->error.c_str());
    if(result)for(const auto &d:result->dense)if(d->view.resampledPanel)p.hookMask|=31;
  }
  if(p.hookMask&&s_clothInputHookInstaller)s_clothInputHookInstaller();
  if(p.reported&&p.job&&p.job->error.empty()) {
    if(now>=p.nextPathCheck){p.nextPathCheck=now+2000;if(ClothAutoModelPathFor(ClothHostEntity(),ClothHostAnimator())!=p.model)ClothPrefetchCancel();}
    return;
  }
  if(p.attempts>=3||now<p.nextTry)return;
  ++p.attempts;p.nextTry=now+2000;const auto start=GetTickCount64();ClothPrefetchCapture();
  Log("[CLOTH-PREFETCH] stage=capture elapsedMs=%llu attempt=%u ownerWrites=0",GetTickCount64()-start,p.attempts);
}
static void ClothPrefetchPulseCpp() {
  try { ClothPrefetchPulseImpl(); }
  catch(const std::exception &e){ClothPrefetchCancel();s_clothPrefetch.nextTry=GetTickCount64()+5000;
    Log("[CLOTH-PREFETCH] stage=cancelled reason=%s normalPlaybackPreparationRetained=1",e.what());}
  catch(...){ClothPrefetchCancel();s_clothPrefetch.nextTry=GetTickCount64()+5000;
    Log("[CLOTH-PREFETCH] stage=cancelled reason=metadata-read-exception normalPlaybackPreparationRetained=1");}
}
static void ClothPrefetchPulse() {
  auto &busy=s_clothPrefetch.busy;if(!ClothOnMainThread()||busy)return;busy=true;
  __try { ClothPrefetchPulseCpp(); }
  __except(EXCEPTION_EXECUTE_HANDLER){ClothPrefetchCancel();s_clothPrefetch.nextTry=GetTickCount64()+5000;Log("[CLOTH-PREFETCH] stage=cancelled reason=metadata-read-fault normalPlaybackPreparationRetained=1");}
  busy=false;
}
