#pragma once
#include "../bonecloth/cloth_bonecloth_limits.h"
#include <array>
#include <vector>
#include <cstdint>
#include <cstring>

namespace eiem_cloth_display {
struct Array { uintptr_t data=0;int length=0,allocator=0; };
inline bool Same(Array a,Array b) {return a.data==b.data&&a.length==b.length&&a.allocator==b.allocator;}
struct Job {
  float delta=0;uint32_t padding=0;
  Array teams,ids,old,oldPosition,display,positions,velocity,oldRotation,attributes,rotations,roots;
  int count=0;uint32_t tail=0;
};
static_assert(sizeof(Array)==16&&sizeof(Job)==192&&offsetof(Job,roots)==168&&offsetof(Job,count)==184,"display input ABI");
struct Policy {
  uint64_t session=0,generation=0,command=0,process=0,data=0;
  int team=0,particleStart=0,proxyStart=0,count=0;
  uint8_t move=0,fixed=0;
  Array teams,ids,roots,attributes;
  std::array<int,ClothBoneMaxParticles> originalRoots{};
  std::array<uint8_t,ClothBoneMaxParticles> originalAttributes{};
};
inline bool Range(Array a,int start,int count) {
  return a.data&&a.length>0&&a.length<=65536&&start>=0&&count>0&&start<=a.length&&count<=a.length-start;
}
inline bool Valid(const Policy &p) {
  if(!p.session||!p.generation||!p.command||!p.process||!p.data||p.team<=0||p.team>INT16_MAX||
      !p.move||!p.fixed||(p.move&p.fixed)||p.count<1||p.count>ClothBoneMaxParticles||
      !Range(p.teams,p.team,1)||!Range(p.ids,p.particleStart,p.count)||
      !Range(p.roots,p.proxyStart,p.count)||!Range(p.attributes,p.proxyStart,p.count))return false;
  int moves=0;
  for(int n=0;n<p.count;++n) {
    const auto a=p.originalAttributes[n];const int r=p.originalRoots[n];
    if(bool(a&p.move)==bool(a&p.fixed)||r<-1||r>=p.count)return false;
    if(a&p.move) {
      if(r<0||!(p.originalAttributes[r]&p.fixed))return false;
      ++moves;
    }
  }
  return moves>0&&moves<p.count;
}
enum class Result { Inactive, Repeated, Mismatch, Bound };
class View {
  bool ready_=false,submitted_=false;
  Policy policy_{};
  std::vector<int> storage_;
public:
  void Revoke(){ready_=false;}
  void CompletedBoundary(){ready_=false;submitted_=false;}
  bool Publish(const Policy &p) {
    ready_=false;if(submitted_||!Valid(p))return false;
    policy_=p;ready_=true;return true;
  }
  const Policy &Identity()const{return policy_;}
  bool Ready()const{return ready_;}
  Result Bind(Job &job,int count,int &changed,const Array *sourceRoots=nullptr) {
    changed=0;if(!ready_)return Result::Inactive;
    if(submitted_)return Result::Repeated;
    const auto &p=policy_;
    if(count!=job.count||count<1||count>job.ids.length||p.particleStart>count||p.count>count-p.particleStart||
        !Same(job.teams,p.teams)||!Same(job.ids,p.ids)||!Same(sourceRoots?*sourceRoots:job.roots,p.roots)||!job.roots.data||job.roots.length!=p.roots.length||job.roots.allocator!=p.roots.allocator||!Same(job.attributes,p.attributes))return Result::Mismatch;
    const auto roots=reinterpret_cast<const int*>(p.roots.data);
    const auto ids=reinterpret_cast<const int16_t*>(p.ids.data);
    const auto attrs=reinterpret_cast<const uint8_t*>(p.attributes.data);
    for(int n=0;n<p.count;++n)
      if(ids[p.particleStart+n]!=p.team||roots[p.proxyStart+n]!=p.originalRoots[n]||
          attrs[p.proxyStart+n]!=p.originalAttributes[n])return Result::Mismatch;
    // The host supplies the original producer identity when composing actors.
    // Keep prior actors' private changes; never mutate the game's source array.
    const auto input=reinterpret_cast<const int*>(job.roots.data);
    storage_.assign(input,input+p.roots.length);
    for(int n=0;n<p.count;++n)if(p.originalAttributes[n]&p.move) {
      storage_[p.proxyStart+n]=-1;++changed;
    }
    job.roots.data=reinterpret_cast<uintptr_t>(storage_.data());
    submitted_=true;return Result::Bound;
  }
};
}
