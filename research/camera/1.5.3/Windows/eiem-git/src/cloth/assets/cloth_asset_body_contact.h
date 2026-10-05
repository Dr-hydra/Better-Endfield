#pragma once
namespace eiem_cloth_asset {
inline std::vector<ClothBoneBodySphere> FitThighSections(const std::vector<Point> &samples,double length,int bone) {
  Need(bone>=0&&samples.size()>=100&&samples.size()<=200000&&std::isfinite(length)&&length>.2&&length<.65,"body-contact-thigh-samples");
  for(auto p:samples)for(auto x:p)Need(std::isfinite(x),"body-contact-nonfinite-sample");
  std::vector<ClothBoneBodySphere> shapes;std::vector<std::array<Point,2>> centers;std::vector<double> radii;
  for(double fraction:{.14,.38,.62,.86}){
    const double x=-length*fraction;Point lo{x,1e100,1e100},hi{x,-1e100,-1e100};size_t count=0;
    for(auto p:samples)if(std::abs(p[0]-x)<=length*.14){++count;for(int k=1;k<3;++k){lo[k]=(std::min)(lo[k],p[k]);hi[k]=(std::max)(hi[k],p[k]);}}
    Need(count>=16,"body-contact-thigh-section-sparse");const double ry=(hi[1]-lo[1])*.5,rz=(hi[2]-lo[2])*.5,radius=(std::min)(ry,rz);const int major=ry>rz?1:2;
    Need(radius>length*.07&&radius<length*.25&&(std::max)(ry,rz)<length*.3,"body-contact-thigh-section-shape");
    std::array<Point,2> pair;for(int end=0;end<2;++end){Point p{x,(lo[1]+hi[1])*.5,(lo[2]+hi[2])*.5};p[major]+=(end?1.:-1.)*((std::max)(ry,rz)-radius);pair[end]=p;}
    centers.push_back(pair);radii.push_back(radius);
  }
  for(size_t n=1;n<centers.size();++n)for(int end=0;end<2;++end){const auto a=centers[n-1][end],b=centers[n][end],axis=Sub(a,b);const double span=std::sqrt(Dot(axis,axis));
    Need(span>length*.2&&span<length*.3,"body-contact-section-axis");const double qw=1+axis[0]/span,qy=-axis[2]/span,qz=axis[1]/span,qn=std::sqrt(qw*qw+qy*qy+qz*qz);
    Need(std::isfinite(qn)&&qn>1,"body-contact-section-rotation");
    shapes.push_back({bone,{float(a[0]),float(a[1]),float(a[2])},float(radii[n-1]),float(radii[n]),float(span+radii[n-1]+radii[n]),{0,float(qy/qn),float(qz/qn),float(qw/qn)}});}
  return shapes;
}
inline std::shared_ptr<DenseRecipe> GenerateBodyContact(Package &package,Scene &scene,int64_t component,const Query &query,const eiem_cloth_cache::Profile &base) {
  const auto &p=base.view;Need(p.runtimeBodyOnly&&SourceBodyContact(p.prefabSha,p.component)&&!p.generatedLocal&&
      p.nativeGraphCount==1&&p.nativeGraphs[0].faceCount==0&&p.nativeGraphs[0].lineCount==25,"body-contact-original-line-contract");
  int64_t id=0;for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==137&&scene.Name(o.first)=="S_actor_laevat_body_01_lod0"){
    Need(!id,"body-contact-body-ambiguous");id=o.first;}
  Need(id!=0,"body-contact-body-missing");const auto m=ReadMesh(package,scene,id);
  Need(m.sourceHash=="43d7c8daf7f7515b991c608d774815e6585e2f53830f1e06aae98bdff738fad7"&&m.vertices==3289,"body-contact-body-source-changed");
  auto out=std::make_shared<DenseRecipe>();auto &r=out->view;r.runtimeGenerated=r.multipleLod=r.sourceBodyOnly=r.bodyCoverage=true;r.loop=false;
  r.originalCount=p.boneCount;r.originalRoots=r.rootCount=p.rootCount;r.depth=p.depth;
  r.baseSignature=out->String(p.signature);r.prefabSha=out->String(p.prefabSha);
  const auto identity=std::string(p.signature)+"\nnative-source-line-thigh-sections-v1\n"+m.sourceHash;
  r.signature=out->String(Digest(Bytes(identity.begin(),identity.end())));
  out->roots.assign(p.roots,p.roots+p.rootCount);for(const auto &b:base.bones)out->columns.push_back(b.column);
  out->faces=base.faces;out->lines=base.lines;
  const auto &sd=scene.file.Get(component).At("serializeData");out->radiusCurve=CurveSamples(sd.At("radius"),true);out->distanceCurve=CurveSamples(sd.At("distanceConstraint").At("stiffness"),false);
  out->bodyAsset={out->String(m.name),out->String(m.mesh),out->String(m.root),m.vertices,m.submeshes,nullptr,0,out->String(m.parent)};
  for(size_t n=0;n<m.bones.size();++n){ClothBoneBinding b{out->String(scene.Name(m.bones[n])),out->String(scene.Name(scene.Parent(m.bones[n]))),-1,{}};
    for(int k=0;k<16;++k)b.bind[k]=float(m.binds[n][k]);out->bodyBindings.push_back(b);}
  const BoneKey thighs[]{query.leftThigh,query.rightThigh},calves[]{query.leftCalf,query.rightCalf};
  size_t selected=0;
  for(int side=0;side<2;++side){const auto thigh=FindBodyKey(scene,thighs[side]),calf=FindBodyKey(scene,calves[side]);
    Need(scene.Parent(calf)==thigh,"body-contact-leg-parent-chain");const auto inverse=Inverse(scene.World(thigh));QuaternionOf(scene.World(thigh));
    const auto end=Transform(inverse,Transform(scene.World(calf),{0,0,0}));const double length=-end[0];
    Need(length>.2&&length<.65&&std::hypot(end[1],end[2])<.001,"body-contact-thigh-axis");
    int target=-1;std::set<int> group;
    for(size_t n=0;n<m.bones.size();++n){if(m.bones[n]==thigh)target=int(n);if(Above(scene,m.bones[n],thigh)&&!Above(scene,m.bones[n],calf))group.insert(int(n));}
    Need(target>=0&&group.size()==3,"body-contact-thigh-palette");std::vector<Point> samples;
    for(size_t v=0;v<m.world.size();++v){double sum=0,own=0;for(size_t k=0;k<m.weights[v].size();++k){sum+=m.weights[v][k];if(group.count(int(m.indices[v][k])))own+=m.weights[v][k];}
      if(sum<=0||own/sum<.85)continue;auto w=m.world[v];for(auto &x:w)x/=sum;const auto q=Transform(inverse,w);
      if(q[0]<=0&&q[0]>=-length)samples.push_back(q);}
    auto shapes=FitThighSections(samples,length,target);selected+=samples.size();out->bodySpheres.insert(out->bodySpheres.end(),shapes.begin(),shapes.end());}
  out->densityReport="auto-source-line-body-contact bodySamples="+std::to_string(selected)+" addedThighCapsules=12 nativeLineRetained=1 originalSkinRetained=1 sourceCapsuleWrites=0 contactAndVisual=pending";
  out->Link();Need(r.NativeBodyOnly(),"body-contact-generated-contract");return out;
}
}
