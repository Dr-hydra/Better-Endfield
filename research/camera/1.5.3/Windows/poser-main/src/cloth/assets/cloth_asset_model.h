#pragma once
#include "cloth_asset_serialized.h"
#include "cloth_asset_manifest.h"
#include <functional>
namespace eiem_cloth_asset {
using Point=std::array<double,3>;
using Matrix=std::array<double,16>;
inline Point Sub(Point a,Point b){return {a[0]-b[0],a[1]-b[1],a[2]-b[2]};}
inline double Dot(Point a,Point b){return a[0]*b[0]+a[1]*b[1]+a[2]*b[2];}
inline double Distance(Point a,Point b){const auto d=Sub(a,b);return std::sqrt(Dot(d,d));}
inline Point Cross(Point a,Point b){return {a[1]*b[2]-a[2]*b[1],a[2]*b[0]-a[0]*b[2],a[0]*b[1]-a[1]*b[0]};}
inline Point Vec(const Value &v){return {v.At("x").Number(),v.At("y").Number(),v.At("z").Number()};}
inline Matrix Identity(){return {1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1};}
inline Matrix Mul(const Matrix &a,const Matrix &b){Matrix c{};for(int j=0;j<4;++j)for(int i=0;i<4;++i)for(int k=0;k<4;++k)c[j*4+i]+=a[k*4+i]*b[j*4+k];return c;}
inline Point Transform(const Matrix &m,Point p){Point q{};for(int i=0;i<3;++i)q[i]=m[12+i]+m[i]*p[0]+m[4+i]*p[1]+m[8+i]*p[2];return q;}
inline Matrix Bind(const Value &v){Matrix m{};for(int j=0;j<4;++j)for(int i=0;i<4;++i)m[j*4+i]=v.At("e"+std::to_string(i)+std::to_string(j)).Number();return m;}
inline Matrix Trs(const Value &v){const auto &q=v.At("m_LocalRotation");const double x=q.At("x").Number(),y=q.At("y").Number(),z=q.At("z").Number(),w=q.At("w").Number();
  Need(std::abs(x*x+y*y+z*z+w*w-1)<.001,"asset-transform-quaternion");const auto s=Vec(v.At("m_LocalScale")),p=Vec(v.At("m_LocalPosition"));for(auto a:s)Need(a>0&&a<1000,"asset-transform-scale");
  return {(1-2*y*y-2*z*z)*s[0],(2*x*y+2*z*w)*s[0],(2*x*z-2*y*w)*s[0],0,
    (2*x*y-2*z*w)*s[1],(1-2*x*x-2*z*z)*s[1],(2*y*z+2*x*w)*s[1],0,
    (2*x*z+2*y*w)*s[2],(2*y*z-2*x*w)*s[2],(1-2*x*x-2*y*y)*s[2],0,p[0],p[1],p[2],1};}
inline Bytes Blob(const Value &v){if(v.kind==Value::Blob)return v.bytes;Bytes b;for(const auto &n:v.List()){const auto i=n.Int();Need(i>=0&&i<=255,"asset-byte-array");b.push_back(uint8_t(i));}return b;}
inline std::string Leaf(const std::string &s){return s.substr(s.find_last_of('/')+1);}
struct Package {
  const Vfs &vfs;const Manifest &manifest;
  std::map<std::string,std::shared_ptr<SerializedFile>> files;
  std::map<std::string,std::string> sources;
  std::set<int> loaded;
  size_t totalBytes=0;
  std::shared_ptr<size_t> valueBudget=std::make_shared<size_t>(2000000);
  Package(const Vfs &v,const Manifest &m):vfs(v),manifest(m){}
  void Load(int bundle){Need(bundle>=0&&size_t(bundle)<manifest.bundles.size(),"asset-package-bundle");if(loaded.count(bundle))return;
    Need(loaded.size()<256,"asset-package-dependency-budget");const auto path="Data/Bundles/Windows/"+manifest.bundles[bundle].name;
    auto bytes=vfs.Read(path);const auto sha=Digest(bytes);auto nodes=DecodeBundle(bytes,vfs.cancel);
    for(auto &node:nodes){totalBytes+=node.bytes.size();Need(totalBytes<=384*1024*1024,"asset-package-memory-budget");
      if(node.name.rfind("CAB-",0)!=0||node.name.find('.')!=std::string::npos)continue;
      auto f=std::make_shared<SerializedFile>();f->cancel=vfs.cancel;f->valueBudget=valueBudget;f->Open(node.name,std::move(node.bytes));const auto old=files.find(f->name);
      if(old!=files.end())Need(old->second->sha==f->sha,"asset-package-conflicting-CAB");else files[f->name]=std::move(f);
    }sources[path]=sha;loaded.insert(bundle);
  }
  void Models(int prefab){Load(prefab);std::set<int> dependencies{prefab};for(int b:manifest.bundles[prefab].dependencies)dependencies.insert(b);
    for(const auto &a:manifest.assets){if(!dependencies.count(a.bundleId))continue;const auto n=Lower(a.name);
      if((n.size()>=6&&n.compare(n.size()-6,6,".asset")==0)||(n.size()>=4&&n.compare(n.size()-4,4,".fbx")==0))Load(a.bundleId);
    }
  }
  std::pair<SerializedFile*,int64_t> Resolve(SerializedFile &origin,const Value &ptr,int expected){const auto index=ptr.At("m_FileID").Int(),id=ptr.At("m_PathID").Int();Need(id&&index>=0&&size_t(index)<=origin.externals.size(),"asset-reference-range");SerializedFile *f=&origin;
    if(index){const auto name=Leaf(origin.externals[size_t(index)-1]);auto found=files.find(name);Need(found!=files.end(),"asset-model-dependency-unavailable");f=found->second.get();}Need(f->Class(id)==expected,"asset-reference-class");return {f,id};
  }
};
struct MeshView;
struct Scene {
  SerializedFile &file;
  int64_t bodyRoot=0;
  int64_t animatorRoot=0;
  std::map<int64_t,int64_t> goTransform;
  std::map<int64_t,std::vector<int64_t>> children;
  std::map<int64_t,Matrix> worlds;
  std::map<int64_t,int64_t> parents;
  std::map<int64_t,const std::string*> names;
  std::map<int64_t,std::shared_ptr<const MeshView>> meshes;
  size_t meshCacheBytes=0,meshReads=0,meshCacheHits=0;uint64_t meshDecodeMs=0;
  explicit Scene(SerializedFile &f):file(f){worlds[0]=Identity();for(const auto &o:f.objects)if(f.Class(o.first)==4){const auto &v=f.Get(o.first);Need(goTransform.emplace(v.At("m_GameObject").LocalRef(),o.first).second,"asset-duplicate-transform");children[o.first];}
    for(const auto &entry:goTransform){const auto p=Parent(entry.second);if(p)Need(children.count(p)>0,"asset-transform-parent-missing");children[p].push_back(entry.second);}}
  int64_t Parent(int64_t id){auto found=parents.find(id);if(found!=parents.end())return found->second;const auto parent=file.Get(id).At("m_Father").LocalRef();parents.emplace(id,parent);return parent;}
  int64_t TransformId(int64_t id){if(file.Class(id)==4)return id;const auto go=file.Class(id)==1?id:file.Get(id).At("m_GameObject").LocalRef();auto i=goTransform.find(go);Need(i!=goTransform.end(),"asset-transform-missing");return i->second;}
  const std::string &Name(int64_t id){static const std::string empty;if(!id)return empty;auto found=names.find(id);if(found!=names.end())return *found->second;
    const auto &v=file.Get(id),&name=v.At("m_Name");const auto &text=name.kind!=Value::Null&&!name.Text().empty()?name.Text():file.Get(v.At("m_GameObject").LocalRef()).At("m_Name").Text();names.emplace(id,&text);return text;}
  Matrix World(int64_t id,unsigned depth=0){Need(depth<128,"asset-transform-cycle");auto i=worlds.find(id);if(i!=worlds.end())return i->second;return worlds[id]=Mul(World(Parent(id),depth+1),Trs(file.Get(id)));}
  std::vector<int64_t> Branch(const std::vector<int64_t> &roots,std::map<int64_t,int> *columns=nullptr){std::vector<int64_t> result;std::set<int64_t> seen;
    for(size_t c=0;c<roots.size();++c){std::vector<int64_t> stack{roots[c]};while(!stack.empty()){const auto id=stack.back();stack.pop_back();Need(children.count(id)&&seen.insert(id).second&&seen.size()<=512,"asset-branch-overlap-or-budget");result.push_back(id);if(columns)(*columns)[id]=int(c);for(auto p:children[id])stack.push_back(p);}}return result;}
};
inline double Half(uint16_t h){const int sign=h>>15,exponent=(h>>10)&31,mantissa=h&1023;Need(exponent!=31,"asset-nonfinite-half");return (sign?-1.:1.)*std::ldexp(exponent?1.+mantissa/1024.:mantissa/1024.,exponent?exponent-15:-14);}
inline std::vector<std::vector<double>> Channel(const Value &mesh,int channel){const auto &vd=mesh.At("m_VertexData");const auto &cs=vd.At("m_Channels").List();const auto count=vd.At("m_VertexCount").Int();Need(count>0&&count<=200000&&channel>=0&&size_t(channel)<cs.size(),"asset-channel-count");
  constexpr int widths[]{4,2,1,1,2,2,1,1,2,2,4,4};std::array<size_t,8> stride{},offset{};
  for(const auto &c:cs){const auto stream=c.At("stream").Int(),format=c.At("format").Int(),dimension=c.At("dimension").Int()&15;Need(stream>=0&&stream<8&&format>=0&&format<12&&dimension<=4,"asset-channel-format");stride[size_t(stream)]+=size_t(dimension)*widths[format];}
  size_t bytes=0;for(size_t k=0;k<8;++k){offset[k]=bytes;bytes=(bytes+size_t(count)*stride[k]+15)&~size_t(15);}
  const auto &blob=vd.At("m_DataSize");const auto owned=blob.kind==Value::Blob?Bytes{}:Blob(blob);const auto &raw=blob.kind==Value::Blob?blob.bytes:owned;
  Need(raw.size()<=bytes&&bytes-raw.size()<16,"asset-inline-stream-size");const auto &c=cs[size_t(channel)];const auto stream=size_t(c.At("stream").Int()),format=size_t(c.At("format").Int()),dimension=size_t(c.At("dimension").Int()&15),start=size_t(c.At("offset").Int());Need(dimension>0&&start+dimension*widths[format]<=stride[stream],"asset-channel-stride");
  std::vector<std::vector<double>> result(static_cast<size_t>(count),std::vector<double>(dimension));
  for(size_t n=0;n<result.size();++n){const size_t at=offset[stream]+n*stride[stream]+start;Need(at<=raw.size()&&dimension*widths[format]<=raw.size()-at,"asset-channel-byte-range");Reader r(raw.data()+at,dimension*widths[format]);
    for(auto &v:result[n]){const auto u=r.U(widths[format]);switch(format){case 0:{uint32_t b=uint32_t(u);float f;memcpy(&f,&b,4);v=f;break;}case 1:v=Half(uint16_t(u));break;case 2:v=u/255.;break;case 3:v=(std::max)(-1.,double(int8_t(u))/127.);break;case 4:v=u/65535.;break;case 5:v=(std::max)(-1.,double(int16_t(u))/32767.);break;case 6:case 8:case 10:v=double(u);break;case 7:v=int8_t(u);break;case 9:v=int16_t(u);break;case 11:v=int32_t(u);break;}Need(std::isfinite(v),"asset-channel-nonfinite");}}
  return result;
}
inline std::vector<std::array<int,3>> Triangles(const Value &mesh){const auto format=mesh.At("m_IndexFormat").Int();Need(format==0||format==1,"asset-index-format");const size_t width=format?4:2;
  const auto &blob=mesh.At("m_IndexBuffer");const auto owned=blob.kind==Value::Blob?Bytes{}:Blob(blob);const auto &raw=blob.kind==Value::Blob?blob.bytes:owned;std::vector<std::array<int,3>> result;
  for(const auto &s:mesh.At("m_SubMeshes").List()){const auto begin=s.At("firstByte").Int(),count=s.At("indexCount").Int(),base=s.At("baseVertex").Int();Need(s.At("topology").Int()==0&&begin>=0&&count>=0&&count%3==0&&size_t(begin)%width==0&&uint64_t(begin)+uint64_t(count)*width<=raw.size(),"asset-triangle-range");Reader r(raw.data()+begin,size_t(count)*width);
    for(int64_t n=0;n<count;n+=3){std::array<int,3> f{};for(auto &i:f){const auto value=int64_t(r.U(width))+base;Need(value>=0&&value<mesh.At("m_VertexData").At("m_VertexCount").Int(),"asset-triangle-vertex");i=int(value);}result.push_back(f);}}
  Need(result.size()<=400000,"asset-triangle-budget");return result;
}
struct MeshView {
  std::string name,root,mesh,parent,sourceHash;
  int64_t id=0;int submeshes=0,vertices=0;
  std::vector<int64_t> bones;
  std::vector<Matrix> binds;
  std::vector<Point> world;
  std::vector<std::vector<double>> weights,indices;
  std::vector<std::array<int,3>> triangles;
};
inline MeshView DescribeMesh(Package &package,Scene &scene,int64_t id){const auto &r=scene.file.Get(id);auto ref=package.Resolve(scene.file,r.At("m_Mesh"),43);const auto &m=ref.first->Get(ref.second);
  MeshView out;out.id=id;out.name=scene.Name(id);out.parent=scene.Name(scene.Parent(scene.TransformId(id)));out.mesh=m.At("m_Name").Text();out.root=scene.Name(r.At("m_RootBone").LocalRef());out.sourceHash=ref.first->sha;out.submeshes=int(m.At("m_SubMeshes").List().size());out.vertices=int(m.At("m_VertexData").At("m_VertexCount").Int());Need(out.vertices>0&&out.vertices<=200000,"asset-mesh-vertex-budget");
  for(const auto &b:r.At("m_Bones").List())out.bones.push_back(b.LocalRef());for(const auto &b:m.At("m_BindPose").List())out.binds.push_back(Bind(b));Need(!out.bones.empty()&&out.bones.size()==out.binds.size()&&out.bones.size()<=512&&std::set<int64_t>(out.bones.begin(),out.bones.end()).size()==out.bones.size(),"asset-mesh-binding");
  return out;
}
inline MeshView ReadMesh(Package &package,Scene &scene,int64_t id){CheckCancel(package.vfs.cancel);++scene.meshReads;
  auto cached=scene.meshes.find(id);if(cached!=scene.meshes.end()){++scene.meshCacheHits;return *cached->second;}
  const auto start=GetTickCount64();auto out=DescribeMesh(package,scene,id);auto ref=package.Resolve(scene.file,scene.file.Get(id).At("m_Mesh"),43);const auto &m=ref.first->Get(ref.second);
  Need(!m.At("m_StreamData").At("size").Int()&&!m.At("m_MeshCompression").Int(),"asset-streamed-or-compressed-mesh");
  const auto positions=Channel(m,0);out.weights=Channel(m,12);out.indices=Channel(m,13);Need(positions.size()==out.weights.size()&&positions.size()==out.indices.size(),"asset-skin-count");out.world.resize(positions.size());std::vector<Matrix> matrices;for(size_t k=0;k<out.bones.size();++k)matrices.push_back(Mul(scene.World(out.bones[k]),out.binds[k]));
  for(size_t n=0;n<positions.size();++n){Need(positions[n].size()==3&&out.weights[n].size()==out.indices[n].size(),"asset-skin-dimension");Point p{positions[n][0],positions[n][1],positions[n][2]};double sum=0;
    for(size_t k=0;k<out.weights[n].size();++k){const auto w=out.weights[n][k],ix=out.indices[n][k];Need(w>=0&&w<=1&&ix>=0&&ix<out.bones.size()&&ix==std::floor(ix),"asset-skin-weight-or-index");sum+=w;const auto q=Transform(matrices[size_t(ix)],p);for(int j=0;j<3;++j)out.world[n][j]+=q[j]*w;}Need(std::abs(sum-1)<.0001,"asset-skin-sum");}
  out.triangles=Triangles(m);scene.meshDecodeMs+=GetTickCount64()-start;
  size_t bytes=sizeof(out)+out.name.size()+out.root.size()+out.mesh.size()+out.parent.size()+out.sourceHash.size()+out.bones.size()*sizeof(int64_t)+out.binds.size()*sizeof(Matrix)+out.world.size()*sizeof(Point)+out.triangles.size()*sizeof(std::array<int,3>);
  for(const auto *rows:{&out.weights,&out.indices}){bytes+=rows->size()*(sizeof(std::vector<double>)+32);for(const auto &row:*rows)bytes+=row.size()*sizeof(double);}
  constexpr size_t budget=32*1024*1024;if(scene.meshes.size()<64&&bytes<=budget-scene.meshCacheBytes){scene.meshes.emplace(id,std::make_shared<MeshView>(out));scene.meshCacheBytes+=bytes;}
  return out;
}
}
