#pragma once
#include <cstdint>
namespace cloth_schedule {
// One actor's expensive preparation per frame, with round-robin waiters.
// Native completion, active simulation and restoration never use this gate.
struct PreparationBudget {
  int frame=-1, owner=-1;
  unsigned waiting=0, next=0;
  double startMs=-1;
  void cancel(unsigned actor) {if(actor<5)waiting&=~(1u<<actor);}
  bool acquire(unsigned actor,int currentFrame,double nowMs) {
    if(actor>=5||currentFrame<0)return false;
    waiting|=1u<<actor;
    if(frame!=currentFrame) {
      frame=currentFrame;owner=-1;startMs=-1;
      for(unsigned k=0;k<5;++k) {const unsigned n=(next+k)%5;
        if(waiting&(1u<<n)) {owner=int(n);next=(n+1)%5;break;}}
    }
    if(owner!=int(actor))return false;
    if(startMs<0)startMs=nowMs;
    if(nowMs-startMs>=4)return false;
    waiting&=~(1u<<actor);return true;
  }
};
}
