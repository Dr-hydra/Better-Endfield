#pragma once
#include <cstdint>
#include <cstddef>
#include <cstring>

namespace eiem_cloth_contact_job {
struct Container { uintptr_t data=0; uint64_t tail=0; };
struct Job { Container count,next,old,list; };
static_assert(sizeof(Container)==16 && sizeof(Job)==64,"x64 native contact ABI");
enum class Patch { Unchanged, Repaired, Invalid };
inline Patch BindCount(const Job &source,uintptr_t expectedHeader,size_t lengthOffset,Job &copy) {
  if(!expectedHeader || expectedHeader!=source.list.data || (expectedHeader&7) ||
      lengthOffset!=sizeof(uintptr_t) || expectedHeader>UINTPTR_MAX-lengthOffset ||
      !source.next.data || !source.old.data) return Patch::Invalid;
  if(source.count.data) return Patch::Unchanged;
  if(source.count.tail) return Patch::Invalid;
  copy=source;
  copy.count.data=expectedHeader+lengthOffset;
  return Patch::Repaired;
}
}
