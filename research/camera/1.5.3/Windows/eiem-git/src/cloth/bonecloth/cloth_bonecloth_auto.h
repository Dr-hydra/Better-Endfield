#pragma once
#include "../assets/cloth_asset_worker.h"
static std::shared_ptr<eiem_cloth_asset::Job> s_clothAutoJob;
static std::shared_ptr<eiem_cloth_asset::Generated> s_clothAutoLease;
static bool s_clothAutoWaiting=false;
static bool s_clothAutoSkipped=false;
static bool s_clothAutoNoRenderableSources=false;
static uint64_t s_clothAutoTriedSession=0,s_clothAutoTriedGeneration=0;
static bool ClothPrefetchWorkerPending();
static std::string ClothAutoModelPath();
struct ClothAutoCaptureHolds {
  std::vector<uint32_t> handles;
  ~ClothAutoCaptureHolds(){for(auto h:handles)il2cpp_gchandle_free(h);}
  bool Add(void *p){if(!p)return false;const auto h=il2cpp_gchandle_new(p,false);if(!h)return false;handles.push_back(h);return true;}
};
static bool ClothAutoHipsPath(void *animator,void *hips,ClothAutoCaptureHolds &holds,std::vector<std::string> &out);
struct ClothAutoSourceRef {ClothRef bbc{};uint32_t process=0,data=0;int index=-1;};
struct ClothAutoMeshRef {ClothRef renderer{},mesh{},root{};std::vector<ClothRef> bones,parents;};
#include "../diagnostics/cloth_shoulder_trace.h"
static std::vector<ClothAutoSourceRef> s_clothAutoSources;
static std::vector<ClothAutoMeshRef> s_clothAutoMeshes;
static void ClothAutoFreeSources(std::vector<ClothAutoSourceRef> &sources,std::vector<ClothAutoMeshRef> &meshes){for(auto &s:sources){ClothFree(s.bbc);if(s.process)il2cpp_gchandle_free(s.process);if(s.data)il2cpp_gchandle_free(s.data);}sources.clear();
  for(auto &r:meshes){ClothFree(r.renderer);ClothFree(r.mesh);ClothFree(r.root);for(auto &b:r.bones)ClothFree(b);for(auto &p:r.parents)ClothFree(p);}meshes.clear();}
