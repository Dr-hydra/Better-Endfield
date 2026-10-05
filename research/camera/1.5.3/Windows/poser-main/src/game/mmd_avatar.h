#pragma once
#include "game/mmd_runtime_api.h"
#include "math/mmd_avatar.h"

// Natural Avatar metadata, as used by Sasye/EIEM's direct VMD backend
// (8b46b76, AGPL-3.0). No mesh vertices, GPU readback or current-pose sampling.
namespace mmd_avatar {
struct Api {
  bool initialized=false,ready=false;
  void *avatar=nullptr,*description=nullptr;
  int skeleton=-1,stride=0,name=-1,position=-1,rotation=-1,scale=-1;
};
static Api api;
inline bool Init() {
  if(api.initialized)return api.ready;
  api.initialized=true;
  auto animator=mmd_api::Class("UnityEngine","Animator"),avatar=mmd_api::Class("UnityEngine","Avatar");
  auto desc=mmd_api::Class("UnityEngine","HumanDescription"),bone=mmd_api::Class("UnityEngine","SkeletonBone");
  if(!animator||!avatar||!desc||!bone||!il2cpp_class_value_size)return false;
  api.avatar=mmd_api::Method(animator,"get_avatar","UnityEngine.Avatar");
  api.description=mmd_api::Method(avatar,"get_humanDescription","UnityEngine.HumanDescription");
  api.skeleton=mmd_api::Field(desc,"m_Skeleton","UnityEngine.SkeletonBone[]");
  if(api.skeleton<0)api.skeleton=mmd_api::Field(desc,"skeleton","UnityEngine.SkeletonBone[]");
  api.name=mmd_api::Field(bone,"name","System.String");
  api.position=mmd_api::Field(bone,"position","UnityEngine.Vector3");
  api.rotation=mmd_api::Field(bone,"rotation","UnityEngine.Quaternion");
  api.scale=mmd_api::Field(bone,"scale","UnityEngine.Vector3");
  uint32_t align=0;api.stride=il2cpp_class_value_size(bone,&align);
  // IL2CPP field offsets for value types normally include the boxed header.
  int header=api.name==16?16:api.name==0?0:-1;
  if(header<0)return false;
  api.name-=header;api.position-=header;api.rotation-=header;api.scale-=header;
  api.ready=api.avatar&&api.description&&api.skeleton>=16&&api.stride>=48&&api.stride<=256&&
    api.name>=0&&api.name+8<=api.stride&&api.position>=0&&api.position+12<=api.stride&&
    api.rotation>=0&&api.rotation+16<=api.stride&&api.scale>=0&&api.scale+12<=api.stride;
  Log("[MMD-AVATAR] metadata available=%d stride=%d skeleton=%d",api.ready,api.stride,api.skeleton);
  return api.ready;
}
inline bool Calibrate(void *animator,mmd::RetargetProfile &profile,std::string &error) {
  if(!Init()||!UnityObjAlive(animator)){error=u8"Avatar 骨架接口尚未就绪";return false;}
  void *avatar=nullptr,*description=nullptr,*array=nullptr;uintptr_t count=0;
  if(!mmd_api::Call(api.avatar,animator,nullptr,avatar)||!UnityObjAlive(avatar)||
     !mmd_api::Call(api.description,avatar,nullptr,description)||
     !mmd_api::At(description,api.skeleton,array)||!mmd_api::At(array,IL2CPP_ARRAY_LEN,count)||count==0||count>8192) {
    error=u8"当前角色未提供完整 Avatar 骨架";return false;
  }
  mmd::AvatarSkeleton skeleton;
  for(uintptr_t i=0;i<count;++i) {
    auto element=(char*)array+IL2CPP_ARRAY_DATA+i*api.stride;
    void *name=nullptr;mmd::AvatarBone b;char text[512]{};
    if(!mmd_api::At(element,api.name,name)||ReadStrUtf8(name,text,sizeof(text))<=0||
       !mmd_api::At(element,api.position,b.position)||!mmd_api::At(element,api.rotation,b.rotation)||
       !mmd_api::At(element,api.scale,b.scale)){error=u8"Avatar 骨架数据不完整";return false;}
    skeleton[text].push_back(b);
  }
  return mmd::CalibrateAvatar(profile,skeleton,error);
}
} // namespace mmd_avatar
