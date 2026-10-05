#pragma once
#include "math/eye_gaze.h"
#include "nlohmann/json.hpp"

namespace eye_gaze {
inline float Number(const nlohmann::json &j,const char *key,float fallback,float lo,float hi) {
  float v=j.value(key,fallback);
  if(!std::isfinite(v)||v<lo||v>hi)throw std::runtime_error("Invalid gaze setting");
  return v;
}
inline Limits ReadLimits(const nlohmann::json &j) {
  if(!j.is_object())throw std::runtime_error("Invalid gaze limits");
  return {Number(j,"left",20,0,30),Number(j,"right",20,0,30),
          Number(j,"up",10,0,20),Number(j,"down",15,0,20)};
}
inline nlohmann::json WriteProfile(Profile p) {
  p=Sanitize(p);
  return {{"camera_yaw",p.cameraYaw},{"camera_pitch",p.cameraPitch},
          {"limits",{{"left",p.limits.left},{"right",p.limits.right},{"up",p.limits.up},{"down",p.limits.down}}}};
}
inline Profile ReadProfile(const nlohmann::json &j) {
  if(!j.is_object())throw std::runtime_error("Invalid gaze profile");
  Profile p;p.cameraYaw=Number(j,"camera_yaw",0,-30,30);p.cameraPitch=Number(j,"camera_pitch",0,-20,20);
  p.limits=ReadLimits(j.at("limits"));return p;
}
struct Reference { bool ready=false,estimatedLimits=false; Vec3 forward; Limits limits; };
inline Reference ReadReference(const nlohmann::json &j) {
  if(!j.is_object()||j.value("version",0)!=1||j.value("method",std::string{})!="pmx-iris-aperture")
    throw std::runtime_error("Invalid gaze reference");
  const auto &f=j.at("forward");if(!f.is_array()||f.size()!=3)throw std::runtime_error("Invalid gaze forward");
  Reference r;r.forward={f[0].get<float>(),f[1].get<float>(),f[2].get<float>()};
  if(!Finite(r.forward)||std::fabs(Len(r.forward)-1)>.01f)throw std::runtime_error("Invalid gaze forward length");
  r.limits=ReadLimits(j.at("limits"));r.estimatedLimits=j.value("estimated",true);r.ready=true;return r;
}
} // namespace eye_gaze
