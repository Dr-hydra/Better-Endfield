#pragma once
#include "cloth_asset_model.h"
#include "../bonecloth/cloth_bonecloth_panel_policy.h"
#include "../bonecloth/cloth_bonecloth_apron_policy.h"

namespace eiem_cloth_asset {
struct PanelChart {
  bool open=false;
  Point origin{},up{},side{},forward{};
  double scale=0;
  std::vector<Point> points;
  std::vector<std::array<int,3>> faces;
  std::vector<int> roots;
  static double Wrap(double angle) {
    constexpr double pi=3.14159265358979323846;
    return angle-2*pi*std::floor((angle+pi)/(2*pi));
  }
  Point Coordinate(Point p) const {
    const auto v=Sub(p,origin);const double x=Dot(v,side),z=Dot(v,forward);
    if(open){Need(std::isfinite(x)&&std::isfinite(Dot(v,up)),"apron-chart-nonfinite");return {x,Dot(v,up),0};}
    Need(std::isfinite(x)&&std::isfinite(z)&&x*x+z*z>1e-10,"panel-chart-axis-singularity");
    return {std::atan2(z,x),Dot(v,up),0};
  }
  double WaistHeight(double angle) const {
    if(open) {
      if(angle<=points[roots.front()][0])return points[roots.front()][1];
      for(size_t n=1;n<roots.size();++n)if(angle<=points[roots[n]][0]){const auto a=points[roots[n-1]],b=points[roots[n]];return a[1]+(angle-a[0])/(b[0]-a[0])*(b[1]-a[1]);}
      return points[roots.back()][1];
    }
    for(size_t n=0;n<roots.size();++n){const auto a=points[roots[n]],b=points[roots[(n+1)%roots.size()]];
      const double span=Wrap(b[0]-a[0]),offset=Wrap(angle-a[0]);
      if(std::abs(span)<1e-6)continue;const double t=offset/span;
      if(t>=-1e-9&&t<=1+1e-9)return a[1]+t*(b[1]-a[1]);}
    throw std::runtime_error("panel-chart-waist-not-covered");
  }
  std::vector<Point> Around(Point p) const {
    auto out=points;for(auto &v:out)v[0]=(open?v[0]-p[0]:Wrap(v[0]-p[0]))*scale;return out;
  }
};
inline PanelChart MakeApronChart(const std::vector<Point> &world,const std::vector<std::vector<int>> &chains) {
  Need(chains.size()>=2&&chains.size()<=16&&!world.empty()&&world.size()<=128,"apron-chart-budget");
  PanelChart out;out.open=true;out.scale=1;Point bottom{};
  for(const auto &c:chains){Need(c.size()>=2&&c.size()<=16,"apron-chart-chain");for(int n:c)Need(n>=0&&size_t(n)<world.size(),"apron-chart-index");
    out.roots.push_back(c[0]);for(int k=0;k<3;++k){out.origin[k]+=world[c[0]][k]/chains.size();bottom[k]+=world[c.back()][k]/chains.size();}}
  auto unit=[](Point v){const double length=std::sqrt(Dot(v,v));Need(std::isfinite(length)&&length>.001,"apron-chart-degenerate");for(auto &x:v)x/=length;return v;};
  out.up=unit(Sub(out.origin,bottom));auto side=Sub(world[chains.back()[0]],world[chains.front()[0]]);const double along=Dot(side,out.up);for(int k=0;k<3;++k)side[k]-=out.up[k]*along;
  out.side=unit(side);out.forward=unit(Cross(out.up,out.side));out.points.resize(world.size());
  double previous=-1e100;
  for(const auto &c:chains){double height=1e100;for(int n:c){const auto p=out.Coordinate(world[n]);Need(p[1]<height-1e-5,"apron-chart-reversed-chain");height=p[1];out.points[n]=p;}
    Need(out.points[c[0]][0]>previous+1e-5,"apron-chart-reversed-columns");previous=out.points[c[0]][0];}
  for(size_t c=1;c<chains.size();++c){const auto &a=chains[c-1],&b=chains[c];Need(a.size()==b.size(),"apron-chart-depth");
    for(size_t d=1;d<a.size();++d){out.faces.push_back({a[d-1],b[d-1],a[d]});out.faces.push_back({b[d-1],b[d],a[d]});}}
  return out;
}
inline PanelChart MakePanelChart(const std::vector<Point> &world,const std::vector<std::vector<int>> &chains) {
  Need(chains.size()>=4&&chains.size()<=24&&!world.empty()&&world.size()<=ClothBoneMaxIdentities,"panel-chart-budget");
  size_t active=0;for(const auto &chain:chains)active+=chain.size();
  Need(ClothBoneIdentityBudget(world.size(),active),"panel-chart-active-budget");
  PanelChart chart;Point end{};
  for(const auto &c:chains){Need(c.size()>=2&&c.size()<=16,"panel-chart-chain");
    for(int n:c)Need(n>=0&&size_t(n)<world.size(),"panel-chart-index");
    chart.roots.push_back(c[0]);for(int k=0;k<3;++k){chart.origin[k]+=world[c.front()][k]/chains.size();end[k]+=world[c.back()][k]/chains.size();}}
  auto unit=[](Point p){const double length=std::sqrt(Dot(p,p));Need(std::isfinite(length)&&length>1e-6,"panel-chart-degenerate-frame");for(auto &v:p)v/=length;return p;};
  chart.up=unit(Sub(chart.origin,end));auto radial=Sub(world[chains[0][0]],chart.origin);
  const double along=Dot(radial,chart.up);for(int k=0;k<3;++k)radial[k]-=along*chart.up[k];
  chart.side=unit(radial);chart.forward=unit(Cross(chart.up,chart.side));
  for(const auto &c:chains){double previous=1e100;for(int n:c){const auto v=Sub(world[n],chart.origin);const double h=Dot(v,chart.up);
    Need(h<previous-1e-5,"panel-chart-folded-chain-axis");previous=h;chart.scale+=std::hypot(Dot(v,chart.side),Dot(v,chart.forward));}}
  size_t count=0;for(const auto &c:chains)count+=c.size();chart.scale/=count;Need(chart.scale>.001,"panel-chart-zero-width");
  chart.points.resize(world.size());for(const auto &c:chains)for(int n:c)chart.points[n]=chart.Coordinate(world[n]);
  double winding=0;int direction=0;
  for(size_t c=0;c<chains.size();++c){const auto &a=chains[c],&b=chains[(c+1)%chains.size()];Need(a.size()==b.size(),"panel-chart-unequal-depth");
    const double span=PanelChart::Wrap(chart.points[b[0]][0]-chart.points[a[0]][0]);
    Need(std::abs(span)>1e-5&&std::abs(span)<3.14159265358979323846,"panel-chart-overlapping-columns");
    const int sign=span>0?1:-1;Need(!direction||direction==sign,"panel-chart-reversed-waist");direction=sign;winding+=span;
    for(size_t d=1;d<a.size();++d){chart.faces.push_back({a[d-1],b[d-1],a[d]});chart.faces.push_back({b[d-1],b[d],a[d]});}}
  Need(std::abs(std::abs(winding)-2*3.14159265358979323846)<1e-5,"panel-chart-open-or-multiple-winding");
  return chart;
}
}
