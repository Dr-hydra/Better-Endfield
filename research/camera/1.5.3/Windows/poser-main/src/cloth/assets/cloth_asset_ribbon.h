#pragma once
namespace eiem_cloth_asset {
inline bool RibbonSource(Scene &s,int64_t id) {
  return s.file.sha=="d911c12a646d6f4c3f5068b63768c38b6a6a13c6fa2d58710c2b42ead69beda4"&&s.Name(id)=="MC_Seraph_Skirt_Ribbon";
}
inline std::shared_ptr<eiem_cloth_cache::Profile> GenerateRibbon(Package &package,Scene &s,int64_t id,const Query &query,std::shared_ptr<DenseRecipe> &recipe) {
  Need(RibbonSource(s,id),"ribbon-source-identity");const auto &obj=s.file.Get(id),&sd=obj.At("serializeData"),&sd2=obj.At("serializeData2");
  Need(sd.At("clothType").Int()==1&&sd.At("connectionMode").Int()==0&&sd.At("colliderCollisionConstraint").At("mode").Int()==1,"ribbon-original-line-point-policy");
  const auto roots=Refs(sd.At("rootBones"));auto branch=EffectiveBranch(s,sd);const auto &ids=branch.ids;
  Need(roots.size()==1&&ids.size()==9&&branch.ignored.empty()&&branch.excluded.empty(),"ribbon-source-closure");
  const auto attrs=SourceSelection(s,id,sd2,ids);const auto prebuild=SourcePrebuild(s,id,sd2,ids,attrs);std::array<int,5> center{};center.fill(-1);
  for(size_t n=0;n<ids.size();++n)if(attrs.at(ids[n])){for(int row=0;row<5;++row)if(s.Name(ids[n])=="dress_pd_L_a_0"+std::to_string(row+1)+"_jnt_ctrl"){
      Need(center[row]<0&&attrs.at(ids[n])==(row?2:1),"ribbon-center-attribute");center[row]=int(n);}}
  for(int row=0;row<5;++row)Need(center[row]>=0&&(!row||s.Parent(ids[center[row]])==ids[center[row-1]]),"ribbon-center-chain");
  Need(ids[center[0]]==roots[0],"ribbon-source-root");
  auto out=std::make_shared<eiem_cloth_cache::Profile>();auto text=[&](const std::string &v){out->strings.push_back(v);return out->strings.back().c_str();};auto &p=out->view;
  p.component=text(s.Name(id));p.prefabSha=text(s.file.sha);p.depth=5;p.loop=false;p.runtimeGenerated=true;p.ribbonSource=true;
  if(!prebuild.empty())p.prebuildId=text(prebuild);
  std::map<int64_t,int> index;for(size_t n=0;n<ids.size();++n)index[ids[n]]=int(n);
  std::vector<Matrix> world;std::vector<Point> positions;
  for(auto b:ids){const auto &source=s.file.Get(b);const auto q=source.At("m_LocalRotation");const auto pos=Vec(source.At("m_LocalPosition")),scale=Vec(source.At("m_LocalScale"));const auto parent=s.Parent(b);int row=-1;
    for(int k=0;k<5;++k)if(ids[center[k]]==b)row=k;
    Need((row<0)==(attrs.at(b)==0),"ribbon-unexpected-active-output");
    out->bones.push_back({text(s.Name(b)),text(s.Name(parent)),index.count(parent)?index[parent]:-1,attrs.at(b),row<0?-1:0,row,
      {float(pos[0]),float(pos[1]),float(pos[2])},{float(q.At("x").Number()),float(q.At("y").Number()),float(q.At("z").Number()),float(q.At("w").Number())},{float(scale[0]),float(scale[1]),float(scale[2])}});
    world.push_back(s.World(b));positions.push_back(Transform(world.back(),{0,0,0}));}
  out->roots={center[0]};out->originalRoots=out->roots;
  out->faces.emplace_back();out->lines.emplace_back();out->graphs.emplace_back();for(int row=1;row<5;++row)out->lines[0].push_back({center[row-1],center[row]});
  std::vector<MeshView> meshes;
  for(const auto &entry:s.file.objects)if(s.file.Class(entry.first)==137){const auto palette=Refs(s.file.Get(entry.first).At("m_Bones"));bool uses=false;for(int row=1;row<5;++row)uses|=std::find(palette.begin(),palette.end(),ids[center[row]])!=palette.end();if(!uses)continue;
    auto m=ReadMesh(package,s,entry.first);size_t matched=0;for(const auto &live:query.renderers)matched+=RendererMatch(s,m,live);
    Need(matched==1&&!query.reservedRenderers.count({m.name,m.parent})&&!query.reservedRenderers.count({m.name,""}),"ribbon-live-renderer-binding-unconfirmed");
    bool positive=false;for(size_t n=0;n<m.world.size();++n)for(size_t k=0;k<m.weights[n].size();++k)if(m.weights[n][k]>0)for(int row=1;row<5;++row)positive|=m.bones[int(m.indices[n][k])]==ids[center[row]];if(!positive)continue;
    std::vector<ClothBoneBinding> binding;for(size_t k=0;k<m.bones.size();++k){const auto b=m.bones[k];ClothBoneBinding v{text(s.Name(b)),text(s.Name(s.Parent(b))),index.count(b)?index[b]:-1,{}};for(int j=0;j<16;++j)v.bind[j]=float(m.binds[k][j]);binding.push_back(v);}out->bindings.push_back(std::move(binding));
    out->renderers.push_back({text(m.name),text(m.mesh),text(m.root),m.vertices,m.submeshes,nullptr,0,text(m.parent)});meshes.push_back(std::move(m));}
  Need(!meshes.empty()&&meshes.size()<=8,"ribbon-renderer-coverage");
  int64_t producer=0;for(const auto &o:s.file.objects)if(s.file.Class(o.first)==114&&s.Name(o.first)=="MC_Seraph_Skirt"){Need(!producer,"ribbon-producer-ambiguous");producer=o.first;}
  Need(producer!=0,"ribbon-producer-missing");auto producerBones=s.Branch(Refs(s.file.Get(producer).At("serializeData").At("rootBones")));
  for(auto c:Refs(sd.At("colliderCollisionConstraint").At("colliderList"))){const auto t=s.TransformId(c);Need(std::find(producerBones.begin(),producerBones.end(),t)!=producerBones.end(),"ribbon-collider-source-not-main-skirt");out->colliders.push_back({text(s.Name(c)),text(s.Name(s.Parent(t)))});}
  Need(out->colliders.size()==3,"ribbon-native-collider-count");out->nativeProducers.push_back(text(s.Name(producer)));
  std::string signature="runtime-native-ribbon-source-v1\n"+s.file.sha+"\n"+std::to_string(id);for(const auto &source:package.sources)signature+="\n"+source.first+"="+source.second;
  p.signature=text(Digest(Bytes(signature.begin(),signature.end())));out->Link();eiem_cloth_cache::Validate(*out);
  auto d=std::make_shared<DenseRecipe>();auto &r=d->view;r.runtimeGenerated=true;r.ribbonSurface=true;r.multipleLod=true;r.loop=false;r.originalCount=9;r.originalRoots=1;r.depth=5;
  r.baseSignature=d->String(p.signature);r.prefabSha=d->String(p.prefabSha);signature="native-ribbon-width-five-columns-v1\n"+signature;r.signature=d->String(Digest(Bytes(signature.begin(),signature.end())));
  d->radiusCurve=CurveSamples(sd.At("radius"),true);d->distanceCurve=CurveSamples(sd.At("distanceConstraint").At("stiffness"),false);
  auto bones=out->bones;for(auto &b:bones)if(b.attribute)b.column=2;for(const auto &b:bones)d->columns.push_back(b.column);
  const auto best=std::max_element(meshes.begin(),meshes.end(),[](const MeshView &a,const MeshView &b){return a.vertices<b.vertices;});
  const auto inverse=Inverse(world[center[0]]);std::vector<Point> local;std::vector<bool> pure(best->world.size(),true);
  for(size_t n=0;n<best->world.size();++n){double sum=0;for(size_t k=0;k<best->weights[n].size();++k){const auto w=best->weights[n][k];sum+=w;if(w>0){const auto b=best->bones[int(best->indices[n][k])];pure[n]=pure[n]&&index.count(b)&&attrs.at(b)>0;}}auto v=best->world[n];for(auto &x:v)x/=sum;local.push_back(Transform(inverse,v));}
  std::vector<std::array<int,3>> surface;double lo=1e30,hi=-1e30;for(auto f:best->triangles)if(pure[f[0]]&&pure[f[1]]&&pure[f[2]]){surface.push_back(f);for(int v:f){lo=(std::min)(lo,local[v][0]);hi=(std::max)(hi,local[v][0]);}}
  Need(surface.size()>20&&hi-lo>.4,"ribbon-visible-surface-too-small");
  std::array<std::array<int,5>,5> grid{};for(int row=0;row<5;++row)grid[2][row]=center[row];
  for(int col:{0,1,3,4})for(int row=0;row<5;++row){const auto centre=Transform(inverse,positions[center[row]]);
    const double segment=Distance(positions[center[0]],positions[center[1]]);
    const double along=(std::max)(lo+segment*.25,(std::min)(hi-segment*.05,centre[0]));const auto section=RibbonSlice(local,surface,along);
    double left=section.front().side,right=section.back().side;
    if(row==4)for(auto f:surface)for(int v:f)if(local[v][0]<centre[0]+segment*.5){left=(std::min)(left,local[v][2]);right=(std::max)(right,local[v][2]);}
    const double side=col<2?(left+1e-6)*double(2-col)/2:(right-1e-6)*double(col-2)/2;
    auto reference=RibbonSurfacePoint(local,surface,centre[0],side);if(row==0)reference[0]=centre[0];
    const auto point=Transform(world[center[0]],reference);auto wanted=world[center[row]];for(int k=0;k<3;++k)wanted[12+k]=point[k];
    const int parent=row?grid[col][row-1]:-1;const auto parentWorld=row?world[parent]:s.World(s.Parent(roots[0]));const auto transform=Mul(Inverse(parentWorld),wanted);const int n=int(bones.size());
    ClothBoneAsset b{d->String("EIEM_RibbonWidth_"+std::to_string(col)+"_"+std::to_string(row)),row?bones[parent].name:p.bones[center[0]].parentName,parent,row?2:1,col,row,
      {float(transform[12]),float(transform[13]),float(transform[14])},QuaternionOf(transform),{1,1,1}};
    grid[col][row]=n;bones.push_back(b);d->added.push_back(b);d->parents.push_back(center[0]);world.push_back(Mul(parentWorld,BoneMatrix(b)));positions.push_back(Transform(world.back(),{0,0,0}));}
  for(const auto &chain:grid)d->roots.push_back(chain[0]);
  const auto lengths=NativeFixedPathLengths(bones,positions);const auto maximum=*std::max_element(lengths.begin(),lengths.end());
  for(size_t n=9;n<bones.size();++n)d->radii.push_back(CurveAt(d->radiusCurve,lengths[n]/maximum));
  for(bool reverse:{false,true}){auto g=NativeGraph(positions,bones,5,false,reverse);if(d->faces.empty()||d->faces[0]!=g.faces||d->lines[0]!=g.lines){d->faces.push_back(g.faces);d->lines.push_back(g.lines);}}
  for(int c=0;c<4;++c)for(int row=1;row<5;++row){std::array<int,2> e{grid[c][row],grid[c+1][row]};std::sort(e.begin(),e.end());d->cross.push_back(e);}
  const auto waist=Transform(s.World(s.Parent(roots[0])),{0,0,0});std::map<std::array<int,3>,int> oriented;
  for(const auto &graph:d->faces)for(auto f:graph){std::sort(f.begin(),f.end());const auto normal=Cross(Sub(positions[f[1]],positions[f[0]]),Sub(positions[f[2]],positions[f[0]]));auto radial=Sub(positions[f[0]],waist);radial[1]=0;
    const double size=std::sqrt(Dot(normal,normal)*Dot(radial,radial)),side=Dot(normal,radial);Need(size>1e-9&&std::abs(side)>.1*size,"ribbon-face-orientation-ambiguous");const int sign=side>0?1:-1;auto q=oriented.emplace(f,sign);Need(q.second||q.first->second==sign,"ribbon-face-orientation-conflict");}
  for(const auto &f:oriented)d->responseFaces.push_back({f.first,f.second});
  const Graph graph{d->faces[0],d->lines[0]};for(const auto &m:meshes){auto mesh=DenseBinding(package,s,m,*out,*d,world,positions,bones,graph);if(mesh.view.skinVertices)d->meshes.push_back(std::move(mesh));}
  Need(d->added.size()==20&&d->meshes.size()==meshes.size(),"ribbon-private-output-incomplete");d->Link();p.generatedLocal=&d->view;recipe=d;return out;
}
}
