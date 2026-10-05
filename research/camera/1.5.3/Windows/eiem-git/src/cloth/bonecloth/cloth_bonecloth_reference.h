#pragma once
#include "cloth_bonecloth_limits.h"
#include "../native/cloth_native_math.h"
#include "cloth_bonecloth_graph_order.h"
#include <map>
#include <iterator>
namespace eiem_cloth_rebuild {
inline bool PrebuildReferenceDelta(int before,int after,int delta) {
  return before>=0&&after>=0&&before<=100000&&after<=100000&&
      (delta==1||delta==-1)&&after-before==delta;
}
using eiem_cloth_surface::OutputMatrix;
using eiem_cloth_surface::OutputAffine;
using eiem_cloth_surface::OutputMultiply;
using eiem_cloth_surface::OutputPoint;
struct ReferencePose { double p[3]{}, q[4]{0,0,0,1}; };
inline bool PositiveAxes(double x,double y,double z) {
  return std::isfinite(x)&&std::isfinite(y)&&std::isfinite(z)&&x>1e-6&&y>1e-6&&z>1e-6&&x<1e3&&y<1e3&&z<1e3;
}
inline bool OrthogonalPositive(const OutputMatrix &m) {
  if(!OutputAffine(m))return false;
  double lengths[3]{};
  for(int a=0;a<3;++a)for(int k=0;k<3;++k)lengths[a]+=m.v[a*4+k]*m.v[a*4+k];
  if(!PositiveAxes(std::sqrt(lengths[0]),std::sqrt(lengths[1]),std::sqrt(lengths[2])))return false;
  for(int a=0;a<3;++a)for(int b=a+1;b<3;++b) {
    double dot=0;for(int k=0;k<3;++k)dot+=m.v[a*4+k]*m.v[b*4+k];
    if(std::fabs(dot)>std::sqrt(lengths[a]*lengths[b])*.001)return false;
  }
  return m.v[0]*(m.v[5]*m.v[10]-m.v[9]*m.v[6])-m.v[4]*(m.v[1]*m.v[10]-m.v[9]*m.v[2])+
      m.v[8]*(m.v[1]*m.v[6]-m.v[5]*m.v[2])>0;
}
template<class Profile> inline bool NativeGraphCell(const Profile &p,const std::array<int,3> &face,std::array<int,4> &cell) {
  int loC=32,hiC=-1,loD=32,hiD=-1;
  for(int n:face) {
    if(n<0||n>=p.boneCount||!p.bones[n].attribute||p.Passive(n))return false;
    const auto &b=p.bones[n];loC=(std::min)(loC,b.column);hiC=(std::max)(hiC,b.column);
    loD=(std::min)(loD,b.depth);hiD=(std::max)(hiD,b.depth);
  }
  if(hiD-loD!=1||!(hiC-loC==1||(p.loop&&loC==0&&hiC==p.rootCount-1)))return false;
  cell.fill(-1);
  for(int n=0;n<p.boneCount;++n) {
    const auto &b=p.bones[n];if(!b.attribute||p.Passive(n))continue;
    if((b.column==loC||b.column==hiC)&&(b.depth==loD||b.depth==hiD)) {
      const int k=2*int(b.column==hiC)+int(b.depth==hiD);if(cell[k]>=0)return false;cell[k]=n;
    }
  }
  if(std::find(cell.begin(),cell.end(),-1)!=cell.end())return false;
  for(int n:face)if(std::find(cell.begin(),cell.end(),n)==cell.end())return false;
  std::sort(cell.begin(),cell.end());return true;
}
template<class Profile> inline bool NativeGraphQuadPair(const Profile &p,const std::vector<std::array<int,3>> &faces) {
  if(faces.size()!=2||faces[0]==faces[1])return false;
  std::vector<int> shared;
  for(int n:faces[0])if(std::find(faces[1].begin(),faces[1].end(),n)!=faces[1].end())shared.push_back(n);
  return shared.size()==2&&p.bones[shared[0]].column!=p.bones[shared[1]].column&&
      p.bones[shared[0]].depth!=p.bones[shared[1]].depth;
}
template<class Profile> inline bool NativeGraphMatch(const Profile &p,const std::vector<std::array<int,3>> &faces,
    const std::vector<std::array<int,2>> &lines) {
  if(!p.nativeGraphs||p.nativeGraphCount<1||p.nativeGraphCount>8||(faces.empty()&&!p.runtimeBodyOnly)||faces.size()>ClothBoneMaxFaces||lines.size()>ClothBoneMaxEdges)return false;
  if(p.runtimeFixedForks)return p.runtimeGenerated&&p.nativeGraphOrder&&eiem_cloth_graph::Match(*p.nativeGraphOrder,faces,lines);
  for(int n=0;n<p.nativeGraphCount;++n) {
    const auto &g=p.nativeGraphs[n];if(g.faceCount!=int(faces.size())||g.lineCount!=int(lines.size()))continue;
    std::vector<std::array<int,3>> expectedFaces;std::vector<std::array<int,2>> expectedLines;
    for(int k=0;k<g.faceCount;++k)expectedFaces.push_back(g.faces[k]);
    for(int k=0;k<g.lineCount;++k)expectedLines.push_back(g.lines[k]);
    if(eiem_cloth_surface::SameSimplices(faces,expectedFaces)&&eiem_cloth_surface::SameSimplices(lines,expectedLines))return true;
  }
  if(p.rootCount<3||p.boneCount>ClothBoneMaxIdentities)return false;
  using Face=std::array<int,3>;using Cell=std::array<int,4>;
  auto canonical=[](std::vector<Face> fs){for(auto &f:fs)std::sort(f.begin(),f.end());std::sort(fs.begin(),fs.end());return fs;};
  const auto actual=canonical(faces);
  if(std::adjacent_find(actual.begin(),actual.end())!=actual.end())return false;
  for(int left=0;left<p.nativeGraphCount;++left)for(int right=left+1;right<p.nativeGraphCount;++right) {
    const auto &a=p.nativeGraphs[left],&b=p.nativeGraphs[right];
    if(a.faceCount!=b.faceCount||a.faceCount!=int(actual.size()))continue;
    std::vector<std::array<int,2>> al(a.lines,a.lines+a.lineCount),bl(b.lines,b.lines+b.lineCount);
    if(!eiem_cloth_surface::SameSimplices(lines,al)||!eiem_cloth_surface::SameSimplices(al,bl))continue;
    const auto af=canonical(std::vector<Face>(a.faces,a.faces+a.faceCount)),bf=canonical(std::vector<Face>(b.faces,b.faces+b.faceCount));
    std::vector<Face> common;
    std::set_intersection(af.begin(),af.end(),bf.begin(),bf.end(),std::back_inserter(common));
    using Groups=std::map<Cell,std::vector<Face>>;
    auto groups=[&](const std::vector<Face> &fs,Groups &out) {
      if(!std::includes(fs.begin(),fs.end(),common.begin(),common.end()))return false;
      for(const auto &f:fs)if(!std::binary_search(common.begin(),common.end(),f)) {
        Cell cell{};if(!NativeGraphCell(p,f,cell))return false;out[cell].push_back(f);
      }
      for(const auto &g:out)if(!NativeGraphQuadPair(p,g.second))return false;
      return true;
    };
    Groups ga,gb,got;
    if(!groups(af,ga)||!groups(bf,gb)||!groups(actual,got)||ga.empty()||ga.size()!=gb.size()||ga.size()!=got.size())continue;
    bool same=true;
    for(const auto &g:ga) {
      auto alt=gb.find(g.first),now=got.find(g.first);
      if(alt==gb.end()||now==got.end()||(now->second!=g.second&&now->second!=alt->second)){same=false;break;}
    }
    if(same)return true;
  }
  return false;
}
inline bool UnitQuaternion(const double *q) {
  double n=0; for(int k=0;k<4;++k) { if(!std::isfinite(q[k])) return false; n+=q[k]*q[k]; }
  return std::fabs(n-1)<.002;
}
inline void MultiplyQuaternion(const double *a,const double *b,double *out) {
  const double q[]{a[3]*b[0]+a[0]*b[3]+a[1]*b[2]-a[2]*b[1],
    a[3]*b[1]-a[0]*b[2]+a[1]*b[3]+a[2]*b[0],
    a[3]*b[2]+a[0]*b[1]-a[1]*b[0]+a[2]*b[3],
    a[3]*b[3]-a[0]*b[0]-a[1]*b[1]-a[2]*b[2]};
  std::copy(q,q+4,out);
}
inline bool UniformPositive(const OutputMatrix &m) {
  if(!OutputAffine(m)) return false;
  double lengths[3]{};
  for(int a=0;a<3;++a) for(int k=0;k<3;++k) lengths[a]+=m.v[a*4+k]*m.v[a*4+k];
  if(lengths[0]<1e-8 || lengths[0]>1e6) return false;
  for(int a=0;a<3;++a) for(int b=0;b<3;++b) {
    double dot=0; for(int k=0;k<3;++k) dot+=m.v[a*4+k]*m.v[b*4+k];
    if(std::fabs(dot-(a==b?lengths[0]:0))>lengths[0]*.001) return false;
  }
  const double det=m.v[0]*(m.v[5]*m.v[10]-m.v[9]*m.v[6])-
      m.v[4]*(m.v[1]*m.v[10]-m.v[9]*m.v[2])+m.v[8]*(m.v[1]*m.v[6]-m.v[5]*m.v[2]);
  return det>0;
}
inline bool PrebuildReferenceScale(const OutputMatrix &cachedInverse,
    const OutputMatrix &cloth,const OutputMatrix &bone,double (&out)[3]) {
  if(!UniformPositive(cachedInverse)||!UniformPositive(cloth)||!UniformPositive(bone))return false;
  double inverse2=0,current2=0;
  for(int k=0;k<3;++k){inverse2+=cachedInverse.v[k]*cachedInverse.v[k];current2+=cloth.v[k]*cloth.v[k];}
  const double factor=std::sqrt(inverse2*current2);
  for(int axis=0;axis<3;++axis){double length2=0;for(int k=0;k<3;++k)length2+=bone.v[axis*4+k]*bone.v[axis*4+k];out[axis]=std::sqrt(length2)/factor;}
  return PositiveAxes(out[0],out[1],out[2]);
}
inline bool ParentReference(const ReferencePose &local,const OutputMatrix &parent,
    const double parentRotation[4],ReferencePose &out) {
  if(!UniformPositive(parent) || !UnitQuaternion(local.q) || !UnitQuaternion(parentRotation) ||
      !OutputPoint(parent,local.p,out.p)) return false;
  MultiplyQuaternion(parentRotation,local.q,out.q);
  return UnitQuaternion(out.q);
}
inline bool SameReference(const ReferencePose &a,const ReferencePose &b,double tolerance=.00001) {
  if(!UnitQuaternion(a.q) || !UnitQuaternion(b.q)) return false;
  for(int k=0;k<3;++k) if(!std::isfinite(a.p[k]) || !std::isfinite(b.p[k]) ||
      std::fabs(a.p[k]-b.p[k])>tolerance) return false;
  double dot=0; for(int k=0;k<4;++k) dot+=a.q[k]*b.q[k];
  return std::fabs(std::fabs(dot)-1)<.00001;
}
inline bool ConstructionChildren(const std::vector<int> &ids,const std::vector<std::vector<int>> &children,
    const std::vector<int> &excluded,std::vector<std::vector<int>> &out) {
  if(ids.empty() || ids.size()>ClothBoneMaxParticles+1 || children.size()!=ids.size()) return false;
  auto unique=[](std::vector<int> v) {
    std::sort(v.begin(),v.end()); return std::find(v.begin(),v.end(),0)==v.end() &&
        std::adjacent_find(v.begin(),v.end())==v.end();
  };
  if(!unique(ids) || !unique(excluded)) return false;
  for(int id:excluded) if(std::find(ids.begin(),ids.end(),id)!=ids.end()) return false;
  std::vector<std::vector<int>> result(children.size());
  for(size_t n=0;n<children.size();++n) {
    if(children[n].size()>127 || !unique(children[n])) return false;
    for(int child:children[n]) {
      if(child==ids[n]) return false;
      if(std::find(ids.begin(),ids.end(),child)!=ids.end()) result[n].push_back(child);
      else if(std::find(excluded.begin(),excluded.end(),child)==excluded.end()) return false;
    }
  }
  out=std::move(result); return true;
}
inline bool ConnectedStrip(const std::vector<std::array<int,2>> &labels,
    const std::vector<std::array<int,3>> &faces,int columns,int depth,bool loop) {
  if(columns<2 || columns>32 || depth<2 || depth>32 || (loop && columns<3) ||
      columns*depth>ClothBoneMaxParticles || labels.size()!=size_t(columns*depth) ||
      faces.size()!=size_t((loop?columns:columns-1)*(depth-1)*2)) return false;
  std::vector<bool> seen(labels.size());
  for(auto l:labels) {
    if(l[0]<0 || l[0]>=columns || l[1]<0 || l[1]>=depth || seen[l[0]*depth+l[1]]) return false;
    seen[l[0]*depth+l[1]]=true;
  }
  std::vector<std::vector<std::array<int,3>>> cells((loop?columns:columns-1)*(depth-1));
  for(auto t:faces) {
    int loC=columns,hiC=-1,loD=depth,hiD=-1;
    for(int v:t) {
      if(v<0 || size_t(v)>=labels.size()) return false;
      loC=(std::min)(loC,labels[v][0]); hiC=(std::max)(hiC,labels[v][0]);
      loD=(std::min)(loD,labels[v][1]); hiD=(std::max)(hiD,labels[v][1]);
    }
    std::sort(t.begin(),t.end());
    if(t[0]==t[1] || t[1]==t[2] || hiD-loD!=1) return false;
    int column=loC;
    if(loop && loC==0 && hiC==columns-1) column=columns-1;
    else if(hiC-loC!=1) return false;
    for(int v:t) if(labels[v][0]!=loC && labels[v][0]!=hiC) return false;
    cells[column*(depth-1)+loD].push_back(t);
  }
  for(auto &cell:cells) {
    if(cell.size()!=2 || cell[0]==cell[1]) return false;
    std::vector<int> shared,all;
    for(int a:cell[0]) for(int b:cell[1]) if(a==b) shared.push_back(a);
    all.insert(all.end(),cell[0].begin(),cell[0].end()); all.insert(all.end(),cell[1].begin(),cell[1].end());
    std::sort(all.begin(),all.end()); all.erase(std::unique(all.begin(),all.end()),all.end());
    if(all.size()!=4 || shared.size()!=2 || labels[shared[0]][0]==labels[shared[1]][0] ||
        labels[shared[0]][1]==labels[shared[1]][1]) return false;
  }
  return true;
}
inline bool OpenStrip(const std::vector<std::array<int,2>> &labels,
    const std::vector<std::array<int,3>> &faces) { return ConnectedStrip(labels,faces,6,4,false); }
}
