#pragma once
#include <array>
#include <cmath>
#include <cstdint>
#include <set>
#include <stdexcept>
#include <vector>
#include <algorithm>

namespace BetterEndfieldNext::CustomModel::ClothPrototype {
using Point=std::array<float,3>;
using Face=std::array<uint32_t,3>;
inline void Require(bool value,const char* reason) {if(!value)throw std::invalid_argument(reason);}
// Runtime preflight is independent of the Python authoring check. A private
// diagnostic file must not be allowed to submit malformed topology to Unity.
inline void Validate(const std::vector<Point>& points,const std::vector<Face>& faces,
                     const std::vector<uint32_t>& fixed) {
    Require(points.size()>=3&&points.size()<=1024,"cloth point budget invalid");
    Require(!faces.empty()&&faces.size()<=2048,"cloth face budget invalid");
    Require(!fixed.empty()&&fixed.size()<points.size(),"cloth fixed region invalid");
    for(const auto& p:points)for(float value:p)Require(std::isfinite(value)&&std::abs(value)<=3,"cloth coordinate invalid");
    std::set<uint32_t> anchors;
    for(auto i:fixed)Require(i<points.size()&&anchors.insert(i).second,"cloth anchor invalid");
    std::set<Face> unique;
    std::vector<std::vector<uint32_t>> adjacent(points.size());
    for(auto face:faces) {
        for(auto i:face)Require(i<points.size(),"cloth triangle index invalid");
        auto ordered=face;std::sort(ordered.begin(),ordered.end());
        Require(ordered[0]!=ordered[1]&&ordered[1]!=ordered[2]&&unique.insert(ordered).second,"cloth duplicate or collapsed face");
        const auto &a=points[face[0]],&b=points[face[1]],&c=points[face[2]];
        double u[3]{b[0]-a[0],b[1]-a[1],b[2]-a[2]},v[3]{c[0]-a[0],c[1]-a[1],c[2]-a[2]};
        double cross[3]{u[1]*v[2]-u[2]*v[1],u[2]*v[0]-u[0]*v[2],u[0]*v[1]-u[1]*v[0]};
        Require(cross[0]*cross[0]+cross[1]*cross[1]+cross[2]*cross[2]>1e-16,"cloth degenerate face");
        for(auto i:face)for(auto j:face)if(i!=j)adjacent[i].push_back(j);
    }
    std::vector<bool> seen(points.size(),false);
    for(uint32_t i=0;i<points.size();++i) {
        if(seen[i])continue;
        bool anchored=false;std::vector<uint32_t> todo{i};
        while(!todo.empty()) {
            auto current=todo.back();todo.pop_back();if(seen[current])continue;
            Require(!adjacent[current].empty(),"cloth unreferenced point");
            seen[current]=true;anchored|=anchors.contains(current);
            for(auto j:adjacent[current])if(!seen[j])todo.push_back(j);
        }
        Require(anchored,"cloth disconnected panel has no anchor");
    }
}
}
