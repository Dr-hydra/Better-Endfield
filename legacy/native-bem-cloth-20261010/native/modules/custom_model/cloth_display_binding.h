#pragma once
#include <array>
#include <cmath>
#include <cstdint>
#include <stdexcept>
#include <vector>

namespace BetterEndfieldNext::CustomModel::ClothDisplay {
struct V {double x=0,y=0,z=0;};
using M=std::array<double,16>; // Unity column-major Matrix4x4
inline V operator+(V a,V b){return {a.x+b.x,a.y+b.y,a.z+b.z};}
inline V operator-(V a,V b){return {a.x-b.x,a.y-b.y,a.z-b.z};}
inline V operator*(V a,double b){return {a.x*b,a.y*b,a.z*b};}
inline double Dot(V a,V b){return a.x*b.x+a.y*b.y+a.z*b.z;}
inline V Cross(V a,V b){return {a.y*b.z-a.z*b.y,a.z*b.x-a.x*b.z,a.x*b.y-a.y*b.x};}
inline V Unit(V a){const double n=std::sqrt(Dot(a,a));if(!std::isfinite(n)||n<1e-10)throw std::runtime_error("degenerate cloth frame");return a*(1/n);}
inline V Direction(const M& m,V p){return {m[0]*p.x+m[4]*p.y+m[8]*p.z,m[1]*p.x+m[5]*p.y+m[9]*p.z,m[2]*p.x+m[6]*p.y+m[10]*p.z};}
inline V Point(const M& m,V p){return Direction(m,p)+V{m[12],m[13],m[14]};}
inline V TransposeDirection(const M& m,V p){return {m[0]*p.x+m[1]*p.y+m[2]*p.z,m[4]*p.x+m[5]*p.y+m[6]*p.z,m[8]*p.x+m[9]*p.y+m[10]*p.z};}
inline M Multiply(const M& a,const M& b){M out{};for(int c=0;c<4;++c)for(int r=0;r<4;++r)for(int k=0;k<4;++k)out[c*4+r]+=a[k*4+r]*b[c*4+k];return out;}
inline M Inverse(const M& m) {
    for(double v:m)if(!std::isfinite(v))throw std::runtime_error("non-finite skin matrix");
    if(std::abs(m[3])+std::abs(m[7])+std::abs(m[11])>1e-6||std::abs(m[15]-1)>1e-3)throw std::runtime_error("non-affine skin matrix");
    V a{m[0],m[1],m[2]},b{m[4],m[5],m[6]},c{m[8],m[9],m[10]};
    const double determinant=Dot(a,Cross(b,c));
    if(!std::isfinite(determinant)||std::abs(determinant)<1e-12)throw std::runtime_error("singular skin matrix");
    V r0=Cross(b,c)*(1/determinant),r1=Cross(c,a)*(1/determinant),r2=Cross(a,b)*(1/determinant);
    M result{r0.x,r1.x,r2.x,0,r0.y,r1.y,r2.y,0,r0.z,r1.z,r2.z,0,0,0,0,1};
    V t=Direction(result,{m[12],m[13],m[14]})*-1;result[12]=t.x;result[13]=t.y;result[14]=t.z;return result;
}
inline M Skin(const std::vector<M>& palette,const std::array<uint16_t,4>& weights,const std::array<uint8_t,4>& bones) {
    M result{};uint32_t total=0;
    for(int i=0;i<4;++i)if(weights[i]) {
        if(bones[i]>=palette.size())throw std::runtime_error("cloth skin palette index invalid");
        total+=weights[i];for(int k=0;k<16;++k)result[k]+=palette[bones[i]][k]*(weights[i]/65535.0);
    }
    if(total<65530||total>65540)throw std::runtime_error("cloth skin weights are not normalized");
    return result;
}
struct Frame {V t,b,n;};
inline Frame Triangle(V a,V b,V c){V t=Unit(b-a),n=Unit(Cross(b-a,c-a));return {t,Cross(n,t),n};}
inline V Local(Frame f,V v){return {Dot(f.t,v),Dot(f.b,v),Dot(f.n,v)};}
inline V World(Frame f,V v){return f.t*v.x+f.b*v.y+f.n*v.z;}
inline V Follow(V a,V b,V c,const std::array<double,3>& bary,V offset){return a*bary[0]+b*bary[1]+c*bary[2]+World(Triangle(a,b,c),offset);}
inline V TransportOffset(const M& transform,V a,V b,V c,V offset) {
    return Local(Triangle(Point(transform,a),Point(transform,b),Point(transform,c)),
        Direction(transform,World(Triangle(a,b,c),offset)));
}
}
