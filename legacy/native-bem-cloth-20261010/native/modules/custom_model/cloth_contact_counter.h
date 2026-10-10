#pragma once
#include <cstdint>
#include <cstddef>

namespace BetterEndfieldNext::CustomModel::ClothContact {
// ABI admitted only after named IL2CPP fields and value sizes are checked.
struct Container { uintptr_t data=0; uint64_t tail=0; };
struct Job { Container count,next,old,list; };
struct Handle { uint64_t group=0; uint64_t type_and_padding=0; };
static_assert(sizeof(Container)==16 && sizeof(Job)==64 && sizeof(Handle)==16);
enum class Repair { Unchanged, Bound, Refused };
inline Repair BindCounter(const Job& source,uintptr_t leased_list,size_t length_offset,Job& copy) {
    if(!leased_list || source.list.data!=leased_list || (leased_list%alignof(uintptr_t)) ||
       length_offset!=sizeof(uintptr_t) || leased_list>UINTPTR_MAX-length_offset ||
       !source.next.data || !source.old.data) return Repair::Refused;
    if(source.count.data)return Repair::Unchanged;
    if(source.count.tail)return Repair::Refused;
    copy=source;copy.count.data=leased_list+length_offset;
    return Repair::Bound;
}
}