static void ClothAutoClearSources(){ClothAutoFreeSources(s_clothAutoSources,s_clothAutoMeshes);}
struct ClothAutoCaptureRefs {std::vector<ClothAutoSourceRef> sources;std::vector<ClothAutoMeshRef> meshes;~ClothAutoCaptureRefs(){ClothAutoFreeSources(sources,meshes);}};
static void ClothAutoCancel(){if(s_clothAutoJob)s_clothAutoJob->cancel.store(true,std::memory_order_release);s_clothAutoWaiting=s_clothAutoDeferred=s_clothAutoNoRenderableSources=false;s_clothAutoFallbacks.clear();ClothAutoClearSources();ClothShoulderEvidenceClear();}
static bool ClothAutoSourceCurrent(){const auto owner=s_clothBone.owner;const auto command=s_clothBone.command;ClothAutoCaptureRefs captured;captured.sources.swap(s_clothAutoSources);captured.meshes.swap(s_clothAutoMeshes);
  if(captured.sources.empty()&&captured.meshes.empty())return false;
  for(auto &s:captured.sources){if(s.index<0||s.index>=s_cloth.count)return false;auto bbc=ClothTarget(s.bbc);auto &i=s_cloth.instances[s.index];void *process=nullptr,*data=nullptr;
    if(!bbc||bbc!=ClothTarget(i.ref)||!ClothInvoke(i.api.process,bbc,nullptr,process)||!ClothInvoke(i.api.serialize,bbc,nullptr,data)||!process||!data||process!=CollisionGc(s.process)||data!=CollisionGc(s.data))return false;}
  std::set<size_t> selected;
  const auto result=s_clothAutoJob?s_clothAutoJob->result:nullptr;
  if(result&&result->rendererScopeKnown){
    if(s_clothAutoJob->query.renderers.size()!=captured.meshes.size()||result->liveRenderers.empty())return false;
    for(auto k:result->liveRenderers)if(k>=captured.meshes.size()||!selected.insert(k).second)return false;
  }
  for(size_t k=0;k<captured.meshes.size();++k){
    if(result&&result->rendererScopeKnown&&!selected.count(k))continue;
    auto &r=captured.meshes[k];auto renderer=ClothTarget(r.renderer);void *mesh=nullptr,*root=nullptr;if(!renderer||!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(renderer),"get_sharedMesh","UnityEngine.Mesh"),renderer,nullptr,mesh)||mesh!=ClothTarget(r.mesh)||!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(renderer),"get_rootBone","UnityEngine.Transform"),renderer,nullptr,root)||root!=ClothTarget(r.root)||!SurfaceRenderSameReferences(renderer,"get_bones","UnityEngine.Transform[]",r.bones))return false;
    for(size_t n=0;n<r.bones.size();++n)if(!ClothTarget(r.bones[n])||CollisionParent(ClothTarget(r.bones[n]))!=ClothTarget(r.parents[n]))return false;}
  if(s_clothAutoJob&&!s_clothAutoJob->query.modelPath.empty()&&ClothAutoModelPath()!=s_clothAutoJob->query.modelPath)return false;
  if(s_clothAutoJob&&!s_clothAutoJob->query.hipsPath.empty()){
    ClothAutoCaptureHolds holds;std::vector<std::string> path;auto animator=ClothTarget(s_cloth.animator);
    if(!ClothAutoHipsPath(animator,CollisionBody(animator,0),holds,path)||path!=s_clothAutoJob->query.hipsPath)return false;}
  const bool current=ClothOwns(owner)&&s_clothBone.owner==owner&&s_clothBone.command==command&&(s_clothAutoDeferred||!s_clothBone.stopRequested);
  if(current&&s_clothAutoJob&&s_clothAutoJob->result)ClothShoulderEvidenceTryPrepare(*s_clothAutoJob->result,s_clothAutoJob->query,captured.meshes);
  return current;
}
static eiem_cloth_asset::BoneKey ClothAutoBoneKey(void *t){char name[128]{},parent[128]{};CollisionName(t,name,sizeof(name));CollisionName(CollisionParent(t),parent,sizeof(parent));return {name,parent};}
static bool ClothAutoHipsPath(void *animator,void *hips,ClothAutoCaptureHolds &holds,std::vector<std::string> &out){
  const auto root=CollisionTransform(animator);if(!root||!hips)return false;
  std::vector<std::string> path;std::set<void*> seen;auto t=hips;
  for(int n=0;t&&t!=root&&n<128;++n){if(!seen.insert(t).second||!holds.Add(t))return false;
    char name[128]{};CollisionName(t,name,sizeof(name));if(!name[0])return false;path.push_back(name);t=CollisionParent(t);}
  if(t!=root||path.empty())return false;out.assign(path.rbegin(),path.rend());return true;
}
static bool ClothAutoWaistRoots(void *hips,void *spine,void *left,void *right,
    ClothAutoCaptureHolds &holds,std::set<void*> &required,std::vector<void*> &branch,ClothBoneGuard guard=s_cloth.bodyGuard) {
  if(!guard)return true;
  for(void *parent:{hips,spine}){int count=0;if(!parent||!ClothValue(s_clothUnity.childCount,parent,count)||count<0||count>128)return false;
    for(int k=0;k<count;++k){void *child=nullptr,*args[]{&k};if(!ClothInvoke(s_clothUnity.child,parent,args,child)||!child)return false;
      if(child==left||child==right||child==spine||!guard(child))continue;
      if(!holds.Add(child))return false;if(required.insert(child).second)branch.push_back(child);}}
  return true;
}
static std::string ClothAutoModelPathFor(void *entity,void *animator){
  if(!ClothOnMainThread())return {};ClothAutoCaptureHolds holds;
  if(!entity||!holds.Add(entity)||!ClothTypeIs(il2cpp_class_get_type(il2cpp_object_get_class(entity)),"Beyond.Gameplay.Core.Entity"))return {};
  void *model=nullptr,*go=nullptr,*path=nullptr;
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(entity),"get_modelCom","Beyond.Gameplay.View.ModelComponent"),entity,nullptr,model)||!holds.Add(model)||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(model),"get_model","UnityEngine.GameObject"),model,nullptr,go)||!holds.Add(go)||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(model),"get_modelPath","System.String"),model,nullptr,path)||!holds.Add(path))return {};
  const auto modelRoot=CollisionTransform(go);auto t=CollisionTransform(animator);bool related=false;
  for(int n=0;t&&n<128;++n){if(t==modelRoot){related=true;break;}t=CollisionParent(t);}if(!related)return {};
  char text[4096]{};ReadStrUtf8(path,text,sizeof(text));std::string result=text;std::replace(result.begin(),result.end(),'\\','/');
  return eiem_cloth_asset::ResourcePath(result)&&result.find('/')!=std::string::npos?result:std::string{};
}
static std::string ClothAutoModelPath(){
  const auto owner=s_clothBone.owner;if(!ClothOwns(owner))return {};
  auto path=ClothAutoModelPathFor(reinterpret_cast<void*>(owner.character),ClothTarget(s_cloth.animator));
  return ClothOwns(owner)&&s_clothBone.owner==owner?path:std::string{};
}
static bool ClothAutoCaptureRenderers(const std::set<void*> &required,eiem_cloth_asset::Query &out,
    ClothAutoCaptureRefs &captured,ClothAutoCaptureHolds &holds,const char **issue=nullptr,void *animator=nullptr) {
  size_t bindingCount=0;char current[128]{};
  auto reject=[&](const char *reason){if(issue)*issue=reason;Log("[CLOTH-AUTO-CAPTURE] session=%llu generation=%llu command=%u reason=%s renderer=%s renderers=%zu bindings=%zu complete=0",(unsigned long long)s_clothBone.owner.session,(unsigned long long)s_clothBone.owner.generation,s_clothBone.command,reason,current,out.renderers.size(),bindingCount);return false;};
  if(!animator)animator=ClothTarget(s_cloth.animator);
  auto root=CollisionTransform(animator);if(!root)return reject("owner-transform-unavailable");struct Node{void *t;int depth;};std::vector<Node> nodes{{root,0}};
  for(size_t at=0;at<nodes.size();++at){if(nodes.size()>2048)return reject("owner-hierarchy-node-budget");const auto node=nodes[at];if(ClothOwnedRoot(node.t))continue;
    void *type=il2cpp_type_get_object(il2cpp_class_get_type(g_componentClass)),*go=nullptr,*array=nullptr,*args[]{type};uintptr_t count=0;
    if(!type||!ClothInvoke(s_clothUnity.getGO,node.t,nullptr,go)||!ClothInvoke(s_clothUnity.components,go,args,array)||!holds.Add(array)||!ClothArray(array,"UnityEngine.Component[]",count)||count>32)return reject("renderer-metadata-or-owner-unconfirmed");
    for(size_t k=0;k<count;++k){auto component=reinterpret_cast<void**>((char*)array+32)[k];if(!component||!ClothTypeIs(il2cpp_class_get_type(il2cpp_object_get_class(component)),"UnityEngine.SkinnedMeshRenderer"))continue;
      const auto rendererKey=ClothAutoBoneKey(node.t);strncpy_s(current,rendererKey.name.c_str(),_TRUNCATE);
      if(out.reservedRenderers.count({rendererKey.name,rendererKey.parent})||out.reservedRenderers.count({rendererKey.name,""}))continue;
      void *mesh=nullptr,*boneRoot=nullptr,*bones=nullptr,*binds=nullptr;uintptr_t numBones=0,numBinds=0;
      if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(component),"get_sharedMesh","UnityEngine.Mesh"),component,nullptr,mesh)||!mesh)continue;
      if(!holds.Add(mesh)||!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(component),"get_bones","UnityEngine.Transform[]"),component,nullptr,bones)||!holds.Add(bones)||!ClothArray(bones,"UnityEngine.Transform[]",numBones)||numBones>512)return reject("renderer-metadata-or-owner-unconfirmed");
      bool related=false;for(size_t b=0;b<numBones;++b)related|=required.count(reinterpret_cast<void**>((char*)bones+32)[b])!=0;if(!related)continue;
      if(out.renderers.size()>=256)return reject("owner-renderer-count-budget");
      if(numBones>32768-bindingCount)return reject("owner-renderer-binding-budget");bindingCount+=numBones;
      if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(component),"get_rootBone","UnityEngine.Transform"),component,nullptr,boneRoot)||!boneRoot||!ClothUnderAnimator(boneRoot,animator)||
          !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(mesh),"get_bindposes","UnityEngine.Matrix4x4[]"),mesh,nullptr,binds)||!holds.Add(binds)||!ClothArray(binds,"UnityEngine.Matrix4x4[]",numBinds)||numBones!=numBinds||
          !ClothInputLayout(SurfaceClass("UnityEngine","Matrix4x4"),"UnityEngine.Matrix4x4",64))return reject("renderer-metadata-or-owner-unconfirmed");
      captured.meshes.emplace_back();auto &refs=captured.meshes.back();refs.renderer=ClothProtect(component);refs.mesh=ClothProtect(mesh);refs.root=ClothProtect(boneRoot);if(!refs.renderer.handle||!refs.mesh.handle||!refs.root.handle)return reject("renderer-metadata-or-owner-unconfirmed");
      eiem_cloth_asset::LiveRenderer r;const auto key=ClothAutoBoneKey(node.t);r.name=key.name;r.parent=key.parent;char name[128]{},rootName[128]{};CollisionName(mesh,name,sizeof(name));CollisionName(boneRoot,rootName,sizeof(rootName));r.mesh=name;r.root=rootName;
      if(!ClothValue(SurfaceMethod(il2cpp_object_get_class(mesh),"get_vertexCount","System.Int32"),mesh,r.vertices)||!ClothValue(SurfaceMethod(il2cpp_object_get_class(mesh),"get_subMeshCount","System.Int32"),mesh,r.submeshes)||r.vertices<1||r.vertices>200000)return reject("renderer-metadata-or-owner-unconfirmed");
      for(size_t b=0;b<numBones;++b){auto t=reinterpret_cast<void**>((char*)bones+32)[b];if(!t||!ClothUnderAnimator(t,animator))return reject("renderer-metadata-or-owner-unconfirmed");refs.bones.push_back(ClothProtect(t));refs.parents.push_back(ClothProtect(CollisionParent(t)));if(!refs.bones.back().handle||!refs.parents.back().handle)return reject("renderer-metadata-or-owner-unconfirmed");eiem_cloth_asset::LiveBinding bind;bind.bone=ClothAutoBoneKey(t);float matrix[16]{};memcpy(matrix,(char*)binds+32+b*64,64);for(int j=0;j<16;++j){if(!std::isfinite(matrix[j]))return reject("renderer-metadata-or-owner-unconfirmed");bind.bind[j]=matrix[j];}r.bones.push_back(std::move(bind));}
      out.renderers.push_back(std::move(r));
    }
    int children=0;if(!ClothValue(s_clothUnity.childCount,node.t,children)||children<0||children>128||(node.depth>=32&&children))return reject("renderer-metadata-or-owner-unconfirmed");
    for(int k=0;k<children;++k){void *child=nullptr,*childArgs[]{&k};if(!ClothInvoke(s_clothUnity.child,node.t,childArgs,child)||!child||!holds.Add(child))return reject("renderer-metadata-or-owner-unconfirmed");nodes.push_back({child,node.depth+1});}
  }
  return !out.renderers.empty() || reject("owner-renderers-empty");
}
static bool ClothAutoCaptureInput(void *entity,void *animator,ClothInstance *instances,int instanceCount,
    const std::set<int> &reserved,const std::vector<const ClothBoneProfile*> &authored,eiem_cloth_asset::Query &out,
    ClothAutoCaptureRefs &captured,ClothAutoCaptureHolds &holds,ClothBoneGuard guard,const char **issue){
  if(!ClothOnMainThread()||!animator||!holds.Add(animator)||!holds.Add(entity))return false;std::set<void*> required;std::vector<void*> branch;
  void *hips=CollisionBody(animator,0),*left=CollisionBody(animator,6),*right=CollisionBody(animator,9),*spine=CollisionBody(animator,1);
  if(!hips||!left||!right||!spine)return false;
  if(!ClothAutoHipsPath(animator,hips,holds,out.hipsPath))return false;
  if(!ClothAutoWaistRoots(hips,spine,left,right,holds,required,branch,guard))return false;
  for(const auto p:authored)for(int n=0;n<p->rendererCount;++n)out.reservedRenderers.insert({p->renderers[n].name,p->renderers[n].parent?p->renderers[n].parent:""});
  for(int n=0;n<instanceCount;++n){if(reserved.count(n))continue;auto &i=instances[n];auto bbc=ClothTarget(i.ref);void *data=nullptr,*list=nullptr;
    if(!bbc||!ClothInvoke(i.api.serialize,bbc,nullptr,data)||!holds.Add(data)||!CollisionField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",list)||!holds.Add(list))return false;
    const int count=CollisionCount(list);if(count<0||count>128)return false;if(count>16||count<1||(count==1&&strcmp(i.name,"MC_Seraph_Skirt_Ribbon")))continue;
    if(!hips||!left||!right||!spine)return false;
    bool waist=true;for(int k=0;k<count;++k){auto t=CollisionItem(list,k,"UnityEngine.Transform");bool attached=false;
      for(int depth=0;t&&depth<128;++depth){if(t==left||t==right)break;if(t==hips){attached=depth>0;break;}t=CollisionParent(t);}waist&=attached;}
    if(!waist)continue;
    captured.sources.emplace_back();auto &source=captured.sources.back();source.bbc=ClothProtect(bbc);source.index=n;void *process=nullptr;
    if(!source.bbc.handle||!ClothInvoke(i.api.process,bbc,nullptr,process)||!process||!(source.process=il2cpp_gchandle_new(process,false))||!(source.data=il2cpp_gchandle_new(data,false)))return false;
    eiem_cloth_asset::LiveCloth c;c.name=i.name;for(int k=0;k<count;++k){auto t=CollisionItem(list,k,"UnityEngine.Transform");if(!t||!ClothUnderAnimator(t,animator)||!holds.Add(t))return false;c.roots.push_back(ClothAutoBoneKey(t));if(required.insert(t).second)branch.push_back(t);}out.cloths.push_back(std::move(c));
  }
  for(size_t n=0;n<branch.size();++n){if(branch.size()>2048)return false;int count=0;if(!ClothValue(s_clothUnity.childCount,branch[n],count)||count<0||count>128)return false;
    for(int k=0;k<count;++k){void *child=nullptr,*args[]{&k};if(!ClothInvoke(s_clothUnity.child,branch[n],args,child)||!child||!holds.Add(child))return false;if(required.insert(child).second)branch.push_back(child);}}
  out.modelPath=ClothAutoModelPathFor(entity,animator);
  out.hips=ClothAutoBoneKey(hips);out.leftThigh=ClothAutoBoneKey(left);out.rightThigh=ClothAutoBoneKey(right);out.spine=ClothAutoBoneKey(spine);
  if(auto chest=CollisionBody(animator,2)){if(!ClothUnderAnimator(chest,animator)||!holds.Add(chest))return false;out.chest=ClothAutoBoneKey(chest);}
  if(auto upper=CollisionBody(animator,3)){if(!ClothUnderAnimator(upper,animator)||!holds.Add(upper))return false;out.upperChest=ClothAutoBoneKey(upper);}
  for(const auto entry:std::initializer_list<std::pair<int,eiem_cloth_asset::BoneKey*>>{{7,&out.leftCalf},{10,&out.rightCalf},{8,&out.leftFoot},{11,&out.rightFoot}})
    if(auto t=CollisionBody(animator,entry.first)){if(!ClothUnderAnimator(t,animator)||!holds.Add(t))return false;*entry.second=ClothAutoBoneKey(t);}
  if(auto cls=SurfaceClass("BeyondDynamicBone","BeyondBoneCapsuleCollider"))if(auto field=CollisionFieldInfo(cls,"direction","BeyondDynamicBone.BeyondBoneCapsuleCollider.Direction")){
    auto e=il2cpp_class_from_type(il2cpp_field_get_type(field));const char *names[]{"X","Y","Z"};for(int n=0;n<3;++n)if(!CollisionEnumValue(e,names[n],out.capsuleDirections[n]))out.capsuleDirections[n]=-1;}
  Log("[CLOTH-AUTO-LEGS] leftCalf=%s rightCalf=%s leftFoot=%s rightFoot=%s source=Animator-HumanBodyBones",out.leftCalf.name.c_str(),out.rightCalf.name.c_str(),out.leftFoot.name.c_str(),out.rightFoot.name.c_str());
  std::string hipsPath;for(const auto &part:out.hipsPath){if(!hipsPath.empty())hipsPath+='/';hipsPath+=part;}Log("[CLOTH-AUTO-BODY-PATH] hips=%s relativeTo=current-Animator workerPointers=0",hipsPath.c_str());
  Log("[CLOTH-AUTO-BODY] model=%s hips=%s spine=%s chest=%s upperChest=%s leftThigh=%s rightThigh=%s source=Animator-HumanBodyBones",
      out.modelPath.c_str(),out.hips.name.c_str(),out.spine.name.c_str(),out.chest.name.c_str(),out.upperChest.name.c_str(),out.leftThigh.name.c_str(),out.rightThigh.name.c_str());
  return ClothAutoCaptureRenderers(required,out,captured,holds,issue,animator);
}
static bool ClothAutoCapture(const std::set<int> &reserved,const std::vector<const ClothBoneProfile*> &authored,eiem_cloth_asset::Query &out,const char **issue=nullptr){
  if(!ClothOnMainThread()||!ClothOwns(s_clothBone.owner))return false;ClothAutoCaptureHolds holds;ClothAutoCaptureRefs captured;
  const auto owner=s_clothBone.owner;const auto command=s_clothBone.command;
  if(!ClothAutoCaptureInput(reinterpret_cast<void*>(owner.character),ClothTarget(s_cloth.animator),s_cloth.instances,s_cloth.count,
      reserved,authored,out,captured,holds,s_cloth.bodyGuard,issue)||!ClothOwns(owner)||
      !(s_clothBone.owner==owner)||s_clothBone.command!=command||s_clothBone.stopRequested)return false;
  captured.sources.swap(s_clothAutoSources);captured.meshes.swap(s_clothAutoMeshes);return true;
}
static std::wstring ClothAutoDataRoot(){wchar_t name[32768]{};const DWORD n=GetModuleFileNameW(nullptr,name,DWORD(std::size(name)));if(!n||n>=std::size(name))return {};std::wstring path(name,n);const auto dot=path.find_last_of('.'),slash=path.find_last_of(L"\\/");if(dot==std::wstring::npos||dot<slash)return {};return path.substr(0,dot)+L"_Data";}
static int ClothAutoPrepare(const std::set<int> &reserved,const std::vector<const ClothBoneProfile*> &authored,bool acceptResult=true){
  using namespace eiem_cloth_asset;auto &state=s_clothBone;s_clothAutoWaiting=false;
  if(!state.autoSelect||s_clothAutoSkipped)return 1;
  if(ClothPrefetchWorkerPending()){s_clothAutoWaiting=true;return 0;}
  if(s_clothAutoJob&&!JobMatches(*s_clothAutoJob,state.owner.session,state.owner.generation,state.command,state.owner.backend,state.owner.character)){
    s_clothAutoJob->cancel.store(true,std::memory_order_release);if(!s_clothAutoJob->done.load(std::memory_order_acquire)){s_clothAutoWaiting=true;return 0;}s_clothAutoJob.reset();
  }
  if(!s_clothAutoJob){auto job=std::make_shared<Job>();job->session=state.owner.session;job->generation=state.owner.generation;job->command=state.command;job->backend=state.owner.backend;job->character=state.owner.character;job->dataRoot=ClothAutoDataRoot();
    job->foreground=g_clothPlaybackGate.Holding(job->backend,job->generation);
    LARGE_INTEGER start{},end{},frequency{};QueryPerformanceFrequency(&frequency);QueryPerformanceCounter(&start);
    const char *captureIssue="owner-source-descriptor-unavailable";ClothAutoClearSources();const bool captured=!job->dataRoot.empty()&&ClothAutoCapture(reserved,authored,job->query,&captureIssue);QueryPerformanceCounter(&end);
    Log("[CLOTH-AUTO] stage=capture ok=%d mainThreadMs=%.3f components=%zu renderers=%zu",captured,frequency.QuadPart?1000.*double(end.QuadPart-start.QuadPart)/frequency.QuadPart:0.,job->query.cloths.size(),job->query.renderers.size());
    Log("[CLOTH-AUTO] sourceLocator=%s modelPath=%s",job->query.modelPath.empty()?"full-binding-search":"owner-model-component",job->query.modelPath.empty()?"unavailable":job->query.modelPath.c_str());
    if(!captured){s_clothAutoNoRenderableSources=!strcmp(captureIssue,"owner-renderers-empty");ClothAutoClearSources();Log("[CLOTH-AUTO] stage=capture reason=%s authoredPreserved=1 completeEmpty=%d",captureIssue,int(s_clothAutoNoRenderableSources));return 1;}
    if(job->query.renderers.empty())return 1;
    if(!ClothOwns(state.owner)||state.stopRequested)return 1;
    if(!StartJob(job)){ClothAutoClearSources();Log("[CLOTH-AUTO] stage=prepare reason=worker-start-failed win32=%lu",GetLastError());return 1;}
    s_clothAutoJob=std::move(job);s_clothAutoLease.reset();ClothBoneNote("reading-current-outfit-source-original-simulation-retained");
    Log("[CLOTH-AUTO] stage=start session=%llu generation=%llu command=%u components=%zu renderers=%zu workerUnityCalls=0 singleDLL=1",
        (unsigned long long)state.owner.session,(unsigned long long)state.owner.generation,state.command,s_clothAutoJob->query.cloths.size(),s_clothAutoJob->query.renderers.size());
  }
  if(!s_clothAutoJob->done.load(std::memory_order_acquire)||!acceptResult){s_clothAutoWaiting=true;return 0;}
  if(!JobMatches(*s_clothAutoJob,state.owner.session,state.owner.generation,state.command,state.owner.backend,state.owner.character)||!ClothOwns(state.owner)||(!s_clothAutoDeferred&&state.stopRequested))return 1;
  if(!ClothAutoSourceCurrent()){Log("[CLOTH-AUTO] stage=discard reason=owner-Process-SerializeData-or-renderer-replaced originalUnchanged=1");ClothAutoCancel();s_clothAutoLease.reset();return 1;}
  s_clothAutoLease=s_clothAutoJob->result;
  ClothAutoClearSources();
  Log("[CLOTH-AUTO] stage=ready session=%llu generation=%llu elapsedMs=%llu generated=%zu source=%s key=%s reason=%s cache=%s livePreflight=pending GPUContentHashVerified=0",
      (unsigned long long)state.owner.session,(unsigned long long)state.owner.generation,(unsigned long long)s_clothAutoJob->elapsedMs,s_clothAutoLease?s_clothAutoLease->profiles.size():0,
      s_clothAutoLease?s_clothAutoLease->source.c_str():"unconfirmed",s_clothAutoLease?s_clothAutoLease->key.c_str():"none",s_clothAutoJob->error.empty()?"complete":s_clothAutoJob->error.c_str(),s_clothAutoJob->cacheHit?"payload-revalidated-hit":"generated");
  if(s_clothAutoLease)for(const auto &r:s_clothAutoLease->reports)Log("[CLOTH-AUTO] component=%s decision=%s",r.component.c_str(),r.reason.c_str());
  Log("[CLOTH-AUTO] stage=worker-cost indexMs=%llu generationMs=%llu cacheKey=%s unityCalls=0",(unsigned long long)s_clothAutoJob->indexMs,(unsigned long long)s_clothAutoJob->generationMs,s_clothAutoJob->cacheKey.c_str());
  if(s_clothAutoLease&&!s_clothAutoJob->cacheHit)Log("[CLOTH-AUTO] stage=source-decode reads=%zu reused=%zu decodeMs=%llu cacheBytes=%zu scope=current-immutable-source fullPayloadValidation=1",
      s_clothAutoLease->meshReads,s_clothAutoLease->meshCacheHits,(unsigned long long)s_clothAutoLease->meshDecodeMs,s_clothAutoLease->meshCacheBytes);
  if(s_clothAutoLease)for(const auto &p:s_clothAutoLease->profiles){const auto dense=p->view.generatedLocal;size_t changed=0;int packed=0,floating=0;
    if(dense)for(int k=0;k<dense->meshCount;++k){changed+=dense->meshes[k].skinVertices;packed+=dense->meshes[k].generatedWeights!=nullptr;floating+=dense->meshes[k].generatedFloatWeights!=nullptr;}
    Log("[CLOTH-AUTO] stage=generated-shape component=%s originalBones=%d candidatePoints=%d nativeFaces=%d addedBones=%d privateMeshes=%d changedSkinVertices=%zu partialSourceRegions=%d preservedRegionVertices=%zu packedMeshes=%d floatMeshes=%d sourcePanelFit=%d sourceApronFit=%d originalNativePoints=%d verifiedInputAnchors=%d belowWaistFixedSkinVertices=%zu source=installed-runtime-bytes catalogBoneTablesUsed=0 resampledLongPanel=%d fittedBodyShapes=%d originalCapsuleWrites=0 visualVerified=0",p->view.component,p->view.boneCount,(dense&&dense->resampledPanel?0:dense&&dense->sourceShortSkin?p->view.boneCount:p->view.EffectiveCount()+(dense&&dense->sourcePanelFit?eiem_cloth_asset::SourcePanelPromotedCount(p->view):0))+(dense?dense->addedCount:0),dense?dense->graphs[0].faceCount:p->view.FaceCount(),dense?dense->addedCount:0,dense?dense->meshCount:0,changed,dense&&dense->partialSurface,dense?dense->preservedVertices:0,packed,floating,dense&&dense->sourcePanelFit,dense&&dense->sourceApronFit,p->view.OriginalCount(),p->view.inputAnchorCount,dense?dense->fixedSkinVertices:0,dense&&dense->resampledPanel,dense?dense->bodySphereCount:0);}
  return 1;
}
