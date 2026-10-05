#pragma once
#include "math/mmd_retarget.h"
#include <array>
#include <cctype>

namespace face_geometry {
constexpr int MaxBones=256;
inline float Clamp(float x,float lo,float hi) {
  return std::isfinite(x)?(std::max)(lo,(std::min)(hi,x)):lo;
}
inline std::string Canonical(std::string s) {
  auto p=s.find_last_of("/:|");if(p!=s.npos)s=s.substr(p+1);
  for(char &c:s)if(static_cast<unsigned char>(c)<128)c=char(std::tolower(static_cast<unsigned char>(c)));
  return s;
}
inline Vec3 Vector(const mmd::Matrix &m,Vec3 v) {
  return {m.m[0]*v.x+m.m[4]*v.y+m.m[8]*v.z,
          m.m[1]*v.x+m.m[5]*v.y+m.m[9]*v.z,
          m.m[2]*v.x+m.m[6]*v.y+m.m[10]*v.z};
}
struct Bone {
  std::string name;
  int parent=-1;
  mmd::Matrix neutral,parentNeutral;
};
} // namespace face_geometry
