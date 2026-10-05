#pragma once
#include <atomic>
#include <cstdint>

namespace eiem_playback {
enum class Preparation : uint32_t { Idle, Waiting, Prepared, Ready, Failed };
struct TimelineAdmission {
  bool wasHeld=false;
  bool HoldElapsed(bool held) {
    const bool suppress=held||wasHeld;wasHeld=held;return suppress;
  }
};
class StartGate {
  std::atomic<uint64_t> stamp_{0}, generation_{0};
  std::atomic<uint32_t> backend_{0};
  uintptr_t owner_=0;
  uint64_t serial_=0, started_=0;
  int readyFrame_=-1;
public:
  uint64_t Arm(uint32_t backend,uint64_t generation,uintptr_t owner,uint64_t now) {
    if(Matches(backend,generation,owner) && State()!=Preparation::Idle)return Ticket();
    Cancel();
    owner_=owner;started_=now;readyFrame_=-1;
    backend_.store(backend,std::memory_order_relaxed);
    generation_.store(generation,std::memory_order_relaxed);
    stamp_.store((++serial_<<3)|uint64_t(Preparation::Waiting),std::memory_order_release);
    return Ticket();
  }
  void Cancel() {
    stamp_.store(++serial_<<3,std::memory_order_release);readyFrame_=-1;
  }
  uint64_t Ticket()const{return stamp_.load(std::memory_order_acquire)>>3;}
  Preparation State()const{return Preparation(stamp_.load(std::memory_order_acquire)&7);}
  Preparation StateFor(uint32_t backend,uint64_t generation)const {
    const auto before=stamp_.load(std::memory_order_acquire);
    return backend_.load(std::memory_order_relaxed)==backend &&
        generation_.load(std::memory_order_relaxed)==generation &&
        stamp_.load(std::memory_order_acquire)==before ? Preparation(before&7) : Preparation::Idle;
  }
  bool Holding(uint32_t backend,uint64_t generation)const {
    const auto phase=StateFor(backend,generation);
    return phase==Preparation::Waiting||phase==Preparation::Prepared||phase==Preparation::Failed;
  }
  bool BlocksPose(uint32_t backend,uint64_t generation)const {
    const auto phase=StateFor(backend,generation);
    return phase==Preparation::Waiting||phase==Preparation::Failed;
  }
  bool Matches(uint32_t backend,uint64_t generation,uintptr_t owner)const {
    return backend_.load()==backend&&generation_.load()==generation&&owner_==owner;
  }
  uint64_t Elapsed(uint64_t now)const{return now>=started_?now-started_:0;}
  int PreparedFrame()const{return readyFrame_;}
  bool Observe(uint64_t ticket,bool ready,bool failed,int completedFrame) {
    const auto phase=State();
    if(ticket!=Ticket()||(phase!=Preparation::Waiting&&phase!=Preparation::Prepared))return false;
    if(failed) {stamp_.store((ticket<<3)|uint64_t(Preparation::Failed),std::memory_order_release);return false;}
    if(!ready) {
      readyFrame_=-1;
      stamp_.store((ticket<<3)|uint64_t(Preparation::Waiting),std::memory_order_release);
      return false;
    }
    if(phase!=Preparation::Waiting)return false;
    if(completedFrame<0){readyFrame_=-1;return false;}
    if(readyFrame_<0){readyFrame_=completedFrame;return false;}
    if(completedFrame<=readyFrame_)return false;
    readyFrame_=completedFrame;
    stamp_.store((ticket<<3)|uint64_t(Preparation::Prepared),std::memory_order_release);return true;
  }
  bool PoseSubmitted(uint64_t ticket,uint32_t backend,uint64_t generation,uintptr_t owner,int frame) {
    if(ticket!=Ticket()||State()!=Preparation::Prepared||!Matches(backend,generation,owner)||frame<readyFrame_)return false;
    stamp_.store((ticket<<3)|uint64_t(Preparation::Ready),std::memory_order_release);return true;
  }
};
}
static eiem_playback::StartGate g_clothPlaybackGate;
