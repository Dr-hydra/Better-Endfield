#pragma once
namespace eiem_cloth_asset {
// Authored Li Zhiyan coat layout: chest on Spine1 and transverse waist on
// Spine. Scale the two regions independently; all coordinates are bone-local.
inline std::array<ClothBoneBodySphere,2> CoatTorsoCapsules(double chestLength,double hipWidth,int chest,int waist) {
  const double upper=chestLength/.080229469,lower=hipWidth/.151599968;
  Need(chest>=0&&waist>=0&&chest!=waist&&std::isfinite(upper)&&std::isfinite(lower)&&
      upper>=.75&&upper<=1.4&&lower>=.75&&lower<=1.4,"coat-torso-reference-proportion");
  const float chestRadius=float(.100*upper),waistRadius=float(.095*lower);
  return {{{chest,{0,float(.020*upper),0},chestRadius,chestRadius,float(.330*upper),{0,0,0,1}},
      {waist,{0,0,float(.036*lower)},waistRadius,waistRadius,float(.262*lower),{0,-.7071067812f,0,.7071067812f}}}};
}
inline ClothBoneBodySphere CoatHipBridgeCapsule(double hipWidth,int pelvis) {
  const double scale=hipWidth/.151599968;
  Need(pelvis>=0&&std::isfinite(scale)&&scale>=.75&&scale<=1.4,"coat-hip-bridge-proportion");
  const float radius=float(.095*scale),half=float(hipWidth/3.);
  return {pelvis,{0,0,half},radius,radius,2*(radius+half),{0,-.7071067812f,0,.7071067812f}};
}
// A lower bound on overlap for ANY rotation of the child about its joint.
// The parent capsule has equal radii. Each endpoint sphere in the child stays
// at a fixed distance from the joint, even while the waist bends or leg lifts.
inline double CoatJointOverlap(const eiem_collision::Capsule &parent,Point joint,const eiem_collision::Capsule &child) {
  using namespace eiem_collision;
  Need(parent.valid&&child.valid&&Finite(parent.a)&&Finite(parent.b)&&Finite(child.a)&&Finite(child.b)&&
      std::isfinite(parent.ra)&&parent.ra>0&&parent.ra==parent.rb&&
      std::isfinite(child.ra)&&std::isfinite(child.rb)&&child.ra>0&&child.rb>0,"coat-joint-overlap-shape");
  const V pivot{joint[0],joint[1],joint[2]};Need(Finite(pivot),"coat-joint-overlap-pivot");
  return parent.ra-SegmentDistance(pivot,parent.a,parent.b)+
      (std::max)(child.ra-Length(child.a-pivot),child.rb-Length(child.b-pivot));
}
inline void ConfigureCoatHipBridge(Scene &scene,const Query &query,const ClothBoneProfile &p,
    DenseRecipe &out,const ClothBoneBodySphere &waist,double hipWidth) {
  Need(SourceUpperCoatFront(p)&&out.view.CoatCalfCoverage()&&!out.view.sourceCoatTorso,"coat-hip-bridge-source");
  const auto left=FindBodyKey(scene,query.leftThigh),right=FindBodyKey(scene,query.rightThigh),pelvis=scene.Parent(left);
  Need(scene.Name(pelvis)=="Bip001_Pelvis"&&scene.Parent(right)==pelvis&&
      scene.Name(scene.Parent(pelvis))=="Bip001"&&scene.Parent(FindBodyKey(scene,query.spine))==pelvis,"coat-hip-bridge-hierarchy");
  const auto world=scene.World(pelvis),inverse=Inverse(world);QuaternionOf(world);
  const auto l=Transform(inverse,Transform(scene.World(left),{})),r=Transform(inverse,Transform(scene.World(right),{}));
  Need(std::hypot(l[0],l[1])<.001&&std::hypot(r[0],r[1])<.001&&
      std::abs(l[2]+r[2])<.001&&std::abs(std::abs(l[2]-r[2])-hipWidth)<.001,"coat-hip-bridge-axis");
  int index=-1;for(size_t n=0;n<out.bodyBindings.size();++n)if(!strcmp(out.bodyBindings[n].name,"Bip001_Pelvis")){
    Need(index<0,"coat-hip-bridge-binding-ambiguous");index=int(n);}
  const auto bridge=CoatHipBridgeCapsule(hipWidth,index);
  const auto capsule=[](const Matrix &parent,const ClothBoneBodySphere &shape){
    ClothBoneAsset bone{};bone.position=shape.center;bone.rotation=shape.rotation;bone.scale={1,1,1};
    return SourceWorldCapsule(Mul(parent,BoneMatrix(bone)),{},{shape.radius,shape.endRadius,shape.length},0,false,true,false);
  };
  const auto bridgeWorld=capsule(world,bridge);const double minimum=.012*hipWidth/.151599968;
  const auto spine=FindBodyKey(scene,query.spine);
  const double waistOverlap=CoatJointOverlap(bridgeWorld,Transform(scene.World(spine),{}),capsule(scene.World(spine),waist));
  Need(waistOverlap>=minimum,"coat-hip-waist-overlap-insufficient");
  int64_t component=0;for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==114&&scene.Name(o.first)==p.component){
    Need(!component,"coat-hip-component-ambiguous");component=o.first;}
  Need(component!=0,"coat-hip-component-missing");
  double overlaps[2]{};int matches[2]{};
  for(auto ref:Refs(scene.file.Get(component).At("serializeData").At("colliderCollisionConstraint").At("colliderList"))){
    const auto t=scene.TransformId(ref);if(t!=left&&t!=right)continue;const int side=t==left?0:1;
    const auto &c=scene.file.Get(ref);Need(++matches[side]==1&&c.At("m_Enabled").Int()!=0,"coat-hip-thigh-collider-ambiguous-or-disabled");
    const auto shape=SourceWorldCapsule(scene.World(t),Vec(c.At("center")),Vec(c.At("size")),int(c.At("direction").Int()),
        c.At("reverseDirection").Int()!=0,c.At("radiusSeparation").Int()!=0,c.At("alignedOnCenter").Int()!=0);
    overlaps[side]=CoatJointOverlap(bridgeWorld,Transform(scene.World(t),{}),shape);
    Need(overlaps[side]>=minimum,"coat-hip-thigh-overlap-insufficient");
  }
  Need(matches[0]==1&&matches[1]==1,"coat-hip-thigh-collider-missing");
  out.bodySpheres.push_back(bridge);
  out.densityReport+=" addedHipBridgeCapsules=1 jointRotationOverlapLowerBound="+std::to_string(waistOverlap)+","+
      std::to_string(overlaps[0])+","+std::to_string(overlaps[1])+" overlapOrder=waist,left-thigh,right-thigh";
}
inline void ConfigureCoatTorso(Scene &scene,const Query &query,const ClothBoneProfile &p,DenseRecipe &out) {
  auto &r=out.view;
  Need((SourceOriginalCoverageCoat(p.prefabSha,p.component)&&r.CoatCalfCoverage())||
      (SourceForkCoatFront(p)&&!r.bodyCoverage&&!r.bodySphereCount),"coat-torso-source-policy");
  const auto spine=FindBodyKey(scene,query.spine),chest=FindBodyKey(scene,query.chest),upper=FindBodyKey(scene,query.upperChest);
  Need(scene.Name(spine)=="Bip001_Spine"&&scene.Name(chest)=="Bip001_Spine1"&&scene.Name(upper)=="Bip001_Spine2"&&
      scene.Parent(chest)==spine&&scene.Parent(upper)==chest,"coat-torso-source-hierarchy");
  const auto a=scene.World(chest),b=scene.World(upper);QuaternionOf(a);QuaternionOf(scene.World(spine));
  const auto end=Transform(Inverse(a),Transform(b,{}));
  Need(end[0]<0&&std::hypot(end[1],end[2])<.002,"coat-torso-source-axis");
  int chestIndex=-1,waistIndex=-1;
  for(size_t n=0;n<out.bodyBindings.size();++n){const auto &binding=out.bodyBindings[n];
    if(!strcmp(binding.name,"Bip001_Spine1")){Need(chestIndex<0,"coat-torso-duplicate-chest");chestIndex=int(n);}
    if(!strcmp(binding.name,"Bip001_Spine")){Need(waistIndex<0,"coat-torso-duplicate-waist");waistIndex=int(n);}}
  const auto hipWidth=Distance(Transform(scene.World(FindBodyKey(scene,query.leftThigh)),{}),Transform(scene.World(FindBodyKey(scene,query.rightThigh)),{}));
  const auto shapes=CoatTorsoCapsules(-end[0],hipWidth,chestIndex,waistIndex);
  if(SourceOriginalCoverageCoat(p.prefabSha,p.component))ConfigureCoatHipBridge(scene,query,p,out,shapes[1],hipWidth);
  out.bodySpheres.insert(out.bodySpheres.end(),shapes.begin(),shapes.end());r.bodyCoverage=r.sourceCoatTorso=true;
  const auto key=std::string(r.signature)+"\nsource-coat-chest-transverse-waist-pelvis-bridge-v2";
  r.signature=out.String(Digest(Bytes(key.begin(),key.end())));out.Link();
  Need(r.CoatTorsoCoverage(),"coat-torso-generated-contract");
  out.densityReport+=" addedChestCapsules=1 addedWaistCapsules=1 torsoLayout=authored-Spine1-Spine independentlyScaled=1 originalCollidersRetained=1 originalSkinRetained=1 contactAndVisual=pending";
}
inline std::shared_ptr<DenseRecipe> GenerateForkCoatBody(Package &package,Scene &scene,int64_t component,const Query &query,const eiem_cloth_cache::Profile &base) {
  const auto &p=base.view;Need(SourceForkCoatFront(p)&&!p.generatedLocal,"fork-coat-body-source-changed");
  const auto &sourceData=scene.file.Get(component).At("serializeData");
  const auto branch=EffectiveBranch(scene,sourceData);
  auto proposed=SourcePrebuildEnabled(scene.file.Get(component).At("serializeData2"))?
      SourcePrebuildAttributes(scene.file.Get(component).At("serializeData2")):
      SourceSelection(scene,component,scene.file.Get(component).At("serializeData2"),branch.ids);
  const auto originalPeers=SharedSelection(scene,component,branch.ids,proposed);
  Need(branch.ids.size()==size_t(p.boneCount),"fork-coat-release-source-order");
  for(int n=0;n<p.boneCount;++n){Need(scene.Name(branch.ids[n])==p.bones[n].name,"fork-coat-release-source-order");
    if(SourceForkCoatRelease(p,n))proposed[branch.ids[n]]=2;}
  const auto peers=SharedSelection(scene,component,branch.ids,proposed);
  // The original coat contains excluded ribbon branches. Keep their ownership
  // unchanged; only release coat inputs that no other BBC writes or anchors.
  Need(peers.foreign==originalPeers.foreign,"fork-coat-release-foreign-ownership-changed");
  for(int n=0;n<p.boneCount;++n)if(SourceForkCoatRelease(p,n)) {
    const auto bone=branch.ids[n];Need(!peers.foreign.count(bone),"fork-coat-release-foreign-output");
    for(const auto &peer:peers.proofs)if(peer.bone==bone)
      Need(peer.attribute<=0,"fork-coat-release-shared-input");
  }
  int64_t renderer=0;for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==137&&scene.Name(o.first)=="S_actor_endminf_cloth_02_lod0"){
    Need(!renderer,"fork-coat-body-renderer-ambiguous");renderer=o.first;}
  Need(renderer!=0,"fork-coat-body-renderer-missing");const auto m=ReadMesh(package,scene,renderer);
  Need(m.vertices==470,"fork-coat-body-renderer-changed");
  auto out=std::make_shared<DenseRecipe>();auto &r=out->view;r.runtimeGenerated=r.multipleLod=true;r.loop=false;
  r.originalCount=p.boneCount;r.originalRoots=r.rootCount=p.rootCount;r.depth=p.depth;
  r.baseSignature=out->String(p.signature);r.prefabSha=out->String(p.prefabSha);
  const auto key=std::string(p.signature)+"\nfork-coat-original-graph-body-contact-free-side-inputs-v2\n"+m.sourceHash;
  r.signature=out->String(Digest(Bytes(key.begin(),key.end())));
  out->roots.assign(p.roots,p.roots+p.rootCount);for(const auto &bone:base.bones)out->columns.push_back(bone.column);
  out->faces=base.faces;out->lines=base.lines;
  const auto &sd=scene.file.Get(component).At("serializeData");out->radiusCurve=CurveSamples(sd.At("radius"),true);
  out->distanceCurve=CurveSamples(sd.At("distanceConstraint").At("stiffness"),false);
  out->bodyAsset={out->String(m.name),out->String(m.mesh),out->String(m.root),m.vertices,m.submeshes,nullptr,0,out->String(m.parent)};
  for(size_t n=0;n<m.bones.size();++n){ClothBoneBinding binding{out->String(scene.Name(m.bones[n])),out->String(scene.Name(scene.Parent(m.bones[n]))),-1,{}};
    for(int k=0;k<16;++k)binding.bind[k]=float(m.binds[n][k]);out->bodyBindings.push_back(binding);}
  ConfigureCoatTorso(scene,query,p,*out);
  Need(r.ForkCoatBodyOnly(),"fork-coat-body-original-graph-contract");return out;
}
}
