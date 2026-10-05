#pragma once
#include "core/game_hooks.h"

// SMC also animates shader/BlendShape channels. Holding its bone transforms
// alone cannot freeze an automatic blink. Pause the original controller while
// posing, leaving Update and the explicit MMD expression evaluator running.
// Resolve instance Boolean fields by metadata; never guess offsets here.
static int SMCBooleanOffset(void *klass, const char *name) {
  if (!il2cpp_class_get_fields || !il2cpp_field_get_name ||
      !il2cpp_field_get_flags || !il2cpp_field_get_type ||
      !il2cpp_type_get_type || !il2cpp_field_get_offset) return -1;
  for (int depth=0;klass && depth<16;++depth) {
    void *iter=nullptr;
    while (void *field=il2cpp_class_get_fields(klass,&iter)) {
      const char *actual=il2cpp_field_get_name(field);
      if (!actual || strcmp(actual,name)) continue;
      if ((il2cpp_field_get_flags(field)&0x10) ||
          il2cpp_type_get_type(il2cpp_field_get_type(field))!=2) return -1;
      auto offset=il2cpp_field_get_offset(field);
      return offset>=16 && offset<4096 ? int(offset) : -1;
    }
    klass=il2cpp_class_get_parent?il2cpp_class_get_parent(klass):nullptr;
  }
  return -1;
}
static bool SMCReadBoolean(void *object,int offset,bool &value) {
  if (!object || offset<16 || RuntimeClosing()) return false;
  __try {
    unsigned char raw=*(unsigned char *)((char *)object+offset);
    if (raw>1) return false;
    value=raw!=0;return true;
  } __except(1) {return false;}
}
static bool SMCWriteBoolean(void *object,int offset,bool value) {
  if (!object || offset<16 || RuntimeClosing()) return false;
  __try {*(bool *)((char *)object+offset)=value;return true;}
  __except(1) {return false;}
}
struct SMCAutomationPause {
  struct Field {const char *name;bool target;int offset=-1;bool original=false;bool captured=false;};
  Field fields[3]={{"m_pauseEmotionAutoBlink",true},{"m_pauseEmotion",true},{"m_isEyeLookAtIKEnable",false}};
  void *core=nullptr,*animator=nullptr;
  uint32_t coreHandle=0,animatorHandle=0;
  bool confirmed=false;

  bool live() const {
    if (RuntimeClosing() || !core || !animator || !il2cpp_gchandle_get_target) return false;
    __try {
      return il2cpp_gchandle_get_target(coreHandle)==core &&
          il2cpp_gchandle_get_target(animatorHandle)==animator && UnityObjAlive(animator);
    } __except(1) {return false;}
  }
  void release(bool restore=true) {
    if (restore && live())
      for (auto &field:fields) if(field.captured) SMCWriteBoolean(core,field.offset,field.original);
    if (!RuntimeClosing() && il2cpp_gchandle_free) {
      if(coreHandle)il2cpp_gchandle_free(coreHandle);
      if(animatorHandle)il2cpp_gchandle_free(animatorHandle);
    }
    core=nullptr;animator=nullptr;coreHandle=animatorHandle=0;confirmed=false;
    for(auto &field:fields){field.offset=-1;field.captured=false;}
  }
  void update(void *current,void *owner,bool hold,bool verified) {
    if (!hold || !verified || core!=current || animator!=owner) release();
    if (!hold || !verified || !current || !owner || RuntimeClosing()) return;
    if (!core) {
      if (!UnityObjAlive(owner) || !il2cpp_gchandle_new || !il2cpp_gchandle_free ||
          !il2cpp_gchandle_get_target || !il2cpp_object_get_class) return;
      coreHandle=il2cpp_gchandle_new(current,false);
      animatorHandle=il2cpp_gchandle_new(owner,false);
      if (!coreHandle || !animatorHandle) {release(false);return;}
      core=current;animator=owner;
      void *klass=il2cpp_object_get_class(current);
      for (auto &field:fields) {
        field.offset=SMCBooleanOffset(klass,field.name);
        field.captured=SMCReadBoolean(core,field.offset,field.original);
      }
      Log("[SMC] freeze automation: autoBlink=%d emotion=%d eyeLookAt=%d",
          fields[0].offset,fields[1].offset,fields[2].offset);
    }
    if (!live()) {release(false);return;}
    confirmed=true;
    for (auto &field:fields) {
      bool value=false;
      bool ok=field.captured && SMCWriteBoolean(core,field.offset,field.target) &&
          SMCReadBoolean(core,field.offset,value) && value==field.target;
      // Eye tracking is optional; both native expression pauses are required.
      if (&field!=&fields[2] && !ok) confirmed=false;
    }
  }
};
static SMCAutomationPause s_smcAutomation;
