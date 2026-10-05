#pragma once
#include "cloth_asset_model.h"

namespace eiem_cloth_asset {
struct FixedTree {
  std::vector<std::vector<int64_t>> chains;
  std::map<int64_t,int> rows;
  size_t depth=0;
  bool irregular=false,forks=false;
};
inline FixedTree SourceFixedTree(Scene &s,const std::vector<int64_t> &roots,
    const std::vector<int64_t> &ids,const std::map<int64_t,int> &columns,
    const std::map<int64_t,int> &attrs,const std::set<int64_t> &passive,Point up,double hipWidth) {
  Need(!roots.empty()&&roots.size()<=16&&ids.size()<=128,"auto-tree-budget");
  FixedTree out;
  for(size_t c=0;c<roots.size();++c){std::vector<int64_t> starts;
    for(auto b:ids)if(columns.at(b)==int(c)&&attrs.at(b)&&!passive.count(b)&&
        (!attrs.count(s.Parent(b))||!attrs.at(s.Parent(b))))starts.push_back(b);
    Need(starts.size()==1&&attrs.at(starts[0])==1,"auto-no-unique-fixed-chain-start");
    for(auto a=starts[0];a!=roots[c];){a=s.Parent(a);Need(columns.count(a)&&columns.at(a)==int(c)&&!attrs.at(a),"auto-active-ancestor-cannot-be-skipped");}
    std::vector<std::vector<int64_t>> pending{{starts[0]}};std::vector<int64_t> longest;
    while(!pending.empty()){
      auto path=std::move(pending.back());pending.pop_back();const auto n=path.back();
      Need(path.size()<=32&&out.rows.emplace(n,int(path.size()-1)).second,"auto-tree-cycle-or-depth-budget");
      std::vector<int64_t> children;for(auto b:s.children[n])if(attrs.count(b)&&attrs.at(b)&&!passive.count(b))children.push_back(b);
      if(children.size()>1){Need(children.size()==2&&attrs.at(n)==1&&attrs.at(children[0])==2&&attrs.at(children[1])==2,"auto-branch-needs-authored-graph");out.forks=true;}
      if(children.empty()){
        Need(path.size()>=2&&attrs.at(n)==2,"auto-chain-needs-fixed-and-move");
        const auto from=Transform(s.World(path.front()),{0,0,0}),to=Transform(s.World(n),{0,0,0});
        Need(Dot(Sub(from,to),up)>hipWidth*.2,"auto-not-descending-skirt-chain");
        if(path.size()>longest.size())longest=path;
      }
      for(auto b:children){Need(attrs.at(b)==2||attrs.at(n)==1,"auto-fixed-after-moving-region");out.irregular|=attrs.at(b)==1;auto next=path;next.push_back(b);pending.push_back(std::move(next));}
    }
    out.irregular|=starts[0]!=roots[c]||(out.depth&&out.depth!=longest.size());out.depth=(std::max)(out.depth,longest.size());out.chains.push_back(std::move(longest));
  }
  for(auto b:ids)Need(!attrs.at(b)||out.rows.count(b)||passive.count(b),"auto-move-below-invalid-ancestor");
  out.irregular|=out.forks||!passive.empty();return out;
}
inline bool UpperCoatAttachment(double height,double distance,double waistTop,double hipWidth,double movingLow) {
  return std::isfinite(height)&&std::isfinite(distance)&&std::isfinite(waistTop)&&std::isfinite(hipWidth)&&std::isfinite(movingLow)&&
      hipWidth>.02&&hipWidth<1&&waistTop>0&&waistTop<1&&height>=waistTop&&height<waistTop+hipWidth&&
      distance<hipWidth*3&&movingLow<-.2*hipWidth;
}
}
