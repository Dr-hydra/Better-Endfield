#pragma once
#include "core/game_hooks.h"
#include <initializer_list>
#include <string>
#include <vector>

// Reflection only, on the existing game thread. Never assume an overload or a
// game-version-specific field offset when crossing the managed/native boundary.
namespace mmd_api {
inline bool Copy(const void *from,void *to,size_t bytes) {
  __try {if(!from)return false;memcpy(to,from,bytes);return true;}
  __except(EXCEPTION_EXECUTE_HANDLER){return false;}
}
inline bool Type(void *type,const char *expected) {
  if(!type||!il2cpp_type_get_name||!hGA)return false;
  using Free=void(*)(void*);
  static auto release=reinterpret_cast<Free>(GetProcAddress(hGA,"il2cpp_free"));
  if(!release)return false;
  const char *name=il2cpp_type_get_name(type);
  bool equal=name&&!strcmp(name,expected);if(name)release(const_cast<char*>(name));return equal;
}
inline void *Class(const char *space,const char *name) {
  if(!il2cpp_domain_get||!il2cpp_domain_get_assemblies)return nullptr;
  size_t count=0;auto domain=il2cpp_domain_get();
  auto assemblies=domain?il2cpp_domain_get_assemblies(domain,&count):nullptr;
  if(!assemblies)return nullptr;
  if(il2cpp_class_from_name)for(size_t i=0;i<count;++i) {
    auto image=il2cpp_assembly_get_image(assemblies[i]);
    if(auto cls=image?il2cpp_class_from_name(image,space,name):nullptr)return cls;
  }
  return nullptr;
}
inline void *Method(void *cls,const char *name,const char *result,
                    std::initializer_list<const char*> args={},bool isStatic=false) {
  if(!il2cpp_method_get_flags||!il2cpp_method_get_return_type||!il2cpp_method_get_param||
     !il2cpp_class_get_methods||!il2cpp_class_get_parent)return nullptr;
  for(int depth=0;cls&&depth<16;++depth,cls=il2cpp_class_get_parent(cls)) {
    void *it=nullptr;
    while(auto m=il2cpp_class_get_methods(cls,&it)) {
      uint32_t flags=0;auto n=il2cpp_method_get_name(m);
      if(!n||strcmp(n,name)||il2cpp_method_get_param_count(m)!=args.size()||
         ((il2cpp_method_get_flags(m,&flags)&0x10)!=0)!=isStatic||
         !Type(il2cpp_method_get_return_type(m),result))continue;
      size_t i=0;bool ok=true;for(auto type:args)ok=Type(il2cpp_method_get_param(m,uint32_t(i++)),type)&&ok;
      if(ok)return m;
    }
  }
  return nullptr;
}
inline int Field(void *cls,const char *name,const char *type) {
  if(!il2cpp_class_get_fields||!il2cpp_field_get_type||!il2cpp_class_get_parent)return -1;
  for(int depth=0;cls&&depth<16;++depth,cls=il2cpp_class_get_parent(cls)) {
    void *it=nullptr;
    while(auto f=il2cpp_class_get_fields(cls,&it)) {
      auto n=il2cpp_field_get_name(f);
      if(n&&!strcmp(n,name)&&Type(il2cpp_field_get_type(f),type)&&!(il2cpp_field_get_flags(f)&0x10)) {
        auto off=il2cpp_field_get_offset(f);return off<65536?int(off):-1;
      }
    }
  }
  return -1;
}
inline bool Call(void *method,void *self,void **args,void *&result) {
  result=nullptr;if(!method||!il2cpp_runtime_invoke||RuntimeClosing())return false;
  __try {void *error=nullptr;result=il2cpp_runtime_invoke(method,self,args,&error);return !error;}
  __except(EXCEPTION_EXECUTE_HANDLER){return false;}
}
template<class T> inline bool Value(void *method,void *self,T &value) {
  void *box=nullptr;return Call(method,self,nullptr,box)&&box&&Copy((char*)box+16,&value,sizeof(T));
}
template<class T> inline bool At(void *object,int offset,T &value) {
  return object&&offset>=0&&Copy((char*)object+offset,&value,sizeof(T));
}
} // namespace mmd_api
