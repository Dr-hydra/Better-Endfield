#pragma once
#include "core/game_hooks.h"
#include "core/frame_driver.h"
#include "math/eye_gaze_profile.h"
#include <filesystem>
#include <fstream>
#include <map>

// All access is under g_poseMutex, in the existing SMC/camera game callbacks.
// UI only changes settings. No camera objects or cross-actor commands are kept.
namespace poser_gaze {
struct Binding {
  void *owner=nullptr,*head=nullptr,*eyes[2]={},*parents[2]={};
  uint64_t generation=0;
  eye_gaze::Basis basis;
  eye_gaze::Basis fallbackBasis;
  eye_gaze::Profile defaults;
  Quat neutralHeadInverse;
  bool pmxReference=false,estimatedLimits=false;
  Quat eyeInHead[2];
  bool opticalReady=false;
};
struct Lease {bool active=false;uint32_t owner=0,eyes[2]={};Quat original[2];};
struct Context {
  eye_gaze::Settings settings;
  Binding binding;
  Lease lease;
  std::string modelKey;
  const char *status=u8"跟随游戏或 MMD 眼神";
};
static Context editor;
static auto &settings=editor.settings;
static auto &binding=editor.binding;
static auto &lease=editor.lease;
static auto &status=editor.status;
// Under g_poseMutex after initialization; actor switches discard leases, never
// another character's preferences. File I/O occurs only at startup/UI save.
static std::map<std::string,eye_gaze::Profile> profiles;
static std::string profileError;
static eye_gaze::Profile ProfileFor(const Context &context=editor) {
  auto found=profiles.find(context.modelKey);
  return found!=profiles.end()?found->second:context.binding.defaults;
}
static void LoadProfiles() {
  try {
    std::filesystem::path path(PoserFilePath(L"mmd\\eye-gaze.json"));
    if(!std::filesystem::exists(path))return;
    if(std::filesystem::file_size(path)>1024*1024)throw std::runtime_error("Gaze settings too large");
    std::ifstream f(path);nlohmann::json j;f>>j;
    if(j.value("version",0)!=1||!j.at("models").is_object()||j.at("models").size()>1024)
      throw std::runtime_error("Invalid gaze settings file");
    std::map<std::string,eye_gaze::Profile> parsed;
    for(auto it=j.at("models").begin();it!=j.at("models").end();++it) {
      if(it.key().empty()||it.key().size()>256)throw std::runtime_error("Invalid gaze model key");
      parsed.emplace(it.key(),eye_gaze::ReadProfile(it.value()));
    }
    profiles=std::move(parsed);profileError.clear();
  } catch(const std::exception &e){profileError=u8"眼睛设置读取失败，暂用默认值";Log("[GAZE] load failed: %s",e.what());}
}
static bool SaveProfiles() {
  try {
    std::filesystem::path path(PoserFilePath(L"mmd\\eye-gaze.json")),temp=path;temp+=L".tmp";
    std::filesystem::create_directories(path.parent_path());
    nlohmann::json j={{"version",1},{"models",nlohmann::json::object()}};
    for(const auto &p:profiles)j["models"][p.first]=eye_gaze::WriteProfile(p.second);
    {std::ofstream f(temp,std::ios::binary|std::ios::trunc);f<<j.dump(2);f.flush();
      if(!f)throw std::runtime_error("Could not write gaze settings");}
    if(!MoveFileExW(temp.c_str(),path.c_str(),MOVEFILE_REPLACE_EXISTING|MOVEFILE_WRITE_THROUGH))
      throw std::runtime_error("Could not replace gaze settings");
    profileError.clear();return true;
  } catch(const std::exception &e){profileError=u8"眼睛设置未保存，请重试";Log("[GAZE] save failed: %s",e.what());return false;}
}
static void ApplyReference(Context &context,const eye_gaze::Reference *reference,Vec3 forward) {
  auto &b=context.binding;b.basis=b.fallbackBasis;b.defaults={};b.pmxReference=b.estimatedLimits=false;
  if(!reference||!reference->ready||!b.basis.ready||!eye_gaze::Finite(forward))return;
  forward=Norm(forward-b.basis.right*Dot(forward,b.basis.right));
  if(Len(forward)<.9f||Dot(forward,b.basis.forward)<.7f)return;
  auto up=Norm(Cross(forward,b.basis.right));
  if(Dot(up,b.basis.up)<0)up=up*-1.f;
  b.basis.forward=forward;b.basis.up=up;b.defaults.limits=reference->limits;
  b.opticalReady=b.pmxReference=true;b.estimatedLimits=reference->estimatedLimits;
}
// MMD preference survives neutral-face capture. Each actor receives it through
// the same immutable, generation-checked mailbox as that actor's expression.
static bool motionLock=false;
static float motionStrength=1;
static Vec3 cameraPosition;
static double cameraTime=-1e30;
static bool Read(void *method,void *object,void *value,size_t size) {
  if(!method||!UnityObjAlive(object)||RuntimeClosing())return false;
  __try {
    void *error=nullptr,*box=il2cpp_runtime_invoke(method,object,nullptr,&error);
    if(error||!box)return false;memcpy(value,(char*)box+16,size);return true;
  } __except(1){return false;}
}
static bool Write(void *object,Quat q) {
  if(!g_transform_set_localRotation||!UnityObjAlive(object)||RuntimeClosing())return false;
  __try {void *error=nullptr,*args[]={&q};il2cpp_runtime_invoke(g_transform_set_localRotation,object,args,&error);return !error;}
  __except(1){return false;}
}
static bool Owned(Context &context=editor) {
  auto &lease=context.lease;auto &binding=context.binding;
  if(!lease.active||RuntimeClosing()||!il2cpp_gchandle_get_target)return false;
  __try {return il2cpp_gchandle_get_target(lease.owner)==binding.owner&&UnityObjAlive(binding.owner);}
  __except(1){return false;}
}
static void Release(bool restore=true,const Quat *fallback=nullptr,Context &context=editor) {
  auto &lease=context.lease;auto &binding=context.binding;
  if(restore&&Owned(context))for(int i=0;i<2;++i)
    if(il2cpp_gchandle_get_target(lease.eyes[i])==binding.eyes[i])Write(binding.eyes[i],fallback?fallback[i]:lease.original[i]);
  if(!RuntimeClosing()&&il2cpp_gchandle_free) {
    for(auto ref:lease.eyes)if(ref)il2cpp_gchandle_free(ref);
    if(lease.owner)il2cpp_gchandle_free(lease.owner);
  }
  lease={};
}
static void Reset(bool restore=true,Context &context=editor) {
  Release(restore,nullptr,context);context={};
}
static void SetCamera(void *camera) {
  cameraTime=-1e30;
  if(!UnityObjAlive(camera)||!g_component_get_transform)return;
  __try {
    void *error=nullptr,*t=il2cpp_runtime_invoke(g_component_get_transform,camera,nullptr,&error);
    if(!error&&Read(g_transform_get_position,t,&cameraPosition,sizeof(cameraPosition))&&eye_gaze::Finite(cameraPosition))cameraTime=FrameNow();
  } __except(1){}
}
static bool Capture(Context &context=editor) {
  auto &lease=context.lease;auto &binding=context.binding;
  if(!il2cpp_gchandle_new||!il2cpp_gchandle_free||!il2cpp_gchandle_get_target)return false;
  for(int i=0;i<2;++i)if(!Read(g_transform_get_localRotation,binding.eyes[i],&lease.original[i],sizeof(Quat)))return false;
  lease.owner=il2cpp_gchandle_new(binding.owner,false);
  for(int i=0;i<2;++i)lease.eyes[i]=il2cpp_gchandle_new(binding.eyes[i],false);
  lease.active=lease.owner&&lease.eyes[0]&&lease.eyes[1];
  if(!lease.active)Release(false,nullptr,context);
  return lease.active;
}
static void Update(bool allowed,uint64_t generation,const Quat *fallback=nullptr,
                   Context &context=editor,void *actor=nullptr,const eye_gaze::Settings *overrideSettings=nullptr) {
  auto &lease=context.lease;auto &binding=context.binding;auto &status=context.status;
  const auto &settings=overrideSettings?*overrideSettings:context.settings;
  if(RuntimeClosing()){Release(false,nullptr,context);return;}
  if(CharacterSwitchInProgress())return;
  if(binding.owner!=(actor?actor:g_charAnimator)||binding.generation!=generation) {Reset(true,context);return;}
  if(settings.mode==eye_gaze::Mode::Follow||!allowed) {
    Release(true,fallback,context);status=allowed?u8"跟随游戏或 MMD 眼神":u8"冻结角色或播放动作后生效";return;
  }
  if(!binding.basis.ready||!UnityObjAlive(binding.owner)||!UnityObjAlive(binding.head)) {
    Release(true,nullptr,context);status=u8"当前角色的眼睛控制尚未就绪";return;
  }
  for(int i=0;i<2;++i)if(!UnityObjAlive(binding.eyes[i])||!UnityObjAlive(binding.parents[i])) {Reset(false,context);return;}
  if(settings.mode==eye_gaze::Mode::Camera&&!binding.opticalReady) {
    Release(true,fallback,context);status=u8"缺少虹膜参考点，请使用手动方向";return;
  }
  if(settings.mode==eye_gaze::Mode::Camera&&FrameNow()-cameraTime>.5) {
    Release(true,fallback,context);status=u8"等待当前镜头，暂时跟随原眼神";return;
  }
  Quat head,parents[2];Vec3 points[2];
  if(!Read(g_transform_get_rotation,binding.head,&head,sizeof(head)))return;
  for(int i=0;i<2;++i)if(!Read(g_transform_get_rotation,binding.parents[i],&parents[i],sizeof(Quat))||
      !Read(g_transform_get_position,binding.eyes[i],&points[i],sizeof(Vec3)))return;
  if(!std::isfinite(QuatLen(head))||QuatLen(head)<.5f)return;
  for(int i=0;i<2;++i)if(!std::isfinite(QuatLen(parents[i]))||QuatLen(parents[i])<.5f||!eye_gaze::Finite(points[i]))return;
  if(lease.active&&!Owned(context)){Reset(false,context);return;}
  if(!lease.active&&!Capture(context)){status=u8"无法保存眼睛原始方向，未接管";return;}
  auto inverseHead=Conj(NormQ(head));auto center=(points[0]+points[1])*.5f;
  auto direction=inverseHead*(cameraPosition-center);
  Vec3 offsets[2]={inverseHead*(points[0]-center),inverseHead*(points[1]-center)};
  Quat eyeDelta[2];eye_gaze::AimEyes(binding.basis,settings,direction,offsets,eyeDelta);
  for(int i=0;i<2;++i) {
    auto q=NormQ(Conj(NormQ(parents[i]))*NormQ(head)*eyeDelta[i]*binding.eyeInHead[i]);
    float strength=std::isfinite(settings.strength)?(std::max)(0.f,(std::min)(1.f,settings.strength)):1.f;
    q=Quat::Slerp(fallback?fallback[i]:lease.original[i],q,strength);
    if(strength>0) {
      // The final bound includes authored motion blended into the gaze and
      // per-eye convergence. Work in neutral head space, never Euler bone axes.
      auto delta=NormQ(inverseHead*NormQ(parents[i])*q*Conj(binding.eyeInHead[i]));
      delta=eye_gaze::ClampDelta(binding.basis,delta,settings.profile.limits);
      q=NormQ(Conj(NormQ(parents[i]))*NormQ(head)*delta*binding.eyeInHead[i]);
    }
    if(!Write(binding.eyes[i],q)){Release(true,fallback,context);status=u8"眼睛写入失败，已恢复";return;}
  }
  status=settings.mode==eye_gaze::Mode::Manual?u8"手动眼睛方向（覆盖 MMD 眼神）":u8"眼神已锁定当前摄像机";
}
} // namespace poser_gaze
