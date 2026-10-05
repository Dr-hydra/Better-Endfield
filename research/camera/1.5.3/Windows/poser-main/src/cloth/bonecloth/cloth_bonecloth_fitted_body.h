#pragma once
static bool ClothBoneFittedBodyGeometry(size_t n) {
  auto &l=ClothBoneState().local;const auto &recipe=*l.recipe;
  if(n>=l.fittedBody.size()||n>=size_t(recipe.bodySphereCount)||!recipe.bodySpheres)return false;
  const auto &r=l.fittedBody[n];const auto &fit=recipe.bodySpheres[n];
  auto c=ClothTarget(r.collider),t=ClothTarget(r.transform),p=ClothTarget(r.parent);Vector3 position{},scale{};Quaternion rotation{};
  if(fit.bone<0||size_t(fit.bone)>=l.bodyRenderer.bones.size()||!c||!t||!p||
      p!=ClothTarget(l.bodyRenderer.bones[fit.bone])||CollisionTransform(c)!=t||CollisionParent(t)!=p||!ClothAnchorUnderOwner(p)||
      !SurfaceVisiblePose(t,position,rotation,scale)||!ClothSameLocal(position,rotation,fit.center,fit.rotation)||
      fabsf(scale.x-1)>1e-5f||fabsf(scale.y-1)>1e-5f||fabsf(scale.z-1)>1e-5f)return false;
  const auto g=CollisionReadGeometry(c);
  if(!g.valid||!g.enabled||!g.active||!g.uniform||strcmp(g.type,fit.Capsule()?"BeyondBoneCapsuleCollider":"BeyondBoneSphereCollider")||
      !ClothFinitePosition(g.center)||eiem_collision::Length(CollisionV(g.center))>=1e-6||
      !std::isfinite(g.size.x)||fabsf(g.size.x-fit.radius)>=1e-6f)return false;
  return !fit.Capsule()||(g.flagsKnown&&!strcmp(g.direction,"X")&&!g.reverse&&!g.centered&&g.separated&&
      std::isfinite(g.size.y)&&std::isfinite(g.size.z)&&fabsf(g.size.y-fit.endRadius)<1e-6f&&fabsf(g.size.z-fit.length)<1e-6f);
}
static bool ClothBoneFittedBodyIdentity() {
  auto &l=ClothBoneState().local;
  if(!l.fittedBodyCreated)return l.fittedBody.empty();
  if(l.fittedBody.size()!=size_t(l.recipe->bodySphereCount)||!ClothBoneBodySourceIdentity())return false;
  for(size_t n=0;n<l.fittedBody.size();++n)if(!ClothBoneFittedBodyGeometry(n))return false;
  return true;
}
static bool ClothBoneFittedBodyCreate() {
  if(!ClothOnMainThread())return false;
  auto &s=ClothBoneState();auto &l=s.local;const auto &r=*l.recipe;
  const bool panels=s.profile&&eiem_cloth_asset::SourceLongLegPanels(*s.profile)&&r.NativePanelsOnly()&&r.nativeLayer==2&&r.bodySphereCount==2;
  const bool longSkirt=s.profile&&r.resampledPanel&&eiem_cloth_asset::SourceSeraphPanel(*s.profile)&&r.bodySphereCount==14;
  const bool thighs=s.profile&&s.profile->runtimeBodyOnly&&eiem_cloth_asset::SourceBodyContact(s.profile->prefabSha,s.profile->component)&&r.NativeBodyOnly();
  const bool coat=s.profile&&((eiem_cloth_asset::SourceOriginalCoverageCoat(s.profile->prefabSha,s.profile->component)&&r.NativePanelsOnly()&&r.CoatCalfCoverage())||
      (eiem_cloth_asset::SourceForkCoatFront(*s.profile)&&r.ForkCoatBodyOnly()));
  if(!l.requested||!s.profile||!r.runtimeGenerated||!r.bodyCoverage||(!panels&&!longSkirt&&!thighs&&!coat)||
      !r.bodyAsset||!r.bodySpheres||l.fittedBodyCreated||!l.fittedBody.empty()||
      !ClothOwns(s.owner)||s.stopRequested||!s_clothSurfaceAtBoundary||s_clothInputUpdateDepth!=1||!ClothBoneBodySourceIdentity())return false;
  auto ctor=ClothMethod(g_gameObjectClass,".ctor","System.Void","System.String"),add=ClothMethod(g_gameObjectClass,"AddComponent","UnityEngine.Component","System.Type");
  if(!ctor||!add)return false;
  for(int n=0;n<r.bodySphereCount;++n){const auto &fit=r.bodySpheres[n];
    if(fit.bone<0||size_t(fit.bone)>=l.bodyRenderer.bones.size()||!ClothFinitePosition(fit.center)||!std::isfinite(fit.radius)||fit.radius<=0||fit.radius>.25f)return false;
    const bool hip=coat&&r.sourceCoatCalves&&r.CoatTorsoCoverage()&&n==2;
    const bool torso=coat&&r.CoatTorsoCoverage()&&n>=r.bodySphereCount-2;
    const bool capsule=fit.Capsule();if(capsule&&((longSkirt&&n<12)||!std::isfinite(fit.endRadius)||fit.endRadius<=0||fit.endRadius>.25f||(!thighs&&!torso&&!hip&&fit.endRadius>=fit.radius)||
        !std::isfinite(fit.length)||fit.length<=fit.radius+fit.endRadius||fit.length>1))return false;
    if(!capsule&&(panels||thighs||coat||n>=12||fit.endRadius!=0||fit.length!=0))return false;
    auto cls=SurfaceClass("BeyondDynamicBone",capsule?"BeyondBoneCapsuleCollider":"BeyondBoneSphereCollider");
    auto size=capsule?CollisionCapsuleSizeMethod(cls):ClothMethod(cls,"SetSize","System.Void","System.Single"),update=ClothMethod(cls,"UpdateParameters","System.Void");
    if(!cls||!size||!update)return false;
    auto parent=ClothTarget(l.bodyRenderer.bones[fit.bone]);Vector3 scale{};
    if(!parent||!ClothAnchorUnderOwner(parent)||!CollisionScale(parent,scale)||!eiem_collision::UniformPositive(CollisionV(scale))||!CollisionUniformFrame(parent,scale.x))return false;
    char name[96]{};snprintf(name,sizeof(name),"EIEM_BoneCloth_FittedBody_%d",n);
    void *go=il2cpp_object_new(g_gameObjectClass),*label=il2cpp_string_new(name),*unused=nullptr,*args[]{label};
    if(!go||!label||!ClothBoneHold(go)||!ClothBoneHold(label))return false;
    const bool made=ClothInvoke(ctor,go,args,unused);auto ref=ClothProtect(go);
    if(!ref.handle){void *destroy[]{go};ClothInvoke(ClothMethod(SurfaceClass("UnityEngine","Object"),"Destroy","System.Void","UnityEngine.Object",true),nullptr,destroy,unused);return false;}
    l.objects.push_back(ref);l.destroyIssued.push_back(false);auto t=CollisionTransform(go);
    if(!made||!t||!SurfaceTRS(t,parent,fit.center,fit.rotation,Vector3{1,1,1}))return false;
    l.fittedBody.emplace_back();auto &shape=l.fittedBody.back();shape.transform=ClothProtect(t);shape.parent=ClothProtect(parent);
    void *sceneBox=nullptr;int scene=0;
    if(!shape.transform.handle||!shape.parent.handle||!ClothInvoke(s_clothUnity.scene,go,nullptr,sceneBox)||!ClothField(sceneBox,"m_Handle","System.Int32",scene)||scene!=s_cloth.scene)return false;
    void *type=il2cpp_type_get_object(il2cpp_class_get_type(cls)),*component=nullptr,*addArgs[]{type};
    if(!type||!ClothBoneHold(type)||!ClothOwns(s.owner)||!ClothInvoke(add,go,addArgs,component)||!component)return false;
    shape.collider=ClothProtect(component);float radius=fit.radius,endRadius=fit.endRadius,length=fit.length;void *sizeArgs[]{&radius,&endRadius,&length};bool member=false;int count=-1;
    if(!shape.collider.handle||!ClothInvoke(size,component,sizeArgs,unused))return false;
    if(capsule&&(!SurfaceEnum(component,"direction","BeyondDynamicBone.BeyondBoneCapsuleCollider.Direction","X")||
        !SurfaceScalar(component,"reverseDirection","System.Boolean",false)||!SurfaceScalar(component,"alignedOnCenter","System.Boolean",false)||
        !SurfaceScalar(component,"radiusSeparation","System.Boolean",true)))return false;
    if(!ClothInvoke(update,component,nullptr,unused)||!ClothBoneFittedBodyGeometry(n)||
        !CollisionTeams(component,s.team[0],member,count)||member||count!=0)return false;
    Log("[CLOTH-BONE-FITTED-BODY] stage=created generation=%llu index=%d collider=%d parent=%d center=%g,%g,%g radius=%g endRadius=%g length=%g shape=%s scale=%g originalCapsuleWrites=0 nativeRegistration=pending",
        s.owner.generation,n,shape.collider.id.instance,shape.parent.id.instance,fit.center.x,fit.center.y,fit.center.z,radius,endRadius,length,capsule?(hip?"pelvis-bridge-capsule":torso?(n==r.bodySphereCount-2?"chest-capsule":"transverse-waist-capsule"):thighs?"thigh-capsule":"calf-capsule"):"proximal-sphere",scale.x);
  }
  l.fittedBodyCreated=true;return ClothBoneFittedBodyIdentity();
}
static bool ClothBoneFittedBodyRegistration(void *process,int team,bool restoring) {
  auto &s=ClothBoneState();auto &l=s.local;
  if(!l.fittedBodyCreated)return l.fittedBody.empty();
  const bool candidate=!restoring&&process==CollisionGc(s.process[1]);
  for(const auto &r:l.fittedBody){void *c=nullptr;const auto life=ClothInspect(r.collider,c);if(life==ClothLife::Destroyed&&!candidate)continue;
    bool member=false,listed=false;int count=-1;
    if(life!=ClothLife::Alive||!process||team<=0||!CollisionTeams(c,team,member,count)||!CollisionProcessContains(process,c,listed)||
        member!=candidate||listed!=candidate||(!restoring&&count!=int(candidate)))return false;}
  if(candidate&&!l.fittedBodyRegistered){l.fittedBodyRegistered=true;int capsules=0;for(int n=0;n<l.recipe->bodySphereCount;++n)capsules+=l.recipe->bodySpheres[n].Capsule();
    const bool thighs=l.recipe->NativeBodyOnly();
    const int torso=l.recipe->sourceCoatTorso?2:0;
    const int hips=l.recipe->sourceCoatTorso&&l.recipe->sourceCoatCalves?1:0;
    Log("[CLOTH-BONE-FITTED-BODY] stage=registered generation=%llu Process=%p team=%d shapes=%zu spheres=%d calfCapsules=%d thighCapsules=%d torsoCapsules=%d hipBridgeCapsules=%d listAndTeamReadback=1 contactVerified=0 visualVerified=0",s.owner.generation,process,team,l.fittedBody.size(),int(l.fittedBody.size())-capsules,thighs?0:capsules-torso-hips,thighs?capsules:0,torso,hips);}
  return true;
}
static bool ClothBoneFittedBodyReleaseReady() {
  auto &s=ClothBoneState();auto &l=s.local;
  for(const auto &r:l.fittedBody){if(!r.collider.handle)continue;void *c=nullptr;const auto life=ClothInspect(r.collider,c);
    if(life==ClothLife::Destroyed)c=CollisionGc(r.collider.handle);
    else {bool member=false;int count=-1;if(life!=ClothLife::Alive||!CollisionTeams(c,s.team[1],member,count)||member||count!=0)return false;}
    if(!c)continue;
    for(int n=0;n<s_cloth.count;++n){auto &i=s_cloth.instances[n];void *bbc=nullptr,*data=nullptr,*constraint=nullptr,*list=nullptr;
      const auto state=ClothInspect(i.ref,bbc);if(state==ClothLife::Destroyed)continue;
      if(state!=ClothLife::Alive||!ClothInvoke(i.api.serialize,bbc,nullptr,data))return false;
      if(data==CollisionGc(s.candidateData))continue;bool has=false;
      if(!CollisionList(data,constraint,list)||!CollisionContains(list,c,"BeyondDynamicBone.ColliderComponent",has)||has)return false;}
    if(auto data=CollisionGc(s.candidateData)){void *constraint=nullptr,*list=nullptr;bool has=false;
      if(!CollisionList(data,constraint,list))return false;
      for(int attempt=0;;++attempt){if(!CollisionContains(list,c,"BeyondDynamicBone.ColliderComponent",has))return false;if(!has)break;if(attempt>=128)return false;
        void *unused=nullptr,*args[]{c};if(!ClothInvoke(ClothMethod(il2cpp_object_get_class(list),"Remove","System.Boolean","BeyondDynamicBone.ColliderComponent"),list,args,unused))return false;}}
  }
  return true;
}
