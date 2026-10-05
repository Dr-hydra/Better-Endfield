#pragma once
namespace eiem_cloth_asset {
inline bool ApronSource(Scene &s,int64_t id) {
  return s.file.sha==ApronPrefab&&s.Name(id)=="MC_frontSkirt";
}
inline void ApronLayerSource(Scene &s,const Value &inner,int64_t outer,const std::vector<int64_t> &ids) {
  Need(ids.size()==6&&s.Name(outer)=="MC_frontCloth","apron-layer-source-identity");
  const auto &other=s.file.Get(outer).At("serializeData");
  const auto incoming=Refs(inner.At("colliderCollisionConstraint").At("colliderList"));
  const auto response=Refs(other.At("colliderCollisionConstraint").At("colliderList"));
  Need(incoming.size()==6&&response.size()==7&&other.At("clothType").Int()==1&&
       other.At("colliderCollisionConstraint").At("mode").Int()==1,"apron-layer-source-policy");
  for(const auto *d:{&inner,&other}){
    const auto &contact=d->At("selfCollisionConstraint");
    Need(contact.At("selfMode").Int()==0&&contact.At("syncMode").Int()==0&&
         !contact.At("syncPartner").At("m_PathID").Int(),"apron-layer-existing-surface-relation");
  }
  for(int n=0;n<4;++n)Need(std::count(response.begin(),response.end(),incoming[n])==1,"apron-layer-body-reference-mismatch");
  for(int n:{1,4}){
    int count=0;for(auto c:response)count+=s.TransformId(c)==ids[n];
    Need(count==1,"apron-layer-inner-response-missing-or-ambiguous");
  }
  const auto roots=Refs(other.At("rootBones"));Need(roots.size()==2,"apron-layer-outer-roots");
  for(int n:{4,5}){
    const auto t=s.TransformId(incoming[n]);int count=0;
    for(auto root:roots)count+=Above(s,t,root);
    Need(count==1,"apron-layer-incoming-volume-not-outer-owned");
  }
}
inline std::shared_ptr<eiem_cloth_cache::Profile> GenerateApron(Package &package,Scene &s,int64_t id,const Query &query,SurfaceInput &surface) {
  Need(ApronSource(s,id),"apron-source-content-mismatch");const auto &obj=s.file.Get(id),&sd=obj.At("serializeData"),&sd2=obj.At("serializeData2");
  Need(sd.At("clothType").Int()==1&&sd.At("connectionMode").Int()==0&&sd.At("colliderCollisionConstraint").At("mode").Int()==1,"apron-original-line-point-contract");
  for(const auto *d:{&sd,&sd2})if(d->Has("preBuildData"))Need(d->At("preBuildData").At("enabled").Int()==0,"apron-prebuild-unsupported");
  const auto roots=Refs(sd.At("rootBones"));auto branch=EffectiveBranch(s,sd);Need(roots.size()==2&&branch.ids.size()==4&&branch.ignored.empty()&&branch.excluded.empty(),"apron-source-closure");
  const auto sourceAttributes=SourceSelection(s,id,sd2,branch.ids);std::vector<int64_t> ids;std::map<int64_t,int> attrs;
  const auto hips=FindBodyKey(s,query.hips),left=FindBodyKey(s,query.leftThigh),right=FindBodyKey(s,query.rightThigh);
  for(auto root:roots){const auto parent=s.Parent(root);Need(parent&&Above(s,parent,hips)&&!Above(s,parent,left)&&!Above(s,parent,right)&&parent!=hips,"apron-nonbody-attachment");
    Need(s.children[parent].size()==1&&s.children[parent][0]==root&&s.children[root].size()==1,"apron-single-child-attachment");const auto tip=s.children[root][0];
    Need(sourceAttributes.at(root)==1&&sourceAttributes.at(tip)==2&&s.children[tip].empty(),"apron-source-fixed-move-attributes");
    ids.insert(ids.end(),{parent,root,tip});attrs[parent]=1;attrs[root]=1;attrs[tip]=2;}
  Need(s.Parent(ids[0])==s.Parent(ids[3])&&ids[0]!=ids[3],"apron-shared-waist-parent");
  const auto ownership=SharedSelection(s,id,ids,attrs);Need(ownership.foreign.empty(),"apron-attachment-has-another-writer");
  for(const auto &proof:ownership.proofs)Need(proof.attribute<=0,"apron-overlapping-active-output");
  auto out=std::make_shared<eiem_cloth_cache::Profile>();auto text=[&](const std::string &v){Need(!v.empty()&&v.size()<128,"apron-name-budget");out->strings.push_back(v);return out->strings.back().c_str();};auto &p=out->view;
  p.component=text(s.Name(id));p.prefabSha=text(s.file.sha);p.loop=false;p.depth=3;p.runtimeGenerated=true;
  std::map<int64_t,int> index;for(size_t n=0;n<ids.size();++n)index[ids[n]]=int(n);
  for(size_t n=0;n<ids.size();++n){const auto b=ids[n],parent=s.Parent(b);const auto &v=s.file.Get(b),&q=v.At("m_LocalRotation");const auto t=Vec(v.At("m_LocalPosition")),scale=Vec(v.At("m_LocalScale"));
    out->bones.push_back({text(s.Name(b)),text(s.Name(parent)),index.count(parent)?index.at(parent):-1,attrs.at(b),int(n/3),int(n%3),{float(t[0]),float(t[1]),float(t[2])},{float(q.At("x").Number()),float(q.At("y").Number()),float(q.At("z").Number()),float(q.At("w").Number())},{float(scale[0]),float(scale[1]),float(scale[2])}});}
  out->roots={0,3};out->originalRoots={1,4};out->inputAnchors={0,3};
  for(const auto &proof:ownership.proofs)out->ownership.push_back({text(s.Name(proof.component)),index.at(proof.bone),proof.attribute});
  size_t fixedSkin=0;
  for(const auto &o:s.file.objects)if(s.file.Class(o.first)==137){const auto boneIds=Refs(s.file.Get(o.first).At("m_Bones"));bool uses=false;for(auto b:boneIds)uses|=index.count(b)!=0;if(!uses)continue;
    auto m=ReadMesh(package,s,o.first);Need(!query.reservedRenderers.count({m.name,m.parent})&&!query.reservedRenderers.count({m.name,""}),"apron-renderer-already-owned");
    size_t matches=0;for(const auto &live:query.renderers)matches+=RendererMatch(s,m,live);Need(matches==1,"apron-live-binding-unconfirmed");
    std::vector<ClothBoneBinding> bindings;for(size_t n=0;n<m.bones.size();++n){const auto b=m.bones[n];ClothBoneBinding v{text(s.Name(b)),text(s.Name(s.Parent(b))),index.count(b)?index.at(b):-1,{}};for(int k=0;k<16;++k)v.bind[k]=float(m.binds[n][k]);bindings.push_back(v);}
    for(size_t v=0;v<m.weights.size();++v)for(size_t k=0;k<m.weights[v].size();++k)if(m.weights[v][k]>0){const auto b=m.bones[size_t(m.indices[v][k])];if(!index.count(b))continue;Need(attrs.at(b)==1,"apron-source-visible-skin-is-no-longer-fixed-only");++fixedSkin;}
    out->bindings.push_back(std::move(bindings));out->renderers.push_back({text(m.name),text(m.mesh),text(m.root),int(m.world.size()),m.submeshes,nullptr,0,text(m.parent)});surface.meshes.push_back(std::move(m));}
  Need(fixedSkin>0&&!surface.meshes.empty()&&surface.meshes.size()<=16,"apron-visible-source-coverage");
  std::set<int64_t> producers;
  for(auto c:Refs(sd.At("colliderCollisionConstraint").At("colliderList"))){const auto t=s.TransformId(c);out->colliders.push_back({text(s.Name(c)),text(s.Name(s.Parent(t)))});
    for(const auto &o:s.file.objects)if(o.first!=id&&s.file.Class(o.first)==114){const auto &other=s.file.Get(o.first).At("serializeData");if(!other.Has("rootBones"))continue;
      for(auto root:Refs(other.At("rootBones")))if(Above(s,t,root))producers.insert(o.first);}}
  Need(out->colliders.size()==6&&producers.size()==1,"apron-body-collider-source-count");
  ApronLayerSource(s,sd,*producers.begin(),ids);
  for(auto producer:producers)out->nativeProducers.push_back(text(s.Name(producer)));
  std::string signature="runtime-fixed-apron-source-v1\n"+s.file.sha+"\n"+std::to_string(id);for(const auto &source:package.sources)signature+="\n"+source.first+"="+source.second;
  p.signature=text(Digest(Bytes(signature.begin(),signature.end())));out->Link();std::vector<Point> points;for(auto b:ids)points.push_back(Transform(s.World(b),{0,0,0}));GraphVariants(*out,points);
  eiem_cloth_cache::Validate(*out);Need(SourceApronFit(p),"apron-profile-contract");surface.ids=std::move(ids);surface.singleSheet=false;return out;
}
}
