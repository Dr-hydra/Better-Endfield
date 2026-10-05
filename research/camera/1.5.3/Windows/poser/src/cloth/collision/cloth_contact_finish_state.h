#pragma once
#include "cloth_display_state.h"

namespace eiem_cloth_finish {
using eiem_cloth_display::Array;
using eiem_cloth_display::Same;
using eiem_cloth_display::Range;
struct Ref { uintptr_t data=0;uint64_t tail=0; };
inline bool Same(Ref a,Ref b){return a.data==b.data&&a.tail==b.tail;}
struct EdgeJob {
  Array steps,teams,parameters,attributes,depths,teamIds,edges,next,velocity,friction,normal,flags,colliders;
  Array count,sum,tempFriction,tempNormal;
  Ref counter;
};
struct ReduceJob {Array steps,friction,normal,next,velocity,count,sum,tempFriction,tempNormal;Ref counter;};
struct Edge {int a,b;};
struct Handle {uint64_t a=0,b=0;};
inline bool Same(Handle a,Handle b){return a.a==b.a&&a.b==b.b;}
static_assert(sizeof(EdgeJob)==288&&offsetof(EdgeJob,counter)==272&&sizeof(ReduceJob)==160&&
    offsetof(ReduceJob,counter)==144&&sizeof(Ref)==16,"native contact producer ABI");
constexpr int Passes=12;
struct Policy {
  eiem_cloth_display::Policy owner;
  uintptr_t distance=0;
  EdgeJob edge;
  ReduceJob reduce;
  int edgeStart=0,edgeCount=0;
  bool emptyTeamZero=false;
};
inline bool Buffer(Array a,int limit=65536){return a.data&&a.length>0&&a.length<=limit;}
inline bool Compatible(const EdgeJob &e,const ReduceJob &r) {
  return Same(e.next,r.next)&&Same(e.velocity,r.velocity)&&Same(e.friction,r.friction)&&Same(e.normal,r.normal)&&
      Same(e.count,r.count)&&Same(e.sum,r.sum)&&Same(e.tempFriction,r.tempFriction)&&Same(e.tempNormal,r.tempNormal);
}
inline bool Valid(const Policy &p) {
  const auto &o=p.owner;const auto &e=p.edge;const auto &r=p.reduce;
  if(!eiem_cloth_display::Valid(o)||!p.distance||!p.emptyTeamZero||!Compatible(e,r)||
      !Same(e.teams,o.teams)||!Same(e.attributes,o.attributes)||!e.counter.data||!r.counter.data||
      e.counter.data==r.counter.data||!Buffer(e.steps)||!Buffer(r.steps)||
      !Range(e.parameters,o.team,1)||!Range(e.depths,o.proxyStart,o.count)||
      !Range(e.edges,p.edgeStart,p.edgeCount)||!Range(e.teamIds,p.edgeStart,p.edgeCount)||
      e.edges.length!=e.teamIds.length||p.edgeCount>ClothBoneMaxEdges||
      !Buffer(e.flags)||!Buffer(e.colliders)||e.flags.length!=e.colliders.length)return false;
  const Array particle[]{e.next,e.velocity,e.friction,e.normal,e.count,e.tempFriction};
  for(auto a:particle)if(!Range(a,o.particleStart,o.count))return false;
  return Buffer(e.sum,196608)&&Buffer(e.tempNormal,196608)&&e.sum.length>=3*e.next.length&&
      e.tempNormal.length>=3*e.next.length&&e.count.length>=e.next.length&&e.tempFriction.length>=e.next.length;
}

class View {
  Policy p_{};bool ready_=false,borrowed_=false;
  std::vector<int16_t> ids_;
  std::vector<int> count_,sum_,friction_,normal_;
  int edges_=0;
public:
  void Revoke(){ready_=false;}
  void CompletedBoundary(){ready_=false;borrowed_=false;}
  bool Ready()const{return ready_;}
  const Policy &Identity()const{return p_;}
  int OwnedEdges()const{return edges_;}
  bool Publish(const Policy &p) {
    ready_=false;if(borrowed_||!Valid(p))return false;
    const auto &o=p.owner;const auto &e=p.edge;
    const auto ids=reinterpret_cast<const int16_t*>(e.teamIds.data);
    const auto particles=reinterpret_cast<const int16_t*>(o.ids.data);
    const auto attrs=reinterpret_cast<const uint8_t*>(o.attributes.data);
    const auto edges=reinterpret_cast<const Edge*>(e.edges.data);
    for(int n=0;n<o.count;++n)if(particles[o.particleStart+n]!=o.team||
        attrs[o.proxyStart+n]!=o.originalAttributes[n])return false;
    ids_.assign(e.teamIds.length,0);edges_=0;int owned=0;
    for(int n=0;n<e.teamIds.length;++n)if(ids[n]==o.team) {
      if(n<p.edgeStart||n-p.edgeStart>=p.edgeCount)return false;
      const auto edge=edges[n];
      if(edge.a<0||edge.b<0||edge.a>=o.count||edge.b>=o.count||edge.a==edge.b)return false;
      ++owned;
      if((o.originalAttributes[edge.a]&o.move)&&(o.originalAttributes[edge.b]&o.move)) {
        ids_[n]=int16_t(o.team);++edges_;
      }
    }
    if(owned!=p.edgeCount||!edges_)return false;
    count_.assign(e.count.length,0);sum_.assign(e.sum.length,0);
    friction_.assign(e.tempFriction.length,0);normal_.assign(e.tempNormal.length,0);
    p_=p;ready_=true;return true;
  }
  bool Bind(const EdgeJob &source,const ReduceJob &reduce,EdgeJob &e,ReduceJob &r) {
    if(!ready_||memcmp(&source,&p_.edge,sizeof(source))||memcmp(&reduce,&p_.reduce,sizeof(reduce)))return false;
    e=source;r=reduce;e.teamIds.data=uintptr_t(ids_.data());
    e.count.data=r.count.data=uintptr_t(count_.data());e.sum.data=r.sum.data=uintptr_t(sum_.data());
    e.tempFriction.data=r.tempFriction.data=uintptr_t(friction_.data());
    e.tempNormal.data=r.tempNormal.data=uintptr_t(normal_.data());
    borrowed_=true;return true;
  }
};

class Chain {
  enum class Phase {None,Distance,Edge,Reduce,Consumed};Phase phase_=Phase::None;
  Handle last_{};
public:
  void Clear(){phase_=Phase::None;}
  void First(Handle output){phase_=Phase::Distance;last_=output;}
  bool Edge(Handle ,Handle output){
    if(phase_!=Phase::Distance){Clear();return false;}
    phase_=Phase::Edge;last_=output;return true;
  }
  bool Reduce(Handle input,Handle output){
    if(phase_!=Phase::Edge||!Same(input,last_)){Clear();return false;}
    phase_=Phase::Reduce;last_=output;return true;
  }
  bool Consume(Handle input){
    const bool ok=phase_==Phase::Reduce&&Same(input,last_);phase_=Phase::Consumed;return ok;
  }
};
}
