#pragma once
namespace eiem_cloth_asset {
inline void ConfigureCoatCalves(Package &package,Scene &scene,const Query &query,const ClothBoneProfile &p,DenseRecipe &out) {
  auto &r=out.view;
  Need(SourceOriginalCoverageCoat(p.prefabSha,p.component)&&r.NativePanelsOnly()&&!r.bodyCoverage&&
      !r.nativeLayer&&!r.bodySphereCount&&!r.meshCount&&out.bodyBindings.empty()&&out.bodySpheres.empty(),"coat-calf-existing-policy");
  int64_t renderer=0;
  for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==137&&scene.Name(o.first)=="S_actor_endminm_cloth_02_lod0"){
    Need(!renderer,"coat-calf-renderer-ambiguous");renderer=o.first;}
  Need(renderer!=0,"coat-calf-renderer-missing");const auto m=ReadMesh(package,scene,renderer);
  Need(m.sourceHash=="e644d2ddf27b4189d123fc51a5c6a4bc29d8f3ad67684fc54aae9e8b541c036b"&&m.vertices==13346,"coat-calf-source-changed");
  const BoneKey calves[]{query.leftCalf,query.rightCalf},feet[]{query.leftFoot,query.rightFoot};
  std::vector<ClothBoneBodySphere> shapes;size_t selected=0;
  for(int side=0;side<2;++side){const auto calf=FindBodyKey(scene,calves[side]),foot=FindBodyKey(scene,feet[side]);
    Need(scene.Parent(foot)==calf,"coat-calf-foot-parent");const auto inverse=Inverse(scene.World(calf));QuaternionOf(scene.World(calf));
    const auto end=Transform(inverse,Transform(scene.World(foot),{0,0,0}));const double length=-end[0];
    Need(length>.2&&length<.65&&std::hypot(end[1],end[2])<.001,"coat-calf-axis-unconfirmed");
    int target=-1;std::set<int> group;
    for(size_t n=0;n<m.bones.size();++n){if(m.bones[n]==calf)target=int(n);if(Above(scene,m.bones[n],calf)&&!Above(scene,m.bones[n],foot))group.insert(int(n));}
    Need(target>=0&&group.size()==3,"coat-calf-palette-unconfirmed");std::vector<Point> samples;
    for(size_t v=0;v<m.world.size();++v){double sum=0,own=0;for(size_t k=0;k<m.weights[v].size();++k){sum+=m.weights[v][k];if(group.count(int(m.indices[v][k])))own+=m.weights[v][k];}
      if(sum<=0||own/sum<.5)continue;auto point=m.world[v];for(auto &x:point)x/=sum;point=Transform(inverse,point);
      if(point[0]<=0&&point[0]>=-length)samples.push_back(point);}
    shapes.push_back(FitCalfEnvelope(samples,length,target));selected+=samples.size();
  }
  out.bodyAsset={out.String(m.name),out.String(m.mesh),out.String(m.root),m.vertices,m.submeshes,nullptr,0,out.String(m.parent)};
  for(size_t n=0;n<m.bones.size();++n){ClothBoneBinding b{out.String(scene.Name(m.bones[n])),out.String(scene.Name(scene.Parent(m.bones[n]))),-1,{}};
    for(int k=0;k<16;++k)b.bind[k]=float(m.binds[n][k]);out.bodyBindings.push_back(b);}
  out.bodySpheres=std::move(shapes);r.bodyCoverage=r.sourceCoatCalves=true;
  const auto identity=std::string(r.signature)+"\nendminm-calf-envelope-v1\n"+m.sourceHash;
  r.signature=out.String(Digest(Bytes(identity.begin(),identity.end())));out.Link();
  Need(r.NativePanelsOnly()&&r.CoatCalfCoverage(),"coat-calf-generated-contract");
  out.densityReport+=" addedCalfCapsules=2 calfSamples="+std::to_string(selected)+" originalSkinRetained=1 sourceCapsuleWrites=0 contactAndVisual=pending";
}
}
