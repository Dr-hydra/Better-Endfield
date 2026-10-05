#pragma once
#include "core/game_hooks.h"
#include <array>

// Read the game's ordered slots, not scene enumeration or the current leader.
// Resolve full signatures; unknown versions leave the feature unavailable.
namespace poser_squad {
constexpr size_t MaxMembers=4;
struct Member { void *entity=nullptr,*animator=nullptr,*component=nullptr; };
struct Snapshot {
  void *manager=nullptr,*squad=nullptr;
  std::array<Member,MaxMembers> members{};
  int count=0;
  bool valid=false;
};
static void *klass=nullptr,*getSquad=nullptr,*getCount=nullptr,*getMember=nullptr,*getLoading=nullptr;
static void *playerClass=nullptr,*getPlayer=nullptr;
static int squadOffset=-1;
static const char *status=u8"等待游戏线程读取小队";
static void *Method(const char *name,const char *returnNs,const char *returnName,bool withIndex=false,void *owner=nullptr,bool isStatic=false) {
  if(!owner)owner=klass;
  if(!owner)return nullptr;
  void *it=nullptr,*m=nullptr;
  while((m=il2cpp_class_get_methods(owner,&it))) {
    const char *n=il2cpp_method_get_name(m);
    if(!n || strcmp(n,name) || bool(il2cpp_method_get_flags(m,nullptr)&0x10)!=isStatic ||
       il2cpp_method_get_param_count(m)!=(withIndex?1:0))continue;
    if(!MetadataClassIs(il2cpp_method_get_return_type(m),returnNs,returnName))continue;
    if(withIndex && !MetadataClassIs(il2cpp_method_get_param(m,0),"System","Int32"))continue;
    return m;
  }
  return nullptr;
}
static bool ResolveApi() {
  if(getMember&&getPlayer&&squadOffset>=16)return true;
  size_t count=0;void **assemblies=il2cpp_domain_get_assemblies(il2cpp_domain_get(),&count);
  klass=FindClass("Beyond.Gameplay.Core","SquadManager",assemblies,count);
  if(!klass) {status=u8"未找到游戏小队接口";return false;}
  getSquad=Method("get_curSquad","","Squad");
  getCount=Method("get_slotCount","System","Int32");
  getLoading=Method("get_isSquadLoading","System","Boolean");
  getMember=Method("GetMemberBySlot","Beyond.Gameplay.Core","Entity",true);
  if(!getSquad || !getCount || !getLoading || !getMember) {status=u8"小队接口签名不匹配";return false;}
  // SquadManager is owned by GamePlayer, not a singleton. Only read the typed
  // existing player; do not create managers or enumerate scene objects.
  void *instanceClass=FindClass("Beyond.Gameplay","GameInstance",assemblies,count);
  playerClass=FindClass("Beyond.Gameplay","GamePlayer",assemblies,count);
  getPlayer=instanceClass?Method("get_player","Beyond.Gameplay","GamePlayer",false,instanceClass,true):nullptr;
  squadOffset=-1;
  if(playerClass) {
    void *it=nullptr,*f=nullptr;
    while((f=il2cpp_class_get_fields(playerClass,&it))) {
      const char *name=il2cpp_field_get_name(f);
      if(!name||strcmp(name,"squadManager")||(il2cpp_field_get_flags(f)&0x10)||
          !MetadataClassIs(il2cpp_field_get_type(f),"Beyond.Gameplay.Core","SquadManager"))continue;
      size_t off=il2cpp_field_get_offset(f);if(off>=16&&off<0x4000)squadOffset=int(off);
    }
  }
  if(!getPlayer||squadOffset<16) {status=u8"无法解析当前玩家的小队管理器";return false;}
  return true;
}
static void *Instance() {
  if(!getPlayer||squadOffset<16||squadOffset>=0x4000)return nullptr;
  void *player=Invoke(getPlayer,nullptr);
  if(!player){status=u8"当前场景尚未提供小队玩家";return nullptr;}
  if(il2cpp_object_get_class(player)!=playerClass){status=u8"当前玩家类型与小队接口不匹配";return nullptr;}
  void *manager=*(void**)((char*)player+squadOffset);
  if(!manager){status=u8"当前玩家的小队管理器尚未就绪";return nullptr;}
  if(il2cpp_object_get_class(manager)!=klass){status=u8"当前小队管理器类型不匹配";return nullptr;}
  return manager;
}
static bool ReadRaw(Snapshot *out) {
  __try {
    if(RuntimeClosing() || CharacterSwitchInProgress()) {status=u8"正在切换角色或退出";return false;}
    if(!ResolveApi())return false;
    out->manager=Instance();if(!out->manager)return false;
    void *loading=Invoke(getLoading,out->manager);
    if(!loading || *(bool*)((char*)loading+16)) {status=u8"小队正在加载";return false;}
    out->squad=Invoke(getSquad,out->manager);if(!out->squad) {status=u8"当前没有可用小队";return false;}
    void *size=Invoke(getCount,out->manager);if(!size)return false;
    out->count=*(int*)((char*)size+16);
    if(out->count<1 || out->count>int(MaxMembers)) {status=u8"小队人数不在 1–4 人范围内";return false;}
    for(int n=0;n<out->count;++n) {
      void *args[]={&n};auto &m=out->members[n];
      m.entity=Invoke(getMember,out->manager,args);
      if(m.entity)ReadCharacterRig(m.entity,&m.animator,&m.component);
    }
    out->valid=true;return true;
  } __except(1) {status=u8"读取小队时实例已失效";return false;}
}
static Snapshot Read() {
  Snapshot result;
  if(!ReadRaw(&result))result.valid=false;
  else status=u8"按当前小队第 1–4 位读取";
  return result;
}
}
