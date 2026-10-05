#pragma once
#include <cmath>

struct Vec3 {
    float x=0,y=0,z=0;
    Vec3() = default;
    Vec3(float x_, float y_, float z_) : x(x_), y(y_), z(z_) {}
};
inline Vec3 operator+(Vec3 a, Vec3 b){return {a.x+b.x,a.y+b.y,a.z+b.z};}
inline Vec3 operator-(Vec3 a, Vec3 b){return {a.x-b.x,a.y-b.y,a.z-b.z};}
inline Vec3 operator*(Vec3 a, float s){return {a.x*s,a.y*s,a.z*s};}
inline float Dot(Vec3 a, Vec3 b){return a.x*b.x+a.y*b.y+a.z*b.z;}
inline Vec3 Cross(Vec3 a, Vec3 b){return {a.y*b.z-a.z*b.y,a.z*b.x-a.x*b.z,a.x*b.y-a.y*b.x};}
inline float Len(Vec3 a){return std::sqrt(Dot(a,a));}
inline Vec3 Norm(Vec3 a){float l=Len(a);return l>1e-6f?a*(1.0f/l):Vec3{};}

struct Quat;
inline Quat operator*(Quat a, Quat b);
inline Quat Conj(Quat q);
inline float DotQ(Quat a, Quat b);
inline Quat NormQ(Quat q);

struct Quat {
    float x=0,y=0,z=0,w=1;
    Quat() = default;
    Quat(float x_, float y_, float z_, float w_) : x(x_), y(y_), z(z_), w(w_) {}

    static Quat AxisAngle(Vec3 axis, float rad){
        float h=rad*0.5f, s=std::sin(h); Vec3 n=Norm(axis);
        return {n.x*s,n.y*s,n.z*s,std::cos(h)};
    }
    static Quat FromEulerDeg(Vec3 deg){   // YXZ 顺序（Unity 惯例）
        const float D2R=3.14159265358979f/180.0f;
        Vec3 r{deg.x*D2R*0.5f, deg.y*D2R*0.5f, deg.z*D2R*0.5f};
        float cx=std::cos(r.x),sx=std::sin(r.x),cy=std::cos(r.y),sy=std::sin(r.y),cz=std::cos(r.z),sz=std::sin(r.z);
        return {sx*cy*cz+cx*sy*sz, cx*sy*cz-sx*cy*sz, cx*cy*sz-sx*sy*cz, cx*cy*cz+sx*sy*sz};
    }
    Vec3 ToEulerDeg() const {              // 与 FromEulerDeg 互逆（YXZ）
        const float R2D=180.0f/3.14159265358979f;
        float sp=2.0f*(w*y - x*z); sp = sp>1.0f?1.0f:(sp<-1.0f?-1.0f:sp);
        float rx=std::atan2(2.0f*(w*x+y*z), 1.0f-2.0f*(x*x+y*y));
        float ry=std::asin(sp);
        float rz=std::atan2(2.0f*(w*z+x*y), 1.0f-2.0f*(y*y+z*z));
        return {rx*R2D, ry*R2D, rz*R2D};
    }
    static Quat Slerp(Quat a, Quat b, float t){
        float d=DotQ(a,b); if (d<0){b={-b.x,-b.y,-b.z,-b.w}; d=-d;}
        if (d>0.9995f) { Quat r{a.x+(b.x-a.x)*t,a.y+(b.y-a.y)*t,a.z+(b.z-a.z)*t,a.w+(b.w-a.w)*t}; return NormQ(r); }
        float th=std::acos(d), sth=std::sin(th);
        float s0=std::sin((1-t)*th)/sth, s1=std::sin(t*th)/sth;
        return {a.x*s0+b.x*s1, a.y*s0+b.y*s1, a.z*s0+b.z*s1, a.w*s0+b.w*s1};
    }
    static Quat Delta(Quat from, Quat to){ return NormQ(Conj(from) * to); } // to = Delta(from,to) * from
    static float Angle(Quat a, Quat b){ float d=std::fabs(DotQ(a,b)); if(d>1)d=1; return 2.0f*std::acos(d); }
    // 把方向向量 from 旋转到 to 的最短旋转（IK 根/中间骨写回用）
    static Quat FromTo(Vec3 from, Vec3 to){
        from = Norm(from); to = Norm(to);
        float d = Dot(from, to);
        if (d > 0.99999f) return Quat{0,0,0,1};          // 同向
        if (d < -0.99999f) {                              // 180°：任取垂直轴
            Vec3 axis = Norm(Cross(from, Vec3{0,1,0}));
            if (Len(axis) < 1e-5f) axis = Norm(Cross(from, Vec3{0,0,1}));
            return AxisAngle(axis, 3.14159265358979f);
        }
        Vec3 c = Cross(from, to);               // 幅度=sinθ（from/to 已归一）
        float s = std::sqrt((1.0f + d) * 2.0f); // 2*cos(θ/2)
        return NormQ(Quat{c.x / s, c.y / s, c.z / s, s * 0.5f}); // xyz=sin(θ/2)*axis
    }
};

inline Quat operator*(Quat a, Quat b){
    return {a.w*b.x+a.x*b.w+a.y*b.z-a.z*b.y,
            a.w*b.y-a.x*b.z+a.y*b.w+a.z*b.x,
            a.w*b.z+a.x*b.y-a.y*b.x+a.z*b.w,
            a.w*b.w-a.x*b.x-a.y*b.y-a.z*b.z};
}
inline Vec3 operator*(Quat q, Vec3 v){
    Vec3 u{q.x,q.y,q.z};
    return u*(2.0f*Dot(u,v)) + v*(q.w*q.w-Dot(u,u)) + Cross(u,v)*(2.0f*q.w);
}
inline Quat Conj(Quat q){return {-q.x,-q.y,-q.z,q.w};}
inline float QuatLen(Quat q){return std::sqrt(q.x*q.x+q.y*q.y+q.z*q.z+q.w*q.w);}
inline Quat NormQ(Quat q){float l=QuatLen(q); return l>1e-6f?Quat{q.x/l,q.y/l,q.z/l,q.w/l}:Quat{};}
inline float DotQ(Quat a, Quat b){return a.x*b.x+a.y*b.y+a.z*b.z+a.w*b.w;}
