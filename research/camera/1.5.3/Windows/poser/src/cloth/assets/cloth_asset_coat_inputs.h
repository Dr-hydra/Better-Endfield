#pragma once
#include "cloth_asset_model.h"
namespace eiem_cloth_asset {
struct CoatFixedSkin {size_t vertices=0;double low=1e100,high=-1e100;};
inline std::vector<int> SeparatedCoatInputs(const std::vector<Point> &points,const std::vector<int> &attributes,
    const std::vector<std::vector<int>> &chains,const std::vector<int> &groups,const std::vector<CoatFixedSkin> &skin,
    Point hip,Point up,double hipWidth) {
  if(points.size()!=attributes.size()||points.size()!=skin.size()||points.size()>128||chains.size()<4||chains.size()>12||
      groups.size()!=chains.size()||!std::isfinite(hipWidth)||hipWidth<=0||std::abs(Dot(up,up)-1)>.0001)return {};
  for(auto p:points)for(auto x:p)if(!std::isfinite(x))return {};
  for(auto x:hip)if(!std::isfinite(x))return {};
  for(auto x:up)if(!std::isfinite(x))return {};
  std::map<int,int> sizes;bool surface=false;std::set<int> seen;
  for(size_t c=0;c<chains.size();++c){if(groups[c]<0||groups[c]>=6||chains[c].size()<3||chains[c].size()>16)return {};
    ++sizes[groups[c]];for(int n:chains[c])if(n<0||size_t(n)>=points.size()||!seen.insert(n).second)return {};}
  for(auto v:sizes)surface|=v.second>=2;if(sizes.size()<2||!surface)return {};
  std::vector<int> result;
  for(size_t c=0;c<chains.size();++c){const auto &chain=chains[c];if(sizes[groups[c]]!=1)continue;
    const int root=chain[0],joint=chain[1];if(attributes[root]!=1||attributes[joint]!=1)continue;
    bool movable=true;for(size_t d=2;d<chain.size();++d)movable&=attributes[chain[d]]==2;if(!movable)continue;
    const double top=Dot(Sub(points[root],hip),up),height=Dot(Sub(points[joint],hip),up);const auto &visible=skin[joint];
    if(top<hipWidth*.15||top>hipWidth*2||height>=-hipWidth*.1||height<-hipWidth||
        top-height<hipWidth*.5||top-height>hipWidth*2||visible.vertices<8||
        !std::isfinite(visible.low)||!std::isfinite(visible.high)||visible.low>visible.high||
        visible.high>top-hipWidth*.15||visible.low>=-hipWidth*.25)continue;
    result.push_back(joint);
  }
  if(result.size()>6)return {};return result;
}
}
