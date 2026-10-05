#pragma once
#include "math/quat_math.h"
#include <algorithm>
#include <array>
#include <cstring>
#include <functional>
#include <map>
#include <string>
#include <vector>
#include <stdexcept>

namespace face_math {
// Column-major, like Unity's Matrix4x4 value type.
struct Matrix {
  float m[16] = {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1};
  Vec3 position() const { return {m[12], m[13], m[14]}; }
};
inline Matrix operator*(const Matrix &a, const Matrix &b) {
  Matrix o;
  for (int c = 0; c < 4; c++)
    for (int r = 0; r < 4; r++) {
      o.m[c * 4 + r] = 0;
      for (int k = 0; k < 4; k++)
        o.m[c * 4 + r] += a.m[k * 4 + r] * b.m[c * 4 + k];
    }
  return o;
}
inline bool Inverse(const Matrix &a, Matrix &out) {
  double t[4][8] = {};
  for (int r = 0; r < 4; r++)
    for (int c = 0; c < 4; c++) {
      t[r][c] = a.m[c * 4 + r];
      t[r][c + 4] = r == c ? 1 : 0;
    }
  for (int c = 0; c < 4; c++) {
    int p = c;
    for (int r = c + 1; r < 4; r++)
      if (std::fabs(t[r][c]) > std::fabs(t[p][c]))
        p = r;
    if (std::fabs(t[p][c]) < 1e-10)
      return false;
    for (int j = 0; j < 8; j++)
      std::swap(t[p][j], t[c][j]);
    double d = t[c][c];
    for (int j = 0; j < 8; j++)
      t[c][j] /= d;
    for (int r = 0; r < 4; r++)
      if (r != c) {
        double s = t[r][c];
        for (int j = 0; j < 8; j++)
          t[r][j] -= s * t[c][j];
      }
  }
  for (int r = 0; r < 4; r++)
    for (int c = 0; c < 4; c++) {
      out.m[c * 4 + r] = float(t[r][c + 4]);
      if (!std::isfinite(out.m[c * 4 + r]))
        return false;
    }
  return true;
}
inline Quat Basis(Vec3 x, Vec3 y, Vec3 z) {
  float trace = x.x + y.y + z.z;
  Quat q;
  if (trace > 0) {
    float s = std::sqrt(trace + 1) * 2;
    q = {(y.z - z.y) / s, (z.x - x.z) / s, (x.y - y.x) / s, .25f * s};
  } else if (x.x > y.y && x.x > z.z) {
    float s = std::sqrt(1 + x.x - y.y - z.z) * 2;
    q = {.25f * s, (y.x + x.y) / s, (z.x + x.z) / s, (y.z - z.y) / s};
  } else if (y.y > z.z) {
    float s = std::sqrt(1 + y.y - x.x - z.z) * 2;
    q = {(y.x + x.y) / s, .25f * s, (z.y + y.z) / s, (z.x - x.z) / s};
  } else {
    float s = std::sqrt(1 + z.z - x.x - y.y) * 2;
    q = {(z.x + x.z) / s, (z.y + y.z) / s, .25f * s, (x.y - y.x) / s};
  }
  return NormQ(q);
}
inline Quat Rotation(const Matrix &m) {
  Vec3 x = Norm({m.m[0], m.m[1], m.m[2]}), y = Norm({m.m[4], m.m[5], m.m[6]});
  Vec3 z = Norm(Cross(x, y));
  y = Norm(Cross(z, x));
  return Basis(x, y, z);
}
inline Matrix TRS(Vec3 p, Quat q, Vec3 scale = {1, 1, 1}) {
  Matrix m;
  Vec3 axes[] = {q * Vec3{scale.x, 0, 0}, q * Vec3{0, scale.y, 0},
                 q * Vec3{0, 0, scale.z}};
  for (int c = 0; c < 3; ++c) {
    m.m[c * 4] = axes[c].x;
    m.m[c * 4 + 1] = axes[c].y;
    m.m[c * 4 + 2] = axes[c].z;
  }
  m.m[12] = p.x;
  m.m[13] = p.y;
  m.m[14] = p.z;
  return m;
}
inline std::string Name(std::string s) {
  const char *from[] = {u8"０", u8"１", u8"２", u8"３", u8"４", u8"５",
                        u8"６", u8"７", u8"８", u8"９", u8"Ｉ", u8"Ｋ"};
  const char *to[] = {"0", "1", "2", "3", "4", "5",
                      "6", "7", "8", "9", "I", "K"};
  for (size_t i = 0; i < 12; i++) {
    size_t p = 0;
    while ((p = s.find(from[i], p)) != s.npos) {
      s.replace(p, std::strlen(from[i]), to[i]);
      p++;
    }
  }
  return s;
}
}
