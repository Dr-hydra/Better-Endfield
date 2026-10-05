#pragma once
#include "math/mmd_retarget.h"
#include <nlohmann/json.hpp>

namespace mmd {
inline RetargetProfile ReadCalibration(const nlohmann::json &j) {
  if(j.value("version",0)!=3||!j.at("bones").is_array()||j.at("bones").size()>4096)
    throw std::runtime_error("Invalid body calibration");
  RetargetProfile p;p.model=j.at("model");p.fingerprint=j.at("fingerprint");
  for(const auto &v:j.at("bones")) {
    TargetBone b;b.name=v.at("name");b.parent=v.at("parent");b.role=v.at("role");
    if(b.name.size()>512||b.role < -1||b.role>=55)throw std::runtime_error("Invalid calibrated bone");
    auto a=v.at("pos"),q=v.at("rot"),s=v.at("scale");
    b.localPos={a.at(0),a.at(1),a.at(2)};b.localRot={q.at(0),q.at(1),q.at(2),q.at(3)};
    b.localScale={s.at(0),s.at(1),s.at(2)};b.calibrated=v.value("calibrated",false);
    for(float f:{b.localPos.x,b.localPos.y,b.localPos.z,b.localRot.x,b.localRot.y,b.localRot.z,b.localRot.w,
                 b.localScale.x,b.localScale.y,b.localScale.z})
      if(!std::isfinite(f))throw std::runtime_error("Non-finite calibration");
    b.localRot=NormQ(b.localRot);p.bones.push_back(b);
  }
  p.globals();return p;
}
inline bool BodyCalibrationRole(int role) {return role>=0&&role!=21&&role!=22&&role!=23;}
// Identify the body by named ancestor paths, independent of traversal indices
// or weapon/effect/LOD nodes. The full live profile stays indexed to its rig.
inline bool RestoreBodyCalibration(const RetargetProfile &saved,RetargetProfile &live) {
  if(saved.model!=live.model||!saved.valid()||live.bones.empty())return false;
  if(saved.fingerprint!=live.fingerprint&&live.fingerprint.find("hierarchy1-")!=0&&saved.bones.size()==live.bones.size()) {
    bool sameLayout=true;
    for(size_t i=0;i<saved.bones.size();++i) {
      const auto &a=saved.bones[i],&b=live.bones[i];
      if(a.name!=b.name||a.parent!=b.parent||a.role!=b.role){sameLayout=false;break;}
    }
    // With an unchanged hierarchy, a different old fingerprint means the
    // bindposes changed. Do not treat replacement skin data as a prop change.
    if(sameLayout)return false;
  }
  auto layout=[](const RetargetProfile &p,std::vector<std::string> &paths,std::map<std::string,int> &body) {
    paths.resize(p.bones.size());std::vector<bool> needed(p.bones.size());
    for(size_t i=0;i<p.bones.size();++i) {
      const auto &b=p.bones[i];if(b.parent>=int(i)||b.parent < -1)return false;
      // Length prefixes distinguish arbitrary bone names containing slashes.
      paths[i]=(b.parent<0?std::string{}:paths[b.parent])+std::to_string(b.name.size())+":"+b.name;
      if(paths[i].size()>32768)return false;
      if(BodyCalibrationRole(b.role))for(int at=int(i);at>=0;at=p.bones[at].parent)needed[at]=true;
    }
    for(int i=0;i<int(needed.size());++i)if(needed[i]&&!body.emplace(paths[i],i).second)return false;
    return true;
  };
  std::vector<std::string> oldPaths,newPaths;std::map<std::string,int> oldBody,newBody;
  if(!layout(saved,oldPaths,oldBody)||!layout(live,newPaths,newBody)||oldBody.size()!=newBody.size())return false;
  auto copy=live;
  for(const auto &entry:newBody) {
    auto found=oldBody.find(entry.first);if(found==oldBody.end())return false;
    const auto &from=saved.bones[found->second];auto &to=copy.bones[entry.second];
    if(from.role!=to.role||!from.calibrated||Len(from.localScale-to.localScale)>1e-5f)return false;
    // Hips/root translation is animated; limb offsets and intermediate bones
    // below the hips still validate proportions and reject changed skeletons.
    bool belowHips=false;
    for(int at=to.parent;at>=0;at=copy.bones[at].parent)if(copy.bones[at].role==0){belowHips=true;break;}
    if(belowHips&&Len(from.localPos-to.localPos)>(std::max)(1e-4f,Len(from.localPos)*.001f))return false;
    to.localPos=from.localPos;to.localRot=from.localRot;to.calibrated=true;
  }
  // Eye/jaw scale and translation may be animated by SMC, so they must not
  // invalidate a body T pose. Reuse their reference only when paths match.
  for(int role:{21,22,23}) {
    int a=saved.roles[role],b=live.roles[role];
    if(a>=0&&b>=0&&oldPaths[a]==newPaths[b]) {
      copy.bones[b].localPos=saved.bones[a].localPos;copy.bones[b].localRot=saved.bones[a].localRot;
      copy.bones[b].calibrated=saved.bones[a].calibrated;
    }
  }
  copy.globals();if(!copy.valid())return false;live=std::move(copy);return true;
}
}
