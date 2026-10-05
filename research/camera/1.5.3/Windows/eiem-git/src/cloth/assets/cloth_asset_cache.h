#pragma once
#include "cloth_asset_recipe.h"

namespace eiem_cloth_asset {
constexpr const char *AutoAlgorithm="runtime-effective-graph-v3-dense-regions-v2-selection-v2-ownership-v1-waist-v2-bind-domain-v1-fixed-depth-v1-panel-fit-v3-long-skin-envelope-calf-knee-contour-v14-ribbon-width-v1-separated-panels-lines-v2-short-native-v1-layer-calf-short-sides-clearance-v4-fixed-apron-ordered-volumes-v3-prebuild-v1-bundle-v2-reader-v1-cell-aspect-v1-source-decode-v2-source-waist-panel-point-v1-leg-regions-native-lines-prebuild-closure-v1-isolated-strip-width-v1-fixed-fork-coat-v7-waist-field-prebuild-closure-v2-body-contact-v1-unowned-waist-v1-Animator-body-scope-v1-separated-panels-six-v1-collider-Animator-parent-v1-separated-coat-inputs-point-flexible-v1-owner-renderer-scope-v1";
inline std::string QueryKey(const Query &q,const std::wstring &root,
    const std::vector<std::pair<std::wstring,std::string>> &indices) {
  Bytes bytes;
  auto number=[&](uint64_t n){for(int i=0;i<8;++i)bytes.push_back(uint8_t(n>>(8*i)));};
  auto raw=[&](const void *p,size_t n){number(n);const auto b=static_cast<const uint8_t*>(p);bytes.insert(bytes.end(),b,b+n);};
  auto text=[&](const std::string &s){raw(s.data(),s.size());};
  auto key=[&](const BoneKey &b){text(b.name);text(b.parent);};
  text(AutoAlgorithm);raw(root.data(),root.size()*sizeof(wchar_t));text(q.modelPath);
  number(q.hipsPath.size());for(const auto &part:q.hipsPath)text(part);
  number(indices.size());for(const auto &i:indices){raw(i.first.data(),i.first.size()*sizeof(wchar_t));text(i.second);}
  key(q.hips);key(q.spine);key(q.leftThigh);key(q.rightThigh);key(q.chest);key(q.upperChest);key(q.leftCalf);key(q.rightCalf);key(q.leftFoot);key(q.rightFoot);for(int d:q.capsuleDirections)number(uint64_t(d));
  number(q.cloths.size());for(const auto &c:q.cloths){text(c.name);number(c.roots.size());for(const auto &r:c.roots)key(r);}
  number(q.renderers.size());for(const auto &r:q.renderers){text(r.name);text(r.parent);text(r.mesh);text(r.root);number(r.vertices);number(r.submeshes);number(r.bones.size());
    for(const auto &b:r.bones){key(b.bone);for(double d:b.bind){Need(std::isfinite(d),"auto-cache-nonfinite-binding");uint64_t bits=0;memcpy(&bits,&d,sizeof(bits));number(bits);}}}
  number(q.reservedRenderers.size());for(const auto &r:q.reservedRenderers){text(r.first);text(r.second);}
  return Digest(bytes);
}
inline size_t ResultBytes(const Generated &g){size_t n=sizeof(g)+g.liveRenderers.capacity()*sizeof(size_t)+g.key.size()+g.source.size()+g.sourceHash.size()+g.relevantHash.size();
  for(const auto &s:g.sources)n+=128+s.first.size()+s.second.size();for(const auto &r:g.reports)n+=sizeof(r)+r.component.size()+r.reason.size();
  for(const auto &p:g.profiles){n+=sizeof(*p)+p->bones.size()*sizeof(ClothBoneAsset)+p->renderers.size()*sizeof(ClothBoneRendererAsset)+p->colliders.size()*sizeof(ClothBoneColliderAsset);
    for(const auto &s:p->strings)n+=64+s.size();for(const auto &b:p->bindings)n+=64+b.size()*sizeof(ClothBoneBinding);
    n+=(p->roots.size()+p->originalRoots.size()+p->inputAnchors.size()+p->sourceBranches.size()+p->ignored.size()+p->candidateAttributes.size()+p->releasedFixed.size())*sizeof(int);
    n+=p->foreignIgnored.size()*sizeof(int)+p->ownership.size()*sizeof(ClothBoneOwnership)+p->unownedGeometry.size()*sizeof(ClothBoneColliderGeometry);
    n+=(p->originalExcluded.size()+p->excludedBranches.size()+p->prebuildOmitted.size())*sizeof(ClothBoneColliderAsset);
    for(const auto &f:p->faces)n+=f.size()*sizeof(std::array<int,3>);for(const auto &l:p->lines)n+=l.size()*sizeof(std::array<int,2>);
    n+=p->graphOrder.source.capacity()*sizeof(eiem_cloth_graph::Face)+p->graphOrder.adjacent.capacity()*sizeof(eiem_cloth_graph::Edge)+p->graphOrder.rules.capacity()*sizeof(eiem_cloth_graph::Rule);
    for(const auto &r:p->graphOrder.rules)n+=r.choices.capacity()*sizeof(eiem_cloth_graph::Choice);}
  for(const auto &r:g.dense){n+=sizeof(*r)+r->added.size()*sizeof(ClothBoneAsset)+(r->columns.size()+r->roots.size()+r->parents.size())*sizeof(int)+r->radii.size()*sizeof(float)+r->cross.size()*sizeof(std::array<int,2>);
    n+=r->bodyBindings.size()*sizeof(ClothBoneBinding)+r->bodySpheres.size()*sizeof(ClothBoneBodySphere);
    n+=r->responses.size()*sizeof(ClothBoneResponseFrame)+r->responsePoints.size()*sizeof(ClothBoneResponsePoint)+(r->responseFaces.size()+r->layerFaces.size())*sizeof(ClothBoneResponseFace);
    n+=r->densityReport.size();for(const auto &s:r->strings)n+=64+s.size();for(const auto &f:r->faces)n+=f.size()*sizeof(std::array<int,3>);for(const auto &l:r->lines)n+=l.size()*sizeof(std::array<int,2>);
    for(const auto &m:r->meshes)n+=sizeof(m)+m.payload.size()+m.binds.size()*sizeof(std::array<float,16>)+(m.bindingIds.size()+m.foreign.size())*sizeof(int)+m.samples.size()*sizeof(eiem_cloth_skin::Sample)+m.edges.size()*sizeof(eiem_cloth_skin::Edge)+m.seams.size()*sizeof(eiem_cloth_skin::Seam)+m.weights.size()*8+m.floatWeights.size()*16+m.indices.size()*4;}
  return n;
}
struct ContentCache {
  struct Entry {std::string key;std::shared_ptr<Generated> result;size_t bytes=0;};
  std::deque<Entry> entries;
  static constexpr size_t MaxEntries=4,MaxBytes=16*1024*1024;
  template<class ReadHash> std::shared_ptr<Generated> Find(const std::string &key,ReadHash readHash){
    for(auto i=entries.begin();i!=entries.end();++i)if(i->key==key){
      Entry entry=std::move(*i);entries.erase(i);
      for(const auto &s:entry.result->sources)if(readHash(s.first)!=s.second)return {};
      auto result=entry.result;entries.push_front(std::move(entry));return result;
    }return {};
  }
  void Put(std::string key,const std::shared_ptr<Generated> &result){
    if(!result||result->sources.empty()||result->key.empty())return;const auto bytes=ResultBytes(*result);if(bytes>MaxBytes)return;
    for(auto i=entries.begin();i!=entries.end();)if(i->key==key)i=entries.erase(i);else ++i;
    entries.push_front({std::move(key),result,bytes});size_t total=0;for(const auto &e:entries)total+=e.bytes;
    while(entries.size()>MaxEntries||total>MaxBytes){total-=entries.back().bytes;entries.pop_back();}
  }
};
}
