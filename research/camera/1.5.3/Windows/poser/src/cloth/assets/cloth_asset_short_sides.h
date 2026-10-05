#pragma once
namespace eiem_cloth_asset {
inline void CompleteShortSides(Package &package,Scene &scene,eiem_cloth_cache::Profile &source,DenseRecipe &out) {
  Need(SourceShortContract(source.view)&&!SourceShortSides(source.view)&&out.view.sourceShortSkin&&out.meshes.empty(),"short-side-base-contract");
  auto text=[&](const std::string &s){Need(!s.empty()&&s.size()<128,"short-side-name-budget");source.strings.push_back(s);return source.strings.back().c_str();};
  std::vector<int64_t> ids;for(const auto &b:source.bones)ids.push_back(FindKey(scene,{b.name,b.parentName}));
  const auto parent=scene.Parent(ids[source.originalRoots[0]]);const auto parentWorld=scene.World(parent);
  for(int side=0;side<2;++side)for(int row=0;row<3;++row){const auto name=std::string(side?"R":"L")+"_shortskirtB_0"+std::to_string(row+1)+"_jnt";
    const auto id=FindKey(scene,{name,row?scene.Name(ids.back()):scene.Name(parent)});
    Need(scene.children[id].size()==size_t(row==2?0:1),"short-side-source-branch-shape");
    if(row)Need(scene.children[ids.back()][0]==id,"short-side-source-parent");
    const auto &v=scene.file.Get(id),&q=v.At("m_LocalRotation");const auto p=Vec(v.At("m_LocalPosition")),scale=Vec(v.At("m_LocalScale"));
    const int n=int(ids.size());source.bones.push_back({text(name),text(scene.Name(scene.Parent(id))),row?n-1:-1,0,-1,-1,
        {float(p[0]),float(p[1]),float(p[2])},{float(q.At("x").Number()),float(q.At("y").Number()),float(q.At("z").Number()),float(q.At("w").Number())},{float(scale[0]),float(scale[1]),float(scale[2])}});
    source.sourceBranches.push_back(n);ids.push_back(id);}
  int64_t component=0;for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==114&&scene.Name(o.first)==source.view.component){Need(!component,"short-side-BBC-ambiguous");component=o.first;}
  Need(component!=0,"short-side-BBC-missing");std::map<int64_t,int> wanted;for(auto id:ids)wanted[id]=2;
  const auto ownership=SharedSelection(scene,component,ids,wanted);Need(ownership.foreign.empty()&&ownership.proofs.empty(),"short-side-competing-source-owner");
  std::vector<int> starts(source.originalRoots.begin(),source.originalRoots.end());starts.insert(starts.end(),{12,15});
  std::map<int64_t,int> column;for(int c=0;c<6;++c)for(int d=0;d<3;++d)column[ids[starts[c]+d]]=c;
  std::set<std::pair<int,int>> adjacency;size_t sideWeights[2]{};
  for(size_t k=0;k<source.renderers.size();++k){const auto &asset=source.renderers[k];
    for(auto &binding:source.bindings[k])for(int n=12;n<18;++n)if(!strcmp(binding.name,source.bones[n].name)&&!strcmp(binding.parent,source.bones[n].parentName))binding.cloth=n;
    if(std::string(asset.name).find("_lod0")==std::string::npos)continue;
    CheckCancel(package.vfs.cancel);int64_t renderer=0;
    for(const auto &o:scene.file.objects)if(scene.file.Class(o.first)==137&&scene.Name(o.first)==asset.name&&scene.Name(scene.Parent(scene.TransformId(o.first)))==(asset.parent?asset.parent:"")){Need(!renderer,"short-side-renderer-ambiguous");renderer=o.first;}
    Need(renderer!=0,"short-side-renderer-missing");const auto m=ReadMesh(package,scene,renderer);
    std::vector<std::set<int>> owners(m.world.size());std::vector<bool> pure(m.world.size(),true);
    for(size_t v=0;v<m.world.size();++v)for(size_t b=0;b<m.weights[v].size();++b)if(m.weights[v][b]>0){const auto id=m.bones[size_t(m.indices[v][b])];
      if(!column.count(id))pure[v]=false;else {const int c=column.at(id);owners[v].insert(c);if(c>=4)++sideWeights[c-4];}}
    for(auto f:m.triangles)if(pure[f[0]]&&pure[f[1]]&&pure[f[2]]){std::set<int> c;for(int v:f)c.insert(owners[v].begin(),owners[v].end());if(c.size()==2)adjacency.insert({*c.begin(),*c.rbegin()});}}
  bool loop=false;const auto order=ChainOrder(6,adjacency,loop);Need(loop&&sideWeights[0]>100&&sideWeights[1]>100,"short-side-visible-ring-unconfirmed");
  std::string signature=std::string(source.view.signature)+"\nsource-visible-side-branches-v1";source.view.signature=text(Digest(Bytes(signature.begin(),signature.end())));source.Link();
  Need(SourceShortSides(source.view),"short-side-source-profile");
  auto &r=out.view;r.sourceShortSides=true;r.loop=true;r.separatedPanels=0;r.originalCount=18;
  r.baseSignature=out.String(source.view.signature);r.signature=out.String(Digest(Bytes(signature.begin(),signature.end())));
  out.added.clear();out.parents.clear();out.roots.clear();out.columns.assign(18,-1);out.radii.clear();out.cross.clear();out.faces.clear();out.lines.clear();
  std::vector<ClothBoneAsset> bones=source.bones;std::vector<Matrix> world;std::vector<Point> points;
  for(auto id:ids){world.push_back(scene.World(id));points.push_back(Transform(world.back(),{0,0,0}));}
  std::vector<std::vector<int>> chains;
  for(int c=0;c<6;++c){const int start=starts[order[c]];std::vector<int> chain;
    for(int d=0;d<3;++d){const int n=start+d;auto &b=bones[n];b.column=2*c;b.depth=d;b.attribute=d?2:1;out.columns[n]=b.column;chain.push_back(n);}chains.push_back(chain);}
  const auto originalLengths=NativeFixedPathLengths(bones,points);const double maximum=*std::max_element(originalLengths.begin(),originalLengths.end());
  std::vector<std::vector<int>> dense;
  for(int c=0;c<6;++c){const auto &a=chains[c],&b=chains[(c+1)%6];dense.push_back(a);out.roots.push_back(a[0]);std::vector<int> chain;
    for(int d=0;d<3;++d){const auto wanted=MidReference(world[a[d]],world[b[d]]),parentPose=d?world[chain.back()]:parentWorld;const auto local=Mul(Inverse(parentPose),wanted);
      ClothBoneAsset bone{};bone.name=out.String("EIEM_Auto_ShortSides_"+std::to_string(c)+"_"+std::to_string(d));bone.parentName=d?bones[chain.back()].name:out.String(scene.Name(parent));bone.parent=d?chain.back():-1;
      bone.column=2*c+1;bone.depth=d;bone.attribute=d?2:1;bone.position={float(local[12]),float(local[13]),float(local[14])};bone.rotation=QuaternionOf(local);bone.scale={1,1,1};
      chain.push_back(int(bones.size()));bones.push_back(bone);out.added.push_back(bone);out.parents.push_back(source.originalRoots[0]);world.push_back(Mul(parentPose,BoneMatrix(bone)));points.push_back(Transform(world.back(),{0,0,0}));}
    dense.push_back(chain);out.roots.push_back(chain[0]);}
  const auto lengths=NativeFixedPathLengths(bones,points);Need(*std::max_element(lengths.begin(),lengths.end())<=maximum+.00001,"short-side-length-normalization");
  for(size_t n=18;n<bones.size();++n)out.radii.push_back(CurveAt(out.radiusCurve,lengths[n]/maximum));
  for(bool reverse:{false,true}){auto graph=NativeGraph(points,bones,12,true,reverse);Need(graph.faces.size()==48&&graph.lines.empty(),"short-side-native-ring-graph");
    if(out.faces.empty()||graph.faces!=out.faces[0]){out.faces.push_back(graph.faces);out.lines.push_back(graph.lines);}}
  for(int c=0;c<12;++c)for(int d=1;d<3;++d){std::array<int,2> e{dense[c][d],dense[(c+1)%12][d]};std::sort(e.begin(),e.end());out.cross.push_back(e);}
  out.Link();source.Link();
}
}
