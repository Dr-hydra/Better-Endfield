#pragma once

// CPU geometry only. Unity vertex streams are never repacked by this code.
#include <algorithm>
#include <array>
#include <cmath>
#include <cstdint>
#include <limits>
#include <numeric>
#include <string>
#include <unordered_map>
#include <vector>

namespace BetterEndfield::FirstPersonMesh {
struct Point { double x=0, y=0, z=0; };
inline Point operator+(Point a, Point b) { return {a.x+b.x,a.y+b.y,a.z+b.z}; }
inline Point operator-(Point a, Point b) { return {a.x-b.x,a.y-b.y,a.z-b.z}; }
inline Point operator*(Point a, double s) { return {a.x*s,a.y*s,a.z*s}; }
inline double Dot(Point a, Point b) { return a.x*b.x+a.y*b.y+a.z*b.z; }
inline Point Cross(Point a, Point b) { return {a.y*b.z-a.z*b.y,a.z*b.x-a.x*b.z,a.x*b.y-a.y*b.x}; }
inline double Length(Point a) { return std::sqrt(Dot(a,a)); }
inline bool Finite(Point p) { return std::isfinite(p.x)&&std::isfinite(p.y)&&std::isfinite(p.z); }
struct Vertex {
    Point position;
    std::array<uint32_t,4> bone{};
    std::array<double,4> weight{};
};
struct Part { std::vector<uint32_t> indices; };
struct Frame { Point origin, axis; double length=0; };
struct Result {
    std::vector<Part> parts;
    size_t hidden_triangles=0, cap_triangles=0, rings=0;
    std::string error;
};
inline uint64_t EdgeKey(uint32_t a,uint32_t b) {
    return (uint64_t(std::min(a,b))<<32)|std::max(a,b);
}
struct Cell {
    int64_t x,y,z;
    bool operator==(const Cell& b) const { return x==b.x&&y==b.y&&z==b.z; }
};
struct CellHash {
    size_t operator()(Cell c) const {
        return std::hash<int64_t>{}(c.x) ^ (std::hash<int64_t>{}(c.y)<<1) ^ (std::hash<int64_t>{}(c.z)<<2);
    }
};
inline bool SameSkin(const Vertex& a,const Vertex& b) {
    std::unordered_map<uint32_t,double> delta;
    for(int j=0;j<4;++j) { delta[a.bone[j]]+=a.weight[j]; delta[b.bone[j]]-=b.weight[j]; }
    double distance=0;
    for(const auto& p:delta) distance+=std::abs(p.second);
    return distance<=128.0/65535.0;
}
// Weld only for topology. Original vertex/UV/normal/skin bytes remain untouched.
inline std::vector<uint32_t> Weld(const std::vector<Vertex>& vertices,double epsilon) {
    std::vector<uint32_t> canonical(vertices.size());
    std::unordered_map<Cell,std::vector<uint32_t>,CellHash> buckets;
    for(uint32_t i=0;i<vertices.size();++i) {
        const Point p=vertices[i].position;
        Cell c{int64_t(std::floor(p.x/epsilon)),int64_t(std::floor(p.y/epsilon)),int64_t(std::floor(p.z/epsilon))};
        uint32_t match=i;
        for(int x=-1;x<=1;++x) for(int y=-1;y<=1;++y) for(int z=-1;z<=1;++z) {
            auto found=buckets.find({c.x+x,c.y+y,c.z+z});
            if(found==buckets.end()) continue;
            for(auto j:found->second)
                if(j<match && Length(p-vertices[j].position)<=epsilon && SameSkin(vertices[i],vertices[j])) match=j;
        }
        canonical[i]=match;
        if(match==i) buckets[c].push_back(i);
    }
    return canonical;
}
struct P2 { double x,y; };
inline double Turn(P2 a,P2 b,P2 c) { return (b.x-a.x)*(c.y-a.y)-(b.y-a.y)*(c.x-a.x); }
inline bool Triangulate(const std::vector<uint32_t>& loop,const std::vector<Vertex>& vertices,
    std::vector<uint32_t>& triangles) {
    if(loop.size()<3 || loop.size()>1024) return false;
    Point normal{};
    const Point origin=vertices[loop.front()].position;
    for(size_t i=0;i<loop.size();++i)
        normal=normal+Cross(vertices[loop[i]].position-origin,vertices[loop[(i+1)%loop.size()]].position-origin);
    double n=Length(normal);
    if(n<1e-12) return false;
    normal=normal*(1/n);
    Point u=Cross(normal,std::abs(normal.x)<0.8?Point{1,0,0}:Point{0,1,0}); u=u*(1/Length(u));
    Point v=Cross(normal,u);
    std::vector<P2> points;
    double extent=0;
    for(auto id:loop) { Point p=vertices[id].position-origin; points.push_back({Dot(p,u),Dot(p,v)}); extent=std::max(extent,Length(p)); }
    double eps=std::max(1e-14,extent*extent*1e-10);
    // Reject self intersections; do not emit a partial or overlapping cap.
    auto on=[&](P2 a,P2 b,P2 p) { return std::abs(Turn(a,b,p))<=eps &&
        (p.x-a.x)*(p.x-b.x)+(p.y-a.y)*(p.y-b.y)<=eps; };
    for(size_t i=0;i<points.size();++i) for(size_t j=i+1;j<points.size();++j) {
        size_t ni=(i+1)%points.size(),nj=(j+1)%points.size();
        if(i==nj||ni==j) continue;
        auto a=points[i],b=points[ni],c=points[j],d=points[nj];
        if((Turn(a,b,c)*Turn(a,b,d)<-eps*eps && Turn(c,d,a)*Turn(c,d,b)<-eps*eps)||
            on(a,b,c)||on(a,b,d)||on(c,d,a)||on(c,d,b)) return false;
    }
    std::vector<size_t> left(loop.size()); std::iota(left.begin(),left.end(),0);
    std::vector<uint32_t> candidate;
    while(left.size()>3) {
        bool clipped=false;
        for(size_t i=0;i<left.size();++i) {
            auto a=left[(i+left.size()-1)%left.size()],b=left[i],c=left[(i+1)%left.size()];
            if(Turn(points[a],points[b],points[c])<=eps) continue;
            bool contains=false;
            for(auto p:left) if(p!=a&&p!=b&&p!=c && Turn(points[a],points[b],points[p])>=-eps &&
                Turn(points[b],points[c],points[p])>=-eps && Turn(points[c],points[a],points[p])>=-eps) {contains=true;break;}
            if(contains) continue;
            candidate.insert(candidate.end(),{loop[a],loop[b],loop[c]});
            left.erase(left.begin()+i);clipped=true;break;
        }
        if(!clipped) return false;
    }
    if(Turn(points[left[0]],points[left[1]],points[left[2]])<=eps) return false;
    candidate.insert(candidate.end(),{loop[left[0]],loop[left[1]],loop[left[2]]});
    triangles=std::move(candidate); return true;
}
inline Result Build(const std::vector<Vertex>& vertices,const std::vector<Part>& parts,
    const std::vector<uint8_t>& bone_kind,const Frame& neck,bool hide_all,bool fill,double range=1.0) {
    Result result; result.parts=parts;
    if(vertices.empty()||vertices.size()>200000||bone_kind.empty()) {result.error="invalid vertex or bone count";return result;}
    for(const auto& v:vertices) {
        if(!Finite(v.position)||Length(v.position)>100000) {result.error="invalid vertex position";return result;}
        double total=0;
        for(int i=0;i<4;++i) {
            if(!std::isfinite(v.weight[i])||v.weight[i]<0||v.weight[i]>1.001||
               (v.weight[i]>0&&v.bone[i]>=bone_kind.size())) {result.error="invalid skin data";return result;}
            total+=v.weight[i];
        }
        if(total<0.9||total>1.1) {result.error="unnormalized skin data";return result;}
    }
    for(const auto& part:parts) {
        if(part.indices.size()%3) {result.error="non-triangle index count";return result;}
        for(auto id:part.indices) if(id>=vertices.size()) {result.error="index out of range";return result;}
    }
    auto canonical=Weld(vertices,1e-5);
    std::vector<uint32_t> parents=canonical;
    auto root=[&](uint32_t id) {while(parents[id]!=id) {parents[id]=parents[parents[id]];id=parents[id];} return id;};
    for(const auto& part:parts) for(size_t i=0;i<part.indices.size();i+=3) {
        auto a=root(part.indices[i]),b=root(part.indices[i+1]),c=root(part.indices[i+2]);parents[b]=a;parents[c]=a;
    }
    std::vector<double> total(vertices.size()),head(vertices.size());
    for(uint32_t i=0;i<vertices.size();++i) for(int j=0;j<4;++j) {
        auto w=vertices[i].weight[j]; if(w==0) continue;
        total[root(i)]+=w;
        if(bone_kind[vertices[i].bone[j]]==1) head[root(i)]+=w;
    }
    for(auto& part:result.parts) for(size_t i=0;i<part.indices.size();i+=3) {
        auto a=part.indices[i],b=part.indices[i+1],c=part.indices[i+2];
        if(a==b||a==c||b==c) continue;
        if(hide_all||head[root(a)]>0.5*total[root(a)]) {
            part.indices[i+1]=a;part.indices[i+2]=a;++result.hidden_triangles;
        }
    }
    if(!fill||!Finite(neck.origin)||!Finite(neck.axis)||neck.length<=1e-5) return result;
    struct Edge {uint32_t from=0,to=0,original=0;size_t part=0,count=0;};
    std::unordered_map<uint64_t,Edge> edges;
    for(size_t s=0;s<result.parts.size();++s) {
        const auto& idx=result.parts[s].indices;
        for(size_t i=0;i<idx.size();i+=3) {
            auto a=canonical[idx[i]],b=canonical[idx[i+1]],c=canonical[idx[i+2]];
            if(a==b||b==c||a==c) continue;
            for(int j=0;j<3;++j) {
                auto from=canonical[idx[i+j]],to=canonical[idx[i+(j+1)%3]];
                auto& e=edges[EdgeKey(from,to)];
                if(e.count==0) e={from,to,idx[i+j],s,0};
                ++e.count;
            }
        }
    }
    std::unordered_map<uint32_t,std::vector<Edge>> outgoing;
    std::unordered_map<uint32_t,size_t> incoming;
    for(const auto& p:edges) if(p.second.count==1) {outgoing[p.second.from].push_back(p.second);++incoming[p.second.to];}
    std::unordered_map<uint32_t,bool> visited;
    const Point axis=neck.axis*(1/Length(neck.axis));
    const double radius=neck.length*std::clamp(range,0.2,3.0);
    for(const auto& start:outgoing) {
        if(visited[start.first]) continue;
        uint32_t current=start.first;std::vector<uint32_t> loop;std::vector<size_t> owners;bool closed=false;
        for(size_t step=0;step<=1024;++step) {
            if(visited[current]) {closed=current==start.first;break;}
            visited[current]=true;
            auto found=outgoing.find(current);
            if(found==outgoing.end()||found->second.size()!=1||incoming[current]!=1) break;
            const auto& e=found->second.front();loop.push_back(e.original);owners.push_back(e.part);current=e.to;
        }
        if(!closed||loop.size()<3||loop.size()>1024) continue;
        Point center{};bool inside=true;
        for(auto id:loop) {
            Point delta=vertices[id].position-neck.origin; double along=Dot(delta,axis);
            if(std::abs(along)>1.5*radius||Length(delta-axis*along)>3*radius) inside=false;
            double neck_weight=0;
            for(int j=0;j<4;++j) if(vertices[id].weight[j]>0 && bone_kind[vertices[id].bone[j]]!=0) neck_weight+=vertices[id].weight[j];
            if(neck_weight<0.5) inside=false;
            center=center+vertices[id].position;
        }
        center=center*(1.0/loop.size());Point delta=center-neck.origin;double along=Dot(delta,axis);
        if(!inside||along < -radius||along>1.25*radius||Length(delta-axis*along)>0.8*radius) continue;
        // Cross-material caps cannot have one unambiguous source material.
        if(!std::all_of(owners.begin(),owners.end(),[&](size_t s){return s==owners.front();})) continue;
        std::reverse(loop.begin(),loop.end());
        std::vector<uint32_t> triangles;
        if(!Triangulate(loop,vertices,triangles)) continue;
        auto& destination=result.parts[owners.front()].indices;
        destination.insert(destination.end(),triangles.begin(),triangles.end());
        result.cap_triangles+=triangles.size()/3;++result.rings;
    }
    return result;
}
} // namespace BetterEndfield::FirstPersonMesh
