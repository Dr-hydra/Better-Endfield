#pragma once
#include "math/mmd_retarget.h"
#include <array>

// Foot probes, contact hysteresis and height compensation follow the approach
// in Sasye/EIEM (8b46b76, AGPL-3.0), adapted to Poser's own pose/IK pipeline.
namespace mmd::terrain {
inline bool Finite(Vec3 v){return std::isfinite(v.x)&&std::isfinite(v.y)&&std::isfinite(v.z);}
inline Vec3 Point(const Matrix &m,Vec3 v){return {m.m[0]*v.x+m.m[4]*v.y+m.m[8]*v.z+m.m[12],m.m[1]*v.x+m.m[5]*v.y+m.m[9]*v.z+m.m[13],m.m[2]*v.x+m.m[6]*v.y+m.m[10]*v.z+m.m[14]};}
inline Vec3 Vector(const Matrix &m,Vec3 v){return Point(m,v)-m.position();}
inline float Smooth(float from,float to,float dt,float tau,float speed){return from+Clamp((to-from)*(1-std::exp(-dt/tau)),-speed*dt,speed*dt);}
struct Hit {bool valid=false;Vec3 point,normal{0,1,0};};
struct Plane {bool valid=false;float height=0;Vec3 normal{0,1,0};};
inline Plane Aggregate(const std::array<Hit,5> &hits,Vec3 center,float band) {
  int best=-1,count=0;float nearest=1e30f;
  for(int i=0;i<5;++i)if(hits[i].valid&&Finite(hits[i].point)&&Finite(hits[i].normal)&&hits[i].normal.y>.35f) {
    int members=0;for(const auto &h:hits)if(h.valid&&Finite(h.point)&&h.normal.y>.35f&&std::fabs(h.point.y-hits[i].point.y)<band)++members;
    float d=Len(hits[i].point-center);
    // Prefer the center sample at stair edges instead of inventing a diagonal
    // plane between two stair treads. Never average distinct height clusters.
    if(i==0){best=0;count=members;break;}
    if(members>count||(members==count&&d<nearest)){best=i;count=members;nearest=d;}
  }
  if(best<0)return {};
  Plane p;p.valid=true;Vec3 normal{};float sum=0;int n=0;
  for(const auto &h:hits)if(h.valid&&Finite(h.point)&&Finite(h.normal)&&h.normal.y>.35f&&std::fabs(h.point.y-hits[best].point.y)<band) {
    auto axis=Norm(h.normal);sum+=h.point.y-(axis.x*(center.x-h.point.x)+axis.z*(center.z-h.point.z))/axis.y;normal=normal+axis;++n;
  }
  if(!n)return {};p.height=sum/n;p.normal=Norm(normal);return p;
}
struct Foot {
  bool hasGround=false,contact=false,hasPrevious=false;
  float ground=0,pendingGround=0,pendingSeconds=0,missingSeconds=0,contactSeconds=0;
  Vec3 normal{0,1,0},previous;
};
struct State {
  std::array<Foot,2> feet;
  float rootOffset=0;
  double lastTime=-1,lastMotion=-1;
  uint64_t epoch=0;
};
struct Input {
  std::array<Vec3,2> ankle,hip;
  std::array<float,2> clearance{},legLength{};
  std::array<Plane,2> planes;
  std::array<bool,2> usable{true,true};
  float referenceY=0,strength=1;
  double now=0,motion=0;
  uint64_t epoch=0;
};
struct Output {
  float rootOffset=0;
  std::array<Vec3,2> ankle,normal{{{0,1,0},{0,1,0}}};
  std::array<bool,2> solve{},contact{};
};
inline Output Step(State &s,const Input &in) {
  Output out;out.ankle=in.ankle;
  if(!std::isfinite(in.now)||!std::isfinite(in.motion)||!std::isfinite(in.referenceY))return out;
  bool reset=s.lastTime<0||s.epoch!=in.epoch||in.motion<s.lastMotion-1e-5||in.now<s.lastTime;
  if(reset)s={};
  float dt=s.lastTime<0?1.f/60:Clamp(float(in.now-s.lastTime),0,.1f);
  float sourceDt=s.lastMotion<0?0:float(in.motion-s.lastMotion);
  s.lastTime=in.now;s.lastMotion=in.motion;s.epoch=in.epoch;
  float sum=0;int support=0;bool anyGround=false;
  for(int i=0;i<2;++i) {
    auto &f=s.feet[i];const auto &p=in.planes[i];
    if(!in.usable[i]||!Finite(in.ankle[i])||!Finite(in.hip[i])||in.legLength[i]<.05f){f={};continue;}
    const float len=in.legLength[i],band=Clamp(len*.045f,.025f,.07f);
    if(p.valid&&std::isfinite(p.height)&&Finite(p.normal)&&p.normal.y>.35f&&std::fabs(p.height-in.referenceY)<len*2) {
      f.missingSeconds=0;
      if(!f.hasGround){f.ground=p.height;f.normal=p.normal;f.hasGround=true;}
      else if(std::fabs(p.height-f.ground)<band) {
        f.ground=Smooth(f.ground,p.height,dt,.035f,3);f.pendingSeconds=0;
        f.normal=Norm(f.normal+(p.normal-f.normal)*(1-std::exp(-dt/.08f)));
      } else {
        if(std::fabs(p.height-f.pendingGround)>band){f.pendingGround=p.height;f.pendingSeconds=0;}
        f.pendingSeconds+=dt;
        if(f.pendingSeconds>.04f){f.ground=Smooth(f.ground,p.height,dt,.035f,3);f.normal=p.normal;}
      }
    } else {f.missingSeconds+=dt;if(f.missingSeconds>.15f){f.hasGround=false;f.contact=false;}}
    float lift=in.ankle[i].y-in.referenceY-in.clearance[i];
    float velocity=f.hasPrevious&&sourceDt>1e-6f&&sourceDt<.3f?(in.ankle[i].y-f.previous.y)/sourceDt:0;
    if(sourceDt>1e-6f||!f.hasPrevious){f.previous=in.ankle[i];f.hasPrevious=true;}
    bool closeToFloor=f.hasGround&&lift<(f.contact?.09f:.05f)*len;
    if(!closeToFloor||velocity>len*.8f){f.contact=false;f.contactSeconds=0;}
    else {f.contactSeconds+=dt;if(f.contactSeconds>=.035f||reset)f.contact=true;}
    if(f.contact){sum+=f.ground-in.referenceY;++support;}
    anyGround|=f.hasGround;
    out.contact[i]=f.contact;
  }
  float desired=support?sum/support:s.rootOffset;
  // Lower the shared root when the lower support foot would otherwise exceed
  // leg reach (e.g. one foot on a stair). Keep both original bone lengths.
  for(int i=0;i<2;++i)if(s.feet[i].contact) {
    float dx=in.ankle[i].x-in.hip[i].x,dz=in.ankle[i].z-in.hip[i].z;
    float reach=in.legLength[i]*.997f;
    float vertical=std::sqrt((std::max)(0.f,reach*reach-dx*dx-dz*dz));
    float lift=(std::max)(0.f,in.ankle[i].y-in.referenceY-in.clearance[i]);
    float maximum=s.feet[i].ground+in.clearance[i]+lift+vertical-in.hip[i].y;
    desired=(std::min)(desired,maximum);
  }
  // A brief ray miss holds the previous support; prolonged loss fades out.
  if(!anyGround)desired=0;
  float leg=(std::max)(in.legLength[0],in.legLength[1]);
  desired=Clamp(desired,-leg*1.2f,leg*1.2f);
  s.rootOffset=Smooth(s.rootOffset,desired,dt,.09f,1.5f);
  float strength=Clamp(std::isfinite(in.strength)?in.strength:1.f,0,1);
  out.rootOffset=s.rootOffset*strength;
  for(int i=0;i<2;++i) {
    const auto &f=s.feet[i];auto &goal=out.ankle[i];goal.y+=out.rootOffset;
    if(!in.usable[i]||!f.hasGround)continue;
    float lift=(std::max)(0.f,in.ankle[i].y-in.referenceY-in.clearance[i]);
    float floor=f.ground+in.clearance[i];
    if(f.contact) {
      goal.y=in.ankle[i].y+(floor+lift-in.ankle[i].y)*strength;
      out.normal[i]=Norm(Vec3{0,1,0}+(f.normal-Vec3{0,1,0})*strength);
      out.solve[i]=strength>0;
    } else if(goal.y<floor) {goal.y+=(floor-goal.y)*strength;out.solve[i]=strength>0;}
  }
  return out;
}
inline void Rebuild(const RetargetProfile &p,SampledPose &out) {
  for(size_t i=0;i<p.bones.size();++i) {
    const auto &b=p.bones[i];auto local=TRS(b.localPos,out.localRot[i],b.localScale);
    out.worldMatrix[i]=b.parent<0?local:out.worldMatrix[b.parent]*local;
    out.worldPos[i]=out.worldMatrix[i].position();
    out.worldRot[i]=b.parent<0?out.localRot[i]:NormQ(out.worldRot[b.parent]*out.localRot[i]);
  }
}
inline void SetWorld(const RetargetProfile &p,SampledPose &out,int i,Quat q) {
  int parent=p.bones[i].parent;
  out.localRot[i]=NormQ((parent<0?Quat{}:Conj(out.worldRot[parent]))*q);out.write[i]=true;Rebuild(p,out);
}
inline bool SolveFeet(const RetargetProfile &p,SampledPose &out,const Matrix &model,const Output &targets) {
  Matrix inverse;if(!Inverse(model,inverse)||out.localRot.size()!=p.bones.size())return false;
  const auto modelRotation=Rotation(model);
  for(int side=0;side<2;++side)if(targets.solve[side]) {
    int a=p.roles[1+side],b=p.roles[3+side],c=p.roles[5+side];
    if(a<0||b<0||c<0)continue;
    Vec3 start=out.worldPos[a],knee=out.worldPos[b],ankle=out.worldPos[c],goal=Point(inverse,targets.ankle[side]);
    Vec3 along=Norm(ankle-start),bend=(knee-start)-along*Dot(knee-start,along);
    if(Len(bend)<1e-4f) {
      auto basis=BodyBasis(p.bones[p.roles[1]].restPos,p.bones[p.roles[2]].restPos,p.bones[p.roles[0]].restPos,p.bones[p.roles[10]].restPos);
      bend=basis*Vec3{0,0,1};
    }
    Vec3 pole=start+Norm(bend)*(Len(knee-start)+Len(ankle-knee));
    auto footRotation=out.worldRot[c];Vec3 sa=start,sb=knee,sc=ankle;
    SolveTwoBone(sa,sb,sc,goal,pole,true);
    SetWorld(p,out,a,NormQ(Quat::FromTo(knee-start,sb-sa)*out.worldRot[a]));
    SetWorld(p,out,b,NormQ(Quat::FromTo(out.worldPos[c]-out.worldPos[b],sc-sb)*out.worldRot[b]));
    auto slope=Quat::FromTo({0,1,0},targets.normal[side]);
    float angle=Quat::Angle({},slope);constexpr float limit=.523598776f;
    if(angle>limit)slope=Quat::Slerp({},slope,limit/angle);
    SetWorld(p,out,c,NormQ(Conj(modelRotation)*slope*modelRotation*footRotation));
  }
  return true;
}
} // namespace mmd::terrain
