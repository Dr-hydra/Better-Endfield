#pragma once
#include "../bonecloth/cloth_bonecloth_reference.h"

namespace eiem_cloth_response {
using eiem_cloth_surface::OutputMatrix;
using eiem_cloth_surface::OutputMultiply;
struct Frame {
  int target=-1,count=0;
  int controls[3]{-1,-1,-1};
  double weights[3]{};
  OutputMatrix offsets[3]{};
};
inline bool Rotation(const OutputMatrix &m,double (&q)[4]) {
  if(!eiem_cloth_rebuild::UniformPositive(m))return false;
  const double scale=std::sqrt(m.v[0]*m.v[0]+m.v[1]*m.v[1]+m.v[2]*m.v[2]);
  const double a=m.v[0]/scale,b=m.v[5]/scale,c=m.v[10]/scale;
  if(a+b+c>0){const double v=2*std::sqrt(1+a+b+c);q[3]=v/4;q[0]=(m.v[6]-m.v[9])/scale/v;q[1]=(m.v[8]-m.v[2])/scale/v;q[2]=(m.v[1]-m.v[4])/scale/v;}
  else {int k=a>b?(a>c?0:2):(b>c?1:2),j=(k+1)%3,l=(k+2)%3;
    const double v=2*std::sqrt(1+m.v[k*4+k]/scale-m.v[j*4+j]/scale-m.v[l*4+l]/scale);
    if(v<1e-8)return false;q[k]=v/4;q[j]=(m.v[k*4+j]+m.v[j*4+k])/scale/v;
    q[l]=(m.v[k*4+l]+m.v[l*4+k])/scale/v;q[3]=(m.v[j*4+l]-m.v[l*4+j])/scale/v;}
  return eiem_cloth_rebuild::UnitQuaternion(q);
}
inline bool Valid(const Frame &f,int original,int total) {
  if(f.target<0||f.target>=original||original<1||total<=original||
      !ClothBoneIdentityBudget(total,total-original)||f.count<1||f.count>3)return false;
  double sum=0;
  for(int k=0;k<f.count;++k){if(f.controls[k]<original||f.controls[k]>=total||!std::isfinite(f.weights[k])||f.weights[k]<=0||f.weights[k]>1||
      !eiem_cloth_rebuild::UniformPositive(f.offsets[k]))return false;
    for(int j=0;j<k;++j)if(f.controls[k]==f.controls[j])return false;sum+=f.weights[k];}
  return std::abs(sum-1)<1e-8;
}
inline bool Map(const Frame &f,int original,const std::vector<OutputMatrix> &world,OutputMatrix &out) {
  if(!Valid(f,original,int(world.size())))return false;
  OutputMatrix mapped[3];double q[3][4]{},reference[4]{},sum[4]{},p[3]{},scale=0;int major=0;
  for(int k=1;k<f.count;++k)if(f.weights[k]>f.weights[major])major=k;
  for(int k=0;k<f.count;++k){if(!eiem_cloth_rebuild::UniformPositive(world[f.controls[k]]))return false;
    mapped[k]=OutputMultiply(world[f.controls[k]],f.offsets[k]);if(!Rotation(mapped[k],q[k]))return false;}
  std::copy(q[major],q[major]+4,reference);
  for(int k=0;k<f.count;++k){double dot=0;for(int j=0;j<4;++j)dot+=reference[j]*q[k][j];
    for(int j=0;j<4;++j)sum[j]+=f.weights[k]*(dot<0?-q[k][j]:q[k][j]);
    for(int j=0;j<3;++j)p[j]+=f.weights[k]*mapped[k].v[12+j];
    const double s=std::sqrt(mapped[k].v[0]*mapped[k].v[0]+mapped[k].v[1]*mapped[k].v[1]+mapped[k].v[2]*mapped[k].v[2]);
    if(k&&std::abs(s-scale)>scale*.001)return false;scale=s;}
  double norm=0;for(double v:sum)norm+=v*v;if(norm<1e-8||!std::isfinite(norm))return false;
  float rotation[4]{},axes[]{float(scale),float(scale),float(scale)};for(int j=0;j<4;++j)rotation[j]=float(sum[j]/std::sqrt(norm));
  return eiem_cloth_surface::OutputCompletedTRS(p,rotation,axes,out);
}
inline bool Relative(const OutputMatrix &parent,const OutputMatrix &world,OutputMatrix &out) {
  if(!eiem_cloth_rebuild::UniformPositive(parent)||!eiem_cloth_rebuild::UniformPositive(world))return false;
  OutputMatrix inverse{};const double s2=parent.v[0]*parent.v[0]+parent.v[1]*parent.v[1]+parent.v[2]*parent.v[2];
  for(int c=0;c<3;++c)for(int r=0;r<3;++r)inverse.v[c*4+r]=parent.v[r*4+c]/s2;
  for(int r=0;r<3;++r)for(int k=0;k<3;++k)inverse.v[12+r]-=inverse.v[k*4+r]*parent.v[12+k];
  inverse.v[15]=1;out=OutputMultiply(inverse,world);return eiem_cloth_rebuild::UniformPositive(out);
}
}
