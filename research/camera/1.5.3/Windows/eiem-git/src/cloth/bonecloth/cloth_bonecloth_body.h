#pragma once
static bool ClothBoneBodyGeometry() {
  auto &l=ClothBoneState().local;
  auto c=ClothTarget(l.bodyCollider),t=ClothTarget(l.bodyTransform),p=ClothTarget(l.bodyParent);
  if(!c || !t || !p || CollisionTransform(c)!=t || CollisionParent(t)!=p || !ClothAnchorUnderOwner(p)) return false;
  Vector3 position{},scale{};Quaternion rotation{};
  if(!SurfaceVisiblePose(t,position,rotation,scale) ||
      !ClothSameLocal(position,rotation,Vector3{},Quaternion{0,0,0,1}) ||
      fabsf(scale.x-1)>1e-5f || fabsf(scale.y-1)>1e-5f || fabsf(scale.z-1)>1e-5f) return false;
  const auto g=CollisionReadGeometry(c);
  return g.valid && g.enabled && g.active && g.uniform && !strcmp(g.type,"BeyondBoneSphereCollider") &&
      ClothFinitePosition(g.center) && eiem_collision::Length(CollisionV(g.center))<1e-6 &&
      std::isfinite(g.size.x) && fabsf(g.size.x-ClothBodyContactRadius)<1e-6f;
}
static bool ClothBoneBodySourceIdentity() {
  auto &l=ClothBoneState().local;auto &r=l.bodyRenderer;auto renderer=ClothTarget(r.renderer);bool enabled=false;
  return renderer && ClothValue(SurfaceMethod(il2cpp_object_get_class(renderer),"get_enabled","System.Boolean"),renderer,enabled) && enabled &&
      ClothBoneRendererAssetCheck(r,l.recipe->bodyAsset?*l.recipe->bodyAsset:ClothBodyContactAsset,false);
}
#include "cloth_bonecloth_fitted_body.h"
static bool ClothBoneBodyBindingIdentity() {
  auto &l=ClothBoneState().local;
  if(l.recipe->bodyAsset)return ClothBoneFittedBodyIdentity();
  if(!l.bodyCreated) return true;
  return ClothBodyContactBone>=0 && size_t(ClothBodyContactBone)<l.bodyRenderer.bones.size() &&
      ClothTarget(l.bodyParent)==ClothTarget(l.bodyRenderer.bones[ClothBodyContactBone]) &&
      ClothBoneBodySourceIdentity() && ClothBoneBodyGeometry();
}
static bool ClothBoneBodyRegistration(void *process,int team,bool restoring) {
  auto &s=ClothBoneState();auto &l=s.local;
  if(l.recipe->bodyAsset)return ClothBoneFittedBodyRegistration(process,team,restoring);
  if(!l.bodyCreated) return true;
  void *c=nullptr;const auto life=ClothInspect(l.bodyCollider,c);
  const bool candidate=!restoring && process==CollisionGc(s.process[1]);
  if(life==ClothLife::Destroyed) return !candidate;
  bool member=false,listed=false;int count=-1;
  if(life!=ClothLife::Alive || !process || team<=0 || !CollisionTeams(c,team,member,count) ||
      !CollisionProcessContains(process,c,listed) || member!=candidate || listed!=candidate ||
      (!restoring && count!=(candidate?1:0))) return false;
  if(candidate && !l.bodyRegisteredLogged) {
    l.bodyRegisteredLogged=true;
    Log("[CLOTH-BONE-BODY] stage=registered generation=%llu instance=%d Process=%p team=%d collider=%d recipe=%s contactVerified=0 visualVerified=0",
        (unsigned long long)s.owner.generation,s.bbc.id.instance,process,team,l.bodyCollider.id.instance,ClothBodyContactSignature);
  }
  return true;
}
static bool ClothBoneBodyCreate() {
  if(!ClothOnMainThread()) return false;
  auto &s=ClothBoneState();auto &l=s.local;
  if(l.recipe->bodyAsset)return ClothBoneFittedBodyCreate();
  if(!l.requested || l.bodyCreated || l.bodyCollider.handle || !ClothOwns(s.owner) || s.stopRequested ||
      !s_clothSurfaceAtBoundary || s_clothInputUpdateDepth!=1 ||
      !ClothBoneBodySourceIdentity() ||
      ClothBodyContactBone<0 || size_t(ClothBodyContactBone)>=l.bodyRenderer.bones.size()) return false;
  auto parent=ClothTarget(l.bodyRenderer.bones[ClothBodyContactBone]);Vector3 scale{};
  if(!parent || !ClothAnchorUnderOwner(parent) || !CollisionScale(parent,scale) ||
      !eiem_collision::UniformPositive(CollisionV(scale)) || !CollisionUniformFrame(parent,scale.x)) return false;
  auto cls=SurfaceClass("BeyondDynamicBone","BeyondBoneSphereCollider");
  auto ctor=ClothMethod(g_gameObjectClass,".ctor","System.Void","System.String");
  auto add=ClothMethod(g_gameObjectClass,"AddComponent","UnityEngine.Component","System.Type");
  auto size=ClothMethod(cls,"SetSize","System.Void","System.Single");
  auto update=ClothMethod(cls,"UpdateParameters","System.Void");
  if(!cls || !ctor || !add || !size || !update) return false;
  void *go=il2cpp_object_new(g_gameObjectClass),*label=il2cpp_string_new("EIEM_BoneCloth_BodyContact"),*unused=nullptr,*args[]{label};
  if(!go || !label || !ClothBoneHold(go) || !ClothBoneHold(label)) return false;
  const bool made=ClothInvoke(ctor,go,args,unused);
  auto ref=ClothProtect(go);
  if(!ref.handle) {
    void *destroy[]{go};ClothInvoke(ClothMethod(SurfaceClass("UnityEngine","Object"),"Destroy","System.Void","UnityEngine.Object",true),nullptr,destroy,unused);
    return false;
  }
  l.objects.push_back(ref);l.destroyIssued.push_back(false);
  auto t=CollisionTransform(go);
  if(!made || !t || !SurfaceTRS(t,parent,Vector3{},Quaternion{0,0,0,1},Vector3{1,1,1})) return false;
  l.bodyTransform=ClothProtect(t);l.bodyParent=ClothProtect(parent);
  void *sceneBox=nullptr;int scene=0;
  if(!l.bodyTransform.handle || !l.bodyParent.handle || !ClothInvoke(s_clothUnity.scene,go,nullptr,sceneBox) ||
      !ClothField(sceneBox,"m_Handle","System.Int32",scene) || scene!=s_cloth.scene) return false;
  void *type=il2cpp_type_get_object(il2cpp_class_get_type(cls)),*component=nullptr,*addArgs[]{type};
  if(!type || !ClothBoneHold(type) || !ClothOwns(s.owner) || !ClothInvoke(add,go,addArgs,component) || !component) return false;
  l.bodyCollider=ClothProtect(component);float radius=ClothBodyContactRadius;void *sizeArgs[]{&radius};
  if(!l.bodyCollider.handle || !ClothInvoke(size,component,sizeArgs,unused) ||
      !ClothInvoke(update,component,nullptr,unused) || !ClothBoneBodyGeometry()) return false;
  bool member=false;int count=-1;
  if(!CollisionTeams(component,s.team[0],member,count) || member || count!=0) return false;
  l.bodyCreated=true;
  Log("[CLOTH-BONE-BODY] stage=created generation=%llu collider=%d parent=%d recipe=%s localRadius=%g scale=%g registrationPending=1 originalGeometryWrites=0 bodyMeshWrites=0",
      (unsigned long long)s.owner.generation,l.bodyCollider.id.instance,l.bodyParent.id.instance,ClothBodyContactSignature,radius,scale.x);
  return true;
}
static bool ClothBoneBodyReleaseReady() {
  auto &s=ClothBoneState();auto &l=s.local;
  if(!ClothBoneFittedBodyReleaseReady())return false;
  if(!l.bodyCollider.handle) return true;
  void *c=nullptr;const auto life=ClothInspect(l.bodyCollider,c);
  if(life==ClothLife::Destroyed) return true;
  bool member=false;int count=-1;
  if(life!=ClothLife::Alive || !CollisionTeams(c,s.team[1],member,count) || member || count!=0) return false;
  for(int n=0;n<s_cloth.count;++n) {
    auto &i=s_cloth.instances[n];void *bbc=nullptr,*data=nullptr,*constraint=nullptr,*list=nullptr;
    const auto bbcLife=ClothInspect(i.ref,bbc);if(bbcLife==ClothLife::Destroyed) continue;
    if(bbcLife!=ClothLife::Alive || !ClothInvoke(i.api.serialize,bbc,nullptr,data)) return false;
    if(data==CollisionGc(s.candidateData)) continue;
    bool has=false;
    if(!CollisionList(data,constraint,list) || !CollisionContains(list,c,"BeyondDynamicBone.ColliderComponent",has) || has) return false;
  }
  if(auto data=CollisionGc(s.candidateData)) {
    void *constraint=nullptr,*list=nullptr;bool has=false;
    if(!CollisionList(data,constraint,list) || !CollisionContains(list,c,"BeyondDynamicBone.ColliderComponent",has)) return false;
    if(has) {
      void *result=nullptr,*args[]{c};
      if(!ClothInvoke(ClothMethod(il2cpp_object_get_class(list),"Remove","System.Boolean","BeyondDynamicBone.ColliderComponent"),list,args,result) ||
          !CollisionContains(list,c,"BeyondDynamicBone.ColliderComponent",has) || has) return false;
    }
  }
  return true;
}
static void ClothBoneBodyFree() {
  auto &l=ClothBoneState().local;auto &r=l.bodyRenderer;
  for(auto &v:l.fittedBody){ClothFree(v.collider);ClothFree(v.transform);ClothFree(v.parent);}l.fittedBody.clear();l.fittedBodyCreated=l.fittedBodyRegistered=false;
  ClothFree(l.bodyCollider);ClothFree(l.bodyTransform);ClothFree(l.bodyParent);
  ClothFree(r.renderer);ClothFree(r.mesh);ClothFree(r.root);ClothFree(r.qualifiedParent);
  for(auto &b:r.bones) ClothFree(b);for(auto &p:r.parents) ClothFree(p);
  r={};l.bodyCreated=l.bodyRegisteredLogged=false;
}
