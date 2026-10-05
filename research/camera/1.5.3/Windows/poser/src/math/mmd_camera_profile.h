#pragma once
#include "mmd_camera.h"
#include "mmd_retarget.h"
#include "nlohmann/json.hpp"

namespace mmd {
// Compare head-to-ankle spans, not hair, props, heels or an animated bounding box.
inline float CameraTargetHeight(const RetargetProfile &p) {
  for(int role:{0,5,6,10})
    if(p.roles[role]<0 || p.roles[role]>=int(p.bones.size()) || !p.bones[p.roles[role]].calibrated)return 0;
  Vec3 head=p.bones[p.roles[10]].restPos,hip=p.bones[p.roles[0]].restPos;
  Vec3 feet=(p.bones[p.roles[5]].restPos+p.bones[p.roles[6]].restPos)*.5f;
  float span=Dot(head-feet,Norm(head-hip));
  return std::isfinite(span)&&span>.1f&&span<5?span:0;
}
inline float CameraSourceHeight(const RigDefinition &rig) {
  int head=rig.find(u8"頭"),left=rig.find(u8"左足首"),right=rig.find(u8"右足首");
  if(head<0||left<0||right<0)return 15.6f;
  float span=rig.bones[head].rest.y-(rig.bones[left].rest.y+rig.bones[right].rest.y)*.5f;
  return std::isfinite(span)&&span>1&&span<100?span:15.6f;
}
inline nlohmann::json WriteCameraSettings(const CameraSettings &s) {
  return {{"version",1},{"enabled",s.enabled},{"origin",int(s.origin)},
    {"cut_mode",int(s.cutMode)},{"cut_frames",s.cutFrames},{"follow_vertical",s.followVertical},
    {"offset",{s.offset.x,s.offset.y,s.offset.z}},{"scale",s.scale},{"link_scale",s.linkScale},
    {"distance_scale",s.distanceScale},{"yaw",s.yaw},{"fov_offset",s.fovOffset},
    {"auto_height",s.autoHeight},{"height_scale",s.heightScale},{"reference_height",s.referenceHeight},
    {"time_offset",s.timeOffset},{"follow_correction",s.followCorrection}};
}
inline CameraSettings ReadCameraSettings(const nlohmann::json &j) {
  if(!j.is_object()||j.value("version",0)!=1)throw std::runtime_error("Unsupported camera settings");
  CameraSettings s;
  auto number=[&](const char *key,float fallback,float lo,float hi) {
    float v=j.value(key,fallback);
    if(!std::isfinite(v)||v<lo||v>hi)throw std::runtime_error("Invalid camera setting");
    return v;
  };
  int origin=j.value("origin",0),cuts=j.value("cut_mode",0);
  if(origin<0||origin>1||cuts<0||cuts>2)throw std::runtime_error("Invalid camera mode");
  s.origin=CameraOrigin(origin);s.cutMode=CameraCutMode(cuts);
  s.enabled=j.value("enabled",true);s.followVertical=j.value("follow_vertical",true);
  s.linkScale=j.value("link_scale",true);s.autoHeight=j.value("auto_height",true);
  s.followCorrection=j.value("follow_correction",true);
  s.scale=number("scale",.08f,.001f,1);s.distanceScale=number("distance_scale",1,.05f,10);
  s.yaw=number("yaw",0,-180,180);s.fovOffset=number("fov_offset",0,-120,120);
  s.heightScale=number("height_scale",1,.25f,4);s.referenceHeight=number("reference_height",0,0,100);
  s.timeOffset=number("time_offset",0,-600,600);
  auto xyz=j.value("offset",std::vector<float>{0,0,0});
  if(xyz.size()!=3)throw std::runtime_error("Invalid camera offset");
  for(float x:xyz)if(!std::isfinite(x)||std::fabs(x)>100)throw std::runtime_error("Invalid camera offset");
  s.offset={xyz[0],xyz[1],xyz[2]};
  if(j.contains("cut_frames")) {
    const auto &frames=j.at("cut_frames");
    if(!frames.is_array()||frames.size()>4096)throw std::runtime_error("Too many camera cuts");
    for(const auto &f:frames) {
      if(!f.is_number_integer()||f.get<int64_t>()<0||f.get<int64_t>()>100000000)throw std::runtime_error("Invalid camera cut");
      s.cutFrames.push_back(f.get<uint32_t>());
    }
    std::sort(s.cutFrames.begin(),s.cutFrames.end());
    s.cutFrames.erase(std::unique(s.cutFrames.begin(),s.cutFrames.end()),s.cutFrames.end());
  }
  return s;
}
// Content identity survives moving a camera file; no local material paths in presets.
inline std::string CameraTrackId(const std::vector<CameraKey> &keys) {
  if(keys.empty())return {};
  uint64_t hash=14695981039346656037ull;
  auto add=[&](const auto &v){auto p=reinterpret_cast<const uint8_t*>(&v);for(size_t i=0;i<sizeof(v);++i){hash^=p[i];hash*=1099511628211ull;}};
  for(const auto &k:keys) {
    add(k.frame);add(k.distance);add(k.target.x);add(k.target.y);add(k.target.z);
    add(k.rotation.x);add(k.rotation.y);add(k.rotation.z);add(k.fov);add(k.perspective);
    for(const auto &c:k.curves){add(c.x1);add(c.y1);add(c.x2);add(c.y2);}
  }
  return std::to_string(hash);
}
} // namespace mmd
