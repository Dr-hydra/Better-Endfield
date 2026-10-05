#pragma once
namespace eiem_cloth_asset {
inline std::vector<int> IsolatedStripColumns(const std::vector<int> &groups) {
  std::vector<int> result;
  for(size_t c=0;c<groups.size();++c)
    if((c==0||groups[c-1]!=groups[c])&&(c+1==groups.size()||groups[c+1]!=groups[c]))result.push_back(int(c));
  return result;
}
inline std::vector<unsigned char> StripSkinRegion(const MeshView &mesh,const std::map<int64_t,int> &owned,
    const std::set<int> &strips,const ClothBoneProfile &profile) {
  std::vector<unsigned char> mask(mesh.world.size());
  for(size_t n=0;n<mask.size();++n){int column=-1;bool pure=true,move=false;
    for(size_t k=0;k<mesh.weights[n].size();++k)if(mesh.weights[n][k]>0){
      const auto found=owned.find(mesh.bones.at(size_t(mesh.indices[n][k])));
      if(found==owned.end()){pure=false;continue;}const auto &b=profile.bones[found->second];
      if(!b.attribute||!strips.count(b.column)||(column>=0&&column!=b.column)){pure=false;continue;}
      column=b.column;move|=b.attribute==2;}
    mask[n]=pure&&move;
  }return mask;
}
inline void ExpandSeparatedStrips(Scene &scene,DenseRecipe &out,const eiem_cloth_cache::Profile &base,
    const std::vector<int64_t> &ids,const std::vector<MeshView> &meshes,const std::vector<int> &groups,
    const std::vector<std::vector<int>> &chains,int64_t parent,std::vector<ClothBoneAsset> &bones,
    std::vector<Matrix> &world,std::vector<Point> &points,std::vector<std::vector<int>> &dense,
    std::vector<int> &regions,std::vector<std::vector<unsigned char>> &selected) {
  const auto &p=base.view;Need(SourceLegRibbonWidth(p)&&groups.size()==7&&groups.back()==2,"strip-width-source-contract");
  const auto singles=IsolatedStripColumns(groups);Need(singles.size()==2,"strip-width-isolated-count");
  std::map<int64_t,int> owned;for(size_t n=0;n<ids.size();++n)owned[ids[n]]=int(n);
  std::map<int,std::vector<std::vector<int>>> replacements;
  const auto attachment=scene.World(parent);
  for(int column:singles){const auto &chain=chains[column];Need(chain.size()==6,"strip-width-source-depth");
    for(size_t row=0;row<chain.size();++row)Need(bones[chain[row]].attribute==(row?2:1),"strip-width-source-selection");
    const MeshView *source=nullptr;std::vector<unsigned char> mask;
    for(const auto &mesh:meshes){auto selectedStrip=StripSkinRegion(mesh,owned,{column},p);
      if(std::count(selectedStrip.begin(),selectedStrip.end(),1)<40)continue;
      if(!source||mesh.vertices>source->vertices){source=&mesh;mask=std::move(selectedStrip);}}
    Need(source!=nullptr,"strip-width-no-isolated-surface");
    const auto inverse=Inverse(world[chain[0]]);std::vector<Point> local;
    for(size_t n=0;n<source->world.size();++n){double sum=0;for(double w:source->weights[n])sum+=w;
      Need(std::isfinite(sum)&&sum>0,"strip-width-source-weight-sum");auto v=source->world[n];for(auto &x:v)x/=sum;local.push_back(Transform(inverse,v));}
    std::vector<std::array<int,3>> surface;double low=1e30,high=-1e30;
    for(auto f:source->triangles)if(mask[f[0]]&&mask[f[1]]&&mask[f[2]]){surface.push_back(f);for(int v:f){low=(std::min)(low,local[v][0]);high=(std::max)(high,local[v][0]);}}
    Need(surface.size()>20&&high-low>.4,"strip-width-source-surface-too-small");
    std::vector<std::vector<int>> grid(5);grid[2]=chain;
    const double segment=Distance(points[chain[0]],points[chain[1]]);
    for(int c:{0,1,3,4})for(int row=0;row<6;++row){const auto center=Transform(inverse,points[chain[row]]);
      const double sample=center[0]+(row==5?segment*.25:0);
      const double along=(std::max)(low+segment*.05,(std::min)(high-segment*.25,sample));
      std::vector<RibbonSection> section;
      try{section=RibbonSlice(local,surface,along);}catch(const std::exception &e){throw std::runtime_error(std::string(e.what())+":"+bones[chain[0]].name+":row="+std::to_string(row)+":x="+std::to_string(along));}
      const double side=c<2?(section.front().side+1e-6)*double(2-c)/2:(section.back().side-1e-6)*double(c-2)/2;
      auto reference=RibbonSurfacePoint(local,surface,center[0],side);if(!row)reference[0]=center[0];
      const auto position=Transform(world[chain[0]],reference);auto wanted=world[chain[row]];
      for(int k=0;k<3;++k)wanted[12+k]=position[k];
      const int previous=row?grid[c].back():-1;const auto parentWorld=row?world[previous]:attachment;
      const auto transform=Mul(Inverse(parentWorld),wanted);const int n=int(bones.size());
      ClothBoneAsset b{out.String("EIEM_StripWidth_"+std::to_string(column)+"_"+std::to_string(c)+"_"+std::to_string(row)),
          row?bones[previous].name:out.String(scene.Name(parent)),previous,row?2:1,-1,row,
          {float(transform[12]),float(transform[13]),float(transform[14])},QuaternionOf(transform),{1,1,1}};
      grid[c].push_back(n);bones.push_back(b);out.added.push_back(b);out.parents.push_back(p.originalRoots[column]);
      world.push_back(Mul(parentWorld,BoneMatrix(b)));points.push_back(Transform(world.back(),{0,0,0}));regions.push_back(groups[column]);
    }replacements.emplace(chain[0],std::move(grid));
  }
  std::vector<std::vector<int>> expanded;
  for(const auto &chain:dense){auto found=replacements.find(chain[0]);if(found==replacements.end())expanded.push_back(chain);
    else expanded.insert(expanded.end(),found->second.begin(),found->second.end());}
  dense=std::move(expanded);Need(dense.size()==23&&bones.size()==119,"strip-width-native-budget");
  for(size_t c=0;c<dense.size();++c)if(dense[c].size()==1&&regions[dense[c][0]]<0){
    Need(c>0&&c+1<dense.size(),"strip-width-separator-position");
    const int source=dense[c-1].size()>1?dense[c-1][0]:dense[c+1][0],n=dense[c][0];
    Need(regions[source]>=0&&bones[n].parent<0&&bones[n].attribute==1,"strip-width-separator-owner");
    const auto local=Mul(Inverse(attachment),world[source]);auto &b=bones[n];
    b.position={float(local[12]),float(local[13]),float(local[14])};b.rotation=QuaternionOf(local);
    world[n]=Mul(attachment,BoneMatrix(b));points[n]=Transform(world[n],{0,0,0});
  }
  out.roots.clear();
  for(size_t c=0;c<dense.size();++c){out.roots.push_back(dense[c][0]);for(int n:dense[c])bones[n].column=int(c);}
  for(int n=0;n<p.boneCount;++n)out.columns[n]=bones[n].column;
  out.added.assign(bones.begin()+p.boneCount,bones.end());
  for(const auto &mesh:meshes)selected.push_back(StripSkinRegion(mesh,owned,std::set<int>(singles.begin(),singles.end()),p));
  out.view.separatedWidth=true;
  out.densityReport="auto-native-strip-width ribbons=2 columnsPerRibbon=5 addedWidthBones=48 sourceFixedSkinRetained=1 otherPanelSkinRetained=1 bodyCapsuleWrites=0";
}
}
