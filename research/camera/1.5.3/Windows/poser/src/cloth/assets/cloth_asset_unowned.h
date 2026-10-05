#pragma once
namespace eiem_cloth_asset {
inline bool UnownedWaistChain(const std::vector<Point> &points,Point hips,Point up,double leg) {
  const double len=std::sqrt(Dot(up,up));
  if(!std::isfinite(leg)||leg<.1||leg>2||!std::isfinite(len)||len<.001||points.size()<3||points.size()>8)return false;
  for(auto &v:up)v/=len;
  for(const auto &p:points)for(double v:p)if(!std::isfinite(v))return false;
  const double h=Dot(Sub(points.front(),hips),up),drop=Dot(Sub(points.front(),points.back()),up);
  if(h<-.5*leg||h>.35*leg||drop<.08*leg||drop>1.6*leg)return false;
  for(size_t n=1;n<points.size();++n) {
    const double d=Distance(points[n],points[n-1]),down=Dot(Sub(points[n-1],points[n]),up);
    if(d<.01*leg||d>.7*leg||down<.35*d)return false;
  }
  return true;
}
inline std::vector<std::vector<int>> UnownedGroups(size_t count,const std::set<std::pair<int,int>> &pairs) {
  Need(count<=128,"auto-unowned-chain-budget");std::vector<std::vector<int>> adjacent(count),groups;std::vector<bool> seen(count);
  for(auto e:pairs){Need(e.first>=0&&e.first<e.second&&size_t(e.second)<count,"auto-unowned-adjacency");adjacent[e.first].push_back(e.second);adjacent[e.second].push_back(e.first);}
  for(int n=0;n<int(count);++n)if(!seen[n]){std::vector<int> group{n};seen[n]=true;
    for(size_t k=0;k<group.size();++k)for(int next:adjacent[group[k]])if(!seen[next]){seen[next]=true;group.push_back(next);}groups.push_back(std::move(group));}
  return groups;
}
inline bool UnownedRingEnclosesBody(const std::vector<Point> &roots,Point hips,Point up,Point side,double leg) {
  const double u=std::sqrt(Dot(up,up)),v=std::sqrt(Dot(side,side));if(!std::isfinite(u)||!std::isfinite(v)||u<.001||v<.001||roots.size()<4)return false;
  for(auto &x:up)x/=u;const double project=Dot(up,side);for(int k=0;k<3;++k)side[k]-=up[k]*project;
  const double width=std::sqrt(Dot(side,side));if(width<.001)return false;for(auto &x:side)x/=width;const auto forward=Cross(up,side);
  std::vector<double> angles;for(auto p:roots){p=Sub(p,hips);const double x=Dot(p,side),z=Dot(p,forward),r=std::hypot(x,z);if(!std::isfinite(r)||r<.06*leg||r>1.2*leg)return false;angles.push_back(std::atan2(z,x));}
  constexpr double pi=3.14159265358979323846;double total=0;int sign=0;
  for(size_t k=0;k<angles.size();++k){double d=angles[(k+1)%angles.size()]-angles[k];while(d>pi)d-=2*pi;while(d<-pi)d+=2*pi;
    if(std::abs(d)<.02||std::abs(d)>.95*pi||(sign&&d*sign<=0))return false;sign=d>0?1:-1;total+=d;}
  return std::abs(std::abs(total)-2*pi)<.001;
}
inline void GenerateUnowned(Package &package,Scene &s,const Query &q,Generated &result) {
  const auto hips=FindBodyKey(s,q.hips),spine=FindBodyKey(s,q.spine),left=FindBodyKey(s,q.leftThigh),right=FindBodyKey(s,q.rightThigh);
  if(q.leftCalf.name.empty()||q.rightCalf.name.empty())return;
  const auto hp=Transform(s.World(hips),{0,0,0}),up=Sub(Transform(s.World(spine),{0,0,0}),hp);
  const double leg=.5*(Distance(Transform(s.World(left),{0,0,0}),Transform(s.World(FindBodyKey(s,q.leftCalf)),{0,0,0}))+
      Distance(Transform(s.World(right),{0,0,0}),Transform(s.World(FindBodyKey(s,q.rightCalf)),{0,0,0})));
  std::set<int64_t> owned;
  for(const auto &o:s.file.objects)if(s.file.Class(o.first)==114){const auto &sd=s.file.Get(o.first).At("serializeData");if(!sd.Has("rootBones"))continue;
    for(auto b:s.Branch(Refs(sd.At("rootBones"))))owned.insert(b);}
  for(auto parent:std::set<int64_t>{hips,spine}) {
    std::vector<std::vector<int64_t>> chains;std::map<int64_t,int> column;
    for(auto root:s.children[parent]) {
      std::vector<int64_t> chain;std::vector<Point> points;auto b=root;
      while(b&&!owned.count(b)&&b!=left&&b!=right&&b!=spine&&chain.size()<=8){chain.push_back(b);points.push_back(Transform(s.World(b),{0,0,0}));
        const auto &children=s.children[b];if(children.size()>1){b=-1;break;}b=children.empty()?0:children[0];}
      if(b||!UnownedWaistChain(points,hp,up,leg))continue;
      Need(chains.size()<128,"auto-unowned-chain-budget");for(auto bone:chain)column[bone]=int(chains.size());chains.push_back(std::move(chain));
    }
    if(chains.size()<4)continue;
    std::vector<MeshView> meshes;std::set<std::pair<int,int>> pairs;std::vector<double> moving(chains.size());
    for(const auto &o:s.file.objects)if(s.file.Class(o.first)==137){bool uses=false;for(auto b:Refs(s.file.Get(o.first).At("m_Bones")))uses|=column.count(b)!=0;if(!uses)continue;
      auto mesh=ReadMesh(package,s,o.first);int matches=0;for(const auto &live:q.renderers)matches+=RendererMatch(s,mesh,live);
      Need(matches==1,"auto-unowned-live-renderer-missing-or-ambiguous");
      std::vector<std::set<int>> labels(mesh.vertices);std::vector<bool> pure(mesh.vertices,true);
      for(size_t v=0;v<mesh.weights.size();++v)for(size_t k=0;k<mesh.weights[v].size();++k)if(mesh.weights[v][k]>0){const auto b=mesh.bones[size_t(mesh.indices[v][k])];
        if(!column.count(b)){pure[v]=false;continue;}const int c=column.at(b);labels[v].insert(c);if(b!=chains[c][0])moving[c]+=mesh.weights[v][k];}
      for(auto f:mesh.triangles)if(pure[f[0]]&&pure[f[1]]&&pure[f[2]]){std::set<int> labelsAll;for(int v:f)labelsAll.insert(labels[v].begin(),labels[v].end());if(labelsAll.size()==2)pairs.insert({*labelsAll.begin(),*labelsAll.rbegin()});}
      meshes.push_back(std::move(mesh));
    }
    for(const auto &group:UnownedGroups(chains.size(),pairs)) {
      if(group.size()<4)continue;
      const std::string label="unowned-waist:"+s.Name(chains[group.front()][0]);
      try {
        Need(group.size()<=16,"auto-unowned-ring-root-budget");const size_t depth=chains[group[0]].size();
        std::map<int,int> localColumn;for(int n=0;n<int(group.size());++n){Need(chains[group[n]].size()==depth&&moving[group[n]]>=1.,"auto-unowned-depth-or-visible-Move-unconfirmed");localColumn[group[n]]=n;}
        std::set<std::pair<int,int>> edges;for(auto e:pairs)if(localColumn.count(e.first)&&localColumn.count(e.second)){int a=localColumn.at(e.first),b=localColumn.at(e.second);edges.insert({(std::min)(a,b),(std::max)(a,b)});}
        bool loop=false;const auto order=ChainOrder(group.size(),edges,loop);Need(loop,"auto-unowned-open-or-complex-sheet-needs-policy");
        std::vector<Point> rootPoints;for(int n:order)rootPoints.push_back(Transform(s.World(chains[group[n]][0]),{0,0,0}));
        Need(UnownedRingEnclosesBody(rootPoints,hp,up,Sub(Transform(s.World(right),{0,0,0}),Transform(s.World(left),{0,0,0})),leg),"auto-unowned-sheet-not-a-body-enclosing-waist-ring");
        std::set<int64_t> selected;std::map<int64_t,int> index;
        auto out=std::make_shared<eiem_cloth_cache::Profile>();auto text=[&](const std::string &v){Need(!v.empty()&&v.size()<128,"auto-unowned-name-budget");out->strings.push_back(v);return out->strings.back().c_str();};
        auto &p=out->view;p.prefabSha=text(s.file.sha);p.runtimeGenerated=p.runtimeUnowned=p.loop=true;p.depth=int(depth);
        std::string identity=s.file.sha;double shortest=leg;
        for(int c=0;c<int(order.size());++c){const auto &chain=chains[group[order[c]]];out->roots.push_back(int(out->bones.size()));out->originalRoots.push_back(int(out->bones.size()));
          for(int d=0;d<int(chain.size());++d){const auto id=chain[d],parentId=s.Parent(id);selected.insert(id);index[id]=int(out->bones.size());const auto &t=s.file.Get(id),&r=t.At("m_LocalRotation");const auto pos=Vec(t.At("m_LocalPosition")),scale=Vec(t.At("m_LocalScale"));
            out->bones.push_back({text(s.Name(id)),text(s.Name(parentId)),d?index.at(chain[d-1]):-1,d?2:1,c,d,
                {float(pos[0]),float(pos[1]),float(pos[2])},{float(r.At("x").Number()),float(r.At("y").Number()),float(r.At("z").Number()),float(r.At("w").Number())},{float(scale[0]),float(scale[1]),float(scale[2])}});
            identity+="\n"+s.Name(id)+":"+s.Name(parentId);if(d)shortest=(std::min)(shortest,Distance(Transform(s.World(id),{0,0,0}),Transform(s.World(parentId),{0,0,0})));}}
        const auto signature=Digest(Bytes(identity.begin(),identity.end()));p.component=text("EIEM_AutoWaist_"+signature.substr(0,12));p.signature=text("runtime-unowned-waist-v1-"+signature);
        p.unownedRadius=float((std::max)(.002,(std::min)(.008,shortest*.06)));
        for(const auto &mesh:meshes){bool uses=false;for(auto b:mesh.bones)uses|=selected.count(b)!=0;if(!uses)continue;
          Need(!q.reservedRenderers.count({mesh.name,mesh.parent})&&!q.reservedRenderers.count({mesh.name,""}),"auto-unowned-renderer-reserved-by-authored-recipe");
          std::vector<ClothBoneBinding> bindings;for(size_t k=0;k<mesh.bones.size();++k){const auto b=mesh.bones[k];ClothBoneBinding bind{text(s.Name(b)),text(s.Name(s.Parent(b))),index.count(b)?index.at(b):-1,{}};
            for(int j=0;j<16;++j)bind.bind[j]=float(mesh.binds[k][j]);bindings.push_back(bind);}
          out->bindings.push_back(std::move(bindings));out->renderers.push_back({text(mesh.name),text(mesh.mesh),text(mesh.root),mesh.vertices,mesh.submeshes,nullptr,0,text(mesh.parent)});
        }
        for(auto thigh:{left,right}){int found=0;
          for(const auto &o:s.file.objects)if(s.file.Class(o.first)==114){const auto &c=s.file.Get(o.first);if(!c.Has("direction")||!c.Has("radiusSeparation")||s.TransformId(o.first)!=thigh)continue;
            ++found;int axis=-1;for(int k=0;k<3;++k)if(q.capsuleDirections[k]>=0&&c.At("direction").Int()==q.capsuleDirections[k])axis=k;
            Need(axis>=0,"auto-unowned-capsule-axis-unconfirmed");const auto center=Vec(c.At("center")),size=Vec(c.At("size"));
            const bool reverse=c.At("reverseDirection").Int()!=0,separate=c.At("radiusSeparation").Int()!=0,aligned=c.At("alignedOnCenter").Int()!=0;
            (void)SourceWorldCapsule(s.World(thigh),center,size,axis,reverse,separate,aligned);
            out->colliders.push_back({text(s.Name(thigh)),text(s.Name(s.Parent(thigh)))});
            out->unownedGeometry.push_back({{float(center[0]),float(center[1]),float(center[2])},{float(size[0]),float(size[1]),float(size[2])},axis,reverse,separate,aligned});
          }Need(found==1,"auto-unowned-native-thigh-capsule-missing-or-ambiguous");}
        Need(!out->renderers.empty(),"auto-unowned-visible-binding-unconfirmed");out->Link();eiem_cloth_cache::Validate(*out);
        result.profiles.push_back(out);result.reports.push_back({p.component,"auto-unowned-skinned-waist-ring roots="+std::to_string(p.rootCount)+" points="+std::to_string(p.boneCount)+
            " renderers="+std::to_string(p.rendererCount)+" sourceBBCOwners=0 originalMeshesUnchanged=1 nativeThighVolumes=2 newBBCRequired=1"});
      }catch(const std::exception &e){CheckCancel(package.vfs.cancel);result.reports.push_back({label,e.what()});}
    }
  }
}
}
