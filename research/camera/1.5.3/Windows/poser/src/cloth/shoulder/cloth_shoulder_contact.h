#pragma once
#include "cloth_shoulder_model.h"
#include <vector>
#include <cstdint>
namespace eiem_cloth_shoulder_contact {
using V=std::array<double,3>;
using M=std::array<double,16>;
using Q=eiem_cloth_shoulder::Rotation;
using Pose=eiem_cloth_shoulder::Pose;
inline V Add(V a,V b){for(int k=0;k<3;++k)a[k]+=b[k];return a;}
inline V Sub(V a,V b){for(int k=0;k<3;++k)a[k]-=b[k];return a;}
inline V Scale(V a,double s){for(auto &x:a)x*=s;return a;}
inline double Dot(V a,V b){return a[0]*b[0]+a[1]*b[1]+a[2]*b[2];}
inline V Cross(V a,V b){return {a[1]*b[2]-a[2]*b[1],a[2]*b[0]-a[0]*b[2],a[0]*b[1]-a[1]*b[0]};}
inline double Length(V a){return std::sqrt(Dot(a,a));}
inline bool Unit(V &a){auto l=Length(a);if(!std::isfinite(l)||l<1e-10)return false;a=Scale(a,1/l);return true;}
inline M Identity(){return {1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1};}
inline M Mul(const M &a,const M &b){M r{};for(int j=0;j<4;++j)for(int i=0;i<4;++i)for(int k=0;k<4;++k)r[j*4+i]+=a[k*4+i]*b[j*4+k];return r;}
inline V Vector(const M &m,V p){V q{};for(int i=0;i<3;++i)q[i]=m[i]*p[0]+m[4+i]*p[1]+m[8+i]*p[2];return q;}
inline V Origin(const M &m){return {m[12],m[13],m[14]};}
inline V Point(const M &m,V p){return Add(Vector(m,p),Origin(m));}
inline bool Inverse(const M &m,M &out){
  for(double x:m)if(!std::isfinite(x))return false;
  if(std::abs(m[3])+std::abs(m[7])+std::abs(m[11])+std::abs(m[15]-1)>1e-5)return false;
  V a{m[0],m[1],m[2]},b{m[4],m[5],m[6]},c{m[8],m[9],m[10]};const double det=Dot(a,Cross(b,c));
  if(!std::isfinite(det)||det<1e-10)return false;
  const V rows[]{Scale(Cross(b,c),1/det),Scale(Cross(c,a),1/det),Scale(Cross(a,b),1/det)};
  out=Identity();for(int i=0;i<3;++i){for(int j=0;j<3;++j)out[j*4+i]=rows[i][j];out[12+i]=-Dot(rows[i],Origin(m));}return true;
}
inline Q Multiply(Q a,Q b){return {a[3]*b[0]+b[3]*a[0]+a[1]*b[2]-a[2]*b[1],a[3]*b[1]+b[3]*a[1]+a[2]*b[0]-a[0]*b[2],a[3]*b[2]+b[3]*a[2]+a[0]*b[1]-a[1]*b[0],a[3]*b[3]-a[0]*b[0]-a[1]*b[1]-a[2]*b[2]};}
inline Q Exp(V a){double l=Length(a),s=l>1e-10?std::sin(l*.5)/l:.5;Q q{float(a[0]*s),float(a[1]*s),float(a[2]*s),float(std::cos(l*.5))};eiem_cloth_shoulder::Normalize(q);return q;}
inline V Log(Q q){if(q[3]<0)for(auto &x:q)x=-x;double s=std::sqrt(double(q[0])*q[0]+double(q[1])*q[1]+double(q[2])*q[2]);double a=s>1e-10?2*std::atan2(s,double(q[3]))/s:2;return {q[0]*a,q[1]*a,q[2]*a};}
inline V Rotate(Q q,V v){V a{q[0],q[1],q[2]},t=Scale(Cross(a,v),2);return Add(v,Add(Scale(t,q[3]),Cross(a,t)));}
inline M Transform(const Pose &p){M m=Identity();for(int j=0;j<3;++j){V axis{};axis[j]=1;auto v=Rotate(p.rotation,axis);for(int i=0;i<3;++i)m[j*4+i]=v[i];}for(int k=0;k<3;++k)m[12+k]=p.position[k];return m;}
inline bool Uniform(const M &m){V a{m[0],m[1],m[2]},b{m[4],m[5],m[6]},c{m[8],m[9],m[10]};double l=Length(a);return std::isfinite(l)&&l>1e-5&&l<100&&std::abs(Length(b)-l)<l*1e-4&&std::abs(Length(c)-l)<l*1e-4&&std::abs(Dot(a,b))+std::abs(Dot(a,c))+std::abs(Dot(b,c))<l*l*1e-4&&Dot(a,Cross(b,c))>0;}

struct Influence {uint16_t bone=0;float weight=0;std::array<float,3> local{};};
struct SkinPoint {std::array<Influence,4> influences{};};
struct ContactSample {SkinPoint skin;float margin=0,mass=1;};
struct Bone {const char *name,*parent;M bind;};
struct Side {const SkinPoint *sleeve;size_t sleeveCount;const ContactSample *samples;size_t sampleCount;std::array<int,5> controls,parents;int arm,forearm;};
struct Source {const Bone *bones;size_t boneCount;int chest;std::array<Side,2> sides;};
struct Plane {V normal{};double offset=0;int a=0,b=0,c=0;};
struct Sample {V base{};std::array<V,5> relative{};std::array<double,5> weight{};double margin=0,mass=1;};
struct Stats {unsigned active=0;double before=0,after=0,maxTranslation=0,maxRotation=0;};
struct Workspace {
  std::vector<V> points;
  std::vector<Plane> faces,next,planes;
  std::vector<std::array<int,2>> edges;
  std::vector<Sample> samples;
  std::vector<M> matrices;
  void Reserve(){points.reserve(1024);faces.reserve(4096);next.reserve(4096);planes.reserve(4096);edges.reserve(12288);samples.reserve(256);matrices.reserve(96);}
};
inline bool Face(const std::vector<V> &p,int a,int b,int c,V inside,Plane &out){V n=Cross(Sub(p[b],p[a]),Sub(p[c],p[a]));if(!Unit(n))return false;if(Dot(n,Sub(inside,p[a]))>0){std::swap(b,c);n=Scale(n,-1);}out={n,-Dot(n,p[a]),a,b,c};return true;}
inline double Distance(const Plane &p,V v){return Dot(p.normal,v)+p.offset;}
inline bool Hull(Workspace &w,V axis){
  auto &p=w.points;if(p.size()<4||p.size()>1024||!Unit(axis))return false;
  int a=0,b=0,c=0,d=0;double farthest=0;
  for(size_t n=1;n<p.size();++n)if(p[n][0]<p[a][0])a=int(n);
  for(size_t n=0;n<p.size();++n){double s=Dot(Sub(p[n],p[a]),Sub(p[n],p[a]));if(s>farthest){farthest=s;b=int(n);}}
  farthest=0;for(size_t n=0;n<p.size();++n){double s=Length(Cross(Sub(p[b],p[a]),Sub(p[n],p[a])));if(s>farthest){farthest=s;c=int(n);}}
  V normal=Cross(Sub(p[b],p[a]),Sub(p[c],p[a]));if(!Unit(normal))return false;
  farthest=0;for(size_t n=0;n<p.size();++n){double s=std::abs(Dot(normal,Sub(p[n],p[a])));if(s>farthest){farthest=s;d=int(n);}}if(farthest<1e-7)return false;
  V inside=Scale(Add(Add(p[a],p[b]),Add(p[c],p[d])),.25);w.faces.clear();
  const int initial[][3]{{a,b,c},{a,b,d},{a,c,d},{b,c,d}};
  for(const auto &f:initial){Plane plane;if(!Face(p,f[0],f[1],f[2],inside,plane))return false;w.faces.push_back(plane);}
  for(size_t n=0;n<p.size();++n){if(int(n)==a||int(n)==b||int(n)==c||int(n)==d)continue;w.next.clear();w.edges.clear();
    for(const auto &f:w.faces){if(Distance(f,p[n])<=1e-8){w.next.push_back(f);continue;}
      const int edges[][2]{{f.a,f.b},{f.b,f.c},{f.c,f.a}};
      for(const auto &e:edges){auto it=std::find(w.edges.begin(),w.edges.end(),std::array<int,2>{e[1],e[0]});if(it!=w.edges.end())w.edges.erase(it);else w.edges.push_back({e[0],e[1]});}}
    if(w.edges.empty())continue;
    if(w.next.size()+w.edges.size()>4096)return false;
    for(auto e:w.edges){Plane plane;if(!Face(p,e[0],e[1],int(n),inside,plane))return false;w.next.push_back(plane);}w.faces.swap(w.next);
  }
  w.planes.clear();for(const auto &f:w.faces)if(std::abs(Dot(f.normal,axis))<.55)w.planes.push_back(f);
  return w.planes.size()>=3;
}
inline V Skin(const SkinPoint &p,const std::vector<M> &matrices){V v{};for(const auto &i:p.influences)if(i.weight>0)v=Add(v,Scale(Point(matrices[i.bone],{i.local[0],i.local[1],i.local[2]}),i.weight));return v;}
inline bool Valid(const Source &s){
  if(!s.bones||s.boneCount<3||s.boneCount>96||s.chest<0||size_t(s.chest)>=s.boneCount)return false;
  auto skin=[&](const SkinPoint &p){double sum=0;for(const auto &i:p.influences){if(i.bone>=s.boneCount||!std::isfinite(i.weight)||i.weight<0||!eiem_cloth_shoulder::Finite(i.local))return false;sum+=i.weight;}return std::abs(sum-1)<1e-5;};
  for(const auto &v:s.sides){if(!v.sleeve||!v.samples||v.sleeveCount<4||v.sleeveCount>1024||!v.sampleCount||v.sampleCount>256)return false;
    for(int n:v.controls)if(n<0||size_t(n)>=s.boneCount)return false;for(int n:v.parents)if(n<0||size_t(n)>=s.boneCount)return false;
    if(v.arm<0||v.forearm<0||size_t(v.arm)>=s.boneCount||size_t(v.forearm)>=s.boneCount)return false;
    for(size_t n=0;n<v.sleeveCount;++n)if(!skin(v.sleeve[n]))return false;
    for(size_t n=0;n<v.sampleCount;++n)if(!skin(v.samples[n].skin)||!std::isfinite(v.samples[n].margin)||!std::isfinite(v.samples[n].mass)||v.samples[n].mass<1||v.samples[n].mass>1024)return false;
  }return true;
}
inline bool Solve24(double (&a)[24][24],double (&b)[24],double (&out)[24]){
  double l[24][24]{};for(int i=0;i<24;++i)for(int j=0;j<=i;++j){double v=a[i][j];for(int k=0;k<j;++k)v-=l[i][k]*l[j][k];if(i==j){if(!std::isfinite(v)||v<=1e-12)return false;l[i][j]=std::sqrt(v);}else l[i][j]=v/l[j][j];}
  double y[24]{};for(int i=0;i<24;++i){double v=b[i];for(int k=0;k<i;++k)v-=l[i][k]*y[k];y[i]=v/l[i][i];}
  for(int i=23;i>=0;--i){double v=y[i];for(int k=i+1;k<24;++k)v-=l[k][i]*out[k];out[i]=v/l[i][i];if(!std::isfinite(out[i]))return false;}return true;
}
inline bool Fit(Workspace &w,std::array<V,3> &move,std::array<Q,5> &turn,Stats &stats){
  move={};for(auto &q:turn)q={0,0,0,1};stats={};constexpr double radius=.06,regularizer=.025;
  for(int iter=0;iter<28;++iter){double a[24][24]{},b[24]{},step[24]{};unsigned active=0;
    for(int j=0;j<24;++j)a[j][j]=regularizer;
    for(int j=0;j<3;++j)for(int k=0;k<3;++k)b[j*3+k]=-regularizer*move[j][k];
    for(int j=0;j<5;++j){auto v=Scale(Log(turn[j]),radius);for(int k=0;k<3;++k)b[9+j*3+k]=-regularizer*v[k];}
    for(const auto &s:w.samples){V p=s.base;std::array<V,5> rotated;
      for(int j=0;j<5;++j){rotated[j]=Rotate(turn[j],s.relative[j]);p=Add(p,Sub(rotated[j],s.relative[j]));if(j>=1&&j<=3)p=Add(p,Scale(move[j-1],s.weight[j]));}
      double gap=-1e30;const Plane *plane=nullptr;for(const auto &f:w.planes){auto d=Distance(f,p);if(d>gap){gap=d;plane=&f;}}gap-=s.margin;
      if(iter==0)stats.before=(std::max)(stats.before,-gap);if(gap>=-.0002)continue;++active;
      double jac[24]{};for(int j=0;j<3;++j)for(int k=0;k<3;++k)jac[j*3+k]=s.weight[j+1]*plane->normal[k];
      for(int j=0;j<5;++j){auto v=Scale(Cross(rotated[j],plane->normal),1/radius);for(int k=0;k<3;++k)jac[9+j*3+k]=v[k];}
      for(int j=0;j<24;++j){if(std::abs(jac[j])<1e-12)continue;b[j]-=s.mass*jac[j]*gap;for(int k=0;k<=j;++k)if(std::abs(jac[k])>=1e-12)a[j][k]+=s.mass*jac[j]*jac[k];}
    }
    if(iter==0){stats.active=active;if(!active)return true;}
    if(!Solve24(a,b,step))return false;
    for(int j=0;j<3;++j){V v{step[j*3],step[j*3+1],step[j*3+2]};v=Scale(v,.65*(std::min)(1.,.015/(std::max)(Length(v),1e-10)));move[j]=Add(move[j],v);}
    for(int j=0;j<5;++j){V v{step[9+j*3]/radius,step[10+j*3]/radius,step[11+j*3]/radius};v=Scale(v,.65*(std::min)(1.,.2/(std::max)(Length(v),1e-10)));turn[j]=Multiply(Exp(v),turn[j]);if(!eiem_cloth_shoulder::Normalize(turn[j]))return false;}
  }
  for(const auto &s:w.samples){V p=s.base;for(int j=0;j<5;++j){p=Add(p,Sub(Rotate(turn[j],s.relative[j]),s.relative[j]));if(j>=1&&j<=3)p=Add(p,Scale(move[j-1],s.weight[j]));}double gap=-1e30;for(const auto &f:w.planes)gap=(std::max)(gap,Distance(f,p));stats.after=(std::max)(stats.after,s.margin-gap);}
  for(auto v:move)stats.maxTranslation=(std::max)(stats.maxTranslation,Length(v));for(auto q:turn)stats.maxRotation=(std::max)(stats.maxRotation,Length(Log(q)));return true;
}
inline bool Correct(const Source &source,const std::vector<M> &world,std::array<Pose,10> &target,Workspace &work,std::array<Stats,2> &stats){
  if(world.size()!=source.boneCount||source.chest<0||size_t(source.chest)>=world.size())return false;M inverse;if(!Inverse(world[source.chest],inverse)||!Uniform(world[source.chest]))return false;
  auto &matrices=work.matrices;matrices.resize(world.size());for(size_t n=0;n<world.size();++n){M unused; if(!Inverse(world[n],unused))return false;matrices[n]=Mul(inverse,world[n]);}
  auto out=target;
  for(int side=0;side<2;++side){const auto &s=source.sides[side];std::array<M,5> inverseParent;
    for(int j=0;j<5;++j){const auto &parent=matrices[s.parents[j]];if(!Uniform(parent)||!Inverse(parent,inverseParent[j])||!eiem_cloth_shoulder::Valid(target[side*5+j]))return false;
      const auto local=Mul(inverseParent[j],matrices[s.controls[j]]);
      if(!Uniform(local)||std::abs(Length({local[0],local[1],local[2]})-1)>1e-4)return false;
      matrices[s.controls[j]]=Mul(parent,Transform(target[side*5+j]));}
    work.points.clear();for(size_t n=0;n<s.sleeveCount;++n)work.points.push_back(Skin(s.sleeve[n],matrices));
    if(!Hull(work,Sub(Origin(matrices[s.forearm]),Origin(matrices[s.arm]))))return false;
    work.samples.clear();for(size_t n=0;n<s.sampleCount;++n){const auto &input=s.samples[n];Sample v;v.base=Skin(input.skin,matrices);v.margin=input.margin;v.mass=input.mass;
      for(const auto &term:input.skin.influences)if(term.weight>0)for(int j=0;j<5;++j)if(term.bone==s.controls[j]){v.weight[j]+=term.weight;v.relative[j]=Add(v.relative[j],Scale(Vector(matrices[term.bone],{term.local[0],term.local[1],term.local[2]}),term.weight));}work.samples.push_back(v);}
    std::array<V,3> move;std::array<Q,5> turn;if(!Fit(work,move,turn,stats[side]))return false;
    for(int j=0;j<5;++j){auto &p=out[side*5+j];if(j>=1&&j<=3){auto v=Vector(inverseParent[j],move[j-1]);for(int k=0;k<3;++k)p.position[k]+=float(v[k]);}
      V r=Log(turn[j]);const double scale=Length({matrices[s.parents[j]][0],matrices[s.parents[j]][1],matrices[s.parents[j]][2]});
      r=Scale(Vector(inverseParent[j],r),scale);p.rotation=Multiply(Exp(r),p.rotation);
      if(!eiem_cloth_shoulder::Normalize(p.rotation)||!eiem_cloth_shoulder::Valid(p))return false;}
  }target=out;return true;
}
}
