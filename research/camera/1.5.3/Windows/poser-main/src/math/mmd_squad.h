#pragma once
#include "mmd_retarget.h"
#include <array>
namespace mmd {
constexpr size_t SquadSize=4;
struct SquadIdentity {
  uintptr_t squad=0;
  std::array<uintptr_t,SquadSize> entities{},animators{};
  std::array<bool,SquadSize> enabled{};
  bool matches(const SquadIdentity &current) const {
    if(!squad || squad!=current.squad)return false;
    for(size_t n=0;n<SquadSize;++n)if(enabled[n] &&
        (!entities[n] || !animators[n] || entities[n]!=current.entities[n] || animators[n]!=current.animators[n]))return false;
    return true;
  }
};
struct SquadAnchor {
  Vec3 origin;
  Quat basis;
  struct Placement {Vec3 position;Quat rotation;};
  Placement place(Quat actorBasis,Vec3 motionOffset,Vec3 slotOffset,float yawDegrees,bool inPlace=false,float height=0) const {
    Quat heading=NormQ(basis*Quat::FromEulerDeg({0,yawDegrees,0}));
    Quat rotation=NormQ(heading*Conj(actorBasis));
    Vec3 motion=Conj(actorBasis)*motionOffset;
    if(inPlace){motion.x=0;motion.z=0;}
    motion.y+=height;
    return {origin+basis*slotOffset+heading*motion,rotation};
  }
};
inline double SquadDuration(const std::array<double,SquadSize> &seconds,
                            const std::array<bool,SquadSize> &enabled,double camera=0) {
  double result=std::isfinite(camera)?(std::max)(0.,camera):0;
  for(size_t n=0;n<SquadSize;++n)if(enabled[n]&&std::isfinite(seconds[n]))result=(std::max)(result,seconds[n]);
  return result;
}
}
