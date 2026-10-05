#pragma once
namespace eiem_cloth_asset {
struct LayerEnvelopeFit {
  double scale=0,required=0;size_t samples=0;bool capped=false;
  void Sample(double outward,double radius,double original) {
    Need(std::isfinite(outward)&&std::abs(outward)<.15&&std::isfinite(radius)&&radius>0&&radius<=.1&&
        std::isfinite(original)&&original>0&&original<=radius,"layer-envelope-invalid-sample");
    const double wanted=(std::max)(0.,outward)+original;
    required=(std::max)(required,wanted);scale=(std::max)(scale,wanted/radius);++samples;
  }
  double Finish(const std::array<float,16> &radius,const std::array<float,16> &original) {
    Need(samples>=100&&samples<=100000&&scale>0&&std::isfinite(scale),"layer-envelope-source-sparse");
    for(int n=0;n<16;++n){Need(std::isfinite(radius[n])&&radius[n]>0&&radius[n]<=.1f&&
        std::isfinite(original[n])&&original[n]>0&&original[n]<=radius[n],"layer-envelope-invalid-curve");
      scale=(std::max)(scale,double(original[n])/radius[n]);}
    capped=scale>1;scale=(std::min)(1.,scale);return scale;
  }
};
struct LayerClearanceFit {
  double scale=0,limit=1,gap=0;size_t checks=0,preloaded=0;int kind=0;
};
constexpr double LayerClearanceSlack=.0001;
inline double LayerSegmentDistance(Point a,Point b,Point c,Point d) {
  const auto u=Sub(b,a),v=Sub(d,c),w=Sub(a,c);
  const double aa=Dot(u,u),bb=Dot(u,v),cc=Dot(v,v),dd=Dot(u,w),ee=Dot(v,w);
  Need(aa>1e-12&&cc>1e-12&&std::isfinite(aa+bb+cc+dd+ee),"layer-clearance-invalid-edge");
  auto clamp=[](double t){return (std::max)(0.,(std::min)(1.,t));};
  double best=1e100;
  auto sample=[&](double s,double t){Point delta{};for(int k=0;k<3;++k)delta[k]=w[k]+s*u[k]-t*v[k];best=(std::min)(best,Dot(delta,delta));};
  for(double s:{0.,1.})sample(s,clamp((ee+s*bb)/cc));
  for(double t:{0.,1.})sample(clamp((t*bb-dd)/aa),t);
  const double denominator=aa*cc-bb*bb;
  if(denominator>1e-12*aa*cc){const double s=(bb*ee-cc*dd)/denominator,t=(aa*ee-bb*dd)/denominator;
    if(s>=0&&s<=1&&t>=0&&t<=1)sample(s,t);}
  return std::sqrt(best);
}
struct LayerClearancePrimitives {
  std::set<int> vertices;std::set<std::array<int,2>> edges;
  LayerClearancePrimitives(const std::vector<Point> &points,const std::vector<ClothBoneResponseFace> &faces,const std::vector<double> &radii) {
    Need(points.size()>=3&&points.size()<=128&&radii.size()==points.size()&&!faces.empty()&&faces.size()<=512,"layer-clearance-surface-budget");
    for(size_t n=0;n<points.size();++n){for(double x:points[n])Need(std::isfinite(x),"layer-clearance-invalid-point");
      Need(std::isfinite(radii[n])&&radii[n]>0&&radii[n]<=.1,"layer-clearance-invalid-radius");}
    for(const auto &face:faces){const auto f=face.ids;
      Need(f[0]>=0&&f[1]>=0&&f[2]>=0&&size_t(f[0])<points.size()&&size_t(f[1])<points.size()&&size_t(f[2])<points.size(),"layer-clearance-invalid-face");
      const auto normal=Cross(Sub(points[f[1]],points[f[0]]),Sub(points[f[2]],points[f[0]]));
      Need(Dot(normal,normal)>1e-16,"layer-clearance-degenerate-face");vertices.insert(f.begin(),f.end());
      for(int n=0;n<3;++n){std::array<int,2> e{f[n],f[(n+1)%3]};std::sort(e.begin(),e.end());edges.insert(e);}}
  }
};
inline LayerClearanceFit FitLayerClearance(const std::vector<Point> &inner,const std::vector<ClothBoneResponseFace> &innerFaces,
    const std::vector<double> &innerRadii,const std::vector<Point> &outer,const std::vector<ClothBoneResponseFace> &outerFaces,
    const std::vector<double> &outerRadii,double requested,double minimum) {
  Need(std::isfinite(requested)&&std::isfinite(minimum)&&minimum>0&&minimum<=requested&&requested<=1,"layer-clearance-invalid-scale");
  const LayerClearancePrimitives a(inner,innerFaces,innerRadii),b(outer,outerFaces,outerRadii);
  Need(a.vertices.size()*outerFaces.size()+b.vertices.size()*innerFaces.size()+a.edges.size()*b.edges.size()<=1000000,"layer-clearance-pair-budget");
  LayerClearanceFit fit;
  auto sample=[&](double distance,double shortRadius,double longRadius,int kind){
    Need(std::isfinite(distance)&&distance>=0,"layer-clearance-invalid-distance");++fit.checks;
    fit.preloaded+=distance+1e-8<shortRadius*requested+longRadius;
    const double limit=(distance-longRadius-LayerClearanceSlack)/shortRadius;
    if(limit<fit.limit){fit.limit=limit;fit.gap=distance;fit.kind=kind;}};
  for(int p:a.vertices)for(const auto &f:outerFaces){SurfaceWeight closest;ClosestFace(inner[p],outer,f.ids,closest);
    sample(closest.distance,innerRadii[p],(std::max)({outerRadii[f.ids[0]],outerRadii[f.ids[1]],outerRadii[f.ids[2]]}),1);}
  for(int p:b.vertices)for(const auto &f:innerFaces){SurfaceWeight closest;ClosestFace(outer[p],inner,f.ids,closest);
    sample(closest.distance,(std::max)({innerRadii[f.ids[0]],innerRadii[f.ids[1]],innerRadii[f.ids[2]]}),outerRadii[p],2);}
  for(auto e:a.edges)for(auto f:b.edges)sample(LayerSegmentDistance(inner[e[0]],inner[e[1]],outer[f[0]],outer[f[1]]),
      (std::max)(innerRadii[e[0]],innerRadii[e[1]]),(std::max)(outerRadii[f[0]],outerRadii[f[1]]),3);
  Need(fit.checks>0&&fit.limit>=minimum,"layer-clearance-below-original-contact");
  fit.scale=(std::min)(requested,fit.limit);return fit;
}
inline void FitShortLayerEnvelope(Package &package,Scene &scene,const eiem_cloth_cache::Profile &source,
    DenseRecipe &out,const std::vector<Point> &points,const std::array<float,16> &original,
    const std::vector<Point> &outer,const std::vector<ClothBoneResponseFace> &outerFaces,const std::array<float,16> &outerThickness) {
  Need(SourceShortSides(source.view)&&out.view.sourceShortSides&&points.size()==36&&!out.layerFaces.empty(),"layer-envelope-source-contract");
  std::vector<ClothBoneAsset> bones=source.bones;
  for(int n=0;n<18;++n)bones[n].attribute=SourceShortWaist(source.view,n)?1:2;
  bones.insert(bones.end(),out.added.begin(),out.added.end());
  const auto lengths=NativeFixedPathLengths(bones,points);const double maximum=*std::max_element(lengths.begin(),lengths.end());
  Need(maximum>.05&&maximum<1,"layer-envelope-native-depth");
  std::vector<Point> normals;
  for(const auto &f:out.layerFaces){const auto n=Cross(Sub(points[f.ids[1]],points[f.ids[0]]),Sub(points[f.ids[2]],points[f.ids[0]]));
    const double length=std::sqrt(Dot(n,n));Need(length>1e-8&&std::isfinite(length),"layer-envelope-degenerate-face");
    normals.push_back({n[0]*f.outside/length,n[1]*f.outside/length,n[2]*f.outside/length});}
  LayerEnvelopeFit fit;
  for(size_t k=0;k<source.renderers.size();++k){CheckCancel(package.vfs.cancel);const auto &asset=source.renderers[k];int64_t renderer=0;
    for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==137&&scene.Name(o.first)==asset.name&&
        scene.Name(scene.Parent(scene.TransformId(o.first)))==(asset.parent?asset.parent:"")){Need(!renderer,"layer-envelope-renderer-ambiguous");renderer=o.first;}
    Need(renderer!=0,"layer-envelope-renderer-missing");const auto mesh=ReadMesh(package,scene,renderer);
    std::vector<bool> owned(mesh.bones.size());for(size_t b=0;b<mesh.bones.size();++b)for(const auto &bone:source.bones)
      if(scene.Name(mesh.bones[b])==bone.name&&scene.Name(scene.Parent(mesh.bones[b]))==bone.parentName)owned[b]=true;
    std::vector<bool> used(mesh.world.size());for(auto f:mesh.triangles)for(int n:f)used[n]=true;
    for(size_t v=0;v<mesh.world.size();++v){if(!used[v])continue;double sum=0;bool pure=true;
      for(size_t b=0;b<mesh.weights[v].size();++b)if(mesh.weights[v][b]>0){sum+=mesh.weights[v][b];pure&=owned.at(size_t(mesh.indices[v][b]));}
      if(!pure||sum<=0)continue;auto p=mesh.world[v];for(auto &x:p)x/=sum;
      SurfaceWeight nearest;size_t face=0;
      for(size_t n=0;n<out.layerFaces.size();++n){const double before=nearest.distance;ClosestFace(p,points,out.layerFaces[n].ids,nearest);if(nearest.distance<before)face=n;}
      Need(!nearest.terms.empty()&&std::isfinite(nearest.distance)&&nearest.distance<.15,"layer-envelope-unrepresented-source");
      Point q{};double depth=0;for(auto term:nearest.terms){for(int axis=0;axis<3;++axis)q[axis]+=points[term.first][axis]*term.second;depth+=lengths[term.first]/maximum*term.second;}
      fit.Sample(Dot(Sub(p,q),normals[face]),CurveAt(out.radiusCurve,depth),CurveAt(original,depth));
    }
  }
  const double requested=fit.Finish(out.radiusCurve,original);double minimum=0;
  for(int n=0;n<16;++n){minimum=(std::max)(minimum,double(original[n])/out.radiusCurve[n]);
    Need(std::isfinite(outerThickness[n])&&outerThickness[n]==outerThickness[0]&&outerThickness[n]>0,"layer-clearance-outer-curve-changed");}
  std::vector<double> radii;for(double length:lengths)radii.push_back(CurveAt(out.radiusCurve,length/maximum));
  CheckCancel(package.vfs.cancel);
  const auto clearance=FitLayerClearance(points,out.layerFaces,radii,outer,outerFaces,std::vector<double>(outer.size(),outerThickness[0]),requested,minimum);
  auto &v=out.view;v.layerContactScale=float(clearance.scale);
  if(double(v.layerContactScale)>clearance.scale)v.layerContactScale=std::nextafter(v.layerContactScale,0.f);
  v.layerContactRequired=float(fit.required);v.layerContactSamples=fit.samples;
  v.layerContactCapped=fit.capped||clearance.scale<requested;
  v.layerContactClearanceLimit=float(clearance.limit);v.layerContactClearanceGap=float(clearance.gap);
  v.layerContactClearanceChecks=clearance.checks;v.layerContactPreloaded=clearance.preloaded;v.layerContactClearanceKind=clearance.kind;
}
}
