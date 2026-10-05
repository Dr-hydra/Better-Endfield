#pragma once
namespace eiem_cloth_asset {
struct RibbonSection {double side=0,normal=0;};
inline std::vector<RibbonSection> RibbonSlice(const std::vector<Point> &points,const std::vector<std::array<int,3>> &faces,double along) {
  std::vector<RibbonSection> result;
  for(auto f:faces)for(int k=0;k<3;++k){const auto &a=points[f[k]],&b=points[f[(k+1)%3]];
    if((a[0]<along)==(b[0]<along)||std::abs(b[0]-a[0])<1e-10)continue;
    const double t=(along-a[0])/(b[0]-a[0]);result.push_back({a[2]+(b[2]-a[2])*t,a[1]+(b[1]-a[1])*t});}
  std::sort(result.begin(),result.end(),[](const RibbonSection &a,const RibbonSection &b){return a.side<b.side;});
  for(auto p:result)Need(std::isfinite(p.side)&&std::isfinite(p.normal),"ribbon-nonfinite-section");
  Need(result.size()>=2&&result.front().side<-.002&&result.back().side>.002&&result.back().side-result.front().side<.4,"ribbon-width-section-unconfirmed");return result;
}
inline Point RibbonSurfacePoint(const std::vector<Point> &points,const std::vector<std::array<int,3>> &faces,double along,double side) {
  Point result{};double best=1e30;
  for(auto f:faces){std::vector<Point> hits;for(int k=0;k<3;++k){const auto &a=points[f[k]],&b=points[f[(k+1)%3]];
      if((a[2]<side)==(b[2]<side)||std::abs(b[2]-a[2])<1e-10)continue;const double t=(side-a[2])/(b[2]-a[2]);Point p{};for(int j=0;j<3;++j)p[j]=a[j]+(b[j]-a[j])*t;hits.push_back(p);}
    if(hits.size()!=2)continue;auto a=hits[0],b=hits[1];if(a[0]>b[0])std::swap(a,b);if(b[0]-a[0]<1e-10)continue;
    const double x=(std::max)(a[0],(std::min)(b[0],along)),t=(x-a[0])/(b[0]-a[0]);Point p{x,a[1]+(b[1]-a[1])*t,side};
    const double distance=std::abs(x-along);if(distance<best){best=distance;result=p;}}
  Need(best<.08,"ribbon-surface-endpoint-unconfirmed");return result;
}
}
