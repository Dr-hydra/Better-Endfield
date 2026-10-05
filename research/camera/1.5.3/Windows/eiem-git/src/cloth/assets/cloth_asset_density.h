#pragma once
#include "cloth_asset_model.h"
namespace eiem_cloth_asset {
struct ColumnDensityPlan {
  int desired=2,divisions=2;size_t cells=0;double aspect=0;bool budgetLimited=false;
};
inline ColumnDensityPlan PlanColumnDensity(const std::vector<Point> &points,const std::vector<std::vector<int>> &chains,
    const std::vector<int> &attributes,bool loop) {
  Need(chains.size()>=2&&chains.size()<=16&&points.size()==attributes.size()&&points.size()<=128,"auto-density-plan-input");
  for(const auto &p:points)for(double v:p)Need(std::isfinite(v),"auto-density-plan-nonfinite");
  for(int a:attributes)Need(a>=0&&a<=2,"auto-density-plan-attribute");
  std::set<int> seen;
  for(const auto &chain:chains){Need(chain.size()>=2&&chain.size()<=16,"auto-density-plan-chain");
    for(int n:chain)Need(n>=0&&size_t(n)<points.size()&&attributes[n]&&seen.insert(n).second,"auto-density-plan-identity");}
  ColumnDensityPlan plan;size_t inserted=0;const size_t pairs=loop?chains.size():chains.size()-1;
  for(size_t c=0;c<pairs;++c){const auto &a=chains[c],&b=chains[(c+1)%chains.size()];const auto count=(std::min)(a.size(),b.size());inserted+=count;
    for(size_t d=1;d<count;++d){if(attributes[a[d-1]]==1&&attributes[a[d]]==1&&attributes[b[d-1]]==1&&attributes[b[d]]==1)continue;
      const double left=Distance(points[a[d]],points[a[d-1]]),right=Distance(points[b[d]],points[b[d-1]]);
      Need(std::isfinite(left)&&std::isfinite(right)&&left>1e-8&&right>1e-8,"auto-density-plan-degenerate-cell");const double height=left*.5+right*.5;
      const double width=(std::max)(Distance(points[a[d]],points[b[d]]),Distance(points[a[d-1]],points[b[d-1]]));
      Need(std::isfinite(width)&&width>1e-8&&std::isfinite(width/height),"auto-density-plan-degenerate-cell");++plan.cells;plan.aspect=(std::max)(plan.aspect,width/height);}}
  Need(plan.cells>0,"auto-density-plan-no-move-cell");
  while(plan.desired<4&&plan.aspect/plan.desired>1.5)++plan.desired;
  plan.divisions=plan.desired;
  while(plan.divisions>2&&(points.size()+inserted*size_t(plan.divisions-1)>128||chains.size()+pairs*size_t(plan.divisions-1)>32))--plan.divisions;
  plan.budgetLimited=plan.divisions<plan.desired||plan.aspect/plan.divisions>1.5;return plan;
}
}
