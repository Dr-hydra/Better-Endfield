#pragma once
#include <array>
#include <cmath>
#include <cstddef>
#include <vector>
namespace eiem_cloth_skin {
static constexpr size_t MaxMatrices=384;
struct Influence { int bone; double point[3]; double weight; };
struct Sample { int vertex; bool stable; Influence original[4],candidate[4]; };
struct Edge { int a,b; double rest; };
struct Seam { int a,b; };
using Matrix=std::array<double,16>;
using Point=std::array<double,3>;
struct Result {
  bool valid=true,exceeded=false,seamSeparated=false;
  bool RestoreRequired() const { return !valid || seamSeparated; }
  double stretch=0,relative=0,seamGap=0;
  int a=-1,b=-1,exceededEdges=0,failureA=-1,failureB=-1;
  double failureRest=0,failureSource=0,failureCurrent=0;
};
inline double Length(const Point &a,const Point &b) {
  double sum=0;for(int k=0;k<3;++k) {const auto d=a[k]-b[k];sum+=d*d;}return std::sqrt(sum);
}
inline bool Skin(const Influence (&in)[4],const Matrix *matrices,size_t count,Point &out) {
  out={};double sum=0;int anchor=-1;
  for(const auto &i:in) if(i.weight>0) {anchor=i.bone;break;}
  if(anchor<0 || size_t(anchor)>=count) return false;
  const auto &origin=matrices[anchor];
  for(const auto &i:in) {
    if(!std::isfinite(i.weight) || i.weight<0) return false;
    if(i.weight==0) continue;
    if(i.bone<0 || size_t(i.bone)>=count) return false;
    const auto &m=matrices[i.bone];sum+=i.weight;
    for(int k=0;k<3;++k) out[k]+=i.weight*(m[k]*i.point[0]+m[k+4]*i.point[1]+m[k+8]*i.point[2]+m[k+12]-origin[k+12]);
  }
  if(std::abs(sum-1)>1e-6) return false;
  for(int k=0;k<3;++k) {out[k]+=origin[k+12];if(!std::isfinite(out[k])) return false;}
  return true;
}
inline Result Check(const Sample *samples,size_t count,const Edge *edges,size_t edgeCount,
                    const Seam *seams,size_t seamCount,const Matrix *matrices,size_t matrixCount) {
  Result r{};
  if(!samples || !count || count>8192 || edgeCount>24576 || seamCount>8192 || !matrices || !matrixCount || matrixCount>MaxMatrices) {r.valid=false;return r;}
  std::vector<Point> old(count),now(count);
  for(size_t n=0;n<count;++n) if(!Skin(samples[n].original,matrices,matrixCount,old[n]) ||
      !Skin(samples[n].candidate,matrices,matrixCount,now[n])) {r.valid=false;return r;}
  for(size_t n=0;n<edgeCount;++n) {
    const auto &e=edges[n];
    if(e.a<0 || e.b<0 || size_t(e.a)>=count || size_t(e.b)>=count || !std::isfinite(e.rest) || e.rest<=1e-8) {r.valid=false;return r;}
    const double current=Length(now[e.a],now[e.b]),source=Length(old[e.a],old[e.b]);
    const double stretch=current/e.rest,relative=current/(source>e.rest?source:e.rest);
    if(!std::isfinite(stretch) || !std::isfinite(relative) || !std::isfinite(source)) {r.valid=false;return r;}
    if(stretch>r.stretch) {r.stretch=stretch;r.relative=relative;r.a=samples[e.a].vertex;r.b=samples[e.b].vertex;}
    if(samples[e.a].stable && samples[e.b].stable && stretch>4 && relative>2 && current-source>.02) {
      r.exceeded=true;++r.exceededEdges;
      if(r.failureA<0 || current-source>r.failureCurrent-r.failureSource) {
        r.failureA=samples[e.a].vertex;r.failureB=samples[e.b].vertex;
        r.failureRest=e.rest;r.failureSource=source;r.failureCurrent=current;
      }
    }
  }
  for(size_t n=0;n<seamCount;++n) {
    const auto &s=seams[n];if(s.a<0 || s.b<0 || size_t(s.a)>=count || size_t(s.b)>=count) {r.valid=false;return r;}
    const auto gap=Length(now[s.a],now[s.b]);
    if(!std::isfinite(gap)) {r.valid=false;return r;}
    if(gap>r.seamGap) r.seamGap=gap;
    if(gap>.0005) r.exceeded=r.seamSeparated=true;
  }
  return r;
}
}
