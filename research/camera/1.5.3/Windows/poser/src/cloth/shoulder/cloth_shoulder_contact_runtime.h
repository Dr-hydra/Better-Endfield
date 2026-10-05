#pragma once
#include "../generated/cloth_shoulder_contact_aglina.h"
struct ClothShoulderContactRuntime {
  eiem_cloth::Owner owner{};
  unsigned command=0;
  std::vector<ClothRef> bones,parents;
  std::vector<eiem_cloth_shoulder_contact::M> world,previous;
  eiem_cloth_shoulder_contact::Workspace work;
  std::array<eiem_cloth_shoulder::Pose,10> input{},output{};
  std::array<eiem_cloth_shoulder_contact::Stats,2> stats{};
  std::array<bool,96> controlled{};
  void *getMatrix=nullptr;
  unsigned solves=0,reused=0;
  double lastMs=0,totalMs=0,maxMs=0;
  bool ready=false,cached=false;
  const char *reason="not-prepared";
};
static void ClothShoulderContactClear(ClothShoulderContactRuntime &s){
  for(auto &r:s.bones)ClothFree(r);for(auto &r:s.parents)ClothFree(r);s={};
}
static bool ClothShoulderContactRead(ClothShoulderContactRuntime &s,void *t,eiem_cloth_shoulder_contact::M &matrix){
  float value[16];for(auto &x:value)x=NAN;void *unused=nullptr,*args[]{value};
  if(!ClothInvoke(s.getMatrix,t,args,unused))return false;
  for(size_t n=0;n<16;++n){if(!std::isfinite(value[n]))return false;matrix[n]=value[n];}return true;
}
static bool ClothShoulderContactPrepare(ClothShoulderContactRuntime &s){
  using namespace eiem_cloth_shoulder_contact;
  ClothShoulderContactClear(s);const auto &source=AglinaContactSource;const auto &e=s_clothShoulderEvidence;
  s.reason="source-skin-or-matrix-ABI";
  if(!ClothOnMainThread()||!e.prepared||!ClothOwns(e.owner)||!Valid(source))return false;
  s.owner=e.owner;s.command=e.command;
  auto matrixClass=SurfaceClass("UnityEngine","Matrix4x4");
  s.getMatrix=ClothMethod(g_transformClass,"get_localToWorldMatrix_Injected","System.Void","UnityEngine.Matrix4x4&");
  if(!s.getMatrix||!matrixClass||!ClothInputLayout(matrixClass,"UnityEngine.Matrix4x4",64))return false;
  s.reason="source-bone-parent-or-bind";
  s.world.resize(source.boneCount,Identity());s.previous.resize(source.boneCount);s.work.Reserve();
  std::set<void*> unique;
  for(size_t n=0;n<source.boneCount;++n){const auto &expected=source.bones[n];size_t found=e.bones.size();
    for(size_t k=0;k<e.source.bones.size();++k)if(e.source.bones[k].bone.name==expected.name){if(found!=e.bones.size())return false;found=k;}
    if(found>=e.bones.size()||found>=e.parents.size()||e.source.bones[found].bone.parent!=expected.parent)return false;
    for(size_t k=0;k<16;++k)if(!std::isfinite(e.source.bones[found].bind[k])||std::abs(e.source.bones[found].bind[k]-expected.bind[k])>1e-6)return false;
    auto t=ClothTarget(e.bones[found]),parent=ClothTarget(e.parents[found]);
    if(!t||!parent||!unique.insert(t).second||CollisionParent(t)!=parent||!ClothAnchorUnderOwner(t))return false;
    s.bones.push_back(ClothProtect(t));s.parents.push_back(ClothProtect(parent));
    if(!s.bones.back().handle||!s.parents.back().handle)return false;
  }
  for(const auto &side:source.sides)for(int n:side.controls)s.controlled[n]=true;
  s.reason="matrix-readback-agreement";auto chest=ClothTarget(s.bones[source.chest]);M direct;
  void *box=nullptr;float boxed[16]{};
  if(!ClothShoulderContactRead(s,chest,direct)||
      !ClothInvoke(ClothMethod(g_transformClass,"get_localToWorldMatrix","UnityEngine.Matrix4x4"),chest,nullptr,box)||
      !ClothInputCopyBox(box,"UnityEngine.Matrix4x4",boxed,sizeof(boxed)))return false;
  for(size_t k=0;k<16;++k)if(!std::isfinite(boxed[k])||std::abs(direct[k]-boxed[k])>1e-6)return false;
  s.ready=true;s.reason="ready";return true;
}
static bool ClothShoulderContactApply(ClothShoulderContactRuntime &s,bool audit,std::array<eiem_cloth_shoulder::Pose,10> &target){
  using namespace eiem_cloth_shoulder_contact;
  if(!ClothOnMainThread()||!s.ready)return false;
  if(!ClothOwns(s.owner)||!(s_clothShoulderEvidence.owner==s.owner)||s.command!=s_clothShoulderEvidence.command){s.reason="contact-owner-or-command-changed";return false;}
  LARGE_INTEGER begin{},end{},frequency{};QueryPerformanceFrequency(&frequency);QueryPerformanceCounter(&begin);
  ClothShoulderFrameRefs refs;
  for(size_t n=0;n<s.bones.size();++n){auto t=refs.Get(s.bones[n]);
    if(!t||(audit&&CollisionParent(t)!=refs.Get(s.parents[n]))){s.reason="sleeve-identity-or-parent-changed";return false;}
    if(!ClothShoulderContactRead(s,t,s.world[n])){s.reason="sleeve-matrix-read-failed";return false;}}
  bool same=s.cached;
  for(size_t n=0;same&&n<s.world.size();++n)if(!s.controlled[n])for(size_t k=0;k<16;++k)if(std::abs(s.world[n][k]-s.previous[n][k])>1e-7){same=false;break;}
  for(const auto &side:AglinaContactSource.sides)for(int j=0;j<5;++j){M inverse;
    if(!Inverse(s.world[side.parents[j]],inverse)){s.reason="control-parent-matrix";return false;}
    const auto local=Mul(inverse,s.world[side.controls[j]]);
    if(!Uniform(local)||std::abs(Length({local[0],local[1],local[2]})-1)>1e-4){s.reason="control-scale-changed";return false;}}
  for(size_t n=0;same&&n<10;++n)same=target[n].position==s.input[n].position&&target[n].rotation==s.input[n].rotation;
  if(same){target=s.output;++s.reused;}
  else{auto corrected=target;
    if(!Correct(AglinaContactSource,s.world,corrected,s.work,s.stats)){s.reason="sleeve-envelope-or-uniform-basis-unavailable";return false;}
    s.previous=s.world;s.input=target;s.output=corrected;s.cached=true;target=corrected;++s.solves;}
  QueryPerformanceCounter(&end);if(frequency.QuadPart>0)s.lastMs=double(end.QuadPart-begin.QuadPart)*1000/frequency.QuadPart;
  s.totalMs+=s.lastMs;s.maxMs=(std::max)(s.maxMs,s.lastMs);return true;
}
