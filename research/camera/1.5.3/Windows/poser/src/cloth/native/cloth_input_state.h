#pragma once
#include <cstdint>
#include <cstddef>
#include <cmath>

namespace eiem_cloth_input {
struct Identity {
  uint64_t session = 0, generation = 0;
  uintptr_t owner = 0, process = 0, serialize = 0;
  int cloth = 0, team = 0, scene = 0;
  bool operator==(const Identity &b) const {
    return session == b.session && generation == b.generation && owner == b.owner &&
           process == b.process && serialize == b.serialize && cloth == b.cloth &&
           team == b.team && scene == b.scene;
  }
};
inline bool CanRead(bool main, bool owned, bool insideUpdate, bool exactCallsite,
                    int animator, int cross, bool fingerprint) {
  return main && owned && insideUpdate && exactCallsite && animator == 0 &&
         cross == 1 && fingerprint;
}
inline uint64_t Fingerprint(const unsigned char *bytes, size_t count) {
  uint64_t h = 14695981039346656037ULL;
  for (size_t n = 0; n < count; ++n) h = (h ^ bytes[n]) * 1099511628211ULL;
  return h;
}
inline bool MatchSlot(int team, int slotTeam, uintptr_t expected, int expectedId,
                      uintptr_t actual, int actualId, int slot,
                      int &matchedSlot, bool &duplicate) {
  if (team <= 0 || team != slotTeam || !expected || !expectedId ||
      expected != actual || expectedId != actualId || slot < 0) return false;
  if (matchedSlot >= 0 && matchedSlot != slot) duplicate = true;
  else matchedSlot = slot;
  return true;
}
template<class T, size_t N> struct Ring {
  T data[N]{};
  size_t next = 0, count = 0;
  void Push(const T &v) { data[next] = v; next = (next + 1) % N; if (count < N) ++count; }
  const T &At(size_t n) const { return data[(next + N - count + n) % N]; }
  void Clear() { next = count = 0; }
};
struct Mapping {
  uintptr_t manager = 0, access = 0;
  int length = -1, cursor = 0;
  uint64_t epoch = 0;
  bool Refresh(uintptr_t m, uintptr_t a, int len) {
    if (m == manager && a == access && len == length) return false;
    manager = m; access = a; length = len; cursor = 0; ++epoch; return true;
  }
  void Invalidate() { manager = access = 0; length = -1; cursor = 0; ++epoch; }
};
struct CostBudget {
  unsigned samples = 0, overruns = 0;
  bool Observe(double milliseconds) {
    if (++samples <= 4) return false;
    return std::isfinite(milliseconds) && milliseconds > 5 && ++overruns >= 3;
  }
};
struct RemapBudget {
  uint64_t window = 0;
  unsigned attempts = 0;
  bool Take(uint64_t now) {
    if (!attempts || now - window >= 2000) { window = now; attempts = 0; }
    return ++attempts <= 8;
  }
};
}
