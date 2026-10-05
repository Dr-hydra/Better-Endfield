#pragma once
#include "math/face_mixing.h"
#include "math/eye_gaze_profile.h"
#include <memory>
#include <set>

namespace character_face {
constexpr int MaxMorphs=512;
struct Bone {std::string name;Vec3 rest;};
struct Delta {int bone=-1;Vec3 position,rotation;}; // rotation vector, source global radians
struct Morph {
  std::string name,reason;
  float residual=1;
  bool supported=false;
  std::vector<Delta> deltas;
  int panel=0; // PMX: 1 eyebrows, 2 eyes, 3 mouth, 4 other; 0 legacy/unspecified
};
struct Profile {
  std::string key,label,sourceHash;
  eye_gaze::Reference gaze;
  std::vector<Bone> bones;
  std::vector<Morph> morphs;
  std::map<std::string,int> names;
};
// PMX authors often use half-width katakana to fit VMD's 15-byte names.
// Fold spelling only for lookup; keep original names for labels/manual maps.
inline std::string MorphSpelling(std::string name) {
  name=mmd::Name(std::move(name));
  auto replace=[&](const std::string &from,const std::string &to) {
    size_t at=0;
    while((at=name.find(from,at))!=name.npos){name.replace(at,from.size(),to);at+=to.size();}
  };
  const std::string half=u8"｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ";
  const std::string wide=u8"。「」、・ヲァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワン゙゚";
  for(size_t i=0;i<half.size();i+=3)replace(half.substr(i,3),wide.substr(i,3));
  const std::string base=u8"ウカキクケコサシスセソタチツテトハヒフヘホワヰヱヲ";
  const std::string voiced=u8"ヴガギグゲゴザジズゼゾダヂヅデドバビブベボヷヸヹヺ";
  for(size_t i=0;i<base.size();i+=3)replace(base.substr(i,3)+u8"゙",voiced.substr(i,3));
  const std::string h=u8"ハヒフヘホ",p=u8"パピプペポ";
  for(size_t i=0;i<h.size();i+=3)replace(h.substr(i,3)+u8"゚",p.substr(i,3));
  return name;
}
inline int FindMorph(const Profile &profile,const std::string &name) {
  auto exact=profile.names.find(mmd::Name(name));
  if(exact!=profile.names.end())return exact->second;
  auto spelling=MorphSpelling(name);int found=-1;
  for(int i=0;i<int(profile.morphs.size());++i)if(MorphSpelling(profile.morphs[i].name)==spelling) {
    if(found>=0)return -1; // Distinct original shapes must remain selectable.
    found=i;
  }
  return found;
}
inline std::string ModelKey(std::string key) {
  key=face_geometry::Canonical(key);
  auto p=key.find("(clone)");if(p!=key.npos)key.resize(p);
  p=key.find("_postmodel");if(p!=key.npos)key.resize(p);
  if(key.find("chr_")==0) {p=key.find('_',4);if(p!=key.npos)key=key.substr(p+1);}
  return key;
}
inline Vec3 ReadVec(const nlohmann::json &v,float limit) {
  if(!v.is_array()||v.size()!=3)throw std::runtime_error("Invalid facial vector");
  Vec3 r{v[0].get<float>(),v[1].get<float>(),v[2].get<float>()};
  if(!std::isfinite(Len(r))||Len(r)>limit)throw std::runtime_error("Facial vector out of range");
  return r;
}
inline Profile Read(const nlohmann::json &j) {
  if(j.value("version",0)!=1)throw std::runtime_error("Unsupported character face profile");
  Profile p;p.key=ModelKey(j.at("model").get<std::string>());p.label=j.at("label").get<std::string>();
  p.sourceHash=j.value("source_hash",std::string{});
  if(j.contains("gaze"))p.gaze=eye_gaze::ReadReference(j.at("gaze"));
  if(p.key.empty()||p.key.size()>128||p.label.size()>512)throw std::runtime_error("Invalid character face identity");
  const auto &bones=j.at("bones"),&morphs=j.at("morphs");
  if(!bones.is_array()||bones.empty()||bones.size()>face_geometry::MaxBones||
     !morphs.is_array()||morphs.size()>MaxMorphs)throw std::runtime_error("Invalid character face count");
  std::set<std::string> unique;
  for(const auto &b:bones) {
    Bone n{face_geometry::Canonical(b.at("name").get<std::string>()),ReadVec(b.at("rest"),1000)};
    if(n.name.empty()||n.name.size()>128||!unique.insert(n.name).second)throw std::runtime_error("Duplicate facial bone");
    // Never allow a fitted expression to translate the whole skull/model.
    if(face_mixing::BoneRegion(n.name)<0)throw std::runtime_error("Non-facial bone in character profile");
    p.bones.push_back(n);
  }
  for(const auto &v:morphs) {
    Morph m;m.name=mmd::Name(v.at("name").get<std::string>());m.reason=v.value("reason",std::string{});
    m.panel=v.value("panel",0);
    if(m.panel<0||m.panel>4)throw std::runtime_error("Invalid facial morph panel");
    m.supported=v.value("supported",false);m.residual=v.value("residual",1.f);
    if(m.name.empty()||m.name.size()>512||m.reason.size()>2048||!std::isfinite(m.residual)||m.residual<0)
      throw std::runtime_error("Invalid facial morph");
    if(!p.names.emplace(m.name,int(p.morphs.size())).second)throw std::runtime_error("Duplicate facial morph");
    const auto &deltas=v.at("deltas");
    if(!deltas.is_array()||deltas.size()>p.bones.size())throw std::runtime_error("Invalid facial delta count");
    std::set<int> used;
    for(const auto &d:deltas) {
      Delta out;out.bone=d.at("bone").get<int>();
      if(out.bone<0||out.bone>=int(p.bones.size())||!used.insert(out.bone).second)throw std::runtime_error("Invalid facial delta bone");
      out.position=ReadVec(d.at("position"),5);out.rotation=ReadVec(d.at("rotation"),1.6f);
      m.deltas.push_back(out);
    }
    if(m.supported&&m.deltas.empty())throw std::runtime_error("Empty supported facial morph");
    p.morphs.push_back(std::move(m));
  }
  return p;
}
struct Binding {
  bool ready=false;
  int matched=0,usableCount=0;
  float scale=0,error=0;
  std::string status;
  mmd::Matrix basis;
  float handedness=1;
  std::vector<int> slots;
  std::vector<bool> usable;
  face_mixing::Pose neutral;
};
inline Binding Bind(const Profile &p,const std::string &model,
                    const std::vector<face_geometry::Bone> &nodes,const face_mixing::Hierarchy &h) {
  Binding b;b.status=u8"角色专属校准与当前骨架不匹配";
  if(ModelKey(model)!=p.key||!h.ready||h.count!=int(nodes.size()))return b;
  std::map<std::string,int> targets;
  for(int i=0;i<int(nodes.size());++i)if(!nodes[i].name.empty())targets.emplace(face_geometry::Canonical(nodes[i].name),i);
  b.slots.assign(p.bones.size(),-1);b.usable.assign(p.morphs.size(),false);
  Vec3 xmean,ymean;
  for(int i=0;i<int(p.bones.size());++i) {
    auto t=targets.find(p.bones[i].name);if(t==targets.end())continue;
    b.slots[i]=t->second;++b.matched;xmean=xmean+p.bones[i].rest;ymean=ymean+nodes[t->second].neutral.position();
  }
  if(b.matched<12){b.status=u8"匹配的面部控制点不足";return b;}
  xmean=xmean*(1.f/b.matched);ymean=ymean*(1.f/b.matched);
  double cov[3][3]={},cross[3][3]={};double span=0;
  for(int i=0;i<int(p.bones.size());++i)if(b.slots[i]>=0) {
    Vec3 x=p.bones[i].rest-xmean,y=nodes[b.slots[i]].neutral.position()-ymean;
    double xx[]={x.x,x.y,x.z},yy[]={y.x,y.y,y.z};span+=(double)Dot(y,y);
    for(int a=0;a<3;++a)for(int c=0;c<3;++c){cov[a][c]+=xx[a]*xx[c];cross[a][c]+=yy[a]*xx[c];}
  }
  // Double precision solve, retaining reflections between PMX and Unity bases.
  double augmented[3][6]={};
  for(int r=0;r<3;++r){for(int c=0;c<3;++c)augmented[r][c]=cov[r][c];augmented[r][r+3]=1;}
  for(int c=0;c<3;++c) {
    int pivot=c;for(int r=c+1;r<3;++r)if(std::fabs(augmented[r][c])>std::fabs(augmented[pivot][c]))pivot=r;
    if(std::fabs(augmented[pivot][c])<1e-10)return b;
    for(int k=0;k<6;++k)std::swap(augmented[c][k],augmented[pivot][k]);
    double d=augmented[c][c];for(double &x:augmented[c])x/=d;
    for(int r=0;r<3;++r)if(r!=c){d=augmented[r][c];for(int k=0;k<6;++k)augmented[r][k]-=d*augmented[c][k];}
  }
  Vec3 columns[3];
  for(int c=0;c<3;++c) {
    float v[3]={};for(int r=0;r<3;++r)for(int k=0;k<3;++k)v[r]+=float(cross[r][k]*augmented[k][c+3]);
    columns[c]={v[0],v[1],v[2]};
  }
  b.scale=(Len(columns[0])+Len(columns[1])+Len(columns[2]))/3;
  if(b.scale<.001f||b.scale>1)return b;
  for(int a=0;a<3;++a) {
    if(std::fabs(Len(columns[a])/b.scale-1)>.12f)return b;
    for(int c=a+1;c<3;++c)if(std::fabs(Dot(Norm(columns[a]),Norm(columns[c])))>.12f)return b;
  }
  Vec3 x=Norm(columns[0]),y=Norm(columns[1]-x*Dot(x,columns[1])),z=Norm(Cross(x,y));
  b.handedness=Dot(z,columns[2])<0?-1.f:1.f;z=z*b.handedness;
  Vec3 axes[]={x,y,z};
  for(int c=0;c<3;++c){b.basis.m[c*4]=axes[c].x;b.basis.m[c*4+1]=axes[c].y;b.basis.m[c*4+2]=axes[c].z;}
  double error=0;
  for(int i=0;i<int(p.bones.size());++i)if(b.slots[i]>=0) {
    Vec3 difference=face_geometry::Vector(b.basis,p.bones[i].rest-xmean)*b.scale+ymean-nodes[b.slots[i]].neutral.position();
    error+=Dot(difference,difference);
  }
  b.error=float(std::sqrt(error/(std::max)(span,1e-20)));
  if(b.error>.06f){b.status=u8"中性脸形状不匹配，请重新校准";return b;}
  b.neutral=face_mixing::Globals(h,h.rest);
  for(int i=0;i<int(p.morphs.size());++i) {
    bool usable=p.morphs[i].supported;
    for(const auto &d:p.morphs[i].deltas)if(b.slots[d.bone]<0)usable=false;
    b.usable[i]=usable;b.usableCount+=usable;
  }
  b.ready=b.usableCount>0;b.status=b.ready?u8"角色专属映射已就绪":u8"该模型没有可用的骨骼表情校准";
  return b;
}
inline bool Evaluate(const Profile &p,const Binding &b,const face_mixing::Hierarchy &h,
                     const std::array<float,MaxMorphs> &weights,float strength,face_mixing::Pose &out) {
  if(!b.ready||!h.ready)return false;
  std::array<Vec3,face_geometry::MaxBones> move{},turn{};
  for(int i=0;i<int(p.morphs.size());++i)if(b.usable[i]) {
    float w=face_geometry::Clamp(weights[i],0,1)*face_geometry::Clamp(strength,0,4);
    if(w==0)continue;
    for(const auto &d:p.morphs[i].deltas){int slot=b.slots[d.bone];move[slot]=move[slot]+d.position*w;turn[slot]=turn[slot]+d.rotation*w;}
  }
  face_mixing::Pose desired=b.neutral;
  std::array<bool,face_geometry::MaxBones> controlled{};
  for(int slot:b.slots)if(slot>=0)controlled[slot]=true;
  for(int i=0;i<h.count;++i)if(controlled[i]) {
    desired[i].position=desired[i].position+face_geometry::Vector(b.basis,move[i])*b.scale;
    Vec3 axis=face_geometry::Vector(b.basis,turn[i])*b.handedness;
    float angle=Len(axis);if(angle>1e-7f)desired[i].rotation=NormQ(Quat::AxisAngle(axis,angle)*desired[i].rotation);
  }
  // Reconstruct locals in the actual target hierarchy. Unmatched children keep
  // their local neutral pose and inherit their real parent's motion.
  std::array<mmd::Matrix,face_geometry::MaxBones> matrices;
  std::array<Quat,face_geometry::MaxBones> rotations;
  out=h.rest;
  for(int n=0;n<h.count;++n) {
    int i=h.order[n],parent=h.parent[i];const auto &matrix=parent>=0?matrices[parent]:h.external[i];
    Quat rotation=parent>=0?rotations[parent]:h.externalRotation[i];
    if(controlled[i]) {
      mmd::Matrix inverse;if(!mmd::Inverse(matrix,inverse))return false;
      out[i].position=face_geometry::Vector(inverse,desired[i].position)+inverse.position();
      out[i].rotation=NormQ(Conj(rotation)*desired[i].rotation);
    }
    matrices[i]=matrix*mmd::TRS(out[i].position,out[i].rotation,h.scale[i]);
    rotations[i]=NormQ(rotation*out[i].rotation);
  }
  return true;
}
} // namespace character_face
