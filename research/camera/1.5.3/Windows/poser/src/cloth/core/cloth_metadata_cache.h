#pragma once
#include <cstdint>
#include <map>
#include <string>
#include <string_view>
#include <tuple>

namespace poser_cloth_metadata {
// IL2CPP metadata lives for the process lifetime. Never store scene objects,
// field values, bone identities or native job storage in this cache.
enum class Kind { Method, FieldOffset, NormalizedMethod, DeclaredMethod, NormalizedField, Class };
struct View {
  uintptr_t owner;
  Kind kind;
  bool isStatic;
  std::string_view name, type, arg0, arg1;
};
inline View Lookup(void *owner, Kind kind, const char *name, const char *type,
                   const char *arg0=nullptr, const char *arg1=nullptr, bool isStatic=false) {
  return {uintptr_t(owner),kind,isStatic,name?name:"",type?type:"",arg0?arg0:"",arg1?arg1:""};
}
struct Key {
  uintptr_t owner;
  Kind kind;
  bool isStatic;
  std::string name, type, arg0, arg1;
  explicit Key(View v):owner(v.owner),kind(v.kind),isStatic(v.isStatic),
      name(v.name),type(v.type),arg0(v.arg0),arg1(v.arg1) {}
  operator View() const { return {owner,kind,isStatic,name,type,arg0,arg1}; }
};
struct Less {
  using is_transparent=void;
  bool operator()(View a, View b) const {
    return std::tie(a.owner,a.kind,a.isStatic,a.name,a.type,a.arg0,a.arg1) <
           std::tie(b.owner,b.kind,b.isStatic,b.name,b.type,b.arg0,b.arg1);
  }
};
class Cache {
  std::map<Key,uintptr_t,Less> entries;
  size_t capacity;
public:
  explicit Cache(size_t limit=4096):capacity(limit) {}
  uintptr_t Find(View v) const {
    const auto it=entries.find(v);
    return it==entries.end()?0:it->second;
  }
  uintptr_t Remember(View v, uintptr_t result) {
    // Failed lookups may become available later during runtime startup.
    if(v.owner && result && entries.size()<capacity) entries.emplace(Key(v),result);
    return result;
  }
  size_t Size() const { return entries.size(); }
  void Clear() { entries.clear(); }
};
// No global lock in the native update or contact worker paths.
inline thread_local Cache cache;
}
