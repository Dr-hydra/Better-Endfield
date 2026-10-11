#pragma once
#include <algorithm>
#include <cmath>
#include <cstdint>

namespace BetterEndfieldNext::MediaScreen {
struct Vec2 { float x, y; };
struct Vec3 { float x, y, z; };
struct Ray { Vec3 origin, direction; };
inline Vec3 Sub(Vec3 a, Vec3 b) { return {a.x-b.x, a.y-b.y, a.z-b.z}; }
inline Vec3 Cross(Vec3 a, Vec3 b) { return {a.y*b.z-a.z*b.y, a.z*b.x-a.x*b.z, a.x*b.y-a.y*b.x}; }
inline float Dot(Vec3 a, Vec3 b) { return a.x*b.x+a.y*b.y+a.z*b.z; }
// Two-sided Moller-Trumbore; local-ray parameter preserves world distance even
// under non-uniform transform scale. UV interpolation is perspective-correct.
inline bool HitTriangle(Ray ray, Vec3 a, Vec3 b, Vec3 c, Vec2 ua, Vec2 ub, Vec2 uc, float& nearest, Vec2& uv) {
    auto ab=Sub(b,a), ac=Sub(c,a), p=Cross(ray.direction,ac);
    float det=Dot(ab,p); if (std::abs(det)<1e-8f) return false;
    auto t=Sub(ray.origin,a); float u=Dot(t,p)/det;
    auto q=Cross(t,ab); float v=Dot(ray.direction,q)/det, distance=Dot(ac,q)/det;
    if (u<0 || v<0 || u+v>1 || distance<0 || distance>=nearest) return false;
    nearest=distance; uv={ua.x+(ub.x-ua.x)*u+(uc.x-ua.x)*v, ua.y+(ub.y-ua.y)*u+(uc.y-ua.y)*v}; return true;
}
struct CaptureGeometry { uint32_t source_width=0, source_height=0, left=0, top=0, width=0, height=0; };
// Canvas RGBA rows are flipped on upload, so Unity UV y=1 is the source top.
inline bool SourcePoint(Vec2 uv, int width, int height, const CaptureGeometry& g, int& x, int& y) {
    if (!std::isfinite(uv.x) || !std::isfinite(uv.y) || !g.width || !g.height || !g.source_width || !g.source_height ||
        uint64_t(g.left)+g.width>uint32_t(width) || uint64_t(g.top)+g.height>uint32_t(height)) return false;
    double cx=double(uv.x)*width-g.left, cy=(1-double(uv.y))*height-g.top;
    if (cx<0 || cy<0 || cx>=g.width || cy>=g.height) return false;
    x=std::min(int(g.source_width)-1, int(cx*g.source_width/g.width));
    y=std::min(int(g.source_height)-1, int(cy*g.source_height/g.height)); return true;
}
}
