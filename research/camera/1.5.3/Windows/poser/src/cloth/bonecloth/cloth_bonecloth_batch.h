#pragma once
#include <array>
#include <cstdint>
namespace eiem_cloth_rebuild {
constexpr int BatchCapacity=8;
template<class Step,class Pending,class Clock>
inline bool AdvancePrivatePreparation(Step step,Pending pending,Clock elapsedMs) {
  for(unsigned n=0;n<256 && pending();++n) {
    if(!step())return false;
    if(elapsedMs()>=4.0)return true;
  }
  return true;
}
struct BatchNode {
  uint32_t dependencies=0;
  bool pending=false,lease=false,active=false,confirmed=false,cancelling=false,failed=false;
  bool resourcesOnly=false;
};
using BatchNodes=std::array<BatchNode,BatchCapacity>;
inline bool BatchGraphValid(const BatchNodes &nodes,int count) {
  if(count<1 || count>BatchCapacity) return false;
  uint32_t done=0,all=(1u<<count)-1;
  for(int n=0;n<count;++n)
    if((nodes[n].dependencies&~all) || (nodes[n].dependencies&(1u<<n))) return false;
  for(int pass=0;pass<count;++pass) for(int n=0;n<count;++n)
    if(!(nodes[n].dependencies&~done)) done|=1u<<n;
  return done==all;
}
inline bool BatchDepends(const BatchNodes &nodes,int child,int parent,int count) {
  if(child<0 || parent<0 || child>=count || parent>=count) return false;
  uint32_t parents=nodes[child].dependencies;
  for(int pass=0;pass<count;++pass) for(int n=0;n<count;++n)
    if(parents&(1u<<n)) parents|=nodes[n].dependencies;
  return (parents&(1u<<parent))!=0;
}
inline bool BatchDependenciesReady(const BatchNodes &nodes,int child,int count) {
  for(int n=0;n<count;++n) if(BatchDepends(nodes,child,n,count))
    if(!nodes[n].pending || !nodes[n].lease || !nodes[n].active || !nodes[n].confirmed || nodes[n].cancelling || nodes[n].failed) return false;
  return true;
}
inline bool BatchDependenciesLost(const BatchNodes &nodes,int child,int count) {
  for(int n=0;n<count;++n) if(BatchDepends(nodes,child,n,count))
    if(!nodes[n].pending || nodes[n].cancelling || nodes[n].failed) return true;
  return false;
}
inline int BatchSelectMutation(const BatchNodes &nodes,int count,int inFlight=-1) {
  if(count<1 || count>BatchCapacity) return -1;
  if(inFlight>=0 && inFlight<count && nodes[inFlight].pending && nodes[inFlight].lease && !nodes[inFlight].active && !nodes[inFlight].resourcesOnly)
    return inFlight;
  for(int n=0;n<count;++n) if(nodes[n].pending && nodes[n].cancelling && !nodes[n].resourcesOnly) {
    bool dependent=false;
    for(int k=0;k<count;++k) if(k!=n && nodes[k].pending && nodes[k].lease && !nodes[k].resourcesOnly && BatchDepends(nodes,k,n,count)) dependent=true;
    if(!dependent) return n;
  }
  for(int n=0;n<count;++n) if(nodes[n].pending && nodes[n].lease && !nodes[n].active && !nodes[n].cancelling && !nodes[n].resourcesOnly) return n;
  for(int n=0;n<count;++n) if(nodes[n].pending && !nodes[n].lease && !nodes[n].failed &&
      !nodes[n].cancelling && BatchDependenciesReady(nodes,n,count)) return n;
  return -1;
}
}
