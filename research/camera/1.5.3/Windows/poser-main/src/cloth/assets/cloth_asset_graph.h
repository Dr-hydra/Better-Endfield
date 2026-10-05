#pragma once
#include "cloth_asset_model.h"
#include "../resources/cloth_bonecloth_cache.h"

namespace eiem_cloth_asset {
struct Graph {std::vector<std::array<int,3>> faces;std::vector<std::array<int,2>> lines;};
struct PanelOrder {std::vector<int> order,groups;};
inline PanelOrder SeparatedPanelOrder(const std::vector<Point> &roots,const std::set<std::pair<int,int>> &pairs,Point origin,Point up,Point side,bool retainSingleLines=false,bool useSourceChainOrder=false) {
  Need(roots.size()>=4&&roots.size()<=12,"auto-separated-panel-root-budget");
  for(auto p:roots)for(auto v:p)Need(std::isfinite(v),"auto-separated-nonfinite-root");
  std::vector<std::vector<int>> adjacent(roots.size());for(auto e:pairs){Need(e.first>=0&&e.second>e.first&&size_t(e.second)<roots.size(),"auto-separated-panel-edge");adjacent[e.first].push_back(e.second);adjacent[e.second].push_back(e.first);}
  std::vector<int> group(roots.size(),-1);std::vector<std::vector<int>> components;
  for(int n=0;n<int(roots.size());++n)if(group[n]<0){const int id=int(components.size());std::vector<int> q{n};group[n]=id;
    for(size_t k=0;k<q.size();++k)for(int next:adjacent[q[k]])if(group[next]<0){group[next]=id;q.push_back(next);}Need(q.size()>=2||retainSingleLines,"auto-separated-single-chain-leg-coverage-unconfirmed");components.push_back(q);}
  Need(components.size()>=2&&components.size()<=ClothBoneMaxSeparatedPanels,"auto-separated-panel-count");
  auto unit=[](Point p){const double l=std::sqrt(Dot(p,p));Need(std::isfinite(l)&&l>.001,"auto-separated-panel-axis");for(auto &x:p)x/=l;return p;};
  up=unit(up);const double along=Dot(side,up);for(int k=0;k<3;++k)side[k]-=along*up[k];side=unit(side);const auto forward=Cross(up,side);PanelOrder result;
  constexpr double pi=3.14159265358979323846;
  for(size_t c=0;c<components.size();++c){if(components[c].size()==1){result.order.push_back(components[c][0]);result.groups.push_back(int(c));continue;}std::vector<std::pair<double,int>> angles;
    if(useSourceChainOrder) {
      // Some coats fold around roots at different heights. Their actual skin
      // adjacency is a unique open path even when azimuth sorting crosses it.
      int start=-1,ends=0;
      for(int n:components[c]){Need(adjacent[n].size()==1||adjacent[n].size()==2,"auto-separated-source-chain-branched");
        if(adjacent[n].size()==1){++ends;if(start<0||n<start)start=n;}}
      Need(ends==2,"auto-separated-source-chain-not-open");
      std::set<int> seen;int previous=-1,current=start;
      while(current>=0){Need(seen.insert(current).second,"auto-separated-source-chain-cycle");
        result.order.push_back(current);result.groups.push_back(int(c));int next=-1;
        for(int n:adjacent[current])if(n!=previous)next=n;previous=current;current=next;}
      Need(seen.size()==components[c].size(),"auto-separated-source-chain-incomplete");continue;
    }
    for(int n:components[c]){const auto p=Sub(roots[n],origin);const double x=Dot(p,side),z=Dot(p,forward);Need(std::isfinite(x)&&std::isfinite(z)&&std::hypot(x,z)>.005,"auto-separated-panel-axis-root");angles.push_back({std::atan2(z,x),n});}
    std::sort(angles.begin(),angles.end());double largest=0;size_t start=0;
    for(size_t k=0;k<angles.size();++k){double gap=angles[(k+1)%angles.size()].first-angles[k].first;if(gap<=0)gap+=2*pi;if(gap>largest){largest=gap;start=(k+1)%angles.size();}}
    Need(largest>pi+.05,"auto-separated-panel-not-open-arc");
    for(size_t k=0;k<angles.size();++k){const int n=angles[(start+k)%angles.size()].second;
      if(k){const int prev=angles[(start+k-1)%angles.size()].second;Need(pairs.count({(std::min)(prev,n),(std::max)(prev,n)})!=0,"auto-separated-panel-missing-neighbour");}
      result.order.push_back(n);result.groups.push_back(int(c));}
  }
  return result;
}
inline double Angle(Point a,Point b) {
  const double length=std::sqrt(Dot(a,a)*Dot(b,b));
  Need(std::isfinite(length)&&length>1e-12,"auto-native-graph-degenerate-angle");
  return std::acos((std::max)(-1.,(std::min)(1.,Dot(a,b)/length)))*180./3.14159265358979323846;
}
inline Graph NativeGraph(const std::vector<Point> &p,const std::vector<ClothBoneAsset> &bones,int columns,bool loop,bool reverse,
    eiem_cloth_graph::OrderContract *contract=nullptr) {
  Need(p.size()==bones.size()&&p.size()>=4&&ClothBoneIdentityBudget(bones.size(),
      size_t(std::count_if(bones.begin(),bones.end(),[](const ClothBoneAsset &b){return b.attribute!=0;})))&&
      columns>=2&&columns<=32,"auto-native-graph-budget");
  std::set<std::array<int,3>> faces;std::set<std::array<int,2>> adjacent;
  for(int v=0;v<int(p.size());++v) {
    if(!bones[v].attribute)continue;std::vector<int> neighbors;
    for(int n=0;n<int(p.size());++n) {
      if(!bones[n].attribute||n==v)continue;
      const int d=std::abs(bones[n].column-bones[v].column);
      if(bones[n].parent==v||bones[v].parent==n||(bones[n].depth==bones[v].depth&&(d<=1||(loop&&d==columns-1)))) {
        neighbors.push_back(n);adjacent.insert({(std::min)(n,v),(std::max)(n,v)});
      }
    }
    for(size_t i=0;i<neighbors.size();++i)for(size_t j=i+1;j<neighbors.size();++j) {
      const int a=neighbors[i],b=neighbors[j];
      if(std::set<int>{bones[v].column,bones[a].column,bones[b].column}.size()>2)continue;
      if(bones[a].parent!=v&&bones[v].parent!=a&&bones[b].parent!=v&&bones[v].parent!=b&&bones[a].parent!=b&&bones[b].parent!=a)continue;
      if(Distance(p[a],p[v])<.001||Distance(p[b],p[v])<.001)continue;
      if(Angle(Sub(p[a],p[v]),Sub(p[b],p[v]))<120.) {
        std::array<int,3> f{v,a,b};std::sort(f.begin(),f.end());faces.insert(f);
      }
    }
  }
  std::map<std::array<int,2>,std::vector<std::array<int,3>>> edgeFaces;
  std::map<std::array<int,3>,int> faceIndices;
  std::map<std::array<int,4>,std::vector<eiem_cloth_graph::Choice>> choices;
  if(contract){*contract={};contract->source.assign(faces.begin(),faces.end());contract->adjacent.assign(adjacent.begin(),adjacent.end());
    for(size_t n=0;n<contract->source.size();++n)faceIndices.emplace(contract->source[n],int(n));}
  for(const auto &f:faces)for(int i=0;i<3;++i)for(int j=i+1;j<3;++j)edgeFaces[{f[i],f[j]}].push_back(f);
  std::vector<std::array<int,2>> edges;for(const auto &e:edgeFaces)edges.push_back(e.first);if(reverse)std::reverse(edges.begin(),edges.end());
  std::set<std::array<int,4>> used;std::set<std::array<int,3>> removed;
  for(const auto &e:edges) {
    const auto &fs=edgeFaces[e];const int u=e[0],v=e[1];
    for(size_t i=0;i<fs.size();++i)for(size_t j=i+1;j<fs.size();++j) {
      int a=-1,b=-1;for(int x:fs[i])if(x!=u&&x!=v)a=x;for(int x:fs[j])if(x!=u&&x!=v)b=x;
      const auto axis=Sub(p[v],p[u]),pa=Sub(p[a],p[u]),pb=Sub(p[b],p[u]);
      if(Angle(Cross(pa,axis),Cross(axis,pb))>20.)continue;
      const auto q=Sub(p[a],p[b]);const double aa=Dot(axis,axis),bb=Dot(q,q),ab=Dot(axis,q),det=aa*bb-ab*ab;
      if(det<=aa*bb*1e-12)continue;
      const double s=(Dot(axis,pa)*bb-Dot(q,pa)*ab)/det,t=(Dot(q,pa)*aa-Dot(axis,pa)*ab)/det;
      if(!(s>0&&s<1&&t>0&&t<1))continue;
      std::array<int,4> quad{u,v,a,b};std::sort(quad.begin(),quad.end());
      if(contract)choices[quad].push_back({e,{faceIndices.at(fs[i]),faceIndices.at(fs[j])}});
      if(!used.insert(quad).second){removed.insert(fs[i]);removed.insert(fs[j]);}
    }
  }
  Graph result;std::set<std::array<int,2>> covered;
  for(const auto &f:faces)if(!removed.count(f)) {
    result.faces.push_back(f);for(int i=0;i<3;++i)for(int j=i+1;j<3;++j)covered.insert({f[i],f[j]});
  }
  Need(!result.faces.empty()&&result.faces.size()<=ClothBoneMaxFaces,"auto-native-graph-face-budget");
  for(auto e:adjacent)if(!covered.count(e))result.lines.push_back(e);
  if(contract){for(auto &r:choices)if(r.second.size()>1)contract->rules.push_back({r.first,std::move(r.second)});
    Need(eiem_cloth_graph::Valid(*contract),"auto-native-graph-order-contract");}
  return result;
}
inline void GraphVariants(eiem_cloth_cache::Profile &out,const std::vector<Point> &points) {
  out.faces.clear();out.lines.clear();out.graphs.clear();
  out.graphOrder={};
  auto bones=out.bones;
  if(!out.candidateAttributes.empty())for(size_t n=0;n<bones.size();++n)bones[n].attribute=out.candidateAttributes[n];
  for(auto n:out.ignored)bones[n].attribute=0;
  for(bool reverse:{false,true}) {
    auto g=NativeGraph(points,bones,int(out.roots.size()),out.view.loop,reverse,
        out.view.runtimeFixedForks&&!reverse?&out.graphOrder:nullptr);
    if(!out.faces.empty()&&out.faces[0]==g.faces&&out.lines[0]==g.lines)continue;
    out.faces.push_back(std::move(g.faces));out.lines.push_back(std::move(g.lines));out.graphs.emplace_back();
  }
  out.Link();
}
}
