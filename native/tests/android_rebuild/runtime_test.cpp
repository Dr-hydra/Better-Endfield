#include <cstddef>
#include <cstdint>
#include <string>
#define private public
#include "core/runtime.h"
#undef private
#include <cassert>
#include <cstring>
#include <cstdlib>
#include <iostream>
using namespace betterendfield;
namespace betterendfield {
struct Il2CppDomain{};struct Il2CppAssembly{};struct Il2CppImage{};struct Il2CppClass{};
struct Il2CppType{const char* name;};struct FieldInfo{int flags;};struct MethodInfo{void* entry;};
}
static Il2CppDomain domain; static Il2CppAssembly assembly;static Il2CppImage image;static Il2CppClass klass;
static Il2CppType type{"System.Type"}, str{"System.String"}, obj{"System.Object"};
static int boxed=23, attachCount,detachCount,fieldCalls,parseCalls;static void* current=nullptr;
static bool boxingFails=false,parseThrows=false;
static void ParseEntry(){}
static MethodInfo method{reinterpret_cast<void*>(&ParseEntry)};
int main(){
    Il2CppRuntime rt;
    rt.field_get_flags_=[](const FieldInfo*f){return f->flags;};
    rt.field_get_value_object_=[](const FieldInfo*,void*)->void*{++fieldCalls;return boxingFails?nullptr:&boxed;};
    FieldInfo instance{0},statik{0x10},literal{0x50};
    assert(!rt.ReadFieldObject(&instance,nullptr)&&fieldCalls==0);
    assert(rt.ReadFieldObject(&statik,nullptr)==&boxed&&fieldCalls==1);
    assert(rt.ReadFieldObject(ResolvedField{&statik,0},nullptr)==&boxed);
    assert(rt.ReadFieldObject(&instance,&boxed)==&boxed);
    rt.thread_current_=[]()->void*{return current;};rt.domain_get_=[](){return &domain;};
    rt.thread_attach_=[](Il2CppDomain*)->void*{++attachCount;return current=&boxed;};
    rt.thread_detach_=[](void*){++detachCount;current=nullptr;};
    {Il2CppThreadScope outer(rt);assert(outer.attached());{Il2CppThreadScope inner(rt);assert(inner.attached());}assert(detachCount==0);}assert(attachCount==1&&detachCount==1);
    current=&boxed;{Il2CppThreadScope existing(rt);assert(existing.attached());}assert(detachCount==1);current=nullptr;
    rt.library_=reinterpret_cast<void*>(1);
    rt.domain_get_assemblies_=[](Il2CppDomain*,size_t*count)->const Il2CppAssembly**{static const Il2CppAssembly*list[]{&assembly};*count=1;return list;};
    rt.assembly_get_image_=[](const Il2CppAssembly*)->const Il2CppImage*{return &image;};
    rt.image_get_name_=[](const Il2CppImage*){return "mscorlib.dll";};
    assert(rt.HasAssembly("mscorlib")&&rt.HasAssembly("mscorlib.dll"));assert(!rt.HasAssembly("Gameplay.Beyond.dll"));
    auto enumeration = rt.domain_get_assemblies_;
    auto imageName = rt.image_get_name_;
    rt.domain_assembly_open_=[](Il2CppDomain*,const char* name)->const Il2CppAssembly* {
        // This synthetic player only recognizes the extensionless public name.
        return std::strcmp(name,"mscorlib")==0 ? &assembly : nullptr;
    };
    rt.domain_get_assemblies_=nullptr;
    assert(rt.HasAssembly("mscorlib.dll") && rt.HasAssembly("mscorlib"));
    assert(!rt.HasAssembly("missing.dll"));
    rt.domain_get_assemblies_=enumeration;
    rt.image_get_name_=nullptr;
    assert(rt.HasAssembly("mscorlib.dll") && !rt.HasAssembly("missing"));
    rt.image_get_name_=imageName;
    rt.class_from_name_=[](const Il2CppImage*,const char*,const char*){return &klass;};
    rt.class_get_type_=[](Il2CppClass*)->const Il2CppType*{return &type;};rt.type_get_object_=[](const Il2CppType*)->void*{return &type;};
    rt.class_get_methods_=[](Il2CppClass*,void**iter)->const MethodInfo*{if(*iter)return nullptr;*iter=&method;return &method;};
    rt.method_get_name_=[](const MethodInfo*){return "Parse";};rt.method_get_parameter_count_=[](const MethodInfo*)->uint32_t{return 2;};
    rt.method_get_parameter_=[](const MethodInfo*,uint32_t i)->const Il2CppType*{return i?&str:&type;};
    rt.method_get_return_type_=[](const MethodInfo*)->const Il2CppType*{return &obj;};
    rt.type_get_name_=[](const Il2CppType*t){return strdup(t->name);};rt.free_=std::free;
    rt.field_get_parent_=[](const FieldInfo*){return &klass;};rt.field_get_name_=[](const FieldInfo*){return "Sprint";};
    rt.class_is_enum_=[](const Il2CppClass*){return true;};rt.string_new_=[](const char*)->void*{return &str;};
    rt.runtime_invoke_=[](const MethodInfo*,void*,void**args,void**exception)->void*{assert(args[0]==&type&&args[1]==&str);++parseCalls;if(parseThrows){*exception=&boxed;return nullptr;}return &boxed;};
    boxingFails=true;assert(!rt.ReadFieldObject(&statik,nullptr)&&parseCalls==0);
    assert(rt.ReadFieldObject(&literal,nullptr)==&boxed&&parseCalls==1);
    parseThrows=true;assert(!rt.ReadFieldObject(&literal,nullptr)&&parseCalls==2);
    parseThrows=false;assert(rt.ReadFieldObject(&literal,nullptr)==&boxed&&parseCalls==3);
    rt.library_=nullptr; // Synthetic fixture, not a real dlopen handle.
    std::cout<<"PASS static/null field contract, named enum fallback, isolated failure, loaded images, nested thread scope\n";
}
