#pragma once
#include <atomic>
#include <cstdint>

// A single atomic state closes admission and counts in-flight runtime scopes.
// Shutdown never waits while holding the pose/editor lock.
struct RuntimeAdmission {
  static constexpr uint32_t Closed = 0x80000000u;
  std::atomic<uint32_t> state{0};
  bool enter() {
    uint32_t old=state.load(std::memory_order_acquire);
    while (!(old&Closed)) {
      if ((old&~Closed)==Closed-1) return false;
      if (state.compare_exchange_weak(old,old+1,std::memory_order_acq_rel)) return true;
    }
    return false;
  }
  void leave() {state.fetch_sub(1,std::memory_order_acq_rel);}
  bool close() {return !(state.fetch_or(Closed,std::memory_order_acq_rel)&Closed);}
  bool closing() const {return (state.load(std::memory_order_acquire)&Closed)!=0;}
  unsigned active() const {return state.load(std::memory_order_acquire)&~Closed;}
};
static RuntimeAdmission g_runtimeAdmission;
static std::atomic<bool> g_runtimeTornDown{false};
static bool RuntimeClosing() {return g_runtimeAdmission.closing();}
