#pragma once
#include "../bonecloth/cloth_bonecloth_limits.h"
#include <array>
#include <algorithm>
#include <cstdint>
#include <cstring>
#include <atomic>
#include <windows.h>

namespace eiem_cloth_layer {
struct PointContact {
  uint32_t flags[2]{};
  uint16_t thickness=0,sign=0;
  int point=0,triangle[3]{};
  uint16_t pointMass=0,triangleMass[3]{};
};
static_assert(sizeof(PointContact)==36 && offsetof(PointContact,sign)==10 &&
    offsetof(PointContact,triangle)==16 && offsetof(PointContact,pointMass)==28,"native contact POD");
struct Face { std::array<int,3> ids{}; int outside=0; };
struct Surface {
  int team=0,start=0,count=0,faceCount=0;
  std::array<Face,ClothBoneMaxFaces> faces{};
  bool pointsOnly=false;
  int activeCount=0;
  std::array<int,ClothBoneMaxParticles> active{};
};
struct Ticket {
  uint64_t session=0,generation=0,command=0,process[2]{},data[2]{};
  bool operator==(const Ticket &b) const {
    return session==b.session && generation==b.generation && command==b.command &&
        process[0]==b.process[0] && process[1]==b.process[1] && data[0]==b.data[0] && data[1]==b.data[1];
  }
};
struct Policy { Ticket ticket{}; uintptr_t list=0; Surface inner{},outer{}; };
inline int Parity(const std::array<int,3> &ids) {
  if(ids[0]==ids[1] || ids[1]==ids[2] || ids[0]==ids[2])return 0;
  const int inversions=int(ids[0]>ids[1])+int(ids[0]>ids[2])+int(ids[1]>ids[2]);
  return inversions&1?-1:1;
}
inline bool InStorage(const Surface &s,int particle) {
  return particle>=s.start && uint64_t(particle)-uint64_t(s.start)<uint64_t(s.count);
}
inline bool Contains(const Surface &s,int particle) {
  return InStorage(s,particle) && (!s.pointsOnly ||
      std::binary_search(s.active.begin(),s.active.begin()+s.activeCount,particle));
}
inline bool Valid(const Surface &s) {
  if(s.team<=0 || s.team>0xffffff || s.start<0 || s.count<1 || s.count>ClothBoneMaxParticles ||
      s.start>INT32_MAX-s.count || s.faceCount<0 || s.faceCount>ClothBoneMaxFaces ||
      (s.pointsOnly?s.faceCount!=0:s.faceCount==0) ||
      s.activeCount<0 || s.activeCount>s.count || (s.pointsOnly?s.activeCount==0:s.activeCount!=0))return false;
  for(int n=0;n<s.activeCount;++n)
    if(!InStorage(s,s.active[n]) || (n && s.active[n-1]>=s.active[n]))return false;
  for(int n=0;n<s.faceCount;++n) {
    const auto &f=s.faces[n];
    if(f.outside!=1 && f.outside!=-1)return false;
    if(!(f.ids[0]<f.ids[1] && f.ids[1]<f.ids[2]))return false;
    for(int p:f.ids)if(!Contains(s,p))return false;
    if(n && !(s.faces[n-1].ids<f.ids))return false;
  }
  return true;
}
inline bool Valid(const Policy &p) {
  return p.ticket.session && p.ticket.generation && p.ticket.command && p.ticket.process[0] && p.ticket.process[1] &&
      p.ticket.process[0]!=p.ticket.process[1] && p.ticket.data[0] && p.ticket.data[1] && p.ticket.data[0]!=p.ticket.data[1] &&
      p.list && !(p.list&7) && Valid(p.inner) && Valid(p.outer) && p.inner.team!=p.outer.team &&
      !(p.inner.pointsOnly && p.outer.pointsOnly) &&
      (p.inner.start+p.inner.count<=p.outer.start || p.outer.start+p.outer.count<=p.inner.start);
}
enum class Result { Foreign, Invalid, UnknownFace, Kept, Changed };
inline Result Correct(const Policy &p,uintptr_t list,const PointContact &source,PointContact &copy) {
  if(!list || list!=p.list)return Result::Foreign;
  const int a=int(source.flags[0]&0xffffff),b=int(source.flags[1]&0xffffff);
  const bool pointInside=a==p.inner.team && b==p.outer.team;
  if(!pointInside && !(a==p.outer.team && b==p.inner.team))return Result::Foreign;
  const auto &point=pointInside?p.inner:p.outer,&triangle=pointInside?p.outer:p.inner;
  if(!Contains(point,source.point))return Result::Invalid;
  std::array<int,3> ids{source.triangle[0],source.triangle[1],source.triangle[2]};
  const int parity=Parity(ids);if(!parity)return Result::Invalid;
  for(int id:ids)if(!Contains(triangle,id))return Result::Invalid;
  if(source.sign!=0x3c00 && source.sign!=0xbc00)return Result::Invalid;
  for(uint16_t mass:{source.pointMass,source.triangleMass[0],source.triangleMass[1],source.triangleMass[2],source.thickness})
    if((mass&0x8000) || (mass&0x7c00)==0x7c00)return Result::Invalid;
  std::sort(ids.begin(),ids.end());
  const auto end=triangle.faces.begin()+triangle.faceCount;
  const auto face=std::lower_bound(triangle.faces.begin(),end,ids,[](const Face &f,const std::array<int,3>&key){return f.ids<key;});
  if(face==end || face->ids!=ids)return Result::UnknownFace;
  const int wanted=face->outside*parity*(pointInside?-1:1);
  const uint16_t bits=wanted>0?0x3c00:0xbc00;
  if(source.sign==bits)return Result::Kept;
  copy=source;copy.sign=bits;return Result::Changed;
}
struct ListPrefix { uintptr_t data=0; int length=0,capacity=0; };
struct Counts {
  uint64_t kept=0,changed=0,invalid=0,unknown=0,bufferMismatch=0;
  uint64_t enabled=0,enabledChanged=0;
  uint64_t paths[6]{};
};
class Gate {
  SRWLOCK mutex_=SRWLOCK_INIT;
  struct Guard {
    SRWLOCK *lock;
    explicit Guard(SRWLOCK &value):lock(&value){AcquireSRWLockExclusive(lock);}
    ~Guard(){ReleaseSRWLockExclusive(lock);}
  };
  std::atomic<bool> diagnostics_{false};
  bool active_=false;
  Policy policy_{};
  Counts counts_{};
public:
  Gate()=default;Gate(const Gate &)=delete;Gate &operator=(const Gate &)=delete;
  void Diagnostics(bool enabled) { diagnostics_.store(enabled,std::memory_order_relaxed); }
  bool Publish(const Policy &p) {
    Guard lock(mutex_);
    if(!Valid(p)){active_=false;return false;}
    if(!(policy_.ticket==p.ticket))counts_={};
    policy_=p;active_=true;return true;
  }
  void Clear() { Guard lock(mutex_);active_=false; }
  bool Snapshot(Policy &p,Counts &c) {
    Guard lock(mutex_);p=policy_;c=counts_;return active_;
  }
  Result Apply(uintptr_t list,const PointContact &source,PointContact &copy) {
    const bool record=diagnostics_.load(std::memory_order_relaxed);
    Guard lock(mutex_);
    if(!active_)return Result::Foreign;
    const auto result=Correct(policy_,list,source,copy);
    if(!record)return result;
    if(result==Result::Changed)++counts_.changed;
    else if(result==Result::Kept)++counts_.kept;
    else if(result==Result::Invalid)++counts_.invalid;
    else if(result==Result::UnknownFace)++counts_.unknown;
    return result;
  }
  Result Solve(const PointContact *buffer,int index,PointContact &copy,unsigned path) {
    // Preserve retirement synchronization. Detailed counters are opt-in;
    // shared SRW acquisition measured worse for these very short operations.
    const bool record=diagnostics_.load(std::memory_order_relaxed);
    Guard lock(mutex_);
    if(!active_ || path>=6 || !buffer || index<0)return Result::Foreign;
    const auto &list=*reinterpret_cast<const ListPrefix*>(policy_.list);
    if(list.data!=uintptr_t(buffer)) {if(record)++counts_.bufferMismatch;return Result::Foreign;}
    if(index>=list.length || list.length<0 || list.capacity<list.length) {
      if(record)++counts_.invalid;return Result::Invalid;
    }
    const auto result=Correct(policy_,policy_.list,buffer[index],copy);
    if(!record)return result;
    if(result==Result::Changed)++counts_.changed;
    else if(result==Result::Kept)++counts_.kept;
    else if(result==Result::Invalid)++counts_.invalid;
    else if(result==Result::UnknownFace)++counts_.unknown;
    if(result==Result::Changed || result==Result::Kept) {
      ++counts_.paths[path];
      if(buffer[index].flags[0]&0x80000000u) {++counts_.enabled;if(result==Result::Changed)++counts_.enabledChanged;}
    }
    return result;
  }
};
}
