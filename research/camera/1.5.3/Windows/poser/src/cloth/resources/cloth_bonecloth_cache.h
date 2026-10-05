#pragma once
#include "../bonecloth/cloth_bonecloth_short_policy.h"
#include "../bonecloth/cloth_bonecloth_profile.h"
#include "../bonecloth/cloth_bonecloth_coat_source.h"
#include "../bonecloth/cloth_bonecloth_body_source.h"
#include "../bonecloth/cloth_bonecloth_graph_order.h"
#include <algorithm>
#include <cmath>
#include <cstdint>
#include <cstring>
#include <deque>
#include <memory>
#include <set>
#include <stdexcept>
#include <string>
#include <vector>

namespace eiem_cloth_cache {
constexpr size_t MaxBytes=16*1024*1024;
inline uint64_t Hash(const unsigned char *p,size_t n) {
  uint64_t h=14695981039346656037ull;for(size_t i=0;i<n;++i) h=(h^p[i])*1099511628211ull;return h;
}
struct Profile {
  ClothBoneProfile view{};
  std::deque<std::string> strings;
  std::vector<ClothBoneAsset> bones;
  std::vector<int> roots,originalRoots;
  std::vector<int> candidateAttributes;
  std::vector<int> releasedFixed;
  std::vector<int> inputAnchors;
  std::vector<int> sourceBranches;
  std::vector<ClothBoneRendererAsset> renderers;
  std::vector<std::vector<ClothBoneBinding>> bindings;
  std::vector<ClothBoneColliderAsset> colliders;
  std::vector<ClothBoneColliderGeometry> unownedGeometry;
  std::vector<const char*> dependencies;
  std::vector<const char*> nativeProducers;
  std::vector<ClothBoneColliderSource> sources;
  std::vector<int> ignored,foreignIgnored;
  std::vector<ClothBoneOwnership> ownership;
  std::vector<ClothBoneColliderAsset> originalExcluded,excludedBranches,prebuildOmitted;
  std::vector<ClothBoneNativeGraph> graphs;
  std::vector<std::vector<std::array<int,3>>> faces;
  std::vector<std::vector<std::array<int,2>>> lines;
  eiem_cloth_graph::OrderContract graphOrder;
  void Link() {
    view.bones=bones.data();view.boneCount=int(bones.size());
    view.candidateAttributes=candidateAttributes.empty()?nullptr:candidateAttributes.data();
    view.releasedFixed=releasedFixed.empty()?nullptr:releasedFixed.data();view.releasedFixedCount=int(releasedFixed.size());
    view.roots=roots.data();view.originalRoots=originalRoots.data();view.rootCount=int(roots.size());
    view.inputAnchors=inputAnchors.data();view.inputAnchorCount=int(inputAnchors.size());
    view.sourceBranches=sourceBranches.data();view.sourceBranchCount=int(sourceBranches.size());
    view.renderers=renderers.data();view.rendererCount=int(renderers.size());
    for(size_t n=0;n<renderers.size();++n) {renderers[n].bones=bindings[n].data();renderers[n].boneCount=int(bindings[n].size());}
    view.colliders=colliders.data();view.colliderCount=int(colliders.size());
    view.unownedGeometry=unownedGeometry.empty()?nullptr:unownedGeometry.data();
    view.dependencies=dependencies.data();view.dependencyCount=int(dependencies.size());
    view.nativeProducers=nativeProducers.data();view.nativeProducerCount=int(nativeProducers.size());
    view.bodyColliderSources=sources.data();view.bodyColliderSourceCount=int(sources.size());
    view.candidateIgnored=ignored.data();view.candidateIgnoredCount=int(ignored.size());
    view.foreignIgnored=foreignIgnored.data();view.foreignIgnoredCount=int(foreignIgnored.size());view.ownership=ownership.data();view.ownershipCount=int(ownership.size());
    view.prebuildOmitted=prebuildOmitted.data();view.prebuildOmittedCount=int(prebuildOmitted.size());
    view.originalExcluded=originalExcluded.data();view.originalExcludedCount=int(originalExcluded.size());
    view.excludedBranches=excludedBranches.data();view.excludedBranchCount=int(excludedBranches.size());
    for(size_t n=0;n<graphs.size();++n) graphs[n]={faces[n].data(),int(faces[n].size()),lines[n].data(),int(lines[n].size())};
    view.nativeGraphs=graphs.data();view.nativeGraphCount=int(graphs.size());
    view.nativeGraphOrder=graphOrder.source.empty()?nullptr:&graphOrder;
  }
};
inline void Require(bool ok) { if(!ok) throw std::runtime_error("invalid cloth recipe"); }
struct Reader {
  const unsigned char *p;size_t remaining;
  void Take(void *out,size_t n) {Require(n<=remaining);memcpy(out,p,n);p+=n;remaining-=n;}
  int Int(int low,int high) {int n=0;Take(&n,4);Require(n>=low&&n<=high);return n;}
  float Float() {float f=0;Take(&f,4);Require(std::isfinite(f));return f;}
  const char *Text(Profile &s,bool hex=false) {
    const int n=Int(1,127);Require(size_t(n)<=remaining);
    std::string v(reinterpret_cast<const char*>(p),n);p+=n;remaining-=n;
    Require(v.find('\0')==std::string::npos);
    if(hex) Require(n==64&&v.find_first_not_of("0123456789abcdef")==std::string::npos);
    s.strings.push_back(std::move(v));return s.strings.back().c_str();
  }
};
inline void Validate(const Profile &s) {
  auto p=s.view;std::vector<ClothBoneAsset> candidate;
  for(const auto &c:s.colliders)Require(!c.parentIsAnimator||p.runtimeGenerated);
  Require(p.runtimeSeparatedCoat?(eiem_cloth_asset::SourceSeparatedCoat(p)&&p.releasedFixed==s.releasedFixed.data()&&
      p.releasedFixedCount==int(s.releasedFixed.size())):(s.releasedFixed.empty()&&!p.releasedFixedCount&&!p.releasedFixed));
  Require(s.candidateAttributes.empty()?p.candidateAttributes==nullptr:
      (s.candidateAttributes.size()==s.bones.size()&&p.candidateAttributes==s.candidateAttributes.data()&&eiem_cloth_asset::SourceInactiveCoat(p)));
  if(p.candidateAttributes){candidate=s.bones;for(size_t n=0;n<candidate.size();++n)candidate[n].attribute=s.candidateAttributes[n];p.bones=candidate.data();p.candidateAttributes=nullptr;}
  Require(p.legRequiredMask<=15&&p.legListedMask<=15);
  Require(!p.runtimeUnowned||(p.runtimeGenerated&&p.loop&&p.rootCount>=4&&p.rootCount<=16&&p.depth>=3&&p.depth<=8&&
      !p.generatedLocal&&!p.inputAnchorCount&&!p.sourceBranchCount&&!p.runtimeFixedForks&&!p.prebuildId&&
      p.unownedRadius>=.001f&&p.unownedRadius<=.01f&&s.unownedGeometry.size()==s.colliders.size()&&s.colliders.size()==2));
  Require(!p.runtimeBodyOnly||(p.runtimeGenerated&&!p.loop&&p.rootCount==10&&p.boneCount==44&&p.depth==4&&
      p.prebuildId&&!strcmp(p.prebuildId,"0226bd67")&&eiem_cloth_asset::SourceBodyContact(p.prefabSha,p.component)&&
      !p.runtimeFixedForks&&!p.runtimeForkCoat&&!p.candidateAttributes&&!p.inputAnchorCount&&!p.sourceBranchCount&&
      !p.candidateIgnoredCount&&!p.ownershipCount&&p.nativeGraphCount==1));
  const bool ribbon=p.ribbonSource&&p.runtimeGenerated&&!p.loop&&p.rootCount==1&&p.boneCount==9&&p.depth==5&&p.prefabSha&&p.component&&
      !strcmp(p.prefabSha,"d911c12a646d6f4c3f5068b63768c38b6a6a13c6fa2d58710c2b42ead69beda4")&&!strcmp(p.component,"MC_Seraph_Skirt_Ribbon");
  Require(!p.ribbonSource||ribbon);Require((p.rootCount>=2||ribbon) && (!p.loop||p.rootCount>=3) && p.EffectiveCount()<=128 && p.EffectiveCount()<=p.boneCount);
  Require(s.inputAnchors.size()<=16&&p.inputAnchorCount==int(s.inputAnchors.size())&&(s.inputAnchors.empty()||p.runtimeGenerated));
  Require(p.sourceBranchCount==int(s.sourceBranches.size())&&(s.sourceBranches.empty()||eiem_cloth_asset::SourceShortSides(p)));
  Require(s.roots.size()==size_t(p.rootCount)&&s.originalRoots.size()==s.roots.size()&&s.bones.size()==size_t(p.boneCount));
  for(int n:s.roots)Require(n>=0&&n<p.boneCount);for(int n:s.originalRoots)Require(n>=0&&n<p.boneCount);
  std::set<int> anchors;for(int n:s.inputAnchors)Require(n>=0&&n<p.boneCount&&anchors.insert(n).second&&p.bones[n].attribute==1&&p.bones[n].parent<0&&p.bones[n].depth==0);
  Require(!p.runtimeFixedForks||(p.runtimeGenerated&&p.nativeGraphCount>0&&!p.generatedLocal&&!p.inputAnchorCount&&!p.sourceBranchCount));
  Require(bool(p.nativeGraphOrder)==p.runtimeFixedForks);
  Require(!p.runtimeForkCoat||(p.runtimeFixedForks&&p.runtimeGenerated&&!p.loop&&!p.generatedLocal));
  if(p.nativeGraphOrder)Require(p.nativeGraphOrder==&s.graphOrder&&eiem_cloth_graph::Valid(s.graphOrder));
  std::set<std::string> names;std::set<int> roots,old;std::set<std::pair<int,int>> labels;int activeCount=0,forks=0;
  for(int n=0;n<p.boneCount;++n) {
    const auto &b=p.bones[n];Require(names.insert(p.runtimeGenerated?std::string(b.name)+"\n"+b.parentName:b.name).second&&b.parent<n);
    const float q=b.rotation.x*b.rotation.x+b.rotation.y*b.rotation.y+b.rotation.z*b.rotation.z+b.rotation.w*b.rotation.w;
    Require(std::fabs(q-1.f)<.001f&&b.scale.x>0&&b.scale.y>0&&b.scale.z>0);
    if(b.parent>=0) Require(!strcmp(b.parentName,p.bones[b.parent].name));
    if(p.Foreign(n)) {
      Require(p.runtimeGenerated&&p.nativeGraphCount&&p.Passive(n)&&b.parent>=0&&b.attribute&&b.column==-1&&b.depth==-1);
      int producers=0;for(const auto &proof:s.ownership)producers+=proof.bone==n&&proof.attribute==2;Require(producers==1);
      for(int k=0;k<p.boneCount;++k)if(p.bones[k].parent==n)Require(p.Foreign(k));
    }
    else if(p.Passive(n)) {
      Require(p.nativeGraphCount&&b.parent>=0&&b.attribute&&b.attribute==p.bones[b.parent].attribute&&b.column==-1&&b.depth==-1);
      if(b.attribute==1)Require(p.runtimeGenerated&&std::fabs(b.position.x)<1e-7f&&std::fabs(b.position.y)<1e-7f&&std::fabs(b.position.z)<1e-7f);
      if(!p.runtimeGenerated)Require(std::fabs(b.position.x)<1e-7f&&std::fabs(b.position.y)<1e-7f&&std::fabs(b.position.z)<1e-7f);
      for(int k=0;k<p.boneCount;++k)Require(p.bones[k].parent!=n);
    }
    else if(b.attribute==0) Require(b.column==-1&&b.depth==-1&&(p.ExcludedBranch(n)||p.CandidateAncestor(n)));
    else {
      Require(b.column>=0&&b.column<p.rootCount&&b.depth>=0&&b.depth<p.depth);
      const bool unique=labels.insert({b.column,b.depth}).second;++activeCount;
      Require((unique||p.runtimeFixedForks)&&(b.attribute==(b.depth?2:1)||
          (p.runtimeGenerated&&b.attribute==1&&b.depth>0&&b.parent>=0&&p.bones[b.parent].attribute==1)));
      if(b.depth) Require(b.parent>=0&&p.bones[b.parent].column==b.column&&p.bones[b.parent].depth==b.depth-1);
      else Require(p.roots[b.column]==n&&(b.parent==-1||p.CandidateAncestor(b.parent)));
    }
  }
  Require(activeCount==p.EffectiveCount());
  for(int n=0;n<p.boneCount;++n)if(p.bones[n].attribute&&!p.Passive(n)){
    std::vector<int> children;for(int k=n+1;k<p.boneCount;++k)if(p.bones[k].parent==n&&p.bones[k].attribute&&!p.Passive(k))children.push_back(k);
    if(children.size()>1){Require(p.runtimeFixedForks&&children.size()==2&&p.bones[n].attribute==1&&p.bones[children[0]].attribute==2&&p.bones[children[1]].attribute==2);++forks;}
  }
  Require(p.runtimeFixedForks==(forks>0));
  for(int n=0;n<p.rootCount;++n) {Require(roots.insert(p.roots[n]).second&&old.insert(p.originalRoots[n]).second);}
  std::set<int> origins;
  for(int root:roots) {
    int b=root;
    while(p.bones[b].parent>=0) {b=p.bones[b].parent;Require(p.bones[b].attribute==0);}
    if(p.InputAnchor(b)) {
      int child=-1;for(int n:old)if(p.bones[n].parent==b){Require(child<0);child=n;}
      Require(child>=0&&p.bones[child].attribute==1&&origins.insert(child).second);
    } else Require(old.count(b)&&origins.insert(b).second);
  }
  Require(origins==old);
  for(int n=0;n<p.boneCount;++n)if(p.bones[n].parent==-1)Require(old.count(n)!=0||p.OutsideOriginal(n));
  if(roots!=old)Require(p.nativeGraphCount>0);
  std::set<int> ignored;
  for(int n:s.ignored)Require(n>=0&&n<p.boneCount&&ignored.insert(n).second);
  std::set<int> foreign;for(int n:s.foreignIgnored)Require(n>=0&&n<p.boneCount&&foreign.insert(n).second&&p.Passive(n));
  Require(s.ownership.size()<=1024);std::set<std::pair<std::string,int>> proofs;
  for(const auto &v:s.ownership){Require(p.runtimeGenerated&&v.component&&strcmp(v.component,p.component)&&v.bone>=0&&v.bone<p.boneCount&&v.attribute>=-1&&v.attribute<=2&&proofs.insert({v.component,v.bone}).second);
    Require(!v.zeroRotationAnchor||(p.runtimeGenerated&&p.bones[v.bone].attribute==2&&v.attribute==1));
    Require(p.Passive(v.bone)||p.bones[v.bone].attribute!=2||v.attribute<=0||v.zeroRotationAnchor);}
  Require(s.prebuildOmitted.size()<=16&&p.prebuildOmittedCount==int(s.prebuildOmitted.size()));
  std::set<std::pair<std::string,std::string>> omitted;
  for(const auto &b:s.prebuildOmitted){Require(p.runtimeGenerated&&p.prebuildId&&b.name&&b.parent&&omitted.insert({b.name,b.parent}).second);
    bool parent=false;for(const auto &a:s.bones){Require(strcmp(a.name,b.name)||strcmp(a.parentName,b.parent));parent|=!strcmp(a.name,b.parent);}Require(parent);}
  if(!s.excludedBranches.empty()) {
    Require(p.runtimeGenerated&&!s.originalExcluded.empty()&&s.originalExcluded.size()<=128&&s.excludedBranches.size()<=256);
    std::set<std::pair<std::string,std::string>> closure,declared,active;
    for(const auto &b:s.bones)active.insert({b.name,b.parentName});
    for(const auto &b:s.excludedBranches){Require(b.name&&b.parent&&*b.name&&*b.parent);const auto key=std::make_pair(std::string(b.name),std::string(b.parent));Require(closure.insert(key).second&&!active.count(key));}
    for(const auto &b:s.originalExcluded){Require(b.name&&b.parent);const auto key=std::make_pair(std::string(b.name),std::string(b.parent));Require(closure.count(key)&&declared.insert(key).second);}
  }
  else if(p.runtimeGenerated)Require(s.originalExcluded.empty());
  if(p.nativeGraphCount) {
    Require(activeCount==p.EffectiveCount());
    for(const auto &g:s.graphs) {
      Require((g.faceCount>0||(ribbon&&g.faceCount==0&&g.lineCount==4)||(p.runtimeBodyOnly&&g.faceCount==0&&g.lineCount==25))&&g.faceCount<=256&&g.lineCount<=128);
      std::set<std::array<int,3>> faces;std::set<std::array<int,2>> edges,lines;
      for(int n=0;n<g.faceCount;++n) {
        auto f=g.faces[n];std::sort(f.begin(),f.end());
        Require(f[0]>=0&&f[2]<p.boneCount&&f[0]<f[1]&&f[1]<f[2]&&faces.insert(f).second);
        for(int b:f)Require(p.bones[b].attribute&&!p.Passive(b));
        std::set<int> columns;int minDepth=p.depth,maxDepth=-1;
        for(int b:f){columns.insert(p.bones[b].column);minDepth=(std::min)(minDepth,p.bones[b].depth);maxDepth=(std::max)(maxDepth,p.bones[b].depth);}
        const int lo=*columns.begin(),hi=*columns.rbegin();
        if(p.runtimeFixedForks){
          auto adjacent=[&](int a,int b){const auto &x=p.bones[a],&y=p.bones[b];const int d=std::abs(x.column-y.column);
            return a!=b&&(x.parent==b||y.parent==a||(x.depth==y.depth&&(d<=1||(p.loop&&d==p.rootCount-1))));};
          bool fan=false,parent=false;for(int k=0;k<3;++k){fan|=adjacent(f[k],f[(k+1)%3])&&adjacent(f[k],f[(k+2)%3]);parent|=p.bones[f[k]].parent==f[(k+1)%3]||p.bones[f[(k+1)%3]].parent==f[k];}
          Require(columns.size()<=2&&maxDepth-minDepth==1&&fan&&parent);
          Require(lo==hi||hi-lo==1||(p.loop&&lo==0&&hi==p.rootCount-1));
        }else {Require(columns.size()==2&&maxDepth-minDepth==1);Require(hi-lo==1||(p.loop&&lo==0&&hi==p.rootCount-1));}
        for(int k=0;k<3;++k) {std::array<int,2> e{f[k],f[(k+1)%3]};std::sort(e.begin(),e.end());edges.insert(e);}
      }
      for(int n=0;n<g.lineCount;++n) {
        auto e=g.lines[n];std::sort(e.begin(),e.end());
        Require(e[0]>=0&&e[1]<p.boneCount&&e[0]<e[1]&&(p.bones[e[1]].parent==e[0]||(p.runtimeFixedForks&&p.bones[e[0]].depth==p.bones[e[1]].depth&&
            (std::abs(p.bones[e[0]].column-p.bones[e[1]].column)<=1||(p.loop&&std::abs(p.bones[e[0]].column-p.bones[e[1]].column)==p.rootCount-1))))&&!p.Passive(e[1])&&
            !p.Passive(e[0])&&p.bones[e[0]].attribute&&p.bones[e[1]].attribute&&!edges.count(e)&&lines.insert(e).second);edges.insert(e);
      }
      for(int n=0;n<p.boneCount;++n)if(p.bones[n].depth>0&&!p.Passive(n)&&p.bones[n].attribute)
        Require(edges.count({p.bones[n].parent,n})!=0);
      if(p.nativeGraphOrder)Require(eiem_cloth_graph::Match(*p.nativeGraphOrder,
          std::vector<std::array<int,3>>(faces.begin(),faces.end()),std::vector<std::array<int,2>>(lines.begin(),lines.end())));
    }
  } else Require(s.ignored.empty());
  names.clear();for(const auto &r:s.renderers) {
    const std::string key=std::string(r.name)+"\n"+(r.parent?r.parent:"");
    Require(names.insert(key).second);std::set<int> ids;std::set<std::string> boneNames;
    for(const auto &other:s.renderers)if(&other!=&r&&!strcmp(other.name,r.name))Require(other.parent&&r.parent);
    for(int n=0;n<r.boneCount;++n) {
      const auto &b=r.bones[n];Require(boneNames.insert(p.runtimeGenerated?std::string(b.name)+"\n"+b.parent:b.name).second);
      if(b.cloth>=0) Require(ids.insert(b.cloth).second&&!strcmp(b.name,p.bones[b.cloth].name)&&!strcmp(b.parent,p.bones[b.cloth].parentName));
      Require(b.cloth<0||!p.Passive(b.cloth)||p.Foreign(b.cloth));
    }
  }
  names.clear();for(auto d:s.dependencies) Require(strcmp(d,p.component)&&names.insert(d).second);
  for(auto c:s.sources) Require(names.count(c.component)!=0);
  for(auto d:s.nativeProducers) Require(strcmp(d,p.component)&&names.insert(d).second);
}
struct Catalog {
  struct Source {std::string path,hash;bool present=false;};
  std::vector<Source> sources;
  std::vector<std::unique_ptr<Profile>> owned;
  std::vector<const ClothBoneProfile*> profiles;
  std::string key;
  bool Parse(const unsigned char *bytes,size_t size) {
    try {
      Require(bytes&&size>=20&&size<=MaxBytes+20);
      const bool native=!memcmp(bytes,"EIEMBC05",8);
      const bool qualified=native||!memcmp(bytes,"EIEMBC04",8);
      const bool extended=qualified||!memcmp(bytes,"EIEMBC03",8);
      Require(extended||!memcmp(bytes,"EIEMBC02",8));
      uint32_t length=0;uint64_t sum=0;memcpy(&length,bytes+8,4);memcpy(&sum,bytes+12,8);
      Require(length==size-20&&Hash(bytes+20,length)==sum);
      Catalog next;Reader r{bytes+20,length};Profile header;next.key=r.Text(header,true);
      const int files=r.Int(1,16);std::set<std::string> paths;
      for(int n=0;n<files;++n) {
        Source source;source.path=r.Text(header);source.present=r.Int(0,1)!=0;source.hash=r.Text(header,source.present);
        Require(source.path.find_first_not_of("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_/.-")==std::string::npos&&
            source.path[0]!='/'&&source.path.find("..") == std::string::npos&&source.path.size()>4&&
            source.path.substr(source.path.size()-4)==".blc"&&paths.insert(source.path).second);
        Require(source.present||source.hash=="absent");next.sources.push_back(source);
      }
      const int count=r.Int(0,512);std::set<std::string> signatures;
      for(int i=0;i<count;++i) {
        auto item=std::make_unique<Profile>();auto &s=*item;auto &p=s.view;
        p.component=r.Text(s);p.signature=r.Text(s,true);p.prefabSha=r.Text(s,true);Require(signatures.insert(p.signature).second);
        p.loop=r.Int(0,1)!=0;p.depth=r.Int(2,32);const int bones=r.Int(1,128);
        for(int n=0;n<bones;++n) {
          ClothBoneAsset b{};b.name=r.Text(s);b.parentName=r.Text(s);b.parent=r.Int(-1,n-1);b.attribute=r.Int(0,2);
          b.column=r.Int(-1,31);b.depth=r.Int(-1,31);
          b.position={r.Float(),r.Float(),r.Float()};b.rotation={r.Float(),r.Float(),r.Float(),r.Float()};b.scale={r.Float(),r.Float(),r.Float()};s.bones.push_back(b);
        }
        const int roots=r.Int(2,16);for(int n=0;n<roots;++n) s.roots.push_back(r.Int(0,bones-1));
        for(int n=0;n<roots;++n) s.originalRoots.push_back(r.Int(0,bones-1));
        const int renderers=r.Int(1,64);
        for(int n=0;n<renderers;++n) {
          ClothBoneRendererAsset v{};v.name=r.Text(s);v.mesh=r.Text(s);v.root=r.Text(s);
          if(qualified&&r.Int(0,1))v.parent=r.Text(s);
          v.vertices=r.Int(1,1000000);v.submeshes=r.Int(1,64);
          const int bindings=r.Int(1,256);s.bindings.emplace_back();
          for(int k=0;k<bindings;++k) {
            ClothBoneBinding b{};b.name=r.Text(s);b.parent=r.Text(s);b.cloth=r.Int(-1,bones-1);for(auto &f:b.bind)f=r.Float();s.bindings.back().push_back(b);
          }s.renderers.push_back(v);
        }
        const int colliders=r.Int(0,128);for(int n=0;n<colliders;++n) s.colliders.push_back({r.Text(s),r.Text(s)});
        const int deps=r.Int(0,7);for(int n=0;n<deps;++n)s.dependencies.push_back(r.Text(s));
        const int colliderSources=r.Int(0,128);for(int n=0;n<colliderSources;++n)s.sources.push_back({r.Text(s),r.Int(0,127)});
        if(extended) {
          const int ignored=r.Int(0,16);for(int n=0;n<ignored;++n)s.ignored.push_back(r.Int(0,bones-1));
          const int variants=r.Int(0,8);s.graphs.resize(variants);
          for(int n=0;n<variants;++n) {
            s.faces.emplace_back();s.lines.emplace_back();
            const int faces=r.Int(1,256);for(int k=0;k<faces;++k)s.faces.back().push_back({r.Int(0,bones-1),r.Int(0,bones-1),r.Int(0,bones-1)});
            const int lines=r.Int(0,128);for(int k=0;k<lines;++k)s.lines.back().push_back({r.Int(0,bones-1),r.Int(0,bones-1)});
          }
        }
        if(native) {const int producers=r.Int(0,7);for(int n=0;n<producers;++n)s.nativeProducers.push_back(r.Text(s));}
        s.Link();Validate(s);next.profiles.push_back(&p);next.owned.push_back(std::move(item));
      }
      Require(r.remaining==0);*this=std::move(next);return true;
    } catch(const std::exception &) {return false;}
  }
};
}
