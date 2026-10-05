#pragma once
#include "../resources/cloth_embedded_resources.h"
#include "cloth_bonecloth_panel_policy.h"
#include "cloth_bonecloth_short_policy.h"
static bool ClothBoneLocalResource(size_t layer,const void *&bytes,DWORD &size) {
  auto &s=ClothBoneState();const auto &config=s.local.recipe->meshes[layer];
  bytes=nullptr;size=0;
  if(s.local.recipe->runtimeGenerated) {
    if(config.resource || !config.generatedBytes)return ClothBoneReject("runtime-private-resource-ownership-mismatch");
    bytes=config.generatedBytes;size=config.bytes;
  } else {
    eiem_cloth_resource::Embedded.Get(config.resource,bytes,size);
  }
  const auto limit=eiem_cloth_resource::Limit(s.local.recipe->runtimeGenerated);
  if(const char *issue=ClothBoneLocalResourceIssue(config,bytes,size,limit)) {
    Log("[CLOTH-BONE-RESOURCE] stage=rejected reason=%s generation=%llu session=%llu recipe=%s layer=%zu renderer=%s resource=%u expectedBytes=%u actualBytes=%lu expectedHash=%016llx actualHash=%016llx originalSimulationRetained=1",
        issue,(unsigned long long)s.owner.generation,(unsigned long long)s.owner.session,s.local.recipe->signature,layer,config.renderer,config.resource,config.bytes,size,
        (unsigned long long)config.hash,(unsigned long long)(bytes && eiem_cloth_resource::Fits(size,limit)?eiem_cloth_surface::RenderHash(bytes,size):0));
    return ClothBoneReject(issue);
  }
  return true;
}
static bool ClothBoneLocalMeshEnabled(const ClothBoneLocalMeshState &l,bool requireVisible) {
  auto &s=ClothBoneState();
  if(l.renderer<0 || size_t(l.renderer)>=s.renderers.size()) return false;
  auto r=ClothTarget(s.renderers[l.renderer].renderer);bool visible=false,enabled=false;
  return r && (!requireVisible || (ClothValue(SurfaceMethod(il2cpp_object_get_class(r),"get_isVisible","System.Boolean"),r,visible) && visible)) &&
      ClothValue(SurfaceMethod(il2cpp_object_get_class(r),"get_enabled","System.Boolean"),r,enabled) && enabled;
}
static bool ClothBoneLocalVisible() {
  auto &s=ClothBoneState();auto &local=s.local;auto &layers=local.layers;
  if(local.recipe->CoatWaistSkinOnly()) {
    if(layers.size()!=size_t(local.recipe->meshCount)||!s.profile||s.renderers.size()!=size_t(s.profile->rendererCount))return false;
    for(size_t k=0;k<s.renderers.size();++k){ClothBoneLocalMeshState source{};source.renderer=int(k);if(ClothBoneLocalMeshEnabled(source,true))return true;}
    return false;
  }
  if(local.recipe->NativeSkinRetained()) {
    if(!layers.empty()||!s.profile||s.renderers.size()!=size_t(s.profile->rendererCount))return false;
    for(size_t k=0;k<s.renderers.size();++k){ClothBoneLocalMeshState source{};source.renderer=int(k);if(ClothBoneLocalMeshEnabled(source,true))return true;}
    return false;
  }
  if(layers.empty() || layers.size()!=size_t(local.recipe->meshCount)) return false;
  if(local.recipe->multipleLod) {
    return std::any_of(layers.begin(),layers.end(),[](const ClothBoneLocalMeshState &l){return ClothBoneLocalMeshEnabled(l,true);});
  }
  for(size_t n=0;n<layers.size();++n) if(!ClothBoneLocalMeshEnabled(layers[n],n==0)) return false;
  return true;
}
static bool ClothBoneLocalProfile(ClothBoneRuntime &s) {
  auto &l=s.local;const auto &r=*l.recipe;
  if(!s.profile || ClothBoneLocalRecipeFor(*s.profile)!=l.recipe ||
      s.profile->boneCount!=r.originalCount || s.profile->rootCount!=r.originalRoots ||
      s.profile->depth!=r.depth || (s.profile->loop!=r.loop&&!r.sourceShortSides) || r.Total()>(r.resampledPanel?ClothBoneMaxIdentities:ClothContactParticles) ||
      (r.meshCount<1&&!r.NativeSkinRetained()) || r.meshCount>16 || (r.crossCount<1&&!r.RetainsSourceReference()) || r.crossCount>256 ||
      (s.profile->candidateIgnoredCount&&!r.runtimeGenerated) || (l.recipe==&ClothRingRecipe &&
      (strcmp(ClothLocalBaseSignature,ClothInnerBaseSignature) || strcmp(ClothLocalSignature,ClothInnerPrimarySignature)))) return false;
  if(r.separatedPanels&&!r.NativeSkinRetained()&&!r.NativeRibbonWidth())return false;
  if(r.separatedWidth&&(!r.NativeRibbonWidth()||!eiem_cloth_asset::SourceLegRibbonWidth(*s.profile)))return false;
  if(r.sourceCoatWaist&&(!r.CoatWaistSkinOnly()||!eiem_cloth_asset::SourceInactiveCoat(*s.profile)))return false;
  if(r.sourceBodyOnly&&(!r.NativeBodyOnly()||!s.profile->runtimeBodyOnly||!eiem_cloth_asset::SourceBodyContact(s.profile->prefabSha,s.profile->component)))return false;
  if(s.profile->runtimeBodyOnly&&!r.NativeBodyOnly())return false;
  if(s.profile->runtimeSeparatedCoat&&(!r.NativePanelsOnly()||!eiem_cloth_asset::SourceSeparatedCoat(*s.profile)))return false;
  l.assets.assign(s.profile->bones,s.profile->bones+s.profile->boneCount);
  if(r.sourcePanelFit&&!eiem_cloth_asset::SourcePanelContract(*s.profile))return false;
  if(r.resampledPanel&&(!r.sourcePanelFit||!eiem_cloth_asset::SourceSeraphPanel(*s.profile)||r.addedCount!=ClothLongPanelParticles||r.rootCount!=ClothLongPanelColumns||!r.bodyAsset||r.bodySphereCount!=14))return false;
  if(r.sourceApronFit&&(!eiem_cloth_asset::SourceApronRelease(*s.profile,1)||!eiem_cloth_asset::SourceApronRelease(*s.profile,4)))return false;
  if(r.sourceShortSkin&&(!r.NativeSkinRetained()||!eiem_cloth_asset::SourceShortContract(*s.profile)))return false;
  if(eiem_cloth_asset::SourceShortContract(*s.profile)&&!r.sourceShortSkin)return false;
  if(r.sourceShortSides!=eiem_cloth_asset::SourceShortSides(*s.profile)||
      (r.sourceShortSides&&(!r.loop||r.Total()!=36||r.rootCount!=12||r.meshCount)))return false;
  if(r.nativeLayer&&(r.nativeLayer<1||r.nativeLayer>2||!r.layerPeer||!r.layerFaces||r.layerFaceCount<1||r.layerFaceCount>512||
      (r.nativeLayer==1?!eiem_cloth_asset::SourceShortContract(*s.profile):!eiem_cloth_asset::SourceLongLegPanels(*s.profile))))return false;
  if(r.nativeLayer==2&&(!r.bodyCoverage||!r.bodyAsset||!r.bodySpheres||r.bodySphereCount!=2))return false;
  l.ignored.clear();
  for(int n=0;n<r.originalCount;++n) {l.assets[n].column=r.columns[n];if(r.runtimeGenerated&&s.profile->Passive(n))l.assets[n].attribute=0;
    if(s.profile->runtimeSeparatedCoat&&s.profile->ReleasedFixed(n))l.assets[n].attribute=2;
    if((r.sourcePanelFit&&eiem_cloth_asset::SourcePanelRelease(*s.profile,n)) || (r.sourceApronFit&&eiem_cloth_asset::SourceApronRelease(*s.profile,n)))l.assets[n].attribute=2;
    if(r.sourcePanelFit){l.assets[n].depth=eiem_cloth_asset::SourcePanelDepth(*s.profile,n);if(eiem_cloth_asset::SourceChenWaist(*s.profile,n))l.assets[n].attribute=1;}
    if(r.sourceCoatWaist)l.assets[n].attribute=s.profile->CandidateAttribute(n);
    if(r.sourceShortSkin){l.assets[n].depth=eiem_cloth_asset::SourceShortDepth(*s.profile,n);l.assets[n].attribute=l.assets[n].depth?2:1;}
    if(r.resampledPanel){l.assets[n].attribute=0;l.assets[n].column=l.assets[n].depth=-1;l.ignored.push_back(n);}}
  if(r.addedCount)l.assets.insert(l.assets.end(),r.added,r.added+r.addedCount);
  l.roots.assign(r.roots,r.roots+r.rootCount);
  l.layers.resize(r.meshCount);l.crossRest.assign(r.crossCount,0);
  l.profile=*s.profile;l.profile.signature=r.signature;l.profile.bones=l.assets.data();l.profile.loop=r.loop;
  l.profile.candidateAttributes=nullptr;
  l.profile.nativeGraphs=r.graphs;l.profile.nativeGraphCount=r.graphCount;
  l.profile.boneCount=int(l.assets.size());l.profile.roots=l.roots.data();l.profile.rootCount=int(l.roots.size());
  if(r.resampledPanel){l.profile.depth=ClothLongPanelRows;l.profile.candidateIgnored=l.ignored.data();l.profile.candidateIgnoredCount=int(l.ignored.size());}
  if(r.sourceShortSkin)l.profile.depth=3;
  return ClothBoneIdentityBudget(l.profile.boneCount,l.profile.EffectiveCount()) && l.profile.boneCount==r.Total() && l.profile.EffectiveCount()==(r.resampledPanel?0:r.sourceShortSkin?s.profile->boneCount:s.profile->EffectiveCount()+eiem_cloth_asset::SourcePanelPromotedCount(*s.profile))+r.addedCount;
}
static bool ClothBoneLocalCreate() {
  auto &s=ClothBoneState();auto &l=s.local;const auto &recipe=*l.recipe;
  if(l.created)return true;
  if(s.stopRequested || !ClothOwns(s.owner))return false;
  if(l.createStage==0) {
    if(!ClothBoneLocalProfile(s)) return false;
    for(size_t k=0;k<l.layers.size();++k) {
      const void *bytes=nullptr;DWORD size=0;
      if(!ClothBoneLocalResource(k,bytes,size)) return false;
  }
  Log("[CLOTH-BONE-RESOURCE] stage=all-verified generation=%llu recipe=%s layers=%zu originalSimulationRetained=1",
      (unsigned long long)s.owner.generation,recipe.signature,l.layers.size());
  for(size_t k=0;k<l.layers.size();++k) {
    auto &layer=l.layers[k];const auto &config=recipe.meshes[k];
    for(int n=0;n<s.profile->rendererCount;++n) if(!strcmp(s.profile->renderers[n].name,config.renderer)) {
      const auto parent=s.profile->renderers[n].parent;
      if(config.parent && (!parent || strcmp(config.parent,parent))) continue;
      if(layer.renderer>=0) return false;layer.renderer=n;
    }
    if(layer.renderer<0) return false;
  }
  if(!ClothBoneLocalVisible()) return false;
  const auto sourceRoots=recipe.runtimeGenerated&&!recipe.sourceApronFit?s.profile->originalRoots:s.profile->roots;
  auto parent=ClothTarget(s.bones[sourceRoots[0]].parent);
  if(!parent) return false;
  for(int n=0;n<s.profile->rootCount;++n)
    if(parent!=ClothTarget(s.bones[sourceRoots[n]].parent)) return false;
  int children=0;
  if(!ClothValue(s_clothUnity.childCount,parent,children) || children<0 || children>128) return false;
  for(int n=0;n<children;++n) {
    void *t=nullptr,*args[]{&n};char name[128]{};
    if(!ClothInvoke(s_clothUnity.child,parent,args,t) || !t) return false;CollisionName(t,name,sizeof(name));
    for(int k=0;k<recipe.addedCount;++k) if(!strcmp(name,recipe.added[k].name)) return false;
  }
  s.bones.resize(l.assets.size());
  ++l.createStage;return true;
  }
  if(l.createStage==1) {
    const auto sourceRoots=recipe.runtimeGenerated&&!recipe.sourceApronFit?s.profile->originalRoots:s.profile->roots;
    auto parent=ClothTarget(s.bones[sourceRoots[0]].parent);if(!parent)return false;
    const size_t end=(std::min)(size_t(recipe.addedCount),l.createBone+8);
    for(;l.createBone<end;++l.createBone) {
      const auto n=l.createBone;
      const auto &a=recipe.added[n];auto &b=s.bones[recipe.originalCount+n];
      void *go=il2cpp_object_new(g_gameObjectClass),*label=il2cpp_string_new(a.name),*unused=nullptr,*args[]{label};
      if(!go || !label || !ClothBoneHold(go) || !ClothBoneHold(label)) return false;
      const bool made=ClothInvoke(ClothMethod(g_gameObjectClass,".ctor","System.Void","System.String"),go,args,unused);
      auto ref=ClothProtect(go);
      if(!ref.handle) {
        void *destroyArgs[]{go};ClothInvoke(ClothMethod(SurfaceClass("UnityEngine","Object"),"Destroy","System.Void","UnityEngine.Object",true),nullptr,destroyArgs,unused);
        return false;
      }
      l.objects.push_back(ref);l.destroyIssued.push_back(false);
      auto t=CollisionTransform(go),p=a.parent<0?parent:ClothTarget(s.bones[a.parent].bone);
      if(!made || !t || !p || !SurfaceTRS(t,p,a.position,a.rotation,a.scale)) return false;
      b.bone=ClothProtect(t);b.parent=ClothProtect(p);b.local=a.position;b.rotation=a.rotation;
      b.referenceScale=s.bones[recipe.parents[n]].referenceScale;
      if(!b.bone.handle || !b.parent.handle) return false;
  }
  if(l.createBone==size_t(recipe.addedCount))++l.createStage;
  return true;
  }
  if(l.createStage==2 && l.createLayer<l.layers.size()) {
    auto &layer=l.layers[l.createLayer];const auto &config=recipe.meshes[l.createLayer];
    const auto &source=s.profile->renderers[layer.renderer];
    auto renderer=ClothTarget(s.renderers[layer.renderer].renderer);void *old=nullptr;
    if(!renderer)return false;
    if(!layer.oldArray) {
      const bool paired=s.contactPartner>=0&&s_clothBoneSlots[s.contactPartner].supportCreated;
      if(!ClothBoneLocalBindingMapValid(config,source,recipe.Total(),paired?ClothPartnerSurfaceOriginal:0,
          paired?int(std::size(ClothPartnerSurfaceBones)):0,recipe.CoatWaistSkinOnly()) ||
          !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(renderer),"get_bones","UnityEngine.Transform[]"),renderer,nullptr,old) || !old) return false;
      layer.oldArray=ClothBoneHold(old);
      void *array=recipe.CoatWaistSkinOnly()?old:il2cpp_array_new(g_transformClass,config.sourceBones+config.bindingCount);
      if(!layer.oldArray || !(layer.newArray=ClothBoneHold(array))) return false;
      layer.rendererBones=s.renderers[layer.renderer].bones;
      if(recipe.CoatWaistSkinOnly())layer.buildBinding=config.sourceBones;
    }
    auto array=CollisionGc(layer.newArray);if(!array)return false;
    auto set=SurfaceMethod(il2cpp_object_get_class(array),"SetValue","System.Void","System.Object","System.Int32");
    const size_t total=size_t(config.sourceBones+config.bindingCount),end=(std::min)(total,layer.buildBinding+8);
    for(;layer.buildBinding<end;++layer.buildBinding) {
      int n=int(layer.buildBinding);
      if(n>=config.sourceBones) {
        const auto ref=ClothBoneLocalBindingRef(s,config.bindingNativeIndices[n-config.sourceBones]);
        if(!ref.handle)return false;layer.rendererBones.push_back(ref);
      }
      void *unused=nullptr,*args[]{ClothTarget(layer.rendererBones[n]),&n};
      if(!args[0] || !ClothInvoke(set,array,args,unused)) return false;
    }
    if(layer.buildBinding==total)++l.createLayer;
    return true;
  }
  l.createStage=3;
  if(recipe.NativeSkinRetained())l.registryReady=true;
  else if(const char *issue=SurfaceBundleRegistryAcquire(SurfaceClass("UnityEngine","AssetBundle"),l.registryScan,l.registryCount)) {
    ClothBoneNote(issue);return false;
  }
  if(recipe.bodyCoverage && !ClothBoneBodyCreate()) return false;
  if(ClothBoneSideRecipeFor(*s.profile) && !ClothBoneSideCreate()) return ClothBoneReject("body-side-support-create-unconfirmed");
  l.created=true;l.deadline=GetTickCount64()+12000;
  if(recipe.resampledPanel)Log("[CLOTH-AUTO] stage=long-panel-prepared component=%s sourceIdentities=40 sourceParticleWrites=0 privateColumns=%d privateRows=%d privateFixed=%d privateMove=%d fittedBodyShapes=%d proximalSpheres=12 calfCapsules=2 originalCapsuleWrites=0 upperAttachmentRing=source-wrapper upperAnchorSkinVertices=%zu contactRadius=%g sourceAngleRetained=1 contourMaterialReadback=pending faceBendingReadback=pending nativeReadback=pending visualVerified=0",s.profile->component,ClothLongPanelColumns,ClothLongPanelRows,ClothLongPanelColumns,ClothLongPanelParticles-ClothLongPanelColumns,recipe.bodySphereCount,recipe.upperAnchorSkinVertices,recipe.fittedContactRadius);
  if(recipe.sourcePanelFit){int released=0,waist=0;for(int n=0;n<s.profile->boneCount;++n){released+=eiem_cloth_asset::SourcePanelRelease(*s.profile,n);waist+=l.assets[n].attribute==1&&l.assets[n].depth==0;}
    Log("[CLOTH-AUTO] stage=source-panel-prepared component=%s internalFixedToMove=%d sourceFixedWaistRoots=%d verifiedSourceWaists=%d commonLayerSkin=1 originalBodyCollidersRetained=1 selectionRestore=original-snapshot nativeReadback=pending visualVerified=0",s.profile->component,released,waist,eiem_cloth_asset::SourcePanelPromotedCount(*s.profile));}
  if(recipe.CoatWaistSkinOnly())Log("[CLOTH-AUTO] stage=coat-waist-prepared component=%s meshes=%d addedBones=0 fixedToSourceTrunkField=1 MoveWeightsUnchanged=1 sourceBodyCapsulesUnchanged=1 nativeReadback=pending visualVerified=0",s.profile->component,recipe.meshCount);
  if(recipe.NativeBodyOnly())Log("[CLOTH-AUTO] stage=body-contact-prepared component=%s points=%d sourceLines=%d addedThighCapsules=%d sourceReferenceRetained=1 originalSkinRetained=1 originalCapsuleWrites=0 nativeReadback=pending visualVerified=0",s.profile->component,l.profile.EffectiveCount(),recipe.graphs[0].lineCount,recipe.bodySphereCount);
  if(recipe.sourceShortSkin)Log("[CLOTH-AUTO] stage=short-skin-prepared component=%s waistFixed=%d visibleFixedToMove=4 leasedSideBones=%d nativeControls=%d closedSurface=%d originalSkinRetained=1 sharedRendererWrites=0 originalCapsuleWrites=0 nativeReadback=pending visualVerified=0",s.profile->component,recipe.sourceShortSides?6:4,s.profile->sourceBranchCount,recipe.Total(),recipe.sourceShortSides);
  if(recipe.sourceApronFit)Log("[CLOTH-AUTO] stage=source-apron-prepared originalNativePoints=4 verifiedWaistInputs=2 internalFixedToMove=2 addedBones=%d sourceFixedOnlySkinReleased=1 sideBBCsRetained=1 originalBodyColliders=1 nativeReadback=pending visualVerified=0",recipe.addedCount);
  int addedFixed=0;for(int n=0;n<recipe.addedCount;++n)addedFixed+=recipe.added[n].attribute==1;
  Log("[CLOTH-BONE-LOCAL] stage=prepared recipe=%s generation=%llu scope=%s originalBones=%d addedFixed=%d addedMove=%d firstMeshSkinVertices=%d topologyExpected=%d/%d renderPublished=0",
      recipe.signature,(unsigned long long)s.owner.generation,recipe.loop?"closed-ring":"open-folded-strip",recipe.originalCount,addedFixed,
      recipe.addedCount-addedFixed,recipe.meshCount?recipe.meshes[0].skinVertices:0,l.profile.EffectiveCount(),l.profile.FaceCount());
  if(recipe.NativePanelsOnly()&&s.profile->legRequiredMask)Log("[CLOTH-AUTO-LEGS] stage=prepared component=%s requiredMask=%u listedMask=%u nativeCollisionRequested=%s sourceCapsuleGeometryUnchanged=1 visualVerified=0",s.profile->component,s.profile->legRequiredMask,s.profile->legListedMask,ClothBoneBodyMode(s));
  if(recipe.NativePanelsOnly())Log("[CLOTH-AUTO] stage=separated-panels-prepared component=%s panels=%d originalBones=%d addedFixed=%d addedMove=%d points=%d nativeFaces=%d originalSkinRetained=1 rendererWrites=0 originalCapsuleWrites=0 nativeReadback=pending visualVerified=0",
      s.profile->component,recipe.separatedPanels,recipe.originalCount,addedFixed,recipe.addedCount-addedFixed,l.profile.EffectiveCount(),l.profile.FaceCount());
  if(recipe.NativeRibbonWidth())Log("[CLOTH-AUTO] stage=strip-width-prepared component=%s ribbons=2 columnsPerRibbon=5 points=%d nativeFaces=%d privateMeshes=%d bodyCapsuleWrites=0 fixedSkinRetained=1 shoulderSkinRetained=1 nativeReadback=pending visualVerified=0",
      s.profile->component,l.profile.EffectiveCount(),l.profile.FaceCount(),recipe.meshCount);
  return true;
}
static bool ClothBoneLocalFinishRequest(size_t layer) {
  auto &s=ClothBoneState();auto &l=s.local.layers[layer];const auto &config=s.local.recipe->meshes[layer];
  if(!l.issued || l.completed) return true;
  auto request=CollisionGc(l.request);bool done=false;void *bundle=nullptr;
  if(!request || !ClothValue(SurfaceMethod(il2cpp_object_get_class(request),"get_isDone","System.Boolean"),request,done) || !done) return false;
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(request),"get_assetBundle","UnityEngine.AssetBundle"),request,nullptr,bundle)) return false;
  if(!bundle) {l.completed=true;return true;}
  bool existed=true;char name[128]{};CollisionName(bundle,name,sizeof(name));
  if(!SurfaceBundleSnapshotHas(l.registry,l.registryCount,bundle,existed) || existed || strcmp(name,config.bundleName)) return false;
  if(!l.bundle.handle) l.bundle=ClothProtect(bundle);
  if(!l.bundle.handle || ClothTarget(l.bundle)!=bundle || std::find(l.previousIds.begin(),l.previousIds.end(),l.bundle.id.instance)!=l.previousIds.end()) return false;
  l.taken=l.completed=true;return true;
}
static bool ClothBoneLocalValidateMesh(size_t layer,void *mesh) {
  auto &s=ClothBoneState();auto &l=s.local.layers[layer];const auto &a=s.profile->renderers[l.renderer];
  auto source=ClothTarget(s.renderers[l.renderer].mesh);auto cls=SurfaceClass("UnityEngine","Mesh");
  const auto &config=s.local.recipe->meshes[layer];
  int count=0,submeshes=0;bool readable=false;std::vector<SurfaceRenderDescriptor> layout;
  if(!mesh || mesh==source || il2cpp_object_get_class(mesh)!=cls ||
      !ClothValue(SurfaceMethod(cls,"get_isReadable","System.Boolean"),mesh,readable) || !readable ||
      !ClothValue(SurfaceMethod(cls,"get_vertexCount","System.Int32"),mesh,count) ||
      !ClothValue(SurfaceMethod(cls,"get_subMeshCount","System.Int32"),mesh,submeshes) || !ClothBoneLocalMeshShape(config,a,count,submeshes) ||
      !SurfaceRenderDescriptors(source,layout) || !SurfaceSameDescriptors(mesh,layout)) return false;
  if(config.candidateVertices) {
    int expectedFormat=-1,format=-1,expectedTopology=-1,sourceCount=0;
    if(!ClothValue(SurfaceMethod(cls,"get_vertexCount","System.Int32"),source,sourceCount) || sourceCount!=config.sourceVertices ||
        !CollisionEnumValue(SurfaceClass("UnityEngine.Rendering","IndexFormat"),"UInt16",expectedFormat) ||
        !ClothValue(SurfaceMethod(cls,"get_indexFormat","UnityEngine.Rendering.IndexFormat"),mesh,format) || format!=expectedFormat ||
        !CollisionEnumValue(SurfaceClass("UnityEngine","MeshTopology"),"Triangles",expectedTopology)) return false;
    int sub=0;void *box=nullptr,*args[]{&sub};
    if(!ClothInvoke(SurfaceMethod(cls,"GetIndexCount","System.UInt32","System.Int32"),mesh,args,box) ||
        !box || !ClothTypeIs(il2cpp_class_get_type(il2cpp_object_get_class(box)),"System.UInt32")) return false;
    uint32_t indices=0;memcpy(&indices,(char*)box+16,4);
    if(indices!=uint32_t(config.candidateIndices) ||
        !ClothInvoke(SurfaceMethod(cls,"GetTopology","UnityEngine.MeshTopology","System.Int32"),mesh,args,box) ||
        !box || !ClothTypeIs(il2cpp_class_get_type(il2cpp_object_get_class(box)),"UnityEngine.MeshTopology")) return false;
    int topology=-1;memcpy(&topology,(char*)box+16,4);if(topology!=expectedTopology) return false;
    void *array=nullptr;
    if(!ClothInvoke(SurfaceMethod(cls,"GetIndices","System.Int32[]","System.Int32"),mesh,args,array) || !array) return false;
    const auto hold=il2cpp_gchandle_new(array,false);if(!hold) return false;
    const bool valid=SurfaceRenderArrayLength(array,"System.Int32[]",indices) &&
        ClothBoneLocalIndexData(config,reinterpret_cast<const int*>((char*)array+32),indices);
    il2cpp_gchandle_free(hold);if(!valid) return false;
  }
  std::vector<unsigned char> binds;
  if(!ClothBoneLocalBindingMapFor(s,config,a) ||
      !SurfaceRenderArray(mesh,"get_bindposes","UnityEngine.Matrix4x4[]","UnityEngine","Matrix4x4",64,a.boneCount+config.bindingCount,binds) || binds.size()!=size_t(a.boneCount+config.bindingCount)*64) return false;
  for(int n=0;n<a.boneCount+config.bindingCount;++n) {
    float actual[16]{};memcpy(actual,binds.data()+n*64,64);
    const float *expected=n<a.boneCount?a.bones[n].bind:config.bindings[n-a.boneCount];
    for(int k=0;k<16;++k) if(!std::isfinite(actual[k]) || fabsf(actual[k]-expected[k])>.000001f) return false;
  }
  if(s.local.recipe->runtimeGenerated) {
    auto weightClass=SurfaceClass("UnityEngine","BoneWeight");const char *fields[]{"m_Weight0","m_Weight1","m_Weight2","m_Weight3","m_BoneIndex0","m_BoneIndex1","m_BoneIndex2","m_BoneIndex3"};
    if(config.generatedVertexCount!=size_t(count)||(!config.generatedWeights==!config.generatedFloatWeights)||!config.generatedIndices)return false;
    for(int k=0;k<8;++k)if(!weightClass||ClothValueOffset(weightClass,fields[k],k<4?"System.Single":"System.Int32",32,4)!=k*4)return false;
    std::vector<unsigned char> skin;if(!SurfaceRenderArray(mesh,"get_boneWeights","UnityEngine.BoneWeight[]","UnityEngine","BoneWeight",32,count,skin))return false;
    for(int n=0;n<count;++n){float weights[4]{};int indices[4]{};memcpy(weights,skin.data()+size_t(n)*32,16);memcpy(indices,skin.data()+size_t(n)*32+16,16);
      const bool matches=config.generatedWeights?ClothGeneratedSkinMatches(weights,indices,config.generatedWeights[n],config.generatedIndices[n]):
          ClothGeneratedSkinMatches(weights,indices,config.generatedFloatWeights[n],config.generatedIndices[n]);if(!matches)return ClothBoneReject("runtime-private-skin-native-readback-mismatch");}
    Log("[CLOTH-AUTO] stage=private-skin-readback renderer=%s vertices=%d layout=%s bodyForeignTermsPreserved=%d fixedSkinTermsPreserved=%d waistSourceTrunkField=%d fixedWaistParticlesPreserved=1 sourceFittedSkin=%d nativeWeightsConfirmed=1 partialSourceRegions=%d visualVerified=0",config.renderer,count,config.generatedWeights?"UNorm16/UInt8":"Float32/UInt32",!s.local.recipe->sourceCoatWaist,!s.local.recipe->FittedSkin()&&!s.local.recipe->sourceCoatWaist,s.local.recipe->sourceCoatWaist,s.local.recipe->FittedSkin(),s.local.recipe->partialSurface);
  }
  return true;
}
static bool ClothBoneLocalMeshReady(size_t layer) {
  auto &s=ClothBoneState();auto &l=s.local.layers[layer];const auto &config=s.local.recipe->meshes[layer];
  if(l.ready) return true;
  if(!l.issued) {
    if(!s.local.registryReady || l.namePresent)return false;
    l.previousIds=s.local.registryIds;
    const void *bytes=nullptr;DWORD size=0;
    if(!ClothBoneLocalResource(layer,bytes,size)) return false;
    if(!l.file.Create(bytes,size,eiem_cloth_resource::Limit(s.local.recipe->runtimeGenerated))) return ClothBoneReject("local-resource-temporary-file-failed");
    char path[MAX_PATH*4]{};
    if(!WideCharToMultiByte(CP_UTF8,WC_ERR_INVALID_CHARS,l.file.path,-1,path,int(sizeof(path)),nullptr,nullptr)) return false;
    void *label=il2cpp_string_new(path);if(!(l.pathString=ClothBoneHold(label))) return false;
    auto cls=SurfaceClass("UnityEngine","AssetBundle");
    auto load=ClothMethod(cls,"LoadFromFileAsync","UnityEngine.AssetBundleCreateRequest","System.String",true);
    if(!load || !SurfaceMethod(cls,"Unload","System.Void","System.Boolean") ||
        SurfaceBundleRegistryAcquire(cls,l.registry,l.registryCount)) return false;
    void *request=nullptr,*args[]{label};l.issued=true;
    if(!ClothInvoke(load,nullptr,args,request) || !request || !(l.request=ClothBoneHold(request))) return false;
    return true;
  }
  if(!ClothBoneLocalFinishRequest(layer)) return true;
  if(!l.completed || !l.taken) return false;
  auto bundle=ClothTarget(l.bundle);auto cls=SurfaceClass("UnityEngine","Mesh");
  void *name=il2cpp_string_new(config.asset),*type=cls?il2cpp_type_get_object(il2cpp_class_get_type(cls)):nullptr;
  void *mesh=nullptr,*args[]{name,type};
  if(!bundle || !name || !type || !ClothBoneHold(name) || !ClothBoneHold(type) ||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bundle),"LoadAsset","UnityEngine.Object","System.String","System.Type"),bundle,args,mesh) || !mesh) return false;
  l.mesh=ClothProtect(mesh);
  if(!l.mesh.handle || !ClothBoneLocalValidateMesh(layer,mesh)) return false;
  l.ready=true;
  Log("[CLOTH-BONE-LOCAL] stage=private-mesh-ready layer=%zu renderer=%s mesh=%d sourceBones=%d candidateBones=%d originalRendererWrites=0 packedLayoutReadback=1 bindReadback=1 refinedVertices=%d refinedIndices=%d indexReadback=%d",
      layer,config.renderer,l.mesh.id.instance,config.sourceBones,config.sourceBones+config.bindingCount,config.candidateVertices,config.candidateIndices,int(config.candidateVertices!=0));
  return true;
}
static bool ClothBoneLocalReady() {
  auto &s=ClothBoneState();auto &l=s.local;
  if(l.ready) return true;
  if(!l.created || !ClothOwns(s.owner) || s.stopRequested || GetTickCount64()>=l.deadline || !ClothBoneLocalVisible()) return false;
  if(!l.registryReady) {
    if(const char *issue=SurfaceBundleRegistryAdvanceNames(l.registryScan,l.registryCount,l.registryCursor,l.registryIds,[&](const char *name){
        for(size_t k=0;k<l.layers.size();++k)l.layers[k].namePresent|=!strcmp(name,l.recipe->meshes[k].bundleName);
      },SurfaceBundleRegistryLimit,3)) {ClothBoneNote(issue);return false;}
    if(l.registryCursor<l.registryCount)return true;
    il2cpp_gchandle_free(l.registryScan);l.registryScan=0;l.registryReady=true;
    for(const auto &layer:l.layers)if(layer.namePresent)return false;
    Log("[CLOTH-BONE-RESOURCE] stage=registry-ready sharedLayers=%zu inspected=%zu perFrameSoftBudgetMs=3",l.layers.size(),l.registryCount);
  }
  LARGE_INTEGER start{},clock{},frequency{};QueryPerformanceFrequency(&frequency);QueryPerformanceCounter(&start);
  for(size_t k=0;k<l.layers.size();++k) {
    const auto at=l.meshCursor++%l.layers.size();
    if(l.layers[at].ready)continue;
    if(!ClothBoneLocalMeshReady(at))return false;
    if(s.stopRequested || !ClothOwns(s.owner))return false;
    QueryPerformanceCounter(&clock);
    if(!frequency.QuadPart || (clock.QuadPart-start.QuadPart)*1000>=frequency.QuadPart*4)break;
  }
  l.ready=std::all_of(l.layers.begin(),l.layers.end(),[](const auto &layer){return layer.ready;});
  return true;
}
static bool ClothBoneLocalRendererIdentity(size_t slot,void *renderer,void *mesh) {
  auto &s=ClothBoneState();const ClothBoneLocalMeshState *layer=nullptr;
  for(auto &candidate:s.local.layers) if(candidate.renderer==int(slot)) {if(layer) return false;layer=&candidate;}
  if(!layer) return false;auto &l=*layer;
  if(!l.published || mesh!=ClothTarget(l.mesh) ||
      !SurfaceRenderSameReferences(renderer,"get_bones","UnityEngine.Transform[]",l.rendererBones)) return false;
  const auto &r=s.renderers[slot];
  for(size_t n=0;n<r.bones.size();++n) if(CollisionParent(ClothTarget(r.bones[n]))!=ClothTarget(r.parents[n])) return false;
  const ClothBoneLocalMeshConfig *config=nullptr;
  for(size_t k=0;k<s.local.layers.size();++k)if(&s.local.layers[k]==layer)config=&s.local.recipe->meshes[k];
  if(!config||!ClothBoneLocalBindingMapFor(s,*config,s.profile->renderers[slot]))return false;
  for(int n=0;n<config->bindingCount;++n)
    if(ClothTarget(l.rendererBones[config->sourceBones+n])!=ClothTarget(ClothBoneLocalBindingRef(s,config->bindingNativeIndices[n])))return false;
  return true;
}
static bool ClothBoneLocalMeshRestore(ClothBoneLocalMeshState &l) {
  auto &s=ClothBoneState();
  if(!l.publishAttempted) return true;
  if(l.renderer<0 || size_t(l.renderer)>=s.renderers.size()) return false;
  auto &r=s.renderers[l.renderer];void *renderer=ClothTarget(r.renderer),*mesh=nullptr,*array=nullptr;
  if(!renderer) {
    void *unused=nullptr;if(ClothInspect(r.renderer,unused)!=ClothLife::Destroyed) return false;
    l.published=l.publishAttempted=false;return true;
  }
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(renderer),"get_sharedMesh","UnityEngine.Mesh"),renderer,nullptr,mesh)) return false;
  if(mesh==ClothTarget(l.mesh) && (!ClothTarget(r.mesh) || !SurfaceCall(renderer,"set_sharedMesh","UnityEngine.Mesh",ClothTarget(r.mesh)))) return false;
  if(!s.local.recipe->CoatWaistSkinOnly() && SurfaceRenderSameReferences(renderer,"get_bones","UnityEngine.Transform[]",l.rendererBones) &&
      !SurfaceCall(renderer,"set_bones","UnityEngine.Transform[]",CollisionGc(l.oldArray))) return false;
  if(!ClothInvoke(SurfaceMethod(il2cpp_object_get_class(renderer),"get_sharedMesh","UnityEngine.Mesh"),renderer,nullptr,mesh) || mesh==ClothTarget(l.mesh) ||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(renderer),"get_bones","UnityEngine.Transform[]"),renderer,nullptr,array)) return false;
  uintptr_t count=0;
  if(!ClothArray(array,"UnityEngine.Transform[]",count) || count>256) return false;
  for(size_t n=0;n<count;++n) for(size_t k=s.local.recipe->originalCount;k<s.bones.size();++k)
    if(reinterpret_cast<void**>((char*)array+32)[n]==ClothTarget(s.bones[k].bone)) return false;
  for(size_t n=0;n<count;++n)for(size_t k=r.bones.size();k<l.rendererBones.size();++k)
    if(reinterpret_cast<void**>((char*)array+32)[n]==ClothTarget(l.rendererBones[k]))return false;
  l.published=l.publishAttempted=false;
  Log("[CLOTH-BONE-LOCAL] stage=renderer-restored-before-native-retirement renderer=%d foreignReferencesPreserved=1",r.renderer.id.instance);return true;
}
static bool ClothBoneLocalRestore() {
  auto &l=ClothBoneState().local;bool restored=true;
  if(!ClothBoneResponseRestore())return false;
  for(auto &layer:l.layers) if(!ClothBoneLocalMeshRestore(layer)) restored=false;
  l.published=false;l.publishAttempted=!restored;return restored;
}
static bool ClothBoneLocalCommitMeshes() {
  auto &s=ClothBoneState();auto &l=s.local;
  if(s.stopRequested || !ClothOwns(s.owner)) return false;
  if(!l.contactColliderOmissions.empty() && !l.contactConfirmed) return false;
  if(l.apronLayer.configured && !l.apronLayer.confirmed)return false;
  if(l.contactLayered && l.contactStart.phase!=ClothBoneContactStart::Phase::Ready)return false;
  for(auto &layer:l.layers) if(!layer.ready || layer.renderer<0 || size_t(layer.renderer)>=s.renderers.size()) return false;
  l.publishAttempted=true;
  for(auto &layer:l.layers) {
    auto renderer=ClothTarget(s.renderers[layer.renderer].renderer);
    layer.publishAttempted=true;
    const bool sent=(l.recipe->CoatWaistSkinOnly() || SurfaceCall(renderer,"set_bones","UnityEngine.Transform[]",CollisionGc(layer.newArray))) &&
      ClothOwns(s.owner) && !s.stopRequested &&
      SurfaceCall(renderer,"set_sharedMesh","UnityEngine.Mesh",ClothTarget(layer.mesh));
    layer.published=sent;
    if(!sent || !ClothOwns(s.owner) || s.stopRequested) {ClothBoneLocalRestore();return false;}
  }
  return true;
}
static bool ClothBoneLocalPublish() {
  auto &s=ClothBoneState();auto &l=s.local;
  if(!l.requested) return true;
  if(!l.ready || s.stopRequested || !ClothOwns(s.owner)) return false;
  if(l.published) return ClothBoneBindingIdentity();
  if(l.apronLayer.configured && !ClothBoneApronLayerReadback(s,nullptr))return false;
  if(!ClothBoneLocalVisible()) return false;
  if(l.contactLayered && !ClothBoneBindingIdentity())return false;
  if(!ClothBoneContactStartRequest(s))return false;
  if(!s.teamModeConfirmed || !l.solverConfirmed) return GetTickCount64()<s.modeDeadline;
  if(!l.contactColliderOmissions.empty() && !l.contactConfirmed) return GetTickCount64()<s.modeDeadline;
  if(l.apronLayer.configured && !l.apronLayer.confirmed)return GetTickCount64()<s.modeDeadline;
  if(l.contactLayered && l.contactStart.phase!=ClothBoneContactStart::Phase::Ready)return GetTickCount64()<s.modeDeadline;
  if(!ClothBoneBindingIdentity() || !ClothBoneLocalCommitMeshes()) return false;
  if(!ClothBoneBindingIdentity()) {ClothBoneLocalRestore();return false;}
  l.published=true;
  if(l.recipe->CoatWaistSkinOnly())Log("[CLOTH-AUTO] stage=coat-waist-active component=%s Process=%p team=%d meshes=%d nativeOutputFrames=%u originalBBC=1 same24Particles=1 sourceTrunkField=1 MoveWeightsUnchanged=1 sourceBodyCapsulesUnchanged=1 visualVerified=0",s.profile->component,CollisionGc(s.process[1]),s.team[1],l.recipe->meshCount,l.solverFrames);
  if(l.recipe->NativeBodyOnly())Log("[CLOTH-AUTO] stage=body-contact-active component=%s Process=%p team=%d points=%d sourceLines=%d addedThighCapsules=%d nativeOutputFrames=%u originalBBC=1 originalSkinRetained=1 sourceReferenceRetained=1 visualVerified=0",s.profile->component,CollisionGc(s.process[1]),s.team[1],l.profile.EffectiveCount(),l.recipe->graphs[0].lineCount,l.recipe->bodySphereCount,l.solverFrames);
  if(l.recipe->sourceShortSkin)Log("[CLOTH-AUTO] stage=short-skin-active component=%s team=%d points=%d faces=%d outputFrames=%u visibleFixedToMove=4 leasedSideBones=%d closedSurface=%d sharedRendererWrites=0 visualVerified=0",s.profile->component,s.team[1],l.profile.EffectiveCount(),l.profile.FaceCount(),l.solverFrames,s.profile->sourceBranchCount,l.recipe->sourceShortSides);
  if(l.recipe->NativePanelsOnly())Log("[CLOTH-AUTO] stage=separated-panels-active component=%s Process=%p team=%d panels=%d points=%d nativeFaces=%d nativeOutputFrames=%u originalSkinRetained=1 rendererWrites=0 visualVerified=0",
      s.profile->component,CollisionGc(s.process[1]),s.team[1],l.recipe->separatedPanels,l.profile.EffectiveCount(),l.profile.FaceCount(),l.solverFrames);
  if(l.recipe->NativeRibbonWidth())Log("[CLOTH-AUTO] stage=strip-width-active component=%s Process=%p team=%d ribbons=2 points=%d nativeFaces=%d privateMeshes=%d nativeOutputFrames=%u bodyCapsuleWrites=0 fixedSkinRetained=1 shoulderSkinRetained=1 visualVerified=0",
      s.profile->component,CollisionGc(s.process[1]),s.team[1],l.profile.EffectiveCount(),l.profile.FaceCount(),l.recipe->meshCount,l.solverFrames);
  for(size_t k=0;k<l.layers.size();++k) {
    auto &layer=l.layers[k];const auto &config=l.recipe->meshes[k];
    Log("[CLOTH-BONE-LOCAL] stage=%s recipe=%s Process=%p team=%d renderer=%d mesh=%d verticesRebound=%d nativeOutputFrames=%u layer=%zu layers=%zu sharedNativeBones=%d visualVerified=0",
        k?"layer-published":"published",config.signature,CollisionGc(s.process[1]),s.team[1],s.renderers[layer.renderer].renderer.id.instance,layer.mesh.id.instance,
        config.skinVertices,l.solverFrames,k,l.layers.size(),l.recipe->Total());
    Log("[CLOTH-BONE-LOCAL] stage=joint-chart-binding renderer=%s appendedBindings=%d createdPhysicsBones=%d borrowedOriginalTips=%d fixedContributionChart=%d bodyColliderInflation=0 nativeSolver=originalBBC addedBodyCoverage=%d surfaceCorrespondence=%d addedRenderVertices=%d candidateVertices=%d",
        config.renderer,config.bindingCount,l.recipe->addedCount,config.bindingCount-l.recipe->addedCount,int(!l.recipe->sourceCoatWaist && !l.recipe->rootSkinTransition && !l.recipe->FittedSkin() && ClothLocalFixedContributionChart),int(l.bodyCreated||l.fittedBodyCreated),int(l.recipe==&ClothRingRecipe && k==1 && ClothInnerSurfaceCorrespondence),config.candidateVertices-config.sourceVertices,config.candidateVertices);
  }
  if(l.recipe->rootSkinTransition) Log("[CLOTH-BONE-LOCAL] stage=open-strip-published native=%d authoredFixedPreserved=1 rootSkinTransition=1 protectedIntegerTermsPreserved=1 meshes=%zu contactVerified=0",l.recipe->Total(),l.layers.size());
  if(s.contactPartner>=0&&s.contactPartner<s_clothBoneCount&&s_clothBoneSlots[s.contactPartner].supportCreated)
    Log("[CLOTH-BONE-SURFACE] stage=merged-skin-published recipe=%s partnerTeam=%d supportPoints=%zu singleRendererPublisher=1 originalFixedTermsPreserved=1 innerSkinPreserved=1 naturalShapePreserved=1 visualVerified=0",
        ClothPartnerSurfaceSignature,s_clothBoneSlots[s.contactPartner].team[1],std::size(ClothPartnerSurfaceBones));
  ClothBoneNote(l.recipe->NativeBodyOnly()?"active-original-BBC-Line-Point-fitted-thigh-contact":l.recipe->CoatWaistSkinOnly()?"active-original-BBC-coat-waist-skin":l.recipe->sourceShortSkin?"active-original-BBC-short-visible-skin":l.recipe->NativePanelsOnly()?"active-separated-native-panels-original-skin":l.recipe->NativeRibbonWidth()?"active-original-BBC-isolated-ribbon-width-skin":l.recipe->sourceApronFit?"active-content-fitted-fixed-apron-original-BBC":l.recipe->sourcePanelFit?"active-content-fitted-waist-panels-original-BBC":l.recipe->runtimeGenerated?"active-runtime-generated-simple-BoneCloth-surface":l.recipe->rootSkinTransition?"active-original-BBC-open-strip-skin-candidate":"active-original-BBC-joint-chart-two-layer-skin-candidate");return true;
}
static bool ClothBoneLocalCleanupWait(const char *reason,int item) {
  auto &s=ClothBoneState();auto &l=s.local;const auto now=GetTickCount64();
  if(s.frame>=0 && l.cleanupFrame==s.frame)return false;
  l.cleanupFrame=s.frame;
  char detail[128]{};snprintf(detail,sizeof(detail),"%s item=%d",reason,item);
  if(strcmp(l.cleanupIssue,detail)||now>=l.cleanupNextNote) {
    strncpy_s(l.cleanupIssue,detail,_TRUNCATE);l.cleanupNextNote=now+5000;
    Log("[CLOTH-BONE-CLEANUP] session=%llu generation=%llu component=%s phase=%d nativeLease=%d resourceLease=%d reason=%s",
        s.owner.session,s.owner.generation,s.profile?s.profile->component:"pending",int(s.tx.phase),int(s.tx.lease),int(s.lease),detail);
  }
  return false;
}
static bool ClothBoneLocalMeshCleanup(size_t layer) {
  auto &l=ClothBoneState().local.layers[layer];
  if(!ClothBoneLocalFinishRequest(layer)) return ClothBoneLocalCleanupWait("private-load-or-ownership-pending",int(layer));
  if(l.scan) {il2cpp_gchandle_free(l.scan);l.scan=0;}
  if(l.bundle.handle) {
    if(!l.taken) return ClothBoneLocalCleanupWait("private-bundle-ownership-unconfirmed",int(layer));
    if(!l.unloadIssued) {
      bool all=true;l.unloadIssued=true;
      if(!SurfaceCall(ClothTarget(l.bundle),"Unload","System.Boolean",&all)) return ClothBoneLocalCleanupWait("private-bundle-unload-command-unconfirmed",int(layer));
    }
    bool present=true;void *unused=nullptr;
    if(const char *reason=SurfaceBundleRegistryContains(SurfaceClass("UnityEngine","AssetBundle"),CollisionGc(l.bundle.handle),present)) return ClothBoneLocalCleanupWait(reason,int(layer));
    if(present) return ClothBoneLocalCleanupWait("private-bundle-still-registered",int(layer));
    if(l.mesh.handle && ClothInspect(l.mesh,unused)!=ClothLife::Destroyed) return ClothBoneLocalCleanupWait("private-mesh-unload-pending",int(layer));
  }
  if(!l.file.Remove()) return ClothBoneLocalCleanupWait("private-file-removal-pending",int(layer));
  ClothFree(l.mesh);ClothFree(l.bundle);
  if(l.registry) il2cpp_gchandle_free(l.registry);l.registry=0;
  l.issued=false;l.rendererBones.clear();return true;
}
static bool ClothBoneLocalCleanup() {
  auto &s=ClothBoneState();auto &l=s.local;
  if(!ClothBoneLocalRestore()) return ClothBoneLocalCleanupWait("renderer-still-references-owned-bones");
  if(l.registryScan){il2cpp_gchandle_free(l.registryScan);l.registryScan=0;}
  const bool sidesReleased=ClothBoneSideReleaseReady();
  bool meshesReleased=true;
  for(size_t k=0;k<l.layers.size();++k) if(!ClothBoneLocalMeshCleanup(k)) meshesReleased=false;
  if(!meshesReleased || !sidesReleased) return false;
  if(!ClothBoneBodyReleaseReady()) return ClothBoneLocalCleanupWait("body-collider-registration-or-foreign-reference-pending");
  if(!ClothBoneNativeContactReleaseReady()) return ClothBoneLocalCleanupWait("partner-contact-registration-pending");
  bool destroyed=true;
  for(size_t n=0;n<l.objects.size();++n) {
    void *go=nullptr;auto life=ClothInspect(l.objects[n],go);
    if(life==ClothLife::Destroyed) continue;
    destroyed=false;
    if(!l.destroyIssued[n] && go) {
      void *unused=nullptr,*args[]{go};l.destroyIssued[n]=true;
      ClothInvoke(ClothMethod(SurfaceClass("UnityEngine","Object"),"Destroy","System.Void","UnityEngine.Object",true),nullptr,args,unused);
    }
  }
  if(!destroyed) return ClothBoneLocalCleanupWait("owned-object-destruction-pending");
  for(auto &o:l.objects) ClothFree(o);l.objects.clear();l.destroyIssued.clear();
  ClothBoneBodyFree();
  ClothBoneSideFree();
  l.created=l.cleanup=false;
  Log("[CLOTH-BONE-LOCAL] stage=owned-resources-released layers=%zu originalAssetsModified=0",l.layers.size());return true;
}
