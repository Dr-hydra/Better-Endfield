#pragma once
#include <algorithm>
#include <array>
#include <cmath>
#include <cstddef>
#include <limits>
namespace eiem_cloth_shoulder {
constexpr size_t Features=7, Controls=5, Neighbors=8;
using Feature=std::array<float,Features>;
using Point=std::array<float,3>;
using Rotation=std::array<float,4>;
struct Pose {Point position{};Rotation rotation{0,0,0,1};};
struct Sample {Feature feature{};std::array<Pose,Controls> pose{};};
constexpr float FeatureScale[Features]{.65f,.65f,1.f,1.f,.6f,.2f,.15f};
constexpr const char *FeatureNames[Features]{"Shoulder Down-Up","Shoulder Front-Back","Arm Down-Up",
    "Arm Front-Back","Arm Twist In-Out","Forearm Stretch","Forearm Twist In-Out"};
template<size_t N> inline bool Finite(const std::array<float,N> &a) {for(float x:a)if(!std::isfinite(x))return false;return true;}
inline double Dot(const Rotation &a,const Rotation &b){double r=0;for(size_t k=0;k<4;++k)r+=double(a[k])*b[k];return r;}
inline bool Normalize(Rotation &q){const double n=Dot(q,q);if(!std::isfinite(n)||n<1e-12)return false;for(float &x:q)x=float(x/std::sqrt(n));return true;}
inline bool Valid(const Pose &p){return Finite(p.position)&&Finite(p.rotation)&&std::abs(Dot(p.rotation,p.rotation)-1)<.002;}
inline bool Same(const Point &a,const Point &b){if(!Finite(a)||!Finite(b))return false;for(size_t k=0;k<3;++k)if(std::abs(a[k]-b[k])>1e-5f)return false;return true;}
inline bool Same(const Rotation &a,const Rotation &b){return Finite(a)&&Finite(b)&&std::abs(Dot(a,b))/std::sqrt(Dot(a,a)*Dot(b,b))>.999999;}
inline Pose Blend(const Pose &a,const Pose &b,float t){Pose p;for(size_t k=0;k<3;++k)p.position[k]=a.position[k]+(b.position[k]-a.position[k])*t;
  const float sign=Dot(a.rotation,b.rotation)<0?-1.f:1.f;for(size_t k=0;k<4;++k)p.rotation[k]=a.rotation[k]+(sign*b.rotation[k]-a.rotation[k])*t;Normalize(p.rotation);return p;}
inline Pose Mirror(Pose p){p.position[2]=-p.position[2];p.rotation[0]=-p.rotation[0];p.rotation[1]=-p.rotation[1];return p;}
struct Result {std::array<Pose,Controls> pose{};float nearest=0;size_t sample=0;};
inline bool Validate(const Sample *samples,size_t count){if(!samples||count<Neighbors||count>4096)return false;
  for(size_t n=0;n<count;++n){if(!Finite(samples[n].feature))return false;for(const auto &p:samples[n].pose)if(!Valid(p))return false;}return true;}
inline bool Evaluate(const Sample *samples,size_t count,const Feature &feature,bool right,Result &result){
  if(!samples||count<Neighbors||count>4096||!Finite(feature))return false;
  for(float v:feature)if(std::abs(v)>20)return false;
  std::array<double,Neighbors> distance;distance.fill(std::numeric_limits<double>::infinity());std::array<size_t,Neighbors> index{};
  for(size_t n=0;n<count;++n){double d=0;for(size_t j=0;j<Features;++j){const double v=double(feature[j])*FeatureScale[j]-samples[n].feature[j];d+=v*v;}
    if(d>=distance.back())continue;size_t k=Neighbors-1;while(k&&d<distance[k-1]){distance[k]=distance[k-1];index[k]=index[k-1];--k;}distance[k]=d;index[k]=n;}
  if(!std::isfinite(distance[0]))return false;
  const double sigma=.04+.3*std::sqrt(distance[0]);std::array<double,Neighbors> w{};double sum=0;
  for(size_t k=0;k<Neighbors;++k){const double span=distance.back()-distance[0];
    const double taper=span>1e-12?(distance.back()-distance[k])/span:1;
    w[k]=taper*taper*std::exp(-(distance[k]-distance[0])/(sigma*sigma));sum+=w[k];}
  Result out{};out.nearest=float(std::sqrt(distance[0]));out.sample=index[0];
  for(size_t b=0;b<Controls;++b){auto &p=out.pose[b];p.rotation={0,0,0,0};const auto &q0=samples[index[0]].pose[b].rotation;
    for(size_t k=0;k<Neighbors;++k){const auto &v=samples[index[k]].pose[b];const double weight=w[k]/sum,sign=Dot(q0,v.rotation)<0?-1:1;
      for(size_t j=0;j<3;++j)p.position[j]+=float(weight*v.position[j]);for(size_t j=0;j<4;++j)p.rotation[j]+=float(sign*weight*v.rotation[j]);}
    if(!Normalize(p.rotation))return false;if(right)p=Mirror(p);if(!Valid(p))return false;}
  result=out;return true;
}
struct Lease {Pose original{},last{},attempt{};bool position=false,rotation=false,pendingPosition=false,pendingRotation=false;
  void Capture(const Pose &p){original=last=attempt=p;position=rotation=pendingPosition=pendingRotation=false;}
  bool Uncontested(const Pose &p)const{return Same(p.position,last.position)&&Same(p.rotation,last.rotation);}
  bool RestorePosition(const Pose &p)const{return (position&&Same(p.position,last.position))||(pendingPosition&&Same(p.position,attempt.position));}
  bool RestoreRotation(const Pose &p)const{return (rotation&&Same(p.rotation,last.rotation))||(pendingRotation&&Same(p.rotation,attempt.rotation));}
};
}
