#pragma once
#include <array>
#include <cassert>
#include <memory>

// Slot zero is the editor/single player; slots 1..4 are squad members.
// Selection is thread local, while each actor's state survives deferred native
// restoration. Unity accesses still require the game's thread and pose lock.
constexpr unsigned ClothActorCount = 5;
static thread_local unsigned s_clothActorIndex = 0;
struct ClothActorScope {
  unsigned previous = s_clothActorIndex;
  explicit ClothActorScope(unsigned slot) { assert(slot < ClothActorCount); s_clothActorIndex = slot; }
  ~ClothActorScope() { s_clothActorIndex = previous; }
  ClothActorScope(const ClothActorScope &) = delete;
  ClothActorScope &operator=(const ClothActorScope &) = delete;
};
template<class T> struct ClothActorBank {
  std::array<std::unique_ptr<T>, ClothActorCount> values{};
  T &At(unsigned slot) {
    assert(slot < ClothActorCount);
    if (!values[slot]) values[slot] = std::make_unique<T>();
    return *values[slot];
  }
  T &Get() { return At(s_clothActorIndex); }
};
