#pragma once
#include "cloth_asset_model.h"
namespace eiem_cloth_asset {
struct CoatWaistField {
  struct Face {std::array<Point,3> points;std::array<std::array<double,4>,3> skin;};
  struct Node {Point low{},high{};int start=0,count=0,left=-1,right=-1;};
  std::vector<Face> faces;std::vector<int> order;std::vector<Node> nodes;
  int BuildNode(int start,int count) {
    Node node;node.start=start;node.count=count;node.low={1e100,1e100,1e100};node.high={-1e100,-1e100,-1e100};
    for(int n=start;n<start+count;++n)for(auto p:faces[order[n]].points)for(int k=0;k<3;++k){node.low[k]=(std::min)(node.low[k],p[k]);node.high[k]=(std::max)(node.high[k],p[k]);}
    const int id=int(nodes.size());nodes.push_back(node);
    if(count>8){int axis=0;for(int k=1;k<3;++k)if(node.high[k]-node.low[k]>node.high[axis]-node.low[axis])axis=k;
      const int mid=start+count/2;std::nth_element(order.begin()+start,order.begin()+mid,order.begin()+start+count,[&](int a,int b){
        double x=0,y=0;for(int k=0;k<3;++k){x+=faces[a].points[k][axis];y+=faces[b].points[k][axis];}return x!=y?x<y:a<b;});
      const int left=BuildNode(start,mid-start),right=BuildNode(mid,start+count-mid);nodes[id].left=left;nodes[id].right=right;}
    return id;
  }
  void Build() {
    Need(!faces.empty()&&faces.size()<=16384,"coat-waist-field-budget");order.clear();nodes.clear();
    for(size_t n=0;n<faces.size();++n){const auto &f=faces[n];for(int k=0;k<3;++k){double sum=0;
      for(auto v:f.points[k])Need(std::isfinite(v),"coat-waist-field-position");
      for(double w:f.skin[k]){Need(std::isfinite(w)&&w>=0&&w<=1,"coat-waist-field-weight");sum+=w;}
      Need(std::abs(sum-1)<1e-6,"coat-waist-field-partition");}order.push_back(int(n));}
    BuildNode(0,int(order.size()));
  }
  static double BoxDistance(const Node &n,Point p) {double d=0;for(int k=0;k<3;++k){const double v=(std::max)({n.low[k]-p[k],0.,p[k]-n.high[k]});d+=v*v;}return d;}
  static std::pair<double,std::array<double,3>> Closest(const Face &f,Point p) {
    const auto a=f.points[0],u=Sub(f.points[1],a),v=Sub(f.points[2],a),w=Sub(p,a);
    const double uu=Dot(u,u),vv=Dot(v,v),uv=Dot(u,v),det=uu*vv-uv*uv;
    std::pair<double,std::array<double,3>> best{1e100,{}};
    if(det>1e-12*uu*vv){const double b=(Dot(w,u)*vv-Dot(w,v)*uv)/det,c=(Dot(w,v)*uu-Dot(w,u)*uv)/det;
      if(b>=0&&c>=0&&b+c<=1){auto d=w;for(int k=0;k<3;++k)d[k]-=b*u[k]+c*v[k];best={Dot(d,d),{1-b-c,b,c}};}}
    for(int k=0;k<3;++k){const int j=(k+1)%3;const auto e=Sub(f.points[j],f.points[k]),d=Sub(p,f.points[k]);const double length=Dot(e,e);
      const double t=length>1e-20?(std::max)(0.,(std::min)(1.,Dot(d,e)/length)):0.;auto error=d;for(int axis=0;axis<3;++axis)error[axis]-=t*e[axis];
      const double distance=Dot(error,error);if(distance<best.first){best={distance,{}};best.second[k]=1-t;best.second[j]=t;}}
    return best;
  }
  std::array<double,4> At(Point p,double radius) const {
    Need(!nodes.empty()&&std::isfinite(radius)&&radius>0,"coat-waist-field-unbuilt");for(auto v:p)Need(std::isfinite(v),"coat-waist-field-query");
    double best=radius*radius;int face=-1;std::array<double,3> bary{};std::array<int,32> stack{};int size=1;
    while(size){const auto &node=nodes[stack[--size]];if(BoxDistance(node,p)>best)continue;
      if(node.left<0){for(int n=node.start;n<node.start+node.count;++n){const int id=order[n];const auto q=Closest(faces[id],p);
        if(q.first<best||(q.first==best&&(face<0||id<face))){best=q.first;face=id;bary=q.second;}}}
      else {Need(size+2<=int(stack.size()),"coat-waist-field-query-budget");int nearChild=node.left,farChild=node.right;
        if(BoxDistance(nodes[nearChild],p)>BoxDistance(nodes[farChild],p))std::swap(nearChild,farChild);stack[size++]=farChild;stack[size++]=nearChild;}}
    std::array<double,4> out{};if(face>=0)for(int k=0;k<3;++k)for(int b=0;b<4;++b)out[b]+=bary[k]*faces[face].skin[k][b];return out;
  }
};
struct CoatWaistChart {
  Point spine{},up{};double height=0;
  std::map<int,std::pair<Point,double>> fixed;
  std::set<int> trunk;
  std::array<int,4> trunkBindings{{-1,-1,-1,-1}};const CoatWaistField *field=nullptr;
};
inline double CoatWaistSmooth(double a,double b,double x) {
  Need(std::isfinite(x)&&a<b,"coat-waist-nonfinite-coordinate");
  const double t=(std::max)(0.,(std::min)(1.,(x-a)/(b-a)));return t*t*(3-2*t);
}
inline std::map<int,double> CoatWaistSkin(const CoatWaistChart &chart,Point natural,
    const std::map<int,double> &source,bool packed) {
  Need(std::isfinite(chart.height)&&chart.height>.001&&
      std::abs(Dot(chart.up,chart.up)-1)<1e-6,"coat-waist-invalid-frame");
  for(auto t:source)Need(t.first>=0&&std::isfinite(t.second)&&t.second>0,"coat-waist-invalid-weight");
  auto result=source;
  const double lower=CoatWaistSmooth(-.5,-.2,Dot(Sub(natural,chart.spine),chart.up)/chart.height);
  double moved=0;
  for(auto t:source){const auto f=chart.fixed.find(t.first);if(f==chart.fixed.end())continue;
    Need(!chart.trunk.count(t.first)&&std::isfinite(f->second.second)&&f->second.second>.001,"coat-waist-invalid-root-span");
    const double depth=Dot(Sub(f->second.first,natural),chart.up)/f->second.second;
    const double keep=t.second*(1-CoatWaistSmooth(.1,.65,depth)*lower);
    const double remaining=packed?std::round(keep):double(float(keep));
    Need(remaining>=0&&remaining<=t.second,"coat-waist-invalid-transfer");
    moved+=t.second-remaining;if(remaining)result[t.first]=remaining;else result.erase(t.first);
  }
  if(moved<=0)return source;Need(chart.field,"coat-waist-field-missing");
  const auto support=chart.field->At(natural,chart.height*.75);double supportSum=0;
  for(int n=0;n<4;++n){supportSum+=support[n];if(support[n]>1e-8){const int b=chart.trunkBindings[n];
    if(b<0)return source;Need(chart.trunk.count(b)&&!chart.fixed.count(b),"coat-waist-field-binding");result[b]+=moved*support[n];}}
  if(supportSum<.5)return source;Need(std::abs(supportSum-1)<1e-6,"coat-waist-support-partition");
  std::vector<std::pair<double,int>> attachment;size_t protectedCount=0;double budget=0;
  for(auto t:source)if(chart.fixed.count(t.first)||chart.trunk.count(t.first))budget+=t.second;else ++protectedCount;
  for(auto t:result)if(chart.fixed.count(t.first)||chart.trunk.count(t.first))attachment.push_back({t.second,t.first});
  std::sort(attachment.begin(),attachment.end(),[](auto a,auto b){return a.first!=b.first?a.first>b.first:a.second<b.second;});
  for(size_t n=4-protectedCount;n<attachment.size();++n)result.erase(attachment[n].second);
  double fixed=0,body=0;for(auto t:result){if(chart.fixed.count(t.first))fixed+=t.second;else if(chart.trunk.count(t.first))body+=t.second;}
  if(body<=0)return source;
  std::vector<std::pair<double,int>> fractions;double used=0;const double bodyBudget=budget-fixed;
  for(auto &t:result)if(chart.trunk.count(t.first)){const double value=bodyBudget*t.second/body;
    t.second=packed?std::floor(value):double(float(value));used+=t.second;fractions.push_back({value-std::floor(value),t.first});}
  if(packed){std::sort(fractions.begin(),fractions.end(),[](auto a,auto b){return a.first!=b.first?a.first>b.first:a.second<b.second;});
    const int rest=int(std::llround(bodyBudget-used));Need(rest>=0&&size_t(rest)<=fractions.size(),"coat-waist-quantization");for(int n=0;n<rest;++n)++result[fractions[n].second];
    for(auto t:result)if(t.second>65535){Need(chart.trunk.count(t.first)&&t.second==65536&&result.size()<4,"coat-waist-weight-overflow");
      auto f=std::find_if(source.begin(),source.end(),[&](auto s){return chart.fixed.count(s.first)&&s.second>=1;});Need(f!=source.end(),"coat-waist-overflow-attachment");result[t.first]=65535;result[f->first]+=1;break;}}
  for(auto i=result.begin();i!=result.end();)if(!i->second)i=result.erase(i);else {Need(i->second>0&&i->second<=(packed?65535.:1.0001),"coat-waist-weight-overflow");++i;}
  Need(result.size()<=4,"coat-waist-influence-budget");
  return result;
}
}
