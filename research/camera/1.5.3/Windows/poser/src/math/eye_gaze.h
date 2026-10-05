#pragma once
#include "math/quat_math.h"
#include <algorithm>

namespace eye_gaze {
enum class Mode { Follow, Manual, Camera };
struct Limits { float left=20,right=20,up=10,down=15; };
// Camera alignment is independent from manual direction and from the authored
// neutral pose. It is measured per model, not inferred from an iris bone pivot.
struct Profile { float cameraYaw=0,cameraPitch=0; Limits limits; };
struct Settings { Mode mode=Mode::Follow; float yaw=0,pitch=0,strength=1; Profile profile; };
struct Basis { Vec3 right,up,forward; bool ready=false; };
inline bool Finite(Vec3 v) {return std::isfinite(v.x)&&std::isfinite(v.y)&&std::isfinite(v.z);}
inline float Limit(float v,float amount) {return std::isfinite(v)?(std::max)(-amount,(std::min)(amount,v)):0;}
inline float SafeLimit(float v,float fallback,float maximum) {
  return std::isfinite(v)?(std::max)(0.f,(std::min)(maximum,v)):fallback;
}
inline Profile Sanitize(Profile p) {
  p.cameraYaw=Limit(p.cameraYaw,30);p.cameraPitch=Limit(p.cameraPitch,20);
  p.limits.left=SafeLimit(p.limits.left,20,30);p.limits.right=SafeLimit(p.limits.right,20,30);
  p.limits.up=SafeLimit(p.limits.up,10,20);p.limits.down=SafeLimit(p.limits.down,15,20);
  return p;
}
inline void BoundAngles(float &yaw,float &pitch,const Limits &limits) {
  Profile p;p.limits=limits;const auto l=Sanitize(p).limits;
  yaw=std::isfinite(yaw)?(std::max)(-l.left,(std::min)(l.right,yaw)):0;
  pitch=std::isfinite(pitch)?(std::max)(-l.down,(std::min)(l.up,pitch)):0;
  // An ellipse also constrains diagonals: two individually safe extremes must
  // not combine into a larger, unsafe corner rotation.
  float h=yaw<0?l.left:l.right,v=pitch<0?l.down:l.up;
  float x=h>0?yaw/h:0,y=v>0?pitch/v:0,r=std::sqrt(x*x+y*y);
  if(r>1){yaw/=r;pitch/=r;}
}
inline Vec3 Direction(const Basis &b,float yaw,float pitch) {
  constexpr float rad=3.14159265358979f/180.f;yaw*=rad;pitch*=rad;
  return b.forward*(std::cos(pitch)*std::cos(yaw))+
      b.right*(std::cos(pitch)*std::sin(yaw))+b.up*std::sin(pitch);
}
inline void Angles(const Basis &b,Vec3 d,float &yaw,float &pitch) {
  constexpr float deg=180.f/3.14159265358979f;
  float x=Dot(d,b.right),y=Dot(d,b.up),z=Dot(d,b.forward);
  yaw=std::atan2(x,z)*deg;pitch=std::atan2(y,std::sqrt(x*x+z*z))*deg;
}
inline Quat ClampDelta(const Basis &b,Quat delta,const Limits &limits) {
  if(!b.ready)return {};
  if(!std::isfinite(QuatLen(delta))||QuatLen(delta)<1e-6f)return {};
  delta=NormQ(delta);Vec3 d=delta*b.forward;
  float yaw,pitch;Angles(b,d,yaw,pitch);BoundAngles(yaw,pitch,limits);
  // Keep the authored twist when only the gaze direction needs correction.
  Vec3 bounded=Direction(b,yaw,pitch);
  // Quat::FromTo intentionally ignores sub-degree rotations for body IK. Eyes
  // need a precise correction or the final limit can leak on small overshoots.
  float dot=(std::max)(-1.f,(std::min)(1.f,Dot(Norm(d),bounded)));
  Vec3 cross=Cross(d,bounded);
  Quat correction=dot>-.99999f?NormQ(Quat{cross.x,cross.y,cross.z,1+dot}):Quat::FromTo(d,bounded);
  return NormQ(correction*delta);
}
// Landmarks are in the same neutral head space. Do not assume the eye bone's
// local axes, or a world-up direction: those differ between character rigs.
inline Basis Calibrate(Vec3 left,Vec3 right,Vec3 mouth,Vec3 head,Vec3 optical={}) {
  Basis b;if(!Finite(left)||!Finite(right)||!Finite(mouth)||!Finite(head))return b;
  Vec3 center=(left+right)*.5f;
  b.right=Norm(right-left);
  if(Finite(optical)&&Len(optical)>1e-5f) {
    b.forward=Norm(optical-b.right*Dot(optical,b.right));
    b.up=Norm(Cross(b.forward,b.right));
    if(Dot(b.up,center-mouth)<0)b.up=b.up*-1.f;
    b.ready=Len(right-left)>1e-5f&&Len(b.forward)>.9f&&Len(b.up)>.9f;
    return b;
  }
  Vec3 up=center-mouth;b.up=Norm(up-b.right*Dot(up,b.right));
  b.forward=Norm(Cross(b.right,b.up));
  float depth=Dot(center-head,b.forward);
  if(Len(right-left)<1e-5f||Len(b.up)<.9f||std::fabs(depth)<1e-5f)return b;
  if(depth<0)b.forward=b.forward*-1.f;
  b.ready=true;return b;
}
inline Quat Aim(const Basis &b,const Settings &settings,Vec3 targetDirection,bool validTarget) {
  if(!b.ready)return {};
  float yaw=0,pitch=0;
  auto profile=Sanitize(settings.profile);
  if(settings.mode==Mode::Camera&&validTarget&&Finite(targetDirection)&&Len(targetDirection)>1e-5f) {
    auto d=Norm(targetDirection);float z=Dot(d,b.forward);
    // A camera behind the head is not a valid fixation. Return to forward
    // instead of flipping the eyes.
    if(z>0) {Angles(b,d,yaw,pitch);yaw+=profile.cameraYaw;pitch+=profile.cameraPitch;}
  }
  if(settings.mode==Mode::Manual){yaw=Limit(settings.yaw,30);pitch=Limit(settings.pitch,20);}
  BoundAngles(yaw,pitch,profile.limits);
  return Quat::FromTo(b.forward,Direction(b,yaw,pitch));
}
inline void AimEyes(const Basis &b,const Settings &settings,Vec3 centerToCamera,
                    const Vec3 (&eyeOffsets)[2],Quat (&out)[2]) {
  const bool front=Finite(centerToCamera)&&Len(centerToCamera)>1e-5f&&Dot(centerToCamera,b.forward)>0;
  out[0]=out[1]=Aim(b,settings,centerToCamera,front);
  if(settings.mode!=Mode::Camera||!b.ready||!front||
     !Finite(eyeOffsets[0])||!Finite(eyeOffsets[1]))return;
  // Apply small binocular convergence on top of the authored neutral eyes.
  // Iris centers are offset from the rotation pivots on stylized characters:
  // pivot->iris is NOT an independent optical axis to forcibly straighten.
  // Keep a minimum focus distance proportional to eye separation so a camera
  // pushed into the face cannot create excessive convergence.
  float span=Len(eyeOffsets[1]-eyeOffsets[0]);
  float distance=(std::max)(Len(centerToCamera),span*10.f);
  if(!std::isfinite(distance)||distance<1e-5f)return;
  Vec3 focus=(out[0]*b.forward)*distance;
  Settings centered;centered.mode=Mode::Camera;centered.profile.limits=settings.profile.limits;
  for(int i=0;i<2;++i)out[i]=Aim(b,centered,focus-eyeOffsets[i],true);
}
} // namespace eye_gaze
