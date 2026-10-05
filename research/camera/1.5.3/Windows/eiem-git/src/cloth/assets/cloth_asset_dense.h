#pragma once
#include "cloth_asset_density.h"
#include "cloth_asset_write.h"
#include "../bonecloth/cloth_bonecloth_recipe.h"
#include "cloth_asset_panel.h"
#include "../bonecloth/cloth_bonecloth_short_policy.h"
#include "cloth_asset_ribbon_math.h"
#include "cloth_asset_coat_waist.h"

namespace eiem_cloth_asset {
struct DenseMesh {
  ClothBoneLocalMeshConfig view{};Bytes payload;
  std::vector<std::array<float,16>> binds;
  std::vector<int> bindingIds,foreign;
  std::vector<eiem_cloth_skin::Sample> samples;
  std::vector<eiem_cloth_skin::Edge> edges;
  std::vector<eiem_cloth_skin::Seam> seams;
  std::vector<std::array<uint16_t,4>> weights;
  std::vector<std::array<float,4>> floatWeights;
  std::vector<std::array<uint8_t,4>> indices;
  void Link(){view.generatedBytes=payload.data();view.bindings=reinterpret_cast<const float(*)[16]>(binds.data());view.bindingCount=int(binds.size());view.bindingNativeIndices=bindingIds.data();view.samples=samples.data();view.sampleCount=samples.size();view.edges=edges.data();view.edgeCount=edges.size();view.seams=seams.data();view.seamCount=seams.size();view.foreignBindings=foreign.data();view.foreignCount=int(foreign.size());view.generatedWeights=weights.empty()?nullptr:weights.data();view.generatedFloatWeights=floatWeights.empty()?nullptr:floatWeights.data();view.generatedIndices=indices.data();view.generatedVertexCount=indices.size();}
};
struct DenseRecipe {
  std::string densityReport;
  ClothBoneLocalRecipe view{};
  std::deque<std::string> strings;
  std::vector<ClothBoneAsset> added;
  std::vector<int> columns,roots,parents;
  std::vector<float> radii;
  std::array<float,16> radiusCurve{},distanceCurve{};
  std::vector<std::array<int,2>> cross;
  std::vector<DenseMesh> meshes;
  std::vector<ClothBoneLocalMeshConfig> meshViews;
  std::vector<ClothBoneNativeGraph> graphs;
  std::vector<std::vector<std::array<int,3>>> faces;
  std::vector<std::vector<std::array<int,2>>> lines;
  ClothBoneRendererAsset bodyAsset{};
  std::vector<ClothBoneBinding> bodyBindings;
  std::vector<ClothBoneBodySphere> bodySpheres;
  std::vector<ClothBoneResponseFrame> responses;
  std::vector<ClothBoneResponsePoint> responsePoints;
  std::vector<ClothBoneResponseFace> responseFaces;
  std::vector<ClothBoneResponseFace> layerFaces;
  const char *String(const std::string &s){Need(!s.empty()&&s.size()<128,"auto-dense-name-budget");strings.push_back(s);return strings.back().c_str();}
  void Link(){view.added=added.data();view.addedCount=int(added.size());view.columns=columns.data();view.roots=roots.data();view.rootCount=int(roots.size());view.parents=parents.data();view.radii=radii.data();view.radiusCurve=radiusCurve.data();view.distanceCurve=distanceCurve.data();view.cross=cross.data();view.crossCount=int(cross.size());
    if(!bodySpheres.empty()){bodyAsset.bones=bodyBindings.data();bodyAsset.boneCount=int(bodyBindings.size());view.bodyAsset=&bodyAsset;view.bodySpheres=bodySpheres.data();view.bodySphereCount=int(bodySpheres.size());}
    view.responses=responses.data();view.responseCount=int(responses.size());
    view.responsePoints=responsePoints.data();view.responsePointCount=int(responsePoints.size());view.responseFaces=responseFaces.data();view.responseFaceCount=int(responseFaces.size());
    view.layerFaces=layerFaces.data();view.layerFaceCount=int(layerFaces.size());
    meshViews.clear();for(auto &m:meshes){m.Link();meshViews.push_back(m.view);}view.meshes=meshViews.data();view.meshCount=int(meshViews.size());graphs.resize(faces.size());for(size_t n=0;n<faces.size();++n)graphs[n]={faces[n].data(),int(faces[n].size()),lines[n].data(),int(lines[n].size())};view.graphs=graphs.data();view.graphCount=int(graphs.size());}
};
inline Matrix Inverse(const Matrix &m){double a[4][8]{};for(int r=0;r<4;++r){for(int c=0;c<4;++c)a[r][c]=m[c*4+r];a[r][r+4]=1;}
  for(int c=0;c<4;++c){int best=c;for(int r=c+1;r<4;++r)if(std::abs(a[r][c])>std::abs(a[best][c]))best=r;Need(std::abs(a[best][c])>1e-12,"auto-singular-reference");for(int k=0;k<8;++k)std::swap(a[c][k],a[best][k]);const double d=a[c][c];for(double &v:a[c])v/=d;for(int r=0;r<4;++r)if(r!=c){const double s=a[r][c];for(int k=0;k<8;++k)a[r][k]-=s*a[c][k];}}
  Matrix out{};for(int r=0;r<4;++r)for(int c=0;c<4;++c){out[c*4+r]=a[r][c+4];Need(std::isfinite(out[c*4+r]),"auto-nonfinite-inverse");}return out;
}
inline std::array<float,16> CurveSamples(const Value &data,bool positive) {
  std::array<float,16> out{};const double value=data.At("value").Number();Need((positive?value>0:value>=0)&&value<=1,"auto-native-curve-value");
  if(!data.At("useCurve").Int()){out.fill(float(value));return out;}
  const auto &curve=data.At("curve");const auto &keys=curve.At("m_Curve").List();Need(keys.size()>=2&&keys.size()<=16&&curve.At("m_PreInfinity").Int()==2&&curve.At("m_PostInfinity").Int()==2,"auto-native-curve-wrapping");
  double previous=-1;for(const auto &k:keys){const double t=k.At("time").Number();Need(t>previous&&t>=0&&t<=1&&k.At("weightedMode").Int()==0,"auto-native-curve-keys");previous=t;}
  for(size_t n=0;n<16;++n){const double t=float(n)/15.f;double v=0;
    if(t<=keys.front().At("time").Number())v=keys.front().At("value").Number();else if(t>=keys.back().At("time").Number())v=keys.back().At("value").Number();else for(size_t j=1;j<keys.size();++j)if(t<=keys[j].At("time").Number()){
      const auto &a=keys[j-1],&b=keys[j];const double dt=b.At("time").Number()-a.At("time").Number(),u=(t-a.At("time").Number())/dt;
      v=(2*u*u*u-3*u*u+1)*a.At("value").Number()+(u*u*u-2*u*u+u)*dt*a.At("outSlope").Number()+(-2*u*u*u+3*u*u)*b.At("value").Number()+(u*u*u-u*u)*dt*b.At("inSlope").Number();break;}
    out[n]=float(v)*float(value);Need(std::isfinite(out[n])&&(positive?out[n]>0:out[n]>=0),"auto-native-curve-output");
  }return out;
}
inline float CurveAt(const std::array<float,16> &curve,double depth){Need(std::isfinite(depth)&&depth>=0&&depth<=1.00001,"auto-native-depth");const double x=(std::min)(1.,depth)*15;const int k=(std::min)(14,int(x));return float(curve[k]*(1-(x-k))+curve[k+1]*(x-k));}
inline std::vector<double> NativeFixedPathLengths(const std::vector<ClothBoneAsset> &bones,const std::vector<Point> &points) {
  Need(!bones.empty()&&bones.size()<=ClothBoneMaxIdentities&&bones.size()==points.size(),"auto-density-depth-input");
  for(size_t n=0;n<bones.size();++n){Need(bones[n].attribute>=0&&bones[n].attribute<=2,"auto-density-depth-attribute");
    if(bones[n].attribute)for(double v:points[n])Need(std::isfinite(v),"auto-density-depth-position");}
  std::vector<double> lengths(bones.size());
  for(size_t n=0;n<bones.size();++n){size_t steps=0;int current=int(n);
    while(bones[current].attribute==2){const int parent=bones[current].parent;
      Need(++steps<=bones.size()&&parent>=0&&size_t(parent)<bones.size()&&bones[parent].attribute!=0,"auto-density-depth-parent-path");
      lengths[n]+=Distance(points[current],points[parent]);current=parent;}
    Need(std::isfinite(lengths[n]),"auto-density-depth-length");
  }
  return lengths;
}
inline Quaternion QuaternionOf(const Matrix &m){
  for(int i=0;i<3;++i)for(int j=0;j<3;++j){double v=0;for(int k=0;k<3;++k)v+=m[i*4+k]*m[j*4+k];Need(std::abs(v-(i==j?1.:0.))<1e-5,"auto-nonunit-natural-axes");}
  Need(Dot(Cross({m[0],m[1],m[2]},{m[4],m[5],m[6]}),{m[8],m[9],m[10]})>0,"auto-reflected-natural-axes");
  double x=0,y=0,z=0,w=0;const double trace=m[0]+m[5]+m[10];
  if(trace>0){const double s=std::sqrt(trace+1)*2;w=.25*s;x=(m[6]-m[9])/s;y=(m[8]-m[2])/s;z=(m[1]-m[4])/s;}
  else if(m[0]>m[5]&&m[0]>m[10]){const double s=std::sqrt(1+m[0]-m[5]-m[10])*2;w=(m[6]-m[9])/s;x=.25*s;y=(m[4]+m[1])/s;z=(m[8]+m[2])/s;}
  else if(m[5]>m[10]){const double s=std::sqrt(1+m[5]-m[0]-m[10])*2;w=(m[8]-m[2])/s;x=(m[4]+m[1])/s;y=.25*s;z=(m[9]+m[6])/s;}
  else {const double s=std::sqrt(1+m[10]-m[0]-m[5])*2;w=(m[1]-m[4])/s;x=(m[8]+m[2])/s;y=(m[9]+m[6])/s;z=.25*s;}
  const double length=std::sqrt(x*x+y*y+z*z+w*w);return {float(x/length),float(y/length),float(z/length),float(w/length)};
}
inline Matrix BoneMatrix(const ClothBoneAsset &b){const double x=b.rotation.x,y=b.rotation.y,z=b.rotation.z,w=b.rotation.w;return {(1-2*y*y-2*z*z)*b.scale.x,(2*x*y+2*z*w)*b.scale.x,(2*x*z-2*y*w)*b.scale.x,0,(2*x*y-2*z*w)*b.scale.y,(1-2*x*x-2*z*z)*b.scale.y,(2*y*z+2*x*w)*b.scale.y,0,(2*x*z+2*y*w)*b.scale.z,(2*y*z-2*x*w)*b.scale.z,(1-2*x*x-2*y*y)*b.scale.z,0,b.position.x,b.position.y,b.position.z,1};}
inline Matrix MidReference(const Matrix &a,const Matrix &b){auto x=QuaternionOf(a),y=QuaternionOf(b);const double dot=x.x*y.x+x.y*y.y+x.z*y.z+x.w*y.w;const double sign=dot<0?-1.:1.;Quaternion q{float(x.x+y.x*sign),float(x.y+y.y*sign),float(x.z+y.z*sign),float(x.w+y.w*sign)};const double length=std::sqrt(q.x*q.x+q.y*q.y+q.z*q.z+q.w*q.w);q={float(q.x/length),float(q.y/length),float(q.z/length),float(q.w/length)};ClothBoneAsset v{};v.position={float((a[12]+b[12])*.5),float((a[13]+b[13])*.5),float((a[14]+b[14])*.5)};v.rotation=q;v.scale={1,1,1};return BoneMatrix(v);}
inline Matrix PanelReference(const Matrix &a,const Matrix &b,double t) {
  Need(std::isfinite(t)&&t>0&&t<1,"panel-reference-fraction");auto x=QuaternionOf(a),y=QuaternionOf(b);
  const double sign=x.x*y.x+x.y*y.y+x.z*y.z+x.w*y.w<0?-1.:1.;ClothBoneAsset v{};
  v.rotation={float(x.x*(1-t)+y.x*t*sign),float(x.y*(1-t)+y.y*t*sign),float(x.z*(1-t)+y.z*t*sign),float(x.w*(1-t)+y.w*t*sign)};
  auto &q=v.rotation;const double length=std::sqrt(q.x*q.x+q.y*q.y+q.z*q.z+q.w*q.w);q={float(q.x/length),float(q.y/length),float(q.z/length),float(q.w/length)};
  v.position={float(a[12]*(1-t)+b[12]*t),float(a[13]*(1-t)+b[13]*t),float(a[14]*(1-t)+b[14]*t)};v.scale={1,1,1};return BoneMatrix(v);
}
inline std::pair<size_t,size_t> ChannelLayout(const Value &mesh,int channel){const auto &vd=mesh.At("m_VertexData");const auto &list=vd.At("m_Channels").List();constexpr int widths[]{4,2,1,1,2,2,1,1,2,2,4,4};std::array<size_t,8> stride{},offset{};
  for(const auto &c:list){const auto s=c.At("stream").Int(),f=c.At("format").Int(),d=c.At("dimension").Int()&15;Need(s>=0&&s<8&&f>=0&&f<12,"auto-skin-channel");stride[size_t(s)]+=size_t(d)*widths[f];}size_t total=0;for(size_t s=0;s<8;++s){offset[s]=total;total=(total+stride[s]*size_t(vd.At("m_VertexCount").Int())+15)&~size_t(15);}const auto &c=list.at(channel);const auto s=size_t(c.At("stream").Int());return {offset[s]+size_t(c.At("offset").Int()),stride[s]};
}
struct SurfaceWeight {std::map<int,double> terms;double distance=1e100;};
inline void ClosestSegment(Point p,Point a,Point b,int ia,int ib,SurfaceWeight &best){const auto d=Sub(b,a);const double length=Dot(d,d);if(length<1e-12)return;const double t=(std::max)(0.,(std::min)(1.,Dot(Sub(p,a),d)/length));Point q{};for(int k=0;k<3;++k)q[k]=a[k]+t*d[k];const double distance=Distance(p,q);if(distance<best.distance){best.distance=distance;best.terms.clear();if(t<1-1e-10)best.terms[ia]=1-t;if(t>1e-10)best.terms[ib]=t;}}
inline void ClosestFace(Point p,const std::vector<Point> &points,const std::array<int,3> &f,SurfaceWeight &best){
  const auto a=points[f[0]],u=Sub(points[f[1]],a),v=Sub(points[f[2]],a),w=Sub(p,a);const double uu=Dot(u,u),vv=Dot(v,v),uv=Dot(u,v),det=uu*vv-uv*uv;
  if(det>1e-12*uu*vv){const double b=(Dot(w,u)*vv-Dot(w,v)*uv)/det,c=(Dot(w,v)*uu-Dot(w,u)*uv)/det;
    if(b>=0&&c>=0&&b+c<=1){Point q{};for(int k=0;k<3;++k)q[k]=a[k]+b*u[k]+c*v[k];const double d=Distance(p,q);if(d<best.distance){best.distance=d;best.terms.clear();const double weights[]{1-b-c,b,c};for(int k=0;k<3;++k)if(weights[k]>1e-10)best.terms[f[k]]=weights[k];}}}
  for(int k=0;k<3;++k)ClosestSegment(p,points[f[k]],points[f[(k+1)%3]],f[k],f[(k+1)%3],best);
}
template<class Permitted> inline SurfaceWeight PanelSupport(const PanelChart &panel,Point coordinate,size_t capacity,Permitted permitted) {
  SurfaceWeight best;const auto around=panel.Around(coordinate);const Point target{0,coordinate[1],0};
  for(const auto &f:panel.faces){if(!permitted(f[0])||!permitted(f[1])||!permitted(f[2]))continue;
    const double low=(std::min)({around[f[0]][0],around[f[1]][0],around[f[2]][0]}),high=(std::max)({around[f[0]][0],around[f[1]][0],around[f[2]][0]});
    if(!panel.open&&high-low>=3.14159265358979323846*panel.scale)continue;
    if(capacity>=3)ClosestFace(target,around,f,best);
    else if(capacity==2)for(int k=0;k<3;++k)ClosestSegment(target,around[f[k]],around[f[(k+1)%3]],f[k],f[(k+1)%3],best);
    else if(capacity==1)for(int b:f){const double distance=Distance(target,around[b]);if(distance<best.distance){best.distance=distance;best.terms={{b,1}};}}}
  return best;
}
using PackedSkin=std::map<int,int>;
inline PackedSkin SourceSkin(const MeshView &mesh,size_t vertex){PackedSkin result;for(size_t k=0;k<mesh.weights[vertex].size();++k){const double w=mesh.weights[vertex][k]*65535;const int q=int(std::llround(w));Need(std::abs(w-q)<.0001,"auto-skin-not-unorm16");if(q)result[int(mesh.indices[vertex][k])]+=q;}int sum=0;for(auto i:result){Need(i.second<=65535,"auto-skin-canonical-overflow");sum+=i.second;}Need(sum>0&&sum<=65536&&result.size()<=4,"auto-skin-packed-sum");return result;}
inline PackedSkin Quantize(const PackedSkin &protectedTerms,const SurfaceWeight &support,int total,const std::map<int,int> &binding) {
  Need(total>0&&support.terms.size()+protectedTerms.size()<=4,"auto-skin-influence-budget");PackedSkin result=protectedTerms;int used=0;std::vector<std::pair<double,int>> fractions;
  double sum=0;for(auto v:support.terms)sum+=v.second;Need(std::abs(sum-1)<1e-7,"auto-skin-support-partition");
  for(auto v:support.terms){const auto index=binding.at(v.first);Need(!protectedTerms.count(index),"auto-skin-protected-binding-alias");const double scaled=v.second*total;const int floor=int(std::floor(scaled));result[index]=floor;used+=floor;fractions.push_back({scaled-floor,index});}
  std::sort(fractions.begin(),fractions.end(),[](const auto &a,const auto &b){return a.first!=b.first?a.first>b.first:a.second<b.second;});Need(total-used>=0&&size_t(total-used)<=fractions.size(),"auto-skin-quantization");for(int n=0;n<total-used;++n)++result[fractions[n].second];
  for(auto i=result.begin();i!=result.end();)if(!i->second)i=result.erase(i);else {Need(i->second<=65535,"auto-skin-term-overflow");++i;}return result;
}
using PreciseSkin=std::map<int,double>;
inline PreciseSkin ReadSkin(const MeshView &mesh,size_t vertex,bool packed) {
  PreciseSkin out;if(packed){for(auto t:SourceSkin(mesh,vertex))out[t.first]=t.second;return out;}
  for(size_t k=0;k<mesh.weights[vertex].size();++k)if(mesh.weights[vertex][k]>0)out[int(mesh.indices[vertex][k])]+=mesh.weights[vertex][k];
  double sum=0;for(auto t:out){Need(double(float(t.second))==t.second,"auto-float-skin-duplicate-precision");sum+=t.second;}Need(out.size()<=4&&std::abs(sum-1)<.0001,"auto-float-skin-sum");return out;
}
inline PreciseSkin RedistributeSkin(const PreciseSkin &protectedTerms,const SurfaceWeight &support,double total,const std::map<int,int> &binding,bool packed) {
  PreciseSkin result;
  if(packed){PackedSkin fixed;for(auto t:protectedTerms)fixed[t.first]=int(t.second);for(auto t:Quantize(fixed,support,int(total),binding))result[t.first]=t.second;return result;}
  Need(total>0&&support.terms.size()+protectedTerms.size()<=4,"auto-float-skin-influence-budget");result=protectedTerms;double sum=0;
  for(auto t:support.terms){Need(!result.count(binding.at(t.first)),"auto-float-skin-protected-alias");const double w=float(total*t.second);if(w>0)result[binding.at(t.first)]=w;sum+=t.second;}
  Need(std::abs(sum-1)<1e-7,"auto-float-skin-partition");return result;
}
inline bool CompatibleNaturalBind(const Matrix &a,const Matrix &b,Point low,Point high) {
  for(int k=0;k<16;++k)if(!std::isfinite(a[k])||!std::isfinite(b[k])||
      ((k<12||k==15)&&std::abs(a[k]-b[k])>.0001))return false;
  for(int k=0;k<3;++k)if(!std::isfinite(low[k])||!std::isfinite(high[k])||low[k]>high[k])return false;
  for(int mask=0;mask<8;++mask){Point p{};for(int k=0;k<3;++k)p[k]=mask&(1<<k)?high[k]:low[k];if(Distance(Transform(a,p),Transform(b,p))>.0001)return false;}
  return true;
}
inline DenseMesh DenseBinding(Package &package,Scene &scene,const MeshView &mesh,const eiem_cloth_cache::Profile &base,DenseRecipe &recipe,
    const std::vector<Matrix> &world,const std::vector<Point> &points,const std::vector<ClothBoneAsset> &bones,const Graph &graph,
    const std::vector<unsigned char> *selected=nullptr,const PanelChart *panel=nullptr,int sourceSpan=1,const CoatWaistField *waistField=nullptr) {
  Need(!selected||selected->size()==mesh.world.size(),"auto-dense-region-size");
  Need(!recipe.view.resampledPanel||(SourcePanelContract(base.view)&&SourceSeraphPanel(base.view)),"long-panel-skin-source-contract");
  if(selected&&std::none_of(selected->begin(),selected->end(),[](unsigned char v){return v!=0;}))return {};
  auto ref=package.Resolve(scene.file,scene.file.Get(mesh.id).At("m_Mesh"),43);auto &file=*ref.first;const auto &source=file.Get(ref.second);const auto &channels=source.At("m_VertexData").At("m_Channels").List();
  const bool unorm=channels[12].At("format").Int()==4&&channels[13].At("format").Int()==6;
  const bool floating=channels[12].At("format").Int()==0&&channels[13].At("format").Int()==10;
  Need((unorm||floating)&&channels[12].At("dimension").Int()==4&&channels[13].At("dimension").Int()==4&&source.At("m_BonesPerVertex").Int()==4&&source.At("m_VariableBoneCountWeights").At("m_Data").List().empty(),"auto-dense-skin-layout-unsupported");
  DenseMesh out;auto &config=out.view;config.renderer=recipe.String(mesh.name);config.parent=recipe.String(mesh.parent);config.sourceBones=int(mesh.bones.size());
  const auto positions=Channel(source,0);Point low{1e100,1e100,1e100},high{-1e100,-1e100,-1e100};
  for(const auto &p:positions){Need(p.size()==3,"auto-dense-position-dimension");for(int k=0;k<3;++k){low[k]=(std::min)(low[k],p[k]);high[k]=(std::max)(high[k],p[k]);}}
  std::vector<int> ids;std::map<int,int> bindings;Matrix common{};bool commonKnown=false;
  for(size_t k=0;k<mesh.bones.size();++k){int native=-1;for(int n=0;n<base.view.boneCount;++n)if(scene.Name(mesh.bones[k])==base.bones[n].name&&scene.Name(scene.Parent(mesh.bones[k]))==base.bones[n].parentName)native=n;ids.push_back(native);if(native<0||base.view.Foreign(native))continue;Need(bindings.emplace(native,int(k)).second,"auto-dense-aliased-source-binding");
    const auto m=Mul(scene.World(mesh.bones[k]),mesh.binds[k]);if(!commonKnown){common=m;commonKnown=true;}else if(!recipe.view.sourceCoatWaist)Need(CompatibleNaturalBind(m,common,low,high),"auto-dense-incompatible-natural-bind-families");}
  Need(commonKnown,"auto-dense-no-cloth-binding");
  auto bindTarget=[&](int n){if(bindings.count(n))return;Need(ids.size()<256,"auto-dense-packed-index-overflow");bindings[n]=int(ids.size());ids.push_back(n);out.bindingIds.push_back(n);auto bind=Mul(Inverse(world[n]),common);std::array<float,16> value{};for(int k=0;k<16;++k)value[k]=float(bind[k]);out.binds.push_back(value);};
  CoatWaistChart waist;
  if(recipe.view.sourceCoatWaist) {
    Need(SourceInactiveCoat(base.view)&&world.size()==24&&bones.size()==24,"coat-waist-source-contract");
    int64_t spine=0,hip=0;
    for(auto t:scene.goTransform){const auto &name=scene.Name(t.second);
      if(name=="Bip001_Spine"){Need(!spine,"coat-waist-ambiguous-spine");spine=t.second;}
      if(name=="Bip001_Pelvis"){Need(!hip,"coat-waist-ambiguous-pelvis");hip=t.second;}}
    Need(spine&&hip&&scene.Parent(spine)==hip,"coat-waist-body-chain");
    waist.field=waistField;Need(waist.field,"coat-waist-source-field-missing");
    waist.spine=Transform(scene.World(spine),{});waist.up=Sub(waist.spine,Transform(scene.World(hip),{}));
    waist.height=std::sqrt(Dot(waist.up,waist.up));Need(waist.height>.001,"coat-waist-body-span");for(auto &v:waist.up)v/=waist.height;
    for(size_t k=0;k<mesh.bones.size();++k){const int n=ids[k];const auto &name=scene.Name(mesh.bones[k]);
      const char *trunk[]{"Bip001_Pelvis","Bip001_Spine","Bip001_Spine1","Bip001_Spine2"};
      for(int b=0;b<4;++b)if(name==trunk[b]) {
        Need(n<0&&waist.trunkBindings[b]<0,"coat-waist-body-alias");waist.trunk.insert(int(k));waist.trunkBindings[b]=int(k);}
      if(n>=0&&base.bones[n].attribute==1){Need(n%4==0&&n+1<24&&bones[n+1].parent==n,"coat-waist-root-path");
        const auto root=Transform(world[n],{}),child=Transform(world[n+1],{});const double span=Dot(Sub(root,child),waist.up);
        Need(span>.001,"coat-waist-root-span");waist.fixed[int(k)]={root,span};}}
  }
  auto vertexData=source.At("m_VertexData");auto packed=Blob(vertexData.At("m_DataSize"));const auto wo=ChannelLayout(source,12),io=ChannelLayout(source,13);
  std::set<std::array<int,2>> edges;for(const auto &f:graph.faces)for(int k=0;k<3;++k){std::array<int,2> e{f[k],f[(k+1)%3]};std::sort(e.begin(),e.end());if(bones[e[0]].attribute==2&&bones[e[1]].attribute==2)edges.insert(e);}for(auto e:graph.lines)if(bones[e[0]].attribute==2&&bones[e[1]].attribute==2)edges.insert(e);
  std::vector<PreciseSkin> oldSkin,newSkin;std::set<int> changed;size_t moving=0,capacitySkipped=0;
  auto skinAttribute=[&](int n){return recipe.view.resampledPanel&&n<base.view.boneCount?
      (base.bones[n].attribute?base.bones[n].attribute:1):bones[n].attribute;};
  for(size_t n=0;n<mesh.world.size();++n){if((n&255)==0)CheckCancel(package.vfs.cancel);auto original=ReadSkin(mesh,n,unorm);oldSkin.push_back(original);
    Point reference=mesh.world[n],coordinate{};bool panelVertex=false;
    PreciseSkin candidate;
    if(recipe.view.sourceCoatWaist){double sum=0;for(auto w:mesh.weights[n])sum+=w;for(auto &v:reference)v/=sum;
      candidate=CoatWaistSkin(waist,reference,original,unorm);newSkin.push_back(candidate);
      if(candidate==original)continue;++moving;
    } else {
    if(panel){double sum=0;for(auto w:mesh.weights[n])sum+=w;for(auto &v:reference)v/=sum;
      bool owned=false;for(auto term:original){const int id=ids[term.first];owned|=id>=0&&skinAttribute(id)>0;}
      if(owned){coordinate=panel->Coordinate(reference);panelVertex=coordinate[1]<panel->WaistHeight(coordinate[0])-1e-6;}}
    PreciseSkin protectedTerms;double total=0;for(auto term:original){const int id=ids[term.first];if(id>=0&&(skinAttribute(id)==2||(panelVertex&&skinAttribute(id)==1)))total+=term.second;else protectedTerms.insert(term);}
    if(!total||(selected&&!(*selected)[n])||(panel&&!panelVertex)){newSkin.push_back(original);continue;}++moving;const size_t capacity=4-protectedTerms.size();SurfaceWeight best;
    std::set<int> sourceColumns;for(auto term:original){const int id=ids[term.first];if(id>=0&&(skinAttribute(id)==2||(panelVertex&&skinAttribute(id)==1)))sourceColumns.insert(recipe.view.resampledPanel?recipe.columns[id]:bones[id].column);}
    auto permitted=[&](int id){if(recipe.view.ribbonSurface)return bones[id].column>=0&&bones[id].column<5;for(int c:sourceColumns){int d=std::abs(bones[id].column-c);if(recipe.view.loop)d=(std::min)(d,int(recipe.roots.size())-d);if(d<=(panel?2:sourceSpan))return true;}return false;};
    if(panel)best=PanelSupport(*panel,coordinate,capacity,permitted);
    else {
      if(capacity>=3)for(const auto &f:graph.faces)if(bones[f[0]].attribute==2&&bones[f[1]].attribute==2&&bones[f[2]].attribute==2&&permitted(f[0])&&permitted(f[1])&&permitted(f[2]))ClosestFace(mesh.world[n],points,f,best);
      if(capacity>=2)for(auto e:edges)if(permitted(e[0])&&permitted(e[1]))ClosestSegment(mesh.world[n],points[e[0]],points[e[1]],e[0],e[1],best);
      if(capacity==1){++capacitySkipped;newSkin.push_back(original);continue;}
    }
    Need(!best.terms.empty(),"auto-dense-free-surface-has-no-support");for(auto t:best.terms)bindTarget(t.first);candidate=RedistributeSkin(protectedTerms,best,total,bindings,unorm);newSkin.push_back(candidate);
    if(recipe.view.resampledPanel&&protectedTerms.empty()){
      Point anchor{},radial=Sub(reference,panel->origin);const double along=Dot(radial,panel->up);
      for(int k=0;k<3;++k)radial[k]-=along*panel->up[k];const double length=std::sqrt(Dot(radial,radial));
      Need(length>.001&&std::isfinite(length),"long-panel-contact-axis");
      double sum=0,sourceRadius=0;for(auto t:candidate){sum+=t.second;Need(ids[t.first]>=base.view.boneCount,"long-panel-contact-nonprivate-skin");sourceRadius+=recipe.radii.at(ids[t.first]-base.view.boneCount)*t.second;for(int k=0;k<3;++k)anchor[k]+=points[ids[t.first]][k]*t.second;}
      for(int k=0;k<3;++k)anchor[k]/=sum;
      const double inward=(std::max)(0.,-Dot(Sub(reference,anchor),radial)/length);
      Need(std::isfinite(inward)&&inward<.038,"long-panel-contact-envelope-too-thick");
      Need(std::isfinite(sourceRadius)&&sourceRadius>0,"long-panel-contact-source-radius");
      recipe.view.contactRadiusScale=(std::max)(recipe.view.contactRadiusScale,float((inward+.002)*sum/sourceRadius));
    }
    if(panelVertex){bool released=false;for(auto term:original){const int id=ids[term.first];released|=id>=0&&id<base.view.boneCount&&base.bones[id].attribute==1;}recipe.view.fixedSkinVertices+=released;}
    if(panelVertex&&recipe.view.resampledPanel){bool released=false;for(auto term:original){const int id=ids[term.first];released|=id>=0&&id<base.view.boneCount&&base.bones[id].attribute==0;}recipe.view.upperAnchorSkinVertices+=released;}
    }
    Point actual{};double sum=0;for(auto term:candidate){sum+=term.second;const Matrix bind=term.first<int(mesh.binds.size())?mesh.binds[term.first]:Matrix{};Matrix bm=bind;
      if(term.first>=int(mesh.binds.size()))for(int k=0;k<16;++k)bm[k]=out.binds[size_t(term.first)-mesh.binds.size()][k];
      const auto m=term.first<int(mesh.bones.size())?Mul(scene.World(mesh.bones[term.first]),bm):Mul(world[ids[term.first]],bm);const auto p=Transform(m,{positions[n][0],positions[n][1],positions[n][2]});for(int k=0;k<3;++k)actual[k]+=p[k]*term.second;}
    for(auto &v:actual)v/=sum;Point natural=mesh.world[n];double originalSum=0;for(double v:mesh.weights[n])originalSum+=v;for(auto &v:natural)v/=originalSum;
    Need(Distance(actual,natural)<=.0002,"auto-dense-natural-skin-drift");
    if(candidate==original)continue;changed.insert(int(n));size_t slot=0;
    const size_t weightBytes=unorm?2:4,indexBytes=unorm?1:4;
    auto writeSlot=[&](size_t at,int bone,double weight){const auto w=wo.first+n*wo.second+at*weightBytes,i=io.first+n*io.second+at*indexBytes;Need(w+weightBytes<=packed.size()&&i+indexBytes<=packed.size(),"auto-dense-skin-byte-range");uint32_t bits=uint32_t(weight);if(floating){const float value=float(weight);memcpy(&bits,&value,4);}for(size_t b=0;b<weightBytes;++b)packed[w+b]=uint8_t(bits>>(8*b));for(size_t b=0;b<indexBytes;++b)packed[i+b]=uint8_t(uint32_t(bone)>>(8*b));};
    for(auto term:candidate){Need(term.first>=0&&term.first<256,"auto-dense-index-range");writeSlot(slot++,term.first,term.second);}for(;slot<4;++slot)writeSlot(slot,0,0);
  }
  if(changed.empty()||(out.binds.empty()&&!recipe.view.sourceCoatWaist))return {};
  Need(changed.size()<=8192&&capacitySkipped*4<=moving,"auto-dense-insufficient-rebind-coverage");config.skinVertices=int(changed.size());
  if(unorm)out.weights.resize(newSkin.size());else out.floatWeights.resize(newSkin.size());out.indices.resize(newSkin.size());for(size_t n=0;n<newSkin.size();++n){size_t k=0;for(auto t:newSkin[n]){if(unorm)out.weights[n][k]=uint16_t(t.second);else out.floatWeights[n][k]=float(t.second);out.indices[n][k++]=uint8_t(t.first);}}
  std::vector<Matrix> allBind=mesh.binds;for(auto a:out.binds){Matrix m{};std::copy(a.begin(),a.end(),m.begin());allBind.push_back(m);}
  std::set<int> observed=changed;for(auto f:mesh.triangles)if(changed.count(f[0])||changed.count(f[1])||changed.count(f[2]))observed.insert(f.begin(),f.end());Need(observed.size()<=8192,"auto-dense-surface-observation-budget");
  std::map<int,int> foreign;for(int n:observed)for(const auto *skin:{&oldSkin[n],&newSkin[n]})for(auto term:*skin)if(ids[term.first]<0&&!foreign.count(term.first)){foreign[term.first]=int(bones.size()+out.foreign.size());out.foreign.push_back(term.first);}
  Need(bones.size()+out.foreign.size()<=eiem_cloth_skin::MaxMatrices,"auto-dense-observed-bone-budget");
  std::map<int,int> sampleIndex;for(int n:observed){sampleIndex[n]=int(out.samples.size());eiem_cloth_skin::Sample sample{};sample.vertex=n;sample.stable=true;
    auto terms=[&](const PreciseSkin &skin,eiem_cloth_skin::Influence (&dst)[4]){double sum=0;for(auto t:skin)sum+=t.second;size_t slot=0;for(auto t:skin){auto &v=dst[slot++];v.bone=ids[t.first]>=0?ids[t.first]:foreign.at(t.first);const auto p=Transform(allBind[t.first],{positions[n][0],positions[n][1],positions[n][2]});std::copy(p.begin(),p.end(),v.point);v.weight=t.second/sum;}};
    terms(oldSkin[n],sample.original);terms(newSkin[n],sample.candidate);out.samples.push_back(sample);}
  std::set<std::array<int,2>> renderEdges;for(auto f:mesh.triangles)if(changed.count(f[0])||changed.count(f[1])||changed.count(f[2]))for(int k=0;k<3;++k){std::array<int,2> e{f[k],f[(k+1)%3]};std::sort(e.begin(),e.end());renderEdges.insert(e);}for(auto e:renderEdges){const double rest=Distance(mesh.world[e[0]],mesh.world[e[1]]);if(rest>1e-8)out.edges.push_back({sampleIndex.at(e[0]),sampleIndex.at(e[1]),rest});}
  std::map<Bytes,int> copies;for(int n:observed){ByteWriter key;for(double v:positions[n]){uint64_t bits=0;memcpy(&bits,&v,8);key.U(bits,8);}for(auto t:oldSkin[n]){key.U(t.first,4);uint64_t bits=0;memcpy(&bits,&t.second,8);key.U(bits,8);}auto old=copies.emplace(key.bytes,n);
    if(!old.second){const int other=old.first->second;Need(newSkin[n]==newSkin[other],"auto-dense-seam-skin-diverged");out.seams.push_back({sampleIndex.at(other),sampleIndex.at(n)});}}
  auto bindValue=source.At("m_BindPose"),hashes=source.At("m_BoneNameHashes"),bounds=source.At("m_BonesAABB");
  Need(bindValue.List().size()==mesh.bones.size()&&hashes.List().size()==mesh.bones.size()&&bounds.List().size()==mesh.bones.size(),"auto-dense-binding-metadata-count");
  for(auto b:out.binds){Matrix matrix{};std::copy(b.begin(),b.end(),matrix.begin());bindValue.items.push_back(MatrixValue(matrix));hashes.items.push_back(Integer(0));Point lo{1e100,1e100,1e100},hi{-1e100,-1e100,-1e100};for(int n:changed){const auto p=Transform(matrix,{positions[n][0],positions[n][1],positions[n][2]});for(int k=0;k<3;++k){lo[k]=(std::min)(lo[k],p[k]);hi[k]=(std::max)(hi[k],p[k]);}}bounds.items.push_back(Object({{"m_Min",VectorValue(lo)},{"m_Max",VectorValue(hi)}}));}
  Field(vertexData,"m_DataSize")=Binary(std::move(packed));int64_t bundle=0;for(const auto &o:file.objects)if(file.Class(o.first)==142){Need(!bundle,"auto-private-container-ambiguous");bundle=o.first;}Need(bundle!=0,"auto-private-container-missing");const auto &ab=file.Get(bundle);std::vector<Value> entries;
  for(const auto &v:ab.At("m_Container").List())if(v.At("second").At("asset").LocalRef()==ref.second)entries.push_back(v);Need(entries.size()==1,"auto-private-mesh-container-entry");auto &entry=entries[0];auto &second=Field(entry,"second");Field(second,"preloadIndex")=Integer(0);Field(second,"preloadSize")=Integer(1);
  ByteWriter identity;identity.Raw(Blob(vertexData.At("m_DataSize")));for(auto b:out.binds)identity.Raw(reinterpret_cast<const uint8_t*>(b.data()),sizeof(b));identity.Z(file.sha);identity.Z(recipe.view.signature);identity.Z(mesh.name);identity.Z(mesh.parent);
  const auto signature=Digest(identity.bytes),name="eiem/auto-"+signature.substr(0,24)+".ab";config.signature=recipe.String(signature);config.bundleName=recipe.String(name);config.asset=recipe.String(entry.At("first").Text());
  auto cab=Repack(file,{{ref.second,{{"m_VertexData",vertexData},{"m_BindPose",bindValue},{"m_BoneNameHashes",hashes},{"m_BonesAABB",bounds},{"m_IsReadable",Integer(1)}}},{bundle,{{"m_Name",Text(name)},{"m_AssetBundleName",Text(name)},{"m_Container",Array(entries)},{"m_PreloadTable",Array({Pointer(ref.second)})}}}});
  out.payload=PrivateBundle(cab,"CAB-"+Digest(cab).substr(0,32));Need(eiem_cloth_resource::Fits(out.payload.size(),eiem_cloth_resource::RuntimeBytes),"auto-private-payload-budget");config.bytes=unsigned(out.payload.size());config.hash=eiem_cloth_cache::Hash(out.payload.data(),out.payload.size());out.Link();return out;
}
inline CoatWaistField SourceCoatWaistField(Package &package,Scene &scene,const std::vector<MeshView> &meshes) {
  const char *names[]{"Bip001_Pelvis","Bip001_Spine","Bip001_Spine1","Bip001_Spine2"};std::array<int64_t,4> trunk{};
  for(auto t:scene.goTransform)for(int n=0;n<4;++n)if(scene.Name(t.second)==names[n]){Need(!trunk[n],"coat-waist-field-body-alias");trunk[n]=t.second;}
  for(int n=0;n<4;++n)Need(trunk[n]&&(!n||scene.Parent(trunk[n])==trunk[n-1]),"coat-waist-field-body-chain");
  const auto spine=Transform(scene.World(trunk[1]),{});auto up=Sub(spine,Transform(scene.World(trunk[0]),{}));const double height=std::sqrt(Dot(up,up));
  Need(height>.001,"coat-waist-field-body-span");for(auto &v:up)v/=height;
  CoatWaistField out;
  for(const auto &m:meshes){CheckCancel(package.vfs.cancel);if(m.name.size()<5||m.name.substr(m.name.size()-5)!="_lod0")continue;
    std::vector<int> roles(m.bones.size(),-1);for(size_t b=0;b<m.bones.size();++b)for(int n=0;n<4;++n)if(m.bones[b]==trunk[n])roles[b]=n;
    std::vector<std::array<double,4>> skin(m.world.size());std::vector<Point> points(m.world.size());std::vector<bool> valid(m.world.size(),true);
    for(size_t v=0;v<m.world.size();++v){double sum=0;for(size_t k=0;k<m.weights[v].size();++k){const double w=m.weights[v][k];sum+=w;if(w<=0)continue;
        const int r=roles.at(int(m.indices[v][k]));if(r<0)valid[v]=false;else skin[v][r]+=w;}
      if(!valid[v]||sum<=0){valid[v]=false;continue;}points[v]=m.world[v];for(auto &x:points[v])x/=sum;for(auto &w:skin[v])w/=sum;
      const double h=Dot(Sub(points[v],spine),up)/height;valid[v]=h>-.75&&h<1.9;}
    for(auto f:m.triangles)if(valid[f[0]]&&valid[f[1]]&&valid[f[2]]){
      const auto normal=Cross(Sub(points[f[1]],points[f[0]]),Sub(points[f[2]],points[f[0]]));if(Dot(normal,normal)<1e-18)continue;
      out.faces.push_back({{points[f[0]],points[f[1]],points[f[2]]},{skin[f[0]],skin[f[1]],skin[f[2]]}});
      Need(out.faces.size()<=16384,"coat-waist-field-budget");}}
  out.Build();return out;
}
inline std::shared_ptr<DenseRecipe> GenerateCoatWaist(Package &package,Scene &scene,int64_t component,
    eiem_cloth_cache::Profile &base,const std::vector<int64_t> &ids,const std::vector<MeshView> &meshes) {
  const auto &p=base.view;Need(SourceInactiveCoat(p)&&!p.generatedLocal&&ids.size()==24&&meshes.size()<=16,"coat-waist-source-unconfirmed");
  auto out=std::make_shared<DenseRecipe>();auto &r=out->view;r.sourceCoatWaist=r.runtimeGenerated=r.multipleLod=true;r.loop=false;
  r.originalCount=24;r.originalRoots=6;r.depth=4;r.baseSignature=out->String(p.signature);r.prefabSha=out->String(p.prefabSha);
  const std::string identity=std::string("source-coat-waist-trunk-field-v2\n")+p.signature;r.signature=out->String(Digest(Bytes(identity.begin(),identity.end())));
  out->roots=base.roots;out->faces=base.faces;out->lines=base.lines;
  const auto &data=scene.file.Get(component).At("serializeData");out->radiusCurve=CurveSamples(data.At("radius"),true);
  out->distanceCurve=CurveSamples(data.At("distanceConstraint").At("stiffness"),false);
  std::vector<ClothBoneAsset> bones=base.bones;std::vector<Matrix> world;std::vector<Point> points;
  for(int n=0;n<24;++n){Need(scene.Name(ids[n])==bones[n].name&&scene.Name(scene.Parent(ids[n]))==bones[n].parentName,"coat-waist-source-order");
    bones[n].attribute=p.CandidateAttribute(n);out->columns.push_back(bones[n].column);world.push_back(scene.World(ids[n]));points.push_back(Transform(world.back(),{}));}
  Graph graph;graph.faces=base.faces[0];graph.lines=base.lines[0];size_t bytes=0,changed=0;
  const auto field=SourceCoatWaistField(package,scene,meshes);
  for(const auto &mesh:meshes){auto m=DenseBinding(package,scene,mesh,base,*out,world,points,bones,graph,nullptr,nullptr,1,&field);
    if(m.payload.empty())continue;Need(m.binds.empty()&&m.bindingIds.empty(),"coat-waist-unexpected-appended-binding");
    bytes+=m.payload.size();changed+=m.view.skinVertices;out->meshes.push_back(std::move(m));}
  Need(changed>0&&eiem_cloth_resource::Fits(bytes,eiem_cloth_resource::RuntimeBytes),"coat-waist-private-budget");out->Link();
  Need(r.CoatWaistSkinOnly(),"coat-waist-generated-contract");
  out->densityReport="source-coat-waist-trunk-field-skin meshes="+std::to_string(r.meshCount)+" vertices="+std::to_string(changed)+" referenceTriangles="+std::to_string(field.faces.size())+" addedBones=0 MoveWeightsUnchanged=1 bodyCapsuleWrites=0 visualVerified=0";
  return out;
}
inline double ResampleLongPanel(DenseRecipe &out,Scene &scene,int64_t parent,const std::vector<int> &sourceRoots,
    const std::vector<std::vector<int>> &source,std::vector<ClothBoneAsset> &bones,std::vector<Matrix> &world,
    std::vector<Point> &points,std::vector<std::vector<int>> &dense) {
  constexpr int columns=ClothLongPanelColumns,rows=ClothLongPanelRows;
  Need(source.size()==8&&bones.size()==40&&sourceRoots.size()==8,"long-panel-source-grid");
  for(const auto &chain:source)Need(chain.size()==4,"long-panel-source-depth");
  auto full=source;double maximumLength=0;
  for(size_t c=0;c<full.size();++c){const int root=bones[full[c][0]].parent;
    Need(root>=0&&size_t(root)<bones.size()&&bones[root].attribute==0&&bones[root].parent<0&&
        std::count(sourceRoots.begin(),sourceRoots.end(),root)==1&&bones[full[c][0]].attribute==1,"long-panel-upper-attachment-contract");
    full[c].insert(full[c].begin(),root);double length=0;
    for(size_t d=1;d<full[c].size();++d)length+=Distance(points[full[c][d-1]],points[full[c][d]]);
    Need(std::isfinite(length)&&length>0,"long-panel-upper-attachment-length");maximumLength=(std::max)(maximumLength,length);
    for(int n:full[c])out.columns[n]=int(std::lround(double(c)*columns/full.size()))%columns;
  }
  out.view.resampledPanel=true;
  for(auto &b:bones){b.attribute=0;b.column=b.depth=-1;}
  auto interpolate=[](const Matrix &a,const Matrix &b,double t){return t<1e-9?a:t>1-1e-9?b:PanelReference(a,b,t);};
  for(int c=0;c<columns;++c){const double x=double(c)*source.size()/columns;const int left=int(x),right=(left+1)%source.size();std::vector<int> chain;
    for(int row=0;row<rows;++row){const double y=double(row)*4/(rows-1);const int lo=(std::min)(3,int(y));
      auto at=[&](int d){return interpolate(world[full[left][d]],world[full[right][d]],x-left);};
      const auto wanted=interpolate(at(lo),at(lo+1),y-lo),parentWorld=row?world[chain.back()]:scene.World(parent),local=Mul(Inverse(parentWorld),wanted);
      ClothBoneAsset b{};b.name=out.String("EIEM_LongPanel_"+std::to_string(c)+"_"+std::to_string(row));b.parentName=row?bones[chain.back()].name:out.String(scene.Name(parent));b.parent=row?chain.back():-1;
      b.attribute=row?2:1;b.column=c;b.depth=row;b.position={float(local[12]),float(local[13]),float(local[14])};b.rotation=QuaternionOf(local);b.scale={1,1,1};
      const int n=int(bones.size());bones.push_back(b);out.added.push_back(b);out.parents.push_back(full[left][0]);world.push_back(Mul(parentWorld,BoneMatrix(b)));points.push_back(Transform(world.back(),{0,0,0}));chain.push_back(n);
    }out.roots.push_back(chain[0]);dense.push_back(chain);
  }
  Need(bones.size()==ClothLongPanelIdentities&&out.added.size()==ClothLongPanelParticles,"long-panel-candidate-budget");
  return maximumLength;
}
inline void FitLongPanelBody(Package &package,Scene &scene,DenseRecipe &out) {
  int64_t id=0;for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==137&&scene.Name(o.first)=="S_actor_seraph_cloth_03_lod0"){Need(!id,"long-panel-body-ambiguous");id=o.first;}
  Need(id!=0,"long-panel-body-missing");const auto ref=package.Resolve(scene.file,scene.file.Get(id).At("m_Mesh"),43);
  Need(ref.first->sha=="7f9683c2f2d86331f1ea56b88b49db5df3ffc50a08c30dc5b1a0e01cfba9ac83","long-panel-body-source-changed");
  const auto m=ReadMesh(package,scene,id);Need(m.world.size()==1238,"long-panel-body-shape-changed");
  out.bodyAsset={out.String(m.name),out.String(m.mesh),out.String(m.root),int(m.world.size()),m.submeshes,nullptr,0,out.String(m.parent)};
  for(size_t n=0;n<m.bones.size();++n){ClothBoneBinding b{out.String(scene.Name(m.bones[n])),out.String(scene.Name(scene.Parent(m.bones[n]))),-1,{}};for(int k=0;k<16;++k)b.bind[k]=float(m.binds[n][k]);out.bodyBindings.push_back(b);}
  for(const std::string side:{"L","R"}){const auto prefix="Bip001_"+side;int target=-1,mid=-1;std::set<int> group;
    for(size_t k=0;k<m.bones.size();++k){const auto name=scene.Name(m.bones[k]);if(name==prefix+"ThighTwist"){Need(target<0,"long-panel-body-bone-ambiguous");target=int(k);}if(name==prefix+"ThighTwist1")mid=int(k);
      if(name==prefix+"_Thigh"||name==prefix+"ThighTwist"||name==prefix+"ThighTwist1")group.insert(int(k));}
    Need(target>=0&&mid>=0&&group.size()==3&&scene.Name(scene.Parent(m.bones[target]))==prefix+"_Thigh","long-panel-body-parent-unconfirmed");
    const auto inverse=Inverse(scene.World(m.bones[target]));const auto middle=Transform(inverse,Transform(scene.World(m.bones[mid]),{0,0,0}));const double length=-middle[0]*2;
    Need(length>.2&&length<.65&&std::hypot(middle[1],middle[2])<.001,"long-panel-body-axis-unconfirmed");
    for(double fraction:{.04,.21,.38}){const double x=-length*fraction;Point low{1e100,1e100,1e100},high{-1e100,-1e100,-1e100};size_t count=0;
      for(size_t v=0;v<m.world.size();++v){double owned=0,sum=0;for(size_t k=0;k<m.weights[v].size();++k){sum+=m.weights[v][k];if(group.count(int(m.indices[v][k])))owned+=m.weights[v][k];}if(owned<.5||sum<=0)continue;
        Point p=m.world[v];for(auto &a:p)a/=sum;p=Transform(inverse,p);if(std::abs(p[0]-x)>length*.09)continue;++count;for(int k=1;k<3;++k){low[k]=(std::min)(low[k],p[k]);high[k]=(std::max)(high[k],p[k]);}}
      Need(count>=16,"long-panel-body-section-too-sparse");const double ry=(high[1]-low[1])*.5,rz=(high[2]-low[2])*.5,radius=(std::min)(ry,rz);const int major=ry>rz?1:2;
      Need(radius>length*.08&&radius<length*.25,"long-panel-body-section-invalid");
      for(double sign:{-1.,1.}){Point center{x,(low[1]+high[1])*.5,(low[2]+high[2])*.5};center[major]+=sign*((std::max)(ry,rz)-radius);
        out.bodySpheres.push_back({target,{float(center[0]),float(center[1]),float(center[2])},float(radius)});}
    }
  }
  Need(out.bodySpheres.size()==12,"long-panel-body-fit-count");out.view.bodyCoverage=true;
  for(const std::string side:{"L","R"}){const auto prefix="Bip001_"+side;int target=-1,mid=-1;std::set<int> group;
    for(size_t k=0;k<m.bones.size();++k){const auto name=scene.Name(m.bones[k]);
      if(name==prefix+"CalfTwist"||name==prefix+"_Calf"){Need(target<0,"long-panel-calf-bone-ambiguous");target=int(k);}
      if(name==prefix+"CalfTwist1")mid=int(k);
      if(name==prefix+"_Calf"||name==prefix+"CalfTwist"||name==prefix+"CalfTwist1")group.insert(int(k));}
    Need(target>=0&&mid>=0&&group.size()==2,"long-panel-calf-bindings-unconfirmed");
    auto ancestor=m.bones[mid];for(int steps=0;steps<2&&ancestor!=m.bones[target];++steps)ancestor=scene.Parent(ancestor);
    Need(ancestor==m.bones[target],"long-panel-calf-parent-unconfirmed");
    const auto inverse=Inverse(scene.World(m.bones[target]));const auto middle=Transform(inverse,Transform(scene.World(m.bones[mid]),{0,0,0}));const double length=-middle[0]*2;
    Need(length>.2&&length<.65&&std::hypot(middle[1],middle[2])<.001,"long-panel-calf-axis-unconfirmed");
    std::vector<Point> samples;Point low{1e100,1e100,1e100},high{-1e100,-1e100,-1e100};
    for(size_t v=0;v<m.world.size();++v){double owned=0,sum=0;for(size_t k=0;k<m.weights[v].size();++k){sum+=m.weights[v][k];if(group.count(int(m.indices[v][k])))owned+=m.weights[v][k];}
      if(owned<.5||sum<=0)continue;Point p=m.world[v];for(auto &a:p)a/=sum;p=Transform(inverse,p);
      if(p[0]>0||p[0]<-length)continue;samples.push_back(p);for(int k=1;k<3;++k){low[k]=(std::min)(low[k],p[k]);high[k]=(std::max)(high[k],p[k]);}}
    Need(samples.size()>=80,"long-panel-calf-surface-too-sparse");
    Point centers[2]{{-length*.12,0,0},{-length*.92,0,0}};double radii[2]{};
    for(int end=0;end<2;++end){Point lo{0,1e100,1e100},hi{0,-1e100,-1e100};size_t count=0;
      const auto selected=[&](Point p){return end?p[0]<-length*.75:p[0]>-length*.6;};
      for(const auto &p:samples)if(selected(p)){++count;for(int k=1;k<3;++k){lo[k]=(std::min)(lo[k],p[k]);hi[k]=(std::max)(hi[k],p[k]);}}
      Need(count>=16,"long-panel-calf-endpoint-too-sparse");for(int k=1;k<3;++k)centers[end][k]=(lo[k]+hi[k])*.5;
      for(const auto &p:samples)if(selected(p))radii[end]=(std::max)(radii[end],std::hypot(p[1]-centers[end][1],p[2]-centers[end][2]));
    }
    const auto center=centers[0];const double proximal=radii[0],distal=radii[1];
    Need(proximal>length*.08&&proximal<length*.22&&distal>length*.04&&distal<proximal,"long-panel-calf-envelope-invalid");
    const auto axis=Sub(centers[0],centers[1]);const double span=std::sqrt(Dot(axis,axis)),qw=1+axis[0]/span,qy=-axis[2]/span,qz=axis[1]/span,qn=std::sqrt(qw*qw+qy*qy+qz*qz);
    Need(std::isfinite(qn)&&qn>1,"long-panel-calf-axis-rotation");
    out.bodySpheres.push_back({target,{float(center[0]),float(center[1]),float(center[2])},float(proximal),float(distal),float(span+proximal+distal),{0,float(qy/qn),float(qz/qn),float(qw/qn)}});
  }
  Need(out.bodySpheres.size()==14,"long-panel-body-and-calf-fit-count");
}
inline void LongPanelResponse(Scene &scene,DenseRecipe &out,const std::vector<int64_t> &ids,
    const std::vector<Matrix> &world,const std::vector<Point> &points,const PanelChart &panel) {
  Need(out.view.resampledPanel&&ids.size()==40&&world.size()==ClothLongPanelIdentities,"long-response-source-contract");
  int64_t consumer=0;
  for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==114&&scene.Name(o.first)=="MC_Seraph_Skirt_Ribbon"){
    Need(!consumer,"long-response-consumer-ambiguous");consumer=o.first;}
  Need(consumer!=0,"long-response-consumer-missing");const auto &data=scene.file.Get(consumer).At("serializeData");
  const auto &roots=data.At("rootBones").List(),&colliders=data.At("colliderCollisionConstraint").At("colliderList").List();
  Need(roots.size()==1&&colliders.size()==3,"long-response-authored-relation");
  out.view.responseConsumer=out.String(scene.Name(consumer));out.view.responseRoot=out.String(scene.Name(roots[0].LocalRef()));
  Need(data.At("clothType").Int()==1&&data.At("connectionMode").Int()==0&&data.At("selfCollisionConstraint").At("selfMode").Int()==0&&
      data.At("selfCollisionConstraint").At("syncMode").Int()==0&&!data.At("selfCollisionConstraint").At("syncPartner").At("m_PathID").Int(),"long-response-source-contact-policy");
  for(int k=1;k<=5;++k){const auto name="dress_pd_L_a_0"+std::to_string(k)+"_jnt_ctrl";int64_t bone=0;
    for(auto b:scene.Branch({roots[0].LocalRef()}))if(scene.Name(b)==name){Need(!bone,"long-response-output-ambiguous");bone=b;}
    Need(bone!=0,"long-response-output-missing");out.responsePoints.push_back({out.String(name),out.String(scene.Name(scene.Parent(bone))),k==1?1:2});}
  for(const auto &ptr:colliders){const auto id=ptr.LocalRef(),transform=scene.TransformId(id);auto found=std::find(ids.begin(),ids.end(),transform);
    Need(found!=ids.end(),"long-response-collider-not-on-source");const int target=int(found-ids.begin());
    const auto &source=scene.file.Get(id);Need(source.At("direction").Int()==0,"long-response-axis-not-authored-X");
    ClothBoneResponseFrame response;auto &f=response.frame;f.target=target;
    const auto support=PanelSupport(panel,panel.Coordinate(points[target]),3,[](int n){return n>=40;});
    Need(!support.terms.empty()&&support.terms.size()<=3,"long-response-surface-support-missing");
    for(auto term:support.terms){const int k=f.count++;f.controls[k]=term.first;f.weights[k]=term.second;
      const auto relative=Mul(Inverse(world[term.first]),world[target]);std::copy(relative.begin(),relative.end(),f.offsets[k].v);}
    auto vector=[](const Value &v){return Vector3{float(v.At("x").Number()),float(v.At("y").Number()),float(v.At("z").Number())};};
    response.center=vector(source.At("center"));response.size=vector(source.At("size"));
    response.reverse=source.At("reverseDirection").Int()!=0;response.separated=source.At("radiusSeparation").Int()!=0;response.centered=source.At("alignedOnCenter").Int()!=0;
    Need(eiem_cloth_response::Valid(f,40,ClothLongPanelIdentities),"long-response-frame-invalid");out.responses.push_back(response);
  }
}
inline void LongPanelContactFaces(DenseRecipe &out,const std::vector<Point> &points,const PanelChart &panel) {
  std::map<std::array<int,3>,int> signs;
  for(const auto &graph:out.faces)for(auto f:graph){std::sort(f.begin(),f.end());
    const auto a=points.at(f[0]),b=points.at(f[1]),c=points.at(f[2]);auto radial=Sub(a,panel.origin);
    const double along=Dot(radial,panel.up);for(int k=0;k<3;++k)radial[k]-=along*panel.up[k];
    const auto normal=Cross(Sub(b,a),Sub(c,a));const double size=std::sqrt(Dot(normal,normal)*Dot(radial,radial)),side=Dot(normal,radial);
    Need(std::isfinite(size)&&size>1e-9&&std::abs(side)>size*.1,"long-response-face-side-ambiguous");
    const int sign=side>0?1:-1;auto added=signs.emplace(f,sign);Need(added.second||added.first->second==sign,"long-response-face-side-conflict");}
  Need(!signs.empty()&&signs.size()<=ClothBoneMaxFaceChoices,"long-response-oriented-face-budget");
  for(const auto &f:signs)out.responseFaces.push_back({f.first,f.second});
  out.view.contactProducer=out.view.responseConsumer;
}
}
#include "cloth_asset_strip_width.h"
namespace eiem_cloth_asset {
inline std::shared_ptr<DenseRecipe> GenerateDenseCandidate(Package &package,Scene &scene,int64_t component,eiem_cloth_cache::Profile &base,const std::vector<int64_t> &ids,const std::vector<MeshView> &meshes,
    const std::vector<std::vector<unsigned char>> &selected,const std::vector<int> &groups,int refinement=2,bool allowStripWidth=true) {
  Need(selected.empty()||selected.size()==meshes.size(),"auto-density-region-input");
  Need(!base.view.runtimeFixedForks,"auto-density-fixed-fork-requires-surface-recipe");
  Need(!base.view.candidateAttributes,"auto-density-source-selection-needs-surface-recipe");
  const auto &p=base.view;Need(p.rootCount<=16&&p.depth<=16&&p.dependencyCount==0&&meshes.size()<=16&&ids.size()==size_t(p.boneCount),"auto-density-structure-needs-specialized-recipe");
  const bool stripWidth=allowStripWidth&&SourceLegRibbonWidth(p)&&!groups.empty()&&selected.empty();
  const bool shortSkin=SourceShortContract(p),longPanel=SourceSeraphPanel(p),apronFit=SourceApronFit(p),panelFit=SourcePanelFit(p),fitted=apronFit||panelFit;const int divisions=fitted?3:refinement;
  Need(divisions>=2&&divisions<=4,"auto-density-refinement-range");
  Need(groups.empty()||(!fitted&&!p.loop&&groups.size()==size_t(p.rootCount)&&groups.front()==0&&groups.back()>=1&&groups.back()<ClothBoneMaxSeparatedPanels),"auto-separated-density-contract");
  for(size_t n=1;n<groups.size();++n)Need(groups[n]==groups[n-1]||groups[n]==groups[n-1]+1,"auto-separated-group-order");
  const auto sourceRoots=apronFit?p.roots:p.originalRoots;const auto parent=scene.Parent(ids[sourceRoots[0]]);for(int c=0;c<p.rootCount;++c)Need(scene.Parent(ids[sourceRoots[c]])==parent,"auto-density-independent-attachment-frames");
  Need(!panelFit||(SourcePanelContract(p)&&selected.empty()),"panel-fit-source-contract");
  Need(!apronFit||(SourceApronRelease(p,1)&&SourceApronRelease(p,4)&&!p.loop&&selected.empty()),"apron-fixed-boundary-contract");
  auto out=std::make_shared<DenseRecipe>();auto &r=out->view;r.runtimeGenerated=true;r.loop=p.loop;r.multipleLod=true;r.originalCount=p.boneCount;r.originalRoots=p.rootCount;r.depth=p.depth;r.bodyCoverage=false;r.sourcePanelFit=panelFit;r.sourceApronFit=apronFit;
  r.separatedPanels=groups.empty()?0:groups.back()+1;
  r.sourceShortSkin=shortSkin;
  r.baseSignature=out->String(p.signature);r.prefabSha=out->String(p.prefabSha);const std::string identity=std::string(!groups.empty()?"runtime-separated-panel-roots-lines-v2\n":shortSkin?"runtime-fixed-short-native-skin-v1\n":longPanel?"runtime-long-panel-knee-contour-v12\n":apronFit?"runtime-fixed-apron-ordered-volumes-v3\n":SourceChenPanel(p)?"runtime-source-waist-folded-panels-point-v1\n":panelFit?"runtime-fitted-waist-panels-v1\n":"runtime-simple-surface-regions-v2\n")+p.signature;r.signature=out->String(Digest(Bytes(identity.begin(),identity.end())));
  if(!fitted&&divisions!=2){const auto adaptive=identity+"\ncell-aspect-refinement-v1:"+std::to_string(divisions);r.signature=out->String(Digest(Bytes(adaptive.begin(),adaptive.end())));}
  if(stripWidth){const auto widthIdentity=identity+"\nnative-isolated-ribbon-width-v1";r.signature=out->String(Digest(Bytes(widthIdentity.begin(),widthIdentity.end())));}
  const auto &sd=scene.file.Get(component).At("serializeData");out->radiusCurve=CurveSamples(sd.At("radius"),true);out->distanceCurve=CurveSamples(sd.At("distanceConstraint").At("stiffness"),false);
  if(longPanel){Need(sd.At("connectionMode").Int()==0,"long-panel-source-not-line");
    for(float v:out->distanceCurve)Need(v==1,"long-panel-source-distance-changed");
    r.bendingStiffness=0;r.distanceStiffness=ClothLongPanelDistanceStiffness;r.tetherStretch=ClothLongPanelTetherStretch;
    out->distanceCurve.fill(r.distanceStiffness);}
  std::vector<Matrix> world;std::vector<Point> points;std::vector<ClothBoneAsset> bones=base.bones;std::vector<std::vector<int>> chains(p.rootCount,std::vector<int>(p.depth+(shortSkin?1:0),-1));double maxSpacing=0;
  for(int n=0;n<p.boneCount;++n){world.push_back(scene.World(ids[n]));if(!p.Passive(n))QuaternionOf(world.back());points.push_back(Transform(world.back(),{0,0,0}));
    if(p.Passive(n))bones[n].attribute=0;
    if(SourceSeparatedCoat(p)&&p.ReleasedFixed(n))bones[n].attribute=2;
    if(panelFit&&SourcePanelRelease(p,n))bones[n].attribute=2;
    if(panelFit){bones[n].column=SourcePanelColumn(p,n);bones[n].depth=SourcePanelDepth(p,n);if(SourceChenWaist(p,n))bones[n].attribute=1;}
    if(apronFit&&SourceApronRelease(p,n))bones[n].attribute=2;
    if(shortSkin){bones[n].column=SourceShortColumn(p,n);bones[n].depth=SourceShortDepth(p,n);bones[n].attribute=bones[n].depth?2:1;}
    if(bones[n].attribute){Need(bones[n].column>=0&&bones[n].depth>=0,"auto-density-source-label");chains[bones[n].column][bones[n].depth]=n;}
    out->columns.push_back(bones[n].column<0?-1:bones[n].column*divisions+(groups.empty()?0:groups[bones[n].column]));bones[n].column=out->columns.back();}
  for(auto &chain:chains){while(!chain.empty()&&chain.back()<0)chain.pop_back();Need(chain.size()>=2,"auto-density-source-chain");for(size_t d=1;d<chain.size();++d)Need(chain[d]>=0&&chain[d-1]>=0,"auto-density-source-chain");}
  const auto originalLengths=NativeFixedPathLengths(bones,points);const double maxLength=*std::max_element(originalLengths.begin(),originalLengths.end());
  Need(maxLength>1e-6,"auto-density-zero-move-length");
  const int pairs=p.loop?p.rootCount:p.rootCount-1;
  size_t addedCount=0;for(int c=0;c<pairs;++c){if(!groups.empty()&&groups[c]!=groups[c+1]){addedCount+=2;continue;}const size_t length=(std::min)(chains[c].size(),chains[(c+1)%p.rootCount].size());addedCount+=length*(divisions-1);for(size_t d=1;d<length;++d)maxSpacing=(std::max)(maxSpacing,Distance(points[chains[c][d]],points[chains[(c+1)%p.rootCount][d]]));}
  Need(maxSpacing>.05,"auto-density-original-spacing-sufficient");Need(p.boneCount+addedCount<=128,"auto-density-particle-budget");const auto prefix="EIEM_Auto_"+std::string(p.signature).substr(0,12)+"_";
  std::vector<std::vector<int>> dense,panelChains;double permittedLength=maxLength;
  if(longPanel){permittedLength=ResampleLongPanel(*out,scene,parent,std::vector<int>(sourceRoots,sourceRoots+p.rootCount),chains,bones,world,points,dense);panelChains=dense;FitLongPanelBody(package,scene,*out);}
  else for(int c=0;c<p.rootCount;++c){dense.push_back(chains[c]);out->roots.push_back(chains[c][0]);if(c>=pairs)continue;const int next=(c+1)%p.rootCount;
    if(!groups.empty()&&groups[c]!=groups[next]) {
      for(int edge:{c,next}){const int source=chains[edge][0],n=int(bones.size());Need(bones[source].attribute==1,"auto-separated-boundary-not-fixed");
        const auto parentWorld=scene.World(parent),local=Mul(Inverse(parentWorld),world[source]);ClothBoneAsset b{};
        b.name=out->String(prefix+"gap_"+std::to_string(c)+"_"+std::to_string(edge));b.parentName=out->String(scene.Name(parent));b.parent=-1;b.attribute=1;
        b.column=bones[chains[c][0]].column+(edge==c?1:2);b.depth=0;b.position={float(local[12]),float(local[13]),float(local[14])};b.rotation=QuaternionOf(local);b.scale={1,1,1};
        bones.push_back(b);out->added.push_back(b);out->parents.push_back(sourceRoots[edge]);world.push_back(Mul(parentWorld,BoneMatrix(b)));points.push_back(Transform(world.back(),{0,0,0}));
        Need(Distance(points.back(),points[source])<.000001,"auto-separated-root-reference-drift");dense.push_back({n});out->roots.push_back(n);}
      continue;
    }
    for(int step=1;step<divisions;++step){std::vector<int> chain;
      for(size_t d=0;d<(std::min)(chains[c].size(),chains[next].size());++d){const int n=int(bones.size());auto wanted=(fitted||divisions!=2)?PanelReference(world[chains[c][d]],world[chains[next][d]],double(step)/divisions):MidReference(world[chains[c][d]],world[chains[next][d]]);const auto parentWorld=d?world[chain.back()]:scene.World(parent);const auto local=Mul(Inverse(parentWorld),wanted);ClothBoneAsset b{};
        b.name=out->String(prefix+std::to_string(c)+"_"+((fitted||divisions!=2)?std::to_string(step)+"_":"")+std::to_string(d));b.parentName=d?bones[chain.back()].name:out->String(scene.Name(parent));b.parent=d?chain.back():-1;
        b.attribute=fitted?(d==0?1:2):(bones[chains[c][d]].attribute==1&&bones[chains[next][d]].attribute==1?1:2);b.column=bones[chains[c][0]].column+step;b.depth=int(d);b.position={float(local[12]),float(local[13]),float(local[14])};b.rotation=QuaternionOf(local);b.scale={1,1,1};bones.push_back(b);out->added.push_back(b);out->parents.push_back(sourceRoots[c]);world.push_back(Mul(parentWorld,BoneMatrix(b)));points.push_back(Transform(world.back(),{0,0,0}));chain.push_back(n);
      }dense.push_back(chain);out->roots.push_back(chain[0]);if(panelFit)panelChains.push_back(chain);}}
  std::vector<int> region(bones.size(),-1);std::vector<std::vector<unsigned char>> stripSelected;
  if(!groups.empty())for(int c=0;c<p.rootCount;++c){const int begin=bones[chains[c][0]].column;for(size_t n=0;n<bones.size();++n)if(bones[n].column==begin)region[n]=groups[c];
    if(c+1<p.rootCount&&groups[c]==groups[c+1])for(size_t n=0;n<bones.size();++n)if(bones[n].column==begin+1)region[n]=groups[c];}
  if(stripWidth)ExpandSeparatedStrips(scene,*out,base,ids,meshes,groups,chains,parent,bones,world,points,dense,region,stripSelected);
  PanelChart panel;if(panelFit)panel=MakePanelChart(points,panelChains);if(apronFit)panel=MakeApronChart(points,dense);
  if(longPanel)LongPanelResponse(scene,*out,ids,world,points,panel);
  const auto candidateLengths=NativeFixedPathLengths(bones,points);
  const double nativeMax=*std::max_element(candidateLengths.begin(),candidateLengths.end());
  for(size_t n=0;n<out->added.size();++n){const double length=candidateLengths[p.boneCount+n];Need(stripWidth||length<=permittedLength+.00001,"auto-density-changes-maximum-length");out->radii.push_back(CurveAt(out->radiusCurve,length/((longPanel||stripWidth)?nativeMax:maxLength)));}
  for(bool reverse:{false,true}){const auto g=NativeGraph(points,bones,int(dense.size()),p.loop,reverse);if(out->faces.empty()||g.faces!=out->faces[0]||g.lines!=out->lines[0]){out->faces.push_back(g.faces);out->lines.push_back(g.lines);}}
  if(!groups.empty()) {
    Need(dense.size()<=32,"auto-separated-native-root-budget");
    for(const auto &fs:out->faces)for(auto f:fs)Need(region[f[0]]>=0&&region[f[0]]==region[f[1]]&&region[f[1]]==region[f[2]],"auto-separated-native-graph-crosses-opening");
    for(const auto &ls:out->lines)for(auto e:ls)Need((region[e[0]]>=0&&region[e[0]]==region[e[1]])||
        (bones[e[0]].attribute==1&&bones[e[1]].attribute==1&&bones[e[0]].depth==0&&bones[e[1]].depth==0&&
         std::abs(bones[e[0]].column-bones[e[1]].column)==1),"auto-separated-native-line-crosses-opening");
  }
  if(longPanel){LongPanelContactFaces(*out,points,panel);Need(base.nativeProducers.empty(),"long-response-existing-producer");}
  for(size_t c=0;c<dense.size();++c){if(!p.loop&&c+1==dense.size())break;const auto &next=dense[(c+1)%dense.size()];for(size_t d=1;d<(std::min)(dense[c].size(),next.size());++d){std::array<int,2> e{dense[c][d],next[d]};if(bones[e[0]].attribute==1&&bones[e[1]].attribute==1)continue;std::sort(e.begin(),e.end());for(const auto &fs:out->faces){bool found=false;for(auto f:fs)found|=std::find(f.begin(),f.end(),e[0])!=f.end()&&std::find(f.begin(),f.end(),e[1])!=f.end();Need(found,"auto-density-missing-native-cross-connection");}out->cross.push_back(e);}}
  Graph graph{out->faces[0],out->lines[0]};size_t resultBytes=0;if((groups.empty()||stripWidth)&&!shortSkin)for(size_t k=0;k<meshes.size();++k){auto mesh=DenseBinding(package,scene,meshes[k],base,*out,world,points,bones,graph,stripWidth?&stripSelected[k]:selected.empty()?nullptr:&selected[k],fitted?&panel:nullptr,stripWidth?2:divisions-1);if(!mesh.view.skinVertices)continue;out->meshes.push_back(std::move(mesh));const auto &m=out->meshes.back();resultBytes+=m.payload.size()+m.samples.size()*sizeof(eiem_cloth_skin::Sample)+m.edges.size()*sizeof(eiem_cloth_skin::Edge)+m.seams.size()*sizeof(eiem_cloth_skin::Seam)+m.weights.size()*8+m.floatWeights.size()*16+m.indices.size()*4;Need(resultBytes<=16*1024*1024,"auto-density-result-byte-budget");}
  Need(!stripWidth||!out->meshes.empty(),"strip-width-no-private-skin");
  Need(shortSkin||!groups.empty()||!out->meshes.empty(),"auto-density-no-private-mesh");
  if(longPanel){Need(std::isfinite(r.contactRadiusScale)&&r.contactRadiusScale>0&&r.contactRadiusScale<1,"long-panel-contact-envelope-missing");
    for(auto &v:out->radiusCurve)v*=r.contactRadiusScale;for(auto &v:out->radii)v*=r.contactRadiusScale;
    r.fittedContactRadius=*std::max_element(out->radiusCurve.begin(),out->radiusCurve.end());
    Need(r.fittedContactRadius>=.002f&&r.fittedContactRadius<.04f,"long-panel-contact-envelope-too-wide");}
  out->Link();
  if(longPanel){base.nativeProducers.push_back(out->view.responseConsumer);base.Link();}
  return out;
}
inline std::shared_ptr<DenseRecipe> GenerateDense(Package &package,Scene &scene,int64_t component,eiem_cloth_cache::Profile &base,const std::vector<int64_t> &ids,const std::vector<MeshView> &meshes,
    const std::vector<std::vector<unsigned char>> &selected={},const std::vector<int> &groups={}) {
  const auto &p=base.view;
  if(!groups.empty()&&SourceLegRibbonWidth(p))try{return GenerateDenseCandidate(package,scene,component,base,ids,meshes,selected,groups);}
    catch(const std::exception &e){CheckCancel(package.vfs.cancel);auto fallback=GenerateDenseCandidate(package,scene,component,base,ids,meshes,selected,groups,2,false);
      fallback->densityReport=std::string("auto-native-strip-width-unavailable-original-panels-retained:")+e.what();return fallback;}
  if(!groups.empty()||SourcePanelFit(p)||SourceApronFit(p)||SourceShortContract(p))return GenerateDenseCandidate(package,scene,component,base,ids,meshes,selected,groups);
  ColumnDensityPlan plan;std::string fallback;
  try{Need(ids.size()==base.bones.size()&&p.rootCount>=2&&p.rootCount<=16&&p.depth>=2&&p.depth<=16,"auto-density-plan-source");
    std::vector<Point> points;std::vector<int> attributes;std::vector<std::vector<int>> chains(p.rootCount);
    for(size_t n=0;n<ids.size();++n){points.push_back(Transform(scene.World(ids[n]),{0,0,0}));const auto &b=base.bones[n];attributes.push_back(p.Passive(int(n))?0:b.attribute);
      if(attributes.back()){Need(b.column>=0&&b.column<p.rootCount,"auto-density-plan-column");chains[b.column].push_back(int(n));}}
    for(auto &c:chains)std::sort(c.begin(),c.end(),[&](int a,int b){return base.bones[a].depth<base.bones[b].depth;});
    plan=PlanColumnDensity(points,chains,attributes,p.loop);
  }catch(const std::exception &e){CheckCancel(package.vfs.cancel);fallback=e.what();}
  if(plan.divisions>2)try{auto out=GenerateDenseCandidate(package,scene,component,base,ids,meshes,selected,groups,plan.divisions);
    out->densityReport="auto-cell-aspect-refinement divisions="+std::to_string(plan.divisions)+" worstSourceAspect="+std::to_string(plan.aspect)+" cells="+std::to_string(plan.cells)+" budgetLimited="+std::to_string(plan.budgetLimited);return out;
  }catch(const std::exception &e){CheckCancel(package.vfs.cancel);fallback=e.what();}
  auto out=GenerateDenseCandidate(package,scene,component,base,ids,meshes,selected,groups);
  out->densityReport="auto-cell-aspect-refinement divisions=2 worstSourceAspect="+std::to_string(plan.aspect)+" cells="+std::to_string(plan.cells)+" budgetLimited="+std::to_string(plan.budgetLimited);
  if(!fallback.empty())out->densityReport+=" fallback="+fallback;return out;
}
}
