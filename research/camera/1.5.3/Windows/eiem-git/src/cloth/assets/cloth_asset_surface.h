#pragma once
#include "cloth_asset_write.h"

namespace eiem_cloth_asset {
struct SurfaceRegions {
  std::vector<std::vector<unsigned char>> vertices;
  size_t selected=0,preserved=0,queries=0;
};
struct SurfaceChartFace {
  size_t mesh=0;std::array<int,3> indices{};
  std::array<std::array<double,2>,3> uv{};
  std::array<Point,3> points{};
  double area=0;
  bool unsafe=false;
};
inline bool ChartPoint(const SurfaceChartFace &f,double x,double y,Point &out) {
  const auto &t=f.uv;
  const double b=((x-t[0][0])*(t[2][1]-t[0][1])-(t[2][0]-t[0][0])*(y-t[0][1]))/f.area;
  const double c=((t[1][0]-t[0][0])*(y-t[0][1])-(x-t[0][0])*(t[1][1]-t[0][1]))/f.area,a=1-b-c;
  if(a<1e-7||b<1e-7||c<1e-7)return false;
  for(int k=0;k<3;++k)out[k]=a*f.points[0][k]+b*f.points[1][k]+c*f.points[2][k];return true;
}
inline SurfaceRegions IsolatedSurfaceRegions(const std::vector<MeshView> &meshes,const std::vector<int> &lod,
    const std::map<int64_t,int> &attributes,Point origin,Point up,Point side,const std::atomic<bool> *cancel=nullptr) {
  Need(meshes.size()==lod.size()&&!meshes.empty(),"auto-surface-region-input");
  const double alignment=Dot(up,side);for(int k=0;k<3;++k)side[k]-=alignment*up[k];
  const double length=std::sqrt(Dot(side,side));Need(length>.001,"auto-surface-region-side-axis");for(auto &v:side)v/=length;const auto front=Cross(up,side);
  SurfaceRegions result;result.vertices.resize(meshes.size());
  std::vector<std::vector<unsigned char>> moving(meshes.size()),owned(meshes.size());
  std::vector<std::vector<std::array<int,3>>> triangles(meshes.size());double low=1e100,high=-1e100;
  for(size_t k=0;k<meshes.size();++k){const auto &m=meshes[k];moving[k].resize(m.world.size());owned[k].resize(m.world.size());result.vertices[k].resize(m.world.size());
    for(size_t n=0;n<m.world.size();++n)for(size_t b=0;b<m.weights[n].size();++b)if(m.weights[n][b]>0){const auto found=attributes.find(m.bones[size_t(m.indices[n][b])]);if(found!=attributes.end()&&found->second){owned[k][n]=1;moving[k][n]|=found->second==2;}}
    for(auto f:m.triangles)if(owned[k][f[0]]&&owned[k][f[1]]&&owned[k][f[2]]&&(moving[k][f[0]]||moving[k][f[1]]||moving[k][f[2]])){
      triangles[k].push_back(f);for(int n:f){const double y=Dot(Sub(m.world[n],origin),up);low=(std::min)(low,y);high=(std::max)(high,y);}}
  }
  Need(high-low>.01&&high-low<3.,"auto-surface-region-height");
  std::vector<SurfaceChartFace> faces;constexpr int Grid=64;
  using Bucket=std::vector<size_t>;std::map<int,std::array<Bucket,Grid*Grid>> groups;
  for(size_t k=0;k<meshes.size();++k){CheckCancel(cancel);const auto &m=meshes[k];
    for(auto f:triangles[k]){SurfaceChartFace face;face.mesh=k;face.indices=f;
      for(int n=0;n<3;++n){const auto v=Sub(m.world[f[n]],origin);face.points[n]=m.world[f[n]];face.uv[n]={(std::atan2(Dot(v,front),Dot(v,side))+3.14159265358979323846)/(2*3.14159265358979323846),(Dot(v,up)-low)/(high-low)};}
      double lo=face.uv[0][0],hi=lo;for(auto v:face.uv){lo=(std::min)(lo,v[0]);hi=(std::max)(hi,v[0]);}if(hi-lo>.5)for(auto &v:face.uv)if(v[0]<.5)v[0]+=1;
      const auto &t=face.uv;face.area=(t[1][0]-t[0][0])*(t[2][1]-t[0][1])-(t[2][0]-t[0][0])*(t[1][1]-t[0][1]);
      const size_t id=faces.size();faces.push_back(face);Need(faces.size()<=200000,"auto-surface-region-face-budget");
      if(std::abs(face.area)<1e-10){faces.back().unsafe=true;continue;}
      double minX=t[0][0],maxX=minX,minY=t[0][1],maxY=minY;for(auto v:t){minX=(std::min)(minX,v[0]);maxX=(std::max)(maxX,v[0]);minY=(std::min)(minY,v[1]);maxY=(std::max)(maxY,v[1]);}
      auto &buckets=groups[lod[k]];
      for(int y=(std::max)(0,int(minY*Grid));y<=(std::min)(Grid-1,int(maxY*Grid));++y)for(int x=int(minX*Grid);x<=int(maxX*Grid);++x){auto &bucket=buckets[size_t(y)*Grid+size_t((x%Grid+Grid)%Grid)];Need(bucket.size()<2048,"auto-surface-region-overlap-budget");bucket.push_back(id);}
    }
  }
  for(size_t id=0;id<faces.size();++id){if((id&127)==0)CheckCancel(cancel);auto &f=faces[id];if(std::abs(f.area)<1e-10)continue;
    auto query=[&](double x,double y){Point p{};if(!ChartPoint(f,x,y,p))return;const int ix=int(std::floor(x*Grid)),iy=(std::min)(Grid-1,(std::max)(0,int(std::floor(y*Grid))));
      for(size_t other:groups[lod[f.mesh]][size_t(iy)*Grid+size_t((ix%Grid+Grid)%Grid)])if(other!=id){Need(++result.queries<=8000000,"auto-surface-region-query-budget");auto &g=faces[other];Point q{};bool inside=false;
        for(int shift=-1;shift<=1&&!inside;++shift)inside=ChartPoint(g,x+shift,y,q);
        if(inside&&Distance(p,q)>.0005){f.unsafe=true;g.unsafe=true;}}
    };
    for(const auto &w:std::array<std::array<double,3>,4>{{{1./3,1./3,1./3},{.8,.1,.1},{.1,.8,.1},{.1,.1,.8}}}){double x=0,y=0;for(int n=0;n<3;++n){x+=w[n]*f.uv[n][0];y+=w[n]*f.uv[n][1];}query(x,y);}
    double minX=f.uv[0][0],maxX=minX,minY=f.uv[0][1],maxY=minY;for(auto v:f.uv){minX=(std::min)(minX,v[0]);maxX=(std::max)(maxX,v[0]);minY=(std::min)(minY,v[1]);maxY=(std::max)(maxY,v[1]);}
    for(int y=(std::max)(0,int(minY*Grid));y<=(std::min)(Grid-1,int(maxY*Grid));++y)for(int x=int(minX*Grid);x<=int(maxX*Grid);++x)query((x+.5)/Grid,(y+.5)/Grid);
  }
  std::vector<std::vector<unsigned char>> blocked(meshes.size());for(size_t k=0;k<meshes.size();++k)blocked[k].resize(meshes[k].world.size());
  for(const auto &f:faces)for(int n:f.indices){result.vertices[f.mesh][n]=moving[f.mesh][n];if(f.unsafe)blocked[f.mesh][n]=1;}
  for(size_t k=0;k<meshes.size();++k){const auto &m=meshes[k];
    auto expanded=blocked[k];for(auto f:m.triangles)if(blocked[k][f[0]]||blocked[k][f[1]]||blocked[k][f[2]])for(int n:f)expanded[n]=1;
    std::map<Bytes,std::vector<size_t>> copies;
    for(size_t n=0;n<m.world.size();++n){ByteWriter key;for(auto p:m.world[n]){uint64_t bits=0;memcpy(&bits,&p,8);key.U(bits,8);}std::map<int64_t,double> weights;
      for(size_t b=0;b<m.weights[n].size();++b)if(m.weights[n][b]>0)weights[m.bones[size_t(m.indices[n][b])]]+=m.weights[n][b];for(auto w:weights){uint64_t bits=0;memcpy(&bits,&w.second,8);key.U(uint64_t(w.first),8);key.U(bits,8);}copies[key.bytes].push_back(n);}
    for(const auto &copy:copies){bool allow=true;for(size_t n:copy.second)allow&=result.vertices[k][n]&&!expanded[n];for(size_t n:copy.second)result.vertices[k][n]=allow;}
    for(size_t n=0;n<m.world.size();++n)if(moving[k][n]){if(result.vertices[k][n])++result.selected;else ++result.preserved;}
  }
  Need(result.selected>=16,"auto-no-isolated-single-surface-region");return result;
}
}
