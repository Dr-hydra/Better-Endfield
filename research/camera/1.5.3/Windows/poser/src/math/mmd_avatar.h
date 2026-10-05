#pragma once
#include "math/mmd_calibration.h"
#include <map>
#include <sstream>

namespace mmd {
struct AvatarBone {Vec3 position;Quat rotation;Vec3 scale{1,1,1};};
using AvatarSkeleton=std::map<std::string,std::vector<AvatarBone>>;
// Required ancestor paths must be complete and unambiguous. Props and facial
// nodes outside the humanoid body do not make a usable Avatar fail calibration.
inline bool CalibrateAvatar(RetargetProfile &profile,const AvatarSkeleton &skeleton,std::string &error) {
  auto result=profile;std::vector<bool> required(result.bones.size());
  for(size_t i=0;i<result.bones.size();++i) {
    if(result.bones[i].parent>=int(i)||result.bones[i].parent < -1){error="Invalid live hierarchy";return false;}
    if(BodyCalibrationRole(result.bones[i].role))
      for(int j=int(i);j>=0;j=result.bones[j].parent)required[j]=true;
  }
  uint64_t fingerprint=14695981039346656037ull;
  auto hash=[&](const void *v,size_t size){auto bytes=(const unsigned char*)v;while(size--){fingerprint^=*bytes++;fingerprint*=1099511628211ull;}};
  for(size_t i=0;i<result.bones.size();++i) {
    auto &b=result.bones[i];
    if(b.parent<0){b.localPos={};b.localRot={};b.localScale={1,1,1};b.calibrated=true;continue;}
    auto found=skeleton.find(b.name);
    if(found==skeleton.end()||found->second.size()!=1) {
      if(required[i]){error="Missing/ambiguous Avatar bone: "+b.name;return false;}
      continue;
    }
    const auto &a=found->second.front();
    for(float f:{a.position.x,a.position.y,a.position.z,a.rotation.x,a.rotation.y,a.rotation.z,a.rotation.w,a.scale.x,a.scale.y,a.scale.z})
      if(!std::isfinite(f)){error="Non-finite Avatar bone: "+b.name;return false;}
    if(QuatLen(a.rotation)<.5f||a.scale.x<=1e-5f||a.scale.y<=1e-5f||a.scale.z<=1e-5f){error="Degenerate Avatar bone: "+b.name;return false;}
    b.localPos=a.position;b.localRot=NormQ(a.rotation);b.localScale=a.scale;b.calibrated=true;
    if(required[i]) {
      hash(b.name.data(),b.name.size());hash(&b.role,sizeof(b.role));
      const auto &parent=result.bones[b.parent].name;hash(parent.data(),parent.size());
      hash(&b.localPos,sizeof(b.localPos));hash(&b.localRot,sizeof(b.localRot));hash(&b.localScale,sizeof(b.localScale));
    }
  }
  result.globals();if(!result.valid()){error="Incomplete humanoid Avatar";return false;}
  std::ostringstream id;id<<"avatar1-"<<std::hex<<fingerprint;result.fingerprint=id.str();
  profile=std::move(result);error.clear();return true;
}
} // namespace mmd
