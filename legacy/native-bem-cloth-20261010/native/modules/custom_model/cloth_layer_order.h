#pragma once
#include "cloth_display_binding.h"
#include <algorithm>
#include <array>
#include <cstdint>
#include <cstddef>
#include <vector>

// EIEM's owned point/triangle sign policy, adapted to runtime-matched Rin
// particles. The native contact is copied; mass, flags and thickness are kept.
namespace BetterEndfieldNext::CustomModel::ClothLayer {
namespace CD=ClothDisplay;
struct Contact {
    uint32_t flags[2]{};
    uint16_t thickness=0,sign=0;
    int point=0,triangle[3]{};
    uint16_t point_mass=0,triangle_mass[3]{};
};
static_assert(sizeof(Contact)==36&&offsetof(Contact,sign)==10&&offsetof(Contact,triangle)==16);
struct Surface {int team=0,start=0;std::vector<CD::V> rest;CD::V center{},axis{};};
struct Policy {Surface inner,outer;uintptr_t list=0;};
enum class Result {Foreign,Invalid,Kept,Changed};
inline bool Contains(const Surface& s,int index) {
    return index>=s.start&&uint64_t(index)-uint64_t(s.start)<s.rest.size();
}
inline bool Valid(const Surface& s) {
    return s.team>0&&s.team<=0xffffff&&s.start>=0&&!s.rest.empty()&&s.rest.size()<=128&&
        s.start<=INT32_MAX-int(s.rest.size())&&std::abs(CD::Dot(s.axis,s.axis)-1)<1e-5;
}
inline bool Valid(const Policy& p) {
    return p.list&&!(p.list&7)&&Valid(p.inner)&&Valid(p.outer)&&p.inner.team!=p.outer.team&&
        (p.inner.start+int(p.inner.rest.size())<=p.outer.start||p.outer.start+int(p.outer.rest.size())<=p.inner.start);
}
inline Result Correct(const Policy& p,const Contact& source,Contact& copy) {
    const int a=int(source.flags[0]&0xffffff),b=int(source.flags[1]&0xffffff);
    const bool inside=a==p.inner.team&&b==p.outer.team;
    if(!inside&&!(a==p.outer.team&&b==p.inner.team))return Result::Foreign;
    const auto& points=inside?p.inner:p.outer;
    const auto& faces=inside?p.outer:p.inner;
    if(!Contains(points,source.point))return Result::Invalid;
    for(int id:source.triangle)if(!Contains(faces,id))return Result::Invalid;
    if(source.triangle[0]==source.triangle[1]||source.triangle[1]==source.triangle[2]||source.triangle[0]==source.triangle[2])return Result::Invalid;
    if(source.sign!=0x3c00&&source.sign!=0xbc00)return Result::Invalid;
    for(uint16_t half:{source.thickness,source.point_mass,source.triangle_mass[0],source.triangle_mass[1],source.triangle_mass[2]})
        if((half&0x8000)||(half&0x7c00)==0x7c00)return Result::Invalid;
    const auto x=faces.rest[size_t(source.triangle[0]-faces.start)];
    const auto y=faces.rest[size_t(source.triangle[1]-faces.start)];
    const auto z=faces.rest[size_t(source.triangle[2]-faces.start)];
    auto radial=(x+y+z)*(1.0/3)-faces.center;
    radial=radial-faces.axis*CD::Dot(radial,faces.axis);
    const double direction=CD::Dot(CD::Cross(y-x,z-x),radial);
    if(!std::isfinite(direction)||std::abs(direction)<1e-10)return Result::Invalid;
    const bool positive=(direction>0)!=inside;
    const uint16_t wanted=positive?0x3c00:0xbc00;
    if(source.sign==wanted)return Result::Kept;
    copy=source;copy.sign=wanted;return Result::Changed;
}
}
