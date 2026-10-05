#pragma once
#include <cstdint>
#include <cstddef>
#include <vector>
#include <array>
#include <algorithm>
#include <cmath>
namespace eiem_cloth_surface {
template<size_t N> inline bool SameSimplices(
    std::vector<std::array<int,N>> actual, std::vector<std::array<int,N>> expected) {
  if (actual.size() != expected.size()) return false;
  for (auto &row : actual) {
    std::sort(row.begin(), row.end());
    for (size_t k = 1; k < N; ++k) if (row[k] == row[k-1]) return false;
  }
  for (auto &row : expected) std::sort(row.begin(), row.end());
  std::sort(actual.begin(), actual.end()); std::sort(expected.begin(), expected.end());
  return std::adjacent_find(actual.begin(),actual.end()) == actual.end() && actual == expected;
}
inline bool CanMutate(bool main, unsigned depth, bool exactCallsite, int animator,
                      int cross, bool fingerprint, bool currentManager) {
  return main && depth == 1 && exactCallsite && animator == 0 && cross == 1 &&
      fingerprint && currentManager;
}
inline uint64_t RenderHash(const void *data, size_t bytes) {
  uint64_t h = 14695981039346656037ULL;
  const auto *p = static_cast<const unsigned char *>(data);
  for (size_t n = 0; n < bytes; ++n) h = (h ^ p[n])*1099511628211ULL;
  return h;
}
struct OutputMatrix { double v[16]{}; };
inline bool OutputAffine(const OutputMatrix &m) {
  for (double v : m.v) if (!std::isfinite(v)) return false;
  return std::fabs(m.v[3]) < 1e-6 && std::fabs(m.v[7]) < 1e-6 &&
      std::fabs(m.v[11]) < 1e-6 && std::fabs(m.v[15]-1) < 1e-6;
}
inline OutputMatrix OutputMultiply(const OutputMatrix &a, const OutputMatrix &b) {
  OutputMatrix r;
  for (int c=0;c<4;++c) for (int row=0;row<4;++row)
    for (int k=0;k<4;++k) r.v[c*4+row]+=a.v[k*4+row]*b.v[c*4+k];
  return r;
}
inline bool OutputPoint(const OutputMatrix &m, const double p[3], double out[3]) {
  if (!OutputAffine(m)) return false;
  double r[3]{};
  for (int i=0;i<3;++i) {
    if (!std::isfinite(p[i])) return false;
    r[i]=m.v[12+i];
    for (int k=0;k<3;++k) r[i]+=m.v[k*4+i]*p[k];
    if (!std::isfinite(r[i])) return false;
  }
  std::copy(r,r+3,out); return true;
}
inline bool OutputCompletedTRS(const double (&p)[3],const float (&q)[4],const float (&scale)[3],OutputMatrix &out) {
  for (double v:p) if (!std::isfinite(v)) return false;
  double norm=0; for (float v:q) { if (!std::isfinite(v)) return false; norm+=double(v)*v; }
  if (std::fabs(norm-1)>1e-4) return false;
  for (float v:scale) if (!std::isfinite(v) || v<=.0001f || std::fabs(v-scale[0])>scale[0]*.0001) return false;
  const double x=q[0],y=q[1],z=q[2],w=q[3],s=scale[0]; OutputMatrix m;
  m.v[0]=s*(1-2*(y*y+z*z)); m.v[1]=s*2*(x*y+z*w); m.v[2]=s*2*(x*z-y*w);
  m.v[4]=s*2*(x*y-z*w); m.v[5]=s*(1-2*(x*x+z*z)); m.v[6]=s*2*(y*z+x*w);
  m.v[8]=s*2*(x*z+y*w); m.v[9]=s*2*(y*z-x*w); m.v[10]=s*(1-2*(x*x+y*y));
  for (int k=0;k<3;++k) m.v[12+k]=p[k]; m.v[15]=1; out=m; return true;
}
}
