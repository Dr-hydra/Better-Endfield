#pragma once

#include <atomic>
#include <cstdint>
#include "playback_start.h"

enum class MotionBackend : uint32_t {
  Native = 0,
  Muscle,
  DirectVmd,
};

static inline const char *MotionBackendName(MotionBackend backend) {
  switch (backend) {
  case MotionBackend::Muscle:
    return "Muscle";
  case MotionBackend::DirectVmd:
    return "DirectVmd";
  default:
    return "Native";
  }
}

class MotionBackendStateMachine {
public:
  MotionBackendStateMachine()
      : m_backend(static_cast<uint32_t>(MotionBackend::Native)),
        m_generation(1) {}

  MotionBackend Current() const {
    return static_cast<MotionBackend>(
        m_backend.load(std::memory_order_acquire));
  }

  bool Is(MotionBackend backend) const { return Current() == backend; }

  uint64_t Generation() const {
    return m_generation.load(std::memory_order_acquire);
  }

  bool TransitionTo(MotionBackend backend) {
    const uint32_t desired = static_cast<uint32_t>(backend);
    const uint32_t previous =
        m_backend.exchange(desired, std::memory_order_acq_rel);
    if (previous == desired)
      return false;
    m_generation.fetch_add(1, std::memory_order_acq_rel);
    return true;
  }

  uint64_t InvalidateOwner() {
    return m_generation.fetch_add(1, std::memory_order_acq_rel) + 1;
  }

  void RequestProcessDetach() {
    m_backend.store(static_cast<uint32_t>(MotionBackend::Native),
                    std::memory_order_release);
    m_generation.fetch_add(1, std::memory_order_acq_rel);
  }

private:
  std::atomic<uint32_t> m_backend;
  std::atomic<uint64_t> m_generation;
};

