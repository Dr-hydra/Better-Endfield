#pragma once
#include "cloth_asset_short_sides.h"
#include "cloth_asset_layer_envelope.h"
namespace eiem_cloth_asset {
inline ClothBoneBodySphere FitCalfEnvelope(const std::vector<Point> &samples,double length,int bone) {
  Need(bone>=0&&samples.size()>=80&&std::isfinite(length)&&length>.2&&length<.65,"native-calf-source-invalid");
  Point centers[2]{{-length*.12,0,0},{-length*.92,0,0}};double radii[2]{};
  for(const auto &p:samples){for(auto v:p)Need(std::isfinite(v),"native-calf-nonfinite");Need(p[0]<=0&&p[0]>=-length,"native-calf-axis-range");}
  for(int end=0;end<2;++end){Point lo{0,1e100,1e100},hi{0,-1e100,-1e100};size_t count=0;
    const auto selected=[&](Point p){return end?p[0]<-length*.75:p[0]>-length*.6;};
    for(auto p:samples)if(selected(p)){++count;for(int k=1;k<3;++k){lo[k]=(std::min)(lo[k],p[k]);hi[k]=(std::max)(hi[k],p[k]);}}
    Need(count>=16,"native-calf-section-sparse");for(int k=1;k<3;++k)centers[end][k]=(lo[k]+hi[k])*.5;
    for(auto p:samples)if(selected(p))radii[end]=(std::max)(radii[end],std::hypot(p[1]-centers[end][1],p[2]-centers[end][2]));
  }
  Need(radii[0]>length*.08&&radii[0]<length*.22&&radii[1]>length*.04&&radii[1]<radii[0],"native-calf-envelope-invalid");
  const auto axis=Sub(centers[0],centers[1]);const double span=std::sqrt(Dot(axis,axis)),qw=1+axis[0]/span,qy=-axis[2]/span,qz=axis[1]/span,qn=std::sqrt(qw*qw+qy*qy+qz*qz);
  Need(std::isfinite(qn)&&qn>1,"native-calf-axis-rotation");
  return {bone,{float(centers[0][0]),float(centers[0][1]),float(centers[0][2])},float(radii[0]),float(radii[1]),float(span+radii[0]+radii[1]),{0,float(qy/qn),float(qz/qn),float(qw/qn)}};
}
inline std::vector<ClothBoneResponseFace> NativeLayerFaces(const std::vector<Point> &points,const DenseRecipe &d,Point origin,Point up) {
  const double length=std::sqrt(Dot(up,up));Need(std::isfinite(length)&&length>.001,"native-layer-axis-invalid");for(auto &v:up)v/=length;
  std::map<std::array<int,3>,int> unique;
  for(const auto &graph:d.faces)for(auto face:graph){std::sort(face.begin(),face.end());
    Need(face[0]>=0&&face[0]<face[1]&&face[1]<face[2]&&size_t(face[2])<points.size(),"native-layer-face-invalid");
    Point center{};for(int id:face)for(int k=0;k<3;++k)center[k]+=points[id][k]/3;
    auto radial=Sub(center,origin);const double along=Dot(radial,up);for(int k=0;k<3;++k)radial[k]-=along*up[k];
    const auto normal=Cross(Sub(points[face[1]],points[face[0]]),Sub(points[face[2]],points[face[0]]));
    const double denominator=std::sqrt(Dot(normal,normal)*Dot(radial,radial)),direction=Dot(normal,radial);
    Need(std::isfinite(denominator)&&denominator>1e-9&&std::abs(direction)>denominator*.2,"native-layer-outside-ambiguous");
    const int sign=direction>0?1:-1;auto inserted=unique.emplace(face,sign);Need(inserted.second||inserted.first->second==sign,"native-layer-face-order-conflict");
  }
  Need(!unique.empty()&&unique.size()<=512,"native-layer-face-budget");std::vector<ClothBoneResponseFace> result;
  for(auto f:unique)result.push_back({f.first,f.second});return result;
}
inline void ConfigureArdeliaLayers(Package &package,Scene &scene,const Query &query,Generated &result) {
  if(!SourceShortSkin(scene.file.sha.c_str(),"MC_SkirtShort"))return;
  eiem_cloth_cache::Profile *inner=nullptr,*outer=nullptr;DenseRecipe *inside=nullptr,*outside=nullptr;
  for(auto &p:result.profiles){if(SourceShortContract(p->view))inner=p.get();if(SourceLongLegPanels(p->view))outer=p.get();}
  if(!inner||!outer||!inner->view.generatedLocal||!outer->view.generatedLocal)return;
  for(auto &d:result.dense){if(&d->view==inner->view.generatedLocal)inside=d.get();if(&d->view==outer->view.generatedLocal)outside=d.get();}
  Need(inside&&outside&&inside->view.NativeSkinRetained()&&outside->view.NativePanelsOnly()&&inner->nativeProducers.empty(),"native-layer-existing-policy");
  CompleteShortSides(package,scene,*inner,*inside);
  std::array<float,16> innerThickness{},outerThickness{};
  for(auto profile:{inner,outer}){int64_t id=0;for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==114&&scene.Name(o.first)==profile->view.component){Need(!id,"native-layer-component-ambiguous");id=o.first;}
    Need(id!=0,"native-layer-component-missing");const auto &self=scene.file.Get(id).At("serializeData").At("selfCollisionConstraint");
    Need(self.At("selfMode").Int()==0&&self.At("syncMode").Int()==0&&!self.At("syncPartner").At("m_PathID").Int(),"native-layer-original-sync-present");
    (profile==inner?innerThickness:outerThickness)=CurveSamples(self.At("surfaceThickness"),true);}
  auto points=[&](const eiem_cloth_cache::Profile &p,const DenseRecipe &d){std::vector<Matrix> world;
    for(const auto &b:p.bones)world.push_back(scene.World(FindKey(scene,{b.name,b.parentName})));
    for(size_t n=0;n<d.added.size();++n){const auto &b=d.added[n];Matrix parent{};
      if(b.parent>=0)parent=world.at(b.parent);else {const auto source=FindKey(scene,{p.bones[d.parents[n]].name,p.bones[d.parents[n]].parentName});parent=scene.World(scene.Parent(source));Need(scene.Name(scene.Parent(source))==b.parentName,"native-layer-root-parent-changed");}
      world.push_back(Mul(parent,BoneMatrix(b)));}
    std::vector<Point> out;for(auto w:world)out.push_back(Transform(w,{0,0,0}));return out;};
  const auto origin=Transform(scene.World(FindBodyKey(scene,query.hips)),{0,0,0});
  const auto up=Sub(Transform(scene.World(FindBodyKey(scene,query.spine)),{0,0,0}),origin);
  auto innerFaces=NativeLayerFaces(points(*inner,*inside),*inside,origin,up),outerFaces=NativeLayerFaces(points(*outer,*outside),*outside,origin,up);
  int64_t renderer=0;for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==137&&scene.Name(o.first)=="S_actor_ardelia_cloth_06_lod0"){Need(!renderer,"native-calf-renderer-ambiguous");renderer=o.first;}
  Need(renderer!=0,"native-calf-renderer-missing");const auto m=ReadMesh(package,scene,renderer);
  Need(m.sourceHash=="4575dd9deb9c974638f930a8a0f378d4419668835190a8b070c3b43c6f922ba1"&&m.vertices==723,"native-calf-source-changed");
  std::vector<ClothBoneBodySphere> shapes;
  for(const std::string side:{"L","R"}){const auto prefix="Bip001_"+side;int root=-1,target=-1,mid=-1;std::set<int> group;
    for(size_t k=0;k<m.bones.size();++k){const auto name=scene.Name(m.bones[k]);if(name==prefix+"_Calf")root=int(k);if(name==prefix+"CalfTwist")target=int(k);if(name==prefix+"CalfTwist1")mid=int(k);
      if(name==prefix+"_Calf"||name==prefix+"CalfTwist"||name==prefix+"CalfTwist1")group.insert(int(k));}
    Need(root>=0&&target>=0&&mid>=0&&group.size()==3&&scene.Parent(m.bones[target])==m.bones[root]&&scene.Parent(m.bones[mid])==m.bones[target],"native-calf-parent-chain-unconfirmed");
    const auto inverse=Inverse(scene.World(m.bones[target]));const auto middle=Transform(inverse,Transform(scene.World(m.bones[mid]),{0,0,0}));const double length=-middle[0]*2;
    Need(std::hypot(middle[1],middle[2])<.001,"native-calf-axis-unconfirmed");std::vector<Point> samples;
    for(size_t v=0;v<m.world.size();++v){double sum=0,owned=0;for(size_t k=0;k<m.weights[v].size();++k){sum+=m.weights[v][k];if(group.count(int(m.indices[v][k])))owned+=m.weights[v][k];}
      if(sum<=0||owned/sum<.5)continue;auto p=m.world[v];for(auto &x:p)x/=sum;p=Transform(inverse,p);if(p[0]<=0&&p[0]>=-length)samples.push_back(p);}
    shapes.push_back(FitCalfEnvelope(samples,length,target));}
  auto &r=outside->view;outside->bodyAsset={outside->String(m.name),outside->String(m.mesh),outside->String(m.root),m.vertices,m.submeshes,nullptr,0,outside->String(m.parent)};
  for(size_t n=0;n<m.bones.size();++n){ClothBoneBinding b{outside->String(scene.Name(m.bones[n])),outside->String(scene.Name(scene.Parent(m.bones[n]))),-1,{}};for(int k=0;k<16;++k)b.bind[k]=float(m.binds[n][k]);outside->bodyBindings.push_back(b);}
  outside->bodySpheres=std::move(shapes);r.bodyCoverage=true;r.nativeLayer=2;r.layerPeer=outside->String(inner->view.component);outside->layerFaces=std::move(outerFaces);
  auto &s=inside->view;s.nativeLayer=1;s.layerPeer=inside->String(outer->view.component);s.contactProducer=s.layerPeer;inside->layerFaces=std::move(innerFaces);
  FitShortLayerEnvelope(package,scene,*inner,*inside,points(*inner,*inside),innerThickness,points(*outer,*outside),outside->layerFaces,outerThickness);
  for(auto d:{inside,outside}){const std::string identity=std::string(d->view.signature)+"\nnative-layer-calf-skin-clearance-v3\n"+m.sourceHash;d->view.signature=d->String(Digest(Bytes(identity.begin(),identity.end())));d->Link();}
  inner->nativeProducers.push_back(s.contactProducer);inner->Link();
  Need(ClothGeneratedLayerPair(outer->view,inner->view),"native-layer-final-contract");
  Need(ClothGeneratedContactEnvelope(outer->view,inner->view),"native-layer-envelope-contract");
  result.reports.push_back({inner->view.component,"native-short-inside-long-FullMesh-layer-order-pending"});
  result.reports.push_back({inner->view.component,"source-visible-six-chain-short-ring-side-branches-pending"});
  result.reports.push_back({outer->view.component,"native-long-panels-two-source-fitted-calf-capsules-pending"});
  result.reports.push_back({inner->view.component,s.layerContactClearanceLimit<1?"source-skin-contact-envelope-capped-by-natural-layer-clearance":
      s.layerContactCapped?"source-skin-contact-envelope-capped-by-authored-particle-radius":"source-skin-contact-envelope-fitted"});
}
}
