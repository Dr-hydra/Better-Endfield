#pragma once
#include "game/mmd_runtime_api.h"
#include "math/mmd_terrain.h"

namespace mmd_terrain {
struct Settings {bool enabled=false;float strength=1;};
struct Api {
  bool initialized=false,ready=false;
  void *raycast=nullptr;
  int hitSize=0,point=-1,normal=-1,distance=-1;
};
static Api api;
inline void *RaycastMethod(void *cls,const char *name,bool isStatic) {
  return mmd_api::Method(cls,name,"System.Boolean",{"UnityEngine.Vector3","UnityEngine.Vector3",
    "UnityEngine.RaycastHit&","System.Single","System.Int32","UnityEngine.QueryTriggerInteraction"},isStatic);
}
inline bool Init() {
  if(api.initialized)return api.ready;api.initialized=true;
  auto hit=mmd_api::Class("UnityEngine","RaycastHit");
  api.raycast=RaycastMethod(mmd_api::Class("UnityEngine","Physics"),"Raycast",true);
  if(!hit||!il2cpp_class_value_size)return false;
  uint32_t alignment=0;api.hitSize=il2cpp_class_value_size(hit,&alignment);
  api.point=mmd_api::Field(hit,"m_Point","UnityEngine.Vector3");
  api.normal=mmd_api::Field(hit,"m_Normal","UnityEngine.Vector3");
  api.distance=mmd_api::Field(hit,"m_Distance","System.Single");
  int header=api.point==16?16:api.point==0?0:-1;if(header<0)return false;
  api.point-=header;api.normal-=header;api.distance-=header;
  api.ready=api.hitSize>0&&api.hitSize<=128&&api.point>=0&&api.point+12<=api.hitSize&&
    api.normal>=0&&api.normal+12<=api.hitSize&&api.distance>=0&&api.distance+4<=api.hitSize;
  Log("[MMD-TERRAIN] ray layout available=%d size=%d physics=%p",api.ready,api.hitSize,api.raycast);
  return api.ready;
}
struct Runtime {
  mmd::terrain::State state;
  std::array<mmd::terrain::Plane,2> planes;
  std::array<Vec3,2> previousQuery;
  void *grounder=nullptr,*groundingClass=nullptr,*delegateClass=nullptr,*delegateInvoke=nullptr;
  int solver=-1,layers=-1,ray=-1;
  bool configured=false,wasEnabled=false;
  double nextProbe=0;
  uint64_t epoch=1,lastProbeEpoch=0,queries=0;
  float rootOffset=0;
  int contacts=0;
  const char *status=u8"地形跟随已关闭";
};
inline void Configure(Runtime &r,void *grounder) {
  r.grounder=grounder;r.configured=true;
  if(!Init()||!UnityObjAlive(grounder)){r.status=u8"未找到角色贴地组件，保留原动作";return;}
  r.solver=mmd_api::Field(il2cpp_object_get_class(grounder),"solver","RootMotion.FinalIK.Grounding");
  if(r.solver<0){r.status=u8"贴地组件接口不匹配，保留原动作";return;}
  void *grounding=nullptr;if(!mmd_api::At(grounder,r.solver,grounding)||!grounding)return;
  r.groundingClass=il2cpp_object_get_class(grounding);
  r.layers=mmd_api::Field(r.groundingClass,"layers","UnityEngine.LayerMask");
  if(r.layers<0)r.layers=mmd_api::Field(r.groundingClass,"layers","System.Int32");
  // Resolve the optional game-specific raycast delegate by its exact Invoke
  // signature. If absent, Unity Physics uses the same native ground-layer mask.
  for(void *cls=r.groundingClass;cls;cls=il2cpp_class_get_parent(cls)) {
    void *it=nullptr;
    while(auto f=il2cpp_class_get_fields(cls,&it)) {
      auto name=il2cpp_field_get_name(f);
      if(!name||(!strstr(name,"Raycast")&&!strstr(name,"raycast"))||il2cpp_field_get_flags(f)&0x10)continue;
      auto offset=il2cpp_field_get_offset(f);void *delegate=nullptr;
      if(offset<16||offset>65536||!mmd_api::At(grounding,int(offset),delegate)||!delegate)continue;
      auto type=il2cpp_object_get_class(delegate);auto method=RaycastMethod(type,"Invoke",false);
      if(method){r.ray=int(offset);r.delegateClass=type;r.delegateInvoke=method;break;}
    }
    if(r.ray>=0)break;
  }
  Log("[MMD-TERRAIN] grounder=%p solver=%d layers=%d delegate=%d",grounder,r.solver,r.layers,r.ray);
}
inline bool Probe(Runtime &r,Vec3 query,float distance,int mask,void *method,void *receiver,mmd::terrain::Hit &out) {
  out={};if(!mmd::terrain::Finite(query)||distance<=0||!mask||!method)return false;
  alignas(16) unsigned char hit[128]{};Vec3 down{0,-1,0};int ignoreTriggers=1;
  void *args[]={&query,&down,hit,&distance,&mask,&ignoreTriggers},*result=nullptr;
  ++r.queries;
  if(!mmd_api::Call(method,receiver,args,result)||!result)return false;
  bool present=false;float traveled=0;
  if(!mmd_api::At(result,16,present)||!present)return true;
  if(!mmd_api::At(hit,api.point,out.point)||!mmd_api::At(hit,api.normal,out.normal)||
     !mmd_api::At(hit,api.distance,traveled))return false;
  out.valid=mmd::terrain::Finite(out.point)&&mmd::terrain::Finite(out.normal)&&
    std::isfinite(traveled)&&traveled>=0&&traveled<=distance+.01f&&out.normal.y>.35f;
  if(out.valid)out.normal=Norm(out.normal);
  return true;
}
inline float Apply(Runtime &r,const Settings &settings,const mmd::RetargetProfile &profile,
                   mmd::SampledPose &pose,const mmd::Matrix &baseModel,const mmd::Matrix &anchor,
                   double now,double motion) {
  if(!settings.enabled||!profile.valid()||pose.worldPos.size()!=profile.bones.size()) {
    r.state={};r.rootOffset=0;r.contacts=0;r.wasEnabled=false;r.status=u8"地形跟随已关闭";return 0;
  }
  if(!r.wasEnabled){r.state={};r.nextProbe=0;r.wasEnabled=true;++r.epoch;}
  if(!api.ready||r.solver<0||r.layers<0||!UnityObjAlive(r.grounder)) {
    r.status=u8"地面探测不可用，保留原动作";r.rootOffset=0;return 0;
  }
  mmd::terrain::Input in;in.now=now;in.motion=motion;in.epoch=r.epoch;
  in.referenceY=anchor.position().y;in.strength=settings.strength;
  for(int i=0;i<2;++i) {
    int a=profile.roles[1+i],b=profile.roles[3+i],c=profile.roles[5+i];
    in.ankle[i]=mmd::terrain::Point(baseModel,pose.worldPos[c]);
    in.hip[i]=mmd::terrain::Point(baseModel,pose.worldPos[a]);
    auto knee=mmd::terrain::Point(baseModel,pose.worldPos[b]);
    in.legLength[i]=Len(knee-in.hip[i])+Len(in.ankle[i]-knee);
    auto rest=mmd::terrain::Point(anchor,profile.bones[c].restPos);
    in.clearance[i]=mmd::Clamp(rest.y-in.referenceY,in.legLength[i]*.015f,in.legLength[i]*.25f);
  }
  bool moved=false;for(int i=0;i<2;++i)moved|=Len(in.ankle[i]-r.previousQuery[i])>.15f;
  if(now>=r.nextProbe||moved||r.epoch!=r.lastProbeEpoch||motion<r.state.lastMotion) {
    r.nextProbe=now+.04;r.lastProbeEpoch=r.epoch;
    void *grounding=nullptr,*receiver=nullptr,*method=api.raycast;int mask=0;
    bool usable=mmd_api::At(r.grounder,r.solver,grounding)&&grounding&&
      il2cpp_object_get_class(grounding)==r.groundingClass&&mmd_api::At(grounding,r.layers,mask)&&mask!=0;
    if(usable&&r.ray>=0) {
      void *delegate=nullptr;
      if(mmd_api::At(grounding,r.ray,delegate)&&delegate&&il2cpp_object_get_class(delegate)==r.delegateClass)
        {receiver=delegate;method=r.delegateInvoke;}
    }
    bool apiFailed=!usable||!method;
    for(int i=0;i<2;++i) {
      r.previousQuery[i]=in.ankle[i];r.planes[i]={};
      if(apiFailed)continue;
      float radius=mmd::Clamp(in.legLength[i]*.07f,.03f,.08f),rise=mmd::Clamp(in.legLength[i]*.9f,.5f,1.f);
      const Vec3 offsets[]={{},{radius,0,0},{-radius,0,0},{0,0,radius},{0,0,-radius}};
      std::array<mmd::terrain::Hit,5> hits;
      auto center=in.ankle[i];center.y+=r.rootOffset;
      for(int k=0;k<5;++k)if(!Probe(r,center+offsets[k]+Vec3{0,rise,0},rise+in.legLength[i]*1.3f,mask,method,receiver,hits[k]))apiFailed=true;
      r.planes[i]=mmd::terrain::Aggregate(hits,center,mmd::Clamp(in.legLength[i]*.04f,.025f,.06f));
    }
    r.status=apiFailed?u8"地面探测失败，短暂保持后恢复原动作":u8"地形跟随中";
  }
  in.planes=r.planes;auto result=mmd::terrain::Step(r.state,in);
  r.rootOffset=result.rootOffset;r.contacts=int(result.contact[0])+int(result.contact[1]);
  auto model=baseModel;model.m[13]+=result.rootOffset;
  mmd::terrain::SolveFeet(profile,pose,model,result);
  if(!r.planes[0].valid&&!r.planes[1].valid)r.status=u8"未探测到可站立地面";
  return r.rootOffset;
}
} // namespace mmd_terrain
