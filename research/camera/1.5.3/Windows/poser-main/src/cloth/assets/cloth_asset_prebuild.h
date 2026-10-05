#pragma once
#include "cloth_asset_model.h"

namespace eiem_cloth_asset {
inline Bytes PrebuildBytes(const Value &v,size_t stride,size_t limit=256) {
  const auto count=v.At("count").Int(),length=v.At("length").Int();
  Need(count>=0&&length>=count&&size_t(length)<=limit,"auto-prebuild-array-budget");
  auto bytes=Blob(v.At("arrayBytes"));
  Need(stride&&bytes.size()==size_t(length)*stride,"auto-prebuild-array-layout");
  bytes.resize(size_t(count)*stride);return bytes;
}
inline int PrebuildInt(const Bytes &b,size_t n) {
  Need(n<b.size()/4,"auto-prebuild-index-range");int32_t v=0;memcpy(&v,b.data()+n*4,4);return v;
}
inline bool SourcePrebuildEnabled(const Value &data2) {
  return data2.Has("preBuildData")&&data2.At("preBuildData").At("enabled").Int()!=0;
}
inline std::map<int64_t,int> SourcePrebuildAttributes(const Value &data2) {
  Need(SourcePrebuildEnabled(data2),"auto-prebuild-selection-disabled");
  const auto &pb=data2.At("preBuildData"),&vm=pb.At("preBuildData").At("proxyMesh");
  std::vector<int64_t> transforms;for(const auto &v:pb.At("uniquePreBuildData").At("proxyMesh").At("transformData").At("transformArray").List())transforms.push_back(v.LocalRef());
  Need(!transforms.empty()&&transforms.size()<=129,"auto-prebuild-closure-budget");
  const auto refs=PrebuildBytes(vm.At("referenceIndices"),4),skin=PrebuildBytes(vm.At("skinBoneTransformIndices"),4),flags=PrebuildBytes(vm.At("attributes"),1);
  Need(!flags.empty()&&refs.size()==flags.size()*4,"auto-prebuild-closure-attributes");std::map<int64_t,int> cached;
  for(size_t n=0;n<flags.size();++n){const int ref=PrebuildInt(refs,n);Need(ref>=0,"auto-prebuild-closure-reference");const int t=PrebuildInt(skin,size_t(ref));
    Need(t>=0&&size_t(t)<transforms.size()&&transforms[t]&&flags[n]<=2&&cached.emplace(transforms[t],flags[n]).second,"auto-prebuild-closure-identity");}
  return cached;
}
inline std::string SourcePrebuild(Scene &s,int64_t component,const Value &data2,
    const std::vector<int64_t> &ids,const std::map<int64_t,int> &attrs) {
  if(!data2.Has("preBuildData")||!data2.At("preBuildData").At("enabled").Int())return {};
  const auto &pb=data2.At("preBuildData"),&shared=pb.At("preBuildData"),&unique=pb.At("uniquePreBuildData");
  Need(shared.At("version").Int()>0&&shared.At("version").Int()==unique.At("version").Int()&&
      shared.At("buildResult").At("result").Int()==2&&unique.At("buildResult").At("result").Int()==2,
      "auto-prebuild-invalid-source-result");
  const auto id=shared.At("buildId").Text();Need(!id.empty()&&id.size()<128,"auto-prebuild-build-id");
  Need(shared.At("renderSetupDataList").List().empty()&&unique.At("renderSetupDataList").List().empty()&&
      shared.At("renderMeshList").List().empty()&&unique.At("renderMeshList").List().empty(),"auto-prebuild-nonbone-render-data");
  const auto &vm=shared.At("proxyMesh"),&td=vm.At("transformData");
  Need(vm.At("isBoneCloth").Int()==1,"auto-prebuild-not-bonecloth");
  std::vector<int64_t> transforms;for(const auto &t:unique.At("proxyMesh").At("transformData").At("transformArray").List())transforms.push_back(t.LocalRef());
  const auto refs=PrebuildBytes(vm.At("referenceIndices"),4),skin=PrebuildBytes(vm.At("skinBoneTransformIndices"),4),
      flags=PrebuildBytes(vm.At("attributes"),1),lines=PrebuildBytes(vm.At("lines"),8),triangles=PrebuildBytes(vm.At("triangles"),12),
      positions=PrebuildBytes(td.At("initLocalPositionArray"),12),rotations=PrebuildBytes(td.At("initLocalRotationArray"),16);
  Need(transforms.size()==ids.size()+1&&refs.size()==ids.size()*4&&flags.size()==ids.size()&&triangles.empty()&&
      positions.size()==transforms.size()*12&&rotations.size()==transforms.size()*16,"auto-prebuild-source-shape");
  std::set<int64_t> expected(ids.begin(),ids.end()),seen;std::vector<int64_t> particles;
  for(size_t n=0;n<ids.size();++n) {
    const int ref=PrebuildInt(refs,n);Need(ref>=0,"auto-prebuild-negative-reference");
    const int index=PrebuildInt(skin,size_t(ref));Need(index>=0&&size_t(index)<transforms.size(),"auto-prebuild-transform-index");
    const auto bone=transforms[size_t(index)];Need(expected.count(bone)&&seen.insert(bone).second&&attrs.at(bone)==flags[n],"auto-prebuild-labelled-attributes");
    const auto &t=s.file.Get(bone),&q=t.At("m_LocalRotation");const auto p=Vec(t.At("m_LocalPosition"));
    float lp[3]{},lq[4]{};memcpy(lp,positions.data()+12*index,12);memcpy(lq,rotations.data()+16*index,16);
    for(int k=0;k<3;++k)Need(std::isfinite(lp[k])&&std::abs(lp[k]-p[k])<.0002,"auto-prebuild-natural-local-position");
    const double actualQ[]{q.At("x").Number(),q.At("y").Number(),q.At("z").Number(),q.At("w").Number()};double dot=0,norm=0;
    for(int k=0;k<4;++k){Need(std::isfinite(lq[k]),"auto-prebuild-local-rotation");dot+=actualQ[k]*lq[k];norm+=double(lq[k])*lq[k];}
    Need(std::abs(norm-1)<.002&&std::abs(std::abs(dot)-1)<.00001,"auto-prebuild-natural-local-rotation");particles.push_back(bone);
  }
  std::set<int64_t> all(transforms.begin(),transforms.end());expected.insert(s.TransformId(component));
  Need(all==expected&&all.size()==transforms.size(),"auto-prebuild-transform-closure");
  std::set<std::array<int64_t,2>> expectedLines,actualLines;
  auto edge=[](int64_t a,int64_t b){std::array<int64_t,2> e{a,b};std::sort(e.begin(),e.end());return e;};
  for(auto b:ids)if(seen.count(s.Parent(b)))expectedLines.insert(edge(b,s.Parent(b)));
  for(size_t n=0;n<lines.size()/8;++n){const auto a=PrebuildInt(lines,2*n),b=PrebuildInt(lines,2*n+1);
    Need(a>=0&&b>=0&&size_t(a)<particles.size()&&size_t(b)<particles.size()&&a!=b&&
        actualLines.insert(edge(particles[a],particles[b])).second,"auto-prebuild-invalid-line");}
  Need(expectedLines==actualLines,"auto-prebuild-source-parent-lines");return id;
}
}
