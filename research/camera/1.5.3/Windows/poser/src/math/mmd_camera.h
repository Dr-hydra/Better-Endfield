#pragma once
#include "mmd_motion.h"

namespace mmd {
inline CameraKey SampleCamera(const std::vector<CameraKey> &keys, double frame,
                              bool cuts = true) {
  if (keys.empty()) return {};
  const auto n = Upper(keys, frame);
  if (!n) return keys.front();
  if (n == keys.size()) return keys.back();
  const auto &a = keys[n - 1], &b = keys[n];
  // Adjacent MMD keys commonly mark a cut; keep it crisp at higher game FPS.
  if (cuts && b.frame - a.frame == 1) return a;
  float t = float((frame - a.frame) / double(b.frame - a.frame));
  auto mix = [&](float x, float y, int curve) { return x + (y - x) * Bezier(b.curves[curve], t); };
  CameraKey out = a;
  out.target = {mix(a.target.x,b.target.x,0),mix(a.target.y,b.target.y,1),mix(a.target.z,b.target.z,2)};
  // Preserve authored Euler turns rather than taking a quaternion shortcut.
  out.rotation = {mix(a.rotation.x,b.rotation.x,3),mix(a.rotation.y,b.rotation.y,3),mix(a.rotation.z,b.rotation.z,3)};
  out.distance = mix(a.distance,b.distance,4);
  out.fov = mix(a.fov,b.fov,5);
  return out; // Projection is a step track.
}
enum class CameraOrigin { File = 0, Follow = 1 };
enum class CameraCutMode { Adjacent = 0, Continuous = 1, Manual = 2 };
struct CameraSettings {
  bool enabled = true, followVertical = true;
  CameraOrigin origin = CameraOrigin::File;
  CameraCutMode cutMode = CameraCutMode::Adjacent;
  std::vector<uint32_t> cutFrames;
  Vec3 offset; // Character/MMD axes, in game world units after scaling
  float scale = .08f, distanceScale = 1, yaw = 0, fovOffset = 0;
  bool linkScale = true;
  bool autoHeight = true, followCorrection = true;
  float heightScale = 1, referenceHeight = 0, timeOffset = 0;
};
inline double CameraFrame(double seconds, const CameraSettings &s) {
  return (std::max)(0., (seconds - s.timeOffset) * 30.);
}
inline double CameraDuration(const std::vector<CameraKey> &keys, const CameraSettings &s) {
  return keys.empty() ? 0 : (std::max)(0., keys.back().frame / 30. + s.timeOffset);
}
inline CameraKey SampleCamera(const std::vector<CameraKey> &keys, double frame,
                              const CameraSettings &s) {
  if (s.cutMode == CameraCutMode::Manual && !keys.empty()) {
    auto n=Upper(keys,frame);
    if(n && n<keys.size() && std::find(s.cutFrames.begin(),s.cutFrames.end(),keys[n].frame)!=s.cutFrames.end())
      return keys[n-1];
  }
  return SampleCamera(keys,frame,s.cutMode==CameraCutMode::Adjacent);
}
inline float CameraUnitScale(const CameraSettings &s,float motionScale,
                             float targetHeight=0,float sourceHeight=0) {
  float scale=Clamp(s.linkScale?motionScale:s.scale,.001f,1.f);
  float reference=s.referenceHeight>0?s.referenceHeight:sourceHeight;
  if(s.autoHeight && std::isfinite(targetHeight) && targetHeight>.1f && targetHeight<5 &&
     std::isfinite(reference) && reference>1 && reference<100)
    scale=targetHeight/reference;
  return Clamp(scale*Clamp(s.heightScale,.25f,4.f),.001f,1.f);
}
struct CameraPose {
  Vec3 position, target;
  Quat rotation;
  float fov = 30, orthoSize = 1;
  bool perspective = true;
};
inline Quat CameraOrbit(Vec3 e) {
  // MMD camera orbit: negative Y, then negative X, then negative Z.
  return NormQ(Quat::AxisAngle({0,1,0},-e.y) *
               Quat::AxisAngle({1,0,0},-e.x) * Quat::AxisAngle({0,0,1},-e.z));
}
inline CameraPose PlaceCamera(const CameraKey &key, const CameraSettings &settings,
                              Vec3 start, Quat sourceToWorld, Vec3 characterDelta,
                              float motionScale, float targetHeight=0,float sourceHeight=0,
                              Vec3 placementCorrection={}) {
  const float scale = CameraUnitScale(settings,motionScale,targetHeight,sourceHeight);
  const float distance = key.distance * scale * Clamp(settings.distanceScale,.05f,10.f);
  Quat basis = NormQ(sourceToWorld * Quat::AxisAngle({0,1,0},settings.yaw * .0174532925199433f));
  Vec3 origin = start;
  characterDelta=characterDelta-placementCorrection;
  if (settings.origin == CameraOrigin::Follow) {
    if (!settings.followVertical) characterDelta.y = 0;
    origin = origin + characterDelta;
  }
  if(settings.followCorrection) origin=origin+placementCorrection;
  CameraPose pose;
  pose.target = origin + basis * (key.target * scale) + sourceToWorld * settings.offset;
  pose.rotation = NormQ(basis * CameraOrbit(key.rotation));
  pose.position = pose.target + pose.rotation * Vec3{0,0,distance};
  pose.perspective = key.perspective;
  if (!pose.perspective && distance > 1e-5f)
    pose.rotation = NormQ(pose.rotation * Quat::AxisAngle({0,1,0},3.141592653589793f));
  pose.fov = Clamp(key.fov + settings.fovOffset,1.f,179.f);
  // MMD's orthographic vertical span is 25 units at distance 45.
  pose.orthoSize = (std::max)(.001f,25.f * std::fabs(distance) / 90.f);
  return pose;
}
} // namespace mmd
