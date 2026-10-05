#pragma once
#include "cloth_asset_model.h"
#include "../collision/cloth_collision_math.h"
namespace eiem_cloth_asset {
struct LegRegion {Point from,to;};
struct LegReachPlan {unsigned required=0,listed=0,centerlineCovered=0;double low=0,high=0;};
inline LegReachPlan PlanLegReach(const std::vector<Point> &garment,Point origin,Point up,
    const std::array<LegRegion,4> &legs) {
  Need(!garment.empty()&&garment.size()<=200000,"auto-leg-garment-budget");
  for(auto v:origin)Need(std::isfinite(v),"auto-leg-origin-nonfinite");
  const double norm=std::sqrt(Dot(up,up));Need(std::isfinite(norm)&&norm>1e-8,"auto-leg-frame");for(auto &v:up)v/=norm;
  LegReachPlan p;p.low=1e100;p.high=-1e100;
  for(auto v:garment){for(auto x:v)Need(std::isfinite(x),"auto-leg-nonfinite-garment");const double h=Dot(Sub(v,origin),up);p.low=(std::min)(p.low,h);p.high=(std::max)(p.high,h);}
  for(int n=0;n<4;++n){const auto &s=legs[n];const double length=Distance(s.from,s.to);Need(std::isfinite(length)&&length>1e-5,"auto-leg-degenerate-segment");
    double a=Dot(Sub(s.from,origin),up),b=Dot(Sub(s.to,origin),up);if(a>b)std::swap(a,b);
    if((std::min)(b,p.high)-(std::max)(a,p.low)>length*.01)p.required|=1u<<n;}
  return p;
}
inline eiem_collision::Capsule SourceWorldCapsule(const Matrix &m,Point center,Point size,int axis,bool reverse,bool separate,bool centered) {
  for(auto v:m)Need(std::isfinite(v),"auto-leg-capsule-nonfinite-matrix");
  Need(std::abs(m[3])+std::abs(m[7])+std::abs(m[11])+std::abs(m[15]-1)<1e-10,"auto-leg-capsule-nonaffine");
  using namespace eiem_collision;Need(axis>=0&&axis<3,"auto-leg-capsule-direction");
  const V x{m[0],m[1],m[2]},y{m[4],m[5],m[6]},z{m[8],m[9],m[10]};const double scale=Length(x);
  Need(UniformBasis(x,y,z,scale),"auto-leg-capsule-nonuniform-frame");V direction{};if(axis==0)direction.x=1;else if(axis==1)direction.y=1;else direction.z=1;
  auto c=LocalCapsule({center[0],center[1],center[2]},direction,{size[0],size[1],size[2]},reverse,separate,centered);Need(c.valid,"auto-leg-capsule-invalid");
  const auto a=Transform(m,{c.a.x,c.a.y,c.a.z}),b=Transform(m,{c.b.x,c.b.y,c.b.z});c.a={a[0],a[1],a[2]};c.b={b[0],b[1],b[2]};c.ra*=scale;c.rb*=scale;return c;
}
inline double LegCapsuleGap(Point point,const eiem_collision::Capsule &c) {
  using namespace eiem_collision;Need(c.valid&&Finite(c.a)&&Finite(c.b)&&c.ra>0&&c.rb>0,"auto-leg-capsule-gap-input");
  const V p{point[0],point[1],point[2]};Need(Finite(p),"auto-leg-capsule-gap-nonfinite");
  auto gap=[&](double t){return Length(p-(c.a+(c.b-c.a)*t))-(c.ra+(c.rb-c.ra)*t);};
  double lo=0,hi=1;for(int n=0;n<36;++n){double a=(lo*2+hi)/3,b=(lo+hi*2)/3;if(gap(a)<gap(b))hi=b;else lo=a;}
  return (std::min)({gap(0),gap(1),gap((lo+hi)*.5)});
}
inline bool LegCenterlineCovered(const LegRegion &leg,const std::vector<eiem_collision::Capsule> &shapes,
    const std::vector<eiem_collision::Capsule> &jointShapes={}) {
  if(shapes.empty()||!std::isfinite(Distance(leg.from,leg.to))||Distance(leg.from,leg.to)<=1e-5)return false;
  for(int n=0;n<=12;++n){const double t=double(n)/12;Point p{};for(int k=0;k<3;++k)p[k]=leg.from[k]*(1-t)+leg.to[k]*t;
    bool covered=false;for(const auto &c:shapes)covered|=LegCapsuleGap(p,c)<=0;
    // Adjacent torso contact can cover the hip pivot outside the thigh capsule.
    // Only actual source volumes count; do not inflate geometry or allow gaps.
    for(const auto &c:jointShapes)covered|=LegCapsuleGap(p,c)<=0;
    if(!covered)return false;}
  return true;
}
}
