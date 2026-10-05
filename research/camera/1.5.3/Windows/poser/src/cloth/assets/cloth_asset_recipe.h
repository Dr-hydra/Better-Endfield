#pragma once
#include "cloth_asset_prebuild.h"
#include "cloth_asset_model.h"
#include "cloth_asset_coat_inputs.h"
#include "../resources/cloth_bonecloth_cache.h"
#include "cloth_asset_graph.h"
#include "cloth_asset_dense.h"
#include "cloth_asset_selection.h"
#include "cloth_asset_surface.h"
#include "cloth_asset_leg_coverage.h"
#include "cloth_asset_fixed_tree.h"

namespace eiem_cloth_asset {
struct BoneKey {std::string name,parent;bool operator==(const BoneKey &b)const{return name==b.name&&parent==b.parent;}};
struct LiveBinding {BoneKey bone;Matrix bind{};};
struct LiveRenderer {std::string name,parent,mesh,root;int vertices=0,submeshes=0;std::vector<LiveBinding> bones;bool offlineBinding=false;};
struct LiveCloth {std::string name;std::vector<BoneKey> roots;};
struct Query {std::vector<LiveCloth> cloths;std::vector<LiveRenderer> renderers;std::set<std::pair<std::string,std::string>> reservedRenderers;BoneKey hips,leftThigh,rightThigh,spine,chest,upperChest,leftCalf,rightCalf,leftFoot,rightFoot;std::array<int,3> capsuleDirections{-1,-1,-1};std::string modelPath;std::vector<std::string> hipsPath;};
struct Report {std::string component,reason;};
struct Generated {
  std::vector<std::shared_ptr<eiem_cloth_cache::Profile>> profiles;
  std::vector<std::shared_ptr<DenseRecipe>> dense;
  std::vector<Report> reports;
  bool rendererScopeKnown=false;
  std::vector<size_t> liveRenderers;
  std::string source,sourceHash,key;
  std::string relevantHash;
  std::map<std::string,std::string> sources;
  size_t equivalentSources=1;
  size_t meshReads=0,meshCacheHits=0,meshCacheBytes=0;uint64_t meshDecodeMs=0;
};
inline void ResolveGeneratedRenderers(Generated &result) {
  std::set<const ClothBoneProfile*> conflictingPanels;
  for(const auto &p:result.profiles)if(p->view.runtimeUnowned||(p->view.generatedLocal&&p->view.generatedLocal->NativeSkinRetained())) {
    bool shared=false;for(const auto &q:result.profiles)if(q!=p&&q->view.generatedLocal&&!q->view.generatedLocal->NativeSkinRetained())
      for(const auto &a:p->renderers)for(const auto &b:q->renderers)shared|=!strcmp(a.name,b.name)&&!strcmp(a.parent?a.parent:"",b.parent?b.parent:"");
    if(shared){conflictingPanels.insert(&p->view);result.reports.push_back({p->view.component,p->view.runtimeUnowned?"auto-unowned-renderer-reserved-by-private-skin":"auto-separated-panels-renderer-reserved-by-private-skin"});}
  }
  result.profiles.erase(std::remove_if(result.profiles.begin(),result.profiles.end(),[&](const auto &p){return conflictingPanels.count(&p->view)!=0;}),result.profiles.end());
  std::set<const ClothBoneProfile*> privateGraphRequired;
  for(const auto &p:result.profiles)if(p->view.generatedLocal&&p->view.generatedLocal->separatedPanels)privateGraphRequired.insert(&p->view);
  std::map<std::pair<std::string,std::string>,int> users;
  for(const auto &p:result.profiles)for(const auto &r:p->renderers)++users[{r.name,r.parent?r.parent:""}];
  for(const auto &p:result.profiles)if(p->view.generatedLocal&&!p->view.generatedLocal->NativeSkinRetained()){bool shared=false;for(const auto &r:p->renderers)shared|=users[{r.name,r.parent?r.parent:""}]>1;
    if(shared){p->view.generatedLocal=nullptr;result.reports.push_back({p->view.component,p->view.inputAnchorCount?
        "auto-fixed-apron-shared-renderer-original-unchanged":"auto-density-shared-renderer-retains-original-skin-connections-only"});}}
  result.profiles.erase(std::remove_if(result.profiles.begin(),result.profiles.end(),[&](const auto &p){return (p->view.inputAnchorCount||privateGraphRequired.count(&p->view))&&!p->view.generatedLocal;}),result.profiles.end());
  std::set<const ClothBoneProfile*> orphanRibbons;
  for(const auto &p:result.profiles)if(p->view.ribbonSource) {
    int consumers=0;for(const auto &q:result.profiles)if(const auto r=q->view.generatedLocal)
      consumers+=r->resampledPanel&&r->responseConsumer&&!strcmp(r->responseConsumer,p->view.component)&&!strcmp(q->view.prefabSha,p->view.prefabSha);
    if(!p->view.generatedLocal||!p->view.generatedLocal->ribbonSurface||consumers!=1) {
      orphanRibbons.insert(&p->view);result.reports.push_back({p->view.component,"ribbon-paired-main-surface-unavailable-original-retained"});
    }
  }
  result.profiles.erase(std::remove_if(result.profiles.begin(),result.profiles.end(),[&](const auto &p){return orphanRibbons.count(&p->view)!=0;}),result.profiles.end());
  std::set<const ClothBoneProfile*> orphanLayers;
  for(const auto &p:result.profiles)if(p->view.generatedLocal&&p->view.generatedLocal->nativeLayer==1) {
    int peers=0;for(const auto &q:result.profiles)peers+=ClothGeneratedLayerPair(q->view,p->view);
    if(peers!=1){orphanLayers.insert(&p->view);result.reports.push_back({p->view.component,"native-layer-partner-unavailable-original-retained"});}
  }
  result.profiles.erase(std::remove_if(result.profiles.begin(),result.profiles.end(),[&](const auto &p){return orphanLayers.count(&p->view)!=0;}),result.profiles.end());
  result.dense.erase(std::remove_if(result.dense.begin(),result.dense.end(),[&](const auto &d){return std::none_of(result.profiles.begin(),result.profiles.end(),[&](const auto &p){return p->view.generatedLocal==&d->view;});}),result.dense.end());
  for(const auto &p:result.profiles)result.reports.push_back({p->view.component,p->view.generatedLocal?
      (p->view.generatedLocal->sourceCoatTorso?"source-coat-chest-waist-contact-original-skin-native-BBC-pending":p->view.generatedLocal->NativeBodyOnly()?"source-Line-Point-fitted-thigh-contact-original-skin-native-BBC-pending":p->view.generatedLocal->CoatWaistSkinOnly()?"source-coat-waist-trunk-field-private-skin-native-BBC-pending":p->view.generatedLocal->sourceShortSkin?"source-fixed-short-skirt-native-skin-release-pending":p->view.generatedLocal->NativePanelsOnly()?"runtime-separated-native-panels-original-skin-retained-native-BBC-pending":p->view.generatedLocal->NativeRibbonWidth()?"runtime-native-ribbon-width-skin-native-BBC-pending":p->view.generatedLocal->ribbonSurface?"source-fitted-ribbon-width-sheet-native-contact-pending":p->view.generatedLocal->sourceApronFit?"content-fitted-fixed-apron-native-BBC-and-visual-validation-pending":p->view.generatedLocal->sourcePanelFit?"content-fitted-waist-panels-native-BBC-and-visual-validation-pending":p->view.generatedLocal->partialSurface?"runtime-isolated-source-regions-bones-and-private-skin-generated-native-BBC-pending":
      "runtime-simple-surface-bones-and-private-skin-generated-native-BBC-pending"):
      p->view.runtimeUnowned?"runtime-unowned-sheet-new-native-BBC-required-original-skin-retained":
      "runtime-original-bone-connections-generated-density-and-body-coverage-unchanged"});
}
inline BoneKey Key(Scene &s,int64_t id){return {s.Name(id),s.Name(s.Parent(id))};}
inline std::vector<int64_t> Refs(const Value &v){std::vector<int64_t> out;for(const auto &p:v.List()){const auto id=p.LocalRef();Need(id!=0,"asset-null-list-reference");out.push_back(id);}return out;}
inline bool RootMatch(Scene &s,int64_t id,const LiveCloth &c){const auto &v=s.file.Get(id);if(s.Name(id)!=c.name||!v.At("serializeData").Has("rootBones"))return false;const auto roots=Refs(v.At("serializeData").At("rootBones"));if(roots.size()!=c.roots.size())return false;for(size_t k=0;k<roots.size();++k)if(!(Key(s,roots[k])==c.roots[k]))return false;return true;}
inline bool RendererMatch(Scene &s,const MeshView &m,const LiveRenderer &r){if(m.name!=r.name||m.parent!=r.parent||m.mesh!=r.mesh||m.root!=r.root||m.vertices!=r.vertices||m.submeshes!=r.submeshes||m.bones.size()!=r.bones.size())return false;
  for(size_t n=0;n<m.bones.size();++n){if(!(Key(s,m.bones[n])==r.bones[n].bone))return false;if(!r.offlineBinding)for(int k=0;k<16;++k)if(!std::isfinite(r.bones[n].bind[k])||std::abs(m.binds[n][k]-r.bones[n].bind[k])>.0001)return false;}return true;}
inline bool MatchSourceRenderers(Package &package,Scene &scene,const Query &captured,
    const std::vector<int64_t> &cloths,Query &query,std::vector<size_t> &selected,std::string *issue=nullptr) {
  auto reject=[&](const std::string &reason){if(issue)*issue=reason;return false;};
  std::set<std::pair<std::string,std::string>> declared;
  for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==137)
    declared.insert({scene.Name(o.first),scene.Name(scene.Parent(scene.TransformId(o.first)))});
  query=captured;query.renderers.clear();selected.clear();std::set<std::pair<std::string,std::string>> seen;
  for(size_t k=0;k<captured.renderers.size();++k){const auto &r=captured.renderers[k];
    if(declared.count({r.name,r.parent})){if(!seen.insert({r.name,r.parent}).second)return reject("auto-source-renderer-identity-ambiguous:"+r.name);query.renderers.push_back(r);selected.push_back(k);}}
  if(query.renderers.empty())return reject("auto-source-renderer-scope-empty");
  std::set<int64_t> referenced;
  for(auto id:cloths){auto branch=scene.Branch(Refs(scene.file.Get(id).At("serializeData").At("rootBones")));referenced.insert(branch.begin(),branch.end());}
  for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==137){const auto &r=scene.file.Get(o.first);bool related=false;
    for(auto id:Refs(r.At("m_Bones")))related|=referenced.count(id)!=0;if(!related)continue;
    const auto mesh=DescribeMesh(package,scene,o.first);size_t matches=0;
    for(const auto &live:query.renderers)matches+=RendererMatch(scene,mesh,live);
    if(matches!=1)return reject("auto-source-required-renderer-matches-"+std::to_string(matches)+":"+mesh.name);}
  for(const auto &live:query.renderers){size_t matches=0;
    for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==137&&scene.Name(o.first)==live.name)
      matches+=RendererMatch(scene,DescribeMesh(package,scene,o.first),live);
    if(matches!=1)return reject("auto-source-live-renderer-matches-"+std::to_string(matches)+":"+live.name);}
  return true;
}
inline bool Above(Scene &s,int64_t descendant,int64_t ancestor){for(int steps=0;descendant&&steps<128;++steps){if(descendant==ancestor)return true;descendant=s.Parent(descendant);}return false;}
inline std::string RelevantSource(Package &package,Scene &s,const std::vector<int64_t> &cloths){std::map<std::string,std::string> parts;std::set<int64_t> transforms,clothBones;
  auto hashValue=[&](SerializedFile &f,int64_t id,const Value &v){const auto &o=f.objects.at(id);Need(v.start<=o.size&&v.length<=o.size-v.start,"auto-evidence-byte-range");return Digest(Bytes(f.bytes.begin()+o.offset+v.start,f.bytes.begin()+o.offset+v.start+v.length));};
  for(auto id:cloths){const auto &v=s.file.Get(id);const auto name=s.Name(id);Need(parts.emplace("bbc-data:"+name,hashValue(s.file,id,v.At("serializeData"))).second,"auto-evidence-duplicate-component");parts["bbc-selection:"+name]=hashValue(s.file,id,v.At("serializeData2"));auto branch=s.Branch(Refs(v.At("serializeData").At("rootBones")));clothBones.insert(branch.begin(),branch.end());transforms.insert(branch.begin(),branch.end());
    for(auto c:Refs(v.At("serializeData").At("colliderCollisionConstraint").At("colliderList"))){const auto &collider=s.file.Get(c);const auto t=s.TransformId(c);transforms.insert(t);parts["collider:"+s.Name(t)+":"+s.Name(s.Parent(t))]=hashValue(s.file,c,collider);}}
  for(const auto &o:s.file.objects)if(s.file.Class(o.first)==114&&std::find(cloths.begin(),cloths.end(),o.first)==cloths.end()){
    const auto &v=s.file.Get(o.first),&sd=v.At("serializeData");if(!sd.Has("rootBones"))continue;bool related=false;
    for(auto b:s.Branch(Refs(sd.At("rootBones"))))related|=clothBones.count(b)!=0;if(!related)continue;
    const auto name=s.Name(o.first);Need(parts.emplace("bbc-data:"+name,hashValue(s.file,o.first,sd)).second,"auto-evidence-duplicate-component");
    parts["bbc-selection:"+name]=hashValue(s.file,o.first,v.At("serializeData2"));
  }
  for(const auto &o:s.file.objects)if(s.file.Class(o.first)==137){const auto &r=s.file.Get(o.first);bool related=false;for(auto b:Refs(r.At("m_Bones")))related|=clothBones.count(b)!=0;if(!related)continue;const auto t=s.TransformId(o.first);const auto name=s.Name(t)+":"+s.Name(s.Parent(t));auto mesh=package.Resolve(s.file,r.At("m_Mesh"),43);
    Need(parts.emplace("renderer:"+name,hashValue(s.file,o.first,r)).second,"auto-evidence-duplicate-renderer");parts["mesh:"+name]=hashValue(*mesh.first,mesh.second,mesh.first->Get(mesh.second));for(auto b:Refs(r.At("m_Bones")))transforms.insert(b);transforms.insert(t);}
  for(const auto &o:s.file.objects)if(s.file.Class(o.first)==205){const auto t=s.TransformId(o.first);Need(parts.emplace("lod:"+s.Name(t)+":"+s.Name(s.Parent(t)),hashValue(s.file,o.first,s.file.Get(o.first))).second,"auto-evidence-ambiguous-lod-group");}
  const auto descendants=transforms;for(auto id:descendants){for(int n=0;id&&n<128;++n){transforms.insert(id);id=s.Parent(id);}Need(id==0,"auto-evidence-parent-cycle");}
  for(auto id:transforms){const auto &t=s.file.Get(id);const auto key=s.Name(id)+":"+s.Name(s.Parent(id));const auto value=hashValue(s.file,id,t.At("m_LocalPosition"))+hashValue(s.file,id,t.At("m_LocalRotation"))+hashValue(s.file,id,t.At("m_LocalScale"));Need(parts.emplace("transform:"+key,value).second,"auto-evidence-ambiguous-transform");}
  std::string evidence="exact-cloth-source-closure-v1";for(const auto &p:parts)evidence+="\n"+p.first+"="+p.second;return Digest(Bytes(evidence.begin(),evidence.end()));
}
inline int64_t FindPath(Scene &s,int64_t root,const std::vector<std::string> &path){
  Need(root&&path.size()<=128,"asset-body-path-budget");
  for(const auto &name:path){Need(!name.empty()&&name.size()<128,"asset-body-path-name");int64_t found=0;
    for(auto child:s.children[root])if(s.Name(child)==name){Need(!found,"asset-body-path-ambiguous-sibling");found=child;}
    Need(found!=0,"asset-body-path-missing");root=found;
  }return root;
}
inline void SelectBodyScope(Scene &s,const Query &q){
  s.bodyRoot=s.animatorRoot=0;if(q.hipsPath.empty())return;
  int64_t selected=0,animator=0;
  for(const auto &o:s.file.objects)if(s.file.Class(o.first)==95){int64_t bone=0;
    try{bone=FindPath(s,s.TransformId(o.first),q.hipsPath);}catch(const std::exception &){continue;}
    if(!(Key(s,bone)==q.hips))continue;Need(!selected,"asset-body-owner-path-ambiguous");selected=bone;animator=s.TransformId(o.first);
  }
  Need(selected!=0,"asset-body-owner-path-unconfirmed");s.bodyRoot=selected;s.animatorRoot=animator;
}
inline int64_t FindKeyWithin(Scene &s,const BoneKey &key,int64_t root){int64_t result=0;for(const auto &entry:s.goTransform)if((!root||Above(s,entry.second,root))&&Key(s,entry.second)==key){Need(result==0,"asset-ambiguous-body-reference");result=entry.second;}Need(result!=0,"asset-body-reference-missing");return result;}
inline int64_t FindKey(Scene &s,const BoneKey &key){return FindKeyWithin(s,key,0);}
inline int64_t FindBodyKey(Scene &s,const BoneKey &key){return FindKeyWithin(s,key,s.bodyRoot);}
inline LegReachPlan SourceLegReach(Scene &s,const Query &q,const Value &sd,const std::map<int64_t,int> &attrs,const std::vector<MeshView> &meshes) {
  if(q.leftCalf.name.empty()||q.rightCalf.name.empty()||q.leftFoot.name.empty()||q.rightFoot.name.empty())return {};
  const std::array<int64_t,6> ids{FindBodyKey(s,q.leftThigh),FindBodyKey(s,q.rightThigh),FindBodyKey(s,q.leftCalf),FindBodyKey(s,q.rightCalf),FindBodyKey(s,q.leftFoot),FindBodyKey(s,q.rightFoot)};
  std::array<Point,6> points;for(int n=0;n<6;++n)points[n]=Transform(s.World(ids[n]),{0,0,0});
  for(int side=0;side<2;++side)Need(Above(s,ids[side+4],ids[side+2])&&Above(s,ids[side+2],ids[side]),"auto-leg-role-ancestry");
  const std::array<LegRegion,4> legs{{{points[0],points[2]},{points[1],points[3]},{points[2],points[4]},{points[3],points[5]}}};
  std::vector<Point> garment;
  for(const auto &m:meshes)for(size_t v=0;v<m.world.size();++v){double own=0,sum=0;for(size_t k=0;k<m.weights[v].size();++k){const double w=m.weights[v][k];sum+=w;const auto b=m.bones[size_t(m.indices[v][k])];if(attrs.count(b)&&attrs.at(b))own+=w;}
    if(own<=0||sum<=0)continue;auto p=m.world[v];for(auto &x:p)x/=sum;garment.push_back(p);}
  const auto origin=Transform(s.World(FindBodyKey(s,q.hips)),{0,0,0}),up=Sub(Transform(s.World(FindBodyKey(s,q.spine)),{0,0,0}),origin);
  auto result=PlanLegReach(garment,origin,up,legs);std::array<std::vector<eiem_collision::Capsule>,4> shapes;
  std::vector<eiem_collision::Capsule> torsoShapes;
  const auto hips=FindBodyKey(s,q.hips),torsoTop=FindBodyKey(s,q.upperChest.name.empty()?q.spine:q.upperChest);
  for(auto ref:Refs(sd.At("colliderCollisionConstraint").At("colliderList"))){const auto t=s.TransformId(ref);const auto &c=s.file.Get(ref);int region=-1;
    for(int side=0;side<2;++side)if(Above(s,t,ids[side])&&!Above(s,t,ids[side+4]))region=side+(Above(s,t,ids[side+2])?2:0);
    const bool torso=Above(s,torsoTop,t)&&Above(s,t,hips);
    if(region<0&&!torso)continue;if(region>=0)result.listed|=1u<<region;
    int axis=-1;if(c.Has("direction"))for(int n=0;n<3;++n)if(q.capsuleDirections[n]>=0&&c.At("direction").Int()==q.capsuleDirections[n])axis=n;
    if(axis<0||!c.Has("center")||!c.Has("size"))continue;
    try{(region>=0?shapes[region]:torsoShapes).push_back(SourceWorldCapsule(s.World(t),Vec(c.At("center")),Vec(c.At("size")),axis,c.At("reverseDirection").Int()!=0,c.At("radiusSeparation").Int()!=0,c.At("alignedOnCenter").Int()!=0));}
    catch(const std::exception &){}}
  for(int n=0;n<4;++n)if(n<2?LegCenterlineCovered(legs[n],shapes[n],torsoShapes):LegCenterlineCovered(legs[n],shapes[n]))result.centerlineCovered|=1u<<n;return result;
}
inline std::vector<int> ChainOrder(size_t count,const std::set<std::pair<int,int>> &pairs,bool &loop){Need(count>=2&&count<=16,"auto-root-budget");std::vector<std::vector<int>> adjacency(count);
  for(const auto &p:pairs){Need(p.first>=0&&p.second>p.first&&size_t(p.second)<count,"auto-root-edge");adjacency[p.first].push_back(p.second);adjacency[p.second].push_back(p.first);}
  int ends=0,start=0;for(size_t n=0;n<count;++n){Need(adjacency[n].size()==1||adjacency[n].size()==2,"auto-independent-or-branched-panels");if(adjacency[n].size()==1){++ends;start=int(n);}}
  Need(ends==0||ends==2,"auto-open-path-ambiguous");loop=ends==0;std::vector<int> order;std::set<int> seen;int previous=-1,current=start;
  while(seen.insert(current).second){order.push_back(current);int next=-1;for(int n:adjacency[current])if(n!=previous){next=n;break;}if(next<0)break;previous=current;current=next;}
  Need(order.size()==count&&(!loop||current==start),"auto-independent-panels");return order;
}
struct SheetSamples {static constexpr int Grid=64;std::array<std::vector<Point>,Grid*Grid> samples;size_t work=0,hits=0;};
inline void SingleSheet(const std::vector<std::array<double,2>> &uv,const std::vector<Point> &points,
    const std::vector<std::array<int,3>> &faces,int columns,bool loop,SheetSamples *shared=nullptr){
  SheetSamples local;auto &state=shared?*shared:local;constexpr int Grid=SheetSamples::Grid;auto &samples=state.samples;auto &work=state.work;auto &hits=state.hits;
  for(const auto &f:faces){std::array<std::array<double,2>,3> t{uv[f[0]],uv[f[1]],uv[f[2]]};
    if(loop){double lo=t[0][0],hi=lo;for(const auto &v:t){lo=(std::min)(lo,v[0]);hi=(std::max)(hi,v[0]);}if(hi-lo>columns*.5)for(auto &v:t)if(v[0]<columns*.5)v[0]+=columns;}
    const double area=(t[1][0]-t[0][0])*(t[2][1]-t[0][1])-(t[2][0]-t[0][0])*(t[1][1]-t[0][1]);if(std::abs(area)<1e-10)continue;
    double loX=t[0][0],hiX=loX,loY=t[0][1],hiY=loY;for(const auto &v:t){loX=(std::min)(loX,v[0]);hiX=(std::max)(hiX,v[0]);loY=(std::min)(loY,v[1]);hiY=(std::max)(hiY,v[1]);}
    const int x0=int(std::floor(loX/columns*Grid)),x1=int(std::ceil(hiX/columns*Grid));const int y0=(std::max)(0,int(std::floor(loY*Grid))),y1=(std::min)(Grid-1,int(std::ceil(hiY*Grid)));
    for(int y=y0;y<=y1;++y)for(int x=x0;x<=x1;++x){Need(++work<=4000000,"auto-sheet-query-budget");const double px=(x+.5)*columns/Grid,py=(y+.5)/Grid;
      const double b=((px-t[0][0])*(t[2][1]-t[0][1])-(t[2][0]-t[0][0])*(py-t[0][1]))/area;
      const double c=((t[1][0]-t[0][0])*(py-t[0][1])-(px-t[0][0])*(t[1][1]-t[0][1]))/area,a=1-b-c;if(a<1e-7||b<1e-7||c<1e-7)continue;
      Point p{};for(int j=0;j<3;++j)p[j]=a*points[f[0]][j]+b*points[f[1]][j]+c*points[f[2]][j];auto &bucket=samples[size_t(y)*Grid+size_t((x%Grid+Grid)%Grid)];
      for(const auto &old:bucket)Need(Distance(old,p)<.0005,"auto-multiple-layers-or-folded-skin-needs-authored-profile");if(bucket.empty())bucket.push_back(p);++hits;
    }
  }Need(hits>=16,"auto-skin-coordinate-coverage-insufficient");
}
struct SurfaceInput {std::vector<MeshView> meshes;std::vector<std::vector<int64_t>> chains;std::vector<int> order,groups;std::vector<int64_t> ids;bool singleSheet=false,spatialSelection=false;std::string densityIssue,sheetIssue,legReport;SurfaceRegions regions;};
inline std::vector<int> SourceLodSheets(Package &package,Scene &s,const std::vector<MeshView> &meshes) {
  std::vector<int> levels(meshes.size(),-1);int64_t group=0;
  for(const auto &o:s.file.objects)if(s.file.Class(o.first)==205){const auto &lods=s.file.Get(o.first).At("m_LODs").List();Need(lods.size()<=8,"auto-lod-level-budget");
    for(size_t level=0;level<lods.size();++level)for(const auto &entry:lods[level].At("renderers").List())for(size_t m=0;m<meshes.size();++m)if(entry.At("renderer").LocalRef()==meshes[m].id){
      Need(!group||group==o.first,"auto-multiple-lod-groups-need-composition");group=o.first;
      Need(levels[m]<0||levels[m]==int(level),"auto-shared-across-lod-levels");levels[m]=int(level);}}
  if(!group)return std::vector<int>(meshes.size(),0);
  for(size_t m=0;m<meshes.size();++m)if(levels[m]<0){const auto ref=package.Resolve(s.file,s.file.Get(meshes[m].id).At("m_Mesh"),43);int found=-1;
    for(size_t n=0;n<meshes.size();++n)if(levels[n]>=0&&m!=n&&meshes[n].bones==meshes[m].bones&&meshes[n].binds==meshes[m].binds){const auto other=package.Resolve(s.file,s.file.Get(meshes[n].id).At("m_Mesh"),43);
      if(ref==other){Need(found<0||found==levels[n],"auto-shadow-lod-source-ambiguous");found=levels[n];}}
    Need(found>=0,"auto-renderer-outside-known-lod-source");levels[m]=found;}
  return levels;
}
inline void RadialSourceSheet(const std::vector<MeshView> &meshes,const std::vector<int> &lod,const std::map<int64_t,int> &attributes,
    Point origin,Point up,Point side,int columns) {
  const double along=Dot(side,up);for(int k=0;k<3;++k)side[k]-=along*up[k];const double width=std::sqrt(Dot(side,side));Need(width>.001,"auto-sheet-body-axis");for(auto &v:side)v/=width;const auto front=Cross(up,side);
  std::vector<std::vector<std::array<int,3>>> faces(meshes.size());double low=1e100,high=-1e100;
  for(size_t k=0;k<meshes.size();++k){const auto &m=meshes[k];std::vector<bool> own(m.world.size()),move(m.world.size());
    for(size_t n=0;n<m.world.size();++n)for(size_t b=0;b<m.weights[n].size();++b)if(m.weights[n][b]>0){const auto found=attributes.find(m.bones[size_t(m.indices[n][b])]);if(found!=attributes.end()&&found->second){own[n]=true;move[n]=move[n]||found->second==2;}}
    for(auto f:m.triangles)if(own[f[0]]&&own[f[1]]&&own[f[2]]&&(move[f[0]]||move[f[1]]||move[f[2]])){faces[k].push_back(f);for(int n:f){const double y=Dot(Sub(m.world[n],origin),up);low=(std::min)(low,y);high=(std::max)(high,y);}}
  }
  Need(high-low>.01&&high-low<3.,"auto-sheet-natural-height");std::map<int,SheetSamples> sheets;
  for(size_t k=0;k<meshes.size();++k){if(faces[k].empty())continue;const auto &m=meshes[k];std::vector<std::array<double,2>> uv(m.world.size());
    for(size_t n=0;n<m.world.size();++n){const auto v=Sub(m.world[n],origin);uv[n]={(std::atan2(Dot(v,front),Dot(v,side))+3.14159265358979323846)*columns/(2*3.14159265358979323846),(Dot(v,up)-low)/(high-low)};}
    try{SingleSheet(uv,m.world,faces[k],columns,true,&sheets[lod[k]]);}
    catch(const std::exception &e){throw std::runtime_error(std::string(e.what())+":"+m.name);}
  }
}
inline std::shared_ptr<eiem_cloth_cache::Profile> GenerateConnections(Package &package,Scene &s,int64_t id,const Query &query,SurfaceInput *surface=nullptr){
  const auto &object=s.file.Get(id),&sd=object.At("serializeData"),&sd2=object.At("serializeData2");
  const bool bodyOnly=SourceBodyContact(s.file.sha.c_str(),s.Name(id).c_str());
  Need(sd.At("clothType").Int()==1,"auto-not-bonecloth");Need(sd.At("connectionMode").Int()==0,"auto-original-already-connected");
  Need(sd.At("colliderCollisionConstraint").At("mode").Int()==1,"auto-existing-collision-policy");
  if(sd.Has("preBuildData"))Need(sd.At("preBuildData").At("enabled").Int()==0,"auto-prebuild-requires-authored-lifecycle");
  const auto roots=Refs(sd.At("rootBones"));Need(roots.size()>=2&&roots.size()<=16,"auto-single-chain-or-root-budget");
  auto branch=EffectiveBranch(s,sd);PrebuildSourceClosure(s,id,sd2,branch);auto columns=branch.columns;const auto &ids=branch.ids;
  const int64_t hips=FindBodyKey(s,query.hips),left=FindBodyKey(s,query.leftThigh),right=FindBodyKey(s,query.rightThigh);const auto hp=Transform(s.World(hips),{0,0,0}),lp=Transform(s.World(left),{0,0,0}),rp=Transform(s.World(right),{0,0,0});
  const double hipWidth=Distance(lp,rp);Need(hipWidth>.02&&hipWidth<1.,"auto-body-reference-scale");
  auto up=Sub(Transform(s.World(FindBodyKey(s,query.spine)),{0,0,0}),hp);const double upLength=std::sqrt(Dot(up,up));Need(upLength>.001,"auto-torso-reference-degenerate");for(auto &x:up)x/=upLength;
  double waistTop=hipWidth;auto chest=query.chest.name.empty()?int64_t(0):FindBodyKey(s,query.chest);
  if(chest){Need(Above(s,chest,hips),"auto-chest-reference-ancestry");waistTop=Dot(Sub(Transform(s.World(chest),{0,0,0}),hp),up);Need(waistTop>0&&waistTop<1.,"auto-chest-reference-height");}
  if(!query.upperChest.name.empty()){const auto upper=FindBodyKey(s,query.upperChest);Need(chest&&Above(s,upper,chest),"auto-upper-chest-reference-ancestry");
    const double top=Dot(Sub(Transform(s.World(upper),{0,0,0}),hp),up);Need(top>=waistTop&&top<1.,"auto-upper-chest-reference-height");waistTop=top;chest=upper;}
  for(auto root:roots){Need(Above(s,root,hips)&&!Above(s,root,left)&&!Above(s,root,right),"auto-not-independent-waist-cloth");const auto p=Transform(s.World(root),{0,0,0});const double height=Dot(Sub(p,hp),up);Need(Distance(p,hp)<hipWidth*3.&&height>=-hipWidth&&height<waistTop+hipWidth,"auto-attachment-outside-waist");}
  bool spatialSelection=false;auto attrs=SourcePrebuildEnabled(sd2)?SourcePrebuildAttributes(sd2):SourceSelection(s,id,sd2,ids,&spatialSelection);
  const auto prebuildId=SourcePrebuild(s,id,sd2,ids,attrs);
  const auto originalAttrs=attrs;const auto ownership=SharedSelection(s,id,ids,attrs);
  if(bodyOnly)Need(prebuildId=="0226bd67"&&ids.size()==44&&roots.size()==10&&branch.prebuildOmitted.size()==13&&
      ownership.proofs.empty()&&ownership.foreign.empty()&&branch.ignored.empty(),"body-contact-source-closure-changed");
  const bool shortSkin=surface&&SourceShortSkin(s.file.sha.c_str(),s.Name(id).c_str());
  if(shortSkin){Need(roots.size()==4&&ids.size()==12&&branch.ignored.empty()&&branch.excluded.empty()&&ownership.proofs.empty(),"short-source-ownership-contract");
    for(auto root:roots){Need(attrs[root]==0&&s.Parent(root)==FindBodyKey(s,query.spine)&&s.children[root].size()==1,"short-source-waist-contract");
      const auto fixed=s.children[root][0];Need(attrs[fixed]==1&&s.children[fixed].size()==1,"short-source-visible-fixed-contract");
      const auto tip=s.children[fixed][0];Need(attrs[tip]==2&&s.children[tip].empty(),"short-source-tip-contract");}}
  auto passive=PassiveSourceLeaves(s,ids,attrs);passive.insert(ownership.foreign.begin(),ownership.foreign.end());
  for(auto b:ownership.foreign)attrs[b]=0;
  const bool inactiveCoat=s.file.sha==InactiveCoatPrefab&&s.Name(id)=="MC_Lifeng_Coat";
  if(inactiveCoat) {
    Need(roots.size()==6&&ids.size()==24&&passive.empty()&&ownership.proofs.empty()&&
        branch.ignored.empty()&&branch.excluded.empty()&&!prebuildId.empty(),"inactive-coat-original-closure");
    for(int c=0;c<6;++c)for(int d=0;d<4;++d){const auto b=ids[c*4+d];
      Need(columns.at(b)==c&&attrs.at(b)==(d?(c>=4?0:2):1)&&
          (d?s.Parent(b)==ids[c*4+d-1]:b==roots[c])&&s.children[b].size()==size_t(d==3?0:1),"inactive-coat-original-chain");
      if(c>=4&&d)attrs[b]=2;
    }
    const auto candidateOwnership=SharedSelection(s,id,ids,attrs);
    Need(candidateOwnership.proofs.empty()&&candidateOwnership.foreign.empty(),"inactive-coat-output-already-owned");
  }
  auto tree=SourceFixedTree(s,roots,ids,columns,attrs,passive,up,hipWidth);
  tree.irregular|=inactiveCoat;
  auto &chains=tree.chains;auto &rows=tree.rows;const size_t depth=tree.depth;const bool irregular=tree.irregular;
  bool upperCoat=false;
  for(size_t c=0;c<roots.size();++c){const auto p=Transform(s.World(roots[c]),{0,0,0});const double height=Dot(Sub(p,hp),up);
    if(height<waistTop)continue;double movingLow=1e100;for(auto b:ids)if(columns.at(b)==int(c)&&attrs.at(b)==2&&!passive.count(b))
      movingLow=(std::min)(movingLow,Dot(Sub(Transform(s.World(b),{0,0,0}),hp),up));
    Need(UpperCoatAttachment(height,Distance(p,hp),waistTop,hipWidth,movingLow),"auto-high-attachment-not-descending-coat");upperCoat=true;}
  std::set<int64_t> movable;for(auto bone:ids)if((attrs[bone]==2||(shortSkin&&attrs[bone]==1))&&!passive.count(bone))movable.insert(bone);
  std::vector<MeshView> meshes;std::set<std::pair<int,int>> pairs;std::string densityIssue;
  auto densityCheck=[&](bool ok,const char *reason){if(ok||bodyOnly)return;if(!surface)throw std::runtime_error(reason);if(densityIssue.empty())densityIssue=reason;};
  for(const auto &o:s.file.objects)if(s.file.Class(o.first)==137){const auto &r=s.file.Get(o.first);const auto boneIds=Refs(r.At("m_Bones"));bool uses=false;for(auto b:boneIds)uses|=movable.count(b)!=0;if(!uses)continue;
    auto mesh=ReadMesh(package,s,o.first);Need(!query.reservedRenderers.count({mesh.name,mesh.parent})&&!query.reservedRenderers.count({mesh.name,""}),"auto-renderer-reserved-by-authored-contact-group");size_t matches=0;for(const auto &live:query.renderers)matches+=RendererMatch(s,mesh,live);Need(matches==1,"auto-renderer-live-binding-mismatch-or-ambiguous");
    std::vector<std::set<int>> vertexRoots(mesh.world.size());std::vector<bool> pure(mesh.world.size(),true),moving(mesh.world.size());
    for(size_t n=0;n<mesh.world.size();++n)for(size_t k=0;k<mesh.weights[n].size();++k)if(mesh.weights[n][k]>0){const auto b=mesh.bones[size_t(mesh.indices[n][k])];if(!columns.count(b)||(!attrs[b]&&!shortSkin))pure[n]=false;else {vertexRoots[n].insert(columns[b]);moving[n]=moving[n]||movable.count(b)!=0;
      if(shortSkin&&attrs[b]==1){const auto waist=Transform(s.World(s.Parent(b)),{0,0,0});Need(Dot(Sub(mesh.world[n],waist),up)<.002,"short-visible-fixed-weight-above-waist");}}}
    for(size_t n=0;n<pure.size();++n)if(moving[n]&&!pure[n]){bool protectedAnchors=true;
      for(size_t k=0;k<mesh.weights[n].size();++k)if(mesh.weights[n][k]>0){const auto b=mesh.bones[size_t(mesh.indices[n][k])];if(columns.count(b)&&attrs[b])continue;
        bool ancestor=chest&&Above(s,chest,b)&&Above(s,b,hips);for(auto root:roots)ancestor|=Above(s,root,b);
        if(!ancestor)for(auto moved:movable)if(Above(s,moved,b)){ancestor=true;break;}
        protectedAnchors&=ancestor;}
      densityCheck(protectedAnchors,"auto-density-mixed-foreign-cloth-needs-contact-group");}
    size_t faces=0;for(const auto &f:mesh.triangles){if(!pure[f[0]]||!pure[f[1]]||!pure[f[2]]||!(moving[f[0]]||moving[f[1]]||moving[f[2]]))continue;std::set<int> owners;for(int v:f)owners.insert(vertexRoots[v].begin(),vertexRoots[v].end());if(owners.size()==2)pairs.insert({*owners.begin(),*owners.rbegin()});++faces;}meshes.push_back(std::move(mesh));
  }
  if(meshes.empty()) {
    bool attachedSkin=false;
    for(const auto &o:s.file.objects)if(s.file.Class(o.first)==137){bool related=false;
      for(auto b:Refs(s.file.Get(o.first).At("m_Bones")))related|=columns.count(b)!=0;if(!related)continue;
      const auto m=ReadMesh(package,s,o.first);
      for(size_t v=0;v<m.weights.size();++v)for(size_t k=0;k<m.weights[v].size();++k)if(m.weights[v][k]>0){
        const auto b=m.bones[size_t(m.indices[v][k])];attachedSkin|=columns.count(b)&&attrs[b]!=2;}
    }
    Need(!attachedSkin,"auto-visible-skin-fixed-or-invalid-no-moving-influence");
  }
  Need(!meshes.empty()&&meshes.size()<=64,"auto-renderer-coverage-missing-or-budget");
  LegReachPlan leg;
  const bool originalCoverageCoat=SourceOriginalCoverageCoat(s.file.sha.c_str(),s.Name(id).c_str());
  try{leg=SourceLegReach(s,query,sd,attrs,meshes);}catch(const std::exception &e){if(surface)surface->legReport=std::string("auto-leg-coverage-unconfirmed:")+e.what();}
  if(surface&&surface->legReport.empty())surface->legReport="auto-leg-coverage requiredMask="+std::to_string(leg.required)+" listedMask="+std::to_string(leg.listed)+
      " sourceCapsuleCenterlineMask="+std::to_string(leg.centerlineCovered)+" missingMask="+std::to_string(leg.required&~leg.listed)+
      " geometryGapMask="+std::to_string(leg.required&~leg.centerlineCovered)+" maskOrder=left-thigh,right-thigh,left-calf,right-calf geometryWrites=0 renderDepth=unmeasured";
  if(upperCoat)Need((leg.listed&leg.centerlineCovered&3)==3&&(leg.required&3)!=0,"auto-upper-coat-thigh-coverage-unconfirmed");
  const bool retainOriginalCoverage=OriginalCoverageCoatEligible(originalCoverageCoat,leg.required,leg.listed,leg.centerlineCovered);
  if(retainOriginalCoverage&&surface)surface->legReport+=" sourceTorsoThighUnion=1 calfSupplement=pending";
  bool loop=false;std::vector<int> order,groups;
  if(bodyOnly){for(size_t n=0;n<roots.size();++n)order.push_back(int(n));}
  else try{order=ChainOrder(roots.size(),pairs,loop);}catch(const std::exception &){
    Need(surface!=nullptr,"auto-independent-panels-needs-private-root-separators");std::vector<Point> positions;for(auto root:roots)positions.push_back(Transform(s.World(root),{0,0,0}));
    const auto separated=SeparatedPanelOrder(positions,pairs,hp,up,Sub(rp,lp),retainOriginalCoverage||(leg.required!=0&&(leg.required&~leg.centerlineCovered)==0),retainOriginalCoverage);order=separated.order;groups=separated.groups;loop=false;
    if(surface)surface->legReport+=" independentPanels="+std::to_string(groups.back()+1)+" sourceSkinAdjacency=1 crossOpeningFaces=0";
  }
  Need(!tree.forks||groups.empty(),"auto-fixed-fork-disconnected-panels");
  std::vector<int> orderedColumn(roots.size());for(size_t c=0;c<order.size();++c)orderedColumn[order[c]]=int(c);
  if(!bodyOnly)for(const auto &m:meshes)for(const auto &f:m.triangles){std::set<int> labels;bool moving=false,pure=true;
    for(int v:f)for(size_t k=0;k<m.weights[v].size();++k)if(m.weights[v][k]>0){const auto b=m.bones[size_t(m.indices[v][k])];if(columns.count(b)&&attrs[b]){labels.insert(orderedColumn[columns[b]]);moving|=attrs[b]==2;}else pure=false;}
    // Connection groups preserve the verified coat skin, including existing
    // triangles influenced by several groups; no cross-opening cloth faces.
    if(!retainOriginalCoverage&&moving&&pure&&!groups.empty()&&!labels.empty())for(int c:labels)Need(groups[c]==groups[*labels.begin()],"auto-separated-panels-share-render-face");
    if(!moving||labels.size()<3)continue;int gaps=0;for(int c:labels)if(!labels.count(c+1)&&!(loop&&c==int(roots.size())-1&&labels.count(0)))++gaps;
    if(!retainOriginalCoverage)densityCheck(gaps<=1&&labels.size()<=3,"auto-density-disconnected-or-wide-skin-junction");}
  if(retainOriginalCoverage)Need(!groups.empty(),"auto-original-coverage-coat-connection-groups-required");
  std::string panelIdentity=std::string(ownership.proofs.empty()?"runtime-effective-graph-v3\n":"runtime-effective-graph-v4-ownership\n")+s.file.sha+"\n"+std::to_string(id);
  for(const auto &source:package.sources)panelIdentity+="\n"+source.first+"="+source.second;
  const auto panelKey=Digest(Bytes(panelIdentity.begin(),panelIdentity.end()));ClothBoneProfile panelSource{};
  panelSource.runtimeGenerated=true;panelSource.signature=panelKey.c_str();panelSource.prefabSha=s.file.sha.c_str();
  const bool fittedPanels=surface&&SourcePanelFit(panelSource);bool singleSheet=!fittedPanels;
  if(bodyOnly)singleSheet=false;
  if(fittedPanels)surface->sheetIssue="content-fitted-folded-source-not-generic-single-sheet";
  if(tree.forks||inactiveCoat){singleSheet=false;if(surface)surface->sheetIssue=inactiveCoat?"auto-inactive-coat-branches-completed-original-skin-retained":"auto-fixed-fork-native-connections-original-skin-retained";}
  if(!bodyOnly&&!tree.forks&&!inactiveCoat&&!fittedPanels&&groups.empty()&&!shortSkin)try {
  const auto lod=SourceLodSheets(package,s,meshes);std::map<int,SheetSamples> sheets;
  for(size_t meshIndex=0;meshIndex<meshes.size();++meshIndex){const auto &m=meshes[meshIndex];std::vector<std::array<double,2>> uv(m.world.size());std::vector<bool> pure(m.world.size(),true),moving(m.world.size());std::vector<std::array<int,3>> faces;
    for(size_t n=0;n<m.world.size();++n){std::vector<std::pair<int,double>> labels;for(size_t k=0;k<m.weights[n].size();++k)if(m.weights[n][k]>0){const auto b=m.bones[size_t(m.indices[n][k])];if(!columns.count(b)||!attrs[b]){pure[n]=false;continue;}const double w=m.weights[n][k];labels.push_back({orderedColumn[columns[b]],w});uv[n][1]+=w*rows[b]/double(depth-1);moving[n]=moving[n]||attrs[b]==2;}
      bool wrap=false;if(loop)for(const auto &a:labels)for(const auto &b:labels)wrap|=std::abs(a.first-b.first)>int(roots.size())/2;for(const auto &a:labels)uv[n][0]+=a.second*(a.first+(wrap&&a.first<int(roots.size())/2?int(roots.size()):0));if(uv[n][0]>=roots.size())uv[n][0]-=roots.size();}
    for(const auto &f:m.triangles)if(pure[f[0]]&&pure[f[1]]&&pure[f[2]]&&(moving[f[0]]||moving[f[1]]||moving[f[2]]))faces.push_back(f);
    if(!faces.empty())try{SingleSheet(uv,m.world,faces,int(roots.size()),loop,&sheets[lod[meshIndex]]);}
      catch(const std::exception &e){throw std::runtime_error(std::string(e.what())+":"+m.name);}
  }
  } catch(const std::exception &) {
    try{const auto lod=SourceLodSheets(package,s,meshes);RadialSourceSheet(meshes,lod,attrs,hp,up,Sub(rp,lp),int(roots.size()));}
    catch(const std::exception &e){singleSheet=false;if(!surface)throw;surface->sheetIssue=e.what();
      try{surface->regions=IsolatedSurfaceRegions(meshes,SourceLodSheets(package,s,meshes),attrs,hp,up,Sub(rp,lp),package.vfs.cancel);}
      catch(const std::exception &){CheckCancel(package.vfs.cancel);if(densityIssue.empty())densityIssue=e.what();}}
  }
  if(!groups.empty()||shortSkin){singleSheet=false;densityIssue.clear();}
  auto out=std::make_shared<eiem_cloth_cache::Profile>();auto text=[&](const std::string &v){Need(!v.empty()&&v.size()<128,"auto-profile-name-budget");out->strings.push_back(v);return out->strings.back().c_str();};auto &p=out->view;p.component=text(s.Name(id));p.prefabSha=text(s.file.sha);p.depth=int(depth);p.loop=loop;p.runtimeFixedForks=tree.forks;
  p.legRequiredMask=leg.required;p.legListedMask=leg.listed;
  p.runtimeForkCoat=upperCoat&&tree.forks&&!loop;
  if(p.runtimeForkCoat&&surface)surface->legReport+=" contact=Point faceBending=inactive-source-line";
  if(upperCoat&&surface)surface->legReport+=" attachment=upper-torso-descending-coat";
  if(!prebuildId.empty())p.prebuildId=text(prebuildId);
  std::map<int64_t,int> indices;for(size_t n=0;n<ids.size();++n)indices[ids[n]]=int(n);
  for(auto bone:ids){const auto &b=s.file.Get(bone),&q=b.At("m_LocalRotation");const auto t=Vec(b.At("m_LocalPosition")),scale=Vec(b.At("m_LocalScale"));const auto parent=s.Parent(bone);out->bones.push_back({text(s.Name(bone)),text(s.Name(parent)),indices.count(parent)?indices[parent]:-1,originalAttrs.at(bone),attrs[bone]?orderedColumn[columns[bone]]:-1,attrs[bone]?rows[bone]:-1,{float(t[0]),float(t[1]),float(t[2])},{float(q.At("x").Number()),float(q.At("y").Number()),float(q.At("z").Number()),float(q.At("w").Number())},{float(scale[0]),float(scale[1]),float(scale[2])}});}
  if(inactiveCoat)for(auto b:ids)out->candidateAttributes.push_back(attrs.at(b));
  for(int c:order)out->roots.push_back(indices[chains[c][0]]);for(auto root:roots)out->originalRoots.push_back(indices[root]);
  if(!groups.empty()&&!tree.forks&&!inactiveCoat&&!shortSkin&&!fittedPanels&&!bodyOnly&&
      prebuildId.empty()&&passive.empty()&&branch.ignored.empty()&&branch.excluded.empty()&&branch.prebuildOmitted.empty()&&
      ownership.proofs.empty()&&ownership.foreign.empty()&&(leg.listed&leg.centerlineCovered&3)==3) {
    std::vector<Point> points;std::vector<int> selection;std::vector<std::vector<int>> orderedChains;
    std::vector<CoatFixedSkin> skin(ids.size());
    for(auto bone:ids){points.push_back(Transform(s.World(bone),{0,0,0}));selection.push_back(originalAttrs.at(bone));}
    for(int c:order){orderedChains.emplace_back();for(auto bone:chains[c])orderedChains.back().push_back(indices.at(bone));}
    for(const auto &m:meshes)for(size_t v=0;v<m.world.size();++v){std::set<int> touched;
      for(size_t k=0;k<m.weights[v].size();++k)if(m.weights[v][k]>0){const auto found=indices.find(m.bones[size_t(m.indices[v][k])]);
        if(found!=indices.end()&&selection[found->second]==1)touched.insert(found->second);}
      const double height=Dot(Sub(m.world[v],hp),up);
      for(int n:touched){auto &sample=skin[n];++sample.vertices;sample.low=(std::min)(sample.low,height);sample.high=(std::max)(sample.high,height);}}
    auto released=SeparatedCoatInputs(points,selection,orderedChains,groups,skin,hp,up,hipWidth);
    if(!released.empty()){auto proposed=attrs;for(int n:released)proposed[ids[n]]=2;
      const auto peers=SharedSelection(s,id,ids,proposed);
      Need(peers.proofs.empty()&&peers.foreign.empty(),"auto-separated-coat-input-owned-by-peer");
      out->releasedFixed=std::move(released);p.runtimeSeparatedCoat=true;
      if(surface)surface->legReport+=" contact=Point faceBending=inactive-source-line releasedInteriorFixed="+std::to_string(out->releasedFixed.size())+" waistRoots=retained sourceSkin=retained";
    }
  }
  for(auto leaf:passive){out->ignored.push_back(indices[leaf]);out->bones[indices[leaf]].column=out->bones[indices[leaf]].depth=-1;}
  for(auto b:ownership.foreign)out->foreignIgnored.push_back(indices.at(b));
  for(const auto &v:ownership.proofs)out->ownership.push_back({text(s.Name(v.component)),indices.at(v.bone),v.attribute,v.zeroRotationAnchor});
  for(auto b:branch.prebuildOmitted)out->prebuildOmitted.push_back({text(s.Name(b)),text(s.Name(s.Parent(b)))});
  for(auto b:branch.ignored)out->originalExcluded.push_back({text(s.Name(b)),text(s.Name(s.Parent(b)))});
  for(auto b:branch.excluded)out->excludedBranches.push_back({text(s.Name(b)),text(s.Name(s.Parent(b)))});
  for(const auto &m:meshes){std::vector<ClothBoneBinding> binding;for(size_t n=0;n<m.bones.size();++n){const auto bone=m.bones[n];ClothBoneBinding b{text(s.Name(bone)),text(s.Name(s.Parent(bone))),indices.count(bone)?indices[bone]:-1,{}};for(int j=0;j<16;++j)b.bind[j]=float(m.binds[n][j]);binding.push_back(b);}out->bindings.push_back(std::move(binding));out->renderers.push_back({text(m.name),text(m.mesh),text(m.root),int(m.world.size()),m.submeshes,nullptr,0,text(m.parent)});}
  std::set<int64_t> retainedProducers;
  for(auto collider:Refs(sd.At("colliderCollisionConstraint").At("colliderList"))){const auto t=s.TransformId(collider);int64_t producer=0;
    for(const auto &o:s.file.objects)if(s.file.Class(o.first)==114){const auto &other=s.file.Get(o.first).At("serializeData");if(!other.Has("rootBones"))continue;const auto foreign=s.Branch(Refs(other.At("rootBones")));bool follows=false;
      for(auto b:foreign)follows|=Above(s,t,b);if(!follows)continue;
      Need(o.first!=id&&!producer,"auto-collider-producer-overlap");producer=o.first;
      for(auto b:foreign)Need(!columns.count(b),"auto-collider-producer-shared-output");
      for(auto b:Refs(other.At("rootBones")))for(auto own:roots)Need(!Above(s,b,own),"auto-collider-producer-follows-consumer");
      for(const auto &live:query.cloths)Need(!RootMatch(s,o.first,live),"auto-collider-producer-needs-ordered-batch");
      for(auto c:Refs(other.At("colliderCollisionConstraint").At("colliderList")))for(auto own:roots)Need(!Above(s,s.TransformId(c),own),"auto-collider-producer-feedback");
    }
    if(producer)retainedProducers.insert(producer);out->colliders.push_back({text(s.Name(collider)),text(s.Name(s.Parent(t))),s.animatorRoot&&s.Parent(t)==s.animatorRoot});
  }
  Need(retainedProducers.size()<=7,"auto-native-producer-budget");for(auto producer:retainedProducers)out->nativeProducers.push_back(text(s.Name(producer)));
  Need(!out->colliders.empty(),"auto-body-collision-coverage-missing");
  std::string signature=std::string(ownership.proofs.empty()?"runtime-effective-graph-v3\n":"runtime-effective-graph-v4-ownership\n")+s.file.sha+"\n"+std::to_string(id);for(const auto &source:package.sources)signature+="\n"+source.first+"="+source.second;if(tree.forks)signature+="\nfixed-fork-native-tree-v2";if(p.runtimeForkCoat)signature+="\nfork-coat-point-flexible-faces-v1";if(inactiveCoat)signature+="\ninactive-coat-branches-v1";if(p.runtimeSeparatedCoat){signature+="\nseparated-coat-inputs-point-flexible-v1";for(int n:out->releasedFixed)signature+=":"+std::to_string(n);}p.signature=text(Digest(Bytes(signature.begin(),signature.end())));Need(!fittedPanels||panelKey==p.signature,"panel-fit-source-closure-changed");p.runtimeGenerated=true;out->Link();
  if(originalCoverageCoat) {
    Need(SourceUpperCoatFront(p),"auto-upper-coat-source-chain-changed");
    auto proposed=attrs;for(int n=0;n<p.boneCount;++n){auto bone=p.bones[n];SourceUpperCoatSelection(p,n,bone);proposed[ids[n]]=bone.attribute;}
    const auto peers=SharedSelection(s,id,ids,proposed);
    Need(peers.proofs.empty()&&peers.foreign.empty(),"auto-upper-coat-input-owned-by-peer");
    if(surface)surface->legReport+=" releasedWaistFixed=9 promotedUpperAttachments=7 upperOriginsRetained=4 sourceSkin=retained contact=Point";
    const auto revised=std::string(p.signature)+"\nupper-coat-higher-attachments-free-waist-v2";
    p.signature=text(Digest(Bytes(revised.begin(),revised.end())));
  }
  if(bodyOnly){p.runtimeBodyOnly=true;out->faces.resize(1);out->lines.resize(1);out->graphs.resize(1);
    for(size_t n=0;n<out->bones.size();++n){const auto &b=out->bones[n];if(b.attribute&&b.parent>=0&&out->bones[b.parent].attribute)out->lines[0].push_back({b.parent,int(n)});}out->Link();}
  else if(irregular){std::vector<Point> points;for(auto b:ids)points.push_back(Transform(s.World(b),{0,0,0}));GraphVariants(*out,points);}
  eiem_cloth_cache::Validate(*out);
  Need(!shortSkin||SourceShortContract(p),"short-source-profile-contract");
  if(surface){surface->meshes=std::move(meshes);surface->chains=std::move(chains);surface->order=order;surface->groups=groups;surface->ids=ids;surface->singleSheet=singleSheet;surface->spatialSelection=spatialSelection;surface->densityIssue=std::move(densityIssue);}
  return out;
}
}
#include "cloth_asset_apron.h"
#include "cloth_asset_ribbon.h"
#include "cloth_asset_native_layers.h"
#include "cloth_asset_coat_calves.h"
#include "cloth_asset_coat_torso.h"
#include "cloth_asset_body_contact.h"
#include "cloth_asset_unowned.h"
namespace eiem_cloth_asset {
inline Generated Generate(const Vfs &vfs,const Manifest &manifest,const Query &captured){Need(!captured.renderers.empty(),"auto-owner-input-empty");
  for(const auto &r:captured.renderers)Need(!r.offlineBinding||!captured.modelPath.empty(),"offline-binding-requires-exact-model-path");
  std::set<int> shortlist;size_t best=SIZE_MAX;
  if(!captured.modelPath.empty()){
    const auto path=Lower(captured.modelPath);Need(ResourcePath(path)&&path.find('/')!=std::string::npos,"auto-model-resource-path-invalid");
    for(const auto &a:manifest.assets){auto name=Lower(a.name);if(name.size()<7||name.compare(name.size()-7,7,".prefab"))continue;
      for(int extension=0;extension<2;++extension){if(name==path||(name.size()>path.size()&&name.compare(name.size()-path.size(),path.size(),path)==0&&name[name.size()-path.size()-1]=='/'))shortlist.insert(a.bundleId);if(!extension)name.resize(name.size()-7);}}
    Need(shortlist.size()==1,"auto-model-resource-path-not-unique-in-manifest");
  }
  if(captured.modelPath.empty())for(const auto &renderer:captured.renderers){std::set<int> modelBundles;const auto name=Lower(renderer.mesh);for(const auto &a:manifest.assets){const auto leaf=Lower(Leaf(a.name));if(leaf==name+".asset"||leaf==name+".fbx")modelBundles.insert(a.bundleId);}if(modelBundles.empty())continue;
    std::set<int> candidates;for(const auto &a:manifest.assets)if(a.name.size()>=7&&a.name.compare(a.name.size()-7,7,".prefab")==0){const auto &deps=manifest.bundles[a.bundleId].dependencies;bool match=modelBundles.count(a.bundleId)!=0;for(int dep:deps)match|=modelBundles.count(dep)!=0;if(match)candidates.insert(a.bundleId);}if(!candidates.empty()&&candidates.size()<best){best=candidates.size();shortlist=std::move(candidates);}}
  Need(!shortlist.empty()&&shortlist.size()<=32,"auto-prefab-source-unavailable-or-ambiguous");Generated result;bool matched=false;std::string bindingIssue;
  for(int bundle:shortlist){CheckCancel(vfs.cancel);Package package(vfs,manifest);package.Load(bundle);std::vector<std::shared_ptr<SerializedFile>> prefabs;for(auto &entry:package.files)prefabs.push_back(entry.second);
    for(auto &file:prefabs){Scene scene(*file);std::vector<int64_t> cloths;bool rootsMatch=true;for(const auto &live:captured.cloths){int64_t found=0;for(const auto &o:file->objects)if(file->Class(o.first)==114&&RootMatch(scene,o.first,live)){Need(!found,"auto-prefab-duplicate-cloth");found=o.first;}if(!found){rootsMatch=false;break;}cloths.push_back(found);}if(!rootsMatch)continue;
      package.Models(bundle);Query query;std::vector<size_t> selected;
      if(!MatchSourceRenderers(package,scene,captured,cloths,query,selected,&bindingIssue))continue;
      const auto evidence=RelevantSource(package,scene,cloths)+file->sha;
      const auto relevant=Digest(Bytes(evidence.begin(),evidence.end()));
      if(matched){Need(result.relevantHash==relevant,"auto-source-full-binding-ambiguous-nonidentical-cloth-input");++result.equivalentSources;continue;}
      matched=true;result.source=manifest.bundles[bundle].name;result.sourceHash=file->sha;result.relevantHash=relevant;result.sources=package.sources;result.sources[manifest.sourceName]=manifest.sourceHash;
      result.rendererScopeKnown=true;result.liveRenderers=std::move(selected);
      result.reports.push_back({"renderer-scope","captured="+std::to_string(captured.renderers.size())+
          " sourceMatched="+std::to_string(query.renderers.size())+" extraPreserved="+std::to_string(captured.renderers.size()-query.renderers.size())+" fullBindingVerified=1"});
      std::set<size_t> retained(result.liveRenderers.begin(),result.liveRenderers.end());size_t extraReported=0;
      for(size_t k=0;k<captured.renderers.size()&&extraReported<8;++k)if(!retained.count(k)){
        const auto &r=captured.renderers[k];result.reports.push_back({r.name,"not-in-matched-source-renderer-scope-preserved parent="+r.parent});++extraReported;}
      SelectBodyScope(scene,query);
      if(scene.bodyRoot){int count=0;for(const auto &t:scene.goTransform)count+=Key(scene,t.second)==query.hips;
        result.reports.push_back({"body-reference","auto-Animator-relative-path-confirmed hipsCandidates="+std::to_string(count)+" selectedSkeletons=1"});}
      for(size_t k=0;k<cloths.size();++k){CheckCancel(vfs.cancel);try{
          if(RibbonSource(scene,cloths[k])){std::shared_ptr<DenseRecipe> dense;auto p=GenerateRibbon(package,scene,cloths[k],query,dense);result.profiles.push_back(p);result.dense.push_back(dense);continue;}
          SurfaceInput surface;auto p=ApronSource(scene,cloths[k])?GenerateApron(package,scene,cloths[k],query,surface):GenerateConnections(package,scene,cloths[k],query,&surface);
          if(!surface.legReport.empty())result.reports.push_back({query.cloths[k].name,surface.legReport});
          if(surface.spatialSelection)result.reports.push_back({query.cloths[k].name,"auto-selection-spatial-candidate-original-live-graph-required"});
          const bool panelFit=SourcePanelFit(p->view)||SourceApronFit(p->view);
          if((surface.singleSheet||surface.regions.selected||panelFit||SourceShortContract(p->view)||!surface.groups.empty())&&surface.densityIssue.empty())try{auto dense=GenerateDense(package,scene,cloths[k],*p,surface.ids,surface.meshes,panelFit?std::vector<std::vector<unsigned char>>{}:surface.regions.vertices,surface.groups);dense->view.partialSurface=!panelFit&&!SourceShortContract(p->view)&&!surface.singleSheet&&surface.groups.empty();dense->view.preservedVertices=panelFit?0:surface.regions.preserved;p->view.generatedLocal=&dense->view;result.dense.push_back(std::move(dense));}
            catch(const std::exception &e){CheckCancel(vfs.cancel);surface.densityIssue=e.what();}
          if(SourceInactiveCoat(p->view))try{auto dense=GenerateCoatWaist(package,scene,cloths[k],*p,surface.ids,surface.meshes);p->view.generatedLocal=&dense->view;result.dense.push_back(std::move(dense));}
            catch(const std::exception &e){CheckCancel(vfs.cancel);surface.densityIssue=e.what();}
          if(p->view.runtimeBodyOnly){auto dense=GenerateBodyContact(package,scene,cloths[k],query,*p);p->view.generatedLocal=&dense->view;result.dense.push_back(std::move(dense));}
          if(SourceShortContract(p->view))Need(p->view.generatedLocal!=nullptr,surface.densityIssue.empty()?"short-native-visible-skin-required":surface.densityIssue.c_str());
          if(p->view.inputAnchorCount)Need(p->view.generatedLocal!=nullptr,surface.densityIssue.empty()?"apron-private-skin-required":surface.densityIssue.c_str());
          if(!surface.groups.empty())Need(p->view.generatedLocal!=nullptr,surface.densityIssue.empty()?"auto-separated-panels-native-graph-required":surface.densityIssue.c_str());
          if(p->view.runtimeSeparatedCoat)Need(p->view.generatedLocal&&p->view.generatedLocal->NativePanelsOnly(),"auto-separated-coat-private-graph-required");
          if(SourceOriginalCoverageCoat(p->view.prefabSha,p->view.component)) {
            const auto r=p->view.generatedLocal;
            Need(r&&r->NativePanelsOnly()&&!r->bodyCoverage&&!r->bodySphereCount&&!r->meshCount,
                "auto-original-coverage-coat-requires-native-panels-and-original-skin");
            for(auto &d:result.dense)if(&d->view==r) {ConfigureCoatCalves(package,scene,query,p->view,*d);ConfigureCoatTorso(scene,query,p->view,*d);}
            Need(r->CoatCalfCoverage()&&r->CoatTorsoCoverage(),"auto-coat-body-coverage-missing");
          }
          if(SourceForkCoatFront(p->view)) {auto dense=GenerateForkCoatBody(package,scene,cloths[k],query,*p);
            p->view.generatedLocal=&dense->view;result.dense.push_back(std::move(dense));}
          if(p->view.generatedLocal)for(const auto &d:result.dense)if(&d->view==p->view.generatedLocal&&!d->densityReport.empty())result.reports.push_back({query.cloths[k].name,d->densityReport});
          if(!surface.densityIssue.empty())result.reports.push_back({query.cloths[k].name,surface.densityIssue});
          if(!surface.sheetIssue.empty()&&surface.sheetIssue!=surface.densityIssue&&!(p->view.generatedLocal&&(p->view.generatedLocal->FittedSkin()||p->view.generatedLocal->CoatWaistSkinOnly())))result.reports.push_back({query.cloths[k].name,surface.sheetIssue});result.profiles.push_back(std::move(p));
        }catch(const std::exception &e){CheckCancel(vfs.cancel);result.reports.push_back({query.cloths[k].name,e.what()});}}
      ConfigureArdeliaLayers(package,scene,query,result);
      try{GenerateUnowned(package,scene,query,result);}catch(const std::exception &e){CheckCancel(vfs.cancel);result.reports.push_back({"unowned-waist-discovery",e.what()});}
      result.meshReads+=scene.meshReads;result.meshCacheHits+=scene.meshCacheHits;result.meshCacheBytes+=scene.meshCacheBytes;result.meshDecodeMs+=scene.meshDecodeMs;
      result.sources=package.sources;result.sources[manifest.sourceName]=manifest.sourceHash;
      std::string key="runtime-effective-graph-v3-dense-regions-v2-selection-v2-ownership-v1-waist-v2-bind-domain-v1-panel-fit-v3-long-skin-envelope-calf-surface-v5-ribbon-width-v1-separated-panels-lines-v2-short-native-v1-layer-calf-short-sides-v2-fixed-apron-v1-bundle-v2-cell-aspect-v1-leg-coverage-native-lines-v1-isolated-strip-width-v1-fixed-fork-coat-v7-waist-field-prebuild-closure-v2-body-contact-v1-unowned-waist-v1-Animator-body-scope-v1-separated-panels-six-v1-collider-Animator-parent-v1-separated-coat-inputs-point-flexible-v1\n"+manifest.sourceHash;for(const auto &p:package.sources)key+="\n"+p.first+"="+p.second;key+="\nendminm-upper-coat-source-skin-interior-release-point-v7-endmin-torso-chest-waist-v1-upper-hangers-free-lower-v2-pelvis-overlap-v1";result.key=Digest(Bytes(key.begin(),key.end()));
    }
  }Need(matched,bindingIssue.empty()?"auto-source-full-binding-not-confirmed":bindingIssue.c_str());
  ResolveGeneratedRenderers(result);
  Need(vfs.StillCurrent()&&Digest(vfs.Read(manifest.sourceName))==manifest.sourceHash,"auto-source-updated-during-generation");return result;
}
}
