#pragma once
#include "math/quat_math.h"
#include <cmath>
#include <algorithm>

// 绕轴旋转（Rodrigues）。axis 需为单位向量，rad 为旋转角。
inline Vec3 RotateAxis(Vec3 axis, Vec3 v, float rad){
    Vec3 k = Norm(axis);
    float c = std::cos(rad), s = std::sin(rad);
    return v*c + Cross(k, v)*s + k*(Dot(k, v)*(1.0f - c));
}

// 标准解析式 2-bone IK。a=根, b=中间(肘), c=末端。target=期望末端位置。pole=弯曲方向（肘往哪边偏）。
// 直接原地改写 a/b/c 的坐标（供测试）；游戏内由调用方转成每根骨 localRotation。
// 数学：在 a-target-pole 确定的平面内，根角 θ1 / 肘角 θ2 由余弦定理给出；
// 上臂方向 = 绕 bendAxis 旋转 at 方向 θ1，由余弦定理可证 |c'-target|=lbc，末端精确命中。
inline void SolveTwoBone(Vec3& a, Vec3& b, Vec3& c, Vec3 target, Vec3 pole, bool enforcePole){
    float lab = Len(b - a), lbc = Len(c - b);
    float d = Len(target - a);
    Vec3 at = d>1e-6f ? Norm(target-a) : Norm(c-a);
    if(Len(at)<1e-6f)at=Norm(b-a);
    if(Len(at)<1e-6f)at={0,-1,0};
    if(lab<1e-6f||lbc<1e-6f){b=a+at*lab;c=b+at*lbc;return;}
    const float epsilon=(std::min)(1e-4f,(std::min)(lab,lbc)*.01f);
    d=(std::max)(std::fabs(lab-lbc)+epsilon,(std::min)(lab+lbc-epsilon,d));
    Vec3 reachable=a+at*d;
    // 根关节弯折角（余弦定理）
    float cos1 = (lab*lab + d*d - lbc*lbc) / (2.0f*lab*d);
    cos1 = cos1 > 1.0f ? 1.0f : (cos1 < -1.0f ? -1.0f : cos1);
    float ang1 = std::acos(cos1);

    // 弯折轴：垂直于 a→target 与 a→pole 张成的平面；退化时退化为世界默认轴
    Vec3 poleDir = Norm(pole - a);
    Vec3 axis = Norm(Cross(at, poleDir));
    if (Len(axis) < 1e-5f) axis = Norm(Cross(at, Vec3{0,1,0}));
    if (Len(axis) < 1e-5f) axis = Norm(Cross(at, Vec3{0,0,1}));

    Vec3 upperDir = RotateAxis(axis, at, ang1);   // 上臂方向：at 绕轴弯 ang1（朝 pole 一侧）
    b = a + upperDir * lab;

    Vec3 foreDir = Norm(reachable - b);
    c = b + foreDir * lbc;
}
