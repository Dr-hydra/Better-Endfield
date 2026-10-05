#pragma once
#include "cloth_asset_model.h"
#include "cloth_asset_prebuild.h"
#include "cloth_asset_dense.h"

namespace eiem_cloth_asset {
struct SourceBranch {
  std::vector<int64_t> ids,ignored,excluded,prebuildOmitted;
  std::map<int64_t,int> columns;
};
inline SourceBranch EffectiveBranch(Scene &s,const Value &sd) {
  std::vector<int64_t> roots;
  for(const auto &v:sd.At("rootBones").List()){const auto id=v.LocalRef();Need(id!=0,"auto-null-original-root");roots.push_back(id);}
  SourceBranch out;const auto full=s.Branch(roots,&out.columns);Need(full.size()<=512,"auto-source-branch-budget");
  std::set<int64_t> removed,declared;
  if(sd.Has("ignoreFromRootBones"))for(const auto &v:sd.At("ignoreFromRootBones").List()) {
    const auto id=v.LocalRef();Need(id&&out.columns.count(id)&&declared.insert(id).second&&
        std::find(roots.begin(),roots.end(),id)==roots.end(),"auto-ignore-reference-outside-or-duplicate-root");
    out.ignored.push_back(id);
    for(auto b:s.Branch({id}))removed.insert(b);
  }
  Need(out.ignored.size()<=128&&removed.size()<=256,"auto-ignore-branch-budget");
  for(auto b:full)if(removed.count(b)){out.excluded.push_back(b);out.columns.erase(b);}else out.ids.push_back(b);
  Need(out.ids.size()<=128,"auto-bone-budget");return out;
}
inline void PrebuildSourceClosure(Scene &s,int64_t component,const Value &data2,SourceBranch &branch) {
  if(!data2.Has("preBuildData")||!data2.At("preBuildData").At("enabled").Int())return;
  Need(component!=0,"auto-prebuild-component-missing");
  const auto cached=SourcePrebuildAttributes(data2);
  std::set<int64_t> omitted;for(auto b:branch.ids)if(!cached.count(b)){
    const auto parent=s.Parent(b);Need(cached.count(parent)&&s.children[b].empty(),"auto-prebuild-omitted-nonleaf-or-uncached-parent");
    const auto go=s.file.Get(b).At("m_GameObject").LocalRef();
    for(const auto &o:s.file.objects){const int type=s.file.Class(o.first);if(type==1||o.first==b)continue;const auto &v=s.file.Get(o.first);
      if(v.Has("m_GameObject"))Need(v.At("m_GameObject").LocalRef()!=go,"auto-prebuild-omitted-leaf-has-component");
      if(type==137)for(const auto &r:v.At("m_Bones").List())Need(r.LocalRef()!=b,"auto-prebuild-omitted-leaf-render-bound");
      if(type==114){const auto &sd=v.At("serializeData");if(sd.Has("rootBones"))for(const auto &r:sd.At("rootBones").List())Need(r.LocalRef()!=b,"auto-prebuild-omitted-leaf-other-owner");}}
    Need(omitted.insert(b).second&&omitted.size()<=16,"auto-prebuild-omitted-leaf-budget");
  }
  if(omitted.empty())return;
  std::vector<int64_t> kept;for(auto b:branch.ids)if(omitted.count(b)){branch.prebuildOmitted.push_back(b);branch.columns.erase(b);}else kept.push_back(b);
  Need(kept.size()==cached.size(),"auto-prebuild-closure-outside-source");branch.ids=std::move(kept);
}
inline std::vector<int> MatchSelection(const std::vector<Point> &samples,const std::vector<int> &flags,const std::vector<Point> &bones) {
  Need(!samples.empty()&&samples.size()==flags.size()&&samples.size()<=512&&!bones.empty(),"auto-selection-size");
  std::vector<bool> covered(samples.size());std::vector<int> result;
  for(int f:flags)Need(f>=0&&f<=2,"auto-selection-flag");
  for(auto b:bones) {
    int attribute=-1;
    for(size_t n=0;n<samples.size();++n)if(Distance(samples[n],b)<=.001) {
      Need(attribute<0||attribute==flags[n],"auto-selection-conflicting-coincident-attributes");
      attribute=flags[n];covered[n]=true;
    }
    Need(attribute>=0,"auto-selection-unmapped-bone");result.push_back(attribute);
  }
  Need(std::all_of(covered.begin(),covered.end(),[](bool b){return b;}),"auto-selection-unmapped-source-sample");return result;
}
inline std::vector<int> SpatialSelection(const std::vector<Point> &samples,const std::vector<int> &flags,
    const std::vector<Point> &bones,double radius,bool exactDominance=false) {
  Need(!samples.empty()&&samples.size()==flags.size()&&samples.size()<=512&&!bones.empty()&&bones.size()<=128&&
      std::isfinite(radius)&&radius>0,"auto-selection-spatial-input");
  for(auto p:samples)for(double x:p)Need(std::isfinite(x),"auto-selection-spatial-nonfinite");
  for(auto p:bones)for(double x:p)Need(std::isfinite(x),"auto-selection-spatial-nonfinite");
  for(int f:flags)Need(f>=0&&f<=2,"auto-selection-flag");
  std::vector<int> result;std::vector<bool> used(samples.size());std::vector<double> distances(samples.size());bool allExact=true;
  for(auto p:bones) {
    double nearest=std::numeric_limits<double>::infinity();size_t first=0;
    for(size_t n=0;n<samples.size();++n){distances[n]=Distance(samples[n],p);if(distances[n]<nearest){nearest=distances[n];first=n;}}
    Need(nearest<=radius,"auto-selection-spatial-outside-radius");allExact&=nearest<=1e-5;
    for(size_t n=0;n<samples.size();++n) {
      if(distances[n]<=nearest+1e-5){Need(flags[n]==flags[first],"auto-selection-spatial-conflicting-attributes");used[n]=true;}
      if(Distance(samples[n],samples[first])>1e-5)
        Need(nearest<=distances[n]*.05,"auto-selection-spatial-ambiguous-neighbour");
    }
    result.push_back(flags[first]);
  }
  for(size_t n=0;n<samples.size();++n)Need(used[n]||flags[n]==1||(exactDominance&&allExact),"auto-selection-spatial-unmapped-nonfixed-sample");
  return result;
}
inline std::map<int64_t,int> SourceSelection(Scene &s,int64_t component,const Value &data2,const std::vector<int64_t> &ids,bool *spatial=nullptr,bool exactDominance=false) {
  if(spatial)*spatial=false;
  const auto &selection=data2.At("selectionData"),&raw=selection.At("attributes");
  std::vector<Point> samples,world,local;std::vector<int> flags;
  for(const auto &p:selection.At("positions").List())samples.push_back(Vec(p));
  if(raw.kind==Value::Blob)for(auto f:raw.bytes)flags.push_back(f);
  else for(const auto &f:raw.List())flags.push_back(int(f.kind==Value::Integer?f.Int():f.At("Value").Int()));
  const auto inverse=Inverse(s.World(s.TransformId(component)));
  for(auto id:ids){world.push_back(Transform(s.World(id),{0,0,0}));local.push_back(Transform(inverse,world.back()));}
  std::vector<int> a,b;bool worldOK=false,localOK=false;
  try{a=MatchSelection(samples,flags,world);worldOK=true;}catch(const std::runtime_error &){}
  try{b=MatchSelection(samples,flags,local);localOK=true;}catch(const std::runtime_error &){}
  if(!worldOK&&!localOK) {
    Need(selection.Has("maxConnectionDistance"),"auto-selection-source-and-effective-graph-unresolved");
    b=SpatialSelection(samples,flags,local,selection.At("maxConnectionDistance").Number(),exactDominance);localOK=true;if(spatial)*spatial=true;
  }
  Need(!worldOK||!localOK||a==b,"auto-selection-reference-space-ambiguous");
  const auto &chosen=worldOK?a:b;std::map<int64_t,int> out;
  for(size_t n=0;n<ids.size();++n)out.emplace(ids[n],chosen[n]);return out;
}
struct SourceOwnership {
  struct Proof {int64_t component,bone;int attribute;bool zeroRotationAnchor=false;};
  std::vector<Proof> proofs;
  std::set<int64_t> foreign;
};
inline SourceOwnership SharedSelection(Scene &s,int64_t component,const std::vector<int64_t> &ids,const std::map<int64_t,int> &attrs) {
  SourceOwnership out;std::set<int64_t> own(ids.begin(),ids.end());
  for(const auto &o:s.file.objects)if(o.first!=component&&s.file.Class(o.first)==114) {
    const auto &object=s.file.Get(o.first),&sd=object.At("serializeData");if(!sd.Has("clothType"))continue;
    std::vector<int64_t> roots;for(const auto &v:sd.At("rootBones").List())roots.push_back(v.LocalRef());
    const auto full=s.Branch(roots);std::set<int64_t> related;for(auto b:full)if(own.count(b))related.insert(b);if(related.empty())continue;
    Need(sd.At("clothType").Int()==1,"auto-shared-bone-foreign-cloth-type");
    auto other=EffectiveBranch(s,sd);PrebuildSourceClosure(s,o.first,object.At("serializeData2"),other);std::map<int64_t,int> flags;
    try{const auto &data2=object.At("serializeData2");flags=SourcePrebuildEnabled(data2)?SourcePrebuildAttributes(data2):SourceSelection(s,o.first,data2,other.ids,nullptr,true);SourcePrebuild(s,o.first,data2,other.ids,flags);}
    catch(const std::exception &e){throw std::runtime_error("auto-shared-selection:"+s.Name(o.first)+":"+e.what());}
    for(auto b:related){const int flag=flags.count(b)?flags.at(b):-1;
      const bool anchor=attrs.at(b)==2&&flag==1&&std::find(roots.begin(),roots.end(),b)!=roots.end()&&
          sd.Has("rootRotation")&&sd.At("rootRotation").Number()==0&&sd.Has("connectionMode")&&sd.At("connectionMode").Int()==0;
      out.proofs.push_back({o.first,b,flag,anchor});}
    for(auto b:ids)if(attrs.at(b)==1&&flags.count(b)&&flags.at(b)==2) {
      const auto parent=s.Parent(b);
      Need(own.count(parent)&&attrs.at(parent)==1&&flags.count(parent)&&flags.at(parent)==1,"auto-foreign-branch-attachment-unconfirmed");
      for(auto child:s.Branch({b}))if(own.count(child)) {
        Need(attrs.at(child)>0&&flags.count(child)&&flags.at(child)==2,"auto-foreign-branch-mixed-ownership");out.foreign.insert(child);
      }
    }
  }
  Need(out.proofs.size()<=1024,"auto-ownership-proof-budget");
  for(auto b:out.foreign){int producers=0;for(const auto &p:out.proofs)producers+=p.bone==b&&p.attribute==2;Need(producers==1,"auto-foreign-branch-multiple-producers");}
  for(const auto &p:out.proofs)if(attrs.at(p.bone)==2&&!out.foreign.count(p.bone))Need(p.attribute<=0||p.zeroRotationAnchor,"auto-shared-free-bone");
  return out;
}
inline std::set<int64_t> PassiveSourceLeaves(Scene &s,const std::vector<int64_t> &ids,const std::map<int64_t,int> &attrs) {
  std::set<int64_t> bound,passive,referencedRoots;
  for(const auto &o:s.file.objects)if(s.file.Class(o.first)==137)for(const auto &b:s.file.Get(o.first).At("m_Bones").List())bound.insert(b.LocalRef());
  for(const auto &o:s.file.objects)if(s.file.Class(o.first)==114){const auto &data=s.file.Get(o.first).At("serializeData");if(data.Has("rootBones"))for(const auto &b:data.At("rootBones").List())referencedRoots.insert(b.LocalRef());}
  for(auto id:ids) {
    const auto &t=s.file.Get(id);const auto parent=s.Parent(id);
    if(!attrs.at(id)||!attrs.count(parent)||attrs.at(parent)!=attrs.at(id)||bound.count(id)||referencedRoots.count(id)||!s.children[id].empty())continue;
    const auto &go=s.file.Get(t.At("m_GameObject").LocalRef());if(!go.Has("m_Component"))continue;
    const auto &components=go.At("m_Component").List();bool verified=components.size()==1||components.size()==2,transform=false;
    for(const auto &c:components){const auto object=c.At("component").LocalRef();if(object==id){transform=true;continue;}
      const auto &v=s.file.Get(object);verified&=s.file.Class(object)==114&&v.Has("center")&&v.Has("size");}
    if(!verified||!transform)continue;
    if(Distance(Vec(t.At("m_LocalPosition")),{0,0,0})<1e-7){passive.insert(id);continue;}
    if(components.size()!=1||attrs.at(id)!=2)continue;
    int continuation=0;
    for(auto child:s.children[parent])if(child!=id&&attrs.count(child)&&attrs.at(child)&&bound.count(child))++continuation;
    if(continuation==1){passive.insert(id);continue;}
    if(continuation)continue;
    const auto grand=s.Parent(parent);if(!grand||!attrs.count(grand))continue;
    const auto p=Transform(s.World(parent),{0,0,0}),incoming=Sub(p,Transform(s.World(grand),{0,0,0}));
    double best=-2,next=-2;int64_t chosen=0;
    for(auto child:s.children[parent])if(attrs.count(child)&&attrs.at(child)) {
      const auto direction=Sub(Transform(s.World(child),{0,0,0}),p);const double length=std::sqrt(Dot(direction,direction)*Dot(incoming,incoming));if(length<1e-10)continue;
      const double cosine=Dot(direction,incoming)/length;if(cosine>best){next=best;best=cosine;chosen=child;}else next=(std::max)(next,cosine);
    }
    if(chosen&&chosen!=id&&best>.98&&best-next>.25)passive.insert(id);
  }
  return passive;
}
}
